using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalcClassLibrary;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Calc myCalc = new Calc();

        [TestMethod]
        public void TestAvearge()
        {
            // Arrange
            var nums = new int[] { 1, 2, 5, 6, 8, 9 };

            // Act
            var result = myCalc.Average(nums);

            // Assert
            Assert.IsTrue(result == 5);
        }

        [TestMethod]
        public void testlargest()
        {
            // arrange
            var nums = new int[] { 1, 2, 3, 6, 8, 9 };

            // act
            var result = myCalc.Largest(nums);

            // assert
            Assert.IsTrue(result == 9);
        }

        [TestMethod]
        public void Testsmallest()
        {
            // arrange
            var nums = new int[] { 100, 2, 3, 6, 20, 8 };

            // act
            var result = myCalc.Smallest(nums);

            // assert
            Assert.IsTrue(result == 2);
        }
    }
}
