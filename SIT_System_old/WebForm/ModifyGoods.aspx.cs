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

public partial class WebForm_ModifyGoods : System.Web.UI.Page
{
    public static string strDate1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            Session["FileN"] = "";
            loadKind(this.ddlKind);
 
            Session["Upload_Kind"] = "Goods";

            Session["GoodsID"] = Request.QueryString["ID"];
            loadEmployees(this.ddlCustodian);
            loadEmployees(this.ddlCustodian1);
            getGoods();
        }

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
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion

    private void getGoods()
    {
        DateTime dt1;
        int intI = 0;
        string strDate11;

        DataTable dt = clsData.UploadGoodsQuery(Session["GoodsID"].ToString(), "1", "");
        txtName_En.Text = dt.Rows[0]["Name_En"].ToString();
        txtName_CH.Text = dt.Rows[0]["Name_CH"].ToString();
        txtMF_CH.Text = dt.Rows[0]["MF_CH"].ToString();
        txtMF_EN.Text = dt.Rows[0]["MF_EN"].ToString();
        txtMF_Mail.Text = dt.Rows[0]["Procurement_staff"].ToString();
        ddlStatus.Text = dt.Rows[0]["Status"].ToString();
        //txtMoney.Text = dt.Rows[0]["Money"].ToString();
        txtQuantity_Stock.Text = dt.Rows[0]["Quantity_Stock"].ToString();
        txtQuantity_Safety.Text = dt.Rows[0]["Quantity_Safety"].ToString();
        txtPart_No.Text = dt.Rows[0]["Part_No"].ToString();
        txtMF_Number.Text = dt.Rows[0]["MF_Number"].ToString();
        txtBrand.Text = dt.Rows[0]["Brand"].ToString();
        ddlDepartment.Text = dt.Rows[0]["Custodian_Department"].ToString();

        //if ((strDate1 == "") || (strDate1 == null))
        //{
        //    dt1 = Convert.ToDateTime(dt.Rows[0]["Check_Date"].ToString());

        //    strDate11 = dt1.ToString("yyyy/MM/dd");
        //    if (strDate11 == "1900/01/01")
        //        strDate11 = "";
        //    else
        //        strDate1 = strDate11;
        //}
        ddlDate.Text = dt.Rows[0]["Check_Date"].ToString();

        txtPlace.Text = dt.Rows[0]["Place"].ToString();
        ddlKind.Text = dt.Rows[0]["Kind"].ToString();
        if (ddlKind.Text == "")
            ddlKind.Text = "";
        //txtCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();

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


        dt = clsData.UploadGoodsFileQuery(Session["GoodsID"].ToString(), "0");
        string strPath1 = Server.MapPath(".") + @"\pic";
        string strPath2;



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
                File.Copy(strPath2, strPath1, true);

            intI = intI + 1;
        }

        dt = clsData.UploadGoodsFileQuery(Session["GoodsID"].ToString(), "2");
        this.gvwMain.DataSource = dt;
        this.DataBind();

    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strName, strPath;

        strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[2].FindControl("lblName")).Text;
        //string path = @"C:/test/" + strName;
        string path = @"D:\Goods\" + Session["GoodsID"].ToString() + @"\" + strName;
        if (clsTransaction.DelApparatusFiles(strName, Session["GoodsID"].ToString()) == true)
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

        DataTable dt = clsData.UploadGoodsFileQuery(Session["GoodsID"].ToString(), "2");
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
        string strCheck_Date, strMaintenance;
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

        if (((txtName_En.Text.Trim() == "") && (txtName_CH.Text == "")) || (ddlKind.Text == "") || (ddlDepartment.Text == ""))
        {
            Session["FileN"] = "";
            strFile = "";
            clsMsg.AlertMessage("*為必填欄位....", this.Page);
        }
        else
        {
            //strCheck_Date = Request["date1"].ToString();
            //if (strCheck_Date != "")
            //{
            //    dt = Convert.ToDateTime(strCheck_Date);
            //    strCheck_Date = dt.ToString("yyyyMMdd");
            //}


            if (clsTransaction.UpDateGoods(Session["GoodsID"].ToString(), txtName_En.Text.Trim(), txtName_CH.Text.Trim(), ddlKind.Text, txtMF_EN.Text.Trim(), txtMF_CH.Text.Trim(), txtMF_Mail.Text.Trim(), ddlCustodian.Text, ddlDate.Text, txtQuantity_Stock.Text, txtQuantity_Safety.Text, txtPlace.Text, ddlStatus.Text, txtNote.Text, txtPart_No.Text.Trim(), ddlCustodian1.Text, txtMF_Number.Text.Trim(), txtBrand.Text.Trim(),ddlDepartment.Text) == true)
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
                            clsTransaction.InsertUploadFile_Goods(Session["GoodsID"].ToString(), strFile_Name, strPath);
                        }
                    }
                    clsMsg.AlertMessage("修改成功！", this.Page);

                    //GvQuery();
                }
                else
                    clsMsg.AlertMessage("修改成功！", this.Page);

                //getDate();
            }
            else
            {
                clsMsg.AlertMessage("修改失敗....", this.Page);
            }

        }
        Session["FileN"] = "";

        getGoods();
    }

    private void getDate()
    {
        DateTime dt1;
        string strDate11;

        DataTable dt = clsData.UploadGoodsQuery(Session["GoodsID"].ToString(), "1", "");
        if ((strDate1 == "") || (strDate1 == null))
        {
            dt1 = Convert.ToDateTime(dt.Rows[0]["Check_Date"].ToString());

            strDate11 = dt1.ToString("yyyy/MM/dd");
            if (strDate11 == "1900/01/01")
                strDate11 = "";
            else
                strDate1 = strDate11;
        }

    }
}
