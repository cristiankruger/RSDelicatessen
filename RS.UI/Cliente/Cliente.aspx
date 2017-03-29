<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cliente.aspx.vb" Inherits="RS.UI.Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {

            $('#<%= txtNomeCliente.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/1234567890]/g, '');
                $(this).val(char);
            });

            $('#<%= txtBairro.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
            $('#<%= txtCidade.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
            $('#<%= txtRG.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
            $('#<%= txtLogradouro.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });

            $('#<%= txtEmail.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*()+=",ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
            
            $('#<%= txtCPF.ClientID%>').mask("999.999.999-99");
            $('#<%= txtCEP.ClientID%>').mask("99.999-999");
            $('#<%= txtNumero.ClientID%>').mask("9999999");


            var SPMaskBehavior = function (val) {
                return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
            }, spOptions = {
                onKeyPress: function (val, e, field, options) {
                    field.mask(SPMaskBehavior.apply({}, arguments), options);
                }
            };
            $('#<%= txtTel1.ClientID%>').mask(SPMaskBehavior, spOptions);
            $('#<%= txtTel2.ClientID%>').mask(SPMaskBehavior, spOptions);
            $('#<%= txtTel3.ClientID%>').mask(SPMaskBehavior, spOptions);

            $('.selectpicker').selectpicker('refresh');

        }

        function LimpaHiddenContatos() {
            document.getElementById('<%= hdnIdContato1.ClientID%>').value = "";
            document.getElementById('<%= hdnIdContato2.ClientID%>').value = "";
            document.getElementById('<%= hdnIdContato3.ClientID%>').value = "";
        }

        function limparCampos() {
            document.getElementById('<%= pAcao.ClientID %>').value = "Inserir";
            document.getElementById('<%= txtNomeCliente.ClientID%>').value = "";
            document.getElementById('<%= txtCPF.ClientID%>').value = "";
            document.getElementById('<%= txtRG.ClientID%>').value = "";
            document.getElementById('<%= txtEmail.ClientID%>').value = "";
            document.getElementById('<%= txtCEP.ClientID%>').value = "";
            document.getElementById('<%= txtLogradouro.ClientID%>').value = "";
            document.getElementById('<%= txtNumero.ClientID%>').value = "";
            document.getElementById('<%= txtComplemento.ClientID%>').value = "";
            document.getElementById('<%= txtBairro.ClientID%>').value = "";
            document.getElementById('<%= txtCidade.ClientID%>').value = "";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%= drpUF.ClientID%>').value = "0";
            document.getElementById('<%= txtTel1.ClientID%>').value = "";
            document.getElementById('<%= txtTel2.ClientID%>').value = "";
            document.getElementById('<%= txtTel3.ClientID%>').value = "";
            document.getElementById('<%= hdnImgCliente.ClientID%>').value = "";
            document.getElementById('<%= imgCliente.ClientID%>').setAttribute('src', '/Foto/FotoCliente/semImagem.jpg');
            LimpaHiddenContatos();
            $('.selectpicker').selectpicker('refresh');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Cliente";
            CloseValidationSummary();
            OpenModal("dialogCliente");
        }

        function CloseValidationSummary() {
            <%--document.getElementById('<%= ValidationSummary.ClientID%>').style.display = 'none';--%>
            document.getElementById('<%= validaDrpUF.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtNomeCliente.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtBairro.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtCidade.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtCPF.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validatxtLogradouro.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtTel1.ClientID%>').style.visibility = "hidden";
        }

        function GetContatos(idPessoa) {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Cliente.aspx/TableContato",
                data: '{idPessoa: ' + idPessoa + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoContatos,
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

        function RetornoContatos(response) {
            var json = $.parseJSON(response.d);

            document.getElementById('<%= hdnIdContato1.ClientID%>').value = json.TableContato[0].IdContato;
            document.getElementById('<%= txtTel1.ClientID%>').value = json.TableContato[0].Telefone;
            document.getElementById('<%= hdnIdContato2.ClientID%>').value = json.TableContato[1].IdContato;
            document.getElementById('<%= txtTel2.ClientID%>').value = json.TableContato[1].Telefone;
            document.getElementById('<%= hdnIdContato3.ClientID%>').value = json.TableContato[2].IdContato;
            document.getElementById('<%= txtTel3.ClientID%>').value = json.TableContato[2].Telefone;
        };

        function GetCliente() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Cliente.aspx/TableCliente",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoCliente,
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

        function RetornoCliente(response) {
            var tr;
            var json = $.parseJSON(response.d);

            $("#divCliente").empty();

            if (response.d == 0) {
                $('#alertRegistros').show();
            }
            else {
                var html = '<table id="tbCliente" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Nome</th> \
                <th>Email</th> \
                <th>Telefone</th> \
                <th>CPF</th> \
                <th>Descrição Data</th> \
                <th>Próxima Data Especial</th> \
                <th>Anos</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

                $('#divCliente').append(html);


                for (var i = 0; i < json.TableCliente.length; i++) {

                    tr = $('<tr/>');

                    tr.append("<td>" + json.TableCliente[i].Acao + "</td>")
                    tr.append("<td>" + json.TableCliente[i].Nome + "</td>");
                    tr.append("<td>" + json.TableCliente[i].Email + "</td>");
                    tr.append("<td>" + json.TableCliente[i].Telefone + "</td>");
                    tr.append("<td>" + json.TableCliente[i].CPF + "</td>");
                    tr.append("<td>" + json.TableCliente[i].Descricao + "</td>");
                    tr.append("<td>" + json.TableCliente[i].DataComemorativa + "</td>");
                    tr.append("<td>" + json.TableCliente[i].Anos + "</td>");

                    $('#tbCliente').append(tr);

                }
                $('#tbCliente').dataTable();
            }
            waitingDialog.hide('Aguarde...')
        };

        function PopulaCliente(idPessoa) {
            waitingDialog.show('Aguarde...');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Alterar Cliente";
            $.ajax({
                type: "POST",
                url: "Cliente.aspx/PopulaClienteById",
                data: '{idPessoa: ' + idPessoa + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoClienteById,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornoClienteById(response) {
            var tr;
            var json = $.parseJSON(response.d);

            for (var i = 0; i < json.TableClienteById.length; i++) {
                document.getElementById('<%= txtNomeCliente.ClientID%>').value = json.TableClienteById[i].Nome;
                document.getElementById('<%= txtEmail.ClientID%>').value = json.TableClienteById[i].Email;
                document.getElementById('<%= txtBairro.ClientID%>').value = json.TableClienteById[i].Bairro;
                document.getElementById('<%= txtCEP.ClientID%>').value = json.TableClienteById[i].CEP;
                document.getElementById('<%= txtCidade.ClientID%>').value = json.TableClienteById[i].Cidade;
                document.getElementById('<%= txtComplemento.ClientID%>').value = json.TableClienteById[i].Complemento;
                document.getElementById('<%= txtCPF.ClientID%>').value = json.TableClienteById[i].CPF;
                document.getElementById('<%= hdnIdPessoa.ClientID%>').value = json.TableClienteById[i].IdPessoa;
                document.getElementById('<%= txtLogradouro.ClientID%>').value = json.TableClienteById[i].Logradouro;
                document.getElementById('<%= txtNumero.ClientID%>').value = json.TableClienteById[i].NumeroResidencia;
                document.getElementById('<%= txtRG.ClientID%>').value = json.TableClienteById[i].RG;
                document.getElementById('<%= drpUF.ClientID%>').value = json.TableClienteById[i].IdUF;
                document.getElementById('<%= imgCliente.ClientID%>').src = json.TableClienteById[i].Foto;
                document.getElementById('<%= hdnImgCliente.ClientID%>').value = json.TableClienteById[i].Foto;
            }
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById("conteudoPagina_pAcao").value = "Alterar";
            $('.selectpicker').selectpicker('refresh');
            waitingDialog.hide('Aguarde...')
            OpenModal('dialogCliente');
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

                GetCliente();
            }
        }
        function ExcluiCliente(hdnIdPessoa) {

            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdPessoa.ClientID%>').value = hdnIdPessoa;
            OpenModal("dialogDel");
        }

        var loadFile = function (event) {
            var output = document.getElementById('<%= imgCliente.ClientID%>');
            output.src = URL.createObjectURL(event.target.files[0]);
            document.getElementById('<%= hdnImgCliente.ClientID%>').value = output.src;
        };               
    </script>
    <style type="text/css">
        .image-box {}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Cadastro de Clientes
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="box">
        <!-- /.box-header -->
        <div class="box-body table-responsive">
            <div class="col-md-12">
                <a href="javascript:limparCampos();" class="btn btn-primary margin">Novo Cliente</a>
            </div>

            <div class="row ">
                <div class="box-body">
                    <div id="divCliente"></div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.row -->
            <div class="alert alert-info fade in" id="alertRegistros" style="display: none;" role="alert">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Informação:</span>
                Não há Clientes cadastrados. 
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->


    <!-- Modal Documento -->
    <div class="modal dialogFull" id="dialogCliente" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
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

                                        <legend>Dados do Cliente</legend>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div>
                                                        <label>Foto do Cliente</label></div>
                                                    <div>
                                                        <asp:Image CssClass="img-rouded"  ID="imgCliente"  Height="350px" Width="350px" runat="server" BackColor="#999999" />
                                                        <asp:HiddenField runat="server" ID="hdnImgCliente" />
                                                    </div>
                                                    <div>
                                                        <asp:FileUpload CssClass="btn btn-primary" OnChange="loadFile(event);" ID="FileUpload" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Nome<asp:RequiredFieldValidator ID="validaTxtNomeCliente" runat="server" ErrorMessage="Informe o Nome do Cliente."
                                                            ControlToValidate="txtNomeCliente" MaxLength="100" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtNomeCliente" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnIdPessoa" runat="server" />
                                                    <asp:HiddenField ID="hdnIdContato1" runat="server" />
                                                    <asp:HiddenField ID="hdnIdContato2" runat="server" />
                                                    <asp:HiddenField ID="hdnIdContato3" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        CPF<asp:RequiredFieldValidator ID="validaTxtCPF" runat="server" ErrorMessage="Informe o CPF do Cliente."
                                                            ControlToValidate="txtCPF" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtCPF" placeholder="informe somente números." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>RG</label>
                                                    <asp:TextBox ID="txtRG" placeholder="informe o RG." MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>E-mail</label>
                                                    <asp:TextBox ID="txtEmail" placeholder="informe o E-mail." MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Telefone Principal<asp:RequiredFieldValidator ID="validaTxtTel1" runat="server" ErrorMessage="Informe o telefone do Cliente."
                                                            ControlToValidate="txtTel1" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtTel1" placeholder="informe somente números." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Telefone Secundário</label>
                                                    <asp:TextBox ID="txtTel2" placeholder="informe somente números." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Telefone para Recado</label>
                                                    <asp:TextBox ID="txtTel3" placeholder="informe somente números." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>CEP</label>
                                                    <asp:TextBox ID="txtCEP" placeholder="informe somente números." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Logradouro<asp:RequiredFieldValidator ID="validatxtLogradouro" runat="server" ErrorMessage="Informe o endereço do Cliente."
                                                            ControlToValidate="txtLogradouro" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtLogradouro" MaxLength="150" placeholder="informe o endereço do cliente." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Número</label>
                                                    <asp:TextBox ID="txtNumero" placeholder="informe número do endereço." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Complemento</label>
                                                    <asp:TextBox ID="txtComplemento" MaxLength="50" placeholder="informe o complemento do endereço." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Bairro<asp:RequiredFieldValidator ID="validaTxtBairro" runat="server" ErrorMessage="Informe o Bairro."
                                                            ControlToValidate="txtBairro" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtBairro" MaxLength="50" placeholder="informe o Bairro." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Cidade<asp:RequiredFieldValidator ID="validaTxtCidade" runat="server" ErrorMessage="Informe a Cidade."
                                                            ControlToValidate="txtCidade" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtCidade" MaxLength="50" placeholder="informe a cidade." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                           
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        UF<asp:RequiredFieldValidator ID="validaDrpUF" runat="server" ErrorMessage="Selecione a UF."
                                                            ControlToValidate="drpUF" InitialValue="0" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:DropDownList ID="drpUF" CssClass="form-control input-sm selectpicker btn-dropdown" data-live-search="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-12"  id="divModalErro"  runat="server">
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
                    <h4 class="modal-title">Cadastro de Cliente</h4>
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
                    <h4 class="modal-title">Excluir Cliente</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <input id="hdnIdAtividade" runat="server" type="hidden" />
                            <div class="alert alert-info ">Você realmente deseja excluir este Cliente? </div>
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
