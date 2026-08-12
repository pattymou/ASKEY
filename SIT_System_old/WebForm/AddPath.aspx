<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddPath.aspx.cs" Inherits="WebForm_AddPath" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
    <font face="verdana"color="0000DD"size="4" ><legend>新增/刪除檔案路徑</legend></font>
        <table>
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="Function："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTestCase" runat="server"></asp:TextBox>
                    
                </td>
                <td colspan = 3>
                    
                    <asp:Button ID="btnAddTestCase" runat="server" Text="新增" 
                        onclick="btnAddTestCase_Click" />
                         
                &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" 
                        onclick="btnSearch_Click" />         
                        
                    
                </td>
            </tr>
            <tr>
                <td align ="center" colspan="5">
                    <asp:GridView ID="gvwMain1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                    ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="gvwMain1_PageIndexChanging" OnRowDeleting="gvwMain1_RowDeleting">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                    Text="刪除" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>                    
<%--                        <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                            <ItemTemplate>
                                &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                    Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="清單">
                            <ItemTemplate>
                                <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("TestCase") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            
                        <asp:TemplateField HeaderText="seq" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </td>
            
            </tr>             
            <tr>
                <td colspan =5>
                    ***********************************************************
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="Function："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlTestCase" runat="server" AutoPostBack="True"
                        onselectedindexchanged="ddlTestCase_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="TestCase："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddItem" runat="server" Text="新增" 
                        onclick="btnAddItem_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#dfe9f7" style="height: 27px" colspan=5>
                    <font face="新細明體" size="2">檔案命名清單</font></td>
            </tr>
            <tr>
                <td align ="center" colspan="5">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="1"
                    ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDeleting="gvwMain_RowDeleting">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                    Text="刪除" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>                    
<%--                        <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                            <ItemTemplate>
                                &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                    Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="清單">
                            <ItemTemplate>
                                <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Kind") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            
                        <asp:TemplateField HeaderText="seq" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("FilePath_TestCase_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </td>
            
            </tr>            
        </table> 
    </fieldset> 
</asp:Content>

