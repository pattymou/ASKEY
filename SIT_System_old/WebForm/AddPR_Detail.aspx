<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddPR_Detail.aspx.cs" Inherits="WebForm_AddPR_Detail" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">

  
    <style>
        /* Adjust the jQuery UI widget font-size: */
        .ui-widget {
            font-size: 0.95em;
    }
    </style>
    
    <fieldset>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode ="Conditional">
                <ContentTemplate>    
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="類別"></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">

                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                    
                    <asp:TextBox ID="txtSearch" runat="server" Width="323px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                    <asp:Label ID="Label18" runat="server" Text="(設備搜尋)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align ="center">
                                        
                    <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" AllowPaging="True" OnPageIndexChanging="gvwMain_PageIndexChanging" onRowCommand="gvwMain_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <%--<asp:ButtonField Text="DoubleClick" CommandName="DoubleClick" Visible="false" />--%>
                                <asp:BoundField DataField="Part_No" HeaderText="料號" ReadOnly="True" SortExpression="Part_No">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Kind" HeaderText="類型" ReadOnly="True" SortExpression="Kind">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="Name" HeaderText="貨品名稱" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="MF" HeaderText="廠商名稱" ReadOnly="True" SortExpression="MF">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                  
                                
                                <asp:TemplateField>
                                  <ItemTemplate>
                                    <asp:Button ID="btnSearch" runat="server" 
                                      CommandName="AddToCart" 
                                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                      Text="選取" />
                                  </ItemTemplate> 
                                </asp:TemplateField>
                                
                                
                                
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>           
                </td>
            </tr>            
        </table> 
        
        <br />
        <br />
        <table id="Table1" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
        <tr style="font-size: 9pt">
            <td colspan =4 align="center" bgcolor="#dfe9f7" style="height: 27px">
                <font face="新細明體" size="2">設備借用資訊</font></td>
        </tr>
        <tr>
            <td rowspan=2 valign=middle>
                <asp:Label ID="Label2" runat="server" Text="貨品資訊"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="貨品名稱："></asp:Label>
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>                
            </td>            
            <td >
                <asp:Label ID="Label3" runat="server" Text="廠商名稱："></asp:Label>
                <asp:Label ID="lblMF" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label5" runat="server" Text="料號："></asp:Label>
                <asp:Label ID="lblPart_No" runat="server" Text=""></asp:Label>                
            </td>
        </tr>
        
        <tr>
            <td rowspan=8 valign=middle>
                <asp:Label ID="Label7" runat="server" Text="採購明細"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="採購數量："></asp:Label>
                <asp:TextBox ID="txtPurchase_Quantity" runat="server"></asp:TextBox>
            </td>
            
        </tr>   
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="單位："></asp:Label>
                <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>            
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="採購窗口："></asp:Label>
                <asp:TextBox ID="txtProcurement_Staff" runat="server"></asp:TextBox>            
            </td>            
        </tr>  
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="需求課別："></asp:Label>
                
                <asp:DropDownList ID="ddlTeam" runat="server">
                </asp:DropDownList>          
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="需求人："></asp:Label>
                <asp:TextBox ID="txtDemand_Person" runat="server"></asp:TextBox>            
            </td>            
        </tr> 
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="幣別："></asp:Label>
                <asp:TextBox ID="txtCurrency" runat="server"></asp:TextBox>            
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="外幣匯率："></asp:Label>
                <asp:TextBox ID="txtExchangeRate" runat="server"></asp:TextBox>   
         
            </td>
            
        </tr>  
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="預估單價："></asp:Label>
                <asp:TextBox ID="txtEstimated_Price" runat="server" OnTextChanged ="txtEstimated_Price_TextChanged" AutoPostBack ="true"></asp:TextBox>            
            </td>        
            <td>
                <asp:Label ID="Label14" runat="server" Text="外幣總價："></asp:Label>
                <asp:TextBox ID="txtUS_Price" runat="server"></asp:TextBox>            
            </td>
            
        </tr>
        <tr>
            <td colspan =2>
                <asp:Label ID="Label15" runat="server" Text="預估NTD總價："></asp:Label>
                <asp:TextBox ID="txtEstimated_TotalPrice" runat="server"></asp:TextBox>
                <asp:Button ID="butConversion" runat="server" Text="換算" 
                            onclick="butConversion_Click" />                            
            </td>        
        </tr>  
         
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="實際到貨日："></asp:Label>
                <input type="text" id="datepicker" name = "date1" value = "<%=strArrival_Date%>">
                 <script>
                     $(function() {
                     Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args)
                     {
                     $("#datepicker").datepicker();
                     });
                     });
                     
                
                 </script>          
            </td>
            <td>
                <asp:Label ID="Label17" runat="server" Text="驗收日："></asp:Label>
                <input type="text" id="datepicker1" name = "date2" value = "<%=strCheck_Date%>">
                 <script>
                     $(function() {
                     Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args)
                     {
                     $("#datepicker1").datepicker();
                     });
                     });
                
                 </script>            
            </td>            
        </tr>     
        <tr>
            <td colspan =2>
                <asp:Label ID="Label19" runat="server" Text="備註："></asp:Label>            
                <asp:TextBox ID="txtNote" runat="server" MaxLength="500" Rows="5" Width="496px"></asp:TextBox>
            </td>
        </tr>                                  
        <tr>
            <td align ="center" colspan = 3 style="COLOR: red">
                <br />
                <br />
                    
                <asp:Button ID="butOK" runat="server" Text="確定" 
                            onclick="butOK_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;            
                <asp:Button ID="butReturn" runat="server" Text="上一頁" 
                            onclick="butReturn_Click" />                            
                            
                    
                <br />
                <br />
            </td>
        </tr>    
        </table>
        </ContentTemplate> 
        </asp:UpdatePanel> 
    </fieldset>
</asp:Content>

