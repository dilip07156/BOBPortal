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
    
    public partial class CH_SMSLogger
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public long SendBy { get; set; }
        public System.DateTime SendDate { get; set; }
        public string SMSStatus { get; set; }
    }
}
