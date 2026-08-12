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

public partial class WebForm_ModifyDashBoard : System.Web.UI.Page
{
    public static string strStart;
    public static string strEnd;
    public static string strCStart;
    public static string strCEnd;
    public static string strSample;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadCustomer(this.ddlCustomer);
            loadNPI(this.ddlNPI);
            loadEmployees(this.ddlAssign);
            //loadEmployees(this.ddlCAssign);
            //loadDepartment(this.ddlDepartment);
            DataTable dt1 = clsData.getEmployees("0", "");
            loadDepartment(this.ddlDepartment);
            loadDepartment(this.ddlDepartment2);
            listLeft.DataSource = dt1;
            listLeft.DataBind();

            loadTeam(this.ddlTeam);

            getProject();
            getProjectTask();
        }
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

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        UpDate_Project();
        getProject();
        getProjectTask();
    }

    #region Update Project (更新Project資訊)
    private void UpDate_Project()
    {
        string strStart1, strEnd1, strSample1, strName, strKind, strAssign, strCustomer, strPM, strSW, strHW, strMechanical, strA_Department, strFW, strWireless, strProduct, strNPI, strPCB, strChipset, strMac, strUtility, strDSP, strResult, strStatus, strExplain, strProgress, strA_Department2;
        string strToday;
        string strToday1;
        string strTeam;
        string strAccepted;
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        DateTime dt;

        string strPID, strCID;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        strAccepted = "台北";
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

        strName = lblID.Text.Trim();
        //strKind = ddlKind.Text;
        strKind = lblKind.Text;
        strAssign = ddlAssign.Text;
        strCustomer = ddlCustomer.Text;
        strPM = txtPM.Text.Trim();
        strSW = txtSW.Text.Trim();
        strHW = txtHW.Text.Trim();
        strMechanical = txtMechanical.Text.Trim();
        //strA_Department = ddlDepartment.Text.Trim();
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
        strA_Department2 = ddlDepartment2.Text;

        if (clsTransaction.UpdateDashBoardFunctionData(strPID, strAssign, strCustomer, strPM, strSW, strHW, strMechanical, strFW, strWireless, strProduct, strNPI, strPCB, strChipset, strMac, strUtility, strDSP, strStart1, strEnd1, strResult, strStatus, strExplain, strProgress, strSample1, strTeam, strA_Department2) == true)
        {
            UpdateProjectTask();
            //clsMsg.AlertMessage("修改成功....", this.Page);
            //getProject();
        }
        else
            clsMsg.AlertMessage("修改失敗....", this.Page);

    }
    #endregion

    private void UpdateProjectTask()
    {
        string strAssign1, strStatus, strResult, strProgress, strStart1, strEnd1, strExplain, strLab, strQuoted, strReimburse;
        DateTime dt;
        string strAssign = "";
        string strPID, strCID;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        if ((ddlStatus.Text == "Close") && (ddlResult.Text == ""))
        {
            clsMsg.AlertMessage("結果判定不得為空白....", this.Page);
        }
        else
        {

            DataTable dt2 = clsData.UploadProjectTask(strPID, strCID);
            if (dt2.Rows.Count != 0)
                strAssign = dt2.Rows[0]["assign"].ToString();


            strStart1 = Request["date4"].ToString();
            if (strStart1 != "")
            {
                dt = Convert.ToDateTime(strStart1);
                strStart1 = dt.ToString("yyyyMMdd");
            }

            strEnd1 = Request["date5"].ToString();
            if (strEnd1 != "")
            {
                dt = Convert.ToDateTime(strEnd1);
                strEnd1 = dt.ToString("yyyyMMdd");
            }

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
            strAssign1 = strItems;
            strStatus = ddlCStatus.Text;
            strExplain = txtNote.Text.Trim();
            strProgress = ddlCProgress.Text;
            strResult = ddlCResult.Text;
            strLab = txtLab.Text;
            strQuoted = txtQuoted.Text;
            strReimburse = txtReimburse.Text;

            if (clsTransaction.UpdateProjectCaseFunctionData(strPID, strCID, lblCaseName.Text, strAssign1, strStart1, strEnd1, strResult, strStatus, strExplain, strProgress, ddlDepartment.Text, txtModelName.Text, strLab, strQuoted, strReimburse) == true)
            {
                if (lblKind.Text == "驗証申請")
                {
                    if (ddlAssign.Text.Trim() != "")
                    {

                        if (strAssign != strItems)
                            MailData(strStart1, strEnd1, "0");
                    }

                    if ((strCEnd != Request["date4"].ToString()) && (strCEnd != ""))
                        MailData(strCEnd, strEnd1, "2");
                }

                clsMsg.AlertMessage("修改成功....", this.Page);

            }
            else
            {
                clsMsg.AlertMessage("修改失敗....", this.Page);

            }
        }
    }

    private void getProject()
    {
        string strPID, strCID;
        string strStart1, strEnd1, strSample1;
        DateTime dt1;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        DataTable dt = clsData.UploadProjectQuery(strPID, "Project");

        lblID.Text = dt.Rows[0]["Name"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        ddlTeam.Text = dt.Rows[0]["Team"].ToString();
        ddlCustomer.Text = dt.Rows[0]["Customer"].ToString();
        txtPM.Text = dt.Rows[0]["PM"].ToString();
        txtSW.Text = dt.Rows[0]["SW_Engineer"].ToString();
        txtHW.Text = dt.Rows[0]["HW_Engineer"].ToString();
        txtMechanical.Text = dt.Rows[0]["Mechanical_Engineer"].ToString();
        txtDSP.Text = dt.Rows[0]["DSP_Model"].ToString();
        txtFW.Text = dt.Rows[0]["FW_Version"].ToString();
        txtWireless.Text = dt.Rows[0]["WirelessDrive"].ToString();
        txtProductName.Text = dt.Rows[0]["Customer_Product_Name"].ToString();
        ddlNPI.Text = dt.Rows[0]["NPI"].ToString();
        txtH_Version.Text = dt.Rows[0]["PCB_Version"].ToString();
        txtChipset.Text = dt.Rows[0]["Chipset"].ToString();
        txtMAC.Text = dt.Rows[0]["Sample_Mac_address"].ToString();
        txtUtility.Text = dt.Rows[0]["Utility_Version"].ToString();
        ddlDepartment2.Text = dt.Rows[0]["A_Department2"].ToString();        

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

        dt1 = Convert.ToDateTime(dt.Rows[0]["Sample_Ready_Date"].ToString());
        strSample1 = dt1.ToString("yyyy/MM/dd");
        if (strSample1 == "1900/01/01")
            strSample = "";
        else
            strSample = strSample1;

        txtNote.Text = dt.Rows[0]["Explain"].ToString();
        ddlAssign.Text = dt.Rows[0]["Assign"].ToString();
        ddlProgress.Text = dt.Rows[0]["Progress"].ToString();

        if (lblKind.Text == "驗証申請")
            Name1.Visible = false;
        else
            Name1.Visible = true;

    }

    private void getProjectTask()
    {
        string strDate;
        DateTime dTime;
        string strPID, strCID;
        string strStart1, strEnd1, strStart, strEnd;
        DateTime dt1;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        DataTable dt = clsData.UploadProjectTask_DB(strPID, strCID);



        if (dt.Rows.Count != 0)
        {

            lblCaseID.Text = strCID;
            lblCaseName.Text = dt.Rows[0]["Name"].ToString();
            lblCID.Text = dt.Rows[0]["Kind"].ToString() + " - " + dt.Rows[0]["Name"].ToString();
            // ddlCAssign.Text = dt.Rows[0]["Assign"].ToString();
            string strRelated = dt.Rows[0]["Assign"].ToString();
            string[] sArray = strRelated.Split(',');
            this.listRight.Items.Clear();
            foreach (string i in sArray)
            {
                this.listRight.Items.Add(i);
                this.listLeft.Items.Remove(i);
            }

            string strAssign1 = dt.Rows[0]["Assign"].ToString();
            dt1 = Convert.ToDateTime(dt.Rows[0]["Start_Date1"].ToString());
            strStart1 = dt1.ToString("yyyy/MM/dd");
            if (strStart1 == "1900/01/01")
                strCStart = "";
            else
                strCStart = strStart1;

            dt1 = Convert.ToDateTime(dt.Rows[0]["End_Date1"].ToString());
            strEnd1 = dt1.ToString("yyyy/MM/dd");
            if (strEnd1 == "1900/01/01")
                strCEnd = "";
            else
                strCEnd = strEnd1;

            ddlCResult.Text = dt.Rows[0]["result"].ToString();
            ddlCStatus.Text = dt.Rows[0]["Status"].ToString();
            ddlCProgress.Text = dt.Rows[0]["Progress"].ToString();
            txtNote.Text = dt.Rows[0]["explain_case"].ToString();
            ddlDepartment.Text = dt.Rows[0]["sub_pu"].ToString();
            txtModelName.Text = dt.Rows[0]["model_name"].ToString();

        }

    }

    #region MailData
    private void MailData(string strStart1, string strEnd1, string strKind)
    {
        DateTime dt;
        string strPID, strCID;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        if (strKind == "0")
        {
            //mail標題
            string MailSubject = "任務指派通知";

            //MAIL內容
            StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Project.txt");
            string strMailBody = myMailBody.ReadToEnd();


            #region 找資料塞到SendMail內

            //==sam測試
            for (int intI = 0; intI < 2; intI++)
            {
                if (intI == 0)
                {
                    DataTable dt3 = clsData.UploadProjectTask_DB(strPID, strCID);
                    if (dt3.Rows.Count > 0)
                    {
                        string strRelated = dt3.Rows[0]["Assign"].ToString();
                        string[] sArray = strRelated.Split(',');
                        foreach (string i in sArray)
                        {
                            DataTable dt1 = clsData.getEmployees("1", i);
                            string strMail1 = dt1.Rows[0]["Email"].ToString();
                            DataTable dt2 = clsData.UploadProjectQuery(strPID, "Project");

                            string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + lblCaseName.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strPID);

                            clsTransaction.SendMail(strMail1, MailSubject, strBody);
                        }
                    }
                }
                else
                {
                    DataTable dt1 = clsData.UploadLeader("1", "", "");
                    string strMail1 = dt1.Rows[0]["Email"].ToString();
                    DataTable dt2 = clsData.UploadProjectQuery(strPID, "Project");


                    string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + lblCaseName.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strPID);

                    clsTransaction.SendMail(strMail1, MailSubject, strBody);
                }
            }

            myMailBody.Close();
            myMailBody.Dispose();
            //====

            //DataTable dt1 = clsData.getEmployees("1", ddlAssign.Text);
            //string strMail1 = dt1.Rows[0]["Email"].ToString();
            //DataTable dt2 = clsData.UploadProjectQuery(strID, "Project");


            //string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + txtTask.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

            //clsTransaction.SendMail(strMail1, MailSubject, strBody);


            #endregion
        }
        else if (strKind == "1")
        {   //發給sam及申請人
            //mail標題
            string MailSubject = "專案狀態變更通知";

            //MAIL內容
            StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Project2.txt");
            string strMailBody = myMailBody.ReadToEnd();


            #region 找資料塞到SendMail內

            string strMail1;
            DataTable dt1 = clsData.UploadLeader("1", "", "");

            DataTable dt2 = clsData.UploadProjectQuery(strPID, "Project");

            string strBody = string.Format(strMailBody, strStart1, dt2.Rows[0]["Name"].ToString(), strKind + "-" + lblCaseName.Text.Trim(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strPID);

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    strMail1 = dt1.Rows[0]["Email"].ToString();
                else
                    strMail1 = dt2.Rows[0]["A_mail"].ToString();


                clsTransaction.SendMail(strMail1, MailSubject, strBody);
            }

            myMailBody.Close();
            myMailBody.Dispose();

            #endregion
        }
        else
        {
            string MailSubject = "專案預計完成日期變更通知";

            //MAIL內容
            StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Project3.txt");
            string strMailBody = myMailBody.ReadToEnd();


            #region 找資料塞到SendMail內


            string strMail1;
            DataTable dt1 = clsData.UploadLeader("1", "", "");

            DataTable dt2 = clsData.UploadProjectQuery(strPID, "Project");

            string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + lblCaseName.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", strPID);

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    strMail1 = dt1.Rows[0]["Email"].ToString();
                else
                    strMail1 = dt2.Rows[0]["A_mail"].ToString();


                clsTransaction.SendMail(strMail1, MailSubject, strBody);
            }
            myMailBody.Close();
            myMailBody.Dispose();

            #endregion
        }


    }
    #endregion

    protected void butReturn_Click(object sender, EventArgs e)
    {
        string strPID, strCID;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        Response.Redirect("~/WebForm/DashBoardDetail.aspx?PID=" + strPID + "&CID=" + strCID);
    }
    protected void btnRight_Click(object sender, EventArgs e)
    {
        DateTime dt;
        string strStart1, strEnd1;
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
        strEnd = strEnd1;

    }

    protected void btnLeft_Click(object sender, EventArgs e)
    {
        DateTime dt;
        string strStart1, strEnd1;
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
        strEnd = strEnd1;
    }

}
