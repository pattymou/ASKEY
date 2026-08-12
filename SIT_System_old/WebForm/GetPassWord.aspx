<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GetPassWord.aspx.cs" Inherits="WebForm_GetPassWord" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <font face="verdana"color="0000DD"size="4" ><legend>忘記密碼</legend></font>
    <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="工號："></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
                
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="ASKEY Mail："></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
         
            </td>
                
        </tr>
        <tr>
            <td align ="center" colspan = 2>
                <asp:Label ID="lblMsg" runat="server" Text="密碼已寄到Askey電子信箱" Font-Bold="True" 
                    Font-Size="X-Large" ForeColor="Red"></asp:Label>
            </td>
            
                
        </tr>
        <tr>
            <td align ="center" colspan = 2>

                <br />
                <br />
                    <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="butReturn" runat="server" Text="回登入畫面" 
                            onclick="butReturn_Click" /> 
                     
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

