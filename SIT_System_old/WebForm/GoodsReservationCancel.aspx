<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReservationCancel.aspx.cs" Inherits="WebForm_GoodsReservationCancel" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>

  <script>
    $(function() {
    $( "#tabs" ).tabs();
  });
  </script>
  
    <fieldset>
    
    
        <table id="Table5" class="one" width="100%">
            <tr>    
                <td>
                    <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>

                    <asp:DropDownList ID="ddlKind" runat="server">
                    <%--                    <asp:ListItem Value="0">ALL</asp:ListItem>--%>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;

                    <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                    
                    <asp:Label ID="Label18" runat="server" Text="(貨品搜尋)"></asp:Label>
                </td>
            </tr> 
            <tr>
                <td align ="center">
                    <%--<asp:Label ID="Label19" runat="server" Text="預約日期："></asp:Label>
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
                    
                     </script> --%>
                     <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />                    
                </td>
            </tr> 
  </table>

<br />
                    <asp:DropDownList ID="ddlLocal" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlLocal_SelectedIndexChanged">
                        <asp:ListItem>台北</asp:ListItem>
                        <asp:ListItem>吳江</asp:ListItem>
                    </asp:DropDownList>            
            
<div id="tabs">
  <ul>
    <li><a href="#tabs-1">尚未逾期貨品</a></li>
    <li><a href="#tabs-2">續用貨品</a></li>
  </ul>
  <div id="tabs-1">
    <table id="Table1" class="one" width="100%">    
            <tr>
                <td>
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
                            <%--<asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>     
                            <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                
                            <asp:BoundField DataField="MF" HeaderText="廠商名稱" ReadOnly="True" SortExpression="MF">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="貨品名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
                            <%--<asp:TemplateField HeaderText="領用數量" SortExpression="BorrowedQuantity">                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblBorrowedQuantity" runat="server" Text='<%#Eval("BorrowedQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>--%>  
                            <asp:BoundField DataField="BorrowedQuantity" HeaderText="領用數量" ReadOnly="True" SortExpression="BorrowedQuantity">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <%--<asp:TemplateField HeaderText="歸還數量" SortExpression="ContinuousCount">                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblContinuousCount" runat="server" Text='<%#Eval("ContinuousCount") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>--%>  
                            <asp:BoundField DataField="ContinuousCount" HeaderText="歸還數量" ReadOnly="True" SortExpression="ContinuousCount">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                        
                            <asp:BoundField DataField="StartDate" HeaderText="開始日期" ReadOnly="True" SortExpression="StartDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>       
                            <asp:BoundField DataField="EndDate" HeaderText="建議更換日期" ReadOnly="True" SortExpression="EndDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                   
                            
                             <asp:TemplateField HeaderText="領用人">
                                
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
       </table> 
  </div> 
  <div id="tabs-2">
    <table id="Table3" class="one" width="100%">    
            <tr>
                <td>
                    <asp:GridView ID="gvwMain1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain1_PageIndexChanging" onRowCommand="gvwMain1_RowCommand" OnRowDataBound="gvwMain1_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
                            <%--<asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>     
                            <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                
                            <asp:BoundField DataField="MF" HeaderText="廠商名稱" ReadOnly="True" SortExpression="MF">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="貨品名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
                            <%--<asp:TemplateField HeaderText="領用數量" SortExpression="BorrowedQuantity">                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblBorrowedQuantity" runat="server" Text='<%#Eval("BorrowedQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>--%>  
                            <%--<asp:BoundField DataField="BorrowedQuantity" HeaderText="領用數量" ReadOnly="True" SortExpression="BorrowedQuantity">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%> 
                            <%--<asp:TemplateField HeaderText="歸還數量" SortExpression="ContinuousCount">                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblContinuousCount" runat="server" Text='<%#Eval("ContinuousCount") %>'></asp:Label>
                                    </ItemTemplate>
                            </asp:TemplateField>--%>  
                            <asp:BoundField DataField="ContinuousCount" HeaderText="續用歸還數量" ReadOnly="True" SortExpression="ContinuousCount">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                        
                            <asp:BoundField DataField="EndDate" HeaderText="開始日期" ReadOnly="True" SortExpression="EndDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>       
                            <asp:BoundField DataField="ContinuousDate" HeaderText="建議更換日期" ReadOnly="True" SortExpression="ContinuousDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                   
                            
                             <asp:TemplateField HeaderText="領用人">
                                
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
       </table> 
  </div> 
</div>           
    
       <table id="Table2" class="one" width="100%">
            <tr>
                <td align =center >
                    
                    <asp:Button ID="btnReturn" runat="server" Text="上一頁" 
                        onclick="btnReturn_Click" />
                    
                </td>
            </tr>                    
        </table> 
    </fieldset>
</asp:Content>

