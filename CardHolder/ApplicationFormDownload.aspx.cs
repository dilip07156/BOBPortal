using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.Utility;

namespace CardHolder
{
    public partial class ApplicationFormDownload : System.Web.UI.Page
    {
        string qsk = "afq4dfrs";

        string queryString = "f={0}";

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Handles the Click event of the btnEnter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (IsValidInfo())
            {
                string Appnum = txtAppNumber.Text.Trim();

                if (!string.IsNullOrEmpty(Appnum))
                {

                    string fn = string.Format(queryString, Appnum);
                    string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);

                    ScriptManager.RegisterStartupScript(this, GetType(), "DisplayApp", "DisplayApplication('" + urlQueryString + "');", true);

                    //btnEnter.Attributes.Add("OnClick", "return DisplayApplication('" + urlQueryString + "');");
                }
                else
                {
                    lblerror.Text = "Please enter application number";
                }
            }
            else
            {
                lblerror.Text = Constants.InvalidCaptcha;
                txtCaptchaFirst.Text = string.Empty;
            }
        }

        /// <summary>
        /// Determines whether [is valid info].
        /// </summary>
        /// <returns><c>true</c> if [is valid info]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        private bool IsValidInfo()
        {
            if (!Page.IsValid)
                return false;

            try
            {
                string captchaFrst = txtCaptchaFirst.Text.Trim();
                if (!string.IsNullOrEmpty(captchaFrst) && captchaFrst.Length == 6)
                {
                    //CaptchaFirst.ValidateCaptcha(captchaFrst);

                    if (captchaFrst != "" && Convert.ToString(Session["strRandomCH"]) == captchaFrst)
                        return true;
                    else
                    {
                        txtCaptchaFirst.Text = string.Empty;
                        lblerror.Text = Constants.InvalidCaptcha;
                        return false;
                    }
                }
                else
                {
                    lblerror.Text = Constants.InvalidCaptcha;
                    txtCaptchaFirst.Text = string.Empty;
                    txtCaptchaFirst.Focus();

                    return false;
                }

                //if (CaptchaFirst.UserValidated)
                //    return true;
                //lblerror.Text = Constants.InvalidCaptcha;
                //txtCaptchaFirst.Text = string.Empty;
                //txtCaptchaFirst.Focus();
                //return false;
            }
            catch (Exception)
            {
                lblerror.Text = Constants.InvalidCaptcha;
                txtCaptchaFirst.Text = string.Empty;
                txtCaptchaFirst.Focus();
                return false;
            }
        }
    }
}