using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
//using DayPilot.Utils;
//using DayPilot.Web.Ui;
//using DayPilot.Web.Ui.Enums;
//using DayPilot.Web.Ui.Events;
//using DayPilot.Web.Ui.Events.Scheduler;

using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Scheduler;
using System.Text;

public partial class WebForm_Gantt_Scheduler : System.Web.UI.Page
{
    //private DataTable tasks;

    //protected void Page_Load(object sender, EventArgs e)
    //{
        //DayPilotScheduler1.TimeHeaders.Clear();
        //DayPilotScheduler1.TimeHeaders.Add(new TimeHeader(GroupByEnum.Month));
        //DayPilotScheduler1.TimeHeaders.Add(new TimeHeader(GroupByEnum.Week));

        //LoadResources();
        //UpdateScheduler();

        //DayPilotScheduler1.SetScrollX(new clsScheduler().GetStart() ?? DateTime.Today);

    //}

    //private void UpdateScheduler()
    //{
    //    tasks = new clsScheduler().GetTasks();
    //    var start = (new clsScheduler().GetStart() ?? DateTime.Today).Date;
    //    start = new DateTime(start.Year, start.Month, 1).AddMonths(-1);
    //    var end = new clsScheduler().GetEnd() ?? DateTime.Today.AddDays(1);
    //    end = new DateTime(end.Year, end.Month, 1).AddMonths(2);

    //    DayPilotScheduler1.StartDate = start;
    //    DayPilotScheduler1.Days = (int)Math.Ceiling((end - start).TotalDays);
    //    DayPilotScheduler1.DataSource = new clsScheduler().GetTasks();
    //    DayPilotScheduler1.DataBind();
    //    DayPilotScheduler1.Update();
    //}

    //private void LoadResources()
    //{
    //    DataTable locations = new clsScheduler().GetTasks();
    //    DayPilotScheduler1.Resources.Clear();
    //    DayPilotScheduler1.Resources.Add("New Task", "NEW");
    //    foreach (DataRow dr in locations.Rows)
    //    {
    //        DayPilotScheduler1.Resources.Add((string)dr["TaskName"], Convert.ToString(dr["TaskId"]));
    //    }
    //}

    //protected void DayPilotCalendar1_EventMenuClick(object sender, EventMenuClickEventArgs e)
    //{
    //    switch (e.Command)
    //    {
    //        case "Delete":
    //            int id = e.Recurrent ? Convert.ToInt32(e.RecurrentMasterId) : Convert.ToInt32(e.Value);
    //            new clsScheduler().DeleteTask(id);
    //            UpdateScheduler();
    //            LoadResources();
    //            DayPilotScheduler1.Update(CallBackUpdateType.Full);
    //            break;


    //    }
    //}

    //protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
    //{
    //    int id = e.Recurrent ? Convert.ToInt32(e.RecurrentMasterId) : Convert.ToInt32(e.Value);
    //    new clsScheduler().MoveTask(id, e.NewStart, e.NewEnd);
    //    LoadResources(); // update order
    //    UpdateScheduler();
    //}

    //protected void DayPilotCalendar1_Command(object sender, CommandEventArgs e)
    //{
    //    switch (e.Command)
    //    {
    //        case "refresh":
    //            UpdateScheduler();
    //            LoadResources();
    //            DayPilotScheduler1.Update(CallBackUpdateType.Full);
    //            break;

    //    }
    //}

    //protected void DayPilotCalendar1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    //{
    //    e.InnerHTML = Server.HtmlEncode(e.Text);
    //    e.EventMoveVerticalEnabled = false;
    //    e.StaticBubbleHTML = String.Format("<b>{0}</b><br/>Start: {1}<br/>End: {2}", e.Text, e.Start, e.End);

    //}

