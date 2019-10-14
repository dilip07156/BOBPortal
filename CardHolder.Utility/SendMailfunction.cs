using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using CardHolder.DAL;
using CardHolder.DAL.Interface;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolderOracle.DAL;
using StructureMap;
/// <summary>
/// Summary description for SendMail
/// </summary>
/// <remarks></remarks>
public class SendMailfunction
{
   // private static Logger _logger = LogManager.GetCurrentClassLogger();
    /// <summary>
    /// 
    /// </summary>
    private static string DisplayName = ConfigurationManager.AppSettings["BOB_EMAIL_Name"];

    /// <summary>
    /// Gets the rep email logger.
    /// </summary>
    /// <remarks></remarks>
    public IRepository<CH_EmailLogger> repEmailLogger
    {
        get
        {
            return ObjectFactory.GetInstance<IRepository<CH_EmailLogger>>();
        }
    }
    /// <summary>
    /// Sends the mail.
    /// </summary>
    /// <param name="FromEmailID">From email ID.</param>
    /// <param name="ToEmailID">To email ID.</param>
    /// <param name="CCEmailID">The CC email ID.</param>
    /// <param name="BCCEmailID">The BCC email ID.</param>
    /// <param name="NotificationMailID">The notification mail ID.</param>
    /// <param name="Subject">The subject.</param>
    /// <param name="Body">The body.</param>
    /// <param name="IsBodyHtml">if set to <c>true</c> [is body HTML].</param>
    /// <param name="userId">The user id.</param>
    /// <param name="_Attachments">The _ attachments.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool SendMail(string FromEmailID, List<string> ToEmailID, List<string> CCEmailID, string BCCEmailID, string NotificationMailID, string Subject, string Body, Boolean IsBodyHtml, long userId, List<AttachmentDTO> _Attachments = null)
    {
        MailMessage mm;
        SmtpClient smtp;
        Email_LoggerDTO objEmail_LoggerDTO = new Email_LoggerDTO();
        SendMailfunction objSendMailfunction = new SendMailfunction();
        try
        {
            objEmail_LoggerDTO.ToEmail = Convert.ToString(ToEmailID[0]);
            objEmail_LoggerDTO.FromEmail = FromEmailID;
            objEmail_LoggerDTO.Subject = Subject;
            objEmail_LoggerDTO.Body = Body;
            objEmail_LoggerDTO.SendBy = userId;
            objEmail_LoggerDTO.SendDate = DateTime.Now;

            using (MemoryStream memoryStream = new MemoryStream())
            {

                mm = new MailMessage();
                if (!string.IsNullOrEmpty(DisplayName))
                    mm.From = new MailAddress(FromEmailID, DisplayName);
                else
                    mm.From = new MailAddress(FromEmailID);

                foreach (string toemail in ToEmailID)
                {
                    mm.To.Add(new MailAddress(toemail));
                }

                mm.IsBodyHtml = IsBodyHtml;
                foreach (string ccemail in CCEmailID)
                {
                    mm.CC.Add(new MailAddress(ccemail));
                }
                if (!string.IsNullOrEmpty(BCCEmailID)) mm.Bcc.Add(BCCEmailID);

                mm.Subject = Subject;
                mm.Body = Body;

                if (NotificationMailID != "")
                    mm.Headers.Add("Disposition-Notification-To", NotificationMailID);//ConfigurationManager.AppSettings["NotificationEmail"].ToString());

                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                // Create a memory stream

                Attachment attach = null;
                if (_Attachments != null)
                {
                    for (int i = 0; i < _Attachments.Count; i++)
                    {
                        memoryStream.Write(_Attachments[i].contentAsBytes, 0, _Attachments[i].contentAsBytes.Length);

                        // Set the position to the beginning of the stream.
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        // Create attachment
                        ContentType contentType = new ContentType();
                        contentType.MediaType = MediaTypeNames.Application.Octet;
                        contentType.Name = _Attachments[i].fileName;
                        Attachment attachment = new Attachment(memoryStream, contentType);

                        // Add the attachment
                        mm.Attachments.Add(attachment);

                    }
                }

                //smtp = new SmtpClient(Host, Convert.ToInt32(Port));
                //smtp.UseDefaultCredentials = false;
                //System.Net.NetworkCredential cr = new NetworkCredential(UserName, Password);
                //smtp.Credentials = cr;

                //Code Added on 07/Mar/14
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                //string username = smtpSection.Network.UserName.Decrypt();
                //string Password = smtpSection.Network.Password.Decrypt();
                string host = smtpSection.Network.Host.Decrypt();
                int port = smtpSection.Network.Port;

                smtp = new SmtpClient(host, Convert.ToInt32(port));
                smtp.UseDefaultCredentials = false;
                //System.Net.NetworkCredential cr = new NetworkCredential(username, Password);
                System.Net.NetworkCredential cr = new NetworkCredential();
                smtp.Credentials = cr;

                //smtp = new SmtpClient();
                smtp.Send(mm);

                objEmail_LoggerDTO.EmailStatus = Constants.emailsent;

                // logger.Info(" FromEmailID: " + FromEmailID + " |--ToEmailID: " + ToEmailID[0].ToString() + " |--Subject " + Subject + " |--DateTime: " + System.DateTime.Now);
                if (attach != null)
                    attach.Dispose();
                return true;
            }

        }
        catch (Exception ex)
        // catch
        {
            common.logger.Debug("Mail SendMail Fail ERROR:" + ex.Message);
            common.logger.Debug("Error SendMail StackTrace:" + ex.StackTrace + "<br/><br/>");
            common.logger.Debug("Error SendMail InnerException :" + Convert.ToString(ex.InnerException) + "<br/>");
            objEmail_LoggerDTO.EmailStatus = Constants.emailfail;
            throw;
          //  return false;
        }
        finally
        {
            objSendMailfunction.SaveEmailLog(objEmail_LoggerDTO);
        }
    }

    /// <summary>
    /// Saves the email log.
    /// </summary>
    /// <param name="objEmail_LoggerDTO">The obj email_ logger DTO.</param>
    /// <remarks></remarks>
    public void SaveEmailLog(Email_LoggerDTO objEmail_LoggerDTO)
    {
        CH_EmailLogger objemail = new CH_EmailLogger();
        objemail.FromEmail = objEmail_LoggerDTO.FromEmail;
        objemail.ToEmail = objEmail_LoggerDTO.ToEmail;
        objemail.SendBy = objEmail_LoggerDTO.SendBy;
        objemail.SendDate = objEmail_LoggerDTO.SendDate;
        objemail.Subject = objEmail_LoggerDTO.Subject;
        objemail.Body = objEmail_LoggerDTO.Body;
        objemail.EmailStatus = objEmail_LoggerDTO.EmailStatus;
        repEmailLogger.Add(objemail);
        GeneralManagerUtility.Commit();

    }

}

/// <summary>
/// 
/// </summary>
/// <remarks></remarks>
public class AttachmentDTO
{
    /// <summary>
    /// Gets or sets the content as bytes.
    /// </summary>
    /// <value>The content as bytes.</value>
    /// <remarks></remarks>
    public byte[] contentAsBytes { get; set; }

    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>The name of the file.</value>
    /// <remarks></remarks>
    public string fileName { get; set; }
}