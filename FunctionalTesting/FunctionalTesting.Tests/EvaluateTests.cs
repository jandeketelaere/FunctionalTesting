using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FunctionalTesting.EvaluateMethods;

namespace FunctionalTesting.Tests
{
    [TestClass]
    public class EvaluateTests
    {
        [TestMethod]
        public void Test()
        {
            Evaluate(1, 2, 3)
                .When(i => i == 1, i => i < 5, i => i < 6)
                    .Then(PrintYay)
                    .Else(PrintNay)
                .When(i => i == 1, i => i < 5, i => i < 6)
                    .Then(PrintYay)
                    .Else(PrintNay)
                .When(i => i > 0, i => i <= 2, i => i == 3)
                    .Then(PrintYay)
                .When(i => i >= 0, i => i == 2)
                    .Then(PrintYay);
        }

        private static void PrintYay()
        {
            Console.WriteLine("Yay!");
        }

        private static void PrintNay()
        {
            Console.WriteLine("Nay!");
        }
    }
}