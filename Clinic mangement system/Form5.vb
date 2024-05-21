Public Class Form5
    Dim con As New OleDb.OleDbConnection
    Dim _method As String
    Dim lastuser As Integer
    Private Function lastID() As Integer

        Dim qry As String = "SELECT MAX(ID) AS last_id FROM patients;"
        Dim cmd As New OleDb.OleDbCommand


        Try
            con.Open()
            With cmd
                .Connection = con
                .CommandText = qry
                lastuser = 1 + Convert.ToInt32(.ExecuteScalar())
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If issetInput() Then
            Dim cmd As New OleDb.OleDbCommand
            Dim s As String = "INSERT INTO accounts (consult_fee, method, drug_fee, bed_fee, user_id) VALUES (" & Val(TextBox1.Text) & ", '" & _method & "', " & Val(TextBox2.Text) & ", " & Val(TextBox3.Text) & ", " & lastuser & " )"

            Try
                con.Open()
                With cmd
                    .Connection = con
                    .CommandText = s
                    .ExecuteNonQuery()
                End With
                Form1.Reset_Data()
                Form1.Show()
                Me.Close()
                MsgBox("Successfully entered", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            con.Close()
        Else
            MsgBox("please enter all details", MsgBoxStyle.Critical)
        End If
    End Sub
    Private Function issetInput() As Boolean
        If TextBox1.Text <> "" And TextBox2.Text <> "" And RadioButton1.Checked = False Or RadioButton2.Checked = False Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub payMethod(ByVal gender As String)
        If _method = "swipe" Then
            RadioButton2.Checked = True

        Else
            RadioButton1.Checked = True

        End If
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\clinic.mdb"
        lastID()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton2.Checked Then
            RadioButton1.Checked = False
        End If
        _method = "cash"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked Then
            RadioButton2.Checked = False
        End If
        _method = "swipe"
    End Sub
End Class