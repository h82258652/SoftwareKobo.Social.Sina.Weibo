﻿using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SoftwareKobo.Social.Weibo.Utils;
using System;

namespace SoftwareKobo.Social.Weibo.Test
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        public void TestMethods()
        {
            var dt = DateTime.Now;
            var v = DateTimeHelper.ToTimestamp(dt);
            var dt2 = DateTimeHelper.FromTimestamp(v);
            Assert.AreEqual(dt.ToString(), dt2.ToString());
        }
    }
}