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

public partial class WebForm_AddWeeklyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["EmpNo"] == null) || (Session["EmpName"] == null))
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;

            DataTable dt1 = clsData.getEmployees("1",Session["EmpName"].ToString());

            if ((dt1.Rows[0]["TeamLeader"].ToString() == "Y") && (dt1.Rows[0]["Manager"].ToString() == "N"))
                getWeekReport_Leader(dt);
            else if (dt1.Rows[0]["Manager"].ToString() == "Y")
                getWeekReport_Manager(dt);
            else
                getWeekReport(dt);

        }
    }

    private void getWeekReport_Manager(DateTime dt)
    {
        DataRow dr;
        //DateTime dt = DateTime.Now;


        string strYear = dt.Year.ToString();
        string strWeek = GetWeekOfYear(dt).ToString();
        string strStart = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

        string strEnd = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

        lblWeek.Text = strWeek;

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Item");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Item";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("W1");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "W1";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("W2");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "W2";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("W3");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "W3";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("W4");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "W4";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("W5");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "W5";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("ID");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "ID";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Project_ID");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Project_ID";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Detail");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Detail";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        int intW = 0;
        int intX;

        DataTable dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());
        if (dt1.Rows.Count > 0)
        {
            //是否有寫過
            DataTable dt2 = clsData.UploadWeeklyReport1(lblWeek.Text, Session["EmpName"].ToString(), strYear);

            if (dt2.Rows.Count == 0)
            {
                dr = dt_new.NewRow();
                dr["Name"] = "Day off";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();
                dr["Name"] = "Meeting";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();
                dr["Name"] = "Other";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                DataTable dt3 = clsData.UploadWeeklyReport_Manager(strStart);

                for (int intJ = 0; intJ < dt3.Rows.Count; intJ++)
                {
                    dr = dt_new.NewRow();
                    dr["Name"] = dt3.Rows[intJ]["Name"].ToString();
                    dr["Item"] = "";
                    dr["Detail"] = "";
                    dr["W1"] = "";
                    dr["W2"] = "";
                    dr["W3"] = "";
                    dr["W4"] = "";
                    dr["W5"] = "";
                    dr["ID"] = dt3.Rows[intJ]["ID"].ToString();
                    dr["Project_ID"] = dt3.Rows[intJ]["ID"].ToString();

                    dt_new.Rows.Add(dr);
                }

                dt1 = clsData.UploadWeeklyReport_Leader1(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {
                    dr = dt_new.NewRow();
                    dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                    dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                    dr["Detail"] = "";
                    dr["W1"] = "";
                    dr["W2"] = "";
                    dr["W3"] = "";
                    dr["W4"] = "";
                    dr["W5"] = "";
                    dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                    dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                    dt_new.Rows.Add(dr);
                }

            }
            else
            {
                dt1 = clsData.UploadWeeklyReport_Manager(strStart);

                if (dt1.Rows.Count == 0)
                {
                    string strKind;
                    for (intX = 0; intX < 3; intX++)
                    {
                        if (intX == 0)
                            strKind = "Day off";
                        else if (intX == 1)
                            strKind = "Meeting";
                        else
                            strKind = "Other";
                        dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), strKind, "", strYear);
                        dr = dt_new.NewRow();
                        if (dt2.Rows.Count == 0)
                        {
                            dr["Name"] = strKind;
                            dr["Item"] = "";
                            dr["Detail"] = "";
                        }
                        else
                        {
                            dr["Name"] = dt2.Rows[0]["Project"].ToString();
                            dr["Item"] = dt2.Rows[0]["Item"].ToString();
                            dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        }
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (int intY = 0; intY < dt2.Rows.Count; intY++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                        }
                        dr["ID"] = "";
                        dr["Project_ID"] = "";

                        dt_new.Rows.Add(dr);
                    }
                }

                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {
                    if (intJ == 0)
                    {
                        string strKind;
                        for (intX = 0; intX < 3; intX++)
                        {
                            if (intX == 0)
                                strKind = "Day off";
                            else if (intX == 1)
                                strKind = "Meeting";
                            else
                                strKind = "Other";
                            dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), strKind, "", strYear);
                            dr = dt_new.NewRow();
                            if (dt2.Rows.Count == 0)
                            {
                                dr["Name"] = strKind;
                                dr["Item"] = "";
                                dr["Detail"] = "";
                            }
                            else
                            {
                                dr["Name"] = dt2.Rows[0]["Project"].ToString();
                                dr["Item"] = dt2.Rows[0]["Item"].ToString();
                                dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                            }
                            dr["W1"] = "";
                            dr["W2"] = "";
                            dr["W3"] = "";
                            dr["W4"] = "";
                            dr["W5"] = "";
                            for (int intY = 0; intY < dt2.Rows.Count; intY++)
                            {
                                if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "3")
                                {
                                    dr["W1"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "4")
                                {
                                    dr["W2"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "5")
                                {
                                    dr["W3"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "6")
                                {
                                    dr["W4"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "7")
                                {
                                    dr["W5"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                            }
                            dr["ID"] = "";
                            dr["Project_ID"] = "";

                            dt_new.Rows.Add(dr);
                        }

                    }


                    dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), dt1.Rows[intJ]["ID"].ToString(), dt1.Rows[intJ]["ID"].ToString(), strYear);
                    //dt2 = clsData.UploadWeeklyReport_Leader1(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                    if (dt2.Rows.Count == 0)
                    {
                        dr = dt_new.NewRow();
                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Item"] = "";
                        dr["Detail"] = "";
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                        dr["Project_ID"] = dt1.Rows[intJ]["ID"].ToString();

                        dt_new.Rows.Add(dr);
                    }
                    else
                    {

                        //DataTable dt3 = clsData.UploadWeeklyReportCase(dt2.Rows[0]["Project"].ToString(), dt2.Rows[0]["Item"].ToString());
                        DataTable dt3 = clsData.UploadProjectQuery(dt2.Rows[0]["Project"].ToString(), "Project");
                        dr = dt_new.NewRow();
                        dr["Name"] = dt3.Rows[0]["Name"].ToString();
                        dr["Item"] = "";
                        dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (intX = 0; intX < dt2.Rows.Count; intX++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            dr["ID"] = dt2.Rows[intX]["Item"].ToString();
                            dr["Project_ID"] = dt2.Rows[intX]["Project"].ToString();


                        }
                        dt_new.Rows.Add(dr);
                    }
                }

                /////////////////////////////////
                dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());
                //抓project裡被assign的任務
                dt1 = clsData.UploadWeeklyReport_Leader1(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                //跑project裡被assign任務的迴圈
                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {

                    dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), dt1.Rows[intJ]["Project_ID"].ToString(), dt1.Rows[intJ]["ID"].ToString(), strYear);

                    if (dt2.Rows.Count == 0)
                    {
                        dr = dt_new.NewRow();
                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                        dr["Detail"] = "";
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                        dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                        dt_new.Rows.Add(dr);
                    }
                    else
                    {

                        DataTable dt3 = clsData.UploadWeeklyReportCase(dt2.Rows[0]["Project"].ToString(), dt2.Rows[0]["Item"].ToString());
                        dr = dt_new.NewRow();
                        dr["Name"] = dt3.Rows[0]["Name"].ToString();
                        dr["Item"] = dt3.Rows[0]["Item"].ToString();
                        dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (intX = 0; intX < dt2.Rows.Count; intX++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            dr["ID"] = dt2.Rows[intX]["Item"].ToString();
                            dr["Project_ID"] = dt2.Rows[intX]["Project"].ToString();


                        }
                        dt_new.Rows.Add(dr);
                    }
                }
            }
        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        int intI = 3;
        while (startDate <= endDate)
        {
            string strDateW;

            strDateW = startDate.ToString("MM/dd") + getChtWeek(startDate);

            gvwMain.HeaderRow.Cells[intI].Text = strDateW;

            startDate = startDate.AddDays(1);
            intI++;


        }

        dt1 = clsData.UploadWeekPlan(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        for (intI = 0; intI < dt1.Rows.Count; intI++)
        {
            if (intI == 0)
                txt1.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 1)
                txt2.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 2)
                txt3.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 3)
                txt4.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else
                txt5.Text = dt1.Rows[intI]["Week_Plan"].ToString();
        }
    }

    private void getWeekReport_Leader(DateTime dt)
    {
        DataRow dr;
        //DateTime dt = DateTime.Now;


        string strYear = dt.Year.ToString();
        string strWeek = GetWeekOfYear(dt).ToString();
        string strStart = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

        string strEnd = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

        lblWeek.Text = strWeek;

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Item");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Item";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("W1");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "W1";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("W2");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "W2";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("W3");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "W3";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("W4");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "W4";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("W5");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "W5";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("ID");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "ID";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Project_ID");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Project_ID";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Detail");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Detail";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        int intW = 0;
        int intX;

        DataTable dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());
        if (dt1.Rows.Count > 0)
        {
            //是否有寫過
            DataTable dt2 = clsData.UploadWeeklyReport1(lblWeek.Text, Session["EmpName"].ToString(), strYear);

            if (dt2.Rows.Count == 0)
            {
                dr = dt_new.NewRow();
                dr["Name"] = "Day off";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();
                dr["Name"] = "Meeting";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();
                dr["Name"] = "Other";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                DataTable dt3 = clsData.UploadWeeklyReport_Leader(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                for (int intJ = 0; intJ < dt3.Rows.Count; intJ++)
                {
                    dr = dt_new.NewRow();
                    dr["Name"] = dt3.Rows[intJ]["Name"].ToString();
                    dr["Item"] = "";
                    dr["Detail"] = "";
                    dr["W1"] = "";
                    dr["W2"] = "";
                    dr["W3"] = "";
                    dr["W4"] = "";
                    dr["W5"] = "";
                    dr["ID"] = dt3.Rows[intJ]["ID"].ToString();
                    dr["Project_ID"] = dt3.Rows[intJ]["ID"].ToString();

                    dt_new.Rows.Add(dr);
                }

                dt1 = clsData.UploadWeeklyReport_Leader1(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {
                    dr = dt_new.NewRow();
                    dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                    dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                    dr["Detail"] = "";
                    dr["W1"] = "";
                    dr["W2"] = "";
                    dr["W3"] = "";
                    dr["W4"] = "";
                    dr["W5"] = "";
                    dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                    dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                    dt_new.Rows.Add(dr);
                }

            }
            else
            {
                dt1 = clsData.UploadWeeklyReport_Leader(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                if (dt1.Rows.Count == 0)
                {
                    string strKind;
                    for (intX = 0; intX < 3; intX++)
                    {
                        if (intX == 0)
                            strKind = "Day off";
                        else if (intX == 1)
                            strKind = "Meeting";
                        else
                            strKind = "Other";
                        dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), strKind, "", strYear);
                        dr = dt_new.NewRow();
                        if (dt2.Rows.Count == 0)
                        {
                            dr["Name"] = strKind;
                            dr["Item"] = "";
                            dr["Detail"] = "";
                        }
                        else
                        {
                            dr["Name"] = dt2.Rows[0]["Project"].ToString();
                            dr["Item"] = dt2.Rows[0]["Item"].ToString();
                            dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        }
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (int intY = 0; intY < dt2.Rows.Count; intY++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                        }
                        dr["ID"] = "";
                        dr["Project_ID"] = "";

                        dt_new.Rows.Add(dr);
                    }
                }

                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {
                    if (intJ == 0)
                    {
                        string strKind;
                        for (intX = 0; intX < 3; intX++)
                        {
                            if (intX == 0)
                                strKind = "Day off";
                            else if (intX == 1)
                                strKind = "Meeting";
                            else
                                strKind = "Other";
                            dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), strKind, "", strYear);
                            dr = dt_new.NewRow();
                            if (dt2.Rows.Count == 0)
                            {
                                dr["Name"] = strKind;
                                dr["Item"] = "";
                                dr["Detail"] = "";
                            }
                            else
                            {
                                dr["Name"] = dt2.Rows[0]["Project"].ToString();
                                dr["Item"] = dt2.Rows[0]["Item"].ToString();
                                dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                            }
                            dr["W1"] = "";
                            dr["W2"] = "";
                            dr["W3"] = "";
                            dr["W4"] = "";
                            dr["W5"] = "";
                            for (int intY = 0; intY < dt2.Rows.Count; intY++)
                            {
                                if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "3")
                                {
                                    dr["W1"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "4")
                                {
                                    dr["W2"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "5")
                                {
                                    dr["W3"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "6")
                                {
                                    dr["W4"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "7")
                                {
                                    dr["W5"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                            }
                            dr["ID"] = "";
                            dr["Project_ID"] = "";

                            dt_new.Rows.Add(dr);
                        }

                    }


                    dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), dt1.Rows[intJ]["ID"].ToString(), dt1.Rows[intJ]["ID"].ToString(), strYear);
                    //dt2 = clsData.UploadWeeklyReport_Leader1(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                    if (dt2.Rows.Count == 0)
                    {
                        dr = dt_new.NewRow();
                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Item"] = "";
                        dr["Detail"] = "";
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                        dr["Project_ID"] = dt1.Rows[intJ]["ID"].ToString();

                        dt_new.Rows.Add(dr);
                    }
                    else
                    {

                        //DataTable dt3 = clsData.UploadWeeklyReportCase(dt2.Rows[0]["Project"].ToString(), dt2.Rows[0]["Item"].ToString());
                        DataTable dt3 = clsData.UploadProjectQuery(dt2.Rows[0]["Project"].ToString(), "Project");
                        dr = dt_new.NewRow();
                        dr["Name"] = dt3.Rows[0]["Name"].ToString();
                        dr["Item"] = "";
                        dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (intX = 0; intX < dt2.Rows.Count; intX++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            dr["ID"] = dt2.Rows[intX]["Item"].ToString();
                            dr["Project_ID"] = dt2.Rows[intX]["Project"].ToString();


                        }
                        dt_new.Rows.Add(dr);
                    }
                }

                /////////////////////////////////
                dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());
                //抓project裡被assign的任務
                dt1 = clsData.UploadWeeklyReport_Leader1(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                //跑project裡被assign任務的迴圈
                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {

                    dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), dt1.Rows[intJ]["Project_ID"].ToString(), dt1.Rows[intJ]["ID"].ToString(), strYear);

                    if (dt2.Rows.Count == 0)
                    {
                        dr = dt_new.NewRow();
                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                        dr["Detail"] = "";
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                        dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                        dt_new.Rows.Add(dr);
                    }
                    else
                    {

                        DataTable dt3 = clsData.UploadWeeklyReportCase(dt2.Rows[0]["Project"].ToString(), dt2.Rows[0]["Item"].ToString());
                        dr = dt_new.NewRow();
                        dr["Name"] = dt3.Rows[0]["Name"].ToString();
                        dr["Item"] = dt3.Rows[0]["Item"].ToString();
                        dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (intX = 0; intX < dt2.Rows.Count; intX++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            dr["ID"] = dt2.Rows[intX]["Item"].ToString();
                            dr["Project_ID"] = dt2.Rows[intX]["Project"].ToString();


                        }
                        dt_new.Rows.Add(dr);
                    }
                }
            }
        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        int intI = 3;
        while (startDate <= endDate)
        {
            string strDateW;

            strDateW = startDate.ToString("MM/dd") + getChtWeek(startDate);

            gvwMain.HeaderRow.Cells[intI].Text = strDateW;

            startDate = startDate.AddDays(1);
            intI++;


        }

        dt1 = clsData.UploadWeekPlan(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        for (intI = 0; intI < dt1.Rows.Count; intI++)
        {
            if (intI == 0)
                txt1.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 1)
                txt2.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 2)
                txt3.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 3)
                txt4.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else
                txt5.Text = dt1.Rows[intI]["Week_Plan"].ToString();
        }
    }

    private int GetWeekOfYear(DateTime dt)
    {
        GregorianCalendar gc = new GregorianCalendar();
        return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }

    protected void loadTestCase_Kind(DropDownList DDL, string strDepartment, string strKind)
    {
        clsDropDownList.ddlApplication_TestCase_Kind(DDL, strDepartment, strKind);
    }

    protected void loadTestCase_Function(DropDownList DDL, string strID)
    {
        clsDropDownList.ddlApplication_TestCase_Function(DDL, strID);
    }

    protected void loadTestCase_Item(DropDownList DDL, string strID, string strFunctionID)
    {
        clsDropDownList.ddlApplication_TestCase_Item(DDL, strID, strFunctionID);
    }

    public static DateTime GetTheFirstDayOfWeek(DateTime dt1)
    {
        return dt1.AddDays(((int)dt1.DayOfWeek * -1) + 1).Date;
    }

    public static DateTime GetTheLastDayOfWeek(DateTime dt1)
    {
        return dt1.AddDays(7 + (int)dt1.DayOfWeek * -1 - 1 - 1).Date;

    }

    private void Query()
    {
        DataRow dr;
        DateTime dt = DateTime.Now;

        lblWeek.Text = GetWeekOfYear(dt).ToString();
        string strStart = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

        string strEnd = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Item");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Item";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("W1");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "W1";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("W2");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "W2";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("W3");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "W3";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("W4");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "W4";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("W5");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "W5";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("ID");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "ID";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Project_ID");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Project_ID";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        dr = dt_new.NewRow();
        dr["Name"] = "Day off";
        dr["Item"] = "";
        dr["W1"] = "";
        dr["W2"] = "";
        dr["W3"] = "";
        dr["W4"] = "";
        dr["W5"] = "";
        dr["ID"] = "";
        dr["Project_ID"] = "";

        dt_new.Rows.Add(dr);

        dr = dt_new.NewRow();
        dr["Name"] = "Meeting";
        dr["Item"] = "";
        dr["W1"] = "";
        dr["W2"] = "";
        dr["W3"] = "";
        dr["W4"] = "";
        dr["W5"] = "";
        dr["ID"] = "";
        dr["Project_ID"] = "";

        dt_new.Rows.Add(dr);

        //DataTable dt1 = clsData.UploadNumber(Session["AppNo"].ToString());
        DataTable dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());
        if (dt1.Rows.Count > 0)
        {
            dt1 = clsData.UploadWeeklyReport(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

            for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
            {
                dr = dt_new.NewRow();
                dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                dt_new.Rows.Add(dr);
            }


            gvwMain.DataSource = dt_new;
            gvwMain.DataBind();

            //DateTime dt2 = DateTime.Now;
            //string strStart = GetTheFirstDayOfWeek(dt2).ToString("yyyy/MM/dd");

            //string strEnd = GetTheLastDayOfWeek(dt2).ToString("yyyy/MM/dd");

            DateTime startDate = Convert.ToDateTime(strStart);
            DateTime endDate = Convert.ToDateTime(strEnd);
            int intI = 3;
            while (startDate <= endDate)
            {
                string strDateW;

                strDateW = startDate.ToString("MM/dd") + getChtWeek(startDate);

                gvwMain.HeaderRow.Cells[intI].Text = strDateW;

                startDate = startDate.AddDays(1);
                intI++;


            }
        }


    }

    protected string getChtWeek(DateTime inputDT)
    {
        switch (inputDT.DayOfWeek.ToString())
        {
            case "Monday": return "(一)";
            case "Tuesday": return "(二)";
            case "Wednesday": return "(三)";
            case "Thursday": return "(四)";
            case "Friday": return "(五)";
            case "Saturday": return "(六)";
            case "Sunday": return "(日)";
            default: return "系統無法判斷";
        }
    }

    protected string getLocation(DateTime inputDT)
    {
        switch (inputDT.DayOfWeek.ToString())
        {
            case "Monday": return "3";
            case "Tuesday": return "4";
            case "Wednesday": return "5";
            case "Thursday": return "6";
            case "Friday": return "7";
            case "Saturday": return "";
            case "Sunday": return "";
            default: return "系統無法判斷";
        }
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)gvwMain.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)gvwMain.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)gvwMain.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                    TextBox box4 = (TextBox)gvwMain.Rows[rowIndex].Cells[4].FindControl("TextBox4");
                    //TextBox box5 = (TextBox)gvwMain.Rows[rowIndex].Cells[5].FindControl("TextBox5");
                    //TextBox box6 = (TextBox)gvwMain.Rows[rowIndex].Cells[6].FindControl("TextBox6");
                    //TextBox box7 = (TextBox)gvwMain.Rows[rowIndex].Cells[7].FindControl("TextBox7");
                    //TextBox box8 = (TextBox)gvwMain.Rows[rowIndex].Cells[8].FindControl("TextBox8");

                    drCurrentRow = dtCurrentTable.NewRow();
                    //drCurrentRow["RowNumber"] = i + 1;
                    drCurrentRow["Name"] = box1.Text;
                    drCurrentRow["Item"] = box2.Text;
                    drCurrentRow["ID"] = box3.Text;
                    drCurrentRow["Project_ID"] = box4.Text;
                    //drCurrentRow["Column5"] = box5.Text;
                    //drCurrentRow["Column6"] = box6.Text;
                    //drCurrentRow["Column7"] = box7.Text;
                    //drCurrentRow["Column8"] = box8.Text;


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                gvwMain.DataSource = dtCurrentTable;
                gvwMain.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvwMain.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)gvwMain.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    TextBox box3 = (TextBox)gvwMain.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();

                    rowIndex++;

                }
            }
            // ViewState["CurrentTable"] = dt;

        }
    }

    double dT1 = 0;
    double dT2 = 0;
    double dT3 = 0;
    double dT4 = 0;
    double dT5 = 0;
    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState != DataControlRowState.Edit && ((int)e.Row.RowState) != 5)
            {
                double dW1 = 0.0;
                double dW2 = 0.0;
                double dW3 = 0.0;
                double dW4 = 0.0;
                double dW5 = 0.0;

                string strVale = ((TextBox)(e.Row.FindControl("txtW1"))).Text.Trim();
                string strVale1 = ((TextBox)(e.Row.FindControl("txtW2"))).Text.Trim();
                string strVale2 = ((TextBox)(e.Row.FindControl("txtW3"))).Text.Trim();
                string strVale3 = ((TextBox)(e.Row.FindControl("txtW4"))).Text.Trim();
                string strVale4 = ((TextBox)(e.Row.FindControl("txtW5"))).Text.Trim();

                double.TryParse(strVale, out dW1);
                dT1 = dT1 + dW1;
                double.TryParse(strVale1, out dW2);
                dT2 = dT2 + dW2;
                double.TryParse(strVale2, out dW3);
                dT3 = dT3 + dW3;
                double.TryParse(strVale3, out dW4);
                dT4 = dT4 + dW4;
                double.TryParse(strVale4, out dW5);
                dT5 = dT5 + dW5;
            }
        }

        else if (e.Row.RowType == DataControlRowType.Footer)
        {


            //e.Row.Cells[3].Text = String.Format("{0:N1}", dT1);
            //e.Row.Cells[4].Text = String.Format("{0:N1}", dT2);
            //e.Row.Cells[5].Text = String.Format("{0:N1}", dT3);
            //e.Row.Cells[6].Text = String.Format("{0:N1}", dT4);
            //e.Row.Cells[7].Text = String.Format("{0:N1}", dT5);
            ((Label)(e.Row.FindControl("lblW1"))).Text = String.Format("{0:N1}", dT1);
            ((Label)(e.Row.FindControl("lblW2"))).Text = String.Format("{0:N1}", dT2);
            ((Label)(e.Row.FindControl("lblW3"))).Text = String.Format("{0:N1}", dT3);
            ((Label)(e.Row.FindControl("lblW4"))).Text = String.Format("{0:N1}", dT4);
            ((Label)(e.Row.FindControl("lblW5"))).Text = String.Format("{0:N1}", dT5);

        }

    }

    protected void txtW1_TextChanged(object sender, EventArgs e)
    {
        double rst1 = 0.0;

        double W1 = 0.0;


        for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
        {
            if (((TextBox)this.gvwMain.Rows[intI].Cells[3].FindControl("txtW1")).Text == "")
                W1 = 0;
            else
                W1 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[3].FindControl("txtW1")).Text);
            rst1 = rst1 + W1;


        }

        ((Label)this.gvwMain.FooterRow.FindControl("lblW1")).Text = String.Format("{0:N1}", rst1);

        Page.RegisterStartupScript("", "<script>NewFocus($('#txt'))</script>");

    }

    protected void txtW2_TextChanged(object sender, EventArgs e)
    {
        double rst1 = 0.0;

        double W1 = 0.0;


        for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
        {
            if (((TextBox)this.gvwMain.Rows[intI].Cells[4].FindControl("txtW2")).Text == "")
                W1 = 0;
            else
                W1 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[4].FindControl("txtW2")).Text);
            rst1 = rst1 + W1;


        }

        ((Label)this.gvwMain.FooterRow.FindControl("lblW2")).Text = String.Format("{0:N1}", rst1);

        Page.RegisterStartupScript("", "<script>NewFocus($('#txt'))</script>");

    }

    protected void txtW3_TextChanged(object sender, EventArgs e)
    {
        double rst1 = 0.0;

        double W1 = 0.0;


        for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
        {
            if (((TextBox)this.gvwMain.Rows[intI].Cells[5].FindControl("txtW3")).Text == "")
                W1 = 0;
            else
                W1 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[5].FindControl("txtW3")).Text);
            rst1 = rst1 + W1;


        }

        ((Label)this.gvwMain.FooterRow.FindControl("lblW3")).Text = String.Format("{0:N1}", rst1);

        Page.RegisterStartupScript("", "<script>NewFocus($('#txt'))</script>");

    }

    protected void txtW4_TextChanged(object sender, EventArgs e)
    {
        double rst1 = 0.0;

        double W1 = 0.0;


        for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
        {
            if (((TextBox)this.gvwMain.Rows[intI].Cells[6].FindControl("txtW4")).Text == "")
                W1 = 0;
            else
                W1 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[6].FindControl("txtW4")).Text);
            rst1 = rst1 + W1;


        }

        ((Label)this.gvwMain.FooterRow.FindControl("lblW4")).Text = String.Format("{0:N1}", rst1);

        Page.RegisterStartupScript("", "<script>NewFocus($('#txt'))</script>");

    }

    protected void txtW5_TextChanged(object sender, EventArgs e)
    {
        double rst1 = 0.0;

        double W1 = 0.0;


        for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
        {
            if (((TextBox)this.gvwMain.Rows[intI].Cells[7].FindControl("txtW5")).Text == "")
                W1 = 0;
            else
                W1 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[7].FindControl("txtW5")).Text);
            rst1 = rst1 + W1;


        }

        ((Label)this.gvwMain.FooterRow.FindControl("lblW5")).Text = String.Format("{0:N1}", rst1);

        Page.RegisterStartupScript("", "<script>NewFocus($('#txt'))</script>");

    }

    private void getWeekReport(DateTime dt)
    {
        DataRow dr;
        //DateTime dt = DateTime.Now;


        string strYear = dt.Year.ToString();
        string strWeek = GetWeekOfYear(dt).ToString();
        string strStart = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

        string strEnd = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

        lblWeek.Text = strWeek;

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Name");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Name";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Item");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Item";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("W1");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "W1";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("W2");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "W2";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("W3");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "W3";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("W4");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "W4";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("W5");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "W5";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("ID");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "ID";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Project_ID");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Project_ID";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Detail");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Detail";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        int intW = 0;
        int intX;

        DataTable dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());
        if (dt1.Rows.Count > 0)
        {
            //是否有寫過
            DataTable dt2 = clsData.UploadWeeklyReport1(lblWeek.Text, Session["EmpName"].ToString(), strYear);

            if (dt2.Rows.Count == 0)
            {
                dr = dt_new.NewRow();
                dr["Name"] = "Day off";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();
                dr["Name"] = "Meeting";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dr = dt_new.NewRow();
                dr["Name"] = "Other";
                dr["Item"] = "";
                dr["Detail"] = "";
                dr["W1"] = "";
                dr["W2"] = "";
                dr["W3"] = "";
                dr["W4"] = "";
                dr["W5"] = "";
                dr["ID"] = "";
                dr["Project_ID"] = "";

                dt_new.Rows.Add(dr);

                dt1 = clsData.UploadWeeklyReport(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {
                    dr = dt_new.NewRow();
                    dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                    dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                    dr["Detail"] = "";
                    dr["W1"] = "";
                    dr["W2"] = "";
                    dr["W3"] = "";
                    dr["W4"] = "";
                    dr["W5"] = "";
                    dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                    dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                    dt_new.Rows.Add(dr);
                }

            }
            else
            {
                //抓project裡被assign的任務
                dt1 = clsData.UploadWeeklyReport(dt1.Rows[0]["Name_En"].ToString().Trim(), strStart);

                if (dt1.Rows.Count == 0)
                {
                    string strKind;
                    for (intX = 0; intX < 3; intX++)
                    {
                        if (intX == 0)
                            strKind = "Day off";
                        else if (intX == 1)
                            strKind = "Meeting";
                        else
                            strKind = "Other";

                        //weeklyreport的資料
                        dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), strKind, "", strYear);
                        dr = dt_new.NewRow();
                        if (dt2.Rows.Count == 0)
                        {
                            dr["Name"] = strKind;
                            dr["Item"] = "";
                            dr["Detail"] = "";
                        }
                        else
                        {
                            dr["Name"] = dt2.Rows[0]["Project"].ToString();
                            dr["Item"] = dt2.Rows[0]["Item"].ToString();
                            dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        }
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (int intY = 0; intY < dt2.Rows.Count; intY++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intY]["Hours"].ToString();
                            }
                        }
                        dr["ID"] = "";
                        dr["Project_ID"] = "";

                        dt_new.Rows.Add(dr);
                    }
                }

                //跑project裡被assign任務的迴圈
                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {
                    if (intJ == 0)
                    {
                        string strKind;
                        for (intX = 0; intX < 3; intX++)
                        {
                            if (intX == 0)
                                strKind = "Day off";
                            else if (intX == 1)
                                strKind = "Meeting";
                            else
                                strKind = "Other";
                            dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), strKind, "", strYear);
                            dr = dt_new.NewRow();
                            if (dt2.Rows.Count == 0)
                            {
                                dr["Name"] = strKind;
                                dr["Item"] = "";
                                dr["Detail"] = "";
                            }
                            else
                            {
                                dr["Name"] = dt2.Rows[0]["Project"].ToString();
                                dr["Item"] = dt2.Rows[0]["Item"].ToString();
                                dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                            }
                            dr["W1"] = "";
                            dr["W2"] = "";
                            dr["W3"] = "";
                            dr["W4"] = "";
                            dr["W5"] = "";
                            for (int intY = 0; intY < dt2.Rows.Count; intY++)
                            {
                                if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "3")
                                {
                                    dr["W1"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "4")
                                {
                                    dr["W2"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "5")
                                {
                                    dr["W3"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "6")
                                {
                                    dr["W4"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                                else if (getLocation(Convert.ToDateTime(dt2.Rows[intY]["Report_Date"].ToString())) == "7")
                                {
                                    dr["W5"] = dt2.Rows[intY]["Hours"].ToString();
                                }
                            }
                            dr["ID"] = "";
                            dr["Project_ID"] = "";

                            dt_new.Rows.Add(dr);
                        }

                    }


                    dt2 = clsData.UploadWeeklyReport2(lblWeek.Text, Session["EmpName"].ToString(), dt1.Rows[intJ]["Project_ID"].ToString(), dt1.Rows[intJ]["ID"].ToString(), strYear);

                    if (dt2.Rows.Count == 0)
                    {
                        dr = dt_new.NewRow();
                        dr["Name"] = dt1.Rows[intJ]["Name"].ToString();
                        dr["Item"] = dt1.Rows[intJ]["Item"].ToString();
                        dr["Detail"] = "";
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        dr["ID"] = dt1.Rows[intJ]["ID"].ToString();
                        dr["Project_ID"] = dt1.Rows[intJ]["Project_ID"].ToString();

                        dt_new.Rows.Add(dr);
                    }
                    else
                    {

                        DataTable dt3 = clsData.UploadWeeklyReportCase(dt2.Rows[0]["Project"].ToString(), dt2.Rows[0]["Item"].ToString());
                        dr = dt_new.NewRow();
                        dr["Name"] = dt3.Rows[0]["Name"].ToString();
                        dr["Item"] = dt3.Rows[0]["Item"].ToString();
                        dr["Detail"] = dt2.Rows[0]["Detail"].ToString();
                        dr["W1"] = "";
                        dr["W2"] = "";
                        dr["W3"] = "";
                        dr["W4"] = "";
                        dr["W5"] = "";
                        for (intX = 0; intX < dt2.Rows.Count; intX++)
                        {
                            if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "3")
                            {
                                dr["W1"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "4")
                            {
                                dr["W2"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "5")
                            {
                                dr["W3"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "6")
                            {
                                dr["W4"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            else if (getLocation(Convert.ToDateTime(dt2.Rows[intX]["Report_Date"].ToString())) == "7")
                            {
                                dr["W5"] = dt2.Rows[intX]["Hours"].ToString();
                            }
                            dr["ID"] = dt2.Rows[intX]["Item"].ToString();
                            dr["Project_ID"] = dt2.Rows[intX]["Project"].ToString();


                        }
                        dt_new.Rows.Add(dr);
                    }
                }
            }
        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();

        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        int intI = 3;
        while (startDate <= endDate)
        {
            string strDateW;

            strDateW = startDate.ToString("MM/dd") + getChtWeek(startDate);

            gvwMain.HeaderRow.Cells[intI].Text = strDateW;

            startDate = startDate.AddDays(1);
            intI++;


        }

        dt1 = clsData.UploadWeekPlan(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        for (intI = 0; intI < dt1.Rows.Count; intI++)
        {
            if (intI == 0)
                txt1.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 1)
                txt2.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 2)
                txt3.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else if (intI == 3)
                txt4.Text = dt1.Rows[intI]["Week_Plan"].ToString();
            else
                txt5.Text = dt1.Rows[intI]["Week_Plan"].ToString();
        }



    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intCell = 3;
        string strTxtName = "txtW";
        string strTxtName1;
        string strHeader;
        string strProject, strItem, strDetail, strHours;
        string strReturn;
        DataTable dt;

        DateTime dtTime = DateTime.Now;
        string strYear = dtTime.Year.ToString();
        strReturn = "0";

        dt = clsData.UploadWeeklyReport1(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        if (dt.Rows.Count > 0)
        {
            clsTransaction.DelWeeklyReport(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        }

        for (int intJ = 0; intJ < 5; intJ++)
        {
            strTxtName1 = strTxtName + (intJ + 1).ToString();
            strHeader = gvwMain.HeaderRow.Cells[intCell].Text;
            strHeader = strYear + "/" + strHeader.Substring(0, 5);
            for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
            {
                if (((TextBox)this.gvwMain.Rows[intI].Cells[intCell].FindControl(strTxtName1)).Text != "")
                {
                    strProject = ((Label)gvwMain.Rows[intI].Cells[8].FindControl("lblProject_ID")).Text.Trim();
                    if (strProject == "")
                    {
                        if (intI == 0)
                            strProject = "Day off";
                        else if (intI == 1)
                            strProject = "Meeting";
                        else
                            strProject = "Other";
                    }
                    strItem = ((Label)gvwMain.Rows[intI].Cells[9].FindControl("lblCase_ID")).Text.Trim();
                    strDetail = ((TextBox)gvwMain.Rows[intI].Cells[2].FindControl("txtDetail")).Text.Trim();
                    strHours = ((TextBox)gvwMain.Rows[intI].Cells[2].FindControl(strTxtName1)).Text.Trim();
                    if (clsTransaction.InsertWeeklyReport(Session["EmpName"].ToString(), lblWeek.Text, strProject, strItem, strDetail, strHeader, strHours) == false)
                    {
                        strReturn = "1";
                        intI = gvwMain.Rows.Count;
                        intJ = 5;
                    }
                }



            }
            intCell++;
        }

        dt = clsData.UploadWeekPlan(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        if (dt.Rows.Count > 0)
        {
            clsTransaction.DelWeekPlan(lblWeek.Text, Session["EmpName"].ToString(), strYear);
        }
        strTxtName = "txt";
        for (int intI = 1; intI < 6; intI++)
        {
            if (intI == 1)
                strTxtName = txt1.Text;
            else if (intI == 2)
                strTxtName = txt2.Text;
            else if (intI == 3)
                strTxtName = txt3.Text;
            else if (intI == 4)
                strTxtName = txt4.Text;
            else
                strTxtName = txt5.Text;

            if (clsTransaction.InsertWeekPlan(Session["EmpName"].ToString(), lblWeek.Text, intI.ToString(), strTxtName, strYear) == false)
            {
                strReturn = "1";
                intI = gvwMain.Rows.Count;
                intI = 6;
            }
        }





        if (strReturn == "1")
            clsMsg.AlertMessage("新增失敗！", this.Page);
        else
            clsMsg.AlertMessage("新增成功！", this.Page);
    }

    //protected void txtW5_TextChanged(object sender, EventArgs e)
    //{
    //    //string strVale;
    //    double rst1 = 0.0;
    //    double rst2 = 0.0;
    //    double rst3 = 0.0;
    //    double rst4 = 0.0;
    //    double rst5 = 0.0;
    //    double W1 = 0.0;
    //    double W2 = 0.0;
    //    double W3 = 0.0;
    //    double W4 = 0.0;
    //    double W5 = 0.0;

    //    for (int intI = 0; intI < gvwMain.Rows.Count; intI++)
    //    {
    //        if (((TextBox)this.gvwMain.Rows[intI].Cells[3].FindControl("txtW1")).Text == "")
    //            W1 = 0;
    //        else
    //            W1 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[3].FindControl("txtW1")).Text);
    //        rst1 = rst1 + W1;

    //        if (((TextBox)this.gvwMain.Rows[intI].Cells[4].FindControl("txtW2")).Text == "")
    //            W2 = 0;
    //        else
    //            W2 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[4].FindControl("txtW2")).Text);
    //        rst2 = rst2 + W2;

    //        if (((TextBox)this.gvwMain.Rows[intI].Cells[5].FindControl("txtW3")).Text == "")
    //            W3 = 0;
    //        else
    //            W3 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[5].FindControl("txtW3")).Text);
    //        rst3 = rst3 + W3;

    //        if (((TextBox)this.gvwMain.Rows[intI].Cells[6].FindControl("txtW4")).Text == "")
    //            W4 = 0;
    //        else
    //            W4 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[6].FindControl("txtW4")).Text);
    //        rst4 = rst4 + W4;

    //        if (((TextBox)this.gvwMain.Rows[intI].Cells[7].FindControl("txtW5")).Text == "")
    //            W5 = 0;
    //        else
    //            W5 = Convert.ToDouble(((TextBox)this.gvwMain.Rows[intI].Cells[7].FindControl("txtW5")).Text);
    //        rst5 = rst5 + W5;
    //    }



    //    ((Label)this.gvwMain.FooterRow.FindControl("lblW1")).Text = String.Format("{0:N1}", rst1);
    //    ((Label)this.gvwMain.FooterRow.FindControl("lblW2")).Text = String.Format("{0:N1}", rst2);
    //    ((Label)this.gvwMain.FooterRow.FindControl("lblW3")).Text = String.Format("{0:N1}", rst3);
    //    ((Label)this.gvwMain.FooterRow.FindControl("lblW4")).Text = String.Format("{0:N1}", rst4);
    //    ((Label)this.gvwMain.FooterRow.FindControl("lblW5")).Text = String.Format("{0:N1}", rst5);

    //}


    protected void lbtnlblLastWeek_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;

        DataTable dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());

        dt = dt.AddDays(-7);

        if ((dt1.Rows[0]["TeamLeader"].ToString() == "Y") && (dt1.Rows[0]["Manager"].ToString() == "N"))
            getWeekReport_Leader(dt);
        else if (dt1.Rows[0]["Manager"].ToString() == "Y")
            getWeekReport_Manager(dt);
        else
            getWeekReport(dt);
    }
    protected void lbtnThisWeek_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;

        DataTable dt1 = clsData.getEmployees("1", Session["EmpName"].ToString());

        if ((dt1.Rows[0]["TeamLeader"].ToString() == "Y") && (dt1.Rows[0]["Manager"].ToString() == "N"))
            getWeekReport_Leader(dt);
        else if (dt1.Rows[0]["Manager"].ToString() == "Y")
            getWeekReport_Manager(dt);
        else
            getWeekReport(dt);
    }
}
