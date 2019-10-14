using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using CardHolder.Utility.Payment;
using com.bob.utils.security;

using System.Web;
using System.Security.Cryptography;

namespace CardHolder.Utility.bobibanking
{
    public class BobibankingRequest
    {
        #region Variable

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, string> BasicParameters = new Dictionary<string, string>();

        private Dictionary<string, string> QSParameters = new Dictionary<string, string>();

        #endregion

        #region Properties
        //private string _BobibankingPaymeGetwayURL = " https://www.bobibanking.com/BankAwayRetail/sgonHttpHandler.aspx?Action.BOBCARDS.ShoppingMall.Login.Init1=Y"; //Production live URL
        private string _BobibankingPaymeGetwayURL = "https://14.140.233.72/BankAwayRetailRM/sgonHttpHandler.aspx?Action.BOBCARDS.ShoppingMall.Login.Init1=Y";

           
        public string BobibankingPaymeGetwayURL { get { return _BobibankingPaymeGetwayURL; } }

        private string _BankId = "012";
        public string BANK_ID { get { return _BankId; } }

        private string _bobKeyFilePath = "NA";
        public string bobKeyFilePath { get { return _bobKeyFilePath; } set { _bobKeyFilePath = value; } }

        private string _PID = "000000022498"; //"000029372568"
        //private string _PID = "  000000000732";
        public string PID { get { return _PID; } }

        private string _PRN = "NA";
        public string PRN { get { return _PRN; } set { _PRN = value; } }

        private string _AMT = "NA";
        public string AMT { get { return _AMT; } set { _AMT = value; } }

        private string _RU = "NA";
        public string RU { get { return _RU; } set { _RU = value; } }

        private string _ITC = "NA";
        public string ITC { get { return _ITC; } set { _ITC = value; } }

        private string _CHECKSUM = "NA";
        public string CHECKSUM { get { return _CHECKSUM; } set { _CHECKSUM = value; } }

        //new parameters will be added 

        private string _FORMSGROUP_ID__ = "AuthenticationFG&__START_TRAN_FLAG__=Y&FG_BUTTONS__=LOAD&ACTION.LOAD=Y";
        public string FORMSGROUP_ID__ { get { return _FORMSGROUP_ID__; } set { _FORMSGROUP_ID__ = value; } }

        
        private string _AuthenticationFGLOGIN_FLAG = "1";        
        public string AuthenticationFGLOGIN_FLAG { get { return _AuthenticationFGLOGIN_FLAG ; } set { _AuthenticationFGLOGIN_FLAG = value; } }


        private string _AuthenticationFGUSER_TYPE = "1";
        public string AuthenticationFGUSER_TYPE { get { return _AuthenticationFGUSER_TYPE; } set { _AuthenticationFGUSER_TYPE = value; } }

        private string _AuthenticationFGMENU_ID = "CIMSHP";
        public string AuthenticationFGMENU_ID { get { return _AuthenticationFGMENU_ID; } set { _AuthenticationFGMENU_ID = value; } }

        private string _AuthenticationFGCALL_MODE = "2";
        public string AuthenticationFGCALL_MODE { get { return _AuthenticationFGCALL_MODE; } set { _AuthenticationFGCALL_MODE = value; } }
        

        private string _CATEGORY_ID = "TPN"; //need information about this 
        public string CATEGORY_ID { get { return _CATEGORY_ID; } set { _CATEGORY_ID = value; } }


        private string _ShoppingMallTranFGTRAN_CRN = "INR"; //need information about this 
        public string ShoppingMallTranFGTRAN_CRN { get { return _ShoppingMallTranFGTRAN_CRN; } set { _ShoppingMallTranFGTRAN_CRN = value; } }

        private string _ShoppingMallTranFGTXN_AMT = "NA"; //need information about this 
        public string ShoppingMallTranFGTXN_AMT { get { return _ShoppingMallTranFGTXN_AMT; } set { _ShoppingMallTranFGTXN_AMT = value; } }

        private string _ShoppingMallTranFGPID = "000000022498";//"000000000732"; //need information about this "000029372568"
        public string ShoppingMallTranFGPID { get { return _ShoppingMallTranFGPID; } set { _ShoppingMallTranFGPID = value; } }

