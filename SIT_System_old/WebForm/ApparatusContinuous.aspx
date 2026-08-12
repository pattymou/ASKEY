<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApparatusContinuous.aspx.cs" Inherits="WebForm_ApparatusContinuous" %>

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
            <td align ="center">
                                    
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
<%--                            <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>   
                                                     
                            <asp:BoundField DataField="Brand" HeaderText="廠商" ReadOnly="True" SortExpression="Brand">
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
                            
                            <asp:BoundField DataField="StartDate" HeaderText="借用日期" ReadOnly="True" SortExpression="StartDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>   
                            <asp:BoundField DataField="EndDate" HeaderText="預計歸還日期" ReadOnly="True" SortExpression="EndDate">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>                                                      

                            <asp:BoundField DataField="Borrower" HeaderText="借用人" ReadOnly="True" SortExpression="Borrower">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Ext" HeaderText="借用人分機" ReadOnly="True" SortExpression="Ext">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField> 
                              

                            
                           
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
                <asp:Label ID="lblKind" runat="server" Text="" Visible="False"></asp:Label> 
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
            <td>
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
            <td rowspan=4 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="申請者資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label8" runat="server" Text="姓名：" ForeColor="Black"></asp:Label>
                
                <asp:Label ID="lblCName" runat="server"></asp:Label>
                
                <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Sub-PU：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>*--%>
                
                <%--<asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>*--%>
                
            </td>
            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="分機：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblExt" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtExt" runat="server"></asp:TextBox>*--%>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Email：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtEmail" runat="server" Width="257px"></asp:TextBox>*--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="任務名稱：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblMission" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtMission" runat="server"></asp:TextBox>*--%>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="機種名稱：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblGName" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtGName" runat="server" Width="257px"></asp:TextBox>*--%>
            </td>
        </tr> 
        <tr>
            <td colspan =2>
                <asp:Label ID="Label1" runat="server" Text="客戶："></asp:Label>
                <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>       
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="原歸還日期"></asp:Label>
                
            </td> 
            <td>
                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblID" runat="server"></asp:Label>
                <asp:Label ID="lblAID" runat="server"></asp:Label>
                <%--<asp:Label ID="lblEndDate" runat="server"></asp:Label>--%>
            </td>            
                     
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="預計歸還日期"></asp:Label>
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
                </asp:DropDownList>--%>
                
                <%--<asp:Label ID="Label15" runat="server" Text="："></asp:Label>
                
                <asp:DropDownList ID="ddlMinR" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>             
                &nbsp;&nbsp;&nbsp;&nbsp;
                (預約天數上限為工作日7天)
            </td>           
        </tr> 
        <%--<tr>
            <td>
                <asp:Label ID="Label17" runat="server" Text="預約時段"></asp:Label>
            </td>
            <td colspan =2>
                 <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>               

            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Text="使用類型"></asp:Label>
            </td>
            <td colspan =2>
                <asp:Label ID="lblUseKind" runat="server" Text=""></asp:Label>
                
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

