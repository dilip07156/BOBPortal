<%@ Page Language="C#" Title="Payment Credit Detail" AutoEventWireup="true" CodeBehind="PaymentCreditDetail.aspx.cs"
    Inherits="CardHolder.PaymentCreditDetail" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        div.instruction1 { margin-top: 5px; margin-bottom: 10px; color: Red; }
        table.gridStyle { width: 972px; }
        ul.addUser li span.left { width: 85px !important; }
    </style>
    <script src="../javascript/DateTimePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>
                <asp:Literal ID="ltrFormHeader" runat="server" Text="Payment / Credit Detail"></asp:Literal>
            </span>
        </h3>
    </div>
    <asp:CustomValidator ID="CustomValidator1" OnServerValidate="DateValidate" ErrorMessage="From date cannot be greater than To date.Please select properly."
        runat="server" ValidationGroup="submit" CssClass="error" Display="Dynamic" />
    <div class="instruction1">
        <asp:Label ID="lblrange" CssClass="success" runat="server"></asp:Label>
    </div>
    <ul class="addUser">
        <li><span class="left">
        <label>From:</label> <span class="red">*</span>
             </span><span class="right">
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="inputText wd146 date-picker1"
                    Style="margin-right: 5px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="txtFromDate"
                    Font-Size="11px" ForeColor="Red" ErrorMessage="Please select 'From' date" ValidationGroup="submit" />
            </span></li>
        <li><span class="left">
        <label>To:</label> <span class="red">*</span>
           
            </span><span class="right">
                <asp:TextBox ID="txtToDate" runat="server" CssClass="inputText wd146 date-picker2"
                    Style="margin-right: 5px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqToDate" ControlToValidate="txtToDate"
                    CssClass="error" ForeColor="Red" ErrorMessage="Please select 'To' date" ValidationGroup="submit"
                    Display="Dynamic" />
            </span></li>
        <li><span class="left"></span><span class="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Get Details" class="button navbluebtm" OnClick="btnSubmit_Click"
                ValidationGroup="submit" />
        </span></li>
    </ul>
    <div class="contactinformation noPadding">
    <asp:Label ID="lblnorecords" runat="server"></asp:Label>
        <h4 id="gridheader" runat="server">
            List Payment Credit Details</h4>
        <asp:GridView runat="server" ID="grdPaymnetCreditDetail" Width="100%" AutoGenerateColumns="false"
            CssClass="gridStyle">
            <AlternatingRowStyle CssClass="secondrow" />
            <RowStyle CssClass="firstrow" />
            <Columns>
                <asp:TemplateField HeaderText="Transaction Date">
                    <ItemTemplate>
                        <%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("Transaction_date")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField HeaderText="Amount in (INR)">
                    <ItemTemplate>
                        <%# string.Format("{0:0.00}",Convert.ToDouble(Eval("Amount")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DETAILS" HeaderText="Details" />
            </Columns>
            <EmptyDataTemplate>
                Sorry!! No record found
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <div class="addcontactdiv">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="10" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="10" PageSize="10" />
    </div>
</asp:Content>
