<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddInfoCase.aspx.cs" Inherits="WebForm_AddInfoCase" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
  
      <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../css/jquery.fileupload.css" />
    <link rel="stylesheet" href="../css/jquery.fileupload-ui.css" />
    <!-- CSS adjustments for browsers with JavaScript disabled -->
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-noscript.css" /></noscript>
    <noscript><link rel="stylesheet" href="../css/jquery.fileupload-ui-noscript.css" /></noscript>  
    
    <script>
      // Initialize the jQuery UI theme switcher:
        $('#theme-switcher1').change(function() {
            var theme = $('#theme');
            theme.prop(
            'href',
            theme.prop('href').replace(
                /[\w\-]+\/jquery-ui.css/,
                $(this).val() + '/jquery-ui.min.css'
            )
        );
        });
        $('#theme-switcher2').change(function() {
            var theme = $('#theme');
            theme.prop(
            'href',
            theme.prop('href').replace(
                /[\w\-]+\/jquery-ui.css/,
                $(this).val() + '/jquery-ui.min.css'
            )
        );
        });
    </script>
  
    <fieldset>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional">
                <ContentTemplate>    
        <table>
             
            <tr>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="部門："></asp:Label>
                    <asp:DropDownList ID="ddlDepartment_T" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlDepartment_T_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="類別："></asp:Label>
                    <asp:DropDownList ID="ddlKind" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp
                    <asp:Button ID="btnDKind" runat="server" Text="刪除" onclick="btnDKind_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp
                    <asp:Label ID="Label10" runat="server" Text="負責Team："></asp:Label>
                    <asp:DropDownList ID="ddlTeam" runat="server">
                    </asp:DropDownList>       
                    &nbsp;&nbsp
                    <asp:Button ID="btnTeam" runat="server" Text="修改" onclick="btnTeam_Click" />             
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:TextBox ID="txtKind" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAKind" runat="server" Text="新增" onclick="btnAKind_Click" />                    
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Function："></asp:Label>
                    <asp:DropDownList ID="ddlKind1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind1_SelectedIndexChanged">
                    </asp:DropDownList>
<%--                    <asp:DropDownList ID="ddlFunction" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlFunction" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;                   
                    <asp:Button ID="btnDFunction" runat="server" Text="刪除" onclick="btnDFunction_Click" />
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKind2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind2_SelectedIndexChanged">
                    </asp:DropDownList>                    
                    <asp:TextBox ID="txtFunction" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    <asp:Button ID="btnAFunction" runat="server" Text="新增" onclick="btnAFunction_Click" />                    
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="項目："></asp:Label>
                    <asp:DropDownList ID="ddlKind3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind3_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction1_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp                                     
                    <asp:Button ID="btnDItem" runat="server" Text="刪除" onclick="btnDItem_Click" />
                </td> 
            </tr> 
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlKind4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind4_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:DropDownList ID="ddlFunction2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction2_SelectedIndexChanged">
                    </asp:DropDownList>                                                            
                    <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    &nbsp;&nbsp
                    
                    <asp:Button ID="btnAItem" runat="server" Text="新增" onclick="btnAItem_Click" />                    
                </td>
            </tr> 
           <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="EV to DV NPI："></asp:Label>
                    <asp:DropDownList ID="ddlKind5" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind5_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction3_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem1_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlL1" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Option</asp:ListItem>
                        <asp:ListItem>Mandatory</asp:ListItem>
                        
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    <asp:Button ID="btnNPIL1" runat="server" Text="更新" onclick="btnNPIL1_Click" />
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="DV to PV NPI："></asp:Label>
                    <asp:DropDownList ID="ddlKind6" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind6_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction4_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem2_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlL2" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Option</asp:ListItem>
                        <asp:ListItem>Mandatory</asp:ListItem>
                        
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    <asp:Button ID="btnNPIL2" runat="server" Text="更新" onclick="btnNPIL2_Click" />
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label30" runat="server" Text="Lead Time："></asp:Label>
                    <asp:DropDownList ID="ddlKind8" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind8_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction6" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction6_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem4_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCost" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp 
                    <asp:Button ID="btnCost" runat="server" Text="更新" onclick="btnCost_Click" />

                </td>
            </tr>
            <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="測試內容說明："></asp:Label>
                    <asp:DropDownList ID="ddlKind7" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind7_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFunction5" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFunction5_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlItem3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlItem3_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="578px"></asp:TextBox>  
                    &nbsp;&nbsp 
                    <asp:Button ID="btnNote" runat="server" Text="更新" onclick="btnNote_Click" />
                    
                </td>

                 
            </tr>
            <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="測試環境架構："></asp:Label>
                    <asp:DropDownList ID="ddlFileK" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileK_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFileF" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileF_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlFileI" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileI_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    
                    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="檔案名稱："></asp:Label>                                    
                    <asp:Label ID="lblFileN" runat="server"></asp:Label>
                </td>            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="(限上傳一個檔案)" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table> 
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlKind" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDFunction" EventName="Click" />

                </Triggers>                    

        </asp:UpdatePanel>
        <table >
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
                                   
                    <asp:Button ID="btnMFile" runat="server" Text="更新" onclick="btnMFile_Click" />
                </td>
            </tr>              
           </table>
               <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode ="Conditional">
            <ContentTemplate>              
           <table>

           <tr>   
                <td>
                ***************************************************
                </td> 
            </tr>
           <tr>
                <td>

                    <asp:Label ID="Label20" runat="server" Text="文件下載："></asp:Label>
                    <asp:DropDownList ID="ddlFileK1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileK1_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFileF1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileF1_SelectedIndexChanged">
                    </asp:DropDownList>  
                    <asp:DropDownList ID="ddlFileI1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlFileI1_SelectedIndexChanged">
                    </asp:DropDownList>  
                    &nbsp;&nbsp 
                    
    
                </td>

                 
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="檔案名稱："></asp:Label>                                    
                    <asp:Label ID="lblFileN1" runat="server"></asp:Label>
                </td>            
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="(限上傳一個檔案)" ForeColor="Red"></asp:Label>
                </td>
            </tr>

        </table> 
                            </ContentTemplate>
                    
            <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlKind" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDFunction" EventName="Click" />

                </Triggers>--%>
        </asp:UpdatePanel>
        <table >
            <tr>
                <td>
                    <form style="visibility :hidden">
                        <label style="visibility :hidden" for="theme-switcher2">Theme:</label>
                        <select style="visibility :hidden" id="theme-switcher2" class="pull-right" >
                            <option value="dark-hive" selected>Dark Hive</option>
                        </select>
                    </form>
                    <form id="fileupload2" action="UploadProgress.aspx" method="POST" enctype="multipart/form-data">

                        <div class="row fileupload-buttonbar">
                            <div id = "Div2" class="col-lg-7">
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
                                   
                    <asp:Button ID="btnMFile1" runat="server" Text="更新" onclick="btnMFile1_Click" />
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

        <script src="../js/jquery.min.js"></script>
        <script src="../js/jquery-ui.min.js"></script>
        <!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
        <!-- jquery.ui.widget.js 不能省略，否則已上傳及要上傳之清單會看不到-->
        <script src="../js/vendor/jquery.ui.widget.js"></script>
        <!-- The Templates plugin is included to render the upload/download listings -->
        <script src="../js/tmpl.min.js"></script>
        <!-- The Load Image plugin is included for the preview images and image resizing functionality -->
        <script src="../js/load-image.all.min.js"></script>
        <!-- The Canvas to Blob plugin is included for image resizing functionality -->
        <script src="../js/canvas-to-blob.min.js"></script>
        <!-- blueimp Gallery script -->
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

