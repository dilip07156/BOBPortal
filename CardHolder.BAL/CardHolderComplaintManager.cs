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
    public class CardHolderComplaintManager
    {

        #region Repository Members

        /// <summary>
        /// Gets the rep C h_ complaint type_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_ComplaintType_Mst> repCH_ComplaintType_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_ComplaintType_Mst>>();
            }
        }

        /// <summary>
        /// Gets the rep C h_ complaint_ DTL.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_Complaint_Dtl> repCH_Complaint_Dtl
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_Complaint_Dtl>>();
            }
        }

        #endregion Repository Members

        #region Select Methods

        /// <summary>
        /// This method is use to get all complaints type
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>

        public List<CH_ComplaintType_MstDTO> getCHComplaintType()
        {
            return (from r in repCH_ComplaintType_Mst.GetAll()
                    where r.IsActive == true
                    select new CH_ComplaintType_MstDTO
                    {
                        ComplaintType_Id = r.ComplaintType_Id,
                        ComplaintType_nm = r.ComplaintType_nm,
                    }).ToList();
        }

        #endregion Select Methods

        #region ComplaintDetail

        /// <summary>
        /// This method is used to save the complaint submitted by CardHolder
        /// </summary>
        /// <param name="objComplaint_DltDTO">The obj complaint_ DLT DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public long SaveComplaintDetail(CH_Complaint_DtlDTO objComplaint_DltDTO)
        {
            if (objComplaint_DltDTO.Complaint_Dtl_Id == 0)
            {
                BOBCardEntities _db = new BOBCardEntities();
                var complaintDtl = _db.saveComplaintDtl(
                    objComplaint_DltDTO.Complaint_Dtl_Id,
                    objComplaint_DltDTO.Complaint_Dt,
                    objComplaint_DltDTO.CardHolder_Id,
                    objComplaint_DltDTO.ComplaintType_Id,
                    objComplaint_DltDTO.Remarks,
                    objComplaint_DltDTO.Complaint_Status,
                    objComplaint_DltDTO.Created_by,
                    objComplaint_DltDTO.Created_dt,
                    objComplaint_DltDTO.Updated_by,
                    objComplaint_DltDTO.Updated_dt,
                    objComplaint_DltDTO.IP_Address,
                    null,
                    null,
                    objComplaint_DltDTO.Created_dt.ToString("yyyyMMddHHmmssffff"),
                    objComplaint_DltDTO.AdminRemarks,
                    null,
                    null,
                    null
                ).ToList();
                return (long)complaintDtl[0];
            }
            else
            {
                CH_Complaint_Dtl obj = repCH_Complaint_Dtl.SingleOrDefault(c => c.Complaint_Dtl_Id == objComplaint_DltDTO.Complaint_Dtl_Id);

                if (obj == null || obj.Complaint_Dtl_Id != objComplaint_DltDTO.Complaint_Dtl_Id)
                {
                    obj = new CH_Complaint_Dtl();
                }


                obj.Complaint_Dt = objComplaint_DltDTO.Complaint_Dt;
                obj.CardHolder_Id = objComplaint_DltDTO.CardHolder_Id;
                obj.ComplaintType_Id = objComplaint_DltDTO.ComplaintType_Id;
                obj.IP_Address = objComplaint_DltDTO.IP_Address;
                obj.Remarks = objComplaint_DltDTO.Remarks;
                obj.Complaint_Status = objComplaint_DltDTO.Complaint_Status;
                obj.Remarks = objComplaint_DltDTO.Remarks;
                if (obj.Complaint_Dtl_Id == 0)
                {
                    obj.Created_by = objComplaint_DltDTO.Created_by;
                    obj.Created_dt = objComplaint_DltDTO.Created_dt;
                    obj.UID = objComplaint_DltDTO.Created_dt.ToString("yyyyMMddHHmmssffff");
                }

                obj.Updated_by = objComplaint_DltDTO.Updated_by;
                obj.Updated_dt = objComplaint_DltDTO.Updated_dt;

                if (obj.Complaint_Dtl_Id == 0)
                {
                    repCH_Complaint_Dtl.Add(obj);
                }


                GeneralManager.Commit();

                return obj.Complaint_Dtl_Id;
            }
        }

        /// <summary>
        /// This Method use to get list and details of complaints submitted by users.
        /// </summary>
        /// <param name="cardholderId">Id of logged in user</param>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <returns>List of complaints</returns>
        /// <remarks></remarks>

        public List<CH_Complaint_DtlDTO> getComplaintsDetails(long cardholderId, int SkipCount, int PageSize, ref int RecordCount)
        {
            // RecodeCount = repCH_Complaint_Dtl.GetAll().Count();

            IRepository<CH_ComplaintType_Mst> cht = ObjectFactory.GetInstance<IRepository<CH_ComplaintType_Mst>>();
            List<CH_Complaint_DtlDTO> lst = new List<CH_Complaint_DtlDTO>();

            CardHolderManager chm = new CardHolderManager();
            lst = (from x in repCH_Complaint_Dtl.GetAll()
                   join y in chm.repCardHolder_Mst.GetQuery()
                      on x.CardHolder_Id equals y.CardHolder_Id
                   join a in cht.GetAll()
                        on x.ComplaintType_Id equals a.ComplaintType_Id
                   where cardholderId == y.CardHolder_Id
                   select new CH_Complaint_DtlDTO()
                   {
                       Complaint_Dtl_Id = x.Complaint_Dtl_Id,
                       Complaint_Dt = x.Complaint_Dt,
                       CardHolder_Id = x.CardHolder_Id,
                       ComplaintType_Id = x.ComplaintType_Id,
                       ComplaintType = a.ComplaintType_nm,
                       Remarks = x.Remarks,
                       Complaint_Status = x.Complaint_Status,
                       UID = x.UID,
                       AdminRemarks = x.AdminRemarks
                   }).OrderByDescending(t => t.Complaint_Dt).ToList();

            RecordCount = lst.Count();
            lst = lst.Skip(SkipCount).Take(PageSize).ToList();
            return lst;
        }

        /// <summary>
        /// This method is use to get unique id of complaint (used for sending emails)
        /// </summary>
        /// <param name="ComplaintDtlId">The complaint DTL id.</param>
        /// <returns>Unique Id of complaint</returns>
        /// <remarks></remarks>

        public CH_Complaint_DtlDTO getComplaintUID(long ComplaintDtlId)
        {
            //CardHolderManager chm = new CardHolderManager();
            /*
            return (from x in repCH_Complaint_Dtl.GetAll()
                    where ComplaintDtlId == x.Complaint_Dtl_Id
                    select new CH_Complaint_DtlDTO()
                    {
                        UID = x.UID,
                    }).FirstOrDefault();
            */
            
            BOBCardEntities _db = new BOBCardEntities();
            var complaintDtl = _db.getComplaint_UID(ComplaintDtlId).Select(u => new CH_Complaint_DtlDTO()
            {
                UID = u.UID
            });
            
            return complaintDtl.First();

        }


        #endregion

        #region CommentPart

        //public CH_ComplaintType_MstDTO getCHComplaintTypeById(long ComplaintType_Id)
        //{
        //    CH_ComplaintType_MstDTO objToReturn = new CH_ComplaintType_MstDTO();
        //    CH_ComplaintType_Mst obj = repCH_ComplaintType_Mst.SingleOrDefault(c => c.ComplaintType_Id == ComplaintType_Id);
        //    if (obj != null && obj.ComplaintType_Id == ComplaintType_Id)
        //    {
        //        objToReturn.ComplaintType_Id = obj.ComplaintType_Id;
        //        objToReturn.ComplaintType_nm = obj.ComplaintType_nm;
        //        objToReturn.ComplaintType_Desc = obj.ComplaintType_Desc;
        //        objToReturn.Department = obj.Dept_Mst.Dept_nm;
        //        objToReturn.Dept_Id = obj.Dept_Id;
        //        objToReturn.Created_by = obj.Created_by;
        //        objToReturn.Created_dt = obj.Created_dt;
        //        objToReturn.Updated_by = obj.Updated_by;
        //        objToReturn.Updated_dt = obj.Updated_dt;
        //        objToReturn.IP_Address = obj.IP_Address;
        //    }
        //    return objToReturn;
        //}

        //public bool CheckComplaintTypeExist(string ComplaintType_nm, long ComplaintType_Id = 0)
        //{
        //    if (ComplaintType_Id != 0)
        //    {
        //        return repCH_ComplaintType_Mst.Find(c => c.ComplaintType_nm == ComplaintType_nm && c.ComplaintType_Id != ComplaintType_Id).Count() > 0;
        //    }
        //    else
        //    {
        //        return repCH_ComplaintType_Mst.Find(c => c.ComplaintType_nm == ComplaintType_nm).Count() > 0;
        //    }
        //}

        //#region Data Modification Methods

        //public long SaveComplaintType(CH_ComplaintType_MstDTO objComplaintType_MstDTO)
        //{
        //    CH_ComplaintType_Mst obj = repCH_ComplaintType_Mst.SingleOrDefault(c => c.ComplaintType_Id == objComplaintType_MstDTO.ComplaintType_Id);

        //    if (obj == null || obj.ComplaintType_Id != objComplaintType_MstDTO.ComplaintType_Id)
        //    {
        //        obj = new CH_ComplaintType_Mst();
        //    }


        //    obj.ComplaintType_nm = objComplaintType_MstDTO.ComplaintType_nm;
        //    obj.ComplaintType_Desc = objComplaintType_MstDTO.ComplaintType_Desc;
        //    obj.Dept_Id = objComplaintType_MstDTO.Dept_Id;
        //    obj.IP_Address = objComplaintType_MstDTO.IP_Address;

        //    if (obj.ComplaintType_Id == 0)
        //    {
        //        obj.Created_by = objComplaintType_MstDTO.Created_by;
        //        obj.Created_dt = objComplaintType_MstDTO.Created_dt;
        //    }

        //    obj.Updated_by = objComplaintType_MstDTO.Updated_by;
        //    obj.Updated_dt = objComplaintType_MstDTO.Updated_dt;

        //    if (obj.ComplaintType_Id == 0)
        //    {
        //        repCH_ComplaintType_Mst.Add(obj);
        //    }


        //    GeneralManager.Commit();

        //    return objComplaintType_MstDTO.ComplaintType_Id;
        //}

        //public bool DeleteCHComplaintType(long ComplaintType_Id)
        //{
        //    bool rvalue = false;

        //    repCH_ComplaintType_Mst.Delete(c => c.ComplaintType_Id == ComplaintType_Id);

        //    GeneralManager.Commit();

        //    return rvalue;
        //}

        //#endregion Data Modification Methods


        /// <summary>
        /// Find Complaint By Primary Key Complaint_Dtl_Id
        /// </summary>
        /// <param name="Complaint_Dtl_Id"></param>
        /// <returns></returns>
        //public CH_Complaint_DtlDTO FindComplaintDetail(long Complaint_Dtl_Id)
        //{
        //    CH_Complaint_DtlDTO Compl_detail = repCH_Complaint_Dtl.Find(c => c.Complaint_Dtl_Id == Complaint_Dtl_Id).Select(
        //         x => new CH_Complaint_DtlDTO()
        //         {
        //             Complaint_Dtl_Id = x.Complaint_Dtl_Id,
        //             Complaint_Dt = x.Complaint_Dt,
        //             CardHolder_Id = x.CardHolder_Id,
        //             ComplaintType_Id = x.ComplaintType_Id,
        //             IP_Address = x.IP_Address,
        //             Remarks = x.Remarks,
        //             Complaint_Status = x.Complaint_Status,
        //             Created_by = x.Created_by,
        //             Created_dt = x.Created_dt,
        //             Updated_by = x.Updated_by,
        //             Updated_dt = x.Updated_dt,
        //             UID = x.UID,
        //         }
        //        ).Single();

        //    return Compl_detail;
        //}

        #endregion

        public int CheckComplaintPending(CH_Complaint_DtlDTO objCH_Complaint_DtlDTO)
        {
            int count = 0;

            IQueryable<CH_Complaint_Dtl> repCH_Complaint_Dtl = ObjectFactory.GetInstance<IRepository<CH_Complaint_Dtl>>().GetQuery();
            var Complaint_list = (from a in repCH_Complaint_Dtl
                                  where a.CardHolder_Id == objCH_Complaint_DtlDTO.CardHolder_Id
                                   && a.Complaint_Status == objCH_Complaint_DtlDTO.Complaint_Status
                                   && a.ComplaintType_Id == objCH_Complaint_DtlDTO.ComplaintType_Id
                                  select a).ToList();
            if (Complaint_list != null && Complaint_list.Count > 0)
            {
                count = Complaint_list.Count;
            }
            return count;
        }
    }
}
