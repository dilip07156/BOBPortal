using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AutoDebitDe_Registration : PageBase
    {
        #region Variables
        string DEFAULT_STATUS = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        #endregion

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
                if (Request.Params["requestid"] != null)
                {
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().DecryptURL();
                    if (!IsPostBack)
                    {
                        EnableDisableControl(true);
                        loadCreditCards();
                        LoadReasons();
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                            return;
                        }
                        if (btnproceed.Visible = true && btndisable.Visible == false)
                        {
                            CheckforEligibility();
                        }
                    }
                }
            }
        }

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

                bool IsAllowToAdd = CheckPendingRequest();
                if (!IsAllowToAdd)
                {
                    Clearcontrols();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
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
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.GeneralErrorMessage;
                lblMessage.CssClass = "error";
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Enable/Diable control
        /// </summary>
        private void EnableDisableControl(bool CtrlState)
        {
            ddlReasons.Enabled = CtrlState;
            chkAgree.Enabled = CtrlState;
        }

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {

            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();

            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnproceed.Visible = false;
                btndisable.Visible = true;
                EnableDisableControl(false);
                return false;
            }
            else
            {
                btnproceed.Visible = true;
                btndisable.Visible = false;
                EnableDisableControl(true);
                return true;
            }
        }

        /// <summary>
        /// Loads the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCards()
        {
            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;
            if (card != null)
            {
                lblCardHolder.Text = card.FULL_NAME;
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
            }
        }

        /// <summary>
        /// Checkfors the eligibility.
        /// </summary>
        /// <remarks></remarks>
        private void CheckforEligibility()
        {
            //string CurrenAccBranch = "";
            int? BranchFlag = null;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            string Cr_Acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (Cr_Acc_num != "")
            {
                cDTO = cm.GetAccountDetails(new CH_CardDTO() { Cr_Account_Nbr = Cr_Acc_num });
                if (cDTO != null)
                {
                    // CurrenAccBranch = cDTO.CURRENT_ACC_BRANCH;
                    // lblBranchName.Text = cm.GetBranchNameByCode(CurrenAccBranch);
                    BranchFlag = cDTO.DIRECT_DEBIT_AMOUNT_FLAG;
                    lblbnkAccnum.Text = cDTO.CURRENT_ACC_NBR;
                }
                if (BranchFlag == null)
                {
                    btnproceed.Visible = false;
                    btndisable.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.AutoDeRegisNotAllowed + "');", true);
                }
                else
                {
                    btnproceed.Visible = true;
                    btndisable.Visible = false;
                }
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

            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            if (!string.IsNullOrEmpty(OverRideEmail))
            {
                Email = OverRideEmail;
            }
            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO chdto = new CH_Request_DtlDTO();
            chdto = cdm.getRequestUID(RequestDtlID);
            string RequestNumber = chdto.UID;

            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.AutoDebitDeRegistrationTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCardAcc", lblAccountNumber.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
                if (IsMailSent)
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ErrorMailButRqstLogged + "');", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ErrorMailButRqstLogged + "');", true);
            }
        }

        /// <summary>
        /// Clearcontrolses this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Clearcontrols()
        {
            chkAgree.Checked = false;
            lblMessage.Text = "";
            ddlReasons.SelectedValue = "0";
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
            ddlReasons.Items.Insert(0, new ListItem(Constants.DDLReason, "0"));

        }


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
                    //string DebitType = cm.GetCodeNameFromSyscode(cDTO.AUTO_DEBIT_MODE);
                    string DebitType = cDTO.AUTO_DEBIT_MODE;
                    string BranchName = cm.GetBranchNameByCode(cDTO.AUTO_DEBIT_BRANCH);
                    PaymentDtl = cDTO.Embossed_Name + "," + DebitType + "," + cDTO.DIRECT_DEBIT_PERCENTAGE + "," + BranchName;
                }
            }
            return PaymentDtl;
        }


        #endregion

    }
}