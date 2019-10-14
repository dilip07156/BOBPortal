using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Security.Cryptography;
using com.bob.utils.security;

namespace CardHolder.Utility.bobibanking
{
    public class BobibankingCalculateCheckSum
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Calculate Checksum
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetCheckSum(string data)
        {
            string checksum = "";
            checksum = GetMD5Hash(data);
            return checksum;

        }

        public static string GetCheckSumNew(string data)
        {
            BOBSymmetricCipherHelper sch = new BOBSymmetricCipherHelper();
            string checksum = "";
            checksum = sch.getMD5Hash(data);
            return checksum;

        }
        /// <summary>
        /// Gets the M d5 hash.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetMD5Hash(string name)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] ba = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
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
            string calculate_checksum = GetCheckSum(response);
            logger.Info("Response Verify: " + (response + "|" + calculate_checksum));
            if (checksum.Trim() == calculate_checksum.Trim())
            {
                return true;
            }
            return false;
        }

        public static bool VarifyCheckSumNew(string[] data, string checksum)
        {
            BOBSymmetricCipherHelper sch = new BOBSymmetricCipherHelper();
            string strCheckSum = data[data.Length - 1].Split('=')[1];
            string hashgendata = data[0] + "&" + data[1] + "&" + data[2] + "&" + data[3] + "&" + data[4] + "&" + data[5] + "&" + data[6];
            string calculate_checksum = sch.getSHA512Hash(hashgendata);
            logger.Info("Response Verify: " + (checksum + "|" + calculate_checksum));
            if (strCheckSum.Trim() == calculate_checksum.Trim())
            {
                return true;
            }
            return false;
        }
    }
}
