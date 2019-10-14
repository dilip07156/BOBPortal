using System;
using System.Collections.Generic;
using System.Data;
using CardHolder.DTO;
using Oracle.DataAccess.Client;

namespace CardHolderOracle.DAL
{
    public class TransactionDALC
    {
        #region Variables

        static string errorGenerated = "Error generated : ";
        static string col_Transaction_date = "Transaction_date";
        static string col_Card_Number = "Card_Number";
        static string col_Currency = "Currency";
        static string col_Description = "Description";
        static string col_Amount = "Amount";
        static string col_DETAILS = "DETAILS";
        static string col_MICROFILM_REF_NUMBER = "MICROFILM_REF_NUMBER";

        #endregion

        #region Selection Methods

        #region Unbilled/Unsettled Transactions
        /// <summary>
        /// Gets the unbilled transactions.
        /// </summary>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <returns></returns>
        public static List<CH_UnbilledUnsettled_TransactionsDTO> GetUnbilledUnSettledTransactions(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, int TransactionType)
        {
            List<CH_UnbilledUnsettled_TransactionsDTO> objTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO>();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = string.Empty;
                    if (TransactionType == 1) // All Trasansactions
                    {
                        RecordCount = GetUnbilledTransactionsCount(accountNumber);
                        sql = "select UnbilledTransactions.* from (SELECT ROWNUM as RowNumber,BOBVW_UNBILLEDTRANSACTIONS.* from BOBVW_UNBILLEDTRANSACTIONS where cr_account_nbr=:cr_account_nbr) UnbilledTransactions where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";
                    }
                    else if (TransactionType == 2) // For Unbilled Trasansctions
                    {
                        RecordCount = GetUnbilledTransactionsCount(accountNumber);
                        sql = "select UnbilledTransactions.* from (SELECT ROWNUM as RowNumber,BOBVW_UNBILLEDTRANSACTIONS.* from BOBVW_UNBILLEDTRANSACTIONS where cr_account_nbr=:cr_account_nbr) UnbilledTransactions where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";
                    }
                    else if (TransactionType == 3) // For unsettled Trasanctions
                    {
                        RecordCount = GetUnSettledTransactionsCount(accountNumber);
                        sql = "select UnsettledTransactions.* from (SELECT ROWNUM as RowNumber,BOBVW_UNSETTLEDTRANSACTIONS.* from BOBVW_UNSETTLEDTRANSACTIONS where cr_account_nbr=:cr_account_nbr) UnsettledTransactions where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";
                    }
                    DataSet ds = new DataSet();
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleParameter p2 = new OracleParameter("ROWNUM1", OracleDbType.Int32);
                    p2.Value = SkipCount;
                    cmd.Parameters.Add(p2);
                    OracleParameter p3 = new OracleParameter("ROWNUM2", OracleDbType.Int32);
                    p3.Value = SkipCount + PageSize;
                    cmd.Parameters.Add(p3);

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                        return null;
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CH_UnbilledUnsettled_TransactionsDTO obj = new CH_UnbilledUnsettled_TransactionsDTO();
                            if (dr[col_Transaction_date] != null && Convert.ToString(dr[col_Transaction_date]) != "")
                                obj.Transaction_date = Convert.ToDateTime(dr[col_Transaction_date]);
                            else
                                obj.Transaction_date = DateTime.MinValue;
                            obj.Card_Number = dr[col_Card_Number] == null ? "" : Convert.ToString(dr[col_Card_Number]);
                            obj.Currency = dr[col_Currency] == null ? "" : Convert.ToString(dr[col_Currency]);
                            obj.Description = dr[col_Description] == null ? "" : Convert.ToString(dr[col_Description]);
                            if (TransactionType == 1)
                                obj.MICROFILM_REF_NUMBER = dr[col_MICROFILM_REF_NUMBER] == null ? "" : Convert.ToString(dr[col_MICROFILM_REF_NUMBER]);
                            if (dr[col_Amount] != null && Convert.ToString(dr[col_Amount]) != "")
                                obj.Amount = Convert.ToDouble(dr[col_Amount]);
                            else
                                obj.Amount = 0;
                            //if (TransactionType == 1)
                            obj.Merchant_Name = Convert.ToString(dr["MERCHANT_NAME"]) == "" ? "" : Convert.ToString(dr["MERCHANT_NAME"]);
                            //if (TransactionType == 2)
                            //    obj.Merchant_Name = "test";
                            objTransactions.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " UnbilledUnSettledTransactions :" + ex.Message.ToString());
                return null;
            }
            return objTransactions;
        }
        /// <summary>
        /// Gets the unbilled transactions count.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        private static int GetUnbilledTransactionsCount(string accountNumber)
        {
            int totalCount = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
                {
                    connection.Open();
                    string sql = "SELECT count(*) from BOBVW_UNBILLEDTRANSACTIONS where cr_account_nbr=:cr_account_nbr";
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " UnbilledTransactionsCount :" + ex.Message.ToString());
                return 0;
            }
            return totalCount;
        }









        /// <summary>
        /// Gets the un settled transactions count.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        private static int GetUnSettledTransactionsCount(string accountNumber)
        {
            int totalCount = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
                {
                    connection.Open();
                    string sql = "SELECT count(*) from BOBVW_UNSETTLEDTRANSACTIONS where cr_account_nbr=:cr_account_nbr";
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " UnSettledTransactionsCount :" + ex.Message.ToString());
                return 0;
            }
            return totalCount;
        }
        #endregion

        #region UnbilledForEmi

        public static List<CH_UnbilledUnsettled_TransactionsDTO> GetUnbilledTransactionsForEMI(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, int TransactionType)
        {
            List<CH_UnbilledUnsettled_TransactionsDTO> objTransactions = new List<CH_UnbilledUnsettled_TransactionsDTO>();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = string.Empty;
                    if (TransactionType == 1)
                    {
                        RecordCount = GetUnbilledTransactionsCountForEMI(accountNumber);
                        sql = "select UnbilledTxn_FOREMI.* from (SELECT ROWNUM as RowNumber,BOBVW_UNBILLEDTXN_FOREMI.* from BOBVW_UNBILLEDTXN_FOREMI where cr_account_nbr=:cr_account_nbr) UnbilledTxn_FOREMI where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";
                    }
                    //else if (TransactionType == 2)
                    //{
                    //    RecordCount = GetUnSettledTransactionsCount(accountNumber);
                    //    sql = "select UnsettledTransactions.* from (SELECT ROWNUM as RowNumber,BOBVW_UNSETTLEDTRANSACTIONS.* from BOBVW_UNSETTLEDTRANSACTIONS where cr_account_nbr=:cr_account_nbr) UnsettledTransactions where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";
                    //}
                    DataSet ds = new DataSet();
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleParameter p2 = new OracleParameter("ROWNUM1", OracleDbType.Int32);
                    p2.Value = SkipCount;
                    cmd.Parameters.Add(p2);
                    OracleParameter p3 = new OracleParameter("ROWNUM2", OracleDbType.Int32);
                    p3.Value = SkipCount + PageSize;
                    cmd.Parameters.Add(p3);

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                        return null;
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CH_UnbilledUnsettled_TransactionsDTO obj = new CH_UnbilledUnsettled_TransactionsDTO();
                            if (dr[col_Transaction_date] != null && Convert.ToString(dr[col_Transaction_date]) != "")
                                obj.Transaction_date = Convert.ToDateTime(dr[col_Transaction_date]);
                            else
                                obj.Transaction_date = DateTime.MinValue;
                            obj.Card_Number = dr[col_Card_Number] == null ? "" : Convert.ToString(dr[col_Card_Number]);
                            obj.Currency = dr[col_Currency] == null ? "" : Convert.ToString(dr[col_Currency]);
                            obj.Description = dr[col_Description] == null ? "" : Convert.ToString(dr[col_Description]);
                            if (TransactionType == 1)
                                obj.MICROFILM_REF_NUMBER = dr[col_MICROFILM_REF_NUMBER] == null ? "" : Convert.ToString(dr[col_MICROFILM_REF_NUMBER]);
                            if (dr[col_Amount] != null && Convert.ToString(dr[col_Amount]) != "")
                                obj.Amount = Convert.ToDouble(dr[col_Amount]);
                            else
                                obj.Amount = 0;
                            //if (TransactionType == 1)
                            obj.Merchant_Name = Convert.ToString(dr["MERCHANT_NAME"]) == "" ? "" : Convert.ToString(dr["MERCHANT_NAME"]);
                            //if (TransactionType == 2)
                            //    obj.Merchant_Name = "test";
                            objTransactions.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " UnbilledTransactionsFOREMI :" + ex.Message.ToString());
                return null;
            }
            return objTransactions;
        }
        /// <summary>
        /// Gets the unbilled transactions count.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        private static int GetUnbilledTransactionsCountForEMI(string accountNumber)
        {
            int totalCount = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
                {
                    connection.Open();
                    string sql = "SELECT count(*) from BOBVW_UNBILLEDTXN_FOREMI where cr_account_nbr=:cr_account_nbr";
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " UnbilledTransactionsFOREMICount :" + ex.Message.ToString());
                return 0;
            }
            return totalCount;
        }


        #endregion

        #region Payment Credit Details
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
        public static List<CH_Cr_Current_Trans> GetPaymentCreditDetails(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, DateTime? FromDate, DateTime? Todate)
        {
            List<CH_Cr_Current_Trans> objPaymentCreditDetails = new List<CH_Cr_Current_Trans>();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = string.Empty;
                    RecordCount = GetPaymentCreditDetailsCount(accountNumber, FromDate, Todate);
                    sql = "select PaymentCreditDetails.* from (SELECT ROWNUM as RowNumber,BOBVW_PAYMENTCREDITDETAILS.* from BOBVW_PAYMENTCREDITDETAILS where (trunc(TRANSACTION_DATE) between :p_from_date and :p_to_date) and cr_account_nbr=:cr_account_nbr) PaymentCreditDetails where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";
                    DataSet ds = new DataSet();
                    OracleCommand cmd = new OracleCommand(sql, conn);


                    OracleParameter p1 = new OracleParameter("p_from_date", OracleDbType.Date);
                    p1.Value = FromDate;
                    cmd.Parameters.Add(p1);

                    OracleParameter p2 = new OracleParameter("p_to_date", OracleDbType.Date);
                    p2.Value = Todate;
                    cmd.Parameters.Add(p2);

                    OracleParameter p3 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p3.Value = accountNumber;
                    cmd.Parameters.Add(p3);

                    OracleParameter p4 = new OracleParameter("ROWNUM1", OracleDbType.Int32);
                    p4.Value = SkipCount;
                    cmd.Parameters.Add(p4);
                    OracleParameter p5 = new OracleParameter("ROWNUM2", OracleDbType.Int32);
                    p5.Value = SkipCount + PageSize;
                    cmd.Parameters.Add(p5);



                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                        return null;
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CH_Cr_Current_Trans obj = new CH_Cr_Current_Trans();
                            if (dr[col_Transaction_date] != null)
                                obj.Transaction_Date = Convert.ToDateTime(dr[col_Transaction_date]);
                            else
                                obj.Transaction_Date = DateTime.MinValue;
                            obj.Description = dr[col_Description] == null ? "" : Convert.ToString(dr[col_Description]);
                            if (dr[col_Amount] != null && Convert.ToString(dr[col_Amount]) != "")
                                obj.Amount = Convert.ToDouble(dr[col_Amount]);
                            else
                                obj.Amount = 0;
                            obj.DETAILS = dr[col_DETAILS] == null ? "" : Convert.ToString(dr[col_DETAILS]);
                            objPaymentCreditDetails.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " PaymentCreditDetails :" + ex.Message.ToString());
                return null;
            }
            return objPaymentCreditDetails;
        }
        /// <summary>
        /// Gets the unbilled transactions count.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        private static int GetPaymentCreditDetailsCount(string accountNumber, DateTime? FromDate, DateTime? Todate)
        {
            int totalCount = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
                {
                    connection.Open();
                    string sql = "SELECT count(*) from BOBVW_PAYMENTCREDITDETAILS where (trunc(TRANSACTION_DATE) between :p_from_date and :p_to_date) and cr_account_nbr=:cr_account_nbr ";
                    OracleCommand cmd = new OracleCommand(sql, connection);

                    OracleParameter p1 = new OracleParameter("p_from_date", OracleDbType.Date);
                    p1.Value = FromDate;
                    cmd.Parameters.Add(p1);

                    OracleParameter p2 = new OracleParameter("p_to_date", OracleDbType.Date);
                    p2.Value = Todate;
                    cmd.Parameters.Add(p2);
                    OracleParameter p3 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p3.Value = accountNumber;
                    cmd.Parameters.Add(p3);

                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " PaymentCreditDetailsCount :" + ex.Message.ToString());
                return 0;
            }
            return totalCount;
        }
        #endregion

        #region LoanDetails

        public static List<CH_LOAN_TransactionsDTO> GetTransactionsForLoan(int SkipCount, int PageSize, ref int RecordCount, string accountNumber)
        {
            var objTransactions = new List<CH_LOAN_TransactionsDTO>();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = string.Empty;
                    RecordCount = GetTransactionsCountForLoan(accountNumber);
                    sql = "select Txn_FORLoan.* from (SELECT ROWNUM as RowNumber,BOBVW_TXN_FOR_LOAN.* from BOBVW_TXN_FOR_LOAN where cr_account_nbr=:cr_account_nbr) Txn_FORLoan where (RowNumber > :ROWNUM1 and RowNumber <= :ROWNUM2)";

                    DataSet ds = new DataSet();
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleParameter p2 = new OracleParameter("ROWNUM1", OracleDbType.Int32);
                    p2.Value = SkipCount;
                    cmd.Parameters.Add(p2);
                    OracleParameter p3 = new OracleParameter("ROWNUM2", OracleDbType.Int32);
                    p3.Value = SkipCount + PageSize;
                    cmd.Parameters.Add(p3);

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                        return null;
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            var obj = new CH_LOAN_TransactionsDTO();
                            obj.CR_ACCOUNT_NBR = dr["CR_ACCOUNT_NBR"] == null ? "" : Convert.ToString(dr["CR_ACCOUNT_NBR"]);
                            obj.MICROFILM_REF_NUMBER = dr[col_MICROFILM_REF_NUMBER] == null ? "" : Convert.ToString(dr[col_MICROFILM_REF_NUMBER]);
                            if (dr["CREDIT_LIMIT"] != null && Convert.ToString(dr["CREDIT_LIMIT"]) != "")
                                obj.Credit_Limit = Convert.ToDouble(dr["CREDIT_LIMIT"]);
                            else
                                obj.Credit_Limit = 0;

                            if (dr["AVAILABLE_CREDIT_LIMIT"] != null && Convert.ToString(dr["AVAILABLE_CREDIT_LIMIT"]) != "")
                                obj.Available_Credit_Limit = Convert.ToDouble(dr["AVAILABLE_CREDIT_LIMIT"]);
                            else
                                obj.Available_Credit_Limit = 0;

                            if (dr["PREAPPROVED_LIMIT"] != null && Convert.ToString(dr["PREAPPROVED_LIMIT"]) != "")
                                obj.PreApproved_Limit = Convert.ToDouble(dr["PREAPPROVED_LIMIT"]);
                            else
                                obj.PreApproved_Limit = 0;

                            objTransactions.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " TransactionsFORLoan :" + ex.Message.ToString());
                return null;
            }
            return objTransactions;
        }
        /// <summary>
        /// Gets the Loan transactions count.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        private static int GetTransactionsCountForLoan(string accountNumber)
        {
            int totalCount = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
                {
                    connection.Open();
                    string sql = "SELECT count(*) from BOBVW_TXN_FOR_LOAN where CR_ACCOUNT_NBR=:CR_ACCOUNT_NBR";
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    OracleParameter p1 = new OracleParameter("CR_ACCOUNT_NBR", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " TransactionsForLoanCount :" + ex.Message.ToString());
                return 0;
            }
            return totalCount;
        }

        #endregion

        #endregion
    }
}
