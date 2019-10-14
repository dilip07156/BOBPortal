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
    public partial class PreserveStatementRequest : PageBase
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        /// <summary>
        /// 
        /// </summary>
        int CHPreservedStatementyrs = 1;
        /// <summary>
        /// 
        /// </summary>
        string CHPreservedStatementYearly = "PreservedStatementYearly";
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
                        loadCreditCards();
                        PreservedStmntYear();
                        EnableDisalbeControl(true);
                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                            return;
                        }
                        if (btnSubmit.Visible == true && btndisable.Visible == false)
                        {
                            IsAccountEligible();
                        }
                    }
                }
                else
                {
                    btnSubmit.Disabled = true;
                }
            }
        }

        #endregion

        #region ClickEvent

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
                    ClearControls();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                    return;
                }

                string mode = "";
                if (chkEStatement.Checked == false & chkHardCopy.Checked == false)
                {
                    lblMessage.Text = Constants.selectStmntType;
                    return;
                }
                else if (chkEStatement.Checked == true & chkHardCopy.Checked == true)
                {
                    mode = "B";
                }
                else if (chkEStatement.Checked == true & chkHardCopy.Checked == false)
                {
                    mode = "E";
                }
                else if (chkEStatement.Checked == false & chkHardCopy.Checked == true)
                {
                    mode = "H";
                }

                if (month.SelectedValue == "0")
                {
                    lblMessage.Text = Constants.selectmonth;
                    return;
                }
                if (ddlyear.SelectedValue == "0")
                {
                    lblMessage.Text = Constants.selectyear;
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
                    Mode_Send_Statment = mode,
                    Statement_month = month.SelectedValue,
                    Statement_year = ddlyear.SelectedValue,
                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                });
                Mailfunction(RequestDtlID);
                ClearControls();
                //lblMessage.Text = "Preserve Statement Request has been sent";
                //lblMessage.CssClass = "msgsuccess";
            }
            catch (Exception)
            {
                lblMessage.Text = Constants.GeneralErrorMessage;
                lblMessage.CssClass = "error";
            }
        }

        #endregion

        #region PrivateMethods


        /// <summary>
        /// Enable/disable control
        /// </summary>        
        private void EnableDisalbeControl(bool ctrlState)
        {
            month.Enabled = ctrlState;
            ddlyear.Enabled = ctrlState;
            chkEStatement.Enabled = ctrlState;
            chkHardCopy.Enabled = ctrlState;

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
                btnSubmit.Visible = false;
                btndisable.Visible = true;
                EnableDisalbeControl(false);
                return false;
            }
            else
            {
                btnSubmit.Visible = true;
                btndisable.Visible = false;
                EnableDisalbeControl(true);
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
                lblCardHolder.Text = card.Embossed_Name;
                lblCreditCardAccNumber.Text = card.Cr_Account_Nbr;
            }
            else
            {
                btnSubmit.Disabled = true;
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        /// <remarks></remarks>
        private void ClearControls()
        {
            chkHardCopy.Checked = false;
            chkEStatement.Checked = false;
            month.SelectedValue = "0";
            ddlyear.SelectedValue = "0";
            chkAgree.Checked = false;
        }

        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            string Charge = CH_CardDTO.FeeCharge;
            //string Charge = Request.Form[hdncharge.UniqueID].ToString();

            string mode = string.Empty;
            string AccNum = CardHolderManager.GetLoggedInUser().CH_Card.Cr_Account_Nbr;
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


            if (chkEStatement.Checked == true && chkHardCopy.Checked == true)
            {
                mode = chkEStatement.Text + " & " + chkHardCopy.Text;
            }

            else if (chkEStatement.Checked == true && chkHardCopy.Checked == false)
            {
                mode = chkEStatement.Text;
            }

            else if (chkEStatement.Checked == false && chkHardCopy.Checked == true)
            {
                mode = chkHardCopy.Text;
            }

            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.PreserveStatementRequestTemplatepath));
                bodyString.Replace("@@CardHolderName", lblCardHolder.Text);
                bodyString.Replace("@@Accnum", AccNum);
                bodyString.Replace("@@month", month.SelectedItem.Text);
                bodyString.Replace("@@year", ddlyear.SelectedItem.Text);
                bodyString.Replace("@@mode", mode);
                bodyString.Replace("@@Rupee", Charge);
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
        /// Preserveds the STMNT year.
        /// </summary>
        /// <remarks></remarks>
        private void PreservedStmntYear()
        {
            List<string> Last10Yrs = new List<string>();
            int PreservedStmntYears = GetyearsForPreserveStatementRequest(CHPreservedStatementYearly);
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - PreservedStmntYears; i--)
            {
                string j = Convert.ToString(i);
                Last10Yrs.Add(j);
            }
            //databind here

            ddlyear.DataSource = Last10Yrs;
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("Year", "0"));
        }

        /// <summary>
        /// Gets the years for Preserved statement.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int GetyearsForPreserveStatementRequest(string keyValue)
        {
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(keyValue);
            if (obj != null && obj.Parameter_ValueN != null)
                CHPreservedStatementyrs = Convert.ToInt32(obj.Parameter_ValueN);
            return CHPreservedStatementyrs;
        }

        /// <summary>
        /// Determines whether [is account eligible].
        /// </summary>
        /// <remarks></remarks>
        private void IsAccountEligible()
        {
            string EligibleAccount = "";
            CardManager cm = new CardManager();
            string Acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (Acc_num != "")
                EligibleAccount = cm.CardEligibleForPreserveStmnt(Acc_num);
            if (EligibleAccount == "")
            {
                btnSubmit.Visible = false;
                btndisable.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.NotEligibleForPreserve + "');", true);
            }
            else
            {
                btnSubmit.Visible = true;
                btndisable.Visible = false;
            }
        }

        #endregion

        #region Webmethods

        /// <summary>
        /// Gets the last STMNT details.
        /// </summary>
        /// <param name="AccountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string GetLastStmntDetails(string AccountNumber)
        {
            string ReqDtls = "";
            //string ATMPinregDate;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            if (AccountNumber != "")
            {
                cDTO = cm.GetPreserveStmntDetails(new CH_CardDTO() { Cr_Account_Nbr = AccountNumber });
                if (cDTO != null)
                {
                    string StmntDate = GeneralMethods.FormatDate(Convert.ToDateTime(cDTO.PRESERVE_STMNT_GENERATION_DATE).ToString()); ;
                    ReqDtls = cDTO.FOR_MONTH + "," + StmntDate + "," + cDTO.PRESERVE_STMNT_REQUEST_NUMBER;
                    //string FOR_MONTH = "July";
                    //string FOR_MONTH1 = "11/11/2011";
                    //string FOR_MONTH2 = "855679521";

                    //ReqDtls = FOR_MONTH + "," + FOR_MONTH1 + "," + FOR_MONTH2;

                }
            }
            return ReqDtls;
        }

        /// <summary>
        /// Statements the charges.
        /// </summary>
        /// <param name="estatement">The estatement.</param>
        /// <param name="hardcopy">The hardcopy.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string StatementCharges(string estatement, string hardcopy)
        {
            string FeeCharge = "";
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            // string Cardnumber = CardHolderManager.GetLoggedInUser().credit_card_number.Decrypt();
            string Cardnumber = CardHolderManager.GetLoggedInUser().CH_Card.card_number;// credit_card_number.Decrypt();
            if (Cardnumber != "")
            {
                cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = Cardnumber });
                if (cDTO != null)
                {

                    if (estatement == "1" && hardcopy == "1")
                    {
                        FeeCharge = Convert.ToString(cDTO.RESTATEMENT_THRU_EMAIL_CHARGES + cDTO.STMT_REG_AMT);
                    }
                    else if (estatement == "1" && hardcopy == "0")
                    {
                        FeeCharge = Convert.ToString(cDTO.RESTATEMENT_THRU_EMAIL_CHARGES);
                    }
                    else if (estatement == "0" && hardcopy == "1")
                    {
                        FeeCharge = Convert.ToString(cDTO.STMT_REG_AMT);
                    }

                    // ScriptManager.RegisterStartupScript(this, GetType(), "displayPOpup", "OpenChargePopup();", true);
                }
            }
            CH_CardDTO.FeeCharge = FeeCharge;
            return FeeCharge;
        }

        #endregion




    }
}