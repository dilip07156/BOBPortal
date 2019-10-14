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
    public class CardHolderReasonManager
    {
        /// <summary>
        /// Gets the C h_ request reason_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CH_RequestReason_Mst> CH_RequestReason_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_RequestReason_Mst>>();
            }
        }



        #region Reason

        /// <summary>
        /// Lists the reason by request id.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_RequestReason_MstDTO> ListReasonByRequestId(long requestId)
        {

            IRepository<CH_RequestType_Mst> cht = ObjectFactory.GetInstance<IRepository<CH_RequestType_Mst>>();
            List<CH_RequestReason_MstDTO> lst = new List<CH_RequestReason_MstDTO>();
            //var lst = (from a in CH_RequestReason_Mst.GetAll()
            //                                            join b in cht.GetAll()
            //                                            on a.RequestType_Id equals b.RequestType_Id
            //                                            where a.RequestType_Id == requestId
            //                                            orderby a.RequestReason_Id
            //                                            select new CH_RequestReason_MstDTO
            //                                                {
            //                                                    RequestReason_Id = a.RequestReason_Id,
            //                                                    RequestType = b.RequestType_nm,
            //                                                    Reason_nm = a.Reason_nm,
            //                                                    RequestType_Id = a.RequestType_Id,
            //                                                }).ToList();
            //return lst;


            lst = (from a in CH_RequestReason_Mst.GetAll()
                   join b in cht.GetAll()
                   on a.RequestType_Id equals b.RequestType_Id
                   where a.RequestType_Id == requestId
                   select new CH_RequestReason_MstDTO
                   {
                       RequestReason_Id = a.RequestReason_Id,
                       RequestType = b.RequestType_nm,
                       Reason_nm = a.Reason_nm,
                       RequestType_Id = a.RequestType_Id,
                   }).ToList();


            return lst;



        }

        #endregion

        #region comment
        //public string DeleteReason(int RequestReason_Id)
        //{
        //    CH_RequestReason_Mst.Delete(d => d.RequestReason_Id == RequestReason_Id);
        //    GeneralManager.Commit();
        //    return "0";
        //}

        //public IEnumerable<CH_RequestReason_MstDTO> ListReason()
        //{

        //    IRepository<CH_RequestType_Mst> CH_RequestType_Mst = ObjectFactory.GetInstance<IRepository<CH_RequestType_Mst>>();

        //    IEnumerable<CH_RequestReason_MstDTO> list = from a in CH_RequestReason_Mst.GetQuery()
        //                                                join b in CH_RequestType_Mst.GetQuery()
        //                                                on a.RequestType_Id equals b.RequestType_Id
        //                                                orderby a.RequestReason_Id
        //                                                select new CH_RequestReason_MstDTO()
        //                                                {
        //                                                    RequestReason_Id = a.RequestReason_Id,
        //                                                    RequestType = b.RequestType_nm,
        //                                                    Reason_nm = a.Reason_nm,
        //                                                    RequestType_Id = a.RequestType_Id,
        //                                                };
        //    return list;
        //}

        //public string SaveParameter(CH_RequestReason_MstDTO CH_RequestReason_MstDTO)
        //{

        //    CH_RequestReason_Mst obj = CH_RequestReason_Mst.SingleOrDefault(c => c.RequestReason_Id == CH_RequestReason_MstDTO.RequestReason_Id);

        //    if (obj == null || obj.RequestReason_Id != CH_RequestReason_MstDTO.RequestReason_Id)
        //    {
        //        obj = new CH_RequestReason_Mst();
        //    }

        //    obj.Reason_nm = CH_RequestReason_MstDTO.Reason_nm;
        //    obj.Updated_dt = CH_RequestReason_MstDTO.Updated_dt;
        //    obj.Updated_by = CH_RequestReason_MstDTO.Updated_by;
        //    obj.IP_Address = CH_RequestReason_MstDTO.IP_Address;

        //    obj.RequestType_Id = CH_RequestReason_MstDTO.RequestType_Id;

        //    if (obj.RequestReason_Id == 0)
        //    {
        //        obj.Created_dt = CH_RequestReason_MstDTO.Created_dt;
        //        obj.Created_by = CH_RequestReason_MstDTO.Created_by;
        //        CH_RequestReason_Mst.Add(obj);
        //    }

        //    GeneralManager.Commit();

        //    return obj.RequestReason_Id.ToString();


        //}

        #endregion

    }
}
