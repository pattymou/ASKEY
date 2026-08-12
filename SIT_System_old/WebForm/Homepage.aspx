<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="false" CodeFile="Homepage.aspx.cs" Inherits="WebForm_Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1" >
    <table>
        <tr>
            <td>
                <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content> 