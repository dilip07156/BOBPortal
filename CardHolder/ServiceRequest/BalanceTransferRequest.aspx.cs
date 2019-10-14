using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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
    public partial class BalanceTransferRequest : PageBase
    {
        #region PageLoad
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
                        LoadIssuingBank();
                        loadCreditCards();
                        LoadPlans();
                    }
                }
            }
            lblMessage.Text = "";

        }
        #endregion

        #region ClickEvent
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
                //if (ddlIssueBank.SelectedValue == "-1")
                //{
                //    lblMessage.Text = "Please select Bank name";
                //    return;
                //}
                CHRequestDetailManager crdm = new CHRequestDetailManager();
                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                  {
                      Request_Dt = DateTime.Now,
                      CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                      RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                      IP_Address = Request.UserHostAddress,
                      OtherCreditCardNumber = txtCRnum1.Text + txtCRnum2.Text + txtCRnum3.Text + txtCRnum4.Text,
                      Bank_nm = ddlIssueBank.SelectedItem.Text,
                      Transferred_Amt = Convert.ToDecimal(txtAmtTransfered.Text),
                      Balance_Transferred_Plan = Convert.ToString(ddlPLan.SelectedValue), // ddlPLan.SelectedItem.Text,
                      Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                      Created_dt = DateTime.Now,
                      Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                  });
                Mailfunction(RequestDtlID);
                //lblMessage.Text = "Request for Transfer amount has been sent";
                //lblMessage.CssClass = "msgsuccess";
                ClearControls();
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.GeneralErrorMessage;
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
            try
            {
                ClearControls();
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.GeneralErrorMessage;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the issuing bank.
        /// </summary>
        /// <remarks></remarks>
        private void LoadIssuingBank()
        {
            CardHolderRequestManager chrm = new CardHolderRequestManager();
            ddlIssueBank.DataSource = chrm.getBankList();
            ddlIssueBank.DataTextField = "Bank_nm";
            ddlIssueBank.DataValueField = "Bank_Id";
            ddlIssueBank.DataBind();
            ddlIssueBank.Items.Insert(0, new ListItem(Constants.DDLBank, "-1"));

        }

        /// <summary>
        /// Loads the plans.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPlans()
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            List<DropDown_HdrDTO> list = dhm.SearchDllHeader("Balance_Transfer_Plan").ToList();
            if (list.Count > 0)
            {
                ddlPLan.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlPLan.DataTextField = "Description";
                ddlPLan.DataValueField = "DropDown_Dtl_Id";
                ddlPLan.DataBind();
                ddlPLan.Items.Insert(0, new ListItem(Constants.DDLPlan, "-1"));
            }
            else
            {
                lblMessage.Text = Constants.planNotfound;
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
                string Cardnumber = card.card_number;
                string StartCardnumber = "";
                string EndCardnumber = "";
                if (Cardnumber != "")
                {
                    StartCardnumber = Cardnumber.Substring(0, 4);
                    if (Cardnumber.Length == 16)
                        EndCardnumber = Cardnumber.Substring(13, 3);
                }

                lblCreditCardNumber.Text = StartCardnumber + "XXXXXXXXX" + EndCardnumber;
            }
        }

        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            string OtherCreditcardnumber = txtCRnum1.Text + txtCRnum2.Text + txtCRnum3.Text + txtCRnum4.Text;
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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.BalanceTransferRequestTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", OtherCreditcardnumber);
                bodyString.Replace("@@Bankname", ddlIssueBank.SelectedItem.Text);
                bodyString.Replace("@@Amount", txtAmtTransfered.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
               bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true,CardHolderId, null);
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
        /// Clears the controls.
        /// </summary>
        /// <remarks></remarks>
        private void ClearControls()
        {
            ddlIssueBank.SelectedValue = "-1";
            ddlPLan.SelectedValue = "-1";
            txtAmtTransfered.Text = string.Empty;
            txtCRnum1.Text = string.Empty;
            txtCRnum2.Text = string.Empty;
            txtCRnum3.Text = string.Empty;
            txtCRnum4.Text = string.Empty;
            txtRcnfrmCrnum1.Text = string.Empty;
            txtRcnfrmCrnum2.Text = string.Empty;
            txtRcnfrmCrnum3.Text = string.Empty;
            txtRcnfrmCrnum4.Text = string.Empty;
            chkAgree.Checked = false;
        }

        #endregion


    }
}