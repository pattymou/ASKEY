using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

public partial class WebForm_AddPlan : System.Web.UI.Page
{
    //public static DataTable dt_new = new DataTable("dt_new");
    public static DataTable dt_new;
    public static DataTable dt;
    public static int intDefault;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            intDefault = 0;
            loadCategory(this.ddlCategory, ddlKind_T.Text, ddlCustomer.Text);
            loadP_Name(this.ddlP_Name, ddlCustomer.Text,"1");
            loadP_Name(this.ddlP_Name1, ddlCustomer1.Text, "0");
            loadCustomer(this.ddlCustomer,"1");
            loadCustomer(this.ddlCustomer1,"0");
            loadKind(this.ddlKind_T,"1");
            loadKind(this.ddlKind_T1,"0");
        }
    }

    #region loadCategory
    protected void loadCategory(DropDownList DDL, string strKind, string strCustomer)
    {
        clsDropDownList.ddlCategory(DDL, strKind, strCustomer);
    }
    #endregion 

    #region loadP_Name
    protected void loadP_Name(DropDownList DDL, string strCategory, string strKind1)
    {
        clsDropDownList.ddlP_Name(DDL, strCategory, strKind1);
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL,string strKind1)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, strKind1);
    }
    #endregion 

    #region loadKind
    protected void loadKind(DropDownList DDL,string strKind1)
    {
        clsDropDownList.ddlTestCaseKind(DDL, strKind1);
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        getTestCase();

        if (intDefault == 0)
        {
            setDatatable();
            intDefault = 1;
        }
        
    }

    private void setDatatable()
    {
        //DataTable dt_new = new DataTable("dt_new");
        dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        //DataColumn column2 = new DataColumn("RequirementID");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "RequirementID";
        //column1.DefaultValue = "0";
        //dt_new.Columns.Add(column2);

        DataColumn column2 = new DataColumn("Category");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Category";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("SubCategory");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "SubCategory";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Purpose");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Purpose";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("EnvironmentSetup");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "EnvironmentSetup";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("TestSteps");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "TestSteps";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("ExpectedResults");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ExpectedResults";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("TestResult");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "TestResult";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("BugTicketID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "BugTicketID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("RDComment");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "RDComment";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("Kind");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Kind";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("Customer");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Customer";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("ProductName");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ProductName";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column13);


    }

    private void getTestCase()
    {
        //int intKind = 0;

        //if (ddlKind.Text == "ALL")
        //    intKind = 0;
        //else if (ddlKind.Text == "Category")
        //    intKind = 1;
        //else if (ddlKind.Text == "Headline")
        //    intKind = 2;
        //else if (ddlKind.Text == "Engineer")
        //    intKind = 3;
        dt = clsData.UploadTestPool(ddlKind_T.Text, ddlCustomer.Text, ddlCategory.Text, txtSearch.Text, ddlP_Name.Text);
        //dt = clsData.UploadTestPlanQuery1(ddlKind_T.Text, ddlCustomer.Text, ddlCategory.Text, txtSearch.Text, ddlP_Name.Text, "N");
        //dt = clsData.UploadTestPlanQuery(intKind, txtSearch.Text,"",ddlKind_T.Text);
        this.gvwMain.DataSource = dt;
        this.gvwMain.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        getTestCase();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();
            DataTable dt2 = clsData.UploadTestPool1(4, strID, "", ddlKind_T.Text);
            DataRow dr = dt_new.NewRow();
            dr["ID"] = dt2.Rows[0]["ID"].ToString();
            //dr["RequirementID"] = dt2.Rows[0]["RequirementID"].ToString();
            dr["Category"] = dt2.Rows[0]["Category"].ToString();
            dr["SubCategory"] = dt2.Rows[0]["SubCategory"].ToString();
            dr["Purpose"] = dt2.Rows[0]["Purpose"].ToString();
            dr["EnvironmentSetup"] = dt2.Rows[0]["EnvironmentSetup"].ToString();
            dr["TestSteps"] = dt2.Rows[0]["TestSteps"].ToString();
            dr["ExpectedResults"] = dt2.Rows[0]["ExpectedResults"].ToString();
            dr["TestResult"] = dt2.Rows[0]["TestResult"].ToString();

            dr["BugTicketID"] = dt2.Rows[0]["BugTicketID"].ToString();
            dr["RDComment"] = dt2.Rows[0]["RDComment"].ToString();
            dr["Kind"] = dt2.Rows[0]["Kind"].ToString();
            dr["Customer"] = dt2.Rows[0]["Customer"].ToString();
            dr["ProductName"] = dt2.Rows[0]["ProductName"].ToString();


            dt_new.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ID"].ToString() == strID)
                    {
                        dt.Rows.Remove(dt.Rows[i]);
                        this.gvwMain.DataSource = dt;
                        this.gvwMain.DataBind();
                        //this.DataBind();
                    }
                }
            }


            //this.DataBind();
        }
            this.gvwMain1.DataSource = dt_new;
            this.gvwMain1.DataBind();
    }

    protected void gvwMain1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;

        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();
            if (dt_new.Rows.Count > 0)
            {
                for (int i = 0; i < dt_new.Rows.Count; i++)
                {
                    if (dt_new.Rows[i]["ID"].ToString() == strID)
                    {
                        dt_new.Rows.Remove(dt_new.Rows[i]);
                        //this.gvwMain1.DataSource = dt_new;
                        //this.gvwMain1.DataBind();
                        //this.DataBind();
                    }
                }
            }
        }
        this.gvwMain1.DataSource = dt_new;
        this.gvwMain1.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strName,strKind,strP_Name;
        string strNumber,strAdd;

        strAdd = "0";
        StringBuilder strSQL = new StringBuilder();
        DataTable dt1, dt2;

        strName = ddlCustomer1.Text;
        strKind = ddlKind_T1.Text;
        strP_Name = ddlP_Name1.Text;
        if ((ddlCustomer1.Text == "") || (ddlKind_T1.Text == "") || (ddlP_Name.Text == ""))
            clsMsg.AlertMessage("請輸入客戶、類別及機種名稱！", this.Page);
        else
        {
            int intCount;
            DataTable dt;

            dt = clsData.UploadTestPlanQuery1(ddlKind_T.Text, ddlCustomer.Text, "", "", ddlP_Name.Text, "");
            intCount = dt.Rows.Count;
            intCount = intCount + 1;
            strNumber = intCount.ToString();
            dt = clsData.UploadRequirementIDQuery(ddlKind_T.Text, ddlCustomer.Text, ddlP_Name.Text, "");

            if (dt_new.Rows.Count > 0)
            {
                for (int intI = 0; intI < dt_new.Rows.Count; intI++)
                {
                    clsTransaction.InsertExcelToSQL(strKind, "", dt_new.Rows[intI]["Category"].ToString(), dt_new.Rows[intI]["SubCategory"].ToString(), dt_new.Rows[intI]["Purpose"].ToString(), dt_new.Rows[intI]["EnvironmentSetup"].ToString(), dt_new.Rows[intI]["TestSteps"].ToString(), dt_new.Rows[intI]["ExpectedResults"].ToString(), dt_new.Rows[intI]["TestResult"].ToString(), dt_new.Rows[intI]["BugTicketID"].ToString(), dt_new.Rows[intI]["RDComment"].ToString(), strName, strP_Name, strNumber);
                    intCount = intCount + 1;
                    strNumber = intCount.ToString();


                }
                //clsMsg.AlertMessage("新增成功！", this.Page);
                strAdd = "1";
            }

            if (strAdd == "1")
            {
                strSQL.Append("select * from Requirement");

                dt1 = clsData.UploadTestPlanRequirement(strSQL);

                string strRequirementB, strTestPlanID, strRequirement_ID;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    strRequirement_ID = dt1.Rows[i]["Requirement_ID"].ToString();
                    strSQL.Length = 0;
                    //strRequirement_ID = dt1.Rows[0]["Kind"].ToString() + dt1.Rows[0]["Customer"].ToString() + dt1.Rows[0]["Product_Name"].ToString() + "-" + dt1.Rows[0]["Number"].ToString();
                    strSQL.Append("select * from TestPlan ");
                    strSQL.AppendFormat("WHERE Purpose like '%{0}%' {1} TestSteps like '%{2}%' {3} ExpectedResults like '%{4}%' and Kind = '{5}' and Customer = '{6}' and ProductName = '{7}' ", dt1.Rows[i]["PurposeKeyword"].ToString(), dt1.Rows[i]["Associate1"].ToString(), dt1.Rows[i]["TestStepsKeyword"].ToString(), dt1.Rows[i]["Associate2"].ToString(), dt1.Rows[i]["ExpectedResultsKeyword"].ToString(), ddlKind_T.Text, ddlCustomer.Text, ddlP_Name.Text);

                    dt2 = clsData.UploadTestPlanRequirement(strSQL);
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {

                        strRequirementB = dt2.Rows[j]["RequirementID_B"].ToString();

                        if (strRequirementB.IndexOf(strRequirement_ID) < 0)
                        {
                            strTestPlanID = dt2.Rows[j]["ID"].ToString();
                            if (strRequirementB != "")
                                strRequirementB = strRequirementB + "," + strRequirement_ID;
                            else
                                strRequirementB = strRequirement_ID;

                            if (clsTransaction.UpDateTestPlanRequirement(strTestPlanID, strRequirementB) == false)
                                clsMsg.AlertMessage("Requirement修改失敗！", this.Page);
                        }
                    }

                }
                clsMsg.AlertMessage("新增成功！", this.Page);
            }

            dt = null;
            dt_new = null;
            this.gvwMain1.DataSource = dt_new;
            this.gvwMain1.DataBind();
            this.gvwMain.DataSource = dt;
            this.gvwMain.DataBind();
            //txtName.Text = "";
        }
    }

    protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        this.gvwMain1.DataSource = dt_new;
        this.DataBind();
    }
    protected void linkAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/AddPlanItem.aspx?id=");
    }

    protected void gvwMain1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\n", "<br />");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[0].Width = 100;

            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\n", "<br />");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[1].Width = 130;

            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("\n", "<br />");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[2].Width = 250;

            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("\n", "<br />");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[3].Width = 300;

            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("\n", "<br />");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[4].Width = 300;

            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("\n", "<br />");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[5].Width = 300;

            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("\n", "<br />");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[6].Width = 150;

            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("\n", "<br />");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[7].Width = 100;

        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\n", "<br />");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[0].Width = 100;

            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\n", "<br />");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[1].Width = 130;

            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("\n", "<br />");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[2].Width = 250;

            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("\n", "<br />");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[3].Width = 300;

            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("\n", "<br />");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[4].Width = 300;

            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("\n", "<br />");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[5].Width = 300;

            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("\n", "<br />");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[6].Width = 150;

            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("\n", "<br />");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[7].Width = 100;


        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCategory(this.ddlCategory, ddlKind_T.Text, ddlCategory.Text);
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "1");
    }
    protected void ddlKind_T_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCategory(this.ddlCategory, ddlKind_T.Text, ddlCategory.Text);
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "1");
    }
    protected void ddlCustomer1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadP_Name(this.ddlP_Name1, ddlCustomer1.Text, "0");
    }
}
