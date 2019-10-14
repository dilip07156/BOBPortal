<%@ Page Language="C#" Title="Statement Delivery Mode" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="StatementDeliveryMode.aspx.cs" Inherits="CardHolder.ServiceRequest.StatementDeliveryMode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .appFormsubmtpop {
            width: 440px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!--Statement Delivery Mode-->




    <div class="card mb-4">
        <div class="card-header">
            <h6>Statement Delivery Mode</h6>
            <small>Please select the mode by which you want to receive the statement</small>
        </div>
        <asp:CustomValidator ID="CustomValidator1" OnServerValidate="DateValidate" ErrorMessage="From date cannot be greater than To date.Please select properly."
            runat="server" ValidationGroup="submit" CssClass="error" Display="Dynamic" />
        <div class="card-body">
            <%-- <asp:CustomValidator ID="CustomValidator1" OnServerValidate="DateValidate" ErrorMessage="From date cannot be greater than To date.Please select properly."
            runat="server" ValidationGroup="submit" CssClass="error" Display="Dynamic" />--%>
            <div class="alert alert-danger" role="alert" id="DivERROR" runat="server" style="display: none">
                <figure class="icon mr-2">
                    <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22"></figure>
                <asp:Label ID="LblErrorMessage" runat="server" Text=""></asp:Label>
            </div>
            <div class="row">
                <%--<div class="col-6 mb-3">
                      <label for="ddlCreditCardAccount">Credit Card</label>                        
                    <asp:DropDownList ID="ddlCreditCardAccount" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true">
                    </asp:DropDownList>



                    <div>
                        <div class="form-text text-primary">
                            Name on Card :
                            <asp:Label ID="lblCardHolder" runat="server" />
                        </div>
                    </div>
                </div>--%>
                <div class="col-md-5 mb-3">
                    <div class="form-group">
                        <label for="">Credit Card:<span class="orange"></span></label>
                        <asp:DropDownList ID="ddlCreditCardAccount" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true">
                            
                        </asp:DropDownList>
                        <div id="Small1" class="form-text text-primary">
                            Name on Card:
                            <asp:Label ID="lblCardHolder" runat="server" />
                        </div>
                    </div>
                </div>

            </div>
            <div class="row align-items-end">

                <div class="col-md-5 mb-3">
                    <label for="">From</label>
                    <div class="input-group  input-group-lg date-input-group">
                        <%--<input data-date-format="dd/mm/yyyy" id="" type="text" class="form-control datepicker" placeholder="Select Date" aria-label="" aria-describedby="">--%>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <div class="input-group-append">
                            <span class="input-group-text" id="">
                                <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>" alt="calendar-icon">
                            </span>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="txtFromDate"
                        Font-Size="11px" ForeColor="Red" ErrorMessage="Please select 'From' date" ValidationGroup="submit" Display="Dynamic" />
                </div>
                <div class="col-md-5 mb-3">
                    <label for="">To</label>
                    <div class="input-group  input-group-lg date-input-group">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <%--<input data-date-format="dd/mm/yyyy" id="" type="text" class="form-control datepicker" placeholder="Select Date" aria-label="" aria-describedby="">--%>
                        <div class="input-group-append">
                            <span class="input-group-text" id="">
                                <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>" alt="calendar-icon">
                            </span>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ID="reqToDate" ControlToValidate="txtToDate"
                        Font-Size="11px" ForeColor="Red" ErrorMessage="Please select 'To' date" ValidationGroup="submit" Display="Dynamic" />
                </div>
            </div>
            <div class="row">
                <div class="col-12 form-group">
                    <div>
                        <label>Statement Mode</label>
                    </div>
                    <div class="checkbox-control-table custom-checkbox inline-control">
                        <asp:CheckBoxList runat="server" ID="chkMode" RepeatDirection="Horizontal">
                            <asp:ListItem Value="H" Text=" Hard Copy ">
                            </asp:ListItem>
                            <asp:ListItem Value="E" Text=" E-Statement">
                            </asp:ListItem>

                        </asp:CheckBoxList>
                        <div class="statmentchkboxlist">
                            <span id="Errochkmode"></span>

                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click"
                        CssClass="btn btn-primary btn-lg text-uppercase" OnClientClick="return validate();" ValidationGroup="submit" />
                    <asp:HiddenField runat="server" ID="hideRequestTypeId" Value="6" />
                </div>

                <!--old code label-->
                <div>
                    <asp:Label runat="server" ID="lblMessage" CssClass="error" />
                </div>


            </div>
        </div>
    </div>










    <script type="text/javascript">
        var Isvalid = false;
        function validate() {
            //if (!$("input[id$='chkAgree']").is(':checked')) {
            //    alert("Please go through terms and conditions before agreeing it");
            //    return false;
            //}
            //else {
            ValidateColorList();
            //}
        }

        function ValidateColorList() {
            var chkListModules = document.getElementById('<%= chkMode.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    Isvalid = true;
                    return;
                }
            }
            $("span[id$='Errochkmode']").html("Please select atleast one mode for statement");
            Isvalid = false;
            return false;

        }



        $(document).ready(function () {

            $('.checkbox-control-table input').addClass('custom-control-input');
            $('.checkbox-control-table label').addClass('custom-control-label');

            //$("input[id$='btnconfirm']").click(function () {

            //    ValidateColorList();
            //    if (Isvalid == true) {
            //        $("span[id$='Errochkmode']").html("");
            //        var docHeight = $(document).height();
            //        $("body").append("<div id='overlay'></div>");
            //        $("#overlay").height(docHeight).css({
            //            'opacity': 0.4,
            //            'position': 'absolute',
            //            'top': 0,
            //            'left': 0,
            //            'background-color': 'black',
            //            'width': '100%',
            //            'z-index': 5000
            //        });
            //        loadPopupBox();
            //    }
            //});


            //$('#popupBoxClose').click(function () {
            //    unloadPopupBox();
            //});
            //$("input[id$='btnNo']").click(function () {
            //    unloadPopupBox();
            //});

            //add classes to checkbox for custom design
            //$('#ContentPlaceHolder1_chkMode span').addClass('custom-control custom-checkbox inline-control');
            $('#ContentPlaceHolder1_chkMode span > input').addClass('custom-control-input');
            $('#ContentPlaceHolder1_chkMode span > label').addClass('custom-control-label');

            //datepicker
            $('.datepicker').datepicker({
                autoclose: true,
                todayHighlight: true,
            });
        });
        //function unloadPopupBox() {
        //    $('#overlay').remove();
        //    $('#popup_box').fadeOut("slow");
        //}

        //function loadPopupBox() {
        //    $('#popup_box').fadeIn("slow");
        //}

        function Showalert() {
            alert('Your Request for statement has been successfully registered');
        }

        ///---
        /// Agree Checkbox
        ///---
        //function CheckBoxRequired_ClientValidate(sender, e) {
        //    e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        //}



        $('[id*=txtFromDate]').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('[id*=txtToDate]').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });



    </script>
</asp:Content>
