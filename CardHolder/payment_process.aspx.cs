using System;
using CardHolder.Utility;
using CardHolder.Utility.Payment;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class payment_process : PageBase
    {
      //  private static Logger logger = LogManager.GetCurrentClassLogger();
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
                    var billDesk = new BillDeskRequest();
                    //BillDesk.CustomerID = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 1).ToString();
                    billDesk.TxnAmount = Request["TxnAmount"].Replace(" ", "+").Decrypt(); //"2.00";
                    billDesk.CustomerID = Request["CustomerID"].Replace(" ", "+").Decrypt();
                    billDesk.AdditionalInfo1 = Request["AdditionalInfo1"].Replace(" ", "+").Decrypt();
                    billDesk.AdditionalInfo2 = Request["AdditionalInfo2"].Replace(" ", "+").Decrypt();
                    if (Request.UrlReferrer != null)
                        billDesk.RU = Request.UrlReferrer.OriginalString;
                    msg.Value = billDesk.GetPaymentRequest();
                }
            }
        }
    }
}