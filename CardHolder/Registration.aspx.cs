using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.OTP;

namespace CardHolder
{
    public partial class Registration : Page
    {

        #region Configuration
        string[] EXTENSIONS_ATTACH = ConfigurationManager.AppSettings["ImageFiles"].ToString().Split(',');
        string ROOT_UPLOAD_FOLDER = ConfigurationManager.AppSettings["ROOT_UPLOAD_FOLDER"];
        string PROFILE_FOLDER = ConfigurationManager.AppSettings["PROFILE_FOLDER"];

        string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
        string OverRideMobile = ConfigurationManager.AppSettings["OverRideUserMobile"];
        readonly string _expiryYear = ConfigurationManager.AppSettings["ExpiryYear"];

        protected static string ReCaptcha_Key = Convert.ToString(ConfigurationManager.AppSettings["ReCaptcha_Key"]);
        protected static string ReCaptcha_Secret = Convert.ToString(ConfigurationManager.AppSettings["ReCaptcha_Secret"]);
        #endregion


        /// <summary>
        /// Page load and make OTP session variable null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            btnSubmit.Attributes.Add("onClick", "javascript:return ValidateThisForm(this.form);");
            if (!IsPostBack)
            {
                hdnOTP.Value = String.Empty;
                mvNewUserRegistration.ActiveViewIndex = 0;
                MonthYear();
                txtMobileNo.Attributes.Add("readonly", "readonly");
            }
            //if(Session["OTP"] != null)
            //{
            //    divremaining.Attributes.CssStyle.Add("display", "block");
            //}
            //else
            //{
            //    divremaining.Attributes.CssStyle.Add("display", "none");
            //}
            //ImgOTPCaptcha.ImageUrl = "../captcha.ashx?dt=" + DateTime.Now.ToString();
            //ImgInfoCaptcha.ImageUrl = "../captcha.ashx?dt=" + DateTime.Now.ToString();
        }

