<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MojProfil.aspx.cs" Inherits="RestBiz.MojProfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="Scripts/mojProfil.js"></script>

    <div class="content-subhead">
        <asp:Literal ID="ProfileHeader" runat="server"></asp:Literal>
    </div>


    <div class="pure-g">
    <div class="pure-u-1-2">
    <div style ="float: left;">
    <fieldset>
        <asp:Repeater ID="PodaciKorisnika" runat="server">
            <ItemTemplate>
                <div class="pure-control-group">
                    <label for="name">Ime</label>
                    <input value="<%#Eval("Ime") %>" id="name" type="text" readonly="readonly">
                </div>

                <div class="pure-control-group">
                    <label for="prezime">Prezime</label>
                    <input value="<%# Eval("Prezime") %>" id="lastName" type="text" readonly="readonly">
                </div>

                <div class="pure-control-group">
                    <label for="adresa">Adresa</label>
                    <input value="<%#Eval("Adresa") %>" id="address" type="text" readonly="readonly">
                </div>
                <input type="hidden" value="<%#Eval("KorisnikId") %>" id="id" />
            </ItemTemplate>
        </asp:Repeater>

        <div class="pure-controls" id="controlsDiv">
            <a href="javascript:enableEdit()" class="pure-button pure-button-primary" id="editButton">Izmeni podatke</a>
        </div>
    </fieldset>
    </div>
    </div>

            
    <div class="pure-u-1-2">
    <div id="friendsTableDiv" runat="server" style="float: right">
        <legend>Prijatelji</legend>
        <table class="pure-table pure-table-bordered" id ="friendsTable">
            <thead>
                <tr>
                    <th>Ime i prezime</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>

                <asp:Repeater ID="PrijateljiKorisnika" runat="server" OnItemDataBound="PrijateljiKorisnika_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ImePrezime") %></td>
                            <td><a runat="server" id="buttonCell" href="#"></a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </tbody>
        </table>
    </div>
    </div>

    </div>

    <div style="margin-top: 50px;">

        <legend>Dodaj prijatelje</legend>
        <asp:TextBox ID="SearchInput" runat="server" class="pure-input-rounded"></asp:TextBox>
        <asp:Button ID="SearchButton" runat="server" class="pure-button" Text="Pretraži" OnClick="SearchButton_Click" />

        <div class="content-subhead">
        <div>

        <div id="friendsResults" runat="server" style="display: none">
            <table class="pure-table pure-table-bordered">
                <thead>
                    <tr>
                        <th>Ime i prezime</th>
                        <th>Dodaj/obriši</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="KorisniciRepeater" runat="server" OnItemDataBound="KorisniciRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("ImePrezime") %></td>
                                <td><a runat="server" id="buttonCell" href="#"></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

    </div>
    

</asp:Content>
