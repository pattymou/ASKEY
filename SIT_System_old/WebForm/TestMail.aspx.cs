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
using System.Net.Mail;
using System.Collections.Generic;

public partial class WebForm_TestMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void butOK_Click(object sender, EventArgs e)
    {
        SmtpClient sc = new SmtpClient("smtp.askey.com.tw");

        List<string> MailList = new List<string>();
        //MailAddress receiverAddress = new MailAddress("patty_lu@askey.com.tw,sam01_chien@askey.com.tw,ally_lin@askey.com.tw");
        //MailAddress senderAddress = new MailAddress("sit_da40@askey.com.tw", "SIT");
        //MailMessage mail = new MailMessage(senderAddress, receiverAddress);
        MailMessage mail = new MailMessage();

        

        MailList.Add("patty_lu@askey.com.tw");
        MailList.Add("sam01_chien@askey.com.tw");
        MailList.Add("ally_lin@askey.com.tw");

        mail.From = new MailAddress("sit_da40@askey.com.tw", "SIT");
        mail.To.Add(string.Join(",", MailList.ToArray()));

        mail.Subject = "主旨";
        mail.Body = "test123";
        sc.Send(mail);
        clsMsg.AlertMessage("發送成功....", this.Page);

    }
    protected void butReturn_Click(object sender, EventArgs e)
    {

    }
}
