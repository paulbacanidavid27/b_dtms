Imports System.Web.Security
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32

Public Class crypt
    Dim myKey As String
    Dim cryptDES3 As New TripleDESCryptoServiceProvider()
    Dim cryptMD5Hash As New MD5CryptoServiceProvider()
    Public Sub New()
        myKey = Chr(20) & Chr(11) & Chr(70) & Chr(10) & Chr(220) & Chr(11) & Chr(234) & Chr(110) & Chr(80) & Chr(130) & Chr(100) & Chr(4)
        'myKey = Chr(70) & Chr(20) & Chr(11) & Chr(10) & Chr(220) & Chr(11) & Chr(234) & Chr(110) & Chr(80) & Chr(130) & Chr(100) & Chr(4) --mwss
    End Sub
    Public Function Decrypt(ByVal myString As String) As String
        cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        cryptDES3.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = cryptDES3.CreateDecryptor()
        Dim buff() As Byte = Convert.FromBase64String(myString)
        Decrypt = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    End Function
    Public Function Encrypt(ByVal myString As String) As String
        cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
        cryptDES3.Mode = CipherMode.ECB
        Dim desdencrypt As ICryptoTransform = cryptDES3.CreateEncryptor()
        Dim MyASCIIEncoding = New ASCIIEncoding()
        Dim buff() As Byte = ASCIIEncoding.ASCII.GetBytes(myString)
        Encrypt = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    End Function
End Class
