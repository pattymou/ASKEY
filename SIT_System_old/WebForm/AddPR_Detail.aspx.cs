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

public partial class WebForm_AddPR_Detail : System.Web.UI.Page
{
    public static string strArrival_Date;
    public static string strCheck_Date;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            loadKind(this.ddlKind);
            loadTeam(this.ddlTeam);
        }
    }

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlTeam(DDL,"0");
    }
    #endregion

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction1(DDL, 10, "1");
    }
    #endregion 

    private void Query()
    {
        string strKind;
        if (ddlKind.Text == "ALL")
            strKind = "";
        else
            strKind = ddlKind.Text;


        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Kind");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Kind";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("MF");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "MF";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column6 = new DataColumn("Name");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Name";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Part_No");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Part_No";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataTable dt;
        for (int intJ = 0; intJ < 2; intJ++)
        {
            if (intJ == 0)
                dt = clsData.UploadGoodsQuery(txtSearch.Text, "2", strKind);
            else
                dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", strKind);

            for (int intI = 0; intI < dt.Rows.Count; intI++)
            {

                DataRow dr = dt_new.NewRow();

                if (intJ == 0)
                {
                    dr["ID"] = dt.Rows[intI]["ID"].ToString();
                    dr["Kind"] = dt.Rows[intI]["Kind"].ToString();
                    dr["MF"] = dt.Rows[intI]["MF"].ToString();

                    dr["Name"] = dt.Rows[intI]["Name"].ToString();
                    dr["Part_No"] = dt.Rows[intI]["Part_No"].ToString();
                }
                else
                {
                    dr["ID"] = dt.Rows[intI]["ID"].ToString();
                    dr["Kind"] = dt.Rows[intI]["Kind"].ToString();
                    dr["MF"] = dt.Rows[intI]["Brand"].ToString() + "-" + dt.Rows[intI]["model"].ToString();

                    dr["Name"] = dt.Rows[intI]["Name"].ToString();
                    dr["Part_No"] = dt.Rows[intI]["Part_No"].ToString();
                }
                dt_new.Rows.Add(dr);
            }
        }




        this.gvwMain.DataSource = dt_new;
        this.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Query();
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strPR_ID,strName;

        if (e.CommandName == "AddToCart")
        {
            lblName.Text = "";
            lblMF.Text = "";
            lblPart_No.Text = "";

            strPR_ID = Request.QueryString["ID"];
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strName = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            Session["Goods_ID"] = strName;

            strName = strName.Substring(0, 1);

            DataTable dt = clsData.UploadRepeatGoods(Session["Goods_ID"].ToString(), strPR_ID);

            if (dt.Rows.Count > 0)
            {
                clsMsg.AlertMessage("此貨品已在清單中！", this.Page);
            }
            else
            {
                if (strName == "G")
                {
                    dt = clsData.UploadGoodsQuery(Session["Goods_ID"].ToString(), "3", "");
                    lblName.Text = dt.Rows[0]["name"].ToString().Trim();
                    lblMF.Text = dt.Rows[0]["MF"].ToString().Trim();
                    lblPart_No.Text = dt.Rows[0]["Part_No"].ToString().Trim();
                }
                else
                {
                    dt = clsData.UploadApparatusQuery(Session["Goods_ID"].ToString(), "1", "");
                    lblName.Text = dt.Rows[0]["name"].ToString().Trim();
                    lblMF.Text = dt.Rows[0]["Brand"].ToString() + "-" + dt.Rows[0]["model"].ToString();
                    lblPart_No.Text = dt.Rows[0]["Part_No"].ToString().Trim();
                }
            }

        }
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;

        //DataTable dt = clsData.UploadGoodsQuery(txtSearch.Text, "2", ddlKind.Text);

        //this.gvwMain.DataSource = dt;
        //this.DataBind();
        //GvQuery();
        Query();
    }
    #endregion

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strArrival_Date1, strCheck_Date1;
        DateTime dt;

        strArrival_Date1 = Request["date1"].ToString();
        if (strArrival_Date1 != "")
        {
            dt = Convert.ToDateTime(strArrival_Date1);
            strArrival_Date1 = dt.ToString("yyyy/MM/dd");
            
        }

        strCheck_Date1 = Request["date2"].ToString();
        if (strCheck_Date1 != "")
        {
            dt = Convert.ToDateTime(strCheck_Date1);
            strCheck_Date1 = dt.ToString("yyyy/MM/dd");
        }

        if ((lblName.Text == "") && (lblMF.Text == "") && (lblPart_No.Text == ""))
        {
            clsMsg.AlertMessage("請選擇貨品！", this.Page);
        }
        else
        {
            if (clsTransaction.InsertPR_Detail(Session["PRID"].ToString(), Session["Goods_ID"].ToString(), txtUnit.Text.Trim(), txtPurchase_Quantity.Text.Trim(), ddlTeam.Text, txtDemand_Person.Text.Trim(), txtProcurement_Staff.Text.Trim(), txtCurrency.Text.Trim(), txtEstimated_Price.Text.Trim(), txtUS_Price.Text.Trim(), txtEstimated_TotalPrice.Text.Trim(), strArrival_Date1, strCheck_Date1, "Open", txtNote.Text.Trim(), txtExchangeRate.Text.Trim()) == true)
            {
                clsMsg.AlertMessage("新增成功！", this.Page);
                setEmpty();
            }
            else
                clsMsg.AlertMessage("新增失敗！", this.Page);
        }
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/PR_Detail.aspx?ID=" + Session["PRID"].ToString());
    }

    private void setEmpty()
    {
        txtCurrency.Text = "";
        txtDemand_Person.Text = "";
        txtEstimated_Price.Text = "";
        txtEstimated_TotalPrice.Text = "";
        txtNote.Text = "";
        txtProcurement_Staff.Text = "";
        txtPurchase_Quantity.Text = "";
        txtUnit.Text = "";
        txtUS_Price.Text = "";
    }

    protected void txtEstimated_Price_TextChanged(object sender, EventArgs e)
    {
        string strVale = txtEstimated_Price.Text;
        double rst = 0.0;

        double.TryParse(strVale, out rst);

        txtEstimated_Price.Text = String.Format("{0:N2}", rst);

        if (txtPurchase_Quantity.Text != "")
        {
            double dPrice;
            dPrice = rst * Convert.ToInt32(txtPurchase_Quantity.Text);

            if (txtCurrency.Text == "NTD")
            {
                txtEstimated_TotalPrice.Text = String.Format("{0:N2}", dPrice);
                txtUS_Price.Text = "0";
            }
            else
            {
                txtUS_Price.Text = String.Format("{0:N2}", dPrice);
                txtEstimated_TotalPrice.Text = "0";
            }
        }

    }

    protected void butConversion_Click(object sender, EventArgs e)
    {
        if (txtExchangeRate.Text != "")
        {
            string strVale = txtExchangeRate.Text;
            string strVale1 = txtUS_Price.Text;
            string strTotalPrice;

            double dPrice = 0.0;
            double dExchangeRate = 0.0;

            double.TryParse(strVale, out dExchangeRate);
            double.TryParse(strVale1, out dPrice);

            //dExchangeRate = String.Format("{0:N2}", rst);
            txtEstimated_TotalPrice.Text = String.Format("{0:N2}", dExchangeRate * dPrice);

        }
        else
        {
            txtEstimated_TotalPrice.Text = (Convert.ToInt32(txtPurchase_Quantity.Text) * Convert.ToInt32(txtEstimated_Price.Text)).ToString();
        }
    }
}
