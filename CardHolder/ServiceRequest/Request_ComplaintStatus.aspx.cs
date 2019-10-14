using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
    public partial class Request_ComplaintStatus : PageBase
    {
        #region PageVariables

        /// <summary>
        /// Gets or sets Total record count of user list
        /// </summary>
        /// <value>The record count.</value>
        /// <remarks></remarks>
        public int RecordCount
        {
            /// Gets record count
            get { return ViewState["RecordCount"] == null ? 0 : Convert.ToInt32(ViewState["RecordCount"].ToString()); }

            /// Sets record count
            set { ViewState["RecordCount"] = value; }
        }

        public int NumberofRowsShows = 10;

        public List<CH_Request_DtlDTO> lstRequestStatus = new List<CH_Request_DtlDTO> { };

        public List<CH_Complaint_DtlDTO> lstComplaintStatus = new List<CH_Complaint_DtlDTO> { };

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
            if (IsXsrf) { }
            else
            {

                if (!IsPostBack)
                {
                    loadCreditCards();
                    btnRequstViewMore.Visible = false;
                    btnComplaintViewMore.Visible = false;
                    //gridheader.Visible = false;
                }
                //lblMessage.Text = "";
            }
        }
        /// <summary>
        /// Page pre render event to set pager item count
        /// </summary>
        /// <param name="e">event argument</param>
        /// <remarks></remarks>
        protected override void OnPreRender(EventArgs e)
        {
            //Pager1.ItemCount = RecordCount;
            //Pager2.ItemCount = RecordCount;
            base.OnPreRender(e);
        }
        #endregion

        #region Events
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

                if (ddlReqcomplaint.SelectedValue == "0")
                {
                    BindRequests(NumberofRowsShows);
                }

                else if (ddlReqcomplaint.SelectedValue == "1")
                {
                    BindComplaints(NumberofRowsShows);
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = Constants.GeneralErrorMessage;
            }
        }

        /// <summary>
        /// To View details of Request
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void gvRequestCH_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    Pager1.Visible = RecordCount > Pager1.PageSize;
        //    Pager2.Visible = false;
        //    if (e.CommandName == "ViewRequest")
        //    {
        //        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
        //        Label lblRqsttype = (Label)row.FindControl("lblrqsttype");
        //        try
        //        {
        //            string viewRequest = CardHolder.BAL.Controller.CHRequestView.ProcessTemplate(e.CommandArgument);
        //            ltrDetail.Text = viewRequest;
        //            if (lblRqsttype.Text.ToLower().Replace(" ", "") == "statementmode" || lblRqsttype.Text.ToLower().Replace(" ", "") == "preservedstatementrequest")
        //                trStmntType.Visible = true;
        //            else
        //                trStmntType.Visible = false;
        //        }
        //        catch (Exception)
        //        {
        //            lblMessage.Text = Constants.GeneralErrorMessage;
        //            lblMessage.CssClass = "error";
        //        }
        //        ClientScript.RegisterStartupScript(typeof(Page), "loadPopupBox", "loadPopupBox();", true);
        //    }
        //}

        /// <summary>
        /// To View details of Complaint
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        //protected void gvComplaintCH_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    Pager2.Visible = RecordCount > Pager2.PageSize;
        //    Pager1.Visible = false;

        //    if (e.CommandName == "View")
        //    {
        //        try
        //        {
        //            string view = CardHolder.BAL.Controller.CHComplaintView.ProcessTemplate(e.CommandArgument);
        //            ltrDetail.Text = view;
        //            trStmntType.Visible = false;
        //        }
        //        catch (Exception)
        //        {
        //            lblMessage.Text = Constants.GeneralErrorMessage;
        //            lblMessage.CssClass = "error";
        //        }
        //        ClientScript.RegisterStartupScript(typeof(Page), "loadPopupBox", "loadPopupBox();", true);
        //    }
        //}

        /// <summary>
        /// Pager command
        /// </summary>
        /// <param name="sender">sender control</param>
        /// <param name="e">event argument</param>
        /// <remarks></remarks>
        protected void pager_Command(object sender, CommandEventArgs e)
        {
            Pager1.CurrentIndex = Convert.ToInt32(e.CommandArgument.ToString());
            BindRequests(NumberofRowsShows);
        }

        /// <summary>
        /// Handles the Command event of the pager2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void pager2_Command(object sender, CommandEventArgs e)
        {
            Pager2.CurrentIndex = Convert.ToInt32(e.CommandArgument.ToString());
            BindComplaints(NumberofRowsShows);
        }

        protected void lstViewRequestStatus_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    string Status = (e.Item.FindControl("lblRequestStatus") as Label).Text;
                    HtmlImage img = (HtmlImage)e.Item.FindControl("RequestStatusImage");
                    if (Status == "Failed")
                    {
                        //Access the image tag
                        img.Src = this.Page.GetNewImagePath("Fail.svg");
                        Panel DivReason = (Panel)e.Item.FindControl("DivRequestReason");
                        DivReason.Visible = true;
                    }
                    else if (Status == "Resolved" || Status == "Approved")
                    {
                        img.Src = this.Page.GetNewImagePath("Success.svg");
                    }
                    else if (Status == "Pending")
                    {
                        img.Src = this.Page.GetNewImagePath("Pending.svg");
                    }
                    else if (Status == "In Process")
                    {
                        img.Src = this.Page.GetNewImagePath("Process.svg");
                    }
                    else if (Status == "Rejected")
                    {
                        img.Src = this.Page.GetNewImagePath("Reject.svg");
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void lstViewComplaintStatus_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    string Status = (e.Item.FindControl("lblComplaintStatus") as Label).Text;
                    HtmlImage img = (HtmlImage)e.Item.FindControl("ComplaintStatusImage");
                    if (Status == "Failed")
                    {
                        //Access the image tag
                        img.Src = this.Page.GetNewImagePath("Fail.svg");
                        Panel DivReason = (Panel)e.Item.FindControl("DivComplaintReason");
                        DivReason.Visible = true;
                    }
                    else if (Status == "Resolved")
                    {
                        img.Src = this.Page.GetNewImagePath("Success.svg");
                    }
                    else if (Status == "Pending")
                    {
                        img.Src = this.Page.GetNewImagePath("Pending.svg");
                    }
                    else if (Status == "In Process")
                    {
                        img.Src = this.Page.GetNewImagePath("Process.svg");
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnRequestViewMore_Click(object sender, EventArgs e)
        {
            NumberofRowsShows += 10;
            BindRequests(NumberofRowsShows);

        }

        protected void btnComplaintViewMore_Click(object sender, EventArgs e)
        {
            NumberofRowsShows += 10;
            BindComplaints(NumberofRowsShows);

        }
        #endregion

        #region Private methods

        /// <summary>
        /// Binds the requests.
        /// </summary>
        /// <remarks></remarks>
        private void BindRequests(int NumberofRowsShows)
        {
            btnComplaintViewMore.Visible = false;
            long cardholderID;
            int pRecordCount = 0;
            int SkipCount = (Pager1.CurrentIndex - 1) * Pager1.PageSize;
            this.Pager1.PageSize = NumberofRowsShows;
            cardholderID = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            CHRequestDetailManager crdm = new CHRequestDetailManager();
            lstViewRequestStatus.DataSource = lstRequestStatus = crdm.getCHRequestDetails(cardholderID, SkipCount, Pager1.PageSize, ref pRecordCount);
            RecordCount = pRecordCount;
            Pager1.Visible = RecordCount > Pager1.PageSize;
            lstViewRequestStatus.DataBind();
            lstViewRequestStatus.Visible = true;
            Pager2.Visible = false;
            lstViewComplaintStatus.Visible = false;
            if (lstRequestStatus != null && lstRequestStatus.Count > 0 && lstRequestStatus.Count >= NumberofRowsShows)
            {
                btnRequstViewMore.Visible = true;
            }
            else
            {
                btnRequstViewMore.Visible = false;
            }
            //if (RecordCount == 0)
            //{
            //    gridheader.Visible = false;
            //    lblheaderReqCompl.Text = Constants.RequestNotFound;
            //    lblheaderReqCompl.CssClass = "msgerror";
            //}
            //else
            //{
            //    gridheader.Visible = true;
            //    lblheaderReqCompl.Text = "";
            //}


        }

        /// <summary>
        /// Binds the complaints.
        /// </summary>
        /// <remarks></remarks>
        private void BindComplaints(int NumberofRowsShows)
        {
            btnRequstViewMore.Visible = false;
            long cardholderID;
            int pRecordCount = 0;
            int SkipCount = (Pager2.CurrentIndex - 1) * Pager2.PageSize;
            this.Pager1.PageSize = NumberofRowsShows;
            cardholderID = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            CardHolderComplaintManager crdm = new CardHolderComplaintManager();
            lstViewComplaintStatus.DataSource = lstComplaintStatus = crdm.getComplaintsDetails(cardholderID, SkipCount, Pager2.PageSize, ref pRecordCount);
            RecordCount = pRecordCount;
            Pager2.Visible = RecordCount > Pager2.PageSize;
            lstViewComplaintStatus.DataBind();
            lstViewComplaintStatus.Visible = true;
            Pager1.Visible = false;
            lstViewRequestStatus.Visible = false;
            if (lstComplaintStatus != null && lstComplaintStatus.Count > 0 && lstComplaintStatus.Count >= NumberofRowsShows)
            {
                btnComplaintViewMore.Visible = true;
            }
            else
            {
                btnComplaintViewMore.Visible = false;
            }
            //if (RecordCount == 0)
            //{
            //    gridheader.Visible = false;
            //    lblheaderReqCompl.Text = Constants.CompliantNotFound;
            //    lblheaderReqCompl.CssClass = "msgerror";
            //}
            //else
            //{
            //    gridheader.Visible = true;
            //    lblheaderReqCompl.Text = "";
            //}
        }

        /// <summary>
        /// Loads the credit cards.
        /// </summary>
        /// <remarks></remarks>
        private void loadCreditCards()
        {
            CH_CardDTO card = CardHolderManager.GetLoggedInUser().CH_Card;
            CardManager cm = new CardManager();
            string Cardnumber = card.card_number;
            card = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = card.card_number });
            if (card != null)
            {
                string firstName = UrlHelper.FirstCharToUpper(card.FIRST_NAME.ToLower());
                string lastName = UrlHelper.FirstCharToUpper(card.FAMILY_NAME.ToLower());
                lblCardHolder.Text = firstName + " " + lastName;
                //lblCardHolder.Text = card.FULL_NAME;                
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
        }



        #endregion
    }
}