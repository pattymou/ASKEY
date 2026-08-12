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
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Text;

public partial class WebForm_ProjectTask : System.Web.UI.Page
{
    //public string strID;
    //public static string strApparatusID;
    //public static string strName;
    //public static string strKind;
    //public static string strProjectCaseID;
    //public static string strCase;
    //public static string strAuthority;
    //public static string strWrite;
    //public static string strProjectKind;
    //public static string strFun;
    //public static string strLocation_P;
    //public static string strAuthority1;
    //ProjectInfo projectinfo = new ProjectInfo(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        //strID = "";

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            Session["FileN"] = "";

            if (Session["Fun"].ToString() == "9")
            {
                Name1.Visible = false;
                //lblPU.Visible = false;
                //lblModelName.Visible = false;
                //lblPU1.Visible = false;
                //lblModelName1.Visible = false;
            }
            else
            {
                Name1.Visible = true;
                //lblPU.Visible = true;
                //lblModelName.Visible = true;
                //lblPU1.Visible = true;
                //lblModelName1.Visible = true;
            }
            //string strItemName, strProjectCaseID;

            //HttpCookie cookie_Customer = Request.Cookies["Project"];
            //string strID = cookie_Customer.Values["ID"];

            //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
            //string strName = Server.UrlDecode(cookie_CaseName.Value);
            //strFun = cookie_Customer.Values["Fun"];

            //Session["ProjectID"] = cookie_Customer.Values["ID"];
            Session["CaseID"] = Request.QueryString["CaseID"];
            Session["ItemName"] = Server.UrlDecode(Request.QueryString["Value"]);

            //strID = Request.QueryString["ID"];
            //strProjectCaseID = Request.QueryString["CaseID"];
            //strFun = Request.QueryString["Fun"];
            //strItemName = Request.QueryString["Value"];
            ////strKind = Request.QueryString["Kind"];
            ////strCase = Request.QueryString["Case"];

            //HttpCookie cookie_ItemName = new HttpCookie("ItemName");
            //cookie_ItemName.Value = Server.UrlEncode(strItemName);
            ////cookie_CaseName.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_ItemName);

            //HttpCookie cookie_CaseID = new HttpCookie("CaseID");
            //cookie_CaseID.Value = Server.UrlEncode(strProjectCaseID);
            ////cookie_CaseName.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_CaseID);


            //HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
            //cookie_Upload_Kind.Value = Server.UrlEncode("TestReport");
            ////cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_Upload_Kind);
            Session["Upload_Kind"] = "TestReport";

            //HttpCookie cookie_Location_P = Request.Cookies["Location"];
            //string strLocation_P = Server.UrlDecode(cookie_Location_P.Value);


            //HttpCookie cookie_ProjectKind = Request.Cookies["ProjectKind"];
            //string strProjectKind = Server.UrlDecode(cookie_ProjectKind.Value);
            //clsParameter.strUpload_Kind = "TestReport";

            //string strAuthority;

            //HttpCookie cookie_DetailName = new HttpCookie("DetailName");
            //cookie_DetailName.Value = Server.UrlEncode(strName);
            ////cookie_DetailName.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_DetailName);

            HttpCookie cookie_Authority = Request.Cookies["Authority"];
            string strAuthority = Server.UrlDecode(cookie_Authority.Value);

            HttpCookie cookie_Write = Request.Cookies["Write"];
            string strWrite = Server.UrlDecode(cookie_Write.Value);

            if (strAuthority == "False")
            {
                lblAdd.Visible = false;
                //lblDel.Visible = false;
                //butOK.Visible = false;
            }
            else
            {
                if (strWrite == "N")
                {
                    lblAdd.Visible = false;
                    //lblDel.Visible = false;
                    //butOK.Visible = false;
                }
            }
            //strAuthority1 = "True";
            //strID = "20141210102633";
            //strName = "ADSL TR-067 Interoperability Test";
            //strKind = "DSL Interoperability";
            //clsParameter.strCustomer = "ASKEY";
            //clsParameter.strDepartment = "D200";

            getProjectTask();

            string strCaseID;

