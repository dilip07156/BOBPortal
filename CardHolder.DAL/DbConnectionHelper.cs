using System;

namespace CardHolder.DAL
{
   public static class DbConnectionHelper
    {

       public static string GetConnectionString()
       {
           string DecryptedCn = string.Empty;
           string EncryptedCn = System.Configuration.ConfigurationManager.ConnectionStrings["BOBCardEntities"].ConnectionString;
           if (!string.IsNullOrWhiteSpace(EncryptedCn))
           {
               DecryptedCn = EncryptedCn.DecryptForDALOnly();
           }
           return DecryptedCn;
       }

       #region string Extention Methods for encrypt decrypt

       /// <summary>
       /// Encrypts the specified STR to encrypt.
       /// </summary>
       /// <param name="strToEncrypt">The STR to encrypt.</param>
       /// <returns></returns>
       public static string EncryptForDALOnly(this String strToEncrypt)
       {
           Crypt objCrypt = new Crypt();
           try
           {
               return objCrypt.EncryptText(strToEncrypt, "TH#&^$HSJB$@#^GGHWF&)!&^@*(#$HJDY");
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

       /// <summary>
       /// Decrypts the specified STR to decrypt.
       /// </summary>
       /// <param name="strToDecrypt">The STR to decrypt.</param>
       /// <returns></returns>
       public static string DecryptForDALOnly(this String strToDecrypt)
       {
           Crypt objCrypt = new Crypt();
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

       /// <summary>
       /// Decrypts the specified STR to decrypt.
       /// </summary>
       /// <param name="strToDecrypt">The STR to decrypt.</param>
       /// <param name="IsQueryStringParameter">if set to <c>true</c> [is query string parameter].</param>
       /// <returns></returns>
       public static string DecryptForDALOnly(this String strToDecrypt, bool IsQueryStringParameter = false)
       {
           Crypt objCrypt = new Crypt();

           try
           {
               return objCrypt.DecryptText(IsQueryStringParameter ? strToDecrypt.Replace(" ", "+") : strToDecrypt, "TH#&^$HSJB$@#^GGHWF&)!&^@*(#$HJDY");
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

       #endregion string Extention Methods for encrypt decrypt


    }
}
