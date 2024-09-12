using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class CheckWord
    {
        public string FindLongestWord(string[] words)
        {
            if (words == null || words.Length == 0)
                return null;

            string longestWord = words[0];

            foreach (string word in words)
            {
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }

            return longestWord;
        }
        public string FindShortestWord(string[] words)
        {
            if (words == null || words.Length == 0)
                return null;

            string ShortestWord = words[0];

            foreach (string word in words)
            {
                if (word.Length < ShortestWord.Length)
                {
                    ShortestWord = word;
                }
            }

            return ShortestWord;
        }


    }
}
