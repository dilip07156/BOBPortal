using System;

namespace CardHolder.DTO
{
    public class CH_RequestReason_MstDTO
    {
        public System.Int64 RequestReason_Id { get; set; }

        public System.Int64 RequestType_Id { get; set; }

        public System.String Reason_nm { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public Nullable<System.Int64> Updated_by { get; set; }

        public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }

        public System.String RequestType { get; set; }
    }
}