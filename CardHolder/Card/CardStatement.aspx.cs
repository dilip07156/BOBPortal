using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder.Card
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class CardStatement : PageBase
    {
        #region Variables


        /// <summary>
        /// 
        /// </summary>
        string printControlId = "ibtnPrint";
        /// <summary>
        /// 
        /// </summary>
        string downloadControlId = "ibtnPDF";
        /// <summary>
        /// 
        /// </summary>
        string LblNoPDfId = "lblNopdf";
        /// <summary>
        /// 
        /// </summary>
        string lblnoPrintId = "lblNoprint";
        /// <summary>
        /// 
        /// </summary>
        string lblPDFNameControlId = "lblPDFName";
        /// <summary>
        /// 
        /// </summary>
        string qsk = "awa4befr";
        /// <summary>
        /// 
        /// </summary>
        string queryString = "f={0}";
        /// <summary>
        /// 
        /// </summary>
        string printPDFToolTip = "Print PDF";
        /// <summary>
        /// 
        /// </summary>
        string downloadPDFToolTip = "Download PDF";
        /// <summary>
        /// 
        /// </summary>
        string sessionFilePath = "CardHolderStatementFilePath";
        /// <summary>
        /// 
        /// </summary>
        string sessionAccountNumber = "AccountNumber";
        /// <summary>
        /// 
        /// </summary>
        string sessionAmountDue = "AmountDue";
        /// <summary>
        /// 
        /// </summary>
        string totalUnbilledAmount = "totalUnbilledAmount";
        //string cmdDownloadPDF = "downloadpdf";
        //string cmdPrintPdf = "ibtnprintpdf";
        //string clientCodeControlId = "lblClientCode";
        //string cardNumberControlId = "lblCardnumber";
        //string pdfContentType = "Application/pdf";
        //string contentDisposition = "Content-Disposition";
        //string attachmentFileName = "attachment; filename=";
        //string printpdf = "printpdf";
        //string lblStatementDateControlId = "lblStatementDate";


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

        public List<CH_CR_TERMDTO> lstCardStatement = new List<CH_CR_TERMDTO> { };

        public int rowcount = 0;
        public int subrowcount = 0;
        #endregion

        #region Page Methods
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
        }
        /// <summary>
        /// Page pre render event to set pager item count
        /// </summary>
        /// <param name="e">event argument</param>
        /// <remarks></remarks>
        protected override void OnPreRender(EventArgs e)
        {
            //this.Pager1.ItemCount = this.RecordCount;
            base.OnPreRender(e);
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the RowDataBound event of the grdCardStatement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void grdCardStatement_RowDataBound(object sender, GridViewRowEventArgs e)
        protected void lstViewCardStatement_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                bool IsAccHavePdf = false;
                string FileName = "";
                string AccountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
                Label LblNoPdf = (Label)e.Item.FindControl(LblNoPDfId);
                Label LblNoPrint = (Label)e.Item.FindControl(lblnoPrintId);
                Label lblPDFName = (Label)e.Item.FindControl(lblPDFNameControlId);
                if (lblPDFName.Text.Trim() != "")
                    FileName = Convert.ToString(lblPDFName.Text.Trim());

                //CH_EVG_EVENTS_QUEUEDTO objPDF = new CardManager().GetCardStatementPDFFileName(cardNumber, Convert.ToDateTime(lblStatementDate.Text));
                //if (objPDF != null && objPDF.EVE_OUT_FILENAME != string.Empty)
                //    FileName = Convert.ToString(objPDF.EVE_OUT_FILENAME);

                LinkButton ibtnPrint = (LinkButton)e.Item.FindControl(printControlId);
                LinkButton ibtnDownload = (LinkButton)e.Item.FindControl(downloadControlId);
                string pdfPath = "";
                if (Session[sessionFilePath] == null)
                {
                    pdfPath = GetFilePath();
                    Session[sessionFilePath] = pdfPath;
                }
                else if (Session[sessionFilePath] != null)
                    pdfPath = Convert.ToString(Session[sessionFilePath]);
                if (!string.IsNullOrEmpty(FileName))
                {
                    IsAccHavePdf = CardManager.GetPDFnames(AccountNumber, FileName);
                    if (IsAccHavePdf == true)
                    {
                        if (File.Exists(pdfPath + FileName))
                        {
                            LblNoPdf.Visible = false;
                            LblNoPrint.Visible = false;
                            ibtnPrint.Enabled = true;
                            ibtnDownload.Enabled = true;
                            ibtnPrint.ToolTip = printPDFToolTip;
                            ibtnDownload.ToolTip = downloadPDFToolTip;
                            string fn = string.Format(queryString, FileName);
                            string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
                            ibtnPrint.Attributes.Add("OnClick", "return DisplayPDF('" + urlQueryString + "');");
                            ibtnDownload.Attributes.Add("OnClick", "return DwnloadPDF('" + urlQueryString + "');");
                        }
                        else
                        {
                            LblNoPdf.Visible = true;
                            LblNoPrint.Visible = true;
                            ibtnDownload.Visible = false;
                            ibtnPrint.Visible = false;
                            LblNoPdf.ToolTip = Constants.fileNotFound;
                            LblNoPrint.ToolTip = Constants.fileNotFound;
                        }
                    }
                    else
                    {
                        LblNoPdf.Visible = true;
                        LblNoPrint.Visible = true;
                        ibtnDownload.Visible = false;
                        ibtnPrint.Visible = false;
                        LblNoPdf.ToolTip = Constants.fileNotFound;
                        LblNoPrint.ToolTip = Constants.fileNotFound;
                    }
                }
                else
                {
                    LblNoPdf.Visible = true;
                    LblNoPrint.Visible = true;
                    ibtnDownload.Visible = false;
                    ibtnPrint.Visible = false;
                    LblNoPdf.ToolTip = Constants.fileNotFound;
                    LblNoPrint.ToolTip = Constants.fileNotFound;
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
            //this.Pager1.CurrentIndex = Convert.ToInt32(e.CommandArgument.ToString());

            //this.BindGridCardStatement(Convert.ToString(Session[sessionAccountNumber]));
        }

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
      
        #endregion

        #region Helper Methods
        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPage()
        {
            string accountNumber = "";
            if (Session[sessionAccountNumber] != null)
            {
                accountNumber = Convert.ToString(Session[sessionAccountNumber]);
            }
            else
                Response.Redirect("~/Login.aspx");

            //this.Pager1.Visible = false;
            //btnPayNow.Visible = false;
            BindGridCardStatement(accountNumber);
        }
        /// <summary>
        /// Binds the grid card statement.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        private void BindGridCardStatement(string accountNumber)
        {
            lblStmntmonthrange.Text = "";
            try
            {
                int monthRange = GetMonthRangeToShowStatement(Constants.CardHolderStatementMonthly);                
                if (monthRange > 0)
                    lblStmntmonthrange.Text = Constants.StatementMonths + monthRange + " months";
                int pRecordCount = 0;
                int SkipCount = (this.Pager1.CurrentIndex - 1) * this.Pager1.PageSize;
                lstCardStatement = new CardManager().GetCardStatement(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, monthRange);
                lstViewCardStatement.DataSource = lstCardStatement;
                this.RecordCount = pRecordCount;
                this.Pager1.Visible = this.RecordCount > this.Pager1.PageSize;               
                //if (pRecordCount > 0)
                //    btnPayNow.Visible = true;
                //else
                //    btnPayNow.Visible = false;
                lstViewCardStatement.DataBind();
                double closingBalance = 0;
                // CH_CR_TERMDTO lastRecordInGrid = lstCardStatement[lstCardStatement.Count - 1];
                CH_CR_TERMDTO lastRecordInGrid = lstCardStatement[0];
                if (lastRecordInGrid != null)
                    closingBalance = lastRecordInGrid.Billed_Closing_Bal;
                if (Session[totalUnbilledAmount] != null)
                    Session[totalUnbilledAmount] = null;

                Session[sessionAmountDue] = closingBalance;             
            }
            catch (Exception ex)
            {
                DisplayMessage("Some error has generated.Please contact administrator.", false);
            }
        }


        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetFilePath()
        {
            string pdfFilePath = string.Empty;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(Constants.CardHolderStatementFilePath);
            if (obj != null && obj.Parameter_ValueC != null)
                pdfFilePath = Convert.ToString(obj.Parameter_ValueC);
            return pdfFilePath;
        }
        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <remarks></remarks>
        private void DisplayMessage(string msg, bool visible)
        {
            //lblMessageDisplay.Visible = visible;
            //lblMessageDisplay.Text = msg;
        }

        /// <summary>
        /// Gets the month range to show statement.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int GetMonthRangeToShowStatement(string keyValue)
        {
            int noOfMonths = 6;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(keyValue);
            if (obj != null && obj.Parameter_ValueN != null)
                noOfMonths = Convert.ToInt32(obj.Parameter_ValueN);
            return noOfMonths;
        }

        #endregion           
    }      

        
    }
