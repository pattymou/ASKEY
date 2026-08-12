<%@ Page Language="C#" MasterPageFile="~/WebForm/MasterPage.master" AutoEventWireup="true" CodeFile="AddNumber.aspx.cs" Inherits="WebForm_AddNumber" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../js/jquery_1.11.0.min.js"></script>
    
    <script type="text/javascript">
        jQuery(document).ready(init);
        function init() {

           /*每次Dom載入完，確保圖片都不一樣*/
           jQuery("img[name='imgCode']").attr("src", "../ajax/ValidateNumber.ashx?" + Math.random());
        
        }

        function isPassValidateCode() {
          var  nowValidateNumber =  jQuery.ajax({
                url: "../ajax/readSessionValidateNumber.ashx",
                type: "post",
                async: false,
                data:{},
                success: function (htmlVal) {  }
            }).responseText;

            if (nowValidateNumber == "" || nowValidateNumber == null) {
                alert("驗證碼逾時，請重新整理");
                return false;
            }
            var userInput = jQuery("#<%= txt_input.ClientID%>").val();

            var validateResult = ((nowValidateNumber == userInput) ? true : false);


            if (validateResult == false) {
                jQuery("#span_result").html("驗證碼輸入不正確");
            }
            else
                jQuery("#span_result").html("驗證碼輸入成功");            

            //回傳true Or false
            return validateResult;
        }
    </script>
    
        <!--驗證結果訊息為了美觀，多追加以下的css-->
    <style type="text/css">
    #span_result
    {
     color:Red;
     font-size:12px;      
     }
    </style>
    
    <fieldset>
        <table id="Table1" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td colspan=2>
                    <asp:Label ID="Label1" runat="server" Text="(*為必填項目)" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="工號："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="AD帳號："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                    <asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="姓名："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="分機："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExt" runat="server"></asp:TextBox>
                    <asp:Label ID="Label14" runat="server" Text="*" ForeColor="Red"></asp:Label>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="ASKEY Mail："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                    <asp:Label ID="Label15" runat="server" Text="*" ForeColor="Red"></asp:Label>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="部門："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label16" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="門禁卡號："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCard" runat="server"></asp:TextBox>
             
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="密碼："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="Label17" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="確認密碼："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Label ID="Label18" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            
            
        </table>
        <table id="Table2" class="one" style="border: 1px solid " cellpadding="5" cellspacing="5" frame="border" rules="all" width="100%">
            <tr>
                <td colspan = 2 align ="center">

                     <!--src連結到ValidateNumber.ashx即可-->
                 <%--<img src="http://10.1.7.121/SIT_System_patty/ajax/ValidateNumber.ashx" alt="驗證碼" name="imgCode" /> --%>
                 <img src="../ajax/ValidateNumber.ashx" alt="驗證碼" name="imgCode" /> 
                 <input type="button" onclick="imgCode.src='../ajax/ValidateNumber.ashx?' + Math.random();" value="重新整理" />
                 <hr />


                 <!--前端驗證結果訊息要放到span_result的innerHtml-->
                 <asp:TextBox ID="txt_input" runat="server" /><span id="span_result"></span>
                 <asp:Button Text="送出" ID="btn_submit" runat="server" 
                         OnClientClick="return isPassValidateCode();" onclick="btn_submit_Click" />
                  
                 </td>
            </tr>
            <tr>
                <td align ="center" colspan = 2>

                    <br />
                    <br />
                        <asp:Button ID="btnOK" runat="server" Text="確定" onclick="btnOK_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="butReturn" runat="server" Text="回登入畫面" 
                                onclick="butReturn_Click" /> 
                         
                    <br />
                </td>
            </tr>
        </table> 
    </fieldset> 
</asp:Content>