        /// <summary>
        /// Active Page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitfinal_Click(object sender, EventArgs e)
        {
            //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT issue
            if (Session["OTP_Reg_User"] != null && Session["OTP_Reg_User"].ToString() != "")
            {
                int cnt = 0;
                if (Int32.TryParse(Session["OTP_Reg_User"].ToString(), out cnt) && cnt >= 3)
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
            if (IsValidInfo())
            {
                //FirstFour.Text = hdnCard1.Value;
                //SecondFour.Text = hdnCard2.Value;
                //ThirdFour.Text = hdnCard3.Value;
                //ForthFour.Text = hdnCard4.Value;
                //string FullCardnumber = hdnCard.Value;

                string FullCardnumber = (FirstFour.Text + SecondFour.Text + ThirdFour.Text + ForthFour.Text).Trim();



                int ExpiryMonth = Convert.ToInt32(ddlmonth.SelectedItem.Text);
                int ExpiryYear = Convert.ToInt32(ddlyear.SelectedItem.Text);
                string DateOfBirth = Convert.ToString(GetDateTime(txtbirthdate.Text));

                //string DateOfBirth = txtbirthdate.Text;
                // Step 1 Find CARD In Oracle Database

                CardManager cm = new CardManager();
                CH_CardDTO card = cm.AuthenticateCrNumberDOBForRegis(new CH_CardDTO() { card_number = FullCardnumber, EXPIRY_MONTH = ExpiryMonth, EXPIRY_YEAR = ExpiryYear, BIRTH_DATE = Convert.ToDateTime(DateOfBirth) });


                // Step 2 Find Cardholder In SQL Database Either exists or not

                CardHolderManager Cardholder = new CardHolderManager();
                // CardHolder_MstDTO user = Cardholder.FindUserByCrNumber(FullCardnumber.Encrypt());                
                if (card != null)
                    IsUserExists = Cardholder.FindUserByAccountNumber(card.Cr_Account_Nbr.Encrypt());
                else
                {
                    lblStep1Message.Text = Constants.DataNotFoundindb;
                    DivStep1Message.Attributes.CssStyle.Add("display", "block");
                    txtCaptchaFirst.Text = string.Empty;
                    mvNewUserRegistration.ActiveViewIndex = 0;
                    return;
                }


                if (card != null && IsUserExists == false)
                {
                    mvNewUserRegistration.ActiveViewIndex = 1;
                    ViewState["mobile"] = card.PHONE_MOBILE;
                    ViewState["CARD_NUMBER"] = card.card_number;
                    ViewState["CR_ACCOUNT_NBR"] = card.Cr_Account_Nbr;
                    ViewState["Email_ID"] = card.EMAIL_ID;
                    lblhdnfullname.Value = card.FULL_NAME;

                    //string ImageCaptcha = ImgOTPCaptcha.ClientID; // Request.Form[ImgOTPCaptcha.UniqueID].ToString();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "GenerateCaptcha", "RefreshCaptcha('" + ImageCaptcha + "');", true);
                    // mvNewUserRegistration.ActiveViewIndex = 1;

                    string OtpSuccess = GenerateOTP();
                    if (OtpSuccess != "0" && !string.IsNullOrEmpty(OtpSuccess))
                    {
                        mvNewUserRegistration.ActiveViewIndex = 1;
                        //divremaining.Visible = true;
                        StartOTPTimer();


                    }
                    else
                    {
                        Clearcontrols();
                        LblStep1ErrorMessage.Text = Constants.TechnicalError;
                        DivStep1ERROR.Attributes.CssStyle.Add("display", "block");
                        return;
                    }

                    if (ViewState["mobile"] != null)
                    {
                        string mobilenum = ViewState["mobile"].ToString();
                        string lastFourdgts = string.Empty;
                        if (mobilenum != "")
                        {
                            int numberkength = mobilenum.Length;
                            if (numberkength > 4)
                                lastFourdgts = mobilenum.Substring(numberkength - 4, 4);
                            else
                                lastFourdgts = mobilenum;
                        }

                        // mobilenum = mobilenum.Substring(6, 4);
                        //lblOTPdesc.Text = Constants.OTPDesc1;
                        //lblmob.Text = "(i.e. XXXXXX" + lastFourdgts + ")";
                        //lbl3.Text = Constants.OTPDesc2;
                        txtMobileNo.Text = Convert.ToString(ViewState["mobile"]);
                    }
                    //else
                    //{
                    //    lblOTPdesc.Text = Constants.OTPDesc1;
                    //    lblmob.Text = "(i.e. XXXXXXXXXX)";
                    //    lbl3.Text = Constants.OTPDesc2;
                    //}
                }

                else //if (card != null && user != null)
                {
                    lblStep1Message.Text = Constants.AlreadyRegister;
                    DivStep1Message.Attributes.CssStyle.Add("display", "block");
                    Clearcontrols();
                }

                //else
                //{
                //    lblStep1Message.Text = Constants.DataNotFoundindb;
                //    // Clearcontrols();
                //}
            }
        }



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
                //    //CaptchaFirst.ValidateCaptcha(captchaFrst);
                //    if (captchaFrst != "" && Convert.ToString(Session["strRandomCH"]) == captchaFrst)
                        return true;
                //    else
                //    {
                //        lblStep1Message.Text = Constants.InvalidCaptcha;
                //        DivStep1Message.Attributes.CssStyle.Add("display", "block");
                //        txtCaptchaFirst.Text = string.Empty;
                //        txtCaptchaFirst.Focus();
                //        mvNewUserRegistration.ActiveViewIndex = 0;
                //        return false;
                //    }
                //}
                //else
                //{
                //    lblStep1Message.Text = Constants.InvalidCaptcha;
                //    DivStep1Message.Attributes.CssStyle.Add("display", "block");
                //    txtCaptchaFirst.Text = string.Empty;
                //    txtCaptchaFirst.Focus();
                //    mvNewUserRegistration.ActiveViewIndex = 0;
                //    return false;
                //}

                //if (CaptchaFirst.UserValidated)
                //    return true;
                //lblStep1Message.Text = Constants.InvalidCaptcha;
                //txtCaptchaFirst.Text = string.Empty;
                //txtCaptchaFirst.Focus();
                //mvNewUserRegistration.ActiveViewIndex = 0;
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
        /// Gets the date time.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        /// <returns></returns>
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
        /// OTP & Captcha Validation Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOTPContinue_Click(object sender, EventArgs e)
        {
            //string ImageCaptcha = ImgOTPCaptcha.ClientID; // Request.Form[ImgOTPCaptcha.UniqueID].ToString();
            try
            {
                if (Page.IsValid)
                {
                    if (hdnOTP.Value != String.Empty)
                    {
                        if (hdnOTP.Value.ToString() != txtOTP.Text)
                        {
                            LabelOTPMessage.Text = Constants.IncorrectOTP;
                            DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                            Label1.Attributes.CssStyle.Add("display", "none");
                            txtOTP.Focus();
                            StartOTPTimer();
                            mvNewUserRegistration.ActiveViewIndex = 1;
                            return;
                        }
                        else
                        {
                            mvNewUserRegistration.ActiveViewIndex = 2;
                        }
                    }
                    else
                    {
                        LabelOTPMessage.Text = Constants.IncorrectOTP;
                        DivOTPMessage.Attributes.CssStyle.Add("display", "block");
                        txtOTP.Focus();
                        StartOTPTimer();
                        mvNewUserRegistration.ActiveViewIndex = 1;
                        return;
                    }
                //    try
                //    {
                //        //string otpText = txtOTPCaptcha.Text.Trim();
                //        string isvalid = "N";
                //        if (!string.IsNullOrEmpty(otpText) && otpText.Length == 6)
                //        {
                //            //cptCaptcha.ValidateCaptcha(otpText);
                //            if (otpText != "" && Convert.ToString(Session["strRandomCH"]) == otpText)
                //                isvalid = "Y";
                //            else
                //            {
                //                isvalid = "N";
                //                lblOTPMessage.Text = Constants.InvalidCaptcha;
                //                txtCaptchaFirst.Text = string.Empty;
                //                //txtOTPCaptcha.Focus();
                //                mvNewUserRegistration.ActiveViewIndex = 1;
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            lblOTPMessage.Text = Constants.InvalidCaptcha;
                //            txtCaptchaFirst.Text = string.Empty;
                //            txtOTPCaptcha.Focus();
                //            mvNewUserRegistration.ActiveViewIndex = 1;
                //            return;
                //        }

                //        //if (cptCaptcha.UserValidated)
                //        if (isvalid == "Y")
                //        {
                //            mvNewUserRegistration.ActiveViewIndex = 2;
                //        }
                //        else
                //        {
                //            lblOTPMessage.Text = Constants.InvalidCaptcha;
                //            txtCaptchaFirst.Text = string.Empty;
                //            //txtOTPCaptcha.Focus();
                //            mvNewUserRegistration.ActiveViewIndex = 1;
                //            return;
                //        }
                //    }
                //    catch (Exception)
                //    {
                //        lblOTPMessage.Text = Constants.InvalidCaptcha;
                //        txtCaptchaFirst.Text = string.Empty;
                //        txtOTPCaptcha.Focus();
                //        mvNewUserRegistration.ActiveViewIndex = 1;
                //    }
                }
            }

            catch (Exception)
            {
                //lblOTPMessage.Text = Constants.Error1;
                //txtCaptchaFirst.Text = string.Empty;
            }
        }

        private string GenerateOTP()
        {
            string code = string.Empty;
            try
            {
                if (hdnOTP.Value == String.Empty)
                {
                    txtOTP.Text = "";
                    //txtOTPCaptcha.Text = "";
                    string MobileNum = Convert.ToString(ViewState["mobile"]);
                    string EmailId = Convert.ToString(ViewState["Email_ID"]);
                    if (!string.IsNullOrEmpty(OverRideMobile))
                        MobileNum = OverRideMobile;
                    if (!string.IsNullOrEmpty(OverRideEmail))
                        EmailId = OverRideEmail;
                    OTPClient otp = new OTPClient();
                    long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                    code = otp.SendRequest(MobileNum, EmailId, Constants.Registration, CardHolderId);
                    // txtOTP.Text = code;
                    hdnOTP.Value = code;
                    //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session, VAPT issue
                    if (Session["OTP_Reg_User"] != null && Session["OTP_Reg_User"].ToString() != "")
                    {
                        int cnt = 0;
                        if (Int32.TryParse(Session["OTP_Reg_User"].ToString(), out cnt))
                        {
                            Session["OTP_Reg_User"] = cnt + 1;
                        }
                    }
                    else
                        Session["OTP_Reg_User"] = "1";
                    ViewState["LastOTPSent"] = DateTime.Now;
                    //End Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT, issue
                }
            }
            catch (Exception)
            {
                LblStep1ErrorMessage.Text = Constants.TechnicalError;
                DivStep1ERROR.Attributes.CssStyle.Add("display", "block");
                return "0";
            }
            return code;
        }


        /// <summary>
        /// Submit Card Holder Master Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string filename = "";
            string saveFile = "";
            // string ImageInfoCaptcha = ImgInfoCaptcha.ClientID; // Request.Form[ImgInfoCaptcha.UniqueID].ToString(); 

            //try
            //{
            //    //Step 0 Check Captcha
            //    string isvalid = "N";
            //    string otpInfoText = txtInfoCaptcha.Text.Trim();
            //    if (!string.IsNullOrEmpty(otpInfoText) && otpInfoText.Length == 6)
            //    {
            //        //CaptchaforInfo.ValidateCaptcha(otpInfoText);
            //        if (otpInfoText != "" && Convert.ToString(Session["strRandomCH"]) == otpInfoText)
            //            isvalid = "Y";
            //        else
            //        {
            //            isvalid = "N";
            //            lblStep3Message.Text = Constants.InvalidCaptcha;
            //            txtCaptchaFirst.Text = string.Empty;
            //            txtPassword.Text = "";
            //            txtConfirmPassword.Text = "";
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        lblStep3Message.Text = Constants.InvalidCaptcha;
            //        txtCaptchaFirst.Text = string.Empty;
            //        txtPassword.Text = "";
            //        txtConfirmPassword.Text = "";
            //        return;
            //    }

            //    //if (!CaptchaforInfo.UserValidated)
            //    if (isvalid == "N")
            //    {
            //        txtPassword.Text = "";
            //        txtConfirmPassword.Text = "";
            //        lblStep3Message.Text = Constants.InvalidCaptcha;
            //        txtCaptchaFirst.Text = string.Empty;
            //        txtInfoCaptcha.Focus();
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    lblStep3Message.Text = Constants.InvalidCaptcha;
            //    txtCaptchaFirst.Text = string.Empty;
            //    return;
            //}

            try
            {

                //Step 1 Check User Availibality
                if (CheckAvailability() == false)
                {
                    return;
                }

                ///Step 2 Upload file
                //if (photoUpload.HasFile)
                //{
                //    var bytes = new byte[20];
                //    photoUpload.PostedFile.InputStream.Read(bytes, 0, 20);


                //    if (!GeneralMethods.CheckFileHeader(photoUpload.FileName, bytes, EXTENSIONS_ATTACH))
                //    {
                //        GeneralMethods.AlertMessage(Page, "Please Upload file having file type is .jpg or .jpeg or .png only");
                //        return;
                //    }
                //    if (!System.IO.Directory.Exists(string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, ROOT_UPLOAD_FOLDER, PROFILE_FOLDER)))
                //    {
                //        System.IO.Directory.CreateDirectory(string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, ROOT_UPLOAD_FOLDER, PROFILE_FOLDER));
                //    }
                //    filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photoUpload.FileName);
                //    saveFile = string.Format("{0}{1}\\{2}\\{3}", AppDomain.CurrentDomain.BaseDirectory, ROOT_UPLOAD_FOLDER, PROFILE_FOLDER, filename);
                //    photoUpload.SaveAs(saveFile);
                //}

                //Step 3 Submit CardHolder Master Data
                CardHolderManager chm = new CardHolderManager();
                chm.SaveCardHolder(new DTO.CardHolder_MstDTO()
                {
                    User_nm = txtUserId.Text.Trim(),
                    User_pwd = txtPassword.Text.Trim(),
                    Profile_Photo = filename,
                    Personal_Msg = txtPersonalMessage.Text.Trim(),
                    IP_Address = Request.UserHostAddress.Trim(),
                    Created_dt = DateTime.Now,
                    credit_card_number = ViewState["CARD_NUMBER"].ToString().Encrypt(),
                    creditcard_acc_number = ViewState["CR_ACCOUNT_NBR"].ToString().Encrypt(),
                });

                //proImage.ImageUrl = string.Format("/{0}/{1}/{2}", ROOT_UPLOAD_FOLDER, PROFILE_FOLDER, filename);
                MailSendFunction();
                mvNewUserRegistration.ActiveViewIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
                //Session["CAPTCHA"] = null;
                Clearcontrols();
                ClearOnlyFormControls();
                LblSuccessMessage.Text = "Registration has been completed successfully, click <a href='login.aspx'>Login</a>";
                DivSuccess.Attributes.CssStyle.Add("display", "block");                
                //Clearcontrols();
            }
            catch (Exception)
            {
                if (System.IO.File.Exists(saveFile))
                {
                    System.IO.File.Delete(saveFile);
                }
                lblpwd.Text = Constants.ErrorRegister;
                DivPwd.Attributes.CssStyle.Add("display", "block");
            }
        }


