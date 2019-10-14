using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class PaymentCreditDetail : PageBase
    {
        #region variables
        /// <summary>
        /// 
        /// </summary>
        string sessionPaymentHistoryMonths = "PaymentHistoryMonths";
        /// <summary>
        /// 
        /// </summary>
        string sessionAccountNumber = "AccountNumber";

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

        #region Page events
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
            RegisterDatePickerScript();
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
            if (Page.IsValid)
                BindData();
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
            this.BindData();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPage()
        {
            if (Session[sessionAccountNumber] == null)
                Response.Redirect("~/Login.aspx");

            this.Pager1.Visible = false;
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            SetVisibility(false);
        }
        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <remarks></remarks>
        private void BindData()
        {
            string accountNumber = "";
           
            if (Session[sessionAccountNumber] != null)
                accountNumber = Convert.ToString(Session[sessionAccountNumber]);

            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (txtFromDate.Text.Trim() != string.Empty)
                fromDate = GetDateTime(txtFromDate.Text.Trim());
            //fromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            if (txtToDate.Text.Trim() != string.Empty)
                toDate = GetDateTime(txtToDate.Text.Trim());
            //toDate = Convert.ToDateTime(txtToDate.Text.Trim());
            if (txtFromDate.Text.Trim() != string.Empty && txtToDate.Text.Trim() != string.Empty)
                BindPaymentCreditDetail(accountNumber, fromDate, toDate);

        }
        /// <summary>
        /// Binds the grid card statement.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="Todate">The todate.</param>
        /// <remarks></remarks>
        private void BindPaymentCreditDetail(string accountNumber, DateTime? FromDate, DateTime? Todate)
        {
            int pRecordCount = 0;
            lblnorecords.Text = "";
            int SkipCount = (this.Pager1.CurrentIndex - 1) * this.Pager1.PageSize;
            List<CH_Cr_Current_Trans> lst = new List<CH_Cr_Current_Trans>();
            TransactionManager tm = new TransactionManager();
            lst = tm.GetPaymentCreditDetails(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, FromDate, Todate);
            this.RecordCount = pRecordCount;
            this.Pager1.Visible = this.RecordCount > this.Pager1.PageSize;
            if (lst != null && lst.Count > 0)
                BindGrid(lst);
            else
            {
                lblnorecords.Text = Constants.RecordNotFound;
                lblnorecords.CssClass = "error";
                SetVisibility(false);
            }
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <remarks></remarks>
        private void BindGrid(List<CH_Cr_Current_Trans> lst)
        {
            SetVisibility(true);
            grdPaymnetCreditDetail.DataSource = lst;
            grdPaymnetCreditDetail.DataBind();
        }
        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="setVisible">if set to <c>true</c> [set visible].</param>
        /// <remarks></remarks>
        private void SetVisibility(bool setVisible)
        {
            grdPaymnetCreditDetail.Visible = setVisible;
            gridheader.Visible = setVisible;
        }
        /// <summary>
        /// Registers the date picker script.
        /// </summary>
        /// <remarks></remarks>
        private void RegisterDatePickerScript()
        {
            int months = 12;
            if (Session[sessionPaymentHistoryMonths] == null)
            {
                months = GetNumberForDatePicker(Constants.paymentHistoryMonths);
                Session[sessionPaymentHistoryMonths] = months;
            }
            else
                months = Convert.ToInt32(Session[sessionPaymentHistoryMonths]);
            lblrange.Text = Constants.PaymentHistoryRange + months + " months.";
            string csname1 = "BindDatePickerScript";
            Type cstype = this.GetType();
            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;
            // Check to see if the startup script is already registered.
            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                System.Text.StringBuilder cstext1 = new System.Text.StringBuilder();
                cstext1.Append("<script type='text/javascript'>");
                cstext1.Append("var td = new Date();var minDate = new Date(td.getFullYear(), td.getMonth() - " + months + ", td.getDate());var maxDate = new Date(td.getFullYear(), td.getMonth(), td.getDate());$('.date-picker1').datepicker({ dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: '../images/datepick.png', buttonImageOnly: true,minDate:minDate,maxDate:maxDate,changeMonth: true, changeYear: true");
                cstext1.Append("});");
                cstext1.Append("var td = new Date();var minDate = new Date(td.getFullYear(), td.getMonth() - " + months + ", td.getDate());var maxDate = new Date(td.getFullYear(), td.getMonth(), td.getDate());$('.date-picker2').datepicker({ dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: '../images/datepick.png', buttonImageOnly: true,minDate:minDate,maxDate:maxDate,changeMonth: true, changeYear: true");
                cstext1.Append("});");
                cstext1.Append("</");
                cstext1.Append("script>");
                cs.RegisterStartupScript(cstype, csname1, cstext1.ToString());
                //if (months == 0)
                //{
                //    string constrain = -years + ":c";
                //    cstext1.Append("var td = new Date();var minDate = new Date(td.getFullYear() - " + years + ", td.getMonth(), td.getDay());var maxDate = new Date(td.getFullYear(), td.getMonth(), td.getDay());$('.date-picker1').datepicker({ dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: '../images/datepick.png', buttonImageOnly: true,minDate:minDate,maxDate:maxDate,changeMonth: true, changeYear: true, yearRange: '" + constrain + "'");
                //    cstext1.Append("});");
                //    cstext1.Append("var td = new Date();var minDate = new Date(td.getFullYear() - " + years + ", td.getMonth(), td.getDay());var maxDate = new Date(td.getFullYear(), td.getMonth(), td.getDay());$('.date-picker2').datepicker({ dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: '../images/datepick.png', buttonImageOnly: true,minDate:minDate,maxDate:maxDate,changeMonth: true, changeYear: true, yearRange: '" + constrain + "'");
                //    cstext1.Append("});");
                //}
                //else if (years == 0)
                //{
                //}
            }
        }
        /// <summary>
        /// Gets the months.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int GetNumberForDatePicker(string keyValue)
        {
            int noOfMonths = 12;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(keyValue);
            if (obj != null && obj.Parameter_ValueN != null)
                noOfMonths = Convert.ToInt32(obj.Parameter_ValueN);
            return noOfMonths;
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
            e.IsValid = true;
            DateTime start = GetDateTime(txtFromDate.Text.Trim());
            DateTime end = GetDateTime(txtToDate.Text.Trim());
            if (end < start)
            {
                e.IsValid = false;
                this.Pager1.Visible = false;
            }
        }

        #endregion
    }
}
