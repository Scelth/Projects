using Admin.Model.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using SimpleInjector;
using System.Windows.Navigation;
using Admin.ViewModel;
using Admin.Services.Classes;
using Admin.Services.Interfaces;

namespace Admin
{
    public partial class App : Application
    {
        public static Container container { get; set; } = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();
            MainStartup();
        }

        private void Register()
        {
            container.RegisterSingleton<IMessenger, Messenger>();
            container.RegisterSingleton<INavigateService, NavigateService>();
            container.RegisterSingleton<IDownloadService, DownloadService>();
            container.RegisterSingleton<ISerializeService, SerializeService>();
            container.RegisterSingleton<IProjectManageService, ProjectManageService>();

            container.RegisterSingleton<MainVM>();
            container.RegisterSingleton<MenuVM>();
            container.RegisterSingleton<AddVM>();
            container.RegisterSingleton<EditVM>();
            container.RegisterSingleton<DeleteVM>();
        }

        private void MainStartup()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = container.GetInstance<MainVM>();
            mainWindow.ShowDialog();
        }
    }
}