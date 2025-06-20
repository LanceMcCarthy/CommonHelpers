using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CommonHelpers.Tasks;

public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
{
    private readonly TResult defaultResult;

    public NotifyTaskCompletion(Task<TResult> task)
    {
        this.Task = task;

        if (!task.IsCompleted)
        {
            var _ = WatchTaskAsync(task);
        }
    }

    public NotifyTaskCompletion(Task<TResult> task, TResult defaultResult = default)
    {
        this.defaultResult = defaultResult;

        this.Task = task;

        if (!task.IsCompleted)
        {
            var _ = WatchTaskAsync(task);
        }
    }

    private async Task WatchTaskAsync(Task task)
    {
        try
        {
            await task;
        }
        catch
        {
        }

        var propertyChanged = PropertyChanged;

        if (propertyChanged == null)
            return;

        propertyChanged(this, new PropertyChangedEventArgs("Status"));
        propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
        propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));

        if (task.IsCanceled)
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
        }
        else if (task.IsFaulted)
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
            propertyChanged(this, new PropertyChangedEventArgs("Exception"));
            propertyChanged(this, new PropertyChangedEventArgs("InnerException"));
            propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
        }
        else
        {
            propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("Result"));
        }
    }

    public Task<TResult> Task { get; private set; }

    public TResult Result => this.Task.Status == TaskStatus.RanToCompletion ? this.Task.Result : defaultResult;

    public TaskStatus Status => this.Task.Status;

    public bool IsCompleted => this.Task.IsCompleted;

    public bool IsNotCompleted => !this.Task.IsCompleted;

    public bool IsSuccessfullyCompleted => this.Task.Status == TaskStatus.RanToCompletion;

    public bool IsCanceled => this.Task.IsCanceled;

    public bool IsFaulted => this.Task.IsFaulted;

    public AggregateException Exception => this.Task.Exception;

    public Exception InnerException => Exception?.InnerException;

    public string ErrorMessage => InnerException?.Message;

    public event PropertyChangedEventHandler PropertyChanged;
}