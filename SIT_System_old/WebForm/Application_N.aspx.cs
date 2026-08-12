using System;
using System.Data;
using System.Text;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class WebForm_Application_N : System.Web.UI.Page
{
    //public static string strToday;
    public static string strRule;
    public static string strUFile;

    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager scriptManger = ScriptManager.GetCurrent(this.Page);
        //scriptManger.RegisterPostBackControl(this.btnBT);

        string strToday;
        //string strAKind;
        

        //if (Request.QueryString["Fun"] == "1")
        //    strAKind = "general";

        //else
        //    strAKind = "NPI";

        HttpCookie cookie_Rule_Kind = new HttpCookie("Rule_Kind");
        cookie_Rule_Kind.Value = Server.UrlEncode("Verification");
        //cookie_Rule.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookie_Rule_Kind);

        Session["EmpName"] = "";
        //lblName.Text = Session["AppNo"].ToString();
        if (Session["AppNo"] == null)
        {
            Response.Redirect("~/ApplicationDefault.aspx");


        }
        else
        {
            HttpCookie cookie_Rule = Request.Cookies["Rule_A"];
            if (cookie_Rule == null)
                Response.Redirect("~/WebForm/HomePage_A.aspx?Fun=" + Request.QueryString["Fun"]);

            strRule = Server.UrlDecode(cookie_Rule.Value);
            if (strRule == "")
                Response.Redirect("~/WebForm/HomePage_A.aspx?Fun=" + Request.QueryString["Fun"]);
        }

        if (!IsPostBack)
        {
            strUFile = "0";
            Session["FileN"] = "";
            //HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
            //cookie_Upload_Kind.Value = Server.UrlEncode("申請單");
            ////cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_Upload_Kind);
            Session["Upload_Kind"] = "申請單";

            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");
            //HttpCookie cookie_ApplicationID = new HttpCookie("ApplicationID");
            //cookie_ApplicationID.Value = Server.UrlEncode(strToday);
            ////cookie_ApplicationID.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_ApplicationID);
            Session["ApplicationID"] = strToday;
            //clsParameter.strApplicationID = strToday;
            loadCustomer(this.ddlCustomer1);
            loadNumber(this.ddlDQA, "Q600(品保總部)");
            loadNPI(this.ddlNPI);
            //loadCustomer(this.ddlCustomer);
            loadDepartment(this.ddlDepartment2);
            //loadDepartment(this.ddlDepartment);
            //DataTable dt = clsData.UploadEmployeesQuery(Session["sess_emp_no"].ToString());
            DataTable dt = clsData.UploadNumber(Session["AppNo"].ToString());
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["Name"].ToString().Trim();
                lblDepartment.Text = dt.Rows[0]["Department"].ToString().Trim();                
                lblEmail.Text = dt.Rows[0]["Mail"].ToString().Trim();
                lblExt.Text = dt.Rows[0]["Ext"].ToString().Trim();
            }
            ddlDepartment.Text = "DA40";
            //ddlCustomer.Visible = true;
            //lblCustomer1.Visible = true;
            //rdoAcceptT.Checked = true;
            //dt = clsData.UploadApplication_TestCase(ddlDepartment.Text);
            string strAKind;

            if (Request.QueryString["Fun"] == "1")
            {
                strAKind = "general";
            }
            else
            {
                strAKind = "NPI";
            }
            
            dt = clsData.UploadApplication_TestCaseN(ddlDepartment.Text, "",strAKind);
            this.gvwMain.DataSource = dt;
            this.DataBind();

            if (Request.QueryString["Fun"] == "1")
            {
                Label51.Visible = false;
                ddlDQA.Visible = false;
                Label54.Visible = false;
                ddlDQA.Text = "sam test";
            }
            else
            {
                Label51.Visible = true;
                ddlDQA.Visible = true;
                Label54.Visible = true;
            }


        }
        //lblName.Text = "呂昱蓁";
        //lblDepartment.Text = "DA40";
        //lblEmail.Text = "patty_lu@askey.com.tw";
        //lblExt.Text = "18186";

        //clsParameter.strUpload_Kind = "申請單";




    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    #region loadNumber
    protected void loadNumber(DropDownList DDL, string strDepartment)
    {
        clsDropDownList.ddlNumberD(DDL, strDepartment, "0");
    }
    #endregion

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DataTable dt = clsData.UploadApplication_TestCase(ddlDepartment.Text);
        //this.gvwMain.DataSource = dt;
        //this.DataBind();
        string strAKind;

        if (Request.QueryString["Fun"] == "1")
        {
            strAKind = "general";
        }
        else
        {
            strAKind = "NPI";
        }

        DataTable dt = clsData.UploadApplication_TestCaseN(ddlDepartment.Text, "", strAKind);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        //txtCalendar.Text = Calendar1.SelectedDate.ToString();
        //pl1.Visible = false;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //if (pl1.Visible == true)
        //    pl1.Visible = false;
        //else if (pl1.Visible == false)
        //    pl1.Visible = true; 



    }

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

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
        this.gvwMain.DataSource = dt1;
        this.DataBind();

        getTestCase();
    }

    private void getTestCase()
    {
        int intJ;
        string strCase;
        DataTable dt = clsData.UploadCustomerTestCase("");

        if (dt.Rows.Count > 0)
        {
            strCase = dt.Rows[0]["TestCase"].ToString();
            string[] sArray = strCase.Split(',');
            foreach (string i in sArray)
            {
                for (intJ = 0; intJ < this.gvwMain.Rows.Count; intJ++)
                {
                    string strFunction_No;

                    strFunction_No = ((Label)this.gvwMain.Rows[intJ].Cells[5].FindControl("lblGVSeq")).Text;
                    if (strFunction_No == i.ToString())
                    {
                        ((CheckBox)gvwMain.Rows[intJ].FindControl("CheckBox2")).Checked = true;
                    }

                }
            }
        }
    }

    private void setEmpty()
    {
        txtPM.Text = "";
        txtSW.Text = "";
        txtHW.Text = "";
        txtModelName.Text = "";
        txtFW.Text = "";
        txtWireless.Text = "";
        txtCustomer.Text = "";
        txtPCB.Text = "";
        txtBOM.Text = "";
        txtMAC.Text = "";
        txtUtility.Text = "";
        txtPart.Text = "";
        txtNote.Text = "";
        //txtCustomer1.Text = "";
        txtMechanical.Text = "";

        foreach (GridViewRow gridviewRow in gvwMain.Rows)
        {
            if (gridviewRow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkDelete = (CheckBox)gridviewRow.Cells[0].FindControl("CheckBox2");
                chkDelete.Checked = false;
            }
        }

    }

    bool checkWifi()
    {
        bool bPF = true;
        DataTable dt;

        //dt = clsData.UploadCertification_Wifi("1234");
        dt = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
        if ((dt.Rows[0]["ProductName"].ToString() == "") || (dt.Rows[0]["ModelNumber"].ToString() == "") || (dt.Rows[0]["WirelessChipset"].ToString() == "") || (dt.Rows[0]["ProductOperatingSystem"].ToString() == "") || (dt.Rows[0]["OSVersion"].ToString() == "") || (dt.Rows[0]["HardwareVersion_Product"].ToString() == "") || (dt.Rows[0]["FirmwareVersion_Product"].ToString() == "") || (dt.Rows[0]["HardwareVersion_WiFi"].ToString() == "") || (dt.Rows[0]["FirmwareVersion_WiFi"].ToString() == "") || (dt.Rows[0]["ProductNotes"].ToString() == "") || (dt.Rows[0]["Searchable"].ToString() == "") || (dt.Rows[0]["Publish"].ToString() == "") || (dt.Rows[0]["Publish_Date"].ToString() == "") || (dt.Rows[0]["DeviceType"].ToString() == "") || (dt.Rows[0]["ProductType"].ToString() == "") || (dt.Rows[0]["PrimaryProductCategory"].ToString() == "") || (dt.Rows[0]["SecondaryProductCategory"].ToString() == "") || (dt.Rows[0]["LeastOneBand"].ToString() == "") || (dt.Rows[0]["MandatoryProgram"].ToString() == "") || (dt.Rows[0]["OptionalProgram"].ToString() == "") || (dt.Rows[0]["SupportedSpatialStreams_Tx"].ToString() == "") || (dt.Rows[0]["SupportedSpatialStreams_Rx"].ToString() == "") || (dt.Rows[0]["AdditionalCapabilities"].ToString() == "") || (dt.Rows[0]["SecurityType"].ToString() == "") || (dt.Rows[0]["SpectrumAndRegulatoryFeatures"].ToString() == "") || (dt.Rows[0]["NOptionalFeature"].ToString() == "") || (dt.Rows[0]["ACOptionalFeature"].ToString() == ""))
            bPF = false;

        return bPF;
    }

    bool checkBT()
    {
        bool bPF = true;
        DataTable dt;

        //dt = clsData.UploadCertification_Wifi("1234");
        dt = clsData.UploadCertification_BT(Session["ApplicationID"].ToString());

        if ((dt.Rows[0]["BT_Version"].ToString() == "") || (dt.Rows[0]["Core_Mode"].ToString() == "") || (dt.Rows[0]["Briefly_Describe"].ToString() == "") || (dt.Rows[0]["Application_Profiles"].ToString() == "") || (dt.Rows[0]["Controller_Vendor"].ToString() == "") || (dt.Rows[0]["Controller_DID"].ToString() == "") || (dt.Rows[0]["Host_Vendor"].ToString() == "") || (dt.Rows[0]["Host_DID"].ToString() == "") || (dt.Rows[0]["Component_Vendor"].ToString() == "") || (dt.Rows[0]["Component_DID"].ToString() == "") || (dt.Rows[0]["End_Vendor"].ToString() == "") || (dt.Rows[0]["End_DID"].ToString() == ""))
            bPF = false;

        return bPF;
    }

    protected void butOK_Click(object sender, EventArgs e)
    {

        string strGName = txtModelName.Text;
        if ((strGName.IndexOf("ROHS") == -1) && (strGName.IndexOf("RoHs") == -1) && (strGName.IndexOf("-D") == -1) && (strGName.IndexOf("- D") == -1))
        {

            string strAdd;
            string strAccepted;
            string strFile = "";
            string strReady;
            string strStart = "";
            string strEnd = "";
            string strExpect;
            string strApplication;
            //string strFile = "";
            string strPath = "";
            string strFile_Name = "";
            string strAKind = "";
            int intFile;
            DateTime dt;

            //clsMsg.AlertMessage("*123", this.Page);
            int intX = 0;
            int intWifi = 0;
            int intBT = 0;
            int intLTE = 0;
            DataTable dtC;

            if (Request.QueryString["Fun"] == "1")
                strAKind = "一般驗証";
            else
                strAKind = "NPI驗証";

            for (int i = 0; i < this.gvwMain.Rows.Count; i++)
            {
                if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
                {
                    intX = 1;
                    if (gvwMain.Rows[i].Cells[1].Text == "LTE")
                    {
                        if (gvwMain.Rows[i].Cells[3].Text == "TRP/TIS")
                        {
                            if (intLTE == 0)
                            {
                                intLTE = 1;
                                dtC = clsData.UploadApplication_LTE(Session["ApplicationID"].ToString());
                                if (dtC.Rows.Count == 0)
                                {
                                    clsMsg.AlertMessage("請填寫LTE TRP/TIS Application Form！", this.Page);
                                    return;
                                }

                            }
                        }
                    }

                    if (gvwMain.Rows[i].Cells[1].Text == "Certification")
                    {
                        if (gvwMain.Rows[i].Cells[2].Text == "WiFi")
                        {
                            if (intWifi == 0)
                            {
                                intWifi = 1;
                                dtC = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
                                if (dtC.Rows.Count == 0)
                                {
                                    clsMsg.AlertMessage("請填寫WiFi認證申請單！", this.Page);
                                    return;
                                }
                                else
                                {
                                    if (checkWifi() == false)
                                    {
                                        clsMsg.AlertMessage("WiFi認證申請單填寫尚未完成！", this.Page);
                                        return;
                                    }
                                }
                            }
                        }
                        if (gvwMain.Rows[i].Cells[2].Text == "Bluetooth")
                        {
                            if (intBT == 0)
                            {
                                intBT = 1;
                                dtC = clsData.UploadCertification_BT(Session["ApplicationID"].ToString());
                                if (dtC.Rows.Count == 0)
                                {
                                    clsMsg.AlertMessage("請填寫BT認證申請單！", this.Page);
                                    return;
                                }
                                else
                                {
                                    if (checkBT() == false)
                                    {
                                        clsMsg.AlertMessage("Bluetooth認證申請單填寫尚未完成！", this.Page);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            if (intX == 0)
            {
                clsMsg.AlertMessage("請選擇測試項目！", this.Page);
                DataTable dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
                this.gvwMain.DataSource = dt1;
                this.DataBind();
            }
            else
            {
                strExpect = Request["date1"].ToString();
                if (Session["FileN"] != null)
                {
                    strFile = Session["FileN"].ToString();
                    //strFile = strFile.Replace("c:\\test\\\\", "");

                }
                if ((ddlCustomer1.Text == "") || (txtPM.Text.Trim() == "") || (txtSW.Text.Trim() == "") || (txtHW.Text.Trim() == "") || (txtMechanical.Text.Trim() == "") || (txtModelName.Text.Trim() == "") || (txtFW.Text.Trim() == "") || (txtCustomer.Text.Trim() == "") || (txtPCB.Text.Trim() == "") || (txtBOM.Text.Trim() == "") || (ddlNPI.Text.Trim() == "") || (strExpect == "") || (ddlDQA.Text == "") || (strFile == null) || (strFile == "") || (ddlDepartment2.Text == ""))
                {
                    if ((strFile == null) || (strFile == ""))
                    {
                        clsMsg.AlertMessage("*請上傳檔案....", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("*為必填欄位....", this.Page);
                    //Session["FileN"] = "";
                    //strFile = "";
                }
                else
                {
                    if (strUFile == "0")
                    {
                        string[] sArray = strFile.Split(',');
                        foreach (string i in sArray)
                        {
                            if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                            {
                                intFile = i.LastIndexOf('\\');
                                strPath = i.Substring(0, intFile);
                                strFile_Name = i.Substring(intFile + 1);
                                clsTransaction.InsertUploadFile(Session["ApplicationID"].ToString(), strFile_Name, "驗証申請", strPath);
                            }
                        }
                    }

                    //string[] sArray = strFile.Split(',');
                    //foreach (string i in sArray)
                    //{
                    //    if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                    //    {
                    //        intFile = i.LastIndexOf('\\');
                    //        strPath = i.Substring(0, intFile);
                    //        strFile_Name = i.Substring(intFile + 1);
                    //        clsTransaction.InsertUploadFile(Session["ApplicationID"].ToString(), strFile_Name, "驗証申請", strPath);
                    //    }
                    //}

                    Session["FileN"] = "";
                    strFile = "";
                    //if (rdoAcceptT.Checked == true)
                    //    strAccepted = "台北";
                    //else
                    strAccepted = "";


                    if (strExpect != "")
                    {
                        dt = Convert.ToDateTime(strExpect);
                        strExpect = dt.ToString("yyyyMMdd");
                    }
                    strReady = Request["date2"].ToString();
                    if (strReady != "")
                    {
                        dt = Convert.ToDateTime(strReady);
                        strReady = dt.ToString("yyyyMMdd");
                    }

                    DateTime myDate = DateTime.Now;
                    strApplication = myDate.ToString("yyyyMMdd");


                    //if ((strFile != null) && (strFile != ""))
                    //{
                    //    string[] sArray = strFile.Split(',');
                    //    foreach (string i in sArray)
                    //    {
                    //        if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                    //        {
                    //            intFile = i.LastIndexOf('\\');
                    //            strPath = i.Substring(0, intFile);
                    //            strFile_Name = i.Substring(intFile + 1);
                    //            clsTransaction.InsertUploadFile(strToday, strFile_Name, "驗証申請", strPath);
                    //        }
                    //    }
                    //}
                    string strID, strFunction, strItem, strKind;
                    strID = "";
                    strKind = "";
                    int intI = 0;
                    int intJ = 0;
                    string strLocal;
                    if (ddlDepartment.Text == "DA40")
                        strLocal = "台北";
                    else
                        strLocal = "吳江";

                    if (clsTransaction.InsertProject(Session["ApplicationID"].ToString(), txtModelName.Text.Trim(), strLocal, ddlCustomer1.Text, ddlNPI.Text, txtPM.Text.Trim(), txtHW.Text.Trim(), txtSW.Text.Trim(), txtMechanical.Text.Trim(), txtFW.Text.Trim(), txtWireless.Text.Trim(), txtPCB.Text.Trim(), txtBOM.Text.Trim(), txtMAC.Text.Trim(), txtUtility.Text.Trim(), txtPart.Text.Trim(), strReady, txtCustomer.Text.Trim(), strExpect, lblName.Text, lblDepartment.Text, lblExt.Text, lblEmail.Text, "", strStart, strEnd, strApplication, "", txtNote.Text, "驗証申請", "", "", "", "", "", "", "", ddlDQA.Text, ddlDepartment2.Text,strAKind) == true)
                    {
                        for (int i = 0; i < this.gvwMain.Rows.Count; i++)
                        {
                            if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
                            {
                                if (strID == "")
                                    strID = ((Label)this.gvwMain.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;
                                else
                                    strID = strID + "," + ((Label)this.gvwMain.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;

                                if (strKind != gvwMain.Rows[i].Cells[1].Text)
                                {
                                    intI = intI + 1;
                                    intJ = intI * 10;
                                }
                                else
                                    intJ = intJ + 1;

                                strKind = gvwMain.Rows[i].Cells[1].Text;
                                strFunction = gvwMain.Rows[i].Cells[2].Text;
                                strItem = gvwMain.Rows[i].Cells[3].Text;
                                clsTransaction.InsertProjectCase(intJ.ToString(), Session["ApplicationID"].ToString(), strKind + " " + strFunction, strItem, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            }
                        }


                        if (clsTransaction.InsertApplication_TestCase(Session["ApplicationID"].ToString(), strID, ddlDepartment.Text, "") == true)
                        {
                            clsMsg.AlertMessage("申請成功！申請單編號為" + Session["ApplicationID"].ToString(), this.Page);
                            MailData(strAccepted, lblName.Text, lblEmail.Text, lblExt.Text, Session["ApplicationID"].ToString());
                            setEmpty();
                            clsTransaction.DelApplication_Temporarily(Session["ApplicationID"].ToString());
                            clsTransaction.DelProjectCaseData_Temporarily(Session["ApplicationID"].ToString(), "", "1");
                            clsTransaction.DelApplicationTestCase_Temporarily(Session["ApplicationID"].ToString());
                        }
                        else
                        {
                            clsMsg.AlertMessage("申請失敗....", this.Page);
                        }
                        Session["FileN"] = "";
                    }
                }
                //DataTable dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
                //this.gvwMain.DataSource = dt1;
                //this.DataBind();
                Response.Redirect("~/WebForm/SearchApplication.aspx");
            }
        }
        else
            clsMsg.AlertMessage("機種名稱後面請勿加[客戶代碼]及[ROHS]！！", this.Page);

    }

    #region MailData
    private void MailData(string strLocation, string strName, string strMail, string strExt, string strNumber)
    {


        DateTime dt;

        //mail標題
        string MailSubject = "系統驗證申請單通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Application.txt");
        string strMailBody = myMailBody.ReadToEnd();


        #region 找資料塞到SendMail內


        DataTable dt1 = clsData.UploadLeader("1", "", "");
        string strMail1 = "";
        //dt1.Rows[0]["Email"].ToString();

        for (int intI = 0; intI < dt1.Rows.Count; intI++)
        {
            strMail1 = strMail1 + "," + dt1.Rows[intI]["Email"].ToString();
        }
        if (ddlDQA.Text != "")
        {
            dt1 = clsData.UploadDQA("Q600(品保總部)", ddlDQA.Text);
            strMail1 = strMail1 + "," + dt1.Rows[0]["Mail"].ToString();
        }
        string[] strEmail1 = strMail1.Split(',');
        foreach (string i in strEmail1)
        {
            if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
            {
                string strBody = string.Format(strMailBody, strNumber, strName, strMail, strExt, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");
                clsTransaction.SendMail(i, MailSubject, strBody);
            }
        }

        //clsTransaction.SendMail(strMail1, MailSubject, strBody);

        myMailBody.Close();
        myMailBody.Dispose();
        #endregion
    }
    #endregion

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        //string strID1 = "";
        Server.Transfer("~/WebForm/SearchApplication.aspx");
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HyperLink)e.Row.Cells[6].Controls[1]).Text != "")
                ((HyperLink)e.Row.Cells[6].Controls[1]).Text = "下載";

            if (((HyperLink)e.Row.Cells[5].Controls[1]).Text != "")
                ((HyperLink)e.Row.Cells[5].Controls[1]).Text = "開啟圖片";

            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("\n", "<br />");

        }
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;

        HyperLink Hyper = new HyperLink();
        Label lblText = new Label();

        for (int intI = 0; intI < e.Row.Cells.Count; intI++)
        {
            if (e.Row.Cells[1].Text == "LTE")
            {
                if (e.Row.Cells[3].Text == "TRP/TIS")
                {
                    Hyper.Text = "Application form for LTE TRP/TIS";
                    Hyper.NavigateUrl = "javascript:window.open('../WebForm/Application_LTE.aspx?ID=" + Session["ApplicationID"] + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1300,height=950');";
                    Hyper.ForeColor = System.Drawing.Color.Red;

                    lblText.Text = e.Row.Cells[4].Text + "  ";
                    e.Row.Cells[4].Controls.Add(lblText);
                    e.Row.Cells[4].Controls.Add(Hyper);
                }



            }

            //if (e.Row.Cells[3].Text == "IP Throughput")
            //{
            //    string str123;
            //    e.Row.Cells[4].Text.Replace("\n", "<br />");
            //    str123 = e.Row.Cells[4].Text;
            //}

            //e.Row.Cells[4].Text.Replace("/n","<br />");

        }


    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {


        for (int intI = 1; intI < 3; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        if (gvItem.Cells[intI-1].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI-1].Text.Trim())
                        {
                            gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                            gvItem.Cells[intI].Visible = false;
                            i = i + 1;
                        }
                        else
                        {
                            gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                            i = 1;
                        }
                    }
                    else
                    {
                        gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }

    }
    #endregion

    protected void butWifi_Click(object sender, EventArgs e)
    {
        string win_str;
        //win_str = "<script language='javascript'>window.open('../WebForm/Certification_BT.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        win_str = @"window.open('../WebForm/Certification_BT.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');";

        //Response.Write(win_str);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }
    protected void btnBT_Click(object sender, EventArgs e)
    {
        string win_str;
        //win_str = "<script language='javascript'>window.open('../WebForm/Certification_BT.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        win_str = @"window.open('../WebForm/Certification_BT.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');";

        //Response.Write(win_str);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    
    }
    protected void butTemporarily_Click(object sender, EventArgs e)
    {
        string strAdd;
        string strAccepted;
        string strFile = "";
        string strReady;
        string strStart = "";
        string strEnd = "";
        string strExpect;
        string strApplication;
        //string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        string strAKind = "";
        int intFile;
        DateTime dt;

        //clsMsg.AlertMessage("*123", this.Page);
        int intX = 0;
        int intWifi = 0;
        int intBT = 0;
        DataTable dtC;

        strExpect = Request["date1"].ToString();
        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();
            //strFile = strFile.Replace("c:\\test\\\\", "");

        }
        if (Request.QueryString["Fun"] == "1")
            strAKind = "一般驗証";
        else
            strAKind = "NPI驗証";
        //if ((ddlCustomer1.Text == "") || (txtPM.Text.Trim() == "") || (txtSW.Text.Trim() == "") || (txtHW.Text.Trim() == "") || (txtMechanical.Text.Trim() == "") || (txtModelName.Text.Trim() == "") || (txtFW.Text.Trim() == "") || (txtCustomer.Text.Trim() == "") || (txtPCB.Text.Trim() == "") || (txtBOM.Text.Trim() == "") || (ddlNPI.Text.Trim() == "") || (strExpect == "") || (ddlDQA.Text == "") || (strFile == null) || (strFile == "") || (ddlDepartment2.Text == ""))
        //{
        //    if ((strFile == null) || (strFile == ""))
        //    {
        //        clsMsg.AlertMessage("*請上傳檔案....", this.Page);
        //    }
        //    else
        //        clsMsg.AlertMessage("*為必填欄位....", this.Page);
        //    Session["FileN"] = "";
        //    strFile = "";
        //}
        //else
        //{
        if ((strFile == null) || (strFile == ""))
        {
            clsMsg.AlertMessage("*請上傳檔案....", this.Page);
        }
        else
        {
            string[] sArray = strFile.Split(',');
            foreach (string i in sArray)
            {
                if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                {
                    intFile = i.LastIndexOf('\\');
                    strPath = i.Substring(0, intFile);
                    strFile_Name = i.Substring(intFile + 1);
                    clsTransaction.InsertUploadFile(Session["ApplicationID"].ToString(), strFile_Name, "驗証申請", strPath);
                }
            }
            strUFile = "1";
            //Session["FileN"] = "";
            //strFile = "";
            //if (rdoAcceptT.Checked == true)
            //    strAccepted = "台北";
            //else
            strAccepted = "";


            if (strExpect != "")
            {
                dt = Convert.ToDateTime(strExpect);
                strExpect = dt.ToString("yyyyMMdd");
            }
            strReady = Request["date2"].ToString();
            if (strReady != "")
            {
                dt = Convert.ToDateTime(strReady);
                strReady = dt.ToString("yyyyMMdd");
            }

            DateTime myDate = DateTime.Now;
            strApplication = myDate.ToString("yyyyMMdd");


            //if ((strFile != null) && (strFile != ""))
            //{
            //    string[] sArray = strFile.Split(',');
            //    foreach (string i in sArray)
            //    {
            //        if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
            //        {
            //            intFile = i.LastIndexOf('\\');
            //            strPath = i.Substring(0, intFile);
            //            strFile_Name = i.Substring(intFile + 1);
            //            clsTransaction.InsertUploadFile(strToday, strFile_Name, "驗証申請", strPath);
            //        }
            //    }
            //}
            string strID, strFunction, strItem, strKind;
            strID = "";
            strKind = "";
            int intI = 0;
            int intJ = 0;
            string strLocal;
            if (ddlDepartment.Text == "DA40")
                strLocal = "台北";
            else
                strLocal = "吳江";

            //clsMsg.AlertMessage("2....", this.Page);
            //StringBuilder strSQL = new StringBuilder();

            //strSQL = clsTransaction.InsertProject_Temporarily1(Session["ApplicationID"].ToString(), txtModelName.Text.Trim(), strLocal, ddlCustomer1.Text, ddlNPI.Text, txtPM.Text.Trim(), txtHW.Text.Trim(), txtSW.Text.Trim(), txtMechanical.Text.Trim(), txtFW.Text.Trim(), txtWireless.Text.Trim(), txtPCB.Text.Trim(), txtBOM.Text.Trim(), txtMAC.Text.Trim(), txtUtility.Text.Trim(), txtPart.Text.Trim(), strReady, txtCustomer.Text.Trim(), strExpect, lblName.Text, lblDepartment.Text, lblExt.Text, lblEmail.Text, "", strStart, strEnd, strApplication, "", txtNote.Text, "驗証申請", "", "", "", "", "", "", "", ddlDQA.Text, ddlDepartment2.Text);

            //clsMsg.AlertMessage(strSQL.ToString(), this.Page);
            if (clsTransaction.InsertProject_Temporarily(Session["ApplicationID"].ToString(), txtModelName.Text.Trim(), strLocal, ddlCustomer1.Text, ddlNPI.Text, txtPM.Text.Trim(), txtHW.Text.Trim(), txtSW.Text.Trim(), txtMechanical.Text.Trim(), txtFW.Text.Trim(), txtWireless.Text.Trim(), txtPCB.Text.Trim(), txtBOM.Text.Trim(), txtMAC.Text.Trim(), txtUtility.Text.Trim(), txtPart.Text.Trim(), strReady, txtCustomer.Text.Trim(), strExpect, lblName.Text, lblDepartment.Text, lblExt.Text, lblEmail.Text, "", strStart, strEnd, strApplication, "", txtNote.Text, "驗証申請", "", "", "", "", "", "", "", ddlDQA.Text, ddlDepartment2.Text,strAKind) == true)
            {
                for (int i = 0; i < this.gvwMain.Rows.Count; i++)
                {
                    if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
                    {
                        if (strID == "")
                            strID = ((Label)this.gvwMain.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;
                        else
                            strID = strID + "," + ((Label)this.gvwMain.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;

                        if (strKind != gvwMain.Rows[i].Cells[1].Text)
                        {
                            intI = intI + 1;
                            intJ = intI * 10;
                        }
                        else
                            intJ = intJ + 1;

                        strKind = gvwMain.Rows[i].Cells[1].Text;
                        strFunction = gvwMain.Rows[i].Cells[2].Text;
                        strItem = gvwMain.Rows[i].Cells[3].Text;
                        clsTransaction.InsertProjectCase_Temporarily(intJ.ToString(), Session["ApplicationID"].ToString(), strKind + " " + strFunction, strItem, "", "", "", "", "", "", "", "", "", "", "");
                    }
                }

                //clsMsg.AlertMessage("1....", this.Page);
                if (clsTransaction.InsertApplication_TestCase_Temporarily(Session["ApplicationID"].ToString(), strID, ddlDepartment.Text, "") == true)
                {
                    clsMsg.AlertMessage("暫存成功！申請單編號為" + Session["ApplicationID"].ToString(), this.Page);
                    //MailData(strAccepted, lblName.Text, lblEmail.Text, lblExt.Text, Session["ApplicationID"].ToString());
                    //setEmpty();
                }
                else
                {
                    clsMsg.AlertMessage("暫存失敗....", this.Page);
                }
                //Session["FileN"] = "";
            }
        }
        //DataTable dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
        //string strAKind;

        if (Request.QueryString["Fun"] == "1")
            strAKind = "general";
        else
            strAKind = "NPI";

        DataTable dt1 = clsData.UploadApplication_TestCaseN(ddlDepartment.Text, "", strAKind);
        //DataTable dt1 = clsData.UploadApplication_TestCaseN(ddlDepartment.Text, "");
        this.gvwMain.DataSource = dt1;
        this.DataBind();
    }
}
