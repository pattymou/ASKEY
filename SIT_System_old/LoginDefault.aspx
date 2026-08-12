<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginDefault.aspx.cs" Inherits="LoginDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIT System</title>
</head>
<body>
    <style>

        #center1  
        {

            height: 200px;
            width: 400px;
            position: absolute;     /*絕對位置*/
            top: 50%;               /*從上面開始算，下推 50% (一半) 的位置*/
            left: 50%;              /*從左邊開始算，右推 50% (一半) 的位置*/
            margin-top: -100px;     /*高度的一半*/
            margin-left: -200px;    /*寬度的一半*/
            
            vertical-align:middle;
            line-height:50px;
            

            
        }
       

	</style>
	

	

	
	
<body>
    <form id="form1" runat="server">

        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
            <tr>
                <td colspan="3" style="height: 100px">
                    &nbsp;</td>
            </tr>
        <%--<div id="center1">--%>
            <tr>
                <td align="center" valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" width="300">
                        <tr>
                            <td class="systembox" height="51"  width="396" align ="left" >
                                <span class="systemname" 
                                      >請選擇系統</span></td>
                        </tr>            
                        <%--<tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="請選擇系統"></asp:Label>
                            </td>
                        </tr>--%>
                        <%--<center >--%>
                        <br />
                        <tr>
                            <td align="center" valign="middle">
                                <tr>
                                    <td align ="center" height="40">
                                        <asp:LinkButton ID="link1" runat="server" onclick="link1_Click">台北 SIT System</asp:LinkButton>
                                    </td>
                                </tr>

　                              <tr>
　                                  <td align ="center" height="40">
                                        <asp:LinkButton ID="Link2" runat="server" onclick="Link2_Click">吳江 SIT System</asp:LinkButton>
                                    </td>
                                </tr>
                            </td>
                        </tr>
                        <%--</center>--%>
                    </table> 
                </td>
            </tr>
        <%--</div>--%> 
        
        </table> 

   
    </form>
</body>
</html>
