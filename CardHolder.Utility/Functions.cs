using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.IO;
using System;
using System.Web.Security;
using CardHolder.DTO;

namespace CardHolder.Utility
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
   public static class Functions
    {
        /// <summary>
        /// Generates the hash.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns></returns>
        /// <remarks></remarks>
       public static string GenerateHash(string plainText)
       {
           MD5 md5 = MD5.Create();
           byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(plainText));

           StringBuilder result = new StringBuilder(32);
           foreach (byte b in hashBytes)
           {
               result.Append(b.ToString("x2").ToUpper()); // used to convert each byte to a hex string
           }

           return result.ToString().ToLower();
       }

       /// <summary>
       /// Logs the out me.
       /// </summary>
       /// <remarks></remarks>
       public static void LogOutMe()
       {
           CacheHelperBySession<CardHolder_MstDTO>.InvalidateCache();
           FormsAuthentication.SignOut();
           HttpContext.Current.Session.Clear();
           HttpContext.Current.Session.Abandon();
           HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
           HttpContext.Current.Response.Redirect(Constants.loginPage, true);

       }

       /// <summary>
       /// Gets the IP.
       /// </summary>
       /// <returns></returns>
       /// <remarks></remarks>
       public static string GetIP()
       {
           return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
       }

    }
}
