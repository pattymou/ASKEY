<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="StatisticsCase.aspx.cs" Inherits="WebForm_StatisticsCase" Title="" %>

<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>    
                    
                    <asp:Label ID="Label1" runat="server" Text="日期區間："></asp:Label>
                    <asp:Label ID="Label6" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearS" runat="server" Width="75px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonthS" runat="server">
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
                    <asp:Label ID="Label2" runat="server" Text="月～(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearE" runat="server"　Width="75px"></asp:TextBox>
                    
                    <asp:Label ID="Label4" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonthE" runat="server">
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
                    <asp:Label ID="Label5" runat="server" Text="月"></asp:Label>     

                    
                       
                </td>
                
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label7" runat="server" Text="Team："></asp:Label>
                    
                    <asp:DropDownList ID="ddlTeam" runat="server">
                    </asp:DropDownList>
                    
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                       
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    
                </td>
            </tr>
            <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalCase" HeaderText="總案件" ReadOnly="True" SortExpression="TotalCase">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="Complete" HeaderText="已完成" ReadOnly="True" SortExpression="Complete">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NComplete" HeaderText="未完成" ReadOnly="True" SortExpression="NComplete">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                               
<%--                                <asp:BoundField DataField="Delay" HeaderText="Delay" ReadOnly="True" SortExpression="Delay">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> --%>                           

                                <asp:BoundField DataField="Percent" HeaderText="完成率" ReadOnly="True" SortExpression="Percent">
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
        </table> 
        <table >
            <asp:Chart ID="Chart_Complete" runat="server" Height="800px" Width="1000px" 
                EnableViewState="True" onclick="Chart_Complete_Click" >
                <Legends>
                    <asp:Legend DockedToChartArea="ChartArea2" Enabled="false" 
                        IsDockedInsideChartArea ="False" Name="Legend1" Alignment="Center" 
                        Docking="Bottom">
                    </asp:Legend>
                    <asp:Legend DockedToChartArea="ChartArea1" Enabled="False" Name="Legend2">
                    </asp:Legend>
                </Legends>
                <series>
                    <asp:Series Name="Average" ChartArea="ChartArea1" ChartType="Pie" 
                        CustomProperties="DrawingStyle=Cylinder, PieLabelStyle=Outside" 
                        Label="#VALX (#PERCENT)" PostBackValue="#INDEX,#VALX" Legend="Legend2">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea2" ChartType="StackedColumn" 
                        CustomProperties="DrawingStyle=Cylinder" Legend="Legend1" Name="NComplete" 
                        Label="#VAL" LabelForeColor="White" LegendText="未完成案件">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea2" ChartType="StackedColumn" 
                        CustomProperties="DrawingStyle=Cylinder" Name="Complete" Legend="Legend1" 
                        Label="#VAL" LabelForeColor="White" LegendText="完成案件">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                        <Area3DStyle Enable3D="True" Inclination="20" />
                    </asp:ChartArea>
                    <asp:ChartArea Name="ChartArea2">
                        <AxisX IntervalAutoMode="VariableCount" IntervalOffset="1" 
                            IsLabelAutoFit="False">
                            <MajorGrid IntervalOffset="1" />
                            <LabelStyle IsStaggered="True" />
                        </AxisX>
                        <AxisX2 IntervalAutoMode="VariableCount" IntervalOffset="1" 
                            IsLabelAutoFit="False">
                            <LabelStyle IsStaggered="True" />
                        </AxisX2>
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>        
        </table>
    </fieldset> 
</asp:Content>

