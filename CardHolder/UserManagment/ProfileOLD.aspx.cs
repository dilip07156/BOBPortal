using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System.Configuration;
namespace CardHolder.UserManagment
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class ProfileOLD : PageBase
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
                Mobile.Text = startmobilenumber + "XXXX" + endMobilenumber;

                if (addresstype != "")
                {
                    if (addresstype.ToUpper() == "O")
                        lblAddrestype.Text = Constants.OfficeAddress;
                    if (addresstype.ToUpper() == "P")
                        lblAddrestype.Text = Constants.Permanent_Address;
                    if (addresstype.ToUpper() == "C")
                        lblAddrestype.Text = Constants.Correspondence_Address;
                }

            }
        }


        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            frmProfile.ChangeMode(FormViewMode.Edit);
            LoadProfile();           
        }
        
       
        #endregion


       
    }
}