<%@ Page Language="C#" Title="Change Password" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="CardHolder.UserManagment.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        //return GetEncrypt();
                        EncryptOldPassword();
                        EncryptPassword();
                        EncryptRePassword();
                        return true;
                    }
                }

                return false;
            }
        }

        function EncryptOldPassword() {
            var pwd = document.getElementById('<%=txtOldPassword.ClientID %>');
            if (pwd.value != '') {
                enc = hex_md5(pwd.value);
                pwd.value = enc;
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



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnOTP" />
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Change Password</h6>
        </div>
        <%--<div class="row">
            
        </div>--%>
        <asp:MultiView runat="server" ID="mvPasswordChange" ActiveViewIndex="0">
            <asp:View ID="step1" runat="server">
                <div class="card-body">
                    <div class="col-md-12">                       
                        <div class="alert alert-danger" role="alert" id="DivERROR" runat="server" style="display: none">
                            <figure class="icon mr-2">
                                <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                            </figure>
                            <asp:Label ID="LblErrorMessage" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="alert alert-danger" id="DivMessage" runat="server" style="display: none">
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>

                    </div>
                    <%-- <div class="row">--%>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label for="">Old password</label>
                            <asp:TextBox ID="txtOldPassword" TextMode="Password" MaxLength="50" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="save" CssClass="error" ID="reqOldPassword" Display="Dynamic"
                                ControlToValidate="txtOldPassword" ErrorMessage="Please enter old password" />
                        </div>
                        <div class="form-group">
                            <label for="">New password</label>
                            <asp:TextBox ID="txtNewPassword" TextMode="Password" MaxLength="50" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                            <div class="errorValidator">
                                <asp:CompareValidator ID="cmpNewPassord" Display="dynamic" ValidationGroup="save" ControlToValidate="txtNewPassword"
                                    Operator="NotEqual" ControlToCompare="txtOldPassword" Type="String"
                                    EnableClientScript="true" CssClass="error" Text="New password should not be equal to new password"
                                    runat="server" />
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="save" CssClass="error" ID="reqNewPassword" Display="Dynamic"
                                    ControlToValidate="txtNewPassword" ErrorMessage="Please enter new password" />
                                <asp:RegularExpressionValidator ValidationGroup="save" ID="revPasswordValidator" runat="server" Display="Dynamic"
                                    CssClass="error" EnableClientScript="true" ControlToValidate="txtNewPassword"
                                    ValidationExpression="(?=^.{10,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                                    ErrorMessage="Password at least 10 characters, has at least one lower case letter, one upper case letter, one digit or special character" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="">Confirm password</label>
                            <div class="input-group custom-input-group">
                                <asp:TextBox ID="txtConfirmPassword" TextMode="Password" MaxLength="50" runat="server" CssClass="form-control form-control-lg" aria-describedby="ConfirmPassword"></asp:TextBox>
                                <div class="input-group-append">
                                    <button class="btn btn-outline-secondary" type="button" id="ConfirmPassword" onclick="ShowHidePassword()">
                                        <img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">
                                    </button>
                                </div>
                            </div>
                            <asp:CompareValidator ID="cmpConfirmPassowrd" ValidationGroup="save" Display="dynamic" ControlToValidate="txtConfirmPassword"
                                Operator="Equal" ControlToCompare="txtNewPassword" Type="String"
                                EnableClientScript="true" CssClass="error" Text="Confirm password is not equal new password"
                                runat="server" />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="save" Display="Dynamic" ID="reqConfirmPassword"
                                CssClass="error" ControlToValidate="txtConfirmPassword" ErrorMessage="Please enter confirm password" />
                        </div>
                        <asp:Button ID="BtnCancel" class="btn btn-outline-primary btn-lg text-uppercase" Text="Cancel"  runat="server" OnClick="BtnCancel_Click"
                             />
                        <asp:Button ID="btnProceed" Text="Proceed" ValidationGroup="save" runat="server" OnClick="btnProceed_Click"
                            CssClass="btn btn-lg btn-primary text-uppercase" />
                         
                    </div>
                    <%-- </div>--%>
                </div>
            </asp:View>
            <%--<asp:View ID="step2" runat="server">
                <div class="card-body">
                    <asp:Label ID="MsgStep2" runat="server" ForeColor="Red" />
                    <div class="alert alert-primary">
                        <asp:Label ID="lblDescOTP" runat="server"></asp:Label>
                        <b>
                            <asp:Label ID="lblmob" runat="server"></asp:Label></b>
                        <asp:Label ID="lbl3" runat="server"></asp:Label>
                    </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Label ID="lblOTP" Text="One-Time Password(OTP):" runat="server"></asp:Label>
                                <asp:TextBox ID="txtOTP" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvOTP" EnableClientScript="true"
                                    CssClass="error" ControlToValidate="txtOTP" ValidationGroup="saveOTP" ErrorMessage="Please enter OTP" Display="Dynamic" />

                            </div>
                            <asp:Button ID="btnSubmit" Text="Submit" ValidationGroup="saveOTP" runat="server" OnClick="btnSubmit_Click"
                                CssClass="btn btn-lg btn-primary text-uppercase" />
                        </div>
                    </div>
                </div>
            </asp:View>--%>
            <asp:View ID="viewOTP" runat="server">
                <div class="card-body">
                    <div class="mb-3">
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
                         <div class="alert alert-primary" role="alert" id="DivSuccess" runat="server" style="display: none">
                            <figure class="icon mr-2">
                                <img src="<%= this.Page.GetNewImagePath("success.svg") %>" alt="info-icon" width="22">
                            </figure>
                            <asp:Label ID="LblSuccessMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
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
                                <div class=" row col-auto" id="divremaining" runat="server">
                                    <small id="Small1" class="form-text"><span id="timer"></span> remaining</small>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:LinkButton runat="server" ID="linkRegenerateOTP" CausesValidation="false" Text="Regenerate OTP"
                                        OnClick="linkRegenerateOTP_Click" CssClass="btn btn-lg btn-outline-primary  text-uppercase btn-block"></asp:LinkButton>
                                </div>
                                <div class="col">
                                    <asp:Button ID="btnSubmit" Text="Submit" ValidationGroup="OptValid" runat="server" OnClick="btnSubmit_Click"
                                        CssClass="btn btn-lg btn-primary btn-block text-uppercase" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>

    </div>
    <script type="text/javascript">
        $(document).ready(function () {


           <%-- $('#ConfirmPassword').hover(function show() {
                var x = $("#ContentPlaceHolder1_txtConfirmPassword");
                x.prop('type', 'text');
                $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-close.svg") %>">');
            },
                function () {
                    var x = $("#ContentPlaceHolder1_txtConfirmPassword")
                    //Change the attribute back to password  
                    x.prop('type', 'password');
                    $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">');
                });--%>
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
        function ShowHidePassword() {
            
            var x = document.getElementById('<%=txtConfirmPassword.ClientID %>');
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        function StopFunction() {
            clearTimeout(ATMOTPTimer);
        }
    </script>
</asp:Content>
