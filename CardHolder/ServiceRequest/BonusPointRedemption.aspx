<%@ Page Title="Bonus Point Redemption" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="BonusPointRedemption.aspx.cs" Inherits="CardHolder.ServiceRequest.BonusPointRedemption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Reward Point Redemption</h6>
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
                    <div class="alert alert-danger" id ="DivMessage" runat="server" style="display:none">
                           <asp:Label ID="lblMessage"  runat="server" Text=""></asp:Label>
                    </div> 
                  <div class="mb-4">
                    <div class="d-label mb-2">Credit Card</div>
                        <div class="alert alert-secondary mb-1">
                            <div class="d-value">
                                <asp:Label runat="server" ID="lblCreditCardNumber" /></div>
                        </div>
                         <div class="text-primary">Name on Card: <asp:Label ID="lblCardHolder" runat="server" /></div>
                </div>

                     <div class="mb-4">
                        <div class="d-label mb-2">Total Bonus Points Earned</div>
                        <div class="alert alert-secondary">
                            <div class="d-value">
                                <asp:Label ID="lblEarnedPoints" runat="server" /></div>
                        </div>
                    </div>


                    <div class="form-group">
                        <label for="">Bonus points you would like to redeem</label>
                        <asp:TextBox ID="txtpointsReddeem" MaxLength="6" onkeypress="return numbersonly(this, event)"
                            runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="error" ID="rfvddlReason" runat="server" ControlToValidate="txtpointsReddeem"
                            Display="Dynamic" ErrorMessage="Please enter points to redeem"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cmpmin" CssClass="error" runat="server" ControlToValidate="txtpointsReddeem"
                            Operator="GreaterThanEqual" Display="Dynamic" Type="Integer" SetFocusOnError="true"
                            ValueToCompare="500" ErrorMessage="Points should be greater than 500">
                        </asp:CompareValidator>
                    </div>

                    <div class="form-group">
                    <div class="custom-control custom-checkbox termsConditions">
                        <asp:CheckBox runat="server" ID="chkAgree" Text="I accept the" />
                        <a target="_blank" href="../terms_conditions.htm#request9">Terms & Conditions</a><span class="orange"></span>
                        </div>
                        <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" EnableClientScript="true"
                            ClientValidationFunction="CheckBoxRequired_ClientValidate" ErrorMessage="Please check the terms and conditions checkbox to proceed further."
                            Display="Dynamic" />
                    
                        </div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Redeem" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-lg text-uppercase" />
                    <asp:HiddenField runat="server" ID="hideRequestTypeId" />
                  
              </div>
                </div>
         </div>
   
   
        
          
         </div>
    <script type="text/javascript">
         $(document).ready(function () {
            //custom checkbox design classess
            $('.termsConditions input').addClass('custom-control-input');
            $('.termsConditions label').addClass('custom-control-label');
        });
        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }
    </script>
</asp:Content>
