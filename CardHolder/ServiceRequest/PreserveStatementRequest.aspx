<%@ Page Language="C#" Title="Preserve Statement Request" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="PreserveStatementRequest.aspx.cs" Inherits="CardHolder.ServiceRequest.PreserveStatementRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .myselect { width: 80px !important; }
        table.tlbForm { border-collapse: collapse; width: 400px; }
        table.tlbForm td { border: 1px solid black; padding: 5px; }
        table.tlbForm th { border: 1px solid black; padding: 5px; font-weight: bold; text-align: center; background: #EDE8DD; color: #434343; text-align: left; width: 160px; }
        
        .appFormsubmtpop { width: 440px !important; }
    </style>
    <script type="text/javascript">

        function Reset() {
            $("span[id$='ErroDateRange']").html("");
            $("span[id$='ErroStatment']").html("");
        }
        function Validate() {
            Reset();
            var valid = true;
            if ($("select[id$='month']").val() == "0") {
                $("span[id$='ErroDateRange']").html("Please select month in a Date Range");
                $("select[id$='month']").focus();
                valid = false;
            }
            if ($("select[id$='year']").val() == "0") {
                $("span[id$='ErroDateRange']").html("Please select year in a Date Range");
                $("select[id$='year']").focus();
                valid = false;
            }
            if (!$("input[id$='chkEStatement']").is(':checked') && !$("input[id$='chkHardCopy']").is(':checked')) {
                $("span[id$='ErroStatment']").html("Please select statement type");
                valid = false;
            }
            if (valid == false) {
                return false;
            }
            if (!$("input[id$='chkAgree']").is(':checked')) {
                alert("Please go through terms and conditions before agreeing it");
                return false;
            }
            return true;
        }
        var estatement;
        var hardcopy;
        $(document).ready(function () {
            //$("[id*=btnSubmit]").live("click", function () {
            $("input[id$='btnSubmit']").click(function () {
                Reset();
                var valid = true;
                var AccountNumber = $("span[id$='lblCreditCardAccNumber']").text();
                //                var estatement;
                //                var hardcopy;
                if ($("select[id$='month']").val() == "0") {
                    $("span[id$='ErroDateRange']").html("Please select month in a Date Range");
                    $("select[id$='month']").focus();
                    valid = false;
                }
                if ($("select[id$='year']").val() == "0") {
                    $("span[id$='ErroDateRange']").html("Please select year in a Date Range");
                    $("select[id$='year']").focus();
                    valid = false;
                }
                if (!$("input[id$='chkEStatement']").is(':checked') && !$("input[id$='chkHardCopy']").is(':checked')) {
                    $("span[id$='ErroStatment']").html("Please select statement type");
                    valid = false;
                }
                if (valid == false) {
                    return false;
                }

                if ($("input[id$='chkEStatement']").is(':checked')) {
                    estatement = 1;
                }
                else {
                    estatement = 0;
                }
                if ($("input[id$='chkHardCopy']").is(':checked')) {
                    hardcopy = 1;
                }
                else {
                    hardcopy = 0;
                }

                AjaxCallForDetails(estatement, hardcopy, AccountNumber);
            });

            $("input[id$='ATMPinbtn']").click(function () {
                $('#overlay').remove();
                $('#AtmPin_popup_box').fadeOut("slow");
                AjaxCall(estatement, hardcopy);
            });

            $("a[id$='AtmpPin_popupBoxClose']").click(function () {
                unloadATMPopupBox();
            });
            $("input[id$='btnclose']").click(function () {
                unloadATMPopupBox();
            });
            $("a[id$='popupBoxClose']").click(function () {
                unloadPopupBox();
            });
            $("input[id$='btnNo']").click(function () {
                unloadPopupBox();
            });
        });

        //To get Preserve Stmnt Details
        function AjaxCallForDetails(estatement, hardcopy, AccountNumber) {
            var CardHolderName = $("span[id$='lblCardHolder']").text();
            $.ajax({
                url: "PreserveStatementRequest.aspx/GetLastStmntDetails",
                data: '{"AccountNumber":"' + AccountNumber + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var strings = data.d.split(",");
                    if (data.d != "") {
                        $("span[id$='lblStmntAccNumber']").html(AccountNumber);
                        $("span[id$='lblStmntCardHolder']").html(CardHolderName);
                        $("span[id$='lblForMonth']").html(strings[0]);
                        $("span[id$='lblStmntDate']").html(strings[1]);
                        $("span[id$='lblStmntReqNum']").html(strings[2]);
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
                        loadATMPopupBox();
                    }
                    else {
                        AjaxCall(estatement, hardcopy);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }

        //        function IsAccountEligible(estatement, hardcopy) {
        //            var e = false;
        //            $.ajax({
        //                url: "PreserveStatementRequest.aspx/IsAccountEligible",
        //                dataType: "json",
        //                type: "POST",
        //                contentType: "application/json; charset=utf-8",
        //                dataFilter: function (data) { return data; },
        //                success: function (data) {
        //                    if (data.d != "") {
        //                        AjaxCall(estatement, hardcopy, e)
        //                        if (e = true) {
        //                            
        //                        }
        //                    }
        //                    else {
        //                        alert("Sorry!! Your account is not eligible for preserve statement request");
        //                        return false;
        //                    }
        //                },
        //                error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                    alert(textStatus);
        //                    return false;
        //                }
        //            });
        //        }


        function AjaxCall(estatement, hardcopy) {
            $.ajax({
                url: "PreserveStatementRequest.aspx/StatementCharges",
                data: '{"estatement":"' + estatement + '","hardcopy":"' + hardcopy + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d != "") {
                        $("span[id$='lblCharge']").html(data.d);
                        $("#hdncharge").val(data.d);
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
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }



        function loadATMPopupBox() {
            $('#AtmPin_popup_box').fadeIn("slow");

        }

        function unloadATMPopupBox() {
            $('#overlay').remove();
            $('#AtmPin_popup_box').fadeOut("slow");
        }

        function unloadPopupBox() {
            $('#overlay1').remove();
            $('#popup_box').fadeOut("slow");
        }

        function loadPopupBox() {
            $('#popup_box').fadeIn("slow");
        }



        function Showalert() {
            alert('Your Request for Preserve statement has been successfully registered')
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>Preserved Statement Request</span></h3>
    </div>
    <div class="commonLabel" style="color: #FE5200">
        Please select statement type by which you want to do request.
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error" /></div>
    <div>
        <ul class="addUser" style="width: 100%">
            <li><span class="left commonLabel">
                <asp:Label ID="lblCCN" runat="server" Text="Account Number:"></asp:Label></span>
                <span class="right" style="width: 200px">
                    <asp:Label ID="lblCreditCardAccNumber" runat="server"></asp:Label>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Label ID="lblNOC" runat="server" Text="Name Of Card-Holder:"></asp:Label></span>
                <span class="right" style="width: 200px">
                    <asp:Label ID="lblCardHolder" runat="server"></asp:Label>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Label ID="lblDateRange" Text="Date Range:" runat="server"></asp:Label><span
                    class="red">*</span> </span><span class="right">
                        <asp:DropDownList runat="server" ID="month" CssClass="myselect">
                            <asp:ListItem Text="Month" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Jan" Value="01"></asp:ListItem>
                            <asp:ListItem Text="Feb" Value="02"></asp:ListItem>
                            <asp:ListItem Text="Mar" Value="03"></asp:ListItem>
                            <asp:ListItem Text="Apr" Value="04"></asp:ListItem>
                            <asp:ListItem Text="May" Value="05"></asp:ListItem>
                            <asp:ListItem Text="Jun" Value="06"></asp:ListItem>
                            <asp:ListItem Text="Jul" Value="07"></asp:ListItem>
                            <asp:ListItem Text="Aug" Value="08"></asp:ListItem>
                            <asp:ListItem Text="Sep" Value="09"></asp:ListItem>
                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlyear" CssClass="myselect">
                        </asp:DropDownList>
                        <span id="ErroDateRange" class="error"></span></span></li>
            <li><span class="left commonLabel">
                <asp:Label ID="lblStatementType" Text="Statement Type:" runat="server"></asp:Label><span
                    class="red">*</span> </span><span class="right">
                        <asp:CheckBox runat="server" ID="chkEStatement" Text=" E-Statement" 
                     />
                        <asp:CheckBox runat="server" ID="chkHardCopy" Text=" Hard Copy" />
                        <span id="ErroStatment" class="error"></span></span></li>
            <li><span class="left"></span><span class="right">
                <input type="button" id="btnSubmit" value="Submit" runat="server" class="button navbluebtm" />
                <input type="button" id="btndisable" value="Submit" runat="server" title="Disabled!! As your card is inactive"
                    disabled="True" class="buttonDisble  greybtn" />
                <asp:HiddenField runat="server" ID="hideRequestTypeId" />
            </span></li>
        </ul>
    </div>
    <!--ATMPIN Reg Detail POPUP content are here -->
    <div id="AtmPin_popup_box">
        <a id="AtmpPin_popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px">
            <center>
                <h3>
                    <span style="text-align: center">Last Statement Detail </span>
                </h3>
                <table cellpadding="5" cellspacing="5" class="tlbForm">
                    <tr>
                        <th>
                            <span class="left commonLabel">Account Number:</span>
                        </th>
                        <td>
                            <span class="right">
                                <asp:Label runat="server" ID="lblStmntAccNumber" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Name Of Card-Holder:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblStmntCardHolder" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Statement Regeneration Date:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblStmntDate" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="left commonLabel">Request Number:</span>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="lblStmntReqNum" />
                        </td>
                    </tr>
                </table>
                <div class="pt10 wd200">
                    <input type="button" id="ATMPinbtn" value="Confirm to Proceed" name="ATMPinbtn" runat="server"
                        class="button" />
                    <input id="btnclose" type="button" name="btnclose" value="Cancel" runat="server"
                        class="button" />
                </div>
            </center>
        </div>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box" style="width: 440px; height: auto">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px;">
            <center>
                <h3>
                    <span>You shall be charged by Rs.<asp:Label ID="lblCharge" runat="server" Text="XXX" /></span>
                    <asp:HiddenField runat="server" ID="hdncharge" ClientIDMode="Static"  />
                </h3>
                <div style="width: 450px; text-align: left">
                    <asp:CheckBox runat="server" ID="chkAgree" Text="       I here by agree to all terms and conditions and laible to pay the charges for the
                    same" />
                    <a target="_blank" href="../terms_conditions.htm#request2">Terms & Conditions*</a>
                </div>
                <div class="pt10 wd126">
                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button" OnClick="btnSubmitfinal_Click"
                        Text="Submit" OnClientClick="return Validate();" />
                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button" />
                </div>
            </center>
        </div>
    </div>
</asp:Content>
