Public Class BookDetail
    Dim sel As Integer
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label1.Text = ComboBox1.Text
        Label1.Visible = True
        If Label1.Text = "STATUS" Then
            ComboBox2.Enabled = True
            ComboBox2.Visible = True
            TextBox1.Visible = False

        Else
            ComboBox2.Enabled = False
            ComboBox2.Visible = False
            TextBox1.Visible = True

        End If
        Call forselect()
    End Sub
    Sub forselect()
        If ComboBox1.Text = "BOOK ID" Then
            sel = 1
        ElseIf ComboBox1.Text = "BOOK NAME" Then
            sel = 2
        ElseIf ComboBox1.Text = "AUTHOR" Then
            sel = 3
        ElseIf ComboBox1.Text = "STATUS" Then
            sel = 8
        End If
    End Sub

    Private Sub BookDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox2.Visible = False
        TextBox1.Visible = False
        Label1.Visible = False
        Call readData()
    End Sub
    Sub readData()
        ListView1.Clear()
        ListView1.Columns.Add("BOOK ID", 90, HorizontalAlignment.Center)

        ListView1.Columns.Add("BOOK NAME", 310, HorizontalAlignment.Center)
        ListView1.Columns.Add("PUBLISHER", 90, HorizontalAlignment.Center)
        ListView1.Columns.Add("AUTHOR", 90, HorizontalAlignment.Center)
        ListView1.Columns.Add("PUBLISHING YEAR", 130, HorizontalAlignment.Center)
        ListView1.Columns.Add("EDITION", 90, HorizontalAlignment.Center)

        ListView1.Columns.Add("STATUS", 90, HorizontalAlignment.Center)
        ListView1.View = View.Details
        sel = 5
        'Call whenclick()
    End Sub
    Sub whenclick()
        Try

            While dr.Read()
                Call adddatatolistview(ListView1, dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6))
            End While
            dr.Close()
            objcon.Close()
        Catch
            'MsgBox("Please Refresh", MsgBoxStyle.Information, "")
        End Try
    End Sub
    Public Sub adddatatolistview(ByVal lvw As ListView, ByVal BookID As String, ByVal BookName As String, ByVal publisher As String, ByVal author As String, ByVal pubyear As String, ByVal edi As String, ByVal status As String)
        Dim lv As New ListViewItem
        lvw.Items.Add(lv)
        lv.Text = BookID

        lv.SubItems.Add(BookName)
        lv.SubItems.Add(publisher)
        lv.SubItems.Add(author)
        lv.SubItems.Add(pubyear)
        lv.SubItems.Add(edi)

        lv.SubItems.Add(status)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Select Case (sel)
            Case 1
                com = New OleDb.OleDbCommand("select * from Books where BookID='" & TextBox1.Text & "'", objcon)
                dr = com.ExecuteReader
            Case 2
                com = New OleDb.OleDbCommand("select * from Books where BookName='" & TextBox1.Text & "'", objcon)
                dr = com.ExecuteReader
            Case 3
                com = New OleDb.OleDbCommand("select * from Books where Author='" & TextBox1.Text & "'", objcon)
                dr = com.ExecuteReader
            Case 5
                com = New OleDb.OleDbCommand("select * from Books", objcon)
                dr = com.ExecuteReader
            Case 8
                com = New OleDb.OleDbCommand("select * from Books where Status='" & ComboBox2.Text & "'", objcon)
                dr = com.ExecuteReader
        End Select
        Call readData()
        Call whenclick()
        objcon.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Selected = True Then
                ' ComboBox1.Text = ListView1.Items(i).SubItems(0).Text
                Exit For
            End If
        Next
        ListView1.Focus()
        ListView1.FullRowSelect = True
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        ' Dim selectedRow As SelectedListViewIte = Nothing

        If ListView1.SelectedItems.Count > 0 Then
            ' Access the selected row and print its contents

            'selectedRow = ListView1.SelectedRow(0)
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If ListView1.Items.Count > 0 Then
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
End Class