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

public partial class WebForm_GoodsBorrowDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GvQuery();
    }

    private void GvQuery()
    {
        string strID;

        strID = Request.QueryString["ID"];
        DataTable dt = clsData.UploadGoodsReservationQuery(strID, "2");
        DataTable dt1 = clsData.UploadGoodsQuery(strID, "1", "");


        lblName.Text = dt1.Rows[0]["Name_En"].ToString() + "-" + dt1.Rows[0]["Name_CH"].ToString();

        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

        GvQuery();
    }
    #endregion
}
