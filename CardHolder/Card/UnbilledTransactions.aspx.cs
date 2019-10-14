using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder
{
    /// <summary>
    /// UnbilledTransactions
    /// </summary>
    /// <remarks></remarks>
    public partial class UnbilledTransactions : PageBase
    {
        #region Variables
/*
        string lblMerchantName = "lblMerchantName";
*/
        /// <summary>
        /// 
        /// </summary>
        public string sessionAccountNumber = "AccountNumber";
        /// <summary>
        /// 
        /// </summary>
        string unbilledtransactions = "unbilledtransactions";
        /// <summary>
        /// 
        /// </summary>
        string unsettledtransactions = "unsettledtransactions";
        /// <summary>
        /// 
        /// </summary>
        static string totalUnbilledAmount = "totalUnbilledAmount";
        /// <summary>
        /// 
        /// </summary>
        static string sessionCardStatementAmountDue = "AmountDue";

        public int NumberofRowsShows = 6;

        /// <summary>
        /// Gets or sets Total record count of user list
        /// </summary>
        /// <value>The record count.</value>
        /// <remarks></remarks>
        public int RecordCount
        {
            /// Gets record count
            get { return this.ViewState["RecordCount"] == null ? 0 : Convert.ToInt32(this.ViewState["RecordCount"].ToString()); }

            /// Sets record count
            set { this.ViewState["RecordCount"] = value; }
        }

        public List<CH_UnbilledUnsettled_TransactionsDTO> lstAllTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO> { };
        public List<CH_UnbilledUnsettled_TransactionsDTO> lstAllBilledTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO> { };
        public List<CH_UnbilledUnsettled_TransactionsDTO> lstAllUnbilledTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO> { };
        public List<CH_UnbilledUnsettled_TransactionsDTO> lstAllUnsettledTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO> { };

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
                if (!IsPostBack)
                    LoadPage();
            }
            //ConvertToEMI.NavigateUrl = "~/ServiceRequest/EMIRequest.aspx?requestid=" + RequestType_Id.EncryptURL();
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
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    if (Session[sessionAccountNumber] != null)
        //    {
        //        BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]));
        //    }
        //}
        /// <summary>
        /// Handles the Click event of the btnPayNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void btnPayNow_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Card/PaymentProcess.aspx");
        //}

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            decimal amount = 0.00M;
            if (hdnIntTot.Value != null && Convert.ToDecimal(hdnIntTot.Value) > amount)
            {
                Session[totalUnbilledAmount] = hdnIntTot.Value;
                Response.Redirect("~/Card/PaymentProcess.aspx");
            }
        }

        protected void btnAllView_Click(object sender, EventArgs e)
        {
            NumberofRowsShows += 6;
            BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]), 1, NumberofRowsShows);
            ScriptManager.RegisterStartupScript(this, GetType(), "HighlightAllTab", "HighlightAllTab();", true);
        }

        protected void btnUnbilledView_Click(object sender, EventArgs e)
        {
            NumberofRowsShows += 6;
            CheckBox chkAllSelect = (CheckBox)lstviewUnbilledTrasnaction.FindControl("chkAllSelect");
            chkAllSelect.Checked = false;
            BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]), 2, NumberofRowsShows);                  
            ScriptManager.RegisterStartupScript(this, GetType(), "HighlightUnbilledTab", "HighlightUnbilledTab();", true);
        }

        protected void btnUnsettledView_Click(object sender, EventArgs e)
        {      
            NumberofRowsShows += 6;
            BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]), 3, NumberofRowsShows);
            ScriptManager.RegisterStartupScript(this, GetType(), "HighlightUnSettledTab", "HighlightUnSettledTab();", true);
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
            //this.BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]));
        }
        /// <summary>
        /// Handles the RowDataBound event of the grdUnbilledTransactions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void grdUnbilledTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //if (Convert.ToInt32(ddlSelectTransactionType.SelectedIndex) == 1)
        //    //{
        //    //    if (e.Row.RowType == DataControlRowType.Header)
        //    //    {
        //    //        TableHeaderCell NewCell = new TableHeaderCell();
        //    //        NewCell.Text = "Merchant Name";
        //    //        e.Row.Cells.AddAt(2, NewCell);
        //    //    }
        //    //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    //    {
        //    //        string cardNumber = string.Empty;
        //    //        Label lblMerchant = (Label)e.Row.FindControl(lblMerchantName);
        //    //        TableCell NewCell = new TableCell();
        //    //        NewCell.ID = "MerchantCell";
        //    //        NewCell.Text = lblMerchant.Text.Trim();
        //    //        e.Row.Cells.AddAt(2, NewCell);
        //    //    }
        //    //}
        //}
        #endregion

        #region Helper Methods
        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPage()
        {
            if (Session[sessionAccountNumber] == null)
                Response.Redirect("~/Login.aspx");

            this.Pager1.Visible = false;
            //ConvertToEMI.Visible = false;
            //SetVisibility(false);
            //BindTransactionTypeDropdownlist();
            if (Session[sessionAccountNumber] != null)
            {
                
                BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]), 1, NumberofRowsShows);
                BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]), 2, NumberofRowsShows);
                BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]), 3, NumberofRowsShows);               
            }

        }
        ///// <summary>
        ///// Binds the summary dropdownlist.
        ///// </summary>
        //private void BindTransactionTypeDropdownlist()
        //{
        //    string[] TransactionTypeItems = { selectItem, unbilledTransactionItem, unSettledTransactionItem };
        //    foreach (string s in TransactionTypeItems)
        //    {
        //        ListItem newItem = new ListItem();
        //        newItem.Text = s.Split('|')[0];
        //        newItem.Value = s.Split('|')[1];
        //        if (s.Split('|')[0].Replace(" ", "").ToLower() == selectItem)
        //            newItem.Selected = true;
        //        ddlSelectTransactionType.Items.Add(newItem);
        //    }
        //}
        /// <summary>
        /// Binds the transaction type dropdownlist.
        /// </summary>
        /// <remarks></remarks>
        //private void BindTransactionTypeDropdownlist()
        //{
        //    DropdownHdrManager dhm = new DropdownHdrManager();
        //    List<DropDown_HdrDTO> list = dhm.SearchDllHeader("Unbilled_Transactions").ToList();
        //    if (list.Count > 0)
        //    {
        //        ddlSelectTransactionType.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
        //        ddlSelectTransactionType.DataTextField = "Description";
        //        ddlSelectTransactionType.DataValueField = "DropDown_Dtl_Id";
        //        ddlSelectTransactionType.DataBind();
        //        ddlSelectTransactionType.Items.Insert(0, new ListItem(Constants.selectItemTransactionType, "0"));
        //    }
        //}
        /// <summary>
        /// Binds the grid card statement.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void BindGridUnbilledTransactions(string accountNumber, int TransactionType, int NumberofRowsShows)
        {
            
            int pRecordCount = 0;
            int SkipCount = (this.Pager1.CurrentIndex - 1) * this.Pager1.PageSize;
           // int TransactionType = 0;
            //ConvertToEMI.Visible = true;
            //BindGrid(null);
            //if (Convert.ToInt32(ddlSelectTransactionType.SelectedIndex) > 0)
            {
                //gridheader.InnerText = "List Of " + ddlSelectTransactionType.SelectedItem.Text.Trim();
                //if (ddlSelectTransactionType.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == unbilledtransactions)
                    //TransactionType = 1;
                //else if (ddlSelectTransactionType.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == unsettledtransactions)
                //    TransactionType = 2;
                //if (TransactionType == 2)
                //    ConvertToEMI.Visible = false;
                //List<CH_UnbilledUnsettled_TransactionsDTO> lst = new List<CH_UnbilledUnsettled_TransactionsDTO>();
                TransactionManager tm = new TransactionManager();
                if (TransactionType == 1) //All Transaction
                {
                    this.Pager1.PageSize = NumberofRowsShows;
                    lstAllTransactions = tm.GetUnbilledTransactions(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, TransactionType);
                    if (lstAllTransactions == null)
                    {                      
                        LblErrorMessage.Text = Constants.RecordNotFound;
                        DivERROR.Attributes.CssStyle.Add("display", "block");
                    }
                    if (lstAllTransactions != null && lstAllTransactions.Count > 0 && lstAllTransactions.Count > NumberofRowsShows)
                    {
                        btnAllView.Visible = true;                        
                    }
                    else
                    {
                        btnAllView.Visible = false;                       
                    }
                }
                else if (TransactionType == 2) // Unbilled Transactions
                {
                    this.Pager1.PageSize = NumberofRowsShows;
                    lstviewUnbilledTrasnaction.DataSource = lstAllUnbilledTransactions = tm.GetUnbilledTransactions(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, TransactionType);
                    lstviewUnbilledTrasnaction.DataBind();
                    if (lstAllUnbilledTransactions == null)
                        pRecordCount = 0;
                    this.RecordCount = pRecordCount;
                    this.Pager1.Visible = this.RecordCount > this.Pager1.PageSize;
                    if(lstAllUnbilledTransactions != null && lstAllUnbilledTransactions.Count>0 && lstAllUnbilledTransactions.Count> NumberofRowsShows)
                    {
                        btnUnbilledView.Visible = true;
                    }
                    else
                    {
                        btnUnbilledView.Visible = false;
                    }
                    //if (lstAllUnbilledTransactions != null && lstAllUnbilledTransactions.Count > 0)
                    //{
                    //    //btnpaynowWithouAmt.Visible = false;
                    //    double Totalamount = 0;
                    //    foreach (var item in lstAllUnbilledTransactions)
                    //    {
                    //        Totalamount = Totalamount + item.Amount;
                    //    }
                    //    if (Session[sessionCardStatementAmountDue] != null)
                    //        Session[sessionCardStatementAmountDue] = null;

                    //    Session[totalUnbilledAmount] = Totalamount;
                    //    //BindGrid(lst);
                    //}
                    //else
                    //{
                    //    lblNoRecords.Visible = true;
                    //    //btnpaynowWithouAmt.Visible = true;
                    //    //SetVisibility(false);
                    //}
                }
                else if (TransactionType == 3) // UnsettledTrasanction
                {
                    this.Pager1.PageSize = NumberofRowsShows;
                    lstviewUnsettledTransaction.DataSource = lstAllUnsettledTransactions = tm.GetUnbilledTransactions(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, TransactionType);
                    lstviewUnsettledTransaction.DataBind();
                    if (lstAllUnsettledTransactions != null && lstAllUnsettledTransactions.Count > 0 && lstAllUnsettledTransactions.Count > NumberofRowsShows)
                    {
                        btnUnsettledView.Visible = true;
                    }
                    else
                    {
                        btnUnsettledView.Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <remarks></remarks>
        //private void BindGrid(List<CH_UnbilledUnsettled_TransactionsDTO> lst)
        //{
        //    SetVisibility(true);
        //    grdUnbilledTransactions.DataSource = lst;
        //    grdUnbilledTransactions.DataBind();
        //}
        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="setVisible">if set to <c>true</c> [set visible].</param>
        /// <remarks></remarks>
        //private void SetVisibility(bool setVisible)
        //{
        //    gridheader.Visible = setVisible;
        //    grdUnbilledTransactions.Visible = setVisible;
        //    ConvertToEMI.Visible = setVisible;
        //    btnPayNow.Visible = setVisible;

        //}

        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static List<CH_UnbilledUnsettled_TransactionsDTO> BindGridUnbilledTransactionstest(string accountNumber,int TransactionType)
        //{

        //    int pRecordCount = 0;
        //    int SkipCount = 0;           
        //    //ConvertToEMI.Visible = true;
        //    //BindGrid(null);
        //    //if (Convert.ToInt32(ddlSelectTransactionType.SelectedIndex) > 0)
        //    {
        //        //gridheader.InnerText = "List Of " + ddlSelectTransactionType.SelectedItem.Text.Trim();
        //        //if (ddlSelectTransactionType.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == unbilledtransactions)                
        //        //else if (ddlSelectTransactionType.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == unsettledtransactions)
        //        //    TransactionType = 2;
        //        //if (TransactionType == 2)
        //        //    ConvertToEMI.Visible = false;
        //        //List<CH_UnbilledUnsettled_TransactionsDTO> lst = new List<CH_UnbilledUnsettled_TransactionsDTO>();
        //        TransactionManager tm = new TransactionManager();
        //        lst = tm.GetUnbilledTransactions(0, 10, ref pRecordCount, accountNumber, TransactionType);
        //        if (lst == null)
        //            pRecordCount = 0;
        //        //this.RecordCount = pRecordCount;
        //        //this.Pager1.Visible = this.RecordCount > this.Pager1.PageSize;
        //        //lblNoRecords.Visible = false;
        //        if (lst != null && lst.Count > 0)
        //        {
        //            //btnpaynowWithouAmt.Visible = false;
        //            double Totalamount = 0;
        //            foreach (var item in lst)
        //            {
        //                Totalamount = Totalamount + item.Amount;
        //            }
        //            if (HttpContext.Current.Session[sessionCardStatementAmountDue] != null)
        //                HttpContext.Current.Session[sessionCardStatementAmountDue] = null;

        //            HttpContext.Current.Session[totalUnbilledAmount] = Totalamount;
        //            //BindGrid(lst);
                  
        //        }
        //        else
        //        {
        //            //lblNoRecords.Visible = true;
        //            //btnpaynowWithouAmt.Visible = true;
        //            //SetVisibility(false);
        //        }
        //    }
        //    return lst;
        //}

        //protected void pillsUnbilledTab_Click(object sender, EventArgs e)
        //{
            
        //}
        #endregion
    }
}