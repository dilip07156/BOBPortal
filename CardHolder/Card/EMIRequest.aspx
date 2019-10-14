<%@ Page Title="EMI Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EMIRequest.aspx.cs" Inherits="CardHolder.ServiceRequest.EMIRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #divEMI ul.addUser {
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

    <asp:HiddenField ID="hideRequestTypeId" runat="server" />
    <div class="card-header mb-4">
        <h6>EMI Request </h6>
        <small>Convert your Card to outstanding EMI</small>

    </div>
    <%--    <div class="mb-4" id="DivERROR" runat="server" style="display: none">
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="orange"></asp:Label>
    </div>--%>
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
    <h6 id="gridheader" runat="server" class="mb-3">List of Unbilled Transactions</h6>

    <div class="mb-4" id="divEMIRequest">
        <div class="contactinformation noPadding">
        </div>

        <%--<asp:GridView ID="gvEMItxn" runat="server" Width="100%" 
            AutoGenerateColumns="false" CssClass="table-card-row" OnRowDataBound="gvEMItxn_RowDataBound"> 
            <AlternatingRowStyle CssClass="secondrow" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>   
                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAll(this);"/> 
                        </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkTransactions" class="rowselect" runat="server"/>
                            <asp:HiddenField ID="hdnAmount" runat="server" Value='<%#Eval("Amount","{0:f}")%>' />
                        <asp:HiddenField ID="hdnEMIOracleId" Value='<%#Eval("MICROFILM_REF_NUMBER")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>                        
                        <%# (Convert.ToDateTime(Eval("Transaction_date"))).ToString("dd MMMM yy")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Card_Number" HeaderText="Card Number" />
                <asp:TemplateField HeaderText="Merchant Name">
                    <ItemTemplate>
                        <div>
                        <asp:Label ID="lblmerchantNm" runat="server" Text='<%#Eval("merchant_name")%>' />
                            </div>
                        <div>
                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>' />
                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount (INR)">                   
                    <ItemTemplate>                       
                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount","{0:f}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="firstrow" />
        </asp:GridView>--%>
        <div class="divtable accordion-xs">
            <asp:ListView runat="server" ID="lstViewCardStatement" OnItemDataBound="lstViewCardStatement_ItemDataBound">
                <LayoutTemplate>
                    <!--header-->
                    <div class="tr headings emi-heading">
                        <%-- <div class="col-1">
                                <asp:CheckBox ID="chkAllSelect" runat="server" />
                            </div--%>
                        <div class="th emi-date">
                            <div class="custom-control custom-checkbox inline-control">
                                <asp:CheckBox ID="chkAllSelect" runat="server" Text=" " />
                                <span class="d-lg-none d-inline-block select-all">Select All</span>
                                <%--<input type="checkbox" class="custom-control-input" id="checkAll" >--%>
                                <%--<label class="custom-control-label" for="checkAll"><span class="d-lg-none d-inline-block">Select All</span></label>--%>
                            </div>
                            <span class="d-none d-lg-inline-block date-text">Date</span>
                        </div>

                        <div class="th emi-card-number">
                            Card Number
                        </div>
                        <div class="th emi-merchant-name">
                            Merchant Name
                        </div>
                        <div class="th emi-amount  text-right">
                            Amount In (INR)
                        </div>
                    </div>
                    <div id="itemPlaceholder" runat="server"></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="tr">
                        <div class="td emi-date accordion-xs-toggle">
                            <div class="custom-control custom-checkbox inline-control">
                                <asp:CheckBox ID="chkTransactions" runat="server" onclick="Getamount()" Text=" " />
                                <asp:HiddenField ID="hdnAmount" runat="server" Value='<%#Eval("Amount","{0:f}")%>' />
                                <asp:HiddenField ID="hdnEMIOracleId" Value='<%#Eval("MICROFILM_REF_NUMBER")%>' runat="server" />
                                <asp:Label ID="lblEMIRequested" runat="server" Visible="false"></asp:Label>
                            </div>
                            <span class="date-text">
                                <%# (Convert.ToDateTime(Eval("Transaction_date"))).ToString("dd MMMM yy")%>
                            </span>

                        </div>


                        <div class="accordion-xs-collapse">
                            <div class="inner">
                                <div class="td emi-card-number">
                                    <asp:Label ID="lblCardNumber" runat="server" Text='<%#Eval("Card_Number")%>' />
                                </div>
                                <%--<i class="fas fa-rupee-sign small-icon"></i><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>--%>


                                <div class="td emi-merchant-name">
                                    <asp:Label ID="lblmerchantNm" runat="server" Text='<%#Eval("merchant_name")%>' />

                                    <div class="small">
                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>' class="small" />
                                    </div>
                                </div>
                                <%--<i class="fas fa-rupee-sign small-icon"></i><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>--%>

                                <div class="td emi-amount text-left text-lg-right">
                                    <span class="medium-icon">₹  </span>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount","{0:f}")%>' />
                                    <%--<i class="fas fa-rupee-sign small-icon"></i><%= string.Format("{0:0.00}", Convert.ToDouble(item.Amount)) %>--%>
                                </div>
                            </div>
                        </div>



                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="addcontactdiv">
        <csc:Pager ID="Pager1" runat="server" EnableViewState="false" OnCommand="pager_Command"
            CompactModePageCount="200" GenerateFirstLastSection="True" GenerateGoToSection="True"
            GeneratePagerInfoSection="true" NormalModePageCount="200" PageSize="200" />
    </div>
    <div class="card mb-4">

        <div class="card-body" id="divEMIWithDesign" runat="server">
            <div class="row" id="divEMI">
                <div class="col-lg-6">
                    <div class="mb-3">
                        <div class="d-label mb-2">
                            <asp:Literal ID="litAmount" runat="server" Text="Amount"></asp:Literal>
                        </div>
                        <div class="alert alert-secondary">
                            <div class="d-value">
                                <asp:Label ID="lblamount" Text="0.00" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="ddlterms">
                            <asp:Literal ID="litterms" runat="server" Text="Terms(In months)"></asp:Literal></label>
                        <asp:DropDownList runat="server" ID="ddlterms" CssClass="form-control form-control-lg custom-select wide">
                            <%--CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ddlterms_SelectedIndexChanged"--%>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Literal ID="litInterst" runat="server" Text="Annual Interest Rate (%)"></asp:Literal>
                        <asp:TextBox ID="txtInterest" runat="server" Text="18" CssClass="form-control form-control-lg"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Literal ID="litEMI" runat="server" Text="EMI (In INR)"></asp:Literal>
                        <asp:TextBox ID="txtEMI" runat="server" ReadOnly="true" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:Label runat="server" ID="lblErrorEMI" CssClass="error" />
                    </div>
                    <!--new reset button-->
                    <%--<button class="btn btn-outline-primary btn-lg text-uppercase mr-3">Reset</button>--%>
                    <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" CausesValidation="false" Text="Reset" CssClass="btn btn-outline-primary btn-lg text-uppercase mr-3" />

                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit"
                        CssClass="btn btn-lg btn-primary text-uppercase" />
                    <input id="btndisabled" name="btndisabled" type="button" runat="server" value="Submit" class="btn btn-lg btn-primary text-uppercase"
                        title="Disabled!! As no transactions found" />



                    <asp:HiddenField ID="hdnIntTot" runat="server" />
                    <asp:HiddenField ID="hdnEMI" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <%--<div id="divEMI" style="width: 100%; float: left">
        <ul class="addUser">
            <li><span class="left commonLabel">
                <asp:Literal ID="litAmount" runat="server" Text="Amount"></asp:Literal>
                : </span><span class="right">
                    <asp:Label ID="lblamount" Text="0.00" runat="server"></asp:Label>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litterms" runat="server" Text="Terms(In months)"></asp:Literal>
                : </span><span class="right">
                    <asp:DropDownList runat="server" Width="50px" ID="ddlterms" CssClass="myselect" CausesValidation="false">
                    </asp:DropDownList>
                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litInterst" runat="server" Text="Annual Interest Rate (%)"></asp:Literal>
                : </span><span class="right">
                    <asp:TextBox ID="txtInterest" runat="server" Text="18"></asp:TextBox>

                </span></li>
            <li><span class="left commonLabel">
                <asp:Literal ID="litEMI" runat="server" Text="EMI (in INR)"></asp:Literal>
                : </span><span class="right">
                    <asp:TextBox ID="txtEMI" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:Label runat="server" ID="lblErrorEMI" CssClass="error" />
                </span></li>         
            <li><span class="left"></span><span class="right">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit"
                    CssClass="button navbluebtm" />
                <input id="btndisabled" name="btndisabled" type="button" runat="server" value="Submit" class="buttonDisble navbluebtm"
                    title="Disabled!! As no transactions found" />
                <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" CausesValidation="false" Text="Reset" CssClass="button greybtn" />
                <asp:HiddenField ID="hdnIntTot" runat="server" />
                <asp:HiddenField ID="hdnEMI" runat="server" />
            </span></li>
        </ul>
    </div>--%>
    <!-- POPUP content are here -->
    <%--<div id="popup_box" style="width: 440px; height: auto">
        <a id="popupBoxClose" class="popClosebtn"></a>
        <div class="appFormsubmtpop" style="padding: 0px">
            <center>
                <h3>You shall be charged by Rs.<asp:Label ID="lblCharge" runat="server" Text="XXX" />
                </h3>
                <div style="width: 405px; text-align: left">
                    <asp:CheckBox runat="server" ID="chkAgree" Text="        I here by agree to all terms and conditions and laible to pay the charges for the same" />
                    <a target="_blank" href="../terms_conditions.htm#request12">Terms & Conditions*</a>
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
    </div>--%>

    <script type="text/javascript">

        $(document).ready(function () {
            $('.custom-control  > input, .custom-control > span > input').addClass('custom-control-input');
            $('.custom-control  > label, .custom-control > span > label').addClass('custom-control-label');

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
            alert('Your EMI Request for Unbilled transactions has been successfully registered');
        }


        $(function () {
            var calculate = $('.table-card-row input:checkbox').click(function (e) {
                Getamount();
            });
        });


        $(function () {
            $("select").change(function () {
                Getamount();
            });
        });

        function Getamount() {
            var total = 0;
            var netTotal = 0;
            var TotalChecked = 0;

            var favorite = [];
            //$.each($("input[name='ctl00$ContentPlaceHolder1$lstViewCardStatement$ctrl0$chkTransactions']:checked"), function(){            
            //    favorite.push($("input[id$='lblAmount']").val());
            //});
            //alert("My favourite sports are: " + favorite.join(", "));
            $('#divEMIRequest input[type="checkbox"]').each(function () {
                if ($(this).prop('checked') == true) {
                    //var hidden = $(this).closest('div').find(':hidden').val();                     
                    var hidden = $(this).closest('div').find('input:hidden:first').attr('value');
                    if (hidden == undefined) {
                        hidden = 0;
                    }
                    total += parseFloat(hidden);
                    TotalChecked = TotalChecked + 1;
                }
                else if ($(this).prop('checked') == false) {
                    $("input[id$='chkAllSelect']").prop("checked", false);
                }
            });

            //$('tr:has(:checkbox:checked) td:nth-child(5)').each(function () {
            //    total += parseFloat($(this).text());
            //    TotalChecked = TotalChecked + 1;
            //});

            netTotal = total.toFixed(2);
            document.getElementById('<%=hdnIntTot.ClientID %>').value = total.toFixed(2);

            if (TotalChecked > 0) {
                AjaxCallToGetCharges(TotalChecked);
            }

            $('#ContentPlaceHolder1_lblamount').text(netTotal);
            EMICalculcation(netTotal);
            //            var NetAmountTotal = 0.0;
            //            $('.rowselect').each(function () {
            //                
            //                var str = $(this).context.children[0].id.replace("chkTransactions", "lblAmount");
            //                if ($(this).context.children[0].checked == true) {
            //                    // document.getElementById(str).disabled = false;
            //                    NetAmountTotal += parseFloat(document.getElementById(str).value);
            //                }
            //                // else
            //                //   document.getElementById(str).disabled = true;
            //            });
            //            $("#ContentPlaceHolder1_lblamount").text(NetAmountTotal.toFixed(2));

        }

        function AjaxCallToGetCharges(TotalChecked) {
            $.ajax({
                url: "EMIRequest.aspx/GetEMICharges",
                //// data: '{"CardNumber":"' + CardNumber + '"}',
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

        var maincontent = "ContentPlaceHolder1_";
        var gridmaincontent = "ContentPlaceHolder1_gvEMItxn_";
        var TotalAmount = document.getElementById(maincontent + "lblamount");
        var rate = document.getElementById(maincontent + "txtInterest");
        var ddlterms = document.getElementById(maincontent + "ddlterms");

        var CalculatedEMI = document.getElementById(maincontent + "txtEMI");
        var floatRate;
        var floatTerms;
        var floatEMI;

        function EMICalculcation(netTotal) {
            floatRate = rate.value;
            floatTerms = ddlterms.options[ddlterms.selectedIndex].text;

            floatRate = floatRate / (12 * 100);
            var plusfloatrate = 1 + floatRate;

            if (netTotal == 0 || floatRate == 0 || floatTerms == 0) {
                CalculatedEMI.value = 0;
            }
            else
            // var floatEMI = (floatTotalAmount * floatRate) * ((1 + floatRate) ^ floatTerms) / (((1 + floatRate) ^ floatTerms) - 1);
            {
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

        $(document).ready(function () {

            $("input[id$='chkAllSelect']").click(function () {
                $('input:checkbox').not(this).prop('checked', this.checked);

                Getamount();
                //var total = 0;
                //$("input[id$='lblAmount']").each(function () {
                //    if ($(this).val() != '') {
                //        total += parseInt($(this).val());
                //    }
                //});
                // var sum = 0;
                //$('[name^=hdnAmountValue]').each(function() {
                //    sum += parseFloat($(this).val());
                //                });
                //                alert(sum);
            });
        });

        //new table design
        $(function () {
            debugger;
            var isXS = false,
                $accordionXSCollapse = $('.accordion-xs-collapse');

            // Window resize event (debounced)
            var timer;
            $(window).resize(function () {
                if (timer) { clearTimeout(timer); }
                timer = setTimeout(function () {
                    isXS = Modernizr.mq('only screen and (max-width: 992px)');

                    // Add/remove collapse class as needed
                    if (isXS) {
                        $accordionXSCollapse.addClass('collapse');
                    } else {
                        $accordionXSCollapse.removeClass('collapse');
                    }
                }, 100);
            }).trigger('resize'); //trigger window resize on pageload

            // Initialise the Bootstrap Collapse
            $accordionXSCollapse.each(function () {
                $(this).collapse({ toggle: false });
            });


            // <a href="https://www.jqueryscript.net/accordion/">Accordion</a> toggle click event (live)
            $(document).on('click', '.accordion-xs-toggle', function (e) {
                //e.preventDefault();
                debugger;
                var $thisToggle = $(this),
                    $targetRow = $thisToggle.parent('.tr'),
                    $targetCollapse = $targetRow.find('.accordion-xs-collapse');

                if (isXS && $targetCollapse.length) {
                    var $siblingRow = $targetRow.siblings('.tr'),
                        $siblingToggle = $siblingRow.find('.accordion-xs-toggle'),
                        $siblingCollapse = $siblingRow.find('.accordion-xs-collapse');

                    $targetCollapse.collapse('toggle'); //toggle this collapse
                    $siblingCollapse.collapse('hide'); //close siblings

                    $thisToggle.toggleClass('collapsed'); //class used for icon marker
                    $siblingToggle.removeClass('collapsed'); //remove sibling marker class
                }
            });
        });

        <%--function CheckAll(Checkbox) {
            debugger;
            var GridVwHeaderCheckbox = document.getElementById("<%=lstViewCardStatement.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }--%>

    //    $('#chkTransactions').on('change', function () {
    //        alert('hi');

    //$('#show').html(this.checked ? '12' : '');
//});

        //                        $(function () {
        //                            
        //                            $("input[type=checkbox]").click(function () {
        //                                var totalPrice = 0, ctlPrice;

        //                                $('#ContentPlaceHolder1_gvEMItxn tr').each(function () {

        //                                    if ($(this).find('input:checkbox').attr("checked")) {
        //                                        ctlPrice = $(this).find('[id$= lblAmount]');

        //                                        totalPrice += parseFloat(ctlPrice.text());
        //                                    }
        //                                });

        //                                $('#ContentPlaceHolder1_lblamount').text(totalPrice.toFixed(2));
        //                            });
        //                        });

        //        function UpdateAmount() {
        //            var NetAmountTotal = 0.0;
        //            $('.rowselect').each(function () {
        //                var str = $(this).context.children[0].id.replace("chkTransactions", "lblAmount");
        //                if ($(this).context.children[0].checked == true) {
        //                    // document.getElementById(str).disabled = false;
        //                    NetAmountTotal += parseFloat(document.getElementById(str).value);
        //                }
        //                // else
        //                //   document.getElementById(str).disabled = true;
        //            });
        //            if ($(".NetTotalAmount").length != undefined) {
        //                $(".NetTotalAmount").text(NetAmountTotal.toFixed(2));
        //            }
        //        }

        //        function GridHeaderRowSelect() {
        //            $('.rowselect').click(function () {
        //                UpdateAmount();

        //                if ($(this).children().attr("checked") == false) {
        //                    $('.selectall').children().attr("checked", false);
        //                }
        //                else {
        //                    var totalcount = 0;
        //                    var checkcount = 0;
        //                    $('.rowselect').each(function () {
        //                        if ($(this).children().attr("checked") == true) {
        //                            totalcount++; checkcount++;
        //                        }
        //                        else { totalcount++; }
        //                        if (totalcount == checkcount) {
        //                            $('.selectall').children().attr("checked", true);
        //                        }
        //                        else {
        //                            $('.selectall').children().attr("checked", false);
        //                        }
        //                    });
        //                }


        //                //$(this).children().attr("checked", false);
        //            });

        //            $('.selectall').click(function () {

        //                if ($(this).children().attr("checked") == true) {
        //                    $(".rowselect").each(function () {
        //                        $(this).children().attr("checked", true);
        //                    });
        //                }
        //                else {
        //                    $(".rowselect").each(function () {
        //                        $(this).children().attr("checked", false);
        //                    });
        //                }
        //                UpdateAmount();
        //            });
        //        }

        //        $(document).ready(function () {
        //            UpdateAmount();
        //        });


    </script>
</asp:Content>
