<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddRequirement.aspx.cs" Inherits="WebForm_AddRequirement" ValidateRequest="false"%>

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
        CKEDITOR.replace('<%=CKEditorControl2.ClientID %>', { filebrowserImageUploadUrl: '../ajax/Upload.ashx' });
        });
    </script>
    
    <fieldset>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label17" runat="server" Text="類別："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>        
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="客戶："></asp:Label>
                    
                </td>
                <td>
                                       
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True"
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label8" runat="server" Text="機種名稱："></asp:Label>
                    
                </td>
                <td>
                                        
                    <asp:DropDownList ID="ddlP_Name" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlP_Name_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label18" runat="server" Text="ID："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="文件版本："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <asp:TextBox ID="txtVer" runat="server"></asp:TextBox>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="日期："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label4" runat="server" Text="Requirement Description："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <asp:TextBox ID="txtDescription" runat="server" Height="125px" 
                        TextMode="MultiLine" Width="389px"></asp:TextBox>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="Table："></asp:Label>
                    
                </td>
                <td>
                    <%--<asp:TextBox ID="txtTable" runat="server"></asp:TextBox>--%>
                    
                    <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server">
                    </CKEditor:CKEditorControl>
                    
                </td>
            </tr>   
            <tr>
                <td>
                    
                    <asp:Label ID="Label6" runat="server" Text="Figure："></asp:Label>
                    
                </td>
                <td>
                    <%--<asp:TextBox ID="txtFigure" runat="server"></asp:TextBox>--%>
                    
                    <CKEditor:CKEditorControl ID="CKEditorControl2" runat="server">
                    </CKEditor:CKEditorControl>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label7" runat="server" Text="Owner："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <%--<asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>--%>
                    
                    <asp:DropDownList ID="ddlOwner" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label9" runat="server" Text="Purpose Keyword："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <asp:TextBox ID="txtPurposeKeyword" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="Label19" runat="server" Text="關聯"></asp:Label>
                
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlAssociate1" runat="server">
                        <asp:ListItem>and</asp:ListItem>
                        <asp:ListItem>or</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label10" runat="server" Text="Test Steps Keyword："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <asp:TextBox ID="txtTestStepsKeyword" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="Label12" runat="server" Text="關聯"></asp:Label>
                
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlAssociate2" runat="server">
                        <asp:ListItem>and</asp:ListItem>
                        <asp:ListItem>or</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>             
            <tr>
                <td>
                    
                    <asp:Label ID="Label11" runat="server" Text="Expected Results Keyword："></asp:Label>
                    
                </td>
                <td>
                                                           
                    <asp:TextBox ID="txtExpectedKeyword" runat="server"></asp:TextBox>
                    
                </td>
            </tr>                        
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="是否為Requirement："></asp:Label>
                </td>
                <td>
                    
                    <%--<asp:CheckBox ID="checkRequirement" runat="server" Text="是否為Requirement" />--%>
                    
                    <asp:RadioButton ID="radioRequirementY" runat="server" Text="是" Checked="True" 
                        GroupName="0" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radioRequirementN" runat="server" Text="否" GroupName="0" />
                    
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="是否Review："></asp:Label>
                </td>
                <td>
                    
                    <%--<asp:CheckBox ID="chkReview" runat="server" Text="是否Review" />--%>
                    <asp:RadioButton ID="radioReviewY" runat="server" Text="是" GroupName="1" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radioReviewN" runat="server" Text="否" Checked="True" 
                        GroupName="1" />
                </td>
            </tr>                                                                                     
            <tr>
                <td colspan=2 align=center>
                    
                    <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="btnReturn" runat="server" Text="回上一頁" 
                        onclick="btnReturn_Click" />
                    
                    
                </td>
            </tr>           
        </table> 
    </fieldset> 
</asp:Content>

