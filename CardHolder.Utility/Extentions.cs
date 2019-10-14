using System;
using System.Configuration;
using System.Web;
using System.Web.UI.HtmlControls;
using CardHolder.Utility.Enums;

namespace CardHolder.Utility
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class Extentions
    {
        #region string Extention Methods for encrypt decrypt

        /// <summary>
        /// Encrypts the specified STR to encrypt.
        /// </summary>
        /// <param name="strToEncrypt">The STR to encrypt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Encrypt(this String strToEncrypt)
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
        /// <remarks></remarks>
        public static string Decrypt(this String strToDecrypt)
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
        /// <remarks></remarks>
        public static string Decrypt(this String strToDecrypt, bool IsQueryStringParameter = false)
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


        #region string Extention Methods for encrypt decrypt URL

        /// <summary>
        /// Encrypts the specified URL to encrypt.
        /// </summary>
        /// <param name="strToEncrypt">The STR to encrypt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string EncryptURL(this String strToEncrypt)
        {
            Crypt objCrypt = new Crypt();
            try
            {
                return objCrypt.EncryptURL(strToEncrypt);
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
        /// Decrypts the specified URL to decrypt.
        /// </summary>
        /// <param name="strToDecrypt">The STR to decrypt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DecryptURL(this String strToDecrypt)
        {
            Crypt objCrypt = new Crypt();
            try
            {
                return objCrypt.DecryptURL(strToDecrypt);
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

        #region string extenstion methods to strip html tages

        /// <summary>
        /// Strips the HTML tags.
        /// </summary>
        /// <param name="strToStrip">The STR to strip.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string StripHtmlTags(this String strToStrip)
        {
            char[] array = new char[strToStrip.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < strToStrip.Length; i++)
            {
                char let = strToStrip[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Strips the HTML tags. With Exlude parameter
        /// </summary>
        /// <param name="strToStrip">The STR to strip.</param>
        /// <param name="strTagesToExlude">The STR tages to exlude.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string StripHtmlTags(this String strToStrip, string[] strTagesToExlude)
        {
            char[] array = new char[strToStrip.Length];
            int arrayIndex = 0;
            bool inside = false;
            bool insideExludeTag = false;
            for (int i = 0; i < strToStrip.Length; i++)
            {
                char let = strToStrip[i];
                if (let == '<')
                {
                    inside = true;
                    for (int z = 0; z < strTagesToExlude.Length; z++)
                    {
                        int startindex = i + 1;
                        if (strToStrip[i + 1] == '/')
                            startindex++;
                        if ((startindex + strTagesToExlude[z].Length) < strToStrip.Length && strToStrip.Substring(startindex, strTagesToExlude[z].Length) == strTagesToExlude[z])
                        {
                            insideExludeTag = true;
                            inside = false;
                            break;
                        }
                    }
                    if (inside)
                        continue;
                }
                if (let == '>' && insideExludeTag == false)
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        #endregion string extenstion methods to strip html tages

        #region String Extension Methods for Enum Parsing

        /// <summary>
        /// String extension method for get related enum value
        /// </summary>
        /// <typeparam name="T">Target Enum Type</typeparam>
        /// <param name="inString">The in string.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ToEnum<T>(this string inString, bool ignoreCase = true, bool throwException = true) where T : struct
        {
            return (T)EnumUtils.ParseEnum<T>(inString, ignoreCase, throwException);
        }

        /// <summary>
        /// String extension method for get related enum value
        /// </summary>
        /// <typeparam name="T">Target Enum Type</typeparam>
        /// <param name="inString">The in string.</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ToEnum<T>(this string inString, T defaultValue, bool ignoreCase = true, bool throwException = false) where T : struct
        {
            return (T)EnumUtils.ParseEnum<T>(inString, defaultValue, ignoreCase, throwException);
        }

        #endregion String Extension Methods for Enum Parsing

        #region Int Extension Methods for Enum Parsing

        /// <summary>
        /// Integer extension method for get related enum value
        /// </summary>
        /// <typeparam name="T">Target Enum Type</typeparam>
        /// <param name="input">The input.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ToEnum<T>(this int input, bool throwException = true) where T : struct
        {
            return (T)EnumUtils.ParseEnum<T>(input, default(T), throwException);
        }

        /// <summary>
        /// Integer extension method for get related enum value
        /// </summary>
        /// <typeparam name="T">Target Enum Type</typeparam>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T ToEnum<T>(this int input, T defaultValue, bool throwException = false) where T : struct
        {
            return (T)EnumUtils.ParseEnum<T>(input, defaultValue, throwException);
        }

        #endregion Int Extension Methods for Enum Parsing

        #region Controles extention methods

        /// <summary>
        /// ListControl extension method to bind it with Enum type
        /// </summary>
        /// <typeparam name="TEnum">Target Enum Type</typeparam>
        /// <param name="ddl">The DDL.</param>
        /// <remarks></remarks>
        public static void BindByEnum<TEnum>(this System.Web.UI.WebControls.ListControl ddl) where TEnum : struct
        {
            ddl.DataSource = EnumUtils.GetListItemCollection<TEnum>();
            ddl.DataTextField = "Value";
            ddl.DataValueField = "Key";
            ddl.DataBind();
        }

        #endregion Controles extention methods

        #region Page Extentsion and Static Methods
        /// <summary>
        /// 
        /// </summary>
        public static string strRoolPath = UrlHelper.GetAbsoluteUri();
        
       // public const string strRoolPath = "http://localhost:57685";
       //  public const string strRoolPath = "https://202.131.117.209:1551";
       //   public const string strRoolPath = "https://192.168.0.220:1603";

        /// <summary>
        /// Page extention method to get root css path
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetCSSPath(this System.Web.UI.Page objPage)
        {
            return GetCSSPath();
        }

        /// <summary>
        /// Static method to get root css path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetCSSPath()
        {
            return strRoolPath + "/css/";
        }

        /// <summary>
        /// Page extention method to get root javascript path
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetJScriptPath(this System.Web.UI.Page objPage)
        {
            return GetJScriptPath();
        }

        /// <summary>
        /// Static method to get root javascript path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetJScriptlibraryPath(this System.Web.UI.Page objPage)
        {
            return strRoolPath + "/assets/scripts/";
        }
        /// <summary>
        /// Static method to get root javascript path
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetJScriptPath()
        {
            return strRoolPath + "/javascript/";
        }

        /// <summary>
        /// Page extention method to get image path
        /// </summary>
        /// <param name="strPath">Image name with subfolder path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetImagePath(string strPath = "")
        {
            return strRoolPath + "/images/" + strPath + getCSSJSVersion();
        }

        /// <summary>
        /// Static method to get image path
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <param name="strPath">Image name with subfolder path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetImagePath(this System.Web.UI.Page objPage, string strPath = "")
        {
            return GetImagePath(strPath);
        }

        /// <summary>
        /// Get current css and javascript version
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string getCSSJSVersion()
        {
            string strVersion = "?2";
            return strVersion;
        }

        /// <summary>
        /// Page extention method to register css
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <param name="CSSPath">CSS name with subfolder path</param>
        /// <param name="Index">index of css to add at</param>
        /// <remarks></remarks>
        public static void RegisterStyleSheet(this System.Web.UI.Page objPage, string CSSPath, int Index = 0)
        {
            string strVersion = getCSSJSVersion();
            System.Web.UI.HtmlControls.HtmlLink h1 = new HtmlLink();
            h1.Href = objPage.GetCSSPath().ToLower() + CSSPath.ToLower() + strVersion;
            h1.Attributes.Add("rel", "stylesheet");
            h1.Attributes.Add("type", "text/css");
            if (Index == 0)
            {
                objPage.Page.Header.Controls.Add(h1);
            }
            else
            {
                objPage.Page.Header.Controls.AddAt(Index, h1);
            }
        }

        /// <summary>
        /// Page extention method to register css
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <param name="CSSPath">CSS name with subfolder path</param>
        /// <param name="Index">index of css to add at</param>
        /// <remarks></remarks>
        public static void RegisterNewCSS(this System.Web.UI.Page objPage, string CSSPath, int Index = 0)
        {
            string strVersion = getNEWCSSJSVersion();
            System.Web.UI.HtmlControls.HtmlLink h1 = new HtmlLink();
            h1.Href = objPage.GetNewCSSPath().ToLower() + CSSPath.ToLower() + strVersion;
            h1.Attributes.Add("rel", "stylesheet");
            h1.Attributes.Add("type", "text/css");
            if (Index == 0)
            {
                objPage.Page.Header.Controls.Add(h1);
            }
            else
            {
                objPage.Page.Header.Controls.AddAt(Index, h1);
            }
        }

        /// <summary>
        /// Page extention method to register javascript
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <param name="JsPath">Javascript name with subfolder path</param>
        /// <param name="Index">index of css to add at</param>
        /// <remarks></remarks>
        public static void RegisterNewJavaScript(this System.Web.UI.Page objPage, string JsPath, int Index = 0)
        {
            string strVersion = getNEWCSSJSVersion();
            HtmlGenericControl gc1 = new HtmlGenericControl("script");
            gc1.Attributes.Add("type", "text/javascript");
            gc1.Attributes.Add("src", objPage.GetJScriptlibraryPath().ToLower() + JsPath.ToLower() + strVersion);

            System.Web.UI.WebControls.PlaceHolder ph = objPage.Master.FindControl("phJavaScripts") as System.Web.UI.WebControls.PlaceHolder;
            if (ph != null)
            {
                if (Index == 0)
                {
                    ph.Controls.Add(gc1);
                }
                else
                {
                    ph.Controls.AddAt(Index, gc1);
                }
            }
            else
            {
                if (Index == 0)
                {
                    objPage.Header.Controls.Add(gc1);
                }
                else
                {
                    objPage.Header.Controls.AddAt(Index, gc1);
                }
            }
        }

        /// <summary>
        /// Page extention method to get root css path
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetNewCSSPath(this System.Web.UI.Page objPage)
        {
            return strRoolPath + "/assets/scss/";
        }
        /// <summary>
        /// Get current css and javascript version
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string getNEWCSSJSVersion()
        {
            string strVersion = "?3";
            return strVersion;
        }

        /// <summary>
        /// Static method to get image path
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <param name="strPath">Image name with subfolder path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetNewImagePath(this System.Web.UI.Page objPage, string strPath = "")
        {
            return GetNewDesignImagePath(strPath);
        }

        /// <summary>
        /// Page extention method to get image path
        /// </summary>
        /// <param name="strPath">Image name with subfolder path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetNewDesignImagePath(string strPath = "")
        {
            return strRoolPath + "/assets/images/" + strPath + getCSSJSVersion();
        }
        /// <summary>
        /// Page extention method to register javascript
        /// </summary>
        /// <param name="objPage">The obj page.</param>
        /// <param name="JsPath">Javascript name with subfolder path</param>
        /// <param name="Index">index of css to add at</param>
        /// <remarks></remarks>
        public static void RegisterJavaScript(this System.Web.UI.Page objPage, string JsPath, int Index = 0)
        {
            string strVersion = getCSSJSVersion();
            HtmlGenericControl gc1 = new HtmlGenericControl("script");
            gc1.Attributes.Add("type", "text/javascript");
            gc1.Attributes.Add("src", objPage.GetJScriptPath().ToLower() + JsPath.ToLower() + strVersion);

            System.Web.UI.WebControls.PlaceHolder ph = objPage.Master.FindControl("phJavaScripts") as System.Web.UI.WebControls.PlaceHolder;
            if (ph != null)
            {
                if (Index == 0)
                {
                    ph.Controls.Add(gc1);
                }
                else
                {
                    ph.Controls.AddAt(Index, gc1);
                }
            }
            else
            {
                if (Index == 0)
                {
                    objPage.Header.Controls.Add(gc1);
                }
                else
                {
                    objPage.Header.Controls.AddAt(Index, gc1);
                }
            }
        }

        ///// <summary>
        ///// Page extention method to register third party javascript
        ///// </summary>
        ///// <param name="objPage"></param>
        ///// <param name="JsPath">Javascript path (third party URL)</param>
        ///// <param name="index">index of css to add at</param>
        //public static void RegisterThirdPartyJavaScript(this System.Web.UI.Page objPage, string JsPath, int index)
        //{
        //    if (HttpContext.Current.Request.IsSecureConnection)
        //        JsPath = JsPath.Replace("http:", "https:");

        //    HtmlGenericControl gc1 = new HtmlGenericControl("script");
        //    gc1.Attributes.Add("type", "text/javascript");
        //    gc1.Attributes.Add("src", JsPath);

        //    System.Web.UI.WebControls.PlaceHolder ph = objPage.FindControl("phJavaScripts") as System.Web.UI.WebControls.PlaceHolder;
        //    if (ph != null)
        //    {
        //        if (index == 0)
        //        {
        //            ph.Controls.Add(gc1);
        //        }
        //        else
        //        {
        //            ph.Controls.AddAt(index, gc1);
        //        }
        //    }
        //    else
        //    {
        //        if (index == 0)
        //        {
        //            objPage.Header.Controls.Add(gc1);
        //        }
        //        else
        //        {
        //            objPage.Header.Controls.AddAt(index, gc1);
        //        }
        //    }
        //}

        #endregion Page Extentsion and Static Methods
    }
}