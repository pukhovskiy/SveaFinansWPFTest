using System.Windows;
using SveaFinansTest.ViewModels;

namespace SveaFinansTest.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;
            var vm = new MainViewModel();
            LayoutRoot.DataContext = vm;
            await vm.Initialize();

        }
    }
}
