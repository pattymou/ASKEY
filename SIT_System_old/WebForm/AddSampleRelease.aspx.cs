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

public partial class WebForm_AddSampleRelease : System.Web.UI.Page
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
            strID = Request.QueryString["ID"];
            strSID = Request.QueryString["SID"];
            loadNPI(this.ddlNPI);

            if (strID != "")
            {
                getSID();
            }
            else 
            {
                getID();
            }
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
        int intCount;
        //string strCount;
        DataTable dt;

        //dt = clsData.UploadSample();
        //strCount = dt.Rows.Count;

        dt = clsData.UploadSampleRelease("",strSID);
        intCount = dt.Rows.Count;
        lblNumber.Text = strSID + "-" + (intCount + 1).ToString();

        dt = clsData.UploadSample1("", strSID);
        lblName.Text = dt.Rows[0]["Name"].ToString();
    }

    private void getSID()
    {
        int intCount;
        //string strCount;
        DataTable dt,dt2;
        DateTime dt1;
        string strStart1, strEnd1, strSample1;
        //dt = clsData.UploadSample();
        //strCount = dt.Rows.Count;

        dt = clsData.UploadSampleRelease(strID,strSID);

        dt2 = clsData.UploadSample1("",strSID);

        lblNumber.Text = strSID + "-" + strID;
        lblName.Text = dt2.Rows[0]["Name"].ToString();
        txtMAC.Text = dt.Rows[0]["MAC"].ToString();
        txtTotal.Text = dt.Rows[0]["Total"].ToString();
        ddlNPI.Text = dt.Rows[0]["NPI"].ToString();
        txtCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        txtProvide.Text = dt.Rows[0]["Provide"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["ReceiveDate"].ToString());
        strStart1 = dt1.ToString("yyyy/MM/dd");
        if (strStart1 == "1900/01/01")
            strStart = "";
        else
            strStart = strStart1;

        dt1 = Convert.ToDateTime(dt.Rows[0]["ReturnDate"].ToString());
        strEnd1 = dt1.ToString("yyyy/MM/dd");
        if (strEnd1 == "1900/01/01")
            strEnd = "";
        else
            strEnd = strEnd1;

        txtExplain.Text = dt.Rows[0]["Note"].ToString();

    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strReceiveDate, strReturnDate;
        int intCount;
        DateTime dt;

        if (strID == "")
        {
            DataTable dt1 = clsData.UploadSampleRelease("", strSID);
            intCount = dt1.Rows.Count + 1;


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

            if (clsTransaction.InsertSampleRelease(intCount.ToString(),strSID, txtMAC.Text.Trim(), ddlNPI.Text.Trim(), txtTotal.Text.Trim(), txtCustodian.Text.Trim(), txtProvide.Text.Trim(), strReceiveDate, strReturnDate, txtExplain.Text.Trim()) == true)
            {
                clsMsg.AlertMessage("新增成功....", this.Page);
            }
            else
                clsMsg.AlertMessage("新增失敗....", this.Page);
        }
        else
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

            if (clsTransaction.UpDateSampleRelease(strID, strSID, txtMAC.Text, ddlNPI.Text, txtTotal.Text, txtCustodian.Text, txtProvide.Text, strReceiveDate, strReturnDate, txtExplain.Text) == true)
                clsMsg.AlertMessage("修改成功....", this.Page);
            else
                clsMsg.AlertMessage("修改失敗....", this.Page);
        }
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SampleRelease.aspx?ID=" + strSID);
    }
}
