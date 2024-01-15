Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.UploadyURL = TextBox1.Text.TrimEnd("/")
        Form1.APIKey = TextBox2.Text
        MsgBox("Settings Saved")
        Me.Close()
    End Sub
End Class