using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressShop.Storage.Extensions
{
    public static class Extensions
    {
        #region Bool

        public static bool Not(this bool arg)
        {
            return !arg;
        }

        #endregion

        #region IEnumerable

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> src)
        {
            return src.IsNull() || src.Any().Not();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> src)
        {
            return src.IsNotNull() && src.Any();
        }

        #endregion

        #region Guid

        public static bool IsNotEmpty(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsNotEmpty(this Guid? guid)
        {
            return guid.HasValue && guid.Value != Guid.Empty;
        }

        public static bool IsEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }

        #endregion

        #region Object

        public static bool IsNull(this object arg)
        {
            return arg == null;
        }

        public static bool IsNotNull(this object arg)
        {
            return arg != null;
        }

        #endregion

        #region String

        public static bool IsEmpty(this string src)
        {
            return string.IsNullOrWhiteSpace(src);
        }

        public static bool IsNotEmpty(this string src)
        {
            return !string.IsNullOrWhiteSpace(src);
        }

        #endregion

        #region Decimal

        public static bool CheckDecimalValue(this decimal value)
        {
            return value >= 0.00m ? true : false;
        }

        #endregion
    }
}
