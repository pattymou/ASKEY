using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_StatisticsCReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {

            loadDepartment(this.ddlDepartment);
            loadCertification(this.ddlKind);


            int iYear = System.DateTime.Now.Year;
            int iMonth = System.DateTime.Now.Month;

            txtYearE.Text = iYear.ToString();
            txtYearS.Text = iYear.ToString();
            //ddlMonthS.Text = String.Format("{0:00}", iMonth);
            //ddlMonthE.Text = String.Format("{0:00}", iMonth);
        }
    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlDepartment(DDL, "1", "1");
    }
    #endregion

    #region loadCertification
    protected void loadCertification(DropDownList DDL)
    {
        clsDropDownList.ddlCertification_Kind(DDL, "44", "1");
    }
    #endregion

    private void Search()
    {
        string win_str;
        string strReportDateE;



        if ((txtYearS.Text == "") || (txtYearE.Text == ""))
            clsMsg.AlertMessage("請輸入日期區間！", this.Page);
        else
        {
            Session["RDateS"] = txtYearS.Text.Trim() + "/" + "01/01";
            Session["RDateE"] = txtYearE.Text + "/" + "12/31";
            //if ((Convert.ToInt32(ddlMonthE.Text) < 9))
            //{
            //    if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
            //    {
            //        if (ddlMonthE.Text == "02")
            //        {
            //            if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
            //                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
            //            else
            //                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
            //        }
            //        else
            //        {
            //            if (ddlMonthE.Text == "08")
            //                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
            //            else
            //                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
            //        }
            //    }
            //    else
            //        Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
            //}
            //else
            //{
            //    if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
            //        Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
            //    else
            //        Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
            //}
        }

        Session["RDep"] = ddlDepartment.Text;
        Session["RKind"] = ddlKind.Text;

        win_str = "<script language='javascript'>window.open('../Report/rpt_StatisticsCReport.aspx',null,'status=yes,toolbar=yes,scrollbars=yes,left=10,top=10,width=1500,height=800');</script>";

        Response.Write(win_str);
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        Search();
    }
}
