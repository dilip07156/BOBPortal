<%@ Page Title="Loan Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LoanRequest.aspx.cs" Inherits="CardHolder.ServiceRequest.LoanRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #divLoan ul.addUser {
            border: 1px solid #d1d1d1;
            padding: 10px;
        }

        ul.addUser li span.left {
            width: 160px !important;
        }

        .appFormsubmtpop {
            width: 440px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%">
        <asp:HiddenField ID="hideRequestTypeId" runat="server" />
        <div class="commontitlediv">
            <h3>
                <span>
                    <asp:Literal ID="ltrFormHeader" runat="server" Text="Loan Request"></asp:Literal>
                </span>
            </h3>
        </div>
        <div class="addcontactdiv">
            <span class="contractno">
                <asp:Label runat="server" ID="lblSuccessMsg" CssClass="msgsuccess" />
                <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
            </span>
        </div>
    </div>
    <div class="contactinformation noPadding">
        <h4 id="gridheader" runat="server">Details
        </h4>
        <asp:GridView ID="gvLoantxn" runat="server" Width="100%" OnRowDataBound="gvLoantxn_RowDataBound"
            AutoGenerateColumns="false" CssClass="gridCssLoan">
            <AlternatingRowStyle CssClass="secondrow" />
            <Columns>
                <asp:TemplateField HeaderStyle-Width="110" HeaderText="Select Transaction">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkTransactions" class="rowselect" runat="server" />
                        <asp:HiddenField ID="hdnLoanOracleId" Value='<%#Eval("MICROFILM_REF_NUMBER")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CR_ACCOUNT_NBR" HeaderText="Account Number" />
                <asp:TemplateField HeaderText="Credit Limit (INR)">
                    <ItemTemplate>
                        <asp:Label ID="lblCreditLimit" runat="server" Text='<%#Eval("CREDIT_LIMIT","{0:f}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Available Credit Limit (INR)">
                    <ItemTemplate>
                        <asp:Label ID="lblAVAILABLECREDITLIMIT" runat="server" Text='<%#Eval("AVAILABLE_CREDIT_LIMIT","{0:f}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pre-Approved Limit (INR)">
                    <ItemTemplate>
                        <asp:Label ID="lblPREAPPROVEDLIMIT" runat="server" Text='<%#Eval("PREAPPROVED_LIMIT","{0:f}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="firstrow" />
        </asp:GridView>
    </div>
    <div class="addcontactdiv">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="200" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="200" PageSize="200" />
    </div>
    <div id="divLoan" class="divLoanClass" style="width: 100%; float: left">
        <ul class="addUser">
            <li><span class="left commonLabel">
                <asp:Literal ID="litAmount" runat="server" Text="Amount"></asp:Literal>
                : </span><span class="right">
                    <asp:Label ID="lblamount" Text="0.00" runat="server"></asp:Label>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litterms" runat="server" Text="Terms(With Interest)"></asp:Literal>
                : </span><span class="right">
                    <asp:DropDownList runat="server" Width="100px" ID="ddlterms"
                        CssClass="myselect" CausesValidation="false">
                    </asp:DropDownList>
                </span></li>

            <li><span class="left commonLabel">
                <asp:Literal ID="littermsMonth" runat="server" Text="Terms (In Month)"></asp:Literal>
                : </span><span class="right">
                    <asp:TextBox ID="txttermInMonth" runat="server" Text="3" ReadOnly="true"></asp:TextBox>

                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litInterst" runat="server" Text="Annual Interest Rate (%)"></asp:Literal>
                : </span><span class="right">
                    <asp:TextBox ID="txtInterest" runat="server" Text="10.25" ReadOnly="true"></asp:TextBox>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litEMI" runat="server" Text="EMI (in INR)"></asp:Literal>
                : </span><span class="right">
                    <asp:TextBox ID="txtEMI" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:Label runat="server" ID="lblErrorEMI" CssClass="error" />
                </span></li>

            <li><span class="left"></span><span class="right">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit"
                    CssClass="button  navbluebtm" />
                <input id="btndisabled" name="btndisabled" type="button" runat="server" value="Submit" class="buttonDisble navbluebtm"
                    title="Disabled!! As no transactions found" />
                <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" CausesValidation="false" Text="Reset" CssClass="button greybtn" />
                <asp:HiddenField ID="hdnIntTot" runat="server" />
                <asp:HiddenField ID="hdnEMI" runat="server" />
            </span></li>
        </ul>
    </div>
    <!-- POPUP content are here -->
    <div id="popup_box" style="width: 440px; height: auto">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px">
            <center>
                <h3>You shall be charged by Rs.<asp:Label ID="lblCharge" runat="server" Text="XXX" />
                </h3>
                <div style="width: 405px; text-align: left">
                    <asp:CheckBox runat="server" ID="chkAgree" Text="        I here by agree to all terms and conditions and laible to pay the charges for the same" />
                    <a target="_blank" href="../terms_conditions.htm#request12Loan">Terms & Conditions*</a>
                    <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" EnableClientScript="true"
                        ClientValidationFunction="CheckBoxRequired_ClientValidate" ErrorMessage="You must select this box to proceed"
                        Display="Dynamic" />
                </div>
                <div class="pt10 wd126">
                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="button navbluebtm" OnClick="btnSubmit_Click"
                        Text="Submit" />
                    <input id="btnNo" type="button" name="btnNo" value="Cancel" runat="server" class="button greybtn" />
                </div>
            </center>
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {
            $("input[id$='btnSubmit']").click(function () {
                $("span[id$='lblErrorEMI']").html("");
                if ($("input[id$='txtEMI']").val() == "") {
                    $("span[id$='lblErrorEMI']").html("Select Transaction(s) for EMI.");
                    return false;
                }
                else if ($("input[id$='txtEMI']").val() == "0") {
                    $("span[id$='lblErrorEMI']").html("EMI should be greater than 0.");
                    return false;
                }

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

            $('#popupBoxClose').click(function () {
                unloadPopupBox();
            });
            $("input[id$='btnNo']").click(function () {
                unloadPopupBox();
            });

        });

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }


        function Showalert() {
            alert('Your Loan Request has been successfully registered');
        }


        $(function () {
            var calculate = $('.gridCssLoan input:checkbox').click(function (e) {
                var termval = $("#ContentPlaceHolder1_ddlterms option:selected").val();
                GetInterestRate(termval);
                //// Getamount();
            });
        });

        $(function () {
            $(".divLoanClass select").change(function () {
                GetInterestRate($(this).val());
                //// Getamount();
            });
        });

        function Getamount() {
            var total = 0;
            var netTotal = 0;
            var TotalChecked = 0;

            $('tr:has(:checkbox:checked) td:nth-child(5)').each(function () {
                total += parseFloat($(this).text());
                TotalChecked = TotalChecked + 1;
            });

            netTotal = total.toFixed(2);
            document.getElementById('<%=hdnIntTot.ClientID %>').value = total.toFixed(2);

            if (TotalChecked > 0) {
                AjaxCallToGetCharges(TotalChecked);
            }

            $('#ContentPlaceHolder1_lblamount').text(netTotal);
            EMICalculcation(netTotal);
        }

        function AjaxCallToGetCharges(TotalChecked) {
            $.ajax({
                url: "LoanRequest.aspx/GetLoanCharges",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d != "") {
                        var FinalVal = parseFloat(data.d) * TotalChecked;
                        $("span[id$='lblCharge']").html(FinalVal);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }

        function GetInterestRate(termValue) {
            $.ajax({
                url: "LoanRequest.aspx/GetLoanInterestRate",
                data: '{"termValue":"' + termValue + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d != "") {
                        var strings = data.d.split(",");
                        var FinalTerm = parseFloat(strings[0]);
                        var FinalRate = parseFloat(strings[1]);
                        $("input[id$='txttermInMonth']").val(FinalTerm);
                        $("input[id$='txtInterest']").val(FinalRate);
                    }
                },
                complete: function (data) {
                    Getamount();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }

        var maincontent = "ContentPlaceHolder1_";
        var gridmaincontent = "ContentPlaceHolder1_gvLoantxn_";
        var TotalAmount = document.getElementById(maincontent + "lblamount");
        var rate = document.getElementById(maincontent + "txtInterest");
        var ddlterms = document.getElementById(maincontent + "ddlterms");

        var CalculatedEMI = document.getElementById(maincontent + "txtEMI");
        var floatRate;
        var floatTerms;
        var floatEMI;

        function EMICalculcation(netTotal) {
            floatRate = rate.value;
            floatTerms = $("input[id$='txttermInMonth']").val(); //// ddlterms.options[ddlterms.selectedIndex].text;

            floatRate = floatRate / (12 * 100);
            var plusfloatrate = 1 + floatRate;

            if (netTotal == 0 || floatRate == 0 || floatTerms == 0) {
                CalculatedEMI.value = 0;
            }
            else {
                floatEMI = netTotal * floatRate * Math.pow(plusfloatrate, floatTerms) / (Math.pow(plusfloatrate, floatTerms) - 1);

                CalculatedEMI.value = Math.floor(parseFloat(floatEMI));

                document.getElementById('<%=hdnEMI.ClientID %>').value = CalculatedEMI.value;
            }
        }

        function FormattedAmount(amount) {
            var i = parseFloat(amount);
            if (isNaN(i)) { i = 0.00; }
            var minus = '';
            if (i < 0) { minus = '-'; }
            i = Math.abs(i);
            i = parseInt((i + .005) * 100);
            i = i / 100;
            var s;
            s = new String(i);
            if (s.indexOf('.') < 0) { s += '.00'; }
            if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
            s = minus + s;
            return s;
        }

        function unloadPopupBox() {
            $('#overlay1').remove();
            $('#popup_box').fadeOut("slow");
        }

        function loadPopupBox() {
            $('#popup_box').fadeIn("slow");

        }
    </script>
</asp:Content>