            DataTable dt1 = clsData.UploadProjectCaseID(Session["ID"].ToString(), Session["CaseName"].ToString(), "0", Session["ItemName"].ToString());
            if (dt1.Rows.Count != 0)
            {
                strCaseID = Session["ID"].ToString() + "-" + dt1.Rows[0]["ID"].ToString();

                DataTable dt2 = clsData.UploadApparatusProjectListQuery(strCaseID);
                if (dt2.Rows.Count != 0)
                {
                    listRight.DataSource = dt2;
                    listRight.DataBind();
                }
            }


        }
    }

    protected void rdoReport_CheckedChanged(object sender, EventArgs e)
    {
        HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
        cookie_Upload_Kind.Value = Server.UrlEncode("TestReport");
        //cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_Upload_Kind);
    }

    protected void rdoOther_CheckedChanged(object sender, EventArgs e)
    {
        HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
        cookie_Upload_Kind.Value = Server.UrlEncode("Other");
        //cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_Upload_Kind);
    }


    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName, strPath;

        strName = ((HyperLink)this.gvwMain.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        strPath = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblGVSeq")).Text;
        string path = strPath + "\\" + strName;
        if (clsTransaction.DelUploadFilesCase1(strName, Session["ID"].ToString(), "", "0") == true)
        {
            if (File.Exists(path))
                File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            GvQuery();
            //=================================debbie SIT Benchmark 20180503====================================
            if (Session["ProjectKind"].ToString() == "驗証申請")
            {
                if (strName.IndexOf("-LOS") > 0)
                {
                    if (clsBM.DelLosDataToSQL(Session["ID"].ToString()) == true)
                    {
                        if (clsBM.DelLosAngleToSQL(Session["ID"].ToString()) == true)
                        {
                            if (clsBM.DelInformationToSQL(Session["ID"].ToString()) == true)
                            {

                                clsMsg.AlertMessage("刪除成功！", this.Page);
                            }
                        }
                    }
                    else
                        clsMsg.AlertMessage("資料刪除失敗！", this.Page);
                }
                else if (strName.IndexOf("-Mesh") > 0)
                {
                    if (clsBM.DelMeshDataToSQL(Session["ID"].ToString()) == true)
                    {
                        if (clsBM.DelMeshInformationToSQL(Session["ID"].ToString()) == true)
                        {
                            clsMsg.AlertMessage("刪除成功！", this.Page);
                        }
                    }
                    else
                        clsMsg.AlertMessage("資料刪除失敗！", this.Page);
                }
                else if (strName.IndexOf("-Indoor") > 0)
                {
                    if (clsBM.DelIndoorDataToSQL(Session["ID"].ToString()) == true)
                    {
                        if (clsBM.DelIndoorInformationToSQL(Session["ID"].ToString()) == true)
                        {
                            clsMsg.AlertMessage("刪除成功！", this.Page);
                        }
                    }
                    else
                        clsMsg.AlertMessage("資料刪除失敗！", this.Page);
                }
                else if (strName.IndexOf("WL-Throughput") > 0)
                {
                    if (clsBM.DelOTADataToSQL(Session["ID"].ToString()) == true)
                    {
                        if (clsBM.DelOTAInformationToSQL(Session["ID"].ToString()) == true)
                        {
                            clsMsg.AlertMessage("刪除成功！", this.Page);
                        }
                    }
                    else
                        clsMsg.AlertMessage("資料刪除失敗！", this.Page);
                }
            }
            else
                //=================================debbie SIT Benchmark 20180503====================================
                clsMsg.AlertMessage("刪除成功！", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗！", this.Page);
        }
    }
    #endregion

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    private void GvQuery()
    {
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseID = Request.Cookies["CaseID"];
        //string strProjectCaseID = Server.UrlDecode(cookie_CaseID.Value);

        DataTable dt = clsData.UploadProjectCaseFileQuery(Session["CaseID"].ToString(), Session["ID"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    private void getProjectTask()
    {
        string strDate;
        DateTime dTime;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        //HttpCookie cookie_ItemName = Request.Cookies["ItemName"];
        //string strItemName = Server.UrlDecode(cookie_ItemName.Value);

        //HttpCookie cookie_CaseID = Request.Cookies["CaseID"];
        //string strProjectCaseID = Server.UrlDecode(cookie_CaseID.Value);

        if (Session["CaseName"].ToString().IndexOf("BQB", 0) != -1)
            Session["CaseName"] = "BQB Review(2.0 Version & EDR Review)";

        DataTable dt = clsData.UploadProjectTask(Session["ID"].ToString(), Session["CaseID"].ToString());
        //DataTable dt = clsData.UploadProjectTask(strID, strKind, strName);
        //strProjectCaseID = dt.Rows[0]["id"].ToString();
        lblCaseID.Text = Session["CaseID"].ToString();
        //lblName.Text = Session["ItemName"].ToString();
        lblName.Text = dt.Rows[0]["Name"].ToString();

        if (dt.Rows.Count != 0)
        {
            lblAssign.Text = dt.Rows[0]["assign"].ToString();
            dTime = Convert.ToDateTime(dt.Rows[0]["start_date1"].ToString());
            strDate = dTime.ToString("yyyy/MM/dd");

            if (strDate != "1900/01/01")
                lblStartdate.Text = strDate;



            dTime = Convert.ToDateTime(dt.Rows[0]["end_date1"].ToString());
            strDate = dTime.ToString("yyyy/MM/dd");
            if (strDate != "1900/01/01")
                lblEnddate.Text = strDate;

            lblResult.Text = dt.Rows[0]["result"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();
            lblProgress.Text = dt.Rows[0]["Progress"].ToString();
            txtNote.Text = dt.Rows[0]["explain_case"].ToString();
            lblPU.Text = dt.Rows[0]["Sub_PU"].ToString();
            lblModelName.Text = dt.Rows[0]["Model_Name"].ToString();
            lblLab.Text = dt.Rows[0]["Lab"].ToString();
            lblQuoted.Text = dt.Rows[0]["Quoted"].ToString();
            lblReimburse.Text = dt.Rows[0]["Reimburse"].ToString();

        }
        dt = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");

        //HttpCookie cookie_Customer = new HttpCookie("Project");
        //cookie_Customer.Values.Add("Customer", dt.Rows[0]["Customer"].ToString());
        //cookie_Customer.Values.Add("Department", dt.Rows[0]["A_Department"].ToString());
        //cookie_Customer.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(cookie_Customer);
        //clsParameter.strCustomer = dt.Rows[0]["Customer"].ToString();
        //clsParameter.strDepartment = dt.Rows[0]["A_Department"].ToString();
        LTE.Visible = false;
        if (Session["CaseName"].ToString().IndexOf("LTE", 0) != -1)
        {
            if (lblName.Text.IndexOf("TRP/TIS", 0) != -1)
                LTE.Visible = true;
            else
                LTE.Visible = false;
        }



        GvQuery();

    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strToday;


        if ((Session["EmpNo"] == null) || (Session["CaseID"] == null) || (Session["ID"] == null))
            Response.Redirect("~/Default.aspx");

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();
        }

        if ((strFile != null) || (strFile != ""))
        {
            string[] sArray = strFile.Split(',');
            foreach (string i in sArray)
            {
                if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                {
                    intFile = i.LastIndexOf('\\');
                    strPath = i.Substring(0, intFile);
                    strFile_Name = i.Substring(intFile + 1);

                    //lblResult.Text = Session["CaseID"].ToString() + "," + Session["ID"].ToString() + "," + strFile_Name + "," + strPath + "," + strToday + "," + Session["EmpName"].ToString();
                    DataTable dt = clsData.UploadAttachmenFileCase(strFile_Name, strPath);
                    if (dt.Rows.Count > 0)
                    {
                        clsMsg.AlertMessage("檔案已存在...請確認後重試!", this.Page);
                    }
                    else if (clsTransaction.InsertUploadFile_Case(Session["CaseID"].ToString(), Session["ID"].ToString(), strFile_Name, strPath, strToday, Session["EmpName"].ToString()) == true)
                    {
                        Session["FileN"] = "";
                        //if (intAdd == 0)
                        //if (Session["ProjectKind"].ToString() == "驗証申請")
                        //{
                        //if (strPath.IndexOf("TestReport") > 0)
                        //=================================debbie SIT Benchmark 20180503====================================
                        if (Session["ProjectKind"].ToString() == "驗証申請")
                        {
                            if ((strFile_Name.IndexOf(".xls") > 0) && (strFile_Name.IndexOf("-TR-") > 0))
                            {
                                if (strFile_Name.IndexOf("-LOS") > 0)
                                {
                                    DataTable dt_InfoID = clsBM.UploadLosInfoID(Session["ID"].ToString());
                                    if (dt_InfoID.Rows.Count != 0)
                                    {
                                        clsMsg.AlertMessage("Excel檔案已存在...請將此檔案及原始檔案刪除後重試!", this.Page);
                                    }
                                    else
                                    {
                                        ConvertToSQL_Los(strPath, strFile_Name, strToday);
                                    }
                                }
                                else if (strFile_Name.IndexOf("-Mesh") > 0)
                                {
                                    DataTable dt_InfoID = clsBM.UploadMeshInfoID(Session["ID"].ToString());
                                    if (dt_InfoID.Rows.Count != 0)
                                    {
                                        clsMsg.AlertMessage("Excel檔案已存在...請將此檔案及原始檔案刪除後重試!", this.Page);
                                    }
                                    else
                                    {
                                        ConvertToSQL_MeshRvR(strPath, strFile_Name, strToday);
                                    }
                                }
                                else if (strFile_Name.IndexOf("-Indoor") > 0)
                                {
                                    if (strFile_Name.IndexOf("NR-Indoor") <= 0)
                                    {
                                        DataTable dt_InfoID = clsBM.UploadIndoorInfoID(Session["ID"].ToString());
                                        if (dt_InfoID.Rows.Count != 0)
                                        {
                                            clsMsg.AlertMessage("Excel檔案已存在...請將此檔案及原始檔案刪除後重試!", this.Page);
                                        }
                                        else
                                        {
                                            ConvertToSQL_Indoor(strPath, strFile_Name, strToday);
                                        }
                                    }

                                }
                                else if (strFile_Name.IndexOf("WL-Throughput") > 0)
                                {
                                    DataTable dt_InfoID = clsBM.UploadOTAInfoID(Session["ID"].ToString());
                                    if (dt_InfoID.Rows.Count != 0)
                                    {
                                        clsMsg.AlertMessage("Excel檔案已存在...請將此檔案及原始檔案刪除後重試!", this.Page);
                                    }
                                    else
                                    {
                                        ConvertToSQL_OTA(strPath, strFile_Name, strToday);
                                    }
                                }
                            }
                        }
                        //=================================debbie SIT Benchmark 20180503====================================
                        MailData(strFile_Name);

                        //}
                        clsMsg.AlertMessage("儲存成功....", this.Page);
                    }
                    else
                    {
                        //File.Delete(Session["FileN"].ToString());
                        Session["FileN"] = "";
                        clsMsg.AlertMessage("儲存失敗....", this.Page);
                    }
                }
            }
        }
        GvQuery();
        Session["FileN"] = "";


    }

    #region MailData
    private void MailData(string strFileN)
    {

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        //HttpCookie cookie_Location_P = Request.Cookies["Location"];
        //string strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

        DateTime dt;

        //mail標題
        string MailSubject = "檔案上傳通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Project1.txt");
        string strMailBody = myMailBody.ReadToEnd();


        #region 找資料塞到SendMail內


        //===sam測試
        for (int intI = 0; intI < 2; intI++)
        {
            DataTable dt5 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
            if (intI == 0)
            {
                if (lblAssign.Text == "")
                {
                    DataTable dt2;
                    //DataTable dt1 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
                    string strRelated = dt5.Rows[0]["Related"].ToString();
                    string[] sArray = strRelated.Split(',');
                    foreach (string i in sArray)
                    {
                        if (i != "")
                        {
                            dt2 = clsData.getEmployees("1", i);
                            if (dt2.Rows.Count != 0)
                            {
                                string strMail1 = dt2.Rows[0]["Email"].ToString();
                                string strBody = string.Format(strMailBody, Session["EmpName"], dt5.Rows[0]["Name"].ToString(), Session["CaseName"].ToString() + "-" + lblName.Text.Trim(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strFileN, Session["ID"].ToString());
                                clsTransaction.SendMail(strMail1, MailSubject, strBody);
                            }
                        }

                    }
                }
                else
                {
                    DataTable dt1 = clsData.getEmployees("1", lblAssign.Text);
                    if (dt1.Rows.Count != 0)
                    {
                        DataTable dt2 = clsData.UploadLeader("2", "", dt1.Rows[0]["Team"].ToString());
                        if (dt2.Rows.Count != 0)
                        {
                            string strMail1 = dt2.Rows[0]["Email"].ToString();
                            dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");

                            string strBody = string.Format(strMailBody, Session["EmpName"], dt5.Rows[0]["Name"].ToString(), Session["CaseName"].ToString() + "-" + lblName.Text.Trim(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strFileN, Session["ID"].ToString());

                            clsTransaction.SendMail(strMail1, MailSubject, strBody);
                        }
                    }
                }
            }
            else
            {
                //DataTable dt1 = clsData.getEmployees("1", lblAssign.Text);
                DataTable dt2 = clsData.UploadLeader("1", "", "");
                string strMail1 = dt2.Rows[0]["Email"].ToString();
                dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");

                string strBody = string.Format(strMailBody, Session["EmpName"], dt5.Rows[0]["Name"].ToString(), Session["CaseName"].ToString() + "-" + lblName.Text.Trim(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strFileN, Session["ID"].ToString());

                clsTransaction.SendMail(strMail1, MailSubject, strBody);
            }
        }
        //====

        //DataTable dt1 = clsData.getEmployees("1", lblAssign.Text);
        //DataTable dt2 = clsData.UploadLeader("2", dt1.Rows[0]["Location"].ToString(), dt1.Rows[0]["Team"].ToString());
        //string strMail1 = dt2.Rows[0]["Email"].ToString();
        //dt2 = clsData.UploadProjectQuery(strID, "Project");

        //string strBody = string.Format(strMailBody,lblAssign.Text, dt2.Rows[0]["Name"].ToString(), strKind + "-" + lblName.Text.Trim(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

        //clsTransaction.SendMail(strMail1, MailSubject, strBody);
        myMailBody.Close();
        myMailBody.Dispose();

        #endregion
    }
    #endregion

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        //string strID1 = "";
        Server.Transfer("~/WebForm/TaskEdit.aspx");
    }
    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        string strFile;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        //HttpCookie cookie_ItemName = Request.Cookies["ItemName"];
        //string strItemName = Server.UrlDecode(cookie_ItemName.Value);

        //DataTable dt = clsData.UploadProjectCaseID(Session["ID"].ToString(), Session["CaseName"].ToString(), "0", Session["ItemName"].ToString());
        DataTable dt = clsData.UploadProjectCaseID(Session["ID"].ToString(), Session["CaseName"].ToString(), "0", lblName.Text);

        foreach (DataRow dr in dt.Rows)
        {
            DataTable dt1 = clsData.UploadProjectCaseFileQuery(dt.Rows[0]["ID"].ToString(), Session["ID"].ToString());

            if (dt1.Rows.Count != 0)
            {
                strFile = dt1.Rows[0]["File_Path"].ToString() + @"\" + dt1.Rows[0]["File_Name"].ToString();
                File.Delete(strFile);
            }

            clsTransaction.DelUploadFilesCase("", Session["ID"].ToString(), dt.Rows[0]["ID"].ToString(), "1");
        }

        //if (clsTransaction.DelProjectTaskData(Session["ID"].ToString(), Session["CaseName"].ToString(), Session["ItemName"].ToString()) == true)
        if (clsTransaction.DelProjectTaskData(Session["ID"].ToString(), Session["CaseName"].ToString(), lblName.Text) == true)
        {
            Response.Redirect("~/WebForm/ProjectCase.aspx?Value=R");
        }
        else
            clsMsg.AlertMessage("刪除失敗！", this.Page);
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/WebForm/ProjectCase.aspx?Value=" + strCase + "&ID=" + strID);
        Server.Transfer("~/WebForm/ProjectCase.aspx?Value=R");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {


        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);
        listLeft.DataSource = dt;
        listLeft.DataBind();



        //this.gvwApparatus.DataSource = dt;
        //this.DataBind();  
    }
    protected void btnRight_Click(object sender, EventArgs e)
    {
        int count = listLeft.Items.Count;
        int index = 0;
        string strStartDate, strEndDate;
        DateTime dt;

        if ((lblStartdate.Text == "") || (lblEnddate.Text == ""))
        {
            clsMsg.AlertMessage("請設定時段！", this.Page);
        }
        else
        {
            dt = Convert.ToDateTime(lblStartdate.Text);
            strStartDate = dt.ToString("yyyy/MM/dd");
            strStartDate = strStartDate + " " + "00:00:00";

            dt = Convert.ToDateTime(lblEnddate.Text);
            dt.AddDays(1);
            strEndDate = dt.ToString("yyyy/MM/dd");
            strEndDate = strEndDate + " " + "00:00:00";

            DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, "", "");

            if (dt1.Rows.Count == 0)
            {

                for (int i = 0; i < count; i++)
                {
                    ListItem item = listLeft.Items[index];
                    //ListItem item = listLeft.Items[i];


                    if (listLeft.Items[index].Selected == true)
                    //if (listLeft.Items[i].Selected == true)
                    {
                        listLeft.Items.Remove(item);
                        listRight.Items.Add(item);
                        index--;
                    }
                    index++;
                }
            }
            else
                clsMsg.AlertMessage("此時段已被預約，請修改時段或選擇其他設備！", this.Page);
        }

    }
    protected void btnLeft_Click(object sender, EventArgs e)
    {
        int count = listRight.Items.Count - 1;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            ListItem item = listRight.Items[index];
            if (listRight.Items[index].Selected == true)
            {
                listRight.Items.Remove(item);
                listLeft.Items.Add(item);
                index--;
            }
            index++;
        }
    }
    protected void listRight_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        // Get the currently selected item in the ListBox.
        string curItem = listRight.SelectedItem.ToString();


    }
    protected void btnApparatus_Click(object sender, EventArgs e)
    {
        int count = listRight.Items.Count;
        int index = 0;
        DateTime dt;
        DataTable dt1;
        string strStartDate, strEndDate, strDepartment, strExt, strEmail, strCaseID;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        //HttpCookie cookie_ItemName = Request.Cookies["ItemName"];
        //string strItemName = Server.UrlDecode(cookie_ItemName.Value);

        dt1 = clsData.UploadProjectCaseID(Session["ID"].ToString(), Session["CaseName"].ToString(), "0", Session["ItemName"].ToString());
        strCaseID = Session["ID"].ToString() + "-" + dt1.Rows[0]["ID"].ToString();

        clsTransaction.DelReservation(strCaseID);

        dt = Convert.ToDateTime(lblStartdate.Text);
        strStartDate = dt.ToString("yyyy/MM/dd");
        strStartDate = strStartDate + " " + "00:00:00";

        dt = Convert.ToDateTime(lblEnddate.Text);
        strEndDate = dt.ToString("yyyy/MM/dd");
        strEndDate = strEndDate + " " + "00:00:00";

        dt1 = clsData.getEmployees("1", lblAssign.Text);
        strDepartment = dt1.Rows[0]["Department"].ToString();
        strExt = dt1.Rows[0]["Extension"].ToString();
        strEmail = dt1.Rows[0]["Email"].ToString();



        for (int i = 0; i < count; i++)
        {
            ListItem item = listRight.Items[i];

            if (clsTransaction.InsertApparatusReservation(item.Value, strStartDate, strEndDate, lblAssign.Text, strDepartment, strExt, strEmail, "", "", "", "", "Y", strCaseID, "", "", "", "", "", "", "", "", "","","","") == true)
                clsMsg.AlertMessage("設備安排成功！", this.Page);
        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (Control c in e.Row.Cells[0].Controls)
            {
                if (c.GetType().Equals(typeof(LinkButton)))
                {

                    //Session["fileupload_Name"] = e.Row.Cells[0].Text;
                    //Session["fileupload_Path"] = e.Row.Cells[3].Text;

                    //clsMsg.AlertMessage(Session["fileupload_Name"].ToString() + "-" + Session["fileupload_Path"].ToString(), this.Page);


                    LinkButton hl = (LinkButton)c;
                    hl.Attributes.Add("onclick", "window.open('filedownload.aspx')");
                    //hl.NavigateUrl = "#";
                }
            }
        }
    }

    protected void gvwMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Response.Write("您選取了：" + ((GridView)sender).SelectedIndex);
        //GridViewRow row = gvwMain.SelectedRow; 

        //Session["fileupload_Name"] = row.Cells[0].Text;
        //Session["fileupload_Path"] = row.Cells[4].Text;

        //GridViewRow row1 = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

        //strStatus = ((LinkButton)row1.Cells[6].FindControl("LinkButton1")).Text.Trim();

        string strName = ((LinkButton)this.gvwMain.Rows[gvwMain.SelectedIndex].Cells[0].FindControl("LinkButton1")).Text;

        string strName1 = ((Label)this.gvwMain.Rows[gvwMain.SelectedIndex].Cells[3].FindControl("lblGVSeq")).Text;

        Session["fileupload_Name"] = strName;
        Session["fileupload_Path"] = strName1;


        //clsMsg.AlertMessage(strName + "-" + row.Cells[1].Text + "-" + row.Cells[2].Text + "-" + strName1 , this.Page);
        //Response.Write("<script>window.open('filedownload.aspx');</script>");

        //Response.Write("<script>window.open('DepartmentDailyReport.aspx?Value=0&ID=" + strID + "');</script>");



    }


    //=================================debbie SIT Benchmark 20180517====================================
    #region ConvertToSQL Los
    private void ConvertToSQL_Los(string strPath, string strFile_Name, string strToday)
    {
        DataTable dt = clsBM.UploadProjectQuery(Session["ID"].ToString(), "Project");
        DataTable dt1 = clsBM.getProjectCase1(Session["ID"].ToString());
        string strKind, strCustomer, strNPI, strID, strFile, strProtocol, strBand, strBandwidth;
        string strType = "";
        string strName, strP_Name, strMaxID;
        int intW, intW1;
        StringBuilder strSQL = new StringBuilder();
        // DataTable dt1, dt2;

        strID = dt.Rows[0]["ID"].ToString();
        strKind = dt1.Rows[0]["Kind"].ToString();
        strP_Name = dt.Rows[0]["Name"].ToString();
        strCustomer = dt.Rows[0]["Customer"].ToString();
        strNPI = dt.Rows[0]["NPI"].ToString();
        strFile = Session["FileN"].ToString();
        strProtocol = "--";
        strBand = "--";
        strBandwidth = "--";
        strMaxID = "0";
        DateTime thisDate1 = new DateTime();
        thisDate1 = Convert.ToDateTime(strToday);
        strToday = thisDate1.ToString("yyyy/MM/dd");

        DataTable dt3 = clsBM.UploadLosInfoLastIDQuery();

        //strMaxID = dt3.Rows[0]["ID"].ToString();
        string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + "/" + strFile_Name + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
        OleDbConnection objConn = new OleDbConnection(strConn);

        objConn.Open();
        int intI1 = 0;
        string strSheetName = "";
        DataRow[] sheetList = objConn.GetSchema("Tables").Select();
        foreach (DataRow sheet in sheetList)
        {

            strSheetName = sheet["TABLE_NAME"].ToString();
            intW = 0;
            string strExcel = "";
            if (strSheetName.IndexOf("Print_Area") > 0)
            {
            }
            else
            {
                if (strSheetName.IndexOf("Introduction") > 0)
                {
                    int intI;
                    string strAskModelName = "", strLanMAC = "", str24WLanMAC = "", str5WLanMAC = "";
                    string strMainChipset = "", strChipsetNum = "", strEthType = "", strBootVersion = "", str24Mimo = "", str5Mimo = "";
                    string strFrequencyBand = "", strCusModelName = "", strHWVersion = "", strFWVersion = "", strBOMVersion = "";
                    string str24WLanChipset = "", str5WLanChipset = "", str24WLanChipsetNum = "", str5WLanChipsetNum = "";
                    string strReportNPI = "", strDriverVersion = "", strReportBand = "", strReportBandwidth = "", strLocation = "", strBandMode = "";
                    strExcel = "select * from [" + strSheetName + "]";
                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table2");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt4 = ds.Tables["table2"];

                    for (intI = 0; intI < dt4.Rows.Count; intI++)
                    {
                        intW1 = 0;
                        if (dt4.Rows[intI][1].ToString().Trim() == "Askey Model Name：")
                        {
                            strAskModelName = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Customer Model Name：")
                        {
                            strCusModelName = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "LAN MAC Address：")
                        {
                            strLanMAC = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Hardware Version：")
                        {
                            strHWVersion = dt4.Rows[intI][8].ToString();
                        }

                        if (dt4.Rows[intI][6].ToString().Trim() == "Firmware Version：")
                        {
                            strFWVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "WLAN MAC Address：")
                        {
                            str24WLanMAC = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5WLanMAC = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "BOM Version：")
                        {
                            strBOMVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Main Chipset Model：")
                        {
                            strMainChipset = dt4.Rows[intI][3].ToString();
                            strChipsetNum = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "WLAN Chipset Model：")
                        {
                            str24WLanChipset = dt4.Rows[intI][9].ToString();
                            str24WLanChipsetNum = dt4.Rows[intI][10].ToString();
                            intI = intI + 1;
                            str5WLanChipset = dt4.Rows[intI][9].ToString();
                            str5WLanChipsetNum = dt4.Rows[intI][10].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Ethernet Type：")
                        {
                            strEthType = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Boot Loader Version：")
                        {
                            strBootVersion = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "NPI Stage：")
                        {
                            strReportNPI = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Wireless Driver Version：")
                        {
                            strDriverVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Spatial Stream (Tx / Rx)：")
                        {
                            str24Mimo = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5Mimo = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Frequency Band (GHz)：")
                        {
                            strReportBand = dt4.Rows[intI][8].ToString();
                            if ((strReportBand.IndexOf("4G") > 0) && (strReportBand.IndexOf("5G") > 0))
                            {
                                strBandMode = "Dual-Band";
                            }
                            else if ((strReportBand.IndexOf("4G") > 0) || (strReportBand.IndexOf("5G") > 0))
                            {
                                strBandMode = "Single-Band";
                            }
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Frequency Band Mode：")
                        {
                            strFrequencyBand = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Bandwidth (MHz)：")
                        {
                            strReportBandwidth = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Test Laboratory Location")
                        {
                            intI++;
                            strLocation = dt4.Rows[intI][1].ToString();
                            intW1 = 1;
                        }
                        if (intW1 == 1)
                        {
                            string strReportVersion = strFile_Name;
                            string[] strReportName2 = strReportVersion.Split('-');

                            for (int intX = 0; intX < strReportName2.Length; intX++)
                            {
                                if (strReportName2[intX].IndexOf(".xls") > 0)
                                {
                                    strReportVersion = strReportName2[intX];
                                    strReportName2 = strReportVersion.Split('.');
                                    strReportVersion = strReportName2[0];
                                }
                            }
                            clsBM.InsertLosInformationToSQL(strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, strBOMVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strLocation, strBandMode, strToday, strReportVersion, strFile_Name);
                            intI = dt4.Rows.Count;
                        }
                    }

                }
                else if ((strSheetName.IndexOf("11n-2#4G-20M") > 0) || (strSheetName.IndexOf("11n-2#4G-40M") > 0) || (strSheetName.IndexOf("11n-5G-20M") > 0) || (strSheetName.IndexOf("11n-5G-40M") > 0) || (strSheetName.IndexOf("11ac-5G-20M") > 0) || (strSheetName.IndexOf("11ac-5G-40M") > 0) || (strSheetName.IndexOf("11ac-5G-80M") > 0))
                {
                    strExcel = "select * from [" + strSheetName + "]";

                    if (strSheetName.IndexOf("11n-2#4G-20M") > 0)
                    {
                        strProtocol = "802.11n";
                        strBand = "2.4G";
                        strBandwidth = "20MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("11n-2#4G-40M") > 0)
                    {
                        strProtocol = "802.11n";
                        strBand = "2.4G";
                        strBandwidth = "40MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("11n-5G-20M") > 0)
                    {
                        strProtocol = "802.11n";
                        strBand = "5G";
                        strBandwidth = "20MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("11n-5G-40M") > 0)
                    {
                        strProtocol = "802.11n";
                        strBand = "5G";
                        strBandwidth = "40MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("11ac-5G-20M") > 0)
                    {
                        strProtocol = "802.11ac";
                        strBand = "5G";
                        strBandwidth = "20MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("11ac-5G-40M") > 0)
                    {
                        strProtocol = "802.11ac";
                        strBand = "5G";
                        strBandwidth = "40MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("11ac-5G-80M") > 0)
                    {
                        strProtocol = "802.11ac";
                        strBand = "5G";
                        strBandwidth = "80MHz";
                        intW = 1;
                    }



                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table1");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt2 = ds.Tables["table1"];

                    int intI, intJ;
                    string strChannel, strFequency;
                    string strAngle = "";
                    string strNumber;
                    string[] strAtt = new string[18];
                    string[] strDistance = new string[18];
                    string[] strThroughput = new string[18];
                    string[] strAngle_2 = new string[12];
                    string[] strTput_A = new string[12];
                    string[] strRx = new string[8];
                    string[] strTRx = new string[8];
                    string[] strTotal_A = new string[8];
                    string strProtocol_A, strBand_A, strBandwidth_A, strBandwidth_ALL, strAtt_A;

                    DataTable dt_InfoID = clsBM.UploadLosInfoID(strID);
                    if (dt_InfoID.Rows.Count > 0)
                        strMaxID = dt_InfoID.Rows[0]["ID"].ToString();


                    for (intI = 0; intI < dt2.Rows.Count; intI++)
                    {
                        if (dt2.Rows[intI][0].ToString().Trim() == "Defined the Best Angle")
                        {

                            for (intJ = intI; intJ < 17; intJ++)
                            {
                                strFequency = "";
                                strChannel = "";
                                strAtt_A = "";
                                if (dt2.Rows[intJ][0].ToString().Trim() == "Channel / Angle")
                                {
                                    strBandwidth_ALL = dt2.Rows[intJ][3].ToString();
                                    string[] strBandwidth2 = strBandwidth_ALL.Split('-');
                                    strProtocol_A = strBandwidth2[0];
                                    strBand_A = strBandwidth2[1];
                                    strBandwidth_A = strBandwidth2[2];
                                    intJ = intJ + 1;
                                    strAtt_A = dt2.Rows[intJ][3].ToString();
                                    string[] strAtt2 = strAtt_A.Split(':');
                                    strAtt_A = strAtt2[1];
                                    intJ = intJ + 1;
                                    int intA = 0;
                                    for (int intK = 0; intK < 12; intK++)
                                    {
                                        strAngle_2[intA] = dt2.Rows[intJ][intK + 3].ToString();
                                        //intK = intK + 1;
                                        intA += 1;
                                    }
                                    intJ = intJ + 1;
                                    strFequency = dt2.Rows[intJ][0].ToString();
                                    string[] strFequency_A = strFequency.Split('\n');
                                    strFequency = strFequency_A[0];
                                    strChannel = strFequency_A[1];
                                    intA = 0;
                                    for (int intL = 0; intL < 4; intL++)
                                    {
                                        intA = 0;
                                        strType = dt2.Rows[intJ][1].ToString();
                                        for (int intK = 0; intK < 12; intK++)
                                        {
                                            strTput_A[intA] = dt2.Rows[intJ][intK + 3].ToString();
                                            //intK = intK + 1;
                                            intA += 1;
                                        }
                                        for (int intY = 0; intY < 12; intY++)
                                        {
                                            if (strMaxID != "0")
                                                clsBM.InsertLosAngleToSQL(strMaxID, strID, strProtocol, strBand, strBandwidth, strType, strAtt_A, strFequency, strChannel, strAngle_2[intY], strTput_A[intY]);
                                        }
                                        intJ = intJ + 1;
                                    }
                                }
                            }


                        }
                        strFequency = "";
                        strChannel = "";
                        intW1 = 0;
                        if (dt2.Rows[intI][0].ToString().IndexOf("Tx. Throughput") > 0)
                        {
                            strType = "Tx";
                            //intW1 = 1;
                        }
                        if ((dt2.Rows[intI][0].ToString().IndexOf("G - 20MHz  Rx. Throughput") > 0) || (dt2.Rows[intI][0].ToString().IndexOf("G - 40MHz  Rx. Throughput") > 0) || (dt2.Rows[intI][0].ToString().IndexOf("5G - 80MHz  Rx. Throughput") > 0))
                        {
                            strType = "Rx";
                            //intW1 = 1;
                        }
                        if (dt2.Rows[intI][0].ToString().IndexOf("Tx. + Rx. Throughput") > 0)
                        {
                            strType = "TxRx";
                            //intW1 = 1;
                        }

                        if (dt2.Rows[intI][0].ToString().Trim() == "Attenuation (dB)")
                        {
                            for (intJ = 0; intJ < 18; intJ++)
                            {
                                strAtt[intJ] = dt2.Rows[intI][intJ + 2].ToString();
                            }
                        }

                        if (dt2.Rows[intI][0].ToString().Trim() == "Distance (meter)")
                        {
                            for (intJ = 0; intJ < 18; intJ++)
                            {
                                strDistance[intJ] = dt2.Rows[intI][intJ + 2].ToString();
                            }
                            //intI = intI + 1;
                        }


                        if (dt2.Rows[intI][0].ToString().Trim() == "Channel")
                        {

                            strAngle = dt2.Rows[intI][13].ToString();
                            intI = intI + 1;
                            intW1 = 1;
                        }

                        if ((intW == 1) && (intW1 == 1))
                        {
                            while (dt2.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                            //if (dt.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                            {
                                strFequency = dt2.Rows[intI][0].ToString();
                                strChannel = dt2.Rows[intI][1].ToString();
                                for (intJ = 0; intJ < 18; intJ++)
                                {
                                    //strThroughput[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                    strNumber = dt2.Rows[intI][intJ + 2].ToString();
                                    if ((strNumber != "N/S") && (strMaxID != "0") && (strNumber != "") && (strNumber != "N/A") && (strNumber != "N/T"))
                                        clsBM.InsertLosDataToSQL(strMaxID, strID, strProtocol, strBand, strBandwidth, strType, strAtt[intJ], strDistance[intJ], strFequency, strChannel, strAngle, strNumber);

                                    else
                                        intJ = 18;

                                }
                                intI++;
                            }
                        }


                    }
                    intI1++;

                }
            }
        }
        objConn.Close();

    }
    #endregion

    #region ConvertToSQL MeshRvR
    private void ConvertToSQL_MeshRvR(string strPath, string strFile_Name, string strToday)
    {
        DataTable dt = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
        DataTable dt1 = clsData.getProjectCase1(Session["ID"].ToString());
        string strKind, strCustomer, strNPI, strID, strFile, strMode, strBand, strBandwidth, strSheet = "";
        string strDirection = "", strDirectionName = "";
        string strName, strP_Name, strMaxID;
        int intW, intW1;
        StringBuilder strSQL = new StringBuilder();
        // DataTable dt1, dt2;

        strID = dt.Rows[0]["ID"].ToString();
        strKind = dt1.Rows[0]["Kind"].ToString();
        strP_Name = dt.Rows[0]["Name"].ToString();
        strCustomer = dt.Rows[0]["Customer"].ToString();
        strNPI = dt.Rows[0]["NPI"].ToString();
        strFile = Session["FileN"].ToString();
        strMode = "--";
        strBand = "--";
        strBandwidth = "--";
        strMaxID = "0";

        DateTime thisDate1 = new DateTime();
        thisDate1 = Convert.ToDateTime(strToday);
        strToday = thisDate1.ToString("yyyy/MM/dd");

        DataTable dt3 = clsBM.UploadMeshInfoLastIDQuery();

        //strMaxID = dt3.Rows[0]["ID"].ToString();
        string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + "/" + strFile_Name + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
        OleDbConnection objConn = new OleDbConnection(strConn);

        objConn.Open();
        int intI1 = 0;
        string strSheetName = "";
        DataRow[] sheetList = objConn.GetSchema("Tables").Select();
        foreach (DataRow sheet in sheetList)
        {

            strSheetName = sheet["TABLE_NAME"].ToString();
            intW = 0;
            string strExcel = "";
            if (strSheetName.IndexOf("Print_Area") > 0)
            {
            }
            else
            {
                if (strSheetName.IndexOf("Introduction") > 0)
                {
                    int intI;
                    string strAskModelName = "", strLanMAC = "", str24WLanMAC = "", str5WLanMAC = "", str5WLanMAC2 = "", strFrequencyBand = "";
                    string strMainChipset = "", strChipsetNum = "", str24Mimo = "", str5Mimo = "", str5Mimo2 = "", strBootVersion = "";
                    string strCusModelName = "", strHWVersion = "", strFWVersion = "", strBOMVersion = "", strPCBVersion = "", strEthType = "";
                    string str24WLanChipset = "", str5WLanChipset = "", str5WLanChipset2 = "", str24WLanChipsetNum = "", str5WLanChipsetNum = "", str5WLanChipsetNum2 = "";
                    string strReportNPI = "", strDriverVersion = "", strReportBand = "", strReportBandwidth = "", strLocation = "";
                    strExcel = "select * from [" + strSheetName + "]";
                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table2");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt4 = ds.Tables["table2"];

                    for (intI = 1; intI < dt4.Rows.Count; intI++)
                    {
                        intW1 = 0;
                        if (dt4.Rows[intI][1].ToString().Trim() == "Askey Model Name：")
                        {
                            strAskModelName = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Customer Model Name：")
                        {
                            strCusModelName = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "LAN MAC Address：")
                        {
                            strLanMAC = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Firmware Version：")
                        {
                            strFWVersion = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Main Chipset Model：")
                        {
                            strMainChipset = dt4.Rows[intI][8].ToString();
                            strChipsetNum = dt4.Rows[intI][9].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "WLAN MAC Address：")
                        {
                            str24WLanMAC = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5WLanMAC = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5WLanMAC2 = dt4.Rows[intI][4].ToString();
                        }
                        if ((dt4.Rows[intI][6].ToString().Trim() == "WLAN Chipset Model：") || (dt4.Rows[intI - 1][6].ToString().Trim() == "WLAN Chipset Model："))
                        {
                            if (dt4.Rows[intI][8].ToString().Trim() == "2.4GHz：")
                            {
                                str24WLanChipset = dt4.Rows[intI][9].ToString();
                                str24WLanChipsetNum = dt4.Rows[intI][10].ToString();
                                intI = intI + 1;
                            }
                            if (dt4.Rows[intI - 1][8].ToString().Trim() == "2.4GHz：")
                            {
                                str24WLanChipset = dt4.Rows[intI - 1][9].ToString();
                                str24WLanChipsetNum = dt4.Rows[intI - 1][10].ToString();
                            }
                            str5WLanChipset = dt4.Rows[intI][9].ToString();
                            str5WLanChipsetNum = dt4.Rows[intI][10].ToString();
                            intI = intI + 1;
                            str5WLanChipset2 = dt4.Rows[intI][9].ToString();
                            str5WLanChipsetNum2 = dt4.Rows[intI][10].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Hardware Version：")
                        {
                            strHWVersion = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI - 1][1].ToString().Trim() == "Hardware Version：")
                        {
                            strHWVersion = dt4.Rows[intI - 1][3].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Firmware Version：")
                        {
                            strPCBVersion = dt4.Rows[intI][3].ToString();
                        }

                        if (dt4.Rows[intI][6].ToString().Trim() == "Ethernet Type：")
                        {
                            strEthType = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Boot Loader Version：")
                        {
                            strBootVersion = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "NPI Stage：")
                        {
                            strReportNPI = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Wireless Driver Version：")
                        {
                            strDriverVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Frequency Band (GHz)：")
                        {
                            strReportBand = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Spatial Stream (Tx / Rx)：")
                        {
                            str24Mimo = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5Mimo = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5Mimo2 = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI - 1][6].ToString().Trim() == "Frequency Band Mode：")
                        {
                            strFrequencyBand = dt4.Rows[intI - 1][8].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Frequency Band Mode：")
                        {
                            strFrequencyBand = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Bandwidth (MHz)：")
                        {
                            strReportBandwidth = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Test Laboratory Location")
                        {
                            intI++;
                            strLocation = dt4.Rows[intI][1].ToString();
                            intW1 = 1;
                        }

                        if (intW1 == 1)
                        {
                            string strReportVersion = strFile_Name;
                            string[] strReportName2 = strReportVersion.Split('-');

                            for (int intX = 0; intX < strReportName2.Length; intX++)
                            {
                                if (strReportName2[intX].IndexOf(".xls") > 0)
                                {
                                    strReportVersion = strReportName2[intX];
                                    strReportName2 = strReportVersion.Split('.');
                                    strReportVersion = strReportName2[0];
                                }
                            }
                            clsBM.InsertMeshInformationToSQL(strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, str5WLanMAC2, strHWVersion, strPCBVersion, strBootVersion, str24Mimo, str5Mimo, str5Mimo2, strCusModelName, strEthType, strMainChipset, str24WLanChipset, str5WLanChipset, str5WLanChipset2, str24WLanChipsetNum, str5WLanChipsetNum, str5WLanChipsetNum2, strReportNPI, strDriverVersion, strReportBand, strFrequencyBand, strReportBandwidth, strLocation, strToday, strReportVersion, strFile_Name);
                            intI = dt4.Rows.Count;
                        }
                    }
                }
                else if ((strSheetName.IndexOf("AP Mode") > 0) || (strSheetName.IndexOf("Backhaul") > 0))
                {
                    strExcel = "select * from [" + strSheetName + "]";

                    if (strSheetName.IndexOf("AP Mode") > 0)
                    {
                        strSheet = "AP Mode";
                    }
                    if (strSheetName.IndexOf("Backhaul") > 0)
                    {
                        strSheet = "Backhaul";
                    }

                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table1");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt2 = ds.Tables["table1"];

                    int intI, intJ;
                    string strChannel = "";

                    string strNumber;
                    string[] strAtt = new string[18];
                    string[] strDistance = new string[18];
                    string[] strThroughput = new string[18];
                    string[] strAngle_2 = new string[8];
                    string[] strRx = new string[8];
                    string[] strTRx = new string[8];

                    DataTable dt_InfoID = clsBM.UploadMeshInfoID(strID);
                    if (dt_InfoID.Rows.Count != 0)
                    {
                        strMaxID = dt_InfoID.Rows[0]["ID"].ToString();

                        for (intI = 0; intI < dt2.Rows.Count; intI++)
                        {
                            if ((dt2.Rows[intI][0].ToString().Trim().IndexOf("20MHz") > 0) && (dt2.Rows[intI][0].ToString().Trim().IndexOf("802.11n") > 0))
                            {
                                strMode = "802.11n";
                                strBand = "2.4G";
                                strBandwidth = "20MHz";
                                intW = 1;
                            }
                            if ((dt2.Rows[intI][0].ToString().Trim().IndexOf("40MHz") > 0) && (dt2.Rows[intI][0].ToString().Trim().IndexOf("802.11n") > 0))
                            {
                                strMode = "802.11n";
                                strBand = "2.4G";
                                strBandwidth = "40MHz";
                                intW = 1;
                            }

                            if ((dt2.Rows[intI][0].ToString().Trim().IndexOf("80MHz") > 0) && (dt2.Rows[intI][0].ToString().Trim().IndexOf("802.11ac") > 0))
                            {
                                strMode = "802.11ac";
                                strBand = "5G";
                                strBandwidth = "80MHz";
                                intW = 1;
                            }

                            if (dt2.Rows[intI][0].ToString().Trim() == "Direction")
                            {
                                strChannel = dt2.Rows[intI][9].ToString();
                            }
                            intW1 = 0;
                            if (dt2.Rows[intI][0].ToString().IndexOf("Tx") > 0)
                            {
                                strDirection = "Tx";
                                strDirectionName = dt2.Rows[intI][0].ToString();
                                intW1 = 1;
                            }
                            if (dt2.Rows[intI][0].ToString().IndexOf("Rx") > 0)
                            {
                                strDirection = "Rx";
                                strDirectionName = dt2.Rows[intI][0].ToString();

                                intW1 = 1;
                            }
                            if (dt2.Rows[intI][0].ToString().IndexOf("BD") > 0)
                            {
                                strDirection = "BD";
                                strDirectionName = dt2.Rows[intI][0].ToString();

                                intW1 = 1;
                            }
                            if (dt2.Rows[intI][0].ToString().Trim() == "Attenuation (dB)")
                            {
                                for (intJ = 0; intJ < 18; intJ++)
                                {
                                    strAtt[intJ] = dt2.Rows[intI][intJ + 2].ToString();
                                }
                            }
                            if ((intW == 1) && (intW1 == 1))
                            {
                                //while (dt2.Rows[intI][0].ToString() == strDirectionName)
                                if (dt2.Rows[intI][0].ToString() == strDirectionName)
                                {
                                    //strFequency = dt2.Rows[intI][0].ToString();
                                    //strChannel = dt2.Rows[intI][1].ToString();
                                    for (intJ = 0; intJ < 18; intJ++)
                                    {
                                        //strThroughput[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                        strNumber = dt2.Rows[intI][intJ + 2].ToString();
                                        if ((strNumber != "N/A") && (strNumber != "") && (strNumber != "N/S") && (strNumber != "N/T"))
                                            clsBM.InsertMeshDataToSQL(strMaxID, strID, strSheet, strMode, strBand, strBandwidth, strDirection, strDirectionName, strAtt[intJ], strChannel, strNumber);
                                        //else
                                        //    intJ = 19;

                                    }
                                }

                            }


                        }
                    }
                    else
                    {
                        clsMsg.AlertMessage("Benchmark上傳失敗!!", this.Page);
                    }
                }
            }
        }
        objConn.Close();

    }
    #endregion

    #region ConvertToSQL Indoor
    private void ConvertToSQL_Indoor(string strPath, string strFile_Name, string strToday)
    {
        DataTable dt = clsBM.UploadProjectQuery(Session["ID"].ToString(), "Project");
        DataTable dt1 = clsBM.getProjectCase1(Session["ID"].ToString());
        string strKind, strCustomer, strNPI, strID, strFile, strMode, strBand, strBandwidth;

        string strName, strP_Name, strMaxID;
        int intW, intW1;
        StringBuilder strSQL = new StringBuilder();
        // DataTable dt1, dt2;

        strID = dt.Rows[0]["ID"].ToString();
        strKind = dt1.Rows[0]["Kind"].ToString();
        strP_Name = dt.Rows[0]["Name"].ToString();
        strCustomer = dt.Rows[0]["Customer"].ToString();
        strNPI = dt.Rows[0]["NPI"].ToString();
        strFile = Session["FileN"].ToString();
        strMode = "--";
        strBand = "--";
        strBandwidth = "--";
        strMaxID = "0";
        byte[] Special_Picture = { };
        DateTime thisDate1 = new DateTime();
        thisDate1 = Convert.ToDateTime(strToday);
        strToday = thisDate1.ToString("yyyy/MM/dd");

        DataTable dt3 = clsBM.UploadLosInfoLastIDQuery();

        //strMaxID = dt3.Rows[0]["ID"].ToString();
        string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + "/" + strFile_Name + ";Extended Properties='Excel 12.0 Xml;IMEX=1;HDR=YES'";
        OleDbConnection objConn = new OleDbConnection(strConn);

        objConn.Open();
        string strSheetName = "";
        string[] strAngle = new string[9];
        DataRow[] sheetList = objConn.GetSchema("Tables").Select();
        foreach (DataRow sheet in sheetList)
        {

            strSheetName = sheet["TABLE_NAME"].ToString();
            intW = 0;
            string strExcel = "";
            if (strSheetName.IndexOf("Print_Area") > 0)
            {
            }
            else
            {
                if (strSheetName.IndexOf("Introduction") > 0)
                {
                    int intI;
                    string strAskModelName = "", strLanMAC = "", str24WLanMAC = "", str5WLanMAC = "";
                    string strMainChipset = "", strChipsetNum = "", strEthType = "", strBootVersion = "", str24Mimo = "", str5Mimo = "";
                    string strFrequencyBand = "", strCusModelName = "", strHWVersion = "", strFWVersion = "", strBOMVersion = "";
                    string str24WLanChipset = "", str5WLanChipset = "", str24WLanChipsetNum = "", str5WLanChipsetNum = "";
                    string strReportNPI = "", strDriverVersion = "", strReportBand = "", strReportBandwidth = "", strLocation = "", strBandMode = "", strWLanCard = "";
                    strExcel = "select * from [" + strSheetName + "]";
                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table2");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt4 = ds.Tables["table2"];
                    intW = 0;
                    intW1 = 0;
                    for (intI = 0; intI < dt4.Rows.Count; intI++)
                    {
                        if (dt4.Rows[intI][1].ToString().Trim() == "Askey Model Name：")
                        {
                            strAskModelName = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Customer Model Name：")
                        {
                            strCusModelName = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "LAN MAC Address：")
                        {
                            strLanMAC = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Hardware Version：")
                        {
                            strHWVersion = dt4.Rows[intI][8].ToString();
                        }

                        if (dt4.Rows[intI][6].ToString().Trim() == "Firmware Version：")
                        {
                            strFWVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "WLAN MAC Address：")
                        {
                            str24WLanMAC = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5WLanMAC = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "BOM Version：")
                        {
                            strBOMVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Main Chipset Model：")
                        {
                            strMainChipset = dt4.Rows[intI][3].ToString();
                            strChipsetNum = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "WLAN Chipset Model：")
                        {
                            str24WLanChipset = dt4.Rows[intI][9].ToString();
                            str24WLanChipsetNum = dt4.Rows[intI][10].ToString();
                            intI = intI + 1;
                            str5WLanChipset = dt4.Rows[intI][9].ToString();
                            str5WLanChipsetNum = dt4.Rows[intI][10].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Ethernet Type：")
                        {
                            strEthType = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Boot Loader Version：")
                        {
                            strBootVersion = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "NPI Stage：")
                        {
                            strReportNPI = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Wireless Driver Version：")
                        {
                            strDriverVersion = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Spatial Stream (Tx / Rx)：")
                        {
                            str24Mimo = dt4.Rows[intI][4].ToString();
                            intI = intI + 1;
                            str5Mimo = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Frequency Band (GHz)：")
                        {
                            strReportBand = dt4.Rows[intI][8].ToString();
                            if ((strReportBand.IndexOf("4G") > 0) && (strReportBand.IndexOf("5G") > 0))
                            {
                                strBandMode = "Dual-Band";
                            }
                            else if ((strReportBand.IndexOf("4G") > 0) || (strReportBand.IndexOf("5G") > 0))
                            {
                                strBandMode = "Single-Band";
                            }
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Frequency Band Mode：")
                        {
                            strFrequencyBand = dt4.Rows[intI][3].ToString();
                        }
                        if (dt4.Rows[intI][6].ToString().Trim() == "Bandwidth (MHz)：")
                        {
                            strReportBandwidth = dt4.Rows[intI][8].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Test Laboratory Location")
                        {
                            intI++;
                            strLocation = dt4.Rows[intI][1].ToString();
                            intW1 = 1;
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "WLAN Card")
                        {
                            strWLanCard = dt4.Rows[intI][3].ToString();
                            intW = 1;
                        }

                        if ((intW1 == 1) && (intW == 1))
                        {
                            string strReportVersion = strFile_Name;
                            string[] strReportName2 = strReportVersion.Split('-');

                            for (int intX = 0; intX < strReportName2.Length; intX++)
                            {
                                if (strReportName2[intX].IndexOf(".xls") > 0)
                                {
                                    strReportVersion = strReportName2[intX];
                                    strReportName2 = strReportVersion.Split('.');
                                    strReportVersion = strReportName2[0];
                                }
                            }
                            clsBM.InsertIndoorInformationToSQL(strID, strAskModelName, strLanMAC, str24WLanMAC, str5WLanMAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, strBOMVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strLocation, strBandMode, strWLanCard, strToday, strReportVersion, strFile_Name);

                            intI = dt4.Rows.Count;
                        }
                    }
                }
                else if ((strSheetName.IndexOf("Room DUT 2#4G") > 0) || (strSheetName.IndexOf("Room DUT 5G") > 0))
                {
                    strExcel = "select * from [" + strSheetName + "]";

                    if (strSheetName.IndexOf("Room DUT 2#4G") > 0)
                    {
                        strBand = "2.4G";
                        intW = 1;
                    }

                    if (strSheetName.IndexOf("Room DUT 5G") > 0)
                    {
                        strBand = "5G";
                        intW = 1;
                    }

                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table1");
                    //strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt2 = ds.Tables["table1"];

                    int intI, intJ;
                    string strChannel = "", strTestPoint = "", strDirection = "";

                    string strNumber;

                    string[] strRssi = new string[9];


                    DataTable dt_InfoID = clsBM.UploadIndoorInfoID(strID);
                    if (dt_InfoID.Rows.Count > 0)
                        strMaxID = dt_InfoID.Rows[0]["ID"].ToString();

                    //string strRow = "";
                    strMode = "";
                    strChannel = "";
                    intW = 0;
                    for (intI = 0; intI < dt2.Rows.Count; intI++)
                    {
                        //strRow = dt2.Rows[intI][0].ToString().Trim();
                        if (dt2.Rows[intI][1].ToString().Trim() == "Band")
                        {
                            strMode = dt2.Rows[intI][4].ToString();
                            strBandwidth = dt2.Rows[intI][6].ToString();
                            strChannel = dt2.Rows[intI][8].ToString();
                            intW = 1;
                        }

                        if (dt2.Rows[intI][1].ToString() == "Test Point")
                        {
                            for (intJ = 0; intJ < 9; intJ++)
                            {
                                strAngle[intJ] = dt2.Rows[intI][intJ + 3].ToString().Trim();
                                if (strAngle[0] == "")
                                {
                                    strAngle[0] = "0°";
                                    intJ = 9;
                                }
                            }
                        }
                        //strRow = dt2.Rows[intI][2].ToString().Trim();
                        if (dt2.Rows[intI][2].ToString() == "RSSI")
                        {
                            for (intJ = 0; intJ < 8; intJ++)
                            {
                                strRssi[intJ] = dt2.Rows[intI][intJ + 3].ToString().Trim();
                            }
                            strTestPoint = dt2.Rows[intI][1].ToString().Trim();
                        }
                        intW1 = 0;
                        if (dt2.Rows[intI][2].ToString() == "Tx")
                        {
                            strDirection = "Tx";
                            intW1 = 1;
                        }
                        if (dt2.Rows[intI][2].ToString() == "Rx")
                        {
                            strDirection = "Rx";
                            intW1 = 1;
                        }
                        if (dt2.Rows[intI][2].ToString() == "Tx+Rx")
                        {
                            strDirection = "Tx+Rx";
                            intW1 = 1;
                        }
                        if ((intW == 1) && (intW1 == 1))
                        {
                            for (intJ = 0; intJ < 9; intJ++)
                            {
                                strNumber = dt2.Rows[intI][intJ + 3].ToString();
                                strRssi[8] = "";
                                if ((strNumber != "N/S") && (strMaxID != "0") && (strNumber != "") && (strNumber != "N/A") && (strNumber != "N/T"))
                                    clsBM.InsertIndoorDataToSQL(strMaxID, strID, strMode, strBand, strBandwidth, strChannel, strDirection, strTestPoint, strRssi[intJ], strAngle[intJ], strNumber);
                                else
                                    intJ = 10;
                            }
                        }
                    }
                }
            }
        }
        objConn.Close();
    }

    #endregion

    #region ConvertToSQL OTA
    private void ConvertToSQL_OTA(string strPath, string strFile_Name, string strToday)
    {
        DataTable dt = clsBM.UploadProjectQuery(Session["ID"].ToString(), "Project");
        DataTable dt1 = clsBM.getProjectCase1(Session["ID"].ToString());
        string strKind, strCustomer, strNPI, strID, strFile, strMode, strBand, strBandwidth;
        string strDirection = "";
        string strName, strP_Name, strMaxID;
        int intW, intW1;
        StringBuilder strSQL = new StringBuilder();
        // DataTable dt1, dt2;

        strID = dt.Rows[0]["ID"].ToString();
        strKind = dt1.Rows[0]["Kind"].ToString();
        strP_Name = dt.Rows[0]["Name"].ToString();
        strCustomer = dt.Rows[0]["Customer"].ToString();
        strNPI = dt.Rows[0]["NPI"].ToString();
        strFile = Session["FileN"].ToString();
        strMode = "--";
        strBand = "--";
        strBandwidth = "--";
        strMaxID = "0";
        DateTime thisDate1 = new DateTime();
        thisDate1 = Convert.ToDateTime(strToday);
        strToday = thisDate1.ToString("yyyy/MM/dd");

        DataTable dt3 = clsBM.UploadLosInfoLastIDQuery();

        //strMaxID = dt3.Rows[0]["ID"].ToString();
        string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + "/" + strFile_Name + ";Extended Properties='Excel 12.0 Xml;IMEX=1;HDR=YES'";
        OleDbConnection objConn = new OleDbConnection(strConn);

        objConn.Open();
        int intI1 = 0;
        string strSheetName = "";
        DataRow[] sheetList = objConn.GetSchema("Tables").Select();
        foreach (DataRow sheet in sheetList)
        {

            strSheetName = sheet["TABLE_NAME"].ToString();
            intW = 0;
            string strExcel = "";
            if (strSheetName.IndexOf("Print_Area") > 0)
            {
            }
            else if (strSheetName.IndexOf("$'Z") > 0)
            {
            }
            else
            {
                if (strSheetName.IndexOf("Introduction") > 0)
                {
                    int intI;
                    string strAskModelName = "", strLanMAC = "", str24MAC = "", str5MAC = "";
                    string strMainChipset = "", strChipsetNum = "", strEthType = "", strBootVersion = "", str24Mimo = "", str5Mimo = "";
                    string strFrequencyBand = "", strCusModelName = "", strHWVersion = "", strFWVersion = "", strBOMVersion = "";
                    string str24WLanChipset = "", str5WLanChipset = "", str24WLanChipsetNum = "", str5WLanChipsetNum = "";
                    string strReportNPI = "", strDriverVersion = "", strReportBand = "", strReportBandwidth = "", strLocation = "", strBandMode = "";
                    strExcel = "select * from [" + strSheetName + "]";
                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table2");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt4 = ds.Tables["table2"];

                    for (intI = 0; intI < dt4.Rows.Count; intI++)
                    {
                        intW1 = 0;
                        if (dt4.Rows[intI][1].ToString().Trim() == "DUT Model Name：")
                        {
                            string[] strDUTModelName = dt4.Rows[intI][4].ToString().Split('/');
                            strAskModelName = strDUTModelName[0];
                            if (strDUTModelName.Length > 1)
                                strCusModelName = strDUTModelName[1];
                            else
                                strCusModelName = "";
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "DUT MAC Address：")
                        {
                            if (dt4.Rows[intI][4].ToString().Trim() == "LAN :")
                            {
                                strLanMAC = dt4.Rows[intI][5].ToString();
                                intI = intI + 1;
                            }
                            if (dt4.Rows[intI][4].ToString().Trim() == "2.4GHz :")
                            {
                                str24MAC = dt4.Rows[intI][5].ToString();
                                intI = intI + 1;
                            }
                            if (dt4.Rows[intI][4].ToString().Trim() == "5GHz :")
                            {
                                str5MAC = dt4.Rows[intI][5].ToString();
                            }
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Hardware Version：")
                        {
                            strHWVersion = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Firmware Version：")
                        {
                            strFWVersion = dt4.Rows[intI][4].ToString();
                        }

                        if (dt4.Rows[intI][1].ToString().Trim() == "Main Chipset：")
                        {
                            strMainChipset = dt4.Rows[intI][4].ToString();
                            strChipsetNum = dt4.Rows[intI][5].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "WLAN Chipset：")
                        {
                            str24WLanChipset = dt4.Rows[intI][5].ToString();
                            str24WLanChipsetNum = dt4.Rows[intI][6].ToString();
                            intI = intI + 1;
                            str5WLanChipset = dt4.Rows[intI][5].ToString();
                            str5WLanChipsetNum = dt4.Rows[intI][6].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Ethernet Type：")
                        {
                            strEthType = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Boot Loader Version：")
                        {
                            strBootVersion = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "NPI Stage：")
                        {
                            strReportNPI = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Wireless Driver Version：")
                        {
                            strDriverVersion = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Spatial Stream (Tx / Rx)：")
                        {
                            str24Mimo = dt4.Rows[intI][6].ToString();
                            intI = intI + 1;
                            str5Mimo = dt4.Rows[intI][6].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Frequency Band (GHz)：")
                        {
                            strReportBand = dt4.Rows[intI][4].ToString();
                            if ((strReportBand.IndexOf("4G") > 0) && (strReportBand.IndexOf("5G") > 0))
                            {
                                strBandMode = "Dual-Band";
                            }
                            else if ((strReportBand.IndexOf("4G") > 0) || (strReportBand.IndexOf("5G") > 0))
                            {
                                strBandMode = "Single-Band";
                            }
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Frequency Band Mode：")
                        {
                            strFrequencyBand = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Bandwidth (MHz)：")
                        {
                            strReportBandwidth = dt4.Rows[intI][4].ToString();
                        }
                        if (dt4.Rows[intI][1].ToString().Trim() == "Test Laboratory Location")
                        {
                            intI++;
                            strLocation = dt4.Rows[intI][1].ToString();
                            intW1 = 1;
                        }
                        if (intW1 == 1)
                        {
                            string strReportVersion = strFile_Name;
                            string[] strReportName2 = strReportVersion.Split('-');

                            for (int intX = 0; intX < strReportName2.Length; intX++)
                            {
                                if (strReportName2[intX].IndexOf(".xls") > 0)
                                {
                                    strReportVersion = strReportName2[intX];
                                    strReportName2 = strReportVersion.Split('.');
                                    strReportVersion = strReportName2[0];
                                }
                            }
                            clsBM.InsertOTAInformationToSQL(strID, strAskModelName, strLanMAC, str24MAC, str5MAC, strMainChipset, strChipsetNum, strEthType, strBootVersion, str24Mimo, str5Mimo, strFrequencyBand, strCusModelName, strHWVersion, strFWVersion, str24WLanChipset, str5WLanChipset, str24WLanChipsetNum, str5WLanChipsetNum, strReportNPI, strDriverVersion, strReportBand, strReportBandwidth, strLocation, strBandMode, strToday, strReportVersion, strFile_Name);
                            intI = dt4.Rows.Count;
                        }
                    }

                }
                else if ((strSheetName.IndexOf("2#4GHz") > 0) || (strSheetName.IndexOf("5GHz") > 0))
                {
                    strExcel = "select * from [" + strSheetName + "]";
                    string strBandMode = "";
                    if (strSheetName.IndexOf("2#4GHz") > 0)
                    {
                        //strProtocol = "802.11n";
                        strBand = "2.4G";
                        strBandMode = "Single";
                        //strBandwidth = "20MHz";
                        intW = 1;
                    }
                    if (strSheetName.IndexOf("5GHz") > 0)
                    {
                        //strProtocol = "802.11n";
                        strBand = "5G";
                        strBandMode = "Single";
                        //strBandwidth = "40MHz";
                        intW = 1;
                    }

                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table1");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt2 = ds.Tables["table1"];

                    int intI, intJ, intDirection = 0;
                    string strChannel, strFequency;
                    string strNumber;

                    DataTable dt_InfoID = clsBM.UploadOTAInfoID(strID);
                    if (dt_InfoID.Rows.Count > 0)
                        strMaxID = dt_InfoID.Rows[0]["ID"].ToString();
                    intW1 = 0;
                    for (intI = 0; intI < dt2.Rows.Count; intI++)
                    {
                        if (dt2.Rows[intI][2].ToString().Trim() == "Tx")
                        {
                            intW1 = 0;
                            intDirection = intI;
                            string str;
                            str = dt2.Rows[intI - 1][2].ToString().Trim();
                            if (str.IndexOf("802.11n") > 0)
                            {
                                strMode = "802.11n";
                            }
                            else if (str.IndexOf("802.11ac") > 0)
                            {
                                strMode = "802.11ac";
                            }
                            if (str.IndexOf("20MHz") > 0)
                            {
                                strBandwidth = "20MHz";
                            }
                            else if (str.IndexOf("40MHz") > 0)
                            {
                                strBandwidth = "40MHz";
                            }
                            else if (str.IndexOf("80MHz") > 0)
                            {
                                strBandwidth = "80MHz";
                            }
                            if ((strBandwidth != "") && (strMode != ""))
                            {
                                intW1 = 1;
                            }
                            intI = intI + 1;
                        }
                        strFequency = "";
                        strChannel = "";

                        if ((intW == 1) && (intW1 == 1))
                        {
                            while (dt2.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                            //if (dt.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                            {
                                strFequency = dt2.Rows[intI][0].ToString();
                                strChannel = dt2.Rows[intI][1].ToString();

                                for (intJ = 0; intJ < 3; intJ++)
                                {
                                    strDirection = dt2.Rows[intDirection][intJ + 2].ToString();

                                    //strThroughput[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                    strNumber = dt2.Rows[intI][intJ + 2].ToString();
                                    if ((strNumber != "N/S") && (strMaxID != "0") && (strNumber != "") && (strNumber != "N/A") && (strNumber != "N/T"))
                                        clsBM.InsertOTADataToSQL(strMaxID, strID, strMode, strBand, strBandMode, strBandwidth, strDirection, strFequency, strChannel, "", strNumber);
                                }
                                intI++;
                            }
                        }
                    }
                    intI1++;
                }
                else if (strSheetName.IndexOf("Concurrent") > 0)
                {
                    strExcel = "select * from [" + strSheetName + "]";
                    string strBandMode = "";
                    strBand = "2.4G+5G";
                    strBandMode = "Dual";

                    OleDbDataAdapter myCommand = null;
                    DataSet ds = null;

                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    ds = new DataSet();
                    myCommand.Fill(ds, "table1");
                    strName = dt.Rows[0]["Customer"].ToString();
                    DataTable dt2 = ds.Tables["table1"];

                    int intI, intJ, intDirection = 0;
                    string strChannel, strFequency, strBand2 = "", strChannel2, strMode2 = "", strBandwidth2 = "";
                    string strNumber;

                    DataTable dt_InfoID = clsBM.UploadOTAInfoID(strID);
                    if (dt_InfoID.Rows.Count > 0)
                        strMaxID = dt_InfoID.Rows[0]["ID"].ToString();
                    intW1 = 0;
                    for (intI = 0; intI < dt2.Rows.Count; intI++)
                    {
                        string str;
                        str = dt2.Rows[intI][0].ToString();

                        intW1 = 0;
                        if (str.IndexOf("4GHz - 20MHz") > 0)
                        {
                            strBand = "2.4G";
                            strBandwidth = "20MHz";
                        }
                        if (str.IndexOf("5GHz - 20MHz") > 0)
                        {
                            strBand2 = "5G";
                            strBandwidth2 = "20MHz";
                        }
                        if (str.IndexOf("5GHz - 40MHz") > 0)
                        {
                            strBand2 = "5G";
                            strBandwidth2 = "40MHz";
                        }
                        if (str.IndexOf("5GHz - 80MHz") > 0)
                        {
                            strBand2 = "5G";
                            strBandwidth2 = "80MHz";
                        }
                        if (dt2.Rows[intI][1].ToString().Trim() == "Tx")
                        {
                            intDirection = intI;

                            str = dt2.Rows[intI - 1][1].ToString();
                            if (str.IndexOf("4GHz - 11n") > 0)
                            {
                                strMode = "802.11n";
                            }
                            if (str.IndexOf("5GHz - 802.11ac") > 0)
                            {
                                strMode2 = "802.11ac";
                            }
                            if (str.IndexOf("5GHz - 802.11n") > 0)
                            {
                                strMode2 = "802.11n";
                            }
                            if ((strBandwidth2 != "") && (strMode2 != ""))
                            {
                                intW1 = 1;
                            }
                        }
                        strFequency = "";
                        strChannel = "";

                        if (intW1 == 1)
                        {
                            intI = intI + 2;
                            while ((dt2.Rows[intI][0].ToString().IndexOf("+ CH") > 0) || (dt2.Rows[intI][0].ToString().IndexOf("+  CH") > 0))
                            //if (dt.Rows[intI][0].ToString().IndexOf("MHz") > 0)
                            {
                                strFequency = "";
                                string[] strChannel3 = dt2.Rows[intI][0].ToString().Split('+');
                                strChannel = strChannel3[0];
                                strChannel2 = strChannel3[1];
                                //strChannel = dt2.Rows[intI][0].ToString();
                                for (intJ = 1; intJ < 10; intJ++)
                                {
                                    strDirection = dt2.Rows[intDirection][intJ].ToString();

                                    //strThroughput[intJ] = dt.Rows[intI][intJ + 2].ToString();
                                    strNumber = dt2.Rows[intI][intJ].ToString();
                                    if ((strNumber != "N/S") && (strMaxID != "0") && (strNumber != "") && (strNumber != "N/A") && (strNumber != "N/T"))
                                        clsBM.InsertOTADataToSQL(strMaxID, strID, strMode, strBand, strBandMode, strBandwidth, strDirection, strFequency, strChannel, strChannel2, strNumber);
                                    intJ = intJ + 1;
                                    strNumber = "";
                                    strNumber = dt2.Rows[intI][intJ].ToString();
                                    if ((strNumber != "N/S") && (strMaxID != "0") && (strNumber != "") && (strNumber != "N/A") && (strNumber != "N/T"))
                                        clsBM.InsertOTADataToSQL(strMaxID, strID, strMode2, strBand2, strBandMode, strBandwidth2, strDirection, strFequency, strChannel, strChannel2, strNumber);
                                    intJ = intJ + 1;
                                }
                                intI++;
                            }
                        }
                    }
                    intI1++;
                }
            }
        }
        objConn.Close();

    }
    #endregion
    //=================================debbie SIT Benchmark 20180517====================================
    protected void lbtnForm_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Application_LTE.aspx?ID=" + Session["ID"].ToString() + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);

    }
}
