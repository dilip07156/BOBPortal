using System;

namespace CardHolder.DTO
{
    public class CH_CR_TERMDTO
    {
        public Double Total_Amount_Due { get; set; }
        public Double Total_Outstanding { get; set; }
        public Double Minimum_Amount_Due { get; set; }
        public DateTime Payment_Due_Date { get; set; }
        public Double Amount_Received { get; set; }
        public DateTime Paymnet_Received_Date { get; set; }
        public Double Opening_Balance { get; set; }
        public Double Earned_For_The_Month { get; set; }
        public Double Redeemed_For_The_Month { get; set; }
        public Double Closing_Balance { get; set; }
        public Double Points_Expiring { get; set; }
        public int? Row_Num { get; set; }
        public DateTime Stat_Date { get; set; }
       // public Double Billed_Opening_Bal { get; set; }
        public Double Total_Payment { get; set; }
        public Double BILLED_OPENING_BAL { get; set; }
        public Double Billed_Closing_Bal { get; set; }
        public Double Payment_Charges { get; set; }
        public string Client_Code { get; set; } // will come from Card table by join - require due to PDF display
        public string Card_Number { get; set; } // will come from Card table by join - require due to PDF display
        public string Pdf_File { get; set; }
        public Double PTS_CLOSING { get; set; }
        public Double PTS_OPENING { get; set; }
        public Double TOTAL_CREDITS { get; set; }
        public Double TOTAL_DEBITS { get; set; }
        public string STATEMENT_MONTH { get; set; }
        public double MINIMUM_PAYMENT_DUE { get; set; }
    }

    public class CH_EVG_EVENTS_QUEUEDTO
    {
        //public string EVE_CLIENT_CODE { get; set; }
        //public string EVE_CARD_NUMBER { get; set; }        
        public string EVE_OUT_FILENAME { get; set; }
    }
}
