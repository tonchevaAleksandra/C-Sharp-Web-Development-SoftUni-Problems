using System;
using SIS.HTTP;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SIS.HTTP.Response;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static async Task StartAsync(IMvcApplication application)
        {
            var routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);
            AutoREgisterRoutes(routeTable, application);
            foreach (var route in routeTable)
            {
                Console.WriteLine(route.ToString());
            }
            var httpServer = new HttpServer(80, routeTable);
            await httpServer.StartAsync();
        }

        private static void AutoREgisterRoutes(List<Route> routeTable, IMvcApplication application)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);
            foreach (var file in staticFiles)
            {
                routeTable.Add(new Route(file, HttpMethodType.Get, (request) =>
                {
                    var fileInfo = new FileInfo(file);

                    var contentType = fileInfo.Extension switch
                    {
                        "css" => "text/css",
                        "html" => "text/html",
                        "js" => "text/javascript",
                        "ico" => "image/x-icon",
                        "jpg" => "image/jpeg",
                        "jpeg" => "image/jpeg",
                        "png" => "image/png",
                        "gif" => "image/gif",
                        "csv" => "text/csv",
                        "doc" => "application/msword",
                        "ics" => "text/calendar",
                        "json" => "application/json",
                        "pdf" => "application/pdf",
                        "xls" => "application/vnd.ms-excel",
                        "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                        _ => "text/plain",
                    };



                    return new FileResponse(File.ReadAllBytes(file), contentType);

                }));
            }
        }
    }
}
