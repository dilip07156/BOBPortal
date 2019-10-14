using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using NLog;

namespace CardHolder.Utility.Payment
{
    public class BillDeskRequest
    {
        #region Variable

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, string> BasicParameters = new Dictionary<string, string>();

        #endregion

        #region Properties

        private string _PaymeGetwayURL = "https://www.billdesk.com/pgidsk/PGIMerchantPayment";
        public string PaymeGetwayURL { get { return _PaymeGetwayURL; } }

        private string _MerchantID = "BOBCARD";
        public string MerchantID { get { return _MerchantID; } }

        private string _CurrencyType = "INR";
        public string CurrencyType { get { return _CurrencyType; } }

        private string _TypeField1 = "R";
        public string TypeField1 { get { return _TypeField1; } }

        private string _SecurityID = "bobcard";
        public string SecurityID { get { return _SecurityID; } }

        private string _TypeField2 = "F";
        public string TypeField2 { get { return _TypeField2; } }

        private string _TxnAmount = "NA";
        public string TxnAmount { get { return _TxnAmount; } set { _TxnAmount = value; } }

        private string _CustomerID = "NA";
        public string CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }

        private string _AdditionalInfo1 = "NA";
        public string AdditionalInfo1 { get { return _AdditionalInfo1; } set { _AdditionalInfo1 = value; } }

        private string _AdditionalInfo2 = "NA";
        public string AdditionalInfo2 { get { return _AdditionalInfo2; } set { _AdditionalInfo2 = value; } }

        private string _AdditionalInfo3 = "NA";
        public string AdditionalInfo3 { get { return _AdditionalInfo3; } set { _AdditionalInfo3 = value; } }

        private string _AdditionalInfo4 = "NA";
        public string AdditionalInfo4 { get { return _AdditionalInfo4; } set { _AdditionalInfo4 = value; } }

        private string _AdditionalInfo5 = "NA";
        public string AdditionalInfo5 { get { return _AdditionalInfo5; } set { _AdditionalInfo5 = value; } }

        private string _AdditionalInfo6 = "NA";
        public string AdditionalInfo6 { get { return _AdditionalInfo6; } set { _AdditionalInfo6 = value; } }

        private string _AdditionalInfo7 = "NA";
        public string AdditionalInfo7 { get { return _AdditionalInfo7; } set { _AdditionalInfo7 = value; } }

        private string _RU = "NA";
        public string RU { get { return _RU; } set { _RU = value; } }

        private string _CHECKSUM = "NA";
        public string CHECKSUM { get { return _CHECKSUM; } set { _CHECKSUM = value; } }

        #endregion

        ///// <summary>
        ///// Fill Dictionary With Basic Parametes
        ///// </summary>
        //private void FillDictionary()
        //{
        //    BasicParameters.Add("PaymeGetwayURL", PaymeGetwayURL);
        //    BasicParameters.Add("MerchantID", MerchantID);
        //    BasicParameters.Add("CurrencyType", CurrencyType);
        //    BasicParameters.Add("TypeField1", TypeField1);
        //    BasicParameters.Add("SecurityID", SecurityID);
        //    BasicParameters.Add("TypeField2", TypeField2);
        //    BasicParameters.Add("TxnAmount", TxnAmount);
        //    BasicParameters.Add("CustomerID", CustomerID);
        //    BasicParameters.Add("AdditionalInfo1", AdditionalInfo1);
        //    BasicParameters.Add("AdditionalInfo2", AdditionalInfo2);
        //    BasicParameters.Add("AdditionalInfo3", AdditionalInfo3);
        //    BasicParameters.Add("AdditionalInfo4", AdditionalInfo4);
        //    BasicParameters.Add("AdditionalInfo5", AdditionalInfo5);
        //    BasicParameters.Add("AdditionalInfo6", AdditionalInfo6);
        //    BasicParameters.Add("AdditionalInfo7", AdditionalInfo7);
        //    BasicParameters.Add("RU", RU);
        //}

