using System;

namespace CardHolder.DTO
{
    public class CH_Request_DtlDTO
    {
        public System.Int64 Request_Dtl_Id { get; set; }

        public System.DateTime Request_Dt { get; set; }

        public System.Int64 CardHolder_Id { get; set; }

        public System.Int64 RequestType_Id { get; set; }

        public Nullable<System.Int64> RequestReason_Id { get; set; }

        public System.String Mode_Send_Statment { get; set; }

        public Nullable<System.DateTime> DOB { get; set; }

        public System.String Relation { get; set; }

        public System.String Gender { get; set; }

        public System.String Addon_Profile_Photo { get; set; }

        public System.String Payment_Type { get; set; }

        public Nullable<System.Decimal> Specific_Monthly_due { get; set; }

        public Nullable<System.Int32> Points_Wants_Redeem { get; set; }

        public System.String Bank_nm { get; set; }

        public Nullable<System.Decimal> Transferred_Amt { get; set; }

        public System.String Balance_Transferred_Plan { get; set; }

        public Nullable<System.Decimal> EMI_Principal_Amt { get; set; }

        public Nullable<System.Int32> EMI_Terms { get; set; }

        public System.String Remarks { get; set; }

        public System.String Request_Status { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public Nullable<System.Int64> Updated_by { get; set; }

        public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }

        public System.String Statement_month { get; set; }

        public System.String Statement_year { get; set; }

        public System.String Add_On_Card_Applicant { get; set; }

        public System.String RequestType { get; set; }

        public Nullable<global::System.Decimal> EMI_Amount { get; set; }

        public Nullable<global::System.Decimal> EMI_InterestRate { get; set; }

        public global::System.String OtherCreditCardNumber { get; set; }

        public System.String UID { get; set; }

        public System.String AdminRemarks { get; set; }

        public global::System.String HotlistingCardNumber { get; set; }

        public Nullable<System.Decimal> Loan_Principal_Amt { get; set; }

        public Nullable<System.Int32> Loan_Terms { get; set; }

        public Nullable<global::System.Decimal> Loan_InterestRate { get; set; }

        public Nullable<global::System.Decimal> Loan_Amount { get; set; }

        public string RequestFlag { get; set; }

        public string RequestorComplaint { get; set; }

        public string request_Microfilm_Ref_NumberParameter { get; set; }

        ///// <summary>
        ///// MSSQL CardHolder_Mst
        ///// </summary>
        //public System.Int64 Oracle_Customer_Id { get; set; }

        ///// <summary>
        ///// Oracle Customer_Mst
        ///// </summary>
        //public string CreditCard_Acc_Num { get; set; }

        ///// <summary>
        ///// Oracle Customer_Mst
        ///// </summary>
        //public string Credit_Card_Num { get; set; }

        ///// <summary>
        ///// Oracle Customer_Mst
        ///// </summary>
        //public string NameOnCard { get; set; }

        ///// <summary>
        ///// Oracle Customer_Mst
        ///// </summary>
        //public string Reason_nm { get; set; }
    }
}