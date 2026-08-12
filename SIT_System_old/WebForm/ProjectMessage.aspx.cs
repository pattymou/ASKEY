using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;


public partial class WebForm_ProjectMessage : System.Web.UI.Page
{
    //public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string strID;
        //DateTime dt1;

        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            //strID = "20150313101506";
            //List<Event> events = new List<Event>();
            DataTable dt = clsData.UploadProjectIDQuery(Request.QueryString["ID"], "");
            lblProjectName.Text = dt.Rows[0]["Name"].ToString();

            getMessage("");

            //dt = clsData.UploadProjectMessageQuery(strID);
            ////lblProjectName.Text = dt.Rows[0]["Name"].ToString();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Event _Event = new Event();
            //    _Event.ID = dr["ID"].ToString();
            //    _Event.ProjectID = dr["Project_ID"].ToString();
            //    _Event.ProjectName = dr["Name"].ToString();
            //    _Event.Kind = dr["Kind"].ToString();
            //    _Event.Message = dr["Message"].ToString();
            //    dt1 = Convert.ToDateTime(dr["MessageTime"].ToString());
            //    _Event.MessageTime = dt1.ToString("yyyy/MM/dd HH:mm");
            //    _Event.MessageUser = dr["MessageUser"].ToString();
            //    events.Add(_Event);
            //}

            

            //rptEmployees.DataSource = events;
            //rptEmployees.DataBind();
        }
    }

    private void getMessage(string strKind)
    {
        DateTime dt1;
        List<Event> events = new List<Event>();

        DataTable dt = clsData.UploadProjectMessageQuery(Request.QueryString["ID"], strKind);
        //lblProjectName.Text = dt.Rows[0]["Name"].ToString();
        foreach (DataRow dr in dt.Rows)
        {
            Event _Event = new Event();
            _Event.ID = dr["ID"].ToString();
            _Event.ProjectID = dr["Project_ID"].ToString();
            _Event.ProjectName = dr["Name"].ToString();
            _Event.Kind = dr["Kind"].ToString();
            _Event.Message = dr["Message"].ToString();
            dt1 = Convert.ToDateTime(dr["MessageTime"].ToString());
            _Event.MessageTime = dt1.ToString("yyyy/MM/dd HH:mm");
            _Event.MessageUser = dr["MessageUser"].ToString();
            events.Add(_Event);
        }



        rptEmployees.DataSource = events;
        rptEmployees.DataBind();
    }

    public class Event
    {
        public string ID { get; set; }

        public string ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string Kind { get; set; }

        public string Message { get; set; }

        public string MessageTime { get; set; }

        public string MessageUser { get; set; }

    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/AddProjectMessage.aspx?ID=" + Request.QueryString["ID"]);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SearchProjectMessage.aspx");
    }

    protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strKind1;

        if (ddlKind.Text == "ALL")
            strKind1 = "";
        else
            strKind1 = ddlKind.Text;
        getMessage(strKind1);
    }
}
