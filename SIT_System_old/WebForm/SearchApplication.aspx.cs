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

public partial class WebForm_SearchApplication : System.Web.UI.Page
{
    //public static string strNumber1;
    //public static string strName1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["AppNo"] == null)
            Response.Redirect("~/ApplicationDefault.aspx");

        if (!IsPostBack)
        {
            GvQuery();
            GvQuery1();
        }
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    //strNumber1 = txtNumber.Text.Trim();
    //    //strName1 = txtName.Text.Trim();
    //    GvQuery(txtNumber.Text.Trim(), txtName.Text.Trim());
    //}

    #region gvList_RowDeleting
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        bool bTF = false;
        string strNumber1 = ((Label)this.gvList.Rows[e.RowIndex].Cells[0].FindControl("lblID")).Text.Trim();

        //if (clsTransaction.DelApplication(strNumber1) == true)
        //{
        //    if (clsTransaction.DelProjectCaseData(strNumber1, "", "1") == true)
        //    {
        //        if (clsTransaction.DelApplication_File(strNumber1) == true)
        //        {
        //            if (clsTransaction.DelUploadFilesCase("", strNumber1, "", "2") == true)
        //            {
        //                string strPath;

        //                strPath = @"D:\Application\" + strNumber1;
        //                //System.IO.Directory.Delete(strPath);
        //                if (Directory.Exists(strPath) == true)
        //                    Directory.Delete(strPath, true);

        //                bTF = true;
        //            }
        //        }
        //    }
        //}

        if (clsTransaction.DelApplication_Temporarily(strNumber1) == true)
        {
            if (clsTransaction.DelProjectCaseData_Temporarily(strNumber1, "", "1") == true)
            {
                if (clsTransaction.DelApplicationTestCase_Temporarily(strNumber1) == true)
                {
                    if (clsTransaction.DelApplication_File(strNumber1) == true)
                    {
                        if (clsTransaction.DelUploadFilesCase("", strNumber1, "", "2") == true)
                        {
                            string strPath;

                            strPath = @"D:\Application\" + strNumber1;
                            //System.IO.Directory.Delete(strPath);
                            if (Directory.Exists(strPath) == true)
                                Directory.Delete(strPath, true);

                            bTF = true;
                        }
                    }
                }
            }

        }

        //bool bTF = false;
        //if (clsTransaction.DelApplication(txtNumber.Text.Trim()) == true)
        //{
        //    if (clsTransaction.DelProjectCaseData(txtNumber.Text.Trim(), "", "1") == true)
        //    {
        //        if (clsTransaction.DelApplication_Wireless(txtNumber.Text.Trim()) == true)
        //        {
        //            if (clsTransaction.DelApplication_WiFi(txtNumber.Text.Trim()) == true)
        //            {
        //                if (clsTransaction.DelApplication_USB(txtNumber.Text.Trim()) == true)
        //                {
        //                    if (clsTransaction.DelApplication_LTE(txtNumber.Text.Trim()) == true)
        //                    {
        //                        if (clsTransaction.DelApplication_DSL(txtNumber.Text.Trim()) == true)
        //                        {
        //                            if (clsTransaction.DelApplication_Bluetooth(txtNumber.Text.Trim()) == true)
        //                            {
        //                                if (clsTransaction.DelApplication_File(txtNumber.Text.Trim()) == true)
        //                                {
        //                                    //if (clsTransaction.DelApplication_ProjectCase(strNumber1) == true)
        //                                    //{
        //                                        //File.Delete(path);
        //                                        ((GridView)sender).SelectedIndex = -1;
        //                                        ((GridView)sender).EditIndex = -1;
        //                                        GvQuery(txtNumber.Text.Trim(), txtName.Text.Trim());

        //                                        string strPath;

        //                                        strPath = @"D:\Application\" + txtNumber.Text.Trim();
        //                                        System.IO.Directory.Delete(strPath);

