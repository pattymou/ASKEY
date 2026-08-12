using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebForm_Certification_Wifi : System.Web.UI.Page
{
    //public static string strAP;
    public static string strDate;
    public static string strPublish_Date;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ApplicationID"] = Request.QueryString["ID"];
            //Session["ApplicationID"] = "20200330174641";
            //ddlAP.Visible = true;
            //ddlSTA.Visible = false;
            //ddlAPS.Visible = true;
            //ddlSTAS.Visible = false;
            divAP.Visible = true;
            divSTA.Visible = false;
            divAPS.Visible = true;
            divSTAS.Visible = false;
            Deferred.Visible = false;

            DataTable dt = clsData.UploadCertification_Wifi_Data("1","0");
            lblMandatory.Text = dt.Rows[0]["Description"].ToString();

            dt = clsData.UploadCertification_Wifi_Data("2","0");
            lblMandatory1.Text = dt.Rows[0]["Description"].ToString();

            dt = clsData.UploadCertification_Wifi_Data("3","0");
            lblMandatory2.Text = dt.Rows[0]["Description"].ToString();

            DataTable dt1 = clsData.UploadCertification_Wifi_Data("1","1");
            for (int intI = 0; intI < dt1.Rows.Count; intI++)
            {
                listLeft.Items.Add(dt1.Rows[intI]["Name"].ToString());
            }
            //if (dt1.Rows.Count != 0)
            //{
            //    listLeft.DataSource = dt1;
            //    listLeft.DataBind();
            //}
            listLeft.Attributes.Add("onclick", ClientScript.GetPostBackEventReference(listLeft, "event 1"));

            getData();
        }
    }

    private void getData()
    {
        DataTable dt;

        //dt = clsData.UploadCertification_Wifi("1234");
        dt = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
            txtModelNumber.Text = dt.Rows[0]["ModelNumber"].ToString();
            txtChipset.Text = dt.Rows[0]["WirelessChipset"].ToString();
            txtProductOperating.Text = dt.Rows[0]["ProductOperatingSystem"].ToString();
            txtOSVersion.Text = dt.Rows[0]["OSVersion"].ToString();
            txtPHardware.Text = dt.Rows[0]["HardwareVersion_Product"].ToString();
            txtPFirmware.Text = dt.Rows[0]["FirmwareVersion_Product"].ToString();
            txtWHardware.Text = dt.Rows[0]["HardwareVersion_WiFi"].ToString();
            txtWFirmware.Text = dt.Rows[0]["FirmwareVersion_WiFi"].ToString();
            txtPNote.Text = dt.Rows[0]["ProductNotes"].ToString();
            if (dt.Rows[0]["Searchable"].ToString() == "Yes")
                ddlSearchable.Text = "Yes";
            else
                ddlSearchable.Text = "No";

            if (dt.Rows[0]["Publish"].ToString() == "Certification Data")
            {
                ddlPublish.Text = "Certification Data";
                Deferred.Visible = false;
            }
            else if (dt.Rows[0]["Publish"].ToString() == "Deferred Date")
            {
                ddlPublish.Text = "Deferred Date";
                Deferred.Visible = true;
            }
            else if (dt.Rows[0]["Publish"].ToString() == "Never")
            {
                ddlPublish.Text = "Never";
                Deferred.Visible = false;
            }

            DateTime dt1;
            string strDate1;
            dt1 = Convert.ToDateTime(dt.Rows[0]["Publish_Date"].ToString());
            strDate1 = dt1.ToString("yyyy/MM/dd");
            if (strDate1 == "1900/01/01")
                strDate = "";
            else
                strDate = strDate1;

            if (dt.Rows[0]["DeviceType"].ToString() == "Personal")
                ddlDeveicType.Text = "Personal";
            else
                ddlDeveicType.Text = "Enterprise";


            string strRelated;
            string[] sArray;
            if (dt.Rows[0]["ProductType"].ToString() == "AP")
            {
                ddlProductType.Text = "AP";
                divAP.Visible = true;
                divAPS.Visible = true;
                divSTA.Visible = false;
                divSTAS.Visible = false;
                ddlAP.Text = dt.Rows[0]["PrimaryProductCategory"].ToString();
                ddlAPS.Text = dt.Rows[0]["SecondaryProductCategory"].ToString();
            }
            else if (dt.Rows[0]["ProductType"].ToString() == "STA")
            {
                ddlProductType.Text = "STA";
                divSTA.Visible = true;
                divSTAS.Visible = true;
                divAP.Visible = false;
                divAPS.Visible = false;
                ddlSTA.Text = dt.Rows[0]["PrimaryProductCategory"].ToString();
                ddlSTAS.Text = dt.Rows[0]["SecondaryProductCategory"].ToString();
            }
            else if (dt.Rows[0]["ProductType"].ToString() == "Mobile AP")
            {
                ddlProductType.Text = "Mobile AP";
                divAP.Visible = true;
                divAPS.Visible = true;
                divSTA.Visible = false;
                divSTAS.Visible = false;
                ddlAP.Text = dt.Rows[0]["PrimaryProductCategory"].ToString();
                ddlAPS.Text = dt.Rows[0]["SecondaryProductCategory"].ToString();
            }
            else if (dt.Rows[0]["ProductType"].ToString() == "STA(20MHz)")
            {
                ddlProductType.Text = "STA(20MHz)";
                divSTA.Visible = true;
                divSTAS.Visible = true;
                divAP.Visible = false;
                divAPS.Visible = false;
                ddlSTA.Text = dt.Rows[0]["PrimaryProductCategory"].ToString();
                ddlSTAS.Text = dt.Rows[0]["SecondaryProductCategory"].ToString();
            }


            strRelated = dt.Rows[0]["LeastOneBand"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                if (i == "2.4 GHz")
                    chkBand.Checked = true;
                if (i == "5 GHz")
                    chkBand1.Checked = true;
                if (i == "WiGig")
                    chkBand2.Checked = true;
            }

            if (dt.Rows[0]["MandatoryProgram"].ToString() == "Wi-Fi CERTIFIED ac & n")
            {
                rdoMandatory.Checked = true;
            }
            else if (dt.Rows[0]["MandatoryProgram"].ToString() == "Wi-Fi CERTIFIED n")
            {
                rdoMandatory1.Checked = true;
            }
            else if (dt.Rows[0]["MandatoryProgram"].ToString() == "Wi-Fi CERTIFIED 6")
            {
                rdoMandatory2.Checked = true;
            }

            strRelated = dt.Rows[0]["OptionalProgram"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                this.listRight.Items.Add(i);
                this.listLeft.Items.Remove(i);
            }

            strRelated = dt.Rows[0]["SupportedSpatialStreams_Tx"].ToString();
            sArray = strRelated.Split(',');
            ddlStream_T_2.Text = sArray[0].ToString();
            ddlStream_T_5.Text = sArray[1].ToString();

            strRelated = dt.Rows[0]["SupportedSpatialStreams_Rx"].ToString();
            sArray = strRelated.Split(',');
            ddlStream_R_2.Text = sArray[0].ToString();
            ddlStream_R_5.Text = sArray[1].ToString();

            strRelated = dt.Rows[0]["AdditionalCapabilities"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                if (i == "Power saving features")
                    chkAdditional.Checked = true;
                if (i == "Wi-Fi Enhanced Open")
                    chkAdditional1.Checked = true;

            }

            strRelated = dt.Rows[0]["SecurityType"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                if (i == "WPA")
                    chkSecurity.Checked = true;
                if (i == "WPA2")
                    chkSecurity1.Checked = true;
                if (i == "WPA3")
                    chkSecurity2.Checked = true;
                if (i == "WEP Support")
                    chkSecurity3.Checked = true;

            }

            strRelated = dt.Rows[0]["SpectrumAndRegulatoryFeatures"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                if (i == "802.11h")
                    chkSpectrum.Checked = true;


            }

            strRelated = dt.Rows[0]["NOptionalFeature"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                if (i == "Short Guard Interval 20 MHz")
                    chk11nOptional.Checked = true;
                if (i == "Short Guard Interval 40 MHz")
                    chk11nOptional1.Checked = true;
                if (i == "TX A-MPDU")
                    chk11nOptional2.Checked = true;
                if (i == "STBC")
                    chk11nOptional3.Checked = true;
                if (i == "40MHz operation in 2.4GHz with coexistence mechanisms")
                    chk11nOptional4.Checked = true;
                if (i == "40MHz operation in 5GHz")
                    chk11nOptional5.Checked = true;
                if (i == "HT Duplicate Mode (MCS 32)")
                    chk11nOptional6.Checked = true;
                if (i == "OBSS on Extension Channel")
                    chk11nOptional7.Checked = true;
                if (i == "STAUT Power Management")
                    chk11nOptional8.Checked = true;


            }

            strRelated = dt.Rows[0]["ACOptionalFeature"].ToString();
            sArray = strRelated.Split(',');
            foreach (string i in sArray)
            {
                if (i == "Rx MCS 8 (256-QAM)")
                    chk11acOptional.Checked = true;
                if (i == "Rx MCS 8-9 (256-QAM)")
                    chk11acOptional1.Checked = true;
                if (i == "Rx Short Guard Interval")
                    chk11acOptional2.Checked = true;
                if (i == "STBC 2x1")
                    chk11acOptional3.Checked = true;
                if (i == "Rx A-MPDU of A-MSDU")
                    chk11acOptional4.Checked = true;
                if (i == "Tx LDPC")
                    chk11acOptional5.Checked = true;
                if (i == "Rx LDPC")
                    chk11acOptional6.Checked = true;
                if (i == "Tx SU beamformee / beamformer")
                    chk11acOptional7.Checked = true;
                if (i == "DL MU-MIMO")
                    chk11acOptional8.Checked = true;
                if (i == "RTS with BW Signaling")
                    chk11acOptional9.Checked = true;
                if (i == "Rx 160 MHz operations")
                    chk11acOptional10.Checked = true;
                if (i == "Extended 5 GHz Channel Support")
                    chk11acOptional11.Checked = true;



            }

        }


    }

    protected void btnRight_Click(object sender, EventArgs e)
    {
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
    }
    protected void btnLeft_Click(object sender, EventArgs e)
    {
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
    }

    protected void listLeft_Click(object sender, EventArgs e)
    {
        lblDescription.Text = "123";
    }

    protected void listLeft_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        // Get the currently selected item in the ListBox.
        lblDescription.Text = listLeft.SelectedItem.ToString();
 
        //lblDescription.Text = "123";


    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((ddlProductType.Text == "AP") || (ddlProductType.Text == "Mobile AP"))
        {
            //ddlAP.Visible = true;
            //ddlAPS.Visible = true;
            divAP.Visible = true;
            divAPS.Visible = true;
        }
        else
        {
            //ddlAP.Visible = false;
            //ddlAPS.Visible = false;
            divAP.Visible = false;
            divAPS.Visible = false;
        }

        if ((ddlProductType.Text == "STA") || (ddlProductType.Text == "STA(20MHz)"))
        {
            //ddlSTA.Visible = true;
            //ddlSTAS.Visible = true;
            divSTA.Visible = true;
            divSTAS.Visible = true;
        }
        else
        {
            //ddlSTA.Visible = false;
            //ddlSTAS.Visible = false;
            divSTA.Visible = false;
            divSTAS.Visible = false;
        }
        


    }

    protected void ddlPublish_SelectedIndexChanged(object sender, EventArgs e)
    {

        if ((ddlPublish.Text == "Certification Data") || (ddlPublish.Text == "Never"))
        {
            Deferred.Visible = false;
        }
        else
            Deferred.Visible = true;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string str123;
        str123 = Request["ddlAP1"].ToString();
    }
    protected void butOK_Click(object sender, EventArgs e)
    {

        //string str123 = ddHospital.Text;
        //string str456 = "";
        string strProjectID = Session["ApplicationID"].ToString();
        string strProductName, strModelNumber, strWirelessChipset, strProductOperatingSystem, strOSVersion, strHardwareVersion_Product, strFirmwareVersion_Product, strHardwareVersion_WiFi, strFirmwareVersion_WiFi, strProductNotes, strSearchable, strPublish, strDeviceType, strProductType, strPrimaryProductCategory, strSecondaryProductCategory, strLeastOneBand, strMandatoryProgram, strOptionalProgram, strSupportedSpatialStreams_Tx, strSupportedSpatialStreams_Rx, strAdditionalCapabilities, strSecurityType, strSpectrumAndRegulatoryFeatures, strNOptionalFeature, strACOptionalFeature;
        DateTime dt;

        strProductName = txtProductName.Text.Trim();
        strWirelessChipset = txtChipset.Text.Trim();
        strModelNumber = txtModelNumber.Text.Trim();
        strProductOperatingSystem = txtProductOperating.Text.Trim();
        strOSVersion = txtOSVersion.Text.Trim();
        strHardwareVersion_Product = txtPHardware.Text.Trim();
        strFirmwareVersion_Product = txtPFirmware.Text.Trim();
        strHardwareVersion_WiFi = txtWHardware.Text.Trim();
        strFirmwareVersion_WiFi = txtWFirmware.Text.Trim();
        strSearchable = ddlSearchable.Text;
        strProductNotes = txtPNote.Text;
        strPublish = ddlPublish.Text;
        //if ((Request["date1"].ToString() == "") || (Request["date1"].ToString() == null))
        //    strPublish_Date = "";
        //else
        if (strPublish == "Deferred Date")
        {
            strPublish_Date = Request["date1"].ToString();
            if (strPublish_Date != "")
            {
                dt = Convert.ToDateTime(strPublish_Date);
                strPublish_Date = dt.ToString("yyyyMMdd");
            }
        }
        else
            strPublish_Date = "";
        strDeviceType = ddlDeveicType.Text;
        strProductType = ddlProductType.Text;
        if ((strProductType == "AP") || (strProductType == "Mobile AP"))
        {
            strPrimaryProductCategory = ddlAP.Text;
            strSecondaryProductCategory = ddlAPS.Text;
        }
        else
        {
            strPrimaryProductCategory = ddlSTA.Text;
            strSecondaryProductCategory = ddlSTAS.Text;
        }
        strLeastOneBand = "";
        if (chkBand.Checked == true)
            strLeastOneBand = strLeastOneBand + chkBand.Text + ",";
        if (chkBand1.Checked == true)
            strLeastOneBand = strLeastOneBand + chkBand1.Text + ",";
        if (chkBand2.Checked == true)
            strLeastOneBand = strLeastOneBand + chkBand2.Text + ",";

        strMandatoryProgram = "";
        if (rdoMandatory.Checked == true)
            strMandatoryProgram = rdoMandatory.Text;
        else if (rdoMandatory1.Checked == true)
            strMandatoryProgram = rdoMandatory1.Text;
        else if (rdoMandatory2.Checked == true)
            strMandatoryProgram = rdoMandatory2.Text;

        strOptionalProgram = "";
        int count = listRight.Items.Count;
        for (int i = 0; i < count; i++)
        {
            ListItem item = listRight.Items[i];

            if (strOptionalProgram == "")
                strOptionalProgram = item.Text;
            else
                strOptionalProgram = strOptionalProgram + "," + item.Text;

        }

        strSupportedSpatialStreams_Tx = ddlStream_T_2.Text + "," + ddlStream_T_5.Text;
        strSupportedSpatialStreams_Rx = ddlStream_R_2.Text + "," + ddlStream_R_5.Text;

        strAdditionalCapabilities = "";
        if (chkAdditional.Checked == true)
            strAdditionalCapabilities = strAdditionalCapabilities + chkAdditional.Text + ",";
        if (chkAdditional1.Checked == true)
            strAdditionalCapabilities = strAdditionalCapabilities + chkAdditional1.Text + ",";

        strSecurityType = "";
        if (chkSecurity.Checked == true)
            strSecurityType = strSecurityType + chkSecurity.Text + ",";
        if (chkSecurity1.Checked == true)
            strSecurityType = strSecurityType + chkSecurity1.Text + ",";
        if (chkSecurity2.Checked == true)
            strSecurityType = strSecurityType + chkSecurity2.Text + ",";
        if (chkSecurity3.Checked == true)
            strSecurityType = strSecurityType + chkSecurity3.Text + ",";

        strSpectrumAndRegulatoryFeatures = "";
        if (chkSpectrum.Checked == true)
            strSpectrumAndRegulatoryFeatures = strSpectrumAndRegulatoryFeatures + chkSpectrum.Text + ",";

        strNOptionalFeature = "";
        if (chk11nOptional.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional.Text + ",";
        if (chk11nOptional1.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional1.Text + ",";
        if (chk11nOptional2.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional2.Text + ",";
        if (chk11nOptional3.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional3.Text + ",";
        if (chk11nOptional4.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional4.Text + ",";
        if (chk11nOptional5.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional5.Text + ",";
        if (chk11nOptional6.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional6.Text + ",";
        if (chk11nOptional7.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional7.Text + ",";
        if (chk11nOptional8.Checked == true)
            strNOptionalFeature = strNOptionalFeature + chk11nOptional8.Text + ",";

        strACOptionalFeature = "";
        if (chk11acOptional.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional.Text + ",";
        if (chk11acOptional1.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional1.Text + ",";
        if (chk11acOptional2.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional2.Text + ",";
        if (chk11acOptional3.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional3.Text + ",";
        if (chk11acOptional4.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional4.Text + ",";
        if (chk11acOptional5.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional5.Text + ",";
        if (chk11acOptional6.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional6.Text + ",";
        if (chk11acOptional7.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional7.Text + ",";
        if (chk11acOptional8.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional8.Text + ",";
        if (chk11acOptional9.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional9.Text + ",";
        if (chk11acOptional10.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional10.Text + ",";
        if (chk11acOptional11.Checked == true)
            strACOptionalFeature = strACOptionalFeature + chk11acOptional11.Text + ",";


        DataTable dt1 = clsData.UploadCertification_Wifi(strProjectID);
        //DataTable dt1 = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
        if (dt1.Rows.Count > 0)
        {
            if (clsTransaction.UpDateCertification_Wifi(strProjectID, strProductName, strModelNumber, strWirelessChipset, strProductOperatingSystem, strOSVersion, strHardwareVersion_Product, strFirmwareVersion_Product, strHardwareVersion_WiFi, strFirmwareVersion_WiFi, strProductNotes, strSearchable, strPublish, strPublish_Date, strDeviceType, strProductType, strPrimaryProductCategory, strSecondaryProductCategory, strLeastOneBand, strMandatoryProgram, strOptionalProgram, strSupportedSpatialStreams_Tx, strSupportedSpatialStreams_Rx, strAdditionalCapabilities, strSecurityType, strSpectrumAndRegulatoryFeatures, strNOptionalFeature, strACOptionalFeature) == true)
                clsMsg.AlertMessage("暫存成功....", this.Page);
            else
                clsMsg.AlertMessage("暫存失敗....", this.Page);
        }
        else
        {
            if (clsTransaction.InsertCertification_Wifi(strProjectID, strProductName, strModelNumber, strWirelessChipset, strProductOperatingSystem, strOSVersion, strHardwareVersion_Product, strFirmwareVersion_Product, strHardwareVersion_WiFi, strFirmwareVersion_WiFi, strProductNotes, strSearchable, strPublish, strPublish_Date, strDeviceType, strProductType, strPrimaryProductCategory, strSecondaryProductCategory, strLeastOneBand, strMandatoryProgram, strOptionalProgram, strSupportedSpatialStreams_Tx, strSupportedSpatialStreams_Rx, strAdditionalCapabilities, strSecurityType, strSpectrumAndRegulatoryFeatures, strNOptionalFeature, strACOptionalFeature) == true)
                clsMsg.AlertMessage("暫存成功....", this.Page);
            else
                clsMsg.AlertMessage("暫存失敗....", this.Page);
        }
    }
}
