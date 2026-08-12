<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReservationList.aspx.cs" Inherits="WebForm_GoodsReservationList" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <link rel="stylesheet" href="../css/jquery-ui.min.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
  <script src="../js/jquery-1.10.4.min.js"></script>
  <link rel="stylesheet" href="../css/jquery.dataTables.min1.10.8.css">
  <script src="../js/jquery.dataTables.min1.8.js"></script>
  
  
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
            table1.search( locak );
            table1.draw();
            

        });
        
        var Local1="<%=strValue1()%>"
//        alert (Local1);
        if (Local1 == "DA40")
            var ddl = document.getElementById('the_status').value = 'TP';
        else
            var ddl = document.getElementById('the_status').value = 'WJ';
        var name1="<%=strName1()%>"
        
        var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_Reservation_'+ name1+'.txt',
              "columnDefs": [
                { "visible": false, "targets": 4 },
               
                { "visible": false, "targets": 5 }
              ]
          });

          $('#example tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(4).text();
                var name = table.row(this).data()[4];
              if (name != "") {
                  var url = "GoodsReservationAssign.aspx?ID=" + name;
                  location.href = (url);
              }
          }); 
          
          
          var table1 = $('#example1').DataTable({
              "ajax": '../ajax/data/arays_Continuous_'+ name1+'.txt',
              "deferRender": true,
              "columnDefs": [
                { "visible": false, "targets": 4 },
               
                { "visible": false, "targets": 5 }
              ],
              

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
//              var name = $('td', this).eq(4).text();
                var name = table1.row(this).data()[4];
              if (name != "") {
                  var url = "GoodsReservationAssign.aspx?ID=" + name;
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
            table1.search( locak );
            table1.draw();
                 
      });


  
  </script>
  <select name="the_status" id="the_status">
    <option value="TP">台北</option>
    <option value="WJ">吳江</option>
    
</select>     

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">預約貨品</a></li>
    <li><a href="#tabs-2">續借貨品</a></li>
  </ul>
  <div id="tabs-1">
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <%--<th>財產編號</th>--%>
                    <th>料號</th>
                    <th>貨品名稱</th>
                    <th>廠商名稱</th>
                    <th>領用日期</th>
                    <%--<th>歸還日期</th>--%>
                    <th>預約單編號</th>
                    <th>地點</th>
                    
                </tr>
            </thead>
            <tfoot>
            
        </tfoot>
            <tbody>           
            </tbody> 
        </table>   
  
  </div>
  <div id="tabs-2">
    <table id="example1" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <%--<th>財產編號</th>--%>
                    <th>料號</th>                
                    <th>貨品名稱</th>
                    <th>廠商名稱</th>
                    <th>領用日期</th>
                    <%--<th>原歸還日期</th>
                    <th>預計歸還日期</th>--%>
                    <th>預約單編號</th>
                    <th>地點</th>
                    
                </tr>
            </thead>
            <tfoot>
            
        </tfoot>
            <tbody>   
            </tbody> 
        </table>  
  
  </div>
</div>
</asp:Content>

