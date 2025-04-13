using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFBrowser.Data;

public class GenericPropertyChanged : INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}