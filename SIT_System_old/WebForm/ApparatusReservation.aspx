<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApparatusReservation.aspx.cs" Inherits="WebForm_ApparatusReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
 

<%--  <script src="//code.jquery.com/jquery-1.9.1.js"></script>
  <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>--%>
<%--  <link rel="stylesheet" href="//apps.bdimg.com/libs/jqueryui/1.10.4/css/jquery-ui.min.css">
  <script src="//apps.bdimg.com/libs/jquery/1.10.2/jquery.min.js"></script>
  <script src="//apps.bdimg.com/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>--%>
  
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
                <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>
                
                <asp:DropDownList ID="ddlKind" runat="server">
<%--                    <asp:ListItem Value="0">ALL</asp:ListItem>--%>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                
                <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                <asp:Label ID="Label18" runat="server" Text="(設備搜尋)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align ="center">
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width ="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
                            <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="Brand" HeaderText="廠牌" ReadOnly="True" SortExpression="Brand">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="型號" ReadOnly="True" SortExpression="Model">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="設備名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <%--<asp:BoundField DataField="Custodian_Department" HeaderText="保管部門" ReadOnly="True" SortExpression="Custodian_Department">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> --%>                           
                            
                            <asp:BoundField DataField="Custodian" HeaderText="保管人" ReadOnly="True" SortExpression="Custodian">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="保管人分機" ReadOnly="True" SortExpression="">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Place" HeaderText="設備位置" ReadOnly="True" SortExpression="Place">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ReservationStatus" HeaderText="設備狀態" ReadOnly="True" SortExpression="ReservationStatus">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>  
                             <asp:TemplateField HeaderText="設備狀態" Visible="False">

                                <ItemTemplate>
                                    <asp:Label ID="lblRStatus" runat="server" Text='<%# Bind("ReservationStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
                            
                            <%-- ==================0217--%>
