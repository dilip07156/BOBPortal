using System;
using CardHolder.DAL;
using CardHolder.DAL.Base;
using CardHolder.DAL.Interface;
using StructureMap;
using System.Data.Objects;
using System.Linq;
using System.Xml.Linq;
using CardHolder.DTO;
using System.Collections.Generic;
using CardHolder.Utility;

namespace CardHolder.BAL.Controller
{
    public static class CHRequestView
    {

        /// <summary>
        /// Row Template
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetRowTemplate(string Text, string Value)
        {
            return string.Format("<tr><th>{0} :</th><td>{1}</td></tr>", Text, Value);
        }

        #region Fetch Field From Database
        //public static string GetPointRedeem(Dictionary<string, string> FD, Field field)
        //{

        //    return GetRowTemplate(field.Text, FD[field.Value]);
        //}

        public static string GetPaymentType(Dictionary<string, string> FD, Field field)
        {
            if (FD[field.Value] == "1")
            {
                return GetRowTemplate(field.Text, "Total Amount Due");
            }
            else if (FD[field.Value] == "2")
            {
                return GetRowTemplate(field.Text, "Minimum Amount Due");
            }
            else if (FD[field.Value] == "3")
            {
                return GetRowTemplate(field.Text, "Specific % of monthly Due " + FD["Specific_Monthly_due"]);
            }
            return "";
        }

        public static string GetRequestReason(Dictionary<string, string> FD, Field field)
        {
            CardHolderRequestManager chrm = new CardHolderRequestManager();
            string reason = string.Empty;
            if (FD[field.Value] != "")
            {
                reason = chrm.getCHRequestReasonById(Convert.ToInt64(FD[field.Value])).Reason_nm;
            }
            else
            {
                reason = string.Empty;
            }

            return GetRowTemplate(field.Text, reason);
        }


        public static string GetBalanceTransferredPlan(Dictionary<string, string> FD, Field field)
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            return GetRowTemplate(field.Text, dhm.SearchDllDetailById(Convert.ToInt32(FD[field.Value])).Description);
        }

        public static string GetRelation(Dictionary<string, string> FD, Field field)
        {
            DropdownHdrManager dhm = new DropdownHdrManager();
            return GetRowTemplate(field.Text, dhm.SearchDllDetailById(Convert.ToInt32(FD[field.Value])).Description);
        }

        public static string GetDOB(Dictionary<string, string> FD, Field field)
        {
            return GetRowTemplate(field.Text, DateTime.Parse(FD[field.Value]).ToString("MM/dd/yyyy"));
        }
        public static string GetGender(Dictionary<string, string> FD, Field field)
        {
            return GetRowTemplate(field.Text, FD[field.Value] == "M" ? "Male" : "Female");
        }

        public static string GetCommonField(Dictionary<string, string> FD, Field field)
        {
            if (field.Text.ToLower().Replace(" ", "") == "cardtobehotlist")
            {
                if (FD[field.Value] != "")
                    return GetRowTemplate(field.Text, FD[field.Value].Decrypt());
                else
                    return GetRowTemplate(field.Text, FD[field.Value]);
            }
            else
                return GetRowTemplate(field.Text, FD[field.Value]);
        }
        #endregion


        #region Process Template
        public static string ProcessSwitch(Dictionary<string, string> FD, Field field)
        {
            if (field.Value.Trim() == "RequestReason_Id")
            {
                return GetRequestReason(FD, field);
            }
            else if (field.Value.Trim() == "DOB")
            {
                return GetDOB(FD, field);
            }
            else if (field.Value.Trim() == "Relation")
            {
                return GetRelation(FD, field);
            }
            else if (field.Value.Trim() == "Gender")
            {
                return GetGender(FD, field);
            }
            else if (field.Value.Trim() == "Payment_Type")
            {
                return GetPaymentType(FD, field);
            }
            else if (field.Value.Trim() == "Balance_Transferred_Plan")
            {
                return GetBalanceTransferredPlan(FD, field);
            }
            return GetCommonField(FD, field);
        }

        public static string ProcessTemplate(object CommandArgument)
        {
            string detail = "";

            ///STEP 1 Split Details & Fetch Request Detail 
            string[] p = Convert.ToString(CommandArgument).Split(';');

            long req_dlt_id = Convert.ToInt64(p[0]);
            CHRequestDetailManager cdm = new CHRequestDetailManager();
            CH_Request_DtlDTO request_dlt = cdm.FindRequestDetail(req_dlt_id);
            if (string.IsNullOrEmpty(p[5]))
                p[5] = "No Remark";
            ///STEP 2 Get Card Details Using Card Holder
            //long created_by = request_dlt.Created_by;
            //CardHolderManager chm = new CardHolderManager();
            //CardHolder_MstDTO ch = chm.getUserByID(created_by);
            //CardManager cm = new CardManager();
            //CH_CardDTO card = cm.GetCardByCreditCardNumber(new CH_CardDTO()
            //{
            //    card_number = ch.creditcard_acc_number
            //});
            //detail += GetRowTemplate("Name of Card-Holder", card.Embossed_Name);
            //detail += GetRowTemplate("Card Number", card.card_number);
            //detail += GetRowTemplate("Credit Account Number", card.Cr_Account_Nbr);
            detail += GetRowTemplate("Request Date", GeneralMethods.FormatDate(Convert.ToDateTime(p[1]))); //.ToString("dd/MM/yyyy"));
            detail += GetRowTemplate("Request Number", p[2]);
            detail += GetRowTemplate("RequestType Name", p[3]);
            detail += GetRowTemplate("Request Status", p[4]);

            ///STEP 3 Load Fields As Per 
            string request_type = p[3];
            XElement root = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Filters\\CH-Request.xml").Root;
            var request = from x in root.Descendants("Request") where x.Attribute("type").Value == request_type select x;
            var fields = from y in request.Descendants("field")
                         select new Field()
                         {
                             Text = y.Attribute("Text").Value,
                             Value = y.Value,
                         };


            ///STEP 4 Convert To DataDictionary & Compare                
            Dictionary<string, string> FD = (from x in request_dlt.GetType().GetProperties() select x).ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(request_dlt, null) == null ? "" : x.GetGetMethod().Invoke(request_dlt, null).ToString()));
            foreach (var field in fields)
            {
                detail += ProcessSwitch(FD, field);
            }
            detail += GetRowTemplate("Your Remark", p[5]);
            if (!string.IsNullOrEmpty(p[4]) && p[4].ToLower() == "pending")
                detail += GetRowTemplate("Back-Office's Remark", "No Remark");
            else if (!string.IsNullOrEmpty(p[4]) && p[4].ToLower() == "Approved")
                detail += GetRowTemplate("Back-Office's Remark", p[6]);
            else if (!string.IsNullOrEmpty(p[4]) && p[4].ToLower() == "Rejected")
                detail += GetRowTemplate("Back-Office's Remark", p[6]);
            else
                detail += GetRowTemplate("Back-Office's Remark", p[6]);
            return detail;
        }
        #endregion
    }
}