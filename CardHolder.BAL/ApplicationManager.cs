using CardHolder.DAL;
using CardHolder.DAL.Interface;
using CardHolder.DTO;
using StructureMap;
using System.Reflection;
using System.Data.Linq.Mapping;
using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class ApplicationManager
    {
        /// <summary>
        /// Gets the rep application MST.
        /// </summary>
        /// <remarks></remarks>
        private static IRepository<Application> RepApplicationMst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Application>>();
            }
        }


        /// <summary>
        /// Save card holder application
        /// </summary>
        /// <param name="objApplicationDto">The obj application dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int SaveApplication(Application_DTO objApplicationDto)
        {
            Application obj = RepApplicationMst.SingleOrDefault(c => c.ID == objApplicationDto.ID);

            if (obj == null || obj.ID != objApplicationDto.ID)
            {
                obj = new Application();
            }


            obj.SOURCE_APPLICATION_NO = objApplicationDto.SOURCE_APPLICATION_NO;
            obj.SOURCE_TYPE = objApplicationDto.SOURCE_TYPE;
            obj.SOURCE_CODE = objApplicationDto.SOURCE_CODE;
            obj.BANK_CODE = objApplicationDto.BANK_CODE;
            obj.PRODUCT_CODE = objApplicationDto.PRODUCT_CODE;
            obj.VIP_CODE = objApplicationDto.VIP_CODE;
            obj.SOCIAL_STATUS = objApplicationDto.SOCIAL_STATUS;
            obj.BASIC_CARD_FLAG = objApplicationDto.BASIC_CARD_FLAG;
            obj.PRIMARY_CARD_NUMBER = objApplicationDto.PRIMARY_CARD_NUMBER;
            obj.CLIENT_ID_OF_COMPANY = objApplicationDto.CLIENT_ID_OF_COMPANY;
            obj.TITLE = objApplicationDto.TITLE;
            obj.FAMILY_NAME = objApplicationDto.FAMILY_NAME;
            obj.FIRST_NAME = objApplicationDto.FIRST_NAME;
            obj.EMBOSSED_NAME = objApplicationDto.EMBOSSED_NAME;
            obj.BIRTH_DATE = objApplicationDto.BIRTH_DATE;
            obj.BIRTH_CITY = objApplicationDto.BIRTH_CITY;
            obj.BIRTH_COUNTRY = objApplicationDto.BIRTH_COUNTRY;
            obj.GENDER = objApplicationDto.GENDER;
            obj.MARITAL_STATUS = objApplicationDto.MARITAL_STATUS;
            obj.NO_OF_DEPENDENTS = objApplicationDto.NO_OF_DEPENDENTS;
            obj.NATIONALITY = objApplicationDto.NATIONALITY;
            obj.PERM_ADDRESS1 = objApplicationDto.PERM_ADDRESS1;
            obj.PERM_ADDRESS2 = objApplicationDto.PERM_ADDRESS2;
            obj.PERM_ADDRESS3 = objApplicationDto.PERM_ADDRESS3;
            obj.PERM_ADDRESS4 = objApplicationDto.PERM_ADDRESS4;
            obj.PERM_POSTAL_CODE = objApplicationDto.PERM_POSTAL_CODE;
            obj.PERM_CITY_CODE = objApplicationDto.PERM_CITY_CODE;
            obj.PERM_COUNTRY_CODE = objApplicationDto.PERM_COUNTRY_CODE;
            obj.RESIDENCE_STATUS = objApplicationDto.RESIDENCE_STATUS;
            obj.CURR_ADDRESS1 = objApplicationDto.CURR_ADDRESS1;
            obj.CURR_ADDRESS2 = objApplicationDto.CURR_ADDRESS2;
            obj.CURR_ADDRESS3 = objApplicationDto.CURR_ADDRESS3;
            obj.CURR_ADDRESS4 = objApplicationDto.CURR_ADDRESS4;
            obj.CURR_POSTAL_CODE = objApplicationDto.CURR_POSTAL_CODE;
            obj.CURR_CITY_CODE = objApplicationDto.CURR_CITY_CODE;
            obj.CURR_COUNTRY_CODE = objApplicationDto.CURR_COUNTRY_CODE;
            obj.HOME_PHONE_NUMBER = objApplicationDto.HOME_PHONE_NUMBER;
            obj.OFFICE_PHONE_NUMBER = objApplicationDto.OFFICE_PHONE_NUMBER;
            obj.MOBILE_NUMBER = objApplicationDto.MOBILE_NUMBER;
            obj.EMAIL_ID = objApplicationDto.EMAIL_ID;
            obj.EDUCATION = objApplicationDto.EDUCATION;
            obj.EMPLOYMENT_STATUS = objApplicationDto.EMPLOYMENT_STATUS;
            obj.COMP_TYPE = objApplicationDto.COMP_TYPE;
            obj.APPLICANT_PROF = objApplicationDto.APPLICANT_PROF;
            obj.EMPLOYER_NAME = objApplicationDto.EMPLOYER_NAME;
            obj.EMPL_ADDRESS1 = objApplicationDto.EMPL_ADDRESS1;
            obj.EMPL_ADDRESS2 = objApplicationDto.EMPL_ADDRESS2;
            obj.EMPL_ADDRESS3 = objApplicationDto.EMPL_ADDRESS3;
            obj.EMPL_ADDRESS4 = objApplicationDto.EMPL_ADDRESS4;
            obj.EMPL_POSTAL_CODE = objApplicationDto.EMPL_POSTAL_CODE;
            obj.EMPL_CITY_CODE = objApplicationDto.EMPL_CITY_CODE;
            obj.EMPL_COUNTRY_CODE = objApplicationDto.EMPL_COUNTRY_CODE;
            obj.EXISTING_CREDIT_CARD_NUMBER = objApplicationDto.EXISTING_CREDIT_CARD_NUMBER;
            obj.ACC_IN_BANK = objApplicationDto.ACC_IN_BANK;
            obj.ACCOUNT_BRANCH = objApplicationDto.ACCOUNT_BRANCH;
            obj.ACCOUNT_NUMBER = objApplicationDto.ACCOUNT_NUMBER;
            obj.OWNED_VEHICLE_TYPE = objApplicationDto.OWNED_VEHICLE_TYPE;
            obj.ANNUAL_INCOME_CODE = objApplicationDto.ANNUAL_INCOME_CODE;
            obj.INSURANCE_NOM_NAME = objApplicationDto.INSURANCE_NOM_NAME;
            obj.INSURANCE_NOM_REL = objApplicationDto.INSURANCE_NOM_REL;
            obj.PAN_GIR_NO = objApplicationDto.PAN_GIR_NO;
            obj.PASSPORT_NUMBER = objApplicationDto.PASSPORT_NUMBER;
            obj.DRIVING_LICENSE_NUMBER = objApplicationDto.DRIVING_LICENSE_NUMBER;
            obj.PASSPORT_PLACE_OF_ISSUE = objApplicationDto.PASSPORT_PLACE_OF_ISSUE;
            obj.ADDITIONAL_CARD_NAME = objApplicationDto.ADDITIONAL_CARD_NAME;
            obj.ADDITIONAL_CARD_EMB_NAME = objApplicationDto.ADDITIONAL_CARD_EMB_NAME;
            obj.ADDITIONAL_CARD_REL = objApplicationDto.ADDITIONAL_CARD_REL;
            obj.APPLICATION_STATUS = objApplicationDto.APPLICATION_STATUS;
            obj.CLIENT_CODE = objApplicationDto.CLIENT_CODE;
            obj.CARD_NUMBER = objApplicationDto.CARD_NUMBER;
            obj.BATCH_NUMBER = objApplicationDto.BATCH_NUMBER;
            obj.RISK_STATUS = objApplicationDto.RISK_STATUS;
            obj.CREDIT_LIMIT = objApplicationDto.CREDIT_LIMIT;
            obj.CURRENCY_CODE = objApplicationDto.CURRENCY_CODE;
            obj.APP_PROCESS_DATE = objApplicationDto.APP_PROCESS_DATE;
            obj.SCORE = objApplicationDto.SCORE;
            obj.AGE = objApplicationDto.AGE;
            obj.REMARKS = objApplicationDto.REMARKS;
            obj.REASON_CODE = objApplicationDto.REASON_CODE;
            obj.USER_CREATE = objApplicationDto.USER_CREATE;
            obj.DATE_CREATE = objApplicationDto.DATE_CREATE;
            obj.USER_MODIF = objApplicationDto.USER_MODIF;
            obj.DATE_MODIF = objApplicationDto.DATE_MODIF;
            obj.USER_DELETE = objApplicationDto.USER_DELETE;
            obj.DATE_DELETE = objApplicationDto.DATE_DELETE;
            obj.LANGUAGE_PREF = objApplicationDto.LANGUAGE_PREF;
            obj.MAIDEN_NAME = objApplicationDto.MAIDEN_NAME;
            obj.PREFERENCES = objApplicationDto.PREFERENCES;
            obj.SEC_CARD_NUMBER = objApplicationDto.SEC_CARD_NUMBER;
            obj.CR_ACCOUNT_NBR = objApplicationDto.CR_ACCOUNT_NBR;
            obj.PHOTOCARD_APP_IND = objApplicationDto.PHOTOCARD_APP_IND;
            obj.BT_IND = objApplicationDto.BT_IND;
            obj.BT_BANK = objApplicationDto.BT_BANK;
            obj.BT_CARD = objApplicationDto.BT_CARD;
            obj.BT_AMOUNT = objApplicationDto.BT_AMOUNT;
            obj.BT_INTEREST_RATE = objApplicationDto.BT_INTEREST_RATE;
            obj.SEC_BIRTH_DATE = objApplicationDto.SEC_BIRTH_DATE;
            obj.USER_VERIF = objApplicationDto.USER_VERIF;
            obj.DATE_VERIFY = objApplicationDto.DATE_VERIFY;
            obj.USER_APPROVED = objApplicationDto.USER_APPROVED;
            obj.DATE_APPROVED = objApplicationDto.DATE_APPROVED;
            obj.USER_APPROVED_SATYAM = objApplicationDto.USER_APPROVED_SATYAM;
            obj.DATE_APPROVED_SATYAM = objApplicationDto.DATE_APPROVED_SATYAM;
            obj.SATYAM_FILE = objApplicationDto.SATYAM_FILE;
            obj.ADDITIONAL_FAMILY_NAME = objApplicationDto.ADDITIONAL_FAMILY_NAME;
            obj.EMBOSSED_LINE_4 = objApplicationDto.EMBOSSED_LINE_4;
            obj.CASH_LIMIT = objApplicationDto.CASH_LIMIT;
            obj.SEC_TITLE = objApplicationDto.SEC_TITLE;
            obj.SPOUSE_NAME = objApplicationDto.SPOUSE_NAME;
            obj.SPOUSE_INCOME = objApplicationDto.SPOUSE_INCOME;
            obj.PERMNANT_ADDRESS_TENURE = objApplicationDto.PERMNANT_ADDRESS_TENURE;
            obj.MAILING_ADDRESS_TENURE = objApplicationDto.MAILING_ADDRESS_TENURE;
            obj.PASSWORD = objApplicationDto.PASSWORD;
            obj.EMPLOYER_BUSINESS_NATURE = objApplicationDto.EMPLOYER_BUSINESS_NATURE;
            obj.CURRENT_JOB_TENURE = objApplicationDto.CURRENT_JOB_TENURE;
            obj.PREVIOUS_EMPLOYER_NAME = objApplicationDto.PREVIOUS_EMPLOYER_NAME;
            obj.PREVIOUS_JOB_TENURE = objApplicationDto.PREVIOUS_JOB_TENURE;
            obj.WORKPLACE = objApplicationDto.WORKPLACE;
            obj.OUR_ACCOUNT_TYPE = objApplicationDto.OUR_ACCOUNT_TYPE;
            obj.OUR_ACCOUNT_TENURE = objApplicationDto.OUR_ACCOUNT_TENURE;
            obj.OUR_ACCOUNT_STATUS = objApplicationDto.OUR_ACCOUNT_STATUS;
            obj.OUR_BANK_ACC_AVERAGE_BALANCES = objApplicationDto.OUR_BANK_ACC_AVERAGE_BALANCES;
            obj.OTHER_BANK_NAME = objApplicationDto.OTHER_BANK_NAME;
            obj.OTHER_ACCOUNT_TYPE = objApplicationDto.OTHER_ACCOUNT_TYPE;
            obj.OTHER_ACCOUNT_NUMBER = objApplicationDto.OTHER_ACCOUNT_NUMBER;
            obj.OTHER_ACCOUNT_TENURE = objApplicationDto.OTHER_ACCOUNT_TENURE;
            obj.OTHER_ACCOUNT_STATUS = objApplicationDto.OTHER_ACCOUNT_STATUS;
            obj.OTHER_BANK_ACC_AVG_BALANCES = objApplicationDto.OTHER_BANK_ACC_AVG_BALANCES;
            obj.HOME_ESTIMATED_CURRENT_VALUE = objApplicationDto.HOME_ESTIMATED_CURRENT_VALUE;
            obj.HOME_CURR_MORTGAGE_OUTSTANDING = objApplicationDto.HOME_CURR_MORTGAGE_OUTSTANDING;
            obj.HOME_MONTHLY_MORTGAGE_REPAY = objApplicationDto.HOME_MONTHLY_MORTGAGE_REPAY;
            obj.HSG_LENDER_NAME = objApplicationDto.HSG_LENDER_NAME;
            obj.HSG_LENDER_ADDRESS1 = objApplicationDto.HSG_LENDER_ADDRESS1;
            obj.HSG_LENDER_ADDRESS2 = objApplicationDto.HSG_LENDER_ADDRESS2;
            obj.HSG_LENDER_ADDRESS3 = objApplicationDto.HSG_LENDER_ADDRESS3;
            obj.HSG_LENDER_ADDRESS4 = objApplicationDto.HSG_LENDER_ADDRESS4;
            obj.HSG_LENDER_ZIP_CODE = objApplicationDto.HSG_LENDER_ZIP_CODE;
            obj.HSG_LENDER_CITY_CODE = objApplicationDto.HSG_LENDER_CITY_CODE;
            obj.HSG_LENDER_COUNTRY_CODE = objApplicationDto.HSG_LENDER_COUNTRY_CODE;
            obj.HSG_LENDER_PHONE_NUMBER = objApplicationDto.HSG_LENDER_PHONE_NUMBER;
            obj.HSG_LENDER_ACCOUNT_NUMBER = objApplicationDto.HSG_LENDER_ACCOUNT_NUMBER;
            obj.HOME_MONTHLY_RENT = objApplicationDto.HOME_MONTHLY_RENT;
            obj.OTHER_LOAN = objApplicationDto.OTHER_LOAN;
            obj.OTHER_LOAN_DESC = objApplicationDto.OTHER_LOAN_DESC;
            obj.OTHER_LENDER1_NAME = objApplicationDto.OTHER_LENDER1_NAME;
            obj.OTHER_LENDER1_ADDRESS1 = objApplicationDto.OTHER_LENDER1_ADDRESS1;
            obj.OTHER_LENDER1_ADDRESS2 = objApplicationDto.OTHER_LENDER1_ADDRESS2;
            obj.OTHER_LENDER1_ADDRESS3 = objApplicationDto.OTHER_LENDER1_ADDRESS3;
            obj.OTHER_LENDER1_ADDRESS4 = objApplicationDto.OTHER_LENDER1_ADDRESS4;
            obj.OTHER_LENDER1_ZIP_CODE = objApplicationDto.OTHER_LENDER1_ZIP_CODE;
            obj.OTHER_LENDER1_CITY_CODE = objApplicationDto.OTHER_LENDER1_CITY_CODE;
            obj.OTHER_LENDER1_COUNTRY_CODE = objApplicationDto.OTHER_LENDER1_COUNTRY_CODE;
            obj.OTHER_LENDER1_PHONE_NUMBER = objApplicationDto.OTHER_LENDER1_PHONE_NUMBER;
            obj.OTHER_LENDER1_MONTHLY_PAYMENT = objApplicationDto.OTHER_LENDER1_MONTHLY_PAYMENT;
            obj.OTHER_LENDER1_CURRENT_BALANCE = objApplicationDto.OTHER_LENDER1_CURRENT_BALANCE;
            obj.OTHER_LENDER2_NAME = objApplicationDto.OTHER_LENDER2_NAME;
            obj.OTHER_LENDER2_ADDRESS1 = objApplicationDto.OTHER_LENDER2_ADDRESS1;
            obj.OTHER_LENDER2_ADDRESS2 = objApplicationDto.OTHER_LENDER2_ADDRESS2;
            obj.OTHER_LENDER2_ADDRESS3 = objApplicationDto.OTHER_LENDER2_ADDRESS3;
            obj.OTHER_LENDER2_ADDRESS4 = objApplicationDto.OTHER_LENDER2_ADDRESS4;
            obj.OTHER_LENDER2_ZIP_CODE = objApplicationDto.OTHER_LENDER2_ZIP_CODE;
            obj.OTHER_LENDER2_CITY_CODE = objApplicationDto.OTHER_LENDER2_CITY_CODE;
            obj.OTHER_LENDER2_COUNTRY_CODE = objApplicationDto.OTHER_LENDER2_COUNTRY_CODE;
            obj.OTHER_LENDER2_PHONE_NUMBER = objApplicationDto.OTHER_LENDER2_PHONE_NUMBER;
            obj.OTHER_LENDER2_MONTHLY_PAYMENT = objApplicationDto.OTHER_LENDER2_MONTHLY_PAYMENT;
            obj.OTHER_LENDER2_CURRENT_BALANCE = objApplicationDto.OTHER_LENDER2_CURRENT_BALANCE;
            obj.OTHER_CREDIT_CARDS_TYPE = objApplicationDto.OTHER_CREDIT_CARDS_TYPE;
            obj.OTHER_CREDIT_CARDS_COUNT = objApplicationDto.OTHER_CREDIT_CARDS_COUNT;
            obj.CARD1_ISSUED_BY = objApplicationDto.CARD1_ISSUED_BY;
            obj.CARD1_TENURE = objApplicationDto.CARD1_TENURE;
            obj.CARD1_REFERENCES = objApplicationDto.CARD1_REFERENCES;
            obj.CARD2_ISSUED_BY = objApplicationDto.CARD2_ISSUED_BY;
            obj.CARD2_NUMBER = objApplicationDto.CARD2_NUMBER;
            obj.CARD2_TENURE = objApplicationDto.CARD2_TENURE;
            obj.CARD2_REFERENCES = objApplicationDto.CARD2_REFERENCES;
            obj.MONTHLY_REPAYMENTS_TO_LOAN = objApplicationDto.MONTHLY_REPAYMENTS_TO_LOAN;
            obj.REF1_NAME = objApplicationDto.REF1_NAME;
            obj.REF1_ADDRESS1 = objApplicationDto.REF1_ADDRESS1;
            obj.REF1_ADDRESS2 = objApplicationDto.REF1_ADDRESS2;
            obj.REF1_ADDRESS3 = objApplicationDto.REF1_ADDRESS3;
            obj.REF1_ADDRESS4 = objApplicationDto.REF1_ADDRESS4;
            obj.REF1_ZIP_CODE = objApplicationDto.REF1_ZIP_CODE;
            obj.REF1_CITY_CODE = objApplicationDto.REF1_CITY_CODE;
            obj.REF1_COUNTRY_CODE = objApplicationDto.REF1_COUNTRY_CODE;
            obj.REF1_PHONE_NUMBER = objApplicationDto.REF1_PHONE_NUMBER;
            obj.REF2_NAME = objApplicationDto.REF2_NAME;
            obj.REF2_ADDRESS1 = objApplicationDto.REF2_ADDRESS1;
            obj.REF2_ADDRESS2 = objApplicationDto.REF2_ADDRESS2;
            obj.REF2_ADDRESS3 = objApplicationDto.REF2_ADDRESS3;
            obj.REF2_ADDRESS4 = objApplicationDto.REF2_ADDRESS4;
            obj.REF2_ZIP_CODE = objApplicationDto.REF2_ZIP_CODE;
            obj.REF2_CITY_CODE = objApplicationDto.REF2_CITY_CODE;
            obj.REF2_COUNTRY_CODE = objApplicationDto.REF2_COUNTRY_CODE;
            obj.REF2_PHONE_NUMBER = objApplicationDto.REF2_PHONE_NUMBER;
            obj.OTHER_REFERENCES = objApplicationDto.OTHER_REFERENCES;
            obj.SEC_PAN_GIR_NO = objApplicationDto.SEC_PAN_GIR_NO;
            obj.SEC_PASSPORT_NUMBER = objApplicationDto.SEC_PASSPORT_NUMBER;
            obj.SEC_PASSPORT_PLACE_OF_ISSUE = objApplicationDto.SEC_PASSPORT_PLACE_OF_ISSUE;
            obj.SEC_EMPLOYER_NAME = objApplicationDto.SEC_EMPLOYER_NAME;
            obj.SEC_TELEPHONE_NO = objApplicationDto.SEC_TELEPHONE_NO;
            obj.SEC_CREDIT_LIMIT = objApplicationDto.SEC_CREDIT_LIMIT;
            obj.DIRECT_DEBIT_FLAG = objApplicationDto.DIRECT_DEBIT_FLAG;
            obj.DIRECT_DEBIT_BRANCH = objApplicationDto.DIRECT_DEBIT_BRANCH;
            obj.DIRECT_DEBIT_ACCOUNT_NAME = objApplicationDto.DIRECT_DEBIT_ACCOUNT_NAME;
            obj.DIRECT_DEBIT_ACCOUNT_TYPE = objApplicationDto.DIRECT_DEBIT_ACCOUNT_TYPE;
            obj.DIRECT_DEBIT_ACCOUNT_NUMBER = objApplicationDto.DIRECT_DEBIT_ACCOUNT_NUMBER;
            obj.ACTUAL_JOB_TENURE = objApplicationDto.ACTUAL_JOB_TENURE;
            obj.OTHER_BANK_BRANCH = objApplicationDto.OTHER_BANK_BRANCH;
            obj.DIRECT_DEBIT_AMOUNT_FLAG = objApplicationDto.DIRECT_DEBIT_AMOUNT_FLAG;
            obj.CYCOFF_CODE = objApplicationDto.CYCOFF_CODE;
            obj.TARIFF_CODE = objApplicationDto.TARIFF_CODE;
            obj.PROFILE_CODE = objApplicationDto.PROFILE_CODE;
            obj.DIRECT_DEBIT_PERCENTAGE = objApplicationDto.DIRECT_DEBIT_PERCENTAGE;
            obj.MICR_CODE = objApplicationDto.MICR_CODE;
            obj.PREFERRED_MAILING_ADDRESS = objApplicationDto.PREFERRED_MAILING_ADDRESS;
            obj.PICTURE_CODE = objApplicationDto.PICTURE_CODE;
            obj.PROMO_CODE = objApplicationDto.PROMO_CODE;
            obj.CUSTOM_LYT_PLAN_ENABLE = objApplicationDto.CUSTOM_LYT_PLAN_ENABLE;
            obj.SEC_PHOTOCARD_APP_IND = objApplicationDto.SEC_PHOTOCARD_APP_IND;
            obj.SEC_PICTURE_CODE = objApplicationDto.SEC_PICTURE_CODE;
            obj.CHOICE_WELCOME_GIFT = objApplicationDto.CHOICE_WELCOME_GIFT;
            obj.I_AM_INTERESTED_IN = objApplicationDto.I_AM_INTERESTED_IN;
            obj.FAVOURITE_RESTURANT = objApplicationDto.FAVOURITE_RESTURANT;
            obj.MERCHANT_CAT_CODE_1 = objApplicationDto.MERCHANT_CAT_CODE_1;
            obj.PTS_DISTRIB_FOR_MCG_1 = objApplicationDto.PTS_DISTRIB_FOR_MCG_1;
            obj.MERCHANT_CAT_CODE_2 = objApplicationDto.MERCHANT_CAT_CODE_2;
            obj.PTS_DISTRIB_FOR_MCG_2 = objApplicationDto.PTS_DISTRIB_FOR_MCG_2;
            obj.MERCHANT_CAT_CODE_3 = objApplicationDto.MERCHANT_CAT_CODE_3;
            obj.PTS_DISTRIB_FOR_MCG_3 = objApplicationDto.PTS_DISTRIB_FOR_MCG_3;
            obj.MERCHANT_CAT_CODE_4 = objApplicationDto.MERCHANT_CAT_CODE_4;
            obj.PTS_DISTRIB_FOR_MCG_4 = objApplicationDto.PTS_DISTRIB_FOR_MCG_4;
            obj.MERCHANT_CAT_CODE_5 = objApplicationDto.MERCHANT_CAT_CODE_5;
            obj.PTS_DISTRIB_FOR_MCG_5 = objApplicationDto.PTS_DISTRIB_FOR_MCG_5;
            obj.MERCHANT_CAT_CODE_6 = objApplicationDto.MERCHANT_CAT_CODE_6;
            obj.PTS_DISTRIB_FOR_MCG_6 = objApplicationDto.PTS_DISTRIB_FOR_MCG_6;
            obj.MERCHANT_CAT_CODE_7 = objApplicationDto.MERCHANT_CAT_CODE_7;
            obj.PTS_DISTRIB_FOR_MCG_7 = objApplicationDto.PTS_DISTRIB_FOR_MCG_7;
            obj.MERCHANT_CAT_CODE_8 = objApplicationDto.MERCHANT_CAT_CODE_8;
            obj.PTS_DISTRIB_FOR_MCG_8 = objApplicationDto.PTS_DISTRIB_FOR_MCG_8;
            obj.MERCHANT_CAT_CODE_9 = objApplicationDto.MERCHANT_CAT_CODE_9;
            obj.PTS_DISTRIB_FOR_MCG_9 = objApplicationDto.PTS_DISTRIB_FOR_MCG_9;
            obj.MERCHANT_CAT_CODE_10 = objApplicationDto.MERCHANT_CAT_CODE_10;
            obj.PTS_DISTRIB_FOR_MCG_10 = objApplicationDto.PTS_DISTRIB_FOR_MCG_10;
            obj.MNAME = objApplicationDto.MNAME;
            obj.CHILD1_NAME = objApplicationDto.CHILD1_NAME;
            obj.CHILD1_DOB = objApplicationDto.CHILD1_DOB;
            obj.NCM_NAME_CHILD2 = objApplicationDto.NCM_NAME_CHILD2;
            obj.CHILD2_DOB = objApplicationDto.CHILD2_DOB;
            obj.NCM_DOB_CHILD2 = objApplicationDto.NCM_DOB_CHILD2;
            obj.VC_DEGREE = objApplicationDto.VC_DEGREE;
            obj.VC_UNIVERSITY = objApplicationDto.VC_UNIVERSITY;
            obj.LOS_TOTALYRSEXPERIENCE_N = objApplicationDto.LOS_TOTALYRSEXPERIENCE_N;
            obj.LOS_DESIGNATION_C = objApplicationDto.LOS_DESIGNATION_C;
            obj.ADDYEAR = objApplicationDto.ADDYEAR;
            obj.ALTERNATE_NUBMER = objApplicationDto.ALTERNATE_NUBMER;
            obj.PERM_ALTERNATE_NUBMER = objApplicationDto.PERM_ALTERNATE_NUBMER;
            obj.OFFICE_EMAIL = objApplicationDto.OFFICE_EMAIL;
            obj.OFFICE_MOBILE = objApplicationDto.OFFICE_MOBILE;
            obj.PAGER = objApplicationDto.PAGER;
            obj.TELEX = objApplicationDto.TELEX;
            obj.VEHICLE_OWNERSHIP = objApplicationDto.VEHICLE_OWNERSHIP;
            obj.VEHICLE_REG_NO = objApplicationDto.VEHICLE_REG_NO;
            obj.ISSUER_BANK1 = objApplicationDto.ISSUER_BANK1;
            obj.MEMBERSINCE = objApplicationDto.MEMBERSINCE;
            obj.CARD1_CREDIT_LIMIT = objApplicationDto.CARD1_CREDIT_LIMIT;
            obj.ISSUER_BANK2 = objApplicationDto.ISSUER_BANK2;
            obj.CARD2_MEMBER_SINCE = objApplicationDto.CARD2_MEMBER_SINCE;
            obj.CARD2_CREDIT_LIMIT = objApplicationDto.CARD2_CREDIT_LIMIT;
            obj.VC_BRANCH_CD = objApplicationDto.VC_BRANCH_CD;
            obj.VC_FEE_CD = objApplicationDto.VC_FEE_CD;
            obj.N_SMAC = objApplicationDto.N_SMAC;
            obj.PREF_EMAILID = objApplicationDto.PREF_EMAILID;
            obj.NCM_FIN_CUST_ID = objApplicationDto.NCM_FIN_CUST_ID;
            obj.NCM_ECS_CUST_ID = objApplicationDto.NCM_ECS_CUST_ID;
            obj.VC_EMPLOYMENT_STATUS = objApplicationDto.VC_EMPLOYMENT_STATUS;
            obj.LAA_CAMP_TYPE = objApplicationDto.LAA_CAMP_TYPE;
            obj.LOS_WHETHER_RESI_C = objApplicationDto.LOS_WHETHER_RESI_C;
            obj.PERM_ADDRESS_CONTACT_PERSON = objApplicationDto.PERM_ADDRESS_CONTACT_PERSON;
            obj.FAX = objApplicationDto.FAX;
            obj.LOS_NATUREOFCOMP_C = objApplicationDto.LOS_NATUREOFCOMP_C;
            obj.NCM_ADDON_GENDER = objApplicationDto.NCM_ADDON_GENDER;
            obj.BAL_TRANS_NAME = objApplicationDto.BAL_TRANS_NAME;
            obj.NEGATIVE_AREA = objApplicationDto.NEGATIVE_AREA;
            obj.CUST_PROFILE_FLAG = objApplicationDto.CUST_PROFILE_FLAG;
            obj.INCOME_DOC_STAT_FLAG = objApplicationDto.INCOME_DOC_STAT_FLAG;
            obj.APPL_SIGNED_FLAG = objApplicationDto.APPL_SIGNED_FLAG;
            obj.ISSUERNAME = objApplicationDto.ISSUERNAME;
            obj.RESIDENCETYPE = objApplicationDto.RESIDENCETYPE;
            obj.VC_DELIVERY_OP = objApplicationDto.VC_DELIVERY_OP;
            obj.VC_ADDON_PRODUCT = objApplicationDto.VC_ADDON_PRODUCT;
            obj.VC_BANK_NAME = objApplicationDto.VC_BANK_NAME;
            obj.N_AMOUNT = objApplicationDto.N_AMOUNT;
            obj.VC_ALIAS_NAME = objApplicationDto.VC_ALIAS_NAME;
            obj.FATHERNAME = objApplicationDto.FATHERNAME;
            obj.N_CARS_OWN = objApplicationDto.N_CARS_OWN;
            obj.SC_ST_FLAG = objApplicationDto.SC_ST_FLAG;
            obj.RENTPERMONTH = objApplicationDto.RENTPERMONTH;
            obj.CUST_ID_N = objApplicationDto.CUST_ID_N;
            obj.LIABILITY = objApplicationDto.LIABILITY;
            obj.STAFF_ID = objApplicationDto.STAFF_ID;
            obj.EL = objApplicationDto.EL;
            obj.EAD = objApplicationDto.EAD;
            obj.FEE_CODE = objApplicationDto.FEE_CODE;
            obj.PROB_DEFAULT = objApplicationDto.PROB_DEFAULT;
            obj.USER_DEF_FIELD1 = objApplicationDto.USER_DEF_FIELD1;
            obj.USER_DEF_FIELD2 = objApplicationDto.USER_DEF_FIELD2;
            obj.USER_DEF_FIELD3 = objApplicationDto.USER_DEF_FIELD3;
            obj.USER_DEF_FIELD4 = objApplicationDto.USER_DEF_FIELD4;
            obj.USER_DEF_FIELD5 = objApplicationDto.USER_DEF_FIELD5;
            obj.EMPL_DESIGNATION = objApplicationDto.EMPL_DESIGNATION;
            obj.EMPL_DEPARTMENT = objApplicationDto.EMPL_DEPARTMENT;
            obj.EMPL_CURRENT_POSITION = objApplicationDto.EMPL_CURRENT_POSITION;
            obj.SPOUSE_WORKING_STATUS = objApplicationDto.SPOUSE_WORKING_STATUS;
            obj.DEMAT_ACCOUNT_NUMBER = objApplicationDto.DEMAT_ACCOUNT_NUMBER;
            obj.EXISTING_CREDIT_CARD_LIMIT = objApplicationDto.EXISTING_CREDIT_CARD_LIMIT;
            obj.CREDIT_AS_PERCENT_OF_INCOME = objApplicationDto.CREDIT_AS_PERCENT_OF_INCOME;
            obj.MIDDLE_NAME = objApplicationDto.MIDDLE_NAME;
            obj.ADDITIONAL_CARD_MIDDLE_NAME = objApplicationDto.ADDITIONAL_CARD_MIDDLE_NAME;
            obj.ADDITIONAL_GENDER = objApplicationDto.ADDITIONAL_GENDER;
            obj.WITNESSED_BY = objApplicationDto.WITNESSED_BY;
            obj.BIRTH_DATE_NOMINEE = objApplicationDto.BIRTH_DATE_NOMINEE;
            obj.REFERENCE_EMPLOYEE_ID = objApplicationDto.REFERENCE_EMPLOYEE_ID;
            obj.EMPL_ID = objApplicationDto.EMPL_ID;
            obj.APPLICATION_TYPE = objApplicationDto.APPLICATION_TYPE;
            obj.DSA_CODE = objApplicationDto.DSA_CODE;
            obj.VERIFICATION_AGENCY_CODE = objApplicationDto.VERIFICATION_AGENCY_CODE;
            obj.VOTER_ID = objApplicationDto.VOTER_ID;
            obj.STAFF_E_C_NO = objApplicationDto.STAFF_E_C_NO;
            obj.STAFF_NAME = objApplicationDto.STAFF_NAME;
            obj.STAFF_BRANCH = objApplicationDto.STAFF_BRANCH;
            obj.RECOMMENDED_BY = objApplicationDto.RECOMMENDED_BY;
            obj.DOM = objApplicationDto.DOM;
            obj.UNIVERSITY = objApplicationDto.UNIVERSITY;
            obj.FATHER_NAME = objApplicationDto.FATHER_NAME;
            obj.SPOUSE_MOB_NO = objApplicationDto.SPOUSE_MOB_NO;
            obj.RESIDANCE_CHANGED = objApplicationDto.RESIDANCE_CHANGED;
            obj.OTHER_INCOME = objApplicationDto.OTHER_INCOME;
            obj.INCOME_PER_MONTH = objApplicationDto.INCOME_PER_MONTH;
            obj.CUSTOMER_ID = objApplicationDto.CUSTOMER_ID;
            obj.TAX_PAID = objApplicationDto.TAX_PAID;
            obj.YEAR_TAX_PAID = objApplicationDto.YEAR_TAX_PAID;
            obj.OTHER_BRANCH = objApplicationDto.OTHER_BRANCH;
            obj.OTHER_CITY = objApplicationDto.OTHER_CITY;
            obj.HOUSING_LOAN = objApplicationDto.HOUSING_LOAN;
            obj.CAR_LOAN = objApplicationDto.CAR_LOAN;
            obj.CONSUMER_LOAN = objApplicationDto.CONSUMER_LOAN;
            obj.BUSINESS_LOAN = objApplicationDto.BUSINESS_LOAN;
            obj.OTHR_LOAN = objApplicationDto.OTHR_LOAN;
            obj.TYPE_OF_LOAN_OTHERS = objApplicationDto.TYPE_OF_LOAN_OTHERS;
            obj.LOAN_AMOUNT = objApplicationDto.LOAN_AMOUNT;
            obj.CURRENT_OUTSTANDING = objApplicationDto.CURRENT_OUTSTANDING;
            obj.DURATION_OF_LOAN = objApplicationDto.DURATION_OF_LOAN;
            obj.LOAN_INSTUTION_NAME = objApplicationDto.LOAN_INSTUTION_NAME;
            obj.LOAN_BRANCH = objApplicationDto.LOAN_BRANCH;
            obj.BOB_DEBIT_CARD_NO = objApplicationDto.BOB_DEBIT_CARD_NO;
            obj.DC_VALID_UPTO = objApplicationDto.DC_VALID_UPTO;
            obj.CC_BANK_NAME1 = objApplicationDto.CC_BANK_NAME1;
            obj.CC_NO1 = objApplicationDto.CC_NO1;
            obj.CC_VALID_UPTO1 = objApplicationDto.CC_VALID_UPTO1;
            obj.CC_CR_LITMIT1 = objApplicationDto.CC_CR_LITMIT1;
            obj.CC_BANK_NAME2 = objApplicationDto.CC_BANK_NAME2;
            obj.CC_NO2 = objApplicationDto.CC_NO2;
            obj.CC_VALID_UPTO2 = objApplicationDto.CC_VALID_UPTO2;
            obj.CC_CR_LITMIT2 = objApplicationDto.CC_CR_LITMIT2;
            obj.CC_BANK_NAME3 = objApplicationDto.CC_BANK_NAME3;
            obj.CC_NO3 = objApplicationDto.CC_NO3;
            obj.CC_VALID_UPTO3 = objApplicationDto.CC_VALID_UPTO3;
            obj.CC_CR_LITMIT3 = objApplicationDto.CC_CR_LITMIT3;
            obj.SEC2_TITLE = objApplicationDto.SEC2_TITLE;
            obj.SEC2_FIRST_NAME = objApplicationDto.SEC2_FIRST_NAME;
            obj.SEC2_MIDDLE_NAME = objApplicationDto.SEC2_MIDDLE_NAME;
            obj.SEC2_LAST_NAME = objApplicationDto.SEC2_LAST_NAME;
            obj.SEC2_EMBOSS_NAME = objApplicationDto.SEC2_EMBOSS_NAME;
            obj.SEC2_BIRTH_DATE = objApplicationDto.SEC2_BIRTH_DATE;
            obj.SEC2_GENDER = objApplicationDto.SEC2_GENDER;
            obj.SEC2_RELATION = objApplicationDto.SEC2_RELATION;
            obj.REF_NAME1 = objApplicationDto.REF_NAME1;
            obj.REF_ADDR1 = objApplicationDto.REF_ADDR1;
            obj.REF_CITY1 = objApplicationDto.REF_CITY1;
            obj.REF_TELEPH1 = objApplicationDto.REF_TELEPH1;
            obj.REF_ZIP_CODE1 = objApplicationDto.REF_ZIP_CODE1;
            obj.REF_NAME2 = objApplicationDto.REF_NAME2;
            obj.REF_ADDR2 = objApplicationDto.REF_ADDR2;
            obj.REF_CITY2 = objApplicationDto.REF_CITY2;
            obj.REF_TELEPH2 = objApplicationDto.REF_TELEPH2;
            obj.REF_ZIP_CODE2 = objApplicationDto.REF_ZIP_CODE2;
            obj.SEC2_APPLICANT_PROF = objApplicationDto.SEC2_APPLICANT_PROF;
            obj.SEC1_APPLICANT_PROF = objApplicationDto.SEC1_APPLICANT_PROF;
            obj.PERM_EMAIL_ID = objApplicationDto.PERM_EMAIL_ID;
            obj.PERM_MOBILE_NUMBER = objApplicationDto.PERM_MOBILE_NUMBER;
            obj.EMPL_EMAIL_ID = objApplicationDto.EMPL_EMAIL_ID;
            obj.EMPL_MOBILE_NUMBER = objApplicationDto.EMPL_MOBILE_NUMBER;
            obj.REF1_EMAIL_ID = objApplicationDto.REF1_EMAIL_ID;
            obj.REF1_MOBILE_NUMBER = objApplicationDto.REF1_MOBILE_NUMBER;
            obj.RECOMMENDED_BRANCH = objApplicationDto.RECOMMENDED_BRANCH;
            obj.APPLICANT_PHOTO = objApplicationDto.APPLICANT_PHOTO;
            obj.APPLICATION_NO = objApplicationDto.APPLICATION_NO;
            //obj.inserted = DateTime.Now;
            //obj.updated = DateTime.Now;
            //FindLongStrings(obj);
            FindLongStrings1(obj);
            string AppNum = new ApplicationManager().GetApplicationNo();
            string IsValid = "N";
            BOBCardEntities ObjContext = new BOBCardEntities();

            while (IsValid == "N")
            {
                bool isExists = ObjContext.Applications.Where(x => x.APPLICATION_NO == AppNum).Count() > 0;
                if (isExists == true)
                {
                    AppNum = new ApplicationManager().GetApplicationNo();
                    IsValid = "N";
                }
                else
                    IsValid = "Y";
            }
            obj.APPLICATION_NO = AppNum;

            if (obj.ID == 0)
            {
                RepApplicationMst.Add(obj);
            }

            GeneralManager.Commit();

            objApplicationDto.ID = obj.ID;

            return objApplicationDto.ID;

        }

        /// <summary>
        /// Gets the application by identifier.
        /// </summary>
        /// <param name="ApplicationId">The application identifier.</param>
        /// <returns></returns>
        public Application_DTO GetApplicationById(Int64 ApplicationId)
        {
            Application_DTO objApplicationDTO = new Application_DTO();
            Application obj = RepApplicationMst.SingleOrDefault(c => c.ID == objApplicationDTO.ID);

            //if (obj == null || obj.ID != objApplicationDTO.ID)
            //{
            //    obj = new Application();
            //}

            if (obj != null && obj.ID == ApplicationId)
            {
                objApplicationDTO.APPLICATION_NO = obj.APPLICATION_NO;
                objApplicationDTO.PAN_GIR_NO = obj.PAN_GIR_NO;
                objApplicationDTO.FIRST_NAME = obj.FIRST_NAME;
                objApplicationDTO.FAMILY_NAME = obj.FAMILY_NAME;
                objApplicationDTO.MIDDLE_NAME = obj.MIDDLE_NAME;
                objApplicationDTO.SOURCE_CODE = obj.SOURCE_CODE;
                objApplicationDTO.MOBILE_NUMBER = obj.MOBILE_NUMBER;
                objApplicationDTO.EMAIL_ID = obj.EMAIL_ID;
                objApplicationDTO.PRODUCT_CODE = obj.PRODUCT_CODE;
                objApplicationDTO.SOURCE_APPLICATION_NO = obj.SOURCE_APPLICATION_NO;
                return objApplicationDTO;
            }
            return null;
        }


        private static object locker = new object();
        public string GetApplicationNo()
        {
            //return "" +
            //   DateTime.Now.Year + DateTime.Now.Month +
            //   DateTime.Now.Day +
            //   DateTime.Now.Hour +
            //   DateTime.Now.Minute +
            //   DateTime.Now.Second +
            //   DateTime.Now.Millisecond;

            //  return DateTime.Now.ToString("yyyyMMddHHmmssf");
            int r = (new Random()).Next(0, 1000);
            string rndno = r.ToString("000");
            lock (locker)
            {
                Thread.Sleep(100);
                //return DateTime.Now.ToString("yyyyMMddHHmmssf");
                return (DateTime.Now.ToString("yyyyMMddHHmm") + rndno);
            }
        }

        public List<ClassA> FindLongStrings1(object testObject)
        {
            List<ClassA> lstlength = new List<ClassA>();
            foreach (PropertyInfo propInfo in testObject.GetType().GetProperties())
            {
                ClassA objClassA = new ClassA();
                objClassA.COLUMN_NAME = propInfo.Name;
                objClassA.PropertysValue = Convert.ToString(propInfo.GetValue(testObject, null));
                objClassA.CHARACTER_MAXIMUM_LENGTH = (objClassA.PropertysValue ?? string.Empty).Length;
                lstlength.Add(objClassA);
            }
            List<ClassA> lstA = new List<ClassA>();
            string StrQry = "select COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH from information_schema.columns where table_schema = 'dbo' AND   table_name = 'Application'";
            using (var context = new BOBCardEntities())
            {
                lstA = (from u in context.ExecuteStoreQuery<ClassA>(StrQry)
                        select new ClassA
                        {
                            CHARACTER_MAXIMUM_LENGTH = u.CHARACTER_MAXIMUM_LENGTH,
                            COLUMN_NAME = u.COLUMN_NAME
                        }).ToList();

            }
            foreach (var item in lstlength)
            {
                ClassA objcls = lstA.Where(t => t.COLUMN_NAME == item.COLUMN_NAME).FirstOrDefault();

                if (objcls != null && objcls.CHARACTER_MAXIMUM_LENGTH > 0)
                {
                    if (objcls.CHARACTER_MAXIMUM_LENGTH < item.CHARACTER_MAXIMUM_LENGTH)
                    {
                        CreateLog("column: " + item.COLUMN_NAME + "___Length: " + "DB Column length : " + objcls.CHARACTER_MAXIMUM_LENGTH + "Entererd value Length :" + item.CHARACTER_MAXIMUM_LENGTH, "Application");
                    }
                }
            }
            return lstlength;
        }


        public static void CreateLog(string sLogMsg, string MessageType)
        {
            //Console.WriteLine(sLogMsg);

            string LogPath = System.Configuration.ConfigurationSettings.AppSettings["binaryErrorLogPath"];


            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string sErrorTime = sYear + sMonth + sDay;

            string Path = LogPath + MessageType + "\\" + sYear + "\\" + sMonth + "\\" + sDay + sMonth + sYear;
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            //FileStream fileStream = new FileStream(Path + "\\log.txt", FileMode.Append);
            StreamWriter SW;

            SW = File.AppendText(Path + "\\log.txt");
            SW.WriteLine(sLogMsg);
            SW.Close();


        }
        public static void FindLongStrings(object testObject)
        {
            foreach (PropertyInfo propInfo in testObject.GetType().GetProperties())
            {
                foreach (ColumnAttribute attribute in propInfo.GetCustomAttributes(typeof(ColumnAttribute), true))
                {
                    if (attribute.DbType.ToLower().Contains("varchar"))
                    {
                        string dbType = attribute.DbType.ToLower();
                        int numberStartIndex = dbType.IndexOf("varchar(") + 8;
                        int numberEndIndex = dbType.IndexOf(")", numberStartIndex);
                        string lengthString = dbType.Substring(numberStartIndex, (numberEndIndex - numberStartIndex));
                        int maxLength = 0;
                        int.TryParse(lengthString, out maxLength);

                        string currentValue = (string)propInfo.GetValue(testObject, null);

                        if (!string.IsNullOrEmpty(currentValue) && currentValue.Length > maxLength)
                            Console.WriteLine(testObject.GetType().Name + "." + propInfo.Name + " " + currentValue + " Max: " + maxLength);

                    }
                }
            }
        }


        /// <summary>
        /// Find Method
        /// </summary>
        /// <param name="AddressId"></param>
        /// <returns></returns>
        public static Application_DTO FindRecord(string Appnum)
        {

            Application objAdd = RepApplicationMst.SingleOrDefault(x => x.APPLICATION_NO == Appnum);


            if (objAdd == null)
            {
                return new Application_DTO();
            }
            return new Application_DTO
            {
                ID = objAdd.ID,
                APPLICATION_NO = objAdd.APPLICATION_NO,
                SOURCE_APPLICATION_NO = objAdd.SOURCE_APPLICATION_NO,
                SOURCE_TYPE = objAdd.SOURCE_TYPE,
                SOURCE_CODE = objAdd.SOURCE_CODE,
                BANK_CODE = objAdd.BANK_CODE,
                PRODUCT_CODE = objAdd.PRODUCT_CODE,
                VIP_CODE = objAdd.VIP_CODE,
                SOCIAL_STATUS = objAdd.SOCIAL_STATUS,
                BASIC_CARD_FLAG = objAdd.BASIC_CARD_FLAG,
                PRIMARY_CARD_NUMBER = objAdd.PRIMARY_CARD_NUMBER,
                CLIENT_ID_OF_COMPANY = objAdd.CLIENT_ID_OF_COMPANY,
                TITLE = objAdd.TITLE,
                FAMILY_NAME = objAdd.FAMILY_NAME,
                FIRST_NAME = objAdd.FIRST_NAME,
                EMBOSSED_NAME = objAdd.EMBOSSED_NAME,
                BIRTH_DATE = objAdd.BIRTH_DATE,
                BIRTH_CITY = objAdd.BIRTH_CITY,
                BIRTH_COUNTRY = objAdd.BIRTH_COUNTRY,
                GENDER = objAdd.GENDER,
                MARITAL_STATUS = objAdd.MARITAL_STATUS,
                NO_OF_DEPENDENTS = objAdd.NO_OF_DEPENDENTS,
                NATIONALITY = objAdd.NATIONALITY,
                PERM_ADDRESS1 = objAdd.PERM_ADDRESS1,
                PERM_ADDRESS2 = objAdd.PERM_ADDRESS2,
                PERM_ADDRESS3 = objAdd.PERM_ADDRESS3,
                PERM_ADDRESS4 = objAdd.PERM_ADDRESS4,
                PERM_POSTAL_CODE = objAdd.PERM_POSTAL_CODE,
                PERM_CITY_CODE = objAdd.PERM_CITY_CODE,
                PERM_COUNTRY_CODE = objAdd.PERM_COUNTRY_CODE,
                RESIDENCE_STATUS = objAdd.RESIDENCE_STATUS,
                CURR_ADDRESS2 = objAdd.CURR_ADDRESS2,
                CURR_ADDRESS3 = objAdd.CURR_ADDRESS3,
                CURR_ADDRESS4 = objAdd.CURR_ADDRESS4,
                CURR_POSTAL_CODE = objAdd.CURR_POSTAL_CODE,
                CURR_CITY_CODE = objAdd.CURR_CITY_CODE,
                CURR_COUNTRY_CODE = objAdd.CURR_COUNTRY_CODE,
                HOME_PHONE_NUMBER = objAdd.HOME_PHONE_NUMBER,
                OFFICE_PHONE_NUMBER = objAdd.OFFICE_PHONE_NUMBER,
                MOBILE_NUMBER = objAdd.MOBILE_NUMBER,
                EMAIL_ID = objAdd.EMAIL_ID,
                EDUCATION = objAdd.EDUCATION,
                EMPLOYMENT_STATUS = objAdd.EMPLOYMENT_STATUS,
                COMP_TYPE = objAdd.COMP_TYPE,
                APPLICANT_PROF = objAdd.APPLICANT_PROF,
                EMPLOYER_NAME = objAdd.EMPLOYER_NAME,
                EMPL_ADDRESS1 = objAdd.EMPL_ADDRESS1,
                EMPL_ADDRESS2 = objAdd.EMPL_ADDRESS2,
                EMPL_ADDRESS3 = objAdd.EMPL_ADDRESS3,
                EMPL_ADDRESS4 = objAdd.EMPL_ADDRESS4,
                EMPL_POSTAL_CODE = objAdd.EMPL_POSTAL_CODE,
                EMPL_CITY_CODE = objAdd.EMPL_CITY_CODE,
                EMPL_COUNTRY_CODE = objAdd.EMPL_COUNTRY_CODE,
                EXISTING_CREDIT_CARD_NUMBER = objAdd.EXISTING_CREDIT_CARD_NUMBER,
                ACC_IN_BANK = objAdd.ACC_IN_BANK,
                ACCOUNT_BRANCH = objAdd.ACCOUNT_BRANCH,
                ACCOUNT_NUMBER = objAdd.ACCOUNT_NUMBER,
                OWNED_VEHICLE_TYPE = objAdd.OWNED_VEHICLE_TYPE,
                ANNUAL_INCOME_CODE = objAdd.ANNUAL_INCOME_CODE,
                INSURANCE_NOM_NAME = objAdd.INSURANCE_NOM_NAME,
                INSURANCE_NOM_REL = objAdd.INSURANCE_NOM_REL,
                PAN_GIR_NO = objAdd.PAN_GIR_NO,
                PASSPORT_NUMBER = objAdd.PASSPORT_NUMBER,
                DRIVING_LICENSE_NUMBER = objAdd.DRIVING_LICENSE_NUMBER,
                PASSPORT_PLACE_OF_ISSUE = objAdd.PASSPORT_PLACE_OF_ISSUE,
                ADDITIONAL_CARD_NAME = objAdd.ADDITIONAL_CARD_NAME,
                ADDITIONAL_CARD_EMB_NAME = objAdd.ADDITIONAL_CARD_EMB_NAME,
                ADDITIONAL_CARD_REL = objAdd.ADDITIONAL_CARD_REL,
                APPLICATION_STATUS = objAdd.APPLICATION_STATUS,
                CLIENT_CODE = objAdd.CLIENT_CODE,
                CARD_NUMBER = objAdd.CARD_NUMBER,
                BATCH_NUMBER = objAdd.BATCH_NUMBER,
                RISK_STATUS = objAdd.RISK_STATUS,
                CREDIT_LIMIT = objAdd.CREDIT_LIMIT,
                CURRENCY_CODE = objAdd.CURRENCY_CODE,
                APP_PROCESS_DATE = objAdd.APP_PROCESS_DATE,
                SCORE = objAdd.SCORE,
                AGE = objAdd.AGE,
                REMARKS = objAdd.REMARKS,
                REASON_CODE = objAdd.REASON_CODE,
                USER_CREATE = objAdd.USER_CREATE,
                DATE_CREATE = objAdd.DATE_CREATE,
                USER_MODIF = objAdd.USER_MODIF,
                DATE_MODIF = objAdd.DATE_MODIF,
                USER_DELETE = objAdd.USER_DELETE,
                DATE_DELETE = objAdd.DATE_DELETE,
                LANGUAGE_PREF = objAdd.LANGUAGE_PREF,
                MAIDEN_NAME = objAdd.MAIDEN_NAME,
                PREFERENCES = objAdd.PREFERENCES,
                SEC_CARD_NUMBER = objAdd.SEC_CARD_NUMBER,
                CR_ACCOUNT_NBR = objAdd.CR_ACCOUNT_NBR,
                PHOTOCARD_APP_IND = objAdd.PHOTOCARD_APP_IND,
                BT_IND = objAdd.BT_IND,
                BT_BANK = objAdd.BT_BANK,
                BT_CARD = objAdd.BT_CARD,
                BT_AMOUNT = objAdd.BT_AMOUNT,
                BT_INTEREST_RATE = objAdd.BT_INTEREST_RATE,
                SEC_BIRTH_DATE = objAdd.SEC_BIRTH_DATE,
                USER_VERIF = objAdd.USER_VERIF,
                DATE_VERIFY = objAdd.DATE_VERIFY,
                USER_APPROVED = objAdd.USER_APPROVED,
                DATE_APPROVED = objAdd.DATE_APPROVED,
                USER_APPROVED_SATYAM = objAdd.USER_APPROVED_SATYAM,
                DATE_APPROVED_SATYAM = objAdd.DATE_APPROVED_SATYAM,
                SATYAM_FILE = objAdd.SATYAM_FILE,
                ADDITIONAL_FAMILY_NAME = objAdd.ADDITIONAL_FAMILY_NAME,
                EMBOSSED_LINE_4 = objAdd.EMBOSSED_LINE_4,
                CASH_LIMIT = objAdd.CASH_LIMIT,
                SEC_TITLE = objAdd.SEC_TITLE,
                SPOUSE_NAME = objAdd.SPOUSE_NAME,
                SPOUSE_INCOME = objAdd.SPOUSE_INCOME,
                PERMNANT_ADDRESS_TENURE = objAdd.PERMNANT_ADDRESS_TENURE,
                MAILING_ADDRESS_TENURE = objAdd.MAILING_ADDRESS_TENURE,
                PASSWORD = objAdd.PASSWORD,
                EMPLOYER_BUSINESS_NATURE = objAdd.EMPLOYER_BUSINESS_NATURE,
                CURRENT_JOB_TENURE = objAdd.CURRENT_JOB_TENURE,
                PREVIOUS_EMPLOYER_NAME = objAdd.PREVIOUS_EMPLOYER_NAME,
                PREVIOUS_JOB_TENURE = objAdd.PREVIOUS_JOB_TENURE,
                WORKPLACE = objAdd.WORKPLACE,
                OUR_ACCOUNT_TYPE = objAdd.OUR_ACCOUNT_TYPE,
                OUR_ACCOUNT_TENURE = objAdd.OUR_ACCOUNT_TENURE,
                OUR_ACCOUNT_STATUS = objAdd.OUR_ACCOUNT_STATUS,
                OUR_BANK_ACC_AVERAGE_BALANCES = objAdd.OUR_BANK_ACC_AVERAGE_BALANCES,
                OTHER_BANK_NAME = objAdd.OTHER_BANK_NAME,
                OTHER_ACCOUNT_TYPE = objAdd.OTHER_ACCOUNT_TYPE,
                OTHER_ACCOUNT_NUMBER = objAdd.OTHER_ACCOUNT_NUMBER,
                OTHER_ACCOUNT_TENURE = objAdd.OTHER_ACCOUNT_TENURE,
                OTHER_ACCOUNT_STATUS = objAdd.OTHER_ACCOUNT_STATUS,
                OTHER_BANK_ACC_AVG_BALANCES = objAdd.OTHER_BANK_ACC_AVG_BALANCES,
                HOME_ESTIMATED_CURRENT_VALUE = objAdd.HOME_ESTIMATED_CURRENT_VALUE,
                HOME_CURR_MORTGAGE_OUTSTANDING = objAdd.HOME_CURR_MORTGAGE_OUTSTANDING,
                HOME_MONTHLY_MORTGAGE_REPAY = objAdd.HOME_MONTHLY_MORTGAGE_REPAY,
                HSG_LENDER_NAME = objAdd.HSG_LENDER_NAME,
                HSG_LENDER_ADDRESS1 = objAdd.HSG_LENDER_ADDRESS1,
                HSG_LENDER_ADDRESS2 = objAdd.HSG_LENDER_ADDRESS2,
                HSG_LENDER_ADDRESS3 = objAdd.HSG_LENDER_ADDRESS3,
                HSG_LENDER_ADDRESS4 = objAdd.HSG_LENDER_ADDRESS4,
                HSG_LENDER_ZIP_CODE = objAdd.HSG_LENDER_ZIP_CODE,
                HSG_LENDER_CITY_CODE = objAdd.HSG_LENDER_CITY_CODE,
                HSG_LENDER_COUNTRY_CODE = objAdd.HSG_LENDER_COUNTRY_CODE,
                HSG_LENDER_PHONE_NUMBER = objAdd.HSG_LENDER_PHONE_NUMBER,
                HSG_LENDER_ACCOUNT_NUMBER = objAdd.HSG_LENDER_ACCOUNT_NUMBER,
                HOME_MONTHLY_RENT = objAdd.HOME_MONTHLY_RENT,
                OTHER_LOAN = objAdd.OTHER_LOAN,
                OTHER_LOAN_DESC = objAdd.OTHER_LOAN_DESC,
                OTHER_LENDER1_NAME = objAdd.OTHER_LENDER1_NAME,
                OTHER_LENDER1_ADDRESS1 = objAdd.OTHER_LENDER1_ADDRESS1,
                OTHER_LENDER1_ADDRESS2 = objAdd.OTHER_LENDER1_ADDRESS2,
                OTHER_LENDER1_ADDRESS3 = objAdd.OTHER_LENDER1_ADDRESS3,
                OTHER_LENDER1_ADDRESS4 = objAdd.OTHER_LENDER1_ADDRESS4,
                OTHER_LENDER1_ZIP_CODE = objAdd.OTHER_LENDER1_ZIP_CODE,
                OTHER_LENDER1_CITY_CODE = objAdd.OTHER_LENDER1_CITY_CODE,
                OTHER_LENDER1_COUNTRY_CODE = objAdd.OTHER_LENDER1_COUNTRY_CODE,
                OTHER_LENDER1_PHONE_NUMBER = objAdd.OTHER_LENDER1_PHONE_NUMBER,
                OTHER_LENDER1_MONTHLY_PAYMENT = objAdd.OTHER_LENDER1_MONTHLY_PAYMENT,
                OTHER_LENDER1_CURRENT_BALANCE = objAdd.OTHER_LENDER1_CURRENT_BALANCE,
                OTHER_LENDER2_NAME = objAdd.OTHER_LENDER2_NAME,
                OTHER_LENDER2_ADDRESS1 = objAdd.OTHER_LENDER2_ADDRESS1,
                OTHER_LENDER2_ADDRESS2 = objAdd.OTHER_LENDER2_ADDRESS2,
                OTHER_LENDER2_ADDRESS3 = objAdd.OTHER_LENDER2_ADDRESS3,
                OTHER_LENDER2_ADDRESS4 = objAdd.OTHER_LENDER2_ADDRESS4,
                OTHER_LENDER2_ZIP_CODE = objAdd.OTHER_LENDER2_ZIP_CODE,
                OTHER_LENDER2_CITY_CODE = objAdd.OTHER_LENDER2_CITY_CODE,
                OTHER_LENDER2_COUNTRY_CODE = objAdd.OTHER_LENDER2_COUNTRY_CODE,
                OTHER_LENDER2_PHONE_NUMBER = objAdd.OTHER_LENDER2_PHONE_NUMBER,
                OTHER_LENDER2_MONTHLY_PAYMENT = objAdd.OTHER_LENDER2_MONTHLY_PAYMENT,
                OTHER_LENDER2_CURRENT_BALANCE = objAdd.OTHER_LENDER2_CURRENT_BALANCE,
                OTHER_CREDIT_CARDS_TYPE = objAdd.OTHER_CREDIT_CARDS_TYPE,
                OTHER_CREDIT_CARDS_COUNT = objAdd.OTHER_CREDIT_CARDS_COUNT,
                CARD1_ISSUED_BY = objAdd.CARD1_ISSUED_BY,
                CARD1_TENURE = objAdd.CARD1_TENURE,
                CARD1_REFERENCES = objAdd.CARD1_REFERENCES,
                CARD2_ISSUED_BY = objAdd.CARD2_ISSUED_BY,
                CARD2_NUMBER = objAdd.CARD2_NUMBER,
                CARD2_TENURE = objAdd.CARD2_TENURE,
                CARD2_REFERENCES = objAdd.CARD2_REFERENCES,
                MONTHLY_REPAYMENTS_TO_LOAN = objAdd.MONTHLY_REPAYMENTS_TO_LOAN,
                REF1_NAME = objAdd.REF1_NAME,
                REF1_ADDRESS1 = objAdd.REF1_ADDRESS1,
                REF1_ADDRESS2 = objAdd.REF1_ADDRESS2,
                REF1_ADDRESS3 = objAdd.REF1_ADDRESS3,
                REF1_ADDRESS4 = objAdd.REF1_ADDRESS4,
                REF1_ZIP_CODE = objAdd.REF1_ZIP_CODE,
                REF1_CITY_CODE = objAdd.REF1_CITY_CODE,
                REF1_COUNTRY_CODE = objAdd.REF1_COUNTRY_CODE,
                REF1_PHONE_NUMBER = objAdd.REF1_PHONE_NUMBER,
                REF2_NAME = objAdd.REF2_NAME,
                REF2_ADDRESS1 = objAdd.REF2_ADDRESS1,
                REF2_ADDRESS2 = objAdd.REF2_ADDRESS2,
                REF2_ADDRESS3 = objAdd.REF2_ADDRESS3,
                REF2_ADDRESS4 = objAdd.REF2_ADDRESS4,
                REF2_ZIP_CODE = objAdd.REF2_ZIP_CODE,
                REF2_CITY_CODE = objAdd.REF2_CITY_CODE,
                REF2_COUNTRY_CODE = objAdd.REF2_COUNTRY_CODE,
                REF2_PHONE_NUMBER = objAdd.REF2_PHONE_NUMBER,
                OTHER_REFERENCES = objAdd.OTHER_REFERENCES,
                SEC_PAN_GIR_NO = objAdd.SEC_PAN_GIR_NO,
                SEC_PASSPORT_NUMBER = objAdd.SEC_PASSPORT_NUMBER,
                SEC_PASSPORT_PLACE_OF_ISSUE = objAdd.SEC_PASSPORT_PLACE_OF_ISSUE,
                SEC_EMPLOYER_NAME = objAdd.SEC_EMPLOYER_NAME,
                SEC_TELEPHONE_NO = objAdd.SEC_TELEPHONE_NO,
                SEC_CREDIT_LIMIT = objAdd.SEC_CREDIT_LIMIT,
                DIRECT_DEBIT_FLAG = objAdd.DIRECT_DEBIT_FLAG,
                DIRECT_DEBIT_BRANCH = objAdd.DIRECT_DEBIT_BRANCH,
                DIRECT_DEBIT_ACCOUNT_NAME = objAdd.DIRECT_DEBIT_ACCOUNT_NAME,
                DIRECT_DEBIT_ACCOUNT_TYPE = objAdd.DIRECT_DEBIT_ACCOUNT_TYPE,
                DIRECT_DEBIT_ACCOUNT_NUMBER = objAdd.DIRECT_DEBIT_ACCOUNT_NUMBER,
                ACTUAL_JOB_TENURE = objAdd.ACTUAL_JOB_TENURE,
                OTHER_BANK_BRANCH = objAdd.OTHER_BANK_BRANCH,
                DIRECT_DEBIT_AMOUNT_FLAG = objAdd.DIRECT_DEBIT_AMOUNT_FLAG,
                CYCOFF_CODE = objAdd.CYCOFF_CODE,
                TARIFF_CODE = objAdd.TARIFF_CODE,
                PROFILE_CODE = objAdd.PROFILE_CODE,
                DIRECT_DEBIT_PERCENTAGE = objAdd.DIRECT_DEBIT_PERCENTAGE,
                MICR_CODE = objAdd.MICR_CODE,
                PREFERRED_MAILING_ADDRESS = objAdd.PREFERRED_MAILING_ADDRESS,
                PICTURE_CODE = objAdd.PICTURE_CODE,
                PROMO_CODE = objAdd.PROMO_CODE,
                CUSTOM_LYT_PLAN_ENABLE = objAdd.CUSTOM_LYT_PLAN_ENABLE,
                SEC_PHOTOCARD_APP_IND = objAdd.SEC_PHOTOCARD_APP_IND,
                SEC_PICTURE_CODE = objAdd.SEC_PICTURE_CODE,
                CHOICE_WELCOME_GIFT = objAdd.CHOICE_WELCOME_GIFT,
                I_AM_INTERESTED_IN = objAdd.I_AM_INTERESTED_IN,
                FAVOURITE_RESTURANT = objAdd.FAVOURITE_RESTURANT,
                MERCHANT_CAT_CODE_1 = objAdd.MERCHANT_CAT_CODE_1,
                PTS_DISTRIB_FOR_MCG_1 = objAdd.PTS_DISTRIB_FOR_MCG_1,
                MERCHANT_CAT_CODE_2 = objAdd.MERCHANT_CAT_CODE_2,
                PTS_DISTRIB_FOR_MCG_2 = objAdd.PTS_DISTRIB_FOR_MCG_2,
                MERCHANT_CAT_CODE_3 = objAdd.MERCHANT_CAT_CODE_3,
                PTS_DISTRIB_FOR_MCG_3 = objAdd.PTS_DISTRIB_FOR_MCG_3,
                MERCHANT_CAT_CODE_4 = objAdd.MERCHANT_CAT_CODE_4,
                PTS_DISTRIB_FOR_MCG_4 = objAdd.PTS_DISTRIB_FOR_MCG_4,
                MERCHANT_CAT_CODE_5 = objAdd.MERCHANT_CAT_CODE_5,
                PTS_DISTRIB_FOR_MCG_5 = objAdd.PTS_DISTRIB_FOR_MCG_5,
                MERCHANT_CAT_CODE_6 = objAdd.MERCHANT_CAT_CODE_6,
                PTS_DISTRIB_FOR_MCG_6 = objAdd.PTS_DISTRIB_FOR_MCG_6,
                MERCHANT_CAT_CODE_7 = objAdd.MERCHANT_CAT_CODE_7,
                PTS_DISTRIB_FOR_MCG_7 = objAdd.PTS_DISTRIB_FOR_MCG_7,
                MERCHANT_CAT_CODE_8 = objAdd.MERCHANT_CAT_CODE_8,
                PTS_DISTRIB_FOR_MCG_8 = objAdd.PTS_DISTRIB_FOR_MCG_8,
                MERCHANT_CAT_CODE_9 = objAdd.MERCHANT_CAT_CODE_9,
                PTS_DISTRIB_FOR_MCG_9 = objAdd.PTS_DISTRIB_FOR_MCG_9,
                MERCHANT_CAT_CODE_10 = objAdd.MERCHANT_CAT_CODE_10,
                PTS_DISTRIB_FOR_MCG_10 = objAdd.PTS_DISTRIB_FOR_MCG_10,
                MNAME = objAdd.MNAME,
                CHILD1_NAME = objAdd.CHILD1_NAME,
                CHILD1_DOB = objAdd.CHILD1_DOB,
                NCM_NAME_CHILD2 = objAdd.NCM_NAME_CHILD2,
                CHILD2_DOB = objAdd.CHILD2_DOB,
                NCM_DOB_CHILD2 = objAdd.NCM_DOB_CHILD2,
                VC_DEGREE = objAdd.VC_DEGREE,
                VC_UNIVERSITY = objAdd.VC_UNIVERSITY,
                LOS_TOTALYRSEXPERIENCE_N = objAdd.LOS_TOTALYRSEXPERIENCE_N,
                LOS_DESIGNATION_C = objAdd.LOS_DESIGNATION_C,
                ADDYEAR = objAdd.ADDYEAR,
                ALTERNATE_NUBMER = objAdd.ALTERNATE_NUBMER,
                PERM_ALTERNATE_NUBMER = objAdd.PERM_ALTERNATE_NUBMER,
                OFFICE_EMAIL = objAdd.OFFICE_EMAIL,
                OFFICE_MOBILE = objAdd.OFFICE_MOBILE,
                PAGER = objAdd.PAGER,
                TELEX = objAdd.TELEX,
                VEHICLE_OWNERSHIP = objAdd.VEHICLE_OWNERSHIP,
                VEHICLE_REG_NO = objAdd.VEHICLE_REG_NO,
                ISSUER_BANK1 = objAdd.ISSUER_BANK1,
                MEMBERSINCE = objAdd.MEMBERSINCE,
                CARD1_CREDIT_LIMIT = objAdd.CARD1_CREDIT_LIMIT,
                ISSUER_BANK2 = objAdd.ISSUER_BANK2,
                CARD2_MEMBER_SINCE = objAdd.CARD2_MEMBER_SINCE,
                CARD2_CREDIT_LIMIT = objAdd.CARD2_CREDIT_LIMIT,
                VC_BRANCH_CD = objAdd.VC_BRANCH_CD,
                VC_FEE_CD = objAdd.VC_FEE_CD,
                N_SMAC = objAdd.N_SMAC,
                PREF_EMAILID = objAdd.PREF_EMAILID,
                NCM_FIN_CUST_ID = objAdd.NCM_FIN_CUST_ID,
                NCM_ECS_CUST_ID = objAdd.NCM_ECS_CUST_ID,
                VC_EMPLOYMENT_STATUS = objAdd.VC_EMPLOYMENT_STATUS,
                LAA_CAMP_TYPE = objAdd.LAA_CAMP_TYPE,
                LOS_WHETHER_RESI_C = objAdd.LOS_WHETHER_RESI_C,
                PERM_ADDRESS_CONTACT_PERSON = objAdd.PERM_ADDRESS_CONTACT_PERSON,
                FAX = objAdd.FAX,
                LOS_NATUREOFCOMP_C = objAdd.LOS_NATUREOFCOMP_C,
                NCM_ADDON_GENDER = objAdd.NCM_ADDON_GENDER,
                BAL_TRANS_NAME = objAdd.BAL_TRANS_NAME,
                NEGATIVE_AREA = objAdd.NEGATIVE_AREA,
                CUST_PROFILE_FLAG = objAdd.CUST_PROFILE_FLAG,
                INCOME_DOC_STAT_FLAG = objAdd.INCOME_DOC_STAT_FLAG,
                APPL_SIGNED_FLAG = objAdd.APPL_SIGNED_FLAG,
                ISSUERNAME = objAdd.ISSUERNAME,
                RESIDENCETYPE = objAdd.RESIDENCETYPE,
                VC_DELIVERY_OP = objAdd.VC_DELIVERY_OP,
                VC_ADDON_PRODUCT = objAdd.VC_ADDON_PRODUCT,
                VC_BANK_NAME = objAdd.VC_BANK_NAME,
                N_AMOUNT = objAdd.N_AMOUNT,
                VC_ALIAS_NAME = objAdd.VC_ALIAS_NAME,
                FATHERNAME = objAdd.FATHERNAME,
                N_CARS_OWN = objAdd.N_CARS_OWN,
                SC_ST_FLAG = objAdd.SC_ST_FLAG,
                RENTPERMONTH = objAdd.RENTPERMONTH,
                CUST_ID_N = objAdd.CUST_ID_N,
                LIABILITY = objAdd.LIABILITY,
                STAFF_ID = objAdd.STAFF_ID,
                EL = objAdd.EL,
                EAD = objAdd.EAD,
                FEE_CODE = objAdd.FEE_CODE,
                PROB_DEFAULT = objAdd.PROB_DEFAULT,
                USER_DEF_FIELD1 = objAdd.USER_DEF_FIELD1,
                USER_DEF_FIELD2 = objAdd.USER_DEF_FIELD2,
                USER_DEF_FIELD3 = objAdd.USER_DEF_FIELD3,
                USER_DEF_FIELD4 = objAdd.USER_DEF_FIELD4,
                USER_DEF_FIELD5 = objAdd.USER_DEF_FIELD5,
                EMPL_DESIGNATION = objAdd.EMPL_DESIGNATION,
                EMPL_DEPARTMENT = objAdd.EMPL_DEPARTMENT,
                EMPL_CURRENT_POSITION = objAdd.EMPL_CURRENT_POSITION,
                SPOUSE_WORKING_STATUS = objAdd.SPOUSE_WORKING_STATUS,
                DEMAT_ACCOUNT_NUMBER = objAdd.DEMAT_ACCOUNT_NUMBER,
                EXISTING_CREDIT_CARD_LIMIT = objAdd.EXISTING_CREDIT_CARD_LIMIT,
                CREDIT_AS_PERCENT_OF_INCOME = objAdd.CREDIT_AS_PERCENT_OF_INCOME,
                MIDDLE_NAME = objAdd.MIDDLE_NAME,
                ADDITIONAL_CARD_MIDDLE_NAME = objAdd.ADDITIONAL_CARD_MIDDLE_NAME,
                ADDITIONAL_GENDER = objAdd.ADDITIONAL_GENDER,
                WITNESSED_BY = objAdd.WITNESSED_BY,
                BIRTH_DATE_NOMINEE = objAdd.BIRTH_DATE_NOMINEE,
                REFERENCE_EMPLOYEE_ID = objAdd.REFERENCE_EMPLOYEE_ID,
                EMPL_ID = objAdd.EMPL_ID,
                APPLICATION_TYPE = objAdd.APPLICATION_TYPE,
                DSA_CODE = objAdd.DSA_CODE,
                VERIFICATION_AGENCY_CODE = objAdd.VERIFICATION_AGENCY_CODE,
                VOTER_ID = objAdd.VOTER_ID,
                STAFF_E_C_NO = objAdd.STAFF_E_C_NO,
                STAFF_NAME = objAdd.STAFF_NAME,
                STAFF_BRANCH = objAdd.STAFF_BRANCH,
                RECOMMENDED_BY = objAdd.RECOMMENDED_BY,
                DOM = objAdd.DOM,
                UNIVERSITY = objAdd.UNIVERSITY,
                FATHER_NAME = objAdd.FATHER_NAME,
                SPOUSE_MOB_NO = objAdd.SPOUSE_MOB_NO,
                RESIDANCE_CHANGED = objAdd.RESIDANCE_CHANGED,
                OTHER_INCOME = objAdd.OTHER_INCOME,
                INCOME_PER_MONTH = objAdd.INCOME_PER_MONTH,
                CUSTOMER_ID = objAdd.CUSTOMER_ID,
                TAX_PAID = objAdd.TAX_PAID,
                YEAR_TAX_PAID = objAdd.YEAR_TAX_PAID,
                OTHER_BRANCH = objAdd.OTHER_BRANCH,
                OTHER_CITY = objAdd.OTHER_CITY,
                HOUSING_LOAN = objAdd.HOUSING_LOAN,
                CAR_LOAN = objAdd.CAR_LOAN,
                CONSUMER_LOAN = objAdd.CONSUMER_LOAN,
                BUSINESS_LOAN = objAdd.BUSINESS_LOAN,
                OTHR_LOAN = objAdd.OTHR_LOAN,
                TYPE_OF_LOAN_OTHERS = objAdd.TYPE_OF_LOAN_OTHERS,
                LOAN_AMOUNT = objAdd.LOAN_AMOUNT,
                CURRENT_OUTSTANDING = objAdd.CURRENT_OUTSTANDING,
                DURATION_OF_LOAN = objAdd.DURATION_OF_LOAN,
                LOAN_INSTUTION_NAME = objAdd.LOAN_INSTUTION_NAME,
                LOAN_BRANCH = objAdd.LOAN_BRANCH,
                BOB_DEBIT_CARD_NO = objAdd.BOB_DEBIT_CARD_NO,
                DC_VALID_UPTO = objAdd.DC_VALID_UPTO,
                CC_BANK_NAME1 = objAdd.CC_BANK_NAME1,
                CC_NO1 = objAdd.CC_NO1,
                CC_VALID_UPTO1 = objAdd.CC_VALID_UPTO1,
                CC_CR_LITMIT1 = objAdd.CC_CR_LITMIT1,
                CC_BANK_NAME2 = objAdd.CC_BANK_NAME2,
                CC_NO2 = objAdd.CC_NO2,
                CC_VALID_UPTO2 = objAdd.CC_VALID_UPTO2,
                CC_CR_LITMIT2 = objAdd.CC_CR_LITMIT2,
                CC_BANK_NAME3 = objAdd.CC_BANK_NAME3,
                CC_NO3 = objAdd.CC_NO3,
                CC_VALID_UPTO3 = objAdd.CC_VALID_UPTO3,
                CC_CR_LITMIT3 = objAdd.CC_CR_LITMIT3,
                SEC2_TITLE = objAdd.SEC2_TITLE,
                SEC2_FIRST_NAME = objAdd.SEC2_FIRST_NAME,
                SEC2_MIDDLE_NAME = objAdd.SEC2_MIDDLE_NAME,
                SEC2_LAST_NAME = objAdd.SEC2_LAST_NAME,
                SEC2_EMBOSS_NAME = objAdd.SEC2_EMBOSS_NAME,
                SEC2_BIRTH_DATE = objAdd.SEC2_BIRTH_DATE,
                SEC2_GENDER = objAdd.SEC2_GENDER,
                SEC2_RELATION = objAdd.SEC2_RELATION,
                REF_NAME1 = objAdd.REF_NAME1,
                REF_ADDR1 = objAdd.REF_ADDR1,
                REF_CITY1 = objAdd.REF_CITY1,
                REF_TELEPH1 = objAdd.REF_TELEPH1,
                REF_ZIP_CODE1 = objAdd.REF_ZIP_CODE1,
                REF_NAME2 = objAdd.REF_NAME2,
                REF_ADDR2 = objAdd.REF_ADDR2,
                REF_CITY2 = objAdd.REF_CITY2,
                REF_TELEPH2 = objAdd.REF_TELEPH2,
                REF_ZIP_CODE2 = objAdd.REF_ZIP_CODE2,
                SEC2_APPLICANT_PROF = objAdd.SEC2_APPLICANT_PROF,
                SEC1_APPLICANT_PROF = objAdd.SEC1_APPLICANT_PROF,
                PERM_EMAIL_ID = objAdd.PERM_EMAIL_ID,
                PERM_MOBILE_NUMBER = objAdd.PERM_MOBILE_NUMBER,
                EMPL_EMAIL_ID = objAdd.EMPL_EMAIL_ID,
                EMPL_MOBILE_NUMBER = objAdd.EMPL_MOBILE_NUMBER,
                REF1_EMAIL_ID = objAdd.REF1_EMAIL_ID,
                REF1_MOBILE_NUMBER = objAdd.REF1_MOBILE_NUMBER,
                RECOMMENDED_BRANCH = objAdd.RECOMMENDED_BRANCH,
                APPLICANT_PHOTO = objAdd.APPLICANT_PHOTO,
                CURR_ADDRESS1 = objAdd.CURR_ADDRESS1

            };
        }

    }

    public class ClassA
    {
        public string COLUMN_NAME { get; set; }
        public string PropertysValue { get; set; }
        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }

    }
}
