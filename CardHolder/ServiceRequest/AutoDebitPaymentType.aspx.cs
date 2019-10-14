
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AutoDebitPaymentType : PageBase
    {

        #region Variables
        string DEFAULT_STATUS = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        private object divNotLinkedAccount;
        #endregion

        #region Load Data Events & Methods
        /// <summary>
        /// Loads the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCards()
        {
            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;
            if (card != null)
            {
               
                lblAccountNumber.Text = card.Cr_Account_Nbr;
                string Cardnumber = card.card_number;
                string StartCardnumber = "";
                string EndCardnumber = "";
                if (Cardnumber != "")
                {
                    StartCardnumber = Cardnumber.Substring(0, 4);
                    if (Cardnumber.Length == 16)
                        EndCardnumber = Cardnumber.Substring(13, 3);
                }

                lblCardNumber.Text = StartCardnumber + "XXXXXXXXX" + EndCardnumber;


                CH_CardDTO chdto = new CH_CardDTO();
                CardManager cm = new CardManager();
                chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Cardnumber });
                if (chdto != null)
                {

                    string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                    string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                    lblCardHolder.Text = firstName + " " + lastName;
                }

                CheckforEligibility();
            }
        }
        /// <summary>
        /// Loads the reasons.
        /// </summary>
        /// <remarks></remarks>
        private void LoadReasons()
        {
            CardHolderReasonManager chrm = new CardHolderReasonManager();
            ddlReasons.DataSource = chrm.ListReasonByRequestId(Convert.ToInt64(hideRequestTypeId.Value));
            ddlReasons.DataTextField = "Reason_nm";
            ddlReasons.DataValueField = "RequestReason_Id";
            ddlReasons.DataBind();
            ddlReasons.Items.Insert(0, new ListItem(Constants.DDLReason, "-1"));

        }
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


            }
            else
            {
                if (Request.Params["requestid"] != null)
                {
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().DecryptURL();
                    if (!IsPostBack)
                    {                        
                        loadCreditCards();
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            btnReset.Enabled = false;
                            lblMessage.Text = Constants.PendingRequestState;
                            DivMessage.Attributes.CssStyle.Add("display", "block");
                            return;
                        }
                        CheckforEligibility();                       
                    }
                }
            }
        }
        #endregion

        #region Submit Request Details
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
                var crdm = new CHRequestDetailManager();
                var requestDtlId = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                 {
                     Request_Dt = DateTime.Now,
                     CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                     RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                     Payment_Type = (rbTotalAmountDue.Checked == true ? hideTotalAmountDue.Value : (rbMinimumAmountDue.Checked == true ? hideMinimumAmountDue.Value : (rbPercentage.Checked == true ? hidePercentage.Value : ""))),
                     Specific_Monthly_due = rbPercentage.Checked == true ? Convert.ToDecimal(txtPercentage.Text) : (decimal?)null,
                     IP_Address = Request.UserHostAddress,
                     Created_dt = DateTime.Now,
                     Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                     Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                 });
                Mailfunction(requestDtlId);
                Clearcontrols();              
                
            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnSubmitfinal.Attributes.CssStyle.Add("display", "block");
        //        btnReset.Visible = false;
        //        divStatusMessage.Attributes.CssStyle.Add("display", "none");
        //        divMain.Attributes.CssStyle.Add("display", "block");
        //        EnableDisableControl();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnDeregister_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlReasons.SelectedValue == "-1")
                {
                    lblErrorReasons.Text = Constants.Selectreason;
                    return;
                }

                CHRequestDetailManager crdm = new CHRequestDetailManager();
                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                {
                    Request_Dt = DateTime.Now,
                    CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                    RequestReason_Id = Convert.ToInt64(ddlReasons.SelectedValue),
                    IP_Address = Request.UserHostAddress,
                    Created_dt = DateTime.Now,
                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                });
                Mailfunction(RequestDtlID);
                Clearcontrols();
                LblSuccessMessage.Text = "Request of discontinue of Auto Debit Payment has been sent";
                DivSuccess.Attributes.CssStyle.Add("display", "block");

            }
            catch (Exception)
            {              
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }



        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                CheckforEligibility();
                Clearcontrols();

            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }
        #endregion

        #region PrivateMethod

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();

            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
               
                lblMessage.Text = Constants.PendingRequestState;
                DivMessage.Attributes.CssStyle.Add("display", "block");
                return false;
            }
            else
            {
                //btnproceed.Visible = true;
                //btndisable.Visible = false;               
                return true;
            }
            
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlReqcomplaint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void OnRadioEnableDisable_Changed(object sender, EventArgs e)
        {
            if (RadioEnableDisable.SelectedValue == "1")
            {
                LoadReasons();
                lbldivDeregister.Attributes.CssStyle.Add("display", "block");
                divTermsandCondition.Attributes.CssStyle.Add("display", "none");
                divEnable.Attributes.CssStyle.Add("display", "none");
            }
            else if (RadioEnableDisable.SelectedValue == "0")
            {
                lbldivDeregister.Attributes.CssStyle.Add("display", "none");
                divTermsandCondition.Attributes.CssStyle.Add("display", "block");
                divEnable.Attributes.CssStyle.Add("display", "block");
            }
        }

        
        /// <summary>
        /// Enable/Diable control
        /// </summary>
        private void EnableDisableControl()
        {
            
            if (RadioEnableDisable.SelectedValue != "1")
            {                           
                divEnable.Attributes.CssStyle.Add("display", "block");
                divMain.Attributes.CssStyle.Add("display", "block");
                divTermsandCondition.Attributes.CssStyle.Add("display", "block");
                chkAgree.Enabled = true;

                if (ViewState["autoDebitPaymentValue"].ToString() == "TAD")
                {
                    rbTotalAmountDue.Checked = true;                    
                }
                else if (ViewState["autoDebitPaymentValue"].ToString() == "MAD")
                {
                    rbMinimumAmountDue.Checked = true;                   
                }
                else if (ViewState["autoDebitPaymentValue"].ToString() == "PERCENTAGE")
                {
                    rbPercentage.Checked = true;
                    txtPercentage.Text = ViewState["DIRECTDEBITPERCENTAGE"].ToString();
                }
            }
        }

        /// <summary>
        /// Checkfors the eligibility.
        /// </summary>
        /// <remarks></remarks>
        private void CheckforEligibility()
        {
            // string IsEligible = "";

          
            var cDTO = new CH_CardDTO();
            var cm = new CardManager();
            string _urlstring = "Auto Debit Payment cannot be requested as no Bank Account linked to this card. To link account please contact your Bank Branch <a href='{0}' target='_blank'> Check for nearest branch </a>";
            string _url = "http://www.bobcards.com/Auto_Debit.doc";            
            string crAccNum = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (crAccNum != "")
            {
                cDTO = cm.GetAccountDetails(new CH_CardDTO() { Cr_Account_Nbr = crAccNum });
                if (cDTO != null)
                {
                    lblbnkAccnum.Text = cDTO.CURRENT_ACC_NBR;                   
                }
                if (lblbnkAccnum.Text == "")
                {                    
                     lblbnkAccnum.Text = string.Format(_urlstring, _url);
                }
                else
                {                   
                    loadExitingDetails();                   
                    
                }
            }
        }

        /// <summary>
        /// Checkfors the eligibility.
        /// </summary>
        /// <remarks></remarks>
        private void LoadAccountNumber()
        {
            // string IsEligible = "";
            var cDTO = new CH_CardDTO();
            var cm = new CardManager();            
            string crAccNum = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (crAccNum != "")
            {
                cDTO = cm.GetAccountDetails(new CH_CardDTO() { Cr_Account_Nbr = crAccNum });
                if (cDTO != null)
                {
                    lblbnkAccnum.Text = cDTO.CURRENT_ACC_NBR;
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "EditPaymentType();", true);                
            }
        }


        private void loadExitingDetails()
        {
            var cDTO = new CH_CardDTO();
            var cm = new CardManager();
            string autoDebitPaymentStatus = string.Empty;
            string autoDebitPaymentValue = string.Empty;
            string autoDebitPaymentType = string.Empty;
            string crAccNum = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            cDTO = cm.GetAutoDebitPaymentType(new CH_CardDTO() { Cr_Account_Nbr = crAccNum });
            if (cDTO != null)
            {
                autoDebitPaymentStatus = cDTO.AUTO_DEBIT_STATUS;
                autoDebitPaymentValue = cDTO.AUTO_DEBIT_TYPE;
                autoDebitPaymentType = SetPaymentType(cDTO.AUTO_DEBIT_TYPE);
                ViewState["autoDebitPaymentValue"] = autoDebitPaymentValue;
                ViewState["DIRECTDEBITPERCENTAGE"] = cDTO.DIRECT_DEBIT_PERCENTAGE;
            }

            if (autoDebitPaymentStatus == "ACTIVE")
            {
                LblStatusMessage.Text = "Your Credit Card is enabled for Auto Debit Payment type for " + "'" + autoDebitPaymentType +  "'" +  " Please click Edit " +
                        "to change or disable ";
                divStatusMessage.Attributes.CssStyle.Add("display", "block");
                btnReset.Visible = true;
                RadioEnableDisable.SelectedValue = "0";
                
            }
            else if (autoDebitPaymentStatus == "INACTIVE")
            {
                RadioEnableDisable.SelectedValue = "1";
                lbldivDeregister.Attributes.CssStyle.Add("display", "block");
                divTermsandCondition.Attributes.CssStyle.Add("display", "none");
                divEnable.Attributes.CssStyle.Add("display", "none");               
                LoadReasons();
            }
        }

        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            string CardHolderName = lblCardHolder.Text;
            string Email = CardHolderManager.GetLoggedInUser().CH_Card.EMAIL_ID;
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string EMAIL_Subject = ConfigurationManager.AppSettings["REQUEST_EMAIL_SUBJECT"].ToString();


            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO chdto = new CH_Request_DtlDTO();
            chdto = cdm.getRequestUID(RequestDtlID);
            string RequestNumber = chdto.UID;
            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            if (!string.IsNullOrEmpty(OverRideEmail))
            {
                Email = OverRideEmail;
            }
            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.AutoDebitPaymentTypeTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCardAcc", lblAccountNumber.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
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
        /// Clearcontrolses this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Clearcontrols()
        {
            rbMinimumAmountDue.Checked = false;
            rbPercentage.Checked = false;
            rbTotalAmountDue.Checked = false;
            chkAgree.Checked = false;
            lblMessage.Text = "";
            divEnable.Attributes.CssStyle.Add("display", "none");
            divMain.Attributes.CssStyle.Add("display", "none");
            lbldivDeregister.Attributes.CssStyle.Add("display", "none");
            btnReset.Enabled = true;
        }


        private string SetPaymentType(string AUTO_DEBIT_TYPE)
        {
            string paymentType = string.Empty;
            if (AUTO_DEBIT_TYPE == "TAD")
            {
                paymentType=  "Total Amount Due";               
            }
            else if (AUTO_DEBIT_TYPE == "MAD")
            {
                paymentType = "Minimum Amount Due";
            }
            else if (AUTO_DEBIT_TYPE == "PERCENTAGE")
            {
                paymentType = "Specific % Monthly Due";               
            }

            return paymentType;
        }

        
        #endregion

        #region WebMethod

        /// <summary>
        /// Gets the last auto debit details.
        /// </summary>
        /// <param name="AccountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string GetLastAutoDebitDetails(string AccountNumber)
        {
            string PaymentDtl = "";
            //string ATMPinregDate;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            if (AccountNumber != "")
            {
                cDTO = cm.GetAutoDebitPaymentDetails(new CH_CardDTO() { Cr_Account_Nbr = AccountNumber });
                if (cDTO != null)
                {
                    // string DebitType = cm.GetCodeNameFromSyscode(cDTO.AUTO_DEBIT_MODE);
                    string DebitType = cDTO.AUTO_DEBIT_MODE;
                    string BranchName = cm.GetBranchNameByCode(cDTO.AUTO_DEBIT_BRANCH);
                    PaymentDtl = cDTO.Embossed_Name + "," + DebitType + "," + cDTO.DIRECT_DEBIT_PERCENTAGE + "," + BranchName;
                }
            }
            return PaymentDtl;
        }

        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                btnSubmitfinal.Attributes.CssStyle.Add("display", "block");
                btnReset.Visible = false;
                divStatusMessage.Attributes.CssStyle.Add("display", "none");
                divMain.Attributes.CssStyle.Add("display", "block");
                EnableDisableControl();
            }
            catch (Exception ex)
            {

            }
        }
    }
}