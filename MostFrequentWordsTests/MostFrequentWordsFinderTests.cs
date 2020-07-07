using Microsoft.VisualStudio.TestTools.UnitTesting;
using MostFrequentWords;
using Moq;
using System.Collections.Generic;

namespace MostFrequentWordsTests
{
    [TestClass]
    public class MostFrequentWordsFinderTests
    {
        private MostFrequentWordsFinder _mostFrequentWordsFinder;

        [TestInitialize]
        public void SetUp()
        {
            var mockedFileReader = new Mock<IFileReader>();
            var result = new List<string>()
            {
                "Cambridge", "Cambridge","Cambridge", "Cambridge",
                "Oxford","Oxford","Oxford",
                "LSE","LSE",
                "Imperial","Imperial","Imperial","Imperial",
                "Warwick", "Warwick",
                "Bath", "Bath", "Bath", "Bath", "Bath",
                "LSE", "LSE", "LSE", "LSE"
            };
            mockedFileReader.Setup(x => x.GetAllWords()).Returns(result);
            _mostFrequentWordsFinder = new MostFrequentWordsFinder(mockedFileReader.Object, 3);
        }

        [TestMethod]
        public void FindMostFrequentWords_CorrectResult()
        {
            _mostFrequentWordsFinder.FindMostFrequentWords();
            var result = _mostFrequentWordsFinder.GetTheListOfMostFrequentWords();
            Assert.AreEqual("Cambridge: 4\r\nImperial: 4\r\nBath: 5\r\nLSE: 6\r\n", result);
        }
    }
}
