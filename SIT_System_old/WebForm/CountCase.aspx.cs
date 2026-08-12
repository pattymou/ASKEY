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

public partial class WebForm_CountCase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;
            loadAssign(this.ddlAssign);
            loadDepartment(this.ddlDepartment);
            txtYear.Text = dt.Year.ToString();
            txtYear1.Text = dt.Year.ToString();
        }
    }

    #region loadAssign
    protected void loadAssign(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL,"1");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3,"1");
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GvQuery();
    }

    private void GvQuery()
    {
        string strDate, strDate1;

        strDate = txtYear.Text + ddlMonth.Text + "01";

        if ((Convert.ToInt32(ddlMonth1.Text) < 9))
        {
            if ((Convert.ToInt32(ddlMonth1.Text) % 2) == 0)
            {
                if (ddlMonth1.Text == "2")
                {
                    if ((Convert.ToInt32(txtYear1.Text) / 4) == 0)
                        strDate1 = txtYear1.Text + ddlMonth1.Text + "29";
                    else
                        strDate1 = txtYear1.Text + ddlMonth1.Text + "28";
                }
                else
                {
                    if (ddlMonth1.Text == "8")
                        strDate1 = txtYear1.Text + ddlMonth1.Text + "31";
                    else
                        strDate1 = txtYear1.Text + ddlMonth1.Text + "30";
                }
            }
            else
                strDate1 = txtYear1.Text + ddlMonth1.Text + "31";
        }
        else
        {
            if ((ddlMonth1.Text == "9") || (ddlMonth1.Text == "11"))
                strDate1 = txtYear1.Text + ddlMonth1.Text + "30";
            else
                strDate1 = txtYear1.Text + ddlMonth1.Text + "31";
        }

        DataTable dt = clsData.UploadProjectCount(strDate, strDate1, ddlDepartment.Text, ddlAssign.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();

        lblCount.Text = dt.Rows.Count.ToString();
    }
}
