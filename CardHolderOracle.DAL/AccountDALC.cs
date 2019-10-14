using System;
using System.Data;
using CardHolder.DTO;
using Oracle.DataAccess.Client;


namespace CardHolderOracle.DAL
{
    public static class AccountDALC
    {
        #region Variables

        private static string BILLED_OPENING_BAL = "BILLED_OPENING_BAL";
        private static string STAT_DATE = "STAT_DATE";
        private static string TOTAL_CREDITS = "TOTAL_CREDITS";
        private static string TOTAL_DEBITS = "TOTAL_DEBITS";
        private static string col_Total_Amount_Due = "TOTAL_AMOUNT_DUE";
        //private static string col_Total_Outstanding = "";
        private static string col_Billed_Min_Due = "BILLED_MIN_DUE";
        private static string col_Payment_Due_Date = "PAYMENT_DUE_DATE";
        private static string col_Amount_Received = "LAST_PAYMENT_AMOUNT";
        private static string col_Payment_Received_Date = "LAST_PAYMENT_DATE";
        private static string col_Pts_Closing = "PTS_CLOSING";
        private static string col_Pts_Opening = "PTS_OPENING";
        private static string col_Earned_For_The_Month = "PTS_EARNED";
        private static string col_Redeemed_For_The_Month = "PTS_REDEEMED";
        private static string errorGenerated = "Error generated : ";
        private static string col_Account_Total_Account_Limit = "TOTAL_CREDIT_LIMIT";
        private static string col_Account_Avl_Account_Limit = "AVAILABLE_CREDIT_LIMIT";
        private static string col_TOTAL_ACCOUNT_CASH_LIMIT = "TOTAL_ACCOUNT_CASH_LIMIT";
        private static string col_Account_Avl_Account_Cash_Limit = "AVL_ACCOUNT_CASH_LIMIT";
        #endregion

