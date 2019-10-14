<%@ Page Title="" Language="C#" MasterPageFile="~/Simaple.Master" AutoEventWireup="true"
    CodeBehind="ApplicationSuccess.aspx.cs" Inherits="CardHolder.ApplicationSuccess" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label runat="server" Font-Bold="true" ID="successMsg"></asp:Label>
        <br />
        <asp:Label runat="server" Font-Bold="true" ID="lblMessage"></asp:Label>
        <div id="divbtn" runat="server">
            Click here to take printout of your form<asp:Button runat="server" ID="btnprintform"
                CssClass="button" Text="Print" />
        </div>
        <a href="Login.aspx" id="alogin" class="button" runat="server">Click Here for login</a>
    </div>
</asp:Content>
