using System.Collections.Generic;
using System.Text;

namespace MostFrequentWords
{
    public class MostFrequentWordsFinder
    {
        private readonly IFileReader _fileReader;
        private readonly int _numOfMostFrequentWords;
        private readonly Dictionary<string, int> _wordCountLookUp;
        private readonly List<string> _mostFrequentWords;
        private int _minCountOfMostFrequentWords;

        public MostFrequentWordsFinder(IFileReader fileReader, int numOfMostFrequentWords)
        {
            _fileReader = fileReader;
            _numOfMostFrequentWords = numOfMostFrequentWords;
            _wordCountLookUp = new Dictionary<string, int>();
            _mostFrequentWords = new List<string>();
        }

        public void FindMostFrequentWords()
        {
            var words = _fileReader.GetAllWords();

            foreach(var word in words)
            {
                var countOfCurrWord = 1;
                if (_wordCountLookUp.ContainsKey(word))
                {
                    countOfCurrWord += _wordCountLookUp[word];
                    _wordCountLookUp[word] = countOfCurrWord;
                }
                else
                {
                    _wordCountLookUp.Add(word, 1);
                }

                if (_mostFrequentWords.Contains(word))
                {
                    if (ShouldCleanUpMostFrequentWordList(countOfCurrWord))
                    {
                        CleanUpMostFrequentWordList();
                    }
                }
                else
                {
                    if (_mostFrequentWords.Count < _numOfMostFrequentWords)
                    {
                        _minCountOfMostFrequentWords = 1;
                        _mostFrequentWords.Add(word);
                    }
                    else if (countOfCurrWord == _minCountOfMostFrequentWords)
                    {
                        _mostFrequentWords.Add(word);
                    }
                }
            }
        }

        private bool ShouldCleanUpMostFrequentWordList(int countOfCurrWord)
        {
            return countOfCurrWord == _minCountOfMostFrequentWords + 1 && 
                _mostFrequentWords.Count > _numOfMostFrequentWords;
        }

        private void CleanUpMostFrequentWordList()
        {
            var wordsWithMinCount = new List<string>();
            foreach (var word in _mostFrequentWords)
            {
                if (_wordCountLookUp[word] == _minCountOfMostFrequentWords)
                {
                    wordsWithMinCount.Add(word);
                }
            }

            if (wordsWithMinCount.Count == 0 || 
                _mostFrequentWords.Count - wordsWithMinCount.Count >= _numOfMostFrequentWords)
            {
                _minCountOfMostFrequentWords++;
                foreach (var word in wordsWithMinCount)
                {
                    _mostFrequentWords.Remove(word);
                }
            }
        }

        public string GetTheListOfMostFrequentWords()
        {
            var sb = new StringBuilder();

            foreach (var word in _mostFrequentWords)
            {
                var result = string.Format("{0}: {1}", word, _wordCountLookUp[word]);
                sb.AppendLine(result);
            }

            return sb.ToString();
        } 
    }
}
