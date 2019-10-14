<%@ Page Title="Auto Debit De-Registration" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AutoDebitDe_Registration.aspx.cs" Inherits="CardHolder.ServiceRequest.AutoDebitDe_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        ul.addUser li span.left { width: 160px !important; }
        table.tlbForm { border-collapse: collapse; width: 400px; }
        table.tlbForm td { border: 1px solid black; padding: 5px; }
        table.tlbForm th { border: 1px solid black; padding: 5px; font-weight: bold; text-align: center; background: #EDE8DD; color: #434343; text-align: left; width: 160px; }
        .appFormsubmtpop { width: 440px !important; }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $("input[id$='btnproceed']").click(function () {

                if ($("select[id$='ddlReasons']").val() == "0") {
                    $("span[id$='lblErrorReasons']").html("Please select reason");
                    $("select[id$='ddlReasons']").focus();
                    return false;
                }
                $("span[id$='lblErrorReasons']").html("");

                if ($("input[id$='chkAgree']").is(':checked') == false) {
                    $("span[id$='cvAgree']").html("Please go through Terms and Conditions before agreeing to it");
                    return false;
                }
                else if($("input[id$='chkAgree']").is(':checked') == true) {
                    $("span[id$='cvAgree']").html("");
                    return false;
                }
                $("span[id$='cvAgree']").html("");
                var AccountNumber = $("span[id$='lblAccountNumber']").text();
                AjaxCall(AccountNumber);
            });

            function AjaxCall(AccountNumber) {
                $.ajax({
                    url: "AutoDebitPaymentType.aspx/GetLastAutoDebitDetails",
                    data: '{"AccountNumber":"' + AccountNumber + '"}',
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        var strings = data.d.split(",");
                        var docHeight;
                        if (data.d != "") {
                            $("span[id$='lblAutoName']").html(strings[0]);
                            $("span[id$='lblAutoType']").html(strings[1]);
                            $("span[id$='lblAutoPercentage']").html(strings[2]);
                            $("span[id$='lblAutoBranch']").html(strings[3]);
                            docHeight = $(document).height();
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
                            loadATMPopupBox();
                        }
                        else {
                            docHeight = $(document).height();
                            $("body").append("<div id='overlay1'></div>");
                            $("#overlay1").height(docHeight).css({
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
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                        return false;
                    }
                });
            }

            $('#AtmpPin_popupBoxClose').click(function () {
                unloadATMPopupBox();
            });
            $("input[id$='btnclose']").click(function () {
                unloadATMPopupBox();
            });

            $("input[id$='AutoDebitbtn']").click(function () {
                unloadATMPopupBox();
                $("span[id$='lblMessage']").html("");
                var docHeight = $(document).height();
                $("body").append("<div id='overlay1'></div>");
                $("#overlay1").height(docHeight).css({
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

            $("a[id$='popupBoxClose']").click(function () {
                unloadPopupBox();
            });
            $("input[id$='btnNo']").click(function () {
                unloadPopupBox();
            });
        });

        function unloadATMPopupBox() {
            $('#overlay').remove();
            $('#AtmPin_popup_box').fadeOut("slow");
        }

        function loadATMPopupBox() {
            //$('#overlay').add();
            $('#AtmPin_popup_box').fadeIn("slow");

        }


        function unloadPopupBox() {
            $('#overlay1').remove();
            $('#popup_box').fadeOut("slow");
        }

        function loadPopupBox() {
            $('#popup_box').fadeIn("slow");

        }


        function Showalert() {
            alert('Your Request for De-Registration of auto debit payment type has been successfully registered');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>Auto Debit Payment Type De-Registration</span></h3>
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error" />
    </div>
    <div>
        <ul class="addUser">
            <li><span class="left commonLabel">Credit Card Account Number: </span><span class="right">
                <asp:Label runat="server" ID="lblAccountNumber" />
            </span></li>
            <li><span class="left commonLabel">Credit Card Number: </span><span class="right">
                <asp:Label runat="server" ID="lblCardNumber" />
            </span></li>
            <li><span class="left commonLabel">Name of Card-Holder: </span><span class="right">
                <asp:Label runat="server" ID="lblCardHolder" />
            </span></li>
            <li><span class="left commonLabel">Bank Account Number:</span>
                <span class="right">
                    <asp:Label ID="lblbnkAccnum" runat="server"></asp:Label><span class="hints"> (From which
                        we are debiting)</span></span></li>
            <li><span class="left commonLabel">Branch Name: </span><span class="right">
                <asp:Label runat="server" ID="lblBranchName" />
            </span></li>
            <li><span class="left commonLabel">Reason for De-registration:<span class="red">*</span></span><span
                class="right">
                <asp:DropDownList runat="server" ID="ddlReasons" CssClass="myselect">
                </asp:DropDownList>
                <asp:Label runat="server" ID="lblErrorReasons" CssClass="error" />
            </span></li>
            <li><span class="left"></span><span class="right">
                <asp:CheckBox runat="server" ID="chkAgree" Text="        I hereby agree to all terms & conditions for De-Registeration of Auto Debit payment type" />
                <a target="_blank" href="../terms_conditions.htm#request8">Terms & Conditions</a><span class="red">*</span>
                <asp:Label ID="cvAgree" runat="server" CssClass="error"></asp:Label>
            </span></li>
            <li><span class="left"></span><span class="right">
                <input id="btnproceed" type="button" name="btnproceed" value="Proceed" runat="server"
                    class="button navbluebtm" />
                <input type="button" id="btndisable" value="Submit" runat="server" title="Disabled!! As your card is inactive"
                    disabled="True" class="buttonDisble" />
                <asp:HiddenField runat="server" ID="hideRequestTypeId" Value="" />
            </span></li>
        </ul>
    </div>
    <!--Auto Debit Payment Detail POPUP content are here -->
    <div id="AtmPin_popup_box">
        <a id="AtmpPin_popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px">
            <center>
                <h3>
                    <span style="text-align: center">Last Auto Debit Payment Detail </span>
                </h3>
                <table cellpadding="5" cellspacing="5" class="tlbForm">
                    <tr>
                        <th>
                            <span class="left commonLabel">Name Of Card-Holder:</span>
                        </th>
                        <td>
                            <span class="right">
                                <asp:Label runat="server" ID="lblAutoName" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Auto Debit Type:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblAutoType" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Debit Percentage:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblAutoPercentage" />%
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Auto Debit Branch:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblAutoBranch" />
                        </td>
                    </tr>
                </table>
                <div class="pt10 wd200">
                    <input type="button" id="AutoDebitbtn" value="Confirm to Proceed" name="AutoDebitbtn"
                        runat="server" class="button" />
                    <input id="btnclose" type="button" name="btnclose" value="Cancel" runat="server"
                        class="button" />
                </div>
            </center>
        </div>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box" style="width: 460px; height: auto">
        <a id="popupBoxClose" clientidmode="Static" runat="server" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px;">
            <center>
                <h3>
                    Are you sure you want to do De-Register auto debit payment type for your card.?
                </h3>
                <div class="pt10 wd126">
                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button" OnClick="btnSubmit_Click"
                        Text="Submit" />
                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button" />
                </div>
            </center>
        </div>
    </div>
</asp:Content>
