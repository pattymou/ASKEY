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

public partial class WebForm_AddPR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strToday;

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

            Session["PRID"] = "P" + strToday;

            if (Session["EmpDepartment"].ToString() == "DA40")
                rdoLocal.Checked = true;
            else
                rdoLocal1.Checked = true;

            //loadKind(this.ddlKind);


        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion 

    private DateTime DTAdd(DateTime dt, int addday)
    {
        int i = 0;
        while (i <= addday)
        {
            dt = dt.AddDays(1);
            if ((dt.DayOfWeek.ToString() != "Saturday") & (dt.DayOfWeek.ToString() != "Sunday"))
            {
                i += 1;
            }
        }
        return dt;
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strApplication_Date, strPR_Date;
        string strNotification_Date = "";
        DateTime dt;

        strApplication_Date = Request["date1"].ToString();
        if (strApplication_Date != "")
        {
            dt = Convert.ToDateTime(strApplication_Date);
            strApplication_Date = dt.ToString("yyyyMMdd");
            //strNotification_Date = dt.AddDays(17).ToString("yyyyMMdd");
            dt = DTAdd(dt, 15);
            strNotification_Date = dt.ToString("yyyyMMdd");
        }

        strPR_Date = Request["date2"].ToString();
        if (strPR_Date != "")
        {
            dt = Convert.ToDateTime(strPR_Date);
            strPR_Date = dt.ToString("yyyyMMdd");
           
        }

        string strLocal;
        if (rdoLocal.Checked == true)
            strLocal = "台北";
        else
            strLocal = "吳江";

        if (clsTransaction.InsertPR(Session["PRID"].ToString(), strApplication_Date, txtPR_No.Text.Trim(), strPR_Date, txtSigned_ID.Text.Trim(), txtNote.Text.Trim(), "Open", txtDemand_Person.Text.Trim(), txtMail.Text.Trim(), strNotification_Date, strLocal) == true)
        {
            clsMsg.AlertMessage("新增成功！", this.Page);
            string strToday;

            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

            Session["PRID"] = "P" + strToday;

            setEmpty();
        }
        else
            clsMsg.AlertMessage("新增失敗！", this.Page);
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/GoodsList.aspx");
    }

    private void setEmpty()
    {
        txtNote.Text = "";
        txtPR_No.Text = "";
        txtSigned_ID.Text = "";
        txtDemand_Person.Text = "";
        txtMail.Text = "";
    }
}
