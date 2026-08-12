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

public partial class WebForm_SampleReservation : System.Web.UI.Page
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
            //loadKind(this.ddlKind);
            loadDepartment(this.ddlDepartment);
            //ddlHourB.Text = "09";
            //ddlHourR.Text = "18";
            //ddlMinR.Text = "30";
            DataTable dt1 = clsData.UploadApparatusMasterQuery("A4", "0");

            string strNumber;
            dt1 = clsData.UploadWorkTimeQuery("A2S");
            strNumber = dt1.Rows[0]["Name"].ToString();
            string[] strNumber1 = strNumber.Split(':');
            ddlHourB.Text = strNumber1[0];
            ddlMinB.Text = strNumber1[1];

            dt1 = clsData.UploadWorkTimeQuery("A2E");
            strNumber = dt1.Rows[0]["Name"].ToString();
            strNumber1 = strNumber.Split(':');
            ddlHourR.Text = strNumber1[0];
            ddlMinR.Text = strNumber1[1];

            ddlHourR.Enabled = false;
            ddlMinR.Enabled = false;
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

        string strDepartment;

        if ((Session["EmpNo"].ToString() != "") && (Session["EmpNo"].ToString() != null))
        {
            strDepartment = "";
        }
        else
        {
            strDepartment = Session["AppNo"].ToString();

        }


        DataTable dt = clsData.UploadSampleQuery(txtSearch.Text, "0");

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

            lblNumber.Text = "";
            lblKind.Text = "";
            lblFunction.Text = "";
            lblItem.Text = "";
            lblCategory.Text = "";
            lblVendor.Text = "";
            lblModelName.Text = "";
            lblMAC.Text = "";
            lblPHY.Text = "";
            lblFirmware.Text = "";
            
            //lblStartDate.Text = "";

            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;

            strRStatus = ((Label)row.Cells[6].FindControl("lblStatus")).Text.Trim();

            DataTable dt1 = clsData.UploadApparatusMasterQuery("A1D", "0");
            strApparatusD = dt1.Rows[0]["Name"].ToString();

            if (strRStatus == "閒置中")   //0217
            {
                Session["SampleID"] = ((Label)row.Cells[6].FindControl("lblGVSeq")).Text.Trim();
                DataTable dt = clsData.UploadSampleQuery(Session["SampleID"].ToString(), "1");
                lblNumber.Text = dt.Rows[0]["Number"].ToString().Trim();
                lblKind.Text = dt.Rows[0]["Kind"].ToString().Trim();
                lblFunction.Text = dt.Rows[0]["Function_Name"].ToString().Trim();
                lblItem.Text = dt.Rows[0]["Item"].ToString().Trim();
                lblCategory.Text = dt.Rows[0]["Category"].ToString().Trim();
                lblVendor.Text = dt.Rows[0]["Vendor"].ToString().Trim();
                lblModelName.Text = dt.Rows[0]["ModelName"].ToString().Trim();
                lblMAC.Text = dt.Rows[0]["MAC"].ToString().Trim();
                lblPHY.Text = dt.Rows[0]["PHY"].ToString().Trim();
                lblFirmware.Text = dt.Rows[0]["Firmware"].ToString().Trim();

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

            }
            else
            {
                if (strApparatusD == ddlDepartment.Text)
                {
                    Session["SampleID"] = ((Label)row.Cells[7].FindControl("lblGVSeq")).Text.Trim();
                    DataTable dt = clsData.UploadSampleQuery(Session["SampleID"].ToString(), "1");
                    lblNumber.Text = dt.Rows[0]["Number"].ToString().Trim();
                    lblKind.Text = dt.Rows[0]["Kind"].ToString().Trim();
                    lblFunction.Text = dt.Rows[0]["Function_Name"].ToString().Trim();
                    lblItem.Text = dt.Rows[0]["Item"].ToString().Trim();
                    lblCategory.Text = dt.Rows[0]["Category"].ToString().Trim();
                    lblVendor.Text = dt.Rows[0]["Vendor"].ToString().Trim();
                    lblModelName.Text = dt.Rows[0]["ModelName"].ToString().Trim();
                    lblMAC.Text = dt.Rows[0]["MAC"].ToString().Trim();
                    lblPHY.Text = dt.Rows[0]["PHY"].ToString().Trim();
                    lblFirmware.Text = dt.Rows[0]["Firmware"].ToString().Trim();

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
                }
                else
                    clsMsg.AlertMessage("此樣品不可外借！", this.Page);
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

        }
    }


    protected void butOK_Click(object sender, EventArgs e)
    {
        //DateTime dt;
        DateTime dtS = Convert.ToDateTime("1911/01/01");
        DateTime dtE = Convert.ToDateTime("1911/01/01");
        string strStartDate, strEndDate, strToday, strToday1;

        DataTable dtMaster = clsData.UploadApparatusMasterQuery("A1", "0");
        string strMaster = dtMaster.Rows[0]["Name"].ToString();

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        strToday1 = DateTime.Now.ToString("yyyy/MM/dd");

        strStartDate = Request["date1"].ToString();
        if (strStartDate != "")
        {
            dtS = Convert.ToDateTime(strStartDate);
            strStartDate = dtS.ToString("yyyy/MM/dd");
            strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text + ":00";
        }

        strEndDate = Request["date2"].ToString();
        if (strEndDate != "")
        {
            dtE = Convert.ToDateTime(strEndDate);
            strEndDate = dtE.ToString("yyyy/MM/dd");
            strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text + ":00";
        }

        //checkDate(strStartDate, strEndDate);
        if (dtE < dtS)
        {
            clsMsg.AlertMessage("歸還日期不得小於借用日期！", this.Page);
        }
        else
        {
            if (Session["EmpName"].ToString() == strMaster)
            {
                if ((Session["SampleID"].ToString() != "") || (Session["SampleID"].ToString() != null))
                {
                    if ((txtName.Text.Trim() != "") && (txtExt.Text.Trim() != "") && (txtEmail.Text.Trim() != "") && (strStartDate != "") && (strEndDate != "") && (ddlDepartment.Text != ""))
                    {

                        DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, Session["SampleID"].ToString(),"");

                        if (dt1.Rows.Count == 0)
                        {
                            if (clsTransaction.InsertApparatusReservation(Session["SampleID"].ToString(), strStartDate, strEndDate, txtName.Text, ddlDepartment.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", lblCustodian.Text, "", "", "", txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text,"","","","","","","","") == true)
                            {
                                if (clsTransaction.UpDateSampleStatus("借用中", Session["SampleID"].ToString()) == true)  //0217
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

                if ((Session["SampleID"].ToString() != "") || (Session["SampleID"].ToString() != null))
                {
                    if ((txtName.Text.Trim() != "") && (txtExt.Text.Trim() != "") && (txtEmail.Text.Trim() != "") && (strStartDate != "") && (strEndDate != "") && (ddlDepartment.Text != ""))
                    {
                        DataTable dt3 = clsData.UploadApparatusStatus(Session["SampleID"].ToString(), strToday1);
                        string strStatus1;
                        if (dt3.Rows.Count == 0)
                            strStatus1 = "";
                        else
                            strStatus1 = dt3.Rows[0]["Status"].ToString();
                        if (strStatus1 != "Y")
                        {

                            DataTable dt2 = clsData.UploadReservationRepeat(Session["SampleID"].ToString(), strToday, ddlDepartment.Text,"");

                            if (dt2.Rows.Count == 0)
                            {
                                if (checkDate(strStartDate, strEndDate) == true)
                                {

                                    DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, Session["SampleID"].ToString(),"");

                                    if (dt1.Rows.Count == 0)
                                    {
                                        if (clsTransaction.InsertApparatusReservation(Session["SampleID"].ToString(), strStartDate, strEndDate, txtName.Text, ddlDepartment.Text, txtExt.Text, txtEmail.Text, txtMission.Text, txtGName.Text, "", lblCustodian.Text, "", "", "", txtAgent.Text, txtAgentExt.Text, txtAgentEmail.Text,"","","","","","","","") == true)
                                        {
                                            if (clsTransaction.UpDateSampleStatus("借用中", Session["SampleID"].ToString()) == true)    //0217
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
                                        clsMsg.AlertMessage("此時段已被預約，請選擇其他時段！", this.Page);
                                }
                                else
                                    clsMsg.AlertMessage("預約天數上限為工作日5天！", this.Page);
                            }
                            else
                                clsMsg.AlertMessage("此設備貴部門尚在使用中，請使用結束後再進行預約！", this.Page);
                        }
                        else
                            clsMsg.AlertMessage("此設備尚未歸還！", this.Page);
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
        lblNumber.Text = "";
        lblKind.Text = "";
        lblFunction.Text = "";
        lblItem.Text = "";
        lblCategory.Text = "";
        lblVendor.Text = "";
        lblModelName.Text = "";
        lblMAC.Text = "";
        lblPHY.Text = "";
        lblFirmware.Text = "";
        txtName.Text = "";
        txtExt.Text = "";
        txtEmail.Text = "";
        txtNote.Text = "";
        strStart = "";
        strStart1 = "";
        txtMission.Text = "";
        txtGName.Text = "";

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
        string MailSubject = "樣品預約通知";

        for (int intI = 0; intI < 2; intI++)
        {
            if (intI == 0)
            {
                //MAIL內容
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body.txt");
                dt1 = clsData.UploadApparatusMasterQuery("A4", "1");
                strMail = dt1.Rows[0]["Email"].ToString();
                
            }
            else
            {
                myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body3.txt", System.Text.Encoding.Default);

                strMail = txtAgentEmail.Text.Trim();
            }

            string strMailBody = myMailBody.ReadToEnd();
            #region 找資料塞到SendMail內




            //string strMail = txtEmail.Text;
            string strName = ddlDepartment.Text + "-" + txtName.Text + "(" + txtExt.Text + ")";
            string strStartDate, strEndDate;
            string strApparatus;

            strApparatus = lblModelName.Text + "(" + lblNumber.Text + ")";

            strStartDate = Request["date1"].ToString();
            if (strStartDate != "")
            {
                dt = Convert.ToDateTime(strStartDate);
                strStartDate = dt.ToString("yyyy/MM/dd");
                strStartDate = strStartDate + " " + ddlHourB.Text + ":" + ddlMinB.Text;
            }
            //strStartDate = strStartDate + strApparatus;

            strEndDate = Request["date2"].ToString();
            if (strEndDate != "")
            {
                dt = Convert.ToDateTime(strEndDate);
                strEndDate = dt.ToString("yyyy/MM/dd");
                strEndDate = strEndDate + " " + ddlHourR.Text + ":" + ddlMinR.Text;
            }

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

    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SampleReservationMain.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = clsData.UploadSampleQuery(txtSearch.Text, "0");

        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
}
