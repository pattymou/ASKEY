<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddPlan.aspx.cs" Inherits="WebForm_AddPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<fieldset>
         <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    
                    <asp:LinkButton ID="linkAdd" runat="server" onclick="linkAdd_Click">[建立新TestCase]</asp:LinkButton>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind_T" runat="server" AutoPostBack="True"
                        onselectedindexchanged="ddlKind_T_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="客戶："></asp:Label>
                    
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True"
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>  
            <tr>
                <td>
                    
                    <asp:Label ID="Label8" runat="server" Text="機種名稱："></asp:Label>
                    
                    <asp:DropDownList ID="ddlP_Name" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>             
            <tr>
                <td>
                    
                    <asp:Label ID="Label6" runat="server" Text="Category："></asp:Label>
                    
                    <asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>                     
            <tr>
                <td>
<%--                    <asp:DropDownList ID="ddlKind" runat="server">
                        <asp:ListItem>ALL</asp:ListItem>
                        <asp:ListItem>Category</asp:ListItem>
                        <asp:ListItem>Sub-Category</asp:ListItem>
                        <asp:ListItem>Purpose</asp:ListItem>
                    </asp:DropDownList>--%>         
                    <asp:Label ID="Label7" runat="server" Text="關鍵字："></asp:Label>           
                    <asp:TextBox ID="txtSearch" runat="server" Width="366px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                                        
                    
                                        
                </td>
            </tr>

            <tr>
                <td align ="center">
                                        
                    &nbsp;</td>
            </tr>

            
 
            
            <tr>
                <td>
                    

                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333"
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="gvwMain_PageIndexChanging" 
                        onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>                           
                                <%--<asp:BoundField DataField="RequirementID" HeaderText="Requirement ID" ReadOnly="True" SortExpression="RequirementID">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True" SortExpression="Category">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubCategory" HeaderText="Sub-Category" ReadOnly="True" SortExpression="SubCategory">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Purpose" HeaderText="Purpose" ReadOnly="True" SortExpression="Purpose">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="EnvironmentSetup" HeaderText="Environment Setup" ReadOnly="True" SortExpression="Purpose">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="TestSteps" HeaderText="Test Steps" ReadOnly="True" SortExpression="TestSteps">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>     
                                <asp:BoundField DataField="ExpectedResults" HeaderText="Expected Results" ReadOnly="True" SortExpression="ExpectedResults">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                                                                                                                                      

                                <asp:BoundField DataField="TestResult" HeaderText="Test Result" ReadOnly="True" SortExpression="TestResult">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="BugTicketID" HeaderText="Bug Ticket ID" ReadOnly="True" SortExpression="BugTicketID">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RDComment" HeaderText="RD Comment" ReadOnly="True" SortExpression="RDComment">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="選取" />
                                  </ItemTemplate>
                                   
                                </asp:TemplateField>                                                                                                                                                                                                                                                              
                                                                                                                               
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--<Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True" SortExpression="Category">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Headline" HeaderText="Headline" ReadOnly="True" SortExpression="Headline">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Engineer" HeaderText="Engineer" ReadOnly="True" SortExpression="Engineer">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="選取" />
                                  </ItemTemplate>
                                   
                                </asp:TemplateField>                                
                                                              
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>--%>
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>           
                    
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#dfe9f7">               
               
                    <asp:Label ID="Label1" runat="server" Text="已選擇清單"></asp:Label>
               
                </td>                
            </tr>            
            <tr>
                <td>
                     <asp:Label ID="Label4" runat="server" Text="類別："></asp:Label>
                    <asp:DropDownList ID="ddlKind_T1" runat="server">
                    </asp:DropDownList>               
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="客戶："></asp:Label>
                    <asp:DropDownList ID="ddlCustomer1" runat="server" AutoPostBack="True"
                        onselectedindexchanged="ddlCustomer1_SelectedIndexChanged">
                    </asp:DropDownList>                
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="機種名稱："></asp:Label>
                    <asp:DropDownList ID="ddlP_Name1" runat="server">
                    </asp:DropDownList>                
                </td>
            </tr>                    
            <tr>
                <td>
                    

                    <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                    
                    <asp:GridView ID="gvwMain1" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333"
                            Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="gvwMain1_PageIndexChanging"  
                        onRowCommand="gvwMain1_RowCommand" OnRowDataBound ="gvwMain1_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>                           
<%--                                <asp:BoundField DataField="RequirementID" HeaderText="Requirement ID" ReadOnly="True" SortExpression="RequirementID">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True" SortExpression="Category">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubCategory" HeaderText="Sub-Category" ReadOnly="True" SortExpression="SubCategory">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Purpose" HeaderText="Purpose" ReadOnly="True" SortExpression="Purpose">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField> 
                                 <asp:BoundField DataField="EnvironmentSetup" HeaderText="Environment Setup" ReadOnly="True" SortExpression="Purpose">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="TestSteps" HeaderText="Test Steps" ReadOnly="True" SortExpression="TestSteps">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>     
                                <asp:BoundField DataField="ExpectedResults" HeaderText="Expected Results" ReadOnly="True" SortExpression="ExpectedResults">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                                                                                                                                      

                                <asp:BoundField DataField="TestResult" HeaderText="Test Result" ReadOnly="True" SortExpression="TestResult">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="BugTicketID" HeaderText="Bug Ticket ID" ReadOnly="True" SortExpression="BugTicketID">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RDComment" HeaderText="RD Comment" ReadOnly="True" SortExpression="RDComment">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="移除" />
                                  </ItemTemplate>
                                   
                                </asp:TemplateField>                                                                                                                                                                                                                                                              
                                                                                                                               
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--<Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True" SortExpression="Category">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Headline" HeaderText="Headline" ReadOnly="True" SortExpression="Headline">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Engineer" HeaderText="Engineer" ReadOnly="True" SortExpression="Engineer">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="移除" />
                                  </ItemTemplate>
                                   
                                </asp:TemplateField>                                
                                                              
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>--%>
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>           
                    
                </td>
            </tr>
            <tr>
                <td align ="center">
                                        
                    &nbsp;</td>
            </tr>           
            <tr>
                <td align =center>
                    
                    <asp:Button ID="btnSave" runat="server" Text="儲存" onclick="btnSave_Click" />
                    
                </td>
            </tr>        
         </table> 
    </fieldset>

</asp:Content>

