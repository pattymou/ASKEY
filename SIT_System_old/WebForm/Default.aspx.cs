using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebForm_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        TableRow row = new TableRow();
        Label lbl1 = new Label();
        lbl1.Text = "No.1";
        TableCell cell1 = new TableCell();
        Literal liter1 = new Literal();
        liter1.Text = "<div id=\"piechart_div\" style=\"border: 1px solid #ccc\"></div>";
        cell1.Controls.Add(liter1);
        row.Cells.Add(cell1);
        table1.Rows.Add(row);

    }
}
