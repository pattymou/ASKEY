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

public partial class WebForm_UserView1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((clsParameter.strEmpNo == "") || (clsParameter.strEmpNo == null))
        //    Response.Redirect("~/Default.aspx");
        if (Session["EmpNo"] == null)
            Response.Redirect("~/Default.aspx");
        if (!IsPostBack)
        {
            loadDepartment(this.ddlDepartment);
            GvQuery();
            GvQuery1("0", "");
        }
    }

    #region loadDepartment
    protected void loadDepartment(DropDownList DDL)
    {
        clsDropDownList.ddlInfoFunction(DDL, 3, "1");
    }
    #endregion

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        GvQuery1("2", ddlDepartment.SelectedValue);
    }

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            //if (clsTransaction.UpDateReservation("C", strID, "", "1") == true)
            //    clsMsg.AlertMessage("取消成功！", this.Page);
            //else
            //    clsMsg.AlertMessage("取消失敗！", this.Page);

            //GvQuery();
            //Server.Transfer("AddUser.aspx?ID=" + strID);
            Response.Redirect("AddUser.aspx?ID=" + strID);
            //Response.Write("<script>window.open('AddUser.aspx?ID=" + strID + "');</script>");
        }
        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            //string strToday;

            //strToday = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            //GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            //strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            if (clsTransaction.DelEmployees(strID) == true)
            {
                if (clsTransaction.DelEmployees_Authority(strID) == true)
                    clsMsg.AlertMessage("刪除成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("刪除失敗！", this.Page);

            GvQuery();
            GvQuery1("0","");
        }
    }

    private void GvQuery()
    {
        DataTable dt;


        dt = clsData.getEmployees("0", "");
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    private void GvQuery1(string strKind,string strSeach)
    {
        DataTable dt;


        dt = clsData.getNumber(strKind, strSeach);
        this.gvwMain1.DataSource = dt;
        this.DataBind();
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/AddUser.aspx");
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
        GvQuery1("0", "");
    }
    #endregion

    #region gvwMain1_PageIndexChanging
    protected void gvwMain1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        GvQuery();
        GvQuery1("2", ddlDepartment.SelectedValue);
    }
    #endregion

    protected void gvwMain1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);

            //Server.Transfer("ModifyNumber.aspx?ID=" + strID);
            Response.Redirect("ModifyNumber.aspx?ID=" + strID);
            //Response.Write("<script>window.open('ModifyNumber.aspx?ID=" + strID + "');</script>");
        }
        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[0].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);


            if (clsTransaction.DelNumber(strID) == true)
            {
                //if (clsTransaction.DelEmployees_Authority(strID) == true)
                    clsMsg.AlertMessage("刪除成功！", this.Page);
            }
            else
                clsMsg.AlertMessage("刪除失敗！", this.Page);

            GvQuery1("2", ddlDepartment.SelectedValue);
            GvQuery();
        }
    }
}
