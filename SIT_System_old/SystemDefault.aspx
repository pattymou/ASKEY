<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemDefault.aspx.cs" Inherits="SystemDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<style>
    #center
    {
        height: 200px;
        width: 400px;
        position: absolute; /*絕對位置*/
        top: 50%; /*從上面開始算，下推 50% (一半) 的位置*/
        left: 50%; /*從左邊開始算，右推 50% (一半) 的位置*/
        margin-top: -100px; /*高度的一半*/
        margin-left: -200px; /*寬度的一半*/
        vertical-align: middle;
        line-height: 50px;
    }
</style>
<body>
    <form id="form1" runat="server">
    <div id="center">
        <asp:Label ID="Label1" runat="server" Text="請選擇系統"></asp:Label>
        <center>
            <asp:LinkButton ID="link1" runat="server" OnClick="link1_Click">SIT System</asp:LinkButton>
            <br />
            <asp:LinkButton ID="Link2" runat="server" OnClick="Link2_Click">Application System</asp:LinkButton>
            <br />
            <asp:LinkButton ID="Link3" runat="server" OnClick="Link3_Click">SIT Benchmark</asp:LinkButton>
            <br />
            <asp:LinkButton ID="Link4" runat="server" OnClick="Link4_Click">SIT Reservation</asp:LinkButton>
        </center>
    </div>
    </form>
</body>
</html>
