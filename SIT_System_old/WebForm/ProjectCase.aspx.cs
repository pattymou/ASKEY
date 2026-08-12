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

public partial class WebForm_ProjectCase : System.Web.UI.Page
{
    //public static string strID;
    //public static string strProjectCaseID;
    //public static string strName;
    //public static string strFun;
    public static string strLocation_P;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            string strName,strV;

            Session["FileN"] = "";
            strV = Request.QueryString["Value"];

            if (strV != "R")
            {
                HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
                cookie_Upload_Kind.Value = Server.UrlEncode("Lab");
                //cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie_Upload_Kind);
                Session["Upload_Kind"] = "Lab";

                //HttpCookie cookie_Customer = Request.Cookies["Project"];
                //string strID = cookie_Customer.Values["ID"];
                //strFun = cookie_Customer.Values["Fun"];

                //strFun = Request.QueryString["Fun"];

                //HttpCookie cookie_Location_P = Request.Cookies["Location"];
                //strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

                //clsParameter.strUpload_Kind = "Lab";
                //strID = Request.QueryString["ID"];
                //strProjectCaseID = Request.QueryString["CaseID"];
                //Session["CaseID"] = Request.QueryString["CaseID"];

                Session["CaseName"] = Server.UrlDecode(Request.QueryString["Value"]);

                //HttpCookie cookie_CaseName = new HttpCookie("CaseName");
                //cookie_CaseName.Value = Server.UrlEncode(strName);
                ////cookie_CaseName.Expires = DateTime.Now.AddDays(1);
                //Response.Cookies.Add(cookie_CaseName);
            }

            DataTable dt5 = clsData.UploadProjectCaseID(Session["ID"].ToString(), Session["CaseName"].ToString(), "1", "");

            if (dt5.Rows.Count == 0)
            {
                Server.Transfer("~/WebForm/ProjectDetail.aspx");
            }
            else
            {



                //clsParameter.strCaseName = strName;

                //strID = "20141215164636";
                //strName = "DSL Interoperability";

                string strAuthority, strWrite;

                HttpCookie cookie_Authority = Request.Cookies["Authority"];
                strAuthority = Server.UrlDecode(cookie_Authority.Value);

                HttpCookie cookie_Write = Request.Cookies["Write"];
                strWrite = Server.UrlDecode(cookie_Write.Value);

                if (strAuthority == "False")
                {
                    lblAdd.Visible = false;
                    //lblDel.Visible = false;
                    butOK.Visible = false;
                }
                else
                {
                    if (strWrite == "N")
                    {
                        lblAdd.Visible = false;
                        //lblDel.Visible = false;
                        butOK.Visible = false;
                    }
                }

                DataTable dt;
                string strPath1;

                //主管用1，工程師用0
                dt = clsData.getProjectItem(Session["ID"].ToString(), Session["CaseName"].ToString(), "", "1", "Open");
                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_projectitem_Open.txt";
                //dataTableToText(dt, 6, "C:\\inetpub\\wwwroot\\SIT_System\\ajax\\data\\arays_projectitem_Open.txt");
                //dataTableToText(dt, 6, "..\\ajax\\data\\arays_projectitem_Open.txt");
                dataTableToText(dt, 8, strPath1);

                dt = clsData.getProjectItem(Session["ID"].ToString(), Session["CaseName"].ToString(), "", "1", "Close");
                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_projectitem_Close.txt";
                //dataTableToText(dt, 6, "C:\\inetpub\\wwwroot\\SIT_System\\ajax\\data\\arays_projectitem_Close.txt");
                //dataTableToText(dt, 6, "..\\ajax\\data\\arays_projectitem_Close.txt");
                dataTableToText(dt, 8, strPath1);

                dt = clsData.getProjectItem(Session["ID"].ToString(), Session["CaseName"].ToString(), "", "1", "Hold");
                strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_projectitem_Hold.txt";
                //dataTableToText(dt, 6, "C:\\inetpub\\wwwroot\\SIT_System\\ajax\\data\\arays_projectitem_Hold.txt");
                //dataTableToText(dt, 6, "..\\ajax\\data\\arays_projectitem_Hold.txt");
                dataTableToText(dt, 8, strPath1);

                getProjectItem();
            }

