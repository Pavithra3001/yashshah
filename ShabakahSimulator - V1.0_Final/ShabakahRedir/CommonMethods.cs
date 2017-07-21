using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ShabakahRedir
{
    class CommonMethods
    {
        static int m_SCID20CHARLENGTH = 20;
        static int m_SCID19CHARLENGTH = 19;
        public static string[] m_DateFormatList = new string[]{"yyyy-MM-dd",
                                                    "MM/dd/yyyy",
                                                   "dd/MM/yyyy"};

        public static string[] m_TimeFormatList = new string[]{"hh:mm:ss tt",
                                                   "HH:mm:ss"};

        public static short m_standardDateFormat = 1;
        public static short m_standardTimeFormat = 1;

        public CommonMethods()
        {

        }



        public static bool IsHex(string number)
        {
            Regex hexNumber = new Regex("^[a-fA-F0-9]+$");
            bool retVal = true; // is a hexadecimal of blank
            if (number != "")
            {
                retVal = hexNumber.IsMatch(number);
            }
            return retVal;
        }
        public static bool IsDigit(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                foreach (char c in value)
                {
                    if (!Char.IsDigit(c))
                        return false;
                }
            }
            return true;
        }
        public static bool IsInt(string number)
        {
            Regex hexNumber = new Regex("^[0-9]+$");
            bool retVal = true; // is a hexadecimal of blank
            if (number != "")
            {
                retVal = hexNumber.IsMatch(number);
            }
            return retVal;
        }




        private static bool IsBinary(string number)
        {
            Regex hexNumber = new Regex("^[01]+$");
            bool retVal = true; // is a hexadecimal of blank
            if (number != "")
            {
                retVal = hexNumber.IsMatch(number);
            }
            return retVal;
        }



        public static string AppendLeadingZeros(string val, int length)
        {
            while (val.Length < length)
            {
                val = "0" + val;
            }
            return val;
        }



        public static string IntToHex(int number)
        {
            return String.Format("{0:X}", number);
        }



        //length refers to the number of characters to display in final output
        public static string IntToHex(int number, int length)
        {
            return AppendLeadingZeros(String.Format("{0:X}", number), length);
        }

        public static string LongToHex(long number, int length)
        {
            return AppendLeadingZeros(String.Format("{0:X}", number), length);
        }



        public static int HexToInt(string hexString)
        {
            return int.Parse(hexString, System.Globalization.NumberStyles.HexNumber, null);
        }

        public static long HexToLong(string hexString)
        {
            return long.Parse(hexString, System.Globalization.NumberStyles.HexNumber, null);
        }


        private static string IntToBin(int val)
        {
            return Convert.ToString(val, 2);
        }



        //returns a string representation of the conversion
        //of an integer into any other base 
        private static string IntToBase(int val, int baseNumber)
        {
            return Convert.ToString(val, baseNumber);
        }



        private static string HexToBin(string hexString)
        {
            return IntToBin(HexToInt(hexString));
        }




        private static string IntToBin(int val, int length)
        {
            string bits = Convert.ToString(val, 2);

            while (bits.Length < length)
            {
                bits = "0" + bits;
            }
            return bits;

        }



        private static string HexToBin(string hexString, int length)
        {
            return IntToBin(HexToInt(hexString), length);
        }




        public static string ByteArrayToHex(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            hex = hex.Replace("-", "");

            return hex;
        }



        public static string ByteArrayToHex(byte[] ba, int length)
        {

            string hex = BitConverter.ToString(ba);
            hex = hex.Replace("-", "");
            hex = AppendLeadingZeros(hex, length);
            return hex;
        }


        public static byte[] HexToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }





        public static bool ValidateSCID(string SCID)
        {
            bool returnVal = false;
            SCID = SCID.Trim();

            if (SCID != null && !SCID.Equals(""))
            {//is it a valid number              
                if (CommonMethods.IsInt(SCID))
                {   //Cingular (PREFIX - 8901410), Rogers(PREFIX - 89302720) -  SCIDs are 20 char long
                    if (SCID.Length == m_SCID20CHARLENGTH)
                    {//does it have the proper prefix
                        if (SCID.StartsWith("8901410") || SCID.StartsWith("89302720"))
                        {
                            returnVal = true;
                        }
                    }
                    //Jasper Wireless (PREFIX - 8988216) and Wireless (PREFIX - 89443006) SCIDs are 19 char long
                    else if (SCID.Length == m_SCID19CHARLENGTH)
                    {	//does it have the proper prefix
                        if (SCID.StartsWith("8988216") || SCID.StartsWith("89443006"))
                        {
                            returnVal = true;
                        }
                    }
                }
            }
            return returnVal;
        }


        public static bool ValidateBatchID(string BatchID)
        {
            bool retVal = false;

            Regex stationNumber = new Regex("[a-zA-Z0-9]");

            //batch id must be 6 characters long - 1st is station followed by 5 hex numbers
            //1st char must be a letter or number
            //next 5 chars must be a hexadecimal 
            try
            {

                retVal = stationNumber.IsMatch(BatchID.Substring(0, 1)) &&
                  CommonMethods.IsHex(BatchID.Substring(1));
            }
            catch
            {
                retVal = false;
            }


            return retVal;
        }




        public static bool IsEmailValid(string email)
        {
            bool retVal = false;
            //check email address one at a triggerTime

            // regex source msdn.microsoft.com/en-us/library/01escwtf.aspx
            // John ski updated regex per RFC 2822 to include "/" and "_" 
            // John ski exclude "&" to prevent appending of "amp" from flash, "&" is part of RFC 2822 
            // special characters currently allowed . @ ! # $ % ' * + - / = ? ^ _ ` { | } ~  
            // Note: when dealing with special charaters, some are considered metacharacters and must be escaped "\" 

            //separate lenght testing for min/max string lenght

            string expr = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))" +
                          @"|[-!#\$%/_'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z]{2,6}))$";

            string expr1 = @"^.{6,255}$";

            Regex emailExpr = new Regex(expr);
            Regex emailExpr1 = new Regex(expr1);

            if (email == null || email.Trim().Equals(String.Empty))
            {
                retVal = false;
            }
            else
            {
                //EmailAddress validation          

                if (emailExpr.IsMatch(email.Trim()) && emailExpr1.IsMatch(email.Trim()))
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }

            return retVal;
        }




        public static string FormatDateTime(DateTime dateAndTime, short outputDateFormat, short outputTimeFormat)
        {
            string retVal = "";
            retVal = dateAndTime.ToString(m_DateFormatList[outputDateFormat] + " " + m_TimeFormatList[outputTimeFormat]);

            return retVal;
        }



        //May throw an error if it cannot parse string
        public static DateTime GetStringAsDateTime(string dateTime, short InputDateFormat,
                                                                      short InputTimeFormat)
        {
            DateTime RetVal = new DateTime();


            RetVal = DateTime.ParseExact(dateTime, m_DateFormatList[InputDateFormat] + " " +
                                          m_TimeFormatList[InputTimeFormat], null);


            return RetVal;
        }




        public static bool IsDateFormatValid(short DateFormat)
        {
            bool retVal = false;

            if (DateFormat >= 0 && DateFormat < m_DateFormatList.Length)
                retVal = true;

            return retVal;
        }




        public static bool IsTimeFormatValid(short TimeFormat)
        {
            bool retVal = false;

            if (TimeFormat >= 0 && TimeFormat < m_TimeFormatList.Length)
                retVal = true;

            return retVal;
        }




        public static bool AreDatesValid(string Start, string End, short DateFormat, short TimeFormat)
        {
            bool retVal = false;

            //for dates to be valid, they need to be parsable, date and triggerTime format indicies valid and Start < End
            if (IsDateFormatValid(DateFormat) && IsTimeFormatValid(TimeFormat))
            {
                DateTime StartDate,
                          EndDate;
                bool ValidStart = DateTime.TryParseExact(Start, m_DateFormatList[DateFormat] + " " +
                                          m_TimeFormatList[TimeFormat], null, DateTimeStyles.None, out StartDate);

                bool ValidEnd = DateTime.TryParseExact(End, m_DateFormatList[DateFormat] + " " +
                                          m_TimeFormatList[TimeFormat], null, DateTimeStyles.None, out EndDate);

                if (ValidStart && ValidEnd && StartDate < EndDate)
                    retVal = true;
            }

            return retVal;
        }


        public static bool IsDateValid(string Start, short DateFormat, short TimeFormat)
        {
            bool retVal = false;
            //for date to be valid, they need to be parsable and Date and triggerTime format indicies valid
            if (IsDateFormatValid(DateFormat) && IsTimeFormatValid(TimeFormat))
            {
                DateTime StartDate;

                if (DateTime.TryParseExact(Start, m_DateFormatList[DateFormat] + " " +
                                          m_TimeFormatList[TimeFormat], null, DateTimeStyles.None, out StartDate))
                {
                    retVal = true;
                }
            }

            return retVal;
        }



        public static bool IsMACValid(string MACHex)
        {
            bool retVal = false;
            if (MACHex != null)
            {
                MACHex = MACHex.Trim();
                retVal = MACHex.Length == 12 && MACHex.ToUpper().StartsWith("00D02D") && IsHex(MACHex);
            }

            return retVal;
        }

        public static bool IsCameraMACValid(string MACHex)
        {
            bool retVal = false;
            if (MACHex != null)
            {
                MACHex = MACHex.Trim();
                retVal = MACHex.Length == 12 &&
                    //(MACHex.ToUpper().StartsWith("00C002") || MACHex.ToUpper().StartsWith("000E8F")) && 
                  IsHex(MACHex);
            }

            return retVal;
        }

        public static bool IsDeviceSerialTextValid(string DeviceSerialText)
        {
            bool retVal = false;

            //it must be either a security panel or a Camera to be valid as of 4/2011
            retVal = IsMACValid(DeviceSerialText) || IsCameraMACValid(DeviceSerialText);

            return retVal;
        }
        public static string Serialize<T>(T value)
        {
            if (value.ToString() == null)
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;
            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }
                return textWriter.ToString();
            }
        }

        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlReaderSettings settings = new XmlReaderSettings();
            // No settings need modifying here
            using (StringReader textReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        public static string IntToBinaryString(int number)
        {
            const int mask = 1;
            var binary = string.Empty;
            while (number > 0)
            {
                // Logical AND the number and prepend it to the result string
                binary = (number & 1) + binary;
                number = number >> 1;
            }

            return binary;
        }

        public static string InttoIPAddress(int test)
        {
            string str = CommonMethods.IntToBinaryString(test);
            string str1 = str.PadLeft(32, '0');
            string ip1 = str1.Substring(0, 8);
            string ip2 = str1.Substring(8, 8);
            string ip3 = str1.Substring(16, 8);
            string ip4 = str1.Substring(24, 8);

            int address1 = Convert.ToInt32(ip1, 2);
            int address2 = Convert.ToInt32(ip2, 2);
            int address3 = Convert.ToInt32(ip3, 2);
            int address4 = Convert.ToInt32(ip4, 2);

            string finalip;

            finalip = address1.ToString() + "." + address2.ToString() + "." + address3.ToString() + "." + address4.ToString();

            return finalip;

        }

        public static void displayDecimalAndHexValue(byte[] bytes)
        {
            foreach (byte b in bytes)
                Console.WriteLine("Decimal value " + b.ToString() + " Hex value " + String.Format("{0:X}", b));
        }

    }
}
