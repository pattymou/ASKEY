<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsView.aspx.cs" Inherits="WebForm_GoodsView" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
	<link href="../css/shCore.css" rel="stylesheet" type="text/css" />
    <link href="../css/shThemeDefault.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="../css/flexslider.css" type="text/css" media="screen" />
	
<%--    <link rel="stylesheet" href="../css/slicebox.css">
    <link rel="stylesheet" href="../css/custom.css">   
    <script src="../js/modernizr.custom.46884.js"></script>
    <script src="../js/jquery.slicebox.js"></script>--%>
            
        
    


    
 
 
  
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
         <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[修改貨品資訊]</asp:LinkButton>
                </td>
                <td align ="right">
                    <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnDel_Click">[刪除此貨品資訊]</asp:LinkButton>
                </td>
            </tr>         
         </table> 
         <br />   
         <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <%--<tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="財產編號"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProduct_ID" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>         
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="貨品名稱(英文)"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblName_En" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="貨品名稱(中文)"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblName_CH" runat="server" Text=""></asp:Label>
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="類別"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblKind" runat="server" Text=""></asp:Label>
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="料號"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPart_No" runat="server" Text=""></asp:Label>
                </td>
            </tr>                                  
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="廠商(英文)"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMF_EN" runat="server" Text=""></asp:Label>
                </td>
            </tr>   
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="廠商(中文)"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMF_CH" runat="server" Text=""></asp:Label>
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="廠牌"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="廠商貨品編號"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMF_Number" runat="server" Text=""></asp:Label>
                </td>
            </tr>  
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="對應採購"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMF_Mail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="保管部門"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDep" runat="server" Text=""></asp:Label>
                </td>
            </tr>                             
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="保管人"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCustodian" runat="server" Text=""></asp:Label>
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="保管代理人"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCustodian1" runat="server" Text=""></asp:Label>
                </td>
            </tr>           
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="有效期限天數"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCheck_Date" runat="server" Text=""></asp:Label>
                </td>
            </tr> 
<%--            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="金額"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMoney" runat="server" Text=""></asp:Label>
                </td>
            </tr>--%>              
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="庫存數量"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblQuantity_Stock" runat="server" Text=""></asp:Label>
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="安全存量"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblQuantity_Safety" runat="server" Text=""></asp:Label>
                </td>
            </tr>                             
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="放置地點"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label>
                </td>
            </tr>    
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="貨品狀態"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>                 
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="備註"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="496px"></asp:TextBox>
                </td>
            </tr>   
    
         </table> 
                      
<%--    </fieldset>--%> 
    
<%--    <div id="tabs">
  <ul>
    <li><a href="#tabs-1">Feature</a></li>
    <li><a href="#tabs-2">Specification</a></li>
  </ul>
  <div id="tabs-1">
    <p><%= strFeature%></p>
  </div>
  <div id="tabs-2">
    <p><%= strSpec%></p>
  </div>--%>
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
            <td align ="center" colspan = 2 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="上一頁" 
                        onclick="butOK_Click" />
                    
                <br />
                <br />
            </td>
        </tr>
    </table>  
    </fieldset>
</asp:Content>

