<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectView1.aspx.cs" Inherits="WebForm_ProjectView1" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
  <script src="../js/jquery-1.10.4.min.js"></script>
  <link rel="stylesheet" href="../css/jquery.dataTables.min1.10.8.css">
  <script src="../js/jquery.dataTables.min1.8.js"></script>
  
        <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../css/jquery.fileupload.css" />
    <link rel="stylesheet" href="../css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-noscript.css" /></noscript>
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-ui-noscript.css" /></noscript>
  
  
  <script>
    $(function() {
    $( "#tabs" ).tabs();
  });
  </script>
  

<%-- <style type="text/css" class="init">
	th, td { white-space: nowrap; }
	div.dataTables_wrapper {
		width: 1250px;
		margin: 0 auto;
	}
	
 </style>--%>

	
  <script>
    var name1="<%=strName1()%>"
      $(document).ready(function() {
          var table = $('#example').DataTable({
              "ajax": "../ajax/data/arays_Open_"+ name1+".txt",
              "deferRender": true,
//              "columnDefs": [
//               
//                { "visible": false, "targets": 9 }
//              ],

              initComplete: function() {
                  this.api().columns().every(function() {
                      
                      var column = this;
                      var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function() {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                      column.data().unique().sort().each(function(d, j) {
                          select.append('<option value="' + d + '">' + d + '</option>')
                      });
                  });
              }
          });

          $('#example tbody').on('click', 'tr', function() {
          var name = $('td', this).eq(10).text();
              //var name = $('td', this).eq(9).text();
          //                var name = table.row(this).data()[9];
          var strCustomer1 = $('td', this).eq(9).text();
//              var strCustomer1 = $('td', this).eq(8).text();
//              var strDepartment1 = $('td', this).eq(9).text();
              if (name != "") {
                  var url = "ProjectDetail.aspx?Value=1&ID=" + name + "&Customer=" + strCustomer1;
                  location.href = (url);
              }
          });  
          
          
          var table1 = $('#example1').DataTable({
              "ajax": "../ajax/data/arays_Close_"+ name1+".txt",
              "deferRender": true,
//              "columnDefs": [
//               
//                { "visible": false, "targets": 9 }
//              ],

              initComplete: function() {
                  this.api().columns().every(function() {
                      
                      var column = this;
                      var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function() {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                      column.data().unique().sort().each(function(d, j) {
                          select.append('<option value="' + d + '">' + d + '</option>')
                      });
                  });
              }
          });

          $('#example1 tbody').on('click', 'tr', function() {
          var name = $('td', this).eq(10).text();
//              var name = $('td', this).eq(9).text();
          //                var name = table1.row(this).data()[9];
          var strCustomer1 = $('td', this).eq(9).text();
//              var strCustomer1 = $('td', this).eq(8).text();
//              var strDepartment1 = $('td', this).eq(9).text();
              if (name != "") {
                  var url = "ProjectDetail.aspx?Value=1&ID=" + name + "&Customer=" + strCustomer1;
                  location.href = (url);
              }
          });
          
          var table2 = $('#example2').DataTable({
              "ajax": "../ajax/data/arays_Hold_"+ name1+".txt",
              "deferRender": true,
//              "columnDefs": [
//               
//                { "visible": false, "targets": 9 }
//              ],

              initComplete: function() {
                  this.api().columns().every(function() {
                      
                      var column = this;
                      var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function() {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                      column.data().unique().sort().each(function(d, j) {
                          select.append('<option value="' + d + '">' + d + '</option>')
                      });
                  });
              }
          });

          $('#example2 tbody').on('click', 'tr', function() {
          var name = $('td', this).eq(10).text();
//              var name = $('td', this).eq(9).text();
          //                var name = table2.row(this).data()[9];
          var strCustomer1 = $('td', this).eq(9).text();
//              var strCustomer1 = $('td', this).eq(8).text();
//              var strDepartment1 = $('td', this).eq(9).text();
              if (name != "") {
                  var url = "ProjectDetail.aspx?Value=1&ID=" + name + "&Customer=" + strCustomer1;
                  location.href = (url);
              }
          });        
      });


  
  </script>

 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增專案]</asp:LinkButton> 
 <br />
 
    

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">Open</a></li>
    <li><a href="#tabs-2">Close</a></li>
    <li><a href="#tabs-3">Hold</a></li>
    <li><a href="#tabs-4">File Upload</a></li>
    <li><a href="#tabs-5">Project Information</a></li>
  </ul>
  <div id="tabs-1">
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>Team</th>
                    <th>專案名稱</th>
                    <th>種類</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W VR.</th>
                    <th>S/W VR.</th>                    
                    <th>H/W Engineer</th>                    
                    <th>S/W Engineer</th>
                    <th>PJM</th>
                    <th>申請單編號</th>
                    
                </tr>
            </thead>
            <tfoot>
            <tr>
                    <th>Team</th>
                    <th>專案名稱</th>
                    <th>種類</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W VR.</th>
                    <th>S/W VR.</th>                    
                    <th>H/W Engineer</th>                    
                    <th>S/W Engineer</th>
                    <th>PJM</th>
                    <th>申請單編號</th>
            </tr>
        </tfoot>
            <tbody>           
            </tbody> 
        </table>   
  
  </div>
  <div id="tabs-2">
    <table id="example1" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>Team</th>
                    <th>專案名稱</th>
                    <th>種類</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W VR.</th>
                    <th>S/W VR.</th>                    
                    <th>H/W Engineer</th>                    
                    <th>S/W Engineer</th>
                    <th>PJM</th>
                    <th>申請單編號</th>
                    
                </tr>
            </thead>
            <tfoot>
            <tr>
                    <th>Team</th>
                    <th>專案名稱</th>
                    <th>種類</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W VR.</th>
                    <th>S/W VR.</th>                    
                    <th>H/W Engineer</th>                    
                    <th>S/W Engineer</th>
                    <th>PJM</th>
                    <th>申請單編號</th>
            </tr>
        </tfoot>
            <tbody>   
            </tbody> 
        </table>  
  
  </div>
  <div id="tabs-3">
    <table id="example2" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>Team</th>
                    <th>專案名稱</th>
                    <th>種類</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W VR.</th>
                    <th>S/W VR.</th>                    
                    <th>H/W Engineer</th>                    
                    <th>S/W Engineer</th>
                    <th>PJM</th>
                    <th>申請單編號</th>
                    
                </tr>
            </thead>
            <tfoot>
            <tr>
                    <th>Team</th>
                    <th>專案名稱</th>
                    <th>種類</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W VR.</th>
                    <th>S/W VR.</th>                    
                    <th>H/W Engineer</th>                    
                    <th>S/W Engineer</th>
                    <th>PJM</th>
                    <th>申請單編號</th>
            </tr>
        </tfoot>
            <tbody>   
            </tbody> 
        </table>     
  </div>
<div id="tabs-4">
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
                    
                <asp:Button ID="btnSave" runat="server" Text="儲存" 
    onclick="btnSave_Click" />
                    
                <br />
                <br />
            </td>
        </tr>        
    </table> 

  </div>
  <div id="tabs-5">
    <table id="Table3" border="0" cellpadding="5" cellspacing="5" width="100%">
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
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
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
            </td>
        </tr>
        

    </table>
<%--                    <tr>
                <td align ="center" colspan = 2 style="COLOR: red">
                    <br />
                    <br />
                    
                        <asp:Button ID="Button2" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />                          
                    <br />
                    <br />
                </td>
            </tr> --%>   
  </div>  
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
<%--<script src="../js/jquery.min.js"></script>--%>
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

