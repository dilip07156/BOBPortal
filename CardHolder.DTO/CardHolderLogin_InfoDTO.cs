using System;

namespace CardHolder.DTO
{
   public class CardHolderLogin_InfoDTO
    {
        #region Primitive Properties

       
        public global::System.Int64 CardHolderLog_InfoId { get; set; }
       
        public Nullable<global::System.Int64> CardHolder_Id { get; set; }
        
        public Nullable<global::System.Int32> Login_Attempts { get; set; }
        
        public Nullable<global::System.DateTime> Login_Attempt_FirstDt { get; set; }
       
        public Nullable<global::System.DateTime> Login_Attempt_SecondDt { get; set; }
       
        public Nullable<global::System.DateTime> Login_Attempt_ThirdDt { get; set; }

        public global::System.String IP_Address { get; set; }
     
        #endregion


    }
}
