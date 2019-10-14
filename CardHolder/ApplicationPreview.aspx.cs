using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System.Configuration;

namespace CardHolder
{
    public partial class ApplicationPreview : System.Web.UI.Page
    {
        readonly string _expiryYear = ConfigurationManager.AppSettings["ExpiryYear"];
        string qsk = "afq4dfrs";

        public string ApplicationNum
        {
            /// Gets record count
            get { return ViewState["ApplicationNum"] == null ? "" : Convert.ToString(ViewState["ApplicationNum"]); }

            /// Sets record count
            set { ViewState["ApplicationNum"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationNum = GetApplicationNum();

            if (!string.IsNullOrEmpty(ApplicationNum))
                Onload();
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "Closewindow", "window.close();", true);
        }


        private string GetApplicationNum()
        {
            ApplicationNum = "";
            try
            {
                string strReq = "";
                strReq = Request.Form["txtPostData"];
                if (!strReq.Equals(""))
                    strReq = EncryptDecryptQueryString.Decrypt(strReq, qsk);
                string[] arrMsgs = strReq.Split('&');
                string[] arrIndMsg;
                arrIndMsg = arrMsgs[0].Split('='); //Get the Details
                ApplicationNum = Convert.ToString(arrIndMsg[1]);
                return ApplicationNum;
            }
            catch (Exception)
            {
                return ApplicationNum;
            }


        }

        private void GetApplicationDetails()
        {

            if (!string.IsNullOrEmpty(ApplicationNum))
            {
                //string appnum = Convert.ToString(Session["AppHashNum"]).Decrypt();
                Application_DTO objdto = ApplicationManager.FindRecord(ApplicationNum);

                if (objdto != null)
                {
                    lblSOURCE_APPLICATION_NO.Text = objdto.SOURCE_APPLICATION_NO;
                    hdnId.Value = Convert.ToString(objdto.ID);
                    ddlBranchlist.SelectedValue = !string.IsNullOrEmpty(objdto.SOURCE_CODE) ? objdto.SOURCE_CODE : "-1";
                    lblSelbranch.Text = ddlBranchlist.SelectedItem.Text;
                    ddlBranchlist.Visible = false;
                    if (!string.IsNullOrEmpty(objdto.PRODUCT_CODE))
                        lblAppliedCard.Text = GetAppliedCardName(objdto.PRODUCT_CODE);

                    ddlVIPCode.SelectedValue = Convert.ToString(objdto.VIP_CODE);
                    ddlSocialStatus.SelectedValue = objdto.SOCIAL_STATUS;
                    lblStaff_EC_No.Text = string.IsNullOrEmpty(objdto.STAFF_E_C_NO) ? "" : objdto.STAFF_E_C_NO.ToUpper();
                    lblSTAFF_NAME.Text = string.IsNullOrEmpty(objdto.STAFF_NAME) ? "" : objdto.STAFF_NAME.ToUpper();
                    if (ddlRecommendedBy.Items.FindByValue(objdto.RECOMMENDED_BY) != null)
                    {
                        ddlRecommendedBy.SelectedValue = string.IsNullOrEmpty(objdto.RECOMMENDED_BY) ? "0" : objdto.RECOMMENDED_BY;
                    }
                    else
                    {
                        ddlRecommendedBy.SelectedValue = "0";
                    }
                    lblRecommendedBy.Text = string.IsNullOrEmpty(ddlRecommendedBy.SelectedItem.Text) ? "" : ddlRecommendedBy.SelectedItem.Text == "Select" ? "" : ddlRecommendedBy.SelectedItem.Text;
                    ddlRecommendedBy.Visible = false;
                    ddlRecommendedBranch.SelectedValue = string.IsNullOrEmpty(objdto.RECOMMENDED_BRANCH) ? "-1" : objdto.RECOMMENDED_BRANCH;
                    lblRecommendedBranch.Text = string.IsNullOrEmpty(ddlRecommendedBranch.SelectedItem.Text) ? "" : ddlRecommendedBranch.SelectedItem.Text;
                    ddlRecommendedBranch.Visible = false;
                    chkOffice.Checked = objdto.PREFERRED_MAILING_ADDRESS == "1" ? true : false;
                    chkResi.Checked = objdto.PREFERRED_MAILING_ADDRESS == "0" ? true : false;
                    //----------------BASIC_CARD_FLAG = "Need Clarifi
                    ddlTitle.SelectedValue = Convert.ToString(objdto.TITLE);
                    lblFullname.Text = ddlTitle.SelectedItem.Text.Replace("--", "") + " " + objdto.FIRST_NAME + " " + objdto.MIDDLE_NAME + " " + objdto.FAMILY_NAME;
                    ddlTitle.Visible = false;
                    //FIRST_NAME.Value = objdto.FIRST_NAME;
                    //MIDDLE_NAME.Value = objdto.MIDDLE_NAME;
                    //FAMILY_NAME.Value = objdto.FAMILY_NAME;
                    EMBOSSED_NAME.Value = objdto.EMBOSSED_NAME;

                    if (!string.IsNullOrEmpty(objdto.GENDER) && objdto.GENDER.ToLower() == "m")
                        MALE.Checked = true;
                    else if (!string.IsNullOrEmpty(objdto.GENDER) && objdto.GENDER.ToLower() == "f")
                        FEMALE.Checked = true;

                    BIRTH_DATE.Value = GeneralMethods.FormatDate(objdto.BIRTH_DATE);
                    AGE.Value = Convert.ToString(objdto.AGE);
                    ddlMaritalStatus.SelectedValue = Convert.ToString(objdto.MARITAL_STATUS);
                    lblMarriageDay.Text = objdto.DOM;
                    //ddlMarriageMonth.Value = objdto.DOM.Substring(2,3) ;
                    NO_OF_DEPENDENTS.Value = Convert.ToString(objdto.NO_OF_DEPENDENTS);

                    if (objdto.NATIONALITY == "000")
                        NonResidentIndian.Checked = true;
                    else
                        Residentindian.Checked = true;

                    //Residentindian.Checked = Convert.ToBoolean(Convert.ToInt32(objdto.NATIONALITY));
                    //NonResidentIndian.Checked = Convert.ToBoolean(Convert.ToInt32(objdto.NATIONALITY));
                    CURR_ADDRESS1.Value = objdto.CURR_ADDRESS1;
                    CURR_ADDRESS2.Value = objdto.CURR_ADDRESS2;
                    CURR_ADDRESS3.Value = objdto.CURR_ADDRESS3;
                    CURR_ADDRESS4.Value = objdto.CURR_ADDRESS4;
                    ddlCurrCountry.SelectedValue = Convert.ToString(objdto.CURR_COUNTRY_CODE);
                    ddlCurrCity.SelectedValue = Convert.ToString(objdto.CURR_CITY_CODE);
                    CURR_POSTAL_CODE.Value = objdto.CURR_POSTAL_CODE;
                    MOBILE_NUMBER.Value = objdto.MOBILE_NUMBER;
                    EMAIL_ID.Value = objdto.EMAIL_ID;
                    HOME_PHONE_NUMBER.Value = objdto.HOME_PHONE_NUMBER;
                    ddlchangeResi.SelectedValue = objdto.RESIDANCE_CHANGED;
                    radResiSatus.SelectedValue = objdto.RESIDENCE_STATUS != null ? Convert.ToString(objdto.RESIDENCE_STATUS) : null;
                    PERM_ADDRESS1.Value = objdto.PERM_ADDRESS1;
                    PERM_ADDRESS2.Value = objdto.PERM_ADDRESS2;
                    PERM_ADDRESS3.Value = objdto.PERM_ADDRESS3;
                    PERM_ADDRESS4.Value = objdto.PERM_ADDRESS4;
                    ddlPermCountry.SelectedValue = objdto.PERM_COUNTRY_CODE != null ? Convert.ToString(objdto.PERM_COUNTRY_CODE) : "-1";
                    ddlPermCity.SelectedValue = objdto.PERM_CITY_CODE != null ? Convert.ToString(objdto.PERM_CITY_CODE) : "-1";
                    PERM_POSTAL_CODE.Value = objdto.PERM_POSTAL_CODE;
                    DRIVING_LICENSE_NUMBER.Value = objdto.DRIVING_LICENSE_NUMBER;
                    PASSPORT_NUMBER.Value = objdto.PASSPORT_NUMBER;


                    ddlEducation.SelectedValue = objdto.EDUCATION != null ? Convert.ToString(objdto.EDUCATION) : "-1";
                    UNIVERSITY.Value = objdto.UNIVERSITY;
                    FATHER_NAME.Value = objdto.FATHER_NAME;
                    MAIDEN_NAME.Value = objdto.MAIDEN_NAME;
                    SPOUSE_NAME.Value = objdto.SPOUSE_NAME;
                    SPOUSE_MOB_NO.Value = objdto.SPOUSE_MOB_NO;

                    chkOwnedVehicle.SelectedValue = Convert.ToString(objdto.OWNED_VEHICLE_TYPE);
                    chkEmpStatus.SelectedValue = Convert.ToString(objdto.EMPLOYMENT_STATUS);
                    ddlempType.SelectedValue = objdto.COMP_TYPE != null ? Convert.ToString(objdto.COMP_TYPE) : "-1";
                    EMPLOYER_NAME.Value = objdto.EMPLOYER_NAME;
                    chkdesignation.SelectedValue = Convert.ToString(objdto.EMPL_DESIGNATION);
                    chkEmpProfession.SelectedValue = Convert.ToString(objdto.APPLICANT_PROF);
                    EMPL_DEPARTMENT.Value = objdto.EMPL_DEPARTMENT;
                    EMPL_ADDRESS1.Value = objdto.EMPL_ADDRESS1;
                    EMPL_ADDRESS2.Value = objdto.EMPL_ADDRESS2;
                    EMPL_ADDRESS3.Value = objdto.EMPL_ADDRESS3;
                    EMPL_ADDRESS4.Value = objdto.EMPL_ADDRESS4;
                    ddlEmpCity.SelectedValue = objdto.EMPL_CITY_CODE != null ? Convert.ToString(objdto.EMPL_CITY_CODE) : "-1";
                    ddlEmpCountry.SelectedValue = objdto.EMPL_COUNTRY_CODE != null ? Convert.ToString(objdto.EMPL_COUNTRY_CODE) : "-1";
                    EMPL_POSTAL_CODE.Value = objdto.EMPL_POSTAL_CODE;
                    OFFICE_PHONE_NUMBER.Value = objdto.OFFICE_PHONE_NUMBER;
                    int totalJobmonths = Convert.ToInt32(objdto.CURRENT_JOB_TENURE);
                    var Jobyears = totalJobmonths / 12;
                    var Jobmonths = totalJobmonths % 12;
                    JOB_MONTHS.Value = Convert.ToString(Jobmonths);
                    CURRENT_JOB_TENURE.Value = Convert.ToString(Jobyears);
                    //CURRENT_JOB_TENURE.Value = Convert.ToString(objdto.CURRENT_JOB_TENURE);
                    ANNUAL_INCOME_CODE.Value = Convert.ToString(objdto.ANNUAL_INCOME_CODE);
                    EMPL_ID.Value = Convert.ToString(objdto.EMPL_ID);
                    OTHER_INCOME.Value = Convert.ToString(objdto.OTHER_INCOME);
                    SPOUSE_INCOME.Value = Convert.ToString(objdto.SPOUSE_INCOME);

                    radIncomePerMonth.SelectedValue = Convert.ToString(objdto.INCOME_PER_MONTH);
                    CUSTOMER_ID.Value = objdto.CUSTOMER_ID;
                    PAN_GIR_NO.Value = objdto.PAN_GIR_NO;
                    TAX_PAID.Value = objdto.TAX_PAID;
                    YEAR_TAX_PAID.Value = objdto.YEAR_TAX_PAID;
                    ddlIsAccountWithbank.SelectedValue = objdto.ACC_IN_BANK != null ? Convert.ToString(objdto.ACC_IN_BANK) : "-1";
                    ddlAccountBranch.SelectedValue = objdto.ACCOUNT_BRANCH != null ? Convert.ToString(objdto.ACCOUNT_BRANCH) : "-1";
                    OTHER_CITY.Value = objdto.OTHER_CITY;
                    OUR_ACCOUNT_TENURE.Value = Convert.ToString(objdto.OUR_ACCOUNT_TENURE);
                    if (objdto.OUR_ACCOUNT_TYPE == "0")
                        saving_acc.Checked = true;
                    else if (objdto.OUR_ACCOUNT_TYPE == "1")
                        other_acc.Checked = true;
                    else if (objdto.OUR_ACCOUNT_TYPE == "2")
                        current_acc.Checked = true;
                    ACCOUNT_NUMBER.Value = objdto.ACCOUNT_NUMBER;


                    HOUSING_LOAN.Checked = !string.IsNullOrEmpty(objdto.HOUSING_LOAN) && Convert.ToBoolean(Convert.ToInt32(objdto.HOUSING_LOAN));
                    CAR_LOAN.Checked = !string.IsNullOrEmpty(objdto.CAR_LOAN) && Convert.ToBoolean(Convert.ToInt32(objdto.CAR_LOAN));
                    CONSUMER_LOAN.Checked = !string.IsNullOrEmpty(objdto.CONSUMER_LOAN) && Convert.ToBoolean(Convert.ToInt32(objdto.CONSUMER_LOAN));
                    BUSINESS_LOAN.Checked = !string.IsNullOrEmpty(objdto.BUSINESS_LOAN) && Convert.ToBoolean(Convert.ToInt32(objdto.BUSINESS_LOAN));
                    OTHR_LOAN.Checked = !string.IsNullOrEmpty(objdto.OTHR_LOAN) && Convert.ToBoolean(Convert.ToInt32(objdto.OTHR_LOAN));
                    OTHER_LOAN.Value = objdto.OTHER_LOAN;
                    LOAN_AMOUNT.Value = Convert.ToString(objdto.LOAN_AMOUNT);
                    CURRENT_OUTSTANDING.Value = Convert.ToString(objdto.CURRENT_OUTSTANDING);
                    DURATION_OF_LOAN.Value = objdto.DURATION_OF_LOAN;
                    LOAN_INSTUTION_NAME.Value = objdto.LOAN_INSTUTION_NAME;
                    LOAN_BRANCH.Value = objdto.LOAN_BRANCH;
                    BOB_DEBIT_CARD_NO.Value = objdto.BOB_DEBIT_CARD_NO;
                    DC_VALID_UPTO.Text = objdto.DC_VALID_UPTO;
                    CC_BANK_NAME1.Value = objdto.CC_BANK_NAME1;
                    CC_NO1.Value = objdto.CC_NO1;
                    CC_VALID_UPTO.Text = objdto.CC_VALID_UPTO1;
                    CC_CR_LITMIT1.Value = Convert.ToString(objdto.CC_CR_LITMIT1);
                    CC_BANK_NAME2.Value = objdto.CC_BANK_NAME2;
                    CC_NO2.Value = objdto.CC_NO2;
                    CC_VALID_UPTO2.Text = objdto.CC_VALID_UPTO2;
                    CC_CR_LITMIT2.Value = Convert.ToString(objdto.CC_CR_LITMIT2);
                    CC_BANK_NAME3.Value = objdto.CC_BANK_NAME3;
                    CC_NO3.Value = objdto.CC_NO3;
                    CC_VALID_UPTO3.Text = objdto.CC_VALID_UPTO3;
                    CC_CR_LITMIT2.Value = Convert.ToString(objdto.CC_CR_LITMIT2);
                    CC_CR_LITMIT3.Value = Convert.ToString(objdto.CC_CR_LITMIT3);

                    ADDITIONAL_CARD_NAME.Value = objdto.ADDITIONAL_CARD_NAME;

                    radAddonRelation.SelectedValue = Convert.ToString(objdto.ADDITIONAL_CARD_REL);
                    radAddGender.SelectedValue = Convert.ToString(objdto.ADDITIONAL_GENDER);
                    SEC_BIRTH_DATE.Value = GeneralMethods.FormatDate(objdto.SEC_BIRTH_DATE);

                    SEC1_APPLICANT_PROF.SelectedValue = objdto.SEC1_APPLICANT_PROF == null ? "-1" : Convert.ToString(objdto.SEC1_APPLICANT_PROF);
                    lblSEC1_APPLICANT_PROF.Text = string.IsNullOrEmpty(SEC1_APPLICANT_PROF.SelectedItem.Text) ? "" : SEC1_APPLICANT_PROF.SelectedItem.Text == "Select" ? "" : SEC1_APPLICANT_PROF.SelectedItem.Text;
                    SEC1_APPLICANT_PROF.Visible = false;

                    SEC2_APPLICANT_PROF.SelectedValue = objdto.SEC2_APPLICANT_PROF == null ? "-1" : Convert.ToString(objdto.SEC2_APPLICANT_PROF);
                    lblSEC1_APPLICANT_PROF.Text = string.IsNullOrEmpty(SEC2_APPLICANT_PROF.SelectedItem.Text) ? "" : SEC2_APPLICANT_PROF.SelectedItem.Text == "Select" ? "" : SEC1_APPLICANT_PROF.SelectedItem.Text;
                    SEC2_APPLICANT_PROF.Visible = false;

                    REF1_NAME.Value = objdto.REF1_NAME;
                    REF1_ADDRESS1.Value = objdto.REF1_ADDRESS1;
                    REF1_ADDRESS2.Value = objdto.REF1_ADDRESS2;
                    REF1_ADDRESS3.Value = objdto.REF1_ADDRESS3;
                    REF1_ADDRESS4.Value = objdto.REF1_ADDRESS4;
                    ddlRelcountry.SelectedValue = Convert.ToString(objdto.REF1_COUNTRY_CODE);
                    ddlRelcity.SelectedValue = Convert.ToString(objdto.REF1_CITY_CODE);
                    REF1_ZIP_CODE.Value = objdto.REF1_ZIP_CODE;
                    REF1_PHONE_NUMBER.Value = objdto.REF1_PHONE_NUMBER;


                    REF2_NAME.Value = objdto.REF2_NAME;
                    REF2_ADDRESS1.Value = objdto.REF2_ADDRESS1;
                    REF2_ADDRESS2.Value = objdto.REF2_ADDRESS2;
                    REF2_ADDRESS3.Value = objdto.REF2_ADDRESS3;
                    REF2_ADDRESS4.Value = objdto.REF2_ADDRESS4;
                    ddlRel2Country.SelectedValue = objdto.REF2_COUNTRY_CODE != null ? Convert.ToString(objdto.REF2_COUNTRY_CODE) : "-1";
                    ddlrel2city.SelectedValue = objdto.REF2_CITY_CODE != null ? Convert.ToString(objdto.REF2_CITY_CODE) : "-1";
                    REF2_ZIP_CODE.Value = objdto.REF2_ZIP_CODE;
                    REF2_PHONE_NUMBER.Value = objdto.REF2_PHONE_NUMBER;
                    INSURANCE_NOM_NAME.Value = objdto.INSURANCE_NOM_NAME;
                    ddlNomiRelation.SelectedItem.Text = objdto.INSURANCE_NOM_REL == null ? Constants.DDLNomineeRel : Convert.ToString(objdto.INSURANCE_NOM_REL);

                    lblBobCardType.Text = lblAppliedCard.Text;
                    DIRECT_DEBIT_ACCOUNT_NUMBER.Text = string.IsNullOrEmpty(objdto.DIRECT_DEBIT_ACCOUNT_NUMBER) ? "_______" : objdto.DIRECT_DEBIT_ACCOUNT_NUMBER;
                    DIRECT_DEBIT_ACCOUNT_NAME.Text = string.IsNullOrEmpty(objdto.DIRECT_DEBIT_ACCOUNT_NAME) ? "_____________" : objdto.DIRECT_DEBIT_ACCOUNT_NAME;
                    lblDebitAccountType.Text = string.IsNullOrEmpty(objdto.DIRECT_DEBIT_ACCOUNT_TYPE) ? "_______" : objdto.DIRECT_DEBIT_ACCOUNT_TYPE;
                    ddlDebitAccountType.Visible = false;
                    ddlDebitBranchList.SelectedValue = string.IsNullOrEmpty(objdto.DIRECT_DEBIT_BRANCH) ? "-1" : objdto.DIRECT_DEBIT_BRANCH;
                    lblDebitBranchList.Text = string.IsNullOrEmpty(ddlDebitBranchList.SelectedItem.Text) ? "________" : ddlDebitBranchList.SelectedItem.Text == "Select" ? "________" : ddlDebitBranchList.SelectedItem.Text;
                    ddlDebitBranchList.Visible = false;
                    chkautodebit.Checked = !string.IsNullOrEmpty(objdto.DIRECT_DEBIT_FLAG) && Convert.ToBoolean(Convert.ToInt32(objdto.DIRECT_DEBIT_FLAG));
                    lblVC_ALIAS_NAME.Text = Convert.ToString(objdto.VC_ALIAS_NAME);
                    rbTotalAmountDue.Checked = objdto.DIRECT_DEBIT_AMOUNT_FLAG == 0 ? true : false;
                    rbMinimumAmountDue.Checked = objdto.DIRECT_DEBIT_AMOUNT_FLAG == 1 ? true : false;
                    rbPercentage.Checked = objdto.DIRECT_DEBIT_AMOUNT_FLAG == 2 ? true : false;
                    txtPercentage.Text = objdto.DIRECT_DEBIT_PERCENTAGE == null ? "" : Convert.ToString(objdto.DIRECT_DEBIT_PERCENTAGE);
                    ddlApplicationType.SelectedValue = string.IsNullOrEmpty(objdto.APPLICATION_TYPE) ? "-1" : objdto.APPLICATION_TYPE;
                    ddlPromoCode.SelectedValue = string.IsNullOrEmpty(objdto.PROMO_CODE) ? "-1" : objdto.PROMO_CODE;

                    btnCloseWindow.Visible = true;
                    btnPrint.Visible = true;
                }
                else
                {
                    lblempty.Text = "<h3>No data found. Please try again</h3>";
                    btnCloseWindow.Visible = true;
                    btnPrint.Visible = false;
                }
            }

        }

        private static string GetAppliedCardName(string productCode)
        {
            string cardname = string.Empty;

            if (productCode == Constants.Silvervisa)
                cardname = Constants.P01;

            else if (productCode == Constants.Exclusivegeneralmaster)
                cardname = Constants.P02;

            else if (productCode == Constants.Exclusiveyouthmaster)
                cardname = Constants.P03;

            else if (productCode == Constants.Exclusivewomenmaster)
                cardname = Constants.P04;

            else if (productCode == Constants.Goldvisa)
                cardname = Constants.P05;

            else if (productCode == Constants.Goldinternationalvisa)
                cardname = Constants.P06;

            else if (productCode == Constants.Goldmastercard)
                cardname = Constants.P07;

            else if (productCode == Constants.Platinummaster)
                cardname = Constants.P21;

            else if (productCode == Constants.Platinumvisa)
                cardname = Constants.P22;

            else if (productCode == Constants.Elite)
                cardname = Constants.P23;

            else if (productCode == Constants.Corporatepremium)
                cardname = Constants.P24;

            else if (productCode == Constants.Platinumselect)
                cardname = Constants.P25;

            else if (productCode == Constants.Visasignature)
                cardname = Constants.P26;

            else if (productCode == Constants.Platinumbba)
                cardname = Constants.P27;

            else if (productCode == Constants.Bobcarassure)
                cardname = Constants.P28;

            else if (productCode == Constants.Titanium)
                cardname = Constants.P29;

            return cardname;

        }


        private void Onload()
        {
            //MonthYear();
            //GetBranchlist();
            //GetCountryList();
            //GetCityList();
            //var lstsyscodeDto = new List<SYSCodeDTO>();
            //BindSYSCodeList(ref lstsyscodeDto);

            //GetViPlist(lstsyscodeDto);
            //GetSocialStatusList(lstsyscodeDto);
            //GetTittleFromSysCode(lstsyscodeDto);
            //GetMaritialStatusFromSysCode(lstsyscodeDto);
            //GetResidenceChangedList(lstsyscodeDto);
            //BindResidenceStatus(lstsyscodeDto);
            //BindEducationDetails(lstsyscodeDto);
            //BindEmployementStatus(lstsyscodeDto);
            //BindEmployeeType(lstsyscodeDto);
            //BindApplicantProfession(lstsyscodeDto);
            //BindApplicantDesignation(lstsyscodeDto);
            //BindOwnedVehicle(lstsyscodeDto);
            //BindIncomePerMonth(lstsyscodeDto);
            //GetBankListSysCode(lstsyscodeDto);
            //BindRelation(lstsyscodeDto);
            //GetNomiRelListSysCode(lstsyscodeDto);


            MonthYear();
            GetBranchlist();
            GetCityList();
            GetCountryList();
            List<SYSCodeDTO> lstsyscodeDTO = new List<SYSCodeDTO>();
            BindSYSCodeList(ref lstsyscodeDTO);
            BindApplicationType();
            BindPromoCode();
            GetVIPlist(lstsyscodeDTO);
            GetSocialStatusList(lstsyscodeDTO);
            GetTittleFromSysCode(lstsyscodeDTO);
            GetMaritialStatusFromSysCode(lstsyscodeDTO);
            GetResidenceChangedList(lstsyscodeDTO);
            BindResidenceStatus(lstsyscodeDTO);
            BindEducationDetails(lstsyscodeDTO);
            BindEmployementStatus(lstsyscodeDTO);
            BindEmployeeType(lstsyscodeDTO);
            BindApplicantProfession(lstsyscodeDTO);
            BindApplicantDesignation(lstsyscodeDTO);
            BindOwnedVehicle(lstsyscodeDTO);
            BindIncomePerMonth(lstsyscodeDTO);
            GetBankListSysCode(lstsyscodeDTO);
            GetAccountTypeSysCode(lstsyscodeDTO);
            BindRelation(lstsyscodeDTO);
            GetNomiRelListSysCode(lstsyscodeDTO);
            GetApplicationDetails();
        }

        private void GetAccountTypeSysCode(List<SYSCodeDTO> lstsyscodeDTO)
        {
            ddlDebitAccountType.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlDebitAccountType.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.ACH_ACCOUNT_TYPE.ToLower()).ToList();
                ddlDebitAccountType.DataTextField = "SHORT_NAME";
                ddlDebitAccountType.DataValueField = "SHORT_NAME";
                ddlDebitAccountType.DataBind();
            }
            ddlDebitAccountType.Items.Insert(0, new ListItem(Constants.DDLResidance, "-1"));
        }

