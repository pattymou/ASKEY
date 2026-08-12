<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="BenchmarkSearch.aspx.cs" Inherits="WebForm_BenchmarkSearch" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
  <script src="../js/jquery-1.10.4.min.js"></script>
    <script>
      $(function() {
          $("#tabs").tabs();
      });
  </script>
<%--<fieldset>--%>
        <table id="Table3" class="one" width="100%">
<%--            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="類別："></asp:Label>
                    
                    
                    <asp:DropDownList ID="ddlKind" runat="server" Height="16px">
                    </asp:DropDownList>
                    
                    
                </td>
            </tr>--%>        
                                   
            <tr>
                <td>
                    
                    <asp:Label ID="Label5" runat="server" Text="Benchmark："></asp:Label>
                    
                    <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                    
                    
                    <asp:DropDownList ID="ddlBenchmark" runat="server">
                        <asp:ListItem>Los</asp:ListItem>
                        <asp:ListItem>Indoor</asp:ListItem>
                    </asp:DropDownList>
                    
                    
                </td>
            </tr>
            </table> 
                <asp:Panel ID="Panel1" runat="server" Width = "100%">
                <%--======--%>  
                                    
<%--                    <table>
--%>                    <div id="tabs">
                      <ul>
                        <li><a href="#tabs-1">11a</a></li>
                        <li><a href="#tabs-2">11b</a></li>
                        <li><a href="#tabs-3">11g</a></li>
                        <li><a href="#tabs-4">11n - 2.4G</a></li>
                        <li><a href="#tabs-5">11n - 5G</a></li>
                        <li><a href="#tabs-6">11ac</a></li>                       
                      </ul>
                      <div id="tabs-1">
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkA1" runat="server" Text ="5170 MHz  /  CH 34" />   
                                        &nbsp;&nbsp;&nbsp;                      
                                        <asp:CheckBox ID="chkA2" runat="server" Text ="5180 MHz  /  CH 36" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA3" runat="server" Text ="5200 MHz  /  CH 40" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA4" runat="server" Text ="5220 MHz  /  CH 44" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA5" runat="server" Text ="5240 MHz  /  CH 48" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA6" runat="server" Text ="5260 MHz  /  CH 52" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkA7" runat="server" Text ="5280 MHz  /  CH 56" />
                                        &nbsp;&nbsp;&nbsp; 
                                                                                                                                                                
                                    </td>
                                    
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkA8" runat="server" Text ="5300 MHz  /  CH 60" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkA9" runat="server" Text ="5320 MHz  /  CH 64" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkA10" runat="server" Text ="5500 MHz  /  CH 100" />   
                                        &nbsp;&nbsp;&nbsp;                       
                                        <asp:CheckBox ID="chkA11" runat="server" Text ="5520 MHz  /  CH 104" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA12" runat="server" Text ="5540 MHz  /  CH 108" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA13" runat="server" Text ="5560 MHz  /  CH 112" />
                                        &nbsp;&nbsp;&nbsp;

                                                                                                                                                                 
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkA14" runat="server" Text ="5580 MHz  /  CH 116" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA15" runat="server" Text ="5600 MHz  /  CH 120" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkA16" runat="server" Text ="5620 MHz  /  CH 124" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkA17" runat="server" Text ="5640 MHz  /  CH 128" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                       <asp:CheckBox ID="chkA18" runat="server" Text ="5660 MHz  /  CH 132" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkA19" runat="server" Text ="5680 MHz  /  CH 136" />   
                                        &nbsp;&nbsp;&nbsp; 
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>                                                              
                                        <asp:CheckBox ID="chkA20" runat="server" Text ="5700 MHz  /  CH 140" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA21" runat="server" Text ="5745 MHz  /  CH 149" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA22" runat="server" Text ="5765 MHz  /  CH 153" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA23" runat="server" Text ="5787 MHz  /  CH 157" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkA24" runat="server" Text ="5805 MHz  /  CH 161" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkA25" runat="server" Text ="5825 MHz  /  CH 165" />                                                                                                                                                                
                                    </td>                                                                       
                                </tr>   
                            </table>                       
                        </div>
                        <div id="tabs-2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkB1" runat="server" Text ="2412 MHz  /  CH 01" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB2" runat="server" Text ="2417 MHz  /  CH 02" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB3" runat="server" Text ="2422 MHz  /  CH 03" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB4" runat="server" Text ="2427 MHz  /  CH 04" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB5" runat="server" Text ="2432 MHz  /  CH 05" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB6" runat="server" Text ="2437 MHz  /  CH 06" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB7" runat="server" Text ="2442 MHz  /  CH 07" />
                                        &nbsp;&nbsp;&nbsp;                                        
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkB8" runat="server" Text ="2447 MHz  /  CH 08" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB9" runat="server" Text ="2452 MHz  /  CH 09" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB10" runat="server" Text ="2457 MHz  /  CH 10" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB11" runat="server" Text ="2462 MHz  /  CH 11" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB12" runat="server" Text ="2467 MHz  /  CH 12" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkB13" runat="server" Text ="2472 MHz  /  CH 13" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        
                        </div>
                        <div id="tabs-3">
                            <table >
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkG1" runat="server" Text ="2412 MHz  /  CH 01" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG2" runat="server" Text ="2417 MHz  /  CH 02" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG3" runat="server" Text ="2422 MHz  /  CH 03" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG4" runat="server" Text ="2427 MHz  /  CH 04" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG5" runat="server" Text ="2432 MHz  /  CH 05" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG6" runat="server" Text ="2437 MHz  /  CH 06" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG7" runat="server" Text ="2442 MHz  /  CH 07" />
                                        &nbsp;&nbsp;&nbsp;                                        
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkG8" runat="server" Text ="2447 MHz  /  CH 08" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG9" runat="server" Text ="2452 MHz  /  CH 09" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG10" runat="server" Text ="2457 MHz  /  CH 10" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG11" runat="server" Text ="2462 MHz  /  CH 11" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG12" runat="server" Text ="2467 MHz  /  CH 12" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkG13" runat="server" Text ="2472 MHz  /  CH 13" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>

                            </table>
                        </div> 
                        <div id="tabs-4">
                            <table >
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="20MHz："></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN221" runat="server" Text ="2412 MHz  /  CH 01" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN222" runat="server" Text ="2417 MHz  /  CH 02" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN223" runat="server" Text ="2422 MHz  /  CH 03" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN224" runat="server" Text ="2427 MHz  /  CH 04" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN225" runat="server" Text ="2432 MHz  /  CH 05" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN226" runat="server" Text ="2437 MHz  /  CH 06" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN227" runat="server" Text ="2442 MHz  /  CH 07" />
                                        &nbsp;&nbsp;&nbsp;                                        
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN228" runat="server" Text ="2447 MHz  /  CH 08" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN229" runat="server" Text ="2452 MHz  /  CH 09" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN2210" runat="server" Text ="2457 MHz  /  CH 10" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN2211" runat="server" Text ="2462 MHz  /  CH 11" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN2212" runat="server" Text ="2467 MHz  /  CH 12" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN2213" runat="server" Text ="2472 MHz  /  CH 13" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>   
                                    <td>
                                    ***************************************************
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="40MHz："></asp:Label>
                                    </td>
                                </tr>  
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN241" runat="server" Text ="2422 MHz  /  CH 03" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN242" runat="server" Text ="2427 MHz  /  CH 04" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN243" runat="server" Text ="2432 MHz  /  CH 05" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN244" runat="server" Text ="2437 MHz  /  CH 06" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN245" runat="server" Text ="2442 MHz  /  CH 07" />
                                        &nbsp;&nbsp;&nbsp;                                        
                                        <asp:CheckBox ID="chkN246" runat="server" Text ="2447 MHz  /  CH 08" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN247" runat="server" Text ="2452 MHz  /  CH 09" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>                                        
                                        <asp:CheckBox ID="chkN248" runat="server" Text ="2457 MHz  /  CH 10" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>                                                                                             
                            </table>
                        </div> 
                        <div id="tabs-5">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="20MHz："></asp:Label>
                                    </td>
                                </tr>                            
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN521" runat="server" Text ="5170 MHz  /  CH 34" />   
                                        &nbsp;&nbsp;&nbsp;                      
                                        <asp:CheckBox ID="chkN522" runat="server" Text ="5180 MHz  /  CH 36" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN523" runat="server" Text ="5200 MHz  /  CH 40" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN524" runat="server" Text ="5220 MHz  /  CH 44" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN525" runat="server" Text ="5240 MHz  /  CH 48" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN526" runat="server" Text ="5260 MHz  /  CH 52" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN527" runat="server" Text ="5280 MHz  /  CH 56" />
                                        &nbsp;&nbsp;&nbsp; 
                                                                                                                                                               
                                    </td>
                                    
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN528" runat="server" Text ="5300 MHz  /  CH 60" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN529" runat="server" Text ="5320 MHz  /  CH 64" />
                                        &nbsp;&nbsp;&nbsp;                                      
                                        <asp:CheckBox ID="chkN5210" runat="server" Text ="5500 MHz  /  CH 100" />   
                                        &nbsp;&nbsp;&nbsp;                       
                                        <asp:CheckBox ID="chkN5211" runat="server" Text ="5520 MHz  /  CH 104" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5212" runat="server" Text ="5540 MHz  /  CH 108" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5213" runat="server" Text ="5560 MHz  /  CH 112" />
                                        &nbsp;&nbsp;&nbsp;

                                                                                                                                                                 
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN5214" runat="server" Text ="5580 MHz  /  CH 116" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5215" runat="server" Text ="5600 MHz  /  CH 120" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN5216" runat="server" Text ="5620 MHz  /  CH 124" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN5217" runat="server" Text ="5640 MHz  /  CH 128" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkN5218" runat="server" Text ="5660 MHz  /  CH 132" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkN5219" runat="server" Text ="5680 MHz  /  CH 136" />   
                                        &nbsp;&nbsp;&nbsp; 
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>                                                              
                                        <asp:CheckBox ID="chkN5220" runat="server" Text ="5700 MHz  /  CH 140" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5221" runat="server" Text ="5745 MHz  /  CH 149" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5222" runat="server" Text ="5765 MHz  /  CH 153" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5223" runat="server" Text ="5785 MHz  /  CH 157" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkN5224" runat="server" Text ="5805 MHz  /  CH 161" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN5225" runat="server" Text ="5825 MHz  /  CH 165" />                                                                                                                                                                
                                    </td>                                                                       
                                </tr> 
                                <tr>   
                                    <td>
                                    ***************************************************
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="40MHz："></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkN541" runat="server" Text ="5190 MHz  /  CH 38" />
                                        &nbsp;&nbsp;&nbsp;     
                                        <asp:CheckBox ID="chkN542" runat="server" Text ="5230 MHz  /  CH 46" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN543" runat="server" Text ="5270 MHz  /  CH 54" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN544" runat="server" Text ="5310 MHz  /  CH 62" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN545" runat="server" Text ="5510 MHz  /  CH 102" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN546" runat="server" Text ="5550 MHz  /  CH 110" />
                                        &nbsp;&nbsp;&nbsp; 

                                    </td> 
                                  </tr> 
                                  <tr>
                                    <td>  
                                        <asp:CheckBox ID="chkN547" runat="server" Text ="5590 MHz  /  CH 118" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN548" runat="server" Text ="5630 MHz  /  CH 126" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkN549" runat="server" Text ="5670 MHz  /  CH 134" />
                                        &nbsp;&nbsp;&nbsp;                                                                           
                                        <asp:CheckBox ID="chkN5410" runat="server" Text ="5710 MHz  /  CH 142" />
                                        &nbsp;&nbsp;&nbsp;  
                                        <asp:CheckBox ID="chkN5411" runat="server" Text ="5755 MHz  /  CH 151" />
                                        &nbsp;&nbsp;&nbsp;   
                                        <asp:CheckBox ID="chkN5412" runat="server" Text ="5795 MHz  /  CH 159" />
                                        &nbsp;&nbsp;&nbsp;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                    </td>
                                </tr>
                                                                  
                            </table>                            
                        </div> 
                        <div id="tabs-6">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="20MHz："></asp:Label>
                                    </td>
                                </tr>                            
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAC21" runat="server" Text ="5170 MHz  /  CH 34" />   
                                        &nbsp;&nbsp;&nbsp;                      
                                        <asp:CheckBox ID="chkAC22" runat="server" Text ="5180 MHz  /  CH 36" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC23" runat="server" Text ="5200 MHz  /  CH 40" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC24" runat="server" Text ="5220 MHz  /  CH 44" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC25" runat="server" Text ="5240 MHz  /  CH 48" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC26" runat="server" Text ="5260 MHz  /  CH 52" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC27" runat="server" Text ="5280 MHz  /  CH 56" />
                                        &nbsp;&nbsp;&nbsp; 
                                                                                                                                                                
                                    </td>
                                    
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAC28" runat="server" Text ="5300 MHz  /  CH 60" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC29" runat="server" Text ="5320 MHz  /  CH 64" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkAC210" runat="server" Text ="5500 MHz  /  CH 100" />   
                                        &nbsp;&nbsp;&nbsp;                       
                                        <asp:CheckBox ID="chkAC211" runat="server" Text ="5520 MHz  /  CH 104" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC212" runat="server" Text ="5540 MHz  /  CH 108" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC213" runat="server" Text ="5560 MHz  /  CH 112" />
                                        &nbsp;&nbsp;&nbsp;

                                                                                                                                                                 
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAC214" runat="server" Text ="5580 MHz  /  CH 116" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC215" runat="server" Text ="5600 MHz  /  CH 120" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC216" runat="server" Text ="5620 MHz  /  CH 126" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC217" runat="server" Text ="5640 MHz  /  CH 128" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkAC218" runat="server" Text ="5660 MHz  /  CH 132" />
                                        &nbsp;&nbsp;&nbsp;                                     
                                        <asp:CheckBox ID="chkAC219" runat="server" Text ="5680 MHz  /  CH 136" />   
                                        &nbsp;&nbsp;&nbsp;   
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>                                                            
                                        <asp:CheckBox ID="chkAC220" runat="server" Text ="5700 MHz  /  CH 140" /> 
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC221" runat="server" Text ="5745 MHz  /  CH 149" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC222" runat="server" Text ="5765 MHz  /  CH 153" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC223" runat="server" Text ="5785 MHz  /  CH 157" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAC224" runat="server" Text ="5805 MHz  /  CH 161" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC225" runat="server" Text ="5825 MHz  /  CH 165" />                                                                                                                                                                
                                    </td>                                                                       
                                </tr> 
                                <tr>   
                                    <td>
                                    ***************************************************
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="40MHz："></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAC41" runat="server" Text ="5190 MHz  /  CH 38" />
                                        &nbsp;&nbsp;&nbsp;     
                                        <asp:CheckBox ID="chkAC42" runat="server" Text ="5230 MHz  /  CH 46" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC43" runat="server" Text ="5270 MHz  /  CH 54" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC44" runat="server" Text ="5310 MHz  /  CH 62" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC45" runat="server" Text ="5510 MHz  /  CH 102" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC46" runat="server" Text ="5550 MHz  /  CH 110" />
                                        &nbsp;&nbsp;&nbsp; 

                                    </td> 
                                  </tr> 
                                  <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAC47" runat="server" Text ="5590 MHz  /  CH 118" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC48" runat="server" Text ="5630 MHz  /  CH 126" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC49" runat="server" Text ="5670 MHz  /  CH 134" />
                                        &nbsp;&nbsp;&nbsp;                                    
                                        <asp:CheckBox ID="chkAC410" runat="server" Text ="5710 MHz  /  CH 142" />
                                        &nbsp;&nbsp;&nbsp;  
                                        <asp:CheckBox ID="chkAC411" runat="server" Text ="5755 MHz  /  CH 151" />
                                        &nbsp;&nbsp;&nbsp;   
                                        <asp:CheckBox ID="chkAC412" runat="server" Text ="5795 MHz  /  CH 159" />
                                        &nbsp;&nbsp;&nbsp;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                    </td>
                                </tr>
                                <tr>   
                                    <td>
                                    ***************************************************
                                    </td> 
                                </tr> 
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="80MHz："></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAC81" runat="server" Text ="5210 MHz  /  CH 42" />
                                        &nbsp;&nbsp;&nbsp;     
                                        <asp:CheckBox ID="chkAC82" runat="server" Text ="5290 MHz  /  CH 58" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC83" runat="server" Text ="5530 MHz  /  CH 106" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC84" runat="server" Text ="5610 MHz  /  CH 122" />
                                        &nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chkAC85" runat="server" Text ="5775 MHz  /  CH 155" />
                                        &nbsp;&nbsp;&nbsp; 
                                    </td>
                                </tr>                                  
                            </table>                            
                        </div> 
                       </div>                    
                    
                    <%--</table>--%>                 
                <%--=====--%>    
                </asp:Panel> 
            <table Width = "100%">
            <tr>
                <td align ="center">
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    
                </td>
            </tr>
            <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Customer" HeaderText="客戶" ReadOnly="True" SortExpression="Customer">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="機種名稱" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NPI" HeaderText="NPI" ReadOnly="True" SortExpression="NPI">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                
                                <asp:TemplateField>
                                    <headertemplate> 
                                        <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="Comply(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                                    </headertemplate>
                                    <itemtemplate> 
                                        <asp:CheckBox ID="CheckBox2" runat="server"/> 
                                    </itemtemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                                                              
                                <%--<asp:TemplateField HeaderText="設備名稱" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ApparatusView.aspx?ID="+Eval("ID") %>'
                                            Text='<%# Bind("Name") %>'></asp:HyperLink>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>   
                        
                        
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align ="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                </td>
            </tr>
             
        </table> 
    <%--</fieldset>--%>
</asp:Content>

