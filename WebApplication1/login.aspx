<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication1.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

           
                Name:
                <input type="text" id="txtName" name="Name" value="" />
                <br />
               
                <br />
                <br />
            <asp:Button Text="Submit" runat="server" OnClick="Submit" />
            
        </div>
    </form>
</body>
</html>
