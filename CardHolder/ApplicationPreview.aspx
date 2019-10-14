<%@ Page Language="C#" Title="ApplicationPreview" AutoEventWireup="true" CodeBehind="ApplicationPreview.aspx.cs"
    Inherits="CardHolder.ApplicationPreview" %>

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
</head>
<body>
    <form id="MasterPage" runat="server" method="post">
    <asp:Label runat="server" ID="lblempty"></asp:Label>
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
        <div style="text-align: center;">
            <asp:Button runat="server" ID="btnPrint" CssClass="button" Text="Print" OnClientClick="javascript:window.print();"
                Visible="false" />
            <asp:Button runat="server" ID="btnCloseWindow" CssClass="button" Text="Close window"
                OnClientClick="javascript:window.close();" Visible="false" />
        </div>
        <div class="appformContent">
            <div id="tab-1" class="tabcontent clearfix">
                <!-- Application Form 1st step -->
                <div class="appformLeft">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="45" align="left" valign="top">
                                You Wished to Applied for BOBCARD
                            </td>
                            <td align="left" valign="top">
                                <asp:HiddenField ID="hdnId" runat="server" Value="" />
                                <%--<table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblCards">
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
                                </table>--%>
                                <asp:Label runat="server" ID="lblAppliedCard"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="45" align="left" valign="top">
                                Application Type
                            </td>
                            <td align="left" valign="top">
                                <span class="right">
                                    <asp:DropDownList ID="ddlApplicationType" TabIndex="1" runat="server" CssClass="myselect">
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
                                    <asp:DropDownList ID="ddlPromoCode" TabIndex="2" runat="server" CssClass="myselect">
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
                                            <asp:CheckBox runat="server" ID="chkOffice" Text="       Office" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:CheckBox runat="server" ID="chkResi" Text="       Residence" />
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
                                    <asp:Label runat="server" ID="lblStaff_EC_No"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" align="left" valign="middle">
                                    Name
                                </td>
                                <td align="left" valign="middle">
                                    <asp:Label runat="server" ID="lblSTAFF_NAME"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" align="left" valign="middle">
                                    Branch
                                </td>
                                <td align="left" valign="middle">
                                    <asp:Label runat="server" ID="lblSelbranch"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlBranchlist" Style="width: 90px" CssClass="myselect">
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
                                    <asp:Label runat="server" ID="lblRecommendedBy"></asp:Label>
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
                                    <asp:Label runat="server" ID="lblRecommendedBranch"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlRecommendedBranch" Style="width: 90px" CssClass="myselect">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="55%" height="20" align="left" valign="middle">
                                    Ref No.
                                </td>
                                <td align="left" valign="middle" width="45%">
                                    <asp:Label runat="server" ID="lblSOURCE_APPLICATION_NO"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    UID
                                </td>
                                <td height="30" align="left" valign="middle" colspan="3">
                                    <asp:Label runat="server" ID="lblVC_ALIAS_NAME"></asp:Label>
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
                        <%-- <table width="83%" border="0" cellspacing="0" cellpadding="0" class="fRight mt10">
                            <tr>
                                <td align="left" valign="middle" height="25" width="80">
                                    Ref No.
                                </td>
                                <td align="left" valign="middle" height="25">
                                    <input name="SOURCE_APPLICATION_NO" id="SOURCE_APPLICATION_NO" runat="server" type="text"
                                        class="inputText wd200" />
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
                                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="myselect wd50">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblFullname"></asp:Label>
                                            </td>
                                            <%-- <td>
                                                <input name="MIDDLE_NAME" runat="server" id="MIDDLE_NAME" placeholder="Middle Name"
                                                    maxlength="26" type="text" class="inputText wd126 onlyAlphabet required" />
                                            </td>
                                            <td>
                                                <input name="FAMILY_NAME" runat="server" id="FAMILY_NAME" placeholder="Family Name"
                                                    maxlength="20" type="text" class="inputText wd126 onlyAlphabet required" />
                                            </td>--%>
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
                                    Gender
                                </td>
                                <td>
                                    <label>
                                        <input type="radio" name="GENDER" runat="server" id="MALE" value="0" />
                                        Male</label>
                                    <label>
                                        <input type="radio" name="GENDER" runat="server" id="FEMALE" value="1" />
                                        Female</label>
                                </td>
                            </tr>
                            <tr class="rowLine">
                                <td height="35">
                                    Date of Birth
                                </td>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <input name="BIRTH_DATE" id="BIRTH_DATE" runat="server" type="text" class="inputText wd100" />
                                            </td>
                                            <td>
                                                Age
                                                <input name="AGE" id="AGE" runat="server" readonly="readonly" type="text" maxlength="2"
                                                    class="inputText wd30 onlynumeric" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="rowLine">
                                <td height="35">
                                    Marital Status
                                </td>
                                <td>
                                    <%-- <label>
                                            <input type="radio" name="MARITAL_STATUS" id="SINGLE" runat="server" value="0" />
                                            Single</label>
                                        <label>
                                            <input type="radio" name="MARITAL_STATUS" id="MARRIED" runat="server" value="1" />
                                            Married</label>--%>
                                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="myselect wd150">
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
                                                <asp:Label runat="server" ID="lblMarriageDay"></asp:Label>
                                            </td>
                                            <td>
                                                <span class="hints">ddmm Format</span>
                                            </td>
                                            <td>
                                                No of Dependents
                                            </td>
                                            <td>
                                                <input name="NO_OF_DEPENDENTS" id="NO_OF_DEPENDENTS" runat="server" type="text" maxlength="2"
                                                    class="inputText wd30 onlynumeric" />
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
                                        <input type="radio" name="NATIONALITY" value="356" id="Residentindian" runat="server" />
                                        Indian</label>
                                    <label>
                                        <input type="radio" name="NATIONALITY" value="000" id="NonResidentIndian" runat="server" />
                                        Non Resident Indian</label>
                                </td>
                            </tr>
                        </table>
                        <div class="linespacer">
                        </div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td height="30" align="left" valign="middle" style="width: 135px">
                                    Present Address 1
                                </td>
                                <td height="30" align="left" valign="middle" colspan="3">
                                    <%--<textarea name="CURR_ADDRESS" id="CURR_ADDRESS" runat="server" class="wd300 ht50 textArea"></textarea>--%>
                                    <input runat="server" type="text" name="CURR_ADDRESS1" id="CURR_ADDRESS1" maxlength="45"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Present Address 2
                                </td>
                                <td align="left" valign="middle">
                                    <input name="CURR_ADDRESS2" maxlength="45" id="CURR_ADDRESS2" runat="server" type="text"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Present Address 3
                                </td>
                                <td align="left" valign="middle">
                                    <input name="CURR_ADDRESS3" maxlength="45" id="CURR_ADDRESS3" runat="server" type="text"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Present Address 4
                                </td>
                                <td align="left" valign="middle">
                                    <input name="CURR_ADDRESS4" maxlength="45" id="CURR_ADDRESS4" runat="server" type="text"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Changed your Address?
                                </td>
                                <td style="height: 29px">
                                    <asp:DropDownList runat="server" ID="ddlchangeResi" CssClass="myselect wd200">
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
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mt10 .ml44">
                            <tr>
                                <td height="35" align="right" valign="top">
                                    <div style="text-align: right; float: right; padding-right: 10px; padding-top: 5px;">
                                        Your name as you would like to have on card</div>
                                </td>
                                <td height="35" align="left" valign="top">
                                    <input name="EMBOSSED_NAME" maxlength="26" id="EMBOSSED_NAME" runat="server" type="text"
                                        class="inputText wd200 onlyAlphabet" />
                                </td>
                            </tr>
                        </table>
                        <div class="linespacer">
                        </div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="eduBlock">
                            <tr>
                                <td height="22">
                                    Education Qualification
                                </td>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlEducation" CssClass="myselect wd150">
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
                                    <input name="UNIVERSITY" maxlength="25" runat="server" id="UNIVERSITY" type="text"
                                        class="inputText wd300 onlyAlphabet" />
                                </td>
                            </tr>
                            <tr>
                                <td height="22">
                                    Father's Name
                                </td>
                                <td>
                                    <input name="FATHER_NAME" maxlength="25" runat="server" id="FATHER_NAME" type="text"
                                        class="inputText wd300 onlyAlphabet" />
                                </td>
                            </tr>
                            <tr>
                                <td height="22">
                                    Mother's Maiden Name
                                </td>
                                <td>
                                    <input name="MAIDEN_NAME" maxlength="25" runat="server" id="MAIDEN_NAME" type="text"
                                        class="inputText wd300 onlyAlphabet" />
                                </td>
                            </tr>
                            <tr>
                                <td height="22">
                                    Spouse Name
                                </td>
                                <td>
                                    <input maxlength="40" name="SPOUSE_NAME" runat="server" id="SPOUSE_NAME" type="text"
                                        class="inputText wd300 onlyAlphabet" />
                                </td>
                            </tr>
                            <tr>
                                <td height="22">
                                    Mobile no. Of Spouse
                                </td>
                                <td>
                                    <input name="SPOUSE_MOB_NO" maxlength="12" runat="server" id="SPOUSE_MOB_NO" type="text"
                                        class="inputText wd200 onlynumeric" />
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
                                                <asp:CheckBoxList runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"
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
                                                Country
                                            </td>
                                            <td align="left" valign="middle">
                                                <div>
                                                    <asp:UpdatePanel ID="UpCurrCountry" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlCurrCountry" CssClass="myselect wd150">
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
                                                City
                                            </td>
                                            <td align="left" valign="middle">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlCurrCity" CssClass="myselect wd150">
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
                                                <input name="CURR_POSTAL_CODE" maxlength="10" id="CURR_POSTAL_CODE" runat="server"
                                                    type="text" class="wd200 inputText onlynumeric" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                Mobile
                                            </td>
                                            <td align="left" valign="middle">
                                                <input runat="server" maxlength="20" name="MOBILE_NUMBER" id="MOBILE_NUMBER" type="text"
                                                    class="wd200 inputText onlynumeric" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                Email Id
                                            </td>
                                            <td align="left" valign="middle">
                                                <input maxlength="50" runat="server" name="EMAIL_ID" id="EMAIL_ID" type="text" class="wd200 inputText" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                Resi No.
                                            </td>
                                            <td align="left" valign="middle">
                                                <%-- <input name="EXT" id="EXT" maxlength="5" runat="server" type="text" class="wd30 inputText onlynumeric" />--%>
                                                <input name="HOME_PHONE_NUMBER" maxlength="15" id="HOME_PHONE_NUMBER" runat="server"
                                                    type="text" class="wd150 inputText ml10 onlynumeric" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 3px">
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Resi. Status
                                </td>
                                <td align="left" valign="middle">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblResiStatus">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="radResiSatus" RepeatColumns="5" Width="100%" RepeatLayout="Table"
                                                    RepeatDirection="Horizontal" runat="server">
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
                            <input name="chkTickPermanentAddress" id="chkTickPermanentAddress" runat="server"
                                type="checkbox" value="" />
                            Tick here if Permanent address is same as above
                        </div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblPermanentAddress"
                            class="mt10">
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Permanent Address 1
                                </td>
                                <td height="30" align="left" valign="middle" colspan="3">
                                    <input runat="server" type="text" name="PERM_ADDRESS1" id="PERM_ADDRESS1" maxlength="45"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Permanent Address 2
                                </td>
                                <td align="left" valign="middle">
                                    <input name="PERM_ADDRESS2" maxlength="45" id="PERM_ADDRESS2" runat="server" type="text"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Permanent Address 3
                                </td>
                                <td align="left" valign="middle">
                                    <input name="PERM_ADDRESS3" maxlength="45" id="PERM_ADDRESS3" runat="server" type="text"
                                        class="wd260 inputText" />
                                </td>
                            </tr>
                            <tr>
                                <td height="30" align="left" valign="middle">
                                    Permanent Address 4
                                </td>
                                <td align="left" valign="middle">
                                    <input name="PERM_ADDRESS4" maxlength="45" id="PERM_ADDRESS4" runat="server" type="text"
                                        class="wd260 inputText" />
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
                                                Country
                                            </td>
                                            <td align="left" valign="middle">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                        <ContentTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlPermCountry" CssClass="myselect wd150">
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
                                                City
                                            </td>
                                            <td align="left" valign="middle">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
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
                                                <input name="PERM_POSTAL_CODE" maxlength="10" id="PERM_POSTAL_CODE" runat="server"
                                                    type="text" class="inputText wd150 onlynumeric" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                Passport Number
                                            </td>
                                            <td align="left" valign="middle">
                                                <input name="PASSPORT_NUMBER" maxlength="15" id="PASSPORT_NUMBER" runat="server"
                                                    type="text" class="inputText wd150" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                VIP Code
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlVIPCode" CssClass="myselect wd150">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                Social Status
                                            </td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList runat="server" ID="ddlSocialStatus" CssClass="myselect wd150">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                Resi No.
                                            </td>
                                            <td align="left" valign="middle">
                                                <!-- Need to find associate fields in table -->
                                                <input name="PERM_EXT" id="PERM_EXT" maxlength="3" type="text" class="inputText wd30 onlynumeric" />
                                                <input name="PERM_TELEPHONE_NO" maxlength="15" id="PERM_TELEPHONE_NO" type="text"
                                                    class="inputText wd134 ml10 onlynumeric" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="left" valign="middle">
                                                D/L Number
                                            </td>
                                            <td align="left" valign="middle">
                                                <input name="DRIVING_LICENSE_NUMBER" maxlength="20" placeholder="Driving License Number"
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
                                            <asp:CheckBoxList runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"
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
                                <input name="EMPLOYER_NAME" maxlength="40" id="EMPLOYER_NAME" runat="server" type="text"
                                    class="inputText wd300" />
                            </td>
                            <td align="left" valign="middle" height="30">
                                Employer Type
                            </td>
                            <td align="left" valign="middle" height="30">
                                <asp:DropDownList runat="server" ID="ddlempType" CssClass="myselect wd150">
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
                                            <asp:CheckBoxList runat="server" ID="chkdesignation" Width="100%" RepeatColumns="5"
                                                RepeatDirection="Horizontal" RepeatLayout="Table">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" height="30" class="pt10">
                                Your Profession
                            </td>
                            <td align="left" valign="middle" height="30" colspan="3" class="pt10">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblProfession">
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList runat="server" ID="chkEmpProfession" Width="100%" RepeatColumns="5"
                                                RepeatDirection="Horizontal" RepeatLayout="Table">
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
                                            <input maxlength="50" name="EMPL_DEPARTMENT" id="EMPL_DEPARTMENT" runat="server"
                                                type="text" class="inputText wd200" />
                                        </td>
                                        <td>
                                            <!-- Need to find associate fields in table -->
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        Duration in Current org.
                                                    </td>
                                                    <td>
                                                        <input name="CURRENT_JOB_TENURE" id="CURRENT_JOB_TENURE" maxlength="2" placeholder="Years"
                                                            runat="server" type="text" class="inputText wd50 onlynumeric" />
                                                    </td>
                                                    <td>
                                                        <input name="JOB_MONTHS" id="JOB_MONTHS" runat="server" maxlength="2" placeholder="Months"
                                                            type="text" class="inputText wd50 onlynumeric" />
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
                                <input name="EMPL_ID" id="EMPL_ID" runat="server" type="text" maxlength="20" class="inputText wd100" />
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
                                                        <input runat="server" type="text" placeholder="Present Office Address 1" name="EMPL_ADDRESS1"
                                                            id="EMPL_ADDRESS1" maxlength="45" class="wd200 inputText" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="middle">
                                                        <input name="EMPL_ADDRESS2" maxlength="45" placeholder="Present Office Address 2"
                                                            id="EMPL_ADDRESS2" runat="server" type="text" class="wd200 inputText" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="middle">
                                                        <input name="EMPL_ADDRESS3" maxlength="45" placeholder="Present Office Address 3"
                                                            id="EMPL_ADDRESS3" runat="server" type="text" class="wd200 inputText" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="middle">
                                                        <input name="EMPL_ADDRESS4" maxlength="45" id="EMPL_ADDRESS4" placeholder="Present Office Address 4"
                                                            runat="server" type="text" class="wd200 inputText" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="30">
                                                        Country
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddlEmpCountry" CssClass="myselect wd150">
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
                                                        <input name="OFFICE_PHONE_NUMBER" maxlength="20" id="OFFICE_PHONE_NUMBER" runat="server"
                                                            type="text" class="inputText wd150 onlynumeric" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        City
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlEmpCity" runat="server" CssClass="myselect wd150">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        Pincode
                                                    </td>
                                                    <td>
                                                        <input maxlength="10" name="EMPL_POSTAL_CODE" id="EMPL_POSTAL_CODE" runat="server"
                                                            type="text" class="inputText wd150 onlynumeric" />
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
                                Annual income (In Rs.)
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="ANNUAL_INCOME_CODE" maxlength="12" id="ANNUAL_INCOME_CODE" runat="server"
                                    type="text" class="wd150 inputText onlynumeric" />
                            </td>
                            <td height="35" align="left" valign="middle" width="130">
                                Other Income(In Rs.)
                            </td>
                            <td height="35" align="left" valign="middle" width="160">
                                <input name="OTHER_INCOME" id="OTHER_INCOME" runat="server" type="text" class="wd150 inputText onlyDecimalnumeric" />
                            </td>
                            <td height="35" align="left" valign="middle" width="140">
                                Spouse Income(In Rs.)
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="SPOUSE_INCOME" id="SPOUSE_INCOME" runat="server" type="text" class="wd150 inputText onlyDecimalnumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="left" valign="middle">
                                Income Per Month
                            </td>
                            <td colspan="3" height="35" align="left" valign="middle">
                                <asp:RadioButtonList ID="radIncomePerMonth" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Table" Width="100%" RepeatColumns="4">
                                </asp:RadioButtonList>
                            </td>
                            <td height="35" align="left" valign="middle">
                                Customer ID
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input runat="server" id="CUSTOMER_ID" name="CUSTOMER_ID" maxlength="10" type="text"
                                    class="wd150 inputText" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="left" valign="middle">
                                PAN No.
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="PAN_GIR_NO" id="PAN_GIR_NO" maxlength="10" onblur="ValidatePAN(this);"
                                    runat="server" type="text" class="wd150 inputText" />
                            </td>
                            <td height="35" align="left" valign="middle">
                                Tax Paid (In Rs.)
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="TAX_PAID" maxlength="12" id="TAX_PAID" runat="server" type="text" class="wd150 inputText onlynumeric" />
                            </td>
                            <td height="35" align="left" valign="middle">
                                Year of Tax Paid
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="YEAR_TAX_PAID" maxlength="4" id="YEAR_TAX_PAID" runat="server" type="text"
                                    class="wd150 inputText onlynumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="left" valign="middle">
                                Is Account With BOB
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlIsAccountWithbank" CssClass="myselect wd200">
                                </asp:DropDownList>
                            </td>
                            <td height="35" align="left" valign="middle">
                                Account Branch
                            </td>
                            <td height="35" align="left" valign="middle">
                                <asp:DropDownList runat="server" ID="ddlAccountBranch" CssClass="myselect wd200">
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
                                <textarea name="BANK_BRANCH_ADDRESS" id="BANK_BRANCH_ADDRESS" runat="server" class="textArea wd200 ht50"></textarea>
                            </td>
                            <td colspan="4" height="35" align="left" valign="middle">
                                <table width="98%" border="0" cellspacing="0" cellpadding="0" class="ml10">
                                    <tr>
                                        <td height="45" width="85" align="left" valign="middle">
                                            City
                                        </td>
                                        <td height="45" align="left" valign="middle">
                                            <!-- Need to find associate field in table -->
                                            <input name="OTHER_CITY" id="OTHER_CITY" runat="server" maxlength="50" type="text"
                                                class="wd150 inputText" />
                                        </td>
                                        <td height="45" align="left" valign="middle" width="140">
                                            No. of Years with Bank
                                        </td>
                                        <td height="45" align="left" valign="middle">
                                            <!-- Need to find associate field in table -->
                                            <input name="OUR_ACCOUNT_TENURE" maxlength="4" id="OUR_ACCOUNT_TENURE" runat="server"
                                                type="text" class="wd150 inputText onlynumeric" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- Need to find associate fields -->
                                        <td height="45" align="left" valign="middle">
                                            Natures of A/c
                                        </td>
                                        <td height="45" align="left" valign="middle">
                                            <span class="fLeft">
                                                <input name="OUR_ACCOUNT_TYPE" id="saving_acc" runat="server" type="checkbox" value="0" />
                                                Savings A/c</span> <span class="fLeft ml10">
                                                    <input name="OUR_ACCOUNT_TYPE" id="other_acc" runat="server" type="checkbox" value="1" />
                                                    Other</span> <span class="fLeft">
                                                        <input name="OUR_ACCOUNT_TYPE" id="current_acc" runat="server" type="checkbox" value="2" />
                                                        Current A/c</span>
                                        </td>
                                        <td height="45" align="left" valign="middle">
                                            CBS A/c Number
                                        </td>
                                        <td height="45" align="left" valign="middle">
                                            <input name="ACCOUNT_NUMBER" id="ACCOUNT_NUMBER" maxlength="24" runat="server" type="text"
                                                class="wd150 inputText" />
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
                                            <input name="HOUSING_LOAN" id="HOUSING_LOAN" runat="server" type="checkbox" />
                                            Housing Loan
                                        </td>
                                        <td height="35" align="left" valign="middle">
                                            <input name="CAR_LOAN" id="CAR_LOAN" runat="server" type="checkbox" />
                                            Car Loan
                                        </td>
                                        <td height="35" align="left" valign="middle">
                                            <input name="CONSUMER_LOAN" id="CONSUMER_LOAN" runat="server" type="checkbox" />
                                            Consumer Loan
                                        </td>
                                        <td height="35" align="left" valign="middle">
                                            <input name="BUSINESS_LOAN" id="BUSINESS_LOAN" runat="server" type="checkbox" />
                                            Business Loan
                                        </td>
                                        <td height="35" align="left" valign="middle">
                                            <input name="OTHR_LOAN" id="OTHR_LOAN" runat="server" type="checkbox" />
                                            Others
                                        </td>
                                        <td height="35" align="left" valign="middle">
                                            <input name="OTHER_LOAN" id="OTHER_LOAN" runat="server" type="text" class="wd30 inputText" />
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
                                <input name="LOAN_AMOUNT" maxlength="20" id="LOAN_AMOUNT" runat="server" type="text"
                                    class="wd150 inputText onlyDecimalnumeric" />
                            </td>
                            <td height="35" align="left" valign="middle">
                                Current Outstandings
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="CURRENT_OUTSTANDING" maxlength="20" id="CURRENT_OUTSTANDING" runat="server"
                                    type="text" class="wd150 inputText onlyDecimalnumeric" />
                            </td>
                            <td height="35" align="left" valign="middle">
                                Duration of Loan
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input name="DURATION_OF_LOAN" maxlength="3" id="DURATION_OF_LOAN" runat="server"
                                    type="text" class="wd150 inputText onlynumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35" align="left" valign="middle">
                                Name of Institution from<br />
                                where Loan taken
                            </td>
                            <td colspan="3" height="35" align="left" valign="middle">
                                <input maxlength="25" name="LOAN_INSTUTION_NAME" id="LOAN_INSTUTION_NAME" runat="server"
                                    type="text" class="wd300 inputText" />
                            </td>
                            <td height="35" align="left" valign="middle">
                                Branch Name
                            </td>
                            <td height="35" align="left" valign="middle">
                                <input maxlength="25" name="LOAN_BRANCH" id="LOAN_BRANCH" runat="server" type="text"
                                    class="wd150 inputText" />
                            </td>
                        </tr>
                    </table>
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
                                <input name="BOB_DEBIT_CARD_NO" maxlength="19" id="BOB_DEBIT_CARD_NO" runat="server"
                                    type="text" class="wd200 inputText" />
                            </td>
                            <td>
                                Valid Upto
                            </td>
                            <td>
                                <div class="fLeft">
                                    <asp:Label runat="server" ID="DC_VALID_UPTO"></asp:Label>
                                    <span class="hints">MMYY format</span>
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
                                <input name="CC_BANK_NAME1" maxlength="50" id="CC_BANK_NAME1" runat="server" type="text"
                                    class="wd300 inputText" />
                            </td>
                            <td>
                                <input name="CC_NO1" maxlength="19" id="CC_NO1" runat="server" type="text" class="wd150 inputText" />
                            </td>
                            <td>
                                <div class="fLeft">
                                    <asp:Label runat="server" ID="CC_VALID_UPTO"></asp:Label>
                                    <span class="hints">MMYY format</span>
                                </div>
                            </td>
                            <td>
                                <input name="CC_CR_LITMIT1" maxlength="15" id="CC_CR_LITMIT1" runat="server" type="text"
                                    class="wd150 inputText onlyDecimalnumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" height="35">
                                2
                            </td>
                            <td>
                                <input name="CC_BANK_NAME2" maxlength="50" id="CC_BANK_NAME2" runat="server" type="text"
                                    class="wd300 inputText" />
                            </td>
                            <td>
                                <input name="CC_NO2" runat="server" maxlength="19" id="CC_NO2" type="text" class="wd150 inputText" />
                            </td>
                            <td>
                                <div class="fLeft">
                                    <asp:Label runat="server" ID="CC_VALID_UPTO2"></asp:Label>
                                    <span class="hints">MMYY format</span>
                                </div>
                            </td>
                            <td>
                                <input name="CC_CR_LITMIT2" maxlength="15" id="CC_CR_LITMIT2" runat="server" type="text"
                                    class="wd150 inputText onlyDecimalnumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" height="35">
                                3
                            </td>
                            <td>
                                <input name="CC_BANK_NAME3" maxlength="50" id="CC_BANK_NAME3" runat="server" type="text"
                                    class="wd300 inputText" />
                            </td>
                            <td>
                                <input name="CC_NO3" maxlength="16" id="CC_NO3" runat="server" type="text" class="wd150 inputText" />
                            </td>
                            <td>
                                <div class="fLeft">
                                    <asp:Label runat="server" ID="CC_VALID_UPTO3"></asp:Label>
                                    <span class="hints">MMYY format</span>
                                </div>
                            </td>
                            <td>
                                <input name="CC_CR_LITMIT3" maxlength="15" id="CC_CR_LITMIT3" runat="server" type="text"
                                    class="wd150 inputText onlyDecimalnumeric" />
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
                                <input name="ADDITIONAL_CARD_NAME" maxlength="40" placeholder="First Addon Applicant Name"
                                    id="ADDITIONAL_CARD_NAME" runat="server" type="text" class="inputText wd300" />
                            </td>
                            <td width="100" align="left" valign="middle">
                            </td>
                            <td>
                                Date of Birth
                            </td>
                            <td>
                                <input name="SEC_BIRTH_DATE" id="SEC_BIRTH_DATE" runat="server" type="text" class="wd100 inputText" />
                            </td>
                            <td>
                                Gender
                            </td>
                            <td width="100" align="left" valign="middle">
                                <asp:RadioButtonList runat="server" ID="radAddGender">
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
                                <asp:RadioButtonList runat="server" ID="radAddonRelation" RepeatColumns="4" Width="100%"
                                    RepeatLayout="Table" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                Occupation
                            </td>
                            <td colspan="1">
                                <asp:Label runat="server" ID="lblSEC1_APPLICANT_PROF"></asp:Label>
                                <asp:DropDownList runat="server" TabIndex="128" ID="SEC1_APPLICANT_PROF" CssClass="myselect wd100">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="left" valign="middle">
                                2
                            </td>
                            <td>
                                <input name="" type="text" placeholder="Second Addon Applicant Name" class="inputText wd300" />
                            </td>
                            <td width="100" align="left" valign="middle">
                            </td>
                            <td>
                                Date of Birth
                            </td>
                            <td>
                                <input name="SEC2_BIRTH_DATE" id="SEC2_BIRTH_DATE" runat="server" type="text" class="wd100 inputText" />
                            </td>
                            <td>
                                Gender
                            </td>
                            <td width="100" align="left" valign="middle">
                                <asp:RadioButtonList runat="server" ID="radAddGender2">
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
                                <asp:RadioButtonList runat="server" ID="radAddon1Relation" RepeatColumns="4" Width="100%"
                                    RepeatLayout="Table" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                Occupation
                            </td>
                            <td colspan="1">
                                <asp:Label runat="server" ID="lblSEC2_APPLICANT_PROF"></asp:Label>
                                <asp:DropDownList runat="server" TabIndex="133" ID="SEC2_APPLICANT_PROF" CssClass="myselect wd100">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td height="30" align="left" valign="middle">
                                3
                            </td>
                            <td>
                                <input name="" type="text" placeholder="Third Addon Applicant Name" class="inputText wd300" />
                            </td>
                            <td width="100" align="left" valign="middle">
                            </td>
                            <td>
                                Date of Birth
                            </td>
                            <td>
                                <input name="" type="text" class="wd100 inputText" />
                            </td>
                            <td>
                                Gender
                            </td>
                            <td width="100" align="left" valign="middle">
                                <asp:RadioButtonList runat="server" ID="radAddGender3">
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
                                <asp:RadioButtonList runat="server" ID="Rad3AddonRelation" RepeatColumns="4" Width="100%"
                                    RepeatLayout="Table" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                Occupation
                            </td>
                            <td colspan="1">
                                <input name="" type="text" class="wd100 inputText" />
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
                                <input name="REF1_NAME" id="REF1_NAME" maxlength="40" placeholder="Refrence Name"
                                    runat="server" type="text" class="wd300 inputText onlyAlphabet" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Address Line 1
                            </td>
                            <td height="30" align="left" valign="middle">
                                <input runat="server" type="text" name="REF1_ADDRESS1" placeholder="Address Line 1"
                                    id="REF1_ADDRESS1" maxlength="45" class="wd260 inputText" />
                            </td>
                            <td align="left" valign="top">
                                Country
                            </td>
                            <td align="left" valign="middle">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                        <ContentTemplate>
                                            <asp:DropDownList runat="server" ID="ddlRelcountry" CssClass="myselect wd150">
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
                                <input runat="server" type="text" name="REF1_ADDRESS2" placeholder="Address Line 2"
                                    id="REF1_ADDRESS2" maxlength="45" class="wd260 inputText" />
                            </td>
                            <td height="30" align="left" valign="middle">
                                City
                            </td>
                            <td align="left" valign="middle">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
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
                                <input name="REF1_ADDRESS3" maxlength="45" placeholder="Address Line 3" id="REF1_ADDRESS3"
                                    runat="server" type="text" class="wd260 inputText" />
                            </td>
                            <td align="left" valign="middle">
                                Pincode
                            </td>
                            <td align="left" valign="middle">
                                <input name="REF1_ZIP_CODE" id="REF1_ZIP_CODE" maxlength="10" runat="server" type="text"
                                    class="wd150 inputText onlynumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Address Line 4
                            </td>
                            <td align="left" valign="middle">
                                <input name="REF1_ADDRESS4" maxlength="45" placeholder="Address Line 4" id="REF1_ADDRESS4"
                                    runat="server" type="text" class="wd260 inputText" />
                            </td>
                            <td align="left" valign="middle">
                                Phone Number
                            </td>
                            <td align="left" valign="top">
                                <input name="REF1_PHONE_NUMBER" maxlength="20" id="REF1_PHONE_NUMBER" runat="server"
                                    type="text" class="wd150 inputText onlynumeric" />
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
                                <input name="REF2_NAME" id="REF2_NAME" maxlength="40" placeholder="Refrence Name"
                                    runat="server" type="text" class="wd300 inputText onlyAlphabet" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Address Line 1
                            </td>
                            <td height="30" align="left" valign="middle">
                                <input runat="server" type="text" name="REF2_ADDRESS1" placeholder="Address Line 1"
                                    id="REF2_ADDRESS1" maxlength="45" class="wd260 inputText" />
                            </td>
                            <td align="left" valign="top">
                                Country
                            </td>
                            <td align="left" valign="middle">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                        <ContentTemplate>
                                            <asp:DropDownList runat="server" ID="ddlRel2Country" CssClass="myselect wd150">
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
                                <input runat="server" type="text" name="REF2_ADDRESS2" placeholder="Address Line 2"
                                    id="REF2_ADDRESS2" maxlength="45" class="wd260 inputText" />
                            </td>
                            <td height="30" align="left" valign="middle">
                                City
                            </td>
                            <td align="left" valign="middle">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="always">
                                        <ContentTemplate>
                                            <asp:DropDownList runat="server" ID="ddlrel2city" CssClass="myselect wd150">
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
                                <input name="REF2_ADDRESS3" maxlength="45" placeholder="Address Line 3" id="REF2_ADDRESS3"
                                    runat="server" type="text" class="wd260 inputText" />
                            </td>
                            <td align="left" valign="middle">
                                Pincode
                            </td>
                            <td align="left" valign="middle">
                                <input name="REF2_ZIP_CODE" id="REF2_ZIP_CODE" maxlength="10" runat="server" type="text"
                                    class="wd150 inputText onlynumeric" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                Address Line 4
                            </td>
                            <td align="left" valign="middle">
                                <input name="REF2_ADDRESS4" maxlength="45" placeholder="Address Line 4" id="REF2_ADDRESS4"
                                    runat="server" type="text" class="wd260 inputText" />
                            </td>
                            <td align="left" valign="middle">
                                Phone Number
                            </td>
                            <td align="left" valign="top">
                                <input name="REF2_PHONE_NUMBER" maxlength="20" id="REF2_PHONE_NUMBER" runat="server"
                                    type="text" class="wd150 inputText onlynumeric" />
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
                                <input name="INSURANCE_NOM_NAME" runat="server" id="INSURANCE_NOM_NAME" maxlength="40"
                                    type="text" class="wd300 inputText" />
                            </td>
                            <td align="left" valign="top">
                                Relationship
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList runat="server" ID="ddlNomiRelation" CssClass="myselect wd150">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="linespacer">
                    &nbsp;</div>
                <div class="appformstep2">
                    <img src="images/application-form-2nd-step.jpg" alt="" /></div>
            </div>
            <!-- Tabbing 3 -->
            <form id="frmPage3">
            <div id="tab-3" class="tabcontent clearfix">
                <div class="appformstep3">
                    <img src="images/application-form-3rd-step.jpg" alt="" /></div>
            </div>
            </form>
            <!-- Tabbing 4 -->
            <%--<form id="frmPage4" action="">
            <div id="tab-4" class="tabcontent clearfix" style="padding: 0px 5px;">--%>
            <div class="appformSubtitle">
                <h3>
                    (For Auto Debit Faculity)</h3>
            </div>
            <table width="97%" border="0" cellspacing="0" cellpadding="0" class="ml30 mb30" id="tblautdebit">
                <tr>
                    <td>
                        Do you want Auto Debit Facility
                        <asp:CheckBox runat="server" ID="chkautodebit" />
                    </td>
                </tr>
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
                            <asp:Label runat="server" ID="lblBobCardType" Style="font-weight: bold"></asp:Label>
                            card. I irrevocably authorize Bobcards Ltd. to debit my (SB/CA)
                            <asp:DropDownList ID="ddlDebitAccountType" runat="server" CssClass="myselect wd100">
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblDebitAccountType" Style="font-weight: bold"></asp:Label>
                            <%-- <select class="myselect wd100">
                                <option>Select A/c</option>
                            </select>--%>
                            A/c no.
                            <asp:Label runat="server" ID="DIRECT_DEBIT_ACCOUNT_NUMBER"></asp:Label>
                            maintained at Bank of Baroda
                            <asp:DropDownList ID="ddlDebitBranchList" TabIndex="158" runat="server" CssClass="myselect">
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblDebitBranchList" Style="font-weight: bold"></asp:Label>
                            <%-- <input runat="server" name="DIRECT_DEBIT_BRANCH" id="DIRECT_DEBIT_BRANCH" maxlength="20"
                                class="inputText wd150" />
                            <select class="myselect wd100">
                                        <option>Select Branch</option>
                                    </select>--%>
                            Branch as indicated below</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            I, hereby also confirm that I am a authorized signatory of the above stated a/c
                            & it pertains to me.
                            <br />
                            <asp:RadioButton ID="rbTotalAmountDue" TabIndex="162" runat="server" Text="Total Amount Due"
                                GroupName="PaymentType" />
                            <asp:HiddenField ID="hideTotalAmountDue" runat="server" Value="1" />
                            <br />
                            <asp:RadioButton ID="rbMinimumAmountDue" TabIndex="163" runat="server" Text="Minimum Amount Due"
                                GroupName="PaymentType" />
                            <asp:HiddenField ID="hideMinimumAmountDue" runat="server" Value="2" />
                            <br />
                            <asp:RadioButton ID="rbPercentage" runat="server" TabIndex="164" Text="Specific % of monthly Due"
                                GroupName="PaymentType" />
                            <asp:HiddenField ID="hidePercentage" runat="server" Value="3" />
                            <asp:TextBox ID="txtPercentage" Width="50" runat="server" TabIndex="165" CssClass="inputText onlynumeric"
                                MaxLength="3"></asp:TextBox>
                            <asp:Label ID="cvPaymentType" runat="server" CssClass="error"></asp:Label>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" valign="middle">
                        Debit Percentage
                    </td>
                    <td>
                        &nbsp;
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
                                    <asp:Label runat="server" ID="DIRECT_DEBIT_ACCOUNT_NAME" Style="font-weight: bold"></asp:Label>
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
            <%--</div>
            </form>--%>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="javascript/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="javascript/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.appformContent').find('input, select, textarea').attr('disabled', 'disabled');
        });
    </script>
</body>
</html>
