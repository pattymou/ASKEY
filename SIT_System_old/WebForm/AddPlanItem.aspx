<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddPlanItem.aspx.cs" Inherits="WebForm_AddPlanItem" ValidateRequest="false"%>

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
        <font face="verdana"color="0000DD"size="4" ><legend>新增TestCase</legend></font>
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
                    
                    <%--<asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox>--%>
                    
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
                    
                    <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                    
                    <asp:DropDownList ID="ddlP_Name" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>            
            <tr>
                <td>
                    
                    <asp:Label ID="Label18" runat="server" Text="Requirement ID："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtRequirement" runat="server"></asp:TextBox>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    
                    <asp:Label ID="Label11" runat="server" Text="Requirement關聯："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtRequirementB" runat="server" TextMode="MultiLine" Width="400px" 
                        Height="83px"></asp:TextBox>
                    
                </td>
            </tr>            
                      
            <tr>
                <td>
                
                    <asp:Label ID="Label2" runat="server" Text="Category："></asp:Label>
                
                </td>
                <td>
                    
                    <asp:TextBox ID="txtCategory" runat="server" TextMode="MultiLine" Width="400px" 
                        Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="Sub-Category："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtSubCategory" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label4" runat="server" Text="Purpose："></asp:Label>
                    
                </td>
                <td>
                    <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server">
                    </CKEditor:CKEditorControl>
                </td>
            </tr>
            
             <tr>
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="Environment Setup："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtEnvironmentSetup" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            
            
            <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="Test Steps："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTestSteps" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label6" runat="server" Text="Expected Results："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtExpectedResults" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label7" runat="server" Text="Test Result："></asp:Label>
                    
                </td>
                <td>
                    
                    
                    <asp:DropDownList ID="ddlResult" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Pass</asp:ListItem>
                        <asp:ListItem>Fail</asp:ListItem>
                        <asp:ListItem>TBD</asp:ListItem>
                        <asp:ListItem>N/T</asp:ListItem>
                        <asp:ListItem>N/A</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label9" runat="server" Text="Bug Ticket ID："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtBugTicketID" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    
                    <asp:Label ID="Label10" runat="server" Text="RD Comment："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtRDComment" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr> 
            <tr>
                <td>
                </td>
                <td>
                    
                    <asp:CheckBox ID="chkTestPool" runat="server" Text="加入TestPool" />
                    
                </td>
            </tr> 
            <%--<tr>
                <td>
                    
                    <asp:Label ID="Label11" runat="server" Text="Test Result："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTestResult" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="Date："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtDate" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label13" runat="server" Text="Priority："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtPriority" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="Location："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtLocation" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label15" runat="server" Text="Ticket ID："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTicketID" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr> 
            <tr>
                <td>
                    
                    <asp:Label ID="Label16" runat="server" Text="Comment："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>                                      
            <tr>
                <td>
                    
                    <asp:Label ID="Label8" runat="server" Text="Engineer："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlEngineer" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>--%>
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

