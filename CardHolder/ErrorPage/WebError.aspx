<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebError.aspx.cs" Inherits="CardHolder.ErrorPage.WebError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Web Page Error!!!</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <!--[if IE]>
    <script type="text/javascript" src="../javascript/jquery-1.8.3.js"></script>

 <script type="text/javascript" language="javascript">
 $(document).ready(function () {
 
  var objhdn = document.getElementById('hdnBack');

   if (objhdn.value != ''){
   
         location.href = "/ErrorPage/WebError.aspx";

        
         }
         });
            </script>
<![endif]-->
    <script type="text/javascript" src="../javascript/jquery-1.8.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (window.history && history.pushState) { // check for history api support
                window.addEventListener('load', function () {
                    // create history states
                    history.pushState(-1, null); // back state
                    history.pushState(0, null); // main state
                    history.pushState(1, null); // forward state
                    history.go(-1); // start in main state
                   // this.addEventListener('popstate', function (event, state) {
                    this.addEventListener('popstate', function (event) {
                        // check history state and fire custom events
                        if (state = event.state) {
                            event = document.createEvent('Event');
                            event.initEvent(state > 0 ? 'next' : 'previous', true, true);
                            this.dispatchEvent(event);
                            // reset state
                            location.href = "/ErrorPage/WebError.aspx";
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
        <div id="header" class="clearfix">
            <h1 id="logo">
                <a href="#">
                    <img src="../images/logo.png" width="197" height="75" alt="BOBCARDS" title="BOBCARDS" /></a></h1>
            <div class="fRight">
                <div class="topLinks">
                    <ul>
                        <li class="login">
                            <asp:LinkButton runat="server" Text="Click here to login" ID="lnklogin" OnClick="lnklogin_Click"></asp:LinkButton></li>
                    </ul>
                </div>
                <a href="#">
                    <img src="../images/small-logo.gif" width="120" height="32" alt="Bank of Baroda"
                        title="Bank of Baroda" class="smLogo" /></a></div>
        </div>
        <p class="ht250 clear">
            &nbsp;</p>
        <div class="errorOccure">
            <strong>Web Page Error!!</strong><br />
            Sorry for the inconvenience caused. But it seems, you are logged in from somewhere
            else. In case if you didn't login from somewhere else, Immidiately contact us at
            1800 225 110
        </div>
    </div>
    </form>
</body>
</html>
