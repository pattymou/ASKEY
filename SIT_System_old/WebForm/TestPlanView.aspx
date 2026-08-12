<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="TestPlanView.aspx.cs" Inherits="WebForm_TestPlanView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    
                    <asp:Label ID="Label17" runat="server" Text="類別："></asp:Label>
                    
                </td>
                <td>
                    <asp:Label ID="lblKind" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label1" runat="server" Text="客戶："></asp:Label>
                    
                </td>
                <td>
                    <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label8" runat="server" Text="機種名稱："></asp:Label>
                    
                </td>
                <td>
                    <asp:Label ID="lblP_Name" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label18" runat="server" Text="Requirement ID："></asp:Label>
                    
                </td>
                <td>
                    <asp:Label ID="lblRequirementID" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                
                    <asp:Label ID="Label2" runat="server" Text="Category："></asp:Label>
                
                </td>
                <td>
                    
                    <asp:TextBox ID="txtCategory" runat="server" TextMode="MultiLine" Width="400px" 
                        Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="Sub-Category："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtSubCategory" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label4" runat="server" Text="Purpose："></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtPurpose" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                </td>
            </tr> 
            
            <tr>
                <td>
                    
                    <asp:Label ID="Label112" runat="server" Text="Environment Setup："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtEnvironmentSetup" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            
            <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="Test Steps："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTestSteps" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label6" runat="server" Text="Expected Results："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtExpectedResults" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label7" runat="server" Text="Test Result："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtTestResult" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label9" runat="server" Text="Bug Ticket ID："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtBugTicketID" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label10" runat="server" Text="RD Comment："></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtRDComment" runat="server" TextMode="MultiLine" 
                        Width="400px" Height="83px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2 align=center>
                    
                    <asp:Button ID="btnOK" runat="server" Text="關閉" onclick="btnOK_Click" />
<%--                    &nbsp;&nbsp;&nbsp;&nbsp;
                    
                    <asp:Button ID="btnReturn" runat="server" Text="回上一頁" 
                        onclick="btnReturn_Click" />--%>
                    
                    
                </td>
            </tr>           
            
        </table> 
    </fieldset> 
</asp:Content>

