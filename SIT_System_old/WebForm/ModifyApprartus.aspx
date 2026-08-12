<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyApprartus.aspx.cs" Inherits="WebForm_ModifyApprartus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
    <script src="../js/jquery_1.11.0.min.js"></script>
    <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../css/jquery.fileupload.css" />
    <link rel="stylesheet" href="../css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-noscript.css" /></noscript>
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-ui-noscript.css" /></noscript>
    
    <script>
          $(function() {
              $("#tabs").tabs();
          });
    </script>    
  
    <fieldset>
        <font face="verdana"color="0000DD"size="4" ><legend>新增設備</legend></font>
        <hr size="5" width="100%" color="DDDDDD" style="height: 5px">
        <asp:Label ID="Label35" runat="server" Text="*" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label34" runat="server" Text="為必填欄位" ForeColor="Blue"></asp:Label>        
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="財產編號"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtProductID" runat="server" Width="324px"></asp:TextBox>
                
                </td>
            
            </tr>        
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="設備名稱(中文)"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtName" runat="server" Width="324px"></asp:TextBox>
                    <asp:Label ID="Label27" runat="server" Text="*" ForeColor="Red"></asp:Label>
                
                </td>
            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="設備名稱(英文)"></asp:Label>
                
                </td>
                <td>                
                    <asp:TextBox ID="txtName_En" runat="server" Width="324px"></asp:TextBox>
                    <%--<asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                
                </td>
            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label2" runat="server" Text="類別"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="料號"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtPart_No" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>  
            <tr>
                <td>
                    
                    <asp:Label ID="Label28" runat="server" Text="廠商(中/英文)"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtMF" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label30" runat="server" Text="廠商貨品編號"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtMF_Number" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>          
            <tr>
                <td>
                    
                    <asp:Label ID="Label3" runat="server" Text="廠牌"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtBrand" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="型號"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModel" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="序號"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumber" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="對應採購"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProcurement_staff" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="IMEI"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIMEI" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>             
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="OS"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOS" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>IOS</asp:ListItem>
                        <asp:ListItem>Android</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>   
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="OS Ver."></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVR" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>                       
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="校驗日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker" name = "date1"  value = "<%=strDate1%>">
                    
                     <script>
                         $(function() {
                         $("#datepicker").datepicker();
                     });
                    
                     </script>                    
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="保養日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker1" name = "date2"  value = "<%=strDate2%>">
                     <script>
                         $(function() {
                             $("#datepicker1").datepicker();
                         });
                     </script>                    
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="放置地點"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPlace" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr id="Cost" runat="server">
                <td>
                    <asp:Label ID="lblCost" runat="server" Text="取得成本"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCost" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr id="UseYear" runat="server">
                <td>
                    <asp:Label ID="lblUseYear" runat="server" Text="使用年限"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUseYear" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr id="UseDays" runat="server">
                <td>
                    <asp:Label ID="lblUseDays" runat="server" Text="使用天數"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUseDays" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr id="UsePrice" runat="server">
                <td>
                    <asp:Label ID="lblUsePrice" runat="server" Text="使用價格"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsePrice" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
<%--            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="設備狀態"></asp:Label>
                </td>
                <td style="COLOR: red">
                    
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>閒置中</asp:ListItem>
                        <asp:ListItem>使用中</asp:ListItem>
                        <asp:ListItem>校驗中</asp:ListItem>
                        <asp:ListItem>送修中</asp:ListItem>
                    </asp:DropDownList>*
                    
                </td>                
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="保管部門"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtCustodianD" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>DA40</asp:ListItem>
                        <asp:ListItem>DA40-WJ</asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:Label ID="Label22" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="保管人"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtCustodian" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlCustodian" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    <%--<asp:DropDownList ID="ddlCustodian" runat="server">
                    </asp:DropDownList>--%>
                    
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="保管代理人"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtCustodian" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlCustodian1" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label26" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    
                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="設備狀態"></asp:Label>
                </td>
                <td>
                    <%--0217--%>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <%--<asp:ListItem>閒置中</asp:ListItem>
                        <asp:ListItem>借用中</asp:ListItem>--%>
                        <asp:ListItem>可借用</asp:ListItem>
                        <asp:ListItem>校驗中</asp:ListItem>
                        <asp:ListItem>異常維修中</asp:ListItem>
                        <%--<asp:ListItem>使用中</asp:ListItem>--%>
                        <asp:ListItem>不可借用</asp:ListItem>
                        <asp:ListItem>採購中</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;  
                    <%--0217--%>              
                    <%--<asp:CheckBox ID="chkReservation" runat="server" Text="提供預約" />--%>
                    
                </td>
            </tr>             
            <tr>  
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="Feature"></asp:Label>
                    
                </td>
                <td>
                   
                    <asp:TextBox ID="txtFeature" runat="server" MaxLength="2000" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
                
            </tr> 
            <tr>  
                <td>
                    
                    <asp:Label ID="Label13" runat="server" Text="Specification"></asp:Label>
                    
                </td>
                <td>
                   
                    <asp:TextBox ID="txtSpec" runat="server" MaxLength="2000" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
                
            </tr>             
            <tr>  
                <td>
                    
                    <asp:Label ID="Label33" runat="server" Text="備註"></asp:Label>
                    
                </td>
                <td>
                   
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
                
            </tr> 
        
        </table>
        <div id="tabs">
  <ul>
    <%--<li><a href="#tabs-1">設備資源</a></li>--%>
    <li><a href="#tabs-2">檔案上傳</a></li>
    <li><a href="#tabs-3">檔案清單</a></li>
    <%--<li><a href="#tabs-4">申請單附件</a></li>--%>
  </ul>
  <%--<div id="tabs-1">
    <p></p>
  </div>--%>
  <div id="tabs-2">
    <table>
        <tr>
            <td style="COLOR: red">
                
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
                    *上傳後請按確定！
             </td>        
        </tr>
    </table> 

  </div>
  <div id="tabs-3">
    <table id="Table2" border="0" cellpadding="5" cellspacing="5" width="100%">
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

	                        <asp:TemplateField HeaderText="圖片">
	                            <ItemTemplate> 
	                                <%--<asp:Image ID="Image1" runat="server" Height="50" Width="50" ImageUrl='<%# Eval("file_path") %>' />--%> 
	                                <%--<asp:Image ID="Image1" runat="server" Height="50" Width="50" ImageUrl="pic/1.jpg" />--%>
                                    <asp:Image ID="Image1" runat="server" Height="100" Width="100" ImageUrl='<%#"pic/" + Eval("File_Name")%>' />	                                
	                            </ItemTemplate> 
	                        </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
<%--                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                        Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                                </ItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("File_Name") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("Apparatus_ID") %>'></asp:Label>
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
  <%--<div id="tabs-4">
    <p></p>
  </div>--%>  
</div>

    <tr>
        <td align ="center" colspan = 2 style="COLOR: red">
            <br />
            <br />
                
            <asp:Button ID="butOK" runat="server" Text="確定" 
                    onclick="butOK_Click" />
                
            <br />
            <br />
        </td>
    </tr>        
         

    </fieldset>
    
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
</asp:Content>

