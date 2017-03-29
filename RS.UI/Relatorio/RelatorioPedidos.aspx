<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioPedidos.aspx.vb" Inherits="RS.UI.RelatorioPedidos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=txtDtInicial.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%=txtDtFinal.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%=txtDtInicial.ClientID%>').mask("99/99/9999");
            $('#<%=txtDtFinal.ClientID%>').mask("99/99/9999");
            $('.selectpicker').selectpicker('refresh');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="row" runat="server">
        <div class="col-md-4">
            <div class="form-group">
                <label>Cliente</label>
                <asp:TextBox ID="txtNomeCliente" placeholder="Pesquise pelo nome ou parte do nome do Cliente" MaxLength="100" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Categoria</label>
                <asp:DropDownList ID="drpSituacao" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Data Inicial</label>
                <asp:TextBox ID="txtDtInicial" placeholder="dd/mm/aaaa" MaxLength="10" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Data Final</label>
                <asp:TextBox ID="txtDtFinal" placeholder="dd/mm/aaaa" MaxLength="10" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row" id="botoes" runat="server">
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
    </div>

    <div class="row" id="divModalErro" visible="false" runat="server">
        <div class="box-body col-md-12">
            <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
            <div class="alert alert-warning">
                <asp:Label ID="lblErro" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="rptPedido" runat="server" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" width="100%">
        <LocalReport ReportEmbeddedResource="RS.UI.rpt_Pedidos.rdlc" ReportPath="Report\rpt_Pedidos.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="objDataSource" Name="DataSetPedido" />
            </DataSources>
        </LocalReport>
    </rsweb:reportviewer>

    <asp:ObjectDataSource ID="objDataSource" runat="server" SelectMethod="GetRelatorioPedidos" TypeName="RS.Servico.ServicoPedido">
        <SelectParameters>
            <asp:ControlParameter ControlID="drpSituacao" Name="IdSituacao" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="txtNomeCliente" Name="NomeCliente" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtDtInicial" Name="DataInicial" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtDtFinal" Name="DataFinal" PropertyName="Text" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
