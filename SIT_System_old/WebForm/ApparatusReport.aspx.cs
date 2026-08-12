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

public partial class WebForm_ApparatusReport : System.Web.UI.Page
{
    public static string strStart;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadKind(this.ddlKind, "1");
            loadKind(this.ddlKind1, "0");
            String strYear = DateTime.Now.Year.ToString();

            txtYearE.Text = strYear;
            txtYearS.Text = strYear;
            txtYearA.Text = strYear;
            loadDepartment(this.ddlDepartment);
            rdoDepartment.Checked = true;
            //rdoWeek.Checked = true;
            rdoMonth.Checked = true;
            loadEmployees(this.ddlCustodian);
            rdoCustodian.Checked = true;

            gvwMain.Visible = false;
            gvwMain1.Visible = false;
            //div1.Visible = false;
            //div2.Visible = false;

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

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
        if (rdoMonth.Checked == true)
        {
            gvwMain.Visible = false;
            gvwMain1.Visible = true;
            //div1.Visible = false;
            //div2.Visible = true;
            getDataM();
        }
        else
        {
            gvwMain.Visible = true;
            gvwMain1.Visible = false;
            //div1.Visible = true;
            //div2.Visible = false;
            getData1();
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

    public DataTable dtColumnChart(GridView gvw, int intACell, int intMCell)
    {
        DataTable dt_new1 = new DataTable("dt_new1");
        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new1.Columns.Add(column1);

        DataColumn column2 = new DataColumn("ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "ID";
        column2.DefaultValue = "0";
        dt_new1.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Auto");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Auto";
        column3.DefaultValue = "0";
        dt_new1.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Manual");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Manual";
        column4.DefaultValue = "0";
        dt_new1.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Idle");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Idle";
        column5.DefaultValue = "0";
        dt_new1.Columns.Add(column5);

        string strName = "";
        string strID = "";
        int intAuto = 0;
        int intManual = 0;
        DataRow dr;
        for (int intJ = 0; intJ < gvw.Rows.Count; intJ++)
        {

            if (intJ == 0)
            {
                strName = gvw.Rows[0].Cells[0].Text;
                strID = gvw.Rows[0].Cells[1].Text;

            }
            if ((strName == gvw.Rows[intJ].Cells[0].Text) && (strID == gvw.Rows[intJ].Cells[1].Text))
            {
                intAuto = intAuto + Convert.ToInt32(gvw.Rows[intJ].Cells[intACell].Text);
                intManual = intManual + Convert.ToInt32(gvw.Rows[intJ].Cells[intMCell].Text);

                if (intJ == gvw.Rows.Count - 1)
                {
                    //for (int intX = 0; intX < 2; intX++)
                    //{
                    //    dr = dt_new1.NewRow();

                    //    dr["Name"] = strName;
                    //    dr["ID"] = strID;
                    //    if (intX == 0)
                    //    {
                    //        dr["Kind"] = "M";
                    //        dr["Total"] = intManual.ToString();
                    //    }
                    //    else
                    //    {
                    //        dr["Kind"] = "A";
                    //        dr["Total"] = intAuto.ToString();
                    //    }

                    //    dt_new1.Rows.Add(dr);
                    //}
                    dr = dt_new1.NewRow();

                    dr["Name"] = strName;
                    dr["ID"] = strID;
                    dr["Auto"] = intAuto.ToString();
                    dr["Manual"] = intManual.ToString();

                    if ((Convert.ToInt32(lblCount.Text) - intAuto - intManual) > 0)
                        dr["Idle"] = (Convert.ToInt32(lblCount.Text) - intAuto - intManual).ToString();
                    else
                        dr["Idle"] = 0;


                    dt_new1.Rows.Add(dr);
                }
            }
            else
            {
                //for (int intX = 0; intX < 2; intX++)
                //{
                //    dr = dt_new1.NewRow();

                //    dr["Name"] = strName;
                //    dr["ID"] = strID;
                //    if (intX == 0)
                //    {
                //        dr["Kind"] = "M";
                //        dr["Total"] = intManual.ToString();
                //    }
                //    else
                //    {
                //        dr["Kind"] = "A";
                //        dr["Total"] = intAuto.ToString();
                //    }

                //    dt_new1.Rows.Add(dr);
                //}
                dr = dt_new1.NewRow();

                dr["Name"] = strName;
                dr["ID"] = strID;
                dr["Auto"] = intAuto.ToString();
                dr["Manual"] = intManual.ToString();
                if ((Convert.ToInt32(lblCount.Text) - intAuto - intManual) > 0)
                    dr["Idle"] = (Convert.ToInt32(lblCount.Text) - intAuto - intManual).ToString();
                else
                    dr["Idle"] = 0;
                //dr["Idle"] = (Convert.ToInt32(lblCount.Text) - intAuto - intManual).ToString();


                dt_new1.Rows.Add(dr);

                intAuto = 0;
                intManual = 0;

                strName = gvw.Rows[intJ].Cells[0].Text;
                strID = gvw.Rows[intJ].Cells[1].Text;

                intAuto = intAuto + Convert.ToInt32(gvw.Rows[intJ].Cells[intACell].Text);
                intManual = intManual + Convert.ToInt32(gvw.Rows[intJ].Cells[intMCell].Text);
            }


        }

        return dt_new1;
    }


    private void BindColumnChart(GridView gvw, int intACell, int intMCell)
    {
        StringBuilder strScript = new StringBuilder();

        if (rdoKind1.Checked == true)
        {
            DataTable dsChartData = dtColumnChart(gvw, intACell, intMCell);
            strScript.Append(@"<script type='text/javascript'>  
                                    google.charts.load('current', {packages:['corechart']});
                                        
                                                </script>  
                                                 
                                                <script type='text/javascript'>  
                                                 
                                                function drawChart() {  
                                                var data = google.visualization.arrayToDataTable([  
                                                ['Kind', 'Auto', 'Manual', 'Idle'],");
            foreach (DataRow row1 in dsChartData.Rows)
            {

                if ((row1["ID"].ToString() == "&nbsp;") || (row1["ID"].ToString() == ""))
                    strScript.Append("['" + row1["Name"] + "'," + row1["Auto"] + "," + row1["Manual"] + "," + row1["Idle"] + "],");
                else
                    strScript.Append("['" + row1["ID"] + "-" + row1["Name"] + "'," + row1["Auto"] + "," + row1["Manual"] + "," + row1["Idle"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");



            strScript.Append(@"var options = {
                                                    title: '',
                                                    width: 800,
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



            //TableRow row = new TableRow();
            //Label lbl1 = new Label();
            //lbl1.Text = "No.1";
            //TableCell cell1 = new TableCell();
            //Literal liter1 = new Literal();
            //Literal liter2 = new Literal();
            //liter1.Text = strScript.ToString();
            //liter1.ID = "liter1";
            //cell1.Controls.Add(liter1);
            //row.Cells.Add(cell1);
            //liter2.ID = "liter2";
            //liter2.Text = "<div id=\"columnchart\" style=\"width: 900px; height: 500px;\"></div>";
            //cell1.Controls.Add(liter2);
            //row.Cells.Add(cell1);
            ColumnChart.Rows.Add(TRow);
        }
    }

    private void BindChart1(string strStartDate, string strEndDate)
    {
        if (rdoKind1.Checked == true)
        {
            int intRowCount = 0;
            int intChartCount = 0;
            TableCell TCell;
            TableRow TRow;
            string strAID = "";
            string strAName = "";
            DataTable dt = clsData.UploadApparatusReportChart(strStartDate, strEndDate, ddlKind1.Text);
            int intCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(dt.Rows.Count) / 2));
            string strToday = DateTime.Now.ToString("yyyy/MM/dd");
            string strDepartment = "";


            DateTime STime = DateTime.Parse(strStartDate); //起始日
            DateTime ETime = DateTime.Parse(strEndDate); //結束日
            TimeSpan TimeTotal = ETime.Subtract(STime); //日期相減
            lblCount.Text = ((TimeTotal.TotalDays + 1) * 2).ToString();
            int intDay = 0;
            int intNight = 0;
            for (int intI = 0; intI < intCount; intI++)
            {
                //if (intI == 0)
                //    strDepartment = gvwMain.Rows[intRowCount].Cells[2].Text;
                TRow = new TableRow();
                for (int intJ = 0; intJ < 2; intJ++)
                {
                    int intTotal = 0;
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
                    //string strDepartment;

                    if (intRowCount == 0)
                    {
                        strAID = ((Label)this.gvwMain.Rows[intRowCount].Cells[41].FindControl("lblGVSeq")).Text;
                        strDepartment = gvwMain.Rows[intRowCount].Cells[2].Text;
                    }
                    if (intRowCount == gvwMain.Rows.Count - 1)
                    {
                        if ((gvwMain.Rows[intRowCount].Cells[1].Text == "") || (gvwMain.Rows[intRowCount].Cells[1].Text == "&nbsp;"))
                            strAName = gvwMain.Rows[intRowCount].Cells[0].Text;
                        else
                            strAName = gvwMain.Rows[intRowCount].Cells[1].Text + "-" + gvwMain.Rows[intRowCount].Cells[0].Text;

                        dr = dt_new1.NewRow();

                        dr["Deparment"] = gvwMain.Rows[intRowCount].Cells[2].Text;
                        intDay = 0;
                        intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                        intNight = 0;
                        dr["Count"] = (intDay + intNight).ToString();
                        intTotal = intDay + intNight;
                        dt_new1.Rows.Add(dr);
                        intRowCount++;
                        intJ = 2;
                    }
                    else if (intRowCount < gvwMain.Rows.Count - 1)
                    {
                        if (strAID == ((Label)this.gvwMain.Rows[intRowCount].Cells[41].FindControl("lblGVSeq")).Text)
                        {

                            if ((gvwMain.Rows[intRowCount].Cells[1].Text == "") || (gvwMain.Rows[intRowCount].Cells[1].Text == "&nbsp;"))
                                strAName = gvwMain.Rows[intRowCount].Cells[0].Text;
                            else
                                strAName = gvwMain.Rows[intRowCount].Cells[1].Text + "-" + gvwMain.Rows[intRowCount].Cells[0].Text;

                            //intCount = 0;
                            //string strDepartment = "";

                            int intX = 0;
                            intTotal = 0;
                            while (intX == 0)
                            {
                                if (intRowCount < gvwMain.Rows.Count)
                                {
                                    if (intRowCount == gvwMain.Rows.Count - 1)
                                    {
                                        intJ = 2;
                                        intX = 1;
                                    }
                                    else
                                    {
                                        if (strAID != ((Label)this.gvwMain.Rows[intRowCount].Cells[41].FindControl("lblGVSeq")).Text)
                                        {
                                            strAID = ((Label)this.gvwMain.Rows[intRowCount].Cells[41].FindControl("lblGVSeq")).Text;
                                            intX = 1;
                                        }
                                        else
                                        {
                                            //if (strDepartment == gvwMain.Rows[intRowCount + 2].Cells[2].Text)
                                            //{
                                            intDay = intDay + Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                            intRowCount++;
                                            intNight = intNight + Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);

                                            //}
                                            //else
                                            if ((intRowCount + 1) < gvwMain.Rows.Count)
                                            {
                                                if (strAID != ((Label)this.gvwMain.Rows[intRowCount + 1].Cells[41].FindControl("lblGVSeq")).Text)
                                                {
                                                    if (strDepartment != gvwMain.Rows[intRowCount + 1].Cells[2].Text)
                                                    {
                                                        dr = dt_new1.NewRow();

                                                        dr["Deparment"] = strDepartment;

                                                        //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);

                                                        //intRowCount++;
                                                        //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                                        dr["Count"] = (intDay + intNight).ToString();
                                                        intTotal = intDay + intNight;
                                                        dt_new1.Rows.Add(dr);
                                                        intRowCount++;
                                                        intDay = 0;
                                                        intNight = 0;
                                                        strDepartment = gvwMain.Rows[intRowCount].Cells[2].Text;
                                                    }
                                                    else
                                                    {
                                                        dr = dt_new1.NewRow();

                                                        dr["Deparment"] = strDepartment;

                                                        //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);

                                                        //intRowCount++;
                                                        //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                                        dr["Count"] = (intDay + intNight).ToString();
                                                        intTotal = intDay + intNight;
                                                        dt_new1.Rows.Add(dr);
                                                        intRowCount++;
                                                        intDay = 0;
                                                        intNight = 0;
                                                        //strDepartment = gvwMain.Rows[intRowCount].Cells[2].Text;
                                                    }
                                                }
                                                else
                                                {
                                                    if (strDepartment != gvwMain.Rows[intRowCount + 1].Cells[2].Text)
                                                    {
                                                        dr = dt_new1.NewRow();

                                                        dr["Deparment"] = strDepartment;

                                                        //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);

                                                        //intRowCount++;
                                                        //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                                        dr["Count"] = (intDay + intNight).ToString();
                                                        intTotal = intDay + intNight;
                                                        dt_new1.Rows.Add(dr);
                                                        intRowCount++;
                                                        intDay = 0;
                                                        intNight = 0;
                                                        strDepartment = gvwMain.Rows[intRowCount].Cells[2].Text;
                                                    }
                                                    else
                                                        intRowCount++;
                                                }
                                            }
                                            else
                                            {
                                                dr = dt_new1.NewRow();

                                                dr["Deparment"] = strDepartment;

                                                //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);

                                                //intRowCount++;
                                                //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                                dr["Count"] = (intDay + intNight).ToString();
                                                intTotal = intDay + intNight;
                                                dt_new1.Rows.Add(dr);
                                                intRowCount++;
                                                intDay = 0;
                                                intNight = 0;
                                                //strDepartment = gvwMain.Rows[intRowCount+2].Cells[2].Text;
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
                        else
                            strAID = ((Label)this.gvwMain.Rows[intRowCount].Cells[41].FindControl("lblGVSeq")).Text;
                    }
                    dr = dt_new1.NewRow();

                    dr["Deparment"] = "Idle";

                    if ((TimeTotal.TotalDays + 1) * 2 - intTotal < 0)
                        dr["Count"] = "0";
                    else
                        dr["Count"] = ((TimeTotal.TotalDays + 1) * 2 - intTotal).ToString();


                    dt_new1.Rows.Add(dr);


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

    private void BindLineChart(string strStartDate, string strEndDate)
    {
        if (rdoKind1.Checked == true)
        {
            int intRowCount = 0;
            int intChartCount = 0;
            TableCell TCell;
            TableRow TRow;
            string strAID = "";
            string strAName = "";
            DataTable dt = clsData.UploadApparatusReportChart(strStartDate, strEndDate, ddlKind1.Text);
            int intCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(dt.Rows.Count) / 2));
            string strToday = DateTime.Now.ToString("yyyy/MM/dd");

            double[] intAuto = new double[13];
            double[] intManual = new double[13];
            double[] intIdle = new double[13];
            double[] intTotal = new double[13];
            string strYear = DateTime.Now.Year.ToString();

            DateTime STime = DateTime.Parse(strStartDate); //起始日
            DateTime ETime = DateTime.Parse(strEndDate); //結束日
            TimeSpan TimeTotal = ETime.Subtract(STime); //日期相減
            lblCount.Text = ((TimeTotal.TotalDays + 1) * 2).ToString();
            for (int intI = 0; intI < intCount; intI++)
            {
                for (int intMonth = 1; intMonth < 13; intMonth++)
                {
                    intAuto[intMonth] = 0;
                    intManual[intMonth] = 0;
                    intIdle[intMonth] = 0;
                }

                TRow = new TableRow();
                for (int intJ = 0; intJ < 2; intJ++)
                {
                    //int intTotal = 0;
                    TCell = new TableCell();
                    //================================
                    DataTable dt_new1 = new DataTable("dt_new1");
                    //DataColumn column1 = new DataColumn("Deparment");
                    //column1.DataType = System.Type.GetType("System.String");
                    //column1.AllowDBNull = true;
                    //column1.Caption = "Deparment";
                    //column1.DefaultValue = "0";
                    //dt_new1.Columns.Add(column1);

                    //DataColumn column2 = new DataColumn("Count");
                    //column2.DataType = System.Type.GetType("System.String");
                    //column2.AllowDBNull = true;
                    //column2.Caption = "Count";
                    //column2.DefaultValue = "0";
                    //dt_new1.Columns.Add(column2);

                    DataColumn column1 = new DataColumn("Auto");
                    column1.DataType = System.Type.GetType("System.String");
                    column1.AllowDBNull = true;
                    column1.Caption = "Auto";
                    column1.DefaultValue = "0";
                    dt_new1.Columns.Add(column1);

                    DataColumn column2 = new DataColumn("Manual");
                    column2.DataType = System.Type.GetType("System.String");
                    column2.AllowDBNull = true;
                    column2.Caption = "Manual";
                    column2.DefaultValue = "0";
                    dt_new1.Columns.Add(column2);

                    DataColumn column3 = new DataColumn("Idle");
                    column3.DataType = System.Type.GetType("System.String");
                    column3.AllowDBNull = true;
                    column3.Caption = "Idle";
                    column3.DefaultValue = "0";
                    dt_new1.Columns.Add(column3);

                    DataColumn column4 = new DataColumn("Total");
                    column4.DataType = System.Type.GetType("System.String");
                    column4.AllowDBNull = true;
                    column4.Caption = "Total";
                    column4.DefaultValue = "0";
                    dt_new1.Columns.Add(column4);



                    DataRow dr;
                    Literal liter1 = new Literal();
                    Literal liter2 = new Literal();
                    //=============================
                    DataTable dsChartData = new DataTable();
                    StringBuilder strScript = new StringBuilder();

                    if (intRowCount == 0)
                        strAID = ((Label)this.gvwMain1.Rows[intRowCount].Cells[31].FindControl("lblGVSeq")).Text;
                    if (intRowCount == gvwMain1.Rows.Count - 1)
                    {
                        if ((gvwMain1.Rows[intRowCount].Cells[1].Text == "") || (gvwMain1.Rows[intRowCount].Cells[1].Text == "&nbsp;"))
                            strAName = gvwMain1.Rows[intRowCount].Cells[0].Text;
                        else
                            strAName = gvwMain1.Rows[intRowCount].Cells[1].Text + "-" + gvwMain1.Rows[intRowCount].Cells[0].Text;

                        int intMonth1 = 1;
                        for (int intMCount = 4; intMCount < 28; intMCount++)
                        {


                            intAuto[intMonth1] = intAuto[intMonth1] + Convert.ToInt32(gvwMain1.Rows[intRowCount].Cells[intMCount].Text);
                            intMCount++;
                            intManual[intMonth1] = intManual[intMonth1] + Convert.ToInt32(gvwMain1.Rows[intRowCount].Cells[intMCount].Text);
                            int intX1 = DateTime.DaysInMonth(Convert.ToInt32(strYear), intMonth1);
                            intIdle[intMonth1] = (intX1 * 2) - intAuto[intMonth1] - intManual[intMonth1];
                            intTotal[intMonth1] = intX1 * 2;
                            intMonth1++;
                        }

                        for (int intMCount = 1; intMCount < 13; intMCount++)
                        {
                            dr = dt_new1.NewRow();

                            dr["Auto"] = ((intAuto[intMCount] / intTotal[intMCount]) * 100).ToString();
                            dr["Manual"] = ((intManual[intMCount] / intTotal[intMCount]) * 100).ToString();
                            dr["Idle"] = ((intIdle[intMCount] / intTotal[intMCount]) * 100).ToString();
                            dr["Total"] = intTotal[intMCount].ToString();
                            //dr["Deparment"] = gvwMain.Rows[intRowCount].Cells[2].Text;
                            //int intDay = 0;
                            //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                            //int intNight = 0;
                            //intRowCount++;
                            //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                            //dr["Count"] = (intDay + intNight).ToString();
                            //intTotal = intDay + intNight;
                            dt_new1.Rows.Add(dr);
                        }
                        //dr = dt_new1.NewRow();

                        //dr["Deparment"] = gvwMain.Rows[intRowCount].Cells[2].Text;
                        //int intDay = 0;
                        //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                        //int intNight = 0;
                        //dr["Count"] = (intDay + intNight).ToString();
                        //intTotal = intDay + intNight;
                        //dt_new1.Rows.Add(dr);
                        intRowCount++;
                        intJ = 2;
                    }
                    else
                    {
                        if (strAID == ((Label)this.gvwMain1.Rows[intRowCount].Cells[31].FindControl("lblGVSeq")).Text)
                        {

                            if ((gvwMain1.Rows[intRowCount].Cells[1].Text == "") || (gvwMain1.Rows[intRowCount].Cells[1].Text == "&nbsp;"))
                                strAName = gvwMain1.Rows[intRowCount].Cells[0].Text;
                            else
                                strAName = gvwMain1.Rows[intRowCount].Cells[1].Text + "-" + gvwMain1.Rows[intRowCount].Cells[0].Text;

                            //intCount = 0;
                            string strDepartment = "";
                            int intX = 0;
                            //intTotal = 0;
                            while (intX == 0)
                            {
                                if (intRowCount < gvwMain1.Rows.Count)
                                {
                                    if (intRowCount == gvwMain1.Rows.Count - 1)
                                    {
                                        intJ = 2;
                                        intX = 1;
                                        for (int intMCount = 1; intMCount < 13; intMCount++)
                                        {
                                            dr = dt_new1.NewRow();

                                            dr["Auto"] = ((intAuto[intMCount] / intTotal[intMCount]) * 100).ToString();
                                            dr["Manual"] = ((intManual[intMCount] / intTotal[intMCount]) * 100).ToString();
                                            dr["Idle"] = ((intIdle[intMCount] / intTotal[intMCount]) * 100).ToString();
                                            dr["Total"] = intTotal[intMCount].ToString();
                                            //dr["Deparment"] = gvwMain.Rows[intRowCount].Cells[2].Text;
                                            //int intDay = 0;
                                            //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                            //int intNight = 0;
                                            //intRowCount++;
                                            //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                            //dr["Count"] = (intDay + intNight).ToString();
                                            //intTotal = intDay + intNight;
                                            dt_new1.Rows.Add(dr);
                                        }
                                    }
                                    else
                                    {
                                        if (strAID != ((Label)this.gvwMain1.Rows[intRowCount].Cells[31].FindControl("lblGVSeq")).Text)
                                        {
                                            strAID = ((Label)this.gvwMain1.Rows[intRowCount].Cells[31].FindControl("lblGVSeq")).Text;
                                            intX = 1;
                                            if (intX == 1)
                                            {
                                                for (int intMCount = 1; intMCount < 13; intMCount++)
                                                {
                                                    dr = dt_new1.NewRow();

                                                    dr["Auto"] = ((intAuto[intMCount] / intTotal[intMCount]) * 100).ToString();
                                                    dr["Manual"] = ((intManual[intMCount] / intTotal[intMCount]) * 100).ToString();
                                                    if (intIdle[intMCount] < 0)
                                                        dr["Idle"] = "0";
                                                    else
                                                        dr["Idle"] = ((intIdle[intMCount] / intTotal[intMCount]) * 100).ToString();
                                                    dr["Total"] = intTotal[intMCount].ToString();
                                                    //dr["Deparment"] = gvwMain.Rows[intRowCount].Cells[2].Text;
                                                    //int intDay = 0;
                                                    //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                                    //int intNight = 0;
                                                    //intRowCount++;
                                                    //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                                    //dr["Count"] = (intDay + intNight).ToString();
                                                    //intTotal = intDay + intNight;
                                                    dt_new1.Rows.Add(dr);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            int intMonth1 = 1;
                                            for (int intMCount = 4; intMCount < 28; intMCount++)
                                            {


                                                intAuto[intMonth1] = intAuto[intMonth1] + Convert.ToInt32(gvwMain1.Rows[intRowCount].Cells[intMCount].Text);
                                                intMCount++;
                                                intManual[intMonth1] = intManual[intMonth1] + Convert.ToInt32(gvwMain1.Rows[intRowCount].Cells[intMCount].Text);
                                                int intX1 = DateTime.DaysInMonth(Convert.ToInt32(strYear), intMonth1);
                                                intIdle[intMonth1] = (intX1 * 2) - intAuto[intMonth1] - intManual[intMonth1];
                                                intTotal[intMonth1] = intX1 * 2;
                                                intMonth1++;
                                            }

                                            //if (intX == 1)
                                            //{
                                            //    for (int intMCount = 1; intMCount < 13; intMCount++)
                                            //    {
                                            //        dr = dt_new1.NewRow();

                                            //        dr["Auto"] = intAuto[intMCount].ToString();
                                            //        dr["Manual"] = intManual[intMCount].ToString();
                                            //        dr["Idle"] = intIdle[intMCount].ToString();
                                            //        dr["Total"] = intTotal[intMCount].ToString();
                                            //        //dr["Deparment"] = gvwMain.Rows[intRowCount].Cells[2].Text;
                                            //        //int intDay = 0;
                                            //        //intDay = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                            //        //int intNight = 0;
                                            //        //intRowCount++;
                                            //        //intNight = Convert.ToInt32(gvwMain.Rows[intRowCount].Cells[40].Text);
                                            //        //dr["Count"] = (intDay + intNight).ToString();
                                            //        //intTotal = intDay + intNight;
                                            //        dt_new1.Rows.Add(dr);
                                            //    }
                                            //}
                                            intRowCount++;

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
                        else
                            strAID = ((Label)this.gvwMain1.Rows[intRowCount].Cells[31].FindControl("lblGVSeq")).Text;
                    }
                    //dr = dt_new1.NewRow();

                    //dr["Deparment"] = "Idle";

                    //dr["Count"] = ((TimeTotal.TotalDays + 1) * 2 - intTotal).ToString();


                    //dt_new1.Rows.Add(dr);


                    dsChartData = dt_new1;

                    strScript.Append(@"<script type='text/javascript'>  
                                                google.load('visualization', '1', {packages: ['corechart']}); </script>  
                                                  
                                                <script type='text/javascript'>  
                                                 
                                                function drawChart() {         
                                                var data = google.visualization.arrayToDataTable([  
                                                ['Month', 'Auto', 'Manual', 'Idle'],");
                    int intMonth2 = 1;
                    foreach (DataRow row1 in dsChartData.Rows)
                    {


                        strScript.Append("['" + intMonth2.ToString() + "'," + row1["Auto"] + "," + row1["Manual"] + "," + row1["Idle"] + "],");
                        intMonth2++;
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
                        intAuto[intMonth] = 0;
                        intManual[intMonth] = 0;
                        intIdle[intMonth] = 0;
                    }
                    //================================
                    //TRow.Cells.Add(TCell);
                }
                PieChart.Rows.Add(TRow);

            }
        }
    }

    //    private void BindChart(string strStartDate1, string strEndDate)
    //    {

    //        if (rdoKind1.Checked == true)
    //        {
    //            int intCountChart = 0;
    //            TableRow row;
    //            TableCell cell1;
    //            int intCell = 0;
    //            for (int intJ = 0; intJ < this.gvwMain.Rows.Count; intJ++)
    //            {
    //                //BindChart(strStartDate);
    //                DataTable dt_new1 = new DataTable("dt_new1");
    //                DataColumn column1 = new DataColumn("Deparment");
    //                column1.DataType = System.Type.GetType("System.String");
    //                column1.AllowDBNull = true;
    //                column1.Caption = "Deparment";
    //                column1.DefaultValue = "0";
    //                dt_new1.Columns.Add(column1);

    //                DataColumn column2 = new DataColumn("Count");
    //                column2.DataType = System.Type.GetType("System.String");
    //                column2.AllowDBNull = true;
    //                column2.Caption = "Count";
    //                column2.DefaultValue = "0";
    //                dt_new1.Columns.Add(column2);

    //                string strToday = DateTime.Now.ToString("yyyy/MM/dd");

    //                string strAID = "";
    //                string strAName = "";

    //                DateTime STime = DateTime.Parse(strStartDate1); //起始日
    //                DateTime ETime = DateTime.Parse(strEndDate); //結束日
    //                TimeSpan TimeTotal = ETime.Subtract(STime); //日期相減



    //                DataRow dr;
    //            //for (int intI = 0; intI < dt_new.Rows.Count; intI++)
    //                int intCount = 0;
    //                string strDepartment = "";
    //                int intX = 0;
    //                int intTotal = 0;

    //                if (intJ == 0)
    //                    strAID = ((Label)this.gvwMain.Rows[intJ].Cells[41].FindControl("lblGVSeq")).Text;

    //                if (intJ == gvwMain.Rows.Count - 1)
    //                {
    //                    //row = new TableRow();
    //                    //cell1 = new TableCell();
    //                    //Label lbl1 = new Label();
    //                    //intCountChart++;
    //                    //lbl1.Text = "No." + intCountChart.ToString();

    //                    //Literal liter1 = new Literal();
    //                    //Literal liter2 = new Literal();

    //                    dr = dt_new1.NewRow();

    //                    dr["Deparment"] = gvwMain.Rows[intJ].Cells[2].Text;
    //                    int intDay = 0;
    //                    intDay = Convert.ToInt32(gvwMain.Rows[intJ].Cells[40].Text);
    //                    int intNight = 0;
    //                    //intJ++;
    //                    //intNight = Convert.ToInt32(gvwMain.Rows[intJ].Cells[39].Text);
    //                    dr["Count"] = (intDay + intNight).ToString();
    //                    intTotal = intDay + intNight;
    //                    dt_new1.Rows.Add(dr);
    //                    intJ++;

    //                    intX = 1;
    //                }
    //                else
    //                {
    //                    if (strAID == ((Label)this.gvwMain.Rows[intJ + 1].Cells[41].FindControl("lblGVSeq")).Text)
    //                    {

    //                        strAName = gvwMain.Rows[intJ].Cells[1].Text + "-" + gvwMain.Rows[intJ].Cells[0].Text;

    //                        if ((intCell > 1) || (intJ == 0))
    //                        {
    //                            row = new TableRow();
    //                        }
    //                        intCell++;
    //                        cell1 = new TableCell();
    //                        Label lbl1 = new Label();
    //                        intCountChart++;
    //                        lbl1.Text = "No." + intCountChart.ToString();
    //                        //TableCell cell1 = new TableCell();
    //                        Literal liter1 = new Literal();
    //                        Literal liter2 = new Literal();
    //                        //liter1.Text = "<div id=\"piechart_div\" style=\"border: 1px solid #ccc\"></div>";
    //                        //=============================
    //                        DataTable dsChartData = new DataTable();
    //                        StringBuilder strScript = new StringBuilder();


    //                        intCount = 0;
    //                        strDepartment = "";
    //                        intX = 0;
    //                        intTotal = 0;
    //                        while (intX == 0)
    //                        {
    //                            if (intJ < gvwMain.Rows.Count)
    //                            {
    //                                if (intJ == gvwMain.Rows.Count - 1)
    //                                {
    //                                    if (strAID != ((Label)this.gvwMain.Rows[intJ].Cells[41].FindControl("lblGVSeq")).Text)
    //                                    {
    //                                        strAName = gvwMain.Rows[intJ].Cells[1].Text + "-" + gvwMain.Rows[intJ].Cells[0].Text;
    //                                        dr = dt_new1.NewRow();

    //                                        dr["Deparment"] = "Idle";

    //                                        dr["Count"] = ((TimeTotal.TotalDays + 1) * 2 - intTotal).ToString();


    //                                        dt_new1.Rows.Add(dr);


    //                                        dsChartData = dt_new1;

    //                                        strScript.Append(@"<script type='text/javascript'>  
    //                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
    //                      
    //                    <script type='text/javascript'>  
    //                     
    //                    function drawChart() {         
    //                    var data = google.visualization.arrayToDataTable([  
    //                    ['Task', 'Hours of Day'],");

    //                                        foreach (DataRow row1 in dsChartData.Rows)
    //                                        {
    //                                            strScript.Append("['" + row1["Deparment"] + "'," + row1["Count"] + "],");
    //                                        }
    //                                        strScript.Remove(strScript.Length - 1, 1);
    //                                        strScript.Append("]);");

    //                                        //                    strScript.Append(@" var options = {     
    //                                        //                                    title: 'My Daily Schedule',            
    //                                        //                                    is3D: true,          
    //                                        //                                    };   ");
    //                                        strScript.Append(@" var options = {     
    //                                    title: '" + strAName + @"',            
    //                                    is3D: true,          
    //                                    };   ");

    //                                        strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d" + intCountChart.ToString() + @"'));          
    //                                chart.draw(data, options);        
    //                                }    
    //                                google.setOnLoadCallback(drawChart);  
    //                                ");
    //                                        strScript.Append(" </script>");

    //                                        //ltScripts.Text = strScript.ToString();



    //                                        //=============================

    //                                        cell1.ID = "cell" + intCountChart.ToString();
    //                                        liter1.Text = strScript.ToString();
    //                                        liter1.ID = "liter" + intCountChart.ToString();
    //                                        cell1.Controls.Add(liter1);
    //                                        row.ID = "row" + intCountChart.ToString();
    //                                        row.Cells.Add(cell1);
    //                                        liter2.Text = "<div id=\"piechart_3d" + intCountChart.ToString() + "\" style=\"border: 1px solid #ccc\"></div>";
    //                                        liter2.ID = "literP" + intCountChart.ToString();
    //                                        cell1.Controls.Add(liter2);
    //                                        row.ID = "row" + intCountChart.ToString();
    //                                        row.Cells.Add(cell1);
    //                                        //tableChart.Rows.Add(row);
    //                                    }
    //                                    dr = dt_new1.NewRow();

    //                                    dr["Deparment"] = gvwMain.Rows[intJ].Cells[2].Text;
    //                                    int intDay = 0;
    //                                    intDay = Convert.ToInt32(gvwMain.Rows[intJ].Cells[40].Text);
    //                                    int intNight = 0;
    //                                    //intJ++;
    //                                    //intNight = Convert.ToInt32(gvwMain.Rows[intJ].Cells[39].Text);
    //                                    dr["Count"] = (intDay + intNight).ToString();
    //                                    intTotal = intDay + intNight;
    //                                    dt_new1.Rows.Add(dr);
    //                                    intJ++;

    //                                    intX = 1;
    //                                }
    //                                else
    //                                {
    //                                    if (strAID != ((Label)this.gvwMain.Rows[intJ + 1].Cells[41].FindControl("lblGVSeq")).Text)
    //                                    {
    //                                        intX = 1;
    //                                    }
    //                                    else
    //                                    {
    //                                        dr = dt_new1.NewRow();

    //                                        dr["Deparment"] = gvwMain.Rows[intJ].Cells[2].Text;
    //                                        int intDay = 0;
    //                                        intDay = Convert.ToInt32(gvwMain.Rows[intJ].Cells[40].Text);
    //                                        int intNight = 0;
    //                                        intJ++;
    //                                        intNight = Convert.ToInt32(gvwMain.Rows[intJ].Cells[39].Text);
    //                                        dr["Count"] = (intDay + intNight).ToString();
    //                                        intTotal = intDay + intNight;
    //                                        dt_new1.Rows.Add(dr);
    //                                        intJ++;
    //                                    }
    //                                }
    //                            }
    //                            else
    //                                intX = 1;


    //                        }

    //                        dr = dt_new1.NewRow();

    //                        dr["Deparment"] = "Idle";

    //                        dr["Count"] = ((TimeTotal.TotalDays + 1) * 2 - intTotal).ToString();


    //                        dt_new1.Rows.Add(dr);


    //                        dsChartData = dt_new1;

    //                        strScript.Append(@"<script type='text/javascript'>  
    //                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
    //                      
    //                    <script type='text/javascript'>  
    //                     
    //                    function drawChart() {         
    //                    var data = google.visualization.arrayToDataTable([  
    //                    ['Task', 'Hours of Day'],");

    //                        foreach (DataRow row1 in dsChartData.Rows)
    //                        {
    //                            strScript.Append("['" + row1["Deparment"] + "'," + row1["Count"] + "],");
    //                        }
    //                        strScript.Remove(strScript.Length - 1, 1);
    //                        strScript.Append("]);");

    //                        //                    strScript.Append(@" var options = {     
    //                        //                                    title: 'My Daily Schedule',            
    //                        //                                    is3D: true,          
    //                        //                                    };   ");
    //                        strScript.Append(@" var options = {     
    //                                    title: '" + strAName + @"',            
    //                                    is3D: true,          
    //                                    };   ");

    //                        strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'" + intCountChart.ToString() + @"));          
    //                                chart.draw(data, options);        
    //                                }    
    //                            google.setOnLoadCallback(drawChart);  
    //                            ");
    //                        strScript.Append(" </script>");

    //                        //ltScripts.Text = strScript.ToString();



    //                        //=============================
    //                        row = new TableRow();
    //                        cell1 = new TableCell();
    //                        cell1.ID = "cell" + intCountChart.ToString();
    //                        liter1.Text = strScript.ToString();
    //                        liter1.ID = "liter" + intCountChart.ToString();
    //                        cell1.Controls.Add(liter1);
    //                        row.Cells.Add(cell1);
    //                        liter2.Text = "<div id=\"piechart_3d" + intCountChart.ToString() + "\" style=\"border: 1px solid #ccc\"></div>";
    //                        liter2.ID = "literP" + intCountChart.ToString();
    //                        cell1.Controls.Add(liter2);
    //                        row.ID = "row" + intCountChart.ToString();
    //                        row.Cells.Add(cell1);
    //                        //tableChart.Rows.Add(row);
    //                    }
    //                    tableChart.Rows.Add(row);
    //                }
    //                //}

    //                //if (strAID != dt_new.Rows[intI]["ID"].ToString())
    //                //{
    //                //    //TableRow row = new TableRow();
    //                //    Label lbl1 = new Label();
    //                //    lbl1.Text = "No.1";
    //                //    TableCell cell1 = new TableCell();
    //                //    Literal liter1 = new Literal();
    //                //    //liter1.Text = "<div id=\"piechart_div\" style=\"border: 1px solid #ccc\"></div>";
    //                //    liter1.Text = BindChart(strStartDate, strAID).ToString();
    //                //    cell1.Controls.Add(liter1);
    //                //    row.Cells.Add(cell1);
    //                //    tableChart.Rows.Add(row);

    //                //    strAID = dt_new.Rows[intI]["ID"].ToString();
    //                //}

    //            }

    //        }





    ////        DataTable dsChartData = new DataTable();
    ////        StringBuilder strScript = new StringBuilder();

    ////        //try
    ////        //{
    ////            DataTable dt_new1 = new DataTable("dt_new1");
    ////            DataColumn column1 = new DataColumn("Deparment");
    ////            column1.DataType = System.Type.GetType("System.String");
    ////            column1.AllowDBNull = true;
    ////            column1.Caption = "Deparment";
    ////            column1.DefaultValue = "0";
    ////            dt_new1.Columns.Add(column1);

    ////            DataColumn column2 = new DataColumn("Count");
    ////            column2.DataType = System.Type.GetType("System.String");
    ////            column2.AllowDBNull = true;
    ////            column2.Caption = "Count";
    ////            column2.DefaultValue = "0";
    ////            dt_new1.Columns.Add(column2);

    ////            string strToday = DateTime.Now.ToString("yyyy/MM/dd");

    ////            DataTable dt = clsData.UploadApparatusReportChart(strStartDate1, strToday, "A20171026151701");

    ////            int intCount = 0;
    ////            string strDepartment = "";
    ////            for (int intI = 0; intI < dt.Rows.Count; intI++)
    ////            {
    ////                if (intI == 0)
    ////                {
    ////                    intCount = 1;
    ////                    strDepartment = dt.Rows[0]["Department"].ToString();
    ////                }
    ////                if (strDepartment == dt.Rows[intI]["Department"].ToString())
    ////                {
    ////                    if (intI != 0)
    ////                        intCount++;
    ////                    if (intI == dt.Rows.Count - 1)
    ////                    {
    ////                        DataRow dr = dt_new1.NewRow();

    ////                        dr["Deparment"] = dt.Rows[intI]["Department"].ToString();
    ////                        dr["Count"] = intCount.ToString();

    ////                        dt_new1.Rows.Add(dr);
    ////                    }

    ////                }
    ////                else
    ////                {
    ////                    if (intI == dt.Rows.Count - 1)
    ////                    {
    ////                        DataRow dr = dt_new1.NewRow();
    ////                        dr["Deparment"] = dt.Rows[intI]["Department"].ToString();
    ////                        dr["Count"] = "1";

    ////                        dt_new1.Rows.Add(dr);
    ////                    }
    ////                    else
    ////                    {
    ////                        DataRow dr = dt_new1.NewRow();
    ////                        dr["Deparment"] = dt.Rows[intI]["Department"].ToString();
    ////                        dr["Count"] = intCount.ToString();

    ////                        dt_new1.Rows.Add(dr);

    ////                        strDepartment = dt.Rows[0]["Department"].ToString();
    ////                        intCount = 1;
    ////                    }
    ////                }

    ////            }


    ////            dsChartData = dt_new1;

    ////            strScript.Append(@"<script type='text/javascript'>  
    ////                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
    ////                      
    ////                    <script type='text/javascript'>  
    ////                     
    ////                    function drawChart() {         
    ////                    var data = google.visualization.arrayToDataTable([  
    ////                    ['Task', 'Hours of Day'],");

    ////            foreach (DataRow row in dsChartData.Rows)
    ////            {
    ////                strScript.Append("['" + row["Deparment"] + "'," + row["Count"] + "],");
    ////            }
    ////            strScript.Remove(strScript.Length - 1, 1);
    ////            strScript.Append("]);");

    ////            strScript.Append(@" var options = {     
    ////                                    title: 'My Daily Schedule',            
    ////                                    is3D: true,          
    ////                                    };   ");

    ////            strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));          
    ////                                chart.draw(data, options);        
    ////                                }    
    ////                            google.setOnLoadCallback(drawChart);  
    ////                            ");
    ////            strScript.Append(" </script>");

    ////            //ltScripts.Text = strScript.ToString();
    ////            return strScript;
    //        //}
    //        //catch
    //        //{
    //        //}
    //        //finally
    //        //{
    //        //    dsChartData.Dispose();

    //        //}
    //    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {
        int intN;
        if (((txtYearA.Text == "2018") && ((ddlMonthA.Text == "11") || (ddlMonthA.Text == "12"))) || (Convert.ToInt32(txtYearA.Text) > 2018))
            intN = 41;
        else
            intN = 6;

        for (int intI = 0; intI < intN; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {

                    if ((gvItem.Cells[0].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[0].Text.Trim()) && (gvItem.Cells[1].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[1].Text.Trim()) && (gvItem.Cells[2].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[2].Text.Trim()) && (gvItem.Cells[3].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[3].Text.Trim()) && (gvItem.Cells[4].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[4].Text.Trim()) && (gvItem.Cells[5].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[5].Text.Trim()))
                    //if ((gvItem.Cells[0].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[0].Text.Trim()) && (gvItem.Cells[1].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[1].Text.Trim()) && (gvItem.Cells[2].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[2].Text.Trim()) && (gvItem.Cells[3].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[3].Text.Trim()) && (gvItem.Cells[4].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[4].Text.Trim()))
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
            if (e.Row.Cells[6].Text == "D")
                e.Row.Cells[6].Text = "Day";
            else if (e.Row.Cells[6].Text == "N")
                e.Row.Cells[6].Text = "Night";
            else
                e.Row.Cells[6].Text = "";
        }
        if (((txtYearA.Text == "2018") && ((ddlMonthA.Text == "11") || (ddlMonthA.Text == "12"))) || (Convert.ToInt32(txtYearA.Text) > 2018))
        {
            e.Row.Cells[6].Visible = false;
        }

    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[0].Visible = false;
        //e.Row.Cells[1].Visible = false;
    }

    #region gvwMain1_PreRender
    protected void gvwMain1_PreRender(object sender, EventArgs e)
    {
        int intN;
        if (((txtYearS.Text == "2018") && ((ddlMonthS.Text == "11") || (ddlMonthS.Text == "12"))) || (Convert.ToInt32(txtYearS.Text) > 2018))
            intN = 31;
        else
            intN = 3;


        for (int intI = 0; intI < intN; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain1.Rows)
            {
                if (gvItem.RowIndex != 0)
                {

                    if ((gvItem.Cells[0].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[0].Text.Trim()) && (gvItem.Cells[1].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[1].Text.Trim()) && (gvItem.Cells[2].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[2].Text.Trim()))
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
            if (e.Row.Cells[3].Text == "D")
                e.Row.Cells[3].Text = "Day";
            else if (e.Row.Cells[3].Text == "N")
                e.Row.Cells[3].Text = "Night";
            else
                e.Row.Cells[3].Text = "";
        }
        if (((txtYearS.Text == "2018") && ((ddlMonthS.Text == "11") || (ddlMonthS.Text == "12"))) || (Convert.ToInt32(txtYearS.Text) > 2018))
        {
            //e.Row.Cells[3].Visible = false;
        }
    }

    protected void gvwMain1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[0].Visible = false;
        //e.Row.Cells[1].Visible = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TableCellCollection oldCell = e.Row.Cells;
            oldCell.Clear();

            #region 第一列
            //多重表頭的第一列
            GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //第一欄
            TableCell tc = new TableCell();
            tc.Text = "Equip";
            //tc.BackColor = System.Drawing.Color.AliceBlue; //背景色彩
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.VerticalAlign = VerticalAlign.Middle;
            tc.Width = 180;
            tc.RowSpan = 2; //所跨的row數
            tc.ColumnSpan = 1; //所跨的column數
            gvRow.Cells.Add(tc); //新增

            //第二欄
            tc = new TableCell();
            tc.Text = "Asset Number";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 100;
            tc.RowSpan = 2;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第三欄
            tc = new TableCell();
            tc.Text = "Department";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 100;
            tc.RowSpan = 2;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第四欄
            tc = new TableCell();
            tc.Text = "Period";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 2;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第五欄
            tc = new TableCell();
            tc.Text = "1";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第六欄
            tc = new TableCell();
            tc.Text = "2";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "3";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "4";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "5";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "6";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "7";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "8";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "9";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "10";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "11";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "12";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 1;
            tc.ColumnSpan = 2;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Auto";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 2;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Manual";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 2;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Total";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 80;
            tc.RowSpan = 2;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //新增至GridView
            gvwMain1.Controls[0].Controls.Add(gvRow);

            #endregion

            #region 第二列

            //多重表頭的第二列
            gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "A";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "M";
            tc.Width = 40;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            gvwMain1.Controls[0].Controls.Add(gvRow);

            #endregion
        }
    }

    //#region gvwMain1_PreRender
    //protected void gvwMain1_PreRender(object sender, EventArgs e)
    //{
    //    for (int intI = 0; intI < 6; intI++)
    //    {
    //        int i = 1;
    //        foreach (GridViewRow gvItem in gvwMain1.Rows)
    //        {
    //            if (gvItem.RowIndex != 0)
    //            {

    //                if ((gvItem.Cells[0].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[0].Text.Trim()) && (gvItem.Cells[1].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[1].Text.Trim()) && (gvItem.Cells[2].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[2].Text.Trim()) && (gvItem.Cells[3].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[3].Text.Trim()) && (gvItem.Cells[4].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[4].Text.Trim()) && (gvItem.Cells[5].Text.Trim() == gvwMain1.Rows[(gvItem.RowIndex - 1)].Cells[5].Text.Trim()))
    //                {
    //                    gvwMain1.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
    //                    gvItem.Cells[intI].Visible = false;
    //                    i = i + 1;

    //                }
    //                else
    //                {
    //                    gvwMain1.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
    //                    i = 1;
    //                }

    //            }
    //            else
    //                gvItem.Cells[intI].RowSpan = 1;
    //        }
    //    }
    //}
    //#endregion

    //protected void gvwMain1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (e.Row.Cells[6].Text == "D")
    //            e.Row.Cells[6].Text = "Day";
    //        else if (e.Row.Cells[6].Text == "N")
    //            e.Row.Cells[6].Text = "Night";
    //        else
    //            e.Row.Cells[6].Text = "";
    //    }
    //}

    //protected void gvwMain1_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    //e.Row.Cells[0].Visible = false;
    //    //e.Row.Cells[1].Visible = false;
    //}

    private void getData()
    {
        string strStartDate = "";
        string strEndDate = "";

        if (rdoMonth.Checked == true)
        {
            strStartDate = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";


            if ((txtYearS.Text == "") || (txtYearE.Text == ""))
                clsMsg.AlertMessage("請輸入日期區間！", this.Page);
            else
            {
                if ((Convert.ToInt32(ddlMonthE.Text) < 9))
                {
                    if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
                    {
                        if (ddlMonthE.Text == "02")
                        {
                            if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                            else
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                        }
                        else
                        {
                            if (ddlMonthE.Text == "08")
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                            else
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                        }
                    }
                    else
                        strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                }
                else
                {
                    if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                        strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                    else
                        strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                }


            }
        }
        else
        {
            if (strStart != "")
            {
                strStart = Request["date1"].ToString();
                DateTime dt = Convert.ToDateTime(strStart);

                strStartDate = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

                strEndDate = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

            }
            else
                clsMsg.AlertMessage("請輸入日期！", this.Page);
        }

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Products_ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Products_ID";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Department");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Department";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("PU");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "PU";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Customer");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Customer";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("ModelName");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "ModelName";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Period");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Period";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("UseKind");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "UseKind";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("D1");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "D1";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("D2");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "D2";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("D3");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "D3";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("D4");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "D4";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("D5");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "D5";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("D6");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "D6";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);

        DataColumn column15 = new DataColumn("D7");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "D7";
        column15.DefaultValue = "0";
        dt_new.Columns.Add(column15);

        DataColumn column16 = new DataColumn("D8");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "D8";
        column16.DefaultValue = "0";
        dt_new.Columns.Add(column16);

        DataColumn column17 = new DataColumn("D9");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "D9";
        column17.DefaultValue = "0";
        dt_new.Columns.Add(column17);

        DataColumn column18 = new DataColumn("D10");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "D10";
        column18.DefaultValue = "0";
        dt_new.Columns.Add(column18);

        DataColumn column19 = new DataColumn("D11");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "D11";
        column19.DefaultValue = "0";
        dt_new.Columns.Add(column19);

        DataColumn column20 = new DataColumn("D12");
        column20.DataType = System.Type.GetType("System.String");
        column20.AllowDBNull = true;
        column20.Caption = "D12";
        column20.DefaultValue = "0";
        dt_new.Columns.Add(column20);

        DataColumn column21 = new DataColumn("D13");
        column21.DataType = System.Type.GetType("System.String");
        column21.AllowDBNull = true;
        column21.Caption = "D13";
        column21.DefaultValue = "0";
        dt_new.Columns.Add(column21);

        DataColumn column22 = new DataColumn("D14");
        column22.DataType = System.Type.GetType("System.String");
        column22.AllowDBNull = true;
        column22.Caption = "D14";
        column22.DefaultValue = "0";
        dt_new.Columns.Add(column22);

        DataColumn column23 = new DataColumn("D15");
        column23.DataType = System.Type.GetType("System.String");
        column23.AllowDBNull = true;
        column23.Caption = "D15";
        column23.DefaultValue = "0";
        dt_new.Columns.Add(column23);

        DataColumn column24 = new DataColumn("D16");
        column24.DataType = System.Type.GetType("System.String");
        column24.AllowDBNull = true;
        column24.Caption = "D16";
        column24.DefaultValue = "0";
        dt_new.Columns.Add(column24);

        DataColumn column25 = new DataColumn("D17");
        column25.DataType = System.Type.GetType("System.String");
        column25.AllowDBNull = true;
        column25.Caption = "D17";
        column25.DefaultValue = "0";
        dt_new.Columns.Add(column25);

        DataColumn column26 = new DataColumn("D18");
        column26.DataType = System.Type.GetType("System.String");
        column26.AllowDBNull = true;
        column26.Caption = "D18";
        column26.DefaultValue = "0";
        dt_new.Columns.Add(column26);

        DataColumn column27 = new DataColumn("D19");
        column27.DataType = System.Type.GetType("System.String");
        column27.AllowDBNull = true;
        column27.Caption = "D19";
        column27.DefaultValue = "0";
        dt_new.Columns.Add(column27);

        DataColumn column28 = new DataColumn("D20");
        column28.DataType = System.Type.GetType("System.String");
        column28.AllowDBNull = true;
        column28.Caption = "D20";
        column28.DefaultValue = "0";
        dt_new.Columns.Add(column28);

        DataColumn column29 = new DataColumn("D21");
        column29.DataType = System.Type.GetType("System.String");
        column29.AllowDBNull = true;
        column29.Caption = "D21";
        column29.DefaultValue = "0";
        dt_new.Columns.Add(column29);

        DataColumn column30 = new DataColumn("D22");
        column30.DataType = System.Type.GetType("System.String");
        column30.AllowDBNull = true;
        column30.Caption = "D22";
        column30.DefaultValue = "0";
        dt_new.Columns.Add(column30);

        DataColumn column31 = new DataColumn("D23");
        column31.DataType = System.Type.GetType("System.String");
        column31.AllowDBNull = true;
        column31.Caption = "D23";
        column31.DefaultValue = "0";
        dt_new.Columns.Add(column31);

        DataColumn column32 = new DataColumn("D24");
        column32.DataType = System.Type.GetType("System.String");
        column32.AllowDBNull = true;
        column32.Caption = "D24";
        column32.DefaultValue = "0";
        dt_new.Columns.Add(column32);

        DataColumn column33 = new DataColumn("D25");
        column33.DataType = System.Type.GetType("System.String");
        column33.AllowDBNull = true;
        column33.Caption = "D25";
        column33.DefaultValue = "0";
        dt_new.Columns.Add(column33);

        DataColumn column34 = new DataColumn("D26");
        column34.DataType = System.Type.GetType("System.String");
        column34.AllowDBNull = true;
        column34.Caption = "D26";
        column34.DefaultValue = "0";
        dt_new.Columns.Add(column34);

        DataColumn column35 = new DataColumn("D27");
        column35.DataType = System.Type.GetType("System.String");
        column35.AllowDBNull = true;
        column35.Caption = "D27";
        column35.DefaultValue = "0";
        dt_new.Columns.Add(column35);

        DataColumn column36 = new DataColumn("D28");
        column36.DataType = System.Type.GetType("System.String");
        column36.AllowDBNull = true;
        column36.Caption = "D28";
        column36.DefaultValue = "0";
        dt_new.Columns.Add(column36);

        DataColumn column37 = new DataColumn("D29");
        column37.DataType = System.Type.GetType("System.String");
        column37.AllowDBNull = true;
        column37.Caption = "D29";
        column37.DefaultValue = "0";
        dt_new.Columns.Add(column37);

        DataColumn column38 = new DataColumn("D30");
        column38.DataType = System.Type.GetType("System.String");
        column38.AllowDBNull = true;
        column38.Caption = "D30";
        column38.DefaultValue = "0";
        dt_new.Columns.Add(column38);

        DataColumn column39 = new DataColumn("D31");
        column39.DataType = System.Type.GetType("System.String");
        column39.AllowDBNull = true;
        column39.Caption = "D31";
        column39.DefaultValue = "0";
        dt_new.Columns.Add(column39);

        DataColumn column40 = new DataColumn("Auto");
        column40.DataType = System.Type.GetType("System.String");
        column40.AllowDBNull = true;
        column40.Caption = "Auto";
        column40.DefaultValue = "0";
        dt_new.Columns.Add(column40);

        DataColumn column41 = new DataColumn("Manual");
        column41.DataType = System.Type.GetType("System.String");
        column41.AllowDBNull = true;
        column41.Caption = "Manual";
        column41.DefaultValue = "0";
        dt_new.Columns.Add(column41);

        DataColumn column42 = new DataColumn("Total");
        column42.DataType = System.Type.GetType("System.String");
        column42.AllowDBNull = true;
        column42.Caption = "Total";
        column42.DefaultValue = "0";
        dt_new.Columns.Add(column42);

        DataTable dt1;

        if (rdoDepartment.Checked == true)
        {
            if (rdoCustodian.Checked == true)
                dt1 = clsData.UploadApparatusReport("0", ddlDepartment.Text, ddlCustodian.Text, strStartDate, strEndDate);
            else
                dt1 = clsData.UploadApparatusReport("1", ddlDepartment.Text, ddlKind.Text, strStartDate, strEndDate);

        }
        else if (rdoProducts_ID.Checked == true)
            dt1 = clsData.UploadApparatusReport("2", txtProducts_ID.Text, "", strStartDate, strEndDate);
        else
            dt1 = clsData.UploadApparatusReport("3", ddlKind1.Text, "", strStartDate, strEndDate);

        DataRow dr;
        string strName = "";
        string strProducts_ID = "";
        string strCustomer = "";
        string strDepartment = "";
        string strGName = "";
        string strPeriod = "";
        string[] strDay = new string[32];
        int intAuto = 0;
        int intManual = 0;
        DateTime startDate;
        DateTime endDate;

        for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
        {
            if ((intJ == 0) || ((strName != dt1.Rows[intJ]["Name"].ToString()) || (strProducts_ID != dt1.Rows[intJ]["Products_ID"].ToString()) || (strCustomer != dt1.Rows[intJ]["Customer"].ToString()) || (strDepartment != dt1.Rows[intJ]["Department"].ToString()) || (strGName != dt1.Rows[intJ]["GName"].ToString()) || (strPeriod != dt1.Rows[intJ]["Period"].ToString())))
            {
                if (intJ != 0)
                {
                    dr = dt_new.NewRow();


                    dr["Name"] = strName;
                    dr["Products_ID"] = strProducts_ID;
                    int intIndex, intIndex1;
                    intIndex = strCustomer.IndexOf("(");
                    if (intIndex < 0)
                        dr["Customer"] = strCustomer;
                    else
                        dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                    string strDepartment2;


                    intIndex = strDepartment.IndexOf("(");
                    intIndex1 = strDepartment.IndexOf(")");
                    if (intIndex > 0)
                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                    else
                        strDepartment2 = strDepartment;

                    dr["Department"] = strDepartment2;

                    string[] sArray = strDepartment.Split('-');
                    int intU = 0;
                    foreach (string l in sArray)
                    {
                        intU++;
                    }
                    if (intU == 2)
                        dr["PU"] = sArray[1].Replace("PU", "");
                    else
                        dr["PU"] = sArray[0].Replace("PU", "");

                    dr["ModelName"] = strGName;
                    dr["Period"] = strPeriod;

                    if ((intAuto + intManual) == 0)
                    {
                        startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                        endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                        while (startDate <= endDate)
                        {
                            string strDateW;

                            strDateW = startDate.Day.ToString();

                            strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                            startDate = startDate.AddDays(1);

                            if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                intAuto++;
                            else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                intManual++;

                        }
                    }

                    for (intU = 1; intU < 32; intU++)
                    {
                        dr["D" + intU.ToString()] = strDay[intU];
                    }

                    dr["Auto"] = intAuto.ToString();
                    dr["Manual"] = intManual.ToString();
                    dr["Total"] = (intAuto + intManual).ToString();

                    dt_new.Rows.Add(dr);


                    intAuto = 0;
                    intManual = 0;
                    for (intU = 1; intU < 32; intU++)
                    {
                        strDay[intU] = "";
                    }

                    strName = dt1.Rows[intJ]["Name"].ToString();
                    strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
                    strCustomer = dt1.Rows[intJ]["Customer"].ToString();
                    strDepartment = dt1.Rows[intJ]["Department"].ToString();
                    strGName = dt1.Rows[intJ]["GName"].ToString();
                    strPeriod = dt1.Rows[intJ]["Period"].ToString();

                    startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                    endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                    while (startDate <= endDate)
                    {
                        string strDateW;

                        strDateW = startDate.Day.ToString();

                        strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                        startDate = startDate.AddDays(1);

                        if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                            intAuto++;
                        else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                            intManual++;

                    }

                    if (intJ == dt1.Rows.Count - 1)
                    {
                        dr = dt_new.NewRow();

                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Products_ID"] = dt1.Rows[intJ]["Products_ID"].ToString();
                        //int intIndex, intIndex1;
                        intIndex = dt1.Rows[intJ]["Customer"].ToString().IndexOf("(");
                        if (intIndex < 0)
                            dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString();
                        else
                            dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString().Substring(1, intIndex - 1);


                        //string strDepartment2;


                        intIndex = dt1.Rows[intJ]["Department"].ToString().IndexOf("(");
                        intIndex1 = dt1.Rows[intJ]["Department"].ToString().IndexOf(")");
                        if (intIndex > 0)
                            strDepartment2 = dt1.Rows[intJ]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                        else
                            strDepartment2 = dt1.Rows[intJ]["Department"].ToString();

                        dr["Department"] = strDepartment2;

                        sArray = dt1.Rows[intJ]["Department"].ToString().Split('-');
                        intU = 0;
                        foreach (string l in sArray)
                        {
                            intU++;
                        }
                        if (intU == 2)
                            dr["PU"] = sArray[1].Replace("PU", "");
                        else
                            dr["PU"] = sArray[0].Replace("PU", "");

                        dr["ModelName"] = dt1.Rows[intJ]["GName"].ToString();
                        dr["Period"] = dt1.Rows[intJ]["Period"].ToString();

                        //startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                        //endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                        //while (startDate < endDate)
                        //{
                        //    string strDateW;

                        //    strDateW = startDate.Day.ToString();

                        //    dr["D" + intU.ToString()] = dt1.Rows[intJ]["UseKind"].ToString();

                        //    startDate = startDate.AddDays(1);

                        //    if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                        //        intAuto++;
                        //    else
                        //        intManual++;

                        //}

                        for (intU = 1; intU < 32; intU++)
                        {
                            dr["D" + intU.ToString()] = strDay[intU];
                        }
                        dr["Auto"] = intAuto.ToString();
                        dr["Manual"] = intManual.ToString();
                        dr["Total"] = (intAuto + intManual).ToString();

                        dt_new.Rows.Add(dr);
                        intAuto = 0;
                        intManual = 0;
                        for (intU = 1; intU < 32; intU++)
                        {
                            strDay[intU] = "";
                        }

                    }

                }
                else
                {
                    strName = dt1.Rows[intJ]["Name"].ToString();
                    strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
                    strCustomer = dt1.Rows[intJ]["Customer"].ToString();
                    strDepartment = dt1.Rows[intJ]["Department"].ToString();
                    strGName = dt1.Rows[intJ]["GName"].ToString();
                    strPeriod = dt1.Rows[intJ]["Period"].ToString();

                    startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                    endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                    while (startDate < endDate)
                    {
                        string strDateW;

                        strDateW = startDate.Day.ToString();

                        strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                        startDate = startDate.AddDays(1);

                        if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                            intAuto++;
                        else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                            intManual++;

                    }
                }
            }
            else if ((strName == dt1.Rows[intJ]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ]["Department"].ToString()) && (strGName == dt1.Rows[intJ]["GName"].ToString()) && (strPeriod == dt1.Rows[intJ]["Period"].ToString()))
            {
                startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                while (startDate < endDate)
                {
                    string strDateW;

                    strDateW = startDate.Day.ToString();

                    strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                    startDate = startDate.AddDays(1);

                    if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                        intAuto++;
                    else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                        intManual++;

                }

                if (intJ == dt1.Rows.Count - 1)
                {
                    dr = dt_new.NewRow();


                    dr["Name"] = strName;
                    dr["Products_ID"] = strProducts_ID;
                    int intIndex, intIndex1;
                    intIndex = strCustomer.IndexOf("(");
                    if (intIndex < 0)
                        dr["Customer"] = strCustomer;
                    else
                        dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                    string strDepartment2;


                    intIndex = strDepartment.IndexOf("(");
                    intIndex1 = strDepartment.IndexOf(")");
                    if (intIndex > 0)
                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                    else
                        strDepartment2 = strDepartment;

                    dr["Department"] = strDepartment2;

                    string[] sArray = strDepartment.Split('-');
                    int intU = 0;
                    foreach (string l in sArray)
                    {
                        intU++;
                    }
                    if (intU == 2)
                        dr["PU"] = sArray[1].Replace("PU", "");
                    else
                        dr["PU"] = sArray[0].Replace("PU", "");

                    dr["ModelName"] = strGName;
                    dr["Period"] = strPeriod;

                    for (intU = 1; intU < 32; intU++)
                    {
                        dr["D" + intU.ToString()] = strDay[intU];
                    }
                    dr["Auto"] = intAuto.ToString();
                    dr["Manual"] = intManual.ToString();
                    dr["Total"] = (intAuto + intManual).ToString();

                    dt_new.Rows.Add(dr);
                    intAuto = 0;
                    intManual = 0;
                    for (intU = 1; intU < 32; intU++)
                    {
                        strDay[intU] = "";
                    }
                }
            }

        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
    }

    //private void getDataMonth()
    //{
    //    string strStartDate = "";
    //    string strEndDate = "";

    //    if (rdoMonth.Checked == true)
    //    {
    //        strStartDate = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";


    //        if ((txtYearS.Text == "") || (txtYearE.Text == ""))
    //            clsMsg.AlertMessage("請輸入日期區間！", this.Page);
    //        else
    //        {
    //            if ((Convert.ToInt32(ddlMonthE.Text) < 9))
    //            {
    //                if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
    //                {
    //                    if (ddlMonthE.Text == "02")
    //                    {
    //                        if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
    //                            strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
    //                        else
    //                            strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
    //                    }
    //                    else
    //                    {
    //                        if (ddlMonthE.Text == "08")
    //                            strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
    //                        else
    //                            strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
    //                    }
    //                }
    //                else
    //                    strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
    //            }
    //            else
    //            {
    //                if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
    //                    strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
    //                else
    //                    strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
    //            }


    //        }
    //    }
    //    else
    //    {
    //        if (strStart != "")
    //        {
    //            strStart = Request["date1"].ToString();
    //            DateTime dt = Convert.ToDateTime(strStart);

    //            strStartDate = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

    //            strEndDate = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

    //        }
    //        else
    //            clsMsg.AlertMessage("請輸入日期！", this.Page);
    //    }

    //    DataTable dt_new = new DataTable("dt_new");

    //    DataColumn column1 = new DataColumn("Name");
    //    column1.DataType = System.Type.GetType("System.String");
    //    column1.AllowDBNull = true;
    //    column1.Caption = "Name";
    //    column1.DefaultValue = "0";
    //    dt_new.Columns.Add(column1);

    //    DataColumn column2 = new DataColumn("Products_ID");
    //    column2.DataType = System.Type.GetType("System.String");
    //    column2.AllowDBNull = true;
    //    column2.Caption = "Products_ID";
    //    column2.DefaultValue = "0";
    //    dt_new.Columns.Add(column2);

    //    DataColumn column3 = new DataColumn("Department");
    //    column3.DataType = System.Type.GetType("System.String");
    //    column3.AllowDBNull = true;
    //    column3.Caption = "Department";
    //    column3.DefaultValue = "0";
    //    dt_new.Columns.Add(column3);

    //    DataColumn column4 = new DataColumn("PU");
    //    column4.DataType = System.Type.GetType("System.String");
    //    column4.AllowDBNull = true;
    //    column4.Caption = "PU";
    //    column4.DefaultValue = "0";
    //    dt_new.Columns.Add(column4);

    //    DataColumn column5 = new DataColumn("Customer");
    //    column5.DataType = System.Type.GetType("System.String");
    //    column5.AllowDBNull = true;
    //    column5.Caption = "Customer";
    //    column5.DefaultValue = "0";
    //    dt_new.Columns.Add(column5);

    //    DataColumn column6 = new DataColumn("ModelName");
    //    column6.DataType = System.Type.GetType("System.String");
    //    column6.AllowDBNull = true;
    //    column6.Caption = "ModelName";
    //    column6.DefaultValue = "0";
    //    dt_new.Columns.Add(column6);

    //    DataColumn column7 = new DataColumn("Period");
    //    column7.DataType = System.Type.GetType("System.String");
    //    column7.AllowDBNull = true;
    //    column7.Caption = "Period";
    //    column7.DefaultValue = "0";
    //    dt_new.Columns.Add(column7);

    //    DataColumn column8 = new DataColumn("UseKind");
    //    column8.DataType = System.Type.GetType("System.String");
    //    column8.AllowDBNull = true;
    //    column8.Caption = "UseKind";
    //    column8.DefaultValue = "0";
    //    dt_new.Columns.Add(column8);

    //    DataColumn column9 = new DataColumn("M1");
    //    column9.DataType = System.Type.GetType("System.String");
    //    column9.AllowDBNull = true;
    //    column9.Caption = "M1";
    //    column9.DefaultValue = "0";
    //    dt_new.Columns.Add(column9);

    //    DataColumn column10 = new DataColumn("M2");
    //    column10.DataType = System.Type.GetType("System.String");
    //    column10.AllowDBNull = true;
    //    column10.Caption = "M2";
    //    column10.DefaultValue = "0";
    //    dt_new.Columns.Add(column10);

    //    DataColumn column11 = new DataColumn("M3");
    //    column11.DataType = System.Type.GetType("System.String");
    //    column11.AllowDBNull = true;
    //    column11.Caption = "M3";
    //    column11.DefaultValue = "0";
    //    dt_new.Columns.Add(column11);

    //    DataColumn column12 = new DataColumn("M4");
    //    column12.DataType = System.Type.GetType("System.String");
    //    column12.AllowDBNull = true;
    //    column12.Caption = "M4";
    //    column12.DefaultValue = "0";
    //    dt_new.Columns.Add(column12);

    //    DataColumn column13 = new DataColumn("M5");
    //    column13.DataType = System.Type.GetType("System.String");
    //    column13.AllowDBNull = true;
    //    column13.Caption = "M5";
    //    column13.DefaultValue = "0";
    //    dt_new.Columns.Add(column13);

    //    DataColumn column14 = new DataColumn("M6");
    //    column14.DataType = System.Type.GetType("System.String");
    //    column14.AllowDBNull = true;
    //    column14.Caption = "M6";
    //    column14.DefaultValue = "0";
    //    dt_new.Columns.Add(column14);

    //    DataColumn column15 = new DataColumn("M7");
    //    column15.DataType = System.Type.GetType("System.String");
    //    column15.AllowDBNull = true;
    //    column15.Caption = "M7";
    //    column15.DefaultValue = "0";
    //    dt_new.Columns.Add(column15);

    //    DataColumn column16 = new DataColumn("M8");
    //    column16.DataType = System.Type.GetType("System.String");
    //    column16.AllowDBNull = true;
    //    column16.Caption = "M8";
    //    column16.DefaultValue = "0";
    //    dt_new.Columns.Add(column16);

    //    DataColumn column17 = new DataColumn("M9");
    //    column17.DataType = System.Type.GetType("System.String");
    //    column17.AllowDBNull = true;
    //    column17.Caption = "M9";
    //    column17.DefaultValue = "0";
    //    dt_new.Columns.Add(column17);

    //    DataColumn column18 = new DataColumn("M10");
    //    column18.DataType = System.Type.GetType("System.String");
    //    column18.AllowDBNull = true;
    //    column18.Caption = "M10";
    //    column18.DefaultValue = "0";
    //    dt_new.Columns.Add(column18);

    //    DataColumn column19 = new DataColumn("M11");
    //    column19.DataType = System.Type.GetType("System.String");
    //    column19.AllowDBNull = true;
    //    column19.Caption = "M11";
    //    column19.DefaultValue = "0";
    //    dt_new.Columns.Add(column19);

    //    DataColumn column20 = new DataColumn("M12");
    //    column20.DataType = System.Type.GetType("System.String");
    //    column20.AllowDBNull = true;
    //    column20.Caption = "M12";
    //    column20.DefaultValue = "0";
    //    dt_new.Columns.Add(column20);

    //    DataColumn column21 = new DataColumn("Auto");
    //    column21.DataType = System.Type.GetType("System.String");
    //    column21.AllowDBNull = true;
    //    column21.Caption = "Auto";
    //    column21.DefaultValue = "0";
    //    dt_new.Columns.Add(column21);

    //    DataColumn column22 = new DataColumn("Manual");
    //    column22.DataType = System.Type.GetType("System.String");
    //    column22.AllowDBNull = true;
    //    column22.Caption = "Manual";
    //    column22.DefaultValue = "0";
    //    dt_new.Columns.Add(column22);

    //    DataColumn column23 = new DataColumn("Total");
    //    column23.DataType = System.Type.GetType("System.String");
    //    column23.AllowDBNull = true;
    //    column23.Caption = "Total";
    //    column23.DefaultValue = "0";
    //    dt_new.Columns.Add(column23);

    //    DataColumn column24 = new DataColumn("ID");
    //    column24.DataType = System.Type.GetType("System.String");
    //    column24.AllowDBNull = true;
    //    column24.Caption = "ID";
    //    column24.DefaultValue = "0";
    //    dt_new.Columns.Add(column24);

    //    DataTable dt1;

    //    if (rdoDepartment.Checked == true)
    //    {
    //        if (rdoCustodian.Checked == true)
    //            dt1 = clsData.UploadApparatusReport("0", ddlDepartment.Text, ddlCustodian.Text, strStartDate, strEndDate);
    //        else
    //            dt1 = clsData.UploadApparatusReport("1", ddlDepartment.Text, ddlKind.Text, strStartDate, strEndDate);

    //    }
    //    else if (rdoProducts_ID.Checked == true)
    //        dt1 = clsData.UploadApparatusReport("2", txtProducts_ID.Text, "", strStartDate, strEndDate);
    //    else
    //        dt1 = clsData.UploadApparatusReport("3", ddlKind1.Text, "", strStartDate, strEndDate);

    //    DataRow dr;
    //    string strName = "";
    //    string strProducts_ID = "";
    //    string strCustomer = "";
    //    string strDepartment = "";
    //    string strGName = "";
    //    string strPeriod = "";
    //    string strID = "";
    //    string[] strDay = new string[32];
    //    int intAuto = 0;
    //    int intManual = 0;
    //    DateTime startDate;
    //    DateTime endDate;

    //    for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
    //    {
    //        DataTable dt2 = clsData.UploadApparatusReportM(dt1.Rows[intJ]["ID"].ToString(), strStartDate, strEndDate);

    //        for (int intACount = 0; intACount < dt2.Rows.Count; intACount++)
    //        {
    //            if (intJ == 0)
    //            {
    //                strName = dt1.Rows[intJ]["Name"].ToString();
    //                strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
    //                strCustomer = dt1.Rows[intJ]["Customer"].ToString();
    //                strDepartment = dt1.Rows[intJ]["Department"].ToString();
    //                strGName = dt1.Rows[intJ]["GName"].ToString();
    //                strPeriod = dt1.Rows[intJ]["Period"].ToString();
    //                strID = dt1.Rows[intJ]["ID"].ToString();

    //                if (strPeriod == "N")
    //                {
    //                    dr = dt_new.NewRow();

    //                    dr["Name"] = strName;
    //                    dr["Products_ID"] = strProducts_ID;
    //                    int intIndex, intIndex1;
    //                    intIndex = strCustomer.IndexOf("(");
    //                    if (intIndex < 0)
    //                        dr["Customer"] = strCustomer;
    //                    else
    //                        dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


    //                    string strDepartment2;


    //                    intIndex = strDepartment.IndexOf("(");
    //                    intIndex1 = strDepartment.IndexOf(")");
    //                    if (intIndex > 0)
    //                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                    else
    //                        strDepartment2 = strDepartment;

    //                    dr["Department"] = strDepartment2;

    //                    string[] sArray = strDepartment.Split('-');
    //                    int intU = 0;
    //                    foreach (string l in sArray)
    //                    {
    //                        intU++;
    //                    }
    //                    if (intU == 2)
    //                        dr["PU"] = sArray[1].Replace("PU", "");
    //                    else
    //                        dr["PU"] = sArray[0].Replace("PU", "");

    //                    dr["ModelName"] = strGName;
    //                    dr["Period"] = "D";
    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        dr["D" + intU.ToString()] = "";
    //                    }
    //                    dr["Auto"] = "0";
    //                    dr["Manual"] = "0";
    //                    dr["Total"] = "0";
    //                    dr["ID"] = strID;

    //                    dt_new.Rows.Add(dr);

    //                    intAuto = 0;
    //                    intManual = 0;
    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        strDay[intU] = "";
    //                    }


    //                }

    //                startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
    //                endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
    //                while (startDate <= endDate)
    //                {
    //                    string strDateW;

    //                    strDateW = startDate.Day.ToString();

    //                    strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

    //                    startDate = startDate.AddDays(1);

    //                    if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
    //                        intAuto++;
    //                    else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
    //                        intManual++;

    //                }


    //            }
    //            else
    //            {



    //                if ((strName == dt1.Rows[intJ]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ]["Department"].ToString()) && (strGName == dt1.Rows[intJ]["GName"].ToString()))
    //                {
    //                    if (strPeriod == dt1.Rows[intJ]["Period"].ToString())
    //                    {
    //                        startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
    //                        endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
    //                        while (startDate <= endDate)
    //                        {
    //                            string strDateW;

    //                            strDateW = startDate.Day.ToString();

    //                            strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

    //                            startDate = startDate.AddDays(1);

    //                            if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
    //                                intAuto++;
    //                            else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
    //                                intManual++;

    //                        }
    //                    }
    //                    else
    //                    {
    //                        dr = dt_new.NewRow();


    //                        dr["Name"] = strName;
    //                        dr["Products_ID"] = strProducts_ID;
    //                        int intIndex, intIndex1;
    //                        intIndex = strCustomer.IndexOf("(");
    //                        if (intIndex < 0)
    //                            dr["Customer"] = strCustomer;
    //                        else
    //                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


    //                        string strDepartment2;


    //                        intIndex = strDepartment.IndexOf("(");
    //                        intIndex1 = strDepartment.IndexOf(")");
    //                        if (intIndex > 0)
    //                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                        else
    //                            strDepartment2 = strDepartment;

    //                        dr["Department"] = strDepartment2;

    //                        string[] sArray = strDepartment.Split('-');
    //                        int intU = 0;
    //                        foreach (string l in sArray)
    //                        {
    //                            intU++;
    //                        }
    //                        if (intU == 2)
    //                            dr["PU"] = sArray[1].Replace("PU", "");
    //                        else
    //                            dr["PU"] = sArray[0].Replace("PU", "");

    //                        dr["ModelName"] = strGName;
    //                        dr["Period"] = strPeriod;


    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            dr["D" + intU.ToString()] = strDay[intU];
    //                        }

    //                        dr["Auto"] = intAuto.ToString();
    //                        dr["Manual"] = intManual.ToString();
    //                        dr["Total"] = (intAuto + intManual).ToString();
    //                        dr["ID"] = strID;

    //                        dt_new.Rows.Add(dr);

    //                        intAuto = 0;
    //                        intManual = 0;
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            strDay[intU] = "";
    //                        }

    //                        strName = dt1.Rows[intJ]["Name"].ToString();
    //                        strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
    //                        strCustomer = dt1.Rows[intJ]["Customer"].ToString();
    //                        strDepartment = dt1.Rows[intJ]["Department"].ToString();
    //                        strGName = dt1.Rows[intJ]["GName"].ToString();
    //                        strPeriod = dt1.Rows[intJ]["Period"].ToString();
    //                        strID = dt1.Rows[intJ]["ID"].ToString();

    //                        startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
    //                        endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
    //                        while (startDate <= endDate)
    //                        {
    //                            string strDateW;

    //                            strDateW = startDate.Day.ToString();

    //                            strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

    //                            startDate = startDate.AddDays(1);

    //                            if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
    //                                intAuto++;
    //                            else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
    //                                intManual++;

    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    dr = dt_new.NewRow();


    //                    dr["Name"] = strName;
    //                    dr["Products_ID"] = strProducts_ID;
    //                    int intIndex, intIndex1;
    //                    intIndex = strCustomer.IndexOf("(");
    //                    if (intIndex < 0)
    //                        dr["Customer"] = strCustomer;
    //                    else
    //                        dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


    //                    string strDepartment2;


    //                    intIndex = strDepartment.IndexOf("(");
    //                    intIndex1 = strDepartment.IndexOf(")");
    //                    if (intIndex > 0)
    //                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                    else
    //                        strDepartment2 = strDepartment;

    //                    dr["Department"] = strDepartment2;

    //                    string[] sArray = strDepartment.Split('-');
    //                    int intU = 0;
    //                    foreach (string l in sArray)
    //                    {
    //                        intU++;
    //                    }
    //                    if (intU == 2)
    //                        dr["PU"] = sArray[1].Replace("PU", "");
    //                    else
    //                        dr["PU"] = sArray[0].Replace("PU", "");

    //                    dr["ModelName"] = strGName;
    //                    dr["Period"] = strPeriod;


    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        dr["D" + intU.ToString()] = strDay[intU];
    //                    }

    //                    dr["Auto"] = intAuto.ToString();
    //                    dr["Manual"] = intManual.ToString();
    //                    dr["Total"] = (intAuto + intManual).ToString();
    //                    dr["ID"] = strID;

    //                    dt_new.Rows.Add(dr);

    //                    intAuto = 0;
    //                    intManual = 0;
    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        strDay[intU] = "";
    //                    }

    //                    if (strPeriod == "D")
    //                    {
    //                        dr = dt_new.NewRow();


    //                        dr["Name"] = strName;
    //                        dr["Products_ID"] = strProducts_ID;
    //                        intIndex = strCustomer.IndexOf("(");
    //                        if (intIndex < 0)
    //                            dr["Customer"] = strCustomer;
    //                        else
    //                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




    //                        intIndex = strDepartment.IndexOf("(");
    //                        intIndex1 = strDepartment.IndexOf(")");
    //                        if (intIndex > 0)
    //                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                        else
    //                            strDepartment2 = strDepartment;

    //                        dr["Department"] = strDepartment2;

    //                        sArray = strDepartment.Split('-');
    //                        intU = 0;
    //                        foreach (string l in sArray)
    //                        {
    //                            intU++;
    //                        }
    //                        if (intU == 2)
    //                            dr["PU"] = sArray[1].Replace("PU", "");
    //                        else
    //                            dr["PU"] = sArray[0].Replace("PU", "");

    //                        dr["ModelName"] = strGName;
    //                        dr["Period"] = "N";
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            dr["D" + intU.ToString()] = "";
    //                        }
    //                        dr["Auto"] = "0";
    //                        dr["Manual"] = "0";
    //                        dr["Total"] = "0";
    //                        dr["ID"] = strID;

    //                        dt_new.Rows.Add(dr);

    //                        intAuto = 0;
    //                        intManual = 0;
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            strDay[intU] = "";
    //                        }
    //                    }




    //                    strName = dt1.Rows[intJ]["Name"].ToString();
    //                    strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
    //                    strCustomer = dt1.Rows[intJ]["Customer"].ToString();
    //                    strDepartment = dt1.Rows[intJ]["Department"].ToString();
    //                    strGName = dt1.Rows[intJ]["GName"].ToString();
    //                    strPeriod = dt1.Rows[intJ]["Period"].ToString();
    //                    strID = dt1.Rows[intJ]["ID"].ToString();

    //                    if (intJ != dt1.Rows.Count - 1)
    //                    {

    //                        if (strPeriod == "N")
    //                        {
    //                            dr = dt_new.NewRow();

    //                            dr["Name"] = strName;
    //                            dr["Products_ID"] = strProducts_ID;
    //                            intIndex = strCustomer.IndexOf("(");
    //                            if (intIndex < 0)
    //                                dr["Customer"] = strCustomer;
    //                            else
    //                                dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




    //                            intIndex = strDepartment.IndexOf("(");
    //                            intIndex1 = strDepartment.IndexOf(")");
    //                            if (intIndex > 0)
    //                                strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                            else
    //                                strDepartment2 = strDepartment;

    //                            dr["Department"] = strDepartment2;

    //                            sArray = strDepartment.Split('-');
    //                            intU = 0;
    //                            foreach (string l in sArray)
    //                            {
    //                                intU++;
    //                            }
    //                            if (intU == 2)
    //                                dr["PU"] = sArray[1].Replace("PU", "");
    //                            else
    //                                dr["PU"] = sArray[0].Replace("PU", "");

    //                            dr["ModelName"] = strGName;
    //                            dr["Period"] = "D";
    //                            for (intU = 1; intU < 32; intU++)
    //                            {
    //                                dr["D" + intU.ToString()] = "";
    //                            }
    //                            dr["Auto"] = "0";
    //                            dr["Manual"] = "0";
    //                            dr["Total"] = "0";
    //                            dr["ID"] = strID;

    //                            dt_new.Rows.Add(dr);

    //                            intAuto = 0;
    //                            intManual = 0;
    //                            for (intU = 1; intU < 32; intU++)
    //                            {
    //                                strDay[intU] = "";
    //                            }
    //                        }

    //                        startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
    //                        endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
    //                        while (startDate <= endDate)
    //                        {
    //                            string strDateW;

    //                            strDateW = startDate.Day.ToString();

    //                            strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

    //                            startDate = startDate.AddDays(1);

    //                            if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
    //                                intAuto++;
    //                            else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
    //                                intManual++;

    //                        }
    //                    }
    //                }
    //            }

    //            if (intJ == dt1.Rows.Count - 1)
    //            {
    //                if ((strName == dt1.Rows[intJ - 1]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ - 1]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ - 1]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ - 1]["Department"].ToString()) && (strGName == dt1.Rows[intJ - 1]["GName"].ToString()))
    //                {
    //                    if (strPeriod == dt1.Rows[intJ - 1]["Period"].ToString())
    //                    {
    //                        startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
    //                        endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
    //                        while (startDate <= endDate)
    //                        {
    //                            string strDateW;

    //                            strDateW = startDate.Day.ToString();

    //                            strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

    //                            startDate = startDate.AddDays(1);

    //                            if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
    //                                intAuto++;
    //                            else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
    //                                intManual++;

    //                        }

    //                        dr = dt_new.NewRow();


    //                        dr["Name"] = strName;
    //                        dr["Products_ID"] = strProducts_ID;
    //                        int intIndex, intIndex1;
    //                        intIndex = strCustomer.IndexOf("(");
    //                        if (intIndex < 0)
    //                            dr["Customer"] = strCustomer;
    //                        else
    //                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


    //                        string strDepartment2;


    //                        intIndex = strDepartment.IndexOf("(");
    //                        intIndex1 = strDepartment.IndexOf(")");
    //                        if (intIndex > 0)
    //                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                        else
    //                            strDepartment2 = strDepartment;

    //                        dr["Department"] = strDepartment2;

    //                        string[] sArray = strDepartment.Split('-');
    //                        int intU = 0;
    //                        foreach (string l in sArray)
    //                        {
    //                            intU++;
    //                        }
    //                        if (intU == 2)
    //                            dr["PU"] = sArray[1].Replace("PU", "");
    //                        else
    //                            dr["PU"] = sArray[0].Replace("PU", "");

    //                        dr["ModelName"] = strGName;
    //                        dr["Period"] = strPeriod;


    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            dr["D" + intU.ToString()] = strDay[intU];
    //                        }

    //                        dr["Auto"] = intAuto.ToString();
    //                        dr["Manual"] = intManual.ToString();
    //                        dr["Total"] = (intAuto + intManual).ToString();
    //                        dr["ID"] = strID;

    //                        dt_new.Rows.Add(dr);

    //                        intAuto = 0;
    //                        intManual = 0;
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            strDay[intU] = "";
    //                        }

    //                        if (strPeriod == "D")
    //                        {
    //                            dr = dt_new.NewRow();

    //                            dr["Name"] = strName;
    //                            dr["Products_ID"] = strProducts_ID;
    //                            intIndex = strCustomer.IndexOf("(");
    //                            if (intIndex < 0)
    //                                dr["Customer"] = strCustomer;
    //                            else
    //                                dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




    //                            intIndex = strDepartment.IndexOf("(");
    //                            intIndex1 = strDepartment.IndexOf(")");
    //                            if (intIndex > 0)
    //                                strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                            else
    //                                strDepartment2 = strDepartment;

    //                            dr["Department"] = strDepartment2;

    //                            sArray = strDepartment.Split('-');
    //                            intU = 0;
    //                            foreach (string l in sArray)
    //                            {
    //                                intU++;
    //                            }
    //                            if (intU == 2)
    //                                dr["PU"] = sArray[1].Replace("PU", "");
    //                            else
    //                                dr["PU"] = sArray[0].Replace("PU", "");

    //                            dr["ModelName"] = strGName;
    //                            dr["Period"] = "N";
    //                            for (intU = 1; intU < 32; intU++)
    //                            {
    //                                dr["D" + intU.ToString()] = "";
    //                            }
    //                            dr["Auto"] = "0";
    //                            dr["Manual"] = "0";
    //                            dr["Total"] = "0";
    //                            dr["ID"] = strID;

    //                            dt_new.Rows.Add(dr);

    //                            intAuto = 0;
    //                            intManual = 0;
    //                            for (intU = 1; intU < 32; intU++)
    //                            {
    //                                strDay[intU] = "";
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {


    //                        dr = dt_new.NewRow();


    //                        dr["Name"] = strName;
    //                        dr["Products_ID"] = strProducts_ID;
    //                        int intIndex, intIndex1;
    //                        intIndex = strCustomer.IndexOf("(");
    //                        if (intIndex < 0)
    //                            dr["Customer"] = strCustomer;
    //                        else
    //                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


    //                        string strDepartment2;


    //                        intIndex = strDepartment.IndexOf("(");
    //                        intIndex1 = strDepartment.IndexOf(")");
    //                        if (intIndex > 0)
    //                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                        else
    //                            strDepartment2 = strDepartment;

    //                        dr["Department"] = strDepartment2;

    //                        string[] sArray = strDepartment.Split('-');
    //                        int intU = 0;
    //                        foreach (string l in sArray)
    //                        {
    //                            intU++;
    //                        }
    //                        if (intU == 2)
    //                            dr["PU"] = sArray[1].Replace("PU", "");
    //                        else
    //                            dr["PU"] = sArray[0].Replace("PU", "");

    //                        dr["ModelName"] = strGName;
    //                        dr["Period"] = strPeriod;


    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            dr["D" + intU.ToString()] = strDay[intU];
    //                        }

    //                        dr["Auto"] = intAuto.ToString();
    //                        dr["Manual"] = intManual.ToString();
    //                        dr["Total"] = (intAuto + intManual).ToString();
    //                        dr["ID"] = strID;

    //                        dt_new.Rows.Add(dr);


    //                        intAuto = 0;
    //                        intManual = 0;
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            strDay[intU] = "";
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    int intIndex, intIndex1;
    //                    string strDepartment2;
    //                    string[] sArray;
    //                    int intU = 0;
    //                    intManual = 0;
    //                    intAuto = 0;


    //                    if (dt1.Rows[intJ]["Period"].ToString() == "N")
    //                    {
    //                        dr = dt_new.NewRow();

    //                        dr["Name"] = strName;
    //                        dr["Products_ID"] = strProducts_ID;
    //                        intIndex = strCustomer.IndexOf("(");
    //                        if (intIndex < 0)
    //                            dr["Customer"] = strCustomer;
    //                        else
    //                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);



    //                        intIndex = strDepartment.IndexOf("(");
    //                        intIndex1 = strDepartment.IndexOf(")");
    //                        if (intIndex > 0)
    //                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                        else
    //                            strDepartment2 = strDepartment;

    //                        dr["Department"] = strDepartment2;

    //                        sArray = strDepartment.Split('-');
    //                        foreach (string l in sArray)
    //                        {
    //                            intU++;
    //                        }
    //                        if (intU == 2)
    //                            dr["PU"] = sArray[1].Replace("PU", "");
    //                        else
    //                            dr["PU"] = sArray[0].Replace("PU", "");

    //                        dr["ModelName"] = strGName;
    //                        dr["Period"] = "D";
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            dr["D" + intU.ToString()] = "";
    //                        }
    //                        dr["Auto"] = "0";
    //                        dr["Manual"] = "0";
    //                        dr["Total"] = "0";
    //                        dr["ID"] = strID;

    //                        dt_new.Rows.Add(dr);

    //                        intAuto = 0;
    //                        intManual = 0;
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            strDay[intU] = "";
    //                        }
    //                    }

    //                    dr = dt_new.NewRow();

    //                    dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
    //                    dr["Products_ID"] = dt1.Rows[intJ]["Products_ID"].ToString();
    //                    intIndex = dt1.Rows[intJ]["Customer"].ToString().IndexOf("(");
    //                    if (intIndex < 0)
    //                        dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString();
    //                    else
    //                        dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString().Substring(1, intIndex - 1);




    //                    intIndex = dt1.Rows[intJ]["Department"].ToString().IndexOf("(");
    //                    intIndex1 = dt1.Rows[intJ]["Department"].ToString().IndexOf(")");
    //                    if (intIndex > 0)
    //                        strDepartment2 = dt1.Rows[intJ]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                    else
    //                        strDepartment2 = dt1.Rows[intJ]["Department"].ToString();

    //                    dr["Department"] = strDepartment2;

    //                    sArray = dt1.Rows[intJ]["Department"].ToString().Split('-');
    //                    intU = 0;
    //                    foreach (string l in sArray)
    //                    {
    //                        intU++;
    //                    }
    //                    if (intU == 2)
    //                        dr["PU"] = sArray[1].Replace("PU", "");
    //                    else
    //                        dr["PU"] = sArray[0].Replace("PU", "");

    //                    dr["ModelName"] = dt1.Rows[intJ]["GName"].ToString();
    //                    dr["Period"] = dt1.Rows[intJ]["Period"].ToString();

    //                    startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
    //                    endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
    //                    //intU = 0;
    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        strDay[intU] = "";
    //                    }
    //                    while (startDate <= endDate)
    //                    {
    //                        string strDateW;

    //                        strDateW = startDate.Day.ToString();

    //                        //dr["D" + intU.ToString()] = dt1.Rows[intJ]["UseKind"].ToString();
    //                        //dr["D" + Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();
    //                        strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();
    //                        startDate = startDate.AddDays(1);

    //                        if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
    //                            intAuto++;
    //                        else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
    //                            intManual++;

    //                    }

    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        dr["D" + intU.ToString()] = strDay[intU];
    //                    }
    //                    dr["Auto"] = intAuto.ToString();
    //                    dr["Manual"] = intManual.ToString();
    //                    dr["Total"] = (intAuto + intManual).ToString();
    //                    dr["ID"] = strID;

    //                    dt_new.Rows.Add(dr);

    //                    intAuto = 0;
    //                    intManual = 0;
    //                    for (intU = 1; intU < 32; intU++)
    //                    {
    //                        strDay[intU] = "";
    //                    }

    //                    if (dt1.Rows[intJ]["Period"].ToString() == "D")
    //                    {
    //                        dr = dt_new.NewRow();

    //                        dr["Name"] = strName;
    //                        dr["Products_ID"] = strProducts_ID;
    //                        intIndex = strCustomer.IndexOf("(");
    //                        if (intIndex < 0)
    //                            dr["Customer"] = strCustomer;
    //                        else
    //                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




    //                        intIndex = strDepartment.IndexOf("(");
    //                        intIndex1 = strDepartment.IndexOf(")");
    //                        if (intIndex > 0)
    //                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
    //                        else
    //                            strDepartment2 = strDepartment;

    //                        dr["Department"] = strDepartment2;

    //                        sArray = strDepartment.Split('-');
    //                        intU = 0;
    //                        foreach (string l in sArray)
    //                        {
    //                            intU++;
    //                        }
    //                        if (intU == 2)
    //                            dr["PU"] = sArray[1].Replace("PU", "");
    //                        else
    //                            dr["PU"] = sArray[0].Replace("PU", "");

    //                        dr["ModelName"] = strGName;
    //                        dr["Period"] = "N";
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            dr["D" + intU.ToString()] = "";
    //                        }
    //                        dr["Auto"] = "0";
    //                        dr["Manual"] = "0";
    //                        dr["Total"] = "0";
    //                        dr["ID"] = strID;

    //                        dt_new.Rows.Add(dr);

    //                        intAuto = 0;
    //                        intManual = 0;
    //                        for (intU = 1; intU < 32; intU++)
    //                        {
    //                            strDay[intU] = "";
    //                        }
    //                    }

    //                }
    //            }
    //        }


    //    }

    //    gvwMain.DataSource = dt_new;
    //    gvwMain.DataBind();

    //    BindChart1(strStartDate, strEndDate);
    //    BindColumnChart();
    //}

    private void getData1()
    {
        string strStartDate = "";
        string strEndDate = "";

        if (rdoWeek.Checked == true)
        {
            strStartDate = txtYearA.Text.Trim() + "/" + ddlMonthA.Text + "/01";


            if (txtYearA.Text == "")
                clsMsg.AlertMessage("請輸入日期！", this.Page);
            else
            {
                if ((Convert.ToInt32(ddlMonthA.Text) < 9))
                {
                    if ((Convert.ToInt32(ddlMonthA.Text) % 2) == 0)
                    {
                        if (ddlMonthA.Text == "02")
                        {
                            if ((Convert.ToInt32(txtYearA.Text) / 4) == 0)
                                strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/29";
                            else
                                strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/28";
                        }
                        else
                        {
                            if (ddlMonthE.Text == "08")
                                strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/31";
                            else
                                strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/30";
                        }
                    }
                    else
                        strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/31";
                }
                else
                {
                    if ((ddlMonthA.Text == "09") || (ddlMonthA.Text == "11"))
                        strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/30";
                    else
                        strEndDate = txtYearA.Text + "/" + ddlMonthA.Text + "/31";
                }


            }
        }
        else
        {
            if (strStart != "")
            {
                strStart = Request["date1"].ToString();
                DateTime dt = Convert.ToDateTime(strStart);

                strStartDate = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

                strEndDate = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

            }
            else
                clsMsg.AlertMessage("請輸入日期！", this.Page);
        }

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Products_ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Products_ID";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Department");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Department";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("PU");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "PU";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Customer");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Customer";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("ModelName");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "ModelName";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Period");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Period";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("UseKind");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "UseKind";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("D1");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "D1";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("D2");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "D2";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("D3");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "D3";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("D4");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "D4";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("D5");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "D5";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("D6");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "D6";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);

        DataColumn column15 = new DataColumn("D7");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "D7";
        column15.DefaultValue = "0";
        dt_new.Columns.Add(column15);

        DataColumn column16 = new DataColumn("D8");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "D8";
        column16.DefaultValue = "0";
        dt_new.Columns.Add(column16);

        DataColumn column17 = new DataColumn("D9");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "D9";
        column17.DefaultValue = "0";
        dt_new.Columns.Add(column17);

        DataColumn column18 = new DataColumn("D10");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "D10";
        column18.DefaultValue = "0";
        dt_new.Columns.Add(column18);

        DataColumn column19 = new DataColumn("D11");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "D11";
        column19.DefaultValue = "0";
        dt_new.Columns.Add(column19);

        DataColumn column20 = new DataColumn("D12");
        column20.DataType = System.Type.GetType("System.String");
        column20.AllowDBNull = true;
        column20.Caption = "D12";
        column20.DefaultValue = "0";
        dt_new.Columns.Add(column20);

        DataColumn column21 = new DataColumn("D13");
        column21.DataType = System.Type.GetType("System.String");
        column21.AllowDBNull = true;
        column21.Caption = "D13";
        column21.DefaultValue = "0";
        dt_new.Columns.Add(column21);

        DataColumn column22 = new DataColumn("D14");
        column22.DataType = System.Type.GetType("System.String");
        column22.AllowDBNull = true;
        column22.Caption = "D14";
        column22.DefaultValue = "0";
        dt_new.Columns.Add(column22);

        DataColumn column23 = new DataColumn("D15");
        column23.DataType = System.Type.GetType("System.String");
        column23.AllowDBNull = true;
        column23.Caption = "D15";
        column23.DefaultValue = "0";
        dt_new.Columns.Add(column23);

        DataColumn column24 = new DataColumn("D16");
        column24.DataType = System.Type.GetType("System.String");
        column24.AllowDBNull = true;
        column24.Caption = "D16";
        column24.DefaultValue = "0";
        dt_new.Columns.Add(column24);

        DataColumn column25 = new DataColumn("D17");
        column25.DataType = System.Type.GetType("System.String");
        column25.AllowDBNull = true;
        column25.Caption = "D17";
        column25.DefaultValue = "0";
        dt_new.Columns.Add(column25);

        DataColumn column26 = new DataColumn("D18");
        column26.DataType = System.Type.GetType("System.String");
        column26.AllowDBNull = true;
        column26.Caption = "D18";
        column26.DefaultValue = "0";
        dt_new.Columns.Add(column26);

        DataColumn column27 = new DataColumn("D19");
        column27.DataType = System.Type.GetType("System.String");
        column27.AllowDBNull = true;
        column27.Caption = "D19";
        column27.DefaultValue = "0";
        dt_new.Columns.Add(column27);

        DataColumn column28 = new DataColumn("D20");
        column28.DataType = System.Type.GetType("System.String");
        column28.AllowDBNull = true;
        column28.Caption = "D20";
        column28.DefaultValue = "0";
        dt_new.Columns.Add(column28);

        DataColumn column29 = new DataColumn("D21");
        column29.DataType = System.Type.GetType("System.String");
        column29.AllowDBNull = true;
        column29.Caption = "D21";
        column29.DefaultValue = "0";
        dt_new.Columns.Add(column29);

        DataColumn column30 = new DataColumn("D22");
        column30.DataType = System.Type.GetType("System.String");
        column30.AllowDBNull = true;
        column30.Caption = "D22";
        column30.DefaultValue = "0";
        dt_new.Columns.Add(column30);

        DataColumn column31 = new DataColumn("D23");
        column31.DataType = System.Type.GetType("System.String");
        column31.AllowDBNull = true;
        column31.Caption = "D23";
        column31.DefaultValue = "0";
        dt_new.Columns.Add(column31);

        DataColumn column32 = new DataColumn("D24");
        column32.DataType = System.Type.GetType("System.String");
        column32.AllowDBNull = true;
        column32.Caption = "D24";
        column32.DefaultValue = "0";
        dt_new.Columns.Add(column32);

        DataColumn column33 = new DataColumn("D25");
        column33.DataType = System.Type.GetType("System.String");
        column33.AllowDBNull = true;
        column33.Caption = "D25";
        column33.DefaultValue = "0";
        dt_new.Columns.Add(column33);

        DataColumn column34 = new DataColumn("D26");
        column34.DataType = System.Type.GetType("System.String");
        column34.AllowDBNull = true;
        column34.Caption = "D26";
        column34.DefaultValue = "0";
        dt_new.Columns.Add(column34);

        DataColumn column35 = new DataColumn("D27");
        column35.DataType = System.Type.GetType("System.String");
        column35.AllowDBNull = true;
        column35.Caption = "D27";
        column35.DefaultValue = "0";
        dt_new.Columns.Add(column35);

        DataColumn column36 = new DataColumn("D28");
        column36.DataType = System.Type.GetType("System.String");
        column36.AllowDBNull = true;
        column36.Caption = "D28";
        column36.DefaultValue = "0";
        dt_new.Columns.Add(column36);

        DataColumn column37 = new DataColumn("D29");
        column37.DataType = System.Type.GetType("System.String");
        column37.AllowDBNull = true;
        column37.Caption = "D29";
        column37.DefaultValue = "0";
        dt_new.Columns.Add(column37);

        DataColumn column38 = new DataColumn("D30");
        column38.DataType = System.Type.GetType("System.String");
        column38.AllowDBNull = true;
        column38.Caption = "D30";
        column38.DefaultValue = "0";
        dt_new.Columns.Add(column38);

        DataColumn column39 = new DataColumn("D31");
        column39.DataType = System.Type.GetType("System.String");
        column39.AllowDBNull = true;
        column39.Caption = "D31";
        column39.DefaultValue = "0";
        dt_new.Columns.Add(column39);

        DataColumn column40 = new DataColumn("Auto");
        column40.DataType = System.Type.GetType("System.String");
        column40.AllowDBNull = true;
        column40.Caption = "Auto";
        column40.DefaultValue = "0";
        dt_new.Columns.Add(column40);

        DataColumn column41 = new DataColumn("Manual");
        column41.DataType = System.Type.GetType("System.String");
        column41.AllowDBNull = true;
        column41.Caption = "Manual";
        column41.DefaultValue = "0";
        dt_new.Columns.Add(column41);

        DataColumn column42 = new DataColumn("Total");
        column42.DataType = System.Type.GetType("System.String");
        column42.AllowDBNull = true;
        column42.Caption = "Total";
        column42.DefaultValue = "0";
        dt_new.Columns.Add(column42);

        DataColumn column43 = new DataColumn("ID");
        column43.DataType = System.Type.GetType("System.String");
        column43.AllowDBNull = true;
        column43.Caption = "ID";
        column43.DefaultValue = "0";
        dt_new.Columns.Add(column43);

        DataTable dt1;

        string strLocal;

        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";

        if (rdoDepartment.Checked == true)
        {
            if (rdoCustodian.Checked == true)
                dt1 = clsData.UploadApparatusReport1("0", ddlDepartment.Text, ddlCustodian.Text, strStartDate, strEndDate, strLocal);
            else
                dt1 = clsData.UploadApparatusReport1("1", ddlDepartment.Text, ddlKind.Text, strStartDate, strEndDate, strLocal);

        }
        else if (rdoProducts_ID.Checked == true)
            dt1 = clsData.UploadApparatusReport1("2", txtProducts_ID.Text, "", strStartDate, strEndDate, strLocal);
        else
            dt1 = clsData.UploadApparatusReport1("3", ddlKind1.Text, "", strStartDate, strEndDate, strLocal);

        DataRow dr;
        string strName = "";
        string strProducts_ID = "";
        string strCustomer = "";
        string strDepartment = "";
        string strGName = "";
        string strPeriod = "";
        string strID = "";
        string[] strDay = new string[32];
        int intAuto = 0;
        int intManual = 0;
        DateTime startDate;
        DateTime endDate;

        for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
        {

            if ((dt1.Rows[intJ]["Period"].ToString() == null) || (dt1.Rows[intJ]["Period"].ToString() == ""))
            {
                int intIndex, intIndex1;
                string strDepartment2;
                int intU = 0;
                string[] sArray;

                if (intJ != 0)
                {
                    if (strPeriod != "")
                    {
                        dr = dt_new.NewRow();


                        dr["Name"] = strName;
                        dr["Products_ID"] = strProducts_ID;

                        intIndex = strCustomer.IndexOf("(");
                        if (intIndex < 0)
                            dr["Customer"] = strCustomer;
                        else
                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);





                        intIndex = strDepartment.IndexOf("(");
                        intIndex1 = strDepartment.IndexOf(")");
                        if (intIndex > 0)
                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                        else
                            strDepartment2 = strDepartment;

                        dr["Department"] = strDepartment2;

                        sArray = strDepartment.Split('-');

                        foreach (string l in sArray)
                        {
                            intU++;
                        }
                        if (intU == 2)
                            dr["PU"] = sArray[1].Replace("PU", "");
                        else
                            dr["PU"] = sArray[0].Replace("PU", "");

                        dr["ModelName"] = strGName;
                        dr["Period"] = strPeriod;


                        for (intU = 1; intU < 32; intU++)
                        {
                            dr["D" + intU.ToString()] = strDay[intU];
                        }

                        dr["Auto"] = intAuto.ToString();
                        dr["Manual"] = intManual.ToString();
                        dr["Total"] = (intAuto + intManual).ToString();
                        dr["ID"] = strID;

                        dt_new.Rows.Add(dr);

                        intAuto = 0;
                        intManual = 0;
                        for (intU = 1; intU < 32; intU++)
                        {
                            strDay[intU] = "";
                        }
                    }
                }
                if (strPeriod == "D")
                {
                    dr = dt_new.NewRow();


                    dr["Name"] = strName;
                    dr["Products_ID"] = strProducts_ID;
                    intIndex = strCustomer.IndexOf("(");
                    if (intIndex < 0)
                        dr["Customer"] = strCustomer;
                    else
                        dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




                    intIndex = strDepartment.IndexOf("(");
                    intIndex1 = strDepartment.IndexOf(")");
                    if (intIndex > 0)
                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                    else
                        strDepartment2 = strDepartment;

                    dr["Department"] = strDepartment2;

                    sArray = strDepartment.Split('-');
                    intU = 0;
                    foreach (string l in sArray)
                    {
                        intU++;
                    }
                    if (intU == 2)
                        dr["PU"] = sArray[1].Replace("PU", "");
                    else
                        dr["PU"] = sArray[0].Replace("PU", "");

                    dr["ModelName"] = strGName;
                    dr["Period"] = "N";
                    for (intU = 1; intU < 32; intU++)
                    {
                        dr["D" + intU.ToString()] = "";
                    }
                    dr["Auto"] = "0";
                    dr["Manual"] = "0";
                    dr["Total"] = "0";
                    dr["ID"] = strID;

                    dt_new.Rows.Add(dr);

                    intAuto = 0;
                    intManual = 0;
                    for (intU = 1; intU < 32; intU++)
                    {
                        strDay[intU] = "";
                    }
                }

                strName = dt1.Rows[intJ]["Name"].ToString();
                strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
                strCustomer = dt1.Rows[intJ]["Customer"].ToString();
                strDepartment = dt1.Rows[intJ]["Department"].ToString();
                strGName = dt1.Rows[intJ]["GName"].ToString();
                strPeriod = dt1.Rows[intJ]["Period"].ToString();
                strID = dt1.Rows[intJ]["ID"].ToString();

                dr = dt_new.NewRow();

                dr["Name"] = strName;
                dr["Products_ID"] = strProducts_ID;
                //int intIndex, intIndex1;
                intIndex = strCustomer.IndexOf("(");
                if (intIndex < 0)
                    dr["Customer"] = strCustomer;
                else
                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                //string strDepartment2;


                intIndex = strDepartment.IndexOf("(");
                intIndex1 = strDepartment.IndexOf(")");
                if (intIndex > 0)
                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                else
                    strDepartment2 = strDepartment;

                dr["Department"] = strDepartment2;

                sArray = strDepartment.Split('-');
                //int intU = 0;
                foreach (string l in sArray)
                {
                    intU++;
                }
                if (intU == 2)
                    dr["PU"] = sArray[1].Replace("PU", "");
                else
                    dr["PU"] = sArray[0].Replace("PU", "");

                dr["ModelName"] = strGName;
                dr["Period"] = "D";
                for (intU = 1; intU < 32; intU++)
                {
                    dr["D" + intU.ToString()] = "";
                }
                dr["Auto"] = "0";
                dr["Manual"] = "0";
                dr["Total"] = "0";
                dr["ID"] = strID;

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();

                dr["Name"] = strName;
                dr["Products_ID"] = strProducts_ID;
                //int intIndex, intIndex1;
                intIndex = strCustomer.IndexOf("(");
                if (intIndex < 0)
                    dr["Customer"] = strCustomer;
                else
                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                //string strDepartment2;


                intIndex = strDepartment.IndexOf("(");
                intIndex1 = strDepartment.IndexOf(")");
                if (intIndex > 0)
                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                else
                    strDepartment2 = strDepartment;

                dr["Department"] = strDepartment2;

                sArray = strDepartment.Split('-');
                intU = 0;
                foreach (string l in sArray)
                {
                    intU++;
                }
                if (intU == 2)
                    dr["PU"] = sArray[1].Replace("PU", "");
                else
                    dr["PU"] = sArray[0].Replace("PU", "");

                dr["ModelName"] = strGName;
                dr["Period"] = "N";
                for (intU = 1; intU < 32; intU++)
                {
                    dr["D" + intU.ToString()] = "";
                }
                dr["Auto"] = "0";
                dr["Manual"] = "0";
                dr["Total"] = "0";
                dr["ID"] = strID;

                dt_new.Rows.Add(dr);


            }
            else
            {
                if ((intJ == 0) && (intJ != dt1.Rows.Count - 1))
                {
                    strName = dt1.Rows[intJ]["Name"].ToString();
                    strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
                    strCustomer = dt1.Rows[intJ]["Customer"].ToString();
                    strDepartment = dt1.Rows[intJ]["Department"].ToString();
                    strGName = dt1.Rows[intJ]["GName"].ToString();
                    strPeriod = dt1.Rows[intJ]["Period"].ToString();
                    strID = dt1.Rows[intJ]["ID"].ToString();

                    if (strPeriod == "N")
                    {
                        dr = dt_new.NewRow();

                        dr["Name"] = strName;
                        dr["Products_ID"] = strProducts_ID;
                        int intIndex, intIndex1;
                        intIndex = strCustomer.IndexOf("(");
                        if (intIndex < 0)
                            dr["Customer"] = strCustomer;
                        else
                            dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                        string strDepartment2;


                        intIndex = strDepartment.IndexOf("(");
                        intIndex1 = strDepartment.IndexOf(")");
                        if (intIndex > 0)
                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                        else
                            strDepartment2 = strDepartment;

                        dr["Department"] = strDepartment2;

                        string[] sArray = strDepartment.Split('-');
                        int intU = 0;
                        foreach (string l in sArray)
                        {
                            intU++;
                        }
                        if (intU == 2)
                            dr["PU"] = sArray[1].Replace("PU", "");
                        else
                            dr["PU"] = sArray[0].Replace("PU", "");

                        dr["ModelName"] = strGName;
                        dr["Period"] = "D";
                        for (intU = 1; intU < 32; intU++)
                        {
                            dr["D" + intU.ToString()] = "";
                        }
                        dr["Auto"] = "0";
                        dr["Manual"] = "0";
                        dr["Total"] = "0";
                        dr["ID"] = strID;

                        dt_new.Rows.Add(dr);

                        intAuto = 0;
                        intManual = 0;
                        for (intU = 1; intU < 32; intU++)
                        {
                            strDay[intU] = "";
                        }


                    }

                    startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                    DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                    if (startDate < strStartDate1)
                        startDate = strStartDate1;
                    endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                    while (startDate <= endDate)
                    {
                        string strDateW;

                        strDateW = startDate.Day.ToString();

                        strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                        startDate = startDate.AddDays(1);

                        if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                            intAuto++;
                        else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                            intManual++;

                    }


                }
                else
                {



                    if ((strName == dt1.Rows[intJ]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ]["Department"].ToString()) && (strGName == dt1.Rows[intJ]["GName"].ToString()))
                    //if ((strName == dt1.Rows[intJ]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ]["Department"].ToString()))
                    {
                        if (strPeriod == dt1.Rows[intJ]["Period"].ToString())
                        {
                            startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                            endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                            DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                            if (startDate < strStartDate1)
                                startDate = strStartDate1;
                            while (startDate <= endDate)
                            {
                                string strDateW;

                                strDateW = startDate.Day.ToString();

                                strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                                startDate = startDate.AddDays(1);

                                if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                    intAuto++;
                                else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                    intManual++;

                            }
                        }
                        else
                        {
                            dr = dt_new.NewRow();


                            dr["Name"] = strName;
                            dr["Products_ID"] = strProducts_ID;
                            int intIndex, intIndex1;
                            intIndex = strCustomer.IndexOf("(");
                            if (intIndex < 0)
                                dr["Customer"] = strCustomer;
                            else
                                dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                            string strDepartment2;


                            intIndex = strDepartment.IndexOf("(");
                            intIndex1 = strDepartment.IndexOf(")");
                            if (intIndex > 0)
                                strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                            else
                                strDepartment2 = strDepartment;

                            dr["Department"] = strDepartment2;

                            string[] sArray = strDepartment.Split('-');
                            int intU = 0;
                            foreach (string l in sArray)
                            {
                                intU++;
                            }
                            if (intU == 2)
                                dr["PU"] = sArray[1].Replace("PU", "");
                            else
                                dr["PU"] = sArray[0].Replace("PU", "");

                            dr["ModelName"] = strGName;
                            dr["Period"] = strPeriod;

                            //if ((intAuto + intManual) == 0)
                            //{
                            //startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                            //endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                            //while (startDate < endDate)
                            //{
                            //    string strDateW;

                            //    strDateW = startDate.Day.ToString();

                            //    strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                            //    startDate = startDate.AddDays(1);

                            //    if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                            //        intAuto++;
                            //    else
                            //        intManual++;

                            //}
                            //}

                            for (intU = 1; intU < 32; intU++)
                            {
                                dr["D" + intU.ToString()] = strDay[intU];
                            }

                            dr["Auto"] = intAuto.ToString();
                            dr["Manual"] = intManual.ToString();
                            dr["Total"] = (intAuto + intManual).ToString();
                            dr["ID"] = strID;

                            dt_new.Rows.Add(dr);

                            intAuto = 0;
                            intManual = 0;
                            for (intU = 1; intU < 32; intU++)
                            {
                                strDay[intU] = "";
                            }

                            strName = dt1.Rows[intJ]["Name"].ToString();
                            strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
                            strCustomer = dt1.Rows[intJ]["Customer"].ToString();
                            strDepartment = dt1.Rows[intJ]["Department"].ToString();
                            strGName = dt1.Rows[intJ]["GName"].ToString();
                            strPeriod = dt1.Rows[intJ]["Period"].ToString();
                            strID = dt1.Rows[intJ]["ID"].ToString();

                            startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                            endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                            DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                            if (startDate < strStartDate1)
                                startDate = strStartDate1;
                            while (startDate <= endDate)
                            {
                                string strDateW;

                                strDateW = startDate.Day.ToString();

                                strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                                startDate = startDate.AddDays(1);

                                if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                    intAuto++;
                                else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                    intManual++;

                            }
                        }
                    }
                    else
                    {
                        int intIndex, intIndex1;
                        string strDepartment2;
                        int intU = 0;
                        string[] sArray;

                        if (intJ != 0)
                        {
                            if (strPeriod != "")
                            {
                                dr = dt_new.NewRow();


                                dr["Name"] = strName;
                                dr["Products_ID"] = strProducts_ID;

                                intIndex = strCustomer.IndexOf("(");
                                if (intIndex < 0)
                                    dr["Customer"] = strCustomer;
                                else
                                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);





                                intIndex = strDepartment.IndexOf("(");
                                intIndex1 = strDepartment.IndexOf(")");
                                if (intIndex > 0)
                                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                else
                                    strDepartment2 = strDepartment;

                                dr["Department"] = strDepartment2;

                                sArray = strDepartment.Split('-');

                                foreach (string l in sArray)
                                {
                                    intU++;
                                }
                                if (intU == 2)
                                    dr["PU"] = sArray[1].Replace("PU", "");
                                else
                                    dr["PU"] = sArray[0].Replace("PU", "");

                                dr["ModelName"] = strGName;
                                dr["Period"] = strPeriod;

                                //if ((intAuto + intManual) == 0)
                                //{
                                //    startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                                //    endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                                //    while (startDate < endDate)
                                //    {
                                //        string strDateW;

                                //        strDateW = startDate.Day.ToString();

                                //        strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                                //        startDate = startDate.AddDays(1);

                                //        if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                //            intAuto++;
                                //        else
                                //            intManual++;

                                //    }
                                //}

                                for (intU = 1; intU < 32; intU++)
                                {
                                    dr["D" + intU.ToString()] = strDay[intU];
                                }

                                dr["Auto"] = intAuto.ToString();
                                dr["Manual"] = intManual.ToString();
                                dr["Total"] = (intAuto + intManual).ToString();
                                dr["ID"] = strID;

                                dt_new.Rows.Add(dr);

                                intAuto = 0;
                                intManual = 0;
                                for (intU = 1; intU < 32; intU++)
                                {
                                    strDay[intU] = "";
                                }
                            }
                        }
                        if (strPeriod == "D")
                        {
                            dr = dt_new.NewRow();


                            dr["Name"] = strName;
                            dr["Products_ID"] = strProducts_ID;
                            intIndex = strCustomer.IndexOf("(");
                            if (intIndex < 0)
                                dr["Customer"] = strCustomer;
                            else
                                dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




                            intIndex = strDepartment.IndexOf("(");
                            intIndex1 = strDepartment.IndexOf(")");
                            if (intIndex > 0)
                                strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                            else
                                strDepartment2 = strDepartment;

                            dr["Department"] = strDepartment2;

                            sArray = strDepartment.Split('-');
                            intU = 0;
                            foreach (string l in sArray)
                            {
                                intU++;
                            }
                            if (intU == 2)
                                dr["PU"] = sArray[1].Replace("PU", "");
                            else
                                dr["PU"] = sArray[0].Replace("PU", "");

                            dr["ModelName"] = strGName;
                            dr["Period"] = "N";
                            for (intU = 1; intU < 32; intU++)
                            {
                                dr["D" + intU.ToString()] = "";
                            }
                            dr["Auto"] = "0";
                            dr["Manual"] = "0";
                            dr["Total"] = "0";
                            dr["ID"] = strID;

                            dt_new.Rows.Add(dr);

                            intAuto = 0;
                            intManual = 0;
                            for (intU = 1; intU < 32; intU++)
                            {
                                strDay[intU] = "";
                            }
                        }




                        strName = dt1.Rows[intJ]["Name"].ToString();
                        strProducts_ID = dt1.Rows[intJ]["Products_ID"].ToString();
                        strCustomer = dt1.Rows[intJ]["Customer"].ToString();
                        strDepartment = dt1.Rows[intJ]["Department"].ToString();
                        strGName = dt1.Rows[intJ]["GName"].ToString();
                        strPeriod = dt1.Rows[intJ]["Period"].ToString();
                        strID = dt1.Rows[intJ]["ID"].ToString();

                        if (intJ != dt1.Rows.Count - 1)
                        {

                            if (strPeriod == "N")
                            {
                                dr = dt_new.NewRow();

                                dr["Name"] = strName;
                                dr["Products_ID"] = strProducts_ID;
                                intIndex = strCustomer.IndexOf("(");
                                if (intIndex < 0)
                                    dr["Customer"] = strCustomer;
                                else
                                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




                                intIndex = strDepartment.IndexOf("(");
                                intIndex1 = strDepartment.IndexOf(")");
                                if (intIndex > 0)
                                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                else
                                    strDepartment2 = strDepartment;

                                dr["Department"] = strDepartment2;

                                sArray = strDepartment.Split('-');
                                intU = 0;
                                foreach (string l in sArray)
                                {
                                    intU++;
                                }
                                if (intU == 2)
                                    dr["PU"] = sArray[1].Replace("PU", "");
                                else
                                    dr["PU"] = sArray[0].Replace("PU", "");

                                dr["ModelName"] = strGName;
                                dr["Period"] = "D";
                                for (intU = 1; intU < 32; intU++)
                                {
                                    dr["D" + intU.ToString()] = "";
                                }
                                dr["Auto"] = "0";
                                dr["Manual"] = "0";
                                dr["Total"] = "0";
                                dr["ID"] = strID;

                                dt_new.Rows.Add(dr);

                                intAuto = 0;
                                intManual = 0;
                                for (intU = 1; intU < 32; intU++)
                                {
                                    strDay[intU] = "";
                                }
                            }

                            startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                            endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                            DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                            if (startDate < strStartDate1)
                                startDate = strStartDate1;
                            while (startDate <= endDate)
                            {
                                string strDateW;

                                strDateW = startDate.Day.ToString();

                                strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                                startDate = startDate.AddDays(1);

                                if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                    intAuto++;
                                else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                    intManual++;

                            }
                        }
                        //}
                    }
                }


                if (intJ == dt1.Rows.Count - 1)
                {
                    if (intJ == 0)
                    {
                        int intIndex, intIndex1;
                        string strDepartment2;
                        string[] sArray;
                        int intU = 0;
                        intManual = 0;
                        intAuto = 0;


                        if (dt1.Rows[intJ]["Period"].ToString() == "N")
                        {
                            dr = dt_new.NewRow();

                            dr["Name"] = strName;
                            dr["Products_ID"] = strProducts_ID;
                            intIndex = strCustomer.IndexOf("(");
                            if (intIndex < 0)
                                dr["Customer"] = strCustomer;
                            else
                                dr["Customer"] = strCustomer.Substring(1, intIndex - 1);



                            intIndex = strDepartment.IndexOf("(");
                            intIndex1 = strDepartment.IndexOf(")");
                            if (intIndex > 0)
                                strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                            else
                                strDepartment2 = strDepartment;

                            dr["Department"] = strDepartment2;

                            sArray = strDepartment.Split('-');
                            foreach (string l in sArray)
                            {
                                intU++;
                            }
                            if (intU == 2)
                                dr["PU"] = sArray[1].Replace("PU", "");
                            else
                                dr["PU"] = sArray[0].Replace("PU", "");

                            dr["ModelName"] = strGName;
                            dr["Period"] = "D";
                            for (intU = 1; intU < 32; intU++)
                            {
                                dr["D" + intU.ToString()] = "";
                            }
                            dr["Auto"] = "0";
                            dr["Manual"] = "0";
                            dr["Total"] = "0";
                            dr["ID"] = strID;

                            dt_new.Rows.Add(dr);

                            intAuto = 0;
                            intManual = 0;
                            for (intU = 1; intU < 32; intU++)
                            {
                                strDay[intU] = "";
                            }
                        }

                        dr = dt_new.NewRow();

                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Products_ID"] = dt1.Rows[intJ]["Products_ID"].ToString();
                        intIndex = dt1.Rows[intJ]["Customer"].ToString().IndexOf("(");
                        if (intIndex < 0)
                            dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString();
                        else
                            dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString().Substring(1, intIndex - 1);




                        intIndex = dt1.Rows[intJ]["Department"].ToString().IndexOf("(");
                        intIndex1 = dt1.Rows[intJ]["Department"].ToString().IndexOf(")");
                        if (intIndex > 0)
                            strDepartment2 = dt1.Rows[intJ]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                        else
                            strDepartment2 = dt1.Rows[intJ]["Department"].ToString();

                        dr["Department"] = strDepartment2;

                        sArray = dt1.Rows[intJ]["Department"].ToString().Split('-');
                        intU = 0;
                        foreach (string l in sArray)
                        {
                            intU++;
                        }
                        if (intU == 2)
                            dr["PU"] = sArray[1].Replace("PU", "");
                        else
                            dr["PU"] = sArray[0].Replace("PU", "");

                        dr["ModelName"] = dt1.Rows[intJ]["GName"].ToString();
                        dr["Period"] = dt1.Rows[intJ]["Period"].ToString();

                        startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                        endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                        DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                        if (startDate < strStartDate1)
                            startDate = strStartDate1;
                        //intU = 0;
                        for (intU = 1; intU < 32; intU++)
                        {
                            strDay[intU] = "";
                        }
                        while (startDate <= endDate)
                        {
                            string strDateW;

                            strDateW = startDate.Day.ToString();

                            //dr["D" + intU.ToString()] = dt1.Rows[intJ]["UseKind"].ToString();
                            //dr["D" + Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();
                            strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();
                            startDate = startDate.AddDays(1);

                            if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                intAuto++;
                            else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                intManual++;

                        }

                        for (intU = 1; intU < 32; intU++)
                        {
                            dr["D" + intU.ToString()] = strDay[intU];
                        }
                        dr["Auto"] = intAuto.ToString();
                        dr["Manual"] = intManual.ToString();
                        dr["Total"] = (intAuto + intManual).ToString();
                        dr["ID"] = strID;

                        dt_new.Rows.Add(dr);

                        intAuto = 0;
                        intManual = 0;
                        for (intU = 1; intU < 32; intU++)
                        {
                            strDay[intU] = "";
                        }

                        if (dt1.Rows[intJ]["Period"].ToString() == "D")
                        {
                            dr = dt_new.NewRow();

                            dr["Name"] = strName;
                            dr["Products_ID"] = strProducts_ID;
                            intIndex = strCustomer.IndexOf("(");
                            if (intIndex < 0)
                                dr["Customer"] = strCustomer;
                            else
                                dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




                            intIndex = strDepartment.IndexOf("(");
                            intIndex1 = strDepartment.IndexOf(")");
                            if (intIndex > 0)
                                strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                            else
                                strDepartment2 = strDepartment;

                            dr["Department"] = strDepartment2;

                            sArray = strDepartment.Split('-');
                            intU = 0;
                            foreach (string l in sArray)
                            {
                                intU++;
                            }
                            if (intU == 2)
                                dr["PU"] = sArray[1].Replace("PU", "");
                            else
                                dr["PU"] = sArray[0].Replace("PU", "");

                            dr["ModelName"] = strGName;
                            dr["Period"] = "N";
                            for (intU = 1; intU < 32; intU++)
                            {
                                dr["D" + intU.ToString()] = "";
                            }
                            dr["Auto"] = "0";
                            dr["Manual"] = "0";
                            dr["Total"] = "0";
                            dr["ID"] = strID;

                            dt_new.Rows.Add(dr);

                            intAuto = 0;
                            intManual = 0;
                            for (intU = 1; intU < 32; intU++)
                            {
                                strDay[intU] = "";
                            }
                        }
                    }
                    else
                    {
                        if ((strName == dt1.Rows[intJ - 1]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ - 1]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ - 1]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ - 1]["Department"].ToString()) && (strGName == dt1.Rows[intJ - 1]["GName"].ToString()))
                        //if ((strName == dt1.Rows[intJ - 1]["Name"].ToString()) && (strProducts_ID == dt1.Rows[intJ - 1]["Products_ID"].ToString()) && (strCustomer == dt1.Rows[intJ - 1]["Customer"].ToString()) && (strDepartment == dt1.Rows[intJ - 1]["Department"].ToString()))
                        {
                            if (strPeriod == dt1.Rows[intJ - 1]["Period"].ToString())
                            {
                                startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                                endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                                DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                                if (startDate < strStartDate1)
                                    startDate = strStartDate1;
                                while (startDate <= endDate)
                                {
                                    string strDateW;

                                    strDateW = startDate.Day.ToString();

                                    strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                                    startDate = startDate.AddDays(1);

                                    if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                        intAuto++;
                                    else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                        intManual++;

                                }

                                dr = dt_new.NewRow();


                                dr["Name"] = strName;
                                dr["Products_ID"] = strProducts_ID;
                                int intIndex, intIndex1;
                                intIndex = strCustomer.IndexOf("(");
                                if (intIndex < 0)
                                    dr["Customer"] = strCustomer;
                                else
                                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                                string strDepartment2;


                                intIndex = strDepartment.IndexOf("(");
                                intIndex1 = strDepartment.IndexOf(")");
                                if (intIndex > 0)
                                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                else
                                    strDepartment2 = strDepartment;

                                dr["Department"] = strDepartment2;

                                string[] sArray = strDepartment.Split('-');
                                int intU = 0;
                                foreach (string l in sArray)
                                {
                                    intU++;
                                }
                                if (intU == 2)
                                    dr["PU"] = sArray[1].Replace("PU", "");
                                else
                                    dr["PU"] = sArray[0].Replace("PU", "");

                                dr["ModelName"] = strGName;
                                dr["Period"] = strPeriod;


                                for (intU = 1; intU < 32; intU++)
                                {
                                    dr["D" + intU.ToString()] = strDay[intU];
                                }

                                dr["Auto"] = intAuto.ToString();
                                dr["Manual"] = intManual.ToString();
                                dr["Total"] = (intAuto + intManual).ToString();
                                dr["ID"] = strID;

                                dt_new.Rows.Add(dr);

                                intAuto = 0;
                                intManual = 0;
                                for (intU = 1; intU < 32; intU++)
                                {
                                    strDay[intU] = "";
                                }

                                if (strPeriod == "D")
                                {
                                    dr = dt_new.NewRow();

                                    dr["Name"] = strName;
                                    dr["Products_ID"] = strProducts_ID;
                                    intIndex = strCustomer.IndexOf("(");
                                    if (intIndex < 0)
                                        dr["Customer"] = strCustomer;
                                    else
                                        dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




                                    intIndex = strDepartment.IndexOf("(");
                                    intIndex1 = strDepartment.IndexOf(")");
                                    if (intIndex > 0)
                                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                    else
                                        strDepartment2 = strDepartment;

                                    dr["Department"] = strDepartment2;

                                    sArray = strDepartment.Split('-');
                                    intU = 0;
                                    foreach (string l in sArray)
                                    {
                                        intU++;
                                    }
                                    if (intU == 2)
                                        dr["PU"] = sArray[1].Replace("PU", "");
                                    else
                                        dr["PU"] = sArray[0].Replace("PU", "");

                                    dr["ModelName"] = strGName;
                                    dr["Period"] = "N";
                                    for (intU = 1; intU < 32; intU++)
                                    {
                                        dr["D" + intU.ToString()] = "";
                                    }
                                    dr["Auto"] = "0";
                                    dr["Manual"] = "0";
                                    dr["Total"] = "0";
                                    dr["ID"] = strID;

                                    dt_new.Rows.Add(dr);

                                    intAuto = 0;
                                    intManual = 0;
                                    for (intU = 1; intU < 32; intU++)
                                    {
                                        strDay[intU] = "";
                                    }
                                }
                            }
                            else
                            {


                                dr = dt_new.NewRow();


                                dr["Name"] = strName;
                                dr["Products_ID"] = strProducts_ID;
                                int intIndex, intIndex1;
                                intIndex = strCustomer.IndexOf("(");
                                if (intIndex < 0)
                                    dr["Customer"] = strCustomer;
                                else
                                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);


                                string strDepartment2;


                                intIndex = strDepartment.IndexOf("(");
                                intIndex1 = strDepartment.IndexOf(")");
                                if (intIndex > 0)
                                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                else
                                    strDepartment2 = strDepartment;

                                dr["Department"] = strDepartment2;

                                string[] sArray = strDepartment.Split('-');
                                int intU = 0;
                                foreach (string l in sArray)
                                {
                                    intU++;
                                }
                                if (intU == 2)
                                    dr["PU"] = sArray[1].Replace("PU", "");
                                else
                                    dr["PU"] = sArray[0].Replace("PU", "");

                                dr["ModelName"] = strGName;
                                dr["Period"] = strPeriod;


                                //startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                                //endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                                //while (startDate < endDate)
                                //{
                                //    string strDateW;

                                //    strDateW = startDate.Day.ToString();

                                //    strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();

                                //    startDate = startDate.AddDays(1);

                                //    if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                //        intAuto++;
                                //    else
                                //        intManual++;

                                //}


                                for (intU = 1; intU < 32; intU++)
                                {
                                    dr["D" + intU.ToString()] = strDay[intU];
                                }

                                dr["Auto"] = intAuto.ToString();
                                dr["Manual"] = intManual.ToString();
                                dr["Total"] = (intAuto + intManual).ToString();
                                dr["ID"] = strID;

                                dt_new.Rows.Add(dr);


                                intAuto = 0;
                                intManual = 0;
                                for (intU = 1; intU < 32; intU++)
                                {
                                    strDay[intU] = "";
                                }
                            }
                        }
                        else
                        {
                            int intIndex, intIndex1;
                            string strDepartment2;
                            string[] sArray;
                            int intU = 0;
                            intManual = 0;
                            intAuto = 0;


                            if (dt1.Rows[intJ]["Period"].ToString() == "N")
                            {
                                dr = dt_new.NewRow();

                                dr["Name"] = strName;
                                dr["Products_ID"] = strProducts_ID;
                                intIndex = strCustomer.IndexOf("(");
                                if (intIndex < 0)
                                    dr["Customer"] = strCustomer;
                                else
                                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);



                                intIndex = strDepartment.IndexOf("(");
                                intIndex1 = strDepartment.IndexOf(")");
                                if (intIndex > 0)
                                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                else
                                    strDepartment2 = strDepartment;

                                dr["Department"] = strDepartment2;

                                sArray = strDepartment.Split('-');
                                foreach (string l in sArray)
                                {
                                    intU++;
                                }
                                if (intU == 2)
                                    dr["PU"] = sArray[1].Replace("PU", "");
                                else
                                    dr["PU"] = sArray[0].Replace("PU", "");

                                dr["ModelName"] = strGName;
                                dr["Period"] = "D";
                                for (intU = 1; intU < 32; intU++)
                                {
                                    dr["D" + intU.ToString()] = "";
                                }
                                dr["Auto"] = "0";
                                dr["Manual"] = "0";
                                dr["Total"] = "0";
                                dr["ID"] = strID;

                                dt_new.Rows.Add(dr);

                                intAuto = 0;
                                intManual = 0;
                                for (intU = 1; intU < 32; intU++)
                                {
                                    strDay[intU] = "";
                                }
                            }

                            dr = dt_new.NewRow();

                            dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                            dr["Products_ID"] = dt1.Rows[intJ]["Products_ID"].ToString();
                            intIndex = dt1.Rows[intJ]["Customer"].ToString().IndexOf("(");
                            if (intIndex < 0)
                                dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString();
                            else
                                dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString().Substring(1, intIndex - 1);




                            intIndex = dt1.Rows[intJ]["Department"].ToString().IndexOf("(");
                            intIndex1 = dt1.Rows[intJ]["Department"].ToString().IndexOf(")");
                            if (intIndex > 0)
                                strDepartment2 = dt1.Rows[intJ]["Department"].ToString().Substring(intIndex + 1, intIndex1 - intIndex - 1);
                            else
                                strDepartment2 = dt1.Rows[intJ]["Department"].ToString();

                            dr["Department"] = strDepartment2;

                            sArray = dt1.Rows[intJ]["Department"].ToString().Split('-');
                            intU = 0;
                            foreach (string l in sArray)
                            {
                                intU++;
                            }
                            if (intU == 2)
                                dr["PU"] = sArray[1].Replace("PU", "");
                            else
                                dr["PU"] = sArray[0].Replace("PU", "");

                            dr["ModelName"] = dt1.Rows[intJ]["GName"].ToString();
                            dr["Period"] = dt1.Rows[intJ]["Period"].ToString();

                            startDate = Convert.ToDateTime(dt1.Rows[intJ]["StartDate"].ToString());
                            endDate = Convert.ToDateTime(dt1.Rows[intJ]["EndDate"].ToString());
                            DateTime strStartDate1 = Convert.ToDateTime(strStartDate);
                            if (startDate < strStartDate1)
                                startDate = strStartDate1;
                            //intU = 0;
                            for (intU = 1; intU < 32; intU++)
                            {
                                strDay[intU] = "";
                            }
                            while (startDate <= endDate)
                            {
                                string strDateW;

                                strDateW = startDate.Day.ToString();

                                //dr["D" + intU.ToString()] = dt1.Rows[intJ]["UseKind"].ToString();
                                //dr["D" + Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();
                                strDay[Convert.ToInt32(strDateW)] = dt1.Rows[intJ]["UseKind"].ToString();
                                startDate = startDate.AddDays(1);

                                if (dt1.Rows[intJ]["UseKind"].ToString() == "A")
                                    intAuto++;
                                else if (dt1.Rows[intJ]["UseKind"].ToString() == "M")
                                    intManual++;

                            }

                            for (intU = 1; intU < 32; intU++)
                            {
                                dr["D" + intU.ToString()] = strDay[intU];
                            }
                            dr["Auto"] = intAuto.ToString();
                            dr["Manual"] = intManual.ToString();
                            dr["Total"] = (intAuto + intManual).ToString();
                            dr["ID"] = strID;

                            dt_new.Rows.Add(dr);

                            intAuto = 0;
                            intManual = 0;
                            for (intU = 1; intU < 32; intU++)
                            {
                                strDay[intU] = "";
                            }

                            if (dt1.Rows[intJ]["Period"].ToString() == "D")
                            {
                                dr = dt_new.NewRow();

                                dr["Name"] = strName;
                                dr["Products_ID"] = strProducts_ID;
                                intIndex = strCustomer.IndexOf("(");
                                if (intIndex < 0)
                                    dr["Customer"] = strCustomer;
                                else
                                    dr["Customer"] = strCustomer.Substring(1, intIndex - 1);




                                intIndex = strDepartment.IndexOf("(");
                                intIndex1 = strDepartment.IndexOf(")");
                                if (intIndex > 0)
                                    strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                                else
                                    strDepartment2 = strDepartment;

                                dr["Department"] = strDepartment2;

                                sArray = strDepartment.Split('-');
                                intU = 0;
                                foreach (string l in sArray)
                                {
                                    intU++;
                                }
                                if (intU == 2)
                                    dr["PU"] = sArray[1].Replace("PU", "");
                                else
                                    dr["PU"] = sArray[0].Replace("PU", "");

                                dr["ModelName"] = strGName;
                                dr["Period"] = "N";
                                for (intU = 1; intU < 32; intU++)
                                {
                                    dr["D" + intU.ToString()] = "";
                                }
                                dr["Auto"] = "0";
                                dr["Manual"] = "0";
                                dr["Total"] = "0";
                                dr["ID"] = strID;

                                dt_new.Rows.Add(dr);

                                intAuto = 0;
                                intManual = 0;
                                for (intU = 1; intU < 32; intU++)
                                {
                                    strDay[intU] = "";
                                }
                            }

                        }
                    }
                }
            }


        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        if (dt1.Rows.Count > 0)
        {
            if (rdoKind1.Checked == true)
            {
                BindChart1(strStartDate, strEndDate);
                BindColumnChart(gvwMain, 38, 39);
            }
        }
        //dtColumnChart();

        //if (rdoKind1.Checked == true)
        //{
        //    //BindChart(strStartDate);
        //    string strAID = "";
        //    TableRow row = new TableRow();
        //    //for (int intI = 0; intI < dt_new.Rows.Count; intI++)
        //    for (int intJ = 0; intJ < this.gvwMain.Rows.Count; intJ++)
        //    {
        //        if (intJ == 0)
        //        {
        //            strAID = ((Label)this.gvwMain.Rows[intJ].Cells[41].FindControl("lblGVSeq")).Text;


        //            Label lbl1 = new Label();
        //            lbl1.Text = "No.1";
        //            TableCell cell1 = new TableCell();
        //            Literal liter1 = new Literal();
        //            Literal liter2 = new Literal();
        //            //liter1.Text = "<div id=\"piechart_div\" style=\"border: 1px solid #ccc\"></div>";
        //            //=============================



        //            //=============================

        //            liter1.Text = BindChart(strStartDate, strAID,intJ).ToString();
        //            cell1.Controls.Add(liter1);
        //            row.Cells.Add(cell1);
        //            liter2.Text = "<div id=\"piechart_3d\" style=\"border: 1px solid #ccc\"></div>";

        //            cell1.Controls.Add(liter2);
        //            row.Cells.Add(cell1);
        //            tableChart.Rows.Add(row);
        //        }

        //if (strAID != dt_new.Rows[intI]["ID"].ToString())
        //{
        //    //TableRow row = new TableRow();
        //    Label lbl1 = new Label();
        //    lbl1.Text = "No.1";
        //    TableCell cell1 = new TableCell();
        //    Literal liter1 = new Literal();
        //    //liter1.Text = "<div id=\"piechart_div\" style=\"border: 1px solid #ccc\"></div>";
        //    liter1.Text = BindChart(strStartDate, strAID).ToString();
        //    cell1.Controls.Add(liter1);
        //    row.Cells.Add(cell1);
        //    tableChart.Rows.Add(row);

        //    strAID = dt_new.Rows[intI]["ID"].ToString();
        //}
        //}
        //}
    }

    private void getDataM()
    {
        string strStartDate = "";
        string strEndDate = "";



        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Products_ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Products_ID";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Department");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Department";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Period");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Period";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("UseKind");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "UseKind";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("M1A");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "M1A";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("M1M");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "M1M";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("M2A");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "M2A";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("M2M");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "M2M";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("M3A");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "M3A";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("M3M");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "M3M";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("M4A");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "M4A";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("M4M");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "M4M";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("M5A");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "M5A";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);

        DataColumn column15 = new DataColumn("M5M");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "M5M";
        column15.DefaultValue = "0";
        dt_new.Columns.Add(column15);

        DataColumn column16 = new DataColumn("M6A");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "M6A";
        column16.DefaultValue = "0";
        dt_new.Columns.Add(column16);

        DataColumn column17 = new DataColumn("M6M");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "M6M";
        column17.DefaultValue = "0";
        dt_new.Columns.Add(column17);

        DataColumn column18 = new DataColumn("M7A");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "M7A";
        column18.DefaultValue = "0";
        dt_new.Columns.Add(column18);

        DataColumn column19 = new DataColumn("M7M");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "M7M";
        column19.DefaultValue = "0";
        dt_new.Columns.Add(column19);

        DataColumn column20 = new DataColumn("M8A");
        column20.DataType = System.Type.GetType("System.String");
        column20.AllowDBNull = true;
        column20.Caption = "M8A";
        column20.DefaultValue = "0";
        dt_new.Columns.Add(column20);

        DataColumn column21 = new DataColumn("M8M");
        column21.DataType = System.Type.GetType("System.String");
        column21.AllowDBNull = true;
        column21.Caption = "M8M";
        column21.DefaultValue = "0";
        dt_new.Columns.Add(column21);

        DataColumn column22 = new DataColumn("M9A");
        column22.DataType = System.Type.GetType("System.String");
        column22.AllowDBNull = true;
        column22.Caption = "M9A";
        column22.DefaultValue = "0";
        dt_new.Columns.Add(column22);

        DataColumn column23 = new DataColumn("M9M");
        column23.DataType = System.Type.GetType("System.String");
        column23.AllowDBNull = true;
        column23.Caption = "M9M";
        column23.DefaultValue = "0";
        dt_new.Columns.Add(column23);

        DataColumn column24 = new DataColumn("M10A");
        column24.DataType = System.Type.GetType("System.String");
        column24.AllowDBNull = true;
        column24.Caption = "M10A";
        column24.DefaultValue = "0";
        dt_new.Columns.Add(column24);

        DataColumn column25 = new DataColumn("M10M");
        column25.DataType = System.Type.GetType("System.String");
        column25.AllowDBNull = true;
        column25.Caption = "M10M";
        column25.DefaultValue = "0";
        dt_new.Columns.Add(column25);

        DataColumn column26 = new DataColumn("M11A");
        column26.DataType = System.Type.GetType("System.String");
        column26.AllowDBNull = true;
        column26.Caption = "M11A";
        column26.DefaultValue = "0";
        dt_new.Columns.Add(column26);

        DataColumn column27 = new DataColumn("M11M");
        column27.DataType = System.Type.GetType("System.String");
        column27.AllowDBNull = true;
        column27.Caption = "M11M";
        column27.DefaultValue = "0";
        dt_new.Columns.Add(column27);

        DataColumn column28 = new DataColumn("M12A");
        column28.DataType = System.Type.GetType("System.String");
        column28.AllowDBNull = true;
        column28.Caption = "M12A";
        column28.DefaultValue = "0";
        dt_new.Columns.Add(column28);

        DataColumn column29 = new DataColumn("M12M");
        column29.DataType = System.Type.GetType("System.String");
        column29.AllowDBNull = true;
        column29.Caption = "M12M";
        column29.DefaultValue = "0";
        dt_new.Columns.Add(column29);

        DataColumn column30 = new DataColumn("Auto");
        column30.DataType = System.Type.GetType("System.String");
        column30.AllowDBNull = true;
        column30.Caption = "Auto";
        column30.DefaultValue = "0";
        dt_new.Columns.Add(column30);

        DataColumn column31 = new DataColumn("Manual");
        column31.DataType = System.Type.GetType("System.String");
        column31.AllowDBNull = true;
        column31.Caption = "Manual";
        column31.DefaultValue = "0";
        dt_new.Columns.Add(column31);

        DataColumn column32 = new DataColumn("Total");
        column32.DataType = System.Type.GetType("System.String");
        column32.AllowDBNull = true;
        column32.Caption = "Total";
        column32.DefaultValue = "0";
        dt_new.Columns.Add(column32);

        DataColumn column33 = new DataColumn("ID");
        column33.DataType = System.Type.GetType("System.String");
        column33.AllowDBNull = true;
        column33.Caption = "ID";
        column33.DefaultValue = "0";
        dt_new.Columns.Add(column33);

        DataTable dt1;

        int intMStart = Convert.ToInt32(ddlMonthS.Text);
        int intMEnd = Convert.ToInt32(ddlMonthE.Text);

        DataRow dr;
        string strName = "";
        string strProducts_ID = "";
        string strDepartment = "";
        string strID = "";
        string[] strMonth = new string[13];
        int[] intAuto = new int[13];
        int[] intManual = new int[13];
        int intIndex, intIndex1;
        string strDepartment2;
        int intU = 0;
        string strLocal;

        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";


        if (rdoDepartment.Checked == true)
        {
            if (rdoCustodian.Checked == true)
                dt1 = clsData.UploadAChart_View("0", ddlDepartment.Text, ddlCustodian.Text, strLocal);
            else
                dt1 = clsData.UploadAChart_View("1", ddlDepartment.Text, ddlKind.Text, strLocal);

        }
        else if (rdoProducts_ID.Checked == true)
            dt1 = clsData.UploadAChart_View("2", txtProducts_ID.Text, "", strLocal);
        else
            dt1 = clsData.UploadAChart_View("3", ddlKind1.Text, "", strLocal);

        DataTable dt2;

        for (int intACount = 0; intACount < dt1.Rows.Count; intACount++)
        {
            strName = dt1.Rows[intACount]["Name"].ToString();
            strProducts_ID = dt1.Rows[intACount]["Products_ID"].ToString();
            strID = dt1.Rows[intACount]["ID"].ToString();



            strStartDate = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";


            if ((txtYearS.Text == "") || (txtYearE.Text == ""))
                clsMsg.AlertMessage("請輸入日期區間！", this.Page);
            else
            {
                if ((Convert.ToInt32(ddlMonthE.Text) < 9))
                {
                    if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
                    {
                        if (ddlMonthE.Text == "02")
                        {
                            if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                            else
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                        }
                        else
                        {
                            if (ddlMonthE.Text == "08")
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                            else
                                strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                        }
                    }
                    else
                        strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                }
                else
                {
                    if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                        strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                    else
                        strEndDate = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                }


            }

            DataTable dt3;

            if (rdoDepartment.Checked == true)
            {
                if (rdoCustodian.Checked == true)
                    dt3 = clsData.UploadApparatusReportM("0", ddlDepartment.Text, dt1.Rows[intACount]["ID"].ToString(), strStartDate, strEndDate, "0", "", "");
                else
                    dt3 = clsData.UploadApparatusReportM("1", "", dt1.Rows[intACount]["ID"].ToString(), strStartDate, strEndDate, "0", "", "");

            }
            else
                dt3 = clsData.UploadApparatusReportM("3", "", dt1.Rows[intACount]["ID"].ToString(), strStartDate, strEndDate, "0", "", "");

            if (dt3.Rows.Count == 0)
            {
                for (int intI = 0; intI < 2; intI++)
                {
                    dr = dt_new.NewRow();


                    dr["Name"] = strName;
                    dr["Products_ID"] = strProducts_ID;
                    dr["ID"] = strID;

                    strDepartment = "";

                    intIndex = strDepartment.IndexOf("(");
                    intIndex1 = strDepartment.IndexOf(")");
                    if (intIndex > 0)
                        strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                    else
                        strDepartment2 = strDepartment;
                    dr["Department"] = strDepartment2;
                    for (intU = 1; intU < 13; intU++)
                    {
                        dr["M" + intU.ToString() + "A"] = "0";
                        dr["M" + intU.ToString() + "M"] = "0";
                    }

                    if (intI == 0)
                        dr["Period"] = "D";
                    else
                        dr["Period"] = "N";

                    dr["Auto"] = "0";
                    dr["Manual"] = "0";
                    dr["Total"] = "0";

                    dt_new.Rows.Add(dr);
                }
            }
            for (int intDCount = 0; intDCount < dt3.Rows.Count; intDCount++)
            {

                for (int intX = 1; intX < 13; intX++)
                {
                    intAuto[intX] = 0;
                    intManual[intX] = 0;
                }
                for (int intDay = 0; intDay < 4; intDay++)
                {


                    //for (int intX = 1; intX < 13; intX++)
                    //{
                    //    intAuto[intX] = 0;
                    //    intManual[intX] = 0;
                    //}


                    for (int intMonth = intMStart; intMonth <= intMEnd; intMonth++)
                    {
                        strStartDate = txtYearS.Text.Trim() + "/" + intMonth.ToString() + "/01";


                        if ((txtYearS.Text == "") || (txtYearE.Text == ""))
                            clsMsg.AlertMessage("請輸入日期區間！", this.Page);
                        else
                        {
                            if ((Convert.ToInt32(intMonth.ToString()) < 9))
                            {
                                if ((Convert.ToInt32(intMonth.ToString()) % 2) == 0)
                                {
                                    if (intMonth == 2)
                                    {
                                        if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                                            strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/29";
                                        else
                                            strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/28";
                                    }
                                    else
                                    {
                                        if (intMonth == 8)
                                            strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/31";
                                        else
                                            strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/30";
                                    }
                                }
                                else
                                    strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/31";
                            }
                            else
                            {
                                if ((intMonth == 9) || (intMonth == 11))
                                    strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/30";
                                else
                                    strEndDate = txtYearE.Text + "/" + intMonth.ToString() + "/31";
                            }


                        }

                        string strDay, strKindUse;
                        if (intDay == 0)
                        {
                            strDay = "D";
                            strKindUse = "A";
                        }
                        else if (intDay == 1)
                        {
                            strDay = "D";
                            strKindUse = "M";
                        }
                        else if (intDay == 2)
                        {
                            strDay = "N";
                            strKindUse = "A";
                        }
                        else
                        {
                            strDay = "N";
                            strKindUse = "M";
                        }

                        string strSelect = "0";
                        //if (Convert.ToDateTime(strStartDate) > Convert.ToDateTime(dt3.Rows[intACount]["StartDate"].ToString()))
                        //    strSelect = "1";
                        //else
                        //    strSelect = "0";

                        if (rdoDepartment.Checked == true)
                        {
                            if (rdoCustodian.Checked == true)
                                dt2 = clsData.UploadApparatusReportM1("0", ddlDepartment.Text, dt1.Rows[intACount]["ID"].ToString(), strStartDate, strEndDate, strSelect, strDay, strKindUse);
                            else
                                dt2 = clsData.UploadApparatusReportM1("1", dt3.Rows[intDCount]["Department"].ToString(), dt1.Rows[intACount]["ID"].ToString(), strStartDate, strEndDate, strSelect, strDay, strKindUse);

                        }
                        else
                            dt2 = clsData.UploadApparatusReportM1("3", dt3.Rows[intDCount]["Department"].ToString(), dt1.Rows[intACount]["ID"].ToString(), strStartDate, strEndDate, strSelect, strDay, strKindUse);




                        for (int intCountI = 0; intCountI < dt2.Rows.Count; intCountI++)
                        {



                            if ((intDay == 0) || (intDay == 2))
                            {
                                if (dt2.Rows[intCountI]["UseKind"].ToString() == "A")
                                {
                                    //dr["M" + intU.ToString() + "A"] = dt2.Rows[intCountI]["daycount"].ToString();
                                    //intAuto = intAuto + Convert.ToInt32(dt2.Rows[intCountI]["daycount"].ToString());
                                    intAuto[intMonth] = Convert.ToInt32(dt2.Rows[intCountI]["daycount"].ToString());

                                }
                            }
                            else
                            {
                                if (dt2.Rows[intCountI]["UseKind"].ToString() == "M")
                                {
                                    //dr["M" + intU.ToString() + "M"] = dt2.Rows[intCountI]["daycount"].ToString();
                                    //intManual = intManual + Convert.ToInt32(dt2.Rows[intCountI]["daycount"].ToString());
                                    intManual[intMonth] = Convert.ToInt32(dt2.Rows[intCountI]["daycount"].ToString());
                                }

                            }
                        }




                    }

                    if ((intDay == 1) || (intDay == 3))
                    {
                        dr = dt_new.NewRow();


                        dr["Name"] = strName;
                        dr["Products_ID"] = strProducts_ID;
                        dr["ID"] = strID;

                        strDepartment = dt3.Rows[intDCount]["Department"].ToString();

                        intIndex = strDepartment.IndexOf("(");
                        intIndex1 = strDepartment.IndexOf(")");
                        if (intIndex > 0)
                            strDepartment2 = strDepartment.Substring(intIndex + 1, intIndex1 - intIndex - 1);
                        else
                            strDepartment2 = strDepartment;
                        dr["Department"] = strDepartment2;
                        for (intU = 1; intU < 13; intU++)
                        {
                            dr["M" + intU.ToString() + "A"] = intAuto[intU].ToString();
                            dr["M" + intU.ToString() + "M"] = intManual[intU].ToString();
                        }

                        if (intDay == 1)
                            dr["Period"] = "D";
                        else
                            dr["Period"] = "N";

                        int intTAuto = 0;
                        int intTManual = 0;

                        for (intU = 1; intU < 13; intU++)
                        {
                            intTAuto = intTAuto + intAuto[intU];
                            intTManual = intTManual + intManual[intU];
                        }
                        dr["Auto"] = intTAuto.ToString();
                        dr["Manual"] = intTManual.ToString();
                        dr["Total"] = (intTAuto + intTManual).ToString();

                        dt_new.Rows.Add(dr);

                        for (int intX = 1; intX < 13; intX++)
                        {
                            intAuto[intX] = 0;
                            intManual[intX] = 0;
                        }
                    }


                }
            }
        }

        gvwMain1.DataSource = dt_new;
        gvwMain1.DataBind();

        if (dt1.Rows.Count > 0)
        {
            if (rdoKind1.Checked == true)
            {
                BindLineChart(strStartDate, strEndDate);
                BindColumnChart(gvwMain1, 28, 29);
            }
        }

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //Response.ContentType = "application/x-msexcel";
        //Response.AddHeader("Content-Disposition", "attachment;filename=Application.xls");
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        //StringWriter tw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(tw);
        //tb1.RenderControl(hw);
        ////Response.Write(strStyle);
        //Response.Write(tw.ToString());
        //Response.End();
        export_excel("Report", 1);
    }

    private void export_excel(string filename, int t_mode)
    {
        //  呼叫方式 export_excel("gridview1", "output",1);
        // export_excel(要匯出的 Gridview 名稱, 匯出的檔名,模式);  // 1=會加入日期時間
        //GridView xgv = (GridView)FindControl(gvname);
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
        gvwMain.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }
}
