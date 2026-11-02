using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashReg.Helper
{
    public static class EcrConvert
    {
        public static Int16 ByteToInt16(byte[] value)
        {
            return BitConverter.ToInt16(value,0);
        }
        public static Int16 BeByteToInt16(byte[] value)
        {
            if (value == null) return 0;
            byte[] buffer = new byte[value.Count()];
            value.CopyTo(buffer, 0);
            if (BitConverter.IsLittleEndian) Array.Reverse(buffer);
            var result = BitConverter.ToInt16(buffer, 0);
            return result;
        }
        public static Int16 LittleEndianByteToInt16(byte[] value)
        {
            byte[] buffer = new byte[value.Count()];
            value.CopyTo(buffer, 0);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer,0);
        }
        public static byte[] Int16ToByte(Int16 value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] Int16ToBigEndianByte(Int16 value)
        {
            var buffer = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return buffer;
        }
        public static byte[] Int16ToLittleEndianByte(Int16 value)
        {
            var buffer = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return buffer;
        }
        public static Int32 ByteToInt(byte[] value)
        {
            return BitConverter.ToInt32(value,0);
        }
        public static Int32 BigEndianByteToInt(byte[] value)
        {
            byte[] buffer = new byte[value.Count()];
            value.CopyTo(buffer, 0);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer,0);
        }
        public static Int32 LittleEndianByteToInt(byte[] value)
        {
            byte[] buffer = new byte[value.Count()];
            value.CopyTo(buffer, 0);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer,0);
        }
        public static byte[] IntToByte(int value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] IntToBigEndianByte(int value)
        {
            var buffer = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return buffer;
        }
        public static byte[] IntToLittleEndianByte(int value)
        {
            var buffer = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return buffer;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static string ImageToBase64String(Bitmap bitmap)
        {
            // Convert the image to byte[]
            var stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imageBytes = stream.ToArray();

            // Convert byte[] to Base64 String
            return Convert.ToBase64String(imageBytes);
        }
        public static DateTime IntToDateTime(int date)
        {
            return  DateTime.Parse(date.ToString());
        }
        /// <summary>
        /// Convert a date time object to Unix time representation.
        /// </summary>
        /// <param name="datetime">The datetime object to convert to Unix time stamp.</param>
        /// <returns>Returns a numerical representation (Unix time) of the DateTime object.</returns>
        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }
        /// <summary>
        /// Convert Unix time value to a DateTime object.
        /// </summary>
        /// <param name="unixtime">The Unix time stamp you want to convert to DateTime.</param>
        /// <returns>Returns a DateTime object that represents value of the Unix time.</returns>
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }

        public static DateTime ConvertUnixTimestampToDatetime(long millis)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddMilliseconds(millis);

            return time.AddHours(4);
        }
        public static long ConvertDatetimeToUnixTimestamp(DateTime value)
        {
            TimeSpan span = (value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)));
            return (long) span.TotalMilliseconds;
        }
        //public static long ConvertDatetimeToUnixTimestamp(DateTime value)
        //{
        //    var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0));
        //    return (long)timeSpan.TotalMilliseconds;
            //TimeSpan span = (value.AddHours(-4) - new DateTime(1970, 1, 1, 0, 0, 0, 0));
            //return (int)span.TotalMilliseconds;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertToInt(string value)
        {
            return value != null ? int.Parse(value) : 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToDouble(string value)
        {
            return value != null ? double.Parse(value) : 0.0;
        }
    }
}
