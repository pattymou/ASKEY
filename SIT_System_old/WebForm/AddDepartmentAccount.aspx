<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddDepartmentAccount.aspx.cs" Inherits="WebForm_AddDepartmentAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <font face="verdana"color="0000DD"size="4" ><legend>新增部門權限</legend></font>
        <hr size="5" width="100%" color="DDDDDD" style="height: 5px">   
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td  align ="right">
                    <asp:Label ID="Label1" runat="server" Text="部門名稱："></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  align ="right">
                    <asp:Label ID="Label2" runat="server" Text="密碼："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
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

