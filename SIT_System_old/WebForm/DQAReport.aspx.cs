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

public partial class WebForm_DQAReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["EmpNo"] == null)
        //    Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadCustomer(this.ddlCustomer);
            //loadNPI(this.ddlNPI);
            //loadDepartment(this.ddlDepartment);

            //if ((Session["AppDep"].ToString() == "Q600(品保總部)") || (Session["AppDep"].ToString() == "DA40-SIT") || (Session["AppDep"].ToString() == "DA40"))
            //{
            //    Table2.Visible = true;
                
            //}
            //else
            //    Table2.Visible = false;
        }
    }

    #region loadNPI
    protected void loadNPI(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 2, "0");
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, "0");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    #region loadProjectName
    protected void loadProjectName(DropDownList DDL, string strCustomer)
    {
        clsDropDownList.ddlProjectName(DDL, strCustomer);
    }
    #endregion

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProjectName(this.ddlName, ddlCustomer.Text);
    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {

        //for (int intI = 0; intI < 1; intI++)
        //{
        //    int i = 1;
        //    foreach (GridViewRow gvItem in gvwMain.Rows)
        //    {
        //        if (gvItem.RowIndex != 0)
        //        {
        //            if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
        //            {
        //                gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
        //                gvItem.Cells[intI].Visible = false;
        //                i = i + 1;
        //            }
        //            else
        //            {
        //                gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
        //                i = 1;
        //            }
        //        }
        //        else
        //            gvItem.Cells[intI].RowSpan = 1;
        //    }
        //}
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[2].ColumnSpan = 3;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;

            //e.Row.Cells[5].ColumnSpan = 3;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;

            //e.Row.Cells[8].ColumnSpan = 3;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;

            //e.Row.Cells[11].ColumnSpan = 3;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;

            

        }
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //DataTable dt_Info;
        //DateTime d_Time;

        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    DataTable dt = clsData.UploadDashBoardSummaryList("0", ddlName.Text, "", "", "", ddlCustomer.Text);
        //    string strProjectID;

        //    strProjectID = "(";
        //    for (int intI = 0; intI < dt.Rows.Count; intI++)
        //    {
        //        if (intI == dt.Rows.Count - 1)
        //            strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "')";
        //        else
        //            strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "' or ";

        //    }

        //    string strID, strHW, strSW, strDate, strDate1;


        //    dt_Info = clsData.UploadDQADashBoard("2", ddlName.Text, ddlNPI.Text, "", "", ddlCustomer.Text);
        //    if (dt_Info.Rows.Count == 0)
        //    {
        //        strID = "";
        //        strHW = "";
        //        strSW = "";
        //        strDate = "";
        //    }
        //    else
        //    {
        //        strID = dt_Info.Rows[0]["ID"].ToString();
        //        strHW = dt_Info.Rows[0]["PCB_Version"].ToString();
        //        strSW = dt_Info.Rows[0]["FW_Version"].ToString();

        //        d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
        //        strDate1 = d_Time.ToString("yyyy/MM/dd");
        //        if (strDate1 == "1900/01/01")
        //            strDate = "";
        //        else
        //            strDate = strDate1;
        //    }

            

        //    //將原有的表頭移除
        //    TableCellCollection oldCell = e.Row.Cells;
        //    oldCell.Clear();

        //    #region 第一列
        //    //多重表頭的第一列
        //    GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    //第一欄
        //    TableCell tc = new TableCell();
        //    tc.Text = " ";
        //    //tc.BackColor = System.Drawing.Color.AliceBlue; //背景色彩
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.VerticalAlign = VerticalAlign.Middle;
        //    tc.Width = 150;
        //    tc.RowSpan = 4; //所跨的row數
        //    tc.ColumnSpan = 1; //所跨的column數
        //    gvRow.Cells.Add(tc); //新增

        //    //第二欄
        //    tc = new TableCell();
        //    tc.Text = "";
        //    //tc.BackColor = System.Drawing.Color.AliceBlue;
        //    tc.Width = 200;
        //    tc.RowSpan = 4;
        //    tc.ColumnSpan = 1;
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    gvRow.Cells.Add(tc);

        //    //第三欄
        //    tc = new TableCell();
        //    tc.Text = strID;
        //    //tc.BackColor = System.Drawing.Color.AliceBlue;
        //    tc.Width = 150;
        //    tc.RowSpan = 1;
        //    tc.ColumnSpan = 3;
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    gvRow.Cells.Add(tc);

            

        //    //新增至GridView
        //    gvwMain.Controls[0].Controls.Add(gvRow);

        //    #endregion

        //    #region 第二列


        //    //多重表頭的第三列
        //    gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    tc = new TableCell();
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.Text = "H/W";
        //    tc.Width = 50;
        //    //tc.BackColor = System.Drawing.Color.FromName("red");
        //    gvRow.Cells.Add(tc);

        //    tc = new TableCell();
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.Text = "S/W";
        //    tc.Width = 50;
        //    //tc.BackColor = System.Drawing.Color.BlueViolet;
        //    gvRow.Cells.Add(tc);

        //    tc = new TableCell();
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.Text = "完成日";
        //    tc.Width = 50;
        //    //tc.BackColor = System.Drawing.Color.BlueViolet;
        //    gvRow.Cells.Add(tc);

            

        //    gvwMain.Controls[0].Controls.Add(gvRow);

        //    //多重表頭的第四列
        //    gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    tc = new TableCell();
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.Text = strHW;
        //    tc.Wrap = true;
        //    tc.Width = 50;
        //    //tc.BackColor = System.Drawing.Color.FromName("red");
        //    gvRow.Cells.Add(tc);

        //    tc = new TableCell();
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.Text = strSW;
        //    tc.Wrap = true;
        //    tc.Width = 50;
        //    //tc.BackColor = System.Drawing.Color.BlueViolet;
        //    gvRow.Cells.Add(tc);

        //    tc = new TableCell();
        //    tc.HorizontalAlign = HorizontalAlign.Center;
        //    tc.Text = strDate;
        //    tc.Wrap = true;
        //    tc.Width = 50;
        //    //tc.BackColor = System.Drawing.Color.BlueViolet;
        //    gvRow.Cells.Add(tc);

        //    gvwMain.Controls[0].Controls.Add(gvRow);

        //    #endregion


        //}
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlName.Text == "")
            clsMsg.AlertMessage("請選擇專案名稱！", this.Page);
        else
        {
            Query1(ddlName.Text, ddlCustomer.Text);
            //if (ddlNPI.Text == "")
            //    clsMsg.AlertMessage("請選擇NPI！", this.Page);
            //else
            //    Query1(ddlName.Text, ddlCustomer.Text, ddlNPI.Text);
        }
    }

    private void Query1(string strName, string strCustomer)
    {
        DataTable dt_Info;
        DateTime d_Time;
        DataTable dt1;
        DataRow dr;


        Session["DBS"] = strName;
        Session["DBSC"] = strCustomer;
        //Session["DBSD"] = strDepartment;
        DataTable dt_new = new DataTable("dt_new");

        //DataColumn column1 = new DataColumn("Kind");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "Kind";
        //column1.DefaultValue = "0";
        //dt_new.Columns.Add(column1);

        DataColumn column1 = new DataColumn("Function");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Function";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Item");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Item";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);


        DataColumn column3 = new DataColumn("Result");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Result";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);


        DataColumn column4 = new DataColumn("NPI");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "NPI";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        string strNPI;
        dr = dt_new.NewRow();
        for (int intNPI = 0; intNPI < 4; intNPI++)
        {
            if (intNPI == 0)
                strNPI = "ES";
            else if (intNPI == 1)
                strNPI = "EV";
            else if (intNPI == 2)
                strNPI = "DV";
            else 
                strNPI = "PV";
            DataTable dt = clsData.UploadDQADashBoard("0", strName, strNPI, "", "", strCustomer);
            string strProjectID;

            if (dt.Rows.Count > 0)
            {
                strProjectID = "(";
                for (int intI = 0; intI < dt.Rows.Count; intI++)
                {
                    if (intI == dt.Rows.Count - 1)
                        strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "')";
                    else
                        strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "' or ";

                }

                string strID, strHW;

                DataTable dt_Function = clsData.UploadDQADashBoard("4", strProjectID, "", "", "", "");


                for (int intJ = 0; intJ < dt_Function.Rows.Count; intJ++)
                {

                    
                    dr["Function"] = dt_Function.Rows[intJ]["Kind"].ToString();
                    dr["Item"] = dt_Function.Rows[intJ]["Name"].ToString();
                    dr["Result"] = dt_Function.Rows[intJ]["Result"].ToString();
                    dr["NPI"] = strNPI;

                    dt_new.Rows.Add(dr);
                }

                //gvwMain.DataSource = dt_new;
                //gvwMain.DataBind();
            }
            else
            {
                //gvwMain.DataSource = dt_new;
                //gvwMain.DataBind();
            }
        }
        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
    }

    //private void Query(string strName, string strCustomer, string strNPI)
    //{
    //    DataTable dt_Info;
    //    DateTime d_Time;
    //    DataTable dt1;
    //    DataRow dr;


    //    Session["DBS"] = strName;
    //    Session["DBSC"] = strCustomer;
    //    //Session["DBSD"] = strDepartment;
    //    DataTable dt_new = new DataTable("dt_new");

    //    DataColumn column1 = new DataColumn("Function");
    //    column1.DataType = System.Type.GetType("System.String");
    //    column1.AllowDBNull = true;
    //    column1.Caption = "Function";
    //    column1.DefaultValue = "0";
    //    dt_new.Columns.Add(column1);

    //    DataColumn column2 = new DataColumn("Item");
    //    column2.DataType = System.Type.GetType("System.String");
    //    column2.AllowDBNull = true;
    //    column2.Caption = "Item";
    //    column2.DefaultValue = "0";
    //    dt_new.Columns.Add(column2);

    //    DataColumn column3 = new DataColumn("HW");
    //    column3.DataType = System.Type.GetType("System.String");
    //    column3.AllowDBNull = true;
    //    column3.Caption = "HW";
    //    column3.DefaultValue = "0";
    //    dt_new.Columns.Add(column3);

    //    DataColumn column4 = new DataColumn("SW");
    //    column4.DataType = System.Type.GetType("System.String");
    //    column4.AllowDBNull = true;
    //    column4.Caption = "SW";
    //    column4.DefaultValue = "0";
    //    dt_new.Columns.Add(column4);

    //    DataColumn column5 = new DataColumn("Date");
    //    column5.DataType = System.Type.GetType("System.String");
    //    column5.AllowDBNull = true;
    //    column5.Caption = "Date";
    //    column5.DefaultValue = "0";
    //    dt_new.Columns.Add(column5);

    //    DataColumn column6 = new DataColumn("NPI");
    //    column6.DataType = System.Type.GetType("System.String");
    //    column6.AllowDBNull = true;
    //    column6.Caption = "NPI";
    //    column6.DefaultValue = "0";
    //    dt_new.Columns.Add(column6);


    //    DataTable dt = clsData.UploadDQADashBoard("0", strName, "", "", strCustomer);
    //    string strProjectID;

    //    if (dt.Rows.Count > 0)
    //    {
    //        strProjectID = "(";
    //        for (int intI = 0; intI < dt.Rows.Count; intI++)
    //        {
    //            if (intI == dt.Rows.Count - 1)
    //                strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "')";
    //            else
    //                strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "' or ";

    //        }

    //        string strID, strHW, strSW, strDate, strDate1;
    //        //string strESID, strESHW, strESSW, strESDate;
    //        //string strEVID, strEVHW, strEVSW, strEVDate;
    //        //string strPVID, strPVHW, strPVSW, strPVDate;


    //        dt_Info = clsData.UploadDQADashBoard("2", strName, "", "", strCustomer);
    //        if (dt_Info.Rows.Count == 0)
    //        {
    //            strID = "";
    //            strHW = "";
    //            strSW = "";
    //            strDate = "";
    //        }
    //        else
    //        {
    //            strID = dt_Info.Rows[0]["ID"].ToString();
    //            strHW = dt_Info.Rows[0]["PCB_Version"].ToString();
    //            strSW = dt_Info.Rows[0]["FW_Version"].ToString();
    //            //strDVDate = dt_Info.Rows[0]["End_Date"].ToString();

    //            d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
    //            strDate1 = d_Time.ToString("yyyy/MM/dd");
    //            if (strDate1 == "1900/01/01")
    //                strDate = "";
    //            else
    //                strDate = strDate1;
    //        }

    //        DataTable dt_Function = clsData.UploadDQADashBoard("1", strProjectID, "", "", "");

    //        for (int intJ = 0; intJ < dt_Function.Rows.Count; intJ++)
    //        {

    //            dr = dt_new.NewRow();
    //            dr["Function"] = dt_Function.Rows[intJ]["Kind"].ToString();
    //            dr["Item"] = dt_Function.Rows[intJ]["Name"].ToString();

    //            //DataTable dt1 = clsData.UploadDashBoardSummaryList("2", txtSearch.Text, "DV", dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString());

    //            //dr["DV_HW"] 


    //            dt1 = clsData.UploadDQADashBoard("3", strID, dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString(), strCustomer);

    //            if (dt1.Rows.Count == 0)
    //            {
    //                dr["HW"] = "N/A";
    //                dr["SW"] = "N/A";
    //                dr["Date"] = "N/A";
    //                dr["NPI"] = strNPI;
    //            }
    //            else
    //            {
    //                dr["HW"] = dt1.Rows[0]["Result"].ToString();
    //                dr["SW"] = dt1.Rows[0]["Result"].ToString();
    //                dr["Date"] = dt1.Rows[0]["Result"].ToString();
    //                dr["NPI"] = strNPI;
    //            }


    //            dt_new.Rows.Add(dr);
    //        }

    //        gvwMain.DataSource = dt_new;
    //        gvwMain.DataBind();
    //    }
    //    else
    //    {
    //        gvwMain.DataSource = dt_new;
    //        gvwMain.DataBind();
    //    }

    //}
}
