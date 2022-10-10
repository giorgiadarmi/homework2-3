Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBox1.EnableAutoDragDrop = True
    End Sub

    Private Sub RichTextBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragDrop
        e.Effect = DragDropEffects.None

        Me.RichTextBox1.Clear()

        Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

        For Each path In files
            Me.ProcessaFile(path)
        Next
    End Sub

    Public Sub RichTextBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Sub ProcessaFile(path As String)
        Me.RichTextBox1.AppendText(path & Environment.NewLine)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Path As String = "C:\Users\giorgia.darmiento\source\repos\ReadWriteCSV (Debigging)\population_csv.csv"

        Dim ListOfUnits As New List(Of Country)


        Using R As New TextFieldParser(Path)

            R.Delimiters = New String() {","}

            Dim NamesOfVariables As String = R.ReadLine

            Do While Not R.EndOfData

                Dim Values() As String = R.ReadFields()

                Dim Country As New Country

                'fille the fields with actual data

                Country.CountryName = Values(0)
                Country.CountryCode = Values(1)
                Country.Year = Values(2)
                Country.Value = Values(3)

                ListOfUnits.Add(Country)
            Loop

        End Using

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim o As New OpenFileDialog
        o.ShowDialog()

        If String.IsNullOrWhiteSpace(o.FileName) Then Exit Sub
        Me.RichTextBox1.Text = o.FileName

    End Sub
End Class

Public Class Country
    Public CountryName As String
    Public CountryCode As String
    Public Year As String
    Public Value As String
End Class