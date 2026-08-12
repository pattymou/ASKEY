<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Certification_PTCRB.aspx.cs" Inherits="WebForm_Certification_PTCRB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <%--<tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Certification"></asp:Label>
                    <asp:Label ID="Label32" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>                  
                    <asp:RadioButton ID="rdoCertification" runat="server" Text="GCF" GroupName="1" AutoPostBack ="true" 
                        Width="300px" oncheckedchanged="rdoCertification_CheckedChanged"/>
                </td>
                <td>
                     <asp:RadioButton ID="rdoCertification1" runat="server" Text="PTCRB" AutoPostBack ="true"
                         GroupName="1" oncheckedchanged="rdoCertification1_CheckedChanged"/>  &nbsp;&nbsp;
                      
                                 
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Number of SIM slot"></asp:Label>
                    <asp:Label ID="Label19" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td colspan ="2">
                     <asp:TextBox ID="txtSIM" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr id="tIMEI" runat ="server" >
                <td rowspan=2>
                    <asp:Label ID="Label17" runat="server" Text="IMEI"></asp:Label>
                    <asp:Label ID="Label18" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>                 
                    <asp:RadioButton ID="rdoIMEI" runat="server" Text="Purchase New IMEI TAC" GroupName="5" 
                    /> &nbsp;&nbsp;
                </td>
                <td>
                    <asp:RadioButton ID="rdoIMEI1" runat="server" Text="Using module's TAC" GroupName="5" 
                    /> 
                    <br />
                    
                </td>
            </tr>
            <tr>
                <td colspan =2 id="tIMEI1" runat ="server">
                    <asp:Label ID="Label15" runat="server" Text="Note 1.using module's IMEI production cannot exceed 100,000 units, if exceed will need to purchase own IMEI."></asp:Label>
                    <br />
                    <asp:Label ID="Label16" runat="server" Text="Note 2.must inherit all module's capability."></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="VoLTE support"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>                 
                    <asp:RadioButton ID="rdoSupport" runat="server" Text="No" GroupName="2" 
                        Width="300px" />
                </td>
                <td>
                    <asp:RadioButton ID="rdoSupport1" runat="server" Text="Yes" GroupName="2" Width="300px"/>               
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Based on a Certified Module"></asp:Label>
                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>                   
                    <asp:RadioButton ID="rdoBase" runat="server" Text="No" GroupName="3" 
                        AutoPostBack ="true" oncheckedchanged="rdoBase_CheckedChanged"/> &nbsp;&nbsp;
                    </td>
                <td>
                    <asp:RadioButton ID="rdoBase1" runat="server" Text="Yes" GroupName="3" 
                        AutoPostBack ="true" oncheckedchanged="rdoBase1_CheckedChanged"/>  &nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="Module Number ："></asp:Label>
                    <asp:TextBox ID="txtModuleNumber" runat="server" Width ="200px"></asp:TextBox>  
                </td>
            </tr>
            <tr id="tInherits" runat ="server">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="DUT inherits all bands and CA from Module"></asp:Label>
                    <asp:Label ID="Label8" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td>                 
                    <asp:RadioButton ID="rdoInherits" runat="server" Text="No" GroupName="4" 
                    AutoPostBack ="true" oncheckedchanged="rdoInherits_CheckedChanged"/> &nbsp;&nbsp;
                    </td>
                <td>
                    <asp:RadioButton ID="rdoInherits1" runat="server" Text="Yes" GroupName="4" 
                    AutoPostBack ="true" oncheckedchanged="rdoInherits1_CheckedChanged"/>  &nbsp;&nbsp;
                    </td>
            </tr>
            
            <tr id = "tRAT_2G" runat ="server">
                <td rowspan=4 valign=middle>
                    <asp:Label ID="Label10" runat="server" Text="Supported RAT"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                </td>
                <td colspan=2>                 
                    <asp:CheckBox ID="chkRAT_2G" runat="server" Text="2G " AutoPostBack ="true"  
                        oncheckedchanged="chkRAT_2G_CheckedChanged" /> <br />
                    <asp:TextBox ID="txtRAT_2G" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                    <asp:Label ID="Label12" runat="server" Text="eq.850,900,1800,1900.." 
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id = "tRAT_3G" runat ="server">
                <td colspan=2>                 
                    <asp:CheckBox ID="chkRAT_3G" runat="server" Text="3G " AutoPostBack ="true"
                        oncheckedchanged="chkRAT_3G_CheckedChanged" /> <br />
                    <asp:TextBox ID="txtRAT_3G" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr id = "tRAT_4G" runat ="server">
                <td colspan=2>                 
                    <asp:CheckBox ID="chkRAT_4G" runat="server" Text="4G " AutoPostBack ="true"
                        oncheckedchanged="chkRAT_4G_CheckedChanged" /> <br />
                    <asp:TextBox ID="txtRAT_4G" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr id = "tRAT_5G" runat ="server">
                <td colspan=2>                 
                    <asp:CheckBox ID="chkRAT_5G" runat="server" Text="5G " AutoPostBack ="true"
                        oncheckedchanged="chkRAT_5G_CheckedChanged" /> <br />
                    <asp:TextBox ID="txtRAT_5G" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr id = "tCA_4G" runat ="server">
                <td rowspan=2 valign=middle>
                    <asp:Label ID="Label11" runat="server" Text="Supported CA"></asp:Label>
                    <%--<asp:Label ID="Label15" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>--%>
                </td>
                <td colspan=2>                 
                    <asp:CheckBox ID="chkCA_4G" runat="server" Text="4G " AutoPostBack ="true"
                        oncheckedchanged="chkCA_4G_CheckedChanged" /> <br />
                    <asp:TextBox ID="txtCA_4G" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr id = "tCA_5G" runat ="server">
                <td colspan=2>                 
                    <asp:CheckBox ID="chkCA_5G" runat="server" Text="5G " AutoPostBack ="true"
                        oncheckedchanged="chkCA_5G_CheckedChanged" /> <br />
                    <asp:TextBox ID="txtCA_5G" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr id = "tMR" runat ="server">
                <td rowspan=2 valign=middle>
                    <asp:Label ID="Label14" runat="server" Text="MR-DC"></asp:Label>
                    <%--<asp:Label ID="Label16" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>--%>
                </td>
                <td colspan=2>                 
                    <asp:CheckBox ID="chkMR" runat="server" Text="LTE+5G " AutoPostBack ="true"
                        oncheckedchanged="chkMR_CheckedChanged"/> <br />
                    <asp:TextBox ID="txtMR" runat="server" Width ="500px" TextMode="MultiLine" 
                        Height="50px"></asp:TextBox>
                </td>
            </tr>
        </table>
                </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="ddlPublish" 
                                EventName="SelectedIndexChanged" />--%>


                </Triggers>                    

        </asp:UpdatePanel>  
        <table  width ="100%">
            <tr>
                        <td align ="center">

                            <asp:Label ID="Label58" runat="server" 
                                Text="*為必填項目" Font-Bold="True" Font-Size="Large" 
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
        </table>
        <div align ="center" style="COLOR: red">
                    


                        
                        <asp:Button ID="butOK" runat="server" Text="確定" 
                                onclick="butOK_Click" Width="59px" Height="30px" />
                               
                        
                    <br />
                    <br />
        </div>
    </div>
    </form>
</body>
</html>
