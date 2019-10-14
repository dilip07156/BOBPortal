using System;
using CardHolder.Utility;
using CardHolder.Utility.Payment;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class Payee : PageBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            CustomerID.Value = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 1).ToString();
            if (Request.Params["msg"] != null)
            {
                string msg = Request.Params["msg"];
                BillDeskResponseString.Text = msg;

                BillDeskResponse BillDeskResponse = new BillDeskResponse(msg);
                try
                {
                    string status = BillDeskResponse.IsTransactionValid();
                    BillDeskPaymentStatus.Text = status;
                }
                catch (Exception)
                {
                    Exception.Text = Constants.GeneralErrorMessage;
                }
            }

        }
    }
}