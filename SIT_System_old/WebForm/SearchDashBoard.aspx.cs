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

public partial class WebForm_SearchDashBoard : System.Web.UI.Page
{
    public static DataTable dt_new;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            rdoTeam.Checked = true;
            loadTeam(this.ddlTeam);
            loadFunction(this.ddlProject);

            int iYear = System.DateTime.Now.Year;
            int iMonth = System.DateTime.Now.Month;

            txtYearE.Text = iYear.ToString();
            txtYearS.Text = iYear.ToString();
            ddlMonthS.Text = String.Format("{0:00}", iMonth);
            ddlMonthE.Text = String.Format("{0:00}", iMonth);

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

            btnExcel1.Visible = false;
        }
    }

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlTeam(DDL,"1");
    }
    #endregion

    #region loadTeamEmp
    protected void loadTeamEmp(DropDownList DDL,string strTeam)
    {
        clsDropDownList.ddlTeamEmployees(DDL, "1", strTeam);
    }
    #endregion

    #region loadFunction
    protected void loadFunction(DropDownList DDL)
    {
        clsDropDownList.ddlDashBoardFunction(DDL, "1");
    }
    #endregion

    protected void rdoTeam_CheckedChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTeamEmp(this.ddlEmp, ddlTeam.SelectedItem.Text);
    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        string strLocal;
        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";
        GvQuery(strLocal);
    }
    #endregion

    private void GvQuery(string strLocal)
    {
        string strStart, strEnd;

        if ((Convert.ToInt32(ddlMonthE.Text) < 9))
        {
            if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
            {
                if (ddlMonthE.Text == "02")
                {
                    if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                }
                else
                {
                    if (ddlMonthE.Text == "08")
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                }
            }
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        else
        {
            if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        strStart = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";


        if (rdoTeam.Checked == true)
            PersonalQuery(strStart, strEnd, strLocal);
        else
        {
            if (strLocal == "DA40")
                ProjectQuery(strStart, strEnd, "台北");
            else
                ProjectQuery(strStart, strEnd, "吳江");
        }

        Session["DB_SD"] = strStart;
        Session["DB_ED"] = strEnd;
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strLocal;
        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";
        GvQuery(strLocal);
        btnExcel1.Visible = true;
    }

    private void PersonalQuery(string strStart1,string strEnd1,string strLocal)
    {
        DataTable dt,dt1;
        int intCount = 0 ;

        dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Open");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Open";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Close");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Close";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Hold");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Hold";
        column4.DefaultValue = "0";
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


        dt = null;

        if ((ddlEmp.Text == "ALL") || (ddlTeam.Text == "ALL"))
        {
            dt = clsData.UploadTeamEmp(ddlTeam.Text, strLocal);
            intCount = dt.Rows.Count;
        }
        else
            intCount = 1;
        

        string strName;

        for (int intI = 0; intI < intCount; intI++)
        {
           DataRow dr = dt_new.NewRow();
           for (int intJ = 0; intJ < 5; intJ++)
           {
                

               if (intCount == 1)
                   strName = ddlEmp.Text;
               else
                   strName = dt.Rows[intI]["Name_En"].ToString();

               dr["Name"] = strName;
                if (intJ == 0)
                {
                    dt1 = clsData.UploadDashBoardQuery("1", "Personal", "Open", strName, strStart1, strEnd1);
                    dr["Open"] = dt1.Rows[0]["CountCase"].ToString();
                }
                else if (intJ == 1)
                {
                    dt1 = clsData.UploadDashBoardQuery("1", "Personal", "Close", strName, strStart1, strEnd1);
                    dr["Close"] = dt1.Rows[0]["CountCase"].ToString();
                }
                else if (intJ == 2)
                {
                    dt1 = clsData.UploadDashBoardQuery("1", "Personal", "Hold", strName, strStart1, strEnd1);
                    dr["Hold"] = dt1.Rows[0]["CountCase"].ToString();
                }
                else if (intJ == 3)
                {
                    dt1 = clsData.UploadDashBoardQuery("1", "Personal", "Delay", strName, strStart1, strEnd1);
                    dr["Delay"] = dt1.Rows[0]["CountCase"].ToString();
                }
                else
                {
                    dt1 = clsData.UploadDashBoardQuery("1", "Personal", "Total", strName, strStart1, strEnd1);
                    dr["Total"] = dt1.Rows[0]["CountCase"].ToString();
                }
                

            }
            dt_new.Rows.Add(dr);
        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        Session["DB_Kind"] = "Team";

        

    }

    private void ProjectQuery(string strStart1, string strEnd1, string strLocal)
    {
        DataTable dt, dt1;
        int intCount = 0;

        dt_new = new DataTable("dt_new");

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

        dt = clsData.UploadProjectDateRange2(strStart1, strEnd1, ddlProject.Text, strLocal);
        intCount = dt.Rows.Count;


        string strName = "";
        int intOpen = 0;
        int intClose = 0;
        int intHold = 0;
        int intDelay = 0;
        int intTotal = 0;

        for (int intI = 0; intI < intCount; intI++)
        {


            if ((strName != dt.Rows[intI]["Name"].ToString()) || (intI == intCount - 1) || (intI == 0))
            {
                strName = dt.Rows[intI]["Name"].ToString();

                for (int intJ = 0; intJ < 5; intJ++)
                {

                    if (intJ == 0)
                    {
                        dt1 = clsData.UploadDashBoardProject("1", ddlProject.Text, "Open", strName, strStart1, strEnd1);
                        intOpen = intOpen + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
                    }
                    else if (intJ == 1)
                    {
                        dt1 = clsData.UploadDashBoardProject("1", ddlProject.Text, "Close", strName, strStart1, strEnd1);
                        intClose = intClose + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
                    }
                    else if (intJ == 2)
                    {
                        dt1 = clsData.UploadDashBoardProject("1", ddlProject.Text, "Hold", strName, strStart1, strEnd1);
                        intHold = intHold + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
                    }
                    else if (intJ == 3)
                    {
                        dt1 = clsData.UploadDashBoardProject("1", ddlProject.Text, "Delay", strName, strStart1, strEnd1);
                        intDelay = intDelay + Convert.ToInt32(dt1.Rows[0]["CountCase"].ToString());
                    }
                    else
                    {
                        dt1 = clsData.UploadDashBoardProject("1", ddlProject.Text, "Total", strName, strStart1, strEnd1);
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

                    dt_new.Rows.Add(dr);
                }

                intOpen = 0;
                intClose = 0;
                intHold = 0;
                intDelay = 0;
                intTotal = 0;

                
            }
       


        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        Session["DB_Kind"] = "Project";
    }

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        string strName = "DashBoard " + Session["DB_SD"].ToString() + "-" + Session["DB_ED"].ToString();
        export_excel(strName, 0);
    }

    private void export_excel(string filename, int t_mode)
    {
        //  呼叫方式 export_excel("gridview1", "output",1);
        // export_excel(要匯出的 Gridview 名稱, 匯出的檔名,模式);  // 1=會加入日期時間
        //GridView xgv = (GridView)FindControl(gvwMain);
        string style = "<style> .text { mso-number-format:\\@; } </script> ";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        Response.Clear();
        if (t_mode == 1)  // 加上時間日期
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "_" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".xls");
        else
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        gvwMain.AllowPaging = false;
        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
        
        gvwMain.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
        gvwMain.AllowPaging = true;
        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
        
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }
}
