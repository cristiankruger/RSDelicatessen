Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports System.Web.Services

Public Class Pedido
    Inherits System.Web.UI.Page
#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objModeloPedido As New ModeloPedido
    Private objServicoPedido As New ServicoPedido
    Private objServicoItem As New ServicoItem
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objUtilValidacao As New UtilValidacao
    Private objServicoCliente As New ServicoCliente
    Private objServicoProduto As New ServicoProduto
    Private objServicoParcela As New ServicoParcela
    Private objModeloPedidoDTO As New ModeloPedidoDTO
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(btnGravar)

            objFuncionarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)

            If IsNothing(objFuncionarioLogado) Then
                Response.Redirect("~/Login.aspx")
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetPedido();", True)
                carregaDrop()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub carregaDrop()
        Try
            carregaCliente()
            carregaProduto()
            carregaPedido()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub carregaCliente()
        Try
            drpCliente.DataSource = objServicoCliente.GetCliente()
            drpCliente.DataTextField = "nome"
            drpCliente.DataValueField = "idpessoa"
            drpCliente.DataBind()
            drpCliente.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub carregaProduto()
        Try
            drpItem.DataSource = objServicoProduto.GetProduto()
            drpItem.DataTextField = "produtoCategoria"
            drpItem.DataValueField = "idProduto"
            drpItem.DataBind()
            drpItem.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub carregaPedido()
        Try
            drpPedido.Items.Insert(0, New ListItem("---Selecione---", "0"))
            drpPedido.Items.Insert(1, New ListItem("Orçamento", "1"))
            drpPedido.Items.Insert(2, New ListItem("Pedido", "2"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Try
            Dim objModeloPedido As New ModeloPedido
            Dim verifica As Boolean
            Dim lista As List(Of ModeloItemDTO)

            objModeloPedido.DataPedido = txtDataPedido.Text
            objModeloPedido.DataValidade = txtDataValidade.Text
            objModeloPedido.Descricao = txtDescricao.Text
            objModeloPedido.IdFuncionario = objFuncionarioLogado.IdPessoa
            objModeloPedido.IdSituacao = drpPedido.SelectedValue
            objModeloPedido.IdCliente = drpCliente.SelectedValue

            lista = Session("ItemPedido")

            verifica = objServicoItem.verificaListaVazia(lista)

            If verifica = False Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoSemItem")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPedido', true); $('#tbItemPedido').dataTable(); ", True)
                Exit Sub
            ElseIf txtDataValidade.Text <> "" Then
                If ValidaDatas(txtDataPedido, txtDataValidade) = False Then
                    divModalErro.Visible = True
                    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataPedido")
                    lblErro.Text += objServicoMensagemSistema.GetMensagemSistemaString("msg", "validadeOrcamento")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPedido', true); $('#tbItemPedido').dataTable(); ", True)
                    Exit Sub
                ElseIf validaDataFabricaValidade(txtDataPedido, txtDataValidade) = False Then
                    divModalErro.Visible = True
                    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoMaiorOrcamento")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPedido', true); $('#tbItemPedido').dataTable(); ", True)
                    Exit Sub
                End If
                'ElseIf validaDataFabricaValidade(txtDataPedido, txtDataValidade) = False Then
                '    divModalErro.Visible = True
                '    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoMaiorOrcamento")
                '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPedido', true); $('#tbItemPedido').dataTable(); ", True)
                '    Exit Sub
            
            End If

            Select Case pAcao.Value
                Case "Inserir"
                    Dim idPedido As Integer
                    idPedido = objServicoPedido.InserePedido(objModeloPedido, objFuncionarioLogado)

                    For Each objItem As ModeloItemDTO In lista
                        Dim objModeloItem As New ModeloItemDTO

                        objModeloItem.IdPedido = idPedido
                        objModeloItem.IdProduto = objItem.IdProduto
                        objModeloItem.Quantidade = objItem.Quantidade
                        objModeloItem.Valor = objItem.Valor / objItem.Quantidade

                        objServicoItem.InsereItemPedido(objModeloItem, objFuncionarioLogado)
                    Next

                    lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoCadastrado")

                Case "Alterar"
                    objModeloPedido.IdPedido = hdnIdPedido.Value
                    objServicoPedido.AlteraPedido(objModeloPedido, objFuncionarioLogado)

                    lista = Session("ItensExcluidos")
                    If Not lista Is Nothing Then
                        For Each Item As ModeloItemDTO In lista
                            If Item.IdItem > 0 Then
                                objServicoItem.DeletaItemPedido(Item, objFuncionarioLogado)
                            End If
                        Next
                    End If

                    lista = Session("ItemPedido")
                    If Not lista Is Nothing Then
                        For Each Item As ModeloItemDTO In lista
                            Item.IdPedido = hdnIdPedido.Value
                            If Item.IdItem < 0 Then
                                Item.Valor = Item.Valor / Item.Quantidade
                                objServicoItem.InsereItemPedido(Item, objFuncionarioLogado)
                            End If
                        Next
                    End If
                    lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoAlterado")

            End Select

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            objModeloPedido.IdPedido = hdnIdPedido.Value

            objServicoPedido.DeletaPedido(objModeloPedido, objFuncionarioLogado)
            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoDeletado")
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmaExclusaoItem_Click(sender As Object, e As EventArgs) Handles btnConfirmaExclusaoItem.Click
        Try
            Dim objServicoItem As New ServicoItem
            Dim ListaItensExcluidos As New List(Of ModeloItemDTO)
            Dim ListaItens As New List(Of ModeloItemDTO)

            ListaItens = Session("ItemPedido")
            If Not IsNothing(Session("ItensExcluidos")) Then
                ListaItensExcluidos = Session("ItensExcluidos")
            End If

            Dim linhaItemExcluir = ListaItens.Find(Function(m) m.IdItem = hdnIdItemPedido.Value)
            ListaItensExcluidos.Add(linhaItemExcluir)
            ListaItens.Remove(linhaItemExcluir)

            Dim totalItens As Double
            For Each item In ListaItens
                totalItens += item.Valor
            Next

            txtTotalPedido.Text = FormatCurrency(totalItens, 2).Replace("R$", "")

            Session("ItemPedido") = ListaItens
            Session("ItensExcluidos") = ListaItensExcluidos

            drpItem.SelectedValue = 0
            txtQuantidade.Text = 1
            txtValorItem.Text = 0

            ltrTableItem.Text = objServicoItem.TableItem(ListaItens, Nothing)

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true); MantemModal(); $('#tbItemPedido').dataTable();", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim lista As New List(Of ModeloItemDTO)
            Dim objModeloItemDTO As New ModeloItemDTO
            Dim objServicoItem As New ServicoItem
            Dim idItemTemporario As Integer

            If Not IsNothing(Session("ItemPedido")) Then
                lista = Session("ItemPedido")
                idItemTemporario = CInt(lista.Count * -1 - 1)
            Else
                idItemTemporario = -1
            End If

            objModeloItemDTO.IdItem = idItemTemporario.ToString
            objModeloItemDTO.IdProduto = drpItem.SelectedValue
            objModeloItemDTO.Quantidade = txtQuantidade.Text
            objModeloItemDTO.Nome = drpItem.SelectedItem.ToString()
            objModeloItemDTO.Valor = FormatCurrency(txtValorItem.Text, 2).Replace("R$", "")

            If lista.FindAll(Function(s) s.IdProduto = drpItem.SelectedValue).Count > 0 Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroItem")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPedido', true); $('#tbItemPedido').dataTable();", True)

            Else
                lista.Add(objModeloItemDTO)
                ltrTableItem.Text = objServicoItem.TableItem(lista, Nothing)
                Session("ItemPedido") = lista

                If txtTotalPedido.Text = "" Then
                    txtTotalPedido.Text = 0
                End If

                txtTotalPedido.Text += CDbl(objModeloItemDTO.Valor)
                txtTotalPedido.Text = FormatCurrency(txtTotalPedido.Text, 2).Replace("R$", "")
            End If

            drpItem.SelectedValue = 0
            txtQuantidade.Text = 1
            txtValorItem.Text = 0
            drpPedido.Enabled = True
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetPedido(); MantemModal(); $('#tbItemPedido').dataTable();", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpaSession_Click(sender As Object, e As EventArgs) Handles btnLimpaSession.Click
        Try
            Session("ItemPedido") = Nothing
            Session("ItensExcluidos") = Nothing
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetPedido();", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click
        Try
            Dim lista As New List(Of ModeloItemDTO)
            Dim idPedido = hdnIdPedido.Value

            objModeloPedidoDTO = objServicoPedido.GetPedidoById(idPedido)

            txtDataPedido.Text = objModeloPedidoDTO.DataPedido
            txtDataValidade.Text = objModeloPedidoDTO.DataValidade
            txtDescricao.Text = objModeloPedidoDTO.Descricao
            txtTotalPedido.Text = objModeloPedidoDTO.ValorPedido
            drpCliente.SelectedValue = objModeloPedidoDTO.IdCliente
            drpPedido.SelectedValue = objModeloPedidoDTO.IdSituacao

            If objModeloPedidoDTO.IdSituacao <> 1 Then
                hdnBoolPedido.Value = True
            Else
                hdnBoolPedido.Value = False
            End If

            lista = objServicoItem.GetItemPedido(idPedido)
            ltrTableItem.Text = objServicoItem.TableItem(lista, hdnBoolPedido.Value)
            Session("ItemPedido") = lista
            pAcao.Value = "Alterar"
            If drpPedido.SelectedValue = 2 Then
                drpPedido.Enabled = False
            Else
                drpPedido.Enabled = True
            End If

            If objModeloPedidoDTO.IdSituacao = 1 Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "desBloqueiaCampos(); MantemModal(); $('#tbItemPedido').dataTable(); PopulaTxtCliente(" + objModeloPedidoDTO.IdCliente.ToString + "); ", True)
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "bloqueiaCampos(); MantemModal(); $('#tbItemPedido').dataTable(); PopulaTxtCliente(" + objModeloPedidoDTO.IdCliente.ToString + "); ", True)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmaEstorno_Click(sender As Object, e As EventArgs) Handles btnConfirmaEstorno.Click
        Try
            objServicoParcela.EstornaParcela(hdnIdPedido.Value, 0)
            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "pedidoDeletado")
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta',true); ", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnVerificaParcelasPagas_Click(sender As Object, e As EventArgs) Handles btnVerificaParcelasPagas.Click
        Try
            Dim ParcelasPagas As Integer
            ParcelasPagas = objServicoParcela.VerificaParcelasPagas(hdnIdPedido.Value)

            If ParcelasPagas = 0 Then
                lblCancelaPedido.Text = objServicoMensagemSistema.GetMensagemSistemaString("info", "nenhumaParcelaPaga")
            Else
                lblCancelaPedido.Text = String.Format(objServicoMensagemSistema.GetMensagemSistemaString("info", "parcelasPagas"), ParcelasPagas.ToString)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogEstornaPedido',true); ", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TablePedido() As String
        Try
            Dim objServicoPedido As New ServicoPedido
            Return objServicoPedido.TablePedido()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function TableContatosById(ByVal idCliente As Integer) As String
        Try
            Dim objServicoContato As New ServicoContato
            Return objServicoContato.TableContato(idCliente)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function TableClienteById(ByVal idCliente As Integer) As String
        Try
            Dim objServicoCliente As New ServicoCliente
            Return objServicoCliente.TableClienteById(idCliente)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function TableItemById(ByVal idProduto As Integer) As String
        Try
            Dim objServicoProduto As New ServicoProduto
            Return objServicoProduto.TableProdutoById(idProduto)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaGrid() As String
        Try
            Dim dt As New Data.DataTable
            Dim lista As New List(Of ModeloItemDTO)

            Dim ObjJson As New SharedJson
            lista = HttpContext.Current.Session("ItemPedido")

            If lista.Count > 0 Then
                Return ObjJson.JsonDados(dt)
            Else
                Return "0"
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "FUNCTION"
    Protected Function validaDataFabricaValidade(ByVal txtDataPedido As TextBox, ByVal txtDataValidade As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.validaDataTxtBox(txtDataPedido, txtDataValidade)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Function ValidaDatas(ByVal txtDataPedido As TextBox, ByVal txtDataValidade As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.VerificaDatas(txtDataPedido, txtDataValidade)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class