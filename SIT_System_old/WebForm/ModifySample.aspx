<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ModifySample.aspx.cs" Inherits="WebForm_ModifySample" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/jquery-ui.min.css">
  <script src="../js/jquery_1.11.0.min.js"></script>
  
      <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../css/jquery.fileupload.css" />
    <link rel="stylesheet" href="../css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-noscript.css" /></noscript>
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-ui-noscript.css" /></noscript>

<fieldset>
        
        <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>
                </td>
                <td colspan=3>
                    
                    <asp:DropDownList ID="ddlKind" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label2" runat="server" Text="功能"></asp:Label>
                </td>
                <td colspan=3>
                    
                    <asp:DropDownList ID="ddlFunction" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction_SelectedIndexChanged">
                    </asp:DropDownList> 
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label3" runat="server" Text="項目"></asp:Label>
                </td>
                <td colspan=3>
                    
                    <asp:DropDownList ID="ddlItem" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label4" runat="server" Text="系統編號"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                    
                </td>

            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label9" runat="server" Text="品名"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label23" runat="server" Text="品名代號"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtNameCode" runat="server"></asp:TextBox>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label10" runat="server" Text="Vendor"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtVendor" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label5" runat="server" Text="Model Name"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label6" runat="server" Text="MAC Address"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtMAC" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label7" runat="server" Text="PHY driver vesion"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtPhy" runat="server"></asp:TextBox>
                    
                </td>
                
            </tr>
            <tr>
                <td colspan=2>
                    
                    <asp:Label ID="Label8" runat="server" Text="Firmware version"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:TextBox ID="txtFirmware" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td rowspan=7 valign=middle >
                    
                    <asp:Label ID="Label11" runat="server" Text="Interface"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:Label ID="Label12" runat="server" Text="Physical"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtPhysical" runat="server"></asp:TextBox>
                    
                </td>

            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label13" runat="server" Text="VoIP"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtVoip" runat="server"></asp:TextBox>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="CATV"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtCATV" runat="server"></asp:TextBox>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label15" runat="server" Text="USB"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtUSB" runat="server"></asp:TextBox>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label16" runat="server" Text="LAN"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtLAN" runat="server"></asp:TextBox>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label17" runat="server" Text="WLAN"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtWLAN" runat="server"></asp:TextBox>
                    
                </td>            
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="Label18" runat="server" Text="WPS"></asp:Label>
                    
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtWPS" runat="server"></asp:TextBox>
                    
                </td>            
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label21" runat="server" Text="設備狀態"></asp:Label>
                </td>
                <td colspan=3>
                    <%--<asp:CheckBox ID="chkReservation" runat="server" Text="提供預約" />--%>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>閒置中</asp:ListItem>
                        <asp:ListItem>借用中</asp:ListItem>
                        <asp:ListItem>校驗中</asp:ListItem>
                        <asp:ListItem>異常維修中</asp:ListItem>
                        <asp:ListItem>使用中</asp:ListItem>
                        <asp:ListItem>不可借用</asp:ListItem>
                        <asp:ListItem>採購中</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label19" runat="server" Text="放置地點"></asp:Label>
                </td>
                <td colspan=3>
                    
                    <asp:TextBox ID="txtPlace" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label25" runat="server" Text="保管部門"></asp:Label>
                </td>
                <td colspan=3>
                    <%--<asp:TextBox ID="txtCustodianD" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>DA40</asp:ListItem>
                        <asp:ListItem>DA40-WJ</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label26" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label20" runat="server" Text="保管人"></asp:Label>
                </td>
                <td colspan=3>
                    <asp:DropDownList ID="ddlCustodian" runat="server">
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtCustodian" runat="server"></asp:TextBox>--%>
                    
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label24" runat="server" Text="保管代理人"></asp:Label>
                </td>
                <td colspan=3>
                    <asp:DropDownList ID="ddlCustodian1" runat="server">
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtCustodian" runat="server"></asp:TextBox>--%>
                    
                </td>
            </tr>
            <tr>  
                <td colspan=2>
                    
                    <asp:Label ID="Label33" runat="server" Text="備註"></asp:Label>
                    
                </td>
                <td colspan=3>
                   
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                   
                </td>
                
            </tr>
            <tr>
            <td align ="center" colspan = 5 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="確定" 
                            onclick="butOK_Click" />
                <%--&nbsp;&nbsp;&nbsp;&nbsp;            
                <asp:Button ID="butReturn" runat="server" Text="上一頁" 
                            onclick="butReturn_Click" />  --%>                          
                    
                <br />
                <br />
            </td>
            </tr>
            <tr>
                <td colspan=2>
                        <asp:Label ID="Label22" runat="server" Text="上傳報告"></asp:Label>
                </td>
                <td>
                
                    <form style="visibility :hidden">
                        <label style="visibility :hidden" for="theme-switcher">Theme:</label>
                        <select style="visibility :hidden" id="theme-switcher" class="pull-right" >
                            <option value="dark-hive" selected>Dark Hive</option>
                        </select>
                    </form>
                     <form id="fileupload" action="UploadProgress.aspx" method="POST" enctype="multipart/form-data">

                            <div class="row fileupload-buttonbar">
                                <div id = "id1" class="col-lg-7">
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

                    
                </td>        
            </tr> 
            <tr>
                <td align ="center" colspan = 5 style="COLOR: red">
                    <br />
                    <br />
                        
                    <asp:Button ID="butSave" runat="server" Text="儲存" 
                            onclick="butSave_Click" />
                        
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align ="center"  colspan=5>
                 
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>                
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDeleting="gvwMain_RowDeleting">
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
                            <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("你確定要刪除此筆資料嗎？");'
                                        Text="刪除" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                            
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView> 
                    </ContentTemplate>
                        </asp:UpdatePanel>                              
                </td>
            </tr>
            <tr>
                <td align ="center" colspan = 5 style="COLOR: red">
                    <br />
                    <br />
                        
                    <%--<asp:Button ID="Button1" runat="server" Text="確定" 
                                onclick="butOK_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;--%>            
                    <asp:Button ID="butReturn" runat="server" Text="上一頁" 
                                onclick="butReturn_Click" />                            
                        
                    <br />
                    <br />
                </td>
            </tr>                       
        </table> 
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

