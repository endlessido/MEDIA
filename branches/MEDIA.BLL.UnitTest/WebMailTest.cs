using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using System.Net;
using MEDIA.Infrastructure.Utilities;

namespace MEDIA.BLL.UnitTest
{
    [TestClass]
    public class WebMailTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            EmailMessage msg = new EmailMessage();
            msg.To = "guih@shinetechchina.com";
            msg.Subject = "Test Email";
            msg.Body = "Hello !";
            msg.SendCompleted += new EmailMessage.SendResultCallBackHandler(msg_SendCompleted);
            msg.Send();
            System.Threading.Thread.Sleep(5000);
        }

        void msg_SendCompleted(object sender, EmailMessage.SendResult e)
        {
            switch (e)
            {
                case EmailMessage.SendResult.Success:
                    Assert.IsTrue(true);
                    break;
                case EmailMessage.SendResult.Error:
                    Assert.IsTrue(false);
                    break;
                case EmailMessage.SendResult.Cancled:
                    Assert.Inconclusive();
                    break;
            }
        }
    }
}
