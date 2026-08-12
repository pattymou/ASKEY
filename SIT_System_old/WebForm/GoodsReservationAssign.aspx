<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReservationAssign.aspx.cs" Inherits="WebForm_GoodsReservationAssign" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">

        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label2" runat="server" Text="貨品資訊"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="貨品名稱："></asp:Label>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>                
                <asp:Label ID="lblAID" runat="server" Text="Label"></asp:Label>
            </td>            
            <td>
                <asp:Label ID="Label3" runat="server" Text="廠商名稱："></asp:Label>
                <asp:Label ID="lblMF" runat="server" Text=""></asp:Label>                
            </td>

        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label6" runat="server" Text="料號："></asp:Label>
                <asp:Label ID="lblPart_No" runat="server" Text=""></asp:Label>                
            </td>
            <%--<td>
                <asp:Label ID="Label6" runat="server" Text="貨品保管人："></asp:Label>
                <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>                
            </td>--%>            
        </tr> 
     
        <tr>
            <td rowspan=3 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="申請者資訊"></asp:Label>
            </td>
            <td colspan =2>
                
                <asp:Label ID="Label8" runat="server" Text="姓名：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblBorrower" runat="server" Text=""></asp:Label>
                <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                
                
                
            </td>
            <%--<td>
                <asp:Label ID="Label9" runat="server" Text="部門：" ForeColor="Black"></asp:Label>
                <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                
            </td>--%>
            
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
                <asp:Label ID="Label12" runat="server" Text="領用日期"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblDateB" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDateR3" runat="server" Text="原建議更換日期"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblDateR2" runat="server" Text=""></asp:Label>
            </td>       
        </tr>         
        <tr>
            <td>
                <asp:Label ID="lblDateR1" runat="server" Text="建議更換日期"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblDateR" runat="server" Text=""></asp:Label>
            </td>       
        </tr>  
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="領用數量"></asp:Label>
            </td> 
            <td colspan =2>
                <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
<%--        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="備註"></asp:Label>
            </td>
            <td colspan =2>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
            </td>
        </tr>--%>
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

