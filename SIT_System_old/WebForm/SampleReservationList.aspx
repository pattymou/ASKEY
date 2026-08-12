<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="SampleReservationList.aspx.cs" Inherits="WebForm_SampleReservationList" Title="" %>

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
        var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_Reservation.txt'
          });

          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(7).text();
              if (name != "") {
                  var url = "SampleReservationAssign.aspx?ID=" + name;
                  location.href = (url);
              }
          }); 
          
          
          $('#example1').DataTable({
              "ajax": "../ajax/data/arays_Continuous.txt",
              "deferRender": true,
              

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
              var name = $('td', this).eq(8).text();
              if (name != "") {
                  var url = "SampleReservationAssign.aspx?ID=" + name;
                  location.href = (url);
              }
          });
          
                 
      });


  
  </script>
     

<div id="tabs">
  <ul>
    <li><a href="#tabs-1">預約樣品</a></li>
    <li><a href="#tabs-2">續借樣品</a></li>
  </ul>
  <div id="tabs-1">
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>編號</th>
                    <th>類別</th>
                    <th>功能</th>
                    <th>項目</th>
                    <th>Model Name</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>樣品編號</th>
                    
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
                    <th>編號</th>
                    <th>類別</th>
                    <th>功能</th>
                    <th>項目</th>
                    <th>Model Name</th>
                    <th>借用日期</th>
                    <th>原歸還日期</th>
                    <th>預計歸還日期</th>
                    <th>樣品編號</th>
                    
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


