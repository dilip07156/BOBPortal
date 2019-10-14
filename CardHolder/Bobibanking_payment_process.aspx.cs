using System;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.bobibanking;
using NLog;
using com.bob.utils.security;
using System.Configuration;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class Bobibanking_payment_process : PageBase
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 
        /// </summary>
        string bobibankingFileName = "BOB.KEY";
        /// <summary>
        /// 
        /// </summary>
        string BobibankingKeyPath = "BobibankingKeyPath";
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
            try
            {
                if (IsXsrf) { }
                else
                {
                    if (!IsPostBack)
                    {
                        BobibankingRequest bobibankingRequest = new BobibankingRequest();
                        bobibankingRequest.ShoppingMallTranFGTXN_AMT = Convert.ToString(Request["AMT"]).Replace(" ", "+").Decrypt();
                        bobibankingRequest.ShoppingMallTranFGPRN = Convert.ToString(Request["PRN"]).Replace(" ", "+").Decrypt();
                        bobibankingRequest.ShoppingMallTranFGITC = Convert.ToString(Request["ITC"]).Replace(" ", "+").Decrypt();
                        //string RU = Convert.ToString(Request.UrlReferrer.OriginalString);
                        //string url = Convert.ToString(Request.UrlReferrer.OriginalString);
                        //if (url.IndexOf("?") > 0)
                        //{
                        //    string[] a = url.Split('?');
                        //    RU = a[0];
                        //}
                        bobibankingRequest.RU = UrlHelper.GetAbsoluteUri() + "Card/PaymentProcess.aspx";
                        //bobibankingRequest.RU = "http://123.108.44.156" + "Card /PaymentProcess.aspx";
                        string rootFilePath = GetBobibankingKeyPath().Trim();
                        string bobKeyFilePath = rootFilePath + bobibankingFileName.Trim();
                        bobibankingRequest.bobKeyFilePath = bobKeyFilePath;
                        string requestStringBeforeEncryption = bobibankingRequest.GetBobibankingPaymentRequest();
                        //BOBSymmetricCipherHelper bOBSymmetricCipherHelper = new BOBSymmetricCipherHelper();
                        //string requestEncData = bOBSymmetricCipherHelper.encrypt(requestStringBeforeEncryption, "SHA-512");//BobibankingEncryptionDecryption.Encrypt(requestStringBeforeEncryption,bobKeyFilePath);

                        string requestEncData = ConfigurationManager.AppSettings["BOBBankingNetBankingURL"] + requestStringBeforeEncryption;

                        Response.Redirect(requestEncData, false);
                        //encdata.Value = requestStringBeforeEncryption;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info("Exception: " + ex.Message);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetBobibankingKeyPath()
        {
            string FilePath = string.Empty;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(BobibankingKeyPath);
            if (obj != null && obj.Parameter_ValueC != null)
                FilePath = Convert.ToString(obj.Parameter_ValueC);
            return FilePath.Trim();
        }
        #endregion
    }
}
