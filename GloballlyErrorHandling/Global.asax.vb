Imports System.Web.Optimization
Imports System.Web.Mvc

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Private Sub MvcApplication_Error(sender As Object, e As EventArgs) Handles Me.Error

        Dim httpContext = DirectCast(sender, MvcApplication).Context

        Dim currentRouteData = RouteTable.Routes.GetRouteData(New HttpContextWrapper(httpContext))
        Dim currentController = " "
        Dim currentAction = " "

        Dim ex = Server.GetLastError()
        'log something here

        
        If currentRouteData IsNot Nothing Then
            If currentRouteData.Values("controller") IsNot Nothing AndAlso Not [String].IsNullOrEmpty(currentRouteData.Values("controller").ToString()) Then
                currentController = currentRouteData.Values("controller").ToString()
            End If

            If currentRouteData.Values("action") IsNot Nothing AndAlso Not [String].IsNullOrEmpty(currentRouteData.Values("action").ToString()) Then
                currentAction = currentRouteData.Values("action").ToString()
            End If
        End If

        Dim controller = New ErrorController()
        Dim routeData = New RouteData()
        Dim action = "Index"
        If New HttpRequestWrapper(System.Web.HttpContext.Current.Request).IsAjaxRequest() Then
            Diagnostics.Debug.Write("is ajax")
            action = "JsonResult"
        Else
            Diagnostics.Debug.Write("is not ajax")
            If TypeOf ex Is HttpException Then
                Dim httpEx = TryCast(ex, HttpException)

                Select Case httpEx.GetHttpCode()
                    Case 404
                        action = "NotFound"
                        Exit Select
                    Case Else

                        ' others if any

                        action = "Index"
                        Exit Select
                End Select
            End If
        End If
        httpContext.ClearError()
        httpContext.Response.Clear()
        httpContext.Response.StatusCode = If(TypeOf ex Is HttpException, DirectCast(ex, HttpException).GetHttpCode(), 500)
        httpContext.Response.TrySkipIisCustomErrors = True
        routeData.Values("controller") = "Error"
        routeData.Values("action") = action

        controller.ViewData.Model = New HandleErrorInfo(ex, currentController, currentAction)
        DirectCast(controller, IController).Execute(New RequestContext(New HttpContextWrapper(httpContext), routeData))


    End Sub


End Class

