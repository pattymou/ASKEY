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

public partial class WebForm_SearchSample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadSample_N(txtSearch.Text, "0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddSample1.aspx");
    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strPath, strPath1;
        string strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[0].FindControl("lblGVSeq")).Text;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        if (clsTransaction.DelSample1(strName) == true)
        {
            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        }

        DataTable dt = clsData.UploadSample_N(txtSearch.Text, "0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadSample_N(txtSearch.Text, "0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion
}
