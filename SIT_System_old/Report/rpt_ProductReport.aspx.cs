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

public partial class Report_rpt_ProductReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strProducts_ID;
        string strStartDate, strStartDate1, strEndDate, strEndDate1, strID;
        int intUseTime, intUseHoliday, intJ;

        intUseTime = 0;
        intUseHoliday = 0;

        HttpCookie cookie_ReportDateS = Request.Cookies["ReportDateS"];
        strStartDate = Server.UrlDecode(cookie_ReportDateS.Value);

        HttpCookie cookie_ReportDateE = Request.Cookies["ReportDateE"];
        strEndDate = Server.UrlDecode(cookie_ReportDateE.Value);

        HttpCookie cookie_Products_ID = Request.Cookies["Products_ID"];
        strProducts_ID = Server.UrlDecode(cookie_Products_ID.Value);

        //strStartDate = Session["ReportDateS"].ToString();
        //strEndDate = Session["ReportDateE"].ToString();
        //strProducts_ID = Session["Products_ID"].ToString();

        strStartDate1 = strStartDate;
        strEndDate1 = strEndDate;

        strStartDate = strStartDate + " 09:00";
        strEndDate = strEndDate + " 18:30";

        //strStartDate = "2017/01/01 09:00";
        //strEndDate = "2017/05/01 18:30";
        //strProducts_ID = "537186";

        DataTable dt = clsData.UploadProductReportQuery(strStartDate, strEndDate, strProducts_ID);

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Products_ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Products_ID";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Name");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Name";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Custodian_Department");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Custodian_Department";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Borrower");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Borrower";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Department");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Department";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("UseTime");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "UseTime";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("WorkTime");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "WorkTime";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("UseHoliday");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "UseHoliday";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Holiday");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Holiday";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("WorkPercent");
        column11.DataType = System.Type.GetType("System.Decimal");
        column11.AllowDBNull = true;
        column11.Caption = "WorkPercent";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("HolidayPercent");
        column12.DataType = System.Type.GetType("System.Decimal");
        column12.AllowDBNull = true;
        column12.Caption = "HolidayPercent";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                intJ = i + 1;
                if (intJ == dt.Rows.Count)
                {
                    strID = "";
                }
                else
                    strID = dt.Rows[intJ]["Department"].ToString();

                if (dt.Rows[i]["Department"].ToString() == strID)
                {
                    intUseTime = intUseTime + Convert.ToInt32(dt.Rows[i]["UseTime"].ToString());
                    intUseHoliday = intUseHoliday + Convert.ToInt32(dt.Rows[i]["UseHoliday"].ToString());
                }
                else
                {
                    intUseTime = intUseTime + Convert.ToInt32(dt.Rows[i]["UseTime"].ToString());
                    intUseHoliday = intUseHoliday + Convert.ToInt32(dt.Rows[i]["UseHoliday"].ToString());
                    int intWorkTime = Convert.ToInt32(dt.Rows[i]["WorkTime"].ToString());
                    int intHoliday = Convert.ToInt32(dt.Rows[i]["Holiday"].ToString());

                    decimal decimalTemp = (decimal)intUseTime / intWorkTime;
                    decimal decWorkPercent = Math.Round(decimalTemp, 4);

                    decimal decimalTemp1 = (decimal)intUseHoliday / intHoliday;
                    decimal decHolidayPercent = Math.Round(decimalTemp1, 4);


                    DataRow dr = dt_new.NewRow();
                    dr["ID"] = dt.Rows[i]["ID"].ToString();
                    dr["Products_ID"] = dt.Rows[i]["Products_ID"].ToString();
                    dr["Name"] = dt.Rows[i]["Name"].ToString();
                    dr["Custodian_Department"] = dt.Rows[i]["Custodian_Department"].ToString();
                    dr["Borrower"] = dt.Rows[i]["Borrower"].ToString();
                    dr["Department"] = dt.Rows[i]["Department"].ToString();
                    dr["UseTime"] = intUseTime;
                    dr["WorkTime"] = dt.Rows[i]["WorkTime"].ToString();
                    dr["UseHoliday"] = intUseHoliday;
                    dr["Holiday"] = dt.Rows[i]["Holiday"].ToString();
                    dr["WorkPercent"] = decWorkPercent * 100;
                    dr["HolidayPercent"] = decHolidayPercent * 100;

                    dt_new.Rows.Add(dr);

                    intUseTime = 0;
                    intUseHoliday = 0;

                }

            }
        }

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_ProductReport.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt_new);
        rptDoc.SetDataSource(ds);
        rptDoc.SetParameterValue("StartDate", strStartDate1);
        rptDoc.SetParameterValue("EndDate", strEndDate1);
        rptDoc.SetParameterValue("ProductsID", dt.Rows[0]["Products_ID"].ToString());
        rptDoc.SetParameterValue("ProductsName", dt.Rows[0]["Name"].ToString());

        CrystalReportViewer1.ReportSource = rptDoc;
        CrystalReportViewer1.DataBind();


    }
}
