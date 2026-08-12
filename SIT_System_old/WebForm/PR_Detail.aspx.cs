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
using System.Text;



public partial class WebForm_PR_Detail : System.Web.UI.Page
{
    public static string strDate;
    public static string strDate1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");

        if (!IsPostBack)
        {
            //Session["PRID"] = Request.QueryString["ID"];
            //loadKind(this.ddlKind);
            getPR();

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion

    #region loadTeam
    protected void loadTeam(DropDownList DDL)
    {
        clsDropDownList.ddlTeam(DDL, "0");
    }
    #endregion

    private void getPR()
    {
        string strID, strDateA;
        DateTime dt1;

        strID = Request.QueryString["ID"];
        Session["PRID"] = Request.QueryString["ID"];
        DataTable dt = clsData.getGoodsList(strID, "");

        if (dt.Rows.Count > 0)
        {
            txtNote.Text = dt.Rows[0]["Note"].ToString();
            txtSigned_ID.Text = dt.Rows[0]["Signed_ID"].ToString();
            txtPR_No.Text = dt.Rows[0]["PR_No"].ToString();
            ddlPRStatus.Text = dt.Rows[0]["Status"].ToString();
            txtDemand_Person.Text = dt.Rows[0]["Demand_Person"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            ddlAcceptedTeam.Text = dt.Rows[0]["Accepted_Team"].ToString();
            //ddlKind.Text = dt.Rows[0]["PR_Kind"].ToString();

            dt1 = Convert.ToDateTime(dt.Rows[0]["Application_Date"].ToString());
            strDateA = dt1.ToString("yyyy/MM/dd");
            if (strDateA == "1900/01/01")
                strDate = "";
            else
                strDate = strDateA;

            dt1 = Convert.ToDateTime(dt.Rows[0]["PR_Date"].ToString());
            strDateA = dt1.ToString("yyyy/MM/dd");
            if (strDateA == "1900/01/01")
                strDate1 = "";
            else
                strDate1 = strDateA;

            GvQuery();
        }

    }

    #region gvList_RowDeleting
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strPR_ID = ((Label)this.gvList.Rows[e.RowIndex].Cells[17].FindControl("lblPR_ID")).Text;
        string strGoods_ID = ((Label)this.gvList.Rows[e.RowIndex].Cells[18].FindControl("lblGoods_ID")).Text;
        string strStatus = ((Label)this.gvList.Rows[e.RowIndex].Cells[15].FindControl("lblStatus")).Text;
        string strQuantity = ((Label)this.gvList.Rows[e.RowIndex].Cells[3].FindControl("lblQuantity")).Text;

        string strCustomer = "";

        int intStock, intQuantity_Stock2;

        if (strStatus == "Close")
        {
            DataTable dt = clsData.UploadGoodsQuery(strGoods_ID, "1", "");


            if (strGoods_ID.Substring(0, 1) == "G")
            {
                if (dt.Rows[0]["Quantity_Stock"].ToString() == "")
                    intQuantity_Stock2 = 0;
                else
                    intQuantity_Stock2 = Convert.ToInt16(dt.Rows[0]["Quantity_Stock"].ToString());



                intStock = intQuantity_Stock2 - Convert.ToInt16(strQuantity);
                clsTransaction.UpDateGoodsQuantity(strGoods_ID, intStock.ToString());

            }
        }

        //strCustomer = ddlCustomer.Text;
        if (clsTransaction.DelPR_Detail(strPR_ID, strGoods_ID) != true)
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);

        ((GridView)sender).SelectedIndex = -1;
        ((GridView)sender).EditIndex = -1;
        GvQuery();

    }
    #endregion

    #region gvList_RowUpdating (指定資料行更新)
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int intStock, intQuantity_Stock2;

