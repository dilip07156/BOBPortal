<%@ Page Language="C#" Title="Card Holder Profile" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ProfileOLD.aspx.cs" Inherits="CardHolder.UserManagment.ProfileOLD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {            
            //message section start here
            $(".message-edit-section").hide();
            $("#btn-message-edit").on("click", function () {
                $(this).hide();
                $(".message-edit-section").show();
                $(".message-edit-section").addClass("pt-2");
                $('#btn-mobile-edit, #btn-email-edit').attr("disabled", true);
                $(".from-section").css("opacity", "0.5");
                $(this).closest('.from-section').css("opacity", "1");
            });

            $("#btn-message-cancel").on("click", function () {
                $("#btn-message-edit").show();
                $(".message-edit-section").hide();
                $(".message-edit-section").removeClass("pt-2");
                $('#btn-mobile-edit, #btn-email-edit').attr("disabled", false);
                $(".from-section").css("opacity", "1");
            });

            $("#btn-message-submit").on("click", function () {
                $("#btn-message-edit").show();
                $(".message-edit-section").hide();
                $(".message-edit-section").removeClass("pt-2");
                $('#btn-mobile-edit, #btn-email-edit').attr("disabled", false);
                $(".from-section").css("opacity", "1");
            });
            //message section end here
        });
    </script>
       
   
</asp:Content>

    
      

       <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          
            <!--RIGHT SECTION-->
            <asp:FormView runat="server" ID="frmProfile" Width="980px" DefaultMode="ReadOnly">
            <ItemTemplate>
            <div class="col-lg-8">
                <div class="card">
                    <div class="card-body">
                        <div class="form-display">
                            <div class="from-section">
                                <div class="row">
                                    <!--old mobile display-->
                                    <div class="col-lg-3 col-md col-9 mb-md-0 mb-2">
                                        <div><span class="d-label">Mobile Number</span></div>
                                        <div><span class="d-value"><asp:Label runat="server" ID="lblMobileNumber" /></span></div>
                                    </div>                                                                   
                                </div>

                            </div> 
                            
                            <!--Email id-->
                            <div class="from-section">
                                <div class="row">
                                    <div class="col-12 mb-1"><span class="d-label">Email ID</span></div>
                                    <div class="col-9"><span class="d-value"><%# Eval("CH_Card.EMAIL_ID")%></span></div>                                    
                                </div>
                            </div> 
                            
                            <!--Date of Birth-->
                            <div class="from-section dark-border">
                                <div class=" mb-1"><span class="d-label">Date of Birth</span></div>
                                <div><span class="d-value"><%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("CH_Card.BIRTH_DATE")))%></span></div>
                            </div> 
                            
                            <!--Address Type-->
                            <div class="from-section">
                                <div class="mb-1"><span class="d-label">Address Type</span></div>
                                <div><span class="d-value"> <asp:Label runat="server" ID="lblAddrestype"></asp:Label></span></div>
                            </div>
                            
                            <!--Address-->
                            <div class="from-section">
                                <div class="mb-1"><span class="d-label">Address</span></div>
                                <div><span class="d-value"> <%# Eval("CH_Card.MAILING_ADDRESS1")%>
                                                            <%# Eval("CH_Card.MAILING_ADDRESS2")%>
                                                            <%# Eval("CH_Card.MAILING_ADDRESS3")%>
                                                            <%# Eval("CH_Card.MAILING_ADDRESS4")%></span></div>
                            </div> 
                            
                            <!--City-->
                            <div class="from-section">
                                <div class="mb-1"><span class="d-label">City</span></div>
                                <div><span class="d-value"><%# Eval("CH_Card.CITY_NAME")%></span></div>
                            </div> 

                            <!--Pin Code-->
                            <div class="from-section dark-border">
                                <div class="mb-1"><span class="d-label">Pin Code</span></div>
                                <div><span class="d-value"><%# Eval("CH_Card.MAILING_ZIP_CODE")%></span></div>
                            </div> 
                            
                            <!--Personal Message-->
                            <div class="from-section">
                                <div class="row">
                                    <div class="col-12 mb-1"><span class="d-label">Personal Message</span></div>
                                    <div class="col-9"><span class="d-value"><%# Eval("Personal_Msg")%></span></div>
                                    <div class="col-3 text-right">
                                       
                                        <asp:Button ID="btn-message-edit" runat="server" Text="Edit" OnClick="btnEdit_Click" CssClass="btn btn-link" />
                                </div>

                                <div class="message-edit-section">
                                    <div class="row">
                                        <div class="col-md-7">
                                            <div class="form-group">
                                                <label for="">Personal Message</label>
                                                <input type="text" class="form-control form-control-lg" id="Text3" placeholder="Enter Personal Message">
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <button type="button" class="btn btn-outline-primary btn-lg text-uppercase" id="btn-message-cancel">Cancel</button>                                       
                                        <asp:Button ID="btn-message-submit" runat="server" Text="Submit" OnClick="btnEdit_Click" CssClass="btn btn-primary btn-lg text-uppercase" />
                                    </div>
                                </div>
                            </div>                       
                        </div>
                    </div>
                </div>
            </div>
                </div>
           </ItemTemplate>
            </asp:FormView>

        </asp:Content>
        

    