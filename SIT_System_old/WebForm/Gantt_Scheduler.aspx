<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="Gantt_Scheduler.aspx.cs" Inherits="WebForm_Gantt_Scheduler" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="~/Media/layout.css" rel="stylesheet" type="text/css" />
    <link href='~/themes/scheduler_green.css' rel="stylesheet" type="text/css" />
    <link href='~/themes/menu_default.css' rel="stylesheet" type="text/css" />
    <link href='~/themes/bubble_default.css' rel="stylesheet" type="text/css" />
<%--    <script src="~/Scripts/DayPilot/modal.js" type="text/javascript"></script>
    <script src="~/Scripts/DayPilot/event_handling.js" type="text/javascript"></script>--%>
    <script src="~/Scripts/jquery-1.4.1.min.js" type="text/javascript" ></script>
<script src='<%# ResolveUrl("~/Scripts/DayPilot/daypilot-modal-2.0.js") %>' type="text/javascript"></script>
<script src='<%# ResolveUrl("~/Scripts/App/gantt.js") %>' type="text/javascript"></script>    
    
    <fieldset>
      
<div style="overflow-x:auto; width: 100%; position: relative;" id="dps">
<DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" 
        HeaderFontSize="8pt" 
        HeaderHeight="20" 
        EventHeight="20"
        EventFontSize="11px" 
        CellDuration="1440" 
        CellWidth="50"

        BorderColor="#aaaaaa"
        EventBorderColor="#aaaaaa"

        TimeRangeSelectedHandling="JavaScript"
        TimeRangeSelectedJavaScript="create('{0}', null, '{1}');"
        EventClickHandling="JavaScript"
        EventClickJavaScript="edit('{0}')"
        OnHeaderColumnWidthChanged="DayPilotScheduler1_HeaderColumnWidthChanged"
        OnBeforeResHeaderRender="DayPilotScheduler1_BeforeResHeaderRender"

        OnBeforeEventRender="DayPilotScheduler1_BeforeEventRender"
        ViewType="Gantt"
        >
        <HeaderColumns>
        <DayPilot:RowHeaderColumn title="Task" Width="150" />
        <DayPilot:RowHeaderColumn title="Duration" Width="80" />
        </HeaderColumns>
    </DayPilot:DayPilotScheduler>
</div>


<div style="display:none">
<asp:HiddenField ID="HiddenOrder" runat="server" />
<asp:Button id="ButtonRefresh" runat="server" OnClick="ButtonRefresh_Click" />
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
        init();
    });

    var id = {};
    id.refreshButton = '<%# ButtonRefresh.UniqueID %>';
    id.root = '<%# ResolveUrl("~/") %>';

    var drag = {};
    function init() {
        $(document).ready(function() {
            $(".task_status.planned")
            .css("cursor", "move")
            .attr('unselectable', 'on')
            .css('user-select', 'none')
            .css('-webkit-user-select', 'none')
            .css('-moz-user-select', 'none')
            .each(function() {
                $(this)[0].onselectstart = function() { return false; };
            });
            /*
            $('#MainContent_DayPilotScheduler1_corner div').filter(function() {
            return $(this).css('vertical-align') == 'top';
            }).css("padding-left", "2px").css("padding-top", "2px");
            */
            $(".task_status.planned").mousedown(function(e) {
                var id = $(this).data("taskid");
                drag.active = true;
                drag.id = id;
                drag.start = DayPilot.mo3($("#dps")[0], e.originalEvent);
                drag.source = $(this);

                //alert("drag.id:" + drag.id);

                var div = document.createElement("div");
                div.style.position = "absolute";
                div.style.height = "2px";
                div.style.width = "60px";
                div.style.backgroundColor = "red";
                div.style.display = "none";
                div.style.userSelect = "none";
                div.style.WebkitUserSelect = "none";
                div.style.MozUserSelect = "none";
                div.setAttribute("unselectable", "on");
                $("#dps")[0].appendChild(div);

                drag.div = div;
                //alert("drag.start:" + drag.start);
            });
            $(document).mousemove(function(e) {
                if (!drag.active) {
                    return;
                }
                drag.source.parent().css({ opacity: 0.5 });

                var rowHeight = 20;
                var headerHeight = 20;
                var coords = DayPilot.mo3($("#dps")[0], e.originalEvent);
                drag.position = Math.floor((coords.y - headerHeight) / rowHeight);
                drag.position = Math.max(0, drag.position);

                var offset = $("#dps").offset();
                drag.div.style.top = (offset.top + rowHeight * drag.position + headerHeight) + "px";
                drag.div.style.left = offset.left + "px";
                drag.div.style.display = "";
            });
            $(document).mouseup(function() {
                if (!drag.active) {
                    return;
                }

                $("#dps")[0].removeChild(drag.div);

                var order = [];
                var placed = false;
                $(".task_status").each(function(index) {
                    if (!$(this).hasClass("planned")) {
                        return;
                    }
                    var id = $(this).data("taskid");
                    //alert("id:" + id);

                    if (index == drag.position) {
                        order.push(drag.id);
                        placed = true;
                    }

                    if (id == drag.id) {
                        return;
                    }
                    order.push(id);
                });

                if (!placed) {
                    order.push(drag.id);
                }

                //alert("new order:" + order.join(","));
                updateOrder(order.join(','));

                drag = {};
            });
        });
    }

    function updateOrder(order) {
        $("#<%# HiddenOrder.ClientID %>").val(order);
        __doPostBack(id.reorderButton, '');
    }

    init();
</script>                  
    
    </fieldset> 
    







    
</asp:Content>

