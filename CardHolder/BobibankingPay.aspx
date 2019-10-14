<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BobibankingPay.aspx.cs"
    Inherits="CardHolder.BobibankingPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" action="TestBobibanking.aspx" method="post">
    <input type="hidden" name="AMT" value="1.00" />
    <input type="hidden" name="PRN" id="PRN" runat="server" />
    <input type="hidden" name="ITC" id="ITC" runat="server" />
    <input type="submit" value="Pay Now using bobibanking" onclick="document.getElementById('form1').submit()" />
    </form>
    <form id="status" runat="server">
    <table width="100%">
        <tr>
            <th style="width: 250px; text-align: right">
                Bobibanking Response:
            </th>
            <td>
                <asp:Label runat="server" Text="N/V" ID="BobibankingString"></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="width: 250px; text-align: right">
                Bobibanking Payment Status:
            </th>
            <td>
                <asp:Label runat="server" Text="N/V" ID="BobibankingPaymentStatus"></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="width: 250px; text-align: right">
                Exception:
            </th>
            <td>
                <asp:Label runat="server" Text="N/V" ID="Exception"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
