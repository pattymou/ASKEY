<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoardSList.aspx.cs" Inherits="WebForm_DashBoardSList" Title="" %>

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
                headerrowcount: 1 
            }); 
        } 
    </script>
    <fieldset>
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="客戶代碼："></asp:Label>
                    <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label>
                </td>
            </tr>
<%--            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="部門："></asp:Label>
                    <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>                    
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="專案名稱："></asp:Label>
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="NPI："></asp:Label>
                    <asp:Label ID="lblNPI" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="TestCase："></asp:Label>
                    <asp:Label ID="lblTestCase" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align ="center">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" GridLines="None"
                            Width="100%" AllowPaging="True" OnRowDataBound="gvwMain_RowDataBound" OnPageIndexChanging="gvwMain_PageIndexChanging">
                            
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="申請單編號" ReadOnly="True" SortExpression="ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                             
                                <asp:BoundField DataField="PCB_Version" HeaderText="H/W" ReadOnly="True" SortExpression="PCB_Version">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="FW_Version" HeaderText="S/W" ReadOnly="True" SortExpression="FW_Version">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="NPI" HeaderText="NPI" ReadOnly="True" SortExpression="NPI">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> --%>
                                <asp:BoundField DataField="End_Date" HeaderText="完成日" ReadOnly="True" SortExpression="End_Date">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Result" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                            Target="_blank" Text='<%# Bind("Result") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq1" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />                            
<%--                            <RowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#DFDDD9" Font-Bold="True" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="White" />--%>
                        </asp:GridView>
                    <table id="Table3" class="one" width="100%">
                        <tr>
                            <td align =center>
                                
                                <%--<asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                                    onclick="btnExcel_Click" />--%>
                      
                                
                            </td>
                        </tr>     
                    </table>                                    
                </td>
            </tr>
        </table> 
    </fieldset> 
</asp:Content>

