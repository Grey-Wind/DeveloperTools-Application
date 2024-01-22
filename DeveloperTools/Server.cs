using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.IO;


namespace DeveloperTools
{
    public static class Server
    {
        private static IDisposable _app;
        private static string _baseAddress = "http://localhost:8080/";

        public static void Start()
        {
            // 启动Web服务器
            _app = WebApp.Start<Startup>(url: _baseAddress);
        }

        public static void Stop()
        {
            // 停止Web服务器
            _app.Dispose();
        }

        public static string BaseAddress
        {
            get { return _baseAddress; }
            set { _baseAddress = value; }
        }

        private class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                // 配置静态文件中间件，指定webapp文件夹作为静态文件目录
                var webAppFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "webapp");

                // 检查webapp文件夹是否存在
                if (!Directory.Exists(webAppFolderPath))
                {
                    // webapp文件夹不存在，显示错误提示
                    app.Use(async (context, next) =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("未找到已安装的离线包，请下载并安装离线包。");
                        Stop();
                    });
                }
                else
                {
                    // webapp文件夹存在，使用静态文件中间件
                    var staticFileOptions = new FileServerOptions
                    {
                        EnableDirectoryBrowsing = true,
                        FileSystem = new PhysicalFileSystem(webAppFolderPath)
                    };

                    app.UseFileServer(staticFileOptions);
                }
            }


        }
    }
}
