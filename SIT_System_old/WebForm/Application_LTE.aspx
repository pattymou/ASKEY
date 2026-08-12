<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Application_LTE.aspx.cs" Inherits="WebForm_Application_LTE" %>

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
        <div align ="center" style="COLOR: red">
                    


                        <br />
                        <asp:Button ID="butOK1" runat="server" Text="確定" 
                                onclick="butOK_Click" Width="59px" Height="30px" />
                               
                        
                    <br />
                    <br />
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text=""></asp:Label>                    
                </td>
                <td colspan = 4 align =center >
                    <asp:Label ID="Label6" runat="server" Text="LTE TRP"></asp:Label>
                </td>
                <td colspan = 4 align =center >
                    <asp:Label ID="Label7" runat="server" Text="LTE TIS"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Band"></asp:Label>                    
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Uplink Channel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Freq.(MHz)"></asp:Label>                    
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="BW-MHz"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Conductive Power"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Channel"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Freq.(MHz)"></asp:Label>                    
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="BW-MHz"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Conductive Sensitivity(dBm)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk1" runat="server" Text="1" AutoPostBack ="true"  
                        oncheckedchanged="chk1_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="18050"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="1925"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label18" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_1_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="50"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="2115"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label22" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_1_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label24" runat="server" Text="18300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="1950"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label26" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTRP_1_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="2140"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label30" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTIS_1_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label23" runat="server" Text="18550"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="1975"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label32" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTRP_1_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label34" runat="server" Text="550"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label35" runat="server" Text="2165"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label36" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTIS_1_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk2" runat="server" Text="2" AutoPostBack ="true"  
                        oncheckedchanged="chk2_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="18650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="1855"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label15" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_2_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="1935"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label33" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_2_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label37" runat="server" Text="18900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label38" runat="server" Text="1880"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label39" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTRP_2_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label40" runat="server" Text="900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label41" runat="server" Text="1960"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label42" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTIS_2_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label43" runat="server" Text="19150"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label44" runat="server" Text="1905"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label45" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTRP_2_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label46" runat="server" Text="1150"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label47" runat="server" Text="1985"></asp:Label>
                </td>
                <%--<td>
                    <asp:Label ID="Label48" runat="server" Text="10"></asp:Label>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtTIS_2_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk3" runat="server" Text="3" AutoPostBack ="true"  
                        oncheckedchanged="chk3_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label49" runat="server" Text="19250"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label50" runat="server" Text="1250"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label51" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_3_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label52" runat="server" Text="1250"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label53" runat="server" Text="1810"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label54" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_3_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label55" runat="server" Text="19575"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label56" runat="server" Text="1575"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_3_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label58" runat="server" Text="1575"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label59" runat="server" Text="1842.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_3_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label61" runat="server" Text="19900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label62" runat="server" Text="1900"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_3_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label64" runat="server" Text="1900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label65" runat="server" Text="1875"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_3_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk4" runat="server" Text="4" AutoPostBack ="true"  
                        oncheckedchanged="chk4_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="20000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label30" runat="server" Text="1715"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label32" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_4_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label36" runat="server" Text="2000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label39" runat="server" Text="2115"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label42" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_4_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label45" runat="server" Text="20175"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label48" runat="server" Text="1747.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_4_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label57" runat="server" Text="2175"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label60" runat="server" Text="2132.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_4_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label63" runat="server" Text="20350"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label66" runat="server" Text="1780"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_4_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label67" runat="server" Text="2350"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label68" runat="server" Text="2150"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_4_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk5" runat="server" Text="5" AutoPostBack ="true"  
                        oncheckedchanged="chk5_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label69" runat="server" Text="20450"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label70" runat="server" Text="829"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label71" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_5_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label72" runat="server" Text="2450"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label73" runat="server" Text="874"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label74" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_5_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label75" runat="server" Text="20525"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label76" runat="server" Text="836.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_5_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label77" runat="server" Text="2525"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label78" runat="server" Text="881.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_5_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label79" runat="server" Text="20600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label80" runat="server" Text="844"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_5_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label81" runat="server" Text="2600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label82" runat="server" Text="889"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_5_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk6" runat="server" Text="6" AutoPostBack ="true"  
                        oncheckedchanged="chk6_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                    <%--<asp:Label ID="Label83" runat="server" Text=" "></asp:Label>--%>
                </td>
                <td>
                    <asp:Label ID="Label84" runat="server" Text=" "></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label85" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="TextBox13" runat="server" Width ="200px"></asp:TextBox>--%>
                </td>
                <td>
                    <asp:Label ID="Label86" runat="server" Text=" "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label87" runat="server" Text=" "></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label88" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="TextBox14" runat="server" Width ="200px"></asp:TextBox>--%>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label89" runat="server" Text="20700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label90" runat="server" Text="835"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_6_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label91" runat="server" Text="2700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label92" runat="server" Text="880"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_6_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk7" runat="server" Text="7" AutoPostBack ="true"  
                        oncheckedchanged="chk7_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label83" runat="server" Text="20800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label93" runat="server" Text="2505"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label94" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_7_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label97" runat="server" Text="2800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label98" runat="server" Text="2625"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label99" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_7_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label100" runat="server" Text="21100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label101" runat="server" Text="2535"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_7_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label102" runat="server" Text="3100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label103" runat="server" Text="2655"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_7_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label104" runat="server" Text="21400"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label105" runat="server" Text="2565"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_7_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label106" runat="server" Text="3400"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label107" runat="server" Text="2685"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_7_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk8" runat="server" Text="8" AutoPostBack ="true"  
                        oncheckedchanged="chk8_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label108" runat="server" Text="21500"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label109" runat="server" Text="885"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label110" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_8_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label111" runat="server" Text="3500"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label112" runat="server" Text="930"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label113" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_8_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label114" runat="server" Text="21625"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label115" runat="server" Text="897.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_8_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label116" runat="server" Text="3625"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label117" runat="server" Text="942.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_8_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label118" runat="server" Text="21750"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label119" runat="server" Text="910"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_8_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label120" runat="server" Text="3750"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label121" runat="server" Text="955"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_8_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk9" runat="server" Text="9" AutoPostBack ="true"  
                        oncheckedchanged="chk9_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label122" runat="server" Text="21850"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label123" runat="server" Text="1754.9"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label124" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_9_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label125" runat="server" Text="3850"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label126" runat="server" Text="1849.9"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label127" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_9_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label128" runat="server" Text="21975"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label129" runat="server" Text="1767.4"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_9_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label130" runat="server" Text="3975"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label131" runat="server" Text="1862.4"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_9_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label132" runat="server" Text="22100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label133" runat="server" Text="1779.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_9_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label134" runat="server" Text="4100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label135" runat="server" Text="1875.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_9_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk10" runat="server" Text="10" AutoPostBack ="true"  
                        oncheckedchanged="chk10_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label136" runat="server" Text="22200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label137" runat="server" Text="1715"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label138" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_10_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label139" runat="server" Text="4200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label140" runat="server" Text="2115"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label141" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_10_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label142" runat="server" Text="22450"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label143" runat="server" Text="1740"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_10_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label144" runat="server" Text="4450"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label145" runat="server" Text="2140"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_10_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label146" runat="server" Text="22700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label147" runat="server" Text="1765"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_10_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label148" runat="server" Text="4700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label149" runat="server" Text="2165"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_10_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk11" runat="server" Text="11" AutoPostBack ="true"  
                        oncheckedchanged="chk11_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label95" runat="server" Text="22800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label96" runat="server" Text="1432.9"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label150" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_11_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label151" runat="server" Text="4800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label152" runat="server" Text="1480.9"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label153" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_11_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label154" runat="server" Text="22850"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label155" runat="server" Text="1437.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_11_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label156" runat="server" Text="4850"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label157" runat="server" Text="1485.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_11_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label158" runat="server" Text="22900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label159" runat="server" Text="1442.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_11_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label160" runat="server" Text="4900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label161" runat="server" Text="1490.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_11_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk12" runat="server" Text="12" AutoPostBack ="true"  
                        oncheckedchanged="chk12_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label162" runat="server" Text="23060"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label163" runat="server" Text="699.97"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label164" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_12_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label165" runat="server" Text="5060"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label166" runat="server" Text="734"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label167" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_12_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label168" runat="server" Text="23095"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label169" runat="server" Text="707.41"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_12_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label170" runat="server" Text="5095"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label171" runat="server" Text="737.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_12_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label172" runat="server" Text="23130"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label173" runat="server" Text="715.03"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_12_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label174" runat="server" Text="5130"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label175" runat="server" Text="741"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_12_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk13" runat="server" Text="13" AutoPostBack ="true"  
                        oncheckedchanged="chk13_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label178" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label181" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label182" runat="server" Text="23230"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label183" runat="server" Text="782"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_13_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label184" runat="server" Text="5230"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label185" runat="server" Text="751"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_13_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk14" runat="server" Text="14" AutoPostBack ="true"  
                        oncheckedchanged="chk14_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label176" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label177" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label179" runat="server" Text="23330"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label180" runat="server" Text="793"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_14_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label186" runat="server" Text="5330"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label187" runat="server" Text="763"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_14_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk17" runat="server" Text="17" AutoPostBack ="true"  
                        oncheckedchanged="chk17_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label188" runat="server" Text="23780"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label189" runat="server" Text="709"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label190" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_17_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label191" runat="server" Text="5780"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label192" runat="server" Text="709"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label193" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_17_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label194" runat="server" Text="23790"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label195" runat="server" Text="710"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_17_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label196" runat="server" Text="5790"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label197" runat="server" Text="710"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_17_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label198" runat="server" Text="23800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label199" runat="server" Text="711"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_17_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label200" runat="server" Text="5800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label201" runat="server" Text="711"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_17_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk18" runat="server" Text="18" AutoPostBack ="true"  
                        oncheckedchanged="chk18_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label202" runat="server" Text="23900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label203" runat="server" Text="820"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label204" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_18_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label205" runat="server" Text="5900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label206" runat="server" Text="820"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label207" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_18_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label208" runat="server" Text="23925"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label209" runat="server" Text="822.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_18_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label210" runat="server" Text="5925"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label211" runat="server" Text="822.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_18_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label212" runat="server" Text="23950"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label213" runat="server" Text="825"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_18_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label214" runat="server" Text="5950"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label215" runat="server" Text="825"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_18_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk19" runat="server" Text="19" AutoPostBack ="true"  
                        oncheckedchanged="chk19_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label216" runat="server" Text="24050"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label217" runat="server" Text="835"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label218" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_19_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label219" runat="server" Text="6050"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label220" runat="server" Text="835"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label221" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_19_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label222" runat="server" Text="24075"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label223" runat="server" Text="837.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_19_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label224" runat="server" Text="6075"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label225" runat="server" Text="837.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_19_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label226" runat="server" Text="24100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label227" runat="server" Text="840"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_19_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label228" runat="server" Text="6100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label229" runat="server" Text="840"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_19_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk20" runat="server" Text="20" AutoPostBack ="true"  
                        oncheckedchanged="chk20_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label230" runat="server" Text="24200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label231" runat="server" Text="837"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label232" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_20_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label233" runat="server" Text="6200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label234" runat="server" Text="837"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label235" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_20_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label236" runat="server" Text="24300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label237" runat="server" Text="847"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_20_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label238" runat="server" Text="6300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label239" runat="server" Text="847"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_20_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label240" runat="server" Text="24400"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label241" runat="server" Text="857"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_20_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label242" runat="server" Text="6400"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label243" runat="server" Text="857"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_20_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk21" runat="server" Text="21" AutoPostBack ="true"  
                        oncheckedchanged="chk21_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label244" runat="server" Text="24500"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label245" runat="server" Text="1452.9"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label246" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_21_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label247" runat="server" Text="6500"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label248" runat="server" Text="1500.9"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label249" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_21_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label250" runat="server" Text="24525"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label251" runat="server" Text="1455.4"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_21_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label252" runat="server" Text="6525"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label253" runat="server" Text="1503.4"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_21_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label254" runat="server" Text="24550"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label255" runat="server" Text="1457.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_21_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label256" runat="server" Text="6550"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label257" runat="server" Text="1505.9"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_21_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk22" runat="server" Text="22" AutoPostBack ="true"  
                        oncheckedchanged="chk22_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label258" runat="server" Text="24650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label259" runat="server" Text="3415"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label260" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_22_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label261" runat="server" Text="6650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label262" runat="server" Text="3515"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label263" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_22_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label264" runat="server" Text="25000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label265" runat="server" Text="3450"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_22_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label266" runat="server" Text="7000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label267" runat="server" Text="3550"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_22_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label268" runat="server" Text="25350"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label269" runat="server" Text="3485"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_22_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label270" runat="server" Text="7350"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label271" runat="server" Text="3585"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_22_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk23" runat="server" Text="23" AutoPostBack ="true"  
                        oncheckedchanged="chk23_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label272" runat="server" Text="25550"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label273" runat="server" Text="2005"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label274" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_23_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label275" runat="server" Text="7550"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label276" runat="server" Text="2185"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label277" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_23_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label278" runat="server" Text="25600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label279" runat="server" Text="2010"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_23_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label280" runat="server" Text="7600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label281" runat="server" Text="2190"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_23_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label282" runat="server" Text="25650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label283" runat="server" Text="2015"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_23_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label284" runat="server" Text="7650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label285" runat="server" Text="2195"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_23_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk24" runat="server" Text="24" AutoPostBack ="true"  
                        oncheckedchanged="chk24_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label286" runat="server" Text="25750"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label287" runat="server" Text="1631.5"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label288" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_24_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label289" runat="server" Text="7750"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label290" runat="server" Text="1530"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label291" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_24_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label292" runat="server" Text="25870"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label293" runat="server" Text="1643.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_24_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label294" runat="server" Text="7870"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label295" runat="server" Text="1542"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_24_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label296" runat="server" Text="25990"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label297" runat="server" Text="1655.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_24_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label298" runat="server" Text="7990"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label299" runat="server" Text="1554"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_24_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk25" runat="server" Text="25" AutoPostBack ="true"  
                        oncheckedchanged="chk25_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label300" runat="server" Text="26090"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label301" runat="server" Text="1855"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label302" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_25_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label303" runat="server" Text="8090"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label304" runat="server" Text="1935"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label305" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_25_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label306" runat="server" Text="26365"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label307" runat="server" Text="1882.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_25_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label308" runat="server" Text="8365"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label309" runat="server" Text="1962.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_25_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label310" runat="server" Text="26640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label311" runat="server" Text="1910"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_25_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label312" runat="server" Text="8640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label313" runat="server" Text="1990"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_25_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk26" runat="server" Text="26" AutoPostBack ="true"  
                        oncheckedchanged="chk26_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label314" runat="server" Text="26750"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label315" runat="server" Text="820"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label316" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_26_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label317" runat="server" Text="8750"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label318" runat="server" Text="865"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label319" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_26_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label320" runat="server" Text="26865"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label321" runat="server" Text="831.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_26_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label322" runat="server" Text="8865"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label323" runat="server" Text="876.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_26_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label324" runat="server" Text="26990"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label325" runat="server" Text="844"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_26_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label326" runat="server" Text="8990"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label327" runat="server" Text="889"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_26_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk27" runat="server" Text="27" AutoPostBack ="true"  
                        oncheckedchanged="chk27_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label328" runat="server" Text="27090"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label329" runat="server" Text="812"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label330" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_27_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label331" runat="server" Text="9090"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label332" runat="server" Text="857"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label333" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_27_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label334" runat="server" Text="27125"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label335" runat="server" Text="815.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_27_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label336" runat="server" Text="9125"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label337" runat="server" Text="862.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_27_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label338" runat="server" Text="27160"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label339" runat="server" Text="819"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_27_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label340" runat="server" Text="9160"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label341" runat="server" Text="864"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_27_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk28" runat="server" Text="28" AutoPostBack ="true"  
                        oncheckedchanged="chk28_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label342" runat="server" Text="27260"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label343" runat="server" Text="708"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label344" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_28_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label345" runat="server" Text="9260"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label346" runat="server" Text="763"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label347" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_28_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label348" runat="server" Text="27410"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label349" runat="server" Text="723"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_28_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label350" runat="server" Text="9410"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label351" runat="server" Text="778"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_28_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label352" runat="server" Text="27610"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label353" runat="server" Text="743"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_28_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label354" runat="server" Text="9610"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label355" runat="server" Text="798"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_28_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk30" runat="server" Text="30" AutoPostBack ="true"  
                        oncheckedchanged="chk30_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label358" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label361" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label362" runat="server" Text="27710"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label363" runat="server" Text="2310"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_30_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label364" runat="server" Text="9820"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label365" runat="server" Text="2355"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_30_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk31" runat="server" Text="31" AutoPostBack ="true"  
                        oncheckedchanged="chk31_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label356" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label357" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label359" runat="server" Text="277850"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label360" runat="server" Text="465"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_31_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label366" runat="server" Text="9895"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label367" runat="server" Text="466.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_31_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk33" runat="server" Text="33" AutoPostBack ="true"  
                        oncheckedchanged="chk33_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label368" runat="server" Text="36050"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label369" runat="server" Text="1905"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label370" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_33_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label371" runat="server" Text="36050"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label372" runat="server" Text="1905"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label373" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_33_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label374" runat="server" Text="36100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label375" runat="server" Text="1910"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_33_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label376" runat="server" Text="36100"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label377" runat="server" Text="1910"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_33_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label378" runat="server" Text="36150"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label379" runat="server" Text="1915"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_33_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label380" runat="server" Text="36150"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label381" runat="server" Text="1915"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_33_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk34" runat="server" Text="34" AutoPostBack ="true"  
                        oncheckedchanged="chk34_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label382" runat="server" Text="36250"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label383" runat="server" Text="2015"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label384" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_34_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label385" runat="server" Text="36250"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label386" runat="server" Text="2015"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label387" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_34_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label388" runat="server" Text="35275"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label389" runat="server" Text="2017.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_34_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label390" runat="server" Text="35275"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label391" runat="server" Text="2017.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_34_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label392" runat="server" Text="36300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label393" runat="server" Text="2020"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_34_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label394" runat="server" Text="36300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label395" runat="server" Text="2020"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_34_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk35" runat="server" Text="35" AutoPostBack ="true"  
                        oncheckedchanged="chk35_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label396" runat="server" Text="36400"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label397" runat="server" Text="1855"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label398" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_35_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label399" runat="server" Text="36400"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label400" runat="server" Text="1855"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label401" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_35_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label402" runat="server" Text="36650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label403" runat="server" Text="1880"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_35_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label404" runat="server" Text="36650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label405" runat="server" Text="1880"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_35_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label406" runat="server" Text="36900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label407" runat="server" Text="1905"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_35_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label408" runat="server" Text="36900"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label409" runat="server" Text="1905"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_35_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk36" runat="server" Text="36" AutoPostBack ="true"  
                        oncheckedchanged="chk36_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label410" runat="server" Text="37000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label411" runat="server" Text="1935"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label412" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_36_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label413" runat="server" Text="37000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label414" runat="server" Text="1935"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label415" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_36_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label416" runat="server" Text="37250"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label417" runat="server" Text="1960"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_36_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label418" runat="server" Text="37250"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label419" runat="server" Text="1960"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_36_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label420" runat="server" Text="37500"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label421" runat="server" Text="1985"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_36_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label422" runat="server" Text="37500"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label423" runat="server" Text="1985"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_36_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk37" runat="server" Text="37" AutoPostBack ="true"  
                        oncheckedchanged="chk37_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label424" runat="server" Text="37600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label425" runat="server" Text="1915"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label426" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_37_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label427" runat="server" Text="37600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label428" runat="server" Text="1915"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label429" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_37_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label430" runat="server" Text="37650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label431" runat="server" Text="1920"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_37_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label432" runat="server" Text="37650"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label433" runat="server" Text="1920"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_37_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label434" runat="server" Text="37700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label435" runat="server" Text="1925"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_37_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label436" runat="server" Text="37700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label437" runat="server" Text="1925"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_37_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk38" runat="server" Text="38" AutoPostBack ="true"  
                        oncheckedchanged="chk38_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label438" runat="server" Text="37800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label439" runat="server" Text="2575"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label440" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_38_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label441" runat="server" Text="37800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label442" runat="server" Text="2575"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label443" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_38_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label444" runat="server" Text="38000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label445" runat="server" Text="2595"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_38_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label446" runat="server" Text="38000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label447" runat="server" Text="2595"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_38_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label448" runat="server" Text="38200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label449" runat="server" Text="2615"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_38_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label450" runat="server" Text="38200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label451" runat="server" Text="2615"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_38_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk39" runat="server" Text="39" AutoPostBack ="true"  
                        oncheckedchanged="chk39_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label452" runat="server" Text="38300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label453" runat="server" Text="1885"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label454" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_39_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label455" runat="server" Text="38300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label456" runat="server" Text="1885"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label457" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_39_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label458" runat="server" Text="38450"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label459" runat="server" Text="1900"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_39_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label460" runat="server" Text="38450"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label461" runat="server" Text="1900"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_39_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label462" runat="server" Text="38600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label463" runat="server" Text="1915"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_39_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label464" runat="server" Text="38600"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label465" runat="server" Text="1915"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_39_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk40" runat="server" Text="40" AutoPostBack ="true"  
                        oncheckedchanged="chk40_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label466" runat="server" Text="37800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label467" runat="server" Text="2575"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label468" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_40_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label469" runat="server" Text="37800"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label470" runat="server" Text="2575"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label471" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_40_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label472" runat="server" Text="38000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label473" runat="server" Text="2595"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_40_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label474" runat="server" Text="38000"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label475" runat="server" Text="2595"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_40_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label476" runat="server" Text="38200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label477" runat="server" Text="2615"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_40_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label478" runat="server" Text="38200"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label479" runat="server" Text="2615"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_40_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk41" runat="server" Text="41" AutoPostBack ="true"  
                        oncheckedchanged="chk41_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label480" runat="server" Text="39700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label481" runat="server" Text="2501"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label482" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_41_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label483" runat="server" Text="39700"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label484" runat="server" Text="2501"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label485" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_41_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label486" runat="server" Text="40620"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label487" runat="server" Text="2593"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_41_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label488" runat="server" Text="40620"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label489" runat="server" Text="2593"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_41_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label490" runat="server" Text="41540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label491" runat="server" Text="2685"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_41_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label492" runat="server" Text="41540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label493" runat="server" Text="2685"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_41_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk42" runat="server" Text="42" AutoPostBack ="true"  
                        oncheckedchanged="chk42_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label494" runat="server" Text="41640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label495" runat="server" Text="3405"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label496" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_42_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label497" runat="server" Text="41640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label498" runat="server" Text="3405"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label499" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_42_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label500" runat="server" Text="42590"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label501" runat="server" Text="3500"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_42_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label502" runat="server" Text="42590"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label503" runat="server" Text="3500"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_42_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label504" runat="server" Text="43540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label505" runat="server" Text="3595"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_42_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label506" runat="server" Text="43540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label507" runat="server" Text="3595"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_42_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk43" runat="server" Text="43" AutoPostBack ="true"  
                        oncheckedchanged="chk43_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label508" runat="server" Text="43640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label509" runat="server" Text="2605"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label510" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_43_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label511" runat="server" Text="43640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label512" runat="server" Text="2605"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label513" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_43_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label514" runat="server" Text="44590"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label515" runat="server" Text="3700"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_43_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label516" runat="server" Text="44590"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label517" runat="server" Text="3700"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_43_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label518" runat="server" Text="45540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label519" runat="server" Text="3798"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_43_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label520" runat="server" Text="45540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label521" runat="server" Text="3798"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_43_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk44" runat="server" Text="44" AutoPostBack ="true"  
                        oncheckedchanged="chk44_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label522" runat="server" Text="45640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label523" runat="server" Text="708"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label524" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_44_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label525" runat="server" Text="45640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label526" runat="server" Text="708"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label527" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_44_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label528" runat="server" Text="46090"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label529" runat="server" Text="753"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_44_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label530" runat="server" Text="46090"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label531" runat="server" Text="753"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_44_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label532" runat="server" Text="46540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label533" runat="server" Text="798"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_44_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label534" runat="server" Text="46540"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label535" runat="server" Text="798"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_44_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk45" runat="server" Text="45" AutoPostBack ="true"  
                        oncheckedchanged="chk45_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label536" runat="server" Text="46640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label537" runat="server" Text="1452"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label538" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_45_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label539" runat="server" Text="46640"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label540" runat="server" Text="1452"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label541" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_45_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label542" runat="server" Text="46690"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label543" runat="server" Text="1457"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_45_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label544" runat="server" Text="46690"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label545" runat="server" Text="1457"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_45_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label546" runat="server" Text="46740"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label547" runat="server" Text="1462"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_45_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label548" runat="server" Text="46740"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label549" runat="server" Text="1462"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_45_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk46" runat="server" Text="46" AutoPostBack ="true"  
                        oncheckedchanged="chk46_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label550" runat="server" Text="46840"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label551" runat="server" Text="5155"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label552" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_46_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label553" runat="server" Text="46840"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label554" runat="server" Text="5155"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label555" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_46_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label556" runat="server" Text="50665"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label557" runat="server" Text="5537.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_46_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label558" runat="server" Text="50665"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label559" runat="server" Text="5537.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_46_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label560" runat="server" Text="54490"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label561" runat="server" Text="5920"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_46_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label562" runat="server" Text="54490"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label563" runat="server" Text="5920"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_46_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk47" runat="server" Text="47" AutoPostBack ="true"  
                        oncheckedchanged="chk47_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label564" runat="server" Text="54590"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label565" runat="server" Text="5860"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label566" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_47_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label567" runat="server" Text="54590"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label568" runat="server" Text="5860"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label569" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_47_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label570" runat="server" Text="54890"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label571" runat="server" Text="5890"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_47_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label572" runat="server" Text="54890"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label573" runat="server" Text="5890"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_47_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label574" runat="server" Text="55190"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label575" runat="server" Text="5920"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_47_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label576" runat="server" Text="55190"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label577" runat="server" Text="5920"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_47_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk48" runat="server" Text="48" AutoPostBack ="true"  
                        oncheckedchanged="chk48_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label578" runat="server" Text="55290"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label579" runat="server" Text="3555"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label580" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_48_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label581" runat="server" Text="55290"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label582" runat="server" Text="3555"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label583" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_48_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label584" runat="server" Text="55990"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label585" runat="server" Text="3625"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_48_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label586" runat="server" Text="55990"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label587" runat="server" Text="3625"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_48_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label588" runat="server" Text="56690"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label589" runat="server" Text="3695"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_48_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label590" runat="server" Text="56690"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label591" runat="server" Text="3695"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_48_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk50" runat="server" Text="50" AutoPostBack ="true"  
                        oncheckedchanged="chk50_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label592" runat="server" Text="58290"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label593" runat="server" Text="1437"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label594" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_50_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label595" runat="server" Text="58290"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label596" runat="server" Text="1437"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label597" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_50_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label598" runat="server" Text="58665"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label599" runat="server" Text="1474.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_50_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label600" runat="server" Text="58665"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label601" runat="server" Text="1474.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_50_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label602" runat="server" Text="59040"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label603" runat="server" Text="1512"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_50_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label604" runat="server" Text="59040"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label605" runat="server" Text="1512"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_50_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk51" runat="server" Text="51" AutoPostBack ="true"  
                        oncheckedchanged="chk51_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label606" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label607" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label608" runat="server" Text="59115"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label609" runat="server" Text="1429.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_51_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label610" runat="server" Text="59115"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label611" runat="server" Text="1429.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_51_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk65" runat="server" Text="65" AutoPostBack ="true"  
                        oncheckedchanged="chk65_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label612" runat="server" Text="131122"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label613" runat="server" Text="1925"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label614" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_65_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label615" runat="server" Text="65586"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label616" runat="server" Text="2115"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label617" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_65_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label618" runat="server" Text="131522"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label619" runat="server" Text="1965"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_65_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label620" runat="server" Text="65986"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label621" runat="server" Text="2155"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_65_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label622" runat="server" Text="131922"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label623" runat="server" Text="2005"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_65_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label624" runat="server" Text="66386"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label625" runat="server" Text="2195"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_65_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk66" runat="server" Text="66" AutoPostBack ="true"  
                        oncheckedchanged="chk66_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label626" runat="server" Text="132022"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label627" runat="server" Text="1715"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label628" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_66_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label629" runat="server" Text="66486"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label630" runat="server" Text="2115"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label631" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_66_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label632" runat="server" Text="132322"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label633" runat="server" Text="1745"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_66_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label634" runat="server" Text="66786"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label635" runat="server" Text="2145"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_66_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label636" runat="server" Text="132622"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label637" runat="server" Text="1775"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_66_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label638" runat="server" Text="67086"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label639" runat="server" Text="2175"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_66_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk68" runat="server" Text="68" AutoPostBack ="true"  
                        oncheckedchanged="chk68_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label640" runat="server" Text="132722"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label641" runat="server" Text="709"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label642" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_68_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label643" runat="server" Text="67586"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label644" runat="server" Text="758"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label645" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_68_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label646" runat="server" Text="132822"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label647" runat="server" Text="713"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_68_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label648" runat="server" Text="67686"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label649" runat="server" Text="768"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_68_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label650" runat="server" Text="132922"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label651" runat="server" Text="723"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_68_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label652" runat="server" Text="67786"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label653" runat="server" Text="778"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_68_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk70" runat="server" Text="70" AutoPostBack ="true"  
                        oncheckedchanged="chk70_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label654" runat="server" Text=" "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label655" runat="server" Text=" "></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label656" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="Label657" runat="server" Text="68386"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label658" runat="server" Text="2000"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label659" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label660" runat="server" Text="133047"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label661" runat="server" Text="1702.41"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_70_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label662" runat="server" Text="68411"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label663" runat="server" Text="2002.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_70_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label664" runat="server" Text=" "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label665" runat="server" Text=" "></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="Label666" runat="server" Text="68436"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label667" runat="server" Text="2005"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk71" runat="server" Text="71" AutoPostBack ="true"  
                        oncheckedchanged="chk71_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label668" runat="server" Text="133172"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label669" runat="server" Text="668"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label670" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_71_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label671" runat="server" Text="68636"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label672" runat="server" Text="622"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label673" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_71_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label674" runat="server" Text="133297"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label675" runat="server" Text="680.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_71_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label676" runat="server" Text="68761"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label677" runat="server" Text="634.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_71_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label678" runat="server" Text="133497"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label679" runat="server" Text="693"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_71_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label680" runat="server" Text="6886"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label681" runat="server" Text="647"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_71_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk72" runat="server" Text="72" AutoPostBack ="true"  
                        oncheckedchanged="chk72_CheckedChanged"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label682" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label683" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label684" runat="server" Text="133497"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label685" runat="server" Text="453.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_72_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label686" runat="server" Text="68961"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label687" runat="server" Text="463.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_72_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    &nbsp;
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td rowspan =3>
                    <asp:CheckBox ID="chk74" runat="server" Text="74" AutoPostBack ="true"  
                        oncheckedchanged="chk74_CheckedChanged"/>
                </td>
                <td>
                    <asp:Label ID="Label688" runat="server" Text="133622"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label689" runat="server" Text="1432"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label690" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_74_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label691" runat="server" Text="69086"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label692" runat="server" Text="1480"></asp:Label>
                </td>
                <td rowspan =3>
                    <asp:Label ID="Label693" runat="server" Text="10"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_74_1" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label694" runat="server" Text="133787"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label695" runat="server" Text="1448.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_74_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label696" runat="server" Text="69251"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label697" runat="server" Text="1496.5"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_74_2" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                              
                <td>
                    <asp:Label ID="Label698" runat="server" Text="133952"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label699" runat="server" Text="1465"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTRP_74_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label700" runat="server" Text="69416"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label701" runat="server" Text="1513"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTIS_74_3" runat="server" Width ="200px"></asp:TextBox>
                </td>
            </tr>
        </table> 
        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="ddlPublish" 
                                EventName="SelectedIndexChanged" />--%>


                </Triggers>                    

        </asp:UpdatePanel>  
        <div align ="center" style="COLOR: red">
                    


                        <br />
                        <asp:Button ID="butOK" runat="server" Text="確定" 
                                onclick="butOK_Click" Width="59px" Height="30px" />
                               
                        
                    <br />
                    <br />
        </div>
    </div>
    </form>
</body>
</html>
