<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage_A.aspx.cs" Inherits="WebForm_HomePage_A" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
        <tr>
            <td align ="center" >
                <asp:Label ID="Label1" runat="server" Text="~~~申請驗証測試前, 請務必與以下窗口排訂時程, 並確認您的機種到達時間, 再提出申請, 避免造成專案延誤及資源使用~~~" 
                    Font-Bold="True" Font-Names="標楷體" Font-Size="XX-Large" Font-Underline="True" 
                    ForeColor="#FF3300"></asp:Label>

                

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" 
                    
                    Text="* SmallCell相關產品:" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" 
                    
                    Text="Max lin(林重宏_Askey_TW) / 分機 16870" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" 
                    
                    Text="無線相關產品:" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003300" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label7" runat="server" 
                    
                    Text="Kelvin Wang (王書偉_Askey_TW) / 分機 17596, 實驗室分機 18915 / 17059" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003300" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" 
                    
                    Text="* LTE相關產品:" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label8" runat="server" 
                    
                    Text="Marcos Chang (張浩銘_Askey_TW) / 分機 18267, 實驗室分機 17553" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" 
                    Text="* 吳江部門主管請連絡: " Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003300" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label9" runat="server" 
                    
                    Text="Kandi Xu (徐文華_Askey_WJ) / 分機 10418, 實驗室分機 10422" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003300" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" 
                    Text="* 台北部門主管: " Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label11" runat="server" 
                    
                    Text="Sam Chien (簡光賢_Askey_TW) / 分機 18707" Font-Names="標楷體" 
                    Font-Size="X-Large" ForeColor="#003399" Font-Bold="True"></asp:Label>
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

