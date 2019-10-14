using System;

namespace CardHolder.DTO
{
    public class Bank_MstDTO
    {
        #region Primitive Properties


        public Int32 Bank_Id { get; set; }

        public String Bank_nm { get; set; }

        //public Nullable<global::System.Int64> Created_By { get; set; }

        //public Nullable<global::System.DateTime> Created_dt { get; set; }

        //public Nullable<global::System.Int64> Updated_By { get; set; }

        //public Nullable<global::System.DateTime> Updated_dt { get; set; }

        //public global::System.String Ip_Address { get; set; }

        //From Oracle
        public string BRANCH_CODE { get; set; }
        public string BRANCH_NAME { get; set; }

        #endregion



    }
}
