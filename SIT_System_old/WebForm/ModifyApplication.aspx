<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyApplication.aspx.cs" Inherits="WebForm_ModifyApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
  <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
  <script src="../js/jquery_1.11.0.min.js"></script>
   

 

    <style>
    /* Adjust the jQuery UI widget font-size: */
    .ui-widget {
        font-size: 0.95em;
    }
    </style>

    <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../css/jquery.fileupload.css" />
    <link rel="stylesheet" href="../css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-noscript.css" /></noscript>
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-ui-noscript.css" /></noscript>   
    
    <script>
           $(function() {
               $("#tabs1").tabs();
           });
    </script>          

    <fieldset>   
         <%--<table id="Table11" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[建立申請單]</asp:LinkButton>
                     &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lblModify" runat="server" OnClick="lbtnModify_Click">[搜尋申請單]</asp:LinkButton>
                </td>

            </tr>
         </table> --%>          
                          
                 
        <font face="verdana"color="0000DD"size="4" ><legend>Application's Information</legend></font>
        <hr size="5" width="100%" color="DDDDDD" style="height: 5px">
    
        <asp:Label ID="Label35" runat="server" Text="*" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label34" runat="server" Text="為必填欄位" ForeColor="Blue"></asp:Label>
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        
        
            <%--<tr>
                <td style="width: 23%">
                    
                    <asp:Label ID="Label1" runat="server" Text="受理對象"></asp:Label>
                    
                </td>
                <td colspan=2>
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdoAcceptT" runat="server" Text="台北" GroupName="2" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoAcceptW" runat="server" Text="吳江" GroupName="2" />
                </td>
                
            </tr>--%>
            <tr>
                <td rowspan=3 valign=middle>
                    <asp:Label ID="Label2" runat="server" Text="申請者資訊"></asp:Label>
                </td>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="姓名："></asp:Label>
                    
                    <%--<asp:Label ID="lblName" runat="server"></asp:Label>--%>
                    
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="部門："></asp:Label>
                    <%--<asp:Label ID="lblDepartment" runat="server"></asp:Label>--%>
                    <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="分機："></asp:Label>
                    <%--<asp:Label ID="lblExt" runat="server"></asp:Label>--%>
                    <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
                    <asp:Label ID="Label26" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Email："></asp:Label>
                    <%--<asp:Label ID="lblEmail" runat="server"></asp:Label>--%>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                
                <td>
                    
                    <asp:Label ID="Label8" runat="server" Text="期望完成日："></asp:Label>
                    
                    <input type="text" id="datepicker" name = "date1" value = "<%=strExpect%>" />
                    <asp:Label ID="Label28" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                     <script>
                         $(function() {
                             $("#datepicker").datepicker();
                         });
                    
                     </script>
                     

