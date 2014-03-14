<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Key2Code.aspx.cs" Inherits="ShowVerifyCode.Key2Code" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img alt="qqqq" id="qqq" src="VerifyCode.aspx?v=111" onclick="a()" />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Style="height: 21px" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
    <script type="text/javascript">
        function a() {
            document.getElementById("qqq").src = document.getElementById("qqq").src + "111";
        }
    </script>
</body>
</html>
