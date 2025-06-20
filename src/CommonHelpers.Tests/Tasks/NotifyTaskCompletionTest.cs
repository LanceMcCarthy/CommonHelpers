using CommonHelpers.Tasks;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelpers.Tests.Tasks;

[TestClass]
[TestSubject(typeof(NotifyTaskCompletion<>))]
public class NotifyTaskCompletionTest
{
    [TestMethod]
    public async Task NotifyTaskCompletion_SuccessfulTask_PropertiesAreCorrect()
    {
        var task = Task.FromResult(42);
        var notifyTask = new NotifyTaskCompletion<int>(task);
        await task;
        Assert.IsTrue(notifyTask.IsCompleted);
        Assert.IsTrue(notifyTask.IsSuccessfullyCompleted);
        Assert.IsFalse(notifyTask.IsFaulted);
        Assert.IsFalse(notifyTask.IsCanceled);
        Assert.AreEqual(42, notifyTask.Result);
    }

    [TestMethod]
    public async Task NotifyTaskCompletion_FaultedTask_PropertiesAreCorrect()
    {
        var exception = new InvalidOperationException("fail");
        var task = Task.FromException<int>(exception);
        var notifyTask = new NotifyTaskCompletion<int>(task);
        try { await task; } catch { }
        Assert.IsTrue(notifyTask.IsCompleted);
        Assert.IsFalse(notifyTask.IsSuccessfullyCompleted);
        Assert.IsTrue(notifyTask.IsFaulted);
        Assert.IsFalse(notifyTask.IsCanceled);
        Assert.AreEqual(exception, notifyTask.InnerException);
        Assert.AreEqual("fail", notifyTask.ErrorMessage);
    }

    [TestMethod]
    public async Task NotifyTaskCompletion_CanceledTask_PropertiesAreCorrect()
    {
        var cts = new CancellationTokenSource();
        var task = Task.Run<int>(() => {
            cts.Token.ThrowIfCancellationRequested();
            return 0;
        }, cts.Token);
        cts.Cancel();
        try { await task; } catch { }
        var notifyTask = new NotifyTaskCompletion<int>(task);
        Assert.IsTrue(notifyTask.IsCompleted);
        Assert.IsFalse(notifyTask.IsSuccessfullyCompleted);
        Assert.IsFalse(notifyTask.IsFaulted);
        Assert.IsTrue(notifyTask.IsCanceled);
    }

    [TestMethod]
    public async Task NotifyTaskCompletion_RaisesPropertyChangedEvents()
    {
        var tcs = new TaskCompletionSource<int>();
        var notifyTask = new NotifyTaskCompletion<int>(tcs.Task);
        string[] expectedProps = new[] { "Status", "IsCompleted", "IsNotCompleted", "IsSuccessfullyCompleted", "Result" };
        var changedProps = new System.Collections.Generic.List<string>();
        notifyTask.PropertyChanged += (s, e) => changedProps.Add(e.PropertyName);
        tcs.SetResult(123);
        await tcs.Task;
        // Wait for property changed events to propagate
        await Task.Delay(50);
        foreach (var prop in expectedProps)
        {
            Assert.IsTrue(changedProps.Contains(prop), $"PropertyChanged for {prop} not raised");
        }
    }
}