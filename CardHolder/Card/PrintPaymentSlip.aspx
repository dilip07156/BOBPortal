<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPaymentSlip.aspx.cs"
    Inherits="CardHolder.Card.PrintPaymentSlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BOBCARDS Payment Slip</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <label id="lblDisplayMessage" runat="server" visible="false" style="color: Red" />
    <div class="taWrap">
        <div class="taLogo">
            <img src="../images/ta-logo.jpg" width="193" height="74" alt="Transaction Acknowledgement"
                title="Transaction Acknowledgement" /></div>
        <div class="clearfix">
            <p>
                <b>Transaction Acknowledgement:</b></p>
            <p>
                Thank You. The payment has been successfully received and the same shall be credited
                to your credit card account within 2 working days. Please find transaction summary
                as under:</p>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="taGrid">
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>Particular</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <strong>Details</strong>
                    </td>
                </tr>
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>Transaction Reference Number</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <asp:Label runat="server" ID="lblTransactionNum"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>Transaction Date & Time</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <asp:Label runat="server" ID="lbltxnDateTime"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>BOBCARD Number</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <asp:Label runat="server" ID="lblCardnumber"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>Name of the Cardholder</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <asp:Label runat="server" ID="lblName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>Transaction Amount</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <asp:Label runat="server" ID="lblamount"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="22" align="left" valign="middle">
                        <strong>Mode</strong>
                    </td>
                    <td height="22" align="left" valign="middle">
                        <asp:Label runat="server" ID="lblModePayment"></asp:Label>
                    </td>
                </tr>
            </table>
            <p>
                Please quote the above mentioned transaction reference number in all future communications
                related to this payment.</p>
            <div class="clearfix">
                <div class="taBtn mb20">
                    <input type="button" value="Print" class="button" onclick="window.print()" />
                </div>
                <div class="taBtn mb20">
                    <input type="button" value="Close" class="button" onclick="window.close()" />
                </div>
                <%--<div class="taBtn mb20">
                    <input type="button" value="Make Another Payment" class="button" />
                </div>--%>
            </div>
            <div class="clearfix">
                <p>
                    <b>You may also perform the following activities at ease on our web portal <a class="taLinks"
                        href="https://online.bobcards.com" target="_blank">https://online.bobcards.com</a></b></p>
                <ul class="bulletText">
                    <li>
                        <img src="../images/bullet-list.png" width="12" height="14" alt="" class="bulletListIco" />View
                        unbilled transactions &amp; account summary</li>
                    <li>
                        <img src="../images/bullet-list.png" width="12" height="14" alt="" class="bulletListIco" />View
                        and download latest and previous card statements</li>
                    <li>
                        <img src="../images/bullet-list.png" width="12" height="14" alt="" class="bulletListIco" />View
                        payment history of past one year</li>
                    <li>
                        <img src="../images/bullet-list.png" width="12" height="14" alt="" class="bulletListIco" />Place
                        and track various requests and many <b>more...</b></li>
                </ul>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
