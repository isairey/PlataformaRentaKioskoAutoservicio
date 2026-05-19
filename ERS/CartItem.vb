Public Class CartItem
    Public Property Equipment As EquipmentItem
    Public Property Quantity As Integer

    Public Function LineTotal(days As Integer) As Decimal
        Return Equipment.DailyRate * Quantity * days
    End Function
End Class
