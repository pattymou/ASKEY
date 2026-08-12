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

public partial class WebForm_AddInfoCase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strNumber;
        DataTable dt1;
        //string[] strNumber1;

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
        {
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");

        }
        if (!IsPostBack)
        {
            
            loadDepartment(this.ddlDepartment_T);
            ddlDepartment_T.Enabled = false;

            DataTable dt = clsData.UploadNumber(Session["AppNo"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlDepartment_T.Text = dt.Rows[0]["Department"].ToString().Trim();
            }

            loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue);
            if (ddlDepartment_T.SelectedValue == "DA40")
                loadTeam(this.ddlTeam);
            else
            {
                ddlTeam.Items.Clear();
                ddlTeam.Items.Add("");
            }


            



        }
    }

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, strKind1);
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    protected void loadTestCase_Function(DropDownList DDL, string strID)
    {
        clsDropDownList.ddlApplication_TestCase_Function(DDL, strID);
    }

    protected void loadExplanation_Item(DropDownList DDL, string strID)
    {
        clsDropDownList.ddlExplanation_Item(DDL, strID);
    }

    protected void loadTestCase_Function1(DropDownList DDL, string strID)
    {
        clsDropDownList.ddlApplication_TestCase_Function1(DDL, strID);
    }

    protected void loadTestCase_Item(DropDownList DDL, string strID, string strFunctionID)
    {
        clsDropDownList.ddlApplication_TestCase_Item(DDL, strID, strFunctionID);
    }

    protected void loadTestCase_Kind(DropDownList DDL, string strDepartment)
    {
        clsDropDownList.ddlApplication_TestCase_Kind(DDL, strDepartment, "general");
    }

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlTeam(DDL, "0");
    }
    #endregion

    protected void ddlDepartment_T_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue);
        //loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue);
        //if (ddlDepartment_T.SelectedValue == "DA40")
        //    loadTeam(this.ddlTeam);
        //else
        //{
        //    ddlTeam.Items.Clear();
        //    ddlTeam.Items.Add("");
        //}

    }

    protected void ddlFileF_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlFileI, ddlFileK.SelectedValue, ddlFileF.SelectedValue);
    }

    protected void ddlFileF1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlFileI1, ddlFileK1.SelectedValue, ddlFileF1.SelectedValue);
    }

    protected void ddlFunction2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDKind_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestKind("", ddlKind.SelectedValue, "1") == true)
            clsMsg.AlertMessage("刪除成功！", this.Page);
    }
    protected void btnAKind_Click(object sender, EventArgs e)
    {
        //if ((txtKind.Text == "") || (ddlTeam.Text == ""))
        //    clsMsg.AlertMessage("請輸入類別及選擇負責Team！", this.Page);
        //else
        //{
        if (clsTransaction.InsertApplication_Kind(txtKind.Text, ddlTeam.Text, ddlDepartment_T.Text, "general") == true)
        {
            loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue);
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue);
            txtKind.Text = "";
        }
        else
            clsMsg.AlertMessage("新增失敗！", this.Page);
        //}
    }
    protected void btnDFunction_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestFunction(ddlKind1.SelectedValue, ddlFunction.Text,"0") == true)
        {
            loadTestCase_Function(this.ddlFunction, ddlKind1.SelectedValue);
            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
    }
    protected void btnAFunction_Click(object sender, EventArgs e)
    {
        int intCount;
        DataTable dt;

        dt = clsData.UploadApplication_TestFunctionMaxID(ddlKind2.SelectedValue);

        if (dt.Rows[0]["ID"].ToString() == "")
            intCount = 1;
        else
            intCount = Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1;


        if ((ddlKind2.Text == "") || (txtFunction.Text == ""))
            clsMsg.AlertMessage("請輸入類別及Function名稱！", this.Page);
        else
        {
            if (clsTransaction.InsertApplication_Function(intCount.ToString(), ddlKind2.SelectedValue, txtFunction.Text.Trim()) == true)
            {
                loadTestCase_Function(this.ddlFunction, ddlKind2.SelectedValue);
                txtFunction.Text = "";
                clsMsg.AlertMessage("新增成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
        }



    }

    protected void btnDItem_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestItem(ddlItem.SelectedValue, ddlKind3.SelectedValue, ddlFunction1.SelectedValue, ddlItem.SelectedItem.Text,"0") == true)
        {
            loadTestCase_Item(this.ddlItem, ddlKind3.SelectedValue, ddlFunction1.SelectedValue);
            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
    }

    protected void btnAItem_Click(object sender, EventArgs e)
    {
        int intCount;
        DataTable dt;

        dt = clsData.UploadApplication_TestItemMaxID(ddlKind4.SelectedValue, ddlFunction2.SelectedValue);

        if (dt.Rows[0]["ID"].ToString() == "")
            intCount = 1;
        else
            intCount = Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1;
        //intCount = dt.Rows.Count + 1;


        if ((ddlKind4.Text == "") || (ddlFunction2.Text == "") || (txtItem.Text == ""))
            clsMsg.AlertMessage("請輸入類別、Function名稱及項目！", this.Page);
        else
        {
            if (clsTransaction.InsertApplication_Item(intCount.ToString(), ddlKind4.SelectedValue, ddlFunction2.SelectedValue, txtItem.Text.Trim()) == true)
            {
                txtFunction.Text = "";
                clsMsg.AlertMessage("新增成功！", this.Page);
                if ((ddlKind3.Text != "") && (ddlFunction1.Text != ""))
                    loadTestCase_Item(this.ddlItem, ddlKind3.SelectedValue, ddlFunction1.SelectedValue);
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlItem1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadTestItem_File(ddlKind5.SelectedValue, ddlFunction3.SelectedValue, ddlItem1.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            ddlL1.Text = dt.Rows[0]["Level1"].ToString();
    }

    protected void ddlItem2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadTestItem_File(ddlKind6.SelectedValue, ddlFunction4.SelectedValue, ddlItem2.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            ddlL2.Text = dt.Rows[0]["Level2"].ToString();
    }

    protected void ddlItem3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadTestItem_File(ddlKind7.SelectedValue, ddlFunction5.SelectedValue, ddlItem3.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            txtNote.Text = dt.Rows[0]["Note"].ToString();
    }

    protected void ddlItem4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadTestItem_File(ddlKind8.SelectedValue, ddlFunction6.SelectedValue, ddlItem4.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            txtCost.Text = dt.Rows[0]["Cost"].ToString();
    }

    protected void ddlFileI_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;

        Session["FileN"] = "";
        Session["Upload_Kind"] = "Application_TestCase";

        Session["Application_K"] = ddlFileK.SelectedItem.Text;
        Session["Application_F"] = ddlFileF.SelectedItem.Text;
        Session["Application_I"] = ddlFileI.SelectedItem.Text;

        dt = clsData.UploadTestItem_File(ddlFileK.SelectedValue, ddlFileF.SelectedValue, ddlFileI.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            lblFileN.Text = dt.Rows[0]["File_Name"].ToString();
        else
            lblFileN.Text = "";

    }

    protected void ddlFileI1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;

        Session["FileN"] = "";
        Session["Upload_Kind"] = "Application_TestCase1";

        Session["Application_K1"] = ddlFileK1.SelectedItem.Text;
        Session["Application_F1"] = ddlFileF1.SelectedItem.Text;
        Session["Application_I1"] = ddlFileI1.SelectedItem.Text;

        dt = clsData.UploadTestItem_File(ddlFileK1.SelectedValue, ddlFileF1.SelectedValue, ddlFileI1.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            lblFileN1.Text = dt.Rows[0]["File_Name1"].ToString();
        else
            lblFileN1.Text = "";

    }

    protected void btnMFile1_Click(object sender, EventArgs e)
    {
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strToday;

        if (lblFileN1.Text != "")
        {
            DataTable dt = clsData.UploadTestItem_File(ddlFileK1.SelectedValue, ddlFileF1.SelectedValue, ddlFileI1.SelectedItem.Text);

            string path = dt.Rows[0]["File_Path"].ToString() + "\\" + dt.Rows[0]["File_Name"].ToString();
            //if (clsTransaction.DelUploadFilesCase(strName, "", "", "0") == true)
            //{
            File.Delete(path);
            //((GridView)sender).SelectedIndex = -1;
            //((GridView)sender).EditIndex = -1;
            //GvQuery();
            //clsMsg.AlertMessage("刪除成功！", this.Page);
            //}
            //if (dt.Rows.Count != 0)
            //    lblFileN.Text = dt.Rows[0]["File_Name"].ToString();
            //else
            //    lblFileN.Text = "";
        }
        //else
        //{
        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();
        }

        if ((strFile != null) || (strFile != ""))
        {
            string[] sArray = strFile.Split(',');
            foreach (string i in sArray)
            {
                if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                {
                    intFile = i.LastIndexOf('\\');
                    strPath = i.Substring(0, intFile);
                    strFile_Name = i.Substring(intFile + 1);

                    if (clsTransaction.UpDateApplicationCaseFile1(strFile_Name, strPath, ddlFileK1.SelectedValue, ddlFileF1.SelectedValue, ddlFileI1.SelectedItem.Text) == true)
                    {
                        Session["FileN"] = "";
                        //if (intAdd == 0)
                        //if (Session["ProjectKind"].ToString() == "驗証申請")
                        //{
                        //if (strPath.IndexOf("TestReport") > 0)

                        //}
                        clsMsg.AlertMessage("更新成功....", this.Page);
                        ddlFileK1.Items.Clear();
                        ddlFileF1.Items.Clear();
                        ddlFileI1.Items.Clear();
                        lblFileN1.Text = "";
                        loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue);
                    }
                    else
                    {
                        Session["FileN"] = "";
                        clsMsg.AlertMessage("更新失敗....", this.Page);
                    }
                }
            }
        }
        Session["FileN"] = "";
        //}


    }

    protected void ddlFileK1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFileF1, ddlFileK1.SelectedValue);
        ddlFileI1.Items.Clear();
        lblFileN1.Text = "";
    }

    protected void btnMFile_Click(object sender, EventArgs e)
    {
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strToday;

        if (lblFileN.Text != "")
        {
            DataTable dt = clsData.UploadTestItem_File(ddlFileK.SelectedValue, ddlFileF.SelectedValue, ddlFileI.SelectedItem.Text);

            string path = dt.Rows[0]["File_Path"].ToString() + "\\" + dt.Rows[0]["File_Name"].ToString();
            //if (clsTransaction.DelUploadFilesCase(strName, "", "", "0") == true)
            //{
            File.Delete(path);
            //((GridView)sender).SelectedIndex = -1;
            //((GridView)sender).EditIndex = -1;
            //GvQuery();
            //clsMsg.AlertMessage("刪除成功！", this.Page);
            //}
            //if (dt.Rows.Count != 0)
            //    lblFileN.Text = dt.Rows[0]["File_Name"].ToString();
            //else
            //    lblFileN.Text = "";
        }
        //else
        //{
        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();
        }

        if ((strFile != null) || (strFile != ""))
        {
            string[] sArray = strFile.Split(',');
            foreach (string i in sArray)
            {
                if ((i.ToString().Trim() != "") && (i.ToString().Trim() != null))
                {
                    intFile = i.LastIndexOf('\\');
                    strPath = i.Substring(0, intFile);
                    strFile_Name = i.Substring(intFile + 1);

                    if (clsTransaction.UpDateApplicationCaseFile(strFile_Name, strPath, ddlFileK.SelectedValue, ddlFileF.SelectedValue, ddlFileI.SelectedItem.Text) == true)
                    {
                        Session["FileN"] = "";
                        //if (intAdd == 0)
                        //if (Session["ProjectKind"].ToString() == "驗証申請")
                        //{
                        //if (strPath.IndexOf("TestReport") > 0)

                        //}
                        clsMsg.AlertMessage("更新成功....", this.Page);
                        ddlFileK.Items.Clear();
                        ddlFileF.Items.Clear();
                        ddlFileI.Items.Clear();
                        lblFileN.Text = "";
                        loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue);
                    }
                    else
                    {
                        Session["FileN"] = "";
                        clsMsg.AlertMessage("更新失敗....", this.Page);
                    }
                }
            }
        }
        Session["FileN"] = "";
        //}


    }

    protected void ddlFileK_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFileF, ddlFileK.SelectedValue);
        ddlFileI.Items.Clear();
        lblFileN.Text = "";
    }

    protected void btnNote_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplicationCaseNPI(txtNote.Text, ddlKind7.SelectedValue, ddlFunction5.SelectedValue, ddlItem3.SelectedItem.Text, "2") == true)
        {
            clsMsg.AlertMessage("更新成功....", this.Page);
            ddlKind7.Items.Clear();
            ddlFunction5.Items.Clear();
            ddlItem3.Items.Clear();
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue);
            txtNote.Text = "";
        }
        else
        {
            clsMsg.AlertMessage("更新失敗....", this.Page);
        }
    }

    protected void ddlFunction1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlItem, ddlKind3.SelectedValue, ddlFunction1.SelectedValue);
    }

    protected void ddlFunction3_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlItem1, ddlKind5.SelectedValue, ddlFunction3.SelectedValue);
    }

    protected void ddlFunction4_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlItem2, ddlKind6.SelectedValue, ddlFunction4.SelectedValue);
    }

    protected void ddlFunction5_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlItem3, ddlKind7.SelectedValue, ddlFunction5.SelectedValue);
    }

    protected void ddlFunction6_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Item(this.ddlItem4, ddlKind8.SelectedValue, ddlFunction6.SelectedValue);
    }

    protected void ddlKind1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function(this.ddlFunction, ddlKind1.SelectedValue);
    }

    protected void ddlKind2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlKind3_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFunction1, ddlKind3.SelectedValue);
        ddlItem.Items.Clear();
    }

    protected void ddlKind4_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFunction2, ddlKind4.SelectedValue);
    }

    protected void ddlKind5_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFunction3, ddlKind5.SelectedValue);
    }

    protected void ddlKind6_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFunction4, ddlKind6.SelectedValue);
    }

    protected void ddlKind7_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFunction5, ddlKind7.SelectedValue);
    }

    protected void ddlKind8_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFunction6, ddlKind8.SelectedValue);
    }

    protected void btnNPIL1_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplicationCaseNPI(ddlL1.Text, ddlKind5.SelectedValue, ddlFunction3.SelectedValue, ddlItem1.SelectedItem.Text, "0") == true)
        {
            clsMsg.AlertMessage("更新成功....", this.Page);
            ddlKind5.Items.Clear();
            ddlFunction3.Items.Clear();
            ddlItem1.Items.Clear();
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue);
        }
        else
        {
            clsMsg.AlertMessage("更新失敗....", this.Page);
        }
    }

    protected void btnNPIL2_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplicationCaseNPI(ddlL2.Text, ddlKind6.SelectedValue, ddlFunction4.SelectedValue, ddlItem2.SelectedItem.Text, "1") == true)
        {
            clsMsg.AlertMessage("更新成功....", this.Page);
            ddlKind6.Items.Clear();
            ddlFunction4.Items.Clear();
            ddlItem2.Items.Clear();
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue);
        }
        else
        {
            clsMsg.AlertMessage("更新失敗....", this.Page);
        }
    }

    protected void btnCost_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplicationCaseNPI(txtCost.Text, ddlKind8.SelectedValue, ddlFunction6.SelectedValue, ddlItem4.SelectedItem.Text, "3") == true)
        {
            clsMsg.AlertMessage("更新成功....", this.Page);
            ddlKind8.Items.Clear();
            ddlFunction6.Items.Clear();
            ddlItem4.Items.Clear();
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue);
            txtCost.Text = "";
        }
        else
        {
            clsMsg.AlertMessage("更新失敗....", this.Page);
        }
    }

    protected void btnTeam_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestKind(ddlTeam.Text, ddlKind.SelectedValue, "0") == true)
            clsMsg.AlertMessage("修改成功！", this.Page);
    }

    protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadTestCase_Function(this.ddlFunction, ddlKind.Text);
        string strTeam;
        DataTable dt;

        dt = clsData.UploadApplication_TestTeam(ddlKind.Text);
        if (dt.Rows.Count == 0)
            ddlTeam.Text = "";
        else
            ddlTeam.Text = dt.Rows[0]["Custodian_Team"].ToString();

    }

    
}
