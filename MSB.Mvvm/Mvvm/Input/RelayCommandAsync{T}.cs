namespace MSB.Mvvm.Input;

/// <summary>
/// A generic command whose sole purpose is to relay its functionality to other
/// objects by invoking asynchronous delegates.
/// </summary>
/// <typeparam name="T">The type of parameter being passed as input to the callbacks.</typeparam>
public sealed class RelayCommandAsync<T> : ICommand
{
    /// <summary>
    /// The asynchronous function to invoke when <see cref="Execute"/> is used.
    /// </summary>
    readonly Func<T, Task> execute;

    /// <summary>
    /// The optional action to invoke when <see cref="CanExecute"/> is used.
    /// </summary>
    readonly Func<T, bool>? canExecute;

    /// <summary>
    /// A flag indicating whether the command is currently executing.
    /// </summary>
    bool isExecuting;

    /// <summary>
    /// Initializes a new instance of the RelayCommand class that can always execute.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public RelayCommandAsync(Func<T, Task> execute) : this(execute, null)
    {

    }

    /// <summary>
    /// Initializes a new instance of the RelayCommand class.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RelayCommandAsync(Func<T, Task> execute, Func<T, bool>? canExecute)
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
        if (isExecuting)
            return false;

        return canExecute?.Invoke((T)parameter) is not false;
    }

    /// <inheritdoc/>
    public async void Execute(object parameter)
    {
        try
        {
            if (CanExecute(parameter))
            {
                isExecuting = true;
                await execute((T)parameter);
            }
        }
        finally
        {
            isExecuting = false;
        }
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