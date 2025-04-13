using System.Windows;
using WPFBrowser.ViewModels;

namespace WPFBrowser.Views;

public partial class HistoryWindow : Window
{
    public HistoryWindow()
    {
        InitializeComponent();
        DataContext = new HistoryWindowViewModel();
    }
}