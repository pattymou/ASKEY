<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="PR_Detail.aspx.cs" Inherits="WebForm_PR_Detail" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">

    <script src="../js/jquery-1.10.2.min.js"></script>

    <script src="../js/jquery-1.10.4.min.js"></script>

    <style>
        /* Adjust the jQuery UI widget font-size: */.ui-widget
        {
            font-size: 0.95em;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindPicker);
            bindPicker();
        });
        function bindPicker() {
            $("input[type=text][id*=DateTimeValue]").datepicker();
            $("input[type=text][id*=DateTimeValue1]").datepicker();
        }
    </script>

    <fieldset>
        <table id="Table2" class="one" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增採購明細]</asp:LinkButton>
                </td>
                <td align="right">
                    <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnDel_Click">[刪除此採購]</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <font face="verdana" color="0000DD" size="4"><legend>採購明細資訊</legend></font>
        <table id="Table1" style="border: 1px solid" cellpadding="5" cellspacing="5" frame="border"
            rules="all" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="開立PR日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker1" name="date2" value="<%=strDate1%>">

                    <script>
                         $(function() {
                         $("#datepicker1").datepicker();
                     });
                    
                    </script>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="請購單號"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPR_No" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="採購類別"></asp:Label>
                
                </td>
                <td>                
                    <asp:DropDownList ID="ddlKind" runat="server">

                    </asp:DropDownList>
                
                </td>
            
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="預計交貨日期"></asp:Label>
                </td>
                <td>
                    <input type="text" id="datepicker" name="date1" value="<%=strDate%>">

                    <script>
                         $(function() {
                         $("#datepicker").datepicker();
                     });
                    
                    </script>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="簽呈編號"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSigned_ID" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="需求人"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDemand_Person" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Email"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="324px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="地點"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAcceptedTeam" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem>台北</asp:ListItem>
                        <asp:ListItem>吳江</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="狀態"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPRStatus" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="Open">Open</asp:ListItem>
                        <asp:ListItem Value="Hold">Hold</asp:ListItem>
                        <asp:ListItem Value="Close">Close</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label33" runat="server" Text="需求原因"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" TextMode="MultiLine"
                        Width="496px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color: red">
                    <br />
                    <br />
                    <asp:Button ID="butOK" runat="server" Text="更新" OnClick="butOK_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="butReturn" runat="server" Text="上一頁" OnClick="butReturn_Click" />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                BorderWidth="1px" CellPadding="3" DataKeyNames="Name" ForeColor="#333333" HorizontalAlign="Center"
                                Width="95%" OnRowDeleting="gvList_RowDeleting" OnPageIndexChanging="gvList_PageIndexChanging"
                                OnRowCancelingEdit="gvList_RowCancelingEdit" OnRowEditing="gvList_RowEditing"
                                OnRowUpdating="gvList_RowUpdating" OnRowDataBound="gvList_RowDataBound" OnRowCreated="gvList_RowCreated"
                                ShowFooter="True">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="貨品名稱" ReadOnly="True" SortExpression="Name">
                                        <ControlStyle Width="30px"></ControlStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                        <ControlStyle Width="30px"></ControlStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                        <ControlStyle Width="30px"></ControlStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="請購數量" SortExpression="Purchase_Quantity">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQuantity_E" runat="server" Width="100%" Text='<%#Eval("Purchase_Quantity") %>'
                                                OnTextChanged="txtQuantity_E_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Purchase_Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="單位" SortExpression="Unit">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit_E" runat="server" Width="100%" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="需求課別" SortExpression="Demand_Team">
                                        <EditItemTemplate>
                                            <%--<asp:TextBox ID="lblDemand_Team_E" runat="server" Width="100%" Text='<%#Eval("Demand_Team") %>'></asp:TextBox>--%>
                                            <%--<asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="True"  
                                        SelectedValue='<%# Bind("Demand_Team") %>'>
                                    </asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="True" SelectedValue='<%# Bind("Demand_Team") %>'>
                                                <%--<asp:ListItem Value="">select...</asp:ListItem>
                                        <asp:ListItem Value="Broadband Team">Broadband Team</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDemand_Team" runat="server" Text='<%#Eval("Demand_Team") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--                            <asp:TemplateField HeaderText="需求人" SortExpression="Demand_Person">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDemand_Person_E" runat="server" Width="100%" Text='<%#Eval("Demand_Person") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDemand_Person" runat="server" Text='<%#Eval("Demand_Person") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> --%>
                                    <asp:TemplateField HeaderText="採購窗口" SortExpression="Procurement_Staff">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtProcurement_Staff_E" runat="server" Width="100%" Text='<%#Eval("Procurement_Staff") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcurement_Staff" runat="server" Text='<%#Eval("Procurement_Staff") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="幣別" SortExpression="Currency">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCurrency_E" runat="server" Width="100%" Text='<%#Eval("Currency") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrency" runat="server" Text='<%#Eval("Currency") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="外幣匯率" SortExpression="ExchangeRate">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtExchangeRate_E" runat="server" Width="100%" Text='<%#Eval("ExchangeRate") %>'
                                                OnTextChanged="txtExchangeRate_E_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblExchangeRate" runat="server" Text='<%#Eval("ExchangeRate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="預估單價" SortExpression="Estimated_Price">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEstimated_Price_E" runat="server" Width="100%" Text='<%#Eval("Estimated_Price") %>'
                                                OnTextChanged="txtEstimated_Price_E_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstimated_Price" runat="server" Text='<%#Eval("Estimated_Price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="外幣總價" SortExpression="US_Price">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUS_Price_E" runat="server" Width="100%" Text='<%#Eval("US_Price") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUS_Price" runat="server" Text='<%#Eval("US_Price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="預估NTD總價" SortExpression="Estimated_TotalPrice">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEstimated_TotalPrice_E" runat="server" Width="100%" Text='<%#Eval("Estimated_TotalPrice") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstimated_TotalPrice" runat="server" Text='<%#Eval("Estimated_TotalPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--                            <asp:TemplateField HeaderText = "到貨日期">
                            <ItemTemplate>
                                <asp:TextBox ID="txtProductDate" runat="server" Text='<%# Eval("Arrival_Date","{0:yyyy/M/d}") %>' ReadOnly = "true"></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField> --%>
                                    <asp:TemplateField HeaderText="到貨日期" SortExpression="Arrival_Date">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="DateTimeValue" runat="server" Text='<%# Eval("Arrival_Date","{0:yyyy/M/d}") %>'
                                                Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrival_Date" runat="server" Text='<%# Eval("Arrival_Date","{0:yyyy/M/d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="驗收日期" SortExpression="Check_Date">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="DateTimeValue1" runat="server" Width="100%" Text='<%#Eval("Check_Date","{0:yyyy/M/d}") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCheck_Date" runat="server" Text='<%#Eval("Check_Date","{0:yyyy/M/d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="狀態" SortExpression="Status">
                                        <EditItemTemplate>
                                            <%--<asp:TextBox ID="txtStatus_E" runat="server" Width="100%" Text='<%#Eval("Status") %>'></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="True" SelectedValue='<%# Bind("Status") %>'>
                                                <asp:ListItem Value="Open">Open</asp:ListItem>
                                                <asp:ListItem Value="Hold">Hold</asp:ListItem>
                                                <asp:ListItem Value="Close">Close</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="備註" SortExpression="Note">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNote_E" runat="server" Width="100%" Text='<%#Eval("Note") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNote" runat="server" Text='<%#Eval("Note") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--                            <asp:TemplateField HeaderText="名稱" SortExpression="name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />                                
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="PR_ID" SortExpression="PR_ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPR_ID" runat="server" Text='<%# Bind("PR_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Goods_ID" SortExpression="Goods_ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGoods_ID" runat="server" Text='<%# Bind("Goods_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="修改" ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                ImageUrl="~/images/WebForm/icon-save.gif" Text="更新" ValidationGroup="ChgCodeList" />
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                ImageUrl="~/images/WebForm/icon-cancel.gif" Text="取消" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                                ImageUrl="~/images/WebForm/icon-edit.gif" Text="編輯" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/WebForm/icon-delete.gif" OnClientClick='return confirm("您確定要刪除此筆資料嗎？");'
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#FFFF99" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<font face="新細明體">
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="100%" 
                          AllowPaging="True" AllowSorting="True" 
                          OnPageIndexChanging="gvwMain_PageIndexChanging" 
                          OnRowCancelingEdit="gvwMain_RowCancelingEdit" 
                          OnRowEditing="gvwMain_RowEditing" OnRowUpdating="gvwMain_RowUpdating" 
                          OnSorting="gvwMain_Sorting" OnRowDeleting="gvwMain_RowDeleting">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                          
                <Columns>
                    <asp:TemplateField HeaderText="編輯">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <EditItemTemplate>
                            <asp:LinkButton ID="lcmdUpdate" runat="server" CommandName="Update">更新</asp:LinkButton>
                            <asp:LinkButton ID="lcmdCancel_E" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lcmdSave" runat="server" OnClick="lcmdSave_Click">儲存</asp:LinkButton>
                            <asp:LinkButton ID="lcmdCancel_F" runat="server" OnClick="lcmdCancel_F_Click">取消</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lcmdModify" runat="server" CommandName="Edit">修改</asp:LinkButton>
                            <asp:LinkButton ID="lcmdDelete" runat="server" CommandName="Delete">刪除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PR_ID" Visible="False">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />                        
                        <EditItemTemplate>
                            <asp:Label ID="lblPR_ID_E" runat="server" Text='<%#Eval("PR_ID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPR_ID" runat="server" Text='<%#Eval("PR_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="貨品名稱" ReadOnly="True" SortExpression="Name">
                        <ControlStyle Width="30px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                        <ControlStyle Width="30px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                        <ControlStyle Width="30px"></ControlStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>                                                          
                    <asp:TemplateField HeaderText="請購數量" SortExpression="Purchase_Quantity">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />                        
                        <EditItemTemplate>
                            <asp:TextBox ID="lblQuantity_E" runat="server" Width="100%" Text='<%#Eval("Purchase_Quantity") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblQuantity_F" runat="server" Width="100%" Text='<%#Eval("Purchase_Quantity") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Purchase_Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="單位" SortExpression="Unit">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblUnit_E" runat="server" Width="100%" Text='<%#Eval("Unit") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblUnit_F" runat="server" Width="100%" Text='<%#Eval("Unit") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="需求課別" SortExpression="Demand_Team">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblDemand_Team_E" runat="server" Width="100%" Text='<%#Eval("Demand_Team") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblDemand_Team_F" runat="server" Width="100%" Text='<%#Eval("Demand_Team") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDemand_Team" runat="server" Text='<%#Eval("Demand_Team") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="需求人" SortExpression="Demand_Person">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblDemand_Person_E" runat="server" Width="100%" Text='<%#Eval("Demand_Person") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblDemand_Person_F" runat="server" Width="100%" Text='<%#Eval("Demand_Person") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDemand_Person" runat="server" Text='<%#Eval("Demand_Person") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="採購窗口" SortExpression="Procurement_Staff">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblProcurement_Staff_E" runat="server" Width="100%" Text='<%#Eval("Procurement_Staff") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblProcurement_Staff_F" runat="server" Width="100%" Text='<%#Eval("Procurement_Staff") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProcurement_Staff" runat="server" Text='<%#Eval("Procurement_Staff") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="幣別" SortExpression="Currency">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblCurrency_E" runat="server" Width="100%" Text='<%#Eval("Currency") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblCurrency_F" runat="server" Width="100%" Text='<%#Eval("Currency") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCurrency" runat="server" Text='<%#Eval("Currency") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="預估單價" SortExpression="Estimated_Price">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblEstimated_Price_E" runat="server" Width="100%" Text='<%#Eval("Estimated_Price") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblEstimated_Price_F" runat="server" Width="100%" Text='<%#Eval("Estimated_Price") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEstimated_Price" runat="server" Text='<%#Eval("Estimated_Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="美金總價" SortExpression="US_Price">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblUS_Price_E" runat="server" Width="100%" Text='<%#Eval("US_Price") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblUS_Price_F" runat="server" Width="100%" Text='<%#Eval("US_Price") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUS_Price" runat="server" Text='<%#Eval("US_Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="預估NTD總價" SortExpression="Estimated_TotalPrice">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblEstimated_TotalPrice_E" runat="server" Width="100%" Text='<%#Eval("Estimated_TotalPrice") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblEstimated_TotalPrice_F" runat="server" Width="100%" Text='<%#Eval("Estimated_TotalPrice") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEstimated_TotalPrice" runat="server" Text='<%#Eval("Estimated_TotalPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="到貨日期" SortExpression="Arrival_Date">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblArrival_Date_E" runat="server" Width="100%" Text='<%#Eval("Arrival_Date") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblArrival_Date_F" runat="server" Width="100%" Text='<%#Eval("Arrival_Date") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblArrival_Date" runat="server" Text='<%#Eval("Arrival_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="驗收日期" SortExpression="Check_Date">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblCheck_Date_E" runat="server" Width="100%" Text='<%#Eval("Check_Date") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblCheck_Date_F" runat="server" Width="100%" Text='<%#Eval("Check_Date") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCheck_Date" runat="server" Text='<%#Eval("Check_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="狀態" SortExpression="Status">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblStatus_E" runat="server" Width="100%" Text='<%#Eval("Status") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblStatus_F" runat="server" Width="100%" Text='<%#Eval("Status") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="備註" SortExpression="Note">
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"/>
                        <ControlStyle Width="100px" />
                        <EditItemTemplate>
                            <asp:TextBox ID="lblNote_E" runat="server" Width="100%" Text='<%#Eval("Note") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="lblNote_F" runat="server" Width="100%" Text='<%#Eval("Note") %>'></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNote" runat="server" Text='<%#Eval("Note") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                                                                                                                                                                                                
                </Columns>
            </asp:GridView>--%>
                    &nbsp; </font>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
