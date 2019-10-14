<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptDecrcypt4All.aspx.cs"
    Inherits="CardHolder.EncryptDecrcypt4All" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            Field 1 :
            <asp:TextBox runat="server" ID="txtencrypt"></asp:TextBox>
        </p>
        <asp:Button runat="server" ID="btnDecrypt" Text="Decrypt Entry" OnClick="btnDecrypt_Click" />
        <asp:Button runat="server" ID="btnEncrypt" Text="Encryp Entry" OnClick="btnEncryp_Click" />
        <p>
            Field 2 :
            <asp:TextBox runat="server" ID="txtdecrypt"></asp:TextBox>
        </p>
    </div>
    </form>
</body>
</html>
