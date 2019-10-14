using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CardHolder.DTO;
using CardHolder.DTO.Oracle;
using Oracle.DataAccess.Client;

namespace CardHolderOracle.DAL
{
    public static class CardDALC
    {
        #region Variables

        static string col_Total_Outstanding = "TOTAL_OUTSTANDING";
        static string col_Total_Card_Limit = "TOTAL_CREDIT_LIMIT";
        static string col_Avl_Card_Limit = "AVAILABLE_CREDIT_LIMIT";
        static string col_Total_Card_Cash_Limit = "TOTAL_CARD_CASH_LIMIT";
        static string col_Avl_Card_Cash_Limit = "AVL_CARD_CASH_LIMIT";
        // static string col_Card_UnBilled_Outstanding = "Card_UnBilled_Outstanding";
        // static string col_Card_UnBilled_Outstanding = "UNBILLED_OUTSTANDING";
        static string errorGenerated = "Error generated : ";

        #endregion

        #region Selection Methods
        /// <summary>
        /// Gets the card list.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        //public static List<string> GetCardList(string accountNumber)
        //{
        //    string Card_number = "CARD_NUMBER";
        //    List<string> cards = new List<string>();
        //    try
        //    {
        //        using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
        //        {
        //            conn.Open();
        //            //string sql = "SELECT * FROM BOBVW_CARDS WHERE cr_account_nbr=:cr_account_nbr";
        //            string sql = "SELECT * FROM BOBVW_CARDS_LIST WHERE cr_account_nbr=:cr_account_nbr";
        //            OracleCommand cmd = new OracleCommand(sql, conn);
        //            OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
        //            p1.Value = accountNumber;
        //            cmd.Parameters.Add(p1);
        //            OracleDataReader dr = cmd.ExecuteReader();
        //            if (dr == null)
        //                return null;
        //            while (dr.Read())
        //            {
        //                // cards.Add(Convert.ToString(dr[common.col_Cr_Account_Nbr]));
        //                cards.Add(Convert.ToString(dr[Card_number]));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        common.logger.Debug(errorGenerated + " CardList " + ex.Message.ToString());
        //        return null;
        //    }
        //    return cards;
        //}

