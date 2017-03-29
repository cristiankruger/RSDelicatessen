Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports Microsoft.Reporting.WebForms

Public Class RelatorioDatasComemorativas
    Inherits System.Web.UI.Page

#Region "OBJETOS"
    Private objValidacao As New UtilValidacao
    Private objServicoMensagemSistema As New ServicoMensagemSistema
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack() Then
                rptDataComemorativa = CarregaReport("objDataSource", "Report\rpt_DataComemorativa.rdlc", Me.Page, rptDataComemorativa)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGeraRelat_Click(sender As Object, e As EventArgs)
        Try
            If txtDataInicial.Text = "" And txtDataFinal.Text = "" Then
                divModalErro.Visible = False
                rptDataComemorativa = CarregaReport("objDataSource", "Report\rpt_DataComemorativa.rdlc", Me.Page, rptDataComemorativa)
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
                            rptDataComemorativa = CarregaReport("objDataSource", "Report\rpt_DataComemorativa.rdlc", Me.Page, rptDataComemorativa)
                        End If
                    End If
                End If
            End If
            
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs)
        Try
            lblErro.Text = ""
            txtDataFinal.Text = ""
            txtDataInicial.Text = ""
            txtNome.Text = ""
            divModalErro.Visible = False
            rptDataComemorativa = CarregaReport("objDataSource", "Report\rpt_DataComemorativa.rdlc", Me.Page, rptDataComemorativa)
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

            lblTitulo.Text = objServicoMensagemSistema.GetMensagemSistemaString("relatorio", "relatDataComemorativa")
            Dim nome_relat As String = lblTitulo.Text

            Dim vetorParametro As New Generic.List(Of ReportParameter)
            vetorParametro.Add(New ReportParameter("pTitulo", nome_relat, False))
            vetorParametro.Add(New ReportParameter("pDataInicial", txtDataInicial.Text.Trim(), False))
            vetorParametro.Add(New ReportParameter("pDataFinal", txtDataFinal.Text.Trim(), False))

            rptDataComemorativa.LocalReport.SetParameters(vetorParametro)

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