using LuwareListImplementation;
using NUnit.Framework;
using System.Linq;

namespace LuwareListTests 
{
    [TestFixture]
    public class WhenRemoving
    {
        [Test]
        public void ListReflectsTheRemoval()
        {
            var list = new LuwareList<int> {1, 2, 3};

            var result = list.Remove(2);

            Assert.IsTrue(result);
            Assert.That(list.Count(), Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] { 1, 3 }, list.ToArray());
        }

        [Test]
        public void AnEmptyListRemainsEmpty() {
            var list = new LuwareList<int>();

            var result = list.Remove(1);

            Assert.IsFalse(result);
            Assert.IsEmpty(list);
        }

        [Test]
        public void DoesNotRemoveUnexistingElement()
        {
            var list = new LuwareList<int> { 1, 2, 3 };

            var result = list.Remove(4);

            Assert.IsFalse(result);
            Assert.That(list.Count(), Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list.ToArray());
        }   

        [Test]
        public void AtTheBeginning_RearrangesTheList()
        {
            var list = new LuwareList<int> { 1, 2, 3 };

            var result = list.Remove(1);

            Assert.IsTrue(result);
            Assert.That(list.Count(), Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] { 2, 3 }, list.ToArray());
        }

        [Test]
        public void AtTheEnd_RearrangesTheList() {
            var list = new LuwareList<int> { 1, 2, 3 };

            var result = list.Remove(3);

            Assert.IsTrue(result);
            Assert.That(list.Count(), Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] { 1, 2 }, list.ToArray());
        }
    }
}
