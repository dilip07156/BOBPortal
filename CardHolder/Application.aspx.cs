using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using System.Configuration;
using System.Web.Services;

namespace CardHolder
{
    public partial class Application : PageBase
    {
        readonly string _expiryYear = ConfigurationManager.AppSettings["ExpiryYear"];


        public List<SYSCodeDTO> Citylist
        {
            get { return ViewState["Citylist"] == null ? null : (List<SYSCodeDTO>)ViewState["Citylist"]; }
            set { ViewState["Citylist"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf) { }
            else
            {
                if (!IsPostBack)
                {
                    Onload();
                }
            }

        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region Comment
                //// Submit logic need to code here

                //// Sample code to save application form
                //// 
                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('simple message');", true);
                ////return;
                ////throw new Exception("Error is appeard");

                ////byte[] buffer = Guid.NewGuid().ToByteArray();
                ////string Application_NO = BitConverter.ToUInt32(buffer, 6).ToString();

                //string Application_NO = DateTime.Now.ToString("yyyyMMddHHmmssf");
                //string AppHashNum = Application_NO.Encrypt();
                //Session["AppHashNum"] = AppHashNum;

                //ApplicationManager apl_mgr = new ApplicationManager();
                //apl_mgr.SaveApplication(new DTO.Application_DTO()
                //{
                //    APPLICATION_NO = Application_NO,
                //    SOURCE_APPLICATION_NO = SOURCE_APPLICATION_NO.Value,
                //    SOURCE_TYPE = "0",
                //    SOURCE_CODE = ddlBranchlist.SelectedValue,
                //    BANK_CODE = "444555",
                //    PRODUCT_CODE = GeneralMethods.GetAppliedCardNumberGlobally(ddlCardType.SelectedValue),
                //    VIP_CODE = (ddlVIPCode.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlVIPCode.SelectedValue)),
                //    SOCIAL_STATUS = ddlSocialStatus.SelectedValue,
                //    //----------------------------BASIC_CARD_FLAG = "Need Clarification",
                //    TITLE = (ddlTitle.SelectedValue == "-1" ? null : ddlTitle.SelectedValue),
                //    FIRST_NAME = FIRST_NAME.Value,
                //    MIDDLE_NAME = MIDDLE_NAME.Value,
                //    FAMILY_NAME = FAMILY_NAME.Value,
                //    EMBOSSED_NAME = EMBOSSED_NAME.Value,
                //    GENDER = (MALE.Checked == true ? "m" : (FEMALE.Checked == true ? "f" : null)),
                //    BIRTH_DATE = (BIRTH_DATE.Value == "" ? (DateTime?)null : Convert.ToDateTime(BIRTH_DATE.Value)),
                //    AGE = (AGE.Value == "" ? (int?)null : Convert.ToInt32(AGE.Value)),
                //    MARITAL_STATUS = (ddlMaritalStatus.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlMaritalStatus.SelectedValue)),
                //    DOM = ddlMarriageDay.Value + "/" + ddlMarriageMonth.Value,
                //    NO_OF_DEPENDENTS = (NO_OF_DEPENDENTS.Value == "" ? (int?)null : Convert.ToInt32(NO_OF_DEPENDENTS.Value)),
                //    NATIONALITY = (Residentindian.Checked == true ? Residentindian.Value : (NonResidentIndian.Checked == true ? NonResidentIndian.Value : null)),
                //    CURR_ADDRESS1 = CURR_ADDRESS1.Value,
                //    CURR_ADDRESS2 = CURR_ADDRESS2.Value,
                //    CURR_ADDRESS3 = CURR_ADDRESS3.Value,
                //    CURR_ADDRESS4 = CURR_ADDRESS4.Value,
                //    CURR_COUNTRY_CODE = (ddlCurrCountry.SelectedValue == "-1" ? null : ddlCurrCountry.SelectedValue),
                //    CURR_CITY_CODE = (ddlCurrCity.SelectedValue == "-1" ? null : ddlCurrCity.SelectedValue),
                //    CURR_POSTAL_CODE = CURR_POSTAL_CODE.Value,
                //    MOBILE_NUMBER = MOBILE_NUMBER.Value,
                //    EMAIL_ID = EMAIL_ID.Value,
                //    HOME_PHONE_NUMBER = EXT.Value + HOME_PHONE_NUMBER.Value,
                //    //RESIDANCE_CHANGED = (Onceinlast3years.Checked == true ? Onceinlast3years.Value : (NotChanged.Checked == true ? NotChanged.Value : (MorethanOnceinlast3Yr.Checked == true ? MorethanOnceinlast3Yr.Value : null))),
                //    RESIDANCE_CHANGED = (ddlchangeResi.SelectedValue == "-1" ? null : ddlchangeResi.SelectedValue),
                //    // RESIDENCE_STATUS = (ResidenceSelfOwned.Checked == true ? Convert.ToInt32(ResidenceSelfOwned.Value) : (ResidenceRented.Checked == true ? Convert.ToInt32(ResidenceRented.Value) : (ResidenceCompanyProvided.Checked == true ? Convert.ToInt32(ResidenceCompanyProvided.Value) : (ResidenceOthers.Checked == true ? Convert.ToInt32(ResidenceOthers.Value) : (int?)null)))),
                //    RESIDENCE_STATUS = (radResiSatus.SelectedValue == "" ? (int?)null : Convert.ToInt32(radResiSatus.SelectedValue)),

                //    PERM_ADDRESS1 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS1.Value : PERM_ADDRESS1.Value,
                //    PERM_ADDRESS2 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS2.Value : PERM_ADDRESS2.Value,
                //    PERM_ADDRESS3 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS3.Value : PERM_ADDRESS3.Value,
                //    PERM_ADDRESS4 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS4.Value : PERM_ADDRESS4.Value,
                //    PERM_COUNTRY_CODE = chkTickPermanentAddress.Checked == true ? (ddlCurrCountry.SelectedValue == "-1" ? null : ddlCurrCountry.SelectedValue) : (ddlPermCountry.SelectedValue == "-1" ? null : ddlPermCountry.SelectedValue),
                //    PERM_CITY_CODE = chkTickPermanentAddress.Checked == true ? (ddlCurrCity.SelectedValue == "-1" ? null : ddlCurrCity.SelectedValue) : (ddlPermCity.SelectedValue == "-1" ? null : ddlPermCity.SelectedValue),
                //    PERM_POSTAL_CODE = chkTickPermanentAddress.Checked == true ? CURR_POSTAL_CODE.Value : PERM_POSTAL_CODE.Value,
                //    DRIVING_LICENSE_NUMBER = DRIVING_LICENSE_NUMBER.Value,
                //    PASSPORT_NUMBER = PASSPORT_NUMBER.Value,


                //    EDUCATION = (ddlEducation.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlEducation.SelectedValue)),
                //    UNIVERSITY = UNIVERSITY.Value,
                //    FATHER_NAME = FATHER_NAME.Value,
                //    MAIDEN_NAME = MAIDEN_NAME.Value,
                //    SPOUSE_NAME = SPOUSE_NAME.Value,
                //    SPOUSE_MOB_NO = SPOUSE_MOB_NO.Value,
                //    //OWNED_VEHICLE_TYPE = (Vehicle2Wheeler.Checked == true ? Convert.ToInt32(Vehicle2Wheeler.Value) : (Vehicle4Wheeler.Checked == true ? Convert.ToInt32(Vehicle4Wheeler.Value) : (SelfOwned.Checked == true ? Convert.ToInt32(SelfOwned.Value) : (Rented.Checked == true ? Convert.ToInt32(Rented.Value) : (CompanyProvided.Checked == true ? Convert.ToInt32(CompanyProvided.Value) : (int?)null))))),
                //    //EMPLOYMENT_STATUS = (Professional.Checked == true ? Convert.ToInt32(Professional.Value) : (Service.Checked == true ? Convert.ToInt32(Service.Value) : (HouseWife.Checked == true ? Convert.ToInt32(HouseWife.Value) : (OccupationOthers.Checked == true ? Convert.ToInt32(OccupationOthers.Value) : (int?)null)))),
                //    OWNED_VEHICLE_TYPE = string.IsNullOrEmpty(chkOwnedVehicle.SelectedValue) ? (int?)null : Convert.ToInt32(chkOwnedVehicle.SelectedValue),
                //    EMPLOYMENT_STATUS = string.IsNullOrEmpty(chkEmpStatus.SelectedValue) ? (int?)null : Convert.ToInt32(chkEmpStatus.SelectedValue),
                //    COMP_TYPE = (ddlempType.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlempType.SelectedValue)),
                //    EMPLOYER_NAME = EMPLOYER_NAME.Value,
                //    //EMPL_DESIGNATION = (Director_SrExecutive_Managerial.Checked == true ? Convert.ToInt32(Director_SrExecutive_Managerial.Value) : (Junior_Clerical.Checked == true ? Convert.ToInt32(Junior_Clerical.Value) : (Designation_Other.Checked == true ? Convert.ToInt32(Designation_Other.Value) : (int?)null))),
                //    EMPL_DESIGNATION = string.IsNullOrEmpty(chkdesignation.SelectedValue) ? (int?)null : Convert.ToInt32(chkdesignation.SelectedValue),
                //    APPLICANT_PROF = string.IsNullOrEmpty(chkEmpProfession.SelectedValue) ? (int?)null : Convert.ToInt32(chkEmpProfession.SelectedValue),
                //    EMPL_DEPARTMENT = EMPL_DEPARTMENT.Value,
                //    EMPL_ADDRESS1 = EMPL_ADDRESS1.Value,
                //    EMPL_ADDRESS2 = EMPL_ADDRESS2.Value,
                //    EMPL_ADDRESS3 = EMPL_ADDRESS3.Value,
                //    EMPL_ADDRESS4 = EMPL_ADDRESS4.Value,
                //    EMPL_CITY_CODE = (ddlEmpCity.SelectedValue == "-1" ? null : ddlEmpCity.SelectedValue),
                //    EMPL_COUNTRY_CODE = (ddlEmpCountry.SelectedValue == "-1" ? null : ddlEmpCountry.SelectedValue),
                //    EMPL_POSTAL_CODE = EMPL_POSTAL_CODE.Value,
                //    OFFICE_PHONE_NUMBER = OFFICE_PHONE_NUMBER.Value,
                //    CURRENT_JOB_TENURE = CURRENT_JOB_TENURE.Value == "" ? (int?)null : Convert.ToInt32(CURRENT_JOB_TENURE.Value),
                //    EMPL_ID = EMPL_ID.Value,
                //    ANNUAL_INCOME_CODE = (ANNUAL_INCOME_CODE.Value == "" ? (int?)null : Convert.ToInt32(ANNUAL_INCOME_CODE.Value)),
                //    OTHER_INCOME = (OTHER_INCOME.Value == "" ? (double?)null : Convert.ToDouble(OTHER_INCOME.Value)),
                //    SPOUSE_INCOME = (SPOUSE_INCOME.Value == "" ? (double?)null : Convert.ToDouble(SPOUSE_INCOME.Value)),
                //    //INCOME_PER_MONTH = (More_than_10000.Checked == true ? More_than_10000.Value : (More_than_15000.Checked == true ? More_than_15000.Value : (More_than_20000.Checked == true ? More_than_20000.Value : ""))),
                //    INCOME_PER_MONTH = string.IsNullOrEmpty(radIncomePerMonth.SelectedValue) ? null : radIncomePerMonth.SelectedValue,
                //    CUSTOMER_ID = CUSTOMER_ID.Value,
                //    PAN_GIR_NO = PAN_GIR_NO.Value,
                //    TAX_PAID = TAX_PAID.Value,
                //    YEAR_TAX_PAID = YEAR_TAX_PAID.Value,
                //    ACC_IN_BANK = (ddlIsAccountWithbank.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlIsAccountWithbank.SelectedValue)),
                //    ACCOUNT_BRANCH = string.IsNullOrEmpty(ddlAccountBranch.SelectedValue) ? null : ddlAccountBranch.SelectedValue,

                //    //OTHER_BANK_NAME = OTHER_BANK_NAME.Value,
                //    //OTHER_BRANCH = OTHER_BRANCH.Value,
                //    OTHER_CITY = OTHER_CITY.Value,
                //    OUR_ACCOUNT_TENURE = (OUR_ACCOUNT_TENURE.Value == "" ? (int?)null : Convert.ToInt32(OUR_ACCOUNT_TENURE.Value)),
                //    OUR_ACCOUNT_TYPE = (saving_acc.Checked == true ? saving_acc.Value : (other_acc.Checked == true ? other_acc.Value : (current_acc.Checked == true ? current_acc.Value : ""))),
                //    ACCOUNT_NUMBER = ACCOUNT_NUMBER.Value,
                //    //OTHER_ACCOUNT_NUMBER = OTHER_ACCOUNT_NUMBER.Value,
                //    HOUSING_LOAN = HOUSING_LOAN.Checked == true ? "1" : "",
                //    CAR_LOAN = CAR_LOAN.Checked == true ? "1" : "",
                //    CONSUMER_LOAN = CONSUMER_LOAN.Checked == true ? "1" : "",
                //    BUSINESS_LOAN = BUSINESS_LOAN.Checked == true ? "1" : "",
                //    OTHR_LOAN = OTHR_LOAN.Checked == true ? "1" : "",
                //    OTHER_LOAN = OTHR_LOAN.Checked == true ? "1" : "",
                //    OTHER_LOAN_DESC = OTHER_LOAN.Value,
                //    LOAN_AMOUNT = LOAN_AMOUNT.Value == "" ? (double?)null : Convert.ToDouble(LOAN_AMOUNT.Value),
                //    CURRENT_OUTSTANDING = CURRENT_OUTSTANDING.Value == "" ? (double?)null : Convert.ToDouble(CURRENT_OUTSTANDING.Value),
                //    DURATION_OF_LOAN = DURATION_OF_LOAN.Value,
                //    LOAN_INSTUTION_NAME = LOAN_INSTUTION_NAME.Value,
                //    LOAN_BRANCH = LOAN_BRANCH.Value,
                //    BOB_DEBIT_CARD_NO = BOB_DEBIT_CARD_NO.Value,
                //    DC_VALID_UPTO = DC_VALID_UPTO_MONTH.Value + DC_VALID_UPTO_YEAR.Value.Substring(2, 2),
                //    CC_BANK_NAME1 = CC_BANK_NAME1.Value,
                //    CC_NO1 = CC_NO1.Value,
                //    CC_VALID_UPTO1 = CC_VALID_UPTO1_MONTH.Value + CC_VALID_UPTO1_YEAR.Value.Substring(2, 2),
                //    CC_CR_LITMIT1 = CC_CR_LITMIT1.Value == "" ? (double?)null : Convert.ToDouble(CC_CR_LITMIT1.Value),
                //    CC_BANK_NAME2 = CC_BANK_NAME2.Value,
                //    CC_NO2 = CC_NO2.Value,
                //    CC_VALID_UPTO2 = CC_VALID_UPTO2_MONTH.Value + CC_VALID_UPTO2_YEAR.Value.Substring(2, 2),
                //    CC_CR_LITMIT2 = CC_CR_LITMIT2.Value == "" ? (double?)null : Convert.ToDouble(CC_CR_LITMIT2.Value),
                //    CC_BANK_NAME3 = CC_BANK_NAME3.Value,
                //    CC_NO3 = CC_NO3.Value,
                //    CC_VALID_UPTO3 = CC_VALID_UPTO3_MONTH.Value + CC_VALID_UPTO3_YEAR.Value.Substring(2, 2),
                //    CC_CR_LITMIT3 = CC_CR_LITMIT2.Value == "" ? (double?)null : Convert.ToDouble(CC_CR_LITMIT2.Value),
                //    DATE_CREATE = DateTime.Now,

                //    ADDITIONAL_CARD_NAME = ADDITIONAL_CARD_NAME.Value,
                //    ADDITIONAL_CARD_EMB_NAME = ADDITIONAL_CARD_NAME.Value,
                //    ADDITIONAL_CARD_REL = string.IsNullOrEmpty(radAddonRelation.SelectedValue) ? null : radAddonRelation.SelectedItem.Text,
                //    ADDITIONAL_GENDER = string.IsNullOrEmpty(radAddGender.SelectedValue) ? null : radAddGender.SelectedValue,
                //    SEC_BIRTH_DATE = (SEC_BIRTH_DATE.Value == "" ? (DateTime?)null : Convert.ToDateTime(SEC_BIRTH_DATE.Value)),

                //    REF1_NAME = REF1_NAME.Value,
                //    REF1_ADDRESS1 = REF1_ADDRESS1.Value,
                //    REF1_ADDRESS2 = REF1_ADDRESS2.Value,
                //    REF1_ADDRESS3 = REF1_ADDRESS3.Value,
                //    REF1_ADDRESS4 = REF1_ADDRESS4.Value,
                //    REF1_COUNTRY_CODE = (ddlRelcountry.SelectedValue == "-1" ? null : ddlRelcountry.SelectedValue),
                //    REF1_CITY_CODE = (ddlRelcity.SelectedValue == "-1" ? null : ddlRelcity.SelectedValue),
                //    REF1_ZIP_CODE = REF1_ZIP_CODE.Value,
                //    REF1_PHONE_NUMBER = REF1_PHONE_NUMBER.Value,


                //    REF2_NAME = REF2_NAME.Value,
                //    REF2_ADDRESS1 = REF2_ADDRESS1.Value,
                //    REF2_ADDRESS2 = REF2_ADDRESS2.Value,
                //    REF2_ADDRESS3 = REF2_ADDRESS3.Value,
                //    REF2_ADDRESS4 = REF2_ADDRESS4.Value,
                //    REF2_COUNTRY_CODE = (ddlRel2Country.SelectedValue == "-1" ? null : ddlRel2Country.SelectedValue),
                //    REF2_CITY_CODE = (ddlrel2city.SelectedValue == "-1" ? null : ddlrel2city.SelectedValue),
                //    REF2_ZIP_CODE = REF2_ZIP_CODE.Value,
                //    REF2_PHONE_NUMBER = REF2_PHONE_NUMBER.Value,
                //    INSURANCE_NOM_NAME = INSURANCE_NOM_NAME.Value,
                //    INSURANCE_NOM_REL = ddlNomiRelation.SelectedValue == "-1" ? null : ddlNomiRelation.SelectedItem.Text,

                //    DIRECT_DEBIT_ACCOUNT_TYPE = "0",
                //    DIRECT_DEBIT_ACCOUNT_NUMBER = DIRECT_DEBIT_ACCOUNT_NUMBER.Value,
                //    DIRECT_DEBIT_ACCOUNT_NAME = DIRECT_DEBIT_ACCOUNT_NAME.Value,
                //    DIRECT_DEBIT_BRANCH =  ddlDebitBranchList.SelectedValue,
                //    DIRECT_DEBIT_FLAG = chkautodebit.Checked == true ? "1" : "0",
                //});

                //// Redirect page it application details has been saved to cater refresh (F5)
                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Constants.AppSuccess + "');window.location ='Application.aspx';", true);

                //var fullname = FIRST_NAME.Value + " " + MIDDLE_NAME.Value + " " + FAMILY_NAME.Value;
                //var email = EMAIL_ID.Value;
                //Mailfunction(AppHashNum, fullname, email);

                //Response.Redirect("ApplicationSuccess.aspx", true);

                ////throw new Exception("new error message"); 
                #endregion
                ApplicationManager apl_mgr = new ApplicationManager();
                Application_DTO objAppDto = new DTO.Application_DTO();
                //Application_NoGlobal = Convert.ToString(Session["AppNo"].ToString());

                //string Application_NO = DateTime.Now.ToString("yyyyMMddHHmmssf");
                //string AppHashNum = Application_NO.Encrypt();
                //Session["AppHashNum"] = AppHashNum;

                //objAppDto.APPLICATION_NO = Application_NO;
                objAppDto.SOURCE_APPLICATION_NO = SOURCE_APPLICATION_NO.Value;
                objAppDto.SOURCE_TYPE = "0";
                objAppDto.SOURCE_CODE = ddlBranchlist.SelectedValue;
                objAppDto.STAFF_E_C_NO = STAFF_E_C_NO.Value.ToUpper();
                objAppDto.STAFF_NAME = STAFF_NAME.Value.ToUpper();
                objAppDto.RECOMMENDED_BY = ddlRecommendedBy.SelectedValue == "0" ? null : ddlRecommendedBy.SelectedValue;
                objAppDto.RECOMMENDED_BRANCH = ddlRecommendedBranch.SelectedValue == "-1" ? null : ddlRecommendedBranch.SelectedValue;
                objAppDto.PREFERRED_MAILING_ADDRESS = (rbOffice.Checked == true ? "1" : (rbResidence.Checked == true ? "0" : null));
                objAppDto.BANK_CODE = "444555";
                objAppDto.PRODUCT_CODE = GeneralMethods.GetAppliedCardNumberGlobally(ddlCardType.SelectedValue);
                objAppDto.VIP_CODE = (ddlVIPCode.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlVIPCode.SelectedValue));
                objAppDto.SOCIAL_STATUS = ddlSocialStatus.SelectedValue;
                //----------------------------BASIC_CARD_FLAG = "Need Clarification";

