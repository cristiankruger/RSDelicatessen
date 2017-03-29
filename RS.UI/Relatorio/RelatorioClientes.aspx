<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioClientes.aspx.vb" Inherits="RS.UI.RelatorioClientes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {
            $('.selectpicker').selectpicker('refresh');
        }
        </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="row" id="divData" runat="server">
        <div class="col-md-3">
            <div class="form-group">
                <label>Nome</label>
                <asp:TextBox ID="txtNome" placeholder="Pesquise pelo nome ou parte do nome" MaxLength="100" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Cidade</label>
                <asp:TextBox ID="txtCidade" placeholder="Pesquise pelo nome ou parte do nome da cidade" MaxLength="50" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Bairro</label>
                <asp:TextBox ID="txtBairro" placeholder="Pesquise pelo nome ou parte do nome do Bairro" MaxLength="50" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>UF</label>
                <asp:DropDownList ID="drpUF" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="col-md-2">
        <div class="form-group">
            <asp:Button ID="btnGeraRelat" OnClick="btnGeraRelat_Click" CssClass="btn btn-primary" runat="server" Text="Gerar Relatório" />
        </div>
    </div>
    <div class="col-md-10">
        <div class="form-group">
            <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="btn btn-primary" runat="server" Text="Limpar Campos" />
        </div>
    </div>
    <rsweb:ReportViewer ID="rptCliente" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
        <LocalReport ReportEmbeddedResource="RS.UI.rpt_Cliente.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="objDataSource" Name="DataSetCliente" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource ID="objDataSource" runat="server" SelectMethod="GetRelatorioCliente" TypeName="RS.Servico.ServicoCliente">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtBairro" DefaultValue="" Name="Bairro" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtCidade" DefaultValue="" Name="Cidade" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtNome" DefaultValue="" Name="Nome" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="drpUF" DefaultValue="0" Name="IdUF" PropertyName="TEXT" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
