using System;
//using System.Data;
//using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;


/// <summary>
/// ProjectInfo 的摘要描述
/// </summary>
public class ProjectInfo
{
    private string Project_ID = string.Empty;
    private Guid Project_Guid = Guid.Empty;

	public ProjectInfo()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
        Project_Guid = Guid.NewGuid();
	}

    public void Close()
    {
        Project_ID = string.Empty;
    }

    public string ID
    {
        get
        {
            return Project_ID;
        }
        set
        {
            Project_ID = value;
        }
    }
}
