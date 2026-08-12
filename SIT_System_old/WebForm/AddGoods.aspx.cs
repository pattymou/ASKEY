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

public partial class WebForm_AddGoods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            string strToday;

            Session["FileN"] = "";
            loadKind(this.ddlKind);

            Session["Upload_Kind"] = "Goods";

            strToday = DateTime.Now.ToString("yyyyMMddHHmmss");
            loadEmployees(this.ddlCustodian);
            loadEmployees(this.ddlCustodian1);
            Session["GoodsID"] = "G" + strToday;
        }
    }

    #region loadEmployees
    protected void loadEmployees(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees_CH(DDL, "0");
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        //string strToday;
        string strUseDate, strMaintenance;
        DateTime dt;
        string strPath = "";
        string strFile_Name = "";
        int intFile;
        string strFile = "";
        string strRStatus;

        //strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

        //Session["GoodID"] = "G" + strToday;



        if (Session["FileN"] != null)
        {
            strFile = Session["FileN"].ToString();

        }

        if (((txtName_CH.Text.Trim() == "") && (txtName_EN.Text == "")) || (ddlKind.Text == "") || (ddlDepartment.Text == ""))
        {
            Session["FileN"] = "";
            strFile = "";
            clsMsg.AlertMessage("*為必填欄位....", this.Page);
        }
        else
        {
            //strUseDate = Request["date1"].ToString();
            //if (strUseDate != "")
            //{
            //    dt = Convert.ToDateTime(strUseDate);
            //    strUseDate = dt.ToString("yyyyMMdd");
            //}
            strUseDate = ddlDate.Text;

            strRStatus = ddlStatus.Text;

            

            if (clsTransaction.InsertGoods(Session["GoodsID"].ToString(), txtName_EN.Text.Trim(), txtName_CH.Text.Trim(), ddlKind.Text, txtMF_EN.Text.Trim(), txtMF_CH.Text.Trim(), txtMF_mail.Text.Trim(), ddlCustodian.Text, strUseDate, txtQuantityStock.Text, txtQuantitySafety.Text, txtPlace.Text.Trim(), ddlStatus.Text, txtNote.Text, txtPart_No.Text.Trim(), "", ddlCustodian1.Text, txtMF_Number.Text.Trim(),txtBrand.Text.Trim(),ddlDepartment.Text) == true)
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
                    clsMsg.AlertMessage("新增成功！", this.Page);
                }
                else
                {
                    clsMsg.AlertMessage("新增成功！", this.Page);
                }
                string strDirectory;

                strDirectory = @"d:\Goods\" + Session["GoodsID"].ToString() + @"\";
                if (!Directory.Exists(strDirectory))  // 若目錄不存在則建立之
                {
                    Directory.CreateDirectory(strDirectory);
                }

            }
            else
            {
                clsMsg.AlertMessage("新增失敗....", this.Page);
            }

        }
        string strToday;


        strToday = DateTime.Now.ToString("yyyyMMddHHmmss");

        Session["GoodsID"] = "G" + strToday;


        setEmpty();
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion

    #region loadCustodian
    protected void loadCustodian(DropDownList DDL)
    {
        clsDropDownList.ddlEmployees(DDL, "0");
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
        //txtProductID.Text = "";
        txtName_CH.Text = "";
        txtName_EN.Text = "";
        txtMF_CH.Text = "";
        txtMF_EN.Text = "";
        txtPlace.Text = "";
        txtMF_mail.Text = "";
        txtQuantitySafety.Text = "";
        txtNote.Text = "";
        //txtCustodian.Text = "";
        ddlKind.Text = "";
        //txtMoney.Text = "";
        txtQuantityStock.Text = "";
        txtNote.Text = "";
        txtMF_Number.Text = "";


    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SearchGoods.aspx");
    }
}
