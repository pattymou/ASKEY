<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Certification_BT.aspx.cs" Inherits="WebForm_Certification_BT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bluetooth</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td align ="center">
                    <asp:Label ID="Label12" runat="server" Text="BLUETOOTH SIG TESTING" 
                        Font-Bold="True" Font-Size="X-Large"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td align ="center">
                    <asp:Label ID="Label13" runat="server" Text="Quotation Estimation" 
                        Font-Bold="True" Font-Size="Large"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <%--<td>
                    <asp:Label ID="Label21" runat="server" Text="Product Category"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProductCategory" runat="server">
                    </asp:DropDownList>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="BT Version"></asp:Label>
                    <asp:Label ID="Label16" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlVersion" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Core Mode"></asp:Label>
                    <asp:Label ID="Label17" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCoreMode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 23%">
                    
                    <asp:Label ID="Label33" runat="server" Text="Birefly describe BT function in the DUT"></asp:Label>
                    <asp:Label ID="Label18" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                    
                </td>
                <td colspan=2>
                   
                    <asp:TextBox ID="txtBriefly" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                <td style="width: 23%">
                    
                    <asp:Label ID="Label3" runat="server" Text="Application Profiles supported by DUT"></asp:Label>
                    <asp:Label ID="Label19" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                    
                </td>
                <td colspan=2>
                   
                    <asp:TextBox ID="txtApplication" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox><br />
                   <asp:Label ID="Label14" runat="server" Text="(Ex：A2DP,AVRCP,GAVDP,HFP,PAN,HOGP...)" 
                        ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label5" runat="server" Text="BT Qualified Design Information"></asp:Label>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td align ="center">
                    <asp:Label ID="Label6" runat="server" Text="Design"></asp:Label>
                </td>
                <%--<td align ="center">
                    <asp:Label ID="Label7" runat="server" Text="Model Name"></asp:Label>
                </td>--%>
                <td align ="center">
                    <asp:Label ID="Label8" runat="server" Text="Vendor"></asp:Label>
                </td>
                <td align ="center">
                    <asp:Label ID="Label9" runat="server" Text="DID / QDID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Controller Subsystem"></asp:Label>
                    <asp:Label ID="Label20" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtController_Vendor" runat="server" Width ="100%"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtController_DID" runat="server" Width ="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Host Subsystem"></asp:Label>
                    <asp:Label ID="Label21" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHost_Vendor" runat="server" Width ="100%"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtHost_DID" runat="server" Width ="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Component"></asp:Label>
                    <asp:Label ID="Label22" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtComponent_Vendor" runat="server" Width ="100%"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtComponent_DID" runat="server" Width ="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="End Product"></asp:Label>
                    <asp:Label ID="Label23" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEnd_Vendor" runat="server" Width ="100%"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtEnd_DID" runat="server" Width ="100%"></asp:TextBox>
                </td>
            </tr>
             
        </table>
        <table  width ="100%">
            <tr>
                <td align ="center">
                    <asp:Label ID="Label15" runat="server" Text="若無資料，請輸入none" 
                        ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
        </table>
        <table width ="100%">   
             
                    <tr>
                        <td align ="center" colspan = 2 style="COLOR: red">
                            <br />
                                
                            <asp:Button ID="butOK" runat="server" Text="確定" 
                onclick="butOK_Click" style="height: 21px" />
                                
                            <br />
                            <br />
                        </td>
                    </tr>                    
                </table>
    </div>
    </form>
</body>
</html>
