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
using System.IO;

public partial class WebForm_ModifySample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            Session["FileN"] = "";
            loadTestCase_Kind(this.ddlKind,"DA40","general");
            loadEmployees(this.ddlCustodian);
            loadEmployees(this.ddlCustodian1);
            getSample();
            GvQuery();
        }
    }

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "0");
    }
    #endregion

    protected void loadTestCase_Kind(DropDownList DDL,string strDepartment,string strApplication_Kind)
    {
        clsDropDownList.ddlApplication_TestCase_Kind(DDL, strDepartment, strApplication_Kind);
    }

    protected void loadTestCase_Function(DropDownList DDL, string strID)
    {
        clsDropDownList.ddlApplication_TestCase_Function1(DDL, strID);
    }

    protected void loadTestCase_Item(DropDownList DDL, string strID, string strFunctionID)
    {
        clsDropDownList.ddlApplication_TestCase_Item(DDL, strID, strFunctionID);
    }

    protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function(this.ddlFunction, ddlKind.SelectedValue);
        ddlItem.Items.Clear();
    }

    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlItem, ddlKind.SelectedValue, ddlFunction.SelectedValue);
    }

    private void getSample()
    {
        string strID;

        strID = Request.QueryString["ID"];
        DataTable dt = clsData.UploadSample_N(strID, "1");

        ddlKind.SelectedItem.Text = dt.Rows[0]["Kind"].ToString();
        loadTestCase_Function(this.ddlFunction, ddlKind.SelectedValue);


        ddlFunction.SelectedItem.Text = dt.Rows[0]["Function_Name"].ToString();
        loadTestCase_Item(this.ddlItem, ddlKind.SelectedValue, ddlFunction.SelectedValue);
        ddlItem.SelectedItem.Text = dt.Rows[0]["Item"].ToString();
        txtNumber.Text = dt.Rows[0]["Number"].ToString();
        txtCategory.Text = dt.Rows[0]["Category"].ToString();
        txtVendor.Text = dt.Rows[0]["Vendor"].ToString();
        txtName.Text = dt.Rows[0]["ModelName"].ToString();
        txtMAC.Text = dt.Rows[0]["MAC"].ToString();
        txtPhy.Text = dt.Rows[0]["PHY"].ToString();
        txtFirmware.Text = dt.Rows[0]["Firmware"].ToString();
        txtPhysical.Text = dt.Rows[0]["Physical"].ToString();
        txtVoip.Text = dt.Rows[0]["VoIP"].ToString();
        txtCATV.Text = dt.Rows[0]["CATV"].ToString();
        txtUSB.Text = dt.Rows[0]["USB"].ToString();
        txtLAN.Text = dt.Rows[0]["LAN"].ToString();
        txtWLAN.Text = dt.Rows[0]["WLAN"].ToString();
        txtWPS.Text = dt.Rows[0]["WPS"].ToString();
        ddlStatus.Text = dt.Rows[0]["ReservationStatus"].ToString();
        txtPlace.Text = dt.Rows[0]["Place"].ToString();
        //txtCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        txtNameCode.Text = dt.Rows[0]["NameCode"].ToString();
        ddlDepartment.Text = dt.Rows[0]["Custodian_Department"].ToString();

        ListItem item = ddlCustodian.Items.FindByValue(dt.Rows[0]["Custodian"].ToString());
        if (item != null)
        {
            ddlCustodian.SelectedValue = dt.Rows[0]["Custodian"].ToString();
        }

        item = ddlCustodian1.Items.FindByValue(dt.Rows[0]["Agent"].ToString());
        if (item != null)
        {
            ddlCustodian1.SelectedValue = dt.Rows[0]["Agent"].ToString();
        }

    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strID;

        strID = Request.QueryString["ID"];
        if (txtName.Text.Trim() == "")
        {
            clsMsg.AlertMessage("請輸入Model Name！", this.Page);

        }
        else
        {
            if (clsTransaction.UpdateSample(strID, ddlKind.SelectedItem.ToString(), ddlFunction.SelectedItem.ToString(), ddlItem.SelectedItem.ToString(), txtNumber.Text.Trim(), txtCategory.Text.Trim(), txtVendor.Text.Trim(), txtName.Text.Trim(), txtMAC.Text.Trim(), txtPhy.Text.Trim(), txtFirmware.Text.Trim(), txtPhysical.Text.Trim(), txtVoip.Text.Trim(), txtCATV.Text.Trim(), txtUSB.Text.Trim(), txtLAN.Text.Trim(), txtWLAN.Text.Trim(), txtWPS.Text.Trim(), ddlStatus.Text,txtPlace.Text.Trim(),ddlCustodian.Text.Trim(),txtNote.Text.Trim(),txtNameCode.Text.Trim(),ddlCustodian1.Text,ddlDepartment.Text) == true)
            {
                clsMsg.AlertMessage("修改成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("修改失敗！", this.Page);
        }
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        string strID;

        strID = Request.QueryString["ID"];
        Response.Redirect("~/WebForm/SampleView.aspx?ID=" + strID);
    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName, strPath;

        strName = ((HyperLink)this.gvwMain.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        strPath = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblGVSeq")).Text;
        string path = strPath + "\\" + strName;
        if (clsTransaction.DelSampleFilesCase(strName, Request.QueryString["ID"]) == true)
        {
            File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            GvQuery();
            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗！", this.Page);
        }
    }
    #endregion

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    private void GvQuery()
    {
        string strID;

        strID = Request.QueryString["ID"];

        DataTable dt = clsData.UploadSampleFileQuery(strID, "0");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void butSave_Click(object sender, EventArgs e)
    {
        string strToday;
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strID;

        strID = Request.QueryString["ID"];
        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();
        }

        if ((strFile != null) && (strFile != ""))
        {
            string[] sArray = strFile.Split(',');
            foreach (string i in sArray)
            {
                if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                {
                    intFile = i.LastIndexOf('\\');
                    strPath = i.Substring(0, intFile);
                    strFile_Name = i.Substring(intFile + 1);
                    clsTransaction.InsertUploadFile_Sample(strID, strFile_Name, strPath, strToday, Session["EmpName"].ToString());
                }
            }
            clsMsg.AlertMessage("新增成功！", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("新增成功！", this.Page);
        }

        GvQuery();
        Session["FileN"] = "";
    }
}
