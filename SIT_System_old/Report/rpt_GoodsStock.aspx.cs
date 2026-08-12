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

public partial class Report_rpt_GoodsStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable dt_Stock = new DataTable("dt_Stock");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_Stock.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Name");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Name";
        column2.DefaultValue = "0";
        dt_Stock.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Kind");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Kind";
        column3.DefaultValue = "0";
        dt_Stock.Columns.Add(column3);


        DataColumn column4 = new DataColumn("MF");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "MF";
        column4.DefaultValue = "0";
        dt_Stock.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Quantity_Stock");
        column5.DataType = System.Type.GetType("System.Int32");
        column5.AllowDBNull = true;
        column5.Caption = "Quantity_Stock";
        column5.DefaultValue = "0";
        dt_Stock.Columns.Add(column5);


        DataTable dt = clsData.UploadGoodsReportStock(Session["RKind"].ToString());
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            DataRow dr = dt_Stock.NewRow();

            dr["ID"] = dt.Rows[j]["ID"].ToString();
            dr["Name"] = dt.Rows[j]["Name"].ToString();
            dr["Kind"] = dt.Rows[j]["Kind"].ToString();
            dr["MF"] = dt.Rows[j]["MF"].ToString();

            if (dt.Rows[j]["Quantity_Stock"].ToString() != "")
                dr["Quantity_Stock"] = Convert.ToInt32(dt.Rows[j]["Quantity_Stock"].ToString());
            else
                dr["Quantity_Stock"] = 0;

            dt_Stock.Rows.Add(dr);
        }


        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("../Report/CR_GoodsStock.rpt"));


        DataSet ds = new DataSet();
        ds.Tables.Add(dt_Stock);
        rptDoc.SetDataSource(ds);
        //rptDoc.SetParameterValue("StartDate", Session["RDateS"].ToString());
        //rptDoc.SetParameterValue("EndDate", Session["RDateE"].ToString());


        CrystalReportViewer1.ReportSource = rptDoc;
        CrystalReportViewer1.DataBind();
    }
}
