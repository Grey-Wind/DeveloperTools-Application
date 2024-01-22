using CefSharp.Wpf;
using CefSharp;
using System;
using System.IO;
using System.Threading;

namespace DeveloperTools
{
    internal static class CefFunc
    {
        private static bool isInitialized = true;

        private static string currentUrl;

        // 获取当前目录
        static string currentDirectory = Environment.CurrentDirectory;

        public static void Initialize(ChromiumWebBrowser browser)
        {
            if (!isInitialized)
            {
                var settings = new CefSettings();
                Cef.Initialize(settings);
                isInitialized = true;
            }

            browser.Address = "https://developer-tools.qingyi-studio.top/";
        }

        public static void LoadUrl(string url, ChromiumWebBrowser browser)
        {
            // 检查即将加载的网址是否与当前网址相同
            if (url != null && url.Trim() != "" && url != currentUrl)
            {
                // 加载指定的网址
                browser.Load(url);
            }
        }

        public static void Shutdown ()
        {
            Cef.Shutdown();

            Thread.Sleep(250);

            // 要删除的后缀名列表
            string[] ext = { ".log", ".pdb", ".log",".xml" };

            // 遍历每个后缀名
            foreach (string extension in ext)
            {
                // 获取后缀名为 extension 的文件路径
                string[] files = Directory.GetFiles(currentDirectory, "*" + ext);

                // 逐个删除文件
                foreach (string filePath in files)
                {
                    File.Delete(filePath);
                }
            }

            Thread.Sleep(100);

            // 要删除的文件夹列表
            string[] folderNames = { "DawnCache", "GPUCache"};

            // 逐个删除文件夹
            foreach (string folderName in folderNames)
            {
                string folderPath = Path.Combine(currentDirectory, folderName);
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }
            }
        }
    }
}