        /// <summary>
        /// Regenerate OTP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkRegenerateOTP_Click(object sender, EventArgs e)
        {
            //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session with 20 sec wait time VAPT issue

            if (Session["OTP_Reg_User"] != null && Session["OTP_Reg_User"].ToString() != "")
            {
                int cnt = 0;
                if (Int32.TryParse(Session["OTP_Reg_User"].ToString(), out cnt) && cnt >= 3)
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
            if (ViewState["mobile"] != null)
            {
                hdnOTP.Value = String.Empty;
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

        /// <summary>
        /// User Id check availability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void lnkAvailability_Click(object sender, EventArgs e)
        //{
        //    if (txtUserId.Text != "")
        //        CheckAvailability();
        //    else
        //    {
        //        lblStep3Message.Text = "Please enter userId";
        //        lblAvailabilityMessage.Text = "";
        //    }
        //}

        private bool CheckAvailability()
        {
            CardHolderManager um = new CardHolderManager();
            CardHolder_MstDTO card_holder = um.FindUser(txtUserId.Text.Trim());
            if (card_holder == null)
            {
                //lblAvailabilityMessage.Text = "User name is available";
                //lblAvailabilityMessage.ForeColor = System.Drawing.Color.Green;
                return true;
            }
            //lblAvailabilityMessage.Text = "User name is not available";
            //lblAvailabilityMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clearcontrols();
            lblStep1Message.Text = string.Empty;
            DivStep1Message.Attributes.CssStyle.Add("display", "none");
        }

        protected void btnOTPReset_Click(object sender, EventArgs e)
        {
            //txtOTPCaptcha.Text = string.Empty;
            txtOTP.Text = string.Empty;
            mvNewUserRegistration.ActiveViewIndex = 1;
            return;
        }

        protected void btnResetForm_Click(object sender, EventArgs e)
        {
            ClearOnlyFormControls();
        }

        private void MailSendFunction()
        {
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string SUBJECT = ConfigurationManager.AppSettings["Registration_EMAIL_SUBJECT"].ToString();
            string Email = ViewState["Email_ID"].ToString();

            if (!string.IsNullOrEmpty(OverRideEmail))
                Email = OverRideEmail;
            //Step 4 Send Email to registered user
            try
            {
                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("") + Constants.RegistrationTemplatepath));
                bodyString.Replace("@@UserName", txtUserId.Text.Trim());
                bodyString.Replace("@@CardHolderName", lblhdnfullname.Value);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", SUBJECT, bodyString.ToString(), true, CardHolderId, null);

            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.Errorsendingmail + "');", true);
            }
        }

