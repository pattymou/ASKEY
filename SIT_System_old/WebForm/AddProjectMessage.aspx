<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddProjectMessage.aspx.cs" Inherits="WebForm_AddProjectMessage" ValidateRequest="false" %>

<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    <script src="../ckeditor/config.js"></script>

    <script type="text/javascript">
        $(function() {
        CKEDITOR.replace('<%=CKEditorControl1.ClientID %>', { filebrowserImageUploadUrl: '../ajax/Upload.ashx' });
        });
    </script>

    <fieldset>
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="姓名："></asp:Label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">
                        <asp:ListItem>Software</asp:ListItem>
                        <asp:ListItem>Hardware</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server"></CKEditor:CKEditorControl>
                </td>
            </tr>
            <tr>
                <td align=center >
                    
                    <asp:Button ID="btnSave" runat="server" Text="儲存" onclick="btnSave_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" runat="server" Text="上一頁" onclick="btnReturn_Click" />
                </td>
            </tr>
    
        </table> 
    </fieldset> 


</asp:Content>

