<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeError.aspx.cs" Inherits="CardHolder.ErrorPage.CodeError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error!!!</title>
    <!--[if IE]>
    <script type="text/javascript" src="../javascript/jquery-1.8.3.js"></script>

 <script type="text/javascript" language="javascript">
 $(document).ready(function () {
 
  var objhdn = document.getElementById('hdnBack');

   if (objhdn.value != ''){
   
         location.href = "/ErrorPage/CodeError.aspx";

        
         }
         });
            </script>
<![endif]-->
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../javascript/jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (window.history && history.pushState) { // check for history api support
                window.addEventListener('load', function () {
                    // create history states
                    history.pushState(-1, null); // back state
                    history.pushState(0, null); // main state
                    history.pushState(1, null); // forward state
                    history.go(-1); // start in main state
                    //this.addEventListener('popstate', function (event, state) {
                    this.addEventListener('popstate', function (event) {
                        // check history state and fire custom events
                        if (state = event.state) {
                            event = document.createEvent('Event');
                            event.initEvent(state > 0 ? 'next' : 'previous', true, true);
                            this.dispatchEvent(event);
                            // reset state
                            location.href = "/ErrorPage/CodeError.aspx";
                            return false;
                            //history.go(-state);
                        }
                    }, false);
                }, false);
            }
        });
        function myOnloadFunc() {
            var objhdn = document.getElementById('hdnBack');
            objhdn.value = "hello";
        }
    
    </script>
</head>
<body onload="myOnloadFunc();">
<noscript class="noscriptDiv">
<div id="scriptOffMsg">Some of the functionalities will not work if javascript off. Please enable Javascript for it.
</div>
</noscript>
    <form id="form1" runat="server">
    <input id="hdnBack" type="hidden" value="" />
    <div id="wrapper" class="clearfix">
        <p class="ht80">
            &nbsp;</p>
        <div class="error404 clearfix">
            <p>
                <span class="sorryMsg">APPLICATION SECURITY ERROR !!</span></p>
            <div class="left ml30">
                <strong>This error has occurred for one of the following reasons :</strong></div>
            <ul class="errorList">
                <li>You have used Back/Forward/Refresh button of your Browser.</li>
                <li>You have clicked twice on any options/buttons.</li>
                <li>You have kept the browser window idle for a long time.</li>
                <li>You have logged in from another browser window</li>
                <li>You are accessing the application URL from a saved or static page.</li>
            </ul>
            <div class="clearfix">
            </div>
            <div class="Error404Link">
                <asp:LinkButton runat="server" Text="Click here to login" ID="lnklogin" OnClick="lnklogin_Click"></asp:LinkButton>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
