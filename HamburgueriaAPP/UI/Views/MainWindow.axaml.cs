using Avalonia.Controls;
using HamburgueriaAPP.ViewModels;

namespace HamburgueriaAPP.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ClienteViewModel();
        }
    }
}