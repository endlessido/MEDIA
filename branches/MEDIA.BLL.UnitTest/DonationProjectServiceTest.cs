using MEDIA.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MEDIA.Model;
using System.Collections.Generic;
using MEDIA.Infrastructure.Utilities;
using MEDIA.Infrastructure.Ioc;

namespace MEDIA.BLL.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for DonationProjectServiceTest and is intended
    ///to contain all DonationProjectServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DonationProjectServiceTest
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
        ///A test for IsProjectNameExist
        ///</summary>
        [TestMethod()]
        public void IsProjectNameExistTest()
        {
            DonationProjectService target = new DonationProjectService(); // TODO: Initialize to an appropriate value
            string projectName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsProjectNameExist(projectName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRecentRandomProject
        ///</summary>
        [TestMethod()]
        public void GetRecentRandomProjectTest()
        {
            DonationProjectService target = new DonationProjectService(); // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            List<DonationProject> expected = new List<DonationProject>(); // TODO: Initialize to an appropriate value
            List<DonationProject> actual;
            actual = target.GetRecentRandomProject(count);
            Assert.AreEqual(expected.Count, actual.Count);

            count = 0;
            actual = target.GetRecentRandomProject(count);
            Assert.IsNotNull(actual);

            count = 2;
            actual = target.GetRecentRandomProject(count);
            Assert.AreEqual(actual.Count, 2);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            DonationProjectService target = new DonationProjectService(); // TODO: Initialize to an appropriate value
            DonationProject newModel = null; // TODO: Initialize to an appropriate value
            string goodyName = string.Empty; // TODO: Initialize to an appropriate value
            string goodyDesc = string.Empty; // TODO: Initialize to an appropriate value
            string goodyNum = string.Empty; // TODO: Initialize to an appropriate value
            string goodyPrice = string.Empty; // TODO: Initialize to an appropriate value
            string ddlCurrency = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Add(newModel, goodyName, goodyDesc, goodyNum, goodyPrice, ddlCurrency);
            Assert.AreEqual(expected, actual);

            goodyName = "1,2,3";
            goodyDesc = "1,2,3";
            goodyNum = "10,20,30";
            goodyPrice = "10,20,30";
            ddlCurrency = "CHR,CHR,CHR";
            newModel = new Model.DonationProject
            {
                AreaName = "Ars",
                CountryName = "Swziterland",
                CategoryName = "FootBall",
                AreaId = 1,
                CategoryId = 1,
                TotalFunding = 100,
                ReceivedFunding = 0,
                DonateNum = 0,
                IsFinished = false,
                CreateDate = System.DateTime.Now,
                CurrencyStr = "CHR",
                YoutubeUrl = "www.sina.com",
                ProjectName = "F1",
                ProjectSummary = "F1",
                SelfIntroduce = "F1",
                YourVision = "F1",
                NeedReason = "F1",
                Need = "F1",
                Homepage = "www.sina.com",
                Feedbookpage = string.Empty,
                IsSendNews = true,
                EndDate = System.DateTime.Now.AddDays(33),
                IsCheck = false,
                UserId = 1
            };

            expected = true;
            actual = target.Add(newModel, goodyName, goodyDesc, goodyNum, goodyPrice, ddlCurrency);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ReceiveFunding
        ///</summary>
        [TestMethod()]
        public void ReceiveFundingTest()
        {
            //DonationProjectService target = new DonationProjectService(); // TODO: Initialize to an appropriate value
            //string goodyIds = string.Empty; // TODO: Initialize to an appropriate value
            //int projectId = 0; // TODO: Initialize to an appropriate value
            //int userId = 0; // TODO: Initialize to an appropriate value
            //bool expected = false; // TODO: Initialize to an appropriate value
            //bool actual;
            //actual = target.ReceiveFunding(goodyIds, projectId, userId);
            //Assert.AreEqual(expected, actual);

            //goodyIds = "1,2";
            //projectId = 1;
            //userId = 1;
            //expected = true;
            //actual = target.ReceiveFunding(goodyIds, projectId, userId);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ReceiveFunding 的测试
        ///</summary>
        [TestMethod()]
        public void ReceiveFundingTest1()
        {
            DonationProjectService target = new DonationProjectService(); // TODO: 初始化为适当的值
            Decimal currency = 22; // TODO: 初始化为适当的值
            string orderID = string.Empty; // TODO: 初始化为适当的值
            Nullable<int> projectId = new Nullable<int>(); // TODO: 初始化为适当的值
            Nullable<int> projectIdExpected = new Nullable<int>(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.ReceiveFunding(currency, orderID, out projectId);
            Assert.AreEqual(projectIdExpected, projectId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
