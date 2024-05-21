Public Class login

    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        Dim result = MessageBox.Show("Are you sure", "Closing",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = (DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Dim x As Integer
        Dim y As Integer

        x = txtNum.Text
        y = x - 1

        If txtusername.Text = "areno" And txtpassword.Text = "areno" Then
            MsgBox("logged in", MessageBoxIcon.Information)
            Main_Menu.Show()
            Me.Hide()
        Else
            txtNum.Text = y
            MsgBox("Invalid Information " & y & " Attempts Left", MessageBoxIcon.Warning)
            If txtNum.Text = "0" Then
                MsgBox("System will now close down", MessageBoxIcon.Asterisk)
                Application.Exit()
            End If
        End If
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtNum.Text = "3"
    End Sub
End Class
