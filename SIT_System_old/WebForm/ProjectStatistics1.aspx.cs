using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebForm_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            GvQuery();
        }
    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    private void GvQuery()
    {
        
        ProjectQuery();

    }

    private void ProjectQuery()
    {
        DataTable dt, dt1;

        string strDate = DateTime.Now.ToString("yyyy/MM/dd");

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Open");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Open";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Close");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Close";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Hold");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Hold";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Delay");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Delay";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Total");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Total";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("ID");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "ID";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        //dt = clsData.UploadProjectDateRange2(strStart1, strEnd1, ddlProject.Text);
        //intCount = dt.Rows.Count;
        string strID;
        strID = Request.QueryString["ID"];
        dt = clsData.UploadProjectQuery(strID, "Project");

        string strName = dt.Rows[0]["Name"].ToString();
        int intOpen = 0;
        int intClose = 0;
        int intHold = 0;
        int intDelay = 0;
        int intTotal = 0;



        for (int intJ = 0; intJ < 5; intJ++)
        {

            if (intJ == 0)
            {
                dt1 = clsData.UploadProjectStatistics1("1", "Open", strID, strDate);
                intOpen = intOpen + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
            }
            else if (intJ == 1)
            {
                dt1 = clsData.UploadProjectStatistics1("1", "Close", strID, strDate);
                intClose = intClose + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
            }
            else if (intJ == 2)
            {
                dt1 = clsData.UploadProjectStatistics1("1", "Hold", strID, strDate);
                intHold = intHold + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
            }
            else if (intJ == 3)
            {
                dt1 = clsData.UploadProjectStatistics1("1", "Delay", strID, strDate);
                intDelay = intDelay + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
            }
            else
            {
                dt1 = clsData.UploadProjectStatistics1("1", "Total", strID, strDate);
                intTotal = intTotal + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
            }


        }

        if (intTotal != 0)
        {
            DataRow dr = dt_new.NewRow();


            dr["Name"] = strName;
            dr["Open"] = intOpen.ToString();
            dr["Close"] = intClose.ToString();
            dr["Hold"] = intHold.ToString();
            dr["Delay"] = intDelay.ToString();
            dr["Total"] = intTotal.ToString();
            dr["ID"] = strID;

            dt_new.Rows.Add(dr);
        }

        intOpen = 0;
        intClose = 0;
        intHold = 0;
        intDelay = 0;
        intTotal = 0;


            
        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        Session["DB_Kind"] = "Project1";
    }
}
