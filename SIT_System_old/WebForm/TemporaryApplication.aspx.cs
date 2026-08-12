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

public partial class WebForm_TemporaryApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvQuery();
            //GvQuery1();
        }
    }

    #region GvQuery
    private void GvQuery()
    {
        //DataTable dt1 = clsData.UploadNumber(Session["AppNo"].ToString());



        //DataTable dt = clsData.UploadApplicationIDQuery(Session["AppDep"].ToString(), dt1.Rows[0]["Name"].ToString().Trim());
        DataTable dt = clsData.UploadApplication_TemporarilyIDQuery_A();
        this.gvList.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //e.Row.Cells[2].Visible = false;

    }

    #region gvList_PageIndexChanging
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion
}
