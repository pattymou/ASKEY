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

public partial class WebForm_ApparatusReservation : System.Web.UI.Page
{
    //public static string strID;
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //{
        //    if ((clsParameter.strAppNo == "") || (clsParameter.strAppNo == null))
        //        Response.Redirect("~/SystemDefault.aspx");

        //}
        if (Session["EmpNo"] == null)
        {
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");

        }


        if (!IsPostBack)
        {
            loadKind(this.ddlKind);
            loadDepartment(this.ddlDepartment);
            loadCustomer(this.ddlCustomer);
            //ddlHourB.Text = "09";
            //ddlHourR.Text = "18";
            //ddlMinR.Text = "30";
            //rdoTime.Checked = true;
            rdoUse.Checked = true;
            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "0");

            string strNumber, strNumber1;
            dt1 = clsData.UploadWorkTimeQuery("A2S");
            strNumber = dt1.Rows[0]["Name"].ToString();
            //string[] strNumber1 = strNumber.Split(':');
            //ddlHourB.Text = strNumber1[0];
            //ddlMinB.Text = strNumber1[1];

            dt1 = clsData.UploadWorkTimeQuery("A2E");
            strNumber1 = dt1.Rows[0]["Name"].ToString();
            //strNumber1 = strNumber.Split(':');
            //ddlHourR.Text = strNumber1[0];
            //ddlMinR.Text = strNumber1[1];

            //lblTime.Text = strNumber + "~" + strNumber1;
            //lblTime1.Text = strNumber1 + "~" + strNumber;

