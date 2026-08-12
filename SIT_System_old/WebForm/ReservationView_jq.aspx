<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ReservationView_jq.aspx.cs" Inherits="WebForm_ReservationView_jq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<link rel="stylesheet" href="http://fullcalendar.io/js/fullcalendar-2.2.6/lib/cupertino/jquery-ui.min.css">--%>
    <%--<link rel="stylesheet" type="text/css" href="http://arshaw.com/js/fullcalendar-1.5.3/fullcalendar/fullcalendar.css">--%>
    <%--<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script>--%>       <%--<script type='text/javascript' src="http://www.arshaw.com/js/fullcalendar-1.5.3/fullcalendar/fullcalendar.min.js"></script>--%>    <link rel="stylesheet" href="../css/Calendar/jquery-ui.min.css">    <link rel="stylesheet" type="text/css" href="../css/Calendar/fullcalendar.css">    <link href='../css/Calendar/fullcalendar.print.css' rel='stylesheet' media='print' />    <script src='../js/moment.min.js'></script>    <script type='text/javascript' src='../js/Calendar/jquery.min.js'></script>    <script type='text/javascript' src="../js/Calendar/fullcalendar.min.js"></script>            <script >

        $(document).ready(function() {

            $('#calendar').fullCalendar({
                //                theme: true,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                selectable: true,
                events: "../ajax/Calendar.ashx",
                eventRender: function(event, element) {
                    element.attr('title', event.tip);
                },
                eventMouseover: function(calEvent, jsEvent, view) {
                    var fstart = $.fullCalendar.formatDate(calEvent.start, "yyyy/MM/dd");
                    var fend = $.fullCalendar.formatDate(calEvent.end, "yyyy/MM/dd");
                    var mission = calEvent.mission;
                    var gname = calEvent.gname;
                    var period = calEvent.period;
                    var kind = calEvent.kind;
                    var department = calEvent.department;
                    var name1 = calEvent.name;
                    //                    $(this).attr('title', fstart + " - " + fend + " " + calEvent.topic + " : " + calEvent.description);
                    
                    if (kind=="1")
                        $(this).attr('title', "部門：" + department + "　,姓名：" + name1 + "　,機種名稱：" + gname);
                    else
                        $(this).attr('title', "使用時間：" + fstart + " - " + fend + "　,預約時段：" + period + "　,任務名稱：" + mission + "　,機種名稱：" + gname);
                    $(this).css('font-weight', 'normal');
                    $(this).tooltip({
                        effect: 'toggle',
                        cancelDefault: true
                    });
                }

            });
        });            </script>    <style>
	    #calendar {
		    max-width: 900px;
		    margin: 0 auto;
	    }

    </style>        <table id="Table1" class="one" width="100%">        <tr>            <td>                <asp:Label ID="lblName" runat="server" Font-Size="Large" ForeColor="#3333FF"></asp:Label>
            </td>
        </tr>
    </table> 
    <br />
    <br />
<%--<form id="form1" runat="server">--%>
    <div id='calendar'></div>

<%--</form>--%>

    <table id="Table5" class="one" width="100%">
        <tr>
            <td align ="center" colspan = 2 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="上一頁" 
                        onclick="butOK_Click" />
                    
                <br />
                <br />
            </td>
        </tr>
    </table> 

</asp:Content>

