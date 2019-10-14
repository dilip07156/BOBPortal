using System;
using CardHolder.DAL;
using CardHolder.DAL.Interface;
using CardHolder.DTO;
using StructureMap;
using System.Linq;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class CardHolderPaymentManager
    {
        /// <summary>
        /// Gets the rep card holder payment status.
        /// </summary>
        /// <remarks></remarks>
        public IRepository<CH_PaymentStatus_Dtl> repCardHolderPaymentStatus
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_PaymentStatus_Dtl>>();
            }
        }

        /// <summary>
        /// Gets the rep card holder bobibanking payment status.
        /// </summary>
        /// <remarks></remarks>
        public IRepository<CH_BobibankingPaymentStatus_Dtl> repCardHolderBobibankingPaymentStatus
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CH_BobibankingPaymentStatus_Dtl>>();
            }
        }

        #region BillDesk

        /// <summary>
        /// Saves the card holder payment status.
        /// </summary>
        /// <param name="objCardHolderPaymentStatus">The obj card holder payment status.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SaveCardHolderPaymentStatus(CH_Payment_Status_DtlDTO objCardHolderPaymentStatus)
        {
            if (objCardHolderPaymentStatus.PaymentStatus_Id == 0)
            {
                CH_PaymentStatus_Dtl obj = new CH_PaymentStatus_Dtl();
                if (objCardHolderPaymentStatus.Creditcard_acc_number != null)
                    obj.Creditcard_acc_number = objCardHolderPaymentStatus.Creditcard_acc_number;
                if (objCardHolderPaymentStatus.Credit_card_number != null)
                    obj.Credit_card_number = objCardHolderPaymentStatus.Credit_card_number;
                if (objCardHolderPaymentStatus.Transaction_number != null)
                    obj.Transaction_number = objCardHolderPaymentStatus.Transaction_number;
                if (objCardHolderPaymentStatus.TxnReferenceNo != null)
                    obj.TxnReferenceNo = objCardHolderPaymentStatus.TxnReferenceNo;
                else
                    obj.TxnReferenceNo = null;
                obj.BillDeskOnlineID = objCardHolderPaymentStatus.BillDeskOnlineID;
                obj.AmountDue = objCardHolderPaymentStatus.AmountDue;
                obj.PaymentStatus = objCardHolderPaymentStatus.PaymentStatus;

                obj.BankRefNo = objCardHolderPaymentStatus.BankRefNo;
                obj.BankId = objCardHolderPaymentStatus.BankId;
                obj.AuthStatus = objCardHolderPaymentStatus.AuthStatus;

                obj.IP_Address = objCardHolderPaymentStatus.IP_Address;
                obj.Created_by = objCardHolderPaymentStatus.Created_by;
                obj.Created_dt = objCardHolderPaymentStatus.Created_dt;
                repCardHolderPaymentStatus.Add(obj);
                GeneralManager.Commit();
                return obj.Transaction_number;
            }
            return "";
        }

        /// <summary>
        /// Updates the card holder payment status.
        /// </summary>
        /// <param name="transaction_number">The transaction_number.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="amountDue">The amount due.</param>
        /// <param name="TxnRefrenceNo">The TXN refrence no.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UpdateCardHolderPaymentStatus(string transaction_number, string BillDeskOnlineID, double amountDue, string TxnRefrenceNo,
                                                    string paymentStatus, string BankRefNo, string BankId, string AuthStatus) // Add BillDeskOnlineID instead of Accnum 20-jan-2015
        {
            //commented by abhijeet on 21/08/2019
            //string Transaction_number = "";
            //CH_PaymentStatus_Dtl obj = repCardHolderPaymentStatus.SingleOrDefault(c => c.BillDeskOnlineID == BillDeskOnlineID
            //                            && c.Transaction_number == transaction_number
            //                            && c.AmountDue == amountDue && c.PaymentStatus.Trim().ToLower() == "unsuccessful");
            //if (obj != null)
            //{
            //    obj.PaymentStatus = paymentStatus;
            //    obj.TxnReferenceNo = TxnRefrenceNo;
            //    obj.Updated_by = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            //    obj.Updated_dt = DateTime.Now;
            //    obj.BankRefNo = BankRefNo;
            //    obj.BankId = BankId;
            //    obj.AuthStatus = AuthStatus;
            //    GeneralManager.Commit();
            //    Transaction_number = obj.Transaction_number;
            //}
            //return Transaction_number;

            //Added by abhijeet on 21/08/2019
            BOBCardEntities _db = new BOBCardEntities();
            _db.UpdatePaymentStatus_Dtl(BillDeskOnlineID, transaction_number, amountDue, paymentStatus, TxnRefrenceNo,
                CardHolderManager.GetLoggedInUser().CardHolder_Id, DateTime.Now, BankRefNo, BankId, AuthStatus);
            return transaction_number;
        }

        #endregion

        #region Bobibanking

        /// <summary>
        /// Saves the card holder payment status bobibanking.
        /// </summary>
        /// <param name="objCardHolderBobibankingPaymentStatus">The obj card holder bobibanking payment status.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SaveCardHolderPaymentStatusBobibanking(CH_Bobibanking_Payment_Status_DtlDTO objCardHolderBobibankingPaymentStatus)
        {
            if (objCardHolderBobibankingPaymentStatus.BobiBanking_PaymentStatus_Id == 0)
            {
                CH_BobibankingPaymentStatus_Dtl obj = new CH_BobibankingPaymentStatus_Dtl();
                if (objCardHolderBobibankingPaymentStatus.Creditcard_acc_number != null)
                    obj.Creditcard_acc_number = objCardHolderBobibankingPaymentStatus.Creditcard_acc_number;

                if (objCardHolderBobibankingPaymentStatus.Credit_card_number != null)
                    obj.Credit_card_number = objCardHolderBobibankingPaymentStatus.Credit_card_number;

                if (objCardHolderBobibankingPaymentStatus.BankId != null)
                    obj.BankId = objCardHolderBobibankingPaymentStatus.BankId;

                if (objCardHolderBobibankingPaymentStatus.PID != null)
                    obj.PID = objCardHolderBobibankingPaymentStatus.PID;

                if (objCardHolderBobibankingPaymentStatus.PRN != null)
                    obj.PRN = objCardHolderBobibankingPaymentStatus.PRN;

                if (objCardHolderBobibankingPaymentStatus.BID != null)
                    obj.BID = objCardHolderBobibankingPaymentStatus.BID;

                if (objCardHolderBobibankingPaymentStatus.DebtAccountNo != null)
                    obj.DebtAccountNo = objCardHolderBobibankingPaymentStatus.DebtAccountNo;

                if (objCardHolderBobibankingPaymentStatus.ITC != null)
                    obj.ITC = objCardHolderBobibankingPaymentStatus.ITC;

                obj.AmountDue = Convert.ToDouble(objCardHolderBobibankingPaymentStatus.AmountDue);
                obj.PaymentStatus = objCardHolderBobibankingPaymentStatus.PaymentStatus;
                obj.IP_Address = objCardHolderBobibankingPaymentStatus.IP_Address;
                obj.Created_by = objCardHolderBobibankingPaymentStatus.Created_by;
                obj.Created_dt = objCardHolderBobibankingPaymentStatus.Created_dt;
                repCardHolderBobibankingPaymentStatus.Add(obj);
                GeneralManager.Commit();
                return obj.PRN;
            }
            return "";
        }
        /// <summary>
        /// Updates the card holder payment status bobibanking.
        /// </summary>
        /// <param name="prn">The PRN.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="amountDue">The amount due.</param>
        /// <param name="bid">The bid.</param>
        /// <param name="debtAccountNo">The debt account no.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UpdateCardHolderPaymentStatusBobibanking(string prn, string accountNumber, double amountDue, string PID, string bid, 
                                                                string debtAccountNo, string status)
        {
            //commented by abhijeet on 21/08/2019
            //string Transaction_number = "";
            //CH_BobibankingPaymentStatus_Dtl obj = repCardHolderBobibankingPaymentStatus.SingleOrDefault(c => c.Creditcard_acc_number == accountNumber 
            //                                      && c.PRN == prn && c.AmountDue == amountDue && c.PaymentStatus.Trim() == "unsuccessful");
            //if (obj != null)
            //{
            //    obj.PaymentStatus = status;
            //    obj.BID = bid;
            //    obj.DebtAccountNo = debtAccountNo;
            //    obj.Updated_by = CardHolderManager.GetLoggedInUser().CardHolder_Id;
            //    obj.Updated_dt = DateTime.Now;
            //    GeneralManager.Commit();
            //    Transaction_number = obj.PRN;
            //}
            //return Transaction_number;
            BOBCardEntities _db = new BOBCardEntities();
            _db.Update_BobibankingPaymentStatus_Dtl(accountNumber, prn, amountDue, status, bid, debtAccountNo, DateTime.Now, CardHolderManager.GetLoggedInUser().CardHolder_Id.ToString());
            return prn;
        }
        #endregion
    }
}

