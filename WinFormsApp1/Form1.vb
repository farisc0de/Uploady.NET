Imports System.IO
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Diagnostics

Public Class Form1
    Public UploadyURL As String = ""
    Public APIKey As String = ""
    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim client = New HttpClient()
        Dim request = New HttpRequestMessage(HttpMethod.Post, UploadyURL & "/api/upload")
        request.Headers.Add("X-API-KEY", APIKey)
        request.Headers.Add("HTTP_USER_AGENT", "Mozilla/5.0 (Linux; Android 11; Mi 11) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.5735.196 Mobile Safari/537.44")
        Dim content = New MultipartFormDataContent()
        content.Add(New StreamContent(File.OpenRead(TextBox1.Text)), "file", TextBox1.Text)
        request.Content = content
        Dim response = Await client.SendAsync(request)
        response.EnsureSuccessStatusCode()
        Dim fileinfo = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(Await response.Content.ReadAsStringAsync())
        LinkLabel1.Text = fileinfo("downloadlink")
        MsgBox(fileinfo("file_id") & "has been uploaded")
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim file As OpenFileDialog = New OpenFileDialog

        If file.ShowDialog <> file.ShowDialog.Yes Then
            TextBox1.Text = file.FileName
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim myProcess = New Process()
        myProcess.StartInfo.UseShellExecute = True
        myProcess.StartInfo.FileName = LinkLabel1.Text
        myProcess.Start()
    End Sub

    Private Sub APISettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles APISettingsToolStripMenuItem.Click
        Form2.Show()
    End Sub
End Class
