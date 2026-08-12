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
using System.Text;

public partial class WebForm_AddTestPool : System.Web.UI.Page
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

            loadCustomer(this.ddlCustomer, "0");

            loadKind(this.ddlKind, "0");
            //strID = Request.QueryString["ID"];
            //strID = "1";
            if ((Request.QueryString["ID"].ToString() != "") && (Request.QueryString["ID"].ToString() != null))
                getTestPool();

            //clsDropDownList.ddlEmployees(ddlEngineer, "0");
            if ((Request.QueryString["ID"].ToString() != "") && (Request.QueryString["ID"].ToString() != null))
            {
                ddlCustomer.Enabled = false;
                ddlKind.Enabled = false;
                ddlP_Name.Enabled = false;
            }

            //loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");



        }
    }

    #region loadP_Name
    protected void loadP_Name(DropDownList DDL, string strCategory, string strKind1)
    {
        clsDropDownList.ddlP_Name(DDL, strCategory, strKind1);
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, strKind1);
    }
    #endregion

    #region loadKind
    protected void loadKind(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlTestCaseKind(DDL, strKind1);
    }
    #endregion

    private void getTestPool()
    {
        //DataTable dt = clsData.UploadTestPlanQuery(4, strID, "", "");
        DataTable dt = clsData.UploadTestPool1(4, Request.QueryString["ID"].ToString(), "", "");

        ddlCustomer.Text = dt.Rows[0]["Customer"].ToString();
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
        //txtRequirement.Text = dt.Rows[0]["RequirementID"].ToString();
        txtCategory.Text = dt.Rows[0]["Category"].ToString();
        txtSubCategory.Text = dt.Rows[0]["SubCategory"].ToString();
        txtEnvironmentSetup.Text = dt.Rows[0]["EnvironmentSetup"].ToString();
        CKEditorControl1.Text = dt.Rows[0]["Purpose"].ToString();
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
        ddlKind.Text = dt.Rows[0]["Kind"].ToString();
        ddlP_Name.Text = dt.Rows[0]["ProductName"].ToString();
        //txtRequirementB.Text = dt.Rows[0]["RequirementID_B"].ToString();
        //txtRequirement.Text = dt.Rows[0]["RequirementID"].ToString();
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strKind, strCategory, strSubCategory, strPurpose,strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name;
        int intCount;
        DataTable dt;
        string strNumber;

        strKind = ddlKind.Text;
        strCustomer = ddlCustomer.Text;
        //strRequirementID = txtRequirement.Text;
        strCategory = txtCategory.Text;
        strSubCategory = txtSubCategory.Text;
        strPurpose = CKEditorControl1.Text;
        strEnvironmentSetup = txtEnvironmentSetup.Text;
        strTestSteps = txtTestSteps.Text;
        strExpectedResults = txtExpectedResults.Text;
        strTestResult = txtTestResult.Text;
        //strEngineer = ddlEngineer.Text;
        strBugTicketID = txtBugTicketID.Text;
        strRDComment = txtRDComment.Text;
        strP_Name = ddlP_Name.Text;
        //strTestResult = txtTestResult.Text;
        //strPlanDate = txtDate.Text;
        //strPriority = txtPriority.Text;
        //strLocation = txtLocation.Text;
        //strTicketID = txtTicketID.Text;
        //strComment = txtComment.Text;
        //if (strRequirementID == "")
        //    strRequirementB = txtRequirementB.Text;
        //else
        //    strRequirementB = "";

        strPurpose = strPurpose.Replace("<p>", "");
        strPurpose = strPurpose.Replace("</p>", "");

        if ((Request.QueryString["ID"].ToString() != "") && (Request.QueryString["ID"].ToString() != null))
        {
            if (clsTransaction.UpDateTestPool(Request.QueryString["ID"].ToString(), strKind, strCategory, strSubCategory, strPurpose, strEnvironmentSetup, strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name) == true)
            {
                getTestPool();
                clsMsg.AlertMessage("修改成功！", this.Page);
            }

        }
        else
        {
            //StringBuilder strSQL = new StringBuilder();
            //DataTable dt1, dt2;

            dt = clsData.UploadRequirementIDQuery(ddlKind.Text, ddlCustomer.Text, ddlP_Name.Text, "");
            intCount = dt.Rows.Count;
            intCount = intCount + 1;
            strNumber = intCount.ToString();

            //strSQL.Append("select * from Requirement");

            //dt1 = clsData.UploadTestPlanRequirement(strSQL);
            //string strRequirement_ID;
            //strRequirementB = "";
            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    strRequirement_ID = dt1.Rows[i]["Requirement_ID"].ToString();
            //    //strRequirement_ID = dt1.Rows[0]["Kind"].ToString() + dt1.Rows[0]["Customer"].ToString() + dt1.Rows[0]["Product_Name"].ToString() + "-" + dt1.Rows[0]["Number"].ToString();
            //    if (strRequirementB != "")
            //        strRequirementB = strRequirementB + "," + strRequirement_ID;
            //    else
            //        strRequirementB = strRequirement_ID;

            //}

            if (clsTransaction.InsertTestPool(strKind, strCategory, strSubCategory, strPurpose, strEnvironmentSetup,strTestSteps, strExpectedResults, strTestResult, strBugTicketID, strRDComment, strCustomer, strP_Name, strNumber) == true)
            {
                clsMsg.AlertMessage("新增成功！", this.Page);
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/TestPoolView.aspx");
    }


    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
    }
}
