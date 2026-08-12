<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddTestCase.aspx.cs" Inherits="WebForm_AddTestCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table2" class="one" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Size="XX-Large" 
                        ForeColor="#3333FF"></asp:Label>        
                </td>
            </tr>
            
        </table>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <br>
        
        <tr>
            <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" 
             AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        Width="100%" OnPageIndexChanging="gvwMain_PageIndexChanging" 
             OnPreRender ="gvwMain_PreRender">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField>
<%--                        <headertemplate> 
                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="Comply(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                        </headertemplate>--%>
                        <itemtemplate> 
                            <asp:CheckBox ID="CheckBox2" runat="server"/> 
                        </itemtemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                  
                    <asp:BoundField DataField="Kind" HeaderText="" ReadOnly="True" SortExpression="Kind">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="" ReadOnly="True" SortExpression="Name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Item" HeaderText="" ReadOnly="True" SortExpression="Item">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>                                        
                    <%--<asp:TemplateField HeaderText="測試項目說明" SortExpression="file_tag">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemTemplate>
                            &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>        
                             
                    <asp:TemplateField HeaderText="seq" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
                                   
                </td>
            </tr>        
        
                    
                    <tr>
                        <td align ="center" colspan = 2 style="COLOR: red">
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

