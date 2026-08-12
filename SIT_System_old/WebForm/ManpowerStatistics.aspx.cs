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
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using System.Web.Services;
using System.Data.SqlClient;
using System.Reflection;

using System.Diagnostics;
using System.Text;

public partial class WebForm_ManpowerStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            //rdoTeam.Checked = true;
            rdoMonth.Checked = true;
            loadTeam(this.ddlTeam);
            loadTeam(this.ddlTeam1);
            loadTeamEmp(this.ddlEmp1, ddlTeam1.SelectedItem.Text, "0");
            //loadFunction(this.ddlProject);

            int iYear = System.DateTime.Now.Year;
            int iMonth = System.DateTime.Now.Month;

            txtYearE.Text = iYear.ToString();
            txtYearS.Text = iYear.ToString();
            txtYearA.Text = iYear.ToString();
            txtYearM1.Text = iYear.ToString();
            ddlMonthS.Text = String.Format("{0:00}", iMonth);
            ddlMonthE.Text = String.Format("{0:00}", iMonth);
            ddlMonthA.Text = String.Format("{0:00}", iMonth);

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

            rdoReportM1.Checked = true;
        }
    }

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlTeam(DDL, "1");
    }
    #endregion

    #region loadTeamEmp
    protected void loadTeamEmp(DropDownList DDL, string strTeam,string strKind)
    {
        clsDropDownList.ddlTeamEmployees(DDL, strKind, strTeam);
    }
    #endregion

    protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTeamEmp(this.ddlEmp, ddlTeam.SelectedItem.Text,"1");
    }

    protected void ddlTeam1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTeamEmp(this.ddlEmp1, ddlTeam1.SelectedItem.Text,"0");
    }

    protected void butOK1_Click(object sender, EventArgs e)
    {
        
    }

    

    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        //gvwMain.Columns.Clear();
        gvwMain.DataSource = null;
        gvwMain.DataBind();


        if (rdoReportM1.Checked == true)
        {
            GvQuery_Month1();
            BindLineChart();
            BindColumnChart();
        }
        else
        {
            if (rdoMonth.Checked == true)
                GvQuery_Month2();
            else
                GvQuery_Week();
        }

        //if (rdoMonth.Checked == true)
        //{
        //    if (rdoReportM2.Checked == true)
        //        GvQuery_Month2();
        //    else
        //        GvQuery_Month1();
        //}
        //if (rdoWeek.Checked == true)
        //    GvQuery_Week();
    }

    private void GvQuery_Month1()
    {
        string strStart, strEnd;

        gvwMain.Visible = false;
        gvwMain1.Visible = true;
        for (int intW = 0; intW <= 13; intW++)
        {
            gvwMain.Columns[intW].Visible = true;
        }

        strStart = txtYearM1.Text + "/01/01";
        strEnd = txtYearM1.Text + "/12/31";
        

        //if (getMonths(strStart, strEnd) > 12)
        //    clsMsg.AlertMessage("最多可選擇12個月！", this.Page);
        //else
        //{

            //string strWeek_S = GetWeekOfYear(Convert.ToDateTime(strStart)).ToString();
            //string strWeek_E = GetWeekOfYear(Convert.ToDateTime(strEnd)).ToString();

            //int intWeek_S = Convert.ToInt32(strWeek_S);
            //int intWeek_E = Convert.ToInt32(strWeek_E);

            DataTable dt_new = new DataTable("dt_new");

            DataColumn column1 = new DataColumn("Name");
            column1.DataType = System.Type.GetType("System.String");
            column1.AllowDBNull = true;
            column1.Caption = "Name";
            column1.DefaultValue = "0";
            dt_new.Columns.Add(column1);

            DataColumn column2 = new DataColumn("Project");
            column2.DataType = System.Type.GetType("System.String");
            column2.AllowDBNull = true;
            column2.Caption = "Project";
            column2.DefaultValue = "0";
            dt_new.Columns.Add(column2);

            DataColumn column3 = new DataColumn("C1");
            column3.DataType = System.Type.GetType("System.Double");
            column3.AllowDBNull = true;
            column3.Caption = "C1";
            column3.DefaultValue = "0";
            dt_new.Columns.Add(column3);

            DataColumn column4 = new DataColumn("C2");
            column4.DataType = System.Type.GetType("System.Double");
            column4.AllowDBNull = true;
            column4.Caption = "C2";
            column4.DefaultValue = "0";
            dt_new.Columns.Add(column4);

            DataColumn column5 = new DataColumn("C3");
            column5.DataType = System.Type.GetType("System.Double");
            column5.AllowDBNull = true;
            column5.Caption = "C3";
            column5.DefaultValue = "0";
            dt_new.Columns.Add(column5);

            DataColumn column6 = new DataColumn("C4");
            column6.DataType = System.Type.GetType("System.Double");
            column6.AllowDBNull = true;
            column6.Caption = "C4";
            column6.DefaultValue = "0";
            dt_new.Columns.Add(column6);

            DataColumn column7 = new DataColumn("C5");
            column7.DataType = System.Type.GetType("System.Double");
            column7.AllowDBNull = true;
            column7.Caption = "C5";
            column7.DefaultValue = "0";
            dt_new.Columns.Add(column7);

            DataColumn column8 = new DataColumn("C6");
            column8.DataType = System.Type.GetType("System.Double");
            column8.AllowDBNull = true;
            column8.Caption = "C6";
            column8.DefaultValue = "0";
            dt_new.Columns.Add(column8);

            DataColumn column9 = new DataColumn("C7");
            column9.DataType = System.Type.GetType("System.Double");
            column9.AllowDBNull = true;
            column9.Caption = "C7";
            column9.DefaultValue = "0";
            dt_new.Columns.Add(column9);

            DataColumn column10 = new DataColumn("C8");
            column10.DataType = System.Type.GetType("System.Double");
            column10.AllowDBNull = true;
            column10.Caption = "C8";
            column10.DefaultValue = "0";
            dt_new.Columns.Add(column10);

            DataColumn column11 = new DataColumn("C9");
            column11.DataType = System.Type.GetType("System.Double");
            column11.AllowDBNull = true;
            column11.Caption = "C9";
            column11.DefaultValue = "0";
            dt_new.Columns.Add(column11);

            DataColumn column12 = new DataColumn("C10");
            column12.DataType = System.Type.GetType("System.Double");
            column12.AllowDBNull = true;
            column12.Caption = "C10";
            column12.DefaultValue = "0";
            dt_new.Columns.Add(column12);

            DataColumn column13 = new DataColumn("C11");
            column13.DataType = System.Type.GetType("System.Double");
            column13.AllowDBNull = true;
            column13.Caption = "C11";
            column13.DefaultValue = "0";
            dt_new.Columns.Add(column13);

            DataColumn column14 = new DataColumn("C12");
            column14.DataType = System.Type.GetType("System.Double");
            column14.AllowDBNull = true;
            column14.Caption = "C12";
            column14.DefaultValue = "0";
            dt_new.Columns.Add(column14);

            DataColumn column15 = new DataColumn("Total");
            column15.DataType = System.Type.GetType("System.Double");
            column15.AllowDBNull = true;
            column15.Caption = "Total";
            column15.DefaultValue = "0";
            dt_new.Columns.Add(column15);

            //DataColumn column16 = new DataColumn("Metric");
            //column16.DataType = System.Type.GetType("System.Double");
            //column16.AllowDBNull = true;
            //column16.Caption = "Metric";
            //column16.DefaultValue = "0";
            //dt_new.Columns.Add(column16);

            int intMonth = getMonths(strStart, strEnd);
            int intMonth1, intMonth2;
            intMonth1 = Convert.ToInt32(ddlMonthS.Text);
            intMonth2 = Convert.ToInt32(ddlMonthE.Text);
            string strYear = txtYearM1.Text;
            string strDateRage, strDateRage1;
            strDateRage = "";
            for (int intI = 1; intI <= 12; intI++)
            {
                if (intMonth1 >= 13)
                {
                    intMonth1 = 1;
                    strYear = txtYearE.Text;
                }
                strDateRage1 = strYear + "/" + intI.ToString();
                strDateRage = strDateRage + "[" + strDateRage1 + "]";

                //intMonth1++;


                if (intI != 12)
                    strDateRage = strDateRage + ",";
            }
            
            Session["DateRage"] = strDateRage;
            string strLocal;
            if (rdoLocal.Checked == true)
                strLocal = "DA40";
            else
                strLocal = "DA40-WJ";

            string strName;
            //int intCount;
            //DataTable dtEmp = null;
            //if ((ddlEmp.Text == "ALL") || (ddlTeam.Text == "ALL"))
            //{
                //dtEmp = clsData.UploadTeamEmp(ddlTeam.Text, strLocal);
                //intCount = dtEmp.Rows.Count;
            //}
            //else
            //    intCount = 1;

            DataRow dr;
            DataTable dtProject;
            string strEvent;
            //for (int intI = 0; intI < intCount; intI++)
            //{
                //if (intCount == 1)
                    strName = ddlEmp1.Text;
                //else
                //    strName = dtEmp.Rows[intI]["Name_En"].ToString();
            DataTable dt;
            for (int intI = 0; intI < 2; intI++)
            {
                if (intI == 0)
                {
                    dt = clsData.UploadManpowerReport1_Month_O(strName, strStart, strEnd, strDateRage);
                }
                else
                {
                    dt = clsData.UploadManpowerReport1_Month(strName, strStart, strEnd, strDateRage);
                }

                for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
                {

                    //if ((dt.Rows[intJ]["Project"].ToString() == "Day off") || (dt.Rows[intJ]["Project"].ToString() == "Meeting") || (dt.Rows[intJ]["Project"].ToString() == "Other"))
                    //    strEvent = dt.Rows[intJ]["Project"].ToString();
                    //else
                    //{
                    //    dtProject = clsData.UploadProjectQuery(dt.Rows[intJ]["Project"].ToString(), "Project");
                    //    if (dtProject.Rows.Count > 0)
                    //        strEvent = dtProject.Rows[0]["Name"].ToString();
                    //    else
                    //        strEvent = "";
                    //}
                    if (intI == 0)
                    {
                        strEvent = dt.Rows[intJ]["Project"].ToString();
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                            strEvent = dt.Rows[intJ]["Kind"].ToString();
                        else
                            strEvent = "";
                    }

                    dr = dt_new.NewRow();
                    dr["Name"] = strName;
                    dr["Project"] = strEvent;
                    dr["C1"] = 0;
                    dr["C2"] = 0;
                    dr["C3"] = 0;
                    dr["C4"] = 0;
                    dr["C5"] = 0;
                    dr["C6"] = 0;
                    dr["C7"] = 0;
                    dr["C8"] = 0;
                    dr["C9"] = 0;
                    dr["C10"] = 0;
                    dr["C11"] = 0;
                    dr["C12"] = 0;
                    dr["Total"] = 0;
                    //dr["Metric"] = 0;

                    int intW1 = 1;
                    double dTotal = 0;
                    double dTotal1 = 0;
                    double dMetric = 0;
                    
                    intMonth1 = Convert.ToInt32(ddlMonthS.Text);
                    
                    strYear = txtYearS.Text;
                    for (int intW = 1; intW <= 12; intW++)
                    {
                        //if (intMonth1 >= 13)
                        //{
                        //    intMonth1 = 1;
                        strYear = txtYearM1.Text;
                        //}
                        strDateRage1 = strYear + "/" + intW.ToString();

                        //intMonth1++;


                        //if (intI != intMonth)
                        //    strDateRage = strDateRage + ",";

                        if ((dt.Rows[intJ][strDateRage1].ToString() == null) || (dt.Rows[intJ][strDateRage1].ToString() == ""))
                            dTotal1 = 0;
                        else
                            dTotal1 = Convert.ToDouble(dt.Rows[intJ][strDateRage1].ToString());
                        dTotal = dTotal + dTotal1;

                        dr["C" + intW1.ToString()] = dTotal1;

                        intW1++;
                    }

                    dr["Total"] = dTotal;

                    //string strToday;

                    //strToday = DateTime.Now.ToString("yyyy/MM/dd");
                    //DateTime dtE = Convert.ToDateTime(strEnd);
                    //DateTime dtT = Convert.ToDateTime(strToday);

                    //if (dtE > dtT)
                    //    dMetric = dTotal / (getDays(Convert.ToDateTime(strStart), Convert.ToDateTime(strToday)) * 8);
                    //else
                    //    dMetric = dTotal / (getDays(Convert.ToDateTime(strStart), Convert.ToDateTime(strEnd)) * 8);
                    //dr["Metric"] = String.Format("{0:N2}", dMetric);


                    dt_new.Rows.Add(dr);
                }

            }

            gvwMain1.DataSource = dt_new;
            gvwMain1.DataBind();


            //intMonth1 = Convert.ToInt32(ddlMonthS.Text);
            //strYear = txtYearS.Text;
            //int intHeader = 2;
            //for (int intW = 1; intW <= intMonth; intW++)
            //{

            //    if (intMonth1 >= 13)
            //    {
            //        intMonth1 = 1;
            //        strYear = txtYearE.Text;
            //    }
            //    strDateRage1 = strYear + "/" + intMonth1.ToString();

            //    intMonth1++;

            //    gvwMain.HeaderRow.Cells[intHeader].Text = strDateRage1;
            //    intHeader++;
            //}

            //for (int intW = intHeader; intW <= 13; intW++)
            //{
            //    gvwMain.Columns[intW].Visible = false;
            //}
        //}
    }

    private void GvQuery_Month2()
    {
        string strStart, strEnd;

        gvwMain.Visible = true;
        gvwMain1.Visible = false;
        for (int intW = 0; intW <= 13; intW++)
        {
            gvwMain.Columns[intW].Visible = true;
        }

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

        if (getMonths(strStart, strEnd) > 12)
            clsMsg.AlertMessage("最多可選擇12個月！", this.Page);
        else
        {

            //string strWeek_S = GetWeekOfYear(Convert.ToDateTime(strStart)).ToString();
            //string strWeek_E = GetWeekOfYear(Convert.ToDateTime(strEnd)).ToString();

            //int intWeek_S = Convert.ToInt32(strWeek_S);
            //int intWeek_E = Convert.ToInt32(strWeek_E);

            DataTable dt_new = new DataTable("dt_new");

            DataColumn column1 = new DataColumn("Name");
            column1.DataType = System.Type.GetType("System.String");
            column1.AllowDBNull = true;
            column1.Caption = "Name";
            column1.DefaultValue = "0";
            dt_new.Columns.Add(column1);

            DataColumn column2 = new DataColumn("Project");
            column2.DataType = System.Type.GetType("System.String");
            column2.AllowDBNull = true;
            column2.Caption = "Project";
            column2.DefaultValue = "0";
            dt_new.Columns.Add(column2);

            DataColumn column3 = new DataColumn("C1");
            column3.DataType = System.Type.GetType("System.Double");
            column3.AllowDBNull = true;
            column3.Caption = "C1";
            column3.DefaultValue = "0";
            dt_new.Columns.Add(column3);

            DataColumn column4 = new DataColumn("C2");
            column4.DataType = System.Type.GetType("System.Double");
            column4.AllowDBNull = true;
            column4.Caption = "C2";
            column4.DefaultValue = "0";
            dt_new.Columns.Add(column4);

            DataColumn column5 = new DataColumn("C3");
            column5.DataType = System.Type.GetType("System.Double");
            column5.AllowDBNull = true;
            column5.Caption = "C3";
            column5.DefaultValue = "0";
            dt_new.Columns.Add(column5);

            DataColumn column6 = new DataColumn("C4");
            column6.DataType = System.Type.GetType("System.Double");
            column6.AllowDBNull = true;
            column6.Caption = "C4";
            column6.DefaultValue = "0";
            dt_new.Columns.Add(column6);

            DataColumn column7 = new DataColumn("C5");
            column7.DataType = System.Type.GetType("System.Double");
            column7.AllowDBNull = true;
            column7.Caption = "C5";
            column7.DefaultValue = "0";
            dt_new.Columns.Add(column7);

            DataColumn column8 = new DataColumn("C6");
            column8.DataType = System.Type.GetType("System.Double");
            column8.AllowDBNull = true;
            column8.Caption = "C6";
            column8.DefaultValue = "0";
            dt_new.Columns.Add(column8);

            DataColumn column9 = new DataColumn("C7");
            column9.DataType = System.Type.GetType("System.Double");
            column9.AllowDBNull = true;
            column9.Caption = "C7";
            column9.DefaultValue = "0";
            dt_new.Columns.Add(column9);

            DataColumn column10 = new DataColumn("C8");
            column10.DataType = System.Type.GetType("System.Double");
            column10.AllowDBNull = true;
            column10.Caption = "C8";
            column10.DefaultValue = "0";
            dt_new.Columns.Add(column10);

            DataColumn column11 = new DataColumn("C9");
            column11.DataType = System.Type.GetType("System.Double");
            column11.AllowDBNull = true;
            column11.Caption = "C9";
            column11.DefaultValue = "0";
            dt_new.Columns.Add(column11);

            DataColumn column12 = new DataColumn("C10");
            column12.DataType = System.Type.GetType("System.Double");
            column12.AllowDBNull = true;
            column12.Caption = "C10";
            column12.DefaultValue = "0";
            dt_new.Columns.Add(column12);

            DataColumn column13 = new DataColumn("C11");
            column13.DataType = System.Type.GetType("System.Double");
            column13.AllowDBNull = true;
            column13.Caption = "C11";
            column13.DefaultValue = "0";
            dt_new.Columns.Add(column13);

            DataColumn column14 = new DataColumn("C12");
            column14.DataType = System.Type.GetType("System.Double");
            column14.AllowDBNull = true;
            column14.Caption = "C12";
            column14.DefaultValue = "0";
            dt_new.Columns.Add(column14);

            DataColumn column15 = new DataColumn("Total");
            column15.DataType = System.Type.GetType("System.Double");
            column15.AllowDBNull = true;
            column15.Caption = "Total";
            column15.DefaultValue = "0";
            dt_new.Columns.Add(column15);

            DataColumn column16 = new DataColumn("Metric");
            column16.DataType = System.Type.GetType("System.Double");
            column16.AllowDBNull = true;
            column16.Caption = "Metric";
            column16.DefaultValue = "0";
            dt_new.Columns.Add(column16);

            int intMonth = getMonths(strStart, strEnd);
            int intMonth1, intMonth2;
            intMonth1 = Convert.ToInt32(ddlMonthS.Text);
            intMonth2 = Convert.ToInt32(ddlMonthE.Text);
            string strYear = txtYearS.Text;
            string strDateRage, strDateRage1;
            strDateRage = "";
            for (int intI = 1; intI <= intMonth; intI++)
            {
                if (intMonth1 >= 13)
                {
                    intMonth1 = 1;
                    strYear = txtYearE.Text;
                }
                strDateRage1 = strYear + "/" + intMonth1.ToString();
                strDateRage = strDateRage + "[" + strDateRage1 + "]";

                intMonth1++;


                if (intI != intMonth)
                    strDateRage = strDateRage + ",";
            }

            string strLocal;
            if (rdoLocal.Checked == true)
                strLocal = "DA40";
            else
                strLocal = "DA40-WJ";

            string strName;
            int intCount;
            DataTable dtEmp = null;
            if ((ddlEmp.Text == "ALL") || (ddlTeam.Text == "ALL"))
            {
                dtEmp = clsData.UploadTeamEmp(ddlTeam.Text, strLocal);
                intCount = dtEmp.Rows.Count;
            }
            else
                intCount = 1;

            DataRow dr;
            DataTable dtProject;
            string strEvent;
            for (int intI = 0; intI < intCount; intI++)
            {
                if (intCount == 1)
                    strName = ddlEmp.Text;
                else
                    strName = dtEmp.Rows[intI]["Name_En"].ToString();

                DataTable dt = clsData.UploadManpowerReport_Month(strName, strStart, strEnd, strDateRage);

                for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
                {

                    if ((dt.Rows[intJ]["Project"].ToString() == "Day off") || (dt.Rows[intJ]["Project"].ToString() == "Meeting") || (dt.Rows[intJ]["Project"].ToString() == "Other"))
                        strEvent = dt.Rows[intJ]["Project"].ToString();
                    else
                    {
                        dtProject = clsData.UploadProjectQuery(dt.Rows[intJ]["Project"].ToString(), "Project");
                        if (dtProject.Rows.Count > 0)
                            strEvent = dtProject.Rows[0]["Name"].ToString();
                        else
                            strEvent = "";
                    }

                    dr = dt_new.NewRow();
                    dr["Name"] = strName;
                    dr["Project"] = strEvent;
                    dr["C1"] = 0;
                    dr["C2"] = 0;
                    dr["C3"] = 0;
                    dr["C4"] = 0;
                    dr["C5"] = 0;
                    dr["C6"] = 0;
                    dr["C7"] = 0;
                    dr["C8"] = 0;
                    dr["C9"] = 0;
                    dr["C10"] = 0;
                    dr["C11"] = 0;
                    dr["C12"] = 0;
                    dr["Total"] = 0;
                    dr["Metric"] = 0;

                    int intW1 = 1;
                    double dTotal = 0;
                    double dTotal1 = 0;
                    double dMetric = 0;

                    intMonth1 = Convert.ToInt32(ddlMonthS.Text);

                    strYear = txtYearS.Text;
                    for (int intW = 1; intW <= intMonth; intW++)
                    {
                        if (intMonth1 >= 13)
                        {
                            intMonth1 = 1;
                            strYear = txtYearE.Text;
                        }
                        strDateRage1 = strYear + "/" + intMonth1.ToString();

                        intMonth1++;


                        //if (intI != intMonth)
                        //    strDateRage = strDateRage + ",";

                        if ((dt.Rows[intJ][strDateRage1].ToString() == null) || (dt.Rows[intJ][strDateRage1].ToString() == ""))
                            dTotal1 = 0;
                        else
                            dTotal1 = Convert.ToDouble(dt.Rows[intJ][strDateRage1].ToString());
                        dTotal = dTotal + dTotal1;

                        dr["C" + intW1.ToString()] = dTotal1;

                        intW1++;
                    }

                    dr["Total"] = dTotal;


                    dMetric = dTotal / (getDays(Convert.ToDateTime(strStart), Convert.ToDateTime(strEnd)) * 8);
                    dr["Metric"] = String.Format("{0:N2}", dMetric);


                    dt_new.Rows.Add(dr);
                }

            }

            gvwMain.DataSource = dt_new;
            gvwMain.DataBind();


            intMonth1 = Convert.ToInt32(ddlMonthS.Text);
            strYear = txtYearS.Text;
            int intHeader = 2;
            for (int intW = 1; intW <= intMonth; intW++)
            {

                if (intMonth1 >= 13)
                {
                    intMonth1 = 1;
                    strYear = txtYearE.Text;
                }
                strDateRage1 = strYear + "/" + intMonth1.ToString();

                intMonth1++;

                gvwMain.HeaderRow.Cells[intHeader].Text = strDateRage1;
                intHeader++;
            }

            for (int intW = intHeader; intW <= 13; intW++)
            {
                gvwMain.Columns[intW].Visible = false;
            }
        }
    }

    private void GvQuery_Week()
    {
        string strStart, strEnd;

        gvwMain.Visible = true;
        gvwMain1.Visible = false;
        for (int intW = 0; intW <= 13; intW++)
        {
            gvwMain.Columns[intW].Visible = true;
        }

        if ((Convert.ToInt32(ddlMonthA.Text) < 9))
        {
            if ((Convert.ToInt32(ddlMonthA.Text) % 2) == 0)
            {
                if (ddlMonthA.Text == "02")
                {
                    if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                        strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/29";
                    else
                        strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/28";
                }
                else
                {
                    if (ddlMonthA.Text == "08")
                        strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/31";
                    else
                        strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/30";
                }
            }
            else
                strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/31";
        }
        else
        {
            if ((ddlMonthA.Text == "09") || (ddlMonthA.Text == "11"))
                strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/30";
            else
                strEnd = txtYearA.Text + "/" + ddlMonthA.Text + "/31";
        }
        strStart = txtYearA.Text.Trim() + "/" + ddlMonthA.Text + "/01";

        string strWeek_S = GetWeekOfYear(Convert.ToDateTime(strStart)).ToString();
        string strWeek_E = GetWeekOfYear(Convert.ToDateTime(strEnd)).ToString();

        int intWeek_S = Convert.ToInt32(strWeek_S);
        int intWeek_E = Convert.ToInt32(strWeek_E);

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Project");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Project";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("C1");
        column3.DataType = System.Type.GetType("System.Double");
        column3.AllowDBNull = true;
        column3.Caption = "C1";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("C2");
        column4.DataType = System.Type.GetType("System.Double");
        column4.AllowDBNull = true;
        column4.Caption = "C2";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("C3");
        column5.DataType = System.Type.GetType("System.Double");
        column5.AllowDBNull = true;
        column5.Caption = "C3";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("C4");
        column6.DataType = System.Type.GetType("System.Double");
        column6.AllowDBNull = true;
        column6.Caption = "C4";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("C5");
        column7.DataType = System.Type.GetType("System.Double");
        column7.AllowDBNull = true;
        column7.Caption = "C5";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("C6");
        column8.DataType = System.Type.GetType("System.Double");
        column8.AllowDBNull = true;
        column8.Caption = "C6";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("C7");
        column9.DataType = System.Type.GetType("System.Double");
        column9.AllowDBNull = true;
        column9.Caption = "C7";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("C8");
        column10.DataType = System.Type.GetType("System.Double");
        column10.AllowDBNull = true;
        column10.Caption = "C8";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("C9");
        column11.DataType = System.Type.GetType("System.Double");
        column11.AllowDBNull = true;
        column11.Caption = "C9";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("C10");
        column12.DataType = System.Type.GetType("System.Double");
        column12.AllowDBNull = true;
        column12.Caption = "C10";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("C11");
        column13.DataType = System.Type.GetType("System.Double");
        column13.AllowDBNull = true;
        column13.Caption = "C11";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("C12");
        column14.DataType = System.Type.GetType("System.Double");
        column14.AllowDBNull = true;
        column14.Caption = "C12";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);

        DataColumn column15 = new DataColumn("Total");
        column15.DataType = System.Type.GetType("System.Double");
        column15.AllowDBNull = true;
        column15.Caption = "Total";
        column15.DefaultValue = "0";
        dt_new.Columns.Add(column15);

        DataColumn column16 = new DataColumn("Metric");
        column16.DataType = System.Type.GetType("System.Double");
        column16.AllowDBNull = true;
        column16.Caption = "Metric";
        column16.DefaultValue = "0";
        dt_new.Columns.Add(column16);

        string strWeek="";
        for (int intI = intWeek_S; intI <= intWeek_E; intI++)
        {
            strWeek = strWeek + "[" + intI.ToString() + "]";

            if (intI != intWeek_E)
                strWeek = strWeek + ",";
        }

        string strLocal;
        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";

        string strName;
        int intCount;
        DataTable dtEmp = null;
        if ((ddlEmp.Text == "ALL") || (ddlTeam.Text == "ALL"))
        {
            dtEmp = clsData.UploadTeamEmp(ddlTeam.Text, strLocal);
            intCount = dtEmp.Rows.Count;
        }
        else
            intCount = 1;

        DataRow dr;
        DataTable dtProject;
        string strEvent;
        for (int intI = 0; intI < intCount; intI++)
        {
            if (intCount == 1)
                strName = ddlEmp.Text;
            else
                strName = dtEmp.Rows[intI]["Name_En"].ToString();

            DataTable dt = clsData.UploadManpowerReport(strWeek, strName);

            for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
            {

                if ((dt.Rows[intJ]["Project"].ToString() == "Day off") || (dt.Rows[intJ]["Project"].ToString() == "Meeting") || (dt.Rows[intJ]["Project"].ToString() == "Other"))
                    strEvent = dt.Rows[intJ]["Project"].ToString();
                else
                {
                    dtProject = clsData.UploadProjectQuery(dt.Rows[intJ]["Project"].ToString(), "Project");
                    if (dtProject.Rows.Count > 0)
                        strEvent = dtProject.Rows[0]["Name"].ToString();
                    else
                        strEvent = "";
                }

                dr = dt_new.NewRow();
                dr["Name"] = strName;
                dr["Project"] = strEvent;
                dr["C1"] = 0;
                dr["C2"] = 0;
                dr["C3"] = 0;
                dr["C4"] = 0;
                dr["C5"] = 0;
                dr["C6"] = 0;
                dr["C7"] = 0;
                dr["C8"] = 0;
                dr["C9"] = 0;
                dr["C10"] = 0;
                dr["C11"] = 0;
                dr["C12"] = 0;
                dr["Total"] = 0;
                dr["Metric"] = 0;

                int intW1 = 1;
                double dTotal = 0;
                double dTotal1 = 0;
                double dMetric = 0;
                for (int intW = intWeek_S; intW <= intWeek_E; intW++)
                {
                    if ((dt.Rows[intJ][intW.ToString()].ToString() == null) || (dt.Rows[intJ][intW.ToString()].ToString() == ""))
                        dTotal1 = 0;
                    else
                        dTotal1 = Convert.ToDouble(dt.Rows[intJ][intW.ToString()].ToString());
                    dTotal = dTotal + dTotal1;

                    dr["C"+intW1.ToString()] = dTotal1;

                    intW1++;
                }

                dr["Total"] = dTotal;


                dMetric = dTotal / (getDays(Convert.ToDateTime(strStart), Convert.ToDateTime(strEnd)) * 8);
                dr["Metric"] = String.Format("{0:N2}", dMetric); 


                dt_new.Rows.Add(dr);
            }
            
        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        int intHeader = 2;
        for (int intW = intWeek_S; intW <= intWeek_E; intW++)
        {
            gvwMain.HeaderRow.Cells[intHeader].Text = intW.ToString();
            intHeader++;
        }

        for (int intW = intHeader ;intW<=13;intW++)
        {
            gvwMain.Columns[intW].Visible = false;
        }
        

    }

    public static int getMonths(string strFrom, string strTo)
    {
        DateTime dtStart = DateTime.Parse(strFrom);
        DateTime dtEnd = DateTime.Parse(strTo);

        
        int iMonths = dtEnd.Year * 12 + dtEnd.Month - (dtStart.Year * 12 + dtStart.Month) + 1;
        return iMonths;
        
        
    }

    public int getDays(DateTime dt1,DateTime dt2)
    {

        //DateTime dt1 = new DateTime(2010, 01, 01);//初始化一個日期

        DateTime dt3 = DateTime.Now;//獲取今天日期
        TimeSpan ts1;

        if (dt2 > dt3)
        {
            ts1 = dt3.Subtract(dt1);//TimeSpan得到dt1和dt2的時間間隔
        }
        else
            ts1 = dt2.Subtract(dt1);//TimeSpan得到dt1和dt2的時間間隔

        //TimeSpan ts1 = dt1.Subtract(dt2);//TimeSpan得到dt1和dt2的時間間隔
        
        int countday = ts1.Days;//獲取兩個日期間的總天數

        int weekday = 0;//工作日

        //循環用來扣除總天數中的雙休日

        for (int i = 0; i < countday; i++)
        {

            DateTime tempdt = dt1.Date.AddDays(i);

            if (tempdt.DayOfWeek != System.DayOfWeek.Saturday && tempdt.DayOfWeek != System.DayOfWeek.Sunday)
            {

                weekday++;

            }

        }

        return weekday;

    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {

        for (int intI = 0; intI < 1; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[2].ColumnSpan = 3;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;

            //e.Row.Cells[5].ColumnSpan = 3;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;

            //e.Row.Cells[8].ColumnSpan = 3;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;

            //e.Row.Cells[11].ColumnSpan = 3;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;

            //int intHeader = 2;
            //for (int intW = intWeek_S; intW <= intWeek_E; intW++)
            //{
            //    gvwMain.HeaderRow.Cells[intHeader].Text = intW.ToString();
            //    intHeader++;
            //}

            

        }
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    #region gvwMain_PreRender
    protected void gvwMain1_PreRender(object sender, EventArgs e)
    {

        for (int intI = 0; intI < 1; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain1.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwMain1.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwMain1.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }
    }
    #endregion

    protected void gvwMain1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[2].ColumnSpan = 3;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;

            //e.Row.Cells[5].ColumnSpan = 3;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;

            //e.Row.Cells[8].ColumnSpan = 3;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;

            //e.Row.Cells[11].ColumnSpan = 3;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;

            //int intHeader = 2;
            //for (int intW = intWeek_S; intW <= intWeek_E; intW++)
            //{
            //    gvwMain.HeaderRow.Cells[intHeader].Text = intW.ToString();
            //    intHeader++;
            //}



        }
    }

    protected void gvwMain1_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    private int GetWeekOfYear(DateTime dt)
    {
        GregorianCalendar gc = new GregorianCalendar();
        return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }

    private void BindLineChart()
    {
        int intRowCount = 0;
        int intChartCount = 0;
        TableCell TCell;
        TableRow TRow;
        string strAID = "";
        string strAName = "";
        DataTable dt;
        string strToday = DateTime.Now.ToString("yyyy/MM/dd");

        double[] intEvent = new double[13];
        
        int intCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(gvwMain1.Rows.Count) / 2));
        int intCount1 = gvwMain1.Rows.Count;
        
        for (int intI = 0; intI < intCount; intI++)
        {
            
            TRow = new TableRow();
            for (int intJ = 0; intJ < 2; intJ++)
            {
                //int intTotal = 0;
                TCell = new TableCell();
                //================================
                DataTable dt_new1 = new DataTable("dt_new1");
                

                DataColumn column1 = new DataColumn("Event");
                column1.DataType = System.Type.GetType("System.String");
                column1.AllowDBNull = true;
                column1.Caption = "Auto";
                column1.DefaultValue = "0";
                dt_new1.Columns.Add(column1);


                DataRow dr;
                Literal liter1 = new Literal();
                Literal liter2 = new Literal();
                //=============================
                DataTable dsChartData = new DataTable();
                StringBuilder strScript = new StringBuilder();

                if (intRowCount == gvwMain1.Rows.Count - 1)
                {
                    if ((gvwMain1.Rows[intRowCount].Cells[1].Text == "") || (gvwMain1.Rows[intRowCount].Cells[1].Text == "&nbsp;"))
                        strAName = "";
                    else
                        strAName = gvwMain1.Rows[intRowCount].Cells[1].Text;

                    //int intMonth1 = 1;
                    

                    for (int intMCount = 2; intMCount < 14; intMCount++)
                    {
                        dr = dt_new1.NewRow();

                        dr["Event"] = gvwMain1.Rows[intRowCount].Cells[intMCount].Text;
                        
                        dt_new1.Rows.Add(dr);
                    }
                    
                    //intRowCount++;
                    intJ = 2;
                }
                else
                {
                    

                    if ((gvwMain1.Rows[intRowCount].Cells[1].Text == "") || (gvwMain1.Rows[intRowCount].Cells[1].Text == "&nbsp;"))
                        strAName = "";
                    else
                        strAName = gvwMain1.Rows[intRowCount].Cells[1].Text;

                    //string strDepartment = "";
                    int intX = 0;
                    while (intX == 0)
                    {
                        if (intRowCount < gvwMain1.Rows.Count)
                        {
                            if (intRowCount == gvwMain1.Rows.Count - 1)
                            {
                                intJ = 2;
                                intX = 1;
                                for (int intMCount = 2; intMCount < 14; intMCount++)
                                {
                                    dr = dt_new1.NewRow();

                                    dr["Event"] = gvwMain1.Rows[intRowCount].Cells[intMCount].Text;
                                    
                                    dt_new1.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                intX = 1;
                                if (intX == 1)
                                {
                                    for (int intMCount = 2; intMCount < 14; intMCount++)
                                    {
                                        dr = dt_new1.NewRow();

                                        dr["Event"] = gvwMain1.Rows[intRowCount].Cells[intMCount].Text;
                                        
                                        dt_new1.Rows.Add(dr);
                                    }
                                }

                            }
                        }
                        else
                        {
                            intJ = 2;
                            intX = 1;
                        }
                    }
                }


                dsChartData = dt_new1;

                strScript.Append(@"<script type='text/javascript'>  
                                            google.load('visualization', '1', {packages: ['corechart']}); </script>  
                                              
                                            <script type='text/javascript'>  
                                             
                                            function drawChart() {         
                                            var data = google.visualization.arrayToDataTable([  
                                            ['Month', ' " + strAName + "'],");
                
                string strDate = Session["DateRage"].ToString().Replace("[","");
                strDate = strDate.Replace("]", "");
                string[] sArray = strDate.Split(',');

                int intRage = 0;
                foreach (DataRow row1 in dsChartData.Rows)
                {


                    strScript.Append("['" + sArray[intRage] + "'," + row1["Event"] + "],");
                    intRage++;
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                //                    strScript.Append(@" var options = {     
                //                                    title: 'My Daily Schedule',            
                //                                    is3D: true,          
                //                                    };   ");
                strScript.Append(@" var options = {     
                                title: '" + strAName + @"',            
                                is3D: true,          
                                };   ");

                strScript.Append(@"var chart = new google.visualization.LineChart(document.getElementById('piechart_3d" + intChartCount.ToString() + @"'));          
                                            chart.draw(data, options);        
                                            }    
                                        google.setOnLoadCallback(drawChart);  
                                        ");
                strScript.Append(" </script>");


                TCell.ID = "cell" + intChartCount.ToString();
                liter1.Text = strScript.ToString();
                liter1.ID = "liter" + intChartCount.ToString();
                TCell.Controls.Add(liter1);
                TRow.Cells.Add(TCell);
                liter2.Text = "<div id=\"piechart_3d" + intChartCount.ToString() + "\" style=\"width: 500px;border: 1px solid #ccc\"></div>";
                liter2.ID = "literP" + intChartCount.ToString();
                TCell.Controls.Add(liter2);
                TRow.ID = "row" + intChartCount.ToString();
                TRow.Cells.Add(TCell);
                intChartCount++;
                for (int intMonth = 1; intMonth < 13; intMonth++)
                {
                    intEvent[intMonth] = 0;
                    
                }
                
                //================================
                //TRow.Cells.Add(TCell);
                intRowCount++;
            }
            LineChart.Rows.Add(TRow);
            
        }
        
    }

    private void BindColumnChart()
    {
        StringBuilder strScript = new StringBuilder();

        string strTital="['月份',";
        int intI;
        for (intI = 0; intI < gvwMain1.Rows.Count; intI++)
        {
            strTital = strTital + "'" + gvwMain1.Rows[intI].Cells[1].Text + "'";

            if (intI != gvwMain1.Rows.Count - 1)
                strTital = strTital + ",";
        }
        strTital = strTital + "]";
        strScript.Append(@"<script type='text/javascript'>  
                                google.charts.load('current', {packages:['corechart']});
                                    
                                            </script>  
                                             
                                            <script type='text/javascript'>  
                                             
                                            function drawChart() {  
                                            var data = google.visualization.arrayToDataTable([  
                                            " + strTital + ",");
        

        
        string[] sArray = strTital.Split(',');

        string strDate = Session["DateRage"].ToString().Replace("[", "");
        strDate = strDate.Replace("]", "");
        string[] sDate = strDate.Split(',');

        int intCells = 2;

        for (int intJ = 0; intJ < 12; intJ++)
        {
            strScript.Append("['" + sDate[intJ] + "',");
            for (intI = 0; intI < gvwMain1.Rows.Count; intI++)
            {
                strScript.Append(gvwMain1.Rows[intI].Cells[intCells].Text);
                if (intI == gvwMain1.Rows.Count - 1)
                {
                    strScript.Append("],");
                }
                else
                    strScript.Append(",");
            }
            intCells++;
        }

        strScript.Remove(strScript.Length - 1, 1);
        strScript.Append("]);");



        strScript.Append(@"var options = {
                                                title: '',
                                                width: '100%',
                                                height: 400,
                                                legend: { position: 'top', maxLines: 2 },
                                                bar: { groupWidth: '75%' },
                                                isStacked: 'percent'
                                            };   ");
        strScript.Append(@"var chart = new google.visualization.ColumnChart(document.getElementById('columnchart'));          
                                            chart.draw(data, options);        
                                            }    
                                        google.setOnLoadCallback(drawChart);  
                                        ");

        strScript.Append(" </script>");

        TableRow TRow = new TableRow();
        TableCell TCell = new TableCell();
        Literal liter1 = new Literal();
        Literal liter2 = new Literal();
        TCell.ID = "cell";
        liter1.Text = strScript.ToString();
        liter1.ID = "liter";
        TCell.Controls.Add(liter1);
        TRow.Cells.Add(TCell);
        liter2.Text = "<div id=\"columnchart\" style=\"width: 900px; height: 500px;\"></div>";
        liter2.ID = "literP";
        TCell.Controls.Add(liter2);
        TRow.ID = "row";
        TRow.Cells.Add(TCell);

        ColumnChart.Rows.Add(TRow);
    }
    
    
}
