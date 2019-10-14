using System;
using System.Web;
using System.Web.Security;

namespace CardHolder.ErrorPage
{
    public partial class CodeError : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
            
        }

        /// <summary>
        /// Handles the Click event of the lnklogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void lnklogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}