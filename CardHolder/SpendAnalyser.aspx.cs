using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
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
    public partial class SpendAnalyser : PageBase
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        string monthlyGraphLabel = Constants.GraphLabel;
        /// <summary>
        /// 
        /// </summary>
        string monthWiseAreaName = "MonthWiseArea";
        /// <summary>
        /// 
        /// </summary>
        string fontName = "Arial,Helvetica,sans-serif";
        /// <summary>
        /// 
        /// </summary>
        string monthsSeries = "Months";
        #endregion

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
                    loadDetails();
            }
        }


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
                lblDisplayMessage.Text = "";
                Page.Validate();
                if (Page.IsValid)
                {
                    string accountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
                    DateTime? fromDate = null;
                    DateTime? toDate = null;
                    if (txtFromDate.Text.Trim() != string.Empty)
                        fromDate = GetDateTime(txtFromDate.Text.Trim());
                    if (txtToDate.Text.Trim() != string.Empty)
                        toDate = GetDateTime(txtToDate.Text.Trim());
                    if (accountNumber != "" && txtFromDate.Text.Trim() != string.Empty && txtToDate.Text.Trim() != string.Empty)
                        GetSpendAnalyserReport(accountNumber, fromDate, toDate);
                }
            }
            catch
            {
                lblDisplayMessage.Text = Constants.GeneralErrorMessage;
                return;
            }
        }


        /// <summary>
        /// Gets the spend analyser report.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <remarks></remarks>
        private void GetSpendAnalyserReport(string accountNumber, DateTime? FromDate, DateTime? toDate)
        {
            try
            {
                lblReportName.Visible = false;
                lblReportName.InnerText = monthlyGraphLabel;
                gridheader.Visible = false;
                divMonthWiseReport.Visible = false;
                if (accountNumber != "")
                {
                    List<CH_SpendAnalyserDTO> lst = new List<CH_SpendAnalyserDTO>();
                    SpendAnalyserManager sam = new SpendAnalyserManager();
                    lst = sam.GetSpendAnalyserReport(accountNumber, FromDate, toDate);
                    if (lst == null || lst.Count < 0)
                    {
                        lblDisplayMessage.Text = Constants.NoDataFoundForGraph;
                        return;
                    }
                    int rvalue = DisplayMonthWiseReport(lst);
                    if (rvalue == 0)
                    {
                        lblDisplayMessage.Text = Constants.GeneralErrorMessage;
                        return;
                    }
                }
            }
            catch
            {
                lblDisplayMessage.Text = Constants.GeneralErrorMessage;
                return;
            }
        }

        /// <summary>
        /// Displays the month wise report.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int DisplayMonthWiseReport(List<CH_SpendAnalyserDTO> lst)
        {
            try
            {
                divMonthWiseReport.Visible = true;
                Dictionary<string, double> MonthWiseData = new Dictionary<string, double>();
                foreach (CH_SpendAnalyserDTO obj in lst)
                {
                    MonthWiseData.Add(Convert.ToString(obj.DESCRIPTION), Convert.ToDouble(obj.AMOUNT));
                }
                if (MonthWiseData.Count < 0)
                    return 0;

                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisX.Title = Constants.axisXTitle;
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisY.Title = Constants.axisYTitle;
                MonthwiseChart.BorderColor = Color.White;
                MonthwiseChart.BorderlineDashStyle = ChartDashStyle.Solid;
                MonthwiseChart.BackGradientStyle = GradientStyle.TopBottom;
                MonthwiseChart.AntiAliasing = AntiAliasingStyles.All;
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisX.MajorGrid.LineColor = Color.FromArgb(211, 211, 211);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisY.MajorGrid.LineColor = Color.FromArgb(211, 211, 211);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisX.LineColor = Color.FromArgb(167, 186, 197);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisY.LineColor = Color.FromArgb(167, 186, 197);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisY.LabelStyle.ForeColor = Color.FromArgb(12, 12, 12);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisY.LabelStyle.Font = new Font(fontName, 8.0f, FontStyle.Regular);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisX.LabelStyle.Font = new Font(fontName, 8.0f, FontStyle.Regular);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisX.LabelStyle.ForeColor = Color.FromArgb(12, 12, 12);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisX.TitleFont = new Font(fontName, 8.0f, FontStyle.Bold);
                MonthwiseChart.ChartAreas[monthWiseAreaName].AxisY.TitleFont = new Font(fontName, 8.0f, FontStyle.Bold);
                MonthwiseChart.Series[monthsSeries].Points.DataBind(MonthWiseData, "Key", "Value", string.Empty);
                lblReportName.Visible = true;
                gridheader.Visible = true;
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Loads the details.
        /// </summary>
        /// <remarks></remarks>
        private void loadDetails()
        {
            gridheader.Visible = false;
            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;
            if (card != null)
            {
                lblCardHolder.Text = card.FULL_NAME;
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
            }
        }

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
                e.IsValid = false;
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
    }
}