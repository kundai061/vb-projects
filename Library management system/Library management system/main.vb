Public Class main

    Private Sub AddBooksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddBooksToolStripMenuItem.Click
        AddBooks.Show()
    End Sub

    Private Sub IssueBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IssueBookToolStripMenuItem.Click
        IssueBook.Show()
    End Sub

    Private Sub ReturnBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnBookToolStripMenuItem.Click
        ReturnBook.Show()
    End Sub

    Private Sub BookReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BookReportToolStripMenuItem.Click
        BookDetail.Show()
    End Sub

    Private Sub ViewCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewCustomerToolStripMenuItem.Click
        StudentDetail.Show()
    End Sub

    Private Sub AddCustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddCustomerToolStripMenuItem.Click
        AddStudent.Show()
    End Sub

    Private Sub AllRentedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllRentedToolStripMenuItem.Click
        AllRented.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BillBookToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BillBookToolStripMenuItem.Click
        BillStudent.Show()
    End Sub

    Private Sub ViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewToolStripMenuItem.Click

    End Sub

    Private Sub AllBilledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllBilledToolStripMenuItem.Click
        AllBilled.Show()
    End Sub
End Class