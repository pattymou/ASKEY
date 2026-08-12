using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class WebForm_SampleView : System.Web.UI.Page
{
    public static int intCount;
    public static string strFilePathNames;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            getSample();
            GvQuery();
        }
    }

    private void getSample()
    {
        string strID;
        int intI = 0;

        strID =Request.QueryString["ID"];
        DataTable dt = clsData.UploadSample_N(strID, "1");

        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        lblFunction.Text = dt.Rows[0]["Function_Name"].ToString();
        lblItem.Text = dt.Rows[0]["Item"].ToString();
        lblNumber.Text = dt.Rows[0]["Number"].ToString();
        lblCategory.Text = dt.Rows[0]["Category"].ToString();
        lblVendor.Text = dt.Rows[0]["Vendor"].ToString();
        lblName.Text = dt.Rows[0]["ModelName"].ToString();
        lblMAC.Text = dt.Rows[0]["MAC"].ToString();
        lblPhy.Text = dt.Rows[0]["PHY"].ToString();
        lblFirmware.Text = dt.Rows[0]["Firmware"].ToString();
        lblPhysical.Text = dt.Rows[0]["Physical"].ToString();
        lblVoip.Text = dt.Rows[0]["VoIP"].ToString();
        lblCATV.Text = dt.Rows[0]["CATV"].ToString();
        lblUSB.Text = dt.Rows[0]["USB"].ToString();
        lblLAN.Text = dt.Rows[0]["LAN"].ToString();
        lblWLAN.Text = dt.Rows[0]["WLAN"].ToString();
        lblWPS.Text = dt.Rows[0]["WPS"].ToString();
        lblStatus.Text = dt.Rows[0]["ReservationStatus"].ToString();
        lblPlace.Text = dt.Rows[0]["Place"].ToString();
        //lblCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        lblNote.Text = dt.Rows[0]["Note"].ToString();
        lblNameCode.Text = dt.Rows[0]["NameCode"].ToString();
        lblDep.Text = dt.Rows[0]["Custodian_Department"].ToString();

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
                lblAgent.Text = dt2.Rows[0]["Name_CH"].ToString();
        }


        dt = clsData.UploadSampleFileQuery1(Request.QueryString["ID"], "0");
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
            File.Copy(strPath2, strPath1, true);

            intI = intI + 1;
        }

        intI = 0;
        dt = clsData.UploadSampleFileQuery1(Request.QueryString["ID"], "1");
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
        string strID;

        strID = Request.QueryString["ID"];
        Response.Redirect("~/WebForm/ModifySample.aspx?ID=" + strID);
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        string strID;

        strID = Request.QueryString["ID"];
        if (clsTransaction.DelSample1(strID) == true)
        {
            clsMsg.AlertMessage("刪除成功！", this.Page);
            Response.Redirect("~/WebForm/SearchSample.aspx");
        }
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebForm/SearchSample.aspx");
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

    private void GvQuery()
    {
        string strID;

        strID = Request.QueryString["ID"];

        DataTable dt = clsData.UploadSampleFileQuery(strID, "1");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
}
