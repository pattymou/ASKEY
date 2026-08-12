<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="SearchApplication_A.aspx.cs" Inherits="WebForm_SearchApplication_A" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <fieldset>

    <font face="verdana"color="0000DD"size="4" ><legend>項目列表</legend></font>
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>
                                   
                    <asp:Label ID="Label1" runat="server" Text="申請單編號："></asp:Label>
                                   
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                                   
                    <%--<asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />--%>
                                   
                </td>
            </tr>
            <tr>
                <td>
                                   
                    <asp:Label ID="Label2" runat="server" Text="申請者姓名："></asp:Label>
                                   
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                                   
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                                   
                </td>
            </tr>        
            <tr style="font-size: 9pt">
                <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                        <font face="新細明體" size="2">申請單</font></td>
            </tr>        
            <tr style="font-size: 9pt">
                <td align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>                
                        <asp:GridView ID="gvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BorderWidth="1px" CellPadding="3" DataKeyNames="Name"
                                    ForeColor="#333333" HorizontalAlign="Center" Width="95%" 
                                    OnPageIndexChanging="gvList_PageIndexChanging" GridLines="None" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            
                            <asp:BoundField DataField="ID" HeaderText="申請單編號" ReadOnly="True" SortExpression="ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />

                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# "ApplicationDetail.aspx?ID="+Eval("ID") %>'
                                        Text='<%# Bind("Name") %>'></asp:HyperLink>
                                </ItemTemplate>                                    
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                              
                            <asp:TemplateField HeaderText="Name" SortExpression="Name" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>                                    
                            </asp:TemplateField>   
    <%--                        <asp:TemplateField HeaderText="名稱" SortExpression="name" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName1" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />                                
                            </asp:TemplateField>--%>    
                            
<%--                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server"
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="匯出Excel" />
                                  </ItemTemplate>
                                   
                                </asp:TemplateField> --%>                                        
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />                    
                    </asp:GridView>
                    </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
            </tr>
        </table>        
    </fieldset>

</asp:Content>

