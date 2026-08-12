<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReservationMain.aspx.cs" Inherits="WebForm_GoodsReservationMain" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
         <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[預約貨品]</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkCancel" runat="server" OnClick="lbtnCancel_Click">[取消/歸還舊品]</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<asp:LinkButton ID="linkDelay" runat="server" OnClick="lbtnDelay_Click">[逾期尚未歸還貨品]</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;--%>
                    <asp:LinkButton ID="linkContinuous" runat="server" OnClick="lbtnContinuous_Click">[續用/逾期尚未更換貨品]</asp:LinkButton>                    
                    
                    
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
                    (請輸入貨品名稱、廠商名稱)                    
                </td>
            </tr>

            <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <%--<asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <%--<asp:BoundField DataField="MF_EN" HeaderText="廠商(英文)" ReadOnly="True" SortExpression="MF_EN">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="MF_CH" HeaderText="廠商(中文)" ReadOnly="True" SortExpression="MF_CH">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Name_En" HeaderText="貨品名稱(英文)" ReadOnly="True" SortExpression="Name_En">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Name_CH" HeaderText="貨品名稱(中文)" ReadOnly="True" SortExpression="Name_CH">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Custodian" HeaderText="保管人" ReadOnly="True" SortExpression="Custodian">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="" HeaderText="保管人分機" ReadOnly="True" SortExpression="">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>    
                                <asp:BoundField DataField="Place" HeaderText="貨品位置" ReadOnly="True" SortExpression="Place">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <%--<asp:BoundField DataField="Status" HeaderText="貨品狀態" ReadOnly="True" SortExpression="Status">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> --%> 

                                
                              <%--  <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnSearch" runat="server" 
                                  CommandName="AddToCart" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="預約明細" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>--%>
                            
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnReturn" runat="server" 
                                  CommandName="AddToCart1" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="貨品資訊" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>
 
                             <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnReturn1" runat="server" 
                                  CommandName="AddToCart2" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="借用明細" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>                           
                            
                            
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
                </td>
            </tr>            
         </table> 
    </fieldset>
</asp:Content>

