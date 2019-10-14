<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/Simaple.Master"
    AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="CardHolder.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        ul.addUser li span.Classwidth {
            width: 175px !important;
        }

        .loginWrap ul label, .merchloginWrap ul label {
            text-align: left !important;
            width: 162px !important;
        }

        div.form {
            margin-left: 90px;
            margin-top: 15px;
            text-align: left;
        }

        ul.addUser li span.left {
            padding: 3px 21px 0 0 !important;
        }

        .appFormsubmtpop {
            width: 450px !important;
        }
    </style>
    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"
        async defer></script>
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "Login.aspx/VerifyCaptcha",
                        data: "{response: '" + response + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            var captchaResponse = jQuery.parseJSON(r.d);
                            if (captchaResponse.success) {
                                $("[id*=txtCaptcha]").val(captchaResponse.success);
                                $("[id*=rfvCaptcha]").hide();
                            } else {
                                $("[id*=txtCaptcha]").val("");
                                $("[id*=rfvCaptcha]").show();
                                var error = captchaResponse["error-codes"][0];
                                $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                            }
                        }
                    });
                }
            });
        };
    </script>
    <script type="text/javascript">

        ///---
        /// Credit Card Number Validation
        ///---
        function CreditCardValidate(sender, args) {
            var txt1 = document.getElementById("<%= FirstFour.ClientID %>");
            var txt2 = document.getElementById("<%= SecondFour.ClientID%>");
            var txt3 = document.getElementById("<%= ThirdFour.ClientID%>");
            var txt4 = document.getElementById("<%= ForthFour.ClientID%>");

            var ccNumber = txt1.value + txt2.value + txt3.value + txt4.value;
            if (ccNumber.length != 16) {
                args.IsValid = false;
            }
        }

        ///---
        /// Event Handler Settings
        ///---
        $(document).ready(function () {


            <%--$('#button-re-enter-new-pin').hover(function show() {
                var x = $("#ContentPlaceHolder1_txtConfirmPassword");
                x.attr('type', 'text');
                $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-close.svg") %>">');
             },
                 function () {
                     var x = $("#ContentPlaceHolder1_txtConfirmPassword")
                     //Change the attribute back to password  
                     x.attr('type', 'password');
                     $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">');
                });--%>

            $(".dp").datepicker({
                constrainInput: true,
                showOn: 'button'
            });

            ///---
            /// Credit Card Number Skipping
            ///---
            $("input[id$='FirstFour']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
                if ($(this).val().length == 4) {
                    $("input[id$='SecondFour']").focus();
                }
            });
            ///---
            /// Credit Card Number Skipping
            ///---
            $("input[id$='SecondFour']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
                if ($(this).val().length == 4) {

                    $("input[id$='ThirdFour']").focus();
                }
            });
            ///---
            /// Credit Card Number Skipping
            ///---
            $("input[id$='ThirdFour']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
                if ($(this).val().length == 4) {

                    $("input[id$='ForthFour']").focus();
                }
            });

            ///---
            /// Read Only
            ///---
            $("input[id$='txtbirthdate']").keydown(function (event) {
                return false;
            });



        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnCard1" />
    <asp:HiddenField runat="server" ID="hdnCard2" />
    <asp:HiddenField runat="server" ID="hdnCard3" />
    <asp:HiddenField runat="server" ID="hdnCard4" />
    <asp:HiddenField runat="server" ID="hdnOTP" />
    <div runat="server" id="divcontent">
        <div class="container-fluid">
            <div class="pre-login-box">
                <div class="row align-items-center">
                    <div class="col-lg-5">
                        <div class="card mb-4">
                            <div class="card-body">
                                <h5 class="card-title text-uppercase">Forgot Password</h5>
                                <asp:MultiView runat="server" ID="mvFrgtPwd" ActiveViewIndex="0">
                                    <asp:View ID="viewCard" runat="server">
                                        <%--<asp:Label ID="lblStep1Message" runat="server" CssClass="error" />--%>
                                        <div class="col-md-12">
                                            <div class="alert alert-danger" id="DivStep1Message" runat="server" style="display: none">
                                                <asp:Label ID="lblStep1Message" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" role="alert" id="DivStep1ERROR" runat="server" style="display: none">
                                                <figure class="icon mr-2">
                                                    <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                                                </figure>
                                                <asp:Label ID="LblStep1ErrorMessage" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="">Primary Card Number</label>
                                            <div class="row">
                                                <div class="col">
                                                    <asp:TextBox runat="server" ID="FirstFour" MaxLength="4" CssClass="form-control form-control-lg CardNumber"></asp:TextBox>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox runat="server" ID="SecondFour" MaxLength="4" CssClass="form-control form-control-lg CardNumber"></asp:TextBox>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox runat="server" ID="ThirdFour" MaxLength="4" CssClass="form-control form-control-lg CardNumber"></asp:TextBox>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox runat="server" ID="ForthFour" MaxLength="4" CssClass="form-control form-control-lg CardNumber"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:CustomValidator CssClass="error ErrorCard" EnableClientScript="true" ValidationGroup="ValidFirst"
                                                ID="cvCreditCard" runat="server" ErrorMessage="Please enter valid credit card number"
                                                ClientValidationFunction="CreditCardValidate" ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label for="">Expiry Date</label>
                                                    <div class="input-group">
                                                        <asp:DropDownList runat="server" CssClass="form-control form-control-lg  custom-select first-input" ID="ddlmonth">
                                                            <asp:ListItem Text="MM" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Value="01"></asp:ListItem>
                                                            <asp:ListItem Value="02"></asp:ListItem>
                                                            <asp:ListItem Value="03"></asp:ListItem>
                                                            <asp:ListItem Value="04"></asp:ListItem>
                                                            <asp:ListItem Value="05"></asp:ListItem>
                                                            <asp:ListItem Value="06"></asp:ListItem>
                                                            <asp:ListItem Value="07"></asp:ListItem>
                                                            <asp:ListItem Value="08"></asp:ListItem>
                                                            <asp:ListItem Value="09"></asp:ListItem>
                                                            <asp:ListItem Value="10"></asp:ListItem>
                                                            <asp:ListItem Value="11"></asp:ListItem>
                                                            <asp:ListItem Value="12"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList runat="server" CssClass="form-control form-control-lg  custom-select" ID="ddlyear">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <asp:RequiredFieldValidator CssClass="error ErrorCard" ValidationGroup="ValidFirst"
                                                        ID="rfvmonth" runat="server" ControlToValidate="ddlmonth" Display="Dynamic" ErrorMessage="Please select Month, "
                                                        InitialValue="-1"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator CssClass="error ErrorCard" ID="rfvYear" runat="server"
                                                        ControlToValidate="ddlyear" Display="Dynamic" ValidationGroup="ValidFirst" ErrorMessage="Please select Year"
                                                        InitialValue="-1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <label for="txtbirthdate">Date of Birth</label>
                                                <div class="input-group  input-group-lg date-input-group dob-width">
                                                    <asp:TextBox ID="txtbirthdate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                    <div class="input-group-append">
                                                        <span class="input-group-text" id="">
                                                            <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>">
                                                        </span>
                                                    </div>
                                                </div>
                                                <asp:RequiredFieldValidator runat="server" ID="reqOBIRTH_DATE" EnableClientScript="true"
                                                    CssClass="error ErrorCard" ControlToValidate="txtbirthdate" ValidationGroup="ValidFirst"
                                                    ErrorMessage="Please select birth date" Display="Dynamic" />
                                            </div>
                                        </div>

                                        <div class="form-group" style="display: none">
                                            <asp:Image ID="Image2" runat="server" Style="border-width: 0px;" ImageUrl="~/Captchaa.aspx"
                                                AlternateText="Captcha" CssClass="cptimg" />
                                            <asp:Label runat="server" AssociatedControlID="txtCaptchaFirst" Style="float: right;"></asp:Label>
                                            <asp:TextBox ID="txtCaptchaFirst" MaxLength="6" CssClass="Alphanumericsonly form-control form-control-lg" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvCaptchaFirst" ValidationGroup="ValidFirst"
                                                EnableClientScript="true" CssClass="error" ControlToValidate="txtCaptchaFirst"
                                                ErrorMessage="Please enter captcha code" Display="Dynamic" />
                                        </div>
                                        <div class="form-group">
                                            <div id="dvCaptcha">
                                            </div>
                                            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                                            <asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha"
                                                runat="server" CssClass="error" Display="Dynamic" ValidationGroup="ValidFirst" />
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                                                    OnClick="btnReset_Click" />
                                            </div>
                                            <div class="col">
                                                <asp:Button runat="server" ID="btnContinue" Text="Proceed" ValidationGroup="ValidFirst"
                                                    CssClass="btn btn-lg  btn-primary text-uppercase btn-block" />
                                            </div>
                                        </div>



                                        <div class="stepper-view">
                                            <ul>
                                                <li class="active"></li>
                                                <li></li>
                                                <li></li>
                                            </ul>
                                        </div>



                                    </asp:View>
                                    <asp:View ID="viewOTP" runat="server">
                                        <div class="col-md-12">
                                            <%--<asp:Label runat="server" ID="lblOTPMessage" CssClass="error"></asp:Label>--%>
                                            <div class="alert alert-danger" id="DivOTPMessage" runat="server" style="display: none">
                                                <asp:Label ID="LabelOTPMessage" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="alert alert-danger" role="alert" id="DivOTPErrorMessage" runat="server" style="display: none">
                                                <figure class="icon mr-2">
                                                    <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                                                </figure>
                                                <asp:Label ID="LabelOTPErrorMessage" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="">Mobile Number</label>
                                            <asp:TextBox ID="txtMobileNo" runat="server" class="form-control form-control-lg"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="">OTP</label>
                                            <asp:TextBox runat="server" ID="txtOTP" MaxLength="15" ValidationGroup="OptValid" CssClass="form-control form-control-lg" />
                                            <asp:RequiredFieldValidator runat="server" ID="rfvOTP" EnableClientScript="true"
                                                CssClass="error ErrorCard" ControlToValidate="txtOTP" ValidationGroup="OptValid"
                                                ErrorMessage="Please enter OTP" Display="Dynamic" />
                                            <div class="col-auto" id="divremaining" runat="server">
                                                <small id="Small1" class="form-text"><span id="timer"></span>remaining</small>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <asp:LinkButton runat="server" ID="linkRegenerateOTP" CausesValidation="false" Text="Regenerate OTP"
                                                    OnClick="linkRegenerateOTP_Click" CssClass="btn btn-lg btn-outline-primary  text-uppercase btn-block"></asp:LinkButton>
                                            </div>
                                            <div class="col">
                                                <asp:Button runat="server" ID="btnOTPContinue" ValidationGroup="OptValid" Text="Proceed"
                                                    OnClick="btnOTPContinue_Click" CssClass="btn btn-lg btn-primary btn-block text-uppercase" />
                                            </div>
                                        </div>


                                        <div class="stepper-view">
                                            <ul>
                                                <li class="active"></li>
                                                <li class="active"></li>
                                                <li></li>
                                            </ul>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="ViewCPwd" runat="server">
                                        <div class="col-md-12">
                                            <div class="alert alert-danger" id="DivPwd" runat="server" style="display: none">
                                                <asp:Label ID="lblErrpwd" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <%--<asp:Label ID="lblErrpwd" runat="server" ForeColor="Red"></asp:Label>--%>
                                        <asp:Panel ID="PanelCpwd" runat="server" DefaultButton="btnchPwd">
                                            <div class="form-group">
                                                <label for="txtNewPassword">New password</label>
                                                <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="validatorInput form-control form-control-lg" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqNewPassword" Display="Dynamic"
                                                    ControlToValidate="txtNewPassword" CssClass="error ErrorCard" ValidationGroup="save"
                                                    ErrorMessage="Please enter new password" />
                                                <asp:RegularExpressionValidator ID="revPasswordValidator" ValidationGroup="save"
                                                    runat="server" Display="Dynamic" CssClass="error ErrorCard" EnableClientScript="true"
                                                    ControlToValidate="txtNewPassword" ValidationExpression="(?=^.{10,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                                                    ErrorMessage="Password at least 10 characters, has at least one lower case letter, one upper case letter, one digit or special character" />
                                            </div>

                                            <div class="form-group">
                                                <label for="txtConfirmPassword">Confirm password</label>
                                                <div class="input-group custom-input-group">
                                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="validatorInput form-control form-control-lg"
                                                        aria-describedby="button-re-enter-new-pin" runat="server"></asp:TextBox>
                                                    <div class="input-group-append">
                                                        <button class="btn btn-outline-secondary" type="button" id="button-re-enter-new-pin" onclick="ShowHidePassword()">
                                                            <img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">
                                                        </button>
                                                    </div>
                                                </div>
                                                <asp:CompareValidator ID="cmpConfirmPassowrd" Display="dynamic" CssClass="error ErrorCard"
                                                    ControlToValidate="txtConfirmPassword" ValidationGroup="save" Operator="Equal"
                                                    ControlToCompare="txtNewPassword" Type="String" EnableClientScript="true" Text="Password not matched"
                                                    runat="server" />
                                                <asp:RequiredFieldValidator runat="server" CssClass="error ErrorCard" Display="Dynamic"
                                                    ID="reqConfirmPassword" ValidationGroup="save" ControlToValidate="txtConfirmPassword"
                                                    ErrorMessage="Please enter confirm password" />
                                            </div>

                                            <div class="row">
                                                <%--<div class="col">
                                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" Text="Cancel" CausesValidation="false"
                                                        OnClick="btncancel_Click" />
                                                </div>--%>
                                                <div class="col">
                                                    <asp:Button ID="btnchPwd" runat="server" CssClass="btn btn-lg  btn-primary text-uppercase  btn-block" ValidationGroup="save"
                                                        Text="Change Password" OnClick="btnchPwd_Click" />
                                                </div>
                                            </div>





                                        </asp:Panel>

                                        <div class="stepper-view">
                                            <ul>
                                                <li class="active"></li>
                                                <li class="active"></li>
                                                <li class="active"></li>
                                            </ul>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 text-center d-lg-block d-none">
                        <figure>
                            <img src="<%= this.Page.GetNewImagePath("Loginbanner.svg") %>" alt="banner" class="banner-registration-image LoginBannerImage" />
                        </figure>
                    </div>
                </div>
            </div>
        </div>

        <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button" OnClick="btnSubmitfinal_Click"
            Text="Submit" Style="display: none" />

        <%--<div class="loginSec">
            <div class="merchloginWrap shadow clearfix">
                <div class="fLeft">
                    
                    <!-- POPUP content are here -->
                    <div id="popup_box" style="width: 460px; height: auto">
                        <a id="popupBoxClose" class="popClosebtn"></a>
                        <div class="appFormsubmtpop" style="padding: 0px;">
                            <center>
                                <h3>
                                    Are you sure you want to continue?
                                </h3>
                                <div class="pt10 wd126">
                                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button" OnClick="btnSubmitfinal_Click"
                                        Text="Submit" />
                                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <script type="text/javascript">
        function Showalert() {
            alert('Congratulations! Password has been successfully changed');
            window.location = 'login.aspx';
        }
    </script>
    <script type="text/javascript">
        function ValidateThisForm(frm) {
            if (Page_ClientValidate("save")) {
                if ($("#<%= txtNewPassword.ClientID %>").length > 0) {

                    var pwd = document.getElementById('<%= txtNewPassword.ClientID %>').value;

                    if (pwd.length == 0) {
                        alert("Please enter password.");
                        return false;
                    }

                    msg = "";
                    if (pwd.length < 8) msg = "\nPassword has to be at least eight characters long.";
                    inval = pwd.replace(/[a-z0-9\!\@\$\%\&\/\(\)\=\?\+\*\#\-\.\,\;\:\_]/gi, '');
                    if (inval > '') msg += '\nInvalid character in password: ' + inval;
                    grp = 0;
                    if (pwd.match(/[a-z]/)) grp++;
                    if (pwd.match(/[A-Z]/)) grp++;
                    if (pwd.match(/[0-9]/)) grp++;
                    if (pwd.match(/[\!\@\$\%\&\/\(\)\=\?\+\*\#\-\.\,\;\:\_]/)) grp++;
                    if (grp < 3) msg += "\nPlease set password with following requirements(At least three criteria should comply):\n" +
                        "\n\tLowerCase characters: a-z" +
                        "\n\tUpperCase characters: A-Z" +
                        "\n\tDigits: 0-9" +
                        "\n\tSpecial Characters like: !@$%&/()=?+*#-.,;:_";
                    if (msg > "") {
                        alert("Password invalid." + msg);
                        document.getElementById('<%= txtNewPassword.ClientID %>').value = "";
                        document.getElementById('<%= txtConfirmPassword.ClientID %>').value = "";
                        return false;
                    }
                    else {
                        EncryptPassword();
                        EncryptRePassword();
                        return true;
                    }
                }

                return false;
            }
        }


        function EncryptPassword() {
            var pwd = document.getElementById('<%=txtNewPassword.ClientID %>');
            if (pwd.value != '') {
                enc = hex_md5(pwd.value);
                pwd.value = enc;
            }
        }

        function EncryptRePassword() {
            var pwd = document.getElementById('<%=txtConfirmPassword.ClientID %>');
            if (pwd.value != '') {
                enc = hex_md5(pwd.value);
                pwd.value = enc;
            }
        }

        function ShowHidePassword() {
            
            var x = document.getElementById('<%=txtConfirmPassword.ClientID %>');
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        $(document).ready(function () {
            var val1 = document.getElementById("<%= FirstFour.ClientID %>");
            var val2 = document.getElementById("<%= SecondFour.ClientID%>");
            var val3 = document.getElementById("<%= ThirdFour.ClientID%>");
            var val4 = document.getElementById("<%= ForthFour.ClientID%>");
            var dob = document.getElementById("<%= txtbirthdate.ClientID%>");
            var month = document.getElementById("<%= ddlmonth.ClientID%>");
            var year = document.getElementById("<%= ddlyear.ClientID%>");
            var captcha = document.getElementById("<%= txtCaptchaFirst.ClientID%>");

            if (typeof String.prototype.trim !== 'function') {
                String.prototype.trim = function () {
                    return this.replace(/^\s+|\s+$/g, '');
                };
            }

            //$('#popupBoxClose').click(function () {
            //    unloadPopupBox();
            //});
            //$("input[id$='btnNo']").click(function () {
            //    unloadPopupBox();
            //});

            //function unloadPopupBox() {
            //    $('#overlay').remove();
            //    $('#popup_box').fadeOut("slow");
            //    val1.value = '';
            //    val2.value = '';
            //    val3.value = '';
            //    val4.value = '';
            //    dob.value = '';
            //    month.value = '-1';
            //    year.value = '-1';
            //    captcha.value = '';

            //    window.__doPostBack('btnReset', "Click");

            //}

            //function loadPopupBox() {
            //    $('#popup_box').fadeIn("slow");
            //}

            function Ajaxcall(val1, val2, val3, val4) {
                $.ajax({
                    url: "Registration.aspx/DoMethodEnc",
                    data: '{"val1": "' + val1 + '","val2": "' + val2 + '","val3": "' + val3 + '","val4": "' + val4 + '"}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        if (data.d != "") {
                            var strings = data.d.split(",");
                            $("#ContentPlaceHolder1_hdnCard1").val(strings[0]);
                            $("#ContentPlaceHolder1_hdnCard2").val(strings[1]);
                            $("#ContentPlaceHolder1_hdnCard3").val(strings[2]);
                            $("#ContentPlaceHolder1_hdnCard4").val(strings[3]);
                            $("#ContentPlaceHolder1_FirstFour").val('xxxx');
                            $("#ContentPlaceHolder1_SecondFour").val('xxxx');
                            $("#ContentPlaceHolder1_ThirdFour").val('xxxx');
                            $("#ContentPlaceHolder1_ForthFour").val('xxxx');
                        }
                        else {
                            $("#ContentPlaceHolder1_FirstFour").val(val1);
                            $("#ContentPlaceHolder1_SecondFour").val(val2);
                            $("#ContentPlaceHolder1_ThirdFour").val(val3);
                            $("#ContentPlaceHolder1_ForthFour").val(val4);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
                //                return false;

            }

            $("input[id$='btnContinue']").click(function () {

                var ccNumber = val1.value + val2.value + val3.value + val4.value;
                if (ccNumber.length != 16 || dob.value.trim() == "" || month.value == "-1" || year.value == "-1" || captcha.value == "") {
                    return false;
                }


                if (val1.value.trim() != "" && val2.value.trim() != "" && val3.value.trim() != "" && val4.value.trim() != "") {
                    Ajaxcall(val1.value, val2.value, val3.value, val4.value);
                    $("#<%=btnSubmitfinal.ClientID%>").click();
                    //var docHeight = $(document).height();
                    //$("body").append("<div id='overlay'></div>");
                    //$("#overlay").height(docHeight).css({
                    //    'opacity': 0.4,
                    //    'position': 'absolute',
                    //    'top': 0,
                    //    'left': 0,
                    //    'background-color': 'black',
                    //    'width': '100%',
                    //    'z-index': 5000
                    //});
                    //loadPopupBox();
                }
                else {
                    val1.value = "";
                    val2.value = "";
                    val3.value = "";
                    val4.value = "";
                }
                return false;
            });
        });

        var date = new Date();
        var currentMonth = date.getMonth();
        var currentDate = date.getDate();
        var currentYear = date.getFullYear();
        $('[id*=txtbirthdate]').datepicker({
            autoclose: true,
            startDate: new Date(currentYear - 100, currentMonth, currentDate),
            endDate: new Date(currentYear - 18, currentMonth, currentDate),
            format: 'dd/mm/yyyy',
            orientation: "bottom"
        });

        var ATMOTPTimer;
        let timerOn = true;
        function timer(remaining) {
            var m = Math.floor(remaining / 60);
            var s = remaining % 60;

            m = m < 10 ? '0' + m : m;
            s = s < 10 ? '0' + s : s;

            var remainingSec = m + ':' + s;
            document.getElementById("timer").innerHTML = remainingSec;

            remaining -= 1;

            if (remaining >= 0 && timerOn) {
                ATMOTPTimer = setTimeout(function () {
                    timer(remaining);
                }, 1000);
                return;
            }

            if (!timerOn) {
                // Do validate stuff here
                return;
            }

            // Do timeout stuff here
            //alert('Timeout for otp');
            $('#<%=LabelOTPMessage.ClientID%>').html("Timeout for OTP");
                           <%-- $('#<%=divInvalidfeedback.ClientID%>').css('display', 'block');--%>
                            $('#<%=DivOTPMessage.ClientID%>').css('display', 'block');
            $("#ContentPlaceHolder1_hdnOTP").val("");
        }

        function StopFunction() {
            clearTimeout(ATMOTPTimer);
        }

    </script>
</asp:Content>

