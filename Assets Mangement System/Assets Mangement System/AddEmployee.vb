Imports System.Data.OleDb

Public Class AddEmployee
    Private conn As New ConStr
    Private conStr As String = conn.text
    Dim con As New OleDbConnection(conStr)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim cmd As New OleDbCommand("INSERT INTO employee (Firstname, Surname, Address, Postcode, Salary) VALUES (@Firstname, @Surname, @Address, @Postcode, @Salary)", con)
                cmd.Parameters.AddWithValue("@Firstname", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Surname", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Address", TextBox3.Text)
                cmd.Parameters.AddWithValue("@Postcode", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Salary", TextBox5.Text)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data inserted successfully!", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub AddEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class