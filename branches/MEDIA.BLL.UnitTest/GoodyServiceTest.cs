using MEDIA.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MEDIA.Model;
using System.Collections.Generic;
using MEDIA.Infrastructure.Utilities;
using MEDIA.Infrastructure.Ioc;
using MEDIA.IBLL;
using Microsoft.Practices.Unity;


using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using MEDIA.IDAL;

namespace MEDIA.BLL.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for GoodyServiceTest and is intended
    ///to contain all GoodyServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GoodyServiceTest
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
        //Use TestInitialize to run code before running each test
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
        ///A test for GetGoodysByProjectId
        ///</summary>
        [TestMethod()]
        public void GetGoodysByProjectIdTest()
        {
            IGoodyService target = IoCContext.Container.Resolve<IGoodyService>(); // TODO: Initialize to an appropriate value
            int projectId = 1; // TODO: Initialize to an appropriate value
            int expected = 3; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetGoodysByProjectId(projectId).Count;
            Assert.AreEqual(expected, actual);

            expected = 0;
            projectId = 111;
            actual = target.GetGoodysByProjectId(projectId).Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