        //                                        bTF = true;
        //                                    //}
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        if (bTF == false)
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);

        GvQuery();
        GvQuery1();
    }
    #endregion

    #region gvList_RowUpdating (指定資料行更新)
    protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Response.Redirect("~/WebForm/SearchApplication.aspx?ID=" + strNumber1);
        //string Code_Name = ((Label)this.gvList.Rows[e.RowIndex].Cells[3].FindControl("lblName1")).Text;
        //string Code_UName = ((TextBox)this.gvList.Rows[e.RowIndex].Cells[2].FindControl("txtName")).Text;

        //if (clsTransaction.UpDateInfoData(Code_Name, Code_UName) == true)
        //{
        //    ((GridView)sender).SelectedIndex = -1;
        //    ((GridView)sender).EditIndex = -1;
        //    GvQuery();
        //}
        //else
        //{
        //    clsMsg.AlertMessage("更新失敗，請洽IT人員！", this.Page);
        //}
    }
    #endregion

    #region GvQuery
    private void GvQuery()
    {
        DataTable dt1 = clsData.UploadNumber(Session["AppNo"].ToString());



        //DataTable dt = clsData.UploadApplicationIDQuery(Session["AppDep"].ToString(), dt1.Rows[0]["Name"].ToString().Trim());
        DataTable dt = clsData.UploadApplication_TemporarilyIDQuery(Session["AppDep"].ToString(), dt1.Rows[0]["Name"].ToString().Trim());
        this.gvList.DataSource = dt;
        this.DataBind();
    }
    #endregion

    #region GvQuery
    private void GvQuery1()
    {
        DataTable dt1 = clsData.UploadNumber(Session["AppNo"].ToString());


        DataTable dt2 = clsData.UploadProjectPerson2(dt1.Rows[0]["Name"].ToString(), dt1.Rows[0]["Department"].ToString());
        
        DataTable dt = new DataTable("dt");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt.Columns.Add(column1);

        DataColumn column2 = new DataColumn("Name");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "Name";
        column2.DefaultValue = "0";
        dt.Columns.Add(column2);

        DataColumn column3 = new DataColumn("Status");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "Status";
        column3.DefaultValue = "0";
        dt.Columns.Add(column3);

        DataColumn column4 = new DataColumn("EndDate");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "EndDate";
        column4.DefaultValue = "0";
        dt.Columns.Add(column4);

        DataColumn column5 = new DataColumn("File_Name");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "File_Name";
        column5.DefaultValue = "0";
        dt.Columns.Add(column5);

        DataColumn column6 = new DataColumn("StartDate");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "StartDate";
        column6.DefaultValue = "0";
        dt.Columns.Add(column6);       

        //DataColumn column7 = new DataColumn("File_Path");
        //column7.DataType = System.Type.GetType("System.String");
        //column7.AllowDBNull = true;
        //column7.Caption = "File_Path";
        //column7.DefaultValue = "0";
        //dt.Columns.Add(column7);


        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string strStatus = "";
                string strStart1,strEnd;

                DataRow dr = dt.NewRow();

                dr["ID"] = dt2.Rows[i]["ID"].ToString();
                dr["Name"] = dt2.Rows[i]["Name"].ToString();

                if ((dt2.Rows[i]["Status"].ToString() == ""))
                {
                    strStatus = "申請單審核中";
                }
                else if ((dt2.Rows[i]["Status"].ToString() == "Open") && (dt2.Rows[i]["Assign"].ToString() == ""))
                {
                    strStatus = "任務指派中";
                }
                else if ((dt2.Rows[i]["Status"].ToString() == "Open") && (dt2.Rows[i]["Assign"].ToString() != ""))
                {
                    strStatus = "Assign - " + dt2.Rows[i]["Assign"].ToString();
                }
                else if ((dt2.Rows[i]["Status"].ToString() == "Close") && (dt2.Rows[i]["Assign"].ToString() != ""))
                {
                    strStatus = "任務已結束";
                }

                dr["Status"] = strStatus;
                
                DateTime dt_Date = Convert.ToDateTime(dt2.Rows[i]["End_Date"].ToString());
                DateTime dt_Date2 = Convert.ToDateTime(dt2.Rows[i]["Start_Date"].ToString());
                strEnd = dt_Date.ToString("yyyy/MM/dd");
                strStart1 = dt_Date2.ToString("yyyy/MM/dd");
                if (strEnd == "1900/01/01")
                    dr["EndDate"] = "";
                else
                    dr["EndDate"] = strEnd;

                if (strStart1 == "1900/01/01")
                    dr["StartDate"] = "";
                else
                    dr["StartDate"] = strStart1;

                //DataTable dt3 = clsData.UploadProjectPerson1(dt1.Rows[0]["Name"].ToString(), dt1.Rows[0]["Department"].ToString(), dt2.Rows[i]["ID"].ToString());

                //if (dt3.Rows.Count > 0)
                //{
                //    dr["File_Name"] = dt3.Rows[0]["File_Name"].ToString();
                //    dr["File_Path"] = dt3.Rows[0]["File_Path"].ToString();
                //}
                //else
                //{
                //    dr["File_Name"] = "";
                //    dr["File_Path"] = "";
                //}
                dt.Rows.Add(dr);
            }
        }

        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    #region gvList_RowEditing (指定資料行進行修改)
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        string strNumber1 = ((Label)this.gvList.Rows[e.NewEditIndex].Cells[0].FindControl("lblID")).Text.Trim();

        if (((Label)this.gvList.Rows[e.NewEditIndex].Cells[2].FindControl("lblKind")).Text.Trim() == "驗証申請")
            Response.Redirect("~/WebForm/ModifyApplication.aspx?ID=" + strNumber1);
        else
            Response.Redirect("~/WebForm/ModifyCApplication.aspx?ID=" + strNumber1);
        //((GridView)sender).EditIndex = e.NewEditIndex;
        //GvQuery();
    }
    #endregion

    #region gvList_PageIndexChanging
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
        GvQuery1();
    }
    #endregion

    #region gvList_RowCancelingEdit (指定資料行取消修改)
    protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ((GridView)sender).SelectedIndex = -1;
        ((GridView)sender).EditIndex = -1;
        GvQuery();
        GvQuery1();
    }
    #endregion

    protected void gvwList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        e.Row.Cells[2].Visible = false;

    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
    }
    #endregion

    //protected void lbtnAdd_Click(object sender, EventArgs e)
    //{
    //    //string strID1 = "";
    //    Server.Transfer("~/WebForm/Application.aspx");
    //}
}