<%--                     <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                     <script >
                         $("#TextBox1").value() = $("#datepicker").value();
                     </script>--%>
                </td>
             <td>
                    
                    <asp:Label ID="Label55" runat="server" Text="申請部門："></asp:Label>    
                    <asp:DropDownList ID="ddlDepartment2" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label56" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
            
            <tr>
                <td rowspan=3 valign=middle>
                    
                    <asp:Label ID="Label9" runat="server" Text="聯絡資訊"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="Label10" runat="server" Text="客戶：" ></asp:Label>
                    
                    <%--<asp:DropDownList ID="ddlCustomer" runat="server">
                    </asp:DropDownList>--%>
                    
                    <asp:TextBox ID="txtCustomer1" runat="server"></asp:TextBox>
                    <asp:Label ID="Label29" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="Label11" runat="server" Text="PM/Sales："></asp:Label>
                    
                    <asp:TextBox ID="txtPM" runat="server"></asp:TextBox>
                    <asp:Label ID="Label30" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="S/W Engineer："></asp:Label>
                    
                    
                    <asp:TextBox ID="txtSW" runat="server"></asp:TextBox>
                    <asp:Label ID="Label31" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                    
                </td>
                <td>
                
                    <asp:Label ID="Label13" runat="server" Text="H/W Engineer："></asp:Label>
                
                    <asp:TextBox ID="txtHW" runat="server"></asp:TextBox>
                    <asp:Label ID="Label36" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="Mechanical Engineer：" 
                        ></asp:Label>
                    
                    <asp:TextBox ID="txtMechanical" runat="server"></asp:TextBox>
                    <asp:Label ID="Label37" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="Label51" runat="server" Text="DQA負責人：" 
                        ></asp:Label>
                    
                    <asp:DropDownList ID="ddlDQA" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label54" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td rowspan=10 valign=middle>
                    
                    <asp:Label ID="Label15" runat="server" Text="產品相關資訊"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="Label16" runat="server" Text="ASKEY's Model Name："></asp:Label>
                    
                </td>
                <td>
                
                    <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>
                    <asp:Label ID="Label38" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="F/W Version："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFW" runat="server"></asp:TextBox>
                    <asp:Label ID="Label39" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="Wireless Drive："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWireless" runat="server"></asp:TextBox>
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Customer's Product Name："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
                    <asp:Label ID="Label40" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>                       
            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="NPI Stage (ES/EV/DV)："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNPI" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label41" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="H/W Version："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPCB" runat="server"></asp:TextBox>
                    <asp:Label ID="Label43" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>             
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="Chipset："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBOM" runat="server"></asp:TextBox>
                    <asp:Label ID="Label42" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>             
            <tr>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="Sample MAC Address："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMAC" runat="server"></asp:TextBox>
                </td>
            </tr>             
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="Utility Version："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUtility" runat="server"></asp:TextBox>
                </td>
            </tr>             
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="DSP Model："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPart" runat="server"></asp:TextBox>
                </td>
            </tr>             
            
            <tr>
                <td style="width: 23%">
                    
                    <asp:Label ID="Label32" runat="server" Text="預計Sample Ready日期："></asp:Label>
                    <asp:Label ID="lblKind1" runat="server" Text="" Visible="False"></asp:Label>
                </td>
                <td colspan=2>
                    <input type="text" id="datepicker1" name = "date2" value = "<%=strReady%>" />
                     <script>
                         $(function() {
                             $("#datepicker1").datepicker();
                         });
                     </script>                   
                </td>
                
            </tr>  
            <td style="width: 23%">
                    
                    <asp:Label ID="Label33" runat="server" Text="備註："></asp:Label>
                    
                </td>
                <td colspan=2>
                   
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
                
            </tr>                      
                        
        </table> 
        <%--<table> --%>
        <div id="tabs1">
          <ul>
            <%--<li><a href="#tabs-1">設備資源</a></li>--%>
            <li><a href="#tabs-5">檔案上傳</a></li>
            <li><a href="#tabs-6">檔案清單</a></li>
            <%--<li><a href="#tabs-4">申請單附件</a></li>--%>
          </ul>
          <%--<div id="tabs-1">
            <p></p>
          </div>--%>
          <div id="tabs-5">
            <table>
                <tr>
                    <td>
                        
                        <form style="visibility :hidden">
                            <label style="visibility :hidden" for="theme-switcher">Theme:</label>
                            <select style="visibility :hidden" id="theme-switcher" class="pull-right" >
                                <option value="dark-hive" selected>Dark Hive</option>
                            </select>
                        </form>
                         <form id="fileupload" action="UploadProgress.aspx" method="POST" enctype="multipart/form-data">

                                <div class="row fileupload-buttonbar">
                                    <div class="col-lg-7">
                                        <!-- The fileinput-button span is used to style the file input field as button -->
                                        <span class="btn btn-success fileinput-button">
                                            <i class="glyphicon glyphicon-plus"></i>
                                            <span>選擇檔案</span>
                                            <input type="file" name="files[]" multiple>
                                        </span>
                                    </div>
                                    <!-- The global progress state -->
                                <div class="fileupload-progress fade" style="display:none">
                                    <!-- The global progress bar -->
                                    <div class="progress" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                    <!-- The extended global progress state -->
                                    <div class="progress-extended">&nbsp;</div>
                                </div>
                                </div>
                                <!-- The table listing the files available for upload/download -->
                                <table role="presentation" class="table table-striped"><tbody class="files"></tbody></table>
                                       
                            </form>
                            <br />
                            <asp:Label ID="Label44" runat="server" Text="*上傳後請按確定！" ForeColor="Red"></asp:Label>
                            
                     </td>        
                </tr>
            </table> 

          </div>
          <div id="tabs-6">
            <table id="Table5" border="0" cellpadding="5" cellspacing="5" width="100%">
                <tr>
                    <td align ="center">
                         
                        
                        <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                                Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDeleting="gvwMain_RowDeleting">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemTemplate>
                                            &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                                Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="seq" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
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

        </div>

        
        <br>
        <br>
        <script>

            $(function() {
                $("#accordion").accordion({
                    heightStyle: "content"
                });
            });
        </script>
        
  <script>
      $(function() {
          $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
          $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
      });
  </script>
  <style>
  .ui-tabs-vertical { width: 90em; }
  .ui-tabs-vertical .ui-tabs-nav { padding: .2em .1em .2em .2em; float: left; width: 12em; }
  .ui-tabs-vertical .ui-tabs-nav li { clear: left; width: 100%; border-bottom-width: 1px !important; border-right-width: 0 !important; margin: 0 -1px .2em 0; }
  .ui-tabs-vertical .ui-tabs-nav li a { display:block; }
  .ui-tabs-vertical .ui-tabs-nav li.ui-tabs-active { padding-bottom: 0; padding-right: .1em; border-right-width: 1px; border-right-width: 1px; }
  .ui-tabs-vertical .ui-tabs-panel { padding: 1em; float: right; width: 75em;}
  </style>        
        
  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional">
    <ContentTemplate> 
    <table id="Table2" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <font face="verdana"color="0000DD"size="10" ><legend>
            <caption>
                測試項目</caption>
        </legend></font>
        <tr>
            <td>
                <asp:Label ID="Label531" runat="server" Text="部門："></asp:Label>
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>DA40</asp:ListItem>
                    <asp:ListItem>DA40-WJ</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="butReturn" runat="server" Text="原設定項目" 
                onclick="butReturn_Click" />    
            </td>
        </tr>                
        <tr id="customer_t" runat ="server" >
            <td>
                <asp:Label ID="lblCustomer1" runat="server" Text="客戶："></asp:Label>
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                </asp:DropDownList>
                    
            </td>
        </tr>    
        <tr>
            <td align ="center">
                                            
                <asp:GridView ID="gvwList" runat="server" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                    Width="100%" OnPageIndexChanging="gvwList_PageIndexChanging"  OnRowDataBound="gvwList_RowDataBound"
                    OnPreRender ="gvwList_PreRender">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                                <asp:TemplateField>
    <%--                        <headertemplate> 
                                <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="Comply(全選/取消)" ToolTip="按一次全選，再按一次取消全選" /> 
                            </headertemplate>--%>
                                    <itemtemplate> 
                                        <asp:CheckBox ID="CheckBox2" runat="server"/> 
                                    </itemtemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                  
                                <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="功能" ReadOnly="True" SortExpression="Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Item" HeaderText="項目" ReadOnly="True" SortExpression="Item">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="Note" HeaderText="測試內容說明" ReadOnly="True" SortExpression="Note">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                                        
                                <asp:TemplateField HeaderText="測試環境架構" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "ImageView.aspx?id="+Eval("id1")+"&kid="+Eval("kind_id")+"&fid="+Eval("function_id") %>'
                                            Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <%--<ItemTemplate>
                                       <asp:Image ID="img1" runat="server" ImageUrl='<%#"pic/" + Eval("File_Name")%>' width="150px" height="100px" />
                                    </ItemTemplate>--%>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="文件下載" SortExpression="file_tag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemTemplate>
                                        &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name1")+"&path="+Eval("File_Path1") %>'
                                            Target="_blank" Text='<%# Bind("File_Name1") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>       
                                <asp:BoundField DataField="Level1" HeaderText="EV to DV NPI" ReadOnly="True" SortExpression="Level1">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Level2" HeaderText="DV to PV NPI" ReadOnly="True" SortExpression="Level2">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cost" HeaderText="Lead Time" ReadOnly="True" SortExpression="Cost">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                    <RowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                                   
            </td> 
        </tr> 
    </table>         
    </ContentTemplate>
                

