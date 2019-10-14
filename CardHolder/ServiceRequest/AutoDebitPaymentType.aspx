<%@ Page Language="C#" Title="Auto Debit Payment Type" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AutoDebitPaymentType.aspx.cs" Inherits="CardHolder.ServiceRequest.AutoDebitPaymentType" %>

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
    </style>
    <script type="text/javascript">
        ///---
        /// Allow Only float value
        ///---
        function inputControl(input, format) {
            var value = input.val();
            var values = value.split("");
            var update = "";
            var transition = "";
            if (format == 'int') {
                expression = /^([0-9])$/;
                finalExpression = /^([1-9][0-9]*)$/;
            }
            else if (format == 'float') {
                var expression = /(^\d+$)|(^\d+\.\d+$)|[,\.]/;
                var finalExpression = /^([1-9][0-9]*[,\.]?\d{0,3})$/;
            }
            for (id in values) {
                if (expression.test(values[id]) == true && values[id] != '') {
                    transition += '' + values[id].replace(',', '.');
                    if (finalExpression.test(transition) == true) {
                        update += '' + values[id].replace(',', '.');
                    }
                }
            }
            input.val(update);
        }

        function Reset() {
            $("span[id$='cvPaymentType']").html("");
            $("span[id$='cvAgree']").html("");
        }


        $(document).ready(function () {

            $('.payment-type > input, .termsConditions > input, .radio-contorl-table input').addClass('custom-control-input');
            $('.payment-type > label, .termsConditions > label, .radio-contorl-table label').addClass('custom-control-label');
            $("input[id$='txtPercentage']").live("keyup", function () { inputControl($(this), 'float'); });



        });


        function validate() {

            if ($("input[id$='rbTotalAmountDue']").is(':checked') == false && $("input[id$='rbMinimumAmountDue']").is(':checked') == false && $("input[id$='rbPercentage']").is(':checked') == false) {
                $("span[id$='cvPaymentType']").html("Please select type of payment");
                $("span[id$='cvPaymentType']").css('display', 'block');
                $('#<%=divcvPaymentType.ClientID%>').css('display', 'block');

                return false;
            }
            $("span[id$='cvPaymentType']").html("");
            if ($("input[id$='rbPercentage']").is(':checked') == true) {
                if (jQuery.trim($("input[id$='txtPercentage']").val()) == "") {
                    $("span[id$='cvPaymentType']").html("Please enter specific percentage");
                    $("span[id$='cvPaymentType']").css('display', 'block');
                    $('#<%=divcvPaymentType.ClientID%>').css('display', 'block');
                    return false;
                }
            }
            $("span[id$='cvPaymentType']").html("");
            if ($("input[id$='rbPercentage']").is(':checked') == true) {
                var str = jQuery.trim($("input[id$='txtPercentage']").val());
                var x = parseFloat(str);
                if (isNaN(x) || x < 0 || x > 100) {
                    $("span[id$='cvPaymentType']").html("Percentage must be greater than 0 and less than 100");
                    $("span[id$='cvPaymentType']").css('display', 'block');
                    $('#<%=divcvPaymentType.ClientID%>').css('display', 'block');
                    return false;
                }
            }


            $("span[id$='cvPaymentType']").html("");
            if ($("input[id$='chkAgree']").is(':checked') == false) {
                $("span[id$='cvAgree']").html("Please check the terms and conditions checkbox to proceed further.");
                $('#<%=divTermsandCondition.ClientID%>').css('display', 'block');
                $("span[id$='cvAgree']").css('display', 'block');
                return false;
            }
            else if ($("input[id$='chkAgree']").is(':checked') == true) {
                $("span[id$='cvAgree']").html("");
                $('#<%=divTermsandCondition.ClientID%>').css('display', 'block');
                $("span[id$='cvAgree']").css('display', 'block');
            }
            Reset();

        }

        function EditPaymentType() {

            $('#<%=divPaymentType.ClientID%>').css('display', 'block');
            $('#<%=divTermsandCondition.ClientID%>').css('display', 'block');
            $('#<%=divMain.ClientID%>').css('display', 'block');
            $('#<%=divpayment.ClientID%>').css('display', 'block');
        }


        function Show() {          
            if ($("input[id$='rbPercentage']").is(':checked') == true) {
                $('#<%=percentInput.ClientID%>').css('display', 'inline-block');
            }
            else {
                $('#<%=percentInput.ClientID%>').css('display', 'none');
                     $("span[id$='cvPaymentType']").css('display', 'none');
                $('#<%=divcvPaymentType.ClientID%>').css('display', 'none');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Auto Debit Payment Type</h6>
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

                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="alert alert-danger" id="DivMessage" runat="server" style="display: none">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="mb-4">
                        <div class="d-label mb-2">Credit Card</div>
                        <div class="alert alert-secondary mb-1">
                            <div class="d-value">
                                <asp:Label runat="server" ID="lblCardNumber" />
                            </div>
                        </div>
                        <div class="text-primary">
                            Name on Card:
                            <asp:Label runat="server" ID="lblCardHolder" />
                        </div>
                    </div>
                    <div class="mb-4">
                        <div class="d-label mb-2">Credit Card Account Number</div>
                        <div class="alert alert-secondary">
                            <div class="d-value">
                                <asp:Label runat="server" ID="lblAccountNumber" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="mb-4">
                        <div class="d-label mb-2">Bank Account Number</div>
                        <div class="alert alert-secondary">
                            <div class="d-value">
                                <asp:Label ID="lblbnkAccnum" runat="server"></asp:Label><span class="hints"></span>
                            </div>
                        </div>
                    </div>
                    <div class="mb-4">
                        <div style="display: none" id="divStatusMessage" runat="server">
                            <div class="d-value">
                                <asp:Label ID="LblStatusMessage" runat="server"></asp:Label><span class="hints"></span>
                            </div>
                        </div>
                    </div>

                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-primary btn-lg text-uppercase" Text="Edit" OnClick="btnReset_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-12">

                    <div id="divMain" runat="server" style="display: none">
                        <div class="form-group">
                            <div>
                                <div class="col form-group">
                                    <asp:RadioButtonList CssClass="radio-contorl-table custom-radio inline-control" runat="server" OnSelectedIndexChanged="OnRadioEnableDisable_Changed" AutoPostBack="true" ID="RadioEnableDisable" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">Enable</asp:ListItem>
                                        <asp:ListItem Value="1">Disable</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                            </div>
                            <div runat="server" id="divEnable" style="display: none">
                                <div runat="server" id="divpayment">
                                    <label>Payment Type<span class="orange"></span></label>
                                </div>
                                <div class="row">
                                    <div class="col form-group">
                                        <div id="divPaymentType" runat="server">
                                            <div class="custom-control custom-radio inline-control payment-type">
                                                <asp:RadioButton runat="server" ID="rbTotalAmountDue" Text="Total Amount Due" GroupName="PaymentType" onclick="Show()" />
                                                <asp:HiddenField ID="hideTotalAmountDue" Value="1" runat="server" />
                                            </div>
                                            <div class="custom-control custom-radio inline-control payment-type">
                                                <asp:RadioButton runat="server" ID="rbMinimumAmountDue" Text="Minimum Amount Due"
                                                    GroupName="PaymentType" onclick="Show()" />
                                                <asp:HiddenField ID="hideMinimumAmountDue" Value="2" runat="server" />
                                            </div>
                                            <div class="custom-control custom-radio inline-control payment-type">
                                                <asp:RadioButton runat="server" ID="rbPercentage" Text="Specific % of Monthly Due"
                                                    GroupName="PaymentType" onclick="Show()" />
                                                <asp:HiddenField ID="hidePercentage" Value="3" runat="server" />
                                            </div>

                                            <div runat="server" class="custom-control inline-control col-lg-2 col-md-6 col-12 radio-section mt-2 mt-lg-0 pl-0" id="percentInput" style="display: none">
                                                <asp:TextBox runat="server" ID="txtPercentage" MaxLength="5" CssClass="form-control form-control-lg"></asp:TextBox>
                                            </div>

                                            <div id="divcvPaymentType" runat="server" style="display: none">
                                                <asp:Label ID="cvPaymentType" runat="server" CssClass="error"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <div runat="server" id="divTermsandCondition">
                                            <div class="custom-control custom-checkbox form-group termsConditions">
                                                <asp:CheckBox runat="server" ID="chkAgree" Text="I accept the" />
                                                <a target="_blank" href="../terms_conditions.htm#request7">Terms & Conditions<span class="orange"></span></a>
                                                <div>
                                                    <asp:Label ID="cvAgree" runat="server" CssClass="error"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="mb-4">
                                                <asp:Button runat="server" ID="btnSubmitfinal" CssClass="btn btn-primary btn-lg text-uppercase" Style="display: none" OnClick="btnSubmit_Click"
                                                    OnClientClick="return validate();" Text="Submit" />
                                                <asp:HiddenField runat="server" ID="hideRequestTypeId" Value="" />
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="lbldivDeregister" style="display: none" runat="server">
                                        <div runat="server">
                                            <label>Discontinue using Auto Debit Payment?<span class="orange"></span></label>
                                        </div>
                                        <div class="row de-register-box" id="divDeregister" runat="server">
                                            <div class="col-12 form-group">
                                                <label>Reason<span class="orange">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlReasons" CssClass="form-control form-control-lg custom-select wide">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator CssClass="error" ID="rfvddlReason" runat="server" ControlToValidate="ddlReasons"
                                                    Display="Dynamic" ValidationGroup="LoginFrame" ErrorMessage="Please select Reason" InitialValue="-1"></asp:RequiredFieldValidator>
                                                <asp:Label runat="server" ID="lblErrorReasons" CssClass="error" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Button ID="btncancel" runat="server" CssClass="btn btn-primary btn-lg text-uppercase mr-3" Text="Cancel" CausesValidation="false"
                                                    OnClick="btncancel_Click" />
                                                <asp:Button runat="server" ID="btnDeregister" ValidationGroup="LoginFrame" CssClass="btn btn-primary btn-lg text-uppercase mr-3" OnClick="btnDeregister_Click" Text="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>
    </div>







</asp:Content>