        private string _ShoppingMallTranFGPRN = DateTime.Now.ToString("ddMMyyyyHHmmss"); //need information about this 
        public string ShoppingMallTranFGPRN { get { return _ShoppingMallTranFGPRN; } set { _ShoppingMallTranFGPRN = value; } }

        private string _ShoppingMallTranFGITC = "BOBCARD"; //need information about this 
        public string ShoppingMallTranFGITC { get { return _ShoppingMallTranFGITC; } set { _ShoppingMallTranFGITC = value; } }

        private string _ShoppingMallTranFGACNT_NUM = "NO_ACCOUNT"; //need information about this 
        public string ShoppingMallTranFGACNT_NUM { get { return _ShoppingMallTranFGACNT_NUM; } set { _ShoppingMallTranFGACNT_NUM = value; } }

        private string _ShoppingMallTranFGNAME = "CMNE"; //need information about this 
        public string ShoppingMallTranFGNAME { get { return _ShoppingMallTranFGNAME; } set { _ShoppingMallTranFGNAME = value; } }

        private string _ShoppingMallTranFGSHP_USER_TYPE = "3"; //need information about this //1 : Retail , 2: Corporate , 3: Any 
        public string ShoppingMallTranFGSHP_USER_TYPE { get { return _ShoppingMallTranFGSHP_USER_TYPE; } set { _ShoppingMallTranFGSHP_USER_TYPE = value; } }

        private string _ShoppingMallTranFGCHECKSUM = "SHA-512"; //need information about this //1 : Retail , 2: Corporate , 3: Any 
        public string ShoppingMallTranFGCHECKSUM { get { return _ShoppingMallTranFGCHECKSUM; } set { _ShoppingMallTranFGCHECKSUM = value; } }

        
        private string _QS = string.Empty ; //need information about this 
        public string QS { get { return _QS; } set { _QS = value; } }

        #endregion

