<%@ Page Language="C#" MasterPageFile="~/Simaple.Master" AutoEventWireup="true"
    CodeBehind="LoginNext.aspx.cs" Title="Card Holder Login" Inherits="CardHolder.LoginNext" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <style type="text/css">
        .ml125 ul.addUser li span.left {
            width: 172px !important;
        }

        ul.addUser li span.left {
            width: 90px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="pre-login-box">
            <div class="row align-items-center">
                <div class="col-lg-5">
                    <div class="card mb-4">
                        <div class="card-body">
                            <h5 class="card-title text-uppercase">Welcome Back!</h5>
                            <h6 class="font-weight-semi-bold mb-3">Enter your user ID and Password to access your account</h6>
                            <%--<asp:Label ID="viewUserLoginError" runat="server" ForeColor="#df5435"></asp:Label>--%>
                            <div class="col-md-12">
                                <div class="alert alert-danger" id="DivMessage" runat="server" style="display: none">
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="alert alert-danger" role="alert" id="DivERROR" runat="server" style="display: none">
                                    <figure class="icon mr-2">
                                        <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                                    </figure>
                                    <asp:Label ID="LblErrorMessage" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnErrormsgFromLoginNext" runat="server" />
                            <asp:HiddenField ID="hdnOTP" runat="server" />
                            <%--<asp:HiddenField ID="hideMobileNumber" runat="server" />--%>
                            <%-- <asp:HiddenField ID="hideEmailId" runat="server" />--%>
                            <asp:HiddenField runat="server" ID="hideOTPTimer" />
                            <asp:HiddenField runat="server" ID="hdnTabIndex" Value="0" />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col">
                                        <label for="txtUsername">Username</label>
                                    </div>
                                    <div class="col text-right"><a href="ForgotUsername.aspx" class="link"><small>Forgot Username?</small></a></div>
                                </div>
                                <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control form-control-lg"></asp:TextBox>
                            </div>

                            <%--<div class="mb-4">
                                <div>
                                    <label>Authenticate using<span class="orange"></span></label>
                                </div>
                                <div>
                                    <div class="col form-group">
                                        <asp:RadioButtonList CssClass="radio-contorl-table custom-radio inline-control" runat="server" OnSelectedIndexChanged="rbAuthnticate_Changed" AutoPostBack="true" ID="RadioAuthenticate" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">Password</asp:ListItem>
                                            <asp:ListItem Value="1">OTP</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>                           
                            </div>

                            <div class="form-group" id="divPassword" runat="server">
                                <div class="row">
                                    <div class="col">
                                        <label for="txtPassword">Password</label>
                                    </div>
                                    <div class="col text-right"><a href="ForgotPassword.aspx" class="link"><small>Forgot Password?</small></a></div>
                                </div>
                                <div class="input-group custom-input-group">
                                    <asp:TextBox TextMode="Password" MaxLength="50" runat="server" onfocus="getFocus(this.id);"
                                        ID="txtPassword" CssClass="form-control form-control-lg" aria-describedby="button-show-password"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button class="btn btn-outline-secondary" type="button" id="button-re-enter-new-pin">
                                            <img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">
                                        </button>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="rfvPwd" EnableClientScript="true"
                                    CssClass="error" ControlToValidate="txtPassword" ErrorMessage="Please enter password"
                                    Display="Dynamic" />




                            </div>


                            <div class="form-group" id="divOTP" runat="server" style="display: none">
                                <div class="row">
                                    <div class="col-auto mr-auto">
                                        <label for="">Enter OTP<span class="orange"></span></label>
                                    </div>
                                    <div class="col-auto">
                                        <a href="#" class="primary-link link" onclick="sendOTP();"><u>Resend</u></a>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="txtOTP" placeholder="Enter OTP" aria-describedby="button-enter-new-pin" MaxLength="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOTP" ControlToValidate="txtOTP" CssClass="error" runat="server" Display="Dynamic" ErrorMessage="Please enter OTP" Enabled="false"></asp:RequiredFieldValidator>




                                <div class="row" id="divIncorrectOTP" runat="server" style="display: none">
                                    <div class="col-auto mr-auto">
                                        <div id="divOTPSent" runat="server" style="display: none">
                                            <small id="" class="form-text">OTP has been sent to +91
                                        <asp:Label runat="server" ID="hideResultMobile"></asp:Label></small>

                                        </div>
                                    </div>
                                    <div class="col-auto" id="divremaining" runat="server" style="display: none">
                                        <small id="Small1" class="form-text"><span id="timer"></span>remaining</small>
                                    </div>

                                </div>
                                <div class="invalid-feedback" style="display: none" id="divInvalidfeedback" runat="server">
                                    <asp:Label runat="server" Style="display: none; align-self: center" ID="lblOTPMessage" CssClass="error"></asp:Label>
                                    <asp:Label runat="server" Style="display: none; align-self: center" ID="Label1" CssClass="error"></asp:Label>
                                </div>
                            </div>--%>
                            <div class="custom-tab grey">
                                Authenticate using
                                <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                                    <li class="nav-item" id="liPasswordTab">
                                        <a class="nav-link active" id="pillsPasswordTab" data-toggle="pill" href="#ContentPlaceHolder1_pillspassword" role="tab" aria-controls="ContentPlaceHolder1_pillspassword" aria-selected="false" runat="server" onclick="HighlightPasswordTab()">Password</a>
                                        <%--<asp:LinkButton runat="server" class="nav-link" id="pills-unbilled-tab">Unbilled</asp:LinkButton>--%>
                                    </li>
                                    <li class="nav-item" id="liOTPTab">
                                        <a class="nav-link" id="pillsOTPTab" data-toggle="pill" href="#ContentPlaceHolder1_pillsotp" role="tab" aria-controls="ContentPlaceHolder1_pillsotp" aria-selected="false" runat="server" onclick="HighlightOTPTab()">OTP</a>
                                    </li>
                                </ul>
                                <%-- <div>
                                    <asp:RadioButtonList CssClass="radio-contorl-table custom-radio inline-control" runat="server" OnSelectedIndexChanged="rbAuthnticate_Changed" AutoPostBack="true" ID="RadioAuthenticate" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">Password</asp:ListItem>
                                        <asp:ListItem Value="1">OTP</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>--%>
                                <div class="tab-content" id="pills-tabContent">

                                    <div class="tab-pane fade show active" id="pillspassword" role="tabpanel" aria-labelledby="pillsPasswordTab" runat="server">
                                        <div class="form-group" id="divPassword" runat="server">
                                            <div class="row">
                                                <div class="col">
                                                    <label for="txtPassword">Password</label>
                                                </div>
                                                <div class="col text-right"><a href="ForgotPassword.aspx" class="link"><small>Forgot Password?</small></a></div>
                                            </div>
                                            <div class="input-group custom-input-group">
                                                <asp:TextBox TextMode="Password" MaxLength="50" runat="server" onfocus="getFocus(this.id);"
                                                    ID="txtPassword" CssClass="form-control form-control-lg" aria-describedby="button-show-password"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <button class="btn btn-outline-secondary" type="button" id="button-re-enter-new-pin" onclick="ShowHidePassword()">
                                                        <img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">
                                                    </button>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvPwd" EnableClientScript="true"
                                                CssClass="error" ControlToValidate="txtPassword" ErrorMessage="Please enter password"
                                                Display="Dynamic" />




                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="pillsotp" role="tabpanel" aria-labelledby="pillsOTPTab" runat="server">
                                        <div class="form-group" id="divOTP" runat="server">
                                            <div class="row">
                                                <div class="col-auto mr-auto">
                                                    <label for="">Enter OTP<span class="orange"></span></label>
                                                </div>
                                                <div class="col-auto">
                                                    <a href="#" class="primary-link link" onclick="sendOTP();"><u>Resend</u></a>
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="txtOTP" placeholder="Enter OTP" aria-describedby="button-enter-new-pin" MaxLength="6"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOTP" ControlToValidate="txtOTP" CssClass="error" runat="server" Display="Dynamic" ErrorMessage="Please enter OTP" Enabled="false"></asp:RequiredFieldValidator>




                                            <div class="row" id="divIncorrectOTP" runat="server" style="display: none">
                                                <div class="col-auto mr-auto">
                                                    <div id="divOTPSent" runat="server" style="display: none">
                                                        <small id="" class="form-text">OTP has been sent to +91
                                        <asp:Label runat="server" ID="hideResultMobile"></asp:Label></small>

                                                    </div>
                                                </div>
                                                <div class="col-auto" id="divremaining" runat="server" style="display: none">
                                                    <small id="Small1" class="form-text"><span id="timer"></span>remaining</small>
                                                </div>

                                            </div>
                                            <div class="invalid-feedback" style="display: none" id="divInvalidfeedback" runat="server">
                                                <asp:Label runat="server" Style="display: none; align-self: center" ID="lblOTPMessage" CssClass="error"></asp:Label>
                                                <asp:Label runat="server" Style="display: none; align-self: center" ID="Label1" CssClass="error"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>






                            <div class="form-group" id="divPersonalMessage" runat="server">
                                <label><small>Please confirm your personal message to login</small></label>
                                <div class="custom-control custom-checkbox">
                                    <asp:CheckBox runat="server" ID="chkPersonalMessage" Checked="false"
                                        Text="" />
                                </div>
                                <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree"
                                    EnableClientScript="true" ClientValidationFunction="CheckBoxRequired_ClientValidate"
                                    ErrorMessage="Please select your personal message" Display="Dynamic" />
                            </div>



                            <asp:Button runat="server" ID="btnSubmit" Text="Login" OnClientClick="GetEncrypt();" OnClick="btnSubmit_Click"
                                CssClass="btn btn-lg btn-block btn-primary text-uppercase" />

                        </div>

                        <!-- Virtual Keyboard Section Starts -->
                        <div id="divVirtualKeyboard" class="card Virtualtable d-lg-block d-none">
                            <div class="card-body">
                                <div class="mb-2">
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox runat="server" ID="chkbox" onclick="init();" Text="USE VIRTUAL KEYBOARD" />
                                    </div>
                                </div>
                                <div class="mb-2">(Recommended for security reasons)</div>
                                <div id="kbplaceholder"></div>
                            </div>
                        </div>
                        <!--Virtual Keyboard section Ends here -->
                    </div>


                    <!--Sign up btn-->
                    <div class="card mb-4 signup-btn">
                        <a href="Registration.aspx">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col-10">
                                        <h6 class="text-uppercase">Register Your Card</h6>
                                        <div>Have a BOB card but no online account?</div>
                                    </div>
                                    <div class="col-2 text-right">
                                        <img src="<%= this.Page.GetNewImagePath("Chevron.svg") %>" alt="arrow right" class="chevron-right" />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-7 text-center d-lg-block d-none">
                    <figure>
                        <img src="<%= this.Page.GetNewImagePath("Loginbanner.svg") %>" alt="banner" class="banner-image LoginBannerImage" />
                    </figure>
                </div>
            </div>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function HighlightPasswordTab() {
            $('#divVirtualKeyboard').addClass('d-lg-block');
            $('#pills-tab a').removeClass('active');
            $('#liPasswordTab a').addClass('active');
            $("#pills-tabContent>div").removeClass("active show");
            $("#ContentPlaceHolder1_pillspassword").addClass("active show");
            document.getElementById("<%=rfvPwd.ClientID %>").enabled = true;
            document.getElementById("<%=rfvOTP.ClientID %>").enabled = false;
            $("#<%=hdnTabIndex.ClientID%>").val('0');

        }

        function HighlightOTPTab() {
            $('#divVirtualKeyboard').removeClass('d-lg-block');
            $('#pills-tab a').removeClass('active');
            $('#liOTPTab a').addClass('active');
            $("#pills-tabContent>div").removeClass("active show");
            $("#ContentPlaceHolder1_pillsotp").addClass("active show");
            document.getElementById("<%=rfvOTP.ClientID %>").enabled = true;
            document.getElementById("<%=rfvPwd.ClientID %>").enabled = false;
            $("#<%=hdnTabIndex.ClientID%>").val('1');
            sendOTP();

        }

        function AuthenticateClientValidate(sender, e) {
            if ($("input[id$='rbPassword']").is(':checked') == false && $("input[id$='rbOTP']").is(':checked') == false) {
                e.IsValid = false;
            } else {
                e.IsValid = true;
            }
        }

        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkPersonalMessage']").is(':checked');
        }

        function GetEncrypt() {
            
            var checked = jQuery("input[id$='chkPersonalMessage']").is(':checked');
            if (checked == true) {
                var newuserpwd = document.getElementById('<%=txtPassword.ClientID %>');
                if (newuserpwd.value != '') {
                    newuserpwd.value = hex_md5(newuserpwd.value);
                }
            }
        }

        function Blokalert(msg) {
            alert(msg);
        }

        function ShowHidePassword() {
            
            var x = document.getElementById('<%=txtPassword.ClientID %>');
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
       

        $(document).ready(function () {

            init();
            //custom checbox design
            $('.custom-checkbox > input, .radio-contorl-table input').addClass('custom-control-input');
            $('.custom-checkbox > label, .radio-contorl-table label').addClass('custom-control-label');

            //   $('.custom-checkbox .ChkReglabel > input').addClass('custom-control-input');
            //$('.custom-checkbox .ChkReglabel > label').addClass('custom-control-label');


           <%-- if ($(window).width() <= 992) {
                debugger;
                $('#button-re-enter-new-pin').click(function show() {
                    debugger;
                    var x = $("#ContentPlaceHolder1_txtPassword");
                    x.attr('type', 'text');
                    $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-close.svg") %>">');
                },
                    function () {
                        var x = $("#ContentPlaceHolder1_txtPassword")
                        //Change the attribute back to password  
                        x.attr('type', 'password');
                        $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">');
                    });

                if ($("#<%=hdnTabIndex.ClientID%>").val() == "0") {
                    HighlightPasswordTab()
                }
                else if ($("#<%=hdnTabIndex.ClientID%>").val() == "1") {
                    HighlightOTPTab()
                }
            }
            else {
                $('#button-re-enter-new-pin').hover(function show() {
                    var x = $("#ContentPlaceHolder1_txtPassword");
                    x.attr('type', 'text');
                    $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-close.svg") %>">');
                },
                    function () {
                        var x = $("#ContentPlaceHolder1_txtPassword")
                        //Change the attribute back to password  
                        x.attr('type', 'password');
                        $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">');
                    });

                if ($("#<%=hdnTabIndex.ClientID%>").val() == "0") {
                    HighlightPasswordTab()
                }
                else if ($("#<%=hdnTabIndex.ClientID%>").val() == "1") {
                    HighlightOTPTab()
                }
            }--%>

        });

        var ATMOTPTimer;
        function sendOTP() {

            <%--var MobileNumber = $('#<%=hideMobileNumber.ClientID%>').val();
            var EmailId = $('#<%=hideEmailId.ClientID%>').val();--%>

            StopFunction();
            AjaxCall();

            return true;
        }
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
            $('#<%=lblMessage.ClientID%>').html("Timeout for OTP");
                           <%-- $('#<%=divInvalidfeedback.ClientID%>').css('display', 'block');--%>
            $('#<%=DivMessage.ClientID%>').css('display', 'block');
            $("#ContentPlaceHolder1_hdnOTP").val("");
        }

        function StopFunction() {
            clearTimeout(ATMOTPTimer);
        }

        function AjaxCall() {
            var Username = $("#<%=txtUsername.ClientID%>").val();
            $.ajax({
                url: "LoginNext.aspx/SendOTP",
                //data: '{"UserName":"' + txtUsername.Text.Trim() + '}',
                data: '{"UserName":"' + Username + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var strings = data.d.split(",");
                    if (data.d != "") {
                        $('#<%=hdnOTP.ClientID %>').val(strings[0]);
                        $("span[id$='hideResultMobile']").html(strings[1]);
                        $('#<%=divOTP.ClientID%>').css('display', 'block');
                        $('#<%=divIncorrectOTP.ClientID%>').css('display', 'flex');
                        $('#<%=divOTPSent.ClientID%>').css('display', 'block');
                        $('#<%=divremaining.ClientID%>').css('display', 'block');
                        $('#<%=Label1.ClientID%>').css('display', 'none');
                        $('#<%=lblOTPMessage.ClientID%>').css('display', 'none');
                          <%--  $('#<%=hideOTPTimer.ClientID %>').val(strings[2]);--%>
                        var OTP = $("#<%=txtOTP.ClientID%>").val();
                        if (OTP != '' || OTP != null) {
                            $("#<%=txtOTP.ClientID%>").val('');
                        }
                        timer(60);


                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }

    </script>
</asp:Content>

