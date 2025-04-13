using System.Windows;
using WPFBrowser.ViewModels;

namespace WPFBrowser.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        Application.Current.MainWindow.WindowState = WindowState.Maximized;
        DataContext = new MainWindowViewModel();
    }
}