        /// <summary>
        /// Create Payment Request
        /// </summary>
        /// <returns></returns>
        public string GetBobibankingPaymentRequest()
        {

            if(QSParameters.Keys.Count == 0)
            {
                QSParameters.Add("ShoppingMallTranFG.TRAN_CRN", ShoppingMallTranFGTRAN_CRN);
                QSParameters.Add("ShoppingMallTranFG.TXN_AMT", ShoppingMallTranFGTXN_AMT);
                QSParameters.Add("ShoppingMallTranFG.PID", ShoppingMallTranFGPID);
                QSParameters.Add("ShoppingMallTranFG.PRN", ShoppingMallTranFGPRN);
                QSParameters.Add("ShoppingMallTranFG.ITC", ShoppingMallTranFGITC);
                QSParameters.Add("ShoppingMallTranFG.ACC_NUM", ShoppingMallTranFGACNT_NUM);
                QSParameters.Add("ShoppingMallTranFG.SHOPPING_MALL_NAME_SHP", ShoppingMallTranFGNAME);
                //QSParameters.Add("ShoppingMallTranFG.SHP_USER_TYPE", ShoppingMallTranFGSHP_USER_TYPE);
                
            }

            ///STEP-1 Create Request String for QS
            string requestQS = "ShoppingMallTranFG.TRAN_CRN|ShoppingMallTranFG.TXN_AMT|ShoppingMallTranFG.PID|ShoppingMallTranFG.PRN|ShoppingMallTranFG.ITC|ShoppingMallTranFG.ACC_NUM|ShoppingMallTranFG.SHOPPING_MALL_NAME_SHP";

            string[] keysQS = requestQS.Split('|');
            foreach (var item in keysQS)
            {
                if (item.Trim() != "NA")
                {
                    requestQS = requestQS.Replace(item, item + "~" + QSParameters[item]);
                }
            }

            //_QS = "&"  + requestQS + "|" +  "ShoppingMallTranFG.CHECKSUM " + "= " + BobibankingCalculateCheckSum.GetCheckSum(requestQS);

            string strQS = string.Empty;
            BOBSymmetricCipherHelper sch = new BOBSymmetricCipherHelper();
            string hash_data = "|ShoppingMallTranFG.CHECKSUM~" + sch.getSHA512Hash(requestQS);            
            //strQS = requestQS + " | " + "ShoppingMallTranFG.CHECKSUM " + "~" + sch.getSHA512Hash(requestQS);
            strQS = requestQS + "|ShoppingMallTranFG.SHP_USER_TYPE~" + ShoppingMallTranFGSHP_USER_TYPE + hash_data;
            //string str = HttpUtility.UrlEncode(sch.encode(Encrypt(Encoding.UTF8.GetBytes(txt_hash.Text), GetRijndaelManaged(txt_key.Text))));

            //string requestEncData = sch.encrypt(strQS, "29304E875832789229304E8758327892"); 29304E875832789229304E8758327892
             QS = strQS;

            if (BasicParameters.Keys.Count == 0)
            {
                BasicParameters.Add("BobibankingPaymeGetwayURL", BobibankingPaymeGetwayURL);
                //Old code

                //BasicParameters.Add("BankId", BANK_ID);
                //BasicParameters.Add("PID", PID);
                //BasicParameters.Add("PRN", PRN);
                //BasicParameters.Add("AMT", AMT);
                //BasicParameters.Add("RU", RU);
                //BasicParameters.Add("ITC", ITC);
                //old code end

                BasicParameters.Add("FORMSGROUP_ID__", FORMSGROUP_ID__);
                BasicParameters.Add("AuthenticationFG.LOGIN_FLAG", AuthenticationFGLOGIN_FLAG);
                BasicParameters.Add("BANK_ID", BANK_ID);
                BasicParameters.Add("AuthenticationFG.USER_TYPE", AuthenticationFGUSER_TYPE);
                BasicParameters.Add("AuthenticationFG.MENU_ID", AuthenticationFGMENU_ID);
                BasicParameters.Add("AuthenticationFG.CALL_MODE", AuthenticationFGCALL_MODE);
                BasicParameters.Add("RU", RU);
                BasicParameters.Add("CATEGORY_ID", CATEGORY_ID);
                //BasicParameters.Add("QS", QS);

            }

            //encrypt string 

           
            ///STEP-1 Create Request String
            //string request = "BankId|PID|PRN|AMT|RU|ITC";
            string request = "FORMSGROUP_ID__&AuthenticationFG.LOGIN_FLAG&BANK_ID&AuthenticationFG.USER_TYPE&AuthenticationFG.MENU_ID&AuthenticationFG.CALL_MODE&RU&CATEGORY_ID";


            //string re = "FORMSGROUP_ID__= AuthenticationFG & __START_TRAN_FLAG__ = Y & FG_BUTTONS__ = LOAD & ACTION.LOAD = Y & AuthenticationFG.LOGIN_FLAG = 1 & BANK_ID = 012 & AuthenticationFG.USER_TYPE = 1 & AuthenticationFG.MENU_ID = CIMSHP & AuthenticationFG.CALL_MODE = 2 & RU
            string[] keys = request.Split('&');
            foreach (var item in keys)
            {
                if (item.Trim() != "NA")
                {
                    request = request.Replace(item, item + "=" + BasicParameters[item]);
                }
            }
            logger.Info("Request Parameter String: " + request);

            string finalString = string.Empty;

            ///STEP-2 Calculate Checksum Using Key And Append
            //finalString = request + "&QS=" + QS;
            finalString = request + "&QS=" + HttpUtility.UrlEncode(sch.encode(Encrypt(Encoding.UTF8.GetBytes(strQS), GetRijndaelManaged("29304E875832789229304E8758327892"))));
            //finalString = request;
            logger.Info("Request With CheckSum: " + finalString);
            return finalString;
        }


        private byte[] Encrypt(byte[] encData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor().TransformFinalBlock(encData, 0, encData.Length);
        }

        private RijndaelManaged GetRijndaelManaged(String key)
        {
            byte[] keybytes = new byte[32];
            byte[] secretbytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(secretbytes, keybytes, Math.Min(keybytes.Length, secretbytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                // BlockSize = 128,
                Key = keybytes,
                // IV = keybytes
            };
        }
    }
}