            if (lblName.Text.IndexOf("Certification") >= 0)
            {
                Certification.Visible = true;
                if (lblName.Text.IndexOf("WiFi") > 0)
                {
                    linkCertification_Wifi.Visible = true;
                    linkCertification_BT.Visible = false;
                    linkCertification_GCF.Visible = false;
                    linkCertification_PTCRB.Visible = false;
                }
                else if (lblName.Text.IndexOf("BT") > 0)
                {
                    linkCertification_Wifi.Visible = false;
                    linkCertification_BT.Visible = true;
                    linkCertification_GCF.Visible = false;
                    linkCertification_PTCRB.Visible = false;
                }
                else if (lblName.Text.IndexOf("GCF") > 0)
                {
                    linkCertification_Wifi.Visible = false;
                    linkCertification_BT.Visible = false;
                    linkCertification_GCF.Visible = true;
                    linkCertification_PTCRB.Visible = false;
                }
                else if (lblName.Text.IndexOf("PTCRB") > 0)
                {
                    linkCertification_Wifi.Visible = false;
                    linkCertification_BT.Visible = false;
                    linkCertification_GCF.Visible = false;
                    linkCertification_PTCRB.Visible = true;
                }


            }
            else
            {
                Certification.Visible = false;
            }
        }
    }

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        string strDate;
        DateTime dTime;
        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    if (j == 2)
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i]["start_date1"].ToString());
                        strDate = dTime.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                        else
                            strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    }
                    else if (j == 3)
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i]["end_date1"].ToString());
                        strDate = dTime.ToString("yyyy/MM/dd");
                        if (strDate != "1900/01/01")
                            strSQLFile += @"""" + strDate + @"""" + ",\r\n";
                        else
                            strSQLFile += @"""" + "" + @"""" + ",\r\n";
                    }
                    else
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

    private void getProjectItem()
    {
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        DataTable dt = clsData.UploadProjectCase(Session["ID"].ToString(), Session["CaseName"].ToString());

        lblName.Text = Session["CaseName"].ToString();

        if (dt.Rows.Count != 0)
            txtNote.Text = dt.Rows[0]["explain_kind"].ToString();

        dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), Session["CaseName"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        string strExplain, strNameNew, strProjectCase_Kind;
    
        strExplain = txtNote.Text;
        strNameNew = lblName.Text;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        if (clsTransaction.UpDateProjectItemData(strExplain, Session["CaseName"].ToString(), Session["ID"].ToString(), strNameNew) == true)
        {
            string strScrFilePath, strDestFilePath, strUpload_Project_Kind_Cookie;
            //string strProjectName_Cookie;

            //HttpCookie cookie_ProjectName = Request.Cookies["ProjectName"];
            //strProjectName_Cookie = Server.UrlDecode(cookie_ProjectName.Value);

            //HttpCookie cookie_Upload_Project_Kind = Request.Cookies["Upload_Project_Kind"];
            //strUpload_Project_Kind_Cookie = Server.UrlDecode(cookie_Upload_Project_Kind.Value);

            //strScrFilePath = @"D:\專案管理\" + strProjectName_Cookie + @"\" + strName;
            //strDestFilePath = @"D:\專案管理\" + strProjectName_Cookie + @"\" + strNameNew;

            strScrFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + @"\" + Session["ProjectName"].ToString() + @"\" + Session["CaseName"].ToString();
            strDestFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + @"\" + Session["ProjectName"].ToString() + @"\" + strNameNew;

            if (strScrFilePath != strDestFilePath)
            {
                if (System.IO.Directory.Exists(strScrFilePath))
                {
                    System.IO.Directory.Move(strScrFilePath, strDestFilePath);

                    DataTable dt_ProjectCase_Kind = clsData.SelectProjectCase_Kind(Session["ID"].ToString());

                    for (int intI = 0; intI < dt_ProjectCase_Kind.Rows.Count; intI++)
                    {
                        if ((dt_ProjectCase_Kind.Rows[intI]["File_Path"].ToString()).IndexOf("Test Report") < 0)
                        {
                            strProjectCase_Kind = dt_ProjectCase_Kind.Rows[intI]["ProjectCase_Kind"].ToString();
                            //strScrFilePath = @"D:\專案管理\" + strProjectName_Cookie + @"\"  +strProjectCase_Kind;
                            //strDestFilePath = @"D:\專案管理\" + strProjectName_Cookie + @"\" + strNameNew;
                            strScrFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + @"\" + Session["ProjectName"].ToString() + @"\" + strProjectCase_Kind;
                            strDestFilePath = @"D:\" + Session["Upload_Project_Kind"].ToString() + @"\" + Session["ProjectName"].ToString() + @"\" + strNameNew;

                            clsTransaction.UpDatePath(strDestFilePath, strScrFilePath, strNameNew, Session["ID"].ToString());
                        }
                    }
                    Session["CaseName"] = strNameNew;
                    DataTable dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), Session["CaseName"].ToString());
                    this.gvwMain.DataSource = dt;
                    this.DataBind();
                }
            }
            
            clsMsg.AlertMessage("更新成功！", this.Page);
        }
        else
            clsMsg.AlertMessage("更新失敗！", this.Page);
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        //string strName = "";

        //HttpCookie cookie_CaseName = new HttpCookie("CaseName");
        //cookie_CaseName.Value = Server.UrlEncode(strName);
        ////cookie_CaseName.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(cookie_CaseName);
        //Session["CaseName"] = "";

        //string strID1 = "";
        //Server.Transfer("~/WebForm/TaskEdit.aspx?ID=" + strID + "&Kind=" + strName + "&Value=" + "");
        Server.Transfer("~/WebForm/TaskEdit.aspx?V=A");
    }

    protected void lbtnWifi_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_Wifi.aspx?ID=" + Session["ID"].ToString() + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1300,height=950');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }

    protected void lbtnBT_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_BT.aspx?ID=" + Session["ID"].ToString() + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }

    //protected void lbtnGCF_Click(object sender, EventArgs e)
    //{
    //    string win_str;

    //    win_str = @"window.open('../WebForm/Certification_GCF.aspx?ID=" + Session["ID"] + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    //}

    protected void lbtnGCF_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_GCF.aspx?ID=" + Session["ID"].ToString() + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);

    }
    protected void lbtnPTCRB_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_PTCRB.aspx?ID=" + Session["ID"].ToString() + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);

    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        string strFile;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        DataTable dt = clsData.UploadProjectCaseID(Session["ID"].ToString(), Session["CaseName"].ToString(), "1", "");

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


        if (clsTransaction.DelProjectCaseData(Session["ID"].ToString(), Session["CaseName"].ToString(), "0") == true)
        {
            Response.Redirect("~/WebForm/ProjectDetail.aspx?V=R");
            //Response.Redirect("~/WebForm/ProjectDetail.aspx?Value=1&ID=" + strID + "&Customer=" + clsParameter.strCustomer + "&Dep=" + clsParameter.strDepartment);
        }
        else
            clsMsg.AlertMessage("刪除失敗！", this.Page);
    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        DataTable dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), Session["CaseName"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();
        //GvQuery();
    }
    #endregion

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName1, strPath;

        strName1 = ((HyperLink)this.gvwMain.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        strPath = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblGVSeq")).Text;
        //string path = @"C:/test/" + strName;
        string path = strPath + "\\" + strName1;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

        if (clsTransaction.DelUploadFiles(strName1, Session["ID"].ToString(), Session["CaseName"].ToString()) == true)
        {
            System.IO.File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;

            DataTable dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), Session["CaseName"].ToString());
            this.gvwMain.DataSource = dt;
            this.DataBind();

            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗！", this.Page);
        }
    }
    #endregion

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/ProjectDetail.aspx?V=R");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strPath, strFile_Name;
        string strFile = "";
        int intFile;

        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
        //string strName = Server.UrlDecode(cookie_CaseName.Value);

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
                    clsTransaction.InsertUploadFile(Session["ID"].ToString(), strFile_Name, Session["CaseName"].ToString(), strPath);
                }
            }
            //MailData();
            clsMsg.AlertMessage("新增成功....", this.Page);
        }
        Session["FileN"] = "";
        DataTable dt = clsData.UploadProjectFileQuery(Session["ID"].ToString(), Session["CaseName"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region MailData
    private void MailData()
    {
        #region 宣告變數

        DateTime dt;
        string strMail;
        DataTable dt2;

        #endregion

        #region mail config

        //mail標題
        string MailSubject = "專案更新通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\project_mail_body.txt");
        string strMailBody = myMailBody.ReadToEnd();

        //預設標準時數
        //int defulDailyTime = Convert.ToInt16(WebConfigurationManager.AppSettings["checkDailyTime"]);

        #endregion

        #region 找資料塞到SendMail內
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        DataTable dt1 = clsData.UploadProjectQuery(Session["ID"].ToString(), "Project");
        string strRelated = dt1.Rows[0]["Related"].ToString();
        string strName = dt1.Rows[0]["Name"].ToString();
        string strBody = string.Format(strMailBody, strName, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");


        string[] sArray = strRelated.Split(',');
        foreach (string i in sArray)
        {
            if (i != "")
            {
                dt2 = clsData.getEmployees("1", i);

                strMail = dt2.Rows[0]["Email"].ToString();
                clsTransaction.SendMail(strMail, MailSubject, strBody);
            }

        }

        if (dt1.Rows[0]["Assign"].ToString() != "")
        {
            dt2 = clsData.getEmployees("1",  dt1.Rows[0]["Assign"].ToString());
            strMail = dt2.Rows[0]["Email"].ToString();
            clsTransaction.SendMail(strMail, MailSubject, strBody);
        }

        //=====sam測試
        dt1 = clsData.UploadLeader("1", strLocation_P, "");
        strMail = dt1.Rows[0]["Email"].ToString();
        clsTransaction.SendMail(strMail, MailSubject, strBody);
        //=====

        myMailBody.Close();
        myMailBody.Dispose();

        #endregion
    }
    #endregion
}
