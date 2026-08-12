<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModifyDashBoard.aspx.cs" Inherits="WebForm_ModifyDashBoard" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">

    <script src="../js/jquery-1.10.2.min.js"></script>

    <script src="../js/jquery-1.10.4.min.js"></script>

    <fieldset>
        <font face="verdana" color="0000DD" size="6"><legend>編輯專案</legend></font>
        <br />
        <table id="Table1" class="one" style="border: 1px solid" cellpadding="5" cellspacing="5"
            frame="border" rules="all" width="100%">
            <tr>
                <%--            <td>
                    <asp:Label ID="Label5" runat="server" Text="預計完成日"></asp:Label>
                </td>
                <td>
                        <input type="text" id="datepicker1" name = "date2" value = "<%=strEnd%>">
                         <script>
                             $(function() {
                                 $("#datepicker1").datepicker();
                             });
                         </script>            
                </td>--%>
                <td colspan="4">
                    <asp:Label ID="lblID" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="專案類別"></asp:Label>
                </td>
                <td colspan="3">
                    <%--                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>--%>
                    <asp:Label ID="lblKind" runat="server" Text=""></asp:Label>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="Label26" runat="server" Text="Team"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTeam" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="指派工程師"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAssign" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="客戶"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustomer" runat="server">
                    </asp:DropDownList>
                </td>
                 <td>
                    <asp:Label ID="Label55" runat="server" Text="申請部門"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment2" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label36" runat="server" Text="PM/Sales"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPM" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="S/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSW" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td>
                    <asp:Label ID="Label37" runat="server" Text="H/W Engineer"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHW" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Mechanical Engineer"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtMechanical" runat="server"></asp:TextBox>
                </td>
                <%--                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="部門"></asp:Label>
                    
                </td>--%>
                <%--                <td>
                                        
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="F/W Version"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFW" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Wireless Drive"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWireless" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Customer's Product Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="NPI"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNPI" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="H/W Version"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtH_Version" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Chipset"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtChipset" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="Sample MAC Address"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMAC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="Utility Version"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUtility" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label23" runat="server" Text="DSP Model"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDSP" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="進度"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgress" runat="server">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                        <asp:ListItem>60</asp:ListItem>
                        <asp:ListItem>65</asp:ListItem>
                        <asp:ListItem>70</asp:ListItem>
                        <asp:ListItem>75</asp:ListItem>
                        <asp:ListItem>80</asp:ListItem>
                        <asp:ListItem>85</asp:ListItem>
                        <asp:ListItem>90</asp:ListItem>
                        <asp:ListItem>95</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="開始日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker" name="date1" value="<%=strStart%>">

                    <script>
                        $(function() {
                            $("#datepicker").datepicker();
                        });
                    </script>

                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="預計完成日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker1" name="date2" value="<%=strEnd%>">

                    <script>
                        $(function() {
                            $("#datepicker1").datepicker();
                        });
                    </script>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="預計Sample Ready日期"></asp:Label>
                </td>
                <td colspan="3">
                    <input type="text" id="datepicker2" name="date3" value="<%=strSample%>">

                    <script>
                        $(function() {
                            $("#datepicker2").datepicker();
                        });
                    </script>

                </td>
                <%--<td>
                    <asp:Label ID="Label26" runat="server" Text="預計完成日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="Text2" name = "date2" value = "<%=strEnd%>">
                     <script>
                         $(function() {
                             $("#datepicker1").datepicker();
                         });
                     </script>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="結果判定"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtResult" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlResult" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Pass</asp:ListItem>
                        <asp:ListItem>Fail</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="狀態"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Close</asp:ListItem>
                        <asp:ListItem>Hold</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtExplain" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="496px"></asp:TextBox>
                </td>
            </tr>
            <%--==================--%>
            <tr>
                <%--                <td>
                    
                    <asp:Label ID="Label27" runat="server" Text="子任務名稱"></asp:Label>
                    
                </td>--%>
                <td colspan="4">
                    <asp:Label ID="lblCID" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="子任務名稱"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCaseName" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="子任務ID"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCaseID" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="Name1" runat="server">
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Sub PU"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label35" runat="server" Text="機種名稱"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtModelName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <%-- <td>
                    <asp:Label ID="Label28" runat="server" Text="指派工程師"></asp:Label>
                </td>
                <td>
                
                    <asp:DropDownList ID="ddlCAssign" runat="server">
                    </asp:DropDownList>
                
                </td>--%>
                <td>
                    <asp:Label ID="Label29" runat="server" Text="狀態"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCStatus" runat="server">
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Close</asp:ListItem>
                        <asp:ListItem>Hold</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <%--            <td>
                    <asp:Label ID="Label4" runat="server" Text="開始日期"></asp:Label>
                </td>
                <td>
                        <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                        
                         <script>
                             $(function() {
                             $("#datepicker").datepicker();
                         });
                        
                         </script>             
                </td>--%>
                <td>
                    <asp:Label ID="Label30" runat="server" Text="開始日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="Text1" name="date4" value="<%=strCStart%>">

                    <script>
                        $(function() {
                            $("#datepicker").datepicker();
                        });
                    </script>

                </td>
                <td>
                    <asp:Label ID="Label31" runat="server" Text="預計完成日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="Text2" name="date5" value="<%=strCEnd%>">

                    <script>
                        $(function() {
                            $("#datepicker1").datepicker();
                        });
                    </script>

                </td>
                <%--            <td>
                    <asp:Label ID="Label5" runat="server" Text="預計完成日"></asp:Label>
                </td>
                <td>
                        <input type="text" id="datepicker1" name = "date2" value = "<%=strEnd%>">
                         <script>
                             $(function() {
                                 $("#datepicker1").datepicker();
                             });
                         </script>            
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label32" runat="server" Text="結果判定"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCResult" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Pass</asp:ListItem>
                        <asp:ListItem>Fail</asp:ListItem>
                        <asp:ListItem>未完成</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label33" runat="server" Text="進度"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCProgress" runat="server">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                        <asp:ListItem>60</asp:ListItem>
                        <asp:ListItem>65</asp:ListItem>
                        <asp:ListItem>70</asp:ListItem>
                        <asp:ListItem>75</asp:ListItem>
                        <asp:ListItem>80</asp:ListItem>
                        <asp:ListItem>85</asp:ListItem>
                        <asp:ListItem>90</asp:ListItem>
                        <asp:ListItem>95</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label101" runat="server" Text="實驗室名稱"></asp:Label>
                </td>
                <td colspan =3>
                    <asp:TextBox ID="txtLab" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="報價金額"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQuoted" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="請款金額"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReimburse" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label34" runat="server" Text="備註"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="578px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="指派工程師"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="listLeft" runat="server" DataTextField="Name_En" Height="237px"
                        SelectionMode="Multiple" Width="321px"></asp:ListBox>
                </td>
                <td>
                    <asp:Button ID="btnRight" runat="server" Text=">" OnClick="btnRight_Click" Width="30px" /><br />
                    <br />
                    <asp:Button ID="btnLeft" runat="server" Text="<" OnClick="btnLeft_Click" Width="30px" />
                </td>
                <td>
                    <asp:ListBox ID="listRight" runat="server" Width="321" Height="237" SelectionMode="Multiple"
                        DataTextField="Name_En"></asp:ListBox>
                </td>
            </tr>
        </table>
        <tr>
            <td align="center" colspan="2" style="color: red">
                <br />
                <br />
                <asp:Button ID="butOK" runat="server" Text="確定" OnClick="butOK_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="butReturn" runat="server" Text="回上一頁" OnClick="butReturn_Click" />
                <br />
                <br />
            </td>
        </tr>
    </fieldset>
</asp:Content>
