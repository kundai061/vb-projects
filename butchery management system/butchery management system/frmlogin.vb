Public Class frmLogin
    Private Const MaxLoginAttempts As Integer = 3
    Private ReadOnly connectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Admin\Documents\butchery management system\butchery management system\butchery.mdb"
    Private loginAttempts As Integer = 0

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text

        If ValidateLogin(username, password) Then
            main.Show()
            ClearLoginForm()
            Me.Hide()
        Else
            loginAttempts += 1
            MsgBox("Wrong username or password", MsgBoxStyle.Exclamation)

            ClearLoginForm()

            If loginAttempts >= MaxLoginAttempts Then
                DisableLoginForm()
                MsgBox("You have tried 3 times", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Function ValidateLogin(username As String, password As String) As Boolean
        Using con As New OleDb.OleDbConnection(connectionString)
            Dim sql As String = "SELECT COUNT(*) FROM [user] WHERE Username = ? AND Password = ?"
            Dim cmd As New OleDb.OleDbCommand(sql, con)

            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Password", HashPassword(password))

            con.Open()
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            con.Close()

            Return count > 0
        End Using
    End Function

    Private Function HashPassword(password As String) As String
        ' TODO: Implement a secure password hashing algorithm
        ' For example, you can use bcrypt or PBKDF2
        Return password
    End Function

    Private Sub ClearLoginForm()
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub DisableLoginForm()
        TextBox1.Visible = False
        Label1.Visible = False
        Label2.Visible = False
        TextBox2.Visible = False
        Button2.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class