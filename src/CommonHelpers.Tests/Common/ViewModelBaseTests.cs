using CommonHelpers.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Common;

[TestClass]
public class ViewModelBaseTests
{
    [TestMethod]
    public void BusyStatus()
    {
        const bool isBusy = true;
        var vm = new ViewModelBase { IsBusy = true };
        Assert.AreEqual(isBusy, vm.IsBusy);
    }

    [TestMethod]
    public void BusyMessage()
    {
        const string expectedMessage = "please wait...";
        var vm = new ViewModelBase { IsBusyMessage = "please wait..." };
        Assert.AreEqual(expectedMessage, vm.IsBusyMessage);
    }
}