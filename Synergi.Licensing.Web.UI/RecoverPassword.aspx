<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" 
Inherits="Synergi.Licensing.Web.UI.RecoverPassword" 
Title="Synergi Licensing - Recover Password" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Synergi Licensing - Recover Password</title>
</head>

<body style="background-color: White;">
    <form id="Form1" runat="server">
        <div style="height: 90px;">
            &nbsp;</div>
        <div style="text-align: center;">
            <img id="Img2" src="Images/ezySoftwareLogon.gif" alt="Synergi" /></div>
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
                                <asp:PasswordRecovery ID="PasswordRecoveryControl" runat="server" 
                                    onsendingmail="RecoverPwd_SendingMail">
                                    <LabelStyle ForeColor="Black" Font-Size="10" Font-Bold="True" HorizontalAlign="Left"
                                        Height="30px" Width="120px" />
                                    <InstructionTextStyle Font-Italic="True" Font-Size="10" ForeColor="Black" />
                                    <TitleTextStyle Font-Italic="True" Font-Size="10" ForeColor="Black" />
                                    <SuccessTextStyle Font-Italic="True" Font-Size="10" ForeColor="Black" />
                                    <FailureTextStyle Font-Italic="True" Font-Size="10" ForeColor="Red" />
                                    <ValidatorTextStyle Font-Italic="True" Font-Size="10" ForeColor="Red" />
                                    <SubmitButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="Verdana" Font-Size="Smaller" ForeColor="#284775" />
                                    <MailDefinition BodyFileName="~/EmailTemplates/PasswordRecovery.txt" 
                                        Subject="Your password has been reset...">
                                    </MailDefinition>
                                </asp:PasswordRecovery>                                
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
