<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="SampleReservation.aspx.cs" Inherits="WebForm_SampleReservation" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
  
    <style>
        /* Adjust the jQuery UI widget font-size: */
        .ui-widget {
            font-size: 0.95em;
    }
    </style>
       
<fieldset>
    <table id="Table5" class="one" width="100%">
        <tr>
            <td>
               
                <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                <asp:Label ID="Label28" runat="server" Text="(樣品搜尋)"></asp:Label>
            </td>
        </tr>    
        <tr>
            <td align ="center">
                                    
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Number" HeaderText="編號" ReadOnly="True" SortExpression="Number">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Function_Name" HeaderText="功能" ReadOnly="True" SortExpression="Function_Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Item" HeaderText="項目" ReadOnly="True" SortExpression="Item">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="ModelName" HeaderText="Model Name" ReadOnly="True" SortExpression="ModelName">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Custodian" HeaderText="保管人" ReadOnly="True" SortExpression="Custodian">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="" HeaderText="保管人分機" ReadOnly="True" SortExpression="">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                                                
<%--                                <asp:BoundField DataField="Quantity_Stock" HeaderText="庫存" ReadOnly="True" SortExpression="Quantity_Stock">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> --%>                                   
                                <asp:BoundField DataField="Place" HeaderText="貨品位置" ReadOnly="True" SortExpression="Place">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                 <asp:TemplateField HeaderText="貨品狀態" Visible="True">

                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ReservationStatus") %>'></asp:Label>
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
                            
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
    </table> 
    <br />
    <br />
            <asp:Label ID="Label35" runat="server" Text="*" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label34" runat="server" Text="為必填欄位" ForeColor="Blue"></asp:Label>
    <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr style="font-size: 9pt">
            <td colspan =4 align="center" bgcolor="#dfe9f7" style="height: 27px">
                <font face="新細明體" size="2">樣品借用資訊</font></td>
        </tr>

        <tr>
            <td rowspan=8 valign=middle>
                <asp:Label ID="Label2" runat="server" Text="樣品資訊"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="編號："></asp:Label>
                <asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>                
            </td>            
            <td>
                <asp:Label ID="Label3" runat="server" Text="類別："></asp:Label>
                <asp:Label ID="lblKind" runat="server" Text=""></asp:Label>                
            </td>

        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="功能："></asp:Label>
                <asp:Label ID="lblFunction" runat="server" Text=""></asp:Label>                
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="項目："></asp:Label>
                <asp:Label ID="lblItem" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Category："></asp:Label>
                <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>                
            </td>
            <td>
                <asp:Label ID="Label17" runat="server" Text="Vendor："></asp:Label>
                <asp:Label ID="lblVendor" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label21" runat="server" Text="Model Name："></asp:Label>
                <asp:Label ID="lblModelName" runat="server" Text=""></asp:Label>                
            </td>
            <td>
                <asp:Label ID="Label23" runat="server" Text="MAC Address："></asp:Label>
                <asp:Label ID="lblMAC" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label25" runat="server" Text="PHY driver vesion："></asp:Label>
                <asp:Label ID="lblPHY" runat="server" Text=""></asp:Label>                
            </td>
            <td>
                <asp:Label ID="Label27" runat="server" Text="Firmware version："></asp:Label>
                <asp:Label ID="lblFirmware" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label29" runat="server" Text="保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td> 
            <td>
                <asp:Label ID="Label43" runat="server" Text="保管人分機："></asp:Label>
                <asp:Label ID="lblCustodianExt" runat="server" Text=""></asp:Label>                
            </td>           
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label46" runat="server" Text="保管代理人："></asp:Label>
                <asp:Label ID="lblAgent" runat="server" Text=""></asp:Label>                
            </td>        
            <td>
                <asp:Label ID="Label48" runat="server" Text="保管代理人分機："></asp:Label>
                <asp:Label ID="lblAgentExt" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label18" runat="server" Text="備註："></asp:Label>
                <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        
