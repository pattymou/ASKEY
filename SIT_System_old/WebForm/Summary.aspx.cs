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

public partial class WebForm_Summary : System.Web.UI.Page
{
    //public static string strKind;
    //public static string strCustomer;
    //public static string strPName;
    //public static string strID;
    
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {

            //strID = Request.QueryString["ID"];
            //strKind = "xDSL IAD";
            //strCustomer = "ALL";
            //strPName = "ALL";
            if (Request.QueryString["ID"] == "1")
            {
                //strKind = Request.QueryString["Kind"];
                //strCustomer = Request.QueryString["Customer"];
                //strPName = Request.QueryString["PName"];
                getSummary();
            }
            else
                getSummary1();
            
        }

    }

    private void getSummary1()
    {
        string strNo;
        decimal dPassRate, dFailRate, dTBDRate, dNA, dNT;

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Owner");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Owner";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Kind");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Kind";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Category");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Category";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("TestResult");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "TestResult";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column5);


        DataTable dt_new1 = new DataTable("dt_new1");

        DataColumn column11 = new DataColumn("Owner");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "Owner";
        column11.DefaultValue = "0";
        dt_new1.Columns.Add(column11);

        DataColumn column12 = new DataColumn("Plan");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "Plan";
        column12.DefaultValue = "0";
        dt_new1.Columns.Add(column12);

        DataColumn column13 = new DataColumn("Testcases");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "Testcases";
        column13.DefaultValue = "0";
        dt_new1.Columns.Add(column13);

        DataColumn column14 = new DataColumn("Remaining");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "Remaining";
        column14.DefaultValue = "0";
        dt_new1.Columns.Add(column14);

        DataColumn column15 = new DataColumn("Pass");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "Pass";
        column15.DefaultValue = "0";
        dt_new1.Columns.Add(column15);

        DataColumn column16 = new DataColumn("Fail");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "Fail";
        column16.DefaultValue = "0";
        dt_new1.Columns.Add(column16);

        DataColumn column17 = new DataColumn("TBD");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "TBD";
        column17.DefaultValue = "0";
        dt_new1.Columns.Add(column17);

        DataColumn column18 = new DataColumn("NT");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "NT";
        column18.DefaultValue = "0";
        dt_new1.Columns.Add(column18);

        DataColumn column19 = new DataColumn("NA");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "NA";
        column19.DefaultValue = "0";
        dt_new1.Columns.Add(column19);

        DataColumn column20 = new DataColumn("PassRate");
        column20.DataType = System.Type.GetType("System.String");
        column20.AllowDBNull = true;
        column20.Caption = "PassRate";
        column20.DefaultValue = "0";
        dt_new1.Columns.Add(column20);

        DataColumn column21 = new DataColumn("FailRate");
        column21.DataType = System.Type.GetType("System.String");
        column21.AllowDBNull = true;
        column21.Caption = "FailRate";
        column21.DefaultValue = "0";
        dt_new1.Columns.Add(column21);

        DataColumn column22 = new DataColumn("TBDRate");
        column22.DataType = System.Type.GetType("System.String");
        column22.AllowDBNull = true;
        column22.Caption = "TBDRate";
        column22.DefaultValue = "0";
        dt_new1.Columns.Add(column22);

        DataColumn column23 = new DataColumn("ExecutionRate");
        column23.DataType = System.Type.GetType("System.String");
        column23.AllowDBNull = true;
        column23.Caption = "ExecutionRate";
        column23.DefaultValue = "0";
        dt_new1.Columns.Add(column23);

        DataColumn column24 = new DataColumn("NARate");
        column24.DataType = System.Type.GetType("System.String");
        column24.AllowDBNull = true;
        column24.Caption = "NARate";
        column24.DefaultValue = "0";
        dt_new1.Columns.Add(column24);

        DataColumn column25 = new DataColumn("NTRate");
        column25.DataType = System.Type.GetType("System.String");
        column25.AllowDBNull = true;
        column25.Caption = "NTRate";
        column25.DefaultValue = "0";
        dt_new1.Columns.Add(column25);

        DataColumn column26 = new DataColumn("TOTAL");
        column26.DataType = System.Type.GetType("System.String");
        column26.AllowDBNull = true;
        column26.Caption = "TOTAL";
        column26.DefaultValue = "0";
        dt_new1.Columns.Add(column26);

        

        HttpCookie cookie_SummaryNo = Request.Cookies["SummaryNo"];
        strNo = Server.UrlDecode(cookie_SummaryNo.Value);

        string[] strs = strNo.Split(',');        

        foreach (string strI in strs)
        {
            if (strI != "")
            {
                DataTable dt = clsData.UploadSummary1(strI);

                DataRow dr = dt_new.NewRow();

                dr["Owner"] = dt.Rows[0]["Owner"].ToString();
                dr["ID"] = dt.Rows[0]["ID"].ToString();
                dr["Kind"] = dt.Rows[0]["Kind"].ToString();

                dr["Category"] = dt.Rows[0]["Category"].ToString();
                dr["TestResult"] = dt.Rows[0]["TestResult"].ToString();


                dt_new.Rows.Add(dr);
            }
        }

        DataTable dtDistinct;

        string str1;

        dtDistinct =dt_new.DefaultView.ToTable(true,new string[] {"Category"});
        for (int i = 0; i < dtDistinct.Rows.Count; i++)
        {
            string strOwner = "";
            int intPass = 0;
            int intFail = 0;
            int intTBD = 0;
            int intNT = 0;
            int intNA = 0;

            str1 = dtDistinct.Rows[i]["Category"].ToString();
            DataRow[] dr=dt_new.Select("Category = '" + str1 + "'");
            foreach (DataRow d in dr)
            {
                //string strOwner1 = "";
                strOwner = strOwner + d[0].ToString() + "/";
                if (d[4].ToString() == "Pass")
                    intPass++;
                else if (d[4].ToString() == "Fail")
                    intFail++;
                else if (d[4].ToString() == "TBD")
                    intTBD++;
                else if (d[4].ToString() == "N/T")
                    intNT++;
                else if (d[4].ToString() == "N/A")
                    intNA++;

                
            }

            DataRow dr1 = dt_new1.NewRow();
            dr1["Owner"] = strOwner;

            dr1["Plan"] = str1;
            dr1["Testcases"] = dr.Length;

            dr1["Pass"] = intPass.ToString();
            dr1["Fail"] = intFail.ToString();
            dr1["TBD"] = intTBD.ToString();
            dr1["NT"] = intNT.ToString();
            dr1["NA"] = intNA.ToString();


            dPassRate = (decimal)intPass / int.Parse(dr.Length.ToString()) * 100;

            //dPassRate = int.Parse(dt.Rows[i]["Pass"].ToString()) / int.Parse(dt.Rows[i]["testplan1"].ToString());
            dFailRate = (decimal)intFail / int.Parse(dr.Length.ToString()) * 100;
            dTBDRate = (decimal)intTBD / int.Parse(dr.Length.ToString()) * 100;
            dNA = (decimal)intNA / int.Parse(dr.Length.ToString()) * 100;
            dNT = (decimal)intNT / int.Parse(dr.Length.ToString()) * 100;

            dr1["PassRate"] = (int)dPassRate + "%";
            dr1["FailRate"] = (int)dFailRate + "%";
            dr1["TBDRate"] = (int)dTBDRate + "%";
            dr1["ExecutionRate"] = (int)dPassRate + (int)dFailRate + (int)dTBDRate + "%";
            dr1["NARate"] = (int)dNA + "%";
            dr1["NTRate"] = (int)dNT + "%";
            dr1["TOTAL"] = (int)dPassRate + (int)dFailRate + (int)dTBDRate + (int)dNA + (int)dNT + "%";


            dr1["Remaining"] = int.Parse(dr.Length.ToString()) - intPass - intFail - intTBD - intNA - intNT;




            dt_new1.Rows.Add(dr1);

            
        }

        gvwMain.DataSource = dt_new1;
        gvwMain.DataBind();

    }

    private void getSummary()
    {
        string strNo;
        decimal dPassRate, dFailRate, dTBDRate, dNA, dNT;

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Owner");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Owner";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Kind");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Kind";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Category");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Category";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("TestResult");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "TestResult";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column5);


        DataTable dt_new1 = new DataTable("dt_new1");

        DataColumn column11 = new DataColumn("Owner");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "Owner";
        column11.DefaultValue = "0";
        dt_new1.Columns.Add(column11);

        DataColumn column12 = new DataColumn("Plan");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "Plan";
        column12.DefaultValue = "0";
        dt_new1.Columns.Add(column12);

        DataColumn column13 = new DataColumn("Testcases");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "Testcases";
        column13.DefaultValue = "0";
        dt_new1.Columns.Add(column13);

        DataColumn column14 = new DataColumn("Remaining");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "Remaining";
        column14.DefaultValue = "0";
        dt_new1.Columns.Add(column14);

        DataColumn column15 = new DataColumn("Pass");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "Pass";
        column15.DefaultValue = "0";
        dt_new1.Columns.Add(column15);

        DataColumn column16 = new DataColumn("Fail");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "Fail";
        column16.DefaultValue = "0";
        dt_new1.Columns.Add(column16);

        DataColumn column17 = new DataColumn("TBD");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "TBD";
        column17.DefaultValue = "0";
        dt_new1.Columns.Add(column17);

        DataColumn column18 = new DataColumn("NT");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "NT";
        column18.DefaultValue = "0";
        dt_new1.Columns.Add(column18);

        DataColumn column19 = new DataColumn("NA");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "NA";
        column19.DefaultValue = "0";
        dt_new1.Columns.Add(column19);

        DataColumn column20 = new DataColumn("PassRate");
        column20.DataType = System.Type.GetType("System.String");
        column20.AllowDBNull = true;
        column20.Caption = "PassRate";
        column20.DefaultValue = "0";
        dt_new1.Columns.Add(column20);

        DataColumn column21 = new DataColumn("FailRate");
        column21.DataType = System.Type.GetType("System.String");
        column21.AllowDBNull = true;
        column21.Caption = "FailRate";
        column21.DefaultValue = "0";
        dt_new1.Columns.Add(column21);

        DataColumn column22 = new DataColumn("TBDRate");
        column22.DataType = System.Type.GetType("System.String");
        column22.AllowDBNull = true;
        column22.Caption = "TBDRate";
        column22.DefaultValue = "0";
        dt_new1.Columns.Add(column22);

        DataColumn column23 = new DataColumn("ExecutionRate");
        column23.DataType = System.Type.GetType("System.String");
        column23.AllowDBNull = true;
        column23.Caption = "ExecutionRate";
        column23.DefaultValue = "0";
        dt_new1.Columns.Add(column23);

        DataColumn column24 = new DataColumn("NARate");
        column24.DataType = System.Type.GetType("System.String");
        column24.AllowDBNull = true;
        column24.Caption = "NARate";
        column24.DefaultValue = "0";
        dt_new1.Columns.Add(column24);

        DataColumn column25 = new DataColumn("NTRate");
        column25.DataType = System.Type.GetType("System.String");
        column25.AllowDBNull = true;
        column25.Caption = "NTRate";
        column25.DefaultValue = "0";
        dt_new1.Columns.Add(column25);

        DataColumn column26 = new DataColumn("TOTAL");
        column26.DataType = System.Type.GetType("System.String");
        column26.AllowDBNull = true;
        column26.Caption = "TOTAL";
        column26.DefaultValue = "0";
        dt_new1.Columns.Add(column26);


        DataTable dt_plan = clsData.UploadSummary(Request.QueryString["Kind"], Request.QueryString["Customer"], Request.QueryString["PName"]);

        //HttpCookie cookie_SummaryNo = Request.Cookies["SummaryNo"];
        //strNo = Server.UrlDecode(cookie_SummaryNo.Value);

        //string[] strs = strNo.Split(',');

        //foreach (string strI in strs)
        for (int i = 0; i < dt_plan.Rows.Count; i++)
        {
            //if (strI != "")
            //{
            DataTable dt = clsData.UploadSummary1(dt_plan.Rows[i]["ID"].ToString());

                DataRow dr = dt_new.NewRow();

                dr["Owner"] = dt.Rows[0]["Owner"].ToString();
                dr["ID"] = dt.Rows[0]["ID"].ToString();
                dr["Kind"] = dt.Rows[0]["Kind"].ToString();

                dr["Category"] = dt.Rows[0]["Category"].ToString();
                dr["TestResult"] = dt.Rows[0]["TestResult"].ToString();


                dt_new.Rows.Add(dr);
            //}
        }

        DataTable dtDistinct;

        string str1;

        dtDistinct = dt_new.DefaultView.ToTable(true, new string[] { "Category" });
        for (int i = 0; i < dtDistinct.Rows.Count; i++)
        {
            string strOwner = "";
            int intPass = 0;
            int intFail = 0;
            int intTBD = 0;
            int intNT = 0;
            int intNA = 0;

            str1 = dtDistinct.Rows[i]["Category"].ToString();
            DataRow[] dr = dt_new.Select("Category = '" + str1 + "'");
            foreach (DataRow d in dr)
            {
                //string strOwner1 = "";
                strOwner = strOwner + d[0].ToString() + "/";
                if (d[4].ToString() == "Pass")
                    intPass++;
                else if (d[4].ToString() == "Fail")
                    intFail++;
                else if (d[4].ToString() == "TBD")
                    intTBD++;
                else if (d[4].ToString() == "N/T")
                    intNT++;
                else if (d[4].ToString() == "N/A")
                    intNA++;


            }

            DataRow dr1 = dt_new1.NewRow();
            dr1["Owner"] = strOwner;

            dr1["Plan"] = str1;
            dr1["Testcases"] = dr.Length;

            dr1["Pass"] = intPass.ToString();
            dr1["Fail"] = intFail.ToString();
            dr1["TBD"] = intTBD.ToString();
            dr1["NT"] = intNT.ToString();
            dr1["NA"] = intNA.ToString();


            dPassRate = (decimal)intPass / int.Parse(dr.Length.ToString()) * 100;

            //dPassRate = int.Parse(dt.Rows[i]["Pass"].ToString()) / int.Parse(dt.Rows[i]["testplan1"].ToString());
            dFailRate = (decimal)intFail / int.Parse(dr.Length.ToString()) * 100;
            dTBDRate = (decimal)intTBD / int.Parse(dr.Length.ToString()) * 100;
            dNA = (decimal)intNA / int.Parse(dr.Length.ToString()) * 100;
            dNT = (decimal)intNT / int.Parse(dr.Length.ToString()) * 100;

            dr1["PassRate"] = (int)dPassRate + "%";
            dr1["FailRate"] = (int)dFailRate + "%";
            dr1["TBDRate"] = (int)dTBDRate + "%";
            dr1["ExecutionRate"] = (int)dPassRate + (int)dFailRate + (int)dTBDRate + "%";
            dr1["NARate"] = (int)dNA + "%";
            dr1["NTRate"] = (int)dNT + "%";
            dr1["TOTAL"] = (int)dPassRate + (int)dFailRate + (int)dTBDRate + (int)dNA + (int)dNT + "%";


            dr1["Remaining"] = int.Parse(dr.Length.ToString()) - intPass - intFail - intTBD - intNA - intNT;

            dt_new1.Rows.Add(dr1);

        }

        gvwMain.DataSource = dt_new1;
        gvwMain.DataBind();

        
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
    }
}
