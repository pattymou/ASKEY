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

public partial class WebForm_ProjectEdit : System.Web.UI.Page
{
    public static string strAKind;
    public static string strStart;
    public static string strEnd;
    public static string strSample;
    //public static int intAdd;
    //public static string strFun;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            Session["FileN"] = "";
            //clsParameter.strUpload_Kind = "Lab";
            //loadProjectKind(this.ddlKind);
            loadCustomer(this.ddlCustomer);
            loadNPI(this.ddlNPI);
            loadEmployees(this.ddlAssign);
            loadDepartment(this.ddlDepartment);
            loadDepartment(this.ddlDepartment2);
            loadTeam(this.ddlTeam);
            loadNumber(this.ddlDQA, "Q600(品保總部)");

            rdoLocal.Checked = true;
            //ddlKind.Visible = false;
            //Label2.Visible = false;
            string strID;
            //HttpCookie cookie_Customer = Request.Cookies["Project"];
            string strA = Request.QueryString["A"];
            if (strA != "0")
            {
                strID = "";


            }
            else
            {
                strID = Session["ID"].ToString();
                txtName.Enabled = false;

            }
            //strID = cookie_Customer.Values["ID"];
            //strFun = cookie_Customer.Values["Fun"];
            //strFun = Request.QueryString["Fun"];
            //strID = Request.QueryString["ID"];
            //strID = "20141218120506";
            //strID = "";
            HttpCookie cookie_ApplicationID = new HttpCookie("ApplicationID");
            cookie_ApplicationID.Value = Server.UrlEncode(strID);
            //cookie_ApplicationID.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie_ApplicationID);
            //clsParameter.strApplicationID = strID;
            //strID = "20141205174840";
            //strID = "";
            DataTable dt1 = clsData.getEmployees("0", "");

            listLeft.DataSource = dt1;
            listLeft.DataBind();
            //strFun = Request.QueryString["Fun"];

            if (strID != "")
            {
                //intAdd = 0;
                getProject();
            }
            else
            {

                DataTable dt2 = clsData.getFunction_Name(Session["Fun"].ToString());

                lblKind.Text = dt2.Rows[0]["Function_Name"].ToString();

                strStart = "";
                strEnd = "";
                strSample = "";
                //intAdd = 1;
            }

