<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rezervacija.aspx.cs" Inherits="RestBiz.Rezervacija" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Styles/jquery-ui.css" rel="stylesheet">

    <style scoped>

        .button-xlarge {
            font-size: 150%;
        }

        .stolovi {
            width: 75px;
            border: 2px solid white;
        }

        .free-table {
            background: rgb(28, 184, 65); 
        }

        .selected {
            border : 2px solid red;
        }
        
        .taken-table {
            background: rgb(202, 60, 60); 
        }

    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="Scripts/rezervacija.js"></script>

    <script>

        $(function () {
            $("#Date").datepicker();
        });

        $(function () {
            $('#DateTime').timepicker({ 'step': 15, 'timeFormat': 'H:i:s', 'minTime': '10:00am', 'maxTime': '10:00pm', });
        });

    </script>

    <div class="content-subhead">
        <h2>Rezervacija</h2>
    </div>

    <div id="MessageDiv" runat="server" visible="false">
        <p>
            Rezervacija uspešna.
        </p>
    </div>

    <div id="FormDiv" runat="server">
        <fieldset>
            <div class="pure-control-group">
                <label for="Name">Restoran</label>
                <asp:TextBox ID="Name" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="pure-control-group">
                <label for="Date">Datum</label>
                <input type="text" id="Date" runat="server">
            </div>
            <div class="pure-control-group">
                <label for="DateTime">Vreme</label>
                <input type="text" id="DateTime" runat="server">
            </div>
            <div class="pure-control-group">
                <label for="Time">Trajanje</label>
                <span>
                    <asp:TextBox ID="Time" runat="server" type="number" min="1" max="5"></asp:TextBox>&nbsp;&nbsp; sata
                </span>
            </div>
        </fieldset>


        <div class="pure-controls">
            <asp:Button ID="NextButton1" runat="server" Text="Dalje" CssClass="pure-button pure-button-primary" OnClick="NextButton1_Click" />
        </div>
    </div>

    <div id="RasporedStolovaDiv" runat="server"  style="padding-left:100px; display:none">
        <table style="border-spacing: 8px 2px;">
            <asp:Repeater ID="RowsRepeater" runat="server" OnItemDataBound="RowsRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <asp:Repeater ID="ColsRepeater" runat="server">
                            <ItemTemplate>
                                <td style="padding: 6px;"><a class="pure-button button-xlarge stolovi" runat="server" id="sto">&nbsp</a></td>
                            </ItemTemplate>                        
                         </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <div class="pure-controls">
            <a class="pure-button pure-button-primary" id="testBtn" href="javascript:GetIzabraniStolovi()">Dalje</a>
        </div>

    </div>

    <div id="InviteDiv" runat="server" style="display: none">

        <legend>Pozovi prijatelje</legend>
        <asp:TextBox ID="SearchInput" runat="server" class="pure-input-rounded" onkeypress ="return enterEvent(event)"></asp:TextBox>
        <asp:Button ID="SearchButton" runat="server" class="pure-button" Text="Pretraži" OnClick="SearchButton_Click" />

        <div class="content-subhead">
        </div>

        <div id="friendsResults" runat="server" style="display: none">
            <table class="pure-table pure-table-bordered" id="resTable">
                <thead>
                    <tr>
                        <th>Ime i prezime</th>
                        <th>Dodaj</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="KorisniciRepeater" runat="server" OnItemDataBound="KorisniciRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("ImePrezime") %></td>
                                <td><a runat="server" id="buttonCell" href="#"></a></td>
                            </tr>
                            <input type="hidden" id="userId" runat="server" />
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

            <div class="pure-controls">
                <asp:Button ID="ConfirmButton" runat="server" Text="Potvrdi" CssClass="pure-button pure-button-primary" OnClick="ConfirmButton_Click"/>
            </div>

        </div>
    

</asp:Content>
