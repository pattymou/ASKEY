<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="SearchDashBoardSummary.aspx.cs" Inherits="WebForm_SearchDashBoardSummary" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<link rel="stylesheet" href="../css/GridViewHeaderStyle.css">--%>
    
    
<%--    <link href="../css/GridViewHeaderStyle.css" rel="stylesheet" type="text/css" />
    <script src="../js/GridViewScroll/jquery-1.3.1.js"></script>
    <script src="../js/GridViewScroll/superTables.js"></script>
    <script src="../js/GridViewScroll/jquery.superTable.js"></script>
    <script type="text/javascript">
        $(function() {
            var GridView = document.getElementById('<%=this.gvwMain.ClientID %>');
//            var myObj = document.getElementById('" + this.gvwMain.ClientId + "');
//            alert(GridView.id);
            $(GridView).toSuperTable({ width: "640px", height: "480px", fixedCols: 4 })
            .find("tr:even").addClass("altRow");
        });
    </script>--%>   
    
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>--%> 
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
         <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <%--<tr>
                <td>
                    
                    <asp:TextBox ID="txtSearch" runat="server" Width="366px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />

                    <asp:Label ID="Label1" runat="server" Text=" (請輸入專案名稱)"></asp:Label>

                </td>
            </tr>
            
            <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwCheck" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwCheck_PageIndexChanging" onRowCommand="gvwCheck_RowCommand" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                 <asp:TemplateField HeaderText="專案名稱" Visible="True">

                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                                                                                          
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="選取" />
                                  </ItemTemplate> 
                                </asp:TemplateField>
                               
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
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Size="Large" 
                        ForeColor="Blue"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="客戶代碼："></asp:Label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="部門："></asp:Label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>            
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="機種名稱："></asp:Label>
                    <asp:DropDownList ID="ddlName" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                </td>
            </tr>
       </table>
       <table id="Table1" runat ="server" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">            
            <tr>
                <td align ="center">
                <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="200px" Width="300px" CssClass ="fixedHeader">                        --%>
                
                <%--<div style="overflow-y: scroll; height: 200px;width:300px" id="dvBody">--%>
                <%--<div id="div-gridview">--%>
                <%--<asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" Width="150%"
                             AllowPaging="True"  OnPreRender ="gvwMain_PreRender" 
                    OnRowDataBound="gvwMain_RowDataBound" OnRowCreated="gvwMain_RowCreated" PageSize="20">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                    <asp:GridView ID="gvwMain" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="Both" 
                        OnRowCreated="gvwMain_RowCreated" OnRowDataBound="gvwMain_RowDataBound" OnPreRender ="gvwMain_PreRender">                    
                            <Columns>
                                <asp:BoundField DataField="Function" HeaderText="" ReadOnly="True" SortExpression="Function">
                                    <%--<ControlStyle Width="500"></ControlStyle>--%>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False"/>
                                </asp:BoundField> 
                                <asp:BoundField DataField="Item" HeaderText="" ReadOnly="True" SortExpression="Item">
                                    <%--<ControlStyle Width="30px"></ControlStyle>--%>
                                    <HeaderStyle  />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundField>                            
                                <asp:TemplateField HeaderText="" SortExpression="DV_HW">
                                    <%--<ControlStyle Width="30px" />--%>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=DV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("DV_HW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="DV_SW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=DV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("DV_SW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="" SortExpression="DV_Date">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=DV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("DV_Date") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="" SortExpression="ES_HW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=ES&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("ES_HW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="" SortExpression="ES_SW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=ES&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("ES_SW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="ES_Date">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=ES&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("ES_Date") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="EV_HW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=EV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("EV_HW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="EV_SW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=EV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("EV_SW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="EV_Date">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=EV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("EV_Date") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="PV_HW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=PV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("PV_HW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="PV_SW">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=PV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("PV_SW") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="" SortExpression="PV_Date">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "DashBoardSList.aspx?N=PV&F="+Eval("Function")+"&I="+Eval("Item")+"&Kind=DA40" %>'
                                            Target="_blank" Text='<%# Bind("PV_Date") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                                                                                                                                                                                                                                                                          

                               
                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" ForeColor="Black" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />
<%--                            <RowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#DFDDD9" Font-Bold="True" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="White" />--%>
                        </asp:GridView>
                        <tr>
                            <td align =center>
                                
                                <asp:Button ID="btnExcel1" runat="server" Text="將表格匯出至Excel" 
                                    onclick="btnExcel1_Click" />   
                                    
                            </td>
                        </tr>
                        <%--</div>--%> 
                         
                        <%--</asp:Panel>--%>
                    <%--<table id="Table3" class="one" width="100%">
                        <tr>
                            <td align =center>
                                
                                <asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                                    onclick="btnExcel_Click" />
                      
                                
                            </td>
                        </tr>     
                    </table>--%>                                    
                </td>
            </tr>            
         </table> 
    </fieldset>
    
</asp:Content>

