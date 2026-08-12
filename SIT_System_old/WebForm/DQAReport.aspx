<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DQAReport.aspx.cs" Inherits="WebForm_DQAReport" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>    
    <script type="text/javascript" src="../js/GridViewScroll/gridviewScroll.min.js"></script>
    <script type="text/javascript"> 
         $(document).ready(function () { 
            gridviewScroll(); 
     
            $(window).resize(function () 
            { 
                gridviewScroll(); 
            }); 
        });  
     
        function gridviewScroll() { 
            var GridView = document.getElementById('<%=this.gvwMain.ClientID %>');
            $(GridView).gridviewScroll({ 
                width: gridWidth, 
                height: gridHeight, 
//                width: 660, 
//                height: 200,
                freezesize: 0, 
                headerrowcount: 4 
            }); 
        } 
    </script>
    <fieldset>
         <table id="Table2" runat =server class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="客戶代碼："></asp:Label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
                        
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="機種名稱："></asp:Label>
                    <asp:DropDownList ID="ddlName" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>  
            <tr>
                <td>
                    <%--<asp:Label ID="Label3" runat="server" Text="NPI："></asp:Label>
                    <asp:DropDownList ID="ddlNPI" runat="server">
                    </asp:DropDownList>--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" 
                        Height="31px" Width="58px" />
                </td>
            </tr>           
            <tr>
                <td align ="center">
                    <asp:GridView ID="gvwMain" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        OnRowCreated="gvwMain_RowCreated" OnRowDataBound="gvwMain_RowDataBound" OnPreRender ="gvwMain_PreRender">                    
                            <Columns>
                                <%--<asp:BoundField DataField="Kind" HeaderText="Category" ReadOnly="True" SortExpression="Kind">
                                    <ControlStyle Width="500"></ControlStyle>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                </asp:BoundField>--%> 
                                <asp:BoundField DataField="Function" HeaderText="Function" ReadOnly="True" SortExpression="Function">
                                    <%--<ControlStyle Width="500"></ControlStyle>--%>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False"/>
                                </asp:BoundField> 
                                <asp:BoundField DataField="Item" HeaderText="Test Item" ReadOnly="True" SortExpression="Item">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <HeaderStyle  />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>      
                                <asp:BoundField DataField="NPI" HeaderText="NPI" ReadOnly="True" SortExpression="NPI">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <HeaderStyle  />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:BoundField>                       
                                <asp:TemplateField HeaderText="Result" SortExpression="HW">
                                    <%--<ControlStyle Width="30px" />--%>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N="+Eval("NPI")+"&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DQA" %>'
                                            Target="_blank" Text='<%# Bind("Result") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="" SortExpression="SW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N="+Eval("NPI")+"&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DQA" %>'
                                            Target="_blank" Text='<%# Bind("SW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="" SortExpression="Date">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N="+Eval("NPI")+"&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DQA" %>'
                                            Target="_blank" Text='<%# Bind("Date") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> --%>  
                                                                                                                                                                                                                                                                                                          
                                <asp:BoundField DataField="NPI" HeaderText="" ReadOnly="True" SortExpression="NPI" Visible =false>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                </asp:BoundField> 
                               
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

