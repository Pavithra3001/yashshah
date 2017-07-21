using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabakahEntityManager
{
    class AEScrypt
   {               
        /* two modes of operation supported */
        public const int ECB = 0;
        public const int CBC = 1;

        private bool ROUNDS_12, ROUNDS_14;

        /// <summary>Subkeys 
        /// </summary>
        private int[] K;
        private int[] IK;

        /// <summary>(ROUNDS-1) * 4 
        /// </summary>
        private int limit;
        private int mode; /* ECB or CBC */
        private sbyte[] miv;

        

        protected internal void coreInit(sbyte[] userkey, int mod, sbyte[] iv)
        {
            if (userkey == null)
                throw new System.Exception("RAW bytes missing");
            if (miv != null)
                throw new System.Exception("coreInit has already been called");
            int len = userkey.Length;
            if (len != 16 && len != 24 && len != 32)
                throw new System.Exception("Invalid user key length");

            if (mod != CBC && mod != ECB)
                throw new System.Exception("Unsupported mode!");
            this.mode = mod;
            this.K = makeKey(userkey);
            this.IK = new int[this.K.Length];
            for (int i = 0; i < this.K.Length; i++)
                this.IK[i] = this.K[i];
            invertKey(this.IK);
            /* copy iv vector */
            miv = new sbyte[16];
            for (int i = 0; i < 16; i++)
                miv[i] = iv[i];
            ROUNDS_12 = (len >= 24);
            ROUNDS_14 = (len == 32);

            this.limit = getRounds(len) * 4;
        }

        protected internal void coreCrypt(sbyte[] in_Renamed, int inOffset, sbyte[] out_Renamed, int outOffset, bool decrypt)
        {
            if (decrypt)
                blockDecrypt(in_Renamed, inOffset, out_Renamed, outOffset);
            else
                blockEncrypt(in_Renamed, inOffset, out_Renamed, outOffset);
        }

        // Constants and variables
        //...........................................................................

        private const string SS = "\u637C\u777B\uF26B\u6FC5\u3001\u672B\uFED7\uAB76" + "\uCA82\uC97D\uFA59\u47F0\uADD4\uA2AF\u9CA4\u72C0" + "\uB7FD\u9326\u363F\uF7CC\u34A5\uE5F1\u71D8\u3115" + "\u04C7\u23C3\u1896\u059A\u0712\u80E2\uEB27\uB275" + "\u0983\u2C1A\u1B6E\u5AA0\u523B\uD6B3\u29E3\u2F84" + "\u53D1\u00ED\u20FC\uB15B\u6ACB\uBE39\u4A4C\u58CF" + "\uD0EF\uAAFB\u434D\u3385\u45F9\u027F\u503C\u9FA8" + "\u51A3\u408F\u929D\u38F5\uBCB6\uDA21\u10FF\uF3D2" + "\uCD0C\u13EC\u5F97\u4417\uC4A7\u7E3D\u645D\u1973" + "\u6081\u4FDC\u222A\u9088\u46EE\uB814\uDE5E\u0BDB" + "\uE032\u3A0A\u4906\u245C\uC2D3\uAC62\u9195\uE479" + "\uE7C8\u376D\u8DD5\u4EA9\u6C56\uF4EA\u657A\uAE08" + "\uBA78\u252E\u1CA6\uB4C6\uE8DD\u741F\u4BBD\u8B8A" + "\u703E\uB566\u4803\uF60E\u6135\u57B9\u86C1\u1D9E" + "\uE1F8\u9811\u69D9\u8E94\u9B1E\u87E9\uCE55\u28DF" + "\u8CA1\u890D\uBFE6\u4268\u4199\u2D0F\uB054\uBB16";

        private  readonly sbyte[] S;
        private  readonly sbyte[] Si;

        private  readonly int[] T1;
        private  readonly int[] T2;
        private  readonly int[] T3;
        private  readonly int[] T4;
        private  readonly int[] T5;
        private  readonly int[] T6;
        private  readonly int[] T7;
        private  readonly int[] T8;

        private  readonly int[] U1;
        private  readonly int[] U2;
        private  readonly int[] U3;
        private  readonly int[] U4;

        private  readonly sbyte[] rcon;



        //...........................................................................

        /* Returns total number of bytes - original lenght + padding */
        public int padEncrypt(sbyte[] in_Renamed, int inputOctets, sbyte[] out_Renamed)
        {
            int totalinputOctets = inputOctets;
            if (mode != CBC)
                throw new System.Exception("Only CBC mode is supported now.");

            int numBlocks = inputOctets / 16;
            sbyte[] block = new sbyte[16];
            sbyte[] outtemp = new sbyte[16];

            sbyte[] iv = new sbyte[16];
            for (int k = 0; k < 16; k++)
                iv[k] = miv[k];
            int i = 0;
            for (i = 0; i < numBlocks; i++)
            {
                int t0 = ((in_Renamed[0 + i * 16]) << 24 | (in_Renamed[1 + i * 16] & 0xFF) << 16 | (in_Renamed[2 + i * 16] & 0xFF) << 8 | (in_Renamed[3 + i * 16] & 0xFF)) ^ ((iv[0]) << 24 | (iv[1] & 0xFF) << 16 | (iv[2] & 0xFF) << 8 | (iv[3] & 0xFF));
                int t1 = ((in_Renamed[4 + i * 16]) << 24 | (in_Renamed[5 + i * 16] & 0xFF) << 16 | (in_Renamed[6 + i * 16] & 0xFF) << 8 | (in_Renamed[7 + i * 16] & 0xFF)) ^ ((iv[4]) << 24 | (iv[5] & 0xFF) << 16 | (iv[6] & 0xFF) << 8 | (iv[7] & 0xFF));
                int t2 = ((in_Renamed[8 + i * 16]) << 24 | (in_Renamed[9 + i * 16] & 0xFF) << 16 | (in_Renamed[10 + i * 16] & 0xFF) << 8 | (in_Renamed[11 + i * 16] & 0xFF)) ^ ((iv[8]) << 24 | (iv[9] & 0xFF) << 16 | (iv[10] & 0xFF) << 8 | (iv[11] & 0xFF));

                int t3 = ((in_Renamed[12 + i * 16]) << 24 | (in_Renamed[13 + i * 16] & 0xFF) << 16 | (in_Renamed[14 + i * 16] & 0xFF) << 8 | (in_Renamed[15 + i * 16] & 0xFF)) ^ ((iv[12]) << 24 | (iv[13] & 0xFF) << 16 | (iv[14] & 0xFF) << 8 | (iv[15] & 0xFF));

                /* convert to byte array for blockEncrypt() */
                block[0] = (sbyte)(SupportClass.URShift(t0, 24));
                block[1] = (sbyte)((SupportClass.URShift(t0, 16)) & 0xff);
                block[2] = (sbyte)((SupportClass.URShift(t0, 8)) & 0xff);
                block[3] = (sbyte)((t0) & 0xff);
                block[4] = (sbyte)(SupportClass.URShift(t1, 24));
                block[5] = (sbyte)((SupportClass.URShift(t1, 16)) & 0xff);
                block[6] = (sbyte)((SupportClass.URShift(t1, 8)) & 0xff);
                block[7] = (sbyte)((t1) & 0xff);
                block[8] = (sbyte)(SupportClass.URShift(t2, 24));
                block[9] = (sbyte)((SupportClass.URShift(t2, 16)) & 0xff);
                block[10] = (sbyte)((SupportClass.URShift(t2, 8)) & 0xff);
                block[11] = (sbyte)((t2) & 0xff);
                block[12] = (sbyte)(SupportClass.URShift(t3, 24));
                block[13] = (sbyte)((SupportClass.URShift(t3, 16)) & 0xff);
                block[14] = (sbyte)((SupportClass.URShift(t3, 8)) & 0xff);
                block[15] = (sbyte)((t3) & 0xff);
                blockEncrypt(block, 0, outtemp, 0);
                for (int j = 0; j < 16; j++)
                {
                    out_Renamed[j + i * 16] = outtemp[j];
                    iv[j] = outtemp[j];
                }
            }

            int iblock, iiv;
            int padLen = 16 - (inputOctets - 16 * numBlocks);
            for (int z = 0; z < 16 - padLen; z++)
            {
                iblock = in_Renamed[z + i * 16];
                iiv = iv[z];
                //iblock = (iblock ^ iiv);
                block[z] = (sbyte)(iblock ^ iiv);
                //block[z] = (byte)(iblock);
            }
            for (int z = 16 - padLen; z < 16; z++)
            {
                iiv = iv[z];
                block[z] = (sbyte)(padLen ^ iiv);
            }

            blockEncrypt(block, 0, outtemp, 0);
            for (int j = 0; j < 16; j++)
            {
                out_Renamed[j + i * 16] = outtemp[j];
            }
            return (totalinputOctets + padLen);
        }

        /// <summary> Encrypt exactly one block of plaintext.
        /// </summary>
        private void blockEncrypt(sbyte[] in_Renamed, int inOffset, sbyte[] out_Renamed, int outOffset)
        {
            // testing java cast

            // plaintext to ints + key
            int keyOffset = 0;
            int t0 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ K[keyOffset++];
            int t1 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ K[keyOffset++];
            int t2 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ K[keyOffset++];
            int t3 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ K[keyOffset++];

            // apply round transforms
            while (keyOffset < limit)
            {
                int a0, a1, a2;
                a0 = T1[(SupportClass.URShift(t0, 24))] ^ T2[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T3[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T4[(t3) & 0xFF] ^ K[keyOffset++];
                a1 = T1[(SupportClass.URShift(t1, 24))] ^ T2[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T3[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T4[(t0) & 0xFF] ^ K[keyOffset++];
                a2 = T1[(SupportClass.URShift(t2, 24))] ^ T2[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T3[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T4[(t1) & 0xFF] ^ K[keyOffset++];
                t3 = T1[(SupportClass.URShift(t3, 24))] ^ T2[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T3[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T4[(t2) & 0xFF] ^ K[keyOffset++];
                t0 = a0; t1 = a1; t2 = a2;
            }

            // last round is special
            int tt = K[keyOffset++];
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t0, 24))] ^ (SupportClass.URShift(tt, 24)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t1, 16)) & 0xFF] ^ (SupportClass.URShift(tt, 16)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t2, 8)) & 0xFF] ^ (SupportClass.URShift(tt, 8)));
            out_Renamed[outOffset++] = (sbyte)(S[(t3) & 0xFF] ^ (tt));
            tt = K[keyOffset++];
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t1, 24))] ^ (SupportClass.URShift(tt, 24)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t2, 16)) & 0xFF] ^ (SupportClass.URShift(tt, 16)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t3, 8)) & 0xFF] ^ (SupportClass.URShift(tt, 8)));
            out_Renamed[outOffset++] = (sbyte)(S[(t0) & 0xFF] ^ (tt));
            tt = K[keyOffset++];
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t2, 24))] ^ (SupportClass.URShift(tt, 24)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t3, 16)) & 0xFF] ^ (SupportClass.URShift(tt, 16)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t0, 8)) & 0xFF] ^ (SupportClass.URShift(tt, 8)));
            out_Renamed[outOffset++] = (sbyte)(S[(t1) & 0xFF] ^ (tt));
            tt = K[keyOffset++];
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t3, 24))] ^ (SupportClass.URShift(tt, 24)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t0, 16)) & 0xFF] ^ (SupportClass.URShift(tt, 16)));
            out_Renamed[outOffset++] = (sbyte)(S[(SupportClass.URShift(t1, 8)) & 0xFF] ^ (SupportClass.URShift(tt, 8)));
            out_Renamed[outOffset] = (sbyte)(S[(t2) & 0xFF] ^ (tt));
        }

        /* Returns number of bytes used for padding */
        public int padDecrypt(sbyte[] in_Renamed, int inputLen, sbyte[] out_Renamed)
        {
            if (mode != CBC)
                throw new System.Exception("Only CBC mode is supported now.");

            int numBlocks = inputLen / 16;
            sbyte[] block = new sbyte[16];
            sbyte[] intemp = new sbyte[16];
            sbyte[] iv = new sbyte[16];
            //Array.Copy(SupportClass.ToByteArray(miv), 0, SupportClass.ToByteArray(iv), 0, 16);
            Array.Copy(miv, 0, iv, 0, 16);
            int i = 0;
            for (i = 0; i < numBlocks - 1; i++)
            {
                /* copy to temp 16byte block */
                //Array.Copy(SupportClass.ToByteArray(in_Renamed), i * 16, SupportClass.ToByteArray(intemp), 0, 16);
                Array.Copy(in_Renamed, i * 16, intemp, 0, 16);
                /* save the block into .. block*/
                blockDecrypt(intemp, 0, block, 0);
                for (int k = 0; k < 16; k++)
                {
                    out_Renamed[k + i * 16] = (sbyte)((int)block[k] ^ (int)iv[k]);
                }
                //Array.Copy(SupportClass.ToByteArray(intemp), 0, SupportClass.ToByteArray(iv), 0, 16);
                Array.Copy(intemp, 0, iv, 0, 16);
            }

            /* last block */
            //Array.Copy(SupportClass.ToByteArray(in_Renamed), i * 16, SupportClass.ToByteArray(intemp), 0, 16);
            Array.Copy(in_Renamed, i * 16, intemp, 0, 16);
            blockDecrypt(intemp, 0, block, 0);
            int k2 = 0;
            for (; k2 < 16; k2++)
            {
                out_Renamed[k2 + i * 16] = (sbyte)((int)block[k2] ^ (int)iv[k2]);
            }
            return (int)out_Renamed[(k2 - 1) + i * 16];
        }

        /// <summary> Decrypt exactly one block of plaintext.
        /// </summary>
        private void blockDecrypt(sbyte[] in_Renamed, int inOffset, sbyte[] out_Renamed, int outOffset)
        {

            int keyOffset = 8;
            int t0, t1, t2, t3, a0, a1, a2;

            t0 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ IK[4];
            t1 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ IK[5];
            t2 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset++] & 0xFF)) ^ IK[6];
            t3 = ((in_Renamed[inOffset++]) << 24 | (in_Renamed[inOffset++] & 0xFF) << 16 | (in_Renamed[inOffset++] & 0xFF) << 8 | (in_Renamed[inOffset] & 0xFF)) ^ IK[7];

            if (ROUNDS_12)
            {
                a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
                a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
                a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
                t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];
                t0 = T5[(SupportClass.URShift(a0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a2, 8)) & 0xFF] ^ T8[(a1) & 0xFF] ^ IK[keyOffset++];
                t1 = T5[(SupportClass.URShift(a1, 24))] ^ T6[(SupportClass.URShift(a0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(a2) & 0xFF] ^ IK[keyOffset++];
                t2 = T5[(SupportClass.URShift(a2, 24))] ^ T6[(SupportClass.URShift(a1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
                t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(a2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a1, 8)) & 0xFF] ^ T8[(a0) & 0xFF] ^ IK[keyOffset++];

                if (ROUNDS_14)
                {
                    a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
                    a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
                    a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
                    t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];
                    t0 = T5[(SupportClass.URShift(a0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a2, 8)) & 0xFF] ^ T8[(a1) & 0xFF] ^ IK[keyOffset++];
                    t1 = T5[(SupportClass.URShift(a1, 24))] ^ T6[(SupportClass.URShift(a0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(a2) & 0xFF] ^ IK[keyOffset++];
                    t2 = T5[(SupportClass.URShift(a2, 24))] ^ T6[(SupportClass.URShift(a1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
                    t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(a2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a1, 8)) & 0xFF] ^ T8[(a0) & 0xFF] ^ IK[keyOffset++];
                }
            }
            a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
            a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
            a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];
            t0 = T5[(SupportClass.URShift(a0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a2, 8)) & 0xFF] ^ T8[(a1) & 0xFF] ^ IK[keyOffset++];
            t1 = T5[(SupportClass.URShift(a1, 24))] ^ T6[(SupportClass.URShift(a0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(a2) & 0xFF] ^ IK[keyOffset++];
            t2 = T5[(SupportClass.URShift(a2, 24))] ^ T6[(SupportClass.URShift(a1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(a2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a1, 8)) & 0xFF] ^ T8[(a0) & 0xFF] ^ IK[keyOffset++];
            a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
            a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
            a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];
            t0 = T5[(SupportClass.URShift(a0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a2, 8)) & 0xFF] ^ T8[(a1) & 0xFF] ^ IK[keyOffset++];
            t1 = T5[(SupportClass.URShift(a1, 24))] ^ T6[(SupportClass.URShift(a0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(a2) & 0xFF] ^ IK[keyOffset++];
            t2 = T5[(SupportClass.URShift(a2, 24))] ^ T6[(SupportClass.URShift(a1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(a2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a1, 8)) & 0xFF] ^ T8[(a0) & 0xFF] ^ IK[keyOffset++];
            a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
            a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
            a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];
            t0 = T5[(SupportClass.URShift(a0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a2, 8)) & 0xFF] ^ T8[(a1) & 0xFF] ^ IK[keyOffset++];
            t1 = T5[(SupportClass.URShift(a1, 24))] ^ T6[(SupportClass.URShift(a0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(a2) & 0xFF] ^ IK[keyOffset++];
            t2 = T5[(SupportClass.URShift(a2, 24))] ^ T6[(SupportClass.URShift(a1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(a2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a1, 8)) & 0xFF] ^ T8[(a0) & 0xFF] ^ IK[keyOffset++];
            a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
            a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
            a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];
            t0 = T5[(SupportClass.URShift(a0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a2, 8)) & 0xFF] ^ T8[(a1) & 0xFF] ^ IK[keyOffset++];
            t1 = T5[(SupportClass.URShift(a1, 24))] ^ T6[(SupportClass.URShift(a0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(a2) & 0xFF] ^ IK[keyOffset++];
            t2 = T5[(SupportClass.URShift(a2, 24))] ^ T6[(SupportClass.URShift(a1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(a2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(a1, 8)) & 0xFF] ^ T8[(a0) & 0xFF] ^ IK[keyOffset++];
            a0 = T5[(SupportClass.URShift(t0, 24))] ^ T6[(SupportClass.URShift(t3, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t2, 8)) & 0xFF] ^ T8[(t1) & 0xFF] ^ IK[keyOffset++];
            a1 = T5[(SupportClass.URShift(t1, 24))] ^ T6[(SupportClass.URShift(t0, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t3, 8)) & 0xFF] ^ T8[(t2) & 0xFF] ^ IK[keyOffset++];
            a2 = T5[(SupportClass.URShift(t2, 24))] ^ T6[(SupportClass.URShift(t1, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t0, 8)) & 0xFF] ^ T8[(t3) & 0xFF] ^ IK[keyOffset++];
            t3 = T5[(SupportClass.URShift(t3, 24))] ^ T6[(SupportClass.URShift(t2, 16)) & 0xFF] ^ T7[(SupportClass.URShift(t1, 8)) & 0xFF] ^ T8[(t0) & 0xFF] ^ IK[keyOffset++];

            t1 = IK[0];
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a0, 24))] ^ (SupportClass.URShift(t1, 24)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(t3, 16)) & 0xFF] ^ (SupportClass.URShift(t1, 16)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a2, 8)) & 0xFF] ^ (SupportClass.URShift(t1, 8)));
            out_Renamed[outOffset++] = (sbyte)(Si[(a1) & 0xFF] ^ (t1));
            t1 = IK[1];
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a1, 24))] ^ (SupportClass.URShift(t1, 24)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a0, 16)) & 0xFF] ^ (SupportClass.URShift(t1, 16)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(t3, 8)) & 0xFF] ^ (SupportClass.URShift(t1, 8)));
            out_Renamed[outOffset++] = (sbyte)(Si[(a2) & 0xFF] ^ (t1));
            t1 = IK[2];
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a2, 24))] ^ (SupportClass.URShift(t1, 24)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a1, 16)) & 0xFF] ^ (SupportClass.URShift(t1, 16)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a0, 8)) & 0xFF] ^ (SupportClass.URShift(t1, 8)));
            out_Renamed[outOffset++] = (sbyte)(Si[(t3) & 0xFF] ^ (t1));
            t1 = IK[3];
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(t3, 24))] ^ (SupportClass.URShift(t1, 24)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a2, 16)) & 0xFF] ^ (SupportClass.URShift(t1, 16)));
            out_Renamed[outOffset++] = (sbyte)(Si[(SupportClass.URShift(a1, 8)) & 0xFF] ^ (SupportClass.URShift(t1, 8)));
            out_Renamed[outOffset] = (sbyte)(Si[(a0) & 0xFF] ^ (t1));
        }

        private  int[] makeKey2(sbyte[] keyBytes)
        {
            int ROUNDS = getRounds(keyBytes.Length);
            int ROUND_KEY_COUNT = (ROUNDS + 1) * 4;

            int[] K = new int[ROUND_KEY_COUNT];

            int KC = keyBytes.Length / 4; // keylen in 32-bit elements
            int[] tk = new int[KC];

            int i, j;

            // copy user material bytes into temporary ints
            for (i = 0, j = 0; i < KC; )
                K[i++] = (keyBytes[j++]) << 24 | (keyBytes[j++] & 0xFF) << 16 | (keyBytes[j++] & 0xFF) << 8 | (keyBytes[j++] & 0xFF);
            return K;
        }
        /// <summary> Expand a user-supplied key material into a session key.
        /// *
        /// </summary>
        /// <param name="key">The 128/192/256-bit user-key to use.
        /// </param>
        /// <exception cref=""> InvalidKeyException  If the key is invalid.
        /// 
        /// </exception>
        private  int[] makeKey(sbyte[] keyBytes)
        {
            int ROUNDS = getRounds(keyBytes.Length);
            int ROUND_KEY_COUNT = (ROUNDS + 1) * 4;

            int[] K = new int[ROUND_KEY_COUNT];

            int KC = keyBytes.Length / 4; // keylen in 32-bit elements
            int[] tk = new int[KC];

            int i, j;

            // copy user material bytes into temporary ints
            for (i = 0, j = 0; i < KC; )
                tk[i++] = (keyBytes[j++]) << 24 | (keyBytes[j++] & 0xFF) << 16 | (keyBytes[j++] & 0xFF) << 8 | (keyBytes[j++] & 0xFF);

            // copy values into round key arrays
            int t = 0;
            for (; t < KC; t++)
                K[t] = tk[t];

            int tt, rconpointer = 0;
            while (t < ROUND_KEY_COUNT)
            {
                // extrapolate using phi (the round key evolution function)
                tt = tk[KC - 1];
                tk[0] ^= (S[(SupportClass.URShift(tt, 16)) & 0xFF]) << 24 ^ (S[(SupportClass.URShift(tt, 8)) & 0xFF] & 0xFF) << 16 ^ (S[(tt) & 0xFF] & 0xFF) << 8 ^ (S[(SupportClass.URShift(tt, 24))] & 0xFF) ^ (rcon[rconpointer++]) << 24;
                if (KC != 8)
                    for (i = 1, j = 0; i < KC; )
                        tk[i++] ^= tk[j++];
                else
                {
                    for (i = 1, j = 0; i < KC / 2; )
                        tk[i++] ^= tk[j++];
                    tt = tk[KC / 2 - 1];
                    tk[KC / 2] ^= (S[(tt) & 0xFF] & 0xFF) ^ (S[(SupportClass.URShift(tt, 8)) & 0xFF] & 0xFF) << 8 ^ (S[(SupportClass.URShift(tt, 16)) & 0xFF] & 0xFF) << 16 ^ (S[(SupportClass.URShift(tt, 24))]) << 24;
                    for (j = KC / 2, i = j + 1; i < KC; )
                        tk[i++] ^= tk[j++];
                }

                // copy values into round key arrays
                for (j = 0; (j < KC) && (t < ROUND_KEY_COUNT); j++, t++)
                    K[t] = tk[j];
            }

            return K;
        }

        private  void invertKey(int[] K)
        {

            for (int i = 0; i < K.Length / 2 - 4; i += 4)
            {
                int jj0 = K[i + 0];
                int jj1 = K[i + 1];
                int jj2 = K[i + 2];
                int jj3 = K[i + 3];
                K[i + 0] = K[K.Length - i - 4 + 0];
                K[i + 1] = K[K.Length - i - 4 + 1];
                K[i + 2] = K[K.Length - i - 4 + 2];
                K[i + 3] = K[K.Length - i - 4 + 3];
                K[K.Length - i - 4 + 0] = jj0;
                K[K.Length - i - 4 + 1] = jj1;
                K[K.Length - i - 4 + 2] = jj2;
                K[K.Length - i - 4 + 3] = jj3;
            }

            for (int r = 4; r < K.Length - 4; r++)
            {
                int tt = K[r];
                K[r] = U1[(SupportClass.URShift(tt, 24)) & 0xFF] ^ U2[(SupportClass.URShift(tt, 16)) & 0xFF] ^ U3[(SupportClass.URShift(tt, 8)) & 0xFF] ^ U4[tt & 0xFF];
            }

            int j0 = K[K.Length - 4];
            int j1 = K[K.Length - 3];
            int j2 = K[K.Length - 2];
            int j3 = K[K.Length - 1];
            for (int i = K.Length - 1; i > 3; i--)
                K[i] = K[i - 4];
            K[0] = j0;
            K[1] = j1;
            K[2] = j2;
            K[3] = j3;
        }


        /// <summary> Return The number of rounds for a given Rijndael keysize.
        /// *
        /// </summary>
        /// <param name="keySize"> The size of the user key material in bytes.
        /// MUST be one of (16, 24, 32).
        /// </param>
        /// <returns>         The number of rounds.
        /// 
        /// </returns>
        private  int getRounds(int keySize)
        {
            return (keySize >> 2) + 6;
        }


        public AEScrypt()
        {
            S = new sbyte[256];
            Si = new sbyte[256];
            T1 = new int[256];
            T2 = new int[256];
            T3 = new int[256];
            T4 = new int[256];
            T5 = new int[256];
            T6 = new int[256];
            T7 = new int[256];
            T8 = new int[256];
            U1 = new int[256];
            U2 = new int[256];
            U3 = new int[256];
            U4 = new int[256];
            rcon = new sbyte[30];
            //  code - to intialise S-boxes and T-boxes
            //...........................................................................

            {
                int ROOT = 0x11B;
                int i = 0;

                for (i = 0; i < 256; i++)
                {
                    int s, s2, s3, i2, i4, i8, i9, ib, id, ie, t;
                    char c = SS[SupportClass.URShift(i, 1)];
                    S[i] = (sbyte)(((i & 1) == 0) ? SupportClass.URShift(c, 8) : c & 0xFF);
                    s = S[i] & 0xFF;
                    Si[s] = (sbyte)i;
                    s2 = s << 1;
                    if (s2 >= 0x100)
                    {
                        s2 ^= ROOT;
                    }
                    s3 = s2 ^ s;
                    i2 = i << 1;
                    if (i2 >= 0x100)
                    {
                        i2 ^= ROOT;
                    }
                    i4 = i2 << 1;
                    if (i4 >= 0x100)
                    {
                        i4 ^= ROOT;
                    }
                    i8 = i4 << 1;
                    if (i8 >= 0x100)
                    {
                        i8 ^= ROOT;
                    }
                    i9 = i8 ^ i;
                    ib = i9 ^ i2;
                    id = i9 ^ i4;
                    ie = i8 ^ i4 ^ i2;

                    T1[i] = t = (s2 << 24) | (s << 16) | (s << 8) | s3;
                    T2[i] = (SupportClass.URShift(t, 8)) | (t << 24);
                    T3[i] = (SupportClass.URShift(t, 16)) | (t << 16);
                    T4[i] = (SupportClass.URShift(t, 24)) | (t << 8);

                    T5[s] = U1[i] = t = (ie << 24) | (i9 << 16) | (id << 8) | ib;
                    T6[s] = U2[i] = (SupportClass.URShift(t, 8)) | (t << 24);
                    T7[s] = U3[i] = (SupportClass.URShift(t, 16)) | (t << 16);
                    T8[s] = U4[i] = (SupportClass.URShift(t, 24)) | (t << 8);
                }
                //
                // round constants
                //
                int r = 1;
                rcon[0] = 1;
                for (i = 1; i < 30; i++)
                {
                    r <<= 1;
                    if (r >= 0x100)
                    {
                        r ^= ROOT;
                    }
                    rcon[i] = (sbyte)r;
                }
            }
        }
    }
}
