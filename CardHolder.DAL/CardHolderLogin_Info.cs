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
    
    public partial class CardHolderLogin_Info
    {
        public long CardHolderLog_InfoId { get; set; }
        public Nullable<long> CardHolder_Id { get; set; }
        public Nullable<int> Login_Attempts { get; set; }
        public Nullable<System.DateTime> Login_Attempt_FirstDt { get; set; }
        public Nullable<System.DateTime> Login_Attempt_SecondDt { get; set; }
        public Nullable<System.DateTime> Login_Attempt_ThirdDt { get; set; }
        public string IP_Address { get; set; }
    }
}