        #region Selection Methods
        /// <summary>
        /// Gets the account summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        public static CH_CardDTO GetAccountSummary(string accountNumber)
        {
            CH_CardDTO objAccountSummary = new CH_CardDTO();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = "select * from BOBVW_ACCOUNTSUMMARY where cr_account_nbr=:cr_account_nbr";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr == null)
                        return null;
                    while (dr.Read())
                    {
                        objAccountSummary.Cr_Account_Nbr = Convert.ToString(dr[common.col_Cr_Account_Nbr]);
                        objAccountSummary.Account_Total_Outstanding = Convert.ToDouble(dr["TOTAL_OUTSTANDING"]);
                        // objAccountSummary.Account_UnBilled_Outstanding = Convert.ToDouble(dr["UNBILLED_OUTSTANDING"]);//Require data field for this.
                        objAccountSummary.Account_Total_Account_Limit = dr[col_Account_Total_Account_Limit] == null ? 0 : Convert.ToDouble(dr[col_Account_Total_Account_Limit]);
                        objAccountSummary.Account_Avl_Account_Limit = dr[col_Account_Avl_Account_Limit] == null ? 0 : Convert.ToDouble(dr[col_Account_Avl_Account_Limit]);
                        objAccountSummary.Account_Total_Account_Cash_Limit = dr[col_TOTAL_ACCOUNT_CASH_LIMIT] == null ? 0 : Convert.ToDouble(dr[col_TOTAL_ACCOUNT_CASH_LIMIT]);
                        objAccountSummary.Account_Avl_Account_Cash_Limit = dr[col_Account_Avl_Account_Cash_Limit] == null ? 0 : Convert.ToDouble(dr[col_Account_Avl_Account_Cash_Limit]);
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " AccountSummary :" + ex.Message.ToString());
                return null;
            }
            return objAccountSummary;
        }
        /// <summary>
        /// Gets the last bill summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        public static CH_CR_TERMDTO GetSummary(string accountNumber)
        {
            CH_CR_TERMDTO objTermDTO = new CH_CR_TERMDTO();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    //string sql = "SELECT * from BOBVW_BILLSUMMARY where STAT_DATE =(select max(C.STAT_DATE) from cr_term C where C.cr_account_nbr=:cr_account_nbr) and cr_account_nbr=:cr_account_nbr";
                    string sql = "SELECT * from BOBVW_BILLSUMMARY where cr_account_nbr=:cr_account_nbr";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr == null)
                        return null;
                    while (dr.Read())
                    {

                        if (dr[BILLED_OPENING_BAL] != null & Convert.ToString(dr[BILLED_OPENING_BAL]) != "")
                            objTermDTO.BILLED_OPENING_BAL = Convert.ToDouble(dr[BILLED_OPENING_BAL]);
                        else
                            objTermDTO.BILLED_OPENING_BAL = 0;
                        if (dr[col_Total_Amount_Due] != null & Convert.ToString(dr[col_Total_Amount_Due]) != "")
                            objTermDTO.Total_Amount_Due = Convert.ToDouble(dr[col_Total_Amount_Due]);
                        else
                            objTermDTO.Total_Amount_Due = 0;

                        if (dr[TOTAL_CREDITS] != null & Convert.ToString(dr[TOTAL_CREDITS]) != "")
                            objTermDTO.TOTAL_CREDITS = Convert.ToDouble(dr[TOTAL_CREDITS]);
                        else
                            objTermDTO.BILLED_OPENING_BAL = 0;

                        if (dr[TOTAL_DEBITS] != null & Convert.ToString(dr[TOTAL_DEBITS]) != "")
                            objTermDTO.TOTAL_DEBITS = Convert.ToDouble(dr[TOTAL_DEBITS]);
                        else
                            objTermDTO.BILLED_OPENING_BAL = 0;
                       
                        
                      //  objTermDTO.Total_Outstanding = dr[col_Total_Outstanding] == null ? 0 : Convert.ToDouble(dr[col_Total_Outstanding]);
                        objTermDTO.Minimum_Amount_Due = dr[col_Billed_Min_Due] == null ? 0 : Convert.ToDouble(dr[col_Billed_Min_Due]);
                        objTermDTO.Payment_Due_Date = Convert.ToDateTime(dr[col_Payment_Due_Date]);
                        objTermDTO.Stat_Date = Convert.ToDateTime(dr[STAT_DATE]);
                        if (dr[col_Amount_Received] != null & Convert.ToString(dr[col_Amount_Received]) != "")
                            objTermDTO.Amount_Received = Convert.ToDouble(dr[col_Amount_Received]);
                        else
                            objTermDTO.Amount_Received = 0;
                        if (objTermDTO.Paymnet_Received_Date != null && Convert.ToString(objTermDTO.Paymnet_Received_Date) != "" && Convert.ToString(objTermDTO.Paymnet_Received_Date) != "01-Jan-01 12:00:00 AM")
                            objTermDTO.Paymnet_Received_Date = Convert.ToDateTime(dr[col_Payment_Received_Date]);
                        else
                            objTermDTO.Paymnet_Received_Date = DateTime.MinValue;
                        //objTermDTO.Closing_Balance = dr[col_Closing_Balance] == null ? 0 : Convert.ToDouble(dr[col_Closing_Balance]);
                        //objTermDTO.Opening_Balance = dr[col_Opening_Balance] == null ? 0 : Convert.ToDouble(dr[col_Opening_Balance]);
                        //objTermDTO.Earned_For_The_Month = dr[col_Earned_For_The_Month] == null ? 0 : Convert.ToDouble(dr[col_Earned_For_The_Month]);
                        //objTermDTO.Redeemed_For_The_Month = dr[col_Redeemed_For_The_Month] == null ? 0 : Convert.ToDouble(dr[col_Redeemed_For_The_Month]);
                        //objTermDTO.Points_Expiring = dr[col_Redeemed_For_The_Month] == null ? 0 : Convert.ToDouble(dr[col_Redeemed_For_The_Month]); //Query 
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                common.logger.Debug(errorGenerated + " Summary : " + ex.Message.ToString());
                return null;
            }
            return objTermDTO;
        }


        /// <summary>
        /// Gets the last bill summary.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        public static CH_CR_TERMDTO GetRewardPointsSummary(string accountNumber)
        {
            CH_CR_TERMDTO objTermDTO = new CH_CR_TERMDTO();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = "SELECT * from BOBVW_REWARDPOINT_SUMMARY where cr_account_nbr=:cr_account_nbr";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr == null)
                        return null;
                    while (dr.Read())
                    {
                        objTermDTO.PTS_CLOSING = dr[col_Pts_Closing] == null ? 0 : Convert.ToDouble(dr[col_Pts_Closing]);
                        objTermDTO.PTS_OPENING = dr[col_Pts_Opening] == null ? 0 : Convert.ToDouble(dr[col_Pts_Opening]);
                        objTermDTO.Earned_For_The_Month = dr[col_Earned_For_The_Month] == null ? 0 : Convert.ToDouble(dr[col_Earned_For_The_Month]);
                        objTermDTO.Redeemed_For_The_Month = dr[col_Redeemed_For_The_Month] == null ? 0 : Convert.ToDouble(dr[col_Redeemed_For_The_Month]);
                        objTermDTO.Points_Expiring = dr[col_Redeemed_For_The_Month] == null ? 0 : Convert.ToDouble(dr[col_Redeemed_For_The_Month]); //Query 
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                common.logger.Debug(errorGenerated + " Summary : " + ex.Message.ToString());
                return null;
            }
            return objTermDTO;
        }

        public static string GetBonusPoints(string accountNumber)
        {
            string BonusPoints = string.Empty;
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT bal_pts FROM  BOBVW_BONUSPTSREDEEM WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", accountNumber.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return BonusPoints;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    BonusPoints = row["bal_pts"] == null ? "" : Convert.ToString(row["bal_pts"]);
                                    return BonusPoints;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(ex.Message.ToString());
                return null;
            }
        }



        /// <summary>
        /// Gets the last bill summary date.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>

        public static DateTime GetDateToDisplay(string accountNumber)
        {
            //DateTime Lastbillingdate = DateTime.MinValue;
            DateTime Lastbillingdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Lastbillingdate = Lastbillingdate.AddDays(-1);
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT max(stat_date) stat_date FROM  BOBVW_CR_TERM WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", accountNumber);
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return Lastbillingdate;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    if (row["Stat_Date"] != null && Convert.ToString(row["Stat_Date"]) != "")
                                    {
                                        Lastbillingdate = row["Stat_Date"] == null ? Lastbillingdate : Convert.ToDateTime(row["Stat_Date"]);
                                    }
                                    return Lastbillingdate;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                common.logger.Debug(errorGenerated + " Summary : " + ex.Message.ToString());
                return Lastbillingdate;
            }
        }


        /// <summary>
        /// Gets the Card Balance summary date.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>

        public static DateTime GetDateForCardSummary(string accountNumber)
        {
            //DateTime Lastbillingdate = DateTime.MinValue;
            DateTime CardBalSummaryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            CardBalSummaryDate = CardBalSummaryDate.AddDays(-1);
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        //string sql = @"SELECT to_char(max(DATE_MODIF),'dd-Mon-yy HH:mm:ss') AS DATE_MODIF FROM BOBVW_ACCOUNT";
                        string sql = @"SELECT max(DATE_MODIF) AS DATE_MODIF FROM BOBVW_ACCOUNT";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            //cmd.Parameters.Add(":CR_ACCOUNT_NBR", accountNumber);
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return CardBalSummaryDate;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    if (row["DATE_MODIF"] != null && Convert.ToString(row["DATE_MODIF"]) != "")
                                    {
                                        CardBalSummaryDate = row["DATE_MODIF"] == null ? CardBalSummaryDate : Convert.ToDateTime(row["DATE_MODIF"]);
                                    }
                                    return CardBalSummaryDate;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                common.logger.Debug(errorGenerated + " Summary : " + ex.Message.ToString());
                return CardBalSummaryDate;
            }
        }


        #endregion
    }
}
