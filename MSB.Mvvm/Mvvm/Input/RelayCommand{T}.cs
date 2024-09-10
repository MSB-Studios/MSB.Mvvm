namespace MSB.Mvvm.Input;

/// <summary>
/// A generic command whose sole purpose is to relay its functionality to other
/// objects by invoking delegates.
/// </summary>
/// <typeparam name="T">The type of parameter being passed as input to the callbacks.</typeparam>
public sealed class RelayCommand<T> : ICommand
{
    /// <summary>
    /// The <see cref="Action"/> to invoke when <see cref="Execute"/> is used.
    /// </summary>
    readonly Action<T> execute;

    /// <summary>
    /// The optional action to invoke when <see cref="CanExecute"/> is used.
    /// </summary>
    readonly Func<T, bool>? canExecute;

    /// <summary>
    /// Initializes a new instance of the RelayCommand class that can always execute.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public RelayCommand(Action<T> execute) : this(execute, null)
    {
        // no-op
    }

    /// <summary>
    /// Initializes a new instance of the RelayCommand class.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RelayCommand(Action<T> execute, Func<T, bool>? canExecute)
    {
        if (execute is null)
            throw new ArgumentNullException(nameof(execute));

        this.execute = execute;
        this.canExecute = canExecute;
    }

    #region Methods

    /// <inheritdoc/>
    public bool CanExecute(object parameter)
    {
        return canExecute?.Invoke((T)parameter) is not false;
    }

    /// <inheritdoc/>
    public void Execute(object parameter)
    {
        if (CanExecute((T)parameter))
            execute((T)parameter);
    }

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged"/> event.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region Events

    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged;

    #endregion
}