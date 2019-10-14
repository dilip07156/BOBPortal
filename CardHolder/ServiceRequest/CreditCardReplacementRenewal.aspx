<%@ Page Language="C#" Title="Credit Card Replacement-Renewal" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="CreditCardReplacementRenewal.aspx.cs" Inherits="CardHolder.ServiceRequest.CreditCardReplacementRenewal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table.tlbForm { border-collapse: collapse; width: 400px; }
        table.tlbForm td { border: 1px solid black; padding: 5px; }
        table.tlbForm th { border: 1px solid black; padding: 5px; font-weight: bold; text-align: center; background: #EDE8DD; color: #434343; text-align: left; width: 160px; }
        
        #popup_box { display: block; height: auto !important; width: 440px !important; }
        .appFormsubmtpop { width: 440px !important; }
        
             .buttonDisble {
  cursor: default;
  overflow: visible;
  border: none;
  background: url(../images/disble-btn.png) left top repeat-x;
  padding: 3px 5px 3px;
  color: #999;
  font-size: 12px;
  font-weight: bold;
  border: #d1d1d1 solid 1px;
  margin: 0 5px 0 0;
  float: left;
}

    </style>
    <script type="text/javascript">

        $(document).ready(function () {
         //   var isreturn = false;
            $("input[id$='btnSubmit']").click(function () {
                $("span[id$='lblMessage']").html("");
                if ($("select[id$='ddlRequestType']").val() == "0") {
                    $("span[id$='lblErrorRequestType']").html("Please select request type");
                    $("select[id$='ddlRequestType']").focus();
                    return false;
                }
                else {
                    $("span[id$='lblErrorRequestType']").html("");
                }
                if ($("select[id$='ddlReasons']").val() == "0") {
                    $("span[id$='lblErrorReasons']").html("Please select reason");
                    $("select[id$='ddlReasons']").focus();
                    return false;
                }
                else {
                    $("span[id$='lblErrorReasons']").html("");
                }
                if (!$("input[id$='chkAgree']").is(':checked')) {
                    $("span[id$='lblErrorchkAgree']").html("Please select this box to proceed");
                    return false;
                }
                else {
                    $("span[id$='lblErrorchkAgree']").html("");
                }
                var RequestType = $("select[id$='ddlRequestType'] option:selected").text();
                var CardNumber = $("select[id$='ddlcardlist'] option:selected").val();
                var MaskCardNumber = $("select[id$='ddlcardlist'] option:selected").text();
                $("span[id$='lblConfirmCardNumber']").html(MaskCardNumber);
                $("span[id$='lblConfirmCardHolder']").html($("span[id$='lblCardHolder']").html());
                $("span[id$='lblConfirmReason']").html($("select[id$='ddlReasons'] option:selected").text());
                $("span[id$='lblConfirmType']").html($("select[id$='ddlRequestType'] option:selected").text());
                $("input[id$='hideConfirmReason']").val($("select[id$='ddlReasons']").val());
                $("input[id$='hideConfirmRequest']").val($("select[id$='ddlRequestType'] option:selected").val());
                $("span[id$='popupRequestType']").html("Replacement");

                if (RequestType == "Replacement")
                    AjaxCallforReplacement(CardNumber, RequestType);
                else if (RequestType == "Renewal")
                    AjaxCallforRenewal(CardNumber, RequestType);

            });

            function AjaxCallforReplacement(CardNumber, RequestType) {
                var e = false;
                $.ajax({
                    url: "CreditCardReplacementRenewal.aspx/AllowedforReplacement",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        if (data.d != "") {
                            AjaxCall(CardNumber, RequestType, e);
                            if (e = true) {
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
                        }
                        else {
                            alert("Sorry!! This card is not applicable for replacement");
                            return false;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                        return false;
                    }
                });
            }

            function AjaxCallforRenewal(CardNumber, RequestType) {
                var e = false;
                $.ajax({
                    url: "CreditCardReplacementRenewal.aspx/AllowedforRenewal",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        if (data.d != "") {
                            AjaxCall(CardNumber, RequestType, e);
                            if (e = true) {
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
                        }
                        else {
                            alert("Sorry!! This card is not applicable for renewal");
                            return false;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                        return false;
                    }
                });

            }

            function AjaxCall(CardNumber, RequestType, e) {
                $.ajax({
                    url: "CreditCardReplacementRenewal.aspx/ReplaceRenewCharges",
                    data: '{"RequestType":"' + RequestType + '","CardNumber":"' + CardNumber + '"}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        if (data.d != "") {
                            $("span[id$='lblCharge']").html(data.d);
                            $("span[id$='lblMessage']").html("");
                            e = true;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                        return false;
                    }
                });
            }

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
            alert('Your Request for Credit Card Replacement/Renewal has been successfully registered');
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
            <span>Credit Card Replacement/Renewal</span></h3>
    </div>
    <div>
        <label class="commonLabel" style="color: #FE5200">
            Please select Credit Card for which you want to request.
        </label>
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error" />
    </div>
    <div>
        <ul class="addUser">
            <li><span class="left commonLabel">Credit Card Number:</span> <span class="right">
                <asp:DropDownList ID="ddlcardlist" runat="server" CssClass="myselect" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlcardlist_SelectedIndexChanged">
                </asp:DropDownList>
            </span></li>
            <li><span class="left commonLabel">Name of Card-Holder:</span> <span class="right">
                <asp:Label ID="lblCardHolder" runat="server" /></span> </li>
            <li><span class="left commonLabel">Request:<span class="red">*</span></span> <span
                class="right">
                <asp:DropDownList runat="server" ID="ddlRequestType" CssClass="myselect" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged"
                    AutoPostBack="true" CausesValidation="false" ValidationGroup="none" onchange="Page_BlockSubmit = false;">
                </asp:DropDownList>
                <asp:Label runat="server" CssClass="error" ID="lblErrorRequestType"></asp:Label>
            </span></li>
            <li><span class="left commonLabel">Reason:<span class="red">*</span></span> <span
                class="right"></span><span class="right">
                    <asp:DropDownList ID="ddlReasons" runat="server" CssClass="myselect">
                    </asp:DropDownList>
                    <asp:Label runat="server" CssClass="error" ID="lblErrorReasons"></asp:Label>
                </span></li>
            <li><span>
                <asp:CheckBox runat="server" ID="chkAgree" Text="       I here by agree to all terms and conditions." />
                <a target="_blank" href="../terms_conditions.htm#request3">Terms & Conditions</a> </span><span class="red">*</span>
                <asp:Label runat="server" CssClass="error" ID="lblErrorchkAgree"></asp:Label>
                <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" ValidationGroup="CrRepRen"
                    EnableClientScript="true" ClientValidationFunction="CheckBoxRequired_ClientValidate"
                    ErrorMessage="Please select this box to proceed" Display="Dynamic" />
            </li>
            <li><span class="left"></span><span class="right">
                <input id="btnSubmit" type="button" name="btnSubmit" value="Submit" validationgroup="CrRepRen"
                    runat="server" class="button navbluebtm "/>
                <asp:Button runat="server" ID="btnreset" Text="Reset" CssClass="button greybtn" CausesValidation="false"
                    OnClick="btnreset_Click" />
            </span></li>
        </ul>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px">
            <center>
                <h3>
                    <span style="text-align: center">Card Holder Details </span>
                </h3>
                <table cellpadding="5" cellspacing="5" class="tlbForm">
                    <tr>
                        <th>
                            <span class="left commonLabel">Credit Card Number:</span>
                        </th>
                        <td>
                            <span class="right">
                                <asp:Label runat="server" ID="lblConfirmCardNumber" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Name Of Card-Holder:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblConfirmCardHolder" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Reason:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblConfirmReason" />
                            <asp:HiddenField runat="server" ID="hideConfirmReason" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Type:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblConfirmType" />
                            <asp:HiddenField runat="server" ID="hideConfirmRequest" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Charges:</span>
                        </th>
                        <td>
                            Rs.<asp:Label runat="server" ID="lblCharge" />
                        </td>
                    </tr>
                </table>
                <div class="pt10 wd200">
                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button " OnClick="btnSubmitfinal_Click"
                        Text="Submit" />
                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button" />
                </div>
            </center>
        </div>
    </div>
</asp:Content>
