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

public partial class WebForm_AddSample1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strToday;

        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            Session["FileN"] = "";
            loadTestCase_Kind(this.ddlKind,"DA40","");
            Session["Upload_Kind"] = "Sample";
            //getID();
            loadEmployees(this.ddlCustodian);
            loadEmployees(this.ddlCustodian1);
            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

            Session["SampleID"] = "S" + strToday;
        }
    }

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "0");
    }
    #endregion

    protected void loadTestCase_Kind(DropDownList DDL, string strDepartment, string strKind)
    {
        clsDropDownList.ddlApplication_TestCase_Kind(DDL, strDepartment, strKind);
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

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strToday;
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;

        //strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

        //Session["SampleID"] = "S" + strToday;

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();
        }

        if (txtName.Text == "")
            clsMsg.AlertMessage("請輸入Model Name....", this.Page);
        else
        {
            if (clsTransaction.InsertSample_N(Session["SampleID"].ToString(), ddlKind.SelectedItem.ToString(), ddlFunction.SelectedItem.ToString(), ddlItem.SelectedItem.ToString(), txtNumber.Text.Trim(), txtCategory.Text.Trim(), txtVendor.Text.Trim(), txtName.Text.Trim(), txtMAC.Text.Trim(), txtPhy.Text.Trim(), txtFirmware.Text.Trim(), txtPhysical.Text.Trim(), txtVoip.Text.Trim(), txtCATV.Text.Trim(), txtUSB.Text.Trim(), txtLAN.Text.Trim(), txtWLAN.Text.Trim(), txtWPS.Text.Trim(),ddlStatus.Text,txtPlace.Text.Trim(),ddlCustodian.Text.Trim(),txtNote.Text,txtNameCode.Text.Trim(),ddlCustodian1.Text,ddlDepartment.Text) == true)
            {
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
                            clsTransaction.InsertUploadFile_Sample(Session["SampleID"].ToString(), strFile_Name, strPath, strToday, Session["EmpName"].ToString());
                        }
                    }
                    clsMsg.AlertMessage("新增成功！", this.Page);
                }
                else
                {
                    clsMsg.AlertMessage("新增成功！", this.Page);
                }
                setEmpty();
            }
            else
                clsMsg.AlertMessage("新增失敗....", this.Page);
        }

        Session["FileN"] = "";

        strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

        Session["SampleID"] = "S" + strToday;
    }

    private void setEmpty()
    {
        ddlItem.Items.Clear();
        ddlFunction.Items.Clear();
        ddlKind.Items.Clear();
        loadTestCase_Kind(this.ddlKind,"DA40","");

        txtCategory.Text = "";
        txtVendor.Text = "";
        txtName.Text = "";
        txtMAC.Text = "";
        txtPhy.Text = "";
        txtFirmware.Text = "";
        txtPhysical.Text = "";
        txtVoip.Text = "";
        txtCATV.Text = "";
        txtUSB.Text = "";
        txtLAN.Text = "";
        txtWLAN.Text = "";
        txtWPS.Text = "";
        txtPlace.Text = "";
        //txtCustodian.Text = "";
        txtNote.Text = "";
        txtNameCode.Text = "";


    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SearchSample.aspx");
    }

    
}
