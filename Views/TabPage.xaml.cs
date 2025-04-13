using System.Windows.Controls;
using WPFBrowser.ViewModels;

namespace WPFBrowser.Views;

public partial class TabPage : Page
{
    public TabPage()
    {
        InitializeComponent();
        DataContext = new TabPageViewModel();
    }
}