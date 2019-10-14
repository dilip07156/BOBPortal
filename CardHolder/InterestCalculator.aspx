<%@ Page Language="C#" Title="InterestCalculator" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InterestCalculator.aspx.cs"
    Inherits="BobCardBackOffice.InterestCalculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        ul.addUser li span.left { width: 160px !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%">
        <div class="commontitlediv">
            <h3>
                <span>
                    <asp:Literal ID="ltrFormHeader" runat="server" Text="Interest Calculator"></asp:Literal>
                </span>
            </h3>
        </div>
        <div class="addcontactdiv">
            <span class="contractno">
                <asp:Label runat="server" ID="lblError"></asp:Label>
            </span>
        </div>
        <ul class="addUser">
            <li><span class="left commonLabel">
                <asp:Label ID="lblAmountRs" Text="Amount (Rs.):" runat="server"></asp:Label><span class="red">*</span>
            </span><span class="right">
                <asp:TextBox ID="txtAmountRs" onKeyPress="return numbersWithDotonly(this, event);" MaxLength="15" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvamount" runat="server" ControlToValidate="txtAmountRs"
                    ErrorMessage="Please enter amount" Display="Dynamic" CssClass="error"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" CssClass="error" ErrorMessage="Amount (Rs.) must be greater than 0"
                    ControlToValidate="txtAmountRs" runat="server" Operator="NotEqual" Type="Double"
                    ValueToCompare="0" />
            </span></li>
            <li><span class="left  commonLabel">
                <asp:Label ID="lblAnnualInterestRate" Text="Annual Interest Rate (%):" runat="server"></asp:Label><span class="red">*</span>
            </span><span class="right">
                <asp:TextBox ID="txtAnnualInterestRate" onKeyPress="return numbersWithDotonly(this, event);" MaxLength="6" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvannual" runat="server" ControlToValidate="txtAnnualInterestRate"
                    ErrorMessage="Please enter interest rate" Display="Dynamic" CssClass="error"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" CssClass="error" ErrorMessage="Interest Rate (%) must be greater than 0"
                    ControlToValidate="txtAnnualInterestRate" runat="server" Operator="NotEqual"
                    Type="Double" ValueToCompare="0"  />
                    <asp:CompareValidator ID="CompareValidator4" Display="Dynamic" CssClass="error" ErrorMessage="Interest Rate (%) must be less than 100"
                    ControlToValidate="txtAnnualInterestRate" runat="server" Operator="LessThanEqual"
                    Type="Double" ValueToCompare="100"  />
            </span></li>
            <li><span class="left  commonLabel">
                <asp:Label ID="lblTerm" Text="Term (In months):" runat="server"></asp:Label><span class="red">*</span>
            </span><span class="right">
                <asp:TextBox ID="txtTerm" MaxLength="4" onKeyPress="return numbersonly(this, event);"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvterm" runat="server" ControlToValidate="txtTerm"
                    ErrorMessage="Please enter terms in months" Display="Dynamic" CssClass="error"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" Display="Dynamic" CssClass="error" ErrorMessage="Term (In months) must be greater than 0"
                    ControlToValidate="txtTerm" runat="server" Operator="NotEqual" Type="Double"
                    ValueToCompare="0" />
            </span></li>
            <li><span class="left  commonLabel">
                <asp:Label ID="lblEmi" Text="EMI (Rs.):" runat="server"></asp:Label>
            </span><span class="right">
                <asp:TextBox ID="txtEmi" Enabled="false" runat="server"></asp:TextBox>
            </span></li>
            <li><span class="left"></span><span class="right">
                <asp:Button ID="btnSubmit" runat="server" Text="Calculate" OnClick="btnSubmit_Click"
                    CssClass="button navbluebtm" />
                <asp:Button ID="btnReset" CssClass="button greybtn" Text="Reset" CausesValidation="false" runat="server" OnClick="btnReset_Click" />
            </span></li>
        </ul>
    </div>
    <div class="contactinformation noPadding">
        <h4 id="gridheader" runat="server">
            EMI Details</h4>
        <asp:GridView ID="grdEMIDetail" runat="server" Width="100%" AutoGenerateColumns="false">
            <AlternatingRowStyle CssClass="secondrow" />
            <Columns>
                <asp:BoundField DataField="months" HeaderText="Installment" />
                <asp:BoundField DataField="payment" HeaderText="EMI" />
                <asp:BoundField DataField="principalPaid" HeaderText="Monthly Principal" />
                <asp:BoundField DataField="monthlyInterest" HeaderText="Monthly Interest" />
                <asp:BoundField DataField="cumulativePrinciple" HeaderText="Cumulative Principle" />
                <asp:BoundField DataField="cumulativeInterest" HeaderText="Cumulative Interest" />
                <asp:BoundField DataField="cumulativePayment" HeaderText="Cumulative Payment" />
                <asp:BoundField DataField="balance" HeaderText="Balance" />
            </Columns>
            <RowStyle CssClass="firstrow" />
        </asp:GridView>
    </div>

    <script type="text/javascript">

    
    
    </script>

</asp:Content>
