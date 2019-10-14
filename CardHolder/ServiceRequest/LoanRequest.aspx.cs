using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
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
    public partial class LoanRequest : PageBase
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        string accountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
        string strCardNumber = CardHolderManager.GetLoggedInUser().credit_card_number.Decrypt();


        /// <summary>
        /// Gets or sets Total record count of user list
        /// </summary>
        /// <value>The record count.</value>
        /// <remarks></remarks>
        public int RecordCount
        {
            // Gets record count
            get { return this.ViewState["RecordCount"] == null ? 0 : Convert.ToInt32(this.ViewState["RecordCount"].ToString()); }

            // Sets record count
            set { this.ViewState["RecordCount"] = value; }
        }

        private static List<CH_EMI_Request_DTO> lstEMIDTO;
        #endregion

        #region Page Events
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
                        if (accountNumber != "")
                        {
                            lblSuccessMsg.Visible = false;
                            BindGridLoanTransactions(accountNumber);
                            Loadterms();
                            getLoanChargesOnLoad();
                            SetLoanInterestRate();
                        }
                    }
                }
            }
            lblMessage.Text = "";
            Pager1.Visible = false;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "BOBCard", "$(document).ready(function() {GridHeaderRowSelect();});", true);
        }

        /// <summary>
        /// Page pre render event to set pager item count
        /// </summary>
        /// <param name="e">event argument</param>
        /// <remarks></remarks>
        protected override void OnPreRender(EventArgs e)
        {
            this.Pager1.ItemCount = this.RecordCount;
            base.OnPreRender(e);
        }
        #endregion

        #region Events


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
                string EMI = Request.Form[txtEMI.UniqueID].ToString();
                if (string.IsNullOrWhiteSpace(EMI) || string.IsNullOrEmpty(EMI) || Convert.ToDouble(EMI) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.SelectTxn + "');", true);
                    BindGridLoanTransactions(accountNumber);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Getamount();", false);
                CHRequestDetailManager crdm = new CHRequestDetailManager();

                string strInterest = HttpContext.Current.Request.Form[txtInterest.UniqueID];
                string strEMIMonths = HttpContext.Current.Request.Form[txttermInMonth.UniqueID];

                if(string.IsNullOrEmpty(strInterest))
                {
                    strInterest = txtInterest.Text;
                }

                if(string.IsNullOrEmpty(strInterest))
                {
                    strEMIMonths = txttermInMonth.Text;
                }

                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                 {
                     Request_Dt = DateTime.Now,
                     CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                     RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                     IP_Address = Request.UserHostAddress,
                     Loan_Principal_Amt = Convert.ToDecimal(hdnIntTot.Value),
                     Loan_Terms = Convert.ToInt32(strEMIMonths),
                     Loan_Amount = Convert.ToDecimal(hdnEMI.Value),
                     Loan_InterestRate =  Convert.ToDecimal(strInterest),
                     Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                     Created_dt = DateTime.Now,
                     Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                 });



                //System.Collections.ArrayList[] = 

                int row_Count = gvLoantxn.Rows.Count;

                for (int i = 0; i < row_Count; i++)
                {
                    CheckBox chkRow = (CheckBox)gvLoantxn.Rows[i].FindControl("chkTransactions");
                    HiddenField HdnLoanId = (HiddenField)gvLoantxn.Rows[i].FindControl("hdnLoanOracleId");

                    if (chkRow.Checked)
                    {
                        crdm.SaveEMIRequestDtl(new CH_EMI_Request_DTO()
                        {
                            Creditcard_acc_number = accountNumber.Encrypt(),
                            Oracle_EMI_Id = HdnLoanId.Value,
                            EMI_Loan_Type = "L",
                            Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                            Created_dt = DateTime.Now,
                            IP_Address = Request.UserHostAddress,
                        });
                    }
                }
                Mailfunction(RequestDtlID);
                chkAgree.Checked = false;
                BindGridLoanTransactions(accountNumber);

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
            BindGridLoanTransactions(accountNumber);
            Loadterms();
        }

        /// <summary>
        /// Binds the grid unbilled transactions.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void BindGridLoanTransactions(string accountNumber)
        {
            lblMessage.Text = "";
            int pRecordCount = 0;
            int SkipCount = (this.Pager1.CurrentIndex - 1) * this.Pager1.PageSize;

            List<CH_LOAN_TransactionsDTO> lst = new List<CH_LOAN_TransactionsDTO>();
            TransactionManager tm = new TransactionManager();
            CHRequestDetailManager cdm = new CHRequestDetailManager();
            lst = tm.GetTransactionsForLoan(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber);

            if (lst != null)
            {
                if (lst.Count() > 0)
                {
                    lblSuccessMsg.Visible = true;
                    lblSuccessMsg.Text = Constants.LoanSuccess + lst[0].PreApproved_Limit.ToString() + " )";
                    lblSuccessMsg.CssClass = "msgsuccess";
                    btnSubmit.Visible = true;
                    btndisabled.Visible = false;
                    btnReset.Visible = true;
                    gridheader.Visible = true;
                    RecordCount = pRecordCount;
                    Pager1.Visible = RecordCount > this.Pager1.PageSize;
                    gvLoantxn.DataSource = lst;

                    lstEMIDTO = cdm.GetEMILoanRequests(accountNumber.Encrypt(), "L");
                    gvLoantxn.DataBind();
                }
                else
                    Reset();
            }
            else
                Reset();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Reset()
        {
            btnSubmit.Visible = false;
            btnReset.Visible = false;
            btndisabled.Visible = true;
            gridheader.Visible = false;
            Pager1.Visible = false;
            gvLoantxn.DataSource = null;
            gvLoantxn.DataBind();
            lblSuccessMsg.Visible = false;
            lblMessage.Text = Constants.NoLoan;
            lblMessage.CssClass = "error";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.NoLoan + "');", true);
        }


        /// <summary>
        /// Handles the RowDataBound event of the gvLoantxn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void gvLoantxn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnLoanId = (HiddenField)e.Row.FindControl("hdnLoanOracleId");

                foreach (var item in lstEMIDTO)
                {
                    if (item.Oracle_EMI_Id == HdnLoanId.Value)
                    {
                        e.Row.Enabled = false;
                        CheckBox chk = (CheckBox)e.Row.FindControl("chkTransactions");
                        chk.Visible = false;
                        e.Row.Cells[0].Text = "Loan Requested";
                        e.Row.Cells[0].Font.Italic = true;
                        btnSubmit.Visible = false;
                        btnReset.Visible = false;
                        btndisabled.Visible = true;
                    }
                    else
                    {
                        btnSubmit.Visible = true;
                        btnReset.Visible = true;
                        btndisabled.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Pager command
        /// </summary>
        /// <param name="sender">sender control</param>
        /// <param name="e">event argument</param>
        /// <remarks></remarks>
        protected void pager_Command(object sender, CommandEventArgs e)
        {
            this.Pager1.CurrentIndex = Convert.ToInt32(e.CommandArgument.ToString());
            this.BindGridLoanTransactions(accountNumber);
        }

        #endregion

        #region private method

        /// <summary>
        /// Loadtermses this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Loadterms()
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            List<DropDown_HdrDTO> list = dhm.SearchDllHeader("Loan_Terms").ToList();
            if (list.Count > 0)
            {
                ddlterms.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlterms.DataTextField = "Description";
                ddlterms.DataValueField = "DropDown_Dtl_Id";
                ddlterms.DataBind();
            }
            else
            {
                lblMessage.Text = Constants.termNotfound;
            }
        }

        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            string CardHolderName = CardHolderManager.GetLoggedInUser().CH_Card.Embossed_Name;
            string Email = CardHolderManager.GetLoggedInUser().CH_Card.EMAIL_ID;
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string EMAIL_Subject = ConfigurationManager.AppSettings["REQUEST_EMAIL_SUBJECT"].ToString();

            string strInterest = HttpContext.Current.Request.Form[txtInterest.UniqueID];
            string strEMIMonths = HttpContext.Current.Request.Form[txttermInMonth.UniqueID];

            if (string.IsNullOrEmpty(strInterest))
            {
                strInterest = txtInterest.Text;
            }

            if (string.IsNullOrEmpty(strInterest))
            {
                strEMIMonths = txttermInMonth.Text;
            }

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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.LoanRequestTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@Amount", hdnIntTot.Value);
                bodyString.Replace("@@terms", strEMIMonths);
                bodyString.Replace("@@Rate", strInterest);
                bodyString.Replace("@@EMI", hdnEMI.Value);
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

        private void getLoanChargesOnLoad()
        {
            double dbLoanCharges = 0;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = strCardNumber });
            if (cDTO != null)
            {
                dbLoanCharges = cDTO.LOAN_PROCESSING_FEES;
            }

            lblCharge.Text = Convert.ToString(dbLoanCharges);
        }

        private void SetLoanInterestRate()
        {
            string strLoanInterest = "10.25";
            string strLoanTerm = "3";
            string termValue = ddlterms.SelectedValue;

            if (string.IsNullOrEmpty(termValue.Trim()))
                termValue = "0";

            int intermValue = Convert.ToInt32(termValue);
            DropdownHdrManager dhm = new DropdownHdrManager();
            string strValue = dhm.GetValueFromDLLDetailsById(intermValue);

            if (!string.IsNullOrEmpty(strValue))
            {
                string[] strSplitValue = strValue.Split(' ');
                if (strSplitValue.Count() > 1)
                {
                    strLoanTerm = strSplitValue[0].Trim();
                    strLoanInterest = strSplitValue[1].Replace("(", "").Replace(")", "").Replace("%", "").Trim();
                }
            }
            txttermInMonth.Text = strLoanTerm;
            txtInterest.Text = strLoanInterest;
        }
        #endregion

        #region WebMethod

        /// <summary>
        /// Gets the last ATM pin details.
        /// </summary>
        /// <param name="CardNumber">The card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [WebMethod]
        public static string GetLoanCharges()
        {
            string dbLoanCharges = "0";
            string strCardNumber = CardHolderManager.GetLoggedInUser().credit_card_number.Decrypt();
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = strCardNumber });
            if (cDTO != null)
            {
                dbLoanCharges = Convert.ToString(cDTO.LOAN_PROCESSING_FEES);
            }

            return dbLoanCharges;
        }

        [WebMethod]
        public static string GetLoanInterestRate(string termValue)
        {
            string strLoanInterest = "10.25";
            string strLoanTerm = "3";
            if (string.IsNullOrEmpty(termValue.Trim()))
                termValue = "0";

            int intermValue = Convert.ToInt32(termValue);
            DropdownHdrManager dhm = new DropdownHdrManager();
            string strValue = dhm.GetValueFromDLLDetailsById(intermValue);

            if (!string.IsNullOrEmpty(strValue))
            {
                string[] strSplitValue = strValue.Split(' ');
                if (strSplitValue.Count() > 1)
                {
                    strLoanTerm = strSplitValue[0].Trim();
                    strLoanInterest = strSplitValue[1].Replace("(", "").Replace(")", "").Replace("%", "").Trim();
                }
            }

            return strLoanTerm + "," + strLoanInterest;
        }

        #endregion
    }
}