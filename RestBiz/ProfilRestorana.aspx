<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilRestorana.aspx.cs" Inherits="RestBiz.ProfilRestorana" ClientIDMode="Static"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="Scripts/profilRestorana.js"></script>

    <div class="content-subhead">
        <asp:Literal ID="ProfileHeader" runat="server"></asp:Literal>
    </div>

    <fieldset>
        <asp:Repeater ID="PodaciRestorana" runat="server" OnItemDataBound ="PodaciRestorana_ItemDataBound">
            <ItemTemplate>
                <div class="pure-control-group">
                    <label for="name">Naziv</label>
                    <input value="<%#Eval("Naziv") %>" id="name" type="text" readonly="readonly">
                </div>

                <div class="pure-control-group">
                    <label for="desc">Opis</label>
                    <textarea id="desc" readonly="readonly" rows="2" cols="22"> <%# Eval("Opis") %> </textarea>
                </div>

                <%--<div class="pure-control-group">
                    <label for="adresa">Adresa</label>
                    <input value="<%#Eval("Adresa") %>" id="address" type="text" readonly="readonly">
                </div>--%>
                <input type="hidden" value="<%#Eval("RestoranId") %>" id="idRestorana" />

                <p>
                    <span>
                        <a href="javascript:prikaziJelovnik()" class="pure-button" id="btn">Prikaži jelovnik</a>
                        <a href="javascript:editRest()" class="pure-button pure-button-primary" id="editRestBtn" style="margin-left: 40px;" runat="server" visible="false">Izmeni podatke</a>
                    </span>
                </p>

                
                
            </ItemTemplate>
        </asp:Repeater>

        <p>
            <%--<a class="pure-button" id="btnConf" runat="server">Podesi konfiguraciju sedenja</a>--%>
            <%--<asp:Button ID="ButtonConf" runat="server" Text="Podesi konfiguraciju sedenja" />--%>
            <asp:HyperLink ID="ButtonConf" runat="server" class="pure-button" Visible="false">Podesi konfiguraciju sedenja</asp:HyperLink>
        </p>
        

        

        <%--<div class="pure-controls" id="controlsDiv">
            <a href="javascript:enableEdit()" class="pure-button pure-button-primary" id="editButton">Izmeni podatke</a>
        </div>--%>
    </fieldset>

    <div class="content-subhead">
    </div>
   
     <div id="jelovnikTableDiv" style="display: none">
        <legend>Jelovnik</legend>
        <table class="pure-table pure-table-bordered" id="jelovnikTable">
            <thead>
                <tr>
                    <th>Naziv</th>
                    <th>Opis</th>
                    <th>Cena</th>
                    <th id="thEdit" runat="server" visible="false"></th>
                    <th id="thDel" runat="server" visible="false"></th>
                </tr>
            </thead>
            <tbody id ="tableBody"> 
                <asp:Repeater ID="JelovnikRepeater" runat="server" OnItemDataBound="JelovnikRepeater_ItemDataBound">
                    <ItemTemplate>
                        <tr id = "tr<%#Eval("StavkaId") %>">
                            <td id="nameCell<%# Eval("StavkaId") %>"><%#Eval("Naziv") %></td>
                            <td id="descCell<%# Eval("StavkaId") %>"><%#Eval("Opis") %></td>
                            <td id="priceCell<%# Eval("StavkaId") %>"><%#Eval("Cena") %></td>
                            <td id="editCell" runat="server" visible="false"> <a href="javascript:enableEdit(<%# Eval("StavkaId") %>)" class="pure-button" id="enableEditBtn<%#Eval("StavkaId") %>">Izmeni</a></td>
                            <td id="delCell" runat="server" visible="false"> <a href="javascript:remove(<%# Eval("StavkaId") %>)" class="pure-button">Ukloni</a></td>
                        </tr>
                        <input id="id" type="hidden"  value="<%# Eval("StavkaId") %>"/>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

         <p>
             <a href="javascript:dodajStavku()" class="pure-button pure-button-primary" id="dodajBtn" runat="server" visible="false">Dodaj jelo</a>
         </p>

         <asp:Button ID="hiddenButton" runat="server" Text="Button" style="display:none" OnClick="hiddenButton_Click"/>

    </div>


</asp:Content>
