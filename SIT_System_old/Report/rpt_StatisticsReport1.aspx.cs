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

public partial class Report_rpt_StatisticsReport1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate, strStartDate1, strEndDate, strEndDate1, strID, strDepartment;
        int intUseTime, intJ, intBorrowingTimes,intUseTotal,intBorrowTotal;
        string strGName,strDep;
        double dTotal;

        strStartDate = Session["RDateS"].ToString() + " 00:00:00";
        strEndDate = Session["RDateE"].ToString() + " 23:59:59";
        strDepartment = Session["RDep"].ToString();


        

        DataTable dt_new1 = new DataTable("dt_new1");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new1.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Products_ID");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Products_ID";
        column2.DefaultValue = "0";
        dt_new1.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Name");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Name";
        column3.DefaultValue = "0";
        dt_new1.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Mission");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Mission";
        column4.DefaultValue = "0";
        dt_new1.Columns.Add(column4);

        DataColumn column5 = new DataColumn("GName");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "GName";
        column5.DefaultValue = "0";
        dt_new1.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Borrower");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Borrower";
        column6.DefaultValue = "0";
        dt_new1.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Department");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Department";
        column7.DefaultValue = "0";
        dt_new1.Columns.Add(column7);

        DataColumn column8 = new DataColumn("UseTime");
        column8.DataType = System.Type.GetType("System.Int16");
        column8.AllowDBNull = true;
        column8.Caption = "UseTime";
        column8.DefaultValue = "0";
        dt_new1.Columns.Add(column8);

        DataColumn column9 = new DataColumn("BorrowingTimes");
        column9.DataType = System.Type.GetType("System.Int16");
        column9.AllowDBNull = true;
        column9.Caption = "BorrowingTimes";
        column9.DefaultValue = "0";
        dt_new1.Columns.Add(column9);

        DataColumn column10 = new DataColumn("UsePercent");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "UsePercent";
        column10.DefaultValue = "0";
        dt_new1.Columns.Add(column10);

        DataColumn column11 = new DataColumn("BorrowingPercent");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "BorrowingPercent";
        column11.DefaultValue = "0";
        dt_new1.Columns.Add(column11);

        strGName = "";
        intUseTime = 0;
        intBorrowingTimes = 0;
        intUseTotal = 0;
        intBorrowTotal = 0;
        if (Session["RDep"].ToString() == "ALL")
        {
            DataTable dt2 = clsData.UploadApparatusReportDep(strStartDate, strEndDate, "ALL", "1", "", "", Session["RLocal"].ToString());
            intBorrowTotal = Convert.ToInt32(dt2.Rows[0]["tcount"].ToString());

            dt2 = clsData.UploadApparatusReportDep(strStartDate, strEndDate, "ALL", "0", "", "", Session["RLocal"].ToString());
            for (int x = 0; x < dt2.Rows.Count; x++)
            {
                intUseTotal = intUseTotal + Convert.ToInt32(dt2.Rows[x]["UseTime"].ToString());
            }
            

            DataTable dt1 = clsData.UploadDepartment();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DataTable dt = clsData.UploadApparatusReportDep(strStartDate, strEndDate, dt1.Rows[i]["Name"].ToString(), "0", "", "", Session["RLocal"].ToString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["Department"].ToString() == "D210")
                        strStartDate1 = "0";
                    strGName = dt.Rows[j]["Products_ID"].ToString();

                    if (j == dt.Rows.Count-1)
                    {
                        intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                        intBorrowingTimes = intBorrowingTimes + 1;

                        DataRow dr = dt_new1.NewRow();
                        dr["ID"] = dt.Rows[j]["ID"].ToString();
                        dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                        dr["Name"] = dt.Rows[j]["Name"].ToString();
                        dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                        dr["GName"] = dt.Rows[j]["GName"].ToString();
                        dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                        dr["Department"] = dt.Rows[j]["Department"].ToString();
                        if (intUseTime == 0)
                        {
                            dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
                            dr["UsePercent"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) / intUseTotal).ToString("#0.00");
                        }
                        else
                        {
                            dr["UseTime"] = intUseTime;
                            dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(intUseTotal)) * 100;
                            dr["UsePercent"] = dTotal.ToString("#0.00") + " %";
                        }
                        dr["BorrowingTimes"] = intBorrowingTimes;
                        dTotal = (Convert.ToDouble(intBorrowingTimes) / Convert.ToDouble(intBorrowTotal)) * 100;
                        dr["BorrowingPercent"] = dTotal.ToString("#0.00") + " %";
                        dt_new1.Rows.Add(dr);

                        intUseTime = 0;
                        intBorrowingTimes = 0;
                    }
                    else
                    {
                        if (strGName != dt.Rows[j + 1]["Products_ID"].ToString())
                        {
                            intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                            intBorrowingTimes = intBorrowingTimes + 1;

                            DataRow dr = dt_new1.NewRow();
                            dr["ID"] = dt.Rows[j]["ID"].ToString();
                            dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                            dr["Name"] = dt.Rows[j]["Name"].ToString();
                            dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                            dr["GName"] = dt.Rows[j]["GName"].ToString();
                            dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                            dr["Department"] = dt.Rows[j]["Department"].ToString();
                            if (intUseTime == 0)
                            {
                                dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
                                dr["UsePercent"] = (Convert.ToInt32(dt.Rows[j]["UseTime"].ToString()) / intUseTotal).ToString("#0.00");
                            }
                            else
                            {
                                dr["UseTime"] = intUseTime;
                                dTotal = (Convert.ToDouble(intUseTime) / Convert.ToDouble(intUseTotal)) * 100;
                                dr["UsePercent"] = dTotal.ToString("#0.00") + " %";
                            }
                            dr["BorrowingTimes"] = intBorrowingTimes;
                            dTotal = (Convert.ToDouble(intBorrowingTimes) / Convert.ToDouble(intBorrowTotal)) * 100;
                            dr["BorrowingPercent"] = dTotal.ToString("#0.00") +" %";


                            dt_new1.Rows.Add(dr);
                            //strGName = dt.Rows[i]["Products_ID"].ToString();
                            intUseTime = 0;
                            intBorrowingTimes = 0;
                        }
                        else
                        {
                            intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                            intBorrowingTimes = intBorrowingTimes + 1;
                        }
                    }
                    //intUseTotal = intUseTotal + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                    //intBorrowTotal = intBorrowTotal + 1;

                }
            }
        }
        else
        {
            intUseTotal = 0;
            intBorrowTotal = 0;
            DataTable dt = clsData.UploadApparatusReportDep(strStartDate, strEndDate, strDepartment, "0", "", "", Session["RLocal"].ToString());
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                strGName = dt.Rows[j]["Products_ID"].ToString();

                if (j == dt.Rows.Count-1)
                {
                    DataRow dr = dt_new1.NewRow();
                    dr["ID"] = dt.Rows[j]["ID"].ToString();
                    dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                    dr["Name"] = dt.Rows[j]["Name"].ToString();
                    dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                    dr["GName"] = dt.Rows[j]["GName"].ToString();
                    dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                    dr["Department"] = dt.Rows[j]["Department"].ToString();
                    if (intUseTime == 0)
                        dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
                    else
                        dr["UseTime"] = intUseTime;
                    dr["BorrowingTimes"] = intBorrowingTimes;

                    dt_new1.Rows.Add(dr);
                }
                else
                {
                    if (strGName != dt.Rows[j + 1]["Products_ID"].ToString())
                    {
                        DataRow dr = dt_new1.NewRow();
                        dr["ID"] = dt.Rows[j]["ID"].ToString();
                        dr["Products_ID"] = dt.Rows[j]["Products_ID"].ToString();
                        dr["Name"] = dt.Rows[j]["Name"].ToString();
                        dr["Mission"] = dt.Rows[j]["Mission"].ToString();
                        dr["GName"] = dt.Rows[j]["GName"].ToString();
                        dr["Borrower"] = dt.Rows[j]["Borrower"].ToString();
                        dr["Department"] = dt.Rows[j]["Department"].ToString();
                        if (intUseTime == 0)
                            dr["UseTime"] = dt.Rows[j]["UseTime"].ToString();
                        else
                            dr["UseTime"] = intUseTime;
                        dr["BorrowingTimes"] = intBorrowingTimes;

                        dt_new1.Rows.Add(dr);
                        //strGName = dt.Rows[i]["Products_ID"].ToString();
                        intUseTime = 0;
                        intBorrowingTimes = 1;
                    }
                    else
                    {
                        intUseTime = intUseTime + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                        intBorrowingTimes = intBorrowingTimes + 1;
                    }
                }
                intUseTotal = intUseTotal + Convert.ToInt32(dt.Rows[j]["UseTime"].ToString());
                intBorrowTotal = intBorrowTotal + 1;

            }
        }

        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_StatisticsReport1.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt_new1);
        rptDoc.SetDataSource(ds);
        rptDoc.SetParameterValue("StartDate", Session["RDateS"].ToString());
        rptDoc.SetParameterValue("EndDate", Session["RDateE"].ToString());
        rptDoc.SetParameterValue("UseTotal", intUseTotal);
        rptDoc.SetParameterValue("BorrowTotal", intBorrowTotal);


        CrystalReportViewer1.ReportSource = rptDoc;
        CrystalReportViewer1.DataBind();


        
    }
}
