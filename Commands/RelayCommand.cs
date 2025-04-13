
using System.Windows.Input;

namespace WPFBrowser.Commands;

public class RelayCommand : ICommand
{

    private readonly Predicate<object?> _canExecutePredicate;
    private readonly Action<object?> _executeAction;

    public RelayCommand(Predicate<object?> canExecutePredicate, Action<object?> executeAction)
    {
        _canExecutePredicate = canExecutePredicate;
        _executeAction = executeAction;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecutePredicate.Invoke(parameter);
    }

    public void Execute(object? parameter)
    {
        _executeAction.Invoke(parameter);
    }
}
