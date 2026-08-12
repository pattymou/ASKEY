using System;
using System.IO;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WebForm_AddRequirement : System.Web.UI.Page
{
    public static string strID;

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
            loadEmployees(this.ddlOwner);

            strID = Request.QueryString["ID"];

            if ((strID != "") && (strID != null))
                getRequirement();
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

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL, "0");
    }
    #endregion

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
    }
    protected void ddlP_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intCount;
        DataTable dt;

        dt = clsData.UploadRequirementIDQuery(ddlKind.Text, ddlCustomer.Text, ddlP_Name.Text,"");
        intCount = dt.Rows.Count;
        lblID.Text = (intCount + 1).ToString();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strRequirement_ID, strDoc_Ver, strRequirement_Date, strDescription, strRequirement_Table, strFigure, strOwner, strPurposeKeyword, strTestStepsKeyword, strExpectedResultsKeyword, strAssociate1, strAssociate2, strKind, strCustomer, strProduct_Name, strNumber, strCheck,strReview;
        StringBuilder strSQL = new StringBuilder();
        DataTable dt;

        strRequirement_ID = ddlKind.Text + ddlCustomer.Text + ddlP_Name.Text + "-" + lblID.Text;
        strDoc_Ver = txtVer.Text;
        strRequirement_Date = txtDate.Text;
        strDescription = txtDescription.Text;
        strRequirement_Table = CKEditorControl1.Text;
        strFigure = CKEditorControl2.Text;
        strOwner = ddlOwner.Text;
        strPurposeKeyword = txtPurposeKeyword.Text;
        strTestStepsKeyword = txtTestStepsKeyword.Text;
        strExpectedResultsKeyword = txtExpectedKeyword.Text;
        strAssociate1 = ddlAssociate1.Text;
        strAssociate2 = ddlAssociate2.Text;
        strKind = ddlKind.Text;
        strCustomer = ddlCustomer.Text;
        strProduct_Name = ddlP_Name.Text;
        strNumber = lblID.Text;
        if (radioRequirementY.Checked == true)
            strCheck = "Y";
        else
            strCheck = "N";

        if (radioReviewY.Checked == true)
            strReview = "Y";
        else
            strReview = "N";

        string strRequirementB, strTestPlanID;

        if (strReview == "Y")
        {
            strSQL.Append("select * from TestPlan ");
            strSQL.AppendFormat("WHERE Purpose like '%{0}%' {1} TestSteps like '%{2}%' {3} ExpectedResults like '%{4}%' ", txtPurposeKeyword.Text, ddlAssociate1.Text, txtTestStepsKeyword.Text, ddlAssociate2.Text, txtExpectedKeyword.Text);

            dt = clsData.UploadTestPlanRequirement(strSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strRequirementB = dt.Rows[0]["RequirementID_B"].ToString();
                strTestPlanID = dt.Rows[0]["ID"].ToString();
                if (strRequirementB != "")
                    strRequirementB = strRequirementB + "," + strRequirement_ID;
                else
                    strRequirementB = strRequirement_ID;

                    if (clsTransaction.UpDateTestPlanRequirement(strTestPlanID,strRequirementB) == false)
                        clsMsg.AlertMessage("修改失敗！", this.Page);

            }
        }

        if ((strID != "") && (strID != null))
        {
            if (clsTransaction.UpDateRequirement(strID,strRequirement_ID, strDoc_Ver, strRequirement_Date, strDescription, strRequirement_Table, strFigure, strOwner, strPurposeKeyword, strTestStepsKeyword, strExpectedResultsKeyword, strAssociate1, strAssociate2, strKind, strCustomer, strProduct_Name, strNumber, strCheck, strReview) == true)
                clsMsg.AlertMessage("修改成功！", this.Page);
            else
                clsMsg.AlertMessage("修改失敗！", this.Page);
        }
        else
        {
            if (clsTransaction.InsertRequirement(strRequirement_ID, strDoc_Ver, strRequirement_Date, strDescription, strRequirement_Table, strFigure, strOwner, strPurposeKeyword, strTestStepsKeyword, strExpectedResultsKeyword, strAssociate1, strAssociate2, strKind, strCustomer, strProduct_Name, strNumber, strCheck, strReview) == true)
            {
                clsMsg.AlertMessage("新增成功！", this.Page);
                Server.Transfer("~/WebForm/RequirementView.aspx");
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
            setEmpty();
        }



    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/RequirementView.aspx");

    }

    private void setEmpty()
    {
        ddlKind.Text = "";
        ddlCustomer.Text = "";
        ddlP_Name.Text = "";
        lblID.Text = "";
        txtVer.Text = "";
        txtDate.Text = "";
        txtDescription.Text = "";
        CKEditorControl1.Text = "";
        CKEditorControl2.Text = "";
        ddlOwner.Text = "";
        txtTestStepsKeyword.Text = "";
        txtExpectedKeyword.Text = "";
        txtPurposeKeyword.Text = "";

    }

    private void getRequirement()
    {
        DataTable dt = clsData.UploadRequirementQuery(strID);

        ddlKind.Text = dt.Rows[0]["Kind"].ToString();
        ddlCustomer.Text = dt.Rows[0]["Customer"].ToString();
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "0");
        ddlP_Name.Text = dt.Rows[0]["Product_Name"].ToString();

        lblID.Text = dt.Rows[0]["Number"].ToString();
        txtVer.Text = dt.Rows[0]["Doc_Ver"].ToString();
        txtDate.Text = dt.Rows[0]["Requirement_Date"].ToString();
        txtDescription.Text = dt.Rows[0]["Description"].ToString();
        CKEditorControl1.Text = dt.Rows[0]["Requirement_Table"].ToString();
        CKEditorControl2.Text = dt.Rows[0]["Figure"].ToString();
        ddlOwner.Text = dt.Rows[0]["Owner"].ToString();
        txtPurposeKeyword.Text = dt.Rows[0]["PurposeKeyword"].ToString();
        txtTestStepsKeyword.Text = dt.Rows[0]["TestStepsKeyword"].ToString();
        txtExpectedKeyword.Text = dt.Rows[0]["ExpectedResultsKeyword"].ToString();
        ddlAssociate1.Text = dt.Rows[0]["Associate1"].ToString();
        ddlAssociate2.Text = dt.Rows[0]["Associate2"].ToString();

        if (dt.Rows[0]["Check_Requirement"].ToString() == "Y")
            radioRequirementY.Checked = true;
        else
            radioRequirementN.Checked = false;

        if (dt.Rows[0]["Review"].ToString() == "Y")
            radioReviewY.Checked = true;
        else
            radioReviewN.Checked = false;

    }
}
