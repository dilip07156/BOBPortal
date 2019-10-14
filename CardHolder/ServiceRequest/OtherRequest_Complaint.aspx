<%@ Page Title="Other Request_Complaint" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="OtherRequest_Complaint.aspx.cs" Inherits="CardHolder.ServiceRequest.OtherRequest_Complaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="mb-0">Other Request/Complaint</h6>
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
                                
                                <div class="alert alert-danger" id ="DivMessage" runat="server" c style="display:none">
                           <asp:Label ID="lblMessage"  runat="server" Text=""></asp:Label>
                    </div> 
                                <div class="mb-4">
                                     <div class="d-label mb-2">Credit Card</div>
                                    <div class="alert alert-secondary mb-1">
                                        <div class="d-value"><asp:Label runat="server" ID="lblCreditCardNumber" /></div>
                                    </div>
                                    <div class="text-primary">Name on Card: <asp:Label ID="lblCardHolder" runat="server" /></div>
                                </div>                                
                                <div class="form-group col">                                     
                                    <asp:RadioButtonList CssClass="radio-contorl-table custom-radio inline-control" runat="server" OnSelectedIndexChanged = "OnRadio_Changed" AutoPostBack = "true" ID="RadioRequestComplaint" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="true">Request</asp:ListItem>
                                    <asp:ListItem Value="1">Complaint</asp:ListItem>
                                    </asp:RadioButtonList>                                   
                                    <asp:RequiredFieldValidator ID="RVa1" CssClass="error" runat="server" ControlToValidate="RadioRequestComplaint"
                                        Display="Dynamic" ErrorMessage="Please select Request/Complaint"></asp:RequiredFieldValidator>
                                </div>                      
                                <div class="form-group">
                                    <label for="">Appropriate Request<span class="orange"></span></label>
                                    <asp:DropDownList runat="server" ID="ddlAppropRequestComplaint"
                                        CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                                        CausesValidation="false"
                                        OnSelectedIndexChanged="ddlAppropRequestComplaint_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="error" ID="rfvddlReqComplaint" runat="server" ControlToValidate="ddlAppropRequestComplaint"
                                        Display="Dynamic" ErrorMessage="Please select Request" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">                                        
                                    <label for="">Description<span class="orange"></span></label>
                                    <asp:TextBox ID="txtremarks" runat="server" onkeyDown="checkTextAreaMaxLength(this,event,'500');"
                                        TextMode="MultiLine" MaxLength="500" CssClass="form-control form-control-lg "></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" CssClass="error" ID="RegularExpressionValidator1"
                                        runat="server" ControlToValidate="txtremarks" ErrorMessage="Text limit is 500 characters or less."
                                        ValidationExpression="^[\s\S]{0,500}$" />
                                    <asp:RequiredFieldValidator CssClass="error" ID="rfvRemarks" runat="server" ControlToValidate="txtremarks"
                                        Display="Dynamic" ErrorMessage="Please write some description"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <div class="custom-control custom-checkbox termsConditions">
                                        <asp:CheckBox runat="server" ID="chkAgree" Text="I accept the" />
                                        <a target="_blank" href="../terms_conditions.htm#request14">Terms & Conditions</a><span class="red"></span>
                                    </div>
                                    <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" EnableClientScript="true"
                                        ClientValidationFunction="CheckBoxRequired_ClientValidate" ErrorMessage="Please check the terms and conditions checkbox to proceed further."
                                        Display="Dynamic" />
                                </div>                                
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                     CssClass="btn btn-primary btn-lg text-uppercase" />
                <asp:HiddenField runat="server" ID="hideRequestTypeId" />
                            </div>
                        </div>
                    </div>
                </div>

    
    
    <script type="text/javascript">
        function Showalert() {
            alert('Your Request/Complaint has been successfully registered');
        }

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }

          $(document).ready(function () {
            //custom checkbox design
            $('.termsConditions input, .radio-contorl-table input').addClass('custom-control-input');
            $('.termsConditions label, .radio-contorl-table label').addClass('custom-control-label');
        });

    </script>
</asp:Content>
