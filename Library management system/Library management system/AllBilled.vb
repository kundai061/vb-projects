Public Class AllBilled

    Private Sub AllBilled_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call readData()
    End Sub
    Sub readData()
        ListView1.Clear()
        ListView1.Columns.Add("BOOK ID", 90, HorizontalAlignment.Center)
        ListView1.Columns.Add("STUDENTID", 310, HorizontalAlignment.Center)
        ListView1.GridLines = True
        ListView1.View = View.Details
        Try

            If (objcon.State = ConnectionState.Closed) Then objcon.Open()
            com = New OleDb.OleDbCommand("SELECT * FROM Billing'", objcon)
            dr = com.ExecuteReader
            While dr.Read()
                Call adddatatolistview(ListView1, dr(1), dr(2))
            End While
            dr.Close()
            objcon.Close()
        Catch
            'MsgBox("Please Refresh", MsgBoxStyle.Information, "")
        End Try
    End Sub
    Public Sub adddatatolistview(ByVal lvw As ListView, ByVal STUDENTID As String, ByVal BookID As String)
        Dim lv As New ListViewItem
        lvw.Items.Add(lv)
        lv.Text = BookID

        lv.SubItems.Add(STUDENTID)
    End Sub
End Class