    //protected void DayPilotCalendar1_EventMove(object sender, EventMoveEventArgs e)
    //{
    //    int id = e.Recurrent ? Convert.ToInt32(e.RecurrentMasterId) : Convert.ToInt32(e.Value);
    //    new clsScheduler().MoveTask(id, e.NewStart, e.NewEnd);
    //    LoadResources(); // update order
    //    UpdateScheduler();
    //}
    //protected void DayPilotScheduler1_BeforeCellRender(object sender, BeforeCellRenderEventArgs e)
    //{
    //    if (e.ResourceId == "NEW")
    //    {
    //        if (e.IsBusiness)
    //        {
    //            e.BackgroundColor = "#ffffff";
    //        }
    //        else
    //        {
    //            e.BackgroundColor = "#ffffe7";
    //        }
    //    }
    //    else
    //    {
    //        if (e.IsBusiness)
    //        {
    //            e.BackgroundColor = "#f8f8f8";
    //        }
    //        else
    //        {
    //            e.BackgroundColor = "#f8f8e7";
    //        }
    //    }
    //}

    //protected void DayPilotScheduler1_BeforeTimeHeaderRender(object sender, BeforeTimeHeaderRenderEventArgs e)
    //{
    //    if (e.Level == 1)
    //    {
    //        e.InnerHTML = String.Format("Week {0}", Week.WeekNrISO8601(e.Start));
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DayPilotScheduler1.Days = DateTime.DaysInMonth(2013, 2);
            DayPilotScheduler1.StartDate = new DateTime(2015, 04, 01);
            LoadEvents();
        }

        //string cols = new DataManager().GetUserConfig(User.Identity.Name, "project.cols");
        //if (cols != null)
        //{
        //    DayPilotScheduler1.RowHeaderColumnWidths = cols;
        //}

    }

    private string TaskLink(string name, string id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div style=' padding: 0px 2px 0px 2px'>");
        sb.Append("<div class='task_status ");
        sb.Append("' data-taskid='" + id + "'></div>");

        sb.Append("<a title='");
        sb.Append(name);
        sb.Append("' ");
        sb.Append("href='javascript:edit(\"");
        sb.Append(id);
        sb.Append("\")'>");
        sb.Append(name);
        sb.Append("</a>");
        sb.Append("</div>");

        return sb.ToString();
    }

    protected void UpdatePanelScheduler_Load(object sender, EventArgs e)
    {
    }

    protected void DayPilotScheduler1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
        /*
        Task t = (Task)e.DataItem.Source;
        e.DurationBarColor = Helper.StatusToColor(t["AssignmentStatus"]);
         * */
    }


    protected void DayPilotScheduler1_HeaderColumnWidthChanged(object sender, HeaderColumnWidthChangedEventArgs e)
    {
        //new DataManager().SetUserConfig(User.Identity.Name, "project.cols", DayPilotScheduler1.RowHeaderColumnWidths);
        //LoadEvents();
    }

    protected void RadioButtonListZoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEvents();
    }


    private void LoadEvents()
    {
        DayPilotScheduler1.DataStartField = "Start_Date";
        DayPilotScheduler1.DataEndField = "End_Date";
        DayPilotScheduler1.DataTextField = "Note";
        DayPilotScheduler1.DataValueField = "ID";

        DayPilotScheduler1.DataSource = clsScheduler.UploadInfoProject("","","");
        DataBind();
    }

    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        LoadEvents();
    }

    protected void DayPilotScheduler1_BeforeResHeaderRender(object sender, BeforeHeaderRenderEventArgs e)
    {
        DataItemWrapper task = e.DataItem;

        string name = (string)task["Name"];
        string id = (string)(task["ID"]);

        //TimeSpan duration = TimeSpan.FromDays(Convert.ToInt32(task["AssignmentDuration"]));
        //int duration = Convert.ToInt32(task["AssignmentDuration"]);
        int duration = 6;

        e.InnerHTML = TaskLink(name, id);
        e.Columns[0].InnerHTML = "<div style='text-align:right; padding: 0px 6px 0px 2px;'>" + duration + " days</div>";

    }
}
