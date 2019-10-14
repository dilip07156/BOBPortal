<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payee.aspx.cs" Inherits="CardHolder.Payee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" action="payment_process.aspx" method="post">
    <input type="hidden" name="TxnAmount" value="2.00" />
    <input type="hidden" name="CustomerID" id="CustomerID" runat="server" />
    <input type="submit" value="Pay Now 1" onclick="document.getElementById('form1').submit()" />
    </form>
    <hr />
    <form id="status" runat="server">
    <table width="100%">
        <tr>
            <th style="width: 200px; text-align: right">
                Bill Desk Response:
            </th>
            <td>
                <asp:Label runat="server" Text="N/V" ID="BillDeskResponseString"></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="width: 200px; text-align: right">
                Bill Desk Payment Status:
            </th>
            <td>
                <asp:Label runat="server" Text="N/V" ID="BillDeskPaymentStatus"></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="width: 200px; text-align: right">
                Exception:
            </th>
            <td>
                <asp:Label runat="server" Text="N/V" ID="Exception"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
    <%--<form id="form2" action="payment_process.aspx" method="post">
    <input type="hidden" name="TxnAmount" value="2.00" />
    <input type="hidden" name="CustomerID" value="123457" />
    <input type="submit" value="Pay Now 2" onclick="document.getElementById('form2').submit()" />
    </form><br />
    <form id="form3" action="payment_process.aspx" method="post">
    <input type="hidden" name="TxnAmount" value="2.00" />
    <input type="hidden" name="CustomerID" value="123458" />
    <input type="submit" value="Pay Now 3" onclick="document.getElementById('form3').submit()" />
    </form><br />--%>
</body>
</html>
