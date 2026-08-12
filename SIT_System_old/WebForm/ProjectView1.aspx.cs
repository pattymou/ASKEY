using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class WebForm_ProjectView1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strSQL;
        DataTable dt1;

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            Session["FileN"] = "";
            Session["Upload_Kind"] = "ProjectInfo";

            Session["Fun"] = Request.QueryString["Fun"];
            dt1 = clsData.getFunction_Name(Session["Fun"].ToString());

            Session["Upload_Project_Kind"] = dt1.Rows[0]["Function_Name"].ToString();

            string strKind = Request.QueryString["Kind"];

            //if (strKind == "TP")
            //    strKind = "台北";
            //else
            //    strKind = "吳江";

            string strWrite;

            HttpCookie cookie_Write = Request.Cookies["Write"];
            strWrite = Server.UrlDecode(cookie_Write.Value);

            if (strWrite == "N")
            {
                lblAdd.Visible = false;
            }

            DataTable dt;

            //if (Session["Fun"].ToString() == "9")
            //{
            string strName;

            strName = Request.QueryString["ID"];
            Session["ProjectName"] = Request.QueryString["ID"];
            string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Open_" + Session["EmpNo"].ToString() + ".txt";
            dt = clsData.getProjectList_App("", "3", "Open", strKind, dt1.Rows[0]["Function_Name"].ToString(), strName);
            dataTableToText(dt, 11, strPath1);

            dt = clsData.getProjectList_App("", "3", "Close", strKind, dt1.Rows[0]["Function_Name"].ToString(), strName);
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Close_" + Session["EmpNo"].ToString() + ".txt";
            dataTableToText(dt, 11, strPath1);

            dt = clsData.getProjectList_App("", "3", "Hold", strKind, dt1.Rows[0]["Function_Name"].ToString(), strName);
            strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Hold_" + Session["EmpNo"].ToString() + ".txt";
            dataTableToText(dt, 11, strPath1);


            dt = clsData.UploadProjectFile(Session["ProjectName"].ToString());
            this.gvwMain.DataSource = dt;
            this.DataBind();
        }
        //}
        //else
        //{
        //    string strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Open.txt";
        //    dt = clsData.getProjectList("", "3", "Open", "", dt1.Rows[0]["Function_Name"].ToString());
        //    dataTableToText(dt, 10, strPath1);

        //    dt = clsData.getProjectList("", "3", "Close", "", dt1.Rows[0]["Function_Name"].ToString());
        //    strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Close.txt";
        //    dataTableToText(dt, 10, strPath1);

        //    dt = clsData.getProjectList("", "3", "Hold", "", dt1.Rows[0]["Function_Name"].ToString());
        //    strPath1 = Server.MapPath(@"../") + @"/ajax/data/arays_Hold.txt";
        //    dataTableToText(dt, 10, strPath1);
        //}
    }

    #region GvQuery
    private void GvQuery(Boolean IsPage)
    {
        //if (IsPage != true)
        //    this.gvwMain.PageIndex = 0;

        //DataTable dt = clsData.ViewFilesQuery(this.txtDateS.Text, this.txtDateE.Text, int.Parse(this.Page.Session["sess_emp_no"].ToString().Trim()), this.ddlDept.SelectedValue);
        //this.gvwMain.DataSource = dt;
        //this.DataBind();
    }
    #endregion

    #region dataTableToTxt
    public static void dataTableToText(DataTable dt, int columnCount, string DBPath)
    {
        int intRowcount = dt.Rows.Count;
        string strSQLFile = "{" + "\r\n";
        DateTime dTime;
        string strDate;

        strSQLFile += @"""data"":[" + "\r\n";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strSQLFile += "[\r\n";
            for (int j = 0; j < columnCount; j++)
            {
                if (j != columnCount - 1)
                {
                    if ((j == 14) || (j == 13))
                    {
                        dTime = Convert.ToDateTime(dt.Rows[i][j].ToString().Trim());
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
            strSQLFile = strSQLFile.Replace('\\','_');
            sw.Write(strSQLFile);
        }
    }
    #endregion

    public string strName1()
    {
        return Session["EmpNo"].ToString();
    }


    protected void lbtnAdd_Click(object sender, EventArgs e)
    {

        Server.Transfer("~/WebForm/ProjectEdit.aspx?A=1");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strPath, strFile_Name;
        string strFile = "";
        int intFile;

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
                    clsTransaction.InsertUploadFile_Project(Session["ProjectName"].ToString(), strFile_Name, strPath);
                }
            }
            MailData();
            clsMsg.AlertMessage("新增成功....", this.Page);
        }
        Session["FileN"] = "";
        DataTable dt = clsData.UploadProjectFile(Session["ProjectName"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        DataTable dt = clsData.UploadProjectFile(Session["ProjectName"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();

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

        if (clsTransaction.DelUploadProjectFiles(strName1, Session["ProjectName"].ToString(), strPath) == true)
        {
            System.IO.File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;

            DataTable dt = clsData.UploadProjectFile(Session["ProjectName"].ToString());
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
        string MailSubject = "Project Information檔案上傳通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\project_mail_body.txt");
        string strMailBody = myMailBody.ReadToEnd();

        //預設標準時數
        //int defulDailyTime = Convert.ToInt16(WebConfigurationManager.AppSettings["checkDailyTime"]);

        #endregion

        #region 找資料塞到SendMail內
        //HttpCookie cookie_Customer = Request.Cookies["Project"];
        //string strID = cookie_Customer.Values["ID"];

        DataTable dt1 = clsData.UploadLeader("0", "","");

        string strBody = string.Format(strMailBody, Session["ProjectName"].ToString(), "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");


        for (int intI = 0; intI < dt1.Rows.Count; intI++)
        {
            strMail = dt1.Rows[intI]["Email"].ToString();
            clsTransaction.SendMail(strMail, MailSubject, strBody);
        }

        //=====sam測試
        dt1 = clsData.UploadLeader("1", Session["Location"].ToString(), "");
        strMail = dt1.Rows[0]["Email"].ToString();
        clsTransaction.SendMail(strMail, MailSubject, strBody);
        //=====

        myMailBody.Close();
        myMailBody.Dispose();

        #endregion
    }
    #endregion
}
