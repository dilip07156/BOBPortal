<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<%@ Page Language="C#" Title="Card-Holder Registration" MasterPageFile="~/Simaple.Master"
    AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="CardHolder.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"
        async defer></script>
    <script type="text/javascript">
        var onloadCallback = function () {            
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {                      
                    $.ajax({
                        type: "POST",
                        url: "Registration.aspx/VerifyCaptcha",                      
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

        function ValidateThisForm(frm) {
            if (Page_ClientValidate("save")) {
                if ($("#<%= txtPassword.ClientID %>").length > 0) {

                    var pwd = document.getElementById('<%= txtPassword.ClientID %>').value;

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
                        document.getElementById('<%= txtPassword.ClientID %>').value = "";
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
            var pwd = document.getElementById('<%=txtPassword.ClientID %>');
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

    </script>
    <script type="text/javascript">

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }

        ///---
        /// File Upload Type
        ///---
        function FileUploadType(sender, e) {
            var fupData = jQuery("input[id$='photoUpload']");
            var fileUploadPath = fupData.val();
            if (fileUploadPath == '') {
                //alert("Please select file to upload");
            } else {
                var extension = fileUploadPath.substring(fileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (extension == "gif" || extension == "jpeg" || extension == "jpg" || extension == "png" || extension == "doc" || extension == "docx" || extension == "xls" || extension == "xlsx") {
                    e.IsValid = true;
                } else {
                    e.IsValid = false;
                }
            }
        }

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
        /// Image File Size Validation
        ///---
        var size = 0;
        function FileUploadSize(sender, e) {
            if (size > 20000) {
                e.IsValid = false;
            } else {
                e.IsValid = true;
            }
        }

        ///---
        /// Event Handler Settings
        ///---
        $(document).ready(function () {
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
            /// Credit Card Number Skipping
            ///---
            $("input[id$='ForthFour']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });


            ///---
            /// Only Numbers
            ///---
            $("input[id$='txtMobile']").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }

            });


            ///---
            /// Read Only
            ///---
            $("input[id$='txtbirthdate']").keydown(function (event) {
                return false;
            });


            ///---
            /// File Upload Change
            ///---
            $("input[id$='photoUpload']").change(function () {
                var f = this.files[0];
                size = f.size;
            });

            $('.custom-checkbox .ChkReglabel > input').addClass('custom-control-input');
            $('.custom-checkbox .ChkReglabel > label').addClass('custom-control-label');
        });

    </script>
    <style type="text/css">
        ul.MerchantaddUser li span.left {
            width: 180px !important;
        }

        ul.addUser li span.left {
            padding: 3px 27px 0 0 !important;
        }

        .appFormsubmtpop {
            width: 450px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:HiddenField runat="server" ID="hdnCard1" />
        <asp:HiddenField runat="server" ID="hdnCard2" />
        <asp:HiddenField runat="server" ID="hdnCard3" />
        <asp:HiddenField runat="server" ID="hdnCard4" />
        <asp:HiddenField runat="server" ID="hdnOTP" />

        <div class="pre-login-box">
            <div class="row align-items-center">
                <div class="col-lg-5">
                    <div class="card mb-4">
                        <div class="card-body">
                            <h5 class="card-title text-uppercase">Register Your Card</h5>
                            <asp:MultiView runat="server" ID="mvNewUserRegistration" ActiveViewIndex="0">
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
                                        <asp:CustomValidator CssClass="error ErrorCard" ValidationGroup="ValidFirst" EnableClientScript="true"
                                            ID="cvCreditCard" runat="server" ErrorMessage="Please enter valid credit card number"
                                            ClientValidationFunction="CreditCardValidate" ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="">Expiry Date</label>
                                                <div class="input-group">
                                                    <asp:DropDownList runat="server" CssClass="form-control form-control-lg  custom-select  first-input" ID="ddlmonth">
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
                                                    ID="rfvmonth" runat="server" ControlToValidate="ddlmonth" Display="Dynamic" ErrorMessage="Please select Month,"
                                                    InitialValue="-1"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator CssClass="error ErrorCard" ValidationGroup="ValidFirst"
                                                    ID="rfvYear" runat="server" ControlToValidate="ddlyear" Display="Dynamic" ErrorMessage="Please select Year"
                                                    InitialValue="-1"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label for="">Date of Birth</label>
                                            <div class="input-group  input-group-lg date-input-group dob-width">
                                                <asp:TextBox ID="txtbirthdate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <span class="input-group-text" id="">
                                                        <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>">
                                                    </span>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="reqOBIRTH_DATE" ValidationGroup="ValidFirst"
                                                EnableClientScript="true" CssClass="error ErrorCard" ControlToValidate="txtbirthdate"
                                                ErrorMessage="Please select birth date" Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <asp:Image ID="Image2" runat="server" Style="border-width: 0px;" ImageUrl="~/Captchaa.aspx"
                                            AlternateText="Captcha" CssClass="cptimg" />
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
                                    <div class="form-group">
                                        <div class="custom-control custom-checkbox">
                                            <asp:CheckBox runat="server" ID="chkAgree" CssClass="ChkReglabel" Checked="false"
                                                Text="I accept the " />
                                            <a target="_blank" href="terms_conditions.htm#term3">Terms & Conditions</a>
                                        </div>
                                        <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" ValidationGroup="ValidFirst"
                                            EnableClientScript="true" ClientValidationFunction="CheckBoxRequired_ClientValidate"
                                            ErrorMessage="You must select this box to proceed" Display="Dynamic" />
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                                                OnClick="btnReset_Click" />
                                        </div>
                                        <div class="col">
                                            <asp:Button runat="server" ID="btnCardContinue" Text="Proceed" ValidationGroup="ValidFirst"
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
                                        <asp:Label runat="server" Style="display: none; align-self: center" ID="Label1" CssClass="error"></asp:Label>
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
                                <asp:View runat="server" ID="viewInfo">
                                    <div class="col-md-12">
                                        <div class="alert alert-primary" role="alert" id="DivSuccess" runat="server" style="display: none">
                                            <figure class="icon mr-2">
                                                <img src="<%= this.Page.GetNewImagePath("success.svg") %>" alt="info-icon" width="22">
                                            </figure>
                                            <asp:Label ID="LblSuccessMessage" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="alert alert-danger" id="DivPwd" runat="server" style="display: none">
                                            <asp:Label ID="lblpwd" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <%-- <div>
                                        <asp:Label ID="lblStep3Message" runat="server" CssClass="error" />
                                    </div>--%>
                                    <asp:HiddenField ID="lblhdnfullname" runat="server"></asp:HiddenField>
                                    <div class="form-group">
                                        <label for="">User ID</label>
                                        <asp:TextBox runat="server" MaxLength="50" ID="txtUserId" CssClass="form-control form-control-lg" />
                                        <asp:Label ID="lbluserAvailable" runat="server" Text="User is available" Style="display: none; color: Green; font-style: italic; padding-left: 7px;"></asp:Label>
                                        <asp:Label ID="lbluserUnAvailable" runat="server" Text="Username is unavailable"
                                            Style="display: none; color: Red; font-style: italic; padding-left: 7px;"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" EnableClientScript="true"
                                            CssClass="error ErrorCard" ControlToValidate="txtUserId" ErrorMessage="Please enter user id"
                                            Display="Dynamic" ValidationGroup="save" />
                                    </div>
                                    <div class="form-group">
                                        <label for="">Set Password</label>
                                        <asp:TextBox runat="server" ID="txtPassword" MaxLength="50" TextMode="Password" CssClass="form-control form-control-lg" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" EnableClientScript="true"
                                            CssClass="error ErrorCard" ControlToValidate="txtPassword" ErrorMessage="Please enter password"
                                            Display="Dynamic" ValidationGroup="save" />
                                        <asp:RegularExpressionValidator ID="revPasswordValidator" runat="server" Display="Dynamic"
                                            CssClass="error ErrorCard" EnableClientScript="true" ControlToValidate="txtPassword"
                                            ValidationExpression="(?=^.{10,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                                            ErrorMessage="Password at least 10 characters, has at least one lower case letter, one upper case letter, one digit or special character"
                                            ValidationGroup="save" />
                                    </div>
                                    <div class="form-group">
                                        <label for="">Confirm Password</label>
                                        <div class="input-group custom-input-group">
                                            <asp:TextBox runat="server" ID="txtConfirmPassword" MaxLength="50" TextMode="Password" CssClass="form-control form-control-lg" aria-describedby="button-re-enter-new-pin" />
                                            <div class="input-group-append">
                                                <button class="btn btn-outline-secondary" type="button" id="button-re-enter-new-pin" onclick="ShowHidePassword()">
                                                    <img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">
                                                </button>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" EnableClientScript="true"
                                            CssClass="error ErrorCard" ControlToValidate="txtConfirmPassword" ErrorMessage="Please enter confirm password"
                                            Display="Dynamic" ValidationGroup="save" />
                                        <asp:CompareValidator ID="cmpNewPassord" Display="dynamic" ControlToValidate="txtConfirmPassword"
                                            Operator="Equal" ControlToCompare="txtPassword" CssClass="error ErrorCard" Type="String"
                                            EnableClientScript="true" Text="Confirm password is not matched" runat="server" ValidationGroup="save" />
                                    </div>
                                    <div class="form-group">
                                        <label for="">Personal Message</label>
                                        <asp:TextBox runat="server" MaxLength="50" ID="txtPersonalMessage" CssClass="form-control form-control-lg" />
                                        <asp:RequiredFieldValidator CssClass="error" runat="server" ID="reqPersonalMessage"
                                            ControlToValidate="txtPersonalMessage" EnableClientScript="true" ErrorMessage="Please enter personal message" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="error ErrorCard" ID="RegularExpressionValidator2"
                                            EnableClientScript="true" ControlToValidate="txtPersonalMessage" ValidationExpression="^.{1,50}$"
                                            Display="Dynamic" ErrorMessage="Only 50 characters allowed" runat="server" ValidationGroup="save" />

                                    </div>
                                    <asp:Button runat="server" ID="btnSubmit" Text="Confirm" OnClick="btnSubmit_Click"
                                        CssClass="btn btn-lg btn-primary btn-block text-uppercase" ValidationGroup="save" />


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

                    <!--Sign up btn-->
                    <div class="card mb-4 signup-btn">
                        <a href="Login.aspx">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-10">
                                        <h6 class="text-uppercase">Login</h6>
                                        <div>Already have an account with us?</div>
                                    </div>
                                    <div class="col-2 text-right">
                                        <img src="<%= this.Page.GetNewImagePath("Chevron.svg") %>" alt="arrow right" class="chevron-right" />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-6 text-center d-lg-block d-none" style="margin-bottom:-157px;">
                    <figure>
                        <img src="<%= this.Page.GetNewImagePath("Loginbanner.svg") %>" alt="banner" class="banner-registration-image LoginBannerImage" />
                    </figure>
                </div>
            </div>
        </div>
    </div>

    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button" OnClick="btnSubmitfinal_Click"
        Text="Submit" Style="display: none" />
    <%--<div class="chregist" style="padding: 100px">
        
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
    </div>--%>
    <script type="text/javascript">
        function Showalert() {
            alert('Congratulations! Registration has been successfully completed. Kindly login to Continue');
            window.location = 'login.aspx';
        }

        function ShowHidePassword() {
            
            var x = document.getElementById('<%=txtConfirmPassword.ClientID %>');
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        //CheckUser Availibility
        var Username = document.getElementById("ContentPlaceHolder1_txtUserId");

        $(document).ready(function () {

            if (typeof String.prototype.trim !== 'function') {
                String.prototype.trim = function () {
                    return this.replace(/^\s+|\s+$/g, '');
                };
            }


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

            //$('#popupBoxClose').click(function () {
            //    unloadPopupBox();
            //});
            //$("input[id$='btnNo']").click(function () {
            //    unloadPopupBox();
            //});

            //function unloadPopupBox() {
            //    $('#overlay').remove();
            //    //$('#popup_box').fadeOut("slow");
            //    $("#ContentPlaceHolder1_FirstFour").val('');                
            //    $("#ContentPlaceHolder1_ddlmonth").val('-1');
            //    $("#ContentPlaceHolder1_ddlyear").val('-1');
            //    $("#ContentPlaceHolder1_txtbirthdate").val('');
            //    $("#ContentPlaceHolder1_txtCaptchaFirst").val('');
            //    $("#ContentPlaceHolder1_chkAgree").checked = false;

            //    window.__doPostBack('btnReset', "Click");

            //}

            //function loadPopupBox() {
            //    $('#popup_box').fadeIn("slow");
            //}


            $("#ContentPlaceHolder1_txtUserId").blur(function () {
                var uname = $("#ContentPlaceHolder1_txtUserId").val();
                if (uname.trim() != "") {
                    $.ajax({
                        url: "Registration.aspx/FindUser",
                        data: "{ 'username': '" + uname + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            if (data.d == 1) {

                                $("#ContentPlaceHolder1_lbluserUnAvailable").show();
                                $("#ContentPlaceHolder1_lbluserAvailable").hide();
                            }
                            else if (data.d == 0 && uname != "") {

                                $("#ContentPlaceHolder1_lbluserAvailable").show();
                                $("#ContentPlaceHolder1_lbluserUnAvailable").hide();
                            }
                            else {
                                $("#ContentPlaceHolder1_lbluserUnAvailable").hide();
                                $("#ContentPlaceHolder1_lbluserAvailable").hide();
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                }
                else {
                    $("#ContentPlaceHolder1_lbluserUnAvailable").hide();
                    $("#ContentPlaceHolder1_lbluserAvailable").hide();
                }
            });

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

            $("input[id$='btnCardContinue']").click(function () {
                var val1 = document.getElementById("<%= FirstFour.ClientID %>");
                var val2 = document.getElementById("<%= SecondFour.ClientID%>");
                var val3 = document.getElementById("<%= ThirdFour.ClientID%>");
                var val4 = document.getElementById("<%= ForthFour.ClientID%>");
                var dob = document.getElementById("<%= txtbirthdate.ClientID%>");
                var month = document.getElementById("<%= ddlmonth.ClientID%>");
                var year = document.getElementById("<%= ddlyear.ClientID%>");
                var captcha = document.getElementById("<%= txtCaptchaFirst.ClientID%>");
                var Check = document.getElementById("<%= chkAgree.ClientID%>");


                var ccNumber = val1.value + val2.value + val3.value + val4.value;
                if (ccNumber.length != 16 || dob.value.trim() == "" || month.value == "-1" || year.value == "-1" || captcha.value == "" || Check.checked == false) {
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
