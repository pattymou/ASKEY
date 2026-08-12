<%@ Page ContentType="application/json" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Runtime.Serialization.Json" %>
<%@ Import Namespace="System.Runtime.Serialization" %>
<%@ Import Namespace="System.Runtime.Serialization.Json" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data" %>

<script language="C#" runat="server">    
    // 檔案的上傳路徑
    //private string Upload_Directory =  @"c:\test\"; 
    private string Upload_Directory;
    private static readonly int BUFFER_SIZE = 4 * 1024 * 1024;
            
    [DataContract]
    private class FileResponse
    {
        [DataMember]
        public string name;
        [DataMember]
        public long size;
        [DataMember]
        public string type;
        [DataMember]
        public string url;
        [DataMember]
        public string error;
        [DataMember]
        public string deleteUrl;
        [DataMember]
        public string deleteType;
    }
    
    [DataContract]
    private class UploaderResponse
    {
        [DataMember]
        public FileResponse[] files;
        public UploaderResponse(FileResponse[] fileResponses)
        {
            files = fileResponses;
        }
    }
    private FileResponse CreateFileResponse(string fileName, long size, string error)
    {
        return new FileResponse()
        {
            name = Path.GetFileName(fileName),
            size = size,
            type = String.Empty,
            url = String.Format("{0}?{1}={2}", Request.Url.AbsoluteUri, "file", HttpUtility.UrlEncode(Path.GetFileName(fileName))),
            error = error,
            deleteUrl = String.Format("{0}?{1}={2}", Request.Url.AbsoluteUri, "file", HttpUtility.UrlEncode(Path.GetFileName(fileName))),
			deleteType = "POST"
        };
    }

    private void SerializeUploaderResponse(List<FileResponse> fileResponses)
    {  // 將物件序列化為 JavaScript 物件標記法 (JSON) 以及將 JSON 資料還原序列化為物件
        DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(UploaderResponse));
        // 將物件序列化為可以對應至 JavaScript 物件標記法 (JSON) 的 XML。 使用 XmlWriter 來寫入所有的物件資料，包括起始 XML 項目、內容和結尾項目。 
        Serializer.WriteObject(Response.OutputStream, new UploaderResponse(fileResponses.ToArray()));        
    }
    
    private void FromStreamToStream(Stream source, Stream destination) // 搭配 GET method 使用
    {
        int BufferSize = source.Length >= BUFFER_SIZE ? BUFFER_SIZE : (int)source.Length;
        long BytesLeft = source.Length;
        byte[] Buffer = new byte[BufferSize];
        int BytesRead = 0;
        while (BytesLeft > 0)
        {
            BytesRead = source.Read(Buffer, 0, BytesLeft > BufferSize ? BufferSize : (int)BytesLeft);
            destination.Write(Buffer, 0, BytesRead);
            BytesLeft -= BytesRead;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string QueryFileName = Request["file"];  //從POST資料取出上傳的檔案名稱
        //clsParameter.strFileName = Request["file"];

        //string[] sArray = QueryFileName.Split('-');

        //DataTable dt = clsData.UploadFilePathQuery(sArray[3].Trim());
        //Upload_Directory = @"e:\Test Report\" + sArray[5].Trim() + @"\" + sArray[4].Trim() + @"\" + clsParameter.strCustomer + @"\" + sArray[6].Trim() + @"\" + dt.Rows[0]["TestCase"].ToString() + @"\";
        //Response.Write(Upload_Directory);
        //Upload_Directory = @"e:\Test Report\";
        
        //if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
        //{
        //    Directory.CreateDirectory(Upload_Directory);
        //}

        //Upload_Directory = @"e:\Test Report\" + sArray[5].Trim() + @"\";
        //if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
        //{
        //    Directory.CreateDirectory(Upload_Directory);
        //}
        //Upload_Directory = @"e:\Test Report\" + sArray[5].Trim() + @"\" + sArray[4].Trim() + @"\";
        //if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
        //{
        //    Directory.CreateDirectory(Upload_Directory);
        //}

        //Upload_Directory = @"e:\Test Report\" + sArray[5].Trim() + @"\" + sArray[4].Trim() + @"\" + clsParameter.strCustomer + @"\";
        //if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
        //{
        //    Directory.CreateDirectory(Upload_Directory);
        //}

        //Upload_Directory = @"e:\Test Report\" + sArray[5].Trim() + @"\" + sArray[4].Trim() + @"\" + clsParameter.strCustomer + @"\" + sArray[6].Trim() + @"\";
        //if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
        //{
        //    Directory.CreateDirectory(Upload_Directory);
        //}

        //Upload_Directory = @"e:\Test Report\" + sArray[5].Trim() + @"\" + sArray[4].Trim() + @"\" + clsParameter.strCustomer + @"\" + sArray[6].Trim() + @"\" + dt.Rows[0]["TestCase"].ToString() + @"\";
        //if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
        //{
        //    Directory.CreateDirectory(Upload_Directory);
        //}
        //Session["FileN"] = "";
        string FullFileName = null;     //用來放完整路徑
        string ShortFileName = null;  //存放檔名
        // 判斷上傳的檔名變數是否有內容 

        if (QueryFileName != null) // param specified, but maybe in wrong format (empty). else user will download json with listed files
        {
            ShortFileName = HttpUtility.UrlDecode(QueryFileName);     //取出檔名
            
            
            
            
            FullFileName = String.Format(@"{0}\{1}", Upload_Directory, ShortFileName);   // 結合完整路徑與檔名~~成為完整PATH
            //Session["File1"] = Session["File1"] + FullFileName;
            //Response.Write(FullFileName);
            if (QueryFileName.Trim().Length == 0 || !File.Exists(FullFileName))  //判斷檔案是否存在
            {
                Response.StatusCode = 404;
                Response.StatusDescription = "File not found";
                Response.End();
                return;
            }
        }       
        
        if (Request.HttpMethod.ToUpper() == "GET")   // ---- GET 的處理片段  -----------------------------------
        {           
            if (FullFileName != null)
            {
                Response.ContentType = "application/octet-stream";                   // http://www.digiblog.de/2011/04/android-and-the-download-file-headers/ :)
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}{1}", Path.GetFileNameWithoutExtension(ShortFileName), Path.GetExtension(ShortFileName).ToUpper()));
                using (FileStream FileReader = new FileStream(FullFileName, FileMode.Open, FileAccess.Read))
                {
                    FromStreamToStream(FileReader, Response.OutputStream);
                    Response.OutputStream.Close();
                }
                Response.End();
                return;
            }
            else  // FullFileName == null
            {
                //檔案列表
                List<FileResponse> FileResponseList = new List<FileResponse>();              
                string[] FileNames = Directory.GetFiles(Upload_Directory);
                DateTime TimeRange = DateTime.Now.AddDays(-1); //一天內
                
                foreach (string FileName in FileNames)
                {
                    if (new FileInfo(FileName).CreationTime > TimeRange)
                    {
                        FileResponseList.Add(CreateFileResponse(FileName, new FileInfo(FileName).Length, String.Empty));
                    }
                }
                SerializeUploaderResponse(FileResponseList);
            }
        }  //EOF --- if (Request.HttpMethod.ToUpper() == "GET")
        else if (Request.HttpMethod.ToUpper() == "POST" && Request.QueryString["file"] == null) // ---- POST 的處理片段  -----------------------------------
        {
            List<FileResponse> FileResponseList = new List<FileResponse>();
            for (int FileIndex = 0; FileIndex < Request.Files.Count; FileIndex++)  //利用 HttpPostedFile 方式來將檔案寫入到 Server
            {
                string strFileName, strApplicationID;
                HttpPostedFile File = Request.Files[FileIndex];
                //string FileName = String.Format(@"{0}\{1}", Upload_Directory, Path.GetFileName(File.FileName));
                /////
                //clsParameter.strFileName = Path.GetFileName(File.FileName);
                strFileName = Path.GetFileName(File.FileName);

                //HttpCookie cookie_Upload_Kind = Request.Cookies["Upload_Kind"];
                //strUpload_Kind = Server.UrlDecode(cookie_Upload_Kind.Value);
                
                if (Session["Upload_Kind"].ToString() == "TestReport")//子任務檔案上傳
                {
                    //string[] sArray = clsParameter.strFileName.Split('-');
                    string[] sArray = strFileName.Split('-');

                    try
                    {
                        //if (sArray[6] != "")
                        //{
                            DataTable dt = clsData.UploadFilePathQuery(sArray[3].Trim());


                            //HttpCookie cookie_Customer = Request.Cookies["Project"];
                            Upload_Directory = @"d:\Test Report\" + Session["Dep"].ToString() + @"\" + sArray[4].Trim() + @"\" + Session["Customer"].ToString() + @"\" + sArray[6].Trim() + @"\" + dt.Rows[0]["TestCase"].ToString() + @"\";
                        //}
                        //else
                        //{
                        //    string strProjectName_Cookie, strCaseName_Cookie, strDetailName_Cookie;
                        //    HttpCookie cookie_ProjectName = Request.Cookies["ProjectName"];
                        //    strProjectName_Cookie = Server.UrlDecode(cookie_ProjectName.Value);

                        //    HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
                        //    strCaseName_Cookie = Server.UrlDecode(cookie_CaseName.Value);

                        //    HttpCookie cookie_DetailName = Request.Cookies["DetailName"];
                        //    strDetailName_Cookie = Server.UrlDecode(cookie_DetailName.Value);

                        //    Upload_Directory = @"d:\專案管理\" + strProjectName_Cookie + @"\" + strCaseName_Cookie + @"\" + cookie_DetailName + @"\";
                        //}
                    }
                    catch
                    {
                        //string strProjectName_Cookie, strCaseName_Cookie, strDetailName_Cookie, strUpload_Project_Kind_Cookie;
                        //HttpCookie cookie_ProjectName = Request.Cookies["ProjectName"];
                        //strProjectName_Cookie = Server.UrlDecode(cookie_ProjectName.Value);

                        //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
                        //strCaseName_Cookie = Server.UrlDecode(cookie_CaseName.Value);

                        //HttpCookie cookie_DetailName = Request.Cookies["DetailName"];
                        //strDetailName_Cookie = Server.UrlDecode(cookie_DetailName.Value);

                        //HttpCookie cookie_Upload_Project_Kind = Request.Cookies["Upload_Project_Kind"];
                        //strUpload_Project_Kind_Cookie = Server.UrlDecode(cookie_Upload_Project_Kind.Value);

                        //Upload_Directory = @"d:\專案管理\" + strProjectName_Cookie + @"\" + strCaseName_Cookie + @"\" + strDetailName_Cookie + @"\";
                        Upload_Directory = @"d:\" + Session["Upload_Project_Kind"].ToString() + @"\" + Session["ProjectName"].ToString() + @"\" + Session["CaseName"].ToString() + @"\" + Session["ItemName"].ToString() + @"\";


                    }
                    //Upload_Directory = @"d:\Test Report\" + clsParameter.strDepartment + @"\" + sArray[4].Trim() + @"\" + clsParameter.strCustomer + @"\" + sArray[6].Trim() + @"\" + dt.Rows[0]["TestCase"].ToString() + @"\";
                }
                else if (Session["Upload_Kind"].ToString() == "Apparatus")//設備
                {
                    //DataTable dt1 = clsData.UploadApparatusLastIDQuery();

                    //clsParameter.strApparatusID = dt1.Rows[0]["ID"].ToString();
                    //if (clsParameter.strApparatusID == "")
                    //    clsParameter.strApparatusID = "1";
                    //else
                    //    clsParameter.strApparatusID = (Int32.Parse(clsParameter.strApparatusID) +1).ToString();
                    //string strApparatusID_Cookie;
                    //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
                    //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

                    Upload_Directory = @"d:\Apparatus\" + Session["ApparatusID"].ToString() + @"\";
                }
                else if (Session["Upload_Kind"].ToString() == "Goods")//設備
                {
                    //DataTable dt1 = clsData.UploadApparatusLastIDQuery();

                    //clsParameter.strApparatusID = dt1.Rows[0]["ID"].ToString();
                    //if (clsParameter.strApparatusID == "")
                    //    clsParameter.strApparatusID = "1";
                    //else
                    //    clsParameter.strApparatusID = (Int32.Parse(clsParameter.strApparatusID) +1).ToString();
                    //string strApparatusID_Cookie;
                    //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
                    //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

                    Upload_Directory = @"d:\Goods\" + Session["GoodsID"].ToString() + @"\";
                }
                else if (Session["Upload_Kind"].ToString() == "Sample")//設備
                {
                    //DataTable dt1 = clsData.UploadApparatusLastIDQuery();

                    //clsParameter.strApparatusID = dt1.Rows[0]["ID"].ToString();
                    //if (clsParameter.strApparatusID == "")
                    //    clsParameter.strApparatusID = "1";
                    //else
                    //    clsParameter.strApparatusID = (Int32.Parse(clsParameter.strApparatusID) +1).ToString();
                    //string strApparatusID_Cookie;
                    //HttpCookie cookie_ApparatusID = Request.Cookies["ApparatusID"];
                    //strApparatusID_Cookie = Server.UrlDecode(cookie_ApparatusID.Value);

                    Upload_Directory = @"d:\Sample\" + Session["SampleID"].ToString() + @"\";
                }                                      
                else if (Session["Upload_Kind"].ToString() == "Lab")//任務檔案上傳
                {
                    //string strProjectName_Cookie, strCaseName_Cookie, strUpload_Project_Kind_Cookie;
                    //HttpCookie cookie_ProjectName = Request.Cookies["ProjectName"];
                    //strProjectName_Cookie = Server.UrlDecode(cookie_ProjectName.Value);

                    //HttpCookie cookie_CaseName = Request.Cookies["CaseName"];
                    //strCaseName_Cookie = Server.UrlDecode(cookie_CaseName.Value);

                    //HttpCookie cookie_Upload_Project_Kind = Request.Cookies["Upload_Project_Kind"];
                    //strUpload_Project_Kind_Cookie = Server.UrlDecode(cookie_Upload_Project_Kind.Value);

                    //Upload_Directory = @"d:\專案管理\" + strProjectName_Cookie + @"\" + strCaseName_Cookie + @"\";
                    Upload_Directory = @"d:\" + Session["Upload_Project_Kind"].ToString() + @"\" + Session["ProjectName"].ToString() + @"\" + Session["CaseName"].ToString() + @"\";
                }
                else if (Session["Upload_Kind"].ToString() == "ProjectInfo")//ProjectInfo上傳
                {
                    Upload_Directory = @"d:\驗証申請\" + Session["ProjectName"].ToString() + @"\";
                }                    
                else if (Session["Upload_Kind"].ToString() == "Application_TestCase")//申請單TestCase說明檔案(AddInfo)
                {
                    if ((Session["Application_K"] == null) || (Session["Application_F"] == null) || (Session["Application_I"] == null))
                    {
                        Response.StatusCode = 404;
                        Response.StatusDescription = "Not Found";
                        Response.End();
                        return;
                    }
                    else 
                        Upload_Directory = @"d:\Application_TestCase\" + Session["Application_K"].ToString() + @"\" + Session["Application_F"].ToString() + @"\" + Session["Application_I"].ToString() + @"\";
                        
                }
                else if (Session["Upload_Kind"].ToString() == "Application_TestCase1")//申請單TestCase說明檔案(AddInfo)
                {
                    if ((Session["Application_K1"] == null) || (Session["Application_F1"] == null) || (Session["Application_I1"] == null))
                    {
                        Response.StatusCode = 404;
                        Response.StatusDescription = "Not Found";
                        Response.End();
                        return;
                    }
                    else
                        Upload_Directory = @"d:\Application_TestCase\" + Session["Application_K1"].ToString() + @"\" + Session["Application_F1"].ToString() + @"\" + Session["Application_I1"].ToString() + @"\";

                }
                else if (Session["Upload_Kind"].ToString() == "Explanation")//申請辦法說明檔案(AddInfo)
                {
                    if ((Session["Explanation_K"] == null) || (Session["Explanation_i"] == null))
                    {
                        Response.StatusCode = 404;
                        Response.StatusDescription = "Not Found";
                        Response.End();
                        return;
                    }
                    else
                        Upload_Directory = @"d:\Explanation\" + Session["Explanation_K"].ToString() + @"\" + Session["Explanation_I"].ToString() + @"\";
                }
                else if (Session["Upload_Kind"].ToString() == "Reservation")//預約設備
                {
                    //if ((Session["Explanation_K"] == null) || (Session["Explanation_i"] == null))
                    //{
                    //    Response.StatusCode = 404;
                    //    Response.StatusDescription = "Not Found";
                    //    Response.End();
                    //    return;
                    //}
                    //else
                    Upload_Directory = @"d:\Reservation\";
                }
                else//申請單
                {
                    //HttpCookie cookie_ApplicationID = Request.Cookies["ApplicationID"];
                    //strApplicationID = Server.UrlDecode(cookie_ApplicationID.Value);
                    Upload_Directory = @"d:\Application\" + Session["ApplicationID"].ToString() + @"\";
                    //Upload_Directory = @"d:\Application\" + clsParameter.strApplicationID + @"\";
                }
                
                if (!Directory.Exists(Upload_Directory))  // 若目錄不存在則建立之
                {
                    Directory.CreateDirectory(Upload_Directory);
                }

                /////
                string FileName = String.Format(@"{0}{1}", Upload_Directory, Path.GetFileName(File.FileName));
                string ErrorMessage = String.Empty;
                //if (System.IO.File.Exists(FileName) == false)  // 檔名重複時的處理，增加序號 _yyyyMMddHHmmss.fff
                //{
                //    //System.IO.File.Delete(FileName);
                //    //FileName = String.Format(@"{0}{1}_{2:yyyyMMddHHmmss.fff}{3}", Upload_Directory, Path.GetFileNameWithoutExtension(FileName), DateTime.Now, Path.GetExtension(FileName));
                //}
                //File.SaveAs(FileName);  // 將檔案寫入 Server 端
                //FileResponseList.Add(CreateFileResponse(FileName, File.ContentLength, ErrorMessage));
                //Session["FileN"] = Session["FileN"] +FileName + ",";
                if (System.IO.File.Exists(FileName))  // 檔名重複時的處理，增加序號 _yyyyMMddHHmmss.fff
                {
                    System.IO.File.Delete(FileName);


                    //FileName = String.Format(@"{0}{1}_{2:yyyyMMddHHmmss.fff}{3}", Upload_Directory, Path.GetFileNameWithoutExtension(FileName), DateTime.Now, Path.GetExtension(FileName));


                }
                File.SaveAs(FileName);  // 將檔案寫入 Server 端
                FileResponseList.Add(CreateFileResponse(FileName, File.ContentLength, ErrorMessage));
                Session["FileN"] = Session["FileN"] + FileName + ",";

            }
            SerializeUploaderResponse(FileResponseList);
            
        }  // EOF --- if (Request.HttpMethod.ToUpper() == "POST" && Request.QueryString["file"] == null)
        // 刪除檔案之片段  ---------------------------------------
		else if (Request.HttpMethod.ToUpper() == "POST" && Request.QueryString["file"] != null)   
        {			   
            bool SuccessfullyDeleted = true;          
            try
            {                File.Delete(FullFileName);            }
            catch
            {                SuccessfullyDeleted = false;            }
            Response.Write(String.Format("{{\"{0}\":{1}}}", ShortFileName, SuccessfullyDeleted.ToString().ToLower()));
            String strFileName;
            strFileName = Session["FileN"].ToString();
            strFileName = strFileName.Replace(FullFileName, "");
            Session["FileN"] = strFileName;
        }
        else // 不是 GET 也不是 POST 也不是做 DELETE
        {
            Response.StatusCode = 405;
            Response.StatusDescription = "Method not allowed";
            Response.End();
            return;
        }
        Response.End();
    }
</script>