<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rezervacija.aspx.cs" Inherits="RestBiz.Rezervacija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Styles/jquery-ui.css" rel="stylesheet">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>

        $(function () {
            $("#date").datepicker();
        });
    </script>

    <div class="content-subhead">
        <h2>Rezervacija</h2>
    </div>

    <fieldset>
        <div class="pure-control-group">
            <label for="Name">Restoran</label>
            <asp:TextBox ID="Name" runat="server" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="pure-control-group">
            <label for="Date">Datum</label>
            <input type="text" id="date">
        </div>
        <div class="pure-control-group">
            <label for="Time">Trajanje</label>
            <span>
                <asp:TextBox ID="Time" runat="server" type="number" min="0" max="5"></asp:TextBox>
                <label style="margin-left: -120px;">sata</label>
            </span>
            
        </div>
    </fieldset>
    

</asp:Content>
