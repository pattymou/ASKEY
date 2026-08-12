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

public partial class WebForm_ModifyNumber : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadDepartment(this.ddlDepartment);

            getNumber();
        }
    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateNumber(lblID.Text, txtNumber.Text.Trim(), txtName.Text.Trim(), txtMail.Text.Trim(), ddlDepartment.Text, txtPassWord.Text.Trim(), txtCard.Text.Trim(), txtExt.Text.Trim()) == true)
        {
            clsMsg.AlertMessage("修改成功....", this.Page);
        }
        else
            clsMsg.AlertMessage("修改失敗....", this.Page);
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/UserView1.aspx");
    }

    private void getNumber()
    {
        string strID;

        strID = Request.QueryString["ID"];
        DataTable dt = clsData.getNumber("3", strID);

        lblID.Text = dt.Rows[0]["ID"].ToString();
        txtNumber.Text = dt.Rows[0]["Number"].ToString();
        txtName.Text = dt.Rows[0]["Name"].ToString();
        txtExt.Text = dt.Rows[0]["Ext"].ToString();
        txtMail.Text = dt.Rows[0]["Mail"].ToString();
        ddlDepartment.Text = dt.Rows[0]["Department"].ToString();
        txtCard.Text = dt.Rows[0]["CardNumber"].ToString();
        txtPassWord.Text = dt.Rows[0]["PassWord"].ToString();

    }
}
