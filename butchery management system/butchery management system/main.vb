Imports System.Data.OleDb
Imports System.Collections.Generic

Public Class main
    Dim conStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Admin\Documents\butchery management system\butchery management system\butchery.mdb"
    Dim con As New OleDbConnection(conStr)
    Dim prodPrice As Decimal
    Dim myDictionary As New Dictionary(Of String, Integer)()

    Private Sub loadData()
        Dim dt As New DataTable

        Using con As New OleDbConnection(conStr)
            Dim cmd As New OleDbCommand("SELECT `ID`, Product, Unit_Price, Quantity, Price AS TotalPrice, currentDate FROM sales", con)
            con.Open()
            dt.Load(cmd.ExecuteReader())
        End Using

        DataGridView1.DataSource = dt
    End Sub

    Private Sub ProdData()
        Dim dt As New DataTable

        Using con As New OleDbConnection(conStr)
            Dim cmd As New OleDbCommand("SELECT * FROM product", con)
            con.Open()
            dt.Load(cmd.ExecuteReader())

            For Each row As DataRow In dt.Rows
                Dim name As Object = row("Name")
                Dim price As Object = row("price")
                myDictionary.Add(name, price)
                ComboBox1.Items.Add(name)

                Label5.Text = " " & Label5.Text & " " & name.ToString() & " $" & price.ToString() & " | "
            Next
        End Using
    End Sub

    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProdData()
        loadData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim() = "" Or TextBox1.Text.Trim() = "" Then
            MsgBox("Please enter Product and Quantity.", MsgBoxStyle.Critical)
            Return
        End If

        Dim product As String = ComboBox1.Text
        Dim quantity As Integer = 0

        If Not Integer.TryParse(TextBox1.Text, quantity) Then
            MsgBox("Please enter a valid Quantity.", MsgBoxStyle.Critical)
            Return
        End If

        If Not myDictionary.ContainsKey(product) Then
            MsgBox("Selected Product not found.", MsgBoxStyle.Critical)
            Return
        End If

        Dim unitPrice As Integer = myDictionary(product)
        Dim price As Integer = quantity * unitPrice
        Dim currentDate As Date = DateTimePicker1.Value.Date

        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim cmd As New OleDbCommand("INSERT INTO sales (Product, Quantity, Unit_Price, Price, currentDate) VALUES (@Product, @Quantity, @UnitPrice, @Price, @CurrentDate)", con)
                cmd.Parameters.AddWithValue("@Product", product)
                cmd.Parameters.AddWithValue("@Quantity", quantity)
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice)
                cmd.Parameters.AddWithValue("@Price", price)
                cmd.Parameters.AddWithValue("@CurrentDate", currentDate)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data inserted successfully!", MsgBoxStyle.Information)
            loadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.CurrentRow IsNot Nothing Then
            TextBox1.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            DateTimePicker1.Value = CDate(DataGridView1.CurrentRow.Cells(5).Value)
            ComboBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
        End If
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

                Dim sql As String = "DELETE FROM sales WHERE ID = @ID"
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.CurrentRow Is Nothing Then
            MsgBox("No row selected.", MsgBoxStyle.Critical)
            Return
        End If

        If ComboBox1.Text.Trim() = "" Or TextBox1.Text.Trim() = "" Then
            MsgBox("Please enter Product and Quantity.", MsgBoxStyle.Critical)
            Return
        End If

        Dim product As String = ComboBox1.Text
        Dim quantity As Integer = 0

        If Not Integer.TryParse(TextBox1.Text, quantity) Then
            MsgBox("Please enter a valid Quantity.", MsgBoxStyle.Critical)
            Return
        End If

        If Not myDictionary.ContainsKey(product) Then
            MsgBox("Selected Product not found.", MsgBoxStyle.Critical)
            Return
        End If

        Dim selectedID As Integer = 0
        If Not Integer.TryParse(DataGridView1.CurrentRow.Cells(0).Value.ToString(), selectedID) Then
            MsgBox("Invalid selected ID.", MsgBoxStyle.Critical)
            Return
        End If

        Dim unitPrice As Integer = myDictionary(product)
        Dim price As Integer = quantity * unitPrice
        Dim currentDate As Date = DateTimePicker1.Value.Date

        Try
            Using con As New OleDbConnection(conStr)
                con.Open()

                Dim sql As String = "UPDATE sales SET Product = @Product, Quantity = @Quantity, Unit_Price = @UnitPrice, Price = @Price, currentDate = @CurrentDate WHERE ID = @ID"
                Dim cmd As New OleDbCommand(sql, con)
                cmd.Parameters.AddWithValue("@Product", product)
                cmd.Parameters.AddWithValue("@Quantity", quantity)
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice)
                cmd.Parameters.AddWithValue("@Price", price)
                cmd.Parameters.AddWithValue("@CurrentDate", currentDate)
                cmd.Parameters.AddWithValue("@ID", selectedID)
                Dim result As Integer = cmd.ExecuteNonQuery()

                If result = 0 Then
                    MsgBox("No Data has been Updated!", MsgBoxStyle.Critical)
                Else
                    MsgBox("Data updated successfully!", MsgBoxStyle.Information)
                End If
            End Using

            loadData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub AddEmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddEmployeeToolStripMenuItem.Click
        addEmployee.Show()

    End Sub

    Private Sub ViewEmployeesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewEmployeesToolStripMenuItem.Click
        employees.Show()
    End Sub
   

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
            e.Graphics.DrawString("------------BUTCHERY RECEIPT------------", font, brush, startX, startY)
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
                        cellText = "UNIT PRICE: $" & cell.Value.ToString()
                    Case 3
                        cellText = "PRODUCT QUANTITY: " & cell.Value.ToString() & "kg(s)"
                    Case 4
                        cellText = "TOTAL SALE: $" & cell.Value.ToString()
                    Case 5
                        cellText = "DATE: " & cell.Value.ToString()
                End Select

                e.Graphics.DrawString(cellText, font, brush, startX, startY)
                startY += lineHeight
            Next

            ' Indicate that there are no more pages to print
            e.HasMorePages = False
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
       If e.RowIndex >= 0 Then
            DataGridView1.ClearSelection()
            DataGridView1.Rows(e.RowIndex).Selected = True
        End If
    End Sub


    Private Sub ToolStripDropDownButton2_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton2.Click

    End Sub

    Private Sub ViewItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewItemsToolStripMenuItem.Click
        items.Show()
    End Sub

    Private Sub AddItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddItemsToolStripMenuItem.Click
        addItem.Show()
    End Sub
End Class