using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class OtherRequest_Complaint : PageBase
    {
        #region Variable
        string DEFAULT_STATUS = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString();
        #endregion

        #region Pageload

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
                    loadCreditCards();
                    if(RadioRequestComplaint.SelectedValue == "0")
                    {
                        LoadRequests();
                    }
                       
                }
                lblMessage.Text = "";
            }
        }

        #endregion

        #region ClickEvent

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlAppropRequestComplaint.SelectedValue == "-1" || RadioRequestComplaint.SelectedValue == "-1")
                {

                    lblMessage.Text = Constants.SelectReqComp;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                    return;
                }
                else if (RadioRequestComplaint.SelectedValue == "0")
                {
                    bool IsAllowToAdd = CheckPendingRequest();
                    if (!IsAllowToAdd)
                    {
                       
                        lblMessage.Text = Constants.PendingRequestState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }


                    CHRequestDetailManager crdm = new CHRequestDetailManager();
                   

                    int pendingcount = crdm.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt64(ddlAppropRequestComplaint.SelectedValue), ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString());
                    if (pendingcount > 0)
                    {
                        lblMessage.Text = Constants.PendingRequestState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                    }
                    else
                    {
                        long RequestDtlID = crdm.SaveRequestDetail(new CH_Request_DtlDTO()
                        {
                            Request_Dt = DateTime.Now,
                            CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                            IP_Address = Request.UserHostAddress,
                            RequestType_Id = Convert.ToInt64(ddlAppropRequestComplaint.SelectedValue),
                            Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                            Created_dt = DateTime.Now,
                            Remarks = Convert.ToString(txtremarks.Text),
                            Request_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()

                        });
                        Mailfunction(RequestDtlID);
                    }
                }

                else if (RadioRequestComplaint.SelectedValue == "1")
                {
                    bool IsAllowToAdd = CheckPendingComplaint();
                    if (!IsAllowToAdd)
                    {
                        lblMessage.Text = Constants.PendingRequestState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }

                    CardHolderComplaintManager crdm = new CardHolderComplaintManager();
                    long ComplaintDtlID = crdm.SaveComplaintDetail(new CH_Complaint_DtlDTO()
                    {
                        Complaint_Dt = DateTime.Now,
                        CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        IP_Address = Request.UserHostAddress,
                        ComplaintType_Id = Convert.ToInt64(ddlAppropRequestComplaint.SelectedValue),
                        Created_by = CardHolderManager.GetLoggedInUser().CardHolder_Id,
                        Created_dt = DateTime.Now,
                        Remarks = Convert.ToString(txtremarks.Text),
                        Complaint_Status = ConfigurationManager.AppSettings["DEFAULT_STATUS"].ToString()
                    });
                    Mailfunction(ComplaintDtlID);
                }
                
               Clearcontrols();
            }
            catch (Exception ex)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the requests.
        /// </summary>
        /// <remarks></remarks>
        private void LoadRequests()
        {
            CardHolderRequestManager chrm = new CardHolderRequestManager();
            ddlAppropRequestComplaint.DataSource = chrm.getCHStandardRequestType();
            ddlAppropRequestComplaint.DataTextField = "RequestType_nm";
            ddlAppropRequestComplaint.DataValueField = "RequestType_Id";
            ddlAppropRequestComplaint.DataBind();
            ddlAppropRequestComplaint.Items.Insert(0, new ListItem("Select Request", "-1"));

        }

        /// <summary>
        /// Loads the complaints.
        /// </summary>
        /// <remarks></remarks>
        private void LoadComplaints()
        {
            CardHolderComplaintManager chrm = new CardHolderComplaintManager();
            ddlAppropRequestComplaint.DataSource = chrm.getCHComplaintType();
            ddlAppropRequestComplaint.DataTextField = "ComplaintType_nm";
            ddlAppropRequestComplaint.DataValueField = "ComplaintType_Id";
            ddlAppropRequestComplaint.DataBind();
            ddlAppropRequestComplaint.Items.Insert(0, new ListItem(Constants.DDLComplaint, "-1"));

        }

        /// <summary>
        /// Mailfunctions the specified request compliant DTL id.
        /// </summary>
        /// <param name="requestCompliantDtlId">The request compliant DTL id.</param>
        /// <remarks></remarks>
        private void Mailfunction(long requestCompliantDtlId)
        {
            var cdm = new CHRequestDetailManager();
            var ChRequestDto = new CH_Request_DtlDTO();

            var ccm = new CardHolderComplaintManager();
            var ChComplaintDTO = new CH_Complaint_DtlDTO();

            string EMAIL_Subject = string.Empty;
            string CardHolderName = lblCardHolder.Text;
            string Email = CardHolderManager.GetLoggedInUser().CH_Card.EMAIL_ID;
            string RequestComplType = ddlAppropRequestComplaint.SelectedItem.Text;
            string RequestComplaintDtl = txtremarks.Text;
            string BOBMail = ConfigurationManager.AppSettings["BOB_EMAIL"];
            // long cardHolderID = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            string OverRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            if (!string.IsNullOrEmpty(OverRideEmail))
            {
                Email = OverRideEmail;
            }

            try
            {

                var bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("../") + Constants.OtherReqComplTemplatepath));

                if (RadioRequestComplaint.SelectedValue == "1")
                {
                    ChComplaintDTO = ccm.getComplaintUID(requestCompliantDtlId);
                    string ComplaintNumber = ChComplaintDTO.UID;
                    bodyString.Replace("Request Type", "Complaint Type");
                    //bodyString.Replace("Request Details", "Complaint Details");
                    bodyString.Replace("@@reqcom", "Complaint");
                    bodyString.Replace("@@ReqNum", ComplaintNumber);
                    EMAIL_Subject = ConfigurationManager.AppSettings["COMPLAINT_EMAIL_SUBJECT"].ToString();
                }
                else
                {
                    ChRequestDto = cdm.getRequestUID(requestCompliantDtlId);
                    string RequestNumber = ChRequestDto.UID;
                    bodyString.Replace("@@reqcom", "Request");
                    bodyString.Replace("@@ReqNum", RequestNumber);
                    EMAIL_Subject = ConfigurationManager.AppSettings["REQUEST_EMAIL_SUBJECT"].ToString();
                }

                bodyString.Replace("@@CardHolderName", CardHolderName);
                bodyString.Replace("@@Creditcard", lblCreditCardNumber.Text);
                bodyString.Replace("@@RequestType", RequestComplType);
                bodyString.Replace("@@RequestDetails", RequestComplaintDtl);
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                var CCemail = new List<string>();
                long CardHolderId = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                bool IsMailSent = SendMailfunction.SendMail(BOBMail, new List<string>() { Email }, CCemail, "", "", EMAIL_Subject, bodyString.ToString(), true, CardHolderId, null);
                if (IsMailSent)
                {
                    LblSuccessMessage.Text = "Your Request/Complaint has been successfully registered";
                    DivSuccess.Attributes.CssStyle.Add("display", "block");
                }
                    
                else
                {
                    LblErrorMessage.Text = Constants.ErrorMailButRqstLogged;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                }
                    
            }
            catch (Exception)
            {
                LblErrorMessage.Text = Constants.ErrorMailButRqstLogged;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        /// <summary>
        /// Loads the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCards()
        {
            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;
            string Cardnumber = string.Empty;
            if (card != null)
            {
               
                Cardnumber = card.card_number;
                string StartCardnumber = "";
                string EndCardnumber = "";
                if (Cardnumber != "")
                {
                    StartCardnumber = Cardnumber.Substring(0, 4);
                    if (Cardnumber.Length == 16)
                        EndCardnumber = Cardnumber.Substring(13, 3);
                }

                lblCreditCardNumber.Text = StartCardnumber + "XXXXXXXXX" + EndCardnumber;
            }

            CH_CardDTO chdto = new CH_CardDTO();
            CardManager cm = new CardManager();
            chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Cardnumber });
            if (chdto != null)
            {

                string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                lblCardHolder.Text = firstName + " " + lastName;
            }

        }

        /// <summary>
        /// Clearcontrolses this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Clearcontrols()
        {
            //ddlAppropRequestComplaint.Items.Clear();
            //RadioRequestComplaint.SelectedValue = "0";
            txtremarks.Enabled = false;
            txtremarks.Text = string.Empty;
            chkAgree.Checked = false;
            chkAgree.Enabled = false;
            //ddlAppropRequestComplaint.Enabled = false;
            //RadioRequestComplaint.Enabled = false;
            btnSubmit.Enabled = false;
        }


        /// <summary>
        /// check if reqeust staus is pending than not allow to add request for same card holder.
        /// </summary>
        private bool CheckPendingComplaint()
        {
            btnSubmit.Attributes.Add("class", "button");
            CardHolderComplaintManager objCardHolderComplaintManager = new CardHolderComplaintManager();
            CH_Complaint_DtlDTO objCH_Complaint_DtlDTO = new CH_Complaint_DtlDTO();

            objCH_Complaint_DtlDTO.CardHolder_Id = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            objCH_Complaint_DtlDTO.ComplaintType_Id = Convert.ToInt32(ddlAppropRequestComplaint.SelectedValue);
            objCH_Complaint_DtlDTO.Complaint_Status = DEFAULT_STATUS;

            int PendingCount = objCardHolderComplaintManager.CheckComplaintPending(objCH_Complaint_DtlDTO);
            if (PendingCount > 0)
            {
                btnSubmit.Enabled = false;
                btnSubmit.Attributes.Add("class", "buttonDisble");
                EnableDisableControl(false);
                return false;
            }
            else
            {
                btnSubmit.Enabled = true;
                EnableDisableControl(true);
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
                return true;
            }

        }
        private bool CheckPendingRequest()
        {
            btnSubmit.Attributes.Add("class", "button");
            CHRequestDetailManager objCHRequestDetailManager = new CHRequestDetailManager();

            int pendingCount = objCHRequestDetailManager.CheckRequestPending(CardHolderManager.GetLoggedInUser().CardHolder_Id, Convert.ToInt32(ddlAppropRequestComplaint.SelectedValue), DEFAULT_STATUS);
            if (pendingCount > 0)
            {
                btnSubmit.Enabled = false;
                btnSubmit.Attributes.Add("class", "buttonDisble");
                EnableDisableControl(false);
                return false;
            }
            else
            {
                btnSubmit.Enabled = true;
                EnableDisableControl(true);
                return true;
            }
        }


        /// <summary>
        /// Enable/Diable control
        /// </summary>
        private void EnableDisableControl(bool CtrlState)
        {
            txtremarks.Enabled = CtrlState;
            chkAgree.Enabled = CtrlState;            
        }




        #endregion

        #region DropdownEvents

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlReqcomplaint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void OnRadio_Changed(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            btnSubmit.Attributes.Add("class", "button");
            EnableDisableControl(true);
            LblErrorMessage.Text = string.Empty;
            DivERROR.Attributes.CssStyle.Add("display", "none");
            lblMessage.Text = string.Empty;
            DivMessage.Attributes.CssStyle.Add("display", "none");
            txtremarks.Text = string.Empty;
            chkAgree.Checked = false;

            if (RadioRequestComplaint.SelectedValue == "0")
            {
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
                LoadRequests();
            }
               
            else if (RadioRequestComplaint.SelectedValue == "1")
            {
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
                LoadComplaints();
            }
               
            else
            {
                lblMessage.Text = Constants.SelectReqComp;               
                DivMessage.Attributes.CssStyle.Add("display", "block");
                ddlAppropRequestComplaint.Items.Clear();

            }
        }
        protected void ddlAppropRequestComplaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblErrorMessage.Text = string.Empty;
            DivERROR.Attributes.CssStyle.Add("display", "none");
            lblMessage.Text = string.Empty;            
            DivMessage.Attributes.CssStyle.Add("display", "none");
            txtremarks.Text = string.Empty;
            chkAgree.Checked = false;
            if (ddlAppropRequestComplaint.SelectedValue != "-1")
            {
                if (RadioRequestComplaint.SelectedValue == "0")
                {
                    bool IsAllowToAdd = CheckPendingRequest();
                    if (!IsAllowToAdd)
                    {
                      
                        lblMessage.Text = Constants.PendingRequestState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }
                }
                else if (RadioRequestComplaint.SelectedValue == "1")
                {
                    bool IsAlloToAdd = CheckPendingComplaint();
                    if (!IsAlloToAdd)
                    {
                        lblMessage.Text = Constants.PendingComplaintState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                        return;
                    }
                }
                else
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Attributes.Add("class", "button");
                    EnableDisableControl(true);
                }
            }
            else
            {
                btnSubmit.Enabled = true;
                btnSubmit.Attributes.Add("class", "button");
                EnableDisableControl(true);
                lblMessage.Text = string.Empty;
                DivMessage.Attributes.CssStyle.Add("display", "none");
            }
        }
        #endregion


    }
}