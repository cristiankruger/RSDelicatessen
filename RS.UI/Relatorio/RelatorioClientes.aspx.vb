Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports Microsoft.Reporting.WebForms

Public Class RelatorioClientes
    Inherits System.Web.UI.Page
#Region "OBJETOS"
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objServicoUF As New ServicoUF
    Private objModeloCliente As New ModeloClienteDataDTO
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack() Then
                rptCliente = CarregaReport("objDataSource", "Report\rpt_Cliente.rdlc", Me.Page, rptCliente)
                carregaDropUF()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGeraRelat_Click(sender As Object, e As EventArgs)
        Try
            txtBairro.Text = txtBairro.Text.Trim()
            txtCidade.Text = txtCidade.Text.Trim()
            txtNome.Text = txtNome.Text.Trim()

            rptCliente = CarregaReport("objDataSource", "Report\rpt_Cliente.rdlc", Me.Page, rptCliente)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs)
        Try
            txtBairro.Text = ""
            txtCidade.Text = ""
            txtNome.Text = ""
            drpUF.SelectedValue = 0
            rptCliente = CarregaReport("objDataSource", "Report\rpt_Cliente.rdlc", Me.Page, rptCliente)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub carregaDropUF()
        Try
            drpUF.DataSource = objServicoUF.GetUF()
            drpUF.DataTextField = "sigla"
            drpUF.DataValueField = "idUF"
            drpUF.DataBind()
            drpUF.Items.Insert(0, New ListItem("---Selecione---", "0"))
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

            lblTitulo.Text = objServicoMensagemSistema.GetMensagemSistemaString("relatorio", "relatCliente")
            Dim nome_relat As String = lblTitulo.Text

            Dim vetorParametro As New Generic.List(Of ReportParameter)
            vetorParametro.Add(New ReportParameter("pTitulo", nome_relat, False))

            rptCliente.LocalReport.SetParameters(vetorParametro)

            _reportObjeto.LocalReport.Refresh()
            Return _reportObjeto
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class