using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using CardHolder.Utility.Payment;

namespace CardHolder.Utility.bobibanking
{
    public class BobibankingResponse
    {
        //Format-Pattern of response : encdata=AMT=1.00|PRN=TATA|STATUS=S|BID=0000000000|DebtAccountNo=00000000000000|ITC=12345|CheckSum=12343434343
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string AMT { get; set; }
        public string PRN { get; set; }
        public string STATUS { get; set; }
        public string BID { get; set; }
        public string DebtAccountNo { get; set; }
        public string ITC { get; set; }
        public string ErrorStatus { get; set; }
        public string ErrorDescription { get; set; }
        public string CheckSum { get; set; }

        string[] data = null;
        public BobibankingResponse(string response)
        {
            try
            {
                logger.Info("Response Parameter String:" + response);
                data = response.Split('|');
                if (data.Count() > 1)
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        string[] responses = data[i].Split('=');
                        string responseValue = responses[1].Trim();
                        if (i == 0)
                            AMT = responseValue;
                        else if (i == 1)
                            PRN = responseValue;
                        else if (i == 2)
                            STATUS = responseValue;
                        else if (i == 3)
                            BID = responseValue;
                        else if (i == 4)
                            DebtAccountNo = responseValue;
                        else if (i == 5)
                            ITC = responseValue;
                        else if (i == 6)
                            CheckSum = responseValue;
                    }
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
                return BobibankingCalculateCheckSum.VarifyCheckSum(data, CheckSum);
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
                    logger.Info("InValid: " + "Bobibanking Response Has Empty String.");
                    return "InValid: Bobibanking Response Has Empty String. Please try again or contact Bank.";
                }
                /// Payment Varification
                if (VarifyCheckSum() == false)
                {
                    logger.Info("InValid: " + "check-sum is not verified");
                    //return "Invalid: " + "check-sum is not verified. Please try again or contact Bank.";
                    return "Invalid: " + "Payment not successful. Please try again or contact Bank.";
                }

                /// STATUS Check
                if (STATUS.ToLower() == "cancel" || STATUS.ToLower() == "c")
                {
                    logger.Info("Bobibanking transaction is cancelled due to some reason.Please try again or contact Bank.");
                    return "Bobibanking transaction is cancelled due to some reason.Please try again or contact Bank.";
                }
                else if (STATUS.ToLower() == "success" || STATUS.ToLower() == "s")
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
