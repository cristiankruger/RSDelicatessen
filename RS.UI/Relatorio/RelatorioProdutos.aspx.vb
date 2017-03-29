Imports RS.Servico
Imports RS.Modelo
Imports RS.Util
Imports Microsoft.Reporting.WebForms

Public Class RelatórioProdutos
    Inherits System.Web.UI.Page
#Region "OBJETO"
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objModeloProdutoCategoriaDTO As New ModeloProdutoCategoriaDTO
    Private objValidacao As New UtilValidacao
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack() Then
                rptProduto = CarregaReport("objDataSource", "Report\rpt_Produto.rdlc", Me.Page, rptProduto)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs)
        Try
            txtCategoria.Text = ""
            txtDataFinal.Text = ""
            txtDataInicial.Text = ""
            txtDescricao.Text = ""
            txtNome.Text = ""
            lblErro.Text = ""
            divModalErro.Visible = False
            rptProduto = CarregaReport("objDataSource", "Report\rpt_Produto.rdlc", Me.Page, rptProduto)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGeraRelat_Click(sender As Object, e As EventArgs)
        Try
            If txtDataInicial.Text = "" And txtDataFinal.Text = "" Then
                divModalErro.Visible = False
                rptProduto = CarregaReport("objDataSource", "Report\rpt_Produto.rdlc", Me.Page, rptProduto)
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
                            rptProduto = CarregaReport("objDataSource", "Report\rpt_Produto.rdlc", Me.Page, rptProduto)
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

            lblTitulo.Text = objServicoMensagemSistema.GetMensagemSistemaString("relatorio", "relatProduto")
            Dim nome_relat As String = lblTitulo.Text

            Dim vetorParametro As New Generic.List(Of ReportParameter)
            vetorParametro.Add(New ReportParameter("pTitulo", nome_relat, False))
            vetorParametro.Add(New ReportParameter("pDataInicial", txtDataInicial.Text.Trim(), False))
            vetorParametro.Add(New ReportParameter("pDataFinal", txtDataFinal.Text.Trim(), False))

            rptProduto.LocalReport.SetParameters(vetorParametro)

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
            Return objValidacao.validaDataTxtBox(txtDataInicial, txtDataFinal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaDataVazia() As Boolean
        Try
            Return objValidacao.verificaDataVaziaTxtBox(txtDataInicial, txtDataFinal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaDataFormatoErrado() As Boolean
        Try
            Return objValidacao.verificaDataFormatoErrado(txtDataInicial, txtDataFinal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class