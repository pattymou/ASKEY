<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    

    
    <title>SIT System</title>
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
                                    <td class="tabletext" nowrap="nowrap" style="text-align: right" width="100">
                                        <strong style="font-variant: normal">
                                        <asp:Label ID="Label1" runat="server" Text="登入帳號" Font-Size="Large"></asp:Label>
                                        </strong>
                                    </td>
                                    <td nowrap="nowrap" style="text-align: left">
                                        <asp:TextBox ID="txtAccount" runat="server" Width="150px" Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr height="51">
                                    <td class="tabletext" nowrap="nowrap" style="text-align: right">
                                        <strong>
                                        <asp:Label ID="Label2" runat="server" Text="登入密碼" Font-Size="Large"></asp:Label>
                                        </strong>
                                    </td>
                                    <td nowrap="nowrap" style="text-align: left">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px" 
                                            Font-Size="Large"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" nowrap="nowrap" style="text-align: center">
                                        &nbsp;<asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
                                            Text="登入" Font-Size="Large" />
                                        &nbsp;<asp:Button ID="btnClean" runat="server" Text="清除" 
                                            onclick="btnClean_Click" Font-Size="Large" />
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

    </div>
    </form>
</body>
</html>
