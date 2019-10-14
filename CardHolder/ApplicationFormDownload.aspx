<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%@ Page Title="ApplicationFormDownload" Language="C#" MasterPageFile="~/Simaple.Master" AutoEventWireup="true"
    CodeBehind="ApplicationFormDownload.aspx.cs" Inherits="CardHolder.ApplicationFormDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">

    function DisplayApplication(d) {
        OpenWindowWithPost('ApplicationPreview.aspx', 'ApplicationPreview', d);
        return false;
    }

    function OpenWindowWithPost(url, name, params) {
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", url);
        form.setAttribute("target", name);

        var input = document.createElement('input');
        input.type = 'hidden';
        input.name = "txtPostData";
        input.value = params;
        form.appendChild(input);

        document.body.appendChild(form);
        window.open("nopage.htm", name, "location = no, toolbar = no, menubar = no, scrollbars = yes, resizable = yes, addressbar = 0, titlebar = no, directories = no, channelmode = no, status = no");
        form.submit(url);
        document.body.removeChild(form);
    }         
    </script>
     <style type="text/css">
        .ml125 ul.addUser li span.left { width: 172px !important; }
        ul.addUser li span.left { width: 90px !important; }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="loginSec" style="padding-bottom: 200px">
    <div class="merchloginWrap shadow clearfix">
        <div class="fLeft userLog">
            <div class="ml125">
                <asp:Label ID="lblerror" runat="server" CssClass="error"></asp:Label>
                <ul class="addUser">
                    <li><span class="left">
                        <label>
                            Application Number:<span class="red">*</span>
                        </label>
                    </span><span class="right">
                        <asp:TextBox ID="txtAppNumber" MaxLength="20" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvAppNumber" EnableClientScript="true"
                            CssClass="error" ControlToValidate="txtAppNumber" ValidationGroup="ValidFirst"
                            ErrorMessage="Please enter application number" Display="Dynamic" />
                    </span></li>
                    <li><b>For security reason, please enter the Alphabets showing in the image/Captcha.</b></li>
                    <li><span class="left">
                      <%--  <cc1:captchacontrol id="CaptchaFirst" captchalength="5" runat="server" backcolor="White"
                            captchamintimeout="2" captchamaxtimeout="20" fontcolor="Black" height="51px"
                            captchaheight="50" captchachars="ABCDEFGHIJKLMNOPQRSTUVWXYZ" captchawidth="180"
                            linecolor="Black" noisecolor="Black" />--%>
                             <asp:Image ID="Image2" runat="server" Style="border-width: 0px;" ImageUrl="~/Captchaa.aspx"
                                AlternateText="Captcha" CssClass="cptimg" />
                    </span><span class="right">
                    <asp:Label  runat="server" AssociatedControlID="txtCaptchaFirst"></asp:Label>
                        <asp:TextBox ID="txtCaptchaFirst" MaxLength="6" CssClass="Alphanumericsonly" runat="server"></asp:TextBox></span>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCaptchaFirst" ValidationGroup="ValidFirst"
                            EnableClientScript="true" CssClass="error" ControlToValidate="txtCaptchaFirst"
                            ErrorMessage="Please enter captcha code" Display="Dynamic" />
                    </li>
                    <li><span class="left"></span><span class="right">
                        <asp:Button ID="btnEnter" runat="server" Text="Enter" OnClick="btnEnter_Click" ValidationGroup="ValidFirst"
                            CssClass="button" /></span> </li>
                    <li style="padding: 0px 0px 2px"><span class="left"></span><span class="right"><a
                        href="Login.aspx">Go to Login?</a></span></li>
                </ul>
            </div>
        </div>
        </div>
       
    </div>
</asp:Content>
