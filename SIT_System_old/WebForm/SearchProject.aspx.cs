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

public partial class WebForm_SearchProject : System.Web.UI.Page
{
    public static DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadTeam(this.ddlTeam);
            loadProjectKind(this.ddlKind);
        }
    }

    //protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    loadTeam(this.ddlTeam);
    //}

    //protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    loadTeam(this.ddlKind);
    //}

    #region loadProjectKind
    protected void loadProjectKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 6, "1");
    }
    #endregion

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 4, "1");
    }
    #endregion 

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //getTestPlan();
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getProject();
    }

    private void getProject()
    {
        dt = clsData.UploadProjectIDQuery1(ddlTeam.Text,ddlKind.Text,txtProject.Text);

        gvwMain.DataSource = dt;
        gvwMain.DataBind();
    }

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        export_excel("Report", 1);
    }

    private void export_excel1()
    {
        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    dt_new1.TableName = "Summary";
        //    wb.Worksheets.Add(dt_new1);
        //    dt.TableName = "TestCase";
        //    wb.Worksheets.Add(dt);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=TestPlan.xls");
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(memoryStream);
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.WriteTo(Response.OutputStream);
        //        memoryStream.Close();
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
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
        gvwMain.AllowPaging = false;
        gvwMain.DataSource = dt;
        gvwMain.DataBind();
        gvwMain.Columns[3].Visible = false;
        gvwMain.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
        gvwMain.AllowPaging = true;
        gvwMain.DataSource = dt;
        gvwMain.DataBind();
        gvwMain.Columns[3].Visible =true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strID, strDate1,strDate;
        string strSheet;
        string strCol, strCol1;
         
        //DataTable dt_Date;
        DateTime dt_Date;
        var workbook = new XLWorkbook();
        for (int ii = 0; ii < this.gvwMain.Rows.Count; ii++)
        {
            if (((CheckBox)gvwMain.Rows[ii].FindControl("CheckBox2")).Checked)
            {
                strID = ((Label)this.gvwMain.Rows[ii].Cells[3].FindControl("lblGVSeq")).Text;
                DataTable dt_p = clsData.UploadProjectQuery(strID, "Project");

                //var workbook = new XLWorkbook();
                strSheet = dt_p.Rows[0]["Accepted_Team"].ToString() + dt_p.Rows[0]["Name"].ToString();
                var ws = workbook.Worksheets.Add(strSheet);
                //ws.Cell(1, 1).Value = "Test";
                //ws.Cell(1, 2).Value = "Test1";
                //ws.Cell(2, 1).Value = "Test2";
                ws.Cell(1, 1).Value = "申請人";
                ws.Cell(1, 2).Value = dt_p.Rows[0]["A_Ext"].ToString();
                ws.Cell(1, 3).Value = "部門";
                ws.Cell(1, 4).Value = dt_p.Rows[0]["A_Department"].ToString();

                ws.Cell(2, 1).Value = "分機";
                ws.Cell(2, 2).Value = dt_p.Rows[0]["A_Ext"].ToString();
                ws.Cell(2, 3).Value = "Mail";
                ws.Cell(2, 4).Value = dt_p.Rows[0]["A_mail"].ToString();

                ws.Cell(3, 1).Value = "客戶";
                ws.Cell(3, 2).Value = dt_p.Rows[0]["Customer"].ToString();
                ws.Cell(3, 3).Value = "PM Sales";
                ws.Cell(3, 4).Value = dt_p.Rows[0]["PM"].ToString();

                ws.Cell(4, 1).Value = "S/W Engineer";
                ws.Cell(4, 2).Value = dt_p.Rows[0]["SW_Engineer"].ToString();
                ws.Cell(4, 3).Value = "H/W Engineer";
                ws.Cell(4, 4).Value = dt_p.Rows[0]["HW_Engineer"].ToString();

                ws.Cell(5, 1).Value = "Mechanical Engineer";
                ws.Cell(5, 2).Value = dt_p.Rows[0]["Mechanical_Engineer"].ToString();
                ws.Cell(5, 3).Value = "DSP Model";
                ws.Cell(5, 4).Value = dt_p.Rows[0]["DSP_Model"].ToString();

                ws.Cell(6, 1).Value = "F/W Version";
                ws.Cell(6, 2).Value = dt_p.Rows[0]["FW_Version"].ToString();
                ws.Cell(6, 3).Value = "Wireless Drive";
                ws.Cell(6, 4).Value = dt_p.Rows[0]["WirelessDrive"].ToString();

                ws.Cell(7, 1).Value = "Customer's Product Name";
                ws.Cell(7, 2).Value = dt_p.Rows[0]["Customer_Product_Name"].ToString();
                ws.Cell(7, 3).Value = "NPI";
                ws.Cell(7, 4).Value = dt_p.Rows[0]["NPI"].ToString();

                ws.Cell(8, 1).Value = "H/W Version";
                ws.Cell(8, 2).Value = dt_p.Rows[0]["PCB_Version"].ToString();
                ws.Cell(8, 3).Value = "Chipset";
                ws.Cell(8, 4).Value = dt_p.Rows[0]["Chipset"].ToString();

                ws.Cell(9, 1).Value = "Sample MAC Address";
                ws.Cell(9, 2).Value = dt_p.Rows[0]["Sample_Mac_address"].ToString();
                ws.Cell(9, 3).Value = "Utility Version";
                ws.Cell(9, 4).Value = dt_p.Rows[0]["Utility_Version"].ToString();


                dt_Date = Convert.ToDateTime(dt_p.Rows[0]["Start_Date"].ToString());
                strDate1 = dt_Date.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strDate = "";
                else
                    strDate = strDate1;
                ws.Cell(10, 1).Value = "開始日期";
                ws.Cell(10, 2).Value = strDate;

                dt_Date = Convert.ToDateTime(dt_p.Rows[0]["End_Date"].ToString());
                strDate1 = dt_Date.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strDate = "";
                else
                    strDate = strDate1;
                ws.Cell(10, 3).Value = "預計完成日";
                ws.Cell(10, 4).Value = strDate;

                dt_Date = Convert.ToDateTime(dt_p.Rows[0]["Sample_Ready_Date"].ToString());
                strDate1 = dt_Date.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    strDate = "";
                else
                    strDate = strDate1;
                ws.Cell(11, 1).Value = "預計Sample Ready日期";
                ws.Cell(11, 2).Value = strDate;

                ws.Cell(12, 1).Value = "指派工程師";
                ws.Cell(12, 2).Value = dt_p.Rows[0]["Assign"].ToString();
                ws.Cell(12, 3).Value = "進度";
                ws.Cell(12, 4).Value = dt_p.Rows[0]["Progress"].ToString();

                ws.Cell(13, 1).Value = "備註";
                ws.Cell("B13").Value = dt_p.Rows[0]["Explain"].ToString();  //合併儲存格

                ws.Cell("B13").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Cell("B13").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Range("B13:D13").Merge();

                ws.Rows(13, 2).AdjustToContents();

                var row1 = ws.Row(13);
                row1.Height = 200;

                //ws.Columns("A:F").Width = 26;



                int intCount = 15;

                DataTable dt = clsData.getProjectCase(strID, "1", "");
                DataTable dt1, dt2;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ws.Cell(intCount, 1).Value = dt.Rows[i]["Kind"].ToString().Trim();
                    ws.Cell(intCount, 1).Style.Font.Bold = true;
                    ws.Cell(intCount, 1).Style.Font.SetFontSize(16);
                    dt1 = clsData.getProjectItem(strID, dt.Rows[i]["Kind"].ToString().Trim(), "", "1", "Open");
                    intCount++;
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        ws.Cell(intCount, 1).Value = dt1.Rows[j]["Name"].ToString().Trim();
                        ws.Cell(intCount, 1).Style.Font.Bold = true;
                        intCount++;
                        //dt2 = clsData.UploadProjectTask(strID, dt.Rows[i]["Kind"].ToString().Trim(), dt1.Rows[j]["Name"].ToString().Trim(), dt1.Rows[j]["ID"].ToString().Trim());
                        dt2 = clsData.UploadProjectTask(strID, dt1.Rows[j]["ID"].ToString().Trim());
                        ws.Cell(intCount, 2).Value = "子任務名稱";
                        ws.Cell(intCount, 3).Value = dt1.Rows[j]["Name"].ToString().Trim();
                        ws.Cell(intCount, 4).Value = "子任務ID";
                        ws.Cell(intCount, 5).Value = dt1.Rows[j]["ID"].ToString().Trim();
                        intCount++;

                        ws.Cell(intCount, 2).Value = "指派工程師";
                        ws.Cell(intCount, 3).Value = dt2.Rows[0]["assign"].ToString();
                        ws.Cell(intCount, 4).Value = "狀態";
                        ws.Cell(intCount, 5).Value = dt2.Rows[0]["Status"].ToString();
                        intCount++;

                        string strStartDate = "", strEndDate = "";
                        //string strDate;
                        //DateTime dTime;

                        dt_Date = Convert.ToDateTime(dt2.Rows[0]["start_date1"].ToString());
                        strDate = dt_Date.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strStartDate = strDate;

                        dt_Date = Convert.ToDateTime(dt2.Rows[0]["end_date1"].ToString());
                        strDate = dt_Date.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strEndDate = strDate;

                        ws.Cell(intCount, 2).Value = "開始日期";
                        ws.Cell(intCount, 3).Value = strStartDate;
                        ws.Cell(intCount, 4).Value = "預計完成日";
                        ws.Cell(intCount, 5).Value = strEndDate;
                        intCount++;

                        ws.Cell(intCount, 2).Value = "結果判定";
                        ws.Cell(intCount, 3).Value = dt2.Rows[0]["result"].ToString();
                        ws.Cell(intCount, 4).Value = "進度";
                        ws.Cell(intCount, 5).Value = dt2.Rows[0]["Progress"].ToString();
                        intCount++;

                        ws.Cell(intCount, 2).Value = "備註";
                        ws.Cell(intCount, 3).Value = dt2.Rows[0]["explain_case"].ToString();
                        //intCount++;


                        strCol = "C" + intCount.ToString();
                        strCol1 = "C" + intCount.ToString() + ":" + "F" + intCount.ToString();
                        ws.Cell(strCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell(strCol).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                        ws.Range(strCol1).Merge();

                        strCol = "B" + intCount.ToString();
                        strCol1 = "B" + intCount.ToString() + ":" + "B" + intCount.ToString();
                        ws.Cell(strCol).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Cell(strCol).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;



                        row1 = ws.Row(intCount);
                        row1.Height = 200;

                        //ws.Rows(intCount, 4).AdjustToContents();
                        intCount++;
                    }

                    intCount++;
                }
                ws.Column(1).AdjustToContents();
                ws.Column(2).AdjustToContents();
                ws.Column(3).AdjustToContents();
                ws.Column(4).AdjustToContents();
                ws.Column(5).AdjustToContents();
                ws.Column(6).AdjustToContents();



                //Response.Clear();
                //Response.Buffer = true;
                //Response.Charset = "";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;filename=test.xls");
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    workbook.SaveAs(memoryStream);
                //    byte[] bytes = memoryStream.ToArray();
                //    memoryStream.WriteTo(Response.OutputStream);
                //    memoryStream.Close();
                //    Response.Flush();
                //    Response.End();
                //}

            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=TestCase.xls");
        using (MemoryStream memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.WriteTo(Response.OutputStream);
            memoryStream.Close();
            Response.Flush();
            Response.End();
        }

    }

}
