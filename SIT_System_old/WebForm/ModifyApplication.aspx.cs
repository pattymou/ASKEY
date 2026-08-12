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

public partial class WebForm_ModifyApplication : System.Web.UI.Page
{
    public static string strAKind;
    public static string strExpect;
    public static string strReady;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == "")
            Server.Transfer("~/WebForm/SearchApplication.aspx");
        if (!IsPostBack)
        {
            customer_t.Visible = false;
            Session["FileN"] = "";
            Session["Upload_Kind"] = "申請單";
            Session["ApplicationID"] = Request.QueryString["ID"];
            //strID = Request.QueryString["ID"];
            //strID = "20141226145022";
            loadNPI(this.ddlNPI);
            loadCustomer(this.ddlCustomer);
            //loadDepartment(this.ddlDepartment);
            loadDepartment(this.ddlDepartment2);
            loadNumber(this.ddlDQA, "Q600(品保總部)");

            //DataTable dt = clsData.UploadApplication_TestCase(ddlDepartment.Text);
            //this.gvwList.DataSource = dt;
            //this.DataBind();

            getApplication();



        }

    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, "0");
    }
    #endregion

    #region loadNumber
    protected void loadNumber(DropDownList DDL, string strDepartment)
    {
        clsDropDownList.ddlNumberD(DDL, strDepartment, "0");
    }
    #endregion

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
        this.gvwList.DataSource = dt1;
        this.DataBind();

        getTestCase1();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.Text != "DA40")
        {
            ddlCustomer.Visible = false;
            lblCustomer1.Visible = false;
        }
        else
        {
            ddlCustomer.Visible = true;
            lblCustomer1.Visible = true;
        }
        DataTable dt = clsData.UploadApplication_TestCase(ddlDepartment.Text);
        this.gvwList.DataSource = dt;
        this.DataBind();
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        getApplication();
        if (ddlDepartment.Text != "DA40")
        {
            ddlCustomer.Visible = false;
            lblCustomer1.Visible = false;
        }
        else
        {
            ddlCustomer.Visible = true;
            lblCustomer1.Visible = true;
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
        bool bTF = false;
        //string strName = ((Label)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("lblName")).Text;
        //if (clsTransaction.DelApplication(Request.QueryString["ID"]) == true)
        //{
        //    if (clsTransaction.DelProjectCaseData(Request.QueryString["ID"], "", "1") == true)
        //    {
        //        if (clsTransaction.DelApplicationTestCase(Request.QueryString["ID"]) == true)
        //        {
        //            bTF = true;
        //        }
        //    }

        //}
        int intX = 0;
        int intWifi = 0;
        int intBT = 0;
        DataTable dtC;

        for (int i = 0; i < this.gvwList.Rows.Count; i++)
        {
            if (((CheckBox)gvwList.Rows[i].FindControl("CheckBox2")).Checked)
            {
                intX = 1;
                if (gvwList.Rows[i].Cells[1].Text == "Certification")
                {
                    if (gvwList.Rows[i].Cells[2].Text == "WiFi")
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
                    if (gvwList.Rows[i].Cells[2].Text == "Bluetooth")
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
        if (clsTransaction.DelApplication_Temporarily(Request.QueryString["ID"]) == true)
        {
            if (clsTransaction.DelProjectCaseData_Temporarily(Request.QueryString["ID"], "", "1") == true)
            {
                if (clsTransaction.DelApplicationTestCase_Temporarily(Request.QueryString["ID"]) == true)
                {
                    bTF = true;
                }
            }

        }

        if (bTF == false)
            clsMsg.AlertMessage("修改失敗，請洽IT人員！", this.Page);
        else
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
            int intFile;
            DateTime dt;


            strExpect = Request["date1"].ToString();
            if (Session["FileN"] != null)
            {
                strFile = Session["FileN"].ToString();
                //strFile = strFile.Replace("c:\\test\\\\", "");

            }
            if ((txtName.Text.Trim() == "") || (txtDepartment.Text.Trim() == "") || (txtExt.Text.Trim() == "") || (txtEmail.Text.Trim() == "") || (txtCustomer1.Text.Trim() == "") || (txtPM.Text.Trim() == "") || (txtSW.Text.Trim() == "") || (txtHW.Text.Trim() == "") || (txtMechanical.Text.Trim() == "") || (txtModelName.Text.Trim() == "") || (txtFW.Text.Trim() == "") || (txtCustomer.Text.Trim() == "") || (txtPCB.Text.Trim() == "") || (txtBOM.Text.Trim() == "") || (ddlNPI.Text.Trim() == "") || (strExpect == "") || (ddlDQA.Text == "") || (ddlDepartment2.Text == ""))
            {
                clsMsg.AlertMessage("*為必填欄位....", this.Page);
            }
            else
            {
                if ((strFile != null) && (strFile != ""))
                {
                    string[] sArray = strFile.Split(',');
                    foreach (string i in sArray)
                    {
                        if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                        {
                            intFile = i.LastIndexOf('\\');
                            strPath = i.Substring(0, intFile);
                            strFile_Name = i.Substring(intFile + 1);
                            clsTransaction.InsertUploadFile(Request.QueryString["ID"], strFile_Name, "驗証申請", strPath);
                        }
                    }
                }
                Session["FileN"] = "";
                strFile = "";
                //if (rdoAcceptT.Checked == true)
                //    strAccepted = "台北";
                //else
                //    strAccepted = "吳江";
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
                //            clsTransaction.InsertUploadFile(Request.QueryString["ID"], strFile_Name, "驗証申請", strPath);
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

                if (clsTransaction.InsertProject(Request.QueryString["ID"], txtModelName.Text.Trim(), strLocal, txtCustomer1.Text, ddlNPI.Text, txtPM.Text.Trim(), txtHW.Text.Trim(), txtSW.Text.Trim(), txtMechanical.Text.Trim(), txtFW.Text.Trim(), txtWireless.Text.Trim(), txtPCB.Text.Trim(), txtBOM.Text.Trim(), txtMAC.Text.Trim(), txtUtility.Text.Trim(), txtPart.Text.Trim(), strReady, txtCustomer.Text.Trim(), strExpect, txtName.Text.Trim(), txtDepartment.Text.Trim(), txtExt.Text.Trim(), txtEmail.Text.Trim(), "", strStart, strEnd, strApplication, "", txtNote.Text, lblKind1.Text, "", "", "", "", "", "", "", ddlDQA.Text, ddlDepartment2.Text, strAKind) == true)
                {
                    for (int i = 0; i < this.gvwList.Rows.Count; i++)
                    {
                        if (((CheckBox)gvwList.Rows[i].FindControl("CheckBox2")).Checked)
                        {
                            if (strID == "")
                                strID = ((Label)this.gvwList.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;
                            else
                                strID = strID + "," + ((Label)this.gvwList.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;

                            if (strKind != gvwList.Rows[i].Cells[1].Text)
                            {
                                intI = intI + 1;
                                intJ = intI * 10;
                            }
                            else
                                intJ = intJ + 1;

                            strKind = gvwList.Rows[i].Cells[1].Text;
                            strFunction = gvwList.Rows[i].Cells[2].Text;
                            strItem = gvwList.Rows[i].Cells[3].Text;
                            clsTransaction.InsertProjectCase(intJ.ToString(), Request.QueryString["ID"], strKind + " " + strFunction, strItem, "", "", "", "", "", "", "", "", "", "", "","","","");

                        }
                    }
                    if (clsTransaction.InsertApplication_TestCase(Request.QueryString["ID"], strID, ddlDepartment.Text, ddlCustomer.Text) == true)
                    {
                        clsMsg.AlertMessage("申請成功....", this.Page);

                    }
                    else
                    {
                        clsMsg.AlertMessage("申請失敗....", this.Page);
                    }
                    //if (addDSL(Request.QueryString["ID"]) == true)
                    //{
                    //    if (addWireless(Request.QueryString["ID"]) == true)
                    //    {
                    //        if (addLTE(Request.QueryString["ID"]) == true)
                    //        {
                    //            if (addWifi(Request.QueryString["ID"]) == true)
                    //            {
                    //                if (addUSB(Request.QueryString["ID"]) == true)
                    //                {
                    //                    if (addBluetooth(Request.QueryString["ID"]) == true)
                    //                    {
                    //                        clsMsg.AlertMessage("修改成功....", this.Page);
                    //                        //setEmpty();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                }
                else
                {
                    clsMsg.AlertMessage("申請失敗....", this.Page);
                }

                Session["FileN"] = "";

                Response.Redirect("~/WebForm/SearchApplication.aspx");

                //getApplication();
                //DataTable dt1 = clsData.UploadProjectFileQuery(Request.QueryString["ID"], "驗証申請");
                //this.gvwMain.DataSource = dt1;
                //this.DataBind();

                //dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
                //this.gvwList.DataSource = dt1;
                //this.DataBind();

                //getTestCase("0");
            }
        }

    }

    #region loadNPI
    protected void loadNPI(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 2, "0");
    }
    #endregion

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName, strPath;

        strName = ((HyperLink)this.gvwMain.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        strPath = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblGVSeq")).Text;
        //string path = @"C:/test/" + strName;
        string path = strPath + "\\" + strName;
        if (clsTransaction.DelUploadFiles(strName, Request.QueryString["ID"], "驗証申請") == true)
        {
            File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            DataTable dt1 = clsData.UploadProjectFileQuery(Request.QueryString["ID"], "驗証申請");
            this.gvwMain.DataSource = dt1;
            this.DataBind();
            //GvQuery(false);
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
        DataTable dt = clsData.UploadProjectFileQuery(Request.QueryString["ID"], "驗証申請");
        this.gvwMain.DataSource = dt;
        this.DataBind();
        //GvQuery(true);
    }
    #endregion

    private void getApplication()
    {
        string strStart1, strEnd1;
        ////strID = Request.QueryString["ID"];
        //strID = "20141107164955";
        DataTable dt = clsData.UploadProjectTemporarilyQuery(Request.QueryString["ID"], "Project_Temporarily");
        DateTime dt1;

        //if (dt.Rows[0]["Accepted_Team"].ToString() == "吳江")
        //    rdoAcceptW.Checked = true;
        //else
        //    rdoAcceptT.Checked = true;
        strAKind = dt.Rows[0]["Project_Kind"].ToString();
        txtName.Text = dt.Rows[0]["A_Name"].ToString();
        txtDepartment.Text = dt.Rows[0]["A_Department"].ToString();
        txtExt.Text = dt.Rows[0]["A_Ext"].ToString();
        txtEmail.Text = dt.Rows[0]["A_mail"].ToString();
        txtCustomer1.Text = dt.Rows[0]["Customer"].ToString();
        txtPM.Text = dt.Rows[0]["PM"].ToString();
        txtSW.Text = dt.Rows[0]["SW_Engineer"].ToString();
        txtHW.Text = dt.Rows[0]["HW_Engineer"].ToString();
        txtMechanical.Text = dt.Rows[0]["Mechanical_Engineer"].ToString();
        txtModelName.Text = dt.Rows[0]["Name"].ToString();
        txtFW.Text = dt.Rows[0]["FW_Version"].ToString();
        txtWireless.Text = dt.Rows[0]["WirelessDrive"].ToString();
        txtCustomer.Text = dt.Rows[0]["Customer_Product_Name"].ToString();
        ddlNPI.Text = dt.Rows[0]["NPI"].ToString();
        txtPCB.Text = dt.Rows[0]["PCB_Version"].ToString();
        txtBOM.Text = dt.Rows[0]["Chipset"].ToString();
        txtMAC.Text = dt.Rows[0]["Sample_Mac_address"].ToString();
        txtUtility.Text = dt.Rows[0]["Utility_Version"].ToString();
        txtPart.Text = dt.Rows[0]["DSP_Model"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        ddlDQA.Text = dt.Rows[0]["DQA"].ToString();
        ddlDepartment2.Text = dt.Rows[0]["A_Department2"].ToString();
        //ddlNPI.Text = dt.Rows[0]["NPI"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["Expect_Date"].ToString());
        strStart1 = dt1.ToString("yyyy/MM/dd");
        if (strStart1 == "1900/01/01")
            strExpect = "";
        else
            strExpect = strStart1;

        dt1 = Convert.ToDateTime(dt.Rows[0]["Sample_Ready_Date"].ToString());
        strStart1 = dt1.ToString("yyyy/MM/dd");
        if (strStart1 == "1900/01/01")
            strReady = "";
        else
            strReady = strStart1;

        string strKind1;
        if (dt.Rows[0]["Kind"].ToString() == "認証申請")
        {
            strKind1 = "Certification";
            Certification1.Visible = true;
            Certification2.Visible = true;
            Certification3.Visible = true;

        }
        else
        {
            strKind1 = "";
            Certification1.Visible = false;
            Certification2.Visible = false;
            Certification3.Visible = false;
        }
        lblKind1.Text = dt.Rows[0]["Kind"].ToString();
        dt = clsData.UploadProjectFileQuery(Request.QueryString["ID"], dt.Rows[0]["Kind"].ToString());
        this.gvwMain.DataSource = dt;
        this.DataBind();

        dt = clsData.UploadApplicationTestCase_Temp(Request.QueryString["ID"]);
        if (dt.Rows.Count >0)
            ddlDepartment.Text = dt.Rows[0]["TestCase_Department"].ToString();

        string strTest;
        strTest = dt.Rows[0]["TestCase"].ToString();
        string[] sArray = strTest.Split('-');
        strTest = sArray[0].ToString();
        dt = clsData.UploadTestCaseKind(strTest);
        if (dt.Rows.Count > 0)
            dt = clsData.UploadApplication_TestCaseN(ddlDepartment.Text, strKind1, dt.Rows[0]["Application_Kind"].ToString());
        else
            dt = clsData.UploadApplication_TestCaseN(ddlDepartment.Text, strKind1, "NPI");
        this.gvwList.DataSource = dt;
        this.DataBind();
        

        getTestCase("0");

    }

    private void getTestCase(string strKind)
    {
        int intJ;
        string strCase;
        DataTable dt;

        //if ((strKind == "0") || (ddlCustomer.Text == ""))
        //{
        //    dt = clsData.UploadApplicationTestCase_Temp(Request.QueryString["ID"]);
        //    ddlDepartment.Text = dt.Rows[0]["TestCase_Department"].ToString();
        //    if (dt.Rows[0]["TestCase_Department"].ToString() != "")
        //        ddlCustomer.Text = dt.Rows[0]["TestCase_Customer"].ToString();
        //    else
        //        ddlCustomer.Text = "";
        //    dt = clsData.UploadApplicationTestCase_Temp(ddlDepartment.Text);
        //    this.gvwList.DataSource = dt;
        //    this.DataBind();
        dt = clsData.UploadApplicationTestCase_Temp(Request.QueryString["ID"]);


        //}
        //else
        //{

        //    dt = clsData.UploadCustomerTestCase(ddlCustomer.Text);
        //    this.gvwList.DataSource = dt;
        //    this.DataBind();
        //}


        //dt = clsData.UploadApplicationTestCase_Temp(Request.QueryString["ID"]);
        //ddlDepartment.Text = dt.Rows[0]["TestCase_Department"].ToString();



        if (dt.Rows.Count > 0)
        {
            strCase = dt.Rows[0]["TestCase"].ToString();
            string[] sArray = strCase.Split(',');
            foreach (string i in sArray)
            {
                for (intJ = 0; intJ < this.gvwList.Rows.Count; intJ++)
                {
                    string strFunction_No;

                    strFunction_No = ((Label)this.gvwList.Rows[intJ].Cells[5].FindControl("lblGVSeq")).Text;
                    if (strFunction_No == i.ToString())
                    {
                        ((CheckBox)gvwList.Rows[intJ].FindControl("CheckBox2")).Checked = true;
                    }

                }
            }
        }
    }

    #region gvwList_PageIndexChanging
    protected void gvwList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

    }
    #endregion

    #region gvwList_PreRender
    protected void gvwList_PreRender(object sender, EventArgs e)
    {


        for (int intI = 1; intI < 3; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwList.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwList.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwList.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwList.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }

    }
    #endregion

    protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((HyperLink)e.Row.Cells[6].Controls[1]).Text != "")
                ((HyperLink)e.Row.Cells[6].Controls[1]).Text = "下載";

            if (((HyperLink)e.Row.Cells[5].Controls[1]).Text != "")
                ((HyperLink)e.Row.Cells[5].Controls[1]).Text = "開啟圖片";

        }
        //e.Row.Cells[5].Visible = false;
        //e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
        //e.Row.Cells[9].Visible = false;
    }


    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        //string strID1 = "";
        Server.Transfer("~/WebForm/Application.aspx");
    }

    protected void lbtnModify_Click(object sender, EventArgs e)
    {
        //string strID1 = "";
        Server.Transfer("~/WebForm/SearchApplication.aspx");
    }

    private void getTestCase1()
    {
        int intJ;
        string strCase;
        DataTable dt = clsData.UploadCustomerTestCase(ddlCustomer.Text);

        if (dt.Rows.Count > 0)
        {
            strCase = dt.Rows[0]["TestCase"].ToString();
            string[] sArray = strCase.Split(',');
            foreach (string i in sArray)
            {
                for (intJ = 0; intJ < this.gvwList.Rows.Count; intJ++)
                {
                    string strFunction_No;

                    strFunction_No = ((Label)this.gvwList.Rows[intJ].Cells[5].FindControl("lblGVSeq")).Text;
                    if (strFunction_No == i.ToString())
                    {
                        ((CheckBox)gvwList.Rows[intJ].FindControl("CheckBox2")).Checked = true;
                    }

                }
            }
        }
    }
    protected void butWifi_Click(object sender, EventArgs e)
    {
        string win_str;
        //win_str = "<script language='javascript'>window.open('../WebForm/Certification_BT.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        win_str = @"window.open('../WebForm/Certification_Wifi.aspx?ID=" + Session["ApplicationID"] + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1300,height=950');";

        //Response.Write(win_str);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }
    protected void btnBT_Click(object sender, EventArgs e)
    {
        string win_str;
        //win_str = "<script language='javascript'>window.open('../WebForm/Certification_BT.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        win_str = @"window.open('../WebForm/Certification_BT.aspx?ID=" + Session["ApplicationID"] + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');";

        //Response.Write(win_str);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }
    protected void butTemporarily_Click(object sender, EventArgs e)
    {
        bool bTF = false;
        //string strName = ((Label)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("lblName")).Text;
        if (clsTransaction.DelApplication_Temporarily(Request.QueryString["ID"]) == true)
        {
            if (clsTransaction.DelProjectCaseData_Temporarily(Request.QueryString["ID"], "", "1") == true)
            {
                if (clsTransaction.DelApplicationTestCase_Temporarily(Request.QueryString["ID"]) == true)
                {
                    bTF = true;
                }
            }

        }

        if (bTF == false)
            clsMsg.AlertMessage("暫存失敗，請洽IT人員！", this.Page);
        else
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
            int intFile;
            DateTime dt;


            strExpect = Request["date1"].ToString();
            if (Session["FileN"] != null)
            {
                strFile = Session["FileN"].ToString();
                //strFile = strFile.Replace("c:\\test\\\\", "");

            }
            //if ((txtName.Text.Trim() == "") || (txtDepartment.Text.Trim() == "") || (txtExt.Text.Trim() == "") || (txtEmail.Text.Trim() == "") || (txtCustomer1.Text.Trim() == "") || (txtPM.Text.Trim() == "") || (txtSW.Text.Trim() == "") || (txtHW.Text.Trim() == "") || (txtMechanical.Text.Trim() == "") || (txtModelName.Text.Trim() == "") || (txtFW.Text.Trim() == "") || (txtCustomer.Text.Trim() == "") || (txtPCB.Text.Trim() == "") || (txtBOM.Text.Trim() == "") || (ddlNPI.Text.Trim() == "") || (strExpect == "") || (ddlDQA.Text == "") || (ddlDepartment2.Text == ""))
            //{
            //    clsMsg.AlertMessage("*為必填欄位....", this.Page);
            //}
            //else
            //{
            if ((strFile != null) && (strFile != ""))
            {
                string[] sArray = strFile.Split(',');
                foreach (string i in sArray)
                {
                    if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                    {
                        intFile = i.LastIndexOf('\\');
                        strPath = i.Substring(0, intFile);
                        strFile_Name = i.Substring(intFile + 1);
                        clsTransaction.InsertUploadFile(Request.QueryString["ID"], strFile_Name, "驗証申請", strPath);
                    }
                }
            }
            Session["FileN"] = "";
            strFile = "";
            //if (rdoAcceptT.Checked == true)
            //    strAccepted = "台北";
            //else
            //    strAccepted = "吳江";
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
            //            clsTransaction.InsertUploadFile(Request.QueryString["ID"], strFile_Name, "驗証申請", strPath);
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

            if (clsTransaction.InsertProject_Temporarily(Request.QueryString["ID"], txtModelName.Text.Trim(), strLocal, txtCustomer1.Text, ddlNPI.Text, txtPM.Text.Trim(), txtHW.Text.Trim(), txtSW.Text.Trim(), txtMechanical.Text.Trim(), txtFW.Text.Trim(), txtWireless.Text.Trim(), txtPCB.Text.Trim(), txtBOM.Text.Trim(), txtMAC.Text.Trim(), txtUtility.Text.Trim(), txtPart.Text.Trim(), strReady, txtCustomer.Text.Trim(), strExpect, txtName.Text.Trim(), txtDepartment.Text.Trim(), txtExt.Text.Trim(), txtEmail.Text.Trim(), "", strStart, strEnd, strApplication, "", txtNote.Text, lblKind1.Text, "", "", "", "", "", "", "", ddlDQA.Text, ddlDepartment2.Text, strAKind) == true)
            {
                for (int i = 0; i < this.gvwList.Rows.Count; i++)
                {
                    if (((CheckBox)gvwList.Rows[i].FindControl("CheckBox2")).Checked)
                    {
                        if (strID == "")
                            strID = ((Label)this.gvwList.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;
                        else
                            strID = strID + "," + ((Label)this.gvwList.Rows[i].Cells[4].FindControl("lblGVSeq")).Text;

                        if (strKind != gvwList.Rows[i].Cells[1].Text)
                        {
                            intI = intI + 1;
                            intJ = intI * 10;
                        }
                        else
                            intJ = intJ + 1;

                        strKind = gvwList.Rows[i].Cells[1].Text;
                        strFunction = gvwList.Rows[i].Cells[2].Text;
                        strItem = gvwList.Rows[i].Cells[3].Text;
                        clsTransaction.InsertProjectCase_Temporarily(intJ.ToString(), Request.QueryString["ID"], strKind + " " + strFunction, strItem, "", "", "", "", "", "", "", "", "", "", "");

                    }
                }
                if (clsTransaction.InsertApplication_TestCase_Temporarily(Request.QueryString["ID"], strID, ddlDepartment.Text, ddlCustomer.Text) == true)
                {
                    clsMsg.AlertMessage("暫存成功....", this.Page);

                }
                else
                {
                    clsMsg.AlertMessage("暫存失敗....", this.Page);
                }
                //if (addDSL(Request.QueryString["ID"]) == true)
                //{
                //    if (addWireless(Request.QueryString["ID"]) == true)
                //    {
                //        if (addLTE(Request.QueryString["ID"]) == true)
                //        {
                //            if (addWifi(Request.QueryString["ID"]) == true)
                //            {
                //                if (addUSB(Request.QueryString["ID"]) == true)
                //                {
                //                    if (addBluetooth(Request.QueryString["ID"]) == true)
                //                    {
                //                        clsMsg.AlertMessage("修改成功....", this.Page);
                //                        //setEmpty();
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

            }
            else
            {
                clsMsg.AlertMessage("暫存失敗....", this.Page);
            }

            Session["FileN"] = "";

            //Response.Redirect("~/WebForm/SearchApplication.aspx");
            getApplication();


            //DataTable dt1 = clsData.UploadProjectFileQuery(Request.QueryString["ID"], "驗証申請");
            //this.gvwMain.DataSource = dt1;
            //this.DataBind();

            //dt1 = clsData.UploadApplication_TestCase(ddlDepartment.Text);
            //this.gvwList.DataSource = dt1;
            //this.DataBind();

            getTestCase("0");
            //}
        }
    }
}
