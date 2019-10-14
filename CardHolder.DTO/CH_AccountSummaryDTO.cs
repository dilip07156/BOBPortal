using System;

namespace CardHolder.DTO
{
    public class CH_AccountSummaryDTO
    {     
        public Double Total_Outstanding { get; set; }
        public Double UnBilled_Outstanding { get; set; }
        public Double Total_Account_Limit { get; set; }
        public Double Avl_Account_Limit { get; set; }
        public Double Total_Account_Cash_Limit { get; set; }
        public Double Avl_Account_Cash_Limit { get; set; }
    }
    public class CH_LastBillSummaryDTO
    {
        public Double Total_Amount_Due { get; set; }
        public Double Total_Outstanding { get; set; }
        public Double Minimum_Amount_Due { get; set; }
        public DateTime Payment_Due_Date { get; set; }
        public Double Amount_Received { get; set; }
        public DateTime Paymnet_Received_Date { get; set; }
    }
    public class CH_RewardPointSummaryDTO
    {
        public Double Opening_Balance { get; set; }
        public Double Earned_For_The_Month { get; set; }
        public Double Redeemed_For_The_Month { get; set; }
        public Double Closing_Balance { get; set; }
        public Double Points_Expiring { get; set; }
    }
}
