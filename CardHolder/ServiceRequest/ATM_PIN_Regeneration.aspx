<%@ Page Language="C#" Title="ATM PIN Regeneration" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ATM_PIN_Regeneration.aspx.cs" Inherits="CardHolder.ServiceRequest.ATM_PIN_Regeneration" %>

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

        .button {
        }

        .auto-style2 {
            width: 220px;
        }
    </style>
    <script type="text/javascript">
        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = $("#<%=txtATMPIN.ClientID%>").val()

            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {

                $('#<%=LblError.ClientID%>').html("Only Numbers allowed");
                $('#<%=LblError.ClientID%>').css('display', 'inline-block');
                return false;
            }
            else {
                $('#<%=LblError.ClientID%>').css('display', 'none');
                return true;
            }
        }
        function Validate() {

            if (!$("input[id$='chkAgree']").is(':checked')) {
                alert("Please go through terms and conditions before agreeing it");
                return false;
            }
            return true;
        }




        var ATMOTPTimer;
        function sendOTP() {


            var CardNumber = $("select[id$='ddlcardlist'] option:selected").val();
            var MobileNumber = $('#<%=hideMobileNumber.ClientID%>').val();
            var EmailId = $('#<%=hideEmailId.ClientID%>').val();

            StopFunction();
            AjaxCall(CardNumber, MobileNumber, EmailId);

            return true;
        }



        let timerOn = true;
        function timer(remaining) {

            var m = Math.floor(remaining / 60);
            var s = remaining % 60;

            m = m < 10 ? '0' + m : m;
            s = s < 10 ? '0' + s : s;

            var remainingSec = m + ':' + s;
            document.getElementById("timer").innerHTML = remainingSec;

            remaining -= 1;

            if (remaining >= 0 && timerOn) {
                ATMOTPTimer = setTimeout(function () {
                    timer(remaining);
                }, 1000);
                return;
            }

            if (!timerOn) {
                // Do validate stuff here
                return;
            }

            // Do timeout stuff here
            //alert('Timeout for otp');
            var CardNumber = $("select[id$='ddlcardlist'] option:selected").val();
            ClearHidOTP(CardNumber);

            return true;
        }

        function StopFunction() {
            clearTimeout(ATMOTPTimer);
        }

        function ClearHidOTP(CardNumber) {

            $.ajax({
                url: "ATM_PIN_Regeneration.aspx/ClearHidOTP",
                data: '{"CardNumber":"' + CardNumber + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var strings = data.d.split(",");
                    if (data.d != "") {
                        $('#<%=Label1.ClientID%>').html("Timeout for OTP");
                        $('#<%=divInvalidfeedback.ClientID%>').css('display', 'block');
                        $('#<%=Label1.ClientID%>').css('display', 'block');
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });



        }

        function AjaxCall(CardNumber, MobileNumber, EmailId) {
            $.ajax({
                url: "ATM_PIN_Regeneration.aspx/SendOTP",
                data: '{"CardNumber":"' + CardNumber + '","MobileNumber":"' + MobileNumber + '" ,"EmailId":"' + EmailId + '"}',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {  
                    debugger;
                    var strings = data.d.split(",");
                    if (data.d != "") {
                        if (strings[1] == "") {
                            $('#<%=DivMessage.ClientID%>').css('display', 'block');
                            $('#<%=labelMessage.ClientID%>').html(strings[0]);
                        }

                        $('#<%=hideOTP.ClientID %>').val(strings[0]);
                        $("span[id$='hideResultMobile']").html(strings[1]);
                        $('#<%=divOTP.ClientID%>').css('display', 'block');
                        $('#<%=divIncorrectOTP.ClientID%>').css('display', 'flex');
                        $('#<%=divOTPSent.ClientID%>').css('display', 'block');
                        $('#<%=divremaining.ClientID%>').css('display', 'block');
                        $('#<%=Label1.ClientID%>').css('display', 'none');
                        $('#<%=lblOTPMessage.ClientID%>').css('display', 'none');
                        $('[id$="btnSubmit"]').attr('disabled', false);
                            <%--$("#<%=txtATMPIN.ClientID%>").val("");
                            $("#<%=txtConfirmATMPIN.ClientID%>").val(""); --%>
                        $('#<%=hideOTPTimer.ClientID %>').val(strings[2]);
                        var OTP = $("#<%=txtOTP.ClientID%>").val();
                        if (OTP != '' || OTP != null) {
                            $("#<%=txtOTP.ClientID%>").val('');
                        }
                        $(".custom-control-input").attr("disabled", "disabled");
                        timer(60);


                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        }

        function setValue() {
            $(".custom-control-input").attr("checked", "checked");
            $('[id$="btnSubmit"]').attr('disabled', false);
        }

        function UncheckValue() {

            //$("#acceptTerms").prop("disabled", true);
            $(".custom-control-input").attr("disabled", "disabled");
        }

        function checkValue() {

            $('[id$="btnSubmit"]').attr('disabled', false);
            $(".custom-control-input").attr("checked", false);
        }

        $(document).ready(function () {

            var loading = $(".loading");
            loading.hide();

            $('#<%=btnSubmit.ClientID%>').click(function () {

                var ATMPIN = $("#<%=txtATMPIN.ClientID%>").val(); //get value
                var validOTP = $('#<%=HiddenField1.ClientID %>').val();
                var ConfirmATMPIN = $("#<%=txtConfirmATMPIN.ClientID%>").val();//get value 
                var docHeight;
                var modal;
                if (validOTP != '') {
                    if (ATMPIN != null && ATMPIN != '') {
                        if (ATMPIN != ConfirmATMPIN) {
                            $('#<%=LblConfirmATMPIN.ClientID%>').html("PIN Mismatch");
                            $('#<%=LblATMPINErrorMessage.ClientID%>').css('display', 'none');
                            $('#<%=LblConfirmATMPIN.ClientID%>').css('display', 'block');
                            $(".custom-control-input").attr("disabled", "disabled");
                            return false;
                        }
                        else {
                            $('#<%=LblConfirmATMPIN.ClientID%>').css('display', 'none');
                            $('#<%=LblError.ClientID%>').css('display', 'none');
                            $(".custom-control-input").attr("disabled", "disabled");

                            //docHeight = $(document).height();
                            //$("body").append("<div id='overlay'></div>");
                            //modal = $('<div />');
                            //modal.addClass("modal");
                            //$('body').append(modal);
                            //$("#overlay").height(docHeight).css({
                            //    'opacity': 0.4,
                            //    'position': 'absolute',
                            //    'top': 0,
                            //    'left': 0,
                            //    'background-color': 'black',
                            //    'width': '100%',
                            //    'z-index': 5000
                            //});
                            //ShowProgress();
                            return true;
                        }
                    }
                    else {
                        $('#<%=LblATMPINErrorMessage.ClientID%>').html("Please Enter PIN");
                        $('#<%=LblATMPINErrorMessage.ClientID%>').css('display', 'inline-block');
                        $(".custom-control-input").attr("disabled", "disabled");
                        return false;
                    }
                }
            });


            $("input[id$='ATMPinbtn']").click(function () {
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

            ;


            //Regenerate PIN
            //$('#OTP, #SetNewPin').hide();
            $('#PinSubmitBtn').attr("disabled", true);
            $('#SelectReason').on('change', function () {
                $('#OTP').fadeIn("slow");
                $('#PinSubmitBtn').attr("disabled", false);
                //$('#SetNewPin').fadeOut("fast");
            });
            $('#PinSubmitBtn').on('click', function () {
                $('#OTP').fadeOut("fast");
                //$('#SetNewPin').fadeIn("slow");
            });


            $('#button-re-enter-new-pin').hover(function show() {
                var x = $("#ContentPlaceHolder1_txtConfirmATMPIN");
                x.prop('type', 'text');
                $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-close.svg") %>">');
            },
                function () {
                    var x = $("#ContentPlaceHolder1_txtConfirmATMPIN")
                    //Change the attribute back to password  
                    x.prop('type', 'password');
                    $(this).html('<img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">');
                });
        });


        function Showalert() {
            alert('Your Request for ATM PIN Regeneration has been successfully registered');
        }

    </script>

    <script type="text/javascript">
        /*
    Ref:
    http://www.jqueryscript.net/demo/Buttons-with-Built-in-Loading-Indicators-For-Bootsrap-3-Ladda-Bootstrap/
    */

        //$(window).load(function () {
        $(document).ready(function () {
            var buttons = document.querySelectorAll('.ladda-button');

            Array.prototype.slice.call(buttons).forEach(function (button) {

                var resetTimeout;

                button.addEventListener('click', function () {

                    if (typeof button.getAttribute('data-loading') === 'string') {
                        button.removeAttribute('data-loading');
                    }
                    else {
                        button.setAttribute('data-loading', '');
                    }

                    clearTimeout(resetTimeout);
                }, false);

            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <!--Regenerate PIN-->
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Regenerate PIN</h6>
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
                    <div class="alert alert-danger" id="DivMessage" runat="server" style="display: none">
                        <asp:Label ID="labelMessage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label for="">Credit Card <span class="orange"></span></label>
                        <asp:DropDownList ID="ddlcardlist" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlcardlist_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div id="" class="form-text text-primary">
                            Name on Card: <span class="right">
                                <asp:Label ID="lblCardHolder" runat="server" /></span>
                        </div>
                    </div>
                    <div class="custom-control custom-checkbox form-group">
                        <input type="checkbox" class="custom-control-input" id="acceptTerms" onclick="sendOTP()" name="">
                        <label class="custom-control-label" for="acceptTerms">I accept the <a target="_blank" href="../terms_conditions.htm#request1"><u>Terms and Conditions</u></a></label>

                    </div>




                    <!--OTP-->
                    <div class="form-group" id="divOTP" runat="server" style="display: none">
                        <div class="row">
                            <div class="col-auto mr-auto">
                                <label for="">Enter OTP<span class="orange"></span></label>
                            </div>
                            <div class="col-auto">
                                <a href="#" class="primary-link link" onclick="sendOTP();"><u>Resend</u></a>
                            </div>
                        </div>
                        <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="txtOTP" placeholder="Enter OTP" aria-describedby="button-enter-new-pin" MaxLength="6"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOTP" CssClass="orange" ValidationGroup="LoginFrame" runat="server" Display="Dynamic" ErrorMessage="Please enter OTP"></asp:RequiredFieldValidator>--%>




                        <div class="row" id="divIncorrectOTP" runat="server" style="display: none">
                            <div class="col-auto mr-auto">
                                <div id="divOTPSent" runat="server" style="display: none">
                                    <small id="" class="form-text">OTP has been sent to +91
                                        <asp:Label runat="server" ID="hideResultMobile"></asp:Label></small>

                                </div>
                            </div>
                            <div class="col-auto" id="divremaining" runat="server" style="display: none">
                                <small id="Small1" class="form-text"><span id="timer"></span>remaining</small>
                            </div>

                        </div>
                        <div class="invalid-feedback" style="display: none" id="divInvalidfeedback" runat="server">
                            <asp:Label runat="server" Style="display: none; align-self: center" ID="lblOTPMessage" CssClass="error"></asp:Label>
                            <asp:Label runat="server" Style="display: none; align-self: center" ID="Label1" CssClass="error"></asp:Label>
                        </div>
                    </div>

                    <div id="SetNewPin" runat="server" visible="false">
                        <!--info alert-->
                        <div class="alert alert-primary alert-dismissible fade show  mb-4 mt-3" role="alert">
                            <figure class="icon mr-2 alert-img">
                                <img src="<%= this.Page.GetNewImagePath("Success.svg") %>" alt="info-icon" width="22" />
                            </figure>
                            <span class="alert-content">
                                <asp:Label runat="server" ID="lblmsg"></asp:Label></span>

                            <asp:LinkButton runat="server" ID="btnClose" CssClass="close" OnClick="btnClose_Click" Text="Close"></asp:LinkButton>
                        </div>
                    </div>
                    <div runat="server" cssclass="form-control form-control-lg" visible="false" id="divPIN">
                        <div class="form-group">
                            <label for="">Set New PIN<span class="alert-content orange"></span></label>
                            <%--<input id="" type="password" class="form-control form-control-lg" placeholder="Enter New PIN" aria-label="Enter New PIN" aria-describedby="button-enter-new-pin">--%>
                            <asp:TextBox runat="server" ID="txtATMPIN" CssClass="form-control form-control-lg" placeholder="Enter New PIN" MaxLength="4" TextMode="Password" aria-describedby="button-enter-new-pin" aria-label="Enter New PIN" onkeypress="return validate(event)" />
                            <%--<asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtATMPIN" CssClass="orange" ValidationGroup="LoginFrame" runat="server" Display="Dynamic" ErrorMessage="Please enter PIN"></asp:RequiredFieldValidator>--%>
                            <asp:Label runat="server" Style="display: none;" ID="LblATMPINErrorMessage" CssClass="error"></asp:Label>

                        </div>

                        <div class="form-group">
                            <label for="">Confirm New PIN<span class="orange alert-content"></span></label>
                            <div class="input-group custom-input-group">
                                <%--<input id="confirmNewPin" type="password" class="form-control form-control-lg" placeholder="Re-enter New PIN" aria-label="Enter New PIN" aria-describedby="button-re-enter-new-pin">--%>
                                <asp:TextBox runat="server" CssClass="form-control form-control-lg" ID="txtConfirmATMPIN" MaxLength="4" TextMode="Password" placeholder="Re-enter New PIN" aria-label="Reenter New PIN" aria-describedby="button-re-enter-new-pin" />


                                <div class="input-group-append">
                                    <button class="btn btn-outline-secondary" type="button" id="button-re-enter-new-pin">
                                        <img src="<%= this.Page.GetNewImagePath("eye-open.svg") %>">
                                    </button>
                                </div>

                            </div>
                            <asp:Label runat="server" Style="display: none;" ID="LblError" CssClass="error"></asp:Label>
                            <asp:Label runat="server" Style="display: none;" ID="LblConfirmATMPIN" CssClass="error"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <button type="button" class="btn btn-primary btn-lg text-uppercase ladda-button expand-left" id="btnSubmit" onserverclick="btnSubmit_Click" validationgroup="LoginFrame" runat="server"><span class="label">Submit</span> <span class="spinner"></span></button>
                    </div>
                    <input id="OTPCurrentValue" type="text" value="" style="display: none" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error" />
    </div>
    <div>
        <asp:HiddenField runat="server" ID="hideRequestTypeId" />
        <asp:HiddenField runat="server" ID="hideCreditCardNumber" />
        <asp:HiddenField runat="server" ID="hideCreditAccNumber" />
        <asp:HiddenField runat="server" ID="hideMobileNumber" />
        <asp:HiddenField runat="server" ID="hideEmailId" />
        <asp:HiddenField runat="server" ID="hideATMPin" />
        <asp:HiddenField runat="server" ID="hideCount" />
        <asp:HiddenField runat="server" ID="hideOTP" />
        <asp:HiddenField runat="server" ID="hideOTPTimer" />
        <asp:HiddenField runat="server" ID="HiddenField1" />
    </div>
</asp:Content>
