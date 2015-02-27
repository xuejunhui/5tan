using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTAN.CommonUtility
{
    /// <summary>
    /// 數據轉型處理中心
    /// </summary>
    public static class DataTransformation
    {
        public static object ToValue(this object value, string typeName)
        {
            switch (typeName.ToLower())
            {
                case "int":
                    return ToInt32Value(value);
                case "long":
                    return ToInt64Value(value);
                case "string":
                    return ToStringValue(value);
                case "double":
                    return ToDoubleValue(value);
                case "datetime":
                    return ToDateTimeValue(value);
                case "bool":
                    return ToBoolValue(value);
                default:
                    return value;
            }
        }

        public static bool ToBoolValue(this object value)
        {
            return value == null ? false : ((value is bool) ? (bool)value : Convert.ToBoolean(value));
        }

        public static decimal ToDecimal(this object value)
        {
            return value == null ? 0 : value is Decimal ? (decimal)value : 0;
        }

        public static long ToInt64Value(this object value)
        {
            long result = 0;
            if (value is byte[])
            {
                var vv = (byte[])value;
                if (vv.Length > 0)
                {
                    long p = 1;
                    for (int i = vv.Length - 1; i >= 0; i--)
                    {
                        result += vv[i] * p;
                        p = p * 256;
                    }
                }
                return result;
            }
            return value == null ? 0 : ((value is Int64) ? (Int64)value : Convert.ToInt64(value));
        }

        public static int ToInt32Value(this object value)
        {
            return value == null ? 0 : ((value is Int32) ? (Int32)value : Convert.ToInt32(value));
        }

        public static string ToStringValue(this object value)
        {
            return value == null ? string.Empty : ((value is string) ? (string)value : value.ToString());
        }

        public static double ToDoubleValue(this object value)
        {
            return value == null ? 0 : ((value is double) ? (double)value : Convert.ToDouble(value));
        }

        public static DateTime ToDateTimeValue(this object value)
        {
            return value == null ? new DateTime(1900, 1, 1) : ((value is DateTime) ? (DateTime)value : Convert.ToDateTime(value));
        }
    }
}