                objAppDto.TITLE = (ddlTitle.SelectedValue == "-1" ? null : ddlTitle.SelectedValue);
                objAppDto.FIRST_NAME = FIRST_NAME.Value.ToUpper();
                objAppDto.MIDDLE_NAME = MIDDLE_NAME.Value.ToUpper();
                objAppDto.FAMILY_NAME = FAMILY_NAME.Value.ToUpper();
                objAppDto.EMBOSSED_NAME = EMBOSSED_NAME.Value.ToUpper();
                objAppDto.GENDER = (MALE.Checked == true ? "m" : (FEMALE.Checked == true ? "f" : null));
                // objAppDto.BIRTH_DATE = (BIRTH_DATE.Value == "" ? (DateTime?)null : Convert.ToDateTime(BIRTH_DATE.Value));
                objAppDto.BIRTH_DATE = (BIRTH_DATE.Value == "" ? (DateTime?)null : Convert.ToDateTime(Microsoft.Security.Application.Encoder.HtmlEncode(BIRTH_DATE.Value), GeneralMethods.getDateTimeFormatInfo()));
                objAppDto.AGE = (AGE.Value == "" ? (int?)null : Convert.ToInt32(AGE.Value));
                objAppDto.MARITAL_STATUS = (ddlMaritalStatus.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlMaritalStatus.SelectedValue));
                objAppDto.DOM = ddlMarriageDay.Value + "/" + ddlMarriageMonth.Value;
                objAppDto.NO_OF_DEPENDENTS = (NO_OF_DEPENDENTS.Value == "" ? (int?)null : Convert.ToInt32(NO_OF_DEPENDENTS.Value));
                objAppDto.NATIONALITY = (Residentindian.Checked == true ? Residentindian.Value : (NonResidentIndian.Checked == true ? NonResidentIndian.Value : null));
                objAppDto.CURR_ADDRESS1 = CURR_ADDRESS1.Value.ToUpper();
                objAppDto.CURR_ADDRESS2 = CURR_ADDRESS2.Value.ToUpper();
                objAppDto.CURR_ADDRESS3 = CURR_ADDRESS3.Value.ToUpper();
                objAppDto.CURR_ADDRESS4 = CURR_ADDRESS4.Value.ToUpper();
                objAppDto.CURR_COUNTRY_CODE = (ddlCurrCountry.SelectedValue == "-1" ? null : ddlCurrCountry.SelectedValue);
                objAppDto.CURR_CITY_CODE = (ddlCurrCity.SelectedValue == "-1" ? null : ddlCurrCity.SelectedValue);
                objAppDto.CURR_POSTAL_CODE = CURR_POSTAL_CODE.Value;
                objAppDto.MOBILE_NUMBER = MOBILE_NUMBER.Value;
                objAppDto.EMAIL_ID = EMAIL_ID.Value;
                objAppDto.HOME_PHONE_NUMBER = EXT.Value + "-" + HOME_PHONE_NUMBER.Value;
                //RESIDANCE_CHANGED = (Onceinlast3years.Checked == true ? Onceinlast3years.Value : (NotChanged.Checked == true ? NotChanged.Value : (MorethanOnceinlast3Yr.Checked == true ? MorethanOnceinlast3Yr.Value : null)));
                objAppDto.RESIDANCE_CHANGED = (ddlchangeResi.SelectedValue == "-1" ? null : ddlchangeResi.SelectedValue);
                // RESIDENCE_STATUS = (ResidenceSelfOwned.Checked == true ? Convert.ToInt32(ResidenceSelfOwned.Value) : (ResidenceRented.Checked == true ? Convert.ToInt32(ResidenceRented.Value) : (ResidenceCompanyProvided.Checked == true ? Convert.ToInt32(ResidenceCompanyProvided.Value) : (ResidenceOthers.Checked == true ? Convert.ToInt32(ResidenceOthers.Value) : (int?)null))));
                objAppDto.RESIDENCE_STATUS = (radResiSatus.SelectedValue == "" ? (int?)null : Convert.ToInt32(radResiSatus.SelectedValue));

