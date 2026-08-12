<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="SampleReservationCancel.aspx.cs" Inherits="WebForm_SampleReservationCancel" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>

    <fieldset>
        <table id="Table5" class="one" width="100%">
            <tr>    
                <td>
<%--                    <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>

                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;--%>

                    <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                    
                    <asp:Label ID="Label18" runat="server" Text="(樣品搜尋)"></asp:Label>
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="預約日期："></asp:Label>
                    <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                     <script>
                         $(function() {
                         $("#datepicker").datepicker();
                         });
                    
                     </script>
                     <asp:Label ID="Label2" runat="server" Text="～"></asp:Label>
                     <input type="text" id="datepicker1" name = "date2" value = "<%=strStart1%>">
                     <script>
                         $(function() {
                             $("#datepicker1").datepicker();
                         });
                    
                     </script> 
                     <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />                    
                </td>
            </tr>   
            <tr>
                <td>
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
                            <asp:BoundField DataField="Number" HeaderText="編號" ReadOnly="True" SortExpression="Number">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>     
                            <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                
                            <asp:BoundField DataField="Function_Name" HeaderText="功能" ReadOnly="True" SortExpression="Function_Name">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Item" HeaderText="項目" ReadOnly="True" SortExpression="Item">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Model Name" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("ModelName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                             
                            <asp:BoundField DataField="StartDate" HeaderText="開始日期" ReadOnly="True" SortExpression="StartDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>       
                            <asp:BoundField DataField="EndDate" HeaderText="結束日期" ReadOnly="True" SortExpression="EndDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                   
                            
                             <asp:TemplateField HeaderText="借用人">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblBorrower" runat="server" Text='<%# Bind("Borrower") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnSearch" runat="server" 
                                  CommandName="AddToCart" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="取消" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnReturn" runat="server" 
                                  CommandName="AddToCart1" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="歸還" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>                            
                            
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>                
                </td>
            </tr> 
            <tr>
                <td align =center >
                    
                    <asp:Button ID="btnReturn" runat="server" Text="上一頁" 
                        onclick="btnReturn_Click" />
                    
                </td>
            </tr>                    
        </table> 
    </fieldset>
</asp:Content>