<%--        <tr>
            <td colspan =2>
                <asp:Label ID="Label21" runat="server" Text="備註："></asp:Label>
                <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>--%>               
        <tr>
            <td rowspan=3 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="申請者資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label8" runat="server" Text="姓名："></asp:Label>
                
                <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:Label ID="Label30" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="部門："></asp:Label>
                <%--<asp:Label ID="lblDepartment" runat="server"></asp:Label>--%>
                <%--<asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>*--%>
                
                <asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label31" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="分機："></asp:Label>
                <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
                <asp:Label ID="Label32" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Email："></asp:Label>
                <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtEmail" runat="server" Width="257px"></asp:TextBox>
                <asp:Label ID="Label33" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="任務名稱："></asp:Label>
                <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtMission" runat="server"></asp:TextBox>
                <asp:Label ID="Label36" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="機種名稱："></asp:Label>
                <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtGName" runat="server" Width="257px"></asp:TextBox>
                <asp:Label ID="Label37" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>  
        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label22" runat="server" Text="代理人資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label24" runat="server" Text="姓名："></asp:Label>
                
                
                <asp:TextBox ID="txtAgent" runat="server"></asp:TextBox>
                <asp:Label ID="Label38" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="Label71" runat="server" Text="分機："></asp:Label>
                <asp:TextBox ID="txtAgentExt" runat="server"></asp:TextBox>
                <asp:Label ID="Label39" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label26" runat="server" Text="Email："></asp:Label>
                <asp:TextBox ID="txtAgentEmail" runat="server"></asp:TextBox>
                <asp:Label ID="Label40" runat="server" Text="*" ForeColor="Red"></asp:Label>                
            </td>
        </tr>              
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="借用日期"></asp:Label>
            </td> 
            
            <td colspan =2>                   
                <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                 <script>
                     $(function() {
                     $("#datepicker").datepicker();
                     });
                
                 </script>
                <asp:DropDownList ID="ddlHourB" runat="server">
<%--                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>--%>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <%--<asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>--%>
                </asp:DropDownList>
                
                <asp:Label ID="Label13" runat="server" Text="："></asp:Label>
                
                <asp:DropDownList ID="ddlMinB" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label41" runat="server" Text="*" ForeColor="Red"></asp:Label>            
                
            </td>           
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="歸還日期"></asp:Label>
            </td> 
            
            <td colspan =2>                   
                <input type="text" id="datepicker1" name = "date2" value = "<%=strStart1%>">
                 <script>
                     $(function() {
                         $("#datepicker1").datepicker();
                     });
                
                 </script>
                <asp:DropDownList ID="ddlHourR" runat="server">
                    <asp:ListItem>00</asp:ListItem>
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
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                </asp:DropDownList>
                
                <asp:Label ID="Label15" runat="server" Text="："></asp:Label>
                
                <asp:DropDownList ID="ddlMinR" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label42" runat="server" Text="*" ForeColor="Red"></asp:Label>             
                &nbsp;&nbsp;&nbsp;&nbsp;
                (預約天數上限為5天)
            </td>           
        </tr> 
        <%--<tr>
            <td>
                <asp:Label ID="Label81" runat="server" Text="借用數量：" ForeColor="Black"></asp:Label>
                
            </td>
            <td>
                <asp:DropDownList ID="ddlCount" runat="server" >

                </asp:DropDownList>
            </td>
        </tr>--%> 
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="備註"></asp:Label>
            </td>
            <td colspan =2>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align ="center" colspan = 3>
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="確定" 
                            onclick="butOK_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;            
                <asp:Button ID="butReturn" runat="server" Text="上一頁" 
                            onclick="butReturn_Click" />                            
                            
                    
                <br />
                <br />
            </td>
        </tr>                     
    </table>       
           

</fieldset>
</asp:Content>

