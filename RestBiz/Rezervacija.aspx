<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rezervacija.aspx.cs" Inherits="RestBiz.Rezervacija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Styles/jquery-ui.css" rel="stylesheet">

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

    <asp:Panel ID="RasporedStolova" runat="server" Visible="false" style="padding-left:100px;">
        <table style="border-spacing: 8px 2px;">
            <asp:Repeater ID="RowsRepeater" runat="server" OnItemDataBound="RowsRepeater_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <asp:Repeater ID="ColsRepeater" runat="server">
                            <ItemTemplate>
                                <td style="padding: 6px;"><a class="pure-button button-xlarge stolovi">&nbsp</a></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>

    <div class="pure-controls">
        <asp:Button ID="NextButton1" runat="server" Text="Dalje" CssClass="pure-button pure-button-primary" OnClick="NextButton1_Click" />
    </div>
    

</asp:Content>
