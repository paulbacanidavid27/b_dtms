Imports System.Web.UI.DataVisualization.Charting

Public Class ucBarSeriesTotalDocuments
    Inherits System.Web.UI.UserControl

    Dim ToDate As String = ""
    Dim FromDate As String = ""
    Dim DocType As String = ""

    Public Property pDocType As String
        Get
            Return DocType
        End Get
        Set(ByVal value As String)
            DocType = value
        End Set
    End Property
    Public Property pFromDate As String
        Get
            Return FromDate
        End Get
        Set(ByVal value As String)
            FromDate = value
        End Set
    End Property

    Public Property pToDate As String
        Get
            Return ToDate
        End Get
        Set(ByVal value As String)
            ToDate = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub RetrieveChartData()
        PopulateChart()
    End Sub
    Private Sub ResetChart()
        For Each z As Series In Chart1.Series
            z.Dispose()
        Next
        Chart1.Series.Clear()
    End Sub
    Private Sub PopulateChart()
        Dim ldata As DataTable
        Try


            Dim oList As New DocList
            Dim currentseriesName As String = ""
            'ResetChart()
            oList.pCreatedDateTo = pToDate
            'Chart1.Series.Add("DocCount")
            'Chart1.Series("DocCount").BorderWidth = 2
            'Chart1.Series("DocCount").ShadowOffset = 2

            'Chart1.Series("DocCount").IsValueShownAsLabel = True


            ldata = oList.ChartDocTotalDocuments
            Dim ltotal As Integer = 0
            Chart1.DataSource = ldata
            Chart1.Series(0).XValueMember = "description"
            Chart1.Series(0).YValueMembers = "doccount"

            Chart1.DataBind()
            If Not ldata Is Nothing Then
                For Each lrow As DataRow In ldata.Rows
                    'Chart1.Series("DocCount").Points.AddXY(lrow("description"), CInt(lrow("doccount")))
                    ltotal += CInt(lrow("doccount"))
                Next
                'Chart1.Series("DocCount").LegendText = "Total Documents: " & ltotal.ToString
                Chart1.Titles(0).Text = "Total Documents Received As of " & pToDate & " - " & ltotal.ToString
            End If
            '    'Chart1.DataBind()
            '    Chart1.EnableViewState = True
            pTotalDocuments.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally
            If Not ldata Is Nothing Then
                ldata.dispose()

            End If
        End Try
    End Sub
End Class