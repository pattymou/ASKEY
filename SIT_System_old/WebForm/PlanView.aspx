<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="PlanView.aspx.cs" Inherits="WebForm_PlanView" EnableEventValidation ="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="lblProjectName" runat="server" Text="Label" Font-Bold="True" 
                        Font-Size="X-Large" ForeColor="Blue"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td align =center>
                    
                    <asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                        onclick="btnExcel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                        
                    <asp:Button ID="btnSummary" runat="server" Text="Summary" 
                        onclick="btnSummary_Click" />                    
                </td>
<%--                <td align =center>
                    

                    
                </td> --%>               
            </tr>
           
            <%--<tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDataBound ="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True" SortExpression="Category">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Headline" HeaderText="Headline" ReadOnly="True" SortExpression="Headline">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Pre_Conditions" HeaderText="Pre-Conditions" ReadOnly="True" SortExpression="Pre_Conditions">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="TestSteps" HeaderText="Test Steps" ReadOnly="True" SortExpression="TestSteps">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>     
                                <asp:BoundField DataField="ExpectedResults" HeaderText="Expected Results" ReadOnly="True" SortExpression="ExpectedResults">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                                                                                                                                                                      

                                <asp:BoundField DataField="Engineer" HeaderText="Engineer" ReadOnly="True" SortExpression="Engineer">
                                    <ControlStyle Width="30px"></ControlStyle>
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
                </td>
            </tr>--%>

        </table> 
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDataBound ="gvwMain_RowDataBound">
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

