using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class DeRegisterCreditCard : PageBase
    {
        #region variable
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        #endregion

        #region PageLoad
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
                lblCardAccNumber.Text = card.Cr_Account_Nbr;
            }
            else
            {
                btnContinue.Disabled = true;
            }
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
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().DecryptURL();
                    if (!IsPostBack)
                    {
                        EnableDisableControl(true);
                        loadCreditCards();
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "')", true);
                            return;
                        }
                    }
                }
                else
                {
                    btnContinue.Disabled = true;
                }
            }
        }

        #endregion

        #region Submit Request Detail
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
                bool IsAllowToAdd = CheckPendingRequest();
                if (!IsAllowToAdd)
                {
                    chkAgree.Checked = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "')", true);
                    return;
                }


                CHRequestDetailManager crdm = new CHRequestDetailManager();
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

                Mailfunction(RequestDtlID);
                chkAgree.Checked = false;
                //lblMessage.Text = "De-Register Credit Card Request has been sent";
                //lblMessage.CssClass = "msgsuccess";
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.GeneralErrorMessage;
                lblMessage.CssClass = "error";
            }
        }
        #endregion

        #region Private Method
        private bool CheckPendingRequest()
        {
            btnContinue.Attributes.Add("class", "button");
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();

            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnContinue.Disabled = true;
                btnContinue.Attributes.Add("class", "buttonDisble");
                EnableDisableControl(false);
                return false;
            }
            else
            {
                btnContinue.Disabled = false;
                EnableDisableControl(true);
                return true;
            }
        }

        /// <summary>
        /// Enable/Diable control
        /// </summary>
        private void EnableDisableControl(bool CtrlState)
        {
            chkAgree.Enabled = CtrlState;
        }
        #endregion

        #region Mail Function

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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.DeRegisterCreditCardTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditAccCard", lblCardAccNumber.Text);
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
    }
}