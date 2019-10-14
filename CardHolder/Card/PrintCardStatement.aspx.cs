using System;
using System.IO;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder.Card
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class PrintCardStatement : PageBase
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        string pdfContentType = "Application/pdf";
        /// <summary>
        /// 
        /// </summary>
        string contentDisposition = "Content-Disposition";
        /// <summary>
        /// 
        /// </summary>
        string qsk = "awa4befr";
        /// <summary>
        /// 
        /// </summary>
        string inlineAttachment = "inline; filename=";
        /// <summary>
        /// 
        /// </summary>
        string contentLength = "content-length";
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
            LoadPage();

        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Loads the page.
        /// </summary>
        /// <remarks></remarks>
        private void LoadPage()
        {
            //string strReq = "";
            //strReq = Request.RawUrl;
            //strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            //if (!strReq.Equals(""))
            //    strReq = EncryptDecryptQueryString.Decrypt(strReq, qsk);

            //string directory = GetFilePath();
            //string[] arrMsgs = strReq.Split('&');
            //string[] arrIndMsg;
            //arrIndMsg = arrMsgs[0].Split('='); //Get the Name
            //string filename = arrIndMsg[1].ToString().Trim();
            //string FilePath = directory + filename;
            //byte[] fileBytes = File.ReadAllBytes(FilePath);
            //MemoryStream stream = new MemoryStream(fileBytes);
            //Response.ContentType = pdfContentType;
            //Response.AddHeader(contentDisposition, inlineAttachment + FilePath);
            //Response.AddHeader(contentLength, stream.Length.ToString());
            //Response.BinaryWrite(stream.ToArray());
            //Response.End();

            bool IsAccHavePdf = false;
            string strReq = "";
            string AccountNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (AccountNumber != "")
            {
                //strReq = Request.RawUrl;
                strReq = Request.Form["txtPostData"];
                if (!strReq.Equals(""))
                    strReq = EncryptDecryptQueryString.Decrypt(strReq, qsk);

                string directory = GetFilePath();
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                string filename = arrIndMsg[1].ToString().Trim();
                IsAccHavePdf = CardManager.GetPDFnames(AccountNumber, filename);
                if (IsAccHavePdf == true)
                {
                    string FilePath = directory + filename;
                    byte[] fileBytes = File.ReadAllBytes(FilePath);
                    MemoryStream stream = new MemoryStream(fileBytes);
                    Response.ContentType = pdfContentType;
                    Response.AddHeader(contentDisposition, inlineAttachment + FilePath);
                    Response.AddHeader(contentLength, stream.Length.ToString());
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
                else
                {
                    Response.Write(Constants.NoPdfFound);
                }
            }
            //else
            //{
            //    string strjava = "<script language='javascript' type='text/javascript'>";
            //    strjava += "window.parent.location.reload();";
            //    strjava += "</script>";
            //    Response.Write(strjava);
            //}
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetFilePath()
        {
            string pdfFilePath = string.Empty;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(Constants.CardHolderStatementFilePath);
            if (obj != null && obj.Parameter_ValueC != null)
                pdfFilePath = Convert.ToString(obj.Parameter_ValueC);
            return pdfFilePath;
        }
        #endregion
    }
}
#region Comments
//string Filenm = Convert.ToString(Session["PrintFileName"]);
//string Filenm = @"C:\bobcard\" + "Test.pdf";
#endregion