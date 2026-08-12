using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

public partial class WebForm_GanttProject : System.Web.UI.Page
{
    public string csSource;
    //public static string strFun;

    public class Gantt
    {
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string desc { get; set; }
        public List<Bar> values { get; set; }
    }

    public class Bar
    {
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string label { get; set; }
        public string customClass { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            Session["Fun"] = Request.QueryString["Fun"];
            

            loadEmployees(this.ddlEmployees);
            getProject();
        }
    }

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL, "1");
    }
    #endregion

    private static string DateTimeToSecond(DateTime dt)
    {
        dt = dt.AddDays(-1);
        int timeStamp = Convert.ToInt32(dt.AddHours(8).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        return String.Format("/Date({0}000)/", timeStamp);
    }

    protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        getProject();
    }

    private void getProject()
    {
        string strProjectName = null;
        string strProjectName1 = null;
        //int intI = 0;
        int intX, intY;
        DateTime dtStart, dtEnd;
        Item item;
        Bar bar;
        Gantt gantt = new Gantt();
        gantt.items = new List<Item>();
        DataTable dt;
        DataTable dt1 = clsData.getFunction_Name(Session["Fun"].ToString());

        if (ddlEmployees.Text == "ALL")
            dt = clsScheduler.UploadInfoProject("0","", dt1.Rows[0]["Function_Name"].ToString());
        else
            dt = clsScheduler.UploadInfoProject("2", ddlEmployees.Text, dt1.Rows[0]["Function_Name"].ToString());

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {

            //if (intI == 0)
            //{
            intX = 0;
            intY = 0;
            while (intX == 0)
            {
                strProjectName = dt.Rows[intI]["projectname"].ToString();

                //專案一
                item = new Item();
                if (intY == 0)
                    item.name = strProjectName;
                else
                    item.name = "";
                item.desc = dt.Rows[intI]["name"].ToString();
                item.values = new List<Bar>();
                gantt.items.Add(item);

                dtStart = Convert.ToDateTime(dt.Rows[intI]["start_date"].ToString());
                dtEnd = Convert.ToDateTime(dt.Rows[intI]["end_date"].ToString());
                bar = new Bar();
                bar.id = dt.Rows[intI]["projectid"].ToString();
                bar.from = DateTimeToSecond(dtStart);
                bar.to = DateTimeToSecond(dtEnd);
                //bar.label = "Assign : " + dt.Rows[intI]["assign"].ToString() + ", Note : " + dt.Rows[intI]["explain_case"].ToString();
                bar.customClass = "Assign : " + dt.Rows[intI]["assign"].ToString() + ", Note : " + dt.Rows[intI]["explain_case"].ToString();
                item.values.Add(bar);


                intI = intI + 1;

                if (intI < dt.Rows.Count)
                {
                    strProjectName1 = dt.Rows[intI]["projectname"].ToString();

                    intY = 1;
                    if (strProjectName1 != strProjectName)
                    {
                        intI = intI - 1;
                        intX = 1;
                    }
                }
                else
                    intX = 1;


            }

        }


        string json = JsonConvert.SerializeObject(gantt.items);
        //Response.Write(json);
        csSource = json;
    }
}
