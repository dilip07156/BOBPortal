using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.Enums;
using System.Web.Script.Serialization;
using System.Web.UI;
using NLog;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Data;

namespace CardHolder.AccountSummary
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AccountSummary : PageBase
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        string sessionCardHolderRequestTypes = "CardHolerRequestTypes";
        /// <summary>
        /// <summary>
        /// 
        /// </summary>
        string accountSummaryItem = "accountsummary";
        /// <summary>
        /// 
        /// </summary>
        string cardSummaryItem = "cardsummary";

        /// <summary>
        /// 
        /// </summary>
        string zero = "0";
        /// <summary>
        /// 
        /// </summary>
        string strNotFound = "";
        /// <summary>
        /// 
        /// </summary>
        string sessionAccountNumber = "AccountNumber";


        private static List<CH_Request_DtlDTO> lstRequestDTO;

        private static List<CH_UnbilledUnsettled_TransactionsDTO> lstUnbilledTransactionsDTO;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string Cardnumber = string.Empty;

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

        #region Page  Methods
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
                //else
                //{
                //    ddlCardNumber.SelectedValue = Session["selectedCardNumber"].ToString();
                //}
            }
        }
        #endregion

        #region Events
        ///// <summary>
        ///// Handles the SelectedIndexChanged event of the ddlAccountSummary control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ///// <remarks></remarks>
        //protected void ddlAccountSummary_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string accountnumber = "";
        //    if (Session[sessionAccountNumber] != null)
        //        accountnumber = Convert.ToString(Session[sessionAccountNumber]);
        //    else
        //        Response.Redirect("~/Login.aspx");

        //    if (ddlAccountSummary.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == accountSummaryItem)
        //        ddlCardNumber.Items.Clear();
        //    else if (ddlAccountSummary.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == cardSummaryItem)
        //        BindCardNumberDropdownlist(accountnumber);
        //    else
        //        ddlCardNumber.Items.Clear();
        //}


        ///// <summary>
        ///// Handles the SelectedIndexChanged event of the ddlcardlist control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ///// <remarks></remarks>
        //protected void ddlCardNumber_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["selectedCardNumber"] = ddlCardNumber.SelectedValue;
        //}

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    string accountnumber = "";
        //    hideCreditCardNumber.Value = ddlCardNumber.SelectedValue;
        //    if (Session[sessionAccountNumber] != null)
        //        accountnumber = Convert.ToString(Session[sessionAccountNumber]);
        //    else
        //        Response.Redirect("~/Login.aspx");

        //    //if (ddlAccountSummary.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == Constants.selectItem)
        //    //{
        //    //    ddlCardNumber.Items.Clear();
        //    //    accountsdetailsdisplay.Visible = false;
        //    //}
        //    else if (ddlAccountSummary.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == cardSummaryItem && ddlCardNumber.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == zero)
        //        accountsdetailsdisplay.Visible = false;
        //    else if (ddlAccountSummary.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == accountSummaryItem)
        //    {
        //        accountsdetailsdisplay.Visible = true;
        //        displaynameoncard.Visible = false;
        //        divSpace.Visible = false;
        //        ddlCardNumber.Items.Clear();
        //        Literal2.Visible = true;
        //        Literal6.Visible = false;
        //        SetAccountSummary(accountnumber);
        //        SetSummary(accountnumber);
        //    }
        //    else if (ddlAccountSummary.SelectedItem.Text.Trim().ToLower().Replace(" ", "") == cardSummaryItem)
        //    {
        //        accountsdetailsdisplay.Visible = true;
        //        displaynameoncard.Visible = true;
        //        divSpace.Visible = true;
        //        Literal6.Visible = true;
        //        Literal2.Visible = false;
        //        SetCardSummary(Convert.ToString(ddlCardNumber.SelectedValue.Trim()));
        //        SetSummary(accountnumber);
        //    }
        //    //CreateRequest();
        //}


        //private void CreateRequest()
        //{
        //    string result = string.Empty;
        //    Helper objHelper = new Helper();
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    CommonRequest objBalanceRequest =  new CommonRequest()
        //    {
        //        TxnType = TranscationType.BA.ToString(),
        //        CardNumber = Convert.ToString(hideCreditCardNumber.Value),
        //        TransRefNo = objHelper.RandomDigits(10),
        //        TransDateTime = String.Format("{0:MM/dd/yyyy}", DateTime.Now)
        //    };
        //    string jsondata = new JavaScriptSerializer().Serialize(objBalanceRequest);
        //    result = objHelper.GetResponse(jsondata);
        //    dynamic objResult = js.Deserialize<dynamic>(result);
        //    if (objResult["RespDesc"] == "Success")
        //    {
        //        lblCreditCardNumber.Text = Convert.ToString(hideCreditCardNumber.Value);
        //        LblBalanceAmount.Text = objResult["Amount"];
        //    }

        //}
        protected void btnpaynowWithouAmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Card/PaymentProcess.aspx?ReqFrom=XQvbefr");
        }

        #endregion

        #region Helper methods
        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <remarks></remarks>
        public void LoadPage()
        {
            string accountnumber = "";
            if (Session[sessionAccountNumber] != null)
                accountnumber = Convert.ToString(Session[sessionAccountNumber]);
            else
                Response.Redirect("~/Login.aspx");

            //Literal2.Text = Constants.AccountSummaryOn + string.Format(format2, DateTime.Now);
            //Literal6.Text = Constants.CardSummaryOn + string.Format(format2, DateTime.Now);
            //Literal3.Text = Constants.LastBillSummaryDated + Convert.ToString(string.Format(format2, AccountSummaryManager.GetDateToDisplay(accountnumber)));
            //Literal4.Text = Constants.RewardPointsSummaryOn + Convert.ToString(string.Format(format2, AccountSummaryManager.GetDateToDisplay(accountnumber)));
            Literal2.Text = Convert.ToString(GeneralMethods.FormatDate(AccountSummaryManager.GetDateForCardSummary(accountnumber)));
            Literal6.Text = Convert.ToString(GeneralMethods.FormatDate(AccountSummaryManager.GetDateForCardSummary(accountnumber)));
            Literal3.Text = Convert.ToString(GeneralMethods.FormatDate(AccountSummaryManager.GetDateToDisplay(accountnumber)));
            //Literal4.Text = Constants.RewardPointsSummaryOn + Convert.ToString(GeneralMethods.FormatDate(AccountSummaryManager.GetDateToDisplay(accountnumber)));

            lblrewardPointDate.InnerText = Convert.ToString(GeneralMethods.FormatDate(AccountSummaryManager.GetDateToDisplay(accountnumber)));

            DateTime now = DateTime.Now;
            DateTime lastDayLastMonth = new DateTime(now.Year, now.Month, 1);
            lastDayLastMonth = lastDayLastMonth.AddDays(-1);
            DateTime marchMonthThisYear = DateTime.Now;
            DateTime marchMonthNextYear = DateTime.Now;
            if (now.Month == 1 || now.Month == 2 || now.Month == 3)
            {
                marchMonthThisYear = new DateTime(now.Year, 3, 31);
                //lblPointsExpire.Text = "Points Expiring by " + Convert.ToString(GeneralMethods.FormatDate(marchMonthThisYear));
            }
            else
            {
                marchMonthNextYear = new DateTime(now.Year + 1, 3, 31);
                //lblPointsExpire.Text = "Points Expiring by " + Convert.ToString(GeneralMethods.FormatDate(marchMonthNextYear));
            }
            //Literal3.Text = Constants.LastBillSummaryDated + string.Format(format2, lastDayLastMonth);
            //Literal4.Text = Constants.RewardPointsSummaryOn + string.Format(format2, DateTime.Now);
            lblPointsExpiringOnDate.InnerText = Convert.ToString(GeneralMethods.FormatDate(marchMonthNextYear));
            //BindSummaryDropdownlist();
            BindCardNumberDropdownlist(accountnumber);
            SetAccountSummary(accountnumber);
            SetSummary(accountnumber);
            BindRequests();
            BindGridUnbilledTransactions(Convert.ToString(Session[sessionAccountNumber]));
            SetRequestTypesURL();
            string SelectedValue = string.IsNullOrEmpty(ddlCardNumber.SelectedValue) ? Cardnumber : ddlCardNumber.SelectedValue; 
            SetCardSummary(SelectedValue);
        }
        ///// <summary>
        ///// Binds the summary dropdownlist.
        ///// </summary>
        //public void BindSummaryDropdownlist()
        //{
        //    string[] AccountSummaryItems = { selectItem, accountSummaryItem, cardSummaryItem };
        //    foreach (string s in AccountSummaryItems)
        //    {
        //        ListItem newItem = new ListItem();
        //        newItem.Text = s.Split('|')[0];
        //        newItem.Value = s.Split('|')[1];
        //        if (s.Split('|')[0].Replace(" ", "").ToLower() == accountSummary)
        //            newItem.Selected = true;
        //        ddlAccountSummary.Items.Add(newItem);
        //    }
        //}


        /// <summary>
        /// Sets the request types URL.
        /// </summary>
        /// <remarks></remarks>
        private void SetRequestTypesURL()
        {
            try
            {

                if (Session[sessionCardHolderRequestTypes] != null)
                {
                    List<CH_RequestType_MstDTO> lstRequestTypes = (List<CH_RequestType_MstDTO>)Session[sessionCardHolderRequestTypes];
                    foreach (CH_RequestType_MstDTO RequestType in lstRequestTypes)
                    {
                        string requestTypeName = RequestType.RequestType_nm.Replace(" ", "").Replace("-", "").ToLower();
                        if (requestTypeName == "atmpinregenerationrequest") //1
                            hlnkATM_PIN_REGENERATION.NavigateUrl = "~/ServiceRequest/ATM_PIN_Regeneration.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "blockingofcard") //12 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                            hlnkBLOCKINGCARD.NavigateUrl = "~/ServiceRequest/BlockingCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        //Commented for this release on 28_03_2019
                        else if (requestTypeName == "internationllimit")
                            hlnkInternationalLimitOpenClose.NavigateUrl = "~/ServiceRequest/InternationlLimitOpenClose.ASPX?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if(requestTypeName == "bonuspointredemption")
                            hlnkBonusPointRedemption.PostBackUrl = "~/ServiceRequest/BonusPointRedemption.ASPX?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                    }
                }
                else
                {
                    CardHolderRequestManager objCardHolderRequestType = new CardHolderRequestManager();
                    List<CH_RequestType_MstDTO> lstRequestTypes = objCardHolderRequestType.getCHRequestTypes();
                    foreach (CH_RequestType_MstDTO RequestType in lstRequestTypes)
                    {
                        string requestTypeName = RequestType.RequestType_nm.Replace(" ", "").Replace("-", "").ToLower();
                        if (requestTypeName == "atmpinregenerationrequest") //1
                            hlnkATM_PIN_REGENERATION.NavigateUrl = "~/ServiceRequest/ATM_PIN_Regeneration.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();

                        else if (requestTypeName == "blockingofcard")//11
                            hlnkBLOCKINGCARD.NavigateUrl = "~/ServiceRequest/BlockingCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();

                        else if (requestTypeName == "internationllimit") //12 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                            hlnkInternationalLimitOpenClose.NavigateUrl = "~/ServiceRequest/InternationlLimitOpenClose.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();

                    }
                    Session[sessionCardHolderRequestTypes] = lstRequestTypes;
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.strErrorRequestTypes + "');", true);
            }
        }
        /// <summary>
        /// Binds the summary dropdownlist.
        /// </summary>
        /// <remarks></remarks>
        //private void BindSummaryDropdownlist()
        //{
        //    try
        //    {

        //        DropdownHdrManager dhm = new DropdownHdrManager();
        //        List<DropDown_HdrDTO> list = dhm.SearchDllHeader("Account_Summary").ToList();
        //        if (list.Count > 0)
        //        {
        //            ddlAccountSummary.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
        //            ddlAccountSummary.DataTextField = "Description";
        //            ddlAccountSummary.DataValueField = "DropDown_Dtl_Id";
        //            ddlAccountSummary.DataBind();
        //            ddlAccountSummary.Items.Insert(0, new ListItem("--Select Summary--", "0"));
        //        }
        //        ddlAccountSummary.SelectedIndex = ddlAccountSummary.Items.IndexOf(ddlAccountSummary.Items.FindByText("Account Summary"));

        //    }

        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }

        //}
        /// <summary>
        /// Binds the card number dropdownlist.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void BindCardNumberDropdownlist(string accountNumber)
        {
            try
            {

                ddlCardNumber.Items.Clear();               
                //List<CH_CardDTO> lstCardnumbers = new CardManager().GetCardList(accountNumber);
                List<CH_CardDTO> lstCardnumbers = new CardManager().GetAllCardsForATMPinReg(new CH_CardDTO() { Cr_Account_Nbr = accountNumber });
                if (lstCardnumbers != null)
                {
                    foreach (var item in lstCardnumbers)
                    {
                        if (item.BASIC_CARD_FLAG == "0")
                        {
                            item.MASK_CARD_NUMBER = "Primary " + item.MASK_CARD_NUMBER;
                        }
                        else
                        {
                            item.MASK_CARD_NUMBER = "Add-On " + item.MASK_CARD_NUMBER;
                        }
                    }

                    if (lstCardnumbers.Count > 1)
                    {
                        ddlCardNumber.DataSource = lstCardnumbers;
                        ddlCardNumber.DataTextField = "MASK_CARD_NUMBER";
                        ddlCardNumber.DataValueField = "CARD_NUMBER";
                        ddlCardNumber.DataBind();
                        drpCardlist.Attributes.CssStyle.Add("display", "block");
                        divCardNumber.Visible = false;
                    }
                    else
                    {

                        Cardnumber = lstCardnumbers[0].card_number;
                        string StartCardnumber = "";
                        string EndCardnumber = "";
                        if (Cardnumber != "")
                        {
                            StartCardnumber = Cardnumber.Substring(0, 4);
                            if (Cardnumber.Length == 16)
                                EndCardnumber = Cardnumber.Substring(13, 3);
                        }

                        lblCreditCardNumber.Text = StartCardnumber + "XXXXXXXXX" + EndCardnumber;
                        drpCardlist.Attributes.CssStyle.Add("display", "none");
                        divCardNumber.Visible = true;
                    }
                }

                //ddlCardNumber.Items.Insert(0, new ListItem(Constants.selectSummary, zero));
            }

            catch (Exception ex)
            {
                string str = ex.Message;
            }

        }
        /// <summary>
        /// Sets the account summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void SetAccountSummary(string accountNumber)
        {
            try
            {

                ClearItems();
                CH_CardDTO objAccountSummary = new AccountSummaryManager().GetAccountSummary(accountNumber);
                if (accountNumber != "")
                {
                    if (objAccountSummary != null)
                    {
                        lblTotalOutstanding.InnerText = Convert.ToString(objAccountSummary.Account_Total_Outstanding);
                        //  lblUnbilledOutstanding.InnerText = Constants.rupees + Convert.ToString(objAccountSummary.Account_UnBilled_Outstanding);
                        lblTotalLimit.InnerText = Convert.ToString(objAccountSummary.Account_Total_Account_Limit);
                        //lblAvailableLimit.InnerText = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
                        lblTotalCashLimit.InnerText = Convert.ToString(objAccountSummary.Account_Total_Account_Cash_Limit);
                        lblAvailableCashLimit.InnerText = Convert.ToString(objAccountSummary.Account_Avl_Account_Cash_Limit);
                    }
                }

                if (Request.Params["requestFlag"] != null)
                {
                    CreateRequest(accountNumber, objAccountSummary);
                }
                else
                {
                    lblAvailableLimit.InnerText = Convert.ToString(Session["AvaiableAmount"]);
                }
            }

            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlcardlist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlCardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedCardNumber"] = ddlCardNumber.SelectedValue;
            string accountnumber = "";
            hideCreditCardNumber.Value = ddlCardNumber.SelectedValue;

            if (Session[sessionAccountNumber] != null)
                accountnumber = Convert.ToString(Session[sessionAccountNumber]);
            else
                Response.Redirect("~/Login.aspx");

            Literal6.Visible = true;
            Literal2.Visible = false;
            SetCardSummary(Convert.ToString(ddlCardNumber.SelectedValue.Trim()));
            SetSummary(accountnumber);
            BindCardNumberDropdownlist(accountnumber);
            ddlCardNumber.SelectedValue = Session["selectedCardNumber"].ToString();
        }

        /// <summary>
        /// This method send request to Connect api for balance enquiry 
        /// </summary>
        private void CreateRequest(string accountnumber, CH_CardDTO objAccountSummary)
        {
            string result = string.Empty;
            Helper objHelper = new Helper();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string TransRefNo = objHelper.RandomDigits();
            CommonRequest objBalanceRequest = new CommonRequest()
            {
                TxnType = TranscationType.BA.ToString(),
                CardNumber = Convert.ToString(accountnumber),
                TransRefNo = TransRefNo,
                TransDateTime = Regex.Replace(Convert.ToString(DateTime.Now), @"[^0-9a-zA-Z]+", ""),
                Flag = "A"

            };
            string jsondata = js.Serialize(objBalanceRequest);
            result = objHelper.GetResponse(jsondata);
            logger.Info("Jetty Server Response String:" + result);
            dynamic objResult = null;
            if (result == null)
            {
                lblAvailableLimit.InnerText = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
                hideAvaiableAmount.Value = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
            }
            else
            {
                objResult = js.Deserialize<dynamic>(result);
                DisplayMessage(objResult, objAccountSummary);
            }
            Session["AvaiableAmount"] = lblAvailableLimit.InnerText;
        }


        private void DisplayMessage(dynamic result, CH_CardDTO objAccountSummary)
        {
            if (result != null)
            {
                if (result["RespCode"] == "000")
                {
                    if (result["Amount"] != null)
                    {
                        lblAvailableLimit.InnerText = Constants.rupees + Convert.ToString(result["Amount"]);
                        hideAvaiableAmount.Value = Constants.rupees + Convert.ToString(result["Amount"]);
                    }
                    else
                    {
                        lblAvailableLimit.InnerText = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
                        hideAvaiableAmount.Value = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
                    }
                }
                else if (result["RespCode"] == "204")
                {
                    string scriptText = "alert('" + Constants.ErrorCode204 + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                }
                else
                {
                    string scriptText = "alert('" + Constants.msg + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
                }
            }
            else
            {
                lblAvailableLimit.InnerText = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
                hideAvaiableAmount.Value = Constants.rupees + Convert.ToString(objAccountSummary.Account_Avl_Account_Limit);
            }

        }
        /// <summary>
        /// Sets the summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void SetSummary(string accountNumber)
        {
            ClearItemsLastBillSummary();
            ClearItemsRewardPointSummary();
            if (accountNumber != "")
            {
                CH_CR_TERMDTO objSummary = new AccountSummaryManager().GetSummary(accountNumber);
                if (objSummary != null)
                {
                    lblTotalAmountDue.InnerText = Convert.ToString(objSummary.Total_Amount_Due);
                    //lblBilledOpeningBal.InnerText = Convert.ToString(objSummary.BILLED_OPENING_BAL);
                    // lblLastBillTotalOutstanding.InnerText = Constants.rupees + Convert.ToString(objSummary.Total_Outstanding);
                    //lbltotalCredits.InnerText =  Convert.ToString(objSummary.TOTAL_CREDITS);
                    //lbltotalDebits.InnerText =  Convert.ToString(objSummary.TOTAL_DEBITS);
                    lblMinimumAmoutDue.InnerText = Convert.ToString(objSummary.Minimum_Amount_Due);
                    // lblStatementDt.InnerText = Convert.ToString(string.Format(format1, objSummary.Stat_Date)).Replace("-", "/");
                    // lblPaymentDueDate.InnerText = Convert.ToString(string.Format(format1, objSummary.Payment_Due_Date)).Replace("-", "/");
                    lblStatementDat.Text = Convert.ToString(GeneralMethods.FormatDate(objSummary.Stat_Date));
                    lblPaymentDueDate.Text = Convert.ToString(GeneralMethods.FormatDate(objSummary.Payment_Due_Date));


                    lblAmountReceived.InnerText = Convert.ToString(objSummary.Amount_Received);
                    if ((DateTime)objSummary.Paymnet_Received_Date == DateTime.MinValue)
                        lblPaymentReceivedDate.Text = "";
                    else
                        //    lblPaymentReceivedDate.InnerText = Convert.ToString(string.Format(format1, objSummary.Paymnet_Received_Date)).Replace("-", "/");
                        lblPaymentReceivedDate.Text = Convert.ToString(GeneralMethods.FormatDate(objSummary.Paymnet_Received_Date));
                    SetRewardsSummary(accountNumber);
                }
            }
        }


        private void BindRequests()
        {
            long cardholderID;
            //int pRecordCount = 2;
            //int SkipCount = 0;
            cardholderID = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            CHRequestDetailManager crdm = new CHRequestDetailManager();
            //lstRequestDTO = crdm.getCHRequestDetails(cardholderID, SkipCount, 2, ref pRecordCount);
            //CardHolderComplaintManager chcm = new CardHolderComplaintManager();           
            //var combine = lstRequestDTO.Concat(lstComplaintDTO);


            //List<CH_Request_DtlDTO> chdto = new List<CH_Request_DtlDTO> { };
            lstRequestDTO = crdm.GetRequestStatusRecord(cardholderID);
            if (lstRequestDTO != null && lstRequestDTO.Count > 0)
            {
                divRequest_ComplaintStatus.Visible = true;
                divNoDataRequest_ComplaintStatus.Visible = false;
                hyperlnkRequest.Visible = true;
                gvRequestCH.DataSource = lstRequestDTO;
                gvRequestCH.DataBind();
                gvRequestCH.Visible = true;
            }
            else
            {
                divRequest_ComplaintStatus.Visible = false;
                divNoDataRequest_ComplaintStatus.Visible = true;
                hyperlnkRequest.Visible = false;
            }
        }

        protected void gvRequestCH_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblrqstID = (Label)e.Row.FindControl("lblrqstID");
                HiddenField hdnRequestorComplaint = (HiddenField)e.Row.FindControl("hdnRequestorComplaint");
                if (hdnRequestorComplaint.Value == "Request")
                {
                    lblrqstID.Text = "Request Number:  " + lblrqstID.Text;
                }
                else if (hdnRequestorComplaint.Value == "Complaint")
                {
                    lblrqstID.Text = "Complaint Number:  " + lblrqstID.Text;
                }

                string Status = (e.Row.FindControl("lblRequestStatus") as Label).Text;
                HtmlImage img = (HtmlImage)e.Row.FindControl("StatusImage");
                if (Status == "Failed")
                {
                    //Access the image tag
                    img.Src = this.Page.GetNewImagePath("Fail.svg");
                }
                else if (Status == "Resolved" || Status == "Approved")
                {
                    img.Src = this.Page.GetNewImagePath("Success.svg");
                }
                else if (Status == "Pending")
                {
                    img.Src = this.Page.GetNewImagePath("Pending.svg");
                }
                else if (Status == "In Process")
                {
                    img.Src = this.Page.GetNewImagePath("Process.svg");
                }
                else if (Status == "Rejected")
                {
                    img.Src = this.Page.GetNewImagePath("Reject.svg");
                }

            }

        }



        protected void gvRequestCH_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRequest")
            {
                try
                {
                    //GridViewRow selectedRow = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    //int RowIndexx = Convert.ToInt32(selectedRow.RowIndex);

                    ////gvRequestCH.Rows[RowIndexx].BorderStyle = BorderStyle.Double;
                    ////gvRequestCH.Rows[RowIndexx].box = 1;
                    ////gvRequestCH.Rows[RowIndexx].BorderColor = System.Drawing.Color.RoyalBlue;
                    //gvRequestCH.Rows[RowIndexx].BorderColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2"); 

                    ////Button O1Button = (Button)row.FindControl("O1Button");
                    ///

                    //if (e.Row.RowType == DataControlRowType.DataRow)
                    //{
                    //    e.Row.CssClass = "highlight";
                    //}





                    //foreach (GridViewRow row in gvRequestCH.Rows)
                    //{
                    //    if (row.RowIndex == gvRequestCH.SelectedIndex)
                    //    {
                    //        row.BorderColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");

                    //    }
                    //    else
                    //    {
                    //        row.BorderColor = System.Drawing.Color.Red;
                    //    }
                    //}


                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    row.CssClass = "HighlightBorder";
                    //e.CommandSource.FindControl("myLabel")
                    ////use findcontrol to locate the butotn
                    //Button btn = e.Row.FindControl("LockoutStatus") as Button;

                    ////change the class based on a column value
                    //if (row["ColumnName"].ToString() == "LockedOut")
                    //{
                    //    btn.CssClass = "ClassA";
                    //}
                    string viewRequest = string.Empty;
                    HiddenField hdnRequestorComplaint = (HiddenField)row.FindControl("hdnRequestorComplaint");
                    if (hdnRequestorComplaint.Value == "Request")
                    {
                        viewRequest = CardHolder.BAL.Controller.CHRequestView.ProcessTemplate(e.CommandArgument);
                    }
                    else if (hdnRequestorComplaint.Value == "Complaint")
                    {
                        viewRequest = CardHolder.BAL.Controller.CHComplaintView.ProcessTemplate(e.CommandArgument);
                    }

                    //viewRequest = CardHolder.BAL.Controller.CHRequestView.ProcessTemplate(e.CommandArgument);
                    ltrDetail.Text = viewRequest;
                }
                catch (Exception)
                {

                }
                //ClientScript.RegisterStartupScript(typeof(Page), "loadPopupBox", "loadPopupBox();", true);
                ClientScript.RegisterStartupScript(typeof(Page), "Pop", "openModal();", true);
            }
        }

        protected void grdUnbilledTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                decimal amount = Convert.ToDecimal(lblAmount.Text);
                //decimal amount = int.Parse(e.Row.Cells[1].Text);
                Image imgCreditTag = (Image)e.Row.FindControl("imgCreditTag");
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (amount < 0)
                    {
                        lblAmount.Text = Convert.ToString(Math.Abs(amount)); // remove negaitve sign from amnount
                        imgCreditTag.ImageUrl = this.Page.GetNewImagePath("credit_tag.svg");
                    }
                }
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
            TransactionManager tm = new TransactionManager();
            lstUnbilledTransactionsDTO = tm.GetUnbilledTransactions(SkipCount, this.Pager1.PageSize, ref pRecordCount, accountNumber, 1);
            if (lstUnbilledTransactionsDTO == null)
            {
                pRecordCount = 0;
                divUnbilledTransaction.Visible = false;
                divNoDataUnbilledTransaction.Visible = true;
                HyperLinkUnbilledTransaction.Visible = false;
            }
            else
            {
                divUnbilledTransaction.Visible = true;
                divNoDataUnbilledTransaction.Visible = false;
                HyperLinkUnbilledTransaction.Visible = true;
            }
            this.RecordCount = pRecordCount;
            this.Pager1.Visible = this.RecordCount > this.Pager1.PageSize;
            grdUnbilledTransactions.DataSource = lstUnbilledTransactionsDTO;
            grdUnbilledTransactions.DataBind();
        }
        /// <summary>
        /// Sets the rewards summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <remarks></remarks>
        public void SetRewardsSummary(string accountNumber)
        {
            if (accountNumber != "")
            {
                CH_CR_TERMDTO objSummary = new AccountSummaryManager().GetRewardPointsSummary(accountNumber);
                if (objSummary != null && objSummary.PTS_CLOSING <= 0)
                {
                    divPointSummary.Visible = false;
                    divNoDataPointSummary.Visible = true;
                    lblClosingBalance.InnerText = Convert.ToString(objSummary.PTS_CLOSING);
                }
                else if (objSummary != null)
                {
                    divPointSummary.Visible = true;
                    divNoDataPointSummary.Visible = false;
                    lblOpeningBalance.InnerText = Convert.ToString(objSummary.PTS_OPENING);
                    lblEarnedForMonth.InnerText = Convert.ToString(objSummary.Earned_For_The_Month);
                    lblRedeemedForMonth.InnerText = Convert.ToString(objSummary.Redeemed_For_The_Month);
                    lblClosingBalance.InnerText = Convert.ToString(objSummary.PTS_CLOSING);
                    lblPointsExpiring.InnerText = Convert.ToString(objSummary.Points_Expiring);
                    lblrewardPoints.InnerText = Convert.ToString(objSummary.PTS_CLOSING);
                    lblClosingBalancePoint.InnerText = Convert.ToString(objSummary.PTS_CLOSING);
                }
            }

        }
        /// <summary>
        /// Sets the card summary.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <remarks></remarks>
        public void SetCardSummary(string cardNumber)
        {
            ClearItems();
            CH_CardDTO objCardSummary = new CardManager().GetCardSummary(cardNumber);
            if (objCardSummary != null)
            {
                //lblnameoncard.InnerText = Convert.ToString(objCardSummary.Embossed_Name);
                lblTotalOutstanding.InnerText = Convert.ToString(objCardSummary.Card_Total_Outstanding);
                lblTotalLimit.InnerText = Convert.ToString(objCardSummary.Card_Total_Account_Limit);
                lblAvailableLimit.InnerText = Convert.ToString(Math.Round((Double)objCardSummary.Card_Avl_Account_Limit, 2));
                lblTotalCashLimit.InnerText = Convert.ToString(objCardSummary.Card_Total_Account_Cash_Limit);
                lblAvailableCashLimit.InnerText = Convert.ToString(Math.Round((Double)objCardSummary.Card_Avl_Account_Cash_Limit, 2));
                lblCreditUsedAmount.InnerText = Convert.ToString(Math.Round((Double)objCardSummary.Card_Total_Account_Limit - objCardSummary.Card_Avl_Account_Limit, 2));
                lblCashUsedAmount.InnerText = Convert.ToString(Math.Round((Double)objCardSummary.Card_Total_Account_Cash_Limit - objCardSummary.Card_Avl_Account_Cash_Limit, 2));
                double creditUsedPer = (objCardSummary.Card_Total_Account_Limit - objCardSummary.Card_Avl_Account_Limit) * 100 / objCardSummary.Card_Total_Account_Limit;
                double cashUsedPer = (objCardSummary.Card_Total_Account_Cash_Limit - objCardSummary.Card_Avl_Account_Cash_Limit) * 100 / objCardSummary.Card_Total_Account_Cash_Limit;
                hideprogressbarWidth.Value = Convert.ToString(creditUsedPer);
                hidecashProgressBarWidth.Value = Convert.ToString(cashUsedPer);
                ClientScript.RegisterStartupScript(typeof(Page), "ProgressBar", "progressBar();", true);
            }
        }
        /// <summary>
        /// Clears the items.
        /// </summary>
        /// <remarks></remarks>
        public void ClearItems()
        {
            //lblnameoncard.InnerText = strNotFound;
            lblTotalOutstanding.InnerText = strNotFound;
            // lblUnbilledOutstanding.InnerText = strNotFound;
            lblTotalLimit.InnerText = strNotFound;
            lblAvailableLimit.InnerText = strNotFound;
            lblTotalCashLimit.InnerText = strNotFound;
            lblAvailableCashLimit.InnerText = strNotFound;
        }
        /// <summary>
        /// Clears the items.
        /// </summary>
        /// <remarks></remarks>
        public void ClearItemsLastBillSummary()
        {
            lblTotalAmountDue.InnerText = strNotFound;
            //lblLastBillTotalOutstanding.InnerText = strNotFound;
            lblMinimumAmoutDue.InnerText = strNotFound;
            lblPaymentDueDate.Text = strNotFound;
            lblAmountReceived.InnerText = strNotFound;
            lblPaymentReceivedDate.Text = strNotFound;
            //lbltotalDebits.InnerText = strNotFound;
            //lbltotalCredits.InnerText = strNotFound;
            lblStatementDat.Text = strNotFound;
            //lblBilledOpeningBal.InnerText = strNotFound;

        }
        /// <summary>
        /// Clears the items last bill summary.
        /// </summary>
        /// <remarks></remarks>
        public void ClearItemsRewardPointSummary()
        {
            lblOpeningBalance.InnerText = strNotFound;
            lblEarnedForMonth.InnerText = strNotFound;
            lblRedeemedForMonth.InnerText = strNotFound;
            lblClosingBalance.InnerText = strNotFound;
            lblPointsExpiring.InnerText = strNotFound;
        }


        #endregion
    }
}

