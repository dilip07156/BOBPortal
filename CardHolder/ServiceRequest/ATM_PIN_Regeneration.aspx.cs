using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.Enums;
using CardHolder.Utility.OTP;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;


namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class ATM_PIN_Regeneration : PageBase
    {
        #region variable
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        public char PasswordChar { get; set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string OTPval {get; set;}

        public static DateTime LastOTPSent { get; set; }
        #endregion





        #region PageLoad

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf)
            {

                //ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "timer(" + 60 + ");", true);                
                divOTP.Attributes.CssStyle.Add("display", "block");
                divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
                divOTPSent.Attributes.CssStyle.Add("display", "block");
                divremaining.Attributes.CssStyle.Add("display", "block");
                divInvalidfeedback.Attributes.CssStyle.Add("display", "none");
                lblOTPMessage.Attributes.CssStyle.Add("display", "none");

            }
            else
            {
                if (Request.Params["requestid"] != null)
                {
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().Replace(" ", "+").DecryptURL();
                    if (!IsPostBack)
                    {
                        //btnSubmit.Enabled = false;
                        txtATMPIN.Text = string.Empty;
                        txtConfirmATMPIN.Text = string.Empty;
                        LoadCardsinDDL();
                        EnableDisalbeControl(true);
                        loadCreditCardsName();
                    }
                    
                }

            }
        }


        #endregion

       

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlcardlist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlcardlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreditCardsName();
            Session["selectedCardNumber"] = ddlcardlist.SelectedValue;
        }


        

       
        /// <summary>
        /// This method is used to save ATM PIN details in database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int i = 0;
            bool validOTP = false;
            if (ViewState["OTPAttempt"] != null)
            {
                if (ViewState["OTPAttempt"].ToString() != string.Empty)
                    i = Convert.ToInt32(ViewState["OTPAttempt"]);
            }
           try
           {
                string str = OTPCurrentValue.Value;

                if (txtOTP.Text == string.Empty || txtOTP.Text == null)
                {
                    
                    Label1.Text = "Please enter OTP";
                    Label1.Attributes.CssStyle.Add("display", "block");
                    divInvalidfeedback.Attributes.CssStyle.Add("display", "block");
                    divOTPSent.Attributes.CssStyle.Add("display", "none");
                    divremaining.Attributes.CssStyle.Add("display", "none");
                    divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
                    divOTP.Attributes.CssStyle.Add("display", "block");
                    ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);
                    return;                    
                }
                else
                {

                    if (txtOTP.Text == OTPval && hideCreditAccNumber.Value != null && txtATMPIN.Text == "" && HiddenField1.Value == "")
                    {
                        SetNewPin.Visible = true;
                        //SetNewPin.Attributes.CssStyle.Add("display", "block");
                        divPIN.Visible = true;
                        validOTP = true;
                        lblmsg.Text = "OTP Verified!";
                        divOTP.Attributes.CssStyle.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
                        validOTP = true;
                        HiddenField1.Value = "1";
                        ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);

                    }
                    else if (txtOTP.Text == OTPval && hideCreditAccNumber.Value != null && txtATMPIN.Text != "" && txtConfirmATMPIN.Text != "" && validOTP == true)
                    {
                        SetNewPin.Visible = true;
                        //SetNewPin.Attributes.CssStyle.Add("display", "block");
                        divPIN.Visible = true;
                        validOTP = true;
                        lblmsg.Text = "OTP Verified!";
                        divOTP.Attributes.CssStyle.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
                        validOTP = true;
                        HiddenField1.Value = "1";
                        ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);
                    }

                    else if (txtOTP.Text != OTPval)
                    {

                        validOTP = false;
                        i++;
                        ViewState["OTPAttempt"] = i;
                        lblOTPMessage.Text = Constants.IncorrectOTP;
                        divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
                        divOTP.Attributes.CssStyle.Add("display", "block");
                        divOTPSent.Attributes.CssStyle.Add("display", "none");
                        divremaining.Attributes.CssStyle.Add("display", "none");
                        divInvalidfeedback.Attributes.CssStyle.Add("display", "block");
                        lblOTPMessage.Attributes.CssStyle.Add("display", "block");
                        Label1.Attributes.CssStyle.Add("display", "none");
                        ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);
                        txtOTP.Text = string.Empty;
                        txtOTP.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
                        //mvFrgtUname.ActiveViewIndex = 0;
                        if (i == 3)
                        {
                            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + "You entered the OTP incorrectly a maximum of 3 times." + "Please you may request for a new OTP by clicking on the Regenerate OTP" + "');", true);
                            txtOTP.Text = "";
                            lblOTPMessage.Text = "You entered the OTP incorrectly a maximum of 3 times." + "Please you may request for a new OTP by clicking on the Regenerate OTP";
                            divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");
                            divOTP.Attributes.CssStyle.Add("display", "block");
                            divOTPSent.Attributes.CssStyle.Add("display", "none");
                            divremaining.Attributes.CssStyle.Add("display", "none");
                            divInvalidfeedback.Attributes.CssStyle.Add("display", "block");
                            ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);
                        }
                        return;
                    }
                }
                
                if (Page.IsValid)
                {
                    if (HiddenField1.Value == "1" && txtATMPIN.Text != "")
                    {
                        CHRequestDetailManager crdm = new CHRequestDetailManager();
                        ViewState["ATMPIN"] = txtATMPIN.Text;
                        try
                        {
                            if (txtATMPIN.Text == txtConfirmATMPIN.Text && hideCreditAccNumber.Value != null)
                            {
                                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                                {
                                    Request_Dt = DateTime.Now,
                                    CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                                    RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                                    IP_Address = Request.UserHostAddress,
                                    Created_dt = DateTime.Now,
                                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                                });

                                System.Threading.Thread.Sleep(1000);
                                //ScriptManager.RegisterStartupScript(this, GetType(), "ShowProgress", "ShowProgress();", true);
                                CreateRequest(txtATMPIN.Text, RequestDtlID);
                                btnSubmit.Style.Remove("data-loading");                                
                            }
                            else
                            {
                                LblError.Text = Constants.PINMismatch;
                                LblError.Attributes.CssStyle.Add("display", "block");
                                divIncorrectOTP.Attributes.CssStyle.Add("display", "flex");                               
                                txtATMPIN.Text = ViewState["ATMPIN"].ToString();
                                divOTP.Attributes.CssStyle.Add("display", "none");
                                return;
                            }
                        }
                        catch (Exception exp)
                        {
                            lblOTPMessage.Text = exp.Message;
                            lblOTPMessage.Text = Constants.TechnicalError;
                        }

                        if (ViewState["ATMPIN"] == null)
                        {
                            txtATMPIN.Text = ViewState["ATMPIN"].ToString();
                        }
                    }
                }
                else
                {
                    divOTP.Attributes.CssStyle.Add("display", "none");
                }
            }

            catch (Exception exp)
            {
                lblOTPMessage.Text = Constants.TechnicalError;
            }  

            OTPCurrentValue.Value = string.Empty;
            

        }

        
        protected void btnClose_Click(object sender, EventArgs e)
        {
            SetNewPin.Visible = false;
            divOTP.Style.Add("display", "none");
            ScriptManager.RegisterStartupScript(this, GetType(), "setValue", "setValue();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);
        }
        private void UpdateStatus(dynamic result, long Request_Dtl_Id)
        {
            CHRequestDetailManager crdm = new CHRequestDetailManager();
            string requestStatus = string.Empty;
            if (result != null)
            {
                if (result["RespCode"] == "000")
                {
                    requestStatus = "Approved";
                }
                else
                {
                    requestStatus = "Rejected";
                }
            }
            else
            {
                requestStatus = "Rejected";
            }

            long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
            {
                Request_Dtl_Id = Request_Dtl_Id,
                Request_Dt = DateTime.Now,
                CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                IP_Address = Request.UserHostAddress,
                Created_dt = DateTime.Now,
                Updated_dt = DateTime.Now,
                Updated_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                Request_Status = requestStatus
            });
        }

        private void SaveAuditLog(string TransRefNo, dynamic objResult)
        {
            CHRequestDetailManager crdm = new CHRequestDetailManager();
            String responseStatus = string.Empty;
            if (objResult != null)
            {
                responseStatus = objResult["RespDesc"];
            }
            else
            {
                responseStatus = "Null Response from Jetty Server ";
            }

            long RequestDtlID = crdm.SaveAuditLog(new AuditLog_DTO()
            {
                RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                TxnType = TranscationType.PC.ToString(),
                Credit_card_number = Convert.ToString(hideCreditCardNumber.Value).Encrypt(),
                TxnReferenceNo = TransRefNo,
                ResponseStatus = responseStatus,
                Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                Created_dt = DateTime.Now,
                BankRefNo = hideCreditAccNumber.Value.ToString(),
                IP_Address = Request.UserHostAddress
            });
        }
        private void CreateRequest(string PIN, long RequestDtlID)
        {
            string result = string.Empty;
            Helper objHelper = new Helper();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string TransRefNo = objHelper.RandomDigits();
            ATMPINRequest objATMPINRequest = new ATMPINRequest()
            {
                TxnType = TranscationType.PC.ToString(),
                CardNumber = Convert.ToString(hideCreditCardNumber.Value),
                TransRefNo = TransRefNo,
                TransDateTime = String.Format("{0:MM/dd/yyyy}", DateTime.Now),
                PIN = PIN
            };
            string jsondata = js.Serialize(objATMPINRequest);
            result = objHelper.GetResponse(jsondata);
            logger.Info("Jetty Server Response String:" + result);
            dynamic objResult = null;
            if (result == null)
            {
                SetNewPin.Visible = false;                
                divPIN.Visible = false;
                divOTP.Attributes.CssStyle.Add("display", "none");                      
                LblErrorMessage.Text = Constants.CommonError;
                DivERROR.Attributes.CssStyle.Add("display", "inline-block");
            }
            else
            {
                objResult = js.Deserialize<dynamic>(result);
                DisplayMessage(objResult, RequestDtlID);
            }
                       
            ViewState["OTPAttempt"] = string.Empty;
            UpdateStatus(objResult, RequestDtlID);
            SaveAuditLog(TransRefNo, objResult);
            //btnSubmit.Enabled = false;
            ddlcardlist.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "UncheckValue", "UncheckValue();", true);

        }


        private void DisplayMessage(dynamic result, long Request_Dtl_Id)
        {
            if (result != null)
            {
                if (result["RespCode"] == "000")
                {
                    Mailfunction(Request_Dtl_Id);                  
                    LblSuccessMessage.Text = "PIN has been set successfully";
                    DivSuccess.Attributes.CssStyle.Add("display", "block");                   
                  
                }
                else if (result["RespCode"] == "204")
                {
                    LblErrorMessage.Text = Constants.ErrorCode204;
                    DivERROR.Attributes.CssStyle.Add("display", "block");                   
                }
                else
                {                   
                    LblErrorMessage.Text = Constants.CommonError;
                    DivERROR.Attributes.CssStyle.Add("display", "block");                    
                }
            }
            else
            {
                LblErrorMessage.Text = Constants.CommonError;
                DivERROR.Attributes.CssStyle.Add("display", "block");               
            }


        }


        /// <summary>
        /// This button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlcardlist.Enabled = true;
                ClearControls();
            }
            catch (Exception exp)
            {
                LblErrorMessage.Text = Constants.TechnicalError;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }

        }
        

        #region Private Methods

        private void ClearControls()
        {
            Session.Abandon();
            Session.Clear();
            txtOTP.Text = string.Empty;
            txtATMPIN.Text = string.Empty;
            txtConfirmATMPIN.Text = string.Empty;
          
        }



        /// <summary>
        /// Enable/disable control
        /// </summary>        
        private void EnableDisalbeControl(bool ctrlState)
        {
            ddlcardlist.Enabled = ctrlState;
           
        }


        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            bool IsMailSent = false;

            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO chdto = new CH_Request_DtlDTO();
            chdto = cdm.getRequestUID(RequestDtlID);

            string RequestNumber = chdto.UID;
            string CardHolderName = lblCardHolder.Text;
            string Email = CardHolderManager.GetLoggedInUser().CH_Card.EMAIL_ID;
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string EMAIL_Subject = ConfigurationManager.AppSettings["REQUEST_EMAIL_SUBJECT"].ToString();
            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            if (!string.IsNullOrEmpty(OverRideEmail))
                Email = OverRideEmail;

            try
            {
                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.ATM_PIN_RegenerationTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", ddlcardlist.SelectedItem.Text);
                // bodyString.Replace("@@Reason", ddlReasons.SelectedItem.Text);
                //bodyString.Replace("@@Rupee", lblCharge.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
                if (IsMailSent)
                {
                    LblSuccessMessage.Text = "Your Request has been sent.";
                    DivSuccess.Attributes.CssStyle.Add("display", "block");
                }

                else
                {
                    LblErrorMessage.Text = Constants.ErrorMailButRqstLogged;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                }
            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.ErrorMailButRqstLogged;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }



        /// <summary>
        /// Loads the cardsin DDL.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCardsinDDL()
        {
            string CR_acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            CardManager cm = new CardManager();
            CH_CardDTO card = cm.GetCardByCreditCardNumber(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
            hideCreditAccNumber.Value = CR_acc_num;
            if (card != null)
            {
                hideMobileNumber.Value = card.PHONE_MOBILE;
                hideEmailId.Value = card.EMAIL_ID;
                
            }

            if (CR_acc_num != "")
            {
                ddlcardlist.DataSource = cm.GetAllCardsForATMPinReg(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
                ddlcardlist.DataTextField = "MASK_CARD_NUMBER";
                ddlcardlist.DataValueField = "CARD_NUMBER";
                ddlcardlist.DataBind();                
            }
        }

        
        /// <summary>
        /// Gets the ATM pin fees.
        /// </summary>
        /// <param name="CreditCardNumber">The credit card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double getATMPinFees(string CreditCardNumber)
        {
            double AtmPinfee = 0;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = CreditCardNumber });
            if (cDTO != null)
                AtmPinfee = cDTO.PIN_CAL_FEES;
            return AtmPinfee;
        }

        /// <summary>
        /// Loads the name of the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCardsName()
        {
            string Card_number = ddlcardlist.SelectedValue;
            int? status = 0;
            CardManager cm = new CardManager();
            CH_CardDTO chdto = new CH_CardDTO();
            if (Card_number != "")
            {
                chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Card_number });
                if (chdto != null)
                {
                    
                    string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                    string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                    lblCardHolder.Text = firstName + " " + lastName;
                    status = chdto.STATUS_CODE;
                    if (status != 0)
                    {
                        LblErrorMessage.Text = Constants.AccNotNormal;
                        DivERROR.Attributes.CssStyle.Add("display", "block");                        
                    }
                    else
                    {
                      
                    }

                }
            }
            else
            {
                LblErrorMessage.Text = Constants.SomeProblem;
                DivERROR.Attributes.CssStyle.Add("display", "block");               
            }
        }
        #endregion

        #region WebMethod

        /// <summary>
        /// Gets the last ATM pin details.
        /// </summary>
        /// <param name="CardNumber">The card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string GetLastATMPinDetails(string CardNumber)
        {
            string ReqNum = "";
            //string ATMPinregDate;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            if (CardNumber != "")
            {
                cDTO = cm.GetATMPinDetails(new CH_CardDTO() { card_number = CardNumber });
                if (cDTO != null)
                {                  
                    string PinDate = GeneralMethods.FormatDate(Convert.ToDateTime(cDTO.PIN_REGENERATION_DATE)).ToString();
                    ReqNum = cDTO.ATMP_PIN_REQUEST_NUMBER + "," + PinDate;
                }
            }
            return ReqNum;
        }

        /// <summary>
        /// Send OTP to Card holder registered contact number.
        /// </summary>
        /// <param name="CardNumber">The card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>        
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SendOTP(string CardNumber, string MobileNumber, string EmailId)
        {            
            string MobileNum = "";
            string OtpSuccess = string.Empty;
            string jsonresult = string.Empty;

            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            string OverRideMobile = ConfigurationManager.AppSettings["OverRideUserMobile"];

            MobileNum = MobileNumber.Substring(0, 6);
            string strMobile = MobileNum.Replace(MobileNum, "xxxxxx") + MobileNumber.Substring(MobileNumber.Length - 4);

            //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session with 20 sec wait time VAPT issue

            if (HttpContext.Current.Session["OTP_Count"] != null && HttpContext.Current.Session["OTP_Count"].ToString() != "")
            {
                int cnt = 0;
                if (Int32.TryParse(HttpContext.Current.Session["OTP_Count"].ToString(), out cnt) && cnt >= 3)
                {
                    return jsonresult = Constants.MaxNoOfOTPMessage + ",";
                    
                }
            }
            if (LastOTPSent != null && LastOTPSent.ToString() != "")
            {
                DateTime d;
                if (DateTime.TryParse(LastOTPSent.ToString(), out d))
                {
                    TimeSpan difference = DateTime.Now.Subtract(d);
                    if (difference.TotalSeconds < 20)
                    {
                        return jsonresult = Constants.MaxNoOfOTPMessageForTime + ",";                        
                        
                    }
                    else
                         jsonresult = "";
                }
            }
            //End Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT issue

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
                    //Start Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session, VAPT issue
                    if (HttpContext.Current.Session["OTP_Count"] != null && HttpContext.Current.Session["OTP_Count"].ToString() != "")
                    {
                        int cnt = 0;
                        if (Int32.TryParse(HttpContext.Current.Session["OTP_Count"].ToString(), out cnt))
                        {
                            HttpContext.Current.Session["OTP_Count"] = cnt + 1;
                        }
                    }
                    else
                        HttpContext.Current.Session["OTP_Count"] = "1";
                    LastOTPSent = DateTime.Now;
                    //End Added by abhijeet on 09/10/2019 to restrict 3 OTP in one session VAPT, issue
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


        #endregion
    }
}