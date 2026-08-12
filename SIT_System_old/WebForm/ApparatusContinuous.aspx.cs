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

public partial class WebForm_ApparatusContinuous : System.Web.UI.Page
{
    public static string strDate;
    public static string strStart;
    public static string strStart1;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strID;
        string strDepartment, strNumber;
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
            //ddlHourR.Text = "18";
            //ddlMinR.Text = "30";
            strNumber = "";
            //lblEndDate.Visible = false;
            //strDepartment = "DA40-SIT";
            //strDepartment = Session["EmpName"].ToString().Trim();


            dt = clsData.UploadNumber(Session["AppNo"].ToString());
            if (dt.Rows.Count > 0)
            {
                lblCName.Text = dt.Rows[0]["Name"].ToString().Trim();
                lblExt.Text = dt.Rows[0]["Ext"].ToString().Trim();
                lblEmail.Text = dt.Rows[0]["Mail"].ToString().Trim();
                lblDepartment.Text = dt.Rows[0]["Department"].ToString().Trim();
            }


            strDate = DateTime.Now.ToString("yyyy/MM/dd");

            if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
            {
                strDepartment = "";
            }
            else
            {
                dt = clsData.UploadNumber(Session["AppNo"].ToString());

                strNumber = dt.Rows[0]["Name"].ToString().Trim();
                strDepartment = dt.Rows[0]["Department"].ToString().Trim();

            }

