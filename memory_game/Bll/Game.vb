Imports System.IO
Imports System.Threading
Imports System.Windows.Forms.Control

Public Class Game

    Private _listCards As New List(Of Card)
    Private _listImg As New List(Of Image)
    Private _imgDef As Image
    Private _matrix As New List(Of List(Of Card))
    Private _firstCard As Card

    Private _dificult As Integer = 2

    Public Property Dificult() As Integer
        Get
            Return _dificult

        End Get
        Set(ByVal value As Integer)
            _dificult = value
        End Set
    End Property

    Private _match As Integer
    Private _error As Integer
    Private WithEvents _lblMatches As Label
    Private WithEvents _lblErrors As Label

    Public Sub New(ByRef control As ControlCollection)

        _imgDef = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\Images\Default\logo.jpg")

        Dim directory = New DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\Images")
        Dim files = directory.GetFiles()

        For index = 0 To files.Length - 1
            _listImg.Add(Image.FromFile(files(index).FullName))
        Next

        For index = 0 To _dificult * 2 - 1
            Dim card = New Card(index, _imgDef, Me)
            _listCards.Add(card)
            control.Add(card.ComponentView)
        Next

        _lblMatches = New Label With {
            .AutoSize = True,
            .Location = New Point(81, 5),
            .TabIndex = 1,
            .Size = New Size(13, 13)
        }

        _lblErrors = New Label With {
            .AutoSize = True,
            .Location = New Point(160, 5),
            .TabIndex = 1,
            .Size = New Size(13, 13)
        }

        control.Add(_lblMatches)
        control.Add(_lblErrors)

    End Sub

    Private Sub Match(Optional value As Integer? = Nothing)

        _match = IIf(value Is Nothing, _match + 1, value)
        _lblMatches.Invoke(Function()
                               _lblMatches.Text = _match.ToString()
                               Return Nothing
                           End Function)
    End Sub

    Private Sub Errors(Optional value As Integer? = Nothing)
        _error = IIf(value Is Nothing, _error + 1, value)
        _lblErrors.Invoke(Function()
                              _lblErrors.Text = _error.ToString()
                              Return Nothing
                          End Function)
    End Sub


    Public Sub Validation(card As Card)

        card.DisableCard()

        If _firstCard Is Nothing Then
            _firstCard = card
        Else
            Thread.Sleep(100)
            If _firstCard.Img.Equals(card.Img) Then
                Match()
                card.DisableCard()
            Else
                Errors()
                _firstCard.FlipCard()
                card.FlipCard()
                _firstCard.ActiveCard()
                card.ActiveCard()
            End If
            _firstCard = Nothing
        End If
    End Sub

    Public Sub Reset()
        Match(0)
        Errors(0)

        Dim listInt = New List(Of Integer)

        For index = 0 To _dificult * 2 - 1
            Dim card = New Card(index, _imgDef, Me)
            _listCards.Add(card)
            listInt.Add(index)
        Next


        Dim random = New Random()
        For index = 0 To _dificult - 1
            Dim list = New List(Of Card)
            Dim img = _listImg(index)
            Dim randomOne = random.Next(listInt.Count - 1)
            Dim numberOne = listInt(randomOne)
            _listCards(numberOne).Img = _listImg(index)
            listInt.RemoveAt(randomOne)
            RemoveHandler _listCards(numberOne).ComponentView.Click, AddressOf _listCards(numberOne).ptb_Click
            AddHandler _listCards(numberOne).ComponentView.Click, AddressOf _listCards(numberOne).ptb_Click
            list.Add(_listCards(numberOne))

            Dim randomTwo = random.Next(listInt.Count - 1)
            Dim numberTwo = listInt(randomTwo)
            _listCards(numberTwo).Img = _listImg(index)
            listInt.RemoveAt(randomTwo)
            RemoveHandler _listCards(numberTwo).ComponentView.Click, AddressOf _listCards(numberTwo).ptb_Click
            AddHandler _listCards(numberTwo).ComponentView.Click, AddressOf _listCards(numberTwo).ptb_Click
            list.Add(_listCards(numberTwo))

            _matrix.Add(list)

            _listCards(numberOne).Status = True
            _listCards(numberOne).ResetStatusCard()

            _listCards(numberTwo).Status = True
            _listCards(numberTwo).ResetStatusCard()

        Next
    End Sub
End Class
