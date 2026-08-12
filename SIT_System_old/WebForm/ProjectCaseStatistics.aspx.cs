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

public partial class WebForm_ProjectCaseStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {

            //loadDepartment(this.ddlDepartment);


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

    //#region loadTeam
    //protected void loadProject(DropDownList DDL)
    //{
    //    clsDropDownList.ddlStatisticsProject(DDL, "1", ddlDepartment.Text);
    //}
    //#endregion

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

    //#region gvwMain1_PageIndexChanging (換頁)
    //protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    ((GridView)sender).PageIndex = e.NewPageIndex;
    //    ((GridView)sender).EditIndex = -1;
    //    ((GridView)sender).SelectedIndex = -1;
    //    Query1();
    //}
    //#endregion

    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    loadProject(this.ddlProject);
    //}

    protected void butOK_Click(object sender, EventArgs e)
    {
        Query();
        //Query1();

    }

    private void Query()
    {
        string strTeam, strTeam2;

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

        //if (ddlDepartment.Text == "ALL")
        //    strDepartment = "";
        //else
        //    strDepartment = ddlDepartment.Text;

        //if (ddlProject.Text == "ALL")
        //    strProject = "";
        //else
        //    strProject = ddlProject.Text;
        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("Kind");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "Kind";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Name");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Name";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Item");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Item";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Total");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Total";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataTable dt = clsData.UploadProjectCaseStatistics(strStart, strEnd, strTeam);
        for (int intJ = 0; intJ < dt.Rows.Count; intJ++)
        {
            if (strTeam == "台北")
                strTeam2 = "DA40";
            else if (strTeam == "吳江")
                strTeam2 = "DA40-WJ";
            else
                strTeam2 = "";
            DataTable dt1;
            DataRow dr = dt_new.NewRow();
            dt1 = clsData.UploadProjectCaseKind(dt.Rows[intJ]["Kind"].ToString(), strTeam2);
            if (dt1.Rows.Count > 0)
            {
                dr["Kind"] = dt1.Rows[0]["Kind"].ToString();
                dr["Name"] = dt1.Rows[0]["Name"].ToString();
                dr["Item"] = dt.Rows[intJ]["name"].ToString();
                dr["Total"] = dt.Rows[intJ]["Total"].ToString();

                dt_new.Rows.Add(dr);
            }
        }
        gvwMain.DataSource = dt_new;
        gvwMain.DataBind();
    }


}
