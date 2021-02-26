using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SIS.MvcFramework.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("OnlyHtmlView")]
        [InlineData("ForForeachIfView")]
        [InlineData("ViewModelView")]
        public void Test1(string testName)
        {
            var viewModel = new TestViewModel()
            {
                Name = "Aleks",
                Year = 2021,
                Numbers = new List<int> { 1, 10, 100, 1000, 10000 }
            };

            var viewContent = File.ReadAllText($"{testName}.html");
            var expectedResult = File.ReadAllText($"{testName}.Expected.html");

            Assert
        }
    }
}
