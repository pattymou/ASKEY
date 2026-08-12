<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="PR_Statistics.aspx.cs" Inherits="WebForm_PR_Statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 

    
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    
       
    <style type="text/css">
    table {
            table-layout: fixed;
            word-break: break-all;
            }
    </style> 

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
                <td colspan =2>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label9" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearM1" runat="server"　Width="75px"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" Text="年"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="狀態："></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Close</asp:ListItem>
                        <asp:ListItem>Hold</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
        </table>
        <table>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="butOK" runat="server" Text="搜尋" 
                            onclick="butOK_Click" />
                </td>
            </tr>
        </table>
        <asp:table ID="LineChart" runat ="server" width="100%">                   
                
            
        </asp:table> 
        <asp:table ID="ColumnChart" runat ="server" width="100%">                   
                
            
        </asp:table>
        <table width="100%" >
            <tr>
                <td>
                    <div id="div1" style="overflow:scroll;width:100%;">
                    <asp:GridView ID="gvwMain" runat="server" ShowFooter="true" 
                    AutoGenerateColumns="False" 
                    OnRowCreated="gvwMain_RowCreated" OnRowDataBound="gvwMain_RowDataBound" OnPreRender ="gvwMain_PreRender">                    
                        <Columns>

                            
                            <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C1" HeaderText="1月" ReadOnly="True" SortExpression="C1">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C2" HeaderText="2月" ReadOnly="True" SortExpression="C2">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>  
                            <asp:BoundField DataField="C3" HeaderText="3月" ReadOnly="True" SortExpression="C3">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C4" HeaderText="4月" ReadOnly="True" SortExpression="C4">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>   
                            <asp:BoundField DataField="C5" HeaderText="5月" ReadOnly="True" SortExpression="C5">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C6" HeaderText="6月" ReadOnly="True" SortExpression="C6">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C7" HeaderText="7月" ReadOnly="True" SortExpression="C7">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C8" HeaderText="8月" ReadOnly="True" SortExpression="C8">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C9" HeaderText="9月" ReadOnly="True" SortExpression="C9">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C10" HeaderText="10月" ReadOnly="True" SortExpression="C10">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="C11" HeaderText="11月" ReadOnly="True" SortExpression="C11">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C12" HeaderText="12月" ReadOnly="True" SortExpression="C12">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                <HeaderStyle Wrap="False" />
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
                    </div> 
                </td>
            </tr>
        </table>
        
    </fieldset>
</asp:Content>

