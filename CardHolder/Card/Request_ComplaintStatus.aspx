<%@ Page Title="Request_Complaint Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Request_ComplaintStatus.aspx.cs" Inherits="CardHolder.ServiceRequest.Request_ComplaintStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table.tlbForm {
            border-collapse: collapse;
            width: 400px;
        }

            table.tlbForm td {
                border: 1px solid black;
                padding: 5px;
            }

            table.tlbForm th {
                border: 1px solid black;
                padding: 5px;
                font-weight: bold;
                text-align: center;
                background: #EDE8DD;
                color: #434343;
                text-align: left;
                width: 115px;
            }

        #popup_box {
            display: block;
            height: auto !important;
            width: 400px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card mb-4">
        <div class="card-header">
            <h6 class="m-0">Request Status</h6>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="mb-4">
                        <div class="d-label mb-2">Credit Card</div>
                        <div class="alert alert-secondary mb-1">
                            <div class="d-value">
                                <asp:Label runat="server" ID="lblCreditCardNumber" />
                            </div>
                        </div>
                        <div class="text-primary">
                            Name on Card:
                            <asp:Label ID="lblCardHolder" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="">Request</label>
                        <asp:DropDownList runat="server" ID="ddlReqcomplaint" CssClass="form-control form-control-lg custom-select wide" CausesValidation="false">
                            <asp:ListItem Value="-1">---Select---</asp:ListItem>
                            <asp:ListItem Value="0">Request</asp:ListItem>
                            <asp:ListItem Value="1">Complaint</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="error" ID="rfvddlReqComplaint" runat="server"
                            ControlToValidate="ddlReqcomplaint" Display="Dynamic" ErrorMessage="Please select request or complaint"
                            InitialValue="-1"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click"
                        CssClass="btn btn-primary btn-lg text-uppercase" />
                </div>
            </div>
        </div>
    </div>


    <div class="form-group divtable accordion-xs">
        <asp:ListView runat="server" ID="lstViewRequestStatus" OnItemDataBound="lstViewRequestStatus_ItemDataBound">
            <LayoutTemplate>
                <div class="tr headings">
                    <div class="th request-date">Request Date</div>
                    <div class="th request-number">Request Number</div>
                    <div class="th request-type">Request Type</div>
                    <div class="th request-status">Status</div>


                </div>
                <div id="itemPlaceholder" runat="server"></div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="tr">
                    <div class="td request-date accordion-xs-toggle"><%# (Convert.ToDateTime(Eval("Request_Dt"))).ToString("dd MMMM yy")%> </div>
                    <div class="td request-number"><%#Eval("UID")%></div>
                    <div class="accordion-xs-collapse">
                        <div class="inner">
                            <div class="td request-type">
                                <%#Eval("RequestType")%>
                            </div>
                            <div class="td request-status">
                                <figure class="icon mr-2">
                                    <img src="" id="RequestStatusImage" alt="info-icon" width="22" runat="server" />
                                </figure>
                                <span class="text-muted status-label text-uppercase">
                                    <asp:Label ID="lblRequestStatus" runat="server" Text='<%#Eval("Request_Status")%>'></asp:Label>
                                </span>
                            </div>


                            <asp:Panel class="td request-reason" id="DivRequestReason" runat="server" visible="false">
                                    <span><strong>Reason:</strong><%#Eval("UID")%></span>
                                </asp:Panel>
                        </div>
                    </div>
                    <%--<div class="td request-type accordion-xs-toggle">
                            <%#Eval("RequestType")%>                           
                        </div>
                        <div class="td request-status accordion-xs-toggle">
                            <figure class="icon mr-2">
                                <img src="" id="RequestStatusImage" alt="info-icon" width="22" runat="server" />                                
                            </figure>
                            <span class="text-muted status-label text-uppercase">
                                <asp:Label ID="lblRequestStatus" runat="server" Text='<%#Eval("Request_Status")%>'></asp:Label>
                                </span>
                        </div>
                        <div class="accordion-xs-collapse">
                            <div class="inner">
                                <div class="td request-number"><%#Eval("UID")%></div>
                                <div class="td request-date"> <%# (Convert.ToDateTime(Eval("Request_Dt"))).ToString("dd MMMM yy")%> </div>
                                <panel class="td request-reason" id="DivRequestReason" runat="server" visible="false">
                                    <span><strong>Reason:</strong><%#Eval("UID")%></span>
                                </panel>
                            </div>
                        </div>--%>
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





    <div class="form-group divtable accordion-xs">
        <asp:ListView runat="server" ID="lstViewComplaintStatus" OnItemDataBound="lstViewComplaintStatus_ItemDataBound">
            <LayoutTemplate>
                <div class="tr headings">
                    <div class="th request-date">Complaint Date</div>
                    <div class="th request-number">Complaint Number</div>
                    <div class="th request-type">Complaint Type</div>
                    <div class="th request-status">Status</div>

                </div>
                <div id="itemPlaceholder" runat="server"></div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="tr">
                    <div class="td request-date  accordion-xs-toggle"><%# (Convert.ToDateTime(Eval("Complaint_Dt"))).ToString("dd MMMM yy")%> </div>
                    <div class="td request-number"><%#Eval("UID")%></div>


                    <div class="accordion-xs-collapse">
                        <div class="inner">
                            <div class="td request-type">
                                <%#Eval("ComplaintType")%>
                            </div>
                            <div class="td request-status">
                                <figure class="icon mr-2">
                                    <img src="" id="ComplaintStatusImage" alt="info-icon" width="22" runat="server" />
                                </figure>
                                <span class="text-muted status-label text-uppercase">
                                    <asp:Label ID="lblComplaintStatus" runat="server" Text='<%#Eval("Complaint_Status")%>'></asp:Label>
                                </span>
                            </div>
                            <asp:Panel class="td request-reason" id="DivComplaintReason" runat="server" visible="false">
                                    <span><strong>Reason:</strong><%#Eval("UID")%></span>
                                </asp:Panel>
                        </div>
                    </div>
                </div>
                <%--<div class="tr">
                        <div class="td request-type accordion-xs-toggle">
                            <%#Eval("ComplaintType")%>                           
                        </div>
                        <div class="td request-status accordion-xs-toggle">
                            <figure class="icon mr-2">
                                <img src="" id="ComplaintStatusImage" alt="info-icon" width="22" runat="server" />                                
                            </figure>
                            <span class="text-muted status-label text-uppercase">
                                <asp:Label ID="lblComplaintStatus" runat="server" Text='<%#Eval("Complaint_Status")%>'></asp:Label>
                                </span>
                        </div>
                        <div class="accordion-xs-collapse">
                            <div class="inner">
                                <div class="td request-number"><%#Eval("UID")%></div>
                                <div class="td request-date"> <%# (Convert.ToDateTime(Eval("Complaint_Dt"))).ToString("dd MMMM yy")%> </div>
                                <panel class="td request-reason" id="DivComplaintReason" runat="server" visible="false">
                                    <span><strong>Reason:</strong><%#Eval("UID")%></span>
                                </panel>
                            </div>
                        </div>
                    </div>--%>
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



    <%--   <div style="width: 100%">
        <div class="commontitlediv">
            <h3>
                <span>
                    <asp:Literal ID="ltrFormHeader" runat="server" Text="List of Request and Complaints"></asp:Literal>
                </span>
            </h3>
        </div>
        <div class="addcontactdiv">
            <span class="contractno">
                <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
            </span>
        </div>
        <ul class="addUser">
            <li><span class="left commonLabel">
                <asp:Literal ID="litCrCardNumber" runat="server" Text="Credit Card"></asp:Literal>
                : </span><span class="right">
                    <asp:Label runat="server" ID="lblCreditCardNumber" />
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litNmCardHolder" runat="server" Text="Name on Card"></asp:Literal>
                : </span><span class="right">
                    <asp:Label ID="lblCardHolder" runat="server" />
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litComplRequest" runat="server" Text="Request/Complaint"></asp:Literal>
                :<span class="red">*</span> </span><span class="right">
                    <asp:DropDownList runat="server" ID="ddlReqcomplaint" CssClass="myselect" CausesValidation="false">
                        <asp:ListItem Value="-1">---Select---</asp:ListItem>
                        <asp:ListItem Value="0">Request</asp:ListItem>
                        <asp:ListItem Value="1">Complaint</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="error" ID="rfvddlReqComplaint" runat="server"
                        ControlToValidate="ddlReqcomplaint" Display="Dynamic" ErrorMessage="Please select request or complaint"
                        InitialValue="-1"></asp:RequiredFieldValidator>
                </span></li>
            <li><span class="left"></span><span class="right">
                <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click"
                    CssClass="button navbluebtm" />
            </span></li>
        </ul>
    </div>
    <div class="contactinformation noPadding">
        <h4 id="gridheader" runat="server">
            List Request and Complaints</h4>
        <asp:Label ID="lblheaderReqCompl" runat="server"></asp:Label>
        <asp:GridView ID="gvRequestCH" runat="server" OnRowCommand="gvRequestCH_RowCommand"
            Width="100%" AutoGenerateColumns="false" PageSize ="10" >
            <AlternatingRowStyle CssClass="secondrow" />
            <Columns>
                <asp:TemplateField HeaderStyle-Width="80" HeaderText="Request Date">
                    <ItemTemplate>
                        <%# GeneralMethods.FormatDateTime(Convert.ToDateTime(Eval("Request_Dt")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UID" HeaderStyle-Width="100" SortExpression="UID" HeaderText="Request Number" />
                <asp:TemplateField HeaderText="Request Type" HeaderStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="lblrqsttype" runat="server" Text='<%# Eval("RequestType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>             
                    <asp:TemplateField HeaderText="Remarks by Back-Office" HeaderStyle-Width="200">
                    <ItemTemplate>
                         <asp:Label ID="lblAdminRemarks" runat="server" Visible='<%# Eval("Request_Status").ToString()=="Approved"  ? true : Eval("Request_Status").ToString()=="Rejected"  ? true : false  %>'  
                            Text='<%# (Eval("RequestType").ToString()=="ATM PIN Re-Generation Request" && Eval("Request_Status").ToString()=="Approved"  ? "PIN has been set successfully"
                                :(Eval("RequestType").ToString()=="Blocking of Card"  && Eval("Request_Status").ToString()=="Approved" ? "Card has been successfully blocked."
                                :(Eval("Request_Status").ToString()=="Rejected" ? "Ooops Something went wrong.. Please try after sometime." : Eval("AdminRemarks"))))  %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Request Status" HeaderStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="lblrqstStatus" runat="server" Text='<%# (Eval("Request_Status").ToString()=="Approved"  ? "Approved":(Eval("Request_Status").ToString()=="Rejected"  ? "Rejected":"Pending"))  %>'></asp:Label>
                     
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="ImgViewRequest" ImageUrl="~/images/searchicon.png"
                            CommandName="ViewRequest" CausesValidation="false"
                            CommandArgument='<%# Eval("Request_Dtl_Id") + ";" +Eval("Request_Dt") + ";" + Eval("UID") + ";" + Eval("RequestType") + ";" + Eval("Request_Status") + ";" + Eval("Remarks") + ";" + 
                                (Eval("RequestType").ToString()=="ATM PIN Re-Generation Request" && Eval("Request_Status").ToString()=="Approved"  ? "PIN has been set successfully"
                                :(Eval("RequestType").ToString()=="Blocking of Card"  && Eval("Request_Status").ToString()=="Approved" ? "Card has been successfully blocked."
                                :(Eval("Request_Status").ToString()=="Rejected" ? "Ooops Something went wrong.. Please try after sometime." : Eval("AdminRemarks")))) %>' />
                    </ItemTemplate>
                </asp:TemplateField>               
            </Columns>
            <RowStyle CssClass="firstrow" />
        </asp:GridView>
        <asp:GridView ID="gvComplaintCH" runat="server" Width="100%" OnRowCommand="gvComplaintCH_RowCommand"
            AutoGenerateColumns="false" PageSize="10">
            <AlternatingRowStyle CssClass="secondrow" />
            <Columns>
                <asp:TemplateField HeaderStyle-Width="86" HeaderText="Complaint Date">
                    <ItemTemplate>
                        <%# GeneralMethods.FormatDateTime(Convert.ToDateTime(Eval("Complaint_Dt")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UID" HeaderStyle-Width="100" SortExpression="UID" HeaderText="Complaint Number" />
                <asp:BoundField DataField="ComplaintType" HeaderStyle-Width="150" SortExpression="ComplaintType_nm"
                    HeaderText="Complaint Type" />             
                    <asp:TemplateField HeaderText="Remarks by Back-Office" HeaderStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="lblCAdminRemarks" runat="server" Visible='<%# Eval("Complaint_Status").ToString()=="Approved"  ? true : Eval("Complaint_Status").ToString()=="Rejected"  ? true : false  %>'  Text='<%# Eval("AdminRemarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Complaint Status" HeaderStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="lblCrqstStatus" runat="server" Text='<%# (Eval("Complaint_Status").ToString()=="Approved"  ? "Approved":(Eval("Complaint_Status").ToString()=="Rejected"  ? "Rejected":"Pending"))  %>'></asp:Label>
                     
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="ImgView" ImageUrl="~/images/searchicon.png" CommandName="View"
                            CausesValidation="false" CommandArgument='<%# Eval("Complaint_Dtl_Id") + ";" +Eval("Complaint_Dt") + ";" + Eval("UID") + ";" + Eval("ComplaintType") + ";" + Eval("Remarks") + ";" + Eval("Complaint_Status") + ";" + Eval("AdminRemarks")  %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="firstrow" />
        </asp:GridView>
    </div>

    <div id="popup_box">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px">
            <h3>
                Request/Complaint Detail</h3>
            <table cellpadding="5" cellspacing="5" class="tlbForm">
                <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>
                <tr runat="server" id="trStmntType">
                    <th>
                        Abbreviations :
                    </th>
                    <td>
                        <asp:Label ID="lblmodeDesc" runat="server" Text="H: Hard Copy - E: Email - B: Both"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>
    <div class="form-group">
        <div class="row col-3">
            <asp:Button runat="server" ID="btnRequstViewMore" Text="View More" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                OnClick="btnRequestViewMore_Click" />
            <asp:Button runat="server" ID="btnComplaintViewMore" Text="View More" CssClass="btn btn-lg  btn-outline-primary text-uppercase btn-block" CausesValidation="false"
                OnClick="btnComplaintViewMore_Click" />
        </div>
    </div>
    <div class="addcontactdiv" style="display: none">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="4" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="5" PageSize="10" />
        <csc:Pager ID="Pager2" runat="server" EnableViewState="false" OnCommand="pager2_Command"
            CompactModePageCount="4" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="5" PageSize="10" />
    </div>
    <script type="text/javascript">

        //function unloadPopupBox() {
        //    $('#overlay').remove();
        //    $('#popup_box').fadeOut("slow");
        //}

        //function loadPopupBox() {
        //    var docHeight = $(document).height();
        //    $("body").append("<div id='overlay'></div>");
        //    $("#overlay").height(docHeight).css({
        //        'opacity': 0.4,
        //        'position': 'absolute',
        //        'top': 0,
        //        'left': 0,
        //        'background-color': 'black',
        //        'width': '100%',
        //        'z-index': 5000
        //    });
        //    $('#popup_box').fadeIn("slow");
        //}

        ///---
        /// Event Handler Configuration
        ///---
        //$(document).ready(function () {
        //    $('#popupBoxClose').click(function () {
        //        unloadPopupBox();
        //    });

        //});

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
                e.preventDefault();

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



    </script>
</asp:Content>
