using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.OTP;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Net;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class ForgotUsername : System.Web.UI.Page
    {
        readonly string _expiryYear = ConfigurationManager.AppSettings["ExpiryYear"];
        /// <summary>
        /// 
        /// </summary>
        string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
        /// <summary>
        /// 
        /// </summary>
        string OverRideMobile = ConfigurationManager.AppSettings["OverRideUserMobile"];

        protected static string ReCaptcha_Key = Convert.ToString(ConfigurationManager.AppSettings["ReCaptcha_Key"]);
        protected static string ReCaptcha_Secret = Convert.ToString(ConfigurationManager.AppSettings["ReCaptcha_Secret"]);
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {               
                mvFrgtUname.ActiveViewIndex = 0;
                MonthYear();
                txtMobileNo.Attributes.Add("readonly", "readonly");

            }

        }

        #region ButtonEvents

        /// <summary>
        /// Handles the Click event of the btnSubmitfinal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmitfinal_Click(object sender, EventArgs e)
        {
            //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT issue
            if (Session["OTP_Frg_User"] != null && Session["OTP_Frg_User"].ToString() != "")
            {
                int cnt = 0;
                if (Int32.TryParse(Session["OTP_Frg_User"].ToString(), out cnt) && cnt >= 3)
                {
                    LabelOTPMessage.Text = Constants.MaxNoOfOTPMessage;
                    DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }
            }
            //End Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT issue
            if (!string.IsNullOrEmpty(FirstFour.Text) && !string.IsNullOrEmpty(SecondFour.Text) && !string.IsNullOrEmpty(ThirdFour.Text) && !string.IsNullOrEmpty(ForthFour.Text))
            {
                hdnCard1.Value = FirstFour.Text.Encrypt();
                hdnCard2.Value = SecondFour.Text.Encrypt();
                hdnCard3.Value = ThirdFour.Text.Encrypt();
                hdnCard4.Value = ForthFour.Text.Encrypt();
                FirstFour.Text = "xxxx";
                SecondFour.Text = "xxxx";
                ThirdFour.Text = "xxxx";
                ForthFour.Text = "xxxx";
            }

            CardHolder_MstDTO user = new CardHolder_MstDTO();
            if (IsValidInfo())
            {
                string fullCardnumber = (FirstFour.Text + SecondFour.Text + ThirdFour.Text + ForthFour.Text).Trim();
                //string DateOfBirth = DateTime.ParseExact(txtbirthdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                string DateOfBirth = Convert.ToString(GetDateTime(txtbirthdate.Text));
                int ExpiryMonth = Convert.ToInt32(ddlmonth.SelectedItem.Text);
                int ExpiryYear = Convert.ToInt32(ddlyear.SelectedItem.Text);
                // Step 1 Find CARD In Oracle Database            
                CardManager cm = new CardManager();
                CH_CardDTO card =
                    cm.AuthenticateCrNumberDOB(new CH_CardDTO()
                        {
                            card_number = fullCardnumber,
                            EXPIRY_MONTH = ExpiryMonth,
                            EXPIRY_YEAR = ExpiryYear,
                            BIRTH_DATE = Convert.ToDateTime(DateOfBirth)
                        });


                // Step 2 Find CARD In SQL Database Either exists or not
                // 

                CardHolderManager Cardholder = new CardHolderManager();

                if (card != null)
                    user = Cardholder.FindUserByCrNumber(card.Cr_Account_Nbr.Encrypt());
                else
                {
                    lblMessage.Text = Constants.InvalidEntries;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    txtCaptchaFirst.Text = string.Empty;
                    mvFrgtUname.ActiveViewIndex = 0;
                    return;
                }


                //CardHolder_MstDTO user = Cardholder.FindUserByCrNumber(fullCardnumber.Encrypt());

                 if (card != null && user != null)
                {
                    string mobilenum = "";

                    ViewState["Mobile_Num"] = card.PHONE_MOBILE;
                    if (ViewState["Mobile_Num"] != null)
                        mobilenum = ViewState["Mobile_Num"].ToString();
                    //Session["Card_Num"] = fullCardnumber.Encrypt();
                    Session["Acc_Num"] = card.Cr_Account_Nbr.Encrypt();
                    ViewState["UserName"] = user.User_nm;
                    ViewState["CardHolder_name"] = card.FULL_NAME;
                    ViewState["Email_ID"] = card.EMAIL_ID;

                    string lastFourdgts = string.Empty;
                    if (mobilenum != "")
                    {
                        int numberkength = mobilenum.Length;
                        if (numberkength > 4)
                            lastFourdgts = mobilenum.Substring(numberkength - 4, 4);
                        else
                            lastFourdgts = mobilenum;
                        txtMobileNo.Text = mobilenum;
                    }

                    //mobilenum = mobilenum.Substring(6, 4);
                    //lblDescOTP.Text = Constants.OTPDescforchangePwd;
                    //lblmob.Text = "(i.e. XXXXXX" + lastFourdgts + ")";
                    //lbl3.Text = Constants.OTPDesc2;
                    string OtpSuccess = GenerateOTP();
                    if (OtpSuccess != "0" && !string.IsNullOrEmpty(OtpSuccess))
                    {
                        mvFrgtUname.ActiveViewIndex = 1;
                        StartOTPTimer();
                        lblMessage.Text = string.Empty;
                        DivMessage.Attributes.CssStyle.Add("display", "none");
                    }
                    else
                    {
                        ClearControls();
                        LblErrorMessage.Text = Constants.TechnicalError;
                        DivERROR.Attributes.CssStyle.Add("display", "block");
                        return;
                    }
                }
                else if (card != null && user == null)
                {
                    lblMessage.Text = Constants.NotRegister;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    mvFrgtUname.ActiveViewIndex = 0;
                }

                else
                {
                    lblMessage.Text = Constants.InvalidEntries;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    mvFrgtUname.ActiveViewIndex = 0;
                }
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

            if (FirstFour.Text.Trim() != string.Empty && SecondFour.Text.Trim() != string.Empty &&
               ThirdFour.Text.Trim() != string.Empty && ForthFour.Text.Trim() != string.Empty)
            {

                FirstFour.Text = hdnCard1.Value.Decrypt();
                SecondFour.Text = hdnCard2.Value.Decrypt();
                ThirdFour.Text = hdnCard3.Value.Decrypt();
                ForthFour.Text = hdnCard4.Value.Decrypt();
            }
            else
            {
                lblMessage.Text = Constants.DataNotFoundindb;
                DivMessage.Attributes.CssStyle.Add("display", "block");
                return false;
            }

            try
            {
                //string captchaFrst = txtCaptchaFirst.Text.Trim();


                //if (!string.IsNullOrEmpty(captchaFrst) && captchaFrst.Length == 6)
                //{
                //    //CaptchaFirst.ValidateCaptcha(captchaFrst);
                //    if (captchaFrst != "" && Convert.ToString(Session["strRandomCH"]) == captchaFrst)
                        return true;
                //    else
                //    {
                //        lblMessage.Text = Constants.InvalidCaptcha;
                //        DivMessage.Attributes.CssStyle.Add("display", "block");
                //        txtCaptchaFirst.Text = string.Empty;
                //        txtCaptchaFirst.Focus();
                //        mvFrgtUname.ActiveViewIndex = 0;
                //        return false;
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = Constants.InvalidCaptcha;
                //    DivMessage.Attributes.CssStyle.Add("display", "block");
                //    txtCaptchaFirst.Text = string.Empty;
                //    txtCaptchaFirst.Focus();
                //    mvFrgtUname.ActiveViewIndex = 0;
                //    return false;
                //}

                //if (CaptchaFirst.UserValidated)
                //    return true;
                //lblStep1Message.Text = Constants.InvalidCaptcha;
                //txtCaptchaFirst.Text = string.Empty;
                //txtCaptchaFirst.Focus();
                //mvFrgtUname.ActiveViewIndex = 0;
                //return false;
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.InvalidCaptcha;
                DivMessage.Attributes.CssStyle.Add("display", "block");
                txtCaptchaFirst.Text = string.Empty;
                txtCaptchaFirst.Focus();
                return false;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOTPContinue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnOTPContinue_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (txtOTP.Text == hdnOTP.Value.ToString() && Session["Acc_Num"] != null)
                    {
                        Mailfunction();
                    }
                    else
                    {
                        LabelOTPMessage.Text = Constants.IncorrectOTP;
                        DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                        txtOTP.Focus();
                        StartOTPTimer();
                        mvFrgtUname.ActiveViewIndex = 1;
                        return;
                    }
                }
                catch (Exception exp)
                {
                    LabelOTPErrorMessage.Text = Constants.TechnicalError;
                    DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the btnReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }


        /// <summary>
        /// Regenerate OTP
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void linkRegenerateOTP_Click(object sender, EventArgs e)
        {
            //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session with 20 sec wait time VAPT issue

            if (Session["OTP_Frg_User"] != null && Session["OTP_Frg_User"].ToString() != "")
            {
                int cnt = 0;
                if (Int32.TryParse(Session["OTP_Frg_User"].ToString(), out cnt) && cnt >= 3)
                {
                    LabelOTPMessage.Text = Constants.MaxNoOfOTPMessage;
                    DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }
            }
            if (ViewState["LastOTPSent"] != null && ViewState["LastOTPSent"].ToString() != "")
            {
                DateTime d;
                if (DateTime.TryParse(ViewState["LastOTPSent"].ToString(), out d))
                {
                    TimeSpan difference = DateTime.Now.Subtract(d);
                    if (difference.TotalSeconds < 20)
                    {
                        LabelOTPMessage.Text = Constants.MaxNoOfOTPMessageForTime;
                        DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }
                    else
                        LabelOTPMessage.Text = "";
                }
            }
            //End Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT issue

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
                    //divremaining.Visible = true;
                    StartOTPTimer();
                }
            }
        }

        #endregion

        #region Privatefunction

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private DateTime GetDateTime(string selectedDate)
        {
            int day = DateTime.Today.Date.Day;
            int month = DateTime.Today.Date.Month;
            int year = DateTime.Today.Date.Year;
            string[] dateFormat = selectedDate.Split('/');
            if (dateFormat.Length > 0)
            {
                day = Convert.ToInt32(dateFormat[0]);
                month = Convert.ToInt32(dateFormat[1]);
                year = Convert.ToInt32(dateFormat[2]);
            }
            DateTime returnDate = new DateTime(year, month, day);
            return returnDate;
        }

        /// <summary>
        /// Mailfunctions this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Mailfunction()
        {
            string Email = "";
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string SUBJECT = ConfigurationManager.AppSettings["ForgotUnm_EMAIL_SUBJECT"].ToString();
            long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            if (ViewState["Email_ID"] != null)
                Email = ViewState["Email_ID"].ToString();

            if (!string.IsNullOrEmpty(OverRideEmail))
                Email = OverRideEmail;
            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("") + Constants.ForgotUsernameTemplatepath));
                bodyString.Replace("@@UserName", ViewState["UserName"].ToString());
                bodyString.Replace("@@CardHolderName", ViewState["CardHolder_name"].ToString());
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", SUBJECT, bodyString.ToString(), true, CardHolderId, null);

                if (IsMailSent)
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.GeneralRequestError + "');", true);


                ClearControls();
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.GeneralRequestError + "');", true);
                ClearControls();
                mvFrgtUname.ActiveViewIndex = 0;
            }
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
                string MobileNum = "";
                if (hdnOTP.Value == string.Empty)
                {
                    if (ViewState["Mobile_Num"] != null)
                        MobileNum = ViewState["Mobile_Num"].ToString();
                    string EmailId = Convert.ToString(ViewState["Email_ID"]);
                    if (!string.IsNullOrEmpty(OverRideMobile))
                        MobileNum = OverRideMobile;
                    if (!string.IsNullOrEmpty(OverRideEmail))
                        EmailId = OverRideEmail;
                    txtOTP.Text = "";
                    OTPClient otp = new OTPClient();
                    long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                    code = otp.SendRequest(MobileNum, EmailId, Constants.ForgotUName, CardHolderId);
                    hdnOTP.Value = code;
                    //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session, VAPT issue
                    if (Session["OTP_Frg_User"] != null && Session["OTP_Frg_User"].ToString() != "")
                    {
                        int cnt = 0;
                        if (Int32.TryParse(Session["OTP_Frg_User"].ToString(), out cnt))
                        {
                            Session["OTP_Frg_User"] = cnt + 1;
                        }
                    }
                    else
                        Session["OTP_Frg_User"] = "1";
                    ViewState["LastOTPSent"] = DateTime.Now;
                    //End Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT, issue

                }
            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.TechnicalError;
                DivERROR.Attributes.CssStyle.Add("display", "block");
                return "0";
            }
            return code;
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        /// <remarks></remarks>
        private void ClearControls()
        {
            Session.Abandon();
            Session.Clear();
            txtOTP.Text = string.Empty;
            FirstFour.Text = string.Empty;
            SecondFour.Text = string.Empty;
            ThirdFour.Text = string.Empty;
            ForthFour.Text = string.Empty;
            ddlmonth.SelectedValue = "-1";
            ddlyear.SelectedValue = "-1";
            txtbirthdate.Text = string.Empty;
            txtCaptchaFirst.Text = string.Empty;
        }

        /// <summary>
        /// Monthes the year.
        /// </summary>
        /// <remarks></remarks>
        private void MonthYear()
        {
            List<string> Next10Yrs = new List<string>();
            int currentYear = DateTime.Now.Year;
            int expiryYear = Convert.ToInt32(_expiryYear);
            for (int i = currentYear; i <= currentYear + expiryYear; i++)
            {
                string j = Convert.ToString(i);
                //j = j.Substring(2, 2);
                Next10Yrs.Add(j);
            }
            //databind here

            ddlyear.DataSource = Next10Yrs;
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("YY", "-1"));
        }

        public void StartOTPTimer()
        {
            string remaining = "60";
            ClientScript.RegisterStartupScript(this.GetType(), "timer", "timer('" + remaining + "');", true);
        }

        /// <summary>
        /// Does the method enc.
        /// </summary>
        /// <param name="val1">The val1.</param>
        /// <param name="val2">The val2.</param>
        /// <param name="val3">The val3.</param>
        /// <param name="val4">The val4.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string DoMethodEnc(string val1, string val2, string val3, string val4)
        {
            string cards = val1.Encrypt() + "," + val2.Encrypt() + "," + val3.Encrypt() + "," + val4.Encrypt();
            return cards;
        }

        #region google Recpatcha
        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }
        #endregion
        #endregion
    }
}