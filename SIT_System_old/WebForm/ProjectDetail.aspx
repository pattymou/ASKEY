<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProjectDetail.aspx.cs" Inherits="WebForm_ProjectDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <link rel="stylesheet" href="//apps.bdimg.com/libs/jqueryui/1.10.4/css/jquery-ui.min.css">
  <script src="//apps.bdimg.com/libs/jquery/1.10.2/jquery.min.js"></script>
  <script src="//apps.bdimg.com/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>--%>
    <link rel="stylesheet" href="../css/jquery.dataTables.css">

    <script src="../js/jquery_1.11.0.min.js"></script>

    <script src="../js/jquery.dataTables.min.js"></script>

    <style type="text/css" class="init">
        th, td
        {
            white-space: nowrap;
        }
        div.dataTables_wrapper
        {
            width: 1250px;
            margin: 0 auto;
        }
    </style>

    <script>


        $(document).ready(function() {
            $('#example').dataTable({
                "scrollX": true,
                "ajax": '../ajax/data/arays_projectcase.txt'
            });

            $('#example tbody').on('click', 'tr', function() {
                var name = $('td', this).eq(0).text();
                //              var url = "https://tw.yahoo.com";
                //	              var url = "http://localhost/SIT_System/WebForm/ProjectCase.aspx?Value=" + name + "&ID=" + id;
                if (name != "No data available in table") {
                    var url = "ProjectCase.aspx?Value=" + escape(name);
                    //              window.open(url);
                    location.href = (url);
                }
            });
        });        
    </script>

    <%--<script>
      $(function() {
          $("#tabs").tabs();
      });

      $(document).ready(function() {
          $('#example').dataTable({
              "scrollX": true,
              "ajax": '../ajax/data/arays_projectcase.txt'
          });

          $('#example tbody').on('click', 'tr', function() {
              var name = $('td', this).eq(0).text();
              //              var url = "https://tw.yahoo.com";
              var url = "http://localhost/SIT_System/WebForm/AddUser.aspx?ID=" + name;
              //              window.open(url);
              location.href = (url);
          });
      });
  </script>--%>
    <style>
        table.one
        {
            table-layout: automatic;
        }
    </style>
    <fieldset>
        <%--<br />
 <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增專案]</asp:LinkButton> 
 &nbsp;&nbsp;&nbsp;&nbsp;
 <asp:LinkButton ID="lblModify" runat="server" OnClick="lbtnAdd_Click">[修改此專案]</asp:LinkButton> 
 
 <div align ="right">
 <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnAdd_Click">[刪除此專案]</asp:LinkButton>
 </div>
 
 <br />
 <br />--%>
        <table id="Table1" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增任務]</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lblModify" runat="server" OnClick="lbtnModify_Click">[修改此專案]</asp:LinkButton>
                </td>
                <td align="right">
                    <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnDel_Click">[刪除此專案]</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblLocation" runat="server" ForeColor="#3333FF" Font-Bold="True">&nbsp;&nbsp;&nbsp;</asp:Label><asp:Label
            ID="lblID" runat="server" ForeColor="#3333FF" Font-Bold="True"></asp:Label>
        <table id="Table1" class="one" style="border: 1px solid" cellpadding="5" cellspacing="5"
            frame="border" rules="all" width="100%">
            <tr>
                <td width="25%">
                    <asp:Label ID="Label7" runat="server" Text="申請人"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblName" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="Label10" runat="server" Text="部門"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblDepartment" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="分機"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExt" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Mail"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMail" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="客戶"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCustomer" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="Label26" runat="server" Text="機種所屬Sub-PU"></asp:Label>
                </td>
                <td width="25%">
                    <asp:Label ID="lblDepartment2" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="PM Sales"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblPM" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="S/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="H/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Mechanical Engineer"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMechanical" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="DSP Model"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDSP" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="F/W Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFW" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Wireless Drive"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWireless" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Customer's Product Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProduct" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="NPI"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNPI" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="H/W Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHW_VR" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="Chipset"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblChipset" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="Sample MAC Address"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMAC" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="Utility Version"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUtility" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="開始日期"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStart" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="預計完成日"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblExpect" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="預計Sample Ready日期"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblReady" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="DQA負責人"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDQA" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="實驗室負責人"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEngineer" runat="server" ForeColor="#660066"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="進度"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProgress" runat="server" ForeColor="#660066"></asp:Label>
                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="知會人員"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblRelated" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Jira Link"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblJira" runat="server" ForeColor="#660066"></asp:Label>
                </td>
            </tr>
            <%--        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="TestCase"></asp:Label>
                
            </td>
            <td colspan=3>
                   
                    <asp:TextBox ID="txtTestCase" runat="server" MaxLength="500" Rows="5" 
                        TextMode="MultiLine" Width="579px"></asp:TextBox>
                   
            </td>
           
        </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="申請人備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNoteP" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="578px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label35" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="578px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="附加檔案"></asp:Label>
                </td>
                <td align="center" colspan="5" colspan="3">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="1"
                        ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="文件名稱" SortExpression="file_tag">
                                <ItemTemplate>
                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                        Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="seq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("File_Path") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table id="endT" class="one" width="100%" runat="server">
            <tr id="endT2" runat="server">
                <td>
                    <asp:Label ID="lblLeaderAppLication" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="endT1" runat="server">
                <td>
                    <asp:Label ID="lblLeaderAccepted" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="Tr2" runat="server">
                <td>
                    <asp:Label ID="lblTeamLeader" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="Tr3" runat="server">
                <td>
                    <asp:Label ID="lblEngineer1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="Tr4" runat="server">
                <td>
                    <asp:Label ID="lblEnd" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <table id="example" class="display" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th>
                        任務名稱
                    </th>
                    <th>
                        進行中任務總筆數
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <tr>
            <td align="center" colspan="2" style="color: red">
                <br />
                <br />
                <br />
                <asp:Button ID="butReturn" runat="server" Text="回上一頁" OnClick="butReturn_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="butExcel" runat="server" Text="匯出Excel" OnClick="butExcel_Click" />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <table>

            <script src="../js/jquery.fn.gantt.min.js" type="text/javascript"></script>

            <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
            <div class="gantt">
            </div>

            <script>
        $(function() {
            $(".gantt").gantt({
                source: <%= csSource %>,
                months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
                dow: ["日", "一", "二", "三", "四", "五", "六"],
                navigate: "scroll",
                scale: "days",
                maxScale: "months",
                minScale: "days",
                itemsPerPage: 10
            });
        });    
            </script>

        </table>
    </fieldset>
</asp:Content>
