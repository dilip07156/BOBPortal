using System;

namespace CardHolder.DTO
{
    public class AuditLog_DTO
    {
        public System.Int64 Id { get; set; }

        public System.Int64 RequestType_Id { get; set; }       

        public System.Int64 CardHolder_Id { get; set; }        

        public string TxnType { get; set; }

        public String Credit_card_number { get; set; }

        public string RequestReason { get; set; }

        public String TxnReferenceNo { get; set; }

        public string  ResponseStatus { get; set; }        

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public Nullable<System.Int64> Updated_by { get; set; }

        public Nullable<System.DateTime> Updated_dt { get; set; }

        public String BankRefNo { get; set; }

        public System.String IP_Address { get; set; }
        
    }
}