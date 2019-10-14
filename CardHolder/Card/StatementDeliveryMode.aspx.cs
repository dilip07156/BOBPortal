using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
    public partial class StatementDeliveryMode : PageBase
    {
        #region variable
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        #endregion

        #region PageLoad

        /// <summary>
        /// Loads the customer info.
        /// </summary>
        /// <remarks></remarks>
        private void loadCustomerInfo()
        {
            string CR_acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            CardManager cm = new CardManager();         

            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;

            card = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = card.card_number });
            if (card != null)
            {

                string firstName = UrlHelper.FirstCharToUpper(card.FIRST_NAME.ToLower());
                string lastName = UrlHelper.FirstCharToUpper(card.FAMILY_NAME.ToLower());
                lblCardHolder.Text = firstName + " " + lastName;
            }
            else
            {
                btnSubmit.Enabled = false;
            }


            if (CR_acc_num != "")
            {
                ddlCreditCardAccount.DataSource = cm.GetAllCardsForATMPinReg(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
                ddlCreditCardAccount.DataTextField = "MASK_CARD_NUMBER";
                ddlCreditCardAccount.DataValueField = "CARD_NUMBER";
                ddlCreditCardAccount.DataBind();
                // ddlcardlist.Items.Insert(0, new ListItem(Constants.DDLCard, "-1"));
            }

            DateTime? fromDate = null;
            DateTime? toDate = null;
            string strfromDate = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtFromDate.Text = strfromDate;
            fromDate = GetDateTime(strfromDate);
            string strtoDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtToDate.Text = strtoDate;
            toDate = GetDateTime(strtoDate);

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

                if (Request.Params["requestid"] != null)
                {
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().Replace(" ", "+").DecryptURL();
                    if (!IsPostBack)
                    {
                        loadCustomerInfo();
                        EnableDisalbeControl(true);
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    btnSubmit.Enabled = false;
                }
            }
        }
        #endregion

        #region Clickevents

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
                if (Page.IsValid)
                {
                    bool IsAllowToAdd = CheckPendingRequest();
                    if (!IsAllowToAdd)
                    {
                        Clearcontrols();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                        return;
                    }

                    string mode = "";
                    //if (chkAgree.Checked == false)
                    //{
                    //    lblMessage.Text = Constants.agreeTnC;
                    //    return;
                    //}
                    //else 
                    if (chkMode.Items[0].Selected == false && chkMode.Items[1].Selected == false)
                    {
                        lblMessage.Text = Constants.selectmode;
                        lblMessage.CssClass = "error";
                        return;
                    }
                    else if (chkMode.Items[0].Selected == true && chkMode.Items[1].Selected == true)
                    {
                        mode = "B";
                    }
                    else if (chkMode.Items[0].Selected == true && chkMode.Items[1].Selected == false)
                    {
                        mode = "E";
                    }
                    else if (chkMode.Items[0].Selected == false && chkMode.Items[1].Selected == true)
                    {
                        mode = "H";
                    }

                    CHRequestDetailManager crdm = new CHRequestDetailManager();
                    long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                    {
                        Request_Dt = DateTime.Now,
                        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                        Mode_Send_Statment = mode,
                        IP_Address = Request.UserHostAddress,
                        Created_dt = DateTime.Now,
                        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                    });
                    Mailfunction(RequestDtlID);
                    Clearcontrols();
                    //lblMessage.Text = "Statement Request has been sent";
                    //lblMessage.CssClass = "msgsuccess";
                }
            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        #endregion

        #region Mailfunction

        /// <summary>
        /// Mail functions used to send an email to the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            string mode = string.Empty;
            string CardHolderName = lblCardHolder.Text;
            string AccNum = CardHolderManager.GetLoggedInUser().CH_Card.Cr_Account_Nbr;
            string Email = CardHolderManager.GetLoggedInUser().CH_Card.EMAIL_ID;
            string FromDate = txtFromDate.Text;
            string ToDate = txtToDate.Text;

            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO chdto = new CH_Request_DtlDTO();
            chdto = cdm.getRequestUID(RequestDtlID);
            string RequestNumber = chdto.UID;
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string EMAIL_Subject = ConfigurationManager.AppSettings["REQUEST_EMAIL_SUBJECT"].ToString();
            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            if (!string.IsNullOrEmpty(OverRideEmail))
            {
                Email = OverRideEmail;
            }
            if (chkMode.Items[0].Selected == true && chkMode.Items[1].Selected == true)
            {
                mode = chkMode.Items[0].Text.ToString() + " & " + chkMode.Items[1].Text.ToString();
            }

            else if (chkMode.Items[0].Selected == true && chkMode.Items[1].Selected == false)
            {
                mode = chkMode.Items[0].Text.ToString();
            }

            else if (chkMode.Items[0].Selected == false && chkMode.Items[1].Selected == true)
            {
                mode = chkMode.Items[1].Text.ToString();
            }

            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.StatementRequestTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@AccNum", AccNum);
                bodyString.Replace("@@mode", mode);
                bodyString.Replace("@@FromDate", FromDate);
                bodyString.Replace("@@ToDate", ToDate);
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

        #endregion

        #region PrivateMethods

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {

            CHRequestDetailManager crdm = new CHRequestDetailManager();

            int pendingcount = crdm.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString());

            if (pendingcount > 0)
            {
                EnableDisalbeControl(false);
                return false;
            }
            else
            {               
                EnableDisalbeControl(true);
                return true;
            }
        }

        /// <summary>
        /// Enable/disable control
        /// </summary>        
        private void EnableDisalbeControl(bool ctrlState)
        {
            chkMode.Enabled = ctrlState;
        }

        /// <summary>
        /// Clear controls.
        /// </summary>
        /// <remarks></remarks>
        private void Clearcontrols()
        {
            chkMode.Items[0].Selected = false;
            chkMode.Items[1].Selected = false;
            lblMessage.Text = string.Empty;
            LblErrorMessage.Text = string.Empty;
        }

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

        #endregion

        #region Validations
        /// <summary>
        /// Dates the validate.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DateValidate(object sender, ServerValidateEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {

                e.IsValid = true;
                DateTime start = GetDateTime(txtFromDate.Text.Trim());
                DateTime end = GetDateTime(txtToDate.Text.Trim());
                if (end < start)
                {
                    e.IsValid = false;
                }
            }

        }

        #endregion


    }
}