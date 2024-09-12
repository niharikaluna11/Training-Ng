using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class CheckWordC
    {
        public void ProcessWords(string[] words)
        {
            var wordVowelCounts = new Dictionary<string, int>();
            foreach (string word in words)
            {
                int vowelCount = CountRepeatingVowels(word);
                wordVowelCounts[word] = vowelCount;
            }

            // Find the minimum number of repeating vowels
            int minRepeatingVowels = int.MaxValue;

            foreach (var av in wordVowelCounts)
            {
                if (av.Value < minRepeatingVowels)
                {
                    minRepeatingVowels = av.Value;
                }
            }

            // Find all words with the minimum number of repeating vowels
            var leastRepeatingVowelWords = new List<string>();
            foreach (var av in wordVowelCounts)
            {
                if (av.Value == minRepeatingVowels)
                {
                    leastRepeatingVowelWords.Add(av.Key);
                }
            }

            // Print the results
            Console.WriteLine($"Words with the least number of repeating vowels ({minRepeatingVowels}):");
            foreach (var word in leastRepeatingVowelWords)
            {
                Console.WriteLine(word);
            }
        }


        private int CountRepeatingVowels(string word)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            Dictionary<char, int> vowelCounts = new Dictionary<char, int>();

            foreach (char vowel in vowels)
            {
                vowelCounts[vowel] = 0;
            }

            foreach (char ch in word.ToLower())
            {
                if (vowelCounts.ContainsKey(ch))
                {
                    vowelCounts[ch]++;
                }
            }

            int repeatingVowelCount = vowelCounts.Values.Count(count => count > 1);

            return repeatingVowelCount;
        }
    }
}
