<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataComemorativa.aspx.vb" Inherits="RS.UI.DataComemorativa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script>
        function pageLoad() {
            $('#<%=txtData.ClientID%>').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR',
                autoclose: true,
                "setDate": new Date(),
                yearRange: '1900:2050'
            });

            $('#<%=txtData.ClientID%>').mask("99/99/9999");
        }

        function limparCampos() {
            document.getElementById('<%= pAcao.ClientID %>').value = "Inserir";
            document.getElementById('<%= txtDescricao.ClientID%>').value = "";
            document.getElementById('<%= txtData.ClientID%>').value = "";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Data Comemorativa";
            CloseValidationSummary();
            OpenModal("dialogData");
        }

        function CloseValidationSummary() {
            document.getElementById('<%= validaTxtDescricao.ClientID%>').style.visibility = "hidden";
            document.getElementById('<%= validaTxtData.ClientID%>').style.visibility = "hidden";
        }

        function GetDataComemorativa(idPessoa) {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "DataComemorativa.aspx/TableDataComemorativa",
                data: '{idPessoa: ' + idPessoa + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoData,
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

        function RetornoData(response) {
            var tr;
            var json = $.parseJSON(response.d);
            $("#divData").empty();

            if (response.d == 0) {
                $('#alertRegistros').show();
            }
            else {
                var html = '<table id="tbData" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Descricao</th> \
                <th>Data</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

                $('#divData').append(html);


                for (var i = 0; i < json.TableData.length; i++) {
                    tr = $('<tr/>');

                    tr.append("<td>" + json.TableData[i].Acao + "</td>")
                    tr.append("<td>" + json.TableData[i].Descricao + "</td>");
                    tr.append("<td>" + json.TableData[i].DataComemorativa + "</td>");
                    $('#tbData').append(tr);
                }
                $('#tbData').dataTable();
            }
            waitingDialog.hide('Aguarde...')
        };

        function PopulaDataComemorativa(idDataComemorativa) {
            waitingDialog.show('Aguarde...');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Alterar Data Comemorativa";
            $.ajax({
                type: "POST",
                url: "DataComemorativa.aspx/TableDataComemorativaById",
                data: '{idDataComemorativa: ' + idDataComemorativa + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoDataComemorativaById,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornoDataComemorativaById(response) {
            //var tr;
            var json = $.parseJSON(response.d);

            for (var i = 0; i < json.TableDataById.length; i++) {
                document.getElementById('<%= txtData.ClientID%>').value = json.TableDataById[i].DataComemorativa;
                document.getElementById('<%= txtDescricao.ClientID%>').value = json.TableDataById[i].Descricao;
                document.getElementById('<%= hdnIdDataComemorativa.ClientID%>').value = json.TableDataById[i].IdDataComemorativa;
                document.getElementById('<%= hdnIdPessoa.ClientID%>').value = json.TableDataById[i].IdPessoa;
            }
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById("conteudoPagina_pAcao").value = "Alterar";
            $('.selectpicker').selectpicker('refresh');
            waitingDialog.hide('Aguarde...')
            OpenModal('dialogData');
        };

        function ExcluiDataComemorativa(IdDataComemorativa) {
            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdDataComemorativa.ClientID%>').value = IdDataComemorativa;
            OpenModal("dialogDel");
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
                idPessoa = document.getElementById('<%= hdnIdPessoa.ClientID%>').value;
                GetDataComemorativa(idPessoa);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Datas Comemorativas do Cliente
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="box">
        <br />
        <div class="col-md-12">
            <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="btn btn-primary" Text="Voltar" />
        </div>
        <!-- /.box-header -->
        <div class="row">
            <%--<div class="box-tools pull-right">--%>
            <div class="col-md-12">
                <div class="form-group">
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-3  ">
                    <div class="form-group">
                        <asp:Image CssClass="img-rouded" ID="imgCliente" Height="80%" Width="80%" runat="server" BackColor="#999999" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Nome</label>
                        <asp:TextBox ID="txtNome" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>E-mail</label>
                        <asp:TextBox ID="txtEmail" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Telefone</label>
                        <asp:TextBox ID="txtTelefone" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body table-responsive">
        <div class="col-md-12">
            <a href="javascript:limparCampos();" class="btn btn-primary margin">Nova Data</a>
        </div>

        <div class="row ">
            <div class="box-body">
                <div id="divData"></div>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.row -->
        <div class="alert alert-info fade in" id="alertRegistros" style="display: none;" role="alert">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Informação:</span>
            Não há Datas cadastradas. 
        </div>
    </div>
    <!-- /.box-body -->
    <%--   </div>--%>
    <!-- /.box -->
    <!-- Modal Documento -->
    <div class="modal" id="dialogData" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
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

                                        <legend>Dados da Data Comemorativa</legend>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Descrição da Data Comemorativa<asp:RequiredFieldValidator ID="validaTxtDescricao" runat="server" InitialValue="" ControlToValidate="txtDescricao" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtDescricao" placeholder="informe o nome da data comemorativa." MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnIdPessoa" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Data<asp:RequiredFieldValidator ID="validaTxtData" runat="server" InitialValue="" ControlToValidate="txtData" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtData" placeholder="dd/mm/aaaa" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divModalErro" runat="server">
                                                <h4 class="text-yellow"><i class="icon fa fa-warning marginRight"></i>Alertas</h4>
                                                <div class="alert alert-warning">
                                                    <asp:Label ID="lblErro" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12 ">
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
                    <h4 class="modal-title">Excluir Data</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <input id="hdnIdDataComemorativa" runat="server" type="hidden" />
                            <div class="alert alert-info ">Você realmente deseja excluir esta Data? </div>
                            <div class="modal-footer ">
                                <asp:Button ID="btnConfirmExclusao" class="btn btn-primary" runat="server" CausesValidation="false" Text="Sim" OnClick="btnConfirmExclusao_Click"></asp:Button>
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
