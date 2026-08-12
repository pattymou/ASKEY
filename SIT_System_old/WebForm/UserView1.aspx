<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="UserView1.aspx.cs" Inherits="WebForm_UserView1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
  <script src="../js/jquery-1.10.4.min.js"></script>
  
  
  
  <script>
      $(function() {
          $("#tabs").tabs();


      });
      $(window).load(function() {



      });   
  </script>
  


  
    <%--<fieldset>--%>


    <font face="verdana"color="0000DD"size="4" ><legend>人員設定</legend></font>
    
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">內部人員</a></li>
            <li><a href="#tabs-2">外部人員</a></li>
        </ul>
        <div id="tabs-1">
            <table id="Table3" class="one" width="100%">
            
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增內部人員]</asp:LinkButton> 
                    <br />                
                </td>
            </tr>

                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate> 
                        <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="登入名稱" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("Name_En") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name_CH" HeaderText="姓名" ReadOnly="True" SortExpression="Name_CH">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="修改" />
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
                                
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table> 
        </div>
        <div id="tabs-2">
            <table id="Table1" class="one" width="100%">
          
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="部門："></asp:Label>
                        
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>  
                        <asp:GridView ID="gvwMain1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain1_PageIndexChanging" onRowCommand="gvwMain1_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="工號" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Number" HeaderText="AD帳號" ReadOnly="True" SortExpression="Number">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="Name" HeaderText="姓名" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="Department" HeaderText="部門" ReadOnly="True" SortExpression="Department">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                          
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="修改" />
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
                                
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDepartment" 
                                EventName="SelectedIndexChanged" />

                </Triggers>
                </asp:UpdatePanel>
                    </td>
                </tr>

            </table> 
        </div>  
    </div> 
    <%--</fieldset>--%> 

</asp:Content>

