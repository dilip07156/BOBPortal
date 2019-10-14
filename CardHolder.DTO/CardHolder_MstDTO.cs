using System;

namespace CardHolder.DTO
{
    public class CardHolder_MstDTO
    {
        public System.Int64 CardHolder_Id { get; set; }

        public System.Int64 Oracle_Customer_Id { get; set; }

        public System.String User_nm { get; set; }

        public System.String User_pwd { get; set; }

        public System.String UID { get; set; }

        public System.String Profile_Photo { get; set; }

        public System.String AddOn1_Photo { get; set; }

        public System.String AddOn2_Photo { get; set; }

        public System.String AddOn3_Photo { get; set; }

        public System.String Personal_Msg { get; set; }

        public Nullable<Boolean> IsActive { get; set; }

        public Nullable<Boolean> IsPermanentDisable { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public Nullable<System.Int64> Updated_by { get; set; }

        public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }

        public string credit_card_number { get; set; }

        public string creditcard_acc_number { get; set; }

        public CH_CardDTO CH_Card { get; set; }

      //  public Nullable<Boolean> IsLoggedInCurrently { get; set; }

        public Nullable<System.DateTime> InvalidLastLoginDt { get; set; }

        public System.String LocalIP_Address { get; set; }

        public Nullable<System.DateTime> CurrentLoginDate { get; set; }

        public Nullable<System.DateTime> LastLoginDate { get; set; }

        public String MdfHashingPwd { get; set; }

        public string FULL_NAME { get; set; }


    }
}