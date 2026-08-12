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

public partial class WebForm_ApparatusView : System.Web.UI.Page
{
    public static string strFilePathNames;
    public static string strFeature;
    public static string strSpec;
    //public static string[] strFilePathNames;
    public static int intCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strKindP;
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        string strWrite;

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            lblAdd.Visible = false;
            lblDel.Visible = false;
        }

        if (!IsPostBack)
        {

            strKindP = Request.QueryString["Kind"];
            if (strKindP == "0")
            {
                lblAdd.Visible = false;
                lblDel.Visible = false;
            }
            //HttpCookie cookie_ApparatusID = new HttpCookie("ApparatusID");
            //cookie_ApparatusID.Value = Server.UrlEncode(Request.QueryString["ID"]);
            ////cookie_ApparatusID.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies.Add(cookie_ApparatusID); 
            Session["ApparatusID"] = Request.QueryString["ID"];

            //clsParameter.strApparatusID = Request.QueryString["ID"];
            //clsParameter.strApparatusID = "6";
            getApparatus();
        }
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //cookie_ApparatusID.Value = Server.UrlEncode(Request.QueryString["ID"]);
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        Response.Redirect("~/WebForm/ModifyApprartus.aspx?ID=" + Session["ApparatusID"].ToString());
    }

    private void getApparatus()
    {
        string strFile;
        string strDate;
        string strRStatus;
        DateTime dt1;
        
        //string[] FileNames = New string[count];
        int intI = 0;
        
        //string strApparatusID_Cookie;
        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //cookie_ApparatusID.Value = Server.UrlEncode(Request.QueryString["ID"]);
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        DataTable dt = clsData.UploadApparatusQuery(Session["ApparatusID"].ToString(), "1", "");
        lblProduct_ID.Text = dt.Rows[0]["products_id"].ToString();
        lblName.Text = dt.Rows[0]["name"].ToString();
        lblBrand.Text = dt.Rows[0]["brand"].ToString();
        lblModel.Text = dt.Rows[0]["model"].ToString();
        lblNumber.Text = dt.Rows[0]["number"].ToString();
        lblIMEI.Text = dt.Rows[0]["IMEI"].ToString();
        lblVR.Text = dt.Rows[0]["OS_VR"].ToString();
        lblPart_No.Text = dt.Rows[0]["Part_No"].ToString();
        lblName_En.Text = dt.Rows[0]["name_en"].ToString();
        lblMF.Text = dt.Rows[0]["MF"].ToString();
        lblProcurement_staff.Text = dt.Rows[0]["Procurement_staff"].ToString();
        lblMF_Number.Text = dt.Rows[0]["MF_Number"].ToString();

        lblCost.Text = dt.Rows[0]["Cost_Price"].ToString();
        lblUseYear.Text = dt.Rows[0]["Years_Use"].ToString();
        lblUseDays.Text = dt.Rows[0]["Days_Use"].ToString();
        lblUsePrice.Text = dt.Rows[0]["Price_Use"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        lblDep.Text = dt.Rows[0]["Custodian_Department"].ToString();

        //===============0217
        //strRStatus = dt.Rows[0]["ReservationStatus"].ToString();

        //if (strRStatus == "Y")
        //    lblRStatus.Text = "可借用";
        //else
        //    lblRStatus.Text = "不可借用"; 
        lblRStatus.Text = dt.Rows[0]["ReservationStatus"].ToString();
        //===================0217

        dt1 = Convert.ToDateTime(dt.Rows[0]["InspectionDate"].ToString());
        strDate = dt1.ToString("yyyy/MM/dd");
        if (strDate == "1900/01/01")
            lblInspection.Text = "";
        else
            lblInspection.Text = strDate;
        //lblMaintenance.Text = dt.Rows[0]["MaintenanceDate"].ToString();

        dt1 = Convert.ToDateTime(dt.Rows[0]["MaintenanceDate"].ToString());
        strDate = dt1.ToString("yyyy/MM/dd");
        if (strDate == "1900/01/01")
            lblMaintenance.Text = "";
        else
            lblMaintenance.Text = strDate;

        lblOS.Text = dt.Rows[0]["OS"].ToString();
        lblPlace.Text = dt.Rows[0]["Place"].ToString();
        
        //txtFeature.Text = dt.Rows[0]["Feature"].ToString();
        //txtSpec.Text = dt.Rows[0]["Spec"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        strFeature = dt.Rows[0]["Feature"].ToString();
        strSpec = dt.Rows[0]["Spec"].ToString();

        DataTable dt2;
        if (dt.Rows[0]["Custodian"].ToString() != "")
        {
            dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
            if (dt2.Rows.Count > 0)
                lblCustodian.Text = dt2.Rows[0]["Name_CH"].ToString();
        }

        if (dt.Rows[0]["Agent"].ToString() != "")
        {
            dt2 = clsData.getEmployees("1", dt.Rows[0]["Agent"].ToString());
            if (dt2.Rows.Count > 0)
                lblCustodian1.Text = dt2.Rows[0]["Name_CH"].ToString();
        }

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
        }

        foreach (DataRow dr in dt.Rows)
        {
            strPath1 = Server.MapPath(".") + @"\pic";
            strPath2 = dt.Rows[intI]["file_path"].ToString();
            strPath1 = strPath1 + @"\" + dt.Rows[intI]["File_Name"].ToString();
            File.Copy(strPath2, strPath1,true);
           
            intI = intI + 1;
        }

        intI = 0;
        dt = clsData.UploadApparatusFileQuery(Session["ApparatusID"].ToString(), "1");
        intCount = dt.Rows.Count;
        string[] FileNames = new string[intCount];
        strFilePathNames = "";
        foreach (DataRow dr in dt.Rows)
        {
            FileNames[intI] = dt.Rows[intI]["File_Name"].ToString();

            if (intI == 0)
                strFilePathNames = FileNames[intI];
            else
                strFilePathNames = strFilePathNames + "," + FileNames[intI];
            intI = intI + 1;
        }

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

            if ((strLeader.IndexOf(Session["EmpName"].ToString()) != -1) && (Session["EmpName"].ToString() != ""))
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
    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        string strPath, strPath1;
        string strApparatusID_Cookie;

        //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
        //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

        if (clsTransaction.DelApparatus(Request.QueryString["ID"].ToString()) == true)
        {
            if (clsTransaction.DelApparatusFile(Request.QueryString["ID"].ToString()) == true)
            {
                strPath = @"d:\Apparatus\" + Request.QueryString["ID"].ToString() + @"\";
                strPath1 = @"d:\Apparatus\" + Request.QueryString["ID"].ToString();
                //Directory.Delete(strPath, true);

                DirectoryInfo DIFO = new DirectoryInfo(strPath);
                FileInfo[] filelist = DIFO.GetFiles();
                foreach (FileInfo fl in filelist)
                {
                    System.IO.File.Delete(fl.FullName);
                }
                Directory.Delete(strPath1, true);
                clsTransaction.DelPR_Goods(Request.QueryString["ID"].ToString());
                clsMsg.AlertMessage("刪除成功！", this.Page);
                Response.Redirect("~/WebForm/SearchApparatus.aspx");
            }
        }
    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/WebForm/SearchApparatus.aspx");
        Response.Redirect("~/WebForm/SearchApparatus.aspx");
    }
}
