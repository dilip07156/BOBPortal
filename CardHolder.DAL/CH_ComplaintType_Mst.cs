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
    
    public partial class CH_ComplaintType_Mst
    {
        public CH_ComplaintType_Mst()
        {
            this.CH_ComplaintReason_Mst = new HashSet<CH_ComplaintReason_Mst>();
            this.CH_Complaint_Dtl = new HashSet<CH_Complaint_Dtl>();
        }
    
        public long ComplaintType_Id { get; set; }
        public string ComplaintType_nm { get; set; }
        public string ComplaintType_Desc { get; set; }
        public long Created_by { get; set; }
        public System.DateTime Created_dt { get; set; }
        public Nullable<long> Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_dt { get; set; }
        public string IP_Address { get; set; }
        public Nullable<int> Dept_Id { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ICollection<CH_ComplaintReason_Mst> CH_ComplaintReason_Mst { get; set; }
        public virtual ICollection<CH_Complaint_Dtl> CH_Complaint_Dtl { get; set; }
    }
}
