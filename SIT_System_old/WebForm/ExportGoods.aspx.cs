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
using System.Linq;
using NPOI;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;


public partial class WebForm_ExportGoods : System.Web.UI.Page
{
    public static DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadKind(this.ddlKind);

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion 

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", ddlKind.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        export_excel("Report", 1);
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
        gvwMain.Columns[4].Visible = false;
        gvwMain.RenderControl(hw);
        Response.Write(style);
        Response.Write(sw.ToString().Replace("<div>", "").Replace("</div>", ""));
        Response.End();
        gvwMain.AllowPaging = true;
        gvwMain.DataSource = dt;
        gvwMain.DataBind();
        gvwMain.Columns[4].Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strID, strDate1, strDate;
        string strSheet;
        string strCol, strCol1;

        DateTime dt_Date;

        var wb = new HSSFWorkbook();

        for (int ii = 0; ii < this.gvwMain.Rows.Count; ii++)
        {
            if (((CheckBox)gvwMain.Rows[ii].FindControl("CheckBox2")).Checked)
            {
                strID = ((Label)this.gvwMain.Rows[ii].Cells[3].FindControl("lblGVSeq")).Text;
                DataTable dt_p = clsData.UploadGoodsQuery(strID, "1", "");

                string strName = dt_p.Rows[0]["Name_CH"].ToString();
                strName = strName.Replace("/", "_");
                strName = strName.Replace("*", "x");

                strSheet = dt_p.Rows[0]["Part_No"].ToString() + strName;
                var ws = wb.CreateSheet(strSheet);
                ws.SetColumnWidth(3, 10000);

                ws.CreateRow(0);
                ws.GetRow(0).CreateCell(0).SetCellValue("料號");
                ws.GetRow(0).CreateCell(1).SetCellValue(dt_p.Rows[0]["Part_No"].ToString());
                ws.GetRow(0).CreateCell(2).SetCellValue("貨品名稱");
                ws.GetRow(0).CreateCell(3).SetCellValue(dt_p.Rows[0]["Name_En"].ToString() + "-" + dt_p.Rows[0]["Name_CH"].ToString());

                ws.CreateRow(1);
                ws.GetRow(1).CreateCell(0).SetCellValue("廠商");
                ws.GetRow(1).CreateCell(1).SetCellValue(dt_p.Rows[0]["MF_EN"].ToString() + "-" + dt_p.Rows[0]["MF_CH"].ToString());
                ws.GetRow(1).CreateCell(2).SetCellValue("有效期限天數");
                ws.GetRow(1).CreateCell(3).SetCellValue(dt_p.Rows[0]["Check_Date"].ToString());

                ws.CreateRow(2);
                ws.GetRow(2).CreateCell(0).SetCellValue("庫存數量");
                ws.GetRow(2).CreateCell(1).SetCellValue(dt_p.Rows[0]["Quantity_Stock"].ToString());
                

                ws.CreateRow(3);
                ws.CreateRow(3).Height = 100 * 20;
                ws.GetRow(3).CreateCell(0).SetCellValue("備註");
                ws.GetRow(3).CreateCell(1).CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
                ws.GetRow(3).CreateCell(1).CellStyle.WrapText = true;
                ws.GetRow(3).CreateCell(1).SetCellValue(dt_p.Rows[0]["Note"].ToString());
                ws.AddMergedRegion(new CellRangeAddress(5, 5, 1, 5));


                

                int intI = 4;
                DataTable dt;
                string strPath;
                dt = clsData.UploadApparatusFileQuery(strID, "0");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strPath = dt.Rows[i]["file_path"].ToString();
                    byte[] bytes = System.IO.File.ReadAllBytes(strPath);

                    int pictureIdx = wb.AddPicture(bytes, PictureType.JPEG);
                    var patriarch = ws.CreateDrawingPatriarch();
                    var anchor = new HSSFClientAnchor(0, 0, 255, 255, (short)0, intI, (short)intI, intI + 20);
                    var pict = patriarch.CreatePicture(anchor, pictureIdx);

                    intI = intI + 22;

                }

            }
        }


        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=Goods.xls");
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wb.Write(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.WriteTo(Response.OutputStream);
            memoryStream.Close();
            Response.Flush();
            Response.End();
        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strKind;

        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", "");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion
}
