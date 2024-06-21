Public Class CardPair
    Private _cardOne As Card

    Public Property CardOne() As Card
        Get
            Return _cardOne

        End Get
        Set(ByVal value As Card)
            _cardOne = value
        End Set
    End Property

    Private _cardTwo As String

    Public Property CardTwo() As String
        Get
            Return _cardTwo

        End Get
        Set(ByVal value As String)
            _cardTwo = value
        End Set
    End Property
End Class
