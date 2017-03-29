<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioDatasComemorativas.aspx.vb" Inherits="RS.UI.RelatorioDatasComemorativas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $('#<%=txtDataInicial.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%=txtDataFinal.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
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
                <asp:TextBox ID="txtNome" placeholder="Pesquise pelo nome ou parte do nome" MaxLength="50" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Data Início</label>
                <asp:TextBox ID="txtDataInicial" placeholder="dd/mm/aaaa" CssClass="form-control" MaxLength="10" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Data Fim</label>
                <asp:TextBox ID="txtDataFinal" placeholder="dd/mm/aaaa" CssClass="form-control" MaxLength="10" data-live-search="true" runat="server"></asp:TextBox>
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

    <div class="row" id="divModalErro" visible="false" runat="server">
        <div class="box-body col-md-12">
            <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
            <div class="alert alert-warning">
                <asp:Label ID="lblErro" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <rsweb:ReportViewer ID="rptDataComemorativa" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
        <LocalReport ReportEmbeddedResource="RS.UI.rpt_DataComemorativa.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="objDataSource" Name="DataSetDataComemorativa" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

   <asp:ObjectDataSource ID="objDataSource" runat="server" SelectMethod="GetRelatorioDataComemorativaCliente" TypeName="RS.Servico.ServicoCliente">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDataInicial" DefaultValue="" Name="dataInicial" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtDataFinal" DefaultValue="" Name="dataFinal" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtNome" DefaultValue="" Name="nome" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
