﻿using System;
using System.Windows.Input;

namespace CommonHelpers.Mvvm;

/// <summary>
/// A command whose sole purpose is to relay its functionality 
/// to other objects by invoking delegates. 
/// The default return value for the CanExecute method is 'true'.
/// <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
/// <see cref="CanExecute"/> is expected to return a different value.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action execute;
    private readonly Func<bool> canExecute;

    /// <inheritdoc />
    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Creates a new command that can always execute.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    public RelayCommand(Action execute)
        : this(execute, null)
    {
    }

    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute;
    }

    /// <summary>
    /// Determines whether this <see cref="RelayCommand"/> can execute in its current state.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
    /// </param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter)
    {
        return canExecute?.Invoke() ?? true;
    }

    /// <summary>
    /// Executes the <see cref="RelayCommand"/> on the current command target.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
    /// </param>
    public void Execute(object parameter)
    {
        execute();
    }

    /// <summary>
    /// Method used to raise the <see cref="CanExecuteChanged"/> event
    /// to indicate that the return value of the <see cref="CanExecute"/>
    /// method has changed.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}