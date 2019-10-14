using System;
using System.Collections.Generic;
using CardHolder.DTO;
using CardHolderOracle.DAL;

namespace CardHolder.BAL
{
   public class SpendAnalyserManager
    {
        /// <summary>
        /// Gets the day wise spend analyser report.
        /// </summary>
        /// <param name="merchantId">The merchant id.</param>
        /// <param name="fromYear">From year.</param>
        /// <param name="toYear">To year.</param>
        /// <returns></returns>
       public List<CH_SpendAnalyserDTO> GetSpendAnalyserReport(string AccountNumber, DateTime? Fromdate, DateTime? toDate)
        {
            List<CH_SpendAnalyserDTO> lstBusinessStatisticReport = new List<CH_SpendAnalyserDTO>();
            lstBusinessStatisticReport = SpendAnalyserDALC.GetSpendAnalyserReportFromOracle(AccountNumber, Fromdate, toDate);
            return lstBusinessStatisticReport;
        }
    }
}
