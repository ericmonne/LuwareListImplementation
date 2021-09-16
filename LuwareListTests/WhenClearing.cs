using LuwareListImplementation;
using NUnit.Framework;

namespace LuwareListTests
{
    [TestFixture]
    public class WhenClearing 
    {
        [Test]
        public void EmptiesAFilledList()
        {
            var list = new LuwareList<int> {1};

            list.Clear();

            Assert.IsEmpty(list);
        }        
        
        [Test]
        public void AnEmptyListRemainsEmpty()
        {
            var list = new LuwareList<int>();

            list.Clear();

            Assert.IsEmpty(list);
        }
    }
}
