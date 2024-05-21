Imports System.Data.OleDb
Public Class addItem
    Dim conStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Admin\Documents\butchery management system\butchery management system\butchery.mdb"
    Dim con As New OleDbConnection(conStr)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim cmd As New OleDbCommand("INSERT INTO items (itName, price) VALUES (@itName, @price)", con)
                cmd.Parameters.AddWithValue("@itName", TextBox1.Text)
                cmd.Parameters.AddWithValue("@price", Val(TextBox2.Text))
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data inserted successfully!", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
End Class