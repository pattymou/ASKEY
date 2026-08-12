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
using System.Diagnostics;

public partial class WebForm_SampleReservationAssign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            //strID = Request.QueryString["ID"];
            //strID = "10";
            rdoAccpt.Checked = true;
            //loadCustomer(this.ddlCustomer);
            //loadDepartment(this.ddlDepartment);
            lblDateR2.Visible = false;
            lblDateR3.Visible = false;
            lblAID.Visible = false;
            getSample();
        }
    }

    protected void butOK_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        DateTime dt;
        string strStatus;

        if (rdoAccpt.Checked == true)
            strStatus = "Y";
        else
            strStatus = "N";

        if (lblDateR2.Text == "")
        {

            if (clsTransaction.UpDateReservation(strStatus, Request.QueryString["ID"].ToString(), "", "1", "", "Other") == true)
            {
                if (strStatus == "N")
                    clsTransaction.UpDateSampleStatus("閒置中", lblAID.Text);
                string strMaxID;
                string strStartDate, strEndDate;

                strStartDate = lblDateB.Text;
                if (strStartDate != "")
                {
                    dt = Convert.ToDateTime(strStartDate);
                    strStartDate = dt.ToString();
                    //strStartDate = strStartDate;
                }

                strEndDate = lblDateR.Text;
                if (strEndDate != "")
                {
                    dt = Convert.ToDateTime(strEndDate);
                    strEndDate = dt.ToString();
                    //strEndDate = strEndDate;
                }

                dt1 = clsData.UploadMaxReservation();
                strMaxID = dt1.Rows[0]["ID"].ToString();
                DateTime startDate = Convert.ToDateTime(strStartDate);
                DateTime endDate = Convert.ToDateTime(strEndDate);
                while (startDate < endDate)
                {
                    string strDateW;

                    strDateW = startDate.ToString("yyyy/MM/dd") + "(" + startDate.DayOfWeek.ToString() + ")";

                    clsTransaction.InsertReservationDate(strMaxID);

                    startDate = startDate.AddDays(1);

                }

                //if (strStatus == "Y")
                MailData(strStatus);


                Response.Redirect("~/WebForm/SampleReservationList.aspx");
            }
            else
                clsMsg.AlertMessage("更新失敗！", this.Page);
        }
        else
        {
            bool bStatus;

            DateTime dt5 = Convert.ToDateTime(lblDateR.Text);
            string strEndDate = dt5.ToString("yyyy/MM/dd hh:mm:ss");

            if (strStatus == "Y")
                bStatus = clsTransaction.UpDateReservationContinuous(Request.QueryString["ID"].ToString(), strEndDate, "0");
            else
                bStatus = clsTransaction.UpDateReservationContinuous(Request.QueryString["ID"].ToString(), strEndDate, "1");

            if (bStatus == true)
            {
                MailData1(strStatus);
                Response.Redirect("~/WebForm/SampleReservationList.aspx");
            }
            else
                clsMsg.AlertMessage("更新失敗！", this.Page);

        }

    }

    #region MailData
    private void MailData(string strStatus1)
    {
        #region 宣告變數

        //DateTime dt; 

        #endregion

        #region mail config

        //mail標題
        string MailSubject = "樣品預約通知";



        #endregion

        #region 找資料塞到SendMail內


        //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "1");
        //string strMail = dt1.Rows[0]["Email"].ToString();

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body1.txt");
                string strMailBody = myMailBody.ReadToEnd();
                string strMail;
                DataTable dt2, dt3;
                DataTable dt = clsData.UploadSampleReservationQuery(Request.QueryString["ID"].ToString(), "1");

                for (int intJ = 0; intJ < 6; intJ++)
                {
                    if (intJ == 0)
                        strMail = lblEmail.Text;
                    else if (intJ == 1)
                    {
                        //dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
                        dt2 = clsData.UploadSampleQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1");
                        dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
                        if (dt2.Rows.Count > 0)
                            strMail = dt2.Rows[0]["Email"].ToString();
                        else
                            strMail = "";
                    }
                    else if (intJ == 2)
                    {
                        dt2 = clsData.UploadSampleQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1");
                        dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
                        if (dt2.Rows.Count > 0)
                        {
                            dt2 = clsData.UploadLeader("2", "", dt2.Rows[0]["Team"].ToString());
                            if (dt2.Rows.Count > 0)
                                strMail = dt2.Rows[0]["Email"].ToString();
                            else
                                strMail = "";
                        }
                        else
                            strMail = "";
                    }
                    else if (intJ == 3)
                    {
                        dt2 = clsData.UploadSampleQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1");
                        if (dt2.Rows.Count > 0)
                        {
                            dt2 = clsData.getEmployees("1", dt2.Rows[0]["Agent"].ToString());
                            if (dt2.Rows.Count > 0)
                                strMail = dt2.Rows[0]["Email"].ToString();
                            else
                                strMail = "";
                        }
                        else
                            strMail = "";
                    }
                    else if (intJ == 4)
                    {
                        dt2 = clsData.UploadLeader("1", "", "");
                        if (dt2.Rows.Count > 0)
                            strMail = dt2.Rows[0]["Email"].ToString();
                        else
                            strMail = "";
                    }
                    else
                    {
                        strMail = lblAgentEmail.Text;
                    }

                    if (strMail != "")
                    {
                        string strName = lblBorrower.Text;
                        //string strStartDate, strEndDate;
                        string strApparatus, strBodyM, strYN, strBodyM1;

                        strApparatus = lblModelName.Text;

                        strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

                        if (strStatus1 == "Y")
                        {
                            strBodyM = "請您與樣品保管人借用樣品";
                            strYN = "通過";
                        }
                        else
                        {
                            strBodyM = "請洽樣品保管人";
                            strYN = "失敗";
                        }

                        string strBody = string.Format(strMailBody, strName, strBodyM1, strYN, strBodyM, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                        clsTransaction.SendMail(strMail, MailSubject, strBody);
                    }
                }
                myMailBody.Close();
                myMailBody.Dispose();

            }
            else
            {
                //MAIL內容
                StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body2.txt");
                string strMailBody = myMailBody.ReadToEnd();

                //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1P", "0");

                //string strMail = dt1.Rows[0]["Name"].ToString();
                string strMail = "";
                string strName = lblBorrower.Text;

                //string strStartDate, strEndDate;
                string strApparatus, strBodyM, strYN, strBodyM1;

                strApparatus = lblModelName.Text;

                strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

                if (strStatus1 == "Y")
                {
                    //strBodyM = "請您與設備保管人借用設備";
                    strYN = "通過";
                }
                else
                {
                    //strBodyM = "請洽設備保管人";
                    strYN = "失敗";
                }

                string strBody = string.Format(strMailBody, strName, strBodyM1, strYN, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                clsTransaction.SendMail(strMail, MailSubject, strBody);

                myMailBody.Close();
                myMailBody.Dispose();
            }
        }



        #endregion
    }
    #endregion

    #region MailData1
    private void MailData1(string strStatus1)
    {
        //mail標題
        string MailSubject = "樣品續約通知";


        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                //MAIL內容
                StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body1.txt");
                string strMailBody = myMailBody.ReadToEnd();
                string strMail;
                DataTable dt2, dt3;
                DataTable dt = clsData.UploadReservationQuery(Request.QueryString["ID"].ToString(), "1");
                for (int intJ = 0; intJ < 5; intJ++)
                {
                    if (intJ == 0)
                        strMail = lblEmail.Text;
                    else if (intJ == 1)
                    {
                        //dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
                        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
                        dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
                        if (dt2.Rows.Count > 0)
                            strMail = dt2.Rows[0]["Email"].ToString();
                        else
                            strMail = "";
                    }
                    else if (intJ == 2)
                    {
                        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
                        dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
                        if (dt2.Rows.Count > 0)
                        {
                            dt2 = clsData.UploadLeader("2", "", dt2.Rows[0]["Team"].ToString());
                            if (dt2.Rows.Count > 0)
                                strMail = dt2.Rows[0]["Email"].ToString();
                            else
                                strMail = "";
                        }
                        else
                            strMail = "";
                    }
                    else if (intJ == 3)
                    {
                        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
                        if (dt2.Rows.Count > 0)
                        {
                            dt2 = clsData.getEmployees("1", dt2.Rows[0]["Agent"].ToString());
                            if (dt2.Rows.Count > 0)
                                strMail = dt2.Rows[0]["Email"].ToString();
                            else
                                strMail = "";
                        }
                        else
                            strMail = "";
                    }
                    else
                    {
                        dt2 = clsData.UploadLeader("1", "", "");
                        if (dt2.Rows.Count > 0)
                            strMail = dt2.Rows[0]["Email"].ToString();
                        else
                            strMail = "";
                    }

                    if (strMail != "")
                    {
                        string strName = lblBorrower.Text;
                        //string strStartDate, strEndDate;
                        string strApparatus, strBodyM, strYN, strBodyM1;

                        strApparatus = lblModelName.Text;

                        strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

                        if (strStatus1 == "Y")
                        {
                            strBodyM = "請於" + lblDateR.Text + "歸還";
                            strYN = "通過";
                        }
                        else
                        {
                            strBodyM = "請洽樣品保管人";
                            strYN = "失敗";
                        }

                        string strBody = string.Format(strMailBody, strName, strBodyM1, strYN, strBodyM, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                        clsTransaction.SendMail(strMail, MailSubject, strBody);
                    }
                }
                myMailBody.Close();
                myMailBody.Dispose();
            }
            else
            {
                //MAIL內容
                StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_Continuous1.txt", System.Text.Encoding.Default);
                string strMailBody = myMailBody.ReadToEnd();


                #region 找資料塞到SendMail內


                string strMail = lblEmail.Text;
                string strName = lblBorrower.Text;
                string strApparatus, strBodyM, strYN, strBodyM1;

                strApparatus = lblModelName.Text;



                if (strStatus1 == "Y")
                {
                    strBodyM = "請於" + lblDateR.Text + "歸還";
                    strYN = "通過";
                }
                else
                {
                    strBodyM = "請洽樣品保管人";
                    strYN = "失敗";
                }

                string strBody = string.Format(strMailBody, strName, strApparatus, strYN, strBodyM, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                clsTransaction.SendMail(strMail, MailSubject, strBody);

                myMailBody.Close();
                myMailBody.Dispose();
            }
        }

                #endregion
    }
    #endregion


    private void getSample()
    {
        DataTable dt = clsData.UploadSampleReservationQuery(Request.QueryString["ID"].ToString(), "1");
        lblNumber.Text = dt.Rows[0]["Number"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();
        lblFunction.Text = dt.Rows[0]["Function_Name"].ToString();
        lblItem.Text = dt.Rows[0]["Item"].ToString();
        lblCategory.Text = dt.Rows[0]["Category"].ToString();
        lblVendor.Text = dt.Rows[0]["Vendor"].ToString();
        lblModelName.Text = dt.Rows[0]["ModelName"].ToString();
        lblMAC.Text = dt.Rows[0]["MAC"].ToString();
        lblPHY.Text = dt.Rows[0]["PHY"].ToString();
        lblFirmware.Text = dt.Rows[0]["Firmware"].ToString();
        //lblCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        lblNote.Text = dt.Rows[0]["Note"].ToString();
        

        //lblCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        lblBorrower.Text = dt.Rows[0]["Borrower"].ToString();
        lblDepartment.Text = dt.Rows[0]["Department"].ToString();
        lblExt.Text = dt.Rows[0]["Ext"].ToString();
        lblEmail.Text = dt.Rows[0]["Email"].ToString();
        lblDateB.Text = dt.Rows[0]["StartDate"].ToString();
        lblDateR.Text = dt.Rows[0]["EndDate"].ToString();
        //txtNote.Text = dt.Rows[0]["Note"].ToString();
        lblMission.Text = dt.Rows[0]["Mission"].ToString();
        lblGName.Text = dt.Rows[0]["GName"].ToString();
        lblAID.Text = dt.Rows[0]["Apparatus_ID"].ToString();

        lblAgentName.Text = dt.Rows[0]["Agent"].ToString();
        lblAgentExt.Text = dt.Rows[0]["AgentExt"].ToString();
        lblAgentEmail.Text = dt.Rows[0]["AgentEmail"].ToString();

        string strDate = "";
        if (dt.Rows[0]["ContinuousDate"].ToString() != "")
        {
            DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["ContinuousDate"].ToString());
            strDate = dt1.ToString("yyyy/MM/dd");
        }


        if ((dt.Rows[0]["ContinuousDate"].ToString() != "") && (strDate != "1900/01/01"))
        {
            lblDateR2.Visible = true;
            lblDateR3.Visible = true;
            lblDateR.Text = dt.Rows[0]["ContinuousDate"].ToString();
            lblDateR2.Text = dt.Rows[0]["EndDate"].ToString();
            lblDateR1.Text = "預計歸還日期";
        }

    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/SampleReservationList.aspx");
    }
}
