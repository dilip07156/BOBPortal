using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using CardHolder.BAL;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class Global : System.Web.HttpApplication
    {

        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Application_Start(object sender, EventArgs e)
        {
            GeneralManager.Initialize();
        }

        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Session_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Application_s the pre send request headers.
        /// </summary>
        /// <remarks></remarks>
        protected void Application_PreSendRequestHeaders()
        {
            string GlobalKey = ConfigurationManager.AppSettings["GlobalKey"];

            if (!string.IsNullOrEmpty(GlobalKey) && GlobalKey == "1")
            {
                Response.Headers.Remove("Server");
                Response.Headers.Remove("X-Powered-By");
                Response.Headers.Remove("X-AspNet-Version");
                Response.Headers.Remove("X-AspNetMvc-Version");
                Response.AddHeader("Strict-Transport-Security", "max-age=300");
                Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
            }
        }


        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
            }


            //if (!Request.IsSecureConnection)
            //{
            //    UriBuilder uri = new UriBuilder(Request.Url);
            //    uri.Scheme = Uri.UriSchemeHttps;
            //    uri.Port = 443;
            //    Response.Redirect(uri.ToString());
            //}
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Application_Error(object sender, EventArgs e)
        {
            string ExceptionEmail = ConfigurationManager.AppSettings["ExceptionEmail"];
            Exception ex = Server.GetLastError();
            List<string> CCemail = new List<string>();
            if (!string.IsNullOrEmpty(ExceptionEmail))
            {
                SendMailfunction.SendMail("exceptions@bobcards.com", new List<string>() { ExceptionEmail }, CCemail, "", "", "InnerErrorFromLiveCH", Convert.ToString(ex.InnerException), true, 0, null);
                SendMailfunction.SendMail("exceptions@bobcards.com", new List<string>() { ExceptionEmail }, CCemail, "", "", "MessageFromLiveCH", Convert.ToString(ex.Message), true, 0, null);
            }
        }

        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Application_End(object sender, EventArgs e)
        {
            GeneralManager.Dispose();
        }
    }
}