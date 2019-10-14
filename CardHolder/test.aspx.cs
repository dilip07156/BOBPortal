using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using CardHolder.BAL;
using CardHolder.DTO;
using CardHolder.Utility;
using Microsoft.Security.Application;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using CardHolder.Utility.Enums;
using System.Web.Script.Serialization;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
//using static CardHolder.DataContractJsonSerializer;

namespace CardHolder
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class Login : System.Web.UI.Page
    {

        #region variable

        //For Request.query String 1 use Constants.BlockedAccount
        //For Request.query String 2 use Constants.InactiveAccount
        //For Request.query String 3 use Constants.AccNotInNormalState
        //For Request.query String 4 use Constants.ContinuesBlockedAccount
        //For Request.query String 5 use Constants.InactiveAttempts

        //string CreditAccNumber = CardHolderManager.GetLoggedInUser().creditcard_acc_number; //Added by Sahil on 22'Dec14
        //string CreditCardNumber = CardHolderManager.GetLoggedInUser().CH_Card.card_number.Encrypt(); //Added by Sahil on 22'Dec14
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ClearControls();
            DisplayErrorMessage();


        }

        /// <summary>
        /// Handles the Click event of the btnEnter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (IsValidInfo())
            {
                //start building recaptch api call
                var sb = new StringBuilder();
                sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");

                //our secret key
                var secretKey = "6LfCPbsUAAAAALvGNtSqXRZwX1dp0xUZhd0AIbUT";
                sb.Append(secretKey);

                //response from recaptch control
                sb.Append("&");
                sb.Append("response=");
                var reCaptchaResponse = Request["g-recaptcha-response"];
                sb.Append(reCaptchaResponse);

                //client ip address
                //---- This Ip address part is optional. If you donot want to send IP address you can
                //---- Skip(Remove below 4 lines)
                sb.Append("&");
                sb.Append("remoteip=");
                //var clientIpAddress = GetUserIp();
                //sb.Append(clientIpAddress);

                //make the api call and determine validity
                using (var client = new WebClient())
                {
                    var uri = sb.ToString();
                    var json = client.DownloadString(uri);
                    var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
                    var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                    var result = serializer.ReadObject(ms) as RecaptchaApiResponse;


                    //--- Check if we are able to call api or not.
                    if (result == null)
                    {
                        lblMessage.Text = "Captcha was unable to make the api call";
                    }
                    else // If Yes
                    {
                        //api call contains errors
                        if (result.ErrorCodes != null)
                        {
                            if (result.ErrorCodes.Count > 0)
                            {
                                foreach (var error in result.ErrorCodes)
                                {
                                    lblMessage.Text = "reCAPTCHA Error: " + error;
                                }
                            }
                        }
                        else //api does not contain errors
                        {
                            if (!result.Success) //captcha was unsuccessful for some reason
                            {
                                lblMessage.Text = "Captcha did not pass, please try again.";
                            }
                            else //---- If successfully verified. Do your rest of logic.
                            {
                                lblMessage.Text = "Captcha cleared ";
                            }
                        }

                    }

                }






                Session["CardHolderId"] = "";
                lblMessage.Text = "";
                DivMessage.Attributes.CssStyle.Add("display", "none");
                //viewUserLoginError.Text = "";
                bool ChkActiveUser = false;
                bool UserStatus = false;
                DateTime InvalidLoginDate;
                DateTime TodayDate;
                TimeSpan Diffrence;
                int DurationforActive = 24;
                CardHolderManager am = new CardHolderManager();
                CardManager cardManager = new CardManager();
                string PublicIP = Request.UserHostAddress;

                if (!cardManager.CheckOracleConnection())
                {
                    LblErrorMessage.Text = Constants.DbConnectionNotAvailable;
                    DivERROR.Attributes.CssStyle.Add("display", "block");
                    return;
                }

                //commented by abhijeet on 23/01/2019
                //CardHolder_MstDTO user = am.FindUser(txtCheckUsername.Text.Trim(), PublicIP);
                CardHolder_MstDTO user = am.FindActiveUser(txtCheckUsername.Text.Trim());
                if (user != null)
                {
                    UserStatus = cardManager.AuthenticateUserStatus(user.creditcard_acc_number.Decrypt());

                    if (UserStatus == true)
                    {

                        InvalidLoginDate = Convert.ToDateTime(user.InvalidLastLoginDt);
                        TodayDate = System.DateTime.Now;
                        Diffrence = TodayDate - InvalidLoginDate;

                        int pendingtime = 24 - Convert.ToInt32(Diffrence.TotalHours);
                        string[] parts = Convert.ToString(pendingtime).Split('.');
                        DurationforActive = int.Parse(parts[0]);

                        if (Diffrence.TotalHours >= 24)
                        {
                            ChkActiveUser = am.SetCardHolderActive(user.CardHolder_Id);
                        }

                        if (ChkActiveUser == true)
                        {
                            if (user.IsPermanentDisable == true)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                                                                    "alert('" + Constants.BlockedAccount + "');", true);
                                ClearControls();
                            }
                            else
                            {
                                //mvCheckUser.ActiveViewIndex = 1;
                                //lblPersonalMessage.Text = user.Personal_Msg;
                                //txtUsername.Text = user.User_nm;
                                Session["CardHolderId"] = user.CardHolder_Id;
                                txtCheckUsername.Text = "";
                                txtCaptchaFirst.Text = "";
                                Response.Redirect("~/LoginNext.aspx");  //Redirect to next login here

                            }
                        }

                        else
                        {

                            if (user.IsPermanentDisable == true && user.IsActive == false)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                                                                    "alert('" + Constants.BlockedAccount + "');", true);
                                ClearControls();
                            }
                            else if (user.IsPermanentDisable == true)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                                                                    "alert('" + Constants.BlockedAccount + "');", true);
                                ClearControls();
                            }
                            else if (user.IsActive == false)
                            {
                                if (DurationforActive == 0)
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                                                                        "alert('" + Constants.InactiveAccountAfter +
                                                                        "sometime');", true);
                                else
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert",
                                                                        "alert('" + Constants.InactiveAccountAfter +
                                                                        DurationforActive + "hrs');", true);
                                ClearControls();
                            }
                            else
                            {
                                //mvCheckUser.ActiveViewIndex = 1;
                                //lblPersonalMessage.Text = user.Personal_Msg;
                                //txtUsername.Text = user.User_nm;
                                //Session["CardHolderId"] = user.CardHolder_Id;
                                //txtUsername.Attributes.Add("readonly", "readonly");

                                Session["CardHolderId"] = user.CardHolder_Id;
                                txtCheckUsername.Text = "";
                                txtCaptchaFirst.Text = "";
                                Response.Redirect("~/LoginNext.aspx");

                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "VKeyboard", "init()", true);
                            }
                        }
                        // CreateRequest();
                    }
                    else
                    {
                        lblMessage.Text = Constants.AccNotInNormalState;
                        DivMessage.Attributes.CssStyle.Add("display", "block");
                    }
                }
                else
                {
                    lblMessage.Text = Constants.UnameNtExist;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                }
            }
        }

        /// <summary>
        /// This method is used fro creating reque
        /// </summary>
        //private void CreateRequest()
        //{
        //    string result = string.Empty;
        //    Helper objHelper = new Helper();
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    CommonRequest objCardBlockRequest = new CommonRequest()
        //    {
        //        TxnType = TranscationType.PA.ToString(),
        //        CardNumber = Convert.ToString(CreditCardNumber.Decrypt()),
        //        TransRefNo = objHelper.RandomDigits(10),
        //        TransDateTime = String.Format("{0:MM/dd/yyyy}", DateTime.Now)             
        //    };
        //    string jsondata = js.Serialize(objCardBlockRequest);
        //    result = objHelper.GetResponse(jsondata);
        //    dynamic objResult = js.Deserialize<dynamic>(result);
        //   // DisplayMessage(objResult);
        //}


        //private void DisplayMessage(dynamic result)
        //{
        //    if (result["RespCode"] == "000")
        //    {
        //        lblSuccessMessage.Text = Constants.DataSuccess;
        //    }
        //    else if (result["RespCode"] == "100")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode100;
        //    }
        //    else if (result["RespCode"] == "101")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode101;
        //    }
        //    else if (result["RespCode"] == "102")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode102;
        //    }
        //    else if (result["RespCode"] == "103")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode103;
        //    }
        //    else if (result["RespCode"] == "200")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode200;
        //    }
        //    else if (result["RespCode"] == "201")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode201;
        //    }
        //    else if (result["RespCode"] == "202")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode202;
        //    }
        //    else if (result["RespCode"] == "203")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode203;
        //    }
        //    else if (result["RespCode"] == "204")
        //    {
        //        lblErrorMessage.Text = Constants.ErrorCode204;
        //    }
        //}
        /// <summary>
        /// Determines whether [is valid info].
        /// </summary>
        /// <returns><c>true</c> if [is valid info]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        private bool IsValidInfo()
        {
            if (!Page.IsValid)
                return false;

            try
            {
                //string captchaFrst = txtCaptchaFirst.Text.Trim();
                //if (!string.IsNullOrEmpty(captchaFrst) && captchaFrst.Length == 6)
                //{
                //    //  CaptchaFirst.ValidateCaptcha(captchaFrst);
                //    if (captchaFrst != "" && Convert.ToString(Session["strRandomCH"]) == captchaFrst)
                return true;
                //    else
                //    {
                //        lblMessage.Text = Constants.InvalidCaptcha;
                //        DivMessage.Attributes.CssStyle.Add("display", "block");
                //        txtCaptchaFirst.Text = string.Empty;
                //        txtCaptchaFirst.Focus();
                //        //mvCheckUser.ActiveViewIndex = 0;
                //        return false;
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = Constants.InvalidCaptcha;
                //    DivMessage.Attributes.CssStyle.Add("display", "block");
                //    txtCaptchaFirst.Text = string.Empty;
                //    txtCaptchaFirst.Focus();
                //    //mvCheckUser.ActiveViewIndex = 0;
                //    return false;
                //}             
            }
            catch (Exception ex)
            {
                lblMessage.Text = Constants.InvalidCaptchaExpired;
                DivMessage.Attributes.CssStyle.Add("display", "block");
                txtCaptchaFirst.Text = string.Empty;
                txtCaptchaFirst.Focus();
                return false;
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        /// <remarks></remarks>
        private void ClearControls()
        {
            // mvCheckUser.ActiveViewIndex = 0;
            txtCheckUsername.Text = string.Empty;
            txtCaptchaFirst.Text = string.Empty;
            //lblPersonalMessage.Text = string.Empty;
            //txtUsername.Text = string.Empty;
            Session["CardHolderId"] = string.Empty;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void DisplayErrorMessage()
        {
            if (Request.Params["inmsg"] != null)
            {
                string BlockedAccount = Convert.ToString(Utility.Enums.ErrorStatus.BlockedAccount);
                string InactiveAccount = Convert.ToString(Utility.Enums.ErrorStatus.InactiveAccount);
                string AccNotInNormalState = Convert.ToString(Utility.Enums.ErrorStatus.AccNotInNormalState);
                string ContinuesBlockedAccount = Convert.ToString(Utility.Enums.ErrorStatus.ContinuesBlockedAccount);
                string InactiveAttempts = Convert.ToString(Utility.Enums.ErrorStatus.InactiveAttempts);

                string Code = Request.Params["inmsg"].ToString().DecryptURL();

                if (Code == BlockedAccount)
                {
                    lblMessage.Text = Constants.BlockedAccount;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                }
                else if (Code == InactiveAccount)
                {
                    lblMessage.Text = Constants.InactiveAccount;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                }
                else if (Code == AccNotInNormalState)
                {
                    lblMessage.Text = Constants.AccNotInNormalState;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                }
                else if (Code == ContinuesBlockedAccount)
                {
                    lblMessage.Text = Constants.ContinuesBlockedAccount;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                }
                else if (Code == InactiveAttempts)
                {
                    lblMessage.Text = Constants.InactiveAttempts;
                    DivMessage.Attributes.CssStyle.Add("display", "block");
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    DivMessage.Attributes.CssStyle.Add("display", "none");
                }
            }

            //if (PreviousPage != null && PreviousPage.IsCrossPagePostBack)
            //{
            //    Control placeHolder = PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
            //    HiddenField SourceMsg = (HiddenField)placeHolder.FindControl("hdnErrormsgFromLoginNext");
            //    if (SourceMsg != null && SourceMsg.Value != "")
            //    {
            //        viewCheckUsernameError.Text = SourceMsg.Value;
            //        Session["CardHolderId"] = string.Empty;

            //    }
            //}
        }

        #region google Recpatcha

        #endregion
    }
}