Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports System.Web.Services

Public Class Pagamento
    Inherits System.Web.UI.Page

#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objModeloPagamento As New ModeloPagamento
    Private objModeloParcela As New ModeloParcela
    Private objUtilValidacao As New UtilValidacao
    Private objServicoCliente As New ServicoCliente
    Private objServicoPedido As New ServicoPedido
    Private objServicoPagamento As New ServicoPagamento
    Private objServicoParcela As New ServicoParcela
    Private objServicoTipoPagamento As New ServicoTipoPagamento
    Private objServicoMensagemSistema As New ServicoMensagemSistema
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(btnGerarParcelas)
            scriptManager.RegisterPostBackControl(btnGravar)

            objFuncionarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)

            Dim idPedido = Request.QueryString("Pagamento")
            hdnIdPedido.Value = idPedido

            If Not Page.IsPostBack Then
                ExibeInfoPagamento(idPedido)
                carregaDropTipoPagamento()
                If hdnIdPagamento.Value <> "" Then
                    scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); GetParcelas(" + hdnIdPagamento.Value.ToString + ");", True)
                Else
                    scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "$('#alertRegistros').show();", True)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub carregaDropTipoPagamento()
        Try
            drpTipoPagamento.DataSource = objServicoTipoPagamento.GetTipoPagamento()
            drpTipoPagamento.DataTextField = "descricao"
            drpTipoPagamento.DataValueField = "idTipoPagamento"
            drpTipoPagamento.DataBind()
            drpTipoPagamento.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub ExibeInfoPagamento(ByVal idPedido As Integer)
        Try
            Dim objClienteInfo = objServicoCliente.GetClienteByPedido(idPedido)
            Dim objPedidoInfo = objServicoPedido.GetPedidoById(idPedido)
            Dim objPagamento = objServicoPagamento.GetPagamento(idPedido)
           
            If Not objPagamento Is Nothing Then
                Dim valorFinal = objPedidoInfo.ValorPedido * (100 - objPagamento.Desconto) / 100
                Dim SaldoDevedor = (objPedidoInfo.ValorPedido * (100 - objPagamento.Desconto) / 100) - objPagamento.SaldoPago

                txtObservacao.Text = objPagamento.Observacao
                txtDesconto.Text = CInt(objPagamento.Desconto)
                txtDataInicial.Text = objPagamento.DataInicial
                txtNumeroDeParcelas.Text = objPagamento.NumeroDeParcelas
                hdnIdPagamento.Value = objPagamento.IdPagamento

                txtValorFinal.Text = FormatCurrency(valorFinal, 2).Replace("R$", "")
                txtSaldoDevedor.Text = FormatCurrency(SaldoDevedor, 2).Replace("R$", "")
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaRefatoracao(); $('#alertRegistros').show();", True)

                txtValorFinal.Text = objPedidoInfo.ValorPedido
                txtSaldoDevedor.Text = objPedidoInfo.ValorPedido
                txtDesconto.Text = 0
            End If

            If txtDataInicial.Text = "" Then
                txtDataInicial.Text = objPedidoInfo.DataPedido.AddMonths(1)
            End If
            If objClienteInfo.Email <> "" Then
                txtEmail.Text = objClienteInfo.Email
            Else
                txtEmail.Text = "E-Mail não Cadastrado."
            End If

            txtNome.Text = objClienteInfo.Nome
            txtTelefone.Text = objClienteInfo.Telefone
            txtCPF.Text = objClienteInfo.CPF
            txtValorPedido.Text = objPedidoInfo.ValorPedido
            txtDataPedido.Text = objPedidoInfo.DataPedido
            

            hdnIdPessoa.Value = objClienteInfo.IdPessoa
            imgCliente.ImageUrl = objClienteInfo.Foto
            divModalErro.Visible = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Pedido/Pedido.aspx", False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmRefatorarParcelas_Click(sender As Object, e As EventArgs)
        Try
            Dim IdPgtoAntigo As Integer
            Dim IdPgtoNovo As Integer

            If txtNumeroDeParcelas.Text = 0 Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroParcela")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetParcelas(" + hdnIdPagamento.Value.ToString + "); BloqueiaPagamento();", True)
                Exit Sub
            ElseIf txtDataInicial.Text = "" Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataInicial")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetParcelas(" + hdnIdPagamento.Value.ToString + "); BloqueiaPagamento();", True)
                Exit Sub
            End If

            Dim verifica As Boolean
            verifica = validaDataTxtBox(txtDataInicial)

            If verifica = False Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataParcela")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetParcelas(" + hdnIdPagamento.Value.ToString + "); BloqueiaPagamento();", True)
                Exit Sub
            Else
                verifica = validaDataTxtBoxHdnField(txtDataPedido, txtDataInicial)

                If verifica = False Then
                    divModalErro.Visible = True
                    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroPrimeiroPgto")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetParcelas(" + hdnIdPagamento.Value.ToString + "); BloqueiaPagamento();", True)
                    Exit Sub
                Else
                    objModeloPagamento.IdPagamento = hdnIdPagamento.Value
                    IdPgtoAntigo = hdnIdPagamento.Value
                    objServicoPagamento.DeletaPagamento(objModeloPagamento, objFuncionarioLogado)

                    objModeloPagamento.DataInicial = txtDataInicial.Text
                    objModeloPagamento.Desconto = txtDesconto.Text
                    objModeloPagamento.IdPedido = hdnIdPedido.Value
                    objModeloPagamento.IdPessoa = hdnIdPessoa.Value
                    objModeloPagamento.NumeroDeParcelas = txtNumeroDeParcelas.Text
                    objModeloPagamento.Observacao = txtObservacao.Text
                    objModeloPagamento.IdPagamento = Nothing

                    objModeloPagamento.IdPagamento = objServicoPagamento.InserePagamento(objModeloPagamento, objFuncionarioLogado)
                    objServicoPedido.AlteraStatusPedido(objModeloPagamento, objFuncionarioLogado)
                    hdnIdPagamento.Value = objModeloPagamento.IdPagamento
                    IdPgtoNovo = objModeloPagamento.IdPagamento
                    objServicoParcela.InsereParcelas(objModeloPagamento, objFuncionarioLogado)
                    objServicoParcela.AlteraEstornadas(IdPgtoAntigo, IdPgtoNovo, objFuncionarioLogado)

                    lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "parcelaRefatorada")
                    ExibeInfoPagamento(hdnIdPedido.Value)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); OpenModal('dialogAlerta'); GetParcelas(" + hdnIdPagamento.Value.ToString + ");", True)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGerarParcelas_Click(sender As Object, e As EventArgs)
        Try
            Dim verifica As Boolean
            verifica = validaDataTxtBox(txtDataInicial)

            If verifica = False Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataParcela")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaRefatoracaoErro();", True)
                Exit Sub
            Else
                verifica = validaDataTxtBoxHdnField(txtDataPedido, txtDataInicial)

                If verifica = False Then
                    divModalErro.Visible = True
                    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroPrimeiroPgto")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaRefatoracaoErro();", True)
                    Exit Sub
                Else
                    objModeloPagamento.DataInicial = txtDataInicial.Text
                    objModeloPagamento.Desconto = txtDesconto.Text
                    objModeloPagamento.IdPedido = hdnIdPedido.Value
                    objModeloPagamento.IdPessoa = hdnIdPessoa.Value
                    objModeloPagamento.NumeroDeParcelas = txtNumeroDeParcelas.Text
                    objModeloPagamento.Observacao = txtObservacao.Text

                    objModeloPagamento.IdPagamento = objServicoPagamento.InserePagamento(objModeloPagamento, objFuncionarioLogado)
                    hdnIdPagamento.Value = objModeloPagamento.IdPagamento
                    objServicoParcela.InsereParcelas(objModeloPagamento, objFuncionarioLogado)

                    lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "parcelasGeradas")
                    ExibeInfoPagamento(hdnIdPedido.Value)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); OpenModal('dialogAlerta', true);", True)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs)
        Try
            objModeloParcela.DataPagamento = txtDataPagamentoParcela.Text
            objModeloParcela.IdTipoPagamento = drpTipoPagamento.SelectedValue
            objModeloParcela.ValorParcela = txtValorPagamento.Text.Replace(",", ".")
            objModeloParcela.IdParcela = hdnParcela.Value
            objModeloPagamento.IdPagamento = hdnIdPagamento.Value
            objModeloPagamento.IdPedido = hdnIdPedido.Value

            objServicoParcela.AlteraParcela(objModeloParcela, objModeloPagamento, objFuncionarioLogado)

            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "parcelaPaga")
            ExibeInfoPagamento(hdnIdPedido.Value)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); BloqueiaRefatoracao(); OpenModal('dialogAlerta', true); GetParcelas(" + hdnIdPagamento.Value.ToString + ");", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnVerificaUltimaParcela_Click(sender As Object, e As EventArgs) Handles btnVerificaUltimaParcela.Click
        Try
            carregaDropTipoPagamento()

            Dim lista As List(Of ModeloParcelaDTO)
            lista = objServicoParcela.GetParcelaById(hdnParcela.Value)
            For Each parcela As ModeloParcelaDTO In lista
                If parcela.DataPagamento <> "-" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); bloqueiaCampos(); MantemModal();", True)
                Else
                    Dim quantidade As Integer
                    quantidade = objServicoParcela.VerificaUltimaParcela(hdnIdPagamento.Value, hdnParcela.Value)

                    If quantidade = 1 Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); bloqueiaValorUltimaParcela();", True)
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); desbloqueiaCampos(); MantemModal();", True)
                    End If
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmaEstorno_Click(sender As Object, e As EventArgs)
        Try
            objServicoParcela.EstornaParcela(0, hdnParcela.Value)

            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "ParcelaEstornada")
            ExibeInfoPagamento(hdnIdPedido.Value)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "BloqueiaPagamento(); OpenModal('dialogAlerta',true); ", True)
            'GetParcelas(" + hdnIdPagamento.Value.ToString + ");
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TableParcelas(ByVal idPagamento As Integer) As String
        Try
            Dim objServicoParcela As New ServicoParcela
            Return objServicoParcela.TableParcela(idPagamento)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaParcelaById(ByVal idParcela As Integer) As String
        Try
            Dim objServicoParcela As New ServicoParcela
            Return objServicoParcela.TableParcelaById(idParcela)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "FUNCTION"
    Protected Function validaDataTxtBox(ByVal txtDataInicial As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.validaDataTxtBox(txtDataInicial)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Function validaDataTxtBoxHdnField(ByVal txtDataPedido As TextBox, ByVal txtDataInicial As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.validaDataTxtBox(txtDataPedido, txtDataInicial)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class