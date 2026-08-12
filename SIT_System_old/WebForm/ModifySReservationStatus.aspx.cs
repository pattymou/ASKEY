using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

public partial class WebForm_ModifySReservationStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            getReservation();

            string strID1;
            //============0217
            strID1 = Session["EmpName"].ToString().Trim();
            //strID = "patty_lu";
            if ((strID1 == "") || (strID1 == null))
            {
                butOK.Visible = false;
            }
            //============0217
        }
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strStatus, strToday;

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        //if (rdoAccpt.Checked == true)
        //    strStatus = "Y";
        //else
        //    strStatus = "N";

        if (ddlStatus.Text == "閒置中")
        {
            strStatus = "E";

            if (clsTransaction.UpDateReservation(strStatus, Request.QueryString["ID"], strToday, "2", "", "Other") == true)
            {
                //==========0217
                DataTable dt = clsData.UploadReservationAID(Request.QueryString["ID"]);
                string strApparatusID;
                strApparatusID = dt.Rows[0]["Apparatus_ID"].ToString();
                if (clsTransaction.UpDateSampleStatus("閒置中", strApparatusID) == true)
                    //==========0217
                    Response.Redirect("~/WebForm/DelaySample.aspx");
            }
            else
                clsMsg.AlertMessage("更新失敗！", this.Page);
        }
    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/DelaySample.aspx");
    }

    private void getReservation()
    {
        DataTable dt = clsData.UploadSampleReservationQuery(Request.QueryString["ID"], "1");
        lblNumber.Text = dt.Rows[0]["Number"].ToString();
        //lblProductID.Text = dt.Rows[0]["Products_ID"].ToString();
        //lblBrand.Text = dt.Rows[0]["Brand"].ToString();
        //lblModel.Text = dt.Rows[0]["Model"].ToString();
        //lblCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        lblBorrower.Text = dt.Rows[0]["Borrower"].ToString();
        lblDepartment.Text = dt.Rows[0]["Department"].ToString();
        lblExt.Text = dt.Rows[0]["Ext"].ToString();
        lblMail.Text = dt.Rows[0]["Email"].ToString();
        lblStartDate.Text = dt.Rows[0]["StartDate"].ToString();
        lblEndDate.Text = dt.Rows[0]["EndDate"].ToString();
        lblModelName.Text = dt.Rows[0]["ModelName"].ToString();
        //txtNote.Text = dt.Rows[0]["Note"].ToString();
    }
}
