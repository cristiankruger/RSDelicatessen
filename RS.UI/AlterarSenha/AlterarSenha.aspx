<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AlterarSenha.aspx.vb" Inherits="RS.UI.AlterarSenha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
<script>
    function OpenModal(id, abrir) {
        <%--CloseModal(id);--%>
        $('#' + id).modal('toggle');

        $('.modal-dialog').draggable({
            handle: ".modal-header"
        });
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Alterar Senha
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">

    <div class="box">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        Digite Nova Senha
                        <asp:RequiredFieldValidator ID="requiredSenhaNova" ControlToValidate="txtSenhaNova1" runat="server" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox TextMode="Password" placeholder="mínimo de 6 e máximo 10 caracteres" CssClass="form-control" MaxLength="10" ID="txtSenhaNova1" runat="server"></asp:TextBox>
                &nbsp;</div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        Confirme Nova Senha
                        <asp:RequiredFieldValidator ID="requiredSenhaNova2" ControlToValidate="txtSenhaNova2" runat="server" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                    </label>
                    <asp:TextBox TextMode="Password" placeholder="mínimo de 6 e máximo 10 caracteres" CssClass="form-control" MaxLength="10" ID="txtSenhaNova2" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row" id="divModalErro" visible="false" runat="server">
            <div class=" col-md-12">
                <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
                <div class="alert alert-warning">
                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Button ID="btnGravar" OnClick="btnGravar_Click" runat="server" CssClass="btn btn-primary" Text="Gravar" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="dialogAlerta" tabindex="-1" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Alterar Senha</h4>
                </div>

                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info">
                                    <asp:Label ID="lblAlertaModal" runat="server"></asp:Label>
                            </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnOk" Text="OK" CausesValidation="false" OnClick="btnOk_Click" cssclass="btn btn-primary" runat="server"></asp:Button>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.row -->
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>

</asp:Content>