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
    public partial class CreditCardReplacementRenewal : PageBase
    {
        #region Variable
        /// <summary>
        /// 
        /// </summary>
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        #endregion

        #region Data Load Events
        /// <summary>
        /// Loads the request.
        /// </summary>
        /// <remarks></remarks>
        private void LoadRequest()
        {
            ddlRequestType.Items.Insert(0, new ListItem("Renewal", Request.Params["renewalid"].ToString().DecryptURL()));
            ddlRequestType.Items.Insert(0, new ListItem("Replacement", Request.Params["replacementid"].ToString().DecryptURL()));
            ddlRequestType.Items.Insert(0, new ListItem(Constants.DDLRequest, "0"));
        }

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
                if (Request.Params["replacementid"] != null && Request.Params["renewalid"] != null)
                {
                    if (!IsPostBack)
                    {
                        LoadRequest();
                        LoadCardsinDDL();
                        loadCreditCardsName();
                        EnableDisalbeControl(true);
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                            return;
                        }
                    }
                    btnSubmit.Disabled = false;
                }
                else
                {
                    btnSubmit.Disabled = true;
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlRequestType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CardHolderReasonManager chrm = new CardHolderReasonManager();

            if (ddlRequestType.SelectedItem.Text == "Replacement")
            {
                ddlReasons.DataSource = chrm.ListReasonByRequestId(Convert.ToInt64(ddlRequestType.SelectedValue));
                ddlReasons.DataTextField = "Reason_nm";
                ddlReasons.DataValueField = "RequestReason_Id";
                ddlReasons.DataBind();
            }

            else if (ddlRequestType.SelectedItem.Text == "Renewal")
            {
                ddlReasons.DataSource = chrm.ListReasonByRequestId(Convert.ToInt64(ddlRequestType.SelectedValue));
                ddlReasons.DataTextField = "Reason_nm";
                ddlReasons.DataValueField = "RequestReason_Id";
                ddlReasons.DataBind();
            }
            else
            {
                ddlReasons.Items.Clear();
                ddlReasons.DataBind();
            }
            ddlReasons.Items.Insert(0, new ListItem(Constants.DDLReason, "0"));

            if (ddlRequestType.SelectedValue != "0")
            {
                bool IsAllowToAdd = CheckPendingRequest();
                if (!IsAllowToAdd)
                {
                    Clearcontrols();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                    return;
                }
            }
            else
            {
                btnSubmit.Disabled = false;
                btnSubmit.Attributes.Add("class", "button");
                EnableDisalbeControl(true);
            }
        }
        #endregion

        #region Post Request Detail
        /// <summary>
        /// Handles the Click event of the btnSubmitfinal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmitfinal_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

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
                    RequestType_Id = Convert.ToInt64(hideConfirmRequest.Value),
                    IP_Address = Request.UserHostAddress,
                    RequestReason_Id = Convert.ToInt64(hideConfirmReason.Value),
                    Created_dt = DateTime.Now,
                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                });

                Mailfunction(RequestDtlID);
                Clearcontrols();
                //lblMessage.Text = string.Format("Credit Card {0} Request has been sent", ddlRequestType.SelectedItem.Text);
                //lblMessage.CssClass = "msgsuccess";
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.GeneralErrorMessage;
                lblMessage.CssClass = "error";
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlcardlist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlcardlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreditCardsName();
        }

        /// <summary>
        /// Handles the Click event of the btnreset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnreset_Click(object sender, EventArgs e)
        {
            Clearcontrols();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();           

            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(ddlRequestType.SelectedValue), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnSubmit.Disabled = true;
                btnreset.Visible = false;
                btnSubmit.Attributes.Add("class", "buttonDisble");
                EnableDisalbeControl(false);
                return false;
            }
            else
            {
                btnSubmit.Disabled = false;
                btnreset.Visible = true;
                btnSubmit.Attributes.Add("class", "button");
                EnableDisalbeControl(true);
                return true;
            }
        }
        /// <summary>
        /// Enable/disable control
        /// </summary>        
        private void EnableDisalbeControl(bool ctrlState)
        {
            ddlReasons.Enabled = ctrlState;
            chkAgree.Enabled = ctrlState;
        }

        /// <summary>
        /// Loads the name of the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCardsName()
        {
            string Card_number = ddlcardlist.SelectedValue;
            CardManager cm = new CardManager();
            CH_CardDTO chdto = new CH_CardDTO();
            if (Card_number != "")
            {
                chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Card_number });
                if (chdto != null)
                    lblCardHolder.Text = chdto.FULL_NAME;
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
            if (CR_acc_num != "")
            {
                ddlcardlist.DataSource = cm.GetAllCardsForReplaceRenew(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
                ddlcardlist.DataTextField = "MASK_CARD_NUMBER";
                ddlcardlist.DataValueField = "CARD_NUMBER";
                ddlcardlist.DataBind();
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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.CreditCardReplacementRenewalTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", ddlcardlist.SelectedItem.Text);
                bodyString.Replace("@@RepRen", ddlRequestType.SelectedItem.Text);
                bodyString.Replace("@@Reason", ddlReasons.SelectedItem.Text);
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
            ddlRequestType.SelectedValue = "0";
            ddlReasons.Items.Clear();
            chkAgree.Checked = false;
        }

        #endregion

        #region WebMethods

        /// <summary>
        /// Replaces the renew charges.
        /// </summary>
        /// <param name="RequestType">Type of the request.</param>
        /// <param name="CardNumber">The card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string ReplaceRenewCharges(string RequestType, string CardNumber)
        {
            string FeeCharge = "";
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            //string Cardnumber = CardHolderManager.GetLoggedInUser().credit_card_number;
            if (CardNumber != "")
            {
                cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = CardNumber });
                if (cDTO != null)
                {

                    if (RequestType == "Replacement")
                        FeeCharge = Convert.ToString(cDTO.REPLACEMENT_CHARGES);
                    else
                        FeeCharge = Convert.ToString(cDTO.RENEWAL_CHARGES);
                }
            }
            return FeeCharge;
        }

        /// <summary>
        /// Allowedfors the renewal.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string AllowedforRenewal()
        {
            string Card_num = "";
            string Cr_Acc_Num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            //string ATMPinregDate;
            CardManager cm = new CardManager();
            if (Cr_Acc_Num != "")
                Card_num = cm.CardAllowedForRenewal(Cr_Acc_Num);
            return Card_num;
        }

        /// <summary>
        /// Allowedfors the replacement.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string AllowedforReplacement()
        {
            string Card_num = "";
            string Cr_Acc_Num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            //string ATMPinregDate;
            CardManager cm = new CardManager();
            if (Cr_Acc_Num != "")
                Card_num = cm.CardAllowedForReplacement(Cr_Acc_Num);
            return Card_num;
        }

        #endregion

    }
}