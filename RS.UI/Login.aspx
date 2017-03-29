<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="RS.UI.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>:: Sistema de Controle RS Delicatessen ::</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- bootstrap 3.0.2 -->
    <link href="<%= ResolveUrl("~/Bootstrap/css/bootstrap.min.css")%>" rel="stylesheet" type="text/css" />
    <!-- font Awesome -->
    <link href="<%= ResolveUrl("~/Bootstrap/css/font-awesome.min.css")%>" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="<%= ResolveUrl("~/Bootstrap/css/ionicons.min.css")%>" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="<%= ResolveUrl("~/Bootstrap/css/estilos.css")%>" rel="stylesheet" type="text/css" />


    <link href="<%= ResolveUrl("~/Bootstrap/js/jalert/jalert.css")%>" rel="stylesheet" />

    <link href="<%= ResolveUrl("~/Bootstrap/css/datepicker.css")%>" rel="stylesheet" type="text/css" />
    <!-- Live Search -->
    <link href="<%= ResolveUrl("~/Bootstrap/css/livesearch/bootstrap-select.min.css")%>" rel="stylesheet" type="text/css" />

    <link href="<%= ResolveUrl("~/Bootstrap/css/bootstrap-tagsinput.css")%>" rel="stylesheet" type="text/css" />

    <link href="<%= ResolveUrl("~/Bootstrap/css/datatables/dataTables.bootstrap.css")%>" rel="stylesheet" />

    <link href="<%= ResolveUrl("~/Bootstrap/css/app.css")%>" rel="stylesheet" />

    <!-- jQuery 2.0.2 -->
    <script src="<%= ResolveUrl("~/Bootstrap/js/jquery.min.js")%>" type="text/javascript"></script>
    <!-- jQuery UI 1.10.3 -->
    <script src="<%= ResolveUrl("~/Bootstrap/js/jquery-ui-1.10.3.min.js")%>" type="text/javascript"></script>
    <!-- Bootstrap -->
    <script src="<%= ResolveUrl("~/Bootstrap/js/bootstrap.min.js")%>" type="text/javascript"></script>


    <script src="<%= ResolveUrl("~/Bootstrap/js/bootstrap-select.js")%>"></script>

    <!-- AdminLTE App -->
    <script src="<%= ResolveUrl("~/Bootstrap/js/main/app.js")%>" type="text/javascript"></script>

    <script src="<%= ResolveUrl("~/Bootstrap/js/waitdialog.js")%>" type="text/javascript"></script>

    <script type="text/javascript">

        function pageLoad() {
            $('.selectpicker').selectpicker('render');
        }

        function CloseModal(id) {
            $('.modal-backdrop').remove();
            $('#' + id).modal('hide');
        }

        function OpenModal(id) {
            CloseModal(id);
            $('#' + id).modal('toggle');

            $("#" + id).draggable({
                handle: ".modal-header"
            });
        }
    </script>

</head>



<body class="bg-login">


    <div class="form-box" id="login-box">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="boxLogin">

                <div class="mainLogo">

                    <p>Sistema de Controle RS Delicatessen</p>

                </div>
                <div class="row">

                    <div class="boxBodyLogin bg-gray ">
                        <div class="boxTituloLogin">
                            Sistema RS Delicatessen
                        </div>


                        <div class=" form-group has-feedback">
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Login"></asp:TextBox>
                            <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtSenha" TextMode="Password" runat="server" CssClass="form-control" placeholder="Senha"></asp:TextBox>
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        </div>

                    </div>

                    <div class="footer">
                        <asp:Button ID="btnEntrar" CssClass="btn bg-light-blue btn-block" runat="server" Text="Entrar" />
                    </div>
                    <div class="alert alert-danger fade in" style="display: none;" role="alert">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                        <span class="sr-only">Error:</span>
                        Falha ao se logar. Verifique o Login e Senha digitados.
                    </div>

                </div>

            </div>

            <!-- MODAL PDF -->
            <div class="modal fade" id="dialogSetor" tabindex="-1" role="dialog" aria-hidden="true">
                <asp:UpdatePanel ID="updPanel" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog" style="width: 400px;">
                            <div class="modal-content">

                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Escolha a área que deseja acessar</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row ">
                                        <div class="box-body table-responsive">

                                            <div id="divSetores">
                                                <asp:DropDownList ID="drpSetor" CssClass="form-control input-sm selectpicker" data-live-search="true" runat="server">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer clearfix">
                                    <a class="close" data-dismiss="modal" href="#">Fechar</a>
                                </div>

                            </div>
                            <!-- /.modal-content -->
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->

        </form>

    </div>
</body>
</html>
