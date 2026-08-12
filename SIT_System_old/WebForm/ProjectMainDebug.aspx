<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectMainDebug.aspx.cs" Inherits="WebForm_ProjectMainDebug" Title="" %>

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
  <script>
    $(function() {
    $( "#tabs1" ).tabs();
  });
  </script>
  
  <script>
    $(function() {
    $( "#tabs2" ).tabs();
  });
  </script>
  

	
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


        });
        
//        var Local1="<%=strValue1()%>"
        var Local1 = '<%= Session["EmpDepartment"].ToString() %>';
//        alert (Local1);
        if (Local1 == "DA40")
            var ddl = document.getElementById('the_status').value = 'TP';
        else
            var ddl = document.getElementById('the_status').value = 'WJ';
        
        
          var table = $('#example').DataTable({
              "ajax": "../ajax/data/ProjectMain_Open.txt",
              "deferRender": true,
              
              "columnDefs": [
               
                { "visible": false, "targets": 1 }
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
          
          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(0).text();
                var local = table.row(this).data()[1];

              if (name != "") {
                  var url = "ProjectView1.aspx?Fun=19&Kind=" + local +"&ID=" + name;
                  location.href = (url);
              }
          });  
          
          
          var table1 = $('#example1').DataTable({
              "ajax": "../ajax/data/ProjectMain_Close.txt",
              "deferRender": true,
              
              "columnDefs": [
               
                { "visible": false, "targets": 1 }
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
              var name = $('td', this).eq(0).text();
            var local = table1.row(this).data()[1];

              if (name != "") {
                  var url = "ProjectView1.aspx?Fun=19&Kind=" + local +"&ID=" + name;
                  location.href = (url);
              }
          });
          
          var table2 = $('#example2').DataTable({
              "ajax": "../ajax/data/ProjectMain_Hold.txt",
              "deferRender": true,
              
              "columnDefs": [
               
                { "visible": false, "targets": 1 }
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
              var name = $('td', this).eq(0).text();
              var local = table2.row(this).data()[1];

              if (name != "") {
                  var url = "ProjectView1.aspx?Fun=19&Kind=" + local +"&ID=" + name;
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
          
          
          /////////////////////////////////////
          
//          $('#example2-1').DataTable({
//              "ajax": "../ajax/data/ProjectMain_Open_WJ.txt",
//              "deferRender": true,
//              

//              initComplete: function() {
//                  this.api().columns().every(function() {
//                      
//                      var column = this;
//                      var select = $('<select><option value=""></option></select>')
//                    .appendTo($(column.footer()).empty())
//                    .on('change', function() {
//                        var val = $.fn.dataTable.util.escapeRegex(
//                            $(this).val()
//                        );

//                        column
//                            .search(val ? '^' + val + '$' : '', true, false)
//                            .draw();
//                    });

//                      column.data().unique().sort().each(function(d, j) {
//                          select.append('<option value="' + d + '">' + d + '</option>')
//                      });
//                  });
//              },
//          });
//          
//          $('#example2-1 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(0).text();

//              if (name != "") {
//                  var url = "ProjectView1.aspx?Fun=9&Kind=WJ&ID=" + name;
//                  location.href = (url);
//              }
//          });  
//          
//          
//          $('#example2-2').DataTable({
//              "ajax": "../ajax/data/ProjectMain_Close_WJ.txt",
//              "deferRender": true,
//              

//              initComplete: function() {
//                  this.api().columns().every(function() {
//                      
//                      var column = this;
//                      var select = $('<select><option value=""></option></select>')
//                    .appendTo($(column.footer()).empty())
//                    .on('change', function() {
//                        var val = $.fn.dataTable.util.escapeRegex(
//                            $(this).val()
//                        );

//                        column
//                            .search(val ? '^' + val + '$' : '', true, false)
//                            .draw();
//                    });

//                      column.data().unique().sort().each(function(d, j) {
//                          select.append('<option value="' + d + '">' + d + '</option>')
//                      });
//                  });
//              }
//          });
//          
//          $('#example2-2 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(0).text();

//              if (name != "") {
//                  var url = "ProjectView1.aspx?Fun=9&Kind=WJ&ID=" + name;
//                  location.href = (url);
//              }
//          });
//          
//          $('#example2-3').DataTable({
//              "ajax": "../ajax/data/ProjectMain_Hold_WJ.txt",
//              "deferRender": true,
//              

//              initComplete: function() {
//                  this.api().columns().every(function() {
//                      
//                      var column = this;
//                      var select = $('<select><option value=""></option></select>')
//                    .appendTo($(column.footer()).empty())
//                    .on('change', function() {
//                        var val = $.fn.dataTable.util.escapeRegex(
//                            $(this).val()
//                        );

//                        column
//                            .search(val ? '^' + val + '$' : '', true, false)
//                            .draw();
//                    });

//                      column.data().unique().sort().each(function(d, j) {
//                          select.append('<option value="' + d + '">' + d + '</option>')
//                      });
//                  });
//              },
//          });
//          
//          $('#example2-3 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(0).text();

//              if (name != "") {
//                  var url = "ProjectView1.aspx?Fun=9&Kind=WJ&ID=" + name;
//                  location.href = (url);
//              }
//          });        
      });


  
  </script>


 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增專案]</asp:LinkButton> 
 <br />
 <br />
 
   <select name="the_status" id="the_status">
    <option value="TP">台北</option>
    <option value="WJ">吳江</option>
    
</select>
 

<%--<div id="tabs1">
  <ul>
    <li><a href="#tabs1-1">台北</a></li>
    <li><a href="#tabs1-2">吳江</a></li>
  </ul>
  <div id="tabs1-1"> --%>
   

        <div id="tabs">
          <ul>
            <li><a href="#tabs-1">Open</a></li>
            <li><a href="#tabs-2">Close</a></li>
            <li><a href="#tabs-3">Hold</a></li>
          </ul>
          <div id="tabs-1">
                <table id="example" class="display" cellspacing="0" width="100%">
                <thead>
                        <tr>
                            
                            <th>專案名稱</th>
                            <th>地點</th>
                            
                        </tr>
                    </thead>
                    <tfoot>
                    <tr>
                            <th>專案名稱</th>
                            <th>地點</th>
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

                            <th>專案名稱</th>
                            <th>地點</th>

                            
                        </tr>
                    </thead>
                    <tfoot>
                    <tr>
                            <th>專案名稱</th>
                            <th>地點</th>

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

                            <th>專案名稱</th>
                            <th>地點</th>

                            
                        </tr>
                    </thead>
                    <tfoot>
                    <tr>

                            <th>專案名稱</th>
                            <th>地點</th>

                    </tr>
                </tfoot>
                    <tbody>   
                    </tbody> 
                </table>     
          </div>
        </div>
<%--</div> 
<div id="tabs1-2">
        <div id="tabs2">
          <ul>
            <li><a href="#tabs2-1">Open</a></li>
            <li><a href="#tabs2-2">Close</a></li>
            <li><a href="#tabs2-3">Hold</a></li>
          </ul>
          <div id="tabs2-1">
                <table id="example2-1" class="display" cellspacing="0" width="100%">
                <thead>
                        <tr>
                            
                            <th>專案名稱</th>
                            
                        </tr>
                    </thead>
                    <tfoot>
                    <tr>
                            <th>專案名稱</th>
                    </tr>
                </tfoot>
                    <tbody>           
                    </tbody> 
                </table>   
          
          </div>
          <div id="tabs2-2">
            <table id="example2-2" class="display" cellspacing="0" width="100%">
                <thead>
                        <tr>

                            <th>專案名稱</th>

                            
                        </tr>
                    </thead>
                    <tfoot>
                    <tr>
                            <th>專案名稱</th>

                    </tr>
                </tfoot>
                    <tbody>   
                    </tbody> 
                </table>  
          
          </div>
          <div id="tabs2-3">
            <table id="example2-3" class="display" cellspacing="0" width="100%">
                <thead>
                        <tr>

                            <th>專案名稱</th>

                            
                        </tr>
                    </thead>
                    <tfoot>
                    <tr>

                            <th>專案名稱</th>

                    </tr>
                </tfoot>
                    <tbody>   
                    </tbody> 
                </table>     
          </div>
        </div>
</div> --%>
</asp:Content>

