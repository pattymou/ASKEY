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
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Diagnostics;
using System.IO;
using NPOI;
using NPOI.HSSF;
using NPOI.HSSF.Util;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;


public partial class WebForm_BenchmarkStatistics : System.Web.UI.Page
{
    //XLWorkbook wb = new XLWorkbook();
    //DataTable dtExport = new DataTable();
    HSSFWorkbook wb;

    HSSFSheet ws;
    MemoryStream memoryStream;


    protected void Page_Load(object sender, EventArgs e)
    {
        memoryStream = new MemoryStream();
        wb = new HSSFWorkbook();
        //getThroughput(this.gvwMain,this.Chart1);
        //gvwMain.Visible = false;
        //Chart1.Visible = false;

        //if (Session["EmpNo"] == null)
        //    Response.Redirect("~/Default.aspx");
        //Image2.ImageUrl = "C:/inetpub/wwwroot/SIT_System_patty/WebForm/Benchmark/patty_lu/chart3.jpg";
        if (!IsPostBack)
        {
            getThroughput();
        }
    }

    private void GenImage(int intGID,string strImgPath)
    {
        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
        //System.Drawing.Image img = System.Drawing.Image.FromFile(strImgPath);

        img.ID = "img" + intGID.ToString();
        img.ImageUrl = strImgPath;
        //img.Visible = false;



        Panel1.Controls.Add(img);

        Label lblBr = new Label();
        lblBr.ID = "lbl" + intGID.ToString();
        lblBr.Text = "<br/>";
        Panel1.Controls.Add(lblBr);

        Label lblBr1 = new Label();
        lblBr1.ID = "lbl" + intGID.ToString() + "a";
        lblBr1.Text = "<br/>";
        Panel1.Controls.Add(lblBr1);

    }

    private void GenLable(int intGID,string strT)
    {
        string strGID;



        strGID = (intGID + 1).ToString();

        Label lbl = new Label();
        lbl.ID = "lbl" + intGID.ToString();
        //lbl.Text = "Test Case_" + strGID + " - 802.11ac - 5G Tx Throughput Test ( 20MHz )";

        lbl.Text = "Test Case_" + strGID + strT;

        Panel1.Controls.Add(lbl);

    }

    private GridView GenGridView(string strGID)
    {
        string strName;

        strName = "gvwMain" + strGID;
        GridView gvwMain1 = new GridView();

        //gvwMain1.ID = "gvwMain1";
        gvwMain1.ID = strName;
        gvwMain1.CellPadding = 4;
        gvwMain1.ForeColor = ColorTranslator.FromHtml("#333333");
        gvwMain1.GridLines = GridLines.None;
        gvwMain1.AllowPaging = true;
        gvwMain1.AutoGenerateColumns = false;
        gvwMain1.Style.Add("width", "100%");
        gvwMain1.FooterStyle.BackColor = ColorTranslator.FromHtml("#507CD1");
        gvwMain1.FooterStyle.Font.Bold = true;
        gvwMain1.FooterStyle.ForeColor = Color.FloralWhite;
        gvwMain1.RowStyle.BackColor = ColorTranslator.FromHtml("#EFF3FB");
        gvwMain1.RowStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.EditRowStyle.BackColor = ColorTranslator.FromHtml("#2461BF");
        gvwMain1.SelectedRowStyle.BackColor = ColorTranslator.FromHtml("#D1DDF1");
        gvwMain1.SelectedRowStyle.Font.Bold = true;
        gvwMain1.SelectedRowStyle.ForeColor = ColorTranslator.FromHtml("#333333");
        gvwMain1.PagerStyle.BackColor = ColorTranslator.FromHtml("#2461BF");
        gvwMain1.PagerStyle.ForeColor = Color.FloralWhite;
        gvwMain1.PagerStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.HeaderStyle.BackColor = ColorTranslator.FromHtml("#507CD1");
        gvwMain1.HeaderStyle.Font.Bold = true;
        gvwMain1.HeaderStyle.ForeColor = Color.FloralWhite;
        gvwMain1.AlternatingRowStyle.BackColor = Color.FloralWhite;

        BoundField bField = new BoundField();
        bField.HeaderText = "";
        bField.DataField = "Name";
        bField.ReadOnly = true;
        bField.SortExpression = "Name";
        bField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        bField.HtmlEncode = false;
        gvwMain1.Columns.Add(bField);

        BoundField bField1 = new BoundField();
        bField1.HeaderText = "";
        bField1.DataField = "Throughput1";
        bField1.ReadOnly = true;
        bField1.SortExpression = "Throughput1";
        bField1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField1);

