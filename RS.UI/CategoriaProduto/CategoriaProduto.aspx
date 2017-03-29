<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CategoriaProduto.aspx.vb" Inherits="RS.UI.CategoriaProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {

            $('#<%= txtNomeCategoria.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
        }

        function CloseValidationSummary() {
            document.getElementById('<%= validaTxtNomeCategoria.ClientID%>').style.visibility = "hidden";

        }

        function limparCampos() {
            document.getElementById('<%= pAcao.ClientID %>').value = "Inserir";
            document.getElementById('<%= txtNomeCategoria.ClientID%>').value = "";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Categoria de Produto";
            CloseValidationSummary();
            OpenModal("dialogCategoria");
        }

        function GetCategoria() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "CategoriaProduto.aspx/TableCategoria",
                //data: '{idPessoa: ' + idPessoa + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoCategoria,
                error:
                    function (xhr, textStatus, err) {
                        console.log("readyState: " + xhr.readyState);
                        console.log("responseText: " + xhr.responseText);
                        console.log("status: " + xhr.status);
                        console.log("text status: " + textStatus);
                        console.log("error: " + err);
                    }
            });
        }

        function RetornoCategoria(response) {
            var tr;
            var json = $.parseJSON(response.d);

            $("#divCategoria").empty();

            if (response.d == 0) {
                $('#alertRegistros').show();
            }
            else {
                var html = '<table id="tbCategoria" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Categoria de Produto</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

                $('#divCategoria').append(html);

                for (var i = 0; i < json.TableCategoria.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + json.TableCategoria[i].Acao + "</td>")
                    tr.append("<td>" + json.TableCategoria[i].Descricao + "</td>");
                    $('#tbCategoria').append(tr);

                }
                $('#tbCategoria').dataTable();
            }
            waitingDialog.hide('Aguarde...')
        };

        function PopulaCategoria(idCategoria) {
            waitingDialog.show('Aguarde...');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Alterar Categoria";
            $.ajax({
                type: "POST",
                url: "CategoriaProduto.aspx/PopulaCategoriaById",
                data: '{idCategoria: ' + idCategoria + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoCategoriaById,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornoCategoriaById(response) {
            var tr;
            var json = $.parseJSON(response.d);

            for (var i = 0; i < json.TableCategoriaById.length; i++) {
                document.getElementById('<%= txtNomeCategoria.ClientID%>').value = json.TableCategoriaById[i].Descricao;
                document.getElementById('<%= hdnIdCategoria.ClientID%>').value = json.TableCategoriaById[i].IdCategoria;
            }

            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById("conteudoPagina_pAcao").value = "Alterar";
            $('.selectpicker').selectpicker('refresh');
            waitingDialog.hide('Aguarde...')
            OpenModal('dialogCategoria');
        };

        function CloseModal(id) {
            $('.modal-backdrop').remove();
            $('#' + id).modal('hide');
        }

        function OpenModal(id, abrir) {
            CloseModal(id);
            $('#' + id).modal('toggle');

            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });

            if (abrir == true) {
                GetCategoria();
            }
        }

        function ExcluiCategoria(hdnIdCategoria) {
            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdCategoria.ClientID%>').value = hdnIdCategoria;
            OpenModal("dialogDel");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Cadastro de Categoria de Produto
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
 <div class="box">
        <!-- /.box-header -->
        <div class="box-body table-responsive">
            <div class="col-md-12">
                <a href="javascript:limparCampos();" class="btn btn-primary margin">Nova Categoria</a>
            </div>

            <div class="row ">
                <div class="box-body">
                    <div id="divCategoria"></div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.row -->
            <div class="alert alert-info fade in" id="alertRegistros" style="display: none;" role="alert">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Informação:</span>
                Não há Categorias cadastradas. 
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->


    <!-- Modal Documento -->
    <div class="modal" id="dialogCategoria" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" onclick="CloseValidationSummary();" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblModal" runat="server" Enabled="false" /></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset>

                                        <legend>Dados da Categoria</legend>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Descrição<asp:RequiredFieldValidator ID="validaTxtNomeCategoria" runat="server" ErrorMessage="Informe o Nome do Categoria."
                                                            ControlToValidate="txtNomeCategoria" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtNomeCategoria" placeholder="informe o nome da categoria." CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnIdCategoria" runat="server" />
                                                </div>
                                            </div>
                                           
                                            <div class="col-md-12"  id="divModalErro" runat="server">
                                                <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
                                                <div class="alert alert-warning">
                                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Button ID="btnGravar" runat="server" OnClick="btnGravar_Click" CssClass="btn btn-primary" Text="Gravar" />
                                                    <input id="pAcao" runat="server" type="hidden" />
                                                </div>
                                            </div>
                                       </div>
                                        <!-- /.row -->

                                    </fieldset>
                                </div>
                            </div>

                            <div class="modal-footer ">
                                <a class="close" data-dismiss="modal" onclick="CloseValidationSummary();" href="#">Fechar</a>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /.modal-body -->
                </div>
                <!-- /.modal-content -->
            </div>
        </div>
    </div>

    <div class="modal fade" id="dialogAlerta" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cadastro de Categoria</h4>
                </div>

                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                            <div class="modal-footer ">
                                <button type="button" class="close" data-dismiss="modal">OK</button>
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

    <!-- Modal Del -->
    <div class="modal fade dialogDel" id="dialogDel" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Excluir Categoria</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <%--<input id="hdnIdCategoria" runat="server" type="hidden" />--%>
                            <div class="alert alert-info ">Você realmente deseja excluir esta Categoria de Produto? </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnConfirmExclusao" class="btn btn-primary" runat="server" Text="Sim" OnClientClick="btnConfirmExclusao_Click"></asp:Button>
                                <a class="btn btn-danger" data-dismiss="modal" href="#">Não</a>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.modal-body -->
            </div>
            <!-- /.modal-content -->
        </div>
    </div>
</asp:Content>