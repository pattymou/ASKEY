<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="TestMail.aspx.cs" Inherits="WebForm_TestMail" Title="未命名頁面" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <tr>
        <td align ="center" colspan = 2 style="COLOR: red">
            <br />
            <br />
                
            <asp:Button ID="butOK" runat="server" Text="確定" 
                    onclick="butOK_Click" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />   
            <br />
            <br />
        </td>
    </tr>
</asp:Content>

