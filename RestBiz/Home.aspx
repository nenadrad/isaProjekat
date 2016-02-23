<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RestBiz.Home" ClientIDMode="Static"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="Scripts/home.js"></script>

    <div class="content-subhead" id="testDiv" runat="server">
    </div>
    
    <div id="MenadzerSistemaDiv" runat="server" visible="false">
        <a href="javascript:showFormaRestoran()" class="pure-button pure-button-primary">Dodaj novi restoran</a>
        <a href="javascript:showFormaMenadzer()" class="pure-button pure-button-primary" style="margin-left:50px;">Dodaj novog menadžera</a>

        <div id="FormaRestoran" style="display:none; margin-top: 50px">
            <legend>Novi restoran</legend>
            <fieldset>
                <div class="pure-control-group">
                    <label for="Naziv">Naziv</label>
                    <asp:TextBox ID="Naziv" runat="server"></asp:TextBox>
                </div>

                <div class="pure-control-group">
                    <label for="Opis">Opis</label>
                    <textarea id="Opis" runat="server" rows="2" cols="22"></textarea>
                </div>

                <div class="pure-controls">
                    <asp:Button ID="SubmitRestoran" type="submit" runat="server" Text="Snimi" CssClass="pure-button pure-button-primary" OnClick="SubmitRestoran_Click" />
                </div>
            </fieldset>     
        </div>

        <div id="FormaMenadzer" style="display:none; margin-top: 50px">
            <legend>Novi restoran</legend>
            <fieldset>
                <div class="pure-control-group">
                    <label for="Ime">Ime</label>
                    <asp:TextBox ID="Ime" runat="server"></asp:TextBox>
                </div>

                <div class="pure-control-group">
                    <label for="Prezime">Prezime</label>
                    <asp:TextBox ID="Prezime" runat="server"></asp:TextBox>
                </div>

                <div class="pure-control-group">
                    <label for="Adresa">Adresa</label>
                    <asp:TextBox ID="Adresa" runat="server"></asp:TextBox>
                </div>

                <div class="pure-control-group">
                    <label for="Email">Email adresa</label>
                    <asp:TextBox ID="Email" type="email" runat="server"></asp:TextBox>
                </div>

                <div class="pure-control-group">
                    <label for="Lozinka">Lozinka</label>
                    <asp:TextBox ID="Lozinka" type="password" runat="server"></asp:TextBox>
                </div>

                <div class="pure-control-group">
                    <label for="Restorani">Menadzer u restoranu</label>
                    <asp:DropDownList ID="Restorani" runat="server"></asp:DropDownList>
                </div>     

                <div class="pure-controls">
                    <asp:Button ID="SubmitMenadzer" type="submit" runat="server" Text="Snimi" CssClass="pure-button pure-button-primary" OnClick="SubmitMenadzer_Click"/>
                </div>
            </fieldset>     
        </div>

    </div>
    

</asp:Content>
