<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="StatisticsGoodsReport.aspx.cs" Inherits="WebForm_StatisticsGoodsReport" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../css/Calendar/jquery-ui.css">
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>
    <fieldset>
        <table id="Table5" class="one" width="100%">
                        
            <tr>
                <td>    
                    
                    <asp:Label ID="Label9" runat="server" Text="搜尋條件："></asp:Label>
                </td>
            </tr>            
            <%--<tr>
                <td>
                    <asp:RadioButton ID="rdoDepartment" runat="server" GroupName="1" />
                    <asp:Label ID="Label7" runat="server" Text="部門："></asp:Label>
                    
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>--%>
            <%--<tr>
                <td>
                    <asp:RadioButton ID="rdoProducts_ID" runat="server" GroupName="1" />
                    <asp:Label ID="Label8" runat="server" Text="貨品名稱："></asp:Label>
                    <asp:TextBox ID="txtProducts_ID" runat="server"　Width="75px"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <%--<asp:RadioButton ID="rdoKind" runat="server" GroupName="1" />--%>
                    <asp:Label ID="Label10" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoUse" runat="server" GroupName="1" Text="貨品使用狀態" />

                    
                </td>
            </tr>
            <tr>
                <td>    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="領用日期區間："></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoMonth" runat="server" GroupName="2" Text="月區間" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label6" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearS" runat="server" Width="75px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="年"></asp:Label>
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
                    <asp:Label ID="Label2" runat="server" Text="月～(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearE" runat="server"　Width="75px"></asp:TextBox>
                    
                    <asp:Label ID="Label4" runat="server" Text="年"></asp:Label>
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
                    <asp:Label ID="Label5" runat="server" Text="月"></asp:Label>     

                    
                       
                </td>
                
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoWeek" runat="server" GroupName="2" Text="週區間" />
                    &nbsp;&nbsp;&nbsp;
                    <input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                     <script>
                         $(function() {
                             $("#datepicker").datepicker();
                         });
                     </script>                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoStock" runat="server" GroupName="1" Text="貨品庫存狀態" />

                    
                </td>
            </tr>
            
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                </td>
            </tr>
            
        </table> 
    </fieldset>
    
</asp:Content>

