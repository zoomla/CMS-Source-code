Public Class ZoomLa_Class
    Public Shared Function ObjVer(ByVal a As String) As String
        Dim b As String
        b = ""
        Try
            Dim c = System.Web.HttpContext.Current.Server.CreateObject(a)
            b = c.version
        Catch exception1 As Exception
        End Try
        Return b
    End Function
End Class