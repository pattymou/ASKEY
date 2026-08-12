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

public partial class WebForm_AddSample : System.Web.UI.Page
{
    public static string strStart;
    public static string strEnd;
    public static string strID;
    public static string strSID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadNPI(this.ddlNPI);
            getID();
        }
    }

    #region loadNPI
    protected void loadNPI(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 2, "0");
    }
    #endregion 

    private void getID()
    {
        //int intCount;
        DataTable dt;



        dt = clsData.UploadSample();
        strSID = (dt.Rows.Count + 1).ToString();

        //int intCount1;
        DataTable dt1 = clsData.UploadSampleRelease("", strSID);
        strID = (dt1.Rows.Count + 1).ToString();

        lblNumber.Text = strSID + "-" + strID;


    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strReceiveDate, strReturnDate;
        DateTime dt;

        if (txtName.Text == "")
            clsMsg.AlertMessage("請輸入機種名稱....", this.Page);
        else
        {
            if (clsTransaction.InsertSample(strSID, txtName.Text.Trim()) == true)
            {
                strReceiveDate = Request["date1"].ToString();
                if (strReceiveDate != "")
                {
                    dt = Convert.ToDateTime(strReceiveDate);
                    strReceiveDate = dt.ToString("yyyyMMdd");
                }

                strReturnDate = Request["date2"].ToString();
                if (strReturnDate != "")
                {
                    dt = Convert.ToDateTime(strReturnDate);
                    strReturnDate = dt.ToString("yyyyMMdd");
                }
                //int intCount;
                //DataTable dt1 = clsData.UploadSampleRelease("", strSID);
                //intCount = dt1.Rows.Count + 1;

                if (clsTransaction.InsertSampleRelease(strID, strSID, txtMAC.Text.Trim(), ddlNPI.Text.Trim(), txtTotal.Text.Trim(), txtCustodian.Text.Trim(), txtProvide.Text.Trim(), strReceiveDate, strReturnDate, txtExplain.Text.Trim()) == true)
                {
                    clsMsg.AlertMessage("新增成功....", this.Page);
                }
            }
            else
                clsMsg.AlertMessage("新增失敗....", this.Page);
        }
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SearchSample.aspx");
    }
}
