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
    
    public partial class CH_Sessions_Dtl
    {
        public long Id { get; set; }
        public long CardHolder_Id { get; set; }
        public string Session_id { get; set; }
        public System.DateTime Last_access_time { get; set; }
        public long Created_by { get; set; }
        public System.DateTime Created_dt { get; set; }
        public Nullable<long> Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_dt { get; set; }
        public string IP_Address { get; set; }
    }
}
