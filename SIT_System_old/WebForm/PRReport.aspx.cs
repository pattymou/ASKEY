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

public partial class WebForm_PRReport : System.Web.UI.Page
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
            rdoReport1.Checked = true;

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    private void Search()
    {
        string win_str;

        if (rdoLocal.Checked == true)
            Session["Report_Local"] = "台北";
        else
            Session["Report_Local"] = "吳江";

        if (rdoReport1.Checked == true)
        {
            win_str = "<script language='javascript'>window.open('../Report/rpt_PRReport.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
            //win_str = "<script language='javascript'>window.open('http://10.7.5.88/SIT_System/Report/rpt_PRReport.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        }
        else
        {
            Session["RDateS"] = txtYearS.Text.Trim() + "/" + ddlMonthS.Text + "/01";


            if ((txtYearS.Text == "") || (txtYearE.Text == ""))
                clsMsg.AlertMessage("請輸入日期區間！", this.Page);
            else
            {
                if ((Convert.ToInt32(ddlMonthE.Text) < 9))
                {
                    if ((Convert.ToInt32(ddlMonthE.Text) % 2) == 0)
                    {
                        if (ddlMonthE.Text == "02")
                        {
                            if ((Convert.ToInt32(txtYearE.Text) / 4) == 0)
                                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/29";
                            else
                                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/28";
                        }
                        else
                        {
                            if (ddlMonthE.Text == "08")
                                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                            else
                                Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                        }
                    }
                    else
                        Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                }
                else
                {
                    if ((ddlMonthE.Text == "09") || (ddlMonthE.Text == "11"))
                        Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/30";
                    else
                        Session["RDateE"] = txtYearE.Text + "/" + ddlMonthE.Text + "/31";
                }


            }
            win_str = "<script language='javascript'>window.open('../Report/rpt_PRHistoryReport.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
            //win_str = "<script language='javascript'>window.open('http://10.7.5.88/SIT_System/Report/rpt_PRHistoryReport.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        }

        Response.Write(win_str);
    }
}
