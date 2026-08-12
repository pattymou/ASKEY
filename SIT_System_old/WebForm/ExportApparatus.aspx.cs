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
//using ClosedXML.Excel;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Drawing;
//using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Linq;
//using System.Drawing;
//using System.Windows.Forms;
using NPOI;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
//using NPOI.Util;



public partial class WebForm_ExportApparatus : System.Web.UI.Page
{
    public static DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadKind(this.ddlKind);
        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7, "0");
    }
    #endregion 

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strKind;

        if (ddlKind.Text == "ALL")
            strKind = "";
        else 
            strKind = ddlKind.Text;
        dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", strKind);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }    

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strKind;

        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //if (ddlKind.Text == "ALL")
        //    strKind = "";
        //else
        //    strKind = ddlKind.Text;
        //DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", strKind);
        //this.gvwMain.DataSource = dt;
        //this.DataBind();
    }
    #endregion

    //protected static void InsertImage(Worksheet ws, long x, long y, long? width, long? height, string sImagePath)
    //{
    //    try
    //    {
    //        WorksheetPart wsp = ws.WorksheetPart;
    //        DrawingsPart dp;
    //        ImagePart imgp;
    //        WorksheetDrawing wsd;

    //        ImagePartType ipt;
    //        switch (sImagePath.Substring(sImagePath.LastIndexOf('.') + 1).ToLower())
    //        {
    //            case "png":
    //                ipt = ImagePartType.Png;
    //                break;
    //            case "jpg":
    //            case "jpeg":
    //                ipt = ImagePartType.Jpeg;
    //                break;
    //            case "gif":
    //                ipt = ImagePartType.Gif;
    //                break;
    //            default:
    //                return;
    //        }

    //        if (wsp.DrawingsPart == null)
    //        {
    //            //----- no drawing part exists, add a new one
    //            dp = wsp.AddNewPart<DrawingsPart>();
    //            imgp = dp.AddImagePart(ipt, wsp.GetIdOfPart(dp));
    //            wsd = new WorksheetDrawing();
    //        }
    //        else
    //        {
    //            //----- use existing drawing part
    //            dp = wsp.DrawingsPart;
    //            imgp = dp.AddImagePart(ipt);
    //            dp.CreateRelationshipToPart(imgp);
    //            wsd = dp.WorksheetDrawing;
    //        }

    //        using (FileStream fs = new FileStream(sImagePath, FileMode.Open))
    //        {
    //            imgp.FeedData(fs);
    //        }

    //        int imageNumber = dp.ImageParts.Count<ImagePart>();
    //        if (imageNumber == 1)
    //        {
    //            Drawing drawing = new Drawing();
    //            drawing.Id = dp.GetIdOfPart(imgp);
    //            ws.Append(drawing);
    //        }

    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties nvdp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties();
    //        nvdp.Id = new UInt32Value((uint)(1024 + imageNumber));
    //        nvdp.Name = "Picture " + imageNumber.ToString();
    //        nvdp.Description = "";
    //        DocumentFormat.OpenXml.Drawing.PictureLocks picLocks = new DocumentFormat.OpenXml.Drawing.PictureLocks();
    //        picLocks.NoChangeAspect = true;
    //        picLocks.NoChangeArrowheads = true;
    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties nvpdp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties();
    //        nvpdp.PictureLocks = picLocks;
    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties nvpp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties();
    //        nvpp.NonVisualDrawingProperties = nvdp;
    //        nvpp.NonVisualPictureDrawingProperties = nvpdp;

    //        DocumentFormat.OpenXml.Drawing.Stretch stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
    //        stretch.FillRectangle = new DocumentFormat.OpenXml.Drawing.FillRectangle();

    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill blipFill = new DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill();
    //        DocumentFormat.OpenXml.Drawing.Blip blip = new DocumentFormat.OpenXml.Drawing.Blip();
    //        blip.Embed = dp.GetIdOfPart(imgp);
    //        blip.CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print;
    //        blipFill.Blip = blip;
    //        blipFill.SourceRectangle = new DocumentFormat.OpenXml.Drawing.SourceRectangle();
    //        blipFill.Append(stretch);

    //        DocumentFormat.OpenXml.Drawing.Transform2D t2d = new DocumentFormat.OpenXml.Drawing.Transform2D();
    //        DocumentFormat.OpenXml.Drawing.Offset offset = new DocumentFormat.OpenXml.Drawing.Offset();
    //        offset.X = 0;
    //        offset.Y = 0;
    //        t2d.Offset = offset;
    //        Bitmap bm = new Bitmap(sImagePath);

    //        DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents();

    //        if (width == null)
    //            extents.Cx = (long)bm.Width * (long)((float)914400 / bm.HorizontalResolution);
    //        else
    //            extents.Cx = width;

    //        if (height == null)
    //            extents.Cy = (long)bm.Height * (long)((float)914400 / bm.VerticalResolution);
    //        else
    //            extents.Cy = height;

    //        bm.Dispose();
    //        t2d.Extents = extents;
    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties sp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties();
    //        sp.BlackWhiteMode = DocumentFormat.OpenXml.Drawing.BlackWhiteModeValues.Auto;
    //        sp.Transform2D = t2d;
    //        DocumentFormat.OpenXml.Drawing.PresetGeometry prstGeom = new DocumentFormat.OpenXml.Drawing.PresetGeometry();
    //        prstGeom.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle;
    //        prstGeom.AdjustValueList = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
    //        sp.Append(prstGeom);
    //        sp.Append(new DocumentFormat.OpenXml.Drawing.NoFill());

    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
    //        picture.NonVisualPictureProperties = nvpp;
    //        picture.BlipFill = blipFill;
    //        picture.ShapeProperties = sp;

    //        DocumentFormat.OpenXml.Drawing.Spreadsheet.Position pos = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Position();
    //        pos.X = x;
    //        pos.Y = y;
    //        Extent ext = new Extent();
    //        ext.Cx = extents.Cx;
    //        ext.Cy = extents.Cy;
    //        AbsoluteAnchor anchor = new AbsoluteAnchor();
    //        anchor.Position = pos;
    //        anchor.Extent = ext;
    //        anchor.Append(picture);
    //        anchor.Append(new ClientData());
    //        wsd.Append(anchor);
    //        wsd.Save(dp);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex; // or do something more interesting if you want
    //    }
    //}

    //private void AddImageToExcel(SpreadsheetDocument sd, MemoryStream imagestream)
    //{
    //    DrawingsPart dp = sd.WorkbookPart.WorksheetParts.First().AddNewPart<DrawingsPart>();
    //    ImagePart imgp = dp.AddImagePart(ImagePartType.Jpeg, sd.WorkbookPart.WorksheetParts.First().GetIdOfPart(dp));
    //    MemoryStream bmstream = new MemoryStream(imagestream.ToArray());
    //    bmstream.Seek(0, SeekOrigin.Begin);

    //    MemoryStream fs;
    //    using (fs = imagestream)
    //    {
    //        fs.Position = 0;
    //        imgp.FeedData(fs);
    //    }

    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties nvdp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties();
    //    nvdp.Id = 1025;
    //    nvdp.Name = "Char Image";
    //    nvdp.Description = "Image";
    //    DocumentFormat.OpenXml.Drawing.PictureLocks piclocks = new DocumentFormat.OpenXml.Drawing.PictureLocks();
    //    piclocks.NoChangeAspect = true;
    //    piclocks.NoChangeArrowheads = true;
    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties nvpdp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties();
    //    nvpdp.PictureLocks = piclocks;
    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties nvpp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties();
    //    nvpp.NonVisualDrawingProperties = nvdp;
    //    nvpp.NonVisualPictureDrawingProperties = nvpdp;

    //    DocumentFormat.OpenXml.Drawing.Stretch stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
    //    stretch.FillRectangle = new DocumentFormat.OpenXml.Drawing.FillRectangle();

    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill blipfill = new DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill();
    //    DocumentFormat.OpenXml.Drawing.Blip blip = new DocumentFormat.OpenXml.Drawing.Blip();
    //    blip.Embed = dp.GetIdOfPart(imgp);
    //    blip.CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print;
    //    blipfill.Blip = blip;
    //    blipfill.SourceRectangle = new DocumentFormat.OpenXml.Drawing.SourceRectangle();
    //    blipfill.Append(stretch);

    //    DocumentFormat.OpenXml.Drawing.Transform2D t2d = new DocumentFormat.OpenXml.Drawing.Transform2D();
    //    DocumentFormat.OpenXml.Drawing.Offset offset = new DocumentFormat.OpenXml.Drawing.Offset();
    //    offset.X = 0;
    //    offset.Y = 0;
    //    t2d.Offset = offset;
    //    Bitmap bm = new Bitmap(bmstream);

    //    DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents();
    //    extents.Cx = ((long)bm.Width * (long)((float)914400 / bm.HorizontalResolution));
    //    extents.Cy = ((long)bm.Height * (long)((float)914400 / bm.VerticalResolution));
    //    bm.Dispose();
    //    t2d.Extents = extents;
    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties sp = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties();
    //    sp.BlackWhiteMode = DocumentFormat.OpenXml.Drawing.BlackWhiteModeValues.Auto;
    //    sp.Transform2D = t2d;
    //    DocumentFormat.OpenXml.Drawing.PresetGeometry prstgeom = new DocumentFormat.OpenXml.Drawing.PresetGeometry();
    //    prstgeom.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle;
    //    prstgeom.AdjustValueList = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
    //    sp.Append(prstgeom);
    //    sp.Append(new DocumentFormat.OpenXml.Drawing.NoFill());

    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
    //    picture.NonVisualPictureProperties = nvpp;
    //    picture.BlipFill = blipfill;
    //    picture.ShapeProperties = sp;

    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.Position pos = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Position();

    //    pos.X = 600000;
    //    pos.Y = 200000;

    //    //Extent ext = new Extent();

    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.Extent ext = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Extent();
    //    ext.Cx = extents.Cx;
    //    ext.Cy = extents.Cy;
    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.AbsoluteAnchor anchor = new DocumentFormat.OpenXml.Drawing.Spreadsheet.AbsoluteAnchor();

    //    anchor.Position = pos;
    //    anchor.Extent = ext;
    //    anchor.Append(picture);
    //    anchor.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.ClientData());
    //    DocumentFormat.OpenXml.Drawing.Spreadsheet.WorksheetDrawing wsd = new DocumentFormat.OpenXml.Drawing.Spreadsheet.WorksheetDrawing();
    //    wsd.Append(anchor);
    //    Drawing drawing = new Drawing();
    //    drawing.Id = dp.GetIdOfPart(imgp);
    //    wsd.Save(dp);
    //    sd.WorkbookPart.WorksheetParts.First().Worksheet.Append(drawing);

    //}

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

        string strID, strDate1,strDate;
        string strSheet;
        string strCol, strCol1;

        DateTime dt_Date;

        var wb = new HSSFWorkbook();

        for (int ii = 0; ii < this.gvwMain.Rows.Count; ii++)
        {
            if (((CheckBox)gvwMain.Rows[ii].FindControl("CheckBox2")).Checked)
            {
                strID = ((Label)this.gvwMain.Rows[ii].Cells[3].FindControl("lblGVSeq")).Text;
                DataTable dt_p = clsData.UploadApparatusQuery(strID, "1", "");

                strSheet = dt_p.Rows[0]["products_id"].ToString() + dt_p.Rows[0]["Name"].ToString();
                var ws = wb.CreateSheet(strSheet);
                ws.SetColumnWidth(3,10000);

                ws.CreateRow(0);
                ws.GetRow(0).CreateCell(0).SetCellValue("財產編號");
                ws.GetRow(0).CreateCell(1).SetCellValue(dt_p.Rows[0]["products_id"].ToString());
                ws.GetRow(0).CreateCell(2).SetCellValue("設備名稱");
                ws.GetRow(0).CreateCell(3).SetCellValue(dt_p.Rows[0]["Name"].ToString());

                ws.CreateRow(1);
                ws.GetRow(1).CreateCell(0).SetCellValue("廠牌");
                ws.GetRow(1).CreateCell(1).SetCellValue(dt_p.Rows[0]["brand"].ToString());
                ws.GetRow(1).CreateCell(2).SetCellValue("型號");
                ws.GetRow(1).CreateCell(3).SetCellValue(dt_p.Rows[0]["model"].ToString());

                ws.CreateRow(2);
                ws.GetRow(2).CreateCell(0).SetCellValue("序號");
                ws.GetRow(2).CreateCell(1).SetCellValue(dt_p.Rows[0]["number"].ToString());
                dt_Date = Convert.ToDateTime(dt_p.Rows[0]["InspectionDate"].ToString());
                strDate = dt_Date.ToString("yyyy/MM/dd");
                if (strDate == "1900/01/01")
                    strDate1 = "";
                else
                    strDate1 = strDate;
                ws.GetRow(2).CreateCell(2).SetCellValue("檢查時間");
                ws.GetRow(2).CreateCell(3).SetCellValue(strDate1);

                dt_Date = Convert.ToDateTime(dt_p.Rows[0]["MaintenanceDate"].ToString());
                strDate = dt_Date.ToString("yyyy/MM/dd");
                if (strDate == "1900/01/01")
                    strDate1 = "";
                else
                    strDate1 = strDate;
                ws.CreateRow(3);
                ws.GetRow(3).CreateCell(0).SetCellValue("保養時間");
                ws.GetRow(3).CreateCell(1).SetCellValue(strDate1);
                ws.GetRow(3).CreateCell(2).SetCellValue("放置地點");
                ws.GetRow(3).CreateCell(3).SetCellValue(dt_p.Rows[0]["Place"].ToString());

                ws.CreateRow(4);
                ws.GetRow(4).CreateCell(0).SetCellValue("保管人");
                ws.GetRow(4).CreateCell(1).SetCellValue(dt_p.Rows[0]["Custodian"].ToString());
                ws.GetRow(4).CreateCell(2).SetCellValue("設備狀態");
                if (dt_p.Rows[0]["ReservationStatus"].ToString() == "Y")
                    ws.GetRow(4).CreateCell(3).SetCellValue("可借用");
                else
                    ws.GetRow(4).CreateCell(3).SetCellValue("不可借用");

                ws.CreateRow(5);
                ws.CreateRow(5).Height = 100 * 20;
                ws.GetRow(5).CreateCell(0).SetCellValue("備註");
                ws.GetRow(5).CreateCell(1).CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
                ws.GetRow(5).CreateCell(1).CellStyle.WrapText = true;
                ws.GetRow(5).CreateCell(1).SetCellValue(dt_p.Rows[0]["Note"].ToString());
                ws.AddMergedRegion(new CellRangeAddress(5, 5, 1, 5));


                ws.CreateRow(6);
                ws.CreateRow(6).Height = 100 * 20;
                ws.GetRow(6).CreateCell(0).SetCellValue("Feature");
                string strFeature;
                strFeature = dt_p.Rows[0]["Feature"].ToString().Replace("\n", ((char)10).ToString());
                ws.GetRow(6).CreateCell(1).CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
                ws.GetRow(6).CreateCell(1).CellStyle.WrapText = true;
                ws.GetRow(6).CreateCell(1).SetCellValue(strFeature);
                ws.AddMergedRegion(new CellRangeAddress(6, 6, 1, 5));

                ws.CreateRow(7);
                ws.CreateRow(7).Height = 100 * 20;
                ws.GetRow(7).CreateCell(0).SetCellValue("Spec");
                ws.GetRow(7).CreateCell(1).CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
                ws.GetRow(7).CreateCell(1).CellStyle.WrapText = true;
                ws.GetRow(7).CreateCell(1).SetCellValue(dt_p.Rows[0]["Spec"].ToString());
                ws.AddMergedRegion(new CellRangeAddress(7, 7, 1, 5));

                int intI = 8;
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
        Response.AddHeader("content-disposition", "attachment;filename=Apparatus.xls");
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wb.Write(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.WriteTo(Response.OutputStream);
            memoryStream.Close();
            Response.Flush();
            Response.End();
        }


        //===========ClosedXML
        //string strID, strDate1,strDate;
        //string strSheet;
        //string strCol, strCol1;
         
        //DateTime dt_Date;
        //var workbook = new XLWorkbook();
        //for (int ii = 0; ii < this.gvwMain.Rows.Count; ii++)
        //{
        //    if (((CheckBox)gvwMain.Rows[ii].FindControl("CheckBox2")).Checked)
        //    {
        //        strID = ((Label)this.gvwMain.Rows[ii].Cells[3].FindControl("lblGVSeq")).Text;
        //        DataTable dt_p = clsData.UploadApparatusQuery(strID, "1", "");

        //        strSheet = dt_p.Rows[0]["products_id"].ToString() + dt_p.Rows[0]["Name"].ToString();
        //        var ws = workbook.Worksheets.Add(strSheet);
        //        ws.Cell(1, 1).Value = "財產編號";
        //        ws.Cell(1, 2).Value = dt_p.Rows[0]["products_id"].ToString();
        //        ws.Cell(1, 3).Value = "設備名稱";
        //        ws.Cell(1, 4).Value = dt_p.Rows[0]["Name"].ToString();

        //        ws.Cell(2, 1).Value = "廠牌";
        //        ws.Cell(2, 2).Value = dt_p.Rows[0]["brand"].ToString();
        //        ws.Cell(2, 3).Value = "型號";
        //        ws.Cell(2, 4).Value = dt_p.Rows[0]["model"].ToString();

        //        ws.Cell(3, 1).Value = "序號";
        //        ws.Cell(3, 2).Value = dt_p.Rows[0]["number"].ToString();

        //        dt_Date = Convert.ToDateTime(dt_p.Rows[0]["InspectionDate"].ToString());
        //        strDate = dt_Date.ToString("yyyy/MM/dd");
        //        if (strDate == "1900/01/01")
        //            strDate1 = "";
        //        else
        //            strDate1 = strDate;
        //        ws.Cell(3, 3).Value = "檢查時間";
        //        ws.Cell(3, 4).Value = strDate1;


        //        dt_Date = Convert.ToDateTime(dt_p.Rows[0]["MaintenanceDate"].ToString());
        //        strDate = dt_Date.ToString("yyyy/MM/dd");
        //        if (strDate == "1900/01/01")
        //            strDate1 = "";
        //        else
        //            strDate1 = strDate;
        //        ws.Cell(4, 1).Value = "保養時間";
        //        ws.Cell(4, 2).Value = strDate1;
        //        ws.Cell(4, 3).Value = "放置地點";
        //        ws.Cell(4, 4).Value = dt_p.Rows[0]["Place"].ToString();

        //        ws.Cell(5, 1).Value = "保管人";
        //        ws.Cell(5, 2).Value = dt_p.Rows[0]["Custodian"].ToString();

        //        ws.Cell(5, 3).Value = "設備狀態";
        //        //strRStatus = dt_p.Rows[0]["ReservationStatus"].ToString();
        //        if (dt_p.Rows[0]["ReservationStatus"].ToString() == "Y")
        //            ws.Cell(5, 4).Value = "可借用";
        //        else
        //            ws.Cell(5, 4).Value = "不可借用";

        //        ws.Cell(6, 1).Value = "備註";
        //        ws.Cell("B6").Value = dt_p.Rows[0]["Explain"].ToString();  //合併儲存格

        //        ws.Cell("B6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //        ws.Cell("B6").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        //        ws.Range("B6:D6").Merge();

        //        ws.Rows(6, 2).AdjustToContents();

        //        var row1 = ws.Row(6);
        //        row1.Height = 200;

        //        ws.Cell(7, 1).Value = "Feature";
        //        ws.Cell(7, 2).Value = dt_p.Rows[0]["Feature"].ToString();
        //        ws.Cell(7, 1).Style.Font.Bold = true;

        //        ws.Cell(8, 1).Value = "Spec";
        //        ws.Cell(8, 2).Value = dt_p.Rows[0]["Spec"].ToString();

        //        ws.Cell("B8").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //        ws.Cell("B8").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        //        ws.Range("B8:D8").Merge();

        //        ws.Rows(8, 2).AdjustToContents();

        //        row1 = ws.Row(8);
        //        row1.Height = 200;
                
        //        string ImageFile=@"C:\172162.jpeg";
                
        //        Stream imagestream = new FileStream (ImageFile,FileMode.Open,FileAccess.ReadWrite);
        //        MemoryStream mStream = imagestream as MemoryStream;

        //        //InsertImage(ws,500,500,500,500,"C:\172162.jpeg");
        //        //AddImageToExcel(workbook, mStream);
                


            //}
        //}
    }


}
