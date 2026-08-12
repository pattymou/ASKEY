<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Certification_Wifi.aspx.cs" Inherits="WebForm_Certification_Wifi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wifi</title>
</head>
  <link rel="stylesheet" href="../css/jquery-ui.min.css">
  <script src="../js/jquery-1.10.2.min.js"></script>
  <script src="../js/jquery-1.10.4.min.js"></script>
  <script src="../js/optgroupTrans.js" type="text/javascript"></script>

  <script>
    $(function() {
    $( "#tabs" ).tabs();
  });
  </script>
  <script>
    $(function() {
    $( "#tabs1" ).tabs();
  });
  </script>
  
  <script>
    $(function() {
    $( "#tabs2" ).tabs();
  });
  </script>
  
  <script type="text/javascript">
            $(document).ready(function() {
                $('#ddlAP option').optgroupTrans();
                $('#ddlAPS option').optgroupTrans();
                $('#ddlSTA option').optgroupTrans();
                $('#ddlSTAS option').optgroupTrans();
            });
        </script>
 <script type="text/javascript">
        window.onload = load;
        function slide() {
            //alert(selected_tab);
                $('#ddlAP option').optgroupTrans();
                $('#ddlAPS option').optgroupTrans();
                $('#ddlSTA option').optgroupTrans();
                $('#ddlSTAS option').optgroupTrans();

        }
        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }

        function EndRequestHandler() {
            slide();
        }
    </script>      

<body>
  
    <form id="form1" runat="server">
        <div id="tabs2">
            <ul>
                <li><a href="#tabs1-1">Product Information</a></li>
                <li><a href="#tabs1-2">Product Designators</a></li>
                <li><a href="#tabs1-3">Program Selection</a></li>
                <li><a href="#tabs1-4">Optional Programs</a></li>
                <li><a href="#tabs1-5">Product Capabilities</a></li>
                
            </ul>
            <div id="tabs1-1">
                <asp:Label ID="Label21" runat="server" Text="Product Details - The following fields must include the description of your product exactly as you wish the final certifilcation to read.Please verify the name before submission."></asp:Label>
                <br />
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>--%>
                <table>
                    <tr>
                        <td colspan =2>
                            <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                            <asp:Label ID="Label32" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtProductName" runat="server" Width ="100%"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan =2>
                            <asp:Label ID="Label2" runat="server" Text="Model Number"></asp:Label>
                            <asp:Label ID="Label33" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtModelNumber" runat="server" Width ="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan =2>
                            <asp:Label ID="Label3" runat="server" Text="Wireless Chipset"></asp:Label>
                            <asp:Label ID="Label34" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtChipset" runat="server" Width ="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Product Operating System"></asp:Label>
                            <asp:Label ID="Label35" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtProductOperating" runat="server" Width ="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="OS Version"></asp:Label>
                            <asp:Label ID="Label36" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtOSVersion" runat="server" Width ="100%"></asp:TextBox>
                        </td>                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Product"></asp:Label><br />
                            <asp:Label ID="Label6" runat="server" Text="Hardware Version"></asp:Label>
                            <asp:Label ID="Label37" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtPHardware" runat="server" Width ="100%"></asp:TextBox>
                        </td>
                        <td>
                            <br />
                            <asp:Label ID="Label7" runat="server" Text="Firmware Version"></asp:Label>
                            <asp:Label ID="Label38" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtPFirmware" runat="server" Width ="100%"></asp:TextBox>
                        </td>                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Wi-Fi Component"></asp:Label><br />
                            <asp:Label ID="Label10" runat="server" Text="Hardware Version"></asp:Label>
                            <asp:Label ID="Label39" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtWHardware" runat="server" Width ="100%"></asp:TextBox>
                        </td>
                        <td>
                            <br />
                            <asp:Label ID="Label11" runat="server" Text="Firmware Version"></asp:Label>
                            <asp:Label ID="Label40" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:TextBox ID="txtWFirmware" runat="server" Width ="100%"></asp:TextBox>
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan =2>
                            <asp:Label ID="Label12" runat="server" Text="Product Notes"></asp:Label>
                            <asp:TextBox ID="txtPNote" runat="server" Width ="100%" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Searchable by the Public"></asp:Label>
                            <asp:Label ID="Label41" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:DropDownList ID="ddlSearchable" runat="server">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Publish On Date"></asp:Label>
                            <asp:Label ID="Label42" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:DropDownList ID="ddlPublish" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlPublish_SelectedIndexChanged">
                                <asp:ListItem>Certification Data</asp:ListItem>
                                <asp:ListItem>Deferred Date</asp:ListItem>
                                <asp:ListItem>Never</asp:ListItem>                            
                            </asp:DropDownList>
                        </td>
                        <td id="Deferred" runat ="server">
                            <asp:Label ID="Label31" runat="server" Text="Date："></asp:Label>
                            <asp:Label ID="Label43" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <input type="text" id="datepicker" name = "date1" value="<%=strDate%>">
                            
                             <script>
                                 $(function() {
                                 $("#datepicker").datepicker();
                             });
                            
                             </script>
                        </td>                        
                    </tr>
                </table>
                   <%--</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlPublish" 
                                EventName="SelectedIndexChanged" />


                </Triggers>                    

        </asp:UpdatePanel>--%>
                
            </div>
            <div id="tabs1-2">
                <asp:Label ID="Label15" runat="server" Text="Complete the following fields to describe the capabilities of your product to be certified. Note that Product Categories will be used for public search of Certified Products."></asp:Label>
                <br />
                <br />  
                 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>          
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Device Type"></asp:Label>
                            <asp:Label ID="Label44" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:DropDownList ID="ddlDeveicType" runat="server">
                                <asp:ListItem>Personal</asp:ListItem> 
                                <asp:ListItem>Enterprise</asp:ListItem> 
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label17" runat="server" Text="Product Type"></asp:Label>
                            <asp:Label ID="Label45" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlProductType_SelectedIndexChanged">
                                <asp:ListItem>AP</asp:ListItem> 
                                <asp:ListItem>STA</asp:ListItem> 
                                <asp:ListItem>Mobile AP</asp:ListItem> 
                                <asp:ListItem>STA(20MHz)</asp:ListItem> 
                            </asp:DropDownList>
                        </td>                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Primary Product Category"></asp:Label>
                            <asp:Label ID="Label46" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <%--<asp:DropDownList ID="ddlPrimary" runat="server">
                            </asp:DropDownList>--%>
