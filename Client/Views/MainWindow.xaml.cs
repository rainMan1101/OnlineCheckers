using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OnlineCheckers.Client.ViewModels;

namespace OnlineCheckers.Client.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //NavigationHelper.SetFrame(frame);
            DataContext = new CMainViewModel();
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //public void Window_Initialized(object sender, EventArgs e)
        //{
        //    //frame.Source = new Uri("/Client;component/Views/Pages/PreviewPage.xaml", UriKind.Relative);
        //    frame.Navigate(new PreviewPage());
        //    //frame.Content
        //}

        //private void Frame_Navigated(object sender, NavigationEventArgs e)
        //{
        //    MessageBox.Show("Frame_Navigated" + frame.Source);
        //}

        //private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        //{
        //    MessageBox.Show("Frame_Navigating" + frame.Source);
        //}
    }
}
