using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System.Configuration;


namespace CardHolder
{
    public partial class ApplicationSuccess : System.Web.UI.Page
    {
        string qsk = "afq4dfrs";

        string queryString = "f={0}";
        protected void Page_Load(object sender, EventArgs e)
        {
            string Appnum = Convert.ToString(Session["AppHashNum"]);

            if (!string.IsNullOrEmpty(Appnum))
            {
                divbtn.Visible = true;
                successMsg.Text = "An email with given application number has been sent to your email id. Please save that for future reference";
                lblMessage.Text = Constants.AppSuccess + "Your application number is: " + Appnum.Decrypt();

                string fn = string.Format(queryString, Appnum.Decrypt());
                string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
                btnprintform.Attributes.Add("OnClick", "return DisplayApplication('" + urlQueryString + "');");
                Session["AppHashNum"] = null;

            }
            else
            {
                Response.Redirect("Login.aspx", true);
            }
        }

    }
}