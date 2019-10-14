using System;
using System.Collections.Generic;

namespace CardHolder.Utility.Enums
{
    //public enum UserTypes
    //{
    //    Admin,
    //    StaffUser
    //}

    public static class EnumUtils
    {
        public static IDictionary<int, string> GetListItemCollection<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (int value in Enum.GetValues(enumerationType))
            {
                var name = Enum.GetName(enumerationType, value);
                dictionary.Add(value, name);
            }

            return dictionary;
        }

        #region String to Enum

        public static T ParseEnum<T>(string inString, bool ignoreCase = true, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(inString, default(T), ignoreCase, throwException);
        }

        public static T ParseEnum<T>(string inString, T defaultValue, bool ignoreCase = true, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;

            if (!typeof(T).IsEnum || String.IsNullOrEmpty(inString))
            {
                throw new InvalidOperationException("Invalid Enum Type or Input String 'inString'. " + typeof(T).ToString() + " must be an Enum");
            }

            try
            {
                bool success = Enum.TryParse<T>(inString, ignoreCase, out returnEnum);
                if (!success && throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid Cast", ex);
            }

            return returnEnum;
        }

        #endregion String to Enum

        #region Int to Enum

        //public static T ParseEnum<T>(int input, bool throwException = true) where T : struct
        //{
        //    return (T)ParseEnum<T>(input, default(T), throwException);
        //}

        public static T ParseEnum<T>(int input, T defaultValue, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("Invalid Enum Type. " + typeof(T).ToString() + " must be an Enum");
            }
            if (Enum.IsDefined(typeof(T), input))
            {
                returnEnum = (T)Enum.ToObject(typeof(T), input);
            }
            else
            {
                if (throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }

            return returnEnum;
        }

        #endregion Int to Enum
    }

    public enum ErrorStatus
    {
        BlockedAccount = 1,
        InactiveAccount = 2,
        AccNotInNormalState = 3,
        ContinuesBlockedAccount = 4,
        InactiveAttempts = 5
    }

    public enum TranscationType
    {
        PC = 1,
        CB = 2,
        PA = 3,
        IL = 4,
        BA = 5
    }
}