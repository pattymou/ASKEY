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

public partial class WebForm_AddDepartmentAccount : System.Web.UI.Page
{

    //public static string strID;
    //public static string strKind;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            if (Request.QueryString["ID"].ToString() != null)
            {
                txtDepartment.Enabled = false;
                getAccount(Request.QueryString["ID"].ToString());
            }
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        //if ((strID == "") || (strID == null))
        //    AddAccount();
        //else
        //    ModifyAccount();

        if (((txtDepartment.Text == "") || (txtDepartment.Text == null)) && ((txtPassword.Text == "") || (txtPassword.Text == null)))
            clsMsg.AlertMessage("請輸入部門名稱及密碼....", this.Page);
        else
        {
            //if ((strID == "") || (strID == null))
            //    AddAccount();
            //else
            //    ModifyAccount();
            //if (strKind == "0")
            //    AddAccount();
            //else
            //    ModifyAccount();

            DataTable dt = clsData.getDepartmentAccount(Request.QueryString["ID"].ToString());

            if (dt.Rows.Count == 0)
            {
                AddAccount();
            }
            else
                ModifyAccount();
        }
    }

    #region getAccount
    private void getAccount(string strID1)
    {
        DataTable dt = clsData.getDepartmentAccount(strID1);

        if (dt.Rows.Count == 0)
        {
            txtDepartment.Text = Request.QueryString["ID"].ToString();
            //strKind = "0";
        }
        else
        {
            txtDepartment.Text = dt.Rows[0]["ID"].ToString();
            txtPassword.Text = dt.Rows[0]["Password"].ToString();
            //strKind = "1";
        }
    }
    #endregion

    #region AddAccount
    private void AddAccount()
    {
        string strID1 = txtDepartment.Text;
        string strPassword = txtPassword.Text;

        if (clsTransaction.InsertDepartmentAccount(strID1,strPassword))
        {
            clsMsg.AlertMessage("修改成功....", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("此部門已有帳號....", this.Page);
        }

        //txtDepartment.Text = "";
        //txtPassword.Text = "";
    }
    #endregion

    #region ModifyAccount
    private void ModifyAccount()
    {
        string strID1 = txtDepartment.Text;
        string strPassword = txtPassword.Text;

        if (clsTransaction.UpDateDepartmentAccount(strID1, strPassword))
        {
            clsMsg.AlertMessage("修改成功....", this.Page);
        }
        else
        {
            clsMsg.AlertMessage("修改失敗....", this.Page);
        }
    }
    #endregion
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/DepartmentAccountView.aspx");
    }
}
