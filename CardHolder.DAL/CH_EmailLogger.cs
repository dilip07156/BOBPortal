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
    
    public partial class CH_EmailLogger
    {
        public long Id { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public long SendBy { get; set; }
        public System.DateTime SendDate { get; set; }
        public string EmailStatus { get; set; }
    }
}
