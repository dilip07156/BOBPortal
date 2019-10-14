<%@ Page Language="C#" Title="De-Registration Credit Card" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DeRegisterCreditCard.aspx.cs" Inherits="CardHolder.ServiceRequest.DeRegisterCreditCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .appFormsubmtpop { width: 450px !important; }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("input[id$='btnContinue']").click(function () {
                if (!$("input[id$='chkAgree']").is(':checked')) {
                    $("span[id$='lblchkAgree']").html("You must select this box to proceed");
                    return false;
                }
                $("span[id$='lblchkAgree']").html("");
                $("span[id$='lblMessage']").html("");
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
            });

            $('#popupBoxClose').click(function () {
                unloadPopupBox();
            });
            $("input[id$='btnNo']").click(function () {
                unloadPopupBox();
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
            alert('Your Request for De-Registeration for Credit card has been successfully registered');
        }

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>
                <asp:Literal ID="ltrFormHeader" runat="server" Text="De-Register Credit Card"></asp:Literal>
            </span>
        </h3>
    </div>
    <div class="commonLabel" style="color: #FE5200">
        Please click on continue button if you want to De-Register credit card
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error"></asp:Label>
    </div>
    <div>
        <ul class="addUser">
            <li><span class="left commonLabel">Account Number:</span><span class="right"><asp:Label
                runat="server" ID="lblCardAccNumber"></asp:Label></span></li>
            <li><span class="left commonLabel">Name of Card-Holder:</span><span class="right"><asp:Label
                runat="server" ID="lblCardHolder"></asp:Label></span></li>
            <li><span>
                <asp:CheckBox runat="server" ID="chkAgree" Text="       I here by agree to all terms and conditions." />
                <a target="_blank" href="../terms_conditions.htm#request6">Terms & Conditions</a><span class="red">*</span> </span>
                <asp:Label runat="server" CssClass="error" ID="lblchkAgree"></asp:Label>
                <%--<asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" EnableClientScript="true"
                    ClientValidationFunction="CheckBoxRequired_ClientValidate" ValidationGroup="DeregisterCard"
                    ErrorMessage="You must select this box to proceed" Display="Dynamic" />--%>
            </li>
            <li><span class="left"></span><span class="right">
                <input type="button" id="btnContinue" value="Continue" validationgroup="DeregisterCard"
                    runat="server" class="button navbluebtm" />
                <asp:HiddenField runat="server" ID="hideRequestTypeId" />
            </span></li>
        </ul>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box" style="width: 460px; height: auto">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px;">
            <center>
                <h3>
                    Are you sure you want to do De-Register Credit Card.?
                </h3>
                <div class="pt10 wd126">
                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button" OnClick="btnSubmitfinal_Click"
                        Text="Submit" />
                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button" />
                </div>
            </center>
        </div>
    </div>
</asp:Content>
