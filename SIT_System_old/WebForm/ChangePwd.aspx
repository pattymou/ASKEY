<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="WebForm_ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>修改密碼</legend></font>
    <table>
        <tr>
            <td>
                
                <asp:Label ID="Label1"  runat="server" Text="原始密碼"></asp:Label>
                
            </td>
            <td>
                
                <asp:TextBox ID="txtPwd_O"  TextMode="Password" runat="server"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td>
                
                <asp:Label ID="Label2" runat="server" Text="新密碼"></asp:Label>
                
            </td>
            <td>
                
                <asp:TextBox ID="txtPwd_N"  TextMode="Password" runat="server"></asp:TextBox>
                
            </td>
        </tr>    
        <tr>
            <td>
                
                <asp:Label ID="Label3" runat="server" Text="再次確認新密碼"></asp:Label>
                
            </td>
            <td>
                
                <asp:TextBox ID="txtPwd_N1"  TextMode="Password" runat="server"></asp:TextBox>
                
            </td>
        </tr>        
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />
                        <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                    <br />
                </td>
            </tr>            
    </table> 
    
    
</fieldset> 
</asp:Content>

