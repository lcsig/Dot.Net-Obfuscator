Imports Mono.Cecil

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim x As New SaveFileDialog
        x.Filter = ".Net Executable Files|*.exe"
        x.Title = "Choose a File"
        If x.ShowDialog = Windows.Forms.DialogResult.OK Then
            Encrypt(AssemblyDefinition.ReadAssembly(TextBox1.Text), CheckBox1.Checked).Write(x.FileName)
            MessageBox.Show("Done Successfully!", "Congrats!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("File won't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub OpenFileBtn_Click(sender As Object, e As EventArgs) Handles OpenFileBtn.Click
        Dim x As New OpenFileDialog
        x.Filter = ".Net Executable Files|*.exe"
        x.Title = "Choose a File"
        x.Multiselect = False
        If x.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = x.FileName
        End If
    End Sub

    Private Function Encrypt(srcFile As AssemblyDefinition, sameNameForMethods As Boolean) As AssemblyDefinition
        Dim rand As New Random(Now.Millisecond)
        Dim moddef As ModuleDefinition = srcFile.MainModule
        Dim methodsName As String = ""
        If sameNameForMethods Then
            methodsName = GenerateRandomString(rand.Next(10, 30))
        End If

        For Each td As TypeDefinition In moddef.Types
            For Each fd As FieldDefinition In td.Fields
                fd.Name = GenerateRandomString(rand.Next(10, 30))
            Next

            If td.Namespace.Contains(".My") = False Then
                For Each method As MethodDefinition In td.Methods
                    If method.IsPInvokeImpl = False AndAlso method.IsConstructor = False Then
                        If sameNameForMethods Then
                            method.Name = methodsName
                        Else
                            method.Name = GenerateRandomString(rand.Next(10, 30))
                        End If

                    End If
                Next
            End If

        Next

        Return srcFile
    End Function

    Public Function GenerateRandomString(len As Long)
        Dim rnd As New Random(Now.Millisecond)
        Dim charSet As String = "ابجدهوزحطيكلمنسعفصقرشت"
        Dim result As String = ""

        For i = 0 To len
            result = result & charSet(rnd.Next(charSet.Length))
        Next

        Return result
    End Function

End Class
