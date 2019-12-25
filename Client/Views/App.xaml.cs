using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineCheckers.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHubProxy ChatHubProxy { get; }
        public static IHubProxy GuestHubProxy { get; }
        public static IHubProxy PlayerHubProxy { get; }
        public static IHubProxy UserHubProxy { get; }


        static App()
        {
            var _hubConnection = new HubConnection("http://localhost:52527/");

            UserHubProxy = _hubConnection.CreateHubProxy("UserHub");
            GuestHubProxy = _hubConnection.CreateHubProxy("GuestHub");
            ChatHubProxy = _hubConnection.CreateHubProxy("ChatHub");
            PlayerHubProxy = _hubConnection.CreateHubProxy("PlayerHub");

            _hubConnection.Start(transport: new WebSocketTransport()).Wait();
        }

        [STAThread]
        public static void Main()
        {
            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
