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

public partial class WebForm_PR_Statistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["EmpNo"] == null)
        //    Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {

            int iYear = System.DateTime.Now.Year;


            txtYearM1.Text = iYear.ToString();

            Session["EmpDepartment"] = "DA40";
            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;


        }
    }

    protected void gvwMain_PreRender(object sender, EventArgs e)
    {
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double dValue;

            for (int intI = 1; intI < 14; intI++)
            {
                dValue = Convert.ToDouble(e.Row.Cells[intI].Text);
                e.Row.Cells[intI].Text = dValue.ToString("N2");
            }

        }
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        GvQuery();
        BindLineChart();
        BindColumnChart();
    }

    private void GvQuery()
    {
        string strStart, strEnd;

        //gvwMain.Visible = false;
        //gvwMain1.Visible = true;
        for (int intW = 0; intW <= 13; intW++)
        {
            gvwMain.Columns[intW].Visible = true;
        }

        strStart = txtYearM1.Text + "/01/01";
        strEnd = txtYearM1.Text + "/12/31";




        DataTable dt_new = new DataTable("dt_new");

        //DataColumn column1 = new DataColumn("Name");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "Name";
        //column1.DefaultValue = "0";
        //dt_new.Columns.Add(column1);

        DataColumn column1 = new DataColumn("Kind");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Kind";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("C1");
        column2.DataType = System.Type.GetType("System.Double");
        column2.AllowDBNull = true;
        column2.Caption = "C1";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("C2");
        column3.DataType = System.Type.GetType("System.Double");
        column3.AllowDBNull = true;
        column3.Caption = "C2";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("C3");
        column4.DataType = System.Type.GetType("System.Double");
        column4.AllowDBNull = true;
        column4.Caption = "C3";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("C4");
        column5.DataType = System.Type.GetType("System.Double");
        column5.AllowDBNull = true;
        column5.Caption = "C4";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("C5");
        column6.DataType = System.Type.GetType("System.Double");
        column6.AllowDBNull = true;
        column6.Caption = "C5";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("C6");
        column7.DataType = System.Type.GetType("System.Double");
        column7.AllowDBNull = true;
        column7.Caption = "C6";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("C7");
        column8.DataType = System.Type.GetType("System.Double");
        column8.AllowDBNull = true;
        column8.Caption = "C7";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("C8");
        column9.DataType = System.Type.GetType("System.Double");
        column9.AllowDBNull = true;
        column9.Caption = "C8";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("C9");
        column10.DataType = System.Type.GetType("System.Double");
        column10.AllowDBNull = true;
        column10.Caption = "C9";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("C10");
        column11.DataType = System.Type.GetType("System.Double");
        column11.AllowDBNull = true;
        column11.Caption = "C10";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("C11");
        column12.DataType = System.Type.GetType("System.Double");
        column12.AllowDBNull = true;
        column12.Caption = "C11";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("C12");
        column13.DataType = System.Type.GetType("System.Double");
        column13.AllowDBNull = true;
        column13.Caption = "C12";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("Total");
        column14.DataType = System.Type.GetType("System.Double");
        column14.AllowDBNull = true;
        column14.Caption = "Total";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);



        int intMonth = getMonths(strStart, strEnd);
        int intMonth1, intMonth2;
        //intMonth1 = Convert.ToInt32(ddlMonthS.Text);
        //intMonth2 = Convert.ToInt32(ddlMonthE.Text);
        string strYear = txtYearM1.Text;
        string strDateRage, strDateRage1;
        strDateRage = "";
        for (int intI = 1; intI <= 12; intI++)
        {
            //if (intMonth1 >= 13)
            //{
            //    intMonth1 = 1;
            //    strYear = txtYearM1.Text;
            //}
            strDateRage1 = strYear + "/" + intI.ToString();
            strDateRage = strDateRage + "[" + strDateRage1 + "]";

            //intMonth1++;


            if (intI != 12)
                strDateRage = strDateRage + ",";
        }

        Session["DateRage"] = strDateRage;
        string strLocal;
        if (rdoLocal.Checked == true)
            strLocal = "台北";
        else
            strLocal = "吳江";

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
        //strName = ddlEmp1.Text;
        //else
        //    strName = dtEmp.Rows[intI]["Name_En"].ToString();
        DataTable dt;
        //for (int intI = 0; intI < 2; intI++)
        //{
        //    if (intI == 0)
        //    {
        //        dt = clsData.UploadManpowerReport1_Month_O(strName, strStart, strEnd, strDateRage);
        //    }
        //    else
        //    {
        double[] dMoney = new double[12];
        int intW1;
        double dTotal = 0;
        double dTotal1 = 0;
        for (int intJ = 0; intJ < 3; intJ++)
        {
            if (ddlStatus.Text == "Open")
                dt = clsData.UploadPR_Statistics_open(strStart, strEnd, strDateRage, strLocal, intJ.ToString(), ddlStatus.Text);
            else
                dt = clsData.UploadPR_Statistics(strStart, strEnd, strDateRage, strLocal, intJ.ToString(), ddlStatus.Text);

            for (int intX = 0; intX < dt.Rows.Count; intX++)
            {
                if (intJ == 0)
                {
                    dr = dt_new.NewRow();
                    dr["Kind"] = dt.Rows[intX]["Kind"].ToString();
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


                    intW1 = 1;
                    dTotal = 0;
                    dTotal1 = 0;

                    strYear = txtYearM1.Text;
                    for (int intW = 1; intW <= 12; intW++)
                    {

                        strYear = txtYearM1.Text;

                        strDateRage1 = strYear + "/" + intW.ToString();


                        if ((dt.Rows[intX][strDateRage1].ToString() == null) || (dt.Rows[intX][strDateRage1].ToString() == ""))
                            dTotal1 = 0;
                        else
                            dTotal1 = Convert.ToDouble(dt.Rows[intX][strDateRage1].ToString());
                        dTotal = dTotal + dTotal1;

                        dr["C" + intW1.ToString()] = dTotal1;

                        intW1++;
                    }

                    dr["Total"] = dTotal;

                    dt_new.Rows.Add(dr);
                }
                else if (intJ == 1)
                {
                    intW1 = 1;
                    dTotal = 0;
                    dTotal1 = 0;
                    for (int intW = 1; intW <= 12; intW++)
                    {

                        strYear = txtYearM1.Text;

                        strDateRage1 = strYear + "/" + intW.ToString();


                        if ((dt.Rows[0][strDateRage1].ToString() == null) || (dt.Rows[0][strDateRage1].ToString() == ""))
                            dTotal1 = 0;
                        else
                            dTotal1 = Convert.ToDouble(dt.Rows[0][strDateRage1].ToString());
                        dTotal = dTotal + dTotal1;

                        dMoney[intW - 1] = dTotal1;

                        intW1++;
                    }
                }
                else
                {
                    dr = dt_new.NewRow();
                    dr["Kind"] = dt.Rows[0]["Kind"].ToString();
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


                    intW1 = 1;
                    //dTotal = 0;
                    //dTotal1 = 0;

                    strYear = txtYearM1.Text;
                    for (int intW = 1; intW <= 12; intW++)
                    {

                        strYear = txtYearM1.Text;

                        strDateRage1 = strYear + "/" + intW.ToString();


                        if ((dt.Rows[0][strDateRage1].ToString() == null) || (dt.Rows[0][strDateRage1].ToString() == ""))
                            dTotal1 = 0;
                        else
                            dTotal1 = Convert.ToDouble(dt.Rows[0][strDateRage1].ToString());
                        dTotal = dTotal + dTotal1;

                        dr["C" + intW1.ToString()] = dMoney[intW - 1] + dTotal1;

                        intW1++;
                    }

                    dr["Total"] = dTotal;

                    dt_new.Rows.Add(dr);
                }
            }

        }
        //}

        //for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
        //{



        //    dr = dt_new.NewRow();
        //    //dr["Name"] = strName;
        //    dr["PR_Kind"] = dt.Rows[intJ]["Kind1"].ToString();
        //    dr["C1"] = 0;
        //    dr["C2"] = 0;
        //    dr["C3"] = 0;
        //    dr["C4"] = 0;
        //    dr["C5"] = 0;
        //    dr["C6"] = 0;
        //    dr["C7"] = 0;
        //    dr["C8"] = 0;
        //    dr["C9"] = 0;
        //    dr["C10"] = 0;
        //    dr["C11"] = 0;
        //    dr["C12"] = 0;
        //    dr["Total"] = 0;
        //    //dr["Metric"] = 0;

        //    intW1 = 1;
        //    dTotal = 0;
        //    dTotal1 = 0;
        //    //double dMetric = 0;

        //    //intMonth1 = Convert.ToInt32(ddlMonthS.Text);

        //    strYear = txtYearM1.Text;
        //    for (int intW = 1; intW <= 12; intW++)
        //    {

        //        strYear = txtYearM1.Text;

        //        strDateRage1 = strYear + "/" + intW.ToString();



        //        if ((dt.Rows[intJ][strDateRage1].ToString() == null) || (dt.Rows[intJ][strDateRage1].ToString() == ""))
        //            dTotal1 = 0;
        //        else
        //            dTotal1 = Convert.ToDouble(dt.Rows[intJ][strDateRage1].ToString());
        //        dTotal = dTotal + dTotal1;

        //        dr["C" + intW1.ToString()] = dTotal1;

        //        intW1++;
        //    }

        //    dr["Total"] = dTotal;




        //    dt_new.Rows.Add(dr);
        //}

        //}

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();



    }

    public static int getMonths(string strFrom, string strTo)
    {
        DateTime dtStart = DateTime.Parse(strFrom);
        DateTime dtEnd = DateTime.Parse(strTo);


        int iMonths = dtEnd.Year * 12 + dtEnd.Month - (dtStart.Year * 12 + dtStart.Month) + 1;
        return iMonths;


    }

    private void BindColumnChart()
    {
        StringBuilder strScript = new StringBuilder();

        string strTital = "['月份',";
        int intI;
        for (intI = 0; intI < gvwMain.Rows.Count; intI++)
        {
            strTital = strTital + "'" + gvwMain.Rows[intI].Cells[0].Text + "'";

            if (intI != gvwMain.Rows.Count - 1)
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

        int intCells = 1;
        string strValue;
        for (int intJ = 0; intJ < 12; intJ++)
        {
            strScript.Append("['" + sDate[intJ] + "',");
            for (intI = 0; intI < gvwMain.Rows.Count; intI++)
            {
                strValue = gvwMain.Rows[intI].Cells[intCells].Text.Replace(",", "");
                strScript.Append(strValue);
                if (intI == gvwMain.Rows.Count - 1)
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

    private void BindLineChart()
    {
        int intRowCount = 0;
        int intChartCount = 0;
        //TableCell TCell;
        //TableRow TRow;
        string strAID = "";
        string strAName = "";
        DataTable dt;
        string strToday = DateTime.Now.ToString("yyyy/MM/dd");

        double[] intEvent = new double[13];

        int intCount = Convert.ToInt16(Math.Ceiling(Convert.ToDouble(gvwMain.Rows.Count) / 2));
        int intCount1 = gvwMain.Rows.Count;

        for (int intI = 0; intI < intCount1; intI++)
        {

            //TRow = new TableRow();
            //for (int intJ = 0; intJ < 2; intJ++)
            //{
            //int intTotal = 0;
            //TCell = new TableCell();
            //================================
            DataTable dt_new1 = new DataTable("dt_new1");


            DataColumn column1 = new DataColumn("Event");
            column1.DataType = System.Type.GetType("System.String");
            column1.AllowDBNull = true;
            column1.Caption = "Auto";
            column1.DefaultValue = "0";
            dt_new1.Columns.Add(column1);


            DataRow dr;
            //Literal liter1 = new Literal();
            //Literal liter2 = new Literal();
            //=============================
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();

            if (intRowCount == gvwMain.Rows.Count - 1)
            {
                if ((gvwMain.Rows[intRowCount].Cells[0].Text == "") || (gvwMain.Rows[intRowCount].Cells[0].Text == "&nbsp;"))
                    strAName = "";
                else
                    strAName = gvwMain.Rows[intRowCount].Cells[0].Text;

                //int intMonth1 = 1;


                for (int intMCount = 1; intMCount < 13; intMCount++)
                {
                    dr = dt_new1.NewRow();

                    dr["Event"] = gvwMain.Rows[intRowCount].Cells[intMCount].Text;

                    dt_new1.Rows.Add(dr);
                }

                //intRowCount++;
                //intJ = 2;
            }
            else
            {


                if ((gvwMain.Rows[intRowCount].Cells[0].Text == "") || (gvwMain.Rows[intRowCount].Cells[0].Text == "&nbsp;"))
                    strAName = "";
                else
                    strAName = gvwMain.Rows[intRowCount].Cells[0].Text;

                //string strDepartment = "";
                int intX = 0;
                while (intX == 0)
                {
                    if (intRowCount < gvwMain.Rows.Count)
                    {
                        if (intRowCount == gvwMain.Rows.Count - 1)
                        {
                            //intJ = 2;
                            intX = 1;
                            for (int intMCount = 1; intMCount < 13; intMCount++)
                            {
                                dr = dt_new1.NewRow();

                                dr["Event"] = gvwMain.Rows[intRowCount].Cells[intMCount].Text;

                                dt_new1.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            intX = 1;
                            if (intX == 1)
                            {
                                for (int intMCount = 1; intMCount < 13; intMCount++)
                                {
                                    dr = dt_new1.NewRow();

                                    dr["Event"] = gvwMain.Rows[intRowCount].Cells[intMCount].Text;

                                    dt_new1.Rows.Add(dr);
                                }
                            }

                        }
                    }
                    else
                    {
                        //intJ = 2;
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

            string strDate = Session["DateRage"].ToString().Replace("[", "");
            strDate = strDate.Replace("]", "");
            string[] sArray = strDate.Split(',');

            int intRage = 0;
            string strValue;
            foreach (DataRow row1 in dsChartData.Rows)
            {
                strValue = row1["Event"].ToString().Replace(",", "");

                strScript.Append("['" + sArray[intRage] + "'," + strValue + "],");
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
                                width: '100%',          
                                is3D: true,          
                                };   ");

            strScript.Append(@"var chart = new google.visualization.LineChart(document.getElementById('piechart_3d" + intChartCount.ToString() + @"'));          
                                            chart.draw(data, options);        
                                            }    
                                        google.setOnLoadCallback(drawChart);  
                                        ");
            strScript.Append(" </script>");

            TableRow TRow = new TableRow();
            TableCell TCell = new TableCell();
            Literal liter1 = new Literal();
            Literal liter2 = new Literal();
            TCell.ID = "cell" + intChartCount.ToString();
            liter1.Text = strScript.ToString();
            liter1.ID = "liter" + intChartCount.ToString();
            TCell.Controls.Add(liter1);
            TRow.Cells.Add(TCell);
            liter2.Text = "<div id=\"piechart_3d" + intChartCount.ToString() + "\" style=\"width: '100%';border: 1px solid #ccc\"></div>";
            liter2.ID = "literP" + intChartCount.ToString();
            TCell.Controls.Add(liter2);
            TRow.ID = "row" + intChartCount.ToString();
            TRow.Cells.Add(TCell);
            LineChart.Rows.Add(TRow);

            intChartCount++;
            for (int intMonth = 1; intMonth < 13; intMonth++)
            {
                intEvent[intMonth] = 0;

            }

            //================================
            //TRow.Cells.Add(TCell);
            intRowCount++;
            //}
            //LineChart.Rows.Add(TRow);

        }

    }
}
