<%@ Page Title="" Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectMessage.aspx.cs" Inherits="WebForm_ProjectMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        #divEmployees
        {
            font-family:Arial, Verdana, Sans-Serif;
            font-size:12px;
            padding:10px;
            border:solid 1px #0066CC;
        }
        
        #divEmployees .detail
        {
            border-bottom:dashed 1px #0066CC;
            margin-bottom:10px;
            padding:10px;
        }
    </style>


    <fieldset>
        <asp:Label ID="lblProjectName" runat="server" Font-Bold="True" 
            Font-Size="Large" ForeColor="#3333CC"></asp:Label>
        <br />
        <asp:LinkButton ID="lblAdd" runat="server" OnClick="lbtnAdd_Click">[新增訊息]</asp:LinkButton> 
        <br /> 
        <br /> 
        <asp:Label ID="Label1" runat="server" Text="類別："></asp:Label> 
          
        <asp:DropDownList ID="ddlKind" runat="server" 
            onselectedindexchanged="ddlKind_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>ALL</asp:ListItem>
            <asp:ListItem>Software</asp:ListItem>
            <asp:ListItem>Hardware</asp:ListItem>
            <asp:ListItem>其他</asp:ListItem>
                </asp:DropDownList>        
        
        <table id="Table5" class="one" width="100%">
            <tr>
                <td>    
                    <asp:Repeater ID="rptEmployees" runat="server">
                        <HeaderTemplate>
                            <div id="divEmployees">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="detail">
                                <div>姓名： <strong><%# Eval("MessageUser")%></strong>&nbsp;&nbsp;&nbsp;時間： <strong><%# Eval("MessageTime")%></strong>&nbsp;&nbsp;&nbsp;類別： <strong><%# Eval("Kind")%></strong></div>
<%--                               <div>名稱： <strong><%# Eval("MessageUser")%></strong></div>
                               <div>時間： <strong><%# Eval("MessageTime")%></strong></div>
                               <div>類別： <strong><%# Eval("Kind")%></strong></div>--%>
                               <div>內容： <strong><%# Eval("Message")%></strong></div>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div class="detail">
                                <div>姓名： <strong><%# Eval("MessageUser")%></strong>&nbsp;&nbsp;&nbsp;時間： <strong><%# Eval("MessageTime")%></strong>&nbsp;&nbsp;&nbsp;類別： <strong><%# Eval("Kind")%></strong></div>
                               <%--<div>名稱： <strong><%# Eval("MessageUser")%></strong></div>
                               <div>時間： <strong><%# Eval("MessageTime")%></strong></div>
                               <div>類別： <strong><%# Eval("Kind")%></strong></div>--%>
                               <div>內容： <strong><%# Eval("Message")%></strong></div>
                            </div>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>                
                </td>
            </tr>
            <tr>
                <td align=center >
                    
                    <asp:Button ID="btnReturn" runat="server" Text="上一頁" onclick="btnReturn_Click" />
                </td>
            </tr>
        </table> 
    </fieldset>     
</asp:Content>

