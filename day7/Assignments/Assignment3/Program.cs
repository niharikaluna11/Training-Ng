namespace Assignment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // Take a string from user the words seperated by comma(",").
            //Seperate the words and find the words with the least number of repeating vowels. 
            //print the count and the word. If there is a tie then print all the words that tie for the least

            Console.WriteLine("Enter a string separated by commas (e.g., aeiou,a,iou):");

            String input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] words = input.Split(',')
                                     .Select(word => word.Trim().ToLower())
                                     .ToArray();

                CheckWordC checkWord = new CheckWordC();
                checkWord.ProcessWords(words);
            }
            else
            {
                Console.WriteLine("No input provided.");
            }

        }
    }
}
