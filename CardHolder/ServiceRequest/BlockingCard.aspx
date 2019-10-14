<%@ Page Title="Request for Blocking Card" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="BlockingCard.aspx.cs" Inherits="CardHolder.ServiceRequest.BlockingCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            var loading = $(".loading");
                loading.hide();
            //$('#termsConditions  input').addClass('custom-control-input');
            //$('#termsConditions  label').addClass('custom-control-label');

            $('.termsConditions input, .radio-contorl-table input').addClass('custom-control-input');
            $('.termsConditions label, .radio-contorl-table label').addClass('custom-control-label');

            //$('.termsConditions > input, .custom-control > span > input').addClass('custom-control-input');
            //$('.termsConditions > label, .custom-control > span > label').addClass('custom-control-label');
        });
       function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                $('#loaderId').css('display', 'inline-block');
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function Showalert() {
            alert('Blocking Request data has been saved successfully');
        }

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
            // if (e.IsValid && $("select[id$='ddlReasons']").val() != "-1") {
            //    ShowProgress();
            //}
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
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="m-0">Block Card</h6>
        </div>
         <div class="card-body">

             <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-primary" role="alert" id ="DivSuccess" runat="server" style="display:none">
                       <figure class="icon mr-2"><img src="<%= this.Page.GetNewImagePath("success.svg") %>" alt="info-icon" width="22"></figure><asp:Label ID="LblSuccessMessage"  runat="server" Text=""></asp:Label>
                    </div>
                    <div class="alert alert-danger" role="alert" id ="DivERROR" runat="server" style="display:none">
                       <figure class="icon mr-2"><img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22"></figure><asp:Label ID="LblErrorMessage"  runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                 <div class="col-lg-6">
                    
             <div class="form-group">
                 <label for="">Credit Card:<span class="orange"></span></label>
                    <asp:DropDownList ID="ddlcardlist" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlcardlist_SelectedIndexChanged">
                    </asp:DropDownList>
               <div id="Small1" class="form-text text-primary">Name on Card:
                            <asp:Label ID="lblCardHolder" runat="server" /></div>
            </div>
               <div class="form-group">
                   <label for="">Select Reason<span class="orange"></span></label>
                   <asp:DropDownList runat="server" ID="ddlReasons" CssClass="form-control form-control-lg custom-select wide" CausesValidation="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="error" ID="rfvddlReason" runat="server" ControlToValidate="ddlReasons"
                            Display="Dynamic" ErrorMessage="Please select Reason" InitialValue="-1"></asp:RequiredFieldValidator>
                   <asp:Label runat="server" ID="lblErrorReasons"  CssClass="error" />
                   </div>
              <div class="mb-4">
                   <div class="custom-control custom-checkbox termsConditions">
                      <asp:CheckBox runat="server" ID="chkAgree" Text="I accept the" />
                       <a target="_blank" href="../terms_conditions.htm#request10">Terms & Conditions</a><span class="orange"></span>
                      </div>
                  <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" EnableClientScript="true"
                        ClientValidationFunction="CheckBoxRequired_ClientValidate" ErrorMessage="Please check the terms and conditions checkbox to proceed further."
                        Display="Dynamic" />
                  </div>
                
                   <%-- <asp:Button ID="btnSubmit" runat="server" Text="Block" OnClick="btnSubmit_Click"
                        CssClass="btn btn-primary btn-lg" />--%>
                     <%--<button type="button" class="btn btn-primary btn-lg ladda-button" id="btnSubmit" onserverclick="btnSubmit_Click" runat="server" data-style="expand-left"><span class="ladda-label">Block</span><div class="ladda-progress" style="width: 0px;"></div></button>--%>
                         <button type="button" class="btn btn-primary btn-lg text-uppercase ladda-button expand-left" id="btnSubmit" onserverclick="btnSubmit_Click" runat="server"><span class="label">Submit</span> <span class="spinner"></span></button>
                    <asp:HiddenField runat="server" ID="hideRequestTypeId" />
                        <asp:HiddenField runat="server" ID="hideCreditCardNumber"  />
                    <asp:HiddenField runat="server" ID="hideReason"  />
               
            </div>
         </div>
        </div>
 </div>
    <div class="loading" id="loaderId"  style="display: none" >                                 
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-body text-center">
                                                    <h6 class="text-uppercase">Please Wait</h6>
                                                    <p class="m-0">Your request is being processed</p>
                                                    <figure  class="m-0">
                                                        <img src="../images/index.ajax-spinner-preloader.gif" alt="" />
                                                    </figure>
                                                </div>
                                            </div>
                                        </div>
                          </div>
</asp:Content>
