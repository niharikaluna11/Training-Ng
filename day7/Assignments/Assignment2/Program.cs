namespace Assignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //Take a string from user the words seperated by comma(","). Seperate the words and find out the longest and the shortest word in it
            Console.WriteLine("Enter a string separated by commas (e.g., largest,short):");

            String input =Console.ReadLine();

            string[] words = input.Split(',').Select(word => word.Trim()).ToArray();

            CheckWord checkWord = new CheckWord();  

            string longestWord =checkWord.FindLongestWord(words);


            string shortestWord = checkWord.FindShortestWord(words);

            // Output the results
            Console.WriteLine($"Longest word: {longestWord}");
            Console.WriteLine($"Shortest word: {shortestWord}");


        }
    }
}
