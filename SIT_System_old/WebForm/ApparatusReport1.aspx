<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="ApparatusReport1.aspx.cs" Inherits="WebForm_ApparatusReport1" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 

    <link rel="stylesheet" href="../css/GridViewHeaderStyle.css">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="../js/GridViewScroll/gridviewScroll.min.js"></script>
    <link href="../css/superTables.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/superTables.js"></script>
    <script type="text/javascript" src="../js/jquery.superTable.js"></script>

    
    <style type="text/css">
    .altRow { background-color: #ddddff; }
    </style>
<%--    <script type="text/javascript">


        $(function() {
            var nWidth = screen.width; //取得使用者螢幕寬
            nWidth = nWidth - 60;


            var GridView1 = document.getElementById('<%=this.gvwMain1.ClientID %>');
            if (GridView1 != null) {
                $(GridView1).toSuperTable({ width: nWidth + "px", height: "300px", fixedCols: 7 })
                .find("tr:even").addClass("altRow");
            }

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

                    <asp:Label ID="Label6" runat="server" Text="(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearS" runat="server" Width="75px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="年"></asp:Label>

                    <asp:Label ID="Label2" runat="server" Text="～(西元)"></asp:Label>
                    <asp:TextBox ID="txtYearE" runat="server"　Width="75px"></asp:TextBox>
                    
                    <asp:Label ID="Label4" runat="server" Text="年"></asp:Label>
 
                    <asp:Label ID="lblCount" runat="server" Text="" Visible="False"></asp:Label>
                    
                       
                </td>
                
            </tr>
                       
            <tr>
                <td>    
                    
                    <asp:Label ID="Label9" runat="server" Text="搜尋條件："></asp:Label>
                </td>
            </tr>            
            <%--<tr>
                <td>
                    <asp:RadioButton ID="rdoCase" runat="server" GroupName="2" Text="統計設備使用比例" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoCase1" runat="server" GroupName="2" Text="統計案件比例" />
                </td>
            </tr>--%>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoProducts_ID" runat="server" GroupName="1" />
                    <asp:Label ID="Label8" runat="server" Text="財產編號："></asp:Label>
                    <asp:TextBox ID="txtProducts_ID" runat="server"　Width="75px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoKind1" runat="server" GroupName="1" />
                    <asp:Label ID="Label11" runat="server" Text="類別："></asp:Label>
                    
                    <asp:DropDownList ID="ddlKind1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlKind1_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


                    <asp:DropDownList ID="ddlApparatus" runat="server" >
                    </asp:DropDownList>
                    
                </td>
            </tr>
            
            
            
            
            <tr>
                <td>
                <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" onclick="btnSearch_Click" />

                </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="單位：小時" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr >
                <td>
                                          
                       <asp:GridView ID="gvwMain1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        OnRowCreated="gvwMain1_RowCreated" OnRowDataBound="gvwMain1_RowDataBound" OnPreRender ="gvwMain1_PreRender">                    
                            <Columns>

                                
                                <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" SortExpression="Department">
                                    <ControlStyle Width="100px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  
                                <asp:BoundField DataField="01" HeaderText="01" ReadOnly="True" SortExpression="01">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="02" HeaderText="02" ReadOnly="True" SortExpression="02">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="03" HeaderText="03" ReadOnly="True" SortExpression="03">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="04" HeaderText="04" ReadOnly="True" SortExpression="04">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="05" HeaderText="05" ReadOnly="True" SortExpression="05">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="06" HeaderText="06" ReadOnly="True" SortExpression="06">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="07" HeaderText="07" ReadOnly="True" SortExpression="07">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="08" HeaderText="08" ReadOnly="True" SortExpression="08">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="09" HeaderText="09" ReadOnly="True" SortExpression="09">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="10" HeaderText="10" ReadOnly="True" SortExpression="10">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="11" HeaderText="11" ReadOnly="True" SortExpression="11">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="12" HeaderText="12" ReadOnly="True" SortExpression="12">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Total">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                                                                                

                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />
 
                        </asp:GridView>
                        
                        
                        <%--<asp:GridView ID="gvwMain2" runat="server" Width="100%" 
                        AutoGenerateColumns="False" GridLines="None" 
                        >                    
                            <Columns>
                              
                                
                                <asp:BoundField DataField="GName" HeaderText="機種名稱" ReadOnly="True" SortExpression="GName">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="次數" ReadOnly="True" SortExpression="Total">
                                    <ControlStyle Width="120px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                                                                                                                                                                                                                                                                

                            </Columns>
                            <HeaderStyle CssClass="GridviewScrollHeader" /> 
                            <RowStyle CssClass="GridviewScrollItem" /> 
                            <PagerStyle CssClass="GridviewScrollPager" />
 
                        </asp:GridView>--%>
                       
                
                </td>
            </tr>
  
                    
                        
        </table>
        
        

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

