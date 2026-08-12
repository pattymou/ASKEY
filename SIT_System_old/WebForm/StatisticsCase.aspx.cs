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

public partial class WebForm_StatisticsCase : System.Web.UI.Page
{
    //public static string strFun;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadTeam(this.ddlTeam);
            Session["Fun"] = Request.QueryString["Fun"];
            
        }
    }

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 4, "1");
    }
    #endregion 

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //getTestPlan();
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Chart_Complete.Legends[0].Enabled = false;
        Chart_Complete.Series[0].Points.Clear();
        Chart_Complete.Series[1].Points.Clear();
        Chart_Complete.Series[2].Points.Clear();
        setChart();

        //DataTable dt_new = new DataTable("dt_new");

        //DataColumn column1 = new DataColumn("ID");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "ID";
        //column1.DefaultValue = "0";
        //dt_new.Columns.Add(column1);

        //DataColumn column2 = new DataColumn("TotalCase");
        //column2.DataType = System.Type.GetType("System.String");
        //column2.AllowDBNull = true;
        //column2.Caption = "TotalCase";
        //column2.DefaultValue = "0";
        //dt_new.Columns.Add(column2);

        //DataColumn column3 = new DataColumn("Complete");
        //column3.DataType = System.Type.GetType("System.String");
        //column3.AllowDBNull = true;
        //column3.Caption = "Complete";
        //column3.DefaultValue = "0";
        //dt_new.Columns.Add(column3);

        //DataColumn column4 = new DataColumn("NComplete");
        //column4.DataType = System.Type.GetType("System.String");
        //column4.AllowDBNull = true;
        //column4.Caption = "NComplete";
        //column4.DefaultValue = "0";
        //dt_new.Columns.Add(column4);

        //DataColumn column5 = new DataColumn("Percent");
        //column5.DataType = System.Type.GetType("System.String");
        //column5.AllowDBNull = true;
        //column5.Caption = "Percent";
        //column5.DefaultValue = "0";
        //dt_new.Columns.Add(column5);

        ////DataColumn column6 = new DataColumn("Percent");
        ////column6.DataType = System.Type.GetType("System.String");
        ////column6.AllowDBNull = true;
        ////column6.Caption = "Percent";
        ////column6.DefaultValue = "0";
        ////dt_new.Columns.Add(column6);

        //DataTable dt = clsData.UploadTeamEmployee(ddlTeam.Text);

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataRow dr = dt_new.NewRow();

        //    dr["ID"] = dt.Rows[i]["Assign"].ToString();
        //    dr["TotalCase"] = dt.Rows[i]["total"].ToString();

        //    DataTable dt1 = clsData.UploadStatusCase("close",dt.Rows[i]["Assign"].ToString());
        //    dr["Complete"] = dt1.Rows[0]["case1"].ToString();
        //    dr["NComplete"] = int.Parse(dt.Rows[i]["total"].ToString()) - int.Parse(dt1.Rows[0]["case1"].ToString());
        //    dr["Percent"] = (int)((decimal)int.Parse(dt1.Rows[0]["case1"].ToString()) / int.Parse(dt.Rows[i]["total"].ToString()) * 100) + "%";
        //    dt_new.Rows.Add(dr);
        //}

        //gvwMain.DataSource = dt_new;
        //gvwMain.DataBind();
    }

    private void setChart()
    {
        string strStart,strEnd;
        DataTable dt1 = clsData.getFunction_Name(Session["Fun"].ToString());

        if ((Convert.ToInt32(ddlMonthE.Text) < 9))
        {
            if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
            {
                if (ddlMonthE.Text == "02")
                {
                    if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                }
                else
                {
                    if (ddlMonthE.Text == "08")
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                }
            }
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        else
        {
            if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        strStart = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";

        int intTotal=0;

        DataTable dt_new = new DataTable("dt_new");
        dt_new.Columns.Add("Name");
        dt_new.Columns.Add("Total");

        //if (dt1.Rows[0]["Function_Name"].ToString() != "驗証申請")
        //{
            DataTable dtTeam = clsData.UploadTeam();
            for (int i = 0; i < dtTeam.Rows.Count; i++)
            {
                DataTable dtTotal = clsData.UploadTeamCase(dtTeam.Rows[i]["Name"].ToString(), strStart, strEnd, dt1.Rows[0]["Function_Name"].ToString());
                dt_new.Rows.Add(dtTeam.Rows[i]["Name"].ToString(), dtTotal.Rows[0]["total"].ToString());
                intTotal = intTotal + int.Parse(dtTotal.Rows[0]["total"].ToString());
            }
        //}


        //Chart_Complete.DataSource = dt_new;
        //Chart_Complete.Series["Average"].XValueMember = "Name";
        //Chart_Complete.Series["Average"].YValueMembers = "Total";
        //Chart_Complete.DataBind();

        Chart_Complete.Series[0].Points.DataBind(dt_new.DefaultView, "Name", "Total", "");
        //Chart_Complete.Series[0].Points.DataBindXY(dt_new.DefaultView, "Name", dt_new.DefaultView, "Total");


    }

    protected void Chart_Complete_Click(object sender, ImageMapEventArgs e)
    {
        //for (int i = 0; i < Chart_Complete.Series[0].Points.Count; i++)
        //{
        //    if (Chart_Complete.Series[0].Points[i].AxisLabel == e.PostBackValue)
        //    {
        //        Chart_Complete.Series[0].Points[i]["Exploded"] = "true";
        //        break;
        //    }
        //}
        Chart_Complete.Series[1].Points.Clear();
        Chart_Complete.Series[2].Points.Clear();
        string[] strValue = e.PostBackValue.Split(',');
        Chart_Complete.Series[0].Points[int.Parse(strValue[0])]["Exploded"] = "true";

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("TotalCase");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "TotalCase";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Complete");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Complete";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("NComplete");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "NComplete";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Percent");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Percent";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);


        DataTable dt = clsData.UploadTeamEmployee(strValue[1]);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt_new.NewRow();

            dr["ID"] = dt.Rows[i]["Assign"].ToString();
            dr["TotalCase"] = dt.Rows[i]["total"].ToString();

            DataTable dt1 = clsData.UploadStatusCase("close", dt.Rows[i]["Assign"].ToString());
            dr["Complete"] = dt1.Rows[0]["case1"].ToString();
            dr["NComplete"] = int.Parse(dt.Rows[i]["total"].ToString()) - int.Parse(dt1.Rows[0]["case1"].ToString());
            dr["Percent"] = (int)((decimal)int.Parse(dt1.Rows[0]["case1"].ToString()) / int.Parse(dt.Rows[i]["total"].ToString()) * 100) + "%";
            dt_new.Rows.Add(dr);

            Chart_Complete.Series[1].Points.AddXY(dr["ID"], dr["NComplete"]);
            Chart_Complete.Series[2].Points.AddXY(dr["ID"], dr["Complete"]);
            Chart_Complete.Series[1].Points[i].ToolTip = "完成率：" + dr["Percent"].ToString();
            Chart_Complete.Series[2].Points[i].ToolTip = "完成率：" + dr["Percent"].ToString();
        }
        //Chart_Complete.DataSource = dt_new;
        //Chart_Complete.DataBind();
        //Chart_Complete.Series[1].XValueMember = "ID";
        //Chart_Complete.Series[1].YValueMembers = "NComplete";
        //Chart_Complete.Series[2].XValueMember = "ID";
        //Chart_Complete.Series[2].YValueMembers = "Complete";

        Chart_Complete.ChartAreas["ChartArea2"].AxisY.Title = "案件總量";

        


        Chart_Complete.Legends[0].Enabled = true;

    }
}
