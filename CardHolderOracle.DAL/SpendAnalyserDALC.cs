using System;
using System.Collections.Generic;
using System.Data;
using CardHolder.DTO;
using Oracle.DataAccess.Client;

namespace CardHolderOracle.DAL
{
   public class SpendAnalyserDALC
    {
       static string errorGenerated = "Error generated : ";
       public static List<CH_SpendAnalyserDTO> GetSpendAnalyserReportFromOracle(string AccountNumber, DateTime? Fromdate, DateTime? toDate)
       {
           List<CH_SpendAnalyserDTO> objSpendAnalyserDetails = new List<CH_SpendAnalyserDTO>();
           try
           {
               using (OracleConnection conn = new OracleConnection(common.GetConnectionstring()))
               {
                   conn.Open();
                   string sql = string.Empty;
                   sql = "SELECT DESCRIPTION, sum(AMOUNT) AMOUNT FROM BOBVW_SPENDANALYSER WHERE CR_ACCOUNT_NBR =:AccountNumber AND to_date(STATEMENT_DATE,'dd-mon-yyyy') BETWEEN to_date(:Fromdate,'dd-mon-yyyy') AND to_date(:toDate,'dd-mon-yyyy') GROUP BY DESCRIPTION";
                   DataSet ds = new DataSet();
                   OracleCommand cmd = new OracleCommand(sql, conn);
                   OracleParameter p1 = new OracleParameter("CR_ACCOUNT_NBR", OracleDbType.Varchar2, 30);
                   p1.Value = AccountNumber;
                   cmd.Parameters.Add(p1);
                   OracleParameter p2 = new OracleParameter("Fromdate", OracleDbType.Date);
                   p2.Value = Fromdate;
                   cmd.Parameters.Add(p2);
                   OracleParameter p3 = new OracleParameter("toDate", OracleDbType.Date);
                   p3.Value = toDate;
                   cmd.Parameters.Add(p3);

                   OracleDataAdapter da = new OracleDataAdapter(cmd);
                   da.Fill(ds);
                   if (ds == null || ds.Tables[0].Rows.Count <= 0)
                       return null;
                   else if (ds != null && ds.Tables[0].Rows.Count > 0)
                   {
                       foreach (DataRow dr in ds.Tables[0].Rows)
                       {
                           CH_SpendAnalyserDTO obj = new CH_SpendAnalyserDTO();
                           obj.DESCRIPTION = dr["DESCRIPTION"] == DBNull.Value ? "" : Convert.ToString(dr["DESCRIPTION"]);
                           obj.AMOUNT = dr["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDouble(dr["AMOUNT"]);
                           objSpendAnalyserDetails.Add(obj);
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               common.logger.Debug(errorGenerated + " PaymentCreditDetails :" + ex.Message.ToString());
               return null;
           }
           return objSpendAnalyserDetails;


       }
    }
}
