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

public partial class WebForm_GoodsView : System.Web.UI.Page
{
    public static string strFilePathNames;
    public static string strFeature;
    public static string strSpec;
    public static int intCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strKindP;

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

            Session["GoodsID"] = Request.QueryString["ID"];


            getGoods();
        }
    }

    private void getGoods()
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

        DataTable dt = clsData.UploadGoodsQuery(Session["GoodsID"].ToString(), "1", "");
        lblName_En.Text = dt.Rows[0]["Name_En"].ToString();
        lblName_CH.Text = dt.Rows[0]["Name_CH"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        lblMF_CH.Text = dt.Rows[0]["MF_CH"].ToString();
        lblMF_EN.Text = dt.Rows[0]["MF_EN"].ToString();
        lblMF_Mail.Text = dt.Rows[0]["Procurement_staff"].ToString();
        lblPart_No.Text = dt.Rows[0]["Part_No"].ToString();
        //lblProduct_ID.Text = dt.Rows[0]["Products_ID"].ToString();
        lblMF_Number.Text = dt.Rows[0]["MF_Number"].ToString();
        lblBrand.Text = dt.Rows[0]["Brand"].ToString();
        lblDep.Text = dt.Rows[0]["Custodian_Department"].ToString();


        //dt1 = Convert.ToDateTime(dt.Rows[0]["Check_Date"].ToString());
        //strDate = dt1.ToString("yyyy/MM/dd");
        //if (strDate == "1900/01/01")
        //    lblCheck_Date.Text = "";
        //else
            lblCheck_Date.Text = dt.Rows[0]["Check_Date"].ToString();
        //lblMaintenance.Text = dt.Rows[0]["MaintenanceDate"].ToString();

        lblQuantity_Stock.Text = dt.Rows[0]["Quantity_Stock"].ToString();
        lblQuantity_Safety.Text = dt.Rows[0]["Quantity_Safety"].ToString();

        //lblMoney.Text = dt.Rows[0]["Money"].ToString();
        lblPlace.Text = dt.Rows[0]["Place"].ToString();
        lblCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        //txtFeature.Text = dt.Rows[0]["Feature"].ToString();
        //txtSpec.Text = dt.Rows[0]["Spec"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        //strFeature = dt.Rows[0]["Feature"].ToString();
        //strSpec = dt.Rows[0]["Spec"].ToString();
        lblStatus.Text = dt.Rows[0]["Status"].ToString();

        DataTable dt2;
        if ((dt.Rows[0]["Custodian"].ToString() != "") && (dt.Rows[0]["Custodian"].ToString() != null))
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

        dt = clsData.UploadGoodsFileQuery(Session["GoodsID"].ToString(), "0");
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

            if (System.IO.File.Exists(strPath2))
            {
                File.Copy(strPath2, strPath1, true);
            }
           
            intI = intI + 1;
        }

        intI = 0;
        dt = clsData.UploadGoodsFileQuery(Session["GoodsID"].ToString(), "1");
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

        

        
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/ModifyGoods.aspx?ID=" + Session["GoodsID"].ToString());
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        string strPath, strPath1;

        if (clsTransaction.DelGoods(Request.QueryString["ID"].ToString()) == true)
        {
            if (clsTransaction.DelGoodsFile(Request.QueryString["ID"].ToString()) == true)
            {
                strPath = @"d:\Goods\" + Request.QueryString["ID"].ToString() + @"\";
                strPath1 = @"d:\Goods\" + Request.QueryString["ID"].ToString();
                //Directory.Delete(strPath, true);

                DirectoryInfo DIFO = new DirectoryInfo(strPath);
                FileInfo[] filelist = DIFO.GetFiles();
                foreach (FileInfo fl in filelist)
                {
                    System.IO.File.Delete(fl.FullName);
                }
                Directory.Delete(strPath1, true);
                if (clsTransaction.DelPR_Goods(Request.QueryString["ID"].ToString()) == true)
                    clsMsg.AlertMessage("刪除成功！", this.Page);
                else
                    clsMsg.AlertMessage("刪除失敗！", this.Page);
                Response.Redirect("~/WebForm/SearchGoods.aspx");
            }
        }
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SearchGoods.aspx");
    }
}