        string strQuantity = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtQuantity_E")).Text;
        string strUnit = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtUnit_E")).Text;
        string strTeam = ((DropDownList)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("ddlTeam")).Text;
        string strDemand_Person = "";
        string strProcurement_Staff = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtProcurement_Staff_E")).Text;
        string strCurrency = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtCurrency_E")).Text;
        string strEstimated_Price = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtEstimated_Price_E")).Text;
        string strUS_Price = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtUS_Price_E")).Text;
        string strEstimated_TotalPrice = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtEstimated_TotalPrice_E")).Text;
        string strlArrival_Date = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("DateTimeValue")).Text;
        string strCheck_Date = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("DateTimeValue1")).Text;
        string strStatus = ((DropDownList)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("ddlStatus")).Text;
        string strNote = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtNote_E")).Text;

        string strPR_ID = ((Label)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("lblPR_ID")).Text;
        string strGoods_ID = ((Label)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("lblGoods_ID")).Text;
        string strExchangeRate = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("txtExchangeRate_E")).Text;


        if (clsTransaction.UpDatePR_Detail(strPR_ID, strGoods_ID, strUnit, strQuantity, strTeam, strDemand_Person, strProcurement_Staff, strCurrency, strEstimated_Price, strUS_Price, strEstimated_TotalPrice, strlArrival_Date, strCheck_Date, strStatus, strNote, strExchangeRate) == true)
        {

            DataTable dt = clsData.UploadGoodsQuery(strGoods_ID, "1", "");


            if (strGoods_ID.Substring(0, 1) == "G")
            {
                if (dt.Rows[0]["Quantity_Stock"].ToString() == "")
                    intQuantity_Stock2 = 0;
                else
                    intQuantity_Stock2 = Convert.ToInt16(dt.Rows[0]["Quantity_Stock"].ToString());


                if (strStatus == "Close")
                {
                    if (Session["Status"].ToString() != "Close")
                    {
                        intStock = intQuantity_Stock2 + Convert.ToInt16(strQuantity);
                        clsTransaction.UpDateGoodsQuantity(strGoods_ID, intStock.ToString());
                    }
                }
                else
                {
                    if (Session["Status"].ToString() == "Close")
                    {
                        intStock = intQuantity_Stock2 - Convert.ToInt16(strQuantity);
                        clsTransaction.UpDateGoodsQuantity(strGoods_ID, intStock.ToString());
                    }
                }
            }


            ((GridView)sender).SelectedIndex = -1;
            ((GridView)sender).EditIndex = -1;
            GvQuery();
        }
        else
        {
            clsMsg.AlertMessage("更新失敗，請洽IT人員！", this.Page);
        }
    }
    #endregion

    #region gvList_RowEditing (指定資料行進行修改)
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //DropDownList ddl = (DropDownList)this.gvList.Rows[e.NewEditIndex].FindControl("ddlTeam");
        //loadTeam(ddl);
        ((GridView)sender).EditIndex = e.NewEditIndex;
        GvQuery();
        Session["Status"] = ((DropDownList)this.gvList.Rows[e.NewEditIndex].Cells[0].FindControl("ddlStatus")).Text;

    }
    #endregion

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == gvList.EditIndex)
            {
                DropDownList ddl = ((DropDownList)e.Row.Cells[6].FindControl("ddlTeam"));
                loadTeam(ddl);
            }
        }
    }

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

    double dEstimated = 0;
    double dUS_Price = 0;
    double dEstimated_TotalPrice = 0;
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        //double rst = 0.0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState != DataControlRowState.Edit && ((int)e.Row.RowState) != 5)
            {
                double dPrice = 0.0;
                double dPrice1 = 0.0;
                double dPrice2 = 0.0;

                string strVale = ((Label)(e.Row.FindControl("lblEstimated_Price"))).Text.Trim();
                string strVale1 = ((Label)(e.Row.FindControl("lblUS_Price"))).Text.Trim();
                string strVale2 = ((Label)(e.Row.FindControl("lblEstimated_TotalPrice"))).Text.Trim();

                double.TryParse(strVale, out dPrice);
                dEstimated = dEstimated + dPrice;
                double.TryParse(strVale1, out dPrice1);
                dUS_Price = dUS_Price + dPrice1;
                double.TryParse(strVale2, out dPrice2);
                dEstimated_TotalPrice = dEstimated_TotalPrice + dPrice2;
            }

            gvList.Columns[3].ItemStyle.Width = 80;
            gvList.Columns[4].ItemStyle.Width = 80;
            gvList.Columns[5].ItemStyle.Width = 150;
            gvList.Columns[6].ItemStyle.Width = 120;
            gvList.Columns[7].ItemStyle.Width = 120;
            gvList.Columns[8].ItemStyle.Width = 80;
            gvList.Columns[9].ItemStyle.Width = 100;
            gvList.Columns[10].ItemStyle.Width = 100;
            gvList.Columns[11].ItemStyle.Width = 100;
            gvList.Columns[12].ItemStyle.Width = 100;
            gvList.Columns[13].ItemStyle.Width = 100;
            gvList.Columns[14].ItemStyle.Width = 100;
            gvList.Columns[15].ItemStyle.Width = 220;




        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "總計";
            //e.Row.Cells[9].Text = String.Format("{0:N2}", dEstimated);
            e.Row.Cells[10].Text = String.Format("{0:N2}", dUS_Price);
            e.Row.Cells[11].Text = String.Format("{0:N2}", dEstimated_TotalPrice);

        }
    }

    protected void txtEstimated_Price_E_TextChanged(object sender, EventArgs e)
    {
        string strVale = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text;
        double rst = 0.0;

        double.TryParse(strVale, out rst);

        string strCurrency = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtCurrency_E")).Text;

        ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text = String.Format("{0:N2}", rst);

        if (((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text != "")
        {
            if (strCurrency != "NTD")
            {
                double dPrice;
                dPrice = rst * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text);
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text = String.Format("{0:N2}", dPrice);
                dPrice = 0;
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dPrice);
            }
            else
            {
                double dPrice;
                dPrice = rst * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text);
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dPrice);
                dPrice = 0;
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text = String.Format("{0:N2}", dPrice);
            }
        }

        if ((((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text != "") && (((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text != "0"))
        {
            strVale = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text;
            string strVale1 = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text;
            string strTotalPrice;

            double dPrice = 0.0;
            double dExchangeRate = 0.0;

            double.TryParse(strVale, out dExchangeRate);
            double.TryParse(strVale1, out dPrice);

            //dExchangeRate = String.Format("{0:N2}", rst);
            ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dExchangeRate * dPrice);

        }
        //else
        //{
        //    ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = (Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text) * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text)).ToString();
        //}
    }

    protected void txtQuantity_E_TextChanged(object sender, EventArgs e)
    {
        string strVale = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text;
        double rst = 0.0;

        double.TryParse(strVale, out rst);

        string strCurrency = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtCurrency_E")).Text;

        ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text = String.Format("{0:N2}", rst);

        if (((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text != "")
        {
            if (strCurrency != "NTD")
            {
                double dPrice;
                dPrice = rst * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text);
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text = String.Format("{0:N2}", dPrice);
                dPrice = 0;
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dPrice);

            }
            else
            {
                double dPrice;
                dPrice = rst * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text);
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dPrice);
                dPrice = 0;
                ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text = String.Format("{0:N2}", dPrice);
            }

        }

        if ((((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text != "") && (((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text != "0"))
        {
            strVale = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text;
            string strVale1 = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text;
            string strTotalPrice;

            double dPrice = 0.0;
            double dExchangeRate = 0.0;

            double.TryParse(strVale, out dExchangeRate);
            double.TryParse(strVale1, out dPrice);

            //dExchangeRate = String.Format("{0:N2}", rst);
            ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dExchangeRate * dPrice);

        }
        //else
        //{
        //    ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = (Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text) * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text)).ToString();
        //}
    }

    protected void txtExchangeRate_E_TextChanged(object sender, EventArgs e)
    {
        string strVale = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text;
        double rst = 0.0;

        double.TryParse(strVale, out rst);

        ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text = String.Format("{0:N2}", rst);

        if (((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text != "")
        {
            double dPrice;
            dPrice = rst * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text);
            ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text = String.Format("{0:N2}", dPrice);
        }

        if (((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text != "")
        {
            strVale = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtExchangeRate_E")).Text;
            string strVale1 = ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtUS_Price_E")).Text;
            string strTotalPrice;

            double dPrice = 0.0;
            double dExchangeRate = 0.0;

            double.TryParse(strVale, out dExchangeRate);
            double.TryParse(strVale1, out dPrice);

            //dExchangeRate = String.Format("{0:N2}", rst);
            ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = String.Format("{0:N2}", dExchangeRate * dPrice);

        }
        else
        {
            ((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_TotalPrice_E")).Text = (Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtQuantity_E")).Text) * Convert.ToInt32(((TextBox)this.gvList.Rows[gvList.EditIndex].Cells[4].FindControl("txtEstimated_Price_E")).Text)).ToString();
        }
    }

    #region GvQuery
    private void GvQuery()
    {
        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("PR_ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "PR_ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Name");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Name";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Part_No");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Part_No";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("Kind");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "Kind";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Purchase_Quantity");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Purchase_Quantity";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Unit");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Unit";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Demand_Team");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Demand_Team";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("Demand_Person");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "Demand_Person";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Procurement_Staff");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Procurement_Staff";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Currency");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Currency";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("Estimated_Price");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "Estimated_Price";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("US_Price");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "US_Price";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        DataColumn column13 = new DataColumn("Estimated_TotalPrice");
        column13.DataType = System.Type.GetType("System.String");
        column13.AllowDBNull = true;
        column13.Caption = "Estimated_TotalPrice";
        column13.DefaultValue = "0";
        dt_new.Columns.Add(column13);

        DataColumn column14 = new DataColumn("Arrival_Date");
        column14.DataType = System.Type.GetType("System.String");
        column14.AllowDBNull = true;
        column14.Caption = "Arrival_Date";
        column14.DefaultValue = "0";
        dt_new.Columns.Add(column14);

        DataColumn column15 = new DataColumn("Check_Date");
        column15.DataType = System.Type.GetType("System.String");
        column15.AllowDBNull = true;
        column15.Caption = "Check_Date";
        column15.DefaultValue = "0";
        dt_new.Columns.Add(column15);

        DataColumn column16 = new DataColumn("Status");
        column16.DataType = System.Type.GetType("System.String");
        column16.AllowDBNull = true;
        column16.Caption = "Status";
        column16.DefaultValue = "0";
        dt_new.Columns.Add(column16);

        DataColumn column17 = new DataColumn("Note");
        column17.DataType = System.Type.GetType("System.String");
        column17.AllowDBNull = true;
        column17.Caption = "Note";
        column17.DefaultValue = "0";
        dt_new.Columns.Add(column17);

        DataColumn column18 = new DataColumn("Goods_ID");
        column18.DataType = System.Type.GetType("System.String");
        column18.AllowDBNull = true;
        column18.Caption = "Goods_ID";
        column18.DefaultValue = "0";
        dt_new.Columns.Add(column18);

        DataColumn column19 = new DataColumn("ExchangeRate");
        column19.DataType = System.Type.GetType("System.String");
        column19.AllowDBNull = true;
        column19.Caption = "ExchangeRate";
        column19.DefaultValue = "0";
        dt_new.Columns.Add(column19);


        //DataTable dt = clsData.UploadPR_DetailQuery(1,Session["PRID"].ToString());
        DataTable dt = clsData.UploadPR_DetailQuery(0, Session["PRID"].ToString());
        DataTable dt1;
        string strName, strDate, strDate1 = "";
        DateTime dTime;


        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            strName = dt.Rows[intI]["Goods_ID"].ToString().Substring(0, 1);
            DataRow dr = dt_new.NewRow();
            if (strName == "A")
            {
                dt1 = clsData.UploadApparatusQuery(dt.Rows[intI]["Goods_ID"].ToString(), "1", "");

                dr["Name"] = dt1.Rows[0]["Name"].ToString();
                dr["Part_No"] = dt1.Rows[0]["Part_No"].ToString();
                dr["Kind"] = dt1.Rows[0]["Kind"].ToString();

                dr["Purchase_Quantity"] = dt.Rows[intI]["Purchase_Quantity"].ToString();
                dr["PR_ID"] = dt.Rows[intI]["PR_ID"].ToString();
                dr["Unit"] = dt.Rows[intI]["Unit"].ToString();
                dr["Demand_Team"] = dt.Rows[intI]["Demand_Team"].ToString();
                dr["Demand_Person"] = dt.Rows[intI]["Demand_Person"].ToString();
                dr["Procurement_Staff"] = dt.Rows[intI]["Procurement_Staff"].ToString();
                dr["Currency"] = dt.Rows[intI]["Currency"].ToString();
                dr["Estimated_Price"] = dt.Rows[intI]["Estimated_Price"].ToString();
                dr["US_Price"] = dt.Rows[intI]["US_Price"].ToString();
                dr["Estimated_TotalPrice"] = dt.Rows[intI]["Estimated_TotalPrice"].ToString();

                dTime = Convert.ToDateTime(dt.Rows[intI]["Arrival_Date"].ToString());
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    strDate1 = strDate;

                dr["Arrival_Date"] = strDate1;

                dTime = Convert.ToDateTime(dt.Rows[intI]["Check_Date"].ToString());
                strDate = dTime.ToString("yyyy/MM/dd");
                if (strDate != "1900/01/01")
                    strDate1 = strDate;

                dr["Check_Date"] = strDate1;
                dr["Status"] = dt.Rows[intI]["Status"].ToString();
                dr["Note"] = dt.Rows[intI]["Note"].ToString();
                dr["Goods_ID"] = dt.Rows[intI]["Goods_ID"].ToString();
                dr["ExchangeRate"] = dt.Rows[intI]["ExchangeRate"].ToString();
            }
            else
            {
                dt1 = clsData.UploadGoodsQuery(dt.Rows[intI]["Goods_ID"].ToString(), "1", "");
                if (dt1.Rows.Count > 0)
                {
                    dr["Name"] = dt1.Rows[0]["Name_En"].ToString() + "-" + dt1.Rows[0]["Name_CH"].ToString();
                    dr["Part_No"] = dt1.Rows[0]["Part_No"].ToString();
                    dr["Kind"] = dt1.Rows[0]["Kind"].ToString();

                    dr["Purchase_Quantity"] = dt.Rows[intI]["Purchase_Quantity"].ToString();
                    dr["PR_ID"] = dt.Rows[intI]["PR_ID"].ToString();
                    dr["Unit"] = dt.Rows[intI]["Unit"].ToString();
                    dr["Demand_Team"] = dt.Rows[intI]["Demand_Team"].ToString();
                    dr["Demand_Person"] = dt.Rows[intI]["Demand_Person"].ToString();
                    dr["Procurement_Staff"] = dt.Rows[intI]["Procurement_Staff"].ToString();
                    dr["Currency"] = dt.Rows[intI]["Currency"].ToString();
                    dr["Estimated_Price"] = dt.Rows[intI]["Estimated_Price"].ToString();
                    dr["US_Price"] = dt.Rows[intI]["US_Price"].ToString();
                    dr["Estimated_TotalPrice"] = dt.Rows[intI]["Estimated_TotalPrice"].ToString();

                    dTime = Convert.ToDateTime(dt.Rows[intI]["Arrival_Date"].ToString());
                    strDate = dTime.ToString("yyyy/MM/dd");
                    if (strDate != "1900/01/01")
                        strDate1 = strDate;

                    dr["Arrival_Date"] = strDate1;

                    dTime = Convert.ToDateTime(dt.Rows[intI]["Check_Date"].ToString());
                    strDate = dTime.ToString("yyyy/MM/dd");
                    if (strDate != "1900/01/01")
                        strDate1 = strDate;

                    dr["Check_Date"] = strDate1;
                    dr["Status"] = dt.Rows[intI]["Status"].ToString();
                    dr["Note"] = dt.Rows[intI]["Note"].ToString();
                    dr["Goods_ID"] = dt.Rows[intI]["Goods_ID"].ToString();
                    dr["ExchangeRate"] = dt.Rows[intI]["ExchangeRate"].ToString();
                }
            }

            dt_new.Rows.Add(dr);

        }



        this.gvList.DataSource = dt_new;
        this.DataBind();
    }
    #endregion

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddPR_Detail.aspx");
    }

    private DateTime DTAdd(DateTime dt, int addday)
    {
        int i = 0;
        while (i <= addday)
        {
            dt = dt.AddDays(1);
            if ((dt.DayOfWeek.ToString() != "Saturday") & (dt.DayOfWeek.ToString() != "Sunday"))
            {
                i += 1;
            }
        }
        return dt;
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        string strApplication_Date, strPR_Date;
        string strNotification_Date = "";
        DateTime dt;

        strApplication_Date = Request["date1"].ToString();
        if (strApplication_Date != "")
        {
            dt = Convert.ToDateTime(strApplication_Date);
            strApplication_Date = dt.ToString("yyyyMMdd");
            dt = DTAdd(dt, 15);
            strNotification_Date = dt.ToString("yyyyMMdd");
        }

        strPR_Date = Request["date2"].ToString();
        if (strPR_Date != "")
        {
            dt = Convert.ToDateTime(strPR_Date);
            strPR_Date = dt.ToString("yyyyMMdd");
        }

        string strGoods_ID;
        DataTable dt1;
        if (clsTransaction.UpDatePR(Session["PRID"].ToString(), strApplication_Date, txtPR_No.Text.Trim(), strPR_Date, txtSigned_ID.Text.Trim(), txtNote.Text, txtDemand_Person.Text.Trim(), txtEmail.Text.Trim(), strNotification_Date, ddlPRStatus.Text, ddlAcceptedTeam.Text) == true)
        {
            for (int intI = 0; intI < gvList.Rows.Count; intI++)
            {
                strGoods_ID = ((Label)this.gvList.Rows[intI].Cells[18].FindControl("lblGoods_ID")).Text;

                if (strGoods_ID.IndexOf("G") >= 0)
                {
                    dt1 = clsData.UploadGoodsQuery(strGoods_ID, "1", "");

                    if (dt1.Rows[0]["Status"].ToString() == "採購中")
                        clsTransaction.UpDateGoodsStatus("閒置中", strGoods_ID);
                }
                else
                {
                    dt1 = clsData.UploadApparatusQuery(strGoods_ID, "1", "");

                    if (dt1.Rows[0]["ReservationStatus"].ToString() == "採購中")
                        clsTransaction.UpDateApparatusStatus("可借用", strGoods_ID);
                }
            }


            getPR();
            clsMsg.AlertMessage("更新成功！", this.Page);
        }
        else
            clsMsg.AlertMessage("更新失敗！", this.Page);
    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/GoodsList.aspx");
    }

    protected void lbtnDel_Click(object sender, EventArgs e)
    {


        if (clsTransaction.DelPR(Session["PRID"].ToString()) == true)
            clsMsg.AlertMessage("刪除成功！", this.Page);
        else
            clsMsg.AlertMessage("刪除失敗！", this.Page);

        Response.Redirect("~/WebForm/GoodsList.aspx");
    }
}