            Session["Assign"] = ddlAssign.Text;


        }
        //GvQuery(false);
    }

    #region loadProjectKind
    protected void loadProjectKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 6, "0");
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, "0");
    }
    #endregion

    #region loadNPI
    protected void loadNPI(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 2, "0");
    }
    #endregion

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 4, "0");
    }
    #endregion

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL, "0");
    }
    #endregion

    #region loadNumber
    protected void loadNumber(DropDownList DDL, string strDepartment)
    {
        clsDropDownList.ddlNumberD(DDL, strDepartment, "0");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    #region getProject (取得Project資訊)
    private void getProject()
    {
        string strStart1, strEnd1, strSample1;
        ////strID = Request.QueryString["ID"];
        //strID = "20141107164955";
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];
        DataTable dt = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
        DateTime dt1;

        txtName.Text = dt.Rows[0]["Name"].ToString();
        //ddlKind.Text = dt.Rows[0]["Kind"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        ddlAssign.Text = dt.Rows[0]["Assign"].ToString();
        ddlProgress.Text = dt.Rows[0]["Progress"].ToString();
        ddlDQA.Text = dt.Rows[0]["DQA"].ToString();
        if (dt.Rows[0]["Accepted_Team"].ToString() == "台北")
            rdoLocal.Checked = true;
        else
            rdoLocal1.Checked = true;

        dt1 = Convert.ToDateTime(dt.Rows[0]["Start_Date"].ToString());
        strStart1 = dt1.ToString("yyyy/MM/dd");
        if (strStart1 == "1900/01/01")
            strStart = "";
        else
            strStart = strStart1;

        dt1 = Convert.ToDateTime(dt.Rows[0]["End_Date"].ToString());
        strEnd1 = dt1.ToString("yyyy/MM/dd");
        if (strEnd1 == "1900/01/01")
            strEnd = "";
        else
            strEnd = strEnd1;

        lblEnd.Text = strEnd;

        dt1 = Convert.ToDateTime(dt.Rows[0]["Sample_Ready_Date"].ToString());
        strSample1 = dt1.ToString("yyyy/MM/dd");
        if (strSample1 == "1900/01/01")
            strSample = "";
        else
            strSample = strSample1;

        //strStart = dt.Rows[0]["Start_Date"].ToString();
        //strEnd = dt.Rows[0]["End_Date"].ToString();
        ddlResult.Text = dt.Rows[0]["Result"].ToString();
        ddlStatus.Text = dt.Rows[0]["Status"].ToString();
        txtExplain.Text = dt.Rows[0]["Explain"].ToString();
        strAKind = dt.Rows[0]["Project_Kind"].ToString();


        txtPM.Text = dt.Rows[0]["PM"].ToString();
        txtSW.Text = dt.Rows[0]["SW_Engineer"].ToString();
        txtHW.Text = dt.Rows[0]["HW_Engineer"].ToString();
        txtMechanical.Text = dt.Rows[0]["Mechanical_Engineer"].ToString();
        txtDSP.Text = dt.Rows[0]["DSP_Model"].ToString();
        txtFW.Text = dt.Rows[0]["FW_Version"].ToString();
        txtWireless.Text = dt.Rows[0]["WirelessDrive"].ToString();
        txtProductName.Text = dt.Rows[0]["Customer_Product_Name"].ToString();
        txtH_Version.Text = dt.Rows[0]["PCB_Version"].ToString();
        txtChipset.Text = dt.Rows[0]["Chipset"].ToString();
        txtMAC.Text = dt.Rows[0]["Sample_Mac_address"].ToString();
        txtUtility.Text = dt.Rows[0]["Utility_Version"].ToString();
        ddlCustomer.Text = dt.Rows[0]["Customer"].ToString();
        ddlNPI.Text = dt.Rows[0]["NPI"].ToString();
        ddlDepartment.Text = dt.Rows[0]["A_Department"].ToString();
        ddlDepartment2.Text = dt.Rows[0]["A_Department2"].ToString();
        if (dt.Rows[0]["Kind"].ToString() != "驗証申請")
        {
            ddlDepartment2.Visible = false;
        }
        else
        {
            ddlDepartment2.Visible = true;
        }
        ddlTeam.Text = dt.Rows[0]["Team"].ToString();
        txtJira.Text = dt.Rows[0]["Jira"].ToString();

        string strRelated = dt.Rows[0]["Related"].ToString();
        string[] sArray = strRelated.Split(',');
        foreach (string i in sArray)
        {
            this.listRight.Items.Add(i);
            this.listLeft.Items.Remove(i);
        }
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {


        UpDate_Project();
    }

    #region Add/Update Project (新增/更新Project資訊)
    private void UpDate_Project()
    {
        string strStart1, strEnd1, strSample1, strName, strKind, strAssign, strCustomer, strPM, strSW, strHW, strMechanical, strA_Department, strA_Department2, strFW, strWireless, strProduct, strNPI, strPCB, strChipset, strMac, strUtility, strDSP, strResult, strStatus, strExplain, strProgress, strJira;
        string strToday;
        string strToday1;
        string strTeam;
        string strAccepted;
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        DateTime dt;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        if (rdoLocal.Checked == true)
            strAccepted = "台北";
        else
            strAccepted = "吳江";

        strStart1 = Request["date1"].ToString();
        if (strStart1 != "")
        {
            dt = Convert.ToDateTime(strStart1);
            strStart1 = dt.ToString("yyyyMMdd");
        }

        strEnd1 = Request["date2"].ToString();
        if (strEnd1 != "")
        {
            dt = Convert.ToDateTime(strEnd1);
            strEnd1 = dt.ToString("yyyyMMdd");
        }

        strSample1 = Request["date3"].ToString();
        if (strSample1 != "")
        {
            dt = Convert.ToDateTime(strSample1);
            strSample1 = dt.ToString("yyyyMMdd");
        }

        strName = txtName.Text.Trim();
        //strKind = ddlKind.Text;
        strKind = lblKind.Text;
        strAssign = ddlAssign.Text;
        strCustomer = ddlCustomer.Text;
        strPM = txtPM.Text.Trim();
        strSW = txtSW.Text.Trim();
        strHW = txtHW.Text.Trim();
        strMechanical = txtMechanical.Text.Trim();
        strA_Department = ddlDepartment.Text.Trim();
        strA_Department2 = ddlDepartment2.Text.Trim();
        strFW = txtFW.Text.Trim();
        strWireless = txtWireless.Text.Trim();
        strProduct = txtProductName.Text.Trim();
        strNPI = ddlNPI.Text;
        strPCB = txtH_Version.Text.Trim();
        strChipset = txtChipset.Text.Trim();
        strMac = txtMAC.Text.Trim();
        strUtility = txtUtility.Text.Trim();
        strDSP = txtDSP.Text.Trim();
        strResult = ddlResult.Text.Trim();
        strStatus = ddlStatus.Text;
        strExplain = txtExplain.Text.Trim();
        strProgress = ddlProgress.Text;
        strTeam = ddlTeam.Text;
        strJira = txtJira.Text;

        int count = listRight.Items.Count;
        string strItems = "";

        for (int i = 0; i < count; i++)
        {
            ListItem item = listRight.Items[i];

            if (strItems == "")
                strItems = item.Value;
            else
                strItems = strItems + "," + item.Value;

        }
        string strA = Request.QueryString["A"];
        if (strA == "0")
        {
            if (clsTransaction.UpdateProjectFunctionData(Session["ID"].ToString(), strName, strKind, strAssign, strCustomer, strPM, strSW, strHW, strMechanical, strA_Department, strFW, strWireless, strProduct, strNPI, strPCB, strChipset, strMac, strUtility, strDSP, strStart1, strEnd1, strResult, strStatus, strExplain, strProgress, strSample1, strTeam, strItems, strJira, ddlDQA.Text, strAccepted, strA_Department2) == true)
            {
                string strScrFilePath, strDestFilePath, strProjectCase_Kind;
                string strProjectName_Cookie;

                //HttpCookie cookie_ProjectName = Request.Cookies["ProjectName"];
                //strProjectName_Cookie = Server.UrlDecode(cookie_ProjectName.Value);
                strScrFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + Session["ProjectName"].ToString();
                strDestFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + strName;

                if (strScrFilePath != strDestFilePath)
                {
                    if (System.IO.Directory.Exists(strScrFilePath))
                    {
                        System.IO.Directory.Move(strScrFilePath, strDestFilePath);

                        DataTable dt_ProjectCase_Kind = clsData.SelectProjectCase_Kind(Session["ID"].ToString());
                        strProjectCase_Kind = dt_ProjectCase_Kind.Rows[0]["ProjectCase_Kind"].ToString();
                        strScrFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + Session["ProjectName"].ToString() + @"\" + strProjectCase_Kind;
                        strDestFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + strName + @"\" + strProjectCase_Kind;
                        clsTransaction.UpDatePath(strDestFilePath, strScrFilePath, " ", Session["ID"].ToString());

                        //   HttpCookie cookie_ProjectName = new HttpCookie("ProjectName");
                        Session["ProjectName"] = strName;
                        //cookie_ProjectName.Value = Server.UrlEncode(strName);
                        //Response.Cookies.Add(cookie_ProjectName);
                    }
                }

                if (strStatus == "Close")
                    MailData();

                MailData_Date();
                clsMsg.AlertMessage("修改成功....", this.Page);
                listRight.Items.Clear();
                getProject();
            }
            else
                clsMsg.AlertMessage("修改失敗....", this.Page);
        }
        else
        {
            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");
            strToday1 = DateTime.Now.ToString("yyyyMMdd");
            if (clsTransaction.InsertProject(strToday, strName, strAccepted, strCustomer, strNPI, strPM, strHW, strSW, strMechanical, strFW, strWireless, strPCB, strChipset, strMac, strUtility, "", strSample1, strProduct, "", "", strA_Department, "", "", strAssign, strStart1, strEnd1, strToday1, strStatus, "", strKind, strProgress, strProgress, strResult, strExplain, strTeam, strItems, strJira, ddlDQA.Text, strA_Department2, strAKind) == true)
            {
                if (lblKind.Text == "驗証申請")
                {
                    int intJ = 10;
                    string strKind1 = "Project Information";
                    string strItem;
                    for (int intI = 0; intI < 5; intI++)
                    {
                        if (intI == 0)
                            strItem = "RFQ";
                        else if (intI == 1)
                            strItem = "Release Note";
                        else if (intI == 2)
                            strItem = "Spec";
                        else if (intI == 3)
                            strItem = "FW Version";
                        else
                            strItem = "Schedule";

                        clsTransaction.InsertProjectCase(intJ.ToString(), strToday, strKind1, strItem, "", "", "", "", "", "", "", "", "", "", "","","","");
                        intJ = intJ + 1;
                    }
                }
                //MailData_Date();
                clsMsg.AlertMessage("新增成功....", this.Page);
                Session["ID"] = strToday;
                getProject();
            }
            else
                clsMsg.AlertMessage("新增失敗....", this.Page);
        }
        Session["FileN"] = "";

        if (Session["Assign"] == "")
        {
            if (ddlAssign.Text != "")
                MailData_Assign();
        }

        //DataTable dt1 = clsData.UploadProjectFileQuery(strID);
        //this.gvwMain.DataSource = dt1;
        //this.DataBind();
    }
    #endregion

    #region MailData
    private void MailData()
    {
        DateTime dt;

        //mail標題
        string MailSubject = "系統驗證申請單通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_ProjectStatus.txt");
        string strMailBody = myMailBody.ReadToEnd();


        #region 找資料塞到SendMail內

        DataTable dt5 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
        string strMail = dt5.Rows[0]["A_mail"].ToString();

        DateTime dt1;
        string strDate;

        dt1 = Convert.ToDateTime(dt5.Rows[0]["Application_Date"].ToString());
        strDate = dt1.ToString("yyyy/MM/dd");
        string strBody = string.Format(strMailBody, strDate, dt5.Rows[0]["Name"].ToString(), dt5.Rows[0]["Customer"].ToString(), dt5.Rows[0]["NPI"].ToString(), "<br>", "<font face=arial size=3 color=#3333ff>", "</font>", Session["ID"].ToString());

        if (strMail != "")
        {
            clsTransaction.SendMail(strMail, MailSubject, strBody);
        }
        if (ddlDQA.Text != "")//通知DQA
        {
            DataTable dt4 = clsData.UploadDQA("Q600(品保總部)", ddlDQA.Text);
            strMail = dt4.Rows[0]["Mail"].ToString();

            clsTransaction.SendMail(strMail, MailSubject, strBody);
        }

        myMailBody.Close();
        myMailBody.Dispose();

        #endregion
    }
    #endregion

    #region MailData
    private void MailData_Assign()
    {
        DateTime dt;

        //mail標題
        string MailSubject = "系統驗證申請單通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Application1.txt");
        string strMailBody = myMailBody.ReadToEnd();


        #region 找資料塞到SendMail內


        DataTable dt1 = clsData.getEmployees("1", ddlAssign.Text);
        string strMail = dt1.Rows[0]["Email"].ToString();
        if (strMail != "")
        {

            DataTable dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");


            string strBody = string.Format(strMailBody, Session["ID"].ToString(), dt2.Rows[0]["A_Name"].ToString(), dt2.Rows[0]["A_mail"].ToString(), dt2.Rows[0]["A_Ext"].ToString(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", txtName.Text);

            clsTransaction.SendMail(strMail, MailSubject, strBody);
        }

        myMailBody.Close();
        myMailBody.Dispose();

        #endregion
    }
    #endregion

    #region MailData
    private void MailData_Date()
    {
        DateTime dt;

        //mail標題
        string MailSubject = "系統驗證申請單通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_ProjectDate.txt");
        string strMailBody = myMailBody.ReadToEnd();


        #region 找資料塞到SendMail內

        string strEnd1;

        strEnd1 = Request["date2"].ToString();
        if (strEnd1 != "")
        {
            dt = Convert.ToDateTime(strEnd1);
            strEnd1 = dt.ToString("yyyy/MM/dd");
        }

        if (strEnd1 != lblEnd.Text)
        {
            DataTable dt5 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
            string strMail = dt5.Rows[0]["A_mail"].ToString();
            if (strMail != "")
            {
                DateTime dt1;
                string strDate;

                dt1 = Convert.ToDateTime(dt5.Rows[0]["Application_Date"].ToString());
                strDate = dt1.ToString("yyyy/MM/dd");

                string strBody = string.Format(strMailBody, strDate, dt5.Rows[0]["Name"].ToString(), dt5.Rows[0]["Customer"].ToString(), dt5.Rows[0]["NPI"].ToString(), "<br>", "<font face=arial size=3 color=#3333ff>", "</font>", strEnd1, Session["ID"].ToString());

                clsTransaction.SendMail(strMail, MailSubject, strBody);
            }

            myMailBody.Close();
            myMailBody.Dispose();
        }

        #endregion
    }
    #endregion


    protected void butReturn_Click(object sender, EventArgs e)
    {
        string strA = Request.QueryString["A"];
        if (strA == "0")
            //Server.Transfer("~/WebForm/ProjectDetail.aspx");
            Server.Transfer("~/WebForm/ProjectDetail.aspx?V=R");
        else
            Server.Transfer("~/WebForm/ProjectView.aspx?Fun=" + Session["Fun"].ToString());
    }
    protected void btnRight_Click(object sender, EventArgs e)
    {
        DateTime dt; string strStart1, strEnd1, strSample1;
        strStart1 = Request["date1"].ToString();
        if (strStart1 != "")
        {
            dt = Convert.ToDateTime(strStart1);
            strStart1 = dt.ToString("yyyy/MM/dd");
        }

        strEnd1 = Request["date2"].ToString();
        if (strEnd1 != "")
        {
            dt = Convert.ToDateTime(strEnd1);
            strEnd1 = dt.ToString("yyyy/MM/dd");
        }

        strSample1 = Request["date3"].ToString();
        if (strSample1 != "")
        {
            dt = Convert.ToDateTime(strSample1);
            strSample1 = dt.ToString("yyyy/MM/dd");
        }


        ArrayList arrRight = new ArrayList();
        foreach (ListItem item in this.listLeft.Items)
        {
            if (item.Selected)
                arrRight.Add(item);
        }
        foreach (ListItem item in arrRight)
        {
            this.listRight.Items.Add(item);
            this.listLeft.Items.Remove(item);
        }
        strStart = strStart1;
        strSample = strSample1;
        strEnd = strEnd1;
        lblEnd.Text = strEnd;
        //*************************************************
        //for (int i = 0; i < listLeft.Items.Count; i++)
        //{
        //    if (listLeft.Items[i].Selected)
        //        listRight.Items.Add(listLeft.Items[i]);
        //}

        //*************************************************
        //ListItem selectedItem = listLeft.SelectedItem;
        //selectedItem.Selected = false;

        //listRight.Items.Add(selectedItem);
        //listLeft.Items.Remove(selectedItem);

        //**************************************************
        //int count = listLeft.Items.Count;
        //int index = 0;

        //for (int i = 0; i < count; i++)
        //{
        //    ListItem item = listLeft.Items[index];
        //    //ListItem item = listLeft.Items[i];


        //    if (listLeft.Items[index].Selected)
        //    //if (listLeft.Items[i].Selected == true)
        //    {
        //        listLeft.Items.Remove(item);
        //        listRight.Items.Add(item);
        //        index--;
        //    }
        //    index++;
        //}
    }
    protected void btnLeft_Click(object sender, EventArgs e)
    {

        DateTime dt; string strStart1, strEnd1, strSample1;
        strStart1 = Request["date1"].ToString();
        if (strStart1 != "")
        {
            dt = Convert.ToDateTime(strStart1);
            strStart1 = dt.ToString("yyyy/MM/dd");
        }

        strEnd1 = Request["date2"].ToString();
        if (strEnd1 != "")
        {
            dt = Convert.ToDateTime(strEnd1);
            strEnd1 = dt.ToString("yyyy/MM/dd");
        }

        strSample1 = Request["date3"].ToString();
        if (strSample1 != "")
        {
            dt = Convert.ToDateTime(strSample1);
            strSample1 = dt.ToString("yyyy/MM/dd");
        }

        ArrayList arrLeft = new ArrayList();
        foreach (ListItem item in this.listRight.Items)
        {
            if (item.Selected)
                arrLeft.Add(item);
        }
        foreach (ListItem item in arrLeft)
        {
            this.listLeft.Items.Add(item);
            this.listRight.Items.Remove(item);
        }
        strStart = strStart1;
        strSample = strSample1;
        strEnd = strEnd1;
        lblEnd.Text = strEnd;
    }

    protected void listRight_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        // Get the currently selected item in the ListBox.
        //string curItem = listRight.SelectedItem.ToString();


    }
}
