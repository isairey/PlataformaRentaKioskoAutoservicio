Public Class EquipmentItem
    Public Property EquipmentId As Integer
    Public Property Name As String
    Public Property Category As String
    Public Property DailyRate As Decimal
    Public Property TotalStock As Integer
    Public Property AvailStock As Integer
    Public Property IconTag As String
    Public Property IsActive As Boolean

    Public ReadOnly Property IsAvailable As Boolean
        Get
            Return AvailStock > 0 AndAlso IsActive
        End Get
    End Property
End Class
