<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyReservationStatus.aspx.cs" Inherits="WebForm_ModifyReservationStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="設備名稱："></asp:Label>
                    
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="財產編號："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProductID" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="設備保管人："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="借用日期："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="預計歸還日期："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="借用人："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBorrower" runat="server" Text=""></asp:Label>
                </td>
            </tr>   
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="借用部門："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="分機："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExt" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="E-mail："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="設備狀態："></asp:Label>
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>使用中</asp:ListItem>
                        <asp:ListItem>閒置中</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>   
    <tr>
        <td align ="center" colspan = 2 style="COLOR: red">
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

