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
using ClosedXML.Excel;

public partial class WebForm_SearchApparatus : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");


        string strWrite;

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            lblAdd.Visible = false;
        }

        if (!IsPostBack)
        {
            loadKind(this.ddlKind);

        }
    }

    #region loadKind
    protected void loadKind(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 7, "0");
    }
    #endregion 

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddApparatus.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text,"0","");
        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    protected void gvwMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        string strWrite;

        HttpCookie cookie_Write = Request.Cookies["Write"];
        strWrite = Server.UrlDecode(cookie_Write.Value);

        if (strWrite == "N")
        {
            e.Row.Cells[0].Visible = false;
        }

    }

    protected void gvwMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[6].Text == "Y")
            //    e.Row.Cells[6].Text = "可借用";
            //else
            //    e.Row.Cells[6].Text = "不可借用";

            DataTable dt = clsData.getEmployees("1", e.Row.Cells[6].Text);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Name_CH"].ToString() != "")
                {
                    e.Row.Cells[6].Text = dt.Rows[0]["Name_CH"].ToString();
                    e.Row.Cells[7].Text = dt.Rows[0]["Extension"].ToString();
                }
            }
            else
            {
                e.Row.Cells[6].Text = "";
                e.Row.Cells[7].Text = "";
            }

        }
    }

    #region gvwMain_RowDeleting
    protected void gvwMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strPath, strPath1;
        string strName = ((Label)this.gvwMain.Rows[e.RowIndex].Cells[5].FindControl("lblGVSeq")).Text;
        //string path = Server.MapPath("./doc/") + ((HyperLink)this.gvList.Rows[e.RowIndex].Cells[1].FindControl("HyperLink1")).Text;
        if (clsTransaction.DelApparatus(strName) == true)
        {
            if (clsTransaction.DelApparatusFile(strName) == true)
            {
                strPath = @"d:\Apparatus\" + strName + @"\";
                strPath1 = @"d:\Apparatus\" + strName ;
                //Directory.Delete(strPath, true);

                DirectoryInfo DIFO = new DirectoryInfo(strPath);
                FileInfo[] filelist = DIFO.GetFiles();
                foreach (FileInfo fl in filelist)
                {
                    System.IO.File.Delete(fl.FullName);
                }
                Directory.Delete(strPath1, true);
                clsTransaction.DelPR_Goods(strName);
                //File.Delete(path);
                ((GridView)sender).SelectedIndex = -1;
                ((GridView)sender).EditIndex = -1;
                DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0","");
                this.gvwMain.DataSource = dt;
                this.DataBind();
            }
            else
            {
                clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
            }
        }
        else
        {
            clsMsg.AlertMessage("刪除失敗，請洽IT人員！", this.Page);
        }
    }
    #endregion

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strSystemNo;

        DataTable dt_new = new DataTable("dt_new");

        DataColumn column1 = new DataColumn("ID");
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = true;
        column1.Caption = "ID";
        column1.DefaultValue = "0";
        dt_new.Columns.Add(column1);

        DataColumn column2 = new DataColumn("products_id");
        column2.DataType = System.Type.GetType("System.String");
        column2.AllowDBNull = true;
        column2.Caption = "products_id";
        column2.DefaultValue = "0";
        dt_new.Columns.Add(column2);

        DataColumn column3 = new DataColumn("name");
        column3.DataType = System.Type.GetType("System.String");
        column3.AllowDBNull = true;
        column3.Caption = "name";
        column3.DefaultValue = "0";
        dt_new.Columns.Add(column3);

        DataColumn column4 = new DataColumn("brand");
        column4.DataType = System.Type.GetType("System.String");
        column4.AllowDBNull = true;
        column4.Caption = "brand";
        column4.DefaultValue = "0";
        dt_new.Columns.Add(column4);

        DataColumn column5 = new DataColumn("model");
        column5.DataType = System.Type.GetType("System.String");
        column5.AllowDBNull = true;
        column5.Caption = "model";
        column5.DefaultValue = "0";
        dt_new.Columns.Add(column5);

        DataColumn column6 = new DataColumn("number");
        column6.DataType = System.Type.GetType("System.String");
        column6.AllowDBNull = true;
        column6.Caption = "number";
        column6.DefaultValue = "0";
        dt_new.Columns.Add(column6);

        DataColumn column7 = new DataColumn("ReservationStatus");
        column7.DataType = System.Type.GetType("System.String");
        column7.AllowDBNull = true;
        column7.Caption = "ReservationStatus";
        column7.DefaultValue = "0";
        dt_new.Columns.Add(column7);

        DataColumn column8 = new DataColumn("InspectionDate");
        column8.DataType = System.Type.GetType("System.String");
        column8.AllowDBNull = true;
        column8.Caption = "InspectionDate";
        column8.DefaultValue = "0";
        dt_new.Columns.Add(column8);

        DataColumn column9 = new DataColumn("MaintenanceDate");
        column9.DataType = System.Type.GetType("System.String");
        column9.AllowDBNull = true;
        column9.Caption = "MaintenanceDate";
        column9.DefaultValue = "0";
        dt_new.Columns.Add(column9);

        DataColumn column10 = new DataColumn("Place");
        column10.DataType = System.Type.GetType("System.String");
        column10.AllowDBNull = true;
        column10.Caption = "Place";
        column10.DefaultValue = "0";
        dt_new.Columns.Add(column10);

        DataColumn column11 = new DataColumn("Custodian");
        column11.DataType = System.Type.GetType("System.String");
        column11.AllowDBNull = true;
        column11.Caption = "Custodian";
        column11.DefaultValue = "0";
        dt_new.Columns.Add(column11);

        DataColumn column12 = new DataColumn("Note");
        column12.DataType = System.Type.GetType("System.String");
        column12.AllowDBNull = true;
        column12.Caption = "Note";
        column12.DefaultValue = "0";
        dt_new.Columns.Add(column12);

        //for (int i = 0; i < this.gvwMain.Rows.Count; i++)
        //{
        //    if (((CheckBox)gvwMain.Rows[i].FindControl("CheckBox2")).Checked)
        //    {
        //        strSystemNo = ((Label)this.gvwMain.Rows[i].Cells[3].FindControl("lblGVSeq")).Text;
        //        //DataTable dt2 = clsData.UploadTestPlanQuery(4, strSystemNo, "", "ALL");
        //        DataTable dt2 = clsData.UploadApparatusQuery(strSystemNo, "1", "");
        //        DataRow dr = dt_new.NewRow();

        //        dr["ID"] = dt2.Rows[0]["ID"].ToString();
        //        dr["products_id"] = dt2.Rows[0]["products_id"].ToString();
        //        dr["name"] = dt2.Rows[0]["name"].ToString();
        //        dr["brand"] = dt2.Rows[0]["brand"].ToString();
        //        dr["model"] = dt2.Rows[0]["model"].ToString();
        //        dr["number"] = dt2.Rows[0]["number"].ToString();
        //        if (dt2.Rows[0]["ReservationStatus"].ToString() == "Y")
        //            dr["ReservationStatus"] = "可借用";
        //        else
        //            dr["ReservationStatus"] = "不可借用";
        //        dr["InspectionDate"] = dt2.Rows[0]["InspectionDate"].ToString();
        //        dr["MaintenanceDate"] = dt2.Rows[0]["MaintenanceDate"].ToString();
        //        dr["Place"] = dt2.Rows[0]["Place"].ToString();
        //        dr["Custodian"] = dt2.Rows[0]["Custodian"].ToString();

        //        dt_new.Rows.Add(dr);
                
        //    }
        //}

        //dt_new.Columns.Remove("ID");

        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    dt_new1.TableName = "Summary";
        //    wb.Worksheets.Add(dt_new1);
        //    dt_new.TableName = "TestCase";
        //    wb.Worksheets.Add(dt_new);
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=TestPlan.xls");
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(memoryStream);
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.WriteTo(Response.OutputStream);
        //        memoryStream.Close();
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

    }
}
