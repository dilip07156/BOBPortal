<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MdfHashing.aspx.cs" Inherits="CardHolder.MdfHashing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
    <script src="javascript/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="javascript/bind.js" type="text/javascript"></script>
    <script type="text/javascript">
        function EncryptLoginPassword(strPass) {
            if (strPass != "") {
                 document.getElementById('<%=hdnPass.ClientID %>').value = hex_md5(strPass);
            }
        }
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btngetlist" Text="Get Data" OnClick="btngetlist_Click" />
    </div>
    <div>
        <asp:Button runat="server" ID="btnConvert" Text="Convert Pwd" OnClick="btnConvert_Click" />
        <asp:HiddenField runat="server" ID="hdnPass" />
    </div>
    <div>
        <asp:GridView ID="gvCardholderListing" runat="server" DataKeyNames="CardHolder_Id"
            AutoGenerateColumns="false" Width="100%">
            <AlternatingRowStyle CssClass="secondrow" />
            <Columns>
                <asp:BoundField DataField="User_nm" HeaderText="Username" />
                <asp:BoundField DataField="User_pwd" HeaderText="Password" />
                <asp:BoundField DataField="MdfHashingPwd" HeaderText="Mdf Hashing Pwd" />
            </Columns>
            <EmptyDataTemplate>
                No result found!! Please try again
            </EmptyDataTemplate>
            <RowStyle CssClass="firstrow" />
        </asp:GridView>
    </div>
    
    
    <%--<script type="text/javascript">
        $(".TestButton").click(function () {
            $.ajax({
                url: "MdfHashing.aspx/GetUserData",
                data: '',
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    alert(data.count);
                    for (var i = 0; i < data.length; i++) {
                        alert(data[i]);
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                    return false;
                }
            });
        })
    </script>--%>
    </form>
</body>
</html>
