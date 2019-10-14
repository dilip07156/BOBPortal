using System;
using System.Configuration;
using NLog;

namespace CardHolderOracle.DAL
{
    public static class common
    {
        #region Variables
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static string col_Cr_Account_Nbr = "Cr_Account_Nbr";
        public static string col_Embossed_Name = "Embossed_Name";
        #endregion

        #region Helper Methods
        /// <summary>
        /// Gets the connectionstring.
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionstring()
        {
            string DecryptedCn = string.Empty;
            string EncryptedCn = ConfigurationManager.ConnectionStrings["bobcardconnection"].ConnectionString;
            if (!string.IsNullOrWhiteSpace(EncryptedCn))
            {
                DecryptedCn = EncryptedCn.DecryptForOrCnOnly();
            }
            return DecryptedCn;
        }

        #endregion

        #region string Extention Methods for encrypt decrypt

        ///// <summary>
        ///// Encrypts the specified STR to encrypt.
        ///// </summary>
        ///// <param name="strToEncrypt">The STR to encrypt.</param>
        ///// <returns></returns>
        //public static string EncryptForOrCnOnly(this String strToEncrypt)
        //{
        //    CryptForOracleCn objCrypt = new CryptForOracleCn();
        //    try
        //    {
        //        return objCrypt.EncryptText(strToEncrypt, "TH#&^$HSJB$@#^GGHWF&)!&^@*(#$HJDY");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objCrypt = null;
        //    }
        //}

        /// <summary>
        /// Decrypts the specified STR to decrypt.
        /// </summary>
        /// <param name="strToDecrypt">The STR to decrypt.</param>
        /// <returns></returns>
        public static string DecryptForOrCnOnly(this String strToDecrypt)
        {
            CryptForOracleCn objCrypt = new CryptForOracleCn();
            try
            {
                return objCrypt.DecryptText(strToDecrypt, "TH#&^$HSJB$@#^GGHWF&)!&^@*(#$HJDY");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCrypt = null;
            }
        }

        ///// <summary>
        ///// Decrypts the specified STR to decrypt.
        ///// </summary>
        ///// <param name="strToDecrypt">The STR to decrypt.</param>
        ///// <param name="IsQueryStringParameter">if set to <c>true</c> [is query string parameter].</param>
        ///// <returns></returns>
        //public static string DecryptForOrCnOnly(this String strToDecrypt, bool IsQueryStringParameter = false)
        //{
        //    CryptForOracleCn objCrypt = new CryptForOracleCn();

        //    try
        //    {
        //        return objCrypt.DecryptText(IsQueryStringParameter ? strToDecrypt.Replace(" ", "+") : strToDecrypt, "TH#&^$HSJB$@#^GGHWF&)!&^@*(#$HJDY");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objCrypt = null;
        //    }
        //}

        #endregion string Extention Methods for encrypt decrypt


    }
}
