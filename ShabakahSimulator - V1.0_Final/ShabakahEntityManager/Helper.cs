using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabakahEntityManager
{
    public static class Helper
    {
        public static sbyte GetCALIndex(sbyte p)
        {
            string bina = ToBinary(Convert.ToInt32(p));
            //string[] stringArray = new string[] { bina };

            // Convert.ToInt32(stringArray[0] + stringArray[1] + stringArray[2], 2).ToString("X");

            string bin = "0" + bina[bina.Length - 3].ToString() + bina[bina.Length - 2].ToString() + bina[bina.Length - 1].ToString();

            int rest = bin.Length % 4;
            bin = bin.PadLeft(rest, '0'); //pad the length out to by divideable by 4

            string output = "";

            for (int i = 0; i <= bin.Length - 4; i += 4)
            {
                output += string.Format("{0:X}", Convert.ToByte(bin.Substring(i, 4), 2));
            }

            string ret = "";

            if (Panel.PanelType == PanelType.Vista)
                ret = "5" + output;
            else
                ret = "D" + output;

            return unchecked((sbyte)Convert.ToInt32(ret, 16));
        }

        public static string ToBinary(int number)
        {
            string digit = Convert.ToString(number % 2);
            if (number >= 2)
            {
                int remaining = number / 2;
                string remainingString = ToBinary(remaining);
                return remainingString + digit;
            }
            return digit;
        }

        public static sbyte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            sbyte[] signed = Array.ConvertAll(arr, b => unchecked((sbyte)b));

            return signed;
        }

        public static string GetMAC(byte[] _mac)
        {
            string str = "";
            str += _mac[3].ToString("x2");
            str += _mac[4].ToString("x2");
            str += _mac[5].ToString("x2");
            return "00D02D" + str.ToUpper();
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : 55);
        }

    }
}
