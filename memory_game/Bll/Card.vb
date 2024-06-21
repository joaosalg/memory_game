Imports System.IO
Imports System.Reflection
Imports System.Threading

Public Class Card

    Private _img As Image

    Public Property Img() As Image
        Get
            Return _img

        End Get
        Set(ByVal value As Image)
            _img = value
        End Set
    End Property

    Private _imgDefault As Image

    Public Property ImgDefault() As Image
        Get
            Return _imgDefault

        End Get
        Set(ByVal value As Image)
            _imgDefault = value
        End Set
    End Property

    Private _status As Boolean = False

    Public Property Status() As Boolean
        Get
            Return _status

        End Get
        Set(ByVal value As Boolean)
            _status = value
        End Set
    End Property

    Private _success As Boolean = False

    Public Property Success() As Boolean
        Get
            Return _success

        End Get
        Set(ByVal value As Boolean)
            _success = value
        End Set
    End Property

    Private _componentView As PictureBox

    Public Property ComponentView() As PictureBox
        Get
            Return _componentView

        End Get
        Set(ByVal value As PictureBox)
            _componentView = value

        End Set
    End Property

    Private _game As Game

    Public Sub New(index As Integer, imgNew As Image, game As Game)
        _game = game
        ImgDefault = imgNew
        _componentView = New PictureBox With {
                .Name = "ptb" + index.ToString("00"),
                .Location = New Point(50 + (150 * index), 50),
                .Size = New Size(100, 100),
                .SizeMode = PictureBoxSizeMode.StretchImage}

    End Sub

    Public Sub ActiveCard()
        RemoveHandler _componentView.Click, AddressOf ptb_Click
        AddHandler _componentView.Click, AddressOf ptb_Click
    End Sub

    Public Sub DisableCard()
        RemoveHandler _componentView.Click, AddressOf ptb_Click
    End Sub

    Public Sub ResetStatusCard()
        _status = True
        FlipCard()
    End Sub

    Public Sub ptb_Click(ptb As Object, e As EventArgs)
        Task.Run(Function()
                     FlipCard()
                     _game.Validation(Me)
                     Return Nothing
                 End Function)
    End Sub

    Public Sub FlipCard()
        If Not Success Then
            _status = Not _status
            Dim pathImg = AppDomain.CurrentDomain.BaseDirectory

            If _status Then
                ComponentView.Image = Img
            Else
                ComponentView.Image = ImgDefault
            End If

        End If

    End Sub

End Class
