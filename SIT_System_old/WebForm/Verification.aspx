<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="Verification.aspx.cs" Inherits="WebForm_Verification" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr>
            <td align ="center">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="XX-Large" 
                    ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align ="center" colspan = 2 style="COLOR: red">

                <br />
                <br />
<%--                    <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                    <asp:Button ID="butReturn" runat="server" Text="回登入畫面" 
                            onclick="butReturn_Click" /> 
                     
                <br />
            </td>
        </tr>        
    </table> 
</asp:Content>

