<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="BenchmarkStatistics.aspx.cs" Inherits="WebForm_BenchmarkStatistics" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <fieldset>
        <%--<table id="Table5" class="one" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="Test Case_1 - 802.11ac - 5G Tx Throughput Test ( 20MHz )"></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="lblAngle" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>

                <td align ="center">
                                        
                    
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle> 
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput1" HeaderText="" ReadOnly="True" SortExpression="Throughput1">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="Throughput2" HeaderText="" ReadOnly="True" SortExpression="Throughput2">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput3" HeaderText="" ReadOnly="True" SortExpression="Throughput3">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput4" HeaderText="" ReadOnly="True" SortExpression="Throughput4">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput5" HeaderText="" ReadOnly="True" SortExpression="Throughput5">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput6" HeaderText="" ReadOnly="True" SortExpression="Throughput6">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput7" HeaderText="" ReadOnly="True" SortExpression="Throughput7">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput8" HeaderText="" ReadOnly="True" SortExpression="Throughput8">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput9" HeaderText="" ReadOnly="True" SortExpression="Throughput9">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput10" HeaderText="" ReadOnly="True" SortExpression="Throughput10">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Throughput11" HeaderText="" ReadOnly="True" SortExpression="Throughput11">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                                                                                        

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
            <table >
                    <asp:Chart ID="Chart1" runat="server">
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>            
                    
            </table> 
        </table>--%>
        <table id="Table5" class="one" width="100%">
<%--            <tr>
                <td>
                    
                    <asp:Label ID="lblTitle" runat="server" Text="Test Case_1 - 802.11ac - 5G Tx Throughput Test ( 20MHz )"></asp:Label>
                    
                </td>
                
            </tr>--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Width = "100%">
                    
                    </asp:Panel>
                </td>
            </tr>        
        </table> 
        <table id="Table2" class="one" width="100%">
            <tr>
                <td align =center>
                    
                    <asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                        onclick="btnExcel_Click" />
                                               
                    
                </td>
            </tr>     
        </table>         
    </fieldset> 


</asp:Content>

