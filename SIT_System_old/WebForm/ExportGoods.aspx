<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="ExportGoods.aspx.cs" Inherits="WebForm_ExportGoods" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
 <%--        <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增設備]</asp:LinkButton>
                </td>

            </tr>         
         </table> 
         <br />--%>
         <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            
            <tr>
                <td>
                    <asp:DropDownList ID="ddlKind" runat="server">
<%--                    <asp:ListItem Value="0">ALL</asp:ListItem>--%>
                    </asp:DropDownList>                     
                    <asp:TextBox ID="txtSearch" runat="server" Width="366px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />

                    <asp:Label ID="Label1" runat="server" Text=" (請輸入貨品名稱、廠商名稱)"></asp:Label>

                </td>
            </tr>

            <tr>
                <td align ="center" colspan =2>
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
<%--                                <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                            Text="刪除" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="Brand" HeaderText="廠商" ReadOnly="True" SortExpression="Brand">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Name_En" HeaderText="貨品名稱(英文)" ReadOnly="True" SortExpression="Model">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name_CH" HeaderText="貨品名稱(中文)" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:TemplateField>
                                    <headertemplate> 
                                        <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="Comply(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                                    </headertemplate>
                                    <itemtemplate> 
                                        <asp:CheckBox ID="CheckBox2" runat="server"/> 
                                    </itemtemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                                                              
                                <%--<asp:TemplateField HeaderText="設備名稱" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ApparatusView.aspx?ID="+Eval("ID") %>'
                                            Text='<%# Bind("Name") %>'></asp:HyperLink>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
                        
                        <table id="Table1" class="one" width="100%">
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
                </td>
            </tr>            
         </table> 
    </fieldset>
</asp:Content>