                objAppDto.PERM_ADDRESS1 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS1.Value.ToUpper() : PERM_ADDRESS1.Value.ToUpper();
                objAppDto.PERM_ADDRESS2 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS2.Value.ToUpper() : PERM_ADDRESS2.Value.ToUpper();
                objAppDto.PERM_ADDRESS3 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS3.Value.ToUpper() : PERM_ADDRESS3.Value.ToUpper();
                objAppDto.PERM_ADDRESS4 = chkTickPermanentAddress.Checked == true ? CURR_ADDRESS4.Value.ToUpper() : PERM_ADDRESS4.Value.ToUpper();
                objAppDto.PERM_COUNTRY_CODE = chkTickPermanentAddress.Checked == true ? (ddlCurrCountry.SelectedValue == "-1" ? null : ddlCurrCountry.SelectedValue) : (ddlPermCountry.SelectedValue == "-1" ? null : ddlPermCountry.SelectedValue);
                objAppDto.PERM_CITY_CODE = chkTickPermanentAddress.Checked == true ? (ddlCurrCity.SelectedValue == "-1" ? null : ddlCurrCity.SelectedValue) : (ddlPermCity.SelectedValue == "-1" ? null : ddlPermCity.SelectedValue);
                objAppDto.PERM_POSTAL_CODE = chkTickPermanentAddress.Checked == true ? CURR_POSTAL_CODE.Value : PERM_POSTAL_CODE.Value;
                objAppDto.DRIVING_LICENSE_NUMBER = DRIVING_LICENSE_NUMBER.Value;
                objAppDto.PASSPORT_NUMBER = PASSPORT_NUMBER.Value.ToUpper();

