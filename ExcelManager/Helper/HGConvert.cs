using System;

namespace TaxServiceInvoices.Helper
{
    /// <summary>
    /// Converts a base data type to another base data type.
    /// </summary>
    public static class HgConvert
    {
        /// <summary>
        /// Returns the specified Boolean value
        /// </summary>
        public static bool ToBoolean(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return false;
            }
            return Convert.ToBoolean(o);
        }

        /// <summary>
        /// Returns the specified 8-bit unsigned integer
        /// </summary>
        public static byte ToByte(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToByte(o);
        }

        /// <summary>
        /// Converts the value to a Unicode character.
        /// </summary>
        public static char ToChar(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return char.MinValue;
            }
            return Convert.ToChar(o);
        }

        /// <summary>
        /// Returns the specified System.DateTime;
        /// </summary>
        public static DateTime ToDateTime(object o)
        {
            try
            {
                if (o == null || o == DBNull.Value)
                {
                    return DateTime.MinValue;
                }
                return Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts the value to an equivalent System.Decimal number.
        /// </summary>
        public static decimal ToDecimal(object o)
        {
            if (o == null || (string) o == string.Empty || o == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(o);
        }

        /// <summary>
        /// Returns the specified double-precision floating point number
        /// </summary>
        public static double ToDouble(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDouble(o);
        }

        /// <summary>
        /// Converts the value to the equivalent 16-bit signed integer.
        /// </summary>
        public static Int16 ToInt16(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToInt16(o);
        }

        /// <summary>
        ///  Converts the value to an equivalent 32-bit signed integer.
        /// </summary>
        public static Int32 ToInt32(object o)
        {
            int i;
            if (o != null && int.TryParse(o.ToString(), out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        /// Converts the value to an equivalent 64-bit signed integer.
        /// </summary>
        public static Int64 ToInt64(object o)
        {
            long i;
            if (o != null && long.TryParse(o.ToString(), out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        /// Converts the value to an equivalent single-precision floating point number.
        /// </summary>
        public static float ToFloat(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToSingle(o);
        }

        /// <summary>
        /// Converts the value to its equivalent System.String representation.
        /// </summary>
        public static string ToString(object o)
        {
            if (o == null || o == DBNull.Value)
            {
                return string.Empty;
            }
            return Convert.ToString(o);
        }


    }
}
