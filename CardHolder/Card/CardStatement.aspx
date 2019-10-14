<%@ Page Language="C#" Title="Card Statement" AutoEventWireup="true" CodeBehind="CardStatement.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="CardHolder.Card.CardStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div.instruction {
            margin-top: 20px;
            margin-left: 10px;
        }

        table.gridStyle {
            width: 985px; /*margin: 10px;            
            margin-top: 30px;
            margin-bottom: 30px;*/
        }
    </style>
    <%--<script src="../javascript/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
    <script src="../javascript/jquery-1.7.1.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--CARD STATMENTS-->
    <div class="card mb-4">
        <div class="card-header">
            <h6>Card Statements</h6>
            <small>
                <asp:Label runat="server" CssClass="success" ID="lblStmntmonthrange"></asp:Label></small>
        </div>
    </div>
    <div>
        <asp:Label ID="lblMessageDisplay" runat="server" Style="color: Red; margin-top: 10px; font-weight: bold"
            Visible="false"></asp:Label>
    </div>


    <input style="display: none" value="" id="hideSkipCount" runat="server" />
    <input style="display: none" value="" id="hidePageSize" runat="server" />
    <input style="display: none" value="" id="hidepRecordCount" runat="server" />
    <input style="display: none" value="" id="hideaccountNumber" runat="server" />
    <input style="display: none" value="" id="hidemonthRange" runat="server" />

    <asp:HiddenField ID="hidBILLEDOPENINGBAL" runat="server" />
    <asp:HiddenField ID="hidTOTALCREDITS" runat="server" />
    <asp:HiddenField ID="hidTOTALDEBITS" runat="server" />
    <asp:HiddenField ID="hidBilledClosingBal" runat="server" />

   
    <asp:ListView runat="server" ID="lstViewCardStatement" OnItemDataBound="lstViewCardStatement_ItemDataBound">       
        <LayoutTemplate>
            <!--header-->
            <div class="card mb-4 d-none d-lg-block">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg">Statement Month</div>
                        <div class="col-lg">Statement Date</div>
                        <div class="col-lg text-right">Min. Payment Due</div>
                        <div class="col-lg-4">Actions</div>
                    </div>
                </div>
            </div>

            <div id="itemPlaceholder" runat="server"></div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="statement-accordian">
                <div class="card mb-4">
                    <div class="card-header" id="headingOne">
                        <%--<hyperlink class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseOne<%= rowcount%>"" aria-expanded="true" aria-controls="collapseOne<%= rowcount%>"">--%>
                        <hyperlink class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseOne<%= rowcount%>" aria-expanded="true" aria-controls="collapseOne<%= rowcount%>">
            <div class="row">
                <div class="col-lg mb-lg-0 mb-2"><span class="statement-month"><%# Eval("STATEMENT_MONTH") %></span></div>
                <div class="col-lg">
                    <div class="row mb-lg-0 mb-2">
                        <div class="col-lg-12 col-7 d-block d-lg-none"><span>Statement Date</span></div>
                        <div class="col-lg-12 col-5 text-lg-left text-right"><%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("Stat_Date"))) %></div>
                    </div>
                </div>
                <div class="col-lg">
                    <div class="row mb-lg-0 mb-2">
                         <div class="col-lg-12 col-7 d-block d-lg-none"><span>Min. Payment Due</span></div>
                        <div class="col-lg-12 col-5 text-right"><span class="medium-icon">₹  </span><%# Eval("MINIMUM_PAYMENT_DUE") %></div>
                    </div>
                </div>
                <div class="col-lg-4">
                  <asp:Label ID="lblStatementDate" runat="server" Visible="false" Text='<%# (Convert.ToDateTime(Eval("Stat_Date"))) %>'></asp:Label>
                        <asp:Label ID="lblPDFName" runat="server" Visible="false" Text='<%# (Convert.ToString(Eval("Pdf_File"))) %>'></asp:Label>
                    <%-- <asp:ImageButton ID="ibtnPDF" runat="server" ImageUrl="~/images/pdf.png" ToolTip="Download PDF"
                            CommandName="downloadpdf" />--%> 
                     <asp:LinkButton ID="ibtnPrint" runat="server" CssClass="btn-link link mr-4" Text="Print"></asp:LinkButton>
                    <asp:LinkButton ID="ibtnPDF" runat="server" CssClass="btn-link link mr-4" Text="Download"></asp:LinkButton>
                       <%-- <a href="#" class="btn-link link mr-4" runat="server" id="ibtnPDF">Download</a>--%>
                        <asp:Label runat="server" ID="lblNopdf" Text="To be generated" CssClass="small text-muted"></asp:Label>                
                   <%--  <asp:ImageButton ID="ibtnPrint" runat="server" ImageUrl="~/images/print.png" CommandName="ibtnprintpdf"
                            ToolTip="Print PDF" />--%>
                    <%--<a href="#" class="btn-link link" id="ibtnPrint" runat="server">Print</a>--%>
                        <asp:Label runat="server" ID="lblNoprint" Text=""></asp:Label>
                    </div>
            </div>
                    </hyperlink>
                         <% rowcount++;%>
                    </div>
                    <div id="collapseOne<%= subrowcount%>" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg">
                                    <div class="row mb-lg-0 mb-2">
                                        <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">Opening Balance</small></div>
                                        <div class="col-lg-12 col-5 text-lg-left text-right"><span class="medium-icon">₹  </span><%# Eval("Opening_Balance") %></div>
                                    </div>
                                </div>
                                <div class="col-lg">
                                    <div class="row mb-lg-0 mb-2">
                                        <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">Payment/Credit/Reversals</small></div>
                                        <div class="col-lg-12 col-5 text-lg-left text-right"><span class="medium-icon">₹  </span><%# Eval("TOTAL_CREDITS") %></div>
                                    </div>
                                </div>
                                <div class="col-lg">
                                    <div class="row mb-lg-0 mb-2">
                                        <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">New Payments/Debit/Charges</small></div>
                                        <div class="col-lg-12 col-5 text-lg-left text-right"><span class="medium-icon">₹  </span><%# Eval("TOTAL_DEBITS") %></div>
                                    </div>
                                </div>
                                <div class="col-lg">
                                    <div class="row mb-lg-0 mb-2">
                                        <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">Closing Balance</small></div>
                                        <div class="col-lg-12 col-5 text-lg-left text-right"><span class="medium-icon">₹  </span><%# Eval("Closing_Balance") %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <% subrowcount++; %>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>       
            <div class="alert alert-danger" role="alert" id ="DivERROR" runat="server">
                       <figure class="icon mr-2"><img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22"></figure><asp:Label ID="LblErrorMessage"  runat="server" Text=""> <%= Constants.RecordNotFound %>   </asp:Label>
                    </div>
        </EmptyDataTemplate>
    </asp:ListView>























































    <%-- <!--header-->
    <div class="card mb-4 d-none d-lg-block">
        <div class="card-header">
            <div class="row">
                <div class="col-lg">Statement Month</div>
                <div class="col-lg">Statement Date</div>
                <div class="col-lg">Minimum Payment Due</div>
                <div class="col-lg-4">Actions</div>
            </div>
        </div>
    </div>

    <div class="statement-accordian">




        <% int rowcount = 0;
            int subrowcount = 0;
            if (lstCardStatement != null && lstCardStatement.Count > 0)
            {
                foreach (var item in lstCardStatement)
                {%>        
        <div class="card mb-4">
            <div class="card-header" id="headingOne">
                <hyperlink class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseOne<%= rowcount%>" aria-expanded="true" aria-controls="collapseOne<%= rowcount%>"> 
                             <div class="row">
                                    <div class="col-lg mb-lg-0 mb-2"><span class="statement-month"><%= item.STATEMENT_MONTH %></span></div>
                                    <div class="col-lg">
                                        <div class="row mb-lg-0 mb-2">                                       
                                            <div class="col-lg-12 col-5 text-lg-left text-right"><%= GeneralMethods.FormatDate(Convert.ToDateTime(item.Stat_Date)) %></div>
                                        </div>
                                    </div>
                                    <div class="col-lg">
                                        <div class="row mb-lg-0 mb-2">                                          
                                            <div class="col-lg-12 col-5 text-lg-left text-right"><%= item.MINIMUM_PAYMENT_DUE %></div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">                                        
                                       <div ID="lblPDFName" style="display:none"><%=item.Pdf_File %></div>
                    
                                          <a href=""  class="btn-link link">Download</a>
                        <asp:Label runat="server" ID="lblNopdf" Text="--"></asp:Label>
                                      
                                    </div>
                             </div>
                                                </hyperlink>
                <% rowcount++;%>
            </div>
            <div id="collapseOne<%= subrowcount%>" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg">
                            <div class="row mb-lg-0 mb-2">
                                <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">Opening Balance</small></div>
                                <div class="col-lg-12 col-5 text-lg-left text-right"><%= item.Opening_Balance %></div>
                            </div>
                        </div>
                        <div class="col-lg">
                            <div class="row mb-lg-0 mb-2">
                                <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">Payment/ Credit/ Reversals</small></div>
                                <div class="col-lg-12 col-5 text-lg-left text-right"><%= item.TOTAL_CREDITS %></div>
                            </div>
                        </div>
                        <div class="col-lg">
                            <div class="row mb-lg-0 mb-2">
                                <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">New Payments/ Debit/ Charges</small></div>
                                <div class="col-lg-12 col-5 text-lg-left text-right"><%= item.TOTAL_DEBITS %></div>
                            </div>
                        </div>
                        <div class="col-lg">
                            <div class="row mb-lg-0 mb-2">
                                <div class="col-lg-12 col-7"><small class="font-weight-semi-bold">Closing Balance</small></div>
                                <div class="col-lg-12 col-5 text-lg-left text-right"><%= item.Closing_Balance %></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
        </div>
        <% subrowcount++;
                }
            }
            else
            {%>
        <asp:Label ID="lblNoData" runat="server" Text="No Data Found"></asp:Label>
        <%} %>
    </div>--%>




    <%-----------------------------------------------------------------------------------------------------------------------------------------------------%>
    <%--<div class="contactinformation noPadding" style="margin-top: 20px !important">
       
        <asp:GridView runat="server" ID="grdCardStatement" Width="100%" AutoGenerateColumns="False"
            CssClass="card mb-4 d-none d-lg-block"
            OnRowDataBound="grdCardStatement_RowDataBound">
            <RowStyle CssClass="firstrow" />
            <Columns>                
                <asp:TemplateField>
            <ItemTemplate>
                <asp:GridView ID="gvChild" runat="server">
                    <Columns>
                        

                         <asp:BoundField DataField="BILLED_OPENING_BAL" />
                         <asp:BoundField DataField="TOTAL_CREDITS" />
                         <asp:BoundField DataField="TOTAL_DEBITS" />
                        <asp:BoundField DataField="Billed_Closing_Bal" />
        </Columns>
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField>                  
                <ItemTemplate>
                    <span style="display:none"><%# Eval("Row_Num") %></span>                     
                </ItemTemplate>
            </asp:TemplateField>                
                <asp:BoundField DataField="STATEMENT_MONTH" HeaderText="Statement Month" />               
                <asp:TemplateField HeaderText="Statement Date">
                    <ItemTemplate>
                        <%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("Stat_Date"))) %>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:BoundField DataField="MINIMUM_PAYMENT_DUE" HeaderText="Minimum Payment Due" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnPrint" runat="server"  ImageUrl="~/images/print.png" CommandName="ibtnprintpdf"
                            ToolTip="Print PDF" />
                        <asp:Label runat="server" ID="lblNoprint" Text="--"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:Label ID="lblStatementDate" runat="server" Visible="false" Text='<%# (Convert.ToDateTime(Eval("Stat_Date"))) %>'></asp:Label>
                        <asp:Label ID="lblPDFName" runat="server" Visible="false" Text='<%# (Convert.ToString(Eval("Pdf_File"))) %>'></asp:Label>
                        <asp:ImageButton ID="ibtnPDF" runat="server" ImageUrl="~/images/pdf.png" ToolTip="Download PDF"
                            CommandName="downloadpdf" />
                        <asp:Label runat="server" ID="lblNopdf" Text="--"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="IsExpanded" ClientIDMode="AutoID" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            </Columns>
            <EmptyDataTemplate>
                Records not found.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>--%>
    <div class="addcontactdiv">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="10" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="10" PageSize="10" />
    </div>
   <%-- <div class="addcontactdiv">
        <div class="splBtnwrap">
            <input type="hidden" runat="server" id="msg" value="" />
            <asp:Button ID="btnPayNow" runat="server" Text="Pay Now" OnClick="btnPayNow_Click"
                class="button buttonspl" />
        </div>
    </div>--%>
    <script type="text/javascript" language="javascript">
        function DisplayPDF(d) {
            // debugger;
            //            window.open('PrintCardStatement.aspx?' + d, '_blank');
            OpenWindowWithPost('PrintCardStatement.aspx', 'PrintCardStatement', d);
            //  tb_show("Find", 'PrintCardStatement.aspx' + "?TB_iframe=true&width=800&height=600", false, d);
            return false;
        }

        function DwnloadPDF(d) {
            OpenWindowWithPost('StatementDownload.aspx', 'StatementDownload', d);
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
            window.open("nopage.htm", name, "");
            form.submit(url);
            document.body.removeChild(form);
        }


        $(function () {
            $("[id*=grdCardStatement] td").click(function () {
                if ($(this).parents('tr').find('input[type="hidden"]').val() == "expanded")
                    collapseRow(this);
                else {

                    expandRow(this);
                }

            });

            // Ensure the row doesn't expand/collapse if a button is clicked
            $("[id*=grdCardStatement] td .btn").click(function (e) {
                e.stopPropagation();
            });
            //Ensures any clicks on the child row aren't registered
            $("[id*=grdCardStatement] td .gvChild").click(function (e) {
                e.stopPropagation();
            });
        });

        function expandRow(row) {


            ////var row = $("[id*=gridView] tr:last-child").clone(true);
            //alert(childrow);
            var SkipCount = parseInt($('#<%=hideSkipCount.ClientID%>').val());
            var PageSize = parseInt($('#<%=hidePageSize.ClientID%>').val());

            var RecordCount = parseInt($('#<%=hidepRecordCount.ClientID%>').val());
            var accountNumber = parseInt($('#<%=hideaccountNumber.ClientID%>').val());
            var monthRange = parseInt($('#<%=hidemonthRange.ClientID%>').val());


            var id = $(row).parents('tr').find('span').html();

            AjaxCall(id, SkipCount, PageSize, RecordCount, accountNumber, monthRange, row);

            $(row).closest("tr").after("<tr><td></td><td id='avani'colspan = '999'>" +
                $("[id*=pnlFiles]", $(row).closest("tr")).html() +
                "</td></tr>");
            $(row).parents('tr').find('input[type="hidden"]').val("expanded");



        };


        function AjaxCall(id, SkipCount, PageSize, RecordCount, accountNumber, monthRange, row) {
            var result = "";
            $.ajax({
                url: "CardStatement.aspx/GetCardStatementDetails",
                data: '{"RowNumber":"' + id + '","SkipCount":"' + SkipCount + '" ,"PageSize":"' + PageSize + '" ,"RecordCount":"' + RecordCount + '" ,"accountNumber":"' + accountNumber + '" ,"monthRange":"' + monthRange + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var strings = data.d.split(",");
                    if (data.d != "") {
                        //$("span[id$='lblATMReqNum']").html(strings[0]);
                        $('input[id$=hidBILLEDOPENINGBAL]').val(strings[0]);
                        $('input[id$=hidTOTALCREDITS]').val(strings[1]);
                        $('input[id$=hidTOTALDEBITS]').val(strings[2]);
                        $('input[id$=hidBilledClosingBal]').val(strings[3]);
                        var result1 = $('input[id$=hidBILLEDOPENINGBAL]').val();
                        var result2 = $('input[id$=hidTOTALCREDITS]').val();
                        var result3 = $('input[id$=hidTOTALDEBITS]').val();
                        var result4 = $('input[id$=hidBilledClosingBal]').val();
                        alert(result1);
                        result = result1 + result2 + result3 + result4;

                    }
                    else {
                        alert("data is not displayed successfully");

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }


        // function AjaxCall(id) {
        //     alert('avani');
        //     $.ajax({
        //         url: "CardStatement.aspx/GetCardStatementDetails",
        //         data: '{"RowNumber":"' + id + '"}',
        //         //dataType: "json",
        //         //type: "POST",
        //         //contentType: "application/json; charset=utf-8",  
        //         //success: function (response) {
        //         //    if (response.d) {
        //         //    var names = response.d;  
        //         //    alert(names);  
        //         //  }
        //         //  else {
        //         //    alert("data is not displayed successfully");
        //         //  }
        //         //},
        //         //failure: function (response) {
        //         //   alert(response.d);  
        //         //}
        //  contentType: "application/json; charset=utf-8",  
        //  dataType: "json",  
        //  success: function (response) {  
        //      var names = response.d;  
        //      alert(names);  
        //  },  
        //  failure: function (response) {  
        //      alert(response.d);  
        //  } 
        //});
        // }



        function collapseRow(row) {
            $(row).closest("tr").next().hide();
            $(row).parents('tr').find('input[type="hidden"]').val("collapsed");
        }






    </script>
</asp:Content>
