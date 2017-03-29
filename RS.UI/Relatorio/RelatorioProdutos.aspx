<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RelatorioProdutos.aspx.vb" Inherits="RS.UI.RelatórioProdutos" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
     <script type="text/javascript">
        function pageLoad() {
            $('#<%=txtDataInicial.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%=txtDataFinal.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%=txtDataInicial.ClientID%>').mask("99/99/9999");
            $('#<%=txtDataFinal.ClientID%>').mask("99/99/9999");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
 <div class="row" id="divData" runat="server">
        <div class="col-md-4">
            <div class="form-group">
                <label>Produto</label>
                <asp:TextBox ID="txtNome" placeholder="Pesquise pelo nome ou parte do nome do Produto" MaxLength="100" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
      <div class="col-md-4">
            <div class="form-group">
                <label>Descrição</label>
                <asp:TextBox ID="txtDescricao" placeholder="Pesquise pela descrição ou parte da descrição do Produto" MaxLength="400" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
     <div class="col-md-4">
            <div class="form-group">
                <label>Categoria</label>
                <asp:TextBox ID="txtCategoria" placeholder="Pesquise pela categoria ou parte da categoria do Produto" MaxLength="200" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
    
        <div class="col-md-2">
            <div class="form-group">
                <label>Data Validade (Início)</label>
                <asp:TextBox ID="txtDataInicial" placeholder="dd/mm/aaaa" MaxLength="10" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group">
                <label>Data Validade (Final)</label>
                <asp:TextBox ID="txtDataFinal" placeholder="dd/mm/aaaa"  MaxLength="10" CssClass="form-control" data-live-search="true" runat="server"></asp:TextBox>
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
    <rsweb:ReportViewer ID="rptProduto" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
        <LocalReport ReportEmbeddedResource="RS.UI.rpt_Produto.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="objDataSource" Name="DataSetProduto" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    
   <asp:ObjectDataSource ID="objDataSource" runat="server" SelectMethod="GetRelatorioProduto" TypeName="RS.Servico.ServicoProduto">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDataInicial" DefaultValue="" Name="DataInicial" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtDataFinal" DefaultValue="" Name="DataFinal" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtNome" DefaultValue="" Name="Nome" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtDescricao" DefaultValue="" Name="Descricao" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtCategoria" DefaultValue="" Name="Categoria" PropertyName="Text" Type="String" />
            <%--Nome, Categoria, Descricao, DataInicial, DataFinal--%>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
