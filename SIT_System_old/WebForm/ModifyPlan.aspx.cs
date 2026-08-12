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

public partial class WebForm_ModifyPlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {

            //clsDropDownList.ddlTestPlan(this.ddlPlanName);
            loadCustomer(this.ddlCustomer, "1");
            loadKind(this.ddlKindT, "1");
            loadCategory(this.ddlCategory, ddlKindT.Text, ddlCategory.Text);
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

    #region loadCategory
    protected void loadCategory(DropDownList DDL, string strKind, string strCategory)
    {
        clsDropDownList.ddlCategory(DDL, strKind, strCategory);
    }
    #endregion

    #region loadKind
    protected void loadKind(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlTestCaseKind(DDL, strKind1);
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getTestPlan();
    }

    private void getTestPlan()
    {
        //int intKind;

        //if (ddlKind.Text == "Category")
        //    intKind = 1;
        //else if (ddlKind.Text == "Headline")
        //    intKind = 2;
        //else if (ddlKind.Text == "Engineer")
        //    intKind = 3;
        //else
        //    intKind = 0;
        string strChkReq;

        if (chkRequirement.Checked == true)
            strChkReq = "Y";
        else
            strChkReq = "N";
        DataTable dt = clsData.UploadTestPlanQuery1(ddlKindT.Text, ddlCustomer.Text, ddlCategory.Text, txtSearch.Text, ddlP_Name.Text, strChkReq);
        //DataTable dt = clsData.UploadTestPlanQuery(intKind, txtSearch.Text, ddlPlanName.Text, ddlKind.Text);
        gvwMain.DataSource = dt;
        gvwMain.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        getTestPlan();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();

            //Response.Redirect("~/WebForm/AddPlanItem.aspx?ID=" + strID);
            //Response.Redirect("window.open('~/WebForm/AddPlanItem.aspx?ID='" + strID);
            //string strUrl;

            //strUrl = "AddPlanItem.aspx?ID=" + strID;
            Response.Write("<script>window.open('AddPlanItem.aspx?ID=" + strID + "');</script>");
            
        }

        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();

            clsTransaction.DelTestPlan(strID);
            getTestPlan();

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
            e.Row.Cells[1].Width = 100;

            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("\n", "<br />");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[2].Width = 130;

            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("\n", "<br />");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[3].Width = 250;

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
            e.Row.Cells[6].Width = 100;

            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("\n", "<br />");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[7].Width = 150;

            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("\n", "<br />");
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[8].Width = 100;
        }
    }
    protected void ddlKindT_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCategory(this.ddlCategory, ddlKindT.Text, ddlCategory.Text);
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCategory(this.ddlCategory, ddlKindT.Text, ddlCategory.Text);
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "1");
    }
}
