<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="CountCase.aspx.cs" Inherits="WebForm_CountCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="查詢時間："></asp:Label>
                    <asp:TextBox ID="txtYear" runat="server" Width="45px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonth" runat="server">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="月"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="～"></asp:Label>
                    <asp:TextBox ID="txtYear1" runat="server" Width="45px"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonth1" runat="server">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label6" runat="server" Text="月"></asp:Label>                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="查詢部門："></asp:Label>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="負責人："></asp:Label>
                    <asp:DropDownList ID="ddlAssign" runat="server" AppendDataBoundItems="True">
                        
                    </asp:DropDownList>
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
                                <asp:BoundField DataField="A_Department" HeaderText="部門" ReadOnly="True" SortExpression="A_Department">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="專案名稱" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="Customer" HeaderText="Customer" ReadOnly="True" SortExpression="Customer">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NPI" HeaderText="NPI" ReadOnly="True" SortExpression="NPI">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Assign" HeaderText="負責人" ReadOnly="True" SortExpression="Assign">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                          
                                
<%--                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="選取" />
                                  </ItemTemplate> 
                                </asp:TemplateField>--%>
                                
                                
                                
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
                <td>
                    <asp:Label ID="Label9" runat="server" Text="案件總數："></asp:Label>
                    <asp:Label ID="lblCount" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table> 
    </fieldset> 
</asp:Content>

