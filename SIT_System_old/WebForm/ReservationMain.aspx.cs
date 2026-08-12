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

public partial class WebForm_ReservationMain : System.Web.UI.Page
{
    public static string strRule;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadKind(this.ddlKind);
        }

        if ((Session["EmpNo"] == null) || (Session["EmpNo"] == ""))
        {
            //linkCancel.Visible = false;
            linkCancel.Text = "[取消預約設備]";
            //linkDelay.Visible = false;
            if (Session["AppNo"] == null)
                Response.Redirect("~/SystemDefault.aspx");
            else if (Session["AppNo"].ToString() == "Public")
            {
                lblAdd.Visible = false;
                linkContinuous.Visible = false;
            }

            HttpCookie cookie_Rule = Request.Cookies["Rule"];
            if (cookie_Rule == null)
                Response.Redirect("~/WebForm/HomePage_N.aspx");

            strRule = Server.UrlDecode(cookie_Rule.Value);
            if (strRule == "")
                Response.Redirect("~/WebForm/HomePage_N.aspx");

        }
        else
        {
            //linkCancel.Visible = true;
            linkCancel.Text = "[取消/歸還預約設備]";
            //linkDelay.Visible = true;
            //linkCancel.Visible = false;
            //string strRule;

            //HttpCookie cookie_Rule = Request.Cookies["Rule"];
            //if (cookie_Rule == null)
            //    Response.Redirect("~/WebForm/HomePage_N.aspx");

            //strRule = Server.UrlDecode(cookie_Rule.Value);
            //if (strRule == "")
            //    Response.Redirect("~/WebForm/HomePage_N.aspx");
        }
        string strID;

        strID = Session["EmpName"].ToString().Trim();
        //strID = "patty_lu";
        if ((strID == "") || (strID == null))
        {
            linkCancel.Visible = false;
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
        Server.Transfer("~/WebForm/ApparatusReservation.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //=====0217
        //DataTable dt = clsData.UploadApparatusStatus(txtSearch.Text, "0", "");
        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);
        //=====0217
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }

    #region gvwMain_PageIndexChanging
    protected void gvwMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        ((GridView)sender).EditIndex = -1;
        ((GridView)sender).SelectedIndex = -1;
        //=====0217
        //DataTable dt = clsData.UploadApparatusStatus(txtSearch.Text, "0", "");
        DataTable dt = clsData.UploadApparatusQuery(txtSearch.Text, "0", ddlKind.Text);
        //=====0217
        this.gvwMain.DataSource = dt;
        this.DataBind();
    }
    #endregion

    protected void gvwMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strID;



        if (e.CommandName == "AddToCart")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('ReservationView_jq.aspx?ID=" + strID + "&Kind=0');</script>");
            
        }
        if (e.CommandName == "AddToCart1")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('ApparatusView.aspx?ID=" + strID + "&Kind=0');</script>");
        }

        if (e.CommandName == "AddToCart2")
        {
            GridViewRow row = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            strID = ((Label)row.Cells[5].FindControl("lblGVSeq")).Text.Trim();
            //DataTable dt = clsData.getReservationView(strID);
            Response.Write("<script>window.open('History_R_Detail.aspx?ID=" + strID + "&Kind=0');</script>");
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

            if ((Session["EmpNo"] == null) || (Session["EmpNo"] == ""))
            {
                e.Row.Cells[11].Visible = false;
            }

        }



    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        //if (linkDelay.Visible == false)
        //    Server.Transfer("~/WebForm/DepartmentReservationCancel.aspx");
        //else
            Server.Transfer("~/WebForm/ReservationCancel.aspx");
    }
    //protected void lbtnDelay_Click(object sender, EventArgs e)
    //{
    //    Server.Transfer("~/WebForm/DelayApparatus.aspx");
    //}
    protected void lbtnContinuous_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/WebForm/ApparatusContinuous.aspx");
    }
}
