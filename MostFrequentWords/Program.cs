using System;

namespace MostFrequentWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new FileReader();
            var mostFrequentWordsFinder = new MostFrequentWordsFinder(fileReader, 10);
            try
            {
                mostFrequentWordsFinder.FindMostFrequentWords();
                var result = mostFrequentWordsFinder.GetTheListOfMostFrequentWords();
                Console.WriteLine(result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