        public static List<CH_CardDTO> GetCardList(string accountNumber)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_CARDS_LIST WHERE CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", accountNumber.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new CH_CardDTO()
                                    {
                                        card_number = row.Field<string>("CARD_NUMBER"),
                                        FULL_NAME = row.Field<string>("EMBOSSED_NAME"),
                                        MASK_CARD_NUMBER = row.Field<string>("MASK_CARD_NUMBER")
                                        //BASIC_CARD_FLAG = row.Field<string>("BASIC_CARD_FLAG")
                                    }).ToList();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " CardList " + ex.Message.ToString());
                return null;
            }
        }


        /// <summary>
        /// Gets the card summary.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        public static CH_CardDTO GetCardSummary(string cardNumber)
        {
            CH_CardDTO objCardSummary = new CH_CardDTO();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    string sql = "SELECT * from BOBVW_CARDSUMMARY where card_number=:card_number";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleParameter p1 = new OracleParameter("card_number", OracleDbType.Varchar2, 30);
                    p1.Value = cardNumber;
                    cmd.Parameters.Add(p1);
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr == null)
                        return null;
                    while (dr.Read())
                    {
                        objCardSummary.Cr_Account_Nbr = Convert.ToString(dr[common.col_Cr_Account_Nbr]);
                        objCardSummary.Embossed_Name = Convert.ToString(dr[common.col_Embossed_Name]);
                        objCardSummary.Card_Total_Outstanding = dr[col_Total_Outstanding] == null ? 0 : Convert.ToDouble(dr[col_Total_Outstanding]);
                        // objCardSummary.Card_UnBilled_Outstanding = Convert.ToDouble(dr[col_Card_UnBilled_Outstanding]);  // Query 
                        objCardSummary.Card_Total_Account_Limit = dr[col_Total_Card_Limit] == null ? 0 : Convert.ToDouble(dr[col_Total_Card_Limit]);
                        objCardSummary.Card_Avl_Account_Limit = dr[col_Avl_Card_Limit] == null ? 0 : Convert.ToDouble(dr[col_Avl_Card_Limit]);
                        objCardSummary.Card_Total_Account_Cash_Limit = dr[col_Total_Card_Cash_Limit] == null ? 0 : Convert.ToDouble(dr[col_Total_Card_Cash_Limit]);
                        objCardSummary.Card_Avl_Account_Cash_Limit = dr[col_Avl_Card_Cash_Limit] == null ? 0 : Convert.ToDouble(dr[col_Avl_Card_Cash_Limit]);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                common.logger.Debug(errorGenerated + " CardSummary :" + ex.Message.ToString());
                return null;
            }
            return objCardSummary;
        }
        /// <summary>
        /// Gets the card statement.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        public static List<CH_CR_TERMDTO> GetCardStatement(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, int monthRange)
        {
            List<CH_CR_TERMDTO> objCardstatements = new List<CH_CR_TERMDTO>();
            try
            {
                using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                {
                    conn.Open();
                    RecordCount = GetCardStatementCount(accountNumber, monthRange);
                    DataSet ds = new DataSet();
                    //change 15 to 6 to get last six months statements-----------------------------------------
                    string sql = "select CardStatements.* from (SELECT ROWNUM as RowNumber ,BOBVW_CARDSTATEMENTS.* from BOBVW_CARDSTATEMENTS where cr_account_nbr=:cr_account_nbr) CardStatements where (RowNumber > :ROWNUM1  and RowNumber <= :ROWNUM2) and to_date(statement_date,'MM/DD/YYYY') >= add_months(SYSDATE, -:monthrange)";
                    //string sql = "select CardStatements.* from (SELECT ROWNUM as RowNumber ,BOBVW_CARDSTATEMENTS.* from BOBVW_CARDSTATEMENTS where cr_account_nbr=:cr_account_nbr) CardStatements where to_date(statement_date,'dd/mm/yyyy') BETWEEN add_months(sysdate,-:monthrange) AND SYSDATE";
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

                    OracleParameter p4 = new OracleParameter("monthrange", OracleDbType.Int32);
                    p4.Value = monthRange;
                    cmd.Parameters.Add(p4);

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                        return null;
                    else if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CH_CR_TERMDTO obj = new CH_CR_TERMDTO();
                            obj.Row_Num = Convert.ToInt32(dr["RowNumber"]);
                            if (dr["STATEMENT_DATE"] != null && Convert.ToString(dr["STATEMENT_DATE"]) != "")
                                obj.Stat_Date = Convert.ToDateTime(dr["STATEMENT_DATE"]);
                            else
                                obj.Stat_Date = DateTime.MinValue;
                            obj.BILLED_OPENING_BAL = dr["OPENING_BAL"] == null ? 0 : Convert.ToDouble(dr["OPENING_BAL"]);
                            obj.TOTAL_CREDITS = dr["TOTAL_CREDITS"] == null ? 0 : Convert.ToDouble(dr["TOTAL_CREDITS"]);
                            obj.TOTAL_DEBITS = dr["TOTAL_DEBITS"] == null ? 0 : Convert.ToDouble(dr["TOTAL_DEBITS"]);
                            obj.STATEMENT_MONTH = dr["STATEMENT_MONTH"] == null ? "" : Convert.ToString(dr["STATEMENT_MONTH"]);
                            // obj.Payment_Charges = dr["PAYMENT_CHARGES"] == null ? 0 : Convert.ToDouble(dr["PAYMENT_CHARGES"]);
                            obj.Billed_Closing_Bal = dr["CLOSING_BAL"] == null ? 0 : Convert.ToDouble(dr["CLOSING_BAL"]);
                            obj.MINIMUM_PAYMENT_DUE = dr["MINIMUM_PAYMENT_DUE"] == null ? 0 : Convert.ToDouble(dr["MINIMUM_PAYMENT_DUE"]);
                            obj.Pdf_File = dr["Pdf_File"] == null ? "" : Convert.ToString(dr["Pdf_File"]);
                            objCardstatements.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " CardStatement :" + ex.Message.ToString());
                return null;
            }
            return objCardstatements;
        }
        /// <summary>
        /// Gets the card statement count.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        private static int GetCardStatementCount(string accountNumber, int monthRange)
        {
            int totalCount = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
                {
                    connection.Open();
                    string sql = "SELECT count(*) from BOBVW_CARDSTATEMENTS where cr_account_nbr=:cr_account_nbr and (to_date(statement_date,'dd/mm/yyyy') BETWEEN add_months(sysdate,-:monthrange) AND SYSDATE)";
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    OracleParameter p1 = new OracleParameter("cr_account_nbr", OracleDbType.Varchar2, 30);
                    p1.Value = accountNumber;
                    cmd.Parameters.Add(p1);
                    OracleParameter p2 = new OracleParameter("monthrange", OracleDbType.Int32);
                    p2.Value = monthRange;
                    cmd.Parameters.Add(p2);


                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(errorGenerated + " CardStatementCount :" + ex.Message.ToString());
                return 0;
            }
            return totalCount;
        }

        public static bool GetPDFnames(string accountNumber, string PDFFile = "")
        {
            bool HavePDF = false;
            // string PDF = "";
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT PDF_FILE FROM  BOBVW_CARDSTATEMENTS WHERE CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR and PDF_FILE = :PDF_FILE";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", accountNumber.Trim());
                            cmd.Parameters.Add(":PDF_FILE", PDFFile.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return HavePDF;
                                else
                                {
                                    //DataRow row = ds.Tables[0].Rows[0];
                                    //PDF = row["PDF_FILE"] == null ? "" : Convert.ToString(row["PDF_FILE"]);
                                    HavePDF = true;
                                    return HavePDF;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(ex.Message.ToString());
                return HavePDF;
            }
        }

        ///// <summary>
        ///// Gets the name of the card statement PDF file.
        ///// </summary>
        ///// <param name="cardNumber">The card number.</param>
        ///// <param name="clientCode">The client code.</param>
        ///// <returns></returns>
        //public static CH_EVG_EVENTS_QUEUEDTO GetCardStatementPDFFileName(string cardNumber, DateTime dt)
        //{
        //    CH_EVG_EVENTS_QUEUEDTO objEVGEVENTSQUEUE = new CH_EVG_EVENTS_QUEUEDTO();
        //    try
        //    {
        //        using (OracleConnection connection = new OracleConnection(common.GetConnectionstring()))
        //        {
        //            connection.Open();
        //            string sql = "select EVE_OUT_FILENAME from EVG_EVENTS_QUEUE where EVE_CARD_NUMBER=:EVE_CARD_NUMBER and to_char(to_date(EVE_PROCESSING_DATE, 'DD-MM-YYYY'), 'Month') =to_char(to_date('" + dt.Date.ToString("dd/MM/yyyy") + "', 'DD-MM-YYYY'), 'Month')";
        //            OracleCommand cmd = new OracleCommand(sql, connection);
        //            OracleParameter p1 = new OracleParameter("EVE_CARD_NUMBER", OracleDbType.Varchar2, 30);
        //            p1.Value = cardNumber;
        //            cmd.Parameters.Add(p1);
        //            OracleDataReader dr = cmd.ExecuteReader();
        //            if (dr == null)
        //                return null;
        //            while (dr.Read())
        //            {
        //                objEVGEVENTSQUEUE.EVE_OUT_FILENAME = Convert.ToString(dr["EVE_OUT_FILENAME"]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        common.logger.Debug(errorGenerated + " CardStatementPDFFileName :" + ex.Message.ToString());
        //        return null;
        //    }
        //    return objEVGEVENTSQUEUE;
        //}

        #endregion




        #region Get Card Details

        /// <summary>
        ///  This method is used for getting card details for International Limit
        /// </summary>
        /// <param name="cDto"></param>
        /// <returns>CH_CardDTO</returns>
        public static CH_CardDTO GetCardDetailsForInternationalLimitByCardNumber(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_CREDITCARDDETAIL WHERE CARD_NUMBER= :CARD_NUMBER";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO card = new CH_CardDTO()
                                    {
                                        LIMIT_PURCH_INT = row["LIMIT_PURCH_INT"] == null ? 0 : Convert.ToInt32(row["LIMIT_PURCH_INT"]),
                                        LIMIT_PURCH_INT_resp = row["LIMIT_PURCH_INT_resp"] == null ? 0 : Convert.ToInt32(row["LIMIT_PURCH_INT_resp"]),
                                        Credit_Limit = row["CREDIT_LIMIT"] == null ? 0 : Convert.ToInt32(row["CREDIT_LIMIT"])
                                    };
                                    return card;
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
        ///  This method is used for getting card details for Auto Debit Payment type 
        /// </summary>
        /// <param name="cDto"></param>
        /// <returns>CH_CardDTO</returns>
        public static CH_CardDTO GetAutoDebitPaymentType(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_AutoDebitPaymentType WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO AutoDebitDtl = new CH_CardDTO()
                                    {
                                        AUTO_DEBIT_STATUS = row["AUTO_DEBIT_STATUS"] == null ? "" : Convert.ToString(row["AUTO_DEBIT_STATUS"]),
                                        AUTO_DEBIT_TYPE = row["AUTO_DEBIT_TYPE"] == null ? "" : Convert.ToString(row["AUTO_DEBIT_TYPE"]),
                                        DIRECT_DEBIT_PERCENTAGE = row["DIRECT_DEBIT_PERCENTAGE"] == null ? 0 : Convert.ToInt32(row["DIRECT_DEBIT_PERCENTAGE"]),
                                       
                                    };
                                    return AutoDebitDtl;
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
        /// Find Card Detail From CARD As per discussion with PM
        /// </summary>
        /// <param name="cDto">cDto.card_number</param>
        /// <returns></returns>
        //        public static CH_CardDTO GetCardByCreditCardNumber(CH_CardDTO cDto)
        //        {
        //            try
        //            {
        //                using (DataSet ds = new DataSet())
        //                {
        //                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
        //                    {

        //                        string sql = @"SELECT * FROM  BOBVW_CARDLIST WHERE
        //                                        cr_account_nbr in (SELECT cr_account_nbr FROM BOBVW_CARDLIST WHERE CARD_NUMBER = :CARD_NUMBER) AND BASIC_CARD_FLAG = 0";
        //                        using (OracleCommand cmd = new OracleCommand(sql, conn))
        //                        {
        //                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
        //                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
        //                            {
        //                                dA.Fill(ds);

        //                                if (ds.Tables[0].Rows.Count == 0)
        //                                {
        //                                    return null;
        //                                }
        //                                else
        //                                {
        //                                    DataRow row = ds.Tables[0].Rows[0];
        //                                    CH_CardDTO card = new CH_CardDTO()
        //                                    {
        //                                        card_number = row["CARD_NUMBER"] == null ? "" : row["CARD_NUMBER"].ToString(),
        //                                        CARD_SEQ = row["CARD_SEQ"] == null ? (int?)null : Convert.ToInt32(row["CARD_SEQ"]),
        //                                        Cr_Account_Nbr = row["Cr_Account_Nbr"] == null ? "" : row["Cr_Account_Nbr"].ToString(),
        //                                        TITLE = row["TITLE"] == null ? "" : row["TITLE"].ToString(),
        //                                        Embossed_Name = row["EMBOSSED_NAME"] == null ? "" : row["EMBOSSED_NAME"].ToString(),
        //                                        FAMILY_NAME = row["FAMILY_NAME"] == null ? "" : row["FAMILY_NAME"].ToString(),
        //                                        FIRST_NAME = row["FIRST_NAME"] == null ? "" : row["FIRST_NAME"].ToString(),
        //                                        CLIENT_CODE = row["CLIENT_CODE"] == null ? "" : row["CLIENT_CODE"].ToString(),
        //                                        BRANCH_CODE = row["BRANCH_CODE"] == null ? "" : row["BRANCH_CODE"].ToString(),
        //                                        BASIC_CARD_FLAG = row["BASIC_CARD_FLAG"] == null ? "" : row["BASIC_CARD_FLAG"].ToString(),
        //                                        BIRTH_DATE = row["BIRTH_DATE"] == null ? (DateTime?)null : DateTime.Parse(row["BIRTH_DATE"].ToString()),
        //                                        ADDRESS1 = row["ADDRESS1"] == null ? "" : row["ADDRESS1"].ToString(),
        //                                        ADDRESS2 = row["ADDRESS2"] == null ? "" : row["ADDRESS2"].ToString(),
        //                                        ADDRESS3 = row["ADDRESS3"] == null ? "" : row["ADDRESS3"].ToString(),
        //                                        ADDRESS4 = row["ADDRESS4"] == null ? "" : row["ADDRESS4"].ToString(),
        //                                        CITY_CODE = row["CITY_CODE"] == null ? "" : row["CITY_CODE"].ToString(),
        //                                        ZIP_CODE = row["ZIP_CODE"] == null ? "" : row["ZIP_CODE"].ToString(),
        //                                        EMAIL_ID = row["EMAIL_ID"] == null ? "" : row["EMAIL_ID"].ToString(),
        //                                        PHONE_MOBILE = row["PHONE_MOBILE"] == null ? "" : row["PHONE_MOBILE"].ToString(),
        //                                        BILLDESK_ONLINE_ID = row["BILLDESK_ONLINE_ID"] == null ? "" : row["BILLDESK_ONLINE_ID"].ToString(),
        //                                    };
        //                                    return card;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                common.logger.Debug(ex.Message.ToString());
        //                return null;
        //            }
        //        }

        #region for Card Details
        public static CH_CardDTO GetCardByCreditCardNumber(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_PRIMARYCARDDTL WHERE
                                        DATE_CREATE in (SELECT MAX(DATE_CREATE) DATE_CREATE FROM BOBVW_CARDS WHERE STATUS_CODE NOT IN (4,9) 
                                        AND BASIC_CARD_FLAG='0' AND CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR) AND CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO card = new CH_CardDTO()
                                    {
                                        card_number = row["CARD_NUMBER"] == null ? "" : row["CARD_NUMBER"].ToString(),
                                        //  CARD_SEQ = row["CARD_SEQ"] == null ? (int?)null : Convert.ToInt32(row["CARD_SEQ"]),
                                        Cr_Account_Nbr = row["CR_ACCOUNT_NBR"] == null ? "" : row["CR_ACCOUNT_NBR"].ToString(),
                                        // TITLE = row["TITLE"] == null ? "" : row["TITLE"].ToString(),
                                        Embossed_Name = row["EMBOSSED_NAME"] == null ? "" : row["EMBOSSED_NAME"].ToString(),
                                        FULL_NAME = row["FULL_NAME"] == null ? "" : row["FULL_NAME"].ToString(),
                                        CLIENT_CODE = row["CLIENT_CODE"] == null ? "" : row["CLIENT_CODE"].ToString(),
                                        BRANCH_CODE = row["BRANCH_CODE"] == null ? "" : row["BRANCH_CODE"].ToString(),
                                        BASIC_CARD_FLAG = row["BASIC_CARD_FLAG"] == null ? "" : row["BASIC_CARD_FLAG"].ToString(),
                                        BIRTH_DATE = row["BIRTH_DATE"] == null ? (DateTime?)null : DateTime.Parse(row["BIRTH_DATE"].ToString()),
                                        MAILING_ADDRESS1 = row["MAILING_ADDRESS1"] == null ? "" : row["MAILING_ADDRESS1"].ToString(),
                                        MAILING_ADDRESS2 = row["MAILING_ADDRESS2"] == null ? "" : row["MAILING_ADDRESS2"].ToString(),
                                        MAILING_ADDRESS3 = row["MAILING_ADDRESS3"] == null ? "" : row["MAILING_ADDRESS3"].ToString(),
                                        MAILING_ADDRESS4 = row["MAILING_ADDRESS4"] == null ? "" : row["MAILING_ADDRESS4"].ToString(),
                                        CITY_NAME = row["CITY_NAME"] == null ? "" : row["CITY_NAME"].ToString(),
                                        MAILING_ZIP_CODE = row["MAILING_ZIP_CODE"] == null ? "" : row["MAILING_ZIP_CODE"].ToString(),
                                        EMAIL_ID = row["EMAIL_ID"] == null ? "" : row["EMAIL_ID"].ToString(),
                                        PHONE_MOBILE = row["PHONE_MOBILE"] == null ? "" : row["PHONE_MOBILE"].ToString(),
                                        BILLDESK_ONLINE_ID = row["BILLDESK_ONLINE_ID"] == null ? "" : row["BILLDESK_ONLINE_ID"].ToString(),
                                        PREFERRED_MAILING_ADDRESS = row["PREFERRED_MAILING_ADDRESS"] == null ? "" : row["PREFERRED_MAILING_ADDRESS"].ToString(),
                                        BRANCH_REF_NUMBER = row["BRANCH_REF_NUMBER"] == null ? "" : row["BRANCH_REF_NUMBER"].ToString(),
                                    };
                                    return card;
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
        #endregion

        #region forAccountDetails

        public static CH_CardDTO GetAccountDetails(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_ACCOUNT WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    int testvar = 0;
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO AccountDtl = new CH_CardDTO()
                                    {
                                        //   CURRENT_ACC_BRANCH = row["CURRENT_ACC_BRANCH"] == null ? "" : Convert.ToString(row["CURRENT_ACC_BRANCH"]),
                                        // DIRECT_DEBIT_AMOUNT_FLAG = row["DIRECT_DEBIT_AMOUNT_FLAG"] == null ? 0 : Convert.ToInt32(row["DIRECT_DEBIT_AMOUNT_FLAG"]),

                                        DIRECT_DEBIT_AMOUNT_FLAG = int.TryParse(Convert.ToString(row["DIRECT_DEBIT_AMOUNT_FLAG"]), out testvar) ? Convert.ToInt32(row["DIRECT_DEBIT_AMOUNT_FLAG"]) : new Nullable<int>(),
                                        CURRENT_ACC_NBR = row["CURRENT_ACC_NBR"] == null ? "" : Convert.ToString(row["CURRENT_ACC_NBR"]),

                                    };
                                    return AccountDtl;
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


        #endregion

        #region for ATMPIN

        public static List<CH_CardDTO> GetAllCardsForATMPinReg(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_DDLCARDS WHERE CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new CH_CardDTO()
                                    {
                                        card_number = row.Field<string>("CARD_NUMBER"),
                                        FULL_NAME = row.Field<string>("FULL_NAME"),
                                        MASK_CARD_NUMBER = row.Field<string>("MASK_CARD_NUMBER"),
                                        BASIC_CARD_FLAG = row.Field<string>("BASIC_CARD_FLAG")
                                    }).ToList();
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

        public static CH_CardDTO GetATMPinDetails(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_ATMPIN_DTL WHERE CARD_NUMBER = :CARD_NUMBER";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO ATMPinDetails = new CH_CardDTO()
                                    {
                                        card_number = row["CARD_NUMBER"] == null ? "" : Convert.ToString(row["CARD_NUMBER"]),
                                        PIN_REGENERATION_DATE = row["PIN_REGENERATION_DATE"] == null ? DateTime.MinValue : Convert.ToDateTime(row["PIN_REGENERATION_DATE"]),
                                        ATMP_PIN_REQUEST_NUMBER = row["REQUEST_NUMBER"] == null ? "" : Convert.ToString(row["REQUEST_NUMBER"]),

                                    };
                                    return ATMPinDetails;
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

        #endregion

        #region for Replacement_Renewal

        public static List<CH_CardDTO> GetAllCardsForReplaceRenew(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_DDL_REP_RENEW_CARDS WHERE CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new CH_CardDTO()
                                    {
                                        card_number = row.Field<string>("CARD_NUMBER"),
                                        FULL_NAME = row.Field<string>("FULL_NAME"),
                                        MASK_CARD_NUMBER = row.Field<string>("MASK_CARD_NUMBER")
                                    }).ToList();
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

        public static string CardAllowedForRenewal(string CR_ACCOUNT_NBR)
        {
            string RenewalCard = "";
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT CARD_NUMBER FROM  BOBVW_RENEWAL_REQ WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", CR_ACCOUNT_NBR.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return RenewalCard;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    RenewalCard = row["CARD_NUMBER"] == null ? "" : Convert.ToString(row["CARD_NUMBER"]);
                                    return RenewalCard;
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

        public static string CardAllowedForReplacement(string CR_ACCOUNT_NBR)
        {
            string ReplaceCard = "";
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT CARD_NUMBER FROM  BOBVW_REPLACEMENT_REQ WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", CR_ACCOUNT_NBR.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return ReplaceCard;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    ReplaceCard = row["CARD_NUMBER"] == null ? "" : Convert.ToString(row["CARD_NUMBER"]);
                                    return ReplaceCard;
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

        #endregion

        #region for PreserveStmnt

        public static string CardEligibleForPreserveStmnt(string CR_ACCOUNT_NBR)
        {
            string EligibleAccount = "";
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT CR_ACCOUNT_NBR FROM  BOBVW_ELIGIBLE_ACC_PRSRV_STMNT WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", CR_ACCOUNT_NBR.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return EligibleAccount;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    EligibleAccount = row["CR_ACCOUNT_NBR"] == null ? "" : Convert.ToString(row["CR_ACCOUNT_NBR"]);
                                    return EligibleAccount;
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

        public static CH_CardDTO GetPreserveStmntDetails(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_PRESERVESTMNT_DTL WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO PreserveStmntDetails = new CH_CardDTO()
                                    {
                                        Cr_Account_Nbr = row["CR_ACCOUNT_NBR"] == null ? "" : Convert.ToString(row["CR_ACCOUNT_NBR"]),
                                        PRESERVE_STMNT_GENERATION_DATE = row["PIN_REGENERATION_DATE"] == null ? DateTime.MinValue : Convert.ToDateTime(row["PIN_REGENERATION_DATE"]),
                                        PRESERVE_STMNT_REQUEST_NUMBER = row["REQUEST_NUMBER"] == null ? "" : Convert.ToString(row["REQUEST_NUMBER"]),
                                        FOR_MONTH = row["FOR_MONTH"] == null ? "" : Convert.ToString(row["FOR_MONTH"]),

                                    };
                                    return PreserveStmntDetails;
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


        #endregion

        #region for AddonCardRequest
        public static List<CH_CardDTO> GetAddonCards(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_ADDONCARDDTL WHERE CR_ACCOUNT_NBR= :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new CH_CardDTO()
                                    {
                                        card_number = row.Field<string>("CARD_NUMBER"),
                                        FULL_NAME = row.Field<string>("FULL_NAME"),
                                    }).ToList();
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

        public static string CardEligibleForAddOnReq(string CR_ACCOUNT_NBR)
        {
            string EligibleAccount = "";
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT CR_ACCOUNT_NBR FROM  BOBVW_ACC_STATUS WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", CR_ACCOUNT_NBR.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return EligibleAccount;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    EligibleAccount = row["CR_ACCOUNT_NBR"] == null ? "" : Convert.ToString(row["CR_ACCOUNT_NBR"]);
                                    return EligibleAccount;
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

        public static int CountTotalAddonCards(string CR_ACCOUNT_NBR)
        {
            int TotalAccount = 0;
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT COUNT(1) TOTALCOUNT FROM BOBVW_COUNTADDONS WHERE  CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", CR_ACCOUNT_NBR.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return TotalAccount;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    TotalAccount = row["TOTALCOUNT"] == null ? 0 : Convert.ToInt32(row["TOTALCOUNT"]);
                                    return TotalAccount;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(ex.Message.ToString());
                return 0;
            }
        }

        #endregion

        #region forAutoDebitPaymentTypeDetails

        public static CH_CardDTO GetAutoDebitPaymentDetails(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_LAST_AUTODEBIT_DTL WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CR_ACCOUNT_NBR", cDto.Cr_Account_Nbr.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO AutoDebitDtl = new CH_CardDTO()
                                    {
                                        Embossed_Name = row["EMBOSSED_NAME"] == null ? "" : Convert.ToString(row["EMBOSSED_NAME"]),
                                        AUTO_DEBIT_MODE = row["AUTO_DEBIT_MODE"] == null ? "" : Convert.ToString(row["AUTO_DEBIT_MODE"]),
                                        DIRECT_DEBIT_PERCENTAGE = row["DIRECT_DEBIT_PERCENTAGE"] == null ? 0 : Convert.ToInt32(row["DIRECT_DEBIT_PERCENTAGE"]),
                                        AUTO_DEBIT_BRANCH = row["AUTO_DEBIT_BRANCH"] == null ? "" : Convert.ToString(row["AUTO_DEBIT_BRANCH"]),

                                    };
                                    return AutoDebitDtl;
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


        #endregion

        public static string GetBranchNameByCode(string Branchcode)
        {
            string BRANCH_NAME = "";
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT BRANCH_NAME FROM  BOBVMW_BRANCHLIST WHERE BRANCH_CODE = :Branchcode";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":Branchcode", Branchcode.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return BRANCH_NAME;
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    BRANCH_NAME = row["BRANCH_NAME"] == null ? "" : Convert.ToString(row["BRANCH_NAME"]);
                                    return BRANCH_NAME;
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

        public static CH_CardDTO GetCHNameStatusbyCardNumber(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM  BOBVW_CARDS WHERE CARD_NUMBER= :CARD_NUMBER";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO card = new CH_CardDTO()
                                    {
                                        FULL_NAME = row["FULL_NAME"] == null ? "" : row["FULL_NAME"].ToString(),
                                        FIRST_NAME = row["FIRST_NAME"] == null ? "" : row["FIRST_NAME"].ToString(),
                                        FAMILY_NAME = row["FAMILY_NAME"] == null ? "" : row["FAMILY_NAME"].ToString(),
                                        STATUS_CODE = row["STATUS_CODE"] == null ? 0 : Convert.ToInt32(row["STATUS_CODE"]),
                                        BRANCH_REF_NUMBER = row["BRANCH_REF_NUMBER"] == null ? "" : Convert.ToString(row["BRANCH_REF_NUMBER"])
                                    };
                                    return card;
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


        public static CH_CardDTO AuthenticateCrNumberDOBForRegis(CH_CardDTO cDto)
        {
            string DateOfBirth = Convert.ToDateTime(cDto.BIRTH_DATE).ToString("dd-MMM-yy").ToLower();


            // string DateOfBirth = DateTime.ParseExact(Formatdate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");

            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT FULL_NAME,PHONE_MOBILE,EMAIL_ID,CARD_NUMBER,CR_ACCOUNT_NBR FROM  BOBVW_REGISTRATIONCARDDTL WHERE
                                       CARD_NUMBER in (SELECT CARD_NUMBER FROM BOBVW_CARDDETAIL WHERE CARD_NUMBER = :CARD_NUMBER AND EXPIRY_MONTH = :EXPIRY_MONTH
                                       AND EXPIRY_YEAR = :EXPIRY_YEAR AND to_char(BIRTH_DATE,'dd-mon-yy') = :BIRTH_DATE)";
                        //trunc(BIRTH_DATE) = :BIRTH_DATE)";

                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
                            cmd.Parameters.Add(":EXPIRY_MONTH", cDto.EXPIRY_MONTH);
                            cmd.Parameters.Add(":EXPIRY_YEAR", cDto.EXPIRY_YEAR);
                            cmd.Parameters.Add(":BIRTH_DATE", DateOfBirth);

                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO card = new CH_CardDTO()
                                    {
                                        card_number = row["CARD_NUMBER"] == null ? "" : row["CARD_NUMBER"].ToString(),
                                        Cr_Account_Nbr = row["Cr_Account_Nbr"] == null ? "" : row["Cr_Account_Nbr"].ToString(),
                                        FULL_NAME = row["FULL_NAME"] == null ? "" : row["FULL_NAME"].ToString(),
                                        //FIRST_NAME = row["FIRST_NAME"] == null ? "" : row["FIRST_NAME"].ToString(),
                                        EMAIL_ID = row["EMAIL_ID"] == null ? "" : row["EMAIL_ID"].ToString(),
                                        PHONE_MOBILE = row["PHONE_MOBILE"] == null ? "" : row["PHONE_MOBILE"].ToString(),
                                    };
                                    return card;
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


        public static CH_CardDTO AuthenticateCrNumberDOB(CH_CardDTO cDto)
        {
            string DateOfBirth = Convert.ToDateTime(cDto.BIRTH_DATE).ToString("dd-MMM-yy").ToLower();


            // string DateOfBirth = DateTime.ParseExact(Formatdate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy");

            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT FULL_NAME,PHONE_MOBILE,EMAIL_ID,CARD_NUMBER,CR_ACCOUNT_NBR FROM  BOBVW_FORGOTCARDDTL WHERE
                                       CARD_NUMBER in (SELECT CARD_NUMBER FROM BOBVW_CARDDETAIL WHERE CARD_NUMBER = :CARD_NUMBER AND EXPIRY_MONTH = :EXPIRY_MONTH
                                       AND EXPIRY_YEAR = :EXPIRY_YEAR AND to_char(BIRTH_DATE,'dd-mon-yy') = :BIRTH_DATE)";
                        //trunc(BIRTH_DATE) = :BIRTH_DATE)";

                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
                            cmd.Parameters.Add(":EXPIRY_MONTH", cDto.EXPIRY_MONTH);
                            cmd.Parameters.Add(":EXPIRY_YEAR", cDto.EXPIRY_YEAR);
                            cmd.Parameters.Add(":BIRTH_DATE", DateOfBirth);

                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO card = new CH_CardDTO()
                                    {
                                        card_number = row["CARD_NUMBER"] == null ? "" : row["CARD_NUMBER"].ToString(),
                                        Cr_Account_Nbr = row["Cr_Account_Nbr"] == null ? "" : row["Cr_Account_Nbr"].ToString(),
                                        FULL_NAME = row["FULL_NAME"] == null ? "" : row["FULL_NAME"].ToString(),
                                        //FIRST_NAME = row["FIRST_NAME"] == null ? "" : row["FIRST_NAME"].ToString(),
                                        EMAIL_ID = row["EMAIL_ID"] == null ? "" : row["EMAIL_ID"].ToString(),
                                        PHONE_MOBILE = row["PHONE_MOBILE"] == null ? "" : row["PHONE_MOBILE"].ToString(),
                                    };
                                    return card;
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


        public static bool AuthenticateUserStatus(string AccountNumber)
        {
            bool CardStatus = false;
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_LOGIN_STATUS WHERE CR_ACCOUNT_NBR = :CR_ACCOUNT_NBR";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            if (AccountNumber.Trim() != "")
                                cmd.Parameters.Add(":CR_ACCOUNT_NBR", AccountNumber.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                    return CardStatus;
                                else
                                {
                                    CardStatus = true;
                                    return CardStatus;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.logger.Debug(ex.Message.ToString());
                return CardStatus;
            }
        }



        #endregion

        #region OtherStaticMethod
        public static Personal_MessageDTO GetPersonalMessage(Personal_MessageDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT MESSAGE FROM  BOBVW_PERSONALMESSAGE_CH WHERE CARD_NUMBER = :CARDNUMBER";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARDNUMBER", cDto.CARD_NUMBER.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    Personal_MessageDTO PersonalMessage = new Personal_MessageDTO()
                                    {
                                        //MERCHANT_NUMBER = row["MERCHANT_NUMBER"] == null ? "" : row["MERCHANT_NUMBER"].ToString(),
                                        //FROM_DATE = row["FROM_DATE"] == null ? (DateTime?)null : DateTime.Parse(row["FROM_DATE"].ToString()),
                                        //TO_DATE = row["TO_DATE"] == null ? (DateTime?)null : DateTime.Parse(row["TO_DATE"].ToString()),
                                        MESSAGE = row["MESSAGE"] == null ? "" : row["MESSAGE"].ToString(),
                                    };
                                    return PersonalMessage;
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

        public static CH_CardDTO GetCardFees(CH_CardDTO cDto)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = @"SELECT * FROM  BOBVW_CARDFEES WHERE TARIFF_CODE IN
                                        (SELECT TARIFF_CODE FROM BOBVW_CARDS WHERE CARD_NUMBER = :CARD_NUMBER)";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            cmd.Parameters.Add(":CARD_NUMBER", cDto.card_number.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    DataRow row = ds.Tables[0].Rows[0];
                                    CH_CardDTO cardfees = new CH_CardDTO()
                                    {
                                        PIN_CAL_FEES = row["PIN_CAL_FEES"] == null ? 0 : Convert.ToDouble(row["PIN_CAL_FEES"]),
                                        RESTATEMENT_THRU_EMAIL_CHARGES = row["RESTATEMENT_THRU_EMAIL_CHARGES"] == null ? 0 : Convert.ToDouble(row["RESTATEMENT_THRU_EMAIL_CHARGES"]),
                                        STMT_REG_AMT = row["STMT_REG_AMT"] == null ? 0 : Convert.ToDouble(row["STMT_REG_AMT"]),
                                        REPLACEMENT_CHARGES = row["REPLACEMENT_CHARGES"] == null ? 0 : Convert.ToDouble(row["REPLACEMENT_CHARGES"]),
                                        RENEWAL_CHARGES = row["RENEWAL_CHARGES"] == null ? 0 : Convert.ToDouble(row["RENEWAL_CHARGES"]),
                                        EMI_PROCESSING_FEES = row["EMI_PROCESSING_FEES"] == null ? 0 : Convert.ToDouble(row["EMI_PROCESSING_FEES"]),
                                        LOAN_PROCESSING_FEES = row["LOAN_PROCESSING_FEES"] == null ? 0 : Convert.ToDouble(row["LOAN_PROCESSING_FEES"]),

                                    };
                                    return cardfees;
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

        #endregion

        #region Getlistings

        //GEt Branch Listing
        public static List<Bank_MstDTO> GetBranchList()
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        string sql = "SELECT * FROM BOBVMW_BRANCHLIST";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new Bank_MstDTO()
                                    {
                                        BRANCH_CODE = row.Field<string>("BRANCH_CODE"),
                                        BRANCH_NAME = row.Field<string>("BRANCH_NAME"),
                                    }).ToList();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                //common.logger.Debug(ex.Message.ToString());
                return null;
            }
        }

        public static List<SYSCodeDTO> GetListOfSyscode()
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        //string sql = @"SELECT CODE,SHORT_NAME FROM  BOBVW_SYSCODE WHERE TYPE_ID = :TypeID";
                        string sql = @"SELECT TYPE_ID,CODE,SHORT_NAME FROM  BOBVW_SYSCODE";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            // cmd.Parameters.Add(":TypeID", TypeID.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new SYSCodeDTO()
                                    {
                                        TYPE_ID = row.Field<string>("TYPE_ID"),
                                        CODE = row.Field<string>("CODE"),
                                        SHORT_NAME = row.Field<string>("SHORT_NAME"),
                                    }).ToList();
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

        public static List<SYSCodeDTO> GetListOfCity()
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        //string sql = @"SELECT CODE,SHORT_NAME FROM  BOBVW_SYSCODE WHERE TYPE_ID = :TypeID";
                        string sql = @"SELECT COUNTRY_CODE,CITY_CODE,CITY_NAME FROM  BOBVW_City";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            // cmd.Parameters.Add(":TypeID", TypeID.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new SYSCodeDTO()
                                    {
                                        COUNTRY_CODE = row.Field<string>("COUNTRY_CODE"),
                                        CITY_CODE = row.Field<string>("CITY_CODE"),
                                        CITY_NAME = row.Field<string>("CITY_NAME"),
                                    }).ToList();
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

        public static List<SYSCodeDTO> GetListOfCountry()
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        //string sql = @"SELECT CODE,SHORT_NAME FROM  BOBVW_SYSCODE WHERE TYPE_ID = :TypeID";
                        string sql = @"SELECT COUNTRY_CODE,COUNTRY_CODE_ALPHA,COUNTRY_NAME FROM  BOBVW_Country";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            // cmd.Parameters.Add(":TypeID", TypeID.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new SYSCodeDTO()
                                    {
                                        COUNTRY_CODE = row.Field<string>("COUNTRY_CODE"),
                                        COUNTRY_CODE_ALPHA = row.Field<string>("COUNTRY_CODE_ALPHA"),
                                        COUNTRY_NAME = row.Field<string>("COUNTRY_NAME"),
                                    }).ToList();
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

        public static List<SYSCodeDTO> GetListOfApplicationType()
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        //string sql = @"SELECT CODE,SHORT_NAME FROM  BOBVW_SYSCODE WHERE TYPE_ID = :TypeID";
                        string sql = @"SELECT TYPE_ID, CODE, SHORT_NAME, DESCRIPTION FROM  BOBVW_APPLICATION_TYPE";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            // cmd.Parameters.Add(":TypeID", TypeID.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new SYSCodeDTO()
                                    {
                                        TYPE_ID = row.Field<string>("TYPE_ID"),
                                        CODE = row.Field<string>("CODE"),
                                        SHORT_NAME = row.Field<string>("SHORT_NAME"),
                                    }).ToList();
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

        public static List<SYSCodeDTO> GetPromoCode()
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
                    {

                        //string sql = @"SELECT COUNTRY_CODE,COUNTRY_CODE_ALPHA,COUNTRY_NAME FROM  BOBVW_Country";
                        string sql = @"SELECT PROMO_CODE,DESCRIPTION FROM BOBVW_PROMOTION";
                        using (OracleCommand cmd = new OracleCommand(sql, conn))
                        {
                            // cmd.Parameters.Add(":TypeID", TypeID.Trim());
                            using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
                            {
                                dA.Fill(ds);

                                if (ds.Tables[0].Rows.Count == 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    return ds.Tables[0].AsEnumerable().Select(row => new SYSCodeDTO()
                                    {
                                        PROMO_CODE = row.Field<string>("PROMO_CODE"),
                                        DESCRIPTION = row.Field<string>("DESCRIPTION"),
                                    }).ToList();
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

        #endregion

        #region commentPart
        //public static string GetCodeNameFromSyscode(string Syscode)
        //{
        //    string FullCodeName = "";
        //    try
        //    {
        //        using (DataSet ds = new DataSet())
        //        {
        //            using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
        //            {

        //                string sql = @"SELECT SHORT_NAME FROM  BOBVW_SYSCODE WHERE CODE = :Syscode";
        //                using (OracleCommand cmd = new OracleCommand(sql, conn))
        //                {
        //                    cmd.Parameters.Add(":Syscode", Syscode.Trim());
        //                    using (OracleDataAdapter dA = new OracleDataAdapter(cmd))
        //                    {
        //                        dA.Fill(ds);

        //                        if (ds.Tables[0].Rows.Count == 0)
        //                            return FullCodeName;
        //                        else
        //                        {
        //                            DataRow row = ds.Tables[0].Rows[0];
        //                            FullCodeName = row["SHORT_NAME"] == null ? "" : Convert.ToString(row["SHORT_NAME"]);
        //                            return FullCodeName;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        common.logger.Debug(ex.Message.ToString());
        //        return null;
        //    }
        //}

        #endregion

        #region CheckConnection

        public static bool CheckOracleConnection()
        {
            using (var conn = new OracleConnection(common.GetConnectionstring()))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch( Exception ex)
                {
                    string str = ex.Message;
                    return false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        #endregion
    }
}

