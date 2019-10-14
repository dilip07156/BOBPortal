using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;

namespace CardHolder.ServiceRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class RequestAddonCardPage : PageBase
    {
        #region Configuration
        /// <summary>
        /// 
        /// </summary>
        string[] EXTENSIONS_ATTACH = ConfigurationManager.AppSettings["ImageFiles"].ToString().Split(',');
        /// <summary>
        /// 
        /// </summary>
        string ROOT_UPLOAD_FOLDER = ConfigurationManager.AppSettings["ROOT_UPLOAD_FOLDER"].ToString();
        /// <summary>
        /// 
        /// </summary>
        string ADDON_FOLDER = ConfigurationManager.AppSettings["ADDON_FOLDER"].ToString();
        /// <summary>
        /// 
        /// </summary>
        string DEFAULT_STATUS = System.Configuration.ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        #endregion

        #region Load Data & Event

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf)
            {

            }
            else
            {
                if (Request.Params["requestid"] != null)
                {
                    hideRequestTypeId.Value = Request.Params["requestid"].ToString().Replace(" ", "+").DecryptURL();
                    if (!IsPostBack)
                    {
                        EnableDisableControl(true);
                        LoadRelation();
                        loadCreditCards();
                        txtDOB.Attributes.Add("readonly", "readonly");
                        TotalAddonCards();

                        bool IsAllowToAdd = CheckPendingRequest();
                        if (!IsAllowToAdd)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                            return;
                        }
                    }
                }
                else
                {
                    btnSubmit.Enabled = false;
                }
            }
        }
        #endregion

        #region POST Request Details
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string filename = "";
            string saveFile = "";
            int TotalAddons = 0;
            string IsaccountEligible = "";
            try
            {
                bool IsAllowToAdd = CheckPendingRequest();
                if (!IsAllowToAdd)
                {
                    Clearcontrols();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.PendingRequestState + "');", true);
                    return;
                }
                //Step 1 Check for addons
                IsaccountEligible = IsAccountEligible();
                if (IsaccountEligible == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.AccountNotEligible + "');", true);
                    return;
                }
                TotalAddons = TotalAddonCards();
                if (TotalAddons == 3)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.only3Addons + "');", true);
                    return;
                }

                ///Step 2 Upload File
                if (photoUpload.HasFile)
                {
                    var bytes = new byte[20];
                    photoUpload.PostedFile.InputStream.Read(bytes, 0, 20);


                    if (!GeneralMethods.CheckFileHeader(photoUpload.FileName, bytes, EXTENSIONS_ATTACH))
                    {
                        GeneralMethods.AlertMessage(Page, "Please Upload file having file type is .jpg or .jpeg or .png only");
                        return;
                    }
                    if (!System.IO.Directory.Exists(string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, ROOT_UPLOAD_FOLDER, ADDON_FOLDER)))
                    {
                        System.IO.Directory.CreateDirectory(string.Format("{0}{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, ROOT_UPLOAD_FOLDER, ADDON_FOLDER));
                    }
                    filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(photoUpload.FileName);
                    saveFile = string.Format("{0}{1}\\{2}\\{3}", AppDomain.CurrentDomain.BaseDirectory, ROOT_UPLOAD_FOLDER, ADDON_FOLDER, filename);
                    photoUpload.SaveAs(saveFile);
                }

                //Step 3 POST Request Detail Data
                CHRequestDetailManager crdm = new CHRequestDetailManager();
                long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                {
                    Request_Dt = DateTime.Now,
                    CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                    Relation = ddlRelation.SelectedValue,
                    Gender = rbMale.Checked == true ? "M" : (rbFeMale.Checked == true ? "F" : null),
                    Addon_Profile_Photo = filename,
                    DOB = Convert.ToDateTime(txtDOB.Text),
                    Add_On_Card_Applicant = txtApplicantName.Text.Trim(),
                    IP_Address = Request.UserHostAddress,
                    Created_dt = DateTime.Now,
                    Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                    Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                });
                Mailfunction(RequestDtlID);
                Clearcontrols();
                //lblMessage.Text = "Requset for Add-on card has been sent";
                //lblMessage.CssClass = "msgsuccess";
            }
            catch (Exception)
            {
                if (System.IO.File.Exists(saveFile))
                {
                    System.IO.File.Delete(saveFile);
                }
                lblMessage.Text = Constants.GeneralErrorMessage;
                lblMessage.CssClass = "error";
            }
        }

        /// <summary>
        /// Handles the Click event of the btnReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clearcontrols();
        }

        #endregion

        #region private functions

        /// <summary>
        /// Enable/Diable control
        /// </summary>
        private void EnableDisableControl(bool CtrlState)
        {
            txtApplicantName.Enabled = CtrlState;
            txtDOB.Enabled = CtrlState;
            ddlRelation.Enabled = CtrlState;
            rbMale.Enabled = CtrlState;
            rbFeMale.Enabled = CtrlState;
            photoUpload.Enabled = CtrlState;
            chkAgree.Enabled = CtrlState;
        }

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {
            btnReset.Visible = true;
            btnSubmit.Attributes.Add("class", "button");

            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();

            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(hideRequestTypeId.Value), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnReset.Visible = false;
                btnSubmit.Enabled = false;
                btnSubmit.Attributes.Add("class", "buttonDisble");
                EnableDisableControl(false);
                return false;
            }
            else
            {
                btnReset.Visible = true;
                btnSubmit.Enabled = true;
                EnableDisableControl(true);
                return true;
            }
        }



        /// <summary>
        /// Loads the relation.
        /// </summary>
        /// <remarks></remarks>
        private void LoadRelation()
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            List<DropDown_HdrDTO> list = dhm.SearchDllHeader("AddON Card Request").ToList();
            if (list.Count > 0)
            {
                ddlRelation.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlRelation.DataTextField = "Description";
                ddlRelation.DataValueField = "DropDown_Dtl_Id";
                ddlRelation.DataBind();
                ddlRelation.Items.Insert(0, new ListItem("Select Relation", "0"));
            }
            else
            {
                lblMessage.Text = Constants.RelationNotfound;
            }
        }

        /// <summary>
        /// Loads the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCards()
        {
            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;
            if (card != null)
            {
                lblCardHolder.Text = card.FULL_NAME;
                lblAccountNumber.Text = card.Cr_Account_Nbr;

                string Cardnumber = card.card_number;
                string StartCardnumber = "";
                string EndCardnumber = "";
                if (Cardnumber != "")
                {
                    StartCardnumber = Cardnumber.Substring(0, 4);
                    if (Cardnumber.Length == 16)
                        EndCardnumber = Cardnumber.Substring(13, 3);
                }

                lblCardNumber.Text = StartCardnumber + "XXXXXXXXX" + EndCardnumber;

            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }

        /// <summary>
        /// Mailfunctions the specified request DTL ID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <remarks></remarks>
        private void Mailfunction(long RequestDtlID)
        {
            string CardHolderName = lblCardHolder.Text;
            string Email = CardHolderManager.GetLoggedInUser().CH_Card.EMAIL_ID;
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"].ToString();
            string EMAIL_Subject = ConfigurationManager.AppSettings["REQUEST_EMAIL_SUBJECT"].ToString();


            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO chdto = new CH_Request_DtlDTO();
            chdto = cdm.getRequestUID(RequestDtlID);
            string RequestNumber = chdto.UID;
            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            if (!string.IsNullOrEmpty(OverRideEmail))
            {
                Email = OverRideEmail;
            }
            try
            {

                StringBuilder bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.AddonRequestTemplatepath));
                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@Accnum", lblAccountNumber.Text);
                bodyString.Replace("@@addonName", txtApplicantName.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);

                if (IsMailSent)
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('" + Constants.RequestRegister + "');", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ErrorMailButRqstLogged + "');", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ErrorMailButRqstLogged + "');", true);
            }
        }

        /// <summary>
        /// Clearcontrolses this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Clearcontrols()
        {
            txtApplicantName.Text = string.Empty;
            txtDOB.Text = string.Empty;
            ddlRelation.SelectedValue = "0";
            rbFeMale.Checked = false;
            rbMale.Checked = false;
            lblMessage.Text = "";
            chkAgree.Checked = false;
        }

        /// <summary>
        /// Totals the addon cards.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private int TotalAddonCards()
        {
            int TotalAccount = 0;
            CardManager cm = new CardManager();
            string Acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (Acc_num != "")
                TotalAccount = cm.CountTotalAddonCards(Acc_num);

            lblcountaddons.Text = Convert.ToString(TotalAccount); ;
            return TotalAccount;

        }

        /// <summary>
        /// Determines whether [is account eligible].
        /// </summary>
        /// <remarks></remarks>
        public string IsAccountEligible()
        {
            string EligibleAccount = "";
            CardManager cm = new CardManager();
            string Acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            if (Acc_num != "")
                EligibleAccount = cm.CardEligibleForAddOnReq(Acc_num);
            return EligibleAccount;
        }

        #endregion
    }
}