                objAppDto.EDUCATION = (ddlEducation.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlEducation.SelectedValue));
                objAppDto.UNIVERSITY = UNIVERSITY.Value.ToUpper();
                objAppDto.FATHER_NAME = FATHER_NAME.Value.ToUpper();
                objAppDto.MAIDEN_NAME = MAIDEN_NAME.Value.ToUpper();
                objAppDto.SPOUSE_NAME = SPOUSE_NAME.Value.ToUpper();
                objAppDto.SPOUSE_MOB_NO = SPOUSE_MOB_NO.Value;
                //OWNED_VEHICLE_TYPE = (Vehicle2Wheeler.Checked == true ? Convert.ToInt32(Vehicle2Wheeler.Value) : (Vehicle4Wheeler.Checked == true ? Convert.ToInt32(Vehicle4Wheeler.Value) : (SelfOwned.Checked == true ? Convert.ToInt32(SelfOwned.Value) : (Rented.Checked == true ? Convert.ToInt32(Rented.Value) : (CompanyProvided.Checked == true ? Convert.ToInt32(CompanyProvided.Value) : (int?)null)))));
                //EMPLOYMENT_STATUS = (Professional.Checked == true ? Convert.ToInt32(Professional.Value) : (Service.Checked == true ? Convert.ToInt32(Service.Value) : (HouseWife.Checked == true ? Convert.ToInt32(HouseWife.Value) : (OccupationOthers.Checked == true ? Convert.ToInt32(OccupationOthers.Value) : (int?)null))));
                objAppDto.OWNED_VEHICLE_TYPE = string.IsNullOrEmpty(chkOwnedVehicle.SelectedValue) ? (int?)null : Convert.ToInt32(chkOwnedVehicle.SelectedValue);
                objAppDto.EMPLOYMENT_STATUS = string.IsNullOrEmpty(chkEmpStatus.SelectedValue) ? (int?)null : Convert.ToInt32(chkEmpStatus.SelectedValue);
                objAppDto.COMP_TYPE = (ddlempType.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlempType.SelectedValue));
                objAppDto.EMPLOYER_NAME = EMPLOYER_NAME.Value.ToUpper();
                //EMPL_DESIGNATION = (Director_SrExecutive_Managerial.Checked == true ? Convert.ToInt32(Director_SrExecutive_Managerial.Value) : (Junior_Clerical.Checked == true ? Convert.ToInt32(Junior_Clerical.Value) : (Designation_Other.Checked == true ? Convert.ToInt32(Designation_Other.Value) : (int?)null)));
                objAppDto.EMPL_DESIGNATION = string.IsNullOrEmpty(chkdesignation.SelectedValue) ? (int?)null : Convert.ToInt32(chkdesignation.SelectedValue);
                objAppDto.APPLICANT_PROF = string.IsNullOrEmpty(chkEmpProfession.SelectedValue) ? (int?)null : Convert.ToInt32(chkEmpProfession.SelectedValue);
                objAppDto.EMPL_DEPARTMENT = EMPL_DEPARTMENT.Value.ToUpper();
                objAppDto.EMPL_ADDRESS1 = EMPL_ADDRESS1.Value.ToUpper();
                objAppDto.EMPL_ADDRESS2 = EMPL_ADDRESS2.Value.ToUpper();
                objAppDto.EMPL_ADDRESS3 = EMPL_ADDRESS3.Value.ToUpper();
                objAppDto.EMPL_ADDRESS4 = EMPL_ADDRESS4.Value.ToUpper();
                objAppDto.EMPL_CITY_CODE = (ddlEmpCity.SelectedValue == "-1" ? null : ddlEmpCity.SelectedValue);
                objAppDto.EMPL_COUNTRY_CODE = (ddlEmpCountry.SelectedValue == "-1" ? null : ddlEmpCountry.SelectedValue);
                objAppDto.EMPL_POSTAL_CODE = EMPL_POSTAL_CODE.Value;
                objAppDto.OFFICE_PHONE_NUMBER = OFFICE_PHONE_NUMBER.Value;
                //string JOBMONTHS1 = JOB_MONTHS.Value == "" ? "00" : JOB_MONTHS.Value == null ? "00" : JOB_MONTHS.Value.Length == 1 ? JOB_MONTHS.Value.ToString().PadLeft(1, '0') : JOB_MONTHS.Value;
                //string CurrentTJobTenure = CURRENT_JOB_TENURE.Value == "" ? "00" : CURRENT_JOB_TENURE.Value == null ? "00" : CURRENT_JOB_TENURE.Value.Length == 1 ? CURRENT_JOB_TENURE.Value.ToString().PadLeft(1, '0') : CURRENT_JOB_TENURE.Value;

                string JOBMONTHS = string.IsNullOrEmpty(JOB_MONTHS.Value) ? "0" : JOB_MONTHS.Value;
                string JobYears = string.IsNullOrEmpty(CURRENT_JOB_TENURE.Value) ? "0" : CURRENT_JOB_TENURE.Value;

                int? CurrentTJobTenure = (Convert.ToInt32(JobYears) * 12) + Convert.ToInt32(JOBMONTHS);
                objAppDto.CURRENT_JOB_TENURE = CurrentTJobTenure;

                // objAppDto.CURRENT_JOB_TENURE = Convert.ToInt32(CurrentTJobTenure + JOBMONTHS1);
                //objAppDto.CURRENT_JOB_TENURE = CURRENT_JOB_TENURE.Value == "" ? (int?)null : Convert.ToInt32(CURRENT_JOB_TENURE.Value);
                objAppDto.EMPL_ID = EMPL_ID.Value;
                objAppDto.ANNUAL_INCOME_CODE = (ANNUAL_INCOME_CODE.Value == "" ? (int?)null : Convert.ToInt32(ANNUAL_INCOME_CODE.Value));
                objAppDto.OTHER_INCOME = (OTHER_INCOME.Value == "" ? (double?)null : Convert.ToDouble(OTHER_INCOME.Value));
                objAppDto.SPOUSE_INCOME = (SPOUSE_INCOME.Value == "" ? (double?)null : Convert.ToDouble(SPOUSE_INCOME.Value));
                //INCOME_PER_MONTH = (More_than_10000.Checked == true ? More_than_10000.Value : (More_than_15000.Checked == true ? More_than_15000.Value : (More_than_20000.Checked == true ? More_than_20000.Value : "")));
                objAppDto.INCOME_PER_MONTH = string.IsNullOrEmpty(radIncomePerMonth.SelectedValue) ? "1" : radIncomePerMonth.SelectedValue;
                objAppDto.CUSTOMER_ID = CUSTOMER_ID.Value;
                objAppDto.PAN_GIR_NO = PAN_GIR_NO.Value;
                objAppDto.TAX_PAID = TAX_PAID.Value;
                objAppDto.YEAR_TAX_PAID = YEAR_TAX_PAID.Value;
                objAppDto.ACC_IN_BANK = (ddlIsAccountWithbank.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(ddlIsAccountWithbank.SelectedValue));
                objAppDto.ACCOUNT_BRANCH = string.IsNullOrEmpty(ddlAccountBranch.SelectedValue) ? null : ddlAccountBranch.SelectedValue;

                //OTHER_BANK_NAME = OTHER_BANK_NAME.Value;
                //OTHER_BRANCH = OTHER_BRANCH.Value;
                objAppDto.OTHER_CITY = OTHER_CITY.Value.ToUpper();
                objAppDto.OUR_ACCOUNT_TENURE = string.IsNullOrEmpty(OUR_ACCOUNT_TENURE.Value) ? (int?)null : Convert.ToInt32(OUR_ACCOUNT_TENURE.Value);
                objAppDto.OUR_ACCOUNT_TYPE = (saving_acc.Checked == true ? saving_acc.Value : (other_acc.Checked == true ? other_acc.Value : (current_acc.Checked == true ? current_acc.Value : "")));
                objAppDto.ACCOUNT_NUMBER = ACCOUNT_NUMBER.Value;
                //OTHER_ACCOUNT_NUMBER = OTHER_ACCOUNT_NUMBER.Value;
                objAppDto.HOUSING_LOAN = HOUSING_LOAN.Checked == true ? "1" : "";
                objAppDto.CAR_LOAN = CAR_LOAN.Checked == true ? "1" : "";
                objAppDto.CONSUMER_LOAN = CONSUMER_LOAN.Checked == true ? "1" : "";
                objAppDto.BUSINESS_LOAN = BUSINESS_LOAN.Checked == true ? "1" : "";
                objAppDto.OTHR_LOAN = OTHR_LOAN.Checked == true ? "1" : "";
                //objAppDto.OTHER_LOAN = OTHER_LOAN.Value;
                objAppDto.TYPE_OF_LOAN_OTHERS = OTHER_LOAN.Value; //objAppDto.OTHER_LOAN_DESC = OTHER_LOAN.Value;
                objAppDto.LOAN_AMOUNT = LOAN_AMOUNT.Value == "" ? (double?)null : Convert.ToDouble(LOAN_AMOUNT.Value);
                objAppDto.CURRENT_OUTSTANDING = CURRENT_OUTSTANDING.Value == "" ? (double?)null : Convert.ToDouble(CURRENT_OUTSTANDING.Value);
                objAppDto.DURATION_OF_LOAN = DURATION_OF_LOAN.Value;

                objAppDto.LOAN_INSTUTION_NAME = LOAN_INSTUTION_NAME.Value.ToUpper();
                objAppDto.LOAN_BRANCH = LOAN_BRANCH.Value.ToUpper();
                objAppDto.BOB_DEBIT_CARD_NO = BOB_DEBIT_CARD_NO.Value;
                objAppDto.DC_VALID_UPTO = DC_VALID_UPTO_MONTH.Value + (DC_VALID_UPTO_YEAR.Value == "YY" ? DC_VALID_UPTO_YEAR.Value : DC_VALID_UPTO_YEAR.Value.Substring(2, 2));
                objAppDto.CC_BANK_NAME1 = CC_BANK_NAME1.Value.ToUpper();
                objAppDto.CC_NO1 = CC_NO1.Value;
                objAppDto.CC_VALID_UPTO1 = CC_VALID_UPTO1_MONTH.Value + (CC_VALID_UPTO1_YEAR.Value == "YY" ? CC_VALID_UPTO1_YEAR.Value : CC_VALID_UPTO1_YEAR.Value.Substring(2, 2));
                objAppDto.CC_CR_LITMIT1 = CC_CR_LITMIT1.Value == "" ? (double?)null : Convert.ToDouble(CC_CR_LITMIT1.Value);
                objAppDto.CC_BANK_NAME2 = CC_BANK_NAME2.Value.ToUpper();
                objAppDto.CC_NO2 = CC_NO2.Value;
                objAppDto.CC_VALID_UPTO2 = CC_VALID_UPTO2_MONTH.Value + (CC_VALID_UPTO2_YEAR.Value == "YY" ? CC_VALID_UPTO2_YEAR.Value : CC_VALID_UPTO2_YEAR.Value.Substring(2, 2));
                objAppDto.CC_CR_LITMIT2 = CC_CR_LITMIT2.Value == "" ? (double?)null : Convert.ToDouble(CC_CR_LITMIT2.Value);
                objAppDto.CC_BANK_NAME3 = CC_BANK_NAME3.Value.ToUpper();
                objAppDto.CC_NO3 = CC_NO3.Value;
                objAppDto.CC_VALID_UPTO3 = CC_VALID_UPTO3_MONTH.Value + (CC_VALID_UPTO3_YEAR.Value == "YY" ? CC_VALID_UPTO3_YEAR.Value : CC_VALID_UPTO3_YEAR.Value.Substring(2, 2));
                objAppDto.CC_CR_LITMIT3 = CC_CR_LITMIT2.Value == "" ? (double?)null : Convert.ToDouble(CC_CR_LITMIT2.Value);
                objAppDto.DATE_CREATE = DateTime.Now;

                objAppDto.ADDITIONAL_CARD_NAME = ADDITIONAL_CARD_NAME.Value.ToUpper();
                if (ADDITIONAL_CARD_NAME.Value.Length > 19)
                    objAppDto.ADDITIONAL_CARD_EMB_NAME = ADDITIONAL_CARD_NAME.Value.ToUpper().Substring(0, 19);
                else
                    objAppDto.ADDITIONAL_CARD_EMB_NAME = ADDITIONAL_CARD_NAME.Value.ToUpper();

                objAppDto.ADDITIONAL_CARD_REL = string.IsNullOrEmpty(radAddonRelation.SelectedValue) ? null : radAddonRelation.SelectedValue;
                objAppDto.ADDITIONAL_GENDER = string.IsNullOrEmpty(radAddGender.SelectedValue) ? null : radAddGender.SelectedValue;
                objAppDto.SEC_BIRTH_DATE = (SEC_BIRTH_DATE.Value == "" ? (DateTime?)null : Convert.ToDateTime(Microsoft.Security.Application.Encoder.HtmlEncode(SEC_BIRTH_DATE.Value), GeneralMethods.getDateTimeFormatInfo()));
                objAppDto.SEC1_APPLICANT_PROF = SEC1_APPLICANT_PROF.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(SEC1_APPLICANT_PROF.SelectedValue);

                objAppDto.SEC2_FIRST_NAME = SEC2_FIRST_NAME.Value.ToUpper();
                if (ADDITIONAL_CARD_NAME.Value.Length > 19)
                    objAppDto.SEC2_EMBOSS_NAME = SEC2_FIRST_NAME.Value.ToUpper().Substring(0, 19);
                else
                    objAppDto.SEC2_EMBOSS_NAME = SEC2_FIRST_NAME.Value.ToUpper();
                objAppDto.SEC2_RELATION = string.IsNullOrEmpty(radAddon1Relation.SelectedValue) ? null : radAddon1Relation.SelectedValue;
                objAppDto.SEC2_GENDER = string.IsNullOrEmpty(radAddGender2.SelectedValue) ? null : radAddGender2.SelectedValue;
                objAppDto.SEC2_BIRTH_DATE = (SEC2_BIRTH_DATE.Value == "" ? (DateTime?)null : Convert.ToDateTime(Microsoft.Security.Application.Encoder.HtmlEncode(SEC2_BIRTH_DATE.Value), GeneralMethods.getDateTimeFormatInfo()));
                objAppDto.SEC2_APPLICANT_PROF = SEC2_APPLICANT_PROF.SelectedValue == "-1" ? (int?)null : Convert.ToInt32(SEC2_APPLICANT_PROF.SelectedValue);


                objAppDto.REF1_NAME = REF1_NAME.Value.ToUpper();
                objAppDto.REF1_ADDRESS1 = REF1_ADDRESS1.Value.ToUpper();
                objAppDto.REF1_ADDRESS2 = REF1_ADDRESS2.Value.ToUpper();
                objAppDto.REF1_ADDRESS3 = REF1_ADDRESS3.Value.ToUpper();
                objAppDto.REF1_ADDRESS4 = REF1_ADDRESS4.Value.ToUpper();
                objAppDto.REF1_COUNTRY_CODE = (ddlRelcountry.SelectedValue == "-1" ? null : ddlRelcountry.SelectedValue);
                objAppDto.REF1_CITY_CODE = (ddlRelcity.SelectedValue == "-1" ? null : ddlRelcity.SelectedValue == "" ? null : ddlRelcity.SelectedValue);
                objAppDto.REF1_ZIP_CODE = REF1_ZIP_CODE.Value.ToUpper();
                objAppDto.REF1_PHONE_NUMBER = REF1_PHONE_NUMBER.Value;


                objAppDto.REF2_NAME = REF2_NAME.Value.ToUpper();
                objAppDto.REF2_ADDRESS1 = REF2_ADDRESS1.Value.ToUpper();
                objAppDto.REF2_ADDRESS2 = REF2_ADDRESS2.Value.ToUpper();
                objAppDto.REF2_ADDRESS3 = REF2_ADDRESS3.Value.ToUpper();
                objAppDto.REF2_ADDRESS4 = REF2_ADDRESS4.Value.ToUpper();
                objAppDto.REF2_COUNTRY_CODE = (ddlRel2Country.SelectedValue == "-1" ? null : ddlRel2Country.SelectedValue);
                objAppDto.REF2_CITY_CODE = (ddlrel2city.SelectedValue == "-1" ? null : ddlrel2city.SelectedValue == "" ? null : ddlrel2city.SelectedValue);
                objAppDto.REF2_ZIP_CODE = REF2_ZIP_CODE.Value.ToUpper();
                objAppDto.REF2_PHONE_NUMBER = REF2_PHONE_NUMBER.Value;
                objAppDto.INSURANCE_NOM_NAME = INSURANCE_NOM_NAME.Value;
                objAppDto.INSURANCE_NOM_REL = ddlNomiRelation.SelectedValue == "-1" ? null : ddlNomiRelation.SelectedItem.Text;

                objAppDto.DIRECT_DEBIT_ACCOUNT_TYPE = ddlDebitAccountType.SelectedValue == "-1" ? null : ddlDebitAccountType.SelectedValue;
                objAppDto.DIRECT_DEBIT_ACCOUNT_NUMBER = DIRECT_DEBIT_ACCOUNT_NUMBER.Value;
                objAppDto.DIRECT_DEBIT_ACCOUNT_NAME = DIRECT_DEBIT_ACCOUNT_NAME.Value.ToUpper();
                objAppDto.DIRECT_DEBIT_BRANCH = ddlDebitBranchList.SelectedValue;
                //objAppDto.DIRECT_DEBIT_FLAG = chkautodebit.Checked == true ? "1" : "0";
                if (chkautodebit.Checked == true)
                {
                    if (string.IsNullOrEmpty(DIRECT_DEBIT_ACCOUNT_NAME.Value) && string.IsNullOrEmpty(DIRECT_DEBIT_ACCOUNT_NUMBER.Value) && ddlDebitAccountType.SelectedIndex == 0 && ddlDebitBranchList.SelectedIndex == 0 && string.IsNullOrEmpty(DIRECT_DEBIT_ACCOUNT_NAME.Value))
                        objAppDto.DIRECT_DEBIT_FLAG = "0";
                    else
                        objAppDto.DIRECT_DEBIT_FLAG = "1";
                }
                else
                    objAppDto.DIRECT_DEBIT_FLAG = "0";

                objAppDto.DIRECT_DEBIT_AMOUNT_FLAG = Convert.ToInt32(rbTotalAmountDue.Checked == true ? hideTotalAmountDue.Value : (rbMinimumAmountDue.Checked == true ? hideMinimumAmountDue.Value : (rbPercentage.Checked == true ? hidePercentage.Value : null)));
                objAppDto.DIRECT_DEBIT_PERCENTAGE = Convert.ToInt32(rbPercentage.Checked == true ? DIRECT_DEBIT_PERCENTAGE.Text == null ? (int?)null : DIRECT_DEBIT_PERCENTAGE.Text == "" ? (int?)null : Convert.ToInt32(DIRECT_DEBIT_PERCENTAGE.Text) : (int?)null);
                objAppDto.PROMO_CODE = ddlPromoCode.SelectedValue == "-1" ? null : ddlPromoCode.SelectedValue;
                objAppDto.APPLICATION_TYPE = ddlApplicationType.SelectedValue == "-1" ? null : ddlApplicationType.SelectedValue;
                objAppDto.VC_ALIAS_NAME = VC_ALIAS_NAME.Value == "" ? null : VC_ALIAS_NAME.Value.ToUpper();

                //if (objAppDto.ID == 0)
                //{
                //    objAppDto.Created_By = UserManager.GetLoggedInUser().User_Id;
                //    objAppDto.Created_dt = DateTime.Now;
                //}

                //objAppDto.Updated_By = Convert.ToString(UserManager.GetLoggedInUser().User_Id);
                //objAppDto.Updated_dt = DateTime.Now;
                //objAppDto.Ip_Address = Request.UserHostAddress;

                int Id = apl_mgr.SaveApplication(objAppDto);
                var fullname = FIRST_NAME.Value + " " + MIDDLE_NAME.Value + " " + FAMILY_NAME.Value;
                var email = EMAIL_ID.Value;

                string Application_NO = "";
                string AppHashNum = "";
                Application_DTO objTemp = apl_mgr.GetApplicationById(Id);
                if (objTemp != null)
                {
                    Application_NO = objTemp.APPLICATION_NO;
                    AppHashNum = Application_NO.Encrypt();
                    Session["AppHashNum"] = AppHashNum;
                }
                Mailfunction(AppHashNum, fullname, email);

                Response.Redirect("ApplicationSuccess.aspx", true);

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Constants.Errorapp + " \\n Error Message:" + Constants.GeneralErrorMessage + "');", true);

            }
        }

        protected void ddlCurrCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            CardManager cdmgr = new CardManager();
            if (!string.IsNullOrEmpty(ddlCurrCountry.SelectedValue) && Citylist != null)
            {
                ddlCurrCity.DataSource = Citylist.Where(i => i.COUNTRY_CODE.ToLower() == ddlCurrCountry.SelectedValue.ToLower()).ToList();
                ddlCurrCity.DataTextField = "CITY_NAME";
                ddlCurrCity.DataValueField = "CITY_CODE";
                ddlCurrCity.DataBind();
            }
            ddlCurrCity.Items.Insert(0, new ListItem(Constants.DDLcity, "-1"));
        }

        protected void ddlPermCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            CardManager cdmgr = new CardManager();
            if (!string.IsNullOrEmpty(ddlPermCountry.SelectedValue) && Citylist != null)
            {
                ddlPermCity.DataSource = Citylist.Where(i => i.COUNTRY_CODE.ToLower() == ddlPermCountry.SelectedValue.ToLower()).ToList();
                ddlPermCity.DataTextField = "CITY_NAME";
                ddlPermCity.DataValueField = "CITY_CODE";
                ddlPermCity.DataBind();
            }
            ddlPermCity.Items.Insert(0, new ListItem(Constants.DDLcity, "-1"));
        }

        protected void ddlEmpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            CardManager cdmgr = new CardManager();
            if (!string.IsNullOrEmpty(ddlEmpCountry.SelectedValue) && Citylist != null)
            {
                ddlEmpCity.DataSource = Citylist.Where(i => i.COUNTRY_CODE.ToLower() == ddlEmpCountry.SelectedValue.ToLower()).ToList();
                ddlEmpCity.DataTextField = "CITY_NAME";
                ddlEmpCity.DataValueField = "CITY_CODE";
                ddlEmpCity.DataBind();
            }
            ddlEmpCity.Items.Insert(0, new ListItem(Constants.DDLcity, "-1"));

        }

        protected void ddlRelCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            CardManager cdmgr = new CardManager();
            if (!string.IsNullOrEmpty(ddlRelcountry.SelectedValue) && Citylist != null)
            {
                ddlRelcity.DataSource = Citylist.Where(i => i.COUNTRY_CODE.ToLower() == ddlRelcountry.SelectedValue.ToLower()).ToList();
                ddlRelcity.DataTextField = "CITY_NAME";
                ddlRelcity.DataValueField = "CITY_CODE";
                ddlRelcity.DataBind();
            }
            ddlRelcity.Items.Insert(0, new ListItem(Constants.DDLcity, "-1"));

        }

        protected void ddlRel2Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            CardManager cdmgr = new CardManager();
            if (!string.IsNullOrEmpty(ddlRel2Country.SelectedValue) && Citylist != null)
            {
                ddlrel2city.DataSource = Citylist.Where(i => i.COUNTRY_CODE.ToLower() == ddlRel2Country.SelectedValue.ToLower()).ToList();
                ddlrel2city.DataTextField = "CITY_NAME";
                ddlrel2city.DataValueField = "CITY_CODE";
                ddlrel2city.DataBind();
            }
            ddlrel2city.Items.Insert(0, new ListItem(Constants.DDLcity, "-1"));

        }

        private void GetBranchlist()
        {
            List<Bank_MstDTO> lstbankDTO = new List<Bank_MstDTO>();
            CardManager cdmgr = new CardManager();
            lstbankDTO = cdmgr.GetBranchList();

            if (lstbankDTO != null && lstbankDTO.Count > 0)
            {
                ddlBranchlist.DataSource = lstbankDTO;
                ddlBranchlist.DataTextField = "BRANCH_NAME";
                ddlBranchlist.DataValueField = "BRANCH_CODE";
                ddlBranchlist.DataBind();


                ddlAccountBranch.DataSource = lstbankDTO;
                ddlAccountBranch.DataTextField = "BRANCH_NAME";
                ddlAccountBranch.DataValueField = "BRANCH_CODE";
                ddlAccountBranch.DataBind();

                ddlRecommendedBranch.DataSource = lstbankDTO;
                ddlRecommendedBranch.DataTextField = "BRANCH_NAME";
                ddlRecommendedBranch.DataValueField = "BRANCH_CODE";
                ddlRecommendedBranch.DataBind();

                ddlDebitBranchList.DataSource = lstbankDTO;
                ddlDebitBranchList.DataTextField = "BRANCH_NAME";
                ddlDebitBranchList.DataValueField = "BRANCH_CODE";
                ddlDebitBranchList.DataBind();
            }
            ddlBranchlist.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));
            ddlAccountBranch.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));
            ddlRecommendedBranch.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));
            ddlDebitBranchList.Items.Insert(0, new ListItem(Constants.DDLBranch, "-1"));

        }

        private void GetCountryList()
        {
            CardManager cdmgr = new CardManager();
            List<SYSCodeDTO> lstcountry = new List<SYSCodeDTO>();
            ddlCurrCountry.Items.Clear();
            ddlPermCountry.Items.Clear();
            ddlEmpCountry.Items.Clear();
            lstcountry = cdmgr.GetListOfCountry();

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
            ddlCurrCountry.SelectedValue = "356";
            ddlPermCountry.SelectedValue = "356";
            ddlEmpCountry.SelectedValue = "356";
            ddlRelcountry.SelectedValue = "356";
            ddlRel2Country.SelectedValue = "356";
            ddlCurrCountry_SelectedIndexChanged(null, null);
            ddlPermCountry_SelectedIndexChanged(null, null);
            ddlEmpCountry_SelectedIndexChanged(null, null);
            ddlRelCountry_SelectedIndexChanged(null, null);
            ddlRel2Country_SelectedIndexChanged(null, null);
            //ddlCurrCountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            //ddlPermCountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            //ddlEmpCountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            //ddlRelcountry.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
            //ddlRel2Country.Items.Insert(0, new ListItem(Constants.DDLCountry, "-1"));
        }

        private void GetCityList()
        {
            CardManager cdmgr = new CardManager();
            // Citylist = new List<SYSCodeDTO>() ;
            Citylist = cdmgr.GetListOfCity();
        }

        /// <summary>
        /// Loads the card types.
        /// </summary>
        private void BindCardTypes()
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            var list = dhm.SearchDllHeader("Card_Types").ToList();
            if (list.Count > 0)
            {
                ddlCardType.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlCardType.DataTextField = "Description";
                ddlCardType.DataValueField = "Description";
                ddlCardType.DataBind();

                ddlDIRECT_DEBIT_CARDTYPE.DataSource = dhm.SearchDllDetail(list[0].DropDown_Hdr_Id);
                ddlDIRECT_DEBIT_CARDTYPE.DataTextField = "Description";
                ddlDIRECT_DEBIT_CARDTYPE.DataValueField = "Description";
                ddlDIRECT_DEBIT_CARDTYPE.DataBind();

                ddlCardType.Items.Insert(0, new ListItem("---Select---", "-1"));
                ddlDIRECT_DEBIT_CARDTYPE.Items.Insert(0, new ListItem("---Select---", "-1"));
            }
            else
            {
                //lblMessage.Text = Constants.CardTypeNotfound;
            }
        }

        private void Onload()
        {
            //MonthYear();
            //GetBranchlist();
            //GetCountryList();
            //GetCityList();
            //List<SYSCodeDTO> lstsyscodeDTO = new List<SYSCodeDTO>();
            //BindSYSCodeList(ref lstsyscodeDTO);

            //GetVIPlist(lstsyscodeDTO);
            //GetSocialStatusList(lstsyscodeDTO);
            //GetTittleFromSysCode(lstsyscodeDTO);
            //GetMaritialStatusFromSysCode(lstsyscodeDTO);
            //GetResidenceChangedList(lstsyscodeDTO);
            //BindResidenceStatus(lstsyscodeDTO);
            //BindEducationDetails(lstsyscodeDTO);
            //BindEmployementStatus(lstsyscodeDTO);
            //BindEmployeeType(lstsyscodeDTO);
            //BindApplicantProfession(lstsyscodeDTO);
            //BindApplicantDesignation(lstsyscodeDTO);
            //BindOwnedVehicle(lstsyscodeDTO);
            //BindIncomePerMonth(lstsyscodeDTO);
            //GetBankListSysCode(lstsyscodeDTO);
            //BindRelation(lstsyscodeDTO);
            //GetNomiRelListSysCode(lstsyscodeDTO);

            BindCardTypes();
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

        private void BindSYSCodeList(ref List<SYSCodeDTO> lstsyscodeDTO)
        {
            CardManager cdmgr = new CardManager();
            lstsyscodeDTO = cdmgr.GetListOfSyscode();
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

        private void GetSocialStatusList(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //List<SYSCodeDTO> lstsyscodeDTO = new List<SYSCodeDTO>();
            //CardManager cdmgr = new CardManager();
            //lstsyscodeDTO = cdmgr.GetListOfSyscode(Constants.SOCIAL_STATUS);
            ddlSocialStatus.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlSocialStatus.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.SOCIAL_STATUS.ToLower()).ToList();
                ddlSocialStatus.DataTextField = "SHORT_NAME";
                ddlSocialStatus.DataValueField = "CODE";
                ddlSocialStatus.DataBind();
            }
            ddlSocialStatus.Items.Insert(0, new ListItem(Constants.DDLSocialStatus, "-1"));
        }

        private void GetTittleFromSysCode(List<SYSCodeDTO> lstsyscodeDTO)
        {
            ddlTitle.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlTitle.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.TITLE.ToLower()).ToList();
                ddlTitle.DataTextField = "SHORT_NAME";
                ddlTitle.DataValueField = "CODE";
                ddlTitle.DataBind();
            }
            ddlTitle.Items.Insert(0, new ListItem(Constants.DDLtitle, "-1"));
        }

        private void GetMaritialStatusFromSysCode(List<SYSCodeDTO> lstsyscodeDTO)
        {
            ddlMaritalStatus.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlMaritalStatus.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.MARITAL_STATUS.ToLower()).ToList();
                ddlMaritalStatus.DataTextField = "SHORT_NAME";
                ddlMaritalStatus.DataValueField = "CODE";
                ddlMaritalStatus.DataBind();
            }
            ddlMaritalStatus.Items.Insert(0, new ListItem(Constants.DDLMaritalStatus, "-1"));
        }

        private void GetResidenceChangedList(List<SYSCodeDTO> lstsyscodeDTO)
        {
            ddlchangeResi.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlchangeResi.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.RESIDANCE_CHANGED.ToLower()).ToList();
                ddlchangeResi.DataTextField = "SHORT_NAME";
                ddlchangeResi.DataValueField = "CODE";
                ddlchangeResi.DataBind();
            }

            ddlchangeResi.Items.Insert(0, new ListItem(Constants.DDLResidance, "-1"));
        }

        private void BindResidenceStatus(List<SYSCodeDTO> lstsyscodeDTO)
        {
            radResiSatus.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                radResiSatus.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.RESIDENCE_STATUS.ToLower()).ToList();
                radResiSatus.DataTextField = "SHORT_NAME";
                radResiSatus.DataValueField = "CODE";
                radResiSatus.DataBind();
            }
        }

        private void BindEducationDetails(List<SYSCodeDTO> lstsyscodeDTO)
        {
            ddlEducation.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlEducation.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.EDUCATION.ToLower()).ToList();
                ddlEducation.DataTextField = "SHORT_NAME";
                ddlEducation.DataValueField = "CODE";
                ddlEducation.DataBind();
            }
            ddlEducation.Items.Insert(0, new ListItem(Constants.DDLEducation, "-1"));
        }

        private void BindEmployementStatus(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //EMPLOYMENT_STATUS
            chkEmpStatus.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                chkEmpStatus.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.EMPLOYMENT_STATUS.ToLower()).ToList();
                chkEmpStatus.DataTextField = "SHORT_NAME";
                chkEmpStatus.DataValueField = "CODE";
                chkEmpStatus.DataBind();
            }


        }

        private void BindEmployeeType(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //COMP_TYPE
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlempType.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.COMP_TYPE.ToLower()).ToList();
                ddlempType.DataTextField = "SHORT_NAME";
                ddlempType.DataValueField = "CODE";
                ddlempType.DataBind();
            }
            ddlempType.Items.Insert(0, new ListItem(Constants.DDLEmpType, "-1"));

        }

        private void BindApplicantProfession(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //APPLICANT_PROF
            chkEmpProfession.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                chkEmpProfession.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.APPLICANT_PROF.ToLower()).ToList();
                chkEmpProfession.DataTextField = "SHORT_NAME";
                chkEmpProfession.DataValueField = "CODE";
                chkEmpProfession.DataBind();

                SEC2_APPLICANT_PROF.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.APPLICANT_PROF.ToLower()).ToList();
                SEC2_APPLICANT_PROF.DataTextField = "SHORT_NAME";
                SEC2_APPLICANT_PROF.DataValueField = "CODE";
                SEC2_APPLICANT_PROF.DataBind();

                SEC1_APPLICANT_PROF.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.APPLICANT_PROF.ToLower()).ToList();
                SEC1_APPLICANT_PROF.DataTextField = "SHORT_NAME";
                SEC1_APPLICANT_PROF.DataValueField = "CODE";
                SEC1_APPLICANT_PROF.DataBind();
            }
            SEC1_APPLICANT_PROF.Items.Insert(0, new ListItem(Constants.DDLResidance, "-1"));
            SEC2_APPLICANT_PROF.Items.Insert(0, new ListItem(Constants.DDLResidance, "-1"));
        }

        private void BindApplicantDesignation(List<SYSCodeDTO> lstsyscodeDTO)
        {
            chkdesignation.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                chkdesignation.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.EMPL_DESIGNATION.ToLower()).ToList();
                chkdesignation.DataTextField = "SHORT_NAME";
                chkdesignation.DataValueField = "CODE";
                chkdesignation.DataBind();
            }
        }

        private void BindOwnedVehicle(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //OWNED_VEHICLE_TYPE
            chkOwnedVehicle.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                chkOwnedVehicle.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.OWNED_VEHICLE_TYPE.ToLower()).ToList();
                chkOwnedVehicle.DataTextField = "SHORT_NAME";
                chkOwnedVehicle.DataValueField = "CODE";
                chkOwnedVehicle.DataBind();
            }
        }

        private void BindIncomePerMonth(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //INCOME_PER_MONTH
            radIncomePerMonth.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                radIncomePerMonth.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.INCOME_PER_MONTH.ToLower()).ToList();
                radIncomePerMonth.DataTextField = "SHORT_NAME";
                radIncomePerMonth.DataValueField = "CODE";
                radIncomePerMonth.DataBind();
            }
        }

        private void GetBankListSysCode(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //ACC_IN_BANK
            ddlIsAccountWithbank.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlIsAccountWithbank.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.ACC_IN_BANK.ToLower()).ToList();
                ddlIsAccountWithbank.DataTextField = "SHORT_NAME";
                ddlIsAccountWithbank.DataValueField = "CODE";
                ddlIsAccountWithbank.DataBind();
            }
            ddlIsAccountWithbank.Items.Insert(0, new ListItem(Constants.DDLBankName, "-1"));
        }

        private void BindRelation(List<SYSCodeDTO> lstsyscodeDTO)
        {
            radAddonRelation.ClearSelection();
            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                radAddonRelation.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.RELATION.ToLower()).ToList();
                radAddonRelation.DataTextField = "SHORT_NAME";
                radAddonRelation.DataValueField = "CODE";
                radAddonRelation.DataBind();


                radAddon1Relation.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.RELATION.ToLower()).ToList();
                radAddon1Relation.DataTextField = "SHORT_NAME";
                radAddon1Relation.DataValueField = "CODE";
                radAddon1Relation.DataBind();

                Rad3AddonRelation.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.RELATION.ToLower()).ToList();
                Rad3AddonRelation.DataTextField = "SHORT_NAME";
                Rad3AddonRelation.DataValueField = "CODE";
                Rad3AddonRelation.DataBind();
            }
        }

        private void GetNomiRelListSysCode(List<SYSCodeDTO> lstsyscodeDTO)
        {
            //ACC_IN_BANK
            ddlNomiRelation.Items.Clear();

            if (lstsyscodeDTO != null && lstsyscodeDTO.Count > 0)
            {
                ddlNomiRelation.DataSource = lstsyscodeDTO.Where(x => x.TYPE_ID.ToLower() == Constants.NOMINEE_RELATION.ToLower()).ToList();
                ddlNomiRelation.DataTextField = "SHORT_NAME";
                ddlNomiRelation.DataValueField = "CODE";
                ddlNomiRelation.DataBind();
            }
            ddlNomiRelation.Items.Insert(0, new ListItem(Constants.DDLNomineeRel, "-1"));
        }

        private void MonthYear()
        {
            var next15Yrs = new List<string>();
            int currentYear = DateTime.Now.Year;
            int expiryYear = Convert.ToInt32(_expiryYear);
            for (int i = currentYear; i <= currentYear + expiryYear; i++)
            {
                string j = Convert.ToString(i);
                next15Yrs.Add(j);
            }
            //databind here

            DC_VALID_UPTO_YEAR.DataSource = next15Yrs;
            DC_VALID_UPTO_YEAR.DataBind();
            DC_VALID_UPTO_YEAR.Items.Insert(0, new ListItem("YY", "-1"));

            CC_VALID_UPTO1_YEAR.DataSource = next15Yrs;
            CC_VALID_UPTO1_YEAR.DataBind();
            CC_VALID_UPTO1_YEAR.Items.Insert(0, new ListItem("YY", "-1"));

            CC_VALID_UPTO2_YEAR.DataSource = next15Yrs;
            CC_VALID_UPTO2_YEAR.DataBind();
            CC_VALID_UPTO2_YEAR.Items.Insert(0, new ListItem("YY", "-1"));

            CC_VALID_UPTO3_YEAR.DataSource = next15Yrs;
            CC_VALID_UPTO3_YEAR.DataBind();
            CC_VALID_UPTO3_YEAR.Items.Insert(0, new ListItem("YY", "-1"));
        }

        private void Mailfunction(string applicationnumber, string fullname, string email)
        {
            string bobMail = ConfigurationManager.AppSettings["BOB_EMAIL"];
            string emailSubject = ConfigurationManager.AppSettings["APP_EMAIL_SUBJECT"];
            string overRideEmail = ConfigurationManager.AppSettings["OverRideUserEmail"];
            const long cardHolderId = 0;
            if (!string.IsNullOrEmpty(overRideEmail))
                email = overRideEmail;

            try
            {
                var bodyString = new StringBuilder();
                bodyString.Append(System.IO.File.ReadAllText(Server.MapPath("") + Constants.ATM_PIN_RegenerationTemplatepath));
                bodyString.Replace("@@Fullname", fullname);
                bodyString.Replace("@@APPLICATIONNUM", applicationnumber.Decrypt());
                bodyString.Replace("@@ImagePath", UrlHelper.GetAbsoluteUri() + "/images/mailer-banner.jpg");
                var cCemail = new List<string>();
                bool IsMailSent = SendMailfunction.SendMail(bobMail, new List<string>() { email }, cCemail, "", "", emailSubject, bodyString.ToString(), true, cardHolderId);

                if (IsMailSent)
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ErrorMailButRqstLogged + "');", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.ErrorMailButRqstLogged + "');", true);
            }
        }

        [WebMethod]
        // public static List<SYSCodeDTO> ddlPermCityRecord(string CountryValue)
        public static string ddlPermCityRecord(string CountryValue)
        {
            CardManager cdmgr = new CardManager();
            List<SYSCodeDTO> CityList = cdmgr.GetListOfCity();
            CityList = CityList.Where(i => i.COUNTRY_CODE.ToLower() == CountryValue).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer objJSSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //Serialization .NET Object to JSON
            string strJSON = objJSSerializer.Serialize(CityList);
            // return CityList;
            return strJSON;
        }
    }
}