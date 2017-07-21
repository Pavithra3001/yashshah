using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeywell.SimulationFramework.LoggerComponent;
using ShabakahRedir.TCServiceReference;
using System.Threading;
using System.Configuration;

namespace ShabakahRedir
{
    public class PanelServices
    {
        TCServiceClient client ;
//        TCServiceReference.TCServiceClient client;
        public string MAC = "";
        string AlarmNetUser = "1234";
        int Session = 0;
        int[] ZonesToBypass = null;
        private int delayAfterSession = Convert.ToInt32(ConfigurationSettings.AppSettings["DelayAfterSession"]);

        public int SessionSent = 0;
        public int SessionFailed = 0;
        public int SessionSuccess = 0;

        public int ArmSent = 0;
        public int ArmSuccess = 0;
        public int ArmFailed = 0;

        public int DisArmSent = 0;
        public int DisArmSuccess = 0;
        public int DisArmFailed = 0;

        public static int totSessionSent = 0;
        public static int totSessionSuccess = 0;
        public static int totSessionFailed = 0;

        public static int totArmSent = 0;
        public static int totArmSuccess = 0;
        public static int totArmFailed = 0;

        public static int totDisArmSent = 0;
        public static int totDisArmSuccess = 0;
        public static int totDisArmFailed = 0;

        Logger m_logger;
        private bool blnArmDisArm = false;

        public PanelServices(string _mac, Logger logger)
        {
            m_logger = logger;
            MAC = _mac;
        }

        public void InitializePanelCounter()
        {
            SessionSent = 0;
            SessionSuccess = 0;
            SessionFailed = 0;
                        
            ArmSent = 0;
            ArmSuccess = 0;
            ArmFailed = 0;

            DisArmSent = 0;
            DisArmSuccess = 0;
            DisArmFailed = 0;

            totSessionSent = 0;
            totSessionSuccess = 0;
            totSessionFailed = 0;

            totArmSent = 0;
            totArmSuccess = 0;
            totArmFailed = 0;

            totDisArmSent = 0;
            totDisArmSuccess = 0;
            totDisArmFailed = 0;
        }

        public void StartArmDisArm(int ArmDisArmDelay)
        {
            try
            {
                bool blnResult = true;
                client = new TCServiceClient();
                CallOpenSession();
                Thread.Sleep(delayAfterSession * 1000);
                while (blnArmDisArm)
                {
                    if (Session != 0)
                    {
                        blnResult = SendArmRequest();
                        Thread.Sleep(ArmDisArmDelay);
                        blnResult = SendDisArmRequest();
                        Thread.Sleep(ArmDisArmDelay);

                        if (!blnResult)
                        {
                            SessionFailed++;
                            totSessionFailed++;
                            m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Closing Sending Arm/DisArm as we get Result data as Session ID is not valid");
                            break;
                        }

                    }
                    else
                    {
                        m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "]PanelServices - StartArmDisArm - Session Not Generated ");
                    }
                }
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "]PanelServices - StartArmDisArm - Exception - " + ex.Message);
            }
        }

        public void StopArmDisArm()
        {
            try
            {
                blnArmDisArm = false;

                client.Abort();
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "]PanelServices - StopArmDisArm - Exception - " + ex.Message);
            }
        }

        public void CallOpenSession()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - OpenSession - Start");
                
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - OpenSession - Sending");
                Session = client.OpenSession(MAC, 28, 8, AlarmNetUser);

                SessionSent++;
                lock (this)
                {
                    totSessionSent++;
                }

                if (Session == 0)
                {
                    lock (this)
                    {
                        totSessionFailed++;
                    }
                    SessionFailed++;
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - OpenSession - Failed");
                }
                else
                {
                    lock (this)
                    {
                        totSessionSuccess++;
                    }

                    blnArmDisArm = true;

                    SessionSuccess++;
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - OpenSession - Success");
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - OpenSession - End");
            }
            catch (Exception ex) 
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "]PanelServices - OpenSession - Exception - " + ex.Message);
            }
        }

        public bool SendArmRequest()
        {
            bool blnResult = true;
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendArmRequest - Start");
                
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendArmRequest - Sending");
                var ArmReqResult = client.ArmSystem(MAC, Session, AlarmNetUser, 1, eArmType.ArmAway, "", eArmSubState.Stay, eArmSubStateModifier.Stay1, false, ZonesToBypass);

                ArmSent++;

                lock (this)
                {
                    totArmSent++;
                }


                if (ArmReqResult.ResultStatus.Equals("TRUE"))
                {
                    lock (this)
                    {
                        totArmSuccess++;
                    }

                    ArmSuccess++;
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendArmRequest - Success");
                }
                else if (ArmReqResult.ResultStatus.Equals("FALSE"))
                {
                    lock (this)
                    {
                        totArmFailed++;
                    }

                    if (ArmReqResult.ResultData.Contains("Session ID is not valid"))
                    {
                        blnResult = false;
                    }

                    ArmFailed++;
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendArmRequest - Failed" + ArmReqResult.ResultData.ToString());
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendArmRequest - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "]PanelServices - SendArmRequest - Exception - " + ex.Message);
            }
            return blnResult;
        }

        public bool SendDisArmRequest()
        {
            bool blnResult = true;
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendDisArmRequest - Start");
               
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendDisArmRequest - Sending");
                var DisArmReqResult = client.DisarmSystem(MAC, Session, AlarmNetUser, 1, "");

                DisArmSent++;

                lock (this)
                {
                    totDisArmSent++;
                }


                if (DisArmReqResult.ResultStatus.Equals("TRUE"))
                {
                    lock (this)
                    {
                        totDisArmSuccess++;
                    }

                    DisArmSuccess++;
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendDisArmRequest - Success");
                }
                else if (DisArmReqResult.ResultStatus.Equals("FALSE"))
                {
                    lock (this)
                    {
                        totDisArmFailed++;
                    }

                    if (DisArmReqResult.ResultData.Contains("Session ID is not valid"))
                    {
                        blnResult = false;
                    }

                    DisArmFailed++;
                    m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendDisArmRequest - Fail" + DisArmReqResult.ResultData.ToString());
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "]PanelServices - SendDisArmRequest - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "]PanelServices - SendDisArmRequest - Exception - " + ex.Message);
            }

            return blnResult;
        }
    }
}
