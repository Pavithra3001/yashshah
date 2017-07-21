using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabakahEntityManager
{
    public class PanelState
    {
        public PanelState()
        {
            //ReceivedMsgsQueue = new Queue<CALParseMessage>();
            F7pack = new sbyte[64];
        }

        public string UserCode { get; set; }

        public string InstallerCode { get; set; }

        public string AuthorizingCode { get; set; }

        public bool IsSetPointChanged { get; set; }

        // 0, 1, 4
        public int LEDState { get; set; }

        public bool IsLynx { get; set; }

        public bool IsLynx5000 { get; set; }

        public int NumofPartitions { get; set; }

        public int[] NumofPartitionZones { get; set; }

        public int[] ArmState { get; set; }

        public bool[] PartitionChimed { get; set; }

        public int[] ParitionInSnd { get; set; }

        public string Sysware { get; set; }

        public int DeviceType { get; set; }

        public bool DebugMode { get; set; }

        public bool SimulationMode { get; set; }

        // public Queue<CALParseMessage> ReceivedMsgsQueue { get; set; }

        public string LoggingTimeStamp { get; set; }

        public int CALMessageIndex { get; set; }

        public bool LoggingEnabled { get; set; }

        // some mystery int that needs to be incremented and sent with every cmd -- no one knows why
        public int ivect { get; set; }

        public string SysVersion { get; set; }

        //public PanelFeatureSet2Info PanelFeatureSet2Info { get; set; }

        public int XMode { get; set; }

        public bool XGPRS { get; set; }

        public bool IsConfigurationChanged { get; set; }

        public int ZWaveConfigSeqNumber { get; set; }

        public int ECPAddress { get; set; }

        public sbyte[] F7pack { get; set; }

        public bool IsCAL34 { get; set; }

        public bool ECPOk { get; set; }

        public bool ECPDone { get; set; }

        public int Gorped { get; set; }

        public int PState { get; set; }

        public int Ptrb { get; set; }

        public string StatusLine1 { get; set; }

        public string StatusLine2 { get; set; }

        public bool IsLynxdiscover { get; set; }

        public int Lynxdstate { get; set; }

        public bool ZoneConnection { get; set; }

        // public AdditionalInformation AdditionalInformationObj { get; set; }

        public string[] ldznums { get; set; }

        public string[] ldzones { get; set; }

        public long tc2DeviceID { get; set; }

        public int timeout { get; set; }

        public bool dosess { get; set; }

        public bool running { get; set; }

        public bool connected { get; set; }

        public int maxTimeoutValue { get; set; }

        public bool done3000 { get; set; }

        public string dprev { get; set; }

        public int seq { get; set; }

        public string tc2db { get; set; }

        public string alarmnetUserGUID { get; set; }

        public int tcInstanceID { get; set; }

        public string dataServerHost { get; set; }

        public int dataServerPort { get; set; }

    }
}
