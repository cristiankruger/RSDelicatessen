<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioProdutosVendidos.aspx.vb" Inherits="RS.UI.RelatorioProdutosVendidos" %>

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
                <label>Categoria</label>
                <asp:TextBox ID="txtDescricaoCategoria" placeholder="Pesquise pelo nome ou parte do nome da Categoria do Produto" MaxLength="100" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label>Descrição Produto</label>
                <asp:TextBox ID="txtDescricaoProduto" placeholder="Pesquise pelo nome ou parte do nome da Descrição do Produto" MaxLength="100" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row" id="divData" runat="server">
        <div class="col-md-3">
            <div class="form-group">
                <label>Categoria</label>
                <asp:DropDownList ID="drpCategoria" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server"></asp:DropDownList>
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
    <rsweb:ReportViewer ID="rptProdutosVendidos" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
        <LocalReport ReportEmbeddedResource="RS.UI.rpt_ProdutosVendidos.rdlc" ReportPath="Report\rpt_ProdutosVendidos.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="objDataSourceProduto" Name="DataSetProdutosVendidos" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource ID="objDataSourceProduto" runat="server" SelectMethod="GetRelatorioProdutosVendidos" TypeName="RS.Servico.ServicoProduto">
        <SelectParameters>
            <asp:ControlParameter ControlID="drpCategoria" DefaultValue="0" Name="IdCategoria" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="txtDescricaoCategoria" DefaultValue="" Name="DescricaoCategoria" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtDescricaoProduto" DefaultValue="" Name="DescricaoProduto" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtDtInicial" DefaultValue="" Name="DataInicial" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtDtFinal" DefaultValue="" Name="DataFinal" PropertyName="Text" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
