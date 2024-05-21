Imports System.Data.OleDb
Public Class Customers
    Private str As New MyDataStr
    Private conStr As String = str.conStr
    Private Sub loadData()
        Dim dt As New DataTable

        Using con As New OleDbConnection(conStr)
            Dim cmd As New OleDbCommand("SELECT * FROM Customers", con)
            con.Open()
            dt.Load(cmd.ExecuteReader())
        End Using

        CustomersDataGridView.DataSource = dt
    End Sub

    Private Sub Customers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Main_Menu.Show()
    End Sub
    Private Sub Customers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SexComboBox.Items.AddRange({
                                   "male",
                                  "female"
                                 })
        
        loadData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim cmd As New OleDbCommand("INSERT INTO Customers (Name, Surname, CustomerID, `ID number`, Sex, Contact) VALUES (@Name, @Surname, @CustomerID, @ID_number, @Sex, @Contact)", con)

                cmd.Parameters.AddWithValue("@Name", NameTextBox.Text)
                cmd.Parameters.AddWithValue("@Surname", SurnameTextBox.Text)
                cmd.Parameters.AddWithValue("@CustomerID", CustomerIDTextBox.Text)
                cmd.Parameters.AddWithValue("@ID_number", ID_numberTextBox.Text)
                cmd.Parameters.AddWithValue("@Sex", SexComboBox.Text)
                cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data inserted successfully!", MsgBoxStyle.Information)
            loadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CustomersDataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles CustomersDataGridView.CellClick
        If CustomersDataGridView.CurrentRow IsNot Nothing Then

            NameTextBox.Text = CustomersDataGridView.CurrentRow.Cells(0).Value.ToString()
            SurnameTextBox.Text = CustomersDataGridView.CurrentRow.Cells(1).Value.ToString()
            CustomerIDTextBox.Text = CustomersDataGridView.CurrentRow.Cells(2).Value.ToString()
            ID_numberTextBox.Text = CustomersDataGridView.CurrentRow.Cells(3).Value.ToString()
            SexComboBox.Text = CustomersDataGridView.CurrentRow.Cells(4).Value.ToString()
            ContactTextBox.Text = CustomersDataGridView.CurrentRow.Cells(5).Value.ToString()
        End If
    End Sub

    Private Sub CustomersDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles CustomersDataGridView.CellContentClick

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If CustomersDataGridView.CurrentRow Is Nothing Then
            MsgBox("No row selected.", MsgBoxStyle.Critical)
            Return
        End If

        Dim selectedID As Integer = 0
        If Not Integer.TryParse(CustomersDataGridView.CurrentRow.Cells(2).Value.ToString(), selectedID) Then
            MsgBox("Invalid selected ID.", MsgBoxStyle.Critical)
            Return
        End If

        Dim result As Integer

        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim sql As String = "DELETE FROM Customers WHERE CustomerID = @ID"
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

                Dim cmd As New OleDbCommand("UPDATE Customers SET Name = @Name, Surname = @Surname, CustomerID = @CustomerID, `ID number` = @ID_number, Sex = @Sex, Contact = @Contact WHERE CustomerID = @CustomerID", con)

                cmd.Parameters.AddWithValue("@Name", NameTextBox.Text)
                cmd.Parameters.AddWithValue("@Surname", SurnameTextBox.Text)
                cmd.Parameters.AddWithValue("@CustomerID", CustomerIDTextBox.Text)
                cmd.Parameters.AddWithValue("@ID_number", ID_numberTextBox.Text)
                cmd.Parameters.AddWithValue("@Sex", SexComboBox.Text)
                cmd.Parameters.AddWithValue("@Contact", ContactTextBox.Text)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data updated successfully!", MsgBoxStyle.Information)
            loadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

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
        SexComboBox.Text = String.Empty
    End Sub
End Class