using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShabakahEntityManager;
using Honeywell.SimulationFramework.LoggerComponent;


namespace ShabakahRedir
{
    class RedirSocketListener
    {

        private string m_useLocalTcservice;
        private MockPanel m_mockPanel;
        private TcpClient m_TCPClient;
        private Socket m_clientSocket = null;
        private bool m_stopClient = false;
        private Thread m_clientListenerThread = null;
        private bool m_markedForDeletion = false;
        private string sUseLocalDataServer;
        private int iLocalDataServerPort;
        private string sDataServerIP;
        private int iDataServerPort;


        private StringBuilder m_oneLineBuf = new StringBuilder();
        private DateTime m_lastReceiveDateTime;
        private DateTime m_currentReceiveDateTime;
        //private LoggingHelper loggingHelper;

        private Logger m_logger;

        public RedirSocketListener(Socket clientSocket, Logger mlogger)
        {
            m_logger = mlogger;

            m_clientSocket = clientSocket;

            sUseLocalDataServer = ConfigurationSettings.AppSettings["uselocalDataServer"];
            iLocalDataServerPort = Convert.ToInt32(ConfigurationSettings.AppSettings["uselocalDataServerPort"]);
            //loggingHelper = new LoggingHelper();
        }


        /// <summary>
        /// Client SocketListener Destructor.
        /// </summary>
        ~RedirSocketListener()
        {
            StopSocketListener();
        }

