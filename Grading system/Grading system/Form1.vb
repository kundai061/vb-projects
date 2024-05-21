Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim num As Integer
        num = TextBox1.Text

        Select Case num
            Case Is >= 85
                Label1.Text = "A"
            Case Is < 85 AND  > 75
                Label1.Text = "B"
        End Select

    End Sub
End Class
