<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ReservationList.aspx.cs" Inherits="WebForm_ReservationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <link rel="stylesheet" href="../css/jquery.dataTables.css">
  <script src="../js/jquery_1.11.0.min.js"></script>
  <script src="../js/jquery.dataTables.min.js"></script>
  
 	<style type="text/css" class="init">

	th, td { white-space: nowrap; }
	div.dataTables_wrapper {
		width: 1250px;
		margin: 0 auto;
	}

	</style>
	
	<style>
	    tfoot input {
        width: 100%;
        padding: 3px;
        box-sizing: border-box;
    }
	</style>
	
  <script >

      $(document).ready(function() {

          var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_Reservation.txt'
          });

          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(5).text();
              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name;
                  location.href = (url);
              }
          });


      });        
  </script>  

  
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>待處理設備預約</legend></font>


  
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>預約單編號</th>
                    
                </tr>
            </thead>
            <tfoot>
        </tfoot>
            <tbody>   
            </tbody> 
        </table>    


</fieldset>--%>


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
            table2.search( locak );
            table2.draw();
            table3.search( locak );
            table3.draw();
            table4.search( locak );
            table4.draw();
            table5.search( locak );
            table5.draw();

        });
        
        var Local1="<%=strValue1()%>"
//        alert (Local1);
        if (Local1 == "DA40")
            var ddl = document.getElementById('the_status').value = 'TP';
        else
            var ddl = document.getElementById('the_status').value = 'WJ';
        var name1="<%=strName1()%>"
//        alert(name1);
        var table = $('#example').DataTable({
                       
            
              "scrollX": true,
               
              "ajax": '../ajax/data/arays_Reservation_'+ name1+'.txt',
              
              "columnDefs": [
                { "visible": false, "targets": 8 },
               
                { "visible": false, "targets": 7 }
              ]
           
              
          });

          $('#example tbody').on('click', 'tr', function() {
              var name = table.row(this).data()[7];

              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name + "&Kind=0";
                  location.href = (url);
              }
          }); 
          
          
          var table1 = $('#example1').DataTable({
              "ajax": "../ajax/data/arays_Continuous_"+ name1+".txt",
              "deferRender": true,
              
              "columnDefs": [
                { "visible": false, "targets": 9 },
               
                { "visible": false, "targets": 8 }
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
//              var name = $('td', this).eq(8).text();
                var name = table1.row(this).data()[8];
              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name + "&Kind=0";
                  location.href = (url);
              }
          });
          
          var table2 = $('#example2').DataTable({
                        "ajax": "../ajax/data/arays_Reservation_Agent"+ name1 +".txt",
//              "ajax": '../ajax/data/arays_Reservation_' + name1 + '.txt',
              "deferRender": true,
              
              "columnDefs": [
                { "visible": false, "targets": 8 },
               
                { "visible": false, "targets": 7 }
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

          $('#example2 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(7).text();
                var name = table2.row(this).data()[7];
              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name + "&Kind=1";
                  location.href = (url);
              }
          });
          
          var table3 = $('#example3').DataTable({
                        "ajax": "../ajax/data/arays_Continuous_Agent"+ name1 +".txt",
//              "ajax": "../ajax/data/arays_Continuous_" + name1 + ".txt",
              "deferRender": true,
              
              "columnDefs": [
                { "visible": false, "targets": 9 },
               
                { "visible": false, "targets": 8 }
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

          $('#example3 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(8).text();
                var name = table3.row(this).data()[8];
              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name + "&Kind=1";
                  location.href = (url);
              }
          });
          
          var table4 = $('#example4').DataTable({
                        "ajax": "../ajax/data/arays_Reservation_Leader"+ name1 +".txt",
//              "ajax": '../ajax/data/arays_Reservation_' + name1 + '.txt',
              "deferRender": true,
              
              "columnDefs": [
                { "visible": false, "targets": 8 },
               
                { "visible": false, "targets": 7 }
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

          $('#example4 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(7).text();
                var name = table4.row(this).data()[7];
              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name + "&Kind=2";
                  location.href = (url);
              }
          });
          
          var table5 = $('#example5').DataTable({
                        "ajax": "../ajax/data/arays_Continuous_Leader"+ name1 +".txt",
//              "ajax": "../ajax/data/arays_Continuous_" + name1 + ".txt",
              "deferRender": true,
              
              "columnDefs": [
                { "visible": false, "targets": 9 },
               
                { "visible": false, "targets": 8 }
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

          $('#example5 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(8).text();
            var name = table5.row(this).data()[8];
              if (name != "") {
                  var url = "ReservationAssign.aspx?ID=" + name + "&Kind=2";
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
            table2.search( locak );
            table2.draw();
            table3.search( locak );
            table3.draw();
            table4.search( locak );
            table4.draw();
            table5.search( locak );
            table5.draw();
          
                  
      });

    
  
  </script>
  
  <select name="the_status" id="the_status">
    <option value="TP">台北</option>
    <option value="WJ">吳江</option>
    
</select>

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">預約設備</a></li>
    <li><a href="#tabs-2">續借設備</a></li>
    <li><a href="#tabs-3">預約設備(代理人)</a></li>
    <li><a href="#tabs-4">續借設備(代理人)</a></li>
    <li><a href="#tabs-5">預約設備(管理人)</a></li>
    <li><a href="#tabs-6">續借設備(管理人)</a></li>
  </ul>
  <div id="tabs-1">
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>預約時段</th>
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
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>原歸還日期</th>
                    <th>預計歸還日期</th>
                    <th>預約時段</th>
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
  <div id="tabs-3">
        <table id="example2" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>預約時段</th>
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
  <div id="tabs-4">
        <table id="example3" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>原歸還日期</th>
                    <th>預計歸還日期</th>
                    <th>預約時段</th>
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
  <div id="tabs-5">
        <table id="example4" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>預約時段</th>
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
  <div id="tabs-6">
        <table id="example5" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <th>廠牌</th>
                    <th>型號</th>
                    <th>借用日期</th>
                    <th>原歸還日期</th>
                    <th>預計歸還日期</th>
                    <th>預約時段</th>
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

