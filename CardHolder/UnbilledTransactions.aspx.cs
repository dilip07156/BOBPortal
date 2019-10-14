using System;
using System.Collections.Generic;
using System.Linq;
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
        string sessionAccountNumber = "AccountNumber";
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
        string totalUnbilledAmount = "totalUnbilledAmount";
        /// <summary>
        /// 
        /// </summary>
        string sessionCardStatementAmountDue = "AmountDue";

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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session[sessionAccountNumber] != null)
            {
                BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]));
            }
        }
        /// <summary>
        /// Handles the Click event of the btnPayNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Card/PaymentProcess.aspx");
        }

        protected void btnpaynowWithouAmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Card/PaymentProcess.aspx?ReqFrom=XQvbefr");
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
            this.BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]));
        }
        /// <summary>
        /// Handles the RowDataBound event of the grdUnbilledTransactions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void grdUnbilledTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (Convert.ToInt32(ddlSelectTransactionType.SelectedIndex) == 1)
            //{
            //    if (e.Row.RowType == DataControlRowType.Header)
            //    {
            //        TableHeaderCell NewCell = new TableHeaderCell();
            //        NewCell.Text = "Merchant Name";
            //        e.Row.Cells.AddAt(2, NewCell);
            //    }
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        string cardNumber = string.Empty;
            //        Label lblMerchant = (Label)e.Row.FindControl(lblMerchantName);
            //        TableCell NewCell = new TableCell();
            //        NewCell.ID = "MerchantCell";
            //        NewCell.Text = lblMerchant.Text.Trim();
            //        e.Row.Cells.AddAt(2, NewCell);
            //    }
            //}
        }
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
            ConvertToEMI.Visible = false;
            SetVisibility(false);
            BindTransactionTypeDropdownlist();
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
        private void BindTransactionTypeDropdownlist()
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            List<DropDown_HdrDTO> list = dhm.SearchDllHeader("Unbilled_Transactions").ToList();
            if (list.Count > 0)
            {
                ddlSelectTransactionType.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlSelectTransactionType.DataTextField = "Description";
                ddlSelectTransactionType.DataValueField = "DropDown_Dtl_Id";
                ddlSelectTransactionType.DataBind();
                ddlSelectTransactionType.Items.Insert(0, new ListItem(Constants.selectItemTransactionType, "0"));
            }
        }
        /// <summary>
        /// Binds the grid card statement.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        private void BindGridUnbilledTransactions(string accountNumber)
        {
            
            int pRecordCount = 0;
            int SkipCount = (this.Pager1.CurrentIndex - 1) * this.Pager1.PageSize;
            int TransactionType = 0;
            ConvertToEMI.Visible = true;
            BindGrid(null);
            if (Convert.ToInt32(ddlSelectTransactionType.SelectedIndex) > 0)
            {
                gridheader.InnerText = "List Of " + ddlSelectTransactionType.SelectedItem.Text.Trim();
                if (ddlSelectTransactionType.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == unbilledtransactions)
                    TransactionType = 1;
                else if (ddlSelectTransactionType.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == unsettledtransactions)
                    TransactionType = 2;
                if (TransactionType == 2)
                    ConvertToEMI.Visible = false;
                List<CH_UnbilledUnsettled_TransactionsDTO> lst = new List<CH_UnbilledUnsettled_TransactionsDTO>();
                TransactionManager tm = new TransactionManager();
                lst = tm.GetUnbilledTransactions(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, TransactionType);
                if (lst == null)
                    pRecordCount = 0;
                this.RecordCount = pRecordCount;
                this.Pager1.Visible = this.RecordCount > this.Pager1.PageSize;
                lblNoRecords.Visible = false;
                if (lst != null && lst.Count > 0)
                {
                    btnpaynowWithouAmt.Visible = false;
                    double Totalamount = 0;
                    foreach (var item in lst)
                    {
                        Totalamount = Totalamount + item.Amount;
                    }
                    if (Session[sessionCardStatementAmountDue] != null)
                        Session[sessionCardStatementAmountDue] = null;

                    Session[totalUnbilledAmount] = Totalamount;
                    BindGrid(lst);
                }
                else
                {
                    lblNoRecords.Visible = true;
                    btnpaynowWithouAmt.Visible = true;
                    SetVisibility(false);
                }
            }
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <remarks></remarks>
        private void BindGrid(List<CH_UnbilledUnsettled_TransactionsDTO> lst)
        {
            SetVisibility(true);
            grdUnbilledTransactions.DataSource = lst;
            grdUnbilledTransactions.DataBind();
        }
        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="setVisible">if set to <c>true</c> [set visible].</param>
        /// <remarks></remarks>
        private void SetVisibility(bool setVisible)
        {
            gridheader.Visible = setVisible;
            grdUnbilledTransactions.Visible = setVisible;
            ConvertToEMI.Visible = setVisible;
            btnPayNow.Visible = setVisible;

        }
        #endregion
    }
}