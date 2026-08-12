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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public partial class WebForm_ProjectDetail : System.Web.UI.Page
{
    //public static string strID;
    public string csSource;
    //public static string strUpload_Project_Kind_Cookie;
    //public static string strFun;

    public class Gantt
    {
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string desc { get; set; }
        public List<Bar> values { get; set; }
    }

    public class Bar
    {
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string label { get; set; }
        public string customClass { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strV, strUpload_Project_Kind_Cookie, strID, strCustomer, strDep, strFun;

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        txtNote.Enabled = false;

        strV = Request.QueryString["V"];

        if (strV != "R")
        {
            //strFun = Request.QueryString["Fun"];
            //dt1 = clsData.getFunction_Name(strFun);
            Session["ID"] = Request.QueryString["ID"];
            Session["Customer"] = Request.QueryString["Customer"];
            Session["Dep"] = Request.QueryString["Dep"];
            //strID = Session["ID"].ToString();


            //HttpCookie cookie_Customer = new HttpCookie("Project");
            //cookie_Customer.Values.Add("Customer", strCustomer);
            //cookie_Customer.Values.Add("Department", strDep);
            //cookie_Customer.Values.Add("ID", strID);
            //cookie_Customer.Values.Add("Fun", strFun);
            ////cookie_Customer.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_Customer);
            //strID = Request.QueryString["ID"];

            //HttpCookie cookie_Location_P = Request.Cookies["Location"];
            //strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

            //HttpCookie cookie_Upload_Project_Kind = Request.Cookies["Upload_Project_Kind"];
            //strUpload_Project_Kind_Cookie = Server.UrlDecode(cookie_Upload_Project_Kind.Value);
        }
        if ((Session["ID"].ToString() != null) && (Session["ID"].ToString() != ""))
        {
            getProject();
            getGantt();
        }

        //if (strLocation_P != lblLocation.Text)
        //{
        //    lblAdd.Visible = false;
        //    lblModify.Visible = false;
        //    lblDel.Visible = false;

        //    HttpCookie cookie_Authority = Request.Cookies["Authority"];
        //    cookie_Authority.Expires = DateTime.Now.AddDays(1);
        //    Response.Cookies.Add(cookie_Authority);
        //}
        //else
        //{
        HttpCookie cookie_Authority = Request.Cookies["Authority"];
        //cookie_Authority.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_Authority);

        string strWrite;
        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            lblAdd.Visible = false;
            lblModify.Visible = false;
            //lblDel.Visible = false;
        }
        //}
        showEnd();

        DataTable dt;

        //主管用1，工程師用0
        dt = clsData.getProjectCase(Session["ID"].ToString(), "1", "");
        string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_projectcase.txt";
        dataTableToText(dt, 2, strPath1);
    }

    private void showEnd()
    {
        string strAssign;

        if (lblStatus.Text == "Close")
        {
            endT.Visible = true;

            DataTable dt = clsData.UploadInfoData_Value(lblDepartment.Text);
            if (dt.Rows.Count > 0)
                lblLeaderAppLication.Text = dt.Rows[0]["Value"].ToString() + "  同意";
            else
                lblLeaderAppLication.Text = "";

            dt = clsData.UploadLeader("1", "", "");
            if (dt.Rows.Count > 0)
                lblLeaderAccepted.Text = dt.Rows[0]["Name_En"].ToString() + "  審核通過";
            else
                lblLeaderAccepted.Text = "";

            //dt = clsData.UploadLeader("1", "", "");
            lblLeaderAccepted.Text = lblEngineer.Text + "  任務指派完成";

            dt = clsData.getProjectCase1(Session["ID"].ToString());

            strAssign = "";
            for (int intI = 0; intI < dt.Rows.Count; intI++)
            {
                if (strAssign == "")
                    strAssign = dt.Rows[intI]["Assign"].ToString();
                else
                    strAssign = strAssign + "," + dt.Rows[intI]["Assign"].ToString();
            }
            if (strAssign == "")
                lblEngineer1.Text = "";
            else
                lblEngineer1.Text = strAssign + "  執行任務";

            lblEnd.Text = "任務完成";
        }
        else
            endT.Visible = false;

    }

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + ",\r\n";
                }
                else
                    strSQLFile += @"""" + dt.Rows[i][j].ToString().Trim() + @"""" + "\r\n";
            }
            if (i != dt.Rows.Count - 1)
                strSQLFile += "],\r\n";
            else
                strSQLFile += "]\r\n";
        }
        strSQLFile += "]" + "\r\n" + "}";
        using (StreamWriter sw = new StreamWriter(DBPath))   //小寫TXT     
        {
            sw.Write(strSQLFile);
        }
    }
    #endregion

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery(true);
    }
    #endregion

    #region getProject (取得Project資訊)
    private void getProject()
    {
        //string strID;
        string strTestCase, strDate1;
        string strItem = "";
        DateTime dt1;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];


        DataTable dt = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");

        lblID.Text = dt.Rows[0]["Name"].ToString();

        //HttpCookie cookie_ProjectName = new HttpCookie("ProjectName");
        //cookie_ProjectName.Value = Server.UrlEncode(dt.Rows[0]["Name"].ToString());
        ////cookie_ProjectName.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(cookie_ProjectName);
        Session["ProjectName"] = dt.Rows[0]["Name"].ToString();

        //HttpCookie cookie_ProjectKind = new HttpCookie("ProjectKind");
        //cookie_ProjectKind.Value = Server.UrlEncode(dt.Rows[0]["Kind"].ToString());
        ////cookie_ProjectKind.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(cookie_ProjectKind);
        Session["ProjectKind"] = dt.Rows[0]["Kind"].ToString();

        lblName.Text = dt.Rows[0]["A_Name"].ToString();
        lblDepartment.Text = dt.Rows[0]["A_Department"].ToString();

        if ((dt.Rows[0]["A_Department2"].ToString() != null) && (dt.Rows[0]["A_Department2"].ToString() != ""))
            lblDepartment2.Text = dt.Rows[0]["A_Department2"].ToString();
        else
            lblDepartment2.Text = "";

        lblExt.Text = dt.Rows[0]["A_Ext"].ToString();
        lblMail.Text = dt.Rows[0]["A_mail"].ToString();
        lblCustomer.Text = dt.Rows[0]["Customer"].ToString();
        lblPM.Text = dt.Rows[0]["PM"].ToString();
        lblSW.Text = dt.Rows[0]["SW_Engineer"].ToString();
        lblHW.Text = dt.Rows[0]["HW_Engineer"].ToString();
        lblMechanical.Text = dt.Rows[0]["Mechanical_Engineer"].ToString();
        lblDSP.Text = dt.Rows[0]["DSP_Model"].ToString();
        lblFW.Text = dt.Rows[0]["FW_Version"].ToString();
        lblWireless.Text = dt.Rows[0]["WirelessDrive"].ToString();
        lblProduct.Text = dt.Rows[0]["Customer_Product_Name"].ToString();
        lblNPI.Text = dt.Rows[0]["NPI"].ToString();
        lblHW_VR.Text = dt.Rows[0]["PCB_Version"].ToString();
        lblChipset.Text = dt.Rows[0]["Chipset"].ToString();
        lblMAC.Text = dt.Rows[0]["Sample_Mac_address"].ToString();
        lblUtility.Text = dt.Rows[0]["Utility_Version"].ToString();
        lblLocation.Text = dt.Rows[0]["Accepted_Team"].ToString();
        lblRelated.Text = dt.Rows[0]["Related"].ToString();
        lblJira.Text = dt.Rows[0]["Jira"].ToString();
        lblStatus.Text = dt.Rows[0]["Status"].ToString();
        lblDQA.Text = dt.Rows[0]["DQA"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["Sample_Ready_Date"].ToString());
        strDate1 = dt1.ToString("yyyy/MM/dd");
        if (strDate1 == "1900/01/01")
            lblReady.Text = "";
        else
            lblReady.Text = strDate1;

        lblUtility.Text = dt.Rows[0]["Utility_Version"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["Start_Date"].ToString());
        strDate1 = dt1.ToString("yyyy/MM/dd");
        if (strDate1 == "1900/01/01")
            lblStart.Text = "";
        else
            lblStart.Text = strDate1;

        dt1 = Convert.ToDateTime(dt.Rows[0]["End_Date"].ToString());
        strDate1 = dt1.ToString("yyyy/MM/dd");
        if (strDate1 == "1900/01/01")
            lblExpect.Text = "";
        else
            lblExpect.Text = strDate1;

        txtNoteP.Text = dt.Rows[0]["Note"].ToString();
        txtNote.Text = dt.Rows[0]["Explain"].ToString();
        lblEngineer.Text = dt.Rows[0]["Assign"].ToString();
        lblProgress.Text = dt.Rows[0]["Progress"].ToString();

        dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), "驗証申請");
        //dt = clsData.UploadProjectFileQuery(strID, strUpload_Project_Kind_Cookie);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    #region GvQuery
    private void GvQuery(Boolean IsPage)
    {
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        if (IsPage != true)
            this.gvwMain.PageIndex = 0;
        DataTable dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), "驗証申請");
        this.gvwMain.DataSource = dt;
        this.DataBind();

    }
    #endregion

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        //string strID1 = "";
        //Server.Transfer("~/WebForm/AddCase.aspx?ID=" + strID);
        Server.Transfer("~/WebForm/AddCase.aspx");
    }

    protected void lbtnModify_Click(object sender, EventArgs e)
    {
        //string strID1 = "";
        //Response.Redirect("~/WebForm/ProjectEdit.aspx?ID=" + strID + "&Fun=" + strFun);
        Response.Redirect("~/WebForm/ProjectEdit.aspx?A=0");
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        bool bTF = false;
        //string strUpload_Project_Kind_Cookie;

        //HttpCookie cookie_Upload_Project_Kind = Request.Cookies["Upload_Project_Kind"];
        //strUpload_Project_Kind_Cookie = Server.UrlDecode(cookie_Upload_Project_Kind.Value);

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        if (clsTransaction.DelApplication(Session["ID"].ToString()) == true)
        {
            if (clsTransaction.DelProjectCaseData(Session["ID"].ToString(), "", "1") == true)
            {
                if (clsTransaction.DelApplication_File(Session["ID"].ToString()) == true)
                {
                    if (clsTransaction.DelUploadFilesCase("", Session["ID"].ToString(), "", "2") == true)
                    {
                        string strPath;

                        if (Session["Upload_Project_Kind"].ToString() == "驗証申請")
                        {
                            strPath = @"D:\Test Report\" + lblDepartment.Text + @"\" + lblID.Text + @"\" + lblCustomer.Text + @"\" + lblNPI.Text;
                        }
                        else
                        {
                            strPath = @"D:\" + Session["Upload_Project_Kind"].ToString() + @"\" + lblID.Text;
                        }
                        //strPath = @"E:\Test Report\D200\RTA1445VW\D49\EV\Interoperability" + strID;
                        //strPath = @"D:\Test Report\" + lblDepartment.Text + @"\" + lblID.Text + @"\" + lblCustomer.Text + @"\" + lblNPI.Text;
                        //System.IO.Directory.Delete(strPath);
                        if (Directory.Exists(strPath) == true)
                            Directory.Delete(strPath, true);

                        bTF = true;
                    }
                }
            }
        }





        //bool bTF = false;
        ////string strName = ((Label)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("lblName")).Text;
        //if (clsTransaction.DelApplication(strID) == true)
        //{
        //    if (clsTransaction.DelProjectCaseData(strID, "", "1") == true)
        //    {
        //        if (clsTransaction.DelApplication_Wireless(strID) == true)
        //        {
        //            if (clsTransaction.DelApplication_WiFi(strID) == true)
        //            {
        //                if (clsTransaction.DelApplication_USB(strID) == true)
        //                {
        //                    if (clsTransaction.DelApplication_LTE(strID) == true)
        //                    {
        //                        if (clsTransaction.DelApplication_DSL(strID) == true)
        //                        {
        //                            if (clsTransaction.DelApplication_Bluetooth(strID) == true)
        //                            {
        //                                if (clsTransaction.DelApplication_File(strID) == true)
        //                                {
        //                                    if (clsTransaction.DelUploadFilesCase("",strID,"","2") == true)
        //                                    {
        //                                        //if (clsTransaction.DelApplication_ProjectCase(strID) == true)
        //                                        //{
        //                                            //File.Delete(path);
        //                                            //((GridView)sender).SelectedIndex = -1;
        //                                            //((GridView)sender).EditIndex = -1;
        //                                            //GvQuery(strNumber1);

        //                                            string strPath;

        //                                            //strPath = @"E:\Test Report\D200\RTA1445VW\D49\EV\Interoperability" + strID;
        //                                            strPath = @"D:\Test Report\" + lblDepartment.Text + @"\" + lblID.Text + @"\" + lblCustomer.Text + @"\" + lblNPI.Text;
        //                                            //System.IO.Directory.Delete(strPath);
        //                                            if (Directory.Exists(strPath) == true)
        //                                                Directory.Delete(strPath, true);

        //                                            bTF = true;
        //                                        //}
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        if (bTF == false)
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        else
        {
            clsMsg.AlertMessage("刪除成功！", this.Page);
            Response.Redirect("~/WebForm/ProjectView.aspx?Fun=" + Session["Fun"].ToString());
            //Response.Redirect("~/WebForm/ProjectView.aspx");
        }

        //clsTransaction.DelProjectCaseData(strID, "", "1");


        //if (clsTransaction.DelProjectData(strID) == true)
        //{
        //    Response.Redirect("~/WebForm/ProjectView.aspx");
        //}
        //else
        //    clsMsg.AlertMessage("刪除失敗！", this.Page);
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/WebForm/ProjectView.aspx");
        Response.Redirect("~/WebForm/ProjectView.aspx?Fun=" + Session["Fun"].ToString());
    }
    protected void butExcel_Click(object sender, EventArgs e)
    {
        string strSheet;
        string strCol, strCol1;

        var workbook = new XLWorkbook();
        strSheet = lblLocation.Text + lblID.Text;
        var ws = workbook.Worksheets.Add(strSheet);
        //ws.Cell(1, 1).Value = "Test";
        //ws.Cell(1, 2).Value = "Test1";
        //ws.Cell(2, 1).Value = "Test2";
        ws.Cell(1, 1).Value = "申請人";
        ws.Cell(1, 2).Value = lblName.Text;
        ws.Cell(1, 3).Value = "部門";
        ws.Cell(1, 4).Value = lblDepartment.Text;

        ws.Cell(2, 1).Value = "分機";
        ws.Cell(2, 2).Value = lblExt.Text;
        ws.Cell(2, 3).Value = "Mail";
        ws.Cell(2, 4).Value = lblMail.Text;

        ws.Cell(3, 1).Value = "客戶";
        ws.Cell(3, 2).Value = lblCustomer.Text;
        ws.Cell(3, 3).Value = "PM Sales";
        ws.Cell(3, 4).Value = lblPM.Text;

        ws.Cell(4, 1).Value = "S/W Engineer";
        ws.Cell(4, 2).Value = lblSW.Text;
        ws.Cell(4, 3).Value = "H/W Engineer";
        ws.Cell(4, 4).Value = lblHW.Text;

        ws.Cell(5, 1).Value = "Mechanical Engineer";
        ws.Cell(5, 2).Value = lblMechanical.Text;
        ws.Cell(5, 3).Value = "DSP Model";
        ws.Cell(5, 4).Value = lblDSP.Text;

        ws.Cell(6, 1).Value = "F/W Version";
        ws.Cell(6, 2).Value = lblFW.Text;
        ws.Cell(6, 3).Value = "Wireless Drive";
        ws.Cell(6, 4).Value = lblWireless.Text;

        ws.Cell(7, 1).Value = "Customer's Product Name";
        ws.Cell(7, 2).Value = lblProduct.Text;
        ws.Cell(7, 3).Value = "NPI";
        ws.Cell(7, 4).Value = lblNPI.Text;

        ws.Cell(8, 1).Value = "H/W Version";
        ws.Cell(8, 2).Value = lblHW_VR.Text;
        ws.Cell(8, 3).Value = "Chipset";
        ws.Cell(8, 4).Value = lblChipset.Text;

        ws.Cell(9, 1).Value = "Sample MAC Address";
        ws.Cell(9, 2).Value = lblMAC.Text;
        ws.Cell(9, 3).Value = "Utility Version";
        ws.Cell(9, 4).Value = lblUtility.Text;

        ws.Cell(10, 1).Value = "開始日期";
        ws.Cell(10, 2).Value = lblStart.Text;
        ws.Cell(10, 3).Value = "預計完成日";
        ws.Cell(10, 4).Value = lblExpect.Text;

        ws.Cell(11, 1).Value = "預計Sample Ready日期";
        ws.Cell(11, 2).Value = lblReady.Text;

        ws.Cell(12, 1).Value = "指派工程師";
        ws.Cell(12, 2).Value = lblEngineer.Text;
        ws.Cell(12, 3).Value = "進度";
        ws.Cell(12, 4).Value = lblProgress.Text;

        ws.Cell(13, 1).Value = "備註";
        ws.Cell("B13").Value = txtNote.Text;  //合併儲存格

        ws.Cell("B13").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        ws.Cell("B13").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
        ws.Range("B13:D13").Merge();

        ws.Rows(13, 2).AdjustToContents();

        var row1 = ws.Row(13);
        row1.Height = 200;

        //ws.Columns("A:F").Width = 26;



        int intCount = 15;
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        DataTable dt = clsData.getProjectCase(Session["ID"].ToString(), "1", "");
        DataTable dt1, dt2;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ws.Cell(intCount, 1).Value = dt.Rows[i]["Kind"].ToString().Trim();
            ws.Cell(intCount, 1).Style.Font.Bold = true;
            ws.Cell(intCount, 1).Style.Font.SetFontSize(16);
            dt1 = clsData.getProjectItem(Session["ID"].ToString(), dt.Rows[i]["Kind"].ToString().Trim(), "", "1", "Open");
            intCount++;
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                ws.Cell(intCount, 1).Value = dt1.Rows[j]["Name"].ToString().Trim();
                ws.Cell(intCount, 1).Style.Font.Bold = true;
                intCount++;
                dt2 = clsData.UploadProjectTask(Session["ID"].ToString(), dt1.Rows[j]["ID"].ToString().Trim());
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
                string strDate;
                DateTime dTime;

                dTime = Convert.ToDateTime(dt2.Rows[0]["start_date1"].ToString());
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    strStartDate = strDate;

                dTime = Convert.ToDateTime(dt2.Rows[0]["end_date1"].ToString());
                strDate = dTime.ToString("yyyy/MM/dd");
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

    private static string ConvertToLetter(int iCol)
    {
        string strReturn = "";
        int iQuotient = iCol / 26;
        int iRemainder = iCol % 26;

        if (iRemainder == 0)
            iQuotient--;

        if (iQuotient > 0)
            strReturn = Convert.ToChar(64 + iQuotient).ToString();

        if (iRemainder == 0)
            strReturn += "Z";
        else
            strReturn += Convert.ToChar(64 + iRemainder).ToString();

        return strReturn;


    }

    private static string DateTimeToSecond(DateTime dt)
    {
        dt = dt.AddDays(-1);
        int timeStamp = Convert.ToInt32(dt.AddHours(8).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        return String.Format("/Date({0}000)/", timeStamp);
    }

    private void getGantt()
    {
        string strProjectName = null;
        string strProjectName1 = null;
        //int intI = 0;
        int intX, intY;
        DateTime dtStart, dtEnd;
        Item item;
        Bar bar;
        Gantt gantt = new Gantt();
        gantt.items = new List<Item>();

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        DataTable dt = clsScheduler.UploadInfoProject("1", Session["ID"].ToString(), "");
        //DataTable dt = clsData.UploadProjectQuery(strID, "Project");

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {

            //if (intI == 0)
            //{
            intX = 0;
            intY = 0;
            while (intX == 0)
            {
                strProjectName = dt.Rows[intI]["projectname"].ToString();

                //專案一
                item = new Item();
                if (intY == 0)
                    item.name = strProjectName;
                else
                    item.name = "";
                item.desc = dt.Rows[intI]["name"].ToString();
                item.values = new List<Bar>();
                gantt.items.Add(item);

                dtStart = Convert.ToDateTime(dt.Rows[intI]["start_date"].ToString());
                dtEnd = Convert.ToDateTime(dt.Rows[intI]["end_date"].ToString());
                bar = new Bar();
                bar.id = dt.Rows[intI]["projectid"].ToString();
                bar.from = DateTimeToSecond(dtStart);
                bar.to = DateTimeToSecond(dtEnd);
                //bar.label = "Assign : " + dt.Rows[intI]["assign"].ToString() + ", Note : " + dt.Rows[intI]["explain_case"].ToString();
                bar.customClass = "Assign : " + dt.Rows[intI]["assign"].ToString() + ", Note : " + dt.Rows[intI]["explain_case"].ToString();
                item.values.Add(bar);


                intI = intI + 1;

                if (intI < dt.Rows.Count)
                {
                    strProjectName1 = dt.Rows[intI]["projectname"].ToString();

                    intY = 1;
                    if (strProjectName1 != strProjectName)
                    {
                        intI = intI - 1;
                        intX = 1;
                    }
                }
                else
                    intX = 1;


            }
            //}
            //else
            //{
            //    if (strProjectName == strProjectName1)
            //    {
            //    }
            //}
        }


        ////專案一
        //item = new Item();
        //item.name = "專案一";
        //item.desc = "人員一";
        //item.values = new List<Bar>();
        //gantt.items.Add(item);

        //bar = new Bar();
        //bar.id = "t111";
        //bar.from = DateTimeToSecond(new DateTime(2013, 1, 2));
        //bar.to = DateTimeToSecond(new DateTime(2013, 1, 10));
        //bar.label = "設計階段";
        //bar.customClass = "123";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t112";
        //bar.from = DateTimeToSecond(new DateTime(2013, 1, 11));
        //bar.to = DateTimeToSecond(new DateTime(2013, 1, 25));
        //bar.label = "開發階段";
        //bar.customClass = "222";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t113";
        //bar.from = DateTimeToSecond(new DateTime(2013, 2, 1));
        //bar.to = DateTimeToSecond(new DateTime(2013, 2, 10));
        //bar.label = "測試階段";
        //bar.customClass = "ganttBlue";
        //item.values.Add(bar);

        //item = new Item();
        //item.name = String.Empty;
        //item.desc = "人員二";
        //item.values = new List<Bar>();
        //gantt.items.Add(item);

        //bar = new Bar();
        //bar.id = "t121";
        //bar.from = DateTimeToSecond(new DateTime(2013, 1, 6));
        //bar.to = DateTimeToSecond(new DateTime(2013, 1, 10));
        //bar.label = "設計階段";
        //bar.customClass = "ganttRed";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t122";
        //bar.from = DateTimeToSecond(new DateTime(2013, 1, 15));
        //bar.to = DateTimeToSecond(new DateTime(2013, 1, 30));
        //bar.label = "開發階段";
        //bar.customClass = "ganttGreen";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t123";
        //bar.from = DateTimeToSecond(new DateTime(2013, 2, 11));
        //bar.to = DateTimeToSecond(new DateTime(2013, 2, 20));
        //bar.label = "測試階段";
        //bar.customClass = "ganttBlue";
        //item.values.Add(bar);

        ////專案二
        //item = new Item();
        //item.name = "專案二";
        //item.desc = "人員一";
        //item.values = new List<Bar>();
        //gantt.items.Add(item);

        //bar = new Bar();
        //bar.id = "t211";
        //bar.from = DateTimeToSecond(new DateTime(2013, 2, 2));
        //bar.to = DateTimeToSecond(new DateTime(2013, 3, 1));
        //bar.label = "設計階段";
        //bar.customClass = "ganttRed";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t212";
        //bar.from = DateTimeToSecond(new DateTime(2013, 3, 11));
        //bar.to = DateTimeToSecond(new DateTime(2013, 3, 25));
        //bar.label = "開發階段";
        //bar.customClass = "ganttGreen";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t213";
        //bar.from = DateTimeToSecond(new DateTime(2013, 4, 1));
        //bar.to = DateTimeToSecond(new DateTime(2013, 4, 10));
        //bar.label = "測試階段";
        //bar.customClass = "ganttBlue";
        //item.values.Add(bar);

        //item = new Item();
        //item.name = String.Empty;
        //item.desc = "人員二";
        //item.values = new List<Bar>();
        //gantt.items.Add(item);

        //bar = new Bar();
        //bar.id = "t221";
        //bar.from = DateTimeToSecond(new DateTime(2013, 3, 6));
        //bar.to = DateTimeToSecond(new DateTime(2013, 3, 10));
        //bar.label = "設計階段";
        //bar.customClass = "ganttRed";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t222";
        //bar.from = DateTimeToSecond(new DateTime(2013, 3, 20));
        //bar.to = DateTimeToSecond(new DateTime(2013, 3, 30));
        //bar.label = "開發階段";
        //bar.customClass = "ganttGreen";
        //item.values.Add(bar);

        //bar = new Bar();
        //bar.id = "t223";
        //bar.from = DateTimeToSecond(new DateTime(2013, 4, 11));
        //bar.to = DateTimeToSecond(new DateTime(2013, 5, 30));
        //bar.label = "測試階段";
        //bar.customClass = "ganttBlue";
        //item.values.Add(bar);

        //item = new Item();
        //item.name = String.Empty;
        //item.desc = "人員三";
        //item.values = new List<Bar>();
        //gantt.items.Add(item);

        //bar = new Bar();
        //bar.id = "t231";
        //bar.from = DateTimeToSecond(new DateTime(2013, 1, 8));
        //bar.to = DateTimeToSecond(new DateTime(2013, 5, 20));
        //bar.label = "協助開發";
        //bar.customClass = "ganttOrange";
        //item.values.Add(bar);

        string json = JsonConvert.SerializeObject(gantt.items);
        //Response.Write(json);
        csSource = json;
    }
}
