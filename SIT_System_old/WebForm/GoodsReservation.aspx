<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReservation.aspx.cs" Inherits="WebForm_GoodsReservation" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
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
                <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>
                
                <asp:DropDownList ID="ddlKind" runat="server">
<%--                    <asp:ListItem Value="0">ALL</asp:ListItem>--%>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                
                <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                <asp:Label ID="Label18" runat="server" Text="(貨品搜尋)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align ="center">
                                    
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand" OnRowDataBound ="gvwMain_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                                <%--<asp:BoundField DataField="Products_ID" HeaderText="財產編號" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>                                
                                <asp:BoundField DataField="MF_EN" HeaderText="廠商(英文)" ReadOnly="True" SortExpression="MF_EN">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="MF_CH" HeaderText="廠商(中文)" ReadOnly="True" SortExpression="MF_CH">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Name_En" HeaderText="貨品名稱(英文)" ReadOnly="True" SortExpression="Name_En">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Name_CH" HeaderText="貨品名稱(中文)" ReadOnly="True" SortExpression="Name_CH">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Custodian" HeaderText="保管人" ReadOnly="True" SortExpression="Custodian">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <%--<asp:BoundField DataField="" HeaderText="保管人分機" ReadOnly="True" SortExpression="">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="庫存" SortExpression="Quantity_Stock">                       
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity_Stock" runat="server" Text='<%#Eval("Quantity_Stock") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
<%--                                <asp:BoundField DataField="Quantity_Stock" HeaderText="庫存" ReadOnly="True" SortExpression="Quantity_Stock">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> --%>                                   
                                <%--<asp:BoundField DataField="Place" HeaderText="貨品位置" ReadOnly="True" SortExpression="Place">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> --%>  
                                 <asp:TemplateField HeaderText="貨品狀態" Visible="False">

                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                <asp:TemplateField HeaderText="使用期限" Visible="False">

                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Check_Date") %>'></asp:Label>
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
                <font face="新細明體" size="2">貨品借用資訊</font></td>
        </tr>

        <tr>
            <td rowspan=5 valign=middle>
                <asp:Label ID="Label2" runat="server" Text="貨品資訊"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="貨品名稱："></asp:Label>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>                
            </td>            
            <td>
                <asp:Label ID="Label3" runat="server" Text="廠商名稱："></asp:Label>
                <asp:Label ID="lblMF" runat="server" Text=""></asp:Label>                
            </td>

        </tr> 
        <tr>
            <td colspan =2>
                <asp:Label ID="Label5" runat="server" Text="料號："></asp:Label>
                <asp:Label ID="lblPart_No" runat="server" Text=""></asp:Label>                
            </td>
            <%--<td>
                <asp:Label ID="Label6" runat="server" Text="貨品保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td>--%>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label42" runat="server" Text="保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td>        
            <td>
                <asp:Label ID="Label44" runat="server" Text="保管人分機："></asp:Label>
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
                <asp:Label ID="Label21" runat="server" Text="備註："></asp:Label>
                <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>               
        <tr>
            <td rowspan=3 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="申請者資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label8" runat="server" Text="姓名："></asp:Label>
                
                <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="部門："></asp:Label>
                <%--<asp:Label ID="lblDepartment" runat="server"></asp:Label>--%>
                <%--<asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>*--%>
                
                <asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label23" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="分機："></asp:Label>
                <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
                <asp:Label ID="Label28" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Email："></asp:Label>
                <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtEmail" runat="server" Width="257px"></asp:TextBox>
                <asp:Label ID="Label29" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="任務名稱："></asp:Label>
                <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtMission" runat="server"></asp:TextBox>
                <asp:Label ID="Label30" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="機種名稱："></asp:Label>
                <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                <asp:TextBox ID="txtGName" runat="server" Width="257px"></asp:TextBox>
                <asp:Label ID="Label31" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>  
        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label22" runat="server" Text="代理人資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label24" runat="server" Text="姓名："></asp:Label>
                
                
                <asp:TextBox ID="txtAgent" runat="server"></asp:TextBox>
                <asp:Label ID="Label32" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="Label25" runat="server" Text="分機："></asp:Label>
                <asp:TextBox ID="txtAgentExt" runat="server"></asp:TextBox>
                <asp:Label ID="Label33" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
            
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label26" runat="server" Text="Email："></asp:Label>
                <asp:TextBox ID="txtAgentEmail" runat="server"></asp:TextBox>
                <asp:Label ID="Label36" runat="server" Text="*" ForeColor="Red"></asp:Label>                
            </td>
        </tr>              
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="領用日期"></asp:Label>
            </td> 
            
            <td colspan =2>                   
                <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                 <script>
                     $(function() {
                     $("#datepicker").datepicker();
                     });
                
                 </script>
<%--                <asp:DropDownList ID="ddlHourB" runat="server">
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
                <asp:Label ID="Label37" runat="server" Text="*" ForeColor="Red"></asp:Label>             
                
            </td>           
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="建議更換日期"></asp:Label>
            </td> 
            
            <td  colspan =2>  
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label> 
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDate" runat="server" Text="換算日期" 
                            onclick="btnDate_Click" /> 
                <asp:Label ID="lblDays" runat="server" Visible="False"></asp:Label>               
                <%--<input type="text" id="datepicker1" name = "date2" value = "<%=strStart1%>">
                 <script>
                     $(function() {
                         $("#datepicker1").datepicker();
                     });
                
                 </script>--%>
<%--                <asp:DropDownList ID="ddlHourR" runat="server">
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
                <asp:Label ID="Label38" runat="server" Text="*" ForeColor="Red"></asp:Label>             
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>           
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" Text="領用數量：" ForeColor="Black"></asp:Label>
                
            </td>
            <td>
                <asp:DropDownList ID="ddlCount" runat="server" >
<%--                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>--%>
                </asp:DropDownList>
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
            <td align ="center" colspan = 3 style="COLOR: red">
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

