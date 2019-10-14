using System;

namespace CardHolder.DTO
{
    public class DropDown_HdrDTO
    {
        public System.Int32 DropDown_Hdr_Id { get; set; }

        public System.String Description { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public Nullable<System.Int64> Updated_by { get; set; }

        public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }
    }
}