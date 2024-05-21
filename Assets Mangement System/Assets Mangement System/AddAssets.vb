Imports System.Data.OleDb
Public Class AddAssets
    Private con As New ConStr
    Private conStr As String = con.text
    Private connectionString As String = conStr
    Private connection As OleDbConnection
    Private adapter As OleDbDataAdapter
    Private dataTable As DataTable
    Private Sub loadData()
        connection = New OleDbConnection(connectionString)
        adapter = New OleDbDataAdapter("SELECT * FROM Product", connection)
        dataTable = New DataTable()
        adapter.Fill(dataTable)
        DataGridView1.DataSource = dataTable
    End Sub
    Private Sub setup()
        ComboBox2.Items.AddRange({"good", "damaged", "preowned"})
        ComboBox3.Items.AddRange({"Computer Science", "Electrical Engineering", "Mechanical Engineering", "Civil Engineering", "Business Administration", "Graphic Design", "Automotive Technology", "Applied Sciences"})
        connection = New OleDbConnection(connectionString)
        adapter = New OleDbDataAdapter("SELECT * FROM Supplier", connection)
        dataTable = New DataTable()
        adapter.Fill(dataTable)

        For Each row As DataRow In dataTable.Rows
            ComboBox1.Items.Add(row.Item(1))
        Next

    End Sub

    Private Sub AddAssets_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        main.Show()
    End Sub
    Private Sub AddAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadData()
        setup()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each control As Control In Me.Controls
            If TypeOf control Is TextBox Then
                Dim textBox As TextBox = DirectCast(control, TextBox)
                textBox.Text = String.Empty
            End If
            If TypeOf control Is ComboBox Then
                Dim Combobox As ComboBox = DirectCast(control, ComboBox)
                Combobox.Text = String.Empty
            End If
        Next
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If True Then
            Dim query As String = "INSERT INTO Product (ProdName, Price, Quantity, Supplier, SerialNumber, Model, State, Department) VALUES (@ProdName, @Price, @Quantity, @Supplier, @SerialNumber, @Model, @State, @Department)"

            Using cmd As New OleDbCommand(query, connection)
                cmd.Parameters.AddWithValue("@ProdName", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(TextBox2.Text))
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(TextBox3.Text))
                cmd.Parameters.AddWithValue("@Supplier", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@SerialNumber", Convert.ToInt32(TextBox4.Text))
                cmd.Parameters.AddWithValue("@Model", TextBox5.Text)
                cmd.Parameters.AddWithValue("@State", ComboBox2.Text)
                cmd.Parameters.AddWithValue("@Department", ComboBox3.Text)


                Try
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()

                    ' Refresh the DataGridView
                    loadData()
                Catch ex As Exception
                    MessageBox.Show("Error while adding the product: " + ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
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

                Dim sql As String = "DELETE FROM Product WHERE ID = @ID"
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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
        TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString()
        TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
        ComboBox1.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
        TextBox4.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString()
        TextBox5.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString()
        ComboBox2.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString()
        ComboBox3.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If True Then
            Dim selectedRowIndex As Integer = DataGridView1.SelectedCells(0).RowIndex
            Dim selectedProductID As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("ID").Value)

            Dim query As String = "UPDATE Product SET ProdName = @ProdName, Price=@Price, Quantity= @Quantity, Supplier=@Supplier, SerialNumber=@SerialNumber, Model=@Model, State=@State, Department=@Department WHERE ID=@ID"

            Using cmd As New OleDbCommand(query, connection)
                cmd.Parameters.AddWithValue("@ProdName", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(TextBox2.Text))
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(TextBox3.Text))
                cmd.Parameters.AddWithValue("@Supplier", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@SerialNumber", Convert.ToInt32(TextBox4.Text))
                cmd.Parameters.AddWithValue("@Model", TextBox5.Text)
                cmd.Parameters.AddWithValue("@State", ComboBox2.Text)
                cmd.Parameters.AddWithValue("@Department", ComboBox3.Text)
                cmd.Parameters.AddWithValue("@ID", selectedProductID)

                Try
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()

                    ' Refresh the DataGridView
                    loadData()
                Catch ex As Exception
                    MessageBox.Show("Error while adding the product: " + ex.Message)
                End Try
            End Using
        End If
    End Sub
End Class