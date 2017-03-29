<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Perfil.aspx.vb" Inherits="RS.UI.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="estilos" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <link href="../Bootstrap/css/app.css" rel="stylesheet" />

    <script>
        function pageLoad() {

            $('#<%= txtDescricao.ClientID%>').keyup(function () {
                var char = $(this).val().replace(/[!#$%^&*@()+=",._ºª{}:;?><[\]`´^~§|¨\\/]/g, '');
                $(this).val(char);
            });
        }

        function limpaidCategoria() {
            document.getElementById('<%= idCategoria.ClientID%>').value = "";
        }

        function resetCampos() {
            document.getElementById('<%= pAcao.ClientID %>').value = "Inserir";
            document.getElementById('<%= txtDescricao.ClientID%>').value = "";
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById('<%= idCategoria.ClientID%>').value = "";
            $("#divListBoxEsquerda").empty();
            $("#divListBox").empty();
            IniciaListBoxDireita();
            GetListBox();
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Cadastrar Perfil";
            OpenModal("dialogPerfil");
        }

        function IniciaListBoxDireita() {
            $("#divListBoxDireita").empty();
            var html = '<ul id="ulListBoxDireita" class="list-group"></br>';
            $('#divListBoxDireita').append(html);
        }

        function CloseValidationSummary() {
           <%-- document.getElementById('<%= ValidationSummary.ClientID%>').style.display = 'none';--%>
            document.getElementById('<%= validaTxtDescricao.ClientID%>').style.visibility = "hidden";
        }

        function GetPerfil() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Perfil.aspx/TablePerfil",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoPerfil,
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

        function RetornoPerfil(response) {
            var tr;
            var json = $.parseJSON(response.d);
            $("#divPerfil").empty();

            var html = '<table id="tbPerfil" class="table table-striped table-bordered"> \
                <thead> \
                <tr> \
                <th Style="Width: 150px"></th> \
                <th>Descricao</th> \
                </tr> \
                </thead> \
                <tbody></tbody> \
                </table>';

            $('#divPerfil').append(html);

            for (var i = 0; i < json.TablePerfil.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + json.TablePerfil[i].Acao + "</td>")
                tr.append("<td>" + json.TablePerfil[i].Descricao + "</td>");
                $('#tbPerfil').append(tr);
            }

            $('#tbPerfil').dataTable();
            waitingDialog.hide('Aguarde...')
        };

        function GetListBox() {
            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Perfil.aspx/PopulaListBox",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoListBox,
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

        function RetornoListBox(response) {
            var ul = "";
            var json = $.parseJSON(response.d);
            $("#divListBox").empty();

            var html = '<ul id="ulListBoxLeft" class="list-group"></br>';

            $('#divListBox').append(html);

            for (var i = 0; i < json.ListBox.length; i++) {
                ul += "<li class='list-group-item' value="+ json.ListBox[i].IdCategoria + ">" + json.ListBox[i].Categoria + "</li>";
            }
            ul += '</ul>';
            $('#ulListBoxLeft').append(ul);

        };


        function GetListBoxEsquerda(idPerfil) {

            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Perfil.aspx/PopulaListBoxEsquerda",
                data: '{idPerfil: ' + idPerfil + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoListBoxEsquerda,
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

        function RetornoListBoxEsquerda(response) {
            var ul = "";
            var json = $.parseJSON(response.d);
            $("#divListBox").empty();

            var html = '<ul id="ulListBoxLeft" class="list-group"></br>';

            $('#divListBox').append(html);
            
            if (json.ListBoxEsquerda!= undefined) {
                for (var i = 0; i < json.ListBoxEsquerda.length; i++) {
                    ul += "<li class='list-group-item' value=" + json.ListBoxEsquerda[i].IdCategoria + ">" + json.ListBoxEsquerda[i].Categoria + "</li>";
                }
            }
            ul += '</ul>';
            $('#ulListBoxLeft').append(ul);

        };

        function GetListBoxDireita(idPerfil) {

            waitingDialog.show('Aguarde...')
            $.ajax({
                type: "POST",
                url: "Perfil.aspx/PopulaListBoxDireita",
                data: '{idPerfil: ' + idPerfil + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoListBoxDireita,
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

        function RetornoListBoxDireita(response) {
            var ul = "";
            var json = $.parseJSON(response.d);
            
            $("#divListBoxDireita").empty();

            var html = '<ul id="ulListBoxDireita" class="list-group"></br>';
        
            $('#divListBoxDireita').append(html);
                        
            var idsCategoria = "";

            if (json.ListBoxDireita != undefined) {
                for (var i = 0; i < json.ListBoxDireita.length; i++) {
                    ul += "<li class='list-group-item' value=" + json.ListBoxDireita[i].IdCategoria + ">" + json.ListBoxDireita[i].Categoria + "</li>";
                    idsCategoria += json.ListBoxDireita[i].IdCategoria + ';' ;
                }
                var array = idsCategoria;
                document.getElementById('<%= idCategoria.ClientID%>').value = array;
            }

            ul += '</ul></div>';
            $('#ulListBoxDireita').append(ul);

        };

        function PopulaPerfil(idPerfil) {
            
            waitingDialog.show('Aguarde...');
            document.getElementById('<%=lblModal.ClientID%>').innerText = "Alterar Perfil";
            $.ajax({
                type: "POST",
                url: "Perfil.aspx/PopulaPerfilById",
                data: '{idPerfil: ' + idPerfil + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: RetornoPerfilById,
                error: function (xhr, textStatus, err) {
                    console.log("readyState: " + xhr.readyState);
                    console.log("responseText: " + xhr.responseText);
                    console.log("status: " + xhr.status);
                    console.log("text status: " + textStatus);
                    console.log("error: " + err);
                }
            });
        }

        function RetornoPerfilById(response) {

            var tr;
            var json = $.parseJSON(response.d);

            for (var i = 0; i < json.TablePerfilById.length; i++) {
                document.getElementById('<%= txtDescricao.ClientID %>').value = json.TablePerfilById[i].Descricao;
                document.getElementById('<%= hdnIdPerfil.ClientID%>').value = json.TablePerfilById[i].IdPerfil;

            }
            document.getElementById('<%= divModalErro.ClientID%>').style.display = 'none';
            document.getElementById("conteudoPagina_pAcao").value = "Alterar";

            waitingDialog.hide('Aguarde...')
            OpenModal('dialogPerfil');
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

                GetPerfil();
            }
        }
        function ExcluiPerfil(idPerfil) {

            waitingDialog.show('Aguarde...')
            document.getElementById('<%= hdnIdPerfil.ClientID%>').value = idPerfil;
            OpenModal("dialogDel");
        }


        function xinspect(o, i) {
            if (typeof i == 'undefined') i = '';
            if (i.length > 50) return '[MAX ITERATIONS]';
            var r = [];
            for (var p in o) {
                var t = typeof o[p];
                r.push(i + '"' + p + '" (' + t + ') => ' + (t == 'object' ? 'object:' + xinspect(o[p], i + '  ') : o[p] + ''));
            }
            return r.join(i + '\n');
        }

        $(function () {

            $('body').on('click', '.list-group .list-group-item', function () {
                $(this).toggleClass('active');
            });
            $('.list-arrows a').click(function () {

                var $button = $(this), actives = '';

                $('.list-group .active').each(function () {
                    if ($button.hasClass('move-left')) {
                        actives = $('.list-right ul li.active');
                        actives.clone().appendTo('.list-left ul');
                        actives.remove();
                        var array = document.getElementById('<%= idCategoria.ClientID%>').value;
                    
                    var retira = $(this).val() + ';';
                    
                    document.getElementById('<%= idCategoria.ClientID%>').value = array.replace(retira, "");
                   

                } else if ($button.hasClass('move-right')) {
                    actives = $('.list-left ul li.active');
                    actives.clone().appendTo('.list-right ul');
                    actives.remove();
                    document.getElementById('<%= idCategoria.ClientID%>').value += $(this).val() + ';';
                    };
                });
            });


            $('.dual-list .selector').click(function () {
                var $checkBox = $(this);
                if (!$checkBox.hasClass('selected')) {
                    $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
                    $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
                } else {
                    $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
                    $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
                }
            });
            $('[name="SearchDualList"]').keyup(function (e) {
                var code = e.keyCode || e.which;
                if (code == '9') return;
                if (code == '27') $(this).val(null);
                var $rows = $(this).closest('.dual-list').find('.list-group li');
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return !~text.indexOf(val);
                }).hide();
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="tituloPagina" runat="server">
    Cadastro de Perfil de Acesso

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="conteudoPagina" runat="server">
    <div class="box">     
        <!-- /.box-header -->
        <div class="box-body table-responsive">
            <div class="col-md-12">
                <a href="javascript:resetCampos(); " class="btn btn-primary margin">Novo Perfil</a>
            </div>

            <div class="row ">
                <div class="box-body">
                    <div id="divPerfil"></div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.row -->

        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->

    <!-- Modal-->
    <div class="modal dialogFull" id="dialogPerfil" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" onclick="CloseValidationSummary();" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><asp:Label ID="lblModal" runat="server" Enabled="false"/></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <fieldset>
                                        
                                        <legend>Dados do Perfil</legend>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Descrição<asp:RequiredFieldValidator ID="validaTxtDescricao" runat="server" ErrorMessage="Informe a descrição do perfil." ControlToValidate="txtDescricao" CssClass="text-red">* Campo Obrigatório</asp:RequiredFieldValidator></label>
                                                    <asp:TextBox ID="txtDescricao" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnIdPerfil" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Permissões de acesso</label>
                                                    <fieldset>
                                                        <div class="row">
                                                            <div class="dual-list list-left col-md-5">
                                                                <label>Acessos disponíveis</label>
                                                                <div class="well text-right">
                                                                    <div class="row">
                                                                        <div class="col-md-10">
                                                                            <div class="input-group">
                                                                                <span class="input-group-addon glyphicon glyphicon-search"></span>
                                                                                <input type="text" name="SearchDualList" class="form-control" placeholder="procurar" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <div class="btn-group">
                                                                                <a class="btn btn-default selector" title="select all"><i class="glyphicon glyphicon-unchecked"></i></a>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="divListBox">
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="list-arrows col-md-1 text-center">
                                                                
                                                                <a href="#" class="btn btn-default btn-sm move-left">
                                                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                                                </a>

                                                                <a href="#" class="btn btn-default btn-sm move-right">
                                                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                                                </a>
                                                            </div>

                                                            <div class="dual-list list-right col-md-5">
                                                                <label>Acessos deste Perfil</label>
                                                                <div class="well">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <div class="btn-group">
                                                                                <a class="btn btn-default selector" title="select all"><i class="glyphicon glyphicon-unchecked"></i></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-10">
                                                                            <div class="input-group">
                                                                                <input type="text" id="listBox2" name="SearchDualList" class="form-control" placeholder="procurar" />
                                                                                <span class="input-group-addon glyphicon glyphicon-search"></span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="divListBoxDireita">
                                                                       <ul id="ulListBoxRight" class="list-group"></ul>
                                                                    </div>
                                                                    <br>
                                                                    <asp:HiddenField ID="idCategoria" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
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
                                <a id="hrefFechar" class="close" data-dismiss="modal" onclick="CloseValidationSummary();" href="#">Fechar</a>
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

    <div class="modal fade" id="dialogAlerta" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog"">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cadastro de Perfil</h4>
                </div>

                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <div class="alert alert-info">
                                Operação realizada com sucesso.
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
                    <h4 class="modal-title">Excluir Perfil</h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="box-body">
                            <input id="hdnIdAtividade" runat="server" type="hidden" />
                            <div class="alert alert-info ">Você realmente deseja excluir este Perfil? </div>
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
