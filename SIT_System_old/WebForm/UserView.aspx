<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="UserView.aspx.cs" Inherits="WebForm_UserView" %>

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
	
  <script >


      $(document).ready(function() {
          $('#example').dataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_Employees.txt'
          });

          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(0).text();
              //              var url = "https://tw.yahoo.com";
              //              var url = "http://localhost/SIT_System/WebForm/AddUser.aspx?ID=" + name;
              var url = "AddUser.aspx?ID=" + name;
              //              window.open(url);
              location.href = (url);
          });
      });        
  </script>
  
  
  
  <br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增人員]</asp:LinkButton> 
 <br />

  
<fieldset>
<font face="verdana"color="0000DD"size="4" ><legend>人員設定</legend></font>

  
        <table id="example" class="display" cellspacing="0" width="100%">
        <thead>
                <tr>
                    <th>登入名稱</th>
                    <th>姓名</th>
                                        
                </tr>
            </thead>
            <tbody>   
            </tbody> 
        </table>    


</fieldset>
</asp:Content>

