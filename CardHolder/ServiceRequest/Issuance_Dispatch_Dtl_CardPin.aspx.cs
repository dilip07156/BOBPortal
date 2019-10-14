using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;


namespace CardHolder.ServiceRequest
{
    public partial class Issuance_Dispatch_Dtl_CardPin : PageBase
    {
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf) { }
            else
            {
                if (!IsPostBack)
                {
                    LoadDispatchTypes();
                    LoadCardsinDDL();
                    loadCreditCardsName();
                    gridheader.Visible = false;
                    GridCourier.Visible = false;
                }
                LblErrorMessage.Text = "";
               
            }
        }
        #endregion

        #region Events

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string RefrenceNo;
                int DispatchMonths;
                RefrenceNo = ViewState["BranchRefNumber"].ToString();
                if (RefrenceNo != "")
                {
                    if (ddlDispatchDtlOf.SelectedValue == "-1")
                    {
                        lblMessage.Text = Constants.SelectCardPIN;
                        DivMessage. Attributes.CssStyle.Add("display", "block");
                        gridheader.Visible = false;
                        GridCourier.Visible = false;
                    }
                    else
                    {
                        DispatchMonths = GetMonthRangeToShowDispatchReport();

                        BindDispatchSpeedPostDetails(RefrenceNo, ddlDispatchDtlOf.SelectedItem.Text, DispatchMonths);
                        BindDispatchCourierDetails(RefrenceNo, ddlDispatchDtlOf.SelectedItem.Text, DispatchMonths);
                    }
                }
                
            }
            catch (Exception ex)
            {
                LblErrorMessage.Text = Constants.GeneralErrorMessage;
                DivERROR.Attributes.CssStyle.Add("display", "block");
            }
        }

        protected void ddlcardlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreditCardsName();
        }

        #endregion

        #region Private methods

        //For Courier file

        private void BindDispatchCourierDetails(string RefrenceNo, string type, int DispatchMonths)
        {

            DispatchDetailManager ddm = new DispatchDetailManager();
            List<Dispatch_Courier_RptDTO> lstdispatchCourierDetailDTO = new List<Dispatch_Courier_RptDTO>();
            lstdispatchCourierDetailDTO = ddm.getDispatchCourierDetails(RefrenceNo, type, DispatchMonths);
            if (lstdispatchCourierDetailDTO.Count != 0)
            {
                gvCourierDtl.DataSource = lstdispatchCourierDetailDTO;
                gvCourierDtl.DataBind();
                GridCourier.Visible = true;              
                grdDIv.Visible = true;
            }
            else
            {
                gvCourierDtl.DataSource = null;
                gvCourierDtl.DataBind();
                GridCourier.Visible = false;
                lblMessage.Text = "No record found";
                DivMessage.Attributes.CssStyle.Add("display", "block");
            }
        }


        //For SpeedPost file

        private void BindDispatchSpeedPostDetails(string RefrenceNo, string type, int DispatchMonths)
        {
            DispatchDetailManager ddm = new DispatchDetailManager();
            List<Dispatch_SpeedPost_RptDTO> lstdispatchSpeedPostDetailDTO = new List<Dispatch_SpeedPost_RptDTO>();
            lstdispatchSpeedPostDetailDTO = ddm.getDispatchSpeedPostDetails(RefrenceNo, type, DispatchMonths);
            if (lstdispatchSpeedPostDetailDTO.Count != 0)
            {
                gvSpeedPostDtl.DataSource = lstdispatchSpeedPostDetailDTO;
                gvSpeedPostDtl.DataBind();
                gridheader.Visible = true;               
                grdDIv.Visible = true;
            }
            else
            {
                gvSpeedPostDtl.DataSource = null;
                gvSpeedPostDtl.DataBind();                
                gridheader.Visible = false;
               lblMessage.Text = "No record found";
                DivMessage.Attributes.CssStyle.Add("display", "block");
            }
        }


        private void LoadDispatchTypes()
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            List<DropDown_HdrDTO> list = dhm.SearchDllHeader("Dispatch_types").ToList();
            if (list.Count > 0)
            {
                ddlDispatchDtlOf.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlDispatchDtlOf.DataTextField = "Description";
                ddlDispatchDtlOf.DataValueField = "DropDown_Dtl_Id";
                ddlDispatchDtlOf.DataBind();
                ddlDispatchDtlOf.Items.Insert(0, new ListItem("---Select---", "-1"));
            }
            else
            {                
                lblMessage.Text = Constants.DispatchNotfound;
                DivMessage.Attributes.CssStyle.Add("display", "block");
            }
        }

        private void LoadCardsinDDL()
        {
            string CR_acc_num = CardHolderManager.GetLoggedInUser().creditcard_acc_number.Decrypt();
            CardManager cm = new CardManager();
            if (CR_acc_num != "")
            {
                ddlcardlist.DataSource = cm.GetAllCardsForATMPinReg(new CH_CardDTO() { Cr_Account_Nbr = CR_acc_num });
                ddlcardlist.DataTextField = "MASK_CARD_NUMBER";
                ddlcardlist.DataValueField = "card_number";
                ddlcardlist.DataBind();
               
            }
        }

        private void loadCreditCardsName()
        {
          
            string Card_number = ddlcardlist.SelectedValue;
            CardManager cm = new CardManager();
            CH_CardDTO chdto = new CH_CardDTO();
            if (Card_number != "")
            {
                chdto = cm.GetCHNameStatusbyCardNumber(new CH_CardDTO() { card_number = Card_number });
                if (chdto != null)
                {                   
                    string firstName = UrlHelper.FirstCharToUpper(chdto.FIRST_NAME.ToLower());
                    string lastName = UrlHelper.FirstCharToUpper(chdto.FAMILY_NAME.ToLower());
                    lblCardHolder.Text = firstName + " " + lastName;
                    ViewState["BranchRefNumber"] = chdto.BRANCH_REF_NUMBER;
                }

            }
        }

        private int GetMonthRangeToShowDispatchReport()
        {
            int noOfMonths = 3;
            ParameterManager pm = new ParameterManager();
            Parameter_MstDTO obj = pm.GetParameterByName("DispatchDetailMonths");
            if (obj != null && obj.Parameter_ValueN != null)
                noOfMonths = Convert.ToInt32(obj.Parameter_ValueN);
            return noOfMonths;
        }


        #endregion
    }
}