<%--                            <asp:BoundField DataField="Status" HeaderText="設備狀態" ReadOnly="True" SortExpression="Status">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%> 
                            <%-- ==================0217--%>  
                            <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Button ID="btnASearch" runat="server" 
                                  CommandName="AddToCart1" 
                                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                  Text="預約明細" />
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
                <font face="新細明體" size="2">設備借用資訊</font></td>
        </tr>

        <tr>
            <td rowspan=5 valign=middle>
                <asp:Label ID="Label2" runat="server" Text="設備資訊"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="設備名稱："></asp:Label>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label> 
                <asp:Label ID="lblPrice" runat="server" Text="" Visible="False"></asp:Label>               
                <asp:Label ID="lblAID" runat="server" Text="" Visible="False"></asp:Label>
            </td>            
            <td>
                <asp:Label ID="Label3" runat="server" Text="財產編號："></asp:Label>
                <asp:Label ID="lblProductID" runat="server" Text=""></asp:Label>
            </td>

        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="廠牌："></asp:Label>
                <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>                
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="型號："></asp:Label>
                <asp:Label ID="lblModel" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>
        <%--<tr>
            <td colspan =2>
                <asp:Label ID="Label23" runat="server" Text="保管部門："></asp:Label>
                <asp:Label ID="lblCustodianD" runat="server" Text=""></asp:Label>                
            </td>        
            <td>
                <asp:Label ID="Label17" runat="server" Text="設備保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="Label42" runat="server" Text="保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td>        
            <td>
                <asp:Label ID="Label44" runat="server" Text="保管人分機："></asp:Label>
                <asp:Label ID="lblCustodianExt" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblCMail" runat="server" Visible="False"></asp:Label>                
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
                <asp:Label ID="lblAMail" runat="server" Visible="False"></asp:Label>              
            </td>            
        </tr>
         
        <tr>
            <td colspan =2>
                <asp:Label ID="Label21" runat="server" Text="備註："></asp:Label>
                <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>               
        <tr>
            <td rowspan=5 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="申請者資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label8" runat="server" Text="姓名："></asp:Label>
                
                <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Sub-PU："></asp:Label>
                <%--<asp:Label ID="lblDepartment" runat="server"></asp:Label>--%>
                <%--<asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>*--%>
                
                <asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>
       
                <asp:Label ID="Label28" runat="server" Text="填寫此開案機種名稱之Sub-PU代碼*" ForeColor="Red"></asp:Label>
                
            </td>
            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="分機："></asp:Label>
                <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
                <asp:Label ID="Label29" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Email："></asp:Label>
                <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtEmail" runat="server" Width="257px"></asp:TextBox>
                <asp:Label ID="Label30" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="任務名稱："></asp:Label>
                <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtMission" runat="server"></asp:TextBox>
                <asp:Label ID="Label31" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="機種名稱："></asp:Label>
                <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtGName" runat="server" Width="257px"></asp:TextBox>
                <asp:Label ID="Label17" runat="server" Text="* (範例：RTV1805VW)" ForeColor="Red"></asp:Label>
            </td>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="Label52" runat="server" Text="(機種名稱後面請勿加[客戶代碼]及[ROHS]！！)" 
                        ForeColor="Red" Font-Size="X-Large"></asp:Label>
                </td>            </tr>
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label40" runat="server" Text="客戶："></asp:Label>
                <asp:DropDownList ID="ddlCustomer" runat="server">
                </asp:DropDownList> 
                <asp:Label ID="Label41" runat="server" Text="*" ForeColor="Red"></asp:Label>               
            </td>
        </tr>   
        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label22" runat="server" Text="代理人資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label24" runat="server" Text="姓名："></asp:Label>
                
                
                <asp:TextBox ID="txtAgent" runat="server"></asp:TextBox>
                <asp:Label ID="Label33" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="Label25" runat="server" Text="分機："></asp:Label>
                <asp:TextBox ID="txtAgentExt" runat="server"></asp:TextBox>
                <asp:Label ID="Label36" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label26" runat="server" Text="Email："></asp:Label>
                <asp:TextBox ID="txtAgentEmail" runat="server"></asp:TextBox> 
                <asp:Label ID="Label37" runat="server" Text="*" ForeColor="Red"></asp:Label>               
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
                <%--<asp:DropDownList ID="ddlHourB" runat="server">

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

                </asp:DropDownList>
                
                <asp:Label ID="Label13" runat="server" Text="："></asp:Label>
                
                <asp:DropDownList ID="ddlMinB" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:Label ID="Label38" runat="server" Text="*" ForeColor="Red"></asp:Label>           
                
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
                <%--<asp:DropDownList ID="ddlHourR" runat="server">
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
                </asp:DropDownList>--%>
                <asp:Label ID="Label39" runat="server" Text="*" ForeColor="Red"></asp:Label>             
                &nbsp;&nbsp;&nbsp;&nbsp;
                (預約天數上限為7天)
            </td>           
        </tr>  
       <%-- <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="預約時段"></asp:Label>
            </td>
            <td colspan =2>
                
                <asp:RadioButton ID="rdoTime" runat="server" Text="白天" GroupName="0" />
                <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rdoTime1" runat="server" Text="晚上" GroupName="0" />
                <asp:Label ID="lblTime1" runat="server" Text=""></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="使用類型"></asp:Label>
            </td>
            <td colspan =2>
                
                <asp:RadioButton ID="rdoUse" runat="server" Text="手動測試" GroupName="1" />
                
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rdoUse1" runat="server" Text="自動化程式" GroupName="1" />
                
            </td>
        </tr>
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
            <td align ="center" colspan = 3 >
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

<%--        <tr>
            <td align ="center" colspan = 3 style="COLOR: red">
                    
                <asp:Button ID="butOK" runat="server" Text="確定" 
                            onclick="butOK_Click" />
                    
                <br />
                <br />
            </td>
        </tr>--%> 
        
           

</fieldset> 
</asp:Content>

