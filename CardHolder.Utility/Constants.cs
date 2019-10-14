
namespace CardHolder.Utility
{




    public static class Constants
    {
        #region  Constants

        //General & Validations msgs



        public static string NoDataFoundForGraph = "No Data found to display graph.";
        public static string axisYTitle = "Amount ( In Rupees )";
        public static string axisXTitle = "Description of spends";
        public static string rupees = "Rs.";
        public static string selectItem = "Select Summary";
        public static string selectSummary = "Select Card";
        public static string selectItemTransactionType = "Select Type";
        public static string AccountSummaryOn = "Account Summary as on : ";
        public static string CardSummaryOn = "Card Summary as on : ";
        public static string LastBillSummaryDated = "Last bill summary for dated : ";
        public static string RewardPointsSummaryOn = "Reward points summary as on : ";
        public static string BankId = "012";
        //public static string PID = "000000000732";
        public static string PID = "000000022498"; // "000029372568";
        public static string BobibankingKeyPath = "BobibankingKeyPath";
        public static string bobibankingFileName = "BOB.KEY";
        public static string fileNotFound = "File not found";
        public static string GraphLabel = "Spend Analyser";

        public static string ACH_ACCOUNT_TYPE = "ACH_ACCOUNT_TYPE";




        public static string OTPDesc1 = "As an enhanced security measure you are required to key in a One-Time Password(OTP) in order to proceeed further in your registration process. We have sent the One-Time Password(OTP) to your registered mobile number and email-id. "; //amar



        public static string OTPDescforchangePwd = "As an enhanced security measure you are required to key in a One-Time Password(OTP) in order to proceeed with your request. We have sent the One-Time Password(OTP) to your registered mobile number and email-id. ";//amar



        public static string OTPDesc2 = "In Order if your mobile number/email-id is not correct please contact to our Call Centre.";




        public static string ErrorRegister = "Sorry! There is some problem while registration, kindly try after some time";



        public static string SomeProblem = "Sorry! There is some problem, kindly try after some time";



        public static string AlreadyRegister = "Sorry! You are already registered with us";



        public static string InvalidUnamePwd = "Invalid username or password";



        public static string Leftwith2Attempts = "Your response is incorrect. Please try again. You are left with 2 more attempts";



        public static string Leftwith1Attempts = "Your response is incorrect. Please try again. You are left with 1 more attempts";

        public static string BlockedCard = "Your card is blocked; Please contact customer care";



        public static string SecndDayLeftwith2Attempts = "This is your second day when you are trying with wrong credentials. You are left with 2 more attempts";



        public static string SecndDayLeftwith1Attempts = "This is your second day when you are trying with wrong credentials. You are left with 1 more attempts";




        public static string ThirdDayLeftwith2Attempts = "This is your continuous third day when you are trying with wrong credentials. You are left with 2 more attempts";
        //  public static string ThirdDayLeftwith1Attempts = "This is your continuous third day when you are trying with wrong credentials. You are left with 1 more attempts";



        public static string Leftwithonly1Attempt = "Please Note: Now you are left with only one attempt; if you enter Invalid credentials again, Your account will be blocked!!";//amar




        public static string ErrorMsg1 = "Either file not found or some error has occured.Please contact administrator.";//amar



        public static string InactiveAttempts = "As you have tried login with 3 invalid attempts, your account is inactive for 24hrs";//amar



        public static string InactiveAccount = "Your account is InActive; Please try again after 24hours";



        public static string InactiveAccountAfter = "Your account is InActive; Please try again after ";



        public static string ContinuesBlockedAccount = "You are trying invalid credentials from last three days; So your account is blocked; Please contact customer care";



        public static string BlockedAccount = "Your account is blocked; Please contact customer care";



        public static string UnameNtExist = "Username does not exists";//amar



        public static string NotRegister = "Sorry! You are not registered with us";
        //     public static string InvalidCRnumber = "Credit card number is invalid";



        public static string IncorrectOTP = "Incorrect One-Time Password, Please try again";

        public static string PINMismatch = "PIN Mismatch";


        public static string IncorrectATMPIN = "Incorrect ATM PIN, Please reenter";



        public static string incorrectPWd = "Incorrect Password!! Please try again";



        public static string NotEqualPwd = "New password should not be equal to old password! Please try another";



        public static string IncorrectOldPwd = "Incorrect old password!! Please try again";



        public static string InvalidCaptcha = "Incorrect captcha code, Please try again";

        public static string InvalidCaptchaExpired = "Captcha code expired, Please try again";



        public static string InvalidEntries = "Credit card number/Date of birth/Expiry date is invalid";



        public static string DataNotFoundindb = "Data not found, Please try again with your latest card or contact customer care";



