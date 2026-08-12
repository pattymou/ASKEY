<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" validateRequest="false" CodeFile="AddBulletin.aspx.cs" Inherits="WebForm_AddBulletin" Title="" %>

<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    <script src="../ckeditor/config.js"></script>

    <script type="text/javascript">
        $(function() {
        CKEDITOR.replace('<%=CKEditorControl1.ClientID %>', { filebrowserImageUploadUrl: '../ajax/Upload_Bulletin.ashx' });
        });
    </script>
    <table>
        <tr>
            <td>
                    <%--<asp:TextBox ID="txtTable" runat="server"></asp:TextBox>--%>
                    
                <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server">
                </CKEditor:CKEditorControl>
                    
            </td>
        </tr>
        <tr>
                <td colspan=2 align=center>
                    
                    <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />

                    
                    
                </td>
            </tr>
    </table>
</asp:Content>

