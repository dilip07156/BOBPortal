<%@ Page Title="Spend Analyser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SpendAnalyser.aspx.cs" Inherits="CardHolder.SpendAnalyser" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>
                <asp:Literal ID="ltrFormHeader" runat="server" Text="Spend Analyser"></asp:Literal>
            </span>
        </h3>
    </div>
    <asp:CustomValidator ID="CustomValidator1" OnServerValidate="DateValidate" ErrorMessage="From date cannot be greater than To date.Please select properly."
        runat="server" ValidationGroup="submit" CssClass="error" Display="Dynamic" />
    <div>
        <asp:Label ID="lblDisplayMessage" runat="server" Style="color: Red; font-weight: bold"
            class="displaymessage"></asp:Label>
    </div>
    <ul class="addUser">
        <li><span class="left">
            <label>
                Credit Card Number:</label></span> <span class="right">
                    <asp:Label runat="server" ID="lblCreditCardNumber" />
                </span></li>
        <li><span class="left">
            <label>
                Name of Card-Holder:</label></span> <span class="right">
                    <asp:Label ID="lblCardHolder" runat="server" /></span> </li>
        <li><span class="left">
            <label>
                From:</label>
            <span class="red">*</span> </span><span class="right">
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="inputText wd146 date-picker1"
                    Style="margin-right: 5px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="txtFromDate"
                    Font-Size="11px" ForeColor="Red" ErrorMessage="Please select 'From' date" ValidationGroup="submit" />
            </span></li>
        <li><span class="left">
            <label>
                To:</label>
            <span class="red">*</span> </span><span class="right">
                <asp:TextBox ID="txtToDate" runat="server" CssClass="inputText wd146 date-picker1"
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
    <div class="contactinformation noPadding" id="MonthlyReportGraph" runat="server">
        <h4 id="gridheader" runat="server">
            Spend Analyser
        </h4>
        <div id="divMonthWiseReport" runat="server" visible="false">
            <asp:Chart ID="MonthwiseChart" runat="server" Height="400px" Width="900px">
                <Series>
                    <asp:Series Name="Months" Color="#FE5200" BackGradientStyle="DiagonalLeft" ChartType="Column"
                        Label="#VAL" ToolTip="#VAL">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="MonthWiseArea">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
        <label id="lblReportName" runat="server" style="font-size: 20px; font-weight: bold;
            margin-left: 320px">
        </label>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var maxdate = new Date();
            $(".date-picker1").datepicker({ dateFormat: 'dd/mm/yy', showOn: "both", changeMonth: true, changeYear: true, maxDate: maxdate, buttonImage: "../images/datepick.png", buttonImageOnly: true });
        });
    </script>
</asp:Content>
