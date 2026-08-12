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

public partial class WebForm_AddApparatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string strToday;
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            string strToday;
            Session["FileN"] = "";
            //strToday = DateTime.Now.ToString("yyyyMMddHHmmss");
            //clsParameter.strApparatusID = "A" + strToday;
            loadKind(this.ddlKind);
            //loadDepartment(this.ddlDepartment);
            loadEmployees(this.ddlCustodian);
            loadEmployees(this.ddlCustodian1);
            //loadCustodian(this.ddlCustodian);
            //HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
            //cookie_Upload_Kind.Value = Server.UrlEncode("Apparatus");
            ////cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_Upload_Kind);
            Session["Upload_Kind"] = "Apparatus";
            //clsParameter.strUpload_Kind = "Apparatus";
            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

            Session["ApparatusID"] = "A" + strToday;

            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "0");
            string strMaster = dt1.Rows[0]["Name"].ToString();
            dt1 = clsData.UploadLeader("1", "", "");
            //string strLeader = dt1.Rows[0]["Name_En"].ToString();
            string strLeader = "";
            for (int intI = 0; intI < dt1.Rows.Count; intI++)
            {
                strLeader = strLeader + "," + dt1.Rows[intI]["Name_En"].ToString();
            }

            if (Session["EmpName"].ToString() == strMaster)
            {
                Cost.Visible = true;
                UseYear.Visible = true;
                UseDays.Visible = true;
                UsePrice.Visible = true;

            }
            else
            {
                if (strLeader.IndexOf(Session["EmpName"].ToString()) != -1)
                {
                    Cost.Visible = true;
                    UseYear.Visible = true;
                    UseDays.Visible = true;
                    UsePrice.Visible = true;
                }
                else
                {
                    Cost.Visible = false;
                    UseYear.Visible = false;
                    UseDays.Visible = false;
                    UsePrice.Visible = false;
                }

            }
        }
    }

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "0");
    }
    #endregion

    protected void ddlKind_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        //string strToday;
        string  strInspection,strMaintenance;
        DateTime dt;
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strFile = "";
        string strRStatus;

        //strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

        //Session["ApparatusID"] = "A" + strToday;

        //HttpCookie cookie_ApparatusID = new HttpCookie("ApparatusID");
        //cookie_ApparatusID.Value = Server.UrlEncode("A" + strToday);
        ////cookie_ApparatusID.Expires = DateTime.Now.AddDays(1);
        //Response.Cookies.Add(cookie_ApparatusID);

        //string strApparatusID_Cookie;
        //cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        //clsParameter.strApparatusID = "A" + strToday;
        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();

        }

        if ((txtName.Text.Trim() == "") || (ddlKind.Text == "") || (ddlCustodian.Text == ""))
        {
            Session["FileN"] = "";
            strFile = "";
            clsMsg.AlertMessage("*為必填欄位....", this.Page);
        }
        else
        {
            strInspection = Request["date1"].ToString();
            if (strInspection != "")
            {
                dt = Convert.ToDateTime(strInspection);
                strInspection = dt.ToString("yyyyMMdd");
            }

            strMaintenance = Request["date2"].ToString();
            if (strMaintenance != "")
            {
                dt = Convert.ToDateTime(strMaintenance);
                strMaintenance = dt.ToString("yyyyMMdd");
            }

            //================0217
            //if (chkReservation.Checked == true)
            //    strRStatus = "閒置中";
            //else
            //    strRStatus = "不可借用";   
            //=======================0217
            strRStatus = ddlStatus.Text;

            string strCost, strYears, strDays, strPrice;
            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "0");
            string strMaster = dt1.Rows[0]["Name"].ToString();
            //if (Session["EmpName"].ToString() == strMaster)
            //{
            //    strCost = txtCost.Text;
            //    strYears = txtUseYear.Text;
            //    strDays = txtUseDays.Text;
            //    strPrice = txtUsePrice.Text;
            //}
            //else
            //{
            //    strCost = "";
            //    strYears = "";
            //    strDays = "";
            //    strPrice = "";
            //}

            string strLeader = "";
            for (int intI = 0; intI < dt1.Rows.Count; intI++)
            {
                strLeader = strLeader + "," + dt1.Rows[0]["Name"].ToString();
            }

            if (Session["EmpName"].ToString() == strMaster)
            {
                strCost = txtCost.Text;
                strYears = txtUseYear.Text;
                strDays = txtUseDays.Text;
                strPrice = txtUsePrice.Text;

            }
            else
            {
                if (strLeader.IndexOf(Session["EmpName"].ToString()) != -1)
                {
                    strCost = txtCost.Text;
                    strYears = txtUseYear.Text;
                    strDays = txtUseDays.Text;
                    strPrice = txtUsePrice.Text;
                }
                else
                {
                    strCost = "";
                    strYears = "";
                    strDays = "";
                    strPrice = "";
                }

            }

            if (clsTransaction.InsertApparatus(Session["ApparatusID"].ToString(), txtProductID.Text.Trim(), txtName.Text.Trim(), ddlKind.Text, txtPart_No.Text.Trim(), txtBrand.Text.Trim(), txtModel.Text.Trim(), txtNumber.Text.Trim(), txtIMEI.Text.Trim(), strInspection, strMaintenance, txtPlace.Text.Trim(), ddlCustodian.Text, ddlDepartment.Text, txtFeature.Text, txtSpec.Text, txtNote.Text, strRStatus, ddlOS.Text, txtVR.Text.Trim(), ddlCustodian1.Text,txtName_En.Text.Trim(),txtMF.Text.Trim(),txtProcurement_staff.Text.Trim(),txtMF_Number.Text.Trim(),strCost,strYears,strDays,strPrice) == true)
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
                            clsTransaction.InsertUploadFile_Apparatus(Session["ApparatusID"].ToString(), strFile_Name, strPath);
                        }
                    }
                    clsMsg.AlertMessage("新增成功！", this.Page);
                }
                else
                {
                    clsMsg.AlertMessage("新增成功！", this.Page);
                }
                string strDirectory;

                strDirectory = @"d:\Apparatus\" + Session["ApparatusID"].ToString() + @"\";
                if (!Directory.Exists(strDirectory))  // 若目錄不存在則建立之
                {
                    Directory.CreateDirectory(strDirectory);
                }
                setEmpty();
            }
            else
            {
                clsMsg.AlertMessage("新增失敗....", this.Page);
            }

        }
        Session["FileN"] = "";

        string strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

        Session["ApparatusID"] = "A" + strToday;
        //setEmpty();

    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SearchApparatus.aspx");
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7,"0");
    }
    #endregion 

    #region loadCustodian
    protected void loadCustodian(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL,"0");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "0");
    }
    #endregion

    private void setEmpty()
    {
        txtProductID.Text = "";
        txtName.Text = "";
        txtBrand.Text = "";
        txtModel.Text = "";
        txtNumber.Text = "";
        txtPlace.Text = "";
        txtFeature.Text = "";
        txtSpec.Text = "";
        txtNote.Text = "";
        //txtCustodian.Text = "";
        ddlKind.Text = "";
        ddlDepartment.Text = "";
        //txtCustodianExt.Text = "";
        txtName_En.Text = "";
        txtMF.Text = "";
        txtMF_Number.Text = "";
        txtProcurement_staff.Text = "";
        txtCost.Text = "";
        txtUseYear.Text = "";
        txtUseDays.Text = "";
        txtUsePrice.Text = "";



    }
}
