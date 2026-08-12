<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ReservationMain.aspx.cs" Inherits="WebForm_ReservationMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset>
         <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[預約設備]</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkCancel" runat="server" OnClick="lbtnCancel_Click">[取消/歸還預約設備]</asp:LinkButton>
                    <%--&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkDelay" runat="server" OnClick="lbtnDelay_Click">[逾期尚未歸還設備]</asp:LinkButton>--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="linkContinuous" runat="server" OnClick="lbtnContinuous_Click">[續借設備]</asp:LinkButton>                    
                    
                    
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
                    (請輸入財產編號、設備名稱、廠商或型號)                    
                </td>
            </tr>

            <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="Brand" HeaderText="廠牌" ReadOnly="True" SortExpression="Brand">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="型號" ReadOnly="True" SortExpression="Model">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
<%--                                <asp:TemplateField HeaderText="設備名稱" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ReservationView_jq.aspx?ID="+Eval("ID") %>'
                                            Text='<%# Bind("Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="Name" HeaderText="設備名稱" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Custodian_Department" HeaderText="保管部門" ReadOnly="True" SortExpression="StartDate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Custodian" HeaderText="保管人" ReadOnly="True" SortExpression="EndDate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="" HeaderText="保管人分機" ReadOnly="True" SortExpression="">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="Place" HeaderText="設備位置" ReadOnly="True" SortExpression="EndDate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <%--//0217--%>
                                <asp:BoundField DataField="ReservationStatus" HeaderText="設備狀態" ReadOnly="True" SortExpression="ReservationStatus">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <%--//0217--%>                                                                                                                         
                                <%--<asp:CommandField HeaderText="設備名稱" ShowSelectButton="True" />--%>
                                
                                <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnSearch" runat="server" 
                                  CommandName="AddToCart" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="預約明細" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnReturn" runat="server" 
                                  CommandName="AddToCart1" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="設備資訊" />
                              </ItemTemplate>
                              </asp:TemplateField>
                              
                              <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnHistory" runat="server" 
                                  CommandName="AddToCart2" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="歷史預約明細" />
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

