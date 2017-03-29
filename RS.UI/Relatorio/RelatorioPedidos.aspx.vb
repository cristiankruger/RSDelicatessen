﻿Imports Microsoft.Reporting.WebForms
Imports RS.Servico
Imports RS.Modelo
Imports RS.Util

Public Class RelatorioPedidos
    Inherits System.Web.UI.Page
#Region "OBJETOS"
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objValidacao As New UtilValidacao
    Private objServicoSituacaoPedido As New ServicoSituacaoPedido
#End Region
#Region "SUBS"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack() Then
                rptPedido = CarregaReport("objDataSource", "Report\rpt_Pedidos.rdlc", Me.Page, rptPedido)
                carregaDropSituacaopedido()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs)
        Try
            txtDtInicial.Text = ""
            txtDtFinal.Text = ""
            txtNomeCliente.Text = ""
            drpSituacao.SelectedValue = 0
            lblErro.Text = ""
            rptPedido = CarregaReport("objDataSource", "Report\rpt_Pedidos.rdlc", Me.Page, rptPedido)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub carregaDropSituacaopedido()
        Try
            drpSituacao.DataSource = objServicoSituacaoPedido.GetSituacaoPedido()
            drpSituacao.DataTextField = "situacaoPedido"
            drpSituacao.DataValueField = "idsituacaoPedido"
            drpSituacao.DataBind()
            drpSituacao.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGeraRelat_Click(sender As Object, e As EventArgs)
        Try
            If txtDtInicial.Text = "" And txtDtFinal.Text = "" Then
                divModalErro.Visible = False
                rptPedido = CarregaReport("objDataSource", "Report\rpt_Pedidos.rdlc", Me.Page, rptPedido)
            Else
                Dim verifica As Boolean = VerificaDataVazia()
                If verifica = True Then
                    divModalErro.Visible = True
                    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataInicialFinalRelat")
                Else
                    verifica = VerificaDataFormatoErrado()
                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataInicialFinalRelat")
                    Else
                        verifica = ValidaData()
                        If verifica = False Then
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroInicialMaiorFinal")
                        Else
                            divModalErro.Visible = False
                            rptPedido = CarregaReport("objDataSource", "Report\rpt_Pedidos.rdlc", Me.Page, rptPedido)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "FUNCTION"
    Public Function CarregaReport(ByVal _dataSetNome As String, ByVal _reportNome As String, ByVal _pagina As Page, ByVal _reportObjeto As ReportViewer) As ReportViewer
        Try
            Dim rds As New ReportDataSource()
            rds.Name = _dataSetNome
            Dim caminhoRpt As String = HttpContext.Current.Server.MapPath("~/")

            _reportObjeto.LocalReport.ReportPath = caminhoRpt & _reportNome
            _reportObjeto.LocalReport.DataSources.Add(rds)

            lblTitulo.Text = objServicoMensagemSistema.GetMensagemSistemaString("relatorio", "relatPedidos")
            Dim nome_relat As String = lblTitulo.Text

            Dim vetorParametro As New Generic.List(Of ReportParameter)
            vetorParametro.Add(New ReportParameter("pTitulo", nome_relat, False))
            vetorParametro.Add(New ReportParameter("pDataInicial", txtDtInicial.Text.Trim(), False))
            vetorParametro.Add(New ReportParameter("pDataFinal", txtDtFinal.Text.Trim(), False))

            rptPedido.LocalReport.SetParameters(vetorParametro)

            _reportObjeto.LocalReport.Refresh()
            Return _reportObjeto
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "VALIDACOES"
    Public Function ValidaData() As Boolean
        Try
            Return objValidacao.validaDataTxtBox(txtDtInicial, txtDtFinal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaDataVazia() As Boolean
        Try
            Return objValidacao.verificaDataVaziaTxtBox(txtDtInicial, txtDtFinal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaDataFormatoErrado() As Boolean
        Try
            Return objValidacao.verificaDataFormatoErrado(txtDtInicial, txtDtFinal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class