Public Class Form1
    Dim con As New OleDb.OleDbConnection
    Dim _gender As String
    Public Sub gender(ByVal gender As String)
        If gender = "female" Then
            RadioButton2.Checked = True

        Else
            RadioButton1.Checked = True

        End If
    End Sub

    Private Sub Form1_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged
        Button3.BackColor = Color.Gray
    End Sub


    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        LoginForm1.Show()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Button3.Enabled = False
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\clinic.mdb"
        loadData()
        ComboBox1.Items.AddRange({
            "Aspirin",
            "Ibuprofen",
            "Acetaminophen",
            "Amoxicillin",
            "Lisinopril",
            "Metformin",
            "Simvastatin",
            "Levothyroxine",
            "Omeprazole",
            "Atorvastatin"
        })

        ComboBox2.Items.AddRange({
            "Dr jones",
            "Dr Chimpeni",
            "Dr Ziwada",
            "Dr Kuchidza"
        })
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If issetInput() Then
            Dim result As Integer
            Dim cmd As New OleDb.OleDbCommand

            Try
                Dim sql As String

                sql = "INSERT INTO patients(fname, surname, " &
                    " id_number, gender, date_of_admission, drugs_prescribed, " &
                    " ward_number, bed_number, doctor_on_duty) " &
                    " VALUES('" & TextBox1.Text & "'," &
                    "'" & TextBox2.Text & "','" & TextBox3.Text & "'," &
                    "'" & _gender & "'," & "#" & DateValue(DateTimePicker1.Value) & "#" & "," &
                    "'" & ComboBox1.Text & "'," & Val(TextBox5.Text) & "," &
                    "" & Val(TextBox6.Text) & ",'" & ComboBox2.Text & "' )"

                con.Open()

                With cmd
                    .Connection = con
                    .CommandText = sql

                    result = cmd.ExecuteNonQuery()

                    If result = 0 Then
                        MsgBox("No Data has been Inserted!")
                    Else
                        Form5.Label4.Text = "Billing For " & TextBox1.Text
                        Form5.Show()
                        Me.Hide()

                    End If
                End With

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Information)
            Finally

                con.Close()

            End Try
            Call loadData()
        Else
            MsgBox("PLEASE ENTER ALL DETAILS", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Reset_Data()
    End Sub
    Public Sub Reset_Data()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        TextBox5.Clear()
        TextBox6.Clear()
        ComboBox2.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub

    Private Sub loadData()

        Dim publictable As New DataTable
        Dim cmd As New OleDb.OleDbCommand
        Dim da As New OleDb.OleDbDataAdapter

        Dim sql As String = "SELECT ID as [ID], fname as [Name],surname as [Surname], id_number, gender, date_of_admission, drugs_prescribed, ward_number, bed_number, doctor_on_duty" &
                             " FROM patients ORDER BY ID DESC;"

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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Label10.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        gender(DataGridView1.CurrentRow.Cells(4).Value.ToString)
        DateTimePicker1.Value = DataGridView1.CurrentRow.Cells(5).Value
        ComboBox1.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString
        TextBox5.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString
        TextBox6.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString
        ComboBox2.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim cmd As New OleDb.OleDbCommand
        Dim sql As String = "Delete from patients where ID=" & Val(Label10.Text)
        Dim result As Integer

        Try
            con.Open()
            With cmd
                .Connection = con
                .CommandText = sql
                result = cmd.ExecuteNonQuery
                con.Close()

                If result = 0 Then
                    MsgBox("No Data has been Deleted!", MsgBoxStyle.Critical
                           )
                Else
                    MsgBox("Data is deleted succesfully!")
                    Call loadData()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally

            
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim result As Integer
        Dim cmd As New OleDb.OleDbCommand

        Try

            Dim sql As String = "UPDATE patients SET fname = '" & TextBox1.Text & "', " &
                    "surname = '" & TextBox2.Text & "', " &
                    "id_number = '" & TextBox3.Text & "', " &
                    "gender = '" & _gender & "', " &
                    "date_of_admission = #" & DateValue(DateTimePicker1.Value) & "#, " &
                    "drugs_prescribed = '" & ComboBox1.Text & "', " &
                    "ward_number = " & Val(TextBox5.Text) & ", " &
                    "bed_number = " & Val(TextBox6.Text) & ", " &
                    "doctor_on_duty = '" & ComboBox2.Text & "' " &
                    "WHERE ID= " & Val(Label10.Text) & ""

            con.Open()

            With cmd
                .Connection = con
                .CommandText = sql
                result = cmd.ExecuteNonQuery

                If result = 0 Then
                    MsgBox("No Data has been Updated!", MsgBoxStyle.Critical
                           )
                Else

                    MsgBox("is Updated succesfully!", MsgBoxStyle.Information
                           )

                End If
            End With

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally

            con.Close()

        End Try
        Call loadData()
    End Sub
    Private Function issetInput() As Boolean
        If TextBox1.Text <> "" And TextBox2.Text <> "" And RadioButton1.Checked Or RadioButton2.Checked And TextBox3.Text <> "" And ComboBox1.Text <> "" And TextBox5.Text <> "" And TextBox6.Text <> "" And ComboBox2.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton2.Checked Then
            RadioButton1.Checked = False
        End If
        _gender = "male"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked Then
            RadioButton2.Checked = False
        End If
        _gender = "female"
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form3.Show()

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If issetInput() Then
            accounts.Label1.Text = "PAYMENTS FROM " & TextBox1.Text & " " & TextBox2.Text & " ID : " & TextBox3.Text
            accounts.Show()
        Else
            MsgBox("Select a entry first", MsgBoxStyle.Information)
        End If
    End Sub
End Class