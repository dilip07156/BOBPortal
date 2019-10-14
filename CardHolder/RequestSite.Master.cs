﻿using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.DTO.Oracle;
using CardHolder.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardHolder
{
    public partial class RequestSite : System.Web.UI.MasterPage
    {

        /// <summary>
        /// session variable for CardHolerRequestTypes
        /// </summary>
        string sessionCardHolderRequestTypes = "CardHolerRequestTypes";
        /// <summary>

        /// <summary>
        /// 
        /// </summary>
        // string CardNumber = CardHolderManager.GetLoggedInUser().credit_card_number.Decrypt();
        string CardNumber = Convert.ToString(CardHolderManager.GetLoggedInUser().CH_Card.card_number);

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Caching Management

            Response.AppendHeader("Cache-Control", "no-cache,private,no-store, post-check=0,pre-check=0,must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT");

            #endregion

            //if (Session[".ASPXAUTH"] != null && Request.Cookies[".ASPXAUTH"] != null)
            //{
            //    if (!Session[".ASPXAUTH"].ToString().Equals(Request.Cookies[".ASPXAUTH"].Value))
            //    {
            //        Response.Redirect(Constants.pErrorPage, true);
            //    }
            //}
            //else
            //    Response.Redirect(Constants.pErrorPage, true);

            #region   Privilege escalation (Horizontal) Session of IP and AntiFix

            if (Session.Count > 0)
            {
                //IP Of User
                if (Session["STTLI"] != null)
                {
                    if (Session["STTLI"].ToString() != Request.Cookies["STTLI"].Value && Session["STTLI"].ToString() != Functions.GenerateHash(Functions.GetIP()))
                    {
                        Response.Redirect(Constants.pErrorPage, true);
                    }
                }
                else
                {
                    Response.Redirect(Constants.pErrorPage, true);
                }
                // Random Token antifix
                if (Session["STTLII"] != null)
                {
                    if (Session["STTLII"].ToString() != Request.Cookies["STTLII"].Value)
                    {
                        Response.Redirect(Constants.pErrorPage, true);
                    }
                    else
                    {
                        Random random1 = new Random();
                        string rndstr1 = random1.Next(100000).ToString();
                        rndstr1 = Functions.GenerateHash(rndstr1);
                        Session["STTLII"] = rndstr1;
                        Response.Cookies["STTLII"].Value = rndstr1;
                        Response.Cookies["STTLII"].HttpOnly = true;
                    }
                }
                else
                {
                    Response.Redirect(Constants.loginPage, true);
                }
            }
            else
            {
                Response.Redirect(Constants.loginPage, true);
            }

            #endregion


            // string tmpurl = Request.Url.ToString().Substring(Request.Url.ToString().LastIndexOf(@"/") + 1).ToUpper();
            string tmpurl = Request.FilePath.ToString().Substring(Request.FilePath.ToString().LastIndexOf(@"/") + 1).ToUpper();
            string[] arrytmpUrl = tmpurl.Split('?');
            if (arrytmpUrl.Length > 0)
                hdnCurrentURIPath.Value = arrytmpUrl[0].ToString();
            else
                hdnCurrentURIPath.Value = tmpurl;

            string strActivedv = "";
            // if(hdnCurrentURIPath.Value != "")
            string[] arrayActvDv = hdnCurrentURIPath.Value.ToString().Split('.');

            if (arrayActvDv.Length > 0)
                strActivedv = arrayActvDv[0].ToString();

            string strchkisSublink = "0";
            if (hdnsublinks.Value.Contains(strActivedv))
                strchkisSublink = "1";

            ActiveTab(strActivedv, strchkisSublink);
            RegisterJavaScriptAndCss();
            RegisterJavaScriptlibraries();
            //RegisterNewCSSlibraries();
            if (IsPostBack)
                return;

            OnPageload(strchkisSublink);
            if (CardNumber != "")
                SetMarquee(CardNumber);

        }

        /// <summary>
        /// Sets the marquee.
        /// </summary>
        /// <param name="CardNumber">The card number.</param>
        /// <remarks></remarks>
        private void SetMarquee(string CardNumber)
        {
            Personal_MessageDTO Pdto = new Personal_MessageDTO();
            CardManager cm = new CardManager();
            Pdto = cm.GetPersonalMessage(new Personal_MessageDTO() { CARD_NUMBER = CardNumber });

            if (Pdto != null)
            {
                string PersonalMessge = Pdto.MESSAGE;
                //  string txtmarquee = "<marquee behavior='scroll' direction='left' onmouseover='this.stop()' onmouseout='this.start()'>" + PersonalMessge + "</marquee>";

                litMarquee.Text = PersonalMessge;
            }
            else
                divmarq.Visible = false;
        }
        /// <summary>
        /// Registers the java script and CSS.
        /// </summary>
        /// <remarks></remarks>
        private void RegisterJavaScriptAndCss()
        {
            int index = 0;
            //Page.RegisterStyleSheet("style.css", index++);
            Page.RegisterStyleSheet("thickbox.css");
            Page.RegisterJavaScript("jquery-1.8.3.js");
            Page.RegisterJavaScript("jquery-1.7.1.min.js");
            Page.RegisterJavaScript("css_browser_selector.js");
            Page.RegisterJavaScript("jquery-ui-1.8.18.custom.min.js");
            Page.RegisterJavaScript("bind.js");
            Page.RegisterJavaScript("General.js");
            Page.RegisterJavaScript("thickbox_3.1.js");
        }


        private void RegisterJavaScriptlibraries()
        {
            int index = 0;
            Page.RegisterNewCSS("bootstrap.min.css", index++);
            Page.RegisterNewCSS("bob.min.css");
            Page.RegisterNewCSS("jquery.mCustomScrollbar.min.css");
            Page.RegisterNewCSS("slick.css");
            Page.RegisterNewCSS("slick-theme.css");
            Page.RegisterNewCSS("bootstrap-datepicker.min.css");

            Page.RegisterNewJavaScript("jquery.min.js");
            Page.RegisterNewJavaScript("jquery-1.7.1.min.js");
            Page.RegisterNewJavaScript("popper.min.js");
            Page.RegisterNewJavaScript("bootstrap.min.js");
            Page.RegisterNewJavaScript("slick.min.js");
            Page.RegisterNewJavaScript("jquery.mCustomScrollbar.concat.min.js");
            Page.RegisterNewJavaScript("jquery.nice-select.min.js");
            Page.RegisterNewJavaScript("bootstrap-datepicker.min.js");
        }
        /// <summary>
        /// Actives the tab.
        /// </summary>
        /// <param name="dvName">Name of the dv.</param>
        /// <param name="chkisSublnk">The chkis sublnk.</param>
        /// <remarks></remarks>
        private void ActiveTab(string dvName, string chkisSublnk)
        {

            string Script = "<script type='text/javascript' language='javascript'>";
            Script += "setPannel('" + dvName + "','" + chkisSublnk + "');";
            Script += "</script>";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ChkJSEnable", Script, false);

        }
        /// <summary>
        /// Called when [pageload].
        /// </summary>
        /// <remarks></remarks>
        private void OnPageload(string chkisSublnk)
        {
            SetNameAndLastLoginDate();
            CheckUserIsActiveIsDisabled();
            SetRequestTypesURL();

        }

        /// <summary>
        /// Checks the user is active is disabled.
        /// </summary>
        /// <remarks></remarks>
        private void CheckUserIsActiveIsDisabled()
        {
            CardHolderManager am = new CardHolderManager();
            string username = CardHolderManager.GetLoggedInUser().User_nm;

            //commented by abhijeet on 23/01/2019
            //CardHolder_MstDTO user = am.FindUser(username);
            CardHolder_MstDTO user = am.FindActiveUser(username);
            if (user != null)
            {
                if (user.IsPermanentDisable == true && user.IsActive == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.BlockedAccount + "');", true);
                    SessionLogout();
                }
                else if (user.IsPermanentDisable == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.BlockedAccount + "');", true);
                    SessionLogout();
                }
                else if (user.IsActive == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.InactiveAccount + "');", true);
                    SessionLogout();
                }
            }
        }

        /// <summary>
        /// Sessions the logout.
        /// </summary>
        /// <remarks></remarks>
        private void SessionLogout()
        {

            CardHolder.Utility.CacheHelperBySession<CardHolder_MstDTO>.InvalidateCache();
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            ClearCache();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
            Response.Cookies.Add(new HttpCookie("STTLII", string.Empty));
            Response.Redirect("~/Login.aspx");
        }

        public static void ClearCache()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
            HttpContext.Current.Response.Cookies.Clear();
            HttpContext.Current.Request.Cookies.Clear();
        }
        /// <summary>
        /// Set user name and last login date and time
        /// </summary>
        /// <remarks></remarks>
        private void SetNameAndLastLoginDate()
        {
            CH_CardDTO User = CardHolderManager.GetLoggedInUser().CH_Card;
            if (User != null)
            {
                lblFullName.Text = User.FULL_NAME;
            }
            //lblLastLoginDateTime.Text = CardHolderManager.GetLoggedInUser().LastLoginDate.HasValue ? "Your last login on " + CardHolderManager.GetLoggedInUser().LastLoginDate.Value.ToString("dd MMMM yyyy hh:mm tt") : "You login first time";
        }

        /// <summary>
        /// Sets the request types URL.
        /// </summary>
        /// <remarks></remarks>
        private void SetRequestTypesURL()
        {
            try
            {

                long cardReplacementRequestId1 = 3;
                long cardReplacementRequestId2 = 16;
                //int RequestTypeIdForInternationalLimit = 26;
                if (Session[sessionCardHolderRequestTypes] != null)
                {
                    List<CH_RequestType_MstDTO> lstRequestTypes = (List<CH_RequestType_MstDTO>)Session[sessionCardHolderRequestTypes];
                    CH_RequestType_MstDTO objCardReplacement = lstRequestTypes.Find(x => x.RequestType_nm.Replace(" ", "").ToLower() == "cardreplacement");
                    CH_RequestType_MstDTO objCardRenewal = lstRequestTypes.Find(x => x.RequestType_nm.Replace(" ", "").ToLower() == "cardrenewal");
                    if (objCardReplacement != null)
                        cardReplacementRequestId1 = objCardReplacement.RequestType_Id;

                    if (objCardRenewal != null)
                        cardReplacementRequestId2 = objCardRenewal.RequestType_Id;

                    foreach (CH_RequestType_MstDTO RequestType in lstRequestTypes)
                    {

                        string requestTypeName = RequestType.RequestType_nm.Replace(" ", "").Replace("-", "").ToLower();
                        if (requestTypeName == "atmpinregenerationrequest") //1
                            hlnkATM_PIN_REGENERATION.NavigateUrl = "~/ServiceRequest/ATM_PIN_Regeneration.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        //else if (requestTypeName == "blockingofcard") //2 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                        //    hlnkBLOCKINGCARD.NavigateUrl = "~/ServiceRequest/BlockingCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "cardreplacement")
                            hlnkCREDITCARDREPLACEMENTRENEWAL.NavigateUrl = "~/ServiceRequest/CreditCardReplacementRenewal.aspx?replacementid=" + cardReplacementRequestId1.ToString().EncryptURL() + "&renewalid=" + cardReplacementRequestId2.ToString().EncryptURL();
                        else if (requestTypeName == "emirequest")//4 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                            hlnkEMIREQUEST.NavigateUrl = "~/ServiceRequest/EMIRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "loanrequest")
                            hlnkLOANREQUEST.NavigateUrl = "~/ServiceRequest/LoanRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "addoncardrequest") //5
                            hlnkREQUESTADDONCARDPAGE.NavigateUrl = "~/ServiceRequest/RequestAddonCardPage.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "statementmode")//6
                            hlnkSTATEMENTREQUEST.NavigateUrl = "~/ServiceRequest/StatementDeliveryMode.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "preservedstatementrequest")//7
                            hlnkPRESERVESTATEMENTREQUEST.NavigateUrl = "~/ServiceRequest/PreserveStatementRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "deregistrationofcard")//8
                            hlnkDEREGISTERCREDITCARD.NavigateUrl = "~/ServiceRequest/DeRegisterCreditCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "autodebitpaymenttype")//9
                            hlnkAUTODEBITPAYMENTTYPE.NavigateUrl = "~/ServiceRequest/AutoDebitPaymentType.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "autodebitderegistration")//10
                            hlnkAUTODEBITDE_REGISTRATION.NavigateUrl = "~/ServiceRequest/AutoDebitDe_Registration.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "bonuspointredemption")//11
                            hlnkBONUSPOINTREDEMPTION.NavigateUrl = "~/ServiceRequest/BonusPointRedemption.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "blockingofcard") //12 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                            hlnkBLOCKINGCARD.NavigateUrl = "~/ServiceRequest/BlockingCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        //Commented for this release on 28_03_2019
                        else if (requestTypeName == "internationllimit")
                            hlnkInternationalLimitOpenClose.NavigateUrl = "~/ServiceRequest/InternationlLimitOpenClose.ASPX?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                    }
                }
                else
                {
                    CardHolderRequestManager objCardHolderRequestType = new CardHolderRequestManager();
                    List<CH_RequestType_MstDTO> lstRequestTypes = objCardHolderRequestType.getCHRequestTypes();
                    CH_RequestType_MstDTO objCardReplacement = lstRequestTypes.Find(x => x.RequestType_nm.Replace(" ", "").ToLower() == "cardreplacement");
                    CH_RequestType_MstDTO objCardRenewal = lstRequestTypes.Find(x => x.RequestType_nm.Replace(" ", "").ToLower() == "cardrenewal");
                    if (objCardReplacement != null)
                        cardReplacementRequestId1 = objCardReplacement.RequestType_Id;

                    if (objCardRenewal != null)
                        cardReplacementRequestId2 = objCardRenewal.RequestType_Id;

                    foreach (CH_RequestType_MstDTO RequestType in lstRequestTypes)
                    {
                        string requestTypeName = RequestType.RequestType_nm.Replace(" ", "").Replace("-", "").ToLower();
                        if (requestTypeName == "atmpinregenerationrequest") //1
                            hlnkATM_PIN_REGENERATION.NavigateUrl = "~/ServiceRequest/ATM_PIN_Regeneration.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        //else if (requestTypeName == "blockingofcard") //2 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                        //    hlnkBLOCKINGCARD.NavigateUrl = "~/ServiceRequest/BlockingCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "cardreplacement")
                            hlnkCREDITCARDREPLACEMENTRENEWAL.NavigateUrl = "~/ServiceRequest/CreditCardReplacementRenewal.aspx?replacementid=" + cardReplacementRequestId1.ToString().EncryptURL() + "&renewalid=" + cardReplacementRequestId2.ToString().EncryptURL();
                        //commented by abhijeet on 04/02/2019 as per instruction
                        else if (requestTypeName == "emirequest")//4 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                            hlnkEMIREQUEST.NavigateUrl = "~/ServiceRequest/EMIRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        //else if (requestTypeName == "loanrequest")
                        //    hlnkLOANREQUEST.NavigateUrl = "~/ServiceRequest/LoanRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "addoncardrequest") //5
                            hlnkREQUESTADDONCARDPAGE.NavigateUrl = "~/ServiceRequest/RequestAddonCardPage.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "statementmode")//6
                            hlnkSTATEMENTREQUEST.NavigateUrl = "~/ServiceRequest/StatementDeliveryMode.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "preservedstatementrequest")//7
                            hlnkPRESERVESTATEMENTREQUEST.NavigateUrl = "~/ServiceRequest/PreserveStatementRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "deregistrationofcard")//8
                            hlnkDEREGISTERCREDITCARD.NavigateUrl = "~/ServiceRequest/DeRegisterCreditCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "autodebitpaymenttype")//9
                            hlnkAUTODEBITPAYMENTTYPE.NavigateUrl = "~/ServiceRequest/AutoDebitPaymentType.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "autodebitderegistration")//10
                            hlnkAUTODEBITDE_REGISTRATION.NavigateUrl = "~/ServiceRequest/AutoDebitDe_Registration.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "bonuspointredemption")//11
                            hlnkBONUSPOINTREDEMPTION.NavigateUrl = "~/ServiceRequest/BonusPointRedemption.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        else if (requestTypeName == "blockingofcard")//11
                            hlnkBLOCKINGCARD.NavigateUrl = "~/ServiceRequest/BlockingCard.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();
                        //else if (requestTypeName == "balancetransferrequest") //12 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                        //    hlnkBALANCETRANSFERREQUEST.NavigateUrl = "~/ServiceRequest/BalanceTransferRequest.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL(); 
                        //Commented for this release on 28_03_2019
                        else if (requestTypeName == "internationllimit") //12 //TO BE ADD LATER ON (Commented on 20-May-2014 as client says to depublish it)//
                            hlnkInternationalLimitOpenClose.NavigateUrl = "~/ServiceRequest/InternationlLimitOpenClose.aspx?requestid=" + RequestType.RequestType_Id.ToString().EncryptURL();

                    }
                    Session[sessionCardHolderRequestTypes] = lstRequestTypes;
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Constants.strErrorRequestTypes + "');", true);
            }
        }
    }
}