<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="TraceibilityView.aspx.cs" Inherits="WebForm_TraceibilityView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label4" runat="server" Text="類別："></asp:Label>
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlKindT" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKindT_SelectedIndexChanged">
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
                    <asp:DropDownList ID="ddlP_Name" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlP_Name_SelectedIndexChanged">
                        <asp:ListItem>ALL</asp:ListItem>
                    
                    </asp:DropDownList>                
                </td>
            </tr>            
<%--            <tr>
                <td>
                    
                    <asp:Label ID="Label6" runat="server" Text="Category："></asp:Label>
                </td>
                <td>    
                    <asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>--%>
            <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="Owner："></asp:Label>
                </td>
                <td>    
                    <asp:DropDownList ID="ddlOwner" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>            
<%--            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="關鍵字："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtSearch" runat="server" Width="271px"></asp:TextBox>
                    
                </td>
            </tr>--%>
            <tr>
                <td colspan=2 align =center>
                    
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    
                    
                    
                    
                </td>
            </tr>
            
        </table>
        
        <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="Requirement_ID" HeaderText="Requirement ID" ReadOnly="True" SortExpression="Requirement_ID">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="TestCase ID" SortExpression="file_tag">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemTemplate>
                            &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "TestPlanView.aspx?ID="+Eval("PlanID") %>'
                                Text='<%# Bind("TestCaseID") %>' Target =_blank></asp:HyperLink>
                        </ItemTemplate>                                    
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>         
                    <asp:TemplateField>
                        <headertemplate> 
                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="Comply(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                        </headertemplate>
                        <itemtemplate> 
                            <asp:CheckBox ID="CheckBox2" runat="server"/> 
                        </itemtemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                               
                    <%--<asp:BoundField DataField="TestCaseID" HeaderText="Doc Ver." ReadOnly="True" SortExpression="Doc_Ver">
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
                    </asp:BoundField>--%>                                                                                                                                                                                                                                                               
                    
<%--                    <asp:TemplateField>
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
                       
                    </asp:TemplateField>--%>                                                          
                                                              
                    <asp:TemplateField HeaderText="seq" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("PlanID") %>'></asp:Label>
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
        <table id="Table2" class="one" width="100%">
            <tr>
                <td align =center>
                    
                    <asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                        onclick="btnExcel_Click" />
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSummary" runat="server" Text="Summary" 
                        onclick="btnSummary_Click" />                        
                    
                </td>
            </tr>     
        </table> 
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound ="GridView1_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>                           
                                <asp:BoundField DataField="RequirementID" HeaderText="Requirement ID" ReadOnly="True" SortExpression="RequirementID">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
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

