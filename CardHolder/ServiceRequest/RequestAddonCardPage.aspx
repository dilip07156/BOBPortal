<%@ Page Language="C#" Title="Addon Card Request" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="RequestAddonCardPage.aspx.cs" Inherits="CardHolder.ServiceRequest.RequestAddonCardPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        ///---
        /// Gender Validation
        ///---
        function GenderClientValidate(sender, e) {
            if ($("input[id$='rbMale']").is(':checked') == false && $("input[id$='rbFeMale']").is(':checked') == false) {
                e.IsValid = false;
            } else {
                e.IsValid = true;
            }
        }

        ///---
        /// File Upload Type
        ///---
        function FileUploadType(sender, e) {
            var fupData = jQuery("input[id$='photoUpload']");
            var FileUploadPath = fupData.val();
            if (FileUploadPath == '') {
                //alert("Please select file to upload");
            } else {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "jpeg" || Extension == "jpg" || Extension == "png") {
                    e.IsValid = true;
                } else {
                    e.IsValid = false;
                }
            }
        }

        ///---
        /// Image File Size Validation
        ///---
        var size = 0;
        function FileUploadSize(sender, e) {
            if (size > 20000) {
                e.IsValid = false;
            } else {
                e.IsValid = true;
            }
        }

        ///---
        /// Event Handler Settings
        ///---
        $(document).ready(function () {
            ///---
            /// File Upload Change
            ///---
            $("input[id$='photoUpload']").change(function () {
                var f = this.files[0];
                size = f.size;
            })
        });

        function Showalert() {
            alert('Your Request for Add-On card has been successfully registered');
        }

    </script>
    <style type="text/css">
        ul.addUser li { width: 650px !important; }
        
        ul.addUser li span.left { width: 225px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="commontitlediv">
        <h3>
            <span>Request for Add-on Card</span></h3>
    </div>
    <div class="commonLabel" style="color: #FE5200">
        Number of addon cards with this account number is :
        <asp:Label runat="server" Text="0" ID="lblcountaddons"></asp:Label>
    </div>
    <div>
        <asp:Label runat="server" ID="lblMessage" CssClass="error" /></div>
    <div>
        <ul class="addUser">
            <li><span class="left commonLabel">Name of Primary Card-Holder : </span><span class="right">
                <asp:Label runat="server" ID="lblCardHolder" />
            </span></li>
            <li><span class="left commonLabel">Account Number: </span><span class="right">
                <asp:Label runat="server" ID="lblAccountNumber" />
            </span></li>
            <li><span class="left commonLabel">Primary Credit Card Number: </span><span class="right">
                <asp:Label runat="server" ID="lblCardNumber" />
            </span></li>
            <li><span class="left commonLabel">Full Name of Add-On Card Applicant:<span class="red">*</span></span>
                <span class="right">
                    <asp:TextBox ID="txtApplicantName" MaxLength="100" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" EnableClientScript="true"
                        ForeColor="red" ControlToValidate="txtApplicantName" ErrorMessage="Please enter full Name of Add-On Card Applicant"
                        CssClass="error" Display="Dynamic" />
                </span></li>
            <li><span class="left commonLabel">Date of Birth:<span class="red">*</span> </span><span
                class="right">
                <asp:TextBox ID="txtDOB" runat="server" class="inputText wd146 Newdp" />
                <asp:RequiredFieldValidator runat="server" ID="reqOBIRTH_DATE" EnableClientScript="true"
                    CssClass="error" ForeColor="red" ControlToValidate="txtDOB" ErrorMessage="Please select birth date"
                    Display="Dynamic" />
            </span></li>
            <li><span class="left commonLabel">Relationship with primary card holder:<span class="red">*</span>
            </span><span class="right">
                <asp:DropDownList ID="ddlRelation" runat="server" CssClass="myselect">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="reqRelation" EnableClientScript="true"
                    ForeColor="red" ControlToValidate="ddlRelation" InitialValue="0" ErrorMessage="Please select relation"
                    CssClass="error" Display="Dynamic" />
            </span></li>
            <li><span class="left commonLabel">Gender: <span class="red">*</span> </span><span
                class="right">
                <asp:RadioButton runat="server" ID="rbMale" GroupName="gender" Text="Male" />
                <asp:RadioButton runat="server" ID="rbFeMale" GroupName="gender" Text="Female" />
                <asp:CustomValidator ForeColor="Red" runat="server" ID="cvGender" EnableClientScript="true"
                    CssClass="error" ClientValidationFunction="GenderClientValidate" ErrorMessage="Please select gender"
                    Display="Dynamic" />
            </span></li>
            <li><span class="left commonLabel">Upload Addon Photo: </span><span class="right">
                <asp:FileUpload ID="photoUpload" runat="server" /><span class="hints">Allowed files .jpeg, jpg or .png </span>
                <asp:CustomValidator ForeColor="Red" EnableClientScript="true" ID="cvUploadType"
                    CssClass="error" runat="server" ErrorMessage="Please select valid image file"
                    ClientValidationFunction="FileUploadType" ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
                <asp:CustomValidator ForeColor="Red" EnableClientScript="true" ID="cvUploadSize"
                    CssClass="error" runat="server" ErrorMessage="Please select file, size has less than 20K"
                    ClientValidationFunction="FileUploadSize" ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
            </span></li>
            <li><span>
                <asp:CheckBox runat="server" ID="chkAgree" Text="       I here by agree to all terms and conditions." />
                <a target="_blank" href="../terms_conditions.htm#request5">Terms & Conditions</a> <span class="red">*</span> </span>
                <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" EnableClientScript="true"
                    ClientValidationFunction="CheckBoxRequired_ClientValidate" ErrorMessage="Please check the terms and conditions checkbox to proceed further"
                    Display="Dynamic" />
            </li>
            <li><span class="left"></span><span class="right">
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                   class="button navbluebtm" />
                <asp:Button runat="server" ID="btnReset" Text="Reset" CausesValidation="false" OnClick="btnReset_Click"
                    CssClass="button greybtn" />
                <input type="hidden" runat="server" id="hideRequestTypeId" value="5" />
            </span></li>
        </ul>
    </div>
    <script type="text/javascript">
        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked');
        }

      
    </script>
</asp:Content>
