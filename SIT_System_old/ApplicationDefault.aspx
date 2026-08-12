<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationDefault.aspx.cs" Inherits="ApplicationDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application System</title>
    <link rel="stylesheet" type="text/css" href="css/Login.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="wrapper" >
            <div id="header">
                <div id="headerBody">

                </div> 
            </div>
           
        </div>
        <div id="Body1">
            
            <table id="Body2"  width =100% height="450px">
                <tr>
                    <td width =700px  rowspan =4>
                    </td>

                </tr>
                                                
                    <tr>
                        <td align="center"  valign="middle" width =450px>
                            <br />
                                <table border="0" cellpadding="3" cellspacing="0" width="100">
                                    <tr height="51">
                                        <td class="tabletext" nowrap="nowrap" style="text-align: right" width="70">
                                            <strong style="font-variant: normal">
                                            <asp:Label ID="Label1" runat="server" Text="工號" Font-Size="Large"></asp:Label>
                                            </strong>
                                        </td>
                                        <td nowrap="nowrap" style="text-align: left">
                                            <asp:TextBox ID="txtAccount" runat="server" Width="150px" Font-Size="Large"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr height="51">
                                        <td class="tabletext" nowrap="nowrap" style="text-align: right">
                                            <strong>
                                            <asp:Label ID="Label2" runat="server" Text="密碼" Font-Size="Large"></asp:Label>
                                            </strong>
                                        </td>
                                        <td nowrap="nowrap" style="text-align: left">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px" Font-Size="Large"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr height="51">
                                        <td class="tabletext" nowrap="nowrap" style="text-align: center" colspan =2>
                                            <asp:Label ID="Label5" runat="server" Text="(密碼為註冊時設定密碼)" Font-Bold="True" 
                                                ForeColor="Red" Font-Size="Large"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap" style="text-align: center">
                                            &nbsp;<asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" Text="登入" Font-Size="Large" />
                                            &nbsp;<asp:Button ID="btnClean" runat="server" Text="清除" Font-Size="Large" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align ="center">
                                            
                                            <asp:LinkButton ID="butLink" runat="server" onclick="butLink_Click" Font-Size="Large">註冊會員</asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="linkPwd" runat="server" onclick="linkPwd_Click" Font-Size="Large">忘記密碼</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="2" align ="center">
                                            
                                            <asp:LinkButton ID="linkTaipei" runat="server" onclick="linkTaipei_Click" Font-Size="Large">台北SIT</asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="linkWJ" runat="server" onclick="linkWJ_Click" Font-Size="Large">吳江SIT</asp:LinkButton>
                                            
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="2" align="center" class="copyright" height="26" 
                                             valign="middle">
                                            <asp:Label ID="Label3" runat="server" Font-Size="Large">Any Issue, Pls contact DA40-簡光賢</asp:Label>
                                            <br />
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Large">請使用Chrome瀏覽器</asp:Label>                                
                                        </td>
                                    </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="copyright" height="26" 
                             valign="middle">
                    </td>

            </table> 
        </div>   
        <div id="footer">
        
        </div>
        <%--<table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr>
                <td colspan="3" style="height: 100px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" width="300">
                        <tr>
                            <td class="systembox" height="51" style="background-color: #014E62" width="396">
                                <span class="systemname" 
                                    style="font-size: 16pt; color: white; font-family: Arial">Application System</span></td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: #DDDDDD" valign="middle">
                                <br />
                                <table border="0" cellpadding="3" cellspacing="0" width="250">
                                    <tr>
                                        <td class="tabletext" nowrap="nowrap" style="text-align: right" width="70">
                                            <strong style="font-variant: normal">
                                            <asp:Label ID="Label1" runat="server" Text="工號"></asp:Label>
                                            </strong>
                                        </td>
                                        <td nowrap="nowrap" style="text-align: left">
                                            <asp:TextBox ID="txtAccount" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tabletext" nowrap="nowrap" style="text-align: right">
                                            <strong>
                                            <asp:Label ID="Label2" runat="server" Text="密碼"></asp:Label>
                                            </strong>
                                        </td>
                                        <td nowrap="nowrap" style="text-align: left">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tabletext" nowrap="nowrap" style="text-align: center" colspan =2>
                                            <asp:Label ID="Label5" runat="server" Text="(密碼為註冊時設定密碼)" Font-Bold="True" 
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap="nowrap" style="text-align: center">
                                            &nbsp;<asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" Text="登入" />
                                            &nbsp;<asp:Button ID="btnClean" runat="server" Text="清除" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align ="center">
                                            
                                            <asp:LinkButton ID="butLink" runat="server" onclick="butLink_Click">註冊會員</asp:LinkButton>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align ="center">
                                            
                                            <asp:LinkButton ID="linkTaipei" runat="server" onclick="linkTaipei_Click">台北SIT</asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="linkWJ" runat="server" onclick="linkWJ_Click">吳江SIT</asp:LinkButton>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="copyright" height="26" 
                                style="background-color: #AAAAAA" valign="middle">
                                <asp:Label ID="Label3" runat="server">Any Issue, Pls contact DA40-簡光賢</asp:Label>
                                <br />
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red">建議使用Chrome瀏覽器</asp:Label>                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
    </div>
    </form>
</body>
</html>
