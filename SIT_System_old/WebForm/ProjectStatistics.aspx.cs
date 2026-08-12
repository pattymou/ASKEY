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

public partial class WebForm_ProjectStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {

            loadDepartment(this.ddlDepartment);
            

            int iYear = System.DateTime.Now.Year;
            int iMonth = System.DateTime.Now.Month;

            txtYearE.Text = iYear.ToString();
            txtYearS.Text = iYear.ToString();
            ddlMonthS.Text = String.Format("{0:00}", iMonth);
            ddlMonthE.Text = String.Format("{0:00}", iMonth);
        }
    }

    #region loadTeam
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlDepartment(DDL, "1", "1");
    }
    #endregion

    #region loadTeam
    protected void loadProject(DropDownList DDL)
    {
        clsDropDownList.ddlStatisticsProject(DDL, "1", ddlDepartment.Text);
    }
    #endregion

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        Query();
    }
    #endregion

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {


        for (int intI = 0; intI < 2; intI++)
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

    #region gvwMain1_PageIndexChanging (換頁)
    protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        Query1();
    }
    #endregion

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProject(this.ddlProject);
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        Query();
        Query1();

    }

    private void Query()
    {
        string strTeam, strDepartment, strProject, strKind;

        string strStart, strEnd;

        if ((Convert.ToInt32(ddlMonthE.Text) < 9))
        {
            if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
            {
                if (ddlMonthE.Text == "02")
                {
                    if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                }
                else
                {
                    if (ddlMonthE.Text == "08")
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                }
            }
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        else
        {
            if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        strStart = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";

        if (chkDate.Checked == false)
        {
            strStart = "";
            strEnd = "";
        }

        if (ddlKind.Text == "ALL")
            strTeam = "";
        else
            strTeam = ddlKind.Text;

        if (ddlDepartment.Text == "ALL")
            strDepartment = "";
        else
            strDepartment = ddlDepartment.Text;

        if (ddlProject.Text == "ALL")
            strProject = "";
        else
            strProject = ddlProject.Text;

        if (ddlPKind.Text == "ALL")
            strKind = "";
        else
            strKind = ddlPKind.Text;

        DataTable dt = clsData.UploadProjectStatistics(strDepartment, strStart, strEnd, strTeam, strProject, strKind);

        gvwMain.DataSource = dt;
        gvwMain.DataBind();

    }

    private void Query1()
    {
        string strTeam, strDepartment, strProject, strKind;

        string strStart, strEnd;

        if ((Convert.ToInt32(ddlMonthE.Text) < 9))
        {
            if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
            {
                if (ddlMonthE.Text == "02")
                {
                    if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                }
                else
                {
                    if (ddlMonthE.Text == "08")
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                    else
                        strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                }
            }
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        else
        {
            if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
            else
                strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
        }
        strStart = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";

        if (chkDate.Checked == false)
        {
            strStart = "";
            strEnd = "";
        }

        if (ddlKind.Text == "ALL")
            strTeam = "";
        else
            strTeam = ddlKind.Text;

        if (ddlDepartment.Text == "ALL")
            strDepartment = "";
        else
            strDepartment = ddlDepartment.Text;

        if (ddlProject.Text == "ALL")
            strProject = "";
        else
            strProject = ddlProject.Text;

        if (ddlPKind.Text == "ALL")
            strKind = "";
        else
            strKind = ddlPKind.Text;

        DataTable dt = clsData.UploadProjectStatistics1(strDepartment, strStart, strEnd, strTeam, strProject, strKind);

        gvwMain1.DataSource = dt;
        gvwMain1.DataBind();
    }
}
