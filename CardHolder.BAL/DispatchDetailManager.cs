using System;
using System.Collections.Generic;
using System.Linq;
using CardHolder.DAL;
using CardHolder.DAL.Interface;
using CardHolder.DTO;
using StructureMap;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class DispatchDetailManager
    {
        #region Repository Members

        //private IRepository<Dispatch_Dtl> repDispatch_Dtl
        //{
        //    get
        //    {
        //        return ObjectFactory.GetInstance<IRepository<Dispatch_Dtl>>();
        //    }
        //}

        /// <summary>
        /// Gets the rep dispatch_ courier_ RPT.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<Dispatch_Courier_Rpt> repDispatch_Courier_Rpt
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Dispatch_Courier_Rpt>>();
            }
        }

        /// <summary>
        /// Gets the rep dispatch_ speed post_ RPT.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<Dispatch_SpeedPost_Rpt> repDispatch_SpeedPost_Rpt
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Dispatch_SpeedPost_Rpt>>();
            }
        }


        #endregion Repository Members

        #region Select methods

        #region speedPost

        /// <summary>
        /// Gets the dispatch speed post details.
        /// </summary>
        /// <param name="RefrenceNo">The refrence no.</param>
        /// <param name="DDlCardPin">The D dl card pin.</param>
        /// <param name="DispatchMonths">The dispatch months.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Dispatch_SpeedPost_RptDTO> getDispatchSpeedPostDetails(string RefrenceNo, string DDlCardPin, int DispatchMonths)
        {

            //DateTime startDate = System.DateTime.Now;
            //DateTime endDate = System.DateTime.Now.AddMonths(-DispatchMonths);
            //string startDate = System.DateTime.Now.ToString();
            //string endDate = System.DateTime.Now.AddMonths(-DispatchMonths).ToString();



            //lst = (from x in repDispatch_SpeedPost_Rpt.GetAll()
            //       where Convert.ToDateTime(x.DISPATCH_DATE).Date >= endDate.Date && Convert.ToDateTime(x.DISPATCH_DATE).Date <= startDate.Date
            //       && x.Reference_No == RefrenceNo && x.CARD_PIN.ToLower() == DDlCardPin.ToLower() && x.IsApproved == true
            //       //&& GeneralMetods x.DISPATCH_DATE.CompareTo(endDate) >= 0 && x.DISPATCH_DATE.CompareTo(startDate) <= 0
            //       select new Dispatch_SpeedPost_RptDTO
            //       {
            //           Awb_No = x.Awb_No,
            //           BATCH_NUMBER = x.BATCH_NUMBER,
            //           CARD_PIN = x.CARD_PIN,
            //           COName_on_Primary_Card = x.COName_on_Primary_Card,
            //           Courier = x.Courier,
            //           Delivery_Status = x.Delivery_Status,
            //           DISPATCH_DATE = x.DISPATCH_DATE,
            //           FullAdress = x.MailingAddress1 + "," + x.MailingAddress2 + "," + x.MailingAddress3 + "," + x.MailingAddress4 + "," + x.MailingCity + "," + x.MailingCountry + "," + x.MailingPincode + ".",
            //           MailingCity = x.MailingCity,
            //           MailingEmail = x.MailingEmail,
            //           MailingMobile = x.MailingMobile,
            //           MailingName = x.MailingName,
            //           MailingPhone = x.MailingPhone,
            //           Reference_No = x.Reference_No,
            //           REQUEST_TYPE = x.REQUEST_TYPE

            //       }).ToList();
            //return lst;

            BOBCardEntities _db = new BOBCardEntities();
            List<Dispatch_SpeedPost_RptDTO> lst = new List<Dispatch_SpeedPost_RptDTO>();
            lst = _db.getDispatchSpeedPostDetails(RefrenceNo, DDlCardPin, DispatchMonths).Select(x => new Dispatch_SpeedPost_RptDTO()
            {
                       Awb_No = x.Awb_No,
                       BATCH_NUMBER = x.BATCH_NUMBER,
                       CARD_PIN = x.CARD_PIN,
                       COName_on_Primary_Card = x.COName_on_Primary_Card,
                       Courier = x.Courier,
                       Delivery_Status = x.Delivery_Status,
                       DISPATCH_DATE = x.DISPATCH_DATE,
                //FullAdress = x.MailingAddress1 + "," + x.MailingAddress2 + "," + x.MailingAddress3 + "," + x.MailingAddress4 + "," + x.MailingCity + "," + x.MailingCountry + "," + x.MailingPincode + ".",
                       FullAdress = x.FullAdress,
                       MailingCity = x.MailingCity,
                       MailingEmail = x.MailingEmail,
                       MailingMobile = x.MailingMobile,
                       MailingName = x.MailingName,
                       MailingPhone = x.MailingPhone,
                       Reference_No = x.Reference_No,
                       REQUEST_TYPE = x.REQUEST_TYPE

                   }).ToList();
            return lst;


        }




        #endregion

        #region for CourierReport

        /// <summary>
        /// Gets the dispatch courier details.
        /// </summary>
        /// <param name="RefrenceNo">The refrence no.</param>
        /// <param name="type">The type.</param>
        /// <param name="DispatchMonths">The dispatch months.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Dispatch_Courier_RptDTO> getDispatchCourierDetails(string RefrenceNo, string DDlCardPin, int DispatchMonths)
        {
            //DateTime startDate = System.DateTime.Now;
            //DateTime endDate = System.DateTime.Now.AddMonths(-DispatchMonths);
            //List<Dispatch_Courier_RptDTO> lst = new List<Dispatch_Courier_RptDTO>();

            //lst = (from x in repDispatch_Courier_Rpt.GetAll()
            //       //where Convert.ToDateTime(x.DPUDATEFCH.Substring(6, 2) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(0, 4)).Date >= endDate.Date
            //       //&& Convert.ToDateTime(x.DPUDATEFCH.Substring(6, 2) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(0, 4)).Date <= startDate.Date
            //       where Convert.ToDateTime(x.DPUDATEFCH.Substring(0, 4) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(6, 2)).Date >= endDate.Date
            //     && Convert.ToDateTime(x.DPUDATEFCH.Substring(0, 4) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(6, 2)).Date <= startDate.Date
            //       && x.CCRCRDREF == RefrenceNo && x.CARD_PIN.ToLower() == type.ToLower() && x.IsApproved == true
            //       select new Dispatch_Courier_RptDTO
            //       {
            //           CAWBNO = x.CAWBNO,           //Airwaybill Number
            //           CCNEENAME = x.CCNEENAME,     // Consignee Name
            //           CCRCRDREF = x.CCRCRDREF,     //Reference No
            //           CCUSTCODE = x.CCUSTCODE,     //Customer Code
            //           CDSTDESC = x.CDSTDESC,       //Destination Area Description
            //           CIDNO = x.CIDNO,             //Identity Number
            //           CIDTYPE = x.CIDTYPE,         //Identity Type
            //           CORGAREA = x.CORGAREA,       //Origin Area
            //           CORGDESC = x.CORGDESC,       //Origin Area Description
            //           CPRODCODE = x.CPRODCODE,     //Product code
            //           CPRODTYPE = x.CPRODTYPE,     //Product Type
            //           CPUTIME = x.CPUTIME,         //Pickup Time
            //           CQRYGRP = x.CQRYGRP,         //Group categories
            //           CRECPID = x.CRECPID,         //Received By
            //           CRELATION = x.CRELATION,     //Relation 
            //           CRTOREASON = x.CRTOREASON,   // Reason
            //           CSENDER = x.CSENDER,         //Sender 
            //           CSPLINST = x.CSPLINST,       //Special Instruction
            //           CSTATDESC = x.CSTATDESC,     //Status Description    
            //           CSTATTIME = x.CSTATTIME,     //Status Time
            //           DEPTDTDLV = x.DEPTDTDLV,     //Expected delivery date
            //           DSTATDATE = x.DSTATDATE,     //Status Date
            //           NEWAWBNO = x.NEWAWBNO,       //New waybill number
            //           NPCS = x.NPCS,               // No. of pieces
            //           NWEIGHT = x.NWEIGHT,          //Weight 
            //           CARD_PIN = x.CARD_PIN,
            //           // DPUDATEFCH = x.DPUDATEFCH == null ? string.Empty : x.DPUDATEFCH.Substring(6, 2) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(0, 4) //Pickup Date
            //           DPUDATEFCH = x.DPUDATEFCH == null ? string.Empty : x.DPUDATEFCH.Substring(0, 4) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(6, 2)

            //       }).ToList();

            //return lst;

            BOBCardEntities _db = new BOBCardEntities();
            List<Dispatch_Courier_RptDTO> lst = new List<Dispatch_Courier_RptDTO>();

            lst = _db.getDispatchCourierDetail(RefrenceNo, DDlCardPin, DispatchMonths).Select(x => new Dispatch_Courier_RptDTO()
            {
                CAWBNO = x.CAWBNO,           //Airwaybill Number
                       CCNEENAME = x.CCNEENAME,     // Consignee Name
                       CCRCRDREF = x.CCRCRDREF,     //Reference No
                       CCUSTCODE = x.CCUSTCODE,     //Customer Code
                       CDSTDESC = x.CDSTDESC,       //Destination Area Description
                       CIDNO = x.CIDNO,             //Identity Number
                       CIDTYPE = x.CIDTYPE,         //Identity Type
                       CORGAREA = x.CORGAREA,       //Origin Area
                       CORGDESC = x.CORGDESC,       //Origin Area Description
                       CPRODCODE = x.CPRODCODE,     //Product code
                       CPRODTYPE = x.CPRODTYPE,     //Product Type
                       CPUTIME = x.CPUTIME,         //Pickup Time
                       CQRYGRP = x.CQRYGRP,         //Group categories
                       CRECPID = x.CRECPID,         //Received By
                       CRELATION = x.CRELATION,     //Relation 
                       CRTOREASON = x.CRTOREASON,   // Reason
                       CSENDER = x.CSENDER,         //Sender 
                       CSPLINST = x.CSPLINST,       //Special Instruction
                       CSTATDESC = x.CSTATDESC,     //Status Description    
                       CSTATTIME = x.CSTATTIME,     //Status Time
                       DEPTDTDLV = x.DEPTDTDLV,     //Expected delivery date
                       DSTATDATE = x.DSTATDATE,     //Status Date
                       NEWAWBNO = x.NEWAWBNO,       //New waybill number
                       NPCS = x.NPCS,               // No. of pieces
                       NWEIGHT = x.NWEIGHT,          //Weight 
                       CARD_PIN = x.CARD_PIN,
                       // DPUDATEFCH = x.DPUDATEFCH == null ? string.Empty : x.DPUDATEFCH.Substring(6, 2) + "/" + x.DPUDATEFCH.Substring(4, 2) + "/" + x.DPUDATEFCH.Substring(0, 4) //Pickup Date
                       DPUDATEFCH = x.DPUDATEFCH == null ? string.Empty : x.DPUDATEFCH.Substring(0, 5) + "/" + x.DPUDATEFCH.Substring(5, 2) + "/" + x.DPUDATEFCH.Substring(7, 2)

                   }).ToList();

            return lst;


        }


        //public string SaveCourierReport(Dispatch_Courier_RptDTO objCourierRptDTO)
        //{
        //    Dispatch_Courier_Rpt obj = repDispatch_Courier_Rpt.SingleOrDefault(c => c.CAWBNO == objCourierRptDTO.CAWBNO);

        //    if (obj == null || obj.CAWBNO != objCourierRptDTO.CAWBNO)
        //    {
        //        obj = new Dispatch_Courier_Rpt();
        //    }

        //    obj.CAWBNO = objCourierRptDTO.CAWBNO;                  //Airwaybill Number 
        //    obj.CCRCRDREF = objCourierRptDTO.CCRCRDREF;           //Reference No
        //    obj.DPUDATEFCH = objCourierRptDTO.DPUDATEFCH;        // Pickup Date
        //    obj.CORGDESC = objCourierRptDTO.CORGDESC;           //Origin Area Description
        //    obj.CDSTDESC = objCourierRptDTO.CDSTDESC;          //Destination Area Description
        //    obj.CCNEENAME = objCourierRptDTO.CCNEENAME;       //Consignee Name 
        //    obj.CSTATDESC = objCourierRptDTO.CSTATDESC;      //Status Description 
        //    obj.CQRYGRP = objCourierRptDTO.CQRYGRP;         //Group  
        //    obj.DSTATDATE = objCourierRptDTO.DSTATDATE;    // Status Date
        //    obj.CSTATTIME = objCourierRptDTO.CSTATTIME;   //Status Time
        //    obj.CRECPID = objCourierRptDTO.CRECPID;      //Received By
        //    obj.CRELATION = objCourierRptDTO.CRELATION; //Relation 
        //    obj.CIDTYPE = objCourierRptDTO.CIDTYPE;    // Identity Type
        //    obj.CIDNO = objCourierRptDTO.CIDNO;       //Identity Number
        //    obj.CORGAREA = objCourierRptDTO.CORGAREA;// Origin Area
        //    obj.CCUSTCODE = objCourierRptDTO.CCUSTCODE;             // Customer Code
        //    obj.NWEIGHT = objCourierRptDTO.NWEIGHT;                //Weight 
        //    obj.CPUTIME = objCourierRptDTO.CPUTIME;               //Pickup Time
        //    obj.NPCS = objCourierRptDTO.NPCS;                    // No. of pieces
        //    obj.DEPTDTDLV = objCourierRptDTO.DEPTDTDLV;         //Expected delivery date
        //    obj.CSPLINST = objCourierRptDTO.CSPLINST;          //Special Instruction
        //    obj.NEWAWBNO = objCourierRptDTO.NEWAWBNO;         //New waybill number
        //    obj.CSENDER = objCourierRptDTO.CSENDER;          //Sender 
        //    obj.CRTOREASON = objCourierRptDTO.CRTOREASON;   //Reason
        //    obj.CPRODCODE = objCourierRptDTO.CPRODCODE;    //Product code
        //    obj.CPRODTYPE = objCourierRptDTO.CPRODTYPE;   //Product type


        //    if (obj.CAWBNO != "")
        //    {
        //        repDispatch_Courier_Rpt.Add(obj);
        //    }


        //    GeneralManager.Commit();

        //    return obj.CAWBNO;
        //}


        //public List<Dispatch_SpeedPost_RptDTO> getDispatchSpeedPostCourierDetails(string RefrenceNo)
        //{
        //    List<Dispatch_SpeedPost_RptDTO> lst = new List<Dispatch_SpeedPost_RptDTO>();


        //    lst = (from x in repDispatch_SpeedPost_Rpt.GetQuery()
        //           where x.Reference_No == RefrenceNo
        //           select new Dispatch_SpeedPost_RptDTO
        //           {
        //               Awb_No = x.Awb_No,
        //               BATCH_NUMBER = x.BATCH_NUMBER,
        //               CARD_PIN = x.CARD_PIN,
        //               COName_on_Primary_Card = x.COName_on_Primary_Card,
        //               Courier = x.Courier,
        //               Delivery_Status = x.Delivery_Status,
        //               DISPATCH_DATE = x.DISPATCH_DATE,
        //               FullAdress = x.MailingAddress1 + "," + x.MailingAddress2 + "," + x.MailingAddress3 + "," + x.MailingAddress4 + "," + x.MailingCity + "," + x.MailingCountry + "," + x.MailingPincode + ".",
        //               MailingEmail = x.MailingEmail,
        //               MailingMobile = x.MailingMobile,
        //               MailingName = x.MailingName,
        //               MailingPhone = x.MailingPhone,
        //               Reference_No = x.Reference_No,
        //               REQUEST_TYPE = x.REQUEST_TYPE

        //           }).ToList();
        //    return lst;


        //}



        #endregion

        #endregion


    }


}
