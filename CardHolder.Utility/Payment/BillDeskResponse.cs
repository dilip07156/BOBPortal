using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using NLog;

namespace CardHolder.Utility.Payment
{
    public class BillDeskResponse
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string MerchantID { get; set; }
        public string CustomerID { get; set; }
        public string TxnRefrenceNo { get; set; }
        public string BankReferenceNo { get; set; }
        public string TxtAmount { get; set; }
        public string BankID { get; set; }
        public string BankMerchantID { get; set; }
        public string TxnType { get; set; }
        public string CurrencyName { get; set; }
        public string ItemCode { get; set; }
        public string SecurityType { get; set; }
        public string SecurityID { get; set; }
        public string SecurityPassword { get; set; }
        public string TxnDate { get; set; }
        public string AuthStatus { get; set; }
        public string SettlementType { get; set; }
        public string AdditionalInfo1 { get; set; }
        public string AdditionalInfo2 { get; set; }
        public string AdditionalInfo3 { get; set; }
        public string AdditionalInfo4 { get; set; }
        public string AdditionalInfo5 { get; set; }
        public string AdditionalInfo6 { get; set; }
        public string AdditionalInfo7 { get; set; }
        public string ErrorStatus { get; set; }
        public string ErrorDescription { get; set; }
        public string CheckSum { get; set; }

        string[] data = null;
        public BillDeskResponse(string response)
        {
            try
            {
                logger.Info("Response Parameter String:" + response);
                data = response.Split('|');
                if (data.Count() > 1)
                {
                    MerchantID = data[0].Trim();
                    CustomerID = data[1].Trim();
                    TxnRefrenceNo = data[2].Trim();
                    BankReferenceNo = data[3].Trim();
                    TxtAmount = data[4].Trim();
                    BankID = data[5].Trim();
                    BankMerchantID = data[6].Trim();
                    TxnType = data[7].Trim();
                    CurrencyName = data[8].Trim();
                    ItemCode = data[9].Trim();
                    SecurityType = data[10].Trim();
                    SecurityID = data[11].Trim();
                    SecurityPassword = data[12].Trim();
                    TxnDate = data[13].Trim();
                    AuthStatus = data[14].Trim();
                    SettlementType = data[15].Trim();
                    AdditionalInfo1 = data[16].Trim();
                    AdditionalInfo2 = data[17].Trim();
                    AdditionalInfo3 = data[18].Trim();
                    AdditionalInfo4 = data[19].Trim();
                    AdditionalInfo5 = data[20].Trim();
                    AdditionalInfo6 = data[21].Trim();
                    AdditionalInfo7 = data[22].Trim();
                    ErrorStatus = data[23].Trim();
                    ErrorDescription = data[24].Trim();
                    CheckSum = data[25].Trim();
                }
            }
            catch (Exception exp)
            {
                logger.Info("Exception: " + exp.Message);
            }

        }

        private bool VarifyCheckSum()
        {
            try
            {
                return CalculateCheckSum.VarifyCheckSum(data, CheckSum);
            }
            catch (Exception exp)
            {
                logger.Info("Exception: " + exp.Message);
            }
            return false;
        }

        public string IsTransactionValid()
        {
            try
            {
                /// Response Basic Varification
                if (data.Count() == 0 && !string.IsNullOrEmpty(CheckSum))
                {
                    logger.Info("InValid: " + "Bill Desk Response Has Empty String");
                    return "InValid: Bill Desk Response Has Empty String";
                }
                //if (ErrorStatus != "NA")
                //{
                //    logger.Info("InValid: " + ErrorStatus);
                //    return "Invalid: " + ErrorStatus;
                //}
                //if (ErrorDescription != "NA" && ErrorDescription.Trim().ToLower() != "success")
                //{
                //    logger.Info("InValid: " + ErrorDescription);
                //    return "Invalid: " + ErrorDescription;
                //}

                /// Payment Varification
                if (VarifyCheckSum() == false)
                {
                    logger.Info("InValid: " + "check-sum is not verified");
                    return "Invalid: " + "check-sum is not verified";
                }

                /// AuthStatus Check
                /// 
                if (AuthStatus == "0300")
                {
                    logger.Info("Success");
                    return "Success";
                }
                else if (AuthStatus == "0399")
                {
                    logger.Info("Invalid Authentication At Bank");
                    return "Invalid: " + AuthStatus + " Invalid Authentication at Bank, Transaction is Cancelled";
                }
                else if (AuthStatus == "0002")
                {
                    logger.Info("BillDesk is waiting for Response from Bank");
                    //return "Invalid: " + AuthStatus + " BillDesk is waiting for Response from Bank";
                    return "Invalid: " + AuthStatus + " Response not received from Bank, Transaction is Cancelled";
                }
                else if (AuthStatus == "0001")
                {
                    logger.Info("Error at BillDesk");
                    // return "Invalid: " + AuthStatus + " Error at BillDesk";
                    return "Invalid: " + AuthStatus + " Error in connection, Transaction is Cancelled";
                }
                else if (AuthStatus == "NA")
                {
                    logger.Info(AuthStatus + " 'NA' Is Invalid As AuthStatus");
                    //return "Invalid: " + "'NA' Is Invalid As AuthStatus";
                    return "Invalid: " + "Invalid Input in the Request Message, Transaction is Cancelled ";
                }
                else
                {
                    logger.Info(AuthStatus + " Undefined AuthStatus");
                    //return "Invalid: " + "'NA' Is Invalid As AuthStatus";
                    return "Undefined Transaction with status : " + AuthStatus + " , Transaction is Cancelled ";
                }
            }
            catch (Exception exp)
            {
                logger.Info("Exception: " + exp.Message);
                return "Exception: " + exp.Message;
            }
            //logger.Info("Fail As Auth Status Is Not Found");
            //return "Invalid: Fail As Auth Status Is Not Found";
        }
    }
}
