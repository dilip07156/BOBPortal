using System;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using CardHolder.Utility.bobibanking;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class TestBobibanking : PageBase
    {
        /// <summary>
        /// 
        /// </summary>
        string bobibankingFileName = "BOB.KEY";
        /// <summary>
        /// 
        /// </summary>
        string BobibankingKeyPath = "BobibankingKeyPath";
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
                {
                    BobibankingRequest bobibankingRequest = new BobibankingRequest();
                    bobibankingRequest.AMT = Convert.ToString(Request["AMT"]);
                    bobibankingRequest.PRN = Convert.ToString(Request["PRN"]);
                    bobibankingRequest.ITC = Convert.ToString(Request["ITC"]);
                    string RU = Convert.ToString(Request.UrlReferrer.OriginalString);
                    bobibankingRequest.RU = RU;
                    string rootFilePath = GetBobibankingKeyPath().Trim();
                    string bobKeyFilePath = rootFilePath + bobibankingFileName.Trim();
                    string requestStringBeforeEncryption = bobibankingRequest.GetBobibankingPaymentRequest();
                    string requestEncData = BobibankingEncryptionDecryption.Encrypt(requestStringBeforeEncryption,
                                                                                    bobKeyFilePath);
                    encdata.Value = requestEncData;
                }
            }
        }
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