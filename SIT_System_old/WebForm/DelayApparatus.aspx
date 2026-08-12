<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DelayApparatus.aspx.cs" Inherits="WebForm_DelayApparatus" %>

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

            $("select#the_status").change(function () {
            var val = $("select#the_status option:selected").attr('value');
            
            if( val == "TP"){    // 注意檢查完全沒有選取的寫法，這行是精華
               var locak = "台北"; 
            }
            else{
               var locak = "吳江"; 
               } 
            table.search( locak );
            table.draw();
            

        });
        
        var Local1="<%=strValue1()%>"
//        alert (Local1);
        if (Local1 == "DA40")
            var ddl = document.getElementById('the_status').value = 'TP';
        else
            var ddl = document.getElementById('the_status').value = 'WJ';
          
          var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_DelayApparatus.txt',
              "columnDefs": [
                { "visible": false, "targets": 10 },
               
                { "visible": false, "targets": 11 }
              ]
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
//              var name = $('td', this).eq(10).text();
                var name = table.row(this).data()[10];
              //              var url = "https://tw.yahoo.com";
              //                        var url = "http://localhost/SIT_System/WebForm/ProjectAssign.aspx?Value=1&ID=" + name;
              if (name != "") {
                  var url = "ModifyReservationStatus.aspx?ID=" + name;
                  //              window.open(url);
                  location.href = (url);
              }
          });
          
          
          
          var val = $("select#the_status option:selected").attr('value');
            
            if( val == "TP"){    
               var locak = "台北"; 
            }
            else{
               var locak = "吳江"; 
               } 

            table.search( locak );
            table.draw();


      });        
  </script>  
 
<%-- <br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增工作項目]</asp:LinkButton> 
 <br />--%>

  
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>逾期尚未歸還設備</legend></font>

<select name="the_status" id="the_status">
    <option value="TP">台北</option>
    <option value="WJ">吳江</option>
    
</select>
<br />  
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>設備名稱</th>
                    <th>財產編號</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>預計歸還日期</th>
                    <th>借用人</th>
                    <th>借用人分機</th>
                    <th>代理人</th>
                    <th>代理人分機</th>                     
                    <th>預約單編號</th>
                    <th>地點</th>
                    
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

