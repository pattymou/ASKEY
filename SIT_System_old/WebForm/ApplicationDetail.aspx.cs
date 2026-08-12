using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
//using System.Web.Mvc;

public partial class WebForm_ApplicationDetail : System.Web.UI.Page
{
    //public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            //strID = "20141217151651";
            //rdoAccpt.Checked = true;
            //loadCustomer(this.ddlCustomer);
            //loadDepartment(this.ddlDepartment);
            getProject();
        }
    }


    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, "0");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    private void getProject()
    {
        //string strID;
        string strTestCase;
        string strItem = "";
        string strDate;
        DateTime dTime;
        //string[] strArray;
        //string strArray1;

        //strID = Request.QueryString["ID"];
        //strID = "20141201115442";
        DataTable dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "Project");

        //DataRow dr = dt.Rows;
        lblID.Text = dt.Rows[0]["Name"].ToString();
        lblName.Text = dt.Rows[0]["A_Name"].ToString();
        lblDepartment.Text = dt.Rows[0]["A_Department"].ToString();
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
        lblName.Text = dt.Rows[0]["Note"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        lblName.Visible = false;

        //strArray1 = dt.Rows[0]["Attachmen_File"].ToString();

        //strArray = strArray1.Split(',');
        //gvwMain.DataSource = strArray;

        dt = clsData.UploadProjectFileQuery(Request.QueryString["ID"].ToString(), "驗証申請");
        this.gvwMain.DataSource = dt;
        this.DataBind();





        strTestCase = "";
        dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "TestCase_DSL");

        if (dt.Rows.Count > 0)
        {
            strTestCase = "DSL : \r\n";
            if (dt.Rows[0]["ADSL_TR067_Inter"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL TR-067 Interoperability Test \r\n";
            if (dt.Rows[0]["ADSL_TR067_Loop"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL TR-067 Loop Performance Test \r\n";
            if (dt.Rows[0]["ADSL_Electric"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL Electric Test (PSD/LOV/LCL) \r\n";
            if (dt.Rows[0]["ADSL2_TR100_Inter"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL2/ADSL2_Plus TR-100 Interoperability Test \r\n";
            if (dt.Rows[0]["ADSL2_TR100_Loop"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL2/ADSL2_Plus TR-100 Loop Performance Test \r\n";
            if (dt.Rows[0]["ADSL2_Electric"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL2/ADSL2_Plus Electric Test (PSD/LOV/LCL) \r\n";
            if (dt.Rows[0]["VDSL2_TR114_Inter"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   VDSL2 TR-114 Interoperability Test \r\n";
            if (dt.Rows[0]["VDSL2_TR114_Loop"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   VDSL2 TR-114 Loop Performance Test \r\n";
            if (dt.Rows[0]["VDSL2_Electric"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   VDSL2 Electric Test (PSD/LOV/LCL) \r\n";
            if (dt.Rows[0]["Bonding_TR273"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   ADSL2+/VDSL2 Bonding TR273 Interoperability Test \r\n";
            if (dt.Rows[0]["Remote_TR069"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Remote Management TR-069 \r\n";
            if (dt.Rows[0]["Remote_BBF069"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Remote Management BBF069 \r\n";
            if (dt.Rows[0]["XDSL_LoopTest"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   xDSL Sample Release Test xDSL Loop Test \r\n";
            if (dt.Rows[0]["XDSL_Throughput"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   xDSL Sample Release Test xDSL Throughput Test \r\n";
            if (dt.Rows[0]["Router_Basic_Function"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router Basic Function Test \r\n";
            if (dt.Rows[0]["Router_CDRouter"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router Networking Test(CD-Router) \r\n";
            if (dt.Rows[0]["Router_Full_Function"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router Networking Test(Full Function) \r\n";
            if (dt.Rows[0]["Router_VoIP"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   VoIP Functional Test \r\n";
            if (dt.Rows[0]["Router_Peripheral"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Peripheral Function Test (USB/SD/Display/NAS/DLAN) \r\n";
            if (dt.Rows[0]["Router_ATM"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router ATM Function Test \r\n";
            if (dt.Rows[0]["Router_PTM"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router PTM Function Test \r\n";
            if (dt.Rows[0]["RFC2544_ATM"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RFC2544 ATM Throughput Test \r\n";
            if (dt.Rows[0]["RFC2544_PTM"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RFC2544 PTM Throughput Test \r\n";
            if (dt.Rows[0]["RFC2544_LAN"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RFC2544 LAN Throughput Test \r\n";
            if (dt.Rows[0]["RFC2544_SFP"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RFC2544 SFP Throughput Test \r\n";
            if (dt.Rows[0]["L1_Loop"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   L1 Loop Performance Test (ADSL/ADSL2+/8b/17a) \r\n";
            if (dt.Rows[0]["L1_Function"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   L1 Function Test (Inventory/Stability/Bit Swapping/Recovery) \r\n";
            if (dt.Rows[0]["L1_Electric"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   L1 Electric Test (PSD) \r\n";
        }

        dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "TestCase_Wireless");
        if (dt.Rows.Count > 0)
        {
            strTestCase = strTestCase + "Wireless : \r\n";
            if (dt.Rows[0]["Throughput"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Throughput Test \r\n";
            if (dt.Rows[0]["Angle_Test"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Angle Test \r\n";
            if (dt.Rows[0]["LOS_Test"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   LOS Test \r\n";
            if (dt.Rows[0]["RF_TX"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RF TX Test \r\n";
            if (dt.Rows[0]["RF_RX"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RF RX Test \r\n";
            if (dt.Rows[0]["Indoor_Throughput"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Indoor Throughput (WJ 廠區) \r\n";
            if (dt.Rows[0]["bgn"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   802.11bgn (2.4G) \r\n";
            if (dt.Rows[0]["ac"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   802.11an/802.11ac (5G) \r\n";

            if (dt.Rows[0]["America"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   11 (美規) \r\n";
            }
            else if (dt.Rows[0]["Europe"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   13 (歐規)  \r\n";
            }
            else if (dt.Rows[0]["Japan"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   14 (日規) \r\n";
            }

            if (dt.Rows[0]["Channel_2G"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   2.4G Test Channel : 1, 6, 11 or 1, 7, 13 \r\n";
            if (dt.Rows[0]["Channel_5G"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   5G Test Channel : 36, 64, 100, 149 \r\n";
        }

        dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "TestCase_LTE");
        if (dt.Rows.Count > 0)
        {
            strTestCase = strTestCase + "LTE : \r\n";
            if (dt.Rows[0]["Throughput"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   LTE Throughput Test \r\n";
            if (dt.Rows[0]["LOS_Test"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   LOS Test \r\n";
            if (dt.Rows[0]["RF_TX"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RF TX Test \r\n";
            if (dt.Rows[0]["RF_RX"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RF RX Test \r\n";
            if (dt.Rows[0]["Web_GUI"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Web GUI / Driver / Utility Test \r\n";
            if (dt.Rows[0]["CDRuter"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router Networking Test (CD-Router) \r\n";
            if (dt.Rows[0]["Full_Function"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Router Networking Test (Full Function) \r\n";
            if (dt.Rows[0]["Sample_Release"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Sample Release Test \r\n";

        }

        dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "TestCase_WiFi");
        if (dt.Rows.Count > 0)
        {
            strTestCase = strTestCase + "WiFi : \r\n";
            if (dt.Rows[0]["ProductName"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Customer Product Name：" + dt.Rows[0]["ProductName"] + " \r\n";
            if (dt.Rows[0]["ModelNumber"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Customer Model Number：" + dt.Rows[0]["ModelNumber"] + " \r\n";
            if (dt.Rows[0]["WEB_Link"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Customer Product WEB link：" + dt.Rows[0]["WEB_Link"] + " \r\n";
            if (dt.Rows[0]["CID"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   CID for Certified Device：" + dt.Rows[0]["CID"] + " \r\n";

            if (dt.Rows[0]["WPA2_Persional"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   Security Type : WPA2_Persional \r\n";
            }
            else if (dt.Rows[0]["WPA2_Enterprise"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   Security Type : WPA2_Enterprise \r\n";
            }

            if (dt.Rows[0]["Single_Band"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   Radio Band : 2.4G or 5G Single Band \r\n";
            }
            else if (dt.Rows[0]["Dual_Band"].ToString().Trim() == "Y")
            {
                strTestCase = strTestCase + "   Radio Band : 2.4G or 5G Dual Band \r\n";
            }

            if (dt.Rows[0]["WPA2"].ToString().Trim() == "Y")
                strItem = "WPA2 ,";
            if (dt.Rows[0]["WMM"].ToString().Trim() == "Y")
                strItem = strItem + "WMM ,";
            if (dt.Rows[0]["Power_Save"].ToString().Trim() == "Y")
                strItem = strItem + "WMM-Power Save(U-APSD) ,";
            if (dt.Rows[0]["WPS"].ToString().Trim() == "Y")
                strItem = strItem + "WPS2.0 ,";
            if (dt.Rows[0]["N"].ToString().Trim() == "Y")
                strItem = strItem + "11N ,";
            if (dt.Rows[0]["ac"].ToString().Trim() == "Y")
                strItem = strItem + "11ac/Wi-Fi Direct/Passport/Miracast ,";

            if (strItem != "")
                strTestCase = strTestCase + "   Certification Item : " + strItem + " \r\n";

            strItem = "";

            if ((dt.Rows[0]["Cost_Y"].ToString()).Trim() == "Y")
                strItem = "有, ";
            else if ((dt.Rows[0]["Cost_N"].ToString()).Trim() == "Y")
                strItem = "無, ";

            if ((dt.Rows[0]["Pay_Askey"].ToString()).Trim() == "Y")
                strItem = strItem + "Askey支付, ";
            else if ((dt.Rows[0]["Pay_Customer"].ToString()).Trim() == "Y")
                strItem = strItem + "客戶支付, ";
            if (dt.Rows[0]["Cost"] != null)
                strItem = strItem + "金額 : " + dt.Rows[0]["Cost"];

            if (strItem != "")
                strTestCase = strTestCase + "   外測費用 : " + strItem + " \r\n";

        }

        dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "TestCase_USB");
        if (dt.Rows.Count > 0)
        {
            strTestCase = strTestCase + "USB : \r\n";
            if (dt.Rows[0]["Low_Speed"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Low Speed(1.5Mb/s) \r\n";
            if (dt.Rows[0]["High_Speed"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   High Speed(480Mb/s) \r\n";
            if (dt.Rows[0]["Full_Speed"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   Full Speed(12Mb/s) \r\n";

            strItem = "";

            if ((dt.Rows[0]["Cost_Y"].ToString()).Trim() == "Y")
                strItem = "有, ";
            else if ((dt.Rows[0]["Cost_N"].ToString()).Trim() == "Y")
                strItem = "無, ";

            if ((dt.Rows[0]["Pay_Askey"].ToString()).Trim() == "Y")
                strItem = strItem + "Askey支付, ";
            else if ((dt.Rows[0]["Pay_Customer"].ToString()).Trim() == "Y")
                strItem = strItem + "客戶支付, ";
            if (dt.Rows[0]["Cost"] != null)
                strItem = strItem + "金額 : " + dt.Rows[0]["Cost"];

            if (strItem != "")
                strTestCase = strTestCase + "   外測費用 : " + strItem + " \r\n";

        }

        dt = clsData.UploadProjectQuery(Request.QueryString["ID"].ToString(), "TestCase_Bluetooth");
        if (dt.Rows.Count > 0)
        {
            strTestCase = strTestCase + "Bluetooth : \r\n";
            if (dt.Rows[0]["RF_Testing"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   RF Testing(2.0 Version：15 test case; 35 working hours) \r\n";
            if (dt.Rows[0]["EDR_Testing"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   EDR testing(8 test cases; 10 working days) \r\nn";
            if (dt.Rows[0]["BQB_Testing"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   BQB Review(2.0 Version & EDR Review) \r\n";
            if (dt.Rows[0]["SIG_Listing"].ToString().Trim() == "Y")
                strTestCase = strTestCase + "   SIG Listing \r\n";

            strItem = "";

            if ((dt.Rows[0]["Cost_Y"].ToString()).Trim() == "Y")
                strItem = "有, ";
            else if ((dt.Rows[0]["Cost_N"].ToString()).Trim() == "Y")
                strItem = "無, ";

            if ((dt.Rows[0]["Pay_Askey"].ToString()).Trim() == "Y")
                strItem = strItem + "Askey支付, ";
            else if ((dt.Rows[0]["Pay_Customer"].ToString()).Trim() == "Y")
                strItem = strItem + "客戶支付, ";
            if (dt.Rows[0]["Cost"] != null)
                strItem = strItem + "金額 : " + dt.Rows[0]["Cost"];

            if (strItem != "")
                strTestCase = strTestCase + "   外測費用 : " + strItem + " \r\n";

        }
        lblTestCase.Text = strTestCase.Replace("\r\n", "<br>");
        lblTestCase.Visible = false;
        txtTestCase.Text = strTestCase;




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
        DataTable dt = clsData.UploadProjectFileQuery(Request.QueryString["ID"].ToString(), "驗証申請");
        this.gvwMain.DataSource = dt;
        this.DataBind();

    }
    #endregion
    protected void butOK_Click(object sender, EventArgs e)
    {
        //string strHtml = Form["hHtml"];
        //strHtml = HttpUtility.HtmlDecode(strHtml);
        //byte[] b = System.Text.Encoding.Default.GetBytes(strHtml);
        //return File(b, "application/vnd.ms-excel", "test.xls");

        txtTestCase.Visible = false;
        lblTestCase.Visible = true;
        txtNote.Visible = false;
        lblNote.Visible = true;
        //string strStyle = "";
        //strStyle += @"<style>.height {mso-height-source:userset;height:2000pt} </style>";

        Response.ContentType = "application/x-msexcel";
        Response.AddHeader("Content-Disposition", "attachment;filename=Application.xls");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        tb1.RenderControl(hw);
        //Response.Write(strStyle);
        Response.Write(tw.ToString());
        Response.End();
        txtTestCase.Visible = true;
        lblTestCase.Visible = false;
        txtNote.Visible = true;
        lblNote.Visible = false;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        
        
    }
}
