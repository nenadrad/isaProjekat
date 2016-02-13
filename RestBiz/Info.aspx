<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="RestBiz.Info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="A layout example with a side menu that hides on mobile, just like the Pure website.">
    <link rel="stylesheet" href="http://yui.yahooapis.com/pure/0.6.0/pure-min.css">
    <link rel="stylesheet" href="Styles/side-menu.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.css" />
</head>
<body>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.js"></script>

    <form id="form1" runat="server">
    <div>
        <div id ="main">
            <div class="content-subhead">
            </div>
            <div class="content">
                <p>
                    <asp:Label ID="Message" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    <a href ="Login.aspx" class ="pure-button pure-button-primary">Prijava na sistem</a>
                </p>
            </div>
        </div>
    
    </div>
    </form>
</body>
</html>
