using System;
using System.Collections.Generic;
using CardHolder.DTO;
using CardHolder.DTO.Oracle;
using CardHolderOracle.DAL;

namespace CardHolder.BAL
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class CardManager
    {
        #region Repository Members
        #endregion

        #region Selection Methods

        /// <summary>
        /// Gets the card summary.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetCardSummary(string cardNumber)
        {
            CH_CardDTO objCardSummary = new CH_CardDTO();
            objCardSummary = CardDALC.GetCardSummary(cardNumber);
            return objCardSummary;
        }
        /// <summary>
        /// Gets the card list.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_CardDTO> GetCardList(string accountNumber)
        {
            List<CH_CardDTO> cards = new List<CH_CardDTO>();
            cards = CardDALC.GetCardList(accountNumber);
            return cards;
        }

        /// <summary>
        /// This method is used for getting card details for International Limit
        /// </summary>
        /// <param name="cDto">cDto</param>
        /// <returns>CH_CardDTO</returns>
        public CH_CardDTO GetCardDetailsForInternationalLimitByCardNumber(CH_CardDTO cDto)
        {
            return CardDALC.GetCardDetailsForInternationalLimitByCardNumber(cDto);
        }
        /// <summary>
        /// Gets the card statement.
        /// </summary>
        /// <param name="SkipCount">The skip count.</param>
        /// <param name="PageSize">Size of the page.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="monthRange">The month range.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_CR_TERMDTO> GetCardStatement(int SkipCount, int PageSize, ref int RecordCount, string accountNumber, int monthRange)
        {
            List<CH_CR_TERMDTO> objCardStatement = new List<CH_CR_TERMDTO>();
            objCardStatement = CardDALC.GetCardStatement(SkipCount, PageSize, ref RecordCount, accountNumber, monthRange);
            return objCardStatement;
        }
        /// <summary>
        /// Gets the name of the card statement PDF file.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="clientCode">The client code.</param>
        /// <returns></returns>
        //public CH_EVG_EVENTS_QUEUEDTO GetCardStatementPDFFileName(string cardNumber, DateTime dt)
        //{
        //    CH_EVG_EVENTS_QUEUEDTO objEventsQueue = new CH_EVG_EVENTS_QUEUEDTO();
        //    objEventsQueue = CardDALC.GetCardStatementPDFFileName(cardNumber, dt);
        //    return objEventsQueue;
        //}

        /// <summary>
        /// Gets the list PDF Files.
        /// </summary>
        /// <param name="accountNumber">the account Number.</param>
        /// <param name="PDFFile">The PDF file.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool GetPDFnames(string accountNumber, string PDFFile)
        {
            return CardDALC.GetPDFnames(accountNumber, PDFFile);
        }

        #endregion

        #region Get Card Details
        /// <summary>
        /// Find Card Detail From CARD As per discussion with PM
        /// </summary>
        /// <param name="cDto">cDto.card_number</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetCardByCreditCardNumber(CH_CardDTO cDto)
        {
            return CardDALC.GetCardByCreditCardNumber(cDto);
        }

        /// <summary>
        /// Authenticates the cr number DOB.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO AuthenticateCrNumberDOB(CH_CardDTO cDto)
        {
            return CardDALC.AuthenticateCrNumberDOB(cDto);
        }

        public CH_CardDTO AuthenticateCrNumberDOBForRegis(CH_CardDTO cDto)
        {
            return CardDALC.AuthenticateCrNumberDOBForRegis(cDto);
        }

        /// <summary>
        /// Gets the personal message.
        /// </summary>
        /// <param name="pDto">The p dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Personal_MessageDTO GetPersonalMessage(Personal_MessageDTO pDto)
        {
            return CardDALC.GetPersonalMessage(pDto);
        }

        /// <summary>
        /// Gets the various card fees.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetVariousCardFees(CH_CardDTO cDto)
        {
            return CardDALC.GetCardFees(cDto);
        }

        /// <summary>
        /// Gets the addon cards.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_CardDTO> GetAddonCards(CH_CardDTO cDto)
        {
            return CardDALC.GetAddonCards(cDto);
        }

        /// <summary>
        /// Gets all cards for ATM pin reg.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_CardDTO> GetAllCardsForATMPinReg(CH_CardDTO cDto)
        {
            return CardDALC.GetAllCardsForATMPinReg(cDto);
        }

        /// <summary>
        /// Gets the ATM pin details.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetATMPinDetails(CH_CardDTO cDto)
        {
            return CardDALC.GetATMPinDetails(cDto);
        }

        /// <summary>
        /// Gets the CH name statusby card number.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetCHNameStatusbyCardNumber(CH_CardDTO cDto)
        {
            return CardDALC.GetCHNameStatusbyCardNumber(cDto);
        }

        /// <summary>
        /// Gets all cards for replace renew.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<CH_CardDTO> GetAllCardsForReplaceRenew(CH_CardDTO cDto)
        {
            return CardDALC.GetAllCardsForReplaceRenew(cDto);
        }

        /// <summary>
        /// Cards the allowed for renewal.
        /// </summary>
        /// <param name="CR_ACCOUNT_NBR">The C r_ ACCOUN t_ NBR.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CardAllowedForRenewal(string CR_ACCOUNT_NBR)
        {
            return CardDALC.CardAllowedForRenewal(CR_ACCOUNT_NBR);
        }

        /// <summary>
        /// Cards the allowed for replacement.
        /// </summary>
        /// <param name="CR_ACCOUNT_NBR">The C r_ ACCOUN t_ NBR.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CardAllowedForReplacement(string CR_ACCOUNT_NBR)
        {
            return CardDALC.CardAllowedForReplacement(CR_ACCOUNT_NBR);
        }

        /// <summary>
        /// Cards the eligible for preserve STMNT.
        /// </summary>
        /// <param name="CR_ACCOUNT_NBR">The C r_ ACCOUN t_ NBR.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CardEligibleForPreserveStmnt(string CR_ACCOUNT_NBR)
        {
            return CardDALC.CardEligibleForPreserveStmnt(CR_ACCOUNT_NBR);
        }

        /// <summary>
        /// Cards the eligible for add on req.
        /// </summary>
        /// <param name="CR_ACCOUNT_NBR">The C r_ ACCOUN t_ NBR.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CardEligibleForAddOnReq(string CR_ACCOUNT_NBR)
        {
            return CardDALC.CardEligibleForAddOnReq(CR_ACCOUNT_NBR);
        }

        /// <summary>
        /// Counts the total addon cards.
        /// </summary>
        /// <param name="CR_ACCOUNT_NBR">The C r_ ACCOUN t_ NBR.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CountTotalAddonCards(string CR_ACCOUNT_NBR)
        {
            return CardDALC.CountTotalAddonCards(CR_ACCOUNT_NBR);
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetAccountDetails(CH_CardDTO cDto)
        {
            return CardDALC.GetAccountDetails(cDto);
        }

        /// <summary>
        /// Gets the auto debit payment details.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetAutoDebitPaymentDetails(CH_CardDTO cDto)
        {
            return CardDALC.GetAutoDebitPaymentDetails(cDto);
        }

        //public string GetCodeNameFromSyscode(string Syscode)
        //{
        //    return CardDALC.GetCodeNameFromSyscode(Syscode);
        //}

        /// <summary>
        /// Gets the branch name by code.
        /// </summary>
        /// <param name="Branchcode">The branchcode.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetBranchNameByCode(string Branchcode)
        {
            return CardDALC.GetBranchNameByCode(Branchcode);
        }

        /// <summary>
        /// Gets the preserve STMNT details.
        /// </summary>
        /// <param name="cDto">The c dto.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CH_CardDTO GetPreserveStmntDetails(CH_CardDTO cDto)
        {
            return CardDALC.GetPreserveStmntDetails(cDto);
        }

        /// <summary>
        /// Gets the branch list.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<Bank_MstDTO> GetBranchList()
        {
            return CardDALC.GetBranchList();
        }

        /// <summary>
        /// Authenticates the user status.
        /// </summary>
        /// <param name="AccountNumber">The account number.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool AuthenticateUserStatus(string AccountNumber)
        {
            return CardDALC.AuthenticateUserStatus(AccountNumber);
        }

        /// <summary>
        /// Gets the list of syscode.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<SYSCodeDTO> GetListOfSyscode()
        {
            return CardDALC.GetListOfSyscode();
        }

        /// <summary>
        /// Gets the list of country.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<SYSCodeDTO> GetListOfCountry()
        {
            return CardDALC.GetListOfCountry();
        }

        /// <summary>
        /// Gets the list of city.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<SYSCodeDTO> GetListOfCity()
        {
            return CardDALC.GetListOfCity();
        }

        public List<SYSCodeDTO> GetListOfApplicationType()
        {
            return CardDALC.GetListOfApplicationType();
        }

        public List<SYSCodeDTO> GetPromoCode()
        {
            return CardDALC.GetPromoCode();
        }

        /// <summary>
        /// This method is used for getting card details for Auto Debit Payment Type
        /// </summary>
        /// <param name="cDto">cDto</param>
        /// <returns>CH_CardDTO</returns>
        public CH_CardDTO GetAutoDebitPaymentType(CH_CardDTO cDto)
        {
            return CardDALC.GetAutoDebitPaymentType(cDto);
        }

       
        #endregion

        #region checkConnection
        public bool CheckOracleConnection()
        {
            return CardDALC.CheckOracleConnection();
        }
        #endregion
    }
}
