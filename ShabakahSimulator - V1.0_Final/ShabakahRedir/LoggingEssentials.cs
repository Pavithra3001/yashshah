using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabakahRedir
{
    public class ExtraParam
    {
        public string Key { get; set; }

        private object _value;

        public object Value
        {
            get
            {
                if (_value == null)
                    _value = "";

                return _value;
            }
            set { _value = value; }
        }

        public override string ToString()
        {
            if (Value.GetType() == typeof(sbyte[]) || Value.GetType() == typeof(byte[]))
                return ByteArrayToString((sbyte[])Value);

            return Value.ToString();
        }

        public static string ByteArrayToString(sbyte[] array)
        {
            StringBuilder sbHex = new StringBuilder();

            if (null != array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    sbHex.Append(String.Format("{0:X}", array[i]).PadLeft(2, '0')).Append(" ");
                }
            }
            return sbHex.ToString();
        }
    }

    public class LoggingEssentials
    {
        public string MAC { get; set; }
        public int SessionID { get; set; }
        public string LayerName { get; set; }
        public string MethodName { get; set; }
        public bool IsInput { get; set; }
        public List<ExtraParam> ExtraParams { get; set; }
        private string logMessage = string.Empty;
        private const string tab = "\t";
        public string LoggingMessage { get; set; }

        public LoggingEssentials(string mAC, int sessionID, string layerName, string methodName, bool isInput,
            List<ExtraParam> extraParams = null)
        {
            MAC = mAC;
            SessionID = sessionID;
            LayerName = layerName;
            MethodName = methodName;
            IsInput = isInput;
            ExtraParams = extraParams;
            LoggingMessage = string.Empty;
        }

        public LoggingEssentials(string loggingMessage, List<ExtraParam> extraParams = null)
        {
            LoggingMessage = loggingMessage;
            SessionID = -1;
            ExtraParams = extraParams;
        }

        public LoggingEssentials(string loggingMessage, string mAC, int sessionID, List<ExtraParam> extraParams = null)
        {
            //LoggingMessage = string.Concat(mAC, tab, sessionID, tab, loggingMessage);
            LoggingMessage = loggingMessage;
            MAC = mAC;
            SessionID = sessionID;
            ExtraParams = extraParams;
        }

        public string ConstructLogMessage()
        {
            //return string.IsNullOrEmpty(LoggingMessage) ? ConstructIOLogMessage() : ConstructGeneralLogMessage();
            return ConstructGeneralLogMessage();
        }

        public string ConstructGeneralLogMessage()
        {
            //logMessage = string.Concat(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.ffffff"), tab, LoggingMessage, " ");
            logMessage = LoggingMessage;
            if (ExtraParams != null)
            {
                if (ExtraParams.Count > 0)
                    logMessage += " [";

                foreach (ExtraParam param in ExtraParams)
                {
                    if (param != null)
                    {
                        //object ParamValue = param.Value.GetType() == typeof(sbyte[]) || param.Value.GetType() == typeof(byte[])
                        //    ? UtilityMethods.ByteArrayToString((sbyte[])param.Value) : param.Value.ToString();
                        logMessage += param.Key + " = " + param.ToString() + "; ";
                    }
                }

                if (ExtraParams.Count > 0)
                    logMessage += "]";
            }
            return logMessage;
        }
    }
}
