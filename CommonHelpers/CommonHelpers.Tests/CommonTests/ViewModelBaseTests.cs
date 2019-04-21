using CommonHelpers.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.CommonTests
{
    [TestClass]
    public class ViewModelBaseTests
    {
        [TestMethod]
        public void BusyStatus()
        {
            // Arrange
            var isBusy = true;
            var vm = new ViewModelBase();

            // Act
            vm.IsBusy = true;

            // Assert
            Assert.AreEqual(isBusy, vm.IsBusy);
        }

        [TestMethod]
        public void BusyMessage()
        {
            // Arrange
            var expectedMessage = "please wait...";
            var vm = new ViewModelBase();

            // Act
            vm.IsBusyMessage = "please wait...";

            // Assert
            Assert.AreEqual(expectedMessage, vm.IsBusyMessage);
        }
    }
}
