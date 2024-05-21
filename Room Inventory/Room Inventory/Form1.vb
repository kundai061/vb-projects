Imports System.Data.OleDb

Public Class Form1
    Dim connection As OleDb.OleDbConnection
    Dim connectionStr As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\users\admin\documents\visual studio 2013\Projects\Room Inventory\Room Inventory\Database7.mdb")
    
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub refresh_grid()

        connection = New OleDbConnection(connectionStr)
        Dim dt As New DataTable
        connection.Open()

        Dim command As New OleDbCommand("select ID, Namee as [Name], COST, Datee as  [Date] from items;", connection)
        Dim da As New OleDbDataAdapter(command)

        command.ExecuteNonQuery()
        da.Fill(dt)
        DataGridView1.DataSource = dt
        connection.Close()

        Dim nn = P
        Chart1.DataSource = dt
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.BackColor = Color.SeaShell
        refresh_grid()
    End Sub

    Private Sub submit_data()
        connection = New OleDbConnection(connectionStr)

        Dim dt As New DataTable
        connection.Open()

        Dim command As New OleDbCommand("INSERT INTO items (Namee, Cost, Datee) VALUES (@name, @cost, @date);", connection)
        Dim prop = command.Parameters
        prop.AddWithValue("@name", TextBox1.Text)
        prop.AddWithValue("@cost", TextBox2.Text)
        prop.AddWithValue("@date", DateValue(DateTimePicker1.Value))
        command.ExecuteNonQuery()

        Connection.Close()
        refresh_grid()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        submit_data()
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        If (TextBox1.Text.Length = 0) Or (TextBox2.Text.Length = 0) Then
            Return
        End If
        submit_data()
    End Sub

    Private Sub delete_row(index As Integer)
        connection = New OleDbConnection(connectionStr)

        Dim dt As New DataTable
        connection.Open()

        Dim command As New OleDbCommand("DELETE FROM items where ID=@ID;", connection)
        Dim prop = command.Parameters
        prop.AddWithValue("@ID", index)
        command.ExecuteNonQuery()
        connection.Close()
        refresh_grid()

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim datagrid = DataGridView1
        Dim currow = datagrid.CurrentRow
        Dim cursel = currow.Cells
        
        Try
            If datagrid.RowCount = 0 Then
                Throw New Exception("Hello no deleting")
            End If
            Dim value = ArrayList.Adapter(cursel)
            delete_row(value(0).Value)
        Catch ex As Exception

        End Try


        Return

        'TextBox1.AutoCompleteCustomSource.Add(String.Format("{0:s}", cursel.Value.ToString()))
        'ComboBox1.Items.Add(String.Format("{0:s}", cursel.Value.ToString()))
        'ContextMenuStrip1.Items.Add(String.Format("{0:s}", cursel.Value.ToString()))
        'ContextMenuStrip1.Refresh()

    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As EventArgs) Handles ComboBox1.Click
        ComboBox1.Items.Remove(ComboBox1.SelectedItem)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim timer As New Timer(Me)
    End Sub
End Class
