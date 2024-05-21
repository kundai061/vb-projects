Public Class AddStudent
    Public NameFrm, NameTo As String
    Public curr As String = "US"
    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown

    End Sub
    Private Sub TextBox4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.LostFocus
        TextBox4.Text = TextBox4.Text.Trim
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Me.Close()
    End Sub

    Private Sub TextBox2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.LostFocus
        NameFrm = TextBox2.Text
        Call Sentence()
        TextBox2.Text = NameTo
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub
    Sub Sentence()
        Dim a, b As Integer
        a = NameFrm.Length
        NameTo = ""
        For b = 0 To a - 1
            If b = 0 Then
                If Char.IsLower(NameFrm(0)) Then
                    NameTo = Char.ToUpper(NameFrm(0))
                Else
                    NameTo = NameFrm(0)
                End If
            Else
                If NameFrm(b - 1) = " " Then
                    NameTo = NameTo + Char.ToUpper(NameFrm(b))
                Else
                    NameTo = NameTo + NameFrm(b)
                End If
            End If
        Next
    End Sub


    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter a Student ID", 0, "")
        Else
            Try
                If objcon.State = ConnectionState.Closed Then objcon.Open()
                com = New OleDb.OleDbCommand("INSERT INTO Student (CID, CName, CContact) values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox4.Text & "')", objcon)
                If com.ExecuteNonQuery() Then MsgBox("Saved Success!", 0, "")
                ListView1.Clear()
                Call readData()
                objcon.Close()
                Call DisableThem()
            Catch ex As Exception
                MsgBox(ex.Message, 0, "")
            End Try
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter the ID to be deleted!", 0, "")
        Else
            Try
                objcon.Open()
                com = New OleDb.OleDbCommand("delete from Student where CID='" & TextBox1.Text & "'", objcon)
                If com.ExecuteNonQuery() Then
                    ListView1.Clear()
                    Call readData()
                    MsgBox("Deleted Success!", 0, "")
                Else
                    MsgBox("ID Not Found!", 0, "")
                End If

                objcon.Close()
            Catch ex As Exception
                MsgBox(ex.Message, 0, "")
            End Try
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call EnableThem()
        Call ClearField()
    End Sub
    Sub EnableThem()
        TextBox1.Enabled = True
        TextBox2.Enabled = True

        TextBox4.Enabled = True
    End Sub
    Sub DisableThem()
        'TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        
    End Sub

    Private Sub AddCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call EnableThem()
        Call readData()
    End Sub
    Sub readData()
        ListView1.Columns.Add("STUDENT ID", 90, HorizontalAlignment.Center)
        ListView1.Columns.Add("STUDENT NAME", 210, HorizontalAlignment.Center)
        ListView1.Columns.Add("CONTACT #", 90, HorizontalAlignment.Center)
        ListView1.Columns.Add("DATE", 130, HorizontalAlignment.Center)

        Try

            If (objcon.State = ConnectionState.Closed) Then objcon.Open()
            com = New OleDb.OleDbCommand("SELECT * FROM Student", objcon)
            dr = com.ExecuteReader
            While dr.Read()
                Call adddatatolistview(ListView1, dr(0), dr(1), dr(2), dr(3))
            End While
            dr.Close()
            objcon.Close()
        Catch
            'MsgBox("Please Refresh", MsgBoxStyle.Information, "")
        End Try
    End Sub
    Public Sub adddatatolistview(ByVal lvw As ListView, ByVal CID As String, ByVal CName As String, ByVal CCont As String, ByVal CDate_ As String)
        Dim lv As New ListViewItem
        lvw.Items.Add(lv)
        lv.Text = CID
        lv.SubItems.Add(CName)
        lv.SubItems.Add(CCont)
        lv.SubItems.Add(CDate_)
    End Sub
    Sub ClearField()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox4.Clear()
        
    End Sub
    Sub LoadInto()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Selected = True Then
                    TextBox1.Text = ListView1.Items(i - 1).SubItems(0).Text
                    Exit For
                End If
            Next
            ListView1.Focus()
            ListView1.FullRowSelect = True
        Catch ex As Exception
            MsgBox(ex.Message, 0, "")
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Selected = True Then
                    TextBox1.Text = ListView1.Items(i + 1).SubItems(0).Text
                    Exit For
                End If
            Next
            ListView1.Focus()
            ListView1.FullRowSelect = True
        Catch ex As Exception
            MsgBox(ex.Message, 0, "")
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Selected = True Then
                TextBox1.Text = ListView1.Items(i).SubItems(0).Text
                TextBox2.Text = ListView1.Items(i).SubItems(1).Text

                TextBox4.Text = ListView1.Items(i).SubItems(2).Text
                Exit For
            End If
        Next
        ListView1.Focus()
        ListView1.FullRowSelect = True
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim i As Integer
        ListView1.SelectedItems.Clear()
        TextBox1.Focus()
        Try
            If Me.TextBox1.Text = "" Then
                TextBox2.Text = ""
            Else
                For i = 0 To ListView1.Items.Count - 1
                    If TextBox1.Text = ListView1.Items(i).SubItems(0).Text Then
                        TextBox2.Text = ListView1.Items(i).SubItems(1).Text
                        ListView1.Items(i).Selected = True
                        Exit For
                    End If
                Next
            End If
        Catch

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter a Student ID", 0, "")
        Else
            Try
                If objcon.State = ConnectionState.Closed Then objcon.Open()
                com = New OleDb.OleDbCommand("UPDATE Student SET CID='" & TextBox1.Text & "', CName='" & TextBox2.Text & "', CContact='" & TextBox4.Text & "'", objcon)
                If com.ExecuteNonQuery() Then MsgBox("Update Success!", 0, "")
                ListView1.Clear()
                Call readData()
                objcon.Close()
                Call DisableThem()
            Catch ex As Exception
                MsgBox(ex.Message, 0, "")
            End Try
        End If
    End Sub
End Class
