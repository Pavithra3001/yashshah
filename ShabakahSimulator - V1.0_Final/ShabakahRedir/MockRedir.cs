using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Honeywell.SimulationFramework.LoggerComponent;
using ShabakahEntityManager;

namespace ShabakahRedir
{
    public class MockRedir
    {
		/// <summary>
		/// Default Constants.
		/// </summary>
        /// 
        public static IPAddress m_defaultServer = IPAddress.Parse(ConfigurationSettings.AppSettings["redirip"]);
        public static int m_defaultPort = Convert.ToInt32(ConfigurationSettings.AppSettings["redirport"]);

		public static IPEndPoint m_defaultEndPoint = 
			new IPEndPoint(m_defaultServer, m_defaultPort);

		/// <summary>
		/// Local Variables Declaration.
		/// </summary>
		private TcpListener m_server = null;
		private bool m_stopServer=false;
		private bool m_stopPurging=false;
		private Thread m_serverThread = null;
		private Thread m_purgingThread = null;
		private ArrayList m_socketListenersList = null;
        private Logger m_logger;
        private List<PanelServices> PanelMAC = new List<PanelServices>();

        public static string RedirStatus = "Disconnected";

        /// <summary>
		/// Constructors.
		/// </summary>
		public MockRedir(Logger logger)
		{
            m_logger = logger;
			Init(m_defaultEndPoint);

		}
		
		/// <summary>
		/// Destructor.
		/// </summary>
        ~MockRedir()
		{
			StopServer();
		}

		/// <summary>
		/// Init method that create a server (TCP Listener) Object based on the
		/// IP Address and Port information that is passed in.
		/// </summary>
		/// <param name="ipNport"></param>
		private void Init(IPEndPoint ipNport)
		{
			try
			{
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - Init - Start");
                m_server = new TcpListener(ipNport);
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - Init - End");
            }
			catch(Exception e)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - Init - Exception - " + e.Message);
				m_server=null;
			}
		}

