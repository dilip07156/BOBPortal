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
    public partial class BonusPointRedemption : PageBase
    {

        #region Variable
        string DEFAULT_STATUS = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
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
                        SetBonuspoint();
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            lblMessage.Text = Constants.PendingRequestState;
                            DivMessage.Attributes.CssStyle.Add("display", "block");
                            return;
                        }
                    }
                }
            }
            LblErrorMessage.Text = "";
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
                bool IsAllowToAdd = CheckPendingRequest();
                if (!IsAllowToAdd)
                {
                    txtpointsReddeem.Text = string.Empty;
                    chkAgree.Checked = false;
                    lblMessage.Text = Constants.PendingRequestState;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }


                int redeempts = string.IsNullOrEmpty(txtpointsReddeem.Text) ? 0 : Convert.ToInt32(txtpointsReddeem.Text);
                int earnedpts = string.IsNullOrEmpty(lblEarnedPoints.Text) ? 0 : lblEarnedPoints.Text != "NIL" ? Convert.ToInt32(lblEarnedPoints.Text) :0;
                CHRequestDetailManager crdm = new CHRequestDetailManager();
                if (txtpointsReddeem.Text.Trim() != "" && redeempts >= 500 && redeempts <= earnedpts)
                {

                    long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                    {
                        Request_Dt = DateTime.Now,
                        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                        IP_Address = Request.UserHostAddress,
                        Points_Wants_Redeem = Convert.ToInt32(txtpointsReddeem.Text),
                        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        Created_dt = DateTime.Now,
                        Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                    });
                    Mailfunction(RequestDtlID);
                    
                }
                else
                {
                    lblMessage.Text = Constants.LessPoints;                   
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }
                txtpointsReddeem.Text = string.Empty;
                chkAgree.Checked = false;

            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;                
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Enable/Diable control
        /// </summary>
        private void EnableDisableControl(bool CtrlState)
        {
            txtpointsReddeem.Enabled = CtrlState;
            chkAgree.Enabled = CtrlState;
        }



        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {
            btnSubmit.Attributes.Add("class", "button");
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();

            int pendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), DEFAULT_STATUS);
            if (pendingCount > 0)
            {
                btnSubmit.Enabled = false;
                btnSubmit.Attributes.Add("class", "buttonDisble");
                EnableDisableControl(false);
                return false;
            }
            else
            {
                btnSubmit.Enabled = true;
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

                CH_CardDTO chdto = new CH_CardDTO();
                CardManager cm = new CardManager();
                chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Cardnumber });
                if (chdto != null)
                {

                    string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                    string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                    lblCardHolder.Text = firstName + " " + lastName;
                }
            }
        }

        /// <summary>
        /// Sets the Bonus Point.
        /// </summary>
        /// <remarks></remarks>
        private void SetBonuspoint()
        {
            string EarnedPts = string.Empty;
            string accountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();

            lblEarnedPoints.Text = "0";
            if (accountNumber != "")
            {
                EarnedPts = new AccountSummaryManager().GetBonusPoints(accountNumber);
                if (!string.IsNullOrEmpty(EarnedPts))
                    lblEarnedPoints.Text = EarnedPts;
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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.BonusPointRedemptionTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", lblCreditCardNumber.Text);
                bodyString.Replace("@@Bonuspts", txtpointsReddeem.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
                if (IsMailSent)
                {
                    
                    LblSuccessMessage.Text = "Your Request for Bonus Point Redemption has been successfully registered";
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

        #endregion
    }
}