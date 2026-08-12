using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebForm_StatisticsReport : System.Web.UI.Page
{
    public static string strStart;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadKind(this.ddlKind);
            loadKind(this.ddlKind1);
            String strYear = DateTime.Now.Year.ToString();

            txtYearE.Text = strYear;
            txtYearS.Text = strYear;
            loadDepartment(this.ddlDepartment);
            rdoDepartment.Checked = true;
            //rdoWeek.Checked = true;
            rdoMonth.Checked = true;
            loadEmployees(this.ddlCustodian);
            rdoCustodian.Checked = true;

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7, "1");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "1");
    }
    #endregion

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "1");
    }
    #endregion

    private void Search()
    {
        string win_str;
        string strReportDateE;


        if (rdoMonth.Checked == true)
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
        }
        else
        {
            if (strStart != "")
            {
                strStart = Request["date1"].ToString();
                DateTime dt = Convert.ToDateTime(strStart);

                Session["RDateS"] = GetTheFirstDayOfWeek(dt).ToString("yyyy/MM/dd");

                Session["RDateE"] = GetTheLastDayOfWeek(dt).ToString("yyyy/MM/dd");

            }
            else
                clsMsg.AlertMessage("請輸入日期！", this.Page);
        }

        if (rdoLocal.Checked == true)
            Session["RLocal"] = "DA40";
        else
            Session["RLocal"] = "DA40-WJ";

        if (rdoDepartment.Checked == true)
        {
            Session["RDep"] = ddlDepartment.Text;

            if (rdoCustodian.Checked == true)
            {
                Session["RCustodian"] = ddlCustodian.Text;
                win_str = "<script language='javascript'>window.open('../Report/rpt_StatisticsReport2.aspx?RKind=0',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
            }
            else
            {
                Session["RKind"] = ddlKind.Text;
                win_str = "<script language='javascript'>window.open('../Report/rpt_StatisticsReport2.aspx?RKind=1',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";

            }

            //Response.Redirect("~/Report/rpt_StatisticsReport1.aspx");

            
            //win_str = "<script language='javascript'>window.open('http://10.7.5.88/SIT_System/Report/rpt_StatisticsReport1.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";

            //string strPath;

            //strPath = Server.MapPath("../Report/rpt_StatisticsReport1.aspx");

            //win_str = "<script language='javascript'>window.open('" + strPath + "',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";

        }
        else if (rdoProducts_ID.Checked == true)
        //else
        {
            Session["RPID"] = txtProducts_ID.Text;

            //Response.Redirect("~/Report/rpt_ProductReport1.aspx");

            win_str = "<script language='javascript'>window.open('../Report/rpt_ProductReport2.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
            //win_str = "<script language='javascript'>window.open('http://10.7.5.88/SIT_System/Report/rpt_ProductReport1.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
        }
        else
        {
            Session["RKind"] = ddlKind1.Text;

            //Response.Redirect("~/Report/rpt_StatisticsReport1.aspx");

            win_str = "<script language='javascript'>window.open('../Report/rpt_KindApparatusReport1.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";
            //win_str = "<script language='javascript'>window.open('http://10.7.5.88/SIT_System/Report/rpt_KindApparatusReport.aspx',null,'status=yes,toolbar=no,scrollbars=yes,left=10,top=10,width=1000,height=650');</script>";

        }

        Response.Write(win_str);
    }

    public static DateTime GetTheFirstDayOfWeek(DateTime dt1)
    {
        return dt1.AddDays((int)dt1.DayOfWeek * -1).Date;
    }

    public static DateTime GetTheLastDayOfWeek(DateTime dt1)
    {
        return dt1.AddDays(7 + (int)dt1.DayOfWeek * -1 - 1).Date;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

}
