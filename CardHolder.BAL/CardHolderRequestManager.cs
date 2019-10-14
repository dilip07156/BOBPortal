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
    public class CardHolderRequestManager
    {

        #region Repository Members

        /// <summary>
        /// Gets the rep C h_ request type_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_RequestType_Mst> repCH_RequestType_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_RequestType_Mst>>();
            }
        }

        /// <summary>
        /// Gets the rep C h_ request reason_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_RequestReason_Mst> repCH_RequestReason_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_RequestReason_Mst>>();
            }
        }

        /// <summary>
        /// Gets the rep bank_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<Bank_Mst> repBank_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Bank_Mst>>();
            }
        }



        #endregion Repository Members

        #region For CHRequest Type

        /// <summary>
        /// This method is used to get list of standard Request types which are not allowed to edit/delete
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_RequestType_MstDTO> getCHStandardRequestType()
        {
            return (from r in repCH_RequestType_Mst.GetAll()
                    where r.IsStandard == false && r.IsActive == true
                    select new CH_RequestType_MstDTO
                    {
                        RequestType_Id = r.RequestType_Id,
                        RequestType_nm = r.RequestType_nm,
                    }).ToList();
        }

        /// <summary>
        /// This method used to get all Request types of CardHolder
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_RequestType_MstDTO> getCHRequestTypes()
        {
            return (from r in repCH_RequestType_Mst.GetAll()
                    where r.IsStandard == true && r.IsActive == true
                    select new CH_RequestType_MstDTO
                    {
                        RequestType_Id = r.RequestType_Id,
                        RequestType_nm = r.RequestType_nm,
                    }).ToList();
        }



        #endregion For CHRequest Type

        #region For Request's Reason

        /// <summary>
        /// This method use to get reasons of particular selected request by cardholder
        /// </summary>
        /// <param name="RequestReason_Id">Unique Id of request. On the basis of this this method shows particular reason</param>
        /// <returns>CH_RequestReason_MstDTO</returns>
        /// <remarks></remarks>
        public CH_RequestReason_MstDTO getCHRequestReasonById(long RequestReason_Id)
        {
            CH_RequestReason_MstDTO objToReturn = new CH_RequestReason_MstDTO();
            CH_RequestReason_Mst obj = repCH_RequestReason_Mst.SingleOrDefault(c => c.RequestReason_Id == RequestReason_Id);
            string strReason = string.Empty;
            if (obj!= null && obj.Reason_nm != null)
            {
                strReason = obj.Reason_nm;
            }
            else { strReason = string.Empty; }
            
         
            if (obj != null && obj.RequestReason_Id == RequestReason_Id)
            {
                objToReturn.RequestType_Id = obj.RequestType_Id;
                objToReturn.Reason_nm = strReason;
                objToReturn.RequestReason_Id = obj.RequestReason_Id;
                objToReturn.RequestType = obj.CH_RequestType_Mst.RequestType_nm;
                objToReturn.Created_by = obj.Created_by;
                objToReturn.Created_dt = obj.Created_dt;
                objToReturn.Updated_by = obj.Updated_by;
                objToReturn.Updated_dt = obj.Updated_dt;
                objToReturn.IP_Address = obj.IP_Address;
            }
            return objToReturn;
        }

        #endregion

        #region Banklist

        /// <summary>
        /// Used to get list of all banks
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Bank_MstDTO> getBankList()
        {
            return (from r in repBank_Mst.GetAll()
                    select new Bank_MstDTO
                    {
                        Bank_Id = r.Bank_Id,
                        Bank_nm = r.Bank_nm
                    }).ToList();
        }

        #endregion

        #region CommentPart

        //public List<CH_RequestType_MstDTO> getCHRequestType()
        //{
        //    return (from r in repCH_RequestType_Mst.GetAll()
        //            select new CH_RequestType_MstDTO
        //            {
        //                RequestType_Id = r.RequestType_Id,
        //                RequestType_nm = r.RequestType_nm,
        //                RequestType_Desc = r.RequestType_Desc,
        //                Department = r.Dept_Mst.Dept_nm,
        //                Dept_Id = r.Dept_Id,
        //                IsStandard = r.IsStandard,
        //                Created_by = r.Created_by,
        //                Created_dt = r.Created_dt,
        //                Updated_by = r.Updated_by,
        //                Updated_dt = r.Updated_dt,
        //                IP_Address = r.IP_Address
        //            }).ToList();
        //}

        //public CH_RequestType_MstDTO getCHRequestTypeById(long RequestType_Id)
        //{
        //    CH_RequestType_MstDTO objToReturn = new CH_RequestType_MstDTO();
        //    CH_RequestType_Mst obj = repCH_RequestType_Mst.SingleOrDefault(c => c.RequestType_Id == RequestType_Id);
        //    if (obj != null && obj.RequestType_Id == RequestType_Id)
        //    {
        //        objToReturn.RequestType_Id = obj.RequestType_Id;
        //        objToReturn.RequestType_nm = obj.RequestType_nm;
        //        objToReturn.RequestType_Desc = obj.RequestType_Desc;
        //        objToReturn.Department = obj.Dept_Mst.Dept_nm;
        //        objToReturn.IsStandard = obj.IsStandard;
        //        objToReturn.Dept_Id = obj.Dept_Id;
        //        objToReturn.Created_by = obj.Created_by;
        //        objToReturn.Created_dt = obj.Created_dt;
        //        objToReturn.Updated_by = obj.Updated_by;
        //        objToReturn.Updated_dt = obj.Updated_dt;
        //        objToReturn.IP_Address = obj.IP_Address;
        //    }
        //    return objToReturn;
        //}

        //public bool CheckRequestTypeExist(string RequestType_nm, long RequestType_Id = 0)
        //{
        //    if (RequestType_Id != 0)
        //    {
        //        return repCH_RequestType_Mst.Find(c => c.RequestType_nm == RequestType_nm && c.RequestType_Id != RequestType_Id).Count() > 0;
        //    }
        //    else
        //    {
        //        return repCH_RequestType_Mst.Find(c => c.RequestType_nm == RequestType_nm).Count() > 0;
        //    }
        //}


        //#region Data Modification Methods

        //public long SaveRequestType(CH_RequestType_MstDTO objRequestType_MstDTO)
        //{
        //    CH_RequestType_Mst obj = repCH_RequestType_Mst.SingleOrDefault(c => c.RequestType_Id == objRequestType_MstDTO.RequestType_Id);

        //    if (obj == null || obj.RequestType_Id != objRequestType_MstDTO.RequestType_Id)
        //    {
        //        obj = new CH_RequestType_Mst();
        //    }

        //    obj.RequestType_nm = objRequestType_MstDTO.RequestType_nm;
        //    obj.RequestType_Desc = objRequestType_MstDTO.RequestType_Desc;
        //    obj.Dept_Id = objRequestType_MstDTO.Dept_Id;
        //    obj.IP_Address = objRequestType_MstDTO.IP_Address;
        //    obj.IsStandard = objRequestType_MstDTO.IsStandard == null ? false : objRequestType_MstDTO.IsStandard;
        //    if (obj.RequestType_Id == 0)
        //    {
        //        obj.Created_by = objRequestType_MstDTO.Created_by;
        //        obj.Created_dt = objRequestType_MstDTO.Created_dt;
        //    }

        //    obj.Updated_by = objRequestType_MstDTO.Updated_by;
        //    obj.Updated_dt = objRequestType_MstDTO.Updated_dt;

        //    if (obj.RequestType_Id == 0)
        //    {
        //        repCH_RequestType_Mst.Add(obj);
        //    }


        //    GeneralManager.Commit();

        //    return objRequestType_MstDTO.RequestType_Id;
        //}

        //public bool DeleteCHRequestType(long RequestType_Id)
        //{
        //    bool rvalue = false;

        //    repCH_RequestType_Mst.Delete(c => c.RequestType_Id == RequestType_Id);

        //    GeneralManager.Commit();

        //    return rvalue;
        //}

        //#endregion Data Modification Methods

        //public List<CH_RequestReason_MstDTO> getCHRequestReasonType()
        //{
        //    return (from r in repCH_RequestReason_Mst.GetAll()
        //            select new CH_RequestReason_MstDTO
        //            {
        //                RequestReason_Id = r.RequestReason_Id,
        //                Reason_nm = r.Reason_nm,
        //                RequestType_Id = r.RequestType_Id,
        //                RequestType = r.CH_RequestType_Mst.RequestType_nm,
        //                Created_by = r.Created_by,
        //                Created_dt = r.Created_dt,
        //                Updated_by = r.Updated_by,
        //                Updated_dt = r.Updated_dt,
        //                IP_Address = r.IP_Address
        //            }).ToList();
        //}

        //public bool CheckRequestReasonExist(string Reason_nm, long RequesttypeID = 0, long RequestReason_Id = 0)
        //{
        //    if (RequestReason_Id != 0)
        //    {
        //        return repCH_RequestReason_Mst.Find(c => c.Reason_nm == Reason_nm && c.RequestReason_Id != RequestReason_Id).Count() > 0;
        //    }
        //    else
        //    {
        //        return repCH_RequestReason_Mst.Find(c => c.Reason_nm == Reason_nm).Count() > 0;
        //    }
        //}

        //#region Data Modification Methods

        //public long SaveRequestReasonType(CH_RequestReason_MstDTO objRequestReason_MstDTO)
        //{
        //    CH_RequestReason_Mst obj = repCH_RequestReason_Mst.SingleOrDefault(c => c.RequestReason_Id == objRequestReason_MstDTO.RequestReason_Id);

        //    if (obj == null || obj.RequestReason_Id != objRequestReason_MstDTO.RequestReason_Id)
        //    {
        //        obj = new CH_RequestReason_Mst();
        //    }

        //    obj.RequestReason_Id = objRequestReason_MstDTO.RequestReason_Id;
        //    obj.Reason_nm = objRequestReason_MstDTO.Reason_nm;
        //    obj.RequestType_Id = objRequestReason_MstDTO.RequestType_Id;
        //    obj.IP_Address = objRequestReason_MstDTO.IP_Address;

        //    if (obj.RequestReason_Id == 0)
        //    {
        //        obj.Created_by = objRequestReason_MstDTO.Created_by;
        //        obj.Created_dt = objRequestReason_MstDTO.Created_dt;
        //    }

        //    obj.Updated_by = objRequestReason_MstDTO.Updated_by;
        //    obj.Updated_dt = objRequestReason_MstDTO.Updated_dt;

        //    if (obj.RequestReason_Id == 0)
        //    {
        //        repCH_RequestReason_Mst.Add(obj);
        //    }


        //    GeneralManager.Commit();

        //    return objRequestReason_MstDTO.RequestType_Id;
        //}

        //public bool DeleteRequestReason(long RequestReason_Id)
        //{
        //    bool rvalue = false;

        //    repCH_RequestReason_Mst.Delete(c => c.RequestReason_Id == RequestReason_Id);

        //    GeneralManager.Commit();

        //    return rvalue;
        //}

        //#endregion Data Modification Methods

        #endregion

    }
}