#region Comments

///// <summary>
///// Handles the RowCommand event of the grdCardStatement control.
///// </summary>
///// <param name="sender">The source of the event.</param>
///// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
//protected void grdCardStatement_RowCommand(object sender, GridViewCommandEventArgs e)
//{
//    //if (e.CommandName == cmdDownloadPDF)
//    //{
//    //    string FileName = "";
//    //    string cardNumber = string.Empty;
//    //    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
//    //    Label lblClientCode = (Label)row.FindControl(clientCodeControlId);
//    //    Label lblCardnumber = (Label)row.FindControl(cardNumberControlId);
//    //    Label lblStatementDate = (Label)row.FindControl(lblStatementDateControlId);
//    //    ImageButton ibtnDownload = (ImageButton)row.FindControl(downloadControlId);
//    //    Label lblPDFName = (Label)row.FindControl(lblPDFNameControlId);
//    //    if (lblPDFName.Text.Trim() != "")
//    //        FileName = Convert.ToString(lblPDFName.Text.Trim());
//    //    if (FileName != null && FileName != string.Empty)
//    //    {
//    //        string pdfPath = GetFilePath();
//    //        if (File.Exists(pdfPath + FileName))
//    //        {
//    //            ibtnDownload.Enabled = true;
//    //            Response.ContentType = pdfContentType;
//    //            Response.AppendHeader(contentDisposition, attachmentFileName + FileName);
//    //            //Response.TransmitFile(Server.MapPath(pdfPath + FileName)); //If path is virtual path
//    //            Response.TransmitFile(pdfPath + FileName); //If path is physical path
//    //            Response.End();
//    //        }
//    //    }
//    //}

