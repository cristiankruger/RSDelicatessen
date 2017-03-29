<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Produto.aspx.vb" Inherits="RS.UI.Produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {

            $('#<%= txtNomeProduto.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
            $("#<%=txtValor.ClientID%>").mask('000.000,00', { reverse: true });
            $('#<%=txtDataFabricacao.ClientID%>').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR',
                autoclose: true,
                "setDate": new Date(),
                yearRange: '1900:2050'
            });
            $('#<%=txtDataValidade.ClientID%>').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR',
                autoclose: true,
                "setDate": new Date(),
                yearRange: '1900:2050'
            });
            $('#<%=txtDataFabricacao.ClientID%>').mask("99/99/9999");
            $('#<%=txtDataValidade.ClientID%>').mask("99/99/9999");

            $('.selectpicker').selectpicker('render');
        }

        function CloseValidationSummary() {
            document.getElementById('<%= validaTxtNomeProduto.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtValor.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtDataFabricacao.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= ValidatxtDataValidade.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaDrpCategoria.ClientID%>').style.visibility = "hidden";

        }

        function limparCampos() {
            document.getElementById('<%= pAcao.ClientID %>').value = "Inserir";
            document.getElementById('<%= txtNomeProduto.ClientID%>').value = "";
            document.getElementById('<%= txtDataFabricacao.ClientID%>').value = "";
            document.getElementById('<%= txtDataValidade.ClientID%>').value = "";
            document.getElementById('<%= txtDescricao.ClientID%>').value = "";
            document.getElementById('<%= txtValor.ClientID%>').value = "";
            document.getElementById('<%= drpCategoria.ClientID%>').value = "0";
            document.getElementById('<%= txtNomeProduto.ClientID%>').value = "";
            document.getElementById('<%= hdnImgProduto.ClientID%>').value = "";
            document.getElementById('<%= imgProduto.ClientID%>').setAttribute('src', '/Foto/FotoProduto/imagemNaoCadastrada.png');
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Produto";
            $('.selectpicker').selectpicker('render');
            CloseValidationSummary();
            OpenModal("dialogProduto");
        }

        function GetProduto() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Produto.aspx/TableProduto",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoProduto,
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

        function RetornoProduto(response) {
            var tr;
            var json = $.parseJSON(response.d);

            $("#divProduto").empty();

            if (response.d == 0) {
                $('#alertRegistros').show();
            }
            else {
                var html = '<table id="tbProduto" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Nome</th> \
                <th>Descrição</th> \
                <th>Categoria</th> \
                <th>Valor</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

                $('#divProduto').append(html);

                for (var i = 0; i < json.TableProduto.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + json.TableProduto[i].Acao + "</td>")
                    tr.append("<td>" + json.TableProduto[i].Nome + "</td>");
                    tr.append("<td>" + json.TableProduto[i].Descricao + "</td>");
                    tr.append("<td>" + json.TableProduto[i].Categoria + "</td>");
                    tr.append("<td>" + json.TableProduto[i].Valor + "</td>");
                    $('#tbProduto').append(tr);

                }
                $('#tbProduto').dataTable();
            }
            waitingDialog.hide('Aguarde...')
        };

        function PopulaProduto(idProduto) {
            waitingDialog.show('Aguarde...');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Alterar Produto";
            $.ajax({
                type: "POST",
                url: "Produto.aspx/PopulaProdutoById",
                data: '{idProduto: ' + idProduto + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoProdutoById,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornoProdutoById(response) {
            var tr;
            var json = $.parseJSON(response.d);

            for (var i = 0; i < json.TableProdutoById.length; i++) {
                document.getElementById('<%= txtNomeProduto.ClientID%>').value = json.TableProdutoById[i].Nome;
                document.getElementById('<%= txtDataFabricacao.ClientID%>').value = json.TableProdutoById[i].DataFabricacao;
                document.getElementById('<%= txtDataValidade.ClientID%>').value = json.TableProdutoById[i].DataValidade;
                document.getElementById('<%= txtDescricao.ClientID%>').value = json.TableProdutoById[i].Descricao;
                document.getElementById('<%= txtValor.ClientID%>').value = json.TableProdutoById[i].Valor;
                document.getElementById('<%= drpCategoria.ClientID%>').value = json.TableProdutoById[i].IdCategoria;
                document.getElementById('<%= hdnIdProduto.ClientID%>').value = json.TableProdutoById[i].idProduto;
                document.getElementById('<%= imgProduto.ClientID%>').src = json.TableProdutoById[i].Foto;
                document.getElementById('<%= hdnImgProduto.ClientID%>').value = json.TableProdutoById[i].Foto;
            }

            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById("conteudoPagina_pAcao").value = "Alterar";
            $('.selectpicker').selectpicker('refresh');
            waitingDialog.hide('Aguarde...')
            OpenModal('dialogProduto');
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
                GetProduto();
            }
        }

        function ExcluiProduto(hdnIdProduto) {
            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdProduto.ClientID%>').value = hdnIdProduto;
            OpenModal("dialogDel");
        }

        var loadFile = function (event) {
            var output = document.getElementById('<%= imgProduto.ClientID%>');
            output.src = URL.createObjectURL(event.target.files[0]);
            document.getElementById('<%= hdnImgProduto.ClientID%>').value = output.src;
        };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Cadastro de Produtos
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="box">
        <!-- /.box-header -->
        <div class="box-body table-responsive">
            <div class="col-md-12">
                <a href="javascript:limparCampos();" class="btn btn-primary margin">Novo Produto</a>
            </div>

            <div class="row ">
                <div class="box-body">
                    <div id="divProduto"></div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.row -->
            <div class="alert alert-info fade in" id="alertRegistros" style="display: none;" role="alert">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Informação:</span>
                Não há Produtos cadastrados. 
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->


    <!-- Modal Documento -->
    <div class="modal dialogFull" id="dialogProduto" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
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

                                        <legend>Dados do Produto</legend>

                                        <div class="row">

                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <div>
                                                        <label>Foto do Produto</label>
                                                    </div>
                                                    <div>
                                                        <asp:Image CssClass="img-rouded" ID="imgProduto" Height="350px" Width="350px" runat="server" BackColor="#999999" />
                                                        <asp:HiddenField runat="server" ID="hdnImgProduto" />
                                                    </div>
                                                    <div>
                                                        <asp:FileUpload CssClass="btn btn-primary" OnChange="loadFile(event);" ID="FileUpload" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Nome<asp:RequiredFieldValidator ID="validaTxtNomeProduto" MaxLength="50" runat="server" ControlToValidate="txtNomeProduto"
                                                            CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtNomeProduto" CssClass="form-control"  runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnIdProduto" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Descrição</label>
                                                    <asp:TextBox ID="txtDescricao" CssClass="form-control" placeholder="Informe ingredientes do Produto ou o que compôe o produto/Kit."  MaxLength="200" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Categoria<asp:RequiredFieldValidator ID="validaDrpCategoria" runat="server"
                                                            ControlToValidate="drpCategoria" InitialValue="0" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="drpCategoria" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Valor<asp:RequiredFieldValidator ID="validaTxtValor" runat="server" InitialValue="" ControlToValidate="txtValor"
                                                            CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtValor" placeholder="0,00" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Data Fabricação<asp:RequiredFieldValidator ID="validaTxtDataFabricacao" runat="server" ControlToValidate="txtDataFabricacao"
                                                            CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtDataFabricacao" placeholder="dd/mm/aaaa" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        Data Validade<asp:RequiredFieldValidator ID="ValidatxtDataValidade" runat="server" ControlToValidate="txtDataValidade"
                                                            CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtDataValidade" placeholder="dd/mm/aaaa" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divModalErro" runat="server">
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
                    <h4 class="modal-title">Cadastro de Produto</h4>
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
                    <h4 class="modal-title">Excluir Produto</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <%--<input id="hdnIdCategoria" runat="server" type="hidden" />--%>
                            <div class="alert alert-info ">Você realmente deseja excluir este Produto? </div>
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
