<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="WebForm_test" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table1" class="one" width="100%">
        <tr>
        <td align ="center" colspan = 2 style="COLOR: red">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
                
            <asp:Button ID="butOK" runat="server" Text="確定" 
                    onclick="butOK_Click" />
                
            <br />
            <br />
        </td>
    </tr>
    </table> 
    </fieldset> 
</asp:Content>

