<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CardRequest.aspx.cs" Inherits="CardHolder.ServiceRequest.CardRequest" %>

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

        #popup_box {
            display: block;
            height: auto !important;
            width: 440px !important;
        }

        .appFormsubmtpop {
            width: 440px !important;
        }

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

        $(document).ready(function () {

            //custom checkbox and radio design
            $('.custom-checkbox  input, .custom-radio > input').addClass('custom-control-input');
            $('.custom-checkbox  label, .custom-radio > label').addClass('custom-control-label');

            $('.termsConditions input').addClass('custom-control-input');
            $('.termsConditions label').addClass('custom-control-label');

            $('.customFile').on('change', function () {
                //get the file name
                var fileName = $(this).val();
                //replace the "Choose a file" label
                $(this).next('.custom-file-label').html(fileName);
            })
            //   var isreturn = false;

            $('#<%=btnSubmitfinal.ClientID%>').click(function () {

                $("span[id$='lblMessage']").html("");

                $("input[id$='hideConfirmRequest']").val($("select[id$='ddlRequestType'] option:selected").val());
            });

            ///---
            /// File Upload Change
            ///---
            $("input[id$='photoUpload']").change(function () {
                var f = this.files[0];
                size = f.size;
            })

        });

        function Showalert() {
            alert('Your Request for Credit Card Replacement/Renewal has been successfully registered');
        }

        ///---
        /// Agree Checkbox
        ///---
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery("input[id$='chkAgree']").is(':checked') && $("select[id$='ddlReasons']").val() != "0";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card mb-4">
        <div class="card-header">
            <h6 class="mb-0">Card Requests</h6>
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
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="alert alert-danger" id="DivMessage" runat="server" style="display: none">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="mb-4" id="Divaddoncards" runat="server" style="display: none">
                        <div class="d-label mb-2">Number of addon cards with this account number is</div>
                        <div class="alert alert-secondary">
                            <div class="d-value">
                                <asp:Label runat="server" Text="0" ID="lblcountaddons"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="">Credit Card:<span class="orange"></span></label>
                        <asp:DropDownList ID="ddlcardlist" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlcardlist_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div id="Small1" class="form-text text-primary">
                            Name on Card:
                                                <asp:Label ID="lblCardHolder" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="">Request Service<span class="orange"></span></label>
                        <asp:DropDownList runat="server" ID="ddlRequestService"
                            CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                            CausesValidation="false" ValidationGroup="none" OnSelectedIndexChanged="ddlRequestService_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label runat="server" CssClass="error" ID="lblErrorServiceRequest"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <asp:MultiView runat="server" ID="mvFrgtUname">
                        <asp:View ID="replacementRenewalView" runat="server">
                            <div class="form-group">
                                <label for="">Request Type<span class="orange"></span></label>
                                <asp:DropDownList runat="server" ID="ddlRequestType" CssClass="form-control form-control-lg custom-select wide" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged"
                                    AutoPostBack="true" CausesValidation="false" ValidationGroup="CrRepRen" onchange="Page_BlockSubmit = false;">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" EnableClientScript="true"
                                    ControlToValidate="ddlRequestType" InitialValue="0" ErrorMessage="Please select request type"
                                    CssClass="error" Display="Dynamic" ValidationGroup="CrRepRen" />
                                <asp:Label runat="server" CssClass="error" ID="lblErrorRequestType"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="">Reason<span class="orange"></span></label>
                                <asp:DropDownList ID="ddlReasons" runat="server" CssClass="form-control form-control-lg custom-select wide">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" EnableClientScript="true"
                                    ControlToValidate="ddlReasons" InitialValue="0" ErrorMessage="Please select reason"
                                    CssClass="error" Display="Dynamic" ValidationGroup="CrRepRen" />
                                <asp:Label runat="server" CssClass="error" ID="lblErrorReasons"></asp:Label>
                            </div>
                        </asp:View>
                        <asp:View ID="deregisterView" runat="server">
                            <div id="DeRegister" class="request-service">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="alert alert-primary mb-4">
                                            De-registration of the card is only from the portal, so here we should display a message that the de-registration is only from the portal and to avail the service again they have to register on the portal
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="addoncardView" runat="server">
                            <div id="AddOn" class="request-service">

                                <div class="mb-4">
                                    <div class="d-label mb-2">Primary Credit Card</div>
                                    <div class="alert alert-secondary">
                                        <div class="d-value">
                                            <asp:Label runat="server" ID="lblCardNumber" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtApplicantName">Full Name of Add-on Card Applicant<span class="orange"></span></label>
                                    <asp:TextBox ID="txtApplicantName" MaxLength="50" runat="server" CssClass="form-control form-control-lg" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" EnableClientScript="true"
                                        ControlToValidate="txtApplicantName" ErrorMessage="Please enter full Name of Add-On Card Applicant"
                                        CssClass="error" Display="Dynamic" ValidationGroup="CrRepRen" />
                                </div>
                                <div class="form-group">
                                    <label for="">Date of Birth</label>
                                    <div class="input-group  input-group-lg date-input-group">
                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text" id="">
                                                <img src="<%= this.Page.GetNewImagePath("Calendar.svg") %>">
                                            </span>
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqOBIRTH_DATE" EnableClientScript="true"
                                        CssClass="error" ControlToValidate="txtDOB" ErrorMessage="Please select birth date"
                                        Display="Dynamic" ValidationGroup="CrRepRen" />
                                </div>
                                <div class="form-group">
                                    <label for="">Relationship with Primary Card Holder<span class="orange"></span></label>
                                    <asp:DropDownList ID="ddlRelation" runat="server" CssClass="form-control form-control-lg custom-select wide">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="reqRelation" EnableClientScript="true"
                                        ControlToValidate="ddlRelation" InitialValue="0" ErrorMessage="Please select relation"
                                        CssClass="error" Display="Dynamic" ValidationGroup="CrRepRen" />
                                </div>
                                <div class="mb-4">
                                    <div>
                                        <label>Gender<span class="orange"></span></label>
                                    </div>
                                    <div class="custom-control custom-radio inline-control">
                                        <asp:RadioButton runat="server" ID="rbMale" GroupName="gender" Text="Male" />
                                    </div>
                                    <div class="custom-control custom-radio inline-control">
                                        <asp:RadioButton runat="server" ID="rbFeMale" GroupName="gender" Text="Female" />
                                    </div>
                                    <div class="custom-control custom-radio inline-control">
                                        <asp:RadioButton runat="server" ID="rbOther" GroupName="gender" Text="Other" />
                                    </div>
                                    <div>
                                        <asp:CustomValidator runat="server" ID="cvGender" EnableClientScript="true"
                                            CssClass="error" ClientValidationFunction="GenderClientValidate" ErrorMessage="Please select gender"
                                            Display="Dynamic" ValidationGroup="CrRepRen" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Upload Add-On-Photo</label>
                                    <div class="custom-file">
                                        <!--<asp:FileUpload ID="photoUpload" runat="server" />-->
                                        <input type="file" class="custom-file-input customFile" id="photoUpload" aria-describedby="uploadHelp" />
                                        <label class="custom-file-label" for="customFile">Choose Photo</label>
                                    </div>
                                    <small id="uploadHelp" class="form-text">Allowed files .jpeg, jpg or .png</small>
                                    <asp:CustomValidator EnableClientScript="true" ID="cvUploadType"
                                        CssClass="error" runat="server" ErrorMessage="Please select valid image file"
                                        ClientValidationFunction="FileUploadType" ValidateEmptyText="true" ValidationGroup="CrRepRen" Display="Dynamic"></asp:CustomValidator>
                                    <asp:CustomValidator EnableClientScript="true" ID="cvUploadSize"
                                        CssClass="error" runat="server" ErrorMessage="Please select file, size has less than 20K"
                                        ClientValidationFunction="FileUploadSize" ValidationGroup="CrRepRen" ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
                                </div>

                            </div>
                        </asp:View>
                    </asp:MultiView>
                    <div class="form-group">
                        <div class="custom-control custom-checkbox termsConditions">
                            <asp:CheckBox runat="server" ID="chkAgree" Text="I accept the" />
                            <a target="_blank" href="../terms_conditions.htm#request3">Terms & Conditions</a> <span class="orange"></span>
                        </div>
                        <asp:Label runat="server" CssClass="error" ID="lblErrorchkAgree"></asp:Label>
                        <asp:CustomValidator CssClass="error" runat="server" ID="cvchkAgree" ValidationGroup="CrRepRen"
                            EnableClientScript="true" ClientValidationFunction="CheckBoxRequired_ClientValidate"
                            ErrorMessage="Please check the terms and conditions checkbox to proceed further." Display="Dynamic" />
                    </div>
                    <asp:Button runat="server" ID="btnSubmitfinal" CssClass="btn btn-primary btn-lg text-uppercase mr-3 " OnClick="btnSubmitfinal_Click" ValidationGroup="CrRepRen"
                        Text="Submit" />
                    <asp:HiddenField runat="server" ID="hideRequestTypeId" />
                    <asp:HiddenField runat="server" ID="hideRequestTypeADDONId" />
                    <asp:HiddenField runat="server" ID="hideConfirmRequest" />
                </div>
            </div>

        </div>

    </div>

    <script type="text/javascript">
        var date = new Date();
        var currentMonth = date.getMonth();
        var currentDate = date.getDate();
        var currentYear = date.getFullYear();
        $('[id*=txtDOB]').datepicker({
            autoclose: true,
            startDate: new Date(currentYear - 100, currentMonth, currentDate),
            endDate: new Date(currentYear - 18, currentMonth, currentDate),
            format: 'dd/mm/yyyy'
        });
    </script>

</asp:Content>
