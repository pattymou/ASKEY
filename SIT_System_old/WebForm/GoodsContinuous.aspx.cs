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

public partial class WebForm_GoodsContinuous : System.Web.UI.Page
{
    public static string strDate;
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable dt;

        if (Session["EmpNo"] == null)
        {
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");

        }
        if (!IsPostBack)
        {
            lblID.Visible = false;
            lblAID.Visible = false;
            //lblEndDate.Visible = false;
            //strDepartment = "DA40-SIT";
            //strDepartment = Session["EmpName"].ToString().Trim();


            GvQuery();
            GvQuery1();
        }
    }

    private void GvQuery()
    {
        string strID;
        string strDepartment;
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

        DataColumn column3 = new DataColumn("Part_No");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Part_No";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("MF_CH");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "MF_CH";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Name_En");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Name_En";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Name_CH");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Name_CH";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Custodian");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Custodian";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("Quantity_Stock");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "Quantity_Stock";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Borrower");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Borrower";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Status");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Status";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        strDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
        {
            strDepartment = "";
        }
        else
        {
            strDepartment = Session["AppNo"].ToString();

        }

        DataTable dt = clsData.getContinuousGoodsList(strDate, strDepartment,"0");

        int intQuantity_Stock, intQuantity_Stock1;

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            DataTable dt1 = clsData.UploadBorrowGoodsQuery(dt.Rows[intI]["ID"].ToString());

            DataRow dr = dt_new.NewRow();

            dr["ID"] = dt.Rows[intI]["ID"].ToString();
            dr["Kind"] = dt.Rows[intI]["Kind"].ToString();
            dr["Part_No"] = dt.Rows[intI]["Part_No"].ToString();
            dr["MF_CH"] = dt.Rows[intI]["MF_CH"].ToString();

            dr["Name_En"] = dt.Rows[intI]["Name_En"].ToString();
            dr["Name_CH"] = dt.Rows[intI]["Name_CH"].ToString();
            dr["Custodian"] = dt.Rows[intI]["Custodian"].ToString();

            if (dt1.Rows[0]["Count_ID"].ToString() == "")
                intQuantity_Stock1 = 0;
            else
                intQuantity_Stock1 = Convert.ToInt16(dt1.Rows[0]["Count_ID"].ToString());
            intQuantity_Stock = Convert.ToInt16(dt.Rows[intI]["Quantity_Stock"].ToString()) - intQuantity_Stock1;

            dr["Quantity_Stock"] = intQuantity_Stock.ToString();

            dr["Borrower"] = dt.Rows[intI]["Borrower"].ToString();
            dr["Status"] = dt.Rows[intI]["Status"].ToString();

            dt_new.Rows.Add(dr);

        }

        this.gvwMain.DataSource = dt_new;
        this.DataBind();
        
    }

    private void GvQuery1()
    {
        string strID;
        string strDepartment;
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

        DataColumn column3 = new DataColumn("Part_No");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Part_No";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("MF_CH");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "MF_CH";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("Name_En");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "Name_En";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("Name_CH");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "Name_CH";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("Custodian");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "Custodian";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("Quantity_Stock");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "Quantity_Stock";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("Borrower");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "Borrower";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Status");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Status";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        strDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
        {
            strDepartment = "";
        }
        else
        {
            strDepartment = Session["AppNo"].ToString();

        }

        DataTable dt = clsData.getContinuousGoodsList(strDate, strDepartment,"1");

        int intQuantity_Stock, intQuantity_Stock1;

        for (int intI = 0; intI < dt.Rows.Count; intI++)
        {
            DataTable dt1 = clsData.UploadBorrowGoodsQuery(dt.Rows[intI]["ID"].ToString());

            DataRow dr = dt_new.NewRow();

            dr["ID"] = dt.Rows[intI]["ID"].ToString();
            dr["Kind"] = dt.Rows[intI]["Kind"].ToString();
            dr["Part_No"] = dt.Rows[intI]["Part_No"].ToString();
            dr["MF_CH"] = dt.Rows[intI]["MF_CH"].ToString();

            dr["Name_En"] = dt.Rows[intI]["Name_En"].ToString();
            dr["Name_CH"] = dt.Rows[intI]["Name_CH"].ToString();
            dr["Custodian"] = dt.Rows[intI]["Custodian"].ToString();

            if (dt1.Rows[0]["Count_ID"].ToString() == "")
                intQuantity_Stock1 = 0;
            else
                intQuantity_Stock1 = Convert.ToInt16(dt1.Rows[0]["Count_ID"].ToString());
            intQuantity_Stock = Convert.ToInt16(dt.Rows[intI]["Quantity_Stock"].ToString()) - intQuantity_Stock1;

            dr["Quantity_Stock"] = intQuantity_Stock.ToString();

            dr["Borrower"] = dt.Rows[intI]["Borrower"].ToString();
            dr["Status"] = dt.Rows[intI]["Status"].ToString();

            dt_new.Rows.Add(dr);

        }

        this.gvwMain1.DataSource = dt_new;
        this.DataBind();

    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;


        //DataTable dt = clsData.getContinuousGoodsList(strDate, strDepartment);

        //this.gvwMain.DataSource = dt;
        //this.DataBind();
        GvQuery();
    }
    #endregion

    #region gvwMain1_PageIndexChanging
    protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;


        //DataTable dt = clsData.getContinuousGoodsList(strDate, strDepartment);

        //this.gvwMain.DataSource = dt;
        //this.DataBind();
        GvQuery1();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strRStatus, strID;
        string strApparatusD;
        string strCount;

        if (e.CommandName == "AddToCart")
        {

            lblName.Text = "";
            lblCustodian.Text = "";
            lblNote.Text = "";
            lblCName.Text = "";
            lblDepartment.Text = "";
            lblExt.Text = "";
            lblEmail.Text = "";
            lblMission.Text = "";
            lblGName.Text = "";
            lblMF.Text = "";
            lblPart_No.Text = "";
            lblCustodian.Text = "";
            //lblNote.Text = "";
            //lblStartDate.Text = "";

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            //strID = ((Label)row.Cells[6].FindControl("lblRStatus")).Text.Trim();

            //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");
            //strApparatusD = dt1.Rows[0]["Name"].ToString();

            //if (strRStatus == "閒置中")   //0217
            //{
            strCount = ((Label)row.Cells[7].FindControl("lblQuantity_Stock")).Text.Trim();
            if (strCount == "0")
            {
                clsMsg.AlertMessage("此貨品已無庫存！", this.Page);
            }
            else
            {
                strID = ((Label)row.Cells[6].FindControl("lblGVSeq")).Text.Trim();
                DataTable dt = clsData.UploadGContinuousQuery(strID);
                if (dt.Rows[0]["ContinuousCount"].ToString().Trim() != "")
                    clsMsg.AlertMessage("此預約記錄已續用過！", this.Page);
                else
                {
                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
                    lblCName.Text = dt.Rows[0]["Borrower"].ToString().Trim();
                    //lblDepartment.Text = dt.Rows[0]["Department"].ToString().Trim();
                    lblExt.Text = dt.Rows[0]["Ext"].ToString().Trim();
                    lblEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                    lblMission.Text = dt.Rows[0]["Mission"].ToString().Trim();
                    lblGName.Text = dt.Rows[0]["GName"].ToString().Trim();
                    //lblStartDate.Text = dt.Rows[0]["StartDate"].ToString().Trim();
                    lblID.Text = strID;
                    lblAID.Text = dt.Rows[0]["ID"].ToString().Trim();
                    //lblEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();

                    lblName.Text = dt.Rows[0]["Name_En"].ToString().Trim() + '-' + dt.Rows[0]["Name_CH"].ToString().Trim();
                    lblMF.Text = dt.Rows[0]["MF_EN"].ToString().Trim() + '-' + dt.Rows[0]["MF_CH"].ToString().Trim();
                    lblPart_No.Text = dt.Rows[0]["Part_No"].ToString().Trim();
                    lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();
                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();

                    DateTime startDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
                    lblEndDate.Text = startDate.ToString("yyyy/MM/dd");

                    startDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
                    DateTime answer = startDate.AddDays(Convert.ToInt16(30));

                    //answer = Convert.ToDateTime(strDateC);
                    lblEnd1.Text  = answer.ToString("yyyy/MM/dd");
                    int intCount1 = Convert.ToInt16(dt.Rows[0]["BorrowedQuantity"].ToString().Trim());

                    ddlCount.Items.Clear();
                    for (int intI = 1; intI < intCount1 + 1; intI++)
                    {
                        ddlCount.Items.Add(new ListItem(intI.ToString(),intI.ToString()));
                        
                    }
                }

                
            }
            //}
            //else
            //{
            //    if (strApparatusD == ddlDepartment.Text)
            //    {
            //        strID = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
            //        DataTable dt = clsData.UploadApparatusQuery(strID, "1", "");
            //        lblName.Text = dt.Rows[0]["Name"].ToString().Trim();
            //        lblProductID.Text = dt.Rows[0]["Products_ID"].ToString().Trim();
            //        lblBrand.Text = dt.Rows[0]["Brand"].ToString().Trim();
            //        lblModel.Text = dt.Rows[0]["Model"].ToString().Trim();
            //        lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();
            //        lblCustodianD.Text = dt.Rows[0]["Custodian_Department"].ToString().Trim();
            //        lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
            //    }
            //    else
            //        clsMsg.AlertMessage("此設備不可外借！", this.Page);
            //}

        }

    }

    protected void gvwMain1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strRStatus, strID;
        string strApparatusD;
        string strCount;

        if (e.CommandName == "AddToCart")
        {

            lblName.Text = "";
            lblCustodian.Text = "";
            lblNote.Text = "";
            lblCName.Text = "";
            lblDepartment.Text = "";
            lblExt.Text = "";
            lblEmail.Text = "";
            lblMission.Text = "";
            lblGName.Text = "";
            lblMF.Text = "";
            lblPart_No.Text = "";
            lblCustodian.Text = "";
            //lblNote.Text = "";
            //lblStartDate.Text = "";

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            //strID = ((Label)row.Cells[6].FindControl("lblRStatus")).Text.Trim();

            //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");
            //strApparatusD = dt1.Rows[0]["Name"].ToString();

            //if (strRStatus == "閒置中")   //0217
            //{
            strCount = ((Label)row.Cells[7].FindControl("lblQuantity_Stock")).Text.Trim();
            if (strCount == "0")
            {
                clsMsg.AlertMessage("此貨品已無庫存！", this.Page);
            }
            else
            {

                strID = ((Label)row.Cells[6].FindControl("lblGVSeq")).Text.Trim();
                DataTable dt = clsData.UploadGContinuousQuery(strID);
                if (dt.Rows[0]["ContinuousCount"].ToString().Trim() != "")
                    clsMsg.AlertMessage("此預約記錄已續用過！", this.Page);
                else
                {
                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
                    lblCName.Text = dt.Rows[0]["Borrower"].ToString().Trim();
                    //lblDepartment.Text = dt.Rows[0]["Department"].ToString().Trim();
                    lblExt.Text = dt.Rows[0]["Ext"].ToString().Trim();
                    lblEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                    lblMission.Text = dt.Rows[0]["Mission"].ToString().Trim();
                    lblGName.Text = dt.Rows[0]["GName"].ToString().Trim();
                    //lblStartDate.Text = dt.Rows[0]["StartDate"].ToString().Trim();
                    lblID.Text = strID;
                    lblAID.Text = dt.Rows[0]["ID"].ToString().Trim();
                    //lblEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();

                    lblName.Text = dt.Rows[0]["Name_En"].ToString().Trim() + '-' + dt.Rows[0]["Name_CH"].ToString().Trim();
                    lblMF.Text = dt.Rows[0]["MF_EN"].ToString().Trim() + '-' + dt.Rows[0]["MF_CH"].ToString().Trim();
                    lblPart_No.Text = dt.Rows[0]["Part_No"].ToString().Trim();
                    lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();
                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();

                    DateTime startDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
                    lblEndDate.Text = startDate.ToString("yyyy/MM/dd");

                    startDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
                    DateTime answer = startDate.AddDays(Convert.ToInt16(30));

                    //answer = Convert.ToDateTime(strDateC);
                    lblEnd1.Text = answer.ToString("yyyy/MM/dd");
                    int intCount1 = Convert.ToInt16(dt.Rows[0]["BorrowedQuantity"].ToString().Trim());

                    for (int intI = 1; intI < intCount1 + 1; intI++)
                    {
                        ddlCount.Items.Add(new ListItem(intI.ToString(), intI.ToString()));

                    }
                }
            }
            //}
            //else
            //{
            //    if (strApparatusD == ddlDepartment.Text)
            //    {
            //        strID = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
            //        DataTable dt = clsData.UploadApparatusQuery(strID, "1", "");
            //        lblName.Text = dt.Rows[0]["Name"].ToString().Trim();
            //        lblProductID.Text = dt.Rows[0]["Products_ID"].ToString().Trim();
            //        lblBrand.Text = dt.Rows[0]["Brand"].ToString().Trim();
            //        lblModel.Text = dt.Rows[0]["Model"].ToString().Trim();
            //        lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();
            //        lblCustodianD.Text = dt.Rows[0]["Custodian_Department"].ToString().Trim();
            //        lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
            //    }
            //    else
            //        clsMsg.AlertMessage("此設備不可外借！", this.Page);
            //}

        }

    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //==========0217
        //if (e.Row.RowType == DataControlRowType.DataRow)    
        //{
        //    if (e.Row.Cells[8].Text == "Y")
        //        e.Row.Cells[8].Text = "可借用";
        //    else
        //        e.Row.Cells[8].Text = "不可借用";

        //}
    }

    protected void gvwMain1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //==========0217
        //if (e.Row.RowType == DataControlRowType.DataRow)    
        //{
        //    if (e.Row.Cells[8].Text == "Y")
        //        e.Row.Cells[8].Text = "可借用";
        //    else
        //        e.Row.Cells[8].Text = "不可借用";

        //}
    }


    protected void butOK_Click(object sender, EventArgs e)
    {
        //DateTime dt;
        DateTime dtS = Convert.ToDateTime("1911/01/01");
        DateTime dtE = Convert.ToDateTime("1911/01/01");
        string strStartDate, strEndDate, strToday, strToday1, strAID;

        DataTable dtMaster = clsData.UploadApparatusMasterQuery("A1", "0");
        string strMaster = dtMaster.Rows[0]["Name"].ToString();

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        strToday1 = DateTime.Now.ToString("yyyy/MM/dd");

        strStartDate = lblEndDate.Text;
        if (strStartDate != "")
        {
            dtS = Convert.ToDateTime(strStartDate);
            strStartDate = dtS.ToString("yyyy/MM/dd hh:mm:ss");
            //strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text + ":00";
        }

        //strEndDate = Request["date2"].ToString();
        //if (strEndDate != "")
        //{
        //    dtE = Convert.ToDateTime(strEndDate);
        //    strEndDate = dtE.ToString("yyyy/MM/dd");
        //    strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text + ":00";
        //}

        //checkDate(strStartDate, strEndDate);
        //if (dtE < dtS)
        //{
        //    clsMsg.AlertMessage("歸還日期不得小於借用日期！", this.Page);
        //}
        //else
        //{
            strAID = lblAID.Text;


            if ((strAID != "") || (strAID != null))
            {
                //if (strEndDate != "")
                //{
                    //DataTable dt3 = clsData.UploadApparatusQuery(strAID, "1", "");
                    //string strStatus1;
                    //if (dt3.Rows.Count == 0)
                    //    strStatus1 = "";
                    //else
                    //    strStatus1 = dt3.Rows[0]["ReservationStatus"].ToString();
                    //if ((strStatus1 != "不可借用") && (strStatus1 != "校驗中") && (strStatus1 != "異常維修中"))
                    //{

                        //DataTable dt2 = clsData.UploadReservationRepeat(strID, strToday, ddlDepartment.Text);

                        //if (dt2.Rows.Count == 0)
                        //{
                        //if (checkDate1(strStartDate, strEndDate) == true)
                        //{

                            //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, lblEnd1.Text, strAID);

                            //if ((dt1.Rows[0]["Borrower"].ToString() == lblCName.Text) && (dt1.Rows[0]["Department"].ToString() == lblDepartment.Text) && (dt1.Rows[0]["Ext"].ToString() == lblExt.Text))
                            //{
                                if (clsTransaction.UpDateGoodsContinuousDate(lblID.Text, lblEnd1.Text, ddlCount.Text) == true)
                                {
                                    //if (clsTransaction.UpDateApparatusStatus("借用中", strID) == true)    //0217
                                    //{
                                    //string strMaxID;

                                    //dt1 = clsData.UploadMaxReservation();
                                    //strMaxID=dt1.Rows[0]["ID"].ToString();
                                    //DateTime startDate = Convert.ToDateTime(strStartDate);
                                    //DateTime endDate = Convert.ToDateTime(strEndDate);
                                    //while (startDate < endDate)
                                    //{
                                    //    string strDateW;

                                    //    strDateW = startDate.ToString("yyyy/MM/dd") + "(" + startDate.DayOfWeek.ToString() + ")";

                                    //    clsTransaction.InsertReservationDate(strMaxID, strDateW);

                                    //    startDate = startDate.AddDays(1);

                                    //}

                                    MailData();
                                    clsMsg.AlertMessage("續約成功！", this.Page);
                                    setEmpty();
                                    //}
                                    //else
                                    //    clsMsg.AlertMessage("預約失敗！", this.Page);

                                }
                                else
                                    clsMsg.AlertMessage("續約失敗！", this.Page);
                            //}
                            //else
                            //    clsMsg.AlertMessage("此時段已被預約！", this.Page);
                        //}
                        //else
                        //    clsMsg.AlertMessage("預約天數上限為7天！", this.Page);
                        //}
                        //else
                        //    clsMsg.AlertMessage("此設備貴部門尚在使用中，請使用結束後再進行預約！", this.Page);
                    //}
                    //else
                    //    clsMsg.AlertMessage("此貨品不得外借，請洽負責人！", this.Page);
                //}
                //else
                //    clsMsg.AlertMessage("*為必填欄位....", this.Page);
            }
            else
                clsMsg.AlertMessage("請選擇貨品！", this.Page);
        //}

    }

    private void setEmpty()
    {
        //txtSearch.Text = "";
        this.gvwMain.DataSource = null;
        this.DataBind();
        lblName.Text = "";
        lblCustodian.Text = "";
        lblCName.Text = "";
        lblExt.Text = "";
        lblEmail.Text = "";
        txtNote.Text = "";
        strStart = "";
        strStart1 = "";
        lblMission.Text = "";
        lblGName.Text = "";
        lblAID.Text = "";
        lblEndDate.Text = "";
        lblID.Text = "";
        lblMF.Text = "";
        lblPart_No.Text = "";
        lblNote.Text = "";
        lblEnd1.Text = "";
        ddlCount.Items.Clear();
    }

    #region MailData
    private void MailData()
    {
        #region 宣告變數

        DateTime dt;
        //string To = "";
        //double intDailyTime = 0;
        //string emp_name = "";
        //string emp_no = "@_@";
        //string strDept_id = "@_@";
        //string strDaily = "";
        //int seq = 0;        //計算總筆數
        //int seqOK = 0;    //計算成功筆數 

        #endregion

        #region mail config

        //mail標題
        string MailSubject = "續用貨品通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_GoodsContinuous.txt");
        string strMailBody = myMailBody.ReadToEnd();

        //預設標準時數
        //int defulDailyTime = Convert.ToInt16(WebConfigurationManager.AppSettings["checkDailyTime"]);

        #endregion

        #region 找資料塞到SendMail內

        //找出工作日誌那幾天時數不符
        //if (!txtEmpNo.Text.Equals(""))
        //    emp_no = txtEmpNo.Text;
        //if (!ddlDept.SelectedValue.Equals(""))
        //    strDept_id = ddlDept.SelectedValue;
        //DataTable dt = clsData.getNotInputDaily(emp_no, strDept_id, txtDateS.Text, txtDateE.Text);
        //if (dt.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        intDailyTime = Convert.ToDouble(dr["dailytime"].ToString());
        //        if (intDailyTime < defulDailyTime)
        //        {
        //            seq += 1;
        //            To = dr["e_mail"].ToString();
        //            emp_name = dr["emp_name"].ToString();
        //            strDaily = dr["DailyDate"].ToString();
        //            if (To.Length > 0)
        //            {

        DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "1");
        string strMail = dt1.Rows[0]["Email"].ToString();

        //string strMail = txtEmail.Text;
        string strName = lblDepartment.Text + "-" + lblCName.Text + "(" + lblExt.Text + ")" ;
        string strStartDate, strEndDate;
        string strApparatus;

        strApparatus = lblName.Text + "，續用數量：" + ddlCount.Text;

        //strStartDate = Request["date1"].ToString();
        //if (strStartDate != "")
        //{
        //    dt = Convert.ToDateTime(strStartDate);
        //    strStartDate = dt.ToString("yyyy/MM/dd");
        //    strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text;
        //}
        //strStartDate = strStartDate + strApparatus;

        //strEndDate = Request["date2"].ToString();
        //if (strEndDate != "")
        //{
        //    dt = Convert.ToDateTime(strEndDate);
        //    strEndDate = dt.ToString("yyyy/MM/dd");
        //    strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text;
        //}

        string strBody = string.Format(strMailBody, strName, strApparatus, lblEnd1.Text , "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

        clsTransaction.SendMail(strMail, MailSubject, strBody);
        //seqOK += 1;
        //            }
        //        }
        //    }
        //    clsMsg.AlertMessage("Mail發送成功！總筆數：" + seq + "筆、成功筆數：" + seqOK + "筆", this.Page);
        //}
        //else
        //{
        //    clsMsg.AlertMessage("查無符合資訊！", this.Page);
        //}
        myMailBody.Close();
        myMailBody.Dispose();
        #endregion
    }
    #endregion

    private bool checkDate1(string strStart, string strEnd)
    {
        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        DateTime startDate1 = startDate;
        int intHoliday = 0;

        TimeSpan Total = endDate.Subtract(startDate);

        if (Total.TotalDays > 8)
            return false;
        else
            return true;

    }

    private bool checkDate(string strStart, string strEnd)
    {
        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        DateTime startDate1 = startDate;
        int intHoliday = 0;

        while (startDate < endDate)
        {
            if (((int)startDate.DayOfWeek == 0) || ((int)startDate.DayOfWeek == 6))
            {
                intHoliday += 1;
            }
            startDate = startDate.AddDays(1);

        }
        //TimeSpan Total = new TimeSpan(startDate1.Ticks - endDate.Ticks);

        TimeSpan Total = endDate.Subtract(startDate1);
        string strTotal = Total.TotalDays.ToString();
        //int intTotal = Convert.ToInt32(strTotal.ToString());
        double dTotal = Convert.ToDouble(strTotal);
        //if (strTotal. >= 5)

        if (intHoliday > 0)
        {
            if (intHoliday == 1)
            {
                if (dTotal > 6)
                    return false;
            }
            else
            {
                if (dTotal > 7)
                    return false;
            }
        }
        else
        {
            if (dTotal > 5)
                return false;
        }

        return true;

    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/GoodsReservationMain.aspx");
    }
}
