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

public partial class WebForm_GoodsReservation : System.Web.UI.Page
{
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
        {
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");

        }
        if (!IsPostBack)
        {
            loadKind(this.ddlKind);
            loadDepartment(this.ddlDepartment);
            //ddlHourB.Text = "09";
            //ddlHourR.Text = "18";
            //ddlMinR.Text = "30";
            DataTable dt1 = clsData.UploadApparatusMasterQuery("A3", "0");

            string strNumber;
            dt1 = clsData.UploadWorkTimeQuery("A2S");
            strNumber = dt1.Rows[0]["Name"].ToString();
            string[] strNumber1 = strNumber.Split(':');
            //ddlHourB.Text = strNumber1[0];
            //ddlMinB.Text = strNumber1[1];

            dt1 = clsData.UploadWorkTimeQuery("A2E");
            strNumber = dt1.Rows[0]["Name"].ToString();
            strNumber1 = strNumber.Split(':');
            //ddlHourR.Text = strNumber1[0];
            //ddlMinR.Text = strNumber1[1];

            //ddlHourR.Enabled = false;
            //ddlMinR.Enabled = false;
            //loadCustodian(this.ddlCustodian);
            //clsParameter.strUpload_Kind = "Apparatus";
            if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
            {
                //txtDepartment.Enabled = true;
                ddlDepartment.Enabled = true;
                ddlDepartment.Text = Session["AppNo"].ToString();
            }
            else
            {
                //txtDepartment.Text = clsParameter.strAppNo;
                //txtDepartment.Enabled = false;
                ddlDepartment.Text = Session["AppNo"].ToString();
                ddlDepartment.Enabled = false;

            }
        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 10, "0");
    }
    #endregion 

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlDepartment(DDL, Session["AppNo"].ToString(),"0");
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;

        //DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);

        //this.gvwMain.DataSource = dt;
        //this.DataBind();
        GvQuery();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strStatus;
        string strGoodsD;
        string strCount;

        if (e.CommandName == "AddToCart")
        {

            lblName.Text = "";
            lblMF.Text = "";
            lblPart_No.Text = "";
            lblCustodian.Text = "";
            lblNote.Text = "";

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            strStatus = ((Label)row.Cells[6].FindControl("lblStatus")).Text.Trim();
            lblDays.Text = ((Label)row.Cells[7].FindControl("lblDate")).Text.Trim();

            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");
            strGoodsD = dt1.Rows[0]["Name"].ToString();
            strCount = ((Label)row.Cells[7].FindControl("lblQuantity_Stock")).Text.Trim();

            if (strStatus == "閒置中")   //0217
            {
 
                if (strCount == "0")
                {
                    clsMsg.AlertMessage("此貨品已無庫存！", this.Page);
                }
                else
                {
                    Session["Goods_ID"] = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                    DataTable dt = clsData.UploadGoodsQuery(Session["Goods_ID"].ToString(), "1", "");
                    lblName.Text = dt.Rows[0]["Name_En"].ToString().Trim() + '-' + dt.Rows[0]["Name_CH"].ToString().Trim();
                    lblMF.Text = dt.Rows[0]["MF_EN"].ToString().Trim() + '-' + dt.Rows[0]["MF_CH"].ToString().Trim();
                    lblPart_No.Text = dt.Rows[0]["Part_No"].ToString().Trim();
                    lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();
                    DataTable dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString().Trim());

                    if (dt2.Rows.Count > 0)
                    {
                        lblCustodian.Text = dt2.Rows[0]["Name_CH"].ToString().Trim();
                        lblCustodianExt.Text = dt2.Rows[0]["Extension"].ToString().Trim();
                    }
                    else
                    {
                        lblCustodian.Text = "";
                        lblCustodianExt.Text = "";
                    }

                    dt2 = clsData.getEmployees("1", dt.Rows[0]["Agent"].ToString().Trim());

                    if (dt2.Rows.Count > 0)
                    {
                        lblAgent.Text = dt2.Rows[0]["Name_CH"].ToString().Trim();
                        lblAgentExt.Text = dt2.Rows[0]["Extension"].ToString().Trim();
                    }
                    else
                    {
                        lblAgent.Text = "";
                        lblAgentExt.Text = "";
                    }
                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();

                    for (int intI = 1 ; intI <= Convert.ToInt16(strCount) ; intI++)
                    {
                        ddlCount.Items.Add(new ListItem(intI.ToString(),intI.ToString()));
                    }




                }
            }
            else
            {
                if (strGoodsD == ddlDepartment.Text)
                {
                    Session["ApparatusID"] = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                    DataTable dt = clsData.UploadGoodsQuery(Session["ApparatusID"].ToString(), "1", "");
                    lblName.Text = dt.Rows[0]["Name_En"].ToString().Trim() + '-' + dt.Rows[0]["Name_CH"].ToString().Trim();
                    lblMF.Text = dt.Rows[0]["MF_EN"].ToString().Trim() + '-' + dt.Rows[0]["MF_CH"].ToString().Trim();
                    lblPart_No.Text = dt.Rows[0]["Part_No"].ToString().Trim();
                    lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();

                    DataTable dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString().Trim());

                    if (dt2.Rows.Count > 0)
                    {
                        lblCustodian.Text = dt2.Rows[0]["Name_CH"].ToString().Trim();
                        lblCustodianExt.Text = dt2.Rows[0]["Extension"].ToString().Trim();
                    }
                    else
                    {
                        lblCustodian.Text = "";
                        lblCustodianExt.Text = "";
                    }

                    dt2 = clsData.getEmployees("1", dt.Rows[0]["Agent"].ToString().Trim());

                    if (dt2.Rows.Count > 0)
                    {
                        lblAgent.Text = dt2.Rows[0]["Name_CH"].ToString().Trim();
                        lblAgentExt.Text = dt2.Rows[0]["Extension"].ToString().Trim();
                    }
                    else
                    {
                        lblAgent.Text = "";
                        lblAgentExt.Text = "";
                    }

                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();

                    for (int intI = 1; intI <= Convert.ToInt16(strCount); intI++)
                    {
                        ddlCount.Items.Add(new ListItem(intI.ToString(), intI.ToString()));
                    }

                    
                }
                else
                    clsMsg.AlertMessage("此貨品不可外借！", this.Page);
            }

        }

    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //==========0217
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            DataTable dt = clsData.getEmployees("1", e.Row.Cells[3].Text);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Name_CH"].ToString() != "")
                {
                    e.Row.Cells[3].Text = dt.Rows[0]["Name_CH"].ToString();
                    //e.Row.Cells[5].Text = dt.Rows[0]["Extension"].ToString();
                }
            }
            else
            {
                e.Row.Cells[3].Text = "";
                //e.Row.Cells[5].Text = "";
            }

            //if (e.Row.Cells[4].Text == "")
            //    e.Row.Cells[4].Text = "0";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GvQuery();
     
    }

    private void GvQuery()
    {
        //DataTable dt_new = new DataTable("dt_new");

        //DataColumn column1 = new DataColumn("ID");
        //column1.DataType = System.Type.GetType("System.String");
        //column1.AllowDBNull = true;
        //column1.Caption = "ID";
        //column1.DefaultValue = "0";
        //dt_new.Columns.Add(column1);

        //DataColumn column2 = new DataColumn("Kind");
        //column2.DataType = System.Type.GetType("System.String");
        //column2.AllowDBNull = true;
        //column2.Caption = "Kind";
        //column2.DefaultValue = "0";
        //dt_new.Columns.Add(column2);

        //DataColumn column3 = new DataColumn("MF_EN");
        //column3.DataType = System.Type.GetType("System.String");
        //column3.AllowDBNull = true;
        //column3.Caption = "MF_EN";
        //column3.DefaultValue = "0";
        //dt_new.Columns.Add(column3);

        //DataColumn column4 = new DataColumn("MF_CH");
        //column4.DataType = System.Type.GetType("System.String");
        //column4.AllowDBNull = true;
        //column4.Caption = "MF_CH";
        //column4.DefaultValue = "0";
        //dt_new.Columns.Add(column4);

        //DataColumn column5 = new DataColumn("Name_En");
        //column5.DataType = System.Type.GetType("System.String");
        //column5.AllowDBNull = true;
        //column5.Caption = "Name_En";
        //column5.DefaultValue = "0";
        //dt_new.Columns.Add(column5);

        //DataColumn column6 = new DataColumn("Name_CH");
        //column6.DataType = System.Type.GetType("System.String");
        //column6.AllowDBNull = true;
        //column6.Caption = "Name_CH";
        //column6.DefaultValue = "0";
        //dt_new.Columns.Add(column6);

        //DataColumn column7 = new DataColumn("Custodian");
        //column7.DataType = System.Type.GetType("System.String");
        //column7.AllowDBNull = true;
        //column7.Caption = "Custodian";
        //column7.DefaultValue = "0";
        //dt_new.Columns.Add(column7);

        //DataColumn column8 = new DataColumn("Quantity_Stock");
        //column8.DataType = System.Type.GetType("System.String");
        //column8.AllowDBNull = true;
        //column8.Caption = "Quantity_Stock";
        //column8.DefaultValue = "0";
        //dt_new.Columns.Add(column8);

        //DataColumn column9 = new DataColumn("Place");
        //column9.DataType = System.Type.GetType("System.String");
        //column9.AllowDBNull = true;
        //column9.Caption = "Place";
        //column9.DefaultValue = "0";
        //dt_new.Columns.Add(column9);

        //DataColumn column10 = new DataColumn("Status");
        //column10.DataType = System.Type.GetType("System.String");
        //column10.AllowDBNull = true;
        //column10.Caption = "Status";
        //column10.DefaultValue = "0";
        //dt_new.Columns.Add(column10);

        //DataColumn column11 = new DataColumn("Products_ID");
        //column11.DataType = System.Type.GetType("System.String");
        //column11.AllowDBNull = true;
        //column11.Caption = "Products_ID";
        //column11.DefaultValue = "0";
        //dt_new.Columns.Add(column11);

        //DataColumn column12 = new DataColumn("Part_No");
        //column12.DataType = System.Type.GetType("System.String");
        //column12.AllowDBNull = true;
        //column12.Caption = "Part_No";
        //column12.DefaultValue = "0";
        //dt_new.Columns.Add(column12);

        //DataColumn column13 = new DataColumn("Check_Date");
        //column13.DataType = System.Type.GetType("System.String");
        //column13.AllowDBNull = true;
        //column13.Caption = "Check_Date";
        //column13.DefaultValue = "0";
        //dt_new.Columns.Add(column13);

        //DataTable dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", ddlKind.Text);

        //int intQuantity_Stock, intQuantity_Stock1, intQuantity_Stock2;

        //for (int intI = 0; intI < dt.Rows.Count; intI++)
        //{
        ////    DataTable dt1 = clsData.UploadBorrowGoodsQuery(dt.Rows[intI]["ID"].ToString());

        //    DataRow dr = dt_new.NewRow();

        //    dr["ID"] = dt.Rows[intI]["ID"].ToString();
        //    dr["Kind"] = dt.Rows[intI]["Kind"].ToString();
        //    dr["MF_EN"] = dt.Rows[intI]["MF_EN"].ToString();
        //    dr["MF_CH"] = dt.Rows[intI]["MF_CH"].ToString();

        //    dr["Name_En"] = dt.Rows[intI]["Name_En"].ToString();
        //    dr["Name_CH"] = dt.Rows[intI]["Name_CH"].ToString();
        //    dr["Custodian"] = dt.Rows[intI]["Custodian"].ToString();

        //    //if (dt1.Rows.Count == 0)
        //    //    intQuantity_Stock = 0;
        //    //else
        //    //    intQuantity_Stock = Convert.ToInt16(dt1.Rows[intI]["Count_ID"].ToString());

        //    //if ((dt1.Rows[intI]["Count_ID"].ToString() == null) || dt1.Rows[intI]["Count_ID"].ToString() == "")
        //    //    intQuantity_Stock = 0;
        //    //else
        //    //    intQuantity_Stock = Convert.ToInt16(dt1.Rows[intI]["Count_ID"].ToString());

        //    //if (dt1.Rows[0]["Count_ID"].ToString() == "")
        //    //    intQuantity_Stock1 = 0;
        //    //else
        //    //    intQuantity_Stock1 = Convert.ToInt16(dt1.Rows[0]["Count_ID"].ToString());

        //    //if (dt.Rows[intI]["Quantity_Stock"].ToString() == "")
        //    //    intQuantity_Stock2 = 0;
        //    //else
        //    //    intQuantity_Stock2 = Convert.ToInt16(dt.Rows[intI]["Quantity_Stock"].ToString());

        //    //intQuantity_Stock = intQuantity_Stock2 - intQuantity_Stock1;

        //    if (dt.Rows[intI]["Quantity_Stock"].ToString() == "")
        //        dr["Quantity_Stock"] = "0";
        //    else
        //        dr["Quantity_Stock"] = dt.Rows[intI]["Quantity_Stock"].ToString();

        //    dr["Place"] = dt.Rows[intI]["Place"].ToString();
        //    dr["Status"] = dt.Rows[intI]["Status"].ToString();
        //    dr["Products_ID"] = dt.Rows[intI]["Products_ID"].ToString();
        //    dr["Part_No"] = dt.Rows[intI]["Part_No"].ToString();

        //    dt_new.Rows.Add(dr);

        //}
        DataTable dt = clsData.UploadGoodsQuery(txtSearch.Text, "0", ddlKind.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
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

        StreamReader myMailBody;
        DataTable dt1;
        string strMail;
        //mail標題
        string MailSubject = "貨品預約通知";

        for (int intI = 0; intI < 2; intI++)
        {
            if (intI == 0)
            {
                //MAIL內容
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body.txt");
                //dt1 = clsData.UploadApparatusMasterQuery("A3", "1");
                if (Session["EmpDepartment"] == "DA40")
                    dt1 = clsData.UploadApparatusMasterQuery("A3T", "1");
                else
                    dt1 = clsData.UploadApparatusMasterQuery("A3W", "1");
                strMail = dt1.Rows[0]["Email"].ToString();
            }
            else
            {
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body3.txt", System.Text.Encoding.Default);

                strMail = txtAgentEmail.Text.Trim();

            }
            //預設標準時數
            //int defulDailyTime = Convert.ToInt16(WebConfigurationManager.AppSettings["checkDailyTime"]);
            string strMailBody = myMailBody.ReadToEnd();

            #region 找資料塞到SendMail內





            //string strMail = txtEmail.Text;
            string strName = ddlDepartment.Text + "-" + txtName.Text + "(" + txtExt.Text + ")";
            string strStartDate, strEndDate;
            string strApparatus;

            strApparatus = lblName.Text;

            strStartDate = Request["date1"].ToString();
            if (strStartDate != "")
            {
                dt = Convert.ToDateTime(strStartDate);
                strStartDate = dt.ToString("yyyy/MM/dd");
                //strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text;
            }
            //strStartDate = strStartDate + strApparatus;

            //strEndDate = Request["date2"].ToString();
            //if (strEndDate != "")
            //{
            //    dt = Convert.ToDateTime(strEndDate);
            //    strEndDate = dt.ToString("yyyy/MM/dd");
            //    //strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text;
            //}
            strEndDate = lblDate.Text;

            string strBody = string.Format(strMailBody, strName, strApparatus, strStartDate, strEndDate, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

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
    }
    #endregion

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

    private void setEmpty()
    {
        txtSearch.Text = "";
        this.gvwMain.DataSource = null;
        this.DataBind();
        lblName.Text = "";
        lblMF.Text = "";
        lblPart_No.Text = "";
        lblCustodian.Text = "";
        lblNote.Text = "";
        txtName.Text = "";
        txtExt.Text = "";
        txtEmail.Text = "";
        txtNote.Text = "";
        strStart = "";
        strStart1 = "";
        txtMission.Text = "";
        txtGName.Text = "";
        ddlCount.Items.Clear();
        lblCustodianExt.Text = "";
        lblAgent.Text = "";
        lblAgentExt.Text = "";
        txtAgent.Text = "";
        txtAgentEmail.Text = "";
        txtAgentExt.Text = "";

    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        //DateTime dt;
        string strStock;
        DateTime dtS = Convert.ToDateTime("1911/01/01");
        DateTime dtE = Convert.ToDateTime("1911/01/01");
        string strStartDate, strEndDate, strToday, strToday1;

        //DataTable dtMaster = clsData.UploadApparatusMasterQuery("A3", "0");
        DataTable dtMaster;
        if (Session["EmpDepartment"] == "DA40")
            dtMaster = clsData.UploadApparatusMasterQuery("A3T", "0");
        else
            dtMaster = clsData.UploadApparatusMasterQuery("A3W", "0");
        string strMaster = dtMaster.Rows[0]["Name"].ToString();

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        strToday1 = DateTime.Now.ToString("yyyy/MM/dd");

        strStartDate = Request["date1"].ToString();
        if (strStartDate != "")
        {
            dtS = Convert.ToDateTime(strStartDate);
            strStartDate = dtS.ToString("yyyy/MM/dd");
            //strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text + ":00";
        }

        //strEndDate = Request["date2"].ToString();
        //if (strEndDate != "")
        //{
        //    dtE = Convert.ToDateTime(strEndDate);
        //    strEndDate = dtE.ToString("yyyy/MM/dd");
        //    //strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text + ":00";
        //}

        //checkDate(strStartDate, strEndDate);
        if (lblDate.Text =="")
        {
            clsMsg.AlertMessage("請點選換算日期！", this.Page);
        }
        else
        {
            if (Session["EmpName"].ToString() == strMaster)
            {
                if ((Session["Goods_ID"].ToString() != "") || (Session["Goods_ID"].ToString() != null))
                {
                    if ((txtName.Text.Trim() != "") && (txtExt.Text.Trim() != "") && (txtEmail.Text.Trim() != "") && (strStartDate != "") && (lblDate.Text != "") && (ddlDepartment.Text != ""))
                    {

                        DataTable dt1 = clsData.UploadGoodsQuery(Session["Goods_ID"].ToString(), "1", "");

                        if (dt1.Rows[0]["Quantity_Stock"].ToString() !="0")
                        {
                            //if (clsTransaction.InsertApparatusReservation(Session["Goods_ID"].ToString(), strStartDate, lblDate.Text, txtName.Text, ddlDepartment.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", lblCustodian.Text, "", "", ddlCount.Text, txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text, "", "","") == true)
                            if (clsTransaction.InsertGoodsReservation(Session["Goods_ID"].ToString(), strStartDate, lblDate.Text, txtName.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "","","",ddlCount .Text ,"",txtAgent.Text,txtAgentExt.Text,txtAgentEmail.Text,"","","","") == true)
                            {
                                //strStock = (Convert.ToInt16(dt1.Rows[0]["Quantity_Stock"].ToString()) - Convert.ToInt16(ddlCount.Text)).ToString();
                                //if (clsTransaction.UpDateGoodsQuantityStock(strStock, Session["Goods_ID"].ToString()) == true)  //0217
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
                                    clsMsg.AlertMessage("預約成功！", this.Page);
                                    setEmpty();
                                //}
                                //else
                                //    clsMsg.AlertMessage("預約失敗！", this.Page);

                            }
                            else
                                clsMsg.AlertMessage("預約失敗！", this.Page);
                        }
                        else
                            clsMsg.AlertMessage("此貨品庫存為0！", this.Page);

                    }
                    else
                        clsMsg.AlertMessage("*為必填欄位....", this.Page);
                }
                else
                    clsMsg.AlertMessage("請選擇貨品！", this.Page);
            }
            else
            {

                if ((Session["Goods_ID"].ToString() != "") || (Session["Goods_ID"].ToString() != null))
                {
                    if ((txtName.Text.Trim() != "") && (txtExt.Text.Trim() != "") && (txtEmail.Text.Trim() != "") && (strStartDate != "") && (lblDate.Text != "") && (ddlDepartment.Text != ""))
                    {
                        //DataTable dt3 = clsData.UploadApparatusStatus(Session["Goods_ID"].ToString(), strToday1);
                        //string strStatus1;
                        //if (dt3.Rows.Count == 0)
                        //    strStatus1 = "";
                        //else
                        //    strStatus1 = dt3.Rows[0]["Status"].ToString();
                        //if (strStatus1 != "Y")
                        //{

                            //DataTable dt2 = clsData.UploadReservationRepeat(Session["Goods_ID"].ToString(), strToday, ddlDepartment.Text);

                            //if (dt2.Rows.Count == 0)
                            //{
                                //if (checkDate1(strStartDate, strEndDate) == true)
                                //{

                                    DataTable dt1 = clsData.UploadGoodsQuery(Session["Goods_ID"].ToString(), "1", "");

                                    if (dt1.Rows[0]["Quantity_Stock"].ToString() != "0")
                                    {
                                        //if (clsTransaction.InsertApparatusReservation(Session["Goods_ID"].ToString(), strStartDate, lblDate.Text, txtName.Text, ddlDepartment.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", lblCustodian.Text, "", "", ddlCount.Text, txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text, "", "","") == true)
                                        if (clsTransaction.InsertGoodsReservation(Session["Goods_ID"].ToString(), strStartDate, lblDate.Text, txtName.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", "", "", ddlCount.Text, "", txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text, "", "", "", "") == true)
                                        {
                                            if (clsTransaction.UpDateApparatusStatus("借用中", Session["Goods_ID"].ToString()) == true)    //0217
                                            {
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
                                                clsMsg.AlertMessage("預約成功！", this.Page);
                                                setEmpty();
                                            }
                                            else
                                                clsMsg.AlertMessage("預約失敗！", this.Page);

                                        }
                                        else
                                            clsMsg.AlertMessage("預約失敗！", this.Page);
                                    }
                                    else
                                        clsMsg.AlertMessage("此貨品庫存為0！", this.Page);
                                //}
                                //else
                                //    clsMsg.AlertMessage("預約天數上限為7天！", this.Page);
                            //}
                            //else
                            //    clsMsg.AlertMessage("此設備貴部門尚在使用中，請使用結束後再進行預約！", this.Page);
                        //}
                        //else
                        //    clsMsg.AlertMessage("此貨品尚未歸還！", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("*為必填欄位....", this.Page);
                }
                else
                    clsMsg.AlertMessage("請選擇貨品！", this.Page);
            }
        }
    }

    private bool checkDate1(string strStart, string strEnd)
    {
        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        DateTime startDate1 = startDate;
        int intHoliday = 0;

        TimeSpan Total = endDate.Subtract(startDate);

        if (Total.TotalDays > 7)
            return false;
        else
            return true;

    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/GoodsReservationMain.aspx");
    }

    protected void btnDate_Click(object sender, EventArgs e)
    {
        string strDateC;

        if (Request["date1"].ToString() == "")
        {
            clsMsg.AlertMessage("請選擇領用日期！", this.Page);
        }
        else
        {
            DateTime answer;
            DateTime startDate = Convert.ToDateTime(Request["date1"].ToString());

            if (lblDays.Text.Trim() == "")
                lblDays.Text = "180";
            else if (DateTime.TryParse(lblDays.Text, out answer))
                lblDays.Text = "180";

            answer = startDate.AddDays(Convert.ToInt16(lblDays.Text));

            //answer = Convert.ToDateTime(strDateC);
            strDateC = answer.ToString("yyyy/MM/dd");
            lblDate.Text = strDateC;
            strStart = startDate.ToString("yyyy/MM/dd");


        }
    }
}
