<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ReservationView.aspx.cs" Inherits="WebForm_ReservationView" %>

<%--<%@ Register assembly="DayPilot" namespace="DayPilot.Web.Ui" tagprefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
    <div>
        <a href="javascript:dpm.commandCallBack('previous');">Previous</a>
        |
        <a href="javascript:dpm.commandCallBack('next');">Next</a>
    </div>

    <table style="width:100%">
        <tr>
            <td valign="top" style="width:150px">
                <DayPilot:DayPilotNavigator ID="DayPilotNavigator1" runat="server" 
                    BoundDayPilotID="DayPilotMonth1" 
                    SelectMode="Month"
                    ShowMonths="3"
                    SkipMonths="3"
                    
                    DataStartField="startdate"
                    DataEndField="enddate" 
                    VisibleRangeChangedHandling="CallBack"
                    OnVisibleRangeChanged="DayPilotNavigator1_VisibleRangeChanged"
                    
                    ></DayPilot:DayPilotNavigator>
                <br />
                <br />
                <div style="height:20px"></div>        
            </td>
            <td valign="top">
                <DayPilot:DayPilotMonth 
                    ID="DayPilotMonth1" 
                    runat="server" 
                    DataEndField="enddate" 
                    DataStartField="startdate"
                    DataTextField="borrower" 
                    DataValueField="id" 
                    DataTagFields="borrower, id" 
                    EventClickHandling="Bubble"
                    EventClickJavaScript="alert(e.text());" 
                    ContextMenuID="DayPilotMenu1" 
                    OnEventMenuClick="DayPilotCalendar1_EventMenuClick"
                    ClientObjectName="dpm" 
                    EventMoveHandling="CallBack"
                    OnEventMove="DayPilotMonth1_EventMove"
                    Width="100%" 
                    EventResizeHandling="CallBack" 
                    OnEventResize="DayPilotMonth1_EventResize"
                    OnTimeRangeSelected="DayPilotMonth1_TimeRangeSelected"
                    TimeRangeSelectedHandling="JavaScript" 
                    OnBeforeEventRender="DayPilotMonth1_BeforeEventRender" 
                    BubbleID="DayPilotBubble1" 
                    ShowToolTip="false" 
                    EventStartTime="false" 
                    EventEndTime="false" 
                    OnCommand="DayPilotMonth1_Command" 
                    
                    OnBeforeCellRender="DayPilotMonth1_BeforeCellRender" 
                    OnEventClick="DayPilotMonth1_EventClick" 
                    ContextMenuSelectionID="DayPilotMenuSelection"
                    OnBeforeHeaderRender="DayPilotMonth1_BeforeHeaderRender"
                    OnTimeRangeMenuClick="DayPilotMonth1_TimeRangeMenuClick"
                    
                    TimeRangeDoubleClickHandling="JavaScript"
                    TimeRangeDoubleClickJavaScript="alert('doubleclick:' + start)"
                  
                    >
                </DayPilot:DayPilotMonth>
                &nbsp;
                <br/>
            </td>
        </tr>
    </table>

    <DayPilot:DayPilotMenu ID="DayPilotMenu1" runat="server" >
        <DayPilot:MenuItem Text="Open" Action="JavaScript" JavaScript="alert('Opening event (id ' + e.value() + ')');">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Send" Action="JavaScript" JavaScript="alert('Sending event (id ' + e.value() + ')');">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="-" Action="NavigateUrl"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Delete (CallBack)" Action="Callback" Command="Delete"></DayPilot:MenuItem>
        <DayPilot:MenuItem Action="PostBack" Command="Delete" Text="Delete (PostBack)" />
        <DayPilot:MenuItem Action="NavigateUrl" NavigateUrl="javascript:alert('Going somewhere else (id {0})');"
            Text="NavigateUrl test" />
    </DayPilot:DayPilotMenu>

    <DayPilot:DayPilotMenu ID="DayPilotMenu2" runat="server" ClientObjectName="menu2"  >
        <DayPilot:MenuItem Text="Open" Action="JavaScript" JavaScript="alert('Opening event (id ' + e.value() + ')');">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Send" Action="JavaScript" JavaScript="alert('Sending event (id ' + e.value() + ')');">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="-" Action="NavigateUrl"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Delete (CallBack)" Action="Callback" Command="Delete"></DayPilot:MenuItem>
        <DayPilot:MenuItem Action="PostBack" Command="Delete" Text="Delete (PostBack)" />
        <DayPilot:MenuItem Action="NavigateUrl" NavigateUrl="javascript:alert('Going somewhere else (id {0})');"
            Text="NavigateUrl test" />
    </DayPilot:DayPilotMenu>
    
    
    <DayPilot:DayPilotMenu ID="DayPilotMenuSelection" runat="server" ClientObjectName="dpms">
        <DayPilot:MenuItem Action="JavaScript" JavaScript="dpm.timeRangeSelectedCallBack(e.startdate, e.enddate);"
            Text="Create new event (JavaScript)" />
        <DayPilot:MenuItem Action="PostBack" Command="Insert" Text="Create new event (PostBack)" />
        <DayPilot:MenuItem Action="CallBack" Command="Insert" Text="Create new event (CallBack)" />
        <DayPilot:MenuItem Text="-" ></DayPilot:MenuItem>
        <DayPilot:MenuItem Action="JavaScript" JavaScript="alert('Start: ' + e.startdate.toString() + '\nEnd: ' + e.enddate.toString())"
            Text="Show selection details" />
    </DayPilot:DayPilotMenu>
    
        <DayPilot:DayPilotBubble 
        ID="DayPilotBubble1" 
        runat="server" 
        OnRenderContent="DayPilotBubble1_RenderContent" 
        ClientObjectName="bubble"

        Position="EventTop"
        
        

        >
    </DayPilot:DayPilotBubble>--%>

</asp:Content>

