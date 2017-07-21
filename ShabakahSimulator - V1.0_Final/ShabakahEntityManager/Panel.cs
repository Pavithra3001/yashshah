using Honeywell.SimulationFramework.LoggerComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShabakahEntityManager
{
    public enum PanelType
    {
        L5100 = 1,
        L5200 = 2,
        L7000 = 3,
        Vista = 4,
    }

    public class Panel
    {
        public string MAC;
        //public static bool IsArmed = false;
        //public static bool IsArmedStatusNeedToSend = false;
        //public static bool IsBypassStatusNeedToSend = false;
        //public static bool IsDisArmedStatusNeedToSend = false;
        //public static PanelType PanelType;

        public bool IsArmed = false;
        public bool IsArmedStatusNeedToSend = false;
        public bool IsBypassStatusNeedToSend = false;
        public bool IsDisArmedStatusNeedToSend = false;
        public static PanelType PanelType;
        private Logger m_logger;

        public Panel(Logger logger, string mac, bool isArmed, PanelType panelType)
        {
            m_logger = logger;
            MAC = mac;
            IsArmed = isArmed;
            PanelType = panelType;
        }

        //Disarm - Response
        public string DisarmResponse()
        {
            string response = "59 E6 DA 10 F7 00 02 00 10 00 00 1C 08 00 00 00 44 69 73 61 72 6D 65 64 2C 20 52 65 61 64 79 00 00 00 00 00 00 00 00 00 00 00 20 20 32 3A 30 31 5C 00 00 00 70 7C FB B5 09 E3 6F AF 27 A6 4A 89";
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Panel - DisarmResponse - Start ");
                switch (PanelType)
                {
                    case PanelType.L5100:
                        response = "82 A2 22 05 F2 0D 00 00 02 10 00 D2 FE FE EC 01 00 01 B8 00 00 D9 C0 F5 33 99 30 C2 42 20 E0 14";
                        //Thread.Sleep(281);
                        break;
                    case PanelType.L5200:
                        response = "9A D3 55 0D F2 0D 00 00 02 10 00 D5 FE FE EC 01 00 01 B5 00 B5 8E 0C 12 8B 6B BE EA 0C 33 8E BB";
                        //Thread.Sleep(281);
                        break;
                    case PanelType.L7000:
                        response = "D6 33 39 06 F2 0D 00 00 02 10 00 D2 FE FE EC 01 00 01 B8 00 2D 03 A0 4E 8A 92 41 2F 18 41 81 43";
                        //Thread.Sleep(328);
                        break;
                    case PanelType.Vista:
                        response = "9D E6 9E 05 F2 0D 04 00 00 00 00 57 FE FE EC 01 00 01 BC 00 E9 3A C6 CF C5 AD BD E4 45 9D 26 9F";
                        //Thread.Sleep(1172);
                        break;
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Panel - DisarmResponse - Start ");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - DisarmResponse - Exception - " + ex.Message);
            }
            return response;
        }

        //GetInstallerCode - Response
        public string GetInstallerCodeResponse()
        {
            IsArmedStatusNeedToSend = true;
            string response = "";
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Panel - GetInstallerCode - Start");
                switch (PanelType)
                {
                    case PanelType.L5100:
                        response = "19 A7 63 04 F2 0D 00 00 02 10 00 D0 FE EC 34 31 31 32 F9 00 BD 6E 88 BA 3C D6 36 87 A6 91 AF 8F";
                        //Thread.Sleep(1391);
                        break;
                    case PanelType.L5200:
                        response = "A8 A9 39 0C F2 0D 00 00 02 10 00 D0 FE EC 34 31 31 32 F9 00 7E EE 3C 97 32 6F AE ED 6F 9B C0 BC";
                        //Thread.Sleep(3406);
                        break;
                    case PanelType.L7000:
                        response = "97 4F 49 05 F2 0D 00 00 02 10 00 D0 FE EC 34 31 31 32 F9 00 FF D9 EB 71 F3 BC 9C 3F C1 0F 6A CF";
                        //Thread.Sleep(5359);
                        break;
                    case PanelType.Vista:
                        response = "02 E4 9E 05 F2 0D 04 00 00 00 00 54 FE EC 34 31 31 32 F7 00 5A EA D4 27 86 5A 12 40 ED 04 05 69";
                        //Thread.Sleep(766);
                        break;
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Panel - GetInstallerCode - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - GetInstallerCode - Exception - " + ex.Message );
            }
            return response;
        }

        //IsPassCodeValid - Response
        public string IsPassCodeValidResponse()
        {
            string response = "";
            try
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - IsPassCodeValidResponse - Start");
                switch (PanelType)
                {
                    case PanelType.L5100:
                        response = "84 A7 63 04 F2 0C 00 00 02 10 00 D1 FE EC 31 00 32 5E 00 13 B8 24 B1 17 86 D8 8A B6 31 C3 37 AA";
                        //Thread.Sleep(328);
                        break;
                    case PanelType.L5200:
                        response = "1D AA 39 0C F2 0C 00 00 02 10 00 D1 FE EC 31 00 32 5E 00 76 FF 86 CE 73 60 34 3A 18 44 C7 19 C7";
                        //Thread.Sleep(422);
                        break;
                    case PanelType.L7000:
                        response = "17 50 49 05 F2 0C 00 00 02 10 00 D1 FE EC 31 00 32 5E 00 33 3F 2A CA 1D 69 EE 04 8E D9 B5 06 A1";
                        //Thread.Sleep(515);
                        break;
                    case PanelType.Vista:
                        response = "47 E4 9E 05 F2 0F 04 00 00 00 00 55 FE EC 31 31 31 00 32 00 F7 00 0D BB 64 5B 81 64 29 82 A9 40";
                        //Thread.Sleep(640);
                        break;
                }
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - IsPassCodeValidResponse - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - IsPassCodeValidResponse - Exception - " + ex.Message);
            }
            return response;
        }

        //Arm - Response
        public string ArmResponse()
        {
            IsArmedStatusNeedToSend = false;
            string response = "";
            try
            {
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Panel - ArmResponse - Start ");
                switch (PanelType)
                {
                    case PanelType.L5100:
                        response = "C1 CC 63 04 F2 0D 00 00 02 10 00 D5 FE FE EC 01 00 01 B5 00 FF 08 ED 83 BF 08 7D 31 E0 FD 1B 15";
                        //Thread.Sleep(265);
                        break;
                    case PanelType.L5200:
                        response = "58 AA 39 0C F2 0D 00 00 02 10 00 D2 FE FE EC 02 03 01 B4 00 31 79 C5 8C 45 F1 EB 3D 36 D7 22 CC";
                        //Thread.Sleep(281);
                        break;
                    case PanelType.L7000:
                        response = "5D 50 49 05 F2 0D 00 00 02 10 00 D2 FE FE EC 01 00 01 B8 00 BA 59 9A 95 2C BE 0A 8B F5 DE 96 36";
                        //Thread.Sleep(313);
                        break;
                    case PanelType.Vista:
                        response = "99 E4 9E 05 F2 0D 04 00 00 00 00 56 FE FE EC 01 00 01 BD 00 64 7E A6 2E D6 EE 7F 8A 7C A9 88 DB";
                        //Thread.Sleep(781);
                        break;
                }
                m_logger.LogMessage(Level.TRACE, "[" + MAC + "] Panel - ArmResponse - End ");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - ArmResponse - Exception - " + ex.Message);
            }
            return response;
        }

        //Bypass - Response
        public string BypassResponse()
        {
            IsBypassStatusNeedToSend = false;
            string response = "";
            try
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - BypassResponse - Start ");
                switch (PanelType)
                {
                    case PanelType.L5100:
                        response = "0C 81 72 05 F2 0C 00 00 02 10 00 D4 FE FE EC 34 36 4E 00 E7 AD AD 68 FE 46 C6 DF F7 37 F0 40 CB";
                        //Thread.Sleep(281);
                        break;
                    case PanelType.L5200:
                        response = "B7 59 13 00 F2 0B 00 00 02 10 00 D1 FE FE EC 35 86 00 CF E1 25 9B 1E 4C A6 E9 A8 5C 72 0F 91 45";
                        //Thread.Sleep(281);
                        break;
                    case PanelType.L7000:
                        response = "7D 1D 42 06 F2 0B 00 00 02 10 00 D0 FE FE EC 32 8A 00 39 25 91 76 06 CB BF 57 75 FA D6 A9 E0 8E";
                        //Thread.Sleep(313);
                        break;
                    case PanelType.Vista:
                        response = "C8 52 20 06 F2 09 04 00 00 00 00 50 FE FE B5 00 45 3B 57 E7 B4 83 84 F0 1D CB E9 8E EB 08 C6 67";
                        //Thread.Sleep(688);
                        break;
                }
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - BypassResponse - End ");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "[" + MAC + "] Panel - BypassResponse - Exception - " + ex.Message);
            }
            return response;
        }
    }
}
