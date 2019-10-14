using System;
using CardHolder.DTO;
using CardHolderOracle.DAL;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class AccountSummaryManager
    {

        //    #region Repository Members
        //    #endregion

        #region Selection Methods

        /// <summary>
        /// Gets the account summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetAccountSummary(string accountNumber)
        {
            CH_CardDTO objAccountsummary = new CH_CardDTO();
            objAccountsummary = AccountDALC.GetAccountSummary(accountNumber);
            return objAccountsummary;

            //CH_CardDTO objAccountsummary = new CH_CardDTO();
            //objAccountsummary.Account_Total_Outstanding = 5000;
            //objAccountsummary.Account_UnBilled_Outstanding = -292;
            //objAccountsummary.Account_Total_Account_Limit = 50000;
            //objAccountsummary.Account_Avl_Account_Limit = 5000;
            //objAccountsummary.Account_Total_Account_Cash_Limit = 5000;
            //objAccountsummary.Account_Avl_Account_Cash_Limit = 4000;
            //return objAccountsummary;
        }
        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CR_TERMDTO GetSummary(string accountNumber)
        {
            CH_CR_TERMDTO objLastBillSummary = new CH_CR_TERMDTO();
            objLastBillSummary = AccountDALC.GetSummary(accountNumber);
            return objLastBillSummary;
        }

        /// <summary>
        /// Gets the reward points summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CR_TERMDTO GetRewardPointsSummary(string accountNumber)
        {
            CH_CR_TERMDTO objRewardPointsSummary = new CH_CR_TERMDTO();
            objRewardPointsSummary = AccountDALC.GetRewardPointsSummary(accountNumber);
            return objRewardPointsSummary;

        }

        /// <summary>
        /// Gets the date to display.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime GetDateToDisplay(string accountNumber)
        {
            return AccountDALC.GetDateToDisplay(accountNumber);
        }

        /// <summary>
        /// Gets the date for card summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime GetDateForCardSummary(string accountNumber)
        {
            return AccountDALC.GetDateForCardSummary(accountNumber);
        }

        /// <summary>
        /// Gets the bonus points.
        /// </summary>
        /// <param name="CR_ACCOUNT_NBR">The C r_ ACCOUN t_ NBR.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetBonusPoints(string CR_ACCOUNT_NBR)
        {
            return AccountDALC.GetBonusPoints(CR_ACCOUNT_NBR);
        }

        #endregion

    }
}
