<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Synergi.Licensing.Web.UI.DefaultPage"  Title="Synergi Licensing - Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Synergi Licensing</title>
    <style type="text/css">
        html, body
        {
            height: 100%;
            overflow: auto;
        }
        body
        {
            padding: 0;
            margin: 0;
        }
    </style>

    <script type="text/javascript" src="/Silverlight.js"></script>

    <script type="text/javascript" src="/Scripts/SilverlightHelper.js"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function killIframe() {
            $($("iframe")[1]).remove();
        }
        function hideIframe() {
            $($("iframe")[1]).css("visibility", "hidden");
        }

        function showIframe() {
            $($("iframe")[1]).css("visibility", "visible");
        }  
    </script>

</head>
<body>
    <%--<form id="form1" runat="server" style="height: 100%">
        <uc:Silverlight id="silverlightControlHost" ContentTypeName="DashboardPage" runat="server"></uc:Silverlight>
    </form>--%>
    <%-- Hung Big changed this as Rob want to use dashboard as his homepage. --%>
    <form id="form1" runat="server" style="height: 100%">
        <uc:Silverlight id="silverlightControlHost" ContentTypeName="MainPage" runat="server"></uc:Silverlight>
    </form>
</body>
</html>
