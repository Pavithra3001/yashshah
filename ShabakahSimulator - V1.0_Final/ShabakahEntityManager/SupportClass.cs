using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShabakahEntityManager
{
    class SupportClass
    {
        public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
        {
            stream.Flush();
        }

        /*******************************/
        public static int URShift(int number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2 << ~bits);
        }

        public static int URShift(int number, long bits)
        {
            return URShift(number, (int)bits);
        }

        public static long URShift(long number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2L << ~bits);
        }

        public static long URShift(long number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /*******************************/
        /// <summary>
        /// Converts an array of sbytes to an array of bytes
        /// </summary>
        /// <param name="sbyteArray">The array of sbytes to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] byteArray = new byte[sbyteArray.Length];
            for (int index = 0; index < sbyteArray.Length; index++)
                byteArray[index] = (byte)sbyteArray[index];
            return byteArray;
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="sourceString">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(string sourceString)
        {
            byte[] byteArray = new byte[sourceString.Length];
            for (int index = 0; index < sourceString.Length; index++)
                byteArray[index] = (byte)sourceString[index];
            return byteArray;
        }

        /*******************************/
        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] sbyteArray = new sbyte[byteArray.Length];
            for (int index = 0; index < byteArray.Length; index++)
                sbyteArray[index] = (sbyte)byteArray[index];
            return sbyteArray;
        }

        /*******************************/
        /// <summary>
        /// Converts an array of sbytes to an array of chars
        /// </summary>
        /// <param name="sByteArray">The array of sbytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(sbyte[] sByteArray)
        {
            char[] charArray = new char[sByteArray.Length];
            sByteArray.CopyTo(charArray, 0);
            return charArray;
        }

        /// <summary>
        /// Converts an array of bytes to an array of chars
        /// </summary>
        /// <param name="byteArray">The array of bytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(byte[] byteArray)
        {
            char[] charArray = new char[byteArray.Length];
            byteArray.CopyTo(charArray, 0);
            return charArray;
        }


        /*******************************/
        /// <summary>
        /// This method is used as a dummy method to simulate VJ++ behavior
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static long Identity(long literal)
        {
            return literal;
        }

        /// <summary>
        /// This method is used as a dummy method to simulate VJ++ behavior
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static ulong Identity(ulong literal)
        {
            return literal;
        }

        /// <summary>
        /// This method is used as a dummy method to simulate VJ++ behavior
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static float Identity(float literal)
        {
            return literal;
        }

        /// <summary>
        /// This method is used as a dummy method to simulate VJ++ behavior
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static double Identity(double literal)
        {
            return literal;
        }

        /*******************************/
        /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceStream">The source Stream to read from</param>
        /// <param name="target">Contains the array of characteres read from the source Stream.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source Stream.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream.</returns>
        public static System.Int32 ReadInput(System.IO.Stream sourceStream, ref sbyte[] target, int start, int count)
        {
            byte[] receiver = new byte[target.Length];
            int bytesRead = sourceStream.Read(receiver, start, count);

            for (int i = start; i < start + bytesRead; i++)
                target[i] = (sbyte)receiver[i];

            return bytesRead;
        }

        /// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceTextReader">The source TextReader to read from</param>
        /// <param name="target">Contains the array of characteres read from the source TextReader.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source TextReader.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader.</returns>
        public static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, ref sbyte[] target, int start, int count)
        {
            char[] charArray = new char[target.Length];
            int bytesRead = sourceTextReader.Read(charArray, start, count);

            for (int index = start; index < start + bytesRead; index++)
                target[index] = (sbyte)charArray[index];

            return bytesRead;
        }

        /*******************************/
        public class CalendarManager
        {
            public const int YEAR = 0;
            public const int MONTH = 1;
            public const int DATE = 2;
            public const int HOUR = 3;
            public const int MINUTE = 4;
            public const int SECOND = 5;
            public const int MILLISECOND = 6;
            public const int DAY_OF_MONTH = 7;
            public const int HOUR_OF_DAY = 8;

            static public CalendarHashTable manager = new CalendarHashTable();

            public class CalendarHashTable : System.Collections.Hashtable
            {
                public System.DateTime GetDateTime(System.Globalization.Calendar calendar)
                {
                    if (this[calendar] != null)
                        return ((CalendarProperties)this[calendar]).dateTime;
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTime = System.DateTime.Now;
                        this.Add(calendar, tempProps);
                        return this.GetDateTime(calendar);
                    }
                }
                public void SetDateTime(System.Globalization.Calendar calendar, System.DateTime date)
                {
                    if (this[calendar] != null)
                    {
                        ((CalendarProperties)this[calendar]).dateTime = date;
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTime = date;
                        this.Add(calendar, tempProps);
                    }
                }
                public void Set(System.Globalization.Calendar calendar, int field, int fieldValue)
                {
                    if (this[calendar] != null)
                    {
                        System.DateTime tempDate = ((CalendarProperties)this[calendar]).dateTime;
                        switch (field)
                        {
                            case CalendarManager.DATE:
                                tempDate = tempDate.AddDays(fieldValue - tempDate.Day);
                                break;
                            case CalendarManager.HOUR:
                                tempDate = tempDate.AddHours(fieldValue - tempDate.Hour);
                                break;
                            case CalendarManager.MILLISECOND:
                                tempDate = tempDate.AddMilliseconds(fieldValue - tempDate.Millisecond);
                                break;
                            case CalendarManager.MINUTE:
                                tempDate = tempDate.AddMinutes(fieldValue - tempDate.Minute);
                                break;
                            case CalendarManager.MONTH:
                                //Month value is 0-based. e.g., 0 for January
                                tempDate = tempDate.AddMonths(fieldValue - (tempDate.Month + 1));
                                break;
                            case CalendarManager.SECOND:
                                tempDate = tempDate.AddSeconds(fieldValue - tempDate.Second);
                                break;
                            case CalendarManager.YEAR:
                                tempDate = tempDate.AddYears(fieldValue - tempDate.Year);
                                break;
                            case CalendarManager.DAY_OF_MONTH:
                                tempDate = tempDate.AddDays(fieldValue - tempDate.Day);
                                break;
                            case CalendarManager.HOUR_OF_DAY:
                                tempDate = tempDate.AddHours(fieldValue - tempDate.Hour);
                                break;

                            default:
                                break;
                        }
                        ((CalendarProperties)this[calendar]).dateTime = tempDate;
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTime = System.DateTime.Now;
                        this.Add(calendar, tempProps);
                        this.Set(calendar, field, fieldValue);
                    }
                }

                public void Set(System.Globalization.Calendar calendar, int year, int month, int day)
                {
                    if (this[calendar] != null)
                    {
                        this.Set(calendar, CalendarManager.YEAR, year);
                        this.Set(calendar, CalendarManager.MONTH, month);
                        this.Set(calendar, CalendarManager.DATE, day);
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        //Month value is 0-based. e.g., 0 for January
                        tempProps.dateTime = new System.DateTime(year, month + 1, day);
                        this.Add(calendar, tempProps);
                    }
                }

                public void Set(System.Globalization.Calendar calendar, int year, int month, int day, int hour, int minute)
                {
                    if (this[calendar] != null)
                    {
                        this.Set(calendar, CalendarManager.YEAR, year);
                        this.Set(calendar, CalendarManager.MONTH, month);
                        this.Set(calendar, CalendarManager.DATE, day);
                        this.Set(calendar, CalendarManager.HOUR, hour);
                        this.Set(calendar, CalendarManager.MINUTE, minute);
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        //Month value is 0-based. e.g., 0 for January
                        tempProps.dateTime = new System.DateTime(year, month + 1, day, hour, minute, 0);
                        this.Add(calendar, tempProps);
                    }
                }

                public void Set(System.Globalization.Calendar calendar, int year, int month, int day, int hour, int minute, int second)
                {
                    if (this[calendar] != null)
                    {
                        this.Set(calendar, CalendarManager.YEAR, year);
                        this.Set(calendar, CalendarManager.MONTH, month);
                        this.Set(calendar, CalendarManager.DATE, day);
                        this.Set(calendar, CalendarManager.HOUR, hour);
                        this.Set(calendar, CalendarManager.MINUTE, minute);
                        this.Set(calendar, CalendarManager.SECOND, second);
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        //Month value is 0-based. e.g., 0 for January
                        tempProps.dateTime = new System.DateTime(year, month + 1, day, hour, minute, second);
                        this.Add(calendar, tempProps);
                    }
                }

                public int Get(System.Globalization.Calendar calendar, int field)
                {
                    if (this[calendar] != null)
                    {
                        switch (field)
                        {
                            case CalendarManager.DATE:
                                return ((CalendarProperties)this[calendar]).dateTime.Day;
                            case CalendarManager.HOUR:
                                int tempHour = ((CalendarProperties)this[calendar]).dateTime.Hour;
                                return tempHour > 12 ? tempHour - 12 : tempHour;

                            case CalendarManager.MILLISECOND:
                                return ((CalendarProperties)this[calendar]).dateTime.Millisecond;
                            case CalendarManager.MINUTE:
                                return ((CalendarProperties)this[calendar]).dateTime.Minute;
                            case CalendarManager.MONTH:
                                //Month value is 0-based. e.g., 0 for January
                                return ((CalendarProperties)this[calendar]).dateTime.Month - 1;
                            case CalendarManager.SECOND:
                                return ((CalendarProperties)this[calendar]).dateTime.Second;
                            case CalendarManager.YEAR:
                                return ((CalendarProperties)this[calendar]).dateTime.Year;
                            case CalendarManager.DAY_OF_MONTH:
                                return ((CalendarProperties)this[calendar]).dateTime.Day;
                            case CalendarManager.HOUR_OF_DAY:
                                return ((CalendarProperties)this[calendar]).dateTime.Hour;

                            default:
                                return 0;
                        }
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTime = System.DateTime.Now;
                        this.Add(calendar, tempProps);
                        return this.Get(calendar, field);
                    }
                }

                public void SetTimeInMilliseconds(System.Globalization.Calendar calendar, long milliseconds)
                {
                    if (this[calendar] != null)
                    {
                        ((CalendarProperties)this[calendar]).dateTime = new System.DateTime(milliseconds);
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTime = new System.DateTime(System.TimeSpan.TicksPerMillisecond * milliseconds);
                        this.Add(calendar, tempProps);
                    }
                }

                public System.DayOfWeek GetFirstDayOfWeek(System.Globalization.Calendar calendar)
                {
                    if (this[calendar] != null && ((CalendarProperties)this[calendar]).dateTimeFormat != null)
                    {
                        return ((CalendarProperties)this[calendar]).dateTimeFormat.FirstDayOfWeek;
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
                        tempProps.dateTimeFormat.FirstDayOfWeek = System.DayOfWeek.Sunday;
                        this.Add(calendar, tempProps);
                        return this.GetFirstDayOfWeek(calendar);
                    }
                }

                public void SetFirstDayOfWeek(System.Globalization.Calendar calendar, System.DayOfWeek firstDayOfWeek)
                {
                    if (this[calendar] != null && ((CalendarProperties)this[calendar]).dateTimeFormat != null)
                    {
                        ((CalendarProperties)this[calendar]).dateTimeFormat.FirstDayOfWeek = firstDayOfWeek;
                    }
                    else
                    {
                        CalendarProperties tempProps = new CalendarProperties();
                        tempProps.dateTimeFormat = new System.Globalization.DateTimeFormatInfo();
                        this.Add(calendar, tempProps);
                        this.SetFirstDayOfWeek(calendar, firstDayOfWeek);
                    }
                }

                public void Clear(System.Globalization.Calendar calendar)
                {
                    if (this[calendar] != null)
                        this.Remove(calendar);
                }

                public void Clear(System.Globalization.Calendar calendar, int field)
                {
                    if (this[calendar] != null)
                        this.Remove(calendar);
                    else
                        this.Set(calendar, field, 0);
                }

                class CalendarProperties
                {
                    public System.DateTime dateTime;
                    public System.Globalization.DateTimeFormatInfo dateTimeFormat;
                }
            }
        }

    }
}
