<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SilverlightMinWidthResizer.ascx.cs" Inherits="Synergi.Licensing.Web.UI.Shared.SilverlightMinWidthResizer" %>
<!--[if IE 6]>
<script type="text/javascript">
    function makeWidth() {
        var host = document.getElementById("silverlightControlHost");
        if(host) {
            var minwidth = host.currentStyle['min-width'].replace('px', '');
            host.style.width = document.documentElement.clientWidth < minwidth ? minwidth + "px" : "100%";
        }
    };
    makeWidth();
    window.attachEvent("onresize", makeWidth);
</script>
<![endif]-->