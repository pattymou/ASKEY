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

public partial class WebForm_Certification_BT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ApplicationID"] = Request.QueryString["ID"];
            loadBT(this.ddlCoreMode, "1");
            loadBT(this.ddlVersion, "0");

            getData();
        }
    }

    #region loadBT
    protected void loadBT(DropDownList DDL, string strKind)
    {
        clsDropDownList.ddlCertification_BT(DDL, strKind);
    }
    #endregion

    private void getData()
    {
        DataTable dt;

        //dt = clsData.UploadCertification_Wifi("1234");
        dt = clsData.UploadCertification_BT(Session["ApplicationID"].ToString());
        if (dt.Rows.Count > 0)
        {
            ddlVersion.Text = dt.Rows[0]["BT_Version"].ToString();
            ddlCoreMode.Text = dt.Rows[0]["Core_Mode"].ToString();
            txtBriefly.Text = dt.Rows[0]["Briefly_Describe"].ToString();
            txtApplication.Text = dt.Rows[0]["Application_Profiles"].ToString();
            txtController_Vendor.Text = dt.Rows[0]["Controller_Vendor"].ToString();
            txtController_DID.Text = dt.Rows[0]["Controller_DID"].ToString();
            txtHost_Vendor.Text = dt.Rows[0]["Host_Vendor"].ToString();
            txtHost_DID.Text = dt.Rows[0]["Host_DID"].ToString();
            txtComponent_Vendor.Text = dt.Rows[0]["Component_Vendor"].ToString();
            txtComponent_DID.Text = dt.Rows[0]["Component_DID"].ToString();
            txtEnd_Vendor.Text = dt.Rows[0]["End_Vendor"].ToString();
            txtEnd_DID.Text = dt.Rows[0]["End_DID"].ToString();

        }
    }


    protected void butOK_Click(object sender, EventArgs e)
    {
        string strProjectID = Session["ApplicationID"].ToString();
        string strBT_Version, strCore_Mode, strBriefly_Describe, strApplication_Profiles, strController_Vendor, strController_DID, strHost_Vendor, strHost_DID, strComponent_Vendor, strComponent_DID, strEnd_Vendor, strEnd_DID;

        strBT_Version = ddlVersion.SelectedValue;
        strCore_Mode = ddlCoreMode.SelectedValue;
        strBriefly_Describe = txtBriefly.Text;
        strApplication_Profiles = txtApplication.Text;
        strController_Vendor = txtController_Vendor.Text;
        strController_DID = txtController_DID.Text;
        strHost_Vendor = txtHost_Vendor.Text;
        strHost_DID = txtHost_DID.Text;
        strComponent_Vendor = txtComponent_Vendor.Text;
        strComponent_DID = txtComponent_DID.Text;
        strEnd_Vendor = txtEnd_Vendor.Text;
        strEnd_DID = txtEnd_DID.Text;

        DataTable dt1 = clsData.UploadCertification_BT(strProjectID);
        //DataTable dt1 = clsData.UploadCertification_Wifi(Session["ApplicationID"].ToString());
        if (dt1.Rows.Count > 0)
        {
            if (clsTransaction.UpDateCertification_BT(strProjectID, strBT_Version, strCore_Mode, strBriefly_Describe, strApplication_Profiles, strController_Vendor, strController_DID, strHost_Vendor, strHost_DID, strComponent_Vendor, strComponent_DID, strEnd_Vendor, strEnd_DID) == true)
                clsMsg.AlertMessage("暫存成功....", this.Page);
            else
                clsMsg.AlertMessage("暫存失敗....", this.Page);
        }
        else
        {
            if (clsTransaction.InsertCertification_BT(strProjectID, strBT_Version, strCore_Mode, strBriefly_Describe, strApplication_Profiles, strController_Vendor, strController_DID, strHost_Vendor, strHost_DID, strComponent_Vendor, strComponent_DID, strEnd_Vendor, strEnd_DID) == true)
                clsMsg.AlertMessage("暫存成功....", this.Page);
            else
                clsMsg.AlertMessage("暫存失敗....", this.Page);
        }

    }
}
