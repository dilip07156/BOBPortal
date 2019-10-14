<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<%@ Page Language="C#" Title="Card Holder Login" MasterPageFile="~/Simaple.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CardHolder.Login" %>

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
    <%--<script src="https://www.google.com/recaptcha/api.js?render=6LfCPbsUAAAAALvGNtSqXRZwX1dp0xUZhd0AIbUT"></script>--%>
    <script src='https://www.google.com/recaptcha/api.js'></script>
    <script>
    //grecaptcha.ready(function() {
    //// do request for recaptcha token
    //// response is promise with passed token
    //    grecaptcha.execute('6LfirLsUAAAAACALm96mlfSVbb0WgAhDBqno3aiy', {action:'Login'})
    //              .then(function(token) {
    //        // add token value to form
    //        document.getElementById('g-recaptcha-response').value = token;
    //    });
    //    });
        </script>
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
                            <%--<asp:Label ID="viewCheckUsernameError" runat="server" ForeColor="#df5435"></asp:Label>--%>
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
                            <div class="form-group">
                                <div class="row">
                                    <div class="col">
                                        <label for="txtCheckUsername">Username</label>
                                    </div>
                                    <div class="col text-right"><a href="ForgotUsername.aspx" class="link"><small>Forgot Username?</small></a></div>
                                </div>
                                <asp:TextBox ID="txtCheckUsername" MaxLength="50" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvCheckMerId" EnableClientScript="true"
                                    CssClass="error" ControlToValidate="txtCheckUsername" ValidationGroup="ValidFirst"
                                    ErrorMessage="Please enter username" Display="Dynamic" />
                            </div>
                            <div class="form-group" style="display:none">
                                <asp:Image ID="Image2" runat="server" Style="border-width: 0px;" ImageUrl="~/Captchaa.aspx"
                                    AlternateText="Captcha" CssClass="cptimg" />
                                <asp:TextBox ID="txtCaptchaFirst" MaxLength="6" CssClass="Alphanumericsonly form-control form-control-lg" runat="server"></asp:TextBox></span>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCaptchaFirst" ValidationGroup="ValidFirst"
                                EnableClientScript="true" CssClass="error" ControlToValidate="txtCaptchaFirst"
                                ErrorMessage="Please enter captcha code" Display="Dynamic" Enabled="false" />
                            </div>
                            <div class="form-group">
                                <div class="g-recaptcha" data-sitekey="6LfirLsUAAAAACALm96mlfSVbb0WgAhDBqno3aiy"></div>
                            </div>
                            <asp:Button ID="btnEnter" runat="server" Text="Proceed" OnClick="btnEnter_Click" ValidationGroup="ValidFirst"
                                CssClass="btn btn-lg btn-block btn-primary text-uppercase" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="button greybtn d-none"
                                CausesValidation="False" />
                        </div>
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
</asp:Content>
