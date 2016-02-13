<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prijatelji.aspx.cs" Inherits="RestBiz.Prijatelji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-subhead">
    </div>
    
    <legend>Pretraga</legend>
        <asp:TextBox ID="SearchInput" runat="server" class="pure-input-rounded" onkeypress="return enterEvent(event)"></asp:TextBox>
        <asp:Button ID="SearchButton" runat="server" class="pure-button" Text="Pretraži" OnClick="SearchButton_Click" />
  

    <div class="content-subhead">
    </div>

    <div id="friendsTableDiv" runat="server">
        <table class="pure-table pure-table-bordered">
            <thead>
                <tr>
                    <th>Ime i prezime</th>
                    <th>Dodaj/obriši</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="KorisniciRepeater" runat="server"
                    OnItemDataBound="KorisniciRepeater_ItemDataBound">
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

    <script type="text/javascript" src="Scripts/prijatelji.js"></script>

</asp:Content>
