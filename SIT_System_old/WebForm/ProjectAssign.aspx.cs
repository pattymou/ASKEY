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
using System.Diagnostics;

public partial class WebForm_ProjectAssign : System.Web.UI.Page
{
    //public static string strID;
    //public static string strEmail;
    //public static string strApplicationID;
    //public static string strLocation_P;
    //public static string strLocation;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //HttpCookie cookie_Location_P = Request.Cookies["Location"];
            //strLocation_P = Server.UrlDecode(cookie_Location_P.Value);

            Session["ApplicationID"] = Request.QueryString["ID"];
            //strID = "20141217151651";
            rdoAccpt.Checked = true;
            //loadCustomer(this.ddlCustomer);
            //loadDepartment(this.ddlDepartment);
            getProject();
        }
    }

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1,"0");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3,"0");
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        //string strEnd;
        DateTime dt;
        string sourceFile = @"D:\Application\" + Session["ApplicationID"].ToString();
        string destinationFile = @"D:\Test Report\" + lblDepartment.Text + @"\" + lblID.Text + @"\" + lblCustomer.Text.Trim() + @"\" + lblNPI.Text;

        string strFileName, strPath, strPath1;


        if (lblCustomer.Text.Trim() == "")
            clsMsg.AlertMessage("請選擇客戶別！", this.Page);
        else
        {
            if (rdoAccpt.Checked == true)
            {

                clsTransaction.UpDateApplicationForm("Open", Session["ApplicationID"].ToString(), "");


                DirectoryInfo dir = new DirectoryInfo(sourceFile);

                FileInfo[] fileList = dir.GetFiles();

                foreach (FileInfo file in fileList)
                {
                    strFileName = file.Name.ToString();
                    strPath = sourceFile + @"\" + strFileName;
                    strPath1 = destinationFile + @"\" + strFileName;
                    if (!Directory.Exists(destinationFile))  // 若目錄不存在則建立之
                    {
                        Directory.CreateDirectory(destinationFile);
                    }
                    File.Move(strPath, strPath1);
                    clsTransaction.UpDateApplicationFile(destinationFile, Session["ApplicationID"].ToString(), strFileName);
                }
                Directory.Delete(sourceFile, true);
                MailData("Open");
                Response.Redirect("~/WebForm/ProjectApplication.aspx");
                //}
            }
            else
            {
                if (!Directory.Exists(sourceFile))  // 若目錄不存在
                {
                }
                else
                    Directory.Delete(sourceFile, true);
                clsTransaction.UpDateApplicationForm("Reject", Session["ApplicationID"].ToString(), "");
                MailData("Reject");
                Response.Redirect("~/WebForm/ProjectApplication.aspx");
            }
        }
    }

    #region MailData
    private void MailData(string strKind)
    {

        DateTime dt;
        StreamReader myMailBody=null;

        //mail標題
        string MailSubject = "系統驗證申請單通知";

        if (strKind == "Open")
        {
            for (int a = 0; a < 2; a++)
            {
                if (a == 0)
                    myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Application1.txt");
                else
                    myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Application2.txt");
                string strMailBody = myMailBody.ReadToEnd();

                if (a == 0)
                {
                    string strEmail2 = Session["Email"].ToString();
                    string[] strEmail1 = strEmail2.Split(',');
                    foreach (string i in strEmail1)
                    {
                        if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                        {
                            string strBody = string.Format(strMailBody, Session["ApplicationID"].ToString(), lblName.Text, lblMail.Text, lblExt.Text, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>",lblID.Text);
                            clsTransaction.SendMail(i, MailSubject, strBody);
                        }
                    }

                    
                    //DataTable dt1 = clsData.UploadLeader("1", "", "");
                    //string strMail1 = dt1.Rows[0]["Email"].ToString();
                    //string strBody1 = string.Format(strMailBody, Session["ApplicationID"].ToString(), lblName.Text, lblMail.Text, lblExt.Text, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", lblID.Text);
                    //clsTransaction.SendMail(strMail1, MailSubject, strBody1);
                    DataTable dt1 = clsData.UploadLeader("1", "", "");
                    string strMail1 = "";
                    //dt1.Rows[0]["Email"].ToString();

                    for (int intI = 0; intI < dt1.Rows.Count; intI++)
                    {
                        strMail1 = strMail1 + "," + dt1.Rows[intI]["Email"].ToString();
                    }
                    string[] strEmail3 = strMail1.Split(',');
                    foreach (string i in strEmail3)
                    {
                        if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                        {
                            string strBody1 = string.Format(strMailBody, Session["ApplicationID"].ToString(), lblName.Text, lblMail.Text, lblExt.Text, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>", lblID.Text);
                            clsTransaction.SendMail(i, MailSubject, strBody1);
                        }
                    }
                }
                else
                {
                    string strBody = string.Format(strMailBody, "成功受理", "", "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");
                    clsTransaction.SendMail(lblMail.Text.Trim(), MailSubject, strBody);
                }

            }

        }
        else
        {
            myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Application2.txt");
            string strMailBody = myMailBody.ReadToEnd();
            string strBody = string.Format(strMailBody, "拒絕", "請洽驗證單位", "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");
            clsTransaction.SendMail(lblMail.Text.Trim(), MailSubject, strBody);


        }
        myMailBody.Close();
        myMailBody.Dispose();

    }
    #endregion

    private void getProject()
    {
        //string strID;
        string strTestCase;
        string strItem = "";
        string strDate;
        DateTime dTime;
        string strLocation;
        string strEmail;
        //string[] strArray;
        //string strArray1;

        //strID = Request.QueryString["ID"];
        //strID = "20141201115442";
        DataTable dt = clsData.UploadProjectQuery(Session["ApplicationID"].ToString(), "Project");

        //DataRow dr = dt.Rows;
        lblID.Text = dt.Rows[0]["Accepted_Team"].ToString() + "--" + dt.Rows[0]["Name"].ToString();
        lblName.Text = dt.Rows[0]["A_Name"].ToString();
        lblDepartment.Text = dt.Rows[0]["A_Department"].ToString();
        lblDepartment2.Text = dt.Rows[0]["A_Department2"].ToString();
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
        lblDQA.Text = dt.Rows[0]["DQA"].ToString();
        strLocation = dt.Rows[0]["Accepted_Team"].ToString();
        Session["ApplicationID"] = dt.Rows[0]["ID"].ToString();

        dTime = Convert.ToDateTime(dt.Rows[0]["Sample_Ready_Date"].ToString());
        strDate = dTime.ToString("yyyy/MM/dd");
        if (strDate != "1900/01/01")
            lblReady.Text = strDate;
        else
            lblReady.Text = "";
        lblUtility.Text = dt.Rows[0]["Utility_Version"].ToString();

        dTime = Convert.ToDateTime(dt.Rows[0]["Expect_Date"].ToString());
        strDate = dTime.ToString("yyyy/MM/dd");
        if (strDate != "1900/01/01")
            lblExpect.Text = strDate;
        else
            lblExpect.Text = "";
        //txtTestCase.Text = 
        txtNote.Text = dt.Rows[0]["Note"].ToString();

        //strArray1 = dt.Rows[0]["Attachmen_File"].ToString();

        //strArray = strArray1.Split(',');
        //gvwMain.DataSource = strArray;

        dt = clsData.UploadProjectFileQuery(Session["ApplicationID"].ToString(), "驗証申請");
        this.gvwMain.DataSource = dt;
        this.DataBind();

        //string[] strNumber1 = strNumber.Split(':');
        //ddlHourB.Text = strNumber1[0];
        //ddlMinB.Text = strNumber1[1];

        DataTable dt1 = clsData.UploadProjectCase(Session["ApplicationID"].ToString());
        string strCase;

        strCase = dt1.Rows[0]["TestCase"].ToString();
        string[] strCase1 = strCase.Split(',');
        string strCase3="";
        strEmail = "";
        foreach (string i in strCase1)
        {
            if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
            {
                string[] strCase2 = i.Split('-');
                DataTable dt3 = clsData.UploadTestCaseName(strCase2[2], strCase2[0], strCase2[1]);
                DataTable dt4 = clsData.UploadTestCaseMail(strCase2[0], strLocation);
                DataTable dt5 = clsData.UploadTestCaseKind(strCase2[0]);

                if (dt4.Rows.Count != 0)
                {
                    for (int intX = 0; intX < dt4.Rows.Count; intX++)
                    {
                        if (strEmail == "")
                            strEmail = dt4.Rows[0]["Email"].ToString();
                        else
                        {
                            if (strEmail.IndexOf(dt4.Rows[0]["Email"].ToString()) < 0)
                                strEmail = strEmail + "," + dt4.Rows[0]["Email"].ToString();
                        }
                    }
                    //if (strEmail == "")
                    //    strEmail = dt4.Rows[0]["Email"].ToString();
                    //else
                    //{
                    //    if (strEmail.IndexOf(dt4.Rows[0]["Email"].ToString()) < 0)
                    //        strEmail = strEmail + "," + dt4.Rows[0]["Email"].ToString();
                    //}
                }
                if ((dt5.Rows[0]["Kind"].ToString() == "LTE") && (dt3.Rows[0]["Item"].ToString() == "TRP/TIS"))
                    LTE.Visible =true;
                else
                    LTE.Visible =false;

                

                if (strCase3 =="")
                    strCase3 = dt3.Rows[0]["Item"].ToString() + " \r\n";
                else
                    strCase3 = strCase3 + dt3.Rows[0]["Item"].ToString() + " \r\n";

            }
        }

        txtTestCase.Text = strCase3;
        Session["Email"] = strEmail;

        int intI = 0;
        dt = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            linkCertification_Wifi.Visible = true;
            intI = 1;
        }
        else
            linkCertification_Wifi.Visible = false;

        dt = clsData.UploadCertification_BT(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            linkCertification_BT.Visible = true;
            intI = 1;
        }
        else
            linkCertification_BT.Visible = false;

        dt = clsData.UploadCertification_GCF(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            linkCertification_GCF.Visible = true;
            intI = 1;
        }
        else
            linkCertification_GCF.Visible = false;

        dt = clsData.UploadCertification_PTCRB(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            linkCertification_PTCRB.Visible = true;
            intI = 1;
        }
        else
            linkCertification_PTCRB.Visible = false;

        if (intI == 0)
            Certification.Visible = false;
        else
            Certification.Visible = true;

        //strTestCase = "";
        //dt = clsData.UploadProjectQuery(strID, "TestCase_DSL");

        //if (dt.Rows.Count > 0)
        //{
        //    strTestCase = "DSL : \r\n";
        //    if (dt.Rows[0]["ADSL_TR067_Inter"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL TR-067 Interoperability Test \r\n";
        //    if (dt.Rows[0]["ADSL_TR067_Loop"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL TR-067 Loop Performance Test \r\n";
        //    if (dt.Rows[0]["ADSL_Electric"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL Electric Test (PSD/LOV/LCL) \r\n";
        //    if (dt.Rows[0]["ADSL2_TR100_Inter"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL2/ADSL2_Plus TR-100 Interoperability Test \r\n";
        //    if (dt.Rows[0]["ADSL2_TR100_Loop"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL2/ADSL2_Plus TR-100 Loop Performance Test \r\n";
        //    if (dt.Rows[0]["ADSL2_Electric"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL2/ADSL2_Plus Electric Test (PSD/LOV/LCL) \r\n";
        //    if (dt.Rows[0]["VDSL2_TR114_Inter"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   VDSL2 TR-114 Interoperability Test \r\n";
        //    if (dt.Rows[0]["VDSL2_TR114_Loop"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   VDSL2 TR-114 Loop Performance Test \r\n";
        //    if (dt.Rows[0]["VDSL2_Electric"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   VDSL2 Electric Test (PSD/LOV/LCL) \r\n";
        //    if (dt.Rows[0]["Bonding_TR273"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   ADSL2+/VDSL2 Bonding TR273 Interoperability Test \r\n";
        //    if (dt.Rows[0]["Remote_TR069"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Remote Management TR-069 \r\n";
        //    if (dt.Rows[0]["Remote_BBF069"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Remote Management BBF069 \r\n";
        //    if (dt.Rows[0]["XDSL_LoopTest"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   xDSL Sample Release Test xDSL Loop Test \r\n";
        //    if (dt.Rows[0]["XDSL_Throughput"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   xDSL Sample Release Test xDSL Throughput Test \r\n";
        //    if (dt.Rows[0]["Router_Basic_Function"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router Basic Function Test \r\n";
        //    if (dt.Rows[0]["Router_CDRouter"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router Networking Test(CD-Router) \r\n";
        //    if (dt.Rows[0]["Router_Full_Function"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router Networking Test(Full Function) \r\n";
        //    if (dt.Rows[0]["Router_VoIP"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   VoIP Functional Test \r\n";
        //    if (dt.Rows[0]["Router_Peripheral"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Peripheral Function Test (USB/SD/Display/NAS/DLAN) \r\n";
        //    if (dt.Rows[0]["Router_ATM"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router ATM Function Test \r\n";
        //    if (dt.Rows[0]["Router_PTM"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router PTM Function Test \r\n";
        //    if (dt.Rows[0]["RFC2544_ATM"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RFC2544 ATM Throughput Test \r\n";
        //    if (dt.Rows[0]["RFC2544_PTM"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RFC2544 PTM Throughput Test \r\n";
        //    if (dt.Rows[0]["RFC2544_LAN"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RFC2544 LAN Throughput Test \r\n";
        //    if (dt.Rows[0]["RFC2544_SFP"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RFC2544 SFP Throughput Test \r\n";
        //    if (dt.Rows[0]["L1_Loop"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   L1 Loop Performance Test (ADSL/ADSL2+/8b/17a) \r\n";
        //    if (dt.Rows[0]["L1_Function"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   L1 Function Test (Inventory/Stability/Bit Swapping/Recovery) \r\n";
        //    if (dt.Rows[0]["L1_Electric"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   L1 Electric Test (PSD) \r\n";
        //}

        //dt = clsData.UploadProjectQuery(strID, "TestCase_Wireless");
        //if (dt.Rows.Count > 0)
        //{
        //    strTestCase = strTestCase + "Wireless : \r\n";
        //    if (dt.Rows[0]["Throughput"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Throughput Test \r\n";
        //    if (dt.Rows[0]["Angle_Test"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Angle Test \r\n";
        //    if (dt.Rows[0]["LOS_Test"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   LOS Test \r\n";
        //    if (dt.Rows[0]["RF_TX"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RF TX Test \r\n";
        //    if (dt.Rows[0]["RF_RX"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RF RX Test \r\n";
        //    if (dt.Rows[0]["Indoor_Throughput"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Indoor Throughput (WJ 廠區) \r\n";
        //    if (dt.Rows[0]["bgn"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   802.11bgn (2.4G) \r\n";
        //    if (dt.Rows[0]["ac"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   802.11an/802.11ac (5G) \r\n";

        //    if (dt.Rows[0]["America"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   11 (美規) \r\n";
        //    }
        //    else if (dt.Rows[0]["Europe"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   13 (歐規)  \r\n";
        //    }
        //    else if (dt.Rows[0]["Japan"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   14 (日規) \r\n";
        //    }

        //    if (dt.Rows[0]["Channel_2G"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   2.4G Test Channel : 1, 6, 11 or 1, 7, 13 \r\n";
        //    if (dt.Rows[0]["Channel_5G"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   5G Test Channel : 36, 64, 100, 149 \r\n";
        //}

        //dt = clsData.UploadProjectQuery(strID, "TestCase_LTE");
        //if (dt.Rows.Count > 0)
        //{
        //    strTestCase = strTestCase + "LTE : \r\n";
        //    if (dt.Rows[0]["Throughput"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   LTE Throughput Test \r\n";
        //    if (dt.Rows[0]["LOS_Test"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   LOS Test \r\n";
        //    if (dt.Rows[0]["RF_TX"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RF TX Test \r\n";
        //    if (dt.Rows[0]["RF_RX"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RF RX Test \r\n";
        //    if (dt.Rows[0]["Web_GUI"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Web GUI / Driver / Utility Test \r\n";
        //    if (dt.Rows[0]["CDRuter"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router Networking Test (CD-Router) \r\n";
        //    if (dt.Rows[0]["Full_Function"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Router Networking Test (Full Function) \r\n";
        //    if (dt.Rows[0]["Sample_Release"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Sample Release Test \r\n";

        //}

        //dt = clsData.UploadProjectQuery(strID, "TestCase_WiFi");
        //if (dt.Rows.Count > 0)
        //{
        //    strTestCase = strTestCase + "WiFi : \r\n";
        //    if (dt.Rows[0]["ProductName"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Customer Product Name：" + dt.Rows[0]["ProductName"] + " \r\n";
        //    if (dt.Rows[0]["ModelNumber"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Customer Model Number：" + dt.Rows[0]["ModelNumber"] + " \r\n";
        //    if (dt.Rows[0]["WEB_Link"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Customer Product WEB link：" + dt.Rows[0]["WEB_Link"] + " \r\n";
        //    if (dt.Rows[0]["CID"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   CID for Certified Device：" + dt.Rows[0]["CID"] + " \r\n";

        //    if (dt.Rows[0]["WPA2_Persional"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   Security Type : WPA2_Persional \r\n";
        //    }
        //    else if (dt.Rows[0]["WPA2_Enterprise"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   Security Type : WPA2_Enterprise \r\n";
        //    }

        //    if (dt.Rows[0]["Single_Band"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   Radio Band : 2.4G or 5G Single Band \r\n";
        //    }
        //    else if (dt.Rows[0]["Dual_Band"].ToString().Trim() == "Y")
        //    {
        //        strTestCase = strTestCase + "   Radio Band : 2.4G or 5G Dual Band \r\n";
        //    }

        //    if (dt.Rows[0]["WPA2"].ToString().Trim() == "Y")
        //        strItem = "WPA2 ,";
        //    if (dt.Rows[0]["WMM"].ToString().Trim() == "Y")
        //        strItem = strItem + "WMM ,";
        //    if (dt.Rows[0]["Power_Save"].ToString().Trim() == "Y")
        //        strItem = strItem + "WMM-Power Save(U-APSD) ,";
        //    if (dt.Rows[0]["WPS"].ToString().Trim() == "Y")
        //        strItem = strItem + "WPS2.0 ,";
        //    if (dt.Rows[0]["N"].ToString().Trim() == "Y")
        //        strItem = strItem + "11N ,";
        //    if (dt.Rows[0]["ac"].ToString().Trim() == "Y")
        //        strItem = strItem + "11ac/Wi-Fi Direct/Passport/Miracast ,";

        //    if (strItem != "")
        //        strTestCase = strTestCase + "   Certification Item : " + strItem + " \r\n";

        //    strItem = "";

        //    if ((dt.Rows[0]["Cost_Y"].ToString()).Trim() == "Y")
        //        strItem = "有, ";
        //    else if ((dt.Rows[0]["Cost_N"].ToString()).Trim() == "Y")
        //        strItem = "無, ";

        //    if ((dt.Rows[0]["Pay_Askey"].ToString()).Trim() == "Y")
        //        strItem = strItem + "Askey支付, ";
        //    else if ((dt.Rows[0]["Pay_Customer"].ToString()).Trim() == "Y")
        //        strItem = strItem + "客戶支付, ";
        //    if (dt.Rows[0]["Cost"] != null)
        //        strItem = strItem + "金額 : " + dt.Rows[0]["Cost"];

        //    if (strItem != "")
        //        strTestCase = strTestCase + "   外測費用 : " + strItem + " \r\n";

        //}

        //dt = clsData.UploadProjectQuery(strID, "TestCase_USB");
        //if (dt.Rows.Count > 0)
        //{
        //    strTestCase = strTestCase + "USB : \r\n";
        //    if (dt.Rows[0]["Low_Speed"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Low Speed(1.5Mb/s) \r\n";
        //    if (dt.Rows[0]["High_Speed"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   High Speed(480Mb/s) \r\n";
        //    if (dt.Rows[0]["Full_Speed"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   Full Speed(12Mb/s) \r\n";

        //    strItem = "";

        //    if ((dt.Rows[0]["Cost_Y"].ToString()).Trim() == "Y")
        //        strItem = "有, ";
        //    else if ((dt.Rows[0]["Cost_N"].ToString()).Trim() == "Y")
        //        strItem = "無, ";

        //    if ((dt.Rows[0]["Pay_Askey"].ToString()).Trim() == "Y")
        //        strItem = strItem + "Askey支付, ";
        //    else if ((dt.Rows[0]["Pay_Customer"].ToString()).Trim() == "Y")
        //        strItem = strItem + "客戶支付, ";
        //    if (dt.Rows[0]["Cost"] != null)
        //        strItem = strItem + "金額 : " + dt.Rows[0]["Cost"];

        //    if (strItem != "")
        //        strTestCase = strTestCase + "   外測費用 : " + strItem + " \r\n";

        //}

        //dt = clsData.UploadProjectQuery(strID, "TestCase_Bluetooth");
        //if (dt.Rows.Count > 0)
        //{
        //    strTestCase = strTestCase + "Bluetooth : \r\n";
        //    if (dt.Rows[0]["RF_Testing"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   RF Testing(2.0 Version：15 test case; 35 working hours) \r\n";
        //    if (dt.Rows[0]["EDR_Testing"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   EDR testing(8 test cases; 10 working days) \r\nn";
        //    if (dt.Rows[0]["BQB_Testing"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   BQB Review(2.0 Version & EDR Review) \r\n";
        //    if (dt.Rows[0]["SIG_Listing"].ToString().Trim() == "Y")
        //        strTestCase = strTestCase + "   SIG Listing \r\n";

        //    strItem = "";

        //    if ((dt.Rows[0]["Cost_Y"].ToString()).Trim() == "Y")
        //        strItem = "有, ";
        //    else if ((dt.Rows[0]["Cost_N"].ToString()).Trim() == "Y")
        //        strItem = "無, ";

        //    if ((dt.Rows[0]["Pay_Askey"].ToString()).Trim() == "Y")
        //        strItem = strItem + "Askey支付, ";
        //    else if ((dt.Rows[0]["Pay_Customer"].ToString()).Trim() == "Y")
        //        strItem = strItem + "客戶支付, ";
        //    if (dt.Rows[0]["Cost"] != null)
        //        strItem = strItem + "金額 : " + dt.Rows[0]["Cost"];

        //    if (strItem != "")
        //        strTestCase = strTestCase + "   外測費用 : " + strItem + " \r\n";

        //}

        //txtTestCase.Text = strTestCase;




    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery(true);
    }
    #endregion

    #region GvQuery
    private void GvQuery(Boolean IsPage)
    {
        if (IsPage != true)
            this.gvwMain.PageIndex = 0;
        DataTable dt = clsData.UploadProjectFileQuery(Session["ApplicationID"].ToString(), "驗証申請");
        this.gvwMain.DataSource = dt;
        this.DataBind();

    }
    #endregion

    protected void lbtnWifi_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_Wifi.aspx?ID=" + Session["ApplicationID"] + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1300,height=950');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }

    protected void lbtnBT_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_BT.aspx?ID=" + Session["ApplicationID"] + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);
    }

    protected void lbtnGCF_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_GCF.aspx?ID=" + Session["ApplicationID"] + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);

    }
    protected void lbtnPTCRB_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Certification_PTCRB.aspx?ID=" + Session["ApplicationID"] + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);

    }
    protected void lbtnForm_Click(object sender, EventArgs e)
    {
        string win_str;

        win_str = @"window.open('../WebForm/Application_LTE.aspx?ID=" + Session["ApplicationID"].ToString() + "','新開視窗','status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1200,height=900');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "另開視窗", win_str, true);

    }
}