        /// <summary>
        /// Create Payment Request
        /// </summary>
        /// <returns></returns>
        public string GetPaymentRequest()
        {
            if (BasicParameters.Keys.Count == 0)
            {
                BasicParameters.Add("PaymeGetwayURL", PaymeGetwayURL);
                BasicParameters.Add("MerchantID", MerchantID);
                BasicParameters.Add("CurrencyType", CurrencyType);
                BasicParameters.Add("TypeField1", TypeField1);
                BasicParameters.Add("SecurityID", SecurityID);
                BasicParameters.Add("TypeField2", TypeField2);
                BasicParameters.Add("TxnAmount", TxnAmount);
                BasicParameters.Add("CustomerID", CustomerID);
                BasicParameters.Add("AdditionalInfo1", AdditionalInfo1);
                BasicParameters.Add("AdditionalInfo2", AdditionalInfo2);
                BasicParameters.Add("AdditionalInfo3", AdditionalInfo3);
                BasicParameters.Add("AdditionalInfo4", AdditionalInfo4);
                BasicParameters.Add("AdditionalInfo5", AdditionalInfo5);
                BasicParameters.Add("AdditionalInfo6", AdditionalInfo6);
                BasicParameters.Add("AdditionalInfo7", AdditionalInfo7);
                BasicParameters.Add("RU", RU);
            }
            ///STEP-1 Create Request String
            string request = "MerchantID|CustomerID|NA|TxnAmount|NA|NA|NA|CurrencyType|NA|TypeField1|SecurityID|NA|NA|TypeField2|AdditionalInfo1|AdditionalInfo2|AdditionalInfo3|AdditionalInfo4|AdditionalInfo5|AdditionalInfo6|AdditionalInfo7|RU";
            string[] keys = request.Split('|');
            foreach (var item in keys)
            {
                if (item.Trim() != "NA")
                {
                    request = request.Replace(item, BasicParameters[item]);
                }
            }

            logger.Info("Request Parameter String: " + request);

            ///STEP-2 Calculate Checksum Using Key And Append
            request = request + "|" + CalculateCheckSum.GetCheckSum(request, "Reqest");
            logger.Info("Request With CheckSum: " + request);

            return request;
        }

        ///// <summary>
        ///// POST Payment Request
        ///// </summary>
        ///// <param name="postParameters"></param>
        ///// <returns></returns>
        //public string PayNow(Dictionary<string, string> postParameters)
        //{
        //    ///STEP-0 Return From Here If Payment Setting Is Not Active
        //    if (System.Configuration.ConfigurationManager.AppSettings["PaymentActive"] == null)
        //    {
        //        return "Payment is not active";
        //    }
        //    else
        //    {
        //        if (System.Configuration.ConfigurationManager.AppSettings["PaymentActive"].ToString() == "0")
        //        {
        //            return "Payment is not active";
        //        }
        //    }

        //    string error = "";
        //    FillDictionary();
        //    string postData = GetPaymentRequest();
        //    HttpWebResponse myHttpWebResponse = null;
        //    Stream responseStream = null;
        //    StreamReader myStreamReader = null;
        //    string pageContent = "";
        //    try
        //    {
        //        ///STEP-1 Build Request
        //        HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(PaymeGetwayURL);
        //        myHttpWebRequest.Method = "POST";
        //        myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
        //        //foreach (string key in BasicParameters.Keys)
        //        //{
        //        //    postData += HttpUtility.UrlEncode(key) + "="
        //        //          + HttpUtility.UrlEncode(BasicParameters[key]) + "&";
        //        //}
        //        //foreach (string key in postParameters.Keys)
        //        //{
        //        //    postData += HttpUtility.UrlEncode(key) + "="
        //        //          + HttpUtility.UrlEncode(postParameters[key]) + "&";
        //        //}
        //        //byte[] data = Encoding.ASCII.GetBytes(postData);

        //        byte[] data = Encoding.ASCII.GetBytes(postData);
        //        myHttpWebRequest.ContentLength = data.Length;


        //        ///STEP-2 Send Data On Build Request 
        //        Stream requestStream = null;
        //        try
        //        {
        //            requestStream = myHttpWebRequest.GetRequestStream();
        //            requestStream.Write(data, 0, data.Length);
        //        }
        //        catch (Exception exp)
        //        {
        //            error = exp.Message;
        //        }
        //        finally
        //        {
        //            requestStream.Close();
        //        }


        //        ///STEP-3 Retrieve Response
        //        try
        //        {
        //            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        //            responseStream = myHttpWebResponse.GetResponseStream();
        //            myStreamReader = new StreamReader(responseStream, Encoding.Default);
        //            pageContent = myStreamReader.ReadToEnd();
        //        }
        //        catch (Exception exp)
        //        {
        //            error += exp.Message;
        //        }
        //        finally
        //        {
        //            myStreamReader.Close();
        //            responseStream.Close();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        error += exp.Message;
        //    }
        //    finally
        //    {
        //        myHttpWebResponse.Close();
        //    }

        //    if (error == "")
        //    {
        //        try
        //        {
        //            BillDeskResponse BillDeskResponse = new BillDeskResponse(pageContent);
        //            BillDeskResponse.IsTransactionValid();
        //        }
        //        catch (Exception exp)
        //        {
        //            logger.Info("Exception: " + exp.Message);
        //            return exp.Message;
        //        }
        //    }
        //    else
        //    {
        //        logger.Info("Exception: " + error);
        //    }


        //    return pageContent;
        //}
    }
}