        ////public static string AccNotInNormalState = "Your account is not in normal state; Please contact branch";
        public static string AccNotInNormalState = "Some issue encountered with your account status. Please call our Helpline Number 1800225100";

        public static string DbConnectionNotAvailable = "Error in Processing! Please retry after some time. For assistance call our Helpline Number 1800225100";

        public static string enterPersonalMsg = "Please enter personal message";

        public static string selectmode = "Please select mode for statement delivery";



        public static string selectStmntType = "Please select statement type";



        public static string selectmonth = "Please select month in a Date Range";



        public static string selectyear = "Please select year in a Date Range";



        public static string RequestNotFound = "Sorry!! No request is found";



        public static string CompliantNotFound = "Sorry!! No complaint is found";



        public static string RecordNotFound = "Sorry!! No record found";



        public static string SelectReqComp = "Please select Request/complaint";



        public static string SelectCardPIN = "Please Select Card/PIN";



        public static string Selectreason = "Please select valid reason";
        //    public static string AlreadyLoggedIn = "You are already logged in. Please try after few minutes or log off first from previous login or contact administrator urgently.";



        public static string PaymentHistoryRange = "You can view payment history upto last ";



        public static string StatementMonths = "Below is the card statement for last ";



        public static string OfficeAddress = "Office";



        public static string Permanent_Address = "Permanent";



        public static string Correspondence_Address = "Correspondence";


        //Dropdowns



        public static string DDLComplaint = "Select Complaint";
        //   public static string DDLDispatch = "Select Type";



        public static string DDLReason = "Select Reason";



        public static string DDLRequest = "Select Request";

        public static string DDLService = "Select Service";

        public static string DDLBank = "Select Bank";



        public static string DDLPlan = "Select Plan";
        //    public static string DDLCard = "Select Card";



        public static string DDLBranch = "Select";



        public static string DDLVIP = "Select VIP Status";



        public static string DDLSocialStatus = "Select Social Status";



        public static string DDLMaritalStatus = "Select Marital Status";



        public static string DDLtitle = "--";



        public static string DDLResidance = "Select";



        public static string DDLcity = "Select City";



        public static string DDLCountry = "Select Country";



        public static string DDLEducation = "Select Education";



        public static string DDLEmpType = "Select EmployerType";



        public static string DDLBankName = "Select Bank Name";



        public static string DDLNomineeRel = "Select Relation";



        //Syscodes



        public static string VIP_CODE = "VIP_CODE";



        public static string SOCIAL_STATUS = "SOCIAL_STATUS";



        public static string TITLE = "TITLE";



        public static string MARITAL_STATUS = "MARITAL_STATUS";



        public static string RESIDANCE_CHANGED = "RESIDANCE_CHANGED";



        public static string RESIDENCE_STATUS = "RESIDENCE_STATUS";



        public static string EDUCATION = "EDUCATION";



        public static string EMPLOYMENT_STATUS = "EMPLOYMENT_STATUS";



        public static string COMP_TYPE = "COMP_TYPE";



        public static string APPLICANT_PROF = "APPLICANT_PROF";



        public static string EMPL_DESIGNATION = "EMPL_DESIGNATION";



        public static string OWNED_VEHICLE_TYPE = "OWNED_VEHICLE_TYPE";



        public static string INCOME_PER_MONTH = "INCOME_PER_MONTH";



        public static string ACC_IN_BANK = "ACC_IN_BANK";



        public static string RELATION = "RELATION";



        public static string NOMINEE_RELATION = "NOMINEE_RELATION";




        //Error Msgs

        public static string TechnicalError = "Due to some technical problems unable to process; Please try after some time";

        public static string MaxNoOfOTPMessageForTime = "Please wait 20 seconds before requesting new OTP.";

        public static string MaxNoOfOTPMessage = "You have requested the OTP a maximum of 3 times. Please try after sometime.";

        public static string Error1 = "Please enter proper amount.";

        public static string amounterror = "Please enter amount greater than zero.";


        public static string Error2 = "Please select proper payment method.";



        public static string Error3 = "You have entered amount which is greater than amount due.Please enter correct amount.";



        public static string Error4 = "Payment is not executed successfully. Please try again or contact bank.";



        public static string Error5 = "Payment done but some error generated while updating data.Please contact customer care.";//amar



        public static string PaymentError = "Some error generated executing payment process.Please try again or contact bank.";



        public static string GeneralErrorMessage = "Some error has occured.Please try again or contact customer care.";//amar



        public static string GeneralRequestError = "There is an error while processing your request. Please Try again later";



        public static string Errorapp = "Error while saving Application form details";



        public static string Errorsendingmail = "There is an error occurred while sending Email.";



        public static string ErrorMailButRqstLogged = "There was an error while sending you email!! But your Request has been logged";



