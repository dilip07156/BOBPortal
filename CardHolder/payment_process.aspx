<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_process.aspx.cs"
    Inherits="CardHolder.payment_process" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
    <script type="text/javascript">
        function myfunc() {
            document.getElementById("payment").submit();
        }
        window.onload = myfunc;
    </script>
</head>
<body>
    <form id="payment" name="payment" method="post" action="https://www.billdesk.com/pgidsk/PGIMerchantPayment">
    <input type="hidden" runat="server" id="msg" value="" />
    <label runat="server" id="lblJavaScript"></label>
    </form>
</body>
</html>
