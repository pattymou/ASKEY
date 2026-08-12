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

public partial class WebForm_TestPlanView : System.Web.UI.Page
{
    //public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {

            //loadCustomer(this.ddlCustomer, "0");

            //loadKind(this.ddlKind, "0");
            //strID = Request.QueryString["ID"];
            //strID = "1";
            if ((Request.QueryString["ID"] != "") && (Request.QueryString["ID"] != null))
                getTestPlan();

            //clsDropDownList.ddlEmployees(ddlEngineer, "0");
            //if ((strID != "") && (strID != null))
            //{
            //    ddlCustomer.Enabled = false;
            //    ddlKind.Enabled = false;
            //    ddlP_Name.Enabled = false;
            //}

            //loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");



        }
    }

    private void getTestPlan()
    {
        DataTable dt = clsData.UploadTestPlanQuery(4, Request.QueryString["ID"], "", "");

        lblCustomer.Text = dt.Rows[0]["Customer"].ToString();
        //loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
        lblRequirementID.Text = dt.Rows[0]["RequirementID"].ToString();
        txtCategory.Text = dt.Rows[0]["Category"].ToString();
        txtSubCategory.Text = dt.Rows[0]["SubCategory"].ToString();
        txtPurpose.Text = dt.Rows[0]["Purpose"].ToString();
        txtEnvironmentSetup.Text = dt.Rows[0]["EnvironmentSetup"].ToString();
        txtTestSteps.Text = dt.Rows[0]["TestSteps"].ToString();
        txtExpectedResults.Text = dt.Rows[0]["ExpectedResults"].ToString();
        txtTestResult.Text = dt.Rows[0]["TestResult"].ToString();
        //ddlEngineer.Text = dt.Rows[0]["Engineer"].ToString();

        txtBugTicketID.Text = dt.Rows[0]["BugTicketID"].ToString();
        txtRDComment.Text = dt.Rows[0]["RDComment"].ToString();
        //txtTestResult.Text = dt.Rows[0]["TestResult"].ToString();
        //txtDate.Text = dt.Rows[0]["PlanDate"].ToString();
        //txtPriority.Text = dt.Rows[0]["Priority"].ToString();
        //txtLocation.Text = dt.Rows[0]["Location"].ToString();
        //txtTicketID.Text = dt.Rows[0]["TicketID"].ToString();
        //txtComment.Text = dt.Rows[0]["Comment"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        lblP_Name.Text = dt.Rows[0]["ProductName"].ToString();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>window.close();</" + "script>");
    }
}
