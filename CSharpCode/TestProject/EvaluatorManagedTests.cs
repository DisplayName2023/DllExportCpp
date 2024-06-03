namespace TestProject
{

    using EvaluatorCs;

    [TestClass]
    public class EvaluatorManagedTests
    {
        [TestMethod]
        public void CalculateTest()
        {
            var eval = new EvaluatorManaged();
            var result = eval.Calculate("x * y", 3, 5);
            Assert.AreEqual(15.0, result);
        }
    }
}