</asp:UpdatePanel>
          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode ="Conditional">
            <ContentTemplate>  
                <table width ="100%">   
                    <tr id = "Certification1" runat ="server">
                        <td align ="center">
                            <br />
                            <br />
                            <asp:Label ID="Label57" runat="server" 
                                Text="請點選以下選項，填寫相關資訊！" Font-Bold="True" Font-Size="Large" 
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr> 
                    <tr id = "Certification3" runat ="server">
                        <td align ="center">
                            <asp:Button ID="btnWifi" runat="server" Text="Wifi Certification 申請單" 
                    onclick="butWifi_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnBT" runat="server" Text="BT Certification 申請單" 
                onclick="btnBT_Click" />                    
                        </td>
                    </tr>
                    
                    <tr id = "Certification2" runat ="server">
                        <td align ="center">
                            <br />
                            <br />
                            <asp:Label ID="Label58" runat="server" 
                                Text="按下送出鈕，申請單才會正式送到SIT，暫存鈕只能暫時儲存目前所填寫的資訊" Font-Bold="True" Font-Size="Large" 
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align ="center" style="COLOR: red">
                            <br />
                            <br />
                            <asp:Button ID="butTemporarily" runat="server" Text="暫存" 
                onclick="butTemporarily_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                            <asp:Button ID="butOK" runat="server" Text="送出" 
                onclick="butOK_Click" />
                                
                            <br />
                            <br />
                        </td>
                    </tr>                    
                </table>
               </ContentTemplate>
                    
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBT" EventName="Click" />
                <%--<asp:PostBackTrigger ControlID ="btnBT" />--%>
            </Triggers>
        </asp:UpdatePanel>  
          
        </div> 
        
        
        
       

