<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ReservationAssign.aspx.cs" Inherits="WebForm_ReservationAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
<%--        <tr style="font-size: 9pt">
            <td colspan =4 align="center" bgcolor="#dfe9f7" style="height: 27px">
                <font face="新細明體" size="2">設備借用資訊</font></td>
        </tr>--%>

        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label2" runat="server" Text="設備資訊"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="設備名稱："></asp:Label>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>                
                <asp:Label ID="lblAID" runat="server" Text="Label"></asp:Label>
            </td>            
            <td>
                <asp:Label ID="Label3" runat="server" Text="財產編號："></asp:Label>
                <asp:Label ID="lblProductID" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblKind" runat="server" Text="Label" Visible="False"></asp:Label>
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
 <%--        <tr>
            <td colspan =2>
                <asp:Label ID="Label17" runat="server" Text="設備保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td>            
        </tr>--%>        
        <tr>
            <td rowspan=4 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="申請者資訊"></asp:Label>
            </td>
            <td>
                
                <asp:Label ID="Label8" runat="server" Text="姓名：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblBorrower" runat="server" Text=""></asp:Label>
                <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                
                
                
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Sub-PU：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                <%--<asp:Label ID="lblDepartment" runat="server"></asp:Label>--%>
                
            </td>
            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="分機：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblExt" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtExt" runat="server"></asp:TextBox>--%>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Email：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="任務名稱：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblMission" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtExt" runat="server"></asp:TextBox>--%>
            </td>
            <td>
                <asp:Label ID="Label15" runat="server" Text="機種名稱：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblGName" runat="server"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="客戶：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtExt" runat="server"></asp:TextBox>--%>
            </td>            
        </tr>
        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label13" runat="server" Text="申請代理人資訊"></asp:Label>
            </td>
            <td colspan =2>
                
                <asp:Label ID="Label14" runat="server" Text="姓名：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label>
                <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                
                
                
            </td>
            
            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="分機：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblAgentExt" runat="server"></asp:Label>
                <%--<asp:TextBox ID="txtExt" runat="server"></asp:TextBox>--%>
            </td>
            <td>
                <asp:Label ID="Label22" runat="server" Text="Email：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblAgentEmail" runat="server"></asp:Label>
                
            </td>
        </tr>
               
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="借用日期"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblDateB" runat="server" Text=""></asp:Label>
            </td>
            <%--<td  style="COLOR: red" colspan =2>                   
                <input type="text" id="datepicker" name = "date1">
                 <script>
                     $(function() {
                     $("#datepicker").datepicker();
                     });
                
                 </script>
                <asp:DropDownList ID="ddlHourB" runat="server">
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
                
                <asp:Label ID="Label13" runat="server" ForeColor="Black" Text="："></asp:Label>
                
                <asp:DropDownList ID="ddlMinB" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                </asp:DropDownList>*             
                
            </td>--%>           
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDateR3" runat="server" Text="原歸還日期"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblDateR2" runat="server" Text=""></asp:Label>
            </td>       
        </tr>         
        <tr>
            <td>
                <asp:Label ID="lblDateR1" runat="server" Text="歸還日期"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblDateR" runat="server" Text=""></asp:Label>
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
            
            <td align="center" colspan=2>
                <asp:RadioButton ID="rdoAccpt" runat="server" Text="Accept" GroupName="1" />
                
                 
            </td>
            <td align="center" colspan=2>
                <asp:RadioButton ID="rdoReject" runat="server" Text="Reject" GroupName="1" /> 
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
</asp:Content>

