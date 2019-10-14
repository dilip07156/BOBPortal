using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardHolder.ServiceRequest
{
    public partial class CardRequest : PageBase
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf) { }
            else
            {
                if (Request.Params["replacementid"] != null && Request.Params["renewalid"] != null && Request.Params["deregistercreditcardid"] != null && Request.Params["requestaddoncardid"] != null)
                {
                    hideRequestTypeADDONId.Value = Request.Params["requestaddoncardid"].ToString().Replace(" ", "+").DecryptURL();
                    hideRequestTypeId.Value = Request.Params["deregistercreditcardid"].ToString().DecryptURL();
                    if (!IsPostBack)
                    {
                        LoadServiceRequest();
                        LoadRequest();
                        mvFrgtUname.ActiveViewIndex = 0;
                        LoadCardsinDDL();
                        loadCreditCardsName();
                        EnableDisalbeControl(true);
                        LoadRelation();
                        //txtDOB.Attributes.Add("readonly", "readonly");
                        TotalAddonCards();

                        if (Request.Params["IsAddOn"] != null)
                        {
                            string IsAddOn = Request.Params["IsAddOn"].ToString().DecryptURL();
                            if (IsAddOn == "Yes")
                            {
                                ddlRequestService.SelectedIndex = 2;
                                RequestForAddOn();
                            }
                        }


                        if (ddlRequestService.SelectedItem.Text == @"Request/Renewal")
                        {
                            bool IsAllowToAdd = CheckPendingServiceRequest(Request.Params["replacementid"].ToString().DecryptURL());
                            bool IsAllowToAddForRenewal = CheckPendingServiceRequest(Request.Params["renewalid"].ToString().DecryptURL());
                            if (!IsAllowToAdd)
                            {
                                string requestService = "Replacement";
                                lblMessage.Text = "Sorry! request for " + requestService + " is already in pending state, you cannot make this request";
                                DivMessage.Attributes.CssStyle.Add("display", "block");
                                return;
                            }
                            else if (!IsAllowToAddForRenewal)
                            {
                                string requestService = "Renewal";
                                lblMessage.Text = "Sorry! request for " + requestService + " is already in pending state, you cannot make this request";
                                DivMessage.Attributes.CssStyle.Add("display", "block");
                                return;
                            }
                        }
                        else
                        {

                            bool IsAllowToAdd = CheckPendingRequest();
                            if (!IsAllowToAdd)
                            {
                                string requestService = ddlRequestService.SelectedItem.Text;
                                lblMessage.Text = "Sorry! request for " + requestService + " is already in pending state, you cannot make this request";
                                DivMessage.Attributes.CssStyle.Add("display", "block");
                                return;
                            }
                        }

                    }
                    btnSubmitfinal.Enabled = true;
                }
                else
                {
                    btnSubmitfinal.Enabled = false;
                }
            }

        }

        #region Data Load Events
        /// <summary>
        /// Loads the request.
        /// </summary>
        /// <remarks></remarks>
        private void LoadRequest()
        {
            ddlRequestType.Items.Insert(0, new ListItem("Renewal", Request.Params["renewalid"].ToString().DecryptURL()));
            ddlRequestType.Items.Insert(0, new ListItem("Replacement", Request.Params["replacementid"].ToString().DecryptURL()));
            ddlRequestType.Items.Insert(0, new ListItem(Constants.DDLRequest, "0"));
        }

        /// <summary>
        /// Loads the request.
        /// </summary>
        /// <remarks></remarks>
        private void LoadServiceRequest()
        {
            ListItem item;
            item = new ListItem(@"Request/Renewal", Request.Params["replacementid"].ToString().DecryptURL());
            ddlRequestService.Items.Add(item);
            item = new ListItem("De-Register Credit Card", Request.Params["deregistercreditcardid"].ToString().DecryptURL());
            ddlRequestService.Items.Add(item);
            item = new ListItem("Request for Add-On card", Request.Params["requestaddoncardid"].ToString().Replace(" ", "+").DecryptURL());
            ddlRequestService.Items.Add(item);
        }


        /// <summary>
        /// Loads the cardsin DDL.
        /// </summary>
        /// <remarks></remarks>
        private void LoadCardsinDDL()
        {
            string CR_acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            CardManager cm = new CardManager();
            if (CR_acc_num != "")
            {
                ddlcardlist.DataSource = cm.GetAllCardsForReplaceRenew(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
                ddlcardlist.DataTextField = "MASK_CARD_NUMBER";
                ddlcardlist.DataValueField = "CARD_NUMBER";
                ddlcardlist.DataBind();
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
                btnSubmitfinal.Enabled = false;
            }
        }

        /// <summary>
        /// Loads the name of the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCardsName()
        {
            string Card_number = ddlcardlist.SelectedValue;
            CardManager cm = new CardManager();
            CH_CardDTO chdto = new CH_CardDTO();
            if (Card_number != "")
            {
                chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Card_number });
                string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                if (chdto != null)
                    lblCardHolder.Text = firstName + " " + lastName;
            }
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
        #endregion


        #region Private Methods

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequest()
        {
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();
            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(ddlRequestService.SelectedValue), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnSubmitfinal.Enabled = false;
                EnableDisalbeControl(false);
                return false;
            }
            else
            {
                btnSubmitfinal.Enabled = true;
                EnableDisalbeControl(true);
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
                return true;
            }
        }

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingServiceRequest(string RequestID)
        {
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();
            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(RequestID), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnSubmitfinal.Enabled = false;
                EnableDisalbeControl(false);
                return false;
            }
            else
            {
                btnSubmitfinal.Enabled = true;
                EnableDisalbeControl(true);
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
                return true;
            }
        }

        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingRequestForReplacementRenewal()
        {
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();
            int PendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(ddlRequestType.SelectedValue), DEFAULT_STATUS);
            if (PendingCount > 0)
            {
                btnSubmitfinal.Enabled = false;
                EnableDisalbeControl(false);
                return false;
            }
            else
            {
                btnSubmitfinal.Enabled = true;
                EnableDisalbeControl(true);
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
                return true;
            }
        }
        /// <summary>
        /// Enable/disable control
        /// </summary>        
        private void EnableDisalbeControl(bool ctrlState)
        {
            ddlReasons.Enabled = ctrlState;
            chkAgree.Enabled = ctrlState;
            txtApplicantName.Enabled = ctrlState;
            txtDOB.Enabled = ctrlState;
            ddlRelation.Enabled = ctrlState;
            rbMale.Enabled = ctrlState;
            rbFeMale.Enabled = ctrlState;
            rbOther.Enabled = ctrlState;
            photoUpload.Enabled = ctrlState;
            chkAgree.Enabled = ctrlState;
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
                DivMessage.Attributes.CssStyle.Add("display", "block");
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlRequestType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlRequestService_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            DivMessage.Attributes.CssStyle.Add("display", "none");

            CardHolderReasonManager chrm = new CardHolderReasonManager();
            if (ddlRequestService.SelectedItem.Text == @"Request/Renewal")
            {
                mvFrgtUname.ActiveViewIndex = 0;
                ddlReasons.Items.Clear();
                ddlReasons.DataBind();
                LoadRequest();
            }
            else if (ddlRequestService.SelectedItem.Text == "De-Register Credit Card")
            {
                mvFrgtUname.ActiveViewIndex = 1;
                ddlRequestType.Items.Clear();
                ddlRequestType.DataBind();
            }
            else if (ddlRequestService.SelectedItem.Text == "Request for Add-On card")
            {
                RequestForAddOn();
            }

            if (ddlRequestService.SelectedItem.Text != string.Empty)
            {


                bool IsAllowToAdd = CheckPendingRequest();
                if (!IsAllowToAdd)
                {

                    Clearcontrols();
                    lblMessage.Text = Constants.PendingRequestState;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }



            }
            else
            {
                btnSubmitfinal.Enabled = true;
                EnableDisalbeControl(true);
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlRequestType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {

            CardHolderReasonManager chrm = new CardHolderReasonManager();
            if (ddlRequestType.SelectedItem.Text == "Replacement")
            {
                ddlReasons.DataSource = chrm.ListReasonByRequestId(Convert.ToInt64(ddlRequestType.SelectedValue));
                ddlReasons.DataTextField = "Reason_nm";
                ddlReasons.DataValueField = "RequestReason_Id";
                ddlReasons.DataBind();
            }

            else if (ddlRequestType.SelectedItem.Text == "Renewal")
            {
                ddlReasons.DataSource = chrm.ListReasonByRequestId(Convert.ToInt64(ddlRequestType.SelectedValue));
                ddlReasons.DataTextField = "Reason_nm";
                ddlReasons.DataValueField = "RequestReason_Id";
                ddlReasons.DataBind();
            }
            else
            {
                ddlReasons.Items.Clear();
                ddlReasons.DataBind();
            }
            ddlReasons.Items.Insert(0, new ListItem(Constants.DDLReason, "0"));

            lblMessage.Text = string.Empty;
            DivMessage.Attributes.CssStyle.Add("display", "none");

            if (ddlRequestType.SelectedValue != "0")
            {
                bool IsAllowToAdd = CheckPendingRequestForReplacementRenewal();
                if (!IsAllowToAdd)
                {
                    Clearcontrols();
                    string requestService = ddlRequestType.SelectedItem.Text;
                    lblMessage.Text = "Sorry! request for " + requestService + " is already in pending state, you cannot make this request";
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    DivMessage.Attributes.CssStyle.Add("display", "none");
                }
            }
            else
            {
                btnSubmitfinal.Enabled = true;
                EnableDisalbeControl(true);
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlcardlist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ddlcardlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreditCardsName();
        }

        /// <summary>
        /// Clearcontrolses this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Clearcontrols()
        {
            //ddlRequestService.SelectedValue = "3";
            ddlReasons.Items.Clear();
            ddlReasons.Enabled = false;
            chkAgree.Enabled = false;
            chkAgree.Checked = false;
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

            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            if (!string.IsNullOrEmpty(OverRideEmail))
            {
                Email = OverRideEmail;
            }
            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO chdto = new CH_Request_DtlDTO();
            chdto = cdm.getRequestUID(RequestDtlID);
            string RequestNumber = chdto.UID;
            try
            {
                StringBuilder bodyString = new StringBuilder();
                if (ddlRequestService.SelectedItem.Text == @"Request/Renewal")
                {
                    bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.CreditCardReplacementRenewalTemplatepath));
                }
                else if (ddlRequestService.SelectedItem.Text == "De-Register Credit Card")
                {
                    bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.DeRegisterCreditCardTemplatepath));
                }
                else if (ddlRequestService.SelectedItem.Text == "Request for Add-On card")
                {
                    bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.AddonRequestTemplatepath));
                }

                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@CreditCard", ddlcardlist.SelectedItem.Text);
                bodyString.Replace("@@RepRen", ddlRequestService.SelectedItem.Text);
                if(ddlReasons.SelectedItem != null)
                bodyString.Replace("@@Reason", ddlReasons.SelectedItem.Text);
                bodyString.Replace("@@ReqNum", RequestNumber);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/bob-logo.png");
                List<string> CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
                if (IsMailSent)
                {
                    LblSuccessMessage.Text = string.Format("Credit Card {0} Request has been sent", ddlRequestService.SelectedItem.Text);
                    DivSuccess.Attributes.CssStyle.Add("display", "block");
                }
                else
                {
                    LblErrorMessage.Text = Constants.ErrorMailButRqstLogged;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                }

            }
            catch (Exception ex)
            {
                LblErrorMessage.Text = Constants.ErrorMailButRqstLogged;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }
        #endregion


        #region Post Request Detail
        /// <summary>
        /// Handles the Click event of the btnSubmitfinal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmitfinal_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                string filename = "";
                string saveFile = "";
                int TotalAddons = 0;
                string IsaccountEligible = "";

                if (ddlRequestService.SelectedItem.Text == @"Request/Renewal")
                {
                    bool IsAllowToAdd = CheckPendingRequestForReplacementRenewal();
                    if (!IsAllowToAdd)
                    {
                        Clearcontrols();
                        string requestService = ddlRequestType.SelectedItem.Text;
                        lblMessage.Text = "Sorry! request for " + requestService + " is already in pending state, you cannot make this request";
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;


                    }
                    else
                    {
                        lblMessage.Text = string.Empty;
                        DivMessage.Attributes.CssStyle.Add("display", "none");
                    }

                }
                else
                {
                    bool IsAllowToAdd = CheckPendingRequest();
                    if (!IsAllowToAdd)
                    {
                        Clearcontrols();
                        lblMessage.Text = Constants.PendingRequestState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }
                    else
                    {
                        lblMessage.Text = string.Empty;
                        DivMessage.Attributes.CssStyle.Add("display", "none");
                    }

                }



                if (ddlRequestService.SelectedItem.Text == "Request for Add-On card")
                {
                    //Step 1 Check for addons
                    IsaccountEligible = IsAccountEligible();
                    if (IsaccountEligible == "")
                    {
                        lblMessage.Text = Constants.AccountNotEligible;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }
                    TotalAddons = TotalAddonCards();
                    if (TotalAddons == 3)
                    {
                        lblMessage.Text = Constants.only3Addons;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
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

                }
                CHRequestDetailManager crdm = new CHRequestDetailManager();
                long RequestDtlID = 0;
                if (ddlRequestService.SelectedItem.Text == @"Request/Renewal")
                {
                    RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                    {
                        Request_Dt = DateTime.Now,
                        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        RequestType_Id = Convert.ToInt64(ddlRequestType.SelectedValue),
                        IP_Address = Request.UserHostAddress,
                        RequestReason_Id = Convert.ToInt64(ddlReasons.SelectedValue),
                        Created_dt = DateTime.Now,
                        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                    });
                }
                else if (ddlRequestService.SelectedItem.Text == "De-Register Credit Card")
                {
                    RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                    {
                        Request_Dt = DateTime.Now,
                        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        RequestType_Id = Convert.ToInt64(hideRequestTypeId.Value),
                        IP_Address = Request.UserHostAddress,
                        Created_dt = DateTime.Now,
                        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                    });
                }
                else if (ddlRequestService.SelectedItem.Text == "Request for Add-On card")
                {
                    RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                    {
                        Request_Dt = DateTime.Now,
                        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        RequestType_Id = Convert.ToInt64(hideRequestTypeADDONId.Value),
                        Relation = ddlRelation.SelectedValue,
                        Gender = rbMale.Checked == true ? "M" : (rbFeMale.Checked == true ? "F" : (rbOther.Checked == true ? "Other" : null)),
                        Addon_Profile_Photo = filename,
                        DOB = DateTime.ParseExact(txtDOB.Text, "dd/mm/yyyy", null),
                        Add_On_Card_Applicant = txtApplicantName.Text.Trim(),
                        IP_Address = Request.UserHostAddress,
                        Created_dt = DateTime.Now,
                        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                    });
                }

                Mailfunction(RequestDtlID);
                Clearcontrols();
            }
            catch (Exception ex)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
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

        public void RequestForAddOn()
        {
            mvFrgtUname.ActiveViewIndex = 2;
            Divaddoncards.Attributes.CssStyle.Add("display", "block");
            ddlRequestType.Items.Clear();
            ddlRequestType.DataBind();
            loadCreditCards();
        }
        #endregion
    }
}