        /// <summary>
        /// Method that starts SocketListener Thread.
        /// </summary>
        public void StartSocketListener()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - StartSocketListener - Start");
                if (m_clientSocket != null)
                {
                    m_clientListenerThread =
                        new Thread(new ThreadStart(SocketListenerThreadStart));

                    m_clientListenerThread.Start();
                }
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - StartSocketListener - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - StartSocketListener - Exception - " + ex.Message);
            }
        }

        //private Timer t;
        /// <summary>
        /// Thread method that does the communication with TC Service. This 
        /// thread tries to receive from TC Service

        /// </summary>
        private void SocketListenerThreadStart()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - SocketListenerThreadStart - Start");
                //loggingHelper.LogEvent("Entering SocketListenerThreadStart");
                int size = 0;
                Byte[] byteBuffer = new Byte[1024];

                m_lastReceiveDateTime = DateTime.Now;
                m_currentReceiveDateTime = DateTime.Now;

                //t = new Timer(new TimerCallback(CheckClientCommInterval),
                //   null, 60000, 60000);


                while (!m_stopClient)
                {
                    try
                    {

                        size = m_clientSocket.Receive(byteBuffer);
                        m_logger.LogMessage(Level.TRACE, "RedirSocketListener - SocketListenerThreadStart - New Session Created. request Received");
                        var tempBuffer = byteBuffer.TakeWhile((v, index) => byteBuffer.Skip(index).Any(w => w != 0x00)).ToArray();
                        //loggingHelper.LogEvent("Client Socket Received bytes",
                        //new ExtraParam { Key = "MESSAGE", Value = tempBuffer });
                        m_currentReceiveDateTime = DateTime.Now;
                        //ClientConnected(byteBuffer, size);
                        //ClientConnectedModified(byteBuffer, size);
                        Thread thread = new Thread(() => ClientConnected(byteBuffer, size));
                        thread.Start();
                    }
                    catch (SocketException se)
                    {
                        m_logger.LogMessage(Level.TRACE, "RedirSocketListener - SocketListenerThreadStart - SocketException - " + se.Message);
                        m_stopClient = true;
                        m_markedForDeletion = true;
                    }
                }
                //t.Change(Timeout.Infinite, Timeout.Infinite);
                //t = null;
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - SocketListenerThreadStart - Exception - " + ex.Message);
            }
        }

        /// <summary>
        /// Method that stops Client SocketListening Thread.
        /// </summary>
        public void StopSocketListener()
        {
            if (m_clientSocket != null)
            {
                m_stopClient = true;
                m_clientSocket.Close();

                // Wait for one second for the the thread to stop.
                m_clientListenerThread.Join(1000);

                // If still alive; Get rid of the thread.
                if (m_clientListenerThread.IsAlive)
                {
                    m_clientListenerThread.Abort();
                }
                m_clientListenerThread = null;
                m_clientSocket = null;
                m_markedForDeletion = true;
                //loggingHelper.LogEvent("Socket Listener stopped.");

                //t.Change(Timeout.Infinite, Timeout.Infinite);
                //t = null;
            }
        }


        public bool IsMarkedForDeletion()
        {
            return m_markedForDeletion;
        }

        /*
        private void ClientConnected(Byte[] byteBuffer, int size)
        {

            MemoryStream oMemoryStream = new MemoryStream(byteBuffer);
            BinaryReader oBinaryReader = new BinaryReader(oMemoryStream);
            oBinaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            char[] param1 = oBinaryReader.ReadChars(7);
            ushort cmd = oBinaryReader.ReadUInt16();
            byte[] optvid = oBinaryReader.ReadBytes(6);
            ushort sessrequest = oBinaryReader.ReadUInt16();
            ushort priority = oBinaryReader.ReadUInt16();
            ushort timelive = oBinaryReader.ReadUInt16();
            uint timout = oBinaryReader.ReadUInt32();
            ushort noext = oBinaryReader.ReadUInt16();
            int sessionid = oBinaryReader.ReadInt32();
            Thread.Sleep(500);
            int ip = oBinaryReader.ReadInt32();
            int port = oBinaryReader.ReadInt32();
            Thread.Sleep(500);
            int umode = oBinaryReader.ReadInt32();
            byte[] key256 = oBinaryReader.ReadBytes(32);
            byte[] key192 = oBinaryReader.ReadBytes(24);

            StringBuilder bs = new StringBuilder(128);
            for (int i = 0; i < 32; i++)
            {
                bs.Append(key256[i].ToString("X2"));
            }
            for (int i = 0; i < 24; i++)
            {
                bs.Append(key192[i].ToString("X2"));
            }
            if (sUseLocalDataServer == "1")
            {
                sDataServerIP = "127.0.0.1";
                iDataServerPort = iLocalDataServerPort;
            }
            else
            {
                sDataServerIP = CommonMethods.InttoIPAddress(ip);
                iDataServerPort = port;

            }
            byte[] _mac = optvid;

            //loggingHelper.LogEvent(Helper.GetMAC(_mac), "Client Connected");

            //MockPanel oMockPanel = new MockPanel(IPAddress.Parse(sTCServiceIP), iTCServicePort, ConvertOptvforTCService(optvid), optvid, bs.ToString());
        }
        */

        private void ClientConnected(Byte[] byteBuffer, int size)
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - ClientConnected - Start");
                //lock (this)
                {
                    MemoryStream oMemoryStream = new MemoryStream(byteBuffer);
                    BinaryReader oBinaryReader = new BinaryReader(oMemoryStream);
                    oBinaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

                    char[] param1 = oBinaryReader.ReadChars(7);
                    ushort cmd = oBinaryReader.ReadUInt16();
                    byte[] optvid = oBinaryReader.ReadBytes(6);
                    ushort sessrequest = oBinaryReader.ReadUInt16();
                    ushort priority = oBinaryReader.ReadUInt16();
                    ushort timelive = oBinaryReader.ReadUInt16();
                    uint timout = oBinaryReader.ReadUInt32();
                    ushort noext = oBinaryReader.ReadUInt16();
                    //int sessionid = oBinaryReader.ReadInt32();
                    byte[] sessionIdBytes = oBinaryReader.ReadBytes(4);
                    Thread.Sleep(500);
                    int ip = oBinaryReader.ReadInt32();
                    int port = oBinaryReader.ReadInt32();
                    Thread.Sleep(500);
                    int umode = oBinaryReader.ReadInt32();
                    byte[] key256 = oBinaryReader.ReadBytes(32);
                    byte[] key192 = oBinaryReader.ReadBytes(24);

                    StringBuilder bs = new StringBuilder(128);
                    for (int i = 0; i < 32; i++)
                    {
                        bs.Append(key256[i].ToString("X2"));
                    }
                    for (int i = 0; i < 24; i++)
                    {
                        bs.Append(key192[i].ToString("X2"));
                    }
                    if (sUseLocalDataServer == "1")
                    {
                        sDataServerIP = ConfigurationSettings.AppSettings["dataserverip"];
                        iDataServerPort = 443;
                    }
                    else
                    {
                        sDataServerIP = CommonMethods.InttoIPAddress(ip);
                        iDataServerPort = port;

                    }
                    byte[] _mac = optvid;

                    MockPanel oMockPanel = new MockPanel(m_logger,IPAddress.Parse(sDataServerIP), iDataServerPort, ConvertOptvforTCService(optvid), optvid, bs.ToString(), sessionIdBytes);
                }
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - ClientConnected - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "RedirSocketListener - ClientConnected - Exception - " + ex.Message);
            }
        }



        public byte[] ConvertOptvforTCService(byte[] optvid)
        {
            byte[] receivedOptvId = optvid;
            byte[] PanelSessionId = new byte[8];
            PanelSessionId[0] = Convert.ToByte("1");
            PanelSessionId[1] = optvid[0];
            PanelSessionId[2] = optvid[1];
            PanelSessionId[3] = optvid[2];
            PanelSessionId[4] = optvid[5];
            PanelSessionId[5] = optvid[4];
            PanelSessionId[6] = optvid[3];
            PanelSessionId[7] = Convert.ToByte("2");
            return PanelSessionId;
        }

        private void CheckClientCommInterval(object o)
        {
            if (m_lastReceiveDateTime.Equals(m_currentReceiveDateTime))
            {
                this.StopSocketListener();
            }
            else
            {
                m_lastReceiveDateTime = m_currentReceiveDateTime;
            }
        }


    }
}