        private void GetVIPlist(List<SYSCodeDTO> lstsyscodeDTO)
        {
            ddlVIPCode.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlVIPCode.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.VIP_CODE.ToLower()).ToList();
                ddlVIPCode.DataTextField = "SHORT_NAME";
                ddlVIPCode.DataValueField = "CODE";
                ddlVIPCode.DataBind();
            }
            ddlVIPCode.Items.Insert(0, new ListItem(Constants.DDLVIP, "-1"));

        }


        private void BindApplicationType()
        {
            var cdmgr = new CardManager();
            var objSysCodeDTO = new List<SYSCodeDTO>();

            objSysCodeDTO = cdmgr.GetListOfApplicationType();
            ddlApplicationType.DataSource = objSysCodeDTO;
            ddlApplicationType.DataTextField = "SHORT_NAME";
            ddlApplicationType.DataValueField = "SHORT_NAME";
            ddlApplicationType.DataBind();
            ddlApplicationType.Items.Insert(0, new ListItem("--Select--", "-1"));
        }

        private void BindPromoCode()
        {
            var objCardMgr = new CardManager();
            var objSysCodeDTO = new List<SYSCodeDTO>();

            objSysCodeDTO = objCardMgr.GetPromoCode();
            ddlPromoCode.DataSource = objSysCodeDTO;
            ddlPromoCode.DataTextField = "DESCRIPTION";
            ddlPromoCode.DataValueField = "PROMO_CODE";
            ddlPromoCode.DataBind();
            ddlPromoCode.Items.Insert(0, new ListItem("--Select--", "-1"));
        }

        private void GetBranchlist()
        {
            var cdmgr = new CardManager();
            var lstbankDtO = cdmgr.GetBranchList();

            if (lstbankDtO != null && lstbankDtO.Count > 0)
            {
                ddlBranchlist.DataSource = lstbankDtO;
                ddlBranchlist.DataTextField = "BRANCH_NAME";
                ddlBranchlist.DataValueField = "BRANCH_CODE";
                ddlBranchlist.DataBind();

                ddlRecommendedBranch.DataSource = lstbankDtO;
                ddlRecommendedBranch.DataTextField = "BRANCH_NAME";
                ddlRecommendedBranch.DataValueField = "BRANCH_CODE";
                ddlRecommendedBranch.DataBind();

                ddlDebitBranchList.DataSource = lstbankDtO;
                ddlDebitBranchList.DataTextField = "BRANCH_NAME";
                ddlDebitBranchList.DataValueField = "BRANCH_CODE";
                ddlDebitBranchList.DataBind();

                ddlAccountBranch.DataSource = lstbankDtO;
                ddlAccountBranch.DataTextField = "BRANCH_NAME";
                ddlAccountBranch.DataValueField = "BRANCH_CODE";
                ddlAccountBranch.DataBind();
            }
            ddlBranchlist.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));
            ddlRecommendedBranch.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));
            ddlDebitBranchList.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));
            ddlAccountBranch.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));

        }

        private void GetCountryList()
        {
            var cdmgr = new CardManager();
            ddlCurrCountry.Items.Clear();
            ddlPermCountry.Items.Clear();
            ddlEmpCountry.Items.Clear();
            var lstcountry = cdmgr.GetListOfCountry();

            if (lstcountry != null && lstcountry.Count > 0)
            {
                ddlCurrCountry.DataSource = lstcountry;
                ddlCurrCountry.DataTextField = "COUNTRY_NAME";
                ddlCurrCountry.DataValueField = "COUNTRY_CODE";
                ddlCurrCountry.DataBind();

                ddlPermCountry.DataSource = lstcountry;
                ddlPermCountry.DataTextField = "COUNTRY_NAME";
                ddlPermCountry.DataValueField = "COUNTRY_CODE";
                ddlPermCountry.DataBind();

                ddlEmpCountry.DataSource = lstcountry;
                ddlEmpCountry.DataTextField = "COUNTRY_NAME";
                ddlEmpCountry.DataValueField = "COUNTRY_CODE";
                ddlEmpCountry.DataBind();


                ddlRelcountry.DataSource = lstcountry;
                ddlRelcountry.DataTextField = "COUNTRY_NAME";
                ddlRelcountry.DataValueField = "COUNTRY_CODE";
                ddlRelcountry.DataBind();

                ddlRel2Country.DataSource = lstcountry;
                ddlRel2Country.DataTextField = "COUNTRY_NAME";
                ddlRel2Country.DataValueField = "COUNTRY_CODE";
                ddlRel2Country.DataBind();

            }
            ddlCurrCountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            ddlPermCountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            ddlEmpCountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            ddlRelcountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            ddlRel2Country.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
        }

        private void GetCityList()
        {
            CardManager cdmgr = new CardManager();
            var Citylist = cdmgr.GetListOfCity().ToList();

            if (Citylist != null && Citylist.Count > 0)
            {
                ddlCurrCity.DataSource = Citylist;
                ddlCurrCity.DataTextField = "CITY_NAME";
                ddlCurrCity.DataValueField = "CITY_CODE";
                ddlCurrCity.DataBind();

                ddlPermCity.DataSource = Citylist;
                ddlPermCity.DataTextField = "CITY_NAME";
                ddlPermCity.DataValueField = "CITY_CODE";
                ddlPermCity.DataBind();


                ddlEmpCity.DataSource = Citylist;
                ddlEmpCity.DataTextField = "CITY_NAME";
                ddlEmpCity.DataValueField = "CITY_CODE";
                ddlEmpCity.DataBind();

                ddlRelcity.DataSource = Citylist;
                ddlRelcity.DataTextField = "CITY_NAME";
                ddlRelcity.DataValueField = "CITY_CODE";
                ddlRelcity.DataBind();

                ddlrel2city.DataSource = Citylist;
                ddlrel2city.DataTextField = "CITY_NAME";
                ddlrel2city.DataValueField = "CITY_CODE";
                ddlrel2city.DataBind();
            }



        }


        private void BindSYSCodeList(ref List<SYSCodeDTO> lstsyscodeDto)
        {
            var cdmgr = new CardManager();
            lstsyscodeDto = cdmgr.GetListOfSyscode();
        }

        private void GetViPlist(List<SYSCodeDTO> lstsyscodeDto)
        {
            ddlVIPCode.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlVIPCode.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.VIP_CODE.ToLower()).ToList();
                ddlVIPCode.DataTextField = "SHORT_NAME";
                ddlVIPCode.DataValueField = "CODE";
                ddlVIPCode.DataBind();
            }
            ddlVIPCode.Items.Insert(0, new ListItem(Constants.DDLVIP, "-1"));

        }

        private void GetSocialStatusList(List<SYSCodeDTO> lstsyscodeDto)
        {
            ddlSocialStatus.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlSocialStatus.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.SOCIAL_STATUS.ToLower()).ToList();
                ddlSocialStatus.DataTextField = "SHORT_NAME";
                ddlSocialStatus.DataValueField = "CODE";
                ddlSocialStatus.DataBind();
            }
            ddlSocialStatus.Items.Insert(0, new ListItem(Constants.DDLSocialStatus, "-1"));
        }

        private void GetTittleFromSysCode(List<SYSCodeDTO> lstsyscodeDto)
        {
            ddlTitle.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlTitle.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.TITLE.ToLower()).ToList();
                ddlTitle.DataTextField = "SHORT_NAME";
                ddlTitle.DataValueField = "CODE";
                ddlTitle.DataBind();
            }
            ddlTitle.Items.Insert(0, new ListItem(Constants.DDLtitle, "-1"));
        }

        private void GetMaritialStatusFromSysCode(List<SYSCodeDTO> lstsyscodeDto)
        {
            ddlMaritalStatus.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlMaritalStatus.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.MARITAL_STATUS.ToLower()).ToList();
                ddlMaritalStatus.DataTextField = "SHORT_NAME";
                ddlMaritalStatus.DataValueField = "CODE";
                ddlMaritalStatus.DataBind();
            }
            ddlMaritalStatus.Items.Insert(0, new ListItem(Constants.DDLMaritalStatus, "-1"));
        }

        private void GetResidenceChangedList(List<SYSCodeDTO> lstsyscodeDto)
        {
            ddlchangeResi.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlchangeResi.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.RESIDANCE_CHANGED.ToLower()).ToList();
                ddlchangeResi.DataTextField = "SHORT_NAME";
                ddlchangeResi.DataValueField = "CODE";
                ddlchangeResi.DataBind();
            }

            ddlchangeResi.Items.Insert(0, new ListItem(Constants.DDLResidance, "-1"));
        }

        private void BindResidenceStatus(List<SYSCodeDTO> lstsyscodeDto)
        {
            radResiSatus.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                radResiSatus.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.RESIDENCE_STATUS.ToLower()).ToList();
                radResiSatus.DataTextField = "SHORT_NAME";
                radResiSatus.DataValueField = "CODE";
                radResiSatus.DataBind();
            }
        }

        private void BindEducationDetails(List<SYSCodeDTO> lstsyscodeDto)
        {
            ddlEducation.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlEducation.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.EDUCATION.ToLower()).ToList();
                ddlEducation.DataTextField = "SHORT_NAME";
                ddlEducation.DataValueField = "CODE";
                ddlEducation.DataBind();
            }
            ddlEducation.Items.Insert(0, new ListItem(Constants.DDLEducation, "-1"));
        }

        private void BindEmployementStatus(List<SYSCodeDTO> lstsyscodeDto)
        {
            //EMPLOYMENT_STATUS
            chkEmpStatus.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                chkEmpStatus.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.EMPLOYMENT_STATUS.ToLower()).ToList();
                chkEmpStatus.DataTextField = "SHORT_NAME";
                chkEmpStatus.DataValueField = "CODE";
                chkEmpStatus.DataBind();
            }


        }

        private void BindEmployeeType(List<SYSCodeDTO> lstsyscodeDto)
        {
            //COMP_TYPE
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlempType.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.COMP_TYPE.ToLower()).ToList();
                ddlempType.DataTextField = "SHORT_NAME";
                ddlempType.DataValueField = "CODE";
                ddlempType.DataBind();
            }
            ddlempType.Items.Insert(0, new ListItem(Constants.DDLEmpType, "-1"));

        }

        private void BindApplicantProfession(List<SYSCodeDTO> lstsyscodeDto)
        {
            //APPLICANT_PROF
            chkEmpProfession.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                chkEmpProfession.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.APPLICANT_PROF.ToLower()).ToList();
                chkEmpProfession.DataTextField = "SHORT_NAME";
                chkEmpProfession.DataValueField = "CODE";
                chkEmpProfession.DataBind();

                SEC2_APPLICANT_PROF.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.APPLICANT_PROF.ToLower()).ToList();
                SEC2_APPLICANT_PROF.DataTextField = "SHORT_NAME";
                SEC2_APPLICANT_PROF.DataValueField = "CODE";
                SEC2_APPLICANT_PROF.DataBind();

                SEC1_APPLICANT_PROF.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.APPLICANT_PROF.ToLower()).ToList();
                SEC1_APPLICANT_PROF.DataTextField = "SHORT_NAME";
                SEC1_APPLICANT_PROF.DataValueField = "CODE";
                SEC1_APPLICANT_PROF.DataBind();
            }

        }

        private void BindApplicantDesignation(List<SYSCodeDTO> lstsyscodeDto)
        {
            chkdesignation.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                chkdesignation.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.EMPL_DESIGNATION.ToLower()).ToList();
                chkdesignation.DataTextField = "SHORT_NAME";
                chkdesignation.DataValueField = "CODE";
                chkdesignation.DataBind();
            }
        }

        private void BindOwnedVehicle(List<SYSCodeDTO> lstsyscodeDto)
        {
            //OWNED_VEHICLE_TYPE
            chkOwnedVehicle.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                chkOwnedVehicle.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.OWNED_VEHICLE_TYPE.ToLower()).ToList();
                chkOwnedVehicle.DataTextField = "SHORT_NAME";
                chkOwnedVehicle.DataValueField = "CODE";
                chkOwnedVehicle.DataBind();
            }
        }

        private void BindIncomePerMonth(List<SYSCodeDTO> lstsyscodeDto)
        {
            //INCOME_PER_MONTH
            radIncomePerMonth.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                radIncomePerMonth.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.INCOME_PER_MONTH.ToLower()).ToList();
                radIncomePerMonth.DataTextField = "SHORT_NAME";
                radIncomePerMonth.DataValueField = "CODE";
                radIncomePerMonth.DataBind();
            }
        }

        private void GetBankListSysCode(List<SYSCodeDTO> lstsyscodeDto)
        {
            //ACC_IN_BANK
            ddlIsAccountWithbank.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlIsAccountWithbank.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.ACC_IN_BANK.ToLower()).ToList();
                ddlIsAccountWithbank.DataTextField = "SHORT_NAME";
                ddlIsAccountWithbank.DataValueField = "CODE";
                ddlIsAccountWithbank.DataBind();
            }
            ddlIsAccountWithbank.Items.Insert(0, new ListItem(Constants.DDLBankName, "-1"));
        }

        private void BindRelation(List<SYSCodeDTO> lstsyscodeDto)
        {
            radAddonRelation.ClearSelection();
            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                radAddonRelation.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.RELATION.ToLower()).ToList();
                radAddonRelation.DataTextField = "SHORT_NAME";
                radAddonRelation.DataValueField = "CODE";
                radAddonRelation.DataBind();


                radAddon1Relation.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.RELATION.ToLower()).ToList();
                radAddon1Relation.DataTextField = "SHORT_NAME";
                radAddon1Relation.DataValueField = "CODE";
                radAddon1Relation.DataBind();

                Rad3AddonRelation.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.RELATION.ToLower()).ToList();
                Rad3AddonRelation.DataTextField = "SHORT_NAME";
                Rad3AddonRelation.DataValueField = "CODE";
                Rad3AddonRelation.DataBind();
            }
        }

        private void GetNomiRelListSysCode(List<SYSCodeDTO> lstsyscodeDto)
        {
            //ACC_IN_BANK
            ddlNomiRelation.Items.Clear();

            if (lstsyscodeDto != null && lstsyscodeDto.Count > 0)
            {
                ddlNomiRelation.DataSource = lstsyscodeDto.Where(x => x.TYPE_ID.ToLower() == Constants.NOMINEE_RELATION.ToLower()).ToList();
                ddlNomiRelation.DataTextField = "SHORT_NAME";
                ddlNomiRelation.DataValueField = "CODE";
                ddlNomiRelation.DataBind();
            }
            ddlNomiRelation.Items.Insert(0, new ListItem(Constants.DDLNomineeRel, "-1"));
        }

        private void MonthYear()
        {
            int expiryYear = 15;
            var next15Yrs = new List<string>();
            int currentYear = DateTime.Now.Year;
            if (_expiryYear != "0")
                expiryYear = Convert.ToInt32(_expiryYear);
            for (int i = currentYear; i <= currentYear + expiryYear; i++)
            {
                string j = Convert.ToString(i);
                next15Yrs.Add(j);
            }
            //databind here

            //DC_VALID_UPTO_YEAR.DataSource = next15Yrs;
            //DC_VALID_UPTO_YEAR.DataBind();
            //DC_VALID_UPTO_YEAR.Items.Insert(0, new ListItem("YY", "-1"));

            //CC_VALID_UPTO1_YEAR.DataSource = next15Yrs;
            //CC_VALID_UPTO1_YEAR.DataBind();
            //CC_VALID_UPTO1_YEAR.Items.Insert(0, new ListItem("YY", "-1"));

            //CC_VALID_UPTO2_YEAR.DataSource = next15Yrs;
            //CC_VALID_UPTO2_YEAR.DataBind();
            //CC_VALID_UPTO2_YEAR.Items.Insert(0, new ListItem("YY", "-1"));

            //CC_VALID_UPTO3_YEAR.DataSource = next15Yrs;
            //CC_VALID_UPTO3_YEAR.DataBind();
            //CC_VALID_UPTO3_YEAR.Items.Insert(0, new ListItem("YY", "-1"));
        }
    }
}