<script id="template-upload" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-upload fade">
        <td>
            <span class="preview"></span>
        </td>
        <td>
            <p class="name">{%=file.name%}</p>
            <strong class="error"></strong>
        </td>
        <td>
            <p class="size">Processing...</p>
            <div class="progress"></div>
        </td>
        <td>
            {% if (!i && !o.options.autoUpload) { %}
                <button class="start" disabled>Start</button>
            {% } %}
            {% if (!i) { %}
                <button class="cancel">Cancel</button>
            {% } %}
        </td>
    </tr>
{% } %}
</script>
<!-- The template to display files available for download -->
<script id="template-download" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-download fade">
        <td>
            <span class="preview">
                {% if (file.thumbnailUrl) { %}
                    <a href="{%=file.url%}" title="{%=file.name%}" download="{%=file.name%}" data-gallery><img src="{%=file.thumbnailUrl%}"></a>
                {% } %}
            </span>
        </td>
        <td>
            <p class="name">
                <a href="{%=file.url%}" title="{%=file.name%}" download="{%=file.name%}" {%=file.thumbnailUrl?'data-gallery':''%}>{%=file.name%}</a>
            </p>
            {% if (file.error) { %}
                <div><span class="error">Error</span> {%=file.error%}</div>
            {% } %}
        </td>
        <td>
            <span class="size">{%=o.formatFileSize(file.size)%}</span>
        </td>
        <td>
            <button class="delete" data-type="{%=file.deleteType%}" data-url="{%=file.deleteUrl%}"{% if (file.deleteWithCredentials) { %} data-xhr-fields='{"withCredentials":true}'{% } %}>Delete</button>
            <input type="checkbox" name="delete" value="1" class="toggle">
        </td>
    </tr>
{% } %}
</script>   
<%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
<script src="../js/jquery.min.js"></script>
<%--<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>--%>
<script src="../js/jquery-ui.min.js"></script>
<!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
<!-- jquery.ui.widget.js 不能省略，否則已上傳及要上傳之清單會看不到-->
<script src="../js/vendor/jquery.ui.widget.js"></script>
<!-- The Templates plugin is included to render the upload/download listings -->
<%--<script src="//blueimp.github.io/JavaScript-Templates/js/tmpl.min.js"></script>--%>
<script src="../js/tmpl.min.js"></script>
<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
<%--<script src="//blueimp.github.io/JavaScript-Load-Image/js/load-image.all.min.js"></script>--%>
<script src="../js/load-image.all.min.js"></script>
<!-- The Canvas to Blob plugin is included for image resizing functionality -->
<%--<script src="//blueimp.github.io/JavaScript-Canvas-to-Blob/js/canvas-to-blob.min.js"></script>--%>
<script src="../js/canvas-to-blob.min.js"></script>
<!-- blueimp Gallery script -->
<%--<script src="//blueimp.github.io/Gallery/js/jquery.blueimp-gallery.min.js"></script>--%>
<script src="../js/jquery.blueimp-gallery.min.js"></script>
<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
<script src="../js/jquery.iframe-transport.js"></script>
<!-- The basic File Upload plugin -->
<script src="../js/jquery.fileupload.js"></script>
<!-- The File Upload processing plugin -->
<script src="../js/jquery.fileupload-process.js"></script>
<!-- The File Upload image preview & resize plugin -->
<script src="../js/jquery.fileupload-image.js"></script>
<!-- The File Upload audio preview plugin -->
<script src="../js/jquery.fileupload-audio.js"></script>
<!-- The File Upload video preview plugin -->
<script src="../js/jquery.fileupload-video.js"></script>
<!-- The File Upload validation plugin -->
<script src="../js/jquery.fileupload-validate.js"></script>
<!-- The File Upload user interface plugin -->
<script src="../js/jquery.fileupload-ui.js"></script>
<!-- The File Upload jQuery UI plugin -->
<script src="../js/jquery.fileupload-jquery-ui.js"></script>
<script>    var fileuploadurl = "UploadProgress.aspx";</script>
<!-- The main application script -->
<script src="../js/main.js"></script>   

