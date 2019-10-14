using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    public class CardHolderLoginInfoManager
    {

        /// <summary>
        /// Gets the rep card holder login_ info.
        /// </summary>
        /// <remarks></remarks>
        private IRepository<CardHolderLogin_Info> repCardHolderLogin_Info
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CardHolderLogin_Info>>();
            }
        }

        /// <summary>
        /// Gets the rep card holder_ MST.
        /// </summary>
        /// <remarks></remarks>
        public IRepository<CardHolder_Mst> repCardHolder_Mst
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CardHolder_Mst>>();
            }
        }


        /// <summary>
        /// Gets the card holder login info by ID.
        /// </summary>
        /// <param name="CardHolderID">The card holder ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CardHolderLogin_InfoDTO> getCardHolderLoginInfoByID(int CardHolderID)
        {
            //List<CardHolderLogin_InfoDTO> LoginInfo = repCardHolderLogin_Info.Find(u => u.CardHolder_Id == CardHolderID).Select(u => new CardHolderLogin_InfoDTO()
            //{
            //    CardHolder_Id = u.CardHolder_Id,
            //    CardHolderLog_InfoId = u.CardHolderLog_InfoId,
            //    Login_Attempt_FirstDt = u.Login_Attempt_FirstDt,
            //    Login_Attempt_SecondDt = u.Login_Attempt_SecondDt,
            //    Login_Attempt_ThirdDt = u.Login_Attempt_ThirdDt,
            //    Login_Attempts = u.Login_Attempts
            //}).ToList();

            BOBCardEntities db = new BOBCardEntities();
            List<CardHolderLogin_InfoDTO> LoginInfo = db.getCardHolderLogin_Info(CardHolderID).Select(u => new CardHolderLogin_InfoDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                CardHolderLog_InfoId = u.CardHolderLog_InfoId,
                Login_Attempt_FirstDt = u.Login_Attempt_FirstDt,
                Login_Attempt_SecondDt = u.Login_Attempt_SecondDt,
                Login_Attempt_ThirdDt = u.Login_Attempt_ThirdDt,
                Login_Attempts = u.Login_Attempts
            }).ToList();

            return LoginInfo;

        }


        /// <summary>
        /// Saves the card holder login info.
        /// </summary>
        /// <param name="objCardHolderLogin_InfoDTO">The obj card holder login_ info DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public long SaveCardHolderLoginInfo(CardHolderLogin_InfoDTO objCardHolderLogin_InfoDTO)
        {
            //commented by abhijeet 22/08/2019
            //CardHolderLogin_Info obj = repCardHolderLogin_Info.SingleOrDefault(c => c.CardHolderLog_InfoId == objCardHolderLogin_InfoDTO.CardHolderLog_InfoId);

            //if (obj == null || obj.CardHolderLog_InfoId != objCardHolderLogin_InfoDTO.CardHolderLog_InfoId)
            //{
            //    obj = new CardHolderLogin_Info();
            //}

            //obj.CardHolder_Id = objCardHolderLogin_InfoDTO.CardHolder_Id;
            //obj.Login_Attempts = objCardHolderLogin_InfoDTO.Login_Attempts;
            //obj.Login_Attempt_FirstDt = objCardHolderLogin_InfoDTO.Login_Attempt_FirstDt;
            //obj.Login_Attempt_SecondDt = objCardHolderLogin_InfoDTO.Login_Attempt_SecondDt;
            //obj.Login_Attempt_ThirdDt = objCardHolderLogin_InfoDTO.Login_Attempt_ThirdDt;
            //obj.IP_Address = objCardHolderLogin_InfoDTO.IP_Address;

            //if (obj.CardHolderLog_InfoId == 0)
            //{
            //    repCardHolderLogin_Info.Add(obj);
            //}


            //GeneralManager.Commit();

            //return objCardHolderLogin_InfoDTO.CardHolderLog_InfoId;

            BOBCardEntities _db = new BOBCardEntities();
            _db.save_CardHolderLogin_Info(objCardHolderLogin_InfoDTO.CardHolder_Id, objCardHolderLogin_InfoDTO.Login_Attempts,
                objCardHolderLogin_InfoDTO.Login_Attempt_FirstDt, objCardHolderLogin_InfoDTO.Login_Attempt_SecondDt,
                objCardHolderLogin_InfoDTO.Login_Attempt_ThirdDt, objCardHolderLogin_InfoDTO.IP_Address);

            return 0;


        }



        /// <summary>
        /// Updates the card holder login infofirst.
        /// </summary>
        /// <param name="objCardHolderLogin_InfoDTO">The obj card holder login_ info DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? UpdateCardHolderLoginInfofirst(CardHolderLogin_InfoDTO objCardHolderLogin_InfoDTO)
        {
            // Commented by avani to inprove perfromance on 21-08-2019
            //CardHolderLogin_Info obj = repCardHolderLogin_Info.SingleOrDefault(c => c.CardHolder_Id == objCardHolderLogin_InfoDTO.CardHolder_Id && c.Login_Attempt_FirstDt == objCardHolderLogin_InfoDTO.Login_Attempt_FirstDt);
            //if (obj != null)
            //{
            //    obj.Login_Attempts = objCardHolderLogin_InfoDTO.Login_Attempts;
            //    GeneralManager.Commit();
            //}
            //return objCardHolderLogin_InfoDTO.Login_Attempts;


            //added by Avani on 21-08-2019 to improve performance

            BOBCardEntities db = new BOBCardEntities();
            db.getCardHolderLogin_InfoFirstAttemptDt(objCardHolderLogin_InfoDTO.CardHolder_Id, objCardHolderLogin_InfoDTO.Login_Attempt_FirstDt, objCardHolderLogin_InfoDTO.Login_Attempts);
            return objCardHolderLogin_InfoDTO.Login_Attempts;
        }



        /// <summary>
        /// Updates the card holder login info second.
        /// </summary>
        /// <param name="objCardHolderLogin_InfoDTO">The obj card holder login_ info DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? UpdateCardHolderLoginInfoSecond(CardHolderLogin_InfoDTO objCardHolderLogin_InfoDTO)
        {
            // Commented by avani to inprove perfromance on 21-08-2019

            //CardHolderLogin_Info obj = repCardHolderLogin_Info.SingleOrDefault(c => c.CardHolder_Id == objCardHolderLogin_InfoDTO.CardHolder_Id && c.Login_Attempt_SecondDt == objCardHolderLogin_InfoDTO.Login_Attempt_SecondDt);
            //if (obj != null)
            //{
            //    obj.Login_Attempts = objCardHolderLogin_InfoDTO.Login_Attempts;
            //    GeneralManager.Commit();
            //}
            //return objCardHolderLogin_InfoDTO.Login_Attempts;

            //added by Avani on 21-08-2019 to improve performance

            BOBCardEntities db = new BOBCardEntities();
            db.getCardHolderLogin_InfoAttemptSecondDt(objCardHolderLogin_InfoDTO.CardHolder_Id, objCardHolderLogin_InfoDTO.Login_Attempt_SecondDt, objCardHolderLogin_InfoDTO.Login_Attempts);
            return objCardHolderLogin_InfoDTO.Login_Attempts;

        }



        /// <summary>
        /// Updates the card holder login info third.
        /// </summary>
        /// <param name="objCardHolderLogin_InfoDTO">The obj card holder login_ info DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? UpdateCardHolderLoginInfoThird(CardHolderLogin_InfoDTO objCardHolderLogin_InfoDTO)
        {
            // Commented by avani to inprove perfromance on 21-08-2019
            //CardHolderLogin_Info obj = repCardHolderLogin_Info.SingleOrDefault(c => c.CardHolder_Id == objCardHolderLogin_InfoDTO.CardHolder_Id && c.Login_Attempt_ThirdDt == objCardHolderLogin_InfoDTO.Login_Attempt_ThirdDt);
            //if (obj != null)
            //{
            //    obj.Login_Attempts = objCardHolderLogin_InfoDTO.Login_Attempts;
            //    GeneralManager.Commit();
            //}
            //return objCardHolderLogin_InfoDTO.Login_Attempts;
            //added by Avani on 21-08-2019 to improve performance
            BOBCardEntities db = new BOBCardEntities();
            var result = db.getCardHolderLogin_InfoThirdAttemptDt(objCardHolderLogin_InfoDTO.CardHolder_Id, objCardHolderLogin_InfoDTO.Login_Attempt_ThirdDt, objCardHolderLogin_InfoDTO.Login_Attempts);
            return objCardHolderLogin_InfoDTO.Login_Attempts;

        }


        /// <summary>
        /// Deletes the card holder login info.
        /// </summary>
        /// <param name="CardHolderId">The card holder id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool DeleteCardHolderLoginInfo(int CardHolderId)
        {
            bool rvalue = false;

            //repCardHolderLogin_Info.Delete(c => c.CardHolder_Id == CardHolderId);

            //GeneralManager.Commit();

            BOBCardEntities _db = new BOBCardEntities();
            var obj = _db.deleteCardHolderLogin_Info(CardHolderId);
            rvalue = true;
            return rvalue;
        }


        /// <summary>
        /// Sets the card holder in active.
        /// </summary>
        /// <param name="CardHolderId">The card holder id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SetCardHolderInActive(long CardHolderId)
        {
            CardHolder_Mst objCardholder_Mst = repCardHolder_Mst.SingleOrDefault(c => c.CardHolder_Id == CardHolderId);
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            if (objCardholder_Mst != null && objCardholder_Mst.CardHolder_Id == CardHolderId)
            {
                objCardholder_Mst.IsActive = false;
                objCardholder_Mst.InvalidLastLoginDt = System.DateTime.Now;
                objCardholder_Mst.IP_Address = Convert.ToString(localAddress);
                GeneralManager.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Sets the card holder parmenent disable.
        /// </summary>
        /// <param name="CardHolder_Id">The card holder_ id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SetCardHolderParmenentDisable(long CardHolder_Id)
        {
            CardHolder_Mst objCardholder_Mst = repCardHolder_Mst.SingleOrDefault(c => c.CardHolder_Id == CardHolder_Id);

            if (objCardholder_Mst != null && objCardholder_Mst.CardHolder_Id == CardHolder_Id)
            {
                objCardholder_Mst.IsPermanentDisable = true;
                GeneralManager.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
