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

public partial class WebForm_SearchApplication_A : System.Web.UI.Page
{

    public static string strNumber1;
    public static string strName1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
    }

    #region gvList_PageIndexChanging
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery(strNumber1, strName1);
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[4].FindControl("lblGVSeq")).Text.Trim();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        strNumber1 = txtNumber.Text.Trim();
        strName1 = txtName.Text.Trim();
        GvQuery(strNumber1, strName1);
    }

    #region GvQuery
    private void GvQuery(string strNumber, string strName)
    {

        DataTable dt = clsData.UploadApplicationIDQuery(strNumber, strName);
        this.gvList.DataSource = dt;
        this.DataBind();
    }
    #endregion
}
