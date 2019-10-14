<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentProcess.aspx.cs"
    Title="Bobibanking Payment" Inherits="CardHolder.Card.PaymentProcess" MasterPageFile="~/Site.Master"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">

        function DisplaySlip(d) {
            OpenWindowWithPost('PrintPaymentSlip.aspx', 'PrintPaymentSlip', d);
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--    <input type="hidden" runat="server" id="msg" value="" />
    <div class="commontitlediv">
        <h3>
            <span>
                <asp:Literal ID="ltrFormHeader" runat="server" Text="Payment Details"></asp:Literal>
            </span>
        </h3>
    </div>
    <div style=" color:blue; font-family:Arial ; font-size:medium"><asp:Label runat="server" ID="lblSuccessMessage"  /></div>
     
   

    <asp:HiddenField runat="server" ID="hdnPRN" />
    <div id="divMsgdisplay">
        <asp:Label ID="lblMessageDisplay" runat="server" Style="color: Red; margin-top: 10px;
            font-weight: bold" Visible="false"></asp:Label>
    </div>
    <div id="divDisplayAll" runat="server">
        <br />
        <ul class="addUser">
            <li><span class="left">
                <label>
                    Account Number</label>
                : </span><span class="right">
                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                </span></li>
            <li><span class="left">
                <label>
                    Credit Card Number</label>
                : </span><span class="right">
                    <asp:Label ID="lblCreditCardNo" runat="server"></asp:Label>
                </span></li>
            <li runat="server" id="liamt"><span class="left">
                <label>
                    Amount Due (Rs.)</label>
                : </span><span class="right">
                    <asp:Label ID="lblAmountDue" runat="server"></asp:Label>
                </span></li>
            <li><span class="left">
                <label>
                    Enter Amount (Rs)</label>
                : </span><span class="right">
                    <asp:TextBox ID="txtEnterAmount" runat="server" MaxLength="10" onKeyPress="return numbersWithDotonly(this, event);"
                        Text="2"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator1" runat="server"
                        ValidationGroup="submit" ControlToValidate="txtEnterAmount" Display="Dynamic"
                        ErrorMessage="Please enter amount"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" CssClass="error" ErrorMessage="Amount (Rs.) must be greater than 0"
                        ControlToValidate="txtEnterAmount" runat="server" Operator="NotEqual" Type="Double"
                        ValueToCompare="0" />
                </span></li>
        </ul>
        <br />
        <div style="margin-top: 190px">
            <h3>
                Select your payment mode</h3>
            <table>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblPaymentOptions" runat="server" CssClass="rbpaymenttype"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="  Bank of Baroda Net Banking" Value="1" style="margin-right: 20px"></asp:ListItem>
                            <asp:ListItem Text="  Other Bank Net Banking" Value="2" style="margin-right: 20px"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator2" runat="server"
                            ValidationGroup="submit" ControlToValidate="rblPaymentOptions" Display="Dynamic"
                            ErrorMessage="Please select payment method"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>       
        <div style="margin-top: 20px">
            <asp:Button runat="server" class="button" ValidationGroup="submit" ID="btnPaynow"
                Text="Submit & Initiate Payment" OnClick="btnPaynow_Click" />
            <input type="hidden" name="CustomerID" id="CustomerID" value="" runat="server" />
        </div>
    </div>
    <br />
    <asp:Button runat="server" CssClass="button" ID="btnPrint" Text="Click here to Print"
        Visible="false" />
    <asp:Button runat="server" CssClass="button" ID="btnPrintBillDesk" Text="Click here to Print Receipt"
        Visible="false" />
    <asp:LinkButton ID="lkbRedirectToCardStatement" runat="server" Visible="false" OnClick="lkbRedirectToCardStatement_Click"  >
            Back To Card Statement Page
    </asp:LinkButton>--%>


    <div class="container-fluid">
        <%--<h2 class="sub-title mb-2">Payments</h2>--%>

        <!--old code labels-->
        <input type="hidden" runat="server" id="msg" value="" />
        <asp:Label runat="server" ID="lblSuccessMessage" />
        <asp:HiddenField runat="server" ID="hdnPRN" />
        <asp:Label ID="lblMessageDisplay" runat="server" Style="color: Red; margin-top: 10px; font-weight: bold"
            Visible="false"></asp:Label>

        <div class="card mb-4" id="divPaymentRequest" runat="server">

            <div class="card-header">
                <h6 class="mb-0">Payment Details</h6>
            </div>
            <div class="card-body" id="divDisplayAll" runat="server">
                <div class="row mb-3">
                    <div class="col-lg-3 col-md-4 mb-3">
                        <div class="d-label mb-2">Account Number</div>
                        <asp:TextBox ID="txtAccountNumber" runat="server" class="form-control form-control-lg"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-4 mb-3">
                        <div class="d-label mb-2">Credit Card Number</div>
                        <asp:TextBox ID="txtCreditCardNumber" runat="server" class="form-control form-control-lg"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-4 mb-3" runat="server" id="divamt">
                        <label for="">Amount Due</label>
                        <asp:TextBox ID="txtUnbilledTransactionAmt" runat="server" class="form-control form-control-lg"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-4 mb-3" runat="server">
                        <label for="">Enter Amount</label>
                        <asp:TextBox ID="txtEnterAmount" runat="server" class="form-control form-control-lg" MaxLength="10" onKeyPress="return numbersWithDotonly(this, event);"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator1" runat="server"
                            ValidationGroup="submit" ControlToValidate="txtEnterAmount" Display="Dynamic"
                            ErrorMessage="Please enter amount"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" CssClass="error" ErrorMessage="Amount (Rs.) must be greater than 0"
                            ControlToValidate="txtEnterAmount" runat="server" Operator="NotEqual" Type="Double" ValidationGroup="submit"
                            ValueToCompare="0" />
                    </div>
                </div>
                <div class="form-group col">
                    <div class="row mb-3">
                        <small>Select Payment Mode</small>
                    </div>
                    <asp:RadioButtonList CssClass="radio-contorl-paymentprocess-table custom-radio inline-control" runat="server" AutoPostBack="true" ID="rblPaymentOptions" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Bank of Baroda Net Banking</asp:ListItem>
                        <asp:ListItem Value="2">Other Bank Net Banking</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RVa1" CssClass="error" runat="server" ControlToValidate="rblPaymentOptions"
                        Display="Dynamic" ErrorMessage="Please select payment method" ValidationGroup="submit"></asp:RequiredFieldValidator>
                </div>
                <asp:Button runat="server" ID="btnPaynow" Text="Proceed to payment" ValidationGroup="submit"
                    CssClass="btn btn-lg btn-primary text-uppercase" OnClick="btnPaynow_Click" />
                <input type="hidden" name="CustomerID" id="CustomerID" value="" runat="server" />
            </div>
            </div>
        <div class="card mb-4" id="divPaymentResponse" runat="server">
            <%-- <div class="col-lg-7 text-center d-lg-block">
                   <figure class="icon mr-2">
                                        <img src="" id="StatusImage" alt="info-icon" width="22" runat="server" />
                                    </figure>
                </div>--%>
            <asp:Button runat="server" CssClass="btn btn-lg btn-primary text-uppercase" ID="btnPrint" Text="Click here to Print"
                Visible="false" />
            <asp:Button runat="server" CssClass="btn btn-lg btn-primary text-uppercase" ID="btnPrintBillDesk" Text="Click here to Print Receipt"
                Visible="false" />
            <asp:LinkButton ID="lkbRedirectToCardStatement" runat="server" Visible="false" OnClick="lkbRedirectToCardStatement_Click">
            Back To Card Statement Page
            </asp:LinkButton>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //custom checkbox design
            $('.radio-contorl-paymentprocess-table input').addClass('custom-control-input');
            $('.radio-contorl-paymentprocess-table label').addClass('custom-control-label');
        });
    </script>
</asp:Content>
