// From Steven Cleary's AsyncEx helpers https://github.com/StephenCleary/AsyncEx
/*
 * The MIT License (MIT)

Copyright (c) 2014 StephenCleary

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelpers.Extensions
{
    public sealed class TaskCompletionNotifier<TResult> : INotifyPropertyChanged
    {
        public TaskCompletionNotifier(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var scheduler = SynchronizationContext.Current == null ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext();
                task.ContinueWith(t =>
                {
                    var propertyChanged = PropertyChanged;
                    if (propertyChanged != null)
                    {
                        propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
                        if (t.IsCanceled)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
                        }
                        else if (t.IsFaulted)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
                        }
                        else
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
                        }
                    }
                },
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                scheduler);
            }
        }
        
        public Task<TResult> Task { get; private set; }
        
        public TResult Result => Task.Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult);
        
        public bool IsCompleted => Task.IsCompleted;
        
        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;
        
        public bool IsCanceled => Task.IsCanceled;
        
        public bool IsFaulted => Task.IsFaulted;
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}