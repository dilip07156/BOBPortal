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
    
    public partial class CH_RequestTxn_dtl
    {
        public long Txn_Id { get; set; }
        public System.DateTime Txn_Dt { get; set; }
        public long Request_Dtl_Id { get; set; }
        public long Escalation_Id { get; set; }
        public long Assigned_User_Id { get; set; }
        public Nullable<System.DateTime> Assigned_Dt { get; set; }
        public int Txn_Status { get; set; }
        public long Created_by { get; set; }
        public System.DateTime Created_dt { get; set; }
        public Nullable<long> Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_dt { get; set; }
        public string IP_Address { get; set; }
    
        public virtual CH_Escalation_Mst CH_Escalation_Mst { get; set; }
        public virtual CH_Request_Dtl CH_Request_Dtl { get; set; }
    }
}