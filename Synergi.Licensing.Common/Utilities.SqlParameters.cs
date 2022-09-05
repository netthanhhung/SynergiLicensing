using System;
using System.Data.SqlClient;
using System.Data;

namespace Synergi.Licensing.Common
{
    public static partial class Utilities
    {
        [System.Diagnostics.DebuggerStepThrough()]
        public static SqlParameter MakeInputOutputParameter<T>(string parameterName, T value)
        {
            return MakeParameter(parameterName, value, ParameterDirection.InputOutput);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static SqlParameter MakeInputParameter<T>(string parameterName, T value)
        {
            return MakeParameter(parameterName, value, ParameterDirection.Input);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static SqlParameter MakeOutputParameter<T>(string parameterName, T value)
        {
            return MakeParameter(parameterName, value, ParameterDirection.Output);
        }

        [System.Diagnostics.DebuggerStepThrough()]
        public static SqlParameter MakeParameter<T>(string parameterName, T value, ParameterDirection direction)
        {
            SqlParameter ret = null;
            object obValue = ParseObject(value);   // Get our 'converted' value back...

            if (null == value && obValue == System.DBNull.Value)
            {
                Type genericType = typeof(T);

                if (genericType.IsGenericType) // To be at this location, it should be a generic type!
                {
                    Type[] typeArray = genericType.GetGenericArguments();

                    if (typeArray.Length == 1)   // Only one we expect to deal with
                    {
                        Type underlyingType = typeArray[0];
                        System.Data.SqlDbType sqlType = GetSqlTypeFromSystemType(underlyingType);
                        ret = new SqlParameter(parameterName, sqlType);
                        ret.Value = System.DBNull.Value;
                    }
                    else
                        throw new InvalidOperationException("MakeParameter");
                }
                else
                    throw new InvalidOperationException("MakeParameter");
            }
            else
                ret = new SqlParameter(parameterName, obValue);

            ret.Direction = direction;

            return ret;
        }

        public static SqlDbType GetSqlTypeFromSystemType(System.Type underlyingType)
        {
            if (underlyingType == typeof(Int64))
                return System.Data.SqlDbType.BigInt;

            if (underlyingType == typeof(Byte[]))
                return System.Data.SqlDbType.Binary;

            if (underlyingType == typeof(bool))
                return System.Data.SqlDbType.Bit;

            if (underlyingType == typeof(string))
                return System.Data.SqlDbType.NVarChar;

            if (underlyingType == typeof(DateTime))
                return System.Data.SqlDbType.DateTime;

            if (underlyingType == typeof(Decimal))
                return System.Data.SqlDbType.Decimal;

            if (underlyingType == typeof(Double))
                return System.Data.SqlDbType.Float;

            if (underlyingType == typeof(Int32))
                return System.Data.SqlDbType.Int;

            if (underlyingType == typeof(Single))
                return System.Data.SqlDbType.Real;

            if (underlyingType == typeof(int))
                return System.Data.SqlDbType.SmallInt;

            if (underlyingType == typeof(Byte))
                return System.Data.SqlDbType.TinyInt;

            if (underlyingType == typeof(Guid))
                return System.Data.SqlDbType.UniqueIdentifier;

            return System.Data.SqlDbType.Variant;
        }
    }
}
