using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Report_rpt_TimeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate, strEndDate;

        strStartDate = Session["TDateS"].ToString();
        strEndDate = Session["TDateE"].ToString();

        DateTime startDate = Convert.ToDateTime(strStartDate);
        DateTime endDate = Convert.ToDateTime(strEndDate);
        int workday = 0;
        while (startDate < endDate)
        {
            if ((int)startDate.DayOfWeek != 0 || (int)startDate.DayOfWeek != 6)
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

        string strDepartmentName, strLevel;
        DataTable dt = clsData.UploadApparatusMasterQuery("A5DN", "0");

        strDepartmentName = dt.Rows[0]["Name"].ToString();

        dt = clsData.UploadEmployeesQuery("Order by", Session["RLocal"].ToString());
        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            strLevel = getLevel(dt.Rows[intI]["Position"].ToString());
            DataTable dt1 = clsData.UploadTimeReport(strStartDate,strEndDate,dt.Rows[intI]["Name_En"].ToString());

            for (int intJ = 0; intJ < dt1.Rows.Count; intJ++)
            {
                DataRow dr = dt_new.NewRow();

                if ((dt1.Rows[intJ]["A_Department"].ToString() == "") || (dt1.Rows[intJ]["A_Department"].ToString() == null))
                    dr["PU"] = "";
                else
                {
                    string[] sArray = dt1.Rows[intJ]["A_Department"].ToString().Split('-');
                    int intU = 0;
                    foreach (string l in sArray)
                    {
                        intU++;
                    }
                    if (intU == 2)
                        dr["PU"] = sArray[1].Replace("PU", "");
                    else
                        dr["PU"] = sArray[0].Replace("PU", "");
                }

                if ((dt1.Rows[intJ]["Customer"].ToString() == "") || (dt1.Rows[intJ]["Customer"].ToString() == null))
                {
                    dr["CustomerNumber"] = "";
                    dr["Customer"] = "";
                }
                else
                {
                    int intIndex, intIndex1;
                    intIndex = dt1.Rows[intJ]["Customer"].ToString().IndexOf("(");
                    intIndex1 = dt1.Rows[intJ]["Customer"].ToString().IndexOf(")");
                    if (intIndex < 0)
                        dr["CustomerNumber"] = dt1.Rows[intJ]["Customer"].ToString();
                    else
                        dr["CustomerNumber"] = dt1.Rows[intJ]["Customer"].ToString().Substring(1, intIndex - 1);

                    dr["Customer"] = dt1.Rows[intJ]["Customer"].ToString().Substring(intIndex + 1, intIndex1 - (intIndex + 1));
                }
                dr["Model"] = dt1.Rows[intJ]["Name"].ToString();
                dr["Employees"] = dt.Rows[intI]["Name_En"].ToString();
                dr["Department"] = strDepartmentName;
                dr["Team"] = dt.Rows[intI]["Team"].ToString();
                dr["Detail"] = dt1.Rows[intJ]["Item"].ToString();

                double dHoursP = 0;
                string strHoursP;

                dHoursP = Convert.ToDouble(dt1.Rows[intJ]["Hours"].ToString()) / workday;
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

               

                dt_new.Rows.Add(dr);
            }
        }

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_TimeReport.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt_new);
        rptDoc.SetDataSource(ds);
        rptDoc.SetParameterValue("StartDate", strStartDate);
        rptDoc.SetParameterValue("EndDate", strEndDate);
        //rptDoc.SetParameterValue("UseTotal", intUseTotal);
        //rptDoc.SetParameterValue("BorrowTotal", intBorrowTotal);


        CrystalReportViewer1.ReportSource = rptDoc;
        CrystalReportViewer1.DataBind();

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
}
