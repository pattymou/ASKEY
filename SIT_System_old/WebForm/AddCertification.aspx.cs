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

public partial class WebForm_AddCertification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadOption(this.ddlAOptional, "1");
            loadOption(this.ddlAOptionalD, "1");
            loadBT(this.ddlDBT_CoreMode, "1");
            loadBT(this.ddlBT_CS, "0");

            DataTable dt = clsData.UploadCertification_Wifi_Data("1","0");
            if (dt.Rows.Count > 0)
                txtac.Text = dt.Rows[0]["Description"].ToString();

            dt = clsData.UploadCertification_Wifi_Data("2","0");
            if (dt.Rows.Count > 0)
                txtN.Text = dt.Rows[0]["Description"].ToString();

            dt = clsData.UploadCertification_Wifi_Data("3","0");
            if (dt.Rows.Count > 0)
                txt6.Text = dt.Rows[0]["Description"].ToString();
        }
    }

    #region loadOption
    protected void loadOption(DropDownList DDL, string strKind)
    {
        clsDropDownList.ddlCertification_Wifi_Optional(DDL, strKind);
    }
    #endregion

    #region loadBT
    protected void loadBT(DropDownList DDL, string strKind)
    {
        clsDropDownList.ddlCertification_BT(DDL, strKind);
    }
    #endregion

    protected void ddlAOptional_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = clsData.UploadCertification_Wifi_Data(ddlAOptional.SelectedValue,"0");
        txtOptionM.Text = dt.Rows[0]["Description"].ToString();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (clsTransaction.InsertCertification_Wifi_Data("1", txtOption.Text, txtOptionA.Text,"") == true)
        {
            clsMsg.AlertMessage("新增成功....", this.Page);
            txtOption.Text = "";
            txtOptionA.Text = "";
            loadOption(this.ddlAOptional, "1");
            loadOption(this.ddlAOptionalD, "1");
        }
        else
        {
            clsMsg.AlertMessage("新增失敗....", this.Page);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateCertification_Wifi_Data(ddlAOptional.SelectedValue, "Content", txtOptionM.Text) == true)
        {
            clsMsg.AlertMessage("修改成功....", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("修改失敗....", this.Page);
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateCertification_Wifi_Data(ddlAOptionalD.SelectedValue, "Disable", "Y") == true)
        {
            clsMsg.AlertMessage("刪除成功....", this.Page);
            loadOption(this.ddlAOptional, "1");
            loadOption(this.ddlAOptionalD, "1");
            txtOptionM.Text = "";
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗....", this.Page);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateCertification_Wifi_Data("1", "Content", txtac.Text) == true)
        {
            if (clsTransaction.UpDateCertification_Wifi_Data("2", "Content", txtN.Text) == true)
            {
                if (clsTransaction.UpDateCertification_Wifi_Data("3", "Content", txt6.Text) == true)
                {
                    clsMsg.AlertMessage("修改成功....", this.Page);
                }
                else
                {
                    clsMsg.AlertMessage("修改失敗....", this.Page);
                }
            }
            else
            {
                clsMsg.AlertMessage("修改失敗....", this.Page);
            }
        }
        else
        {
            clsMsg.AlertMessage("修改失敗....", this.Page);
        }
    }

    protected void btnAddBT_CoreMode_Click(object sender, EventArgs e)
    {
        if (clsTransaction.InsertCertification_BT_Data("1", txtBT_CoreMode.Text, "") == true)
        {
            clsMsg.AlertMessage("新增成功....", this.Page);
            txtBT_CoreMode.Text = "";

            loadBT(this.ddlDBT_CoreMode, "1");

        }
        else
        {
            clsMsg.AlertMessage("新增失敗....", this.Page);
        }
    }

    protected void btnDBT_CoreMode_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateCertification_BT_Disable(ddlDBT_CoreMode.SelectedValue) == true)
        {
            clsMsg.AlertMessage("刪除成功....", this.Page);
            loadBT(this.ddlDBT_CoreMode, "1");
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗....", this.Page);
        }
    }

    protected void ddlDBT_CoreMode_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnABT_CS_Click(object sender, EventArgs e)
    {
        if (clsTransaction.InsertCertification_BT_Data("0", txtBT_CS.Text, "") == true)
        {
            clsMsg.AlertMessage("新增成功....", this.Page);
            txtBT_CS.Text = "";

            loadBT(this.ddlBT_CS, "0");

        }
        else
        {
            clsMsg.AlertMessage("新增失敗....", this.Page);
        }
    }
    protected void btnDBT_CS_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateCertification_BT_Disable(ddlBT_CS.SelectedValue) == true)
        {
            clsMsg.AlertMessage("刪除成功....", this.Page);
            loadBT(this.ddlBT_CS, "0");
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗....", this.Page);
        }
    }
}
