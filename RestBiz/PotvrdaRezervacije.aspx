<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PotvrdaRezervacije.aspx.cs" Inherits="RestBiz.PotvrdaRezervacije" ClientIDMode="Static"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="Scripts/potvrdaRezervacije.js"></script>

    <script>

        $(function () {

            $("#rateYo").rateYo({
                normalFill: "#A0A0A0"
            });

        });

    </script>

    <div class="content-subhead">
        <h2>Potvrda rezervacije</h2>
    </div>

    <div id="FormDiv" runat="server">
        <fieldset>
            <div class="pure-control-group">
                <label for="Name">Restoran</label>
                <asp:TextBox ID="Name" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="pure-control-group">
                <label for="Date">Datum</label>
                <input type="text" id="Date" runat="server" readonly="readonly">
            </div>
            <div class="pure-control-group">
                <label for="DateTime">Vreme</label>
                <input type="text" id="DateTime" runat="server" readonly="readonly">
            </div>
            <div class="pure-control-group">
                <label for="Time">Trajanje</label>
                <span>
                    <asp:TextBox ID="Time" runat="server" type="number" min="1" max="5" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp; sata
                </span>
            </div>
            <div class="pure-control-group">
                <label for="Prijatelji">Prijatelji</label>
                <asp:TextBox ID="Prijatelji" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="pure-control-group" id="ocenaDiv" runat="server" visible="false">
                <label for="rateYo">Ocena</label>
                <div id="rateYo" style="display:inline-block; margin-top:5px;"></div>
                <input type="hidden" id="numOfStars" runat="server" />
            </div>
        </fieldset>

        <div class="pure-controls" id="controlsDiv" runat="server">
            <asp:Button ID="AcceptButton" runat="server" Text="Dolazim" 
                CssClass="pure-button pure-button-primary" onclick="AcceptButton_Click"/>
            <asp:Button ID="DeclineButton" runat="server" Text="Ne dolazim" 
                CssClass="pure-button pure-button-primary" style="margin-left:50px" 
                onclick="DeclineButton_Click"/>
        </div>

        <div class="pure-controls" id="controlDivOcena" runat="server" visible="false">
            <a href="javascript:oceni()" class="pure-button pure-button-primary">Oceni</a>
        </div>

    </div>

</asp:Content>
