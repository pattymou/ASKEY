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

public partial class WebForm_AddInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strNumber;
        //string[] strNumber1;

        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //Session["FileN"] = "";
            //Session["Upload_Kind"] = "Application_TestCase";
            ddlCustomer.Visible = false;
            lblCustomer.Visible = false;
            rdoInfo1.Checked = true;
            GvQuery();

            loadCustodian(this.ddlApparatusMaster);
            loadCustodian(this.ddlApparatusMaster1);
            loadCustodian(this.ddlApparatusMaster2);
            loadCustodian(this.ddlGoods);
            loadCustodian(this.ddlSample);
            loadCustodian(this.ddlGoodsW);
            loadCustodian(this.ddlSampleW);
            loadCustodian(this.ddlLos);
            loadCustodian(this.ddlMesh);
            loadCustodian(this.ddlOctoscope);
            loadCustodian(this.ddlVeriwave);
            loadCustodian(this.ddlAP);
            loadDepartment(this.ddlApparatusD);
            loadDepartment(this.ddlDepartment_T);
            //loadTeam(this.ddlTeam);
            loadModelWeb(this.ddlModel);
            //loadTestCase_Kind(this.ddlKind);
            //loadTestCase_Kind(this.ddlKind1);
            //loadTestCase_Kind(this.ddlKind2);
            //loadTestCase_Kind(this.ddlKind3);
            //loadTestCase_Kind(this.ddlKind4);
            //loadTestCase_Kind(this.ddlKind5);
            //loadTestCase_Kind(this.ddlKind6);
            //loadTestCase_Kind(this.ddlKind7);
            //loadTestCase_Kind(this.ddlKind8);
            //loadTestCase_Kind(this.ddlFileK);
            //loadTestCase_Kind(this.ddlFileK1);



            loadExplanation_Kind(this.ddlKind_E);
            loadExplanation_Kind(this.ddlKind1_E);
            loadExplanation_Kind(this.ddlFileK_E);
            loadExplanation_Kind(this.ddlKind2_E);



            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "0");

            ddlApparatusMaster.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A1T", "0");

            ddlApparatusMaster1.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A1W", "0");

            ddlApparatusMaster2.Text = dt1.Rows[0]["Name"].ToString();


            dt1 = clsData.UploadApparatusMasterQuery("A3T", "0");

            ddlGoods.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A3W", "0");

            ddlGoodsW.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A4T", "0");

            ddlSample.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A4W", "0");

            ddlSampleW.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");

            ddlApparatusD.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("A5DN", "0");

            txtDepartmentName.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("LosOwner", "0");

            ddlLos.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("VeriwaveOwner", "0");

            ddlVeriwave.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("OctoscopeOwner", "0");

            ddlOctoscope.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("MeshOwner", "0");

            ddlMesh.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadApparatusMasterQuery("APOwner", "0");

            ddlAP.Text = dt1.Rows[0]["Name"].ToString();

            dt1 = clsData.UploadWorkTimeQuery("A2S");
            strNumber = dt1.Rows[0]["Name"].ToString();
            string[] strNumber1 = strNumber.Split(':');
            ddlHourB.Text = strNumber1[0];
            ddlMinB.Text = strNumber1[1];

            dt1 = clsData.UploadWorkTimeQuery("A2E");
            strNumber = dt1.Rows[0]["Name"].ToString();
            strNumber1 = strNumber.Split(':');
            ddlHourR.Text = strNumber1[0];
            ddlMinR.Text = strNumber1[1];
            loadCustomer(this.ddlCustomer, "0");

            ddlDepartment_T.Text = "DA40";
            txtEFunction.Visible = false;
            btnEFunction.Visible = false;
            txtEItem.Visible = false;
            btnEItem.Visible = false;
        }
    }

    #region loadModelWeb
    protected void loadModelWeb(DropDownList DDL)
    {
        clsDropDownList.ddlModelWeb(DDL);
    }
    #endregion

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlTeam(DDL, "0");
    }
    #endregion

    #region loadCustodian
    protected void loadCustodian(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL, "0");
    }
    #endregion

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL, string strKind1)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, strKind1);
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //getTestPlan();
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strKind, strAdd, strValue;

        if (txtAdd.Text == "")
        {
            clsMsg.AlertMessage("請輸入欲新增項目！", this.Page);
        }
        else
        {
            strValue = "";
            strAdd = txtAdd.Text;
            if (rdoInfo1.Checked == true)
            {
                strKind = "1";

            }
            else if (rdoInfo2.Checked == true)
                strKind = "2";
            else if (rdoInfo3.Checked == true)
            {
                strKind = "3";
                strValue = txtLeader1.Text;
            }
            else if (rdoInfo4.Checked == true)
                strKind = "4";
            else if (rdoInfo5.Checked == true)
                strKind = "5";
            else if (rdoInfo6.Checked == true)
                strKind = "6";
            else if (rdoInfo7.Checked == true)
                strKind = "7";
            else if (rdoInfo8.Checked == true)
                strKind = "8";
            else if (rdoInfo9.Checked == true)
            {
                strKind = "9";
                strAdd = ddlCustomer.Text;
            }
            else
                strKind = "10";


            if (clsTransaction.InsertInfo(strKind, strAdd, strValue) == true)
            {
                if ((strKind == "1") || (strKind == "3") || (strKind == "5"))
                {
                    if (clsTransaction.InsertInfo_PND(strKind, strAdd, strValue) == false)
                    {
                        clsMsg.AlertMessage("新增失敗....", this.Page);
                    }
                }
                if (strKind == "9")
                    clsTransaction.InsertInfo_Product(strAdd, txtAdd.Text);

                clsMsg.AlertMessage("新增成功....", this.Page);
                this.GvQuery();
                txtAdd.Text = "";
            }
            else
            {
                clsMsg.AlertMessage("新增失敗....", this.Page);
            }
        }
        txtLeader1.Text = "";

    }

    protected void rdoInfo10_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }

    protected void rdoInfo9_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = true;
        lblCustomer.Visible = true;
        loadCustomer(this.ddlCustomer, "0");
        this.GvQuery();
        txtAdd.Text = "";
    }

    protected void rdoInfo8_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }

    protected void rdoInfo7_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }

    protected void rdoInfo6_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }

    protected void rdoInfo5_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }
    protected void rdoInfo4_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }
    protected void rdoInfo3_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
        txtLeader1.Text = "";
    }
    protected void rdoInfo2_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";
    }
    protected void rdoInfo1_CheckedChanged(object sender, EventArgs e)
    {
        ddlCustomer.Visible = false;
        lblCustomer.Visible = false;
        this.GvQuery();
        txtAdd.Text = "";

    }

    #region gvList_RowDeleting
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName = ((Label)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("lblName")).Text;
        string strID = ((Label)this.gvList.Rows[e.RowIndex].Cells[5].FindControl("lblGVSeq")).Text.Trim();
        string strKind;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        if (rdoInfo1.Checked == true)
            strKind = "1";
        else if (rdoInfo2.Checked == true)
            strKind = "2";
        else if (rdoInfo3.Checked == true)
            strKind = "3";
        else if (rdoInfo4.Checked == true)
            strKind = "4";
        else if (rdoInfo5.Checked == true)
            strKind = "5";
        else if (rdoInfo6.Checked == true)
            strKind = "6";
        else if (rdoInfo7.Checked == true)
            strKind = "7";
        else if (rdoInfo8.Checked == true)
            strKind = "8";
        else if (rdoInfo9.Checked == true)
            strKind = "9";
        else
            strKind = "10";
        if (rdoInfo9.Checked == true)
        {
            string strCustomer;
            strCustomer = ddlCustomer.Text;
            if (clsTransaction.DelInfo_Product(strCustomer, strName) != true)
                clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);

            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            GvQuery();
        }
        else
        {
            if (clsTransaction.DelInfo(strName, strID) == true)
            {
                if ((rdoInfo1.Checked == true) || (rdoInfo3.Checked == true) || (rdoInfo5.Checked == true))
                {
                    if (clsTransaction.DelInfo_PND(strName, strKind) == false)
                    {
                        clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
                    }
                }
                if (rdoInfo3.Checked == true)
                {
                    if (clsTransaction.DelDepartmentAccount(strName) != true)
                        clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
                }
                //File.Delete(path);
                ((GridView)sender).SelectedIndex = -1;
                ((GridView)sender).EditIndex = -1;
                GvQuery();
            }
            else
            {
                clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
            }
        }
    }
    #endregion

    #region gvList_RowUpdating (指定資料行更新)
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //DropDownList ddlA = ((DropDownList)this.gvList.Rows[e.RowIndex].Cells[4].FindControl("DropDownList1"));
        //string Code_No = ((Label)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("Label1")).Text;
        string Code_Name = ((Label)this.gvList.Rows[e.RowIndex].Cells[3].FindControl("lblName1")).Text;
        string Code_UName = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("txtName")).Text;

        string Code_LName = ((Label)this.gvList.Rows[e.RowIndex].Cells[6].FindControl("lblLeader1")).Text;
        string Code_LUName = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[5].FindControl("txtLeader")).Text;

        string strID = ((Label)this.gvList.Rows[e.RowIndex].Cells[5].FindControl("lblGVSeq")).Text.Trim();
        string strKind;

        if (rdoInfo1.Checked == true)
            strKind = "1";
        else if (rdoInfo2.Checked == true)
            strKind = "2";
        else if (rdoInfo3.Checked == true)
            strKind = "3";
        else if (rdoInfo4.Checked == true)
            strKind = "4";
        else if (rdoInfo5.Checked == true)
            strKind = "5";
        else if (rdoInfo6.Checked == true)
            strKind = "6";
        else if (rdoInfo7.Checked == true)
            strKind = "7";
        else if (rdoInfo8.Checked == true)
            strKind = "8";
        else if (rdoInfo9.Checked == true)
            strKind = "9";
        else
            strKind = "10";
        //string Code_Class = ((Label)this.gvList.Rows[e.RowIndex].Cells[5].FindControl("Label22")).Text;
        //string strCheck = "N";

        //if (ddlA.SelectedValue.Equals("Y"))
        //{
        //    strCheck = "Y";
        //}

        //if (clsTransaction.UpDateInfoData(Code_Name, Code_UName, Code_LUName) == true)
        if (clsTransaction.UpDateInfoData(strID, Code_UName, Code_LUName) == true)
        {
            if ((rdoInfo1.Checked == true) || (rdoInfo3.Checked == true) || (rdoInfo5.Checked == true))
            {
                if (clsTransaction.UpDateInfoData_PND(strKind, Code_UName, Code_LUName, Code_Name) == true)
                {
                    ((GridView)sender).SelectedIndex = -1;
                    ((GridView)sender).EditIndex = -1;
                    GvQuery();
                }
                else
                    clsMsg.AlertMessage("更新失敗，請洽IT人員！", this.Page);
            }
            else
            {
                ((GridView)sender).SelectedIndex = -1;
                ((GridView)sender).EditIndex = -1;
                GvQuery();
            }
        }
        else
        {
            clsMsg.AlertMessage("更新失敗，請洽IT人員！", this.Page);
        }
    }
    #endregion

    #region GvQuery
    private void GvQuery()
    {
        string strKind;
        DataTable dt;

        if (rdoInfo1.Checked == true)
            strKind = "1";
        else if (rdoInfo2.Checked == true)
            strKind = "2";
        else if (rdoInfo3.Checked == true)
            strKind = "3";
        else if (rdoInfo4.Checked == true)
            strKind = "4";
        else if (rdoInfo5.Checked == true)
            strKind = "5";
        else if (rdoInfo6.Checked == true)
            strKind = "6";
        else if (rdoInfo7.Checked == true)
            strKind = "7";
        else if (rdoInfo8.Checked == true)
            strKind = "8";
        else if (rdoInfo9.Checked == true)
            strKind = "9";
        else
            strKind = "10";

        //if (IsPage != true)
        //    this.gvList.PageIndex = 0;
        if (strKind == "9")
            dt = clsData.UploadInfoDataProductQuery(ddlCustomer.Text);
        else
            dt = clsData.UploadInfoDataQuery(int.Parse(strKind));
        this.gvList.DataSource = dt;
        this.DataBind();
    }
    #endregion

    #region gvList_RowEditing (指定資料行進行修改)
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ((GridView)sender).EditIndex = e.NewEditIndex;
        GvQuery();
    }
    #endregion

    #region gvList_PageIndexChanging
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    #region gvList_RowCancelingEdit (指定資料行取消修改)
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ((GridView)sender).SelectedIndex = -1;
        ((GridView)sender).EditIndex = -1;
        GvQuery();
    }
    #endregion

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (rdoInfo1.Checked == false)
        {
            e.Row.Cells[2].Visible = false;


        }
        if (rdoInfo3.Checked == false)
        {
            e.Row.Cells[5].Visible = false;
            lblLeader1.Visible = false;
            txtLeader1.Visible = false;
        }
        else
        {
            lblLeader1.Visible = true;
            txtLeader1.Visible = true;
        }

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string strNumber;


        if (clsTransaction.UpDateApparatusMasterData(ddlApparatusMaster.Text, "A1") == true)
        {
            strNumber = ddlHourB.Text + ":" + ddlMinB.Text;
            if (clsTransaction.UpDateApparatusMasterData(strNumber, "A2S") == true)
            {
                strNumber = ddlHourR.Text + ":" + ddlMinR.Text;
                if (clsTransaction.UpDateApparatusMasterData(strNumber, "A2E") == true)
                {
                    if (clsTransaction.UpDateApparatusMasterData(ddlApparatusD.Text, "A1D") == true)
                    {
                        if (clsTransaction.UpDateApparatusMasterData(ddlApparatusMaster1.Text, "A1T") == true)
                        {
                            if (clsTransaction.UpDateApparatusMasterData(ddlApparatusMaster2.Text, "A1W") == true)
                            {
                                if (clsTransaction.UpDateApparatusMasterData(txtDepartmentName.Text, "A5DN") == true)
                                {
                                    if (clsTransaction.UpDateApparatusMasterData(ddlGoods.Text, "A3T") == true)
                                    {
                                        if (clsTransaction.UpDateApparatusMasterData(ddlGoodsW.Text, "A3W") == true)
                                        {
                                            if (clsTransaction.UpDateApparatusMasterData(ddlSample.Text, "A4T") == true)
                                            {
                                                if (clsTransaction.UpDateApparatusMasterData(ddlSampleW.Text, "A4W") == true)
                                                    clsMsg.AlertMessage("修改成功....", this.Page);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                clsMsg.AlertMessage("修改失敗....", this.Page);
        }
        else
            clsMsg.AlertMessage("修改失敗....", this.Page);
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GvQuery();
    }

    protected void loadTestCase_Kind(DropDownList DDL, string strDepartment, string strApplication_Kind)
    {
        clsDropDownList.ddlApplication_TestCase_Kind(DDL, strDepartment, strApplication_Kind);
    }

    protected void loadExplanation_Kind(DropDownList DDL)
    {
        clsDropDownList.ddlExplanation_Kind(DDL);
    }

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

    protected void ddlFileK_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFileF, ddlFileK.SelectedValue);
        ddlFileI.Items.Clear();
        lblFileN.Text = "";
    }

    protected void ddlFileK1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTestCase_Function1(this.ddlFileF1, ddlFileK1.SelectedValue);
        ddlFileI1.Items.Clear();
        lblFileN1.Text = "";
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

        if (rdoNPI.Checked == true)
        {
            loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue, "general");
        }
        else
        {
            loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue, "NPI");
        }

        if (ddlDepartment_T.SelectedValue == "DA40")
            loadTeam(this.ddlTeam);
        else
        {
            ddlTeam.Items.Clear();
            ddlTeam.Items.Add("");
        }
    }

    protected void btnHKind_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestKind("", ddlKind.SelectedValue, "2") == true)
            clsMsg.AlertMessage("隱藏成功！", this.Page);

        if (rdoNPI.Checked == true)
        {
            loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue, "general");
            loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue, "general");
        }
        else
        {
            loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue, "NPI");
            loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue, "NPI");
        }

        if (ddlDepartment_T.SelectedValue == "DA40")
            loadTeam(this.ddlTeam);
        else
        {
            ddlTeam.Items.Clear();
            ddlTeam.Items.Add("");
        }
    }

    protected void btnAKind_Click(object sender, EventArgs e)
    {
        //if ((txtKind.Text == "") || (ddlTeam.Text == ""))
        //    clsMsg.AlertMessage("請輸入類別及選擇負責Team！", this.Page);
        //else
        //{
        if ((rdoNPI.Checked == false) && (rdoNPI1.Checked ==false ))
        {
            clsMsg.AlertMessage("請選擇申請單種類！", this.Page);
        }
        else
        {
            string strAKind;
            if (rdoNPI.Checked == true)
                strAKind = "general";
            else
                strAKind = "NPI";
            if (clsTransaction.InsertApplication_Kind(txtKind.Text, ddlTeam.Text, ddlDepartment_T.Text, strAKind) == true)
            {
                loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, strAKind);
                txtKind.Text = "";
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
        }
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

    protected void btnHFunction_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestFunction(ddlKind1.SelectedValue, ddlFunction.Text,"1") == true)
        {
            loadTestCase_Function(this.ddlFunction, ddlKind1.SelectedValue);
            clsMsg.AlertMessage("隱藏成功！", this.Page);
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
        ddlKind2.Text = "";


    }

    protected void btnDItem_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestItem(ddlItem.SelectedValue, ddlKind3.SelectedValue, ddlFunction1.SelectedValue, ddlItem.SelectedItem.Text,"0") == true)
        {
            loadTestCase_Item(this.ddlItem, ddlKind3.SelectedValue, ddlFunction1.SelectedValue);
            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
    }

    protected void btnHItem_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestItem(ddlItem.SelectedValue, ddlKind3.SelectedValue, ddlFunction1.SelectedValue, ddlItem.SelectedItem.Text,"1") == true)
        {
            loadTestCase_Item(this.ddlItem, ddlKind3.SelectedValue, ddlFunction1.SelectedValue);
            clsMsg.AlertMessage("隱藏成功！", this.Page);
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
        ddlKind4.Text = "";
        ddlFunction2.Text = "";
        txtItem.Text = "";
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

    protected void btnTeam_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplication_TestKind(ddlTeam.Text, ddlKind.SelectedValue, "0") == true)
            clsMsg.AlertMessage("修改成功！", this.Page);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        if (clsTransaction.DelModelWeb(ddlModel.SelectedValue, "0") == true)
        {
            if (clsTransaction.DelModelWeb(ddlModel.SelectedValue, "1") == true)
            {
                loadModelWeb(this.ddlModel);
                clsMsg.AlertMessage("刪除成功！", this.Page);
            }
        }
    }
    protected void btnAddModel_Click(object sender, EventArgs e)
    {
        string strParent, strID, strName, strSequence, strWeb;

        DataTable dt = clsData.UploadFunction_List("0");

        strParent = (Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1).ToString();
        strID = strParent + "0";
        strName = txtModel.Text.Trim();
        dt = clsData.UploadFunction_List("1");
        strSequence = (Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1).ToString();
        strWeb = "ProjectView.aspx?Fun=" + strParent;

        if (clsTransaction.InsertModelWeb(strParent, "0", strName, "", "Y", strSequence, "Y") == true)
        {
            if (clsTransaction.InsertModelWeb(strID, strParent, "檢視" + strName, strWeb, "", "", "") == true)
            {
                strID = (Convert.ToInt32(strID) + 1).ToString();
                strWeb = "GanttProject.aspx?Fun=" + strParent;
                if (clsTransaction.InsertModelWeb(strID, strParent, "甘特圖", strWeb, "", "", "") == true)
                {
                    strID = (Convert.ToInt32(strID) + 1).ToString();
                    strWeb = "StatisticsCase.aspx?Fun=" + strParent;
                    if (clsTransaction.InsertModelWeb(strID, strParent, "案件統計", strWeb, "", "", "") == true)
                    {
                        clsMsg.AlertMessage("新增成功！", this.Page);
                        loadModelWeb(this.ddlModel);
                        txtModel.Text = "";
                    }
                }
            }
        }
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
                        string strAKind;
                        if (rdoNPI.Checked == true)
                            strAKind = "general";
                        else
                            strAKind = "NPI";

                        loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue, strAKind);
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

    protected void btnNPIL1_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplicationCaseNPI(ddlL1.Text, ddlKind5.SelectedValue, ddlFunction3.SelectedValue, ddlItem1.SelectedItem.Text, "0") == true)
        {
            clsMsg.AlertMessage("更新成功....", this.Page);
            ddlKind5.Items.Clear();
            ddlFunction3.Items.Clear();
            ddlItem1.Items.Clear();
            string strAKind;
            if (rdoNPI.Checked == true)
                strAKind = "general";
            else
                strAKind = "NPI";
            loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, strAKind);
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
            string strAKind;
            if (rdoNPI.Checked == true)
                strAKind = "general";
            else
                strAKind = "NPI";
            loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, strAKind);
        }
        else
        {
            clsMsg.AlertMessage("更新失敗....", this.Page);
        }
    }

    protected void btnNote_Click(object sender, EventArgs e)
    {
        if (clsTransaction.UpDateApplicationCaseNPI(txtNote.Text, ddlKind7.SelectedValue, ddlFunction5.SelectedValue, ddlItem3.SelectedItem.Text, "2") == true)
        {
            clsMsg.AlertMessage("更新成功....", this.Page);
            ddlKind7.Items.Clear();
            ddlFunction5.Items.Clear();
            ddlItem3.Items.Clear();
            string strAKind;
            if (rdoNPI.Checked == true)
                strAKind = "general";
            else
                strAKind = "NPI";
            loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, strAKind);
            txtNote.Text = "";
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

            string strAKind;
            if (rdoNPI.Checked == true)
                strAKind = "general";
            else
                strAKind = "NPI";
            loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, strAKind);
            txtCost.Text = "";
        }
        else
        {
            clsMsg.AlertMessage("更新失敗....", this.Page);
        }
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

                        string strAKind;
                        if (rdoNPI.Checked == true)
                            strAKind = "general";
                        else
                            strAKind = "NPI";
                        loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue, strAKind);
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

    protected void btnDKind_E_Click(object sender, EventArgs e)
    {
        if (clsTransaction.DelExplanation_Kind(ddlKind_E.SelectedValue) == true)
            clsMsg.AlertMessage("刪除成功！", this.Page);
    }

    protected void btnAKind_E_Click(object sender, EventArgs e)
    {
        if (clsTransaction.InsertExplanation_Kind(txtKind_E.Text) == true)
        {
            loadExplanation_Kind(this.ddlKind_E);
            loadExplanation_Kind(this.ddlKind1_E);
            loadExplanation_Kind(this.ddlKind2_E);
            loadExplanation_Kind(this.ddlFileK_E);
            txtKind.Text = "";
        }
        else
            clsMsg.AlertMessage("新增失敗！", this.Page);
    }
    protected void ddlKind1_E_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadExplanation_Item(this.ddlItem_E, ddlKind1_E.SelectedValue);
    }

    protected void btnDItem_E_Click(object sender, EventArgs e)
    {
        if (clsTransaction.DelExplanation_Item(ddlItem_E.SelectedIndex.ToString(), ddlKind1_E.SelectedValue) == true)
        {
            loadExplanation_Item(this.ddlItem_E, ddlKind1_E.SelectedValue);
            clsMsg.AlertMessage("刪除成功！", this.Page);
        }
    }
    protected void btnAItem_E_Click(object sender, EventArgs e)
    {
        int intCount;
        DataTable dt;

        dt = clsData.UploadExplanation_ItemMaxID(ddlKind2_E.SelectedValue);

        if (dt.Rows[0]["ID"].ToString() == "")
            intCount = 1;
        else
            intCount = Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1;


        if ((ddlKind2_E.Text == "") || (txtItem_E.Text == ""))
            clsMsg.AlertMessage("請輸入類別及Function名稱！", this.Page);
        else
        {
            if (clsTransaction.InsertExplanation_Item(intCount.ToString(), ddlKind2_E.SelectedValue, txtItem_E.Text.Trim()) == true)
            {
                loadExplanation_Item(this.ddlItem_E, ddlKind1_E.SelectedValue);
                txtFunction.Text = "";
                clsMsg.AlertMessage("新增成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
        }
    }
    protected void btnMFile_E_Click(object sender, EventArgs e)
    {
        string strFile = "";
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strToday;

        if (lblFileN.Text != "")
        {
            DataTable dt = clsData.UploadExplanation_Item(ddlFileK_E.SelectedValue, ddlFileI_E.SelectedItem.Text);

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

                    if (clsTransaction.UpDateExplanationFile(strFile_Name, strPath, ddlFileK_E.SelectedValue, ddlFileI_E.SelectedItem.Text) == true)
                    {
                        Session["FileN"] = "";
                        //if (intAdd == 0)
                        //if (Session["ProjectKind"].ToString() == "驗証申請")
                        //{
                        //if (strPath.IndexOf("TestReport") > 0)

                        //}
                        clsMsg.AlertMessage("更新成功....", this.Page);
                        ddlFileK_E.Items.Clear();
                        ddlFileI_E.Items.Clear();
                        lblFileN_E.Text = "";
                        loadExplanation_Kind(this.ddlFileK_E);
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
    protected void ddlKind_E_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlKind2_E_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlFileK_E_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadExplanation_Item(this.ddlFileI_E, ddlFileK_E.SelectedValue);

    }
    protected void ddlFileI_E_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;

        Session["FileN"] = "";
        Session["Upload_Kind"] = "Explanation";

        Session["Explanation_K"] = ddlFileK_E.SelectedItem.Text;

        Session["Explanation_I"] = ddlFileI_E.SelectedItem.Text;

        dt = clsData.UploadExplanation_File(ddlFileK_E.SelectedValue, ddlFileI_E.SelectedItem.Text);

        if (dt.Rows.Count != 0)
            lblFileN_E.Text = dt.Rows[0]["File_Name"].ToString();
        else
            lblFileN_E.Text = "";
    }

    protected void btnEItem_Click(object sender, EventArgs e)
    {
        string path_New = "", path_New1 = "";
        int intKind = 0, intKind1 = 0;
        if (txtEItem.Text != "")
        {
            DataTable dt = clsData.UploadTestItem_File(ddlKind3.SelectedValue, ddlFunction1.SelectedValue, ddlItem.SelectedItem.Text);

            if ((dt.Rows[0]["File_Path"].ToString() != null) && (dt.Rows[0]["File_Path"].ToString() != ""))
            {
                string[] path = dt.Rows[0]["File_Path"].ToString().Split('\\');
                for (int intX = 0; intX < path.Length; intX++)
                {
                    if (path[intX] == dt.Rows[0]["Item"].ToString())
                    {
                        path[intX] = txtEItem.Text;
                    }
                    path_New = path_New + path[intX] + "\\";
                }
                intKind = 1;
                path_New = path_New.Remove(path_New.Length - 1, 1);
            }

            if ((dt.Rows[0]["File_Path1"].ToString() != null) && (dt.Rows[0]["File_Path1"].ToString() != ""))
            {
                string[] path = dt.Rows[0]["File_Path1"].ToString().Split('\\');
                for (int intX = 0; intX < path.Length; intX++)
                {
                    if (path[intX] == dt.Rows[0]["Item"].ToString())
                    {
                        path[intX] = txtEItem.Text;
                    }
                    path_New1 = path_New1 + path[intX] + "\\";
                }
                intKind1 = 1;
                path_New1 = path_New1.Remove(path_New1.Length - 1, 1);
            }

            if (clsTransaction.UpDateApplication_Item(ddlItem.SelectedItem.Text, ddlKind3.SelectedValue, ddlFunction1.SelectedValue, txtEItem.Text, intKind, intKind1, path_New, path_New1) == true)
            {
                string strAKind;
                if (rdoNPI.Checked == true)
                    strAKind = "general";
                else
                    strAKind = "NPI";
                loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, strAKind);
                loadTestCase_Function1(this.ddlFunction1, ddlKind3.SelectedValue);
                ddlItem.Items.Clear();

                txtKind.Text = "";

                clsMsg.AlertMessage("修改成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("修改失敗！", this.Page);
        }
        else
            clsMsg.AlertMessage("修改失敗！", this.Page);

    }

    protected void btnEFunction_Click(object sender, EventArgs e)
    {
        if (txtEFunction.Text != "")
        {
            if (clsTransaction.UpDateApplication_Function(ddlFunction.SelectedItem.Text, ddlKind1.SelectedValue, txtEFunction.Text) == true)
            {
                loadTestCase_Function(this.ddlFunction, ddlKind1.SelectedValue);
                loadTestCase_Function1(this.ddlFunction1, ddlKind3.SelectedValue);
                loadTestCase_Function1(this.ddlFunction2, ddlKind4.SelectedValue);
                ddlItem.Items.Clear();
                txtKind.Text = "";

                clsMsg.AlertMessage("修改成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("修改失敗！", this.Page);
        }
        else
            clsMsg.AlertMessage("修改失敗！", this.Page);

    }

    protected void btnOK_Auto_Click(object sender, EventArgs e)
    {


        if (clsTransaction.UpDateApparatusMasterData(ddlLos.Text, "LosOwner") == true)
        {
            if (clsTransaction.UpDateApparatusMasterData(ddlVeriwave.Text, "VeriwaveOwner") == true)
            {
                if (clsTransaction.UpDateApparatusMasterData(ddlOctoscope.Text, "OctoscopeOwner") == true)
                {
                    if (clsTransaction.UpDateApparatusMasterData(ddlMesh.Text, "MeshOwner") == true)
                    {
                        if (clsTransaction.UpDateApparatusMasterData(ddlAP.Text, "APOwner") == true)
                        {
                            clsMsg.AlertMessage("修改成功....", this.Page);
                        }
                        else
                            clsMsg.AlertMessage("修改失敗....", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("修改失敗....", this.Page);
                }
                else
                    clsMsg.AlertMessage("修改失敗....", this.Page);
            }
            else
                clsMsg.AlertMessage("修改失敗....", this.Page);
        }
        else
            clsMsg.AlertMessage("修改失敗....", this.Page);
    }

    protected void rdoNPI_CheckedChanged(object sender, EventArgs e)
    {
        loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue,"general");
        loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue,"general");
        if (ddlDepartment_T.SelectedValue == "DA40")
            loadTeam(this.ddlTeam);
        else
        {
            ddlTeam.Items.Clear();
            ddlTeam.Items.Add("");
        }
    }

    protected void rdoNPI1_CheckedChanged(object sender, EventArgs e)
    {
        loadTestCase_Kind(this.ddlKind, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind1, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind2, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind3, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind4, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind5, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind6, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind7, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlKind8, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlFileK, ddlDepartment_T.SelectedValue, "NPI");
        loadTestCase_Kind(this.ddlFileK1, ddlDepartment_T.SelectedValue, "NPI");
        if (ddlDepartment_T.SelectedValue == "DA40")
            loadTeam(this.ddlTeam);
        else
        {
            ddlTeam.Items.Clear();
            ddlTeam.Items.Add("");
        }
    }
}
