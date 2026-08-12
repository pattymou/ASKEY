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


public partial class WebForm_SearchDashBoardSummary : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadCustomer(this.ddlCustomer);
            //loadDepartment(this.ddlDepartment);
            btnExcel1.Visible = false;
        }
    }

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
    protected void loadProjectName(DropDownList DDL,string strCustomer)
    {
        clsDropDownList.ddlProjectName(DDL, strCustomer);
    }
    #endregion

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProjectName(this.ddlName, ddlCustomer.Text);
    }

    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    loadProjectName(this.ddlName, ddlCustomer.Text,ddlDepartment.Text);
    //}

    //protected void gvwCheck_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    string strName;

    //    if (e.CommandName == "AddToCart")
    //    {
    //        GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

    //        strName = ((Label)row.Cells[0].FindControl("lblName")).Text.Trim();
    //        //Session["DBSN"] = strName;
    //        lblName.Text = strName;
    //        Query(strName);
    //        //Response.Write("<script>window.open('DashBoardSShow.aspx?ID=" + strName + "');</script>");

    //    }
    //}

    //#region gvwCheck_PageIndexChanging
    //protected void gvwCheck_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    ((GridView)sender).PageIndex = e.NewPageIndex;
    //    ((GridView)sender).EditIndex = -1;

    //    DataTable dt = clsData.UploadDashBoardSummaryName(txtSearch.Text.Trim());
    //    gvwCheck.DataSource = dt;
    //    gvwCheck.DataBind();
    //}
    //#endregion

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {

        for (int intI = 0; intI < 1; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].ColumnSpan = 3;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;

            e.Row.Cells[5].ColumnSpan = 3;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;

            e.Row.Cells[8].ColumnSpan = 3;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;

            e.Row.Cells[11].ColumnSpan = 3;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;

            //e.Row.Cells[0].Width = 150;
            //e.Row.Cells[1].Width = 200;

            //e.Row.Cells[2].Width = 300;
            //e.Row.Cells[3].Width = 100;
            //e.Row.Cells[4].Width = 100;

            //e.Row.Cells[5].Width = 300;
            //e.Row.Cells[6].Width = 100;
            //e.Row.Cells[7].Width = 100;

            //e.Row.Cells[8].Width = 300;
            //e.Row.Cells[9].Width = 100;
            //e.Row.Cells[10].Width = 100;

            //e.Row.Cells[11].Width = 300;
            //e.Row.Cells[12].Width = 100;
            //e.Row.Cells[13].Width = 100;

            //e.Row.Cells[0].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
            //gvwMain.Columns[2].ItemStyle.Width = 500;

            //e.Row.Cells[0].CssClass = "locked";

        }
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        DataTable dt_Info;
        DateTime d_Time;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            DataTable dt = clsData.UploadDashBoardSummaryList("0", ddlName.Text, "", "", "",ddlCustomer.Text);
            string strProjectID;

            strProjectID = "(";
            for (int intI = 0; intI < dt.Rows.Count; intI++)
            {
                if (intI == dt.Rows.Count - 1)
                    strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "')";
                else
                    strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "' or ";

            }

            string strDVID, strDVHW, strDVSW, strDVDate, strDate1;
            string strESID, strESHW, strESSW, strESDate;
            string strEVID, strEVHW, strEVSW, strEVDate;
            string strPVID, strPVHW, strPVSW, strPVDate;


            dt_Info = clsData.UploadDashBoardSummaryList("2", ddlName.Text, "DV", "", "", ddlCustomer.Text);
            if (dt_Info.Rows.Count == 0)
            {
                strDVID = "";
                strDVHW = "";
                strDVSW = "";
                strDVDate = "";
            }
            else
            {
                strDVID = dt_Info.Rows[0]["ID"].ToString();
                strDVHW = dt_Info.Rows[0]["PCB_Version"].ToString();
                strDVSW = dt_Info.Rows[0]["FW_Version"].ToString();
                //strDVDate = dt_Info.Rows[0]["End_Date"].ToString();

                d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
                strDate1 = d_Time.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strDVDate = "";
                else
                    strDVDate = strDate1;
            }

            dt_Info = clsData.UploadDashBoardSummaryList("2", ddlName.Text, "ES", "", "", ddlCustomer.Text);
            if (dt_Info.Rows.Count == 0)
            {
                strESID = "";
                strESHW = "";
                strESSW = "";
                strESDate = "";
            }
            else
            {
                strESID = dt_Info.Rows[0]["ID"].ToString();
                strESHW = dt_Info.Rows[0]["PCB_Version"].ToString();
                strESSW = dt_Info.Rows[0]["FW_Version"].ToString();
                //strESDate = dt_Info.Rows[0]["End_Date"].ToString();
                d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
                strDate1 = d_Time.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strESDate = "";
                else
                    strESDate = strDate1;
            }

            dt_Info = clsData.UploadDashBoardSummaryList("2", ddlName.Text, "EV", "", "", ddlCustomer.Text);
            if (dt_Info.Rows.Count == 0)
            {
                strEVID = "";
                strEVHW = "";
                strEVSW = "";
                strEVDate = "";
            }
            else
            {
                strEVID = dt_Info.Rows[0]["ID"].ToString();
                strEVHW = dt_Info.Rows[0]["PCB_Version"].ToString();
                strEVSW = dt_Info.Rows[0]["FW_Version"].ToString();
                //strEVDate = dt_Info.Rows[0]["End_Date"].ToString();
                d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
                strDate1 = d_Time.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strEVDate = "";
                else
                    strEVDate = strDate1;
            }

            dt_Info = clsData.UploadDashBoardSummaryList("2", ddlName.Text, "PV", "", "", ddlCustomer.Text);
            if (dt_Info.Rows.Count == 0)
            {
                strPVID = "";
                strPVHW = "";
                strPVSW = "";
                strPVDate = "";
            }
            else
            {
                strPVID = dt_Info.Rows[0]["ID"].ToString();
                strPVHW = dt_Info.Rows[0]["PCB_Version"].ToString();
                strPVSW = dt_Info.Rows[0]["FW_Version"].ToString();
                //strPVDate = dt_Info.Rows[0]["End_Date"].ToString();
                d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
                strDate1 = d_Time.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strPVDate = "";
                else
                    strPVDate = strDate1;
            }

            //將原有的表頭移除
            TableCellCollection oldCell = e.Row.Cells;
            oldCell.Clear();

            #region 第一列
            //多重表頭的第一列
            GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //第一欄
            TableCell tc = new TableCell();
            tc.Text = " ";
            //tc.BackColor = System.Drawing.Color.AliceBlue; //背景色彩
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.VerticalAlign = VerticalAlign.Middle;
            tc.Width = 150;
            tc.RowSpan = 4; //所跨的row數
            tc.ColumnSpan = 1; //所跨的column數
            gvRow.Cells.Add(tc); //新增

            //第二欄
            tc = new TableCell();
            tc.Text = "";
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 200;
            tc.RowSpan = 4;
            tc.ColumnSpan = 1;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第三欄
            tc = new TableCell();
            tc.Text = strDVID;
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第四欄
            tc = new TableCell();
            tc.Text = strESID;
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第五欄
            tc = new TableCell();
            tc.Text = strEVID;
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //第六欄
            tc = new TableCell();
            tc.Text = strPVID;
            //tc.BackColor = System.Drawing.Color.AliceBlue;
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            tc.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc);

            //新增至GridView
            gvwMain.Controls[0].Controls.Add(gvRow);

            #endregion

            #region 第二列

            //多重表頭的第二列
            gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "DV";
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "ES";
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "EV";
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "PV";
            tc.Width = 150;
            tc.RowSpan = 1;
            tc.ColumnSpan = 3;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            gvwMain.Controls[0].Controls.Add(gvRow);

            //多重表頭的第三列
            gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "H/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "S/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "完成日";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "H/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "S/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "完成日";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "H/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "S/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "完成日";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "H/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "S/W";
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = "完成日";
            //tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            gvwMain.Controls[0].Controls.Add(gvRow);

            //多重表頭的第四列
            gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strDVHW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strDVSW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strDVDate;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strESHW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strESSW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strESDate;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strEVHW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strEVSW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strEVDate;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strPVHW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.FromName("red");
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strPVSW;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            tc = new TableCell();
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Text = strPVDate;
            tc.Wrap = true;
            tc.Width = 50;
            //tc.BackColor = System.Drawing.Color.BlueViolet;
            gvRow.Cells.Add(tc);

            gvwMain.Controls[0].Controls.Add(gvRow);

            #endregion


            //GridViewRow gvHeaderRow = e.Row;
            //GridViewRow gvHeaderRowCopy = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //this.gvwMain.Controls[0].Controls.AddAt(0, gvHeaderRowCopy);

            //int headerCellCount = gvHeaderRow.Cells.Count;
            //int cellIndex = 0;

            //for (int i = 0; i < headerCellCount; i++)
            //{
            //    if (i == 0 || i == 1 || i == 4 || i == 5)
            //    {
            //        cellIndex++;
            //    }
            //    else
            //    {
            //        TableCell tcHeader = gvHeaderRow.Cells[cellIndex];
            //        tcHeader.RowSpan = 2;
            //        gvHeaderRowCopy.Cells.Add(tcHeader);
            //    }
            //}

            //TableCell tcMergeProduct = new TableCell();
            //tcMergeProduct.Text = "Product";
            //tcMergeProduct.ColumnSpan = 2;
            //gvHeaderRowCopy.Cells.AddAt(0, tcMergeProduct); 
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (txtSearch.Text.Trim() == "")
        //{
        //    clsMsg.AlertMessage("請輸入專案名稱！", this.Page);
        //}
        //else
        //{
        //    DataTable dt = clsData.UploadDashBoardSummaryName(txtSearch.Text.Trim());
        //    gvwCheck.DataSource = dt;
        //    gvwCheck.DataBind();
        //}
        btnExcel1.Visible = true;
        if (ddlName.Text =="")
            clsMsg.AlertMessage("請選擇專案名稱！", this.Page);
        else
            Query(ddlName.Text,ddlCustomer.Text);
    }

    private void Query(string strName,string strCustomer)
    {
        DataTable dt_Info;
        DateTime d_Time;
        DataTable dt1;
        DataRow dr;


        Session["DBS"] = strName;
        Session["DBSC"] = strCustomer;
        //Session["DBSD"] = strDepartment;
        DataTable dt_new = new DataTable("dt_new");

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

        DataColumn column3 = new DataColumn("DV_HW");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "DV_HW";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("DV_SW");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "DV_SW";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("DV_Date");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "DV_Date";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("ES_HW");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "ES_HW";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("ES_SW");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "ES_SW";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("ES_Date");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "ES_Date";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("EV_HW");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "EV_HW";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("EV_SW");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "EV_SW";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("EV_Date");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "EV_Date";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("PV_HW");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "PV_HW";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("PV_SW");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "PV_SW";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("PV_Date");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "PV_Date";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);



        DataTable dt = clsData.UploadDashBoardSummaryList("0", strName, "", "", "", strCustomer);
        string strProjectID;

        strProjectID = "(";
        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            if (intI == dt.Rows.Count - 1)
                strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "')";
            else
                strProjectID = strProjectID + "Project_ID = '" + dt.Rows[intI]["ID"].ToString() + "' or ";

        }

        string strDVID, strDVHW, strDVSW, strDVDate, strDate1;
        string strESID, strESHW, strESSW, strESDate;
        string strEVID, strEVHW, strEVSW, strEVDate;
        string strPVID, strPVHW, strPVSW, strPVDate;


        dt_Info = clsData.UploadDashBoardSummaryList("2", strName, "DV", "", "", strCustomer);
        if (dt_Info.Rows.Count == 0)
        {
            strDVID = "";
            strDVHW = "";
            strDVSW = "";
            strDVDate = "";
        }
        else
        {
            strDVID = dt_Info.Rows[0]["ID"].ToString();
            strDVHW = dt_Info.Rows[0]["PCB_Version"].ToString();
            strDVSW = dt_Info.Rows[0]["FW_Version"].ToString();
            //strDVDate = dt_Info.Rows[0]["End_Date"].ToString();

            d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
            strDate1 = d_Time.ToString("yyyy/MM/dd");
            if (strDate1 == "1900/01/01")
                strDVDate = "";
            else
                strDVDate = strDate1;
        }

        dt_Info = clsData.UploadDashBoardSummaryList("2", strName, "ES", "", "", strCustomer);
        if (dt_Info.Rows.Count == 0)
        {
            strESID = "";
            strESHW = "";
            strESSW = "";
            strESDate = "";
        }
        else
        {
            strESID = dt_Info.Rows[0]["ID"].ToString();
            strESHW = dt_Info.Rows[0]["PCB_Version"].ToString();
            strESSW = dt_Info.Rows[0]["FW_Version"].ToString();
            //strESDate = dt_Info.Rows[0]["End_Date"].ToString();
            d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
            strDate1 = d_Time.ToString("yyyy/MM/dd");
            if (strDate1 == "1900/01/01")
                strESDate = "";
            else
                strESDate = strDate1;
        }

        dt_Info = clsData.UploadDashBoardSummaryList("2", strName, "EV", "", "", strCustomer);
        if (dt_Info.Rows.Count == 0)
        {
            strEVID = "";
            strEVHW = "";
            strEVSW = "";
            strEVDate = "";
        }
        else
        {
            strEVID = dt_Info.Rows[0]["ID"].ToString();
            strEVHW = dt_Info.Rows[0]["PCB_Version"].ToString();
            strEVSW = dt_Info.Rows[0]["FW_Version"].ToString();
            //strEVDate = dt_Info.Rows[0]["End_Date"].ToString();
            d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
            strDate1 = d_Time.ToString("yyyy/MM/dd");
            if (strDate1 == "1900/01/01")
                strEVDate = "";
            else
                strEVDate = strDate1;
        }

        dt_Info = clsData.UploadDashBoardSummaryList("2", strName, "PV", "", "", strCustomer);
        if (dt_Info.Rows.Count == 0)
        {
            strPVID = "";
            strPVHW = "";
            strPVSW = "";
            strPVDate = "";
        }
        else
        {
            strPVID = dt_Info.Rows[0]["ID"].ToString();
            strPVHW = dt_Info.Rows[0]["PCB_Version"].ToString();
            strPVSW = dt_Info.Rows[0]["FW_Version"].ToString();
            //strPVDate = dt_Info.Rows[0]["End_Date"].ToString();
            d_Time = Convert.ToDateTime(dt_Info.Rows[0]["End_Date"].ToString());
            strDate1 = d_Time.ToString("yyyy/MM/dd");
            if (strDate1 == "1900/01/01")
                strPVDate = "";
            else
                strPVDate = strDate1;
        }

        DataTable dt_Function = clsData.UploadDashBoardSummaryList("1", strProjectID, "", "", "","");

        //DataTable dt1 = clsData.UploadDashBoardSummaryList("1", strProjectID);

        for (int intJ = 0; intJ < dt_Function.Rows.Count; intJ++)
        {

            dr = dt_new.NewRow();
            dr["Function"] = dt_Function.Rows[intJ]["Kind"].ToString();
            dr["Item"] = dt_Function.Rows[intJ]["Name"].ToString();

            //DataTable dt1 = clsData.UploadDashBoardSummaryList("2", txtSearch.Text, "DV", dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString());

            //dr["DV_HW"] 


            dt1 = clsData.UploadDashBoardSummaryList("3", strDVID, "DV", dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString(), strCustomer);

            if (dt1.Rows.Count == 0)
            {
                dr["DV_HW"] = "N/A";
                dr["DV_SW"] = "N/A";
                dr["DV_Date"] = "N/A";
            }
            else
            {
                dr["DV_HW"] = dt1.Rows[0]["Result"].ToString();
                dr["DV_SW"] = dt1.Rows[0]["Result"].ToString();
                dr["DV_Date"] = dt1.Rows[0]["Result"].ToString();
            }

            dt1 = clsData.UploadDashBoardSummaryList("3", strESID, "ES", dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString(), strCustomer);

            if (dt1.Rows.Count == 0)
            {
                dr["ES_HW"] = "N/A";
                dr["ES_SW"] = "N/A";
                dr["ES_Date"] = "N/A";
            }
            else
            {
                dr["ES_HW"] = dt1.Rows[0]["Result"].ToString();
                dr["ES_SW"] = dt1.Rows[0]["Result"].ToString();
                dr["ES_Date"] = dt1.Rows[0]["Result"].ToString();
            }

            dt1 = clsData.UploadDashBoardSummaryList("3", strEVID, "EV", dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString(), strCustomer);

            if (dt1.Rows.Count == 0)
            {
                dr["EV_HW"] = "N/A";
                dr["EV_SW"] = "N/A";
                dr["EV_Date"] = "N/A";
            }
            else
            {

                dr["EV_HW"] = dt1.Rows[0]["Result"].ToString();
                dr["EV_SW"] = dt1.Rows[0]["Result"].ToString();
                dr["EV_Date"] = dt1.Rows[0]["Result"].ToString();
            }

            dt1 = clsData.UploadDashBoardSummaryList("3", strPVID, "PV", dt_Function.Rows[intJ]["Kind"].ToString(), dt_Function.Rows[intJ]["Name"].ToString(), strCustomer);

            if (dt1.Rows.Count == 0)
            {
                dr["PV_HW"] = "N/A";
                dr["PV_SW"] = "N/A";
                dr["PV_Date"] = "N/A";
            }
            else
            {
                dr["PV_HW"] = dt1.Rows[0]["Result"].ToString();
                dr["PV_SW"] = dt1.Rows[0]["Result"].ToString();
                dr["PV_Date"] = dt1.Rows[0]["Result"].ToString();
            }

            dt_new.Rows.Add(dr);
        }

        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
        
    }

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        //string strName = "DashBoard ";
        //export_excel(strName, 1);
        Query(ddlName.Text, ddlCustomer.Text);
        GridViewExportUtil.Export("DashBoard.xls", gvwMain);
        //export_excel1(strName, 1);
    }

    private void export_excel(string filename, int t_mode)
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
        //gvwMain.DataSource = dt_new;
        //gvwMain.DataBind();

        gvwMain.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
        //gvwMain.AllowPaging = true;
        //gvwMain.DataSource = dt_new;
        //gvwMain.DataBind();

    }

    private void export_excel1(string filename, int t_mode)
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
        //gvwMain.DataSource = dt_new;
        //gvwMain.DataBind();

        Table1.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
        //gvwMain.AllowPaging = true;
        //gvwMain.DataSource = dt_new;
        //gvwMain.DataBind();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }

    
    


}
