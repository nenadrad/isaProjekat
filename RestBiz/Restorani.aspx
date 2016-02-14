<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Restorani.aspx.cs" Inherits="RestBiz.Restorani" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-subhead">
    </div>

    <div id="restoraniTableDiv" runat="server">
        <table class="pure-table pure-table-bordered">
            <thead>
                <tr>
                    <th>Naziv</th>
                    <%--<th>Dodaj/obriši</th>--%>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RestoraniRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><a class="pure-button pure-button-hover" href = "ProfilRestorana.aspx?id=<%# Eval("RestoranId") %>"><%# Eval("Naziv") %></a></td>
                            <%--<td><a runat="server" id="buttonCell" href="#"></a></td>--%>
                        </tr>
                        <input type="hidden"  value="<%# Eval("RestoranId") %>"/>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <script type="text/javascript" src="Scripts/restorani.js"></script>

</asp:Content>
