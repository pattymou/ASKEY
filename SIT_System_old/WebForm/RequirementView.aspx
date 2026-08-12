<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="RequirementView.aspx.cs" Inherits="WebForm_RequirementView" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:LinkButton ID="linkAdd" runat="server" onclick="linkAdd_Click">[建立新Requirement]</asp:LinkButton>
    <fieldset>
        
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label4" runat="server" Text="類別："></asp:Label>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlKind" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind_SelectedIndexChanged">
                        <asp:ListItem>ALL</asp:ListItem>
                    
                    </asp:DropDownList>                
                </td>
            </tr>        
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="客戶："></asp:Label>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                        <asp:ListItem>ALL</asp:ListItem>
                    
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="機種名稱："></asp:Label>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlP_Name" runat="server">
                        <asp:ListItem>ALL</asp:ListItem>
                    
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>
                    
                    <%--<asp:Label ID="Label3" runat="server" Text="機種名稱："></asp:Label>--%>
                    
                </td>
                <td>
                    <asp:CheckBox ID="chkReview" runat="server" Text="是否Review" />
                </td>
            </tr>            
            <tr>
                <td colspan=2 align =center>
                    
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    
                    
                    
                    
                </td>
            </tr> 
                       
                        
        </table> 
        <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="Requirement_ID" HeaderText="Requirement ID" ReadOnly="True" SortExpression="Requirement_ID">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Doc_Ver" HeaderText="Doc Ver." ReadOnly="True" SortExpression="Doc_Ver">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Requirement_Date" HeaderText="Date" ReadOnly="True" SortExpression="Requirement_Date">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>   
                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="Requirement_Table" HeaderText="Table" ReadOnly="True" SortExpression="Requirement_Table">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>     
                    <asp:BoundField DataField="Figure" HeaderText="Figure" ReadOnly="True" SortExpression="Figure">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>                                                                                                                                                                                      

                    <asp:BoundField DataField="Owner" HeaderText="Owner" ReadOnly="True" SortExpression="Owner">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="Review" HeaderText="是否Review" ReadOnly="True" SortExpression="Review">
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
<%--                                <asp:BoundField DataField="EndDate" HeaderText="歸還日期" ReadOnly="True" SortExpression="EndDate">
                        <ControlStyle Width="30px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField> --%>                                                               
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
    </fieldset> 
</asp:Content>

