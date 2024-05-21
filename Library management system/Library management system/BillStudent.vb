Public Class BillStudent

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Selected = True Then
                ComboBox1.Text = ListView1.Items(i).SubItems(0).Text
                Exit For
            End If
        Next
        ListView1.Focus()
        ListView1.FullRowSelect = True
    End Sub

    Private Sub BillStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Retrive_C()
        Call BookID_Combo()
        Call readData()
    End Sub
    Sub Retrive_C()
        Try
            If objcon.State = ConnectionState.Closed Then objcon.Open()
            com = New OleDb.OleDbCommand("Select CID from Student", objcon)
            dr = com.ExecuteReader
            While dr.Read
                ComboBox5.Items.Add(dr.Item(0))
            End While
            dr.Close()
            objcon.Close()
        Catch ex As Exception

        End Try
    End Sub
    Sub BookID_Combo()
        Try
            If objcon.State = ConnectionState.Closed Then objcon.Open()
            com = New OleDb.OleDbCommand("Select BookID from Books WHERE status='Rented' AND ", objcon)
            dr = com.ExecuteReader
            While dr.Read
                ComboBox1.Items.Add(dr.Item(0))
            End While
            dr.Close()
            objcon.Close()
        Catch ex As Exception

        End Try
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
        ListView1.GridLines = True
        ListView1.View = View.Details
        Try

            If (objcon.State = ConnectionState.Closed) Then objcon.Open()
            com = New OleDb.OleDbCommand("SELECT * FROM Books WHERE status='Rented'", objcon)
            dr = com.ExecuteReader
            While dr.Read()
                Call adddatatolistview(ListView1, dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6))
            End While
            dr.Close()
            objcon.Close()
        Catch
            'MsgBox("Please Refresh", MsgBoxStyle.Information, "")
        End Try
    End Sub
    Public Sub adddatatolistview(ByVal lvw As ListView, ByVal BookID As String, ByVal BookName As String, ByVal Publisher As String, ByVal Author As String, ByVal PubYear As String, ByVal edi As String, ByVal st As String)
        Dim lv As New ListViewItem
        lvw.Items.Add(lv)
        lv.Text = BookID
        'lv.SubItems.Add(GroupID)
        lv.SubItems.Add(BookName)
        lv.SubItems.Add(Publisher)
        lv.SubItems.Add(Author)
        lv.SubItems.Add(PubYear)
        lv.SubItems.Add(edi)
        'lv.SubItems.Add(pric)
        lv.SubItems.Add(st)
    End Sub
    Sub Retrive()
        objcon.Open()
        com = New OleDb.OleDbCommand("SELECT * FROM Books", objcon)
        com.ExecuteNonQuery()
        dr = com.ExecuteReader
        dr.Read()
        While (dr.NextResult)
            ComboBox1.Items.Add(dr(1))
        End While
        objcon.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.Text <> "" And ComboBox5.Text <> "" Then
            Try
                If objcon.State = ConnectionState.Closed Then objcon.Open()
                com = New OleDb.OleDbCommand("UPDATE Books SET status='Available' WHERE BookID='" & ComboBox1.Text & "'", objcon)
                com.ExecuteNonQuery()
                objcon.Close()
                Call readData()
                If objcon.State = ConnectionState.Closed Then objcon.Open()
                com = New OleDb.OleDbCommand("INSERT INTO Billing (BOOKID,STUDENTID)VALUES('" & ComboBox1.Text & "','" & ComboBox5.Text & "')", objcon)
                com.ExecuteNonQuery()
                MsgBox("Book has been Billed!", 0, "")
                Call readData()
                objcon.Close()
            Catch ex As Exception
                MsgBox(ex.Message, 0, "")
            End Try
        Else
            MsgBox("please fill all details", MsgBoxStyle.Critical)
        End If

    End Sub
End Class