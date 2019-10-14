<%@ Page Language="C#" Title="Statement Delivery Mode" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="StatementRequest.aspx.cs" Inherits="CardHolder.ServiceRequest.StatementRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .appFormsubmtpop { width: 440px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>Statement Delivery Mode</span></h3>
    </div>
    <div class="commonLabel" style="color: #FE5200">
        Please select mode by which you want Statement.
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error" /></div>
    <div>
        <ul class="addUser">
            <li><span class="left commonLabel">Account Number:</span> <span class="right">
                <asp:Label runat="server" ID="lblCreditCardAccount" />
            </span></li>
            <li><span class="left commonLabel">Name of Card-Holder:</span> <span class="right">
                <asp:Label ID="lblCardHolder" runat="server" /></span> </li>
            <li><span class="left commonLabel">Mode:<span class="red">*</span></span> <span class="right">
                <asp:CheckBoxList runat="server" ID="chkMode" RepeatDirection="Horizontal" CssClass="fLeft wd150">
                    <asp:ListItem Value="E" Text=" Email ">
                    </asp:ListItem>
                    <asp:ListItem Value="H" Text=" Hard Copy ">
                    </asp:ListItem>
                </asp:CheckBoxList>
                <div class="statmentchkboxlist">
                    <span id="Errochkmode"></span>
                </div>
            </span></li>
            <li><span class="left"></span><span class="right">
                <input type="button" id="btnconfirm" value="Submit" runat="server" class="button navbluebtm" />
            </span></li>
        </ul>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box" style="width: 440px; height: auto">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px;">
            <center>
                <h3>
                    <span>Are you sure for statment delivery mode??</span>
                </h3>
                <div style="width: 450px; text-align: left">
                    <asp:CheckBox runat="server" ID="chkAgree" Text="       I here by agree to all terms and conditions and laible to pay the charges for the
                    same" />
                    <a href="#">Terms & Conditions*</a>
                </div>
                <div class="pt10 wd126">
                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click"
                        CssClass="button navbluebtm" OnClientClick="return validate();" />
                    <asp:HiddenField runat="server" ID="hideRequestTypeId" Value="6" />
                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button " />
                </div>
            </center>
        </div>
    </div>
    <script type="text/javascript">
        var Isvalid = false;
        function validate() {
            if (!$("input[id$='chkAgree']").is(':checked')) {
                alert("Please go through terms and conditions before agreeing it");
                return false;
            }
            return true;
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
            //alert('Please select atleast one mode for statement')
            Isvalid = false;
            return false;

        }



        $(document).ready(function () {
            $("input[id$='btnconfirm']").click(function () {

                ValidateColorList();
                if (Isvalid == true) {
                    $("span[id$='Errochkmode']").html("");
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
                }
            });


            $('#popupBoxClose').click(function () {
                unloadPopupBox();
            });
            $("input[id$='btnNo']").click(function () {
                unloadPopupBox();
            });
        });
        function unloadPopupBox() {
            $('#overlay').remove();
            $('#popup_box').fadeOut("slow");
        }

        function loadPopupBox() {
            $('#popup_box').fadeIn("slow");
        }

        function Showalert() {
            alert('Your Request for statement has been successfully registered');
        }

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }
    </script>
</asp:Content>
