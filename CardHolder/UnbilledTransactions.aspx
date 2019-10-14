<%@ Page Language="C#" Title="Unbilled Transactions" AutoEventWireup="true" CodeBehind="UnbilledTransactions.aspx.cs"
    Inherits="CardHolder.UnbilledTransactions" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        div.instruction
        {
            border: 1px solid #D1D0CE;
            width: 970px;
            height: 140px;
            margin-top: 70px;
        }
        table.gridStyle
        {
            width: 985px;
        }
        ul.addUser li
        {
            width: 950px !important;
        }
        ul.addUser li span.right
        {
            width: 780px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>
                <asp:Literal ID="ltrFormHeader" runat="server" Text="Unbilled / Unsettled Transactions"></asp:Literal>
            </span>
        </h3>
        <asp:Label ID="lblNoRecords" runat="server" Text="Sorry!! No records found." CssClass="error"
            Visible="false"></asp:Label>
    </div>
    <ul class="addUser">
        <li><span class="left">
            <label>
                Transaction Type:</label>
            <span class="red">*</span> </span><span class="right">
                <asp:DropDownList ID="ddlSelectTransactionType" runat="server" CssClass="myselect">
                </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="error" ID="rfvTransactionType" runat="server"
                    ControlToValidate="ddlSelectTransactionType" Display="Dynamic" ErrorMessage="Please select transaction type"
                    InitialValue="0"></asp:RequiredFieldValidator>
                <asp:HyperLink runat="server" ID="ConvertToEMI" Text="" Style="color: Blue; text-decoration: underline;
                    float: right;"></asp:HyperLink>
            </span><span class="right"></span></li>
        <li><span class="left"></span><span class="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Get Details" class="button greybtn " OnClick="btnSubmit_Click" />
            <%--<span class="hints">Amount due will be shown zero (0) in payment page; if you directly pay from here.</span>--%>
        </span>
            <div style="float: left; padding-top: 120px; padding-left: 400px;">
                <asp:Button ID="btnpaynowWithouAmt" runat="server" CausesValidation="false" Text="Pay Now"
                    class="button buttonspl navbluebtm" OnClick="btnpaynowWithouAmt_Click" />
            </div>
        </li>
    </ul>
    <div class="contactinformation noPadding">
        <h4 id="gridheader" runat="server" style="margin-top: 20px !important">
            List Payment Credit Details</h4>
        <asp:GridView runat="server" Width="100%" ID="grdUnbilledTransactions" AutoGenerateColumns="false"
            CssClass="gridStyle" OnRowDataBound="grdUnbilledTransactions_RowDataBound">
            <AlternatingRowStyle CssClass="secondrow" />
            <RowStyle CssClass="firstrow" />
            <Columns>
                <asp:TemplateField HeaderText="Transaction Date">
                    <%--<ItemTemplate>                  
                        <%# (Convert.ToDateTime(Eval("Transaction_date"))).ToString("dd/MM/yyyy").Replace("-","/")%>
                    </ItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# ((DateTime)Eval("Transaction_date")) == DateTime.MinValue ? "": GeneralMethods.FormatDate(Convert.ToDateTime(Eval("Transaction_date"))) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Card_Number" HeaderText="Card Number" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Merchant_Name" HeaderText="Merchant Name" />
                <asp:TemplateField HeaderText="Amount in (INR)">
                    <ItemTemplate>
                        <%# string.Format("{0:0.00}",Convert.ToDouble(Eval("Amount")))%>
                        <asp:Label ID="lblMerchantName" runat="server" Text='<%# Eval("merchant_name") %>'
                            Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                Records not found.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <div class="addcontactdiv">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="10" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="10" PageSize="10" />
    </div>
    <div class="addcontactdiv">
        <div class="splBtnwrap">
            <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" OnClick="btnPayNow_Click"
                class="button buttonspl" />
        </div>
    </div>
</asp:Content>
