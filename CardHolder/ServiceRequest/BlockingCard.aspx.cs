using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.Enums;
using System.Web.Script.Serialization;
using NLog;
namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class BlockingCard :PageBase
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
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
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().Replace(" ", "+").DecryptURL();
                    if (!IsPostBack)
                    {
                        LoadReasons();
                        LoadCardsinDDL();
                        loadCreditCardsName();

                    }
                }
            }
            

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
                hideReason.Value = ddlReasons.SelectedItem.Text;
                hideCreditCardNumber.Value = ddlcardlist.SelectedValue;
                ddlcardlist.Enabled = false;
                ddlReasons.Enabled = false;
                Session["Card_Num"] = ddlcardlist.SelectedValue.Encrypt();
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
                    IP_Address = Request.UserHostAddress,
                    RequestReason_Id = Convert.ToInt64(ddlReasons.SelectedValue),
                    HotlistingCardNumber = ddlcardlist.SelectedValue.Encrypt(),
                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    Created_dt = DateTime.Now,
                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                });


                System.Threading.Thread.Sleep(1000);
                CreateRequest(RequestDtlID);
                btnSubmit.Style.Remove("data-loading");
                ddlReasons.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                string strErrpr = ex.Message;
                lblErrorReasons.Text = Constants.GeneralErrorMessage;
            }
        }

        private void CreateRequest(long RequestDtlID)
        {

            string result = string.Empty;
            Helper objHelper = new Helper();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string TransRefNo = objHelper.RandomDigits();
            CardBlockRequest objCardBlockRequest = new CardBlockRequest()
            {
                TxnType = TranscationType.CB.ToString(),
                CardNumber = Convert.ToString(hideCreditCardNumber.Value),
                TransRefNo = TransRefNo,
                TransDateTime = String.Format("{0:MM/dd/yyyy}", DateTime.Now),
                Flag = "Y"
            };
            string jsondata = js.Serialize(objCardBlockRequest);
            result = objHelper.GetResponse(jsondata);
            logger.Info("Jetty Server Response String:" + result);
            dynamic objResult = null;
            if (result == null)
            {
                LblErrorMessage.Text = Constants.msg;
                DivERROR.Attributes.CssStyle.Add("display", "block");                
            }
            else
            {
                objResult = js.Deserialize<dynamic>(result);
                DisplayMessage(objResult, RequestDtlID); ;
            }

            ddlcardlist.Enabled = false;
            ddlReasons.Enabled = false;
            chkAgree.Checked = false;
            chkAgree.Enabled = false;
            //btnSubmit.Enabled = false;
            UpdateStatus(objResult, RequestDtlID);
            SaveAuditLog(TransRefNo, objResult);
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
                TxnType = TranscationType.CB.ToString(),
                Credit_card_number = Convert.ToString(hideCreditCardNumber.Value),
                RequestReason = Convert.ToString(hideReason.Value),
                TxnReferenceNo = TransRefNo,
                ResponseStatus = responseStatus,
                Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                Created_dt = DateTime.Now,
                BankRefNo = Session["crAccNum"].ToString(),
                IP_Address = Request.UserHostAddress
            });

        }

        private void DisplayMessage(dynamic result, long Request_Dtl_Id)
        {
            if (result != null)
            {
                if (result["RespCode"] == "000")
                {
                    Mailfunction(Request_Dtl_Id);   
                    LblSuccessMessage.Text = "Card has been successfully blocked.";
                    DivSuccess.Attributes.CssStyle.Add("display", "block");                   
                }
                else if (result["RespCode"] == "204")
                {
                    LblErrorMessage.Text = Constants.ErrorCode204;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                }
                else
                {
                    LblErrorMessage.Text = Constants.msg;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                }
            }
            else
            {
                LblErrorMessage.Text = Constants.msg;
                DivERROR.Attributes.CssStyle.Add("display", "block");
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
        #endregion

        #region Private methods

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
                {
                    string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                    string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                    lblCardHolder.Text = firstName + " " + lastName;
                }                    
            }
        }


        /// <summary>
        /// Loads the cardsin DDL.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCardsinDDL()
        {
            string CR_acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            Session["crAccNum"] = CR_acc_num;
            List<CH_CardDTO> lstCardnumbers = new CardManager().GetCardList(CR_acc_num);

            if (lstCardnumbers != null && lstCardnumbers.Count > 0)
            {
                ddlcardlist.DataSource = lstCardnumbers;
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
            string CreditcardNumber = ddlcardlist.SelectedItem.Text;
            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.BlockingCardTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", CreditcardNumber);
                bodyString.Replace("@@Reason", ddlReasons.SelectedItem.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/bob-logo.png");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent =  SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
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

        #endregion

    }
}