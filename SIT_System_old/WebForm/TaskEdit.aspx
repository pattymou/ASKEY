<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="TaskEdit.aspx.cs" Inherits="WebForm_TaskEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--    <link rel="stylesheet" href="//apps.bdimg.com/libs/jqueryui/1.10.4/css/jquery-ui.min.css">--%>
<%--  <script src="//apps.bdimg.com/libs/jquery/1.10.2/jquery.min.js"></script>--%>
<%--  <script src="//apps.bdimg.com/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>--%>
    <%--<link rel="stylesheet" href="../css/jquery-ui.min.css">--%>
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
<%--  <script src="../js/jquery_1.11.0.min.js"></script>--%>
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script> 
 

<%--    <style>
    /* Adjust the jQuery UI widget font-size: */
    .ui-widget {
        font-size: 0.95em;
    }
    </style>--%>

<fieldset>

    <font face="verdana"color="0000DD"size="6" ><legend>編輯子任務</legend></font>
    <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
 <%--      <tr>
            <td>
                
                <asp:Label ID="Label9" runat="server" Text="子任務ID"></asp:Label>
                
            </td>
            <td colspan =3>
                
                <asp:TextBox ID="txtCaseID" runat="server"></asp:TextBox>
                
            </td>
        </tr>--%>
        <tr>
            <td>
                
                <asp:Label ID="Label1" runat="server" Text="子任務名稱"></asp:Label>
                
            </td>
            <td colspan =3>
                
                <asp:TextBox ID="txtTask" runat="server" Width="526px"></asp:TextBox>
                
            </td>
        </tr>
        <tr id="Name1" runat ="server">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Sub PU"></asp:Label>
            </td> 
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server">
                </asp:DropDownList>              
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="機種名稱"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtModelName" runat="server" ></asp:TextBox>            
            </td>
        </tr>
        <tr>
            <%--<td>
                <asp:Label ID="Label2" runat="server" Text="指派工程師"></asp:Label>
            </td>
            <td>
            
                <asp:DropDownList ID="ddlAssign" runat="server">
                </asp:DropDownList>
            
            </td>--%>
            <td>
                
                <asp:Label ID="Label3" runat="server" Text="狀態"></asp:Label>
                
            </td>
            <td colspan =3>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem>Open</asp:ListItem>
                    <asp:ListItem>Close</asp:ListItem>
                    <asp:ListItem>Hold</asp:ListItem>
                </asp:DropDownList>                
            </td>
        </tr>
        <tr>
<%--            <td>
                <asp:Label ID="Label4" runat="server" Text="開始日期"></asp:Label>
            </td>
            <td>
                    <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                    
                     <script>
                         $(function() {
                         $("#datepicker").datepicker();
                     });
                    
                     </script>             
            </td>--%>
            <td>
                <asp:Label ID="Label4" runat="server" Text="開始日期"></asp:Label>
            </td>
            <td>
                <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                 <script>
                     $(function() {
                         $("#datepicker").datepicker();
                     });
                 </script>
            </td>  
            <td>
                <asp:Label ID="Label5" runat="server" Text="預計完成日期"></asp:Label>
            </td>
            <td>
                <input type="text" id="datepicker1" name = "date2" value = "<%=strEnd%>">
                 <script>
                     $(function() {
                         $("#datepicker1").datepicker();
                     });
                 </script>
            </td>                      
           
<%--            <td>
                <asp:Label ID="Label5" runat="server" Text="預計完成日"></asp:Label>
            </td>
            <td>
                    <input type="text" id="datepicker1" name = "date2" value = "<%=strEnd%>">
                     <script>
                         $(function() {
                             $("#datepicker1").datepicker();
                         });
                     </script>            
            </td>--%>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="結果判定"></asp:Label>
            </td> 
            <td>
                <asp:DropDownList ID="ddlResult" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Pass</asp:ListItem>
                    <asp:ListItem>Fail</asp:ListItem>
                    <asp:ListItem>未完成</asp:ListItem>
                </asp:DropDownList>                
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="進度"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlProgress" runat="server">
                    <asp:ListItem>0</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>35</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>55</asp:ListItem>
                    <asp:ListItem>60</asp:ListItem>
                    <asp:ListItem>65</asp:ListItem>
                    <asp:ListItem>70</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>80</asp:ListItem>
                    <asp:ListItem>85</asp:ListItem>
                    <asp:ListItem>90</asp:ListItem>
                    <asp:ListItem>95</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>            
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="實驗室名稱"></asp:Label>
            </td>
            <td colspan =3>
                <asp:TextBox ID="txtLab" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="報價金額"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQuoted" runat="server" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="請款金額"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtReimburse" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label ID="Label8" runat="server" Text="備註"></asp:Label>
            </td>
            <td colspan =3>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="578px"></asp:TextBox>
            </td>
        </tr>
        
                
    
    </table> 
    
    <table >
            <tr>
                <td>
                
                    <asp:Label ID="Label27" runat="server" Text="指派工程師"></asp:Label>
                
                </td>
            </tr>
            <tr>
            
			    <td>
			        
				    <asp:ListBox ID="listLeft" runat="server" DataTextField="Name_En" 
                        Height="237px" SelectionMode="Multiple" Width="321px"></asp:ListBox>
			        
				</td>
			    <td>
				    <asp:Button ID="btnRight" runat="server" Text=">" OnClick="btnRight_Click" Width="30px" /><br /><br />
				    <asp:Button ID="btnLeft" runat="server" Text="<" OnClick="btnLeft_Click" Width="30px" />
			    </td>
			    <td>
				    <asp:ListBox ID="listRight" runat="server" Width="321" Height="237" 
                        SelectionMode="Multiple" 
                         DataTextField="Name_En" 
                        ></asp:ListBox>
			    </td>            
                        
           
            </tr>
        </table>
            <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">

            <tr>
            <td align ="center" colspan = 4 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="儲存" 
    onclick="butOK_Click" />
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回上一頁" 
                                onclick="butReturn_Click" />    
                <br />
                <br />
            </td>
        </tr>
        </table>
</fieldset> 

</asp:Content>

