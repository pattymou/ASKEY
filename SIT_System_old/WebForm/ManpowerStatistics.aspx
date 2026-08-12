<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="ManpowerStatistics.aspx.cs" Inherits="WebForm_ManpowerStatistics" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 

    
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <fieldset>
        <table id="Table3" class="one" width="100%" >
            <tr>
                <td colspan =2>
                    
                    <asp:Label ID="Label30" runat="server" Text="地點："></asp:Label>
                                        <asp:RadioButton ID="rdoLocal" runat="server" GroupName="6" Text="台北" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoLocal1" runat="server" GroupName="6" Text="吳江" />

                </td>
                
            </tr>
            <tr>
                <td>
                    
                    <asp:RadioButton ID="rdoReportM2" runat="server" GroupName="3" Text="M2" />
                </td>
            </tr>
            <tr>
                <td  colspan =2>    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="日期區間："></asp:Label>
                </td>
            </tr>

            <tr>
                <td  colspan =2>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoMonth" runat="server" GroupName="2" Text="月區間" />
                    &nbsp;&nbsp;&nbsp;
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
                    <asp:Label ID="lblCount" runat="server" Text="" Visible="False"></asp:Label>
                    <asp:Label ID="Label8" runat="server" Text="＊最多可選擇12個月" ForeColor="Red"></asp:Label>
                       
                </td>
                
            </tr>
            <tr>
                <%--<td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoReportM1" runat="server" GroupName="3" Text="M1" />
                </td>--%>
                <%--<td>
                    <asp:DropDownList ID="ddlTeam1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlTeam1_SelectedIndexChanged">
                    </asp:DropDownList>                                
                    <asp:DropDownList ID="ddlEmp1" runat="server"> </asp:DropDownList>                                             
                </td>--%>
            </tr>
            <%--<tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoReportM2" runat="server" GroupName="3" Text="M2" />
                </td>
            </tr>--%>
            <tr>
                <td colspan =2>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoWeek" runat="server" GroupName="2" Text="單月" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                     <script>
                         $(function() {
                             $("#datepicker").datepicker();
                         });
                     </script>--%> 
                     <asp:Label ID="Label13" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearA" runat="server" Width="75px"></asp:TextBox>
                    <asp:Label ID="Label14" runat="server" Text="年"></asp:Label>
                    <asp:DropDownList ID="ddlMonthA" runat="server">
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
                    <asp:Label ID="Label15" runat="server" Text="月"></asp:Label>                   
                </td>
            </tr>        
            <tr>
                <td style="width:120px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label7" runat="server" Text="Team"></asp:Label>
                </td> 
                <td>
                    <asp:DropDownList ID="ddlTeam" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlTeam_SelectedIndexChanged">
                    </asp:DropDownList>                                
                    <asp:DropDownList ID="ddlEmp" runat="server"> </asp:DropDownList>                                             
                </td>
            </tr>
            <tr>
                <td>
  
                    <asp:RadioButton ID="rdoReportM1" runat="server" GroupName="3" Text="M1" />
                </td>
                
            </tr>
            <tr>
                <td colspan =2>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label9" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearM1" runat="server"　Width="75px"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" Text="年"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="Label11" runat="server" Text="人員"></asp:Label>
                </td>
                <td>
                     
                    <asp:DropDownList ID="ddlTeam1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlTeam1_SelectedIndexChanged">
                    </asp:DropDownList>                                
                    <asp:DropDownList ID="ddlEmp1" runat="server"> </asp:DropDownList>                                             
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="butOK" runat="server" Text="搜尋" 
                            onclick="butOK_Click" />
                    <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="匯出Excel" runat="server" Text="搜尋" 
                            onclick="butOK1_Click" /> --%>       
                </td>
            </tr>
        </table>
        <asp:table ID="LineChart" runat ="server">                   
                
            
        </asp:table> 
        <asp:table ID="ColumnChart" runat ="server">                   
                
            
        </asp:table>
        <table id="Table1" class="one" width="100%" >
            <tr>
                <td>
                    <asp:GridView ID="gvwMain" runat="server" Width="100%" ShowFooter="true" 
                    AutoGenerateColumns="False" GridLines="None" 
                    OnRowCreated="gvwMain_RowCreated" OnRowDataBound="gvwMain_RowDataBound" OnPreRender ="gvwMain_PreRender">                    
                        <Columns>

                            <asp:BoundField DataField="Name" HeaderText="人員" ReadOnly="True" SortExpression="Name">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Project" HeaderText="Event" ReadOnly="True" SortExpression="Project">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C1" HeaderText="" ReadOnly="True" SortExpression="C1">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C2" HeaderText="" ReadOnly="True" SortExpression="C2">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>  
                            <asp:BoundField DataField="C3" HeaderText="" ReadOnly="True" SortExpression="C3">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C4" HeaderText="" ReadOnly="True" SortExpression="C4">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>   
                            <asp:BoundField DataField="C5" HeaderText="" ReadOnly="True" SortExpression="C5">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C6" HeaderText="" ReadOnly="True" SortExpression="C6">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C7" HeaderText="" ReadOnly="True" SortExpression="C7">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C8" HeaderText="" ReadOnly="True" SortExpression="C8">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C9" HeaderText="" ReadOnly="True" SortExpression="C9">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C10" HeaderText="" ReadOnly="True" SortExpression="C10">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C11" HeaderText="" ReadOnly="True" SortExpression="C11">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C12" HeaderText="" ReadOnly="True" SortExpression="C12">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Metric" HeaderText="Metric" ReadOnly="True" SortExpression="Metric">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                                                                         

                        </Columns>
                        <HeaderStyle CssClass="GridviewScrollHeader" /> 
                        <RowStyle CssClass="GridviewScrollItem" /> 
                        <PagerStyle CssClass="GridviewScrollPager" />

                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwMain1" runat="server" Width="100%" ShowFooter="true" 
                    AutoGenerateColumns="False" GridLines="None" 
                    OnRowCreated="gvwMain1_RowCreated" OnRowDataBound="gvwMain1_RowDataBound" OnPreRender ="gvwMain1_PreRender">                    
                        <Columns>

                            <asp:BoundField DataField="Name" HeaderText="人員" ReadOnly="True" SortExpression="Name">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Project" HeaderText="Event" ReadOnly="True" SortExpression="Project">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C1" HeaderText="1月" ReadOnly="True" SortExpression="C1">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C2" HeaderText="2月" ReadOnly="True" SortExpression="C2">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>  
                            <asp:BoundField DataField="C3" HeaderText="3月" ReadOnly="True" SortExpression="C3">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C4" HeaderText="4月" ReadOnly="True" SortExpression="C4">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>   
                            <asp:BoundField DataField="C5" HeaderText="5月" ReadOnly="True" SortExpression="C5">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C6" HeaderText="6月" ReadOnly="True" SortExpression="C6">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C7" HeaderText="7月" ReadOnly="True" SortExpression="C7">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C8" HeaderText="8月" ReadOnly="True" SortExpression="C8">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C9" HeaderText="9月" ReadOnly="True" SortExpression="C9">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C10" HeaderText="10月" ReadOnly="True" SortExpression="C10">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C11" HeaderText="11月" ReadOnly="True" SortExpression="C11">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C12" HeaderText="12月" ReadOnly="True" SortExpression="C12">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Metric" HeaderText="Metric" ReadOnly="True" SortExpression="Metric">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                                                                                         

                        </Columns>
                        <HeaderStyle CssClass="GridviewScrollHeader" /> 
                        <RowStyle CssClass="GridviewScrollItem" /> 
                        <PagerStyle CssClass="GridviewScrollPager" />

                    </asp:GridView>
                </td>
            </tr>
        </table>
        
    </fieldset> 
</asp:Content>

