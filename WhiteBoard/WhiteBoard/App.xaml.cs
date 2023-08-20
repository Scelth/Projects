using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using WhiteBoard.Services.Classes;
using WhiteBoard.Services.Interfaces;
using WhiteBoard.View;
using WhiteBoard.ViewModel;
using SimpleInjector;
using WhiteBoard.Context;
using WhiteBoard.Model;

namespace WhiteBoard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();
            MainStartup();
        }

        private void Register()
        {
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<INavigateService, NavigateService>();
            Container.RegisterSingleton<IUserManageService, UserManageService>();
            Container.RegisterSingleton<ICheckService, CheckService>();
            Container.RegisterSingleton<ISaveService, SaveService>();
            Container.RegisterSingleton<ISendService, SendService>();

            Container.RegisterSingleton<MainVM>();
            Container.RegisterSingleton<LogVM>();
            Container.RegisterSingleton<RegisterVM>();
            Container.RegisterSingleton<LibraryVM>();
            Container.RegisterSingleton<DrawVM>();
            Container.RegisterSingleton<SendVM>();
            Container.RegisterSingleton<WhiteBoardDbContext>();
        }

        private void MainStartup()
        {
            MainWindow mainWindow = new();
            mainWindow.DataContext = Container.GetInstance<MainVM>();
            mainWindow.ShowDialog();
        }
    }
}
