//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardHolder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CH_Request_Dtl_log
    {
        public long Log_Id { get; set; }
        public long Request_Dtl_Id { get; set; }
        public System.DateTime Request_Dt { get; set; }
        public long CardHolder_Id { get; set; }
        public long RequestType_Id { get; set; }
        public Nullable<long> RequestReason_Id { get; set; }
        public string Mode_Send_Statment { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Relation { get; set; }
        public string Gender { get; set; }
        public string Addon_Profile_Photo { get; set; }
        public string Payment_Type { get; set; }
        public Nullable<decimal> Specific_Monthly_due { get; set; }
        public Nullable<int> Points_Wants_Redeem { get; set; }
        public string Bank_nm { get; set; }
        public Nullable<decimal> Transferred_Amt { get; set; }
        public string Balance_Transferred_Plan { get; set; }
        public Nullable<decimal> EMI_Principal_Amt { get; set; }
        public Nullable<int> EMI_Terms { get; set; }
        public Nullable<decimal> EMI_InterestRate { get; set; }
        public Nullable<decimal> EMI_Amount { get; set; }
        public string Remarks { get; set; }
        public string Request_Status { get; set; }
        public long Created_by { get; set; }
        public System.DateTime Created_dt { get; set; }
        public Nullable<long> Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_dt { get; set; }
        public string IP_Address { get; set; }
        public string Statement_month { get; set; }
        public string Statement_year { get; set; }
        public string Add_On_Card_Applicant { get; set; }
        public string OtherCreditCardNumber { get; set; }
        public string HotlistingCardNumber { get; set; }
        public Nullable<long> User_Id { get; set; }
        public Nullable<long> Action_Id { get; set; }
        public Nullable<System.DateTime> Log_dt { get; set; }
        public string UID { get; set; }
        public string AdminRemarks { get; set; }
        public Nullable<long> Mode_Request_Complaint_Id { get; set; }
        public Nullable<long> FinalUpdated_by { get; set; }
        public Nullable<System.DateTime> FinalUpdated_dt { get; set; }
        public string Action { get; set; }
        public Nullable<System.DateTime> ActionDateTime { get; set; }
        public Nullable<int> Loan_Terms { get; set; }
        public Nullable<decimal> Loan_InterestRate { get; set; }
        public Nullable<decimal> Loan_Amount { get; set; }
        public Nullable<decimal> Loan_Principal_Amt { get; set; }
    }
}
