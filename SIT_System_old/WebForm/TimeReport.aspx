<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="TimeReport.aspx.cs" Inherits="WebForm_TimeReport"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <style>
        @import url('https://fonts.googleapis.com/css?family=Lato');
        @import url('http://fonts.googleapis.com/earlyaccess/cwtexyen.css');
        @import url('https://fonts.googleapis.com/css?family=Do+Hyeon');
        .col-Item
        {
            padding: 10px;
        }
        .report-list
        {
            box-shadow: 0px 1px 0px rgb(0, 0, 0,.05);
            border-radius: 1px;
            background: White;
            overflow: hidden;
        }
        .gridview-show
        {
            border: 1px solid #D4D4D4;
            font-size: 16px;
            font-family: 'Lato' , sans-serif;
            margin: 5px 0;
        }
        .btn
        {
            border: 0px solid;
            font-size: 20px;
            font-family: 'cwTeXYen' , sans-serif;
            color: #212121;
            background-color: #bababa;
            margin: 2px 0;
        }
        .btn:hover
        {
            background-color: #2B2B2B;
        }
        .SearchList
        {
            font-size: 22px;
            font-family: 'cwTeXYen' , sans-serif;
            padding: 0 10px;
        }
        th
        {
            text-align: center;
        }
        .ddl
        {
            font-size: 18px;
            font-family: 'Lato' , sans-serif;
        }
        .label
        {
            font-size: 20px;
            font-family: 'cwTeXYen' , sans-serif;
        }
        .label-1
        {
            font-size: 25px;
            font-family: 'cwTeXYen' , sans-serif;
            color: Red;
            text-align: center;
        }
    </style>
    <style type="text/css">
        table
        {
            table-layout: fixed;
            word-break: break-all;
        }
    </style>
    <table id="Table3" class="one" width="100%">
        <tr>
            <td colspan="2">
                <asp:Label ID="Label30" runat="server" Text="地點："></asp:Label>
                <asp:RadioButton ID="rdoLocal" runat="server" GroupName="6" Text="台北" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rdoLocal1" runat="server" GroupName="6" Text="吳江" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="日期區間："></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="(西元)"></asp:Label>
                <asp:TextBox ID="txtYearA" runat="server" Width="75px"></asp:TextBox>
                <asp:Label ID="Label14" runat="server" Text="年"></asp:Label>
                <asp:DropDownList ID="ddlMonthA" runat="server">
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
                <asp:Label ID="Label15" runat="server" Text="月"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="(例：選擇3月，日期區間為2/28~3/28)" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="butOK" runat="server" Text="搜尋" OnClick="butOK_Click" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <div id="div1" style="overflow: scroll; height: 500px; width: 100%;">
                    <asp:GridView ID="gvwMain" runat="server" class="gridview-show" AutoGenerateColumns="False"
                        ForeColor="#333333" OnRowDataBound="gvwMain_RowDataBound" OnRowCreated="gvwMain_RowCreated">
                        <Columns>
                            <asp:BoundField DataField="PU" HeaderText="Sub PU" ReadOnly="True" SortExpression="PU">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="550px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomerNumber" HeaderText="客戶代碼" ReadOnly="True" SortExpression="CustomerNumber">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="150px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer" HeaderText="客戶" ReadOnly="True" SortExpression="Customer">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="150px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" SortExpression="Model">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Detail" HeaderText="細項" ReadOnly="True" SortExpression="Detail">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="狀態" ReadOnly="True" SortExpression="Status">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Start_Date" HeaderText="開始日期" ReadOnly="True" SortExpression="Start_Date">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="End_Date" HeaderText="結束日期" ReadOnly="True" SortExpression="End_Date">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Progress_LastWeek" HeaderText="完成度%" ReadOnly="True" SortExpression="Progress_LastWeek">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="False" />
                            </asp:BoundField>    
                             <asp:BoundField DataField="Progress" HeaderText="完成度%" ReadOnly="True" SortExpression="Progress">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="False" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="Result" HeaderText="結果" ReadOnly="True" SortExpression="Result">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="100px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Employees" HeaderText="人員" ReadOnly="True" SortExpression="Employees">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="150px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Department" HeaderText="處級" ReadOnly="True" SortExpression="Department">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="250px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Team" HeaderText="部級" ReadOnly="True" SortExpression="Team">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" Width="250px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="5" HeaderText="5職等以下" ReadOnly="True" SortExpression="5">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="6" HeaderText="6職等" ReadOnly="True" SortExpression="6">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="7" HeaderText="7職等" ReadOnly="True" SortExpression="7">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="8" HeaderText="8職等" ReadOnly="True" SortExpression="8">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="9" HeaderText="9職等" ReadOnly="True" SortExpression="9">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="10" HeaderText="10~12職等" ReadOnly="True" SortExpression="10">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="總計" ReadOnly="True" SortExpression="Total">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" Width="200px" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle BackColor="#E8E8E8" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#595959" Font-Bold="True" ForeColor="white" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <table id="table1" runat="server" width="100%">
        <tr>
            <td>
                <div id="div2" style="overflow: scroll; height: 500px; width: 100%;">
                    <asp:GridView ID="gvwMain1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="PU" HeaderText="Sub PU" ReadOnly="True" SortExpression="PU">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomerNumber" HeaderText="客戶代碼" ReadOnly="True" SortExpression="CustomerNumber">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer" HeaderText="客戶" ReadOnly="True" SortExpression="Customer">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" SortExpression="Model">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Kind" HeaderText="類別" ReadOnly="True" SortExpression="Kind">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Detail" HeaderText="細項" ReadOnly="True" SortExpression="Detail">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="狀態" ReadOnly="True" SortExpression="Status">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Start_Date" HeaderText="開始日期" ReadOnly="True" SortExpression="Start_Date">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="End_Date" HeaderText="結束日期" ReadOnly="True" SortExpression="End_Date">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Progress" HeaderText="完成度%" ReadOnly="True" SortExpression="Progress">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left"/>
                            </asp:BoundField>  
                            <asp:BoundField DataField="Result" HeaderText="結果" ReadOnly="True" SortExpression="Result">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Employees" HeaderText="人員" ReadOnly="True" SortExpression="Employees">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Department" HeaderText="處級" ReadOnly="True" SortExpression="Department">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Team" HeaderText="部級" ReadOnly="True" SortExpression="Team">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="5" HeaderText="5職等以下" ReadOnly="True" SortExpression="5">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="6" HeaderText="6職等" ReadOnly="True" SortExpression="6">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="7" HeaderText="7職等" ReadOnly="True" SortExpression="7">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="8" HeaderText="8職等" ReadOnly="True" SortExpression="8">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="9" HeaderText="9職等" ReadOnly="True" SortExpression="9">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="10" HeaderText="10~12職等" ReadOnly="True" SortExpression="10">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="總計" ReadOnly="True" SortExpression="Total">
                                <HeaderStyle Wrap="False" />
                                <ControlStyle Width="30px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="GridviewScrollHeader" ForeColor="Black" />
                        <RowStyle CssClass="GridviewScrollItem" />
                        <PagerStyle CssClass="GridviewScrollPager" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <table id="Table2" class="one" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnExcel1" runat="server" Text="將表格匯出至Excel" OnClick="btnExcel1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
