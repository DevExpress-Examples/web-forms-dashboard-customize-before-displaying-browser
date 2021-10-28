Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.Sql
Imports System

Namespace WebApplication7

    Public Partial Class [Default]
        Inherits Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ASPxDashboard1.AllowExecutingCustomSql = True
            ASPxDashboard1.SetDashboardStorage(New DashboardFileStorage("~/App_Data/Dashboards"))
        End Sub

        Protected Sub ASPxDashboard1_DashboardLoading(ByVal sender As Object, ByVal e As DevExpress.DashboardWeb.DashboardLoadingWebEventArgs)
            If e.DashboardId Is "dashboard0" Then
                Dim dashboard As Dashboard = New Dashboard()
                dashboard.LoadFromXDocument(e.DashboardXml)
                ' customization code
                Dim parameterCountry = dashboard.Parameters.FirstOrDefault(Function(p) p.Name Is "CountryDashboardParameter")
                If parameterCountry IsNot Nothing Then
                    parameterCountry.Value = "Germany"
                End If

                Dim nwindDataSource = dashboard.DataSources.OfType(Of DashboardSqlDataSource)().FirstOrDefault(Function(ds) ds.Name Is "NwindDataSource")
                If nwindDataSource IsNot Nothing Then
                    Dim customNwindQuery = nwindDataSource.Queries.OfType(Of CustomSqlQuery)().FirstOrDefault(Function(q) q.Name Is "CustomInvoicesQuery")
                    If customNwindQuery IsNot Nothing Then
                        customNwindQuery.Sql += " AND (Invoices.OrderDate >= #2015-01-01 00:00:00#)"
                    End If
                End If

                '...
                e.DashboardXml = dashboard.SaveToXDocument()
            End If
        End Sub
    End Class
End Namespace
