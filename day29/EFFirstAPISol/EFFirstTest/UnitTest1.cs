using EFFirstAPI.Interfaces;
using EFFirstAPI.Services;

namespace EFFirstTest
{
    //unit testing :) 
    public class Tests
    {
        [Test]
        //ARRANGE varible kya kya hai
        //ACT func calling
        //ASSERT comparying result output nd excepting output


        // Expected: 40
        //But was:  4

        [TestCase(2,2,4)]
        [TestCase(2,5,7)]
        [TestCase(1,2,3)]
        public void Sumof2and2_shouldbe4(int n1,int n2,int output)
        {
            // Arrange
            int num1 = n1, num2 = n2;

            //act
           var res= sum(num1, num2);

            //assert
            Assert.AreEqual(output, res);
    }

        static public int sum(int n1,int n2)
        {
            return n1 + n2;

        }
    }
}