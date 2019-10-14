using System;

namespace CardHolder.DTO
{
    public class CH_UnbilledUnsettled_TransactionsDTO
    {
        public DateTime Transaction_date { get; set; }
        public string Card_Number { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Merchant_Name { get; set; }
        public string MICROFILM_REF_NUMBER { get; set; }
    }

    public class CH_LOAN_TransactionsDTO
    {
        public DateTime Transaction_date { get; set; }
        public string Card_Number { get; set; }
        public string CR_ACCOUNT_NBR { get; set; }
        public string Description { get; set; }
        public double Credit_Limit { get; set; }
        public double Available_Credit_Limit { get; set; }
        public double PreApproved_Limit { get; set; }
        public string MICROFILM_REF_NUMBER { get; set; }
    }
}
