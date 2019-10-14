<%@ Page Language="C#" Title="Unbilled Transactions" AutoEventWireup="true" CodeBehind="UnbilledTransactions.aspx.cs"
    Inherits="CardHolder.UnbilledTransactions" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Transactions</h6>
        </div>
        <asp:Label ID="lblNoRecords" runat="server" Text="Sorry!! No records found." CssClass="error"
            Visible="false"></asp:Label>
    </div>
    <div class="custom-tab">
        <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
            <li class="nav-item" id="liAllTab" style="display: none">
                <%--<a class="nav-link active" id="pillsAllTab" data-toggle="pill" href="#ContentPlaceHolder1_pillsall" role="tab" aria-controls="ContentPlaceHolder1_pillsall" aria-selected="true" runat="server">All</a>--%>
                <a class="nav-link" id="pillsAllTab" data-toggle="pill" href="#ContentPlaceHolder1_pillsall" role="tab" aria-controls="ContentPlaceHolder1_pillsall" aria-selected="false" runat="server">All</a>
            </li>
            <li class="nav-item" id="liUnbilledTab">
                <a class="nav-link active" id="pillsUnbilledTab" data-toggle="pill" href="#ContentPlaceHolder1_pillsunbilled" role="tab" aria-controls="ContentPlaceHolder1_pillsunbilled" aria-selected="false" runat="server" onclick="HighlightUnbilledTab()">Unbilled</a>
                <%--<asp:LinkButton runat="server" class="nav-link" id="pills-unbilled-tab">Unbilled</asp:LinkButton>--%>
            </li>
            <li class="nav-item" id="liUnSettledTab">
                <a class="nav-link" id="pillsUnsettledTab" data-toggle="pill" href="#ContentPlaceHolder1_pillsunsettled" role="tab" aria-controls="ContentPlaceHolder1_pillsunsettled" aria-selected="false" runat="server" onclick="HighlightUnSettledTab()">Unsettled</a>
            </li>
        </ul>
        <div class="tab-content" id="pills-tabContent">
            <%--<div class="tab-pane fade show active" id="pillsall" role="tabpanel" aria-labelledby="pillsAllTab" runat="server">--%>
            <div class="tab-pane fade" id="pillsall" role="tabpanel" aria-labelledby="pillsAllTab" runat="server" style="display: none">
                <ul class="list-group transaction-list-two">

                    <%  if (lstAllTransactions != null && lstAllTransactions.Count > 0)
                        { %>
                    <div class="card-header custom-header p-3">
                        <div class="row">
                            <div class="col-7">
                                <strong>Details</strong>
                            </div>
                            <div class="col-5 text-right">
                                <strong>Amount</strong>
                            </div>
                        </div>
                    </div>
                    <% foreach (var item in lstAllTransactions)
                        {%>
                    <li class="list-group-item">
                        <div class="row">
                            <div class="col-7">
                                <%= item.Merchant_Name %>
                                <div><small class="date text-muted"><%= item.Transaction_date %></small></div>
                            </div>
                            <div class="col-5 text-right">
                                <span class="medium-icon">₹  </span><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>
                            </div>
                        </div>
                    </li>
                    <%}
                        }
                        else
                        {%>
                    <div class="alert alert-danger" role="alert" id="DivERROR" runat="server" style="display: none">
                        <figure class="icon mr-2">
                            <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                        </figure>
                        <asp:Label ID="LblErrorMessage" runat="server" Text=""></asp:Label>
                    </div>

                    <%} %>
                </ul>

                <div class="row">
                    <div>
                        <asp:Button runat="server" ID="btnAllView" Text="View More" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                            OnClick="btnAllView_Click" />
                    </div>
                </div>
            </div>
            <div class="tab-pane fade show active" id="pillsunbilled" role="tabpanel" aria-labelledby="pillsUnbilledTab" runat="server">

                <div class="card mb-4">
                    <asp:HiddenField ID="hdnIntTot" runat="server" />
                    <div class="card-header">
                        <h6 class="mb-0">Payment Summary </h6>
                    </div>
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-lg-9">
                                <div class="row">
                                    <div class="col-md-9">
                                        Total Amount to be paid (<asp:Label ID="lblNoofTransactionSelected" runat="server" Text="0"></asp:Label>
                                        transaction selected)
                                    </div>
                                    <div class="col-md-3 text-md-right text-left h6">
                                        <span class="medium-icon">₹  </span>
                                        <asp:Label ID="lblamount" runat="server" Text="0.00"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 text-lg-right text-left">
                                <asp:Button ID="btnPayNow" OnClick="btnPayNow_Click" runat="server" Text="Pay Now"
                                    CssClass="btn btn-lg btn-primary text-uppercase" Enabled="false" />
                            </div>
                        </div>



                    </div>
                </div>

                <%--<div class="mb-3">
                    <small>Please select the bills which you wish to make payment for</small>
                </div>--%>

                <div id="divtableid" class="divtable accordion-xs form-group">
                    <asp:ListView runat="server" ID="lstviewUnbilledTrasnaction">
                        <LayoutTemplate>
                            <!--header-->
                            <div class="tr headings emi-heading">
                                <%-- <div class="col-1">
                                <asp:CheckBox ID="chkAllSelect" runat="server" />
                            </div--%>
                                <div class="th emi-date">
                                    <div class="custom-control custom-checkbox inline-control">
                                        <asp:CheckBox ID="chkAllSelect" runat="server" Text=" " />
                                        <span class="d-lg-none d-inline-block select-all">Select All</span>
                                        <%--<input type="checkbox" class="custom-control-input" id="checkAll" >--%>
                                        <%--<label class="custom-control-label" for="checkAll"><span class="d-lg-none d-inline-block">Select All</span></label>--%>
                                    </div>
                                    <span class="d-none d-lg-inline-block date-text">Date</span>
                                </div>

                                <div class="th emi-card-number">
                                    Card Number
                                </div>
                                <div class="th emi-merchant-name">
                                    Merchant Name
                                </div>
                                <div class="th emi-amount  text-right">
                                    Amount In (INR)
                                </div>
                            </div>
                            <div id="itemPlaceholder" runat="server"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="tr">
                                <div class="td emi-date accordion-xs-toggle">
                                    <div class="custom-control custom-checkbox inline-control">
                                        <asp:CheckBox ID="chkTransactions" runat="server" onclick="Getamount()" Text=" " />
                                        <asp:HiddenField ID="hdnAmount" runat="server" Value='<%#Eval("Amount","{0:f}")%>' />
                                        <asp:HiddenField ID="hdnEMIOracleId" Value='<%#Eval("MICROFILM_REF_NUMBER")%>' runat="server" />
                                        <asp:Label ID="lblEMIRequested" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <span class="date-text">
                                        <%# (Convert.ToDateTime(Eval("Transaction_date"))).ToString("dd MMMM yy")%>
                                    </span>

                                </div>


                                <div class="accordion-xs-collapse">
                                    <div class="inner">
                                        <div class="td emi-card-number">
                                            <asp:Label ID="lblCardNumber" runat="server" Text='<%#Eval("Card_Number")%>' />
                                        </div>
                                        <%--<i class="fas fa-rupee-sign small-icon"></i><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>--%>


                                        <div class="td emi-merchant-name">
                                            <asp:Label ID="lblmerchantNm" runat="server" Text='<%#Eval("merchant_name")%>' />

                                            <div class="small">
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>' class="small" />
                                            </div>
                                        </div>
                                        <%--<i class="fas fa-rupee-sign small-icon"></i><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>--%>

                                        <div class="td emi-amount text-left text-lg-right">
                                            <span class="medium-icon">₹  </span>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount","{0:f}")%>' />
                                            <%--<i class="fas fa-rupee-sign small-icon"></i><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>--%>
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="alert alert-danger" role="alert" id="DivERROR" runat="server">
                                <figure class="icon mr-2">
                                    <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                                </figure>
                                <asp:Label ID="LblErrorMessage" runat="server" Text=""> <%= Constants.RecordNotFound %>   </asp:Label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>

                <div class="form-group">
                    <div class="row col-3">
                        <asp:Button runat="server" ID="btnUnbilledView" Text="View More" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                            OnClick="btnUnbilledView_Click" />
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" id="pillsunsettled" role="tabpanel" aria-labelledby="pillsUnsettledTab" runat="server">
                <div class="divtable accordion-xs">
                    <asp:ListView runat="server" ID="lstviewUnsettledTransaction">
                        <LayoutTemplate>
                            <!--header-->
                            <div class="tr headings emi-heading">
                                <div class="th emi-date">
                                    <span class="d-none d-lg-inline-block date-text">Date</span>
                                </div>

                                <div class="th emi-card-number">
                                    Card Number
                                </div>
                                <div class="th emi-merchant-name">
                                    Merchant Name
                                </div>
                                <div class="th emi-amount  text-right">
                                    Amount In (INR)
                                </div>
                            </div>
                            <div id="itemPlaceholder" runat="server"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="tr">
                                <div class="td emi-date accordion-xs-toggle">
                                    <span class="date-text">
                                        <%# (Convert.ToDateTime(Eval("Transaction_date"))).ToString("dd MMMM yy")%>
                                    </span>

                                </div>


                                <div class="accordion-xs-collapse">
                                    <div class="inner">
                                        <div class="td emi-card-number">
                                            <asp:Label ID="lblCardNumber" runat="server" Text='<%#Eval("Card_Number")%>' />
                                        </div>


                                        <div class="td emi-merchant-name">
                                            <asp:Label ID="lblmerchantNm" runat="server" Text='<%#Eval("merchant_name")%>' />

                                            <div class="small">
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>' class="small" />
                                            </div>
                                        </div>


                                        <div class="td emi-amount text-left text-lg-right">
                                            <span class="medium-icon">₹  </span>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount","{0:f}")%>' />
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="alert alert-danger" role="alert" id="DivERROR" runat="server">
                                <figure class="icon mr-2">
                                    <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                                </figure>
                                <asp:Label ID="LblErrorMessage" runat="server" Text=""> <%= Constants.RecordNotFound %>   </asp:Label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div class="form-group">
                    <div class="row col-3">
                        <asp:Button runat="server" ID="btnUnsettledView" Text="View More" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                            OnClick="btnUnsettledView_Click" />
                    </div>
                </div>
            </div>
        </div>






        <%--<div class="contactinformation noPadding">
        <h4 id="gridheader" runat="server" style="margin-top: 20px !important">
            List Payment Credit Details</h4>
        <asp:GridView runat="server" Width="100%" ID="grdUnbilledTransactions" AutoGenerateColumns="false"
            CssClass="gridStyle" OnRowDataBound="grdUnbilledTransactions_RowDataBound">
            <AlternatingRowStyle CssClass="secondrow" />
            <RowStyle CssClass="firstrow" />
            <Columns>
                <asp:TemplateField HeaderText="Transaction Date">                   
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
    </div>--%>
        <div class="addcontactdiv" style="display: none">
            <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
                CompactModePageCount="10" GenerateFirstLastSection="True" GenerateGoToSection="True"
                GeneratePagerInfoSection="true" NormalModePageCount="10" PageSize="6" />
        </div>

        <script type="text/javascript">
            $(document).ready(function () {

                $('.custom-control  > input, .custom-control > span > input').addClass('custom-control-input');
                $('.custom-control  > label, .custom-control > span > label').addClass('custom-control-label');          

            });

               $("input[id$='chkAllSelect']").click(function () {
                   $('input:checkbox').not(this).prop('checked', this.checked);                   
                   Getamount();
                });

            function HighlightAllTab() {
                $('#pills-tab a').removeClass('active');
                $('#liAllTab a').addClass('active');
                $("#pills-tabContent>div").removeClass("active show");
                $("#ContentPlaceHolder1_pillsall").addClass("active show");

            }

            function HighlightUnbilledTab() {
                $('#pills-tab a').removeClass('active');
                $('#liUnbilledTab a').addClass('active');
                $("#pills-tabContent>div").removeClass("active show");
                $("#ContentPlaceHolder1_pillsunbilled").addClass("active show");
            }

            function HighlightUnSettledTab() {
                $('#pills-tab a').removeClass('active');
                $('#liUnSettledTab a').addClass('active');
                $("#pills-tabContent>div").removeClass("active show");
                $("#ContentPlaceHolder1_pillsunsettled").addClass("active show");

            }

            //new table design
            $(function () {
                var isXS = false,
                    $accordionXSCollapse = $('.accordion-xs-collapse');

                // Window resize event (debounced)
                var timer;
                $(window).resize(function () {
                    if (timer) { clearTimeout(timer); }
                    timer = setTimeout(function () {
                        isXS = Modernizr.mq('only screen and (max-width: 992px)');

                        // Add/remove collapse class as needed
                        if (isXS) {
                            $accordionXSCollapse.addClass('collapse');
                        } else {
                            $accordionXSCollapse.removeClass('collapse');
                        }
                    }, 100);
                }).trigger('resize'); //trigger window resize on pageload

                // Initialise the Bootstrap Collapse
                $accordionXSCollapse.each(function () {
                    $(this).collapse({ toggle: false });
                });


                // <a href="https://www.jqueryscript.net/accordion/">Accordion</a> toggle click event (live)
                $(document).on('click', '.accordion-xs-toggle', function (e) {
                    //e.preventDefault();                    
                    var $thisToggle = $(this),
                        $targetRow = $thisToggle.parent('.tr'),
                        $targetCollapse = $targetRow.find('.accordion-xs-collapse');

                    if (isXS && $targetCollapse.length) {
                        var $siblingRow = $targetRow.siblings('.tr'),
                            $siblingToggle = $siblingRow.find('.accordion-xs-toggle'),
                            $siblingCollapse = $siblingRow.find('.accordion-xs-collapse');

                        $targetCollapse.collapse('toggle'); //toggle this collapse
                        $siblingCollapse.collapse('hide'); //close siblings

                        $thisToggle.toggleClass('collapsed'); //class used for icon marker
                        $siblingToggle.removeClass('collapsed'); //remove sibling marker class
                    }
                });
            });

            function CheckAll() {
                $("input[type=checkbox]:checked").each(function () {
                    alert('selected: ' + $(this).val());
                });
            }

            function Getamount() {
                var total = 0;
                var netTotal = 0;
                var TotalChecked = 0;

                var favorite = [];                            
                $('#ContentPlaceHolder1_pillsunbilled input[type="checkbox"]').each(function () {
                    if ($(this).prop('checked') == true) {                                               
                        //var hidden = $(this).closest('div').find(':hidden').val();
                        var hidden = $(this).closest('div').find('input:hidden:first').attr('value');
                        if (hidden == undefined) {
                            hidden = 0;
                        }
                        total += parseFloat(hidden);
                        TotalChecked = TotalChecked + 1;                        
                    }
                    else if ($(this).prop('checked') == false) {
                        $("input[id$='chkAllSelect']").prop("checked", false);
                    }
                });
              
                if ($("input[id$='chkAllSelect']").is(":checked")) {
                    TotalChecked = TotalChecked - 1;
                }                
                netTotal = total.toFixed(2);
                document.getElementById('<%=hdnIntTot.ClientID %>').value = total.toFixed(2);

                //if (TotalChecked > 0) {
                //    AjaxCallToGetCharges(TotalChecked);
                //}

                $('#ContentPlaceHolder1_lblamount').text(netTotal);
                $('#ContentPlaceHolder1_lblNoofTransactionSelected').text(TotalChecked);
                if (netTotal < 0) {
                    $("input[id$='btnPayNow']").prop("disabled", true);
                }
                else {
                    $("input[id$='btnPayNow']").prop("disabled", false);
                }
            }
        </script>
</asp:Content>
