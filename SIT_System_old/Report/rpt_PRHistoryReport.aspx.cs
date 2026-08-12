using System;
using System.Collections;
using System.Configuration;
//using System;
using System.IO;
using System.Data;
//using System.Configuration;
//using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Report_rpt_PRHistoryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate, strStartDate1, strEndDate, strEndDate1;

        strStartDate = Session["RDateS"].ToString();
        strEndDate = Session["RDateE"].ToString();

        DateTime dTime;
        string strDate;
        DataTable dt1 = clsData.UploadPRReportQuery("1", strStartDate, strEndDate, Session["Report_Local"].ToString());

        DataTable dt = new DataTable("dt");

        DataColumn column1 = new DataColumn("Application_Date");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Application_Date";
        column1.DefaultValue = "0";
        dt.Columns.Add(column1);

        DataColumn column2 = new DataColumn("PR_No");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "PR_No";
        column2.DefaultValue = "0";
        dt.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Signed_ID");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Signed_ID";
        column3.DefaultValue = "0";
        dt.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Demand_Person");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Demand_Person";
        column4.DefaultValue = "0";
        dt.Columns.Add(column4);

        DataColumn column5 = new DataColumn("g_name");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "g_name";
        column5.DefaultValue = "0";
        dt.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Part_No");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Part_No";
        column6.DefaultValue = "0";
        dt.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Unit");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Unit";
        column7.DefaultValue = "0";
        dt.Columns.Add(column7);

        DataColumn column8 = new DataColumn("Purchase_Quantity");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "Purchase_Quantity";
        column8.DefaultValue = "0";
        dt.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Demand_Team");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Demand_Team";
        column9.DefaultValue = "0";
        dt.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Estimated_TotalPrice");
        column10.DataType = System.Type.GetType("System.Double");
        column10.AllowDBNull = true;
        column10.Caption = "Estimated_TotalPrice";
        column10.DefaultValue = "0";
        dt.Columns.Add(column10);

        DataColumn column11 = new DataColumn("Arrival_Date");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "Arrival_Date";
        column11.DefaultValue = "0";
        dt.Columns.Add(column11);

        DataColumn column12 = new DataColumn("Check_Date");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "Check_Date";
        column12.DefaultValue = "0";
        dt.Columns.Add(column12);

        DataColumn column13 = new DataColumn("ExchangeRate");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "ExchangeRate";
        column13.DefaultValue = "0";
        dt.Columns.Add(column13);

        DataTable dt2;
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["Goods_ID"].ToString().IndexOf("A") != -1)
                {
                    dt2 = clsData.UploadApparatusQuery(dt1.Rows[i]["Goods_ID"].ToString(), "1", "");
                }
                else
                {
                    dt2 = clsData.UploadGoodsQuery(dt1.Rows[i]["Goods_ID"].ToString(), "1", "");
                }

                DataRow dr = dt.NewRow();

                dTime = Convert.ToDateTime(dt1.Rows[i]["Application_Date"].ToString().Trim());
                strDate = dTime.ToString("yyyy/MM/dd");

                dr["Application_Date"] = strDate;
                dr["PR_No"] = dt1.Rows[i]["PR_No"].ToString();
                dr["Signed_ID"] = dt1.Rows[i]["Signed_ID"].ToString();
                dr["Demand_Person"] = dt1.Rows[i]["Demand_Person"].ToString();
                //dr["g_name"] = dt1.Rows[i]["g_name"].ToString();
                if (dt1.Rows[i]["Goods_ID"].ToString().IndexOf("A") != -1)
                {
                    dr["g_name"] = dt2.Rows[0]["Name"].ToString();
                }
                else if (dt1.Rows[i]["Goods_ID"].ToString().IndexOf("G") != -1)
                {
                    dr["g_name"] = dt2.Rows[0]["Name_CH"].ToString() + "-" + dt2.Rows[0]["Name_En"].ToString();
                }
                else
                {
                    dr["g_name"] = "";
                }

                if ((dt2.Rows[0]["Part_No"].ToString() != null) || (dt2.Rows[0]["Part_No"].ToString() != ""))
                    dr["Part_No"] = dt2.Rows[0]["Part_No"].ToString();
                else
                    dr["Part_No"] = "";
                //dr["Part_No"] = dt1.Rows[i]["Part_No"].ToString();
                dr["Unit"] = dt1.Rows[i]["Unit"].ToString();
                dr["Purchase_Quantity"] = dt1.Rows[i]["Purchase_Quantity"].ToString();
                dr["Demand_Team"] = dt1.Rows[i]["Demand_Team"].ToString();
                dr["Estimated_TotalPrice"] = dt1.Rows[i]["Estimated_TotalPrice"].ToString();

                dTime = Convert.ToDateTime(dt1.Rows[i]["Arrival_Date"].ToString().Trim());
                strDate = dTime.ToString("yyyy/MM/dd");
                dr["Arrival_Date"] = strDate;

                dTime = Convert.ToDateTime(dt1.Rows[i]["Check_Date"].ToString().Trim());
                strDate = dTime.ToString("yyyy/MM/dd");
                dr["Check_Date"] = strDate;
                dr["ExchangeRate"] = dt1.Rows[i]["ExchangeRate"].ToString();

                dt.Rows.Add(dr);
            }
        }


        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_PRHistoryReport.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        rptDoc.SetDataSource(ds);
        rptDoc.SetParameterValue("StartDate", Session["RDateS"].ToString());
        rptDoc.SetParameterValue("EndDate", Session["RDateE"].ToString());


        CrystalReportViewer1.ReportSource = rptDoc;
        CrystalReportViewer1.DataBind();
    }
}
