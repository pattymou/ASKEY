using System;
using System.Data;
using System.Text;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WebForm_ChangePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strID;

        //strID = clsParameter.strEmpNo;
        strID = Session["EmpNo"].ToString();


        //DataTable dt = clsData.CheckAccountPwd(clsParameter.strEmpName, txtPwd_O.Text);
        DataTable dt = clsData.CheckAccountPwd(Session["EmpName"].ToString(), txtPwd_O.Text);
        if (dt.Rows.Count > 0)
        {
            if (txtPwd_N.Text != txtPwd_N1.Text)
                clsMsg.AlertMessage("新密碼不符合....", this.Page);
            else
            {
                if ((txtPwd_N.Text == "") && (txtPwd_N1.Text == ""))
                {
                    clsMsg.AlertMessage("新密碼不得為空白", this.Page);
                }
                else
                {
                    if (clsTransaction.UpDatePwd(txtPwd_N.Text, strID) == true)
                        clsMsg.AlertMessage("密碼修改成功....", this.Page);
                    else
                        clsMsg.AlertMessage("密碼修改失敗....", this.Page);
                }
            }
        }
        else
        {
            clsMsg.AlertMessage("原始密碼錯誤，請重新輸入！", this.Page);
        }

    }
}
