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
    public class CHRequestDetailManager
    {
        /// <summary>
        /// Gets the rep C h_ request_ DTL.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_Request_Dtl> repCH_Request_Dtl
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_Request_Dtl>>();
            }
        }

        private IRepository<CH_Complaint_Dtl> repCH_Complaint_Dtl
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_Complaint_Dtl>>();
            }
        }

        /// <summary>
        /// Gets the rep AuditLog.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<AuditLog> AuditLog_Dtl
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AuditLog>>();
            }
        }
        /// <summary>
        /// Gets the rep C h_ EM i_ request_ DTL.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_EMI_Request_Dtl> repCH_EMI_Request_Dtl
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_EMI_Request_Dtl>>();
            }
        }


        /// <summary>
        /// This method is used for getting International Limit amount 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CH_Request_DtlDTO GetInternationalLimitAmount(long CardHolder_Id, long RequestTypeId, string HotlistingCardNumber)
        {
            BOBCardEntities _db = new BOBCardEntities();
            var lst = _db.GetInternationalLimitAmount(CardHolder_Id, RequestTypeId, HotlistingCardNumber).FirstOrDefault();
            if (lst != null)
            {
                CH_Request_DtlDTO result = new CH_Request_DtlDTO()
                {
                    Loan_Principal_Amt = lst.Loan_Principal_Amt,
                    Request_Status = lst.Request_Status,
                    RequestFlag = lst.RequestFlag
                  
                };
                return result;
            }
            else
            {
                return null;
            }

        }

        
        /// <summary>
        /// Saves the request detail.
        /// </summary>
        /// <param name="objRequest_DltDTO">The obj request_ DLT DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>

        //Commented by abhijeet on 24/01/2019 for performance
        /*
        public long SaveRequestDetail(CH_Request_DtlDTO objRequest_DltDTO)
        {
            CH_Request_Dtl obj = repCH_Request_Dtl.SingleOrDefault(c => c.Request_Dtl_Id == objRequest_DltDTO.Request_Dtl_Id);


            if (obj == null || obj.Request_Dtl_Id != objRequest_DltDTO.Request_Dtl_Id)
            {
                obj = new CH_Request_Dtl();
            }

            obj.Request_Dt = objRequest_DltDTO.Request_Dt;
            obj.CardHolder_Id = objRequest_DltDTO.CardHolder_Id;
            obj.RequestType_Id = objRequest_DltDTO.RequestType_Id;
            obj.IP_Address = objRequest_DltDTO.IP_Address;
            obj.RequestReason_Id = objRequest_DltDTO.RequestReason_Id;
            obj.Mode_Send_Statment = objRequest_DltDTO.Mode_Send_Statment;
            obj.Statement_month = objRequest_DltDTO.Statement_month;
            obj.Statement_year = objRequest_DltDTO.Statement_year;
            obj.DOB = objRequest_DltDTO.DOB;
            obj.Relation = objRequest_DltDTO.Relation;
            obj.Gender = objRequest_DltDTO.Gender;
            obj.Addon_Profile_Photo = objRequest_DltDTO.Addon_Profile_Photo;
            obj.Payment_Type = objRequest_DltDTO.Payment_Type;
            obj.Specific_Monthly_due = objRequest_DltDTO.Specific_Monthly_due;
            obj.Points_Wants_Redeem = objRequest_DltDTO.Points_Wants_Redeem;
            obj.Bank_nm = objRequest_DltDTO.Bank_nm;
            obj.Transferred_Amt = objRequest_DltDTO.Transferred_Amt;
            obj.Balance_Transferred_Plan = objRequest_DltDTO.Balance_Transferred_Plan;
            obj.EMI_Principal_Amt = objRequest_DltDTO.EMI_Principal_Amt;
            obj.EMI_Terms = objRequest_DltDTO.EMI_Terms;
            obj.Remarks = objRequest_DltDTO.Remarks;
            obj.Request_Status = objRequest_DltDTO.Request_Status;
            obj.Add_On_Card_Applicant = objRequest_DltDTO.Add_On_Card_Applicant;
            obj.EMI_Amount = objRequest_DltDTO.EMI_Amount;
            obj.EMI_InterestRate = objRequest_DltDTO.EMI_InterestRate;
            obj.OtherCreditCardNumber = objRequest_DltDTO.OtherCreditCardNumber;
            obj.HotlistingCardNumber = objRequest_DltDTO.HotlistingCardNumber;
            obj.Loan_Amount = objRequest_DltDTO.Loan_Amount;
            obj.Loan_InterestRate = objRequest_DltDTO.Loan_InterestRate;
            obj.Loan_Principal_Amt = objRequest_DltDTO.Loan_Principal_Amt;
            obj.Loan_Terms = objRequest_DltDTO.Loan_Terms;

            if (obj.Request_Dtl_Id == 0)
            {
                obj.Created_by = objRequest_DltDTO.Created_by;
                obj.Created_dt = objRequest_DltDTO.Created_dt;
                obj.UID = objRequest_DltDTO.Created_dt.ToString("yyyyMMddHHmmssffff");
            }

            obj.Updated_by = objRequest_DltDTO.Updated_by;
            obj.Updated_dt = objRequest_DltDTO.Updated_dt;

            if (obj.Request_Dtl_Id == 0)
            {
                repCH_Request_Dtl.Add(obj);
            }


            GeneralManager.Commit();

            return obj.Request_Dtl_Id;

        }
        */

        public long SaveRequestDetail(CH_Request_DtlDTO objRequest_DltDTO)
        {
            //commented and rewritten by abhijeet on 22/08/2019 for doing Add and Update in same SP
            /*if (objRequest_DltDTO.Request_Dtl_Id == 0)
            {*/
                BOBCardEntities _db = new BOBCardEntities();
                var requestDtl = _db.saveRequestDtl(
                objRequest_DltDTO.Request_Dtl_Id,
                objRequest_DltDTO.Request_Dt,
                objRequest_DltDTO.CardHolder_Id,
                objRequest_DltDTO.RequestType_Id,
                objRequest_DltDTO.RequestReason_Id,
                objRequest_DltDTO.Mode_Send_Statment,
                objRequest_DltDTO.DOB,
                objRequest_DltDTO.Relation,
                objRequest_DltDTO.Gender,
                objRequest_DltDTO.Addon_Profile_Photo,
                objRequest_DltDTO.Payment_Type,
                objRequest_DltDTO.Specific_Monthly_due,
                objRequest_DltDTO.Points_Wants_Redeem,
                objRequest_DltDTO.Bank_nm,
                objRequest_DltDTO.Transferred_Amt,
                objRequest_DltDTO.Balance_Transferred_Plan,
                objRequest_DltDTO.EMI_Principal_Amt,
                objRequest_DltDTO.EMI_Terms,
                objRequest_DltDTO.EMI_InterestRate,
                objRequest_DltDTO.EMI_Amount,
                objRequest_DltDTO.Remarks,
                objRequest_DltDTO.Request_Status,
                objRequest_DltDTO.Created_by,
                objRequest_DltDTO.Created_dt,
                objRequest_DltDTO.Updated_by,
                objRequest_DltDTO.Updated_dt,
                objRequest_DltDTO.IP_Address,
                objRequest_DltDTO.Statement_month,
                objRequest_DltDTO.Statement_year,
                objRequest_DltDTO.Add_On_Card_Applicant,
                objRequest_DltDTO.OtherCreditCardNumber,
                objRequest_DltDTO.HotlistingCardNumber,
                null,
                null,
                objRequest_DltDTO.Created_dt.ToString("yyyyMMddHHmmssffff"),
                objRequest_DltDTO.AdminRemarks,
                null,
                null,
                null,
                objRequest_DltDTO.Loan_Terms,
                objRequest_DltDTO.Loan_InterestRate,
                objRequest_DltDTO.Loan_Amount,
                objRequest_DltDTO.Loan_Principal_Amt,
                objRequest_DltDTO.RequestFlag,
                objRequest_DltDTO.request_Microfilm_Ref_NumberParameter).ToList();

                return (long)requestDtl[0];
            /*}
            else
            {
                CH_Request_Dtl obj = repCH_Request_Dtl.SingleOrDefault(c => c.Request_Dtl_Id == objRequest_DltDTO.Request_Dtl_Id);


                if (obj == null || obj.Request_Dtl_Id != objRequest_DltDTO.Request_Dtl_Id)
                {
                    obj = new CH_Request_Dtl();
                }

                obj.Request_Dt = objRequest_DltDTO.Request_Dt;
                obj.CardHolder_Id = objRequest_DltDTO.CardHolder_Id;
                obj.RequestType_Id = objRequest_DltDTO.RequestType_Id;
                obj.IP_Address = objRequest_DltDTO.IP_Address;
                obj.RequestReason_Id = objRequest_DltDTO.RequestReason_Id;
                obj.Mode_Send_Statment = objRequest_DltDTO.Mode_Send_Statment;
                obj.Statement_month = objRequest_DltDTO.Statement_month;
                obj.Statement_year = objRequest_DltDTO.Statement_year;
                obj.DOB = objRequest_DltDTO.DOB;
                obj.Relation = objRequest_DltDTO.Relation;
                obj.Gender = objRequest_DltDTO.Gender;
                obj.Addon_Profile_Photo = objRequest_DltDTO.Addon_Profile_Photo;
                obj.Payment_Type = objRequest_DltDTO.Payment_Type;
                obj.Specific_Monthly_due = objRequest_DltDTO.Specific_Monthly_due;
                obj.Points_Wants_Redeem = objRequest_DltDTO.Points_Wants_Redeem;
                obj.Bank_nm = objRequest_DltDTO.Bank_nm;
                obj.Transferred_Amt = objRequest_DltDTO.Transferred_Amt;
                obj.Balance_Transferred_Plan = objRequest_DltDTO.Balance_Transferred_Plan;
                obj.EMI_Principal_Amt = objRequest_DltDTO.EMI_Principal_Amt;
                obj.EMI_Terms = objRequest_DltDTO.EMI_Terms;
                obj.Remarks = objRequest_DltDTO.Remarks;
                obj.Request_Status = objRequest_DltDTO.Request_Status;
                obj.Add_On_Card_Applicant = objRequest_DltDTO.Add_On_Card_Applicant;
                obj.EMI_Amount = objRequest_DltDTO.EMI_Amount;
                obj.EMI_InterestRate = objRequest_DltDTO.EMI_InterestRate;
                obj.OtherCreditCardNumber = objRequest_DltDTO.OtherCreditCardNumber;
                obj.HotlistingCardNumber = objRequest_DltDTO.HotlistingCardNumber;
                obj.Loan_Amount = objRequest_DltDTO.Loan_Amount;
                obj.Loan_InterestRate = objRequest_DltDTO.Loan_InterestRate;
                obj.Loan_Principal_Amt = objRequest_DltDTO.Loan_Principal_Amt;
                obj.Loan_Terms = objRequest_DltDTO.Loan_Terms;
                obj.RequestFlag = objRequest_DltDTO.RequestFlag;

                if (obj.Request_Dtl_Id == 0)
                {
                    obj.Created_by = objRequest_DltDTO.Created_by;
                    obj.Created_dt = objRequest_DltDTO.Created_dt;
                    obj.UID = objRequest_DltDTO.Created_dt.ToString("yyyyMMddHHmmssffff");
                }

                obj.Updated_by = objRequest_DltDTO.Updated_by;
                obj.Updated_dt = objRequest_DltDTO.Updated_dt;

                if (obj.Request_Dtl_Id == 0)
                {
                    repCH_Request_Dtl.Add(obj);
                }


                GeneralManager.Commit();

                return obj.Request_Dtl_Id;

            }*/

        }


        public long SaveAuditLog(AuditLog_DTO objAuditLog_DltDTO)
        {
            BOBCardEntities _db = new BOBCardEntities();
            var requestDtl = _db.saveAuditLog(
            objAuditLog_DltDTO.Id,
            objAuditLog_DltDTO.RequestType_Id,
            objAuditLog_DltDTO.CardHolder_Id,
            objAuditLog_DltDTO.TxnType,
            objAuditLog_DltDTO.Credit_card_number,
            objAuditLog_DltDTO.RequestReason,
            objAuditLog_DltDTO.TxnReferenceNo,
            objAuditLog_DltDTO.ResponseStatus,
            objAuditLog_DltDTO.Created_by,
            objAuditLog_DltDTO.Created_dt,
            objAuditLog_DltDTO.Updated_by,
            objAuditLog_DltDTO.Updated_dt,
            objAuditLog_DltDTO.BankRefNo,
            objAuditLog_DltDTO.IP_Address).ToList();
            return (long)requestDtl[0];

        }

        /// <summary>
        /// Gets the CH request details.
        /// </summary>
        /// <param name="cardholderId">The cardholder id.</param>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_Request_DtlDTO> getCHRequestDetails(long cardholderId, int SkipCount, int PageSize, ref int RecordCount)
        {
           
            try
            {
                List<CH_Request_DtlDTO> lst = new List<CH_Request_DtlDTO>();                
                BOBCardEntities _db = new BOBCardEntities();
                var result = _db.getCHRequestDetails(cardholderId).ToList();
                foreach (var item in result)
                {
                    CH_Request_DtlDTO cH_Request_DtlDTO = new CH_Request_DtlDTO()
                    {
                        Request_Dtl_Id = item.Request_Dtl_Id,
                        Request_Dt = item.Request_Dt,
                        CardHolder_Id = item.CardHolder_Id,
                        RequestType_Id = item.RequestType_Id,
                        RequestType = item.RequestType_nm,
                        Remarks = item.Remarks,
                        RequestReason_Id = item.RequestReason_Id,
                        UID = item.UID,
                        Request_Status = item.Request_Status,
                        AdminRemarks = item.AdminRemarks
                    };
                    lst.Add(cH_Request_DtlDTO);
                }
                
                RecordCount = lst.Count();
                lst = lst.Skip(SkipCount).Take(PageSize).ToList();
                return lst;

            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return null;
            }
           
            
            
        }


        /// <summary>
        /// Gets the request UID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_Request_DtlDTO getRequestUID(long RequestDtlID)
        {
            //CardHolderManager chm = new CardHolderManager();
            /*
            return (from x in repCH_Request_Dtl.GetAll()
                    where RequestDtlID == x.Request_Dtl_Id
                    select new CH_Request_DtlDTO()
                    {
                        UID = x.UID,
                    }).FirstOrDefault();
            */
            BOBCardEntities _db = new BOBCardEntities();
            var requestDtl = _db.getRequest_UID(RequestDtlID).Select(u => new CH_Request_DtlDTO()
            {
                UID = u.UID
            });

            return requestDtl.First();

        }

        #region EMI Request

        /// <summary>
        /// Saves the EMI request DTL.
        /// </summary>
        /// <param name="objEMIRequest_DltDTO">The obj EMI request_ DLT DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public long SaveEMIRequestDtl(CH_EMI_Request_DTO objEMIRequest_DltDTO)
        {
            CH_EMI_Request_Dtl obj = repCH_EMI_Request_Dtl.SingleOrDefault(c => c.Oracle_EMI_Id == objEMIRequest_DltDTO.Oracle_EMI_Id);
            if (obj == null)
            {
                obj = new CH_EMI_Request_Dtl();
            }
            obj.Oracle_EMI_Id = objEMIRequest_DltDTO.Oracle_EMI_Id;
            obj.Creditcard_acc_number = objEMIRequest_DltDTO.Creditcard_acc_number;
            obj.EMI_Loan_Type = objEMIRequest_DltDTO.EMI_Loan_Type;

            if (obj.EMI_Id == 0)
            {
                obj.Created_by = objEMIRequest_DltDTO.Created_by;
                obj.Created_dt = objEMIRequest_DltDTO.Created_dt;
            }
            obj.Updated_by = objEMIRequest_DltDTO.Updated_by;
            obj.Updated_dt = objEMIRequest_DltDTO.Updated_dt;
            obj.IP_Address = objEMIRequest_DltDTO.IP_Address;
            if (obj.EMI_Id == 0)
            {
                repCH_EMI_Request_Dtl.Add(obj);
            }
            GeneralManager.Commit();

            return obj.EMI_Id;
        }

        /// <summary>
        /// Gets the EMI requests.
        /// </summary>
        /// <param name="CrAccountNumber">The cr account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_EMI_Request_DTO> GetEMILoanRequests(string CrAccountNumber, string strType)
        {
            List<CH_EMI_Request_DTO> lstEMI = new List<CH_EMI_Request_DTO>();

            lstEMI = (from a in repCH_EMI_Request_Dtl.GetAll()
                      where a.Creditcard_acc_number == CrAccountNumber && a.EMI_Loan_Type == strType
                      select new CH_EMI_Request_DTO
                      {
                          EMI_Id = a.EMI_Id,
                          Oracle_EMI_Id = a.Oracle_EMI_Id,
                          Creditcard_acc_number = a.Creditcard_acc_number,
                          Created_dt = a.Created_dt
                      }).ToList();

            return lstEMI;
        }

        /// <summary>
        /// Find Request By Primary Key Request_Dtl_Id
        /// </summary>
        /// <param name="Request_Dtl_Id">The request_ DTL_ id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_Request_DtlDTO FindRequestDetail(long Request_Dtl_Id)
        {
            CH_Request_DtlDTO req_detail = repCH_Request_Dtl.Find(c => c.Request_Dtl_Id == Request_Dtl_Id).Select(
                 x => new CH_Request_DtlDTO()
                 {
                     Request_Dtl_Id = x.Request_Dtl_Id,
                     Request_Dt = x.Request_Dt,
                     CardHolder_Id = x.CardHolder_Id,
                     RequestType_Id = x.RequestType_Id,
                     IP_Address = x.IP_Address,
                     RequestReason_Id = x.RequestReason_Id,
                     Mode_Send_Statment = x.Mode_Send_Statment,
                     Statement_month = x.Statement_month,
                     Statement_year = x.Statement_year,
                     DOB = x.DOB,
                     Relation = x.Relation,
                     Gender = x.Gender,
                     Addon_Profile_Photo = x.Addon_Profile_Photo,
                     Payment_Type = x.Payment_Type,
                     Specific_Monthly_due = x.Specific_Monthly_due,
                     Points_Wants_Redeem = x.Points_Wants_Redeem,
                     Bank_nm = x.Bank_nm,
                     Transferred_Amt = x.Transferred_Amt,
                     Balance_Transferred_Plan = x.Balance_Transferred_Plan,
                     EMI_Principal_Amt = x.EMI_Principal_Amt,
                     EMI_Terms = x.EMI_Terms,
                     Remarks = x.Remarks,
                     Request_Status = x.Request_Status,
                     Add_On_Card_Applicant = x.Add_On_Card_Applicant,
                     EMI_Amount = x.EMI_Amount,
                     EMI_InterestRate = x.EMI_InterestRate,
                     OtherCreditCardNumber = x.OtherCreditCardNumber,
                     HotlistingCardNumber = x.HotlistingCardNumber,
                     Loan_Amount = x.Loan_Amount,
                     Loan_InterestRate = x.Loan_InterestRate,
                     Loan_Principal_Amt = x.Loan_Principal_Amt,
                     Loan_Terms = x.Loan_Terms,
                     Created_by = x.Created_by,
                     Created_dt = x.Created_dt,
                     Updated_by = x.Updated_by,
                     Updated_dt = x.Updated_dt,
                     UID = x.UID,
                 }
                ).Single();

            return req_detail;
        }


        #endregion

        #region Get status record on dashboard
        /// <summary>
        /// Gets the request UID.
        /// </summary>
        /// <param name="RequestDtlID">The request DTL ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_Request_DtlDTO> GetRequestStatusRecord(long cardholderID)
        {           
            BOBCardEntities _db = new BOBCardEntities();
            var requestDtl = _db.getRequestStatusRecord(cardholderID).Select(u => new CH_Request_DtlDTO()
            {
                RequestType = u.RequestType,
                Request_Dtl_Id = u.Request_Dtl_Id,
                Request_Status = u.Request_Status,
                UID = u.UID,
                Remarks = u.Remarks,
                RequestorComplaint = u.RequestorComplaint,
                Request_Dt = u.Request_Dt,
                AdminRemarks = u.AdminRemarks
            });

            return requestDtl.ToList();

        }
        #endregion

        #region get total count of pending request based on cardholderid and requesttypeid
        /// <summary>
        /// get total count of pending request based on cardholderid and requesttypeid
        /// </summary>
        /// <param name="CardHolder_Id"></param>
        /// <param name="RequestTypeId"></param>
        /// <param name="DEFAULT_STATUS"></param>
        /// <returns></returns>
        public int CheckRequestPending(long CardHolder_Id, long RequestTypeId, string DEFAULT_STATUS)
        {
            BOBCardEntities _db = new BOBCardEntities();
            var count = _db.getCheckPendingRequestCount(CardHolder_Id, RequestTypeId, DEFAULT_STATUS).FirstOrDefault();
            return count.Value;
        }


        /// <summary>
        /// This method is used for getting card details for Auto Debit Payment Type
        /// </summary>
        /// <param name="cDto">cDto</param>
        /// <returns>CH_CardDTO</returns>
        public long GetRequestTypeId(string requestTypeName)
        {            
            IQueryable<CH_RequestType_Mst> repCH_RequestType_Dtl = ObjectFactory.GetInstance<IRepository<CH_RequestType_Mst>>().GetQuery();
            /// Step 1 Join CH Requests And CH
            CardHolderManager chm = new CardHolderManager();
            var requestTypeId = (from a in repCH_RequestType_Dtl
                             where a.RequestType_nm == requestTypeName
                                 select a).FirstOrDefault();
            return requestTypeId.RequestType_Id;                 

        }
        #endregion

        //CH_RequestType_MstDTO obRqst = new CH_RequestType_MstDTO();

        //return (from r in repCH_Request_Dtl.GetAll()
        //        select new CH_Request_DtlDTO
        //        {
        //            Request_Dtl_Id = r.Request_Dtl_Id,
        //            RequestReason_Id = r.RequestReason_Id,
        //            RequestType_Id = r.RequestType_Id,
        //            RequestType = r.CH_RequestType_Mst.RequestType_nm,
        //            Remarks = r.Remarks,
        //            Request_Dt = r.Request_Dt,
        //            Request_Status = r.Request_Status,
        //            Created_by = r.Created_by,
        //            Created_dt = r.Created_dt,
        //            Updated_by = r.Updated_by,
        //            Updated_dt = r.Updated_dt,
        //            IP_Address = r.IP_Address
        //        }).ToList();

        //Union(from s in repCH_Complaint_Dtl.GetAll()
        //         select new CH_Complaint_DtlDTO
        //         {
        //             Complaint_Dtl_Id = s.Complaint_Dtl_Id,
        //             Complaint_Status = s.Complaint_Status,
        //             Complaint_Dt = s.Complaint_Dt,
        //             ComplaintType_Id = s.ComplaintType_Id,
        //             ComplaintType = s.CH_ComplaintType_Mst.ComplaintType_nm,
        //             Created_by = s.Created_by,
        //             Created_dt = s.Created_dt,
        //             Updated_by = s.Updated_by,
        //             Updated_dt = s.Updated_dt,
        //             IP_Address = s.IP_Address
        //         }).ToList();
        //   }

    }
}
