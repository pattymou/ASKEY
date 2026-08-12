<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectApplication.aspx.cs" Inherits="WebForm_ProjectApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<link rel="stylesheet" href="../css/jquery.dataTables.css">
  <script src="../js/jquery_1.11.0.min.js"></script>
  <script src="../js/jquery.dataTables.min.js"></script>--%>
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
        
         var Local1 = '<%= Session["EmpDepartment"].ToString() %>';
//        alert (Local1);
        if (Local1 == "DA40")
            var ddl = document.getElementById('the_status').value = 'TP';
        else
            var ddl = document.getElementById('the_status').value = 'WJ';
          // Setup - add a text input to each footer cell
//          $('#example tfoot th').each(function() {
//              var title = $('#example thead th').eq($(this).index()).text();
//              $(this).html('<input type="text" placeholder="Search ' + title + '" />');
//          });
          

          // DataTable
          var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_assign.txt',
              
              "columnDefs": [

                { "visible": false, "targets": 13 },
                { "visible": false, "targets": 12 }
              ]
          });

          // Apply the search
//          table.columns().eq(0).each(function(colIdx) {
//              $('input', table.column(colIdx).footer()).on('keyup change', function() {
//                  table
//                .column(colIdx)
//                .search(this.value)
//                .draw();
//              });
//          });

          $('#example tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(12).text();
                var name = table.row(this).data()[12];
              //              var url = "https://tw.yahoo.com";
              //                        var url = "http://localhost/SIT_System/WebForm/ProjectAssign.aspx?Value=1&ID=" + name;
              if (name != "") {
                  var url = "ProjectAssign.aspx?Value=1&ID=" + name;
                  //              window.open(url);
                  location.href = (url);
              }
          });
        /////////////////////////////////////////////////////
        // Setup - add a text input to each footer cell
//          $('#example1 tfoot th').each(function() {
//              var title = $('#example1 thead th').eq($(this).index()).text();
//              $(this).html('<input type="text" placeholder="Search ' + title + '" />');
//          });

          // DataTable
//          var table1 = $('#example1').DataTable({
//              "scrollX": true,
//              "ajax": '../ajax/data/arays_assign_WJ.txt',
//              
//              "columnDefs": [

//                { "visible": false, "targets": 12 }
//              ]
//          });

          // Apply the search
//          table1.columns().eq(0).each(function(colIdx) {
//              $('input', table1.column(colIdx).footer()).on('keyup change', function() {
//                  table
//                .column(colIdx)
//                .search(this.value)
//                .draw();
//              });
//          });

//          $('#example1 tbody').on('click', 'tr', function() {
////              var name = $('td', this).eq(12).text();
//            var name = table1.row(this).data()[12];
//              //              var url = "https://tw.yahoo.com";
//              //                        var url = "http://localhost/SIT_System/WebForm/ProjectAssign.aspx?Value=1&ID=" + name;
//              if (name != "") {
//                  var url = "ProjectAssign.aspx?Value=1&ID=" + name;
//                  //              window.open(url);
//                  location.href = (url);
//              }
//          });

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
<font face="verdana"color="0000DD"size="4" >待處理申請單</font>
<br />
<select name="the_status" id="the_status">
    <option value="TP">台北</option>
    <option value="WJ">吳江</option>
    
</select>

  <%--<div id="tabs">
  <ul>
    <li><a href="#tabs-1">台北</a></li>
    <li><a href="#tabs-2">吳江</a></li>
  </ul>
  <div id="tabs-1">--%>
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>類別</th>
                    <th>專案名稱</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W Engineer</th>
                    <th>H/W VR.</th>
                    <th>S/W Engineer</th>
                    <th>S/W VR.</th>
                    <th>部門</th>
                    <th>Chipset</th>
                    <th>DSP Model</th>
                    <th>負責人</th>
                    <th>申請單編號</th>
                    <th>地點</th>
                    
                </tr>
            </thead>
            
        </table>    
    <%--</div> 
    <div id="tabs-2">
        <table id="example1" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>類別</th>
                    <th>專案名稱</th>
                    <th>Customer</th>
                    <th>NPI</th>
                    <th>H/W Engineer</th>
                    <th>H/W VR.</th>
                    <th>S/W Engineer</th>
                    <th>S/W VR.</th>
                    <th>部門</th>
                    <th>Chipset</th>
                    <th>DSP Model</th>
                    <th>負責人</th>
                    <th>申請單編號</th>
                    
                </tr>
            </thead>
            
        </table>  
    </div> 
</div>--%> 


</fieldset> 


</asp:Content>

