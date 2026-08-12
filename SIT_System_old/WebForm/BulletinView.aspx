<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="BulletinView.aspx.cs" Inherits="WebForm_BulletinView" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        window.onload=jf_init;
        function jf_init(){
        var nHeight = screen.height;//取得使用者螢幕高
        var nWidth = screen.width;//取得使用者螢幕寬

    //    if (nHeight > 600)
    //    {
        nWidth = nWidth-60;
        nHeight = nHeight-150;
     //   alert (nHeight);        
          var divTarget = document.getElementById("div1");
          divTarget.style.width = nWidth + "px";
          divTarget.style.height = nHeight + "px";
          
    //    }
        }
    </script>
    <table>
        <tr align ="center">
            <td>
                <asp:Label ID="Label1" runat="server" Text="最新公告" Font-Bold="True" 
                    Font-Size="XX-Large" ForeColor="#0033CC"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="div1" style="overflow:scroll">
                    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
                </div> 
            </td>
        </tr>
    </table>
</asp:Content>

