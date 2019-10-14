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
    public class ParameterManager
    {
        #region Repository Members
        /// <summary>
        /// Gets the rep parameter_ MST.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<Parameter_Mst> repParameter_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<Parameter_Mst>>();
            }
        }
        #endregion

        #region Selection Methods
        /// <summary>
        /// Gets the name of the parameter by.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Parameter_MstDTO GetParameterByName(string parameterName)
        {
            Parameter_MstDTO objParamenterMstDTO = new Parameter_MstDTO();
            Parameter_Mst objParameterMst = repParameter_Mst.SingleOrDefault(c => c.Parameter_Name == parameterName);
            if (objParameterMst != null && objParameterMst.Parameter_Id > 0)
            {
                objParamenterMstDTO.Parameter_Id = objParameterMst.Parameter_Id;
                objParamenterMstDTO.Parameter_Name = objParameterMst.Parameter_Name;
                objParamenterMstDTO.Parameter_ValueC = objParameterMst.Parameter_ValueC;
                objParamenterMstDTO.Parameter_ValueN = objParameterMst.Parameter_ValueN;
                objParamenterMstDTO.Parameter_ValueD = objParameterMst.Parameter_ValueD;
            }
            return objParamenterMstDTO;
        }

        #endregion
    }
}
