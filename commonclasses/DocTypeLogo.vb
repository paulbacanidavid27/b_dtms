Public Class DocTypeLogo

    Public Shared Function DocTypeBitMap(ByVal docExt As String) As String
        Dim lext As String = "images/doctype/"

        If (docExt = ".doc" OrElse docExt = "docx") Then
            lext = lext & "doc.png"
        ElseIf (docExt = ".xls" OrElse docExt = "xlsx") Then
            lext = lext & "excel.png"
        ElseIf (docExt = ".ppt" OrElse docExt = "pptx") Then
            lext = lext & "ppt.png"
        ElseIf (docExt = ".jpg" OrElse docExt = "jpeg") Then
            lext = lext & "jpg.png"
        ElseIf (docExt = ".tif" OrElse docExt = "tiff") Then
            lext = lext & "tif.png"
        ElseIf (docExt = ".gif") Then
            lext = lext & "gif.png"
        ElseIf (docExt = ".bmp") Then
            lext = lext & "bmp.png"
        ElseIf (docExt = ".png") Then
            lext = lext & "png.png"
        ElseIf (docExt = ".pdf") Then
            lext = lext & "pdf.png"
        ElseIf (docExt = ".zip") OrElse (docExt = ".rar") Then
            lext = lext & "zip.png"
        Else
            lext = lext & "dms.png"
        End If
        Return lext
    End Function

    

End Class
