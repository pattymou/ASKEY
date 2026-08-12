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

public partial class WebForm_ExplanationView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppNo"] == null)
        {
            Response.Redirect("~/ApplicationDefault.aspx");


        }
        if (!IsPostBack)
        {
            DataTable dt = clsData.UploadExplanationViewFile();
            this.gvwMain.DataSource = dt;
            this.DataBind();
        }
    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {


        for (int intI = 0; intI < 1; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }

    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;

    }
    #endregion
}
