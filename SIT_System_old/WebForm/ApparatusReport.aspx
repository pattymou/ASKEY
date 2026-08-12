<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApparatusReport.aspx.cs" Inherits="WebForm_ApparatusReport" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<link rel="stylesheet" href="../css/Calendar/jquery-ui.css">--%>
    <%--<script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/jquery-1.10.4.min.js"></script>--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 

    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="../js/GridViewScroll/gridviewScroll.min.js"></script>
    <link href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="../js/gridviewscroll.js"></script>--%>
    <%--<script type="text/javascript" src="../js/jquery-1.3.1.js"></script>--%>
    <script type="text/javascript" src="../js/superTables.js"></script>
    <script type="text/javascript" src="../js/jquery.superTable.js"></script>

    <%--<script type="text/javascript" src="../js/chart/chartapi.js"></script>--%>
<%--    <script type="text/javascript"> 
         $(document).ready(function () { 
            gridviewScroll(); 
     
            $(window).resize(function () 
            { 
                gridviewScroll(); 
            }); 
        });  
     
        function gridviewScroll() { 
            var GridView = document.getElementById('<%=this.gvwMain.ClientID %>');
            $(GridView).gridviewScroll({ 
                width: gridWidth, 
                height: gridHeight, 
//                width: 660, 
//                height: 200,
                freezesize: 0, 
                headerrowcount: 4 
            }); 
        } 
    </script>--%>
    

    
    <%--<script>
    window.onload=jf_init;
    function jf_init(){
    var nHeight = screen.height;//取得使用者螢幕高
    var nWidth = screen.width;//取得使用者螢幕寬

    nWidth = nWidth-60;
      var divTarget = document.getElementById("div1");
      divTarget.style.width = nWidth + "px";
      var divTarget1 = document.getElementById("div2");
      divTarget1.style.width = nWidth + "px";
    }
    </script>--%>
    
    <style type="text/css">
    .altRow { background-color: #ddddff; }
    </style>
    <script type="text/javascript">


        $(function() {
            var nWidth = screen.width; //取得使用者螢幕寬
            nWidth = nWidth - 60;

            var GridView = document.getElementById('<%=this.gvwMain.ClientID %>');
            if (GridView != null) {
                $(GridView).toSuperTable({ width: nWidth + "px", height: "300px", fixedCols: 7 })
                .find("tr:even").addClass("altRow");
            }

            var GridView1 = document.getElementById('<%=this.gvwMain1.ClientID %>');
            if (GridView1 != null) {
                $(GridView1).toSuperTable({ width: nWidth + "px", height: "300px", fixedCols: 4, headerRows: 2 })
                .find("tr:even").addClass("altRow");
            }

        });
    </script>
    <%--<style type="text/css">
    .GridviewScrollHeader TH, .GridviewScrollHeader TD 
{ 
    padding: 5px; 
    font-weight: bold; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #EFEFEF; 
    text-align: left; 
    vertical-align: bottom; 
} 
.GridviewScrollItem TD 
{ 
    padding: 5px; 
    white-space: nowrap; 
    border-right: 1px solid #AAAAAA; 
    border-bottom: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
} 
.GridviewScrollPager  
{ 
    border-top: 1px solid #AAAAAA; 
    background-color: #FFFFFF; 
} 
.GridviewScrollPager TD 
{ 
    padding-top: 3px; 
    font-size: 14px; 
    padding-left: 5px; 
    padding-right: 5px; 
} 
.GridviewScrollPager A 
{ 
    color: #666666; 
}
.GridviewScrollPager SPAN

{

    font-size: 16px;

    font-weight: bold;

}
</style> 
    <script type="text/javascript">  
    $(document).ready(function () { 
        gridviewScroll();  
    });  
  
    function gridviewScroll() {  
        var GridView1 = document.getElementById('<%=this.gvwMain1.ClientID %>');

        $(GridView1).gridviewScroll({  
            width: 660, 
            height: 300, 
            freezesize: 4, 
            arrowsize: 30,
            headerrowcount: 2 
            });  
        gridViewScroll.enhance();
    }  
    </script>--%>      
    
    

    
   <%--<style>
    .container {
    width: 100%;
    margin: 20px auto;
    border: 14px solid #ddd;
    position: relative;
        -webkit-border-radius: 6px;
        -moz-border-radius: 6px;
        border-radius: 6px;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;

    }
    
    .iframe {
    position:absolute;
    top:0;
    left:0;
    width:100%;
    height:100%;
    overflow:scroll;
    }
   </style>--%>
    
    
<%--    <script src="../js/ScrollableGridViewPlugin_ASP.NetAJAXmin.js"></script>    
 
    <script type="text/javascript">
        var radioObj = document.getElementById("<%=gvwMain.ClientID %>"); 
        alert(gvwMain.ClientID);  
        $(document).ready(function () {

            $(ctl00_ContentPlaceHolder1_gvwMain).Scrollable({
                ScrollHeight: 300,
                IsInUpdatePanel: true
            });
        });
    </script>--%>
    <fieldset>
        <table id="Table5" class="one" width="100%">
            <tr>
                <td colspan =2>
                    
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
                    <asp:Label ID="lblCount" runat="server" Text="" Visible="False"></asp:Label>
                    
                       
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoWeek" runat="server" GroupName="2" Text="單月" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<input type="text" id="datepicker" name = "date1" value = "<%=strStart%>">
                     <script>
                         $(function() {
                             $("#datepicker").datepicker();
                         });
                     </script>--%> 
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
                </td>
            </tr>            
            <tr>
                <td>    
                    
                    <asp:Label ID="Label9" runat="server" Text="搜尋條件："></asp:Label>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:RadioButton ID="rdoDepartment" runat="server" GroupName="1" />
                    <asp:Label ID="Label7" runat="server" Text="部門："></asp:Label>
                    
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    

                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoCustodian" runat="server" GroupName="3" />
                    <asp:Label ID="Label12" runat="server" Text="設備保管人："></asp:Label>
                    
                    <asp:DropDownList ID="ddlCustodian" runat="server">
                    </asp:DropDownList>                
                </td>
            </tr>
            <tr>
                <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoKind" runat="server" GroupName="3" />
                    <asp:Label ID="Label10" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoProducts_ID" runat="server" GroupName="1" />
                    <asp:Label ID="Label8" runat="server" Text="財產編號："></asp:Label>
                    <asp:TextBox ID="txtProducts_ID" runat="server"　Width="75px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoKind1" runat="server" GroupName="1" />
                    <asp:Label ID="Label11" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind1" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:RadioButton ID="rdoKind" runat="server" GroupName="1" />
                    <asp:Label ID="Label10" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>--%>
            
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />

                </td>
            </tr>
            
        </table> 
        <table>
            <tr >
                <td>
                    <%--<div id="div1" style="overflow:scroll;height:300px;" >--%>                  
                    <asp:GridView ID="gvwMain" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        OnRowCreated="gvwMain_RowCreated" OnRowDataBound="gvwMain_RowDataBound" OnPreRender ="gvwMain_PreRender">                    
                            <Columns>

                                <asp:BoundField DataField="Name" HeaderText="Equip" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Products_ID" HeaderText="Asset Number" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" SortExpression="Department">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PU" HeaderText="SubPU" ReadOnly="True" SortExpression="PU">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="Customer" HeaderText="Customer Code" ReadOnly="True" SortExpression="Customer">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="ModelName" HeaderText="Model Name" ReadOnly="True" SortExpression="ModelName">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Period" HeaderText="Period" ReadOnly="True" SortExpression="Period">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="D1" HeaderText="1" ReadOnly="True" SortExpression="D1">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D2" HeaderText="2" ReadOnly="True" SortExpression="D2">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D3" HeaderText="3" ReadOnly="True" SortExpression="D3">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D4" HeaderText="4" ReadOnly="True" SortExpression="D4">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D5" HeaderText="5" ReadOnly="True" SortExpression="D5">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="D6" HeaderText="6" ReadOnly="True" SortExpression="D6">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D7" HeaderText="7" ReadOnly="True" SortExpression="D7">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D8" HeaderText="8" ReadOnly="True" SortExpression="D8">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D9" HeaderText="9" ReadOnly="True" SortExpression="D9">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D10" HeaderText="10" ReadOnly="True" SortExpression="D10">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D11" HeaderText="11" ReadOnly="True" SortExpression="D11">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D12" HeaderText="12" ReadOnly="True" SortExpression="D12">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D13" HeaderText="13" ReadOnly="True" SortExpression="D13">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D14" HeaderText="14" ReadOnly="True" SortExpression="D14">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D15" HeaderText="15" ReadOnly="True" SortExpression="D15">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D16" HeaderText="16" ReadOnly="True" SortExpression="D16">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D17" HeaderText="17" ReadOnly="True" SortExpression="D17">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D18" HeaderText="18" ReadOnly="True" SortExpression="D18">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D19" HeaderText="19" ReadOnly="True" SortExpression="D19">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D20" HeaderText="20" ReadOnly="True" SortExpression="D20">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D21" HeaderText="21" ReadOnly="True" SortExpression="D21">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D22" HeaderText="22" ReadOnly="True" SortExpression="D22">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D23" HeaderText="23" ReadOnly="True" SortExpression="D23">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="D24" HeaderText="24" ReadOnly="True" SortExpression="D24">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D25" HeaderText="25" ReadOnly="True" SortExpression="D25">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D26" HeaderText="26" ReadOnly="True" SortExpression="D26">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D27" HeaderText="27" ReadOnly="True" SortExpression="D27">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D28" HeaderText="28" ReadOnly="True" SortExpression="D28">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D29" HeaderText="29" ReadOnly="True" SortExpression="D29">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D30" HeaderText="30" ReadOnly="True" SortExpression="D30">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D31" HeaderText="31" ReadOnly="True" SortExpression="D31">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="Auto" HeaderText="Auto" ReadOnly="True" SortExpression="Auto">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Manual" HeaderText="Manual" ReadOnly="True" SortExpression="Manual">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                     
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                             

                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />

                        </asp:GridView>  
                        
                        <asp:GridView ID="gvwMain1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        OnRowCreated="gvwMain1_RowCreated" OnRowDataBound="gvwMain1_RowDataBound" OnPreRender ="gvwMain1_PreRender">                    
                            <Columns>

                                <asp:BoundField DataField="Name" HeaderText="" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="180px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Products_ID" HeaderText="" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="100px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="" ReadOnly="True" SortExpression="Department">
                                    <ControlStyle Width="100px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  
                                <asp:BoundField DataField="Period" HeaderText="" ReadOnly="True" SortExpression="Period">
                                    <ControlStyle Width="80px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="M1A" HeaderText="" ReadOnly="True" SortExpression="M1A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M1M" HeaderText="" ReadOnly="True" SortExpression="M1M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M2A" HeaderText="" ReadOnly="True" SortExpression="M2A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M2M" HeaderText="" ReadOnly="True" SortExpression="M2M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M3A" HeaderText="" ReadOnly="True" SortExpression="M3A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M3M" HeaderText="" ReadOnly="True" SortExpression="M3M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M4A" HeaderText="" ReadOnly="True" SortExpression="M4A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M4M" HeaderText="" ReadOnly="True" SortExpression="M4M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M5A" HeaderText="" ReadOnly="True" SortExpression="M5A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="M5M" HeaderText="" ReadOnly="True" SortExpression="M5M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M6A" HeaderText="" ReadOnly="True" SortExpression="M6A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M6M" HeaderText="" ReadOnly="True" SortExpression="M6M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M7A" HeaderText="" ReadOnly="True" SortExpression="M7A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M7M" HeaderText="" ReadOnly="True" SortExpression="M7M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M8A" HeaderText="" ReadOnly="True" SortExpression="M8A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M8M" HeaderText="" ReadOnly="True" SortExpression="M8M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M9A" HeaderText="" ReadOnly="True" SortExpression="M9A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M9M" HeaderText="" ReadOnly="True" SortExpression="M9M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M10A" HeaderText="" ReadOnly="True" SortExpression="M10A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M10M" HeaderText="" ReadOnly="True" SortExpression="M10M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M11A" HeaderText="" ReadOnly="True" SortExpression="M11A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M11M" HeaderText="" ReadOnly="True" SortExpression="M11M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M12A" HeaderText="" ReadOnly="True" SortExpression="M12A">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M12M" HeaderText="" ReadOnly="True" SortExpression="M12M">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Auto" HeaderText="" ReadOnly="True" SortExpression="Auto">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Manual" HeaderText="" ReadOnly="True" SortExpression="Manual">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="" ReadOnly="True" SortExpression="Total">
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                     
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                             

                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />
 
                        </asp:GridView>
                       
                        <%--</div>--%> 
                
                </td>
            </tr>
  
                    
                         
            <%--<tr>
                <td align =center>
                    
                    <asp:Button ID="btnExcel" runat="server" Text="匯出Excel" 
                        onclick="btnExcel_Click" />
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        
                </td>
            </tr>--%>
        </table>
        
        <%--<table>
            <tr >
                <td>
                    <div id="div2" style="overflow:scroll;height:300px;" runat ="server">                  
                    <asp:GridView ID="gvwMain1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        OnRowCreated="gvwMain1_RowCreated" OnRowDataBound="gvwMain1_RowDataBound" OnPreRender ="gvwMain1_PreRender">                    
                            <Columns>

                                <asp:BoundField DataField="Name" HeaderText="" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Products_ID" HeaderText="" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="" ReadOnly="True" SortExpression="Department">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  
                                <asp:BoundField DataField="Period" HeaderText="" ReadOnly="True" SortExpression="Period">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="M1A" HeaderText="" ReadOnly="True" SortExpression="M1A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M1M" HeaderText="" ReadOnly="True" SortExpression="M1M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M2A" HeaderText="" ReadOnly="True" SortExpression="M2A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M2M" HeaderText="" ReadOnly="True" SortExpression="M2M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M3A" HeaderText="" ReadOnly="True" SortExpression="M3A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M3M" HeaderText="" ReadOnly="True" SortExpression="M3M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M4A" HeaderText="" ReadOnly="True" SortExpression="M4A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M4M" HeaderText="" ReadOnly="True" SortExpression="M4M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M5A" HeaderText="" ReadOnly="True" SortExpression="M5A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="M5M" HeaderText="" ReadOnly="True" SortExpression="M5M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M6A" HeaderText="" ReadOnly="True" SortExpression="M6A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M6M" HeaderText="" ReadOnly="True" SortExpression="M6M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M7A" HeaderText="" ReadOnly="True" SortExpression="M7A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M7M" HeaderText="" ReadOnly="True" SortExpression="M7M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M8A" HeaderText="" ReadOnly="True" SortExpression="M8A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M8M" HeaderText="" ReadOnly="True" SortExpression="M8M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M9A" HeaderText="" ReadOnly="True" SortExpression="M9A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M9M" HeaderText="" ReadOnly="True" SortExpression="M9M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M10A" HeaderText="" ReadOnly="True" SortExpression="M10A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M10M" HeaderText="" ReadOnly="True" SortExpression="M10M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M11A" HeaderText="" ReadOnly="True" SortExpression="M11A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M11M" HeaderText="" ReadOnly="True" SortExpression="M11M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M12A" HeaderText="" ReadOnly="True" SortExpression="M12A">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M12M" HeaderText="" ReadOnly="True" SortExpression="M12M">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Auto" HeaderText="" ReadOnly="True" SortExpression="Auto">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Manual" HeaderText="" ReadOnly="True" SortExpression="Manual">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="" ReadOnly="True" SortExpression="Total">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                     
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                             

                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />

                        </asp:GridView>  
                       
                        </div> 
                
                </td>
            </tr>
        </table>--%>
        
        <%--<table>
            <tr >
                <td>
                    <div id="div2" style="overflow:scroll;height:300px;">                  
                    <asp:GridView ID="gvwMain1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        OnRowCreated="gvwMain1_RowCreated" OnRowDataBound="gvwMain1_RowDataBound" OnPreRender ="gvwMain1_PreRender">                    
                            <Columns>

                                <asp:BoundField DataField="Name" HeaderText="Equip" ReadOnly="True" SortExpression="Name">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Products_ID" HeaderText="Asset Number" ReadOnly="True" SortExpression="Products_ID">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" SortExpression="Department">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PU" HeaderText="SubPU" ReadOnly="True" SortExpression="PU">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="Customer" HeaderText="Customer Code" ReadOnly="True" SortExpression="Customer">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="ModelName" HeaderText="Model Name" ReadOnly="True" SortExpression="ModelName">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Period" HeaderText="Period" ReadOnly="True" SortExpression="Period">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="M1" HeaderText="1" ReadOnly="True" SortExpression="M1">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M2" HeaderText="2" ReadOnly="True" SortExpression="M2">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M3" HeaderText="3" ReadOnly="True" SortExpression="M3">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M4" HeaderText="4" ReadOnly="True" SortExpression="M4">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M5" HeaderText="5" ReadOnly="True" SortExpression="M5">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="M6" HeaderText="6" ReadOnly="True" SortExpression="M6">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M7" HeaderText="7" ReadOnly="True" SortExpression="M7">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M8" HeaderText="8" ReadOnly="True" SortExpression="M8">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M9" HeaderText="9" ReadOnly="True" SortExpression="M9">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M10" HeaderText="10" ReadOnly="True" SortExpression="M10">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M11" HeaderText="11" ReadOnly="True" SortExpression="M11">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="M12" HeaderText="12" ReadOnly="True" SortExpression="M12">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Auto" HeaderText="Auto" ReadOnly="True" SortExpression="Auto">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>   
                                <asp:BoundField DataField="Manual" HeaderText="Manual" ReadOnly="True" SortExpression="Manual">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                    <ControlStyle Width="30px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                     
                                <asp:TemplateField HeaderText="seq" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVSeq" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                             

                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />

                        </asp:GridView>  
                       
                        </div> 
                
                </td>
            </tr>
  
                    
                         
        </table>--%>
        <%--<table>
            <tr>
                <td>
                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>  
                    <div id="piechart_3d" style="width: 900px; height: 500px;">  
                    </div>                    
                </td>
            </tr>
        </table>--%>

        <asp:table ID="PieChart" runat ="server">                   
                
            
        </asp:table>
        
        <asp:table ID="ColumnChart" runat ="server">                   
                
            
        </asp:table>
        
    <%--<script type='text/javascript'>  
                                    google.load("visualization", "1", { packages: ["corechart"] });
                                        
                                                </script>  
                                                 
                                                <script type='text/javascript'>  
                                                 
                                                function drawChart() {  
                                                var data = google.visualization.arrayToDataTable([  
                                                ['Kind', 'Auto', 'Manual', 'Idle'],['373939APPLE TV',0,2,54],['392497AP無線基地台',2,2,52],['392804VDSL2數據機',6,5,45]]);var options = {
                                                    title: 'Country wise Order Distribution',
                                                    width: 600,
                                                    height: 400,
                                                    legend: { position: 'top', maxLines: 3 },
                                                    bar: { groupWidth: '75%' },
                                                    isStacked: true
                                                };   var chart = new google.visualization.ColumnChart(document.getElementById('columnchart'));          
                                                chart.draw(data, options);        
                                                }    
                                            google.setOnLoadCallback(drawChart);  
                                             </script>    
        
    <div id="columnchart" style="width: 900px; height: 500px;">
    </div>--%>
    </fieldset>
</asp:Content>

