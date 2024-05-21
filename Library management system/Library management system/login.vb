Public Class login

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

       
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (objcon.State = ConnectionState.Closed) Then
            objcon.Open()
        End If

        com = New OleDb.OleDbCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", objcon)
        com.Parameters.AddWithValue("@Username", TextBox1.Text)
        com.Parameters.AddWithValue("@Password", TextBox2.Text)

        dr = com.ExecuteReader()

        If dr.Read() Then
            MsgBox("Login Success")
            main.Show()
            Me.Hide()
        Else
            ' Invalid login
            MessageBox.Show("Invalid username or password.")
        End If

        dr.Close()
        objcon.Close()
    End Sub
End Class