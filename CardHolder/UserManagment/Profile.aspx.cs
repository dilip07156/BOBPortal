using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System.Configuration;
using System.Web.UI;

namespace CardHolder.UserManagment
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class Profile : PageBase
    {
        #region Configuration
        /// <summary>
        /// 
        /// </summary>
        string[] EXTENSIONS_ATTACH = ConfigurationManager.AppSettings["ImageFiles"].ToString().Split(',');
        /// <summary>
        /// 
        /// </summary>
        string _rootUploadFolder = ConfigurationManager.AppSettings["ROOT_UPLOAD_FOLDER"];
        /// <summary>
        /// 
        /// </summary>
        string _profileFolder = ConfigurationManager.AppSettings["PROFILE_FOLDER"];
        /// <summary>
        /// 
        /// </summary>
        string _addonFolder = ConfigurationManager.AppSettings["ADDON_FOLDER"];
        /// <summary>
        /// 
        /// </summary>
        int _maxSize = Convert.ToInt32(ConfigurationManager.AppSettings["Upload_SizeBytes"]); //Limit of 20 KB for uploading photoes
        #endregion

        #region Load Event & Methods
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
                    LoadProfile();                    
                }

            }


        }

        #endregion

        /// <summary>
        /// Loads the profile.
        /// </summary>
        /// <remarks></remarks>
        private void LoadProfile()
        {
            string startmobilenumber = "";
            string endMobilenumber = "";
            CardHolder_MstDTO cardHolder = CardHolderManager.GetLoggedInUser();
            frmProfile.DataSource = new List<CardHolder_MstDTO>() { cardHolder };
            frmProfile.DataBind();


            if (cardHolder != null && cardHolder.CH_Card != null)
            {
                string mobilenumber = cardHolder.CH_Card.PHONE_MOBILE;
                string addresstype = cardHolder.CH_Card.PREFERRED_MAILING_ADDRESS;
                if (mobilenumber != "")
                {
                    startmobilenumber = mobilenumber.Substring(0, 4);
                    string lastTwodgts;
                    int numberkength = mobilenumber.Length;
                    if (numberkength > 2)
                        lastTwodgts = mobilenumber.Substring(numberkength - 2, 2);
                    else
                        lastTwodgts = mobilenumber;
                    endMobilenumber = lastTwodgts;
                }
                Label Mobile = frmProfile.FindControl("lblMobileNumber") as Label;
                Label lblAddrestype = frmProfile.FindControl("lblAddrestype") as Label;
                Label LblAddress = frmProfile.FindControl("LblAddress") as Label;
                Mobile.Text = "+91" + "  " +  startmobilenumber + "XXXX" + endMobilenumber;

                if (addresstype != "")
                {
                    if (addresstype.ToUpper() == "O")
                        lblAddrestype.Text = Constants.OfficeAddress;
                    if (addresstype.ToUpper() == "P")
                        lblAddrestype.Text = Constants.Permanent_Address;
                    if (addresstype.ToUpper() == "C")
                        lblAddrestype.Text = Constants.Correspondence_Address;
                }

                /// addrees
                string MAILING_ADDRESS1 = string.Empty;
                string MAILING_ADDRESS2 = string.Empty;
                string MAILING_ADDRESS3 = string.Empty;
                string MAILING_ADDRESS4 = string.Empty;
                if (!string.IsNullOrEmpty(cardHolder.CH_Card.MAILING_ADDRESS1))
                 MAILING_ADDRESS1 = UrlHelper.FirstCharToUpper(cardHolder.CH_Card.MAILING_ADDRESS1.ToLower());
                if (!string.IsNullOrEmpty(cardHolder.CH_Card.MAILING_ADDRESS2))
                     MAILING_ADDRESS2 = UrlHelper.FirstCharToUpper(cardHolder.CH_Card.MAILING_ADDRESS2.ToLower());
                if (!string.IsNullOrEmpty(cardHolder.CH_Card.MAILING_ADDRESS3))
                     MAILING_ADDRESS3 = UrlHelper.FirstCharToUpper(cardHolder.CH_Card.MAILING_ADDRESS3.ToLower());
                if (!string.IsNullOrEmpty(cardHolder.CH_Card.MAILING_ADDRESS4))
                     MAILING_ADDRESS4 = UrlHelper.FirstCharToUpper(cardHolder.CH_Card.MAILING_ADDRESS4.ToLower());
                LblAddress.Text = MAILING_ADDRESS1 + " " + MAILING_ADDRESS2  + " " + MAILING_ADDRESS3 + " " + MAILING_ADDRESS4;

            }
        }



        #region Post Events

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //frmProfile.ChangeMode(FormViewMode.Edit);
            TextBox txtperonsal = frmProfile.FindControl("txtpersonalmsg") as TextBox;            
            HtmlGenericControl DivSuccess = frmProfile.FindControl("DivSuccess") as HtmlGenericControl;
            //Step 4 Update Card Holder
            CardHolder_MstDTO CardHolder = CardHolderManager.GetLoggedInUser();            
            CardHolder.Updated_by = CardHolder.CardHolder_Id;
            CardHolder.Updated_dt = DateTime.Now;
            CardHolder.IP_Address = Request.UserHostAddress.Trim();
            if (txtperonsal.Text != "")
            {
                CardHolder.Personal_Msg = txtperonsal.Text;
            }            

            CardHolderManager chm = new CardHolderManager();
            chm.SaveCardHolder(CardHolder);
            frmProfile.ChangeMode(FormViewMode.ReadOnly);           

            ScriptManager.RegisterStartupScript(this, GetType(), "showsuccess", "showSuccess();", true);
            LoadProfile();
        }



        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            frmProfile.ChangeMode(FormViewMode.ReadOnly);
            LoadProfile();            
        }
        #endregion

    
    }
}