<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProjectCaseStatistics.aspx.cs" Inherits="WebForm_ProjectCaseStatistics"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="Table2" class="one" width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkDate" runat="server" Text="日期區間：" />
                            <asp:Label ID="Label6" runat="server" Text="(西元)"></asp:Label>
                            <asp:TextBox ID="txtYearS" runat="server" Width="75px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Text="年"></asp:Label>
                            <asp:DropDownList ID="ddlMonthS" runat="server">
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label5" runat="server" Text="月～(西元)"></asp:Label>
                            <asp:TextBox ID="txtYearE" runat="server" Width="75px"></asp:TextBox>
                            <asp:Label ID="Label8" runat="server" Text="年"></asp:Label>
                            <asp:DropDownList ID="ddlMonthE" runat="server">
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" Text="月"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="＊若需要依照日期做搜尋請打勾，若不打勾則是所有的資料做搜尋" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="案件類別："></asp:Label>
                            <asp:DropDownList ID="ddlKind" runat="server">
                                <asp:ListItem>ALL</asp:ListItem>
                                <asp:ListItem>台北</asp:ListItem>
                                <asp:ListItem>吳江</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%--<tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="部門："></asp:Label>
                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="機種名稱："></asp:Label>
                    <asp:DropDownList ID="ddlProject" runat="server" >
                    </asp:DropDownList>
                </td>
            </tr>--%>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="butOK" runat="server" Text="搜尋" OnClick="butOK_Click" />
                        </td>
                    </tr>
                </table>
                <table id="Table1" class="one" width="100%">
                    <tr style="font-size: 9pt">
                        <td align="center" bgcolor="#dfe9f7" style="height: 27px">
                            <font face="新細明體" size="5">任務統計</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <%--<div id="div1" style="overflow: scroll; height: 500px;">--%>
                            <div id="div1">
                                <asp:GridView ID="gvwMain" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" Width="100%" OnPageIndexChanging="gvwMain_PageIndexChanging"
                                    OnPreRender="gvwMain_PreRender" AllowPaging="False">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="功能" ReadOnly="True" SortExpression="Name">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Item" HeaderText="項目" ReadOnly="True" SortExpression="Item">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="Total" HeaderText="任務總數量" ReadOnly="True" SortExpression="Total">
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                                        <asp:BoundField DataField="Total" HeaderText="案件送件量" ReadOnly="True" SortExpression="Total">
                                            <ControlStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlKind" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDFunction" EventName="Click" />

                </Triggers>--%>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
