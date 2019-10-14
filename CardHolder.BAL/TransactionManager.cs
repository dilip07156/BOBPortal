using System;
using System.Collections.Generic;
using CardHolder.DTO;
using CardHolderOracle.DAL;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class TransactionManager
    {
        #region Unbilled Transaction

        #region Repository Members
        #endregion

        #region Selection Methods
        /// <summary>
        /// Gets the unbilled transactions.
        /// </summary>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_UnbilledUnsettled_TransactionsDTO> GetUnbilledTransactions(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, int TransactionType)
        {
            List<CH_UnbilledUnsettled_TransactionsDTO> lstUnbilledTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO>();
            lstUnbilledTransactions = TransactionDALC.GetUnbilledUnSettledTransactions(SkipCount, PageSize, ref RecordCount, accountNumber, TransactionType);
            return lstUnbilledTransactions;
        }


        /// <summary>
        /// Gets the unbilled transactions.
        /// </summary>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_UnbilledUnsettled_TransactionsDTO> GetUnbilledTransactionsForEMI(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, int TransactionType)
        {
            List<CH_UnbilledUnsettled_TransactionsDTO> lstUnbilledTransactionsForEMI = new List<CH_UnbilledUnsettled_TransactionsDTO>();
            lstUnbilledTransactionsForEMI = TransactionDALC.GetUnbilledTransactionsForEMI(SkipCount, PageSize, ref RecordCount, accountNumber, TransactionType);
            return lstUnbilledTransactionsForEMI;
        }

        #endregion

        #endregion

        #region Payment Credit Detials

        #region Repository Members
        #endregion

        #region Selection Methods
        /// <summary>
        /// Gets the payment credit details.
        /// </summary>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="Todate">The todate.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_Cr_Current_Trans> GetPaymentCreditDetails(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, DateTime? FromDate, DateTime? Todate)
        {
            List<CH_Cr_Current_Trans> lstPaymentCredit = new List<CH_Cr_Current_Trans>();
            lstPaymentCredit = TransactionDALC.GetPaymentCreditDetails(SkipCount, PageSize, ref RecordCount, accountNumber, FromDate, Todate);
            return lstPaymentCredit;
        }
        #endregion

        #endregion

        #region GetLoanDetails

        /// <summary>
        /// Gets the Loan transactions.
        /// </summary>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_LOAN_TransactionsDTO> GetTransactionsForLoan(int SkipCount, int PageSize, ref int RecordCount, string accountNumber)
        {
            var lstUnbilledTransactionsForEMI = new List<CH_LOAN_TransactionsDTO>();
            lstUnbilledTransactionsForEMI = TransactionDALC.GetTransactionsForLoan(SkipCount, PageSize, ref RecordCount, accountNumber);
            return lstUnbilledTransactionsForEMI;
        }

        #endregion

    }
}
