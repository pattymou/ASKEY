<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="SearchProject.aspx.cs" Inherits="WebForm_SearchProject" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label4" runat="server" Text="Team："></asp:Label>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlTeam" runat="server" AutoPostBack="True" >
                        
                        <asp:ListItem>ALL</asp:ListItem>
                    
                    </asp:DropDownList>                
                </td>
            </tr>        
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="類別："></asp:Label>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlKind" runat="server" AutoPostBack="True" >
                        
                        <asp:ListItem>ALL</asp:ListItem>
                    
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="專案名稱："></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtProject" runat="server" Width="229px"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="(可輸入關鍵字)"></asp:Label>
                </td>
            </tr>            
            <%--<ControlStyle Width="30px"></ControlStyle>--%>                        
<%--<ControlStyle Width="30px"></ControlStyle>--%>
            <tr>
                <td colspan=2 align =center>
                    
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    
                    
                    
                    
                </td>
            </tr>
            
        </table>
        
        <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="team" HeaderText="部門" ReadOnly="True" SortExpression="team">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="kind" HeaderText="類別" ReadOnly="True" SortExpression="kind">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="name" HeaderText="專案名稱" ReadOnly="True" SortExpression="name">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
         
                    <asp:TemplateField>
                        <headertemplate> 
                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="匯出詳細資料(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                        </headertemplate>
                        <itemtemplate> 
                            <asp:CheckBox ID="CheckBox2" runat="server"/> 
                        </itemtemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                               
                                                              
                    <asp:TemplateField HeaderText="seq" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
        </asp:GridView> 
        <table id="Table2" class="one" width="100%">
            <tr>
                <td align =center>
                    
                    <asp:Button ID="btnExcel" runat="server" Text="將詳細資料匯出至Excel" 
                        onclick="btnExcel_Click" />
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel1" runat="server" Text="將表格匯出至Excel" 
                        onclick="btnExcel1_Click" />                    
                </td>
            </tr>     
        </table> 
        
   </fieldset>
</asp:Content>

