using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.Utility;

namespace CardHolder.Card
{
    public partial class PrintPaymentSlip : System.Web.UI.Page
    {
        string qsk = "qcv45dnr";

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }


        #region Helper methods

        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPage()
        {
            try
            {
                lblDisplayMessage.Visible = false;
                string strReq = "";
                strReq = Request.Form["txtPostData"];
                if (!strReq.Equals(""))
                    strReq = EncryptDecryptQueryString.Decrypt(strReq, qsk);
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                arrIndMsg = arrMsgs[0].Split('='); //Get the Details
                string[] Details;
                Details = arrIndMsg[1].Split(',');

                if (Details != null)
                {
                    lblTransactionNum.Text = Convert.ToString(Details[0].Trim());
                    lbltxnDateTime.Text = Convert.ToString(Details[1].Trim());
                    lblCardnumber.Text = Convert.ToString(Details[2].Trim());
                    lblName.Text = Convert.ToString(Details[3].Trim());
                    lblamount.Text = Convert.ToString(Details[4].Trim());
                    lblModePayment.Text = Convert.ToString(Details[5].Trim());
                }
            }
            catch
            {
                lblDisplayMessage.Visible = true;
                lblDisplayMessage.InnerText = Constants.GeneralErrorMessage;
            }
        }



        #endregion
    }
}