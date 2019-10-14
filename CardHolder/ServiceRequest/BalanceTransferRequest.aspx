<%@ Page Title="Balance Transfer Request" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="BalanceTransferRequest.aspx.cs" Inherits="CardHolder.ServiceRequest.BalanceTransferRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .appFormsubmtpop h3 { color: #FE5200; font-size: 18px; font-weight: normal; line-height: 24px; padding-bottom: 12px; width: 300px !important; }
        
        ul.addUser li span.left { width: 200px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%">
        <asp:HiddenField ID="hideRequestTypeId" runat="server" />
        <div class="commontitlediv">
            <h3>
                <span>
                    <asp:Literal ID="ltrFormHeader" runat="server" Text="Balance Transfer Request"></asp:Literal>
                </span>
            </h3>
        </div>
        <div class="addcontactdiv">
            <span class="contractno">
                <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
            </span>
        </div>
    </div>
    <div style="width: 100%; float: left">
        <ul class="addUser">
            <li><span class="left commonLabel">
                <asp:Literal ID="litCrCardNumber" runat="server" Text="Credit Card Number"></asp:Literal>
                : </span><span class="right">
                    <asp:Label runat="server" ID="lblCreditCardNumber" />
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litNmCardHolder" runat="server" Text="Name of Card-Holder"></asp:Literal>
                : </span><span class="right">
                    <asp:Label ID="lblCardHolder" runat="server" />
                </span></li>
            <li style="width: 800px"><span class="left commonLabel">
                <asp:Literal ID="litOtherCrCardnumber" runat="server" Text=" Other Credit Card Number"></asp:Literal>:<span
                    class="red">*</span> </span><span class="right">
                        <asp:TextBox runat="server" ID="txtCRnum1" MaxLength="4" Width="40"></asp:TextBox>
                        -
                        <asp:TextBox runat="server" ID="txtCRnum2" MaxLength="4" Width="40"></asp:TextBox>
                        -
                        <asp:TextBox runat="server" ID="txtCRnum3" MaxLength="4" Width="40"></asp:TextBox>
                        -
                        <asp:TextBox runat="server" ID="txtCRnum4" MaxLength="4" Width="40"></asp:TextBox>
                    </span>
                <asp:CustomValidator ForeColor="Red" EnableClientScript="true" ID="cvCreditCard"
                    runat="server" CssClass="error" ValidationGroup="BalanceTransfer" ErrorMessage="Please enter valid credit card number"
                    ClientValidationFunction="CreditCardValidate" ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
            </li>
            <li style="width: 800px"><span class="left commonLabel">
                <asp:Literal ID="litRecnfrmCardNumber" runat="server" Text=" Reconfirm Credit Card Number"></asp:Literal>:<span
                    class="red">*</span> </span><span class="right">
                        <asp:TextBox runat="server" ID="txtRcnfrmCrnum1" MaxLength="4" Width="40"></asp:TextBox>
                        -
                        <asp:TextBox runat="server" ID="txtRcnfrmCrnum2" MaxLength="4" Width="40"></asp:TextBox>
                        -
                        <asp:TextBox runat="server" ID="txtRcnfrmCrnum3" MaxLength="4" Width="40"></asp:TextBox>
                        -
                        <asp:TextBox runat="server" ID="txtRcnfrmCrnum4" MaxLength="4" Width="40"></asp:TextBox>
                    </span>
                <asp:CustomValidator ForeColor="Red" EnableClientScript="true" ID="cvConfrmCreditCard"
                    runat="server" CssClass="error" ValidationGroup="BalanceTransfer" ErrorMessage="Credit card number is not matched"
                    ClientValidationFunction="CreditCardValidateConfirm" ValidateEmptyText="true"
                    Display="Dynamic"></asp:CustomValidator>
            </li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litIssueingBank" runat="server" Text="Name Of Issuing Bank"></asp:Literal>
                :<span class="red">*</span> </span><span class="right">
                    <asp:DropDownList runat="server" ID="ddlIssueBank" ValidationGroup="BalanceTransfer"
                        CssClass="myselect" CausesValidation="false">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ValidationGroup="BalanceTransfer" CssClass="error" ID="rfvddlReason"
                        runat="server" ControlToValidate="ddlIssueBank" Display="Dynamic" ErrorMessage="Please select Issuing Bank"
                        InitialValue="-1"></asp:RequiredFieldValidator>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litAmountTransfered" runat="server" Text="Amount to be Transfered(in INR)"></asp:Literal>
                :<span class="red">*</span> </span><span class="right">
                    <asp:TextBox ID="txtAmtTransfered" onkeypress="return numbersonly(this, event)" MaxLength="15" ValidationGroup="BalanceTransfer" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="error" ID="rfvamtTransfered" runat="server"
                        ControlToValidate="txtAmtTransfered" Display="Dynamic" ValidationGroup="BalanceTransfer"
                        ErrorMessage="Please enter amount"></asp:RequiredFieldValidator>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litPlan" runat="server" Text="Select Plan"></asp:Literal>
                :</span><span class="right">
                    <asp:DropDownList runat="server" ID="ddlPLan" CssClass="myselect" CausesValidation="false">
                    </asp:DropDownList>
                    <a href="#">Plan Details</a>
                    <%--<asp:RequiredFieldValidator CssClass="error" ID="rfvPlan" runat="server" ControlToValidate="ddlPlan"
                        Display="Dynamic" ErrorMessage="Please select Plan" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                </span></li>
            <li style="width: 800px">
                <asp:CheckBox runat="server" ID="chkAgree" Text="       I hereby agree to the Balance transfer "
                    Checked="false" />
                <a target="_blank" href="../terms_conditions.htm#request11">Terms & Conditions</a><span class="red">*</span>
                <asp:CustomValidator ForeColor="Red" runat="server" ID="cvchkAgree" ValidationGroup="BalanceTransfer"
                    ClientValidationFunction="ValidateCheckBox" CssClass="error" ErrorMessage="You must select this box to proceed"
                    Display="Dynamic" />
            </li>
            <li><span class="left"></span><span class="right">
                <asp:Button ID="btnProceed" runat="server" Text="Proceed" ValidationGroup="BalanceTransfer"
                    CssClass="button" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" OnClick="btnReset_Click"
                    CssClass="button" />
            </span></li>
        </ul>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box" style="width: 365px; height: 165px">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding-top: 0px;">
            <center>
                <h3>
                    <span style="text-align: center">Balance Transfer Details </span>
                </h3>
                <div id="divDesc" style="width: 300px; text-align: center; font-size: 15px;">
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </div>
                <div class="balancePop">
                    <asp:Button ID="btnsubmit" runat="server" Text="Confirm" ValidationGroup="BalanceTransfer"
                        OnClick="btnSubmit_Click" CssClass="button navbluebtm" />
                    <asp:Button ID="btncncl" runat="server" Text="Cancel" CssClass="button" />
                </div>
            </center>
        </div>
    </div>
    <script type="text/javascript">

        var checkforValidation = "0";
        var ConfirmCrCardCheck = "0";
        var CreditCardCheck = "0";




        ///---
        /// Credit Card Number Skipping
        ///---
        $("input[id$='txtCRnum1']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
            if ($(this).val().length == 4) {
                $("input[id$='txtCRnum2']").focus();
            }
        });
        ///---
        /// Credit Card Number Skipping
        ///---
        $("input[id$='txtCRnum2']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
            if ($(this).val().length == 4) {

                $("input[id$='txtCRnum3']").focus();
            }
        });
        ///---
        /// Credit Card Number Skipping
        ///---
        $("input[id$='txtCRnum3']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
            if ($(this).val().length == 4) {

                $("input[id$='txtCRnum4']").focus();
            }
        });
        ///---
        /// Credit Card Number Skipping
        ///---
        $("input[id$='txtCRnum4']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });


        ///---
        /// Confirm Credit Card Number Skipping
        ///---
        $("input[id$='txtRcnfrmCrnum1']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
            if ($(this).val().length == 4) {
                $("input[id$='txtRcnfrmCrnum2']").focus();
            }
        });
        ///---
        ///Confirm Credit Card Number Skipping
        ///---
        $("input[id$='txtRcnfrmCrnum2']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
            if ($(this).val().length == 4) {

                $("input[id$='txtRcnfrmCrnum3']").focus();
            }
        });
        ///---
        /// Confirm Credit Card Number Skipping
        ///---
        $("input[id$='txtRcnfrmCrnum3']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
            if ($(this).val().length == 4) {

                $("input[id$='txtRcnfrmCrnum4']").focus();
            }
        });
        ///---
        ///Confirm Credit Card Number Skipping
        ///---
        $("input[id$='txtRcnfrmCrnum4']").keydown(function (event) {
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                return;
            } else {
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });




        function ValidateCheckBox(sender, args) {

            if (document.getElementById("<%=chkAgree.ClientID %>").checked == true) {
                args.IsValid = true;
                checkforValidation = "0";
            } else {
                args.IsValid = false;
                checkforValidation = "1";
            }
        }

        //        ///---
        //        /// Agree Checkbox
        //        ///---
        //        function CheckBoxRequired_ClientValidate(sender, e) {
        //            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        //        }


        ///---
        /// Credit Card Number Validation
        ///---

        var txt1 = document.getElementById("<%= txtCRnum1.ClientID %>");
        var txt2 = document.getElementById("<%= txtCRnum2.ClientID%>");
        var txt3 = document.getElementById("<%= txtCRnum3.ClientID%>");
        var txt4 = document.getElementById("<%= txtCRnum4.ClientID%>");

        //For Reconfirmation
        var txt5 = document.getElementById("<%= txtRcnfrmCrnum1.ClientID %>");
        var txt6 = document.getElementById("<%= txtRcnfrmCrnum2.ClientID%>");
        var txt7 = document.getElementById("<%= txtRcnfrmCrnum3.ClientID%>");
        var txt8 = document.getElementById("<%= txtRcnfrmCrnum4.ClientID%>");

        function CreditCardValidate(sender, args) {

            var cc_number = txt1.value + txt2.value + txt3.value + txt4.value;
            if (cc_number.length != 16) {
                args.IsValid = false;
                CreditCardCheck = "1";
            }
            else
                CreditCardCheck = "0";

        }

        function CreditCardValidateConfirm(sender, args) {

            var cc_number = txt1.value + txt2.value + txt3.value + txt4.value;
            var cc_Confirm_number = txt5.value + txt6.value + txt7.value + txt8.value;
            if (cc_number != cc_Confirm_number) {
                args.IsValid = false;
                ConfirmCrCardCheck = "1";
            }
            else
                ConfirmCrCardCheck = "0";
        }


        //For POPUP BOX

        $(document).ready(function () {
            $("input[id$='btnProceed']").click(function () {
                $("span[id$='lblMessage']").html("");
                if ($("input[id$='txtAmtTransfered']").val() == "") {
                    return false;
                }


                // var bankname = $("#ContentPlaceHolder1_ddlIssueBank:selected").text();
                var bankname = $("select[id$='ddlIssueBank'] option:selected").text();
                var Creditcardnumber = $("input[id$='txtCRnum1']").val() + $("input[id$='txtCRnum2']").val() + $("input[id$='txtCRnum3']").val() + $("input[id$='txtCRnum4']").val()
                var cardHoldername = $("span[id$='lblCardHolder']").html();

                if (bankname == "" || Creditcardnumber == "" || cardHoldername == "") {
                    return false;
                }

                if (checkforValidation == "1" || CreditCardCheck == "1" || ConfirmCrCardCheck == "1") {
                    return false;
                }

                var Message = "DD/Cheque will be dispatched in the name of " + bankname + " , " + Creditcardnumber + " , " + cardHoldername + " to your address. ";
                

                $("div[id$='divDesc']").html(Message);
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
                loadPopupBox();
                return false;
            });




            $('#popupBoxClose').click(function () {
                unloadPopupBox();
            });
            $("input[id$='btncncl']").click(function () {
                unloadPopupBox();
                return false;
            });

            function unloadPopupBox() {
                $('#overlay').remove();
                $('#popup_box').fadeOut("slow");
            }

            function loadPopupBox() {
                $('#popup_box').fadeIn("slow");
            }


        });


        function Showalert() {
            alert('Your Request for Balance Transfer has been successfully registered');
        }



      

    
    </script>
</asp:Content>
