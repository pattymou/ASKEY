<%@ WebHandler Language="C#" Class="Calendar" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;


using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Linq;
using System.Data;

using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class Calendar : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private static string connStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
    
    public void ProcessRequest(HttpContext context)
    {
        DateTime dt1;
        DataTable dt2;

        //context.Response.ContentType = "text/plain";
        string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = context.Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = context.Server.UrlDecode(cookie_ApparatusID.Value);

        strApparatusID_Cookie = context.Session["Calendar"].ToString();

        string strKind = "0";
        List<Event> events;
        events = new List<Event>();
        MicroSovaComponent.Database.SqlServerObject sqlConn = new MicroSovaComponent.Database.SqlServerObject(connStr);
        StringBuilder strSQL = new StringBuilder();

        if (strApparatusID_Cookie.IndexOf("A", 0) > -1)
        {
            dt2 = clsData.UploadApparatusQuery(strApparatusID_Cookie, "1", "");
            if (dt2.Rows[0]["ReservationStatus"].ToString() == "不可借用")
            {
                strKind = "1";
                strSQL.Append("select id,borrower,Department,Ext,StartDate,EndDate,ContinuousDate,Mission,GName,Status,Period from Reservation where ");
                strSQL.AppendFormat("Apparatus_ID ='{0}'", strApparatusID_Cookie);
            }
            else
            {
                strSQL.Append("select id,borrower,Department,Ext,StartDate,EndDate,ContinuousDate,Mission,GName,Status,Period from Reservation where ");
                strSQL.AppendFormat("Apparatus_ID ='{0}' and (status = 'Y' or Status = '')", strApparatusID_Cookie);
            }
        }
        else
        {
            strSQL.Append("select id,borrower,Department,Ext,StartDate,EndDate,ContinuousDate,Mission,GName,Status,Period from Reservation where ");
            strSQL.AppendFormat("Apparatus_ID ='{0}' and (status = 'Y' or Status = '')", strApparatusID_Cookie);
        }
        DataTable dt = sqlConn.getDataTable(strSQL.ToString(), null, CommandType.Text);
        string strStartTime = "", striEndTime = "";

        if (strKind == "1")
        {
            foreach (DataRow dr in dt.Rows)
            {
                Event _Event = new Event();
                _Event.id = Convert.ToInt32(dr["ID"].ToString());
                 dt1 = Convert.ToDateTime(dr["startdate"].ToString());
                 strStartTime = dt1.ToString("hh:mm");
                _Event.start = dt1.ToString("yyyy/MM/dd");
                if ((dr["ContinuousDate"].ToString() != "") && (dr["ContinuousDate"].ToString() != null) && (dr["ContinuousDate"].ToString() != "1900/1/1 上午 12:00:00"))
                    dt1 = Convert.ToDateTime(dr["ContinuousDate"].ToString());
                else
                    dt1 = Convert.ToDateTime(dr["enddate"].ToString());
                striEndTime = dt1.ToString("hh:mm");
                _Event.end = dt1.ToString("yyyy/MM/dd");

                _Event.title = strStartTime + "~" + striEndTime;
                //_Event.title = dr["Department"].ToString() + "," + dr["borrower"].ToString() + "," + dr["Ext"].ToString();
                //_Event.title = "123";

                _Event.mission = dr["mission"].ToString();
                _Event.gname = dr["gname"].ToString();
                _Event.kind = strKind;
                _Event.name = dr["borrower"].ToString();
                _Event.department = dr["Department"].ToString();

                //if (dr["Period"].ToString() == "D")
                //    _Event.period = "白天";
                //else if (dr["Period"].ToString() == "N")
                //    _Event.period = "晚上";
                //else
                //    _Event.period = "整天";

                _Event.color = "Blue";
                //if (dr["Status"].ToString() == "")
                //    _Event.color = "Red";
                //else
                //{
                //    if (dr["Period"].ToString() == "D")
                //        _Event.color = "Blue";
                //    else if (dr["Period"].ToString() == "N")
                //        _Event.color = "Purple";
                //    else
                //        _Event.color = "Grey";
                //    //_Event.color = "Blue";
                //}
                //_Event.department = dr["Department"].ToString();
                //_Event.ext = dr["Ext"].ToString();
                events.Add(_Event);
            }
        }
        else
        {
            foreach (DataRow dr in dt.Rows)
            {
                Event _Event = new Event();
                _Event.id = Convert.ToInt32(dr["ID"].ToString());
                _Event.title = dr["Department"].ToString() + "," + dr["borrower"].ToString() + "," + dr["Ext"].ToString();
                dt1 = Convert.ToDateTime(dr["startdate"].ToString());
                _Event.start = dt1.ToString("yyyy/MM/dd");
                if ((dr["ContinuousDate"].ToString() != "") && (dr["ContinuousDate"].ToString() != null) && (dr["ContinuousDate"].ToString() != "1900/1/1 上午 12:00:00"))
                    dt1 = Convert.ToDateTime(dr["ContinuousDate"].ToString());
                else
                    dt1 = Convert.ToDateTime(dr["enddate"].ToString());
                //dt1 = Convert.ToDateTime(dr["enddate"].ToString());
                _Event.end = dt1.ToString("yyyy/MM/dd");
                _Event.mission = dr["mission"].ToString();
                _Event.gname = dr["gname"].ToString();

                if (dr["Period"].ToString() == "D")
                    _Event.period = "白天";
                else if (dr["Period"].ToString() == "N")
                    _Event.period = "晚上";
                else
                    _Event.period = "整天";


                if (dr["Status"].ToString() == "")
                    _Event.color = "Red";
                else
                {
                    if (dr["Period"].ToString() == "D")
                        _Event.color = "Blue";
                    else if (dr["Period"].ToString() == "N")
                        _Event.color = "Purple";
                    else
                        _Event.color = "Grey";
                    //_Event.color = "Blue";
                }
                //_Event.department = dr["Department"].ToString();
                //_Event.ext = dr["Ext"].ToString();
                events.Add(_Event);
            }
        }

        StringBuilder strSQL1 = new StringBuilder();

        strSQL1.Append("select id,borrower,Department,Ext,ContinuousDate,DATEADD(day,1,EndDate) as EndDate,Mission,GName,Status,Period from Reservation where ");
        strSQL1.AppendFormat(" Apparatus_ID ='{0}' and (Custodian_Check = '' or Admin_Check = '') and YEAR(continuousdate) != '1900'", strApparatusID_Cookie);
        dt = sqlConn.getDataTable(strSQL1.ToString(), null, CommandType.Text);


        foreach (DataRow dr in dt.Rows)
        {
            Event _Event = new Event();
            _Event.id = Convert.ToInt32(dr["ID"].ToString());
            _Event.title = dr["Department"].ToString() + "," + dr["borrower"].ToString() + "," + dr["Ext"].ToString();
            dt1 = Convert.ToDateTime(dr["EndDate"].ToString());
            _Event.start = dt1.ToString("yyyy/MM/dd");
            dt1 = Convert.ToDateTime(dr["ContinuousDate"].ToString());
            _Event.end = dt1.ToString("yyyy/MM/dd");
            _Event.mission = dr["mission"].ToString();
            _Event.gname = dr["gname"].ToString();

            if (dr["Period"].ToString() == "D")
                _Event.period = "白天";
            else if (dr["Period"].ToString() == "N")
                _Event.period = "晚上";
            else
                _Event.period = "整天";


            //if (dr["Status"].ToString() == "")
                _Event.color = "Red";
            //else
            //{
            //    if (dr["Period"].ToString() == "D")
            //        _Event.color = "Blue";
            //    else if (dr["Period"].ToString() == "N")
            //        _Event.color = "Purple";
            //    else
            //        _Event.color = "Grey";
            //    //_Event.color = "Blue";
            //}
            //_Event.department = dr["Department"].ToString();
            //_Event.ext = dr["Ext"].ToString();
            events.Add(_Event);
        }  
        //List<Event> events;
        //events = new List<Event>();
        //for (int i = 0; i < 1; i++)
        //{
        //    Event _Event = new Event();
        //    _Event.id = i;
        //    _Event.title = "Event_" + i.ToString();
        //    _Event.start = "2015/01/23 10:00";
        //    _Event.end = "2015/01/23 11:00";
        //    //_Event.StartDate = "Thursday, 2015/1/24 下午 06:18:19";
        //    //_Event.EndDate = "Thursday, 2015/1/24 下午 06:18:19";
        //    events.Add(_Event);
        //}        

        System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
         new System.Web.Script.Serialization.JavaScriptSerializer();
        //string sJSON = oSerializer.Serialize(tasksList);
        string sJSON = oSerializer.Serialize(events);
        context.Response.Write(sJSON);
    }
    
    private long ToUnixTimespan(DateTime date)
    {
        TimeSpan tspan = date.ToUniversalTime().Subtract(
            new DateTime(1970, 1, 1, 0, 0, 0));

        return (long)Math.Truncate(tspan.TotalSeconds);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public class CalendarDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string mission { get; set; }
        public string gname { get; set; }
        public string color { get; set; }
        public string period { get; set; }
        public string department { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
    }

    public class Event
    {
        //public int? EventID { get; set; }
        //public string EventName { get; set; }
        //public string StartDate { get; set; }
        //public string EndDate { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public string mission { get; set; }
        public string gname { get; set; }
        public string color { get; set; }
        public string period { get; set; }
        public string department { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
        //public string ext { get; set; }
    }
}