<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApparatusDailyReport.aspx.cs" Inherits="WebForm_ApparatusDailyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link rel="stylesheet" href="../css/jquery.dataTables.css">
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


          $('#example tfoot th').each(function() {
              var title = $('#example thead th').eq($(this).index()).text();
              $(this).html('<input type="text" placeholder="Search ' + title + '" />');
          });

          // DataTable
          var table = $('#example').DataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_DailyReport.txt'
          });

          // Apply the search
          table.columns().eq(0).each(function(colIdx) {
              $('input', table.column(colIdx).footer()).on('keyup change', function() {
                  table
                .column(colIdx)
                .search(this.value)
                .draw();
              });
          });

          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(7).text();
              //              var url = "https://tw.yahoo.com";
              //                        var url = "http://localhost/SIT_System/WebForm/ProjectAssign.aspx?Value=1&ID=" + name;
              if (name != "") {
                  var url = "DepartmentDailyReport.aspx?Value=1&ID=" + name;
                  //              window.open(url);
                  location.href = (url);
              }
          });


      });        
  </script>  
 
  
<fieldset>
<table id="Table1" class="one" width="100%">
    <tr>
        <td>
            <font face="verdana"color="0000DD"size="4" ><legend>設備預約工作日誌</legend></font>
            
            
        </td>
        <td align ="right">
            <asp:LinkButton ID="linkDelay" runat="server" OnClick="lbtnDelay_Click">[查閱所有工作日誌]</asp:LinkButton>            
        </td>
    </tr>
    <tr>
        <td>
            <font face="verdana"color="red"size="4" ><legend>*限填寫當天工作日誌</legend></font>
        </td>
    </tr>
</table> 
  
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>財產編號</th>
                    <th>設備名稱</th>
                    <%--<th>保管部門</th>--%>
                    <th>保管人</th>
                    <th>借用日期</th>
                    <th>歸還日期</th>
                    <th>預約時段</th>
                    <th>借用人</th>
                    <th>預約單編號</th>
                    
                </tr>
            </thead>
<%--            <tfoot>
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
        </tfoot>--%>
            <tbody>   
            </tbody> 
        </table>    


</fieldset>

</asp:Content>

