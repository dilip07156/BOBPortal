using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using CardHolder.Utility.Payment;
using com.bob.utils.security;

namespace CardHolder.Utility.bobibanking
{
    public class BobibankingResponseNew
    {
        //Format-Pattern of response : encdata=AMT=1.00|PRN=TATA|STATUS=S|BID=0000000000|DebtAccountNo=00000000000000|ITC=12345|CheckSum=12343434343
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string AMT { get; set; }
        public string PRN { get; set; }
        public string STATUS { get; set; }
        public string BID { get; set; }
        public string DebtAccountNo { get; set; }
        public string ITC { get; set; }
        public string ACNT_NUM { get; set; }
        public string CRN { get; set; }
        public string REFNO { get; set; }
        public string ErrorStatus { get; set; }
        public string ErrorDescription { get; set; }
        public string CheckSum { get; set; }

        string[] data = null;
        public BobibankingResponseNew(string response)
        {
            try
            {
                logger.Info("Response Parameter String:" + response);
                data = response.Split('&');
                if (data.Count() > 1)
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        string[] responses = data[i].Split('=');
                        string responseValue = responses[1].Trim();

                        //if (i == 0)
                        //    AMT = responseValue;
                        //else if (i == 1)
                        //    PRN = responseValue;
                        //else if (i == 2)
                        //    STATUS = responseValue;
                        //else if (i == 3)
                        //    BID = responseValue;
                        //else if (i == 4)
                        //    DebtAccountNo = responseValue;
                        //else if (i == 5)
                        //    ITC = responseValue;
                        //else if (i == 6)
                        //    CheckSum = responseValue;
                        //else if (i == 7)
                        //    REFNO = responseValue;


                        if (i == 0)
                            STATUS = responseValue;
                        else if (i == 1)
                            REFNO = responseValue;
                        else if (i == 2)
                            PRN = responseValue;
                        else if (i == 3)
                            ITC = responseValue;
                        else if (i == 4)
                            AMT = responseValue;
                        else if (i == 5)
                            CRN = responseValue;
                        else if (i == 6)
                            ACNT_NUM = responseValue;
                        else if (i == 7)
                            CheckSum = responseValue;

                    }
                }
            }
            catch (Exception exp)
            {
                logger.Info("Exception: " + exp.Message);
            }
        }

        private bool VarifyCheckSumNew()
        {
           
            try
            {
                return BobibankingCalculateCheckSum.VarifyCheckSumNew(data, CheckSum);
               
            }
            catch (Exception exp)
            {
                logger.Info("Exception: " + exp.Message);
            }
            return false;
        }

        public string IsTransactionValidNew()
        {
            try
            {
                /// Response Basic Varification
                if (data.Count() == 0 && !string.IsNullOrEmpty(CheckSum))
                {
                    logger.Info("InValid: " + "Bobibanking Response Has Empty String.");
                    return "InValidEmptyString";
                }
                /// Payment Varification
                if (VarifyCheckSumNew() == false)
                {
                    logger.Info("InValid: " + "check-sum is not verified");
                    return "Invalidchecksum";
                }

                /// STATUS Check
                if (STATUS.ToLower() == "can" || STATUS.ToLower() == "c")
                {
                    logger.Info("Bobibanking transaction is cancelled due to some reason.Please try again or contact Bank.");
                    return "Cancelled";
                }
                else if (STATUS.ToLower() == "suc" || STATUS.ToLower() == "s")
                {
                    logger.Info("Success");
                    return "Success";
                }
                
            }
            catch (Exception exp)
            {
                logger.Info("Exception: " + exp.Message);
                return "Exception: " + exp.Message;
            }
            logger.Info("Fail As Status Is Not Found");
            return "Invalid: Fail as Status is not found. Please try again or contact Bank.";
        }
    }
}
