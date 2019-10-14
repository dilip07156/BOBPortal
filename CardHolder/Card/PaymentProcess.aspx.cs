using System;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.bobibanking;
using CardHolder.Utility.Payment;
using CardHolder.Utility.Enums;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Text.RegularExpressions;
using NLog;
using com.bob.utils.security;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml;

namespace CardHolder.Card
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class PaymentProcess : PageBase
    {
        #region Variables


        /// <summary>
        /// 
        /// </summary>
        string sessionAmountDue = "AmountDue";
        /// <summary>
        /// 
        /// </summary>
        string totalUnbilledAmount = "totalUnbilledAmount";
        /// <summary>
        /// 
        /// </summary>
        string success = "success";
        /// <summary>
        /// 
        /// </summary>
        string unsuccessful = "unsuccessful";

        string qsk = "qcv45dnr";

        string queryString = "f={0},{1},{2},{3},{4},{5}";

        private static Logger logger = LogManager.GetCurrentClassLogger();


        string CreditAccNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number; //Added by Sahil on 22'Dec14
        string CreditCardNumber = CardHolderManager.GetLoggedInUser().CH_Card.card_number.Encrypt(); //Added by Sahil on 22'Dec14
        string CardHolderName = CardHolderManager.GetLoggedInUser().CH_Card.Embossed_Name; //Added by Sahil on 22'Dec14

        string key = "29304E875832789229304E8758327892";

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
            //Session.Timeout = 10;           
            txtAccountNumber.Attributes.Add("readonly", "readonly");
            txtCreditCardNumber.Attributes.Add("readonly", "readonly");
            txtUnbilledTransactionAmt.Attributes.Add("readonly", "readonly");
            CustomerID.Value = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 1).ToString();

            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ReqFrom"])))
                divamt.Visible = false;
            else
                divamt.Visible = true;


            if (Request.Params["msg"] != null) //Response from Billdesk
            {
                string msg = Request.Params["msg"];
                try
                {
                    ProcessResponseFromBillDesk(msg);
                    return;
                }
                catch (Exception)
                {
                    DisplayMessage(Constants.PaymentError, true);
                    lkbRedirectToCardStatement.Visible = false;
                    divDisplayAll.Visible = true;
                }
            }
            if (Request.Form["QS"] != null) //Response from Bobibanking
            {
                string encdata = Request.Form["QS"];
                
                try
                {    
                    //ProcessResponseFromBobibanking(encdata);
                    ProcessResponseFromBobibankingNew(encdata);                   
                    return;
                }
                catch (Exception)
                {
                    DisplayMessage(Constants.PaymentError, true);
                    lkbRedirectToCardStatement.Visible = false;
                    divDisplayAll.Visible = true;
                }
            }

            if (IsXsrf) { }
            else
            {
                if (!IsPostBack)
                    LoadPage();
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the btnPaynow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnPaynow_Click(object sender, EventArgs e)
        {
            //int amount = 0;
            double amount = 0;
            try
            {
                try
                {
                    // amount = Convert.ToInt32(txtEnterAmount.Text.Trim());
                    amount = Convert.ToDouble(txtEnterAmount.Text.Trim());
                }
                catch (Exception)
                {
                    DisplayMessage(Constants.Error1, true);
                    return;
                }

                if (rblPaymentOptions.SelectedIndex == -1)
                {
                    DisplayMessage(Constants.Error2, true);
                    return;
                }
                if (amount == 0)
                {
                    DisplayMessage(Constants.amounterror, true);
                    return;
                }
                if (rblPaymentOptions.SelectedValue == "1") //Request for Bobibanking Payment  
                {
                    SendRequestToBobibanking();
                }
                else if (rblPaymentOptions.SelectedValue == "2") //Request for BillDesk Payment                
                {
                    SendRequestToBillDesk();
                }
                else
                {
                    DisplayMessage(Constants.Error2, true);
                    return;
                }
                //System.Threading.Thread.Sleep(10000);
                //CreateRequest(amount);
            }
            catch
            {
                DisplayMessage(Constants.PaymentError, true);
                lkbRedirectToCardStatement.Visible = false;
                divDisplayAll.Visible = true;
            }
        }
        #endregion

        #region Helper Methods



        private void CreateRequest(double amount)
        {
            string result = string.Empty;
            Helper objHelper = new Helper();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string TransRefNo = objHelper.RandomDigits();
            PaymentAcknowledgementRequest objPaymentAcknowledgeRequest = new PaymentAcknowledgementRequest()
            {
                TxnType = TranscationType.PA.ToString(),
                CardNumber = Convert.ToString(CreditCardNumber.Decrypt()),
                TransRefNo = TransRefNo,
                TransDateTime = String.Format("{0:MM/dd/yyyy}", DateTime.Now),
                Amount = amount,
                BankRefNumber = Convert.ToString(CreditAccNumber.Decrypt()),
                Narration = "xxxx"
            };
            string jsondata = js.Serialize(objPaymentAcknowledgeRequest);
            result = objHelper.GetResponse(jsondata);
            logger.Info("Jetty Server Response String:" + result);
            dynamic objResult = null;
            if (result == null)
            {
                string scriptText = "alert('" + Constants.CommonError + "" + "'); window.location='" + Request.ApplicationPath + "AccountSummary/AccountSummary.aspx'";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", scriptText, true);
            }
            else
            {
                objResult = js.Deserialize<dynamic>(result);
                //DisplayResponseMessage(objResult);
            }

            //SaveAuditLog(TransRefNo, objResult);
        }




        ////private void SaveAuditLog(string TransRefNo, dynamic objResult)
        ////{
        ////    CHRequestDetailManager crdm = new CHRequestDetailManager();
        ////    String responseStatus = string.Empty;
        ////    if (objResult != null)
        ////    {
        ////        responseStatus = objResult["RespDesc"];
        ////    }
        ////    else
        ////    {
        ////        responseStatus = "Null Response from Jetty Server ";
        ////    }

        ////    long RequestDtlID = crdm.SaveAuditLog(new AuditLog_DTO()
        ////    {
        ////        RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
        ////        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
        ////        TxnType = TranscationType.PC.ToString(),
        ////        Credit_card_number = Convert.ToString(hideCreditCardNumber.Value).Encrypt(),
        ////        TxnReferenceNo = TransRefNo,
        ////        ResponseStatus = responseStatus,
        ////        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
        ////        Created_dt = DateTime.Now,
        ////        BankRefNo = Session["crAccNum"].ToString(),
        ////        IP_Address = Request.UserHostAddress
        ////    });
        ////}
        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPage()
        {
            if (CardHolderManager.GetLoggedInUser() == null)
                Response.Redirect("~/Login.aspx");
            DisplayMessage("", true);
            divDisplayAll.Visible = true;
            btnPrint.Visible = false;
            btnPrintBillDesk.Visible = false;
            txtAccountNumber.Text = CreditAccNumber.Decrypt();

            string Cardnumber = CreditCardNumber.Decrypt();
            string StartCardnumber = "";
            string EndCardnumber = "";
            if (Cardnumber != "")
            {
                StartCardnumber = Cardnumber.Substring(0, 4);
                if (Cardnumber.Length == 16)
                    EndCardnumber = Cardnumber.Substring(13, 3);
            }

            txtCreditCardNumber.Text = StartCardnumber + "XXXXXXXXX" + EndCardnumber;
            //double amountDue = Convert.ToInt64(Session[sessionAmountDue]);
            double amountDue = Convert.ToDouble(Session[sessionAmountDue]);
            //double TotalUnbilledAmount = Convert.ToInt64(Session[totalUnbilledAmount]);
            double TotalUnbilledAmount = Convert.ToDouble(Session[totalUnbilledAmount]);
            //if (Session[sessionAmountDue] != null)
            //{
            //    txtUnbilledTransactionAmt.Text = Convert.ToString(amountDue);
            //    txtEnterAmount.Text = Convert.ToString(amountDue);
            //}
            //else if ((Session[totalUnbilledAmount]) != null)
            if ((Session[totalUnbilledAmount]) != null)
            {
                txtUnbilledTransactionAmt.Text = Convert.ToString(TotalUnbilledAmount);
                txtEnterAmount.Text = Convert.ToString(TotalUnbilledAmount);
                Session[totalUnbilledAmount] = null;
            }
            else
            {
                txtUnbilledTransactionAmt.Text = "0";
                txtEnterAmount.Text = "0";
            }
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <remarks></remarks>
        private void DisplayMessage(string msg, bool visible)
        {
            lblMessageDisplay.Visible = visible;
            lblMessageDisplay.Text = msg;
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetBobibankingKeyPath()
        {
            string FilePath = string.Empty;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(Constants.BobibankingKeyPath);
            if (obj != null && obj.Parameter_ValueC != null)
                FilePath = Convert.ToString(obj.Parameter_ValueC);
            return FilePath.Trim();
        }

        #region bobibanking

        /// <summary>
        /// Sends the request to bobibanking.
        /// </summary>
        /// <remarks></remarks>
        private void SendRequestToBobibanking()
        {
            string customerAccNo = CreditAccNumber.Decrypt(); // by sahil
            CH_Bobibanking_Payment_Status_DtlDTO chpsdto = new CH_Bobibanking_Payment_Status_DtlDTO();
            string customerName = CardHolderName;
            string billdeskOnlineId = CardHolderManager.GetLoggedInUser().CH_Card.BILLDESK_ONLINE_ID;
            string ITC = customerAccNo + "-" + customerName;
            if (billdeskOnlineId != null && billdeskOnlineId != "")
                ITC = billdeskOnlineId + "-" + customerAccNo + "-" + customerName;

            string transactionNumber = "";
            try
            {
                if (Convert.ToDouble(txtEnterAmount.Text.Trim()) == 0)
                {
                    DisplayMessage(Constants.Error1, true);
                    return;
                }
                //double amountDue = Convert.ToInt64(Session[sessionAmountDue]);
                double amountDue = Convert.ToDouble(Session[sessionAmountDue]);
                //double TotalUnbilledAmount = Convert.ToInt64(Session[totalUnbilledAmount]);
                double TotalUnbilledAmount = Convert.ToDouble(Session[totalUnbilledAmount]);


                //------------------------------Refer Email RE: Issues / Bugs Identified 03/20/2015 12:13 PM------------------------------//
                //if (amountDue > 0)
                //{
                //    if (Convert.ToDouble(txtEnterAmount.Text.Trim()) > amountDue)
                //    {
                //        DisplayMessage(Constants.Error3, true);
                //        return;
                //    }
                //}
                //else if (TotalUnbilledAmount > 0)
                //{
                //    if (Convert.ToDouble(txtEnterAmount.Text.Trim()) > TotalUnbilledAmount)
                //    {
                //        DisplayMessage(Constants.Error3, true);
                //        return;
                //    }
                //}
                //------------------------------Refer Email RE: Issues / Bugs Identified 03/20/2015 12:13 PM------------------------------//

                DisplayMessage("", false);
               
                chpsdto.BobiBanking_PaymentStatus_Id = 0;
                chpsdto.Creditcard_acc_number = CreditAccNumber; //changes by Sahil on 22'Dec14
                chpsdto.Credit_card_number = CreditCardNumber; //changes by Sahil on 22'Dec14
                chpsdto.BankId = Constants.BankId;
                chpsdto.PID = Constants.PID;
                chpsdto.PRN = CustomerID.Value + DateTime.Now.ToString("ddMMyyyyHHmmss");
                hdnPRN.Value = chpsdto.PRN;
                chpsdto.ITC = ITC;
                chpsdto.AmountDue = Convert.ToDouble(txtEnterAmount.Text.Trim());
                chpsdto.PaymentStatus = unsuccessful;
                chpsdto.Created_dt = DateTime.Now;
                chpsdto.Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                chpsdto.IP_Address = Request.UserHostAddress;
                transactionNumber = SaveCardHolderPaymentStatusForBobibanking(chpsdto, "", "", 0, "", "", "", 1, "");
            }
            catch (Exception)
            {
                DisplayMessage(Constants.GeneralErrorMessage, true);
                divDisplayAll.Visible = true;
                lkbRedirectToCardStatement.Visible = false;
                return;
            }
            if (transactionNumber != "") //To keep response.redirect out of try catch block
            {
                //if (Convert.ToInt32(txtEnterAmount.Text.Trim()) > 0 && rblPaymentOptions.SelectedValue == "1" && ITC != "")
                if (Convert.ToDouble(txtEnterAmount.Text.Trim()) > 0 && rblPaymentOptions.SelectedValue == "1" && ITC != "")
                    //Response.Redirect("~/Bobibanking_payment_process.aspx?AMT=" + txtEnterAmount.Text.Trim().Encrypt() + "&PRN=" + Convert.ToString(CustomerID.Value).Encrypt() + "&ITC=" + ITC.Encrypt(), false);
                     Response.Redirect("~/Bobibanking_payment_process.aspx?AMT=" + txtEnterAmount.Text.Trim().Encrypt() + "&PRN=" + Convert.ToString(chpsdto.PRN).Encrypt() + "&ITC=" + ITC.Encrypt(), false);
                else
                {
                    DisplayMessage(Constants.PaymentError, true);
                    lkbRedirectToCardStatement.Visible = false;
                    divDisplayAll.Visible = true;
                }
            }
            else
            {
                DisplayMessage(Constants.PaymentError, true);
                lkbRedirectToCardStatement.Visible = false;
                divDisplayAll.Visible = true;
            }
        }
        /// <summary>
        /// Processes the response from bobibanking.
        /// </summary>
        /// <param name="msgResponse">The MSG response.</param>
        /// <remarks></remarks>
        //private void ProcessResponseFromBobibanking(string msgResponse)
        //{
        //    string rootFilePath = GetBobibankingKeyPath();
        //    string bobKeyFilePath = rootFilePath + Constants.bobibankingFileName.Trim();
        //    string msg = BobibankingEncryptionDecryption.Decrypt(msgResponse, bobKeyFilePath);
        //    var bobibankingResponse = new BobibankingResponse(msg);
           
        //    try
        //    {
        //        string StartCardnumber1 = "";
        //        string EndCardnumber1 = "";
        //        string EncCardNumber = "";
        //        double amount = 0;
        //        string PRN = string.Empty;
        //        string bid = string.Empty;
        //        string debtAccountNo = string.Empty;
        //        string itc = string.Empty;
        //        string retVal = string.Empty;
        //        bool transactionUpdateStatus = false;
        //        string accountNumber = CreditAccNumber; //changes by Sahil on 22'Dec14
        //        string cardnumber = CreditCardNumber.Decrypt(); // Added by Sahil on 22'Dec14
        //        if (cardnumber != "")
        //        {
        //            // cardnumber = cardnumber.Decrypt();
        //            StartCardnumber1 = cardnumber.Substring(0, 4);
        //            if (cardnumber.Length == 16)
        //                EndCardnumber1 = cardnumber.Substring(13, 3);
        //        }

        //        EncCardNumber = StartCardnumber1 + "XXXXXXXXX" + EndCardnumber1;


        //        string paymentStatus = bobibankingResponse.IsTransactionValid();
        //        amount = Convert.ToDouble(bobibankingResponse.AMT);
        //        if (bobibankingResponse.BID != null) bid = bobibankingResponse.BID;
                
        //        PRN = bobibankingResponse.PRN;                
        //        debtAccountNo = bobibankingResponse.ACNT_NUM;
        //        itc = bobibankingResponse.ITC;


        //        retVal = SaveCardHolderPaymentStatusForBobibanking(null, PRN, accountNumber, amount, PID, bid, debtAccountNo, 2, paymentStatus);
        //        if (retVal != "")
        //            transactionUpdateStatus = true;

        //        //if (paymentStatus.ToLower() != success)
        //        //{
        //        //    LoadPage();
        //        //    //DisplayMessage(Constants.Error4, true);
        //        //    DisplayMessage(paymentStatus, true);
        //        //    return;
        //        //}

        //        if (paymentStatus.ToLower() == success && transactionUpdateStatus == true)
        //        {
        //            //DisplayMessage("For your card " + EncCardNumber + " having account number " + accountNumber.Decrypt() + ", Payment of Rs." + amount + " executed successfully.Your transaction number for further reference is : " + PRN + ".Thank you for payment.", true);
        //            DisplayMessage("The payment of Rs." + amount + " for card number " + EncCardNumber + "has been processed successfully. The transaction reference number is " + PRN + ".Thank you for payment.", true);
        //            divDisplayAll.Visible = false;
        //            lkbRedirectToCardStatement.Visible = true;
        //            btnPrint.Visible = true;
        //            btnPrintBillDesk.Visible = false;
        //            string dt = DateTime.Now.ToString();
        //            string amt = Convert.ToString(amount);
        //            string mode = "Bank of Baroda Net Banking";
        //            string[] objParams = { PRN, dt, EncCardNumber, CardHolderName, amt, mode };

        //            string fn = string.Format(queryString, objParams);
        //            string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
        //            btnPrint.Attributes.Add("OnClick", "return DisplaySlip('" + urlQueryString + "');");

        //        }
        //        else if (paymentStatus.ToLower() == success && transactionUpdateStatus == false)
        //        {
        //            DisplayMessage(Constants.Error5 + " Your transaction number for further reference is: " + PRN, true);
        //            divDisplayAll.Visible = false;
        //            lkbRedirectToCardStatement.Visible = true;
        //            btnPrint.Visible = true;
        //            btnPrintBillDesk.Visible = false;
        //            string dt = DateTime.Now.ToString();
        //            string amt = Convert.ToString(amount);
        //            string mode = "Bank of Baroda Net Banking";
        //            string[] objParams = { PRN, dt, EncCardNumber, CardHolderName, amt, mode };

        //            string fn = string.Format(queryString, objParams);
        //            string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
        //            btnPrint.Attributes.Add("OnClick", "return DisplaySlip('" + urlQueryString + "');");
        //        }
        //        else
        //        {
        //            LoadPage();
        //            DisplayMessage(paymentStatus, true);
        //            lkbRedirectToCardStatement.Visible = false;
        //            return;
        //        }

        //        //if (paymentStatus.ToLower() == success && retVal != "")
        //        //{
        //        //    System.Threading.Thread.Sleep(10000);
        //        //    CreateRequest(amount, "B");
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        LoadPage();
        //        DisplayMessage(Constants.GeneralErrorMessage, true);
        //        lkbRedirectToCardStatement.Visible = false;
        //        return;
        //    }
        //}


        private void ProcessResponseFromBobibankingNew (string msgResponse)
        {


            BOBSymmetricCipherHelper sch = new BOBSymmetricCipherHelper();
            string incom_data = sch.getURLDecoded(msgResponse);
            string key = "29304E875832789229304E8758327892";

            try
            {
                string StartCardnumber1 = "";
                string EndCardnumber1 = "";
                string EncCardNumber = "";
                double amount = 0;
                string PRN = string.Empty;
                string bid = string.Empty;
                string PID = string.Empty;
                string debtAccountNo = string.Empty;
                string ITC = string.Empty;
                string retVal = string.Empty;
                bool transactionUpdateStatus = false;
                string BRN = string.Empty;
                string accountNumber = CreditAccNumber; //changes by Sahil on 22'Dec14
               
                string cardnumber = CreditCardNumber.Decrypt(); // Added by Sahil on 22'Dec14
                
                if (cardnumber != "")
                {
                    // cardnumber = cardnumber.Decrypt();
                    StartCardnumber1 = cardnumber.Substring(0, 4);
                    if (cardnumber.Length == 16)
                        EndCardnumber1 = cardnumber.Substring(13, 3);
                }

                EncCardNumber = StartCardnumber1 + "XXXXXXXXX" + EndCardnumber1;
                

                byte[] inputStrBytes = sch.decode(incom_data);
                String outp = Encoding.UTF8.GetString(Decrypt(inputStrBytes, GetRijndaelManaged(key)));               
                var bobibankingResponseNew = new BobibankingResponseNew(outp);

                string[] data = outp.Split('&');
                string strCheckSum = data[data.Length - 1].Split('=')[1];

                string hashgendata = data[0] + "&" + data[1] + "&" + data[2] + "&" + data[3] + "&" + data[4] + "&" + data[5] + "&" + data[6];
                
                string paymentStatus = bobibankingResponseNew.IsTransactionValidNew();

                

                amount = Convert.ToDouble(bobibankingResponseNew.AMT);
                PRN = data[2].Split('=')[1];
                debtAccountNo = data[6].Split('=')[1];
                ITC = data[3].Split('=')[1];
                PID = Constants.PID;
                BRN = data[1].Split('=')[1];

                /// this is for Payment verification
                if (paymentStatus == "Success")
                {
                    paymentStatus = VerifyTranscation(bobibankingResponseNew.AMT, PRN, ITC, PID, BRN);                   
                }             
               
                retVal = SaveCardHolderPaymentStatusForBobibanking(null, PRN, accountNumber, amount, PID, bid, debtAccountNo, 2, paymentStatus);
                if (retVal != "")
                    transactionUpdateStatus = true;

                if (paymentStatus.ToString() == "Success" && transactionUpdateStatus == true)
                {                    
                    DisplayMessage("The payment of Rs." + amount + " for card number " + EncCardNumber + "has been processed successfully. The transaction reference number is " + PRN + ".Thank you for payment.", true);
                    divDisplayAll.Visible = false;
                    lkbRedirectToCardStatement.Visible = true;
                    btnPrint.Visible = true;
                    btnPrintBillDesk.Visible = false;
                    string dt = DateTime.Now.ToString();
                    string amt = Convert.ToString(amount);
                    string mode = "Bank of Baroda Net Banking";
                    string[] objParams = { PRN, dt, EncCardNumber, CardHolderName, amt, mode };

                    string fn = string.Format(queryString, objParams);
                    string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
                    btnPrint.Attributes.Add("OnClick", "return DisplaySlip('" + urlQueryString + "');");

                }
                else if (paymentStatus.ToLower() == "Success" && transactionUpdateStatus == false)
               
                    {
                    DisplayMessage(Constants.Error5 + " Your transaction number for further reference is: " + PRN, true);
                    divDisplayAll.Visible = false;
                    lkbRedirectToCardStatement.Visible = true;
                    btnPrint.Visible = true;
                    btnPrintBillDesk.Visible = false;
                    string dt = DateTime.Now.ToString();
                    string amt = Convert.ToString(amount);
                    string mode = "Bank of Baroda Net Banking";
                    string[] objParams = { PRN, dt, EncCardNumber, CardHolderName, amt, mode };

                    string fn = string.Format(queryString, objParams);
                    string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
                    btnPrint.Attributes.Add("OnClick", "return DisplaySlip('" + urlQueryString + "');");
                }
                else
                {
                    LoadPage();
                    if (paymentStatus == "InValidEmptyString")
                    {
                        paymentStatus = "InValid: Bobibanking Response Has Empty String. Please try again or contact Bank."; 
                    }
                    else if(paymentStatus == "Invalidchecksum")
                    {
                        paymentStatus = "Invalid: " + "Payment not successful. Please try again or contact Bank.";
                    }
                    else if (paymentStatus == "Cancelled")
                    {
                        paymentStatus = "Bobibanking transaction is cancelled due to some reason.Please try again or contact Bank.";
                    }
                    DisplayMessage(paymentStatus, true);
                    lkbRedirectToCardStatement.Visible = false;
                    return;
                }

            }



            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Saves the card holder payment status for bobibanking.
        /// </summary>
        /// <param name="chpsdto">The chpsdto.</param>
        /// <param name="prn">The PRN.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="amountDue">The amount due.</param>
        /// <param name="bid">The bid.</param>
        /// <param name="debtAccountNo">The debt account no.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string SaveCardHolderPaymentStatusForBobibanking(CH_Bobibanking_Payment_Status_DtlDTO chpsdto, string prn, string accountNumber, double amountDue, string PID, string bid, string debtAccountNo, int operation, string status)
        {
            string TransactionNumber = "";
            try
            {
                CardHolderPaymentManager chps = new CardHolderPaymentManager();
                if (operation == 1)
                {
                    if (chpsdto != null)
                        TransactionNumber = chps.SaveCardHolderPaymentStatusBobibanking(chpsdto);
                }
                else if (operation == 2)
                    TransactionNumber = chps.UpdateCardHolderPaymentStatusBobibanking(prn, accountNumber, amountDue, PID, bid, debtAccountNo, status);
                else
                    TransactionNumber = "";
            }
            catch
            {
                DisplayMessage(Constants.PaymentError, true);
            }
            return TransactionNumber;
        }

        #endregion

        #region billdesk

        /// <summary>
        /// Sends the request to bill desk.
        /// </summary>
        /// <remarks></remarks>
        private void SendRequestToBillDesk()
        {
            try
            {
                string AdditionalInfocrnum = CardHolderManager.GetLoggedInUser().creditcard_acc_number;
                string AdditionalInfo1 = CardHolderManager.GetLoggedInUser().CH_Card.BILLDESK_ONLINE_ID;  //As per client saying, We add this info instead of Acc_num (27-Jan-2014)
                string AdditionalInfo2 = CardHolderName;

                DisplayMessage("", false);
                if (Convert.ToDouble(txtEnterAmount.Text.Trim()) == 0)
                {
                    DisplayMessage(Constants.Error1, true);
                    return;
                }
                //double amountDue = Convert.ToInt64(Session[sessionAmountDue]);
                double amountDue = Convert.ToDouble(Session[sessionAmountDue]);
                //double TotalUnbilledAmount = Convert.ToInt64(Session[totalUnbilledAmount]);
                double TotalUnbilledAmount = Convert.ToDouble(Session[totalUnbilledAmount]);
                //------------------------------REfer Email RE: Issues / Bugs Identified 03/20/2015 12:13 PM------------------------------//
                //if (amountDue > 0)
                //{
                //    if (Convert.ToDouble(txtEnterAmount.Text.Trim()) > amountDue)
                //    {
                //        DisplayMessage(Constants.Error3, true);
                //        return;
                //    }
                //}
                //else if (TotalUnbilledAmount > 0)
                //{
                //    if (Convert.ToDouble(txtEnterAmount.Text.Trim()) > TotalUnbilledAmount)
                //    {
                //        DisplayMessage(Constants.Error3, true);
                //        return;
                //    }
                //}
                //------------------------------REfer Email RE: Issues / Bugs Identified 03/20/2015 12:13 PM------------------------------//

                CH_Payment_Status_DtlDTO chpsdto = new CH_Payment_Status_DtlDTO();
                chpsdto.PaymentStatus_Id = 0;
                chpsdto.BillDeskOnlineID = AdditionalInfo1;
                chpsdto.Creditcard_acc_number = AdditionalInfocrnum;
                chpsdto.Credit_card_number = CreditCardNumber;
                chpsdto.Transaction_number = CustomerID.Value;
                chpsdto.AmountDue = Convert.ToDouble(txtEnterAmount.Text.Trim());
                chpsdto.PaymentStatus = unsuccessful;
                chpsdto.Created_dt = DateTime.Now;
                chpsdto.Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                chpsdto.IP_Address = Request.UserHostAddress;
                string transactionNumber = SaveCardHolderPaymentStatus(chpsdto, "", "", 0, "", 1, "", "", "", "");
                if (transactionNumber != "")
                {
                    //if (Convert.ToInt32(txtEnterAmount.Text.Trim()) > 0 && rblPaymentOptions.SelectedValue == "2")
                    if (Convert.ToDouble(txtEnterAmount.Text.Trim()) > 0 && rblPaymentOptions.SelectedValue == "2")
                        Response.Redirect("~/payment_process.aspx?TxnAmount=" + txtEnterAmount.Text.Trim().Encrypt() + "&CustomerID=" + CustomerID.Value.Encrypt() + "&AdditionalInfo1=" + AdditionalInfo1.Encrypt() + "&AdditionalInfo2=" + AdditionalInfo2.Encrypt());
                }
                else
                {
                    DisplayMessage(Constants.PaymentError, true);
                    lkbRedirectToCardStatement.Visible = false;
                    divDisplayAll.Visible = true;
                }
            }
            catch
            {
                DisplayMessage(Constants.GeneralErrorMessage, true);
                divDisplayAll.Visible = true;
                lkbRedirectToCardStatement.Visible = false;
                return;
            }
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="msgResponse">The MSG response.</param>
        /// <remarks></remarks>
        private void ProcessResponseFromBillDesk(string msgResponse)
        {
            string msg = msgResponse;
            BillDeskResponse BillDeskResponse = new BillDeskResponse(msg);
            try
            {
                string StartCardnumber1 = "";
                string EndCardnumber1 = "";
                string EncCardNumber = "";
                string tranactionNumber = "";
                double transactionAmount = 0;
                // string accountNumber = "";
                string BillDeskOnlineID = "";
                string TxnRefrenceNo = "";
                string retVal = "";
                bool transactionUpdateStatus = false;
                string cardnumber = CreditCardNumber.Decrypt(); // Added by Sahil on 22'Dec14
                if (cardnumber != "")
                {
                    // cardnumber = cardnumber.Decrypt();
                    StartCardnumber1 = cardnumber.Substring(0, 4);
                    if (cardnumber.Length == 16)
                        EndCardnumber1 = cardnumber.Substring(13, 3);
                }

                EncCardNumber = StartCardnumber1 + "XXXXXXXXX" + EndCardnumber1;



                string paymentStatus = BillDeskResponse.IsTransactionValid();
                tranactionNumber = BillDeskResponse.CustomerID;
                transactionAmount = Convert.ToDouble(BillDeskResponse.TxtAmount);
                //accountNumber = BillDeskResponse.AdditionalInfo1; // Updated on 20-Jan-2015
                BillDeskOnlineID = BillDeskResponse.AdditionalInfo1;
                TxnRefrenceNo = BillDeskResponse.TxnRefrenceNo;
                string BankRefNo = BillDeskResponse.BankReferenceNo;
                string BankId = BillDeskResponse.BankID;
                string AuthStatus = BillDeskResponse.AuthStatus;

                retVal = SaveCardHolderPaymentStatus(null, tranactionNumber, BillDeskOnlineID, transactionAmount,
                                                    TxnRefrenceNo, 2, paymentStatus, BankRefNo, BankId, AuthStatus); // Update the transaction in sql
                if (retVal != "")
                    transactionUpdateStatus = true;

                //if (paymentStatus.ToLower() != success)
                //{
                //    LoadPage();
                //    DisplayMessage(Constants.Error4, true);
                //    return;

                //}
                if (paymentStatus.ToLower() == success && transactionUpdateStatus == true)
                {
                    DisplayMessage("The payment of Rs." + transactionAmount + " for card number " + EncCardNumber + "has been processed successfully. The transaction reference number is " + tranactionNumber + ".Thank you for payment.", true);
                    divDisplayAll.Visible = false;
                    lkbRedirectToCardStatement.Visible = true;

                    btnPrint.Visible = false;
                    btnPrintBillDesk.Visible = true;
                    string dt = DateTime.Now.ToString();
                    string amt = Convert.ToString(transactionAmount);
                    string mode = "Other Bank Net Banking";
                    string[] objParams = { tranactionNumber, dt, EncCardNumber, CardHolderName, amt, mode };

                    string fn = string.Format(queryString, objParams);
                    string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
                    btnPrintBillDesk.Attributes.Add("OnClick", "return DisplaySlip('" + urlQueryString + "');");


                }
                else if (paymentStatus.ToLower() == success && transactionUpdateStatus == false)
                {
                    DisplayMessage(Constants.Error5 + " Your transaction number for further reference is : " + tranactionNumber, true);
                    divDisplayAll.Visible = false;
                    lkbRedirectToCardStatement.Visible = true;

                    btnPrint.Visible = false;
                    btnPrintBillDesk.Visible = true;
                    string dt = DateTime.Now.ToString();
                    string amt = Convert.ToString(transactionAmount);
                    string mode = "Other Bank Net Banking";
                    string[] objParams = { tranactionNumber, dt, EncCardNumber, CardHolderName, amt, mode };

                    string fn = string.Format(queryString, objParams);
                    string urlQueryString = EncryptDecryptQueryString.Encrypt(fn, qsk);
                    btnPrintBillDesk.Attributes.Add("OnClick", "return DisplaySlip('" + urlQueryString + "');");
                }
                else
                {
                    LoadPage();
                    DisplayMessage(paymentStatus, true);
                    lkbRedirectToCardStatement.Visible = false;
                    return;
                }
            }
            catch (Exception)
            {
                LoadPage();
                DisplayMessage(Constants.GeneralErrorMessage, true);
                //divDisplayAll.Visible = true;
                lkbRedirectToCardStatement.Visible = false;
                return;
            }
        }
        /// <summary>
        /// Saves the card holder payment status.
        /// </summary>
        /// <param name="chpsdto">The chpsdto.</param>
        /// <param name="transactionNumber">The transaction number.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="transactionAmount">The transaction amount.</param>
        /// <param name="TxnRefrenceNo">The TXN refrence no.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string SaveCardHolderPaymentStatus(CH_Payment_Status_DtlDTO chpsdto, string transactionNumber, string BillDeskOnlineID,
                                                  double transactionAmount, string TxnRefrenceNo, int operation, string paymentStatus,
                                                  string BankRefNo, string BankId, string AuthStatus) // Add BillDeskOnlineID instead of Accnumber parameter on 20-01-2015 as per client req.
        {
            string TransactionNumber = "";
            try
            {
                CardHolderPaymentManager chps = new CardHolderPaymentManager();
                if (operation == 1)
                {
                    if (chpsdto != null)
                        TransactionNumber = chps.SaveCardHolderPaymentStatus(chpsdto);
                }
                else if (operation == 2)
                    TransactionNumber = chps.UpdateCardHolderPaymentStatus(transactionNumber, BillDeskOnlineID, transactionAmount,
                                                                          TxnRefrenceNo, paymentStatus, BankRefNo, BankId, AuthStatus);
                else
                    TransactionNumber = "";
            }
            catch
            {
                DisplayMessage(Constants.PaymentError, true);
            }
            return TransactionNumber;
        }
        #endregion

        #endregion

        protected void lkbRedirectToCardStatement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Card/CardStatement.aspx");
        }

        private byte[] Decrypt(byte[] encData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(encData, 0, encData.Length);
        }

        private RijndaelManaged GetRijndaelManaged(String key)
        {
            byte[] keybytes = new byte[32];
            byte[] secretbytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(secretbytes, keybytes, Math.Min(keybytes.Length, secretbytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                // BlockSize = 128,
                Key = keybytes,
                // IV = keybytes
            };
        }

        private string VerifyTranscation(string amount, string PRN, string ITC, string PID, string BRN)
        {
            string urlpath = "Action.SHPMALL.Verify.Init=Y&type=1&AppSignonBankId=012&AppType=corporate&QS=TXN_AMT=";
            string URL = ConfigurationManager.AppSettings["BOBNetBankingVericationCallURL"] + urlpath  + amount + "|PRN=" + PRN + "|ITC=" + ITC + "|PID=" + PID + "|BRN="+ BRN;

            string verifiedPaymentStatus = null;
            verifiedPaymentStatus = getrequest(URL);

            if (verifiedPaymentStatus  == "SUC")
            {
                return "Success";
            }
            else if (verifiedPaymentStatus == "CAN")
            {
                return "Cancelled";
            }
            else if (verifiedPaymentStatus == "FAL")
            {
                return "Failure";
            }
            else if (verifiedPaymentStatus == "PEN")
            {
                return "Pending";
            }
            else if (verifiedPaymentStatus == "PRO")
            {
                return "Processing";
            }
            else if (verifiedPaymentStatus == "REC")
            {
                return "Recalled";
            }
            else if (verifiedPaymentStatus == "REJ")
            {
                return "Rejected";
            }
            else if (verifiedPaymentStatus == "SUS")
            {
                return "Suspect";
            }
            else
            {
                return verifiedPaymentStatus;
            }

        }

        public string getrequest(string url)
        {            
            ServicePointManager.ServerCertificateValidationCallback = UrlHelper.TrustAllCertificateCallback;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "get";
            string responseContent = null;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                responseContent = reader.ReadToEnd();
            }            

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(responseContent);
            XmlNodeList xNodeList = xml.SelectNodes("/Report/ShoppingMallVerify");
            foreach (XmlNode xNode in xNodeList)
            {
                responseContent = xNode["STATUS"].InnerText;
                
            }
           
            return responseContent;


        }

       

    }
}


///// <summary>
///// Sends the request.
///// </summary>
//private void SendRequestToBillDesk(string strCustomerId, string TxnAmount, string AdditionalInfo1)
//{
//    try
//    {
//        DisplayMessage("", false);
//        CH_Payment_Status_DtlDTO chpsdto = new CH_Payment_Status_DtlDTO();
//        chpsdto.PaymentStatus_Id = 0;
//        chpsdto.Creditcard_acc_number = AdditionalInfo1;
//        chpsdto.Credit_card_number = CardHolderManager.GetLoggedInUser().credit_card_number;
//        chpsdto.Transaction_number = strCustomerId;
//        chpsdto.AmountDue = Convert.ToDouble(TxnAmount);
//        chpsdto.PaymentStatus = unsuccessful;
//        chpsdto.Created_dt = DateTime.Now;
//        chpsdto.Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id;
//        chpsdto.IP_Address = Request.UserHostAddress;
//        string transactionNumber = SaveCardHolderPaymentStatus(chpsdto, "", "", 0, "", 1);
//        if (transactionNumber != "")
//        {
//            BillDeskRequest BillDesk = new CardHolder.Utility.Payment.BillDeskRequest();
//            BillDesk.CustomerID = strCustomerId;
//            BillDesk.TxnAmount = TxnAmount;
//            BillDesk.AdditionalInfo1 = AdditionalInfo1;
//            BillDesk.RU = Request.UrlReferrer.OriginalString;
//            if (Convert.ToInt16(Request.QueryString["t"]) == 1) //Remove this block once testing is done.
//            {
//                string[] a = BillDesk.RU.Split('?');
//                if (a.Length > 0)
//                    BillDesk.RU = a[0];
//            }
//            msg.Value = BillDesk.GetPaymentRequest();
//            
//            if (msg.Value != "")
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "PostBackPageForPayment", "PostBackPageForPayment();", true); //Call javascript to postback to suggested URL                        
//            //Response.Redirect(BillDesk.PaymeGetwayURL);
//            else
//                DisplayMessage(generalError, true);
//        }
//        else
//        {
//            DisplayMessage(generalError, true);
//        }
//    }
//    catch (Exception ex)
//    {
//        DisplayMessage(generalError, true);
//    }
//}