<%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>
<!-- The Templates plugin is included to render the upload/download listings -->
<script src="//blueimp.github.io/JavaScript-Templates/js/tmpl.min.js"></script>
<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
<script src="//blueimp.github.io/JavaScript-Load-Image/js/load-image.all.min.js"></script>
<!-- The Canvas to Blob plugin is included for image resizing functionality -->
<script src="//blueimp.github.io/JavaScript-Canvas-to-Blob/js/canvas-to-blob.min.js"></script>
<!-- blueimp Gallery script -->
<script src="//blueimp.github.io/Gallery/js/jquery.blueimp-gallery.min.js"></script>
<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
<script src="../js/jquery.iframe-transport.js"></script>
<!-- The basic File Upload plugin -->
<script src="../js/jquery.fileupload.js"></script>
<!-- The File Upload processing plugin -->
<script src="../js/jquery.fileupload-process.js"></script>
<!-- The File Upload image preview & resize plugin -->
<script src="../js/jquery.fileupload-image.js"></script>
<!-- The File Upload audio preview plugin -->
<script src="../js/jquery.fileupload-audio.js"></script>
<!-- The File Upload video preview plugin -->
<script src="../js/jquery.fileupload-video.js"></script>
<!-- The File Upload validation plugin -->
<script src="../js/jquery.fileupload-validate.js"></script>
<!-- The File Upload user interface plugin -->
<script src="../js/jquery.fileupload-ui.js"></script>
<!-- The File Upload jQuery UI plugin -->
<script src="../js/jquery.fileupload-jquery-ui.js"></script>
<script>    var fileuploadurl = "UploadProgress.aspx";</script>
<!-- The main application script -->
<script src="../js/main.js"></script>
<script>
    // Initialize the jQuery UI theme switcher:
    $('#theme-switcher').change(function() {
        var theme = $('#theme');
        theme.prop(
        'href',
        theme.prop('href').replace(
            /[\w\-]+\/jquery-ui.css/,
            $(this).val() + '/jquery-ui.css'
        )
    );
    });
</script>--%>
                                     
            
        
                                     
            
    </fieldset>

</asp:Content>

