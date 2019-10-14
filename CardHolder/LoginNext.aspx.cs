using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using Microsoft.Security.Application;
using System.Web.UI.WebControls;
using CardHolder.Utility.OTP;
using System.Web.Services;
using System.Configuration;

namespace CardHolder
{
    public partial class LoginNext : System.Web.UI.Page
    {
        #region variable
        public static string OTPval { get; set; }
        public static string strMobile { get; set; }
        public static string hideMobileNumber { get; set; }
        public static string hideEmailId { get; set; }
        #endregion
        #region PageLoad Events

        int _cardHolderId;
        string EnumBlockedAccount = Convert.ToString(Utility.Enums.ErrorStatus.BlockedAccount);
        string EnumInactiveAccount = Convert.ToString(Utility.Enums.ErrorStatus.InactiveAccount);
        string EnumAccNotInNormalState = Convert.ToString(Utility.Enums.ErrorStatus.AccNotInNormalState);
        string EnumContinuesBlockedAccount = Convert.ToString(Utility.Enums.ErrorStatus.ContinuesBlockedAccount);
        string EnumInactiveAttempts = Convert.ToString(Utility.Enums.ErrorStatus.InactiveAttempts);



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnSubmit.Attributes.Add("onClick", "javascript:GetEncrypt();");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "VKeyboard", "init();", true);
                OnLoad();
            }


        }



        #endregion

        #region ClickEvents

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {          
           
            hdnErrormsgFromLoginNext.Value = "";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "VKeyboard", "init();", true);
            Session["AccountNumber"] = null;
            _cardHolderId = Convert.ToInt32(Session["CardHolderId"]);

            bool UserStatus = false;
            if (hdnTabIndex.Value == "0" && (txtUsername.Text == "" || txtPassword.Text == ""))
            {
                LblErrorMessage.Text = Constants.InvalidUnamePwd;
                DivERROR.Attributes.CssStyle.Add("display", "block");
                return;
            }
            if (hdnTabIndex.Value == "1" && (txtUsername.Text == "" || txtOTP.Text == ""))
            {
                LblErrorMessage.Text = Constants.InvalidUnamePwd;
                DivERROR.Attributes.CssStyle.Add("display", "block");
                return;
            }
            var am = new CardHolderManager();
            var cmn = new CardManager();
            var chlm = new CardHolderLoginInfoManager();
            var chdto = new List<CardHolderLogin_InfoDTO>();
            string Paswd = txtPassword.Text.Trim();
            txtPassword.Text = string.Empty;
            string publicIp = Request.UserHostAddress;
            var cardHolder = am.AuthenticateUser(txtUsername.Text.Trim(), publicIp);


            if (hdnTabIndex.Value == "0" && String.CompareOrdinal(Paswd, cardHolder.User_pwd) != 0)
            {
                if (_cardHolderId == 0)
                    Response.Redirect("ErrorPage/CodeError.aspx");


                int tries = 1;
                chdto = chlm.getCardHolderLoginInfoByID(_cardHolderId);

                if (chdto.Count > 0)
                {
                    if (chdto[0].Login_Attempt_FirstDt <= System.DateTime.Today.AddDays(-1) &&
                        chdto[0].Login_Attempts < 3)
                        chlm.DeleteCardHolderLoginInfo(_cardHolderId);
                    else if (chdto.Count == 2)
                    {
                        if (chdto[1].Login_Attempt_SecondDt <= System.DateTime.Today.AddDays(-1) &&
                            chdto[1].Login_Attempts < 3)
                            chlm.DeleteCardHolderLoginInfo(_cardHolderId);
                    }
                    else if (chdto.Count == 3)
                    {
                        if (chdto[2].Login_Attempt_ThirdDt <= System.DateTime.Today.AddDays(-1) &&
                            chdto[2].Login_Attempts < 3)
                            chlm.DeleteCardHolderLoginInfo(_cardHolderId);
                    }
                }

                chdto = chlm.getCardHolderLoginInfoByID(_cardHolderId);
                if (chdto.Count > 0)
                {
                    if (chdto[0].Login_Attempt_FirstDt == System.DateTime.Today && chdto[0].Login_Attempts < 3)
                        tries = Convert.ToInt32(chdto[0].Login_Attempts) + 1;

                    if (chdto[0].Login_Attempt_FirstDt != null && chdto[0].Login_Attempts == 3)
                    {
                        if (chdto.Count > 1)
                        {
                            if (chdto[1].Login_Attempt_SecondDt == System.DateTime.Today && chdto[1].Login_Attempts < 3)
                                tries = Convert.ToInt32(chdto[1].Login_Attempts) + 1;

                            if (chdto[1].Login_Attempt_SecondDt != null && chdto[1].Login_Attempts == 3)
                            {
                                if (chdto.Count > 2)
                                {
                                    if (chdto[2].Login_Attempt_ThirdDt == System.DateTime.Today &&
                                        chdto[2].Login_Attempts < 3)
                                        tries = Convert.ToInt32(chdto[2].Login_Attempts) + 1;

                                    if (chdto[2].Login_Attempt_ThirdDt != null && chdto[2].Login_Attempts == 3)
                                    {
                                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.BlockedAccount + "');", true);
                                        // viewCheckUsernameError.Text = Constants.BlockedAccount;
                                        ClearControls(EnumBlockedAccount);
                                    }
                                    else
                                    {
                                        chlm.UpdateCardHolderLoginInfoThird(new CardHolderLogin_InfoDTO()
                                        {
                                            CardHolder_Id = _cardHolderId,
                                            Login_Attempts = tries,
                                            Login_Attempt_ThirdDt = System.DateTime.Today
                                        });
                                        if (tries == 2)
                                        {
                                            lblMessage.Text = Constants.Leftwithonly1Attempt;
                                            DivMessage.Attributes.CssStyle.Add("display", "block");
                                        }
                                        else
                                        {
                                            chlm.SetCardHolderParmenentDisable(_cardHolderId);
                                            // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ContinuesBlockedAccount + "');", true);
                                            //viewCheckUsernameError.Text = Constants.ContinuesBlockedAccount;
                                            ClearControls(EnumContinuesBlockedAccount);
                                        }
                                    }
                                }
                                else
                                {
                                    if (chdto[1].Login_Attempt_SecondDt != System.DateTime.Today)
                                    {
                                        chlm.SaveCardHolderLoginInfo(new CardHolderLogin_InfoDTO()
                                        {
                                            CardHolder_Id = _cardHolderId,
                                            Login_Attempts = tries,
                                            IP_Address = Request.UserHostAddress,
                                            Login_Attempt_ThirdDt = System.DateTime.Today
                                        });
                                        lblMessage.Text = Constants.ThirdDayLeftwith2Attempts;
                                        DivMessage.Attributes.CssStyle.Add("display", "block");
                                    }
                                }
                            }
                            else
                            {
                                if (chdto[1].Login_Attempt_SecondDt != System.DateTime.Today)
                                {
                                    chlm.SaveCardHolderLoginInfo(new CardHolderLogin_InfoDTO()
                                    {
                                        CardHolder_Id = _cardHolderId,
                                        Login_Attempts = tries,
                                        IP_Address = Request.UserHostAddress,
                                        Login_Attempt_ThirdDt = System.DateTime.Today
                                    });
                                    lblMessage.Text = Constants.ThirdDayLeftwith2Attempts;
                                    DivMessage.Attributes.CssStyle.Add("display", "block");
                                }

                                else
                                {
                                    chlm.UpdateCardHolderLoginInfoSecond(new CardHolderLogin_InfoDTO()
                                    {
                                        CardHolder_Id = _cardHolderId,
                                        Login_Attempts = tries,
                                        Login_Attempt_SecondDt = System.DateTime.Today
                                    });

                                    if (tries == 2)
                                    {
                                        lblMessage.Text = Constants.SecndDayLeftwith1Attempts;
                                        DivMessage.Attributes.CssStyle.Add("display", "block");
                                    }
                                    else
                                    {
                                        chlm.SetCardHolderInActive(_cardHolderId);
                                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.InactiveAttempts + "');", true);
                                        // viewCheckUsernameError.Text = Constants.InactiveAttempts;
                                        ClearControls(EnumInactiveAttempts);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (chdto[0].Login_Attempt_FirstDt != System.DateTime.Today)
                            {
                                chlm.SaveCardHolderLoginInfo(new CardHolderLogin_InfoDTO()
                                {
                                    CardHolder_Id = _cardHolderId,
                                    Login_Attempts = tries,
                                    IP_Address = Request.UserHostAddress,
                                    Login_Attempt_SecondDt = System.DateTime.Today
                                });
                                lblMessage.Text = Constants.SecndDayLeftwith2Attempts;
                                DivMessage.Attributes.CssStyle.Add("display", "block");
                            }
                        }
                    }
                    else
                    {

                        if (chdto[0].Login_Attempt_FirstDt != System.DateTime.Today)
                        {
                            chlm.SaveCardHolderLoginInfo(new CardHolderLogin_InfoDTO()
                            {
                                CardHolder_Id = _cardHolderId,
                                Login_Attempts = tries,
                                IP_Address = Request.UserHostAddress,
                                Login_Attempt_SecondDt = System.DateTime.Today
                            });
                            lblMessage.Text = Constants.SecndDayLeftwith2Attempts;
                            DivMessage.Attributes.CssStyle.Add("display", "block");
                        }
                        else
                        {
                            chlm.UpdateCardHolderLoginInfofirst(new CardHolderLogin_InfoDTO()
                            {
                                CardHolder_Id = _cardHolderId,
                                Login_Attempts = tries,
                                Login_Attempt_FirstDt = System.DateTime.Today
                            });
                            if (tries == 2)
                            {
                                lblMessage.Text = Constants.Leftwith1Attempts;
                                DivMessage.Attributes.CssStyle.Add("display", "block");
                            }
                            else
                            {
                                chlm.SetCardHolderInActive(_cardHolderId);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.InactiveAttempts + "');", true);
                                //viewCheckUsernameError.Text = Constants.InactiveAttempts;
                                ClearControls(EnumInactiveAttempts);
                            }
                        }
                    }
                }

                else
                {
                    chlm.SaveCardHolderLoginInfo(new CardHolderLogin_InfoDTO()
                    {
                        CardHolder_Id = _cardHolderId,
                        Login_Attempts = tries,
                        IP_Address = Request.UserHostAddress,
                        Login_Attempt_FirstDt = System.DateTime.Today
                    });
                    lblMessage.Text = Constants.Leftwith2Attempts;
                    DivMessage.Attributes.CssStyle.Add("display", "block");

                }
            }
            else if (hdnTabIndex.Value == "1" && String.CompareOrdinal(txtOTP.Text, hdnOTP.Value.ToString()) != 0)
            {
                lblMessage.Text = Constants.IncorrectOTP;
                DivMessage.Attributes.CssStyle.Add("display", "block");
                divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
                divOTPSent.Attributes.CssStyle.Add("display", "block");
                divremaining.Attributes.CssStyle.Add("display", "block");
                hideResultMobile.Text = strMobile;
                txtOTP.Focus();
                StartOTPTimer();
                return;
            }
            else
            {
                UserStatus = cmn.AuthenticateUserStatus(cardHolder.creditcard_acc_number.Decrypt());
                if (UserStatus)
                {
                    string blocked = Constants.BlockedAccount;
                    Session["AccountNumber"] = cardHolder.creditcard_acc_number.Decrypt();
                    if (cardHolder.IsPermanentDisable == true && cardHolder.IsActive == false)
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "return Blokalert('" + blocked + "');",true);
                        //hdnErrormsgFromLoginNext.Value = Constants.BlockedAccount;
                        ClearControls(EnumBlockedAccount);
                    }
                    else if (cardHolder.IsPermanentDisable == true)
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.BlockedAccount + "');", true);
                        //viewCheckUsernameError.Text = Constants.BlockedAccount;
                        ClearControls(EnumBlockedAccount);
                    }
                    else if (cardHolder.IsActive == false)
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.InactiveAccount + "');", true);
                        //viewCheckUsernameError.Text = Constants.InactiveAccount;
                        ClearControls(EnumInactiveAccount);
                    }
                    else
                    {                       
                        Session["CardHolderId"] = cardHolder.CardHolder_Id;
                        chlm.DeleteCardHolderLoginInfo(_cardHolderId);

                        #region Create Session of IP and AntiFix for Privilege escalation (Horizontal)

                        // Random Token antifix
                        Random random = new Random();
                        string rndstr = random.Next(100000).ToString();
                        rndstr = Functions.GenerateHash(rndstr);
                        Session["STTLII"] = rndstr;
                        Response.Cookies["STTLII"].Value = rndstr;
                        Response.Cookies["STTLII"].HttpOnly = true;
                        //IP Of User
                        Session["STTLI"] = Functions.GenerateHash(Functions.GetIP());
                        Response.Cookies["STTLI"].Value = Functions.GenerateHash(Functions.GetIP());
                        Response.Cookies["STTLI"].HttpOnly = true;

                        #endregion

                        //Step 3 Submit CardHolder Master Data
                        CardHolderManager chm = new CardHolderManager();
                        CardHolder_MstDTO user = new CardHolder_MstDTO();
                        user.CardHolder_Id = _cardHolderId;
                        //chm.UpdateCardHolderLastLoginDetails(user);
                        chm.UpdateCardHolderDetailByID(user);

                        SetCookieAndRedirectToProfilePage(Encoder.HtmlEncode(txtUsername.Text.Trim()),
                                                          cardHolder.CardHolder_Id.ToString());

                    }
                }
                else
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.AccNotInNormalState + "');", true);
                    // viewCheckUsernameError.Text = Constants.AccNotInNormalState;
                    ClearControls(EnumAccNotInNormalState);
                }

            }
        }

        /// <summary>
        /// Handles the Click event of the lnkCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            //Session.RemoveUserDto();
            ClearControls();
        }

        //protected void rbAuthnticate_Changed(object sender, EventArgs e)
        //{
        //    lblMessage.Text = string.Empty;
        //    DivMessage.Attributes.CssStyle.Add("display", "none");
        //    LblErrorMessage.Text = string.Empty;
        //    DivERROR.Attributes.CssStyle.Add("display", "none");
        //    lblOTPMessage.Text = string.Empty;
        //    divOTP.Attributes.CssStyle.Add("display", "none");
        //    if (RadioAuthenticate.SelectedValue == "0")
        //    {
        //        divPassword.Attributes.CssStyle.Add("display", "block");
        //        divOTP.Attributes.CssStyle.Add("display", "none");
        //        divremaining.Attributes.CssStyle.Add("display", "none");
        //        rfvPwd.Enabled = true;
        //        rfvOTP.Enabled = false;                
        //    }
        //    else if(RadioAuthenticate.SelectedValue == "1")
        //    {
        //        divPassword.Attributes.CssStyle.Add("display", "none");
        //        divOTP.Attributes.CssStyle.Add("display", "block");
        //        divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
        //        divOTPSent.Attributes.CssStyle.Add("display", "block");
        //        rfvPwd.Enabled = false;
        //        rfvOTP.Enabled = true;
        //        string publicIp = Request.UserHostAddress;
        //        CardHolderManager chm = new CardHolderManager();
        //        var cardHolder = chm.AuthenticateUser(txtUsername.Text.Trim(), publicIp);
        //        if (cardHolder != null)
        //        {
        //            CardManager cm = new CardManager();
        //            CH_CardDTO card = cm.GetCardByCreditCardNumber(new CH_CardDTO() { Cr_Account_Nbr = cardHolder.creditcard_acc_number.Decrypt() });
        //            if (card != null)
        //            {
        //                hideMobileNumber.Value = card.PHONE_MOBILE;
        //                hideEmailId.Value = card.EMAIL_ID;                      
        //                Page.ClientScript.RegisterStartupScript(this.GetType(), "sendOTP()", "sendOTP()", true);
        //            }
        //        }
             
        //    }
        //}

        #endregion

        #region PrivateMethods



        private void OnLoad()
        {

            if (Session["CardHolderId"] != null && Convert.ToString(Session["CardHolderId"]) != "")
            {
                _cardHolderId = Convert.ToInt32(Session["CardHolderId"]);
                CardHolderManager chm = new CardHolderManager();
                var UserInfo = chm.GetUserInfoById(_cardHolderId);
                txtUsername.Text = UserInfo.User_nm;
                chkPersonalMessage.Text = UserInfo.Personal_Msg;
                txtUsername.Attributes.Add("readonly", "readonly");
            }
            else
                ClearControls();



            //if (PreviousPage != null && PreviousPage.IsCrossPagePostBack)
            //{
            //    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
            //    TextBox SourceTextBox = (TextBox)placeHolder.FindControl("txtCheckUsername");
            //    if (SourceTextBox != null)
            //    {
            //        txtUsername.Text = SourceTextBox.Text;
            //        txtUsername.Attributes.Add("readonly", "readonly");
            //    }
            //}
        }

        /// <summary>
        /// Sets the cookie and redirect to profile page.
        /// </summary>
        /// <param name="strUserName">Name of the STR user.</param>
        /// <param name="strUserID">The STR user ID.</param>
        /// <remarks></remarks>
        private void SetCookieAndRedirectToProfilePage(string strUserName, string strUserID)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
             strUserName,
             DateTime.Now,
             DateTime.Now.AddMinutes(10),
             false,
             strUserID,
             FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);
            //Session[".ASPXAUTH"] = encTicket;
            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            setUserSession(Convert.ToInt64(strUserID));

            //string str=  FormsAuthentication.GetRedirectUrl(, false);
            //Response.Redirect("~/UserRoleManagment/Profile.aspx");
            //Response.Redirect("~/UserManagment/Profile.aspx");
            Response.Redirect("~/AccountSummary/AccountSummary.aspx");
        }

        /// <summary>
        /// Sets the user session.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <remarks></remarks>
        private void setUserSession(long UserID)
        {
            HttpSessionState objHttpSessionState = HttpContext.Current.Session;
            if (objHttpSessionState != null)
            {
                LoggedInUser objLoggedInUsers = new LoggedInUser();
                objLoggedInUsers.SessionId = objHttpSessionState.SessionID;
                objLoggedInUsers.UserId = UserID;
                if ((List<LoggedInUser>)Application["lstLoggedUsers"] == null)
                    Application["lstLoggedUsers"] = new List<LoggedInUser>();

                if (((List<LoggedInUser>)Application["lstLoggedUsers"]).Exists(t => t.UserId == UserID))
                {
                    LoggedInUser objLoggedInUser = ((List<LoggedInUser>)Application["lstLoggedUsers"]).SingleOrDefault(t => t.UserId == UserID);
                    if (objLoggedInUser != null)
                    {
                        ((List<LoggedInUser>)Application["lstLoggedUsers"]).Remove(objLoggedInUser);
                    }
                }
                ((List<LoggedInUser>)Application["lstLoggedUsers"]).Add(objLoggedInUsers);
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        /// <remarks></remarks>
        private void ClearControls(string msg = "")
        {
            chkPersonalMessage.Checked = false;
            txtUsername.Text = string.Empty;
            Session["CardHolderId"] = string.Empty;
            if (msg != "")
                Response.Redirect("~/login.aspx?inmsg=" + msg.EncryptURL(), true);
            else
                Response.Redirect("~/login.aspx", true);



            // HttpContext.Current.RewritePath("login.aspx");
            //Response.Redirect("~/login.aspx", true);
        }

        /// <summary>
        /// Generates the OTP.
        /// </summary>
        /// <remarks></remarks>
        //private string GenerateOTP()
        //{
        //    string code = string.Empty;
        //    try
        //    {
        //        if (hdnOTP.Value == string.Empty)
        //        {
        //            string MobileNum = Convert.ToString(hideMobileNumber.Value);
        //            string EmailId = Convert.ToString(hideEmailId.Value);
        //            //if (!string.IsNullOrEmpty(OverRideMobile))
        //            //    MobileNum = OverRideMobile;
        //            //if (!string.IsNullOrEmpty(OverRideEmail))
        //            //    EmailId = OverRideEmail;
        //            txtOTP.Text = "";
        //            OTPClient otp = new OTPClient();
        //            long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
        //            code = otp.SendRequest(MobileNum, EmailId, Constants.ForgotPwd, CardHolderId);
        //            hdnOTP.Value = code;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LblErrorMessage.Text = Constants.TechnicalError;
        //        string path = Server.MapPath("~/ErrorPage/ErrorLog");
        //        GeneralMethods.ErrorLog(path, ex);
        //        return "0";
        //    }
        //    return code;
        //}

        /// <summary>
        /// Send OTP to Card holder registered contact number.
        /// </summary>
        /// <param name="CardNumber">The card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string SendOTP(string UserName)
        {
            //divPassword.Attributes.CssStyle.Add("display", "none");
            //        divOTP.Attributes.CssStyle.Add("display", "block");
            //        divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
            //        divOTPSent.Attributes.CssStyle.Add("display", "block");
            //        rfvPwd.Enabled = false;
            //        rfvOTP.Enabled = true;
            string MobileNumber = string.Empty;
            string EmailId = string.Empty;
            string publicIp = HttpContext.Current.Request.UserHostAddress;
            CardHolderManager chm = new CardHolderManager();
            var cardHolder = chm.AuthenticateUser(UserName, publicIp);
            if (cardHolder != null)
            {
                CardManager cm = new CardManager();
                CH_CardDTO card = cm.GetCardByCreditCardNumber(new CH_CardDTO() { Cr_Account_Nbr = cardHolder.creditcard_acc_number.Decrypt() });
                if (card != null)
                {                   
                   
                   MobileNumber = hideMobileNumber = card.PHONE_MOBILE;                   
                   EmailId = hideEmailId = card.EMAIL_ID;                    
                }
            }
            
            string MobileNum = "";
            string OtpSuccess = string.Empty;
            string jsonresult = string.Empty;

            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            string OverRideMobile = ConfigurationManager.AppSettings["OverRideUserMobile"];

            MobileNum = MobileNumber.Substring(0, 6);
            strMobile = MobileNum.Replace(MobileNum, "xxxxxx") + MobileNumber.Substring(MobileNumber.Length - 4);
            try
            {
                if (OtpSuccess == null || OtpSuccess == string.Empty)
                {
                    if (!string.IsNullOrEmpty(OverRideMobile))
                        MobileNum = OverRideMobile;
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["OverRideUserEmail"]))
                        EmailId = ConfigurationManager.AppSettings["OverRideUserEmail"];
                    OTPClient otp = new OTPClient();
                    long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                    OtpSuccess = otp.SendOTPRequest(MobileNumber, EmailId, Constants.ForgotUName, CardHolderId);
                    OTPval = OtpSuccess;
                    if (OtpSuccess != "0" && !string.IsNullOrEmpty(OtpSuccess))
                    {
                        int strsec = 20;
                        jsonresult = OtpSuccess + "," + strMobile + "," + strsec;

                    }


                }
            }
            catch (Exception ex)
            {

            }
            return jsonresult;
        }


        /// <summary>
        /// Clear OTP val after timeout operation 
        /// </summary>
        /// <returns></returns>     
        [WebMethod]
        public static string ClearHidOTP(string CardNumber)
        {

            string OtpSuccess = string.Empty;
            string jsonresult = string.Empty;

            try
            {
                OTPval = string.Empty;
                jsonresult = "1";
            }
            catch (Exception ex)
            {

            }
            return jsonresult;
        }

        public void StartOTPTimer()
        {
            string remaining = "60";
            ClientScript.RegisterStartupScript(this.GetType(), "timer", "timer('" + remaining + "');", true);           
            
        }

        #endregion
    }

}