<%--                            <div id="divAP1" runat ="server" >

                            </div>--%>
                        </td>
                        <td id="divAP" runat ="server">
                            <asp:DropDownList ID="ddlAP" runat="server">
                                    <asp:ListItem Value="">請選擇</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design and Modules" Value="Embedded Module">Embedded Module</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design and Modules" Value="Reference Design">Reference Design</asp:ListItem>
                                    <asp:ListItem optgroup="Networking" Value="Access Point for Home or Small Office (Wireless Router)">Access Point for Home or Small Office (Wireless Router)</asp:ListItem>
                                    <asp:ListItem optgroup="Networking" Value="Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)">Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)</asp:ListItem>
                                    <asp:ListItem optgroup="Networking" Value="Repeater, Extender, Mesh System, Controller">Repeater, Extender, Mesh System, Controller</asp:ListItem>
                                    
                            </asp:DropDownList>
                        </td>
                        <%--<td id="divAP1" runat ="server">
                            <select id ="ddlAP" name="ddlAP1">
                                <optgroup label = "Reference Design and Modules">
                                    <option value ="Embedded Module">Embedded Module</option>
                                    <option value ="Reference Design">Reference Design</option>
                                </optgroup>
                                <optgroup label = "Networking">
                                    <option value ="Access Point for Home or Small Office (Wireless Router)">Access Point for Home or Small Office (Wireless Router)</option>
                                    <option value ="Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)">Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)</option>
                                    <option value ="Repeater, Extender, Mesh System, Controller">Repeater, Extender, Mesh System, Controller</option>
                                </optgroup>
                            </select>                        
                        </td>--%>
                        <td id="divSTA" runat ="server">
                            <asp:DropDownList ID="ddlSTA" runat="server">
                                    <asp:ListItem Value="">請選擇</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design and Modules  " Value="Embedded Module">Embedded Module</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design and Modules  " Value="Reference Design">Reference Design</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Digital Audio - Portable (MP3 player)">Digital Audio - Portable (MP3 player)</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Digital Audio - Stationary (speakers, receiver, MP3 player)">Digital Audio - Stationary (speakers, receiver, MP3 player)</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="eReader">eReader</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Game Console or Game Console Adapter">Game Console or Game Console Adapter</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Gaming Device - Portable">Gaming Device - Portable</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Media Adapter">Media Adapter</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Media Remote/Input Device">Media Remote/Input Device</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Media Server">Media Server</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Navigation/GPS">Navigation/GPS</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Picture Frame">Picture Frame</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Set Top box, Media Extender (includes players & recorders)">Set Top box, Media Extender (includes players & recorders)</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Still Camera">Still Camera</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Television">Television</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Video Camera">Video Camera</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics" Value="Web Camera">Web Camera</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Docking Station">Docking Station</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Keyboard">Keyboard</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Monitor">Monitor</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Mouse">Mouse</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Network Storage Device (networked hard drive)">Network Storage Device (networked hard drive)</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Printer/Multi-Function Printer/Print Server">Printer/Multi-Function Printer/Print Server</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Projector">Projector</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Tablet (Wi-Fi and other)">Tablet (Wi-Fi and other)</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals" Value="Tablet (Wi-Fi only)">Tablet (Wi-Fi only)</asp:ListItem>
                                    <asp:ListItem optgroup="Handsets" Value="Phone, multi-mode (Wi-fi and other)">Phone, multi-mode (Wi-fi and other)</asp:ListItem>
                                    <asp:ListItem optgroup="Handsets" Value="Phone, single-mode (Wi-Fi only)">Phone, single-mode (Wi-Fi only)</asp:ListItem>
                                    <asp:ListItem optgroup="Automotive & Transportation" Value="In-vehicle Network">In-vehicle Network</asp:ListItem>
                                    <asp:ListItem optgroup="Automotive & Transportation" Value="Transportation Management">Transportation Management</asp:ListItem>
                                    <asp:ListItem optgroup="Health & Fitness" Value="Medical/Fitness Device">Medical/Fitness Device</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Appliances">Appliances</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Home Energy Management">Home Energy Management</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Home Security and Control">Home Security and Control</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Hot Water Heater">Hot Water Heater</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Refrigerator">Refrigerator</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Thermostat">Thermostat</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Transmission and Distribution Equipment">Transmission and Distribution Equipment</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Utility Meter">Utility Meter</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy" Value="Washer/Dryer">Washer/Dryer</asp:ListItem>
                                    <asp:ListItem optgroup="Other" Value="Embedded Sensor">Embedded Sensor</asp:ListItem>
                                    <asp:ListItem optgroup="Other" Value="Industrial (communications & input)">Industrial (communications & input)</asp:ListItem>
                                    <asp:ListItem optgroup="Other" Value="Other">Other</asp:ListItem>
                                    
                            </asp:DropDownList>
                        </td>
                        <%--<td id="divSTA" runat ="server">
                            <select id ="ddlSTA" name="ddlSTA1">
                                <optgroup label = "Reference Design and Modules">
                                    <option value ="Embedded Module">Embedded Module</option>
                                    <option value ="Reference Design">Reference Design</option>
                                </optgroup>
                                <optgroup label = "Consumer Electronics">
                                    <option value ="Digital Audio - Portable (MP3 player)">Digital Audio - Portable (MP3 player)</option>
                                    <option value ="Digital Audio - Stationary (speakers, receiver, MP3 player)">Digital Audio - Stationary (speakers, receiver, MP3 player)</option>
                                    <option value ="eReader">eReader</option>
                                    <option value ="Game Console or Game Console Adapter">Game Console or Game Console Adapter</option>
                                    <option value ="Gaming Device - Portable">Gaming Device - Portable</option>
                                    <option value ="Media Adapter">Media Adapter</option>
                                    <option value ="Media Remote/Input Device">Media Remote/Input Device</option>
                                    <option value ="Media Server">Media Server</option>
                                    <option value ="Navigation/GPS">Navigation/GPS</option>
                                    <option value ="Picture Frame">Picture Frame</option>
                                    <option value ="Set Top box, Media Extender (includes players & recorders)">Set Top box, Media Extender (includes players & recorders)</option>
                                    <option value ="Still Camera">Still Camera</option>
                                    <option value ="Television">Television</option>
                                    <option value ="Video Camera">Video Camera</option>
                                    <option value ="Web Camera">Web Camera</option>
                                </optgroup>
                                <optgroup label = "Computing & Peripherals">
                                    <option value ="Docking Station">Docking Station</option>
                                    <option value ="Keyboard">Keyboard</option>
                                    <option value ="Monitor">Monitor</option>
                                    <option value ="Mouse">Mouse</option>
                                    <option value ="Network Storage Device (networked hard drive)">Network Storage Device (networked hard drive)</option>
                                    <option value ="Printer/Multi-Function Printer/Print Server">Printer/Multi-Function Printer/Print Server</option>
                                    <option value ="Projector">Projector</option>
                                    <option value ="Tablet (Wi-Fi and other)">Tablet (Wi-Fi and other)</option>
                                    <option value ="Tablet (Wi-Fi only)">Tablet (Wi-Fi only)</option>
                                </optgroup>
                                <optgroup label = "Handsets">
                                    <option value ="Phone, multi-mode (Wi-fi and other)">Phone, multi-mode (Wi-fi and other)</option>
                                    <option value ="Phone, single-mode (Wi-Fi only)">Phone, single-mode (Wi-Fi only)</option>
                                </optgroup>
                                <optgroup label = "Automotive & Transportation">
                                    <option value ="In-vehicle Network">In-vehicle Network</option>
                                    <option value ="Transportation Management">Transportation Management</option>
                                </optgroup>
                                <optgroup label = "Health & Fitness">
                                    <option value ="Medical/Fitness Device">Medical/Fitness Device</option>
                                </optgroup>
                                <optgroup label = "Smart Energy">
                                    <option value ="Appliances">Appliances</option>
                                    <option value ="Home Energy Management">Home Energy Management</option>
                                    <option value ="Home Security and Control">Home Security and Control</option>
                                    <option value ="Hot Water Heater">Hot Water Heater</option>
                                    <option value ="Refrigerator">Refrigerator</option>
                                    <option value ="Thermostat">Thermostat</option>
                                    <option value ="Transmission and Distribution Equipment">Transmission and Distribution Equipment</option>
                                    <option value ="Utility Meter">Utility Meter</option>
                                    <option value ="Washer/Dryer">Washer/Dryer</option>
                                </optgroup>
                                <optgroup label = "Other">
                                    <option value ="Embedded Sensor">Embedded Sensor</option>
                                    <option value ="Industrial (communications & input)">Industrial (communications & input)</option>
                                    <option value ="Other">Other</option>
                                </optgroup>
                            </select>                        
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="Secondary Product Category"></asp:Label>
                        </td> 
                        <td id="divAPS" runat ="server">
                            <asp:DropDownList ID="ddlAPS" runat="server">
                                    <asp:ListItem Value="">請選擇</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design And Modules" Value="Embedded Module">Embedded Module</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design And Modules" Value="Reference Design">Reference Design</asp:ListItem>
                                    <asp:ListItem optgroup="Networking " Value="Access Point for Home or Small Office (Wireless Router)">Access Point for Home or Small Office (Wireless Router)</asp:ListItem>
                                    <asp:ListItem optgroup="Networking " Value="Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)">Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)</asp:ListItem>
                                    <asp:ListItem optgroup="Networking " Value="Repeater, Extender, Mesh System, Controller">Repeater, Extender, Mesh System, Controller</asp:ListItem>
                                    
                            </asp:DropDownList>
                        </td>
                        <%--<td id="divAPS" runat ="server">
                            <select id ="ddlAPS" name="ddlAPS1">
                                <optgroup label = "Reference Design and Modules">
                                    <option value ="Embedded Module">Embedded Module</option>
                                    <option value ="Reference Design">Reference Design</option>
                                </optgroup>
                                <optgroup label = "Networking">
                                    <option value ="Access Point for Home or Small Office (Wireless Router)">Access Point for Home or Small Office (Wireless Router)</option>
                                    <option value ="Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)">Cable, DSL or Other Broadband Gateway (Integrated Home Access Device)</option>
                                    <option value ="Repeater, Extender, Mesh System, Controller">Repeater, Extender, Mesh System, Controller</option>
                                </optgroup>
                            </select>                        
                        </td>--%>
                        <td id="divSTAS" runat ="server">
                            <asp:DropDownList ID="ddlSTAS" runat="server">
                                    <asp:ListItem Value="">請選擇</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design and Modules   " Value="Embedded Module">Embedded Module</asp:ListItem>
                                    <asp:ListItem optgroup="Reference Design and Modules   " Value="Reference Design">Reference Design</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Digital Audio - Portable (MP3 player)">Digital Audio - Portable (MP3 player)</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Digital Audio - Stationary (speakers, receiver, MP3 player)">Digital Audio - Stationary (speakers, receiver, MP3 player)</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="eReader">eReader</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Game Console or Game Console Adapter">Game Console or Game Console Adapter</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Gaming Device - Portable">Gaming Device - Portable</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Media Adapter">Media Adapter</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Media Remote/Input Device">Media Remote/Input Device</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Media Server">Media Server</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Navigation/GPS">Navigation/GPS</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Picture Frame">Picture Frame</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Set Top box, Media Extender (includes players & recorders)">Set Top box, Media Extender (includes players & recorders)</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Still Camera">Still Camera</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Television">Television</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Video Camera">Video Camera</asp:ListItem>
                                    <asp:ListItem optgroup="Consumer Electronics " Value="Web Camera">Web Camera</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Docking Station">Docking Station</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Keyboard">Keyboard</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Monitor">Monitor</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Mouse">Mouse</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Network Storage Device (networked hard drive)">Network Storage Device (networked hard drive)</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Printer/Multi-Function Printer/Print Server">Printer/Multi-Function Printer/Print Server</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Projector">Projector</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Tablet (Wi-Fi and other)">Tablet (Wi-Fi and other)</asp:ListItem>
                                    <asp:ListItem optgroup="Computing & Peripherals " Value="Tablet (Wi-Fi only)">Tablet (Wi-Fi only)</asp:ListItem>
                                    <asp:ListItem optgroup="Handsets " Value="Phone, multi-mode (Wi-fi and other)">Phone, multi-mode (Wi-fi and other)</asp:ListItem>
                                    <asp:ListItem optgroup="Handsets " Value="Phone, single-mode (Wi-Fi only)">Phone, single-mode (Wi-Fi only)</asp:ListItem>
                                    <asp:ListItem optgroup="Automotive & Transportation " Value="In-vehicle Network">In-vehicle Network</asp:ListItem>
                                    <asp:ListItem optgroup="Automotive & Transportation " Value="Transportation Management">Transportation Management</asp:ListItem>
                                    <asp:ListItem optgroup="Health & Fitness " Value="Medical/Fitness Device">Medical/Fitness Device</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Appliances">Appliances</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Home Energy Management">Home Energy Management</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Home Security and Control">Home Security and Control</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Hot Water Heater">Hot Water Heater</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Refrigerator">Refrigerator</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Thermostat">Thermostat</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Transmission and Distribution Equipment">Transmission and Distribution Equipment</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Utility Meter">Utility Meter</asp:ListItem>
                                    <asp:ListItem optgroup="Smart Energy " Value="Washer/Dryer">Washer/Dryer</asp:ListItem>
                                    <asp:ListItem optgroup="Other " Value ="Embedded Sensor">Embedded Sensor</asp:ListItem>
                                    <asp:ListItem optgroup="Other " Value="Industrial (communications & input)">Industrial (communications & input)</asp:ListItem>
                                    <asp:ListItem optgroup="Other " Value="Other">Other</asp:ListItem>
                                    
                            </asp:DropDownList>
                        </td>
                        <%--<td id="divSTAS" runat ="server">
                            <select id ="ddlSTAS" name="ddlSTAS1">
                                <optgroup label = "Reference Design and Modules">
                                    <option value ="Embedded Module">Embedded Module</option>
                                    <option value ="Reference Design">Reference Design</option>
                                </optgroup>
                                <optgroup label = "Consumer Electronics">
                                    <option value ="Digital Audio - Portable (MP3 player)">Digital Audio - Portable (MP3 player)</option>
                                    <option value ="Digital Audio - Stationary (speakers, receiver, MP3 player)">Digital Audio - Stationary (speakers, receiver, MP3 player)</option>
                                    <option value ="eReader">eReader</option>
                                    <option value ="Game Console or Game Console Adapter">Game Console or Game Console Adapter</option>
                                    <option value ="Gaming Device - Portable">Gaming Device - Portable</option>
                                    <option value ="Media Adapter">Media Adapter</option>
                                    <option value ="Media Remote/Input Device">Media Remote/Input Device</option>
                                    <option value ="Media Server">Media Server</option>
                                    <option value ="Navigation/GPS">Navigation/GPS</option>
                                    <option value ="Picture Frame">Picture Frame</option>
                                    <option value ="Set Top box, Media Extender (includes players & recorders)">Set Top box, Media Extender (includes players & recorders)</option>
                                    <option value ="Still Camera">Still Camera</option>
                                    <option value ="Television">Television</option>
                                    <option value ="Video Camera">Video Camera</option>
                                    <option value ="Web Camera">Web Camera</option>
                                </optgroup>
                                <optgroup label = "Computing & Peripherals">
                                    <option value ="Docking Station">Docking Station</option>
                                    <option value ="Keyboard">Keyboard</option>
                                    <option value ="Monitor">Monitor</option>
                                    <option value ="Mouse">Mouse</option>
                                    <option value ="Network Storage Device (networked hard drive)">Network Storage Device (networked hard drive)</option>
                                    <option value ="Printer/Multi-Function Printer/Print Server">Printer/Multi-Function Printer/Print Server</option>
                                    <option value ="Projector">Projector</option>
                                    <option value ="Tablet (Wi-Fi and other)">Tablet (Wi-Fi and other)</option>
                                    <option value ="Tablet (Wi-Fi only)">Tablet (Wi-Fi only)</option>
                                </optgroup>
                                <optgroup label = "Handsets">
                                    <option value ="Phone, multi-mode (Wi-fi and other)">Phone, multi-mode (Wi-fi and other)</option>
                                    <option value ="Phone, single-mode (Wi-Fi only)">Phone, single-mode (Wi-Fi only)</option>
                                </optgroup>
                                <optgroup label = "Automotive & Transportation">
                                    <option value ="In-vehicle Network">In-vehicle Network</option>
                                    <option value ="Transportation Management">Transportation Management</option>
                                </optgroup>
                                <optgroup label = "Health & Fitness">
                                    <option value ="Medical/Fitness Device">Medical/Fitness Device</option>
                                </optgroup>
                                <optgroup label = "Smart Energy">
                                    <option value ="Appliances">Appliances</option>
                                    <option value ="Home Energy Management">Home Energy Management</option>
                                    <option value ="Home Security and Control">Home Security and Control</option>
                                    <option value ="Hot Water Heater">Hot Water Heater</option>
                                    <option value ="Refrigerator">Refrigerator</option>
                                    <option value ="Thermostat">Thermostat</option>
                                    <option value ="Transmission and Distribution Equipment">Transmission and Distribution Equipment</option>
                                    <option value ="Utility Meter">Utility Meter</option>
                                    <option value ="Washer/Dryer">Washer/Dryer</option>
                                </optgroup>
                                <optgroup label = "Other">
                                    <option value ="Embedded Sensor">Embedded Sensor</option>
                                    <option value ="Industrial (communications & input)">Industrial (communications & input)</option>
                                    <option value ="Other">Other</option>
                                </optgroup>
                            </select>                        
                        </td>--%>
                    </tr>
                </table>
                                 </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlProductType" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="listLeft" 
                                EventName="SelectedIndexChanged" />

                </Triggers>                    

        </asp:UpdatePanel>
            </div>
            <div id="tabs1-3">
                 <table>
                    <tr>
                        <td colspan =4>
                            <asp:Label ID="Label20" runat="server" Text="Select at least one band："></asp:Label>
                            <asp:Label ID="Label47" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                            <asp:CheckBox ID="chkBand" runat="server" Text="2.4 GHz"/>&nbsp;&nbsp;
                            <asp:CheckBox ID="chkBand1" runat="server" Text="5 GHz"/>&nbsp;&nbsp;
                            <asp:CheckBox ID="chkBand2" runat="server" Text="WiGig"/>
                        </td>
                    </tr>
                    
                        <tr>

                            <td>
                                <asp:Label ID="Label22" runat="server" Text="Mandatory Program"></asp:Label>
                                <asp:Label ID="Label48" runat="server" Text="*" ForeColor="#FF3300"></asp:Label> <br />                      
                                <asp:RadioButton ID="rdoMandatory" runat="server" Text="Wi-Fi CERTIFIED ac & n" GroupName="1" Width="300px"/> &nbsp;&nbsp;                      
                            </td>
                            <td>              
                                <br />         
                                <asp:RadioButton ID="rdoMandatory1" runat="server" Text="Wi-Fi CERTIFIED n" GroupName="1" Width="300px"/>  &nbsp;&nbsp;&nbsp;                     
                            </td>
                            <td>   
                                <br />                             
                                <asp:RadioButton ID="rdoMandatory2" runat="server" Text="Wi-Fi CERTIFIED 6" GroupName="1" Width="300px"/>                       
                            </td>
                        </tr>
                    <table style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all">
                        <tr>
                            <td>                                                 
                                <asp:Label ID="lblMandatory" runat="server" Text="" Width="300px"></asp:Label>      &nbsp;&nbsp;                 
                            </td>
                            <td>                      
                                <asp:Label ID="lblMandatory1" runat="server" Text="" Width="300px"></asp:Label>       &nbsp;&nbsp;                 
                            </td>
                            <td>                              
                                <asp:Label ID="lblMandatory2" runat="server" Text="" Width="300px"></asp:Label>                          
                            </td>
                        </tr>
                    </table>
                 </table>

            </div>
            <div id="tabs1-4">
                <asp:updatepanel id="UpdatePanel2" runat="server">
                    <ContentTemplate>
                 <table border=2>
                    <tr>
                    
			            <td>
			                <asp:ListBox ID="listLeft" runat="server" Height="237px" Width="321px" 
                                DataTextField="Name" DataValueField="id" SelectionMode="Multiple" OnSelectedIndexChanged="listLeft_SelectedIndexChanged"></asp:ListBox>
				        </td>
			            <td>
				            <asp:Button ID="btnRight" runat="server" Text=">" OnClick="btnRight_Click" Width="30px" /><br /><br />
				            <asp:Button ID="btnLeft" runat="server" Text="<" OnClick="btnLeft_Click" Width="30px" />
			            </td>
			            <td>
				            <asp:ListBox ID="listRight" runat="server" Width="321" Height="237" 
                                SelectionMode="Multiple" 
                                DataTextField="Name" 
                                DataValueField="id" ></asp:ListBox>
                            
			            </td>            
                                
                   
                    </tr>  
                    <tr>
                        <td colspan =3 align =center >
                            
                            <%--<asp:Button ID="btnApparatus" runat="server" Text="確定" OnClick="btnApparatus_Click"/>--%>
                            
                        </td>
                    </tr>
                 </table>  
                <table>
                    <tr>
                        <td>
                            <%--<asp:Label ID="Label23" runat="server" Text="test"></asp:Label>--%>
                            <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label> 
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRight" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLeft" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="listLeft" EventName="SelectedIndexChanged" />

                    </Triggers>
                </asp:updatepanel>
            </div>
            <div id="tabs1-5">
                <asp:Label ID="Label23" runat="server" Text="Please choose  the following capabilities for your product."></asp:Label>
                <br />
                <br />
                 <table>
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                                <font size="3"><b>Supported Spatial Streams</b></font></td>
                    </tr> 
                    <tr>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text="Transmit(Tx)"></asp:Label> &nbsp;&nbsp;  
                            <asp:Label ID="Label25" runat="server" Text="2.4 GHz"></asp:Label> 
                            <asp:DropDownList ID="ddlStream_T_2" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;  
                            <asp:Label ID="Label26" runat="server" Text="5 GHz"></asp:Label> 
                            <asp:DropDownList ID="ddlStream_T_5" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label27" runat="server" Text="Receive(Rx)"></asp:Label> &nbsp;&nbsp;&nbsp;  
                            <asp:Label ID="Label28" runat="server" Text="2.4 GHz"></asp:Label> 
                            <asp:DropDownList ID="ddlStream_R_2" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;  
                            <asp:Label ID="Label29" runat="server" Text="5 GHz"></asp:Label> 
                            <asp:DropDownList ID="ddlStream_R_5" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label30" runat="server" 
                                Text="802.11ac devices supporting 4 or more Spatial Streams will be tested up to 4 Spatial Streams for Receive (Rx) and up to 3 Spatial Streams for Transmit (Tx)" 
                                ForeColor="Red"></asp:Label> 
                        </td>
                    </tr>
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                                <font size="3"><b>Additional Capabilities</b></font></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkAdditional" runat="server" Text="Power saving features" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkAdditional1" runat="server" Text="Wi-Fi Enhanced Open" />                            
                        </td>
                    </tr>
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                                <font size="3"><b>Security Type</b></font></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSecurity" runat="server" Text="WPA" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSecurity1" runat="server" Text="WPA2" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSecurity2" runat="server" Text="WPA3" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSecurity3" runat="server" Text="WEP Support" />                            
                        </td>
                    </tr>
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                                <font size="3"><b>Spectrum and Regulatory Features</b></font></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSpectrum" runat="server" Text="802.11h" />                            
                        </td>
                    </tr>
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                                <font size="3"><b>11n Optional Feature</b></font></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional" runat="server" Text="Short Guard Interval 20 MHz" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional1" runat="server" Text="Short Guard Interval 40 MHz" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional2" runat="server" Text="TX A-MPDU" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional3" runat="server" Text="STBC" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional4" runat="server" Text="40MHz operation in 2.4GHz with coexistence mechanisms" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional5" runat="server" Text="40MHz operation in 5GHz" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional6" runat="server" Text="HT Duplicate Mode (MCS 32)" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional7" runat="server" Text="OBSS on Extension Channel" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11nOptional8" runat="server" Text="STAUT Power Management" />                            
                        </td>
                    </tr>
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                                <font size="3"><b>11ac Optional Feature</b></font></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional" runat="server" Text="Rx MCS 8 (256-QAM)" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional1" runat="server" Text="Rx MCS 8-9 (256-QAM)" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional2" runat="server" Text="Rx Short Guard Interval" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional3" runat="server" Text="STBC 2x1" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional4" runat="server" Text="Rx A-MPDU of A-MSDU" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional5" runat="server" Text="Tx LDPC" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional6" runat="server" Text="Rx LDPC" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional7" runat="server" Text="Tx SU beamformee / beamformer" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional8" runat="server" Text="DL MU-MIMO" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional9" runat="server" Text="RTS with BW Signaling" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional10" runat="server" Text="Rx 160 MHz operations" />                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chk11acOptional11" runat="server" Text="Extended 5 GHz Channel Support" />                            
                        </td>
                    </tr>

                 </table>
            </div>
        </div>
        <table  width ="100%">
            <tr>
                        <td align ="center">
                            <br />
                            <br />
                            <asp:Label ID="Label58" runat="server" 
                                Text="*為必填項目" Font-Bold="True" Font-Size="Large" 
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
        </table>
        <div align ="center" style="COLOR: red">
                    
                    <br />
                    <br />
                        
                        <asp:Button ID="butOK" runat="server" Text="確定" 
                                onclick="butOK_Click" Width="59px" Height="30px" />
                               
                        
                    <br />
                    <br />
            </div>


    </form>
</body>
</html>
