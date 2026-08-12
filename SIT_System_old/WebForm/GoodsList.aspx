<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsList.aspx.cs" Inherits="WebForm_GoodsList" %>

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
            table1.search( locak );
            table1.draw();
            

        });
        
        var Local1="<%=strValue1()%>"
//        alert (Local1);
        if (Local1 == "DA40")
            var ddl = document.getElementById('the_status').value = 'TP';
        else
            var ddl = document.getElementById('the_status').value = 'WJ';


          var table = $('#example').DataTable({
//              "scrollX": true,
              "ajax": '../ajax/data/arays_PR.txt',
              "columnDefs": [
                { "visible": false, "targets": 6 },
               
                { "visible": false, "targets": 7 }
              ]
          });


          $('#example tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(6).text();
                var name = table.row(this).data()[6];
              if (name != "") {
                  var url = "PR_Detail.aspx?ID=" + name;
                  location.href = (url);
              }
          });
          
          
          var table1 = $('#example1').DataTable({
//              "scrollX": true,
              "ajax": '../ajax/data/arays_PR_Hold.txt',
              "columnDefs": [
                { "visible": false, "targets": 6 },
               
                { "visible": false, "targets": 7 }
              ]
          });


          $('#example1 tbody').on('click', 'tr', function() {
//              var name = $('td', this).eq(6).text();
                var name = table1.row(this).data()[6];
              if (name != "") {
                  var url = "PR_Detail.aspx?ID=" + name;
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
 
<%-- <br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增工作項目]</asp:LinkButton> 
 <br />--%>
 


  
<%--<fieldset>--%>
    <table id="Table1" class="one" width="100%">
         
         <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增採購資訊]</asp:LinkButton> 
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="linkHistorical" runat="server" OnClick="lbtnHistorical_Click">[歷史採購資訊]</asp:LinkButton>         
         
     </table> 
<select name="the_status" id="the_status">
    <option value="TP">台北</option>
    <option value="WJ">吳江</option>
    
</select>
<br />
<div id="tabs">
  <ul>
    <li><a href="#tabs-1">Open</a></li>
    <li><a href="#tabs-2">Hold</a></li>
  </ul>
  <div id="tabs-1">
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>開立PR日期</th>
                    <th>請購單號</th>
                    <th>預計交貨日</th>
                    <th>簽呈編號</th>
                    <th>需求原因</th>
                    <th>台幣總金額</th>                    
                    <th>ID</th>
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
                    <th>開立PR日期</th>
                    <th>請購單號</th>
                    <th>預計交貨日</th>
                    <th>簽呈編號</th>
                    <th>需求原因</th>
                    <th>台幣總金額</th>                    
                    <th>ID</th>
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
  
  
           


<%--</fieldset>--%>
</asp:Content>

