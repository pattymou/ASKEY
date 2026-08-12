<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="SearchGoods.aspx.cs" Inherits="WebForm_SearchGoods" Title="貨品查詢" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
         <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增貨品]</asp:LinkButton>
                </td>

            </tr>         
         </table> 
         <br />
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
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDeleting="gvwMain_RowDeleting" OnRowCreated ="gvwMain_RowCreated" OnRowDataBound ="gvwMain_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
                                <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 
                                <asp:TemplateField HeaderText="貨品名稱(英文)" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# "GoodsView.aspx?ID="+Eval("ID") %>'
                                            Text='<%# Bind("Name_En") %>'></asp:HyperLink>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                            
                                <asp:TemplateField HeaderText="貨品名稱(中文)" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "GoodsView.aspx?ID="+Eval("ID") %>'
                                            Text='<%# Bind("Name_CH") %>'></asp:HyperLink>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Custodian" HeaderText="保管人" ReadOnly="True" SortExpression="Custodian">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="" HeaderText="保管人分機" ReadOnly="True" SortExpression="">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Quantity_Stock" HeaderText="庫存數量" ReadOnly="True" SortExpression="Quantity_Stock">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
<%--                                <asp:TemplateField>
                                    <headertemplate> 
                                        <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="Comply(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                                    </headertemplate>
                                    <itemtemplate> 
                                        <asp:CheckBox ID="CheckBox2" runat="server"/> 
                                    </itemtemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> --%>   
                                <asp:BoundField DataField="Status" HeaderText="狀態" ReadOnly="True" SortExpression="Status">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                              
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
                    <table id="Table3" class="one" width="100%">
                        <tr>
                            <td align =center>
                                
                                <%--<asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                                    onclick="btnExcel_Click" />--%>
                      
                                
                            </td>
                        </tr>     
                    </table>                                    
                </td>
            </tr>            
         </table> 
    </fieldset> 
</asp:Content>

