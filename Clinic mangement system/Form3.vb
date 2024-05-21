Public Class Form3
    Dim Sql As String
    Dim con As New OleDb.OleDbConnection
    Dim cuname As String
    Dim cupass As String

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\clinic.mdb"
    End Sub

    Private Function UpdateUser(ByVal username As String, ByVal password As String) As Boolean
        Dim s As String
        s = "UPDATE tbluseraccounts SET username = ?, userpassword = ? WHERE ID = ?"

        Dim cmd As New OleDb.OleDbCommand

        Try
            With cmd
                con.Open()
                .Connection = con
                .CommandText = s
                .Parameters.AddWithValue("username", username)
                .Parameters.AddWithValue("userpassword", password)
                .Parameters.AddWithValue("ID", 1)
                .ExecuteNonQuery()

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        con.Close()

        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Sql = "SELECT * FROM tbluseraccounts WHERE username = ? and userpassword = ?"
        Dim cmd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter
        Dim dt As New DataTable
        Dim username As String
        Dim password As String
        cuname = TextBox1.Text
        cupass = TextBox2.Text

        With cmd
            con.Open()
            .Connection = con
            .CommandText = Sql
            .Parameters.AddWithValue("username", TextBox1.Text)
            .Parameters.AddWithValue("password", TextBox2.Text)
            con.Close()
        End With

        da.SelectCommand = cmd
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            If TextBox4.Text = "" And TextBox3.Text = "" Then
                MsgBox("No change made!", MsgBoxStyle.Information)
            Else
                If TextBox4.Text <> "" Then
                    If TextBox5.Text = TextBox4.Text Then
                        password = TextBox4.Text
                    Else
                        MsgBox("Passwords don't match", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If
                If TextBox3.Text <> "" Then
                    username = TextBox3.Text
                End If
                UpdateUser("username", "password")
            End If

            TextBox1.Clear()
            TextBox2.Clear()
        Else
            MsgBox("Wrong username or password", MsgBoxStyle.Exclamation)

            TextBox1.Clear()
            TextBox2.Clear()
        End If

    End Sub
End Class