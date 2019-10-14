using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;
using CardHolderOracle.DAL;
using System.Net.Security;
using CardHolder.DTO;
using CardHolder.DAL;
using CardHolder.DAL.Interface;
using StructureMap;
using CardHolder.Utility;
namespace CardHolder.Utility.OTP
{
    public class OTPClient
    {
        public IRepository<CH_SMSLogger> repCH_SMSLogger
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_SMSLogger>>();
            }
        }
        public string SendRequest(string mobile, string email, string type, long UserId)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            string code = BitConverter.ToUInt32(buffer, 1).ToString();
            if (mobile != "")
                SendSMS(mobile.Trim(), code.Trim(), UserId, type);
            if (email != "")
                SendEmail(email.Trim(), code.Trim(), UserId);
            return code;
        }
        public string SendOTPRequest(string mobile, string email, string type, long UserId)
        {
            
            //byte[] buffer = new byte[2];
            //buffer = Guid.NewGuid().ToByteArray();
            //uint random = BitConverter.ToUInt32(buffer, 0) % 100000000;
            //string code = String.Format("{0:D4}", random);
            //if (code.Length > 7)
               // ;
            byte[] buffer = Guid.NewGuid().ToByteArray();
            string code = BitConverter.ToUInt32(buffer, 1).ToString();
            code = code.Substring(0, 6);
            if (mobile != "")
                SendSMS(mobile.Trim(), code.Trim(), UserId, type);
            if (email != "")
                SendEmail(email.Trim(), code.Trim(), UserId);
            return code;
        }

        private void SendEmail(string email, string code, long UserId)
        {
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string SUBJECT = ConfigurationManager.AppSettings["OTP_EMAIL_SUBJECT"].ToString();
            //Step 4 Send Email to registered user
            try
            {
                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("") + "\\MailTemplates\\OTPEmail.htm"));
                bodyString.Replace("@@OTP", code);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                SendMailfunction.SendMail(BOBMail, new List<string>() { email }, CCemail, "", "", SUBJECT, bodyString.ToString(), true, UserId, null);

            }
            catch (Exception ex)
            {
               // throw;
            }
        }
        private void SendSMS(string mobile, string code, long UserId, string Category)
        {
            Stream Answer = null;
            StreamReader _Answer = null;
            SMS_LoggerDTO objSMS_LoggerDTO = new SMS_LoggerDTO();
            try
            {
                objSMS_LoggerDTO.Number = mobile;
                objSMS_LoggerDTO.SendBy = UserId;
                objSMS_LoggerDTO.SendDate = DateTime.Now;
                objSMS_LoggerDTO.Category = Category;

                string OTPURL = ConfigurationManager.AppSettings["OTPURL"];
                string FEEDID = ConfigurationManager.AppSettings["FEEDID"];
                string USERNAME = ConfigurationManager.AppSettings["USERNAME"];
                string PASSWORD = ConfigurationManager.AppSettings["PASSWORD"];
                if (string.IsNullOrEmpty(OTPURL) || string.IsNullOrEmpty(FEEDID) || string.IsNullOrEmpty(USERNAME) || string.IsNullOrEmpty(PASSWORD))
                {
                    objSMS_LoggerDTO.SMSStatus = "Due to some reason unable to send OTP";
                    return;
                }
                string message = string.Format(ConfigurationManager.AppSettings["MESSAGE"].Decrypt(), code);
                string url = string.Format("{0}?feedid={1}&username={2}&password={3}&To={4}&Text={5}&time={6}", OTPURL.Decrypt(), FEEDID.Decrypt(), USERNAME.Decrypt(), PASSWORD.Decrypt(), mobile, message, DateTime.Now.ToString("yyyyMMddHHmm"));
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(url));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                request.AllowAutoRedirect = true;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                if (response != null)
                {
                    Answer = response.GetResponseStream();
                    _Answer = new StreamReader(Answer);
                    string str_response = _Answer.ReadToEnd();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        objSMS_LoggerDTO.SMSStatus = "Delivered";
                    }
                    else
                    {
                        objSMS_LoggerDTO.SMSStatus = "SMS Fail ERROR:" + Convert.ToString(response.StatusCode);
                    }
                }
                else
                {
                    objSMS_LoggerDTO.SMSStatus = "Due to some technical issue unable to send OTP";
                    //return "Due to some reason unable to send OTP";
                }

                //SaveSMSLog(objSMS_LoggerDTO);
            }
            catch (Exception exp)
            {
                common.logger.Debug("SMS Fail ERROR:" + exp.Message.ToString());
               // return "SMS Fail ERROR:" + exp.Message;
                objSMS_LoggerDTO.SMSStatus = "SMS Fail ERROR:" + exp.Message;
                throw;
            }
            finally
            {
                SaveSMSLog(objSMS_LoggerDTO);
                Answer.Close();
                _Answer.Close();
            }
        }
        public void SaveSMSLog(SMS_LoggerDTO objSMS_LoggerDTO)
        {
            CH_SMSLogger objSMSLogger = new CH_SMSLogger();
            objSMSLogger.Number = objSMS_LoggerDTO.Number;
            objSMSLogger.SendBy = objSMS_LoggerDTO.SendBy;
            objSMSLogger.SendDate = objSMS_LoggerDTO.SendDate;
            objSMSLogger.Category = objSMS_LoggerDTO.Category;
            objSMSLogger.SMSStatus = objSMS_LoggerDTO.SMSStatus;
            repCH_SMSLogger.Add(objSMSLogger);
            GeneralManagerUtility.Commit();

        }
    }
}
