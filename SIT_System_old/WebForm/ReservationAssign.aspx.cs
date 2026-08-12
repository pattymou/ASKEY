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

public partial class WebForm_ReservationAssign : System.Web.UI.Page
{
    //public static string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
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
            getReservation();

        }
    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        DateTime dt;
        string strStatus,strPeriod;
        string strToday;
        string strStartDate, strEndDate, strEndDate1;

        strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        strStartDate = lblDateB.Text;
        //strStartDate = DateTime.Now.ToString("yyyy/MM/dd");

        if (rdoAccpt.Checked == true)
            strStatus = "Y";
        else
            strStatus = "N";

        //if (lblPeriod.Text == "白天")
            strPeriod = "D";
        //else
        //    strPeriod = "N";
        
        //strEndDate = add_Date(strStartDate,7);
        strEndDate = lblDateR.Text;
        //strEndDate1 = strEndDate + strEndDate1.Substring(10);

        if (strStartDate != "")
        {
            dt = Convert.ToDateTime(strStartDate);
            strStartDate = dt.ToString("yyyy/MM/dd HH:mm:ss");
            //strStartDate = strStartDate;
        }

        //strEndDate = lblDateR.Text;
        if (strEndDate != "")
        {
            dt = Convert.ToDateTime(strEndDate);
            strEndDate = dt.ToString("yyyy/MM/dd HH:mm:ss");
            //strEndDate = strEndDate;
        }

        string strEmp = Request.QueryString["Kind"].ToString();

        int intKind = 0;
        if (lblKind.Text == "外線網路 - 中華電信")
            intKind = 0;
        else
        {
            if (strStatus == "N")
            {
                intKind = 0;
            }
            else
            {
                if (lblDateR2.Text == "")
                {
                    DataTable dt5 = clsData.UploadReservationDateAssign(strStartDate, strEndDate, lblAID.Text, strPeriod);
                    intKind = dt5.Rows.Count;
                    if (lblKind.Text == "外線網路 - 中華電信")
                        intKind = 0;
                    else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                        intKind = 0;
                    else if (lblName.Text == "HUAWEI MA5818")
                        intKind = 0;
                }
                else
                {
                    intKind = 1;
                    DataTable dt5 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, lblAID.Text, strPeriod);
                    if (lblKind.Text == "外線網路 - 中華電信")
                        intKind = 0;
                    else if ((lblProductID.Text == "620057") || (lblProductID.Text == "627787") || (lblProductID.Text == "103675") || (lblProductID.Text == "339215") || (lblProductID.Text == "373967") || (lblProductID.Text == "619988") || (lblProductID.Text == "129302") || (lblProductID.Text == "627788") || (lblProductID.Text == "627789") || (lblProductID.Text == "601085") || (lblProductID.Text == "363865") || (lblProductID.Text == "550328") || (lblProductID.Text == "627910") || (lblProductID.Text == "627911") || (lblProductID.Text == "134700") || (lblProductID.Text == "292Y190197"))
                        intKind = 0;
                    else if (lblName.Text == "HUAWEI MA5818")
                        intKind = 0;
                    else
                    {
                        //DataTable dt1 = clsData.UploadReservationDateQuery(strStartDate, strEndDate, strAID);
                        if (dt5.Rows.Count != 0)
                        {
                            if ((dt5.Rows[0]["Borrower"].ToString() == lblBorrower.Text) && (dt5.Rows[0]["Department"].ToString() == lblDepartment.Text) && (dt5.Rows[0]["Ext"].ToString() == lblExt.Text))
                                intKind = 0;
                        }
                        else
                            intKind = 0;
                    }
                }
            }
        }

        if (intKind == 0)
        {
            if (lblDateR2.Text == "")
            {
                //if (clsTransaction.UpDateReservation(strStatus, Request.QueryString["ID"].ToString(), strEndDate, "3", strStartDate) == true)
                if (clsTransaction.UpDateReservation1(strStatus, Request.QueryString["ID"].ToString(), strEndDate, strEmp, strStartDate) == true)
                {
                    if (strStatus == "N")
                    {
                        dt1 = clsData.UploadReservationUsing(Request.QueryString["ID"].ToString(), strToday);
                        DataTable dt2;
                        dt2 = clsData.UploadReservationUsing1(Request.QueryString["ID"].ToString(), strToday);
                        //if ((dt1.Rows.Count == 0) && (dt2.Rows.Count == 0))
                        //{
                        //    clsTransaction.UpDateApparatusStatus("閒置中", lblAID.Text);
                        //}
                    }
                    string strMaxID;


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
                    if ((strEmp == "0") || (strEmp == "1"))
                        if (strStatus == "Y")
                            MailData_Custodian(strStatus);
                        else
                            MailData(strStatus);
                    else
                        MailData(strStatus);


                    Response.Redirect("~/WebForm/ReservationList.aspx");
                }
                else
                    clsMsg.AlertMessage("更新失敗！", this.Page);
            }
            else
            {
                bool bStatus;

                DateTime dt5 = Convert.ToDateTime(lblDateR.Text);
                strEndDate = dt5.ToString("yyyy/MM/dd HH:mm:ss");

                if (strStatus == "Y")
                    bStatus = clsTransaction.UpDateReservationContinuous1(Request.QueryString["ID"].ToString(), strEndDate, "0",strEmp);
                else
                    bStatus = clsTransaction.UpDateReservationContinuous1(Request.QueryString["ID"].ToString(), strEndDate, "1",strEmp);

                if (bStatus == true)
                {
                    if ((strEmp == "0") || (strEmp == "1"))
                        if (strStatus == "Y")
                            MailData1_Custodian(strStatus);
                        else
                            MailData1(strStatus);
                    else
                        MailData1(strStatus);
                    //MailData1(strStatus);
                    Response.Redirect("~/WebForm/ReservationList.aspx");
                }
                else
                    clsMsg.AlertMessage("更新失敗！", this.Page);

            }
        }
        else
            clsMsg.AlertMessage("此時段已被預約！", this.Page);

    }

    private string add_Date(string DT,int intDate)
    {
        DateTime BF = new DateTime();
        BF = (DateTime.Parse(DT)).AddDays(intDate);
        string NEW_DATE = BF.ToString("yyyy/MM/dd");
        return NEW_DATE;
    }

    #region MailData
    private void MailData_Custodian(string strStatus1)
    {
        #region 宣告變數

        //DateTime dt; 

        #endregion

        #region mail config

        //mail標題
        string MailSubject = "設備預約通知";



        #endregion

        #region 找資料塞到SendMail內


        //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "1");
        //string strMail = dt1.Rows[0]["Email"].ToString();

        //for (int i = 0; i < 2; i++)
        //{
        //    if (i == 0)
        //    {
                //MAIL內容
                //StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body1.txt");
                //string strMailBody = myMailBody.ReadToEnd();
                //string strMail;
                //DataTable dt2, dt3;
                //DataTable dt = clsData.UploadReservationQuery(Request.QueryString["ID"].ToString(), "1");
                //for (int intJ = 0; intJ < 6; intJ++)
                //{
                //    if (intJ == 0)  //借用人
                //        strMail = lblEmail.Text;
                //    else if (intJ == 1)  //保管人
                //    {
                //        //dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
                //        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
                //        dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
                //        if (dt2.Rows.Count > 0)
                //            strMail = dt2.Rows[0]["Email"].ToString();
                //        else
                //            strMail = "";
                //    }
                //    else if (intJ == 2)  //TeamLeader
                //    {
                //        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
                //        dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
                //        if (dt2.Rows.Count > 0)
                //        {
                //            dt2 = clsData.UploadLeader("2", "", dt2.Rows[0]["Team"].ToString());
                //            if (dt2.Rows.Count > 0)
                //                strMail = dt2.Rows[0]["Email"].ToString();
                //            else
                //                strMail = "";
                //        }
                //        else
                //            strMail = "";
                //    }
                //    else if (intJ == 3)  //設備代理人
                //    {
                //        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
                //        if (dt2.Rows.Count > 0)
                //        {
                //            dt2 = clsData.getEmployees("1", dt2.Rows[0]["Agent"].ToString());
                //            if (dt2.Rows.Count > 0)
                //                strMail = dt2.Rows[0]["Email"].ToString();
                //            else
                //                strMail = "";
                //        }
                //        else
                //            strMail = "";
                //    }
                //    else if (intJ == 4)   //單位最大主管
                //    {
                //        dt2 = clsData.UploadLeader("1", "", "");
                //        if (dt2.Rows.Count > 0)
                //            strMail = dt2.Rows[0]["Email"].ToString();
                //        else
                //            strMail = "";
                //    }
                //    else  //預約代理人
                //    {
                //        strMail = lblAgentEmail.Text;
                //    }

                //    if (strMail != "")
                //    {
                //        string strName = lblBorrower.Text;
                //        //string strStartDate, strEndDate;
                //        string strApparatus, strBodyM, strYN, strBodyM1;

                //        strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

                //        strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

                //        if (strStatus1 == "Y")
                //        {
                //            strBodyM = "請您與設備負責人借用設備";
                //            strYN = "通過";
                //        }
                //        else
                //        {
                //            strBodyM = "請洽設備負責人";
                //            strYN = "失敗";
                //        }

                //        string strBody = string.Format(strMailBody, strName, strBodyM1, strYN, strBodyM, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

                //        clsTransaction.SendMail(strMail, MailSubject, strBody);
                //    }
                //}
                //myMailBody.Close();
                //myMailBody.Dispose();
            //}
            //else
            //{
                //MAIL內容
                string strMail="";
                StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body4.txt");
                string strMailBody = myMailBody.ReadToEnd();


                string strName = lblBorrower.Text;
                //string strStartDate, strEndDate;
                string strApparatus, strBodyM, strYN, strBodyM1;

                strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

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

                for (int intI = 0; intI < 2; intI++)
                {
                    if (intI == 0)
                    {
                        DataTable dt1;
                        //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1P", "0");
                        dt1 = clsData.UploadApparatusMasterQuery("A1T", "0");
                        strMail = dt1.Rows[0]["Name"].ToString();

                        

                        clsTransaction.SendMail(strMail, MailSubject, strBody);


                        ////////////////////////////////////////
                        dt1 = clsData.UploadApparatusMasterQuery("A1W", "0");
                        strMail = dt1.Rows[0]["Name"].ToString();

                        //strMail = dt1.Rows[0]["Name"].ToString();

                        

                        clsTransaction.SendMail(strMail, MailSubject, strBody);
                        
                    }
                    else
                    {
                        DataTable dt2 = clsData.UploadLeader("1", "", "");
                        if (dt2.Rows.Count > 0)
                            strMail = dt2.Rows[0]["Email"].ToString();
                        else
                            strMail = "";

                        

                        clsTransaction.SendMail(strMail, MailSubject, strBody);
                    }
                    
                }
                myMailBody.Close();
                myMailBody.Dispose();
            //}
        //}




        #endregion
    }
    #endregion

    #region MailData
    private void MailData(string strStatus1)
    {
        #region 宣告變數

        //DateTime dt; 

        #endregion

        #region mail config

        //mail標題
        string MailSubject = "設備預約通知";



        #endregion

        #region 找資料塞到SendMail內


        //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1", "1");
        //string strMail = dt1.Rows[0]["Email"].ToString();

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                //MAIL內容
                StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body1.txt");
                string strMailBody = myMailBody.ReadToEnd();
                string strMail;
                DataTable dt2,dt3;
                DataTable dt = clsData.UploadReservationQuery(Request.QueryString["ID"].ToString(), "1");
                for (int intJ = 0; intJ < 6; intJ++)
                {
                    if (intJ == 0)
                        strMail = lblEmail.Text;
                    else if (intJ == 1)
                    {
                        //dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
                        dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(),"1","");
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

                        strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

                        strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

                        if (strStatus1 == "Y")
                        {
                            strBodyM = "請您與設備負責人借用設備";
                            strYN = "通過";
                        }
                        else
                        {
                            strBodyM = "請洽設備負責人";
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
                string strName = lblBorrower.Text;

                //string strStartDate, strEndDate;
                string strApparatus, strBodyM, strYN, strBodyM1;

                strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

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

                DataTable dt1;
                string strMail;
                dt1 = clsData.UploadApparatusMasterQuery("A1T", "0");
                strMail = dt1.Rows[0]["Name"].ToString();



                clsTransaction.SendMail(strMail, MailSubject, strBody);


                ////////////////////////////////////////
                dt1 = clsData.UploadApparatusMasterQuery("A1W", "0");
                strMail = dt1.Rows[0]["Name"].ToString();

                clsTransaction.SendMail(strMail, MailSubject, strBody);

                myMailBody.Close();
                myMailBody.Dispose();
            }
        }




        #endregion
    }
    #endregion

    #region MailData
    private void MailData1_Custodian(string strStatus1)
    {


        //mail標題
        string MailSubject = "設備續約通知";


        //for (int i = 0; i < 2; i++)
        //{
        //    if (i == 0)
        //    {
        //        //MAIL內容
        //        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body1.txt");
        //        string strMailBody = myMailBody.ReadToEnd();
        //        string strMail;
        //        DataTable dt2, dt3;
        //        DataTable dt = clsData.UploadReservationQuery(Request.QueryString["ID"].ToString(), "1");
        //        for (int intJ = 0; intJ < 6; intJ++)
        //        {
        //            if (intJ == 0)
        //                strMail = lblEmail.Text;
        //            else if (intJ == 1)
        //            {
        //                //dt2 = clsData.getEmployees("1", dt.Rows[0]["Custodian"].ToString());
        //                dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
        //                dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
        //                if (dt2.Rows.Count > 0)
        //                    strMail = dt2.Rows[0]["Email"].ToString();
        //                else
        //                    strMail = "";
        //            }
        //            else if (intJ == 2)
        //            {
        //                dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
        //                dt2 = clsData.getEmployees("1", dt2.Rows[0]["Custodian"].ToString());
        //                if (dt2.Rows.Count > 0)
        //                {
        //                    dt2 = clsData.UploadLeader("2", "", dt2.Rows[0]["Team"].ToString());
        //                    if (dt2.Rows.Count > 0)
        //                        strMail = dt2.Rows[0]["Email"].ToString();
        //                    else
        //                        strMail = "";
        //                }
        //                else
        //                    strMail = "";
        //            }
        //            else if (intJ == 3)
        //            {
        //                dt2 = clsData.UploadApparatusQuery(dt.Rows[0]["Apparatus_ID"].ToString(), "1", "");
        //                if (dt2.Rows.Count > 0)
        //                {
        //                    dt2 = clsData.getEmployees("1", dt2.Rows[0]["Agent"].ToString());
        //                    if (dt2.Rows.Count > 0)
        //                        strMail = dt2.Rows[0]["Email"].ToString();
        //                    else
        //                        strMail = "";
        //                }
        //                else
        //                    strMail = "";
        //            }
        //            else if (intJ == 4)
        //            {
        //                dt2 = clsData.UploadLeader("1", "", "");
        //                if (dt2.Rows.Count > 0)
        //                    strMail = dt2.Rows[0]["Email"].ToString();
        //                else
        //                    strMail = "";
        //            }
        //            else
        //            {
        //                strMail = lblAgentEmail.Text;
        //            }

        //            if (strMail != "")
        //            {
        //                string strName = lblBorrower.Text;
        //                //string strStartDate, strEndDate;
        //                string strApparatus, strBodyM, strYN, strBodyM1;

        //                strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

        //                strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

        //                if (strStatus1 == "Y")
        //                {
        //                    strBodyM = "請於" + lblDateR.Text + "歸還";
        //                    strYN = "通過";
        //                }
        //                else
        //                {
        //                    strBodyM = "請洽設備保管人";
        //                    strYN = "失敗";
        //                }

        //                string strBody = string.Format(strMailBody, strName, strBodyM1, strYN, strBodyM, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");

        //                clsTransaction.SendMail(strMail, MailSubject, strBody);
        //            }
        //        }
        //        myMailBody.Close();
        //        myMailBody.Dispose();
        //    }
        //    else
        //    {
                //MAIL內容
        StreamReader myMailBody = new StreamReader(Request.PhysicalApplicationPath + "mail\\mail_body4.txt", System.Text.Encoding.Default);
                string strMailBody = myMailBody.ReadToEnd();


                #region 找資料塞到SendMail內


                string strName = lblBorrower.Text;
                string strApparatus, strBodyM, strYN, strBodyM1;

                strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

                strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;


                if (strStatus1 == "Y")
                {
                    //strBodyM = "請於" + lblDateR.Text + "歸還";
                    strYN = "通過";
                }
                else
                {
                    //strBodyM = "請洽設備保管人";
                    strYN = "失敗";
                }

                string strBody = string.Format(strMailBody, strName, strBodyM1, strYN, "<br>", "<font face=arial size=2 color=#3333ff>", "</font>");


                string strMail;
                //string strMail = lblEmail.Text;
                for (int intI = 0; intI < 2; intI++)
                {
                    if (intI == 0)
                    {
                        //DataTable dt1 = clsData.UploadApparatusMasterQuery("A1P", "0");


                        //strMail = dt1.Rows[0]["Name"].ToString();

                        DataTable dt1;
                        //string strMail;
                        dt1 = clsData.UploadApparatusMasterQuery("A1T", "0");
                        strMail = dt1.Rows[0]["Name"].ToString();



                        clsTransaction.SendMail(strMail, MailSubject, strBody);


                        ////////////////////////////////////////
                        dt1 = clsData.UploadApparatusMasterQuery("A1W", "0");
                        strMail = dt1.Rows[0]["Name"].ToString();

                    }
                    else
                    {
                        DataTable dt2 = clsData.UploadLeader("1", "", "");
                        if (dt2.Rows.Count > 0)
                            strMail = dt2.Rows[0]["Email"].ToString();
                        else
                            strMail = "";

                        clsTransaction.SendMail(strMail, MailSubject, strBody);
                    }
                    

                    
                }
                myMailBody.Close();
                myMailBody.Dispose();
            //}
        //}

                #endregion
    }
    #endregion

    #region MailData
    private void MailData1(string strStatus1)
    {


        //mail標題
        string MailSubject = "設備續約通知";


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
                for (int intJ = 0; intJ < 6; intJ++)
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

                        strApparatus = lblName.Text + "(" + lblProductID.Text + ")";

                        strBodyM1 = lblDateB.Text + "~" + lblDateR.Text + "  " + strApparatus;

                        if (strStatus1 == "Y")
                        {
                            strBodyM = "請於" + lblDateR.Text + "歸還";
                            strYN = "通過";
                        }
                        else
                        {
                            strBodyM = "請洽設備保管人";
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

                strApparatus = lblName.Text + "(" + lblProductID.Text + ")";



                if (strStatus1 == "Y")
                {
                    strBodyM = "請於" + lblDateR.Text + "歸還";
                    strYN = "通過";
                }
                else
                {
                    strBodyM = "請洽設備保管人";
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


    private void getReservation()
    {
        DataTable dt = clsData.UploadReservationQuery(Request.QueryString["ID"].ToString(), "1");
        lblName.Text = dt.Rows[0]["Name"].ToString();
        lblProductID.Text = dt.Rows[0]["Products_ID"].ToString();
        lblBrand.Text = dt.Rows[0]["Brand"].ToString();
        lblModel.Text = dt.Rows[0]["Model"].ToString();
        //lblCustodian.Text = dt.Rows[0]["Custodian"].ToString();
        lblBorrower.Text = dt.Rows[0]["Borrower"].ToString();
        lblDepartment.Text = dt.Rows[0]["Department"].ToString();
        lblExt.Text = dt.Rows[0]["Ext"].ToString();
        lblEmail.Text = dt.Rows[0]["Email"].ToString();
        DateTime dTime;
        dTime = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString().Trim());
        lblDateB.Text = dTime.ToString("yyyy/MM/dd");
        //lblDateB.Text = dt.Rows[0]["StartDate"].ToString();
        dTime = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
        lblDateR.Text = dTime.ToString("yyyy/MM/dd");
        //lblDateR.Text = dt.Rows[0]["EndDate"].ToString();
        txtNote.Text = dt.Rows[0]["Note"].ToString();
        lblMission.Text = dt.Rows[0]["Mission"].ToString();
        lblCustomer.Text = dt.Rows[0]["Customer"].ToString();
        lblGName.Text = dt.Rows[0]["GName"].ToString();
        lblAID.Text = dt.Rows[0]["Apparatus_ID"].ToString();
        lblAgentName.Text = dt.Rows[0]["Agent"].ToString();
        lblAgentExt.Text = dt.Rows[0]["AgentExt"].ToString();
        lblAgentEmail.Text = dt.Rows[0]["AgentEmail"].ToString();
        lblKind.Text = dt.Rows[0]["Kind"].ToString();

        //if (dt.Rows[0]["Period"].ToString() == "D")
        //    lblPeriod.Text = "白天";
        //else if (dt.Rows[0]["Period"].ToString() == "N")
        //    lblPeriod.Text = "晚上";
        //else
        //    lblPeriod.Text = "";

        if (dt.Rows[0]["UseKind"].ToString() == "M")
            lblUseKind.Text = "手動測試";
        else if (dt.Rows[0]["UseKind"].ToString() == "A")
            lblUseKind.Text = "自動化程式";
        else
            lblUseKind.Text = "";
        //string strToday;
        //string strEndDate, strEndDate1;

        //strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        //strEndDate = add_Date(strToday, 7);
        //strEndDate1 = dt.Rows[0]["EndDate"].ToString();
        //strEndDate1 = strEndDate + " " + strEndDate1.Substring(10);

        //DateTime dtDate = Convert.ToDateTime(strToday);
        //lblDateB.Text = dtDate.ToString();
        //lblDateR.Text = strEndDate1;

        string strDate = "";
        if (dt.Rows[0]["ContinuousDate"].ToString() != "")
        {
            DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["ContinuousDate"].ToString());
            strDate = dt1.ToString("yyyy/MM/dd");
        }


        if ((dt.Rows[0]["ContinuousDate"].ToString() != "") && (strDate != "1900/01/01"))
        {
            lblDateR2.Visible = true ;
            lblDateR3.Visible = true ;

            dTime = Convert.ToDateTime(dt.Rows[0]["ContinuousDate"].ToString().Trim());
            lblDateR.Text = dTime.ToString("yyyy/MM/dd");

            dTime = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
            lblDateR2.Text = dTime.ToString("yyyy/MM/dd");
            //lblDateR2.Text = dt.Rows[0]["EndDate"].ToString();
            lblDateR1.Text = "預計歸還日期";
        }

    }
    protected void butReturn_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/ReservationList.aspx");
    }
}
