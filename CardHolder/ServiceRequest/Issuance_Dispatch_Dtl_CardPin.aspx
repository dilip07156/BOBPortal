<%@ Page Title="Issuance_Dispatch Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Issuance_Dispatch_Dtl_CardPin.aspx.cs" Inherits="CardHolder.ServiceRequest.Issuance_Dispatch_Dtl_CardPin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">

        <div class="card-header">
            <h6 class="mb-0">Issuance/Dispatch Details of Card</h6>
        </div>

        <div class="card-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-primary" role="alert" id="DivSuccess" runat="server" style="display: none">
                        <figure class="icon mr-2">
                            <img src="<%= this.Page.GetNewImagePath("success.svg") %>" alt="info-icon" width="22"></figure>
                        <asp:Label ID="LblSuccessMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="alert alert-danger" role="alert" id="DivERROR" runat="server" style="display: none">
                        <figure class="icon mr-2">
                            <img src="<%= this.Page.GetNewImagePath("fail.svg") %>" alt="info-icon" width="22"></figure>
                        <asp:Label ID="LblErrorMessage" runat="server" Text=""></asp:Label>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="alert alert-danger" role="alert" id="DivMessage" runat="server" style="display: none">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="">Credit Card<span class="orange"></span></label>

                        <asp:DropDownList ID="ddlcardlist" runat="server" CssClass="form-control form-control-lg custom-select wide" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlcardlist_SelectedIndexChanged">
                        </asp:DropDownList>
                        <div id="" class="form-text text-primary">
                            Name on Card:
                            <asp:Label ID="lblCardHolder" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="">View details of<span class="orange"></span></label>
                        <asp:DropDownList runat="server" ID="ddlDispatchDtlOf" CssClass="form-control form-control-lg custom-select wide" CausesValidation="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="error" ID="rfvddlDispatchDtlOf" runat="server"
                            ControlToValidate="ddlDispatchDtlOf" Display="Dynamic" ErrorMessage="Please select Card or PIN"
                            InitialValue="-1"></asp:RequiredFieldValidator>

                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Proceed" OnClick="btnSearch_Click"
                        CssClass="btn btn-primary btn-lg text-uppercase" />

                </div>
            </div>
        </div>
    </div>

    <div class="card order-4" runat="server" id="grdDIv" visible="false">
        <div>


            <div class="row  align-items-center mb-2">
                <div class="col-auto mr-auto">
                    <h2 class="sub-title" id="gridheader" runat="server">List of Speed Post Dispatch Details</h2>
                </div>

            </div>
            <div class="row service-request">
                <div class="col-12">
                    <asp:GridView ID="gvSpeedPostDtl" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table-card-row">
                        <AlternatingRowStyle CssClass="secondrow" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="76px" HeaderText="Date of Booking">
                                <ItemTemplate>
                                    <%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("DISPATCH_DATE")))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Awb_No" HeaderText="AirWay Bill No." />
                            <asp:BoundField DataField="Reference_No" HeaderText="Reference_No" />
                            <asp:BoundField DataField="MailingName" HeaderText="Name" />
                            <asp:BoundField DataField="MailingCity" HeaderText="Destination" />
                            <asp:BoundField DataField="Delivery_Status" HeaderText="Delivery Status" />
                            <asp:BoundField DataField="Courier" HeaderText="Mode of Dispatch" />
                            <asp:TemplateField HeaderText="Expected date of delivery">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="--" ID="lblExpctdDeliveryDt"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="firstrow" />
                    </asp:GridView>

                </div>
            </div>

            <div class="row  align-items-center mb-2">
                <div class="col-auto mr-auto">
                    <h2 class="sub-title" id="GridCourier" runat="server">List of Courier Dispatch Details</h2>
                </div>

            </div>
            <div class="row service-request">
                <div class="col-12">
                    <asp:GridView ID="gvCourierDtl" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table-card-row overflow">
                        <AlternatingRowStyle CssClass="secondrow" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="76px" HeaderText="Date of Booking">
                                <ItemTemplate>
                                    <%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("DPUDATEFCH")))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CAWBNO" HeaderText="AirWay Bill No." />
                            <asp:BoundField DataField="CCRCRDREF" HeaderText="Reference_No" />
                            <asp:BoundField DataField="CCNEENAME" HeaderText="Name" />
                            <asp:BoundField DataField="CDSTDESC" HeaderText="Destination " />
                            <asp:TemplateField HeaderStyle-Width="76px" HeaderText="Expected delivery date">
                                <ItemTemplate>
                                    <%# GeneralMethods.FormatDate(Convert.ToDateTime(Eval("DEPTDTDLV")))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CSTATDESC" HeaderText="Status Description" />
                        </Columns>
                        <RowStyle CssClass="firstrow" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