        public static string agreeTnC = "Please go through terms and condition carefully before agreeing to it";//amar



        public static string RelationNotfound = "Relation is not found for this request, Please contact Customer care";



        public static string DispatchNotfound = "Dispatch Types are not found for this request, Please contact Customer care";



        public static string termNotfound = "Terms are not found for this request, Please contact Customer care";



        public static string planNotfound = "Plans are not found for this request, Please contact Customer care";



        public static string ErrorUploadMsg = "Please select profile photo having size less than 20Kb";//amar



        public static string ErrorUploadMsg1 = "Please select addon1 photo having size less than 20Kb";//amar



        public static string ErrorUploadMsg2 = "Please select addon2 photo having size less than 20Kb";//amar



        public static string ErrorUploadMsg3 = "Please select addon3 photo having size less than 20Kb";//amar



        public static string only3Addons = "Sorry!! Only 3 add-ons allowed under one account number";//amar



        public static string AccountNotEligible = "Sorry!! Your account is not eligible for addon card";



        public static string AutoDebitNotAllowed = "Sorry!! There is no bank of baroda account number is registered with us; Hence we are unable to activate your auto debit services. Please download and fill particular form and submit through your Bank of Baroda branch.";



        public static string AutoDebitFormforReg = "To download requisite form please find link below on the same page";//amar



        public static string AutoDeRegisNotAllowed = "Sorry!! There is no auto debit services activated in your account";//amar
        //public static string AutoDebitDeRegisNotAllowed = "Sorry!! De-Registration for auto debit payment type is not allowed for your account";



        public static string AccNotNormal = "Sorry! As your account is not in normal state, you cannot make this request";//amar



        public static string SelectTxn = "Please select atleast one transaction to make this request";//amar



        public static string NoEmi = "No Record found to make EMI request";

        public static string NoLoan = "Sorry there is no offer Available for You, Please keep visiting us for latest offers";

        public static string LoanSuccess = "Congratulations You are eligible for Pre-Approved Loan (with in your Credit Limit .i.e. ";

        public static string NotEligibleForPreserve = "Sorry!! Your account is not eligible for preserved statement request";//amar



        public static string NoPdfFound = "Sorry!! No statement is found for your account";



        public static string LessPoints = "Points should be greater than 500 and less than equal to total earned points";//amar




        public static string ForgotPwd = "Forgot Password";



        public static string ModifyPwd = "Modify Password";



        public static string Registration = "Registration";



        public static string ForgotUName = "Forgot Username";

        public static string strErrorRequestTypes = "Request Types are not found; For assistance call our Helpline Number 1800225100";



        public static string ErrorCode100 = "Data Encryption Error";
        public static string ErrorCode101 = "Unable To Connect SOAP Server";
        public static string ErrorCode102 = "Timeout With SOAP server";
        public static string ErrorCode103 = "Portal Connection close";
        public static string ErrorCode200 = "SOAP Server Authentication Error";
        public static string ErrorCode201 = "Unable To Connect HSM";
        public static string ErrorCode202 = "HSM timeout";
        public static string ErrorCode203 = "Unable To Connect Database";
        public static string ErrorCode204 = "Your Card Is Not In Active State";
        public static string ErrorCode205 = "Database Failure";
        public static string CommonError = "Ooops Something went wrong. If You do not receive SMS confirmation of your PIN change Please try after sometime.";
        public static string msg = "Ooops Something went wrong.. Please try after sometime.";
          





        //Success msgs
        public static string AppSuccess = "Congratultaions!! Application form details has been saved sucessfully";
        public static string ProfileSuccess = "Your Profile has been updated successfully";//amar
        public static string PwdSuccess = "Congratulations!! Password has been changed successfully. Please login with new password.";//amar
        public static string RequestRegister = "Your Request has been registered with us";
        public static string emailsent = "Sent";
        public static string emailfail = "Fail";
        public static string smsDelivered = "Delivered";
        public static string DataSuccess = "Data saved Successfully";


        //Parameter config Keys 
        public static string paymentHistoryMonths = "PaymentHistoryMonths";
        public static string CardHolderStatementFilePath = "CardHolderStatementFilePath";
        public static string CardHolderStatementMonthly = "CardHolderStmntMonthly";
        // public static string FilePath = "FilePath";
        // public static string MerchantStatementFilePath = "MerchantStatementFilePath";


