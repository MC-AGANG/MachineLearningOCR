Public Class Form2
    Public pic As New Bitmap(80, 32)
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Update()
    End Sub
    Public Sub Update()              '把模型数据渲染成图像
        For i = 0 To 9
            For x = 0 To 15
                For y = 0 To 15
                    pic.SetPixel((i * 16) Mod 80 + x, (i * 16 \ 80) * 16 + y, Color.FromArgb(255, 127 + Form1.number(i, x, y) \ 2 + Form1.number(i, x, y) Mod 2, 0, 127 - Form1.number(i, x, y) \ 2))
                Next
            Next
        Next
        PictureBox1.Image = pic
    End Sub
End Class