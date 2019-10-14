using System;

namespace CardHolder.DTO
{
    public class CH_Complaint_DtlDTO
    {
        public System.Int64 Complaint_Dtl_Id { get; set; }

        public System.DateTime Complaint_Dt { get; set; }

        public System.Int64 CardHolder_Id { get; set; }

        public System.Int64 ComplaintType_Id { get; set; }

        public System.String Remarks { get; set; }

        public System.String Complaint_Status { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public Nullable<System.Int64> Updated_by { get; set; }

        public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }

        public System.String ComplaintType { get; set; }

        public System.String UID { get; set; }

        public System.String AdminRemarks { get; set; }
    }
}