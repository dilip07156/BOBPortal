
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Security;
using CardHolder.DAL;
using CardHolder.DAL.Interface;
using CardHolder.DTO;
using CardHolder.Utility;
using StructureMap;


namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class CardHolderManager
    {
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

        //public IRepository<CH_Sessions_Dtl> repCHSessionDtl
        //{
        //    get
        //    {
        //        return ObjectFactory.GetInstance<IRepository<CH_Sessions_Dtl>>();
        //    }
        //}
        /// <summary>
        /// Updates the card holder.
        /// </summary>
        /// <param name="userDTO">The user DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UpdateCardHolder(CardHolder_MstDTO userDTO)
        {
            try
            {
                //commented by Avani on 21-08-2019
                //IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                //CardHolder_Mst user = repCardHolder_Mst.SingleOrDefault(u => u.CardHolder_Id == userDTO.CardHolder_Id);               
                //user.User_pwd = userDTO.User_pwd;
                //user.Local_IpAddress = Convert.ToString(localAddress);
                //user.Updated_by = CardHolderManager.GetLoggedInUser().CardHolder_Id;
                //user.Updated_dt = DateTime.Now;
                //GeneralManager.Commit();
                //return "0";
                /***/
                // Added by Avani on 21-08-2019
                BOBCardEntities _db = new BOBCardEntities();
                IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                _db.UpdateCardHolderPasswordDetail(userDTO.CardHolder_Id, userDTO.User_pwd, localAddress.ToString(), CardHolderManager.GetLoggedInUser().CardHolder_Id, DateTime.Now);
                return "0";
            }
            catch (Exception exp)
            {
                return Constants.GeneralErrorMessage;
            }
        }

        /// <summary>
        /// Updates the card holder.
        /// </summary>
        /// <param name="userDTO">The user DTO.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UpdateCardHolderLastLoginDetails(CardHolder_MstDTO userDTO)
        {
            try
            {
                
                IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                //start commented by abhijeet on 20/08/2019
                /*
                CardHolder_Mst user = repCardHolder_Mst.SingleOrDefault(u => u.CardHolder_Id == userDTO.CardHolder_Id);
                user.CurrentLoginDate = DateTime.Now;
                var UserInfo = GetUserInfoById(userDTO.CardHolder_Id);
                user.LastLoginDate = UserInfo.CurrentLoginDate;
                user.Local_IpAddress = Convert.ToString(localAddress);
                user.IsLoggedInCurrently = true;
                GeneralManager.Commit();
                */
                //End start commented by abhijeet on 20/08/2019
                //start Added by abhijeet on 20/08/2019
                BOBCardEntities _db = new BOBCardEntities();
                _db.getCardHolderMst(userDTO.User_nm, localAddress.ToString(), localAddress.ToString());
                //End Added by abhijeet on 20/08/2019
                return "0";
            }
            catch (Exception exp)
            {
                return Constants.GeneralErrorMessage;
            }
        }

        /// <summary>
        /// get total count of pending request based on cardholderid and requesttypeid
        /// </summary>
        /// <param name="CardHolder_Id"></param>
        /// <param name="RequestTypeId"></param>
        /// <param name="DEFAULT_STATUS"></param>
        /// <returns></returns>
        /// This code is added by Avani to improve performance 0n 21-08-2019
        public int UpdateCardHolderDetailByID(CardHolder_MstDTO userDTO)
        {
            BOBCardEntities _db = new BOBCardEntities();           
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            _db.UpdateCardHolderDetailByID(userDTO.CardHolder_Id, localAddress.ToString(), true, DateTime.Now);            
            return 0;
        }

        ///// <summary>
        ///// Updates the card holder.
        ///// </summary>
        ///// <param name="userDTO">The user DTO.</param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public string UpdateCardHolderDetails(CardHolder_MstDTO userDTO)
        //{
        //    try
        //    {
        //        IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        //        CardHolder_Mst user = repCardHolder_Mst.SingleOrDefault(u => u.CardHolder_Id == userDTO.CardHolder_Id);
        //        user.User_pwd = userDTO.User_pwd;
        //        user.Local_IpAddress = Convert.ToString(localAddress);              
        //        GeneralManager.Commit();
        //        return "0";
        //    }
        //    catch (Exception exp)
        //    {
        //        return Constants.GeneralErrorMessage;
        //    }
        //}
        //public string UpdateCardHolderPassword(CardHolder_MstDTO userDTO)
        //{
        //    try
        //    {
        //        var user = repCardHolder_Mst.SingleOrDefault(u => u.CardHolder_Id == userDTO.CardHolder_Id);
        //        if (user != null)
        //        {
        //            user.MdfHashingPwd = userDTO.MdfHashingPwd;
        //            GeneralManager.Commit();
        //        }
        //        return "0";
        //    }
        //    catch (Exception exp)
        //    {

        //        return exp.Message;
        //    }
        //}

        /// <summary>
        /// Updates the card holder access.
        /// </summary>
        /// <param name="objCardHolder_Mst">The obj card holder_ MST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        //public int UpdateCardHolderAccess(long CardHolder_Id, bool IsLoggedInCurrently)
        //{
        //    try
        //    {
        //        CardHolder_Mst user = repCardHolder_Mst.SingleOrDefault(u => u.CardHolder_Id == CardHolder_Id);
        //        user.IsLoggedInCurrently = IsLoggedInCurrently;
        //        GeneralManager.Commit();
        //        return 1;
        //    }
        //    catch (Exception exp)
        //    {
        //        return 0;
        //    }
        //}

        public long SaveCardHolder(CardHolder_MstDTO objCardHolder_Mst)
        {
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            CardHolder_Mst obj = repCardHolder_Mst.SingleOrDefault(c => c.CardHolder_Id == objCardHolder_Mst.CardHolder_Id);

            if (obj == null || obj.CardHolder_Id != objCardHolder_Mst.CardHolder_Id)
                obj = new CardHolder_Mst();

            //obj.Dept_nm = objDept_MstDTO.Dept_nm;
            //obj.Dept_cd = objDept_MstDTO.Dept_cd;

            //obj.Oracle_Customer_Id = objCardHolder_Mst.Oracle_Customer_Id;
            obj.User_nm = objCardHolder_Mst.User_nm;
            obj.User_pwd = objCardHolder_Mst.User_pwd;
            obj.UID = objCardHolder_Mst.UID;

            if (objCardHolder_Mst.credit_card_number != null)
                obj.credit_card_number = objCardHolder_Mst.credit_card_number;

            if (objCardHolder_Mst.creditcard_acc_number != null)
                obj.creditcard_acc_number = objCardHolder_Mst.creditcard_acc_number;
            if (objCardHolder_Mst.Profile_Photo != null)
                obj.Profile_Photo = objCardHolder_Mst.Profile_Photo;
            if (objCardHolder_Mst.AddOn1_Photo != null)
                obj.AddOn1_Photo = objCardHolder_Mst.AddOn1_Photo;
            if (objCardHolder_Mst.AddOn2_Photo != null)
                obj.AddOn2_Photo = objCardHolder_Mst.AddOn2_Photo;
            if (objCardHolder_Mst.AddOn3_Photo != null)
                obj.AddOn3_Photo = objCardHolder_Mst.AddOn3_Photo;
            obj.Personal_Msg = objCardHolder_Mst.Personal_Msg;
            obj.IP_Address = objCardHolder_Mst.IP_Address;
            obj.IsActive = objCardHolder_Mst.IsActive == null ? true : objCardHolder_Mst.IsActive;
            obj.IsPermanentDisable = objCardHolder_Mst.IsPermanentDisable == null ? false : objCardHolder_Mst.IsPermanentDisable;

            if (localAddress != null)
                obj.Local_IpAddress = Convert.ToString(localAddress);


            if (obj.CardHolder_Id == 0)
            {
                obj.Created_by = objCardHolder_Mst.Created_by;
                obj.Created_dt = objCardHolder_Mst.Created_dt;
            }

            obj.Updated_by = objCardHolder_Mst.Updated_by;
            obj.Updated_dt = objCardHolder_Mst.Updated_dt;

            if (obj.CardHolder_Id == 0)
            {
                repCardHolder_Mst.Add(obj);
            }


            GeneralManager.Commit();

            return objCardHolder_Mst.CardHolder_Id;
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="PublicIP">The public IP.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        //commented by Abhijeet on 24/01/2019 for performance improvement
        
        public CardHolder_MstDTO FindUser(string Username, string PublicIP = "")
        {
            var Objusers = new CardHolder_MstDTO();
            Objusers = FindActiveUser(Username, PublicIP);
            return Objusers;
            /*
            // var obj = repCardHolder_Mst.SingleOrDefault(c => c.User_nm == Username && c.IsActive == true && c.IsPermanentDisable == false);

            var users = repCardHolder_Mst.Find(u => u.User_nm == Username).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                User_pwd = u.User_pwd,
                Created_dt = u.Created_dt,
                Updated_dt = u.Updated_dt,
                IP_Address = u.IP_Address,
                Personal_Msg = u.Personal_Msg,
                IsActive = u.IsActive,
                IsPermanentDisable = u.IsPermanentDisable,
                //IsLoggedInCurrently = u.IsLoggedInCurrently,
                Oracle_Customer_Id = u.Oracle_Customer_Id,
                credit_card_number = u.credit_card_number,
                creditcard_acc_number = u.creditcard_acc_number,
                InvalidLastLoginDt = u.InvalidLastLoginDt,
                CurrentLoginDate = u.CurrentLoginDate,
                LastLoginDate = u.LastLoginDate,
                LocalIP_Address = u.Local_IpAddress
            }).ToList();


            var Objusers = new CardHolder_MstDTO();
            if (users.Count < 2)
                Objusers = users.SingleOrDefault();
            else
                Objusers = users.First();

            if (Objusers != null)
            {
                var obj = repCardHolder_Mst.SingleOrDefault(c => c.User_nm == Username && c.IsActive == true && c.IsPermanentDisable == false);
                if (obj != null)
                {

                    IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

                    if (localAddress != null)
                        obj.Local_IpAddress = Convert.ToString(localAddress);


                    if (PublicIP != "")
                        Objusers.IP_Address = PublicIP;
                    GeneralManager.Commit();
                }
            }
         
            return Objusers;
             */ 
        }
        
       


        public CardHolder_MstDTO FindActiveUser(string Username, string PublicIP = "")
        {
            // var obj = repCardHolder_Mst.SingleOrDefault(c => c.User_nm == Username && c.IsActive == true && c.IsPermanentDisable == false);
            BOBCardEntities _db = new BOBCardEntities();

            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            var users = _db.getCardHolderMst(Username, PublicIP,localAddress.ToString()).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                User_pwd = u.User_pwd,
                Created_dt = u.Created_dt,
                Updated_dt = u.Updated_dt,
                IP_Address = u.IP_Address,
                Personal_Msg = u.Personal_Msg,
                IsActive = u.IsActive,
                IsPermanentDisable = u.IsPermanentDisable,
                //IsLoggedInCurrently = u.IsLoggedInCurrently,
                Oracle_Customer_Id = u.Oracle_Customer_Id,
                credit_card_number = u.credit_card_number,
                creditcard_acc_number = u.creditcard_acc_number,
                InvalidLastLoginDt = u.InvalidLastLoginDt,
                CurrentLoginDate = u.CurrentLoginDate,
                LastLoginDate = u.LastLoginDate,
                LocalIP_Address = u.Local_IpAddress
            }).SingleOrDefault();

            //var users = repCardHolder_Mst.Find(u => u.User_nm == Username && u.IsActive == true && u.IsPermanentDisable == false).Select(u => new CardHolder_MstDTO()
            //{
            //    CardHolder_Id = u.CardHolder_Id,
            //    User_nm = u.User_nm,
            //    User_pwd = u.User_pwd,
            //    Created_dt = u.Created_dt,
            //    Updated_dt = u.Updated_dt,
            //    IP_Address = u.IP_Address,
            //    Personal_Msg = u.Personal_Msg,
            //    IsActive = u.IsActive,
            //    IsPermanentDisable = u.IsPermanentDisable,
            //    //IsLoggedInCurrently = u.IsLoggedInCurrently,
            //    Oracle_Customer_Id = u.Oracle_Customer_Id,
            //    credit_card_number = u.credit_card_number,
            //    creditcard_acc_number = u.creditcard_acc_number,
            //    InvalidLastLoginDt = u.InvalidLastLoginDt,
            //    CurrentLoginDate = u.CurrentLoginDate,
            //    LastLoginDate = u.LastLoginDate,
            //    LocalIP_Address = u.Local_IpAddress
            //}).ToList();


            var Objusers = new CardHolder_MstDTO();
            Objusers = users;
            ////if (users.Count < 2)
            ////    Objusers = users.SingleOrDefault();
            ////else
            /*
            if (users.Count() > 0)
                Objusers = users.SingleOrDefault();
            else
                Objusers = users.First();
            */
            //if (Objusers != null)
            //{
            //    var obj = repCardHolder_Mst.SingleOrDefault(c => c.User_nm == Username && c.IsActive == true && c.IsPermanentDisable == false);
            //    if (obj != null)
            //    {

                    

            //        if (localAddress != null)
            //            obj.Local_IpAddress = Convert.ToString(localAddress);


            //        if (PublicIP != "")
            //            Objusers.IP_Address = PublicIP;
            //        GeneralManager.Commit();
            //    }
            //}
            return Objusers;
        }

        /// <summary>
        /// Finds the user by cr number.
        /// </summary>
        /// <param name="CreditCardNum">The credit card num.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CardHolder_MstDTO FindUserByCrNumber(string CreditCardAccNum)
        {
            //var users = repCardHolder_Mst.Find(u => u.credit_card_number == CreditCardNum.Encrypt()).Select(u => new CardHolder_MstDTO()
            var users = repCardHolder_Mst.Find(u => u.creditcard_acc_number == CreditCardAccNum).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                User_pwd = u.User_pwd,
                Created_dt = u.Created_dt,
                Updated_dt = u.Updated_dt,
                IP_Address = u.IP_Address,
                Personal_Msg = u.Personal_Msg,
                IsActive = u.IsActive,
                IsPermanentDisable = u.IsPermanentDisable,
                //IsLoggedInCurrently = u.IsLoggedInCurrently,
                Oracle_Customer_Id = u.Oracle_Customer_Id,
                credit_card_number = u.credit_card_number,
                creditcard_acc_number = u.creditcard_acc_number,
            }).ToList();


            if (users.Count < 2)
            {
                return users.SingleOrDefault();
            }
            else
            {
                return users.First();
            }
        }


         public bool FindUserByAccountNumber(string Cr_Accnum)
        {
            //var users = repCardHolder_Mst.Find(u => u.credit_card_number == CreditCardNum.Encrypt()).Select(u => new CardHolder_MstDTO()
            try
            {
                //commented by abhijeet on 21/08/2019
                //var users = repCardHolder_Mst.Find(u => u.creditcard_acc_number == Cr_Accnum).FirstOrDefault();

                //if (users != null)
                //    return true;
                //else
                //    return false;
                BOBCardEntities _db = new BOBCardEntities();
                var users = _db.FindUserByAccountNumber(Cr_Accnum).ToList();
                int ISuserExist = Convert.ToInt32(users.Select(x => x).FirstOrDefault());
                if (ISuserExist != 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="PublicIP">The public IP.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CardHolder_MstDTO AuthenticateUser(string Username, string PublicIP = "")
        {
            //var users = repCardHolder_Mst.Find(u => u.User_nm == Username).Select(u => new CardHolder_MstDTO()
            //{
            //    CardHolder_Id = u.CardHolder_Id,
            //    User_nm = u.User_nm,
            //    User_pwd = u.User_pwd,
            //    Created_dt = u.Created_dt,
            //    Updated_dt = u.Updated_dt,
            //    IP_Address = u.IP_Address,
            //    Personal_Msg = u.Personal_Msg,
            //    IsActive = u.IsActive,
            //    IsPermanentDisable = u.IsPermanentDisable,
            //    //IsLoggedInCurrently = u.IsLoggedInCurrently,
            //    Oracle_Customer_Id = u.Oracle_Customer_Id,
            //    credit_card_number = u.credit_card_number,
            //    creditcard_acc_number = u.creditcard_acc_number,
            //    CurrentLoginDate = u.CurrentLoginDate,
            //    LastLoginDate = u.LastLoginDate,
            //    LocalIP_Address = u.Local_IpAddress
            //}).ToList();

            var Objusers = new CardHolder_MstDTO();
            //if (users.Count < 2)
            //    Objusers = users.SingleOrDefault();
            //else
            //    Objusers = users.First();

            if (true) //Objusers != null
            {
                //var obj = repCardHolder_Mst.SingleOrDefault(c => c.User_nm == Username && c.IsActive == true && c.IsPermanentDisable == false);
                CardHolderManager _objManager = new CardHolderManager();
                var obj = _objManager.FindActiveUser(Username);
                //var obj = repCardHolder_Mst.Find .SingleOrDefault(c => c.User_nm == Username && c.IsActive == true && c.IsPermanentDisable == false);
                if (obj != null)
                {
                    Objusers = obj;
                    //if (obj.CurrentLoginDate.HasValue)
                    //{
                    //    obj.LastLoginDate = obj.CurrentLoginDate;
                    //}
                    //obj.CurrentLoginDate = DateTime.Now;

                    //IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

                    //if (localAddress != null)
                    //    obj.LocalIP_Address = Convert.ToString(localAddress);


                    //if (PublicIP != "")
                    //    Objusers.IP_Address = PublicIP;
                    //GeneralManager.Commit();
                }
            }
            return Objusers ;
        }

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="CardHolder_Id">The card holder_ id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CardHolder_MstDTO getUserByID(long CardHolder_Id)
        {
            //start commented by abhijeet on 20/08/2019
            /*
            var users = repCardHolder_Mst.Find(u => u.CardHolder_Id == CardHolder_Id).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                User_pwd = u.User_pwd,
                UID = u.UID,
                Profile_Photo = u.Profile_Photo,
                AddOn1_Photo = u.AddOn1_Photo,
                AddOn2_Photo = u.AddOn2_Photo,
                AddOn3_Photo = u.AddOn3_Photo,
                IsActive = u.IsActive,
                IsPermanentDisable = u.IsPermanentDisable,
                //IsLoggedInCurrently = u.IsLoggedInCurrently,
                Created_dt = u.Created_dt,
                Updated_dt = u.Updated_dt,
                IP_Address = u.IP_Address,
                Personal_Msg = u.Personal_Msg,
                Oracle_Customer_Id = u.Oracle_Customer_Id,
                credit_card_number = u.credit_card_number,
                creditcard_acc_number = u.creditcard_acc_number,
                CurrentLoginDate = u.CurrentLoginDate,
                LastLoginDate = u.LastLoginDate
               
            }).ToList();
            */
            //end commented by abhijeet on 20/08/2019

            //start added by abhijeet on 20/08/2019
            BOBCardEntities _db = new BOBCardEntities();
            var obj = _db.GetUserInfoById(CardHolder_Id).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                User_pwd = u.User_pwd,
                Created_dt = u.Created_dt,
                Updated_dt = u.Updated_dt,
                IP_Address = u.IP_Address,
                Personal_Msg = u.Personal_Msg,
                IsActive = u.IsActive,
                IsPermanentDisable = u.IsPermanentDisable,
                //IsLoggedInCurrently = u.IsLoggedInCurrently,
                Oracle_Customer_Id = u.Oracle_Customer_Id,
                credit_card_number = u.credit_card_number,
                creditcard_acc_number = u.creditcard_acc_number,
                InvalidLastLoginDt = u.InvalidLastLoginDt,
                CurrentLoginDate = u.CurrentLoginDate,
                LastLoginDate = u.LastLoginDate,
                LocalIP_Address = u.Local_IpAddress
            });

            var CardHolder = new CardHolder_MstDTO();
            CardHolder = obj.First();

            /*
            CardHolder_MstDTO CardHolder = null;
            if (users.Count < 2)
            {
                CardHolder = users.SingleOrDefault();
            }
            else
            {
                CardHolder = users.First();
            }
            */
            //end added by abhijeet on 20/08/2019

            CardManager cm = new CardManager();
            if (CardHolder != null)
            {
                if (CardHolder.creditcard_acc_number != null)
                {
                    CH_CardDTO card = cm.GetCardByCreditCardNumber(new CH_CardDTO()
                    { Cr_Account_Nbr = CardHolder.creditcard_acc_number.Decrypt() });
                    if (card != null)
                    {
                        CardHolder.CH_Card = card;
                        CardHolder.FULL_NAME = card.FULL_NAME;
                    }
                        
                    
                }
            }

            return CardHolder;
        }


        //public List<CardHolder_MstDTO> GetListCardHolders()
        //{
        //    var lst = new List<CardHolder_MstDTO>();

        //    lst = (from u in repCardHolder_Mst.GetAll()
        //           where string.IsNullOrEmpty(u.MdfHashingPwd)
        //           select new CardHolder_MstDTO()
        //           {
        //               CardHolder_Id = u.CardHolder_Id,
        //               User_nm = u.User_nm,
        //                User_pwd = u.User_pwd.Decrypt(),
        //               //User_pwd = u.User_pwd,
        //               MdfHashingPwd = u.MdfHashingPwd
        //           }).OrderBy(t => t.User_nm).ToList();
        //    return lst;
        //}


        //public bool CheckCardHolderLoggedInCurrently(long CardHolderId)
        //{
        //    bool IsLoggedInCurrently = false;
        //    CardHolder_Mst obj = repCardHolder_Mst.SingleOrDefault(c => c.CardHolder_Id == CardHolderId);
        //    if (obj != null && obj.IsLoggedInCurrently != null)
        //    {
        //        IsLoggedInCurrently = Convert.ToBoolean(obj.IsLoggedInCurrently);
        //    }
        //    return IsLoggedInCurrently;
        //}

        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static CardHolder_MstDTO GetLoggedInUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string strUserName = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.Name;
                string strUserId = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
                string strKey = strUserName + strUserId;

                string strSessionToken = strKey + HttpContext.Current.Request.UserAgent.ToString() + HttpContext.Current.Request.UserHostAddress.ToString();

                if (HttpContext.Current.Session != null)
                {

                    if (HttpContext.Current.Session["UserAuthKey"] == null)
                    {
                        HttpContext.Current.Session["UserAuthKey"] = strSessionToken;
                    }
                    else
                    {
                        if (HttpContext.Current.Session["UserAuthKey"].ToString() != strSessionToken)
                        {
                            Functions.LogOutMe();
                        }
                    }
                }

                if (CacheHelperBySession<CardHolder_MstDTO>.GetCacheItem(strKey) == null)
                {
                    CacheHelperBySession<CardHolder_MstDTO>.AddCacheItem(strKey, (new CardHolderManager()).getUserByID(Convert.ToInt64(strUserId)), System.DateTime.Now.AddMinutes(10));
                }
                return CacheHelperBySession<CardHolder_MstDTO>.GetCacheItem(strKey);
            }
            else
            {
                return new CardHolder_MstDTO();
            }
        }

        /// <summary>
        /// Sets the card holder active.
        /// </summary>
        /// <param name="CardHolderId">The card holder id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SetCardHolderActive(long CardHolderId)
        {
            /* commented by abhijeet on 23/01/2019
            CardHolder_Mst objCardholder_Mst = repCardHolder_Mst.SingleOrDefault(c => c.CardHolder_Id == CardHolderId);

            if (objCardholder_Mst != null && objCardholder_Mst.CardHolder_Id == CardHolderId)
            {
                objCardholder_Mst.IsActive = true;
                GeneralManager.Commit();
                return true;
            }
            else
            {
                return false;
            }*/

            BOBCardEntities _db = new BOBCardEntities();
            var obj = _db.setCardHolderMst_Active(CardHolderId);
            return (bool)obj.ToList()[0];
        }


        public CardHolder_MstDTO GetUserInfoById(long CardHolder_Id)
        {
            /*
            var users = repCardHolder_Mst.Find(u => u.CardHolder_Id == CardHolder_Id).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                Personal_Msg = u.Personal_Msg,
            }).ToList();

            CardHolder_MstDTO CardHolder = null;
            if (users.Count < 2)
            {
                CardHolder = users.SingleOrDefault();
            }
            else
            {
                CardHolder = users.First();
            }
            */
            BOBCardEntities _db = new BOBCardEntities();
            var obj = _db.GetUserInfoById(CardHolder_Id).Select(u => new CardHolder_MstDTO()
            {
                CardHolder_Id = u.CardHolder_Id,
                User_nm = u.User_nm,
                User_pwd = u.User_pwd,
                Created_dt = u.Created_dt,
                Updated_dt = u.Updated_dt,
                IP_Address = u.IP_Address,
                Personal_Msg = u.Personal_Msg,
                IsActive = u.IsActive,
                IsPermanentDisable = u.IsPermanentDisable,
                //IsLoggedInCurrently = u.IsLoggedInCurrently,
                Oracle_Customer_Id = u.Oracle_Customer_Id,
                credit_card_number = u.credit_card_number,
                creditcard_acc_number = u.creditcard_acc_number,
                InvalidLastLoginDt = u.InvalidLastLoginDt,
                CurrentLoginDate = u.CurrentLoginDate,
                LastLoginDate = u.LastLoginDate,
                LocalIP_Address = u.Local_IpAddress
            });

            var Objusers = new CardHolder_MstDTO();
            Objusers = obj.First();
            return Objusers;

        }

        //public long UpdateUserWhileLogin(string strUserName, string strPassword, string IPadress)
        //{
        //    long returnUserId = 0;
        //    CardHolder_Mst obj = repCardHolder_Mst.SingleOrDefault(c => c.User_nm == strUserName && c.User_pwd == strPassword && c.IsActive == true && c.IsPermanentDisable == false);
        //    if (obj != null && obj.User_nm == strUserName && obj.User_pwd == strPassword)
        //    {
        //        returnUserId = obj.CardHolder_Id;
        //        if (obj.CurrentLoginDate.HasValue)
        //        {
        //            obj.LastLoginDate = obj.CurrentLoginDate;
        //        }

        //        obj.CurrentLoginDate = DateTime.Now;


        //        if (IPadress != "")
        //            obj.IP_Address = IPadress;
        //        GeneralManager.Commit();
        //    }
        //    return returnUserId;
        //}




        //#region Multiple sessions block

        ///// <summary>
        ///// Checks the card holder session exist by card holder id.
        ///// </summary>
        ///// <param name="CardHolder_Id">The card holder_ id.</param>
        ///// <returns></returns>
        //public CH_Session_DtlDTO CheckCardHolderSessionExistByCardHolderId(long CardHolder_Id)
        //{
        //    CH_Session_DtlDTO newobj = null;
        //    List<CH_Sessions_Dtl> lst = repCHSessionDtl.Find(c => c.CardHolder_Id == CardHolder_Id).OrderByDescending(x => x.Last_access_time).ToList();
        //    if (lst != null && lst.Count > 0)
        //    {
        //        CH_Sessions_Dtl obj = lst.First();
        //        if (obj != null)
        //        {
        //            newobj = new CH_Session_DtlDTO();
        //            newobj.Id = obj.Id;
        //            newobj.CardHolder_Id = obj.CardHolder_Id;
        //            newobj.Session_Id = obj.Session_id;
        //            newobj.Last_access_time = obj.Last_access_time;
        //            newobj.Created_by = obj.Created_by;
        //            newobj.Created_dt = obj.Created_dt;
        //            newobj.IP_Address = obj.IP_Address;
        //            newobj.Updated_by = obj.Updated_by;
        //        }
        //    }
        //    return newobj;
        //}


        ///// <summary>
        ///// Checks the card holder session exist.
        ///// </summary>
        ///// <param name="Merchant_Id">The merchant_ id.</param>
        ///// <param name="Session_Id">The session_ id.</param>
        ///// <returns></returns>
        //public CH_Session_DtlDTO CheckCardHolderSessionExist(long CardHolder_Id, string Session_Id)
        //{
        //    CH_Session_DtlDTO newobj = null;
        //    CH_Sessions_Dtl obj = repCHSessionDtl.SingleOrDefault(c => c.CardHolder_Id == CardHolder_Id && c.Session_id == Session_Id);
        //    if (obj != null)
        //    {
        //        newobj = new CH_Session_DtlDTO();
        //        newobj.Id = obj.Id;
        //        newobj.CardHolder_Id = obj.CardHolder_Id;
        //        newobj.Session_Id = obj.Session_id;
        //        newobj.Last_access_time = obj.Last_access_time;
        //        newobj.Created_by = obj.Created_by;
        //        newobj.Created_dt = obj.Created_dt;
        //        newobj.IP_Address = obj.IP_Address;
        //        newobj.Updated_by = obj.Updated_by;
        //    }
        //    return newobj;
        //}

        ///// <summary>
        ///// Saves the merchant session details.
        ///// </summary>
        ///// <param name="objCH_Session_DtlDTO">The obj ME r_ session_ DTL DTO.</param>
        ///// <returns></returns>
        //public long SaveCardHolderSessionDetails(CH_Session_DtlDTO objCH_Session_DtlDTO)
        //{
        //    CH_Sessions_Dtl objCHSessions = repCHSessionDtl.SingleOrDefault(c => c.CardHolder_Id == objCH_Session_DtlDTO.CardHolder_Id && c.Session_id == objCH_Session_DtlDTO.Session_Id);
        //    if (objCHSessions == null)
        //    {
        //        objCHSessions = new CH_Sessions_Dtl();
        //    }
        //    objCHSessions.CardHolder_Id = objCH_Session_DtlDTO.CardHolder_Id;
        //    objCHSessions.Session_id = objCH_Session_DtlDTO.Session_Id;
        //    objCHSessions.Last_access_time = objCH_Session_DtlDTO.Last_access_time;

        //    if (objCHSessions.Id == 0)
        //    {
        //        objCHSessions.Created_by = objCH_Session_DtlDTO.Created_by;
        //        objCHSessions.Created_dt = objCH_Session_DtlDTO.Created_dt;
        //    }
        //    else
        //    {
        //        objCHSessions.Updated_by = objCH_Session_DtlDTO.Updated_by;
        //        objCHSessions.Updated_dt = objCH_Session_DtlDTO.Updated_dt;
        //    }
        //    objCHSessions.IP_Address = objCH_Session_DtlDTO.IP_Address;
        //    if (objCHSessions.Id == 0)
        //    {
        //        repCHSessionDtl.Add(objCHSessions);
        //    }
        //    GeneralManager.Commit();
        //    return objCHSessions.Id;
        //}

        ///// <summary>
        ///// Deletes the session details.
        ///// </summary>
        ///// <param name="Merchant_Id">The merchant_ id.</param>
        ///// <returns></returns>
        //public bool DeleteSessionDetails(long CardHolder_Id)
        //{
        //    bool rvalue = false;
        //    try
        //    {
        //        repCHSessionDtl.Delete(c => c.CardHolder_Id == CardHolder_Id);
        //        GeneralManager.Commit();
        //        return true;
        //    }
        //    catch
        //    {
        //        rvalue = false;
        //    }
        //    return rvalue;
        //}

        //#endregion

    }
}
