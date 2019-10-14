using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;

namespace CardHolder.Utility
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class GeneralMethods
    {
        #region Variables

        //private static string dateFormat1 = "{0:dd}{1} {0:MMM yyyy}";

        //private static string dateFormat2 = "{0:dd}{1} {0:MMM yyyy HH:mm:ss}";

        /// <summary>
        /// 
        /// </summary>
        private static string dateFormat1 = "{0:dd-MMM-yyyy}";

        /// <summary>
        /// 
        /// </summary>
        private static string dateFormat2 = "{0:dd-MMM-yyyy HH:mm:ss}";

        #endregion

        #region Methods

        /// <summary>
        /// Checks the file extension.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Extensions">The extensions.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool CheckFileExtension(string FileName, string[] Extensions)
        {
            string _extension;
            try
            {
                _extension = Path.GetExtension(FileName).ToUpper();
                foreach (string ext in Extensions)
                {
                    if (_extension == ext.ToUpper()) return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Alerts the message.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        /// <param name="Message">The message.</param>
        /// <remarks></remarks>
        public static void AlertMessage(Page PageName, string Message)
        {
            string Script;
            Message = Message.Replace("'", " ");
            Message = Message.Replace(";", " ");
            Message = Message.Replace("\\", "\\\\");
            Script = "<script type='text/javascript' language='javascript'>";
            Script += "alert('" + Message + "',0);";
            //Script += "return confirm('Are you sure you want to delete this album?');";
            Script += "</script>";
            PageName.ClientScript.RegisterStartupScript(PageName.GetType(), "Alert", Script, false);
        }

        /// <summary>
        /// Confirms the alert message.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        /// <param name="Message">The message.</param>
        /// <remarks></remarks>
        public static void ConfirmAlertMessage(Page PageName, string Message)
        {
            string Script;
            Message = Message.Replace("'", " ");
            Message = Message.Replace(";", " ");
            Message = Message.Replace("\\", "\\\\");
            Script = "<script type='text/javascript' language='javascript'>";
            //Script += "alert('" + Message + "',0);";
            Script += "var result= confirm('Are you sure you want to delete this?');";
            Script += "alert('Return is : '+result)</script>";
            PageName.ClientScript.RegisterStartupScript(PageName.GetType(), "Alert", Script, false);
        }


        /// <summary>
        /// Checks the file header.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="bytes">The bytes.</param>
        /// <param name="Extensions">The extensions.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool CheckFileHeader(string FileName, Byte[] bytes, string[] Extensions)
        {
            string _extension;
            try
            {
                string hexString = string.Empty;
                for (int i = 0; i < bytes.Length; i++)
                    hexString += bytes[i].ToString("X2");
                _extension = Path.GetExtension(FileName).ToUpper();
                if (string.IsNullOrEmpty(hexString)) return false;
                else
                {
                    foreach (string ext in Extensions)
                    {
                        if (_extension == ext.ToUpper() && hexString.Contains(GetFileHeaderHexCode(ext)))
                            return true;
                    }
                }
            }
            catch (Exception ex) { return false; }
            return false;
        }

        /// <summary>
        /// Check File header to verify file type based on file extension.
        /// </summary>
        /// <param name="FileExtension">The file extension.</param>
        /// <returns>String of File Content Type</returns>
        /// <remarks></remarks>
        public static string GetFileHeaderHexCode(string FileExtension)
        {
            FileExtension = FileExtension.ToLower();
            Dictionary<string, string> d = new Dictionary<string, string>();
            //Images'
            d.Add(".bmp", "424D");
            d.Add(".gif", "47494638");
            d.Add(".jpeg", "FFD8FF");
            d.Add(".jpg", "FFD8FF");
            d.Add(".png", "89504E470D0A1A0A");
            d.Add(".tif", "492049");
            d.Add(".tiff", "492049");
            //Documents'
            d.Add(".doc", "D0CF11E0A1B11AE1");
            d.Add(".docx", "504B030414000600");
            d.Add(".pdf", "25504446");
            //Slideshows'
            d.Add(".ppt", "D0CF11E0A1B11AE1");
            d.Add(".pptx", "504B030414000600");
            //Data'
            d.Add(".xlsx", "504B030414000600");
            d.Add(".xls", "D0CF11E0A1B11AE1");
            //d.Add(".csv", "text/csv");
            d.Add(".xml", "3C");
            //d.Add(".txt", "text/plain");
            //Compressed Folders'
            d.Add(".zip", "504B");
            //Audio'
            d.Add(".ogg", "4F67675300020000000000000000");
            d.Add(".mp3", "494433");
            d.Add(".wma", "3026B2758E66CF11A6D900AA0062CE6C");
            d.Add(".wav", "52494646xxxxxxxx57415645666D7420");
            //Video'
            d.Add(".wmv", "3026B2758E66CF11A6D900AA0062CE6C");
            d.Add(".swf", "435753");
            d.Add(".avi", "52494646xxxxxxxx415649204C495354");
            //d.Add(".mp4", "000000186674797033677035");

            d.Add(".mp4", "0000001C667479706");
            //d.Add(".mp4", "000000146674797069736F6D");                  
            d.Add(".mpeg", "000001Bx");
            d.Add(".mpg", "000001Bx");
            //d.Add(".flv", "464C56010500000009000000001200010C");
            d.Add(".flv", "464C5601");
            d.Add(".mov", "0000001466747970717420200000020071742020");
            //d.Add(".qt", "video/quicktime");
            return d[FileExtension];
        }

        #endregion

        ///// <summary>
        ///// Creates the random password.
        ///// </summary>
        ///// <param name="PasswordLength">Length of the password.</param>
        ///// <returns></returns>
        //public static string CreateRandomPassword(int PasswordLength)
        //{
        //    string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
        //    Random randNum = new Random();
        //    char[] chars = new char[PasswordLength];
        //    int allowedCharCount = _allowedChars.Length;

        //    for (int i = 0; i < PasswordLength; i++)
        //    {
        //        chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        //    }

        //    return new string(chars);
        //}

        /// <summary>
        /// Gets the date time format info.
        /// </summary>
        /// <returns></returns>
        //public static DateTimeFormatInfo getDateTimeFormatInfo()
        //{
        //    DateTimeFormatInfo objDateTimeFormatInfo = new DateTimeFormatInfo();
        //    objDateTimeFormatInfo.ShortDatePattern = Convert.ToString("dd/MM/yyyy");
        //    return objDateTimeFormatInfo;
        //}

        /// <summary>
        /// Formats the specified format.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatDate(object arg)
        {
            if (!(arg is DateTime)) throw new NotSupportedException();
            var dt = (DateTime)arg;
            //string suffix;
            //if (dt.Day % 10 == 1)
            //    suffix = "<sup>st</sup>";
            //else if (dt.Day % 10 == 2)
            //    suffix = "<sup>nd</sup>";
            //else if (dt.Day % 10 == 3)
            //    suffix = "<sup>rd</sup>";
            //else
            //    suffix = "<sup>th</sup>";
            //return string.Format(dateFormat1, arg, suffix);
            return string.Format(dateFormat1, arg);
        }

        /// <summary>
        /// Formats the date time.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatDateTime(object arg)
        {
            if (!(arg is DateTime)) throw new NotSupportedException();
            var dt = (DateTime)arg;
            //string suffix;
            //if (dt.Day % 10 == 1)
            //    suffix = "<sup>st</sup>";
            //else if (dt.Day % 10 == 2)
            //    suffix = "<sup>nd</sup>";
            //else if (dt.Day % 10 == 3)
            //    suffix = "<sup>rd</sup>";
            //else
            //    suffix = "<sup>th</sup>";
            return string.Format(dateFormat2, arg);
        }

        private static string sLogFormat;
        private static string sErrorTime;

        public static void CreateLogFiles()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
        }

        public static void ErrorLog(string sPathName, Exception ex)
        {
            try
            {
                CreateLogFiles();
                string path = sPathName + sErrorTime + ".txt";
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(sLogFormat + ex.Message + "\t\r\n" + "TargetSite : " + ex.TargetSite + "\t\r\n" + "HelpLink: " + ex.HelpLink + "\t\r\n" + "StackTrace: " + ex.StackTrace + "\t\r\n\t\r\n");
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }


        public static string GetAppliedCardNameGlobally(string productCode)
        {
            if (productCode != null && productCode != "")
            {
                string cardname = string.Empty;
                productCode = productCode.ToUpper();
                if (productCode == Constants.Silvervisa)
                    cardname = Constants.P01;

                else if (productCode == Constants.Exclusivegeneralmaster)
                    cardname = Constants.P02;

                else if (productCode == Constants.Exclusiveyouthmaster)
                    cardname = Constants.P03;

                else if (productCode == Constants.Exclusivewomenmaster)
                    cardname = Constants.P04;

                else if (productCode == Constants.Goldvisa)
                    cardname = Constants.P05;

                else if (productCode == Constants.Goldinternationalvisa)
                    cardname = Constants.P06;

                else if (productCode == Constants.Goldmastercard)
                    cardname = Constants.P07;

                else if (productCode == Constants.Platinummaster)
                    cardname = Constants.P21;

                else if (productCode == Constants.Platinumvisa)
                    cardname = Constants.P22;

                else if (productCode == Constants.Elite)
                    cardname = Constants.P23;

                else if (productCode == Constants.Corporatepremium)
                    cardname = Constants.P24;

                else if (productCode == Constants.Platinumselect)
                    cardname = Constants.P25;

                else if (productCode == Constants.Visasignature)
                    cardname = Constants.P26;

                else if (productCode == Constants.Platinumbba)
                    cardname = Constants.P27;

                else if (productCode == Constants.Bobcarassure)
                    cardname = Constants.P28;

                else if (productCode == Constants.Titanium)
                    cardname = Constants.P29;

                else if (productCode == Constants.Xlri)
                    cardname = Constants.P30;

                else if (productCode == Constants.Bobcardpaytm)
                    cardname = Constants.P31;

                else if (productCode == Constants.Personalloan)
                    cardname = Constants.P32;

                return cardname;
            }
            else
            {
                return null;
            }

        }

        public static string GetAppliedCardNumberGlobally(string productNumber)
        {
            if (productNumber != null && productNumber != "")
            {
                string cardnumber = string.Empty;
                productNumber = productNumber.ToUpper();
                if (productNumber == Constants.P01)
                    cardnumber = Constants.Silvervisa;

                else if (productNumber == Constants.P02)
                    cardnumber = Constants.Exclusivegeneralmaster;

                else if (productNumber == Constants.P03)
                    cardnumber = Constants.Exclusiveyouthmaster;

                else if (productNumber == Constants.P04)
                    cardnumber = Constants.Exclusivewomenmaster;

                else if (productNumber == Constants.P05)
                    cardnumber = Constants.Goldvisa;

                else if (productNumber == Constants.P06)
                    cardnumber = Constants.Goldinternationalvisa;

                else if (productNumber == Constants.P07)
                    cardnumber = Constants.Goldmastercard;

                else if (productNumber == Constants.P21)
                    cardnumber = Constants.Platinummaster;

                else if (productNumber == Constants.P22)
                    cardnumber = Constants.Platinumvisa;

                else if (productNumber == Constants.P23)
                    cardnumber = Constants.Elite;

                else if (productNumber == Constants.P24)
                    cardnumber = Constants.Corporatepremium;

                else if (productNumber == Constants.P25)
                    cardnumber = Constants.Platinumselect;

                else if (productNumber == Constants.P26)
                    cardnumber = Constants.Visasignature;

                else if (productNumber == Constants.P27)
                    cardnumber = Constants.Platinumbba;

                else if (productNumber == Constants.P28)
                    cardnumber = Constants.Bobcarassure;

                else if (productNumber == Constants.P29)
                    cardnumber = Constants.Titanium;

                else if (productNumber == Constants.P30)
                    cardnumber = Constants.Xlri;

                else if (productNumber == Constants.P31)
                    cardnumber = Constants.Bobcardpaytm;

                else if (productNumber == Constants.P32)
                    cardnumber = Constants.Personalloan;

                return cardnumber;
            }
            else
            {
                return null;
            }

        }

        public static DateTimeFormatInfo getDateTimeFormatInfo()
        {
            DateTimeFormatInfo objDateTimeFormatInfo = new DateTimeFormatInfo();
            objDateTimeFormatInfo.ShortDatePattern = Convert.ToString("dd/MM/yyyy");
            return objDateTimeFormatInfo;
        }

        //public static string GenerateCode()
        //{
        //    RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        //    byte[] randBytes = new byte[32];
        //    random.GetNonZeroBytes(randBytes);
        //    return Convert.ToBase64String(randBytes);
        //}

    }
}
