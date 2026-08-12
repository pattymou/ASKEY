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

public partial class WebForm_SearchReservation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/ApparatusReservation.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadReservationQuery(txtSearch.Text,"0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadReservationQuery(txtSearch.Text,"0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion
}
