<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="GanttProject.aspx.cs" Inherits="WebForm_GanttProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../js/jquery_1.11.0.min.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>

     
     
<fieldset>     
    <table id="Table1" class="one" width="100%">
        <tr>
        <asp:Label ID="Label10" runat="server" Text="人員："></asp:Label>
        
        <asp:DropDownList ID="ddlEmployees" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlEmployees_SelectedIndexChanged">
        </asp:DropDownList>
<%--        <asp:DropDownList ID="ddlEmployees" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlEmployees_SelectedIndexChanged">
        </asp:DropDownList>--%>
        </tr>
    </table> 
    
    <br />
    
    <table >
        <script src="../js/jquery.fn.gantt.min.js" type="text/javascript"></script>
     <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
    <div class="gantt"></div>
    <script >
        $(function() {
            $(".gantt").gantt({
                source: <%= csSource %>,
                months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
                dow: ["日", "一", "二", "三", "四", "五", "六"],
                navigate: "scroll",
                scale: "days",
                maxScale: "months",
                minScale: "days",
                itemsPerPage: 10
            });
        });    
    </script>
    </table> 
    
</fieldset> 
    
</asp:Content>

