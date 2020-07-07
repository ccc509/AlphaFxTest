using System;
using System.Collections.Generic;
using System.Text;

namespace MostFrequentWords
{
    public interface IFileReader
    {
        public List<string> GetAllWords();
    }
}
