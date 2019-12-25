using System;
using System.Diagnostics;
using System.Linq;

namespace Common
{
    public static class CGuard
    {
        [DebuggerStepThrough]
        public static void IsNotNull(Object value, String paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        [DebuggerStepThrough]
        public static void IsEnumValueInRange<T>(T value) where T : struct
        {
            if (!Enum.GetValues(typeof(T)).Cast<T>().Contains(value))
            {
                throw new ArgumentOutOfRangeException($"{value}");
            }
        }

        [DebuggerStepThrough]
        public static T CheckType<T>(Object value, String paramName) where T : class
        {
            IsNotNull(value, paramName);
            var result = value as T;

            if (result == null)
            {
                throw new ArgumentException($"Parameter type is not an '{typeof(T).Name}'.", paramName);
            }

            return result;
        }

        [DebuggerStepThrough]
        public static void IsValid(Boolean isValid, String paramName)
        {
            if (!isValid)
            {
                throw new ArgumentException($"'{paramName}' is invalid.", paramName);
            }
        }


        [DebuggerStepThrough]
        public static void IsNotNullOrWhiteSpace(String value, String paramName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{paramName}' is null or white space.", paramName);
            }
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmpty(String value, String paramName)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"'{paramName}' is null or white space.", paramName);
            }
        }
    }
}