            dt = clsData.getContinuousApparatusList(strDate, strDepartment, strNumber);
            this.gvwMain.DataSource = dt;
            this.DataBind();


        }

    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;

        string strDepartment;
        string strNumber = "";
        DataTable dt;

        if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
        {
            strDepartment = "";
        }
        else
        {
            dt = clsData.UploadNumber(Session["AppNo"].ToString());

            strNumber = dt.Rows[0]["Name"].ToString().Trim(); ;
            strDepartment = dt.Rows[0]["Department"].ToString().Trim();

        }


        dt = clsData.getContinuousApparatusList(strDate, strDepartment, strNumber);

        this.gvwMain.DataSource = dt;
        this.DataBind();
        //GvQuery();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strRStatus, strID;
        string strApparatusD;

        if (e.CommandName == "AddToCart")
        {

            lblName.Text = "";
            lblProductID.Text = "";
            lblBrand.Text = "";
            lblModel.Text = "";
            lblCustodian.Text = "";
            //lblCustodianD.Text = "";
            lblNote.Text = "";
            lblCName.Text = "";
            lblDepartment.Text = "";
            lblExt.Text = "";
            lblEmail.Text = "";
            lblMission.Text = "";
            lblGName.Text = "";
            lblCustodian.Text = "";
            //lblStartDate.Text = "";

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            //strID = ((Label)row.Cells[6].FindControl("lblRStatus")).Text.Trim();

            //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");
            //strApparatusD = dt1.Rows[0]["Name"].ToString();

            //if (strRStatus == "閒置中")   //0217
            //{
            strID = ((Label)row.Cells[6].FindControl("lblGVSeq")).Text.Trim();
            DataTable dt = clsData.UploadAContinuousQuery(strID);
            lblName.Text = dt.Rows[0]["Name"].ToString().Trim();
            lblProductID.Text = dt.Rows[0]["Products_ID"].ToString().Trim();
            lblBrand.Text = dt.Rows[0]["Brand"].ToString().Trim();
            lblModel.Text = dt.Rows[0]["Model"].ToString().Trim();
            lblKind.Text = dt.Rows[0]["kind"].ToString().Trim();
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
            //lblCustodian.Text = dt.Rows[0]["Custodian"].ToString().Trim();
            //lblCustodianD.Text = dt.Rows[0]["Custodian_Department"].ToString().Trim();
            lblNote.Text = dt.Rows[0]["Note"].ToString().Trim();
            lblCName.Text = dt.Rows[0]["Borrower"].ToString().Trim();
            lblDepartment.Text = dt.Rows[0]["Department"].ToString().Trim();
            lblExt.Text = dt.Rows[0]["Ext"].ToString().Trim();
            lblEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
            lblMission.Text = dt.Rows[0]["Mission"].ToString().Trim();
            lblGName.Text = dt.Rows[0]["GName"].ToString().Trim();
            //lblStartDate.Text = dt.Rows[0]["StartDate"].ToString().Trim();
            lblID.Text = strID;
            lblAID.Text = dt.Rows[0]["ID"].ToString().Trim();

            if ((dt.Rows[0]["ContinuousDate"].ToString().Trim() == "") || (dt.Rows[0]["ContinuousDate"].ToString().Trim().IndexOf("1900") != -1))
                lblEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();
            else
                lblEndDate.Text = dt.Rows[0]["ContinuousDate"].ToString().Trim();
            if (dt.Rows[0]["Customer"].ToString().Trim() != "")
                lblCustomer.Text = dt.Rows[0]["Customer"].ToString().Trim();
            else
                lblCustomer.Text = "";

            //if (dt.Rows[0]["Period"].ToString() == "D")
            //    lblPeriod.Text = "白天";
            //else
            //    lblPeriod.Text = "晚上";

            if (dt.Rows[0]["UseKind"].ToString() == "M")
                lblUseKind.Text = "手動測試";
            else
                lblUseKind.Text = "自動化程式";

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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime dt1 = Convert.ToDateTime(e.Row.Cells[5].Text);
            e.Row.Cells[5].Text = dt1.ToString("yyyy/MM/dd");
            dt1 = Convert.ToDateTime(e.Row.Cells[4].Text);
            e.Row.Cells[4].Text = dt1.ToString("yyyy/MM/dd");

        }
    }


    protected void butOK_Click(object sender, EventArgs e)
    {
        //DateTime dt;
        DateTime dtS = Convert.ToDateTime("1911/01/01");
        DateTime dtE = Convert.ToDateTime("1911/01/01");
        string strStartDate, strEndDate, strToday, strToday1, strAID, strUseKind, strPeriod;

        //DataTable dtMaster = clsData.UploadApparatusMasterQuery("A1", "0");
        //string strMaster = dtMaster.Rows[0]["Name"].ToString();

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


        //if (lblPeriod.Text == "白天")
            strPeriod = "D";
        //else
        //    strPeriod = "N";

        if (lblUseKind.Text == "晚上")
            strUseKind = "M";
        else
            strUseKind = "A";

        strStartDate = lblEndDate.Text;
        if (strStartDate != "")
        {
            dtS = Convert.ToDateTime(strStartDate);
            strStartDate = dtS.ToString("yyyy/MM/dd hh:mm:ss");
            //strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text + ":00";
        }

        strEndDate = Request["date2"].ToString();
        if (strEndDate != "")
        {
            dtE = Convert.ToDateTime(strEndDate);
            strEndDate = dtE.ToString("yyyy/MM/dd 23:59:59");
            //strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text + ":00";
        }

        //checkDate(strStartDate, strEndDate);

        if (dtE < dtS)
        {
            clsMsg.AlertMessage("歸還日期不得小於借用日期！", this.Page);
        }
        else
        {
            strAID = lblAID.Text;

            if ((strMaster.IndexOf(Session["EmpName"].ToString()) != -1) && (Session["EmpName"].ToString() != ""))
            {
                if ((strAID != "") || (strAID != null))
                {
                    if (strEndDate != "")
                    {
                        DataTable dt3 = clsData.UploadApparatusQuery(strAID, "1", "");
                        string strStatus1;
                        if (dt3.Rows.Count == 0)
                            strStatus1 = "";
                        else
                            strStatus1 = dt3.Rows[0]["ReservationStatus"].ToString();
                        if ((strStatus1 != "不可借用") && (strStatus1 != "校驗中") && (strStatus1 != "異常維修中"))
                        {
                            string strKind = "";

                            DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID, strPeriod);

                            if (lblKind.Text == "外線網路 - 中華電信")
                                strKind = "1";
                            else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                                strKind = "1";
                            else if (lblName.Text == "HUAWEI MA5818")
                                strKind = "1";
                            else
                            {
                                //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID);
                                //if ((dt1.Rows[0]["Borrower"].ToString() == lblCName.Text) && (dt1.Rows[0]["Department"].ToString() == lblDepartment.Text) && (dt1.Rows[0]["Ext"].ToString() == lblExt.Text))
                                if (dt1.Rows.Count == 0)
                                    strKind = "1";
                            }
                            //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID);

                            //if ((dt1.Rows[0]["Borrower"].ToString() == lblCName.Text) && (dt1.Rows[0]["Department"].ToString() == lblDepartment.Text) && (dt1.Rows[0]["Ext"].ToString() == lblExt.Text))
                            if (strKind == "1")
                            {
                                if (clsTransaction.UpDateContinuousDate(lblID.Text, strEndDate, "") == true)
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
                                    clsMsg.AlertMessage("預約成功！", this.Page);
                                    setEmpty();
                                    //}
                                    //else
                                    //    clsMsg.AlertMessage("預約失敗！", this.Page);
                                    string strDepartment, strNumber;
                                    strNumber = "";
                                    if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
                                    {
                                        strDepartment = "";
                                    }
                                    else
                                    {
                                        dt1 = clsData.UploadNumber(Session["AppNo"].ToString());

                                        strNumber = dt1.Rows[0]["Name"].ToString().Trim();
                                        strDepartment = dt1.Rows[0]["Department"].ToString().Trim();

                                    }

                                    dt1 = clsData.getContinuousApparatusList(strDate, strDepartment, strNumber);
                                    this.gvwMain.DataSource = dt1;
                                    this.DataBind();

                                }
                                else
                                    clsMsg.AlertMessage("預約失敗！", this.Page);
                            }
                            else
                                clsMsg.AlertMessage("此時段已被預約！", this.Page);
                        }
                        else
                            clsMsg.AlertMessage("此設備不得外借，請洽負責人！", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("*為必填欄位....", this.Page);
                }
                else
                    clsMsg.AlertMessage("請選擇設備！", this.Page);
            }
            else
            {
                if ((strAID != "") || (strAID != null))
                {
                    if (strEndDate != "")
                    {
                        DataTable dt3 = clsData.UploadApparatusQuery(strAID, "1", "");
                        string strStatus1;
                        if (dt3.Rows.Count == 0)
                            strStatus1 = "";
                        else
                            strStatus1 = dt3.Rows[0]["ReservationStatus"].ToString();
                        if ((strStatus1 != "不可借用") && (strStatus1 != "校驗中") && (strStatus1 != "異常維修中"))
                        {

                            //DataTable dt2 = clsData.UploadReservationRepeat(strID, strToday, ddlDepartment.Text);

                            //if (dt2.Rows.Count == 0)
                            //{
                            if (checkDate1(strStartDate, strEndDate) == true)
                            {
                                string strKind = "";

                                DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID, strPeriod);

                                if (lblKind.Text == "外線網路 - 中華電信")
                                    strKind = "1";
                                else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                                    strKind = "1";
                                else if (lblName.Text == "HUAWEI MA5818")
                                    strKind = "1";
                                else
                                {
                                    //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID);
                                    //if ((dt1.Rows[0]["Borrower"].ToString() == lblCName.Text) && (dt1.Rows[0]["Department"].ToString() == lblDepartment.Text) && (dt1.Rows[0]["Ext"].ToString() == lblExt.Text))
                                    if (dt1.Rows.Count == 0)
                                        strKind = "1";
                                }
                                //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID);

                                //if ((dt1.Rows[0]["Borrower"].ToString() == lblCName.Text) && (dt1.Rows[0]["Department"].ToString() == lblDepartment.Text) && (dt1.Rows[0]["Ext"].ToString() == lblExt.Text))
                                if (strKind == "1")
                                {
                                    if (clsTransaction.UpDateContinuousDate(lblID.Text, strEndDate, "") == true)
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
                                        clsMsg.AlertMessage("預約成功！", this.Page);
                                        setEmpty();
                                        //}
                                        //else
                                        //    clsMsg.AlertMessage("預約失敗！", this.Page);
                                        string strDepartment, strNumber;
                                        strNumber = "";
                                        if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
                                        {
                                            strDepartment = "";
                                        }
                                        else
                                        {
                                            dt1 = clsData.UploadNumber(Session["AppNo"].ToString());

                                            strNumber = dt1.Rows[0]["Name"].ToString().Trim();
                                            strDepartment = dt1.Rows[0]["Department"].ToString().Trim();

                                        }

                                        dt1 = clsData.getContinuousApparatusList(strDate, strDepartment, strNumber);
                                        this.gvwMain.DataSource = dt1;
                                        this.DataBind();

                                    }
                                    else
                                        clsMsg.AlertMessage("預約失敗！", this.Page);
                                }
                                else
                                    clsMsg.AlertMessage("此時段已被預約！", this.Page);
                            }
                            else
                                clsMsg.AlertMessage("預約天數上限為7天！", this.Page);
                            //}
                            //else
                            //    clsMsg.AlertMessage("此設備貴部門尚在使用中，請使用結束後再進行預約！", this.Page);
                        }
                        else
                            clsMsg.AlertMessage("此設備不得外借，請洽負責人！", this.Page);
                    }
                    else
                        clsMsg.AlertMessage("*為必填欄位....", this.Page);
                }
                else
                    clsMsg.AlertMessage("請選擇設備！", this.Page);
            }

        }
    }
    private void setEmpty()
    {
        //txtSearch.Text = "";
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
        lblCustomer.Text = "";

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
        string MailSubject = "續借設備通知";

        //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Continuous.txt");
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
        string strMail = "";
        for (int intI = 0; intI < 3; intI++)
        {
            if (intI == 0)
            {
                DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "1");
                strMail = dt1.Rows[0]["Email"].ToString();
            }
            else
            {
                if (intI == 1)
                    strMail = lblCMail.Text;
                else
                    strMail = lblAgent.Text;
            }


            //string strMail = txtEmail.Text;
            string strName = lblDepartment.Text + "-" + lblCName.Text + "(" + lblExt.Text + ")";
            string strStartDate, strEndDate;
            string strApparatus;

            strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

            //strStartDate = Request["date1"].ToString();
            //if (strStartDate != "")
            //{
            //    dt = Convert.ToDateTime(strStartDate);
            //    strStartDate = dt.ToString("yyyy/MM/dd");
            //    strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text;
            //}
            //strStartDate = strStartDate + strApparatus;

            strEndDate = Request["date2"].ToString();
            if (strEndDate != "")
            {
                dt = Convert.ToDateTime(strEndDate);
                strEndDate = dt.ToString("yyyy/MM/dd");
                //strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text;
            }

            string strBody = string.Format(strMailBody, strName, strApparatus, strEndDate, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

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

        if (Total.TotalDays > 8)
            return false;
        else
            return true;

    }

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/ReservationMain.aspx");
    }
}
