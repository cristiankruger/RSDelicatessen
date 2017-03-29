Imports System.Web.UI.WebControls

Public Class UtilValidacao
   
    Public Function validaDataTxtBox(ByVal txtDataInicio As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            If txtDataInicio.Text = "" Then
                verifica = False
            Else
                verifica = True
            End If

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function validaDataTxtBox(ByVal txtDataInicio As TextBox, ByVal txtDataFim As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtDataInicio.Text = "" And txtDataFim.Text = "" Then
                verifica = False
            ElseIf DateTime.Compare(txtDataInicio.Text, txtDataFim.Text) <= 0 Then
                verifica = True
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function verificaDataVaziaTxtBox(ByVal txtDataInicio As TextBox, ByVal txtDataFim As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtDataInicio.Text <> "" And txtDataFim.Text = "" Or txtDataInicio.Text = "" And txtDataFim.Text <> "" Then
                'Or txtDataFim.Text.Length <> 10 Or txtDataInicio.Text.Length <> 10
                verifica = True
            End If

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function verificaDataFormatoErrado(ByVal txtDataInicio As TextBox, ByVal txtDataFim As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtDataInicio.Text <> "" And txtDataInicio.Text.Length <> 10 Or txtDataFim.Text <> "" And txtDataFim.Text.Length <> 10 Then
                verifica = True
            End If

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaSenhas(ByVal txtSenha1 As TextBox, ByVal txtSenha2 As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtSenha1.Text = txtSenha2.Text Then
                verifica = True
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function VerificaTamanhoSenhas(ByVal txtSenha1 As TextBox, ByVal txtSenha2 As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtSenha1.Text.Length < 6 Or txtSenha2.Text.Length < 6 Then
                verifica = True
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidaDataPorAno(drpAno As DropDownList, txtDataInicio As TextBox, txtDataFim As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If CInt(drpAno.SelectedValue) <> 0 Then
                If Year(txtDataInicio.Text) = Year(txtDataFim.Text) Then
                    verifica = False
                Else
                    verifica = True
                End If
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidaAnoDrpTxt(txtDataFim As TextBox, txtDataInicio As TextBox, drpAno As DropDownList) As Boolean
        Try
            Dim verifica As Boolean

            If txtDataInicio.Text = "" And txtDataFim.Text = "" Then
                verifica = False
            ElseIf txtDataInicio.Text <> "" And txtDataFim.Text <> "" And drpAno.SelectedValue = 0 Then
                verifica = False
            ElseIf CInt(drpAno.SelectedValue) = Year(txtDataInicio.Text) And CInt(drpAno.SelectedValue) = Year(txtDataFim.Text) Then
                verifica = False
            Else
                verifica = True
            End If

            Return verifica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub verificaItemDropExiste(ByVal drop As DropDownList, ByVal valor As String)
        Try
            For Each item As ListItem In drop.Items

                If item.Value.Trim = valor.Trim Then
                    drop.SelectedValue = valor.Trim
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function VerificaData(ByVal txtData As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtData.Text.Length <> 10 Then
                verifica = False
            ElseIf IsDate(txtData.Text) Then
                verifica = True
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function VerificaDatas(ByVal txtData1 As TextBox, ByVal txtData2 As TextBox) As Boolean
        Try
            Dim verifica As Boolean

            If txtData1.Text.Length <> 10 Or txtData2.Text.Length <> 10 Then
                verifica = False
            ElseIf IsDate(txtData1.Text) And IsDate(txtData2.Text) Then
                verifica = True
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidaCPF(ByVal CPF As String) As Boolean

        Dim soma As Int32
        Dim resto As Int32

        soma = 0
        resto = 0

        'VERIFICA SE O CPF É NULO OU ESPAÇO EM BRANCO
        If (String.IsNullOrWhiteSpace(CPF)) Then
            Return False
        End If
        'SE O CPF VIER COM A MASCARA DEVEMOS REMOVER
        CPF = CPF.Replace(".", "").Replace("-", "")
        'VERIFICA SE O CPF TEM A QUANTIDADE DE 11 DIGITOS
        If (CPF.Length <> 11) Then
            Return False
        End If
        'VERIFICA SE OS NÚMEROS NÃO SÃO TODOS IGUAIS, EXEMPLO:00000000000
        For index = 1 To 9
            'PadRight COMPLETA O INDEX COM O PRÓPRIO INDEX:
            'EXEMPLO: index = 1 DEVE FICAR 11111111111
            If (CPF.Equals(index.ToString().PadRight(11, index.ToString()))) Then
                Return False
            End If
        Next
        'AQUI VAMOS DISTRIBUIR OS 9 PRIMEIROS DÍGITOS E VAMOS ADICIONAR OS PESOS
        '10,9,8,7,6,5,4,3 e 2
        'ABAIXO O EXEMPLO
        'CPF  | 1|1|1|4|4|4|7|7|7
        '-----------------------------------------------------------------------
        'PESO |10|9|8|7|6|5|4|3|2
        For index = 1 To 9
            'REALIZANDO A SOMA DOS NÚMEROS APÓS MULTIPLICAR O VALOR DE CADA 
            'COLUNA DO NOSSO QUADRO ACIMA(EXEMPLO: 1 X 10, 1 X 9, 1 X 8.......)
            soma += Convert.ToInt32(CPF.Substring(index - 1, 1)) * (11 - index)
        Next
        'Mod = PEGANDO O RESTO DA DIVISÃO
        resto = (soma * 10) Mod 11
        'SE O RESTO FOR 10 OU 11 DEVEMOS ENTÃO MUDAR O RESTO PARA ZERO'
        If (resto = 10 Or resto = 11) Then
            resto = 0
        End If
        'VERIFICA SE O RESTO É DIFERENTE DO PRIMEIRO DIGITO VERIFICADOR'
        If (resto <> Convert.ToInt32(CPF.Substring(9, 1))) Then
            Return False
        End If
        soma = 0
        'AQUI VAMOS DISTRIBUIR OS 10 PRIMEIROS DÍGITOS E VAMOS ADICIONAR OS PESOS
        'CPF  | 1| 1|1|4|4|4|7|7|7|3
        '-----------------------------------------------------------------------
        'PESO |11|10|9|8|7|6|5|4|3|2
        For index = 1 To 10
            soma += Convert.ToInt32(CPF.Substring(index - 1, 1)) * (12 - index)
        Next
        'Mod = PEGANDO O RESTO DA DIVISÃO
        resto = (soma * 10) Mod 11
        If (resto = 10 Or resto = 11) Then
            resto = 0
        End If
        'VERIFICA SE O RESTO É DIFERENTE DO SEGUNDO DIGITO VERIFICADOR'
        If (resto <> Convert.ToInt32(CPF.Substring(10, 1))) Then
            Return False
        End If
        Return True
    End Function
End Class
