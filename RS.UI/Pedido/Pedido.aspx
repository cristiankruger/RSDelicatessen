<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pedido.aspx.vb" Inherits="RS.UI.Pedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {
            $('#<%= txtCPF.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtDataValidade.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtEmail.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtTelPrincipal.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtTelSecundario.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtTelRecado.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtTotalPedido.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtValorItem.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtDataPedido.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%= txtDataValidade.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
            $('#<%= txtDataPedido.ClientID%>').mask("99/99/9999");
            $('#<%= txtDataValidade.ClientID%>').mask("99/99/9999");
            $('#<%= drpPedido.ClientID%>').attr('disabled', false);
            $('#<%= txtQuantidade.ClientID%>').keyup(function () {
                var RegExpression = /^([1-9]|[1-9][0-9]|[1-9][0-9][0-9])$/;
                if (RegExpression.test($('#<%= txtQuantidade.ClientID%>').val())) {
                    $('#<%= txtQuantidade.ClientID%>').val();
                }
                else {
                    $('#<%= txtQuantidade.ClientID%>').val("1");
                }
            });
            document.getElementById('<%= txtQuantidade.ClientID%>').value = "1";
            $('.selectpicker').selectpicker('render');
        }

        function verificaPedido() {
            var pedido = $('#<%= drpPedido.ClientID%>').val();
            if (pedido == 1) {
                $('#<%= txtDataValidade.ClientID%>').attr('readonly', false);
                $('#<%= txtDataValidade.ClientID%>').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
                $('#<%= txtDataValidade.ClientID%>').datepicker('show');
            } else {
                $('#<%= txtDataValidade.ClientID%>').datepicker("destroy");
                $('#<%= txtDataValidade.ClientID%>').unbind();
                $('#<%= txtDataValidade.ClientID%>').attr('readonly', 'readonly');
                $('#<%= txtDataValidade.ClientID%>').val("");
            }
        }

        function limparCampos() {
            document.getElementById('<%= pAcao.ClientID %>').value = "Inserir";
            document.getElementById('<%= drpCliente.ClientID%>').value = "0";
            document.getElementById('<%= drpItem.ClientID%>').value = "0";
            document.getElementById('<%= drpPedido.ClientID%>').value = "0";
            document.getElementById('<%= txtCPF.ClientID%>').value = "";
            document.getElementById('<%= txtEmail.ClientID%>').value = "";
            document.getElementById('<%= txtTelPrincipal.ClientID%>').value = "";
            document.getElementById('<%= txtTelSecundario.ClientID%>').value = "";
            document.getElementById('<%= txtTelRecado.ClientID%>').value = "";
            document.getElementById('<%= txtDataPedido.ClientID%>').value = "";
            document.getElementById('<%= txtDataValidade.ClientID%>').value = "";
            document.getElementById('<%= txtQuantidade.ClientID%>').value = "1";
            document.getElementById('<%= txtValorItem.ClientID%>').value = "";
            document.getElementById('<%= txtTotalPedido.ClientID%>').value = "";
            document.getElementById('<%= txtDescricao.ClientID%>').value = "";
            document.getElementById('<%= hdnValor.ClientID%>').value = "";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Pedido";
            //$('#alertItem').hide();
            $('#divItem').empty();
            CloseValidationSummary();
            $('.selectpicker').selectpicker('render');
        }

        function CloseValidationSummary() {
            document.getElementById('<%= validaDrpCliente.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%=  validaTxtDataPedido.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%=  validaTxtQuantidade.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
        }

        function GetPedido() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Pedido.aspx/TablePedido",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoPedido,
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

        function RetornoPedido(response) {
            var tr;
            var json = $.parseJSON(response.d);
            $("#divPedido").empty();

            if (response.d == 0) {
                $('#alertRegistros').show();
            }
            else {
                var html = '<table id="tbPedido" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Cliente</th> \
                <th>Telefone</th> \
                <th>Data do Pedido</th> \
                <th>Descrição</th> \
                <th>Feito por</th> \
                <th>Valor</th> \
                <th>Situação</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

                $('#divPedido').append(html);

                for (var i = 0; i < json.TablePedido.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + json.TablePedido[i].Acao + "</td>")
                    tr.append("<td>" + json.TablePedido[i].NomeCliente + "</td>");
                    tr.append("<td>" + json.TablePedido[i].TelefoneCliente + "</td>");
                    tr.append("<td>" + json.TablePedido[i].DataPedido + "</td>");
                    tr.append("<td>" + json.TablePedido[i].Descricao + "</td>");
                    tr.append("<td>" + json.TablePedido[i].NomeFuncionario + "</td>");
                    tr.append("<td>" + json.TablePedido[i].ValorPedido + "</td>");
                    tr.append("<td>" + json.TablePedido[i].SituacaoPedido + "</td>");
                    $('#tbPedido').append(tr);                    
                }
                $('#tbPedido').dataTable();
            }
            waitingDialog.hide('Aguarde...')
        };

        function PopulaTxtCliente(idCliente) {
            PopulaDadosCliente(idCliente);
            PopulaContatos(idCliente);
        }

        function PopulaDadosCliente(idCliente) {
            $.ajax({
                type: "POST",
                url: "Pedido.aspx/TableClienteById",
                data: '{idCliente: ' + idCliente + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornaDadosCliente,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornaDadosCliente(response) {
            document.getElementById('<%= txtEmail.ClientID%>').value = "";
            document.getElementById('<%= txtCPF.ClientID %>').value = "";
            var json = $.parseJSON(response.d);
            document.getElementById('<%= txtCPF.ClientID %>').value = json.TableClienteById[0].CPF;
            if (json.TableClienteById[0].Email != "") {
                document.getElementById('<%= txtEmail.ClientID%>').value = json.TableClienteById[0].Email;
            } else {
                document.getElementById('<%= txtEmail.ClientID%>').value = "Email não Cadastrado.";
            }
        }

        function PopulaContatos(idCliente) {
            $.ajax({
                type: "POST",
                url: "Pedido.aspx/TableContatosById",
                data: '{idCliente: ' + idCliente + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornaDadosContato,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornaDadosContato(response) {
            document.getElementById('<%= txtTelPrincipal.ClientID%>').value = "";
            document.getElementById('<%= txtTelSecundario.ClientID%>').value = "";
            document.getElementById('<%= txtTelRecado.ClientID%>').value = "";

            var json = $.parseJSON(response.d);
            document.getElementById('<%= txtTelPrincipal.ClientID%>').value = json.TableContato[0].Telefone;
            if (json.TableContato[1].Telefone != "") {
                document.getElementById('<%= txtTelSecundario.ClientID%>').value = json.TableContato[1].Telefone;
            } else {
                document.getElementById('<%= txtTelSecundario.ClientID%>').value = "Telefone não cadastrado.";
            }
            if (json.TableContato[2].Telefone != "") {
                document.getElementById('<%= txtTelRecado.ClientID%>').value = json.TableContato[2].Telefone;
            } else {
                document.getElementById('<%= txtTelRecado.ClientID%>').value = "Telefone não cadastrado.";
            }
        }

        function PopulaTxtValor(idProduto) {
            $.ajax({
                type: "POST",
                url: "Pedido.aspx/TableItemById",
                data: '{idProduto: ' + idProduto + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornaValorProduto,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornaValorProduto(response) {
            var json = $.parseJSON(response.d);
            document.getElementById('<%= txtValorItem.ClientID%>').value = json.TableProdutoById[0].Valor;
            document.getElementById('<%= hdnValor.ClientID%>').value = json.TableProdutoById[0].Valor;
        }

        function CalculaValorItem() {
            var valorItem = document.getElementById('<%= hdnValor.ClientID%>').value;
            valorItem = parseFloat(valorItem.replace(".", "").replace(",", "."));

            var quantidade = document.getElementById('<%= txtQuantidade.ClientID%>').value
            quantidade = parseFloat(quantidade)

            if ((isNaN(quantidade) != true) && (isNaN(valorItem) != true)) {
                var total = quantidade * valorItem;
                total = parseFloat(total).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                total = total.replace('.', '*').replace(',', '.').replace('*', ',');
                document.getElementById('<%= txtValorItem.ClientID%>').value = total;
            }
        }

        function ExcluiPedido(hdnIdPedido) {
            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdPedido.ClientID%>').value = hdnIdPedido;
            OpenModal("dialogDel");
        }

        function MantemModal() {
            OpenModal('dialogPedido', true);
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            $('.selectpicker').selectpicker('refresh');
        }

       function LimpaSession() {
           document.getElementById("btnLimpaSession").click();
       }

       function ExcluiItemPedido(idItemPedido) {
           waitingDialog.show('Aguarde...')
           document.getElementById('<%= hdnIdItemPedido.ClientID%>').value = idItemPedido;
           OpenModal("dialogDelItem");
       }

       function PopulaGrid() {
           waitingDialog.show('Aguarde...');
           $.ajax({
               type: "POST",
               url: "Pedido.aspx/PopulaGrid",
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: RetornoGridItem,
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

       function RetornoGridItem(response) {
           var tr;
           var json = $.parseJSON(response.d);

           if (response.d == 0) {
               $('#alertItem').show();
           }
           else {
               $("#divItem").empty();

               var html = '<table id="tbItemPedido" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th style="width: 68.0909091234207px;"></th> \
                <th>Item</th> \
                <th>Quantidade</th> \
                <th>Valor</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';
               $('#divItem').append(html);

               for (var i = 0; i < json.TableItem.length; i++) {
                   tr = $('<tr/>');
                   tr.append("<td>" + json.TableItem[i].Botao + "</td>");
                   tr.append("<td>" + json.TableItem[i].Item + "</td>");
                   tr.append("<td>" + json.TableItem[i].Quantidade + "</td>");
                   tr.append("<td>" + json.TableItem[i].Valor + "</td>");
                   $('#tbItemPedido').append(tr);
               }
               $('#tbItemPedido').dataTable();
           }
           $('.selectpicker').selectpicker('refresh');
           waitingDialog.hide('Aguarde...');
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
               GetPedido();
           }
       }

       function PopulaDadosAlteracao(idPedido) {
           waitingDialog.show('Aguarde...')
           document.getElementById('<%= hdnIdPedido.ClientID%>').value = idPedido;
           document.getElementById('<%=btnAlterar.ClientID%>').click();
       }

        function bloqueiaCampos() {
            $('#<%= txtDataPedido.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtQuantidade.ClientID%>').attr('readonly', 'readonly');
            $('#<%= txtDescricao.ClientID%>').attr('readonly', 'readonly');
            $('#<%= drpCliente.ClientID%>').attr('disabled', 'disabled');
            $('#<%= drpPedido.ClientID%>').attr('disabled', 'disabled');
            $('#<%= drpItem.ClientID%>').attr('disabled', 'disabled');
            $('#btnExcluir').hide();
            $('#<%= btnAdd.ClientID%>').hide();
            $('#<%= btnGravar.ClientID%>').hide();
        }

        function desBloqueiaCampos() {
            $('#<%= txtDataPedido.ClientID%>').attr('readonly', false);
            $('#<%= txtQuantidade.ClientID%>').attr('readonly', false);
            $('#<%= txtDescricao.ClientID%>').attr('readonly', false);
            $('#<%= drpCliente.ClientID%>').attr('disabled', false);
            $('#<%= drpPedido.ClientID%>').attr('disabled', false);
            $('#<%= drpItem.ClientID%>').attr('disabled', false);
            $('#<%= btnAdd.ClientID%>').show();
            $('#<%= btnGravar.ClientID%>').show();
        }

        function EstornaPedido(idPedido) {
            document.getElementById('<%= hdnIdPedido.ClientID%>').value = idPedido;
            <%--$('#<%= btnVerificaParcelasPagas.ClientID%>').click();--%>
            document.getElementById('<%=btnVerificaParcelasPagas.ClientID%>').click();
        }

       <%-- function VerificaParcelasPagas(idPedido) {
            $('#<%= btnVerificaParcelasPagas.ClientID%>').click();
            EstornaPedido(idPedido);
        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Cadastro de Pedidos
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="box">
        <!-- /.box-header -->
        <div class="box-body table-responsive">
            <div class="col-md-12">
                <a href="javascript:limparCampos(); MantemModal();" class="btn btn-primary margin">Novo Pedido</a>
            </div>

            <div class="row ">
                <div class="box-body">
                    <div id="divPedido"></div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.row -->
            <div class="alert alert-info fade in" id="alertRegistros" style="display: none;" role="alert">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Informação:</span>
                Não há Pedidos cadastrados. 
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->


    <!-- Modal Documento -->
    <div class="modal dialogFull" id="dialogPedido" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" onclick="LimpaSession();" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblModal" runat="server" Enabled="false" /></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset>

                                        <legend>Dados do Pedido</legend>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>CPF</label>
                                                    <asp:TextBox ID="txtCPF" placeholder="preenchimento automático" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>E-mail</label>
                                                    <asp:TextBox ID="txtEmail" placeholder="preenchimento automático" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Telefone Principal</label>
                                                    <asp:TextBox ID="txtTelPrincipal" placeholder="preenchimento automático" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Telefone Secundário</label>
                                                    <asp:TextBox ID="txtTelSecundario" placeholder="preenchimento automático" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Telefone para Recado</label>
                                                    <asp:TextBox ID="txtTelRecado" placeholder="preenchimento automático" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Cliente
                                                        <asp:RequiredFieldValidator ID="validaDrpCliente" ValidationGroup="cliente" runat="server" ControlToValidate="drpCliente" InitialValue="0" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="drpCliente" CssClass="form-control input-sm selectpicker btn-dropdown" data-live-search="true" runat="server" onchange="PopulaTxtCliente(this.value);"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Data do Pedido<asp:RequiredFieldValidator ID="validaTxtDataPedido" ValidationGroup="cliente" runat="server" ControlToValidate="txtDataPedido" InitialValue="" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtDataPedido" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>
                                                        Tipo de pedido
                                                        <asp:RequiredFieldValidator ID="validaDrpPedido" ValidationGroup="cliente" runat="server" ControlToValidate="drpPedido" InitialValue="0" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="drpPedido" CssClass="form-control input-sm selectpicker btn-dropdown" data-live-search="true" runat="server" onchange="verificaPedido();"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Validade do Orçamento</label>
                                                    <asp:TextBox ID="txtDataValidade" type="date" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Itens do Pedido</label>
                                                    <fieldset>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>
                                                                    Item
                                                        <asp:RequiredFieldValidator ID="validaDrpItem" ValidationGroup="Item" runat="server" ControlToValidate="drpItem" InitialValue="0" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                                </label>
                                                                <asp:DropDownList ID="drpItem" CssClass="form-control input-sm selectpicker btn-dropdown" data-live-search="true" runat="server" onchange="PopulaTxtValor(this.value); CalculaValorItem(); "></asp:DropDownList>
                                                                <asp:HiddenField ID="hdnValor" runat="server" />
                                                                <asp:HiddenField ID="hdnBoolPedido" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    Quantidade
                                                        <asp:RequiredFieldValidator ID="validaTxtQuantidade" ValidationGroup="Item" runat="server" ControlToValidate="txtQuantidade" InitialValue="" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                                </label>
                                                                <asp:TextBox ID="txtQuantidade" onChange="CalculaValorItem();" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>Valor Item</label>
                                                                <asp:TextBox ID="txtValorItem" placeholder="preenchimento automático" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:Button ID="btnLimpaSession" runat="server" CssClass="hidden" OnClick="btnLimpaSession_Click" CausesValidation="false" ClientIDMode="Static" />
                                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" ValidationGroup="Item" CssClass="btn btn-primary" Text="Adicionar" />
                                                                <asp:HiddenField ID="hdnIdItemPedido" runat="server" />

                                                                <asp:Button ID="btnAlterar" class="btn btn-primary" runat="server" Text="Sim" OnClick="btnAlterar_Click" Style="visibility: hidden" CausesValidation="false"></asp:Button>
                                                            </div>
                                                        </div>

                                                        <div class="row ">
                                                            <div class="box-body">
                                                                <div id="divItem">
                                                                    <asp:Literal ID="ltrTableItem" runat="server"></asp:Literal>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group">
                                                                        <label>Total Pedido</label>
                                                                        <asp:TextBox ID="txtTotalPedido" placeholder="R$0,00" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="alert alert-info fade in" id="alertItem" style="display: none;" role="alert">
                                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                            <span class="sr-only">Informação:</span>
                                                            Não há itens neste Pedido! 
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Observação</label>
                                                    <asp:TextBox ID="txtDescricao" CssClass="form-control" placeholder="Informe alguma observação do pedido" MaxLength="200" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                                                    <asp:Button ID="btnGravar" runat="server" OnClick="btnGravar_Click" ValidationGroup="cliente" CssClass="btn btn-primary" Text="Gravar" />
                                                    <input id="pAcao" runat="server" type="hidden" />
                                                    <asp:HiddenField ID="hdnIdPedido" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.row -->
                                    </fieldset>
                                </div>
                            </div>

                            <div class="modal-footer ">
                                <a class="close" data-dismiss="modal" onclick="LimpaSession();" href="#">Fechar</a>
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
                    <h4 class="modal-title">Cadastro de Pedido</h4>
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
                    <h4 class="modal-title">Excluir Pedido</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info ">Você realmente deseja excluir este Pedido? </div>
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
        
    <div class="modal fade dialogDel" id="dialogEstornaPedido" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancelar Pedido</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info "><asp:Label ID="lblCancelaPedido" runat="server"></asp:Label> </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnVerificaParcelasPagas" class="btn btn-primary hidden" Text="verificaParcelasPagas" runat="server" OnClientClick="btnVerificaParcelasPagas_Click" CausesValidation="false" ></asp:Button>
                                <asp:Button ID="btnConfirmaEstorno" class="btn btn-primary" runat="server" Text="Sim" OnClick="btnConfirmaEstorno_Click" CausesValidation="false"></asp:Button>
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

    <div class="modal fade dialogDel" id="dialogDelItem" tabindex="-1" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Excluir Item</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info ">Você realmente deseja excluir este Item? </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnConfirmaExclusaoItem" class="btn btn-primary" runat="server" Text="Sim" OnClick="btnConfirmaExclusaoItem_Click" CausesValidation="false"></asp:Button>
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
