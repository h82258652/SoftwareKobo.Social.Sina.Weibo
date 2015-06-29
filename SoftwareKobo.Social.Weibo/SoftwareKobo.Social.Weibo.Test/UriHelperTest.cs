using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SoftwareKobo.Social.Weibo.Utils;
using System.Collections.Generic;

namespace SoftwareKobo.Social.Weibo.Test
{
    [TestClass]
    public class UriHelperTest
    {
        [TestMethod]
        public void TestAddOrUpdateQuery()
        {
            Uri uri, uri2;

            uri = new Uri("http://www.baidu.com");
            uri2 = UriHelper.AddOrUpdateQuery(uri, "name", "tom");
            Assert.AreEqual(uri2.ToString(), "http://www.baidu.com/?name=tom");

            uri = new Uri("http://www.baidu.com/");
            uri2 = UriHelper.AddOrUpdateQuery(uri, "name", "tom");
            Assert.AreEqual(uri2.ToString(), "http://www.baidu.com/?name=tom");

            uri = new Uri("http://www.baidu.com/?");
            uri2 = UriHelper.AddOrUpdateQuery(uri, "name", "tom");
            Assert.AreEqual(uri2.ToString(), "http://www.baidu.com/?name=tom");

            uri = new Uri("http://www.baidu.com?name=tom");
            uri2 = uri.AddOrUpdateQuery("name", "mary");
            Assert.AreEqual(uri2.ToString(), "http://www.baidu.com/?name=mary");        

            uri=new Uri("http://www.baidu.com");
            uri2 = uri.AddOrUpdateQuery("t", "&");
            Assert.AreEqual(uri2.ToString(),"http://www.baidu.com/?t=%26");
        }

        [TestMethod]
        public void TestAddOrUpdateQuery2()
        {
            Uri uri, uri2;

            uri = new Uri("http://www.baidu.com");
            uri2 = uri.AddOrUpdateQuery(new Dictionary<string, string>() { { "sex", "male" }, {"name","tom" } });

            Assert.AreEqual(uri2.ToString(), "http://www.baidu.com/?sex=male&name=tom");
        }
    }
}
