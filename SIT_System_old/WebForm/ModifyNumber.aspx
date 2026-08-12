<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyNumber.aspx.cs" Inherits="WebForm_ModifyNumber" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <fieldset>
        <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label1" runat="server" Text="(皆為必填項目)" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="工號："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="AD帳號："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="姓名："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="分機："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="ASKEY Mail："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="部門："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="門禁卡號："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCard" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="密碼："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassWord" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="確認密碼："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password"></asp:TextBox>
             
                </td>
                
            </tr>--%>
            
            
        </table>
        <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td align ="center" colspan = 2>

                    <br />
                    <br />
                        <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" /> 
                         
                    <br />
                </td>
            </tr>
        </table> 
    </fieldset>
</asp:Content>

