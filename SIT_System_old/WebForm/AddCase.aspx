<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddCase.aspx.cs" Inherits="WebForm_AddCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <font face="verdana"color="0000DD"size="6" ><legend>檢視任務</legend></font>
        <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="任務名稱"></asp:Label>
                </td>
                
                <td>
                    
                    <asp:TextBox ID="txtCase" runat="server"></asp:TextBox>
                    <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="子任務名稱"></asp:Label>
                </td>
                
                <td>
                    
                    <asp:TextBox ID="txtTask" runat="server"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="備註"></asp:Label>
                </td>
                
                <td>
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                            TextMode="MultiLine" Width="578px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />
                    <br />
                        
                    <asp:Button ID="butOK" runat="server" Text="新增" 
                                        onclick="butOK_Click" />
                        
                    <br />
                    <br />
                </td>
            </tr>            
        </table> 
    </fieldset> 
</asp:Content>

