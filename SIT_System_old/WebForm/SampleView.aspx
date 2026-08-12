<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="SampleView.aspx.cs" Inherits="WebForm_SampleView" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
	<link href="../css/shCore.css" rel="stylesheet" type="text/css" />
    <link href="../css/shThemeDefault.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="../css/flexslider.css" type="text/css" media="screen" />
	
	<script>
      $(function() {
          $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
          $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
      });
  </script>
  <style>
  .ui-tabs-vertical { width: 85em; }
  .ui-tabs-vertical .ui-tabs-nav { padding: .2em .1em .2em .2em; float: left; width: 12em; }
  .ui-tabs-vertical .ui-tabs-nav li { clear: left; width: 100%; border-bottom-width: 1px !important; border-right-width: 0 !important; margin: 0 -1px .2em 0; }
  .ui-tabs-vertical .ui-tabs-nav li a { display:block; }
  .ui-tabs-vertical .ui-tabs-nav li.ui-tabs-active { padding-bottom: 0; padding-right: .1em; border-right-width: 1px; border-right-width: 1px; }
  .ui-tabs-vertical .ui-tabs-panel { padding: 1em; float: right; width: 65em;}
  </style>
  
    <fieldset>
        <table id="Table2" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[修改樣品資訊]</asp:LinkButton>
                </td>
                <td align ="right">
                    <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnDel_Click">[刪除此樣品]</asp:LinkButton>
                </td>
            </tr>         
         </table> 
         <br />
        <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td colspan=2 style="width:50px">
                    <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>
                </td>
                <td>
                    
                    <asp:Label ID="lblKind" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label2" runat="server" Text="功能"></asp:Label>
                </td>
                <td>
                    
                    <asp:Label ID="lblFunction" runat="server" Text=""></asp:Label> 
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label3" runat="server" Text="項目"></asp:Label>
                </td>
                <td>
                    
                    <asp:Label ID="lblItem" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label4" runat="server" Text="系統編號"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
                    
                </td>

            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label9" runat="server" Text="品名"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label23" runat="server" Text="品名代碼"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblNameCode" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label10" runat="server" Text="Vendor"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblVendor" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label5" runat="server" Text="Model Name"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label6" runat="server" Text="MAC Address"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblMAC" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label7" runat="server" Text="PHY driver vesion"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblPhy" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label8" runat="server" Text="Firmware version"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblFirmware" runat="server" Text=""></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td rowspan=7 valign=middle style="width:80px">
                    
                    <asp:Label ID="Label11" runat="server" Text="Interface"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="Physical"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblPhysical" runat="server" Text=""></asp:Label>
                    
                </td>

            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label13" runat="server" Text="VoIP"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblVoip" runat="server" Text=""></asp:Label>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="CATV"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblCATV" runat="server" Text=""></asp:Label>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label15" runat="server" Text="USB"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblUSB" runat="server" Text=""></asp:Label>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label16" runat="server" Text="LAN"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblLAN" runat="server" Text=""></asp:Label>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label17" runat="server" Text="WLAN"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblWLAN" runat="server" Text=""></asp:Label>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label18" runat="server" Text="WPS"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblWPS" runat="server" Text=""></asp:Label>
                    
                </td>            
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label19" runat="server" Text="樣品狀態"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label20" runat="server" Text="放置地點"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label25" runat="server" Text="保管部門"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDep" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label21" runat="server" Text="保管人"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label24" runat="server" Text="保管代理人"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblAgent" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label22" runat="server" Text="備註"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=3>
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                        Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Upload_Date" HeaderText="上傳時間" ReadOnly="True" SortExpression="Upload_Date">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>  
                            <asp:BoundField DataField="Upload_Emp" HeaderText="上傳者" ReadOnly="True" SortExpression="Upload_Emp">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>                                                         
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                        Text="刪除" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>                            
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            </table>
            </div>
    
   <section class="slider">
        <div class="flexslider">
          <ul id="slides" class="slides">

          </ul>
        </div>
      </section> 
      <script defer src="../js/jquery.flexslider.js"></script>
      
      <script type="text/javascript">
          $(function() {
              SyntaxHighlighter.all();
          });
          $(window).load(function() {

              var iCount = '<%= intCount %>'
              var strI = '<%= strFilePathNames %>'
              //		        var strFile = new Array('<%= strFilePathNames %>');
              var strFile = strI.split(",");
              //		        strFile = '<%= strFilePathNames %>'
              //		        document.write(strFile[0]);
              var obj = document.getElementById("slides");
              for (i = 0; i < iCount; i++) {

                  var liobj = document.createElement("li");
                  var aimg = document.createElement("img");
                  var message = document.createTextNode("");

                  aimg.appendChild(message);
                  aimg.src = "pic/" + strFile[i];

                  liobj.appendChild(aimg);
                  obj.appendChild(liobj);
              }


              $('.flexslider').flexslider({
                  animation: "slide",
                  start: function(slider) {
                      $('body').removeClass('loading');
                  }
              });
          });
  </script>
        <table id="Table5" class="one" width="100%">    
            <tr>
            <td align ="center" colspan = 5 style="COLOR: red">
                <br />
                <br />
                    
                <%--<asp:Button ID="butOK" runat="server" Text="確定" 
                            onclick="butOK_Click" />--%>
                <%--&nbsp;&nbsp;&nbsp;&nbsp; --%>           
                <asp:Button ID="butReturn" runat="server" Text="上一頁" 
                            onclick="butReturn_Click" />                            
                    
                <br />
                <br />
            </td>
        </tr>
        </table> 
    </fieldset>
</asp:Content>

