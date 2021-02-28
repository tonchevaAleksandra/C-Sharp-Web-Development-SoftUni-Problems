using System;
using SIS.HTTP;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SIS.HTTP.Response;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static async Task StartAsync(IMvcApplication application)
        {
            var routeTable = new List<Route>();
            application.Configure(routeTable);
            application.ConfigureServices();
            AutoRegisterStaticFilesRoutes(routeTable);
            AutoRegisterActionRoutes(routeTable, application);
            Console.WriteLine("Registered routes:");
            foreach (var route in routeTable)
            {
                Console.WriteLine(route);
            }

            Console.WriteLine();
            Console.WriteLine("Requests:");
            var httpServer = new HttpServer(80, routeTable);
            await httpServer.StartAsync();
        }

        private static void AutoRegisterActionRoutes(List<Route> routeTable, IMvcApplication application)
        {
            var types = application.GetType().Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)) && !t.IsAbstract);
            foreach (var type in types)
            {
                Console.WriteLine(type.GetType().FullName);
                var methods = type.GetMethods().Where(x => !x.IsSpecialName
                                                           && !x.IsConstructor
                                                           && x.IsPublic
                                                           && x.DeclaringType == type);
                foreach (var methodInfo in methods)
                {

                    string url = "/" + type.GetType().Name.Replace("Controller", "/") + methodInfo.GetType().Name;
                    var attribute = methodInfo.GetCustomAttributes()
                        .FirstOrDefault(x => x.GetType().IsSubclassOf(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                    var httpActionType = HttpMethodType.Get;
                    if (attribute!=null)
                    {
                        httpActionType = attribute.Type;
                        if (attribute.Url != null)
                        {
                            url = attribute.Url;
                        }
                    }

                    routeTable.Add(new Route(url,httpActionType, (request) =>
                    {
                        // instance of controller
                        var controller = Activator.CreateInstance(type) as Controller;
                        controller.Request = request;
                        // invoke the action of this instance
                        var response = methodInfo.Invoke(controller, new object[] {}) as HttpResponse;

                        // pass the httpRequest
                        return response;
                    }));
                    Console.WriteLine("    " + url);
                }
            }
        }


        private static void AutoRegisterStaticFilesRoutes(List<Route> routeTable)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);
            foreach (var file in staticFiles)
            {
                var path = file.Replace("wwwroot", string.Empty).Replace("\\", "/");
                routeTable.Add(new Route(path, HttpMethodType.Get, (request) =>
                {
                    var fileInfo = new FileInfo(file);

                    var contentType = fileInfo.Extension switch
                    {
                        ".css" => "text/css",
                        ".html" => "text/html",
                        ".js" => "text/javascript",
                        ".ico" => "image/x-icon",
                        ".jpg" => "image/jpeg",
                        ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".csv" => "text/csv",
                        ".doc" => "application/msword",
                        ".ics" => "text/calendar",
                        ".json" => "application/json",
                        ".pdf" => "application/pdf",
                        ".xls" => "application/vnd.ms-excel",
                        ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                        _ => "text/plain",
                    };



                    return new FileResponse(File.ReadAllBytes(file), contentType);

                }));
            }
        }
    }
}
