using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Session;
using CardHolder.Utility;
using CardHolder.Utility.OTP;
namespace CardHolder.UserManagment
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class ChangePassword : PageBase
    {
        //string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
        /// <summary>
        /// 
        /// </summary>
        string OverRideMobile = ConfigurationManager.AppSettings["OverRideUserMobile"];
        string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf) { }
            else
            {
                if (!IsPostBack)
                {
                    btnProceed.Attributes.Add("onClick", "javascript:return ValidateThisForm(this.form);");
                    CardHolder_MstDTO user = CardHolderManager.GetLoggedInUser();
                    if (user != null)
                    {
                        Session.SaveUserDto(user);
                    }
                    hdnOTP.Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnProceed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmpConfirmPassowrd.IsValid && cmpNewPassord.IsValid && reqConfirmPassword.IsValid && reqNewPassword.IsValid && reqOldPassword.IsValid)
                {
                    string OldPwd = txtOldPassword.Text.Trim();
                    string dbPassword = Session.GetUserPassword();

                    //if (txtOldPassword.Text.Trim().Encrypt() == Session.GetUserPassword())
                    if (String.CompareOrdinal(OldPwd, dbPassword) == 0)
                    {
                        if (hdnOTP.Value == string.Empty)
                        {
                            OTPClient otp = new OTPClient();
                            string mobilenum = Session.GetMobileNum();
                            if (!string.IsNullOrEmpty(OverRideMobile))
                                mobilenum = OverRideMobile;
                            string code = otp.SendRequest(mobilenum, "", Constants.ModifyPwd, CardHolderManager.GetLoggedInUser().CardHolder_Id); // Temporary Commented will added in future
                            mvPasswordChange.ActiveViewIndex = 1;
                            StartOTPTimer();
                            hdnOTP.Value = code;
                            //txtOTP.Text = code;  //for testing remove later
                            string lastFourdgts = string.Empty;
                            if (mobilenum != "")
                            {
                                int numberkength = mobilenum.Length;
                                if (numberkength > 4)
                                    lastFourdgts = mobilenum.Substring(numberkength - 4, 4);
                                else
                                    lastFourdgts = mobilenum;
                                txtMobileNo.Text = mobilenum;
                                ViewState["Mobile_Num"] = mobilenum;
                            }

                            //mobilenum = mobilenum.Substring(6, 4);
                            //lblDescOTP.Text = Constants.OTPDescforchangePwd;
                            //lblmob.Text = "(i.e. XXXXXX" + lastFourdgts + ")";
                            //lbl3.Text = Constants.OTPDesc2;
                        }
                        ViewState["ModifyPassword"] = txtNewPassword.Text.Trim();
                    }
                    else
                    {
                        lblMessage.Text = Constants.IncorrectOldPwd;
                        DivMessage.Attributes.CssStyle.Add("display", "block");

                    }
                }
            }
            catch (Exception exp)
            {
                LblErrorMessage.Text = Constants.TechnicalError;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Profile.aspx");
            }
            catch (Exception)
            {
                LabelOTPErrorMessage.Text = Constants.GeneralErrorMessage;
                DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");
            }
        }
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOTP.Text == hdnOTP.Value.ToString() && ViewState["ModifyPassword"] != null)
                {
                    LabelOTPErrorMessage.Text = string.Empty;
                    DivOTPErrorMessage.Attributes.CssStyle.Add("display", "none");
                    //MsgStep2.Text = string.Empty;
                    LabelOTPMessage.Text = String.Empty;
                    DivOTPMessage.Attributes.CssStyle.Add("display", "none");
                    CardHolderManager um = new CardHolderManager();
                    CardHolder_MstDTO user = Session.GetUserDto();
                    user.User_pwd = ViewState["ModifyPassword"].ToString();
                    um.UpdateCardHolder(user);
                    LblSuccessMessage.Text = Constants.PwdSuccess;
                    DivSuccess.Attributes.CssStyle.Add("display", "block");
                    hdnOTP.Value = string.Empty;
                    //mvPasswordChange.ActiveViewIndex = 0;
                    SessionLogout();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                "setTimeout(function() { window.location='../Login.aspx' }, 5000);", true);
                }
                else
                {
                    StartOTPTimer();
                    LabelOTPMessage.Text = Constants.IncorrectOTP;
                    DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                }
            }
            catch (Exception)
            {
                LabelOTPErrorMessage.Text = Constants.GeneralErrorMessage;
                DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");
            }
        }

        protected void linkRegenerateOTP_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Mobile_Num"] != null)
                {
                    hdnOTP.Value = string.Empty;
                    string OtpSuccess = GenerateOTP();
                    if (OtpSuccess == "0" || OtpSuccess == "")
                    {
                        LabelOTPErrorMessage.Text = Constants.TechnicalError;
                        DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");
                    }
                    else
                    {
                        StartOTPTimer();
                    }
                }
            }
            catch (Exception ex)
            {
                LabelOTPErrorMessage.Text = Constants.TechnicalError;
                DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");
                string path = Server.MapPath("~/ErrorPage/ErrorLog");
                GeneralMethods.ErrorLog(path, ex);
            }
        }

        public void StartOTPTimer()
        {
            string remaining = "60";
            ClientScript.RegisterStartupScript(this.GetType(), "timer", "timer('" + remaining + "');", true);
        }

        /// <summary>
        /// Generates the OTP.
        /// </summary>
        /// <remarks></remarks>
        private string GenerateOTP()
        {
            string code = string.Empty;
            try
            {
                if (hdnOTP.Value == string.Empty)
                {
                    string MobileNum = ViewState["Mobile_Num"].ToString();
                    string EmailId = Convert.ToString(ViewState["Email_ID"]);
                    if (!string.IsNullOrEmpty(OverRideMobile))
                        MobileNum = OverRideMobile;
                    if (!string.IsNullOrEmpty(OverRideEmail))
                        EmailId = OverRideEmail;
                    txtOTP.Text = "";
                    OTPClient otp = new OTPClient();
                    long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                    code = otp.SendRequest(MobileNum, EmailId, Constants.ForgotPwd, CardHolderId);
                    hdnOTP.Value = code;
                }
            }
            catch (Exception ex)
            {
                LabelOTPErrorMessage.Text = Constants.TechnicalError;
                DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");
                string path = Server.MapPath("~/ErrorPage/ErrorLog");
                GeneralMethods.ErrorLog(path, ex);
                return "0";
            }
            return code;
        }

        private void SessionLogout()
        {

            CardHolder.Utility.CacheHelperBySession<CardHolder_MstDTO>.InvalidateCache();
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            ClearCache();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
            Response.Cookies.Add(new HttpCookie("STTLII", string.Empty));
            //Response.Redirect("~/Login.aspx");           
        }

        public static void ClearCache()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.Cookies.Clear();
            HttpContext.Current.Request.Cookies.Clear();
        }
    }
}