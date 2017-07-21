using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabakahEntityManager
{
    class PanelConnector
    {
        private AEScrypt _aesCrypt = null;
        private int DOFF = 16;

        public sbyte CALINdex;

        private PanelState _PanelState;

        public void DoPanelConnector(string key, PanelState state)
        {
            _PanelState = state;
            initAES(key);
        }

        public PanelState PanelState
        {
            get
            {
                return _PanelState;
            }
        }

        private bool initAES(string aee)
        {
            sbyte[] iv = new sbyte[32];
            sbyte[] key = new sbyte[32];

            try
            {
                for (int i = 0, j = 0; i < 32; i++, j += 2)
                {
                    //UPGRADE_TODO: Method 'java.lang.Integer.parseInt' was converted to 'System.Convert.ToInt32' which has a different behavior. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1073"'
                    key[i] = (sbyte)System.Convert.ToInt32(aee.Substring(j, (j + 2) - (j)), 16);
                }
            }
            catch (System.FormatException ne)
            {
                //ExceptionPolicy.HandleException(ne, "TCGeneralExceptionPolicy");
                return false;
            }
            //}

            _aesCrypt = new AEScrypt();
            System.DateTime d = System.DateTime.Now;

            if (_PanelState.ivect == 0)
                _PanelState.ivect = (int)(((d.Ticks - 621355968000000000) / 10000) - (long)System.TimeZone.CurrentTimeZone.GetUtcOffset(d).TotalMilliseconds);

            for (int i = 0; i < 32; i++)
                iv[i] = 0;
            try
            {
                _aesCrypt.coreInit(key, 1, iv);
            }
            catch (System.Exception ae)
            {
                //LoggingHelper.LogEvent(ae, "Error while initializing encryption");
                return false;
            }

            return true;
        }

        public byte[] sendCmd(sbyte[] calmsg, int cmdtype, int byteLengthToSend, int key)
        {
            _PanelState.ivect++;

            var request = new sbyte[512];
            int n = DOFF + byteLengthToSend;
            //MR:>Initialize all elements of Array to 0
            Array.Clear(request, 0, request.Length);
            request[0] = (sbyte)SupportClass.Identity('P');
            request[1] = (sbyte)SupportClass.Identity('R');
            request[2] = (sbyte)SupportClass.Identity('S');
            request[3] = (sbyte)SupportClass.Identity('M');
            sbyte[] bb = ntoh(cmdtype);
            if (key == -1)
                bb = ntoh(1);
            request[4] = bb[0];
            request[5] = bb[1];
            request[6] = bb[2];
            request[7] = bb[3];
            request[8] = 0;

            if (_PanelState.XMode == 2)
                request[9] = (sbyte)(_PanelState.XGPRS ? 0 : 1);
            //should stop stream
            else
                request[9] = 0;

            request[10] = 0;
            request[11] = 0;
            bb = ntoh(byteLengthToSend);
            request[12] = bb[0];
            request[13] = bb[1];
            request[14] = bb[2];
            request[15] = bb[3];

            int t = byteLengthToSend;
            //--if (encrypted)
            //--{
            if (cmdtype == 1)
                calmsg[0] = (sbyte)key;
            //MR:>Initialize all elements of Array to 0
            var xcal = new sbyte[512];
            // Array.Clear(xcal, 0, xcal.Length);
            bb = ntoh(_PanelState.ivect);
            //xcal[0] = bb[0];
            //xcal[1] = bb[1];
            //xcal[2] = bb[2];
            //xcal[3] = bb[3];
            //for (int i = 4; i < t + 4; i++)
            //    xcal[i] = calmsg[i - 4];

            for (int i = 0; i < t; i++)
                xcal[i] = calmsg[i];

            try
            {
                //--if (!simulation)
                //--{

                var tempBuffer = xcal.TakeWhile((v, index) => xcal.Skip(index).Any(w => w != 0x00)).ToArray();

                t = _aesCrypt.padEncrypt(xcal, byteLengthToSend, xcal);

            }
            catch (Exception ex)
            {
                //LoggingHelper.LogEvent(ex, "Error encrypting");
                throw;
            }

            for (int i = 0; i < t; i++)
                request[i + 16] = xcal[i];

            bb = ntoh(t);
            request[12] = bb[0];
            request[13] = bb[1];
            request[14] = (sbyte)((sbyte)(t - byteLengthToSend) | (sbyte)SupportClass.Identity(0x80)); //bb[2];
            request[15] = bb[3];
            n = DOFF + t;

            try
            {
                var brequest = new byte[512];
                System.Buffer.BlockCopy(request, 0, brequest, 0, n);

                return brequest;
            }
            catch (System.IO.IOException ioe)
            {
                //LoggingHelper.LogEvent(ioe, "Error when sending command");
                throw;
            }
        }

        private sbyte[] ntoh(int x)
        {
            sbyte[] res = new sbyte[4];
            res[3] = (sbyte)(SupportClass.URShift(x, 24));
            res[2] = (sbyte)(SupportClass.URShift(x, 16));
            res[1] = (sbyte)(SupportClass.URShift(x, 8));
            res[0] = (sbyte)(x);
            return res;
        }

        int byteLengthToSend = 19; //arm away cal message size
        int byteDefaultSize = 512; //size of sbyte array
        int numberOfbytesInRCPPacket = 60;

        public sbyte[] Decryption(sbyte[] input)
        {
            sbyte[] output = new sbyte[byteDefaultSize];

            _aesCrypt.padDecrypt(input, numberOfbytesInRCPPacket + 4, output);
            return output;
        }
    }
}
