<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage_N.aspx.cs" Inherits="WebForm_HomePage_N" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table width="100%">
            
        <tr>
            <td align ="center" >
                <asp:Label ID="Label1" runat="server" Text="設備預約使用規則~請詳讀以下內容並確實遵守以下守則" 
                    Font-Bold="True" Font-Names="標楷體" Font-Size="XX-Large" Font-Underline="True" 
                    ForeColor="#FF3300"></asp:Label>

                

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" 
                    Text="1. 設備預約時，務必依實際使用時數申請，勿超時預約，以提供實際需求同仁使用，避免浪費公共資源。" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="2. 系統於生效前一日再度發出確認通知信，" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label7" runat="server" Text="預約者" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>                    
                <asp:Label ID="Label3" runat="server" Text="或" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label9" runat="server" Text="其代理人" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>                 
                <asp:Label ID="Label10" runat="server" Text="必須於預約" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                
                <asp:Label ID="Label27" runat="server" Text="前一日 17:00 前" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label> 
                <asp:Label ID="Label28" runat="server" Text="回覆是否使用，如於規定時間內仍無回應，則系統" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>   
                <asp:Label ID="Label29" runat="server" Text="自動取消" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>  
                <asp:Label ID="Label30" runat="server" Text="該預約時段資格" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                                                      
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="3. 如遇設備預約臨時取消或" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label8" runat="server" Text="續借" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label13" runat="server" Text="，請務必" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label11" runat="server" Text="提前" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label15" runat="server" 
                    Text="通知系統管理員進行取消及續借通知，以利有需要的同仁順利預訂。" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="4. 借用人每次只能針對" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label12" runat="server" Text="同一台設備" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label> 
                <asp:Label ID="Label14" runat="server" Text="預約" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                                       
                <asp:Label ID="Label16" runat="server" Text="5個工作天" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label17" runat="server" 
                    Text="，該設備未歸還前無法進行預約;設備若" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label31" runat="server" Text="逾期" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label33" runat="server" Text="未歸還將" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                    
                <asp:Label ID="Label32" runat="server" Text="無法續借" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>  
                <asp:Label ID="Label34" runat="server" Text="，需" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label> 
                <asp:Label ID="Label35" runat="server" Text="歸還後" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" BackColor="Yellow" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label36" runat="server" Text="才可預約。" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                                                                                                   
                
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" 
                    Text="5. 預約完成後，系統會自動通知申請人及系統管理人員，系統管理人員如發現異常預約情況時，有權可取消預約(將先與預約人溝通確認後再行取消)。" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="6. 預約設備以當月預約為主。" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>  
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="7. 如有" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label21" runat="server" Text="連續10個工作天(含)" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="Red" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label22" runat="server" Text="以上需長時間借用或定期循環特定設備之情形，請於10個工作天前或預約後，" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label25" runat="server" Text="以email方式呈單位主管及RD最高主管" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="Red" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label26" runat="server" Text="(雷副總)同意後，知會系統管理員協助提前預約需求時段，始得同意。" 
                    Font-Names="標楷體" Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td align ="center">
                
                <asp:Button ID="btnNext" runat="server" Text="下一頁" onclick="btnNext_Click" />
                
            </td>
        </tr>                         

    </table>
</asp:Content>

