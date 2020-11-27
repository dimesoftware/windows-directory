using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dime.DirectoryServices.Tests
{
    public class MockedWindowsUserStore : WindowsUserStore
    {
        protected override PrincipalContext CreateContext()
        {
            Mock<PrincipalContext> mock = new Mock<PrincipalContext>(ContextType);
            return mock.Object;
        }
    }

    [TestClass]
    public class WindowsUserStoreTests
    {
        [TestMethod]
        [TestCategory("Directory Services")]
        public void WindowsUserStore_Constructor_Default_ShouldContainDefaultProperties()
        {
            WindowsUserStore store = new WindowsUserStore();
            Assert.IsTrue(string.IsNullOrEmpty(store.UserName));
            Assert.IsTrue(string.IsNullOrEmpty(store.Password));
            Assert.IsTrue(string.IsNullOrEmpty(store.Domain));
        }

        [TestMethod]
        [TestCategory("Directory Services")]
        public void WindowsUserStore_Constructor_UserAndPassword_ShouldSetProperties()
        {
            WindowsUserStore store = new WindowsUserStore("Hello", "world");
            Assert.IsTrue(store.UserName == "Hello");
            Assert.IsTrue(store.Password == "world");
            Assert.IsTrue(string.IsNullOrEmpty(store.Domain));
        }

        [TestMethod]
        [TestCategory("Directory Services")]
        public void WindowsUserStore_Constructor_Full_ShouldSetProperties()
        {
            WindowsUserStore store = new WindowsUserStore("Hello", "world", "BE");
            Assert.IsTrue(store.UserName == "Hello");
            Assert.IsTrue(store.Password == "world");
            Assert.IsTrue(store.Domain == "BE");
            Assert.IsTrue(store.ContextType == ContextType.Domain);
        }

        [TestMethod]
        [TestCategory("Directory Services")]
        public async Task WindowsUserStore_GetSidByLoginName_NoMatch_ReturnsEmptyString()
        {
            WindowsUserStore store = new MockedWindowsUserStore();
            Assert.IsTrue(string.IsNullOrEmpty(await store.GetSidByLoginName("jdoe")));
        }

        [TestMethod]
        [TestCategory("Directory Services")]
        public void WindowsUserStore_GetLoginNameBySid()
        {
            WindowsUserStore store = new MockedWindowsUserStore();
            Assert.IsNull(store.GetLoginNameBySid("S-1-5-21-1180699209-877415012-3182924384-1004"));
        }
    }
}