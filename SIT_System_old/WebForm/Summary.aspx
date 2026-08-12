<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="WebForm_Summary" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="Vertical"
                            Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="gvwMain_PageIndexChanging" 
                        onRowCommand="gvwMain_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Owner" HeaderText="Owner" ReadOnly="True" SortExpression="Owner">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Plan" HeaderText="Plan" ReadOnly="True" SortExpression="Plan">
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
                                <asp:BoundField DataField="Testcases" HeaderText="Test cases" ReadOnly="True" SortExpression="Testcases">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Remaining" HeaderText="Remaining" ReadOnly="True" SortExpression="Remaining">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Pass" HeaderText="Pass" ReadOnly="True" SortExpression="Pass">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>    
                                <asp:BoundField DataField="Fail" HeaderText="Fail" ReadOnly="True" SortExpression="Fail">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>     
                                
                                <asp:BoundField DataField="TBD" HeaderText="TBD" ReadOnly="True" SortExpression="TBD">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="NT" HeaderText="N/T" ReadOnly="True" SortExpression="NT">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="NA" HeaderText="N/A" ReadOnly="True" SortExpression="NA">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="PassRate" HeaderText="Pass Rate" ReadOnly="True" SortExpression="PassRate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="FailRate" HeaderText="Fail Rate" ReadOnly="True" SortExpression="FailRate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="TBDRate" HeaderText="TBD Rate" ReadOnly="True" SortExpression="TBDRate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="ExecutionRate" HeaderText="Execution Rate" ReadOnly="True" SortExpression="ExecutionRate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  

                                <asp:BoundField DataField="NARate" HeaderText="N/A Rate" ReadOnly="True" SortExpression="NARate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="NTRate" HeaderText="N/T Rate" ReadOnly="True" SortExpression="NTRate">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" ReadOnly="True" SortExpression="TOTAL">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                                                                                                                                                                                                                         
                                
<%--                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
</asp:Content>

