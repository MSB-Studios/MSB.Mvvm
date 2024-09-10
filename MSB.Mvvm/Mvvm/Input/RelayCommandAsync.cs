namespace MSB.Mvvm.Input;

/// <summary>
/// A command whose sole purpose is to relay its functionality to other
/// objects by invoking delegates.
/// </summary>
public sealed class RelayCommandAsync : ICommand
{
    /// <summary>
    /// The action to invoke when <see cref="Execute"/> is used.
    /// </summary>
    readonly Func<Task> execute;

    /// <summary>
    /// The optional action to invoke when <see cref="CanExecute"/> is used.
    /// </summary>
    readonly Func<bool>? canExecute;

    /// <summary>
    /// A flag indicating whether the command is currently executing.
    /// </summary>
    bool isExecuting;

    /// <summary>
    /// Initializes a new instance of the RelayCommand class that can always execute.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public RelayCommandAsync(Func<Task> execute) : this(execute, null)
    {
        // no-op
    }

    /// <summary>
    /// Initializes a new instance of the RelayCommand class.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RelayCommandAsync(Func<Task> execute, Func<bool>? canExecute)
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

        return canExecute?.Invoke() is not false;
    }

    /// <inheritdoc/>
    public async void Execute(object parameter)
    {
        try
        {
            if (CanExecute(parameter))
            {
                isExecuting = true;
                await execute();
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