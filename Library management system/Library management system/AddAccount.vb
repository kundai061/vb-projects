Public Class AddAccount

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please mention the Book ID", 0, "")
        Else
            Try
                If objcon.State = ConnectionState.Closed Then objcon.Open()
                com = New OleDb.OleDbCommand("UPDATE Books SET status='Available' WHERE BookID='" & ComboBox1.Text & "'", objcon)
                com.ExecuteNonQuery()
                objcon.Close()
                Call readData()
                If objcon.State = ConnectionState.Closed Then objcon.Open()
                com = New OleDb.OleDbCommand("INSERT INTO Returns VALUES('" & ComboBox1.Text & "','" & ComboBox5.Text & "','" & TextBox7.Text & "','" & DateTimePicker2.Text & "')", objcon)
                com.ExecuteNonQuery()
                MsgBox("Book has been returned!", 0, "")
                objcon.Close()
            Catch ex As Exception
                MsgBox(ex.Message, 0, "")
            End Try
        End If
    End Sub
End Class