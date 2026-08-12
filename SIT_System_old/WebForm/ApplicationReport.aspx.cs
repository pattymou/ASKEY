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

public partial class WebForm_ApplicationReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppNo"] == null)
            Response.Redirect("~/ApplicationDefault.aspx");

        if (!IsPostBack)
        {
            String strYear = DateTime.Now.Year.ToString();

            txtYearE.Text = strYear;
            txtYearS.Text = strYear;
            rdoDate.Checked = true;
            loadDepartment(this.ddlDepartmentP);
            rdoAll.Text = "部門";
            //if ((Session["AppDep"].ToString() == "Q600") || (Session["AppDep"].ToString() == "DA40-SIT") || (Session["AppDep"].ToString() == "DA40"))
            //{
            //    loadDepartment(this.ddlDepartmentP);
            //    rdoAll.Text = "部門";
            //}
            //else
            //{
            //    loadDepartmentNumber(ddlDepartmentP, Session["AppDep"].ToString());
            //    rdoAll.Text = "部門人員";
            //}
        }
    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    #region loadCustomer
    protected void loadDepartmentNumber(DropDownList DDL,string strDepartment)
    {
        clsDropDownList.ddlDepartmentNumber(DDL, "1", strDepartment);
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        //Session["AppDep"]
        GvQuery();
    }

    private void GvQuery()
    {
        string strStart = "";
        string strEnd = "";

        if (rdoDate.Checked == true)
        {
            if ((txtYearE.Text.Trim() == "") || (txtYearS.Text.Trim() == ""))
            {
                clsMsg.AlertMessage("請輸入年份", this.Page);
            }
            else
            {
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

            }
        }

        string strSearchID, strSearchName;
        strSearchID = "";
        strSearchName = "";
        string strDepP = "";
        DataTable dt;

        if (rdoApplicationID.Checked == true)
            strSearchID = txtApplication_ID.Text.Trim();
        if (rdoApplicationName.Checked == true)
            strSearchName = txtApplicationi_Name.Text.Trim();
        if (rdoAll.Checked == true)
            strDepP = ddlDepartmentP.Text;

        //if ((Session["AppDep"].ToString() == "Q600") || (Session["AppDep"].ToString() == "DA40-SIT") || (Session["AppDep"].ToString() == "DA40"))
            dt = clsData.UploadApplicationReport1(strStart, strEnd, Session["AppDep"].ToString(), strSearchID, strSearchName, strDepP);
        //else
        //    dt = clsData.UploadApplicationReport(strStart, strEnd, Session["AppDep"].ToString(), strSearchID, strSearchName, strDepP);

        gvwMain.DataSource = dt;
        gvwMain.DataBind();

    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime dt1;
            string strDate, strDate1;
            strDate = e.Row.Cells[1].Text;
            if (strDate != "")
            {
                dt1 = Convert.ToDateTime(strDate);
                strDate1 = dt1.ToString("yyyy/MM/dd");
                if (strDate1 == "1900/01/01")
                    e.Row.Cells[1].Text = "";
                else
                    e.Row.Cells[1].Text = strDate1;
            }

            if (((HyperLink)e.Row.Cells[5].FindControl("HyperLink2")).Text != "")
            {
                ((HyperLink)e.Row.Cells[5].FindControl("HyperLink2")).Text = "Link";
            }

        }
    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {
        for (int intI = 0; intI < 4; intI++)
        {
            int i = 1;
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if ((intI == 1) || (intI == 2) ||(intI == 3))
                    {
                        if (gvItem.Cells[0].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[0].Text.Trim())
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
                        {
                            gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                            i = 1;
                        }
                    }
                    else
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
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }

    }
    #endregion
}
