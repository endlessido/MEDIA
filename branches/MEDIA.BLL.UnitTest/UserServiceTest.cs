using MEDIA.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MEDIA.IBLL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.BLL.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for UserServiceTest and is intended
    ///to contain all UserServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            UnityUtil.LoadUnityConfigurationSectionToContainer("App.config", IoCContext.Container);
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IsEmailExists
        ///</summary>
        [TestMethod()]
        public void IsEmailExistsTest()
        {
            IUserService target = IoCContext.Container.Resolve<IUserService>(); // TODO: Initialize to an appropriate value
            string emailAddress = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsEmailExist(emailAddress);
            Assert.AreEqual(expected, actual);

            emailAddress = "14375398@qq.com";
            expected = true;
            actual = target.IsEmailExist(emailAddress);
            Assert.AreEqual(expected, actual);

            emailAddress = "14@qq.com";
            expected = false;
            actual = target.IsEmailExist(emailAddress);
            Assert.AreEqual(expected, actual);

            emailAddress = null;
            expected = false;
            actual = target.IsEmailExist(emailAddress);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Active
        ///</summary>
        [TestMethod()]
        public void ActiveTest()
        {
            UserService target = new UserService(); // TODO: Initialize to an appropriate value
            int uId = 0; // TODO: Initialize to an appropriate value
            Guid activekey = new Guid(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Active(uId, activekey);
            Assert.AreEqual(expected, actual);

            uId = 8;
            activekey = Guid.Parse("2aba7a83-8c56-44a0-8edf-1da0e5e0da0a");
            expected = false;
            actual = target.Active(uId, activekey);
            Assert.AreEqual(expected, actual);
        }
    }
}
