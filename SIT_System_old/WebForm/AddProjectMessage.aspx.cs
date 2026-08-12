using System;
using System.Collections.Generic;
//using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class WebForm_AddProjectMessage : System.Web.UI.Page
{
    public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strID = Request.QueryString["ID"];
            //if ((clsParameter.strEmpName != "") && (clsParameter.strEmpName != null))
            //    txtName.Text = clsParameter.strEmpName;
            if (Session["EmpName"] != null)
                txtName.Text = Session["EmpName"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strDate,strName;

        //strID = "20150313101506";
        strDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if ((Session["EmpName"].ToString() != "") && (Session["EmpName"].ToString() != null))
            strName = Session["EmpName"].ToString();
            //strName = clsParameter.strEmpName;
        else
            strName = txtName.Text;
        //if ((clsParameter.strEmpName != "") && (clsParameter.strEmpName != null))
        //    strName = clsParameter.strEmpName;
        //else
        //    strName = txtName.Text;
        //strName = "patty_lu";

        if (txtName.Text == "")
            clsMsg.AlertMessage("請輸入姓名！", this.Page);
        else
        {
            if (clsTransaction.InsertProjectMessage(strID, CKEditorControl1.Text, strDate, strName, ddlKind.Text) == true)
            {
                clsMsg.AlertMessage("新增成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/ProjectMessage.aspx?ID=" + strID);

        //Response.Redirect("~/WebForm/ModifyApprartus.aspx?ID=" + clsParameter.strApparatusID);
    }
}
