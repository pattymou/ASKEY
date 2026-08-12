<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="DelaySample.aspx.cs" Inherits="WebForm_DelaySample" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <link rel="stylesheet" href="../css/jquery.dataTables.css">
  <script src="../js/jquery_1.11.0.min.js"></script>
  <script src="../js/jquery.dataTables.min.js"></script>
	
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
              "ajax": '../ajax/data/arays_DelayApparatus.txt'
          });


          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(11).text();
              if (name != "") {
                  var url = "ModifySReservationStatus.aspx?ID=" + name;
                 
                  location.href = (url);
              }
          });


      });        
  </script>  
 

  
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>逾期尚未歸還樣品</legend></font>


  
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>編號</th>
                    <th>類別</th>
                    <th>功能</th>
                    <th>項目</th>
                    <th>Model Name</th>
                    <th>借用日期</th>
                    <th>預計歸還日期</th>
                    <th>借用人</th>
                    <th>借用人分機</th>
                    <th>代理人</th>
                    <th>代理人分機</th>                    
                    <th>預約單編號</th>
                    
                </tr>
            </thead>
            <tfoot>

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

