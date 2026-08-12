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

public partial class WebForm_TimeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {

            String strYear = DateTime.Now.Year.ToString();
            int iMonth = System.DateTime.Now.Month;

            txtYearA.Text = strYear;
            ddlMonthA.Text = String.Format("{0:00}", iMonth);

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

            table1.Visible = false;
        }
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[4].Attributes.Add("style", "word-break :break-all ; word-wrap:break-word");
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            e.Row.Cells[9].Visible = false;
        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int intI = 0; intI < e.Row.Cells.Count - 2; intI++)
            e.Row.Cells[intI].ToolTip = e.Row.Cells[intI].Text;

        if (e.Row.Cells[0].Text.Length > 6) //Just change the value of 10 based on your requirements
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(0, 6) + "...";

        if (e.Row.Cells[1].Text.Length > 10) //Just change the value of 10 based on your requirements
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 10) + "...";

        if (e.Row.Cells[2].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[3].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[4].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[5].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[6].Text.Length > 10) //Just change the value of 5 based on your requirements
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Substring(0, 5) + "...";

        if (e.Row.Cells[7].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Substring(0, 5) + "...";

        if (e.Row.Cells[12].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[13].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[13].Text = e.Row.Cells[13].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[14].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[14].Text = e.Row.Cells[14].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[15].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[15].Text = e.Row.Cells[15].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[16].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[16].Text = e.Row.Cells[16].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[17].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[17].Text = e.Row.Cells[17].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[18].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[18].Text = e.Row.Cells[18].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[19].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[19].Text = e.Row.Cells[19].Text.Substring(0, 15) + "...";

        if (e.Row.Cells[20].Text.Length > 15) //Just change the value of 15 based on your requirements
            e.Row.Cells[20].Text = e.Row.Cells[20].Text.Substring(0, 15) + "...";

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[5].Text == "0")
                e.Row.Cells[5].Text = "";
            if (e.Row.Cells[6].Text == "0")
                e.Row.Cells[6].Text = "";
            if (e.Row.Cells[7].Text == "0")
                e.Row.Cells[7].Text = "";
            if (e.Row.Cells[8].Text == "0")
                e.Row.Cells[8].Text = "";
            //if (e.Row.Cells[12].Text == "0")
            //    e.Row.Cells[12].Text = "";
            //if (e.Row.Cells[13].Text == "0")
            //    e.Row.Cells[13].Text = "";
            //if (e.Row.Cells[14].Text == "0")
            //    e.Row.Cells[14].Text = "";
            if (e.Row.Cells[15].Text == "0")
                e.Row.Cells[15].Text = "";
            if (e.Row.Cells[16].Text == "0")
                e.Row.Cells[16].Text = "";
            if (e.Row.Cells[17].Text == "0")
                e.Row.Cells[17].Text = "";
            if (e.Row.Cells[18].Text == "0")
                e.Row.Cells[18].Text = "";
            if (e.Row.Cells[19].Text == "0")
                e.Row.Cells[19].Text = "";
            if (e.Row.Cells[20].Text == "0")
                e.Row.Cells[20].Text = "";
            //e.Row.Cells[4].Width = 80;
            //gvwMain.Columns[0].ItemStyle.Width = 100;
            //gvwMain.Columns[1].ItemStyle.Width = 100;
            //gvwMain.Columns[2].ItemStyle.Width = 150;
            //gvwMain.Columns[3].ItemStyle.Width = 100;
            //gvwMain.Columns[4].ItemStyle.Width = 250;
            //gvwMain.Columns[5].ItemStyle.Width = 100;
            //gvwMain.Columns[6].ItemStyle.Width = 100;
            //gvwMain.Columns[7].ItemStyle.Width = 100;
            //gvwMain.Columns[8].ItemStyle.Width = 100;
            //gvwMain.Columns[9].ItemStyle.Width = 100;   
            if (DataBinder.Eval(e.Row.DataItem, "Progress").ToString() == e.Row.Cells[9].Text)
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
            else
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Green;
            if (DataBinder.Eval(e.Row.DataItem, "Progress").ToString() == "0")
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Gray;
            if (DataBinder.Eval(e.Row.DataItem, "Progress").ToString() == "100")
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Blue;


        }

        //e.Row.Cells[4].Attributes.Add("style", "word-break :break-all ; word-wrap:break-word");
        //e.Row.Cells[4].Wrap = false;
        //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        export_exce("TimeReport", 1);
    }

    private void export_exce(string filename, int t_mode)
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
        //gvwMain.AllowPaging = false;
        //gvwMain.DataSource = dt;
        //gvwMain.DataBind();
        //gvwMain.Columns[3].Visible = false;
        gvwMain1.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
        //gvwMain.AllowPaging = true;
        //gvwMain.DataSource = dt;
        //gvwMain.DataBind();
        //gvwMain.Columns[3].Visible = true;
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        Search1();
    }

    private void Search1()
    {
        string strStartDate, strEndDate, strLocal;

        //strStartDate = Session["TDateS"].ToString();
        //strEndDate = Session["TDateE"].ToString();

        int intMonthS, intMonthE;
        string strYearS, strYearE;
        string win_str;

        if (Convert.ToInt16(ddlMonthA.Text) == 1)
        {
            intMonthS = 12;
            intMonthE = 1;

            strYearS = (Convert.ToInt16(txtYearA.Text.Trim()) - 1).ToString();
            strYearE = txtYearA.Text.Trim();
        }
        else
        {
            strYearS = txtYearA.Text.Trim();
            strYearE = txtYearA.Text.Trim();

            intMonthS = Convert.ToInt16(ddlMonthA.Text) - 1;
            intMonthE = Convert.ToInt16(ddlMonthA.Text);
        }

        strStartDate = strYearS + "/" + intMonthS.ToString() + "/28";
        strEndDate = strYearE + "/" + intMonthE.ToString() + "/28";

        if (rdoLocal.Checked == true)
            strLocal = "DA40";
        else
            strLocal = "DA40-WJ";

        DateTime startDate = Convert.ToDateTime(strStartDate);
        DateTime endDate = Convert.ToDateTime(strEndDate);
        int workday = 0;
        while (startDate < endDate)
        {
            if ((int)startDate.DayOfWeek != 0 && (int)startDate.DayOfWeek != 6)
            {
                workday += 1;
            }

            startDate = startDate.AddDays(1);

        }

        workday = workday * 8;


        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("PU");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "PU";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("CustomerNumber");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "CustomerNumber";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Customer");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Customer";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Model");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Model";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Employees");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Employees";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Department");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Department";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Team");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Team";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("5");
        column8.DataType = System.Type.GetType("System.Double");
        column8.AllowDBNull = true;
        column8.Caption = "5";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("6");
        column9.DataType = System.Type.GetType("System.Double");
        column9.AllowDBNull = true;
        column9.Caption = "6";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("7");
        column10.DataType = System.Type.GetType("System.Double");
        column10.AllowDBNull = true;
        column10.Caption = "7";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("8");
        column11.DataType = System.Type.GetType("System.Double");
        column11.AllowDBNull = true;
        column11.Caption = "8";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("9");
        column12.DataType = System.Type.GetType("System.Double");
        column12.AllowDBNull = true;
        column12.Caption = "9";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("10");
        column13.DataType = System.Type.GetType("System.Double");
        column13.AllowDBNull = true;
        column13.Caption = "10";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("Detail");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "Detail";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);

        DataColumn column15 = new DataColumn("Total");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "Total";
        column15.DefaultValue = "0";
        dt_new.Columns.Add(column15);

        DataColumn column16 = new DataColumn("Status");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "Status";
        column16.DefaultValue = "0";
        dt_new.Columns.Add(column16);

        DataColumn column17 = new DataColumn("Start_Date");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "Start_Date";
        column17.DefaultValue = "0";
        dt_new.Columns.Add(column17);

        DataColumn column18 = new DataColumn("End_Date");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "End_Date";
        column18.DefaultValue = "0";
        dt_new.Columns.Add(column18);

        DataColumn column19 = new DataColumn("Result");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "Result";
        column19.DefaultValue = "0";
        dt_new.Columns.Add(column19);

        DataColumn column20 = new DataColumn("Kind");
        column20.DataType = System.Type.GetType("System.String");
        column20.AllowDBNull = true;
        column20.Caption = "Kind";
        column20.DefaultValue = "0";
        dt_new.Columns.Add(column20);

        DataColumn column21 = new DataColumn("Progress");
        column21.DataType = System.Type.GetType("System.String");
        column21.AllowDBNull = true;
        column21.Caption = "Progress";
        column21.DefaultValue = "0";
        dt_new.Columns.Add(column21);

        DataColumn column22 = new DataColumn("Progress_LastWeek");
        column22.DataType = System.Type.GetType("System.String");
        column22.AllowDBNull = true;
        column22.Caption = "Progress_LastWeek";
        column22.DefaultValue = "0";
        dt_new.Columns.Add(column22);

        string strDepartmentName, strLevel;
        DataTable dt = clsData.UploadApparatusMasterQuery("A5DN", "0");

        strDepartmentName = dt.Rows[0]["Name"].ToString();

        dt = clsData.UploadEmployeesQuery("Order by", strLocal);

        DataTable dt1;
        DataTable dt2 = null;
        string strPID = "";
        double dHours = 0.0;
        double dHoursP = 0.0;
        string strCase = "";
        string strPU, strCustomerN, strCustomer, strModel, strEmp, strA_Department, strStatus, strStart, strEnd, strResult, strKind;
        string strProgress, strProgress_LastWeek, strDetail;
        strPU = "";
        strCustomer = "";
        strCustomerN = "";
        strModel = "";
        strEmp = "";
        strA_Department = "";
        strStatus = "";
        strStart = "";
        strEnd = "";
        strResult = "";
        strKind = "";
        strProgress = "";
        strProgress_LastWeek = "";
        strDetail = "";
        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            strLevel = getLevel(dt.Rows[intI]["Position"].ToString());
            strEmp = dt.Rows[intI]["Name_CH"].ToString();

            for (int intX = 0; intX < 2; intX++)
            {
                if (intX == 0)
                    dt1 = clsData.UploadTimeReport_N(strStartDate, strEndDate, dt.Rows[intI]["Name_En"].ToString(), "驗証申請");
                else
                    dt1 = clsData.UploadTimeReport_N(strStartDate, strEndDate, dt.Rows[intI]["Name_En"].ToString(), "");


                for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
                {

                    if (strPID != dt1.Rows[intJ]["Project"].ToString())
                    {
                        strPID = dt1.Rows[intJ]["Project"].ToString();
                        //if ((strPU != "") || (strModel != "") || (strCustomer != "") || (strCustomerN != "") || (strCase != ""))
                        //if (intJ == dt1.Rows.Count - 1)
                        //{
                        //    DataRow dr = dt_new.NewRow();
                        //    dr["PU"] = strPU;
                        //    dr["CustomerNumber"] = strCustomerN;
                        //    dr["Customer"] = strCustomer;
                        //    dr["Model"] = strModel;
                        //    dr["Employees"] = strEmp;
                        //    dr["Department"] = strDepartmentName;
                        //    dr["Team"] = dt.Rows[intI]["Team"].ToString();
                        //    strCase = strCase.Trim();
                        //    if (strCase.Length != 0)
                        //        dr["Detail"] = strCase.Remove(strCase.Length - 1, 1);
                        //    else
                        //        dr["Detail"] = strCase;
                        //    //string strHoursP;

                        //    dHoursP = dHours / workday;
                        //    //strHoursP = dHoursP.ToString("#0.0");
                        //    if ((strLevel == "4") || (strLevel == "5"))
                        //    {
                        //        dr["5"] = Math.Round(dHoursP, 5);
                        //        dr["6"] = 0;
                        //        dr["7"] = 0;
                        //        dr["8"] = 0;
                        //        dr["9"] = 0;
                        //        dr["10"] = 0;
                        //    }

                        //    if (strLevel == "6")
                        //    {
                        //        dr["5"] = 0;
                        //        dr["6"] = Math.Round(dHoursP, 5);
                        //        dr["7"] = 0;
                        //        dr["8"] = 0;
                        //        dr["9"] = 0;
                        //        dr["10"] = 0;
                        //    }

                        //    if (strLevel == "7")
                        //    {
                        //        dr["5"] = 0;
                        //        dr["6"] = 0; ;
                        //        dr["7"] = Math.Round(dHoursP, 5);
                        //        dr["8"] = 0;
                        //        dr["9"] = 0;
                        //        dr["10"] = 0;
                        //    }

                        //    if (strLevel == "8")
                        //    {
                        //        dr["5"] = 0;
                        //        dr["6"] = 0;
                        //        dr["7"] = 0;
                        //        dr["8"] = Math.Round(dHoursP, 5);
                        //        dr["9"] = 0;
                        //        dr["10"] = 0;
                        //    }

                        //    if (strLevel == "9")
                        //    {
                        //        dr["5"] = 0;
                        //        dr["6"] = 0;
                        //        dr["7"] = 0;
                        //        dr["8"] = 0;
                        //        dr["9"] = Math.Round(dHoursP, 5);
                        //        dr["10"] = 0;
                        //    }

                        //    dr["Total"] = Math.Round(dHoursP, 5);

                        //    dt_new.Rows.Add(dr);
                        //}

                        //dHours = 0.0;
                        //dHoursP = 0.0;
                        //strCase = "";
                        //strPU = "";
                        //strCustomer = "";
                        //strCustomerN = "";
                        //strModel = "";
                        //dHours = Convert.ToDouble(dt1.Rows[intJ]["Hours"].ToString());
                        if (dt1.Rows[intJ]["Item"].ToString().Length == 14)
                            dt2 = clsData.UploadTimeReport_N1(dt1.Rows[intJ]["Project"].ToString(), "");
                        else
                            dt2 = clsData.UploadTimeReport_N1(dt1.Rows[intJ]["Project"].ToString(), dt1.Rows[intJ]["Item"].ToString());
                        strDetail = "";
                        if (intX == 0)
                        {
                            strModel = dt2.Rows[0]["Name1"].ToString();

                            if ((dt2.Rows[0]["A_Department2"].ToString() == "") || (dt2.Rows[0]["A_Department2"].ToString() == null))
                            {
                                if ((dt2.Rows[0]["A_Department"].ToString() == "") || (dt2.Rows[0]["A_Department"].ToString() == null))
                                    strA_Department = "";
                                else
                                    strA_Department = dt2.Rows[0]["A_Department"].ToString();
                            }
                            else
                                strA_Department = dt2.Rows[0]["A_Department2"].ToString();

                            //if ((dt2.Rows[0]["A_Department"].ToString() == "") || (dt2.Rows[0]["A_Department"].ToString() == null))
                            //    strA_Department = "";
                            //else
                            //    strA_Department = dt2.Rows[0]["A_Department"].ToString();
                        }
                        else
                        {
                            strModel = dt2.Rows[0]["Model_Name"].ToString();


                            if ((dt2.Rows[0]["Sub_PU"].ToString() == "") || (dt2.Rows[0]["Sub_PU"].ToString() == null))
                                strA_Department = "";
                            else
                                strA_Department = dt2.Rows[0]["Sub_PU"].ToString();
                        }

                        if (strA_Department == "")
                            strPU = "";
                        else
                        {
                            string[] sArray = strA_Department.Split('-');
                            int intU = 0;
                            foreach (string l in sArray)
                            {
                                intU++;
                            }
                            if (intU == 2)
                                strPU = sArray[1].Replace("PU", "");
                            else
                                strPU = sArray[0].Replace("PU", "");
                        }


                        if ((dt2.Rows[0]["Customer"].ToString() == "") || (dt2.Rows[0]["Customer"].ToString() == null))
                        {
                            strCustomerN = "";
                            strCustomer = "";
                        }
                        else
                        {
                            int intIndex, intIndex1;
                            intIndex = dt2.Rows[0]["Customer"].ToString().IndexOf("(");
                            intIndex1 = dt2.Rows[0]["Customer"].ToString().IndexOf(")");
                            if (intIndex < 0)
                                strCustomerN = dt2.Rows[0]["Customer"].ToString();
                            else
                                strCustomerN = dt2.Rows[0]["Customer"].ToString().Substring(1, intIndex - 1);

                            strCustomer = dt2.Rows[0]["Customer"].ToString().Substring(intIndex + 1, intIndex1 - (intIndex + 1));
                        }
                        if ((dt2.Rows[0]["Status"].ToString() == "") || (dt2.Rows[0]["Status"].ToString() == null))
                            strStatus = "";
                        else
                            strStatus = dt2.Rows[0]["Status"].ToString();


                        string strP, strP1;
                        if (dt1.Rows[intJ]["Item"].ToString().Length == 14)
                        {
                            strP = "Progress1";
                            strP1 = "Progress_LastWeek1";
                        }
                        else
                        {
                            strP = "Progress";
                            strP1 = "Progress_LastWeek";
                        }
                        if ((dt2.Rows[0][strP].ToString() == "") || (dt2.Rows[0][strP].ToString() == null))
                            strProgress = "";
                        else
                            strProgress = dt2.Rows[0][strP].ToString();
                        if ((dt2.Rows[0][strP1].ToString() == "") || (dt2.Rows[0][strP1].ToString() == null))
                            strProgress_LastWeek = "";
                        else
                            strProgress_LastWeek = dt2.Rows[0][strP1].ToString();


                        if ((dt2.Rows[0]["Kind"].ToString() == "") || (dt2.Rows[0]["Kind"].ToString() == null))
                            strKind = "";
                        else
                            strKind = dt2.Rows[0]["Kind"].ToString();
                        string strStart1, strEnd1;
                        DateTime dt3;
                        dt3 = Convert.ToDateTime(dt2.Rows[0]["Start_Date"].ToString());
                        strStart1 = dt3.ToString("yyyy/MM/dd");
                        if (strStart1 == "1900/01/01")
                            strStart = "";
                        else
                            strStart = strStart1;

                        dt3 = Convert.ToDateTime(dt2.Rows[0]["End_Date"].ToString());
                        strEnd1 = dt3.ToString("yyyy/MM/dd");
                        if (strEnd1 == "1900/01/01")
                            strEnd = "";
                        else
                            strEnd = strEnd1;
                        if ((dt2.Rows[0]["Result"].ToString() == "") || (dt2.Rows[0]["Result"].ToString() == null))
                            strResult = "";
                        else
                            strResult = dt2.Rows[0]["Result"].ToString();
                        if (dt1.Rows[intJ]["Item"].ToString().Length != 14)
                        {
                            dt2 = clsData.UploadTimeReport_N1(dt1.Rows[intJ]["Project"].ToString(), dt1.Rows[intJ]["Item"].ToString());
                            strCase = strCase + dt2.Rows[0]["Name"].ToString() + ",";
                        }

                    }
                    else
                    {
                        if (dt1.Rows[intJ]["Item"].ToString().Length != 14)
                        {
                            dt2 = clsData.UploadTimeReport_N1(dt1.Rows[intJ]["Project"].ToString(), dt1.Rows[intJ]["Item"].ToString());
                            if (dt2.Rows.Count > 0)
                            {
                                if (strCase.IndexOf(dt2.Rows[0]["Name"].ToString()) == -1)
                                    strCase = strCase + dt2.Rows[0]["Name"].ToString() + ",";
                            }
                        }
                        //dHours = dHours + Convert.ToDouble(dt1.Rows[intJ]["Hours"].ToString());
                    }
                    strDetail = "";
                    if ((dt1.Rows[intJ]["Detail"].ToString() != "") && (dt1.Rows[intJ]["Detail"].ToString() != null))
                    {
                        if (strDetail.IndexOf(dt1.Rows[intJ]["Detail"].ToString()) == -1)
                            strDetail = strDetail + dt1.Rows[intJ]["Detail"].ToString() + "/";
                    }
                    dHours = dHours + Convert.ToDouble(dt1.Rows[intJ]["Hours"].ToString());

                    int intT = intJ + 1;
                    if (intT <= dt1.Rows.Count - 1)
                    {
                        if (strPID != dt1.Rows[intJ + 1]["Project"].ToString())
                        {
                            DataRow dr = dt_new.NewRow();
                            dr["PU"] = strPU;
                            dr["CustomerNumber"] = strCustomerN;
                            dr["Customer"] = strCustomer;
                            dr["Model"] = strModel;
                            dr["Employees"] = strEmp;
                            dr["Department"] = strDepartmentName;
                            dr["Team"] = dt.Rows[intI]["Team"].ToString();
                            dr["Status"] = strStatus;
                            dr["Start_Date"] = strStart;
                            dr["End_Date"] = strEnd;
                            dr["Result"] = strResult;
                            dr["Kind"] = strKind;
                            dr["Progress"] = strProgress;
                            dr["Progress_LastWeek"] = strProgress_LastWeek;

                            strCase = strCase.Trim();
                            if (strCase.Length != 0)
                            {
                                dr["Detail"] = strCase.Remove(strCase.Length - 1, 1);
                                strCase = strCase.Remove(strCase.Length - 1, 1);
                            }
                            else
                                dr["Detail"] = strCase;

                            if (strDetail.Length != 0)
                                dr["Detail"] = strCase + "/" + strDetail.Remove(strDetail.Length - 1, 1);
                            if ((strCase.Length == 0) && (strDetail.Length != 0))
                                dr["Detail"] = strCase + strDetail.Remove(strDetail.Length - 1, 1);
                            //string strHoursP;

                            dHoursP = dHours / workday;
                            //strHoursP = dHoursP.ToString("#0.0");
                            if ((strLevel == "4") || (strLevel == "5"))
                            {
                                dr["5"] = Math.Round(dHoursP, 5);
                                dr["6"] = 0;
                                dr["7"] = 0;
                                dr["8"] = 0;
                                dr["9"] = 0;
                                dr["10"] = 0;
                            }

                            if (strLevel == "6")
                            {
                                dr["5"] = 0;
                                dr["6"] = Math.Round(dHoursP, 5);
                                dr["7"] = 0;
                                dr["8"] = 0;
                                dr["9"] = 0;
                                dr["10"] = 0;
                            }

                            if (strLevel == "7")
                            {
                                dr["5"] = 0;
                                dr["6"] = 0; ;
                                dr["7"] = Math.Round(dHoursP, 5);
                                dr["8"] = 0;
                                dr["9"] = 0;
                                dr["10"] = 0;
                            }

                            if (strLevel == "8")
                            {
                                dr["5"] = 0;
                                dr["6"] = 0;
                                dr["7"] = 0;
                                dr["8"] = Math.Round(dHoursP, 5);
                                dr["9"] = 0;
                                dr["10"] = 0;
                            }

                            if (strLevel == "9")
                            {
                                dr["5"] = 0;
                                dr["6"] = 0;
                                dr["7"] = 0;
                                dr["8"] = 0;
                                dr["9"] = Math.Round(dHoursP, 5);
                                dr["10"] = 0;
                            }

                            dr["Total"] = Math.Round(dHoursP, 5);

                            dt_new.Rows.Add(dr);

                            dHours = 0.0;
                            dHoursP = 0.0;
                            strCase = "";
                            strPU = "";
                            strCustomer = "";
                            strCustomerN = "";
                            strModel = "";
                        }
                    }
                    else
                    {
                        DataRow dr = dt_new.NewRow();
                        dr["PU"] = strPU;
                        dr["CustomerNumber"] = strCustomerN;
                        dr["Customer"] = strCustomer;
                        dr["Model"] = strModel;
                        dr["Employees"] = strEmp;
                        dr["Department"] = strDepartmentName;
                        dr["Team"] = dt.Rows[intI]["Team"].ToString();
                        dr["Status"] = strStatus;
                        dr["Start_Date"] = strStart;
                        dr["End_Date"] = strEnd;
                        dr["Result"] = strResult;
                        dr["Kind"] = strKind;
                        dr["Progress"] = strProgress;
                        dr["Progress_LastWeek"] = strProgress_LastWeek;

                        strCase = strCase.Trim();
                        if (strCase.Length != 0)
                        {
                            dr["Detail"] = strCase.Remove(strCase.Length - 1, 1);
                            strCase = strCase.Remove(strCase.Length - 1, 1);
                        }
                        else
                            dr["Detail"] = strCase;

                        if (strDetail.Length != 0)
                            dr["Detail"] = strCase + "/" + strDetail.Remove(strDetail.Length - 1, 1);
                        if ((strCase.Length == 0) && (strDetail.Length != 0))
                            dr["Detail"] = strCase + strDetail.Remove(strDetail.Length - 1, 1);
                        //string strHoursP;

                        dHoursP = dHours / workday;
                        //strHoursP = dHoursP.ToString("#0.0");
                        if ((strLevel == "4") || (strLevel == "5"))
                        {
                            dr["5"] = Math.Round(dHoursP, 5);
                            dr["6"] = 0;
                            dr["7"] = 0;
                            dr["8"] = 0;
                            dr["9"] = 0;
                            dr["10"] = 0;
                        }

                        if (strLevel == "6")
                        {
                            dr["5"] = 0;
                            dr["6"] = Math.Round(dHoursP, 5);
                            dr["7"] = 0;
                            dr["8"] = 0;
                            dr["9"] = 0;
                            dr["10"] = 0;
                        }

                        if (strLevel == "7")
                        {
                            dr["5"] = 0;
                            dr["6"] = 0; ;
                            dr["7"] = Math.Round(dHoursP, 5);
                            dr["8"] = 0;
                            dr["9"] = 0;
                            dr["10"] = 0;
                        }

                        if (strLevel == "8")
                        {
                            dr["5"] = 0;
                            dr["6"] = 0;
                            dr["7"] = 0;
                            dr["8"] = Math.Round(dHoursP, 5);
                            dr["9"] = 0;
                            dr["10"] = 0;
                        }

                        if (strLevel == "9")
                        {
                            dr["5"] = 0;
                            dr["6"] = 0;
                            dr["7"] = 0;
                            dr["8"] = 0;
                            dr["9"] = Math.Round(dHoursP, 5);
                            dr["10"] = 0;
                        }

                        dr["Total"] = Math.Round(dHoursP, 5);

                        dt_new.Rows.Add(dr);

                        dHours = 0.0;
                        dHoursP = 0.0;
                        strCase = "";
                        strPU = "";
                        strCustomer = "";
                        strCustomerN = "";
                        strModel = "";
                    }
                }
            }
        }
        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
        gvwMain1.DataSource = dt_new;
        gvwMain1.DataBind();
    }

    protected string getLevel(string strLevel)
    {
        switch (strLevel)
        {
            case "助工": return "4";
            case "副工": return "5";
            case "工程師": return "6";
            case "高工": return "7";
            case "主工": return "8";
            case "課長": return "7";
            case "副理": return "8";
            case "技術副理": return "8";
            case "經理": return "9";
            default: return "系統無法判斷";
        }
    }

    private void Search()
    {
        int intMonthS, intMonthE;
        string strYearS, strYearE;
        string win_str;

        if (Convert.ToInt16(ddlMonthA.Text) == 1)
        {
            intMonthS = 12;
            intMonthE = 1;

            strYearS = (Convert.ToInt16(txtYearA.Text.Trim()) - 1).ToString();
            strYearE = txtYearA.Text.Trim();
        }
        else
        {
            strYearS = txtYearA.Text.Trim();
            strYearE = txtYearA.Text.Trim();

            intMonthS = Convert.ToInt16(ddlMonthA.Text) - 1;
            intMonthE = Convert.ToInt16(ddlMonthA.Text);
        }

        Session["TDateS"] = strYearS + "/" + intMonthS.ToString() + "/28";
        Session["TDateE"] = strYearE + "/" + intMonthE.ToString() + "/28";

        if (rdoLocal.Checked == true)
            Session["RLocal"] = "DA40";
        else
            Session["RLocal"] = "DA40-WJ";

        win_str = "<script language='javascript'>window.open('../Report/rpt_TimeReport.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        Response.Write(win_str);

    }
}
