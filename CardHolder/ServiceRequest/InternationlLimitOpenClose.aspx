<%@ Page Language="C#" Title="International Limit " MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="InternationlLimitOpenClose.aspx.cs" Inherits="CardHolder.ServiceRequest.InternationlLimitOpenClose" %>

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
                width: 160px;
            }

        .appFormsubmtpop {
            width: 440px !important;
        }

        .button {
        }

        .auto-style2 {
            width: 220px;
        }
    </style>
    <script type="text/javascript">


        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }





        $(document).ready(function () {



            $('.internationalUsage > input, .custom-control > span > input').addClass('custom-control-input');
            $('.internationalUsage > label, .custom-control > span > label').addClass('custom-control-label');

            var loading = $(".loading");
            loading.hide();

            $('#<%=btnSave.ClientID%>').click(function () {                

                if ($('#<%= internationalUsage.ClientID %>').is(':checked')) {
                    var amt = parseInt($('#<%=txtAmount.ClientID%>').val());
                    var creditLimitAmount = parseInt($('#<%=hidelitInternationalMaxLimitAmount.ClientID%>').val());

                    if (amt != null && amt != '') {

                        $('#<%=LblAmoutErrorMessage.ClientID%>').css('display', 'none');
                        $('#<%=LblError.ClientID%>').css('display', 'none');

                        if (amt > creditLimitAmount) {

                            $('#<%=LblInterntionalLimitErrorMessage.ClientID%>').html("The International Usage Limit cannot exceed the total Card limit");
                           $('#<%=LblInterntionalLimitErrorMessage.ClientID%>').css('display', 'block');
                           return false;
                       }
                       else {

                           $('#<%=LblAmoutErrorMessage.ClientID%>').css('display', 'none');
                           $('#<%=LblError.ClientID%>').css('display', 'none');
                            //ShowProgress();
                            return true;
                        }
                    }
                    else {

                        $('#<%=LblAmoutErrorMessage.ClientID%>').html("Please Enter valid Amount");
                        $('#<%=LblAmoutErrorMessage.ClientID%>').css('display', 'inline-block');
                        $('#<%=LblInterntionalLimitErrorMessage.ClientID%>').css('display', 'none');
                        return false;
                    }

                }
                else if (!$('#<%= internationalUsage.ClientID %>').is(':checked')) {
                    $('#divInternationalLimit').css('display', 'none');
                    $('#<%=LblAmoutErrorMessage.ClientID%>').css('display', 'none');
                    //ShowProgress();
                }
            });








            $("input").keyup(function () {

                var amtrs = parseInt($('#<%=txtAmount.ClientID%>').val());
                $('#<%=lblTxtRuppesMessage.ClientID%>').html(inWords(amtrs));
                $('#<%=lblTxtRuppesMessage.ClientID%>').css('display', 'block');
            });

        });
        function hideErrorMeassage() {
            $('#<%=LblAmoutErrorMessage.ClientID%>').css('display', 'none');
        }

        function inWords(num) {
            var a = ['', 'one ', 'two ', 'three ', 'four ', 'five ', 'six ', 'seven ', 'eight ', 'nine ', 'ten ', 'eleven ', 'twelve ', 'thirteen ', 'fourteen ', 'fifteen ', 'sixteen ', 'seventeen ', 'eighteen ', 'nineteen '];
            var b = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
            if ((num = num.toString()).length > 9) return 'overflow';
            n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + '' : '';
            str = str.charAt(0).toUpperCase() + str.substr(1).toLowerCase();
            $('#<%=lblTxtRuppesMessage.ClientID%>').html(str);
            return str;

        }
    </script>
    <script type="text/javascript">
        /*
    Ref:
    http://www.jqueryscript.net/demo/Buttons-with-Built-in-Loading-Indicators-For-Bootsrap-3-Ladda-Bootstrap/
    */

        //$(window).load(function () {
        $(document).ready(function () {
            var buttons = document.querySelectorAll('.ladda-button');

            Array.prototype.slice.call(buttons).forEach(function (button) {

                var resetTimeout;

                button.addEventListener('click', function () {

                    if (typeof button.getAttribute('data-loading') === 'string') {
                        button.removeAttribute('data-loading');
                    }
                    else {
                        button.setAttribute('data-loading', '');
                    }

                    clearTimeout(resetTimeout);
                }, false);

            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="m-0">Manage International Limit</h6>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-primary" role="alert" id="DivSuccess" runat="server" style="display: none">
                        <figure class="icon mr-2">
                            <img src="<%= this.Page.GetNewImagePath("success.svg") %>" alt="info-icon" width="22">
                        </figure>
                        <asp:Label ID="LblSuccessMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="alert alert-danger" role="alert" id="DivERROR" runat="server" style="display: none">
                        <figure class="icon mr-2">
                            <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22">
                        </figure>
                        <asp:Label ID="LblErrorMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="alert alert-danger" id="DivMessage" runat="server" style="display: none">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>


            <%--        <div class="col-lg-6 blue-card" style="padding: .75rem 1.25rem;">
                    Your International Spend Limit is <span class="medium-icon">₹  </span><%= hidelitInternationalMaxLimitAmount.Value %>
                </div>

                <div class="col-lg-6 blue-card mb-4">
                    <small>You may enable or disable international usage on your card</small>
                </div>
            --%>
            <div class="col-lg-6 blue-card mb-4 padding">
                <div class="font-weight-bold">
                    Your International Spend Limit is <span class="medium-icon">₹  </span><%= hidelitInternationalMaxLimitAmount.Value %>
                </div>

                <div>
                    <small>You may enable or disable international usage on your card</small>
                </div>
            </div>


            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label for="ddlcardlist">Credit Card <span class="orange"></span></label>
                        <asp:DropDownList ID="ddlcardlist" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlcardlist_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div class="form-text text-primary">
                            <asp:Literal ID="Literal1" runat="server" Text="Name on Card:"></asp:Literal>
                            <asp:Label ID="lblCardHolder" runat="server" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="custom-control custom-checkbox">
                            <asp:CheckBox runat="server" ID="internationalUsage" Text="Enable International Usage" AutoPostBack="true" class="checkbox" aria-describedby="internationalUsageHelp" OnCheckedChanged="CheckedChanged" />
                        </div>
                        <small id="internationalUsageHelp" class="form-text">
                            <asp:Label ID="LblActivateDeactivateMsg" runat="server" Text=""></asp:Label></small>
                    </div>

                    <div class="form-group set-limit" id="divInternationalLimit" runat="server">
                        <label for="">International Limit<span class="orange"></span></label>
                        <div class="input-group input-group-lg">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="">₹</span>
                            </div>
                            <input style="display: none" value="" type="text" name="txtAmount" maxlength="15" id="txtAmount" class='form-control' onkeypress="javascript:return isNumber(event)" runat="server" />
                            <asp:Label runat="server" Style="display: none;" ID="LblError" CssClass="error"></asp:Label>
                            <asp:Label runat="server" Style="display: none;" ID="LblAmoutErrorMessage" CssClass="error"></asp:Label>
                            <asp:Label runat="server" Style="display: none;" ID="LblInterntionalLimitErrorMessage" CssClass="error"></asp:Label>
                        </div>
                        <small class="text-primary">
                            <asp:Label ID="lblTxtRuppesMessage" Style="display: none" runat="server"></asp:Label></small>
                    </div>
                    <div class="mb-4" style="display: none;" runat="server" id="divReset">
                        <small>To <strong>edit</strong> the International Limit, please click on Reset.</small><br />
                        <small>The service will be deactivated and activated to alter the above amount.</small>
                    </div>
                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-outline-primary btn-lg text-uppercase mr-2" Text="Reset" OnClick="btnReset_Click" />
                   
                  <%--  <button type="button" class="btn btn-primary btn-lg text-uppercase ladda-button" id="btnSave" style="display: none" onserverclick="btnSave_Click" runat="server" data-style="expand-left">
                        <span class="ladda-label">Submit</span><div class="ladda-progress" style="width: 0px;"></div>
                    </button>--%>
                  <button type="button" class="btn btn-primary btn-lg text-uppercase ladda-button expand-left" id="btnSave" onserverclick="btnSave_Click" runat="server"><span class="label">Submit</span> <span class="spinner"></span></button>
                     

                    <asp:HiddenField runat="server" ID="hideRequestTypeId" />
                    <asp:HiddenField runat="server" ID="hideCreditCardNumber" />
                    <asp:HiddenField runat="server" ID="hideInternationalLimit" />
                    <asp:HiddenField runat="server" ID="hidelitInternationalMaxLimitAmount" />
                    <asp:HiddenField runat="server" ID="hdnAmount" />

                    <!--after save-->
                    <div class="mb-4" style="display: none;" runat="server">
                        <div class="alert alert-secondary mb-0">
                            <div class="d-value">International Limit Amount</div>
                            <div class="d-label">
                                ₹
                                <asp:Label ID="Label4" runat="server" />
                            </div>
                        </div>
                        <small class="text-primary">Rupee One lakh</small>
                    </div>


                    <%--<button class="btn btn-primary btn-lg text-uppercase">Reset</button>--%>
                </div>
            </div>
        </div>


        <div class="loading" id="loaderId" style="display: none">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-body text-center">
                        <h6 class="text-uppercase">Please Wait</h6>
                        <p class="m-0">Your request is being processed</p>
                        <figure class="m-0">
                            <img src="../images/index.ajax-spinner-preloader.gif" alt="" />
                        </figure>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
