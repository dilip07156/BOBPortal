<%@ Page Language="C#" Title="Application" AutoEventWireup="true" CodeBehind="Application.aspx.cs"
    Inherits="CardHolder.Application" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Application Form</title>
    <link href="css/style_form.css" type="text/css" rel="stylesheet" />
    <script src="javascript/jquery-1.7.1.min.js" type="text/javascript"></script>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body
        {
            font: 12px/17px Arial,Helvetica,sans-serif !important;
        }
        #tblEmployeeStatus td input, #tblDesignation td input, #tblProfession td input, #tblOwnedVehicle td input
        {
            margin-right: 4px;
        }
        #tblResiStatus td input
        {
            margin: 0 2px 0 2px;
        }
        #popup_box
        {
            top: 50% !important;
            left: 50% !important;
        }
    </style>
    <script type="text/javascript">

        function Showalert() {
            alert('Your Request for online application form has been successfully registered');
        }

    </script>
</head>
<body>
    <form id="MasterPage" runat="server" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <div class="appformWrap">
        <div class="appformHeader">
            <div class="appformLogo">
                <h1>
                    <img src="images/app-form-logo.png" width="233" height="42" alt="Bank of Baroda" /></h1>
            </div>
            <div class="appformTitle">
                <h2>
                    Credit Card Application Form</h2>
            </div>
        </div>
        <div class="appformContent">
            <div id="tabs">
                <ul>
                    <li><a href="#tab-1" id="tab1"><span>1st Page</span></a></li>
                    <li><a href="#tab-2" id="tab2"><span>2nd Page</span></a></li>
                    <li><a href="#tab-3" id="tab3"><span>3rd Page</span></a></li>
                    <li><a href="#tab-4" id="tab4"><span>4th Page</span></a></li>
                </ul>
                <!-- Tabbing 1 -->
                <div id="tab-1" class="tabcontent clearfix">
                    <!-- Application Form 1st step -->
                    <div class="appformLeft">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="45" align="left" valign="top">
                                    I/We Wish to Apply for BOBCARD <span class="red">*</span>
                                </td>
                                <td align="left" valign="top">
                                    <span class="right">
                                        <asp:DropDownList ID="ddlCardType" TabIndex="1" runat="server" CssClass="myselect">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnselectedCard" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnAppId" runat="server" Value="" />
                                        <asp:RequiredFieldValidator CssClass="error" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="ddlCardType" Display="Dynamic" ErrorMessage="Please select card type"
                                            InitialValue="-1" ValidationGroup="ApplicationLot"></asp:RequiredFieldValidator>
                                    </span>
                                </td>
                                <%-- <td align="left" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblCards">
                                        <tr>
                                            <td height="25" align="left" valign="top" width="19%">
                                                
                                                <input type="checkbox" id="p01" value="P01" />
                                                &nbsp;
                                                <label for="p01">
                                                    Silver (Visa)</label>
                                                <asp:HiddenField ID="hdnselectedCard" runat="server" Value="" />
                                            </td>
                                            <td align="left" valign="top" width="32%">
                                                <input type="checkbox" id="chkExGenMst" value="P02" />
                                                &nbsp;
                                                <label for="chkExGenMst">
                                                    Exclusive General Master</label>
                                            </td>
                                            <td align="left" valign="top" width="26%">
                                                <input type="checkbox" id="chkExYuthMst" value="P03" />
                                                &nbsp;
                                                <label for="chkExYuthMst">
                                                    Exclusive Youth Master</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkExWmnMst" value="P04" />
                                                &nbsp;
                                                <label for="chkExWmnMst">
                                                    Exclusive Women Master</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkGoldVisa" value="P05" />
                                                &nbsp;
                                                <label for="chkGoldVisa">
                                                    Gold (Visa)</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkGoldMst" value="P06" />
                                                &nbsp;
                                                <label for="chkGoldMst">
                                                    Gold Master Card</label>
                                            </td>
                                            <td align="left" valign="top">
                                                <input type="checkbox" id="chkGoldIntVisa" value="P07" />
                                                &nbsp;
                                                <label for="chkGoldIntVisa">
                                                    Gold International Visa</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkElite" value="P23" />
                                                &nbsp;
                                                <label for="chkElite">
                                                    Elite</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkPlatMst" value="P21" />
                                                &nbsp;
                                                <label for="chkPlatMst">
                                                    Platinum Master</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkPlatVisa" value="P22" />
                                                &nbsp;
                                                <label for="chkPlatVisa">
                                                    Platinum Visa</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkVisaSign" value="P26" />
                                                &nbsp;
                                                <label for="chkVisaSign">
                                                    Visa Signature</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkVisaSelct" value="P25" />
                                                &nbsp;
                                                <label for="chkVisaSelct">
                                                    Platinum Select</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkCorpPrem" value="P24" />
                                                &nbsp;
                                                <label for="chkCorpPrem">
                                                    Corporate Premium</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkTitanium" value="P29" />
                                                &nbsp;
                                                <label for="chkTitanium">
                                                    Titanium</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkBobAssure" value="P28" />
                                                &nbsp;
                                                <label for="chkBobAssure">
                                                    Bobcard Assure</label>
                                            </td>
                                            <td height="25" align="left" valign="top">
                                                <input type="checkbox" id="chkPlatBBA" value="P27" />
                                                &nbsp;
                                                <label for="chkPlatBBA">
                                                    Platinum-BBA</label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>--%>
                            </tr>
                            <tr>
                                <td height="45" align="left" valign="top">
                                    Application Type
                                </td>
                                <td align="left" valign="top">
                                    <span class="right">
                                        <asp:DropDownList ID="ddlApplicationType" TabIndex="2" runat="server" CssClass="myselect">
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td height="45" align="left" valign="top">
                                    Promo Code
                                </td>
                                <td align="left" valign="top">
                                    <span class="right">
                                        <asp:DropDownList ID="ddlPromoCode" TabIndex="3" runat="server" CssClass="myselect">
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    Cards/ Bill Sent to :
                                </td>
                                <td align="left" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" valign="top" width="33%">
                                                <%--<asp:CheckBox runat="server" ID="chkOffice" Text="       Office" />--%>
                                                <asp:RadioButton ID="rbOffice" TabIndex="4" Text="Office" runat="server" GroupName="BillSentTo" />
                                            </td>
                                            <td align="left" valign="top">
                                                <%--<asp:CheckBox runat="server" ID="chkResi" Text="       Residence" />--%>
                                                <asp:RadioButton ID="rbResidence" TabIndex="5" Text="Residence" runat="server" GroupName="BillSentTo" />
                                            </td>
                                            <td align="left" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="appformRight">
                        <div class="appFormrt1">
                            <table width="100%" border="1" cellspacing="0" cellpadding="0" class="formGrid">
                                <tr>
                                    <td width="45%" height="20" align="left" valign="middle">
                                        Staff EC. No.
                                    </td>
                                    <td align="left" valign="middle" width="55%">
                                        <input name="STAFF_E_C_NO" runat="server" tabindex="6" id="STAFF_E_C_NO" placeholder=""
                                            maxlength="50" type="text" class="uppercase inputText wd100 required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="left" valign="middle">
                                        Name
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="STAFF_NAME" runat="server" tabindex="7" id="STAFF_NAME" placeholder=""
                                            maxlength="50" type="text" class="uppercase inputText wd100 required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="left" valign="middle">
                                        Branch<span class="red">*</span>
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:DropDownList runat="server" ID="ddlBranchlist" TabIndex="8" Style="width: 90px"
                                            CssClass="myselect">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="appFormrt2">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="formGrid">
                                <tr>
                                    <td width="55%" height="20" align="left" valign="middle">
                                        Recommended by
                                    </td>
                                    <td align="left" valign="middle" width="45%">
                                        <asp:DropDownList runat="server" TabIndex="9" ID="ddlRecommendedBy" CssClass="myselect wd150">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="BOB"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="BCL"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="DCL"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Other"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="55%" height="20" align="left" valign="middle">
                                        Recommended Branch
                                    </td>
                                    <td align="left" valign="middle" width="45%">
                                        <asp:DropDownList runat="server" TabIndex="10" ID="ddlRecommendedBranch" CssClass="myselect wd150">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="55%" height="20" align="left" valign="middle">
                                        Ref No.
                                    </td>
                                    <td align="left" valign="middle" width="45%">
                                        <input name="SOURCE_APPLICATION_NO" readonly="readonly" tabindex="11" maxlength="15"
                                            id="SOURCE_APPLICATION_NO" runat="server" type="text" class="inputText wd150" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        UID
                                    </td>
                                    <td height="30" align="left" valign="middle" colspan="3">
                                        <input name="UID" maxlength="12" tabindex="12" placeholder="" id="VC_ALIAS_NAME"
                                            runat="server" type="text" class="wd150 inputText" />
                                    </td>
                                </tr>
                                <%--    <tr>
                                    <td colspan="4" align="center" valign="middle" height="20">
                                        Recommended by
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="center" valign="middle">
                                        BOB Br.
                                    </td>
                                    <td height="20" align="center" valign="middle">
                                        BCL Dr.
                                    </td>
                                    <td height="20" align="center" valign="middle">
                                        BCL Dr.
                                    </td>
                                    <td height="20" align="center" valign="middle">
                                        BCL Dr.
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" colspan="4" align="center" valign="middle">
                                        <input name="RECOMMENDED_BY" runat="server" id="RECOMMENDED_BY" placeholder="" maxlength="1"
                                            type="text" class="uppercase inputText wd300 required" />
                                    </td>
                                </tr>--%>
                            </table>
                            <%--  <table width="83%" border="0" cellspacing="0" cellpadding="0" class="fRight mt10">
                                <tr>
                                    <td align="left" valign="middle" height="25" width="80">
                                        Ref No.
                                    </td>
                                    <td align="left" valign="middle" height="25">
                                        <input name="SOURCE_APPLICATION_NO" maxlength="15" id="SOURCE_APPLICATION_NO" runat="server"
                                            type="text" class="inputText wd200" />
                                    </td>
                                </tr>
                            </table>--%>
                        </div>
                    </div>
                    <!-- Application form title Section -->
                    <div class="appformSubtitle">
                        <h3>
                            Personal Information</h3>
                    </div>
                    <div class="appformPi">
                        <div class="appformPiLt">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt10">
                                <tr>
                                    <td align="left" valign="top" height="35">
                                        Full Name
                                    </td>
                                    <td align="left" valign="top" height="35">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlTitle" TabIndex="13" runat="server" CssClass="myselect wd50">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <input name="FIRST_NAME" runat="server" tabindex="14" id="FIRST_NAME" placeholder="First Name"
                                                        maxlength="26" type="text" class="inputText wd100 onlyAlphabet required blurEmboos" /><span
                                                            class="red">*</span>
                                                </td>
                                                <td>
                                                    <input name="MIDDLE_NAME" runat="server" tabindex="15" id="MIDDLE_NAME" placeholder="Middle Name"
                                                        maxlength="20" type="text" class="inputText wd100 onlyAlphabet required blurEmboos" />
                                                </td>
                                                <td>
                                                    <input name="FAMILY_NAME" runat="server" tabindex="16" id="FAMILY_NAME" placeholder="Family Name"
                                                        maxlength="20" type="text" class="inputText wd100 onlyAlphabet required blurEmboos" /><span
                                                            class="red">*</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div class="linespacer">
                            </div>
                            <table width="98%" border="0" cellspacing="0" cellpadding="0" class="eduBlock1">
                                <tr class="rowLine">
                                    <td height="35">
                                        Gender<span class="red">*</span>
                                    </td>
                                    <td>
                                        <label>
                                            <input type="radio" tabindex="17" name="GENDER" runat="server" id="MALE" value="0" />
                                            Male</label>
                                        <label>
                                            <input type="radio" tabindex="18" name="GENDER" runat="server" id="FEMALE" value="1" />
                                            Female</label>
                                    </td>
                                </tr>
                                <tr class="rowLine">
                                    <td height="35">
                                        Date of Birth<span class="red">*</span>
                                    </td>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <input name="BIRTH_DATE" id="BIRTH_DATE" tabindex="19" runat="server" type="text"
                                                        class="inputText wd100 ClcDate" />
                                                    <span class="hints" id="spanbrth" runat="server">DD/MM/YYYY</span>
                                                </td>
                                                <td>
                                                    Age<span class="red">*</span>
                                                    <input name="AGE" id="AGE" runat="server" tabindex="20" readonly="readonly" type="text"
                                                        maxlength="2" class="inputText wd30 onlynumeric" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="rowLine">
                                    <td height="35">
                                        Marital Status<span class="red">*</span>
                                    </td>
                                    <td>
                                        <%-- <label>
                                            <input type="radio" name="MARITAL_STATUS" id="SINGLE" runat="server" value="0" />
                                            Single</label>
                                        <label>
                                            <input type="radio" name="MARITAL_STATUS" id="MARRIED" runat="server" value="1" />
                                            Married</label>--%>
                                        <asp:DropDownList ID="ddlMaritalStatus" TabIndex="21" runat="server" CssClass="myselect wd150">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="rowLine">
                                    <td height="35">
                                        Date of Marriage Anniversary
                                    </td>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <!-- There is no associate field in table -->
                                                    <select runat="server" id="ddlMarriageDay" tabindex="22" name="ddlMarriageDay" class="myselect wd50">
                                                        <option>DD</option>
                                                        <option>01</option>
                                                        <option>02</option>
                                                        <option>03</option>
                                                        <option>04</option>
                                                        <option>05</option>
                                                        <option>06</option>
                                                        <option>07</option>
                                                        <option>08</option>
                                                        <option>09</option>
                                                        <option>10</option>
                                                        <option>11</option>
                                                        <option>12</option>
                                                        <option>13</option>
                                                        <option>14</option>
                                                        <option>15</option>
                                                        <option>16</option>
                                                        <option>17</option>
                                                        <option>18</option>
                                                        <option>19</option>
                                                        <option>20</option>
                                                        <option>21</option>
                                                        <option>22</option>
                                                        <option>23</option>
                                                        <option>24</option>
                                                        <option>25</option>
                                                        <option>26</option>
                                                        <option>27</option>
                                                        <option>28</option>
                                                        <option>29</option>
                                                        <option>30</option>
                                                        <option>31</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <select runat="server" id="ddlMarriageMonth" tabindex="23" name="ddlMarriageMonth"
                                                        class="myselect wd50">
                                                        <option>MM</option>
                                                        <option>01</option>
                                                        <option>02</option>
                                                        <option>03</option>
                                                        <option>04</option>
                                                        <option>05</option>
                                                        <option>06</option>
                                                        <option>07</option>
                                                        <option>08</option>
                                                        <option>09</option>
                                                        <option>10</option>
                                                        <option>11</option>
                                                        <option>12</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    No of Dependents
                                                </td>
                                                <td>
                                                    <input name="NO_OF_DEPENDENTS" id="NO_OF_DEPENDENTS" tabindex="24" runat="server"
                                                        type="text" maxlength="2" class="inputText wd30 onlynumeric" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="35">
                                        Nationality
                                    </td>
                                    <td>
                                        <label>
                                            <input type="radio" name="NATIONALITY" value="356" tabindex="25" id="Residentindian"
                                                runat="server" />
                                            Indian</label>
                                        <label>
                                            <input type="radio" name="NATIONALITY" tabindex="26" value="000" id="NonResidentIndian"
                                                runat="server" />
                                            Non Resident Indian</label>
                                    </td>
                                </tr>
                            </table>
                            <div class="linespacer">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                                <tr>
                                    <td height="30" align="left" valign="middle" style="width: 135px">
                                        Present Address 1<span class="red">*</span>
                                    </td>
                                    <td height="30" align="left" valign="middle" colspan="3">
                                        <%--<textarea name="CURR_ADDRESS" id="CURR_ADDRESS" runat="server" class="wd300 ht50 textArea"></textarea>--%>
                                        <input runat="server" type="text" tabindex="35" name="CURR_ADDRESS1" id="CURR_ADDRESS1"
                                            maxlength="45" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Present Address 2
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="CURR_ADDRESS2" maxlength="45" tabindex="36" id="CURR_ADDRESS2" runat="server"
                                            type="text" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Present Address 3
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="CURR_ADDRESS3" maxlength="45" tabindex="37" id="CURR_ADDRESS3" runat="server"
                                            type="text" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Present Address 4
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="CURR_ADDRESS4" maxlength="45" tabindex="38" id="CURR_ADDRESS4" runat="server"
                                            type="text" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Changed your Address?
                                    </td>
                                    <td style="height: 29px">
                                        <asp:DropDownList runat="server" ID="ddlchangeResi" TabIndex="39" CssClass="myselect wd200">
                                        </asp:DropDownList>
                                        <%-- <label>
                                            <input runat="server" type="radio" name="RESIDANCE_CHANGED" value="0" id="Onceinlast3years" />
                                            No
                                        </label>
                                        <label>
                                            <input runat="server" type="radio" name="RESIDANCE_CHANGED" value="1" id="NotChanged" />
                                           Once in last 3yr
                                        </label>
                                        <label>
                                            <input runat="server" type="radio" name="RESIDANCE_CHANGED" value="2" id="MorethanOnceinlast3Yr" />
                                             More than Once in last 3Yr
                                        </label>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="appformPiRt">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt10 ml44">
                                <tr>
                                    <td height="35" align="right" valign="top">
                                        <div style="text-align: right; float: right; padding-right: 10px; padding-top: 5px;">
                                            Your name as you would like to have on card<span class="red">*</span></div>
                                    </td>
                                    <td height="35" align="left" valign="top">
                                        <input name="EMBOSSED_NAME" maxlength="27" id="EMBOSSED_NAME" runat="server" type="text"
                                            class="inputText wd150 onlyAlphabet" />
                                    </td>
                                </tr>
                            </table>
                            <div class="linespacer">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="eduBlock">
                                <tr>
                                    <td height="22">
                                        Education Qualification<span class="red">*</span>
                                    </td>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" TabIndex="28" ID="ddlEducation" CssClass="myselect wd150">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22">
                                        University/Institution
                                    </td>
                                    <td>
                                        <input name="UNIVERSITY" maxlength="25" tabindex="29" runat="server" id="UNIVERSITY"
                                            type="text" class="inputText wd300 onlyAlphabet" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22">
                                        Father's Name
                                    </td>
                                    <td>
                                        <input name="FATHER_NAME" maxlength="25" runat="server" tabindex="30" id="FATHER_NAME"
                                            type="text" class="inputText wd300 onlyAlphabet" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22">
                                        Mother's Maiden Name
                                    </td>
                                    <td>
                                        <input name="MAIDEN_NAME" maxlength="25" runat="server" tabindex="31" id="MAIDEN_NAME"
                                            type="text" class="inputText wd300 onlyAlphabet" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22">
                                        Spouse Name
                                    </td>
                                    <td>
                                        <input maxlength="40" name="SPOUSE_NAME" runat="server" tabindex="32" id="SPOUSE_NAME"
                                            type="text" class="inputText wd300 onlyAlphabet" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22">
                                        Mobile no. Of Spouse
                                    </td>
                                    <td>
                                        <input name="SPOUSE_MOB_NO" maxlength="12" runat="server" tabindex="33" id="SPOUSE_MOB_NO"
                                            type="text" class="inputText wd200 onlynumeric" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22">
                                        Vehicle Owned
                                    </td>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblOwnedVehicle">
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList runat="server" RepeatDirection="Horizontal" TabIndex="34" RepeatLayout="Table"
                                                        Width="100%" RepeatColumns="4" ID="chkOwnedVehicle">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <div class="linespacer">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt10">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Country<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <div>
                                                        <asp:UpdatePanel ID="UpCurrCountry" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                            <ContentTemplate>
                                                                <asp:DropDownList runat="server" TabIndex="40" ID="ddlCurrCountry" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlCurrCountry_SelectedIndexChanged" CssClass="myselect wd150">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpCurrCountry"
                                                            DynamicLayout="true">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgLoading222" AlternateText="loading" runat="server" ImageUrl="~/images/loading3.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>
                                                    <%--<input name="CURR_COUNTRY_CODE" runat="server" id="CURR_COUNTRY_CODE" type="text" class="wd200 inputText" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    City<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                            <ContentTemplate>
                                                                <asp:DropDownList runat="server" TabIndex="41" ID="ddlCurrCity" CssClass="myselect wd150">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <%--      <input name="CURR_CITY_CODE" maxlength="5" id="CURR_CITY_CODE" runat="server" type="text"
                                                        class="wd200 inputText" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Pincode
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input name="CURR_POSTAL_CODE" maxlength="6" tabindex="42" id="CURR_POSTAL_CODE"
                                                        runat="server" type="text" class="wd200 inputText onlynumeric" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Mobile<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input runat="server" maxlength="10" tabindex="43" name="MOBILE_NUMBER" id="MOBILE_NUMBER"
                                                        type="text" class="wd200 inputText onlynumeric" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Email Id
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input maxlength="50" runat="server" tabindex="44" name="EMAIL_ID" id="EMAIL_ID"
                                                        type="text" class="wd200 inputText" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Resi No.<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input name="EXT" id="EXT" maxlength="5" tabindex="45" runat="server" type="text"
                                                        class="wd30 inputText onlynumeric" />
                                                    <input name="HOME_PHONE_NUMBER" maxlength="15" tabindex="46" id="HOME_PHONE_NUMBER"
                                                        runat="server" type="text" class="wd150 inputText ml10 onlynumeric" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 3px">
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Resi. Status<span class="red">*</span>
                                    </td>
                                    <td align="left" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblResiStatus">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="radResiSatus" TabIndex="47" RepeatColumns="5" Width="100%"
                                                        RepeatLayout="Table" RepeatDirection="Horizontal" runat="server">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="linespacer">
                            &nbsp;</div>
                        <div class="appformPiLt">
                            <div class="ticker">
                                <input name="chkTickPermanentAddress" tabindex="48" id="chkTickPermanentAddress"
                                    runat="server" type="checkbox" value="" />
                                Tick here if Permanent address is same as above
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblPermanentAddress"
                                class="mt10">
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Permanent Address 1<span class="red">*</span>
                                    </td>
                                    <td height="30" align="left" valign="middle" colspan="3">
                                        <input runat="server" type="text" tabindex="49" name="PERM_ADDRESS1" id="PERM_ADDRESS1"
                                            maxlength="45" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Permanent Address 2
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="PERM_ADDRESS2" maxlength="45" tabindex="50" id="PERM_ADDRESS2" runat="server"
                                            type="text" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Permanent Address 3
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="PERM_ADDRESS3" maxlength="45" tabindex="51" id="PERM_ADDRESS3" runat="server"
                                            type="text" class="wd260 inputText" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="left" valign="middle">
                                        Permanent Address 4
                                    </td>
                                    <td align="left" valign="middle">
                                        <input name="PERM_ADDRESS4" maxlength="45" tabindex="52" id="PERM_ADDRESS4" runat="server"
                                            type="text" class="wd260 inputText" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="appformPiRt">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt10" id="tblrightPermAddress">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Country<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                            <ContentTemplate>
                                                                <asp:DropDownList runat="server" TabIndex="53" ID="ddlPermCountry" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlPermCountry_SelectedIndexChanged" CssClass="myselect wd150">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                                                            DynamicLayout="true">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgLoading" AlternateText="loading" runat="server" ImageUrl="~/images/loading3.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>
                                                    <%--<input name="PERM_COUNTRY_CODE" id="PERM_COUNTRY_CODE" type="text" class="inputText wd200" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    City<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel4" tabindex="54" runat="server" ChildrenAsTriggers="true"
                                                            UpdateMode="always">
                                                            <ContentTemplate>
                                                                <asp:DropDownList runat="server" ID="ddlPermCity" CssClass="myselect wd150">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <%-- <input name="PERM_CITY_CODE" maxlength="5" id="PERM_CITY_CODE" runat="server" type="text"
                                            class="inputText wd200" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Pincode
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input name="PERM_POSTAL_CODE" maxlength="6" tabindex="55" id="PERM_POSTAL_CODE"
                                                        runat="server" type="text" class="inputText wd150 onlynumeric" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Passport Number
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input name="PASSPORT_NUMBER" maxlength="15" tabindex="56" id="PASSPORT_NUMBER" runat="server"
                                                        type="text" class="inputText wd150" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    VIP Code<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:DropDownList runat="server" TabIndex="57" ID="ddlVIPCode" CssClass="myselect wd150">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Social Status<span class="red">*</span>
                                                </td>
                                                <td align="left" valign="middle">
                                                    <asp:DropDownList runat="server" TabIndex="58" ID="ddlSocialStatus" CssClass="myselect wd150">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    Resi No.
                                                </td>
                                                <td align="left" valign="middle">
                                                    <!-- Need to find associate fields in table -->
                                                    <input name="PERM_EXT" id="PERM_EXT" maxlength="3" tabindex="59" type="text" class="inputText wd30 onlynumeric" />
                                                    <input name="PERM_TELEPHONE_NO" maxlength="15" tabindex="60" id="PERM_TELEPHONE_NO"
                                                        type="text" class="inputText wd134 ml10 onlynumeric" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30" align="left" valign="middle">
                                                    D/L Number
                                                </td>
                                                <td align="left" valign="middle">
                                                    <input name="DRIVING_LICENSE_NUMBER" tabindex="61" maxlength="20" placeholder="Driving License Number"
                                                        id="DRIVING_LICENSE_NUMBER" runat="server" type="text" class="inputText" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="appformSubtitle">
                        <span class="appformSubtitle2">(For Verification)</span>
                        <h3>
                            About Your occupation</h3>
                    </div>
                    <div class="appformOcc">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt10">
                            <tr>
                                <td align="left" valign="middle" height="30" width="200">
                                    Occupation
                                </td>
                                <td align="left" valign="middle" height="30" colspan="3">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblEmployeeStatus">
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList runat="server" TabIndex="62" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                    Width="100%" RepeatColumns="5" ID="chkEmpStatus">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="30">
                                    Name of Organisation/Employer
                                </td>
                                <td align="left" valign="middle" height="30">
                                    <input name="EMPLOYER_NAME" tabindex="63" maxlength="40" id="EMPLOYER_NAME" runat="server"
                                        type="text" class="inputText wd300" />
                                </td>
                                <td align="left" valign="middle" height="30">
                                    Employer Type<span class="red">*</span>
                                </td>
                                <td align="left" valign="middle" height="30">
                                    <asp:DropDownList runat="server" TabIndex="64" ID="ddlempType" CssClass="myselect wd150">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="30" class="pt10">
                                    Your Designation
                                </td>
                                <td align="left" valign="middle" height="30" colspan="3" class="pt10">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblDesignation">
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList runat="server" TabIndex="65" ID="chkdesignation" Width="100%" RepeatColumns="5"
                                                    RepeatDirection="Horizontal" RepeatLayout="Table">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="30" class="pt10">
                                    Your Profession<span class="red">*</span>
                                </td>
                                <td align="left" valign="middle" height="30" colspan="3" class="pt10">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblProfession">
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList runat="server" TabIndex="66" ID="chkEmpProfession" Width="100%"
                                                    RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="35">
                                    Department
                                </td>
                                <td align="left" valign="middle" height="35">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <input maxlength="50" name="EMPL_DEPARTMENT" tabindex="67" id="EMPL_DEPARTMENT" runat="server"
                                                    type="text" class="inputText wd200" />
                                            </td>
                                            <td>
                                                <!-- Need to find associate fields in table -->
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            Duration in Current org.<span class="red">*</span>
                                                        </td>
                                                        <td>
                                                            <input name="CURRENT_JOB_TENURE" tabindex="68" id="CURRENT_JOB_TENURE" maxlength="2"
                                                                placeholder="Years" runat="server" type="text" class="inputText wd50 onlynumeric" />
                                                        </td>
                                                        <td>
                                                            <input name="JOB_MONTHS" id="JOB_MONTHS" tabindex="69" runat="server" maxlength="2"
                                                                placeholder="Months" type="text" class="inputText wd50 onlynumeric" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    Employee Code
                                </td>
                                <td>
                                    <input name="EMPL_ID" id="EMPL_ID" runat="server" tabindex="70" type="text" maxlength="20"
                                        class="inputText wd100" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Your Present Office Address
                                </td>
                                <td colspan="3">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="Table1" class="mt10">
                                                    <tr>
                                                        <td height="30" align="left" valign="middle" colspan="3">
                                                            <input runat="server" type="text" tabindex="71" placeholder="Present Office Address 1"
                                                                name="EMPL_ADDRESS1" id="EMPL_ADDRESS1" maxlength="45" class="wd200 inputText" /><span
                                                                    class="red">*</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="middle">
                                                            <input name="EMPL_ADDRESS2" maxlength="45" tabindex="72" placeholder="Present Office Address 2"
                                                                id="EMPL_ADDRESS2" runat="server" type="text" class="wd200 inputText" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="middle">
                                                            <input name="EMPL_ADDRESS3" maxlength="45" tabindex="73" placeholder="Present Office Address 3"
                                                                id="EMPL_ADDRESS3" runat="server" type="text" class="wd200 inputText" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="middle">
                                                            <input name="EMPL_ADDRESS4" maxlength="45" id="EMPL_ADDRESS4" tabindex="74" placeholder="Present Office Address 4"
                                                                runat="server" type="text" class="wd200 inputText" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td height="30">
                                                            Country<span class="red">*</span>
                                                        </td>
                                                        <td>
                                                            <div>
                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlEmpCountry" TabIndex="75" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlEmpCountry_SelectedIndexChanged" CssClass="myselect wd150">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel5"
                                                                    DynamicLayout="true">
                                                                    <ProgressTemplate>
                                                                        <asp:Image ID="imgLoading5" AlternateText="loading" runat="server" ImageUrl="~/images/loading3.gif" />
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                            </div>
                                                            <%--<input maxlength="5" name="EMPL_CITY_CODE" id="EMPL_CITY_CODE" runat="server" type="text"
                                                                class="inputText wd150" />--%>
                                                        </td>
                                                        <td>
                                                            Office Number
                                                        </td>
                                                        <td>
                                                            <input name="OFFICE_PHONE_NUMBER" maxlength="20" tabindex="77" id="OFFICE_PHONE_NUMBER"
                                                                runat="server" type="text" class="inputText wd150 onlynumeric" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30">
                                                            City<span class="red">*</span>
                                                        </td>
                                                        <td>
                                                            <div>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" tabindex="76" ChildrenAsTriggers="true"
                                                                    UpdateMode="always">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlEmpCity" runat="server" CssClass="myselect wd150">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            Pincode<span class="red">*</span>
                                                        </td>
                                                        <td>
                                                            <input maxlength="6" name="EMPL_POSTAL_CODE" tabindex="78" id="EMPL_POSTAL_CODE"
                                                                runat="server" type="text" class="inputText wd150 onlynumeric" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="appformSubtitle">
                        <h3>
                            Details of income, Bank &amp; Financial Outstandings</h3>
                    </div>
                    <div class="appformFo">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="35" align="left" valign="middle" width="200">
                                    Annual income (In Rs.)<span class="red">*</span>
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="ANNUAL_INCOME_CODE" maxlength="12" tabindex="79" id="ANNUAL_INCOME_CODE"
                                        runat="server" type="text" class="wd150 inputText onlynumeric" />
                                </td>
                                <td height="35" align="left" valign="middle" width="130">
                                    Other Income(In Rs.)
                                </td>
                                <td height="35" align="left" valign="middle" width="160">
                                    <input name="OTHER_INCOME" id="OTHER_INCOME" tabindex="80" runat="server" maxlength="12"
                                        type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                                <td height="35" align="left" valign="middle" width="140">
                                    Spouse Income(In Rs.)
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="SPOUSE_INCOME" id="SPOUSE_INCOME" tabindex="81" runat="server" maxlength="12"
                                        type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    Income Per Month
                                </td>
                                <td colspan="3" height="35" align="left" valign="middle">
                                    <asp:RadioButtonList ID="radIncomePerMonth" runat="server" TabIndex="82" RepeatDirection="Horizontal"
                                        RepeatLayout="Table" Width="100%" RepeatColumns="4">
                                    </asp:RadioButtonList>
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Customer ID
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input runat="server" id="CUSTOMER_ID" name="CUSTOMER_ID" tabindex="83" maxlength="10"
                                        type="text" class="wd150 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    PAN No.<span class="red">*</span>
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="PAN_GIR_NO" id="PAN_GIR_NO" maxlength="10" tabindex="84" onblur="ValidatePAN(this);"
                                        runat="server" type="text" class="wd150 inputText" />
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Tax Paid (In Rs.)
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="TAX_PAID" maxlength="12" id="TAX_PAID" runat="server" tabindex="85"
                                        type="text" class="wd150 inputText onlynumeric" />
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Year of Tax Paid
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="YEAR_TAX_PAID" maxlength="4" id="YEAR_TAX_PAID" runat="server" tabindex="86"
                                        type="text" class="wd150 inputText onlynumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    Is Account With BOB
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlIsAccountWithbank" TabIndex="87" CssClass="myselect wd200">
                                    </asp:DropDownList>
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Account Branch
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <asp:DropDownList runat="server" ID="ddlAccountBranch" TabIndex="88" CssClass="myselect wd200">
                                    </asp:DropDownList>
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <%-- Account Number--%>
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <%--<input name="ACCOUNT_NUMBER" id="ACCOUNT_NUMBER" maxlength="24" runat="server" type="text"
                                        class="wd150 inputText" />--%>
                                </td>
                            </tr>
                            <%--  <tr>
                                <td height="35" align="left" valign="middle">
                                    Bank Name
                                </td>
                                <td colspan="3" height="35" align="left" valign="middle">
                                    <input name="OTHER_BANK_NAME" id="OTHER_BANK_NAME" maxlength="40" runat="server"
                                        type="text" class="wd300 inputText" />
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Branch Name
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="OTHER_BRANCH" id="OTHER_BRANCH" maxlength="50" runat="server" type="text"
                                        class="wd150 inputText" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    Address
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <!-- need to find associate field -->
                                    <textarea name="BANK_BRANCH_ADDRESS" id="BANK_BRANCH_ADDRESS" tabindex="89" runat="server"
                                        class="textArea wd200 ht50"></textarea>
                                </td>
                                <td colspan="4" height="35" align="left" valign="middle">
                                    <table width="98%" border="0" cellspacing="0" cellpadding="0" class="ml10">
                                        <tr>
                                            <td height="45" width="85" align="left" valign="middle">
                                                City
                                            </td>
                                            <td height="45" align="left" valign="middle">
                                                <!-- Need to find associate field in table -->
                                                <input name="OTHER_CITY" id="OTHER_CITY" tabindex="90" runat="server" maxlength="50"
                                                    type="text" class="wd150 inputText" />
                                            </td>
                                            <td height="45" align="left" valign="middle" width="140">
                                                No. of Years with Bank
                                            </td>
                                            <td height="45" align="left" valign="middle">
                                                <!-- Need to find associate field in table -->
                                                <input name="OUR_ACCOUNT_TENURE" maxlength="4" tabindex="91" id="OUR_ACCOUNT_TENURE"
                                                    runat="server" type="text" class="wd150 inputText onlynumeric" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <!-- Need to find associate fields -->
                                            <td height="45" align="left" valign="middle">
                                                Natures of A/c
                                            </td>
                                            <td height="45" align="left" valign="middle" id="tdAcounts">
                                                <span class="fLeft">
                                                    <input name="OUR_ACCOUNT_TYPE" title="OUR_ACCOUNT_TYPE" tabindex="92" id="saving_acc"
                                                        runat="server" type="checkbox" value="0" />
                                                    Savings A/c</span> <span class="fLeft ml10">
                                                        <input name="OUR_ACCOUNT_TYPE" title="OUR_ACCOUNT_TYPE" tabindex="93" id="other_acc"
                                                            runat="server" type="checkbox" value="1" />
                                                        Other</span> <span class="fLeft">
                                                            <input name="OUR_ACCOUNT_TYPE" title="OUR_ACCOUNT_TYPE" tabindex="94" id="current_acc"
                                                                runat="server" type="checkbox" value="2" />
                                                            Current A/c</span>
                                            </td>
                                            <td height="45" align="left" valign="middle">
                                                CBS A/c Number
                                            </td>
                                            <td height="45" align="left" valign="middle">
                                                <input name="ACCOUNT_NUMBER" id="ACCOUNT_NUMBER" maxlength="24" tabindex="95" runat="server"
                                                    type="text" class="wd150 inputText" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    Type of Loan
                                </td>
                                <td colspan="5" height="35" align="left" valign="middle">
                                    <table width="80%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td height="35" align="left" valign="middle">
                                                <input name="HOUSING_LOAN" id="HOUSING_LOAN" tabindex="96" runat="server" type="checkbox" />
                                                Housing Loan
                                            </td>
                                            <td height="35" align="left" valign="middle">
                                                <input name="CAR_LOAN" id="CAR_LOAN" runat="server" tabindex="97" type="checkbox" />
                                                Car Loan
                                            </td>
                                            <td height="35" align="left" valign="middle">
                                                <input name="CONSUMER_LOAN" id="CONSUMER_LOAN" runat="server" tabindex="98" type="checkbox" />
                                                Consumer Loan
                                            </td>
                                            <td height="35" align="left" valign="middle">
                                                <input name="BUSINESS_LOAN" id="BUSINESS_LOAN" runat="server" tabindex="99" type="checkbox" />
                                                Business Loan
                                            </td>
                                            <td height="35" align="left" valign="middle">
                                                <input name="OTHR_LOAN" id="OTHR_LOAN" runat="server" type="checkbox" tabindex="100" />
                                                Others
                                            </td>
                                            <td height="35" align="left" valign="middle">
                                                <input name="OTHER_LOAN" maxlength="30" id="OTHER_LOAN" runat="server" tabindex="101"
                                                    type="text" class="wd30 inputText" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    Loan Amount
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="LOAN_AMOUNT" maxlength="20" id="LOAN_AMOUNT" tabindex="101" runat="server"
                                        type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Current Outstandings
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="CURRENT_OUTSTANDING" maxlength="20" id="CURRENT_OUTSTANDING" tabindex="103"
                                        runat="server" type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Duration of Loan
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input name="DURATION_OF_LOAN" maxlength="3" id="DURATION_OF_LOAN" tabindex="104"
                                        runat="server" type="text" class="wd150 inputText onlynumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td height="35" align="left" valign="middle">
                                    Name of Institution from<br />
                                    where Loan taken
                                </td>
                                <td colspan="3" height="35" align="left" valign="middle">
                                    <input maxlength="25" name="LOAN_INSTUTION_NAME" id="LOAN_INSTUTION_NAME" tabindex="105"
                                        runat="server" type="text" class="wd300 inputText" />
                                </td>
                                <td height="35" align="left" valign="middle">
                                    Branch Name
                                </td>
                                <td height="35" align="left" valign="middle">
                                    <input maxlength="25" name="LOAN_BRANCH" id="LOAN_BRANCH" tabindex="106" runat="server"
                                        type="text" class="wd150 inputText" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="appformbutt">
                        <center>
                            <a href="#" id="linkResetPage1" name="linkResetPage1" class="orangeBtn"><span>Reset</span></a>
                            <a href="#tab-2" id="tab_2" class="orangeBtn"><span>Proceed to Next Page</span></a>
                            <asp:LinkButton ID="lnkTab_2" OnClientClick="return validateForm()" Style="display: none;"
                                runat="server">LinkButton</asp:LinkButton>
                        </center>
                    </div>
                </div>
                <!-- Tabbing 2 -->
                <div id="tab-2" class="tabcontent clearfix">
                    <div class="appformSubtitle">
                        <h3>
                            Other Card Details</h3>
                    </div>
                    <div class="appformOthercrddtl">
                        <table width="80%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    Debit Card No.
                                </td>
                                <td width="210">
                                    <input name="BOB_DEBIT_CARD_NO" maxlength="16" id="BOB_DEBIT_CARD_NO" runat="server"
                                        type="text" class="wd200 inputText" />
                                </td>
                                <td>
                                    Valid Upto
                                </td>
                                <td>
                                    <div class="fLeft">
                                        <select name="DC_VALID_UPTO_MONTH" id="DC_VALID_UPTO_MONTH" tabindex="107" runat="server"
                                            class="myselect wd100">
                                            <option>MM</option>
                                            <option>01</option>
                                            <option>02</option>
                                            <option>03</option>
                                            <option>04</option>
                                            <option>05</option>
                                            <option>06</option>
                                            <option>07</option>
                                            <option>08</option>
                                            <option>09</option>
                                            <option>10</option>
                                            <option>11</option>
                                            <option>12</option>
                                        </select>
                                    </div>
                                    <div class="fLeft ml10">
                                        <select name="DC_VALID_UPTO_YEAR" id="DC_VALID_UPTO_YEAR" tabindex="108" runat="server"
                                            class="myselect wd100">
                                        </select>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="hints">
                                    In case already holding other credit cards, last bill statement to be attached.
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="30">
                                    &nbsp;
                                </td>
                                <td>
                                    Bank's Name
                                </td>
                                <td>
                                    Card No.
                                </td>
                                <td>
                                    Valid Up to
                                </td>
                                <td>
                                    Credit Limit
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="35">
                                    1
                                </td>
                                <td>
                                    <input name="CC_BANK_NAME1" maxlength="50" tabindex="109" id="CC_BANK_NAME1" runat="server"
                                        type="text" class="wd300 inputText" />
                                </td>
                                <td>
                                    <input name="CC_NO1" maxlength="16" id="CC_NO1" runat="server" tabindex="110" type="text"
                                        class="wd150 inputText" />
                                </td>
                                <td>
                                    <div class="fLeft">
                                        <select name="CC_VALID_UPTO1_MONTH" id="CC_VALID_UPTO1_MONTH" tabindex="111" runat="server"
                                            class="myselect wd100">
                                            <option>MM</option>
                                            <option>01</option>
                                            <option>02</option>
                                            <option>03</option>
                                            <option>04</option>
                                            <option>05</option>
                                            <option>06</option>
                                            <option>07</option>
                                            <option>08</option>
                                            <option>09</option>
                                            <option>10</option>
                                            <option>11</option>
                                            <option>12</option>
                                        </select>
                                    </div>
                                    <div class="fLeft ml10">
                                        <select name="CC_VALID_UPTO1_YEAR" id="CC_VALID_UPTO1_YEAR" tabindex="112" runat="server"
                                            class="myselect wd100">
                                        </select>
                                    </div>
                                </td>
                                <td>
                                    <input name="CC_CR_LITMIT1" maxlength="15" id="CC_CR_LITMIT1" tabindex="113" runat="server"
                                        type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="35">
                                    2
                                </td>
                                <td>
                                    <input name="CC_BANK_NAME2" maxlength="50" id="CC_BANK_NAME2" tabindex="114" runat="server"
                                        type="text" class="wd300 inputText" />
                                </td>
                                <td>
                                    <input name="CC_NO2" runat="server" maxlength="16" id="CC_NO2" tabindex="115" type="text"
                                        class="wd150 inputText" />
                                </td>
                                <td>
                                    <div class="fLeft">
                                        <select name="CC_VALID_UPTO2_MONTH" id="CC_VALID_UPTO2_MONTH" tabindex="116" runat="server"
                                            class="myselect wd100">
                                            <option>MM</option>
                                            <option>01</option>
                                            <option>02</option>
                                            <option>03</option>
                                            <option>04</option>
                                            <option>05</option>
                                            <option>06</option>
                                            <option>07</option>
                                            <option>08</option>
                                            <option>09</option>
                                            <option>10</option>
                                            <option>11</option>
                                            <option>12</option>
                                        </select>
                                    </div>
                                    <div class="fLeft ml10">
                                        <select name="CC_VALID_UPTO2_YEAR" id="CC_VALID_UPTO2_YEAR" tabindex="117" runat="server"
                                            class="myselect wd100">
                                        </select>
                                    </div>
                                </td>
                                <td>
                                    <input name="CC_CR_LITMIT2" maxlength="15" id="CC_CR_LITMIT2" tabindex="118" runat="server"
                                        type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" height="35">
                                    3
                                </td>
                                <td>
                                    <input name="CC_BANK_NAME3" maxlength="50" id="CC_BANK_NAME3" tabindex="119" runat="server"
                                        type="text" class="wd300 inputText" />
                                </td>
                                <td>
                                    <input name="CC_NO3" maxlength="16" id="CC_NO3" runat="server" tabindex="120" type="text"
                                        class="wd150 inputText" />
                                </td>
                                <td>
                                    <div class="fLeft">
                                        <select name="CC_VALID_UPTO3_MONTH" id="CC_VALID_UPTO3_MONTH" tabindex="121" runat="server"
                                            class="myselect wd100">
                                            <option>MM</option>
                                            <option>01</option>
                                            <option>02</option>
                                            <option>03</option>
                                            <option>04</option>
                                            <option>05</option>
                                            <option>06</option>
                                            <option>07</option>
                                            <option>08</option>
                                            <option>09</option>
                                            <option>10</option>
                                            <option>11</option>
                                            <option>12</option>
                                        </select>
                                    </div>
                                    <div class="fLeft ml10">
                                        <select name="CC_VALID_UPTO3_YEAR" id="CC_VALID_UPTO3_YEAR" runat="server" tabindex="122"
                                            class="myselect wd100">
                                        </select>
                                    </div>
                                </td>
                                <td>
                                    <input name="CC_CR_LITMIT3" maxlength="15" id="CC_CR_LITMIT3" runat="server" tabindex="123"
                                        type="text" class="wd150 inputText onlyDecimalnumeric" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Need to find association fields for while add-ons section -->
                    <div class="appformSubtitle">
                        <h3>
                            Add-on Cards</h3>
                    </div>
                    <div class="appformcrdaddons">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="30" height="30" align="left" valign="middle">
                                    &nbsp;
                                </td>
                                <td colspan="5">
                                    I would like to apply for Add-on Cards for
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    1
                                </td>
                                <td>
                                    <input name="ADDITIONAL_CARD_NAME" maxlength="40" tabindex="124" placeholder="First Addon Applicant Name"
                                        id="ADDITIONAL_CARD_NAME" runat="server" type="text" class="inputText wd300" />
                                </td>
                                <td width="100" align="left" valign="middle">
                                </td>
                                <td>
                                    Date of Birth
                                </td>
                                <td>
                                    <input name="SEC_BIRTH_DATE" id="SEC_BIRTH_DATE" tabindex="125" runat="server" type="text"
                                        class="wd100 inputText dp" />
                                </td>
                                <td>
                                    Gender
                                </td>
                                <td width="100" align="left" valign="middle">
                                    <asp:RadioButtonList runat="server" TabIndex="126" ID="radAddGender">
                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList runat="server" TabIndex="127" ID="radAddonRelation" RepeatColumns="4"
                                        Width="100%" RepeatLayout="Table" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    Occupation
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList runat="server" TabIndex="128" ID="SEC1_APPLICANT_PROF" CssClass="myselect wd100">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    2
                                </td>
                                <td>
                                    <input name="" id="SEC2_FIRST_NAME" runat="server" tabindex="129" type="text" placeholder="Second Addon Applicant Name"
                                        class="uppercase inputText wd300" />
                                </td>
                                <td width="100" align="left" valign="middle">
                                </td>
                                <td>
                                    Date of Birth
                                </td>
                                <td>
                                    <input name="SEC2_BIRTH_DATE" id="SEC2_BIRTH_DATE" tabindex="130" runat="server"
                                        type="text" class="wd100 inputText dp" />
                                </td>
                                <td>
                                    Gender
                                </td>
                                <td width="100" align="left" valign="middle">
                                    <asp:RadioButtonList runat="server" TabIndex="131" ID="radAddGender2">
                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList runat="server" TabIndex="132" ID="radAddon1Relation" RepeatColumns="4"
                                        Width="100%" RepeatLayout="Table" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    Occupation
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList runat="server" TabIndex="133" ID="SEC2_APPLICANT_PROF" CssClass="myselect wd100">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    3
                                </td>
                                <td>
                                    <input name="" type="text" tabindex="134" placeholder="Third Addon Applicant Name"
                                        class="inputText wd300" />
                                </td>
                                <td width="100" align="left" valign="middle">
                                </td>
                                <td>
                                    Date of Birth
                                </td>
                                <td>
                                    <input name="" type="text" tabindex="135" class="wd100 inputText dp" />
                                </td>
                                <td>
                                    Gender
                                </td>
                                <td width="100" align="left" valign="middle">
                                    <asp:RadioButtonList runat="server" TabIndex="136" ID="radAddGender3">
                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:RadioButtonList runat="server" ID="Rad3AddonRelation" TabIndex="137" RepeatColumns="4"
                                        Width="100%" RepeatLayout="Table" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    Occupation
                                </td>
                                <td colspan="1">
                                    <input name="" type="text" tabindex="138" class="wd100 inputText" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="appformSubtitle">
                        <h3>
                            Relative/Reference Name (Mandatory)</h3>
                    </div>
                    <div class="appformrelref">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="30" align="left" valign="top">
                                    Name
                                </td>
                                <td align="left" valign="top" colspan="2">
                                    <input name="REF1_NAME" id="REF1_NAME" maxlength="40" tabindex="139" placeholder="Refrence Name"
                                        runat="server" type="text" class="wd300 inputText onlyAlphabet" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    Address Line 1
                                </td>
                                <td height="30" align="left" valign="middle">
                                    <input runat="server" type="text" name="REF1_ADDRESS1" tabindex="140" placeholder="Address Line 1"
                                        id="REF1_ADDRESS1" maxlength="45" class="wd260 inputText" />
                                </td>
                                <td align="left" valign="top">
                                    Country<span class="red">*</span>
                                </td>
                                <td align="left" valign="middle">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRelcountry" TabIndex="144" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlRelCountry_SelectedIndexChanged" CssClass="myselect wd150">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel6"
                                            DynamicLayout="true">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgLoadingRel" AlternateText="loading" runat="server" ImageUrl="~/images/loading3.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    Address Line 2
                                </td>
                                <td height="30" align="left" valign="middle">
                                    <input runat="server" type="text" name="REF1_ADDRESS2" tabindex="141" placeholder="Address Line 2"
                                        id="REF1_ADDRESS2" maxlength="45" class="wd260 inputText" />
                                </td>
                                <td height="30" align="left" valign="middle">
                                    City<span class="red">*</span>
                                </td>
                                <td align="left" valign="middle">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" tabindex="145" ChildrenAsTriggers="true"
                                            UpdateMode="always">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRelcity" CssClass="myselect wd150">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    Address Line 3
                                </td>
                                <td align="left" valign="middle">
                                    <input name="REF1_ADDRESS3" maxlength="45" tabindex="142" placeholder="Address Line 3"
                                        id="REF1_ADDRESS3" runat="server" type="text" class="wd260 inputText" />
                                </td>
                                <td align="left" valign="middle">
                                    Pincode
                                </td>
                                <td align="left" valign="middle">
                                    <input name="REF1_ZIP_CODE" id="REF1_ZIP_CODE" maxlength="6" tabindex="146" runat="server"
                                        type="text" class="wd150 inputText onlynumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    Address Line 4
                                </td>
                                <td align="left" valign="middle">
                                    <input name="REF1_ADDRESS4" maxlength="45" placeholder="Address Line 4" tabindex="143"
                                        id="REF1_ADDRESS4" runat="server" type="text" class="wd260 inputText" />
                                </td>
                                <td align="left" valign="middle">
                                    Phone Number<span class="red">*</span>
                                </td>
                                <td align="left" valign="top">
                                    <input name="REF1_PHONE_NUMBER" maxlength="20" id="REF1_PHONE_NUMBER" tabindex="147"
                                        runat="server" type="text" class="wd150 inputText onlynumeric" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="appformSubtitle">
                        <h3>
                            Relative/Reference Name (2) (Mandatory)</h3>
                    </div>
                    <div class="appformrelref">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Name
                                </td>
                                <td align="left" valign="top" colspan="2">
                                    <input name="REF2_NAME" id="REF2_NAME" maxlength="40" tabindex="148" placeholder="Refrence Name"
                                        runat="server" type="text" class="wd300 inputText onlyAlphabet" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    Address Line 1
                                </td>
                                <td height="30" align="left" valign="middle">
                                    <input runat="server" type="text" name="REF2_ADDRESS1" tabindex="149" placeholder="Address Line 1"
                                        id="REF2_ADDRESS1" maxlength="45" class="wd260 inputText" />
                                </td>
                                <td align="left" valign="top">
                                    Country
                                </td>
                                <td align="left" valign="middle">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="ddlRel2Country" TabIndex="153" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlRel2Country_SelectedIndexChanged" CssClass="myselect wd150">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel8"
                                            DynamicLayout="true">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgLoadingRel2" AlternateText="loading" runat="server" ImageUrl="~/images/loading3.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    Address Line 2
                                </td>
                                <td height="30" align="left" valign="middle">
                                    <input runat="server" type="text" name="REF2_ADDRESS2" tabindex="150" placeholder="Address Line 2"
                                        id="REF2_ADDRESS2" maxlength="45" class="wd260 inputText" />
                                </td>
                                <td height="30" align="left" valign="middle">
                                    City
                                </td>
                                <td align="left" valign="middle">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" TabIndex="154" ID="ddlrel2city" CssClass="myselect wd150">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    Address Line 3
                                </td>
                                <td align="left" valign="middle">
                                    <input name="REF2_ADDRESS3" maxlength="45" tabindex="151" placeholder="Address Line 3"
                                        id="REF2_ADDRESS3" runat="server" type="text" class="wd260 inputText" />
                                </td>
                                <td align="left" valign="middle">
                                    Pincode
                                </td>
                                <td align="left" valign="middle">
                                    <input name="REF2_ZIP_CODE" id="REF2_ZIP_CODE" maxlength="6" tabindex="155" runat="server"
                                        type="text" class="wd150 inputText onlynumeric" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">
                                    Address Line 4
                                </td>
                                <td align="left" valign="middle">
                                    <input name="REF2_ADDRESS4" maxlength="45" tabindex="152" placeholder="Address Line 4"
                                        id="REF2_ADDRESS4" runat="server" type="text" class="wd260 inputText" />
                                </td>
                                <td align="left" valign="middle">
                                    Phone Number
                                </td>
                                <td align="left" valign="top">
                                    <input name="REF2_PHONE_NUMBER" maxlength="20" id="REF2_PHONE_NUMBER" tabindex="156"
                                        runat="server" type="text" class="wd150 inputText onlynumeric" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Need to find association fields in table -->
                    <div class="appformSubtitle">
                        <h3>
                            Nomination for Primary Applicant</h3>
                    </div>
                    <div class="appformnomini">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="30" align="left" valign="top">
                                    Name
                                </td>
                                <td align="left" valign="top">
                                    <input name="INSURANCE_NOM_NAME" tabindex="157" runat="server" id="INSURANCE_NOM_NAME"
                                        maxlength="40" type="text" class="wd300 inputText" />
                                </td>
                                <td align="left" valign="top">
                                    Relationship
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList runat="server" ID="ddlNomiRelation" TabIndex="158" CssClass="myselect wd150">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="linespacer">
                        &nbsp;</div>
                    <div class="appformstep2">
                        <img src="images/application-form-2nd-step.jpg" alt="" /></div>
                    <div class="appformbutt">
                        <center>
                            <a href="#tab-1" class="orangeBtn"><span>Go to Previous Page</span></a> <a href="#"
                                id="linkResetPage2" name="linkResetPage2" class="orangeBtn"><span>Reset</span></a>
                            <a href="#tab-3" id="tab_3" class="orangeBtn"><span>Proceed to Next Page</span></a>
                            <asp:LinkButton ID="lnkTab_3" OnClientClick="return validateForm2()" Style="display: none;"
                                runat="server">LinkButton</asp:LinkButton>
                        </center>
                    </div>
                </div>
                <!-- Tabbing 3 -->
                <form id="frmPage3">
                <div id="tab-3" class="tabcontent clearfix">
                    <div class="appformstep3">
                        <img src="images/application-form-3rd-step.jpg" alt="" /></div>
                    <div class="appformbutt">
                        <center>
                            <a href="#tab-2" class="orangeBtn"><span>Go to Previous Page</span></a> <a href="#tab-4"
                                id="tab_4" class="orangeBtn"><span>Proceed to Next Page</span></a>
                        </center>
                    </div>
                </div>
                </form>
                <!-- Tabbing 4 -->
                <form id="frmPage4" action="">
                <div id="tab-4" class="tabcontent clearfix" style="padding: 0px 5px;">
                    <div class="appformSubtitle">
                        <h3>
                            (For Auto Debit Faculity)</h3>
                    </div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ml30 mb30"
                        id="Table2">
                        <tr>
                            <td>
                                Do you want Auto Debit Facility
                                <asp:CheckBox runat="server" TabIndex="159" ID="chkautodebit" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ml30 mb30"
                        id="tblautdebit">
                        <tr>
                            <td>
                                Sir,
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Re: Authority to debit my SB/CA a/c againest my Bobcards dues</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="appDesc">
                                    I have applied for Bobcards (Type)
                                    <asp:DropDownList ID="ddlDIRECT_DEBIT_CARDTYPE" TabIndex="1" runat="server" CssClass="myselect">
                                    </asp:DropDownList>
                                    card. I irrevocably authorize Bobcards Ltd. to debit my (SB/CA)
                                    <asp:DropDownList ID="ddlDebitAccountType" TabIndex="161" runat="server" CssClass="myselect wd100">
                                    </asp:DropDownList>
                                    A/c no.
                                    <input type="text" name="DIRECT_DEBIT_ACCOUNT_NUMBER" tabindex="162" runat="server"
                                        maxlength="24" id="DIRECT_DEBIT_ACCOUNT_NUMBER" class="inputText wd100" />
                                    maintained at Bank of Baroda
                                    <asp:DropDownList ID="ddlDebitBranchList" runat="server" TabIndex="163" CssClass="myselect">
                                    </asp:DropDownList>
                                    <%-- <input runat="server" name="DIRECT_DEBIT_BRANCH" id="DIRECT_DEBIT_BRANCH" maxlength="20"
                                        class="inputText wd150" />--%>
                                    Branch as indicated below</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    I, hereby also confirm that I am a authorized signatory of the above stated a/c
                                    & it pertains to me.</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Amount due Minimum amount due Customer Specific
                                <br />
                                <asp:RadioButton ID="rbTotalAmountDue" TabIndex="164" runat="server" Text="Total Amount Due"
                                    GroupName="PaymentType" />
                                <asp:HiddenField ID="hideTotalAmountDue" runat="server" Value="1" />
                                <br />
                                <asp:RadioButton ID="rbMinimumAmountDue" TabIndex="1653" runat="server" Text="Minimum Amount Due"
                                    GroupName="PaymentType" />
                                <asp:HiddenField ID="hideMinimumAmountDue" runat="server" Value="2" />
                                <br />
                                <asp:RadioButton ID="rbPercentage" runat="server" TabIndex="166" Text="Specific % of monthly Due"
                                    GroupName="PaymentType" />
                                <asp:HiddenField ID="hidePercentage" runat="server" Value="3" />
                                <asp:TextBox ID="DIRECT_DEBIT_PERCENTAGE" Width="50" runat="server" TabIndex="167"
                                    CssClass="inputText onlynumeric" MaxLength="3"></asp:TextBox>
                                %
                                <asp:Label ID="cvPaymentType" runat="server" CssClass="error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="100">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            Your faithfully,<br />
                                            <input type="text" id="DIRECT_DEBIT_ACCOUNT_NAME" tabindex="168" maxlength="50" name="DIRECT_DEBIT_ACCOUNT_NAME"
                                                runat="server" class="wd200 inputText" />
                                        </td>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        We Recommend &amp; Verify the above signature<br />
                                                        Bank of Baroda
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Authorized Signatory
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="linespacer">
                    </div>
                    <div class="appformstep4">
                        <img src="images/application-form-4th-step.jpg" alt="" />
                    </div>
                    <!-- POPUP content are here -->
                    <div id="popup_box">
                        <a id="popupBoxClose" class="popClosebtn"></a>
                        <div class="appFormsubmtpop">
                            <center>
                                <h3>
                                    Do you want to Submit?</h3>
                                <%--<a href="#" class="orangeBtn"><span>Yes</span></a>--%>
                                <%--<asp:LinkButton ID="lnkSubmit" runat="server" CssClass="orangeBtn" Text="<span>Yes</span>"
                                    OnClick="lnkSubmit_Click" />--%>
                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="orangeBtn" OnClientClick="return validateFormAll()"
                                    Text="<span>Yes</span>" OnClick="lnkSubmit_Click" />
                                <a href="#" id="btnNo" name="btnNo" class="orangeBtn"><span>No</span></a>
                            </center>
                        </div>
                    </div>
                    <div class="appformbutt">
                        <center>
                            <a href="#tab-3" tabindex="169" class="orangeBtn"><span>Go to Previous Page</span></a>
                            <a href="#" id="btnSubmit" tabindex="170" class="orangeBtn smallpopUp"><span>Submit</span></a>
                        </center>
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="javascript/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="javascript/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            //            $('#chkautodebit').click(function () {
            //                if (!$(this).is(':checked')) {
            //                    $('#DIRECT_DEBIT_ACCOUNT_NUMBER').val('');
            //                    $('#DIRECT_DEBIT_BRANCH').val('');
            //                    $('#DIRECT_DEBIT_ACCOUNT_NAME').val('');
            //                    $('#tblautdebit').find('input[type="text"]').removeClass('input-disabled').prop('disabled', false);
            //                }
            //                else {
            //                    $('#DIRECT_DEBIT_ACCOUNT_NUMBER').val('');
            //                    $('#DIRECT_DEBIT_BRANCH').val('');
            //                    $('#DIRECT_DEBIT_ACCOUNT_NAME').val('');
            //                    $('#tblautdebit').find('input[type="text"]').addClass('input-disabled').prop('disabled', true);
            //                }
            //            });
            if (document.getElementById("chkautodebit").checked == false) {
                $('#tblautdebit').css('display', 'none');
                $('#tblautdebit1').css('display', 'none');

                $('#tblautdebit').find('input').addClass('input-disabled').prop('disabled', true);
                $('#tblautdebit').find('select').addClass('input-disabled').prop('disabled', true);
            }

            if (document.getElementById("rbPercentage").checked == false)
                $("#txtPercentage").attr("disabled", "disabled");

            $('#chkautodebit').click(function () {
                if (!$(this).is(':checked')) {
                    $('#tblautdebit').css('display', 'none');
                    $('#tblautdebit').find('input').addClass('input-disabled').prop('disabled', true);
                    $('#tblautdebit').find('select').addClass('input-disabled').prop('disabled', true);
                    // $("#txtPercentage").attr("disabled", "disabled");
                }
                else {
                    $('#tblautdebit').css('display', '');
                    $('#tblautdebit').find('input').removeClass('input-disabled').prop('disabled', false);
                    $('#tblautdebit').find('select').removeClass('input-disabled').prop('disabled', false);
                }
            });
            $("#tdAcounts").on("change", "input[type='checkbox']", function () {
                var group = ":checkbox[title='" + $(this).attr("title") + "']";
                if ($(this).is(':checked')) {
                    $(group).not($(this)).attr("checked", false);
                }
            });

            $("#tblCards,#tblOwnedVehicle,#tblEmployeeStatus,#tblProfession, #tblDesignation").on("change", "input[type='checkbox']", function () {

                var currCheckbox = $(this);
                var targettbl = $(currCheckbox).closest("table");

                if ($(currCheckbox).prop("checked")) {
                    $(targettbl).find("input[type='checkbox']:checked").prop("checked", false);
                    $(currCheckbox).prop("checked", true);
                    if ($(targettbl).prop("id") == "tblCards") {
                        $("#hdnselectedCard").val($(currCheckbox).val());
                    }
                }
                else {
                    if ($(targettbl).prop("id") == "tblCards") {
                        $("#hdnselectedCard").val("");
                    }

                }
            });

            $('#ddlCardType').change(function () {
                alert($("#ddlDIRECT_DEBIT_CARDTYPE").val());
            })

            //To Calculate Age from date picker

            var maxdate = new Date();

            $(".ClcDate").datepicker({
                flat: true,
                constrainInput: true,
                showOn: 'both',
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                maxDate: maxdate,
                yearRange: "-113:+0",
                onSelect: function (date) {
                    var day = date.split("/")[0];

                    var month = date.split("/")[1];
                    var year = date.split("/")[2];
                    var dt1 = new Date(year, month - 1, day);
                    var past = new Date(dt1);
                    var now = new Date();
                    var nowYear = now.getFullYear();
                    var pastYear = past.getFullYear();
                    var age = nowYear - pastYear;
                    //return age;

                    $('#AGE').val(age);
                }
            });

            $(".dp").datepicker({
                flat: true,
                constrainInput: true,
                showOn: 'both',
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                maxDate: maxdate,
                yearRange: "-113:+0"
            });

            $(".blurEmboos").blur(function () {
                var EmbossedName = $("#FIRST_NAME").val();
                if ($("#MIDDLE_NAME").val() != null && $("#MIDDLE_NAME").val() != "") {
                    EmbossedName = EmbossedName + ' ' + $("#MIDDLE_NAME").val() + ' ' + $("#FAMILY_NAME").val();
                }
                else {
                    EmbossedName = EmbossedName + ' ' + $("#FAMILY_NAME").val();
                }
                $('#EMBOSSED_NAME').val(EmbossedName.substr(0, 20));
            });

            $('#tabs .tabcontent').hide();
            $('#tabs div.tabcontent:first').show();
            $('#tabs ul li:first').addClass('active');
            $('#tabs ul li a').click(function () {
                $('#tabs ul li').removeClass('active');
                $(this).parent().addClass('active');
                var currentTab = $(this).attr('href');
                $('#tabs .tabcontent').hide();
                $(currentTab).show();
                return false;
            });

            $("a[href^='#tab']").click(function () {
                $('#tabs ul li').removeClass('active');
                var currentTab = $(this).attr('href');
                $("#tabs ul li a[href='" + currentTab + "']").parent().addClass('active');
                $('#tabs .tabcontent').hide();
                $(currentTab).show();
                $("html, body").animate({ scrollTop: 0 }, "slow");
                return false;
            });

            $("a[id='linkResetPage1']").click(function () {
                $('#tab-1 input[type="text"]').val('');
                $('#tab-1 input[type="checkbox"]').removeAttr('checked');
                $('#tab-1 input[type="radio"]').removeAttr('checked');
                $('#tab-1 select option:selected').removeAttr('selected');
                $("html, body").animate({ scrollTop: 0 }, "slow");
                return false;
            });
            $("a[id='linkResetPage2']").click(function () {
                $('#tab-2 input[type="text"]').val('');
                $('#tab-2 input[type="checkbox"]').removeAttr('checked');
                $('#tab-2 input[type="radio"]').removeAttr('checked');
                $('#tab-2 select option:selected').removeAttr('selected');
                $("html, body").animate({ scrollTop: 0 }, "slow");
                return false;
            });


            $('.smallpopUp').click(function () {
                loadPopupBox();
            });

            $('#popupBoxClose').click(function () {
                unloadPopupBox();
            });
            $('#btnNo').click(function () {
                unloadPopupBox();
            });

            function unloadPopupBox() {
                $('#popup_box').fadeOut("slow");
            }

            function loadPopupBox() {
                $('#popup_box').fadeIn("slow");
            }


            $('#tab_2').click(function () {
                $('#lnkTab_2').click();
            });

            $('#tab2').click(function () {
                $('#lnkTab_2').click();
            });


            $('#tab3').click(function () {
                $('#lnkTab_3').click();
            });

            $('#tab_3').click(function () {
                $('#lnkTab_3').click();
            });

            $('#tab_4').click(function () {
                if (validateForm() != false) {
                    validateForm2();
                }
            });

            $('#tab4').click(function () {
                //validateForm();
                $('#ddlDIRECT_DEBIT_CARDTYPE').val($('#ddlCountry').val());
                if (validateForm() != false) {
                    validateForm2();
                }
            });

            $('#chkTickPermanentAddress').click(function () {
                if (!$(this).is(':checked')) {
                    $('#PERM_ADDRESS1').val('');
                    $('#PERM_ADDRESS2').val('');
                    $('#PERM_ADDRESS3').val('');
                    $('#PERM_ADDRESS4').val('');
                    $('#PERM_POSTAL_CODE').val('');
                    $('#ddlPermCity').val('-1');
                    $('#ddlPermCountry').val('-1');
                    $('#PERM_EXT').val('');
                    $('#PERM_TELEPHONE_NO').val('');
                    $('#tblPermanentAddress').find('input[type="text"]').removeClass('input-disabled').prop('disabled', false);
                    $('#tblrightPermAddress').find('input[type="text"]').removeClass('input-disabled').prop('disabled', false);
                    $('#ddlPermCity').removeClass('input-disabled').prop('disabled', false);
                    $('#ddlPermCountry').removeClass('input-disabled').prop('disabled', false);


                } else {
                    BindCity($('#ddlCurrCountry').val());
                    $('#PERM_ADDRESS1').val($('#CURR_ADDRESS1').val());
                    $('#PERM_ADDRESS2').val($('#CURR_ADDRESS2').val());
                    $('#PERM_ADDRESS3').val($('#CURR_ADDRESS3').val());
                    $('#PERM_ADDRESS4').val($('#CURR_ADDRESS4').val());
                    $('#PERM_POSTAL_CODE').val($('#CURR_POSTAL_CODE').val());
                    $('#ddlPermCity').val($('#ddlCurrCity').val());
                    $('#ddlPermCountry').val($('#ddlCurrCountry').val());
                    $('#PERM_EXT').val($('#EXT').val());
                    $('#PERM_TELEPHONE_NO').val($('#HOME_PHONE_NUMBER').val());

                    $('#tblPermanentAddress').find('input[type="text"]').addClass('input-disabled').prop('disabled', true);
                    $('#tblrightPermAddress').find('input[type="text"]').addClass('input-disabled').prop('disabled', true);
                    $('#ddlPermCity').addClass('input-disabled').prop('disabled', true);
                    $('#ddlPermCountry').addClass('input-disabled').prop('disabled', true);
                    $('#PASSPORT_NUMBER,#DRIVING_LICENSE_NUMBER').removeClass('input-disabled').prop('disabled', false);

                }
            });


            function BindCity(Country) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Application.aspx/ddlPermCityRecord",
                    data: "{CountryValue : '" + Country + "'}",
                    dataType: "json",
                    success: function (data) {
                        var jsdata = JSON.parse(data.d);
                        $("#ddlPermCity").append($("<option></option>").val("-1").html("Select City"));
                        $.each(jsdata, function (key, value) {
                            $("#ddlPermCity").append($("<option></option>").val(value.CITY_CODE).html(value.CITY_NAME));
                        });
                        $('#ddlPermCity').val($('#ddlCurrCity').val());
                    },
                    error: function (result) {
                        //                        alert("Error");
                    }
                });
            }



            $("#ddlAccountBranch").attr("disabled", "disabled");
            $("#BANK_BRANCH_ADDRESS").attr("disabled", "disabled");
            $("#OTHER_CITY").attr("disabled", "disabled");
            $("#OUR_ACCOUNT_TENURE").attr("disabled", "disabled");
            $("#saving_acc").attr("disabled", "disabled");
            $("#other_acc").attr("disabled", "disabled");
            $("#current_acc").attr("disabled", "disabled");

            $("#ddlIsAccountWithbank").change(function () {
                var sval = $('option:selected', this).text();
                if (sval == "Yes") {
                    $("#ddlAccountBranch").removeAttr("disabled");
                    $("#BANK_BRANCH_ADDRESS").removeAttr("disabled");
                    $("#OTHER_CITY").removeAttr("disabled");
                    $("#OUR_ACCOUNT_TENURE").removeAttr("disabled");
                    $("#saving_acc").removeAttr("disabled");
                    $("#other_acc").removeAttr("disabled");
                    $("#current_acc").removeAttr("disabled");
                }
                else {
                    $("#ddlAccountBranch").attr("disabled", "disabled");
                    $("#BANK_BRANCH_ADDRESS").attr("disabled", "disabled");
                    $("#OTHER_CITY").attr("disabled", "disabled");
                    $("#OUR_ACCOUNT_TENURE").attr("disabled", "disabled");
                    $("#saving_acc").attr("disabled", "disabled");
                    $("#other_acc").attr("disabled", "disabled");
                    $("#current_acc").attr("disabled", "disabled");
                    $('#ddlAccountBranch').val('-1');
                    $("#BANK_BRANCH_ADDRESS").val('');
                    $("#OTHER_CITY").val('');
                    $("#OUR_ACCOUNT_TENURE").val('');
                    $("#saving_acc").prop("checked", false); ;
                    $("#other_acc").checked(false);
                    $("#current_acc").checked(false);
                }
            });


            $('.onlyDecimalnumeric').live("keyup", function () { inputControl($(this), 'float'); });

            /// Allow float value
            function inputControl(input, format) {
                var value = input.val();
                var values = value.split("");
                var update = "";
                var transition = "";
                if (format == 'int') {
                    expression = /^([0-9])$/;
                    finalExpression = /^([1-9][0-9]*)$/;
                }
                else if (format == 'float') {
                    var expression = /(^\d+$)|(^\d+\.\d+$)|[,\.]/;
                    var finalExpression = /^([1-9][0-9]*[,\.]?\d{0,3})$/;
                }
                for (id in values) {
                    if (expression.test(values[id]) == true && values[id] != '') {
                        transition += '' + values[id].replace(',', '.');
                        if (finalExpression.test(transition) == true) {
                            update += '' + values[id].replace(',', '.');
                        }
                    }
                }
                input.val(update);
            }


            ///--- First name
            $(".onlyAlphabet").keypress(function (key) {
                if (key.charCode == 32 || key.keyCode == 46 || key.keyCode == 8 || key.keyCode == 9 || key.keyCode == 27 || key.keyCode == 13 || (key.keyCode == 65 && key.ctrlKey === true) || (key.keyCode >= 35 && key.keyCode <= 39)) {
                    return;
                } else {
                    if ((key.charCode < 97 || key.charCode > 122) && (key.charCode < 65 || key.charCode > 90) && (key.charCode != 45)) {
                        return false;
                    }
                }
            });


            ///--- Age
            $(".onlynumeric").keydown(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });
        });


        //Validate PAN

        function ValidatePAN(Obj) {
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj.value != "") {
                ObjVal = Obj.value;
                var PanNum = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                var Forthcode = /([C,c,P,p,H,h,F,f,A,a,T,t,B,b,L,l,J,j,G,g])/;
                var Pan_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(PanNum) == -1) {
                    alert("Invalid Pan No");
                    Obj.focus();
                    Obj.value = "";
                    return false;
                }
                if (Forthcode.test(Pan_chk) == false) {
                    alert("Invaild PAN Card No.");
                    Obj.value = "";
                    return false;
                }
            }
        }

        function validateForm2() {
            if (dateValidation(document.getElementById("SEC_BIRTH_DATE")) == false && document.getElementById("SEC_BIRTH_DATE").value != "") {
                alert("Please enter correct Birth Date");
                document.getElementById("btnNo").click();
                document.getElementById("tab2").click();
                document.getElementById("SEC_BIRTH_DATE").focus();
                return false;
            }
            else if (dateValidation(document.getElementById("SEC2_BIRTH_DATE")) == false && document.getElementById("SEC2_BIRTH_DATE").value != "") {
                alert("Please enter correct Birth Date");
                document.getElementById("btnNo").click();
                document.getElementById("tab2").click();
                document.getElementById("SEC2_BIRTH_DATE").focus();
                return false;
            }

            else if (document.getElementById('ddlRelcountry').value == "-1") {
                alert("Please Select Reference1 Country");
                document.getElementById("btnNo").click();
                document.getElementById("tab2").click();
                document.getElementById("ddlRelcountry").focus();
                return false;
            }
            else if (document.getElementById('ddlRelcity').value == "-1") {
                alert("Please Select Reference1 City");
                document.getElementById("btnNo").click();
                document.getElementById("tab2").click();
                document.getElementById("ddlRelcity").focus();
                return false;
            }
            else if (document.getElementById("REF1_PHONE_NUMBER").value == null || document.getElementById("REF1_PHONE_NUMBER").value == "") {
                alert("Please Enter Reference1 Phone Number");
                document.getElementById("btnNo").click();
                document.getElementById("tab2").click();
                document.getElementById("REF1_PHONE_NUMBER").focus();
                return false;
            }

        }

        function ValidationForm3() {
            if (CheckPercentage() == false) {
                alert("Please Enter Percentage");
                document.getElementById("btnNo").click();
                document.getElementById("tab4").click();
                document.getElementById("txtPercentage").focus();
                return false;
            }
            else if (checkPercentageValue(document.getElementById("txtPercentage").value) == false) {
                alert("Percentage must be less than 100");
                document.getElementById("btnNo").click();
                document.getElementById("tab4").click();
                document.getElementById("txtPercentage").focus();
                return false;
            }
        }


        function validateFormAll() {
            if (document.getElementById('chkautodebit').checked) {
                if (document.getElementById("DIRECT_DEBIT_ACCOUNT_NAME").value == ''
                   || document.getElementById("DIRECT_DEBIT_ACCOUNT_NUMBER").value == ''
                   || document.getElementById("ddlDebitAccountType").value == "-1"
                   || document.getElementById("ddlDebitBranchList").value == "-1") {

                    $('#popup_box').hide();
                    alert('Please fill Debit account details.');
                    document.getElementById("ddlDebitBranchList").focus();
                    return false;
                }
            }
            validateForm();
            validateForm2();
            ValidationForm3();
            if (validateForm() == false) {
                return false;
            }
            else if (validateForm2() == false) {
                return false;
            }
            else if (ValidationForm3() == false) {
                return false;
            }
        }

        function validateForm() {
            var x = document.getElementById("FIRST_NAME").value;
            if (document.getElementById('ddlCardType').value == "-1") {
                alert("Please Select Card Type");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlCardType").focus();
                return false;
            }
            else if (x == null || x == "") {
                alert("Please Enter First Name");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("FIRST_NAME").focus();
                return false;
            }
            else if (document.getElementById('ddlBranchlist').value == "-1") {
                alert("Please Select Branch");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlBranchlist").focus();
                return false;
            }
            else if (document.getElementById("FAMILY_NAME").value == null || document.getElementById("FAMILY_NAME").value == "") {
                alert("Please Enter Family Name");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("FAMILY_NAME").focus();
                return false;
            }
            else if (RadioGender() == false) {
                alert("Please Select Gender");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                return false;
            }
            else if (document.getElementById("EMBOSSED_NAME").value == null || document.getElementById("EMBOSSED_NAME").value == "") {
                alert("Please Enter Embossed Name");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("EMBOSSED_NAME").focus();
                return false;
            }
            else if (document.getElementById("BIRTH_DATE").value == null || document.getElementById("BIRTH_DATE").value == "") {
                alert("Please Enter Birth Date");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("BIRTH_DATE").focus();
                return false;
            }

            else if (dateValidation(document.getElementById("BIRTH_DATE")) == false) {
                alert("Please Enter correct Birth Date");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("BIRTH_DATE").focus();
                return false;
            }


            else if (document.getElementById("AGE").value == null || document.getElementById("AGE").value == "") {
                alert("Please Enter Age");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("BIRTH_DATE").focus();
                return false;
            }
            else if (ValidateAge(document.getElementById("AGE").value) == false) {
                alert("Age must be above the 18 or older");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("BIRTH_DATE").focus();
                return false;
            }
            else if (document.getElementById("MOBILE_NUMBER").value == null || document.getElementById("MOBILE_NUMBER").value == "") {
                alert("Please Enter Mobile Number");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("MOBILE_NUMBER").focus();
                return false;
            }
            else if (!validateEmail(document.getElementById("EMAIL_ID").value)) {
                alert("Please Enter Valid Email Id");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("EMAIL_ID").focus();
                return false;
            }
            else if (document.getElementById('ddlMaritalStatus').value == "-1") {
                alert("Please Select Marital Status");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlMaritalStatus").focus();
                return false;
            }

            else if (document.getElementById('ddlEducation').value == "-1") {
                alert("Please Select Education");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlEducation").focus();
                return false;
            }
            else if (document.getElementById("CURR_ADDRESS1").value == null || document.getElementById("CURR_ADDRESS1").value == "") {
                alert("Please Enter Current Address1");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("CURR_ADDRESS1").focus();
                return false;
            }
            else if (document.getElementById('ddlCurrCountry').value == "-1") {
                alert("Please Select Current Countr");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlCurrCountry").focus();
                return false;
            }
            else if (document.getElementById('ddlCurrCity').value == "-1") {
                alert("Please Select Current City");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlCurrCity").focus();
                return false;
            }
            else if (document.getElementById("HOME_PHONE_NUMBER").value == null || document.getElementById("HOME_PHONE_NUMBER").value == "") {
                alert("Please Enter Home Phone Number");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("HOME_PHONE_NUMBER").focus();
                return false;
            }
            else if (radResiSatus() == false) {
                alert("Please Select Residential Status");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                return false;
            }
            else if (document.getElementById("PERM_ADDRESS1").value == null || document.getElementById("PERM_ADDRESS1").value == "") {
                alert("Please Enter Permanent Address1");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("PERM_ADDRESS1").focus();
                return false;
            }
            else if (document.getElementById('ddlPermCountry').value == "-1") {
                alert("Please Select Permenent Country");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlPermCountry").focus();
                return false;
            }
            else if (document.getElementById('ddlPermCity').value == "-1") {
                alert("Please Select Permenent City");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlPermCity").focus();
                return false;
            }
            else if (document.getElementById('ddlVIPCode').value == "-1") {
                alert("Please Select VIP Code");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlVIPCode").focus();
                return false;
            }
            else if (document.getElementById('ddlSocialStatus').value == "-1") {
                alert("Please Select Social Status");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlSocialStatus").focus();
                return false;
            }
            else if (chkEmpProfession() == false) {
                alert("Please Select Your Profession");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                return false;
            }
            else if (document.getElementById("EMPL_ADDRESS1").value == null || document.getElementById("EMPL_ADDRESS1").value == "") {
                alert("Please Enter Office Address1");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("EMPL_ADDRESS1").focus();
                return false;
            }
            else if (document.getElementById('ddlEmpCountry').value == "-1") {
                alert("Please Select Employee Country");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlEmpCountry").focus();
                return false;
            }
            else if (document.getElementById('ddlEmpCity').value == "-1") {
                alert("Please Select Employee City");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlEmpCity").focus();
                return false;
            }
            else if (document.getElementById("PAN_GIR_NO").value == null || document.getElementById("PAN_GIR_NO").value == "") {
                alert("Please Enter Pan Card No");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("PAN_GIR_NO").focus();
                return false;
            }
            else if (document.getElementById("EMPL_POSTAL_CODE").value == null || document.getElementById("EMPL_POSTAL_CODE").value == "") {
                alert("Please Enter Employee Postl Code");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("EMPL_POSTAL_CODE").focus();
                return false;
            }
            else if (document.getElementById("CURRENT_JOB_TENURE").value == null || document.getElementById("CURRENT_JOB_TENURE").value == "") {
                alert("Please Enter Current Job Month and Year");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("CURRENT_JOB_TENURE").focus();
                return false;
            }
            else if (ValidateYearAndMonth() == false) {
                alert("Month Value must be between 0 & 11");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("JOB_MONTHS").focus();
                return false;
            }
            else if (document.getElementById("ANNUAL_INCOME_CODE").value == null || document.getElementById("ANNUAL_INCOME_CODE").value == "") {
                alert("Please Enter Annual Income");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ANNUAL_INCOME_CODE").focus();
                return false;
            }

            else if (document.getElementById('ddlempType').value == "-1") {
                alert("Please Select Employer Type");
                document.getElementById("btnNo").click();
                document.getElementById("tab1").click();
                document.getElementById("ddlempType").focus();
                return false;
            }

        }

        function validateEmail(sEmail) {
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (sEmail == null || sEmail == "") {
                return true;
            }
            else if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateYearAndMonth() {
            var x = document.getElementById("JOB_MONTHS").value;
            if (x == null || x == "" || x < 0 || x > 11) {
                return false;
            }
        }

        function CheckPercentage() {
            if (document.getElementById("rbPercentage").checked) {
                if (document.getElementById("txtPercentage").value == null || document.getElementById("txtPercentage").value == "" || document.getElementById("txtPercentage").value >= 100) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                false;
            }
        }

        function radResiSatus() {
            var radio = document.getElementsByName("radResiSatus");
            var counter = 0;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                return true;
            }
            else {
                return false;
            }

        }

        function checkPercentageValue(Per) {
            if (document.getElementById("rbPercentage").checked) {
                if (Per >= 100) {
                    return false;
                }
                else {
                    true;
                }
            }
            else {
                true;
            }
        }

        function ValidateAge(age) {
            if (age < 18) {
                return false;
            }
            else {
                return true;
            }
        }

        function chkEmpProfession() {
            var oOS = document.getElementById("chkEmpProfession");
            var chkList = oOS.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < chkList.length; i++) {
                if (chkList[i].checked) {
                    counter++;
                }
            }
            if (counter > 0) {
                return true;
            }
            else {
                return false;
            }
        }
        function RadioGender() {
            if (document.getElementById("MALE").checked == true) {
                return true;
            }
            else if (document.getElementById("FEMALE").checked == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function dateValidation(obj) {
            if (obj.value == null || obj.value == "") {
                return false;
            }
            var day = obj.value.split("/")[0];

            var month = obj.value.split("/")[1];
            var year = obj.value.split("/")[2];

            if ((day < 1 || day > 31) || (month < 1 || month > 12) || (year.length != 4)) {
                return false;
            }
            else {

                var dt = new Date(year, month - 1, day);
                var today = new Date();

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {
                    return false;
                }
                else {
                    return true;
                }

            }
        }

       
    </script>
</body>
</html>
