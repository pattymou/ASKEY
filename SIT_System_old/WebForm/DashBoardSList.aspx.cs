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

public partial class WebForm_DashBoardSList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //Session["CaseID"] = Request.QueryString["NPI"];
            lblName.Text = Session["DBS"].ToString();
            lblCustomer.Text = Session["DBSC"].ToString();
            //lblDepartment.Text = Session["DBSD"].ToString();
            //lblNPI.Text = Request.QueryString["N"];
            lblTestCase.Text = Request.QueryString["F"] + " - " + Request.QueryString["I"];

            string strKind;
            DataTable dt;
            strKind = Request.QueryString["Kind"];

            if (strKind =="DQA")
                dt = clsData.UploadDashBoardSummaryDetail1(Session["DBS"].ToString(), Request.QueryString["N"], Request.QueryString["F"], Request.QueryString["I"], Session["DBSC"].ToString());
            else
                dt = clsData.UploadDashBoardSummaryDetail(Session["DBS"].ToString(), Request.QueryString["N"], Request.QueryString["F"], Request.QueryString["I"], Session["DBSC"].ToString());

            gvwMain.DataSource = dt;
            gvwMain.DataBind();
        }
    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime dt1;
            string strDate, strDate1;
            strDate = e.Row.Cells[3].Text;
            if (strDate != "")
            {
                dt1 = Convert.ToDateTime(strDate);
                strDate1 = dt1.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    e.Row.Cells[3].Text = "";
                else
                    e.Row.Cells[3].Text = strDate1;
            }
            
        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadDashBoardSummaryDetail(Session["DBS"].ToString(), Request.QueryString["N"], Request.QueryString["F"], Request.QueryString["I"], Session["DBSC"].ToString());
        gvwMain.DataSource = dt;
        gvwMain.DataBind();
    }
    #endregion
}
