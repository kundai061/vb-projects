Public Class accounts
    Dim con As New OleDb.OleDbConnection
    Private Sub loadData()

        Dim publictable As New DataTable
        Dim cmd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter
        Dim sql As String = "SELECT ID, consult_fee, method, drug_fee, bed_fee, user_id, rdate as [Date]" &
                             " FROM accounts WHERE user_id = " & Val(Form1.Label10.Text) & ";"

        Try

            con.Open()
            With cmd
                .Connection = con
                .CommandText = sql
            End With

            da.SelectCommand = cmd
            da.Fill(publictable)
            DataGridView1.DataSource = publictable
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

            con.Close()
            da.Dispose()

        End Try
    End Sub
    Private Sub searchData()

        Dim publictable As New DataTable
        Dim cmd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter

        Dim sql As String = "SELECT ID, consult_fee, method, drug_fee, bed_fee, user_id, rdate as [Date]" &
                             " FROM accounts WHERE rdate = #" & DateValue(DateTimePicker1.Value) & "# AND user_id = " & Val(Form1.Label10.Text) & ";"

        Try

            con.Open()
            With cmd
                .Connection = con
                .CommandText = sql
            End With

            da.SelectCommand = cmd
            da.Fill(publictable)
            DataGridView1.DataSource = publictable
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

            con.Close()
            da.Dispose()

        End Try
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\clinic.mdb"
        loadData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        searchData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        'Form1.Label10.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        'Form1.TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        'Form1.TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        'Form1.TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        'Form1.gender(DataGridView1.CurrentRow.Cells(4).Value.ToString)
        'Form1.DateTimePicker1.Value = DataGridView1.CurrentRow.Cells(5).Value.ToString
        'Form1.ComboBox1.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString
        'Form1.TextBox5.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString
        'Form1.TextBox6.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString
        'Form1.ComboBox2.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        searchData()
    End Sub
End Class