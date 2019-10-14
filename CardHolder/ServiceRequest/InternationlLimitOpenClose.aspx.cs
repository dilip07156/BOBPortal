using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.Enums;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class InternationlLimitOpenClose : PageBase
    {
        #region variable
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().Replace(" ", "+").DecryptURL();
                    if (!IsPostBack)
                    {
                        LoadCardsinDDL();
                        loadCreditCardsName();
                        LoadInternationalLimitAmount();
                    }
                }
            }

        }


        #endregion

        #region Submit Detail
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                hideCreditCardNumber.Value = ddlcardlist.SelectedValue;
                hideInternationalLimit.Value = internationalUsage.Checked ? "Active" : "Deactive";
                ddlcardlist.Enabled = false;
                internationalUsage.Enabled = false;
                txtAmount.Attributes.CssStyle.Add("disabled", "disabled");
                //btnSave.Enabled = false;
                string strInternationalLimit = string.Empty;
                Session["Card_Num"] = ddlcardlist.SelectedValue.Encrypt();
                string crAccNum = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
                Session["crAccNum"] = crAccNum;
                CHRequestDetailManager crdm = new CHRequestDetailManager();
                string hotlistingCardNumber = hideCreditCardNumber.Value;
                Int32 principalAmount = 0;
                string Flag = "I";
                string requestflag = string.Empty;
                LblActivateDeactivateMsg.Text = "";
                if (internationalUsage.Checked == true)
                {
                    requestflag = "Active";
                    principalAmount = Convert.ToInt32(txtAmount.Value);
                }
                else
                {
                    LblAmoutErrorMessage.Attributes.CssStyle.Add("display", "none");
                    principalAmount = 0;
                    requestflag = "Deactive";
                }
                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                {
                    Request_Dt = DateTime.Now,
                    CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                    IP_Address = Request.UserHostAddress,
                    HotlistingCardNumber = hotlistingCardNumber,
                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    Created_dt = DateTime.Now,
                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString(),
                    Loan_Principal_Amt = principalAmount,
                    RequestFlag = requestflag
                });
                System.Threading.Thread.Sleep(1000);
                CreateRequest(RequestDtlID, principalAmount, Flag, hotlistingCardNumber, requestflag);
                btnSave.Style.Remove("data-loading");
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Attributes.CssStyle.Add("display", "block");
                btnReset.Visible = false;
                divReset.Attributes.CssStyle.Add("display", "none");
                lblTxtRuppesMessage.Attributes.CssStyle.Add("display", "none");
                txtAmount.Value = string.Empty;
                txtAmount.Disabled = false;

                if (internationalUsage.Checked == false)
                {
                    LblActivateDeactivateMsg.Text = "Please check the Checkbox to activate International Usage";
                    LblActivateDeactivateMsg.Attributes.CssStyle.Add("CssClass", "error");
                    internationalUsage.Enabled = true;
                }
                else
                {
                    LblActivateDeactivateMsg.Text = "";
                    internationalUsage.Enabled = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CreateRequest(long RequestDtlID, int principalAmount, string Flag, string hotlistingCardNumber, string requestflag)
        {
            string result = string.Empty;
            Helper objHelper = new Helper();
            JavaScriptSerializer js = new JavaScriptSerializer();
            //hideCreditCardNumber.Value = "5344440000003865";
            string TransRefNo = objHelper.RandomDigits();
            InternationlLimitRequest objInternationalRequest = new InternationlLimitRequest()
            {
                TxnType = TranscationType.IL.ToString(),
                CardNumber = Convert.ToString(hideCreditCardNumber.Value),
                TransRefNo = TransRefNo,
                TransDateTime = Regex.Replace(Convert.ToString(DateTime.Now), @"[^0-9a-zA-Z]+", ""),
                Amount = principalAmount,
                Flag = Flag
            };
            string jsondata = js.Serialize(objInternationalRequest);
            result = objHelper.GetResponse(jsondata);
            logger.Info("Jetty Server Response String:" + result);
            dynamic objResult = null;
            if (result == null)
            {
                LblErrorMessage.Text = Constants.msg;
                DivERROR.Attributes.CssStyle.Add("display", "block");
                txtAmount.Disabled = true;
                internationalUsage.Enabled = false;
            }
            else
            {
                objResult = js.Deserialize<dynamic>(result);
                DisplayMessage(objResult, RequestDtlID, requestflag);
            }
            UpdateStatus(objResult, RequestDtlID, principalAmount, hotlistingCardNumber, requestflag);
            SaveAuditLog(TransRefNo, objResult, hotlistingCardNumber);
        }

        private void DisplayMessage(dynamic result, long Request_Dtl_Id, string requestflag)
        {
            if (result != null)
            {
                if (result["RespCode"] == "000")
                {
                    Mailfunction(Request_Dtl_Id, requestflag);
                    LblSuccessMessage.Text = "Request has been completed successfully";
                    DivSuccess.Attributes.CssStyle.Add("display", "block");
                    //string scriptText = "window.location='" + Request.ApplicationPath + "AccountSummary/AccountSummary.aspx'";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                }
                else if (result["RespCode"] == "204")
                {
                    LblErrorMessage.Text = Constants.ErrorCode204;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                    txtAmount.Disabled = true;
                    internationalUsage.Enabled = false;
                }
                else
                {
                    LblErrorMessage.Text = Constants.msg;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                    txtAmount.Disabled = true;
                    internationalUsage.Enabled = false;
                }
            }
            else
            {
                LblErrorMessage.Text = Constants.msg;
                DivERROR.Attributes.CssStyle.Add("display", "block");
                txtAmount.Disabled = true;
                internationalUsage.Enabled = false;
            }

        }

        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID, string requestflag)
        {
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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.InternationalLimitTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", ddlcardlist.SelectedItem.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@requestflag", requestflag);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/bob-logo.png");
                List<string> CCemail = new List<string>();
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
        /// Handles the SelectedIndexChanged event of the ddlcardlist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlcardlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreditCardsName();
            LoadInternationalLimitAmount();
        }

        /// <summary>
        /// This button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
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
        }
        #endregion

        #region Private Methods

        private void ClearControls()
        {
            Session.Abandon();
            Session.Clear();

        }


        /// <summary>
        /// Enable/disable control
        /// </summary>        
        private void EnableDisalbeControl(bool ctrlState)
        {
            ddlcardlist.Enabled = ctrlState;
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
                ddlcardlist.DataSource = cm.GetAllCardsForATMPinReg(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
                ddlcardlist.DataTextField = "MASK_CARD_NUMBER";
                ddlcardlist.DataValueField = "CARD_NUMBER";
                ddlcardlist.DataBind();
            }
        }

        private void SaveAuditLog(string TransRefNo, dynamic objResult, string hotlistingCardNumber)
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
                TxnType = TranscationType.IL.ToString(),
                Credit_card_number = hotlistingCardNumber,
                TxnReferenceNo = TransRefNo,
                ResponseStatus = responseStatus,
                Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                Created_dt = DateTime.Now,
                BankRefNo = Session["crAccNum"].ToString(),
                IP_Address = Request.UserHostAddress
            });
        }

        private void UpdateStatus(dynamic result, long Request_Dtl_Id, int principalAmount, string hotlistingCardNumber, string requestFlag)
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
                Updated_dt = DateTime.Now,
                Updated_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                Created_dt= DateTime.Now,
                Request_Status = requestStatus,
                Loan_Principal_Amt = principalAmount,
                HotlistingCardNumber = hotlistingCardNumber,
                RequestFlag = requestFlag
            });
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
                        btnSave.Visible = false;
                        LblErrorMessage.Text = Constants.AccNotNormal;
                        DivERROR.Attributes.CssStyle.Add("display", "block");
                    }
                    else
                    {
                        btnSave.Visible = true;
                    }

                }
            }
            else
            {
                btnSave.Visible = false;
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.SomeProblem + "');", true);
                LblErrorMessage.Text = Constants.SomeProblem;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        /// <summary>
        /// This method is used for loading existing sttaus of Credit card ( Active/ Deactive)
        /// </summary>
        private void LoadInternationalLimitAmount()
        {
            lblMessage.Text = string.Empty;
            DivMessage.Attributes.CssStyle.Add("display", "none");
            string Card_number = ddlcardlist.SelectedValue;
            CardManager cm = new CardManager();
            CH_CardDTO chdto = new CH_CardDTO();
            string InternationalLimitStatus = string.Empty;
            //hideCreditCardNumber.Value = "5344440000003865";
            Int32? internationalLimitSqlDatabaseAmount = 0;
            string requestFlag = string.Empty;
            string internationalStatus = string.Empty;

            if (Card_number != "")
            {
                chdto = cm.GetCardDetailsForInternationalLimitByCardNumber(new CH_CardDTO() { card_number = Card_number });
                var chRequestDTO = GetInternationalLimitAmount();
                if (chRequestDTO != null)
                {
                    internationalLimitSqlDatabaseAmount = Convert.ToInt32(chRequestDTO.Loan_Principal_Amt);
                    requestFlag = chRequestDTO.RequestFlag;
                }
                internationalStatus = chRequestDTO != null ? chRequestDTO.Request_Status : string.Empty;
                if (chdto != null)
                {
                    hidelitInternationalMaxLimitAmount.Value = Convert.ToString(chdto.Credit_Limit);
                    if (internationalLimitSqlDatabaseAmount > 0)
                    {
                        if (chdto.LIMIT_PURCH_INT > 0)
                        {
                            if (internationalLimitSqlDatabaseAmount > 0)
                            {
                                if (internationalLimitSqlDatabaseAmount != chdto.LIMIT_PURCH_INT)
                                {
                                    txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount);
                                }
                                else if (internationalLimitSqlDatabaseAmount == chdto.LIMIT_PURCH_INT)
                                {
                                    txtAmount.Value = Convert.ToString(chdto.LIMIT_PURCH_INT);
                                }
                            }
                            else
                            {
                                txtAmount.Value = Convert.ToString(chdto.LIMIT_PURCH_INT);
                            }
                            InternationalLimitStatus = "Active";
                        }
                        else if ((chdto.LIMIT_PURCH_INT == null && chdto.LIMIT_PURCH_INT_resp == null))
                        {
                            if (internationalLimitSqlDatabaseAmount > 0)
                            {
                                txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount);
                            }
                            else
                            {
                                txtAmount.Value = Convert.ToString(chdto.Credit_Limit);
                            }
                            InternationalLimitStatus = "Active";
                        }
                        else if ((chdto.LIMIT_PURCH_INT == 0 && chdto.LIMIT_PURCH_INT_resp == 0))
                        {
                            if (internationalLimitSqlDatabaseAmount > 0)
                            {
                                txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount);
                            }
                            else
                            {
                                txtAmount.Value = Convert.ToString(chdto.Credit_Limit);
                            }
                            InternationalLimitStatus = "Active";
                        }
                        else
                        {
                            InternationalLimitStatus = "Deactive";
                            txtAmount.Value = "Not Avaiable";
                        }
                    }
                    else
                    {
                        if (internationalLimitSqlDatabaseAmount == 0 && requestFlag == "Deactive")
                        {
                            InternationalLimitStatus = "Deactive";
                        }
                        else
                        {

                            if (chdto.LIMIT_PURCH_INT > 0)
                            {
                                if (internationalLimitSqlDatabaseAmount > 0)
                                {
                                    if (internationalLimitSqlDatabaseAmount != chdto.LIMIT_PURCH_INT)
                                    {
                                        txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount);
                                    }
                                    else if (internationalLimitSqlDatabaseAmount == chdto.LIMIT_PURCH_INT)
                                    {
                                        txtAmount.Value = Convert.ToString(chdto.LIMIT_PURCH_INT);
                                    }
                                }
                                else
                                {
                                    txtAmount.Value = Convert.ToString(chdto.LIMIT_PURCH_INT);
                                }
                                InternationalLimitStatus = "Active";
                            }
                            else if ((chdto.LIMIT_PURCH_INT == null && chdto.LIMIT_PURCH_INT_resp == null))
                            {
                                if (internationalLimitSqlDatabaseAmount > 0)
                                {
                                    txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount);
                                }
                                else
                                {
                                    txtAmount.Value = Convert.ToString(chdto.Credit_Limit);
                                }
                                InternationalLimitStatus = "Active";
                            }
                            else if ((chdto.LIMIT_PURCH_INT == 0 && chdto.LIMIT_PURCH_INT_resp == 0))
                            {
                                if (internationalLimitSqlDatabaseAmount > 0)
                                {
                                    txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount);
                                }
                                else
                                {
                                    txtAmount.Value = Convert.ToString(chdto.Credit_Limit);
                                }
                                InternationalLimitStatus = "Active";
                            }
                            else
                            {
                                txtAmount.Attributes.CssStyle.Add("display", "block");//As discussed with Amar 11 Oct 2019
                                txtAmount.Value = "0"; //As discussed with Amar 11 Oct 2019
                            }
                            //else if ((chdto.LIMIT_PURCH_INT == 0 && chdto.LIMIT_PURCH_INT_resp == 1))
                            //{
                            //    lblMessage.Text = Constants.BlockedCard;
                            //    DivMessage.Attributes.CssStyle.Add("display", "block");
                            //    internationalUsage.Enabled = false;
                            //    internationalUsage.Checked = false;
                            //    txtAmount.Attributes.CssStyle.Add("display", "block");
                            //    txtAmount.Value = string.Empty;
                            //    txtAmount.Disabled = true;
                            //    lblTxtRuppesMessage.Attributes.CssStyle.Add("display", "none");
                            //}

                        }


                        if (internationalLimitSqlDatabaseAmount == 0 && internationalStatus == "Approved")
                        {
                            txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount) + " " + "(Deactivated)";
                            internationalUsage.Checked = false;
                            //txtAmount.Attributes.CssStyle.Add("display", "none");
                            LblActivateDeactivateMsg.Text = "Check the box to activate International Usage and Click on Reset Button.";
                            txtAmount.Disabled = true;
                        }


                    }
                    if (InternationalLimitStatus == "Active")
                    {
                        //R1.Checked = true;
                        internationalUsage.Checked = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "EnableControl();", true);
                        txtAmount.Attributes.CssStyle.Add("display", "block");
                        LblActivateDeactivateMsg.Text = "Uncheck the box to deactivate International Usage";
                        string number = Convert.ToDouble(txtAmount.Value).ToString();
                        //lblTxtRuppesMessage.Text = UrlHelper.ConvertToWords(number);
                        lblTxtRuppesMessage.Attributes.CssStyle.Add("display", "inline-block");
                        divReset.Attributes.CssStyle.Add("display", "inline-block");
                        txtAmount.Disabled = true;
                        var amount = txtAmount.Value;                                            
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "inWords", "inWords('" + amount + "')", true);                     

                    }
                    else if (InternationalLimitStatus == "Deactive")
                    {
                        internationalUsage.Checked = false;
                        txtAmount.Value = Convert.ToString(internationalLimitSqlDatabaseAmount) + " " + "(Deactivated)";
                        LblActivateDeactivateMsg.Text = "Check the box to activate International Usage and Click on Reset Button.";
                        //lblAmount.Attributes.CssStyle.Add("display", "none");
                        txtAmount.Disabled = true;
                        txtAmount.Attributes.CssStyle.Add("display", "block");
                    }
                }

            }
            else
            {
                btnSave.Visible = false;
                LblErrorMessage.Text = Constants.SomeProblem;
                DivERROR.Attributes.CssStyle.Add("display", "block");
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.SomeProblem + "');", true);
            }
        }



        //private CH_Request_DtlDTO GetInternationalLimitAmount()
        //{
        //    CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();
        //    CH_Request_DtlDTO objCH_Request_DtlDTO = new CH_Request_DtlDTO();
        //    objCH_Request_DtlDTO.CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id;
        //    objCH_Request_DtlDTO.RequestType_Id = Convert.ToInt32(hideRequestTypeId.Value);
        //    objCH_Request_DtlDTO.HotlistingCardNumber = ddlcardlist.SelectedValue;
        //    var internationalLimitDetails = objCHRequestDetailManager.GetInternationalLimitAmount(objCH_Request_DtlDTO);
        //    return internationalLimitDetails;
        //}

        private CH_Request_DtlDTO GetInternationalLimitAmount()
        {
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();
            var internationalLimitDetails = objCHRequestDetailManager.GetInternationalLimitAmount(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), ddlcardlist.SelectedValue);
            return internationalLimitDetails;
        }

        protected void CheckedChanged(object sender, EventArgs e)
        {
            if (internationalUsage.Checked == false)
            {
                divInternationalLimit.Attributes.CssStyle.Add("display", "none");
                LblAmoutErrorMessage.Attributes.CssStyle.Add("display", "none");
                btnSave.Attributes.CssStyle.Add("display", "block");
                btnReset.Visible = false;
                divReset.Attributes.CssStyle.Add("display", "none");
                lblTxtRuppesMessage.Attributes.CssStyle.Add("display", "none");
                LblActivateDeactivateMsg.Text = string.Empty;
                internationalUsage.Enabled = false;
            }
            else if (internationalUsage.Checked == true)
            {
                divInternationalLimit.Attributes.CssStyle.Add("display", "block");
                txtAmount.Disabled = false;
                txtAmount.Value = string.Empty;
                btnSave.Attributes.CssStyle.Add("display", "block");
                btnReset.Visible = false;
            }

        }
        #endregion
    }
}