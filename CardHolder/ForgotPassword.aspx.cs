using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        readonly string _expiryYear = ConfigurationManager.AppSettings["ExpiryYear"];
        /// <summary>
        /// 
        /// </summary>
        string OverRideMobile = ConfigurationManager.AppSettings["OverRideUserMobile"];
        string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
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
            lblStep1Message.Text = "";
            LblStep1ErrorMessage.Text = "";
            DivStep1Message.Attributes.CssStyle.Add("display", "none");
            DivStep1ERROR.Attributes.CssStyle.Add("display", "none");

            if (!IsPostBack)
            {
                btnchPwd.Attributes.Add("onClick", "javascript:return ValidateThisForm(this.form);");
                mvFrgtPwd.ActiveViewIndex = 0;
                MonthYear();
                txtMobileNo.Attributes.Add("readonly", "readonly");
            }

        }


        #region ButtonEvents

        #region FirstView
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

            bool IsUserExists = false;
            try
            {
                if (IsValidInfo())
                {
                    string fullCardnumber = (FirstFour.Text + SecondFour.Text + ThirdFour.Text + ForthFour.Text).Trim();
                    string DateOfBirth = Convert.ToString(GetDateTime(txtbirthdate.Text.Trim()));
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
                    // CardHolder_MstDTO user = Cardholder.FindUserByCrNumber(fullCardnumber.Encrypt()); Updated by Sahil on 29-jan-2015 as client said to use Acc_num instead of card_num
                    if (card != null)
                        IsUserExists = Cardholder.FindUserByAccountNumber(card.Cr_Account_Nbr.Encrypt());
                    else
                    {
                        lblStep1Message.Text = Constants.InvalidEntries;
                        DivStep1Message.Attributes.CssStyle.Add("display", "block");
                        txtCaptchaFirst.Text = string.Empty;
                        mvFrgtPwd.ActiveViewIndex = 0;
                        return;
                    }

                    // if (card != null && user != null)
                    if (card != null && IsUserExists)
                    {

                        ViewState["Mobile_Num"] = card.PHONE_MOBILE;
                        string mobilenum = card.PHONE_MOBILE;
                        //Session["Card_Num"] = fullCardnumber.Encrypt();
                        Session["Acc_Num"] = card.Cr_Account_Nbr.Encrypt();
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
                            mvFrgtPwd.ActiveViewIndex = 1;
                            StartOTPTimer();
                        }
                        else
                        {
                            ClearControls();
                            LblStep1ErrorMessage.Text = Constants.TechnicalError;
                            DivStep1ERROR.Attributes.CssStyle.Add("display", "block");
                            return;
                        }
                    }
                    else// (card != null && user == null)
                    {
                        lblStep1Message.Text = Constants.NotRegister;
                        DivStep1Message.Attributes.CssStyle.Add("display", "block");
                        txtCaptchaFirst.Text = string.Empty;
                        mvFrgtPwd.ActiveViewIndex = 0;
                    }

                    //else
                    //{
                    //    lblStep1Message.Text = Constants.InvalidEntries;
                    //    mvFrgtPwd.ActiveViewIndex = 0;
                    //}
                }
            }
            catch (Exception ex)
            {
                LblStep1ErrorMessage.Text = Constants.TechnicalError;
                DivStep1ERROR.Attributes.CssStyle.Add("display", "block");
                string path = Server.MapPath("~/ErrorPage/ErrorLog");
                GeneralMethods.ErrorLog(path, ex);
                txtCaptchaFirst.Text = string.Empty;
                mvFrgtPwd.ActiveViewIndex = 0;
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
                lblStep1Message.Text = Constants.DataNotFoundindb;
                DivStep1Message.Attributes.CssStyle.Add("display", "block");
                return false;
            }

            try
            {
                //string captchaFrst = txtCaptchaFirst.Text.Trim();


                //if (!string.IsNullOrEmpty(captchaFrst) && captchaFrst.Length == 6)
                //{
                //    //   CaptchaFirst.ValidateCaptcha(captchaFrst);
                //    if (captchaFrst != "" && Convert.ToString(Session["strRandomCH"]) == captchaFrst)
                        return true;
                //    else
                //    {
                //        lblStep1Message.Text = Constants.InvalidCaptcha;
                //        DivStep1Message.Attributes.CssStyle.Add("display", "block");
                //        txtCaptchaFirst.Text = string.Empty;
                //        txtCaptchaFirst.Focus();
                //        mvFrgtPwd.ActiveViewIndex = 0;
                //        return false;
                //    }
                //}
                //else
                //{
                //    lblStep1Message.Text = Constants.InvalidCaptcha;
                //    DivStep1Message.Attributes.CssStyle.Add("display", "block");
                //    txtCaptchaFirst.Text = string.Empty;
                //    txtCaptchaFirst.Focus();
                //    mvFrgtPwd.ActiveViewIndex = 0;
                //    return false;
                //}

                //if (CaptchaFirst.UserValidated)
                //    return true;
                //lblStep1Message.Text = Constants.InvalidCaptcha;
                //txtCaptchaFirst.Text = string.Empty;
                //txtCaptchaFirst.Focus();
                //mvFrgtPwd.ActiveViewIndex = 0;
                //return false;
            }
            catch (Exception)
            {
                lblStep1Message.Text = Constants.InvalidCaptcha;
                DivStep1Message.Attributes.CssStyle.Add("display", "block");
                txtCaptchaFirst.Text = string.Empty;
                txtCaptchaFirst.Focus();
                return false;
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
        #endregion

        #region SecondView
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
                        mvFrgtPwd.ActiveViewIndex = 2;
                    }
                    else
                    {
                        LabelOTPMessage.Text = Constants.IncorrectOTP;
                        DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                        txtOTP.Focus();
                        StartOTPTimer();
                        mvFrgtPwd.ActiveViewIndex = 1;
                        return;
                    }
                }
                catch (Exception exp)
                {
                    LabelOTPErrorMessage.Text = Constants.GeneralErrorMessage;
                    DivOTPErrorMessage.Attributes.CssStyle.Add("display", "block");

                }
            }
        }

        /// <summary>
        /// Regenerate OTP
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void linkRegenerateOTP_Click(object sender, EventArgs e)
        {
            try
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
        #endregion

        #region ThirdView
        /// <summary>
        /// Handles the Click event of the btnchPwd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnchPwd_Click(object sender, EventArgs e)
        {
            ViewState["ModifyPassword"] = null;
            if (cmpConfirmPassowrd.IsValid && reqConfirmPassword.IsValid && reqNewPassword.IsValid)
                ViewState["ModifyPassword"] = txtNewPassword.Text.Trim();
            else
                throw new Exception(Constants.incorrectPWd);

            try
            {
                if (Session["Acc_Num"] != null && ViewState["ModifyPassword"] != null)
                {
                    CardHolderManager um = new CardHolderManager();
                    CardHolder_MstDTO user = um.FindUserByCrNumber(Session["Acc_Num"].ToString());
                    if (user.User_pwd == ViewState["ModifyPassword"].ToString())
                    {
                        lblErrpwd.Text = Constants.NotEqualPwd;
                        DivPwd.Attributes.CssStyle.Add("display", "block");
                    }
                    else
                    {
                        user.User_pwd = ViewState["ModifyPassword"].ToString();
                        um.UpdateCardHolder(user);
                        ClearControls();
                        // Response.Redirect("login.aspx");
                        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.GeneralRequestError + "');", true);
                    ClearControls();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.GeneralRequestError + "');", true);
                ClearControls();

            }
        }

        /// <summary>
        /// Handles the Click event of the btncancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void btncancel_Click(object sender, EventArgs e)
        //{
        //    ClearControls();
        //}
        #endregion

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
            DateTime returnDate = DateTime.MinValue;
            int day = DateTime.Today.Date.Day;
            int month = DateTime.Today.Date.Month;
            int year = DateTime.Today.Date.Year;
            string[] dateFormat = selectedDate.Split('/');
            if (dateFormat.Length == 3)
            {
                day = Convert.ToInt32(dateFormat[0]);
                month = Convert.ToInt32(dateFormat[1]);
                year = Convert.ToInt32(dateFormat[2]);
                returnDate = new DateTime(year, month, day);
            }

            return returnDate;
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
            catch (Exception ex)
            {
                lblStep1Message.Text = Constants.TechnicalError;
                string path = Server.MapPath("~/ErrorPage/ErrorLog");
                GeneralMethods.ErrorLog(path, ex);
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
            mvFrgtPwd.ActiveViewIndex = 0;
            lblErrpwd.Text = string.Empty;
            LabelOTPMessage.Text = string.Empty;
            LabelOTPErrorMessage.Text = string.Empty;
            DivOTPMessage.Attributes.CssStyle.Add("display", "none");
            DivOTPErrorMessage.Attributes.CssStyle.Add("display", "none");
            DivPwd.Attributes.CssStyle.Add("display", "none");
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
                //  j = j.Substring(2, 2);
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