using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
    public partial class EMIRequest : PageBase
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        string accountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
        string strCardNumber = CardHolderManager.GetLoggedInUser().credit_card_number.Decrypt();
        public List<CH_UnbilledUnsettled_TransactionsDTO> lst = new List<CH_UnbilledUnsettled_TransactionsDTO>();
        public int EMIRequestCount = 0;
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
                            BindGridUnbilledTransactions(accountNumber);
                        Loadterms();
                        getEMIChargesOnLoad();
                        SetEmiInterestRate();                       
                    }
                    ShowEMIDiv();
                }
            }
            //lblMessage.Text = "";
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
                    BindGridUnbilledTransactions(accountNumber);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Getamount();", false);
                CHRequestDetailManager crdm = new CHRequestDetailManager();
                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                 {
                     Request_Dt = DateTime.Now,
                     CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                     RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                     IP_Address = Request.UserHostAddress,
                     EMI_Principal_Amt = Convert.ToDecimal(hdnIntTot.Value),
                     EMI_Terms = Convert.ToInt32(ddlterms.SelectedItem.Text),
                     EMI_Amount = Convert.ToDecimal(hdnEMI.Value),
                     EMI_InterestRate = Convert.ToDecimal(txtInterest.Text),
                     Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                     Created_dt = DateTime.Now,
                     Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                 });



                //System.Collections.ArrayList[] = 

                int row_Count = lstViewCardStatement.Items.Count;

                for (int i = 0; i < row_Count; i++)
                {
                    CheckBox chkRow = (CheckBox)lstViewCardStatement.Items[i].FindControl("chkTransactions");
                    HiddenField HdnEMIId = (HiddenField)lstViewCardStatement.Items[i].FindControl("hdnEMIOracleId");

                    if (chkRow.Checked)
                    {
                        crdm.SaveEMIRequestDtl(new CH_EMI_Request_DTO()
                        {
                            Creditcard_acc_number = accountNumber.Encrypt(),
                            Oracle_EMI_Id = HdnEMIId.Value,
                            Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                            Created_dt = DateTime.Now,
                            EMI_Loan_Type = "E",
                            IP_Address = Request.UserHostAddress,
                        });
                    }
                }



                Mailfunction(RequestDtlID);              
                BindGridUnbilledTransactions(accountNumber);
                ShowEMIDiv();

            }
            catch (Exception ex)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
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
            BindGridUnbilledTransactions(accountNumber);
            Loadterms();
            ClearData();
        }

        private void ClearData()
        {
            lblamount.Text = "0.00";
            txtEMI.Text = string.Empty;
        }

        /// <summary>
        /// Binds the grid unbilled transactions.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void BindGridUnbilledTransactions(string accountNumber)
        {
            lblMessage.Text = "";
            int pRecordCount = 0;
            int SkipCount = (this.Pager1.CurrentIndex - 1) * this.Pager1.PageSize;
            int TransactionType = 1;
           
            TransactionManager tm = new TransactionManager();
            CHRequestDetailManager cdm = new CHRequestDetailManager();
            lst = tm.GetUnbilledTransactionsForEMI(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, TransactionType);

            if (lst != null)
            {
                if (lst.Count() > 0)
                {
                    btnSubmit.Visible = true;
                    btndisabled.Visible = false;
                    btnReset.Visible = true;
                    gridheader.Visible = true;
                    RecordCount = pRecordCount;
                    Pager1.Visible = RecordCount > this.Pager1.PageSize;
                    lstViewCardStatement.DataSource = lst;

                    lstEMIDTO = cdm.GetEMILoanRequests(accountNumber.Encrypt(),"E");
                    lstViewCardStatement.DataBind();
                }
                else
                    Reset();
            }
            else
                Reset();
        }

       
        public void ShowEMIDiv()
        {
            if (lst != null && lst.Count() > 0)
            {
                if (EMIRequestCount == lst.Count())
                {
                    divEMIWithDesign.Visible = false;
                    CheckBox chkAll = (CheckBox)lstViewCardStatement.FindControl("chkAllSelect");
                    chkAll.Visible = false;
                }
                else
                {
                    divEMIWithDesign.Visible = true;
                    CheckBox chkAll = (CheckBox)lstViewCardStatement.FindControl("chkAllSelect");
                    chkAll.Visible = true;
                }
            }
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
            lstViewCardStatement.DataSource = null;
            lstViewCardStatement.DataBind();            
            lblMessage.Text = Constants.NoEmi;
            DivMessage.Attributes.CssStyle.Add("display", "block");
            divEMIWithDesign.Visible = false;
        }


        /// <summary>
        /// Handles the RowDataBound event of the gvEMItxn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void lstViewCardStatement_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //List<CH_EMI_Request_DTO> lstEMIDTO = new List<CH_EMI_Request_DTO>();
            //CHRequestDetailManager cdm = new CHRequestDetailManager();
            //lstEMIDTO = cdm.GetEMIRequests(accountNumber.Encrypt());
            
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField HdnEMIId = (HiddenField)e.Item.FindControl("hdnEMIOracleId");

                foreach (var item in lstEMIDTO)
                {
                    if (item.Oracle_EMI_Id == HdnEMIId.Value)
                    {
                        //e.row.enable = false;
                        //e.Item.Visible = false;
                        CheckBox chk = (CheckBox)e.Item.FindControl("chkTransactions");
                        chk.Visible = false;
                        //e.Item.Cells[0].Text = "EMI Requested";
                        //e.Row.Cells[0].Font.Italic = true;
                        Label lblEMIRequested = (Label)e.Item.FindControl("lblEMIRequested");
                        lblEMIRequested.Visible = true;
                        lblEMIRequested.Text = "EMI Requested";
                        lblEMIRequested.Font.Italic = true;
                        btnSubmit.Visible = false;
                        btnReset.Visible = false;
                        btndisabled.Visible = true;
                        EMIRequestCount++;
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
            this.BindGridUnbilledTransactions(accountNumber);
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
            List<DropDown_HdrDTO> list = dhm.SearchDllHeader("EMI_Terms").ToList();
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
                DivMessage.Attributes.CssStyle.Add("display", "block");
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
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.EMIRequestTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@Amount", hdnIntTot.Value);
                bodyString.Replace("@@terms", ddlterms.SelectedItem.Text);
                bodyString.Replace("@@Rate", txtInterest.Text);
                bodyString.Replace("@@EMI", hdnEMI.Value);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/bob-logo.png");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
                if (IsMailSent)
                {
                    LblSuccessMessage.Text = "Your EMI Request for Unbilled transactions has been successfully registered";
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

        private void getEMIChargesOnLoad()
        {
            double dbEMICharges = 0;
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = strCardNumber });
            if (cDTO != null)
            {
                dbEMICharges = cDTO.EMI_PROCESSING_FEES;
            }

           
        }

        private void SetEmiInterestRate()
        {
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName("AnnualInterestRate");
            if (obj != null && obj.Parameter_ValueD != null)
                txtInterest.Text = Convert.ToString(obj.Parameter_ValueD);
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
        public static string GetEMICharges()
        {
            string dbEMICharges = "0";
            string strCardNumber = CardHolderManager.GetLoggedInUser().credit_card_number.Decrypt();
            CH_CardDTO cDTO = new CH_CardDTO();
            CardManager cm = new CardManager();
            cDTO = cm.GetVariousCardFees(new CH_CardDTO() { card_number = strCardNumber });
            if (cDTO != null)
            {
                dbEMICharges = Convert.ToString(cDTO.EMI_PROCESSING_FEES);
            }

            return dbEMICharges;
        }

        #endregion       

      

    }
}