<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectTask.aspx.cs" Inherits="WebForm_ProjectTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--    <link rel="stylesheet" href="//apps.bdimg.com/libs/jqueryui/1.10.4/css/jquery-ui.min.css">
  <script src="//apps.bdimg.com/libs/jquery/1.10.2/jquery.min.js"></script>
  <script src="//apps.bdimg.com/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>--%>
    <link rel="stylesheet" href="../css/jquery-ui.min.css">
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
      $(window).load(function() {



      });   
  </script>

<fieldset>

<%-- <br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[修改此任務]</asp:LinkButton> 
 <br />
 <br />--%>
 
  <table id="Table3" class="one" width="100%">
    <tr>
        <td>
            <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[修改此子任務]</asp:LinkButton>
<%--             &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lblModify" runat="server" OnClick="lbtnModify_Click">[修改此專案]</asp:LinkButton>--%>
        </td>
        <td align ="right">
            <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnDel_Click">[刪除此子任務]</asp:LinkButton>
        </td>
    </tr>
 </table>
 <br />

    <font face="verdana"color="0000DD"size="6" ><legend>檢視子任務</legend></font>
    <%--<table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">--%>
    <table  style="table-layout:fixed" border= "1px" cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="子任務名稱"></asp:Label>    
            </td>
            <td>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
            </td>
            
             <td>
                <asp:Label ID="Label8" runat="server" Text="子任務ID"></asp:Label> 
            </td>
            <td>
                <asp:Label ID="lblCaseID" runat="server" Text=""></asp:Label>
            </td>

        </tr>
        <tr id ="Name1" runat ="server" >
            <td>
                <asp:Label ID="lblPU1" runat="server" Text="Sub PU"></asp:Label>    
            </td>
            <td>
                <asp:Label ID="lblPU" runat="server" Text=""></asp:Label>
            </td>
            
             <td>
                <asp:Label ID="lblModelName1" runat="server" Text="機種名稱"></asp:Label> 
            </td>
            <td>
                <asp:Label ID="lblModelName" runat="server" Text=""></asp:Label>
            </td>

        </tr>
        <tr>
            <td>
                
                <asp:Label ID="Label9" runat="server" Text="指派工程師"></asp:Label>
                
            </td>
            <td>
                <asp:Label ID="lblAssign" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="狀態"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </td>
        </tr>        
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="開始日期"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStartdate" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="預計完成日"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEnddate" runat="server" Text=""></asp:Label>
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="結果判定"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="進度"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProgress" runat="server" Text=""></asp:Label>
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="實驗室名稱"></asp:Label>
            </td>
            <td colspan =3>
                <asp:Label ID="lblLab" runat="server" Text=""></asp:Label>
            </td>
                       
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="報價金額"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblQuoted" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label17" runat="server" Text="請款金額"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReimburse" runat="server" Text=""></asp:Label>
            </td>            
        </tr>
        <tr id="LTE" runat ="server">
            <td>
                <asp:Label ID="Label12" runat="server" Text="Application form for LTE TRP/TIS"></asp:Label>
            </td>
            <td colspan=3>
                <asp:LinkButton ID="linkForm" runat="server" OnClick="lbtnForm_Click">LTE TRP/TIS連結</asp:LinkButton>
            </td>
                       
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="備註"></asp:Label>
            </td>
            <td colspan =3>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="578px"></asp:TextBox>
            </td>
            
        </tr>
    
    </table> 
    
<div id="tabs">
  <ul>
    <li><a href="#tabs-1">設備資源</a></li>
    <li><a href="#tabs-2">檔案上傳</a></li>
    <li><a href="#tabs-3">檔案清單</a></li>
    <%--<li><a href="#tabs-4">申請單附件</a></li>--%>
  </ul>
  <div id="tabs-1">
    <table id="Table4" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="類別"></asp:Label>
                
                <asp:DropDownList ID="ddlKind" runat="server">
<%--                    <asp:ListItem Value="0">ALL</asp:ListItem>--%>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                
                <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                <asp:Label ID="Label18" runat="server" Text="(設備搜尋)"></asp:Label>
            </td>
        </tr>
     </table> 
    
     <table border=2>
            <tr>
            
			    <td>
			        <asp:ListBox ID="listLeft" runat="server" Height="237px" Width="321px" 
                        DataTextField="Name" DataValueField="id" SelectionMode="Multiple"></asp:ListBox>
				</td>
			    <td>
				    <asp:Button ID="btnRight" runat="server" Text=">" OnClick="btnRight_Click" Width="30px" /><br /><br />
				    <asp:Button ID="btnLeft" runat="server" Text="<" OnClick="btnLeft_Click" Width="30px" />
			    </td>
			    <td>
				    <asp:ListBox ID="listRight" runat="server" Width="321" Height="237" 
                        SelectionMode="Multiple" 
                        OnSelectedIndexChanged="listRight_SelectedIndexChanged" DataTextField="Name" 
                        DataValueField="id" ></asp:ListBox>
			    </td>            
                        
           
            </tr>  
            <tr>
                <td colspan =3 align =center >
                    
                    <asp:Button ID="btnApparatus" runat="server" Text="確定" OnClick="btnApparatus_Click"/>
                    
                </td>
            </tr>
         </table>    
  </div>
  <div id="tabs-2">
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
            <td align ="center" colspan = 2 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="儲存" 
    onclick="butOK_Click" />
                    
                <br />
                <br />
            </td>
        </tr>        
    </table> 

  </div>
  <div id="tabs-3">
    <table id="Table2" border="0" cellpadding="5" cellspacing="5" width="100%">
        <tr>
            <td align ="center">
                 
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>                
                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" OnRowDeleting="gvwMain_RowDeleting" OnRowDataBound ="gvwMain_RowDataBound" onselectedindexchanged="gvwMain_SelectedIndexChanged">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                        Target="_blank" Text='<%# Bind("File_Name") %>'></asp:HyperLink>
                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" Text='<%# Bind("File_Name") %>'>  </asp:LinkButton>--%>
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
        

    </table>
                    <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />
                    <br />
                    
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />                          
                    <br />
                    <br />
                </td>
            </tr>    
  </div>
  <%--<div id="tabs-4">
    <p></p>
  </div>--%>  
</div>
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

