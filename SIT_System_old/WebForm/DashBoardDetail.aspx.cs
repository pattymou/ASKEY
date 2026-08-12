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

public partial class WebForm_DashBoardDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            getProject();
            getProjectTask();
        }
    }

    private void getProject()
    {
        string strPID, strCID, strDate1;
        DateTime dt1;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

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
        lblRelated.Text = dt.Rows[0]["Related"].ToString();
        lblDepartment2.Text = dt.Rows[0]["A_Department2"].ToString();

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

        txtNote.Text = dt.Rows[0]["Explain"].ToString();
        lblEngineer.Text = dt.Rows[0]["Assign"].ToString();
        lblProgress.Text = dt.Rows[0]["Progress"].ToString();

    }

    private void getProjectTask()
    {
        string strDate;
        DateTime dTime;
        string strPID, strCID;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        //if (Session["CaseName"].ToString().IndexOf("BQB", 0) != -1)
        //    Session["CaseName"] = "BQB Review(2.0 Version & EDR Review)";

        DataTable dt = clsData.UploadProjectTask_DB(strPID, strCID);

        lblCaseID.Text = strCID;
        lblCaseName.Text = dt.Rows[0]["Name"].ToString();
        lblCID.Text = dt.Rows[0]["Kind"].ToString() + " - " + dt.Rows[0]["Name"].ToString();

        if (dt.Rows.Count != 0)
        {
            lblAssign.Text = dt.Rows[0]["assign"].ToString();
            dTime = Convert.ToDateTime(dt.Rows[0]["start_date1"].ToString());
            strDate = dTime.ToString("yyyy/MM/dd");

            if (strDate != "1900/01/01")
                lblStartdate.Text = strDate;



            dTime = Convert.ToDateTime(dt.Rows[0]["end_date1"].ToString());
            strDate = dTime.ToString("yyyy/MM/dd");
            if (strDate != "1900/01/01")
                lblEnddate.Text = strDate;

            lblResult.Text = dt.Rows[0]["result"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();
            lblCaseProgress.Text = dt.Rows[0]["Progress"].ToString();
            txtCaseNote.Text = dt.Rows[0]["explain_case"].ToString();

        }



    }

    protected void lbtnModify_Click(object sender, EventArgs e)
    {
        string strPID, strCID;

        strPID = Request.QueryString["PID"];
        strCID = Request.QueryString["CID"];

        Response.Redirect("~/WebForm/ModifyDashBoard.aspx?PID=" + strPID + "&CID=" + strCID);
    }
}
