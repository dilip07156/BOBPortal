using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using NLog;

namespace CardHolder.Utility.Payment
{
    public class CalculateCheckSum
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        const string CHECKSUMKEY = "i9yQOHNhD1hV";
        /// <summary>
        /// Calculate Checksum
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetCheckSum(string data,string type)
        {
            data = data + "|" + CHECKSUMKEY;
            logger.Info(type + " Calculate CheckSum: " + data);
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] hashValue;
            byte[] message = encoder.GetBytes(data);
            SHA256Managed hashString = new SHA256Managed();
            string checksum = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                checksum += String.Format("{0:x2}", x);
            }
            return checksum.ToUpper();
        }

        /// <summary>
        /// Compare Checksum
        /// </summary>
        /// <param name="response"></param>
        /// <param name="checksum"></param>
        /// <returns></returns>
        public static bool VarifyCheckSum(string[] data, string checksum)
        {
            string response = data.Take(data.Count() - 1).Aggregate((x, y) => x + "|" + y);
            string calculate_checksum = GetCheckSum(response, "Response");
            logger.Info("Response Verify: " + (response + "|" +calculate_checksum));
            if (checksum.Trim() == calculate_checksum.Trim())
            {
                return true;
            }
            return false;
        }
    }
}
