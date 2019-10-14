<%@ Page Language="C#" Title="Account Summary" AutoEventWireup="true" CodeBehind="AccountSummary.aspx.cs"
    Inherits="CardHolder.AccountSummary.AccountSummary" MasterPageFile="~/Site.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        div.accountsummarysearch {
            border: 1px solid #D1D0CE;
            margin-top: 70px;
        }

        div.accountsummaryselection {
            margin-left: 30px;
        }

        div.accountsummarysubmit {
            margin-left: 350px;
            margin-bottom: 10px;
        }

        div.accountsummarynote {
            margin-bottom: 10px;
            color: Red;
            margin-left: 30px;
        }

        div.displaynameoncard {
            margin-left: 20px;
            font-weight: bold;
            color: #FE5200;
            margin-top: 10px;
        }

        ul.addUser li {
            width: 850px;
        }

        .auto-style1 {
            width: 410px;
            height: 66px;
        }

        #popup_box {
            display: block;
            height: auto !important;
            width: 400px !important;
        }

        .box {
        }

        .boxCash {
        }
    </style>


    <script type='text/javascript'>
        $(document).ready(function () {
            $(".list-table").mCustomScrollbar({
                theme: "minimal-dark"
            });


            $('#popupBoxClose').click(function () {
                unloadPopupBox();
            });
            /*if ($(window).width() >= 992) {
                $(".modal-table").mCustomScrollbar({
                    theme: "minimal-dark"
                });
            }*/
        });
        function openModal() {
            $('[id*=myModal]').modal('show');
        }

        function unloadPopupBox() {
            $('#overlay').remove();
            $('#popup_box').fadeOut("slow");
        }

        function loadPopupBox() {
            var docHeight = $(document).height();
            $("body").append("<div id='overlay'></div>");
            $("#overlay").height(docHeight).css({
                'opacity': 0.4,
                'position': 'absolute',
                'top': 0,
                'left': 0,
                'background-color': 'black',
                'width': '100%',
                'z-index': 5000
            });
            $('#popup_box').fadeIn("slow");
        }

        function progressBar() {

            var percentageWidth = $('#<%=hideprogressbarWidth.ClientID%>').val() + "%";
            var percentageCashUsedWidth = $('#<%=hidecashProgressBarWidth.ClientID%>').val() + "%";
            $(".box").css("width", percentageWidth);
            $(".boxCash").css("width", percentageCashUsedWidth);
        }

        function HighlightRow() {
            //          $('#gvRequestCH').on('click', 'tbody tr', function (event) {
            //              debugger;
            //  $(this).addClass('HighlightBorder').siblings().removeClass('HighlightBorder');
            //});

            //          //$('#ImgViewRequest').click(function (e) {
            //    debugger;
            //  var rows = getHighlightRow();
            //  if (rows != undefined) {
            //    alert(rows.attr('id'));
            //  }
            ////});

            //         function getHighlightRow() {                  
            //  //return $('table > tbody > tr.HighlightBorder');
            //             $(".table-card-row > tbody > tr").addClass("HighlightBorder");

            $(".table-card-row > tbody > tr").addClass('HighlightBorder').siblings().removeClass('HighlightBorder')
            //$("#gvRequestCH tbody tr").addClass("HighlightBorder");


            //}

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">




    <div class="container-fluid">
        <div class="card-columns dashboard-cards">
            <!--CARD SUMMARY-->
            <div class="card">
                <div class="card-summary">
                    <div class="row  align-items-center mb-2">
                        <div class="col-auto mr-auto">
                            <h2 class="sub-title">Card Summary</h2>
                        </div>
                        <div class="col-auto">
                            <span class="title-date">As on : 
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal><asp:Literal ID="Literal6" runat="server" Text="Card Balance Summary as On:" Visible="false"></asp:Literal></span>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-7 form-group" style="display: none" id="drpCardlist" runat="server">
                                    <asp:DropDownList ID="ddlCardNumber" runat="server" AutoPostBack="true" CssClass="form-control form-control-lg  custom-select wide" OnSelectedIndexChanged="ddlCardNumber_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="ddlCardNumber" Display="Dynamic" ErrorMessage="Please select card"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <%-- <div class="alert alert-secondary mb-1" style="display: none" id="divCardNumber" runat="server">--%>
                                <div class="col-lg-7 form-group" visible="false" id="divCardNumber" runat="server">
                                    <div class="d-value">
                                        Card Number:
                                        <asp:Label runat="server" ID="lblCreditCardNumber" />
                                    </div>
                                </div>
                                <div class="col-lg-5 order-lg-2 order-3 upgrad-card-btn mb-lg-3 upgrade-card-btn-block">
                                    <button type="button" class=" d-none btn btn-outline-primary text-uppercase btn-lg btn-block">Upgrade card</button>
                                </div>
                                <div class="col order-sm-2">
                                    <!--Credit limit-->
                                    <section>
                                        <div>
                                            Credit Limit: <span class="medium-icon">₹</span>
                                            <span>
                                                <label id="lblTotalLimit" runat="server" class="font-weight-semi-bold" />
                                            </span>
                                        </div>
                                        <div class="row">
                                            <div class="col-auto mr-auto ">
                                                <div><small>Used</small></div>
                                                <div><span class="medium-icon">₹</span><label id="lblCreditUsedAmount" runat="server" class="font-weight-semi-bold"></label></div>
                                            </div>
                                            <div class="col-auto text-right">
                                                <div><small>Available</small></div>
                                                <div>
                                                    <span class="medium-icon">₹</span>
                                                    <label id="lblAvailableLimit" runat="server" class="font-weight-semi-bold" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="progress" style="height: 6px;">
                                            <div id="creditLimitProgressBar" class="progress-bar box" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </section>

                                    <!--Cash limit-->
                                    <section>
                                        <div>
                                            Cash Limit: <span class="medium-icon">₹</span>
                                            <span>
                                                <label id="lblTotalCashLimit" runat="server" class="font-weight-semi-bold" />
                                            </span>
                                        </div>
                                        <div class="row">
                                            <div class="col-auto mr-auto ">
                                                <div><small>Used</small></div>
                                                <div>
                                                    <span class="medium-icon">₹</span><label id="lblCashUsedAmount" runat="server" class="font-weight-semi-bold"></label>
                                                </div>
                                            </div>
                                            <div class="col-auto text-right">
                                                <div><small>Available</small></div>
                                                <div>
                                                    <span class="medium-icon">₹</span>
                                                    <label id="lblAvailableCashLimit" runat="server" class="font-weight-semi-bold" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="progress" style="height: 6px;">
                                            <div class="progress-bar boxCash" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </section>

                                    <div class="row">
                                        <div class="col-auto mr-auto  align-items-center">
                                            <figure class="icon">
                                                <img src="<%= this.Page.GetNewImagePath("Rewards.svg") %>" alt="Rewards-icon" />
                                            </figure>
                                            <span>Total points earned</span>
                                        </div>
                                        <div class="col-auto">
                                            <label id="lblClosingBalance" runat="server" class="font-weight-semi-bold" />
                                            points
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

            <!--QUICK TASK-->
            <div class="card order-md-2 order-3">
                <h2 class="sub-title mb-2">Quick Task</h2>
                <div class="quick-tasks">
                    <div class="card">
                        <asp:HyperLink ID="hlnkATM_PIN_REGENERATION" runat="server" onclick="return checkCurrentPath('ATM_PIN_REGENERATION.ASPX');"
                            NavigateUrl="~/ServiceRequest/ATM_PIN_Regeneration.aspx">
                                <figure class="icon">
                                            <img src="<%= this.Page.GetNewImagePath("Change PIN.svg") %>" alt="pin-icon"  />
                                        </figure>                                      
                                            <span>Pin Generation</span>
                        </asp:HyperLink>
                    </div>
                    <div class="card">
                        <asp:HyperLink ID="hlnkBLOCKINGCARD" runat="server" onclick="return checkCurrentPath('BlockingCard.aspx');"
                            NavigateUrl="~/ServiceRequest/BlockingCard.aspx">
                                <figure class="icon">
                                            <img src="<%= this.Page.GetNewImagePath("Block card.svg") %>" alt="card-icon" />
                                        </figure>
                                        
                                            <span>Block Your Card</span>
                        </asp:HyperLink>
                    </div>
                    <div class="card">
                        <asp:HyperLink ID="hlnkInternationalLimitOpenClose" runat="server" onclick="return checkCurrentPath('InternationlLimitOpenClose.aspx');"
                            NavigateUrl="~/ServiceRequest/InternationlLimitOpenClose.aspx">
                                <figure class="icon">
                                            <img src="<%= this.Page.GetNewImagePath("Limit.svg") %>" alt="Limit-icon" />
                                        </figure>                                       
                                            <span>Manage International Limit</span>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>

            <!--REWARD POINTS-->
            <div class="card order-md-3 order-6">
                <div class="reward-point">
                    <div class="row  align-items-center mb-2">
                        <div class="col-auto mr-auto">
                            <h2 class="sub-title">Reward Points</h2>
                        </div>
                        <div class="d-none col-auto"><a href="" class="primary-link link">View Details</a></div>
                    </div>
                    <div id="divPointSummary" runat="server">
                        <div class="card brand-gradient text-white point-details">
                            <div class="card-body">
                                <div class="row  align-items-center">
                                    <div class="col-lg-8 mr-auto mb-2 mb-lg-0">
                                        <div class="h6 font-weight-semi-bold mb-0">
                                            <label id="lblrewardPoints" runat="server" class="font-weight-semi-bold" />
                                            Points as on :
                                        <label id="lblrewardPointDate" runat="server" class="font-weight-semi-bold"></label>
                                        </div>
                                        <p class="font-normal mb-0">
                                            <label id="lblPointsExpiring" runat="server" />
                                            points expiring on
                                        <label id="lblPointsExpiringOnDate" runat="server"></label>
                                        </p>
                                    </div>
                                    <div class="col-lg-4 text-lg-right text-left">
                                        <%--<button type="button" class="btn btn-outline-primary text-uppercase btn-lg">Know More</button>--%>
                                        <asp:LinkButton ID="hlnkBonusPointRedemption" runat="server" CssClass="btn btn-outline-primary text-uppercase btn-lg" OnClientClick="return checkCurrentPath('BonusPointRedemption.aspx');">Redeem Now</asp:LinkButton>                                  
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card grey-card points-summary">
                            <div class="card-body">
                                <div class="orange text-uppercase mb-1">Points Summary</div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="row">
                                            <div class="col-lg-12 col-6">
                                                <label for="">Opening Balance</label>
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-none d-lg-block">
                                                <img src="<%= this.Page.GetNewImagePath("plus.svg") %>" alt="plus" />
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-block d-lg-none">
                                                
                                            </div>
                                            <div class="col-lg-8 col-4 text-lg-left text-md-right">
                                                <label id="lblOpeningBalance" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                            </div>                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="row">
                                            <div class="col-lg-12 col-6">
                                                <label for="">Rewards Earned</label>
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-none d-lg-block">
                                                <img src="<%= this.Page.GetNewImagePath("minu.svg") %>" alt="minus" />
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-block d-lg-none  text-md-right">
                                                <img src="<%= this.Page.GetNewImagePath("plus.svg") %>" alt="plus" />
                                            </div>
                                            <div class="col-lg-8 col-4 text-lg-left text-md-right">
                                                <label id="lblEarnedForMonth" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                            </div>                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="row">
                                            <div class="col-lg-12 col-6">
                                                <label for="">Points Redeemed</label>
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-none d-lg-block">
                                                <img src="<%= this.Page.GetNewImagePath("equal.svg") %>" alt="equal" />
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-block d-lg-none  text-md-right">
                                                <img src="<%= this.Page.GetNewImagePath("minu.svg") %>" alt="minus" />
                                            </div>
                                            <div class="col-lg-8 col-4 text-lg-left text-md-right">
                                                <label id="lblRedeemedForMonth" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                            </div>                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="row">
                                            <div class="col-lg-12 col-6">
                                                <label for="">Closing Balance</label>
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3  d-none d-lg-block">
                                            </div>
                                            <div class="col-lg-4 col-2 order-lg-3 d-block d-lg-none text-md-right">
                                                <img src="<%= this.Page.GetNewImagePath("equal.svg") %>" alt="equal" />
                                            </div>
                                            <div class="col-lg-8 col-4 text-lg-left text-md-right">
                                                <label id="lblClosingBalancePoint" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                            </div>                                            
                                        </div>
                                    </div>
                                </div>


                                <div class="row align-items-end">
                                    <%--<div class="col-md-2 mb-3">
                                        <label for="">Opening Balance</label>
                                        <div class="">
                                            <label id="lblOpeningBalance" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                        </div>
                                    </div>
                                    <div class="col-md-1 mb-3">
                                        <img src="<%= this.Page.GetNewImagePath("plus.svg") %>" alt="plus" /> 
                                    </div>
                                    <div class="col-md-2 mb-3">
                                        <label for="">Rewards Earned</label>
                                        <div class="">
                                            <label id="lblEarnedForMonth" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                        </div>
                                    </div>
                                    <div class="col-md-1 mb-3">
                                        <img src="<%= this.Page.GetNewImagePath("minu.svg") %>" alt="minus" />
                                    </div>
                                    <div class="col-md-2 mb-3">
                                        <label for="">Points Redeemed</label>
                                        <div class="">
                                            <label id="lblRedeemedForMonth" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                        </div>
                                    </div>
                                    <div class="col-md-1 mb-3">
                                        <img src="<%= this.Page.GetNewImagePath("equal.svg") %>" alt="equal" />
                                    </div>
                                    <div class="col-md-2 mb-3">
                                        <label for="">Closing Balance</label>
                                        <div class="">
                                            <label id="lblClosingBalancePoint" runat="server" style="font-size:large" /> <span class="text-muted">pts</span>
                                        </div>
                                    </div>--%>
                                    <%--                   <span class="col-auto mr-auto col-3">Redeemed for the month</span>
      <span class="col-auto mr-auto col-3">Earned for the month
    <span class="col-auto mr-auto col-3">Closing Balance</span></span></div>
                                <div class="col-12">
                                    <span class="col-auto col-3"></span>
                                    <span class="col-auto col-3"><label id="lblRedeemedForMonth" runat="server" /></span>
                                    <span class="col-auto col-3"><label id="lblEarnedForMonth" runat="server" /></span>
                                    <span class="col-auto col-3"><label id="lblClosingBalancePoint" runat="server" /></span>
                                </div>--%>
                                    <%--<div class="col-lg-12">
                                    <div>
                                        <div class="col-auto mr-auto">Opening Balance</div>
                                        <div class="col-auto"><label id="lblOpeningBalance" runat="server" /></div>
                                    
                                        <div class="col-auto mr-auto">Redeemed for the month</div>
                                        <div class="col-auto"><label id="lblRedeemedForMonth" runat="server" /></div>
                                   
                                        <div class="col-auto mr-auto">Earned for the month</div>
                                        <div class="col-auto"><label id="lblEarnedForMonth" runat="server" /></div>
                                    
                                        <div class="col-auto mr-auto">Closing Balance</div>
                                        <div class="col-auto"> <label id="lblClosingBalancePoint" runat="server" /></div>
                                    </div>
                                </div>--%>
                                </div>
                            </div>
                            <%--<div class="card grey-card points-summary">
                        <div class="card-body">
                            <div class="orange text-uppercase mb-1">Points Summary</div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-auto mr-auto">Opening Balance</div>
                                        <div class="col-auto"><label id="lblOpeningBalance" runat="server" /></div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-auto mr-auto">Redeemed for the month</div>
                                        <div class="col-auto"><label id="lblRedeemedForMonth" runat="server" /></div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-auto mr-auto">Earned for the month</div>
                                        <div class="col-auto"><label id="lblEarnedForMonth" runat="server" /></div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-auto mr-auto">Closing Balance</div>
                                        <div class="col-auto"> <label id="lblClosingBalancePoint" runat="server" /></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                        </div>
                    </div>
                    <div id="divNoDataPointSummary" runat="server" visible="false">
                        <div class="card mb-3">
                            <div class="card-body brand-gradient text-white">
                                <div class="row mb-lg-1 mb-3">
                                    <div class="col-1">

                                        <figure class="icon info">
                                            <%--<img src="http://localhost:57685//assets/images/info.svg?2" alt="info-icon" data-toggle="tooltip" data-placement="right" title="" data-original-title="Tooltip on top">--%>
                                            <img src="<%= this.Page.GetNewImagePath("frown-face.svg") %>" alt="frown-face" />
                                        </figure>
                                    </div>
                                    <div class="col-11">
                                        <div class="mb-3">
                                            <h6><%= Constants.EmptyPointSummaryMainText %></h6>
                                        </div>
                                        <div><%= Constants.EmptyPointSummarySubText %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--SERVICE REQUEST STATUS-->
            <div class="card order-4">
                <div>
                    <div class="row  align-items-center mb-2">
                        <div class="col-auto mr-auto">
                            <h2 class="sub-title">Service Request Status</h2>
                        </div>
                        <div class="col-auto">
                            <asp:HyperLink ID="hyperlnkRequest" runat="server" onclick="return checkCurrentPath('Request_ComplaintStatus.aspx');"
                                NavigateUrl="~/ServiceRequest/Request_ComplaintStatus.aspx" CssClass="primary-link link">View All</asp:HyperLink>
                        </div>
                    </div>



                    <div  id="divRequest_ComplaintStatus" runat="server">
                    <asp:GridView ID="gvRequestCH" runat="server" OnRowDataBound="gvRequestCH_RowDataBound" OnRowCommand="gvRequestCH_RowCommand"
                        Width="100%" AutoGenerateColumns="False" ShowHeader="false" CssClass="table-card-row">

                        <Columns>

                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblrqstID" runat="server" Text='<%# Eval("Request_Dtl_Id")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnRequestorComplaint" runat="server" Value='<%#Eval("RequestorComplaint")%>' />
                                </ItemTemplate>

                                <HeaderStyle></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <figure class="icon mr-2">
                                        <img src="" id="StatusImage" alt="info-icon" width="22" runat="server" />
                                    </figure>
                                    <asp:Label ID="lblRequestStatus" runat="server" Text='<%#Eval("Request_Status")%>' CssClass="text-muted status-label text-uppercase"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ImgViewRequest" ImageUrl="~/assets/images/Chevron.svg" CssClass="remove-border-highlight chevron-right" OnClientClick="HighlightRow();"
                                        CommandName="ViewRequest" CausesValidation="false"
                                        CommandArgument='<%# Eval("Request_Dtl_Id") + ";" +Eval("Request_Dt") + ";" + Eval("UID") + ";" + Eval("RequestType") + ";" + Eval("Request_Status") + ";" + Eval("Remarks") + ";" + 
                                                                            (Eval("RequestType").ToString()=="ATM PIN Re-Generation Request" && Eval("Request_Status").ToString()=="Approved"  ? "PIN has been set successfully"
                                                                            :(Eval("RequestType").ToString()=="Blocking of Card"  && Eval("Request_Status").ToString()=="Approved" ? "Card has been successfully blocked."
                                                                            :(Eval("Request_Status").ToString()=="Rejected" ? "Ooops Something went wrong.. Please try after sometime." : Eval("AdminRemarks")))) %>' />
                                    <%--<img src="<%= this.Page.GetNewImagePath("Chevron.svg") %>" alt="arrow right" class="chevron-right" id="ImgViewRequest" runat="server" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <!-- Modal -->
                    <div id="myModal" class="modal fade bd-example-modal-lg" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title font-weight-semi-bold">Service Request Status</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body modal-table">
                                    <table cellpadding="5" cellspacing="5" class="tlbForm">
                                        <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-lg btn-outline-primary text-uppercase" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>

                    </div>
</div>
                    <div  id="divNoDataRequest_ComplaintStatus" runat="server">
                        <div class="card mb-3">
                            <div class="card-body">
                                <div class="row mb-lg-1 mb-3">
                                    <div class="col-1">

                                        <figure class="icon info">
                                            <%--<img src="http://localhost:57685//assets/images/info.svg?2" alt="info-icon" data-toggle="tooltip" data-placement="right" title="" data-original-title="Tooltip on top">--%>
                                            <img src="<%= this.Page.GetNewImagePath("help.svg") %>" alt="help" class="imgsize" />
                                        </figure>
                                    </div>
                                    <div class="col-11">
                                        <div class="mb-3">
                                            <h6><%= Constants.EmptyRequestStatusMainText %></h6>
                                        </div>
                                        <div class="mb-3"><%= Constants.EmptyRequestStatusSubText %></div>
                                         <div>
                            <asp:HyperLink ID="HyperLink1" runat="server" onclick="return checkCurrentPath('Request_ComplaintStatus.aspx');"
                                NavigateUrl="~/ServiceRequest/Request_ComplaintStatus.aspx" CssClass="primary-link link">Request Now</asp:HyperLink>
                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                </div>
            </div>


            <!--BILLING SUMMARY-->
            <div class="card order-md-5 order-2">
                <div class="billing-summary">
                    <div class="row  align-items-center mb-2">
                        <div class="col-auto mr-auto">
                            <h2 class="sub-title">Billing Summary</h2>
                        </div>
                        <div class="col-auto">
                            <span class="title-date">As on :
                                <asp:Literal ID="Literal3" runat="server" Text="Last Bill Summary For Dated:"></asp:Literal></span>
                        </div>
                    </div>

                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="row mb-lg-1 mb-3">
                                <div class="col-auto mr-auto">
                                    Total amount due: 
                                    <figure class="icon info" >
                                        <img src="<%= this.Page.GetNewImagePath("info.svg") %>" alt="info-icon" data-toggle="tooltip" data-placement="right" title="<%=Constants.TooltipTotalAmountDue %>" style="width:13px" />
                                    </figure>
                                </div>
                                <div class="col-auto">
                                    <span class="h5 font-normal"><span class="regular-icon">₹</span>
                                        <label id="lblTotalAmountDue" runat="server" class="font-weight-semi-bold" />
                                    </span>
                                </div>
                            </div>
                            <div class="row mb-lg-1 mb-3">
                                <div class="col-auto mr-auto">
                                    Minimum amount due: 
                                    <figure class="icon info">
                                        <img src="<%= this.Page.GetNewImagePath("info.svg") %>" alt="info-icon" data-toggle="tooltip" data-placement="right" title="<%=Constants.TooltipMinimumAmountDue %>" style="width:13px" />
                                    </figure>
                                </div>
                                <div class="col-auto">
                                    <span class="h5 font-normal"><span class="regular-icon">₹</span>
                                        <label id="lblMinimumAmoutDue" runat="server" class="font-weight-semi-bold" />
                                    </span>
                                </div>
                            </div>
                            <hr />
                            <div class="row mb-lg-1 mb-3">
                                <div class="col-auto mr-auto">Due date:</div>
                                <div class="col-auto">
                                    <span>
                                        <asp:Label ID="lblPaymentDueDate" runat="server"></asp:Label></span>
                                </div>
                            </div>
                            <div class="row mb-lg-1 mb-3">
                                <div class="col-auto mr-auto">Generated on:</div>
                                <div class="col-auto">
                                    <span>
                                        <asp:Label ID="lblStatementDat" runat="server"></asp:Label></span>
                                </div>
                            </div>
                            <div class="text-center pay-now-btn-block">
                                <asp:Button ID="btnpaynowWithouAmt" runat="server" CausesValidation="false" Text="Pay Now"
                                    class="btn btn-primary btn-lg text-uppercase pay-now-btn" OnClick="btnpaynowWithouAmt_Click" />
                            </div>
                        </div>
                    </div>

                    <!--UNBILLED AMOUNT-->
                    <div class="card unbilled-details">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-auto mr-auto">
                                    Unbilled amount: 
                                    <figure class="icon  info">
                                        <img src="<%= this.Page.GetNewImagePath("info.svg") %>" class="bob-tooltip" alt="info-icon" data-toggle="tooltip" data-placement="right" title="<%= Constants.TooltipUnbilledAmount %>" style="width:13px" />
                                    </figure>
                                </div>
                                <div class="col-auto">
                                    <span class="h5"><span class="regular-icon">₹</span>
                                        <label id="lblTotalOutstanding" runat="server" class="font-weight-semi-bold m-0"></label>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card grey-card bill-details">
                        <div class="card-body">
                            <div class="row mb-1">
                                <div class="col-auto mr-auto">Last bill:</div>
                                <div class="col-auto">
                                    <span class="h5 font-normal"><span class="regular-icon">₹</span>
                                        <label id="lblAmountReceived" runat="server" class="font-weight-semi-bold" />
                                    </span>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-auto mr-auto">Bill date: </div>
                                <div class="col-auto">
                                    <asp:Label ID="lblPaymentReceivedDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-auto mr-auto">
                                    <asp:HyperLink ID="hyperlnkViewStateMent" runat="server" onclick="return checkCurrentPath('CARDSTATEMENT.ASPX');"
                                        NavigateUrl="~/Card/CardStatement.aspx" CssClass="primary-link link"> View Statement</asp:HyperLink>
                                </div>
                                <div class="col-auto">
                                    <figure class="icon mr-2">
                                        <img src="<%= this.Page.GetNewImagePath("Success.svg") %>" alt="info-icon" width="22" />
                                    </figure>
                                    <span class="text-muted status-label">PAID</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--RECENT TRANSACTIONS-->
            <div class="card order-md-6 order-5">
                <div>
                    <div class="row  align-items-center mb-2">
                        <div class="col-auto mr-auto">
                            <h2 class="sub-title">Recent Transactions</h2>
                        </div>
                        <div class="col-auto">
                            <asp:HyperLink ID="HyperLinkUnbilledTransaction" runat="server" onclick="return checkCurrentPath('UnbilledTransactions.aspx');"
                                NavigateUrl="~/Card/UnbilledTransactions.aspx" CssClass="primary-link link">View All</asp:HyperLink>
                        </div>
                    </div>
                    <div class="list-table" id="divUnbilledTransaction" runat="server">
                        <asp:GridView runat="server" Width="100%" ID="grdUnbilledTransactions" AutoGenerateColumns="false"
                            CssClass="gridStyle table table-striped" ShowHeader="False" OnRowDataBound="grdUnbilledTransactions_RowDataBound">

                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMerchantName" runat="server" Text='<%# Eval("Merchant_Name") %>'></asp:Label>
                                        <div>
                                            <asp:Label ID="lblTranscationDate" runat="server" Text='<%# ((DateTime)Eval("Transaction_date")) == DateTime.MinValue ? "": GeneralMethods.FormatDate(Convert.ToDateTime(Eval("Transaction_date"))) %>' CssClass="small text-muted"> </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <span class="medium-icon">₹</span>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        <div>
                                            <asp:Image ID="imgCreditTag" runat="server" style="height: 12px;" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>                            
                        </asp:GridView>
                    </div>
                     <div id="divNoDataUnbilledTransaction" runat="server" visible="false">
                    <div class="card mb-3">
                            <div class="card-body">
                                <div class="row mb-lg-1 mb-3">
                                    <div class="col-1">

                                        <figure class="icon info">
                                            <%--<img src="http://localhost:57685//assets/images/info.svg?2" alt="info-icon" data-toggle="tooltip" data-placement="right" title="" data-original-title="Tooltip on top">--%>
                                            <img src="<%= this.Page.GetNewImagePath("payment-failure.svg") %>" alt="payment-failure" class="imgsize" />
                                        </figure>
                                    </div>
                                    <div class="col-11">
                                        <div class="mb-3">
                                            <h6><%= Constants.EmptyRecentTransactionMainText %></h6>
                                        </div>
                                        <div><%= Constants.EmptyRecentTransactionSubText %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                         </div>
                    <div class="addcontactdiv">
                        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" Visible="false"
                            CompactModePageCount="10" GenerateFirstLastSection="True" GenerateGoToSection="True"
                            GeneratePagerInfoSection="true" NormalModePageCount="10" PageSize="10" />
                    </div>
                </div>
            </div>

        </div>

        <!--PROMOTIONS-->
        <div class="row mb-4">
            <div class="col">
                <%--<h2 class="sub-title mb-2">Promotions</h2>--%>

                <div class="promotions row">
                    <div class="col">
                        <a href="https://www.google.com" target="_blank">
                            <div class="card">
                                <img src="<%= this.Page.GetNewImagePath("Promotiongraphic.jpg") %>" class="card-img-top" alt="...">
                                <div class="card-body">
                                    <span>Explore </span>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col">
                        <a href="https://www.google.com" target="_blank">
                            <div class="card">
                                <img src="<%= this.Page.GetNewImagePath("Promotiongraphic.jpg") %>" class="card-img-top" alt="...">
                                <div class="card-body">
                                    <span>Explore </span>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col">
                        <a href="https://www.google.com" target="_blank">
                            <div class="card">
                                <img src="<%= this.Page.GetNewImagePath("Promotiongraphic.jpg") %>" class="card-img-top" alt="...">
                                <div class="card-body">
                                    <span>Explore </span>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col">
                        <a href="https://www.google.com" target="_blank">
                            <div class="card">
                                <img src="<%= this.Page.GetNewImagePath("Promotiongraphic.jpg") %>" class="card-img-top" alt="...">
                                <div class="card-body">
                                    <span>Explore </span>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <asp:HiddenField runat="server" ID="hideCreditCardNumber" />
    <asp:HiddenField runat="server" ID="hideAvaiableAmount" />
    <asp:HiddenField runat="server" ID="hideprogressbarWidth" />
    <asp:HiddenField runat="server" ID="hidecashProgressBarWidth" />




    <!--old code-->

    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>
    </label>

</asp:Content>


