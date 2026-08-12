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

public partial class WebForm_TaskEdit : System.Web.UI.Page
{
    //public static string strID;
    //public static string strName;
    //public static string strKind;
    //public static string strProjectCaseID;
    public static string strStart;
    public static string strEnd;
    //public static string strCase;
    //public static int intAdd;
    //public static string strAssign;
    //public static string strLocation_P;
    //public static string strProjectKind;
    //public static string strFun;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            string strV = Request.QueryString["V"];

            DataTable dt1 = clsData.getEmployees("0", "");
            loadDepartment(this.ddlDepartment);
            listLeft.DataSource = dt1;
            listLeft.DataBind();
            if (Session["Fun"].ToString() == "9")
            {
                Name1.Visible = false;
            }
            else
            {
                Name1.Visible = true;
            }
            //loadAssign(this.ddlAssign);
            if (strV != "A")
            {
                //HttpCookie cookie_Customer = Request.Cookies["Project"];
                //string strID = cookie_Customer.Values["ID"];
                //strFun = cookie_Customer.Values["Fun"];

                //HttpCookie cookie_CaseID = Request.Cookies["CaseID"];
                //string strProjectCaseID = Server.UrlDecode(cookie_CaseID.Value);

                //strFun = Request.QueryString["Fun"];
                //strID = Request.QueryString["ID"];
                //string strItemName = Request.QueryString["Value"];
                //strKind = Request.QueryString["Kind"];
                //strCase = Request.QueryString["Case"];
                //strProjectCaseID = Request.QueryString["CaseID"];
                //HttpCookie cookie_Location_P = Request.Cookies["Location"];
                //string strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

                //HttpCookie cookie_ProjectKind = Request.Cookies["ProjectKind"];
                //string strProjectKind = Server.UrlDecode(cookie_ProjectKind.Value);

                //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
                //string strName = Server.UrlDecode(cookie_CaseName.Value);

                string strItemName = Session["ItemName"].ToString();
                //HttpCookie cookie_ItemName = new HttpCookie("ItemName");
                //cookie_ItemName.Value = Server.UrlEncode(strItemName);
                ////cookie_CaseName.Expires = DateTime.Now.AddDays(1);
                //Response.Cookies.Add(cookie_ItemName);

                //strID = "20141217151651";
                //strName = "";
                //strKind = "Bluetooth";

                //loadAssign(this.ddlAssign);

                if (strItemName != "")
                {
                    //txtTask.Enabled = false;
                    //txtCaseID.Enabled = false;
                    //intAdd = 0;
                    getProjectTask();
                }
                else
                {
                    //txtTask.Enabled = true;
                    // txtCaseID.Enabled = true;
                    //intAdd = 1;
                }
            }
        }
    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlDepartment(DDL, Session["AppNo"].ToString(), "0");
    }
    #endregion

    private void getProjectTask()
    {
        string strStart1, strEnd1;
        string strDate;
        DateTime dTime;
        DateTime dt1;

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
        // DataTable dt = clsData.UploadProjectTaskCaseID(strID, strKind, strName,strProjectCaseID);
        //DataTable dt = clsData.UploadProjectTask(Session["ID"].ToString(), Session["CaseName"].ToString(), Session["ItemName"].ToString(), Session["CaseID"].ToString());
        DataTable dt = clsData.UploadProjectTask(Session["ID"].ToString(), Session["CaseID"].ToString());
        //DataTable dt = clsData.UploadProjectTask(strID, strKind, strName);

        // strProjectCaseID = dt.Rows[0]["id"].ToString();
        //txtCaseID.Text = strProjectCaseID;
        txtTask.Text = Session["ItemName"].ToString();

        string strRelated = dt.Rows[0]["assign"].ToString();
        string[] sArray = strRelated.Split(',');
        foreach (string i in sArray)
        {
            this.listRight.Items.Add(i);
            this.listLeft.Items.Remove(i);
        }
        //ddlAssign.Text = dt.Rows[0]["assign"].ToString();
        string strAssign = dt.Rows[0]["assign"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["Start_Date1"].ToString());
        strStart1 = dt1.ToString("yyyy/MM/dd");
        if (strStart1 == "1900/01/01")
            strStart = "";
        else
            strStart = strStart1;

        dt1 = Convert.ToDateTime(dt.Rows[0]["End_Date1"].ToString());
        strEnd1 = dt1.ToString("yyyy/MM/dd");
        if (strEnd1 == "1900/01/01")
            strEnd = "";
        else
            strEnd = strEnd1;

        ddlResult.Text = dt.Rows[0]["result"].ToString();
        ddlStatus.Text = dt.Rows[0]["Status"].ToString();
        ddlProgress.Text = dt.Rows[0]["Progress"].ToString();
        txtNote.Text = dt.Rows[0]["explain_case"].ToString();
        ddlDepartment.Text = dt.Rows[0]["Sub_PU"].ToString();
        txtModelName.Text = dt.Rows[0]["Model_Name"].ToString();
        txtLab.Text = dt.Rows[0]["Lab"].ToString();
        txtQuoted.Text = dt.Rows[0]["Quoted"].ToString();
        txtReimburse.Text = dt.Rows[0]["Reimburse"].ToString();
    }

    #region loadAssign
    protected void loadAssign(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL, "0");
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        UpDate_ProjectTask();

    }

    #region Add/Update ProjectTask (新增/更新Project資訊)
    private void UpDate_ProjectTask()
    {
        string strAssign1, strStatus, strStatus1 = "", strResult, strProgress, strStart1, strEnd1, strExplain, strPU, strModel, strLocation = "", strLab, strQuoted, strReimburse;
        DateTime dt;
        string strToday;
        double intNumber = 0;
        string strTaskID;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        //HttpCookie cookie_ItemName = Request.Cookies["ItemName"];
        //string strItemName = Server.UrlDecode(cookie_ItemName.Value);

        //HttpCookie cookie_CaseID = Request.Cookies["CaseID"];
        //string strProjectCaseID = Server.UrlDecode(cookie_CaseID.Value);

        //HttpCookie cookie_ProjectKind = Request.Cookies["ProjectKind"];
        //string strProjectKind = Server.UrlDecode(cookie_ProjectKind.Value);

        string strAssign = "";

        if ((ddlStatus.Text == "Close") && (ddlResult.Text == ""))
        {
            clsMsg.AlertMessage("結果判定不得為空白....", this.Page);
        }
        else
        {
            string strV = Request.QueryString["V"];

            if (strV != "A")
            {
                //DataTable dt2 = clsData.UploadProjectTask(Session["ID"].ToString(), Session["CaseName"].ToString(), Session["ItemName"].ToString(), Session["CaseID"].ToString());
                DataTable dt2 = clsData.UploadProjectTask(Session["ID"].ToString(), Session["CaseID"].ToString());
                if (dt2.Rows.Count != 0)
                {
                    strAssign = dt2.Rows[0]["assign"].ToString();
                    strStatus1 = dt2.Rows[0]["Status"].ToString();
                }

            }

            DataTable dt3 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
            if (dt3.Rows.Count > 0)
                strLocation = dt3.Rows[0]["Accepted_Team"].ToString();

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
            // strProjectCaseID = txtCaseID.Text.Trim();
            Session["ItemName"] = txtTask.Text.Trim();
            //strAssign1 = ddlAssign.Text;
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
            strStatus = ddlStatus.Text;
            strExplain = txtNote.Text.Trim();
            strProgress = ddlProgress.Text;
            strResult = ddlResult.Text;
            strPU = ddlDepartment.Text;
            strModel = txtModelName.Text.Trim();
            strLab = txtLab.Text.Trim();
            strQuoted = txtQuoted.Text.Trim();
            strReimburse = txtReimburse.Text.Trim();


            if (strV != "A")
            {
                if (clsTransaction.UpdateProjectCaseFunctionData(Session["ID"].ToString(), Session["CaseID"].ToString(), Session["ItemName"].ToString(), strItems, strStart1, strEnd1, strResult, strStatus, strExplain, strProgress, strPU, strModel, strLab, strQuoted, strReimburse) == true)
                {
                    //if (intAdd == 0)
                    if (Session["ProjectKind"].ToString() == "驗証申請")
                    {
                        //if (ddlAssign.Text.Trim() != "")
                        //{

                        //    if (strAssign != ddlAssign.Text)
                        //        MailData(strStart1, strEnd1, "0");
                        //}
                        if (strItems != "")
                        {
                            if (strAssign != strItems)
                                MailData(strStart1, strEnd1, "0", strItems, strLocation);
                        }

                        if ((strEnd != Request["date2"].ToString()) && (strEnd != ""))
                            MailData(strEnd, strEnd1, "2", strItems, strLocation);
                    }

                    clsMsg.AlertMessage("修改成功....", this.Page);
                    //else
                    //{
                    //    clsMsg.AlertMessage("新增成功....", this.Page);
                    //}

                }
                else
                {
                    //if (intAdd == 0)
                    clsMsg.AlertMessage("修改失敗....", this.Page);
                    //else
                    //    clsMsg.AlertMessage("新增失敗....", this.Page);
                }
                //Server.Transfer("~/WebForm/ProjectTask.aspx");

                if (Session["ProjectKind"].ToString() == "驗証申請")
                {
                    if (ddlStatus.Text != strStatus1)
                    {
                        MailData(ddlStatus.Text, "", "1", strItems, strLocation);
                    }
                }
                Server.Transfer("~/WebForm/ProjectTask.aspx");
            }
            else
            {
                string strFirst, strLast;
                DataTable dt1 = clsData.UploadProjectTaskID(Session["ID"].ToString(), Session["CaseName"].ToString());

                foreach (DataRow dr in dt1.Rows)
                {
                    intNumber = Convert.ToInt32(dr["ID"].ToString()) + 1;
                }
                //strFirst = (intNumber.ToString()).Substring(0, 1);
                //strLast = (intNumber.ToString()).Substring(1);

                //strTaskID = strFirst + (Convert.ToInt32(strLast) + 1).ToString();
                strTaskID = (intNumber + 1).ToString();

                if (clsTransaction.InsertProjectCase(strTaskID, Session["ID"].ToString(), Session["CaseName"].ToString(), Session["ItemName"].ToString(), strItems, strStart1, strEnd1, strResult, strStatus, strExplain, strProgress, strProgress, "", strPU, strModel, strLab, strQuoted, strReimburse) == true)
                {
                    if (Session["ProjectKind"].ToString() == "驗証申請")
                        MailData(strStart1, strEnd1, "0", strItems, strLocation);
                    clsMsg.AlertMessage("新增成功....", this.Page);
                }
                else
                    clsMsg.AlertMessage("新增失敗....", this.Page); ;

                Server.Transfer("~/WebForm/ProjectCase.aspx?Value=R");
            }
        }

        //if (Session["ProjectKind"].ToString() == "驗証申請")
        //{
        //    //if ((ddlStatus.Text == "Close") || (ddlStatus.Text == "Hold"))
        //    //{
        //    //    MailData(ddlStatus.Text, "", "1", "");
        //    //}

        //          }

    }
    #endregion

    #region MailData
    private void MailData(string strStart1, string strEnd1, string strKind, string strItem, string strLocation)
    {


        DateTime dt;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_Location_P = Request.Cookies["Location"];
        //string strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

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
                    string[] sArray = strItem.Split(',');
                    foreach (string i in sArray)
                    {
                        if (i != "")
                        {
                            DataTable dt3 = clsData.getEmployees("1", i);
                            if (dt3.Rows.Count != 0)
                            {
                                string strMail1 = dt3.Rows[0]["Email"].ToString();
                                DataTable dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
                                string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + txtTask.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", Session["ID"].ToString());
                                clsTransaction.SendMail(strMail1, MailSubject, strBody);
                            }
                        }

                    }
                    //DataTable dt1 = clsData.getEmployees("1", ddlAssign.Text);
                    //string strMail1 = dt1.Rows[0]["Email"].ToString();
                    //DataTable dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");


                    //string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + txtTask.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                    //clsTransaction.SendMail(strMail1, MailSubject, strBody);
                }
                else
                {
                    DataTable dt1 = clsData.UploadLeader("1", Session["Location"].ToString(), "");
                    string strMail1 = dt1.Rows[0]["Email"].ToString();
                    DataTable dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");


                    string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + txtTask.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", Session["ID"].ToString());

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
            DataTable dt3;
            DataTable dt1 = clsData.UploadLeader("4", Session["Location"].ToString(), "");

            DataTable dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");

            string[] sArray = strItem.Split(',');

            if (strLocation == "吳江")
            {
                dt3 = clsData.UploadLeader("3", Session["Location"].ToString(), "DA40-WJ");
            }
            else
            {
                dt3 = clsData.UploadLeader("3", Session["Location"].ToString(), "DA40");
            }

            string strBody = string.Format(strMailBody, strStart1, dt2.Rows[0]["Name"].ToString(), strKind + "-" + txtTask.Text.Trim(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", Session["ID"].ToString());

            for (int i = 0; i < 5; i++)
            {
                if ((i == 0) && (dt1.Rows.Count > 0))
                    strMail1 = dt1.Rows[0]["Email"].ToString();
                else if ((i == 2) && (dt3.Rows.Count > 0))//台北吳江Leader
                    strMail1 = dt3.Rows[0]["Email"].ToString();
                else if ((i == 1) && (dt2.Rows.Count > 0))
                    strMail1 = dt2.Rows[0]["A_mail"].ToString();
                else if ((i == 3) && (dt2.Rows.Count > 0))   //實驗室負責人
                {
                    if (dt2.Rows[0]["assign"].ToString() != "")
                    {
                        DataTable dt4 = clsData.getEmployees("1", dt2.Rows[0]["assign"].ToString());
                        strMail1 = dt4.Rows[0]["Email"].ToString();
                    }
                    else
                        strMail1 = "";
                }
                //else if ((i == 4) && (dt2.Rows.Count > 0)) //DQA負責人
                //{
                //    if (dt2.Rows[0]["DQA"].ToString() != "")
                //    {
                //        DataTable dt4 = clsData.UploadDQA("Q600(品保總部)", dt2.Rows[0]["DQA"].ToString());
                //        strMail1 = dt4.Rows[0]["Mail"].ToString();
                //    }
                //    else
                //        strMail1 = "";
                //}
                else
                    strMail1 = "";

                clsTransaction.SendMail(strMail1, MailSubject, strBody);
            }
            foreach (string i in sArray) //被assign的工程師
            {
                if (i != "")
                {
                    DataTable dt4 = clsData.getEmployees("1", i);
                    if (dt4.Rows.Count != 0)
                    {
                        strMail1 = dt4.Rows[0]["Email"].ToString();

                        clsTransaction.SendMail(strMail1, MailSubject, strBody);
                    }
                }

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

            DataTable dt3;
            string strMail1;
            DataTable dt1 = clsData.UploadLeader("1", Session["Location"].ToString(), "");

            DataTable dt2 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
            if (strLocation == "吳江")
            {
                dt3 = clsData.UploadLeader("3", Session["Location"].ToString(), "DA40-WJ");
            }
            else
            {
                dt3 = clsData.UploadLeader("3", Session["Location"].ToString(), "DA40");
            }

            string strBody = string.Format(strMailBody, dt2.Rows[0]["Name"].ToString(), strKind + "-" + txtTask.Text.Trim(), strStart1, strEnd1, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", Session["ID"].ToString());

            for (int i = 0; i < 4; i++)
            {
                if ((i == 0) && (dt1.Rows.Count > 0))
                    strMail1 = dt1.Rows[0]["Email"].ToString();
                else if ((i == 1) && (dt2.Rows.Count > 0))
                    strMail1 = dt2.Rows[0]["A_mail"].ToString();
                else if ((i == 2) && (dt3.Rows.Count > 0))//台北吳江Leader
                    strMail1 = dt3.Rows[0]["Email"].ToString();
                else if ((i == 3) && (dt2.Rows.Count > 0))   //實驗室負責人
                {
                    if (dt2.Rows[0]["assign"].ToString() != "")
                    {
                        DataTable dt4 = clsData.getEmployees("1", dt2.Rows[0]["assign"].ToString());
                        strMail1 = dt4.Rows[0]["Email"].ToString();
                    }
                    else
                        strMail1 = "";
                }
                else
                    strMail1 = "";

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
        //Server.Transfer("~/WebForm/ProjectTask.aspx?ID=" + strID + "&Value=" + strName + "&Kind=" + strKind + "&Case=" + strCase);
        string strV = Request.QueryString["V"];

        if (strV == "A")
            Server.Transfer("~/WebForm/ProjectCase.aspx?Value=R");
        else
            Server.Transfer("~/WebForm/ProjectTask.aspx");
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
