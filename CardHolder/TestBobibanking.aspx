<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestBobibanking.aspx.cs"
    Inherits="CardHolder.TestBobibanking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bobibanking Payment</title>
    <script type="text/javascript">
        function myfunc() {
            document.getElementById("payment").submit();
        }
        window.onload = myfunc;
    </script>
</head>
<body>
    <form id="payment" name="payment" method="post" action="https://14.140.233.72/BankAwayRetailRM/sgonHttpHandler.aspx?Action.BOBCARDS.ShoppingMall.Login.Init1=Y">
    <input type="hidden" runat="server" id="encdata" value="" />
    <label runat="server" id="lblJavaScript">
    </label>
        <asp:GridView runat="server">  </asp:GridView>
    </form>
</body>
</html>
