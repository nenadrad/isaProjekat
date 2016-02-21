<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KonfiguracijaSedenja.aspx.cs" Inherits="RestBiz.KonfiguracijaSedenja" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style scoped>

        .button-xlarge {
            font-size: 150%;
        }

        .button-selected {
            background: rgb(223, 117, 20); /* this is an orange */
        }

        .stolovi {
            width: 75px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">

    <script type="text/javascript" src="Scripts/konfiguracijaSedenja.js"></script>

    <div class="content-subhead">
        <asp:Literal ID="ProfileHeader" runat="server"></asp:Literal>
    </div>

    <p>
        Podešavanje konfiguracije sedenja
    </p>

    <table style="border-spacing: 8px 2px;">
        <asp:Repeater ID="RowsRepeater" runat="server" OnItemDataBound="RowsRepeater_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <asp:Repeater ID="ColsRepeater" runat="server">
                        <ItemTemplate>
                            <%--<td style="padding: 6px;"><button class="pure-button button-xlarge stolovi">&nbsp</button></td>--%>
                            <td style="padding: 6px;"><a class="pure-button button-xlarge stolovi">&nbsp</a></td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

    <p>
        <a href="javascript:snimi()" class="pure-button pure-button-primary" id ="btnSnimi">Snimi</a>
        <%--<asp:Button ID="ButtonSnimi" runat="server" Text="Snimi" class="pure-button pure-button-primary" OnClick="ButtonSnimi_Click" />--%>
    </p>

</asp:Content>
