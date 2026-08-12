<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="SearchApplication.aspx.cs" Inherits="WebForm_SearchApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/jquery-ui.min.css">

    <script src="../js/jquery-1.10.2.min.js"></script>

    <script src="../js/jquery-1.10.4.min.js"></script>

    <script>
        $(function() {
            $("#tabs").tabs();


        });
        $(window).load(function() {



        });   
    </script>

    <fieldset>
        <table id="Table1" class="one" width="100%">
            <tr>
                <%--                <td>
                    <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[建立申請單]</asp:LinkButton>
                </td>--%>
                <%--                <td align ="right">
                    <asp:LinkButton ID="lblDel" runat="server" OnClick="lbtnDel_Click">[刪除此專案]</asp:LinkButton>
                </td>--%>
            </tr>
        </table>
        <%--<font face="verdana"color="0000DD"size="4" ><legend>項目列表</legend></font>--%>
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">申請單修改</a></li>
                <li><a href="#tabs-2">申請單狀態</a></li>
            </ul>
            <div id="tabs-1">
                <table id="Table5" class="one" width="100%">
                    <%--<tr>
            <td>
                               
                <asp:Label ID="Label1" runat="server" Text="申請單編號："></asp:Label>
                               
                <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                               
                               
            </td>
        </tr>
        <tr>
            <td>
                               
                <asp:Label ID="Label2" runat="server" Text="申請者姓名："></asp:Label>
                               
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                               
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                               
            </td>
        </tr>--%>
                    <%--<tr style="font-size: 9pt">
            <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                    <font face="新細明體" size="2">申請單</font></td>
        </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="(只能修改、刪除暫存申請單)" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-size: 9pt">
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderWidth="1px" CellPadding="3" DataKeyNames="Name" ForeColor="#333333" HorizontalAlign="Center"
                                        Width="95%" OnRowDeleting="gvList_RowDeleting" OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvwList_RowDataBound"
                                        OnRowCancelingEdit="gvList_RowCancelingEdit" OnRowEditing="gvList_RowEditing"
                                        OnRowUpdating="gvList_RowUpdating">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="申請單編號" SortExpression="name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtID" runat="server" Text='<%# Bind("ID") %>' TextMode="SingleLine"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="專案名稱" SortExpression="name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Kind" SortExpression="name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtKind" runat="server" Text='<%# Bind("Kind") %>' TextMode="SingleLine"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKind" runat="server" Text='<%# Bind("Kind") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Wrap="False" />
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
                                            <%--<asp:TemplateField HeaderText="Name" SortExpression="Name" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>                                    
                        </asp:TemplateField>--%>
                                            <%--                        <asp:TemplateField HeaderText="名稱" SortExpression="name" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName1" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />                                
                        </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle BackColor="#EFF3FB" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs-2">
                <table id="Table2" class="one" width="100%">
                    <tr style="font-size: 9pt">
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvwMain" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderWidth="1px" CellPadding="3" DataKeyNames="Name" ForeColor="#333333" HorizontalAlign="Center"
                                        Width="95%" OnPageIndexChanging="gvwMain_PageIndexChanging">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <%--<asp:BoundField DataField="ID" HeaderText="申請單編號" ReadOnly="True" SortExpression="ID">
                                                <ControlStyle Width="30px"></ControlStyle>                                               
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="申請單編號" SortExpression="ID">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "ApplicationDefault.aspx?PID="+Eval("ID") %>'
                                                        Target="_blank" Text='<%# Bind("ID") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="專案名稱" ReadOnly="True" SortExpression="Name">
                                                <ControlStyle Width="30px"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="目前狀態" ReadOnly="True" SortExpression="Status">
                                                <ControlStyle Width="30px"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StartDate" HeaderText="開始日期" ReadOnly="True" SortExpression="StartDate">
                                                <ControlStyle Width="30px"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EndDate" HeaderText="預計完成日" ReadOnly="True" SortExpression="EndDate">
                                                <ControlStyle Width="30px"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <%-- <asp:TemplateField HeaderText="測試報告" SortExpression="file_tag">
                                                <ItemTemplate>
                                                    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "filedownload.aspx?guid="+Eval("File_Name")+"&path="+Eval("File_Path") %>'
                                                        Target="_blank" Text='<%# Eval("File_Name") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle BackColor="#EFF3FB" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
    </fieldset>
</asp:Content>
