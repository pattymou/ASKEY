<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DelayGoods.aspx.cs" Inherits="WebForm_DelayGoods" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" href="../css/jquery.dataTables.css">
  <script src="../js/jquery_1.11.0.min.js"></script>
  <script src="../js/jquery.dataTables.min.js"></script>
  
<%-- 	<style type="text/css" class="init">

	th, td { white-space: nowrap; }
	div.dataTables_wrapper {
		width: 1250px;
		margin: 0 auto;
	}

	</style>--%>
	
	<style>
	    tfoot input {
        width: 100%;
        padding: 3px;
        box-sizing: border-box;
    }
	</style>
	
  <script >

      $(document).ready(function() {


          //            var table = $('#example').dataTable({
          //              "scrollX": true,
          //              "ajax": '../ajax/data/arays_assign.txt'
          //              
          //          });

          // Setup - add a text input to each footer cell
          //            $('#example tfoot th').each(function() {
          //                var title = $('#example thead th').eq($(this).index()).text();
          //                $(this).html('<input type="text" placeholder="Search ' + title + '" />');
          //            });

          // DataTable
          var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_DelayApparatus.txt'
          });

          // Apply the search
          //            table.columns().eq(0).each(function(colIdx) {
          //                $('input', table.column(colIdx).footer()).on('keyup change', function() {
          //                    table
          //                .column(colIdx)
          //                .search(this.value)
          //                .draw();
          //                });
          //            });

          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(8).text();
              //              var url = "https://tw.yahoo.com";
              //                        var url = "http://localhost/SIT_System/WebForm/ProjectAssign.aspx?Value=1&ID=" + name;
              if (name != "") {
                  var url = "ModifyGReservationStatus.aspx?ID=" + name;
                  //              window.open(url);
                  location.href = (url);
              }
          });


      });        
  </script>  
 
<%-- <br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增工作項目]</asp:LinkButton> 
 <br />--%>

  
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>逾期尚未更換貨品</legend></font>


  
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <%--<th>財產編號</th>--%>
                    <th>料號</th>
                    <th>貨品名稱</th>
                    <th>廠商名稱</th>
                    <th>領用日期</th>
                    <th>建議更換日期</th>
                    <th>領用人</th>
                    <th>領用數量</th>
                    <th>代理人</th>
                    <%--<th>代理人分機</th>--%>                    
                    <th>預約單編號</th>
                    
                </tr>
            </thead>
            <tfoot>
<%--            <tr>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>預約單編號</th>
            </tr>--%>
        </tfoot>
            <tbody>   
            </tbody> 
        </table>    

    <table id="Table5" class="one" width="100%">
        <tr>
            <td align =center >
                
                <asp:Button ID="btnReturn" runat="server" Text="上一頁" 
                    onclick="btnReturn_Click" />
                
            </td>
        </tr>     
    </table> 

</fieldset>
</asp:Content>

