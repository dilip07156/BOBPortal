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
    
    public partial class DropDown_Dtl
    {
        public int DropDown_Dtl_Id { get; set; }
        public int DropDown_Hdr_Id { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public long Created_by { get; set; }
        public System.DateTime Created_dt { get; set; }
        public Nullable<long> Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_dt { get; set; }
        public string IP_Address { get; set; }
    
        public virtual DropDown_Hdr DropDown_Hdr { get; set; }
    }
}
