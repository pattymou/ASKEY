<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="DashBoardDetail.aspx.cs" Inherits="WebForm_DashBoardDetail" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <table id="Table2" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblModify" runat="server" OnClick="lbtnModify_Click">[修改此專案]</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblID" runat="server" ForeColor="#3333FF" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <table id="Table1" class="one" style="border: 1px solid" cellpadding="5" cellspacing="5"
            frame="border" rules="all" width="100%">
            <tr>
                <td width="25%">
                    <asp:Label ID="Label7" runat="server" Text="申請人"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblName" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="Label10" runat="server" Text="部門"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblDepartment" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="分機"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExt" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Mail"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMail" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="客戶"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCustomer" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                 <td width="25%">
                    <asp:Label ID="Label31" runat="server" Text="申請部門"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblDepartment2" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="S/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="PM Sales"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPM" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Mechanical Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMechanical" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="H/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="F/W Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="DSP Model"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDSP" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Customer's Product Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProduct" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Wireless Drive"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWireless" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="H/W Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHW_VR" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="NPI"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNPI" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="Sample MAC Address"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMAC" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="Chipset"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblChipset" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="開始日期"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStart" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="Utility Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUtility" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
             <td>
                    <asp:Label ID="Label20" runat="server" Text="預計完成日"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExpect" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="預計Sample Ready日期"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblReady" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="指派工程師"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEngineer" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="進度"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProgress" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="相關人員"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblRelated" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label35" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="578px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblCID" runat="server" Text="" Font-Bold="True" Font-Size="Large"
                        ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="子任務名稱"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCaseName" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="子任務ID"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCaseID" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="指派工程師"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAssign" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="狀態"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="開始日期"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStartdate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label30" runat="server" Text="預計完成日"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEnddate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label32" runat="server" Text="結果判定"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label33" runat="server" Text="進度"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCaseProgress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label36" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtCaseNote" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="578px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
