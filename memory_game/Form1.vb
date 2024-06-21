Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1

    Private _game As Game

    Private Sub Ptb_Click(ptb As PictureBox, e As EventArgs)

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _game = New Game(Me.Controls)
        _game.Reset()

    End Sub

    Private Sub Reset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        _game.Reset()
    End Sub

    Private Sub lblMatch_Click(sender As Object, e As EventArgs)
    End Sub
End Class