        BoundField bField2 = new BoundField();
        bField2.HeaderText = "";
        bField2.DataField = "Throughput2";
        bField2.ReadOnly = true;
        bField2.SortExpression = "Throughput2";
        bField2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField2);

        BoundField bField3 = new BoundField();
        bField3.HeaderText = "";
        bField3.DataField = "Throughput3";
        bField3.ReadOnly = true;
        bField3.SortExpression = "Throughput3";
        bField3.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField3);

        BoundField bField4 = new BoundField();
        bField4.HeaderText = "";
        bField4.DataField = "Throughput4";
        bField4.ReadOnly = true;
        bField4.SortExpression = "Throughput4";
        bField4.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField4);

        BoundField bField5 = new BoundField();
        bField5.HeaderText = "";
        bField5.DataField = "Throughput5";
        bField5.ReadOnly = true;
        bField5.SortExpression = "Throughput5";
        bField5.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField5);

        BoundField bField6 = new BoundField();
        bField6.HeaderText = "";
        bField6.DataField = "Throughput6";
        bField6.ReadOnly = true;
        bField6.SortExpression = "Throughput6";
        bField6.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField6);

        BoundField bField7 = new BoundField();
        bField7.HeaderText = "";
        bField7.DataField = "Throughput7";
        bField7.ReadOnly = true;
        bField7.SortExpression = "Throughput7";
        bField7.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField7);

        BoundField bField8 = new BoundField();
        bField8.HeaderText = "";
        bField8.DataField = "Throughput8";
        bField8.ReadOnly = true;
        bField8.SortExpression = "Throughput8";
        bField8.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField8);

        BoundField bField9 = new BoundField();
        bField9.HeaderText = "";
        bField9.DataField = "Throughput9";
        bField9.ReadOnly = true;
        bField9.SortExpression = "Throughput9";
        bField9.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField9);

        BoundField bField10 = new BoundField();
        bField10.HeaderText = "";
        bField10.DataField = "Throughput10";
        bField10.ReadOnly = true;
        bField10.SortExpression = "Throughput10";
        bField10.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField10);

        BoundField bField11 = new BoundField();
        bField11.HeaderText = "";
        bField11.DataField = "Throughput11";
        bField11.ReadOnly = true;
        bField11.SortExpression = "Throughput11";
        bField11.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        gvwMain1.Columns.Add(bField11);

        Panel1.Controls.Add(gvwMain1);


        //===================================

        //Chart chartGen = new Chart();
        //ChartArea cArea = new ChartArea();
        //cArea.Name = "ChartArea1";
        //chartGen.ChartAreas.Add(cArea);
        

        //Panel1.Controls.Add(chartGen);

        return gvwMain1;
        //getThroughput(gvwMain1, chartGen);
    }

    private Chart GenChart(string strGID)
    {
        string strName;

        //===================================

        Chart chartGen = new Chart();
        ChartArea cArea = new ChartArea();

        strName = "Chart" + strGID;
        chartGen.ID = strName;
        cArea.Name = "ChartArea1";
        chartGen.ChartAreas.Add(cArea);


        Panel1.Controls.Add(chartGen);

        Label lblBr = new Label();
        lblBr.ID = "lbl" + strGID;
        lblBr.Text = "<br/>";
        Panel1.Controls.Add(lblBr);

        Label lblBr1 = new Label();
        lblBr1.ID = "lbl" + strGID + "a";
        lblBr1.Text = "<br/>";
        Panel1.Controls.Add(lblBr1);


        return chartGen;
        //getThroughput(gvwMain1, chartGen);
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

    protected void getThroughput()
    {
        //var ws = wb.Worksheets.Add("Benchmark");
        string strBenchmarkID;
        string str11 = "";
        string strT = "";
        string strT1 = "";
        string strTitle = "";
        int intGID = 0;
        int intValue = 0;
        //int intExport = 0;
        int intE = 0;

        GridView gv1;
        Chart ct1;

        HttpCookie cookie_BenchmarkID = Request.Cookies["BenchmarkID"];
        strBenchmarkID = Server.UrlDecode(cookie_BenchmarkID.Value);

        //var ws = wb.CreateSheet("Benchmark");
        ws = (HSSFSheet)wb.CreateSheet("Benchmark");


        //string strPath1 = Server.MapPath(".") + @"\Benchmark\" + Session["EmpNo"];
        string strPath1 = Server.MapPath(".") + @"\Benchmark\" + "patty_lu";

        if (!Directory.Exists(strPath1))  // 若目錄不存在則建立之
        {
            Directory.CreateDirectory(strPath1);
        }
        else
        {

            DirectoryInfo DIFO = new DirectoryInfo(strPath1);
            FileInfo[] filelist = DIFO.GetFiles();
            foreach (FileInfo fl in filelist)
            {
                System.IO.File.Delete(fl.FullName);
            }
        }

        for (int intK = 0; intK < 10; intK++)
        {
            if (intK == 0)
            {
                HttpCookie cookie_11A = Request.Cookies["11A"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11a ";
                strT1 = "";
            }
            if (intK == 1)
            {
                HttpCookie cookie_11A = Request.Cookies["11B"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11b ";
                strT1 = "";
            }
            if (intK == 2)
            {
                HttpCookie cookie_11A = Request.Cookies["11G"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11g ";
                strT1 = "";
            }
            if (intK == 3)
            {
                HttpCookie cookie_11A = Request.Cookies["11N22"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11n - 2.4G ";
                strT1 = " 20MHz ";
            }
            if (intK == 4)
            {
                HttpCookie cookie_11A = Request.Cookies["11N24"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11n - 2.4G ";
                strT1 = " 40MHz ";
            }
            if (intK == 5)
            {
                HttpCookie cookie_11A = Request.Cookies["11N52"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11n - 5G ";
                strT1 = " 20MHz ";
            }
            if (intK == 6)
            {
                HttpCookie cookie_11A = Request.Cookies["11N54"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11n - 5G ";
                strT1 = " 40MHz ";
            }
            if (intK == 7)
            {
                HttpCookie cookie_11A = Request.Cookies["11AC2"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11ac - 5G ";
                strT1 = " 20MHz ";
            }
            if (intK == 8)
            {
                HttpCookie cookie_11A = Request.Cookies["11AC4"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11ac - 5G ";
                strT1 = " 40MHz ";
            }
            if (intK == 9)
            {
                HttpCookie cookie_11A = Request.Cookies["11AC8"];
                str11 = Server.UrlDecode(cookie_11A.Value);
                strT = " 802.11ac - 5G ";
                strT1 = " 80MHz ";
            }

            string[] strCH = str11.Split(',');
            string strTRx = "";
            DataRow dr;
            if (str11 != "")
            {
                foreach (string strJ in strCH)
                {
                    for (int intL = 0; intL < 3; intL++)
                    {

                        if (intL == 0)
                            strTRx = "Tx";
                        if (intL == 1)
                            strTRx = "Rx";
                        if (intL == 2)
                            strTRx = "TxRx";

                    DataTable dt_new = new DataTable("dt_new");

                    DataColumn column1 = new DataColumn("Name");
                    column1.DataType = System.Type.GetType("System.String");
                    column1.AllowDBNull = true;
                    column1.Caption = "Name";
                    column1.DefaultValue = "0";
                    dt_new.Columns.Add(column1);

                    DataColumn column2 = new DataColumn("Throughput1");
                    column2.DataType = System.Type.GetType("System.String");
                    column2.AllowDBNull = true;
                    column2.Caption = "Throughput1";
                    column2.DefaultValue = "0";
                    dt_new.Columns.Add(column2);

                    DataColumn column3 = new DataColumn("Throughput2");
                    column3.DataType = System.Type.GetType("System.String");
                    column3.AllowDBNull = true;
                    column3.Caption = "Throughput2";
                    column3.DefaultValue = "0";
                    dt_new.Columns.Add(column3);

                    DataColumn column4 = new DataColumn("Throughput3");
                    column4.DataType = System.Type.GetType("System.String");
                    column4.AllowDBNull = true;
                    column4.Caption = "Throughput3";
                    column4.DefaultValue = "0";
                    dt_new.Columns.Add(column4);

                    DataColumn column5 = new DataColumn("Throughput4");
                    column5.DataType = System.Type.GetType("System.String");
                    column5.AllowDBNull = true;
                    column5.Caption = "Throughput4";
                    column5.DefaultValue = "0";
                    dt_new.Columns.Add(column5);

                    DataColumn column6 = new DataColumn("Throughput5");
                    column6.DataType = System.Type.GetType("System.String");
                    column6.AllowDBNull = true;
                    column6.Caption = "Throughput5";
                    column6.DefaultValue = "0";
                    dt_new.Columns.Add(column6);

                    DataColumn column7 = new DataColumn("Throughput6");
                    column7.DataType = System.Type.GetType("System.String");
                    column7.AllowDBNull = true;
                    column7.Caption = "Throughput6";
                    column7.DefaultValue = "0";
                    dt_new.Columns.Add(column7);

                    DataColumn column8 = new DataColumn("Throughput7");
                    column8.DataType = System.Type.GetType("System.String");
                    column8.AllowDBNull = true;
                    column8.Caption = "Throughput7";
                    column8.DefaultValue = "0";
                    dt_new.Columns.Add(column8);

                    DataColumn column9 = new DataColumn("Throughput8");
                    column9.DataType = System.Type.GetType("System.String");
                    column9.AllowDBNull = true;
                    column9.Caption = "Throughput8";
                    column9.DefaultValue = "0";
                    dt_new.Columns.Add(column9);

                    DataColumn column10 = new DataColumn("Throughput9");
                    column10.DataType = System.Type.GetType("System.String");
                    column10.AllowDBNull = true;
                    column10.Caption = "Throughput9";
                    column10.DefaultValue = "0";
                    dt_new.Columns.Add(column10);

                    DataColumn column11 = new DataColumn("Throughput10");
                    column11.DataType = System.Type.GetType("System.String");
                    column11.AllowDBNull = true;
                    column11.Caption = "Throughput10";
                    column11.DefaultValue = "0";
                    dt_new.Columns.Add(column11);

                    DataColumn column12 = new DataColumn("Throughput11");
                    column12.DataType = System.Type.GetType("System.String");
                    column12.AllowDBNull = true;
                    column12.Caption = "Throughput11";
                    column12.DefaultValue = "0";
                    dt_new.Columns.Add(column12);

                    //if (intExport == 0)
                    //{
                    //    dtExport = dt_new.Clone();
                    //    intExport = 1;
                    //}


                    //strBenchmarkID = "19,20";

                    string[] strBID = strBenchmarkID.Split(',');
                    int intI = 0;
                    string strTName = "";
                    DataTable dt;
                    
                    string strC1="";
                    

                        foreach (string strI in strBID)
                        {

                            if (intI == 0)
                            {
                                dr = dt_new.NewRow();
                                dt = clsData.UploadBenchmarkLos(strI, "Attenuation", "", "");
                                dr["Name"] = "Attenuation (dB)";

                                //string strTName;
                                for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
                                {
                                    strTName = "Throughput" + (intJ + 1).ToString();
                                    dr[strTName] = dt.Rows[intJ]["Attenuation"].ToString();
                                }
                                dt_new.Rows.Add(dr);
                                intI = 1;
                            }
                            if (intI == 1)
                            {
                                dr = dt_new.NewRow();
                                dt = clsData.UploadBenchmarkLos(strI, "Distance", "", "");
                                dr["Name"] = "Distance (meter)";

                                //string strTName;
                                for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
                                {
                                    strTName = "Throughput" + (intJ + 1).ToString();
                                    dr[strTName] = dt.Rows[intJ]["Distance"].ToString();
                                }
                                dt_new.Rows.Add(dr);
                                intI = 2;
                            }
                            //else
                            //{
                            dr = dt_new.NewRow();
                            dt = clsData.UploadBenchmarkLos(strI, "Data", strJ, strTRx);
                            if (dt.Rows.Count != 0)
                            {
                                string[] strC;
                                
                                strC = strJ.Split('/');
                                strC1 = strC[1].Trim();

                                dr["Name"] = dt.Rows[0]["Name"].ToString();

                                //string strTName;
                                for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
                                {
                                    intValue = 1;
                                    strTName = "Throughput" + (intJ + 1).ToString();
                                    dr[strTName] = dt.Rows[intJ]["Throughput"].ToString();
                                }

                                dt_new.Rows.Add(dr);
                            }
                        }
                        //dr = dt_new.NewRow();
                        //dr["Name"] = "<img src=\"../WebForm/Benchmark/chart3.jpg\" />";

                        //dt_new.Rows.Add(dr);
                        if (intValue == 1)
                        {
                            strTitle = string.Format(" - {0} - {1} - {2} Throughput Test ({3})", strT, strC1, strTRx, strT1);
                            GenLable(intGID, strTitle);
                            
                            gv1 = GenGridView(intGID.ToString());
                            ct1 = GenChart(intGID.ToString());


                            //gv1.DataSource = dt_new;
                            //gv1.DataBind();
                            //this.gvwMain.DataSource = dt_new;
                            //this.DataBind();

                            ct1.Height = 450;
                            ct1.Width = 860;

                            for (int intJ = 2; intJ < dt_new.Rows.Count; intJ++)
                            {
                                Series series = new Series(dt_new.Rows[intJ]["Name"].ToString());
                                series.ChartType = SeriesChartType.Line;
                                series.BorderColor = Color.FromArgb(180, 26, 59, 105);
                                series.BorderWidth = 3;
                                series.ShadowColor = Color.Black;
                                series.ShadowOffset = 2;
                                series.IsVisibleInLegend = true;
                                series.IsValueShownAsLabel = false;
                                series.MarkerStyle = MarkerStyle.Circle;
                                series.MarkerSize = 8;
                                series.Points.AddXY(dt_new.Rows[0]["Throughput1"].ToString(), dt_new.Rows[intJ]["Throughput1"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput2"].ToString(), dt_new.Rows[intJ]["Throughput2"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput3"].ToString(), dt_new.Rows[intJ]["Throughput3"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput4"].ToString(), dt_new.Rows[intJ]["Throughput4"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput5"].ToString(), dt_new.Rows[intJ]["Throughput5"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput6"].ToString(), dt_new.Rows[intJ]["Throughput6"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput7"].ToString(), dt_new.Rows[intJ]["Throughput7"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput8"].ToString(), dt_new.Rows[intJ]["Throughput8"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput9"].ToString(), dt_new.Rows[intJ]["Throughput9"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput10"].ToString(), dt_new.Rows[intJ]["Throughput10"].ToString());
                                series.Points.AddXY(dt_new.Rows[0]["Throughput11"].ToString(), dt_new.Rows[intJ]["Throughput11"].ToString());

                                ct1.Series.Add(series);
                            }

                            ct1.BackColor = Color.FromArgb(211, 223, 240);
                            ct1.BackGradientStyle = GradientStyle.TopBottom;
                            ct1.BorderlineColor = Color.FromArgb(26, 59, 105);
                            ct1.BorderlineDashStyle = ChartDashStyle.Solid;
                            ct1.BorderlineWidth = 2;
                            ct1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

                            strTitle = string.Format("{0} - {1} - {2} - {3} Throughput  (Mbps) vs. Attenuation ", strT, strT1, strC1, strTRx);
                            Title title = new Title();
                            //title.Text = "802.11ac - 5G - 80MHz - Ch149 - Rx. Throughput  (Mbps) vs. Attenuation";
                            title.Text = strTitle;
                            title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                            title.ForeColor = Color.FromArgb(26, 59, 105);
                            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
                            title.ShadowOffset = 3;

                            ct1.Titles.Add(title);


                            ct1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                            ct1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = 1;
                            ct1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
                            ct1.ChartAreas["ChartArea1"].AxisY.Interval = 100;
                            ct1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(64, 165, 191, 228);
                            ct1.ChartAreas["ChartArea1"].BackGradientStyle = GradientStyle.TopBottom;
                            ct1.ChartAreas["ChartArea1"].BackSecondaryColor = Color.White;
                            ct1.ChartAreas["ChartArea1"].BorderColor = Color.FromArgb(64, 64, 64, 64);
                            ct1.ChartAreas["ChartArea1"].ShadowColor = Color.Transparent;
                            ct1.ChartAreas["ChartArea1"].AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
                            ct1.ChartAreas["ChartArea1"].AxisX.LineWidth = 1;
                            ct1.ChartAreas["ChartArea1"].AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
                            ct1.ChartAreas["ChartArea1"].AxisY.LineWidth = 1;
                            ct1.ChartAreas["ChartArea1"].AxisX.Title = "Attenuation (dB)";
                            ct1.ChartAreas["ChartArea1"].AxisY.Title = "Throughput (Mbps)";
                            ct1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
                            ct1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 1;
                            ct1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
                            ct1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 1;

                            Legend legend = new Legend();
                            legend.Alignment = StringAlignment.Center;
                            legend.Docking = Docking.Bottom;
                            ct1.Legends.Add(legend);

                            //string strPath1 = Server.MapPath(".") + @"\Benchmark\" + Session["EmpNo"];
                            strPath1 = Server.MapPath(".") + @"\Benchmark\" + "patty_lu";

                            //if (!Directory.Exists(strPath1))  // 若目錄不存在則建立之
                            //{
                            //    Directory.CreateDirectory(strPath1);
                            //}
                            //else
                            //{

                            //    DirectoryInfo DIFO = new DirectoryInfo(strPath1);
                            //    FileInfo[] filelist = DIFO.GetFiles();
                            //    foreach (FileInfo fl in filelist)
                            //    {
                            //        System.IO.File.Delete(fl.FullName);
                            //    }
                            //}  ~/WebForm/Benchmark/chart3.jpg  C:\\inetpub\\wwwroot\\SIT_System_patty\\WebForm\\Benchmark\\patty_lu\\chart3.jpg
                            strPath1 = strPath1 + @"\chart" + intGID.ToString() + ".jpg";
                            
                            ct1.SaveImage(strPath1,ChartImageFormat.Jpeg);
                            strPath1 = "~/WebForm/Benchmark/patty_lu/chart" + intGID.ToString() + ".jpg";
                            //GenImage(intGID, strPath1);
                            ct1.Visible = false;

                            dr = dt_new.NewRow();
                            //dr["Name"] = "<img src=\"../WebForm/Benchmark/chart3.jpg\" />";
                            dr["Name"] = "<img src=\"../WebForm/Benchmark/patty_lu/chart"+ intGID.ToString() +".jpg\" />";

                            dt_new.Rows.Add(dr);
                            gv1.DataSource = dt_new;
                            gv1.DataBind();

                            int intCell;
                            intCell = gv1.Rows.Count;
                            //gv1.Rows[intCell-1].Attributes.Add("colspan", "12");
                            //gv1.Rows[intCell - 1].Cells[0].Attributes.Add("colspan", "12");
                            gv1.Rows[intCell - 1].Cells[0].ColumnSpan = 12;

                            //gridview 刪除後，欄位編號會重編
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            gv1.Rows[intCell - 1].Cells.RemoveAt(1);
                            //gv1.Controls[intCell].Controls.AddAt(0, gv1);

                            gv1.Rows[intCell - 1].BackColor = Color.White;

                            //int intP = dt_new.Rows.Count;
                            for (int i = 0; i < dt_new.Rows.Count-1; i++)
                            {
                                ws.CreateRow(intE);
                                ws.GetRow(intE).CreateCell(0).SetCellValue(dt_new.Rows[i]["Name"].ToString());
                                ws.GetRow(intE).CreateCell(1).SetCellValue(dt_new.Rows[i]["Throughput1"].ToString());
                                ws.GetRow(intE).CreateCell(2).SetCellValue(dt_new.Rows[i]["Throughput2"].ToString());
                                ws.GetRow(intE).CreateCell(3).SetCellValue(dt_new.Rows[i]["Throughput3"].ToString());
                                ws.GetRow(intE).CreateCell(4).SetCellValue(dt_new.Rows[i]["Throughput4"].ToString());
                                ws.GetRow(intE).CreateCell(5).SetCellValue(dt_new.Rows[i]["Throughput5"].ToString());
                                ws.GetRow(intE).CreateCell(6).SetCellValue(dt_new.Rows[i]["Throughput6"].ToString());
                                ws.GetRow(intE).CreateCell(7).SetCellValue(dt_new.Rows[i]["Throughput7"].ToString());
                                ws.GetRow(intE).CreateCell(8).SetCellValue(dt_new.Rows[i]["Throughput8"].ToString());
                                ws.GetRow(intE).CreateCell(9).SetCellValue(dt_new.Rows[i]["Throughput9"].ToString());
                                ws.GetRow(intE).CreateCell(10).SetCellValue(dt_new.Rows[i]["Throughput10"].ToString());
                                ws.GetRow(intE).CreateCell(11).SetCellValue(dt_new.Rows[i]["Throughput11"].ToString());
                                intE = intE + 1;
                            }

                            strPath1 = @"C:\inetpub\wwwroot\SIT_System_patty\WebForm\Benchmark\patty_lu\chart" + intGID.ToString() + ".jpg";
                            //strPath = dt.Rows[i]["file_path"].ToString();
                            byte[] bytes = System.IO.File.ReadAllBytes(strPath1);

                            int pictureIdx = wb.AddPicture(bytes, PictureType.JPEG);
                            var patriarch = ws.CreateDrawingPatriarch();
                            var anchor = new HSSFClientAnchor(0, 0, 255, 255, (short)0, intE, (short)9, intE + 20);
                            var pict = patriarch.CreatePicture(anchor, pictureIdx);

                            intE = intE + 22;

                            intGID = intGID + 1;
                        }
                        intValue = 0;
                    }
                }
            }

        }
        //memoryStream = new MemoryStream();
        //wb.Write(memoryStream);
        //Response.AddHeader("content-disposition", "attachment;filename=Benchmark.xls");
        //Response.BinaryWrite(memoryStream.ToArray());
        //memoryStream.Close();



        //Response.Clear();
        //Response.Buffer = true;
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Response.AddHeader("content-disposition", "attachment;filename=Benchmark.xls");
        //using (MemoryStream memoryStream = new MemoryStream())
        //{
        //wb.Write(memoryStream);

        //byte[] bytes1 = memoryStream.ToArray();
        //memoryStream.WriteTo(Response.OutputStream);
        //memoryStream.Close();
        //    Response.Flush();
        //    Response.End();
        //}


        //float[][] data = new float[3][];
        //data[0] = new float[10] { 1.3f, 2.5f, 2.1f, 3.3f, 2.8f, 3.9f, 4.3f, 3.6f, 4.2f, 3.6f };
        //data[1] = new float[12] { -1.3f, 1.5f, 0.1f, 2.3f, 4.8f, 2.9f, 6.3f, 4.6f, 6.2f, 7.6f, 5.3f, 1.2f };
        //data[2] = new float[10] { 7.3f, 3.5f, 5.1f, 9.3f, 3.8f, 1.9f, 7.3f, 5.6f, 2.2f, 6.6f };

        //for (int i = 0; i < data.Length; i++)
        //{
        //    DateTime dtt = DateTime.Now.Date;
        //    Series series = SetSeriesStyle(i);
        //    for (int j = 0; j < data[i].Length; j++)
        //    {
        //        series.Points.AddXY(dtt, data[i][j]);
        //        dtt = dtt.AddDays(1);
        //    }
        //    ct1.Series.Add(series);
        //}
       


    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;filename=Benchmark.xls");
        ////using (MemoryStream memoryStream = new MemoryStream())
        ////{
        //wb.Write(memoryStream);
        //memoryStream.Flush();
        //memoryStream.Position = 0;
        byte[] bytes = memoryStream.ToArray();
        //string strBytes = System.Text.Encoding.Default.GetString(bytes);
        //Response.WriteFile(strBytes,true);
        memoryStream.WriteTo(Response.OutputStream);
        memoryStream.Close();
        //Response.BinaryWrite(memoryStream.ToArray());
        Response.Flush();
        Response.End();
            //}

    }

    private Series SetSeriesStyle(int i)
    {
        Series series = new Series(string.Format("第{0}條數據", i + 1));
        series.ChartType = SeriesChartType.Line;
        series.BorderColor = Color.FromArgb(180, 26, 59, 105);
        series.BorderWidth = 3;
        series.ShadowColor = Color.Black;
        series.ShadowOffset = 2;
        series.IsVisibleInLegend = true;
        series.IsValueShownAsLabel = false;
        series.MarkerStyle = MarkerStyle.Circle;
        series.MarkerSize = 8;


        return series;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {


    }
}
