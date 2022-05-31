Public Class Form1
    Public number(9, 15, 15) As Integer               '储存模型数据
    Dim pic As New Bitmap(16, 16)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For x = 0 To 15
            For y = 0 To 15
                pic.SetPixel(x, y, Color.White)
            Next
        Next
        PictureBox1.Image = pic
        For i = 0 To 9
            For x = 0 To 15
                For y = 1 To 15
                    number(i, x, y) = 0
                Next
            Next
        Next
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If e.X >= 0 AndAlso e.X < 160 AndAlso e.Y >= 0 AndAlso e.Y < 160 AndAlso e.Button = MouseButtons.Left Then
            pic.SetPixel(e.X \ 10, e.Y \ 10, Color.Black)
            PictureBox1.Image = pic
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Right Then
            For x = 0 To 15
                For y = 0 To 15
                    pic.SetPixel(x, y, Color.White)
                Next
            Next
            PictureBox1.Image = pic
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click          '识别
        ListBox1.Items.Clear()
        Dim max As Integer
        Dim maxn As Integer
        Dim possibility As Integer
        For i = 0 To 9
            possibility = 0
            For x = 0 To 15
                For y = 0 To 15
                    If pic.GetPixel(x, y) = Color.FromArgb(255, 0, 0, 0) Then
                        possibility += number(i, x, y)
                    Else
                        possibility -= number(i, x, y)
                    End If
                Next
            Next
            If i = 0 Then
                max = possibility
                maxn = 0
            ElseIf possibility > max Then
                max = possibility
                maxn = i
            End If
            ListBox1.Items.Add(Str(i) + ": " + Str(possibility))
        Next
        Label2.Text = CStr(maxn)
        ListBox1.SelectedIndex = maxn
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click               '学习
        Dim a, b As Integer                                                                         '使用方法：点击列表框内需要校准的哪一项之后点击校准即可。
        a = ListBox1.SelectedIndex                                                                  '需要几百上千次的训练才能达到一定的精准度。
        b = Val(Label2.Text)
        If a <> b Then
            For x = 0 To 15
                For y = 0 To 15
                    If pic.GetPixel(x, y) = Color.FromArgb(255, 0, 0, 0) Then
                        number(a, x, y) += 3
                        number(b, x, y) -= 2
                    Else
                        number(a, x, y) -= 1
                        number(b, x, y) += 1
                    End If
                Next
            Next
        End If
        Form2.Update()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click       '模型使用二进制文件，编码规则为将三维数组number(,,)按次序以二进制形式输出
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then                                '每个元素占用4个字节。
            Dim fs As New IO.FileStream(OpenFileDialog1.FileName, IO.FileMode.Open)
            Dim reader As New IO.BinaryReader(fs)
            For i = 0 To 9
                For x = 0 To 15
                    For y = 0 To 15
                        number(i, x, y) = reader.ReadInt32
                    Next
                Next
            Next
            reader.Close()
        End If
        Form2.Update()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim fs As New IO.FileStream(SaveFileDialog1.FileName, IO.FileMode.Create)
            Dim writer As New IO.BinaryWriter(fs)
            For i = 0 To 9
                For x = 0 To 15
                    For y = 0 To 15
                        writer.Write(number(i, x, y))
                    Next
                Next
            Next
            writer.Flush()
            writer.Close()
        End If
    End Sub
End Class
