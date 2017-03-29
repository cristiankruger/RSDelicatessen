<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Funcionario.aspx.vb" Inherits="RS.UI.Funcionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {

            $('#<%= txtNomeFuncionario.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/1234567890]/g, '');
                $(this).val(char);
            });

            $('#<%= txtUsuario.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/1234567890]/g, '');
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
            document.getElementById('<%= txtNomeFuncionario.ClientID%>').value = "";
            document.getElementById('<%= txtCPF.ClientID%>').value = "";
            document.getElementById('<%= txtRG.ClientID%>').value = "";
            document.getElementById('<%= txtEmail.ClientID%>').value = "";
            document.getElementById('<%= txtCEP.ClientID%>').value = "";
            document.getElementById('<%= txtLogradouro.ClientID%>').value = "";
            document.getElementById('<%= txtNumero.ClientID%>').value = "";
            document.getElementById('<%= txtComplemento.ClientID%>').value = "";
            document.getElementById('<%= txtBairro.ClientID%>').value = "";
            document.getElementById('<%= txtUsuario.ClientID%>').value = "";
            document.getElementById('<%= txtCidade.ClientID%>').value = "";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%= drpPerfil.ClientID%>').value = "0";
            document.getElementById('<%= drpUF.ClientID%>').value = "0";
            document.getElementById('<%= txtTel1.ClientID%>').value = "";
            document.getElementById('<%= txtTel2.ClientID%>').value = "";
            document.getElementById('<%= txtTel3.ClientID%>').value = "";
            LimpaHiddenContatos();
            $('.selectpicker').selectpicker('refresh');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Funcionário";
            CloseValidationSummary();
            OpenModal("dialogFuncionario");
        }

        function CloseValidationSummary() {
            <%--document.getElementById('<%= ValidationSummary.ClientID%>').style.display = 'none';--%>
            document.getElementById('<%= validaDrpUF.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaDrpPerfil.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtNomeFuncionario.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtBairro.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtCidade.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtCPF.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validatxtLogradouro.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtTel1.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtUsuario.ClientID%>').style.visibility = "hidden";
        }

        function GetContatos(idPessoa) {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Funcionario.aspx/TableContato",
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

        function GetFuncionario() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Funcionario.aspx/TableFuncionario",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoFuncionario,
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

        function RetornoFuncionario(response) {
            var tr;
            var json = $.parseJSON(response.d);

            $("#divFuncionario").empty();


            var html = '<table id="tbFuncionario" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Nome</th> \
                <th>Email</th> \
                <th>Usuario</th> \
                <th>Perfil</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

            $('#divFuncionario').append(html);


            for (var i = 0; i < json.TableFuncionario.length; i++) {

                tr = $('<tr/>');

                tr.append("<td>" + json.TableFuncionario[i].Acao + "</td>")
                tr.append("<td>" + json.TableFuncionario[i].Nome + "</td>");
                tr.append("<td>" + json.TableFuncionario[i].Email + "</td>");
                tr.append("<td>" + json.TableFuncionario[i].Usuario + "</td>");
                tr.append("<td>" + json.TableFuncionario[i].Descricao + "</td>");

                $('#tbFuncionario').append(tr);

            }
            $('#tbFuncionario').dataTable();
            waitingDialog.hide('Aguarde...')

        };

        function PopulaFuncionario(idModuloFuncionarioPerfil) {
            waitingDialog.show('Aguarde...');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Alterar Funcionário";
            $.ajax({
                type: "POST",
                url: "Funcionario.aspx/PopulaFuncionarioById",
                data: '{idModuloFuncionarioPerfil: ' + idModuloFuncionarioPerfil + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoFuncionarioById,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornoFuncionarioById(response) {
            var tr;
            var json = $.parseJSON(response.d);

            for (var i = 0; i < json.TableFuncionarioById.length; i++) {
                document.getElementById('<%= txtNomeFuncionario.ClientID%>').value = json.TableFuncionarioById[i].Nome;
                document.getElementById('<%= txtEmail.ClientID%>').value = json.TableFuncionarioById[i].Email;                
                document.getElementById('<%= txtUsuario.ClientID%>').value = json.TableFuncionarioById[i].Usuario;
                document.getElementById('<%= drpPerfil.ClientID%>').value = json.TableFuncionarioById[i].IdPerfil;
                document.getElementById('<%= hdnIdModuloFuncionarioPerfil.ClientID%>').value = json.TableFuncionarioById[i].IdModuloFuncionarioPerfil;
                document.getElementById('<%= txtBairro.ClientID%>').value = json.TableFuncionarioById[i].Bairro;
                document.getElementById('<%= txtCEP.ClientID%>').value = json.TableFuncionarioById[i].CEP;
                document.getElementById('<%= txtCidade.ClientID%>').value = json.TableFuncionarioById[i].Cidade;
                document.getElementById('<%= txtComplemento.ClientID%>').value = json.TableFuncionarioById[i].Complemento;
                document.getElementById('<%= txtCPF.ClientID%>').value = json.TableFuncionarioById[i].CPF;
                document.getElementById('<%= hdnIdPessoa.ClientID%>').value = json.TableFuncionarioById[i].IdPessoa;
                document.getElementById('<%= txtLogradouro.ClientID%>').value = json.TableFuncionarioById[i].Logradouro;
                document.getElementById('<%= txtNumero.ClientID%>').value = json.TableFuncionarioById[i].NumeroResidencia;
                document.getElementById('<%= txtRG.ClientID%>').value = json.TableFuncionarioById[i].RG;
                document.getElementById('<%= drpUF.ClientID%>').value = json.TableFuncionarioById[i].IdUF;


            }
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById("conteudoPagina_pAcao").value = "Alterar";
            $('.selectpicker').selectpicker('refresh');
            waitingDialog.hide('Aguarde...')
            OpenModal('dialogFuncionario');
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

                GetFuncionario();
            }
        }

        function ExcluiFuncionario(hdnIdPessoa) {
            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdPessoa.ClientID%>').value = hdnIdPessoa;
            OpenModal("dialogDel");
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Cadastro de Funcionários
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="box">
        <!-- /.box-header -->
        <div class="box-body table-responsive">
            <div class="col-md-12">
                <a href="javascript:limparCampos();" class="btn btn-primary margin">Novo Funcionário</a>
            </div>

            <div class="row ">
                <div class="box-body">
                    <div id="divFuncionario"></div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.row -->

        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->


    <!-- Modal Documento -->
    <div class="modal dialogFull " id="dialogFuncionario" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
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

                                        <legend>Dados do Funcionário</legend>

                                        <div class="row">

                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Nome<asp:RequiredFieldValidator ID="validaTxtNomeFuncionario" runat="server" ErrorMessage="Informe o Nome do Funcionário."
                                                            ControlToValidate="txtNomeFuncionario" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtNomeFuncionario" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnIdPessoa" runat="server" />
                                                    <asp:HiddenField ID="hdnIdModuloFuncionarioPerfil" runat="server" />
                                                    <asp:HiddenField ID="hdnIdContato1" runat="server" />
                                                    <asp:HiddenField ID="hdnIdContato2" runat="server" />
                                                    <asp:HiddenField ID="hdnIdContato3" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Usuário<asp:RequiredFieldValidator ID="validaTxtUsuario" runat="server" ErrorMessage="Informe o Usuário do Funcionário."
                                                            ControlToValidate="txtUsuario" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtUsuario" MaxLength="50" placeholder="informe um usuário/Login." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        CPF<asp:RequiredFieldValidator ID="validaTxtCPF" runat="server" ErrorMessage="Informe o CPF do Funcionário."
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
                                                    <label>Telefone Principal<asp:RequiredFieldValidator ID="validaTxtTel1" runat="server" ErrorMessage="Informe o telefone do Funcionário."
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
                                                        Logradouro<asp:RequiredFieldValidator ID="validatxtLogradouro" runat="server" ErrorMessage="Informe o endereço do Funcionário."
                                                            ControlToValidate="txtLogradouro" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtLogradouro" MaxLength="150" placeholder="informe o endereço do Funcionário." CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Número<%--<asp:RequiredFieldValidator ID="validaTxtNumero" runat="server" ErrorMessage="Informe o Número da Residência."
                                                            ControlToValidate="txtNumero" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator>--%></label>
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
                                                    <asp:DropDownList ID="drpUF" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Perfil<asp:RequiredFieldValidator ID="validaDrpPerfil" runat="server" ErrorMessage="Selecione o Perfil do usuário."
                                                            ControlToValidate="drpPerfil" InitialValue="0" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:DropDownList ID="drpPerfil" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                           
                                        </div>
                                        <%-- <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="alert alert-warning" ForeColor="Red" />--%>
                                        <div class="row" id="divModalErro" runat="server">
                                            <div class="box-body col-md-12">
                                                <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
                                                <div class="alert alert-warning">
                                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
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
                    <h4 class="modal-title">Cadastro de Funcionário</h4>
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
                    <h4 class="modal-title">Excluir Funcionário</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <input id="hdnIdAtividade" runat="server" type="hidden" />
                            <div class="alert alert-info ">Você realmente deseja excluir este Funcionário? </div>
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
