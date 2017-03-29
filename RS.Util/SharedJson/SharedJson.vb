Imports System.Text
Imports System.Data

Public Class SharedJson

    Public Function JsonDados(ByVal dtRetorno As DataTable) As String
        Dim StrDc As String() = New String(dtRetorno.Columns.Count - 1) {}
        Dim HeadStr As String = String.Empty

        For i As Integer = 0 To dtRetorno.Columns.Count - 1

            StrDc(i) = dtRetorno.Columns(i).Caption

            HeadStr += """" & StrDc(i) & """ : """ & StrDc(i) & i.ToString() & "¾" & ""","
        Next

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1)

        Dim Sb As New StringBuilder()
        Sb.Append("{""" & Convert.ToString(dtRetorno.TableName) & """ : [")

        For i As Integer = 0 To dtRetorno.Rows.Count - 1

            Dim TempStr As String = HeadStr
            Sb.Append("{")

            For j As Integer = 0 To dtRetorno.Columns.Count - 1

                TempStr = TempStr.Replace(dtRetorno.Columns(j).ToString & j.ToString() & "¾", dtRetorno.Rows(i)(j))
            Next

            Sb.Append(TempStr & "},")
        Next

        Sb = New StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1))
        Sb.Append("]}")

        Return Sb.ToString()
    End Function

End Class
