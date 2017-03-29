<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pagamento.aspx.vb" Inherits="RS.UI.Pagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {
            $('#<%= txtDataPedido.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtSaldoDevedor.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtVencimentoParcela.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtCPF.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtNome.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtEmail.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtTelefone.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtValorPedido.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtValorFinal.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtDataInicial.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%= txtDataInicial.ClientID%>').mask("99/99/9999");
            $('#<%= txtDesconto.ClientID%>').mask("999");
            $('#<%= txtDesconto.ClientID%>').keyup(function () {
                var RegExpression = /^(100|[1-9][0-9]|[1-9])$/;
                if (RegExpression.test($('#<%= txtDesconto.ClientID%>').val())) {
                    $('#<%= txtDesconto.ClientID%>').val();
                }
                else {
                    $('#<%= txtDesconto.ClientID%>').val(0);
                }
            });
            $('#<%= txtNumeroDeParcelas.ClientID%>').keyup(function () {
                var RegExpression = /^([1-9][0-9]|[1-9])$/;

                if (RegExpression.test($('#<%= txtNumeroDeParcelas.ClientID%>').val())) {
                    $('#<%= txtNumeroDeParcelas.ClientID%>').val();
                }
                else {
                    $('#<%= txtNumeroDeParcelas.ClientID%>').val(1);
                }
            });
            $("#<%=txtValorPagamento.ClientID%>").mask('000.000,00', { reverse: true });


            $("#btnRefatorar").validate({
                ignore: ".ignore"
            })
        };

        function LimpaCampos() {
            document.getElementById('<%= txtDataPagamentoParcela.ClientID%>').value = "";
            document.getElementById('<%= txtValorPagamento.ClientID%>').value = "";
            document.getElementById('<%= validaDrpTipoPagamento.ClientID%>').value = "0";
        };

        function CloseValidationSummary() {
            document.getElementById('<%= validaTxtDataInicial.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= ValidaTxtNumeroDeParcelas.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtDataPagamentoParcela.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= ValidaTxtValorPagamento.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaDrpTipoPagamento.ClientID%>').style.visibility = "hidden";
            LimpaCampos();
        };

        function calculaValorFinal() {
            var desconto = document.getElementById('<%= txtDesconto.ClientID%>').value;
            desconto = parseFloat(desconto.replace(".", "").replace(",", "."));

            var pedido = document.getElementById('<%= txtValorPedido.ClientID%>').value
            pedido = parseFloat(pedido.replace(".", "").replace(",", "."));

            if ((isNaN(pedido) != true) && (isNaN(desconto) != true)) {
                var total = pedido * (100 - desconto) / 100;
                total = parseFloat(total).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                total = total.replace('.', '*').replace(',', '.').replace('*', ',');
                document.getElementById('<%= txtValorFinal.ClientID%>').value = total;
                document.getElementById('<%= txtSaldoDevedor.ClientID%>').value = total;
            } else if (desconto == 0) {
                document.getElementById('<%= txtValorFinal.ClientID%>').value = pedido;
            }
    };

    function GetParcelas(idPagamento) {
        waitingDialog.show('Aguarde...')
        $.ajax({
            type: "POST",
            url: "Pagamento.aspx/TableParcelas",
            data: '{idPagamento: ' + idPagamento + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: RetornoParcelas,
            error:
                function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
        });
    };

    function RetornoParcelas(response) {
        var tr;
        var json = $.parseJSON(response.d);

        $("#divParcelas").empty();

        if (response.d == 0) {
            $('#alertRegistros').show();
            $('#btnGerarParcelas').show();
        } else {
            $('#btnGerarParcelas').hide();
            var html = '<table id="tbParcela" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Data do Vencimento</th> \
                <th>Data do Pagamento</th> \
                <th>Valor da Parcela</th> \
                <th>Tipo Pagamento</th> \
                <th>Situação</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

            $('#divParcelas').append(html);

            for (var i = 0; i < json.TableParcela.length; i++) {
                if (json.TableParcela[i].Situacao == "Pago") {
                    BloqueiaRefatoracao();
                }
                if (json.TableParcela.length == json.TableParcela[i].contador) {
                    BloqueiaPagamento();
                }
                tr = $('<tr/>');
                tr.append("<td>" + json.TableParcela[i].Acao + "</td>")
                tr.append("<td>" + json.TableParcela[i].DataVencimento + "</td>");
                tr.append("<td>" + json.TableParcela[i].DataPagamento + "</td>");
                tr.append("<td>" + json.TableParcela[i].ValorParcela + "</td>");
                tr.append("<td>" + json.TableParcela[i].TipoPagamento + "</td>");
                tr.append("<td>" + json.TableParcela[i].Situacao + "</td>");
                $('#tbParcela').append(tr);
            }
            $('#tbParcela').dataTable();
        }
        waitingDialog.hide('Aguarde...')
    };

    function PopulaParcelaById(idParcela) {
        waitingDialog.show('Aguarde...');

        $.ajax({
            type: "POST",
            url: "Pagamento.aspx/PopulaParcelaById",
            data: '{idParcela: ' + idParcela + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: RetornoParcelaById,
            error: function (xhr, textStatus, err) {
                console.log("readyState: " + xhr.readyState);
                console.log("responseText: " + xhr.responseText);
                console.log("status: " + xhr.status);
                console.log("text status: " + textStatus);
                console.log("error: " + err);
            }
        });
    };

    function RetornoParcelaById(response) {
        var tr;
        var json = $.parseJSON(response.d);

        for (var i = 0; i < json.TableParcelaById.length; i++) {
            document.getElementById('<%= hdnParcela.ClientID%>').value = json.TableParcelaById[i].IdParcela;
            document.getElementById('<%= txtVencimentoParcela.ClientID%>').value = json.TableParcelaById[i].DataVencimento;
            document.getElementById('<%= txtValorPagamento.ClientID%>').value = json.TableParcelaById[i].ValorParcela;
            document.getElementById('<%= hdnIdTipoPagamento.ClientID%>').value = json.TableParcelaById[i].TipoPagamento;

            if (json.TableParcelaById[i].DataPagamento == "-") {
                document.getElementById('<%= txtDataPagamentoParcela.ClientID%>').value = "";
                desbloqueiaCampos();
            } else {
                document.getElementById('<%= txtDataPagamentoParcela.ClientID%>').value = json.TableParcelaById[i].DataPagamento;
                bloqueiaCampos();
            }
        };
        document.getElementById('<%= divErroModal.ClientID%>').style.display = 'none';
        $('.selectpicker').selectpicker('refresh');
        waitingDialog.hide('Aguarde...');

        verificaUltimaParcela();
    }
    function MantemModal() {
        OpenModal('dialogParcela', true);
        document.getElementById('<%= divErroModal.ClientID%>').style.display = 'none';

        var tipoPagamento = document.getElementById('<%= hdnIdTipoPagamento.ClientID%>').value
        document.getElementById('<%= drpTipoPagamento.ClientID%>').value = tipoPagamento;
        $('.selectpicker').selectpicker('refresh');

        var valorParcela = document.getElementById('<%= txtValorPagamento.ClientID%>').value;
            valorParcela = valorParcela.replace('R$', "");
            document.getElementById('<%= txtValorPagamento.ClientID%>').value = valorParcela;
        }

        function verificaUltimaParcela() {
            document.getElementById('<%=btnVerificaUltimaParcela.ClientID%>').click();
        }

        function bloqueiaValorUltimaParcela() {
            MantemModal();
            $('#<%= txtValorPagamento.ClientID%>').attr('readonly', 'readonly');
         var valorParcela = document.getElementById('<%= txtSaldoDevedor.ClientID%>').value
         document.getElementById('<%= txtValorPagamento.ClientID%>').value = valorParcela;
         $('#<%= txtDataPagamentoParcela.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
         $('#<%= txtDataPagamentoParcela.ClientID%>').mask("99/99/9999");
     }

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
             var val = document.getElementById('<%= hdnIdPagamento.ClientID%>').value;
                GetParcelas(val);
            }
        };

     function BloqueiaPagamento() {
        $('#<%= btnGerarParcelas.ClientID%>').hide();
        $('#btnRefatorar').show();
    };

    function BloqueiaRefatoracao() {
        $('#btnRefatorar').hide();
    }

    function BloqueiaRefatoracaoErro() {
        $('#btnRefatorar').hide();
        $('#alertRegistros').show();
    }

    function bloqueiaCampos() {
        $('#<%= btnGravar.ClientID%>').hide();
        $('#<%= txtValorPagamento.ClientID%>').attr('readonly', 'readonly');
        $('#<%= drpTipoPagamento.ClientID%>').prop('disabled', true);
        $('#<%= txtDataPagamentoParcela.ClientID%>').attr('readonly', 'readonly');
        $('#<%= txtDataPagamentoParcela.ClientID%>').datepicker("destroy");
        $('#<%= txtDataPagamentoParcela.ClientID%>').unbind();
    };

        function desbloqueiaCampos() {
            $('#btnGravar').show();
            $('#<%= txtValorPagamento.ClientID%>').attr('readonly', false);
            $('#<%= drpTipoPagamento.ClientID%>').prop('disabled', false);
            $('#<%= txtDataPagamentoParcela.ClientID%>').attr('readonly', false);
            $('#<%= txtDataPagamentoParcela.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%= txtDataPagamentoParcela.ClientID%>').mask("99/99/9999");
        };

        function EstornaParcela(idParcela) {
            document.getElementById('<%= hdnParcela.ClientID%>').value = idParcela;
            OpenModal("dialogEstorno");
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Pagamento do Pedido
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">

    <div class="box">
        <br />
        <div class="col-md-12">
            <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="btn btn-primary" Text="Voltar" />
        </div>
        <!-- /.box-header -->
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Image CssClass="img-rouded" ID="imgCliente" Height="85%" Width="85%" runat="server" BackColor="#999999" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Nome</label>
                        <asp:TextBox ID="txtNome" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>CPF</label>
                        <asp:TextBox ID="txtCPF" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>E-mail</label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Telefone Principal</label>
                        <asp:TextBox ID="txtTelefone" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Data do Pedido</label>
                        <asp:TextBox ID="txtDataPedido" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Data do 1º Pagamento
                        <asp:RequiredFieldValidator ID="validaTxtDataInicial" ValidationGroup="gerarParcela" runat="server" ControlToValidate="txtdataInicial" InitialValue="" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                        </label>
                        <asp:TextBox ID="txtDataInicial" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Desconto (em %)</label>
                        <asp:TextBox ID="txtDesconto" placeHolder="Informe um valor em Percentual." CssClass="form-control" onchange="calculaValorFinal();" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Nº de Parcelas
                        <asp:RequiredFieldValidator ID="ValidaTxtNumeroDeParcelas" ValidationGroup="gerarParcela" runat="server" ControlToValidate="txtNumeroDeParcelas" InitialValue="" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                        </label>
                        <asp:TextBox ID="txtNumeroDeParcelas" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Valor do Pedido</label>
                        <asp:TextBox ID="txtValorPedido" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Total à Pagar</label>
                        <asp:TextBox ID="txtValorFinal" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Saldo Devedor</label>
                        <asp:TextBox ID="txtSaldoDevedor" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Observação</label>
                        <asp:TextBox ID="txtObservacao" placeHolder="Observações sobre este Pagamento." CssClass="form-control" MaxLength="400" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12 ">
                    <div class="form-group">
                        <asp:Button ID="btnGerarParcelas" runat="server" OnClick="btnGerarParcelas_Click" ValidationGroup="gerarParcela" CssClass="btn btn-primary" Text="Gerar Parcelas" />
                        <asp:HiddenField ID="hdnIdPedido" runat="server" />
                        <asp:HiddenField ID="hdnIdPessoa" runat="server" />
                        <asp:HiddenField ID="hdnIdPagamento" runat="server" />
                        <asp:HiddenField ID="hdnIdTipoPagamento" runat="server" />
                    </div>
                </div>
                <div class="col-md-12">
                    <a href="javascript:OpenModal('dialogRefatorar');" id="btnRefatorar" class="btn btn-primary margin ignore">Refatorar Parcelas</a>
                </div>

                <div class="col-md-12" id="divModalErro" runat="server">
                    <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
                    <div class="alert alert-warning">
                        <asp:Label ID="lblErro" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body table-responsive">
        <div class="row ">
            <div class="box-body">
                <div id="divParcelas"></div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.row -->
        <div class="alert alert-info fade in" id="alertRegistros" style="display: none;" role="alert">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Informação:</span>
            Não há Parcelas cadastradas. 
        </div>
    </div>

    <div class="modal" id="dialogParcela" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
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
                                        <legend>Dados da Parcela</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Data do Vencimento</label>
                                                    <asp:TextBox ID="txtVencimentoParcela" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnParcela" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Data do Pagamento
                                                        <asp:RequiredFieldValidator ID="validaTxtDataPagamentoParcela" ValidationGroup="Parcela" InitialValue="" runat="server" ControlToValidate="txtDataPagamentoParcela" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtDataPagamentoParcela" placeholder="dd/mm/aaaa" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Valor do Pagamento
                                                        <asp:RequiredFieldValidator ID="ValidaTxtValorPagamento" ValidationGroup="Parcela" InitialValue="" runat="server" ControlToValidate="txtValorPagamento" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtValorPagamento" placeholder="0,00" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Pago com
                                                        <asp:RequiredFieldValidator ID="validaDrpTipoPagamento" ValidationGroup="Parcela" InitialValue="0" runat="server" ControlToValidate="drpTipoPagamento" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="drpTipoPagamento" CssClass="form-control input-sm selectpicker btn-dropdown" data-live-search="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divErroModal" runat="server">
                                                <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
                                                <div class="alert alert-warning">
                                                    <asp:Label ID="lblErroModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12 ">
                                                <div class="form-group">
                                                    <asp:Button ID="btnGravar" runat="server" ValidationGroup="Parcela" OnClick="btnGravar_Click" CssClass="btn btn-primary" Text="Gravar" />
                                                    <asp:Button ID="btnVerificaUltimaParcela" runat="server" CssClass="hidden" Text="ultimaParcela" OnClick="btnVerificaUltimaParcela_Click" CausesValidation="false" />
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
                    <h4 class="modal-title">Cadastro de Parcelas</h4>
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

    <div class="modal fade dialogDel" id="dialogRefatorar" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Refatorar Parcelas</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info ">Você realmente deseja refatorar as Parcelas que já foram geradas? </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnConfirmRefatorarParcelas" class="btn btn-primary" runat="server" Text="Sim" OnClick="btnConfirmRefatorarParcelas_Click"></asp:Button>
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

    <div class="modal fade dialogDel" id="dialogEstorno" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Estornar Parcela</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info ">Você realmente deseja estornar esta Parcela Paga? </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnConfirmaEstorno" class="btn btn-primary" runat="server" Text="Sim" OnClick="btnConfirmaEstorno_Click"></asp:Button>
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