//    //else if (e.CommandName == cmdPrintPdf)
//    //{
//    //    bool IsAccHavePdf = false;
//    //    string FileName = "";
//    //    string cardNumber = string.Empty;

//    //    string AccountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number;
//    //    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
//    //    Label LblNoPdf = (Label)row.FindControl(LblNoPDfId);
//    //    Label LblNoPrint = (Label)row.FindControl(lblnoPrintId);
//    //    Label lblStatementDate = (Label)row.FindControl(lblStatementDateControlId);
//    //    Label lblPDFName = (Label)row.FindControl(lblPDFNameControlId);
//    //    if (lblPDFName.Text.Trim() != "")
//    //        FileName = Convert.ToString(lblPDFName.Text.Trim());

//    //    //CH_EVG_EVENTS_QUEUEDTO objPDF = new CardManager().GetCardStatementPDFFileName(cardNumber, Convert.ToDateTime(lblStatementDate.Text));
//    //    //if (objPDF != null && objPDF.EVE_OUT_FILENAME != string.Empty)
//    //    //    FileName = Convert.ToString(objPDF.EVE_OUT_FILENAME);

//    //    ImageButton ibtnPrint = (ImageButton)row.FindControl(printControlId);
//    //    ImageButton ibtnDownload = (ImageButton)row.FindControl(downloadControlId);
//    //    string pdfPath = "";
//    //    if (Session[sessionFilePath] == null)
//    //    {
//    //        pdfPath = GetFilePath();
//    //        Session[sessionFilePath] = pdfPath;
//    //    }
//    //    else if (Session[sessionFilePath] != null)
//    //        pdfPath = Convert.ToString(Session[sessionFilePath]);
//    //    if (FileName != null && FileName != "")
//    //    {
//    //        IsAccHavePdf = CardManager.GetPDFnames(AccountNumber, FileName);
//    //        if (IsAccHavePdf == true)
//    //        {
//    //            if (File.Exists(pdfPath + FileName))
//    //            {
//    //                LblNoPdf.Visible = false;
//    //                LblNoPrint.Visible = false;
//    //                ibtnPrint.Enabled = true;
//    //                ibtnDownload.Enabled = true;
//    //                ibtnPrint.ToolTip = printPDFToolTip;
//    //                ibtnDownload.ToolTip = downloadPDFToolTip;
//    //                string fn = string.Format(queryString, FileName);
//    //                string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
//    //                //ibtnPrint.Attributes.Add("OnClick", "return DisplayPDF('" + urlQueryString + "');");
//    //                ScriptManager.RegisterStartupScript(this, GetType(), "Dispaly pdf", "DisplayPDF('" + urlQueryString + "');", true);
//    //            }
//    //            else
//    //            {
//    //                LblNoPdf.Visible = true;
//    //                LblNoPrint.Visible = true;
//    //                ibtnDownload.Visible = false;
//    //                ibtnPrint.Visible = false;
//    //                LblNoPdf.ToolTip = Constants.fileNotFound;
//    //                LblNoPrint.ToolTip = Constants.fileNotFound;
//    //            }
//    //        }
//    //        else
//    //        {
//    //            LblNoPdf.Visible = true;
//    //            LblNoPrint.Visible = true;
//    //            ibtnDownload.Visible = false;
//    //            ibtnPrint.Visible = false;
//    //            LblNoPdf.ToolTip = Constants.fileNotFound;
//    //            LblNoPrint.ToolTip = Constants.fileNotFound;
//    //        }
//    //    }
//    //    else
//    //    {
//    //        LblNoPdf.Visible = true;
//    //        LblNoPrint.Visible = true;
//    //        ibtnDownload.Visible = false;
//    //        ibtnPrint.Visible = false;
//    //        LblNoPdf.ToolTip = Constants.fileNotFound;
//    //        LblNoPrint.ToolTip = Constants.fileNotFound;
//    //    }
//    //}

//}

//RowDataBound - if path is virtual path------------
//HtmlAnchor aPrintPDF = (HtmlAnchor)e.Row.FindControl(printpdf);
//CH_EVG_EVENTS_QUEUEDTO objPDF = new CardManager().GetCardStatementPDFFileName(cardNumber, clientCode);
//if (objPDF != null && objPDF.EVE_OUT_FILENAME != string.Empty)
//    aPrintPDF.HRef = pdfPath + objPDF.EVE_OUT_FILENAME;
#endregion

