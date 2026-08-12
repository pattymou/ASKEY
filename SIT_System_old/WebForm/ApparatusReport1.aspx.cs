using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Text;

using System.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;

public partial class WebForm_ApparatusReport1 : System.Web.UI.Page
{
    public static string strStart;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            //loadKind(this.ddlKind, "1");
            loadKind(this.ddlKind1, "0");
            String strYear = DateTime.Now.Year.ToString();

            txtYearE.Text = strYear;
            txtYearS.Text = strYear;
            //loadDepartment(this.ddlDepartment);
            rdoProducts_ID.Checked = true;
            //rdoCase.Checked = true;
            Label5.Visible = false;
            //gvwMain1.Visible = false;


            //if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            //else
            //    rdoLocal1.Checked = true;

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL, string strKind)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7, strKind);
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "1");
    }
    #endregion

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "1");
    }
    #endregion

    public static DateTime GetTheFirstDayOfWeek(DateTime dt1)
    {
        return dt1.AddDays((int)dt1.DayOfWeek * -1).Date;
    }

    public static DateTime GetTheLastDayOfWeek(DateTime dt1)
    {
        return dt1.AddDays(7 + (int)dt1.DayOfWeek * -1 - 1).Date;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //gvwMain1.Visible = true;
        DataTable dt = clsData.UploadApparatusReport_New("", "", "");
        gvwMain1.DataSource = dt;
        gvwMain1.DataBind();

        if (rdoProducts_ID.Checked == true)
            if (txtProducts_ID.Text == "")
                clsMsg.AlertMessage("請輸入財產編號！", this.Page);
            else
                getData_N();
        else
        {
            if (ddlKind1.Text == "")
                clsMsg.AlertMessage("請選擇類別！", this.Page);
            else
            {
                if (ddlApparatus.Text == "")
                    clsMsg.AlertMessage("請選擇設備項目！", this.Page);
                else
                    getData_N();
            }
        }

    }

    private void setTable()
    {
        TableRow row = new TableRow();
        Label lbl1 = new Label();
        lbl1.Text = "No.1";
        TableCell cell1 = new TableCell();
        Literal liter1 = new Literal();
        liter1.Text = "<div id=\"piechart_div\" style=\"border: 1px solid #ccc\"></div>";
        cell1.Controls.Add(liter1);
        row.Cells.Add(cell1);
        PieChart.Rows.Add(row);
    }

    protected void ddlKind1_SelectedIndexChanged(object sender, EventArgs e)
    {
        clsDropDownList.ddlApparatusKind(this.ddlApparatus, ddlKind1.SelectedItem.Text);

    }

    private void getData_N()
    {
        string strStartDate = "";
        string strEndDate = "";
        string strLocal;
        string strProducts_ID="999999";
        DataTable dt1;


        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";

        strStartDate = txtYearS.Text.Trim() + "/" + "01/01";
        strEndDate = txtYearE.Text + "/" + "12/31";

        //if (rdoCase.Checked == true)
        //{
        if (rdoProducts_ID.Checked == true)
            dt1 = clsData.UploadAChart_View1("2", txtProducts_ID.Text, "", strLocal);
        else
            dt1 = clsData.UploadAChart_View1("3", ddlApparatus.SelectedValue, "", strLocal);

        if (dt1.Rows.Count > 0)
        {
            lblName.Text = dt1.Rows[0]["Name"].ToString();
            strProducts_ID = dt1.Rows[0]["Products_ID"].ToString();

        }
        DataTable dt = clsData.UploadApparatusReport_New(strStartDate, strEndDate, strProducts_ID);
        gvwMain1.DataSource = dt;
        gvwMain1.DataBind();
        Label5.Visible = true;
        BindPieChart(strStartDate, strEndDate);
        //}
        //else
        //{
            //DataTable dt = clsData.UploadReservationCaseCount(strStartDate, strEndDate, txtProducts_ID.Text);
            //gvwMain2.DataSource = dt;
            //gvwMain2.DataBind();
        //}

    }

    protected void gvwMain1_PreRender(object sender, EventArgs e)
    {
    }

    protected void gvwMain1_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    protected void gvwMain1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells[1].Text == "&nbsp;")
            e.Row.Cells[1].Text = "0";
        if (e.Row.Cells[2].Text == "&nbsp;")
            e.Row.Cells[2].Text = "0";
        if (e.Row.Cells[3].Text == "&nbsp;")
            e.Row.Cells[3].Text = "0";
        if (e.Row.Cells[4].Text == "&nbsp;")
            e.Row.Cells[4].Text = "0";
        if (e.Row.Cells[5].Text == "&nbsp;")
            e.Row.Cells[5].Text = "0";
        if (e.Row.Cells[6].Text == "&nbsp;")
            e.Row.Cells[6].Text = "0";
        if (e.Row.Cells[7].Text == "&nbsp;")
            e.Row.Cells[7].Text = "0";
        if (e.Row.Cells[8].Text == "&nbsp;")
            e.Row.Cells[8].Text = "0";
        if (e.Row.Cells[9].Text == "&nbsp;")
            e.Row.Cells[9].Text = "0";
        if (e.Row.Cells[10].Text == "&nbsp;")
            e.Row.Cells[10].Text = "0";
        if (e.Row.Cells[11].Text == "&nbsp;")
            e.Row.Cells[11].Text = "0";
        if (e.Row.Cells[12].Text == "&nbsp;")
            e.Row.Cells[12].Text = "0";

        e.Row.Cells[1].Width = 100;
        e.Row.Cells[2].Width = 100;
        e.Row.Cells[3].Width = 100;
        e.Row.Cells[4].Width = 100;
        e.Row.Cells[5].Width = 100;
        e.Row.Cells[6].Width = 100;
        e.Row.Cells[7].Width = 100;
        e.Row.Cells[8].Width = 100;
        e.Row.Cells[9].Width = 100;
        e.Row.Cells[10].Width = 100;
        e.Row.Cells[11].Width = 100;
        e.Row.Cells[12].Width = 100;
        e.Row.Cells[13].Width = 100;
    }

    private void BindPieChart(string strStartDate, string strEndDate)
    {
        int intChartCount = 0;
        TableCell TCell;
        TableRow TRow;
        string strAName = "";
        int intHeader = 1;
        string strToday = DateTime.Now.ToString("yyyy/MM/dd");


        DateTime STime = DateTime.Parse(strStartDate); //起始日
        DateTime ETime = DateTime.Parse(strEndDate); //結束日
        TimeSpan TimeTotal = ETime.Subtract(STime); //日期相減
        lblCount.Text = ((TimeTotal.TotalDays + 1) * 2).ToString();
        float intTotal = 0;
        float fIdel=0;

        for (int intI = 0; intI < 6; intI++)
        {
            TRow = new TableRow();
            for (int intJ = 0; intJ < 2; intJ++)
            {

                TCell = new TableCell();
                //================================
                DataTable dt_new1 = new DataTable("dt_new1");
                DataColumn column1 = new DataColumn("Deparment");
                column1.DataType = System.Type.GetType("System.String");
                column1.AllowDBNull = true;
                column1.Caption = "Deparment";
                column1.DefaultValue = "0";
                dt_new1.Columns.Add(column1);

                DataColumn column2 = new DataColumn("Count");
                column2.DataType = System.Type.GetType("System.String");
                column2.AllowDBNull = true;
                column2.Caption = "Count";
                column2.DefaultValue = "0";
                dt_new1.Columns.Add(column2);



                DataRow dr;
                Literal liter1 = new Literal();
                Literal liter2 = new Literal();
                //=============================
                DataTable dsChartData = new DataTable();
                StringBuilder strScript = new StringBuilder();
                    if (intHeader == 1) 
                        intTotal=31*24;
                    if (intHeader == 2) 
                        intTotal=28*24;
                    if (intHeader == 3) 
                        intTotal=31*24;
                    if (intHeader == 4) 
                        intTotal=30*24;
                    if (intHeader == 5) 
                        intTotal=31*24;
                    if (intHeader == 6) 
                        intTotal=30*24;
                    if (intHeader == 7) 
                        intTotal=31*24;
                    if (intHeader == 8) 
                        intTotal=31*24;
                    if (intHeader == 9) 
                        intTotal=30*24;
                    if (intHeader == 10) 
                        intTotal=31*24;
                    if (intHeader == 11) 
                        intTotal=30*24;
                    if (intHeader == 12) 
                        intTotal=31*24;
                for (int intGVCount = 0;intGVCount <gvwMain1.Rows.Count;intGVCount++)
                {
                    //int intTotal = 0;
                    
                    //string strDepartment;




                    strAName = gvwMain1.Columns[intHeader].HeaderText;
                    dr = dt_new1.NewRow();

                    dr["Deparment"] = gvwMain1.Rows[intGVCount].Cells[0].Text.Replace("&amp;", " & ");

                    dr["Count"] = gvwMain1.Rows[intGVCount].Cells[intHeader].Text;
                    dt_new1.Rows.Add(dr);

                    intTotal = intTotal - float.Parse(gvwMain1.Rows[intGVCount].Cells[intHeader].Text);
                    if (intTotal < 0)
                        intTotal = 0;

                }
                dr = dt_new1.NewRow();

                dr["Deparment"] = "Idle";

                dr["Count"] = intTotal;
                dt_new1.Rows.Add(dr);

                intHeader++;


                


                dsChartData = dt_new1;

                strScript.Append(@"<script type='text/javascript'>  
                                                google.load('visualization', '1', {packages: ['corechart']}); </script>  
                                                  
                                                <script type='text/javascript'>  
                                                 
                                                function drawChart() {         
                                                var data = google.visualization.arrayToDataTable([  
                                                ['Task', 'Hours of Day'],");

                foreach (DataRow row1 in dsChartData.Rows)
                {
                   
                    strScript.Append("['" + row1["Deparment"] + "'," + row1["Count"] + "],");
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
                                    width: 700,
                                    height: 400,      
                                    };   ");

                strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d" + intChartCount.ToString() + @"'));          
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
                liter2.Text = "<div id=\"piechart_3d" + intChartCount.ToString() + "\" style=\"border: 1px solid #ccc\"></div>";
                liter2.ID = "literP" + intChartCount.ToString();
                TCell.Controls.Add(liter2);
                TRow.ID = "row" + intChartCount.ToString();
                TRow.Cells.Add(TCell);
                intChartCount++;
                //================================
                //TRow.Cells.Add(TCell);
            }
            PieChart.Rows.Add(TRow);
        }
    }

}
