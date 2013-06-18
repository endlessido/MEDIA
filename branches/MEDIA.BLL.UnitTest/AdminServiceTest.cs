using System;
using MEDIA.BLL;
using MEDIA.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEDIA.Infrastructure.Ioc;
using MEDIA.Infrastructure.Utilities;
using MEDIA.IBLL;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using MEDIA.IDAL;
namespace MEDIA.BLL.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for AdminServiceTest and is intended
    ///to contain all AdminServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdminServiceTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        /// <summary>
        /// Use TestInitialize to run code before running each test
        /// </summary>
        [TestInitialize()]
        public void MyTestInitialize()
        {
            UnityUtil.LoadUnityConfigurationSectionToContainer("App.config", IoCContext.Container);
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void LoginTest()
        {
            IAdminService target = IoCContext.Container.Resolve<IAdminService>();
            string adminName = string.Empty;
            string adminPwd = string.Empty; 
            Administrator expected = null; 
            Administrator actual;
            actual = target.Login(adminName, adminPwd);
            Assert.AreEqual(expected, actual);

            adminName = "guihuan123";
            adminPwd = "123";
            actual = target.Login(adminName, adminPwd);
            Assert.AreEqual(expected, actual);

            adminName = "guihuan";
            adminPwd = "123";
            actual = target.Login(adminName, adminPwd);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            IAdminService target = IoCContext.Container.Resolve<IAdminService>(); // TODO: Initialize to an appropriate value
            Administrator newModel = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Add(newModel);
            Assert.AreEqual(expected, actual);

            newModel = new Administrator();
            actual = target.Add(newModel);
            Assert.AreEqual(expected, actual);

            newModel = new Administrator { AdminName = "mediaAdmin", AdminPwd = "123" };
            expected = true;
            actual = target.Add(newModel);
            Assert.AreEqual(expected, actual);
        }

    }
}
