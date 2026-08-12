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
using ClosedXML.Excel;

public partial class WebForm_PlanView : System.Web.UI.Page
{
    //public static string strID;
    //public static string strKind;
    //public static string strCustomer;
    public static DataTable dt;
    public static DataTable dt_new;
    public static DataTable dt_new1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            //strKind = Request.QueryString["Kind"];
            //strCustomer = Request.QueryString["Customer"];
            //strID = "O2";
            getPlan();
        }

    }

    private void getPlan()
    {
        dt = clsData.UploadTestPlanQuery2(Request.QueryString["ID"], Request.QueryString["Kind"], Request.QueryString["Customer"]);
        lblProjectName.Text = dt.Rows[0]["ProductName"].ToString();
        gvwMain.DataSource = dt;
        gvwMain.DataBind();

    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadTestPlanQuery2(Request.QueryString["ID"], Request.QueryString["Kind"], Request.QueryString["Customer"]); ;

        gvwMain.DataSource = dt;
        gvwMain.DataBind();
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\n", "<br />");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[0].Width = 100;

            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\n", "<br />");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[1].Width = 100;

            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("\n", "<br />");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[2].Width = 130;

            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("\n", "<br />");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[3].Width = 250;

            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("\n", "<br />");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[4].Width = 300;

            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("\n", "<br />");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[5].Width = 300;

            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("\n", "<br />");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[6].Width = 100;

            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("\n", "<br />");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[7].Width = 150;

            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("\n", "<br />");
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[8].Width = 100;
        }
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


        dt_new1 = new DataTable("dt_new1");

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


        DataTable dt_plan = clsData.UploadSummary(Request.QueryString["Kind"], Request.QueryString["Customer"], Request.QueryString["ID"]);

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


        //DataTable dt1 = clsData.UploadSummary(strKind, strCustomer, strID);

        //dt_new = new DataTable("dt_new");

        //DataColumn column1 = new DataColumn("Owner");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "Owner";
        //column1.DefaultValue = "0";
        //dt_new.Columns.Add(column1);

        //DataColumn column2 = new DataColumn("Plan");
        //column2.DataType = System.Type.GetType("System.String");
        //column2.AllowDBNull = true;
        //column2.Caption = "Plan";
        //column2.DefaultValue = "0";
        //dt_new.Columns.Add(column2);

        //DataColumn column3 = new DataColumn("Testcases");
        //column3.DataType = System.Type.GetType("System.String");
        //column3.AllowDBNull = true;
        //column3.Caption = "Testcases";
        //column3.DefaultValue = "0";
        //dt_new.Columns.Add(column3);

        //DataColumn column4 = new DataColumn("Remaining");
        //column4.DataType = System.Type.GetType("System.String");
        //column4.AllowDBNull = true;
        //column4.Caption = "Remaining";
        //column4.DefaultValue = "0";
        //dt_new.Columns.Add(column4);

        //DataColumn column5 = new DataColumn("Pass");
        //column5.DataType = System.Type.GetType("System.String");
        //column5.AllowDBNull = true;
        //column5.Caption = "Pass";
        //column5.DefaultValue = "0";
        //dt_new.Columns.Add(column5);

        //DataColumn column6 = new DataColumn("Fail");
        //column6.DataType = System.Type.GetType("System.String");
        //column6.AllowDBNull = true;
        //column6.Caption = "Fail";
        //column6.DefaultValue = "0";
        //dt_new.Columns.Add(column6);

        //DataColumn column7 = new DataColumn("TBD");
        //column7.DataType = System.Type.GetType("System.String");
        //column7.AllowDBNull = true;
        //column7.Caption = "TBD";
        //column7.DefaultValue = "0";
        //dt_new.Columns.Add(column7);

        //DataColumn column8 = new DataColumn("NT");
        //column8.DataType = System.Type.GetType("System.String");
        //column8.AllowDBNull = true;
        //column8.Caption = "NT";
        //column8.DefaultValue = "0";
        //dt_new.Columns.Add(column8);

        //DataColumn column9 = new DataColumn("NA");
        //column9.DataType = System.Type.GetType("System.String");
        //column9.AllowDBNull = true;
        //column9.Caption = "NA";
        //column9.DefaultValue = "0";
        //dt_new.Columns.Add(column9);

        //DataColumn column10 = new DataColumn("PassRate");
        //column10.DataType = System.Type.GetType("System.String");
        //column10.AllowDBNull = true;
        //column10.Caption = "PassRate";
        //column10.DefaultValue = "0";
        //dt_new.Columns.Add(column10);

        //DataColumn column11 = new DataColumn("FailRate");
        //column11.DataType = System.Type.GetType("System.String");
        //column11.AllowDBNull = true;
        //column11.Caption = "FailRate";
        //column11.DefaultValue = "0";
        //dt_new.Columns.Add(column11);

        //DataColumn column12 = new DataColumn("TBDRate");
        //column12.DataType = System.Type.GetType("System.String");
        //column12.AllowDBNull = true;
        //column12.Caption = "TBDRate";
        //column12.DefaultValue = "0";
        //dt_new.Columns.Add(column12);

        //DataColumn column13 = new DataColumn("ExecutionRate");
        //column13.DataType = System.Type.GetType("System.String");
        //column13.AllowDBNull = true;
        //column13.Caption = "ExecutionRate";
        //column13.DefaultValue = "0";
        //dt_new.Columns.Add(column13);

        //DataColumn column14 = new DataColumn("NARate");
        //column14.DataType = System.Type.GetType("System.String");
        //column14.AllowDBNull = true;
        //column14.Caption = "NARate";
        //column14.DefaultValue = "0";
        //dt_new.Columns.Add(column14);

        //DataColumn column15 = new DataColumn("NTRate");
        //column15.DataType = System.Type.GetType("System.String");
        //column15.AllowDBNull = true;
        //column15.Caption = "NTRate";
        //column15.DefaultValue = "0";
        //dt_new.Columns.Add(column15);

        //DataColumn column16 = new DataColumn("TOTAL");
        //column16.DataType = System.Type.GetType("System.String");
        //column16.AllowDBNull = true;
        //column16.Caption = "TOTAL";
        //column16.DefaultValue = "0";
        //dt_new.Columns.Add(column16);

        //decimal dPassRate, dFailRate, dTBDRate, dNA, dNT;
        //if (dt1.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        DataRow dr = dt_new.NewRow();
        //        dr["Owner"] = dt1.Rows[i]["owner"].ToString();
        //        dr["Plan"] = dt1.Rows[i]["Category"].ToString();
        //        dr["Testcases"] = dt1.Rows[i]["testplan1"].ToString();

        //        dr["Pass"] = dt1.Rows[i]["Pass"].ToString();
        //        dr["Fail"] = dt1.Rows[i]["Fail"].ToString();
        //        dr["TBD"] = dt1.Rows[i]["TBD"].ToString();
        //        dr["NT"] = dt1.Rows[i]["NT"].ToString();
        //        dr["NA"] = dt1.Rows[i]["NA"].ToString();

        //        //int intHoliday = Convert.ToInt32(dt.Rows[i]["Holiday"].ToString());

        //        dPassRate = (decimal)int.Parse(dt1.Rows[i]["Pass"].ToString()) / int.Parse(dt1.Rows[i]["testplan1"].ToString()) * 100;

        //        //dPassRate = int.Parse(dt.Rows[i]["Pass"].ToString()) / int.Parse(dt.Rows[i]["testplan1"].ToString());
        //        dFailRate = (decimal)int.Parse(dt1.Rows[i]["Fail"].ToString()) / int.Parse(dt1.Rows[i]["testplan1"].ToString()) * 100;
        //        dTBDRate = (decimal)int.Parse(dt1.Rows[i]["TBD"].ToString()) / int.Parse(dt1.Rows[i]["testplan1"].ToString()) * 100;
        //        dNA = (decimal)int.Parse(dt1.Rows[i]["NA"].ToString()) / int.Parse(dt1.Rows[i]["testplan1"].ToString()) * 100;
        //        dNT = (decimal)int.Parse(dt1.Rows[i]["NT"].ToString()) / int.Parse(dt1.Rows[i]["testplan1"].ToString()) * 100;

        //        dr["PassRate"] = (int)dPassRate + "%";
        //        dr["FailRate"] = (int)dFailRate + "%";
        //        dr["TBDRate"] = (int)dTBDRate + "%";
        //        dr["ExecutionRate"] = (int)dPassRate + (int)dFailRate + (int)dTBDRate + "%";
        //        dr["NARate"] = (int)dNA + "%";
        //        dr["NTRate"] = (int)dNT + "%";
        //        dr["TOTAL"] = (int)dPassRate + (int)dFailRate + (int)dTBDRate + (int)dNA + (int)dNT + "%";


        //        dr["Remaining"] = int.Parse(dt1.Rows[i]["testplan1"].ToString()) - int.Parse(dt1.Rows[i]["Pass"].ToString()) - int.Parse(dt1.Rows[i]["Fail"].ToString()) - int.Parse(dt1.Rows[i]["TBD"].ToString()) - int.Parse(dt1.Rows[i]["NA"].ToString()) - int.Parse(dt1.Rows[i]["NT"].ToString());

        //        dt_new.Rows.Add(dr);
        //    }
        //}



        //gvwMain.DataSource = dt_new;
        //gvwMain.DataBind();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        getSummary();

        using (XLWorkbook wb = new XLWorkbook())
        {
            dt_new1.TableName = "Summary";
            wb.Worksheets.Add(dt_new1);
            dt.TableName = "TestCase";
            wb.Worksheets.Add(dt);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TestPlan.xls");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                memoryStream.WriteTo(Response.OutputStream);
                memoryStream.Close();
                Response.Flush();
                Response.End();
            }
        }

        //Response.ClearContent();
        //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        //string excelFileName = lblProjectName.Text + ".xls";
        //Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(excelFileName));
        //Response.ContentType = "application/excel";

        //gvwMain.AllowPaging = false;
        //gvwMain.DataSource = dt;
        //gvwMain.DataBind();

        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //gvwMain.RenderControl(htmlWrite);
        //Response.Write(stringWrite.ToString());
        //Response.End();

        //gvwMain.AllowPaging = true;
        //gvwMain.DataSource = dt;
        //gvwMain.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }
    protected void btnSummary_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/WebForm/Summary.aspx?Kind=" + strKind + "&Customer=" + strCustomer + "&PName=" + strID);

        Response.Write("<script>window.open('Summary.aspx?Kind=" + Request.QueryString["Kind"] + "&Customer=" + Request.QueryString["Customer"] + "&PName=" + Request.QueryString["ID"] + "&ID=1" + "');</script>");
    }
}
