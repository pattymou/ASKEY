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

public partial class WebForm_SearchPlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadKind(this.ddlKind, "0");
            loadCustomer(this.ddlCustomer, "0");
        }
    }

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if ((ddlKind.Text=="") || (ddlCustomer.Text==""))
            clsMsg.AlertMessage("請選擇客戶及類別！", this.Page);
        else
        {
        DataTable dt = clsData.UploadTestPlanNameQuery(ddlKind.Text,ddlCustomer.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadTestPlanNameQuery(ddlKind.Text, ddlCustomer.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].Text = ddlKind.Text;
        //    e.Row.Cells[0].Text = ddlCustomer.Text;

        //}
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strP_Name;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strP_Name = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();

            //Response.Redirect("~/WebForm/AddPlanItem.aspx?ID=" + strID);
            //Response.Redirect("window.open('~/WebForm/AddPlanItem.aspx?ID='" + strID);
            //string strUrl;

            //strUrl = "AddPlanItem.aspx?ID=" + strID;
            Response.Write("<script>window.open('PlanView.aspx?ID=" + strP_Name + "&Kind=" + ddlKind.Text + "&Customer=" + ddlCustomer.Text + "');</script>");

        }
    }
}
