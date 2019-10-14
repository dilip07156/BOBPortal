using System;

namespace CardHolder.DTO
{
    public class CH_CardDTO
    {
        public string Cr_Account_Nbr { get; set; }
        public string card_number { get; set; }
        public string Embossed_Name { get; set; }
        public Double Account_Total_Outstanding { get; set; }
        public Double Account_UnBilled_Outstanding { get; set; }
        public Double Account_Total_Account_Limit { get; set; }
        public Double Account_Avl_Account_Limit { get; set; }
        public Double Account_Total_Account_Cash_Limit { get; set; }
        public Double Account_Avl_Account_Cash_Limit { get; set; }
        public Double Card_Total_Outstanding { get; set; }
        public Double Card_UnBilled_Outstanding { get; set; }
        public Double Card_Total_Account_Limit { get; set; }
        public Double Card_Avl_Account_Limit { get; set; }
        public Double Card_Total_Account_Cash_Limit { get; set; }
        public Double Card_Avl_Account_Cash_Limit { get; set; }


        public int? CARD_SEQ { get; set; }
        public string CLIENT_CODE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string AO_CODE { get; set; }
        public string VIP_FLAG { get; set; }
        public string BASIC_CARD_FLAG { get; set; }
        public string BASIC_CARD_NUMBER { get; set; }
        public string TITLE { get; set; }
        public DateTime? BIRTH_DATE { get; set; }
        public string FAMILY_NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string ADDRESS4 { get; set; }
        public string ZIP_CODE { get; set; }
        public string PREFERRED_MAILING_ADDRESS { get; set; }
        public string CITY_CODE { get; set; }
        public string COUNTRY_CODE { get; set; }
        public DateTime? OPENING_DATE { get; set; }
        public string TARIFF_CODE { get; set; }
        public int? LIMIT_CASH_DOM { get; set; }
        public int? LIMIT_PURCH_DOM { get; set; }
        public int? LIMIT_TE_DOM { get; set; }
        public int? LIMIT_CASH_INT { get; set; }
        public int? LIMIT_PURCH_INT { get; set; }
        public int? LIMIT_TE_INT { get; set; }
        public int? AUTHO_LIMIT_DOM { get; set; }
        public int? AUTHO_LIMIT_INT { get; set; }
        public int? ATM_OFFLINE_LIMIT { get; set; }
        public int? TOTAL_CARD_LIMIT { get; set; }
        public int? TOTAL_ACCOUNT_LIMIT { get; set; }
        public int? TOTAL_CLIENT_LIMIT { get; set; }
        public int? AVL_CARD_LIMIT { get; set; }
        public int? AVL_ACCOUNT_LIMIT { get; set; }
        public int? AVL_CLIENT_LIMIT { get; set; }
        public int? TOTAL_CARD_CASH_LIMIT { get; set; }
        public int? TOTAL_ACCOUNT_CASH_LIMIT { get; set; }
        public int? TOTAL_CLIENT_CASH_LIMIT { get; set; }
        public int? AVL_CARD_CASH_LIMIT { get; set; }
        public int? AVL_ACCOUNT_CASH_LIMIT { get; set; }
        public int? AVL_CLIENT_CASH_LIMIT { get; set; }
        public DateTime? LAST_PAYMENT_DATE { get; set; }
        public int? LAST_PAYMENT_AMOUNT { get; set; }
        public string ACC_ADM_STATUS { get; set; }
        public int? STATUS_CODE { get; set; }
        public int? DIRECT_DEBIT_AMOUNT_FLAG { get; set; }
        public int DIRECT_DEBIT_PERCENTAGE { get; set; }
        public string CURRENT_ACC_BRANCH { get; set; }
        public string CURRENT_ACC_NBR { get; set; }
        public string BILLDESK_ONLINE_ID { get; set; }
        public string BILLDESK_STATUS { get; set; }
        public string BRANCH_REF_NUMBER { get; set; }
        public int? PIN_PRODUCTION_COUNT { get; set; }
        public string EMAIL_ID { get; set; }
        public int? LIMIT_IBFT_DOM { get; set; }
        public int? LIMIT_IBFT_INT { get; set; }
        public int? PURCH_OFFLINE_LIMIT { get; set; }
        public string MAILING_ADDRESS1 { get; set; }
        public string MAILING_ADDRESS2 { get; set; }
        public string MAILING_ADDRESS3 { get; set; }
        public string MAILING_ADDRESS4 { get; set; }
        public string MAILING_ZIP_CODE { get; set; }
        public string MAILING_CITY_CODE { get; set; }
        public string MAILING_COUNTRY_CODE { get; set; }
        public string PHONE_HOME { get; set; }
        public string PHONE_ALTERNATE { get; set; }
        public string PHONE_MOBILE { get; set; }
        public string PICTURE_CODE { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string PERMANENT_PHONE1 { get; set; }
        public string PERMANENT_PHONE2 { get; set; }
        public string PERMANENT_MOBILE { get; set; }
        public string STAFF_ID { get; set; }

        public int? EXPIRY_MONTH { get; set; }
        public int? EXPIRY_YEAR { get; set; }
        public string FULL_NAME { get; set; }
        public string CITY_NAME { get; set; }


        //For Card Fees
        public double PIN_CAL_FEES { get; set; }
        public double RESTATEMENT_THRU_EMAIL_CHARGES { get; set; }
        public double STMT_REG_AMT { get; set; }
        public double REPLACEMENT_CHARGES { get; set; }
        public double RENEWAL_CHARGES { get; set; }
        public double EMI_PROCESSING_FEES { get; set; }
        public double LOAN_PROCESSING_FEES { get; set; }



        //For ATM_PIN details
        public DateTime PIN_REGENERATION_DATE { get; set; }
        public string ATMP_PIN_REQUEST_NUMBER { get; set; }

        //For AutoDebitPaymentType
        public string AUTO_DEBIT_MODE { get; set; }
        public string AUTO_DEBIT_BRANCH { get; set; }

        //For PreserveStmnt details
        public DateTime PRESERVE_STMNT_GENERATION_DATE { get; set; }
        public string PRESERVE_STMNT_REQUEST_NUMBER { get; set; }
        public string FOR_MONTH { get; set; }

        public string MASK_CARD_NUMBER { get; set; }


        public static string FeeCharge { get; set; }

        public int? LIMIT_PURCH_INT_resp { get; set; }

        public double Credit_Limit { get; set; }

        public string AUTO_DEBIT_STATUS { get; set; }

        public string AUTO_DEBIT_TYPE { get; set; }

       
    }
}
