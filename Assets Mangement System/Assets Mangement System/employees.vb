Imports System.Data.OleDb

Public Class Employees
    Private con As New ConStr
    Private conStr As String = con.text

    Private Sub LoadData()
        Dim dt As New DataTable()

        Using con As New OleDbConnection(conStr)
            Dim cmd As New OleDbCommand("SELECT * FROM employee", con)
            con.Open()
            dt.Load(cmd.ExecuteReader())
        End Using

        DataGridView1.DataSource = dt
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.CurrentRow IsNot Nothing Then
            TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
            TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString()
            TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            TextBox4.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
            TextBox5.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString()
        End If
    End Sub

    Private Sub Employees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim sql As String = "UPDATE employee SET Firstname = @Firstname, Surname = @Surname, Address = @Address, Postcode = @Postcode, Salary = @Salary WHERE ID = @ID"
                Dim cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("@Firstname", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Surname", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Address", TextBox3.Text)
                cmd.Parameters.AddWithValue("@Postcode", TextBox4.Text)
                cmd.Parameters.AddWithValue("@Salary", TextBox5.Text)
                cmd.Parameters.AddWithValue("@ID", Val(DataGridView1.CurrentRow.Cells(0).Value.ToString()))

                Dim result As Integer = cmd.ExecuteNonQuery()

                If result = 0 Then
                    MsgBox("No data has been updated!", MsgBoxStyle.Critical)
                Else
                    MsgBox("Data updated successfully!", MsgBoxStyle.Information)
                End If
            End Using

            LoadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.CurrentRow Is Nothing Then
            MsgBox("No row selected.", MsgBoxStyle.Critical)
            Return
        End If

        Dim selectedID As Integer = 0

        If Not Integer.TryParse(DataGridView1.CurrentRow.Cells(0).Value.ToString(), selectedID) Then
            MsgBox("Invalid selected ID.", MsgBoxStyle.Critical)
            Return
        End If

        Dim result As Integer

        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim sql As String = "DELETE FROM employee WHERE ID = @ID"
                Dim cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("@ID", selectedID)
                result = cmd.ExecuteNonQuery()
            End Using

            If result = 0 Then
                MsgBox("No data has been deleted!", MsgBoxStyle.Critical)
            Else
                MsgBox("Data deleted successfully!")
                LoadData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub Reset()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Reset()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub
End Class