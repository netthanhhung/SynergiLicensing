<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Synergi.Licensing.Web.UI.Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="RadComboBox.Net2" Namespace="Telerik.WebControls" TagPrefix="radC" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Synergi Licensing - Logon Page</title>
</head>
<body style="background-color: White;">
    <form id="MainForm" runat="server">
        <div style="height: 90px;">
            &nbsp;</div>
        <div style="text-align: center;">
            <img id="Img2" src="Images/SynergiLicensingLogo.gif" alt="Synergi Licensing" /></div>
        <table style="width: 100%;">
            <colgroup>
                <col />
                <col style="width: 300px;" />
                <col />
            </colgroup>
            <tr style="height: 5px;">
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table class="PaleBlueFullWidth">
                        <colgroup>
                            <col class="Sized" />
                            <col />
                            <col class="Sized" />
                        </colgroup>
                        <tr class="RowTop">
                            <td class="BorderTopLeft">
                            </td>
                            <td class="Header">
                            </td>
                            <td class="BorderTopRight">
                            </td>
                        </tr>
                        <tr>
                            <td class="BorderMiddleLeft">
                            </td>
                            <td class="Body" style="text-align: center;">
                                <asp:Login ID="uiLogin" runat="server" BackColor="#e0eaf6" BorderColor="#e0eaf6" BorderPadding="4"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="0.8em" ForeColor="#333333"
                                    Width="280px" TitleText="">
                                    <LabelStyle ForeColor="Black" Font-Size="1em" Font-Bold="True" HorizontalAlign="Left"
                                        Height="30px" Width="120px" />
                                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                    <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="#284775" />
                                    <CheckBoxStyle CssClass="RememberMe" Height="30px" />
                                    <LayoutTemplate>
                                        <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse">
                                            <tr>
                                                <td>
                                                    
                                                    <table border="0" cellpadding="0" style="width: 280px" runat="server" id="LoginTable">                                                       
                                                        <tr>
                                                            <td align="left" style="font-weight: bold; font-size: 1em; width: 120px; color: black;
                                                                height: 30px">
                                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                                            <td align="left">
                                                                <radA:RadAjaxPanel ID="LoginPanel" EnableAJAX="true" runat="server" Height="100%"
                                                                    Width="100%">
                                                                    <asp:TextBox ID="UserName" runat="server" Width="126px"></asp:TextBox>
                                                                    <radC:RadComboBox ID="UserNameComboBox" Width="126px" Skin="WindowsXP" AutoPostBack="True"
                                                                        AllowCustomText="True" ShowToggleImage="True" EnableLoadOnDemand="True" MarkFirstMatch="True"
                                                                        runat="server">
                                                                    </radC:RadComboBox>
                                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                                </radA:RadAjaxPanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="font-weight: bold; font-size: 1em; width: 120px; color: black;
                                                                height: 30px;">
                                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                                            <td align="left">
                                                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="126px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="RememberMe" colspan="2" style="height: 30px">
                                                                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" style="color: red">
                                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2" style="padding-right: 4px;">
                                                                <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                                                                    BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana"
                                                                    Font-Size="Smaller" ForeColor="#284775" Text="Log In" ValidationGroup="Login1" Width="60px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                               
                                             <tr>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="lnkForgetPassword" NavigateUrl="~/RecoverPassword.aspx">Forgot Your Password?</asp:HyperLink>        
                                                </td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                </asp:Login>
                                <asp:Label runat="server" ID="ExtraErrorInformation" CssClass="validator"></asp:Label>
                            </td>
                            <td class="BorderMiddleRight">
                            </td>
                        </tr>                     
                        <tr class="RowBottom">
                            <td class="BorderBottomLeft">
                            </td>
                            <td class="BorderBottomMiddle">
                            </td>
                            <td class="BorderBottomRight">
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
           
        </table>
    </form>
</body>
</html>