            DataTable dt = clsData.UploadNumber(Session["AppNo"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString().Trim();
                txtExt.Text = dt.Rows[0]["Ext"].ToString().Trim();
                txtEmail.Text = dt.Rows[0]["Mail"].ToString().Trim();
                ddlDepartment.Text = dt.Rows[0]["Department"].ToString().Trim();
            }
            //ddlHourR.Enabled = false;
            //ddlMinR.Enabled = false;
            //loadCustodian(this.ddlCustodian);
            //clsParameter.strUpload_Kind = "Apparatus";
            if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
            {
                //txtDepartment.Enabled = true;
                //ddlDepartment.Enabled = true;
                ddlDepartment.Text = Session["AppNo"].ToString();
            }
            else
            {
                //txtDepartment.Text = clsParameter.strAppNo;
                //txtDepartment.Enabled = false;
                ddlDepartment.Text = Session["AppNo"].ToString();
                //ddlDepartment.Enabled = false;
                txtName.Enabled = false;
                txtEmail.Enabled = false;
                txtExt.Enabled = false;

            }
        }
    }

    #region loadCustomer
    protected void loadCustomer(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 1, "0");
    }
    #endregion

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7, "0");
    }
    #endregion

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlDepartment(DDL, Session["AppNo"].ToString(), "0");
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;

        //=========0217
        //DataTable dt = clsData.UploadApparatusStatus(txtSearch.Text, "0", ddlKind.Text);
        //=========0217

        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);

        this.gvwMain.DataSource = dt;
        this.DataBind();
        //GvQuery();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strRStatus;
        string strApparatusD;

        if (e.CommandName == "AddToCart1")
        {
            string strID;

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('ReservationView_jq.aspx?ID=" + strID + "&Kind=0');</script>");

        }

        if (e.CommandName == "AddToCart")
        {
            clsMsg.AlertMessage("請先與設備保管人確認預約時間！", this.Page);
            lblName.Text = "";
            lblProductID.Text = "";
            lblBrand.Text = "";
            lblModel.Text = "";
            lblCustodian.Text = "";
            //lblCustodianD.Text = "";
            lblNote.Text = "";

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            strRStatus = ((Label)row.Cells[6].FindControl("lblRStatus")).Text.Trim();

            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");
            strApparatusD = dt1.Rows[0]["Name"].ToString();

            //if ((strRStatus == "閒置中") || (strRStatus == "借用中") || (strRStatus == "可借用"))   //0217
            if ((strRStatus == "閒置中") || (strRStatus == "借用中") || (strRStatus == "") || (strRStatus == "可借用")) 
            {
                Session["ApparatusID"] = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                lblAID.Text = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                DataTable dt = clsData.UploadApparatusQuery(Session["ApparatusID"].ToString(), "1", "");
                lblName.Text = dt.Rows[0]["Name"].ToString().Trim();
                lblProductID.Text = dt.Rows[0]["Products_ID"].ToString().Trim();
                lblBrand.Text = dt.Rows[0]["Brand"].ToString().Trim();
                lblModel.Text = dt.Rows[0]["Model"].ToString().Trim();
                lblPrice.Text = dt.Rows[0]["Price_Use"].ToString().Trim();

                DataTable dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString().Trim());

                if (dt2.Rows.Count > 0)
                {
                    lblCustodian.Text = dt2.Rows[0]["Name_CH"].ToString().Trim();
                    lblCustodianExt.Text = dt2.Rows[0]["Extension"].ToString().Trim();
                    lblCMail.Text = dt2.Rows[0]["Email"].ToString().Trim();
                }
                else
                {
                    lblCustodian.Text = "";
                    lblCustodianExt.Text = "";
                    lblCMail.Text = "";
                }

                dt2 = clsData.getEmployees("1", dt.Rows[0]["Agent"].ToString().Trim());

                if (dt2.Rows.Count > 0)
                {
                    lblAgent.Text = dt2.Rows[0]["Name_CH"].ToString().Trim();
                    lblAgentExt.Text = dt2.Rows[0]["Extension"].ToString().Trim();
                    lblAMail.Text = dt2.Rows[0]["Email"].ToString().Trim();
                }
                else
                {
                    lblAgent.Text = "";
                    lblAgentExt.Text = "";
                    lblAMail.Text = "";
                }
                //lblCustodianD.Text = dt.Rows[0]["Custodian_Department"].ToString().Trim();
                lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
            }
            else
            {
                if (strApparatusD == Session["AppNo"].ToString())
                {
                    lblAID.Text = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                    Session["ApparatusID"] = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                    DataTable dt = clsData.UploadApparatusQuery(Session["ApparatusID"].ToString(), "1", "");
                    lblName.Text = dt.Rows[0]["Name"].ToString().Trim();
                    lblProductID.Text = dt.Rows[0]["Products_ID"].ToString().Trim();
                    lblBrand.Text = dt.Rows[0]["Brand"].ToString().Trim();
                    lblModel.Text = dt.Rows[0]["Model"].ToString().Trim();
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
                    //lblCustodianD.Text = dt.Rows[0]["Custodian_Department"].ToString().Trim();
                    lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
                }
                else
                    clsMsg.AlertMessage("此設備不可外借！", this.Page);
            }

        }

    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //==========0217
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            DataTable dt = clsData.getEmployees("1", e.Row.Cells[5].Text);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Name_CH"].ToString() != "")
                {
                    e.Row.Cells[5].Text = dt.Rows[0]["Name_CH"].ToString();
                    e.Row.Cells[6].Text = dt.Rows[0]["Extension"].ToString();
                    
                }
            }
            else
            {
                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";
            }

            if (e.Row.Cells[8].Text == "可借用")
                e.Row.Cells[8].Text = "";

        }



    }

    protected void butOK_Click(object sender, EventArgs e)
    {

        string strGName = txtGName.Text;

        if ((strGName.IndexOf("ROHS") == -1) && (strGName.IndexOf("RoHs") == -1) && (strGName.IndexOf("-D") == -1) && (strGName.IndexOf("- D") == -1))
        {
            DateTime dtS = Convert.ToDateTime("1911/01/01");
            DateTime dtE = Convert.ToDateTime("1911/01/01");
            string strStartDate, strEndDate, strToday, strToday1, strUseKind, strPeriod;

            DataTable dtMaster = clsData.UploadApparatusMasterQuery("A1", "0");
            string strMaster = dtMaster.Rows[0]["Name"].ToString();
            for (int intMaster = 1; intMaster < dtMaster.Rows.Count; intMaster++)
            {
                strMaster = strMaster + "," + dtMaster.Rows[intMaster]["Name"].ToString();
            }
            dtMaster = clsData.UploadApparatusMasterQuery("A1T", "0");
            strMaster = strMaster + "," + dtMaster.Rows[0]["Name"].ToString();
            dtMaster = clsData.UploadApparatusMasterQuery("A1W", "0");
            strMaster = strMaster + "," + dtMaster.Rows[0]["Name"].ToString();

            strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            strToday1 = DateTime.Now.ToString("yyyy/MM/dd");

            //if (rdoTime.Checked == true)
            strPeriod = "D";
            //else
            //    strPeriod = "N";

            if (rdoUse.Checked == true)
                strUseKind = "M";
            else
                strUseKind = "A";

            strStartDate = Request["date1"].ToString();
            if (strStartDate != "")
            {
                dtS = Convert.ToDateTime(strStartDate);
                strStartDate = dtS.ToString("yyyy/MM/dd");
                //strStartDate = strStartDate +" "+ ddlHourB.Text + ":" + ddlMinB.Text + ":00";
            }

            strEndDate = Request["date2"].ToString();
            if (strEndDate != "")
            {
                dtE = Convert.ToDateTime(strEndDate);
                strEndDate = dtE.ToString("yyyy/MM/dd 23:59:59");
                //strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text + ":00";
            }

            if (dtE < dtS)
            {
                clsMsg.AlertMessage("歸還日期不得小於借用日期！", this.Page);
            }
            else
            {
                if ((strMaster.IndexOf(Session["EmpName"].ToString()) != -1) && (Session["EmpName"].ToString() != ""))
                {
                    if ((lblAID.Text != "") || (lblAID.Text != null))
                    {
                        if ((txtName.Text.Trim() != "") && (txtExt.Text.Trim() != "") && (txtEmail.Text.Trim() != "") && (strStartDate != "") && (strEndDate != "") && (ddlDepartment.Text != "") && (ddlCustomer.Text != ""))
                        {
                            int intKind = 0;
                            if (ddlKind.Text == "外線網路 - 中華電信")
                                intKind = 0;
                            else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                                intKind = 0;
                            else if (lblName.Text == "HUAWEI MA5818")
                                intKind = 0;
                            else
                            {

                                DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, lblAID.Text, strPeriod);
                                intKind = dt1.Rows.Count;
                            }

                            //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, Session["ApparatusID"].ToString());

                            if (intKind == 0)
                            {
                                if (clsTransaction.InsertApparatusReservation(lblAID.Text, strStartDate, strEndDate, txtName.Text, ddlDepartment.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", lblCustodian.Text, "", "", "", txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text, ddlCustomer.Text, lblPrice.Text, "", strPeriod, strUseKind, "", "", "") == true)
                                {
                                    //if (clsTransaction.UpDateApparatusStatus("借用中", lblAID.Text) == true)  //0217
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
                                clsMsg.AlertMessage("此時段已被預約，請選擇其他時段！", this.Page);

                        }
                        else
                            clsMsg.AlertMessage("*為必填欄位....", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("請選擇設備！", this.Page);
                }
                else
                {

                    if ((lblAID.Text != "") && (lblAID.Text != null))
                    {
                        if ((txtName.Text.Trim() != "") && (txtExt.Text.Trim() != "") && (txtEmail.Text.Trim() != "") && (strStartDate != "") && (strEndDate != "") && (ddlDepartment.Text != "") && (ddlCustomer.Text != ""))
                        {
                            DataTable dt3 = clsData.UploadApparatusStatus(lblAID.Text, strToday1);
                            string strStatus1;
                            if (dt3.Rows.Count == 0)
                                strStatus1 = "";
                            else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                                strStatus1 = "";
                            else if (lblName.Text == "HUAWEI MA5818")
                                strStatus1 = "";
                            else
                            {
                                if (ddlKind.Text == "外線網路 - 中華電信")
                                    strStatus1 = "";
                                else
                                    strStatus1 = dt3.Rows[0]["Status"].ToString();
                            }
                            //if (strStatus1 != "Y")
                            //{

                            //DataTable dt2 = clsData.UploadReservationRepeat(lblAID.Text, strToday, ddlDepartment.Text, strPeriod);

                            //if ((dt2.Rows.Count == 0) || (Session["AppNo"].ToString() == "DA40"))//暫時先開權限給DA40
                            //{
                            if (checkDate1(strStartDate, strEndDate) == true)
                            {

                                int intKind = 0;
                                if (ddlKind.Text == "外線網路 - 中華電信")
                                    intKind = 0;
                                else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                                    intKind = 0;
                                else if (lblName.Text == "HUAWEI MA5818")
                                    intKind = 0;
                                else
                                {
                                    DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, lblAID.Text, strPeriod);
                                    intKind = dt1.Rows.Count;
                                }

                                if (intKind == 0)
                                {
                                    if (clsTransaction.InsertApparatusReservation(lblAID.Text, strStartDate, strEndDate, txtName.Text, ddlDepartment.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", lblCustodian.Text, "", "", "", txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text, ddlCustomer.Text, lblPrice.Text, "", strPeriod, strUseKind, "", "", "") == true)
                                    {
                                        //if (clsTransaction.UpDateApparatusStatus("借用中", lblAID.Text) == true)    //0217
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
                                    clsMsg.AlertMessage("此時段已被預約，請選擇其他時段！", this.Page);
                            }
                            else
                                clsMsg.AlertMessage("預約天數上限為7天！", this.Page);
                            //}
                            //else
                            //    clsMsg.AlertMessage("此設備貴部門尚在使用中，請使用結束後再進行預約！", this.Page);
                            //}
                            //else
                            //    clsMsg.AlertMessage("此設備尚未歸還！", this.Page);                       
                        }
                        else
                            clsMsg.AlertMessage("*為必填欄位....", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("請選擇設備！", this.Page);
                }
            }
        }
        else
            clsMsg.AlertMessage("機種名稱後面請勿加[客戶代碼]及[ROHS]！！", this.Page);

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


        StreamReader myMailBody, myMailBody1;
        DataTable dt1;
        string strMail;
        //mail標題
        string MailSubject = "設備預約通知";

        for (int intI = 0; intI < 4; intI++)
        {

            if (intI == 0)
            {
                //MAIL內容
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body.txt");
                dt1 = clsData.UploadApparatusMasterQuery("A1", "1");
                strMail = dt1.Rows[0]["Email"].ToString();
                //string strMailBody = myMailBody.ReadToEnd();
            }
            else if (intI == 1)
            {
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body3.txt", System.Text.Encoding.Default);

                strMail = txtAgentEmail.Text.Trim();
                //string strMailBody = myMailBody1.ReadToEnd();

            }
            else
            {
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body.txt");

                if (intI == 2)
                    strMail = lblCMail.Text;
                else
                    if (lblAgent.Text != "")
                    {
                        dt1 = clsData.UploadEmployeeMail(lblAgent.Text);
                        strMail = dt1.Rows[0]["Email"].ToString();
                    }
                    else
                        strMail = lblAgent.Text;
            }

            string strMailBody = myMailBody.ReadToEnd();

            #region 找資料塞到SendMail內




            //string strMail = txtEmail.Text;
            string strName = ddlDepartment.Text + "-" + txtName.Text + "(" + txtExt.Text + ")";
            string strStartDate, strEndDate;
            string strApparatus;

            strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

            strStartDate = Request["date1"].ToString();
            if (strStartDate != "")
            {
                dt = Convert.ToDateTime(strStartDate);
                strStartDate = dt.ToString("yyyy/MM/dd");
                //strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text;
            }

            strEndDate = Request["date2"].ToString();
            if (strEndDate != "")
            {
                dt = Convert.ToDateTime(strEndDate);
                strEndDate = dt.ToString("yyyy/MM/dd");
                //strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text;
            }

            string strBody = string.Format(strMailBody, strName, strApparatus, strStartDate, strEndDate, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

            clsTransaction.SendMail(strMail, MailSubject, strBody);

            myMailBody.Close();
            myMailBody.Dispose();
        }

            #endregion
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

    private bool checkDate1(string strStart, string strEnd)
    {
        DateTime startDate = Convert.ToDateTime(strStart);
        DateTime endDate = Convert.ToDateTime(strEnd);
        DateTime startDate1 = startDate;
        int intHoliday = 0;

        TimeSpan Total = endDate.Subtract(startDate);

        if (Total.TotalDays >= 7)
            return false;
        else
            return true;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //=========0217
        //DataTable dt = clsData.UploadApparatusStatus(txtSearch.Text, "0", ddlKind.Text);
        //=========0217

        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);

        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    private void setEmpty()
    {
        txtSearch.Text = "";
        this.gvwMain.DataSource = null;
        this.DataBind();
        lblName.Text = "";
        lblProductID.Text = "";
        lblBrand.Text = "";
        lblModel.Text = "";
        lblCustodian.Text = "";
        lblCustodianExt.Text = "";
        lblAgent.Text = "";
        lblAgentExt.Text = "";
        //txtName.Text = "";
        //txtExt.Text = "";
        //txtEmail.Text = "";
        txtNote.Text = "";
        strStart = "";
        strStart1 = "";
        txtMission.Text = "";
        txtGName.Text = "";

    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/ReservationMain.aspx");
    }
}
