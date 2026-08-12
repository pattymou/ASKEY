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

public partial class Report_rpt_StatisticsCReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate, strEndDate, strDepartment, strKind;

        strStartDate = Session["RDateS"].ToString();
        strEndDate = Session["RDateE"].ToString();
        strDepartment = Session["RDep"].ToString();
        strKind = Session["RKind"].ToString();

        //DataTable dt_new1 = new DataTable("dt_new1");

        //DataColumn column1 = new DataColumn("Department");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "Department";
        //column1.DefaultValue = "0";
        //dt_new1.Columns.Add(column1);

        //DataColumn column2 = new DataColumn("Kind");
        //column2.DataType = System.Type.GetType("System.String");
        //column2.AllowDBNull = true;
        //column2.Caption = "Kind";
        //column2.DefaultValue = "0";
        //dt_new1.Columns.Add(column2);

        //DataColumn column3 = new DataColumn("Name");
        //column3.DataType = System.Type.GetType("System.String");
        //column3.AllowDBNull = true;
        //column3.Caption = "Name";
        //column3.DefaultValue = "0";
        //dt_new1.Columns.Add(column3);

        //DataColumn column4 = new DataColumn("Quoted");
        //column4.DataType = System.Type.GetType("System.int");
        //column4.AllowDBNull = true;
        //column4.Caption = "QuotedTotal";
        //column4.DefaultValue = "0";
        //dt_new1.Columns.Add(column4);

        //DataColumn column5 = new DataColumn("Reimburse");
        //column5.DataType = System.Type.GetType("System.int");
        //column5.AllowDBNull = true;
        //column5.Caption = "ReimburseTotal";
        //column5.DefaultValue = "0";
        //dt_new1.Columns.Add(column5);

        //DataColumn column6 = new DataColumn("ID");
        //column6.DataType = System.Type.GetType("System.String");
        //column6.AllowDBNull = true;
        //column6.Caption = "ID";
        //column6.DefaultValue = "0";
        //dt_new1.Columns.Add(column6);


        DataTable dt_new = clsData.UploadStatisticsCReport(strDepartment, strStartDate, strEndDate, strKind);

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_StatisticsCReport.rpt"));


        //DataSet ds = new DataSet();
        //ds.Tables.Add(dt_new1);
        rptDoc.SetDataSource(dt_new);
        rptDoc.SetParameterValue("StartDate", Session["RDateS"].ToString());
        rptDoc.SetParameterValue("EndDate", Session["RDateE"].ToString());
        //rptDoc.SetParameterValue("UseTotal", intUseTotal);
        //rptDoc.SetParameterValue("BorrowTotal", intBorrowTotal);


        CrystalReportViewer1.ReportSource = rptDoc;
        CrystalReportViewer1.DataBind();

    }
}
