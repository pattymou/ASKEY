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

public partial class WebForm_AddUser : System.Web.UI.Page
{
    //public static string strID;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strID;
        //clsParameter.strEmpName = "patty_lu";
        //clsParameter.strEmpNo = "806123";
        //clsParameter.strEmpPosition = "3";
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //DefaultData();
            loadTeam(this.ddlTeam);
            loadDepartment(this.ddlDepartment);
            loadPosition(this.ddlJob);
            getSystem();


            strID = Request.QueryString["ID"];
            //strID = "Patty_Lu";
            if (strID != null)
            {
                txtLogin.Enabled = false;
                //txtPwd.Enabled = false;
                //txtPwd_C.Enabled = false;
                //txtNumber.Enabled = false;
                getEmployees(Request.QueryString["ID"].ToString());
            }
        }

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strID;

        strID = Request.QueryString["ID"];
        if ((strID == "") || (strID == null))
            AddEmployees();
        else
        {
            ModifyEmployees();

            getEmployees(strID);
        }
            getSystem();

    }

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 4,"0");    
    }
    #endregion 

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3,"0");
    }
    #endregion

    #region loadPosition
    protected void loadPosition(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 5,"0");
    }
    #endregion 

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //GvQuery(true);
        getSystem();
    }
    #endregion

    #region getSystem 
    private void getSystem()
    {
        DataTable dt = clsData.getSystemList();
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    #region getEmployees
    private void getEmployees(string strID1)
    {
        DataTable dt = clsData.getEmployees("1",strID1);

        //txtNumber.Text = dt.Rows[0]["ID"].ToString();
        txtLogin.Text = dt.Rows[0]["Name_En"].ToString();
        txtName.Text = dt.Rows[0]["Name_CH"].ToString();
        ddlDepartment.Text = dt.Rows[0]["Department"].ToString();
        ddlTeam.Text = dt.Rows[0]["Team"].ToString();
        ddlJob.Text = dt.Rows[0]["Position"].ToString();
        txtExt.Text = dt.Rows[0]["Extension"].ToString();
        txtPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
        txtAddress.Text = dt.Rows[0]["Address"].ToString();
        txtMail.Text = dt.Rows[0]["Email"].ToString();
        txtPwd.Text = dt.Rows[0]["Password"].ToString();
        txtPwd_C.Text = dt.Rows[0]["Password"].ToString();

        //if (dt.Rows[0]["Location"].ToString() == "台北")
        //    rdoAcceptT.Checked = true;
        //else
        //    rdoAcceptW.Checked = true;

        if (dt.Rows[0]["Write"].ToString() == "Y")
            rdoWrite_Y.Checked = true;
        else
            rdoWrite_N.Checked = true;

        if (dt.Rows[0]["TeamLeader"].ToString() == "Y")
            chkLeader.Checked = true;   
        else
            chkLeader.Checked = false;

        if (dt.Rows[0]["Manager"].ToString() == "Y")
            chkManager.Checked = true;
        else
            chkManager.Checked = false;


        int i,j;
        DataTable dt1 = clsData.getAuthority(strID1);

        foreach (DataRow dr in dt1.Rows)
        {
            for (i = 0; i < this.gvwMain.Rows.Count; i++)
            {
                string strFunction_No;

                strFunction_No = ((Label)this.gvwMain.Rows[i].Cells[2].FindControl("lblFunction_NoGV")).Text;
                if (strFunction_No == dr["Function_No"].ToString())
                {
                    ((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked = true;
                }

            }
        }
    }
    #endregion

    #region AddEmployees
    private void AddEmployees()
    {
        DataTable dt;

        //string strNumber = txtNumber.Text.Trim();
        string strNumber = "";
        string strLogin = txtLogin.Text.Trim();
        string strPwd = txtPwd.Text.Trim();
        string strPwd_C = txtPwd_C.Text.Trim();
        string strName = txtName.Text.Trim();
        string strTeam = ddlTeam.SelectedItem.Text;
        string strDepartment = ddlDepartment.Text;
        string strPosition = ddlJob.SelectedItem.Text;
        string strExt = txtExt.Text.Trim();
        string strPhone = txtPhone.Text.Trim();
        string strAdd = txtAddress.Text.Trim();
        string strEmail = txtMail.Text.Trim();
        string strSystemNo;
        string strLocation;
        string strWrite,strLeader,strManager;



        int i;

        //if (rdoAcceptT.Checked == true)
        //    strLocation = "台北";
        //else
        //    strLocation = "吳江";

        if (rdoWrite_Y.Checked == true)
            strWrite = "Y";
        else
            strWrite = "N";

        if (chkLeader.Checked == true)
            strLeader = "Y";
        else
            strLeader = "N";

        if (chkManager.Checked == true)
            strManager = "Y";
        else
            strManager = "N";

        dt = clsData.getEmployees("2", strNumber);

        //if (dt.Rows.Count > 0)
        //    clsMsg.AlertMessage("已有相同工號....", this.Page);
        //else
        //{
            for (i = 0; i < this.gvwMain.Rows.Count; i++)
            {
                if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
                {
                    strSystemNo = ((Label)this.gvwMain.Rows[i].Cells[2].FindControl("lblFunction_NoGV")).Text;
                    clsTransaction.InsertAuthority(strSystemNo, strLogin,"");
                }
            }

            DataTable dt1 = clsData.CreateMenuView("", "2");

            for (i = 0; i < dt1.Rows.Count ; i++)
            {
                //clsTransaction.InsertAuthority(i.ToString(), strLogin,"Y");
                clsTransaction.InsertAuthority(dt1.Rows[i]["Function_No"].ToString(), strLogin, "Y");
            }


            if (clsTransaction.InsertUser(strNumber, strLogin, strName, strDepartment, strTeam, strPosition, strExt, strPhone, strAdd, strEmail, strPwd, "", strWrite, strLeader, strManager) == true)
            {
                clsMsg.AlertMessage("新增成功....", this.Page);
            }
            else
            {
                clsMsg.AlertMessage("新增失敗....", this.Page);
            }

            //txtNumber.Text = "";
            txtLogin.Text = "";
            txtPwd.Text = "";
            txtPwd_C.Text = "";
            txtName.Text = "";
            txtExt.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtMail.Text = "";

            ddlDepartment.SelectedIndex = 0;
            ddlJob.SelectedIndex = 0;
        //}
    }
    #endregion

    #region ModifyEmployees
    private void ModifyEmployees()
    {
        string strNumber = "";
        string strLogin = txtLogin.Text.Trim();
        string strName = txtName.Text.Trim();
        string strTeam = ddlTeam.SelectedItem.Text;
        string strDepartment = ddlDepartment.Text;
        string strPosition = ddlJob.SelectedItem.Text;
        string strExt = txtExt.Text.Trim();
        string strPhone = txtPhone.Text.Trim();
        string strAdd = txtAddress.Text.Trim();
        string strEmail;
        string strSystemNo;
        string strLocation;
        string strPwd,strLeader,strManager;
        string strWrite;
        

        //if (rdoAcceptT.Checked == true)
        //    strLocation = "台北";
        //else
        //    strLocation = "吳江";

        strEmail = txtMail.Text.Trim();

        if (chkLeader.Checked == true)
            strLeader = "Y";
        else
            strLeader ="N";

        if (chkManager.Checked == true)
            strManager = "Y";
        else
            strManager = "N";

        if (rdoWrite_Y.Checked == true)
            strWrite = "Y";
        else
            strWrite = "N";


        if (txtPwd.Text == txtPwd_C.Text)
        {
            strPwd = txtPwd.Text;
            if (clsTransaction.UpdateEmployeesData(strNumber, strLogin, strName, strDepartment, strTeam, strPosition, strExt, strPhone, strAdd, strEmail, "", strPwd, strLeader, strManager, strWrite) == true)
            {
                clsTransaction.DelAuthority(strLogin);
                int i;
                for (i = 0; i < this.gvwMain.Rows.Count; i++)
                {
                    if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
                    {
                        strSystemNo = ((Label)this.gvwMain.Rows[i].Cells[2].FindControl("lblFunction_NoGV")).Text;
                        clsTransaction.InsertAuthority(strSystemNo, strLogin,"");
                    }
                }
                DataTable dt1 = clsData.CreateMenuView("", "2");

                for (i = 0; i < dt1.Rows.Count; i++)
                {
                    //clsTransaction.InsertAuthority(i.ToString(), strLogin,"Y");
                    clsTransaction.InsertAuthority(dt1.Rows[i]["Function_No"].ToString(), strLogin, "Y");
                }

                clsMsg.AlertMessage("修改成功....", this.Page);
            }
            else
                clsMsg.AlertMessage("修改失敗....", this.Page);
        }
        else
            clsMsg.AlertMessage("兩次密碼輸入不一致....", this.Page);
        
    }
    #endregion

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/UserView1.aspx");
    }

    #region gvwMain_PreRender
    protected void gvwMain_PreRender(object sender, EventArgs e)
    {
        int i = 1;

        for (int intI = 1; intI < 2; intI++)
        {
            foreach (GridViewRow gvItem in gvwMain.Rows)
            {
                if (gvItem.RowIndex != 0)
                {
                    if (gvItem.Cells[intI].Text.Trim() == gvwMain.Rows[(gvItem.RowIndex - 1)].Cells[intI].Text.Trim())
                    {
                        gvwMain.Rows[(gvItem.RowIndex - i)].Cells[intI].RowSpan += 1;
                        gvItem.Cells[intI].Visible = false;
                        i = i + 1;
                    }
                    else
                    {
                        gvwMain.Rows[(gvItem.RowIndex)].Cells[intI].RowSpan += 1;
                        i = 1;
                    }
                }
                else
                    gvItem.Cells[intI].RowSpan = 1;
            }
        }

    }
    #endregion

    
}
