using System;

namespace CardHolder.DTO
{
    public class CH_Payment_Status_DtlDTO
    {
        public Int64 PaymentStatus_Id { get; set; }

        public String Creditcard_acc_number { get; set; }

        public String Credit_card_number { get; set; }

        public String Transaction_number { get; set; }

        public String TxnReferenceNo { get; set; }

        public double AmountDue { get; set; }

        public String PaymentStatus { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        public String BillDeskOnlineID { get; set; }

        public String BankRefNo { get; set; }

        public String BankId { get; set; }

        public String AuthStatus { get; set; }

        //public Nullable<System.Int64> Updated_by { get; set; }

        //public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }

    }

    public class CH_Bobibanking_Payment_Status_DtlDTO
    {
        public Int64 BobiBanking_PaymentStatus_Id { get; set; }

        public String Creditcard_acc_number { get; set; }

        public String Credit_card_number { get; set; }

        public String BankId { get; set; }

        public String PID { get; set; }

        public String PRN { get; set; }

        public String BID { get; set; }

        public string DebtAccountNo { get; set; }

        public String ITC { get; set; }

        public double AmountDue { get; set; }

        public String PaymentStatus { get; set; }

        public System.Int64 Created_by { get; set; }

        public System.DateTime Created_dt { get; set; }

        //public Nullable<System.Int64> Updated_by { get; set; }

        //public Nullable<System.DateTime> Updated_dt { get; set; }

        public System.String IP_Address { get; set; }
    }
}
