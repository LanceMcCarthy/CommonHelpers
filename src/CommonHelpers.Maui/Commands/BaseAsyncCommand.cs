#nullable enable
using CommonHelpers.Maui.Events.Exceptions;
using CommonHelpers.Maui.Helpers;
using System.Windows.Input;

namespace CommonHelpers.Maui.Commands;

public class BaseAsyncCommand<TExecute, TCanExecute> : BaseCommand<TCanExecute>, ICommand
{
    private readonly Func<TExecute?, Task> execute;
    private readonly Action<Exception>? onException;
    private readonly bool continueOnCapturedContext;

    private protected BaseAsyncCommand(
        Func<TExecute?, Task>? execute,
        Func<TCanExecute?, bool>? canExecute,
        Action<Exception>? onException,
        bool continueOnCapturedContext,
        bool allowsMultipleExecutions)
        : base(canExecute, allowsMultipleExecutions)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute), $"{nameof(execute)} cannot be null");
        this.onException = onException;
        this.continueOnCapturedContext = continueOnCapturedContext;
    }

    private protected static Func<object, Task>? ConvertExecute(Func<Task>? execute)
    {
        if (execute == null)
            return null;

        return _ => execute();
    }

    private protected static Func<object, bool>? ConvertCanExecute(Func<bool>? canExecute)
    {
        if (canExecute == null)
            return null;

        return _ => canExecute();
    }

    private protected async Task ExecuteAsync(TExecute? parameter)
    {
        ExecutionCount++;

        try
        {
            await execute(parameter).ConfigureAwait(continueOnCapturedContext);
        }
        catch (Exception e) when (onException != null)
        {
            onException(e);
        }
        finally
        {
            if (--ExecutionCount <= 0)
                ExecutionCount = 0;
        }
    }

    bool ICommand.CanExecute(object parameter) => parameter switch
    {
        TCanExecute validParameter => CanExecute(validParameter),
        null when IsNullable<TCanExecute>() => CanExecute((TCanExecute?)parameter), 
        null => throw new InvalidCommandParameterException(typeof(TCanExecute)),
        _ => throw new InvalidCommandParameterException(typeof(TCanExecute), parameter.GetType()),
    };

    void ICommand.Execute(object parameter)
    {
        switch (parameter)
        {
            case TExecute validParameter:
                ExecuteAsync(validParameter).SafeFireAndForget(onException, continueOnCapturedContext);
                break;

            case null when IsNullable<TExecute>():
                ExecuteAsync((TExecute?)parameter).SafeFireAndForget(onException, continueOnCapturedContext);
                break;

            case null:
                throw new InvalidCommandParameterException(typeof(TExecute));

            default:
                throw new InvalidCommandParameterException(typeof(TExecute), parameter.GetType());
        }
    }
}