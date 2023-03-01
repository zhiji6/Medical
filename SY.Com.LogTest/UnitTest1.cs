using Microsoft.VisualStudio.TestTools.UnitTesting;
using SY.Com.Log;
using System;

namespace SY.Com.LogTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Wuha()
        {

        }
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void TestWriteToFile()
        {
            WriteToFile wtf = new WriteToFile();
            for(int i = 0; i < 1000; i++)
            {
                wtf.Write(new LogData { Level = LogLevel.Debug, Message = "我是测试大王" }, Environment.CurrentDirectory + "/log/");
            }            
        }
    }
}
