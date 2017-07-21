using Honeywell.SimulationFramework.LoggerComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ShabakahEntityManager
{
    public class MockPanel
    {
        ManualResetEvent clientDone = new ManualResetEvent(false);
        SocketAsyncEventArgs socketEventArgInital = new SocketAsyncEventArgs();
        SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
        SocketAsyncEventArgs socketEventArgPings = new SocketAsyncEventArgs();
        PanelConnector panelConnector = new PanelConnector();
        System.Timers.Timer t = new System.Timers.Timer();
        System.Threading.Timer PingDelay;
        //LoggingHelper loggingHelper = new LoggingHelper();
        Panel NewPanel;
        Socket sock = null;

        public static int RecArm = 0;
        public static int RespArm = 0;

        public static int RecDisarm = 0;
        public static int RespDisarm = 0;

        internal byte[] bytes;
        internal byte[] mac;
        string key;
        int dummy = 0;
        int DelayTime = Convert.ToInt32(ConfigurationSettings.AppSettings["delayIntervalSession"]);
        String MAC;
        Logger m_logger;

        public MockPanel(Logger logger,IPAddress destinationAddr, int destinationPort, byte[] _bytes, byte[] _mac, string _key, byte[] sessionId)
        {
            try
            {
                m_logger = logger;
                bytes = _bytes;
                mac = _mac;
                key = _key;
                MAC = Helper.GetMAC(_mac);
                NewPanel = new Panel(m_logger, Helper.GetMAC(_mac), false, PanelType.L7000);
                panelConnector.DoPanelConnector(key, new PanelState());

                //Initialize Timer
                PingDelay = new System.Threading.Timer(new System.Threading.TimerCallback(SocketClose), null, DelayTime*1000,DelayTime*1000);

                DoSenReceiveModified(destinationAddr, destinationPort, sessionId);

                t.Interval = 50000; // specify interval time as you want
                t.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
                t.Enabled = true;
            }
            catch (ThreadAbortException e)
            {
                //loggingHelper.LogEvent(e, Panel.MAC + " MockPanel");
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - Constructor - ThreadAbortException - " + e.Message);
            }
            catch (Exception e)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - Constructor - Exception - " + e.Message);
                //Console.WriteLine(e.Message);
                //Console.WriteLine("Usage: AsyncSocketClient.exe <destination IP address> <destination port number>");
                //loggingHelper.LogEvent(e, Panel.MAC + " MockPanel");
            }

        }

        public static void InitializeCounter()
        {
            RecArm = 0;
            RespArm = 0;

            RecDisarm = 0;
            RespDisarm = 0;
        }
        //private void DoSenReceive(IPAddress destinationAddr, int destinationPort)
        //{
        //    if (sock == null || !sock.Connected)
        //    {
        //        sock = new Socket(destinationAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //        socketEventArg.RemoteEndPoint = new IPEndPoint(destinationAddr, destinationPort);
        //        socketEventArg.UserToken = sock;

        //        socketEventArgPings.RemoteEndPoint = new IPEndPoint(destinationAddr, destinationPort);
        //        socketEventArgPings.UserToken = sock;

        //        socketEventArgInital.RemoteEndPoint = new IPEndPoint(destinationAddr, destinationPort);

        //        sock.Connect(destinationAddr, destinationPort);

        //        if (sock.Connected)
        //        {
        //            //socketEventArgInital.SetBuffer(bytes, 0, bytes.Length);
        //            ////loggingHelper.LogEvent(Panel.MAC, "Panel Socket connected");
        //            sock.Send(bytes);

        //            /*//Raghu - To Discuss 
        //             * What Message is decrypted here. 
        //             */
        //            string decrypted = "50 D7 00 00 F7 00 02 00 10 00 00 1E 28 00 00 00 00 00 00 00 00 00 00 00 41 6E 74 69 73 61 62 00 53 61 62 6F 74 61 67 65 20 63 6F 75 76 65 72 2E E7 00 00 00 AA E4 2C DD DA F3 5C 30 A7 1E D9 A7";
        //            sbyte[] dec = Helper.StringToByteArrayFastest(decrypted.Replace(" ", ""));
        //            byte[] sdec = panelConnector.sendCmd(dec, 2, dec.Length, -1);
        //            //loggingHelper.LogEvent(Panel.MAC, "Handshake completed");

        //            StartSend(sdec);

        //            Thread.Sleep(1000);
        //        }
        //    }
        //}
        
        private void DoSenReceiveModified(IPAddress destinationAddr, int destinationPort, byte[] sessionId)
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - DoSenReceiveModified - Start ");
                if (sock == null || !sock.Connected)
                {
                    sock = new Socket(destinationAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    socketEventArg.RemoteEndPoint = new IPEndPoint(destinationAddr, destinationPort);
                    socketEventArg.UserToken = sock;

                    socketEventArgPings.RemoteEndPoint = new IPEndPoint(destinationAddr, destinationPort);
                    socketEventArgPings.UserToken = sock;

                    socketEventArgInital.RemoteEndPoint = new IPEndPoint(destinationAddr, destinationPort);

                    sock.Connect(destinationAddr, destinationPort);

                    if (sock.Connected)
                    {
                        //string decrypted = "50 D7 00 00 F7 00 02 00 10 00 00 1E 28 00 00 00 00 00 00 00 00 00 00 00 41 6E 74 69 73 61 62 00 53 61 62 6F 74 61 67 65 20 63 6F 75 76 65 72 2E E7 00 00 00 AA E4 2C DD DA F3 5C 30 A7 1E D9 A7";
                        byte[] data = new byte[]{0xff, 0xff, 0xff, 0xff, 0x3e, 0x7a, 0x72, 0x7e, 0x50, 0x52, 0x53, 0x4d, 0x00, 0x00, 0x00, 0x00, 
                                            0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x8c, 0x00, 0x90, 0x4b, 0xbf, 0x75, 0x9b, 0x68, 0xa4, 0xb0, 
                                            0x83, 0x87, 0x34, 0x5d, 0xab, 0xdc, 0x93, 0x83, 0xd5, 0xc7, 0x1c, 0x6d, 0x65, 0x14, 0x40, 0x53, 
                                            0x8d, 0xec, 0xa0, 0x09, 0x98, 0x78, 0xfc, 0xf4, 0x50, 0x09, 0x7f, 0x92, 0x36, 0x28, 0xce, 0x29, 
                                            0x4c, 0xd6, 0xae, 0x08, 0xb2, 0xf7, 0xcd, 0x80, 0x8d, 0x28, 0xbb, 0x55, 0x85, 0xa6, 0x56, 0xdb, 
                                            0xd0, 0x44, 0x59, 0x8e, 0x20, 0x68, 0x60, 0x5c };
                        Array.Copy(sessionId, 0, data, 4, 4);

                        StartSendModified(data);
                    }
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - DoSenReceiveModified - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - DoSenReceiveModified - Exception - " + ex.Message);
            }
        }
        public void StartSendModified(byte[] sdec)
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSendModified - Start");
                if (sdec != null)
                {
                    sock.Send(sdec);
                }

                StartReceiveModified();
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSendModified - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSendModified - Exception - " + ex.Message);
            }
        }
        public void StartSend_old(byte[] sdec)
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - Start");
                do
                {
                    if (sock == null)
                    {
                        m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - As Socket Closed. Closing ");
                        break;
                    }
                    if (sdec != null)
                    {
                        sock.Send(sdec);
                        sdec = null;
                    }

                    //sdec = StartReceive();
                    int count = 1;
                    while (sdec == null)    //Executing in same loop
                    {
                        if (count > 20)
                        {         //Discuss with balan
                            m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - Not waiting in sock.Receive. So, Closing socket. ");
                            sock.Close();
                            break;
                        }
                        count++;
                        sdec = StartReceive();
                    }

                } while (sdec != null);
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - StartSend - Exception - " + ex.Message);
            }
        }
        public void StartSend(byte[] sdec)
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - Start");

                if (sdec != null)
                {
                    if(sock != null)
                    {
                        sock.Send(sdec);
                        sdec = null;
                    }
                }


                byte[] receivebytes = new byte[512];
                byte[] recv = new byte[512];
                int numBytesRecv = 0;

                try
                {
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - Start");
                    numBytesRecv = sock.Receive(receivebytes);
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - After SocketReceive. ");

                    while (numBytesRecv > 0)
                    {
                        recv = ProcessReceive(receivebytes);
                        if (sock != null)
                        {
                            if(recv != null)
                                sock.Send(recv);
                          //  recv = null;
                        }
                        numBytesRecv = sock.Receive(receivebytes);
                    }
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartSend - End");
                }
                catch (Exception ex)
                {
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceive - Exception - " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - StartSend - Exception - " + ex.Message);
            }
        }

        private void SocketClose(Object obj)
        {
            try
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - SocketClose - Start");
                t.Enabled = false;
                sock.Close();
                if (PingDelay != null)
                    PingDelay.Dispose();
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - SocketClose - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - SocketClose - Exception - " + ex.Message);
            }
        }

        private void StartReceiveModified()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceiveModified - Start");
                //loggingHelper.LogEvent(Panel.MAC, "Receiving bytes..");
                byte[] receivebytes = new byte[512];
                sock.Receive(receivebytes);
                sock.Send(bytes);//bytes is set with optvid received at redirector

                string decrypted = "50 D7 00 00 F7 00 02 00 10 00 00 1E 28 00 00 00 00 00 00 00 00 00 00 00 41 6E 74 69 73 61 62 00 53 61 62 6F 74 61 67 65 20 63 6F 75 76 65 72 2E E7 00 00 00 AA E4 2C DD DA F3 5C 30 A7 1E D9 A7";
                sbyte[] dec = Helper.StringToByteArrayFastest(decrypted.Replace(" ", ""));
                byte[] sdec = panelConnector.sendCmd(dec, 2, dec.Length, -1);
                //loggingHelper.LogEvent(Panel.MAC, "Connection with TC completed");

                StartSend(sdec);

                Thread.Sleep(1000);
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceiveModified - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceiveModified - Exception - " + ex.Message);
            }
        }
        private byte[] StartReceive()
        {
            byte[] receivebytes = new byte[512];
            byte[] sdec = new byte[512];
            sdec = null;
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceive - Start");
                sock.Receive(receivebytes);
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - After SocketReceive. ");
                sdec = ProcessReceive(receivebytes);
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceive - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - StartReceive - Exception - " + ex.Message);
            }
            return sdec;
        }

        private byte[] ProcessReceive(byte[] receivebytes)
        {
            // var tempBuffer = receivebytes.TakeWhile((v, index) => receivebytes.Skip(index).Any(w => w != 0x00)).ToArray();
            //loggingHelper.LogEvent(Panel.MAC, "Bytes received from TC Service ",
                     //new ExtraParam { Key = "MESSAGE", Value = receivebytes });
            byte[] sdec = new byte[512];
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - ProcessReceive - Start");
                sbyte[] response = HandleRequest(receivebytes);

                if (response != null)
                {
                    sdec = panelConnector.sendCmd(response, 2, response.Length, -1);

                    //StartSend(sdec); //Raghu
                }
                else
                {
                    sdec = null;
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - ProcessReceive - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - ProcessReceive - Exception - " + ex.Message);
            }
            return sdec;
            //Not required
            //StartReceive(); //Raghu - To discuss. Already are calling StartReceive() in StartSend(sdec) method. Is this StartReceive needed. Also, this loop never ends. It seems..
        }

        public void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Thread.Sleep(1000);
            try
            {
                string decrypted = "1E E9 F9 10 F7 00 02 00 10 00 00 1C 08 00 00 00 44 69 73 61 72 6D 65 64 2C 20 52 65 61 64 79 00 00 00 00 00 00 00 00 00 00 00 20 20 34 3A 35 31 55 00 00 00 DF E8 BE EF DF B3 6E 1B B4 FC 39 17";
                sbyte[] dec = Helper.StringToByteArrayFastest(decrypted.Replace(" ", ""));
                byte[] sdec = panelConnector.sendCmd(dec, 2, dec.Length, -1);

                if(sock != null)
                    sock.Send(sdec);
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - _timer_Elapsed - Exception - " + ex.Message);  
            }
        }

        sbyte[] SendProperResponse(string response)
        {
            sbyte[] sbyteResponse = Helper.StringToByteArrayFastest(response.Replace(" ", ""));
            if (sbyteResponse != null && sbyteResponse.Length > 11)
            {
                sbyteResponse[11] = Helper.GetCALIndex(panelConnector.CALINdex);
            }

            return sbyteResponse;
        }

        public sbyte[] HandleRequest(byte[] request)
        {
            string response;
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - HandleRequest - Start ");
                PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                if (request == null && NewPanel.IsArmedStatusNeedToSend)
                {
                    RecArm++;
                    NewPanel.IsArmed = true;
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.ArmResponse();
                    RespArm++;
                }
                else if (request == null && NewPanel.IsDisArmedStatusNeedToSend)
                {
                    RecDisarm++;
                    NewPanel.IsArmed = true;
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.DisarmResponse();
                    RespDisarm++;
                }

                byte[] nrequest = new byte[512];

                for (int i = 0; i < request.Length - 16; i++)
                {
                    nrequest[i] = request[16 + i];
                }

                sbyte[] buffer = Array.ConvertAll(nrequest, b => unchecked((sbyte)b));

                sbyte[] decrypted = panelConnector.Decryption(buffer.ToArray());

                panelConnector.CALINdex = (decrypted != null && decrypted.Length > 4) ? decrypted[4] : sbyte.MinValue;

                //GetInstallerCode command
                if (decrypted[5] == 0x6B && decrypted[6] == (sbyte)SupportClass.Identity(0x0A) && decrypted[7] == (sbyte)SupportClass.Identity(0x81))
                {
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.GetInstallerCodeResponse();
                }
                //IsPassCodeValid
                else if (decrypted[5] == 0x6B && decrypted[6] == (sbyte)SupportClass.Identity(0x0A) && decrypted[7] == (sbyte)SupportClass.Identity(0x80))
                {
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.IsPassCodeValidResponse();
                }
                //Arm command
                else if (decrypted[5] == 0x62 && decrypted[6] == 0x03 && decrypted[7] == (sbyte)SupportClass.Identity(0x88) && decrypted[8] == (sbyte)SupportClass.Identity(0xEC) && decrypted[18] == 1 && decrypted[19] == 2)
                {
                    RecArm++;
                    NewPanel.IsArmed = true;
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.ArmResponse();
                    RespArm++;
                }
                //Disarm command
                else if (decrypted[5] == 0x62 && decrypted[6] == 0x03 && decrypted[7] == (sbyte)SupportClass.Identity(0x88) && decrypted[8] == (sbyte)SupportClass.Identity(0xEC) && decrypted[18] == 1 && decrypted[19] == 1)
                {
                    RecDisarm++;
                    NewPanel.IsArmed = false;
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.DisarmResponse();
                    RespDisarm++;
                }
                //Bypass command
                else if (decrypted[5] == 0x62 && decrypted[6] == 0x32 && decrypted[7] == (sbyte)SupportClass.Identity(0x88) && decrypted[8] == (sbyte)SupportClass.Identity(0xEC))
                {
                    PingDelay.Change(DelayTime * 1000, System.Threading.Timeout.Infinite);
                    response = NewPanel.BypassResponse();
                }
                else
                    response = null;

                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] MockPanel - HandleRequest - End ");
            }
            catch (Exception ex)
            {
                response = null;
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] MockPanel - HandleRequest - Exception - " + ex.Message);
            }
            return response == null ? null : SendProperResponse(response);
        }

    }
}
