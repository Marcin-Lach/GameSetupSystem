using NUnit.Framework;
using ThirdApproachDomain;

namespace ThirdApproachDomainTests
{
    [TestFixture]
    public class ZeroPositiveIntTests
    {
        [Test]
        public void EqualityCheck()
        {
            var firstZeroPositiveInt = new ZeroPositiveInt(1);
            var secondZeroPositiveInt = new ZeroPositiveInt(1);

            Assert.AreEqual(firstZeroPositiveInt, secondZeroPositiveInt);

            var thirdZeroPositiveInt = new ZeroPositiveInt(2);

            Assert.AreNotEqual(firstZeroPositiveInt, thirdZeroPositiveInt);
            Assert.AreNotEqual(secondZeroPositiveInt, thirdZeroPositiveInt);

            var somethingElse = new SomeOtherClass();
            Assert.AreNotEqual(secondZeroPositiveInt, somethingElse);
        }

        [TestCase(int.MaxValue, true)]
        [TestCase(1000, true)]
        [TestCase(10, true)]
        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        [TestCase(int.MinValue, false)]
        public void CreatingCheck(int value, bool willSucceed)
        {
            if (willSucceed)
            {
                var zeroPositive = new ZeroPositiveInt(value);
                Assert.AreEqual(value, zeroPositive.Value);
            }
            else
            {
                Assert.Throws<BusinessLogicException>(() => new ZeroPositiveInt(value));
            }
        }

        public class SomeOtherClass
        { }
    }
}