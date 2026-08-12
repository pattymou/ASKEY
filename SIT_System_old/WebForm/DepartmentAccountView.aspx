<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentAccountView.aspx.cs" Inherits="WebForm_DepartmentAccountView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增部門帳號]</asp:LinkButton>--%> 
 <br />

  
<fieldset>
    <font face="verdana"color="0000DD"size="4" ><legend>部門帳號設定</legend></font>
    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
<%--                            <asp:BoundField DataField="Name_En" HeaderText="登入名稱" ReadOnly="True" SortExpression="Name_En">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="部門帳號" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
<%--                            <asp:BoundField DataField="ID" HeaderText="部門" ReadOnly="True" SortExpression="ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> --%>                           
                            <%--<asp:BoundField DataField="Brand" HeaderText="廠商" ReadOnly="True" SortExpression="Brand">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="型號" ReadOnly="True" SortExpression="Model">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="設備名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                            </asp:TemplateField> --%>                          
                            
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnSearch" runat="server" 
                                  CommandName="AddToCart" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="新增/修改密碼" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnReturn" runat="server" 
                                  CommandName="AddToCart1" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="刪除" />
                              </ItemTemplate>
                               
                            </asp:TemplateField>                            
                            
<%--                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
</fieldset> 
</asp:Content>

