<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="WebForm_Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <fieldset>
        <table id="Table3" class="one" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="類別："></asp:Label>
                    
                    
                    <asp:DropDownList ID="ddlKind" runat="server" Height="16px">
                    </asp:DropDownList>
                    
                    
                </td>
            </tr>        
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="客戶："></asp:Label>
                    
                    <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                    
                    
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="機種名稱："></asp:Label>
                    
                    <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                    
                    
                    <asp:DropDownList ID="ddlP_Name" runat="server">
                    </asp:DropDownList>
                    
                    
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    
                    
                    
                    
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                </td>
            </tr>
<%--            <input type="file" name="myfile" id="myfile" size="100%">                      
	        <input type="Button" value="go" onclick="OpenFile();">--%>             
        </table> 
    </fieldset>

</asp:Content>

