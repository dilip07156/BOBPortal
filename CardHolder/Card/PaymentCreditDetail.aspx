<%@ Page Language="C#" Title="Payment Credit Detail" AutoEventWireup="true" CodeBehind="PaymentCreditDetail.aspx.cs"
    Inherits="CardHolder.PaymentCreditDetail" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        div.instruction1 {
            margin-top: 5px;
            margin-bottom: 10px;
            color: Red;
        }

        table.gridStyle {
            width: 972px;
        }

        ul.addUser li span.left {
            width: 85px !important;
        }
    </style>
    <%--<script src="../javascript/DateTimePicker.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Payment/Credit Details</h6>
            <small>
                <asp:Label ID="lblrange" runat="server"></asp:Label></small>
        </div>
        <asp:CustomValidator ID="CustomValidator1" OnServerValidate="DateValidate" ErrorMessage="From date cannot be greater than To date.Please select properly."
            runat="server" ValidationGroup="submit" CssClass="error" Display="Dynamic" />
        <div class="card-body">
            <div class="row align-items-end">
                <div class="col-md-5 mb-3">
                    <label for="">From</label>
                    <div class="input-group  input-group-lg date-input-group">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <%--<asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>--%>
                        <div class="input-group-append">
                            <span class="input-group-text" id="">
                                <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>">
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-5 mb-3">
                    <label for="">To</label>
                    <div class="input-group  input-group-lg date-input-group">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <div class="input-group-append">
                            <span class="input-group-text" id="">
                                <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>">
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-2 mb-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Proceed" class="btn btn-primary btn-lg text-uppercase" OnClick="btnSubmit_Click"
                        ValidationGroup="submit" />
                </div>
            </div>
        </div>
    </div>
    <%--<ul class="addUser">
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
    </ul>--%>



    <div id="accordian-payment" class="statement-accordian accordian">
        <asp:HiddenField ID="hdnMonthYears" runat="server" Value="" />
        <% int HeaderRowCount = 0;
            int rowcount = 0;
            int subrowcount = 0;
            int lstcount = 0;
            bool IsCreateSubRowdiv = false;
            if (lst != null && lst.Count > 0)
            {
                foreach (var item in lst)
                {
        %>
        <% if ((string.IsNullOrEmpty(hdnMonthYears.Value)) || (!string.IsNullOrEmpty(hdnMonthYears.Value) && (item.MonthYear != hdnMonthYears.Value)))
            {%>
        <div class="card mb-4">
            <%} %>
            <% if ((string.IsNullOrEmpty(hdnMonthYears.Value)) || (!string.IsNullOrEmpty(hdnMonthYears.Value) && (item.MonthYear != hdnMonthYears.Value)))
                {%>
            <div class="card-header" id="heading1">
                <hyperlink class="btn btn-link" data-toggle="collapse" data-target="#collapse1<%= rowcount%>" aria-expanded="true" aria-controls="collapse1<%= rowcount%>">
                                <span class="statement-month text-uppercase"><%= item.MonthYear %></span>
                                 <div style="display:none"> <%= hdnMonthYears.Value = item.MonthYear %></div>
                                <% HeaderRowCount = 0;
                                    IsCreateSubRowdiv = true;%>
                            </hyperlink>
                <% rowcount++;
                %>
            </div>
            <%}
                if (IsCreateSubRowdiv)
                {
            %>
            <div id="collapse1<%= subrowcount%>" class="collapse show" aria-labelledby="heading1" data-parent="#accordian-payment">
                <% } %>
                <% if (HeaderRowCount == 0)
                    { %>
                <div class="card-body mobile-table">
                    <table role="table">
                        <%} %>
                        <% if (HeaderRowCount == 0)
                            { %>
                        <thead role="rowgroup">
                            <tr role="row">
                                <th role="columnheader">Transaction Date</th>
                                <th role="columnheader">Amount in (INR)</th>
                                <th role="columnheader">Description</th>
                                <th role="columnheader">Details</th>
                            </tr>
                        </thead>
                        <%} %>
                        <% if (HeaderRowCount == 0)
                            { %>
                        <tbody role="rowgroup"> 
                        <% HeaderRowCount++;
                            }%>                                                 
                            <tr role="row">
                                <td role="cell"><%= GeneralMethods.FormatDate(Convert.ToDateTime(item.Transaction_Date)) %></td>
                                <td role="cell"><span class="medium-icon">₹  </span><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %></td>
                                <td role="cell"><%= item.Description %></td>
                                <td role="cell"><%= item.DETAILS %></td>
                            </tr>                        
                       <% if (lstcount + 1 < lst.Count && lst[lstcount + 1].MonthYear != item.MonthYear)
                            { %>
                            </tbody>
                    </table>
                </div>
                <%} %>



                <% if (lstcount + 1 < lst.Count && lst[lstcount + 1].MonthYear != item.MonthYear)
                    {
                        IsCreateSubRowdiv = false;
                %>
            </div>
        </div>
        <%  subrowcount++;
                    }
                    IsCreateSubRowdiv = false;
                    lstcount++;
                }
            }
    else
    {%>
        <div class="alert alert-danger" role="alert" id ="DivERROR" runat="server">
                       <figure class="icon mr-2"><img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22"></figure><asp:Label ID="LblErrorMessage"  runat="server" Text=""> <%= Constants.RecordNotFound %>   </asp:Label>
                    </div>
        <%}%>
    </div>
    <%-- <div class="contactinformation noPadding">
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
    </div>--%>
   
    <div class="addcontactdiv" style="display: none">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="10" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="10" PageSize="1000" />
    </div>   
    <script type="text/javascript">       
        var date = new Date();
        var currentMonth = date.getMonth();
        var currentDate = date.getDate();
        var currentYear = date.getFullYear();
        $('[id*=txtFromDate]').datepicker({
            autoclose: true,
            startDate: new Date(currentYear, currentMonth - 24, currentDate),
            endDate: new Date(),
            format: 'dd/mm/yyyy'
        });
        $('[id*=txtToDate]').datepicker({
            autoclose: true,
            startDate: new Date(currentYear, currentMonth - 24, currentDate),
            endDate: new Date(),
            format: 'dd/mm/yyyy'
        });

    </script>
</asp:Content>