        private void MonthYear()
        {
            List<string> Next10Yrs = new List<string>();
            int currentYear = DateTime.Now.Year;
            int expiryYear = Convert.ToInt32(_expiryYear);

            for (int i = currentYear; i <= currentYear + expiryYear; i++)
            {
                string j = Convert.ToString(i);
                Next10Yrs.Add(j);
            }
            //databind here

            ddlyear.DataSource = Next10Yrs;
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("YY", "-1"));
        }

        private void Clearcontrols()
        {

            FirstFour.Text = string.Empty;
            SecondFour.Text = string.Empty;
            ThirdFour.Text = string.Empty;
            ForthFour.Text = string.Empty;
            txtbirthdate.Text = string.Empty;
            ddlmonth.SelectedValue = "-1";
            ddlyear.SelectedValue = "-1";
            txtCaptchaFirst.Text = string.Empty;
            chkAgree.Checked = false;
            Session.Abandon();
            Session.Clear();


        }

        private void ClearOnlyFormControls()
        {
            //lblAvailabilityMessage.Text = "";
            lblpwd.Text = "";
            DivPwd.Attributes.CssStyle.Add("display", "none");
            txtPersonalMessage.Text = string.Empty;
            //txtUID.Text = string.Empty;
            txtUserId.Text = string.Empty;
            //txtInfoCaptcha.Text = string.Empty;
        }

        public void StartOTPTimer()
        {
            string remaining = "60";
        ClientScript.RegisterStartupScript(this.GetType(), "timer", "timer('" + remaining + "');", true);
        }

        [WebMethod]
        public static string FindUser(string username)
        {
            CardHolderManager um = new CardHolderManager();
            return um.FindUser(username) == null ? "0" : "1";
        }

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
    }
}