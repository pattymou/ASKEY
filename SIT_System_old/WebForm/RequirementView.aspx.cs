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


public partial class WebForm_RequirementView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadP_Name(this.ddlP_Name, ddlCustomer.Text, "1");
            loadCustomer(this.ddlCustomer, "1");
            loadKind(this.ddlKind, "1");
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
    protected void linkAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddRequirement.aspx");
    }

    protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadCategory(this.ddlCategory, ddlKindT.Text, ddlCategory.Text);
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadCategory(this.ddlCategory, ddlKindT.Text, ddlCategory.Text);
        loadP_Name(this.ddlP_Name, ddlCustomer.Text, "1");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strKind, strCustomer, strP_Name;

        //if (ddlKind.Text == "ALL")
        //    strKind = "";
        //else
        //    strKind = ddlKind.Text;

        //if (ddlCustomer.Text == "ALL")
        //    strCustomer = "";
        //else
        //    strCustomer = ddlCustomer.Text;

        //if (ddlP_Name.Text == "ALL")
        //    strP_Name = "";
        //else
        //    strP_Name = ddlP_Name.Text;

        getRequirementView();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        getRequirementView();
    }
    #endregion

    private void getRequirementView()
    {
        string strReview;

        if (chkReview.Checked == true)
            strReview = "Y";
        else
            strReview = "N";

        DataTable dt = clsData.UploadRequirementIDQuery(ddlKind.Text, ddlCustomer.Text, ddlP_Name.Text, strReview);

        gvwMain.DataSource = dt;
        gvwMain.DataBind();
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();

            Response.Write("<script>window.open('AddRequirement.aspx?ID=" + strID + "');</script>");

        }

        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();

            clsTransaction.DelRequirement(strID);

            getRequirementView();

        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\n", "<br />");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[0].Width = 170;

            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\n", "<br />");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[1].Width = 100;

            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("\n", "<br />");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;p&gt;", "");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&lt;/p&gt;", "");
            e.Row.Cells[2].Width = 100;

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

            //e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("\n", "<br />");
            //e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;p&gt;", "");
            //e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&lt;/p&gt;", "");
            //e.Row.Cells[7].Width = 150;

            //e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("\n", "<br />");
            //e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&lt;p&gt;", "");
            //e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&lt;/p&gt;", "");
            //e.Row.Cells[8].Width = 100;
        }
    }
}
