using System.Collections.Generic;
using System.Linq;
using CardHolder.DAL.Interface;
using CardHolder.DAL;
using StructureMap;
using CardHolder.DTO;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class DropdownHdrManager
    {
        /// <summary>
        /// Gets the drop down_ HDR_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<DropDown_Hdr> DropDown_Hdr_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DropDown_Hdr>>();
            }
        }

        /// <summary>
        /// Gets the drop down_ DTL_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<DropDown_Dtl> DropDown_Dtl_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DropDown_Dtl>>();
            }
        }

        #region DLL Description       

        /// <summary>
        /// Searches the DLL header.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<DropDown_HdrDTO> SearchDllHeader(string search)
        {

            var dtoDropDownHdrDto = DropDown_Hdr_Mst.Find(ddl => ddl.Description.ToLower().Trim().Contains(search.ToLower().Trim())).OrderBy(ddl => ddl.Created_dt).Select(hdr =>
                                                                                        new DropDown_HdrDTO
                                                                                        {
                                                                                            Description = hdr.Description,
                                                                                            Created_by = hdr.Created_by,
                                                                                            Created_dt = hdr.Created_dt,
                                                                                            DropDown_Hdr_Id = hdr.DropDown_Hdr_Id,
                                                                                            IP_Address = hdr.IP_Address,
                                                                                            Updated_by = hdr.Updated_by,
                                                                                            Updated_dt = hdr.Updated_dt
                                                                                        });
            return dtoDropDownHdrDto;



        }

        /// <summary>
        /// Searches the DLL detail by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DropDown_DtlDTO SearchDllDetailById(int id)
        {

            var dtoDropDownDltDto = DropDown_Dtl_Mst.Find(ddl => ddl.DropDown_Dtl_Id == id).Select(dlt =>
                                                                                        new DropDown_DtlDTO
                                                                                        {
                                                                                            DropDown_Dtl_Id = dlt.DropDown_Dtl_Id,
                                                                                            Description = dlt.Description,
                                                                                            Created_by = dlt.Created_by,
                                                                                            Created_dt = dlt.Created_dt,
                                                                                            DropDown_Hdr_Id = dlt.DropDown_Hdr_Id,
                                                                                            IP_Address = dlt.IP_Address,
                                                                                            Updated_by = dlt.Updated_by,
                                                                                            Updated_dt = dlt.Updated_dt
                                                                                        }).Single();
            return dtoDropDownDltDto;
        }

        /// <summary>
        /// Searches the DLL detail.
        /// </summary>
        /// <param name="headerId">The header_id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<DropDown_DtlDTO> SearchDllDetail(int headerId)
        {

            var dtoDropDownDltDto = DropDown_Dtl_Mst.Find(ddl => ddl.DropDown_Hdr_Id == headerId).OrderBy(ddl => ddl.Created_dt)
                .Select(dlt =>
                                                                                        new DropDown_DtlDTO
                                                                                        {
                                                                                            DropDown_Dtl_Id = dlt.DropDown_Dtl_Id,
                                                                                            Description = dlt.Description,
                                                                                            Created_by = dlt.Created_by,
                                                                                            Created_dt = dlt.Created_dt,
                                                                                            DropDown_Hdr_Id = dlt.DropDown_Hdr_Id,
                                                                                            IP_Address = dlt.IP_Address,
                                                                                            Updated_by = dlt.Updated_by,
                                                                                            Updated_dt = dlt.Updated_dt
                                                                                        });
            return dtoDropDownDltDto;



        }

        public string GetValueFromDLLDetailsById(int inDDLDetailId)
        {
            try
            {
                string strValue = string.Empty;
                var dtoDropDownDltDto = DropDown_Dtl_Mst.Find(ddl => ddl.DropDown_Dtl_Id == inDDLDetailId).FirstOrDefault();
                if (dtoDropDownDltDto != null)
                    strValue = dtoDropDownDltDto.Description;

                return strValue;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
      
        #endregion

        #region CommentPart

        //public string DeleteDLLHeader(int id)
        //{

        //    DropDown_Hdr_Mst.Delete(d => d.DropDown_Hdr_Id == id);
        //    GeneralManager.Commit();
        //    return "0";

        //}

        //public string SaveDLLHeader(DropDown_HdrDTO headerDTO)
        //{

        //    DropDown_Hdr obj = DropDown_Hdr_Mst.SingleOrDefault(c => c.DropDown_Hdr_Id == headerDTO.DropDown_Hdr_Id);

        //    if (obj == null || obj.DropDown_Hdr_Id != headerDTO.DropDown_Hdr_Id)
        //    {
        //        obj = new DropDown_Hdr();
        //    }

        //    obj.Description = headerDTO.Description;
        //    obj.Updated_dt = headerDTO.Updated_dt;
        //    obj.Updated_by = headerDTO.Updated_by;
        //    obj.IP_Address = headerDTO.IP_Address;

        //    if (obj.DropDown_Hdr_Id == 0)
        //    {
        //        obj.Created_dt = headerDTO.Created_dt;
        //        obj.Created_by = headerDTO.Created_by;
        //        DropDown_Hdr_Mst.Add(obj);
        //    }

        //    GeneralManager.Commit();

        //    return obj.DropDown_Hdr_Id.ToString();


        //}

        //public string DeleteDLLDetail(int id)
        //{

        //    DropDown_Dtl_Mst.Delete(d => d.DropDown_Dtl_Id == id);
        //    GeneralManager.Commit();
        //    return "0";

        //}

        //public IEnumerable<DropDown_DtlDTO> SearchDllDetail(int header_id, string description)
        //{

        //    IEnumerable<DropDown_DtlDTO> dto_DropDown_DltDTO = DropDown_Dtl_Mst.Find(ddl => ddl.DropDown_Hdr_Id == header_id && ddl.Description == description).OrderBy(ddl => ddl.Created_dt).Select(dlt =>
        //                                                                                new DropDown_DtlDTO()
        //                                                                                {
        //                                                                                    DropDown_Dtl_Id = dlt.DropDown_Dtl_Id,
        //                                                                                    Description = dlt.Description,
        //                                                                                    Created_by = dlt.Created_by,
        //                                                                                    Created_dt = dlt.Created_dt,
        //                                                                                    DropDown_Hdr_Id = dlt.DropDown_Hdr_Id,
        //                                                                                    IP_Address = dlt.IP_Address,
        //                                                                                    Updated_by = dlt.Updated_by,
        //                                                                                    Updated_dt = dlt.Updated_dt
        //                                                                                });
        //    return dto_DropDown_DltDTO;



        //}

        //public string SaveDLLDetail(DropDown_DtlDTO detailDTO)
        //{

        //    DropDown_Dtl obj = DropDown_Dtl_Mst.SingleOrDefault(c => c.DropDown_Dtl_Id == detailDTO.DropDown_Dtl_Id);

        //    if (obj == null || obj.DropDown_Dtl_Id != detailDTO.DropDown_Dtl_Id)
        //    {
        //        obj = new DropDown_Dtl();
        //    }

        //    obj.DropDown_Hdr_Id = detailDTO.DropDown_Hdr_Id;
        //    obj.Description = detailDTO.Description;
        //    obj.Updated_dt = detailDTO.Updated_dt;
        //    obj.Updated_by = detailDTO.Updated_by;
        //    obj.IP_Address = detailDTO.IP_Address;

        //    if (obj.DropDown_Dtl_Id == 0)
        //    {
        //        obj.Created_dt = detailDTO.Created_dt;
        //        obj.Created_by = detailDTO.Created_by;
        //        DropDown_Dtl_Mst.Add(obj);
        //    }

        //    GeneralManager.Commit();

        //    return obj.DropDown_Dtl_Id.ToString();


        //}
        #endregion

    }
}
