Public Class LoginForm1

    Dim sql As String
    Dim con As New OleDb.OleDbConnection
    Dim trial As Integer
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        sql = "SELECT * FROM tbluseraccounts WHERE username = '" & UsernameTextBox.Text & "' and userpassword = '" & PasswordTextBox.Text & "' "
        Dim cmd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter
        Dim dt As New DataTable


        If trial < 3 Then
            con.Open()

            With cmd
                .Connection = con
                .CommandText = sql
            End With
            da.SelectCommand = cmd
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Me.Hide()
                Form1.Show()
                Form1.Focus()
                UsernameTextBox.Clear()
                PasswordTextBox.Clear()
            Else
                trial += 1
                MsgBox("wrong Username or password", MsgBoxStyle.Exclamation)

                UsernameTextBox.Clear()
                PasswordTextBox.Clear()
            End If
            con.Close()
        Else
            MsgBox("you have tried 3 times ", MsgBoxStyle.Critical)
            UsernameTextBox.Visible = False
            UsernameLabel.Visible = False
            PasswordLabel.Visible = False
            PasswordTextBox.Visible = False
            OK.Visible = False
            Cancel.Visible = False
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\clinic.mdb"

    End Sub
End Class
