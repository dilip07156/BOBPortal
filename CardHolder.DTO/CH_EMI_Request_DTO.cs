using System;

namespace CardHolder.DTO
{
    public class CH_EMI_Request_DTO
    {
        public Int64 EMI_Id { get; set; }
        public string Oracle_EMI_Id { get; set; }
        public string Creditcard_acc_number { get; set; }
        public string EMI_Loan_Type { get; set; }
        public System.Int64 Created_by { get; set; }
        public System.DateTime Created_dt { get; set; }
        public Nullable<System.Int64> Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_dt { get; set; }
        public System.String IP_Address { get; set; }
    }
}
