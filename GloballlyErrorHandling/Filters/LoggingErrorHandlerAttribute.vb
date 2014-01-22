'Public Class LoggingErrorHandlerAttribute
'    Inherits HandleErrorAttribute

'    'Private ReadOnly _logger As ILog

'    Public Sub New()
'        '_logger = LogManager.GetLogger("MyLogger")
'    End Sub

'    Public Overrides Sub OnException(filterContext As ExceptionContext)
'        If filterContext.ExceptionHandled OrElse Not filterContext.HttpContext.IsCustomErrorEnabled Then
'            Return
'        End If

'        If New HttpException(Nothing, filterContext.Exception).GetHttpCode() <> 500 Then
'            Return
'        End If

'        If Not ExceptionType.IsInstanceOfType(filterContext.Exception) Then
'            Return
'        End If

'        ' if the request is AJAX return JSON else view.
'        If filterContext.HttpContext.Request.Headers("X-Requested-With") = "XMLHttpRequest" Then
'            filterContext.Result = New JsonResult() With { _
'                 .JsonRequestBehavior = JsonRequestBehavior.AllowGet, _
'                 .Data = New With { _
'                     .[error] = True, _
'                     .message = filterContext.Exception.Message _
'                } _
'            }
'        Else
'            Dim controllerName = DirectCast(filterContext.RouteData.Values("controller"), String)
'            Dim actionName = DirectCast(filterContext.RouteData.Values("action"), String)
'            Dim model = New HandleErrorInfo(filterContext.Exception, controllerName, actionName)

'			filterContext.Result = New ViewResult() With { _
'				Key .ViewName = View, _
'				Key .MasterName = Master, _
'				Key .ViewData = New ViewDataDictionary(Of HandleErrorInfo)(model), _
'				Key .TempData = filterContext.Controller.TempData _
'			}
'        End If

'        ' log the error using log4net.
'        _logger.[Error](filterContext.Exception.Message, filterContext.Exception)

'        filterContext.ExceptionHandled = True
'        filterContext.HttpContext.Response.Clear()
'        filterContext.HttpContext.Response.StatusCode = 500

'        filterContext.HttpContext.Response.TrySkipIisCustomErrors = True
'    End Sub


'End Class
