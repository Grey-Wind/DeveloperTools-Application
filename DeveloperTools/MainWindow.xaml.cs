using CefSharp.Wpf;
using CefSharp;
using System.Windows;
using System;

namespace DeveloperTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 初始化CefSharp
            CefFunc.Initialize(browser);
        }

        private void OnlineBtnClick(object sender, RoutedEventArgs e)
        {
            CefFunc.LoadUrl("https://developer-tools.qingyi-studio.top/",browser);
        }

        private void OfflineBtnClick(object sender, RoutedEventArgs e)
        {
            Server.Start();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            CefFunc.Shutdown();
            Application.Current.Shutdown();
        }
    }
}
