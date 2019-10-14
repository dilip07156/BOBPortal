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
    public partial class StatementDownload : PageBase
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
        string attachmentFileName = "attachment; filename=";

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
            try
            {
                lblDisplayMessage.Visible = false;
                string strReq = "";
                //strReq = Request.RawUrl;
                strReq = Request.Form["txtPostData"];
                // strReq = strReq.Substring(strReq.IndexOf('?') + 1);
                if (!strReq.Equals(""))
                    strReq = EncryptDecryptQueryString.Decrypt(strReq, qsk);

                string pdfPath = GetFilePath();
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                string filename = arrIndMsg[1].ToString().Trim();
                //string FilePath = directory + filename;
                if (File.Exists(pdfPath + filename))
                {
                    Response.ContentType = pdfContentType;
                    Response.AppendHeader(contentDisposition, attachmentFileName + filename);
                    //Response.TransmitFile(Server.MapPath(pdfPath + FileName)); //If path is virtual path
                    Response.TransmitFile(pdfPath + filename); //If path is physical path
                    Response.End();
                }
            }
            catch
            {
                lblDisplayMessage.Visible = true;
                lblDisplayMessage.InnerText = Constants.ErrorMsg1;
            }
        }


        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetFilePath()
        {
            string imageFilePath = string.Empty;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName(Constants.CardHolderStatementFilePath);
            if (obj != null && obj.Parameter_ValueC != null)
                imageFilePath = Convert.ToString(obj.Parameter_ValueC);
            return imageFilePath;
        }
        #endregion
    }
}