        //Mail Template Path
        public static string ForgotUsernameTemplatepath = "\\MailTemplates\\ForgotUsername.htm";
        public static string RegistrationTemplatepath = "\\MailTemplates\\Registration.htm";
        public static string StatementRequestTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\StatementRequest.htm";
        public static string AddonRequestTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\RequestAddonCardPage.htm";
        public static string PreserveStatementRequestTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\PreserveStatementRequest.htm";
        public static string OtherReqComplTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\OtherRequest_Complaint.htm";
        public static string EMIRequestTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\EMIRequest.htm";
        public static string LoanRequestTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\LoanRequest.htm";
        public static string DeRegisterCreditCardTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\DeRegisterCreditCard.htm";
        public static string CreditCardReplacementRenewalTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\CreditCardReplacementRenewal.htm";
        public static string BonusPointRedemptionTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\BonusPointRedemption.htm";
        public static string BlockingCardTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\BlockingCard.htm";
        public static string BalanceTransferRequestTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\BalanceTransferRequest.htm";
        public static string AutoDebitPaymentTypeTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\AutoDebitPaymentType.htm";
        public static string AutoDebitDeRegistrationTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\AutoDebitDeRegistration.htm";
        public static string ATM_PIN_RegenerationTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\ATM_PIN_Regeneration.htm";
        public static string InternationalLimitTemplatepath = "\\MailTemplates\\ServiceRequestTemplates\\InternationalLimit.htm";
        public static string ApplicationTemplatepath = "\\MailTemplates\\Application.htm";

        // PageLink
        public static string pErrorPage = "~/ErrorPage/CodeError.aspx";
        public static string WebError = "~/ErrorPage/WebError.aspx";
        public static string loginPage = "~/Login.aspx";


        //CreditCardTypes
        public const string Silvervisa = "P01";
        public const string Exclusivegeneralmaster = "P02";
        public const string Exclusiveyouthmaster = "P03";
        public const string Exclusivewomenmaster = "P04";
        public const string Goldvisa = "P05";
        public const string Goldinternationalvisa = "P06";
        public const string Goldmastercard = "P07";
        public const string Platinummaster = "P21";
        public const string Platinumvisa = "P22";
        public const string Elite = "P23";
        public const string Corporatepremium = "P24";
        public const string Platinumselect = "P25";
        public const string Visasignature = "P26";
        public const string Platinumbba = "P27";
        public const string Bobcarassure = "P28";
        public const string Titanium = "P29";
        public const string Xlri = "P30";
        public const string Bobcardpaytm = "P31";
        public const string Personalloan = "P32";

        public const string P01 = "SILVERVISA";
        public const string P02 = "EXCLUSIVEGENERALMASTER";
        public const string P03 = "EXCLUSIVEYOUTHMASTER";
        public const string P04 = "EXCLUSIVE WOMEN MASTER";
        public const string P05 = "GOLD VISA";
        public const string P06 = "GOLD INTERNATIONAL VISA";
        public const string P07 = "GOLD MASTER CARD";
        public const string P21 = "PLATINUM MASTER";
        public const string P22 = "PLATINUM VISA";
        public const string P23 = "ELITE";
        public const string P24 = "CORPORATE PREMIUM";
        public const string P25 = "PLATINUM SELECT";
        public const string P26 = "VISA SIGNATURE";
        public const string P27 = "PLATINUM-BBA";
        public const string P28 = "BOBCARD ASSURE";
        public const string P29 = "TITANIUM";
        public const string P30 = "XLRI";
        public const string P31 = "BOBCARD PAYTM";
        public const string P32 = "PERSONAL LOAN";

        //Request Pending Status
        public static string PendingRequestState = "Sorry! This request is already in pending state, you cannot make this request";
        public static string PendingComplaintState= "Sorry! This complaint is already in pending state, you cannot make this complaint";

        //Dashboard empty data messages
        public static string EmptyPointSummaryMainText = "Oops! Nothing to redeem yet.";
        public static string EmptyPointSummarySubText = "Don't miss …..you. Make your first transaction today!";
        public static string EmptyBillingSummaryMainText = "No Bills Generated Yet";
        public static string EmptyBillingSummarySubText = "Find your current balance,fees and interest charged since your last billing statement";
        public static string EmptyRecentTransactionMainText = "No Transactions Yet";
        public static string EmptyRecentTransactionSubText = "Looks like you have't transacted yet. Your transaction will help you keep the record of the latest credit card transactions.";
        public static string EmptyRequestStatusMainText = "Looks like you haven't raised any service request yet.";
        public static string EmptyRequestStatusSubText = "Request for change PIN, Block, Lost or Stolen Card and much more all within minutes.";

        //Dashboard Tooltip 
        public static string TooltipTotalAmountDue = "Total amount due is the total credit card bill of the present cycle which is required to be paid off before the due date.";
        public static string TooltipMinimumAmountDue = "The basic amount which should be paid in order to on or before the due date in order to avoid the levy of late payment charges.";
        public static string TooltipUnbilledAmount = "The amount you have purchased before the credit card bill generation date.";
        #endregion
    }
}
