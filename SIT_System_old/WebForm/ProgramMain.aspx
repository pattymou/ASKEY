<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ProgramMain.aspx.cs" Inherits="WebForm_ProgramMain" Title="" %>

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
      $(document).ready(function() {
          $('#example').DataTable({
              "ajax": "../ajax/data/ProjectMain_Open.txt",
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
              },
          });
          
          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(0).text();

              if (name != "") {
                  var url = "ProjectView1.aspx?Fun=9&ID=" + name;
                  location.href = (url);
              }
          });  
          
          
          $('#example1').DataTable({
              "ajax": "../ajax/data/ProjectMain_Close.txt",
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
              var name = $('td', this).eq(0).text();

              if (name != "") {
                  var url = "ProjectView1.aspx?Fun=9&ID=" + name;
                  location.href = (url);
              }
          });
          
          $('#example2').DataTable({
              "ajax": "../ajax/data/ProjectMain_Hold.txt",
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
              },
          });
          
          $('#example2 tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(0).text();

              if (name != "") {
                  var url = "ProjectView1.aspx?Fun=9&ID=" + name;
                  location.href = (url);
              }
          });        
      });


  
  </script>

 <br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增自動化程式]</asp:LinkButton> 
 <br />
 
    

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
                    <th>Team</th>
                    <th>名稱</th>
                    
                </tr>
            </thead>
            <tfoot>
            <tr>
                    <th>Team</th>
                    <th>名稱</th>
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
                    <th>名稱</th>

                    
                </tr>
            </thead>
            <tfoot>
            <tr>
                    <th>Team</th>
                    <th>名稱</th>

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
                    <th>名稱</th>

                    
                </tr>
            </thead>
            <tfoot>
            <tr>
                    <th>Team</th>
                    <th>名稱</th>

            </tr>
        </tfoot>
            <tbody>   
            </tbody> 
        </table>     
  </div>
    </div>
</asp:Content>

