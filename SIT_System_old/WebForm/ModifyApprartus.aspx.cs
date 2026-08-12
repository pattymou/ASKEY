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

public partial class WebForm_ModifyApprartus : System.Web.UI.Page
{
    public static string strDate1;
    public static string strDate2;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            Session["FileN"] = "";
            loadKind(this.ddlKind);
            loadEmployees(this.ddlCustodian);
            loadEmployees(this.ddlCustodian1);
            //loadCustodian(this.ddlCustodian);
            //HttpCookie cookie_Upload_Kind = new HttpCookie("Upload_Kind");
            //cookie_Upload_Kind.Value = Server.UrlEncode("Apparatus");
            ////cookie_Upload_Kind.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_Upload_Kind);
            Session["Upload_Kind"] = "Apparatus";
            //clsParameter.strUpload_Kind = "Apparatus";
            //HttpCookie cookie_ApparatusID = new HttpCookie("ApparatusID");
            //cookie_ApparatusID.Value = Server.UrlEncode(Request.QueryString["ID"]);
            ////cookie_ApparatusID.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_ApparatusID);
            Session["ApparatusID"] = Request.QueryString["ID"];
            //clsParameter.strApparatusID = Request.QueryString["ID"];
            //clsParameter.strApparatusID = "4";
            getApparatus();
        }
        //clsParameter.strUpload_Kind = "Apparatus";
        //clsParameter.strApparatusID = "4";

    }

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "0");
    }
    #endregion

    #region loadCustomer
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

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName, strPath;

        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        //strName = ((HyperLink)this.gvwMain.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblName")).Text;
        //string path = @"C:/test/" + strName;
        string path = @"D:\Apparatus\" + Request.QueryString["ID"].ToString() + @"\" + strName;
        if (clsTransaction.DelApparatusFiles(strName, Request.QueryString["ID"].ToString()) == true)
        {
            System.IO.File.Delete(path);
            path = Server.MapPath(".") + @"\pic\" + strName;
            File.Delete(path);
            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            GvQuery();
            clsMsg.AlertMessage("刪除成功！", this.Page);

        }
        else
        {
            clsMsg.AlertMessage("刪除失敗！", this.Page);
        }
    }
    #endregion

    private void GvQuery()
    {
        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        DataTable dt = clsData.UploadApparatusFileQuery(Session["ApparatusID"].ToString(), "2");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging (換頁)
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strInspection, strMaintenance;
        DateTime dt;
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strFile = "";
        string strRStatus;

        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

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

            //========0217
            //if (chkReservation.Checked == true)
                strRStatus = ddlStatus.Text;
            //else
            //{
            //    if ((ddlStatus.Text == "校驗中") || (ddlStatus.Text == "異常維修中"))
            //        strRStatus = ddlStatus.Text;
            //    else 
            //        strRStatus = "不可借用";
            //}
            //========0217

            if (clsTransaction.UpDateApparatus(Session["ApparatusID"].ToString(), txtProductID.Text.Trim(), txtName.Text.Trim(), ddlKind.Text, txtPart_No.Text.Trim(), txtBrand.Text.Trim(), txtModel.Text.Trim(), txtNumber.Text.Trim(), txtIMEI.Text.Trim(), strInspection, strMaintenance, txtPlace.Text.Trim(), ddlCustodian.Text, txtFeature.Text, txtSpec.Text, txtNote.Text, strRStatus, ddlOS.Text, txtVR.Text.Trim(), ddlCustodian1.Text,txtName_En.Text.Trim(),txtMF.Text.Trim(),txtProcurement_staff.Text.Trim(),txtMF_Number.Text.Trim(),txtCost.Text.Trim(),txtUseYear.Text.Trim(),txtUseDays.Text.Trim(),txtUsePrice.Text.Trim(),ddlDepartment.Text) == true)
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
                    clsMsg.AlertMessage("修改成功！", this.Page);
                    
                    //GvQuery();
                }
                else
                    clsMsg.AlertMessage("修改成功！", this.Page);

                getDate();
            }
            else
            {
                clsMsg.AlertMessage("修改失敗....", this.Page);
            }

        }
        Session["FileN"] = "";

        getApparatus();
        //setEmpty();
    }

    private void getDate()
    {
        DateTime dt1;
        string strDate11;

        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        DataTable dt = clsData.UploadApparatusQuery(Session["ApparatusID"].ToString(), "1", "");
        if ((strDate1 == "") || (strDate1 == null))
        {
            dt1 = Convert.ToDateTime(dt.Rows[0]["InspectionDate"].ToString());

            strDate11 = dt1.ToString("yyyy/MM/dd");
            if (strDate11 == "1900/01/01")
                strDate11 = "";
            else
                strDate1 = strDate11;
        }
        if ((strDate2 == "") || (strDate2 == null))
        {
            dt1 = Convert.ToDateTime(dt.Rows[0]["MaintenanceDate"].ToString());

            strDate11 = dt1.ToString("yyyy/MM/dd");
            if (strDate11 == "1900/01/01")
                strDate11 = "";
            else
                strDate2 = strDate11;
        }
    }

    private void getApparatus()
    {
        //string strFile;
        DateTime dt1;
        //string[] FileNames = New string[count];
        int intI = 0;
        string strDate11;
        string strRStatus;

        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        DataTable dt = clsData.UploadApparatusQuery(Session["ApparatusID"].ToString(), "1", "");
        txtProductID.Text = dt.Rows[0]["products_id"].ToString();
        txtName.Text = dt.Rows[0]["name"].ToString();
        txtBrand.Text = dt.Rows[0]["brand"].ToString();
        txtModel.Text = dt.Rows[0]["model"].ToString();
        txtNumber.Text = dt.Rows[0]["number"].ToString();
        txtIMEI.Text = dt.Rows[0]["IMEI"].ToString();
        txtVR.Text = dt.Rows[0]["OS_VR"].ToString();
        strRStatus = dt.Rows[0]["ReservationStatus"].ToString();
        txtPart_No.Text = dt.Rows[0]["Part_No"].ToString();
        txtName_En.Text = dt.Rows[0]["Name_En"].ToString();
        txtProcurement_staff.Text = dt.Rows[0]["Procurement_staff"].ToString();
        txtMF_Number.Text = dt.Rows[0]["MF_Number"].ToString();
        txtMF.Text = dt.Rows[0]["MF"].ToString();

        txtCost.Text = dt.Rows[0]["Cost_Price"].ToString();
        txtUseYear.Text = dt.Rows[0]["Years_Use"].ToString();
        txtUseDays.Text = dt.Rows[0]["Days_Use"].ToString();
        txtUsePrice.Text = dt.Rows[0]["Price_Use"].ToString();
        ddlDepartment.Text = dt.Rows[0]["Custodian_Department"].ToString();
        //if (dt.Rows[0]["ReservationStatus"].ToString() == "不可借用")
        //    ddlStatus.Text = "閒置中";
        //else
        //    ddlStatus.Text = dt.Rows[0]["ReservationStatus"].ToString();
        //if (strRStatus == "Y")
        //    chkReservation.Checked = true;
        //else
        //    chkReservation.Checked = false;
        //if (strRStatus == "不可借用")
        //{
        //    ddlStatus.Text = "閒置中";
        //    chkReservation.Checked = false;
        //}
        //else
        //{
            ddlStatus.Text = dt.Rows[0]["ReservationStatus"].ToString();
        //    chkReservation.Checked = true;
        //}
        //dt1 = dt.Rows[0]["InspectionDate"].ToString();

        if ((strDate1 == "") || (strDate1 == null))
        {
            dt1 = Convert.ToDateTime(dt.Rows[0]["InspectionDate"].ToString());

            strDate11 = dt1.ToString("yyyy/MM/dd");
            if (strDate11 == "1900/01/01")
                strDate11 = "";
            else
                strDate1 = strDate11;
        }
        if ((strDate2 == "") || (strDate2 == null))
        {
            dt1 = Convert.ToDateTime(dt.Rows[0]["MaintenanceDate"].ToString());

            strDate11 = dt1.ToString("yyyy/MM/dd");
            if (strDate11 == "1900/01/01")
                strDate11 = "";
            else
                strDate2 = strDate11;
        }

        ddlOS.Text = dt.Rows[0]["OS"].ToString();
        txtPlace.Text = dt.Rows[0]["Place"].ToString();
        ddlKind.Text = dt.Rows[0]["Kind"].ToString();
        if (ddlKind.Text == "")
            ddlKind.Text = "";
        //txtCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        //txtFeature.Text = dt.Rows[0]["Feature"].ToString();
        //txtSpec.Text = dt.Rows[0]["Spec"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        txtFeature.Text = dt.Rows[0]["Feature"].ToString();
        txtSpec.Text = dt.Rows[0]["Spec"].ToString();

        //DataTable dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());

        ListItem item = ddlCustodian.Items.FindByValue(dt.Rows[0]["Custodian"].ToString());
        if (item != null)
        {
            ddlCustodian.SelectedValue = dt.Rows[0]["Custodian"].ToString();
        }

        item = ddlCustodian1.Items.FindByValue(dt.Rows[0]["Agent"].ToString());
        if (item != null)
        {
            ddlCustodian1.SelectedValue = dt.Rows[0]["Agent"].ToString();
        }

        //if (dt.Rows[0]["Custodian"].ToString() != "")
        //    ddlCustodian.Text = dt.Rows[0]["Custodian"].ToString();

        //dt2 = clsData.getEmployees("1", dt.Rows[0]["Agent"].ToString());
        //if (dt.Rows[0]["Agent"].ToString() != "")
        //    ddlCustodian1.Text = dt.Rows[0]["Agent"].ToString();


        dt = clsData.UploadApparatusFileQuery(Session["ApparatusID"].ToString(), "0");
        string strPath1 = Server.MapPath(".") + @"\pic";
        string strPath2;
        //string strFilePath;



        if (!Directory.Exists(strPath1))  // 若目錄不存在則建立之
        {
            Directory.CreateDirectory(strPath1);
        }
        else
        {
            DirectoryInfo DIFO = new DirectoryInfo(strPath1);
            FileInfo[] filelist = DIFO.GetFiles();
            foreach (FileInfo fl in filelist)
            {
                System.IO.File.Delete(fl.FullName);
            }
            //Directory.Delete(strPath1, true);
            //System.Threading.Thread.Sleep(1000);
            //Directory.CreateDirectory(strPath1);
        }
        //System.Threading.Thread.Sleep(1000);
        foreach (DataRow dr in dt.Rows)
        {
            strPath1 = Server.MapPath(".") + @"\pic";
            strPath2 = dt.Rows[intI]["file_path"].ToString();
            strPath1 = strPath1 + @"\" + dt.Rows[intI]["File_Name"].ToString();
            File.Copy(strPath2, strPath1, true);

            intI = intI + 1;
        }
        //string strApparatusID_Cookie;
        //cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        dt = clsData.UploadApparatusMasterQuery("A1", "0");
        string strMaster = dt.Rows[0]["Name"].ToString();
        dt = clsData.UploadLeader("1", "", "");
        //string strLeader = dt.Rows[0]["Name_En"].ToString();

        //if ((Session["EmpName"].ToString() == strMaster) || (Session["EmpName"].ToString() == strLeader))
        //{
        //    Cost.Visible = true;
        //    UseYear.Visible = true;
        //    UseDays.Visible = true;
        //    UsePrice.Visible = true;

        //}
        //else
        //{
        //    Cost.Visible = false;
        //    UseYear.Visible = false;
        //    UseDays.Visible = false;
        //    UsePrice.Visible = false;

        //}
        string strLeader = "";
        for (intI = 0; intI < dt.Rows.Count; intI++)
        {
            strLeader = strLeader + "," + dt.Rows[intI]["Name_En"].ToString();
        }
        //====================2018/7/5 加入Jenny Tasi 修改攤提金額權限=====================================
        strLeader = strLeader + ",Jenny_Tasi";
        //====================2018/7/5 加入Jenny Tasi 修改攤提金額權限=====================================
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

        dt = clsData.UploadApparatusFileQuery(Session["ApparatusID"].ToString(), "2");
        this.gvwMain.DataSource = dt;
        this.DataBind();

    }
}
