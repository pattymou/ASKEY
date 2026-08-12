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

public partial class WebForm_ApplicationDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppNo"] == null)
            Response.Redirect("~/ApplicationDefault.aspx");

        if (!IsPostBack)
        {
            getProject();
            //getProjectTask();
        }
    }

    private void getProject()
    {
        string strPID, strDate1;
        DateTime dt1;

        strPID = Request.QueryString["PID"];
        //strCID = Request.QueryString["CID"];

        DataTable dt = clsData.UploadProjectQuery(strPID, "Project");

        lblID.Text = dt.Rows[0]["Name"].ToString();


        //Session["ProjectName"] = dt.Rows[0]["Name"].ToString();
        //Session["ProjectKind"] = dt.Rows[0]["Kind"].ToString();

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
        //lblLocation.Text = dt.Rows[0]["Accepted_Team"].ToString();
        //lblRelated.Text = dt.Rows[0]["Related"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["Sample_Ready_Date"].ToString());
        strDate1 = dt1.ToString("yyyy/MM/dd");
        if (strDate1 == "1900/01/01")
            lblReady.Text = "";
        else
            lblReady.Text = strDate1;

        lblUtility.Text = dt.Rows[0]["Utility_Version"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["Start_Date"].ToString());
        strDate1 = dt1.ToString("yyyy/MM/dd");
        if (strDate1 == "1900/01/01")
            lblStart.Text = "";
        else
            lblStart.Text = strDate1;

        dt1 = Convert.ToDateTime(dt.Rows[0]["End_Date"].ToString());
        strDate1 = dt1.ToString("yyyy/MM/dd");
        if (strDate1 == "1900/01/01")
            lblExpect.Text = "";
        else
            lblExpect.Text = strDate1;

        //txtNote.Text = dt.Rows[0]["Explain"].ToString();
        //lblEngineer.Text = dt.Rows[0]["Assign"].ToString();
        //lblProgress.Text = dt.Rows[0]["Progress"].ToString();

        dt = clsData.UploadProjectCase(strPID);
        string strCase;

        if (dt.Rows.Count > 0)
        {
            strCase = dt.Rows[0]["TestCase"].ToString();
            string[] strCase1 = strCase.Split(',');
            string strCase3 = "";

            foreach (string i in strCase1)
            {
                if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                {
                    string[] strCase2 = i.Split('-');
                    DataTable dt3 = clsData.UploadTestCaseName(strCase2[2], strCase2[0], strCase2[1]);


                    if (strCase3 == "")
                        strCase3 = dt3.Rows[0]["Item"].ToString() + " \r\n";
                    else
                        strCase3 = strCase3 + dt3.Rows[0]["Item"].ToString() + " \r\n";

                }
            }

            txtTestCase.Text = strCase3;
        }

    }
}
