using Microsoft.VisualStudio.TestTools.UnitTesting;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic.Print;
using System;
using System.IO;

namespace SY.Com.Medical.TestUnit
{
    [TestClass]
    public class PrintTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        #region PrintFile

        [TestMethod]
        public void TestClone()
        {
            PrintFile file = new PrintFile(2);
            var clonefile = file.Clone(100);
            PrintFile newfile = new PrintFile(clonefile.FileId);
            Assert.AreEqual(clonefile.FilePath, newfile.FilePath);
            Assert.AreEqual(clonefile.TemplateId, newfile.TemplateId);
            Assert.AreEqual(clonefile.TemplateName, newfile.TemplateName);
            Assert.AreEqual(clonefile.TemplatePath, newfile.TemplatePath);
            Assert.AreEqual(clonefile.IsUse, newfile.IsUse);
            Console.WriteLine(newfile.FilePath);

        }
        
        [TestMethod]
        public void TestUpdate()
        {
            PrintFile newfile = new PrintFile(2);
            PrintFile oldfile = new PrintFile(24);
            string path = SystemEnvironment.GetRootDirector() + newfile.FilePathSave;
            var bytes = File.ReadAllText(path,System.Text.Encoding.UTF8);
            oldfile.Update(bytes);
        }


        [TestMethod]
        public void TestDelete()
        {
            try
            {
                PrintFile file = new PrintFile(24);
                file.Delete();
            }catch(MyException)
            {
                Console.WriteLine("文件未找到,请换一个进行测试");
            }
            try
            {
                PrintFile newfile = new PrintFile(24);
            }catch(MyException)
            {
                Console.WriteLine("删除成功");
            }
            

        }


        [TestMethod]
        public void TestSetUse()
        {
            int[] ids = new int[] { 23, 24 };
            PrintFile file23 = new PrintFile(23);
            file23.SetUse();
            PrintFile file23_1 = new PrintFile(23);
            Assert.IsTrue(file23_1.IsUse);
            PrintFile file24 = new PrintFile(24);
            Assert.IsFalse(file24.IsUse);


            file24.SetUse();
            PrintFile file23_2 = new PrintFile(23);
            PrintFile file24_2 = new PrintFile(24);
            Assert.IsTrue(file24_2.IsUse);
            Assert.IsFalse(file23_2.IsUse);


            //PrintFile file = new PrintFile(2);
            //file.SetUse(100);
            //PrintFile file23_3 = new PrintFile(23);
            //PrintFile file24_3 = new PrintFile(24);
            //Assert.IsFalse(file23_3.IsUse);
            //Assert.IsFalse(file24_3.IsUse);
        }

        #endregion

        #region PrintTemplate

        [TestMethod]
        public void TestGetTemplates()
        {
            PrintTemplate template = new PrintTemplate(100);
            var prints = template.GetTemplates();            
            foreach(var print in prints)
            {
                Console.WriteLine(print.TemplateName);
                foreach(var file in print.PrintFiles)
                {
                    Console.WriteLine( file.FileId + ":" + file.IsSystem + ":" + file.IsUse + ":" + file.FilePath);
                }                
            }
        }

        [TestMethod]
        public void TestChooseFile()
        {
            PrintTemplate template = new PrintTemplate(100);
            var file = template.ChooseFile(2);
            Assert.AreEqual(24, file.FileId);
            var file2 = template.ChooseFile(1);
            Assert.AreEqual(1, file2.FileId);
        }


        [TestMethod]
        public void TestAddPrintFile()
        {
            PrintTemplate template = new PrintTemplate(100);
            try
            {
                template.AddPrintFile(2);
            }
            catch (MyException mex)
            {
                Console.WriteLine(mex.Message);
            }
            try
            {
                var file = template.AddPrintFile(1);
                Console.WriteLine($"{file.FileId},{file.FilePath}");
            }catch(MyException mex)
            {
                Console.WriteLine(mex.Message);
            }

        }

        #endregion


    }
}
