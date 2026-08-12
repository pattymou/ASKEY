<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="PRReport.aspx.cs" Inherits="WebForm_PRReport" Title="" %>

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
                    
                    <asp:Label ID="Label30" runat="server" Text="地點："></asp:Label>
                                        <asp:RadioButton ID="rdoLocal" runat="server" GroupName="1" Text="台北" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoLocal1" runat="server" GroupName="1" Text="吳江" />

                </td>
                
            </tr>        
            <tr>
                <td>
                    <asp:RadioButton ID="rdoReport1" runat="server" GroupName="2" Text="採購中總金額" />    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoReport2" runat="server" GroupName="2" Text="歷史記錄" />    
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />
                </td>
            </tr>
        </table> 
    </fieldset> 
</asp:Content>

