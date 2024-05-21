Imports System.Data.OleDb
Public Class Employees
    Private str As New MyDataStr
    Private conStr As String = str.conStr
    Private Sub loadData()
        Dim dt As New DataTable

        Using con As New OleDbConnection(conStr)
            Dim cmd As New OleDbCommand("SELECT * FROM Employees", con)
            con.Open()
            dt.Load(cmd.ExecuteReader())
        End Using

        EmployeesDataGridView.DataSource = dt
    End Sub

    Private Sub Employees_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Main_Menu.Show()
    End Sub
    Private Sub Employees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim cmd As New OleDbCommand("INSERT INTO employees (ID, Name, `Last Name`, Address, `Date Of Birth`, Contact) VALUES (@ID, @Name, @Surname, @Address, @DOB, @Contact)", con)
                cmd.Parameters.AddWithValue("@ID", IDTextBox.Text)
                cmd.Parameters.AddWithValue("@Name", NameTextBox.Text)
                cmd.Parameters.AddWithValue("@Surname", Last_NameTextBox.Text)
                cmd.Parameters.AddWithValue("@Address", AddressTextBox.Text)
                cmd.Parameters.AddWithValue("@DOB", DateValue(Date_Of_BirthDateTimePicker.Value))
                cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text)

                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data inserted successfully!", MsgBoxStyle.Information)
            loadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If EmployeesDataGridView.CurrentRow Is Nothing Then
            MsgBox("No row selected.", MsgBoxStyle.Critical)
            Return
        End If

        Dim selectedID As Integer = 0
        If Not Integer.TryParse(EmployeesDataGridView.CurrentRow.Cells(0).Value.ToString(), selectedID) Then
            MsgBox("Invalid selected ID.", MsgBoxStyle.Critical)
            Return
        End If

        Dim result As Integer

        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim sql As String = "DELETE FROM employees WHERE ID = @ID"
                Dim cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("@ID", selectedID)
                result = cmd.ExecuteNonQuery()
            End Using

            If result = 0 Then
                MsgBox("No Data has been deleted!", MsgBoxStyle.Critical)
            Else
                MsgBox("Data deleted successfully!")
                loadData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim cmd As New OleDbCommand("UPDATE employees SET Name = @Name, [Last Name] = @Surname, Address = @Address, `Date Of Birth` = @DOB, Contact = @Contact WHERE ID = @ID", con)
                cmd.Parameters.AddWithValue("@Name", NameTextBox.Text)
                cmd.Parameters.AddWithValue("@Surname", Last_NameTextBox.Text)
                cmd.Parameters.AddWithValue("@Address", AddressTextBox.Text)
                cmd.Parameters.AddWithValue("@DOB", DateValue(Date_Of_BirthDateTimePicker.Value))
                cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text)
                cmd.Parameters.AddWithValue("@ID", IDTextBox.Text)

                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data updated successfully!", MsgBoxStyle.Information)
            loadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub EmployeesDataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles EmployeesDataGridView.CellClick
        If EmployeesDataGridView.CurrentRow IsNot Nothing Then
            IDTextBox.Text = EmployeesDataGridView.CurrentRow.Cells(0).Value.ToString()
            NameTextBox.Text = EmployeesDataGridView.CurrentRow.Cells(1).Value.ToString()
            Last_NameTextBox.Text = EmployeesDataGridView.CurrentRow.Cells(2).Value.ToString()
            Date_Of_BirthDateTimePicker.Text = EmployeesDataGridView.CurrentRow.Cells(3).Value
            ContactTextBox.Text = EmployeesDataGridView.CurrentRow.Cells(4).Value.ToString()
            AddressTextBox.Text = EmployeesDataGridView.CurrentRow.Cells(5).Value.ToString()
        End If
    End Sub

    Private Sub EmployeesDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles EmployeesDataGridView.CellContentClick

    End Sub

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Main_Menu.Show()
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        For Each control As Control In Me.Controls
            If TypeOf control Is TextBox Then
                Dim textBox As TextBox = DirectCast(control, TextBox)
                textBox.Text = String.Empty
            End If
        Next
    End Sub
End Class