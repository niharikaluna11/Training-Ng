using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
//    4477 4683 4311 3002 

//2003 1134 3864 7744- Reverse the number 

//2+0*2+0+3*2+1+1*2+3+4*2+3+8*2+6+4*2+7+7*2+4+4*2 - identify the even position numbers and multiply by 2 

//2+0+0+6+1+2+3+8+3+16+6+8+7+14+4+8 - Multiplied 

//2+0+0+6+1+2+3+8+3+(1+6)+6+8+7+(1+4)+4+8 - If results in 2 digit number sum them up 

//2+0+0+6+1+2+3+8+3+7+6+8+7+5+4+8 - Sum up all the values 

//70%10-> Divide by 10 if 0 remainder then valid card number
    public class InvalidCardNumberException : Exception
    {
        public InvalidCardNumberException(string message) :base(message) 
        {
            Console.WriteLine("eroor");
        }

    }
    public class CardValidator
    {
        public bool Validate(string cardNumber)
        {

            try
            {   // Remove any non-digit characters 

                if (!IsCardNumberInValidFormat(cardNumber))
                {
                    Console.WriteLine("Card number must be in the format #### #### #### ####.");
                    return false;
                }

                String cleanedNum = CleanNum(cardNumber);

                //check if the card num is 16 digits
                if (cleanedNum.Length != 16)
                {
                    Console.WriteLine("Card number must be exactly 16 digits.");
                }

                // Reverse the card number for processing
                String reversedNum = ReverseNum(cleanedNum);

                // Perform mathematic operation multiple even digit by 2 nd then if any 2 digit num sum then


                // Check if the total sum modulo 10 is zero (valid card number)
                int tsum = processdigit(reversedNum);
                return tsum % 10 == 0;
                //return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;


            }



        }
        static bool IsCardNumberInValidFormat(string cardNumber)
        {
            // Card number should be 19 characters  : 1212 1212 1212 1212
            if (cardNumber.Length != 19)
            {
                Console.WriteLine("Card number must be in the format #### #### #### ####.");
                return false;
            }

            // Check if every 5th character is a space 
            for (int i = 4; i < cardNumber.Length; i += 5)
            {
                if (cardNumber[i] != ' ')
                {
                   
                    Console.WriteLine("Card number must be in the format #### #### #### ####.");
                    return false;
                }
            }

            

            return true;
        }

        private string CleanNum(string cardNumber)
        {
            string cleaned = cardNumber.Replace(" ", "").Replace("-", "");
            return cleaned;

        }
        private string ReverseNum(string cleanedNum)
        {
            char[] charArray = cleanedNum.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private int processdigit(string num)
        {
            int sum = 0;
            for (int i = 0; i < num.Length; i++)
            {
                //multipley by 2 of even digit
                //add if 2 digit num
                //add all digits
                int digit= int.Parse(num[i].ToString());
                if (i % 2 == 1) //even 
                {
                    digit *= 2; //multipley by 2
                    if (digit > 9) //2 digit num
                    {
                        digit = digit - 9;
                    }

                }

                    sum += digit;
            }
            return sum;

        }
    }
}
