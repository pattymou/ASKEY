<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage_N1.aspx.cs" Inherits="WebForm_HomePage_N1" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
        <tr>
            <td align ="center">
                <asp:Label ID="Label1" runat="server" Text="【設備使用原則】" Font-Bold="True" Font-Names="標楷體" Font-Size="XX-Large" Font-Underline="True" ForeColor="#003399"></asp:Label>            
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" 
                    Text="1. 固定設備借用時，寬頻無線處(DA40)人員將不定時抽查設備使用狀況，如發現預約後未使用且未取消者!將通知單位主管協助改善。預約者違規記錄達三次者，將請系統管理員取消該預約人此年度之申請資格。" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" 
                    Text="2. 請按正常操作使用設備並保持外觀整潔。" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" 
                    Text="3. 歸還設備時，與設備保管人確認設備是否依借出時所提供的標準配備及附屬設備歸還後，再於系統上進行歸還作業。" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>                
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" 
                    Text="4. 設備借出期間遺失或歸還檢查時發現有損壞及異常，零附件遺失等情況發生，設備借用人依下列情事賠償損失:" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#FF3300" Font-Bold="True"></asp:Label>                
            </td>
        </tr>    
        <tr>
            <td align ="center">
                <asp:Label ID="Label6" runat="server" 
                    Text="a. 損壞: 借用人/借用單位負買回零附件/設備維修金額之責任" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#FF3300" Font-Bold="True"></asp:Label>                
            </td>
        </tr> 
        <tr>
            <td align ="center">
                <asp:Label ID="Label7" runat="server" 
                    Text="b. 遺失: 借用人/借用單位負買回零附件/設備之責任　　　　" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#FF3300" Font-Bold="True"></asp:Label>                
            </td>
        </tr>
        <tr>
            <td align ="center">
                <asp:Label ID="Label8" runat="server" 
                    Text="c. 報廢: 設備財產轉回該借出人/借出單位進行報廢/除帳作業" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#FF3300" Font-Bold="True"></asp:Label>                
            </td>
        </tr> 
        <tr>
            <td align ="center">
                
                <asp:CheckBox ID="chkOK" runat="server" Text="我已瞭解使用規則" Font-Size="Large" />
                
            </td>
        </tr> 
        <tr>
            <td align ="center">
                
                <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                
            </td>
        </tr>                                
    </table> 
</asp:Content>

