using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MostFrequentWords
{
    public class FileReader : IFileReader
    {
        private readonly string _filename;

        public FileReader()
        {
            _filename = Constants.FILENAME;
        }

        public List<string> GetAllWords()
        {
            var words = new List<string>();

            using (StreamReader reader = new StreamReader(_filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var wordsInLine = line.Split(' ');
                    foreach (var word in wordsInLine)
                    {
                        if (!string.IsNullOrWhiteSpace(word))
                        {
                            var wordWithoutSpecialCharacters = RemoveSpecialCharacters(word);
                            words.Add(wordWithoutSpecialCharacters);
                        }
                    }
                }
            }

            return words;
        }

        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}