		/// <summary>
		/// Method that starts TCP/IP Server.
		/// </summary>
        public void StartServer()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StartServer - Start");
                if (m_server != null)
                {
                    // Create a ArrayList for storing SocketListeners before
                    // starting the server.
                    m_socketListenersList = new ArrayList();

                    // Start the Server and start the thread to listen client 
                    // requests.


                   m_server.Start();

                    //ServerThreadStart();

                    m_serverThread = new Thread(new ThreadStart(ServerThreadStart));
                    m_serverThread.Start();

                    // Create a low priority thread that checks and deletes client
                    // SocktConnection objcts that are marked for deletion.
                    m_purgingThread = new Thread(new ThreadStart(PurgingThreadStart));
                    m_purgingThread.Priority = ThreadPriority.Lowest;
                    m_purgingThread.Start();
                }
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StartServer - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StartServer - Exception - " + ex.Message);
            }
        }

		/// <summary>
		/// Method that stops the TCP/IP Server.
		/// </summary>
		public void StopServer()
		{
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StopServer - Start");
                if (m_server != null)
                {
                    // It is important to Stop the server first before doing
                    // any cleanup. If not so, clients might being added as
                    // server is running, but supporting data structures
                    // (such as m_socketListenersList) are cleared. This might
                    // cause exceptions.

                    // Stop the TCP/IP Server.
                    m_stopServer = true;
                    m_server.Stop();
                    RedirStatus = "Disconnected";
                    // Wait for one second for the the thread to stop.
                    m_serverThread.Join(1000);

                    // If still alive; Get rid of the thread.
                    if (m_serverThread.IsAlive)
                    {
                        m_serverThread.Abort();
                    }
                    m_serverThread = null;

                    m_stopPurging = true;
                    m_purgingThread.Join(1000);
                    if (m_purgingThread.IsAlive)
                    {
                        m_purgingThread.Abort();
                    }
                    m_purgingThread = null;

                    // Free Server Object.
                    m_server = null;

                    // Stop All clients.
                    StopAllSocketListers();
                }
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StopServer - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "ShabakahRedir - StopServer - " + ex.Message);
            }
		}


		/// <summary>
		/// Method that stops all clients and clears the list.
		/// </summary>
        private void StopAllSocketListers()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StopAllSocketListers - Start");
                foreach (RedirSocketListener socketListener
                             in m_socketListenersList)
                {
                    socketListener.StopSocketListener();
                }
                // Remove all elements from the list.
                m_socketListenersList.Clear();
                m_socketListenersList = null;
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StopAllSocketListers - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StopAllSocketListers - Exception - " + ex.Message);
            }
        }

		/// <summary>
		/// TCP/IP Server Thread that is listening for clients.
        /// private async Task ServerThreadStart()
		/// </summary>
		private void ServerThreadStart()
		{
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - ServerThreadStart - Start");
                // Client Socket variable;
                Socket clientSocket = null;
                RedirSocketListener socketListener = null;
                while (!m_stopServer)
                {
                    try
                    {
                        // Wait for any client requests and if there is any 
                        // request from any client accept it (Wait indefinitely).
                        clientSocket = m_server.AcceptSocket();
                        RedirStatus = "Connected";
                        //clientSocket = await m_server.AcceptSocketAsync();
                        m_logger.LogMessage(Level.TRACE, "ShabakahRedir - ServerThreadStart - ClientSocket After AcceptSocket - " + clientSocket.RemoteEndPoint);

                        // Create a SocketListener object for the client.
                        socketListener = new RedirSocketListener(clientSocket,m_logger);

                        lock (m_socketListenersList)
                        {
                            m_socketListenersList.Add(socketListener);
                        }
                        //Monitor.Exit(m_socketListenersList);

                        // Start a communicating with the client in a different
                        // thread.
                        socketListener.StartSocketListener();
                    }
                    catch (SocketException se)
                    {
                        RedirStatus = "Disconnected";
                        m_logger.LogMessage(Level.TRACE, "ShabakahRedir - ServerThreadStart - SocketException - " + se.Message);
                        m_stopServer = true;
                    }
                }
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - ServerThreadStart - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - ServerThreadStart - Exception - " + ex.Message);
            }
		}

        private void InitializeMockerPanel()
        {
            MockPanel.RecArm = 0;
            MockPanel.RespArm = 0;

            MockPanel.RecDisarm = 0;
            MockPanel.RespDisarm = 0;

        }

        public void InitializeCounter()
        {
            InitializeMockerPanel();
            foreach (var panel in PanelMAC)
            {
                panel.InitializePanelCounter();
            }
        }

        public void SendRequest(int TotPanels, int TotArmDisArm)
        {
            try
            {
                int Paneldelay = 60000 / TotPanels;
                int ArmDisArmDelay = 60000 / TotArmDisArm;       //TD - if panel is > ArmDisArm

                foreach (var ShaPanel in PanelMAC)
                {
                    Thread th = new Thread(() => ShaPanel.StartArmDisArm(ArmDisArmDelay));
                    th.Start();
                    Thread.Sleep(Paneldelay);
                    //ShaPanel.CallOpenSession();
                    //ShaPanel.StartArmDisArm();
                }
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - SendRequest - Exception - " + ex.Message);
            }
        }

        public void StopRequest()
        {
            try
            {
                foreach (var panel in PanelMAC)
                {
                    panel.StopArmDisArm();
                }
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahRedir - StopRequest - Exception - " + ex.Message);
            }
        }

        public void ConfigPanelMAC(List<PanelServices> Panels)
        {
            PanelMAC = Panels;
        }

		/// <summary>
		/// Thread method for purging Client Listeneres that are marked for
		/// deletion (i.e. clients with socket connection closed). This thead
		/// is a low priority thread and sleeps for 10 seconds and then check
		/// for any client SocketConnection obects which are obselete and 
		/// marked for deletion.
		/// </summary>
		private void PurgingThreadStart()
		{
			while (!m_stopPurging)
			{
				ArrayList deleteList = new ArrayList();

				// Check for any clients SocketListeners that are to be
				// deleted and put them in a separate list in a thread sage
				// fashon.
				//Monitor.Enter(m_socketListenersList);
				lock(m_socketListenersList)
				{
					foreach (RedirSocketListener socketListener 
								 in m_socketListenersList)
					{
						if (socketListener.IsMarkedForDeletion())
						{
							deleteList.Add(socketListener);
							socketListener.StopSocketListener();
						}
					}

					// Delete all the client SocketConnection ojects which are
					// in marked for deletion and are in the delete list.
					for(int i=0; i<deleteList.Count;++i)
					{
						m_socketListenersList.Remove(deleteList[i]);
					}
				}
				//Monitor.Exit(m_socketListenersList);

				deleteList=null;
                //Thread.Sleep(200000);
                Thread.Sleep(2000);
			}
		}
	}
}
