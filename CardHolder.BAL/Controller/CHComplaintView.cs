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
    public static class CHComplaintView
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

        public static string GetCommonField(Dictionary<string, string> FD, Field field)
        {
            return GetRowTemplate(field.Text, FD[field.Value]);
        }
        #endregion


        #region Process Template
        public static string ProcessSwitch(Dictionary<string, string> FD, Field field)
        {
            return GetCommonField(FD, field);
        }

        public static string ProcessTemplate(object CommandArgument)
        {
            string detail = "";

            ///STEP 1 Split Details & Fetch Request Detail 
            string[] p = Convert.ToString(CommandArgument).Split(';');

            //long Compl_detail = Convert.ToInt64(p[0]);
            //CardHolderComplaintManager cdm = new CardHolderComplaintManager();
            //CH_Complaint_DtlDTO complaint_dlt = cdm.FindComplaintDetail(Compl_detail);

            /////STEP 2 Get Card Details Using Card Holder
            //long created_by = complaint_dlt.Created_by;
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


            detail += GetRowTemplate("Complaint Date", GeneralMethods.FormatDate(Convert.ToDateTime(p[1])));
            detail += GetRowTemplate("Complaint Number", p[2]);
            detail += GetRowTemplate("Complaint Type Name", p[3]);
            detail += GetRowTemplate("Your Remark", p[4]);
            detail += GetRowTemplate("Complaint Status", p[5]);

            if (!string.IsNullOrEmpty(p[4]) && p[4].ToLower() == "pending")
                detail += GetRowTemplate("Back-Office's Remark", "No Remark");
            else
                detail += GetRowTemplate("Back-Office's Remark", p[6]);


            return detail;
        }
        #endregion
    }
}