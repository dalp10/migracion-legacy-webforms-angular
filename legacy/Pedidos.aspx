<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="TuEmpresa.Legacy.Pedidos" %>
<!-- EJEMPLO ILUSTRATIVO del sistema legacy (el "antes"). No se ejecuta tal cual. -->
<!DOCTYPE html>
<html>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" />

    Cliente: <asp:TextBox ID="txtCliente" runat="server" />
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />

    <asp:UpdatePanel ID="up" runat="server">
      <ContentTemplate>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
        <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false"
                      DataKeyNames="Id" OnRowCommand="gvPedidos_RowCommand">
          <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" />
            <asp:ButtonField CommandName="Aprobar" Text="Aprobar" />
          </Columns>
        </asp:GridView>
      </ContentTemplate>
    </asp:UpdatePanel>
  </form>
</body>
</html>
