Imports System.Data.OleDb

Public Class stock
    Private con As New MyDataStr
    Private conStr As String = con.conStr
    Private connectionString As String = conStr
    Private connection As OleDbConnection
    Private adapter As OleDbDataAdapter
    Private dataTable As DataTable
    Private Sub loadData()
        connection = New OleDbConnection(connectionString)
        adapter = New OleDbDataAdapter("SELECT * FROM Products", connection)
        dataTable = New DataTable()
        adapter.Fill(dataTable)
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub stock_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Main_Menu.Show()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.AddRange({"Dairy", "Bakery", "Canned Goods", "Snacks", "Beverages"})
        loadData()
    End Sub

    Private Sub btnAddProduct_Click(sender As Object, e As EventArgs) Handles btnAddProduct.Click
        If ValidateProductInput() Then
            Dim query As String = "INSERT INTO Products (ProductName, Category, UnitPrice, UnitQuantity, QuantityInStock) VALUES (@ProductName, @Category, @UnitPrice, @UnitQuantity, @QuantityInStock)"

            Using cmd As New OleDbCommand(query, connection)
                cmd.Parameters.AddWithValue("@ProductName", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Category", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@UnitPrice", Convert.ToDecimal(TextBox3.Text))
                cmd.Parameters.AddWithValue("@UnitQuantity", Convert.ToInt32(TextBox4.Text))
                cmd.Parameters.AddWithValue("@QuantityInStock", Convert.ToInt32(TextBox5.Text))

                Try
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()

                    ' Refresh the DataGridView
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                Catch ex As Exception
                    MessageBox.Show("Error while adding the product: " + ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Function ValidateProductInput() As Boolean
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please enter a product name.")
            Return False
        End If

        If String.IsNullOrWhiteSpace(ComboBox1.Text) Then
            MessageBox.Show("Please enter a category.")
            Return False
        End If

        Dim unitPrice As Decimal
        If Not Decimal.TryParse(TextBox3.Text, unitPrice) Then
            MessageBox.Show("Please enter a valid unit price.")
            Return False
        End If

        Dim quantityInStock As Integer
        If Not Integer.TryParse(TextBox4.Text, quantityInStock) Then
            MessageBox.Show("Please enter a valid quantity in stock.")
            Return False
        End If

        Return True
    End Function

    Private Sub updateStock_Click(sender As Object, e As EventArgs) Handles updateStock.Click
        If ValidateStockUpdateInput() Then
            Dim selectedRowIndex As Integer = DataGridView1.SelectedCells(0).RowIndex
            Dim selectedProductID As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("ProductID").Value)

            Dim query As String = "UPDATE Products SET ProductName = @ProductName, Category = @Category, UnitPrice = @UnitPrice, UnitQuantity = @UnitQuantity, QuantityInStock = @QuantityInStock WHERE ProductID = @ProductID"

            Using cmd As New OleDbCommand(query, connection)
                cmd.Parameters.AddWithValue("@ProductName", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Category", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@UnitPrice", Convert.ToInt32(TextBox3.Text))
                cmd.Parameters.AddWithValue("@UnitQuantity", TextBox4.Text)
                cmd.Parameters.AddWithValue("@QuantityInStock", Convert.ToInt32(TextBox5.Text))
                cmd.Parameters.AddWithValue("@ProductID", selectedProductID)

                Try
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()

                    ' Refresh the DataGridView
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                Catch ex As Exception
                    MessageBox.Show("Error while updating the stock: " + ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Function ValidateStockUpdateInput() As Boolean
        Dim selectedRowIndex As Integer = DataGridView1.SelectedCells(0).RowIndex
        If selectedRowIndex < 0 Then
            MessageBox.Show("Please select a product from the grid.")
            Return False
        End If

        Dim quantityInStock As Integer
        If Not Integer.TryParse(TextBox5.Text, quantityInStock) Then
            MessageBox.Show("Please enter a valid quantity in stock.")
            Return False
        End If

        Return True
    End Function
    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        Dim selectedRow As DataGridViewRow = Nothing

        If DataGridView1.SelectedRows.Count > 0 Then
            ' Access the selected row and print its contents
            selectedRow = DataGridView1.SelectedRows(0)
            ' Rest of the printing logic
        Else
            ' Display a message or take appropriate action when no row is selected
            MessageBox.Show("Please select a row to print.")
        End If

        If selectedRow IsNot Nothing Then
            ' Define the font and brush for drawing the text
            Dim font As New Font("Arial", 12)
            Dim brush As New SolidBrush(Color.Black)

            ' Define the starting position for drawing
            Dim startX As Integer = 50
            Dim startY As Integer = 50
            Dim lineHeight As Integer = font.Height + 5
            e.Graphics.DrawString("------------ARENO WHOLESALE MANAGEMENT RECEIPT------------", font, brush, startX, startY)
            startY += lineHeight
            startY += lineHeight

            ' Loop through the cells in the selected row and draw the text
            For Each cell As DataGridViewCell In selectedRow.Cells
                Dim cellText As String = String.Empty

                ' Determine the custom text based on the cell value or any other logic
                Select Case cell.ColumnIndex
                    Case 0
                        cellText = "PRODUCT ID: " & cell.Value.ToString()
                    Case 1
                        cellText = "PRODUCT NAME: " & cell.Value.ToString()
                    Case 2
                        cellText = "CATEGORY" & cell.Value.ToString()
                    Case 3
                        cellText = "PRICE " & cell.Value.ToString()
                    Case 4
                        cellText = "QUANTITY" & cell.Value.ToString()
                    Case 5
                        cellText = "QUANTITY IN STOCK" & cell.Value.ToString()
                End Select

                e.Graphics.DrawString(cellText, font, brush, startX, startY)
                startY += lineHeight
            Next

            ' Indicate that there are no more pages to print
            e.HasMorePages = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim printDocument As New Printing.PrintDocument()

            ' Assign the PrintPage event handler
            AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage

            ' Display the PrintPreviewDialog
            Dim printPreviewDialog As New PrintPreviewDialog()
            printPreviewDialog.Document = printDocument
            printPreviewDialog.ShowDialog()
        Else
            MsgBox("Please select a row first", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.CurrentRow IsNot Nothing Then

            TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
            ComboBox1.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString()
            TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            TextBox4.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
            TextBox5.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString()

        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            DataGridView1.ClearSelection()
            DataGridView1.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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

                Dim sql As String = "DELETE FROM Products WHERE ProductID = @ID"
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


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        For Each control As Control In Me.Controls
            If TypeOf control Is TextBox Then
                Dim textBox As TextBox = DirectCast(control, TextBox)
                textBox.Text = String.Empty
            End If
        Next
        ComboBox1.Text = String.Empty
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class