using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JCRDownload.Code;
using System.IO;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (StreamReader reader = new StreamReader("html.txt"))
            {
                string html = reader.ReadToEnd();
                Journal journal = new Journal();

                journal=JCR.GetJournal(journal, html);
            }
       
        }
    }
}
