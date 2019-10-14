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
    public partial class BobibankingPay : PageBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            PRN.Value = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 1).ToString();
           
            if (Request.Params["encdata"] != null)
            {
                string encdata = Request.Params["encdata"];
                //Response.Write(msg);
                try
                {
                    ProcessResponseFromBobibanking(encdata);
                }
                catch (Exception exp)
                {
                    Exception.Text = Constants.GeneralErrorMessage;
                }

            }
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
        /// <summary>
        /// Processes the response from bobibanking.
        /// </summary>
        /// <param name="msgResponse">The MSG response.</param>
        /// <remarks></remarks>
        private void ProcessResponseFromBobibanking(string msgResponse)
        {
           
            string rootFilePath = GetBobibankingKeyPath();
            string bobKeyFilePath = rootFilePath + "BOB.KEY";
            string msg = BobibankingEncryptionDecryption.Decrypt(msgResponse, bobKeyFilePath);
            BobibankingString.Text = msg;
            BobibankingResponse bobibankingResponse = new BobibankingResponse(msg);

            try
            {
                // string accountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
                string paymentStatus = bobibankingResponse.IsTransactionValid();
                BobibankingPaymentStatus.Text = paymentStatus;
            }
            catch (Exception exp)
            {
                Exception.Text = Constants.GeneralErrorMessage;
            }
        }
    }
}