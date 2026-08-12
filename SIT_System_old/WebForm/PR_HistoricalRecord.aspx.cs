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

public partial class WebForm_PR_HistoricalRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            String strYear = DateTime.Now.Year.ToString();

            txtYearE.Text = strYear;
            txtYearS.Text = strYear;

            rdoInfo1.Checked = true;
            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;
        }
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

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strDate;
        DateTime dt;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            strDate = e.Row.Cells[0].Text;
            if (strDate != "")
            {
                dt = Convert.ToDateTime(strDate);
                e.Row.Cells[0].Text = dt.ToString("yyyy/MM/dd");
                if (e.Row.Cells[0].Text == "1900/01/01")
                    e.Row.Cells[0].Text = "";
            }

            strDate = e.Row.Cells[2].Text;
            if (strDate != "")
            {
                dt = Convert.ToDateTime(strDate);
                e.Row.Cells[2].Text = dt.ToString("yyyy/MM/dd");
                if (e.Row.Cells[2].Text == "1900/01/01")
                    e.Row.Cells[2].Text = "";
            }

        }
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('PR_Detail.aspx?ID=" + strID + "');</script>");

        }
    }

    private void GvQuery()
    {
        string strLocal;

        if (rdoLocal.Checked == true)
            strLocal = "台北";
        else
            strLocal = "吳江";

        if (rdoInfo1.Checked == true)
        {
            string strStart, strEnd;

            if ((Convert.ToInt32(ddlMonthE.Text) < 9))
            {
                if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
                {
                    if (ddlMonthE.Text == "02")
                    {
                        if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                            strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                        else
                            strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                    }
                    else
                    {
                        if (ddlMonthE.Text == "08")
                            strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                        else
                            strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                        //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                    }
                }
                else
                    strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
            }
            else
            {
                if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                    strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                else
                    strEnd = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                //clsParameter.strReportDateE = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
            }
            strStart = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";

            if ((txtYearS.Text == "") || (txtYearE.Text == ""))
                clsMsg.AlertMessage("請輸入日期區間！", this.Page);
            else
            {
                DataTable dt = clsData.UploadPRQuery("0", strStart, strEnd, "", strLocal);
                this.gvwMain.DataSource = dt;
                this.DataBind();
            }
        }
        else
        {
            DataTable dt = clsData.UploadPRQuery("1", "", "", txtSearch.Text.Trim(), strLocal);
            this.gvwMain.DataSource = dt;
            this.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GvQuery();
    }
}
